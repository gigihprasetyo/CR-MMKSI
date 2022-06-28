Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessValidation
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq

Public Class FrmInputClaimCBUReturn
    Inherits System.Web.UI.Page

    Private sessHelper As SessionHelper = New SessionHelper
    Dim objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private m_bLihatPrivilege As Boolean = False
    Private m_bInputPrivilege As Boolean = False
    Private m_bUbahVCDPrivilage As Boolean = False
    Private m_bUbahRetailPrivilage As Boolean = False
    Private m_bUbahWholesalesPrivilage As Boolean = False
    Dim crit As CriteriaComposite
    Dim CMClaimFacade As ChassisMasterClaimHeaderFacade = New ChassisMasterClaimHeaderFacade(User)
    Dim PODestFacade As PODestinationFacade = New PODestinationFacade(User)
    Dim DealerFacade As DealerFacade = New DealerFacade(User)
    Dim ChassisMFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
    Dim objCMLFacade As ChassisMasterLogisticCompanyFacade = New ChassisMasterLogisticCompanyFacade(User)
    Dim objPOHFacade As POHeaderFacade = New POHeaderFacade(User)
    Dim objEmailQFacade As ChassisMasterClaimEmailQueueFacade = New ChassisMasterClaimEmailQueueFacade(User)
    Dim objSHistFacade As StatusChangeHistoryFacade = New StatusChangeHistoryFacade(User)
    Dim objAppConfFacade As AppConfigFacade = New AppConfigFacade(User)
    Dim objEnumCBU As EnumCBUReturn = New EnumCBUReturn

    Private Mode As String = "New"
    Private FromUpload As Boolean = False
    Private targetDir As String
    Private cbuRetDir As String
    Private templateDir As String
    Private Const confExt As String = "ChassisMasterClaim.FileExt"
    Private Const confFileSize As String = "ChassisMasterClaim.MaxFileSize"
    Private Const confBlockFakturDesc As String = "ChassisMasterClaim.BlockFakturDesc"

    Private Const CBUReturnEmailToRSD As String = "CBUReturnEmailToRSD"
    Private Const CBUReturnEmailToWSD As String = "CBUReturnEmailToWSD"
    Private Const CBUReturnEmailToVCD As String = "CBUReturnEmailToVCD"
    Private Const EmailBCC As String = "EmailBCC"

#Region "Event Handler"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        targetDir = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        templateDir = Server.MapPath("~/DataFile/EmailTemplate/")
        cbuRetDir = KTB.DNet.Lib.WebConfig.GetValue("CBUReturnDirectory")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            ResetSession()
            ResetControl()
            InitResponClaimDdl()

            If Not IsNothing(Request.QueryString("mode")) Then
                Mode = Request.QueryString("mode").ToString
            End If

            If Not IsNothing(Request.QueryString("FromUpload")) Then
                FromUpload = True
            End If

            ViewState("Mode") = Mode
            ViewState("FromUpload") = FromUpload

            If Mode = "New" Then
                If objDealer.IsDealerDMS Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=Klaim Pengembalian Kendaraan - Pengajuan Claim")
                End If

                InitiateControl()
                RefreshGridClaim()
                RefreshGridAttachment()
            Else
                LoadData()
            End If
        End If
    End Sub

    Protected Sub dgUploadFile_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgUploadFile.ItemDataBound
        Dim RowValue As DocumentUpload = CType(e.Item.DataItem, DocumentUpload)
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim hdDocID As HiddenField = CType(e.Item.FindControl("hdDocID"), HiddenField)
        Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
        Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            lblNo = CType(e.Item.FindControl("lblNo"), Label)
            hdDocID = CType(e.Item.FindControl("hdDocID"), HiddenField)

            Dim lblDeskripsi As Label = CType(e.Item.FindControl("lblDeskripsi"), Label)
            Dim lnkbtnDownload As LinkButton = CType(e.Item.FindControl("lnkbtnDownload"), LinkButton)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgUploadFile.PageSize * dgUploadFile.CurrentPageIndex)
            lblDeskripsi.Text = RowValue.FileDescription
            lnkbtnDownload.Text = RowValue.FileName
            hdDocID.Value = RowValue.ID

            'If Not m_bInputPrivilege Or ViewState("Mode").ToString = "View" Or oLoginUser.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            '    lnkbtnEdit.Visible = False
            '    lnkbtnDelete.Visible = False
            'End If

        ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            lblNo = CType(e.Item.FindControl("lblNo"), Label)
            hdDocID = CType(e.Item.FindControl("hdDocID"), HiddenField)
            Dim txtDeskripsi As TextBox = CType(e.Item.FindControl("txtDeskripsiEdit"), TextBox)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgUploadFile.PageSize * dgUploadFile.CurrentPageIndex)
            txtDeskripsi.Text = RowValue.FileDescription
            hdDocID.Value = RowValue.ID
        End If
    End Sub

    Protected Sub dgUploadFile_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgUploadFile.ItemCommand
        Dim txtDeskripsi As TextBox
        Dim fileUpload As FileUpload
        Dim result As String = String.Empty
        Dim hdDocID As HiddenField = CType(e.Item.FindControl("hdDocID"), HiddenField)
        Dim objDocs As ArrayList
        If (CType(sessHelper.GetSession("DocumentUpload"), ArrayList) Is Nothing) Then
            objDocs = New ArrayList
        Else
            objDocs = CType(sessHelper.GetSession("DocumentUpload"), ArrayList)
        End If

        btnSimpan.Enabled = True
        btnValidasi.Enabled = True

        Select Case e.CommandName
            Case "add"
                txtDeskripsi = CType(e.Item.FindControl("txtDeskripsiNew"), TextBox)
                fileUpload = CType(e.Item.FindControl("fuUploadNew"), FileUpload)
                Dim ext As String = Path.GetExtension(fileUpload.PostedFile.FileName)

                If txtDeskripsi.Text = "" Then
                    MessageBox.Show("Remark harus diisi.")
                    Return
                ElseIf fileUpload.PostedFile.ContentLength = 0 Then
                    MessageBox.Show("Tidak ada file yang akan di upload.")
                    Return
                ElseIf Not CBUReturnValidation.IsValidFileUploadDocument(confExt, ext, result) Then
                    MessageBox.Show(String.Format("Upload File Gagal. Gunakan format file {0}", result))
                    Return
                ElseIf Not CBUReturnValidation.IsValidFileUploadSize(confFileSize, fileUpload.PostedFile.ContentLength, result) Then
                    MessageBox.Show(String.Format("Upload File Gagal. Maksimum per file {0} MB", result))
                    Return
                End If

                Dim objDoc As New DocumentUpload

                If objDocs Is Nothing Then
                    objDoc.ID = 0
                Else
                    objDoc.ID = objDocs.Count + 1
                End If

                objDoc.FileDescription = txtDeskripsi.Text
                objDoc.FileName = fileUpload.FileName
                objDoc.Type = EnumDocumentUpload.DocumentType.Chassis_Master_Claim
                objDoc.Path = UploadTemp(fileUpload.PostedFile.FileName, fileUpload.PostedFile.InputStream)
                objDoc.AttachmentData = fileUpload.PostedFile
                objDocs.Add(objDoc)

            Case "save"
                txtDeskripsi = CType(e.Item.FindControl("txtDeskripsiEdit"), TextBox)
                fileUpload = CType(e.Item.FindControl("fuUploadEdit"), FileUpload)
                Dim ext As String = Path.GetExtension(fileUpload.PostedFile.FileName)

                If txtDeskripsi.Text = "" Then
                    MessageBox.Show("Remark harus diisi.")
                    Return
                End If

                For Each objDoc As DocumentUpload In objDocs
                    If objDoc.ID = CType(hdDocID.Value, Integer) Then
                        If fileUpload.PostedFile.ContentLength <> 0 Then
                            If Not CBUReturnValidation.IsValidFileUploadDocument(confExt, ext, result) Then
                                MessageBox.Show(String.Format("Upload File Gagal. Gunakan format file {0}", result))
                                Return
                            ElseIf Not CBUReturnValidation.IsValidFileUploadSize(confFileSize, fileUpload.PostedFile.ContentLength, result) Then
                                MessageBox.Show(String.Format("Upload File Gagal. Maksimum per file {0} MB", result))
                                Return
                            Else
                                objDoc.Path = UploadTemp(fileUpload.PostedFile.FileName, fileUpload.PostedFile.InputStream)
                                objDoc.FileName = fileUpload.FileName
                                objDoc.AttachmentData = fileUpload.PostedFile
                            End If
                        End If
                        objDoc.FileDescription = txtDeskripsi.Text
                        Exit For
                    End If
                Next

                dgUploadFile.EditItemIndex = -1
                dgUploadFile.ShowFooter = True

            Case "cancel"
                dgUploadFile.EditItemIndex = -1
                dgUploadFile.ShowFooter = True

            Case "delete"
                For Each objDoc As DocumentUpload In objDocs
                    If objDoc.ID = CType(hdDocID.Value, Integer) Then
                        objDocs.Remove(objDoc)
                        Exit For
                    End If
                Next

            Case "edit"
                dgUploadFile.EditItemIndex = e.Item.ItemIndex
                dgUploadFile.ShowFooter = False
                btnSimpan.Enabled = False
                btnValidasi.Enabled = False
            Case "download"
                For Each objDoc As DocumentUpload In objDocs
                    If objDoc.ID = CType(hdDocID.Value, Integer) Then
                        If objDoc.AttachmentData Is Nothing Then
                            crit = New CriteriaComposite(New Criteria(GetType(DocumentUpload), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crit.opAnd(New Criteria(GetType(DocumentUpload), "ID", MatchType.Exact, objDoc.ID))
                            crit.opAnd(New Criteria(GetType(DocumentUpload), "Type", MatchType.Exact, 1))
                            Dim temp As ArrayList = New DocumentUploadFacade(User).Retrieve(crit)
                            If temp Is Nothing Then
                                Response.Redirect("../downloadlocal.aspx?file=" & objDoc.Path)
                            Else
                                Response.Redirect("../download.aspx?file=" & objDoc.Path)
                            End If
                        Else
                            Response.Redirect("../downloadlocal.aspx?file=" & objDoc.Path)
                        End If
                        Exit For
                    End If
                Next
        End Select

        sessHelper.SetSession("DocumentUpload", objDocs)
        RefreshGridAttachment()
        dgUploadFile.Focus()
    End Sub

    Protected Sub dgClaim_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgClaim.ItemDataBound
        Dim RowValue As ChassisMasterClaimDetail = CType(e.Item.DataItem, ChassisMasterClaimDetail)
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim hdID As HiddenField = CType(e.Item.FindControl("hdID"), HiddenField)
        Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
        Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblTipeClaimName As Label = CType(e.Item.FindControl("lblTipeClaimName"), Label)
            Dim lblClaimPoint As Label = CType(e.Item.FindControl("lblClaimPoint"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgClaim.PageSize * dgClaim.CurrentPageIndex)
            lblTipeClaimName.Text = CType(RowValue.ClaimType, EnumCBUReturn.TipeClaim).ToString.Replace("_", " ")
            lblClaimPoint.Text = RowValue.ClaimPoint

            hdID.Value = RowValue.ID

            'If Not m_bInputPrivilege Or ViewState("Mode").ToString = "View" Or oLoginUser.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            '    lnkbtnEdit.Visible = False
            '    lnkbtnDelete.Visible = False
            'End If

        ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim ddlTipeClaimEdit As DropDownList = CType(e.Item.FindControl("ddlTipeClaimEdit"), DropDownList)
            Dim txtClaimPointEdit As TextBox = CType(e.Item.FindControl("txtClaimPointEdit"), TextBox)

            RebuildClaimTypeDdl(ddlTipeClaimEdit)

            ddlTipeClaimEdit.Items.FindByValue(RowValue.ClaimType).Selected = True
            txtClaimPointEdit.Text = RowValue.ClaimPoint
            hdID.Value = RowValue.ID
        End If
    End Sub

    Protected Sub dgClaim_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgClaim.ItemCommand
        Dim hdID As HiddenField = CType(e.Item.FindControl("hdID"), HiddenField)

        Dim objClaimDets As ArrayList
        If (CType(sessHelper.GetSession("ChassisMasterClaimDetail"), ArrayList) Is Nothing) Then
            objClaimDets = New ArrayList
        Else
            objClaimDets = CType(sessHelper.GetSession("ChassisMasterClaimDetail"), ArrayList)
        End If

        btnSimpan.Enabled = True
        btnValidasi.Enabled = True

        Select Case e.CommandName
            Case "add"
                Dim ddlTipeClaimNew As DropDownList = CType(e.Item.FindControl("ddlTipeClaimNew"), DropDownList)
                Dim txtClaimPointNew As TextBox = CType(e.Item.FindControl("txtClaimPointNew"), TextBox)

                If ddlTipeClaimNew.SelectedIndex = 0 Then
                    MessageBox.Show("Tipe Claim harus dipilih.")
                    Return
                ElseIf txtClaimPointNew.Text.Equals("") Then
                    MessageBox.Show("Claim Point harus diisi.")
                    Return
                End If

                Dim objClaimDet As New ChassisMasterClaimDetail

                If objClaimDets Is Nothing Then
                    objClaimDet.ID = 0
                Else
                    objClaimDet.ID = objClaimDets.Count + 1
                End If

                objClaimDet.ClaimType = ddlTipeClaimNew.SelectedValue
                objClaimDet.ClaimPoint = txtClaimPointNew.Text

                objClaimDets.Add(objClaimDet)
            Case "save"
                Dim ddlTipeClaimEdit As DropDownList = CType(e.Item.FindControl("ddlTipeClaimEdit"), DropDownList)
                Dim txtClaimPointEdit As TextBox = CType(e.Item.FindControl("txtClaimPointEdit"), TextBox)

                'Validasi
                If ddlTipeClaimEdit.SelectedIndex = 0 Then
                    MessageBox.Show("Tipe Claim harus dipilih.")
                    Return
                ElseIf txtClaimPointEdit.Text.Equals("") Then
                    MessageBox.Show("Claim Point harus diisi.")
                    Return
                End If

                For Each objClaimDet As ChassisMasterClaimDetail In objClaimDets
                    If objClaimDet.ID = CType(hdID.Value, Integer) Then
                        objClaimDet.ClaimType = ddlTipeClaimEdit.SelectedValue
                        objClaimDet.ClaimPoint = txtClaimPointEdit.Text
                        Exit For
                    End If
                Next

                dgClaim.EditItemIndex = -1
                dgClaim.ShowFooter = True

            Case "cancel"
                dgClaim.EditItemIndex = -1
                dgClaim.ShowFooter = True

            Case "delete"
                For Each objClaimDet As ChassisMasterClaimDetail In objClaimDets
                    If objClaimDet.ID = CType(hdID.Value, Integer) Then
                        objClaimDets.Remove(objClaimDet)
                        Exit For
                    End If
                Next

            Case "edit"
                dgClaim.EditItemIndex = e.Item.ItemIndex
                dgClaim.ShowFooter = False
                btnSimpan.Enabled = False
                btnValidasi.Enabled = False
        End Select

        sessHelper.SetSession("ChassisMasterClaimDetail", objClaimDets)
        RefreshGridClaim()
        dgClaim.Focus()
    End Sub

    Protected Sub ddlTipeClaimNew_PreRender(sender As Object, e As EventArgs)
        Dim ddl As DropDownList = sender
        RebuildClaimTypeDdl(ddl)
    End Sub

    Protected Sub txtDealerName_TextChanged(sender As Object, e As EventArgs)
        ddlKodeDestClaim.Items.Clear()
        If Not txtDealerName.Text.Equals("") Then

            RebuildPODestDDL(txtDealerName.Text.Split("-")(0).Trim)

            lblPopupChassis.Visible = True
            lblPopupDO.Visible = True
            btnGetInfoChassis.Visible = True
            btnGetInfoDO.Visible = True
            txtNoChassis.Enabled = True
            txtNoDO.Enabled = True
        Else
            lblPopupChassis.Visible = False
            lblPopupDO.Visible = False
            btnGetInfoChassis.Visible = False
            btnGetInfoDO.Visible = False
            txtNoChassis.Enabled = False
            txtNoDO.Enabled = False
            ddlKodeDestClaim.Items.Insert(0, "Silahkan Pilih")
        End If
    End Sub

    Protected Sub btnGetInfoChassis_Click(sender As Object, e As ImageClickEventArgs)
        GetChassisDO(1)
    End Sub

    Protected Sub btnGetInfoDO_Click(sender As Object, e As ImageClickEventArgs)
        GetChassisDO(2)
    End Sub

    Protected Sub ddlRespon_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlRespon.SelectedIndex <> 0 Then
            ReEnabledControl(CInt(ddlRespon.SelectedValue))
        Else
            ReEnabledControl(0)
            'txtNoMesin.Text = ""
            txtNominal.Text = ""
            ictTglActFin.Value = DateTime.Now
            ictTglEst.Value = DateTime.Now
            ictTglTrf.Value = DateTime.Now
        End If
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs)
        Dim objChassisMasterClaimHeader As ChassisMasterClaimHeader = CType(sessHelper.GetSession("ChassisMasterClaimHeader"), ChassisMasterClaimHeader)
        Dim objDocs As ArrayList = IIf(CType(sessHelper.GetSession("DocumentUpload"), ArrayList) Is Nothing, New ArrayList, CType(sessHelper.GetSession("DocumentUpload"), ArrayList))
        Dim objExistDocs As ArrayList = New ArrayList

        If hdConfirm.Value = "-1" Then
            Dim confMsg As String = "Anda yakin mau simpan?"

            If Not ViewState("Mode").ToString = "New" Then
                If objChassisMasterClaimHeader.StatusID = EnumCBUReturn.StatusClaim.Konfirmasi And Not objChassisMasterClaimHeader.IsFromUpload Then
                    If Not CBUReturnValidation.IsValidConfirm(objChassisMasterClaimHeader, objDocs.Cast(Of DocumentUpload).ToList, confMsg) Then
                        confMsg = String.Format("Anda yakin mau simpan? \n {0}", confMsg)
                    End If
                End If
            End If

            RegisterStartupScript("Confirm", String.Format("<script>ShowConfirm('{0}', 'btnSimpan');</script>", confMsg))
            Return
        Else
            hdConfirm.Value = "-1"
        End If

        Dim objChassisMasterClaimDetails As ArrayList = IIf(CType(sessHelper.GetSession("ChassisMasterClaimDetail"), ArrayList) Is Nothing, New ArrayList, CType(sessHelper.GetSession("ChassisMasterClaimDetail"), ArrayList))

        If Not ViewState("Mode").ToString = "New" Then
            objExistDocs = objChassisMasterClaimHeader.DocumentUploads
        Else
            objChassisMasterClaimHeader = New ChassisMasterClaimHeader
            objChassisMasterClaimHeader.ClaimNumber = GenerateCode(objDealer.DealerCode, DateTime.Now.Year, DateTime.Now.Month)
            objChassisMasterClaimHeader.StatusID = CType(EnumCBUReturn.StatusClaim.Baru, Short)
        End If

        objChassisMasterClaimHeader = PopulateChassisMasterClaimHeader(objChassisMasterClaimHeader)
        Dim statusClaimInSession As Integer = objChassisMasterClaimHeader.StatusID

        'To make sure the status was not changed by other process in realtime
        objChassisMasterClaimHeader.StatusID = GetCurrentStatus(objChassisMasterClaimHeader.ID)

        If statusClaimInSession = objChassisMasterClaimHeader.StatusID Then
            Dim errMsg As String = String.Empty
            If Not CBUReturnValidation.IsValidClaim(objChassisMasterClaimHeader, objChassisMasterClaimDetails.Cast(Of ChassisMasterClaimDetail).ToList, objDocs.Cast(Of DocumentUpload).ToList, errMsg, objChassisMasterClaimHeader.StatusID) Then
                If errMsg = CBUReturnValidation.STATUS_CLAIM_NOT_VALID Then
                    LoadData(objChassisMasterClaimHeader.ID)
                End If
                MessageBox.Show(errMsg)
                Exit Sub
            End If
        Else
            LoadData(objChassisMasterClaimHeader.ID)
            MessageBox.Show(CBUReturnValidation.STATUS_CLAIM_NOT_VALID)
            Exit Sub
        End If

        If objChassisMasterClaimHeader.StatusID <> EnumCBUReturn.StatusClaim.Konfirmasi Or (objChassisMasterClaimHeader.StatusID = EnumCBUReturn.StatusClaim.Konfirmasi And objChassisMasterClaimHeader.IsFromUpload) Then
            If Not UploadFile(objDocs, objChassisMasterClaimHeader.ClaimNumber, objExistDocs) Then
                Exit Sub
            End If
        End If

        Dim result As Integer = 0

        If ViewState("Mode").ToString = "New" Then
            objChassisMasterClaimHeader.StatusStockDMS = EnumCBUReturn.StatusStockDMS.Available
            result = CMClaimFacade.Insert(objChassisMasterClaimHeader, objChassisMasterClaimDetails, objDocs)
        Else
            result = CMClaimFacade.Update(objChassisMasterClaimHeader, objChassisMasterClaimDetails, objDocs, Nothing, Nothing)
        End If

        If result > 0 Then
            'Dim msg As String = String.Format("Data Berhasil disimpan dengan Nomor Claim {0}", objChassisMasterClaimHeader.ClaimNumber)
            Dim msg As String = "Data Berhasil disimpan"
            MessageBox.Show(msg)
            ViewState("Mode") = "Edit"
            LoadData(result)
        Else
            MessageBox.Show("Simpan Gagal")
        End If
    End Sub

    Protected Sub btnValidasi_Click(sender As Object, e As EventArgs)
        If hdConfirm.Value = "-1" Then
            RegisterStartupScript("Confirm", "<script>ShowConfirm('Anda yakin mau validasi?', 'btnValidasi');</script>")
            Return
        Else
            hdConfirm.Value = "-1"
        End If

        Dim objChassisMasterClaimHeader As ChassisMasterClaimHeader = CType(sessHelper.GetSession("ChassisMasterClaimHeader"), ChassisMasterClaimHeader)
        Dim objDocs As ArrayList = IIf(CType(sessHelper.GetSession("DocumentUpload"), ArrayList) Is Nothing, New ArrayList, CType(sessHelper.GetSession("DocumentUpload"), ArrayList))
        Dim objExistDocs As ArrayList = New ArrayList
        Dim objChassisMasterClaimDetails As ArrayList = IIf(CType(sessHelper.GetSession("ChassisMasterClaimDetail"), ArrayList) Is Nothing, New ArrayList, CType(sessHelper.GetSession("ChassisMasterClaimDetail"), ArrayList))

        If Not objChassisMasterClaimHeader Is Nothing Then
            objExistDocs = objChassisMasterClaimHeader.DocumentUploads
        End If

        objChassisMasterClaimHeader = PopulateChassisMasterClaimHeader(objChassisMasterClaimHeader)

        Dim statusClaimInSession As Integer = objChassisMasterClaimHeader.StatusID
        'To make sure the status was not changed by other process in realtime
        objChassisMasterClaimHeader.StatusID = GetCurrentStatus(objChassisMasterClaimHeader.ID)
        If statusClaimInSession = objChassisMasterClaimHeader.StatusID Then
            Dim errMsg As String = String.Empty
            If Not CBUReturnValidation.IsValidClaim(objChassisMasterClaimHeader, objChassisMasterClaimDetails.Cast(Of ChassisMasterClaimDetail).ToList, objDocs.Cast(Of DocumentUpload).ToList, errMsg, EnumCBUReturn.StatusClaim.Validasi) Then
                If errMsg = CBUReturnValidation.STATUS_CLAIM_NOT_VALID Then
                    LoadData(objChassisMasterClaimHeader.ID)
                End If
                MessageBox.Show(errMsg)
                Exit Sub
            End If
        Else
            LoadData(objChassisMasterClaimHeader.ID)
            MessageBox.Show(CBUReturnValidation.STATUS_CLAIM_NOT_VALID)
            Exit Sub
        End If

        If Not UploadFile(objDocs, objChassisMasterClaimHeader.ClaimNumber, objExistDocs) Then
            Exit Sub
        End If

        Dim result As Integer = 0

        Dim objStatusChangeHistory As New StatusChangeHistory
        objStatusChangeHistory.DocumentRegNumber = objChassisMasterClaimHeader.ClaimNumber
        objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ClaimStatus)
        objStatusChangeHistory.OldStatus = objChassisMasterClaimHeader.StatusID
        objStatusChangeHistory.NewStatus = CType(EnumCBUReturn.StatusClaim.Validasi, Short)

        objChassisMasterClaimHeader.StatusID = CType(EnumCBUReturn.StatusClaim.Validasi, Short)

        'Send Email to queue
        Dim objEmailQueues As ArrayList = GetExistEmailQueue(objChassisMasterClaimHeader.ClaimNumber)
        Dim objEmailQueue As ChassisMasterClaimEmailQueue = New ChassisMasterClaimEmailQueue
        objEmailQueue.ClaimNumber = objChassisMasterClaimHeader.ClaimNumber
        objEmailQueue.StatusClaim = CType(EnumCBUReturn.StatusClaim.Validasi, Short)
        objEmailQueue.IsSend = 0
        objEmailQueues.Add(objEmailQueue)

        result = CMClaimFacade.Update(objChassisMasterClaimHeader, objChassisMasterClaimDetails, objDocs, objStatusChangeHistory, objEmailQueues)

        If result > 0 Then
            Dim msg As String = "Simpan Berhasil"
            MessageBox.Show(msg)
            'ViewState("Mode") = "View"
            LoadData(result)
        Else
            MessageBox.Show("Simpan Gagal")
        End If
    End Sub

    Protected Sub btnRevisi_Click(sender As Object, e As EventArgs)
        If hdConfirm.Value = "-1" Then
            RegisterStartupScript("Confirm", "<script>ShowConfirm('Anda yakin mau revisi?', 'btnRevisi');</script>")
            Return
        Else
            hdConfirm.Value = "-1"
        End If

        Dim result As Integer = 0
        Dim objChassisMasterClaimHeader As ChassisMasterClaimHeader = CType(sessHelper.GetSession("ChassisMasterClaimHeader"), ChassisMasterClaimHeader)
        Dim objStatusChangeHistory As New StatusChangeHistory
        Dim objStatusChanges As ArrayList = New ArrayList
        Dim statusClaimInSession As Integer = objChassisMasterClaimHeader.StatusID

        'To make sure the status was not changed by other process in realtime
        objChassisMasterClaimHeader.StatusID = GetCurrentStatus(objChassisMasterClaimHeader.ID)
        If statusClaimInSession = objChassisMasterClaimHeader.StatusID Then
            Dim errMsg As String = String.Empty
            If Not CBUReturnValidation.IsValidClaim(objChassisMasterClaimHeader, Nothing, Nothing, errMsg, EnumCBUReturn.StatusClaim.Revisi) Then
                If errMsg = CBUReturnValidation.STATUS_CLAIM_NOT_VALID Then
                    LoadData(objChassisMasterClaimHeader.ID)
                End If
                MessageBox.Show(errMsg)
                Exit Sub
            End If
        Else
            LoadData(objChassisMasterClaimHeader.ID)
            MessageBox.Show(CBUReturnValidation.STATUS_CLAIM_NOT_VALID)
            Exit Sub
        End If

        objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ClaimStatus)
        objStatusChangeHistory.OldStatus = objChassisMasterClaimHeader.StatusID
        objStatusChangeHistory.NewStatus = CType(EnumCBUReturn.StatusClaim.Revisi, Short)
        objStatusChanges.Add(objStatusChangeHistory)

        objChassisMasterClaimHeader.StatusID = CType(EnumCBUReturn.StatusClaim.Revisi, Short)
        objChassisMasterClaimHeader.Remark = txtNote.Text

        'Update Email queue to IsSend = 2
        Dim objEmailQueues As ArrayList = GetExistEmailQueue(objChassisMasterClaimHeader.ClaimNumber)
        result = CMClaimFacade.Update(objChassisMasterClaimHeader, objStatusChanges, Nothing, objEmailQueues)

        If result > 0 Then
            Dim msg As String = "Simpan Berhasil"
            MessageBox.Show(msg)
            'ViewState("Mode") = "View"
            LoadData(result)
        Else
            MessageBox.Show("Simpan Gagal")
        End If
    End Sub

    Protected Sub btnKonfirmasi_Click(sender As Object, e As EventArgs)
        If hdConfirm.Value = "-1" Then
            RegisterStartupScript("Confirm", "<script>ShowConfirm('Anda yakin mau konfirmasi?', 'btnKonfirmasi');</script>")
            Return
        Else
            hdConfirm.Value = "-1"
        End If

        Dim result As Integer = 0
        Dim objChassisMasterClaimHeader As ChassisMasterClaimHeader = CType(sessHelper.GetSession("ChassisMasterClaimHeader"), ChassisMasterClaimHeader)
        Dim objStatusChangeHistory As New StatusChangeHistory
        Dim objStatusChanges As ArrayList = New ArrayList
        Dim statusClaimInSession As Integer = objChassisMasterClaimHeader.StatusID

        'To make sure the status was not changed by other process in realtime
        objChassisMasterClaimHeader.StatusID = GetCurrentStatus(objChassisMasterClaimHeader.ID)
        If statusClaimInSession = objChassisMasterClaimHeader.StatusID Then
            Dim errMsg As String = String.Empty
            If Not CBUReturnValidation.IsValidClaim(objChassisMasterClaimHeader, Nothing, Nothing, errMsg, EnumCBUReturn.StatusClaim.Konfirmasi) Then
                If errMsg = CBUReturnValidation.STATUS_CLAIM_NOT_VALID Then
                    LoadData(objChassisMasterClaimHeader.ID)
                End If
                MessageBox.Show(errMsg)
                Exit Sub
            End If
        Else
            LoadData(objChassisMasterClaimHeader.ID)
            MessageBox.Show(CBUReturnValidation.STATUS_CLAIM_NOT_VALID)
            Exit Sub
        End If

        objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ClaimStatus)
        objStatusChangeHistory.OldStatus = objChassisMasterClaimHeader.StatusID
        objStatusChangeHistory.NewStatus = CType(EnumCBUReturn.StatusClaim.Konfirmasi, Short)
        objStatusChanges.Add(objStatusChangeHistory)

        objChassisMasterClaimHeader.StatusID = CType(EnumCBUReturn.StatusClaim.Konfirmasi, Short)

        'Update Email queue to IsSend = 2
        Dim objEmailQueues As ArrayList = GetExistEmailQueue(objChassisMasterClaimHeader.ClaimNumber)
        result = CMClaimFacade.Update(objChassisMasterClaimHeader, objStatusChanges, Nothing, objEmailQueues)

        If result > 0 Then
            Dim msg As String = "Simpan Berhasil"
            MessageBox.Show(msg)
            'ViewState("Mode") = "Edit"
            LoadData(result)
        Else
            MessageBox.Show("Simpan Gagal")
        End If
    End Sub

    Protected Sub btnSend_Click(sender As Object, e As EventArgs)
        Dim result As Integer = 0
        Dim objChassisMasterClaimHeader As ChassisMasterClaimHeader = CType(sessHelper.GetSession("ChassisMasterClaimHeader"), ChassisMasterClaimHeader)
        Dim objCBUReturnSendSAP As CBUReturnSendSAP = New CBUReturnSendSAP
        Dim statusProsesRetur As Integer = 0
        Dim statusReturInSession As Integer = objChassisMasterClaimHeader.StatusProcessRetur
        Dim statusClaimInSession As Integer = objChassisMasterClaimHeader.StatusID
        Dim objStatusChanges As ArrayList = New ArrayList
        Dim objStatusChangeHistory As StatusChangeHistory
        Dim objEmailQueues As ArrayList = New ArrayList
        Dim objChassisMasters As ArrayList = New ArrayList
        Dim objEmailQueue As ChassisMasterClaimEmailQueue
        Dim isFromCancelled As Boolean = False
        Dim endCustomerId As Integer = 0

        If Not objChassisMasterClaimHeader.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
            objChassisMasterClaimHeader = PopulateChassisMasterClaimHeader(objChassisMasterClaimHeader)
        End If

        'To make sure the status was not changed by other process in realtime
        objChassisMasterClaimHeader.StatusID = GetCurrentStatus(objChassisMasterClaimHeader.ID, statusProsesRetur)

        Dim StatusChangeTo As Integer = IIf(statusProsesRetur = EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti, _
                                            EnumCBUReturn.StatusClaim.Selesai, EnumCBUReturn.StatusClaim.Proses)

        If statusClaimInSession = objChassisMasterClaimHeader.StatusID Then
            Dim errMsg As String = String.Empty
            If Not CBUReturnValidation.IsValidClaim(objChassisMasterClaimHeader, objChassisMasterClaimHeader.ChassisMasterClaimDetails.Cast(Of ChassisMasterClaimDetail).ToList, objChassisMasterClaimHeader.DocumentUploads.Cast(Of DocumentUpload).ToList, errMsg, StatusChangeTo) Then
                If errMsg = CBUReturnValidation.STATUS_CLAIM_NOT_VALID Then
                    LoadData(objChassisMasterClaimHeader.ID)
                ElseIf errMsg <> CBUReturnValidation.STATUS_STOCK_DMS_NOT_VALID Then
                    errMsg = errMsg + " Mohon untuk melakukan simpan terlebih dahulu."
                End If
                MessageBox.Show(errMsg)
                Exit Sub
            End If
        Else
            LoadData(objChassisMasterClaimHeader.ID)
            MessageBox.Show(CBUReturnValidation.STATUS_CLAIM_NOT_VALID)
            Exit Sub
        End If

        If objChassisMasterClaimHeader.ResponClaim = EnumCBUReturn.RespondClaim.Ganti_Unit Then
            'To make sure the status return was not changed by other process in realtime
            If statusProsesRetur <> statusReturInSession Then
                LoadData(objChassisMasterClaimHeader.ID)
                MessageBox.Show(CBUReturnValidation.STATUS_PROSES_RETUR_NOT_VALID)
                Exit Sub
            Else
                objChassisMasterClaimHeader.StatusProcessRetur = statusProsesRetur
            End If

            If objChassisMasterClaimHeader.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
                endCustomerId = ChassisMFacade.Retrieve(objChassisMasterClaimHeader.ChassisNumberReplacement).EndCustomerID
            Else
                endCustomerId = objChassisMasterClaimHeader.ChassisMaster.EndCustomerID
            End If

            objStatusChangeHistory = New StatusChangeHistory
            objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ReturStatus)
            objStatusChangeHistory.OldStatus = objChassisMasterClaimHeader.StatusProcessRetur

            'Update Email queue to IsSend = 2
            objEmailQueues = GetExistEmailQueue(objChassisMasterClaimHeader.ClaimNumber)

            If objChassisMasterClaimHeader.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Faktur_sudah_di_print Then
                If hdConfirm.Value = "-1" Then
                    RegisterStartupScript("Confirm", "<script>ShowConfirm('Apakah faktur sudah dikembalikan kepada MMKSI?', 'btnSend');</script>")
                    Return
                Else
                    hdConfirm.Value = "-1"
                End If

                objChassisMasterClaimHeader.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Cancel_Faktur
            ElseIf objChassisMasterClaimHeader.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
                If Not TransferWSM(0, objChassisMasterClaimHeader) Then
                    Exit Sub
                End If

                objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ClaimStatus)
                objStatusChangeHistory.OldStatus = objChassisMasterClaimHeader.StatusID
                objStatusChangeHistory.NewStatus = CType(EnumCBUReturn.StatusClaim.Selesai, Short)
                objStatusChanges.Add(objStatusChangeHistory)

                objStatusChangeHistory = New StatusChangeHistory
                objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ReturStatus)
                objStatusChangeHistory.OldStatus = objChassisMasterClaimHeader.StatusProcessRetur
                objStatusChangeHistory.NewStatus = CType(EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti, Short)
                objStatusChanges.Add(objStatusChangeHistory)

                objChassisMasterClaimHeader.StatusID = CType(EnumCBUReturn.StatusClaim.Selesai, Short)
            Else
                If Me.txtPass.Text = String.Empty Then
                    RegisterStartupScript("OpenWindow", "<script>showPPPassword();</script>")
                    Return
                End If

                objCBUReturnSendSAP.SAPConn = KTB.DNet.Lib.WebConfig.GetValue("SAPConnectionStringEmpty") 'For test using "SAPConnectionString"
                objCBUReturnSendSAP.Username = txtUser.Text
                objCBUReturnSendSAP.Password = txtPass.Text
                objCBUReturnSendSAP.CurrentStatusRetur = statusProsesRetur
                objCBUReturnSendSAP.ChassisClaimHeaders.Add(objChassisMasterClaimHeader)

                If Not CBUReturnValidation.IsValidRetur(objCBUReturnSendSAP) Or objCBUReturnSendSAP.Message <> "" Then
                    SaveToWSLog(objCBUReturnSendSAP.GetBodyResponse)
                    MessageBox.Show(objCBUReturnSendSAP.Message)
                    Exit Sub
                End If
                SaveToWSLog(objCBUReturnSendSAP.GetBodyResponse)

                If statusProsesRetur = 0 Then
                    Dim chassisM As ChassisMaster = ChassisMFacade.Retrieve(objCBUReturnSendSAP.SapResponses(0).ChassisPengganti)
                    If chassisM.ID <> 0 Then
                        If chassisM.EndCustomerID <> 0 Then
                            MessageBox.Show(String.Format("Data Chassis Pengganti : {0} sudah ada", chassisM.ChassisNumber))
                            Exit Sub
                        Else
                            chassisM.RowStatus = -1
                            objChassisMasters.Add(chassisM)
                        End If
                    End If
                End If

                objChassisMasterClaimHeader = CType(objCBUReturnSendSAP.ChassisClaimHeaders(0), ChassisMasterClaimHeader)
            End If

            objChassisMasters = AfterReturnSAPProcess(objChassisMasterClaimHeader, objCBUReturnSendSAP, objChassisMasters, statusProsesRetur, isFromCancelled)

            If objStatusChangeHistory.OldStatus <> EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
                objStatusChangeHistory.NewStatus = objChassisMasterClaimHeader.StatusProcessRetur
                objStatusChanges.Add(objStatusChangeHistory)
            End If

            If objChassisMasterClaimHeader.StatusID = EnumCBUReturn.StatusClaim.Selesai Then
                objStatusChangeHistory = New StatusChangeHistory
                objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ClaimStatus)
                objStatusChangeHistory.OldStatus = statusClaimInSession
                objStatusChangeHistory.NewStatus = objChassisMasterClaimHeader.StatusID
                objStatusChanges.Add(objStatusChangeHistory)
            End If

            If objChassisMasterClaimHeader.StatusProcessRetur <> 0 And objChassisMasterClaimHeader.StatusProcessRetur <> EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
                'Send Email to queue
                objEmailQueue = New ChassisMasterClaimEmailQueue
                objEmailQueue.ClaimNumber = objChassisMasterClaimHeader.ClaimNumber
                objEmailQueue.StatusReturnProcess = objChassisMasterClaimHeader.StatusProcessRetur
                objEmailQueue.StatusClaim = 0
                objEmailQueue.IsSend = 0
                objEmailQueues.Add(objEmailQueue)
            End If
        Else
            objChassisMasters = Nothing
        End If

        If objChassisMasterClaimHeader.StatusID = EnumCBUReturn.StatusClaim.Konfirmasi Then
            objStatusChangeHistory = New StatusChangeHistory
            objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ClaimStatus)
            objStatusChangeHistory.OldStatus = objChassisMasterClaimHeader.StatusID
            objStatusChangeHistory.NewStatus = CType(EnumCBUReturn.StatusClaim.Proses, Short)
            objStatusChanges.Add(objStatusChangeHistory)

            objChassisMasterClaimHeader.StatusID = CType(EnumCBUReturn.StatusClaim.Proses, Short)

            result = CMClaimFacade.Update(objChassisMasterClaimHeader, objStatusChanges, objChassisMasters, objEmailQueues)
        Else
            result = CMClaimFacade.Update(objChassisMasterClaimHeader, objStatusChanges, objChassisMasters, objEmailQueues)
        End If

        If result > 0 Then
            Dim msg As String = "Simpan Berhasil. \n"
            Dim statusReturList As String = String.Format("{0},{1},{2}", _
                CInt(EnumCBUReturn.StatusProsesRetur.Reverse_DO), CInt(EnumCBUReturn.StatusProsesRetur.Sales_Replacement), CInt(EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti))
            If statusReturList.Contains(statusProsesRetur) And endCustomerId <> 0 Then
                Dim emailTo As String = GetEmail("RSD")
                Dim emailBcc As String = GetEmail("EmailBCC")
                SendEmail(objChassisMasterClaimHeader, emailTo, emailBcc, isFromCancelled, msg)
            End If
            MessageBox.Show(msg)
            LoadData(result)
        Else
            MessageBox.Show("Simpan Gagal")
        End If

    End Sub

    Protected Sub btnTolak_Click(sender As Object, e As EventArgs)
        If hdConfirm.Value = "-1" Then
            RegisterStartupScript("Confirm", "<script>ShowConfirm('Anda yakin mau tolak?', 'btnTolak');</script>")
            Return
        Else
            hdConfirm.Value = "-1"
        End If

        Dim result As Integer = 0
        Dim objChassisMasterClaimHeader As ChassisMasterClaimHeader = CType(sessHelper.GetSession("ChassisMasterClaimHeader"), ChassisMasterClaimHeader)
        Dim objStatusChangeHistory As New StatusChangeHistory
        Dim objStatusChanges As ArrayList = New ArrayList
        Dim statusClaimInSession As Integer = objChassisMasterClaimHeader.StatusID

        'To make sure the status was not changed by other process in realtime
        objChassisMasterClaimHeader.StatusID = GetCurrentStatus(objChassisMasterClaimHeader.ID)

        If statusClaimInSession = objChassisMasterClaimHeader.StatusID Then
            Dim errMsg As String = String.Empty
            If Not CBUReturnValidation.IsValidClaim(objChassisMasterClaimHeader, Nothing, Nothing, errMsg, EnumCBUReturn.StatusClaim.Tolak) Then
                If errMsg = CBUReturnValidation.STATUS_CLAIM_NOT_VALID Then
                    LoadData(objChassisMasterClaimHeader.ID)
                End If
                MessageBox.Show(errMsg)
                Exit Sub
            End If
        Else
            LoadData(objChassisMasterClaimHeader.ID)
            MessageBox.Show(CBUReturnValidation.STATUS_CLAIM_NOT_VALID)
            Exit Sub
        End If

        objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ClaimStatus)
        objStatusChangeHistory.OldStatus = objChassisMasterClaimHeader.StatusID
        objStatusChangeHistory.NewStatus = CType(EnumCBUReturn.StatusClaim.Tolak, Short)
        objStatusChanges.Add(objStatusChangeHistory)

        objChassisMasterClaimHeader.StatusID = CType(EnumCBUReturn.StatusClaim.Tolak, Short)
        objChassisMasterClaimHeader.Remark = txtNote.Text

        'Update Email queue to IsSend = 2
        Dim objEmailQueues As ArrayList = GetExistEmailQueue(objChassisMasterClaimHeader.ClaimNumber)
        result = CMClaimFacade.Update(objChassisMasterClaimHeader, objStatusChanges, Nothing, objEmailQueues)

        If result > 0 Then
            Dim msg As String = "Simpan Berhasil"
            MessageBox.Show(msg)
            'ViewState("Mode") = "View"
            LoadData(result)
        Else
            MessageBox.Show("Simpan Gagal")
        End If
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs)
        If hdConfirm.Value = "-1" Then
            RegisterStartupScript("Confirm", "<script>ShowConfirm('Anda yakin mau batal validasi?', 'btnBatal');</script>")
            Return
        Else
            hdConfirm.Value = "-1"
        End If

        Dim result As Integer = 0
        Dim objChassisMasterClaimHeader As ChassisMasterClaimHeader = CType(sessHelper.GetSession("ChassisMasterClaimHeader"), ChassisMasterClaimHeader)
        Dim objStatusChangeHistory As New StatusChangeHistory
        Dim objStatusChanges As ArrayList = New ArrayList
        Dim statusClaimInSession As Integer = objChassisMasterClaimHeader.StatusID

        'To make sure the status was not changed by other process in realtime
        objChassisMasterClaimHeader.StatusID = GetCurrentStatus(objChassisMasterClaimHeader.ID)
        If statusClaimInSession = objChassisMasterClaimHeader.StatusID Then
            Dim errMsg As String = String.Empty
            If Not CBUReturnValidation.IsValidClaim(objChassisMasterClaimHeader, Nothing, Nothing, errMsg, EnumCBUReturn.StatusClaim.Baru) Then
                If errMsg = CBUReturnValidation.STATUS_CLAIM_NOT_VALID Then
                    LoadData(objChassisMasterClaimHeader.ID)
                End If
                MessageBox.Show(errMsg)
                Exit Sub
            End If
        Else
            LoadData(objChassisMasterClaimHeader.ID)
            MessageBox.Show(CBUReturnValidation.STATUS_CLAIM_NOT_VALID)
            Exit Sub
        End If

        objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ClaimStatus)
        objStatusChangeHistory.OldStatus = objChassisMasterClaimHeader.StatusID
        objStatusChangeHistory.NewStatus = CType(EnumCBUReturn.StatusClaim.Baru, Short)
        objStatusChanges.Add(objStatusChangeHistory)

        objChassisMasterClaimHeader.StatusID = CType(EnumCBUReturn.StatusClaim.Baru, Short)

        'Update Email queue to IsSend = 2
        Dim objEmailQueues As ArrayList = GetExistEmailQueue(objChassisMasterClaimHeader.ClaimNumber)
        result = CMClaimFacade.Update(objChassisMasterClaimHeader, objStatusChanges, Nothing, objEmailQueues)

        If result > 0 Then
            Dim msg As String = "Simpan Berhasil"
            MessageBox.Show(msg)
            'ViewState("Mode") = "View"
            LoadData(result)
        Else
            MessageBox.Show("Simpan Gagal")
        End If
    End Sub

    Protected Sub btnCancelProses_Click(sender As Object, e As EventArgs)
        If hdConfirm.Value = "-1" Then
            RegisterStartupScript("Confirm", "<script>ShowConfirm('Anda yakin mau cancel proses?', 'btnCancelProses');</script>")
            Return
        Else
            hdConfirm.Value = "-1"
        End If

        Dim result As Integer = 0
        Dim objChassisMasterClaimHeader As ChassisMasterClaimHeader = CType(sessHelper.GetSession("ChassisMasterClaimHeader"), ChassisMasterClaimHeader)
        Dim objStatusChangeHistory As New StatusChangeHistory
        Dim objStatusChanges As ArrayList = New ArrayList
        Dim statusClaimInSession As Integer = objChassisMasterClaimHeader.StatusID

        'To make sure the status was not changed by other process in realtime
        objChassisMasterClaimHeader.StatusID = GetCurrentStatus(objChassisMasterClaimHeader.ID)
        If statusClaimInSession = objChassisMasterClaimHeader.StatusID Then
            Dim errMsg As String = String.Empty
            If Not CBUReturnValidation.IsValidClaim(objChassisMasterClaimHeader, Nothing, Nothing, errMsg, EnumCBUReturn.StatusClaim.Konfirmasi) Then
                If errMsg = CBUReturnValidation.STATUS_CLAIM_NOT_VALID Then
                    LoadData(objChassisMasterClaimHeader.ID)
                End If
                MessageBox.Show(errMsg)
                Exit Sub
            End If
        Else
            LoadData(objChassisMasterClaimHeader.ID)
            MessageBox.Show(CBUReturnValidation.STATUS_CLAIM_NOT_VALID)
            Exit Sub
        End If

        objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ClaimStatus)
        objStatusChangeHistory.OldStatus = objChassisMasterClaimHeader.StatusID
        objStatusChangeHistory.NewStatus = CType(EnumCBUReturn.StatusClaim.Konfirmasi, Short)
        objStatusChanges.Add(objStatusChangeHistory)

        objStatusChangeHistory = New StatusChangeHistory()
        objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ReturStatus)
        objStatusChangeHistory.OldStatus = objChassisMasterClaimHeader.StatusProcessRetur
        objStatusChangeHistory.NewStatus = CType(EnumCBUReturn.StatusProsesRetur.Send_To_SAP, Short)
        objStatusChanges.Add(objStatusChangeHistory)

        objChassisMasterClaimHeader.ChassisNumberReplacement = ""
        objChassisMasterClaimHeader.StatusID = CType(EnumCBUReturn.StatusClaim.Konfirmasi, Short)
        objChassisMasterClaimHeader.StatusProcessRetur = 0

        Dim objChassisMasterList As ArrayList = New ArrayList
        Dim chassisM As ChassisMaster = ChassisMFacade.Retrieve(objChassisMasterClaimHeader.ChassisMaster.ChassisNumber)
        chassisM.PendingDesc = ""
        objChassisMasterList.Add(chassisM)

        'Update Email queue to IsSend = 2
        Dim objEmailQueues As ArrayList = GetExistEmailQueue(objChassisMasterClaimHeader.ClaimNumber)
        result = CMClaimFacade.Update(objChassisMasterClaimHeader, objStatusChanges, objChassisMasterList, objEmailQueues)

        If result > 0 Then
            Dim msg As String = "Simpan Berhasil"
            MessageBox.Show(msg)
            'ViewState("Mode") = "View"
            LoadData(result)
        Else
            MessageBox.Show("Simpan Gagal")
        End If
    End Sub

    Protected Sub btnSelesai_Click(sender As Object, e As EventArgs)
        If hdConfirm.Value = "-1" Then
            RegisterStartupScript("Confirm", "<script>ShowConfirm('Anda yakin mau selesai?', 'btnSelesai');</script>")
            Return
        Else
            hdConfirm.Value = "-1"
        End If

        Dim result As Integer = 0
        Dim objChassisMasterClaimHeader As ChassisMasterClaimHeader = CType(sessHelper.GetSession("ChassisMasterClaimHeader"), ChassisMasterClaimHeader)
        Dim objStatusChangeHistory As New StatusChangeHistory
        Dim objStatusChanges As ArrayList = New ArrayList
        Dim statusClaimInSession As Integer = objChassisMasterClaimHeader.StatusID

        objChassisMasterClaimHeader = PopulateChassisMasterClaimHeader(objChassisMasterClaimHeader)
        'To make sure the status was not changed by other process in realtime
        objChassisMasterClaimHeader.StatusID = GetCurrentStatus(objChassisMasterClaimHeader.ID)
        If statusClaimInSession = objChassisMasterClaimHeader.StatusID Then
            Dim errMsg As String = String.Empty
            If Not CBUReturnValidation.IsValidClaim(objChassisMasterClaimHeader, Nothing, Nothing, errMsg, EnumCBUReturn.StatusClaim.Selesai) Then
                If errMsg = CBUReturnValidation.STATUS_CLAIM_NOT_VALID Then
                    LoadData(objChassisMasterClaimHeader.ID)
                End If
                MessageBox.Show(errMsg)
                Exit Sub
            End If
        Else
            LoadData(objChassisMasterClaimHeader.ID)
            MessageBox.Show(CBUReturnValidation.STATUS_CLAIM_NOT_VALID)
            Exit Sub
        End If

        objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ClaimStatus)
        objStatusChangeHistory.OldStatus = objChassisMasterClaimHeader.StatusID
        objStatusChangeHistory.NewStatus = CType(EnumCBUReturn.StatusClaim.Selesai, Short)
        objStatusChanges.Add(objStatusChangeHistory)

        objChassisMasterClaimHeader.StatusID = CType(EnumCBUReturn.StatusClaim.Selesai, Short)

        'Update Email queue to IsSend = 2
        Dim objEmailQueues As ArrayList = GetExistEmailQueue(objChassisMasterClaimHeader.ClaimNumber)
        result = CMClaimFacade.Update(objChassisMasterClaimHeader, objStatusChanges, Nothing, objEmailQueues)

        If result > 0 Then
            Dim msg As String = "Simpan Berhasil"
            MessageBox.Show(msg)
            'ViewState("Mode") = "View"
            LoadData(result)
        Else
            MessageBox.Show("Simpan Gagal")
        End If
    End Sub

    Protected Sub btnTransfer_Click(sender As Object, e As EventArgs)
        Dim objChassisMasterClaimHeader As ChassisMasterClaimHeader = CType(sessHelper.GetSession("ChassisMasterClaimHeader"), ChassisMasterClaimHeader)
        TransferWSM(1, objChassisMasterClaimHeader)
        ForceFinishClaim(objChassisMasterClaimHeader)
    End Sub

    Protected Sub btnBaru_Click(sender As Object, e As EventArgs)
        If hdConfirm.Value = "-1" Then
            RegisterStartupScript("Confirm", "<script>ShowConfirm('Anda yakin mau buat baru?', 'btnBaru');</script>")
            Return
        Else
            hdConfirm.Value = "-1"
        End If

        ViewState("Mode") = "New"
        ResetSession()
        ResetControl()
        InitiateControl()
        RefreshGridClaim()
        RefreshGridAttachment()
    End Sub

    Protected Sub btnKembali_Click(sender As Object, e As EventArgs)
        If ViewState("FromUpload") AndAlso objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            Response.Redirect("../CBUReturn/FrmImportClaimCBUReturn.aspx")
        End If
        Response.Redirect("../CBUReturn/FrmDaftarClaimCBUReturn.aspx")
    End Sub
#End Region

#Region "Custom Method"
    Private Sub SaveToWSLog(ByVal listBody As List(Of String))
        Dim func As New WsLogFacade(Me.User)
        For Each iBody As String In listBody
            Try
                Dim wLog As New WsLog
                wLog.Body = iBody
                wLog.Source = "Internal"
                wLog.Status = "True"
                wLog.Message = "Success"
                func.Insert(wLog)
            Catch
            End Try
        Next
    End Sub

    Private Sub CheckPrivilege()
        m_bLihatPrivilege = SecurityProvider.Authorize(Context.User, SR.CBUReturn_PengajuanClaim_View_Privilage)
        m_bInputPrivilege = SecurityProvider.Authorize(Context.User, SR.CBUReturn_PengajuanClaim_Input_Privilage)
        m_bUbahRetailPrivilage = SecurityProvider.Authorize(Context.User, SR.CBUReturn_Retail_Ubah_Privilage)
        m_bUbahVCDPrivilage = SecurityProvider.Authorize(Context.User, SR.CBUReturn_VCD_Ubah_Privilage)
        m_bUbahWholesalesPrivilage = SecurityProvider.Authorize(Context.User, SR.CBUReturn_Wholesales_Ubah_Privilage)

        If Not m_bLihatPrivilege And Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Klaim Pengembalian Kendaraan - Pengajuan Claim")
        End If
    End Sub

    Private Sub InitiateControl(Optional objChassisMasterClaimHeader As ChassisMasterClaimHeader = Nothing)
        Dim isDMSClaim As Boolean = False
        txtCompany.Attributes.Add("readonly", True)
        txtDealerName.Attributes.Add("readonly", True)
        txtStatusClaim.Text = EnumCBUReturn.StatusClaim.Baru.ToString
        txtTglClaim.Text = FormatDateTime(DateTime.Now, Microsoft.VisualBasic.DateFormat.ShortDate)
        txtDealerName.Text = String.Format("{0} - {1}", objDealer.DealerCode, objDealer.DealerName)
        lblStatusStok.Text = "-"

        If (Not objChassisMasterClaimHeader Is Nothing And Not ViewState("Mode").ToString = "New") Then
            isDMSClaim = objChassisMasterClaimHeader.IsDMSClaim
            txtDealerName.Text = String.Format("{0} - {1}", objChassisMasterClaimHeader.Dealer.DealerCode, objChassisMasterClaimHeader.Dealer.DealerName)
            txtStatusClaim.Text = CType(objChassisMasterClaimHeader.StatusID, EnumCBUReturn.StatusClaim).ToString
            lblNoClaim.Text = objChassisMasterClaimHeader.ClaimNumber
            txtTglClaim.Text = FormatDateTime(objChassisMasterClaimHeader.ClaimDate, Microsoft.VisualBasic.DateFormat.ShortDate)
            lblStatusStok.Text = CType(objChassisMasterClaimHeader.StatusStockDMS, EnumCBUReturn.StatusStockDMS).ToString.Replace("_", " ")

            If Not objChassisMasterClaimHeader.ChassisMasterLogisticCompany Is Nothing Then
                txtCompany.Text = String.Format("{0} - {1}", objChassisMasterClaimHeader.ChassisMasterLogisticCompany.Kode, objChassisMasterClaimHeader.ChassisMasterLogisticCompany.Name)
            End If

            Dim code As String = txtDealerName.Text.Split("-")(0).Trim()
            If code = "MKS" Then
                tdKodeDest1.Visible = False
                tdKodeDest2.Visible = False
                tdKodeDest3.Visible = False
            Else
                RebuildPODestDDL(txtDealerName.Text.Split("-")(0).Trim)
                If Not objChassisMasterClaimHeader.PODestination Is Nothing Then
                    ddlKodeDestClaim.Items.FindByValue(objChassisMasterClaimHeader.PODestination.ID).Selected = True
                End If
            End If

            icTglKejadian.Value = IIf(objChassisMasterClaimHeader.DateOccur = CDate("1753-01-01"), DateTime.Now, objChassisMasterClaimHeader.DateOccur)
            txtNoChassis.Text = objChassisMasterClaimHeader.ChassisMaster.ChassisNumber
            txtDealerAlokasi.Text = String.Format("{0} - {1}", objChassisMasterClaimHeader.ChassisMaster.Dealer.DealerCode, objChassisMasterClaimHeader.ChassisMaster.Dealer.DealerName)
            txtNoDO.Text = objChassisMasterClaimHeader.ChassisMaster.DONumber
            txtModel.Text = objChassisMasterClaimHeader.ChassisMaster.VechileColor.VechileType.VechileModel.VechileModelCode
            txtPelaporIssue.Text = objChassisMasterClaimHeader.ReporterIssue
            txtDealerPIC.Text = objChassisMasterClaimHeader.DealerPIC
            txtTempatKejadian.Text = objChassisMasterClaimHeader.PlaceOccur
            txtNominal.Text = FormatNumber(objChassisMasterClaimHeader.Nominal, 0, , , TriState.UseDefault)
            ictTglActFin.Value = IIf(objChassisMasterClaimHeader.CompletionDate = CDate("1753-01-01"), DateTime.Now, objChassisMasterClaimHeader.CompletionDate)
            ictTglEst.Value = IIf(objChassisMasterClaimHeader.RepairEstimationDate = CDate("1753-01-01"), DateTime.Now, objChassisMasterClaimHeader.RepairEstimationDate)
            ictTglTrf.Value = IIf(objChassisMasterClaimHeader.TransferDate = CDate("1753-01-01"), DateTime.Now, objChassisMasterClaimHeader.TransferDate)
            'txtNoMesin.Text = objChassisMasterClaimHeader.EngineNumberReplacement
            txtChassisPengganti.Text = objChassisMasterClaimHeader.ChassisNumberReplacement
            txtNote.Text = objChassisMasterClaimHeader.Remark

            If Not objChassisMasterClaimHeader.ChassisMaster Is Nothing Then
                If Not objChassisMasterClaimHeader.ChassisMaster.Location Is Nothing Then
                    txtKodeDestinasi.Text = String.Format("{0} - {1}", objChassisMasterClaimHeader.ChassisMaster.Location.PODestination.Code, objChassisMasterClaimHeader.ChassisMaster.Location.PODestination.Nama)
                End If
                If Not objChassisMasterClaimHeader.ChassisMaster.ChassisMasterATA Is Nothing Then
                    txtTglUnitTerima.Text = FormatDateTime(objChassisMasterClaimHeader.ChassisMaster.ChassisMasterATA.ATA, Microsoft.VisualBasic.DateFormat.ShortDate)
                End If
            End If

            If objChassisMasterClaimHeader.ResponClaim <> 0 Then
                ddlRespon.Items.FindByValue(objChassisMasterClaimHeader.ResponClaim).Selected = True
            End If

            If objChassisMasterClaimHeader.StatusProcessRetur <> 0 Then
                txtStatusRetur.Text = CType(objChassisMasterClaimHeader.StatusProcessRetur, EnumCBUReturn.StatusProsesRetur).ToString.Replace("_", " ")
            End If

            If ViewState("Mode").ToString = "Edit" Then
                Select Case objChassisMasterClaimHeader.StatusID
                    Case CType(EnumCBUReturn.StatusClaim.Baru, Integer), CType(EnumCBUReturn.StatusClaim.Revisi, Integer)
                        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                            DisabledAllInput()
                        End If

                        trNote.Visible = objChassisMasterClaimHeader.StatusID = EnumCBUReturn.StatusClaim.Revisi
                        txtNote.Visible = objChassisMasterClaimHeader.StatusID = EnumCBUReturn.StatusClaim.Revisi
                        txtNote.Attributes.Add("readonly", True)
                        btnSimpan.Visible = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB And objChassisMasterClaimHeader.StatusID = EnumCBUReturn.StatusClaim.Baru
                        btnBatal.Visible = False

                        txtPelaporIssue.Enabled = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        txtDealerName.Enabled = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        txtCompany.Enabled = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        txtDealerPIC.Enabled = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        txtTempatKejadian.Enabled = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        ddlKodeDestClaim.Enabled = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        lblPopupDealer.Visible = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        lblPopupCompany.Visible = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        btnBaru.Visible = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        btnValidasi.Visible = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        lblPopupChassis.Visible = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        lblPopupDO.Visible = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        btnGetInfoChassis.Visible = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        btnGetInfoDO.Visible = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        txtNoChassis.Enabled = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        txtNoDO.Enabled = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        dgClaim.ShowFooter = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        dgUploadFile.ShowFooter = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        dgClaim.Columns(3).Visible = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        dgUploadFile.Columns(3).Visible = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB

                        tdMksOnly1.Visible = False
                        tdMksOnly2.Visible = False
                        tdMksOnly3.Visible = False
                        trMksOnly1.Visible = False
                        trMksOnly2.Visible = False
                        trMksOnly3.Visible = False
                    Case CType(EnumCBUReturn.StatusClaim.Validasi, Integer)
                        DisabledAllInput()
                        btnKonfirmasi.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                        btnBatal.Visible = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        btnBaru.Visible = Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                    Case CType(EnumCBUReturn.StatusClaim.Konfirmasi, Integer)
                        If objChassisMasterClaimHeader.IsFromUpload Then
                            If Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                                DisabledAllInput()
                            Else
                                lblPopupDealer.Visible = False
                                btnBaru.Visible = False
                                lblPopupChassis.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                                lblPopupDO.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                                btnGetInfoChassis.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                                btnGetInfoDO.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                                txtNoChassis.Enabled = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                                txtNoDO.Enabled = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                                dgClaim.ShowFooter = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                                dgUploadFile.ShowFooter = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                                dgClaim.Columns(3).Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                                dgUploadFile.Columns(3).Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                                ddlKodeDestClaim.Enabled = False
                            End If
                        Else
                            DisabledAllInput()
                        End If

                        txtCompany.Enabled = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                        lblPopupCompany.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                        btnRevisi.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And Not objChassisMasterClaimHeader.IsFromUpload And m_bUbahVCDPrivilage
                        btnTolak.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                        trNote.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                        txtNote.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                        txtNote.Attributes.Clear()
                        btnSend.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                        btnSimpan.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                        ddlRespon.Enabled = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahVCDPrivilage
                        tdMksOnly1.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        tdMksOnly2.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        tdMksOnly3.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        trMksOnly1.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        trMksOnly2.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        trMksOnly3.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB

                        ReEnabledControl(objChassisMasterClaimHeader.ResponClaim)
                    Case CType(EnumCBUReturn.StatusClaim.Proses, Integer)
                        DisabledAllInput()
                        tdMksOnly1.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        tdMksOnly2.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        tdMksOnly3.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        trMksOnly1.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        trMksOnly2.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        trMksOnly3.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        ReEnabledControl(objChassisMasterClaimHeader.ResponClaim)
                        ictTglEst.Enabled = False
                        txtChassisPengganti.Enabled = False
                        If objChassisMasterClaimHeader.ResponClaim = EnumCBUReturn.RespondClaim.Ganti_Unit Then
                            Select Case objChassisMasterClaimHeader.StatusProcessRetur
                                Case CType(EnumCBUReturn.StatusProsesRetur.Faktur_sudah_di_print, Integer), CType(EnumCBUReturn.StatusProsesRetur.Cancel_Faktur, Integer)
                                    btnSend.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahRetailPrivilage
                                    If objChassisMasterClaimHeader.StatusProcessRetur = CType(EnumCBUReturn.StatusProsesRetur.Faktur_sudah_di_print, Integer) Then
                                        btnCancelProses.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahRetailPrivilage
                                    End If
                                Case CType(EnumCBUReturn.StatusProsesRetur.Cancel_Billing, Integer), _
                                     CType(EnumCBUReturn.StatusProsesRetur.Reverse_DO, Integer), _
                                     CType(EnumCBUReturn.StatusProsesRetur.Sales_Replacement, Integer), _
                                     CType(EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti, Integer)
                                    btnSend.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahWholesalesPrivilage
                            End Select

                            btnSend.Text = "Proses Retur"
                        Else
                            btnSelesai.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                        End If
                    Case CType(EnumCBUReturn.StatusClaim.Selesai, Integer), CType(EnumCBUReturn.StatusClaim.Tolak, Integer)
                        DisabledAllInput()
                        If objChassisMasterClaimHeader.ResponClaim <> 0 Then
                            tdMksOnly1.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                            tdMksOnly2.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                            tdMksOnly3.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                            trMksOnly1.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                            trMksOnly2.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                            trMksOnly3.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                            ReEnabledControl(objChassisMasterClaimHeader.ResponClaim)
                            'txtNoMesin.Enabled = False
                            txtNominal.Enabled = False
                            ictTglActFin.Enabled = False
                            ictTglEst.Enabled = False
                            ictTglTrf.Enabled = False
                            txtChassisPengganti.Enabled = False

                            If objChassisMasterClaimHeader.ResponClaim = EnumCBUReturn.RespondClaim.Ganti_Unit And objChassisMasterClaimHeader.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
                                btnTransfer.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB And m_bUbahRetailPrivilage
                            End If

                        End If

                        trNote.Visible = objChassisMasterClaimHeader.StatusID = EnumCBUReturn.StatusClaim.Tolak
                        txtNote.Visible = objChassisMasterClaimHeader.StatusID = EnumCBUReturn.StatusClaim.Tolak
                End Select
            End If

            btnTolak.Enabled = True
            btnKonfirmasi.Enabled = True
            btnValidasi.Enabled = True
        Else
            txtDealerName_TextChanged(Nothing, Nothing)
        End If

        btnSimpan.Enabled = True

        If ViewState("Mode").ToString <> "New" Then
            btnKembali.Visible = True
            lblTitle.Text = "Klaim Pengembalian Kendaraan - Detail Pengajuan Claim"
        Else
            tdMksOnly1.Visible = False
            tdMksOnly2.Visible = False
            tdMksOnly3.Visible = False
            trMksOnly1.Visible = False
            trMksOnly2.Visible = False
            trMksOnly3.Visible = False
            trNote.Visible = False
            dgClaim.ShowFooter = True
            dgUploadFile.ShowFooter = True
            dgClaim.Columns(3).Visible = True
            dgUploadFile.Columns(3).Visible = True
            btnSimpan.Visible = True
            btnBatal.Visible = False
        End If

        If ((Not m_bInputPrivilege Or isDMSClaim) And Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            DisabledAllInput()
        End If
    End Sub

    Private Sub RefreshGridClaim(Optional objChassisMasterClaimHeader As ChassisMasterClaimHeader = Nothing, Optional indexPage As Integer = 0)
        Dim totalRow As Integer
        Dim data As ArrayList = New ArrayList

        If IsNothing(CType(sessHelper.GetSession("ChassisMasterClaimDetail"), ArrayList)) Then
            If Not objChassisMasterClaimHeader Is Nothing And ViewState("Mode").ToString <> "New" Then
                data = objChassisMasterClaimHeader.ChassisMasterClaimDetails
                sessHelper.SetSession("ChassisMasterClaimDetail", data)
            End If
        Else
            data = CType(sessHelper.GetSession("ChassisMasterClaimDetail"), ArrayList)
        End If

        totalRow = data.Count

        If data.Count = 0 Then
            indexPage = 0
            dgClaim.CurrentPageIndex = 0
        Else
            dgClaim.CurrentPageIndex = indexPage
        End If

        dgClaim.DataSource = data
        dgClaim.VirtualItemCount = totalRow
        dgClaim.DataBind()
    End Sub

    Private Sub RefreshGridAttachment(Optional objChassisMasterClaimHeader As ChassisMasterClaimHeader = Nothing, Optional indexPage As Integer = 0)
        Dim totalRow As Integer
        Dim data As ArrayList = New ArrayList

        If IsNothing(CType(sessHelper.GetSession("DocumentUpload"), ArrayList)) Then
            If Not objChassisMasterClaimHeader Is Nothing And ViewState("Mode").ToString <> "New" Then
                data = objChassisMasterClaimHeader.DocumentUploads
                sessHelper.SetSession("DocumentUpload", data)
            End If
        Else
            data = CType(sessHelper.GetSession("DocumentUpload"), ArrayList)
        End If

        totalRow = data.Count

        If data.Count = 0 Then
            indexPage = 0
            dgUploadFile.CurrentPageIndex = 0
        Else
            dgUploadFile.CurrentPageIndex = indexPage
        End If

        dgUploadFile.DataSource = data
        dgUploadFile.VirtualItemCount = totalRow
        dgUploadFile.DataBind()

    End Sub

    Private Function UploadTemp(ByVal dir As String, ByVal srcFile As Stream) As String
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate
        Try
            Dim timeStamp As String = Now.ToString("yyyyMMddHmmss")
            Dim ext As String = Path.GetExtension(dir)
            Dim dirTarget As String = timeStamp & Guid.NewGuid.ToString() & ext
            Dim targetFile As String = Server.MapPath("~/DataTemp/" & dirTarget)

            imp = New SAPImpersonate(_user, _password, _webServer)

            If imp.Start() Then
                Dim objUpload As New UploadToWebServer
                objUpload.Upload(srcFile, targetFile)
            End If

            Return String.Format("DataTemp/{0}", dirTarget)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return ""
        End Try
    End Function

    Private Function UploadFile(ByVal objDocumentUploads As ArrayList, ByVal regNumber As String, ByVal objExistDocument As ArrayList) As Boolean
        Dim timeStamp As String = Now.ToString("yyyyMMddHmmss")
        Dim isValid As Boolean = True
        Dim destPath As String = String.Format("{0}\{1}\VehicleClaim\AttachmentClaim\{2}\{3}\{4}", cbuRetDir, Date.Now.Year, txtDealerName.Text.Split("-")(0), regNumber.Replace("/", ""), timeStamp)
        Dim ext As String
        Dim strDestFile As String
        Dim fileInfo As FileInfo

        For Each objDocumentUpload As DocumentUpload In objDocumentUploads
            If Not objDocumentUpload.AttachmentData Is Nothing Then
                fileInfo = New FileInfo(Server.MapPath(Path.Combine("~/", objDocumentUpload.Path)))
                If Not fileInfo.Exists Then
                    MessageBox.Show(String.Format("File {0} tidak ditemukan. Mohon untuk melakukan upload ulang.", objDocumentUpload.FileName))
                    isValid = False
                    Exit For
                End If

                'ext = String.Format(".{0}", fileInfo.Extension)
                strDestFile = destPath & Guid.NewGuid.ToString() & fileInfo.Extension
                objDocumentUpload.Path = strDestFile
                isValid = CommitAttachment(objDocumentUpload)
            End If
            objDocumentUpload.DocRegNumber = regNumber

            If Not isValid Then
                Exit For
            End If
        Next

        Return isValid
    End Function

    Private Function CommitAttachment(ByVal obj As DocumentUpload) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim TargetFInfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(obj.AttachmentData) Then
                    TargetFInfo = New FileInfo(targetDir + obj.Path)

                    If Not TargetFInfo.Directory.Exists Then
                        Directory.CreateDirectory(TargetFInfo.DirectoryName)
                    End If

                    obj.AttachmentData.SaveAs(targetDir + obj.Path)
                    obj.AttachmentData = Nothing
                    Return True
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.Replace("\n", "").Replace(Environment.NewLine, ""))
        End Try

        Return False
    End Function

    Private Sub LoadData(Optional ByVal id As Integer = 0)
        Dim objCMClaimH As ChassisMasterClaimHeader
        If id <> 0 Then
            objCMClaimH = CMClaimFacade.Retrieve(id)
        Else
            objCMClaimH = CMClaimFacade.Retrieve(CType(Request.QueryString("ID").ToString, Integer))
        End If

        ResetSession()
        sessHelper.SetSession("ChassisMasterClaimHeader", CType(objCMClaimH, ChassisMasterClaimHeader))

        InitResponClaimDdl()
        InitiateControl(objCMClaimH)
        RefreshGridClaim(objCMClaimH)
        RefreshGridAttachment(objCMClaimH)

    End Sub

    Private Function GenerateCode(ByVal dealerCode As String, ByVal PeriodYear As String, ByVal PeriodMonth As String) As String
        Dim _ret = ""

        Dim noBuilder As New StringBuilder
        Dim RunningNumber As Integer = 0
        noBuilder.Append(String.Format("CLM/{0}/{1}", PeriodMonth, PeriodYear.ToString()))

        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimHeader), "ClaimNumber", MatchType.StartsWith, noBuilder.ToString))
        Dim arrl As ArrayList = CMClaimFacade.Retrieve(crit)

        If arrl.Count > 0 Then
            Dim objCMClaimH As ChassisMasterClaimHeader = CommonFunction.SortListControl(arrl, "ClaimNumber", Sort.SortDirection.DESC)(0)
            Dim repNumber As String = objCMClaimH.ClaimNumber
            RunningNumber = (CInt(Right(repNumber, 3)) + 1)
            repNumber = RunningNumber.ToString("d3")
            _ret = String.Format("{0}/{1}", noBuilder.ToString, repNumber)
        Else
            _ret = String.Format("{0}/{1}", noBuilder.ToString, CInt(1).ToString("d3"))
        End If

        Return _ret
    End Function

    Private Sub DisabledAllInput()
        txtDealerName.Enabled = False
        txtCompany.Enabled = False
        txtPelaporIssue.Enabled = False
        ddlKodeDestClaim.Enabled = False
        icTglKejadian.Enabled = False
        txtDealerPIC.Enabled = False
        txtTempatKejadian.Enabled = False
        txtNoChassis.Enabled = False
        txtNoDO.Enabled = False
        'txtNote.Enabled = False
        txtNote.Attributes.Add("readonly", True)
        'txtNoMesin.Enabled = False
        txtNominal.Enabled = False
        txtStatusRetur.Enabled = False
        ictTglActFin.Enabled = False
        ictTglEst.Enabled = False
        ictTglTrf.Enabled = False
        txtChassisPengganti.Enabled = False

        dgClaim.ShowFooter = False
        dgUploadFile.ShowFooter = False
        dgClaim.Columns(3).Visible = False
        dgUploadFile.Columns(3).Visible = False

        btnBaru.Visible = False
        btnSimpan.Visible = False
        btnValidasi.Visible = False
        btnTolak.Visible = False
        btnBatal.Visible = False
        btnKonfirmasi.Visible = False
        btnRevisi.Visible = False
        btnSend.Visible = False
        btnSelesai.Visible = False
        btnTransfer.Visible = False
        btnCancelProses.Visible = False
        lblPopupCompany.Visible = False
        lblPopupDealer.Visible = False
        lblPopupChassis.Visible = False
        lblPopupDO.Visible = False
        btnGetInfoChassis.Visible = False
        btnGetInfoDO.Visible = False

        txtNote.Visible = False
        'txtNoMesin.Visible = False
        txtNominal.Visible = False
        txtStatusRetur.Visible = False
        txtChassisPengganti.Visible = False
        ictTglActFin.Visible = False
        ictTglEst.Visible = False
        ictTglTrf.Visible = False
        ddlRespon.Enabled = False

        tdMksOnly1.Visible = False
        tdMksOnly2.Visible = False
        tdMksOnly3.Visible = False
        trMksOnly1.Visible = False
        trMksOnly2.Visible = False
        trMksOnly3.Visible = False
        trNote.Visible = False
    End Sub

    Private Sub ResetControl()
        txtTglClaim.Text = FormatDateTime(DateTime.Now, Microsoft.VisualBasic.DateFormat.ShortDate)
        lblNoClaim.Text = "[Auto Generate]"
        txtCompany.Text = ""
        txtDealerName.Text = ""
        ddlKodeDestClaim.Items.Clear()
        ddlKodeDestClaim.Items.Insert(0, "Silahkan Pilih")
        icTglKejadian.Value = DateTime.Now
        txtNoChassis.Text = ""
        txtDealerAlokasi.Text = ""
        txtNoDO.Text = ""
        txtTglUnitTerima.Text = ""
        txtModel.Text = ""
        txtPelaporIssue.Text = ""
        txtDealerPIC.Text = ""
        txtTempatKejadian.Text = ""
        'txtNoMesin.Text = ""
        txtNominal.Text = ""
        lblStatusStok.Text = "-"
        ictTglActFin.Value = DateTime.Now
        ictTglEst.Value = DateTime.Now
        ictTglTrf.Value = DateTime.Now

        txtDealerName.Enabled = True
        txtCompany.Enabled = True
        txtPelaporIssue.Enabled = True
        ddlKodeDestClaim.Enabled = True
        icTglKejadian.Enabled = True
        txtDealerPIC.Enabled = True
        txtTempatKejadian.Enabled = True

        txtNoChassis.Enabled = False
        txtNoDO.Enabled = False
        lblPopupChassis.Visible = False
        lblPopupDO.Visible = False
        btnGetInfoChassis.Visible = False
        btnGetInfoDO.Visible = False
        btnValidasi.Visible = False
    End Sub

    Private Sub ResetSession()
        sessHelper.SetSession("ChassisMasterClaimHeader", Nothing)
        sessHelper.SetSession("ChassisMasterClaimDetail", Nothing)
        sessHelper.SetSession("DocumentUpload", Nothing)
    End Sub

    Private Function PopulateChassisMasterClaimHeader(ByVal objCMClaimH As ChassisMasterClaimHeader) As ChassisMasterClaimHeader
        objCMClaimH.ClaimDate = CDate(txtTglClaim.Text)

        If Not txtCompany.Text.Equals("") Then
            objCMClaimH.ChassisMasterLogisticCompany = objCMLFacade.Retrieve(txtCompany.Text.Split("-")(0))
        End If

        If Not txtDealerName.Text.Equals("") Then
            objCMClaimH.Dealer = DealerFacade.Retrieve(txtDealerName.Text.Split("-")(0))
        End If

        If ddlKodeDestClaim.SelectedIndex <> 0 Then
            objCMClaimH.PODestination = PODestFacade.Retrieve(CInt(ddlKodeDestClaim.SelectedValue))
        End If

        objCMClaimH.DateOccur = icTglKejadian.Value

        Dim data As ArrayList = New ArrayList
        crit = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtNoChassis.Text <> "" Then
            crit.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, txtNoChassis.Text))
            data = ChassisMFacade.Retrieve(crit)
        End If

        If txtNoDO.Text <> "" And data.Count = 0 Then
            crit.opAnd(New Criteria(GetType(ChassisMaster), "DONumber", MatchType.Exact, txtNoDO.Text))
            data = ChassisMFacade.Retrieve(crit)
        End If

        If data.Count > 0 Then
            Dim obj As ChassisMaster = CType(data(0), ChassisMaster)
            objCMClaimH.ChassisMaster = obj
        Else
            objCMClaimH.ChassisMaster = Nothing
        End If

        If Not objCMClaimH.ChassisMaster Is Nothing Then
            If Not objCMClaimH.ChassisMaster.Location Is Nothing Then
                objCMClaimH.ChassisPODestination = objCMClaimH.ChassisMaster.Location.PODestination
            End If
        End If

        objCMClaimH.ReporterIssue = txtPelaporIssue.Text
        objCMClaimH.DealerPIC = txtDealerPIC.Text
        objCMClaimH.PlaceOccur = txtTempatKejadian.Text

        'Set Default Value
        objCMClaimH.ResponClaim = 0
        objCMClaimH.Nominal = 0
        objCMClaimH.EngineNumberReplacement = ""
        objCMClaimH.TransferDate = CDate("1753-01-01")
        objCMClaimH.CompletionDate = CDate("1753-01-01")
        objCMClaimH.RepairEstimationDate = CDate("1753-01-01")
        objCMClaimH.ChassisNumberReplacement = ""

        If ddlRespon.SelectedIndex <> 0 Then
            objCMClaimH.ResponClaim = CInt(ddlRespon.SelectedValue)
            Select Case objCMClaimH.ResponClaim
                Case EnumCBUReturn.RespondClaim.Ganti_Unit
                    objCMClaimH.ChassisNumberReplacement = txtChassisPengganti.Text
                Case EnumCBUReturn.RespondClaim.Asuransi
                    If txtNominal.Text <> "" Then
                        objCMClaimH.Nominal = CDec(txtNominal.Text)
                    Else
                        objCMClaimH.Nominal = 0
                    End If

                    objCMClaimH.TransferDate = ictTglTrf.Value
                    objCMClaimH.StatusProcessRetur = 0
                Case EnumCBUReturn.RespondClaim.Ganti_Uang
                    If txtNominal.Text <> "" Then
                        objCMClaimH.Nominal = CDec(txtNominal.Text)
                    Else
                        objCMClaimH.Nominal = 0
                    End If

                    objCMClaimH.TransferDate = ictTglTrf.Value
                    objCMClaimH.StatusProcessRetur = 0
                Case EnumCBUReturn.RespondClaim.Perbaikan_Dealer
                    If txtNominal.Text <> "" Then
                        objCMClaimH.Nominal = CDec(txtNominal.Text)
                    Else
                        objCMClaimH.Nominal = 0
                    End If

                    objCMClaimH.TransferDate = ictTglTrf.Value
                Case EnumCBUReturn.RespondClaim.Perbaikan_MMKSI
                    objCMClaimH.CompletionDate = ictTglActFin.Value
                    objCMClaimH.RepairEstimationDate = ictTglEst.Value
                    objCMClaimH.StatusProcessRetur = 0
            End Select
        End If
        Return objCMClaimH
    End Function

    Private Function AfterReturnSAPProcess(ByRef objClaim As ChassisMasterClaimHeader, ByVal objCBUReturnSendSAP As CBUReturnSendSAP, _
                                           ByVal objList As ArrayList, ByVal currentStatus As Integer, ByRef isFromCancelled As Boolean) As ArrayList
        Dim chassisM As ChassisMaster = ChassisMFacade.Retrieve(objClaim.ChassisMaster.ChassisNumber)
        currentStatus = IIf(currentStatus = 0, objClaim.StatusProcessRetur, currentStatus)
        isFromCancelled = False

        Select Case currentStatus
            Case EnumCBUReturn.StatusProsesRetur.Faktur_sudah_di_print, EnumCBUReturn.StatusProsesRetur.Cancel_Billing
                Dim objAppConfFacade As AppConfigFacade = New AppConfigFacade(User)
                Dim objAppConf As AppConfig = objAppConfFacade.Retrieve(confBlockFakturDesc)
                chassisM.PendingDesc = String.Format("Block_Faktur_{0}", objAppConf.Value)
                objList.Add(chassisM)
            Case EnumCBUReturn.StatusProsesRetur.Reverse_DO, EnumCBUReturn.StatusProsesRetur.Sales_Replacement
                'Set inactive chassis claimed
                chassisM.RowStatus = -1
                chassisM.EndCustomer = Nothing
                objList.Add(chassisM)

                If objClaim.ChassisMaster.EndCustomerID <> 0 Then '
                    crit = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, objClaim.ClaimNumber))
                    crit.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentType", MatchType.Exact, CType(LookUp.DocumentType.CBUReturn_ReturStatus, Integer)))
                    crit.opAnd(New Criteria(GetType(StatusChangeHistory), "NewStatus", MatchType.Exact, CType(EnumCBUReturn.StatusProsesRetur.Cancel_Faktur, Integer)))

                    Dim datas As ArrayList = objSHistFacade.Retrieve(crit)
                    If datas.Count > 0 Then
                        objClaim.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti
                        isFromCancelled = True
                    Else
                        objClaim.StatusID = EnumCBUReturn.StatusClaim.Selesai
                    End If
                Else
                    objClaim.StatusID = EnumCBUReturn.StatusClaim.Selesai
                End If

                'Swap chassis claimed to chassis replacement
                Dim responses As CBUReturnSAPResponse = objCBUReturnSendSAP.SapResponses(0)
                chassisM = ChassisMFacade.Retrieve(objClaim.ChassisMaster.ChassisNumber)
                chassisM.ID = 0
                chassisM.ChassisNumber = objClaim.ChassisNumberReplacement
                chassisM.EngineNumber = responses.EngineNumber
                chassisM.SerialNumber = responses.SerialNumber
                chassisM.DODate = responses.DoDate
                chassisM.PendingDesc = ""
                chassisM.GIDate = CDate("1900-01-01")
                chassisM.ParkingDays = 0
                chassisM.ParkingAmount = 0

                If currentStatus = EnumCBUReturn.StatusProsesRetur.Sales_Replacement Then
                    'chassisM.SONumber = responses.SOReplacement
                    chassisM.DONumber = responses.DOReplacement

                    Dim statusFakturList As String = String.Format("{0},{1},{2}", _
                         CInt(EnumChassisMaster.FakturStatus.Konfirmasi), CInt(EnumChassisMaster.FakturStatus.Proses), CInt(EnumChassisMaster.FakturStatus.Selesai))
                    If statusFakturList.Contains(objClaim.ChassisMaster.FakturStatus) Then
                        chassisM.FakturStatus = EnumChassisMaster.FakturStatus.Konfirmasi
                    End If
                End If

                objList.Add(chassisM)

        End Select
        Return objList
    End Function

    Private Sub RebuildClaimTypeDdl(ByVal ddl As DropDownList)
        ddl.Items.Clear()
        ddl.DataSource = objEnumCBU.TipeClaimList
        ddl.DataTextField = "Value"
        ddl.DataValueField = "ID"
        ddl.DataBind()
        ddl.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Sub InitResponClaimDdl()
        ddlRespon.Items.Clear()
        ddlRespon.DataSource = objEnumCBU.RespondClaimList
        ddlRespon.DataTextField = "Value"
        ddlRespon.DataValueField = "ID"
        ddlRespon.DataBind()
        ddlRespon.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Sub GetChassisDO(ByVal type As Integer)
        Dim code As String = txtDealerName.Text.Split("-")(0).Trim
        Dim data As ArrayList
        Dim query As String = String.Empty

        crit = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If code <> "MKS" Then
            Dim dealer As Dealer = DealerFacade.Retrieve(code)
            crit.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.DealerGroup.ID", MatchType.Exact, dealer.DealerGroup.ID))
        End If

        query = "SELECT DealerSystems.DealerID FROM DealerSystems WHERE DealerSystems.GoLiveDate IS NOT NULL"
        crit.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.ID", MatchType.NotInSet, query))

        Select Case type
            Case 1 'ChassisNumber
                crit.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, txtNoChassis.Text))
            Case 2 'DONumber
                crit.opAnd(New Criteria(GetType(ChassisMaster), "DONumber", MatchType.Exact, txtNoDO.Text))
        End Select

        data = ChassisMFacade.Retrieve(crit)

        If data.Count = 0 Then
            txtDealerAlokasi.Text = ""
            txtTglUnitTerima.Text = ""
            txtModel.Text = ""
            MessageBox.Show("Data tidak ditemukan.")
        Else
            Dim obj As ChassisMaster = CType(data(0), ChassisMaster)
            txtDealerAlokasi.Text = String.Format("{0} - {1}", obj.Dealer.DealerCode, obj.Dealer.DealerName)
            If Not obj.ChassisMasterATA Is Nothing Then
                txtTglUnitTerima.Text = FormatDateTime(obj.ChassisMasterATA.ATA, Microsoft.VisualBasic.DateFormat.ShortDate)
            End If
            txtModel.Text = obj.VechileColor.VechileType.VechileModel.VechileModelCode
            txtNoChassis.Text = obj.ChassisNumber
            txtNoDO.Text = obj.DONumber
            If Not obj.Location Is Nothing Then
                txtKodeDestinasi.Text = obj.Location.PODestination.Code
            Else
                txtKodeDestinasi.Text = ""
            End If
        End If
    End Sub

    Private Sub RebuildPODestDDL(ByVal code As String)
        ddlKodeDestClaim.Items.Clear()

        crit = New CriteriaComposite(New Criteria(GetType(PODestination), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(PODestination), "Dealer.DealerCode", MatchType.Exact, code))

        Dim results As ArrayList = PODestFacade.Retrieve(crit)

        With ddlKodeDestClaim.Items
            For Each obj As PODestination In results
                .Add(New ListItem(String.Format("{0} - {1}", obj.Code, obj.Nama), obj.ID))
            Next
        End With

        ddlKodeDestClaim.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Sub ReEnabledControl(ByVal responClaim As Integer)
        txtNominal.Enabled = False
        txtNominal.Visible = False
        ictTglTrf.Enabled = False
        ictTglTrf.Visible = False
        ictTglActFin.Enabled = False
        ictTglActFin.Visible = False
        ictTglEst.Enabled = False
        ictTglEst.Visible = False
        trMksOnly2.Visible = False
        trMksOnly3.Visible = False
        txtChassisPengganti.Enabled = False
        txtChassisPengganti.Visible = False
        lblTitleF1.Visible = False
        tdMksOnly4.Visible = False
        txtStatusRetur.Visible = False
        btnSend.Text = "Proses Retur"
        btnSend.OnClientClick = "return confirm('Apakah anda yakin mau proses ?');"

        Select Case responClaim
            Case EnumCBUReturn.RespondClaim.Ganti_Unit
                lblTitleF1.Text = "Chassis Pengganti"
                lblTitleF1.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                tdMksOnly4.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                txtChassisPengganti.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                txtStatusRetur.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                trMksOnly2.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                txtChassisPengganti.Enabled = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                lblTitleF2.Text = "Status Proses Retur"
                btnSend.Text = "Send to SAP"
                btnSend.OnClientClick = ""
            Case EnumCBUReturn.RespondClaim.Asuransi
                lblTitleF2.Text = "Tanggal Transfer"
                txtNominal.Enabled = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                txtNominal.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                ictTglTrf.Enabled = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                ictTglTrf.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                trMksOnly2.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                trMksOnly3.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
            Case EnumCBUReturn.RespondClaim.Ganti_Uang
                lblTitleF2.Text = "Tanggal Transfer"
                txtNominal.Enabled = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                txtNominal.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                ictTglTrf.Enabled = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                ictTglTrf.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                trMksOnly2.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                trMksOnly3.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
            Case EnumCBUReturn.RespondClaim.Perbaikan_Dealer
                lblTitleF2.Text = "Tanggal Transfer"
                txtNominal.Enabled = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                txtNominal.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                ictTglTrf.Enabled = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                ictTglTrf.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                trMksOnly2.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                trMksOnly3.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
            Case EnumCBUReturn.RespondClaim.Perbaikan_MMKSI
                lblTitleF1.Text = "Actual Selesai"
                lblTitleF2.Text = "Estimasi Perbaikan"
                lblTitleF1.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                tdMksOnly4.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                ictTglActFin.Enabled = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                ictTglActFin.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                ictTglEst.Enabled = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                ictTglEst.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
                trMksOnly2.Visible = objDealer.Title = EnumDealerTittle.DealerTittle.KTB
        End Select
    End Sub

    Private Function GetCurrentStatus(ByVal id As Integer, Optional ByRef StatusProsesRetur As Integer = 0) As Integer
        Dim objCMClaimH As ChassisMasterClaimHeader = CMClaimFacade.Retrieve(id)
        If Not objCMClaimH Is Nothing Then
            StatusProsesRetur = objCMClaimH.StatusProcessRetur
            Return objCMClaimH.StatusID
        End If

        Return EnumCBUReturn.StatusClaim.Baru
    End Function

    Private Function GetExistEmailQueue(ByVal claimNumber As String)
        Dim objEmailQueues As ArrayList
        crit = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimEmailQueue), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "ClaimNumber", MatchType.Exact, claimNumber))
        crit.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "IsSend", MatchType.InSet, "(0,1)"))
        objEmailQueues = objEmailQFacade.Retrieve(crit)

        For Each item As ChassisMasterClaimEmailQueue In objEmailQueues
            item.IsSend = 2
        Next

        Return objEmailQueues
    End Function

    Private Function TransferWSM(ByVal mode As Integer, ByVal objClaimHeader As ChassisMasterClaimHeader) As Boolean
        Dim isSuccess As Boolean = False
        TransferChasisMasterProfile(mode, objClaimHeader)
        'If isSuccess Then
        '    isSuccess = Transfer(mode = 1, objClaimHeader)
        'End If
        isSuccess = Transfer(mode = 1, objClaimHeader)
        Return isSuccess
    End Function

    Private Function TransferChasisMasterProfile(ByVal mode As Integer, ByVal objClaimHeader As ChassisMasterClaimHeader) As Boolean
        Dim filename = String.Format("{0}{1}{2}", "csprof", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\FK\" & filename  '-- Destination file
        Dim listFaktur As ArrayList = PopulateInvoice(mode, objClaimHeader)
        If listFaktur.Count = 0 Then
            Return False
        End If

        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                If finfo.Exists Then
                    finfo.Delete()
                End If
                Dim fs As FileStream = New FileStream(DestFile, FileMode.CreateNew)
                sw = New StreamWriter(fs)
                DnLoadInvoiceDataChasisProfile(sw, mode, objClaimHeader)
                sw.Close()
                fs.Close()
            End If
        Catch ex As Exception
            sw.Close()
            Throw ex
            Return success
        End Try
        Return success
    End Function

    Private Function Transfer(ByVal ReTransfer As Boolean, ByVal objClaimHeader As ChassisMasterClaimHeader) As Boolean

        Dim dt As DateTime = DateTime.Now
        Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & _
                                CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & _
                                CType(dt.Second, String) & CType(dt.Millisecond, String)
        Dim InvoiceData As String = Server.MapPath("") & "\..\DataTemp\fkopen" & sSuffix & ".txt"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim isSuccess As Boolean = True
        Try
            If imp.Start() Then
                Dim finfo As FileInfo = New FileInfo(InvoiceData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If
                Dim fs As FileStream = New FileStream(InvoiceData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteInvoiceData(sw, objClaimHeader)
                sw.Close()
                fs.Close()
                Dim DestFolder As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\FK"
                If Not IO.Directory.Exists(DestFolder) Then
                    IO.Directory.CreateDirectory(DestFolder)
                End If
                Dim DestFile As String = DestFolder & "\fkopen" & sSuffix & ".txt"
                Dim finfo2 As FileInfo = New FileInfo(InvoiceData)
                finfo2.CopyTo(DestFile, True)
                imp.StopImpersonate()
                imp = Nothing
            End If
            InProcessStatus(objClaimHeader)  '-- Change invoice status from 'Konfirmasi' to 'Proses'
            MessageBox.Show("Transfer data berhasil")
            Return True
        Catch ex As Exception
            MessageBox.Show("Transfer data gagal")
            Return False
        End Try
    End Function

    Private Sub WriteInvoiceData(ByRef sw As StreamWriter, ByVal objClaimHeader As ChassisMasterClaimHeader)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim InvoiceLine As StringBuilder = New StringBuilder  '-- Invoice line in text file
        Dim objInvoice As ChassisMaster = New ChassisMasterFacade(User).Retrieve(objClaimHeader.ChassisNumberReplacement)
        InvoiceLine.Remove(0, InvoiceLine.Length)  '-- Empty Invoice line
        InvoiceLine.Append(objInvoice.ChassisNumber.Replace(tab, " ") & tab) '-- Chassis number
        If Not IsNothing(objInvoice.EndCustomer) Then
            InvoiceLine.Append(Format(objInvoice.EndCustomer.FakturDate, "ddMMyyyy") & tab)  '-- Faktur date
            Dim objRefChassisMaster As ChassisMaster
            objRefChassisMaster = New ChassisMasterFacade(User).Retrieve(objInvoice.EndCustomer.RefChassisNumberID)
            If objRefChassisMaster Is Nothing Then
                InvoiceLine.Append(tab)  '-- Empty column
            Else
                InvoiceLine.Append(objRefChassisMaster.ChassisNumber.Replace(tab, " ") & tab)  '-- Ref chassis number
            End If
            InvoiceLine.Append(" " & tab)   '-- Code

            InvoiceLine.Append(objInvoice.EndCustomer.Customer.Code.Replace(tab, " ") & tab)   '-- Code
            If UCase(objInvoice.EndCustomer.AreaViolationFlag) = "X" Then
                Dim objAreaVioPayMethFacade As PaymentMethodFacade = New PaymentMethodFacade(User)
                Dim objAreaVioPatMeth As PaymentMethod = objAreaVioPayMethFacade.Retrieve(objInvoice.EndCustomer.AreaViolationPaymentMethodID)
                InvoiceLine.Append(objAreaVioPatMeth.Code.Replace(tab, " ") & tab)   '-- Wilayah TOP
                InvoiceLine.Append(Format(objInvoice.EndCustomer.AreaViolationyAmount, "0") & tab)  '-- Wilayah amount
                InvoiceLine.Append(objInvoice.EndCustomer.AreaViolationBankName.Replace(tab, " ") & tab)  '-- Wilayah bank name
                InvoiceLine.Append(objInvoice.EndCustomer.AreaViolationGyroNumber.Replace(tab, " ") & tab)  '-- Wilayah giro#
            Else
                InvoiceLine.Append(tab)  '-- Empty column
                InvoiceLine.Append(tab)  '-- Empty column
                InvoiceLine.Append(tab)  '-- Empty column
                InvoiceLine.Append(tab)  '-- Empty column
            End If
            If UCase(objInvoice.EndCustomer.PenaltyFlag) = "X" Then
                Dim objPenaltyPayMethFacade As PaymentMethodFacade = New PaymentMethodFacade(User)
                Dim objPenaltyPatMeth As PaymentMethod = objPenaltyPayMethFacade.Retrieve(objInvoice.EndCustomer.PenaltyPaymentMethodID)
                InvoiceLine.Append(objPenaltyPatMeth.Code.Replace(tab, " ") & tab)  '-- Disc TOP
                InvoiceLine.Append(Format(objInvoice.EndCustomer.PenaltyAmount, "0") & tab)  '-- Disc amount
                InvoiceLine.Append(objInvoice.EndCustomer.PenaltyBankName.Replace(tab, " ") & tab)  '-- Disc bank name
                InvoiceLine.Append(objInvoice.EndCustomer.PenaltyGyroNumber.Replace(tab, " ") & tab)  '-- Disc giro#
            Else
                InvoiceLine.Append(tab)  '-- Empty column
                InvoiceLine.Append(tab)  '-- Empty column
                InvoiceLine.Append(tab)  '-- Empty column
                InvoiceLine.Append(tab)  '-- Empty column
            End If

            If UCase(objInvoice.EndCustomer.ReferenceLetterFlag) = "X" Then
                InvoiceLine.Append(objInvoice.EndCustomer.ReferenceLetter.Replace(tab, " ") & tab)  '-- Letter
            Else
                InvoiceLine.Append(tab)  '-- Empty column
            End If
            InvoiceLine.Append(IIf(objInvoice.EndCustomer.SaveBy <> "", UserInfo.Convert(objInvoice.EndCustomer.SaveBy.Replace(tab, " ")), "") & tab)  '-- Dibuat oleh
            InvoiceLine.Append(Format(objInvoice.EndCustomer.SaveTime, "ddMMyyyy") & tab)  '-- Tgl dibuat
            InvoiceLine.Append(IIf(objInvoice.EndCustomer.ValidateBy <> "", UserInfo.Convert(objInvoice.EndCustomer.ValidateBy.Replace(tab, " ")), "") & tab)  '-- Divalidasi oleh
            InvoiceLine.Append(Format(objInvoice.EndCustomer.ValidateTime, "ddMMyyyy") & tab)
            InvoiceLine.Append(objInvoice.EndCustomer.Customer.PrintRegion.Replace(tab, " ") & tab)

            'Start  :Add MCP Flag;by:dna;on:20110623;for:rina;
            If Not IsNothing(objInvoice.EndCustomer.Customer.MyCustomerRequest) AndAlso objInvoice.EndCustomer.Customer.MyCustomerRequest.ID > 0 Then
                If objInvoice.EndCustomer.Customer.MyCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP Then
                    InvoiceLine.Append("X" & tab)
                Else
                    InvoiceLine.Append("" & tab)
                End If
            Else
                InvoiceLine.Append("" & tab)
            End If

            If Not IsNothing(objInvoice.EndCustomer.MCPHeader) Then
                InvoiceLine.Append(objInvoice.EndCustomer.MCPHeader.ReferenceNumber & tab)
                InvoiceLine.Append(objInvoice.EndCustomer.MCPHeader.LetterDate.ToString("ddMMyyyy") & tab)
            Else
                InvoiceLine.Append("" & tab)
                InvoiceLine.Append("" & tab)
            End If

            '' AdddSpkNumber
            If Not IsNothing(objInvoice.EndCustomer) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur.SPKHeader) Then
                InvoiceLine.Append(objInvoice.EndCustomer.SPKFaktur.SPKHeader.SPKNumber & tab)
                InvoiceLine.Append(objInvoice.EndCustomer.SPKFaktur.SPKHeader.CreatedTime.ToString("ddMMyyyy") & tab)
            Else
                InvoiceLine.Append("" & tab)
                InvoiceLine.Append("" & tab)
            End If

            If Not IsNothing(objInvoice.FleetFaktur) AndAlso Not IsNothing(objInvoice.FleetFaktur.FleetRequest) Then
                InvoiceLine.Append(objInvoice.FleetFaktur.FleetRequest.NoRegRequest & tab)
            Else
                InvoiceLine.Append("" & tab)
            End If


            If Not IsNothing(objInvoice.EndCustomer) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur.SPKHeader) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch) Then
                InvoiceLine.Append(objInvoice.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch.DealerBranchCode & tab)
            Else
                InvoiceLine.Append("" & tab)
            End If

            If objInvoice.EndCustomer.IsTemporary = CType(EnumEndCustomer.TemporaryFaktur.Temporary, Short) Then
                InvoiceLine.Append("X" & tab) '-- if it is temporary faktur
            Else
                InvoiceLine.Append(tab)  '-- Empty column
            End If
            InvoiceLine.Append(objClaimHeader.ChassisMaster.ChassisNumber & tab)
        Else
        End If
        sw.WriteLine(InvoiceLine.ToString())  '-- Write Invoice line
    End Sub

    Private Function PopulateInvoice(ByVal mode As Integer, ByVal objClaimHeader As ChassisMasterClaimHeader) As ArrayList
        Dim oExArgs As New System.Collections.ArrayList
        Dim _chsMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(objClaimHeader.ChassisNumberReplacement)
        If mode = 0 Then
            If _chsMaster.FakturStatus = EnumChassisMaster.FakturStatus.Konfirmasi Then
                oExArgs.Add(objClaimHeader)
            End If
        End If
        If mode = 1 Then
            If _chsMaster.FakturStatus = EnumChassisMaster.FakturStatus.Proses Then
                oExArgs.Add(objClaimHeader)
            End If
        End If
        Return oExArgs
    End Function

    Private Sub DnLoadInvoiceDataChasisProfile(ByRef sw As StreamWriter, ByVal mode As String, ByVal objClaimHeader As ChassisMasterClaimHeader)
        Dim tab As Char  '-- Separator character <Tab>
        Dim InvoiceLine As StringBuilder = New StringBuilder  '-- Invoice line in text file
        Dim InvoiceResList As ArrayList = PopulateInvoice(mode, objClaimHeader)
        Dim temp As String = String.Empty
        tab = Chr(9)
        For Each _chsMasterClaimHeader As ChassisMasterClaimHeader In InvoiceResList
            For Each objChassisMasterProfile As ChassisMasterProfile In _chsMasterClaimHeader.ChassisMaster.ChassisMasterProfiles
                If objChassisMasterProfile.ProfileValue.Trim <> "" Then
                    InvoiceLine.Append(_chsMasterClaimHeader.ChassisNumberReplacement + tab)
                    InvoiceLine.Append(objChassisMasterProfile.ProfileHeader.Code + tab)
                    temp = objChassisMasterProfile.ProfileValue.Trim
                    InvoiceLine.Append(temp.Trim)
                    InvoiceLine.Append(vbNewLine)
                    temp = String.Empty
                End If
            Next
        Next
        sw.WriteLine(InvoiceLine.ToString())
    End Sub

    Private Sub InProcessStatus(ByVal objClaimHeader As ChassisMasterClaimHeader)
        '-- Change invoice status from 'Konfirmasi' to 'Proses'

        Dim ConfirmList As New ArrayList  '-- List of confirmed invoices

        '-- Iterate invoices selected
        Dim item As ChassisMaster = New ChassisMasterFacade(User).Retrieve(objClaimHeader.ChassisNumberReplacement)

        '-- Only invoices with status 'Konfirmasi' and with End Customer defined
        If item.FakturStatus = EnumChassisMaster.FakturStatus.Konfirmasi Then
            If Not IsNothing(item.EndCustomer) Then

                item.FakturStatus = EnumChassisMaster.FakturStatus.Proses  '-- Change invoice status
                item.EndCustomer.DownloadBy = UserInfo.Convert(User.Identity.Name)  '-- Set its downloader
                item.EndCustomer.DownloadTime = Date.Now  '-- Set its download date

                ConfirmList.Add(item)
            End If
        End If

        '-- If there exists at least a confirmed invoice selected then do update transaction
        Dim ChassisFac As New ChassisMasterFacade(User)
        ChassisFac.UpdateTransaction(ConfirmList)  '-- Update list of confirmed invoices

    End Sub

    Private Sub ForceFinishClaim(ByVal item As ChassisMasterClaimHeader)
        item.StatusID = EnumCBUReturn.StatusClaim.Selesai
        Dim result As Integer = New ChassisMasterClaimHeaderFacade(User).Update(item)
    End Sub

    Private Function SendEmail(ByVal objClaim As ChassisMasterClaimHeader, ByVal emailTo As String, ByVal emailBcc As String, ByVal isFromCancelled As Boolean, ByRef msg As String) As Integer
        Dim retValue As Integer = 0 '0 = gagal ; 1 = sukses

        Dim strSmtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim objEmail As DNetMail = New DNetMail(strSmtp)
        Dim strFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
        Dim strSubject As String = "[MMKSI-DNet] CBUReturn - Notifikasi Unit Replacement"
        Dim strTo As String = emailTo
        Dim strCC As String = ""
        Dim strBcc As String = emailBcc
        If strTo.Trim = String.Empty Then
            msg += "Email Penerima tidak ada."
            Exit Function
        End If

        Dim strBody As String = GetTemplateEmail(objClaim, isFromCancelled)
        Try
            objEmail.sendMail(strTo, strCC, strBcc, strFrom, strSubject, Mail.MailFormat.Html, strBody)
            retValue = 1
        Catch ex As Exception
            msg += ex.Message.Replace("""", "")
            retValue = 0
        End Try
        Return retValue
    End Function

    Public Function GetTemplateEmail(ByVal objClaim As ChassisMasterClaimHeader, ByVal isFromCanceled As Boolean) As String
        Dim body As String = ""
        Dim statusFakturList As String = String.Format("{0},{1},{2}", _
                         CInt(EnumChassisMaster.FakturStatus.Validasi), CInt(EnumChassisMaster.FakturStatus.Konfirmasi), CInt(EnumChassisMaster.FakturStatus.Proses))

        If isFromCanceled Then
            body = ReadFileTemplate("CBUReturnTemplateA.htm") _
                    .Replace("[ChassisNumberDefect]", objClaim.ChassisMaster.ChassisNumber) _
                    .Replace("[ClaimNumber]", objClaim.ClaimNumber)
        ElseIf statusFakturList.Contains(objClaim.ChassisMaster.FakturStatus) Then
            body = ReadFileTemplate("CBUReturnTemplateB.htm") _
                    .Replace("[ChassisNumberDefect]", objClaim.ChassisMaster.ChassisNumber) _
                    .Replace("[FakturStatus]", objClaim.ChassisMaster.FakturStatus) _
                    .Replace("[ChassisNumberReplacement]", objClaim.ChassisNumberReplacement)
        ElseIf objClaim.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
            body = ReadFileTemplate("CBUReturnTemplateD.htm") _
                    .Replace("[ChassisNumberReplacement]", objClaim.ChassisNumberReplacement)
        Else
            body = ReadFileTemplate("CBUReturnTemplateC.htm") _
                    .Replace("[ChassisNumberDefect]", objClaim.ChassisMaster.ChassisNumber) _
                    .Replace("[FakturStatus]", objClaim.ChassisMaster.FakturStatus) _
                    .Replace("[ChassisNumberReplacement]", objClaim.ChassisNumberReplacement)

        End If
        Return body
    End Function

    Public Function GetEmail(ByVal category As String) As String
        Dim email As String = ""
        Dim objAppConf As AppConfig

        Select Case category
            Case "RSD"
                objAppConf = objAppConfFacade.Retrieve(CBUReturnEmailToRSD)
                email = objAppConf.Value
            Case "WSD"
                objAppConf = objAppConfFacade.Retrieve(CBUReturnEmailToWSD)
                email = objAppConf.Value
            Case "VCD"
                objAppConf = objAppConfFacade.Retrieve(CBUReturnEmailToVCD)
                email = objAppConf.Value
            Case "EmailBCC"
                objAppConf = objAppConfFacade.Retrieve(EmailBCC)
                email = objAppConf.Value
        End Select

        Return email
    End Function

    Public Function ReadFileTemplate(ByVal filename As String) As String
        Dim fileReader As String
        fileReader = File.ReadAllText(Path.Combine(templateDir, filename))
        Return fileReader
    End Function
#End Region
End Class
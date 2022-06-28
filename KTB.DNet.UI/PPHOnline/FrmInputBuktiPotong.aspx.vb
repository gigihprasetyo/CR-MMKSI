Imports System.IO
Imports System
Imports System.Text
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports System.Drawing.Color
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports PDFHelper
Imports PdfSharp
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports Patagames.Pdf
Imports KTB.DNet.BusinessValidation
Imports OfficeOpenXml
#End Region

Public Class FrmInputBuktiPotong
    Inherits System.Web.UI.Page

    Dim lstData As New List(Of VW_SalesOrderInterest)
    Public enumStatusPPHInterest As New Dictionary(Of Integer, String)
    Private oDealer As New Dealer
    Private sessHelper As New SessionHelper
    Private isGenerate As Boolean
    Private _mode As String = String.Empty
    Private ListPriv As Boolean
    Private InputPriv As Boolean
    Private sessionName As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If Not IsPostBack Then
            Dim pageID As String = Guid.NewGuid().ToString()
            sessionName = pageID.Substring(pageID.Length = 10)
            ViewState("sessionName") = sessionName
            InitPriv()
            clearSession()
            setControl()
            bindDdlStatus()
            bindDdlStatusInput()
            setDealerNPWP()
            If Not IsNothing(Request.QueryString("mode")) AndAlso Request.QueryString("mode").ToString().ToLower() = "generate" Then
                Session("IBPMODE" + ViewState("sessionName")) = "generate"
                isGenerate = True
                setFieldsAndControls("generate")
            ElseIf Not IsNothing(Request.QueryString("mode")) AndAlso Request.QueryString("mode").ToString().ToLower() = "edit" Then
                Session("IBPMODE" + ViewState("sessionName")) = "edit"
                setFieldsAndControls("edit")
            ElseIf Not IsNothing(Request.QueryString("mode")) AndAlso Request.QueryString("mode").ToString().ToLower() = "view" Then
                Session("IBPMODE" + ViewState("sessionName")) = "view"
                setFieldsAndControls("view")
            Else
                Session("IBPMODE" + ViewState("sessionName")) = "new"
            End If

        End If
    End Sub

    Protected Sub btnOpenModal_Click(sender As Object, e As EventArgs) Handles btnOpenModal.Click
        pnlGeneratePayment.Visible = True
    End Sub

    Protected Sub dgBuktiPotong_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgBuktiPotong.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            Dim obj As VW_SalesOrderInterest = lstData(e.Item.ItemIndex)

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblNoPO As Label = CType(e.Item.FindControl("lblNoPO"), Label)
            Dim lblSO As Label = CType(e.Item.FindControl("lblSO"), Label)
            Dim lblBilling As Label = CType(e.Item.FindControl("lblBilling"), Label)
            Dim lblPercentage As Label = CType(e.Item.FindControl("lblPercentage"), Label)
            Dim lblDPP As Label = CType(e.Item.FindControl("lblDPP"), Label)
            Dim lblPPH As Label = CType(e.Item.FindControl("lblPPH"), Label)
            Dim lblAfterPPH As Label = CType(e.Item.FindControl("lblAfterPPH"), Label)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lblBillingDate As Label = CType(e.Item.FindControl("lblBillingDate"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
            Dim lblDocNumber As Label = CType(e.Item.FindControl("lblDocNumber"), Label)

            lblNo.Text = e.Item.ItemIndex + 1
            lblDealerCode.Text = obj.Dealer.DealerCode
            lblDealerName.Text = obj.Dealer.DealerName
            lblNoPO.Text = obj.PONumber
            lblSO.Text = obj.SONumber
            lblBilling.Text = obj.BillingNumber
            lblDocNumber.Text = obj.DocNumber
            Dim p As Decimal = 0
            If obj.PPHAmount <> 0 And obj.DPPAmount <> 0 Then
                p = obj.PPHAmount / obj.DPPAmount
            End If
            lblPercentage.Text = String.Format("{0:P2} %", p)
            lblDPP.Text = obj.DPPAmount.ToString("#,###.00")
            lblPPH.Text = obj.PPHAmount.ToString("#,###.00")
            lblAfterPPH.Text = (obj.DPPAmount - obj.PPHAmount).ToString("#,###.00")
            If _mode = "view" Then
                lbtnDelete.Visible = False
            End If
            lblBillingDate.Text = obj.BillingDate.ToString("dd/MM/yyyy")
        End If
    End Sub

    Protected Sub dgListSOInterest_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgListSOInterest.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            Dim items As ArrayList = dgListSOInterest.DataSource
            Dim obj As VW_SalesOrderInterest = CType(items(e.Item.ItemIndex), VW_SalesOrderInterest)

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblNoPO As Label = CType(e.Item.FindControl("lblNoPO"), Label)
            Dim lblSO As Label = CType(e.Item.FindControl("lblSO"), Label)
            Dim lblBilling As Label = CType(e.Item.FindControl("lblBilling"), Label)
            Dim lblType As Label = CType(e.Item.FindControl("lblType"), Label)
            Dim lblPercentage As Label = CType(e.Item.FindControl("lblPercentage"), Label)
            Dim lblDPP As Label = CType(e.Item.FindControl("lblDPP"), Label)
            Dim lblPPH As Label = CType(e.Item.FindControl("lblPPH"), Label)
            Dim lblAfterPPH As Label = CType(e.Item.FindControl("lblAfterPPH"), Label)
            Dim lblBillingDate As Label = CType(e.Item.FindControl("lblBillingDate"), Label)

            lblNo.Text = e.Item.ItemIndex + 1
            lblDealerCode.Text = obj.Dealer.DealerCode
            If Not IsNothing(obj.SalesOrder) Then
                lblSO.Text = obj.SalesOrder.SONumber
            End If
            lblNoPO.Text = obj.PONumber
            lblBilling.Text = obj.BillingNumber
            lblType.Text = obj.TrType

            Dim p As Decimal = 0
            If obj.PPHAmount <> 0 And obj.DPPAmount <> 0 Then
                p = obj.PPHAmount / obj.DPPAmount
            End If
            lblPercentage.Text = String.Format("{0:P2} %", p)
            lblDPP.Text = obj.DPPAmount.ToString("#,###.00")
            lblPPH.Text = obj.PPHAmount.ToString("#,###.00")
            lblAfterPPH.Text = (obj.DPPAmount - obj.PPHAmount).ToString("#,###.00")
            lblBillingDate.Text = obj.BillingDate.ToString("dd/MM/yyyy")
        End If
    End Sub

    Protected Sub btnSearchInput_Click(sender As Object, e As EventArgs) Handles btnSearchInput.Click
        bindInputDataGrid(0)
    End Sub

    Protected Sub btnAddInput_Click(sender As Object, e As EventArgs) Handles btnAddInput.Click
        Dim countChecked As Integer = 0
        Dim sumDPP As Decimal = 0
        Dim sumPPH As Decimal = 0
        Dim arrData As ArrayList = Session("ARRDATA" + ViewState("sessionName"))
        If Not IsNothing(Session("DATAINPUT" + ViewState("sessionName"))) Then
            lstData = Session("DATAINPUT" + ViewState("sessionName"))
        End If
        For Each row As DataGridItem In dgListSOInterest.Items

            Dim cbSelected As CheckBox = row.FindControl("chkAdd")

            If cbSelected.Checked = True Then
                Dim objToAdd As VW_SalesOrderInterest = CType(arrData(countChecked), VW_SalesOrderInterest)

                Dim res As List(Of VW_SalesOrderInterest) = lstData.Where(Function(o) o.ID = objToAdd.ID).ToList
                If res.Count = 0 Then
                    lstData.Add(objToAdd)
                End If
            End If
            countChecked = countChecked + 1
        Next
        For Each obj As VW_SalesOrderInterest In lstData
            sumDPP += obj.DPPAmount
            sumPPH += obj.PPHAmount
        Next
        lblTotalDPP.Text = sumDPP.ToString("#,###")
        lblTotalPPH.Text = sumPPH.ToString("#,###")
        Session("DATAINPUT" + ViewState("sessionName")) = lstData
        pnlGeneratePayment.Visible = False
        dgBuktiPotong.DataSource = lstData
        dgBuktiPotong.DataBind()
    End Sub

    Protected Sub dgBuktiPotong_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgBuktiPotong.ItemCommand
        If e.CommandName = "Delete" Then
            lstData = Session("DATAINPUT" + ViewState("sessionName"))
            lstData.RemoveAt(e.Item.ItemIndex)
            dgBuktiPotong.DataSource = lstData
            dgBuktiPotong.DataBind()
        End If
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If Page.IsValid Then
            _mode = Session("IBPMODE" + ViewState("sessionName"))
            Dim isEdit As Boolean = False
            Dim newObj As New InterestPPHHeader()
            If _mode = "edit" Then
                newObj = CType(Session("SAVEDDATA" + ViewState("sessionName")), InterestPPHHeader)
                isEdit = True
            End If
            newObj.Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim)

            If Not IsNothing(Session("EVIDENCEDOC" + ViewState("sessionName"))) Then
                Dim attachmentEvidence As HttpPostedFile = Session("EVIDENCEDOC" + ViewState("sessionName"))
                commitAttachment(attachmentEvidence, newObj, "EBukti", isEdit)
            Else
                If Not isEdit Then
                    MessageBox.Show("Dokumen E-Form wajib diupload")
                    Return
                End If
            End If

            If Not IsNothing(Session("REFDOC" + ViewState("sessionName"))) Then
                Dim attachmentRefDoc As HttpPostedFile = Session("REFDOC" + ViewState("sessionName"))
                commitAttachment(attachmentRefDoc, newObj, "EReference", isEdit)
            Else
                If Not isEdit Then
                    MessageBox.Show("Dokumen Pendukung wajib diupload")
                    Return
                End If
            End If

            newObj.WitholdingNumber = txtNoBuktiPotong.Text.Trim
            newObj.TaxPeriod = Date.Parse(txtMasaPajakThn.Text + "/" + txtMasaPajakBulan.Text + "/01")
            newObj.Remark = txtRemarks.Text
            newObj.DealerNPWP = txtNPWPPemotong.Text.Trim
            newObj.WitholdingDate = icTglPemotongan.Value.Date
            newObj.DealerTaxName = txtNamaPemotong.Text
            newObj.DealerSignatureName = txtNamaPenandatangan.Text
            newObj.TotalDPPAmount = getDec(txtPenghasilanBruto.Text.Trim)
            newObj.TotalPPHAmount = getDec(txtPPH.Text.Trim)
            newObj.ReferenceDocType = txtNamaDocReferensi.Text
            newObj.SubmissionStatus = ddlStatus.SelectedValue
            newObj.PembetulanKe = txtPembetulanKe.Text
            Dim msg As String = ""
            lstData = Session("DATAINPUT" + ViewState("sessionName"))
            If IsNothing(lstData) OrElse lstData.Count = 0 Then
                MessageBox.Show("Silahkan tambah data SO Interest terlebih dahulu")
                Return
            End If
            If Not validateInputAmount(lblTotalDPP.Text, CDec(txtPenghasilanBruto.Text.Trim), msg) Then
                MessageBox.Show("Nilai DPP tidak valid," + msg)
                Return
            End If
            If Not validateInputAmount(getDec(lblTotalPPH.Text), getDec(txtPPH.Text.Trim), msg) Then
                MessageBox.Show("Nilai PPH tidak valid," + msg)
                Return
            End If

            If isEdit Then
                Dim newDetails As New ArrayList()
                Dim detFacade As New InterestPPHDetailFacade(User)
                Dim SOIFacade As New SalesOrderInterestFacade(User)
                For Each SOInterest As VW_SalesOrderInterest In lstData
                    Dim detail As New InterestPPHDetail()
                    detail.SalesOrderInterest = New SalesOrderInterest(SOInterest.ID)
                    detail.InterestPPHHeader = newObj
                    newDetails.Add(detail)
                Next
                newObj.InterestPPHDetailsNew = newDetails
            Else
                For Each SOInterest As VW_SalesOrderInterest In lstData
                    Dim tempDetail As New InterestPPHDetail()
                    tempDetail.SalesOrderInterest = New SalesOrderInterest(SOInterest.ID)
                    newObj.InterestPPHDetails.Add(tempDetail)
                Next
            End If


            Dim result As Integer = 0
            If isEdit Then
                result = New InterestPPHHeaderFacade(User).Update(newObj)
            Else
                result = New InterestPPHHeaderFacade(User).Insert(newObj)
            End If

            If result > 0 Then
                If Not isEdit Then
                    Dim tempArrPPHDet As ArrayList = newObj.InterestPPHDetails
                    newObj = New InterestPPHHeaderFacade(User).Retrieve(result)
                    newObj.InterestPPHDetails = tempArrPPHDet
                    updateSOInterest(newObj, 0)
                End If
                Session("SAVEDDATA" + ViewState("sessionName")) = newObj
                txtNoPengajuan.Text = newObj.NoReg
                disableInput()
                btnValidate.Enabled = True
                MessageBox.Show("Simpan data berhasil")
                Session("REFDOC" + ViewState("sessionName")) = Nothing
                Session("EVIDENCEDOC" + ViewState("sessionName")) = Nothing
            End If
        End If
    End Sub

    Protected Sub btnUploadEvidence_Click(sender As Object, e As EventArgs) Handles btnUploadEvidence.Click
        Dim messages As String = String.Empty
        If IsNothing(iEvidenceDoc.PostedFile) OrElse String.IsNullorEmpty(iEvidenceDoc.PostedFile.FileName) Then
            MessageBox.Show("Silahkan pilih berkas")
            Exit Sub
        End If

        lblWarningEvidence.Text = ""
        Dim fileRefInfo As ArrayList = Session("FILEUPLOADREFERENCE" + ViewState("sessionName"))
        Session("EVIDENCEDOC" + ViewState("sessionName")) = Nothing
        If Not validateFile(iEvidenceDoc.PostedFile, fileRefInfo(0), fileRefInfo(2), messages) Then
            MessageBox.Show(messages)
            Return
        End If

        Dim sizes As String() = {"B", "KB", "MB", "GB", "TB"}
        Dim len As Double = iEvidenceDoc.PostedFile.ContentLength
        Dim order As Integer = 0

        While len >= 1024 And order < sizes.Length - 1
            order += 1
            len = len / 1024
        End While


        Dim resultSize As String = String.Format("{0:0.##} {1}", len, sizes(order))
        lblWarningEvidence.Text = iEvidenceDoc.PostedFile.FileName & "(" & resultSize & ")"
        Session("EVIDENCEDOC" + ViewState("sessionName")) = iEvidenceDoc.PostedFile
        btnParsePDF.Visible = True
        MessageBox.Show("Berkas Berhasil diupload")
        Return
    End Sub

    Protected Sub btnUploadReference_Click(sender As Object, e As EventArgs) Handles btnUploadReference.Click
        Dim messages As String = String.Empty
        If IsNothing(iReferenceDoc.PostedFile) OrElse String.IsNullorEmpty(iReferenceDoc.PostedFile.FileName) Then
            MessageBox.Show("Silahkan pilih berkas")
            Exit Sub
        End If

        lblWarningReference.Text = ""
        Dim fileRefInfo As ArrayList = Session("FILEUPLOADREFERENCE" + ViewState("sessionName"))
        Session("REFDOC" + ViewState("sessionName")) = Nothing
        If Not validateFile(iReferenceDoc.PostedFile, fileRefInfo(1), fileRefInfo(3), messages) Then
            MessageBox.Show(messages)
            Return
        End If

        Dim sizes As String() = {"B", "KB", "MB", "GB", "TB"}
        Dim len As Double = iReferenceDoc.PostedFile.ContentLength
        Dim order As Integer = 0

        While len >= 1024 And order < sizes.Length - 1
            order += 1
            len = len / 1024
        End While


        Dim resultSize As String = String.Format("{0:0.##} {1}", len, sizes(order))
        Dim objUpload As New InterestPPHHeader()

        If parseExcelReference(iReferenceDoc.PostedFile, objUpload) Then
            MessageBox.Show("Berkas Berhasil diupload")
            lblWarningReference.Text = iReferenceDoc.PostedFile.FileName & "(" & resultSize & ")"
            Session("REFDOC" + ViewState("sessionName")) = iReferenceDoc.PostedFile
            btnParsePDF.Visible = True
            txtNoBuktiPotong.Text = objUpload.WitholdingNumber
            txtMasaPajakBulan.Text = objUpload.TaxPeriod.Month
            txtMasaPajakThn.Text = objUpload.TaxPeriod.Year
            icTglPemotongan.Value = objUpload.WitholdingDate
            txtNPWPPemotong.Text = objUpload.DealerNPWP
            txtNamaPemotong.Text = objUpload.DealerTaxName
            txtPenghasilanBruto.Text = objUpload.TotalDPPAmount.ToString("#,###")
            txtPPH.Text = objUpload.TotalPPHAmount.ToString("#,###")
            txtNamaDocReferensi.Text = objUpload.ReferenceDocName
            txtPembetulanKe.Text = objUpload.PembetulanKe
        Else
            MessageBox.Show("Berkas gagal diupload")
        End If
        Return
    End Sub

    Protected Sub btnLoadDealerName_Click(sender As Object, e As EventArgs) Handles btnLoadDealerName.Click
        If txtDealerCode.Text.Trim <> String.Empty Then
            Dim tempDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim)
            If tempDealer.ID <> 0 Then
                lblNamaDealer.Text = tempDealer.DealerName
                setDealerNPWP(tempDealer.DealerCode, tempDealer.DealerName)
            End If
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim path As String = Server.MapPath("") & "\" & File1.PostedFile.FileName
        File1.PostedFile.SaveAs(path)
        readStringPDF(path)
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

    End Sub

    Protected Sub btnDownloadEv_Click(sender As Object, e As EventArgs) Handles btnDownloadEv.Click
        Dim filePath As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") + hdnPathEvidence.Value.ToString()
        Response.Redirect("../Download.aspx?file=" & filePath & "&name=" & Path.GetFileNameWithoutExtension(filePath))
    End Sub

    Protected Sub btnDownloadRef_Click(sender As Object, e As EventArgs) Handles btnDownloadRef.Click
        Dim filePath As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") + hdnPathRef.Value.ToString()
        Response.Redirect("../Download.aspx?file=" & filePath & "&name=" & Path.GetFileNameWithoutExtension(filePath))
    End Sub

    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        If Not IsNothing(Request.QueryString("mode")) Then
            If Request.QueryString("mode").ToString().ToLower() = "generate" Then
                Server.Transfer("../PPHOnline/FrmDaftarPPHInterest.aspx")
            Else
                Dim prevSessID As String = Request.QueryString("sesID").ToString()
                Dim prevPage As String = Request.QueryString("page").ToString()
                Server.Transfer("../PPHOnline/FrmDaftarBuktiPotongInterest.aspx?sesID=" & prevSessID & "&page=" & prevPage)
            End If
        End If
    End Sub

    Protected Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
        Dim objToValidate As InterestPPHHeader = Session("SAVEDDATA" + ViewState("sessionName"))
        objToValidate.SubmissionStatus = 1
        Dim res As Integer = New InterestPPHHeaderFacade(User).Update(objToValidate)
        updateSOInterest(objToValidate, 1)

        If res > 0 Then
            MessageBox.Show("Data berhasil divalidasi")
            btnValidate.Enabled = False
            ddlStatus.SelectedValue = 1
        End If
    End Sub

#Region "Custom method"

    Private Sub InitPriv()
        ListPriv = SecurityProvider.Authorize(Context.User, SR.SOPPHForm_List_Privilage)
        InputPriv = SecurityProvider.Authorize(Context.User, SR.SOPPhForm_input_Privilage)
        If Not InputPriv Then
        End If

        If Not ListPriv Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Status DO - Input Bukti Potong Interest")
        End If
    End Sub

    Private Sub commitAttachment(ByVal attachment As HttpPostedFile, ByRef oPPH As InterestPPHHeader, ByVal fileType As String, ByVal isEdit As Boolean)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim targetDIrectory As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder")
        Dim PPHOnlineDirectory As String = KTB.DNet.Lib.WebConfig.GetValue("PPHOnlineDirectory")
        Dim ID As String = Guid.NewGuid().ToString()
        Dim fileName As String = oPPH.Dealer.DealerCode + "-" + fileType + ID.Substring(ID.Length - 10) + Path.GetExtension(attachment.FileName)

        PPHOnlineDirectory = PPHOnlineDirectory + DateTime.Now.Year.ToString() + "\" + DateTime.Now.ToString("MM") + "\" + fileName

        Dim TargetFInfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(attachment) Then
                    TargetFInfo = New FileInfo(targetDIrectory + PPHOnlineDirectory)

                    If Not TargetFInfo.Directory.Exists Then
                        Directory.CreateDirectory(TargetFInfo.DirectoryName)
                    End If

                    attachment.SaveAs(targetDIrectory + PPHOnlineDirectory)

                    Dim deleteFile As FileInfo

                    If fileType = "EBukti" Then
                        If isEdit Then
                            deleteFile = New FileInfo(targetDIrectory + oPPH.EvidencePDFPath)
                            deleteFile.Delete()
                        End If
                        oPPH.EvidencePDFName = attachment.FileName
                        oPPH.EvidencePDFPath = PPHOnlineDirectory
                    Else
                        If isEdit Then
                            deleteFile = New FileInfo(targetDIrectory + oPPH.ReferenceDocPath)
                            deleteFile.Delete()
                        End If
                        oPPH.ReferenceDocName = attachment.FileName
                        oPPH.ReferenceDocPath = PPHOnlineDirectory
                    End If
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            imp.StopImpersonate()
            imp = Nothing
            Throw ex
        End Try

    End Sub

    Private Function getEvidenceSupportedFileType() As ArrayList
        Dim result As New ArrayList
        Dim oCfg As New AppConfigFacade(User)
        Dim strFType As String() = oCfg.Retrieve("PPHOnline.SupportedFile.Evidence").Value.Split(",")
        For Each ft As String In strFType
            result.Add("." + ft.Trim.ToUpper())
        Next

        Return result
    End Function

    Private Function getReferenceSupportedFileType() As ArrayList
        Dim result As New ArrayList
        Dim oCfg As New AppConfigFacade(User)
        Dim strFType As String() = oCfg.Retrieve("PPHOnline.SupportedFile.Reference").Value.Split(",")
        For Each ft As String In strFType
            result.Add("." + ft.Trim.ToUpper())
        Next
        Return result
    End Function

    Private Sub setControl()
        btnKembali.Visible = False
        btnValidate.Enabled = False
        If oDealer.Title = CInt(EnumDealerTittle.DealerTittle.KTB) Then 'MKS
            lblTitle.Text = "Sales - PPH Interet- Detil Bukti Potong PPH Interest"
            btnOpenModal.Visible = False
            btnSearch.Visible = False
            btnValidate.Visible = False
            pnlUploadDoc.Visible = False
            btnSimpan.Visible = False
            ddlStatus.Enabled = True
            lbtnTemplateRef.Visible = False
        Else
            lblTitle.Text = "Sales - PPH Interet- Detil Input Bukti Potong PPH Interest"
            txtDealerCode.Text = oDealer.DealerCode
            txtKodeDealerInput.Text = oDealer.DealerCode
            lblNamaDealer.Text = oDealer.DealerName
            btnOpenModal.Visible = True
            btnSearch.Visible = False
            Dim sizeEvidence As String = New AppConfigFacade(User).Retrieve("PPHOnline.MaxSize.Evidence").Value
            Dim sizeReference As String = New AppConfigFacade(User).Retrieve("PPHOnline.MaxSize.Reference").Value
            Dim supportedEvidence As String = New AppConfigFacade(User).Retrieve("PPHOnline.SupportedFile.Evidence").Value.ToUpper()
            Dim supportedReference As String = New AppConfigFacade(User).Retrieve("PPHOnline.SupportedFile.Reference").Value.ToUpper()
            lblFileFormatInfoEv.Text = String.Format("- File yang didukung : {0}, Max size {1} MB", supportedEvidence, sizeEvidence / 1024)
            lblFileFormatInfoRef.Text = String.Format("- File yang didukung : {0}, Max size {1} MB", supportedReference, sizeReference / 1024)
            Dim fileUploadRefInfo As New ArrayList()
            fileUploadRefInfo.Add(sizeEvidence)
            fileUploadRefInfo.Add(sizeReference)
            fileUploadRefInfo.Add(supportedEvidence)
            fileUploadRefInfo.Add(supportedReference)
            Session("FILEUPLOADREFERENCE" + ViewState("sessionName")) = fileUploadRefInfo
        End If

        btnDownloadEv.Visible = False
        btnDownloadRef.Visible = False

        If Not InputPriv Then
            btnOpenModal.Visible = False
            btnUploadEvidence.Visible = False
            btnUploadReference.Visible = False
            btnSimpan.Visible = False
        End If
    End Sub

    Private Sub clearInput()
        txtDealerCode.Text = ""
        txtNoPengajuan.Text = ""
        ddlStatus.SelectedValue = ""
        txtNoBuktiPotong.Text = ""
        txtMasaPajakBulan.Text = ""
        txtMasaPajakThn.Text = ""
        txtNPWPPemotong.Text = ""
        icTglPemotongan.Value = ""
        txtNamaPenandatangan.Text = ""
        txtPenghasilanBruto.Text = "0"
        txtPPH.Text = "0"
        lblUploadedFile.Text = ""
        lblUploadedReference.Text = ""
        txtNamaDocReferensi.Text = ""
    End Sub

    Private Sub disableInput()
        btnSimpan.Enabled = False
        btnAddInput.Enabled = False
        btnOpenModal.Enabled = False
        txtDealerCode.Enabled = False
        txtNoPengajuan.Enabled = False
        ddlStatus.Enabled = False
        txtNoBuktiPotong.Enabled = False
        txtMasaPajakBulan.Enabled = False
        txtMasaPajakThn.Enabled = False
        txtNPWPPemotong.Enabled = False
        txtNamaPemotong.Enabled = False
        txtRemarks.Enabled = False
        icTglPemotongan.Enabled = False
        txtNamaPenandatangan.Enabled = False
        txtPenghasilanBruto.Enabled = False
        txtPPH.Enabled = False
        lblUploadedFile.Enabled = False
        lblUploadedReference.Enabled = False
        txtNamaDocReferensi.Enabled = False
        btnUploadEvidence.Enabled = False
        btnUploadReference.Enabled = False
    End Sub

    Private Sub readStringPDF(ByVal tempFileName As String)
        Dim pdfDoc As PdfDocument = PdfReader.Open(tempFileName, PdfDocumentOpenMode.Modify)
        Dim outputText As String = ""

        For idx As Integer = 0 To pdfDoc.PageCount - 1
            For index As Integer = 0 To pdfDoc.Pages(idx).Contents.Elements.Count - 1
                Dim stream As PdfDictionary.PdfStream = pdfDoc.Pages(idx).Contents.Elements.GetDictionary(index).Stream
                outputText += New PDFParser().ExtractTextFromPDFBytes(stream.Value) + "///////////////////"
            Next
        Next

        Dim start As Integer = outputText.IndexOf("A.1")
        Dim fname As String = Path.GetDirectoryName(tempFileName) + Path.GetFileNameWithoutExtension(tempFileName) + DateTime.Now.ToString("hhmmssfff") + ".txt"

        Dim fs As FileStream = File.Create(fname)

        ' Add text to the file.
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(outputText)
        fs.Write(info, 0, info.Length)
        fs.Close()

        Dim f As FileInfo = New FileInfo(tempFileName)
        f.Delete()
        Return
    End Sub

    Private Function GetStringPDF(ByVal tempFileName As String) As String
        Dim pdfDoc As PdfDocument = PdfReader.Open(tempFileName, PdfDocumentOpenMode.ReadOnly)
        Dim outputText As String = ""

        Try
            For idx As Integer = 0 To pdfDoc.PageCount - 1
                For index As Integer = 0 To pdfDoc.Pages(idx).Contents.Elements.Count - 1
                    Dim stream As PdfDictionary.PdfStream = pdfDoc.Pages(idx).Contents.Elements.GetDictionary(index).Stream
                    outputText += New PDFParser().ExtractTextFromPDFBytes(stream.Value) + "///////////////////"
                Next
            Next

        Catch ex As Exception
            Dim aa = ex
        End Try

        Return outputText
    End Function

    Private Sub bindInputDataGrid(ByVal pgIndex As Integer)
        Dim arrData As ArrayList = New VW_SalesOrderInterestFacade(User).Retrieve(getInputSearchCriteria())
        Session("ARRDATA" + ViewState("sessionName")) = arrData
        enumStatusPPHInterest = CType(Session("ENUMSTATUS" + ViewState("sessionName")), Dictionary(Of Integer, String))
        dgListSOInterest.DataSource = arrData
        dgListSOInterest.DataBind()
    End Sub

    Private Sub bindDdlStatus()
        Dim crit As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, 0))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "enumStatusPPHInterest"))
        Dim arrStatus As ArrayList = New StandardCodeFacade(User).Retrieve(crit)

        For Each s As StandardCode In arrStatus
            ddlStatus.Items.Add(New ListItem(s.ValueDesc, s.ValueId))
            enumStatusPPHInterest(s.ValueId) = s.ValueDesc
        Next
        Session("ENUMSTATUS" + ViewState("sessionName")) = enumStatusPPHInterest
        ddlStatus.SelectedValue = 0
        ddlStatus.Enabled = False
    End Sub

    Private Sub bindDdlStatusInput()
        Dim crit As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, 0))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "enumStatusPPHInterest"))
        crit.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.InSet, "(-1,2 )"))
        Dim arrStatus As ArrayList = New StandardCodeFacade(User).Retrieve(crit)

        ddlStatusInput.Items.Add(New ListItem("Semua", -2))
        For Each s As StandardCode In arrStatus
            ddlStatusInput.Items.Add(New ListItem(s.ValueDesc, s.ValueId))
            enumStatusPPHInterest(s.ValueId) = s.ValueDesc
        Next
        Session("ENUMSTATUS") = enumStatusPPHInterest
    End Sub

    Private Sub bindDataGrid()
        lblTotalDPP.Text = "0"
        lblTotalPPH.Text = "0"
        Dim sumDPP As Decimal = 0
        Dim sumPPH As Decimal = 0
        lstData = Session("DATAINPUT" + ViewState("sessionName"))
        If lstData.Count > 0 Then
            dgBuktiPotong.DataSource = lstData
            dgBuktiPotong.DataBind()

            For Each Data As VW_SalesOrderInterest In lstData

                sumDPP += Data.DPPAmount
                sumPPH += Data.PPHAmount
            Next
            lblTotalDPP.Text = sumDPP.ToString("#,###")
            lblTotalPPH.Text = sumPPH.ToString("#,###")
        End If



    End Sub

    Private Function getInputSearchCriteria() As CriteriaComposite
        oDealer = Session("DEALER")
        Dim crit As New CriteriaComposite(New Criteria(GetType(VW_SalesOrderInterest), "RowStatus", MatchType.Exact, 0))
        crit.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "BillingDate", MatchType.GreaterOrEqual, icPeriodStart.Value))
        crit.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "BillingDate", MatchType.LesserOrEqual, icPeriodEnd.Value))
        If Not txtKodeDealerInput.Text.Trim = String.Empty Then
            crit.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealerInput.Text.Trim.Replace(";", "','").Trim() & "')"))


        End If
        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            crit.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(oDealer, User)))
        End If

        If Not ddlStatusInput.SelectedValue = -2 Then
            crit.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "Status", MatchType.Exact, ddlStatusInput.SelectedValue))
        Else
            crit.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "Status", MatchType.InSet, "(-1,2)"))
        End If

        If Not txtSOInput.Text.Trim = String.Empty Then
            crit.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "SONumber", MatchType.Exact, txtSOInput.Text.Trim))
        End If

        Return crit
    End Function

    Private Sub setFieldsAndControls(ByVal qStr As String)
        lblTotalDPP.Text = "0"
        lblTotalPPH.Text = "0"
        If qStr = "generate" Then
            Dim sessionID As String = Request.QueryString("sessionID")
            Dim tempData As ArrayList = Session("ARRSOINTERESTTOGENERATE" + sessionID)
            Dim sumDPP As Decimal = 0
            Dim sumPPH As Decimal = 0
            For Each Data As VW_SalesOrderInterest In tempData
                lstData.Add(Data)
                sumDPP += Data.DPPAmount
                sumPPH += Data.PPHAmount
            Next
            txtPenghasilanBruto.Text = sumDPP.ToString("#,###")
            txtPPH.Text = sumPPH.ToString("#,###")
            Session("DATAINPUT" + ViewState("sessionName")) = lstData
            bindDataGrid()
            btnKembali.Visible = True
        ElseIf qStr = "edit" Or qStr = "view" Then
            _mode = qStr
            Dim idToEdit As Integer = Request.QueryString("id")
            Dim objToEdit As InterestPPHHeader = New InterestPPHHeaderFacade(User).Retrieve(idToEdit)
            Dim arrDetails As ArrayList = New InterestPPHDetailFacade(User).RetrieveDetails(idToEdit)
            For Each dtl As InterestPPHDetail In arrDetails
                lstData.Add(New VW_SalesOrderInterestFacade(User).Retrieve(dtl.SalesOrderInterest.ID))
            Next
            objToEdit.InterestPPHDetails = arrDetails
            Session("DATAINPUT" + ViewState("sessionName")) = lstData
            bindDataGrid()
            txtDealerCode.Text = objToEdit.Dealer.DealerCode
            lblNamaDealer.Text = objToEdit.Dealer.DealerName
            txtNoPengajuan.Text = objToEdit.NoReg
            ddlStatus.SelectedValue = objToEdit.SubmissionStatus
            txtRemarks.Text = objToEdit.Remark
            txtNoBuktiPotong.Text = objToEdit.WitholdingNumber
            txtMasaPajakBulan.Text = objToEdit.TaxPeriod.ToString("MM")
            txtMasaPajakThn.Text = objToEdit.TaxPeriod.Year
            txtNPWPPemotong.Text = objToEdit.DealerNPWP
            txtNamaPemotong.Text = objToEdit.DealerTaxName
            icTglPemotongan.Value = objToEdit.WitholdingDate
            txtNamaPenandatangan.Text = objToEdit.DealerSignatureName
            txtPenghasilanBruto.Text = CDec(objToEdit.TotalDPPAmount).ToString("#,###")
            txtPPH.Text = CDec(objToEdit.TotalPPHAmount).ToString("#,###")
            lblUploadedFile.Text = objToEdit.EvidencePDFName
            lblUploadedReference.Text = objToEdit.ReferenceDocName
            txtNamaDocReferensi.Text = objToEdit.ReferenceDocType
            txtPembetulanKe.Text = objToEdit.PembetulanKe



            If lblUploadedFile.Text.Trim <> String.Empty Then
                btnDownloadEv.Visible = True
                hdnPathEvidence.Value = objToEdit.EvidencePDFPath
            End If
            If lblUploadedReference.Text.Trim <> String.Empty Then
                btnDownloadRef.Visible = True
                hdnPathRef.Value = objToEdit.ReferenceDocPath
            End If

            If qStr = "view" Then
                disableInput()
                pnlUploadDoc.Visible = True
            Else
                If oDealer.Title = 1 Then
                    disableInput()
                    lblTitle.Text = "Sales - Status DO - Input Bukti Potong PPH Interest"
                    btnOpenModal.Visible = True
                    btnOpenModal.Enabled = True
                    btnValidate.Visible = False
                    pnlUploadDoc.Visible = True
                    btnSimpan.Visible = True
                    txtPPH.Enabled = True
                    txtPenghasilanBruto.Enabled = True
                    txtNPWPPemotong.Enabled = True
                    txtNamaPemotong.Enabled = True
                    txtRemarks.Enabled = True
                    ddlStatus.Enabled = False
                    btnSimpan.Enabled = True
                    btnAddInput.Enabled = True
                End If
            End If

            If objToEdit.SubmissionStatus = 0 Then
                btnValidate.Enabled = True
            End If
            btnKembali.Visible = True
            Session("SAVEDDATA" + ViewState("sessionName")) = objToEdit
        End If
    End Sub

    Private Sub updateSOInterest(ByVal newObj As InterestPPHHeader, ByVal status As Integer)
        If newObj.InterestPPHDetails.Count > 0 Then
            Dim SOIFacade As New SalesOrderInterestFacade(User)
            For Each obj As InterestPPHDetail In newObj.InterestPPHDetails
                Dim objToUpdate As SalesOrderInterest = SOIFacade.Retrieve(obj.SalesOrderInterest.ID)
                objToUpdate.Status = status
                objToUpdate.InterestPPHHeader = New InterestPPHHeader(newObj.ID)
                Dim res = SOIFacade.Update(objToUpdate)
            Next
        End If
    End Sub

    'Private Function validateInputAmount(ByVal arrData As List(Of VW_SalesOrderInterest), ByVal inputTotal As Decimal, ByVal flag As Boolean) As Boolean

    '    Dim sum As Decimal = 0
    '    If flag Then
    '        For Each SOInterest As VW_SalesOrderInterest In lstData
    '            sum += SOInterest.PPHAmount
    '        Next
    '    Else
    '        For Each SOInterest As VW_SalesOrderInterest In lstData
    '            sum += SOInterest.DPPAmount
    '        Next
    '    End If

    '    Dim selisih As Double = Double.Parse(New AppConfigFacade(User).Retrieve("PPHOnline.SupportedFile.maxSelisihAmount").Value)
    '    If inputTotal < sum Then
    '        If inputTotal - sum > selisih Then
    '            Return False
    '        Else
    '            Return True
    '        End If
    '    End If

    '    Return True
    'End Function

    Private Function validateInputAmount(ByVal sumFromSO As Double, ByVal inputTotal As Decimal, Optional ByRef Msg As String = "") As Boolean
        Dim selisihBawah As Double = Double.Parse(New AppConfigFacade(User).Retrieve("PPHOnline.SupportedFile.MaxLostAmount").Value)
        Dim selisihAtas As Double = Double.Parse(New AppConfigFacade(User).Retrieve("PPHOnline.SupportedFile.MaxProfitAmount").Value)
        Msg = ""
        'PPHOnline.SupportedFile.MaxProfitAmount()
        'PPHOnline.SupportedFile.MaxLostAmount()

        '        1. Nilai pph actual yg dibayar dealer < nilai pph di SAP -> misloss
        '2. Nilai pph actual yg dibayar dealer > nilai pph di SAP -> mispro

        'untuk misloss, kalo selisihnya lebih dari 1000 stop

        If (sumFromSO - inputTotal) = 0 Then
            Return True
        End If
        '
        If (inputTotal < sumFromSO) AndAlso (sumFromSO - inputTotal) > selisihBawah Then
            Msg = "Selisih lebih dari " + selisihBawah.ToString("#,###")
            Return False
        End If

        If (inputTotal > sumFromSO) AndAlso (inputTotal - sumFromSO) > selisihAtas Then
            Msg = "Selisih lebih dari " + selisihAtas.ToString("#,###")
            Return False
        End If

        Return True
    End Function

    Private Sub clearSession()
        Session("SAVEDDATA" + ViewState("sessionName")) = Nothing
        Session("REFDOC" + ViewState("sessionName")) = Nothing
        Session("EVIDENCEDOC" + ViewState("sessionName")) = Nothing
        Session("IBPMODE" + ViewState("sessionName")) = Nothing
    End Sub

    Private Function validateFile(ByVal file As HttpPostedFile, ByVal maxZise As Integer, ByVal strFormat As String, ByRef messages As String) As Boolean
        Dim ext As String = Path.GetExtension(file.FileName).Substring(1).ToUpper()
        Dim supported As String() = strFormat.Split(",")
        If Not supported.Contains(ext) Then
            messages = "Format file tidak didukung"
            Return False
        End If
        If file.ContentLength > maxZise * 1024 Then
            messages = "Ukuran file melebihi batas maksimal"
            Return False
        End If

        Return True
    End Function

    Private Sub setDealerNPWP()
        Dim crit As New CriteriaComposite(New Criteria(GetType(DealerPajak), "RowStatus", MatchType.Exact, 0))
        crit.opAnd(New Criteria(GetType(DealerPajak), "Dealer.ID", MatchType.Exact, oDealer.ID))
        Dim arrDP As ArrayList = New DealerPajakFacade(User).Retrieve(crit)
        If arrDP.Count > 0 Then
            Dim objDP As DealerPajak = CType(arrDP(0), DealerPajak)
            Dim strNPWP As String = objDP.NPWP.Replace(".", "").Replace("-", "")
            txtNPWPPemotong.Text = strNPWP
        End If
        txtNamaPemotong.Text = oDealer.DealerName
    End Sub

    Private Sub setDealerNPWP(ByVal DealerCode As String, ByVal Dealername As String)
        Dim crit As New CriteriaComposite(New Criteria(GetType(DealerPajak), "RowStatus", MatchType.Exact, 0))
        crit.opAnd(New Criteria(GetType(DealerPajak), "Dealer.DealerCode", MatchType.Exact, DealerCode))
        Dim arrDP As ArrayList = New DealerPajakFacade(User).Retrieve(crit)
        If arrDP.Count > 0 Then
            Dim objDP As DealerPajak = CType(arrDP(0), DealerPajak)
            Dim strNPWP As String = objDP.NPWP.Replace(".", "").Replace("-", "")
            txtNPWPPemotong.Text = strNPWP
        End If
        txtNamaPemotong.Text = Dealername
    End Sub

    Private Function parseExcelReference(ByVal f As HttpPostedFile, ByRef oInterestPPHUpload As InterestPPHHeader) As Boolean
        Try
            Dim arrUploadInterest As New ArrayList()
            Dim result As Boolean = True
            Using pck As New ExcelPackage(f.InputStream)
                Dim ws As ExcelWorksheet = pck.Workbook.Worksheets(1)
                Dim row As Integer = 2
                'While Not IsNothing(ws.Cells(row, 2).Value)
                oInterestPPHUpload.ErrorMessage = ""
                For col As Integer = 1 To 17
                    Select Case col
                        Case 2 'tahun pajak
                            If IsNothing(ws.Cells(row, col).Value) Then
                                oInterestPPHUpload.ErrorMessage += "<tr><td>Harap isi tahun pajak</td></tr>"
                            Else
                                Dim taxYear As Date
                                If Date.TryParse(ws.Cells(row, col).Value.ToString() + "/01/01", taxYear) Then
                                    oInterestPPHUpload.TaxPeriod = taxYear
                                Else
                                    oInterestPPHUpload.ErrorMessage += "<tr><td>Format tahun pajak salah</td></tr>"
                                End If
                            End If

                        Case 3 'masa pajak
                            If IsNothing(ws.Cells(row, col).Value) Then
                                oInterestPPHUpload.ErrorMessage += "<tr><td>Harap isi masa pajak</td></tr>"
                            Else
                                Dim taxYear As Date = oInterestPPHUpload.TaxPeriod
                                Dim intMonth As Integer = 0
                                If Integer.TryParse(ws.Cells(row, col).Value.ToString, intMonth) Then
                                    If Not Date.TryParse(taxYear.Year.ToString + "/" + intMonth.ToString + "/01", taxYear) Then
                                        oInterestPPHUpload.ErrorMessage += "<tr><td>Format bulan pajak salah</td></tr>"
                                    Else
                                        oInterestPPHUpload.TaxPeriod = taxYear
                                    End If
                                Else
                                    oInterestPPHUpload.ErrorMessage += "<tr><td>Format masa pajak salah</td></tr>"
                                End If
                            End If


                        Case 5  'no bukti potong
                            If IsNothing(ws.Cells(row, col).Value) Then
                                oInterestPPHUpload.ErrorMessage += "<tr><td>Harap isi nomor bukti potong</td></tr>"
                            Else
                                oInterestPPHUpload.WitholdingNumber = ws.Cells(row, col).Value.ToString()
                            End If

                        Case 4 'identitas dipotong
                            If IsNothing(ws.Cells(row, col).Value) Then
                                oInterestPPHUpload.ErrorMessage += "<tr><td>Harap isi identitas dipoong</td></tr>"
                            Else
                                oInterestPPHUpload.DealerNPWP = ws.Cells(row, col).Value.ToString()
                            End If

                        Case 8 'nama pemotong
                            If IsNothing(ws.Cells(row, col).Value) Then
                                oInterestPPHUpload.ErrorMessage += "<tr><td>Harap isi nama dipotong</td></tr>"
                            Else
                                oInterestPPHUpload.DealerTaxName = ws.Cells(row, col).Value.ToString()
                            End If

                        Case 10  'jumlah bruto
                            If IsNothing(ws.Cells(row, col).Value) Then
                                oInterestPPHUpload.ErrorMessage += "<tr><td>Harap isi jumlah bruto</td></tr>"
                            Else
                                Dim tempDPP As Double
                                If Double.TryParse(ws.Cells(row, col).Value.ToString(), tempDPP) Then
                                    oInterestPPHUpload.TotalDPPAmount = tempDPP
                                Else
                                    oInterestPPHUpload.ErrorMessage += "<tr><td>Format jumlah bruto salah</td></tr>"
                                End If
                            End If

                        Case 11  'jumlah pph
                            If IsNothing(ws.Cells(row, col).Value) Then
                                oInterestPPHUpload.ErrorMessage += "<tr><td>Harap isi jumlah pph</td></tr>"
                            Else
                                Dim tempPPH As Double
                                If Double.TryParse(ws.Cells(row, col).Value.ToString(), tempPPH) Then
                                    oInterestPPHUpload.TotalPPHAmount = tempPPH
                                Else
                                    oInterestPPHUpload.ErrorMessage += "<tr><td>Format jumlah pph salah</td></tr>"
                                End If
                            End If

                        Case 14  'nama dok ref
                            If IsNothing(ws.Cells(row, col).Value) Then
                                oInterestPPHUpload.ErrorMessage += "<tr><td>Harap isi nama dok ref</td></tr>"
                            Else
                                oInterestPPHUpload.ReferenceDocName = ws.Cells(row, col).Value.ToString()
                            End If

                        Case 14  'pembetulan ke
                            If IsNothing(ws.Cells(row, col).Value) Then
                                oInterestPPHUpload.ErrorMessage += "<tr><td>Harap isi pembetulan ke</td></tr>"
                            Else
                                oInterestPPHUpload.PembetulanKe = ws.Cells(row, col).Value
                            End If

                        Case 15  'nomor dok ref
                            If IsNothing(ws.Cells(row, col).Value) Then
                                oInterestPPHUpload.ErrorMessage += "<tr><td>Harap isi nomor dok ref</td></tr>"
                            Else
                                oInterestPPHUpload.ReferenceDocNumber = ws.Cells(row, col).Value.ToString()
                            End If

                        Case 16  'nomor dok ref
                            If IsNothing(ws.Cells(row, col).Value) Then
                                oInterestPPHUpload.ErrorMessage += "<tr><td>Harap isi tanggal dok ref</td></tr>"
                            Else
                                oInterestPPHUpload.WitholdingDate = ws.Cells(row, col).Value.ToString()
                            End If
                    End Select
                Next

                If oInterestPPHUpload.ErrorMessage <> String.Empty Then
                    result = False
                End If
                'End While
                Return result
            End Using
        Catch ex As Exception
            MessageBox.Show("File Excel gagal diproses")
            Return False
        End Try
    End Function
#End Region


    Protected Sub btnParsePDF_Click(sender As Object, e As EventArgs) Handles btnParsePDF.Click



        If Not IsNothing(Session("EVIDENCEDOC" + ViewState("sessionName"))) Then
            Dim attachmentEvidence As HttpPostedFile = Session("EVIDENCEDOC" + ViewState("sessionName"))

            Dim path As String = Server.MapPath("") & "\DataTemp\PPHOnline\" & DateTime.Now.ToString("yyyyMMddHHmmsstt") & attachmentEvidence.FileName
            attachmentEvidence.SaveAs(path)
            Dim pdfString As String = String.Empty
            pdfString = GetStringPDF(path)
            'Dim sHelper As New SalesOrderInterestValidation.PPHWitholdingForm(pdfString)
            'If sHelper.IsValid Then

            '    txtNoBuktiPotong.Text = sHelper.NoBuktiPotong
            '    txtNPWPPemotong.Text = sHelper.NPWPPemotong
            '    txtNamaPemotong.Text = sHelper.NamaWajibPajak

            '    MessageBox.Show("Berkas berhasil dibaca")
            'Else
            '    MessageBox.Show("Berkas tidak dapat dibaca")
            'End If

        End If

    End Sub

    Protected Sub lbtnTemplateRef_Click(sender As Object, e As EventArgs) Handles lbtnTemplateRef.Click
        Response.Redirect("../downloadlocal.aspx?file=DataFile\PPHInterest\Template_PPH_Online.xlsx")
    End Sub

    Protected Sub btnTestAja_Click(sender As Object, e As EventArgs) Handles btnTestAja.Click
        If validateInputAmount(getDec(lblTotalDPP.Text), getDec(txtPenghasilanBruto.Text)) Then
            MessageBox.Show("Diterima")
        Else
            MessageBox.Show("Tidak diterima")
        End If
    End Sub

    Private Function getDec(ByVal txtValue As String) As Decimal
        Dim val As Decimal = 0

        Try
            val = CDec(txtValue.Replace(".", ""))
        Catch ex As Exception

        End Try
        Return val

    End Function

    Protected Sub lnkSOdownload_Click(sender As Object, e As EventArgs) Handles lnkSOdownload.Click
        Response.Redirect("../downloadlocal.aspx?file=DataFile\PPHInterest\SO_PPH.xlsx")
    End Sub

    Protected Sub btnSOupload_Click(sender As Object, e As EventArgs) Handles btnSOupload.Click
        If (Not fileSO.PostedFile Is Nothing) And (fileSO.PostedFile.ContentLength > 0) Then
            Dim ext As String = Path.GetExtension(fileSO.PostedFile.FileName).Substring(1).ToUpper()
            If (ext.ToUpper = "XLSX") Then
                Try
                    Dim ls As New List(Of String)
                    If parseExcelSO(fileSO.PostedFile, ls) Then
                        If ls.Count = 0 Then
                            MessageBox.Show("Data SO kosong")
                            Exit Sub
                        End If
                        oDealer = Session("DEALER")
                        Dim crit As New CriteriaComposite(New Criteria(GetType(VW_SalesOrderInterest), "RowStatus", MatchType.Exact, 0))



                        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                            crit.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(oDealer, User)))
                        End If

                        Dim lsSO As String = ""

                        For Each Str As String In ls
                            If lsSO = "" Then
                                lsSO = Str.Trim()
                            Else
                                lsSO = lsSO & ";" & Str.Trim()
                            End If

                        Next

                        crit.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "Status", MatchType.InSet, "(-1,2)"))



                        crit.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "SONumber", MatchType.InSet, "('" & lsSO.Replace(";", "','").Trim() & "')"))




                        Dim arrData As ArrayList = New VW_SalesOrderInterestFacade(User).Retrieve(crit)
                        Session("ARRDATA" + ViewState("sessionName")) = arrData
                        enumStatusPPHInterest = CType(Session("ENUMSTATUS" + ViewState("sessionName")), Dictionary(Of Integer, String))
                        dgListSOInterest.DataSource = arrData
                        dgListSOInterest.DataBind()
                    Else
                        MessageBox.Show("Berkas tidak dapat di prosess")
                    End If
                Catch ex As Exception
                    MessageBox.Show("Berkas tidak bisa diprosess")
                End Try

            Else
                MessageBox.Show("Format tidak dikenal, silahkan menggunakan template tersedia")
            End If
        End If
    End Sub

    Private Function parseExcelSO(ByVal f As HttpPostedFile, ByRef soList As List(Of String)) As Boolean
        Dim result As Boolean = False
        Try

            soList = New List(Of String)
            Using pck As New ExcelPackage(f.InputStream)
                Dim ws As ExcelWorksheet = pck.Workbook.Worksheets(1)
                Dim row As Integer = 2
                Dim strVal = String.Empty
                While row <= 100
                    If IsNothing(ws.Cells(row, 1)) OrElse IsNothing(ws.Cells(row, 1).Value) Then
                        Exit While
                    End If
                    strVal = ws.Cells(row, 1).Value.ToString().Trim()
                    If Not String.IsNullorEmpty(strVal) Then
                        soList.Add(strVal)
                    Else
                        Exit While
                    End If
                    row = row + 1
                End While
                If soList.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End Using
        Catch ex As Exception
            Return False


        End Try
    End Function
End Class
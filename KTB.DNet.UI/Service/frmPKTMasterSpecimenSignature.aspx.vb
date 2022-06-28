Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports KTB.DNet.Security
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System.Collections.Generic

Public Class frmPKTMasterSpecimenSignature
    Inherits System.Web.UI.Page

    Private sessHelper As SessionHelper = New SessionHelper
    Dim objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private m_bInputPrivilege As Boolean = False
    Dim crit As CriteriaComposite
    Dim targetDir As String = String.Empty

    Dim pktSpecimenFacade As PKTSpecimenFacade = New PKTSpecimenFacade(User)
    Dim dealerFacade As DealerFacade = New DealerFacade(User)
    Dim validExt As String = ".jpg,.png,.jpeg"
    Private maxFileSize As Integer = 2048000

#Region "Event Handler"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        targetDir = KTB.DNet.Lib.WebConfig.GetValue("SAN")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            txtDealerCode.Attributes.Add("readonly", True)
            ResetControl()
            RefreshGrid()
        End If
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs)
        Dim message As String = String.Empty
        If Not Validate(message) Then
            MessageBox.Show(message)
            Exit Sub
        End If

        Dim pdiSpecimen As PKTSpecimen = CType(sessHelper.GetSession("PKTSpecimen"), PKTSpecimen)
        Dim isExist As Boolean = IsDataExist(pdiSpecimen)

        PopulateData(pdiSpecimen)

        If Not UploadFile(pdiSpecimen) Then
            Exit Sub
        End If

        Dim result As Integer = 0
        If isExist Then
            result = pktSpecimenFacade.Update(pdiSpecimen)
        Else
            Dim objDomains As ArrayList = New ArrayList
            If icPaymentDateFrom.Value.Date = DateTime.Now.Date Then
                Dim oldData As PKTSpecimen = GetOldData()
                If Not IsNothing(oldData) Then
                    objDomains.Add(oldData)
                End If
            End If

            objDomains.Add(pdiSpecimen)
            result = pktSpecimenFacade.Insert(objDomains)
        End If

        If result > 0 Then
            MessageBox.Show("Simpan berhasil.")
            ResetControl()
            RefreshGrid()
        Else
            MessageBox.Show("Simpan gagal.")
        End If
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs)
        ResetControl()
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs)
        RefreshGrid()
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        If (txtDealerCode.Text = "") Or (icPaymentDateFrom.Value = Nothing) Then
            MessageBox.Show("Dealer, dan Tanggal berlaku Mulai harus diisi.")
            Exit Sub
        End If

        Dim ext As String = String.Empty
        Dim msgError As String = String.Empty
        Dim obj As PKTSpecimen
        If IsNothing(CType(sessHelper.GetSession("PKTSpecimen"), PKTSpecimen)) Then
            obj = New PKTSpecimen
        Else
            obj = CType(sessHelper.GetSession("PKTSpecimen"), PKTSpecimen)
        End If

        If fuUpload.PostedFile.ContentLength <> 0 Then
            ext = Path.GetExtension(fuUpload.PostedFile.FileName)
            If Not validExt.Contains(ext) Then
                msgError = String.Format("Upload File Gagal. Gunakan format file {0}", validExt)
            ElseIf fuUpload.PostedFile.ContentLength > maxFileSize Then
                msgError = "Upload File Gagal. Maksimum per file 2 MB"
            Else
                Dim fileByte As Byte() = UploadTemp(fuUpload.PostedFile.FileName, fuUpload.PostedFile.InputStream)
                imgPreview.ImageUrl = "data:image;base64," + Convert.ToBase64String(fileByte)
                obj.AttachmentData = fuUpload.PostedFile
                sessHelper.SetSession("PKTSpecimen", obj)
                MessageBox.Show("Upload berhasil.")
                btnSimpan.Enabled = True
                Return
            End If
        Else
            msgError = "Tidak ada data yang di upload."
        End If

        If Not String.IsNullorEmpty(msgError) Then
            imgPreview.ImageUrl = ""
            sessHelper.SetSession("PKTSpecimen", Nothing)
            MessageBox.Show(msgError)
        End If
    End Sub

    Protected Sub btnPreview_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub dgPKTSpecimen_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgPKTSpecimen.ItemCommand
        Dim hdFilename As HiddenField = CType(e.Item.FindControl("hdFilename"), HiddenField)
        Select Case e.CommandName
            Case "download"
                Response.Redirect("../download.aspx?file=" & hdFilename.Value)
        End Select
    End Sub

    Protected Sub dgPKTSpecimen_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgPKTSpecimen.PageIndexChanged
        RefreshGrid(e.NewPageIndex)
    End Sub

    Protected Sub dgPKTSpecimen_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgPKTSpecimen.SortCommand
        If e.SortExpression = ViewState.Item("SortCol") Then
            If ViewState.Item("SortDirection") = Sort.SortDirection.ASC Then
                ViewState.Item("SortDirection") = Sort.SortDirection.DESC
            Else
                ViewState.Item("SortDirection") = Sort.SortDirection.ASC
            End If
        End If
        ViewState.Item("SortCol") = e.SortExpression
        dgPKTSpecimen.SelectedIndex = -1
        dgPKTSpecimen.CurrentPageIndex = 0
        RefreshGrid(0)
    End Sub

    Protected Sub dgPKTSpecimen_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgPKTSpecimen.ItemDataBound
        Dim RowValue As PKTSpecimen = CType(e.Item.DataItem, PKTSpecimen)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgPKTSpecimen.PageSize * dgPKTSpecimen.CurrentPageIndex)
        End If
    End Sub
#End Region

#Region "Custom Method"
    Private Sub CheckPrivilege()
        m_bInputPrivilege = SecurityProvider.Authorize(Context.User, SR.PKTMasterSpecimen_Input_Privilage)

        If Not m_bInputPrivilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - Umum - PKT Master Specimen Signature")
        End If
    End Sub

    Private Sub RefreshGrid(Optional indexPage As Integer = 0)
        Dim totalRow As Integer


        crit = New CriteriaComposite(New Criteria(GetType(PKTSpecimen), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If Not IsNothing(objDealer.DealerGroup) Then
            'filter dealer group
            crit.opAnd(New Criteria(GetType(PKTSpecimen), "Dealer.DealerGroup", MatchType.Exact, objDealer.DealerGroup.ID))
        End If

        If txtDealerCode.Text <> "" Then
            crit.opAnd(New Criteria(GetType(PKTSpecimen), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Split("-")(0).Trim))
        End If

        If icPaymentDateFrom.Value.ToString <> "01/01/0001 0:00:00" Then
            Dim tglBerlaku As DateTime = New DateTime(icPaymentDateFrom.Value.Year, icPaymentDateFrom.Value.Month, icPaymentDateFrom.Value.Day, 0, 0, 0)
            crit.opAnd(New Criteria(GetType(PKTSpecimen), "ValidFrom", MatchType.GreaterOrEqual, tglBerlaku))
        End If

        If txtNama.Text <> "" Then
            crit.opAnd(New Criteria(GetType(PKTSpecimen), "Name", MatchType.Exact, txtNama.Text))
        End If

        If txtPosisi.Text <> "" Then
            crit.opAnd(New Criteria(GetType(PKTSpecimen), "Position", MatchType.Exact, txtPosisi.Text))
        End If

        If ddlBlok.SelectedIndex <> 0 Then
            crit.opAnd(New Criteria(GetType(PKTSpecimen), "Blok", MatchType.Exact, ddlBlok.SelectedValue))
        End If

        If ddlStatus.SelectedIndex <> 0 Then
            crit.opAnd(New Criteria(GetType(PKTSpecimen), "Status", MatchType.Exact, CType(ddlStatus.SelectedValue, Short)))
        End If

        Dim data As ArrayList = pktSpecimenFacade.RetrieveByCriteria(crit, indexPage + 1, dgPKTSpecimen.PageSize, totalRow, ViewState.Item("SortCol"), ViewState.Item("SortDirection"))

        If data.Count = 0 Then
            dgPKTSpecimen.CurrentPageIndex = 0
        Else
            dgPKTSpecimen.CurrentPageIndex = indexPage
        End If

        dgPKTSpecimen.DataSource = data
        dgPKTSpecimen.VirtualItemCount = totalRow
        dgPKTSpecimen.DataBind()
    End Sub

    Private Sub ResetControl()
        txtDealerCode.Text = ""
        txtNama.Text = ""
        txtPosisi.Text = ""
        icPaymentDateFrom.Value = Nothing
        ddlBlok.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0
        imgPreview.ImageUrl = ""
        sessHelper.SetSession("PKTSpecimen", Nothing)
        btnSimpan.Enabled = False
    End Sub

    Private Function Validate(ByRef message As String) As Boolean
        If txtDealerCode.Text = "" Then
            message = "Kode Dealer harus diisi."
        ElseIf txtNama.Text = "" Then
            message = "Nama harus diisi."
        ElseIf txtPosisi.Text = "" Then
            message = "Posisi harus diisi."
        ElseIf icPaymentDateFrom.Value.ToString = "01/01/0001 0:00:00" Then
            message = "Tanggal Berlaku harus diisi."
        ElseIf icPaymentDateFrom.Value < Date.Now.Date Then
            message = "Tanggal Berlaku minimal hari ini."
        ElseIf ddlBlok.SelectedIndex = 0 Then
            message = "Blok harus dipilih."
        ElseIf IsNothing(CType(sessHelper.GetSession("PKTSpecimen"), PKTSpecimen)) Then
            message = "Belum ada file yang diupload."
        End If
        Return String.IsNullorEmpty(message)
    End Function

    Private Function IsDataExist(ByRef pdiSpecimen As PKTSpecimen) As Boolean
        Dim isExist As Boolean = False
        Dim arrExist As ArrayList = New ArrayList
        crit = New CriteriaComposite(New Criteria(GetType(PKTSpecimen), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(PKTSpecimen), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Split("-")(0).Trim))
        crit.opAnd(New Criteria(GetType(PKTSpecimen), "ValidFrom", MatchType.Exact, icPaymentDateFrom.Value))
        crit.opAnd(New Criteria(GetType(PKTSpecimen), "Blok", MatchType.Exact, ddlBlok.SelectedValue))
        crit.opAnd(New Criteria(GetType(PKTSpecimen), "Status", MatchType.Exact, 1))
        arrExist = pktSpecimenFacade.Retrieve(crit)
        If arrExist.Count > 0 Then
            Dim obj As PKTSpecimen = CType(arrExist(0), PKTSpecimen)
            pdiSpecimen.ID = obj.ID
            pdiSpecimen.CreatedBy = obj.CreatedBy
            isExist = True
        End If

        Return isExist
    End Function

    Private Function GetOldData() As PKTSpecimen
        Dim pKTSpecimen As PKTSpecimen
        Dim arrExist As ArrayList = New ArrayList
        crit = New CriteriaComposite(New Criteria(GetType(PKTSpecimen), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(PKTSpecimen), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Split("-")(0).Trim))
        crit.opAnd(New Criteria(GetType(PKTSpecimen), "ValidFrom", MatchType.Lesser, DateTime.Now.Date))
        crit.opAnd(New Criteria(GetType(PKTSpecimen), "Blok", MatchType.Exact, ddlBlok.SelectedValue))
        crit.opAnd(New Criteria(GetType(PKTSpecimen), "Status", MatchType.Exact, 1))
        arrExist = pktSpecimenFacade.Retrieve(crit)
        If arrExist.Count > 0 Then
            pKTSpecimen = CType(arrExist(0), PKTSpecimen)
            pKTSpecimen.Status = 0
        Else
            pKTSpecimen = Nothing
        End If

        Return pKTSpecimen
    End Function

    Private Sub PopulateData(ByVal pktSpecimen As PKTSpecimen)
        pktSpecimen.Dealer = dealerFacade.Retrieve(txtDealerCode.Text.Split("-")(0).Trim)
        pktSpecimen.Name = txtNama.Text
        pktSpecimen.Position = txtPosisi.Text
        pktSpecimen.Blok = ddlBlok.SelectedValue
        pktSpecimen.ValidFrom = icPaymentDateFrom.Value
        pktSpecimen.Status = IIf(CDate(icPaymentDateFrom.Value) > DateTime.Now.Date, 0, 1)
    End Sub

    Private Function UploadFile(ByVal obj As PKTSpecimen) As Boolean
        Dim isValid As Boolean = True

        If Not obj.AttachmentData Is Nothing Then
            Dim ext As String = Path.GetExtension(obj.AttachmentData.FileName)
            Dim dirTarget As String = Guid.NewGuid.ToString() & ext
            obj.FileName = String.Format("PKT-Specimen\{0}\{1}{2}", obj.Dealer.DealerCode, Guid.NewGuid.ToString(), ext)

            isValid = CommitAttachment(obj)
        Else
            MessageBox.Show("Tidak ada data yang diupload. Mohon untuk melakukan upload terlebih dahulu.")
            isValid = False
        End If

        Return isValid
    End Function

    Private Function CommitAttachment(ByVal obj As PKTSpecimen) As Boolean
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
                    Dim fullpath As String = Path.Combine(targetDir, obj.FileName)
                    TargetFInfo = New FileInfo(fullpath)

                    If Not TargetFInfo.Directory.Exists Then
                        Directory.CreateDirectory(TargetFInfo.DirectoryName)
                    End If

                    obj.AttachmentData.SaveAs(fullpath)
                    obj.AttachmentData = Nothing
                    Return True
                End If
            End If
            imp.StopImpersonate()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return False
    End Function

    Private Function UploadTemp(ByVal dir As String, ByVal srcFile As Stream) As Byte()
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate
        Try
            Dim ext As String = Path.GetExtension(dir)
            Dim dirTarget As String = Guid.NewGuid.ToString() & ext
            Dim targetFile As String = Server.MapPath("~/DataTemp/" & dirTarget)

            imp = New SAPImpersonate(_user, _password, _webServer)

            If imp.Start() Then
                Dim objUpload As New UploadToWebServer
                objUpload.Upload(srcFile, targetFile)
            End If

            imp.StopImpersonate()

            Return File.ReadAllBytes(targetFile)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
    End Function
#End Region
End Class
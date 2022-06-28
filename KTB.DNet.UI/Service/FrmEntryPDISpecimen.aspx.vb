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

Public Class FrmEntryPDISpecimen
    Inherits System.Web.UI.Page

    Private sessHelper As SessionHelper = New SessionHelper
    Dim objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private m_bInputPrivilege As Boolean = False
    Dim crit As CriteriaComposite
    Dim targetDir As String = String.Empty

    Dim pdiSpecimenFacade As PDISpecimenFacade = New PDISpecimenFacade(User)
    Dim dealerFacade As DealerFacade = New DealerFacade(User)
    Dim validExt As String = New AppConfigFacade(User).Retrieve("PDIPKTSpecimenAttExt").Value
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
            'txtDealerCode.Attributes.Add("readonly", True)
            ResetControl()
            RefreshGrid()
        End If
    End Sub

    Protected Sub dgPDISpecimen_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgPDISpecimen.ItemCommand
        Dim hdFilename As HiddenField = CType(e.Item.FindControl("hdFilename"), HiddenField)
        Select Case e.CommandName
            Case "download"
                Response.Redirect("../download.aspx?file=" & hdFilename.Value)
        End Select
    End Sub

    Protected Sub dgPDISpecimen_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgPDISpecimen.PageIndexChanged
        RefreshGrid(e.NewPageIndex)
    End Sub

    Protected Sub dgPDISpecimen_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgPDISpecimen.SortCommand
        If e.SortExpression = ViewState.Item("SortCol") Then
            If ViewState.Item("SortDirection") = Sort.SortDirection.ASC Then
                ViewState.Item("SortDirection") = Sort.SortDirection.DESC
            Else
                ViewState.Item("SortDirection") = Sort.SortDirection.ASC
            End If
        End If
        ViewState.Item("SortCol") = e.SortExpression
        dgPDISpecimen.SelectedIndex = -1
        dgPDISpecimen.CurrentPageIndex = 0
        RefreshGrid(0)
    End Sub

    Protected Sub dgPDISpecimen_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgPDISpecimen.ItemDataBound
        Dim RowValue As PDISpecimen = CType(e.Item.DataItem, PDISpecimen)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgPDISpecimen.PageSize * dgPDISpecimen.CurrentPageIndex)
        End If
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs)
        RefreshGrid()
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs)
        ResetControl()
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs)
        Dim message As String = String.Empty
        If Not Validate(message) Then
            MessageBox.Show(message)
            Exit Sub
        End If

        Dim pdiSpecimen As PDISpecimen = CType(sessHelper.GetSession("PDISpecimen"), PDISpecimen)
        Dim isExist As Boolean = IsDataExist(pdiSpecimen)

        PopulateData(pdiSpecimen)

        If Not UploadFile(pdiSpecimen) Then
            Exit Sub
        End If

        Dim result As Integer = 0
        If isExist Then
            result = pdiSpecimenFacade.Update(pdiSpecimen)
        Else
            Dim objDomains As ArrayList = New ArrayList
            If ccTglBerlaku.Value.Date = DateTime.Now.Date Then
                Dim oldData As PDISpecimen = GetOldData()
                If Not IsNothing(oldData) Then
                    objDomains.Add(oldData)
                End If
            End If

            objDomains.Add(pdiSpecimen)
            result = pdiSpecimenFacade.Insert(objDomains)
        End If

        If result > 0 Then
            MessageBox.Show("Simpan berhasil.")
            ResetControl()
            RefreshGrid()
        Else
            MessageBox.Show("Simpan gagal.")
        End If
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Dim ext As String = String.Empty
        Dim msgError As String = String.Empty
        Dim obj As PDISpecimen
        If IsNothing(CType(sessHelper.GetSession("PDISpecimen"), PDISpecimen)) Then
            obj = New PDISpecimen
        Else
            obj = CType(sessHelper.GetSession("PDISpecimen"), PDISpecimen)
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
                sessHelper.SetSession("PDISpecimen", obj)
                MessageBox.Show("Upload berhasil.")
                Return
            End If
        Else
            msgError = "Tidak ada data yang di upload."
        End If

        If Not String.IsNullorEmpty(msgError) Then
            imgPreview.ImageUrl = ""
            sessHelper.SetSession("PDISpecimen", Nothing)
            MessageBox.Show(msgError)
        End If
    End Sub
#End Region

#Region "Custom Method"
    Private Sub CheckPrivilege()
        m_bInputPrivilege = SecurityProvider.Authorize(Context.User, SR.PDIMasterSpecimen_Input_Privilage)

        If Not m_bInputPrivilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - PDI - Master Specimen Signature")
        End If
    End Sub

    Private Sub RefreshGrid(Optional indexPage As Integer = 0)
        Dim totalRow As Integer

        crit = New CriteriaComposite(New Criteria(GetType(PDISpecimen), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtDealerCode.Text <> "" And objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            crit.opAnd(New Criteria(GetType(PDISpecimen), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))
        ElseIf Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            crit.opAnd(New Criteria(GetType(PDISpecimen), "Dealer.DealerGroup.ID", MatchType.Exact, objDealer.DealerGroup.ID))
            If txtDealerCode.Text <> "" Then
                crit.opAnd(New Criteria(GetType(PDISpecimen), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))
            End If
        End If

        If ccTglBerlaku.Value.ToString <> "01/01/0001 0:00:00" Then
            Dim tglBerlaku As DateTime = New DateTime(ccTglBerlaku.Value.Year, ccTglBerlaku.Value.Month, ccTglBerlaku.Value.Day, 0, 0, 0)
            crit.opAnd(New Criteria(GetType(PDISpecimen), "ValidFrom", MatchType.GreaterOrEqual, tglBerlaku))
        End If

        If txtNama.Text <> "" Then
            crit.opAnd(New Criteria(GetType(PDISpecimen), "Name", MatchType.Exact, txtNama.Text))
        End If

        If txtPos.Text <> "" Then
            crit.opAnd(New Criteria(GetType(PDISpecimen), "Position", MatchType.Exact, txtPos.Text))
        End If

        If ddlBlok.SelectedIndex <> 0 Then
            crit.opAnd(New Criteria(GetType(PDISpecimen), "Blok", MatchType.Exact, ddlBlok.SelectedValue))
        End If

        If ddlStatus.SelectedIndex <> 0 Then
            crit.opAnd(New Criteria(GetType(PDISpecimen), "Status", MatchType.Exact, CType(ddlStatus.SelectedValue, Short)))
        End If

        Dim data As ArrayList = pdiSpecimenFacade.RetrieveByCriteria(crit, indexPage + 1, dgPDISpecimen.PageSize, totalRow, ViewState.Item("SortCol"), ViewState.Item("SortDirection"))

        If data.Count = 0 Then
            dgPDISpecimen.CurrentPageIndex = 0
        Else
            dgPDISpecimen.CurrentPageIndex = indexPage
        End If

        dgPDISpecimen.DataSource = data
        dgPDISpecimen.VirtualItemCount = totalRow
        dgPDISpecimen.DataBind()
    End Sub

    Private Sub ResetControl()
        If Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            txtDealerCode.Text = objDealer.DealerCode
        Else
            txtDealerCode.Text = ""
        End If

        txtNama.Text = ""
        txtPos.Text = ""
        ccTglBerlaku.Value = Nothing
        ddlBlok.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0
        imgPreview.ImageUrl = ""
        sessHelper.SetSession("PDISpecimen", Nothing)
    End Sub

    Private Function Validate(ByRef message As String) As Boolean
        If txtDealerCode.Text = "" Then
            message = "Kode Dealer harus diisi."
        ElseIf txtNama.Text = "" Then
            message = "Nama harus diisi."
        ElseIf txtPos.Text = "" Then
            message = "Posisi harus diisi."
        ElseIf ccTglBerlaku.Value.ToString = "01/01/0001 0:00:00" Then
            message = "Tanggal Berlaku harus diisi."
        ElseIf ccTglBerlaku.Value < Date.Now.Date Then
            message = "Tanggal Berlaku minimal hari ini."
        ElseIf ddlBlok.SelectedIndex = 0 Then
            message = "Blok harus dipilih."
            'ElseIf ddlStatus.SelectedIndex = 0 Then
            '    message = "Status harus dipilih."
        ElseIf IsNothing(CType(sessHelper.GetSession("PDISpecimen"), PDISpecimen)) Then
            message = "Belum ada file yang diupload."
        End If

        If String.IsNullorEmpty(message) Then
            crit = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, txtDealerCode.Text))
            If Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                crit.opAnd(New Criteria(GetType(Dealer), "DealerGroup.ID", MatchType.Exact, objDealer.DealerGroup.ID))
            End If

            Dim dealers As ArrayList = dealerFacade.Retrieve(crit)
            If dealers.Count = 0 Then
                message = "Kode Dealer tidak ditemukan atau tidak dalam satu group dealer."
            End If
        End If

        Return String.IsNullorEmpty(message)
    End Function

    Private Function IsDataExist(ByRef pdiSpecimen As PDISpecimen) As Boolean
        Dim isExist As Boolean = False
        Dim arrExist As ArrayList = New ArrayList
        crit = New CriteriaComposite(New Criteria(GetType(PDISpecimen), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(PDISpecimen), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))
        crit.opAnd(New Criteria(GetType(PDISpecimen), "ValidFrom", MatchType.Exact, ccTglBerlaku.Value))
        crit.opAnd(New Criteria(GetType(PDISpecimen), "Blok", MatchType.Exact, ddlBlok.SelectedValue))
        crit.opAnd(New Criteria(GetType(PDISpecimen), "Status", MatchType.Exact, 1))
        arrExist = pdiSpecimenFacade.Retrieve(crit)
        If arrExist.Count > 0 Then
            Dim obj As PDISpecimen = CType(arrExist(0), PDISpecimen)
            pdiSpecimen.ID = obj.ID
            pdiSpecimen.CreatedBy = obj.CreatedBy
            isExist = True
        End If

        Return isExist
    End Function

    Private Function GetOldData() As PDISpecimen
        Dim pDISpecimen As PDISpecimen
        Dim arrExist As ArrayList = New ArrayList
        crit = New CriteriaComposite(New Criteria(GetType(PDISpecimen), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(PDISpecimen), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))
        crit.opAnd(New Criteria(GetType(PDISpecimen), "ValidFrom", MatchType.Lesser, DateTime.Now.Date))
        crit.opAnd(New Criteria(GetType(PDISpecimen), "Blok", MatchType.Exact, ddlBlok.SelectedValue))
        crit.opAnd(New Criteria(GetType(PDISpecimen), "Status", MatchType.Exact, 1))
        arrExist = pdiSpecimenFacade.Retrieve(crit)
        If arrExist.Count > 0 Then
            pDISpecimen = CType(arrExist(0), PDISpecimen)
            pDISpecimen.Status = 0
        Else
            pDISpecimen = Nothing
        End If

        Return pDISpecimen
    End Function

    Private Sub PopulateData(ByVal pdiSpecimen As PDISpecimen)
        pdiSpecimen.Dealer = dealerFacade.Retrieve(txtDealerCode.Text)
        pdiSpecimen.Name = txtNama.Text
        pdiSpecimen.Position = txtPos.Text
        pdiSpecimen.Blok = ddlBlok.SelectedValue
        pdiSpecimen.ValidFrom = ccTglBerlaku.Value
        pdiSpecimen.Status = IIf(CDate(ccTglBerlaku.Value) > DateTime.Now.Date, 0, 1)
    End Sub

    Private Function UploadFile(ByVal obj As PDISpecimen) As Boolean
        Dim isValid As Boolean = True

        If Not obj.AttachmentData Is Nothing Then
            Dim ext As String = Path.GetExtension(obj.AttachmentData.FileName)
            obj.FileName = String.Format("PDI-Specimen\{0}\{1}_{2}_{3}{4}", obj.Dealer.DealerCode, obj.Dealer.DealerName, obj.Name, obj.Position, ext)
            isValid = CommitAttachment(obj)
        Else
            MessageBox.Show("Tidak ada data yang diupload. Mohon untuk melakukan upload terlebih dahulu.")
            isValid = False
        End If

        Return isValid
    End Function

    Private Function CommitAttachment(ByVal obj As PDISpecimen) As Boolean
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
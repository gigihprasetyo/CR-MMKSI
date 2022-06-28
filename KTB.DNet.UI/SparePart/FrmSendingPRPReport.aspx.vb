Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.IO
Imports System.Web

Public Class FrmSendingPRPReport
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnUpload As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtPIC As System.Web.UI.WebControls.TextBox
    Protected WithEvents lbReceiver As System.Web.UI.WebControls.ListBox
    Protected WithEvents lbCC As System.Web.UI.WebControls.ListBox
    Protected WithEvents inpSendedFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents lblKodeOrganisasiValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaOrganisasiValue As System.Web.UI.WebControls.Label
    Protected WithEvents dtgReportPRP As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnUploadFile As System.Web.UI.WebControls.Button
    Protected WithEvents lblFileName As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private objUserInfo As UserInfo
    Private sesHelper As SessionHelper = New SessionHelper
    Private sHPRP As SessionHelper = New SessionHelper

#Region "Event Method"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objUserInfo = sHPRP.GetSession("LOGINUSERINFO")
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub dtgReportPRP_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgReportPRP.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgReportPRP.CurrentPageIndex * dtgReportPRP.PageSize)
            Dim data As PRPSenderInfo = CType(e.Item.DataItem, PRPSenderInfo)

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Select Case data.Status
                Case PRPSenderInfo.EnumSendStatus.Baru
                    lblStatus.Text = "Baru"
                Case PRPSenderInfo.EnumSendStatus.Sukses
                    lblStatus.Text = "Sukses"
                Case PRPSenderInfo.EnumSendStatus.Gagal
                    lblStatus.Text = "Gagal"
            End Select
        End If
    End Sub

    Private Sub dtgReportPRP_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgReportPRP.PageIndexChanged
        dtgReportPRP.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgReportPRP.CurrentPageIndex)
    End Sub

    Private Sub dtgReportPRP_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgReportPRP.SortCommand
        If CType(ViewState("vsSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("vsSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("vsSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("vsSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("vsSortColumn") = e.SortExpression
            ViewState("vsSortDirect") = Sort.SortDirection.ASC
        End If

        dtgReportPRP.SelectedIndex = -1
        dtgReportPRP.CurrentPageIndex = 0
        BindDataGrid(dtgReportPRP.CurrentPageIndex)
    End Sub

    Private Sub btnUpload_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.ServerClick
        Try
            If Not Page.IsValid Then
                Return
            End If
            If Not IsPageValid() Then
                Return
            End If
            If lblFileName.Text = "" OrElse lblFileName.Text = Nothing Then
                MessageBox.Show("File belum diupload. Silakan tekan tombol UPLOAD setelah BROWSE")
                Return
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return
        End Try

        Try

            Dim objDomain As PRPSenderInfo = InsertData()

            Try
                SendEmail(objDomain)
                SendSuccess(objDomain)
                ClearPage()
                dtgReportPRP.CurrentPageIndex = 0
                BindDataGrid(dtgReportPRP.CurrentPageIndex)
            Catch
                SendFailed(objDomain)
                Return
            End Try
        Catch ex As Exception
            MessageBox.Show("Pengiriman file gagal")
        End Try
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearPage()
        lblFileName.Text = ""
        sesHelper.RemoveSession("UploadFile")
    End Sub
#End Region

#Region "Custom Method"
    Private Sub InitiatePage()
        ClearPage()
        If Not IsNothing(objUserInfo) Then
            lblKodeOrganisasiValue.Text = objUserInfo.Dealer.DealerCode
            lblNamaOrganisasiValue.Text = objUserInfo.Dealer.DealerName & " / " & objUserInfo.Dealer.SearchTerm2
        End If
        BindReceiver()
        BindCC()
        ViewState("vsSortColumn") = "CreatedTime"
        ViewState("vsSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ViewReportSubmitPRP_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PARTSHOP REWARD PROGRAM - Pengiriman Laporan PRP")
        End If
    End Sub

    Private Sub ClearPage()
        txtPIC.Text = objUserInfo.UserName
        lbCC.SelectedIndex = -1
        lbReceiver.SelectedIndex = -1
    End Sub

    Private Sub BindReceiver()
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(PRPUserEmail), "UserName", Sort.SortDirection.ASC))
        Dim data As ArrayList = New PRPUserEmailFacade(User).Retrieve(CreateReceiverCriteria(), sortCol)

        For Each row As PRPUserEmail In data
            Dim li As ListItem = New ListItem(New System.Text.StringBuilder(row.UserName).Append("-").Append(row.Email).ToString, CStr(row.ID))
            lbReceiver.Items.Add(li)
        Next
    End Sub

    Private Sub BindCC()
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(PRPUserEmail), "UserName", Sort.SortDirection.ASC))
        Dim data As ArrayList = New PRPUserEmailFacade(User).Retrieve(CreateCcCriteria(), sortCol)

        For Each row As PRPUserEmail In data
            Dim li As ListItem = New ListItem(New System.Text.StringBuilder(row.UserName).Append("-").Append(row.Email).ToString, CStr(row.ID))
            lbCC.Items.Add(li)
        Next
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim objFacade As PRPSenderInfoFacade = New PRPSenderInfoFacade(User)

        Dim activeDealer As String = String.Format("{0:000000}", objUserInfo.Dealer.ID)

        Dim critComp As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPSenderInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critComp.opAnd(New Criteria(GetType(PRPSenderInfo), "CreatedBy", MatchType.StartsWith, activeDealer))

        Dim totalRow As Integer

        Dim objDomains As ArrayList
        Try
            objDomains = objFacade.RetrieveActiveList(critComp, indexPage + 1, dtgReportPRP.PageSize, totalRow, ViewState("vsSortColumn"), ViewState("vsSortDirect"))
        Catch
            objDomains = New ArrayList
        End Try

        dtgReportPRP.DataSource = objDomains
        dtgReportPRP.VirtualItemCount = totalRow
        dtgReportPRP.DataBind()
    End Sub

    Private Function CreateReceiverCriteria() As ICriteria
        Dim critComp As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPUserEmail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critComp.opAnd(New Criteria(GetType(PRPUserEmail), "Tipe", MatchType.Exact, "TO"))

        Return critComp
    End Function

    Private Function CreateCcCriteria() As ICriteria
        Dim critComp As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPUserEmail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critComp.opAnd(New Criteria(GetType(PRPUserEmail), "Tipe", MatchType.Exact, "CC"))

        Return critComp
    End Function

    Private Function IsPageValid() As Boolean
        If CountItemSelected(lbReceiver.Items) <= 0 Then
            Throw New Exception("Pilih penerima file")
            Return False
        End If
        If CanBeHacked(txtPIC.Text) Then
            Throw New Exception("< dan > bukan karakter valid")
            Return False
        End If
        If txtPIC.Text = String.Empty Then
            Return False
        End If
        If objUserInfo.Email = String.Empty Then
            Throw New Exception("Anda tidak mempunyai email yg valid untuk proses pengiriman laporan PRP")
            Return False
        End If
        Return True
    End Function

    Private Function CountItemSelected(ByVal liCollection As ListItemCollection) As Integer
        Dim count As Integer = 0
        For idx As Integer = 0 To liCollection.Count - 1
            Dim li As ListItem = liCollection(idx)
            If li.Selected Then
                count += 1
            End If
        Next
        Return count
    End Function

    Private Function InsertData() As PRPSenderInfo
        Dim objFacade As PRPSenderInfoFacade = New PRPSenderInfoFacade(User)
        Dim objSenderInfo As PRPSenderInfo = New PRPSenderInfo
        Dim objDomain As PRPSenderInfo = FillObjectFromForm()
        Try
            Dim nResult As Integer = objFacade.Insert(objDomain)
            If nResult <= -1 Then
                Throw New Exception(SR.SaveFail)
            End If
        Catch
            Throw New Exception(SR.SaveFail)
        End Try
        Return objDomain
    End Function

    Private Function FillObjectFromForm() As PRPSenderInfo
        Dim objDomain As PRPSenderInfo = New PRPSenderInfo
        'objDomain.Filename = System.IO.Path.GetFileName(inpSendedFile.PostedFile.FileName)
        objDomain.Filename = System.IO.Path.GetFileName(lblFileName.Text)
        objDomain.Description = New System.Text.StringBuilder(objUserInfo.Dealer.DealerCode).Append("-"). _
            Append(Now.ToString("MMyyyy")).Append(" Laporan PRP").ToString()
        objDomain.PIC = txtPIC.Text
        'objDomain.CreatedBy = objUserInfo.UserName
        objDomain.Status = PRPSenderInfo.EnumSendStatus.Baru
        For idx As Integer = 0 To lbReceiver.Items.Count - 1
            If lbReceiver.Items(idx).Selected Then
                Dim objUserEmail As PRPUserEmail = New PRPUserEmailFacade(User).Retrieve(CInt(lbReceiver.Items(idx).Value))
                Dim objDetail As PRPReceiverInfo = New PRPReceiverInfo
                objDetail.PRPUserEmail = objUserEmail
                objDetail.PRPSenderInfo = objDomain
                objDomain.PRPReceiverInfos.Add(objDetail)
            End If
        Next

        'Dev 45
        If (lbCC.SelectedIndex >= 0) Then
            For idx As Integer = 0 To lbCC.Items.Count - 1
                If lbCC.Items(idx).Selected Then
                    Dim objUserEmail As PRPUserEmail = New PRPUserEmailFacade(User).Retrieve(CInt(lbCC.Items(idx).Value))
                    Dim objDetail As PRPReceiverInfo = New PRPReceiverInfo
                    objDetail.PRPUserEmail = objUserEmail
                    objDetail.PRPSenderInfo = objDomain
                    objDomain.PRPReceiverInfos.Add(objDetail)
                End If
            Next
        End If
        'End Dev 45
        Return objDomain
    End Function

    Private Sub SendEmail(ByVal objDomain As PRPSenderInfo)
        Dim smtpServer As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim subject As String = "[MMKSI-DNet] Parts - Pengiriman Laporan PRP dari " + objUserInfo.Dealer.DealerCode
        Dim objEmail As DNetMail = New DNetMail(smtpServer)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim strBody As String = objDomain.Description
        'Dim attachFileName As System.IO.FileInfo = sesHelper.GetSession("UploadFile")
        Dim attachFileName As String = CType(sesHelper.GetSession("UploadFile"), String)

        Try

            'attachFileName = SaveFileToTemporary(inpSendedFile.PostedFile)

            Dim strEmailTo As String = objDomain.GetEmailList
            Dim strCCEmail As String = objDomain.GetCCEmailList

            Dim attachments As ArrayList = New ArrayList

            'attachments.Add(New Mail.MailAttachment(attachFileName.FullName))
            success = imp.Start()
            If success Then
                attachments.Add(New Mail.MailAttachment(CType(attachFileName, String)))
            End If
            objEmail.sendMail(strEmailTo, strCCEmail, objUserInfo.Email, subject, Mail.MailFormat.Text, strBody, attachments)
        Catch ex As Exception
            Throw New Exception("Proses Kirim Email Tidak Berhasil")
        Finally
            'DeleteTemporaryFile(attachFileName)
            ProcessPRPFile(True)
            sesHelper.RemoveSession("UploadFile")
            imp.StopImpersonate()
            imp = Nothing
        End Try
    End Sub

    Private Function CanBeHacked(ByVal text As String)
        Return text.IndexOf("<") >= 0 Or text.IndexOf(">") >= 0 Or text.IndexOf("'") >= 0
    End Function

#End Region

#Region "Processing Attachment"
    Private Function SaveFileToTemporary(ByVal postedFile As HttpPostedFile) As System.IO.FileInfo
        'Dim filename As String = System.IO.Path.GetFileName(postedFile.FileName)
        'Dim destination As System.IO.FileInfo = New System.IO.FileInfo(AttachmentFilename(filename, True))
        'While destination.Exists()
        'destination = New System.IO.FileInfo(AttachmentFilename(filename, False))
        'End While
        'postedFile.SaveAs(destination.FullName)
        'Return destination
    End Function

    Private Sub ProcessPRPFile(ByVal status As Boolean)
        Dim ext As String = ""
        Dim Rnd As Random = New Random
        Dim RndVal As String = Rnd.Next()
        Dim fileName As String = String.Empty
        fileName = DateTime.Now.ToString("ddMMyyyyHHmmss") & RndVal & "\" & Path.GetFileName(inpSendedFile.PostedFile.FileName)
        fileName = String.Format("{0}{1}{2}", fileName, "", ext)

        If fileName <> "" OrElse fileName <> Nothing Then
            'cek filesize first
            Dim maxFileSize As Integer = CDbl(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
            If inpSendedFile.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                Exit Sub
            Else
                Dim DestFile As String
                If status Then
                    DestFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("PRPDir") & "\" & lblFileName.Text   '-- Destination file
                Else
                    DestFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "DataTemp\PRP" & "\" & fileName   '-- Destination file
                End If
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Dim finfo As New FileInfo(DestFile)
                Try
                    success = imp.Start()
                    If success Then
                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        If sesHelper.GetSession("UploadFile") Is Nothing Then
                            inpSendedFile.PostedFile.SaveAs(DestFile)
                            sesHelper.SetSession("UploadFile", DestFile)
                            lblFileName.Text = fileName
                        Else
                            Dim old As String = CType(sesHelper.GetSession("UploadFile"), String)
                            If status Then
                                File.Copy(old, DestFile)
                                File.Delete(CType(sesHelper.GetSession("UploadFile"), String))
                                Directory.Delete(Path.GetDirectoryName(old))
                            Else
                                inpSendedFile.PostedFile.SaveAs(DestFile)
                                File.Delete(CType(sesHelper.GetSession("UploadFile"), String))
                                Directory.Delete(Path.GetDirectoryName(old))
                                sesHelper.SetSession("UploadFile", DestFile)
                                lblFileName.Text = fileName
                            End If
                        End If

                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    Throw ex
                    Exit Sub
                End Try
            End If
        End If
    End Sub

    Private Function AttachmentFilename(ByVal filename As String, ByVal original As Boolean) As String
        If original Then
            Return New System.Text.StringBuilder(Server.MapPath("../DataTemp/")).Append(filename).ToString
        End If
        Return New System.Text.StringBuilder(Server.MapPath("../DataTemp/")).Append(Format(Now, "yyMMddHHmmssff")).Append(filename).ToString
    End Function

    Private Sub DeleteTemporaryFile(ByVal file As System.IO.FileInfo)
        file.Delete()
    End Sub

    Private Sub SendSuccess(ByVal objDomain As PRPSenderInfo)
        objDomain.SuccessSend()
        Update(objDomain)
        MessageBox.Show("Pengiriman file sukses")
    End Sub

    Private Sub SendFailed(ByVal objDomain As PRPSenderInfo)
        objDomain.FailSend()
        Update(objDomain)
        MessageBox.Show("Pengiriman file gagal")
    End Sub

    Private Sub Update(ByVal objDomain As PRPSenderInfo)
        Dim objFacade As PRPSenderInfoFacade = New PRPSenderInfoFacade(User)
        Dim nResult = objFacade.Update(objDomain)
    End Sub

#End Region

    Private Sub btnUploadFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        If inpSendedFile.Value <> "" OrElse inpSendedFile.Value <> Nothing Then
            If sesHelper.GetSession("UploadFile") <> Nothing Then
                sesHelper.RemoveSession("UploadFile")
            End If
            ProcessPRPFile(False)
        Else
            MessageBox.Show("File masih kosong")
        End If
    End Sub

End Class

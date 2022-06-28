#Region ".NET Base Class Namespace Imports"

Imports System.IO

Imports System.Text

#End Region

#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade

Imports ICSharpCode.SharpZipLib.Core
Imports ICSharpCode.SharpZipLib.Zip
#End Region

Public Class frmPresentationList
    Inherits System.Web.UI.Page

#Region "variable"
    Private ReadOnly varUpload As String = "Presentation\" '  <add key="SAPFolder" value="\\172.17.31.21\ZDNet\Repository\BSI-Net\DNet\SAP\" />
    Private ReadOnly varSession As String = "sessfrmPresentationList"
    Private sesHelper As New SessionHelper
    Dim arlUserGroup As New ArrayList
    Dim IsKTB As Boolean
    Dim IsAllowToEdit As Boolean
    Dim IsAllowToRead As Boolean
#End Region

#Region "Private Function"
    Private Sub SaveCrite()
        Dim ObjArr As New ArrayList


        ObjArr.Add(txtTitle.Text)
        ObjArr.Add(txtDescription.Text)

        ObjArr.Add(ddlStatus.SelectedValue)

        ObjArr.Add(ViewState("SortCol").ToString())
        ObjArr.Add(CInt(ViewState("CurrentSortDirect")))
        ObjArr.Add(dtgPresentation.CurrentPageIndex)

        ObjArr.Add(chkTgl.Checked)
        ObjArr.Add(icPeriodeFrom.Value)
        ObjArr.Add(icPeriodeTo.Value)


        sesHelper.SetSession(varSession, ObjArr)
    End Sub
    Private Sub InitPage()
        If Not IsPostBack Then
            ViewState("SortCol") = "LastUpdateTime"
            ViewState("SortDirection") = Sort.SortDirection.DESC

            If Request.QueryString("Mode") = "" AndAlso Not IsNothing(sesHelper.GetSession(varSession)) Then
                Dim ObjArr As New ArrayList

                ObjArr = CType(sesHelper.GetSession(varSession), ArrayList)
                txtTitle.Text = ObjArr(0).ToString()
                txtDescription.Text = ObjArr(1).ToString()
                If IsKTB Then
                    ddlStatus.SelectedValue = ObjArr(2).ToString()
                End If
                ViewState("SortCol") = ObjArr(3).ToString()
                ViewState("SortDirection") = CType(CInt(ObjArr(4)), Sort.SortDirection)

                chkTgl.Checked = CBool(ObjArr(6))
                icPeriodeFrom.Value = CDate(ObjArr(7))
                icPeriodeTo.Value = CDate(ObjArr(8))
                CreateCriterias()
                dtgPresentation.CurrentPageIndex = CInt(ObjArr(5))
                sesHelper.RemoveSession(varSession)
                BindDataGridMember(CInt(ObjArr(5)) + 1)
            Else
                sesHelper.RemoveSession(varSession)
            End If
        End If


    End Sub

    Private Sub CheckPrivilege()
        Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        If (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            IsKTB = True
            ddlStatus.Visible = True
            If Not SecurityProvider.Authorize(Context.User, SR.PPT_Input_Edit_Privilege) Then
                IsAllowToEdit = False
                If Not SecurityProvider.Authorize(Context.User, SR.PPT_List_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=Marketing - Presentation")
                End If
                IsAllowToRead = True
            Else
                IsAllowToEdit = True
                IsAllowToRead = True



            End If

            dtgPresentation.Columns(5).Visible = True
            dtgPresentation.Columns(6).Visible = True
            dtgPresentation.Columns(7).Visible = True
            dtgPresentation.Columns(8).Visible = True
            dtgPresentation.Columns(9).Visible = True
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.PPT_List_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Marketing - Presentation")
            End If

            IsKTB = False
            IsAllowToEdit = False
            tdStatus.Visible = False
            ddlStatus.Visible = False

        End If
    End Sub

    Private Sub CreateCriterias()
        Dim val As String
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Presentation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtTitle.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(Presentation), "Title", MatchType.[Partial], txtTitle.Text.Trim()))
        End If

        If txtDescription.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(Presentation), "Description", MatchType.[Partial], txtDescription.Text.Trim()))
        End If

        If Not IsKTB Then
            Dim objUserInfo As UserInfo = sesHelper.GetSession("LOGINUSERINFO")
            Dim strSearch As String = String.Format("(SELECT a1.PresentationID FROM dbo.PresentationGroup a1 INNER JOIN dbo.UserGroupMember a2 ON a1.UserGroupID = a2.UserGroupID WHERE a1.RowStatus=0 AND a2.RowStatus=0 AND a2.userID = {0}) ", objUserInfo.ID)

            criterias.opAnd(New Criteria(GetType(Presentation), "ID", MatchType.InSet, strSearch))
            criterias.opAnd(New Criteria(GetType(Presentation), "Status", MatchType.Exact, 1))
        Else
            If ddlStatus.SelectedValue <> "-1" Then
                criterias.opAnd(New Criteria(GetType(Presentation), "Status", MatchType.Exact, ddlStatus.SelectedValue))
            End If
        End If

        If chkTgl.Checked Then
            Dim dtStart As DateTime = New DateTime(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month, icPeriodeFrom.Value.Day, 0, 0, 0)
            Dim dtEnd As DateTime = New DateTime(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month, icPeriodeTo.Value.Day, 23, 59, 0)
            criterias.opAnd(New Criteria(GetType(Presentation), "CreatedTime", MatchType.GreaterOrEqual, dtStart))
            criterias.opAnd(New Criteria(GetType(Presentation), "CreatedTime", MatchType.LesserOrEqual, dtEnd))
        End If

        sesHelper.SetSession("CRITERIASfrmPL", criterias)
    End Sub

    Private Sub BindDataGridMember(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then

            Dim criterias As CriteriaComposite = sesHelper.GetSession("CRITERIASfrmPL")
            'New ForumMemberFacade(User).RetrieveByCriteria(criterias, 1, dtgForumMember.PageSize, totalRow, sHelper.GetSession("SortCol"), sHelper.GetSession("SortDirection"))
            arlUserGroup = New PresentationFacade(User).RetrieveActiveList(indexPage, dtgPresentation.PageSize, totalRow, ViewState("SortCol"), ViewState("SortDirection"), criterias)
            dtgPresentation.DataSource = arlUserGroup
            dtgPresentation.VirtualItemCount = totalRow
            dtgPresentation.DataBind()
            sesHelper.SetSession("arlUserGroup", arlUserGroup)
            If arlUserGroup.Count > 0 Then
                dtgPresentation.Visible = True
                If Not IsKTB Then
                    dtgPresentation.Columns(1).Visible = False

                End If
                'btnDeleteUserGroup.Visible = True
            Else
                btnDeleteUserGroup.Visible = False
            End If
        End If
    End Sub

    Private Sub CommandDelete(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

        Try
            Dim ObjPresentation As New Presentation(CInt(e.Item.Cells(0).Text))

            Dim ObjPresentattionFa As PresentationFacade = New PresentationFacade(User)
            ObjPresentation.RowStatus = DBRowStatus.Deleted
            ObjPresentattionFa.Update(ObjPresentation)

            MessageBox.Show(SR.DeleteSucces)
            BindDataGridMember(0)
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
        End Try


    End Sub

    Private Sub CommandEdit(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

        Try
            Dim ObjPresentation As New Presentation(CInt(e.Item.Cells(0).Text))
            SaveCrite()
            Response.Redirect("frmPresentationManage.aspx?id=" & ObjPresentation.ID.ToString() & "&isSelf=0", False)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub CommandView(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim fileWSM As String = CType(e.Item.FindControl("lblFile"), Label).Text
        Dim lblUni As String = CType(e.Item.FindControl("lblUniq"), Label).Text
        fileWSM = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & varUpload & fileWSM & ".zip"

        Dim fileNew As String
        Dim DirNew As String = Guid.NewGuid().ToString()
        fileNew = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & varUpload & lblUni

        Dim _user As String
        _user = ConfigurationSettings.AppSettings.Get("User")
        Dim _password As String
        _password = ConfigurationSettings.AppSettings.Get("Password")
        Dim _webServer As String
        _webServer = ConfigurationSettings.AppSettings.Get("SAPServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim CFiles As Integer = 1

        Try
            If imp.Start() Then
                Dim sFinfo As FileInfo = New FileInfo(fileWSM)

                If Not sFinfo.Exists Then
                    imp.StopImpersonate()
                    imp = Nothing
                    Throw New Exception("File Log is not Exist")
                End If

                'ExtractZipFile(fileWSM, "", fileNew)

                CFiles = New DirectoryInfo(fileNew).GetFiles("*.PNG").Length

                imp.StopImpersonate()
                imp = Nothing

                If CFiles > 0 Then

                    Dim objH As Hashtable = New Hashtable(4)
                    objH.Add("id", lblUni)
                    objH.Add("number", CFiles.ToString())
                    lblUni = Guid.NewGuid().ToString()
                    objH.Add("key", lblUni)
                    Dim str = DateTime.Now.ToString("HHmmss")
                    objH.Add("token", str)
                    Session("id_ppt") = objH
                    Dim strPage As String = "frmPresentationSlider.aspx?key=" & lblUni & "&number=" & CFiles.ToString() & "&token=" & str
                    Dim strPopP As String = String.Format("<script language='javascript'>showPopUpPPT('{0}','',1024,760);</script>", strPage)
                    txtDownload.Value = strPage
                    'RegisterStartupScript("OpenWindow", strPopP)

                    'Response.Redirect("frmPresentationSlider.aspx?id=" & lblUni & "&number=" & CFiles.ToString(), False)
                Else
                    Throw New Exception(SR.FileNotFound("Image PNG"))
                End If
            Else
                Throw New Exception(SR.DownloadFail(fileWSM))
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub CommandDownload(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim file As String = CType(e.Item.FindControl("lblFile"), Label).Text


        file = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & varUpload & file & ".zip"
        Try
            Response.Redirect("../Download.aspx?file=" & file)
        Catch ex As Exception
            MessageBox.Show(SR.DownloadFail(file))
        End Try
    End Sub

    Public Sub ExtractZipFile(archiveFilenameIn As String, password As String, outFolder As String)
        Dim zf As ZipFile = Nothing
        Try
            Dim fs As FileStream = File.OpenRead(archiveFilenameIn)
            zf = New ZipFile(fs)
            If Not [String].IsNullOrEmpty(password) Then    ' AES encrypted entries are handled automatically
                zf.Password = password
            End If
            For Each zipEntry As ZipEntry In zf
                If Not zipEntry.IsFile Then     ' Ignore directories
                    Continue For
                End If
                Dim entryFileName As [String] = Path.GetFileName(zipEntry.Name)
                ' to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                ' Optionally match entrynames against a selection list here to skip as desired.
                ' The unpacked length is available in the zipEntry.Size property.

                Dim buffer As Byte() = New Byte(4095) {}    ' 4K is optimum
                Dim zipStream As Stream = zf.GetInputStream(zipEntry)

                ' Manipulate the output filename here as desired.
                Dim fullZipToPath As [String] = Path.Combine(outFolder, entryFileName)
                Dim directoryName As String = Path.GetDirectoryName(fullZipToPath)
                If directoryName.Length > 0 Then
                    Directory.CreateDirectory(directoryName)
                End If

                ' Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                ' of the file, but does not waste memory.
                ' The "Using" will close the stream even if an exception occurs.
                Using streamWriter As FileStream = File.Create(fullZipToPath)
                    StreamUtils.Copy(zipStream, streamWriter, buffer)
                End Using
            Next
        Finally
            If zf IsNot Nothing Then
                zf.IsStreamOwner = True     ' Makes close also shut the underlying stream
                ' Ensure we release resources
                zf.Close()
            End If
        End Try
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If 1 = 1 Then
        '    If Not IsPostBack Then
        '        Dim objH As Hashtable = New Hashtable(3)
        '        objH.Add("id", "faf82f4b-f369-4194-8a0f-3e2b66a31d23")
        '        objH.Add("number", "34")

        '        objH.Add("key", "77B7CBCD-7E4B-4E3C-AD75-418D6F49BAFD")
        '        Session("id_ppt") = objH
        '    End If

        '    ' Dim strPage As String = "frmPresentationSlider.aspx?key=" & lblUni & "&number=" & CFiles.ToString() & "&token=" & DateTime.Now.ToString("HHmmss")
        'End If
        CheckPrivilege()
        InitPage()

    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        CreateCriterias()
        BindDataGridMember(0)
    End Sub


    Private Sub dtgPresentation_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPresentation.PageIndexChanged
        dtgPresentation.CurrentPageIndex = e.NewPageIndex
        BindDataGridMember(e.NewPageIndex + 1)
    End Sub

    Private Sub dtgPresentation_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPresentation.SortCommand
        If e.SortExpression = ViewState("SortCol") Then
            If ViewState("SortDirection") = Sort.SortDirection.ASC Then
                ViewState("SortDirection") = Sort.SortDirection.DESC
            Else
                ViewState("SortDirection") = Sort.SortDirection.ASC
            End If
        End If
        ViewState("SortCol") = e.SortExpression
        dtgPresentation.SelectedIndex = -1
        'dtgPresentation.CurrentPageIndex = 0
        BindDataGridMember(dtgPresentation.CurrentPageIndex)
    End Sub

    Private Sub dtgPresentation_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPresentation.ItemCommand


        Select Case e.CommandName.ToLower()
            Case "Delete".ToLower()
                CommandDelete(e)
            Case "Edit".ToLower()
                CommandEdit(e)
            Case "Download".ToLower()
                CommandDownload(e)
            Case "View".ToLower()
                CommandView(e)

        End Select


    End Sub

    Private Sub dtgPresentation_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPresentation.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNO"), Label)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lblNo.Text = CType(e.Item.ItemIndex + 1 + (dtgPresentation.CurrentPageIndex * dtgPresentation.PageSize), String)

            Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim _lbtnDownload As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)
            Dim _lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim _lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

            _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")


            _lbtnDelete.Visible = IsKTB AndAlso IsAllowToEdit
            _lbtnDownload.Visible = IsKTB AndAlso IsAllowToEdit
            _lbtnEdit.Visible = IsKTB AndAlso IsAllowToEdit

            Dim ObjP As Presentation = e.Item.DataItem

            If IsKTB AndAlso IsAllowToEdit AndAlso Not IsNothing(_lblStatus) AndAlso Not IsNothing(e.Item.DataItem) Then
                _lblStatus.Text = IIf(ObjP.Status, "Aktif", "Non Aktif")
            End If


        End If
    End Sub

End Class
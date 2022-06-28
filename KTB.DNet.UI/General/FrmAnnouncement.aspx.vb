Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Imports System.IO


Public Class FrmAnnouncement
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtDescription As CKEditor.NET.CKEditorControl
    Protected WithEvents dgAnnouncement As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim sessHelp As KTB.DNet.Utility.SessionHelper = New KTB.DNet.Utility.SessionHelper

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.AdminAnnouncement_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Announcement")
        End If
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            InitiateAuthorization()
            'BindDropDown()
            ViewState("CurrentSortColumn") = "LastUpdateTime"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            BindDataGrid(0)
        End If

    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        txtDescription.Text = ""
        btnSimpan.Enabled = True
    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(AnnouncementDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(AnnouncementDoc), "SparePartIndicator", MatchType.Exact, 1))

        Dim arrList As ArrayList = New AnnouncementDocFacade(User).RetrieveActiveList(criterias, idxPage + 1, dgAnnouncement.PageSize, totalRow, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))

        dgAnnouncement.CurrentPageIndex = idxPage
        dgAnnouncement.DataSource = arrList
        dgAnnouncement.VirtualItemCount = totalRow
        dgAnnouncement.DataBind()


    End Sub

    Private Sub dgAnnouncement_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgAnnouncement.PageIndexChanged
        dgAnnouncement.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgAnnouncement.CurrentPageIndex)

    End Sub

    Private Sub dgAnnouncement_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgAnnouncement.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If
        dgAnnouncement.SelectedIndex = -1
        dgAnnouncement.CurrentPageIndex = 0
        BindDataGrid(dgAnnouncement.CurrentPageIndex)
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If txtDescription.Text = "" Then
            MessageBox.Show("Isi Data Dulu")
            Exit Sub
        End If

        Dim objUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        Dim objToInsert As AnnouncementDoc = New AnnouncementDoc

        objToInsert.Announcement = txtDescription.Text
        objToInsert.UserInfo = objUserInfo

        Dim result As Integer = New AnnouncementDocFacade(User).InsertTransaction(objToInsert)

        If result > 0 Then
            Dim objToUpdate As AnnouncementDoc = New AnnouncementDocFacade(User).Retrieve(result)
            Dim resultUpdate As Integer = New AnnouncementDocFacade(User).Update(objToInsert)

            WriteLastRecord()

            MessageBox.Show(SR.SaveSuccess)
            txtDescription.Text = ""
        Else
            MessageBox.Show(SR.SaveFail)
        End If

        BindDataGrid(dgAnnouncement.CurrentPageIndex)
    End Sub

    Private Sub WriteLastRecord()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(AnnouncementDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim totalrow As Integer
        Dim arrList As ArrayList = New AnnouncementDocFacade(User).RetrieveActiveList(criterias, 1, 10000, totalrow, "LastUpdateTime", Sort.SortDirection.DESC)
        Dim objToWrite As AnnouncementDoc = arrList(0)

        Dim success As Boolean = False
        Dim Connect As Boolean = False
        Dim sw As StreamWriter
        Dim filename = "Announcement.html"
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("AnnouncementPath") & "\" & filename
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        If (Connect = False) Then           
            Dim finfo As New FileInfo(DestFile)
            Try
                success = imp.Start()
                If success Then
                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    sw = New StreamWriter(DestFile)
                    Connect = True
                End If
            Catch ex As Exception
                Throw ex
                Exit Sub
            End Try
        End If

        If (success = True) Then

            sw.Write(objToWrite.Announcement)
            sw.Close()
            imp.StopImpersonate()
            imp = Nothing
        Else
            MessageBox.Show("Write To File Failed")
        End If

    End Sub

    Private Sub dgAnnouncement_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgAnnouncement.ItemCommand

        If e.CommandName = "View" Then
            Dim objToDisplay As AnnouncementDoc = New AnnouncementDocFacade(User).Retrieve(CInt(e.CommandArgument))
            txtDescription.Text = objToDisplay.Announcement
            btnSimpan.Enabled = False
        End If

    End Sub

    Private Sub dgAnnouncement_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgAnnouncement.ItemDataBound

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            lblNo.Text = (dgAnnouncement.CurrentPageIndex * dgAnnouncement.PageSize) + e.Item.ItemIndex + 1
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim success As Boolean = False
        Dim Connect As Boolean = False
        Dim sr As StreamReader
        Dim filename = "Announcement.html"
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("AnnouncementPath") & "\" & filename
        If (Connect = False) Then
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Dim finfo As New FileInfo(DestFile)
            Try
                success = imp.Start()
                If success Then
                    If finfo.Exists Then
                        finfo.Delete()
                        MessageBox.Show("File Deleted !")
                    Else
                        MessageBox.Show("File Not Exists !")
                    End If
                    Connect = True
                    imp.StopImpersonate()
                    imp = Nothing
                End If
            Catch ex As Exception
                Throw ex
                Exit Sub
            End Try
        End If
    End Sub
End Class

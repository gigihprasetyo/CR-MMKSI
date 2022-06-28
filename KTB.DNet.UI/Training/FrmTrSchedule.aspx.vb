#Region "Custom Namespace"
Imports KTB.DNet.BusinessFacade.Training
'Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmTrSchedule
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgSchedule As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtScheduleName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDesc As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Method"

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ScheduleYear"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Sub BindToYear()

        Dim strSQL As String = "select MIN(ID) from TrScheduleUpload where ScheduleYear = "
        strSQL = strSQL & "(select MIN(ScheduleYear) from TrScheduleUpload)) "
        strSQL = strSQL & "OR ID in "
        strSQL = strSQL & "(select MAX(ID) from TrScheduleUpload where ScheduleYear = "
        strSQL = strSQL & "(select MAX(ScheduleYear) from TrScheduleUpload)"

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrScheduleUpload), "RowStatus", MatchType.No, CType(DBRowStatus.Deleted, Short)))
        criterias.opAnd(New Criteria(GetType(TrScheduleUpload), "ID", MatchType.InSet, "(" & strSQL & ")"))

        Dim arlYear As ArrayList = New TrScheduleUploadFacade(User).Retrieve(criterias)
        If arlYear.Count = 2 Then
            Dim iStart As Integer = CType(arlYear(0), TrScheduleUpload).ScheduleYear
            Dim iEnd As Integer = CType(arlYear(1), TrScheduleUpload).ScheduleYear
            For i As Integer = iStart To iEnd
                ddlYear.Items.Insert(0, New ListItem(iStart.ToString, iStart))
                iStart = iStart + 1
            Next
        End If
        'For Each item As ListItem In LookUp.ArraylistYear(True, 10, 5, DateTime.Now.Year)
        '    ddlYear.Items.Insert(0, New ListItem(item.Text, item.Value))
        'Next
        ddlYear.Items.Insert(0, New ListItem("Pilih Tahun", "0"))
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then

            'dtgSchedule.DataSource = New TrScheduleUploadFacade(User).RetrieveActiveList(indexPage + 1, dtgSchedule.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            '  CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            'dtgSchedule.VirtualItemCount = totalRow

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrScheduleUpload), "RowStatus", MatchType.No, CType(DBRowStatus.Deleted, Short)))
            If ddlYear.SelectedIndex <> 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrScheduleUpload), "ScheduleYear", MatchType.Exact, CType(ddlYear.SelectedValue, Integer)))
            End If
            If txtScheduleName.Text.Trim <> "" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrScheduleUpload), "Name", MatchType.[Partial], txtScheduleName.Text.Trim))
            End If
            If txtDesc.Text.Trim <> "" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrScheduleUpload), "Description", MatchType.[Partial], txtDesc.Text.Trim))
            End If

            dtgSchedule.DataSource() = New TrScheduleUploadFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgSchedule.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgSchedule.VirtualItemCount = totalRow
            dtgSchedule.DataBind()
        End If
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            BindToYear()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.TrainingDownloadJadwalPrivilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Training - Download Jadwal")
        End If
    End Sub

    Private Sub dtgSchedule_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSchedule.PageIndexChanged
        dtgSchedule.SelectedIndex = -1
        dtgSchedule.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgSchedule.CurrentPageIndex)
    End Sub

    Private Sub dtgSchedule_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgSchedule.ItemCommand
        If (e.CommandName = "Download") Then
            Dim _lblDownload As Label = e.Item.FindControl("lblDownload")

            'Dim fileInfo As New fileInfo(Server.MapPath("") & "\..\" & _lblDownload.Text)
            Dim fileInfo As New fileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & _lblDownload.Text)
            Dim fileExist As Boolean = False
            fileExist = CheckFileExist(fileInfo)
            If (fileExist) Then
                Try
                    Response.Redirect("../Download.aspx?file=" & _lblDownload.Text)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(_lblDownload.Text))
                End Try
            Else
                MessageBox.Show(SR.FileNotFound(fileInfo.Name))
            End If
        End If
    End Sub

    Private Function CheckFileExist(ByVal fileinfo As FileInfo) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Try
            success = imp.Start()
            If success Then
                Return fileinfo.Exists
            End If
        Catch ex As Exception
            Return False
        Finally
            imp.StopImpersonate()
            imp = Nothing
        End Try

    End Function

    Private Sub dtgSchedule_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSchedule.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            If Not e.Item.DataItem Is Nothing Then
                e.Item.DataItem.GetType().ToString()
                Dim RowValue As TrScheduleUpload = CType(e.Item.DataItem, TrScheduleUpload)

                If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then

                    Dim str As String() = RowValue.FilePath.Split("\")
                    Dim _linkButton As LinkButton = e.Item.FindControl("lbtnDownload")
                    _linkButton.Text = str(str.Length - 1) 'RowValue.FilePath
                    '_linkButton.Text = RowValue.FilePath
                    Dim _lblDownload As Label = e.Item.FindControl("lblDownload")
                    _lblDownload.Text = RowValue.FilePath

                    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                        CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgSchedule.CurrentPageIndex * dtgSchedule.PageSize)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub dtgSchedule_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSchedule.SortCommand
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

        dtgSchedule.SelectedIndex = -1
        dtgSchedule.CurrentPageIndex = 0
        BindDataGrid(dtgSchedule.CurrentPageIndex)
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgSchedule.CurrentPageIndex = 0
        BindDataGrid(dtgSchedule.CurrentPageIndex)
    End Sub

#End Region

    
End Class
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports KTB.DNet.Security

Public Class FrmManualDocList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgManualDoc As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lnkBaru As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Internal Enum"
    Public Enum TipeUser
        All = 0
        KTB = 1
        Dealer = 2
    End Enum
#End Region

#Region "Deklarasi"
    Dim sHelper As New SessionHelper
    Dim criterias As CriteriaComposite
#End Region

#Region "Custom Method"
    Private Sub BindToGrid(ByVal indexpage As Integer)
        Dim totalRow As Integer
        If indexpage >= 0 Then

            criterias = New CriteriaComposite(New Criteria(GetType(ManualDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If viewstate("DealerTitle") = 0 Then
                'only dealer
                Dim tipe As Integer = TipeUser.All
                criterias.opAnd(New Criteria(GetType(ManualDoc), "Type", MatchType.Exact, tipe), "(", True)
                tipe = TipeUser.Dealer
                criterias.opOr(New Criteria(GetType(ManualDoc), "Type", MatchType.Exact, tipe), ")", False)
            End If

            If txtName.Text.Trim.Length > 0 And txtName.Text.Trim.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(ManualDoc), "Name", MatchType.[Partial], txtName.Text.Trim))
            End If

            If txtDescription.Text.Trim.Length <> 0 And txtDescription.Text.Trim.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(ManualDoc), "Description", MatchType.[Partial], txtDescription.Text.Trim))
            End If

            'If txtName.Text.Trim.Length > 0 And txtDescription.Text.Trim.Length > 0 Then
            '    criterias.opAnd(New Criteria(GetType(ManualDoc), "Name", MatchType.[Partial], txtName.Text.Trim), "(", True)
            '    criterias.opOr(New Criteria(GetType(ManualDoc), "Description", MatchType.[Partial], txtDescription.Text.Trim), ")", False)
            'End If


            Dim arlList As ArrayList = New ManualDocFacade(User).RetrieveActiveList(criterias, indexpage + 1, dtgManualDoc.PageSize, totalRow, viewstate("SortColSM"), viewstate("SortDirectionSM"))
            dtgManualDoc.VirtualItemCount = totalRow
            dtgManualDoc.DataSource = arlList
            If indexpage = 0 Then
                dtgManualDoc.CurrentPageIndex = 0
            End If

            dtgManualDoc.DataBind()
        End If
    End Sub
#End Region

    Private Sub InitiateAuthorization()
        If Not (SecurityProvider.Authorize(context.User, SR.ManualDocMaintain_Privilege) Or SecurityProvider.Authorize(context.User, SR.General_manual_doc_privilege)) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=BULETIN - Manual Doc")
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            InitiateAuthorization()
            Viewstate("SortColSM") = "Sequence"
            viewstate("SortDirectionSM") = Sort.SortDirection.ASC
            Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
            If objUserInfo.Dealer.Title <> 1 Then
                lnkBaru.Visible = False
                viewstate.Add("DealerTitle", 0)
            Else
                lnkBaru.Visible = True
                viewstate.Add("DealerTitle", 1)
            End If
            BindToGrid(0)
        End If
    End Sub

    Private Sub lnkBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkBaru.Click
        Response.Redirect("FrmManualDoc.aspx")
    End Sub

    Private Sub dtgManualDoc_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgManualDoc.PageIndexChanged
        dtgManualDoc.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgManualDoc.CurrentPageIndex)
    End Sub

    Private Sub dtgManualDoc_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgManualDoc.SortCommand
        If e.SortExpression = viewstate("SortColSM") Then
            If viewstate("SortDirectionSM") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirectionSM", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirectionSM", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortColSM", e.SortExpression)
        BindToGrid(dtgManualDoc.CurrentPageIndex)
    End Sub

    Private Sub dtgManualDoc_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgManualDoc.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (dtgManualDoc.CurrentPageIndex * dtgManualDoc.PageSize) + e.Item.ItemIndex + 1

            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
            If objUserInfo.Dealer.Title <> 1 Then
                'bukan ktb
                lbtnEdit.Visible = False
                lnkBaru.Visible = False
            Else
                'ktb
                lbtnEdit.Visible = True
                lnkBaru.Visible = True
            End If

            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim lbtnDownload As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)
            Dim objManDoc As ManualDoc = New ManualDocFacade(User).Retrieve(CInt(lblID.Text))

            'get the dealer dan userinfo
            If objManDoc.LastUpdateBy.Trim.Length > 6 Then
                Try
                    Dim dealerID As Integer = CInt(objManDoc.LastUpdateBy.Substring(0, 6))
                    Dim objdealer As Dealer = New DealerFacade(User).Retrieve(dealerID)
                    Dim username As String = objManDoc.LastUpdateBy.Substring(6)
                    Dim objUserInfo2 As UserInfo = New UserInfoFacade(User).Retrieve(username, objdealer.DealerCode)
                    Dim strBuilder As String = "Download Counter: " & objManDoc.DownloadCounter & vbCrLf & _
                                    "Download Person: " & objUserInfo2.Dealer.DealerCode & "-" & objUserInfo2.UserName & vbCrLf & _
                                    "Download Time: " & objManDoc.LastUpdateTime
                    lbtnDownload.ToolTip = strBuilder
                Catch ex As Exception

                End Try
            Else
                lbtnDownload.ToolTip = ""
            End If

            Dim lbtnUpdStatus As Label = CType(e.Item.FindControl("lbtnUpdStatus"), Label)
            Dim currDay As Date
            currDay = Date.Today.AddMonths(-3)
            If objManDoc.LastUpdateTime > currDay Then
                lbtnUpdStatus.Visible = True
            Else
                lbtnUpdStatus.Visible = False
            End If

            'format the description
            Dim lblDeskripsi As Label = CType(e.Item.FindControl("lblDeskripsi"), Label)
            Dim lblTemp As String = lblDeskripsi.Text.Replace(vbCrLf, "<br>")
            lblDeskripsi.Text = lblTemp
        End If
    End Sub

    Private Sub dtgManualDoc_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgManualDoc.ItemCommand
        If e.CommandName = "Download" Then
            Dim path As String = e.CommandArgument
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim objManDoc As ManualDoc = New ManualDocFacade(User).Retrieve(CInt(lblid.Text))
            objManDoc.DownloadCounter += 1
            Dim iResult As Integer = New ManualDocFacade(User).Update(objManDoc)
            BindToGrid(dtgManualDoc.CurrentPageIndex)
            Response.Redirect("../Download.aspx?file=" & path)
        ElseIf e.CommandName = "Edit" Then
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            sHelper.SetSession("ViewModeMD", "Edit")
            sHelper.SetSession("EditIDMD", lblID.Text)
            Response.Redirect("FrmManualDoc.aspx")
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        '-- Search Documents
        dtgManualDoc.CurrentPageIndex = 0
        BindToGrid(dtgManualDoc.CurrentPageIndex) '-- Bind page-1
    End Sub

End Class

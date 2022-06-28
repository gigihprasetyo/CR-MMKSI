Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement


Public Class PopUpGroupDealerSelection
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lboxGroup As System.Web.UI.WebControls.ListBox
    Protected WithEvents txtGroupCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtGroup As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgDealerGroupSelection As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Protected countChk As Integer = 0
    Private objDealer As Dealer
    Dim criterias As CriteriaComposite
    Private sHelper As SessionHelper = New SessionHelper
    Dim objDealerGroup As DealerGroup

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            sHelper.SetSession("SortColPopUp", "DealerGroupCode")
            sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.ASC)
            BindSearch(0)
            ClearData()
        End If
    End Sub

    Private Sub ClearData()
        txtGroup.Text = String.Empty
        txtGroupCode.Text = String.Empty
    End Sub

    Public Sub BindSearch(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            CreateCriteria()
            dtgDealerGroupSelection.DataSource = New DealerGroupFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgDealerGroupSelection.PageSize, totalRow, sHelper.GetSession("SortColPopUp"), sHelper.GetSession("SortDirectionPopUp"))
            dtgDealerGroupSelection.DataBind()
        End If

        If dtgDealerGroupSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub
    Public Sub CreateCriteria()
        criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'Note : using coma(",") to separate
        If txtGroupCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(DealerGroup), "DealerGroupCode", MatchType.InSet, "(" & txtGroupCode.Text & ")"))
        End If

        If txtGroup.Text <> "" Then
            Dim str As String = "('"


            If txtGroup.Text <> "" Then
                For Each item As String In txtGroup.Text.Split(",")
                    str += item & "','"
                Next
                str = str.Trim(",")

            End If
            str = str.Substring(0, str.Length - 2)
            str += ")"

            criterias.opAnd(New Criteria(GetType(DealerGroup), "GroupName", MatchType.InSet, str))

        End If

      End Sub


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch(0)
    End Sub

    Private Sub dtgDealerGroupSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealerGroupSelection.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim lblGroup As Label = CType(e.Item.FindControl("lblGroup"), Label)
            Dim lblGroupCode As Label = CType(e.Item.FindControl("lblGroupCode"), Label)

            objDealerGroup = e.Item.DataItem
            lblGroupCode.Text = objDealerGroup.DealerGroupCode
            lblGroup.Text = objDealerGroup.GroupName
        End If
    End Sub

    Private Sub dtgDealerGroupSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDealerGroupSelection.SortCommand
        If e.SortExpression = sHelper.GetSession("SortColPopUp") Then
            If sHelper.GetSession("SortDirectionPopUp") = Sort.SortDirection.ASC Then
                sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.DESC)
            Else
                sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.ASC)
            End If
        End If
        sHelper.SetSession("SortColPopUp", e.SortExpression)
        dtgDealerGroupSelection.SelectedIndex = -1
        dtgDealerGroupSelection.CurrentPageIndex = 0
        BindSearch(0)
    End Sub

    Private Sub dtgDealerGroupSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDealerGroupSelection.PageIndexChanged
        dtgDealerGroupSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch(dtgDealerGroupSelection.CurrentPageIndex)
    End Sub
End Class

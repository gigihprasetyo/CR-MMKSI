Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement


Public Class PopUpReacallCategorySelection
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgDealerSelection As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtRecallRegNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDEscription As System.Web.UI.WebControls.TextBox
  
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

 

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            'Added by Ery for sorting
            ViewState.Add("SortColDealer", "RecallRegNo")
            Viewstate.Add("SortDirDealer", Sort.SortDirection.ASC)


        End If
    End Sub
     
    Public Sub BindSearch()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
         

        If Not txtRecallRegNo.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "RecallRegNo", MatchType.[Partial], txtRecallRegNo.Text))
        End If

        If Not txtDEscription.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "Description", MatchType.[Partial], txtDEscription.Text))
        End If
      
        dtgDealerSelection.DataSource = New RecallCategoryFacade(User).RetrieveActiveList(criterias, ViewState("SortColDealer"), ViewState("SortDirDealer"))
        dtgDealerSelection.DataBind()
    End Sub


    Private Sub dtgDealerSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealerSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            'Dim RowValue As Dealer = CType(e.Item.DataItem, Dealer)
            'If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
            '    Dim lblGroup As Label = CType(e.Item.FindControl("lblGroup"), Label)
            '    If Not IsNothing(RowValue.DealerGroup) Then
            '        lblGroup.Text = RowValue.DealerGroup.GroupName
            '    End If
            '    Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
            '    If Not IsNothing(RowValue.City) Then
            '        lblCity.Text = RowValue.City.CityName
            '    End If
            'End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch()

        If dtgDealerSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgDealerSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDealerSelection.SortCommand
        If e.SortExpression = viewstate("SortColDealer") Then
            If viewstate("SortDirDealer") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirDealer", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirDealer", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortColDealer", e.SortExpression)
        BindSearch()
    End Sub

    Private Sub dtgDealerSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDealerSelection.PageIndexChanged
        dtgDealerSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch()
    End Sub
End Class

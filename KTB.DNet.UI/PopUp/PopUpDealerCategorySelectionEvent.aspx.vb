Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade

Public Class PopUpDealerCategorySelectionEvent
    Inherits System.Web.UI.Page

    Protected countChk As Integer = 0
    Private objDealer As Dealer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            'Added by Ery for sorting
            ViewState.Add("SortColDealer", "Dealer.DealerCode")
            ViewState.Add("SortDirDealer", Sort.SortDirection.ASC)

            objDealer = Session("DEALER")
            If Not objDealer.DealerGroup Is Nothing OrElse objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                BindDdlGroup()
            End If

            ClearData()
        End If
    End Sub

    Private Sub ClearData()
        Me.txtSearch1.Text = String.Empty
        Me.txtSearch2.Text = String.Empty
        Me.txtDealerName.Text = String.Empty
        Me.ddlCategory.SelectedIndex = 0
    End Sub

    Private Sub BindDdlGroup()
        objDealer = Session("DEALER")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, "MMC"))
        ddlCategory.DataSource = New CategoryFacade(User).Retrieve(criterias)
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataBind()
    End Sub

    Private Function GetSelectedItem(ByVal listboxGroup As ListBox) As String
        Dim _strGroup As String = String.Empty
        If Not listboxGroup.SelectedIndex = -1 Then
            For Each item As ListItem In listboxGroup.Items
                If item.Selected Then
                    If _strGroup = String.Empty Then
                        _strGroup = item.Value
                    Else
                        _strGroup = _strGroup & "," & item.Value
                    End If
                End If
            Next
        End If

        Return _strGroup
    End Function

    Public Sub BindSearch()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerCategory), "Dealer.Status", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        'If ViewState("Group") Is Nothing Then
        '    objDealer = Session("DEALER")
        '    ' 08-Nov-2007   Deddy H     Handle bug 1383, bila dari send message bs send ke semua dealer
        '    If IsNothing(Request.QueryString("All")) Then
        '        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        '        End If
        '    End If
        'End If


        If Not txtDealerName.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerCategory), "Dealer.DealerGroup", MatchType.[Partial], txtDealerName.Text))
        End If

        If Not txtSearch1.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerCategory), "Dealer.SearchTerm1", MatchType.[Partial], txtSearch1.Text))
        End If
        If Not txtSearch2.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerCategory), "Dealer.SearchTerm2", MatchType.[Partial], txtSearch2.Text))
        End If

        If Not IsNothing(Request.QueryString("DEALERONLY")) Then

            criterias.opAnd(New Criteria(GetType(DealerCategory), "Dealer.ID", MatchType.No, "1"))
            criterias.opAnd(New Criteria(GetType(DealerCategory), "Dealer.Title", MatchType.Exact, "0"))

        End If

        'If ddlCategory.SelectedIndex <> 0 Then
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerCategory), "Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
        'End If
        'dtgDealerSelection.DataSource = New DealerCategoryFacade(User).Retrieve(criterias)
        dtgDealerSelection.DataSource = New DealerCategoryFacade(User).RetrieveActiveList(criterias, ViewState("SortColDealer"), ViewState("SortDirDealer"))
        dtgDealerSelection.DataBind()
    End Sub


    Private Sub dtgDealerSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealerSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        'If Not e.Item.DataItem Is Nothing Then
        '    e.Item.DataItem.GetType().ToString()
        '    Dim RowValue As DealerCategory = CType(e.Item.DataItem, DealerCategory)
        '    If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
        '        Dim lblGroup As Label = CType(e.Item.FindControl("lblGroup"), Label)
        '        If Not IsNothing(RowValue.Dealer.DealerGroup) Then
        '            lblGroup.Text = RowValue.Dealer.DealerGroup.GroupName
        '        End If
        '        Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
        '        If Not IsNothing(RowValue.Dealer.City) Then
        '            lblCity.Text = RowValue.Dealer.City.CityName
        '        End If
        '    End If
        'End If
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
        If e.SortExpression = ViewState("SortColDealer") Then
            If ViewState("SortDirDealer") = Sort.SortDirection.ASC Then
                ViewState.Add("SortDirDealer", Sort.SortDirection.DESC)
            Else
                ViewState.Add("SortDirDealer", Sort.SortDirection.ASC)
            End If
        End If
        ViewState.Add("SortColDealer", e.SortExpression)
        BindSearch()
    End Sub

    Private Sub dtgDealerSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDealerSelection.PageIndexChanged
        dtgDealerSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch()
    End Sub
End Class

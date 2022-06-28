#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility

#End Region

Public Class PopUpDealerBranchSelectionOne
    Inherits System.Web.UI.Page


#Region " custom Declaration "

    Protected countChk As Integer = 0
    Private objDealer As Dealer
#End Region


#Region " Custom Method "

    Private Sub ClearData()
        Me.txtSearch1.Text = String.Empty
        Me.txtSearch2.Text = String.Empty
        Me.txtBranchName.Text = String.Empty
    End Sub

#End Region

#Region " Event Hendler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then
            'Added by Ery for sorting
            ViewState.Add("SortColDealer", "DealerCode")
            ViewState.Add("SortDirDealer", Sort.SortDirection.ASC)

            objDealer = Session("DEALER")

            If Not IsNothing(Request.QueryString("m")) Then
                txtDealerCode.Text = objDealer.DealerCode
            End If

            If Not objDealer.DealerGroup Is Nothing OrElse objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                BindDdlGroup()
            End If
            ClearData()
        End If
    End Sub


    Private Sub BindDdlGroup()
        'Dim nTotRow As Integer = 0
        'Dim nPageNumber As Integer = 1
        'Dim nPageSize As Integer = 50
        objDealer = Session("DEALER")
        If Not objDealer.DealerGroup Is Nothing Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                criterias.opAnd(New Criteria(GetType(DealerGroup), "ID", MatchType.Exact, CInt(objDealer.DealerGroup.ID)))
            End If

            'ddlGroup.DataSource = New DealerGroupFacade(User).RetrieveActiveList(criterias, nPageNumber, nPageSize, nTotRow, "GroupName", Sort.SortDirection.ASC)
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(DealerGroup), "GroupName", Sort.SortDirection.ASC))

            'ddlGroup.Items.Insert(0, New ListItem("Pilih Group", ""))
        Else
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(DealerGroup), "GroupName", Sort.SortDirection.ASC))

        End If

    End Sub

    Private Function GetSelectedItem(ByVal listboxGroup As ListBox) As String
        Dim _strGroup As String = String.Empty
        For Each item As ListItem In listboxGroup.Items
            If item.Selected Then
                If _strGroup = String.Empty Then
                    _strGroup = item.Value
                Else
                    _strGroup = _strGroup & "," & item.Value
                End If
            End If
        Next
        Return _strGroup
    End Function

    Public Sub BindSearch()
        Dim funcD As New DealerFacade(User)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Status", MatchType.Exact, CInt(EnumDealerStatus.DealerStatus.Aktive).ToString()))

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "OrganizationBranchType", MatchType.InSet, "(2,3)"))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then

            If IsNothing(Request.QueryString("m")) Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ParentDealerID", MatchType.InSet, DealerFacade.GenerateDealerIDSelection(objDealer, User)))

                If txtDealerCode.Text <> String.Empty Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ParentDealerID", MatchType.InSet, funcD.GenerateDealerIDSelection(txtDealerCode.Text)))
                End If

            Else
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ParentDealerID", MatchType.Exact, objDealer.ID))
            End If

        Else
            If txtDealerCode.Text <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ParentDealerID", MatchType.InSet, funcD.GenerateDealerIDSelection(txtDealerCode.Text)))
            End If

        End If

        If Not txtBranchName.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "Name", MatchType.[Partial], txtBranchName.Text))
        End If


        If Not txtDealerCode.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ParentDealerID", MatchType.InSet, funcD.GenerateDealerIDSelection(txtDealerCode.Text)))
        End If


        If Not txtBranchCode.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.[Partial], txtBranchCode.Text))
        End If

        If Not txtSearch1.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SearchTerm1", MatchType.[Partial], txtSearch1.Text))
        End If
        If Not txtSearch2.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SearchTerm2", MatchType.[Partial], txtSearch2.Text))
        End If

        Dim sortColl As SortCollection = New SortCollection

        sortColl.Add(New Sort(GetType(Dealer), ViewState("SortColDealer").ToString(), [Enum].Parse(GetType(Sort.SortDirection), ViewState("SortDirDealer").ToString())))

        dtgDealerSelection.DataSource = funcD.RetrieveActiveList(criterias, sortColl)
        dtgDealerSelection.DataBind()
    End Sub

    Private Sub dtgDealerSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealerSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As Dealer = CType(e.Item.DataItem, Dealer)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)


            End If
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

#End Region

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
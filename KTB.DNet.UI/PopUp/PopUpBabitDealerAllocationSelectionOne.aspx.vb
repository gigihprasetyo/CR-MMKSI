#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility

#End Region

Public Class PopUpBabitDealerAllocationSelectionOne
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgDealerSelection As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtDealerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSearch1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlGroup As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtSearch2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lboxGroup As System.Web.UI.WebControls.ListBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " custom Declaration "

    Protected countChk As Integer = 0
    Private objDealer As Dealer
#End Region

#Region " Custom Method "

    Private Sub ClearData()
        Me.txtSearch1.Text = String.Empty
        Me.txtSearch2.Text = String.Empty
        Me.txtDealerName.Text = String.Empty
    End Sub

#End Region

#Region " Event Hendler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then
            'Added by Ery for sorting
            ViewState.Add("SortColDealer", "DealerCode")
            ViewState.Add("SortDirDealer", Sort.SortDirection.ASC)

            Dim dealerID As Integer = Request.QueryString("dealerID")
            If dealerID > 0 Then
                objDealer = New DealerFacade(User).Retrieve(dealerID)
            Else
                objDealer = Session("DEALER")
            End If
            If Not objDealer.DealerGroup Is Nothing OrElse objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                BindDdlGroup()
            End If
            ClearData()
            btnSearch_Click(Nothing, Nothing)
        End If
    End Sub


    Private Sub BindDdlGroup()
        'Dim nTotRow As Integer = 0
        'Dim nPageNumber As Integer = 1
        'Dim nPageSize As Integer = 50
        If Not objDealer.DealerGroup Is Nothing Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                criterias.opAnd(New Criteria(GetType(DealerGroup), "ID", MatchType.Exact, CInt(objDealer.DealerGroup.ID)))
            End If

            'ddlGroup.DataSource = New DealerGroupFacade(User).RetrieveActiveList(criterias, nPageNumber, nPageSize, nTotRow, "GroupName", Sort.SortDirection.ASC)
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(DealerGroup), "GroupName", Sort.SortDirection.ASC))

            lboxGroup.DataSource = New DealerGroupFacade(User).Retrieve(criterias, sortColl)

            lboxGroup.DataTextField = "GroupName"
            lboxGroup.DataValueField = "ID"
            lboxGroup.DataBind()
            'ddlGroup.Items.Insert(0, New ListItem("Pilih Group", ""))
        Else
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(DealerGroup), "GroupName", Sort.SortDirection.ASC))
            Try
                lboxGroup.DataSource = New DealerGroupFacade(User).Retrieve(criterias, sortColl)
            Catch ex As Exception

            End Try
            lboxGroup.DataTextField = "GroupName"
            lboxGroup.DataValueField = "ID"
            lboxGroup.DataBind()
        End If

        If lboxGroup.Items.Count > 0 Then
            lboxGroup.SelectedIndex = 0
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
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Status", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If Not txtDealerName.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerName", MatchType.[Partial], txtDealerName.Text))
        End If
        'If Not ddlGroup.SelectedValue = "" Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerGroup.ID", MatchType.Exact, ddlGroup.SelectedValue))
        'End If
        If lboxGroup.SelectedIndex <> -1 Then
            Dim SelectedGroup As String = GetSelectedItem(lboxGroup)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerGroup.ID", MatchType.InSet, "(" & SelectedGroup & ")"))
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "PKStatus", MatchType.InSet, "(" & SelectedStatus & ")"))
        End If
        If Not txtSearch1.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SearchTerm1", MatchType.[Partial], txtSearch1.Text))
        End If
        If Not txtSearch2.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SearchTerm2", MatchType.[Partial], txtSearch2.Text))
        End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "OrganizationBranchType", MatchType.Exact, 1))

        Dim sortColl As SortCollection = New SortCollection

        sortColl.Add(New Sort(GetType(Dealer), ViewState("SortColDealer").ToString(), [Enum].Parse(GetType(Sort.SortDirection), ViewState("SortDirDealer").ToString())))
        'If (Not IsNothing("DealerCode")) Then
        '    sortColl.Add(New Sort(GetType(Dealer), "DealerCode", Sort.SortDirection.ASC))
        'Else
        '    sortColl = Nothing
        'End If

        dtgDealerSelection.DataSource = New DealerFacade(User).RetrieveActiveList(criterias, sortColl)
        'dtgDealerSelection.DataSource = New DealerFacade(User).RetrieveActiveList(criterias, viewstate("SortColDealer"), viewstate("SortDirDealer"))
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
                Dim lblGroup As Label = CType(e.Item.FindControl("lblGroup"), Label)
                If Not IsNothing(RowValue.DealerGroup) Then
                    lblGroup.Text = RowValue.DealerGroup.GroupName
                End If
                Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
                If Not IsNothing(RowValue.City) Then
                    lblCity.Text = RowValue.City.CityName
                End If
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

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility

'Untuk meretrieve data semua dealer tanpa melihat status aktif/non aktif tambahkan:'
'Buat session dgn nama "showAllDealer" dengan isi apapun)'

Public Class PopUpSelectingDealer
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgDealerSelection As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtSearch2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlGroup As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtSearch1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblQueryString As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private sHDealer As SessionHelper = New SessionHelper

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            sHDealer.SetSession("SortCol", "DealerCode")
            sHDealer.SetSession("SortDirection", Sort.SortDirection.ASC)
            BindDdlGroup()
            ClearData()
            CheckQueryStr()
        End If
    End Sub

    ' 18 Jul 2007   add by Deddy H
    ' Set query string untuk keperluan multiple return value
    Private Sub CheckQueryStr()
        If (Request.QueryString("Multi") <> "") Then
            lblQueryString.Value = Request.QueryString("Multi")
        Else
            lblQueryString.Value = "false"
        End If

    End Sub
    Private Sub BindDdlGroup()
        Dim nTotRow As Integer = 0
        Dim nPageNumber As Integer = 1
        Dim nPageSize As Integer = 50
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ddlGroup.DataSource = New DealerGroupFacade(User).RetrieveActiveList(criterias, nPageNumber, nPageSize, nTotRow, "GroupName", Sort.SortDirection.ASC)
        ddlGroup.DataTextField = "GroupName"
        ddlGroup.DataValueField = "ID"
        ddlGroup.DataBind()
        ddlGroup.Items.Insert(0, New ListItem("Pilih Group", ""))
    End Sub

    Private Sub ClearData()
        Me.txtSearch1.Text = String.Empty
        Me.txtSearch2.Text = String.Empty
        Me.txtDealerName.Text = String.Empty
        Me.ddlGroup.SelectedIndex = 0
        SearchClicked()
    End Sub

    Public Sub BindSearch()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If IsNothing(sHDealer.GetSession("showAllDealer")) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Status", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        End If

        If Not txtDealerName.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerName", MatchType.[Partial], txtDealerName.Text))
        End If
        If Not ddlGroup.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerGroup.ID", MatchType.Exact, ddlGroup.SelectedValue))
        End If
        If Not txtSearch1.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SearchTerm1", MatchType.[Partial], txtSearch1.Text))
        End If
        If Not txtSearch2.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SearchTerm2", MatchType.[Partial], txtSearch2.Text))
        End If
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "OrganizationBranchType", MatchType.InSet, "(1,5,6)"))

        Dim sortColl As SortCollection = New SortCollection
        'If ((IsNothing(sHDealer.GetSession("SortCol"))) And (IsNothing(sHDealer.GetSession("SortDirection")))) Then
        '    sortColl.Add(New Sort(GetType(Dealer), "DealerCode", Sort.SortDirection.ASC))
        'Else
            sortColl.Add(New Sort(GetType(Dealer), sHDealer.GetSession("SortCol"), sHDealer.GetSession("SortDirection")))
        'End If

        dtgDealerSelection.DataSource = New DealerFacade(User).RetrieveActiveList(criterias, sortColl)
        dtgDealerSelection.DataBind()
    End Sub

    Private Sub dtgDealerSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealerSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectDealer"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As Dealer = CType(e.Item.DataItem, Dealer)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then

                Dim lblGroup As Label = CType(e.Item.FindControl("lblGroup"), Label)
                If Not IsNothing(RowValue.DealerGroup) Then
                    lblGroup.Text = RowValue.DealerGroup.GroupName
                End If

                Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
                If Not IsNothing(RowValue.City) Then
                    lblCity.Text = RowValue.City.CityName
                End If

                '09-Aug-2007    Deddy H     Penambahan column area value
                Dim lblArea As Label = CType(e.Item.FindControl("lblArea"), Label)
                If Not IsNothing(RowValue.Area2) Then
                    lblArea.Text = RowValue.Area2.AreaCode
                End If

            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        SearchClicked()
    End Sub

    Private Sub SearchClicked()
        BindSearch()

        If dtgDealerSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgDealerSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDealerSelection.SortCommand
        If e.SortExpression = sHDealer.GetSession("SortCol") Then
            If sHDealer.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sHDealer.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sHDealer.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sHDealer.SetSession("SortCol", e.SortExpression)
        dtgDealerSelection.SelectedIndex = -1
        dtgDealerSelection.CurrentPageIndex = 0
        BindSearch()
    End Sub
End Class

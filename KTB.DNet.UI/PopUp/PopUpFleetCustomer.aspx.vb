#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade

#End Region

Public Class PopUpFleetCustomer
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton

    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgFleetCustHdr As System.Web.UI.WebControls.DataGrid
    Protected WithEvents hdnCustType As System.Web.UI.WebControls.HiddenField

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variables Declaration"
    Private sessHelper As New SessionHelper
    Private objDealer As New Dealer
#End Region

#Region "Custom Method"
    Private Sub ClearData()
        Me.txtCode.Text = String.Empty
        Me.txtName.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        dtgFleetCustHdr.DataSource = New FleetCustomerHeaderFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, _
            dtgFleetCustHdr.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgFleetCustHdr.VirtualItemCount = totalRow
        dtgFleetCustHdr.DataBind()
    End Sub

    Public Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(FleetCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If hdnCustType.Value.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(FleetCustomerHeader), "FleetCustomerType", MatchType.Exact, hdnCustType.Value.Trim))
        End If
        If txtCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(FleetCustomerHeader), "FleetCode", MatchType.Exact, txtCode.Text))
        End If
        If txtName.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(FleetCustomerHeader), "FleetCustomerName", MatchType.[Partial], txtName.Text))
        End If

        Dim strSql As String = "Select distinct a.ID from FleetCustomerHeader a Left Join FleetCustomerDetail b on a.ID=b.FleetCustomerHeaderID and b.RowStatus=0 "
        strSql += " and b.DealerID = " & objDealer.ID & " Where a.RowStatus=0 "
        criterias.opAnd(New Criteria(GetType(FleetCustomerHeader), "ID", MatchType.InSet, "(" & strSql & ")"))

        Return criterias
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)

        If Not Page.IsPostBack Then
            hdnCustType.value = Request.QueryString("custType")
            InitiatePage()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
        If dtgFleetCustHdr.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub dtgPosisi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgFleetCustHdr.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectFleet"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim objFleetCustomerHeader As FleetCustomerHeader = New FleetCustomerHeaderFacade(User).Retrieve(CInt(lblID.Text))
            Dim lblDomainBisnis As Label = CType(e.Item.FindControl("lblDomainBisnis"), Label)
            Try
                lblDomainBisnis.Text = objFleetCustomerHeader.BusinessSectorDetail.BusinessSectorHeader.BusinessSectorName
            Catch
            End Try

            Dim lblBisnisSektor As Label = CType(e.Item.FindControl("lblBisnisSektor"), Label)
            Try
                lblBisnisSektor.Text = objFleetCustomerHeader.BusinessSectorDetail.BusinessDomain
            Catch
            End Try
        End If

    End Sub

    Private Sub dtgPosisi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgFleetCustHdr.PageIndexChanged
        dtgFleetCustHdr.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgFleetCustHdr.CurrentPageIndex)
    End Sub

    Private Sub dtgPosisi_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgFleetCustHdr.SortCommand
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
        dtgFleetCustHdr.CurrentPageIndex = 0
        BindDataGrid(dtgFleetCustHdr.CurrentPageIndex)
    End Sub

End Class

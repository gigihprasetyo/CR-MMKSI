
#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.AfterSales
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmSparepartSalesman
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgSparepartSalesman As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtNama As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeSalesman As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtPosition As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private sessHelp As SessionHelper = New SessionHelper

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Declaration"
    Private m_bFormPrivilege As Boolean = False
    Private ListSalesmanHeader As ArrayList
#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
        End If

    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        SearchSalesmanHeaderByCriteria()
    End Sub

    Private Sub dtgSparepartSalesman_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgSparepartSalesman.ItemCommand

    End Sub

    Private Sub dtgSparepartSalesman_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSparepartSalesman.ItemDataBound
        Dim RowValue As SalesmanHeader = CType(e.Item.DataItem, SalesmanHeader)
        Dim actLb As LinkButton
        Dim nactLb As LinkButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgSparepartSalesman.CurrentPageIndex * dtgSparepartSalesman.PageSize)
        End If

    End Sub

    Private Sub dtgSparepartSalesman_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSparepartSalesman.SortCommand
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

        dtgSparepartSalesman.SelectedIndex = -1
        dtgSparepartSalesman.CurrentPageIndex = 0
        BindDataGrid(dtgSparepartSalesman.CurrentPageIndex)
        ClearData()
    End Sub


    Private Sub dtgSparepartSalesman_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSparepartSalesman.PageIndexChanged
        dtgSparepartSalesman.SelectedIndex = -1
        dtgSparepartSalesman.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgSparepartSalesman.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "Custom"

    Private Sub SetControlPrivilege()

    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AfterSales_Sparepart_Salesman_View_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.AfterSales_Sparepart_Salesman_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Master - Sparepart Salesman")
        End If

    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "SalesmanCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        Dim objUserInfo As UserInfo = sessHelp.GetSession("LOGINUSERINFO")
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            txtKodeDealer.Attributes.Add("readonly", "readonly")
        Else
            txtKodeDealer.ReadOnly = False

        End If
    End Sub

    Private Sub ClearData()
        txtKode.Text = String.Empty
        txtNama.Text = String.Empty

        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "Status", MatchType.Exact, "2")) 'Active

            If Me.txtKodeDealer.Text.Trim <> "" Then
                Dim sCodes As String = Me.txtKodeDealer.Text.Trim()
                sCodes = "'" & sCodes.Replace(";", "','") & "'"
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.InSet, "(" & sCodes & ")"))
            End If

            If txtNama.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesmanHeader), "Name", MatchType.[Partial], txtNama.Text.Trim))
            End If
            If txtPosition.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesmanHeader), "JobPosition.Description", MatchType.[Partial], txtPosition.Text.Trim))
            End If
            If txtKode.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesmanHeader), "ID", MatchType.Exact, txtKode.Text.Trim))
            End If
            If txtKodeSalesman.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesmanHeader), "SalesmanCode", MatchType.[Partial], txtKodeSalesman.Text.Trim))
            End If

            ListSalesmanHeader = New SalesmanHeaderFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
                    dtgSparepartSalesman.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgSparepartSalesman.DataSource = ListSalesmanHeader
            dtgSparepartSalesman.VirtualItemCount = totRow
            dtgSparepartSalesman.DataBind()
        End If
    End Sub

    Private Sub ViewModel(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsSalesmanHeader", objSalesmanHeader)
        If IsNothing(objSalesmanHeader) Then
            txtNama.Text = ""
            txtKode.Text = ""
            txtKodeSalesman.Text = ""
        Else
            txtNama.Text = objSalesmanHeader.Name
            txtKode.Text = objSalesmanHeader.ID
            txtKodeSalesman.Text = objSalesmanHeader.SalesmanCode
        End If

    End Sub

    Private Sub SearchSalesmanHeaderByCriteria()
        dtgSparepartSalesman.CurrentPageIndex = 0
        BindDataGrid(dtgSparepartSalesman.CurrentPageIndex)
    End Sub
#End Region

End Class
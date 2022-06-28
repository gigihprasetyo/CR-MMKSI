
#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNET.BusinessFacade.AfterSales
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmServiceMechanic
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgServiceMechanic As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtNama As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeMekanik As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPosisi As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
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
    Private ListDataSiswa As ArrayList
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
        SearchDataSiswaByCriteria()
    End Sub

    Private Sub dtgServiceMechanic_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgServiceMechanic.ItemCommand

    End Sub

    Private Sub dtgServiceMechanic_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgServiceMechanic.ItemDataBound
        Dim RowValue As V_DataSiswa = CType(e.Item.DataItem, V_DataSiswa)
        Dim actLb As LinkButton
        Dim nactLb As LinkButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgServiceMechanic.CurrentPageIndex * dtgServiceMechanic.PageSize)
        End If

    End Sub

    Private Sub dtgServiceMechanic_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgServiceMechanic.SortCommand
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

        dtgServiceMechanic.SelectedIndex = -1
        dtgServiceMechanic.CurrentPageIndex = 0
        BindDataGrid(dtgServiceMechanic.CurrentPageIndex)
        ClearData()
    End Sub


    Private Sub dtgServiceMechanic_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgServiceMechanic.PageIndexChanged
        dtgServiceMechanic.SelectedIndex = -1
        dtgServiceMechanic.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgServiceMechanic.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "Custom"

    Private Sub SetControlPrivilege()

    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AfterSales_Service_Mechanic_View_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.AfterSales_Service_Mechanic_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Master - Daftar Pegawai After Sales")
        End If

    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "DealerCode"
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
        txtKodeMekanik.Text = String.Empty
        txtNama.Text = String.Empty
        txtPosisi.Text = String.Empty
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(V_DataSiswa), "Status", MatchType.Exact, "Aktif"))

            'gak jadi
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_DataSiswa), "JobPosition", MatchType.Exact, "MKN"))

            If Me.txtKodeDealer.Text.Trim <> "" Then
                Dim sCodes As String = Me.txtKodeDealer.Text.Trim()
                sCodes = "'" & sCodes.Replace(";", "','") & "'"
                criterias.opAnd(New Criteria(GetType(V_DataSiswa), "DealerCode", MatchType.InSet, "(" & sCodes & ")"))
            End If

            If txtNama.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_DataSiswa), "Name", MatchType.[Partial], txtNama.Text.Trim))
            End If
            If txtKodeMekanik.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_DataSiswa), "ID", MatchType.Exact, txtKodeMekanik.Text.Trim))
            End If
            If txtPosisi.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_DataSiswa), "JobPosition", MatchType.Partial, txtPosisi.Text.Trim))
            End If

            ListDataSiswa = New V_DataSiswaFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
                    dtgServiceMechanic.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgServiceMechanic.DataSource = ListDataSiswa
            dtgServiceMechanic.VirtualItemCount = totRow
            dtgServiceMechanic.DataBind()
        End If
    End Sub

    Private Sub ViewModel(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objDataSiswa As V_DataSiswa = New V_DataSiswaFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsDataSiswa", objDataSiswa)
        If IsNothing(objDataSiswa) Then
            txtNama.Text = ""
            txtKodeMekanik.Text = ""
            txtPosisi.Text = ""
        Else
            txtNama.Text = objDataSiswa.Name
            txtKodeMekanik.Text = objDataSiswa.ID
            txtPosisi.Text = objDataSiswa.JobPosition
        End If

    End Sub

    Private Sub SearchDataSiswaByCriteria()
        dtgServiceMechanic.CurrentPageIndex = 0
        BindDataGrid(dtgServiceMechanic.CurrentPageIndex)
    End Sub
#End Region

End Class
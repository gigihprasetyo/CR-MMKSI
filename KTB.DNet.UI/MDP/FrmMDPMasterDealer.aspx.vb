#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.MDP
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports System.Linq

#End Region


Public Class FrmMDPMasterDealer
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Varibles"
    Private arlMDPMasterDealer As ArrayList
    Private _sessHelper As SessionHelper = New SessionHelper
    Private sessCriterias As String = "FrmMDPMasterDealer.sessCriterias"
#End Region

#Region "Custom Methods"

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.MDP_Master_Dealer_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MDPMasterDealer")
        End If
    End Sub

    Private Sub Initialization()
        ViewState.Add("SortColumn", "DealerCode")
        ViewState.Add("SortDirection", Sort.SortDirection.ASC)
        BindStatus()
    End Sub

    Private Sub BindStatus()
        ddlStatus.Items.Clear()
        ddlStatus.Items.Add(New ListItem("Silahkan Pilih", -1))
        ddlStatus.Items.Add(New ListItem("Aktif", 1))
        ddlStatus.Items.Add(New ListItem("Tidak Aktif", 0))
        ddlStatus.SelectedValue = -1
    End Sub

    Private Sub ClearData()
        Dim objUser As UserInfo = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser.Dealer.Title = "1" Then
            txtKodeDealer.Text() = String.Empty
        End If
        txtNamaDealer.Text() = String.Empty
        dtgMain.DataSource = Nothing
        dtgMain.DataBind()
        btnCari.Enabled = True
        BindStatus()
    End Sub
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckUserPrivilege()
        If Not IsPostBack Then
            Initialization()
            Dim objUser As UserInfo = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            If objUser.Dealer.Title <> "1" Then
                txtKodeDealer.Text = objUser.Dealer.DealerCode
                txtKodeDealer.Enabled = False
                lblSearchDealer.Visible = False
            End If
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        BindSearchGrid()
    End Sub

    Private Sub BindSearchGrid()
        ReadData()   '-- Read all data matching criteria
        BindPage(0)  '-- Bind page-1
    End Sub

    Private Sub ReadData()
        '-- Read all data selected
        dtgMain.Visible = True
        Dim objDealer As New Dealer
        '-- Search criteria:
        '-- Row status
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPMasterDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtKodeDealer.Text <> "" Then
            Dim kodeDealer As String = txtKodeDealer.Text.Replace(";", "','")
            criterias.opAnd(New Criteria(GetType(MDPMasterDealer), "Dealer.DealerCode", MatchType.InSet, "('" & kodeDealer & "')"))
        End If

        If ddlStatus.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(MDPMasterDealer), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MDPMasterDealer), "Dealer.DealerCode", Sort.SortDirection.ASC))

        '-- Retrieve color list
        Dim arrMDPMasterDealerList As ArrayList = New MDPMasterDealerFacade(User).Retrieve(criterias, sortColl)

        '-- Store MDP Master Dealer List into session
        _sessHelper.SetSession("sesMDPMasterDealerList", arrMDPMasterDealerList)

        If arrMDPMasterDealerList.Count < 1 Then
            MessageBox.Show(SR.DataNotFound("Data"))
        End If

    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        '-- Bind page-i

        '-- Read MDPMasterDealerList from session
        Dim MDPMasterDealerList As ArrayList = CType(_sessHelper.GetSession("sesMDPMasterDealerList"), ArrayList)

        If Not IsNothing(MDPMasterDealerList) AndAlso MDPMasterDealerList.Count <> 0 Then
            '-- Then paging
            Dim PagedList As ArrayList = ArrayListPager.DoPage(MDPMasterDealerList, pageIndex, dtgMain.PageSize)
            dtgMain.DataSource = PagedList
            dtgMain.VirtualItemCount = MDPMasterDealerList.Count
            dtgMain.CurrentPageIndex = pageIndex
            dtgMain.DataBind()
        Else
            '-- Display datagrid header only
            dtgMain.DataSource = New ArrayList
            dtgMain.VirtualItemCount = 0
            dtgMain.CurrentPageIndex = 0
            dtgMain.DataBind()
        End If

    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim objMasterDealer As MDPMasterDealer = CType(e.Item.DataItem, MDPMasterDealer)

            Dim lblNo As Label = e.Item.FindControl("lblNo")
            lblNo.Text = (dtgMain.PageSize * dtgMain.CurrentPageIndex) + e.Item.ItemIndex + 1

            Dim lblSearch12 As Label = e.Item.FindControl("lblSearch12")
            lblSearch12.Text = e.Item.DataItem.Dealer.SearchTerm1 + " / " + e.Item.DataItem.Dealer.SearchTerm2

            Dim lblStatus As Label = e.Item.FindControl("lblStatus")
            lblStatus.Text = IIf(e.Item.DataItem.Status = 1, "Aktif", "Tidak Aktif")

            Dim lblTglUpdate As Label = e.Item.FindControl("lblTglUpdate")
            lblTglUpdate.Text = Format(e.Item.DataItem.LastUpdateTime, "dd/MM/yyyy")

            Dim lbtnPopUp As LinkButton = CType(e.Item.FindControl("lbtnPopUp"), LinkButton)
            lbtnPopUp.Attributes("OnClick") = "showPopUp('../PopUp/PopUpStatusMasterDealer.aspx?DealerCode=" & objMasterDealer.Dealer.DealerCode & "','',500,760,'');"
        End If
    End Sub

    Private Sub dtgMain_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMain.PageIndexChanged
        BindPage(e.NewPageIndex)
    End Sub

    Private Sub dtgMain_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMain.SortCommand
        If ViewState.Item("SortColumn") = e.SortExpression Then
            If ViewState.Item("SortDirection") = Sort.SortDirection.ASC Then
                ViewState.Item("SortDirection") = Sort.SortDirection.DESC
            Else
                ViewState.Item("SortDirection") = Sort.SortDirection.ASC
            End If
        Else
            ViewState.Item("SortColumn") = e.SortExpression
            ViewState.Item("SortDirection") = Sort.SortDirection.ASC
        End If
        BindPage(0)
    End Sub

#End Region

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub
End Class

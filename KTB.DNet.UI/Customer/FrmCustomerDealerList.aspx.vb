#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Security
#End Region
#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
#End Region

Public Class FrmCustomerDealerList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgCustomerRequest As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
#Region " Private Variables"
    ' Dim sessHelp As SessionHelper = New SessionHelper
    Private custCode As String = String.Empty
    Private objDealer As Dealer
    Private sessHelper As New SessionHelper
    Private CustDealerList As ArrayList
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.CustomerListSAP_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=KONSUMEN - Daftar Konsumen")
        End If
    End Sub

  

    Private Function CheckBtnAtGridPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.CustomerListViewDetail_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'Put user code to initialize the page here
        InitiateAuthorization()
        custCode = Request.QueryString("qxctrvvyuotrpn")
        If Not Page.IsPostBack Then

            BindDatagrid()
        End If
    End Sub

    Private Sub BindDatagrid()
        Dim totalRow As Integer = 0
        Dim strDealerCode As String
        Dim criterias As CriteriaComposite
        Dim _objDealer As Dealer = Session.Item("DEALER")
        criterias = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "Customer.Code", MatchType.Exact, custCode))
        If _objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(CustomerDealer), "Dealer.ID", MatchType.Exact, _objDealer.ID))
        End If
        CustDealerList = New CustomerDealerFacade(User).Retrieve(criterias)
        dtgCustomerRequest.DataSource = CustDealerList
        dtgCustomerRequest.VirtualItemCount = totalRow
        dtgCustomerRequest.DataBind()

    End Sub


    Private Sub dtgCustomerRequest_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCustomerRequest.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim objCustDealer As CustomerDealer = e.Item.DataItem
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lbtnActivate As LinkButton = CType(e.Item.FindControl("lbtnActivate"), LinkButton)
            Dim lbtnNotActivate As LinkButton = CType(e.Item.FindControl("lbtnNotActivate"), LinkButton)
            lblDealerCode.Text = objCustDealer.Dealer.DealerCode
            lblDealerName.Text = objCustDealer.Dealer.DealerName
            If objCustDealer.RowStatus = DBRowStatus.Active Then
                lblStatus.Text = "Aktif"
            Else
                lblStatus.Text = "Tidak Aktif"
            End If
            If objCustDealer.RowStatus = DBRowStatus.Active Then
                lbtnNotActivate.Visible = True
                lbtnActivate.Visible = False
            Else
                lbtnNotActivate.Visible = False
                lbtnActivate.Visible = True
            End If
        End If
    End Sub

    Private Sub dtgCustomerRequest_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCustomerRequest.SortCommand
        If e.SortExpression = viewstate.Item("SortCol") Then
            If viewstate.Item("SortDirection") = Sort.SortDirection.ASC Then
                viewstate.Item("SortDirection") = Sort.SortDirection.DESC
            Else
                viewstate.Item("SortDirection") = Sort.SortDirection.ASC
            End If
        End If
        viewstate.Item("SortCol") = e.SortExpression
        dtgCustomerRequest.SelectedIndex = -1
        dtgCustomerRequest.CurrentPageIndex = 0
        BindDatagrid()
    End Sub


    Private Sub dtgCustomerRequest_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgCustomerRequest.ItemCommand
        If e.CommandName = "NONAKTIVEKAN" Then
            UpdateStatus(CInt(e.CommandArgument))
            BindDatagrid()
        Else
            If e.CommandName = "AKTIVEKAN" Then
                UpdateStatus(CInt(e.CommandArgument))
                BindDatagrid()
            End If
        End If
    End Sub


    Private Sub UpdateStatus(ByVal id As Integer)
        Dim objCustdealer As CustomerDealer = New CustomerDealerFacade(User).Retrieve(id)
        If objCustdealer.RowStatus = DBRowStatus.Active Then
            objCustdealer.RowStatus = CType(DBRowStatus.Deleted, Short)
        Else
            objCustdealer.RowStatus = CType(DBRowStatus.Active, Short)
        End If
        Dim i As Integer = New CustomerDealerFacade(User).Update(objCustdealer)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("FrmCustomerList.aspx")
    End Sub
End Class

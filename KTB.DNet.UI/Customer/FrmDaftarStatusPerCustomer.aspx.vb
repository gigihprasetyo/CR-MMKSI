#Region "Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Security
#End Region


Public Class FrmDaftarStatusPerCustomer
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCustomerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblCustomerName As System.Web.UI.WebControls.Label
    Protected WithEvents dtListStatus As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents dtListRequest As System.Web.UI.WebControls.DataGrid

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
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            DoLoad()

        End If
    End Sub

    Sub DoLoad()
        If Not Request.QueryString("qxctrvvyuotrpn") Is Nothing Then
            Dim _customerReqID As Integer = Integer.Parse(Request.QueryString("qxctrvvyuotrpn").ToString())
            'Response.Write(Request.QueryString("qxctrvvyuotrpn").ToString())
            'QueryString=KodePelanggan
            'GetCustomerDealerBy Code Pelanggan
            'Dim _facadeCustomer As New CustomerDealerFacade(User)
            'Dim l_criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "Customer.Code", MatchType.Exact, _customerCode))

            'Dim _customerDealer As New CustomerDealer
            '_customerDealer = _facadeCustomer.Retrieve(l_criterias)(0)
            'lblCustomerCode.Text = _customerCode
            'lblCustomerName.Text = _customerDealer.Customer.CompleteName
            'lblDealerCode.Text = _customerDealer.Dealer.DealerCode

            'Get Request Id
            dtListRequest.Visible = False
            dtListStatus.Visible = False
           
            Dim _facadeRequest As New CustomerRequestFacade(User)
            'Dim l_criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerRequest), "ID", MatchType.Exact, _customerReqID))
            Dim _customerRequest As New CustomerRequest
            _customerRequest = _facadeRequest.Retrieve(_customerReqID)
            lblCustomerCode.Text = _customerRequest.CustomerCode
            lblDealerCode.Text = _customerRequest.Dealer.DealerCode
            BindListHistory(_customerReqID)

            'Dim _lst As New ArrayList
            '_lst = _facadeRequest.Retrieve(l_criterias2)


            'If (_lst.Count > 0) Then
            '    _customerRequest = _lst(0)
            '    lblCustomerCode.Text = _customerRequest.CustomerCode
            '    lblDealerCode.Text = _customerRequest.Dealer.DealerCode

            '    dtListRequest.Visible = True
            '    Me.dtListRequest.DataSource = _lst
            '    Me.dtListRequest.DataBind()
            'End If

        End If

    End Sub

    Sub BackPage()
        Response.Redirect("FrmCustomerList.aspx")

    End Sub


    Sub BindListHistory(ByVal reqID As Int32)
        Dim _custHistory As New CustomerStatusHistoryFacade(User)
        Dim l_criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerStatusHistory), "CustomerRequest.ID", MatchType.Exact, reqID))
        Dim _lstHistory As New ArrayList
        _lstHistory = _custHistory.Retrieve(l_criterias)
        If Not (_lstHistory Is Nothing) Then
            dtListStatus.Visible = True
            Me.dtListStatus.DataSource = _lstHistory
            Me.dtListStatus.DataBind()
        ElseIf _lstHistory.Count < 1 Then
            dtListStatus.Visible = False
            MessageBox.Show("Historis Pengajuan Tidak Tersedia")
        Else
            MessageBox.Show("Historis Pengajuan Tidak Tersedia")
        End If

    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        BackPage()
    End Sub

    Private Sub dtListRequest_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtListRequest.SelectedIndexChanged

        Dim _requestId As Integer = CType(dtListRequest.SelectedItem().Cells(1).Text, Integer)
        BindListHistory(_requestId)
    End Sub

    Private Sub dtListStatus_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtListStatus.ItemDataBound

        If (e.Item.ItemType = ListItemType.Item) Or e.Item.ItemType = ListItemType.AlternatingItem Then
            DisplayList(e)
        End If

    End Sub

    Sub DisplayList(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        If (e.Item.Cells(0).Text = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Baru) Then
            e.Item.Cells(0).Text = "Baru"
        ElseIf (e.Item.Cells(0).Text = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Block) Then
            e.Item.Cells(0).Text = "Block"
        ElseIf (e.Item.Cells(0).Text = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Proses) Then
            e.Item.Cells(0).Text = "Proses"
        ElseIf (e.Item.Cells(0).Text = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Selesai) Then
            e.Item.Cells(0).Text = "Selesai"

        ElseIf (e.Item.Cells(0).Text = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Validasi) Then
            e.Item.Cells(0).Text = "Validasi"
        Else

            e.Item.Cells(0).Text = ""
        End If

        If (e.Item.Cells(1).Text = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Baru) Then
            e.Item.Cells(1).Text = "Baru"
        ElseIf (e.Item.Cells(1).Text = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Block) Then
            e.Item.Cells(1).Text = "Block"
        ElseIf (e.Item.Cells(1).Text = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Proses) Then
            e.Item.Cells(0).Text = "Proses"
        ElseIf (e.Item.Cells(1).Text = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Selesai) Then
            e.Item.Cells(1).Text = "Selesai"

        ElseIf (e.Item.Cells(1).Text = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Validasi) Then
            e.Item.Cells(1).Text = "Validasi"
        Else
            e.Item.Cells(1).Text = ""
        End If
    End Sub

End Class

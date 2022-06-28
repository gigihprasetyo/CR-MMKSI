#Region "Custom Namespace Imports"
Imports Ktb.DNet.Domain
Imports Ktb.DNet.BusinessFacade.General
Imports Ktb.DNet.BusinessFacade.FinishUnit
Imports Ktb.DNet.BusinessFacade.PO
Imports Ktb.DNet.BusinessFacade.PK
Imports Ktb.DNet.BusinessFacade.Helper
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Utility
Imports Ktb.DNet.Security
Imports KTB.DNet.UI.Helper

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region


Public Class PopUpDailyFlowPOPK
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPKNumber As System.Web.UI.WebControls.Label
    Protected WithEvents dgPO As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblContractNumber As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private PKFacade As New PKHeaderFacade(User)
    Private ContractFacade As New ContractHeaderFacade(User)
    Private POFacade As New POHeaderFacade(User)
    Private ChassisFacade As New ChassisMasterFacade(User)
    Private sessHelper As New SessionHelper
#End Region

#Region "PrivateCustomMethods"
    Private Function ArrListChassisMaster(ByVal SONumber As String) As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "SONumber", _
            MatchType.Exact, SONumber))
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, _
            CType(DBRowStatus.Active, Short)))
        Return ChassisFacade.Retrieve(criterias)
    End Function
    Private Sub BindDgPO(ByVal indexPage As Integer)
        If Not IsNothing(sessHelper.GetSession("ContractHeaderID")) Then
            Dim totalRow As Integer = 0
            Dim ContractHeaderID As Integer = CInt(sessHelper.GetSession("ContractHeaderID"))
            Dim criterias As New CriteriaComposite(New Criteria(GetType(POHeader), "ContractHeader.ID", _
                MatchType.Exact, ContractHeaderID))
            criterias.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, _
                CType(DBRowStatus.Active, Short)))
            Dim ArrPO As ArrayList = POFacade.RetrieveByCriteria(criterias, indexPage + 1, dgPO.PageSize, totalRow)
            dgPO.DataSource = ArrPO
            dgPO.VirtualItemCount = totalRow
            dgPO.DataBind()
        ElseIf Not IsNothing(sessHelper.GetSession("PONumber")) Then
            Dim totalRow As Integer = 0
            Dim PONumber As Integer = CInt(sessHelper.GetSession("PONumber"))
            Dim criterias As New CriteriaComposite(New Criteria(GetType(POHeader), "PONumber", _
                MatchType.Exact, PONumber))
            criterias.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, _
                CType(DBRowStatus.Active, Short)))
            Dim ArrPO As ArrayList = POFacade.RetrieveByCriteria(criterias, indexPage + 1, dgPO.PageSize, totalRow)
            dgPO.DataSource = ArrPO
            dgPO.VirtualItemCount = totalRow
            dgPO.DataBind()
        End If
    End Sub
    Private Sub Initialize(ByVal flow As String)
        Dim arrFlow() As String = flow.Split("_")
        If arrFlow.Length = 2 Then
            Dim type As String = arrFlow(0)
            Dim value As String = arrFlow(1)
            If type = "PK" Then
                Dim ObjPKHeader As PKHeader = PKFacade.Retrieve(value)
                Dim ObjContract As ContractHeader = ContractFacade.RetrieveByPKNumber(value)
                sessHelper.SetSession("ContractHeaderID", ObjContract.ID)
                lblPKNumber.Text = ObjPKHeader.PKNumber
                lblContractNumber.Text = ObjContract.ContractNumber
                BindDgPO(0)
            ElseIf type = "PO" Then
                Dim ObjPO As POHeader = POFacade.Retrieve(value)
                Dim ObjContract As ContractHeader = ObjPO.ContractHeader
                Dim ObjPKHeader As PKHeader = PKFacade.Retrieve(ObjContract.PKNumber)
                sessHelper.SetSession("PONumber", ObjPO.PONumber)
                lblPKNumber.Text = ObjPKHeader.PKNumber
                lblContractNumber.Text = ObjContract.ContractNumber
                BindDgPO(0)
            End If
        End If
    End Sub
    Private Sub Initialize()
        lblPKNumber.Text = ""
        lblContractNumber.Text = ""
        dgPO.DataSource = New ArrayList
        dgPO.DataBind()
    End Sub
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("flow")) Then
                Initialize(Request.QueryString("flow"))
            Else
                Initialize()
            End If
        End If
    End Sub
    Private Sub dgPO_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPO.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgPO.CurrentPageIndex * dgPO.PageSize)
            Dim dgChassisMaster As DataGrid = CType(e.Item.FindControl("dgChasisMaster"), DataGrid)
            Dim ObjPO As POHeader = CType(e.Item.DataItem, POHeader)
            dgChassisMaster.DataSource = ArrListChassisMaster(ObjPO.SONumber)
            dgChassisMaster.DataBind()
        End If
    End Sub
    Private Sub dgPO_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPO.PageIndexChanged
        dgPO.CurrentPageIndex = e.NewPageIndex
        BindDgPO(dgPO.CurrentPageIndex)
    End Sub
#End Region

End Class

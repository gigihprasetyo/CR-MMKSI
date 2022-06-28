#Region "Custom Namespace Imports"
Imports Ktb.DNet.BusinessFacade
Imports Ktb.DNet.BusinessFacade.General
Imports Ktb.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
#End Region

Public Class FrmGetSPPOCheckListDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblOrderType As System.Web.UI.WebControls.Label
    Protected WithEvents lblPONumber As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDate As System.Web.UI.WebControls.Label
    Protected WithEvents dtgCheckListDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private nSPPOID As Integer = 0
    Private sessHelper As SessionHelper = New SessionHelper
    Private objSPPO As SparePartPO
    Private objSPPODetail As SparePartPODetail
    Private objSPMaster As SparePartMaster
    Private SPPODetail As ArrayList
    Private totalRow As Integer
#End Region

#Region "Custom Method"

    Private Function GetFromSession(ByVal sObject As String) As Object
        If Session("DEALER") Is Nothing Then
            Response.Redirect("../SessionExpired.htm")
        Else
            Return Session(sObject)
        End If
    End Function

    Private Sub getSparePartPO()
        objSPPO = New SparePartPOFacade(User).Retrieve(nSPPOID)
        If objSPPO Is Nothing Then
            sessHelper.SetSession("sesSPPO", Nothing)
        Else
            sessHelper.SetSession("sesSPPO", objSPPO)
        End If
    End Sub

    Private Sub retrieveHeader()
        If Not GetFromSession("sesSPPO") Is Nothing Then
            objSPPO = CType(GetFromSession("sesSPPO"), SparePartPO)
            lblOrderType.Text = objSPPO.OrderTypeDesc
            lblPONumber.Text = objSPPO.PONumber
            lblDate.Text = objSPPO.PODate
        Else
            lblOrderType.Text = String.Empty
            lblPONumber.Text = String.Empty
            lblDate.Text = String.Empty
        End If
    End Sub

    Private Sub retrieveDetails(ByVal pageIndex As Integer)
        FindData(pageIndex)
        If SPPODetail.Count > 0 Then
            dtgCheckListDetail.DataSource = SPPODetail
            dtgCheckListDetail.VirtualItemCount = totalRow
            dtgCheckListDetail.DataBind()
        Else
            dtgCheckListDetail.DataSource = New ArrayList
            dtgCheckListDetail.VirtualItemCount = 0
            dtgCheckListDetail.DataBind()
            MessageBox.Show(SR.ViewFail)
        End If
    End Sub

    Private Sub FindData(ByVal pageIndex As Integer)
        If Not GetFromSession("sesSPPO") Is Nothing Then
            objSPPO = CType(GetFromSession("sesSPPO"), SparePartPO)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartPODetail), "SparePartPO.ID", MatchType.Exact, objSPPO.ID))
            criterias.opAnd(New Criteria(GetType(SparePartPODetail), "CheckListStatus", MatchType.Exact, "0"))
            SPPODetail = New SparePartPODetailFacade(User).RetrieveByCriteria(criterias, pageIndex, dtgCheckListDetail.PageSize, totalRow)
        Else
            SPPODetail = New ArrayList
        End If
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("sppoID")) Then
                nSPPOID = CType(Request.QueryString("sppoID"), Integer)
                getSparePartPO()
                retrieveHeader()
                retrieveDetails(1)

            End If
        End If
    End Sub

    Private Sub dtgCheckListDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCheckListDetail.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1
        End If
    End Sub

    Private Sub dtgCheckListDetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCheckListDetail.PageIndexChanged
        dtgCheckListDetail.CurrentPageIndex = e.NewPageIndex
        retrieveDetails(e.NewPageIndex + 1)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        sessHelper.RemoveSession("sessSPPO")
    End Sub

#End Region

End Class
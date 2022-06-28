Imports KTB.DNet.BusinessFacade.SPAF
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility

Public Class PopUpHistorySPAFStatus
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDocType As System.Web.UI.WebControls.Label
    Protected WithEvents dtgSPAF As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btn As System.Web.UI.WebControls.Button
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private lastStatus As String = ""
    Private sHelper As New SessionHelper

    Private ReadOnly Property GetSPAFDoc() As Integer
        Get
            Return Request.QueryString("ID")
        End Get
    End Property

    Private Property DataSource() As ArrayList
        Get
            Return sHelper.GetSession("DataSourceSpafDocHistories")
        End Get
        Set(ByVal Value As ArrayList)
            sHelper.SetSession("DataSourceSpafDocHistories", Value)
        End Set
    End Property

    Private Function ProcessOldStatus(ByVal dataSource As ArrayList) As ArrayList
        For Each objSpaf As SPAFDocHistory In KTB.DNet.Utility.CommonFunction.SortArraylist(dataSource, _
            GetType(SPAFDocHistory), "ID", Sort.SortDirection.ASC)
            objSpaf.OldStatus = lastStatus
            lastStatus = CType(objSpaf.Status, EnumSPAFSubsidy.SPAFDocStatus).ToString
        Next
        Return dataSource
    End Function

    Private Sub BindHistory()
        Dim objFacade As New SPAFFacade(User)
        Dim objSPAF As SPAFDoc = objFacade.Retrieve(GetSPAFDoc)
        lblDealerCode.Text = objSPAF.Dealer.DealerCode
        lblDocType.Text = IIf(objSPAF.DocType = CType(EnumDocumentType.DocumentType.SPAF, Short), _
            EnumDocumentType.DocumentType.SPAF.ToString, EnumDocumentType.DocumentType.Subsidi.ToString)
        Dim objSPAFHistory As ArrayList
        DataSource = ProcessOldStatus(objSPAF.SPAFDocHistorys)
        BindGrid(DataSource)
    End Sub

    Private Sub BindGrid(ByVal SPAFDocHistories As ArrayList)
        SPAFDocHistories = KTB.DNet.Utility.CommonFunction.SortArraylist(SPAFDocHistories, _
            GetType(SPAFDocHistory), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
        dtgSPAF.DataSource = SPAFDocHistories
        dtgSPAF.DataBind()
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            ViewState("CurrentSortColumn") = "ID"
            BindHistory()
            btnClose.Attributes.Add("onclick", "javascript:window.close();")
        End If
    End Sub

    Private Sub dtgSPAF_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSPAF.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(0).Text = (e.Item.ItemIndex + 1 + (dtgSPAF.PageSize * dtgSPAF.CurrentPageIndex)).ToString
        End If
    End Sub

    Private Sub dtgSPAF_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSPAF.SortCommand
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
        BindGrid(DataSource)
    End Sub
End Class
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.BabitSalesComm


Public Class PopUpBabitAllocationProposal
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtNoPengajuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoPersetujuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgBabitProposal As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblDanaBabit As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private objDealer As Dealer
    Private DanaBabit As Decimal
    Private SisaBabit As Decimal
    Private TotalPengajuan As Decimal = 0


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim objAlloc As BabitAllocation = New BabitFacade(User).RetrieveBabitAllocation(CType(Request.QueryString("id"), Integer))
        DanaBabit = objAlloc.DanaBabit
        SisaBabit = DanaBabit

        BindDataGrid()
        lblDanaBabit.Text = "Rp " & DanaBabit.ToString("#,##0")
    End Sub

    Public Sub BindDataGrid()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitProposal), "Status", MatchType.Exact, CType(EnumBabit.StatusBabitProposal.Disetujui, String)))
        criterias.opAnd(New Criteria(GetType(BabitProposal), "BabitAllocation.ID", MatchType.Exact, CType(Request.QueryString("id"), Integer)))

        Dim al As New ArrayList
        al = New BabitProposalFacade(User).Retrieve(criterias)

        dtgBabitProposal.DataSource = al
        dtgBabitProposal.VirtualItemCount = al.Count
        dtgBabitProposal.DataBind()
    End Sub

    Private Sub dtgBabitProposal_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBabitProposal.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim RowData As BabitProposal = e.Item.DataItem
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1

            Dim lblTgl As Label = CType(e.Item.FindControl("lblTgl"), Label)
            If RowData.TglTerimaEvidance < New Date(1900, 1, 1) Then
                lblTgl.Text = ""
            Else
                lblTgl.Text = RowData.TglTerimaEvidance.ToString("dd/MM/yyyy")
            End If

            Dim lblNoPengajuan As LinkButton = CType(e.Item.FindControl("lblNoPengajuan"), LinkButton)
            lblNoPengajuan.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../Babit/FrmPengajuanBabit.aspx?Src=PopUpPengajuan&Mode=View&id=" & RowData.ID, "", 710, 700, "ViewForm")

            SisaBabit -= RowData.KTBApprovalAmount
            TotalPengajuan += RowData.KTBApprovalAmount
            Dim lblSisaBabit As Label = CType(e.Item.FindControl("lblSisaBabit"), Label)
            lblSisaBabit.Text = SisaBabit.ToString("#,##0")

        End If
        If e.Item.ItemType = ListItemType.Footer Then
            Dim lblTotalPengajuan As Label = CType(e.Item.Cells(1).FindControl("lblTotalPengajuan"), Label)
            lblTotalPengajuan.Text = TotalPengajuan.ToString("#,##0")
        End If
    End Sub
End Class

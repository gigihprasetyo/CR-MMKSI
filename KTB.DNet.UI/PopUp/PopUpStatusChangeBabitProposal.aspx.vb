Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search

Public Class PopUpStatusChangeBabitProposal
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblNoDucument As System.Web.UI.WebControls.Label
    Protected WithEvents dgDetails As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Sub BindData(ByVal ID As String)
        Dim arl As New ArrayList
        arl = New BabitSalesComm.BabitProposalFacade(User).RetrieveBabitProposalHistory(ID)
        lblNoDucument.Text = CType(arl(0), BabitProposalHistory).BabitProposal.NoPengajuan
        dgDetails.DataSource = arl
        dgDetails.DataBind()
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BindData(Request.QueryString("ID"))
    End Sub

    Private Sub dgDetails_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDetails.ItemDataBound
        If (e.Item.ItemIndex >= 0) Then
            Dim Obj As BabitProposalHistory = CType(e.Item.DataItem, BabitProposalHistory)
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            If (Obj.Status = CType(EnumBabit.StatusBabitProposal.Baru, Short)) Then
                lblStatus.Text = EnumBabit.StatusBabitProposal.Baru.ToString()
            End If
            If (Obj.Status = CType(EnumBabit.StatusBabitProposal.Batal, Short)) Then
                lblStatus.Text = EnumBabit.StatusBabitProposal.Batal.ToString()
            End If
            If (Obj.Status = CType(EnumBabit.StatusBabitProposal.Disetujui, Short)) Then
                lblStatus.Text = EnumBabit.StatusBabitProposal.Disetujui.ToString()
            End If
            If (Obj.Status = CType(EnumBabit.StatusBabitProposal.Proses, Short)) Then
                lblStatus.Text = EnumBabit.StatusBabitProposal.Proses.ToString()
            End If
            If (Obj.Status = CType(EnumBabit.StatusBabitProposal.Tolak, Short)) Then
                lblStatus.Text = EnumBabit.StatusBabitProposal.Tolak.ToString()
            End If
            If (Obj.Status = CType(EnumBabit.StatusBabitProposal.Validasi, Short)) Then
                lblStatus.Text = EnumBabit.StatusBabitProposal.Validasi.ToString()
            End If

            Dim lblProcessBy As Label = CType(e.Item.FindControl("lblProcessBy"), Label)
            lblProcessBy.Text = KTB.DNet.Utility.CommonFunction.FormatSavedUser(Obj.CreatedBy, User)


        End If
    End Sub

End Class

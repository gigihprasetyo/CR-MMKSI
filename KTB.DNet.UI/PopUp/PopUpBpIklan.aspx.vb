Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.IO
Imports KTB.DNet.Security

Public Class PopUpBpIklan
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgIklan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTotalBiayaIklan As System.Web.UI.WebControls.Label
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblJenisKegiatan As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


    Sub BindGridIklan()
        Dim arl As New ArrayList
        arl = New BabitSalesComm.BabitProposalFacade(Page.User).RetrieveIklanByBabitProposalID(Convert.ToInt32(Request.QueryString("id")))
        dgIklan.DataSource = arl
        dgIklan.DataBind()

        Dim total As Decimal = 0
        For Each obj As BPIklan In arl
            total += obj.Expense
        Next

        Dim facade As New KTB.DNet.BusinessFacade.BabitSalesComm.BabitProposalFacade(Page.User)
        Dim objBp As BabitProposal = facade.Retrieve(Convert.ToInt32(Request.QueryString("id")))

        lblDealerCode.Text = objBp.Dealer.DealerCode
        lblDealerName.Text = objBp.Dealer.DealerName

        If (objBp.ActivityType = CType(EnumBabit.BabitProposalType.Even, Short)) Then
            lblJenisKegiatan.Text = EnumBabit.BabitProposalType.Even.ToString()
        ElseIf (objBp.ActivityType = CType(EnumBabit.BabitProposalType.Iklan, Short)) Then
            lblJenisKegiatan.Text = EnumBabit.BabitProposalType.Iklan.ToString()
        ElseIf (objBp.ActivityType = CType(EnumBabit.BabitProposalType.Pameran, Short)) Then
            lblJenisKegiatan.Text = EnumBabit.BabitProposalType.Pameran.ToString()
        End If

        lblNoPengajuan.Text = objBp.NoPengajuan
        lblTglPengajuan.Text = objBp.CreatedTime.ToString("dd MMM yyyy")

        lblTotalBiayaIklan.Text = String.Format("Rp.{0}", total.ToString("#,##0"))

        'If Not IsNothing(Request.QueryString("paymentid")) Then
        '    btnSave.Enabled = False
        'End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If IsPostBack Then Return

        If (IsNothing(Request.QueryString("id"))) Then Return

        BindGridIklan()
    End Sub

    Public Sub UpdateBpIklan(ByVal babitProposalID As Integer)
        Dim no As String = ""
        Dim errMsg As String = ""
        Dim total As Decimal = 0
        Dim i As Integer = 0
        Dim facade As New KTB.DNet.BusinessFacade.BabitSalesComm.BabitProposalFacade(Page.User)
        For Each item As DataGridItem In dgIklan.Items
            Dim obj As BPIklan = facade.RetrieveIklanByID(Convert.ToInt32(dgIklan.DataKeys().Item(i)))
            Dim txt As TextBox = CType(item.FindControl("txtPersetujuanKTB"), TextBox)

            If (obj.BabitProposal.BabitAllocation.TipeAlokasi = EnumBabit.BabitAllocationType.Babit_Khusus) Then
                If (Convert.ToDecimal(txt.Text) <= obj.Expense) Then
                    obj.KTBApprovalAmount = Convert.ToDecimal(txt.Text)
                    facade.UpdateBpIklan(obj)
                    no = obj.BabitProposal.NoPengajuan
                    total += Convert.ToDecimal(txt.Text)
                Else
                    errMsg += String.Format("item no {0} - {1}, melebihi batas yang diajukan.\n", (i + 1), obj.MediaName)
                End If
            Else
                If (Convert.ToDecimal(txt.Text) > (obj.Expense * 0.5)) Then
                    errMsg += String.Format("item no {0} - {1}, melebihi batas yang diajukan 50% untuk alokasi reguler dan alokasi tambahan.\n", (i + 1), obj.MediaName)
                Else
                    obj.KTBApprovalAmount = Convert.ToDecimal(txt.Text)
                    facade.UpdateBpIklan(obj)
                    no = obj.BabitProposal.NoPengajuan
                    total += Convert.ToDecimal(txt.Text)
                End If
            End If
            i += 1
        Next
        Dim objBp As BabitProposal = facade.Retrieve(Convert.ToInt32(Request.QueryString("id")))
        If (total > objBp.BabitAllocation.SisaBabit) Then
            MessageBox.Show("Dana yg disetujui melebihi jumlah sisa alokasi babit " & "(" & objBp.BabitAllocation.SisaBabit & ")")
            Exit Sub
        End If
        objBp.KTBApprovalAmount = total
        facade.UpdateBabitProposal(objBp)
        MessageBox.Show(String.Format("Jumlah persetujuan telah berhasil di edit untuk nomor pengajuan babit {0}. {1}", no, errMsg))
    End Sub

    Private Sub dgIklan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgIklan.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oIklan As BPIklan = CType(e.Item.DataItem, BPIklan)
            Dim lblMedia As Label = CType(e.Item.FindControl("lblMedia"), Label)

            lblMedia.Text = IIf(oIklan.MediaType = EnumBabit.MediaType.Cetak, "Cetak", "Elektronik")

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", _
                    New CommonFunction().PreventDoubleClickAtGrid(CType(e.Item.FindControl("lbtnDelete"), LinkButton), "Yakin Data ini akan dihapus?"))
            End If

            'If Not IsNothing(Request.QueryString("paymentid")) Then
            '    Dim txtPersetujuanKTB As TextBox = CType(e.Item.FindControl("txtPersetujuanKTB"), TextBox)
            '    txtPersetujuanKTB.Enabled = False
            'End If
        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (IsNothing(Request.QueryString("id"))) Then Return
        UpdateBpIklan(Convert.ToInt32(Request.QueryString("id")))
    End Sub

    Private Sub dgIklan_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgIklan.ItemCommand
        If (e.CommandName = "Reject") Then
            Dim facade As New KTB.DNet.BusinessFacade.BabitSalesComm.BabitProposalFacade(Page.User)
            Dim obj As BPIklan = facade.RetrieveIklanByID(Convert.ToInt32(e.CommandArgument))
            Dim tmpAmount As Decimal = obj.KTBApprovalAmount
            obj.KTBApprovalAmount = 0
            facade.UpdateBpIklan(obj)
            Dim txt As TextBox = CType(e.Item.FindControl("txtPersetujuanKTB"), TextBox)
            txt.Text = "0"

            obj.BabitProposal.KTBApprovalAmount -= tmpAmount
            facade.Update(obj.BabitProposal)

            If Not IsNothing(Request.QueryString("paymentid")) Then
                Dim pfacade As New BabitSalesComm.BabitPaymentFacade(User)
                Dim objPayment As BabitPayment = pfacade.Retrieve(Convert.ToInt32(Request.QueryString("paymentid")))
                If (obj.BabitProposal.KTBApprovalAmount = 0) Then
                    objPayment.NomorPembayaran = BabitPayment.REJECTED_ALL_DESC
                Else
                    objPayment.NomorPembayaran = BabitPayment.REJECTED_ITEM_DESC
                End If
                pfacade.Update(objPayment)
            End If
            MessageBox.Show("Item sudah ditolak")
        End If

    End Sub
End Class
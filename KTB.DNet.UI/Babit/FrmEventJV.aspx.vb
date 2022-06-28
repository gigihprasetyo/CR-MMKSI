Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security

Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports OfficeOpenXml
Imports System.Linq

Public Class FrmEventJV
    Inherits System.Web.UI.Page

    Private oDealer As Dealer
    Private sessHelper As New SessionHelper
    Private Mode As String = ""
    Private oBabitEventReportJV As BabitEventReportJV
    Private vsJV As String = "VS_JVID"
    Private vsMode As String = "VS_MODE"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        ViewState("currSortColumn") = "ID"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
        Authorization()

        PageInit()
        Mode = ViewState(vsMode)
        oBabitEventReportJV = New BabitEventReportJVFacade(User).Retrieve(CInt(ViewState(vsJV)))

        If Not IsPostBack Then
            BindDDL()
            BindData()
            BindGrid(0)

            If Mode = "Detail" Then
                txtRefReceipt.Enabled = False
                txtRefText.Enabled = False
                'ddlNoRek.Enabled = False
                btnSave.Visible = False
            End If

            If oDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                trNoJV.Style.Add("Display", "none")
                trProcessDate.Style.Add("Display", "none")
            End If
        End If
    End Sub

    Private Sub Authorization()
        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            lblRefReceipt.Visible = False
            txtRefReceipt.Visible = True
            lblRefText.Visible = False
            txtRefText.Visible = True
            'ddlNoRek.Visible = False
            lblNoRek.Visible = True
        Else
            lblRefReceipt.Visible = True
            txtRefReceipt.Visible = False
            lblRefText.Visible = True
            txtRefText.Visible = False
            'ddlNoRek.Visible = True
            lblNoRek.Visible = True
        End If
    End Sub

    Private Sub PageInit()
        ViewState(vsJV) = Request.QueryString("BabitEventReportJVID")
        ViewState(vsMode) = Request.QueryString("Mode")
    End Sub

    Private Sub BindDDL()
        'ddlNoRek.Items.Clear()
        'With ddlNoRek.Items
        '    Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    crits.opAnd(New Criteria(GetType(DealerBankAccount), "Dealer.ID", MatchType.Exact, oBabitEventReportJV.Dealer.ID))
        '    Dim arrDBA As ArrayList = New DealerBankAccountFacade(User).Retrieve(crits)
        '    For Each DBA As DealerBankAccount In arrDBA
        '        .Add(New ListItem(DBA.BankName & " / " & DBA.BankAccount, DBA.ID))
        '    Next
        'End With
    End Sub

    Private Sub BindData()
        lblDealer.Text = oBabitEventReportJV.Dealer.DealerCode & " / " & oBabitEventReportJV.Dealer.DealerName
        lblNoPengajuanJV.Text = oBabitEventReportJV.RegNumber
        lblNoJV.Text = oBabitEventReportJV.NoJV
        lblRefReceipt.Text = oBabitEventReportJV.TextReceiptNo
        txtRefReceipt.Text = oBabitEventReportJV.TextReceiptNo
        lblRefText.Text = oBabitEventReportJV.TextRefNo
        txtRefText.Text = oBabitEventReportJV.TextRefNo
        Dim jvToReceipt As BabitEventReportJVtoReceipt = New BabitEventReportJVtoReceiptFacade(User).RetrieveByBabitEventReportJV(oBabitEventReportJV.ID)
        If IsNothing(jvToReceipt.BabitEventReportReceipt.DealerBankAccount) Then
            lblNoRek.Text = ""
        Else
            lblNoRek.Text = jvToReceipt.BabitEventReportReceipt.DealerBankAccount.BankName & " / " & jvToReceipt.BabitEventReportReceipt.DealerBankAccount.BankAccount
        End If
        'If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
        '    If IsNothing(oBabitEventReportJV.DealerBankAccount) Then
        '        lblNoRek.Text = ""
        '    Else
        '        lblNoRek.Text = oBabitEventReportJV.DealerBankAccount.BankName & " / " & oBabitEventReportJV.DealerBankAccount.BankAccount
        '    End If
        'End If
        If oBabitEventReportJV.TglProses.Year > 2000 Then
            lblProcessDate.Text = oBabitEventReportJV.TglProses.ToString("dd/MM/yyyy")
        Else
            lblProcessDate.Text = ""
        End If

        If oBabitEventReportJV.TglPencairan.Year > 2000 Then
            lblPencairan.Text = oBabitEventReportJV.TglPencairan.ToString("dd/MM/yyyy")
        Else
            lblPencairan.Text = ""
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumBabit.EventJVStatus"))
        criterias.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, oBabitEventReportJV.Status))
        Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criterias)
        Dim objStandardCode As New StandardCode
        If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
            objStandardCode = CType(arrDDL(0), StandardCode)
            lblStatus.Text = objStandardCode.ValueDesc
        End If
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(BabitEventReportJVtoReceipt), "BabitEventReportJV.ID", MatchType.Exact, oBabitEventReportJV.ID))
        Dim arrJvToReceipt As ArrayList = New BabitEventReportJVtoReceiptFacade(User).Retrieve(crit)
        If arrJvToReceipt.Count <> 0 Then
            CommonFunction.SortListControl(arrJvToReceipt, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrJvToReceipt, pageIndex, dgListJV.PageSize)
            dgListJV.DataSource = PagedList
            dgListJV.VirtualItemCount = arrJvToReceipt.Count
            dgListJV.DataBind()
        Else
            dgListJV.DataSource = New ArrayList
            dgListJV.VirtualItemCount = 0
            dgListJV.CurrentPageIndex = 0
            dgListJV.DataBind()
        End If
    End Sub


    Protected Sub dgListJV_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgListJV.ItemDataBound
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
        Dim lblNoReceipt As Label = CType(e.Item.FindControl("lblNoReceipt"), Label)
        Dim lblNoAcc As Label = CType(e.Item.FindControl("lblNoAcc"), Label)
        Dim lblAmountClaim As Label = CType(e.Item.FindControl("lblAmountClaim"), Label)
        Dim lblAmountPPn As Label = CType(e.Item.FindControl("lblAmountPPn"), Label)
        Dim lblAmountPPh As Label = CType(e.Item.FindControl("lblAmountPPh"), Label)
        Dim lblReceiptTotal As Label = CType(e.Item.FindControl("lblReceiptTotal"), Label)
        Dim lblText As Label = CType(e.Item.FindControl("lblText"), Label)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oRaw As BabitEventReportJVtoReceipt = CType(e.Item.DataItem, BabitEventReportJVtoReceipt)

            lblNo.Text = (dgListJV.PageSize * dgListJV.CurrentPageIndex) + e.Item.ItemIndex + 1
            lblNoReg.Text = oRaw.BabitEventReportReceipt.BabitEventReportHeader.BabitEventProposalHeader.EventRegNumber
            lblNoReceipt.Text = oRaw.BabitEventReportReceipt.ReceiptNo
            If IsNothing(oRaw.BabitEventReportReceipt.MasterAccrued) Then
                lblNoAcc.Text = ""
            Else
                lblNoAcc.Text = oRaw.BabitEventReportReceipt.MasterAccrued.AccKey
            End If
            lblAmountClaim.Text = oRaw.BabitEventReportReceipt.ClaimAmount.ToString("#,##0")
            lblAmountPPn.Text = oRaw.BabitEventReportReceipt.VATTotal.ToString("#,##0")
            lblAmountPPh.Text = oRaw.BabitEventReportReceipt.PPHTotal.ToString("#,##0")
            lblReceiptTotal.Text = oRaw.BabitEventReportReceipt.TotalReceiptAmount.ToString("#,##0")
            lblText.Text = "BY. EVENTTERPADU : " & oRaw.BabitEventReportReceipt.BabitEventReportHeader.ApprovalNumber
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim strJs As String = String.Empty
        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            oBabitEventReportJV.TextReceiptNo = txtRefReceipt.Text
            oBabitEventReportJV.TextRefNo = txtRefText.Text
            'Else
            'oBabitEventReportJV.DealerBankAccount = New DealerBankAccountFacade(User).Retrieve(CInt(ddlNoRek.SelectedValue))
        End If
        Dim _result As Integer = New BabitEventReportJVFacade(User).Update(oBabitEventReportJV)
        If _result > 0 Then
            strJs = "alert('Simpan Data Berhasil');"
            strJs += "window.location = '../Babit/FrmEventJVList.aspx';"
        Else
            strJs = "alert('Simpan Data Gagal');"
        End If
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("FrmEventJVList.aspx")
    End Sub

    Private Sub dgListJV_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgListJV.PageIndexChanged
        dgListJV.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
    End Sub

    Private Sub dgListJV_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgListJV.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If

        '-- Bind page-1
        dgListJV.CurrentPageIndex = 0
        BindGrid(dgListJV.CurrentPageIndex)
    End Sub
End Class
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

Public Class FrmBabitJV
    Inherits System.Web.UI.Page

    Private oDealer As Dealer
    Private sessHelper As New SessionHelper
    Private Mode As String = ""
    Private oBabitReportJV As BabitReportJV
    Private vsJV As String = "VS_JVID"
    Private vsMode As String = "VS_MODE"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        ViewState("currSortColumn") = "ID"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
        Authorization()

        PageInit()
        Mode = ViewState(vsMode)
        oBabitReportJV = New BabitReportJVFacade(User).Retrieve(CInt(ViewState(vsJV)))

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
        ViewState(vsJV) = Request.QueryString("BabitReportJVID")
        ViewState(vsMode) = Request.QueryString("Mode")
    End Sub

    Private Sub BindDDL()
        'ddlNoRek.Items.Clear()
        'With ddlNoRek.Items
        '    Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    crits.opAnd(New Criteria(GetType(DealerBankAccount), "Dealer.ID", MatchType.Exact, oBabitReportJV.Dealer.ID))
        '    Dim arrDBA As ArrayList = New DealerBankAccountFacade(User).Retrieve(crits)
        '    For Each DBA As DealerBankAccount In arrDBA
        '        .Add(New ListItem(DBA.BankName & " / " & DBA.BankAccount, DBA.ID))
        '    Next
        'End With
    End Sub

    Private Sub BindData()
        lblDealer.Text = oBabitReportJV.Dealer.DealerCode & " / " & oBabitReportJV.Dealer.DealerName
        lblNoPengajuanJV.Text = oBabitReportJV.RegNumber
        lblNoJV.Text = oBabitReportJV.NoJV
        lblRefReceipt.Text = oBabitReportJV.TextReceiptNo
        txtRefReceipt.Text = oBabitReportJV.TextReceiptNo
        lblRefText.Text = oBabitReportJV.TextRefNo
        txtRefText.Text = oBabitReportJV.TextRefNo
        Dim jvToRecipt As BabitReportJVtoReceipt = New BabitReportJVtoReceiptFacade(User).RetrieveByBabitReportJV(oBabitReportJV.ID)
        If IsNothing(jvToRecipt.BabitReportReceipt.DealerBankAccount) Then
            lblNoRek.Text = ""
        Else
            lblNoRek.Text = jvToRecipt.BabitReportReceipt.DealerBankAccount.BankName & " / " & jvToRecipt.BabitReportReceipt.DealerBankAccount.BankAccount
        End If
        'If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
        '    If IsNothing(oBabitReportJV.DealerBankAccount) Then
        '        lblNoRek.Text = ""
        '    Else
        '        lblNoRek.Text = oBabitReportJV.DealerBankAccount.BankName & " / " & oBabitReportJV.DealerBankAccount.BankAccount
        '    End If
        'End If
        If oBabitReportJV.TglProses.Year > 2000 Then
            lblProcessDate.Text = oBabitReportJV.TglProses.ToString("dd/MM/yyyy")
        Else
            lblProcessDate.Text = ""
        End If

        If oBabitReportJV.TglPencairan.Year > 2000 Then
            lblPencairan.Text = oBabitReportJV.TglPencairan.ToString("dd/MM/yyyy")
        Else
            lblPencairan.Text = ""
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumBabit.BabitJVStatus"))
        criterias.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, oBabitReportJV.Status))
        Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criterias)
        Dim objStandardCode As New StandardCode
        If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
            objStandardCode = CType(arrDDL(0), StandardCode)
            lblStatus.Text = objStandardCode.ValueDesc
        End If
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(BabitReportJVtoReceipt), "BabitReportJV.ID", MatchType.Exact, oBabitReportJV.ID))
        Dim arrJvToReceipt As ArrayList = New BabitReportJVtoReceiptFacade(User).Retrieve(crit)
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
            Dim oRaw As BabitReportJVtoReceipt = CType(e.Item.DataItem, BabitReportJVtoReceipt)

            lblNo.Text = (dgListJV.PageSize * dgListJV.CurrentPageIndex) + e.Item.ItemIndex + 1
            lblNoReg.Text = oRaw.BabitReportReceipt.BabitReportHeader.BabitHeader.BabitRegNumber
            lblNoReceipt.Text = oRaw.BabitReportReceipt.ReceiptNo
            If IsNothing(oRaw.BabitReportReceipt.MasterAccrued) Then
                lblNoAcc.Text = ""
            Else
                lblNoAcc.Text = oRaw.BabitReportReceipt.MasterAccrued.AccKey
            End If
            lblAmountClaim.Text = oRaw.BabitReportReceipt.ClaimAmount.ToString("#,##0")
            lblAmountPPn.Text = oRaw.BabitReportReceipt.VATTotal.ToString("#,##0")
            lblAmountPPh.Text = oRaw.BabitReportReceipt.PPHTotal.ToString("#,##0")
            lblReceiptTotal.Text = oRaw.BabitReportReceipt.TotalReceiptAmount.ToString("#,##0")
            If oRaw.BabitReportReceipt.BabitReportHeader.BabitHeader.BabitMasterEventType.ID = 4 Then
                lblText.Text = "BY. IKLANTERPADU : " & oRaw.BabitReportReceipt.BabitReportHeader.BabitHeader.ApprovalNumber
            Else
                lblText.Text = "BY. PAMERANTERPADU : " & oRaw.BabitReportReceipt.BabitReportHeader.BabitHeader.ApprovalNumber
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim strJs As String = String.Empty
        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            oBabitReportJV.TextReceiptNo = txtRefReceipt.Text
            oBabitReportJV.TextRefNo = txtRefText.Text
            'Else
            '    oBabitReportJV.DealerBankAccount = New DealerBankAccountFacade(User).Retrieve(CInt(.SelectedValue))
        End If
        Dim _result As Integer = New BabitReportJVFacade(User).Update(oBabitReportJV)
        If _result > 0 Then
            strJs = "alert('Simpan Data Berhasil');"
            strJs += "window.location = '../Babit/FrmBabitJVList.aspx';"
        Else
            strJs = "alert('Simpan Data Gagal');"
        End If
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("FrmBabitJVList.aspx")
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
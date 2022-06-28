#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports KTB.DNet.SAP
Imports KTB.DNet.UI.Helper
Imports System.Collections.Generic

Imports KTB.DNet.BusinessFacade.PO
#End Region

Public Class FrmTOPSPTransferPayment
    Inherits System.Web.UI.Page

#Region "Custom Variable Declaration"
    Private _sessHelper As SessionHelper = New SessionHelper
    Private objDealer As Dealer
    Dim detail As TOPSPTransferPaymentDetail
    Private Mode As String = String.Empty
    Private IsAuthorizedUbah As Boolean
    Private IsAuthorizedBatal As Boolean
    Private IsAuthorizedValidasi As Boolean
    Private _sessData As String = "FrmTOPSPList._sessData"
    Private _sessBroadCast As String = "FrmTOPSPList._sessBroadCast"

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objDealer = _sessHelper.GetSession("DEALER")
        UserPrivilege()
        bindDDL()

        lblCreditAccount.Text = objDealer.CreditAccount
        lblNamaDealer.Text = objDealer.DealerCode + " / " + objDealer.DealerName

        lblTotalTransfer.Text = "0"
        Dim id As Integer = 0

        If Not Page.IsPostBack Then
            Dim objuser As UserInfo = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            ViewState("TotalTransfer") = "0"

            cleardata()

            If Not IsNothing(Request.QueryString("Mode")) Then
                Mode = Request.QueryString("Mode")
                If Mode <> String.Empty Then

                    Dim objTOPSPDtl As TOPSPTransferPaymentDetail
                    Dim arr As ArrayList

                    arr = CType(_sessHelper.GetSession(_sessBroadCast), ArrayList)
                    _sessHelper.SetSession("BillingSel", arr)
                    ControlEnabler(Mode, arr)

                    dtgTOPSP.DataSource = arr
                    dtgTOPSP.DataBind()
                End If
            Else

            End If


        End If

        'Dim lblBillingNumber As Label = CType(Page.FindControl("lblBillingNumber"), Label)
        'lblBillingNumber.Attributes.Add("onclick", "ShowPPBillingNumber();")

        lbtnBillingNumber.Attributes.Add("onclick", "ShowPPBillingNumber();")
        lblTotalTransfer.Text = ViewState("TotalTransfer")

    End Sub

    Private Sub dtgTOPSP_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgTOPSP.ItemCommand
        Dim arr As New ArrayList
        Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
        Dim lblBillingNumber As Label = CType(e.Item.FindControl("lblBillingNumber"), Label)
        Dim lblTOPSPID As Label = CType(e.Item.FindControl("lblTOPSPID"), Label)

        If (e.CommandName = "Delete") Then
            'If lblTOPSPID.Text = "0" Then
            '    arr = CType(_sessHelper.GetSession("BillingSel"), ArrayList)
            '    For Each row As TOPSPTransferPaymentDetail In arr
            '        If row.SparePartBilling.BillingNumber = lblBillingNumber.Text Then
            '            arr.Remove(row)
            '            Exit For
            '        End If
            '    Next

            '    If arr.Count > 0 Then
            '        _sessHelper.SetSession("BillingSel", arr)
            '    Else
            '        _sessHelper.SetSession("BillingSel", Nothing)
            '    End If

            '    dtgTOPSP.DataSource = arr
            '    dtgTOPSP.DataBind()
            '    txtReturnBilling.Text = ""
            'Else
            '    delete(Val(e.Item.Cells(0).Text))
            'End If
            delete(Val(e.Item.Cells(0).Text), lblBillingNumber.Text)

        End If
    End Sub

    Private Function RowStatus(id As Short) As String
        Dim strStatus
        Select Case id
            Case 0
                strStatus = "Aktif"
            Case -1
                strStatus = "Non Aktif"
        End Select
        Return strStatus
    End Function

    Private Sub dtgTOPSP_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgTOPSP.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim arr As New ArrayList
        Dim objSPBilling As SparePartBilling

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()

            Dim RowValue As TOPSPTransferPaymentDetail = CType(e.Item.DataItem, TOPSPTransferPaymentDetail)

            If RowValue.RowStatus = CType(DBRowStatus.Deleted, Short) Then
                e.Item.Visible = False
            End If

            'objSPBilling = New SparePartBilling
            'objSPBilling = CType(_sessHelper.GetSession("BillingSel"), SparePartBilling)

            'RowValue.SparePartBilling = objSPBilling

            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgTOPSP.CurrentPageIndex * dtgTOPSP.PageSize)
                Dim criDeposit As New CriteriaComposite(New Criteria(GetType(TOPSPDeposit), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criDeposit.opAnd(New Criteria(GetType(TOPSPDeposit), "SparePartBilling.ID", MatchType.Exact, RowValue.SparePartBilling.ID))

                Dim lblTOPSPID As Label = CType(e.Item.FindControl("lblTOPSPID"), Label)
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                Dim lblAmountBillTax As Label = CType(e.Item.FindControl("lblAmountBillTax"), Label)
                Dim lblAmountC2 As Label = CType(e.Item.FindControl("lblAmountC2"), Label)
                Dim lblTotalAmount As Label = CType(e.Item.FindControl("lblTotalAmount"), Label)
                Dim lblTotalAmountHide As Label = CType(e.Item.FindControl("lblTotalAmountHide"), Label)
                Dim lblRowStatus As Label = CType(e.Item.FindControl("lblRowStatus"), Label)

                lblTotalAmountHide.Visible = False

                'If ViewState("StatusTOPSP") = "Baru" Then
                '    lblRowStatus.Text = CType(DBRowStatus.Active, Short)
                'End If

                lblDealerCode.Text = RowValue.SparePartBilling.Dealer.DealerCode
                lblAmountBillTax.Text = (RowValue.SparePartBilling.TotalAmount + RowValue.SparePartBilling.Tax).ToString("N0")

                arr = New TOPSPDepositFacade(User).Retrieve(criDeposit)

                lblTOPSPID.Text = CType(RowValue.ID, Integer).ToString

                lblAmountC2.Text = CType(arr.Item(0), TOPSPDeposit).AmountC2.ToString("N0")
                lblTotalAmount.Text = (CType(arr.Item(0), TOPSPDeposit).AmountC2 + (RowValue.SparePartBilling.TotalAmount + RowValue.SparePartBilling.Tax)).ToString("N0")
                lblTotalAmountHide.Text = (CType(arr.Item(0), TOPSPDeposit).AmountC2 + (RowValue.SparePartBilling.TotalAmount + RowValue.SparePartBilling.Tax))
                valTransferTotal = valTransferTotal + CType(arr.Item(0), TOPSPDeposit).AmountC2 + (RowValue.SparePartBilling.TotalAmount + RowValue.SparePartBilling.Tax)
                If Not RowValue.RowStatus = CType(DBRowStatus.Deleted, Short) Then
                    ViewState("TotalTransfer") = valTransferTotal.ToString("N0")
                End If
            End If
        End If
        If _sessHelper.GetSession("BillingSel") Is Nothing Then
            ViewState("TotalTransfer") = "0"
        Else
            Dim kosong As Integer = 0
            For Each top As TOPSPTransferPaymentDetail In _sessHelper.GetSession("BillingSel")
                If top.RowStatus = CType(DBRowStatus.Active, Short) Then
                    Exit For
                Else
                    kosong = kosong + 1
                End If
            Next
            If CType(_sessHelper.GetSession("BillingSel"), ArrayList).Count = kosong Then
                ViewState("TotalTransfer") = "0"

            End If
        End If

        lblTotalTransfer.Text = ViewState("TotalTransfer")
    End Sub
    Dim valTransferTotal As Double = 0

    Private Sub dtgTOPSP_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgTOPSP.SortCommand
        If e.SortExpression = ViewState("SortColBilling") Then
            If ViewState("SortDirBilling") = Sort.SortDirection.ASC Then
                ViewState.Add("SortDirBilling", Sort.SortDirection.DESC)
            Else
                ViewState.Add("SortDirBilling", Sort.SortDirection.ASC)
            End If
        End If
        ViewState.Add("SortColBilling", e.SortExpression)
        'BindSearch()
    End Sub

    Private Sub btnBaru_Click(sender As Object, e As EventArgs) Handles btnBaru.Click
        _sessHelper.SetSession("TOPSPTransferPayment", Nothing)
        _sessHelper.SetSession("BillingSel", Nothing)
        cleardata()
        lbtnBillingNumber.Visible = True
    End Sub



#Region "Custom Method"

    Private Sub bindDDL()
        Dim arr As ArrayList
        Dim objAppConf As AppConfig

        arr = New TOPSPTransferPaymentFacade(User).Retrieve_Bank()

        ddlBankTranfer.Items.Clear()
        'Dim listitemBlank As New ListItem("Silahkan Pilih", 0)
        'ddlBankTranfer.Items.Add(listitemBlank)

        For Each item As Bank In arr
            Dim listItem As New ListItem(item.BankName, item.ID)
            ddlBankTranfer.Items.Add(listItem)
        Next


    End Sub


    Private Sub delete(ByVal id As Integer, ByVal lblBillingNumber As String)
        Dim arr As ArrayList

        arr = CType(_sessHelper.GetSession("BillingSel"), ArrayList)

        For Each rowdtl As TOPSPTransferPaymentDetail In arr
            If rowdtl.SparePartBilling.BillingNumber = lblBillingNumber Then
                rowdtl.RowStatus = -1
                'arr.Remove(rowdtl)
                Exit For
            End If
        Next

        _sessHelper.SetSession(_sessBroadCast, arr)
        dtgTOPSP.DataSource = arr
        dtgTOPSP.DataBind()
        txtReturnBilling.Text = ""
        'Dim objSalHea As TOPSPTransferPaymentDetail
        'Dim objtopspdtlfacade As New TOPSPTransferPaymentDetailFacade(User)
        'objSalHea = objtopspdtlfacade.Retrieve(id)

        'objtopspdtlfacade.Delete(objSalHea)

    End Sub

    Private Sub UserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.Input_TOPSP_Transfer_Payment_Privilege) Then
            If Request.QueryString("Mode") = "Edit" AndAlso SecurityProvider.Authorize(Context.User, SR.TOPSP_Daftar_Transfer_Payment_Ubah_Privilege) Then
                IsAuthorizedBatal = SecurityProvider.Authorize(Context.User, SR.TOPSP_Daftar_Transfer_Payment_Batal_Privilege)
                IsAuthorizedValidasi = SecurityProvider.Authorize(Context.User, SR.TOPSP_Daftar_Transfer_Payment_Validasi_Privilege)
            ElseIf Request.QueryString("Mode") = "View" Then
                IsAuthorizedBatal = False
                IsAuthorizedUbah = False
                IsAuthorizedValidasi = False
            Else
                Response.Redirect("../frmAccessDenied.aspx?modulName=TOPSparePart")
            End If
            'If Not SecurityProvider.Authorize(Context.User, SR.TOPSP_Daftar_Transfer_Payment_Validasi_Privilege) Then
            'Else
            'IsAuthorizedUbah = SecurityProvider.Authorize(Context.User, SR.Input_TOPSP_Transfer_Payment_Privilege)
            'End If
        Else
            IsAuthorizedUbah = SecurityProvider.Authorize(Context.User, SR.Input_TOPSP_Transfer_Payment_Privilege)
            IsAuthorizedBatal = SecurityProvider.Authorize(Context.User, SR.TOPSP_Daftar_Transfer_Payment_Batal_Privilege)
            IsAuthorizedValidasi = SecurityProvider.Authorize(Context.User, SR.TOPSP_Daftar_Transfer_Payment_Validasi_Privilege)
        End If

    End Sub

    Private Sub cleardata()
        calTanggalTransfer.Enabled = False
        dtgTOPSP.DataSource = New ArrayList

        Dim arrStdCode As ArrayList
        Dim cri As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cri.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumTOPSPTransferPayment.Status"))

        Dim objStdCodeFac As New StandardCodeFacade(User)
        arrStdCode = objStdCodeFac.Retrieve(cri)
        txtReturnBilling.Text = ""
        subStandardCode()

    End Sub

    Private Sub subStandardCode()
        Dim arrStdCode As ArrayList
        Dim cri As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cri.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumTOPSPTransferPayment.Status"))

        Dim objStdCodeFac As New StandardCodeFacade(User)
        arrStdCode = objStdCodeFac.Retrieve(cri)

        'ViewState("StatusTOPSP") = "Baru"

        If CType(_sessHelper.GetSession("TOPSPTransferPayment"), TOPSPTransferPayment) Is Nothing Then
            For Each row As StandardCode In arrStdCode
                If row.ValueId = EnumStatusTOPSPTransferPayment.TOPSPStatus.Baru Then
                    lblStatus.Text = row.ValueDesc
                    lblNoRegPembayaran.Text = ""
                    lblTotalTransfer.Text = ""
                    lblTanggalDibuat.Text = ""
                    lblTglJatuhTempo.Text = ""
                    calTanggalTransfer.Value = Now.Date
                    dtgTOPSP.DataSource = New ArrayList
                    dtgTOPSP.DataBind()
                    ViewState("StatusTOPSP") = "Baru"
                    enableControl(False)
                    Exit For
                End If
            Next
        Else

            Dim objtopsp As TOPSPTransferPayment = CType(_sessHelper.GetSession("TOPSPTransferPayment"), TOPSPTransferPayment)
            For Each row As StandardCode In arrStdCode
                If objtopsp.Status = row.ValueId Then
                    lblStatus.Text = row.ValueDesc
                    lblTanggalDibuat.Text = objtopsp.CreatedTime.ToString("dd/MM/yyyy")
                    calTanggalTransfer.Value = objtopsp.TransferPlanDate
                    lblTglJatuhTempo.Text = objtopsp.DueDate
                    lblNoRegPembayaran.Text = objtopsp.RegNumber
                    ddlBankTranfer.SelectedValue = objtopsp.BankID
                    Exit For
                End If
            Next
        End If
    End Sub


    'Protected Sub txtReturnBilling_TextChanged(sender As Object, e As EventArgs)
    '    Dim arrTOPSPDetail As New ArrayList
    '    Dim str As String()
    '    Dim strBillingID As String
    '    Dim objTOPSPTPdtl As TOPSPTransferPaymentDetail
    '    Dim objSPBill As SparePartBilling
    '    Dim objSPBillFac As New SparePartBillingFacade(User)

    '    str = (txtReturnBilling.Text).Split(New Char() {";"c})
    '    _sessHelper.SetSession("BillingSel", txtReturnBilling.Text)

    '    For Each row As String In str
    '        If String.IsNullOrEmpty(row) Then
    '            objTOPSPTPdtl = New TOPSPTransferPaymentDetail
    '            objSPBill = New SparePartBilling
    '            objSPBill = objSPBillFac.Retrieve(row)

    '            objTOPSPTPdtl.SparePartBilling = objSPBill

    '            arrTOPSPDetail.Add(objTOPSPTPdtl)
    '        End If
    '    Next

    '    dtgTOPSP.DataSource = arrTOPSPDetail
    '    dtgTOPSP.DataBind()
    'End Sub

    Private Sub percepatan(objDealer As Dealer)
        Dim objTOPSP As TOPSPTransferPayment
        Dim objTOPSPOld As TOPSPTransferPayment
        Dim objTOPSPdtl As TOPSPTransferPaymentDetail

        Dim objTOPSPfac As New TOPSPTransferPaymentFacade(User)
        Dim objTOPSPBillingFac As New SparePartBillingFacade(User)
        Dim arrDtl As New ArrayList
        Dim objreturn As TOPSPTransferPayment
        Dim intReturn As Integer

        Try

            objTOPSP = New TOPSPTransferPayment
            objTOPSPOld = CType(_sessHelper.GetSession("TOPSPTransferPayment"), TOPSPTransferPayment)

            intReturn = DateDiff(DateInterval.Day, calTanggalTransfer.Value, objTOPSPOld.TransferPlanDate) > 0

            If intReturn >= 0 Then
                MessageBox.Show("Tanggal Transfer Pemercepatan Harus Lebih Singkat")
                Return
            End If

            If DateDiff(DateInterval.Day, Now.Date, calTanggalTransfer.Value) < 0 Then
                MessageBox.Show("Tanggal Transfer Percepatan Minimal Harus Hari ini")
                Return
            End If

            intReturn = 0

            objTOPSP = objTOPSPOld

            objTOPSP.TransferPlanDate = calTanggalTransfer.Value
            objTOPSP.TOPSPTransferPaymentIDReff = objTOPSPOld.ID
            objTOPSP.IsAccelerated = 1
            objTOPSP.Status = CType(EnumStatusTOPSPTransferPayment.TOPSPStatus.Baru, Short)

            objreturn = objTOPSPfac.InsertReturnObject(objTOPSPOld, calTanggalTransfer.Value)

            If objreturn IsNot Nothing Then
                For Each rowdtg As DataGridItem In dtgTOPSP.Items

                    If rowdtg IsNot Nothing Then
                        objTOPSPdtl = New TOPSPTransferPaymentDetail

                        Dim lblBillingNumber As Label = CType(rowdtg.FindControl("lblBillingNumber"), Label)
                        Dim lblTOPSPID As Label = CType(rowdtg.FindControl("lblTOPSPID"), Label)
                        Dim lblRowStatus As Label = CType(rowdtg.FindControl("lblRowStatus"), Label)

                        If lblTOPSPID.Text <> "0" Then
                            objTOPSPdtl = New TOPSPTransferPaymentDetailFacade(User).Retrieve(lblBillingNumber.Text, objTOPSPOld.ID)
                            objTOPSPdtl.RowStatus = CType(lblRowStatus.Text, Short)
                            intReturn = New TOPSPTransferPaymentDetailFacade(User).Update(objTOPSPdtl)
                        Else
                            objTOPSPdtl.SparePartBilling = New SparePartBillingFacade(User).Retrieve(lblBillingNumber.Text)

                            Dim lblTotalAmountHide As Label = CType(rowdtg.FindControl("lblTotalAmountHide"), Label)
                            objTOPSPdtl.Amount = CType(lblTotalAmountHide.Text, Integer)

                            intReturn = New TOPSPTransferPaymentDetailFacade(User).Insert(objTOPSPdtl)
                        End If
                    End If
                Next
                lblNoRegPembayaran.Text = objreturn.RegNumber
                lblTanggalDibuat.Text = objreturn.CreatedTime.ToString("dd/MM/yyyy ss:mm:HH")

                'objTOPSPOld.Status = CType(EnumStatusTOPSPTransferPayment.TOPSPStatus.Batal, Integer)

                'If (objTOPSPfac.Update(objTOPSPOld)) > 0 Then
                '    MessageBox.Show("Data Berhasil Disimpan")
                'End If
                If intReturn > 0 Then
                    MessageBox.Show("Percepatan Pembayaran Berhasil")
                    ViewState("StatusTOPSP") = String.Empty
                    resetbuttonKembali(False)
                    'Response.Redirect("FrmTOPSPList.aspx")
                    'cleardata()
                End If

            End If

        Catch ex As Exception
            MessageBox.Show("Data Gagal Disimpan")
        End Try
    End Sub

    Private Sub baru(objDealer As Dealer)

        Dim objTOPSP As TOPSPTransferPayment
        Dim objTOPSPdtl As TOPSPTransferPaymentDetail
        Dim objTOPSPfac As New TOPSPTransferPaymentFacade(User)
        Dim objTOPSPBillingFac As New SparePartBillingFacade(User)
        Dim arrDtl As New ArrayList
        Dim objreturn As TOPSPTransferPayment
        Dim calDueDate As Date

        Try

            If String.IsNullOrEmpty(objDealer.CreditAccount.Trim) Then

                MessageBox.Show("Dealer Belum Memiliki Credit Account")
                Return


            Else
                Dim objvCa As v_CreditAccount
                objvCa = New v_CreditAccountFacade(User).Retrieve(objDealer.CreditAccount.Trim)
                If objvCa.ID = 0 Then
                    MessageBox.Show("Dealer Belum Memiliki Credit Account")
                    Return
                End If
            End If

            If dtgTOPSP.Items.Count < 1 Then
                MessageBox.Show("Silahkan Lengkapi Dahulu Billing")
                Return
            End If

            'If dtgTOPSP.Items.Count > 5 Then
            '    MessageBox.Show("Maksimum pengajuan 5 billing")
            '    Return
            'End If

            Dim objPaymentPurpose As PaymentPurpose = New PaymentPurposeFacade(User).Retrieve("SP")

            objTOPSP = New TOPSPTransferPayment
            objTOPSP.Dealer = objDealer
            objTOPSP.CreditAccount = objDealer.CreditAccount
            objTOPSP.PaymentPurposeID = objPaymentPurpose.ID 'CType(EnumStatusTOPSPTransferPayment.PaymentPurposeCode.SP, Integer)
            objTOPSP.TransferPlanDate = calTanggalTransfer.Value
            objTOPSP.TOPSPTransferPaymentIDReff = 0
            objTOPSP.IsAccelerated = 0
            objTOPSP.Status = 0
            objTOPSP.DueDate = calTanggalTransfer.Value
            objTOPSP.BankID = ddlBankTranfer.SelectedValue

            For Each row As DataGridItem In dtgTOPSP.Items
                objTOPSPdtl = New TOPSPTransferPaymentDetail

                Dim lblBillingNumber As Label = CType(row.FindControl("lblBillingNumber"), Label)
                Dim lblTOPSPID As Label = CType(row.FindControl("lblTOPSPID"), Label)
                Dim lblRowStatus As Label = CType(row.FindControl("lblRowStatus"), Label)

                If lblTOPSPID.Text = "0" Then
                    With objTOPSPdtl
                        .SparePartBilling = objTOPSPBillingFac.Retrieve(lblBillingNumber.Text)

                        calDueDate = New TOPSPDueDateFacade(User).Retrieve(.SparePartBilling.BillingNumber).DueDate

                        'If ((DateDiff(DateInterval.Day, calDueDate, Now.Date)) > 0) Then
                        '    MessageBox.Show("Tanggal DueDate Minimal Hari Ini")
                        '    Return
                        'End If

                        If (objTOPSPfac.GetTOPSPSparePartBilling(.SparePartBilling.ID)) > 0 Then
                            MessageBox.Show("Billing Number : " + .SparePartBilling.BillingNumber + " Sudah Digunakan Dipembayaran lainnya")
                            arrDtl = New ArrayList
                            Exit For
                        End If
                    End With
                End If

                Dim lblTotalAmountHide As Label = CType(row.FindControl("lblTotalAmountHide"), Label)
                objTOPSPdtl.Amount = CType(lblTotalAmountHide.Text, Double)

                If Val(lblRowStatus.Text) = CType(DBRowStatus.Active, Short) Then
                    arrDtl.Add(objTOPSPdtl)
                End If

            Next

            If arrDtl.Count > 0 Then
                objreturn = objTOPSPfac.InsertTOPSPTransferPayment(objTOPSP, arrDtl)
            Else
                MessageBox.Show("Silahkan Lengkapi Dahulu Billing")
                Return
            End If

            If objreturn IsNot Nothing Then
                MessageBox.Show("Data Berhasil Disimpan")

                objreturn = objTOPSPfac.Retrieve(objreturn.ID)

                lblNoRegPembayaran.Text = objreturn.RegNumber
                lblTanggalDibuat.Text = objreturn.CreatedTime.ToString("dd/MM/yyyy ss:mm:HH")
                ViewState("StatusTOPSP") = String.Empty
                resetbuttonKembali(False)
                'cleardata()
            End If

        Catch ex As Exception
            MessageBox.Show("Data Gagal Disimpan")
        End Try

    End Sub

    Private Sub baruEdit(objDealer As Dealer)

        Dim objTOPSP As TOPSPTransferPayment
        Dim objTOPSPdtl As TOPSPTransferPaymentDetail
        Dim objTOPSPfac As New TOPSPTransferPaymentFacade(User)
        Dim objTOPSPBillingFac As New SparePartBillingFacade(User)
        Dim arrDtl As New ArrayList
        Dim objreturn As Integer

        Try

            If dtgTOPSP.Items.Count < 1 Then
                MessageBox.Show("Silahkan Lengkapi Dahulu Billing")
                Return
            End If

            If dtgTOPSP.Items.Count < 1 Then
                MessageBox.Show("Silahkan Lengkapi Dahulu Billing")
                Return
            End If

            If ((DateDiff(DateInterval.Day, calTanggalTransfer.Value, Now.Date)) > 0) Then
                MessageBox.Show("Tanggal DueDate Minimal Hari Ini")
                Return
            End If

            Dim rowstatus As Boolean = False

            For Each row As DataGridItem In dtgTOPSP.Items
                Dim lblRowStatus As Label = CType(row.FindControl("lblRowStatus"), Label)
                If Val(lblRowStatus.Text) = CType(DBRowStatus.Active, Short) Then
                    rowstatus = True
                    Exit For
                End If
            Next

            If rowstatus = False Then
                MessageBox.Show("Silahkan Lengkapi Dahulu Billing")
                Return
            End If


            objTOPSP = New TOPSPTransferPayment

            objTOPSP = New TOPSPTransferPaymentFacade(User).Retrieve(lblNoRegPembayaran.Text)
            objTOPSP.DueDate = calTanggalTransfer.Value
            objTOPSP.TransferPlanDate = calTanggalTransfer.Value
            objreturn = New TOPSPTransferPaymentFacade(User).Update(objTOPSP)

            If objreturn > 0 Then
                objreturn = 0
                For Each row As DataGridItem In dtgTOPSP.Items
                    objTOPSPdtl = New TOPSPTransferPaymentDetail

                    Dim lblBillingNumber As Label = CType(row.FindControl("lblBillingNumber"), Label)
                    Dim lblTOPSPID As Label = CType(row.FindControl("lblTOPSPID"), Label)
                    Dim lblRowStatus As Label = CType(row.FindControl("lblRowStatus"), Label)

                    If lblTOPSPID.Text = "0" Then
                        With objTOPSPdtl
                            .SparePartBilling = objTOPSPBillingFac.Retrieve(lblBillingNumber.Text)
                            If (objTOPSPfac.GetTOPSPSparePartBilling(.SparePartBilling.ID)) > 0 Then
                                MessageBox.Show("Billing Number : " + .SparePartBilling.BillingNumber + " Sudah Digunakan Dipembayaran lainnya")
                                arrDtl = New ArrayList
                                Exit For
                            Else
                                Dim lblTotalAmountHide As Label = CType(row.FindControl("lblTotalAmountHide"), Label)
                                objTOPSPdtl.Amount = CType(lblTotalAmountHide.Text, Integer)
                                objTOPSPdtl.TOPSPTransferPayment = objTOPSP
                                objreturn = New TOPSPTransferPaymentDetailFacade(User).Insert(objTOPSPdtl)
                            End If
                        End With
                    Else

                        objTOPSPdtl = New TOPSPTransferPaymentDetailFacade(User).Retrieve(Val(lblTOPSPID.Text))
                        objTOPSPdtl.RowStatus = Val(lblRowStatus.Text)
                        objreturn = New TOPSPTransferPaymentDetailFacade(User).Update(objTOPSPdtl)
                    End If

                    'arrDtl.Add(objTOPSPdtl)
                Next
            End If
            'If arrDtl.Count > 0 Then
            '    objreturn = objTOPSPfac.UpdateTOPSPTransferPayment(objTOPSP, arrDtl)
            'End If

            If objreturn > 0 Then
                MessageBox.Show("Data Berhasil Disimpan")
                ViewState("StatusTOPSP") = String.Empty
                resetbuttonKembali(False)
                'cleardata()
            End If

        Catch ex As Exception
            MessageBox.Show("Data Gagal Disimpan")
        End Try

    End Sub

    Private Sub validasi()

        Dim objTOPSP As TOPSPTransferPayment
        Dim objTOPSPfac As New TOPSPTransferPaymentFacade(User)
        Dim objreturn As Integer

        Try

            objTOPSP = New TOPSPTransferPayment

            objTOPSP = CType(_sessHelper.GetSession("TOPSPTransferPayment"), TOPSPTransferPayment)
            objTOPSP.Status = CType(EnumStatusTOPSPTransferPayment.TOPSPStatus.Validasi, Integer)

            objreturn = objTOPSPfac.UpdateValidasi(objTOPSP)

            If objreturn > 0 Then
                MessageBox.Show("Data Berhasil Disimpan")
                ViewState("StatusTOPSP") = String.Empty
                resetbuttonKembali(False)
                'cleardata()
            End If


        Catch ex As Exception
            MessageBox.Show("Data Gagal Disimpan")
        End Try

    End Sub


    Private Sub BatalPembayaran()
        Dim objTOPSP As TOPSPTransferPayment
        Dim objTOPSPfac As New TOPSPTransferPaymentFacade(User)
        Dim objreturn As Integer

        Try

            objTOPSP = New TOPSPTransferPayment

            objTOPSP = CType(_sessHelper.GetSession("TOPSPTransferPayment"), TOPSPTransferPayment)
            objTOPSP.Status = CType(EnumStatusTOPSPTransferPayment.TOPSPStatus.Batal, Integer)

            objreturn = objTOPSPfac.UpdateBatalPembayaran(objTOPSP)

            If objreturn > 0 Then
                MessageBox.Show("Data Berhasil Disimpan")
                resetbuttonKembali(False)
                'cleardata()
            End If



        Catch ex As Exception
            MessageBox.Show("Data Gagal Disimpan, " + ex.Message)
        End Try

    End Sub

    Private Sub resetbuttonKembali(ByVal status As Boolean)
        btnSimpan.Visible = status
        btnPercepatan.Visible = status
        btnBatalPembayaran.Visible = status
        lbtnBillingNumber.Visible = status
        btnValidasi.Visible = status
        btnBaru.Visible = status
        btnKembali.Visible = Not status
    End Sub

    Private Sub createFileWSM()
        Dim FOLDER_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("DNetServerFolder")
        Dim cri As CriteriaComposite
        Dim oUI As UserInfo = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim _fileHelper As New FileHelper
        Dim objTOPSP As TOPSPTransferPayment
        Dim objTOPSPFac As New TOPSPTransferPaymentFacade(User)
        Dim objTOPSP1 As TOPSPTransferPayment
        Dim arrTOPSP As New List(Of TOPSPTransferPayment)
        Dim str As FileInfo

        Try
            If _sessHelper.GetSession("TOPSPTransferPayment") IsNot Nothing Then
                objTOPSP = CType(_sessHelper.GetSession("TOPSPTransferPayment"), TOPSPTransferPayment)

                Dim status As Boolean = True

                arrTOPSP = objTOPSPFac.up_RetrieveTOPSPTransferPaymentMultiPercepatan(objTOPSP.ID)

                Try
                    str = _fileHelper.TransferTOPSPTransferPaymenttoSAP(arrTOPSP)
                    MessageBox.Show(SR.UploadSucces(str.Name))

                Catch ex As Exception
                    MessageBox.Show(SR.UploadFail(str.Name))

                End Try
            End If

        Catch ex As Exception
            MessageBox.Show("Tidak ada data Pembayaran yang Terbentuk")
        End Try
    End Sub

    Private Sub ControlEnabler(Mode As String, arr As ArrayList)
        Dim objTOPSP As New TOPSPTransferPayment
        For Each rowdtl As TOPSPTransferPaymentDetail In arr
            objTOPSP = rowdtl.TOPSPTransferPayment
            _sessHelper.SetSession("TOPSPTransferPayment", objTOPSP)
            Exit For
        Next

        subStandardCode()

        Dim sesStatus As Short = objTOPSP.Status

        If Mode = "Edit" Then
            If sesStatus = EnumStatusTOPSPTransferPayment.TOPSPStatus.Baru And objTOPSP.IsAccelerated = 0 Then
                btnSimpan.Visible = True
                btnPercepatan.Visible = False
                btnBatalPembayaran.Visible = IsAuthorizedBatal
                'btnBatalPembayaran.Visible = True
                lbtnBillingNumber.Visible = True
                btnValidasi.Visible = IsAuthorizedValidasi
                'btnValidasi.Visible = True
                btnBaru.Visible = False
                btnKembali.Visible = True
                ViewState("StatusTOPSP") = "Edit"
            ElseIf sesStatus = EnumStatusTOPSPTransferPayment.TOPSPStatus.Baru And objTOPSP.IsAccelerated = 1 Then
                btnSimpan.Visible = False
                btnPercepatan.Visible = False
                btnBatalPembayaran.Visible = False
                lbtnBillingNumber.Visible = False
                btnValidasi.Visible = IsAuthorizedValidasi
                'btnValidasi.Visible = False
                btnBaru.Visible = False
                btnKembali.Visible = True
                ViewState("StatusTOPSP") = "Edit"
            ElseIf sesStatus = EnumStatusTOPSPTransferPayment.TOPSPStatus.Konfirmasi And objTOPSP.IsAccelerated = 1 Then
                btnSimpan.Visible = False
                btnPercepatan.Visible = False
                btnBatalPembayaran.Visible = False
                lbtnBillingNumber.Visible = False
                btnValidasi.Visible = IsAuthorizedValidasi
                'btnValidasi.Visible = True
                btnBaru.Visible = False
                btnKembali.Visible = True
            ElseIf sesStatus = EnumStatusTOPSPTransferPayment.TOPSPStatus.Validasi Then
                btnSimpan.Visible = False
                btnPercepatan.Visible = True
                btnBatalPembayaran.Visible = False
                lbtnBillingNumber.Visible = False
                btnValidasi.Visible = False
                btnBaru.Visible = False
                btnKembali.Visible = True
            ElseIf sesStatus = EnumStatusTOPSPTransferPayment.TOPSPStatus.Batal_Konfirmasi OrElse
                sesStatus = EnumStatusTOPSPTransferPayment.TOPSPStatus.Selesai OrElse
                sesStatus = EnumStatusTOPSPTransferPayment.TOPSPStatus.Batal OrElse
                (sesStatus = EnumStatusTOPSPTransferPayment.TOPSPStatus.Konfirmasi AndAlso objTOPSP.IsAccelerated = 0) Then
                btnSimpan.Visible = False
                btnPercepatan.Visible = False
                btnBatalPembayaran.Visible = False
                lbtnBillingNumber.Visible = False
                btnValidasi.Visible = False
                btnBaru.Visible = False
                btnKembali.Visible = True
            End If

            btnBaru.Visible = False
            'btnValidasi.Visible = False
        ElseIf Mode = "View" Then
            btnSimpan.Visible = False
            btnPercepatan.Visible = False
            btnBatalPembayaran.Visible = False
            lbtnBillingNumber.Visible = False
            btnValidasi.Visible = False
            btnBaru.Visible = False
            btnKembali.Visible = True
        End If
    End Sub

    Private Sub enableControl(ByVal status As Boolean)
        If IsAuthorizedUbah Then
            btnBaru.Visible = Not status
            btnSimpan.Visible = Not status
        Else
            btnBaru.Visible = status
            btnSimpan.Visible = status
        End If

        btnValidasi.Visible = status
        btnPercepatan.Visible = status
        btnBatalPembayaran.Visible = status
        btnKembali.Visible = status

    End Sub
#End Region

    Private Sub txtReturnBilling_TextChanged1(sender As Object, e As EventArgs) Handles txtReturnBilling.TextChanged
        Dim arrTOPSPDetail As New ArrayList
        Dim str As String()
        Dim strBillingID As String
        Dim objTOPSPTPdtl As TOPSPTransferPaymentDetail
        Dim objSPBill As SparePartBilling
        Dim objTOPSPDueDate As TOPSPDueDate
        Dim objSPBillFac As New SparePartBillingFacade(User)
        Dim cri As CriteriaComposite

        If String.IsNullOrEmpty(txtReturnBilling.Text) Then
            Return
        End If

        str = (txtReturnBilling.Text).Split(New Char() {";"c})


        For Each row As String In str
            If Not String.IsNullOrEmpty(row) Then

                cri = New CriteriaComposite(New Criteria(GetType(TOPSPDueDate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                objTOPSPTPdtl = New TOPSPTransferPaymentDetail
                objSPBill = New SparePartBilling
                objSPBill = objSPBillFac.Retrieve(row)

                objTOPSPTPdtl.SparePartBilling = objSPBill
                objTOPSPTPdtl.RowStatus = CType(DBRowStatus.Active, Short)

                arrTOPSPDetail.Add(objTOPSPTPdtl)

                cri.opAnd(New Criteria(GetType(TOPSPDueDate), "SparePartBilling.ID", MatchType.Exact, objSPBill.ID))


                Dim calDueDate As Date = CType((New TOPSPDueDateFacade(User).Retrieve(cri)).Item(0), TOPSPDueDate).DueDate
                calTanggalTransfer.Value = calDueDate
                lblTglJatuhTempo.Text = calDueDate.ToString("dd/MM/yyyy")

            End If
        Next

        If _sessHelper.GetSession("BillingSel") IsNot Nothing Then
            Dim arr As ArrayList
            arr = CType(_sessHelper.GetSession("BillingSel"), ArrayList)
            For Each row As TOPSPTransferPaymentDetail In arr
                arrTOPSPDetail.Add(row)
            Next
            _sessHelper.SetSession("BillingSel", arrTOPSPDetail)
        Else
            _sessHelper.SetSession("BillingSel", arrTOPSPDetail)
        End If


        dtgTOPSP.DataSource = arrTOPSPDetail
        dtgTOPSP.DataBind()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click

        Try
            objDealer = _sessHelper.GetSession("DEALER")
            Select Case ViewState("StatusTOPSP")
                Case "Baru"
                    baru(objDealer)
                Case "Percepatan"
                    percepatan(objDealer)
                Case "Edit"
                    baruEdit(objDealer)
            End Select

        Catch ex As Exception
            MessageBox.Show("Data Gagal Disimpan")
        End Try
    End Sub

    Private Sub btnPercepatan_Click(sender As Object, e As EventArgs) Handles btnPercepatan.Click
        ViewState("StatusTOPSP") = "Percepatan"
        calTanggalTransfer.Enabled = True

        enableControl(False)
        btnBaru.Visible = False
    End Sub

    Private Sub btnBatalPembayaran_Click(sender As Object, e As EventArgs) Handles btnBatalPembayaran.Click
        BatalPembayaran()
    End Sub

    Private Sub btnValidasi_Click(sender As Object, e As EventArgs) Handles btnValidasi.Click
        Dim al As ArrayList

        validasi()
        createFileWSM()
        enableControl(False)
        btnSimpan.Visible = False
        btnKembali.Visible = True
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("FrmTOPSPList.aspx")
    End Sub

End Class
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

#End Region

Public Class FrmTOPSPTransferPaymentKTB
    Inherits System.Web.UI.Page

#Region "Custom Variable Declaration"
    Private _sessHelper As SessionHelper = New SessionHelper
    Private objDealer As Dealer
    Dim detail As TOPSPTransferPaymentDetail
    Private Mode As String = String.Empty
    Dim IsAuthorizedValidasi As Boolean = False
    Private _sessData As String = "FrmTOPSPList._sessData"
    Private _sessBroadCast As String = "FrmTOPSPList._sessBroadCast"

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objDealer = _sessHelper.GetSession("DEALER")
        UserPrivilege()
        'lblCreditAccount.Text = objDealer.CreditAccount
        lblNamaDealer.Text = objDealer.DealerCode + " / " + objDealer.DealerName
        btnKonfirmasi.Visible = False
        btnTolak.Visible = False
        btnValidasi.Visible = False
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
            End If
        End If

        lblTotalTransfer.Text = ViewState("TotalTransfer")

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
                lblRowStatus.Text = CType(DBRowStatus.Active, Short)
                lblDealerCode.Text = RowValue.SparePartBilling.Dealer.DealerCode
                lblAmountBillTax.Text = (RowValue.SparePartBilling.TotalAmount + RowValue.SparePartBilling.Tax).ToString("N0")

                arr = New TOPSPDepositFacade(User).Retrieve(criDeposit)

                lblTOPSPID.Text = CType(RowValue.ID, Integer).ToString

                lblAmountC2.Text = CType(arr.Item(0), TOPSPDeposit).AmountC2.ToString("N0")
                lblTotalAmount.Text = (CType(arr.Item(0), TOPSPDeposit).AmountC2 + (RowValue.SparePartBilling.TotalAmount + RowValue.SparePartBilling.Tax)).ToString("N0")
                lblTotalAmountHide.Text = (CType(arr.Item(0), TOPSPDeposit).AmountC2 + (RowValue.SparePartBilling.TotalAmount + RowValue.SparePartBilling.Tax))
                valTransferTotal = valTransferTotal + CType(arr.Item(0), TOPSPDeposit).AmountC2 + (RowValue.SparePartBilling.TotalAmount + RowValue.SparePartBilling.Tax)
                ViewState("TotalTransfer") = valTransferTotal.ToString("N0")

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

#Region "Custom Method"

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
                ResetButtonKembali(False)
                'cleardata()
            End If


        Catch ex As Exception
            MessageBox.Show("Data Gagal Disimpan")
        End Try

    End Sub

    Private Sub UserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.Input_TOPSP_Transfer_Payment_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=TOPSparePart")
        End If
        Dim IsAuthorizedUbah As Boolean = SecurityProvider.Authorize(Context.User, SR.Input_TOPSP_Transfer_Payment_Privilege)
        Dim IsAuthorizedDownload As Boolean = SecurityProvider.Authorize(Context.User, SR.Input_TOPSP_Transfer_Payment_Privilege)
        IsAuthorizedValidasi = SecurityProvider.Authorize(Context.User, SR.TOPSP_Daftar_Transfer_Payment_Validasi_MMKSI_Privilege)
    End Sub

    Private Sub cleardata()
        calTanggalTransfer.Enabled = False
        dtgTOPSP.DataSource = New ArrayList

        Dim arrStdCode As ArrayList
        Dim cri As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cri.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumTOPSPTransferPayment.Status"))

        Dim objStdCodeFac As New StandardCodeFacade(User)
        arrStdCode = objStdCodeFac.Retrieve(cri)

        'ViewState("StatusTOPSP") = "Baru"

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
                    ViewState("StatusTOPSP") = "Baru"
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
                    lblCreditAccount.Text = objtopsp.Dealer.CreditAccount
                    Exit For
                End If
            Next
        End If
    End Sub


    Private Sub Konfirmasi(bolStatus As Boolean)
        Dim objTOPSP As TOPSPTransferPayment
        Dim objTOPSPfac As New TOPSPTransferPaymentFacade(User)
        Dim objreturn As Integer

        Try

            objTOPSP = New TOPSPTransferPayment

            objTOPSP = CType(_sessHelper.GetSession("TOPSPTransferPayment"), TOPSPTransferPayment)

            If bolStatus = True Then
                objTOPSP.Status = CType(EnumStatusTOPSPTransferPayment.TOPSPStatus.Konfirmasi, Integer)
                objTOPSP.TransferPlanDate = calTanggalTransfer.Value
                objreturn = objTOPSPfac.UpdateKonfirmasi(objTOPSP)
            Else
                objTOPSP.Status = CType(EnumStatusTOPSPTransferPayment.TOPSPStatus.Batal_Konfirmasi, Integer)
                objreturn = objTOPSPfac.UpdateBatalKonfirmasi(objTOPSP)
            End If

            If objreturn > 0 Then
                MessageBox.Show("Data Berhasil DiSimpan")
                ResetButtonKembali(False)
            End If
        Catch ex As Exception
            MessageBox.Show("Data Gagal DiKonfirmasi")
        End Try

    End Sub

    Private Sub ResetButtonKembali(ByVal status As Boolean)
        btnKonfirmasi.Visible = status
        btnTolak.Visible = status
        btnKembali.Visible = Not status
    End Sub

    'Private Sub BatalKonfirmasi()
    '    Dim objTOPSP As TOPSPTransferPayment
    '    Dim objTOPSPfac As New TOPSPTransferPaymentFacade(User)
    '    Dim objreturn As Integer

    '    Try

    '        objTOPSP = New TOPSPTransferPayment

    '        objTOPSP = CType(_sessHelper.GetSession("TOPSPTransferPayment"), TOPSPTransferPayment)
    '        objTOPSP.Status = CType(EnumStatusTOPSPTransferPayment.TOPSPStatus.Batal_Konfirmasi, Integer)

    '        objreturn = objTOPSPfac.UpdateBatalKonfirmasi(objTOPSP)

    '        If objreturn > 0 Then
    '            MessageBox.Show("Data Berhasil Disimpan")
    '            cleardata()
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show("Data Gagal Disimpan, " + ex.Message)
    '    End Try

    'End Sub

    Private Sub ControlEnabler(Mode As String, arr As ArrayList)
        Dim objTOPSP As New TOPSPTransferPayment
        For Each rowdtl As TOPSPTransferPaymentDetail In arr
            objTOPSP = rowdtl.TOPSPTransferPayment
            _sessHelper.SetSession("TOPSPTransferPayment", objTOPSP)
            Exit For
        Next

        subStandardCode()

        Dim sesStatus As Short = objTOPSP.Status
        Dim loginTitle As Short = objDealer.Title
        If Mode = "Edit" Then

            Select Case sesStatus
                Case EnumStatusTOPSPTransferPayment.TOPSPStatus.Baru
                    If objTOPSP.IsAccelerated = 1 Then
                        btnKonfirmasi.Visible = True
                        btnTolak.Visible = True
                        calTanggalTransfer.Enabled = True
                    End If
                Case EnumStatusTOPSPTransferPayment.TOPSPStatus.Konfirmasi
                    If objTOPSP.IsAccelerated = 1 Then
                        btnValidasi.Visible = IsAuthorizedValidasi
                    End If
                Case Else
                    btnKonfirmasi.Visible = False
                    btnTolak.Visible = False
                    btnValidasi.Visible = False
            End Select

            'If sesStatus = EnumStatusTOPSPTransferPayment.TOPSPStatus.Baru AndAlso objTOPSP.IsAccelerated = 1 Then
            '    btnKonfirmasi.Visible = True
            '    btnTolak.Visible = True
            '    calTanggalTransfer.Enabled = True
            '    btnValidasi.Visible = IsAuthorizedValidasi
            'Else
            '    btnKonfirmasi.Visible = False
            '    btnTolak.Visible = False
            '    btnValidasi.Visible = False
            'End If
        ElseIf Mode = "View" Then
            btnKonfirmasi.Visible = False
            btnTolak.Visible = False
            btnValidasi.Visible = False
        End If
    End Sub

    Private Sub enableControl(ByVal status As Boolean)
        btnValidasi.Visible = status
        btnKembali.Visible = status

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
#End Region

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("FrmTOPSPList.aspx")
    End Sub

    Private Sub btnKonfirmasi_Click(sender As Object, e As EventArgs) Handles btnKonfirmasi.Click
        Konfirmasi(True)
    End Sub

    Private Sub btnTolak_Click(sender As Object, e As EventArgs) Handles btnTolak.Click
        Konfirmasi(False)
    End Sub

    Private Sub btnValidasi_Click(sender As Object, e As EventArgs) Handles btnValidasi.Click
        Dim al As ArrayList

        validasi()
        createFileWSM()
        enableControl(False)

        btnKembali.Visible = True
    End Sub
End Class
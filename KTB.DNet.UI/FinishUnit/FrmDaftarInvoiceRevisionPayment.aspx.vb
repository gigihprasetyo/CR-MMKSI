#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports System.Globalization
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.WebCC
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade.PO
#End Region

Public Class FrmDaftarInvoiceRevisionPayment
    Inherits System.Web.UI.Page

#Region " Private fields "
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim arrRevisionPaymentHeader As New ArrayList  '-- List of invoice
    Private objDealer As Dealer
    Private IsKTB As Boolean
    Private IsEditDetail As Boolean = False
    Private IsKonfirmasiPriv As Boolean = False
    Private IsTransferPriv As Boolean = False
    Dim myculture As CultureInfo = New CultureInfo("ID-id")
#End Region

#Region " EventHandler "

    Private Sub FrmDaftarInvoiceRevisionPayment_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim objUser As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser.Dealer.Title = "1" Then
            IsKTB = True
        Else
            IsKTB = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"

        If Not IsPostBack Then
            objDealer = Session("DEALER")

            BindDropdownList()  '-- Bind dropdownlist
            BindListBoxList()
            ViewState("currSortColumn") = "CreatedTime"
            ViewState("currSortDirection") = Sort.SortDirection.DESC

            If IsKTB Then
                btnValidasi.Visible = False
                btnKonfirmasi.Visible = IsKonfirmasiPriv
                btnTransfer.Visible = IsTransferPriv
                btnTransferUlang.Visible = IsTransferPriv
                lblSearchCreditAccount.Attributes("onclick") = "ShowPPCreditAccountSelection('All');"
                txtCreditAccount.Enabled = True
                lblSearchCreditAccount.Visible = True
            Else
                btnValidasi.Visible = True
                btnKonfirmasi.Visible = False
                btnTransfer.Visible = False
                btnTransferUlang.Visible = False
                lblSearchCreditAccount.Attributes("onclick") = "ShowPPCreditAccountSelection('" & objDealer.DealerGroup.ID & "');"
                txtCreditAccount.Enabled = False
                lblSearchCreditAccount.Visible = False
                txtCreditAccount.Text = objDealer.CreditAccount
            End If

            SetCriteria()
            ReadData()   '-- Read all data matching criteria
            BindPage(dgInvoiceRevisionPaymentList.CurrentPageIndex)  '-- Bind page-1
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        storeCriteria()
        ReadData()   '-- Read all data matching criteria
        dgInvoiceRevisionPaymentList.CurrentPageIndex = 0
        BindPage(dgInvoiceRevisionPaymentList.CurrentPageIndex)  '-- Bind page-1
    End Sub

    Private Sub dgInvoiceRevisionPaymentList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgInvoiceRevisionPaymentList.SortCommand
        '-- Sort datagrid rows based on a column header clicked
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
        dgInvoiceRevisionPaymentList.CurrentPageIndex = 0
        ReadData()
        BindPage(dgInvoiceRevisionPaymentList.CurrentPageIndex)

    End Sub

    Private Sub dgInvoiceRevisionPaymentList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgInvoiceRevisionPaymentList.PageIndexChanged
        '-- Change datagrid page
        dgInvoiceRevisionPaymentList.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)
    End Sub

    Private Sub dgInvoiceRevisionPaymentList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgInvoiceRevisionPaymentList.ItemCommand

        If e.CommandName = "lnkDetail" Then
            storeCriteria()

            '-- Store the calling page
            sessHelp.SetSession("FrmEntryInvoiceRevisionPayment_CalledBy", "FrmDaftarInvoiceRevisionPayment.aspx")
            Response.Redirect("FrmEntryInvoiceRevisionPayment.aspx?id=" & e.Item.Cells(0).Text & "&mode=" & enumMode.Mode.ViewMode)

        ElseIf e.CommandName = "EditInvoiceRevisionPayment" Then
            If IsEditDetail = False Then
                MessageBox.Show("Maaf : Anda tidak punya akses edit pada modul : REVISI FAKTUR - INPUT PEMBAYARAN")
                Exit Sub
            End If
            storeCriteria()

            sessHelp.SetSession("FrmEntryInvoiceRevisionPayment_CalledBy", "FrmDaftarInvoiceRevisionPayment.aspx")
            Response.Redirect("FrmEntryInvoiceRevisionPayment.aspx?id=" & e.Item.Cells(0).Text & "&mode=" & enumMode.Mode.EditMode)

        ElseIf e.CommandName = "lnkDownloadBukti" Then
            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("FinishUnitFileDirectory") & "\" & e.CommandArgument
            Response.Redirect("../Download.aspx?file=" & PathFile)
        End If

    End Sub

    Private Sub dgInvoiceRevisionPaymentList_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgInvoiceRevisionPaymentList.ItemDataBound
        Dim RowValue As RevisionPaymentHeader = CType(e.Item.DataItem, RevisionPaymentHeader)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '-- Grid detail items

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (dgInvoiceRevisionPaymentList.CurrentPageIndex * dgInvoiceRevisionPaymentList.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

            'Add Revision Button
            Dim lblBankName As Label = CType(e.Item.FindControl("lblBankName"), Label)
            Dim lblGyroNo As Label = CType(e.Item.FindControl("lblGyroNo"), Label)
            Dim lblJVNo As Label = CType(e.Item.FindControl("lblJVNo"), Label)
            Dim lblActualDate As Label = CType(e.Item.FindControl("lblActualDate"), Label)
            Dim lblAmountActual As Label = CType(e.Item.FindControl("lblAmountActual"), Label)
            Dim lblPaymentStatus As Label = CType(e.Item.FindControl("lblPaymentStatus"), Label)
            Dim lblGyroDate As Label = CType(e.Item.FindControl("lblGyroDate"), Label)
            Dim lblPaymentType As Label = CType(e.Item.FindControl("lblPaymentType"), Label)
            Dim lblSelisihTagihan As Label = CType(e.Item.FindControl("lblSelisihTagihan"), Label)
            Dim lblTotalAmount As Label = CType(e.Item.FindControl("lblTotalAmount"), Label)
            Dim lblAmountKliring As Label = CType(e.Item.FindControl("lblAmountKliring"), Label)

            Dim JVNo As String = ""
            Dim AmountKliring As Double = 0
            Dim critNoTR As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IRAccDocNumber), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critNoTR.opAnd(New Criteria(GetType(IRAccDocNumber), "RevisionPaymentHeader.RegNumber", MatchType.Exact, RowValue.RegNumber))
            Dim arlNoTR As ArrayList = New IRAccDocNumberFacade(User).Retrieve(critNoTR)
            For Each noTR As IRAccDocNumber In arlNoTR
                If JVNo.Length = 0 Then
                    JVNo = noTR.TRNo
                Else
                    JVNo = JVNo & ", " & noTR.TRNo
                End If

                If CDbl(noTR.Amount) > 0 Then
                    AmountKliring = AmountKliring + noTR.Amount
                End If
            Next
            lblJVNo.Text = JVNo
            lblAmountKliring.Text = AmountKliring.ToString("#,#", myculture)


            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionTransferActual), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(RevisionTransferActual), "RevisionPaymentHeader.ID", MatchType.Exact, RowValue.ID))
            Dim arlActual As ArrayList = New RevisionTransferActualFacade(User).Retrieve(crit)
            Dim selisih As Integer = 0
            For Each actualTransfer As RevisionTransferActual In arlActual
                selisih = selisih + actualTransfer.Amount
            Next

            Dim selisihTagihan As Double = CDbl(selisih - RowValue.TotalAmount)
            If selisihTagihan < 0 Then
                lblSelisihTagihan.ForeColor = Color.Red
            End If
            lblSelisihTagihan.Text = selisihTagihan.ToString("#,#", myculture)

            lblAmountActual.Text = CDbl(selisih).ToString("#,#", myculture)

            lblTotalAmount.Text = CDbl(RowValue.TotalAmount).ToString("#,#", myculture)


            lblPaymentType.Text = GetPaymentTypeName(RowValue.PaymentType)
            'lblPaymentStatus.Text = GetPaymentRevisionStatusName(RowValue.Status)
            If CDbl(RowValue.TotalAmount) <= CDbl(selisih) Then
                lblPaymentStatus.Text = "Selesai"
            Else
                lblPaymentStatus.Text = "Proses"
            End If

            Dim lnkEdit As LinkButton = CType(e.Item.FindControl("lnkEdit"), LinkButton)
            Dim lnkDetail As LinkButton = CType(e.Item.FindControl("lnkDetail"), LinkButton)
            Dim lnkUploadBukti As Label = CType(e.Item.FindControl("lnkUploadBukti"), Label)
            Dim lnkDownloadBukti As LinkButton = CType(e.Item.FindControl("lnkDownloadBukti"), LinkButton)
            Dim hdnPaymentType As HiddenField = CType(e.Item.FindControl("hdnPaymentType"), HiddenField)
            Dim hdnEvidencePath As HiddenField = CType(e.Item.FindControl("hdnEvidencePath"), HiddenField)

            CType(e.Item.FindControl("lnkUploadBukti"), Label).Attributes.Add("onclick", "showPopUp('../General/../PopUp/PopUpUploadBuktiPembayaranInvRev.aspx?ID=" & RowValue.ID & "', '', 200, 500, isSuccesUpload);")

            If lblActualDate.Text.Trim = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, String) Then
                lblActualDate.Text = ""
            End If

            If lblGyroDate.Text.Trim = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, String) Then
                lblGyroDate.Text = ""
            End If

            If hdnPaymentType.Value <> enumPaymentTypeRevision.PaymentType.Transfer Then
                lnkUploadBukti.Visible = False
                lnkDownloadBukti.Visible = False
            Else
                lnkUploadBukti.Visible = True
                lnkDownloadBukti.Visible = False
                If RowValue.EvidencePath.Trim <> "" And IsKTB Then
                    lnkDownloadBukti.Visible = True
                    lnkUploadBukti.Visible = False
                End If
                If hdnEvidencePath.Value.Trim = "" Then
                    e.Item.Cells(14).BackColor = Color.Red
                Else
                    e.Item.Cells(14).BackColor = Color.Green
                End If
            End If

            Dim strSlipNumber() As String = RowValue.SlipNumber.Split(" ")
            Dim strGiroNo As String = String.Empty
            Dim oBank As Bank
            oBank = New PO.BankFacade(User).Retrieve(strSlipNumber(0).ToString)
            If Not IsNothing(oBank) Then
                lblBankName.Text = oBank.BankName
                If RowValue.SlipNumber.Trim.IndexOf(Chr(32)) <> -1 Then
                    strGiroNo = Trim(RowValue.SlipNumber.Substring(Len(strSlipNumber(0).ToString)))
                    lblGyroNo.Text = strGiroNo
                End If
            End If

            '-- After validasi button isEdit = false
            If RowValue.Status = New EnumDNET().enumPaymentFakturKendaraanRev.Baru Then
                lnkDetail.Visible = True
                lnkEdit.Visible = True
            Else
                lnkDetail.Visible = True
                lnkEdit.Visible = False
                If IsKTB Then
                    'Jika login KTB maka muncul editnya
                    If RowValue.Status = New EnumDNET().enumPaymentFakturKendaraanRev.Selesai Then
                        lnkEdit.Visible = True
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnValidasi_Click(sender As Object, e As EventArgs) Handles btnValidasi.Click
        Dim arl As ArrayList = New ArrayList
        Dim oRevisionPaymentHeader As RevisionPaymentHeader
        Dim arrDataVA As ArrayList = New ArrayList
        Dim strMessage As String = String.Empty

        For Each item As DataGridItem In dgInvoiceRevisionPaymentList.Items
            If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                Dim chkItemChecked As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
                If (chkItemChecked.Checked) Then
                    Dim lblPaymentRegNumber As Label = CType(item.FindControl("lblPaymentRegNumber"), Label)
                    Dim hdnPaymentStatusID As HiddenField = CType(item.FindControl("hdnPaymentStatusID"), HiddenField)
                    Dim lblPaymentStatus As Label = CType(item.FindControl("lblPaymentStatus"), Label)
                    Dim hdnID As HiddenField = CType(item.FindControl("hdnID"), HiddenField)
                    Dim hdnEvidencePath As HiddenField = CType(item.FindControl("hdnEvidencePath"), HiddenField)
                    Dim hdnPaymentType As HiddenField = CType(item.FindControl("hdnPaymentType"), HiddenField)

                    If hdnPaymentStatusID.Value <> New EnumDNET().enumPaymentFakturKendaraanRev.Baru Then
                        MessageBox.Show("Status No Registrasi " & lblPaymentRegNumber.Text & " : " & lblPaymentStatus.Text & ".\nTombol Validasi hanya untuk yang status payment = Baru")
                        BindPage(dgInvoiceRevisionPaymentList.CurrentPageIndex)
                        Return
                    Else
                    End If
                    If hdnPaymentType.Value = enumPaymentTypeRevision.PaymentType.Transfer Then
                        If hdnEvidencePath.Value.Trim = "" Then
                            MessageBox.Show("No Registrasi " & lblPaymentRegNumber.Text & " belum upload bukti pembayaran")
                            BindPage(dgInvoiceRevisionPaymentList.CurrentPageIndex)
                            Return
                        End If
                    End If

                    oRevisionPaymentHeader = New RevisionPaymentHeader
                    oRevisionPaymentHeader = New RevisionPaymentHeaderFacade(User).Retrieve(Convert.ToInt32(hdnID.Value))
                    oRevisionPaymentHeader.Status = New EnumDNET().enumPaymentFakturKendaraanRev.Validasi
                    arl.Add(oRevisionPaymentHeader)

                    If hdnPaymentType.Value = enumPaymentTypeRevision.PaymentType.Gyro OrElse
                        hdnPaymentType.Value = enumPaymentTypeRevision.PaymentType.Transfer OrElse
                        hdnPaymentType.Value = enumPaymentTypeRevision.PaymentType.Virtual_Account Then
                        arrDataVA.Add(oRevisionPaymentHeader)
                    End If
                End If
            End If
        Next

        If (arl.Count > 0) Then
            If (New RevisionPaymentHeaderFacade(User).UpdateRevisionPaymentHeaders(arl) = 1) Then
                If arrDataVA.Count > 0 Then
                    Try
                        Me.TransferVAToSAP(arrDataVA)
                    Catch ex As Exception
                        strMessage = ", Transfer data Virtual Account ke SAP Gagal"
                    End Try
                End If
                MessageBox.Show(SR.UpdateSucces & strMessage)
                btnSearch_Click(New Object(), New System.EventArgs)

                BindPage(dgInvoiceRevisionPaymentList.CurrentPageIndex)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        Else
            MessageBox.Show("Data Pembayaran belum di pilih")
        End If
    End Sub

    Private Sub btnSuccessUpload_Click(sender As Object, e As EventArgs) Handles btnSuccessUpload.Click
        ReadData()
        BindPage(dgInvoiceRevisionPaymentList.CurrentPageIndex)
    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        Dim arl As ArrayList = New ArrayList
        Dim oRevisionPaymentHeader As RevisionPaymentHeader
        Dim strMessage As String = String.Empty
        Dim arrDataGyro As ArrayList = New ArrayList
        Dim arrDataTransfer As ArrayList = New ArrayList

        For Each item As DataGridItem In dgInvoiceRevisionPaymentList.Items
            If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                Dim chkItemChecked As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
                If (chkItemChecked.Checked) Then
                    Dim lblPaymentRegNumber As Label = CType(item.FindControl("lblPaymentRegNumber"), Label)
                    Dim hdnPaymentStatusID As HiddenField = CType(item.FindControl("hdnPaymentStatusID"), HiddenField)
                    Dim hdnPaymentType As HiddenField = CType(item.FindControl("hdnPaymentType"), HiddenField)
                    Dim lblPaymentType As Label = CType(item.FindControl("lblPaymentType"), Label)
                    Dim lblPaymentStatus As Label = CType(item.FindControl("lblPaymentStatus"), Label)
                    Dim hdnID As HiddenField = CType(item.FindControl("hdnID"), HiddenField)

                    If hdnPaymentStatusID.Value <> New EnumDNET().enumPaymentFakturKendaraanRev.Konfirmasi Then
                        MessageBox.Show("Status No Registrasi " & lblPaymentRegNumber.Text & " : " & lblPaymentStatus.Text & ".\nTombol Transfer hanya untuk yang status payment = Konfirmasi")
                        BindPage(dgInvoiceRevisionPaymentList.CurrentPageIndex)
                        Return
                    Else
                    End If
                    oRevisionPaymentHeader = New RevisionPaymentHeader
                    oRevisionPaymentHeader = New RevisionPaymentHeaderFacade(User).Retrieve(Convert.ToInt32(hdnID.Value))
                    oRevisionPaymentHeader.Status = New EnumDNET().enumPaymentFakturKendaraanRev.Proses
                    arl.Add(oRevisionPaymentHeader)

                    If hdnPaymentType.Value = enumPaymentTypeRevision.PaymentType.Gyro Then
                        arrDataGyro.Add(oRevisionPaymentHeader)
                    End If

                    If hdnPaymentType.Value = enumPaymentTypeRevision.PaymentType.Transfer Then
                        arrDataTransfer.Add(oRevisionPaymentHeader)
                    End If
                End If
            End If
        Next

        If (arl.Count > 0) Then
            If (New RevisionPaymentHeaderFacade(User).UpdateRevisionPaymentHeaders(arl) = 1) Then
                If arrDataGyro.Count > 0 Then
                    Try
                        Me.TransferGiroDanTransferToSAP(arrDataGyro, "Gyro")
                    Catch ex As Exception
                        strMessage += ", Kirim data Gyro ke SAP Gagal"
                    End Try
                End If
                If arrDataTransfer.Count > 0 Then
                    Try
                        Me.TransferGiroDanTransferToSAP(arrDataTransfer, "Transfer")
                    Catch ex As Exception
                        strMessage += ", Kirim data Transfer ke SAP Gagal"
                    End Try
                End If

                MessageBox.Show(SR.UpdateSucces & strMessage)
                btnSearch_Click(New Object(), New System.EventArgs)

                BindPage(dgInvoiceRevisionPaymentList.CurrentPageIndex)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        Else
            MessageBox.Show("Data Pembayaran belum di pilih")
        End If
    End Sub

    Private Sub btnKonfirmasi_Click(sender As Object, e As EventArgs) Handles btnKonfirmasi.Click
        Dim arl As ArrayList = New ArrayList
        Dim oRevisionPaymentHeader As RevisionPaymentHeader
        Dim i As Integer = 0
        Dim Status As String = String.Empty

        For Each item As DataGridItem In dgInvoiceRevisionPaymentList.Items
            If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                Dim chkItemChecked As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
                If (chkItemChecked.Checked) Then
                    Dim lblPaymentRegNumber As Label = CType(item.FindControl("lblPaymentRegNumber"), Label)
                    Dim hdnPaymentStatusID As HiddenField = CType(item.FindControl("hdnPaymentStatusID"), HiddenField)
                    Dim lblPaymentStatus As Label = CType(item.FindControl("lblPaymentStatus"), Label)
                    Dim hdnID As HiddenField = CType(item.FindControl("hdnID"), HiddenField)

                    If hdnPaymentStatusID.Value <> New EnumDNET().enumPaymentFakturKendaraanRev.Validasi Then
                        MessageBox.Show("Status No Registrasi " & lblPaymentRegNumber.Text & " : " & lblPaymentStatus.Text & ".\nTombol Konfirmasi hanya untuk yang status payment = Validasi")
                        BindPage(dgInvoiceRevisionPaymentList.CurrentPageIndex)
                        Return
                    Else
                    End If
                    oRevisionPaymentHeader = New RevisionPaymentHeader
                    oRevisionPaymentHeader = New RevisionPaymentHeaderFacade(User).Retrieve(Convert.ToInt32(hdnID.Value))
                    oRevisionPaymentHeader.Status = New EnumDNET().enumPaymentFakturKendaraanRev.Konfirmasi
                    arl.Add(oRevisionPaymentHeader)
                End If
                i = i + 1
            End If
        Next

        If (arl.Count > 0) Then
            If (New RevisionPaymentHeaderFacade(User).UpdateRevisionPaymentHeaders(arl) = 1) Then
                MessageBox.Show("Update status sukses")
                btnSearch_Click(New Object(), New System.EventArgs)

                BindPage(dgInvoiceRevisionPaymentList.CurrentPageIndex)
            Else
                MessageBox.Show("Update status gagal")
            End If
        Else
            MessageBox.Show("Data Pembayaran belum di pilih")
        End If
    End Sub

    Private Sub btnTransferUlang_Click(sender As Object, e As EventArgs) Handles btnTransferUlang.Click
        Dim arl As ArrayList = New ArrayList
        Dim oRevisionPaymentHeader As RevisionPaymentHeader
        Dim i As Integer = 0
        Dim Status As String = String.Empty
        Dim arrDataGyro As ArrayList = New ArrayList
        Dim arrDataTransfer As ArrayList = New ArrayList
        'Dim arrDataVA As ArrayList = New ArrayList

        For Each item As DataGridItem In dgInvoiceRevisionPaymentList.Items
            If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                Dim chkItemChecked As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
                If (chkItemChecked.Checked) Then
                    Dim lblPaymentRegNumber As Label = CType(item.FindControl("lblPaymentRegNumber"), Label)
                    Dim hdnPaymentStatusID As HiddenField = CType(item.FindControl("hdnPaymentStatusID"), HiddenField)
                    Dim hdnPaymentType As HiddenField = CType(item.FindControl("hdnPaymentType"), HiddenField)
                    Dim lblPaymentType As Label = CType(item.FindControl("lblPaymentType"), Label)
                    Dim lblPaymentStatus As Label = CType(item.FindControl("lblPaymentStatus"), Label)
                    Dim hdnID As HiddenField = CType(item.FindControl("hdnID"), HiddenField)

                    If hdnPaymentStatusID.Value <> New EnumDNET().enumPaymentFakturKendaraanRev.Proses Then
                        MessageBox.Show("Status No Registrasi " & lblPaymentRegNumber.Text & " : " & lblPaymentStatus.Text & ".\nTombol Transfer Ulang hanya untuk yang status payment = Proses")
                        BindPage(dgInvoiceRevisionPaymentList.CurrentPageIndex)
                        Return
                    Else
                    End If
                    oRevisionPaymentHeader = New RevisionPaymentHeader
                    oRevisionPaymentHeader = New RevisionPaymentHeaderFacade(User).Retrieve(Convert.ToInt32(hdnID.Value))
                    arl.Add(oRevisionPaymentHeader)

                    If hdnPaymentType.Value = enumPaymentTypeRevision.PaymentType.Gyro Then
                        arrDataGyro.Add(oRevisionPaymentHeader)
                    End If

                    If hdnPaymentType.Value = enumPaymentTypeRevision.PaymentType.Transfer Then
                        arrDataTransfer.Add(oRevisionPaymentHeader)
                    End If

                    'If hdnPaymentType.Value = enumPaymentTypeRevision.PaymentType.Virtual_Account Then
                    '    arrDataVA.Add(oRevisionPaymentHeader)
                    'End If
                End If
                i = i + 1
            End If
        Next

        If (arl.Count > 0) Then
            If (New RevisionPaymentHeaderFacade(User).UpdateRevisionPaymentHeaders(arl) = 1) Then
                If arrDataGyro.Count > 0 Then
                    Try
                        Me.TransferGiroDanTransferToSAP(arrDataGyro, "Gyro")
                    Catch ex As Exception
                        MessageBox.Show("Kirim data Gyro ke SAP Gagal")
                        Return
                    End Try
                End If
                If arrDataTransfer.Count > 0 Then
                    Try
                        Me.TransferGiroDanTransferToSAP(arrDataTransfer, "Transfer")
                    Catch ex As Exception
                        MessageBox.Show("Kirim data Transfer ke SAP Gagal")
                        Return
                    End Try
                End If

                MessageBox.Show("Update status sukses")
                btnSearch_Click(New Object(), New System.EventArgs)

                BindPage(dgInvoiceRevisionPaymentList.CurrentPageIndex)
            Else
                MessageBox.Show("Update status gagal")
            End If
        Else
            MessageBox.Show("Data Pembayaran belum di pilih")
        End If
    End Sub

#End Region

#Region "Custom Method"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.RevisiFakturPembayaranLihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=REVISI FAKTUR - DAFTAR PEMBAYARAN")
        End If

        IsEditDetail = SecurityProvider.Authorize(Context.User, SR.RevisiFakturPembayaranEdit_Privilege)
        IsKonfirmasiPriv = SecurityProvider.Authorize(Context.User, SR.RevisiFakturPembayaranKonfirmasi_Privilege)
        IsTransferPriv = SecurityProvider.Authorize(Context.User, SR.RevisiFakturPembayaranTransfer_Privilege)

        Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)
    End Sub

    Private Sub BindDdlKategori()
        ddlKategori.Items.Clear()

        Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
        Dim sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection
        sortColl.Add(New Sort(GetType(Category), "ID", Sort.SortDirection.ASC))

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim arrCategory As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, companyCode))
        arrCategory = New CategoryFacade(User).Retrieve(criterias, sortColl)

        For Each item As Category In arrCategory
            ddlKategori.Items.Insert(0, New ListItem(item.CategoryCode, item.ID))
        Next
        ddlKategori.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
        ddlKategori.SelectedIndex = 0

    End Sub

    Private Sub BindDropdownList()
        ddlPaymentType.Items.Clear()
        ddlPaymentType.Items.Add(New ListItem("Silahkan Pilih", 0))
        For Each li As ListItem In enumPaymentTypeRevision.GetList
            ddlPaymentType.Items.Add(li)
        Next

        ddlPaymentProof.DataSource = New EnumDNET().RetrieveStatusDaftarDokumenPayment
        ddlPaymentProof.DataTextField = "NameType"
        ddlPaymentProof.DataValueField = "ValType"
        ddlPaymentProof.DataBind()
        ddlPaymentProof.Items.Insert(0, New ListItem("Silahkan Pilih", 0))

        BindDdlKategori()
    End Sub

    Private Sub BindListBoxList()
        lboxPaymentRevisionStatus.DataSource = New EnumDNET().RetrieveStatusPaymentFakturKendaraanRev
        lboxPaymentRevisionStatus.DataTextField = "NameType"
        lboxPaymentRevisionStatus.DataValueField = "ValType"
        lboxPaymentRevisionStatus.DataBind()
    End Sub

    Private Sub ReadData()
        '-- Read all data selected
        Dim strSql As String

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(RevisionPaymentHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '-- Credit Account
        If txtCreditAccount.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(RevisionPaymentHeader), "Dealer.CreditAccount", MatchType.InSet, "('" & txtCreditAccount.Text.Trim().Replace(";", "','") & "')"))
        End If

        '-- Dealer code
        If txtKodeDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(RevisionPaymentHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If

        '-- RegNumber Pembayaran
        If txtRegNumberPayment.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(RevisionPaymentHeader), "RegNumber", MatchType.[Partial], txtRegNumberPayment.Text.Trim()))
        End If

        '-- Jenis Pembayaran
        If ddlPaymentType.SelectedValue.Trim() <> "0" Then
            criterias.opAnd(New Criteria(GetType(RevisionPaymentHeader), "PaymentType", MatchType.Exact, ddlPaymentType.SelectedValue.Trim()))
        End If

        ''-- RegNumber Revisi faktur
        If txtRegNumberRevision.Text.Trim() <> "" Then
            strSql = New RevisionPaymentHeaderFacade(User).RetrievePaymentRevFakturByRegNumber(txtRegNumberRevision.Text.Trim())
            criterias.opAnd(New Criteria(GetType(RevisionPaymentHeader), "ID", MatchType.InSet, "(" & strSql & ")"))
        End If

        '-- Nomor Rangka
        If txtChassisNo.Text.Trim() <> "" Then
            strSql = New RevisionPaymentHeaderFacade(User).RetrievePaymentRevFakturByChassisNo(txtChassisNo.Text.Trim())
            criterias.opAnd(New Criteria(GetType(RevisionPaymentHeader), "ID", MatchType.InSet, "(" & strSql & ")"))
        End If

        '-- Bukti Pembayaran
        If ddlPaymentProof.SelectedValue.Trim() <> "" Then
            If ddlPaymentProof.SelectedValue = New EnumDNET().enumDaftarDokumenPayment.Sudah_Upload Then
                criterias.opAnd(New Criteria(GetType(RevisionPaymentHeader), "EvidencePath", MatchType.No, String.Empty))

            ElseIf ddlPaymentProof.SelectedValue = New EnumDNET().enumDaftarDokumenPayment.Belum_Upload Then
                criterias.opAnd(New Criteria(GetType(RevisionPaymentHeader), "EvidencePath", MatchType.Exact, String.Empty))
            Else
            End If
        End If

        '-- Status Pembayaran
        If lboxPaymentRevisionStatus.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxPaymentRevisionStatus)
            criterias.opAnd(New Criteria(GetType(RevisionPaymentHeader), "Status", MatchType.InSet, "(" & SelectedStatus & ")"))
        End If

        If ddlKategori.SelectedIndex <> 0 Then
            Dim sqlRF As String = "select distinct a.RevisionPaymentHeaderID from RevisionPaymentDetail a join RevisionFaktur b on a.RevisionFakturID = b.ID "
            sqlRF += " and b.RowStatus=0 join ChassisMaster c on b.ChassisMasterID = c.ID and c.RowStatus=0 "
            sqlRF += " join Category d on c.CategoryID = d.ID and d.RowStatus=0 "
            sqlRF += " where a.RowStatus=0 and d.ID = '" & ddlKategori.SelectedValue & "'"

            criterias.opAnd(New Criteria(GetType(RevisionPaymentHeader), "ID", MatchType.InSet, "(" & sqlRF & ")"))
        End If

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            criterias.opAnd(New Criteria(GetType(RevisionPaymentHeader), "Status", MatchType.No, CType(New EnumDNET().enumPaymentFakturKendaraanRev.Baru, Integer)))
        ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(RevisionPaymentHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(RevisionPaymentHeader), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        Dim arrRevisionPaymentHeader As ArrayList = New RevisionPaymentHeaderFacade(User).RetrieveByCriteria(criterias, sortColl)
        sessHelp.SetSession("arrRevisionPaymentHeader", arrRevisionPaymentHeader)
        If arrRevisionPaymentHeader.Count > 0 Then
            btnValidasi.Enabled = True
        Else
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
            btnValidasi.Enabled = False
        End If
    End Sub

    Private Sub SetCriteria()
        Dim crit As Hashtable = New Hashtable
        crit = CType(Session("CriteriaFormRevisionPayment"), Hashtable)
        If Not crit Is Nothing Then
            txtCreditAccount.Text = CStr(crit.Item("CreditAccount"))
            txtKodeDealer.Text = CStr(crit.Item("DealerCode"))
            txtRegNumberPayment.Text = CStr(crit.Item("RegNumberPayment"))
            txtRegNumberRevision.Text = CStr(crit.Item("RegNumberRevision"))
            ddlPaymentType.SelectedValue = CStr(crit.Item("PaymentType"))
            ddlPaymentProof.SelectedValue = CStr(crit.Item("PaymentProof"))
            txtChassisNo.Text = CStr(crit.Item("ChassisNo"))
            ddlKategori.SelectedValue = CStr(crit.Item("Category"))

            lboxPaymentRevisionStatus.Items(0).Selected = CType(crit("Baru"), Boolean)
            lboxPaymentRevisionStatus.Items(1).Selected = CType(crit("Validasi"), Boolean)
            lboxPaymentRevisionStatus.Items(2).Selected = CType(crit("Konfirmasi"), Boolean)
            lboxPaymentRevisionStatus.Items(3).Selected = CType(crit("Proses"), Boolean)
            lboxPaymentRevisionStatus.Items(4).Selected = CType(crit("Selesai"), Boolean)

            dgInvoiceRevisionPaymentList.CurrentPageIndex = CInt(crit.Item("PageIndex"))
        End If
    End Sub

    Private Sub storeCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("CreditAccount", txtCreditAccount.Text)
        crit.Add("DealerCode", txtKodeDealer.Text)
        crit.Add("RegNumberPayment", txtRegNumberPayment.Text)
        crit.Add("RegNumberRevision", txtRegNumberRevision.Text)
        crit.Add("ChassisNo", txtChassisNo.Text)
        crit.Add("PaymentType", ddlPaymentType.SelectedValue)
        crit.Add("PaymentProof", ddlPaymentProof.SelectedValue)
        crit.Add("Category", ddlKategori.SelectedValue)
        crit.Add("Baru", lboxPaymentRevisionStatus.Items(0).Selected)
        crit.Add("Validasi", lboxPaymentRevisionStatus.Items(1).Selected)
        crit.Add("Konfirmasi", lboxPaymentRevisionStatus.Items(2).Selected)
        crit.Add("Proses", lboxPaymentRevisionStatus.Items(3).Selected)
        crit.Add("Selesai", lboxPaymentRevisionStatus.Items(4).Selected)
        crit.Add("PageIndex", dgInvoiceRevisionPaymentList.CurrentPageIndex)

        sessHelp.SetSession("CriteriaFormRevisionPayment", crit)
    End Sub

    Private Function GetSelectedItem(ByVal listboxStatus As ListBox) As String
        '-- Items selected in listbox

        Dim _strStatus As String = String.Empty
        For Each item As ListItem In listboxStatus.Items
            If item.Selected Then
                If _strStatus = String.Empty Then
                    _strStatus = item.Value
                Else
                    _strStatus = _strStatus & "," & item.Value
                End If
            End If
        Next
        Return _strStatus
    End Function

    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim arrRevisionPaymentHeader As ArrayList = CType(sessHelp.GetSession("arrRevisionPaymentHeader"), ArrayList)
        If arrRevisionPaymentHeader.Count <> 0 Then
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrRevisionPaymentHeader, pageIndex, dgInvoiceRevisionPaymentList.PageSize)
            dgInvoiceRevisionPaymentList.DataSource = PagedList
            dgInvoiceRevisionPaymentList.VirtualItemCount = arrRevisionPaymentHeader.Count()
            dgInvoiceRevisionPaymentList.DataBind()
        Else
            dgInvoiceRevisionPaymentList.DataSource = New ArrayList
            dgInvoiceRevisionPaymentList.VirtualItemCount = 0
            dgInvoiceRevisionPaymentList.CurrentPageIndex = 0
            dgInvoiceRevisionPaymentList.DataBind()
        End If
        If dgInvoiceRevisionPaymentList.VirtualItemCount > 0 Then
            lblJumRecord.Text = "Jumlah record : " & dgInvoiceRevisionPaymentList.VirtualItemCount
        End If
    End Sub

    Public Function GetPaymentTypeName(ByVal intID As Integer) As String
        If intID > -1 Then
            Return ([Enum].Parse(GetType(enumPaymentTypeRevision.PaymentType), intID, True)).ToString
        Else
            Return String.Empty
        End If
    End Function

    Public Function GetPaymentRevisionStatusName(ByVal intID As Integer) As String
        If intID > -1 Then
            Return ([Enum].Parse(GetType(EnumDNET.enumPaymentFakturKendaraanRev), intID, True)).ToString
        Else
            Return String.Empty
        End If
    End Function


    Private Sub TransferGiroDanTransferToSAP(ByVal arlToTransfer As ArrayList, ByVal sDataType As String)
        If arlToTransfer.Count < 1 Then
            MessageBox.Show("Tidak ada data yg ditransfer")
            Exit Sub
        End If

        Dim _fileHelper As New FileHelper
        Dim str As FileInfo
        Dim PreFolder As String = "IR"
        Try
            str = _fileHelper.TransferGyroIRToSAP(sDataType, arlToTransfer, PreFolder)
        Catch ex As Exception
            MessageBox.Show(SR.UploadFail(str.Name))
        End Try
    End Sub

    Private Sub TransferVAToSAP(ByVal arlToTransfer As ArrayList)
        If arlToTransfer.Count < 1 Then
            MessageBox.Show("Tidak ada data yg ditransfer")
            Exit Sub
        End If

        Dim _fileHelper As New FileHelper()
        Dim str As FileInfo
        Dim PreFolder As String = "IRTransferPayment"
        Try
            str = _fileHelper.TransferVAIRToSAP(arlToTransfer, PreFolder)
        Catch ex As Exception
            MessageBox.Show(SR.UploadFail(str.Name))
        End Try
    End Sub

    Private Function IsExist(ByRef aDPH As ArrayList, ByVal DPHID As Integer) As Boolean
        For Each oDPH As DailyPaymentHeader In aDPH
            If oDPH.ID = DPHID Then Return True
        Next
        Return False
    End Function


#End Region

End Class
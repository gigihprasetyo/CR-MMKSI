#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
Imports KTB.DNet.BusinessValidation.Helpers
#End Region

Public Class FrmDataSiswaBerbayar
    Inherits DepositBHelper

    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page, "Training After Sales - Daftar Siswa Berbayar")
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)

    Private Sub TitleDescription(ByVal areaid As String)
        If areaid.Equals("1") Then
            lblPageTitle.Text = "Training Sales - Daftar Siswa Berbayar"
            hdnCategory.Value = "sales"
        ElseIf areaid.Equals("2") Then
            lblPageTitle.Text = "Training After Sales - Daftar Siswa Berbayar"
            hdnCategory.Value = "ass"
        ElseIf areaid.Equals("3") Then
            lblPageTitle.Text = "Training Customer Satisfaction - Daftar Siswa Berbayar"
            hdnCategory.Value = "cs"
        Else
            lblPageTitle.Text = "Training - Daftar Siswa Berbayar"
            hdnCategory.Value = "ass"
        End If
    End Sub

    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property

    Private ReadOnly Property RequestID As String
        Get
            Return Request.QueryString("requestid")
        End Get
    End Property

    Private ReadOnly Property ReadOnlyForm As String
        Get
            Return Request.QueryString("readonly")
        End Get
    End Property

    Private ReadOnly Property IsApprove As String
        Get
            Return Request.QueryString("IsApprove")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsDealer() Then
            helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditSiswaBerbayar_Privilege)
            'helpers.Privilage()
        Else
            helpers = New TrainingHelpers(Me.Page, "Training After Sales - Daftar Tagihan")
            helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.view, SR.TrainingAssViewDaftarTagihan_Privilege)
            helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditDaftarTagihan_Privilege)
            'helpers.Privilage()
        End If
        If Not IsPostBack Then
            TitleDescription(AreaId)
            If ReadOnlyForm.Equals("1") Then
                SetDataTagihan()
                btnApprove.Visible = False
                trInfo.Visible = False
            Else
                If Not IsApprove.IsNullorEmpty Then
                    MessageBox.Show("Data berhasil di Approve.")
                End If

                SetDataSiswa(e)
                Dim criterias As New CriteriaComposite(New Criteria(GetType(Reference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(Reference), "Type", MatchType.Exact, "TRASS"))
                criterias.opAnd(New Criteria(GetType(Reference), "Code", MatchType.Exact, "RINF4"))

                Dim dataRef As Reference = CType(New ReferenceFacade(User).Retrieve(criterias)(0), Reference)
                cbxInfo.Text = dataRef.Description
                btnKembali.Visible = False
            End If

        End If
    End Sub

    Private Sub SetDataTagihan()
        trddlTahunFiscal.Visible = False
        lblPageTitle.Text = lblPageTitle.Text.Replace("Daftar Siswa Berbayar", "Detail Tagihan")
        Dim data As TrBillingHeader = New TrBillingHeaderFacade(User).Retrieve(CInt(RequestID))
        Dim listData As List(Of TrBookingCourse) = New List(Of TrBookingCourse)
        If Not data.ID.Equals(0) Then
            txtTahunFiskal.Text = data.FiscalYear
            txtDealerCode.Text = String.Format("{0} - {1}", data.Dealer.DealerCode, data.Dealer.DealerName)
            txtTotalPrice.Text = data.TotalPrice.AddThousandDelimiter()
            txtVoucher.Text = data.TotalVoucher.AddThousandDelimiter()
            txtppn.Text = data.PPN.AddThousandDelimiter()
            txtTotal.Text = data.Total.AddThousandDelimiter()
            txtReqID.Text = data.RequestID
            txtPaymenttype.Text = [Enum].GetName(GetType(EnumTagihanTraining.TipePembayaran), data.PaymentType).Replace("_", " ")
            txtStatus.Text = [Enum].GetName(GetType(EnumTagihanTraining.TagihanStatus), data.Status).Replace("_", " ")
            If Not CType(helpers.GetSession("Dealer"), Dealer).Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                Select Case data.Status
                    Case CType(EnumTagihanTraining.TagihanStatus.Konfirmasi, Short)
                        btnSimpan.ActiveControl()
                    Case CType(EnumTagihanTraining.TagihanStatus.Pembayaran_Transfer, Short)
                        btnSimpan.ActiveControl()
                    Case CType(EnumTagihanTraining.TagihanStatus.Pencairan_Deposit_B, Short)
                        btnSimpan.ActiveControl()
                    Case CType(EnumTagihanTraining.TagihanStatus.Disetujui, Short)
                        btnSimpan.ActiveControl()
                    Case CType(EnumTagihanTraining.TagihanStatus.Validasi, Short)
                        tdUpload.NonActiveControl()
                        td1.NonActiveControl()
                End Select
            Else
                lbnDelete.NonActiveControl()
                tdUpload.NonActiveControl()
            End If

            BindddlDealerGroup(data.DealerCode)
            ddlDealerGroup.Enabled = False
            Label2.Visible = False
            FileSuratKuasa.Visible = False
            extDoc.Visible = False

            If String.IsNullorEmpty(data.PathFaktur) Then
                trUpload.ActiveControl()
                btnSimpan.Enabled = False
            Else
                trDownload.ActiveControl()
                btnSimpan.Enabled = True
                hdnFilePath.Value = data.PathFaktur
            End If

            If String.IsNullorEmpty(data.PathDebitNote) Then
                trUploadDN.ActiveControl()
            Else
                trDownloadDN.ActiveControl()
                hdnFilePath2.Value = data.PathDebitNote
            End If

            For Each item As TrBillingDetail In New TrBillingHeaderFacade(User).RetrievePaymentDetail(data.ID)
                listData.Add(item.TrBookingCourse)
            Next

            dtgBooking.DataSource = listData.OrderBy(Function(x) x.TrClassRegistration.TrClass.ClassCode).ToList()
            dtgBooking.DataBind()

            trReqId.ActiveControl()
            trStatus.ActiveControl()
            trPaymenttype.ActiveControl()
            tr1.NonActiveControl()
            tr2.NonActiveControl()
            txtStatus.Disabled()
            txtTahunFiskal.Disabled()
            txtDealerCode.Disabled()
            txtReqID.Disabled()
            txtTotalPrice.Disabled()
            txtVoucher.Disabled()
            txtppn.Disabled()
            txtTotal.Disabled()
            txtPaymenttype.Disabled()
            lblNotifSaldo.NonVisible()
            lblNotifPlafon.NonVisible()
            lblNominalPencairan.NonVisible()
            If Not helpers.IsEdit Then
                btnApprove.Visible = False
                btnSimpan.Visible = False
                btnSubmit.Visible = False
                lbtnDeleteDN.Visible = False
                lbnDelete.Visible = False
                btnUpload.Visible = False
                btnUploadDN.Visible = False
            End If
            If Not btnSimpan.Visible Then
                lbtnDeleteDN.Visible = False
                lbnDelete.Visible = False
                btnUpload.Visible = False
                btnUploadDN.Visible = False
                FileDN.Visible = False
                photoSrc.Visible = False
            End If
        End If
    End Sub

    Private Sub BindddlDealerGroup(Optional dealerCode As String = "")
        Dim dealer As Dealer
        Dim func As New DealerFacade(Me.User)

        If dealerCode.IsNullorEmpty Then
            dealer = Me.GetDealer()
        Else
            dealer = func.Retrieve(dealerCode)
        End If

        Dim arrDealer As ArrayList = func.GetDealerByGroupID(dealer.DealerGroup.ID)
        ddlDealerGroup.ClearSelection()
        ddlDealerGroup.Items.Clear()
        For Each iDealer As Dealer In arrDealer
            ddlDealerGroup.Items.Add(New ListItem(iDealer.DealerCode, iDealer.DealerCode))
        Next
        ddlDealerGroup.SelectedValue = dealer.DealerCode

    End Sub

    Private Sub SetDataSiswa(ByVal e As System.EventArgs)
        trTxtTahunFiscal.Visible = False
        Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
        Dim funcB As TrBookingCourseFacade = New TrBookingCourseFacade(User)

        BindddlDealerGroup()

        txtDealerCode.Text = String.Format("{0} - {1}", dealer.DealerCode, dealer.DealerName)
        txtDealerCode.Disabled()
        ddlTahunFiscal.ClearSelection()
        ddlTahunFiscal.Items.Clear()
        If DateTime.Now.Month < 4 Then
            ddlTahunFiscal.Items.Add(String.Format("{0}/{1}", (DateTime.Now.Year - 1 - 1), (DateTime.Now.Year - 1)))
            ddlTahunFiscal.Items.Add(String.Format("{0}/{1}", (DateTime.Now.Year - 1), (DateTime.Now.Year)))
            ddlTahunFiscal.Items.Add(String.Format("{0}/{1}", (DateTime.Now.Year), (DateTime.Now.Year + 1)))
            ddlTahunFiscal.SelectedValue = String.Format("{0}/{1}", (DateTime.Now.Year - 1), (DateTime.Now.Year))
            txtTahunFiskal.Text = String.Format("{0}/{1}", (DateTime.Now.Year - 1), (DateTime.Now.Year))
        Else
            ddlTahunFiscal.Items.Add(String.Format("{0}/{1}", (DateTime.Now.Year - 1), (DateTime.Now.Year)))
            ddlTahunFiscal.Items.Add(String.Format("{0}/{1}", (DateTime.Now.Year), (DateTime.Now.Year + 1)))
            ddlTahunFiscal.Items.Add(String.Format("{0}/{1}", (DateTime.Now.Year + 1), (DateTime.Now.Year + 1 + 1)))
            ddlTahunFiscal.SelectedValue = String.Format("{0}/{1}", (DateTime.Now.Year), (DateTime.Now.Year + 1))
            txtTahunFiskal.Text = String.Format("{0}/{1}", (DateTime.Now.Year), (DateTime.Now.Year + 1))
        End If
        txtTahunFiskal.Disabled()

        InitData(ddlTahunFiscal.SelectedValue, e)
    End Sub

    Private Sub InitData(ByVal tahunFiscal As String, ByVal e As System.EventArgs)
        Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
        Dim funcB As TrBookingCourseFacade = New TrBookingCourseFacade(User)

        Dim arrTahunfiscal As String() = tahunFiscal.Split("/")
        Dim saldoDepositB As SaldoDepositB = funcB.Get_SaldoDepositB(dealer.ID, CInt(arrTahunfiscal(0)), 1) 'MMC
        lblNotifSaldo.Text = "Saldo Deposit B : Rp. " + saldoDepositB.SDepositB.ToString().AddThousandDelimiter
        lblNotifPlafon.Text = "Plafon Deposit B : Rp. " + saldoDepositB.Plafon.ToString().AddThousandDelimiter
        If saldoDepositB.SisaSaldo < 0 Then
            lblNominalPencairan.Text = "Transaksi Nominal Pencairan : Rp. " + "0"
        Else
            lblNominalPencairan.Text = "Transaksi Nominal Pencairan : Rp. " + saldoDepositB.SisaSaldo.ToString().AddThousandDelimiter
        End If

        Dim crt As New CriteriaComposite(New Criteria(GetType(TrBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crt.opAnd(New Criteria(GetType(TrBillingDetail), "TrBillingHeader.Dealer.ID", MatchType.Exact, dealer.ID))
        crt.opAnd(New Criteria(GetType(TrBillingDetail), "TrBillingHeader.FiscalYear", MatchType.Exact, tahunFiscal))

        Dim dataApproved As List(Of String) = New TrBillingDetailFacade(User).Retrieve(crt).Cast(Of TrBillingDetail).Select( _
            Function(x) x.TrBookingCourse.ID.ToString()).ToList()

        Dim idSetIn As String = dataApproved.GenerateInSet(False)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.ID", MatchType.Exact, dealer.ID))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, tahunFiscal))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrCourse.PaymentType", MatchType.Exact, 2))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistration.ID", MatchType.IsNotNull, Nothing))
        If Not dataApproved.Count.Equals(0) Then
            criterias.opAnd(New Criteria(GetType(TrBookingCourse), "ID", MatchType.NotInSet, idSetIn))
        End If

        Dim dataSiswa As List(Of TrBookingCourse) = New TrBookingCourseFacade(User).Retrieve(criterias).Cast(Of  _
            TrBookingCourse).ToList()
        If dataSiswa.Count.Equals(0) Then
            btnApprove.Visible = False
        Else
            btnApprove.Visible = True
            btnApprove.Enabled = False
        End If

        dtgBooking.DataSource = dataSiswa.OrderBy(Function(x) x.TrClassRegistration.TrClass.StartDate).ToList()
        dtgBooking.DataBind()

        Dim totalPrice As Double = 0
        Dim voucher As Double = 0


        Dim criteria As New CriteriaComposite(New Criteria(GetType(TrFreePass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(TrFreePass), "Dealer.ID", MatchType.Exact, dealer.ID))
        criteria.opAnd(New Criteria(GetType(TrFreePass), "FiscalYear", MatchType.Exact, tahunFiscal))

        txtTotalPrice.Text = totalPrice.AddThousandDelimiter()
        txtVoucher.Text = voucher.ToString()
        txtTotal.Text = (totalPrice - voucher).AddThousandDelimiter()

        Dim arrDataVoucher As ArrayList = New TrFreePassFacade(User).Retrieve(criteria)
        If arrDataVoucher.Count > 0 Then
            Dim dataVoucher As TrFreePass = CType(arrDataVoucher(0), TrFreePass)
            txJumlahVoucher.Text = dataVoucher.Qty.ToString()
            txSisaVoucher.Text = (dataVoucher.Qty - dataVoucher.QtyUsed).ToString()

            If CInt(txJumlahVoucher.Text) > 0 Then
                GridCalculation()
            End If
        Else
            txJumlahVoucher.Text = "0"
            txSisaVoucher.Text = "0"
        End If

        Dim isCheck As Boolean = False
        Dim ts As TransactionControl = New DealerFacade(Me.User).RetrieveTransactionControl(Me.GetDealer.ID, CType(EnumDealerTransType.DealerTransKind.DataSiswaBerbayar, String))
        If Not IsNothing(ts) Then
            If ts.Status = CType(EnumDealerStatus.DealerStatus.NonAktive, Short) Then
                isCheck = True
            End If
        End If

        For Each i As DataGridItem In dtgBooking.Items
            Dim chkItem As CheckBox = i.FindControl("chkItem")
            chkItem.Checked = True
            chkItem_CheckedChanged(chkItem, e)
            chkItem.Enabled = isCheck
        Next

        txtTotalPrice.ReadOnly = True
        txtVoucher.ReadOnly = True
        txtTotal.ReadOnly = True
        txtppn.ReadOnly = True
        txtTotalPrice.BackColor = Color.LightGray
        txtVoucher.BackColor = Color.LightGray
        txtTotal.BackColor = Color.LightGray
        txtppn.BackColor = Color.LightGray
        txJumlahVoucher.BackColor = Color.LightGray
        txSisaVoucher.BackColor = Color.LightGray
    End Sub

    Private Sub dtgBooking_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgBooking.ItemDataBound
        If ReadOnlyForm.Equals("1") Then
            e.Item.Cells(0).Visible = False
        End If
        If e.Item.DataItem IsNot Nothing Then
            Dim data As TrBookingCourse = e.DataItem(Of TrBookingCourse)()
            Dim lblNo As Label = e.FindLabel("lblNo") 'CType(e.Item.FindControl("lblNo"), Label)
            Dim lblNoReg As Label = e.FindLabel("lblNoReg") 'CType(e.Item.FindControl("lblNoReg"), Label)
            Dim lblNamaSiswa As Label = e.FindLabel("lblNamaSiswa")
            Dim lblPosisi As Label = e.FindLabel("lblPosisi") 'CType(e.Item.FindControl("lblPosisi"), Label)
            Dim lblKategori As Label = CType(e.Item.FindControl("lblKategori"), Label)
            Dim lblPaidDay As Label = CType(e.Item.FindControl("lblPaidDay"), Label)
            Dim lblPricePerDay As Label = CType(e.Item.FindControl("lblPricePerDay"), Label)
            Dim lblTotalPrice As Label = CType(e.Item.FindControl("lblTotalPrice"), Label)
            Dim hdnIDBooking As HiddenField = CType(e.Item.FindControl("hdnIDBooking"), HiddenField)
            Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)
            Dim hClassCode As HyperLink = CType(e.Item.FindControl("hKodeKelas"), HyperLink)
            Dim chx As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
            Dim dataClass As TrClass = data.TrClassRegistration.TrClass
            lblNo.Text = e.CreateNumberPage()
            lblNoReg.Text = data.TrTrainee.ID
            lblNamaSiswa.Text = data.TrTrainee.Name
            lblPosisi.Text = data.TrTrainee.RefJobPosition.Description
            lblKategori.Text = data.TrCourse.CourseCode
            hClassCode.Text = dataClass.ClassCode
            hdnIDBooking.Value = data.ID
            lblPaidDay.Text = dataClass.PaidDay
            lblPricePerDay.Text = dataClass.PricePerDay.AddThousandDelimiter()
            lblTotalPrice.Text = dataClass.PriceTotal.AddThousandDelimiter()
            chx.Enabled = False
            If ReadOnlyForm.Equals("1") Then
                Dim pDetail As TrBillingDetail = New TrBillingDetailFacade(User).RetrieveByBookingID(data.ID)
                If Not pDetail.ID.Equals(0) Then
                    If pDetail.IsVoucherUsed Then chx.Checked = True
                End If
            End If

            Dim actionValue As String = "popUpClassInformation('" + dataClass.ClassCode + "');"
            hClassCode.NavigateUrl = "javascript:" + actionValue

            If Not ReadOnlyForm.Equals("1") Then
                Dim crits As New CriteriaComposite(New Criteria(GetType(Reference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crits.opAnd(New Criteria(GetType(Reference), "Type", MatchType.Exact, "TRASS"))
                crits.opAnd(New Criteria(GetType(Reference), "Code", MatchType.Exact, "DUEDATECFR"))

                Dim spaceDueDate As Integer = CType(CType(New ReferenceFacade(Me.User).Retrieve(crits)(0), Reference).Description.Trim, Integer)
                If Page.DateNow >= data.TrClassRegistration.TrClass.StartDate.AddDays(spaceDueDate * -1).DateDay Then
                    e.Item.BackColor = Color.Yellow
                End If
            End If


        End If
    End Sub

    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("FrmCourseBill.aspx?area=" + AreaId)
    End Sub

    Protected Sub chkItemChecked_CheckedChanged(sender As Object, e As EventArgs)
        Dim chx As CheckBox = CType(sender, CheckBox)
        Dim dtgItem As DataGridItem = CType(chx.Parent.Parent, DataGridItem)
        Dim ppnGlobal As Decimal = helpers.GetSession("PPN")
        If chx.Checked Then
            If Not CheckedCalculate(dtgItem) Then
                Dim totalTemp As Decimal = (Decimal.Parse(txtTotalPrice.Text) - Decimal.Parse(txtVoucher.Text))
                'Dim ppn As Decimal = totalTemp / 10
                Dim ppn As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnGlobal, dpp:=totalTemp)
                txtppn.Text = ppn.AddThousandDelimiter
                txtTotal.Text = (totalTemp + ppn).AddThousandDelimiter()
                MessageBox.Show("Free Pass tidak mencukupi.")
                Return
            End If
        Else
            UnCheckedCalculate(dtgItem)
        End If
        Dim totalTemp2 As Decimal = (Decimal.Parse(txtTotalPrice.Text) - Decimal.Parse(txtVoucher.Text))
        'Dim ppn2 As Decimal = totalTemp2 / 10
        Dim ppn2 As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnGlobal, dpp:=totalTemp2)
        txtppn.Text = ppn2.AddThousandDelimiter
        txtTotal.Text = (totalTemp2 + ppn2).AddThousandDelimiter()
    End Sub

    Private Function CheckedCalculate(ByVal dtgItem As DataGridItem) As Boolean
        Dim lblPaidDay As Label = CType(dtgItem.FindControl("lblPaidDay"), Label)
        Dim lblTotalPrice As Label = CType(dtgItem.FindControl("lblTotalPrice"), Label)
        Dim chx As CheckBox = CType(dtgItem.FindControl("chkItemChecked"), CheckBox)
        If CInt(txSisaVoucher.Text) >= CInt(lblPaidDay.Text) Then
            txtVoucher.Text = (CInt(txtVoucher.Text.Replace(".", "")) + CInt(lblTotalPrice.Text.Replace(".", ""))).AddThousandDelimiter()
            txSisaVoucher.Text = CInt(txSisaVoucher.Text) - CInt(lblPaidDay.Text)
            txtTotal.Text = (CInt(txtTotalPrice.Text.Replace(".", "")) - CInt(txtVoucher.Text.Replace(".", ""))).AddThousandDelimiter()
            Return True
        End If
        txtTotal.Text = (CInt(txtTotalPrice.Text.Replace(".", "")) - CInt(txtVoucher.Text.Replace(".", ""))).AddThousandDelimiter()
        chx.Checked = False
        Return False
    End Function

    Private Sub UnCheckedCalculate(ByVal dtgItem As DataGridItem)
        Dim lblPaidDay As Label = CType(dtgItem.FindControl("lblPaidDay"), Label)
        Dim lblTotalPrice As Label = CType(dtgItem.FindControl("lblTotalPrice"), Label)

        txtVoucher.Text = (CInt(txtVoucher.Text.Replace(".", "")) - CInt(lblTotalPrice.Text.Replace(".", ""))).AddThousandDelimiter()
        txSisaVoucher.Text = CInt(txSisaVoucher.Text) + CInt(lblPaidDay.Text)
        txtTotal.Text = (CInt(txtTotalPrice.Text.Replace(".", "")) - CInt(txtVoucher.Text.Replace(".", ""))).AddThousandDelimiter()

    End Sub

    Private Sub GridCalculation()
        Dim dataLooping As Boolean = True
        For Each dataItem As DataGridItem In dtgBooking.Items
            Dim chx As CheckBox = CType(dataItem.FindControl("chkItemChecked"), CheckBox)
            Dim chx2 As CheckBox = CType(dataItem.FindControl("chkItem"), CheckBox)
            If chx2.Checked Then
                If Not chx.Checked Then
                    dataLooping = CheckedCalculate(dataItem)
                End If
                If Not dataLooping Then
                    hdnCheck.Value = "1"
                    Exit For
                Else
                    chx.Checked = True
                End If
            End If
        Next
    End Sub

    Private Function isValidApprove() As Boolean
        For Each dataItem As DataGridItem In dtgBooking.Items
            Dim chx As CheckBox = CType(dataItem.FindControl("chkItem"), CheckBox)
            If chx.Checked Then
                Return True
            End If
        Next
        Return False
    End Function

    Protected Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            Dim pHeader As TrBillingHeader = New TrBillingHeader()
            Dim dealer As Dealer = Me.GetDealer()
            If Not dealer.DealerCode.Trim = ddlDealerGroup.SelectedValue.Trim Then
                Dim extension As String() = {".pdf", ".jpg", ".png", ".jpeg"}
                Dim rest As String = helpers.UploadFile(FileSuratKuasa, KTB.DNet.Lib.WebConfig.GetValue("FakturPajak"), _
                                                   KTB.DNet.Lib.WebConfig.GetValue("MaximumFakturPajakSize"), extension)
                Dim errArr() As String = rest.Split("|")
                If errArr(0).Equals("Error") Then
                    MessageBox.Show(errArr(1))
                    Return
                End If
                pHeader.DealerCode = ddlDealerGroup.SelectedValue
                pHeader.PathSuratKuasa = errArr(1)
            Else
                pHeader.DealerCode = dealer.DealerCode
            End If

            Dim funcH As TrBillingHeaderFacade = New TrBillingHeaderFacade(User)
            Dim funcD As TrBillingDetailFacade = New TrBillingDetailFacade(User)
            Dim funcB As TrBookingCourseFacade = New TrBookingCourseFacade(User)
            If isValidApprove() Then

                pHeader.RequestID = funcH.CreateRequestId(dealer.ID)
                pHeader.Dealer = dealer
                pHeader.FiscalYear = ddlTahunFiscal.SelectedValue
                pHeader.TotalPrice = Decimal.Parse(txtTotalPrice.Text)
                pHeader.TotalVoucher = Decimal.Parse(txtVoucher.Text)
                pHeader.PPN = Decimal.Parse(txtppn.Text)
                pHeader.Total = Decimal.Parse(txtTotal.Text)
                If pHeader.Total.Equals(0) Then
                    pHeader.Status = CType(EnumTagihanTraining.TagihanStatus.Selesai, Short)
                Else
                    pHeader.Status = CType(EnumTagihanTraining.TagihanStatus.Validasi, Short)
                End If
                If dealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                    Dim saldoDepositB As Double = funcB.GetSaldoDepositB(dealer.ID, CInt(ddlTahunFiscal.SelectedValue.Split("/")(0)), 1) 'MMC
                    If saldoDepositB > pHeader.Total Then
                        pHeader.PaymentType = CType(EnumTagihanTraining.TipePembayaran.Deposit_B, Short)
                    Else
                        pHeader.PaymentType = CType(EnumTagihanTraining.TipePembayaran.Transfer, Short)
                    End If
                End If
                pHeader.ID = funcH.Insert(pHeader)

                If pHeader.ID.Equals(0) Then
                    MessageBox.Show("Gagal saat memproses approval")
                    Return
                End If

                For Each dataItem As DataGridItem In dtgBooking.Items
                    Dim item As TrBillingDetail = New TrBillingDetail()
                    Dim hdnIDBooking As HiddenField = CType(dataItem.FindControl("hdnIDBooking"), HiddenField)
                    Dim chx As CheckBox = CType(dataItem.FindControl("chkItemChecked"), CheckBox)
                    Dim chx2 As CheckBox = CType(dataItem.FindControl("chkItem"), CheckBox)
                    If chx2.Checked Then
                        item.TrBookingCourse = funcB.Retrieve(CInt(hdnIDBooking.Value))
                        item.TrBillingHeader = pHeader
                        item.Status = 1
                        item.IsVoucherUsed = chx.Checked
                        funcD.Insert(item)
                    End If
                Next

                Dim criteria As New CriteriaComposite(New Criteria(GetType(TrFreePass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteria.opAnd(New Criteria(GetType(TrFreePass), "Dealer.ID", MatchType.Exact, dealer.ID))
                criteria.opAnd(New Criteria(GetType(TrFreePass), "FiscalYear", MatchType.Exact, ddlTahunFiscal.SelectedValue))
                Dim funcF As TrFreePassFacade = New TrFreePassFacade(User)
                Dim arrDataVoucher As ArrayList = funcF.Retrieve(criteria)
                If arrDataVoucher.Count > 0 Then
                    Dim dataVoucher As TrFreePass = CType(arrDataVoucher(0), TrFreePass)
                    dataVoucher.QtyUsed = CInt(txJumlahVoucher.Text) - CInt(txSisaVoucher.Text)
                    funcF.Update(dataVoucher)
                End If
                Dim nUrl As String = Me.Request.Url.ToString + "&IsApprove=1"
                Response.Redirect(nUrl)
            Else
                MessageBox.Show("Pilih siswa yang akan diapprove")
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal saat memproses approval")
        End Try
    End Sub

    Private Sub EmptyForm()
        btnApprove.Visible = False
        dtgBooking.DataSource = New List(Of TrBookingCourse)
        dtgBooking.DataBind()

        txtTotalPrice.Text = "0"
        txtVoucher.Text = "0"
        txtTotal.Text = "0"

        txtTotalPrice.ReadOnly = True
        txtVoucher.ReadOnly = True
        txtTotal.ReadOnly = True
        txtTotalPrice.BackColor = Color.LightGray
        txtVoucher.BackColor = Color.LightGray
        txtTotal.BackColor = Color.LightGray
        txJumlahVoucher.BackColor = Color.LightGray
        txSisaVoucher.BackColor = Color.LightGray
    End Sub

    Protected Sub cbxInfo_CheckedChanged(sender As Object, e As EventArgs)
        If cbxInfo.Checked Then
            If isValidApprove() Then
                btnApprove.Enabled = True
            Else
                MessageBox.Show("Pilih siswa yang akan diapprove")
                cbxInfo.Checked = False
            End If
        Else
            btnApprove.Enabled = False
        End If
    End Sub

    Private Sub lbtnDownload_Click(sender As Object, e As EventArgs) Handles lbtnDownload.Click
        Try
            helpers.DownloadFile(hdnFilePath.Value)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub lbnDelete_Click(sender As Object, e As EventArgs) Handles lbnDelete.Click
        hdnFilePath.Value = String.Empty
        trUpload.ActiveControl()
        trDownload.NonActiveControl()
        btnSimpan.Enabled = False
        btnSubmit.Enabled = False
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            Dim rest As String = helpers.UploadFile(photoSrc, KTB.DNet.Lib.WebConfig.GetValue("FakturPajak"), _
                                                    KTB.DNet.Lib.WebConfig.GetValue("MaximumFakturPajakSize"))
            Dim errArr() As String = rest.Split("|")
            If errArr(0).Equals("Error") Then
                MessageBox.Show(errArr(1))
                Return
            Else
                hdnFilePath.Value = errArr(1)
            End If
            trDownload.ActiveControl()
            trUpload.NonActiveControl()
            btnSimpan.Enabled = True
            btnSubmit.Enabled = True
        Catch ex As Exception
            MessageBox.Show("Error saat mengupload foto")
        End Try
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Try
            Dim func As TrBillingHeaderFacade = New TrBillingHeaderFacade(User)
            Dim data As TrBillingHeader = func.Retrieve(CInt(RequestID))
            data.PathFaktur = hdnFilePath.Value
            data.PathDebitNote = hdnFilePath2.Value
            func.Update(data)
            MessageBox.Show(SR.SaveSuccess)
        Catch ex As Exception
            MessageBox.Show("Simpan data gagal.")
        End Try

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            Dim func As TrBillingHeaderFacade = New TrBillingHeaderFacade(User)
            Dim data As TrBillingHeader = func.Retrieve(CInt(RequestID))
            data.Status = CType(EnumTagihanTraining.TagihanStatus.Disetujui, Short)
            data.PathFaktur = hdnFilePath.Value
            func.Update(data)
            btnSubmit.NonActiveControl()
            MessageBox.Show("Submit data Berhasil")
        Catch ex As Exception
            MessageBox.Show("Proses data gagal.")
        End Try
    End Sub


    Protected Sub btnUploadDN_Click(sender As Object, e As EventArgs) Handles btnUploadDN.Click
        Try
            Dim rest As String = helpers.UploadFile(FileDN, KTB.DNet.Lib.WebConfig.GetValue("FakturPajak"), _
                                                    KTB.DNet.Lib.WebConfig.GetValue("MaximumFakturPajakSize"))
            Dim errArr() As String = rest.Split("|")
            If errArr(0).Equals("Error") Then
                MessageBox.Show(errArr(1))
                Return
            Else
                hdnFilePath2.Value = errArr(1)
            End If
            trDownloadDN.ActiveControl()
            trUploadDN.NonActiveControl()

        Catch ex As Exception
            MessageBox.Show("Error saat mengupload foto")
        End Try
    End Sub

    Protected Sub lbtnDeleteDN_Click(sender As Object, e As EventArgs) Handles lbtnDeleteDN.Click
        hdnFilePath2.Value = String.Empty
        trUploadDN.ActiveControl()
        trDownloadDN.NonActiveControl()
    End Sub

    Protected Sub lbtnDonwloadDN_Click(sender As Object, e As EventArgs) Handles lbtnDonwloadDN.Click
        Try
            helpers.DownloadFile(hdnFilePath2.Value)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Protected Sub chkItem_CheckedChanged(sender As Object, e As EventArgs)
        Dim chk As CheckBox = CType(sender, CheckBox)
        Dim item As DataGridItem = chk.Parent.Parent
        Dim hdnID As HiddenField = item.FindHiddenField("hdnIDBooking")
        Dim chk2 As CheckBox = item.FindCheckBox("chkItemChecked")
        Dim dtBooking As TrBookingCourse = New TrBookingCourseFacade(User).Retrieve(CInt(hdnID.Value))
        If chk.Checked Then
            chk2.Enabled = True
            txtTotalPrice.Text = (Decimal.Parse(txtTotalPrice.Text) + dtBooking.TrClassRegistration.TrClass.PriceTotal).AddThousandDelimiter()
            If CInt(txJumlahVoucher.Text) > 0 Then
                If CheckedCalculate(item) Then
                    chk2.Checked = True
                Else
                    chk2.Checked = False
                End If
            Else
                chk2.Checked = False
            End If
        Else
            chk2.Enabled = False
            txtTotalPrice.Text = (Decimal.Parse(txtTotalPrice.Text) - dtBooking.TrClassRegistration.TrClass.PriceTotal).AddThousandDelimiter()
            If chk2.Checked Then
                UnCheckedCalculate(item)
                chk2.Checked = False
            End If
        End If
        Dim dtBookingClass As TrClassRegistration = New TrClassRegistrationFacade(User).Retrieve(dtBooking.TrClassRegistration.ID)
        Dim ppnVal As Decimal = 0
        If Not IsNothing(dtBookingClass) Then
            ppnVal = CalcHelper.GetPPNMasterByTaxTypeId(dtBookingClass.RegistrationDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
            helpers.SetSession("PPN", ppnVal)
        End If

        Dim totalTemp As Decimal = (Decimal.Parse(txtTotalPrice.Text) - Decimal.Parse(txtVoucher.Text))
        'Dim ppn As Decimal = totalTemp / 10
        Dim ppn As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=totalTemp)
        txtppn.Text = ppn.AddThousandDelimiter
        txtTotal.Text = (totalTemp + ppn).AddThousandDelimiter()

    End Sub

    Protected Sub chkAllItems_CheckedChanged(sender As Object, e As EventArgs)
        Dim chx As CheckBox = CType(sender, CheckBox)
        Dim ppnVal As Decimal = 0
        For Each item As DataGridItem In dtgBooking.Items
            Dim chk As CheckBox = item.FindCheckBox("chkItem")
            Dim hdnID As HiddenField = item.FindHiddenField("hdnIDBooking")
            Dim chk2 As CheckBox = item.FindCheckBox("chkItemChecked")
            Dim dtBooking As TrBookingCourse = New TrBookingCourseFacade(User).Retrieve(CInt(hdnID.Value))
            Dim dtBookingClass As TrClassRegistration = New TrClassRegistrationFacade(User).Retrieve(dtBooking.TrClassRegistration.ID)
            ppnVal = CalcHelper.GetPPNMasterByTaxTypeId(dtBookingClass.RegistrationDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
            If chx.Checked Then
                If Not chk.Checked Then
                    chk.Checked = True
                    chk2.Enabled = True
                    txtTotalPrice.Text = (Decimal.Parse(txtTotalPrice.Text) + dtBooking.TrClassRegistration.TrClass.PriceTotal).AddThousandDelimiter()
                    If CInt(txJumlahVoucher.Text) > 0 Then
                        If CheckedCalculate(item) Then
                            chk2.Checked = True
                        End If
                    End If
                End If
            Else
                If chk.Checked Then
                    chk.Checked = False
                    chk2.Enabled = False
                    txtTotalPrice.Text = (Decimal.Parse(txtTotalPrice.Text) - dtBooking.TrClassRegistration.TrClass.PriceTotal).AddThousandDelimiter()
                    If chk2.Checked Then
                        UnCheckedCalculate(item)
                    Else
                        txtTotal.Text = (CInt(txtTotalPrice.Text.Replace(".", "")) - CInt(txtVoucher.Text.Replace(".", ""))).AddThousandDelimiter()
                    End If
                    chk2.Checked = False
                End If
            End If
        Next

        Dim totalTemp As Decimal = (Decimal.Parse(txtTotalPrice.Text) - Decimal.Parse(txtVoucher.Text))
        'Dim ppn As Decimal = totalTemp / 10
        Dim ppn As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=totalTemp)
        txtppn.Text = ppn.AddThousandDelimiter
        txtTotal.Text = (totalTemp + ppn).AddThousandDelimiter()
    End Sub

    Protected Sub ddlTahunFiscal_SelectedIndexChanged(sender As Object, e As EventArgs)
        InitData(ddlTahunFiscal.SelectedValue, e)
    End Sub
End Class
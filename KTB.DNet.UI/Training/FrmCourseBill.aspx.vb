#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
Imports KTB.DNet.SAP
Imports KTB.DNet.BusinessValidation.Helpers
#End Region

Public Class FrmCourseBill
    Inherits System.Web.UI.Page

    Private designerPlaceholderDeclaration As System.Object
    Private helpers As New TrainingHelpers(Me.Page, "Training After Sales - Daftar Tagihan")
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub TitleDescription(ByVal areaid As String)
        If areaid.Equals("1") Then
            lblPageTitle.Text = "Training Sales - Daftar Tagihan"
            hdnCategory.Value = "sales"
        ElseIf areaid.Equals("2") Then
            lblPageTitle.Text = "Training After Sales - Daftar Tagihan"
            hdnCategory.Value = "ass"
        ElseIf areaid.Equals("3") Then
            lblPageTitle.Text = "Training Customer Satisfaction - Daftar Tagihan"
            hdnCategory.Value = "cs"
        Else
            lblPageTitle.Text = "Training - Daftar Tagihan"
            hdnCategory.Value = "ass"
        End If
    End Sub

    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property

    Private Sub BindDDLStatus()
        ddlStatus.ClearSelection()
        ddlStatus.Items.AddRange(GetType(EnumTagihanTraining.TagihanStatus).GetItems())
        ddlStatus.SelectedIndex = 0
    End Sub

    Private Sub BindDDLTahunFiskal(ByVal ddl As DropDownList)
        Dim GetTahun As Integer = DateTime.Now.Year
        ddl.ClearSelection()
        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("Semua", "-1"))
        'Before
        For x As Integer = 10 To 0 Step -1
            Dim value1 As String = (GetTahun - x).ToString()
            Dim value2 As String = (GetTahun - x - 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value2, value1)
            ddl.Items.Add(New ListItem(value, value))
        Next
        'After
        For x As Integer = 0 To 10
            Dim value1 As String = (GetTahun + x).ToString()
            Dim value2 As String = (GetTahun + x + 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value1, value2)
            ddl.Items.Add(New ListItem(value, value))
        Next
        ddl.SelectedValue = String.Format("{0}/{1}", GetTahun.ToString(), (GetTahun + 1).ToString())
    End Sub

    Private Sub FrmCourseBill_Init(sender As Object, e As EventArgs) Handles MyBase.Init

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.view, SR.TrainingAssViewDaftarTagihan_Privilege)
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditDaftarTagihan_Privilege)
        helpers.Privilage()
        If Not IsPostBack Then
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            TitleDescription(AreaId)
            Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
            BindDDLStatus()
            If dealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                BindDDLTahunFiskal(ddlFiscalYear)
                lblPopUpDealer.Visible = False
                txtDealerCode.Text = dealer.DealerCode & " - " & dealer.DealerName
                txtDealerCode.Disabled()
                trFyearMKS.Visible = False
                trAction.NonActiveControl()
                trMks.NonActiveControl()
            Else
                BindDDLTahunFiskal(ddlTahunFiscal)
                trFyearDealer.Visible = False
                trDealer.NonActiveControl()
                trAction.Visible = helpers.IsEdit
            End If
            BindingGrid()
        End If
        lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Private Sub BindingGrid(Optional ByVal pageNumber As Integer = 0)
        Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
        Dim totalRow As Integer = 0
        Dim criteria As New CriteriaComposite(New Criteria(GetType(TrBillingHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ddlStatus.IsSelected Then
            criteria.opAnd(New Criteria(GetType(TrBillingHeader), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        If dealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            'criteria.opAnd(New Criteria(GetType(TrBillingHeader), "Dealer.ID", MatchType.Exact, dealer.ID))
            criteria.opAnd(New Criteria(GetType(TrBillingHeader), "Dealer.ID", MatchType.Exact, dealer.ID), "(", True)
            criteria.opOr(New Criteria(GetType(TrBillingHeader), "DealerCode", MatchType.Exact, dealer.DealerCode), ")", False)
            If ddlFiscalYear.IsSelected Then
                criteria.opAnd(New Criteria(GetType(TrBillingHeader), "FiscalYear", MatchType.Exact, ddlFiscalYear.SelectedValue))
            End If
        Else
            Dim dealerInSet As String = txtKodeDealer.Text.GenerateInSet()
            If Not String.IsNullorEmpty(txtKodeDealer.Text) Then
                'criteria.opAnd(New Criteria(GetType(TrBillingHeader), "Dealer.DealerCode", MatchType.InSet, String.Format("({0})", dealerInSet)))
                criteria.opAnd(New Criteria(GetType(TrBillingHeader), "Dealer.DealerCode", MatchType.InSet, String.Format("({0})", dealerInSet)), "(", True)
                criteria.opOr(New Criteria(GetType(TrBillingHeader), "DealerCode", MatchType.InSet, String.Format("({0})", dealerInSet)), ")", False)
            End If
            If ddlTahunFiscal.IsSelected Then
                criteria.opAnd(New Criteria(GetType(TrBillingHeader), "FiscalYear", MatchType.Exact, ddlTahunFiscal.SelectedValue))
            End If
        End If
        dtgBilling.DataSource = New TrBillingHeaderFacade(User).RetrieveByCriteria(criteria, pageNumber, dtgBilling.PageSize, totalRow, _
                                CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgBilling.VirtualItemCount = totalRow
        dtgBilling.DataBind()
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        BindingGrid(dtgBilling.CurrentPageIndex)
    End Sub

    Private Sub DeleteBill(ByVal id As Integer)
        Try
            Dim funcH As New TrBillingHeaderFacade(User)
            Dim funcD As New TrBillingDetailFacade(User)
            Dim dataBill As TrBillingHeader = funcH.Retrieve(id)
            For Each item As TrBillingDetail In dataBill.ListTrBillingDetail
                item.RowStatus = CType(DBRowStatus.Deleted, Short)
                funcD.Update(item)
            Next
            dataBill.RowStatus = CType(DBRowStatus.Deleted, Short)
            funcH.Update(dataBill)
            MessageBox.Show("Hapus data berhasil")
            BindingGrid(dtgBilling.CurrentPageIndex)
        Catch ex As Exception
            MessageBox.Show("Hapus data gagal")
        End Try

    End Sub

    Private Sub dtgBilling_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgBilling.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "detail"
                Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)
                Response.Redirect("FrmDataSiswaBerbayar.aspx?readonly=1&area=2&requestid=" + hdnID.Value)
            Case "delete"
                Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)
                DeleteBill(CInt(hdnID.Value))
            Case "downloadfaktur"
                Dim hdnSourceFile As HiddenField = CType(e.Item.FindControl("hdnSourceFile"), HiddenField)
                Try
                    helpers.DownloadFile(hdnSourceFile.Value)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            Case "downloaddn"
                Dim hdnSourceFile As HiddenField = CType(e.Item.FindControl("hdnSourceFileDN"), HiddenField)
                Try
                    helpers.DownloadFile(hdnSourceFile.Value)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            Case "downloadsuratkuasa"
                Dim hdnSourceFile As HiddenField = CType(e.Item.FindControl("hdnsuratkuasa"), HiddenField)
                Try
                    helpers.DownloadFile(hdnSourceFile.Value)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            Case "downloadbuktitf"
                Dim hdnSourceFile As HiddenField = CType(e.Item.FindControl("hdnbuktitransfer"), HiddenField)
                Try
                    helpers.DownloadFile(hdnSourceFile.Value)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
        End Select

    End Sub

    Private Sub dtgBilling_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgBilling.ItemDataBound
        If CType(helpers.GetSession("Dealer"), Dealer).Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            e.Item.Cells(0).Visible = False
        End If
        If Not e.Item.DataItem Is Nothing Then
            Dim funcB As New TrBookingCourseFacade(User)
            Dim funcP As New TrTraineePaymentDetailFacade(User)
            Dim data As TrBillingHeader = CType(e.Item.DataItem, TrBillingHeader)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim chk As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
            Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
            Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
            Dim lblDealerPembayar As Label = CType(e.Item.FindControl("lblDealerPembayar"), Label)
            Dim ICPostedDate As KTB.DNet.WebCC.IntiCalendar = e.Item.FindControl("ICPostedDate")

            Dim lblTahunFiskal As Label = CType(e.Item.FindControl("lblTahunFiskal"), Label)
            Dim lblDueDate As Label = CType(e.Item.FindControl("lblDueDate"), Label)
            Dim lblPosteddate As Label = CType(e.Item.FindControl("lblPosteddate"), Label)
            Dim lbltipePembayaran As Label = CType(e.Item.FindControl("lbltipePembayaran"), Label)
            Dim lblJumlahSiswa As Label = CType(e.Item.FindControl("lblJumlahSiswa"), Label)
            Dim lblDNNumber As Label = CType(e.Item.FindControl("lblDNNumber"), Label)
            Dim lblJVNumber As Label = CType(e.Item.FindControl("lblJVNumber"), Label)
            Dim lblJumlahBayar As Label = CType(e.Item.FindControl("lblJumlahBayar"), Label)
            Dim lblTotalVoucher As Label = CType(e.Item.FindControl("lblTotalVoucher"), Label)
            Dim lblPPN As Label = CType(e.Item.FindControl("lblPPN"), Label)
            Dim lblTotalPrice As Label = CType(e.Item.FindControl("lblTotalPrice"), Label)
            Dim lblMaxDepositB As Label = CType(e.Item.FindControl("lblMaxDepositB"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)
            Dim hdnSourceFile As HiddenField = CType(e.Item.FindControl("hdnSourceFile"), HiddenField)
            Dim hdnSourceFileDN As HiddenField = CType(e.Item.FindControl("hdnSourceFileDN"), HiddenField)
            Dim hdnsuratkuasa As HiddenField = CType(e.Item.FindControl("hdnsuratkuasa"), HiddenField)
            Dim hdnbuktitransfer As HiddenField = CType(e.Item.FindControl("hdnbuktitransfer"), HiddenField)
            Dim btnDownloadFaktur As LinkButton = CType(e.Item.FindControl("btnDownloadFaktur"), LinkButton)
            Dim btnDownloadDN As LinkButton = CType(e.Item.FindControl("btnDownloadDN"), LinkButton)
            Dim btnDownloadBuktiTF As LinkButton = CType(e.Item.FindControl("btnDownloadBuktiTF"), LinkButton)
            Dim btnHapus As LinkButton = CType(e.Item.FindControl("btnHapus"), LinkButton)
            Dim btnDownloadSuratKuasa As LinkButton = CType(e.Item.FindControl("btnDownloadSuratKuasa"), LinkButton)

            Dim crits As New CriteriaComposite(New Criteria(GetType(Reference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(Reference), "Type", MatchType.Exact, "TRASS"))
            crits.opAnd(New Criteria(GetType(Reference), "Code", MatchType.Exact, "DUEDATE"))

            Dim spaceDueDate As Integer = CType(CType(New ReferenceFacade(User).Retrieve(crits)(0), Reference).Description.Trim, Integer)
            Dim dueDate As DateTime

            lblNo.Text = e.CreateNumberPage()
            hdnID.Value = data.ID
            lblNoReg.Text = data.RequestID
            lblKodeDealer.Text = data.Dealer.DealerCode
            lblDealerPembayar.Text = data.DealerCode
            lblTahunFiskal.Text = data.FiscalYear
            lblJumlahSiswa.Text = New TrBillingHeaderFacade(User).RetrievePaymentDetail(data.ID).Count
            lblJumlahBayar.Text = data.TotalPrice.AddThousandDelimiter()
            lblTotalVoucher.Text = data.TotalVoucher.AddThousandDelimiter()
            lblPPN.Text = data.PPN.AddThousandDelimiter()
            lblTotalPrice.Text = data.Total.AddThousandDelimiter()

            Dim objDealer As Dealer = New DealerFacade(Me.User).Retrieve(data.DealerCode)
            If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                lbltipePembayaran.Text = [Enum].GetName(GetType(EnumTagihanTraining.TipePembayaran), data.PaymentType).Replace("_", " ")
                Dim saldoDepositB As Double = funcB.GetSaldoDepositB(objDealer.ID, CInt(data.FiscalYear.Split("/")(0)), 1) 'MMC
                If saldoDepositB > 0 Then
                    lblMaxDepositB.Text = saldoDepositB.AddThousandDelimiter()
                Else
                    lblMaxDepositB.Text = String.Empty
                End If
            End If

            lblJVNumber.Text = data.JVNumber
            lblDNNumber.Text = data.DebitNoteNumber
            lblStatus.Text = [Enum].GetName(GetType(EnumTagihanTraining.TagihanStatus), data.Status).Replace("_", " ")

            btnHapus.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")

            If String.IsNullorEmpty(data.PathFaktur) Then
                btnDownloadFaktur.NonActiveControl()
            Else
                hdnSourceFile.Value = data.PathFaktur
            End If

            If String.IsNullorEmpty(data.PathSuratKuasa) Then
                btnDownloadSuratKuasa.NonActiveControl()
            Else
                hdnsuratkuasa.Value = data.PathSuratKuasa
            End If

            If String.IsNullorEmpty(data.PathDebitNote) Then
                btnDownloadDN.NonActiveControl()
            Else
                hdnSourceFileDN.Value = data.PathDebitNote
            End If

            Dim dtPayment As TrTraineePaymentDetail = funcP.GetDataByBilling(data.ID)
            btnDownloadBuktiTF.NonActiveControl()
            If dtPayment.ID <> 0 Then
                If Not String.IsNullorEmpty(dtPayment.TrTraineePaymentHeader.EvidencePath) Then
                    btnDownloadBuktiTF.ActiveControl()
                    hdnbuktitransfer.Value = dtPayment.TrTraineePaymentHeader.EvidencePath
                End If
            End If

            Dim lblHistoryStatus As Label = CType(e.Item.FindControl("lblHistoryStatus"), Label)
            lblHistoryStatus.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpChangeStatusHistoryFU.aspx?DocType=" & LookUp.DocumentType.Tagihan_Training & "&DocNumber=" & data.RequestID, "", 400, 400, "null")
            If data.Status = 2 Or data.Status = 3 Or data.Status = 4 Then
                dueDate = data.PostedDate.AddDays(spaceDueDate).DateDay

                If Me.DateNow > dueDate Then
                    e.Item.BackColor = Color.LightSalmon
                End If
            End If

            If data.Status = CType(EnumTagihanTraining.TagihanStatus.Validasi, Short) Then
                ICPostedDate.Enabled = True
                ICPostedDate.Value = Me.DateNow
                lblPosteddate.Visible = False
            Else
                btnHapus.Visible = False
                lblPosteddate.Text = data.PostedDate.DateToString()
                ICPostedDate.Visible = False
                ICPostedDate.Value = data.PostedDate
                If data.PostedDate.IsValid Then
                    lblDueDate.Text = data.PostedDate.AddDays(spaceDueDate).DateDay.DateToString()
                End If

            End If

            If Not helpers.IsEdit Then
                chk.Visible = False
                btnHapus.Visible = False
            End If
        End If
    End Sub

    Protected Sub ddlFiscalYear_SelectedIndexChanged(sender As Object, e As EventArgs)
        BindingGrid(dtgBilling.CurrentPageIndex)
    End Sub

    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        If isCheckSendSAP() Then
            Try
                Dim listDataNew As List(Of TrBillingHeader) = New List(Of TrBillingHeader)
                Dim funcH As TrBillingHeaderFacade = New TrBillingHeaderFacade(User)
                Dim funcB As TrBookingCourseFacade = New TrBookingCourseFacade(User)
                Dim listDataCheck As List(Of TrBillingHeader) = GetDataCheckBox()

                If Not listDataCheck.Where(Function(x) x.Status = CType(EnumTagihanTraining.TagihanStatus.Validasi, Short)).IsData Then
                    MessageBox.Show("Send ke SAP Gagal. Tidak ada Data tagihan dengan status validasi")
                    Exit Sub
                End If

                If Me.txtPass.Text = String.Empty Then
                    RegisterStartupScript("OpenWindow", "<script>InputPasswordPlease();</script>")
                    Return
                End If

                Dim UserName As String
                Dim Password As String
                Dim sapConStr As String = KTB.DNet.Lib.WebConfig.GetValue("SAPConnectionStringEmpty") ' User "SAPConnectionString" and prompt user to enter password first
                Dim oSAPDnet As SAPDNet

                Dim aErrors As New ArrayList
                Dim oUI As UserInfo = CType(helpers.GetSession("LOGINUSERINFO"), UserInfo)

                Dim crits As New CriteriaComposite(New Criteria(GetType(Reference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crits.opAnd(New Criteria(GetType(Reference), "Type", MatchType.Exact, "TRASS"))
                crits.opAnd(New Criteria(GetType(Reference), "Code", MatchType.Exact, "SAPDESC"))

                Dim descSAP As String = CType(New ReferenceFacade(User).Retrieve(crits)(0), Reference).Description.Trim

                UserName = Me.txtUser.Text
                Password = Me.txtPass.Text
                oSAPDnet = New SAPDNet(sapConStr, UserName, Password)
                Dim isEnabledFuture As Boolean = CType(New AppConfigFacade(User).Retrieve("AfterSalesTraining.IsEnableFuturePostedDate").Value, Boolean)
                For Each data As TrBillingHeader In listDataCheck.Where(Function(x) x.Status = CType(EnumTagihanTraining.TagihanStatus.Validasi, Short))
                    If Not isEnabledFuture Then
                        If Not (data.PostedDate.Month = DateNow.Month And data.PostedDate.Year = DateNow.Year) Then
                            Continue For
                        End If

                        If data.PostedDate < Me.DateNow() Then
                            Continue For
                        End If
                    End If

                    Dim dataDesc As String = descSAP
                    If Not data.DealerCode = data.Dealer.DealerCode Then
                        dataDesc += String.Format(" {0}", data.Dealer.DealerCode)
                    End If

                    'Kalkulasi PPN dan Update data PPN
                    Dim ppnVal As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(data.PostedDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                    Dim ppn As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=data.TotalPrice)
                    data.PPN = ppn
                    data.Total = data.TotalPrice + ppn
                    Dim updatePPN As TrBillingHeader = New TrBillingHeaderFacade(User).Retrieve(data.ID)
                    If Not IsNothing(updatePPN) Then
                        updatePPN.PPN = ppn
                        updatePPN.Total = updatePPN.TotalPrice + ppn
                    End If
                    Dim vReturn As Integer = New TrBillingHeaderFacade(User).Update(updatePPN)

                    Dim DNNumber As String = String.Empty, Msg As String = String.Empty
                    Dim isSucces As Boolean = oSAPDnet.SendBillsViaRFC(data, dataDesc, DNNumber, Msg)
                    If Msg.IsNullorEmpty And isSucces Then
                        data.DebitNoteNumber = DNNumber
                    Else
                        MessageBox.Show(Msg)
                        Continue For
                    End If

                    Dim saldoDepositB As Double = funcB.GetSaldoDepositB(data.Dealer.ID, CInt(data.FiscalYear.Split("/")(0)), 1) 'MMC
                    If saldoDepositB >= data.Total Then
                        data.PaymentType = CType(EnumTagihanTraining.TipePembayaran.Deposit_B, Short)
                    Else
                        data.PaymentType = CType(EnumTagihanTraining.TipePembayaran.Transfer, Short)
                    End If
                    data.Status = CType(EnumTagihanTraining.TagihanStatus.Konfirmasi, Short)
                    listDataNew.Add(data)
                Next
                funcH.Update(listDataNew)
                MessageBox.Show(String.Format("{0} data Send to SAP berhasil. {1} data Send to SAP gagal.", listDataNew.Count, (listDataCheck.Count - listDataNew.Count)))

                BindingGrid(dtgBilling.CurrentPageIndex)
            Catch ex As Exception
                MessageBox.Show("Send to SAP gagal.")
            End Try
        End If
    End Sub

    Protected Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        If isCheck() Then
            Try
                Dim listDataNew As New List(Of TrBillingHeader)
                Dim listDataDebitNote As New List(Of DepositBDebitNote)
                Dim listDataDepositBH As New List(Of DepositBPencairanHeader)
                Dim listDataDepositBD As New List(Of DepositBPencairanDetail)

                Dim funcH As New TrBillingHeaderFacade(User)
                Dim funcB As New TrBookingCourseFacade(User)
                Dim funcDN As New DepositBDebitNoteFacade(User)
                Dim funcDBH As New DepositBPencairanHeaderFacade(User)
                Dim funcDBD As New DepositBPencairanDetailFacade(User)

                Dim crits As New CriteriaComposite(New Criteria(GetType(Reference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crits.opAnd(New Criteria(GetType(Reference), "Type", MatchType.Exact, "TRASS"))
                crits.opAnd(New Criteria(GetType(Reference), "Code", MatchType.Exact, "SAPDESC"))

                Dim descSAP As String = CType(New ReferenceFacade(User).Retrieve(crits)(0), Reference).Description.Trim

                Dim listDataCheck As List(Of TrBillingHeader) = GetDataCheckBox()
                For Each data As TrBillingHeader In listDataCheck.Where(Function(x) x.Status = CType(EnumTagihanTraining.TagihanStatus.Disetujui, Short))
                    Dim saldoDepositB As Double = funcB.GetSaldoDepositB(New DealerFacade(User).Retrieve(data.DealerCode).ID, CInt(data.FiscalYear.Split("/")(0)), 1) 'MMC
                    If saldoDepositB >= data.Total Then
                        data.PaymentType = CType(EnumTagihanTraining.TipePembayaran.Deposit_B, Short)
                        data.Status = CType(EnumTagihanTraining.TagihanStatus.Pencairan_Deposit_B, Short)

                        Dim dataDesc As String = descSAP
                        If Not data.DealerCode = data.Dealer.DealerCode Then
                            dataDesc += String.Format(" {0}", data.Dealer.DealerCode)
                        End If

                        Dim dtDebitNote As New DepositBDebitNote
                        dtDebitNote.Dealer = New DealerFacade(User).Retrieve(data.DealerCode)
                        dtDebitNote.Amount = data.Total
                        dtDebitNote.ProductCategory = New ProductCategory(1)
                        dtDebitNote.DNNumber = data.DebitNoteNumber
                        dtDebitNote.Description = dataDesc
                        dtDebitNote.PostingDate = data.PostedDate
                        dtDebitNote.BillingID = data.ID
                        dtDebitNote.Status = 1
                        listDataDebitNote.Add(dtDebitNote)

                        Dim DepositBH As New DepositBPencairanHeader
                        DepositBH.Dealer = New DealerFacade(User).Retrieve(data.DealerCode)
                        DepositBH.DepositBDebitNote = dtDebitNote
                        DepositBH.DealerAmount = data.Total
                        DepositBH.ApprovalAmount = 0
                        DepositBH.Status = 1
                        DepositBH.TipePengajuan = 2
                        DepositBH.Flag = 0
                        DepositBH.DealerBankAccount = New DealerBankAccount(0)
                        DepositBH.ProductCategory = New ProductCategory(1)
                        DepositBH.BillingID = data.ID
                        listDataDepositBH.Add(DepositBH)

                        Dim DepositBD As New DepositBPencairanDetail
                        DepositBD.DealerAmount = data.Total
                        DepositBD.DepositBPencairanHeader = DepositBH
                        DepositBD.Description = dataDesc
                        DepositBD.BillingID = data.ID
                        listDataDepositBD.Add(DepositBD)
                    Else
                        data.PaymentType = CType(EnumTagihanTraining.TipePembayaran.Transfer, Short)
                        data.Status = CType(EnumTagihanTraining.TagihanStatus.Pembayaran_Transfer, Short)

                    End If
                    listDataNew.Add(data)
                Next
                funcH.Update(listDataNew, listDataDebitNote, listDataDepositBH, listDataDepositBD)
                MessageBox.Show(String.Format("{0} data berhasil diproses. {1} data gagal diproses.", listDataNew.Count, (listDataCheck.Count - listDataNew.Count)))

                BindingGrid(dtgBilling.CurrentPageIndex)
            Catch ex As Exception
                MessageBox.Show("Proses data gagal")
            End Try
        End If
    End Sub

    Private Function isCheck() As Boolean
        For Each item As DataGridItem In dtgBilling.Items
            Dim chx As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If chx.Checked Then
                Return True
            End If
        Next
        MessageBox.Show("Silahkan Pilih Tagihan")
        Return False
    End Function

    Private Function isCheckSendSAP() As Boolean
        For Each item As DataGridItem In dtgBilling.Items
            Dim chx As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim icPosteddate As KTB.DNet.WebCC.IntiCalendar = CType(item.FindControl("ICPostedDate"), KTB.DNet.WebCC.IntiCalendar)
            If chx.Checked And icPosteddate.Visible Then
                Return True
            End If
        Next
        MessageBox.Show("Silahkan Pilih Tagihan")
        Return False
    End Function

    Private Sub dtgBilling_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgBilling.PageIndexChanged
        dtgBilling.SelectedIndex = -1
        dtgBilling.CurrentPageIndex = e.NewPageIndex
        BindingGrid(dtgBilling.CurrentPageIndex)
    End Sub

    Private Function GetDataCheckBox() As List(Of TrBillingHeader)
        Dim listResult As List(Of TrBillingHeader) = New List(Of TrBillingHeader)
        Dim func As TrBillingHeaderFacade = New TrBillingHeaderFacade(User)
        For Each item As DataGridItem In dtgBilling.Items
            Dim chx As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim ICPostedDate As KTB.DNet.WebCC.IntiCalendar = item.FindControl("ICPostedDate")

            If chx.Checked Then
                Dim hdnID As HiddenField = CType(item.FindControl("hdnID"), HiddenField)
                Dim datas As TrBillingHeader = func.Retrieve(CInt(hdnID.Value))
                datas.PostedDate = ICPostedDate.Value
                listResult.Add(datas)

            End If
        Next

        Return listResult
    End Function

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If isCheck() Then
            Try
                Dim listDataNew As List(Of TrBillingHeader) = New List(Of TrBillingHeader)
                Dim funcH As TrBillingHeaderFacade = New TrBillingHeaderFacade(User)
                Dim funcB As TrBookingCourseFacade = New TrBookingCourseFacade(User)
                Dim listDataCheck As List(Of TrBillingHeader) = GetDataCheckBox()
                For Each data As TrBillingHeader In listDataCheck.Where(Function(x) x.Status = CType(EnumTagihanTraining.TagihanStatus.Konfirmasi, Short))
                    If Not String.IsNullorEmpty(data.PathFaktur) Then
                        data.Status = CType(EnumTagihanTraining.TagihanStatus.Disetujui, Short)
                        listDataNew.Add(data)
                    End If
                Next
                funcH.Update(listDataNew)
                MessageBox.Show(String.Format("{0} data berhasil disetujui. {1} data gagal disetujui.", listDataNew.Count, (listDataCheck.Count - listDataNew.Count)))

                BindingGrid(dtgBilling.CurrentPageIndex)
            Catch ex As Exception
                MessageBox.Show("Data gagal disetujui")
            End Try
        End If
    End Sub

End Class
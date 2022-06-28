Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Security

Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports KTB.DNet.BusinessFacade.Helper
Imports OfficeOpenXml
Imports KTB.DNet.BusinessValidation.Helpers

Public Class FrmTOPSPPenaltyList
    Inherits System.Web.UI.Page

    Private objLoginUser As UserInfo
    Private sesHelper As New SessionHelper
    Private uploadPriv As Boolean
    Private oDealer As Dealer
    Private Const MAX_FILE_SIZE As Integer = 1024000
    Private Const SessionGridData As String = "FrmTOPSPPenaltyList.TOPSPPenaltyDetailList"
    Private Const SessionCriteria As String = "FrmTOPSPPenaltyList.CriteriaFrmTOPSPPenaltyDetailList"
    Private TargetDirectory As String = ""
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)

    Private Function TimeStamp() As String
        Return DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & DateTime.Now.Millisecond
    End Function

    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        objLoginUser = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Authorization()

        If Not IsPostBack Then
            ViewState("currSortColumn") = "CreatedTime"
            ViewState("currSortDirection") = Sort.SortDirection.DESC

            ViewState("currSortColumnDetail") = "SparePartBilling.ID"
            ViewState("currSortDirectionDetail") = Sort.SortDirection.ASC

            txtKodeDealer.Text = SesDealer().DealerCode
            hdnDealer.Value = SesDealer().DealerCode
            lblKodeDealer.Text = SesDealer().DealerCode & " / " & SesDealer().DealerName

            PageInit()
            BindDdlLs()

            '-- Restore selection criteria
            ReadCriteria()
            If IsLoginAsDealer() Then
                txtKodeDealer.Attributes("style") = "display:none"
                lnkBtnPopUpDealer.Attributes("style") = "display:none"
                lblKodeDealer.Attributes("style") = "display:table-row"
                btnTransfer.Visible = False
            Else
                lnkBtnPopUpDealer.Attributes("style") = "display:table-row"
                txtKodeDealer.Attributes("style") = "display:table-row"
                lblKodeDealer.Attributes("style") = "display:none"
                txtKodeDealer.Text = ""
                lblChangeStatus.Visible = False
                ddlStatus.Visible = False
                btnProses.Visible = False
            End If
            ReadData()   '-- Read all data matching criteria
            BindGrid(dgTOPSPPenaltyDetailList.CurrentPageIndex)  '-- Bind page-1
        End If

        lnkBtnPopUpDealer.Attributes("onclick") = "ShowPopUpDealer()"
    End Sub

    Private Sub ReadCriteria()
        Dim crit As Hashtable
        crit = CType(sesHelper.GetSession(SessionCriteria), Hashtable)
        If Not crit Is Nothing Then
            txtKodeDealer.Text = CStr(crit.Item("DealerCode"))
            lblKodeDealer.Text = CStr(crit.Item("DealerCode"))
            cbDate.Checked = CBool(crit.Item("cbDate"))
            icDebitMemoDateStart.Value = CStr(crit.Item("icDebitMemoDateStart"))
            icDebitMemoDateEnd.Value = CStr(crit.Item("icDebitMemoDateEnd"))
            txtCreditAccount.Text = CStr(crit.Item("txtCreditAccount"))
            txtRegNumber.Text = CStr(crit.Item("txtRegNumber"))
            txtNoRegPengembalian.Text = CStr(crit.Item("txtNoRegPengembalian"))
            ddlStatusPenalty.SelectedValue = CStr(crit.Item("ddlStatusPenalty"))
            ddlStatusPengembalian.SelectedValue = CStr(crit.Item("ddlStatusPengembalian"))

            txtNoDebitMemo.Text = CStr(crit.Item("txtNoDebitMemo"))
            txtNoBilling.Text = CStr(crit.Item("txtNoBilling"))
            txtNoAccountingDoc.Text = CStr(crit.Item("txtNoAccountingDoc"))
            txtNoClearingDoc.Text = CStr(crit.Item("txtNoClearingDoc"))
            txtNoJV.Text = CStr(crit.Item("txtNoJV"))

            ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
            ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
            dgTOPSPPenaltyDetailList.CurrentPageIndex = CInt(crit.Item("PageIndex"))
        End If
    End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("DealerCode", txtKodeDealer.Text)
        crit.Add("cbDate", cbDate.Checked)
        crit.Add("icDebitMemoDateStart", icDebitMemoDateStart.Value)
        crit.Add("icDebitMemoDateEnd", icDebitMemoDateEnd.Value)
        crit.Add("txtCreditAccount", txtCreditAccount.Text)
        crit.Add("txtRegNumber", txtRegNumber.Text)
        crit.Add("txtNoRegPengembalian", txtNoRegPengembalian.Text)
        crit.Add("ddlStatusPenalty", ddlStatusPenalty.SelectedValue)
        crit.Add("ddlStatusPengembalian", ddlStatusPengembalian.SelectedValue)

        crit.Add("txtNoDebitMemo", txtNoDebitMemo.Text)
        crit.Add("txtNoBilling", txtNoBilling.Text)
        crit.Add("txtNoAccountingDoc", txtNoAccountingDoc.Text)
        crit.Add("txtNoClearingDoc", txtNoClearingDoc.Text)
        crit.Add("txtNoJV", txtNoJV.Text)

        crit.Add("PageIndex", dgTOPSPPenaltyDetailList.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sesHelper.SetSession(SessionCriteria, crit)  '-- Store in session
    End Sub

    Private Sub BindDDLProcessChangeStatus()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumTOPSPPengembalian.Status"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "Sequence", CType(Sort.SortDirection.ASC, Sort.SortDirection)))
        arrDDL = New StandardCodeFacade(User).Retrieve(criterias, sortColl)

        Dim arrDDL2 As New ArrayList
        For Each obj As StandardCode In arrDDL
            Select Case obj.ValueCode
                Case "Validasi", "BatalValidasi"
                    arrDDL2.Add(obj)
            End Select
        Next
        With ddlStatus
            .Items.Clear()
            .DataSource = arrDDL2
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Piih", -1))
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub BindDdlLs()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumTOPSPPenalty.Status"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "Sequence", CType(Sort.SortDirection.ASC, Sort.SortDirection)))
        arrDDL = New StandardCodeFacade(User).Retrieve(criterias, sortColl)
        With ddlStatusPenalty
            .Items.Clear()
            .DataSource = arrDDL
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueID"
            .DataBind()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End With

        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumTOPSPPengembalian.Status"))
        Dim sortColl2 As SortCollection = New SortCollection
        sortColl2.Add(New Sort(GetType(StandardCode), "Sequence", CType(Sort.SortDirection.ASC, Sort.SortDirection)))
        arrDDL = New StandardCodeFacade(User).Retrieve(criterias2, sortColl2)

        Dim arrDDL2 As New ArrayList
        For Each obj As StandardCode In arrDDL
            If obj.ValueCode.Trim <> "BatalValidasi" Then
                arrDDL2.Add(obj)
            End If
        Next

        With ddlStatusPengembalian
            .Items.Clear()
            .DataSource = arrDDL2
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueID"
            .DataBind()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End With

        BindDDLProcessChangeStatus()
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Sub Authorization()
        uploadPriv = True
        If Not SecurityProvider.Authorize(Context.User, SR.TOPSP_Daftar_Penalty_Lihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=TOP SPARE PART - DAFTAR PENALTY")
        Else
            uploadPriv = SecurityProvider.Authorize(Context.User, SR.TOPSP_Daftar_Penalty_Upload_Privilege)
        End If
    End Sub

    Private Sub PageInit()
        icDebitMemoDateStart.Value = Date.Now.AddMonths(-1)
        icDebitMemoDateEnd.Value = Date.Now
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim arrTOPSPPenaltyDetailList As ArrayList = CType(sesHelper.GetSession(SessionGridData), ArrayList)
        If arrTOPSPPenaltyDetailList.Count > 0 Then
            CommonFunction.SortListControl(arrTOPSPPenaltyDetailList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrTOPSPPenaltyDetailList, pageIndex, dgTOPSPPenaltyDetailList.PageSize)
            dgTOPSPPenaltyDetailList.DataSource = PagedList
            dgTOPSPPenaltyDetailList.VirtualItemCount = arrTOPSPPenaltyDetailList.Count()
            dgTOPSPPenaltyDetailList.DataBind()

            lblChangeStatus0.Visible = True
            If Not IsLoginAsDealer() Then
                lblChangeStatus.Visible = False
                ddlStatus.Visible = False
                btnProses.Visible = False
            Else
                lblChangeStatus.Visible = True
                ddlStatus.Visible = True
                btnProses.Visible = True
            End If
        Else
            dgTOPSPPenaltyDetailList.DataSource = New ArrayList
            dgTOPSPPenaltyDetailList.VirtualItemCount = 0
            dgTOPSPPenaltyDetailList.CurrentPageIndex = 0
            dgTOPSPPenaltyDetailList.DataBind()

            lblChangeStatus0.Visible = False
            lblChangeStatus.Visible = False
            ddlStatus.Visible = False
            btnProses.Visible = False
        End If
    End Sub

    Private Sub ReadData()
        '-- Row status = active
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPPenaltyDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If cbDate.Checked Then
            Dim tglFrom As New Date(icDebitMemoDateStart.Value.Year, icDebitMemoDateStart.Value.Month, icDebitMemoDateStart.Value.Day, 0, 0, 0)
            Dim tglTo As New Date(icDebitMemoDateEnd.Value.Year, icDebitMemoDateEnd.Value.Month, icDebitMemoDateEnd.Value.Day, 23, 59, 59)
            If icDebitMemoDateStart.Value = "#12:00:00 AM#" OrElse icDebitMemoDateEnd.Value = "#12:00:00 AM#" Then
                MessageBox.Show("Format Tanggal yang anda masukkan salah")
                Exit Sub
            End If
            crit.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "TOPSPPenalty.DebitMemoDate", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "(", True)
            crit.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "TOPSPPenalty.DebitMemoDate", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), ")", False)
        End If

        If txtKodeDealer.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "TOPSPPenalty.Dealer.DealerCode", MatchType.Partial, txtKodeDealer.Text))
        End If
        If txtCreditAccount.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "TOPSPPenalty.Dealer.CreditAccount", MatchType.Partial, txtCreditAccount.Text.Trim))
        End If
        If txtRegNumber.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "TOPSPPenalty.TOPSPTransferPayment.RegNumber", MatchType.Partial, txtRegNumber.Text.Trim))
        End If
        If txtNoRegPengembalian.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "TOPSPPenalty.NoRegPengembalian", MatchType.Partial, txtNoRegPengembalian.Text.Trim))
        End If
        If ddlStatusPenalty.SelectedIndex > 0 Then
            crit.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "TOPSPPenalty.StatusPenalty", MatchType.Exact, ddlStatusPenalty.SelectedValue))
        End If
        If ddlStatusPengembalian.SelectedIndex > 0 Then
            crit.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "TOPSPPenalty.StatusPengembalian", MatchType.Exact, ddlStatusPengembalian.SelectedValue))
        End If

        If txtNoDebitMemo.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "TOPSPPenalty.DebitMemoNumber", MatchType.Partial, txtNoDebitMemo.Text.Trim))
        End If
        If txtNoBilling.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "SparePartBilling.BillingNumber", MatchType.Partial, txtNoBilling.Text.Trim))
        End If
        If txtNoAccountingDoc.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "TOPSPPenalty.AccountingNumber", MatchType.Partial, txtNoAccountingDoc.Text.Trim))
        End If
        If txtNoClearingDoc.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "TOPSPPenalty.ClearingNumber", MatchType.Partial, txtNoClearingDoc.Text.Trim))
        End If
        If txtNoJV.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "TOPSPPenalty.JVNumber", MatchType.Partial, txtNoJV.Text.Trim))
        End If
        If txtNilaiPenalty.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "AmountPenalty", MatchType.Exact, txtNilaiPenalty.Text.Trim))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(TOPSPPenaltyDetail), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        '-- Retrieve recordset
        Dim arrTOPSPPenaltyDetailList As ArrayList = New TOPSPPenaltyDetailFacade(User).RetrieveByCriteria(crit, sortColl)
        sesHelper.SetSession(SessionGridData, arrTOPSPPenaltyDetailList)
        If arrTOPSPPenaltyDetailList.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ReadData()
        dgTOPSPPenaltyDetailList.CurrentPageIndex = 0 '-- Read all data matching criteria
        BindGrid(dgTOPSPPenaltyDetailList.CurrentPageIndex)  '-- Bind page-1
        StoreCriteria()
    End Sub

    Private Sub dgTOPSPPenaltyDetailList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgTOPSPPenaltyDetailList.PageIndexChanged
        '-- Change datagrid page

        ReadData()
        dgTOPSPPenaltyDetailList.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
        StoreCriteria()
    End Sub

    Private Sub dgTOPSPPenaltyDetailList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgTOPSPPenaltyDetailList.SortCommand
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
        dgTOPSPPenaltyDetailList.CurrentPageIndex = 0
        ReadData()
        BindGrid(dgTOPSPPenaltyDetailList.CurrentPageIndex)
        StoreCriteria()
    End Sub

    Protected Sub dgTOPSPPenaltyDetailList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgTOPSPPenaltyDetailList.ItemCommand
        Select Case e.CommandName
            Case "DownloadDebitMemo"
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)
            Case "DownloadBuktiPotong"
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)
                Dim objTOPSPPenaltyDetail As TOPSPPenaltyDetail = New TOPSPPenaltyDetailFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
                If Not IsNothing(objTOPSPPenaltyDetail) AndAlso objTOPSPPenaltyDetail.ID > 0 Then
                    Dim objTOPSPPenalty As TOPSPPenalty = New TOPSPPenaltyFacade(User).Retrieve(objTOPSPPenaltyDetail.TOPSPPenalty.ID)
                    If Not IsNothing(objTOPSPPenalty) AndAlso objTOPSPPenalty.ID > 0 Then
                        objTOPSPPenalty.LastDownloadby = objLoginUser.UserName
                        objTOPSPPenalty.DownloadedDate = Date.Now
                        Dim _result As Integer = New TOPSPPenaltyFacade(User).Update(objTOPSPPenalty)
                    End If
                End If
            Case "UploadBuktiPotong"
                Dim objTOPSPPenaltyDetail As TOPSPPenaltyDetail = New TOPSPPenaltyDetailFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
                If Not IsNothing(objTOPSPPenaltyDetail) AndAlso objTOPSPPenaltyDetail.ID > 0 Then
                    hdnTOPSPPenaltyID.Value = objTOPSPPenaltyDetail.TOPSPPenalty.ID
                    Dim dblAmountPPh As Double = 0
                    Dim objTOPSPPenalty As TOPSPPenalty = New TOPSPPenaltyFacade(User).Retrieve(CInt(hdnTOPSPPenaltyID.Value))
                    If objTOPSPPenalty.NoRegPengembalian <> "" Then
                        lblNoRegPengembalianUpload.Text = objTOPSPPenalty.NoRegPengembalian
                        dblAmountPPh = objTOPSPPenalty.AmountPPh
                    Else
                        Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objTOPSPPenalty.PaymentDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_1").ValueId)
                        'dblAmountPPh = 0.15 * objTOPSPPenalty.Amount
                        dblAmountPPh = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=objTOPSPPenalty.Amount)
                    End If
                    lblDebitMemoNumber.Text = objTOPSPPenalty.DebitMemoNumber
                    txtNoBuktiPotong.Text = objTOPSPPenalty.NoBuktiPotong
                    lblNoBuktiPotong.Text = objTOPSPPenalty.NoBuktiPotong
                    lblBuktiPotongDate.Text = Date.Now.ToString("dd/MM/yyyy")

                    lblAmountPPh.Text = Format(dblAmountPPh, "#,##0")
                    If objTOPSPPenalty.UploadFilePath <> "" Then
                        If FileUpload.Visible = True Then
                            lblFileUpload.Text = "<br/>" & Path.GetFileName(TargetDirectory & objTOPSPPenalty.UploadFilePath)
                        Else
                            lblFileUpload.Text = Path.GetFileName(TargetDirectory & objTOPSPPenalty.UploadFilePath)
                        End If
                    End If
                    BindGridBilling(0)
                    txtNoBuktiPotong.Visible = uploadPriv
                    lblNoBuktiPotong.Visible = Not uploadPriv
                    FileUpload.Visible = uploadPriv
                    btnSimpan.Visible = uploadPriv
                    If Not IsLoginAsDealer() OrElse
                        objTOPSPPenalty.StatusPenalty <> 1 OrElse
                        objTOPSPPenalty.StatusPengembalian > 0 Then  'bukan status selesai orelse 'selain Baru
                        txtNoBuktiPotong.Visible = False
                        lblNoBuktiPotong.Visible = True
                        FileUpload.Visible = False
                        btnSimpan.Visible = False
                    End If
                    pnlDaftar.Enabled = False
                    pnlUpload.Visible = True
                    txtNoBuktiPotong.Focus()
                End If
            Case Else
        End Select
    End Sub

    Private Sub btnProses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProses.Click
        Dim sb As StringBuilder = New StringBuilder

        If ddlStatus.SelectedIndex = 0 Then
            MessageBox.Show("Pilih daftar status terlebih dahulu")
            Exit Sub
        End If
        Dim objTOPSPPenalty As New TOPSPPenalty
        Dim objTOPSPPenaltyFacade As TOPSPPenaltyFacade = New TOPSPPenaltyFacade(User)
        Dim intTOPSPPenaltyID As Integer = 0

        Dim nResult As Integer
        Dim checkCounter As Integer = 0

        ' Status Report :
        '0: Baru
        '1: Validasi
        '2: Konfirmasi
        '3: Selesai
        '4: BatalValidasi

        If dgTOPSPPenaltyDetailList.Items.Count > 0 Then
            For Each item As DataGridItem In dgTOPSPPenaltyDetailList.Items
                Dim chckbox As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
                Dim StrVal As String = String.Empty
                intTOPSPPenaltyID = 0
                Dim objTOPSPPenaltyDetail As TOPSPPenaltyDetail = New TOPSPPenaltyDetailFacade(User).Retrieve(Convert.ToInt32(item.Cells(0).Text))
                If Not IsNothing(objTOPSPPenaltyDetail) AndAlso objTOPSPPenaltyDetail.ID > 0 Then
                    intTOPSPPenaltyID = objTOPSPPenaltyDetail.TOPSPPenalty.ID
                End If
                objTOPSPPenalty = objTOPSPPenaltyFacade.Retrieve(intTOPSPPenaltyID)
                If Not IsNothing(objTOPSPPenalty) AndAlso objTOPSPPenalty.ID > 0 Then
                    If chckbox.Checked Then
                        Dim strStatusName As String = CType(New StandardCodeFacade(User).RetrieveByValueId(objTOPSPPenalty.StatusPengembalian.ToString(), "EnumTOPSPPengembalian.Status")(0), StandardCode).ValueCode
                        If objTOPSPPenalty.UploadFilePath = "" Then
                            sb.Append("Item No " & item.ItemIndex + 1 & " : " & " tidak dapat diproses, Bukti Potong belum diupload" & "\n")
                        Else
                            If objTOPSPPenalty.StatusPengembalian <> 1 And CByte(ddlStatus.SelectedValue) = 4 Then   'status BatalValidasi
                                StrVal = "Batal Validasi hanya dapat dilakukan untuk data yang statusnya Validasi"
                                sb.Append("Item No " & item.ItemIndex + 1 & " : Status " & strStatusName & " tidak bisa diubah ke status Baru\n" & StrVal & "\n")
                            End If
                            If objTOPSPPenalty.StatusPengembalian <> 0 And CByte(ddlStatus.SelectedValue) = 1 Then   'status Validasi
                                StrVal = "Ubah status menjadi Validasi hanya dapat dilakukan untuk data statusnya Baru"
                                sb.Append("Item No " & item.ItemIndex + 1 & " : Status " & strStatusName & " tidak bisa diubah ke status Validasi\n" & StrVal & "\n")
                            End If
                        End If
                    End If
                End If
            Next
            If (sb.ToString().Length > 0) Then
                MessageBox.Show(sb.ToString())
                Exit Sub
            End If

            For Each item As DataGridItem In dgTOPSPPenaltyDetailList.Items
                Dim chckbox As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
                intTOPSPPenaltyID = 0
                Dim objTOPSPPenaltyDetail As TOPSPPenaltyDetail = New TOPSPPenaltyDetailFacade(User).Retrieve(Convert.ToInt32(item.Cells(0).Text))
                If Not IsNothing(objTOPSPPenaltyDetail) AndAlso objTOPSPPenaltyDetail.ID > 0 Then
                    intTOPSPPenaltyID = objTOPSPPenaltyDetail.TOPSPPenalty.ID
                End If
                objTOPSPPenalty = objTOPSPPenaltyFacade.Retrieve(intTOPSPPenaltyID)
                If Not IsNothing(objTOPSPPenalty) AndAlso objTOPSPPenalty.ID > 0 Then
                    If chckbox.Checked Then
                        If ddlStatus.SelectedValue = 4 Then   'Jika ststus nya Batal Validasi
                            objTOPSPPenalty.StatusPengembalian = 0  ' jadi status Baru
                        Else
                            objTOPSPPenalty.StatusPengembalian = ddlStatus.SelectedValue
                        End If

                        nResult = objTOPSPPenaltyFacade.Update(objTOPSPPenalty)
                        If nResult = -1 Then
                            sb.Append("Nomor Reg Payment : " & objTOPSPPenalty.TOPSPTransferPayment.RegNumber & " tidak bisa di update status\n")
                        End If
                        checkCounter = checkCounter + 1
                    End If
                End If
            Next
        Else
            MessageBox.Show("Daftar Penalty masih kosong")
            Exit Sub
        End If

        If (checkCounter > 0) Then
            If (sb.ToString().Length > 0) Then
                MessageBox.Show(sb.ToString())
            End If
            MessageBox.Show("Proses update status sukses")
        Else
            MessageBox.Show("Silahkan pilih item laporan")
        End If

        ddlStatus.ClearSelection()
        ddlStatus.SelectedIndex = 0
        ReadData()
        BindGrid(0)
    End Sub

    Private Function GetCheckedItem() As ArrayList
        Dim intTOPSPPenaltyID As Integer = 0
        Dim arlCheckedItem As ArrayList = New ArrayList
        For Each dtgItem As DataGridItem In dgTOPSPPenaltyDetailList.Items
            If CType(dtgItem.FindControl("chkItemChecked"), CheckBox).Checked Then
                intTOPSPPenaltyID = 0
                Dim objTOPSPPenaltyDetail As TOPSPPenaltyDetail = New TOPSPPenaltyDetailFacade(User).Retrieve(Convert.ToInt32(dtgItem.Cells(0).Text))
                If Not IsNothing(objTOPSPPenaltyDetail) AndAlso objTOPSPPenaltyDetail.ID > 0 Then
                    intTOPSPPenaltyID = objTOPSPPenaltyDetail.TOPSPPenalty.ID
                End If
                Dim objTOPSPPenalty As TOPSPPenalty = New TOPSPPenaltyFacade(User).Retrieve(intTOPSPPenaltyID)
                If Not IsNothing(objTOPSPPenalty) AndAlso objTOPSPPenalty.ID > 0 Then
                    If isExistTOPSPPenaltyID(arlCheckedItem, objTOPSPPenalty.ID) = False Then
                        arlCheckedItem.Add(objTOPSPPenalty)
                    End If
                End If
            End If
        Next
        Return arlCheckedItem
    End Function

    Function isExistTOPSPPenaltyID(ByVal arlTopSPPenalty As ArrayList, ByVal objTOPSPPenaltyID As Integer) As Boolean
        For Each obj As TOPSPPenalty In arlTopSPPenalty
            If obj.ID = objTOPSPPenaltyID Then
                Return True
            End If
        Next
        Return False
    End Function

    Protected Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        'Tambahkan logic ketika tombol Transfer dan Transfer Ulang dg ketentuan berikut :
        '1.Update TOPSPPenalty.StatusPengembalian = 2  -> Konfirmasi
        '2.Buat WSM transfer ke SAP (lihat detail di sheet WSM to SAP)
        'create txt file to folder :
        'QA: \\172.17.31.121\MDnet\SAP\Sparepart\Penalty
        'Prod: \\172.17.31.62\MDNet\SAP\Sparepart\Penalty
        'nama File : JVRETURSPPENALTY [Timestamp].txt
        Dim sb As StringBuilder = New StringBuilder
        Dim StrVal As String = String.Empty
        Dim arrchk As ArrayList = GetCheckedItem()
        If Not IsNothing(arrchk) AndAlso arrchk.Count = 0 Then
            MessageBox.Show("Tidak ada data Penalty yang di pilih")
            Exit Sub
        ElseIf IsNothing(arrchk) Then
            MessageBox.Show("Tidak ada data Penalty yang di pilih")
            Exit Sub
        End If
        Dim i% = 1
        For Each objPenalty As TOPSPPenalty In arrchk
            Dim strStatusName As String = CType(New StandardCodeFacade(User).RetrieveByValueId(objPenalty.StatusPengembalian.ToString(), "EnumTOPSPPengembalian.Status")(0), StandardCode).ValueCode
            If objPenalty.StatusPengembalian <> 1 Then
                sb.Append(i.ToString() & ". " & objPenalty.DebitMemoNumber & "\n")
                i += 1
            End If
        Next
        If (sb.ToString().Length > 0) Then
            StrVal = "Send to SAP hanya dapat dilakukan untuk data yang statusnya Validasi"
            MessageBox.Show(StrVal & "\nDebit Memo Number:\n" & sb.ToString())
            Exit Sub
        End If

        sb = New StringBuilder
        Dim sbAccountingNumber As StringBuilder = New StringBuilder
        Dim sbNoRegPengembalianPPh As StringBuilder = New StringBuilder
        Dim sbNoBuktiPotong As StringBuilder = New StringBuilder
        Dim sbAmountPPh As StringBuilder = New StringBuilder
        Dim errFlag As Boolean = False
        Dim arrSuccess As New ArrayList

        For Each oBjPenalty As TOPSPPenalty In arrchk
            errFlag = False
            If oBjPenalty.AccountingNumber = "" Then
                sbAccountingNumber.Append(oBjPenalty.DebitMemoNumber & ", ")
                errFlag = True
            End If
            If oBjPenalty.NoRegPengembalian = "" Then
                sbNoRegPengembalianPPh.Append(oBjPenalty.DebitMemoNumber & ", ")
                errFlag = True
            End If
            If oBjPenalty.NoBuktiPotong = "" Then
                sbNoBuktiPotong.Append(oBjPenalty.DebitMemoNumber & ", ")
                errFlag = True
            End If
            If oBjPenalty.AmountPPh = "0" Then
                sbAmountPPh.Append(oBjPenalty.DebitMemoNumber & ", ")
                errFlag = True
            End If
            If errFlag = True Then
                Continue For
            End If
            oBjPenalty.StatusPengembalian = 2 'Konfirmasi
            arrSuccess.Add(oBjPenalty)
        Next

        If sbAccountingNumber.ToString().Trim <> "" Then
            sb.Append("- Accounting Number masih kosong untuk Nomor Debit Memo: " & Left(sbAccountingNumber.ToString().Trim, Len(sbAccountingNumber.ToString().Trim) - 1) & ".\n\n")
        End If
        If sbNoRegPengembalianPPh.ToString().Trim <> "" Then
            sb.Append("- NoReg Pengembalian PPh masih kosong untuk Nomor Debit Memo: " & Left(sbNoRegPengembalianPPh.ToString().Trim, Len(sbNoRegPengembalianPPh.ToString().Trim) - 1) & ".\n\n")
        End If
        If sbNoBuktiPotong.ToString().Trim <> "" Then
            sb.Append("- Nomor Bukti Potong masih kosong untuk Nomor Debit Memo: " & Left(sbNoBuktiPotong.ToString().Trim, Len(sbNoBuktiPotong.ToString().Trim) - 1) & ".\n\n")
        End If
        If sbAmountPPh.ToString().Trim <> "" Then
            sb.Append("- Amount PPh masih kosong untuk Nomor Debit Memo: " & Left(sbAmountPPh.ToString().Trim, Len(sbAmountPPh.ToString().Trim) - 1) & ".\n\n")
        End If
        If sb.ToString().Trim <> "" Then
            MessageBox.Show(sb.ToString())
        End If

        WriteDataPenalty(arrSuccess)

        ReadData()
        dgTOPSPPenaltyDetailList.CurrentPageIndex = 0
        BindGrid(dgTOPSPPenaltyDetailList.CurrentPageIndex)
        StoreCriteria()
    End Sub

    Private Sub WriteDataPenalty(arrSuccess As ArrayList)
        Dim _return As Integer = 0
        Dim lines As New StringBuilder
        If arrSuccess.Count = 0 Then
            MessageBox.Show("Tidak ada data Penalty yang di pilih")
            Exit Sub
        End If
        Dim separator As String = ";"
        Dim index As Integer = 0
        Dim errorMes As String = ""

        For Each oPenalty As TOPSPPenalty In arrSuccess
            Dim line As New System.Text.StringBuilder
            'DealerCode;DebitMemo;AccountingDocNo;NoRegPengembalianPPh;NoBuktiPotong;AmountPPh;BuktiPotongDate

            line.Append(oPenalty.Dealer.DealerCode)
            line.Append(separator)
            line.Append(oPenalty.DebitMemoNumber)
            line.Append(separator)
            line.Append(oPenalty.AccountingNumber)
            line.Append(separator)
            line.Append(oPenalty.NoRegPengembalian)
            line.Append(separator)
            line.Append(oPenalty.NoBuktiPotong)
            line.Append(separator)
            line.Append(Math.Round(oPenalty.AmountPPh))
            line.Append(separator)
            line.Append(oPenalty.BuktiPotongDate.ToString("yyyyMMdd"))
            line.Append(vbNewLine)
            lines.Append(line)
        Next
        If arrSuccess.Count > 0 Then
            _return = New TOPSPPenaltyFacade(User).UpdateStatusTransaction(arrSuccess)
            If _return > 0 Then
                If lines.ToString().Trim <> "" Then
                    Try
                        DoSendSAP(lines)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message & "\nTidak ada data Penalty yang diupload ke SAP")
                    End Try
                Else
                    MessageBox.Show("Tidak ada data Penalty yang diupload ke SAP")
                End If
            Else
                MessageBox.Show("Send to SAP Gagal")
            End If
        Else
            MessageBox.Show("Tidak ada data Penalty yang diupload ke SAP")
        End If
    End Sub

    Private Sub DoSendSAP(ByVal lines As StringBuilder)
        Dim datetimenow As String = Now.ToString("yyyyMMddHmmss")
        Dim PenaltyDataPath As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\Sparepart\Penalty\JVRETURSPPENALTY[" & datetimenow & "].txt"

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        imp.Start()
        Try
            Dim dirInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(Path.GetDirectoryName(PenaltyDataPath))

            If Not dirInfo.Exists Then
                dirInfo.Create()
            End If
            Dim fs As FileStream = New FileStream(PenaltyDataPath, FileMode.CreateNew)
            Dim sw As StreamWriter = New StreamWriter(fs)

            sw.WriteLine(lines.ToString)
            sw.Close()
            fs.Close()

            MessageBox.Show("Data berhasil diupload ke SAP")

        Catch ex As Exception
            Dim errMess As String = ex.Message
        End Try
        Dim debug = ""
    End Sub

    Protected Sub dgTOPSPPenaltyDetailList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgTOPSPPenaltyDetailList.ItemDataBound
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
        Dim lbtnRegNumber As LinkButton = CType(e.Item.FindControl("lbtnRegNumber"), LinkButton)
        Dim lblBillingNumber As Label = CType(e.Item.FindControl("lblBillingNumber"), Label)
        Dim lblAmountPenalty As Label = CType(e.Item.FindControl("lblAmountPenalty"), Label)
        Dim lblDebitMemoDate As Label = CType(e.Item.FindControl("lblDebitMemoDate"), Label)
        Dim lnkbtnDownloadDebitMemo As LinkButton = CType(e.Item.FindControl("lnkbtnDownloadDebitMemo"), LinkButton)
        Dim lblDebitMemoNumber As Label = CType(e.Item.FindControl("lblDebitMemoNumber"), Label)
        Dim lblAccountingNumber As Label = CType(e.Item.FindControl("lblAccountingNumber"), Label)
        Dim lblClearingNumber As Label = CType(e.Item.FindControl("lblClearingNumber"), Label)
        Dim lblPaymentDate As Label = CType(e.Item.FindControl("lblPaymentDate"), Label)
        Dim lblStatusPenalty As Label = CType(e.Item.FindControl("lblStatusPenalty"), Label)
        Dim lblMessage As Label = CType(e.Item.FindControl("lblMessage"), Label)
        Dim lblNoRegPengembalian As Label = CType(e.Item.FindControl("lblNoRegPengembalian"), Label)
        Dim lblNoBuktiPotong As Label = CType(e.Item.FindControl("lblNoBuktiPotong"), Label)
        Dim lnkbtnDownloadBuktiPotong As LinkButton = CType(e.Item.FindControl("lnkbtnDownloadBuktiPotong"), LinkButton)
        Dim lblStatusPengembalian As Label = CType(e.Item.FindControl("lblStatusPengembalian"), Label)
        Dim chkItemChecked As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
        Dim lblDueDate As Label = CType(e.Item.FindControl("lblDueDate"), Label)
        Dim lblTransferActualDate As Label = CType(e.Item.FindControl("lblTransferActualDate"), Label)
        Dim lblKeterlambatanHari As Label = CType(e.Item.FindControl("lblKeterlambatanHari"), Label)
        Dim lblJVNumber As Label = CType(e.Item.FindControl("lblJVNumber"), Label)
        Dim lblPPh As Label = CType(e.Item.FindControl("lblPPh"), Label)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As TOPSPPenaltyDetail = CType(e.Item.DataItem, TOPSPPenaltyDetail)
            lblNo.Text = (e.Item.ItemIndex + 1 + (dgTOPSPPenaltyDetailList.PageSize * dgTOPSPPenaltyDetailList.CurrentPageIndex)).ToString

            If Not IsNothing(oData.TOPSPPenalty.Dealer) Then
                lblDealerCode.Text = oData.TOPSPPenalty.Dealer.DealerCode
            End If
            lbtnRegNumber.Text = oData.TOPSPPenalty.TOPSPTransferPayment.RegNumber
            lbtnRegNumber.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpTOPSPPenaltyTransferActual.aspx?TransferPaymentID=" & oData.TOPSPPenalty.TOPSPTransferPayment.ID & "', '', 600, 800, KTBNote);return false;")
            lblBillingNumber.Text = oData.SparePartBilling.BillingNumber
            lblDueDate.Text = oData.TOPSPPenalty.TOPSPTransferPayment.DueDate.ToString("dd/MM/yyyy")
            lblTransferActualDate.Text = oData.ActualTransferDate.ToString("dd/MM/yyyy")
            lblKeterlambatanHari.Text = oData.PenaltyDays
            lblAmountPenalty.Text = Format(oData.AmountPenalty, "#,##0")
            lblDebitMemoDate.Text = oData.TOPSPPenalty.DebitMemoDate.ToString("dd/MM/yyyy")
            lblDebitMemoNumber.Text = oData.TOPSPPenalty.DebitMemoNumber
            lnkbtnDownloadDebitMemo.CommandArgument = oData.TOPSPPenalty.DebitMemoPath
            lblAccountingNumber.Text = oData.TOPSPPenalty.AccountingNumber
            lblClearingNumber.Text = oData.TOPSPPenalty.ClearingNumber
            If oData.TOPSPPenalty.PaymentDate.ToString("dd/MM/yyyy") <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime).ToString("dd/MM/yyyy") AndAlso _
                oData.TOPSPPenalty.PaymentDate.ToString("dd/MM/yyyy") <> "01/01/1900" Then
                lblPaymentDate.Text = oData.TOPSPPenalty.PaymentDate.ToString("dd/MM/yyyy")
            End If
            lblStatusPenalty.Text = CommonFunction.GetEnumDescription(oData.TOPSPPenalty.StatusPenalty, "EnumTOPSPPenalty.Status")
            lblMessage.Text = oData.TOPSPPenalty.Message

            lblNoRegPengembalian.Text = oData.TOPSPPenalty.NoRegPengembalian
            lblNoBuktiPotong.Text = oData.TOPSPPenalty.NoBuktiPotong
            lnkbtnDownloadBuktiPotong.CommandArgument = oData.TOPSPPenalty.UploadFilePath
            If oData.TOPSPPenalty.UploadFilePath <> "" Then
                lblStatusPengembalian.Text = CommonFunction.GetEnumDescription(oData.TOPSPPenalty.StatusPengembalian, "EnumTOPSPPengembalian.Status")
            End If
            lblJVNumber.Text = oData.TOPSPPenalty.JVNumber
            lblPPh.Text = Format(oData.PPh, "#,##0")

            Dim lnkbtnUploadBuktiPotong As LinkButton = CType(e.Item.FindControl("lnkbtnUploadBuktiPotong"), LinkButton)
            lnkbtnUploadBuktiPotong.Visible = True

            If IsLoginAsDealer() Then
                Select Case oData.TOPSPPenalty.StatusPengembalian
                    Case 0, 1        '--Baru, Validasi
                        chkItemChecked.Visible = True
                        If oData.TOPSPPenalty.NoBuktiPotong = "" Then
                            chkItemChecked.Visible = False
                        End If
                    Case Else
                        chkItemChecked.Visible = False
                End Select
            Else
                Select Case oData.TOPSPPenalty.StatusPengembalian
                    Case 1       '---Validasi
                        chkItemChecked.Visible = True
                    Case Else
                        chkItemChecked.Visible = False
                End Select
            End If
            If oData.TOPSPPenalty.UploadFilePath = "" Then
                lnkbtnDownloadBuktiPotong.Visible = False
            Else
                lnkbtnDownloadBuktiPotong.Visible = True
            End If
            If oData.TOPSPPenalty.DebitMemoPath = "" Then
                lnkbtnDownloadDebitMemo.Visible = False
            Else
                lnkbtnDownloadDebitMemo.Visible = True
            End If
        End If
    End Sub

    Protected Sub hdnDealer_ValueChanged(sender As Object, e As EventArgs) Handles hdnDealer.ValueChanged
        Dim data As String() = hdnDealer.Value.Trim.Split(";")
        txtKodeDealer.Text = data(0)
    End Sub

    Protected Sub btnDownloadExcel_Click(sender As Object, e As EventArgs) Handles btnDownloadExcel.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        If dgTOPSPPenaltyDetailList.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        ' mengambil data yang dibutuhkan
        arrData = CType(sesHelper.GetSession(SessionGridData), ArrayList)
        If arrData.Count > 0 Then
            CreateExcel("DAFTAR LIST PENALTY", arrData)
        End If
    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()
            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Kode Dealer"
            ws.Cells("C3").Value = "Nomor Reg Payment"
            ws.Cells("D3").Value = "No. Billing"
            ws.Cells("E3").Value = "Tanggal Jatuh Tempo"
            ws.Cells("F3").Value = "Tanggal Aktual Transfer"
            ws.Cells("G3").Value = "Keterlambatan (Hari)"
            ws.Cells("H3").Value = "Nilai Penalty"
            ws.Cells("I3").Value = "Tanggal Debit Memo"
            ws.Cells("J3").Value = "No Debit Memo"
            ws.Cells("K3").Value = "No Accounting Doc"
            ws.Cells("L3").Value = "No Clearing Doc"
            ws.Cells("M3").Value = "Tgl Pembayaran Penalty"
            ws.Cells("N3").Value = "Status Penalty"
            ws.Cells("O3").Value = "Nilai PPh"
            ws.Cells("P3").Value = "No Reg Pengembalian PPh"
            ws.Cells("Q3").Value = "No Bukti Potong"
            ws.Cells("R3").Value = "No JV"
            ws.Cells("S3").Value = "Status Pengembalian PPh"

            For i As Integer = 0 To Data.Count - 1
                Dim item As TOPSPPenaltyDetail = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                If IsNothing(item.TOPSPPenalty.Dealer) Then
                    ws.Cells(i + 4, 2).Value = ""
                Else
                    ws.Cells(i + 4, 2).Value = item.TOPSPPenalty.Dealer.DealerCode
                End If
                ws.Cells(i + 4, 3).Value = item.TOPSPPenalty.TOPSPTransferPayment.RegNumber
                ws.Cells(i + 4, 4).Value = item.SparePartBilling.BillingNumber
                ws.Cells(i + 4, 5).Value = item.TOPSPPenalty.TOPSPTransferPayment.DueDate.ToString("dd/MM/yyyy")
                ws.Cells(i + 4, 6).Value = item.ActualTransferDate.ToString("dd/MM/yyyy")
                ws.Cells(i + 4, 7).Value = item.PenaltyDays
                ws.Cells(i + 4, 8).Value = Format(item.AmountPenalty, "#,##0")
                ws.Cells(i + 4, 9).Value = item.TOPSPPenalty.DebitMemoDate.ToString("dd/MM/yyyy")
                ws.Cells(i + 4, 10).Value = item.TOPSPPenalty.DebitMemoNumber
                ws.Cells(i + 4, 11).Value = item.TOPSPPenalty.AccountingNumber
                ws.Cells(i + 4, 12).Value = item.TOPSPPenalty.ClearingNumber
                ws.Cells(i + 4, 13).Value = item.TOPSPPenalty.PaymentDate.ToString("dd/MM/yyyy")
                ws.Cells(i + 4, 14).Value = CommonFunction.GetEnumDescription(item.TOPSPPenalty.StatusPenalty, "EnumTOPSPPenalty.Status")
                ws.Cells(i + 4, 15).Value = Format(item.PPh, "#,##0")
                ws.Cells(i + 4, 16).Value = item.TOPSPPenalty.NoRegPengembalian
                ws.Cells(i + 4, 17).Value = item.TOPSPPenalty.NoBuktiPotong
                ws.Cells(i + 4, 18).Value = item.TOPSPPenalty.JVNumber
                If item.TOPSPPenalty.NoRegPengembalian.Trim = "" Then
                    ws.Cells(i + 4, 19).Value = ""
                Else
                    ws.Cells(i + 4, 19).Value = CommonFunction.GetEnumDescription(item.TOPSPPenalty.StatusPengembalian, "EnumTOPSPPengembalian.Status")
                End If
            Next
            CreateExcelFile(pck, FileName & " [" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & "].xls")
        End Using
    End Sub

    Private Function CreateSheet(pck As ExcelPackage, sheetName As String) As ExcelWorksheet
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(sheetName)
        ws.View.ShowGridLines = False
        Return ws
    End Function

    Private Sub CreateExcelFile(pck As ExcelPackage, fileName As String)
        Dim fileBytes = pck.GetAsByteArray()
        Response.Clear()

        Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        Response.AppendHeader("Content-Disposition", String.Format("attachment; filename=""{0}""; size={1}; creation-date={2}; modification-date={2}; read-date={2}", fileName, fileBytes.Length, DateTime.Now.ToString("R")))
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"  'xlsx
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()
    End Sub

    Private Function GetRegNumber() As String
        Dim YearParameter As Integer = Date.Today.Year
        If Date.Today.Month = 12 Then
            YearParameter = Date.Today.Year + 1
        End If
        Dim crit As New CriteriaComposite(New Criteria(GetType(TOPSPPenalty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(TOPSPPenalty), "BuktiPotongDate", MatchType.GreaterOrEqual, Date.Today.Year & Date.Today.Month.ToString("d2") & "01"))
        crit.opAnd(New Criteria(GetType(TOPSPPenalty), "BuktiPotongDate", MatchType.Lesser, YearParameter & Date.Today.AddMonths(1).Month.ToString("d2") & "01"))
        Dim arrl As ArrayList = New TOPSPPenaltyFacade(User).Retrieve(crit)
        Dim _return As String
        If arrl.Count > 0 Then
            Dim obj As TOPSPPenalty = CommonFunction.SortListControl(arrl, "NoRegPengembalian", Sort.SortDirection.DESC)(0)
            Dim noReg As String = obj.NoRegPengembalian
            If noReg = "" Then
                _return = "P" & Date.Today.ToString("yy") & CInt(1).ToString("d5")
            Else
                _return = "P" & Date.Today.ToString("yy") & (CInt(noReg.Substring(3, 5)) + 1).ToString("d5")
            End If
        Else
            _return = "P" & Date.Today.ToString("yy") & CInt(1).ToString("d5")
        End If
        Return _return
    End Function

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim objPostedData As HttpPostedFile = Nothing
        Dim objTOPSPPenalty As TOPSPPenalty = New TOPSPPenalty()
        Dim sFileName As String

        '========= Validasi Upload =======================================================================
        If txtNoBuktiPotong.Text.Trim = "" Then
            MessageBox.Show("Nomor Bukti Potong harap diisi")
            Exit Sub
        End If
        Dim _filename As String = String.Empty
        If lblFileUpload.Text = "" OrElse (Not IsNothing(FileUpload) AndAlso FileUpload.Value <> String.Empty) Then
            If IsNothing(FileUpload) OrElse FileUpload.Value = String.Empty Then
                MessageBox.Show("Upload Bukti Potong harap diisi")
                Exit Sub
            End If
            _filename = System.IO.Path.GetFileName(FileUpload.PostedFile.FileName)
            If _filename.Trim().Length <= 0 Then
                MessageBox.Show("Upload Bukti Potong harap diisi")
                Exit Sub
            End If

            If _filename.Trim().Length > 0 Then
                If FileUpload.PostedFile.ContentLength > MAX_FILE_SIZE Then
                    MessageBox.Show("Ukuran file tidak boleh melebihi " & (MAX_FILE_SIZE / 1024) & "kb\n")
                    Exit Sub
                End If
            End If
            Dim ext As String = System.IO.Path.GetExtension(FileUpload.PostedFile.FileName)
            If Not (ext.ToUpper() = ".JPG" _
                OrElse ext.ToUpper() = ".JPEG" _
                OrElse ext.ToUpper() = ".PDF") Then
                MessageBox.Show("Hanya menerima file format (PDF/JPG/JPEG)")
                Exit Sub
            End If
            If Not IsNothing(FileUpload) AndAlso FileUpload.Value <> String.Empty Then
                objPostedData = FileUpload.PostedFile
            Else
                objPostedData = Nothing
            End If

            If Not (IsNothing(objPostedData)) Then
                sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)

                If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                    MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                    Exit Sub
                End If
            Else
                MessageBox.Show(SR.DataNotFound("File Bukti Potong"))
                Exit Sub
            End If
        End If

        If IsExistDebitMemoNumber(txtNoBuktiPotong.Text) Then
            MessageBox.Show(SR.DataIsExist("File Bukti Potong"))
            Exit Sub
        End If

        Dim noReg As String = GetRegNumber()
        Dim SrcExtFile As String = String.Empty
        Dim SrcFile As String = String.Empty
        If Not IsNothing(objPostedData) Then
            SrcExtFile = Path.GetExtension(objPostedData.FileName) '-- Source file name
            SrcFile = Path.GetFileName(objPostedData.FileName) '-- Source file name
        End If
        Dim strTOPSPPenaltyPathConfig As String = KTB.DNet.Lib.WebConfig.GetValue("TOPSPPenaltyFileDirectory")
        Dim strTOPSPPenaltyPathFile As String = String.Empty
        Dim strDestFile As String = String.Empty

        Try
            Dim strDealerCode As String = String.Empty
            objTOPSPPenalty = New TOPSPPenaltyFacade(User).Retrieve(CInt(hdnTOPSPPenaltyID.Value))
            If Not IsNothing(objTOPSPPenalty) AndAlso objTOPSPPenalty.ID > 0 Then
                strDealerCode = objTOPSPPenalty.Dealer.DealerCode

                If objTOPSPPenalty.NoRegPengembalian <> "" Then
                    noReg = objTOPSPPenalty.NoRegPengembalian
                End If
                If SrcExtFile <> "" Then
                    strTOPSPPenaltyPathFile = "\BPPenalty_" & strDealerCode & "_" & noReg & "_" & TimeStamp() & SrcExtFile
                    strDestFile = strTOPSPPenaltyPathConfig & strTOPSPPenaltyPathFile '--Destination File                       
                End If

                Dim dblAmount As Double = objTOPSPPenalty.Amount
                If Not IsNothing(objPostedData) Then
                    objTOPSPPenalty.AttachmentData = objPostedData
                Else
                    objTOPSPPenalty.AttachmentData = objTOPSPPenalty.AttachmentData
                End If
                objTOPSPPenalty.NoRegPengembalian = noReg

                Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objTOPSPPenalty.PaymentDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_1").ValueId)

                'objTOPSPPenalty.AmountPPh = 0.15 * dblAmount
                objTOPSPPenalty.AmountPPh = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=dblAmount)
                If objTOPSPPenalty.NoBuktiPotong = "" Then
                    objTOPSPPenalty.StatusPengembalian = 0
                End If
                objTOPSPPenalty.NoBuktiPotong = txtNoBuktiPotong.Text
                objTOPSPPenalty.BuktiPotongDate = Date.Now
                If strDestFile <> "" Then
                    objTOPSPPenalty.UploadFilePath = strDestFile
                End If
                Dim _result As Integer = New TOPSPPenaltyFacade(User).Update(objTOPSPPenalty)
                If _result > 0 Then
                    For Each objTopDtl As TOPSPPenaltyDetail In objTOPSPPenalty.TOPSPPenaltyDetails
                        'objTopDtl.PPh = objTopDtl.AmountPenalty * 0.15
                        objTopDtl.PPh = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=objTopDtl.AmountPenalty)
                        _result = New TOPSPPenaltyDetailFacade(User).Update(objTopDtl)
                    Next
                    If Not IsNothing(objPostedData) Then
                        UploadAttachment(objTOPSPPenalty, TargetDirectory)
                    End If
                End If

                lblNoRegPengembalianUpload.Text = objTOPSPPenalty.NoRegPengembalian
                lblAmountPPh.Text = Format(objTOPSPPenalty.AmountPPh, "#,##0")
                If objTOPSPPenalty.UploadFilePath <> "" Then
                    If FileUpload.Visible = True Then
                        lblFileUpload.Text = "<br/>" & Path.GetFileName(TargetDirectory & objTOPSPPenalty.UploadFilePath)
                    Else
                        lblFileUpload.Text = Path.GetFileName(TargetDirectory & objTOPSPPenalty.UploadFilePath)
                    End If
                End If
            End If
            MessageBox.Show(SR.SaveSuccess())
            btnSearch_Click(Nothing, Nothing)
        Catch
            MessageBox.Show(SR.SaveFail())
        End Try
    End Sub

    Private Sub UploadAttachment(ByVal ObjAttachment As TOPSPPenalty, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(ObjAttachment.UploadFilePath) Then
                    finfo = New FileInfo(TargetPath + ObjAttachment.UploadFilePath)

                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    ObjAttachment.AttachmentData.SaveAs(TargetPath + ObjAttachment.UploadFilePath)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function IsExistDebitMemoNumber(ByVal strNoBuktiPotong As String) As Boolean
        Dim bResult As Boolean = False

        If strNoBuktiPotong.Trim <> "" Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(TOPSPPenalty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TOPSPPenalty), "NoBuktiPotong", MatchType.Exact, strNoBuktiPotong))
            criterias.opAnd(New Criteria(GetType(TOPSPPenalty), "ID", MatchType.No, If(hdnTOPSPPenaltyID.Value = "", 0, hdnTOPSPPenaltyID.Value)))

            Dim arlTOPSPPenalty As ArrayList = New TOPSPPenaltyFacade(User).Retrieve(criterias)
            If Not IsNothing(arlTOPSPPenalty) And arlTOPSPPenalty.Count > 0 Then
                bResult = True
            End If
        End If
        Return bResult
    End Function

    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        hdnTOPSPPenaltyID.Value = ""
        lblDebitMemoNumber.Text = ""
        lblBuktiPotongDate.Text = ""
        lblAmountPPh.Text = 0
        lblFileUpload.Text = ""
        pnlDaftar.Enabled = True
        pnlUpload.Visible = False
        BindGrid(dgTOPSPPenaltyDetailList.CurrentPageIndex)
    End Sub

    Private Sub dgRincianBilling_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgRincianBilling.PageIndexChanged
        dgRincianBilling.CurrentPageIndex = e.NewPageIndex
        BindGridBilling(e.NewPageIndex)
    End Sub

    Private Sub dgRincianBilling_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgRincianBilling.SortCommand
        '-- Sort datagrid rows based on a column header clicked
        If CType(ViewState("currSortColumnDetail"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirectionDetail"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirectionDetail") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirectionDetail") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumnDetail") = e.SortExpression
            ViewState("currSortDirectionDetail") = Sort.SortDirection.ASC
        End If

        '-- Bind page-1
        dgRincianBilling.CurrentPageIndex = 0
        BindGridBilling(dgRincianBilling.CurrentPageIndex)
    End Sub

    Protected Sub dgRincianBilling_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgRincianBilling.ItemDataBound
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblBillingNumber As Label = CType(e.Item.FindControl("lblBillingNumber"), Label)
        Dim lblAmountPenalty As Label = CType(e.Item.FindControl("lblAmountPenalty"), Label)
        Dim lblPPh As Label = CType(e.Item.FindControl("lblPPh"), Label)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As TOPSPPenaltyDetail = CType(e.Item.DataItem, TOPSPPenaltyDetail)
            lblNo.Text = (e.Item.ItemIndex + 1 + (dgRincianBilling.PageSize * dgRincianBilling.CurrentPageIndex)).ToString
            lblBillingNumber.Text = oData.SparePartBilling.BillingNumber
            lblAmountPenalty.Text = Format(oData.AmountPenalty, "#,##0")
            If oData.PPh = 0 Then
                Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(oData.TOPSPPenalty.PaymentDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_1").ValueId)
                'lblPPh.Text = Format(oData.AmountPenalty * 0.15, "#,##0")
                lblPPh.Text = Format(CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=oData.AmountPenalty), "#,##0")
            Else
                lblPPh.Text = Format(oData.PPh, "#,##0")
            End If
        End If
    End Sub

    Private Sub BindGridBilling(ByVal pageIndex As Integer)
        Dim arrRincianBilling As New ArrayList
        Dim objTOPSPPenalty As TOPSPPenalty = New TOPSPPenaltyFacade(User).Retrieve(CInt(hdnTOPSPPenaltyID.Value))
        If Not IsNothing(objTOPSPPenalty) AndAlso objTOPSPPenalty.ID > 0 Then
            arrRincianBilling = objTOPSPPenalty.TOPSPPenaltyDetails
        End If
        If arrRincianBilling.Count > 0 Then
            CommonFunction.SortListControl(arrRincianBilling, CType(ViewState("currSortColumnDetail"), String), CType(ViewState("currSortDirectionDetail"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrRincianBilling, pageIndex, dgTOPSPPenaltyDetailList.PageSize)
            dgRincianBilling.DataSource = PagedList
            dgRincianBilling.VirtualItemCount = arrRincianBilling.Count()
            dgRincianBilling.DataBind()
        Else
            dgRincianBilling.DataSource = New ArrayList
            dgRincianBilling.VirtualItemCount = 0
            dgRincianBilling.CurrentPageIndex = 0
            dgRincianBilling.DataBind()
        End If
    End Sub

End Class
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.General

Public Class FrmPengajuanPencairanDepositA
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents lblCode As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTipePengajuan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ltrDealerCode As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrDealerName As System.Web.UI.WebControls.Literal
    Protected WithEvents txtNomerSuratPengajuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNomorRekening As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents dgEntryPencairanDepositA As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lnkAccount As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblNomorRekening As System.Web.UI.WebControls.Label
    Protected WithEvents rbDN As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbSO As System.Web.UI.WebControls.RadioButton
    Protected WithEvents lblTipeDokumen As System.Web.UI.WebControls.Label
    Protected WithEvents ddlDN As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlSO As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblDNSONumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblPostingDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents rbPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents rbBulan As System.Web.UI.WebControls.Label
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPeriode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents lblBulan As System.Web.UI.WebControls.Label
    Protected WithEvents txtSONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents lblSONumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblNote1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPersetujuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblPersetujuan2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoReg As System.Web.UI.WebControls.Label
    Protected WithEvents btnNew As System.Web.UI.WebControls.Button
    Protected WithEvents btnValidasi As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerID As System.Web.UI.WebControls.TextBox
    Protected WithEvents imgDealer As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lblBankAccount As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"

    Dim sesHelper As SessionHelper = New SessionHelper
    Dim oDealer As Dealer
    Dim objPencairan As DataTable

    Private Mode As enumMode.Mode

    Private arlSO As ArrayList
    Private arlDN As ArrayList

    'Create DataTable
    Dim dtPengajuan As New DataTable("MyPencairanDepositA")
    Dim PengajuanView As DataView

    Dim objDealerBankAccount As DealerBankAccount = New DealerBankAccount
    Dim gridEdited As Boolean = False

    Const DocTypePengajuan = 1
    Const DocTypeKuitansi = 2
#End Region

#Region "Custom Method"

    Sub fillDataDealer(ByVal oD As Dealer)

        Dim Tgl As String

        ltrDealerCode.Text = String.Format("{0} / {1}", oD.DealerCode.ToString(), oD.SearchTerm2)
        ltrDealerCode.Visible = False
        ltrDealerName.Text = oD.DealerName
        ltrDealerName.Visible = False
        lblDealerName.Text = oD.DealerName
        txtDealerID.Text = oD.ID

        If oD.AgreementNo Is Nothing Then
            oD.AgreementNo = String.Empty
        End If

        If oD.AgreementDate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
            Tgl = String.Empty
        Else
            Tgl = Format(oD.AgreementDate, "dd/MM/yyyy").ToString()
        End If

        lblPersetujuan.Text = "No " & oD.AgreementNo & " Tanggal " & Tgl & " dan/atau Akta Perjanjian Jual Beli No. " & oD.SPANumber & " Tanggal " & Format(oD.SPADate, "dd/MM/yyyy")
        lblPersetujuan2.Text = "meskipun tidak ditandatangani oleh pihak " & oD.DealerName

    End Sub


    Sub Initialize()

        oDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        txtKodeDealer.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")

        imgDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblBankAccount.Attributes("OnClick") = "ShowPPDealerBankAccountSelection(" & txtKodeDealer.Text & ")"

        txtKodeDealer.ReadOnly = True

        'start : ori Initialize        
        txtNomorRekening.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")
        'txtQtyClaimEntry.Attributes.Add("onkeypress", "return numericOnlyUniv(event)")

        lblPostingDate.Text = Date.Today.ToShortDateString.ToString()

        BindTipePengajuan(ddlTipePengajuan)
        BindYear()
        BindPeriode()
        lblPeriode.Visible = False
        lblBulan.Visible = False
        rbPeriode.Visible = False
        rbBulan.Visible = False
        'End: ori Initialize
        'oDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        If txtKodeDealer.Text.Trim <> String.Empty Then
            oDealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)
            If oDealer.ID > 0 Then
                
                fillDataDealer(oDealer)
            End If

        End If

        'If CekBtnPriv() Then
        '    btnSave.Enabled = True
        'Else
        '    btnSave.Enabled = False
        'End If
        btnNew.Enabled = False
    End Sub

    Sub BindTipePengajuan(ByVal ddl As DropDownList)
        'ddl.DataSource = [Enum].GetNames(GetType(TipePengajuan))
        'ddl.DataBind()
        'ddl.Items.Insert(0, New ListItem("Silakan Pilih", "0"))
        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("Silahkan Pilih", 0))
        ddl.Items.Add(New ListItem("Offset", TipePengajuan.Offset))
        ddl.Items.Add(New ListItem("Cash Tahunan", TipePengajuan.CashAnnual))
        ddl.Items.Add(New ListItem("Cash Incidental", TipePengajuan.CashIncidental))
        ddl.Items.Add(New ListItem("Cash Interest", TipePengajuan.CashInterest))

    End Sub

    Private Sub ClearAllFields()
        txtNomerSuratPengajuan.Text = String.Empty
        txtNomorRekening.Text = String.Empty
    End Sub

    Private Sub CreateColumn()
        ' Define the columns of the table.
        dtPengajuan.Columns.Add(New DataColumn("DNNumber", GetType(String)))
        dtPengajuan.Columns.Add(New DataColumn("AssignmentNumber", GetType(String)))
        dtPengajuan.Columns.Add(New DataColumn("HeaderAmount", GetType(Double)))
        dtPengajuan.Columns.Add(New DataColumn("DealerAmount", GetType(Double)))
        dtPengajuan.Columns.Add(New DataColumn("PPn", GetType(Double)))
        dtPengajuan.Columns.Add(New DataColumn("Penjelasan", GetType(String)))
    End Sub

    Private Sub BindDetailToGrid(ByVal pTipePengajuan As Integer, ByVal pTipeDokumen As String)
        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)
        lblDealerName.Text = objDealer.DealerName
        If pTipePengajuan = TipePengajuan.Offset Then
            If dtPengajuan.Columns.Count = 0 Then
                CreateColumn()
            End If
        End If
        If pTipePengajuan = TipePengajuan.Offset And pTipeDokumen = "SO" Then
            If txtSONumber.Text <> String.Empty Then
                Dim RowStatus As Integer = -1
                objPencairan = sesHelper.GetSession("SalesEntryPencairan")
                If Not (objPencairan Is Nothing) Then
                    If objPencairan.Rows.Count > 0 Then
                        If objPencairan.Rows(0).Item("AssignmentNumber") <> txtSONumber.Text Then
                            RowStatus = 0
                            objPencairan.Rows.Clear()
                        Else
                            'dtPengajuan.Rows.Clear()
                            dtPengajuan = objPencairan
                        End If
                    Else
                        RowStatus = 0
                        objPencairan.Rows.Clear()
                    End If
                Else
                    RowStatus = 0
                End If

                If RowStatus = 0 Then
                    Dim objSalesOrder As New SalesOrder
                    objSalesOrder = New SalesOrderFacade(User).Retrieve(txtSONumber.Text)
                    If objSalesOrder.ID > 0 Then
                        Dim dr As DataRow
                        dr = dtPengajuan.NewRow
                        dr("AssignmentNumber") = objSalesOrder.SONumber
                        dr("DealerAmount") = 0
                        dr("HeaderAmount") = objSalesOrder.Amount
                        dr("PPn") = 0
                        dr("Penjelasan") = String.Empty
                        dtPengajuan.Rows.Add(dr)
                    End If
                End If
                dgEntryPencairanDepositA.Columns(1).Visible = True
                dgEntryPencairanDepositA.Columns(3).Visible = False
                dgEntryPencairanDepositA.Columns(5).Visible = True
                dgEntryPencairanDepositA.Columns(6).Visible = False
                dgEntryPencairanDepositA.Columns(7).Visible = False
            Else
                dtPengajuan = Nothing
            End If
            dgEntryPencairanDepositA.ShowFooter = False
            dgEntryPencairanDepositA.Columns(2).HeaderText = "Jumlah Pengajuan"
            dgEntryPencairanDepositA.Columns(3).HeaderText = "PPn (10%)"
            objPencairan = dtPengajuan
            sesHelper.SetSession("SalesEntryPencairan", objPencairan)
        ElseIf pTipePengajuan = TipePengajuan.Offset And pTipeDokumen = "DN" Then
            dtPengajuan.Rows.Clear()
            If ddlDN.SelectedIndex > 0 Then
                Dim objDebitNote As DebitNote = New FinishUnit.DebitNoteFacade(User).Retrieve(CInt(ddlDN.SelectedValue))
                Dim dr As DataRow
                dr = dtPengajuan.NewRow
                dr("DNNumber") = objDebitNote.DNNumber.ToString
                dr("DealerAmount") = objDebitNote.Amount
                dr("HeaderAmount") = objDebitNote.Amount
                dr("PPn") = 0.1 * objDebitNote.Amount
                dr("Penjelasan") = objDebitNote.Description
                dtPengajuan.Rows.Add(dr)
            Else
                dgEntryPencairanDepositA.Columns(1).Visible = False
                dtPengajuan = Nothing
            End If
            dgEntryPencairanDepositA.ShowFooter = False
            dgEntryPencairanDepositA.Columns(2).HeaderText = "Jumlah Pengajuan"
            dgEntryPencairanDepositA.Columns(3).HeaderText = "PPn (10%)"
            dgEntryPencairanDepositA.Columns(3).Visible = True
            dgEntryPencairanDepositA.Columns(5).Visible = False
            dgEntryPencairanDepositA.Columns(6).Visible = False
            dgEntryPencairanDepositA.Columns(7).Visible = False
            objPencairan = dtPengajuan
            sesHelper.SetSession("SalesEntryPencairan", objPencairan)
        ElseIf pTipePengajuan = TipePengajuan.CashAnnual Then
            Dim RowStatus As Integer = -1
            If dtPengajuan.Columns.Count = 0 Then
                CreateColumn()
            End If
            'Dim objUser As UserInfo = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
            'objPencairan = sesHelper.GetSession("SalesCashAnnual")
            'If Not (objPencairan Is Nothing) Then
            '    If objPencairan.Rows.Count > 0 Then
            '        dtPengajuan = objPencairan
            '    Else
            '        RowStatus = 0
            '        objPencairan.Rows.Clear()
            '    End If
            'Else
            '    RowStatus = 0
            'End If

            'If RowStatus = 0 Then
            Dim arrDeposit As New ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "ID", MatchType.InSet, "(select id from AnnualDepositAHeader where Year(Todate) = " & DateTime.Now.Year - 1 & ")"))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "Status", MatchType.Exact, CType(StatusPencairanTahunan.DiAjukan, Short)))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "FromDate", Sort.SortDirection.ASC))
            arrDeposit = New AnnualDepositAHeaderFacade(User).Retrieve(criterias, sortColl)

            If Not arrDeposit Is Nothing Then
                If arrDeposit.Count > 0 Then
                    Dim objAnnual As AnnualDepositAHeader
                    objAnnual = arrDeposit(0)
                    sesHelper.SetSession("Annual", objAnnual)
                    If objAnnual.ID > 0 Then
                        Dim dr As DataRow
                        dr = dtPengajuan.NewRow
                        dr("AssignmentNumber") = String.Empty
                        dr("DealerAmount") = objAnnual.NettoAmount
                        dr("HeaderAmount") = 0
                        dr("PPn") = 0
                        dr("Penjelasan") = "Pencairan Saldo Deposit A Tahunan " & objAnnual.FromDate.ToString("MMM yyyy") & " - " & objAnnual.ToDate.ToString("MMM yyyy")
                        dtPengajuan.Rows.Add(dr)
                    End If
                End If
            End If
            'End If
            dgEntryPencairanDepositA.Columns(1).Visible = False
            dgEntryPencairanDepositA.ShowFooter = False
            dgEntryPencairanDepositA.Columns(2).HeaderText = "Jumlah Pengajuan"
            dgEntryPencairanDepositA.Columns(3).HeaderText = "PPn (10%)"
            dgEntryPencairanDepositA.Columns(3).Visible = True
            dgEntryPencairanDepositA.Columns(5).Visible = False
            dgEntryPencairanDepositA.Columns(6).Visible = False
            dgEntryPencairanDepositA.Columns(7).Visible = False
            objPencairan = dtPengajuan
            sesHelper.SetSession("SalesCashAnnual", objPencairan)
        ElseIf pTipePengajuan = TipePengajuan.CashInterest Then

            Dim RowStatus As Integer = -1
            If dtPengajuan.Columns.Count = 0 Then
                CreateColumn()
            End If

            RowStatus = 0
            'Dim objUser As UserInfo = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)

            If RowStatus = 0 Then
                Dim arrDeposit As New ArrayList
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestH), "Year", MatchType.Exact, ddlYear.SelectedItem.Text))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestH), "Periode", MatchType.Exact, ddlPeriode.SelectedValue))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestH), "Dealer.ID", MatchType.Exact, objDealer.ID))
                arrDeposit = New DepositAInterestHFacade(User).Retrieve(criterias)
                If Not arrDeposit Is Nothing Then
                    If arrDeposit.Count > 0 Then
                        Dim objDepositA As New DepositAInterestH
                        objDepositA = arrDeposit(0)

                        criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "DepositAInterestH.ID", MatchType.Exact, objDepositA.ID))
                        'added by anh 2011-12-08 kalo dah dipake gak muncul lagi
                        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "Status", MatchType.Exact, 0))
                        arrDeposit = New DepositAPencairanHFacade(User).Retrieve(criterias)
                        If arrDeposit.Count = 0 Then
                            Dim dr As DataRow
                            dr = dtPengajuan.NewRow
                            dr("AssignmentNumber") = String.Empty
                            dr("DealerAmount") = objDepositA.TaxAmount
                            dr("HeaderAmount") = objDepositA.InterestAmount
                            dr("PPn") = objDepositA.NettoAmount
                            dr("Penjelasan") = "Pencairan bunga saldo Deposit A Periode " & ddlPeriode.SelectedItem.Text & " Tahun " & ddlYear.SelectedItem.Text
                            dtPengajuan.Rows.Add(dr)
                            sesHelper.SetSession("DepositInterest", objDepositA)
                        Else
                            Dim objDepositPencairan As DepositAPencairanH = CType(arrDeposit(0), DepositAPencairanH)
                            If objDepositPencairan.Status = 14 Or objDepositPencairan.Status = 12 Then  'Blok
                                Dim dr As DataRow
                                dr = dtPengajuan.NewRow
                                dr("AssignmentNumber") = String.Empty
                                dr("DealerAmount") = objDepositA.TaxAmount
                                dr("HeaderAmount") = objDepositA.InterestAmount
                                dr("PPn") = objDepositA.NettoAmount
                                dr("Penjelasan") = "Pencairan bunga saldo Deposit A Periode " & ddlPeriode.SelectedItem.Text & " Tahun " & ddlYear.SelectedItem.Text
                                dtPengajuan.Rows.Add(dr)
                                sesHelper.SetSession("DepositInterest", objDepositA)
                            Else
                                'start  :dna, remove logic temp
                                'MessageBox.Show("Deposit Interest periode ini sudah dicairkan")
                                'dgEntryPencairanDepositA.Visible = False
                                'Exit Sub
                                'end    :dna, remove logic temp
                            End If
                        End If
                    End If
                End If
            End If
            dgEntryPencairanDepositA.Columns(1).Visible = True
            dgEntryPencairanDepositA.Columns(1).HeaderText = "Interest"
            dgEntryPencairanDepositA.Columns(2).HeaderText = "Tax 15%"
            dgEntryPencairanDepositA.Columns(3).HeaderText = "Netto Amount"
            dgEntryPencairanDepositA.Columns(3).Visible = True
            dgEntryPencairanDepositA.Columns(5).Visible = False
            dgEntryPencairanDepositA.Columns(6).Visible = False
            dgEntryPencairanDepositA.Columns(7).Visible = False
            dgEntryPencairanDepositA.ShowFooter = False
            objPencairan = dtPengajuan
            sesHelper.SetSession("SalesEntryPencairan", objPencairan)

            'dgEntryPencairanDepositA.Columns(1).Visible = False
            'dgEntryPencairanDepositA.ShowFooter = True

            'objPencairan = sesHelper.GetSession("SalesEntryPencairan")
            'If dtPengajuan.Columns.Count = 0 Then
            '    CreateColumn()
            'End If
            'If (objPencairan Is Nothing) Then
            '    objPencairan = dtPengajuan
            '    sesHelper.SetSession("SalesEntryPencairan", objPencairan)
            'Else
            '    If Not gridEdited Then
            '        dtPengajuan.Rows.Clear()
            '        objPencairan = dtPengajuan
            '    End If
            '    sesHelper.SetSession("SalesEntryPencairan", objPencairan)
            'End If

            'Dim arrDeposit As New ArrayList
            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestH), "Year", MatchType.Exact, ddlYear.SelectedItem.Text))
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestH), "Periode", MatchType.Exact, ddlPeriode.SelectedValue))

            'arrDeposit = New DepositAInterestHFacade(User).Retrieve(criterias)
            'If Not arrDeposit Is Nothing Then
            '    If arrDeposit.Count > 0 Then
            '        sesHelper.SetSession("DepositAPencarian", arrDeposit)
            '    End If
            'End If
        Else
            dgEntryPencairanDepositA.Columns(1).Visible = False
            dgEntryPencairanDepositA.ShowFooter = True
            dgEntryPencairanDepositA.Columns(2).HeaderText = "Jumlah Pengajuan"
            dgEntryPencairanDepositA.Columns(3).HeaderText = "PPn (10%)"
            ' dgEntryPencairanDepositA.Columns(3).Visible = True
            dgEntryPencairanDepositA.Columns(5).Visible = True
            dgEntryPencairanDepositA.Columns(6).Visible = True
            dgEntryPencairanDepositA.Columns(7).Visible = True

            objPencairan = sesHelper.GetSession("SalesEntryPencairan")
            'If dtPengajuan Is Nothing Or dtPengajuan.Columns.Count = 0 Then
            If dtPengajuan.Columns.Count = 0 Then
                CreateColumn()
            End If
            'If objPencairan.Rows(0).Item("AssignmentNumber") <> ddlSO.SelectedItem.Text Then
            '    'dtPengajuan.Rows.Clear()
            'End If
            If (objPencairan Is Nothing) Then
                objPencairan = dtPengajuan
                sesHelper.SetSession("SalesEntryPencairan", objPencairan)
            Else
                If Not gridEdited Then
                    dtPengajuan.Rows.Clear()
                    objPencairan = dtPengajuan
                End If
                sesHelper.SetSession("SalesEntryPencairan", objPencairan)

            End If
        End If

        dgEntryPencairanDepositA.DataSource = objPencairan
        dgEntryPencairanDepositA.DataBind()
    End Sub

    Private Sub BindPeriode()
        ddlPeriode.Items.Clear()
        ddlPeriode.Items.Insert(0, New ListItem("Please Select", "0"))
        ddlPeriode.Items.Insert(1, New ListItem("Jan - Mar", "1"))
        ddlPeriode.Items.Insert(2, New ListItem("Apr - Jun", "2"))
        ddlPeriode.Items.Insert(3, New ListItem("Jul - Sep", "3"))
        ddlPeriode.Items.Insert(4, New ListItem("Okt - Des", "4"))
    End Sub

    Private Sub BindYear()
        Dim curYear As Integer = Date.Now.Year
        Dim startYear As Integer = curYear - 5
        Dim EndYear As Integer = curYear + 5
        Dim intYear As Integer = 0
        'Dim yearMax As Integer = Year(Date.Now) + 5
        ddlYear.Items.Add(New ListItem("Silahkan Pilih", "-1"))

        ddlYear.Items.Clear()
        For intYear = startYear To EndYear
            ddlYear.Items.Add(intYear.ToString)
        Next
        'If ddlYear.Items.Contains(New ListItem(Date.Now.Year.ToString)) Then
        'ddldemotime.Items.FindByText("texthere").Selected = true;
        'ddldemotime.Items.FindByValue("Valuehere").Selected = true;

        ddlYear.Items.FindByValue(Date.Now.Year.ToString).Selected = True
        'Else
        'ddlYear.SelectedIndex = -1
        'End If
    End Sub

    Private Function ValidateItem(ByVal pTipePengajuan As Integer, ByVal txtNomerSuratPengajuan As String, ByVal rbDNChecked As Boolean, ByVal ddlDNSelectedIndex As Integer, ByVal rbSOChecked As Boolean, ByVal ddlSOSelectedIndex As Integer, ByVal txtNomorRekening As String) As Boolean

        If pTipePengajuan = TipePengajuan.Offset Then
            'ElseIf rbDN.Checked And ddlDN.SelectedIndex = 0 Then
            '    MessageBox.Show("Error : Silahkan pilih Rekening Debit Note terlebih dahulu!")
            '    Return False
            'ElseIf rbSO.Checked And ddlSO.SelectedIndex = 0 Then
            '    MessageBox.Show("Error: Silahkan pilih Rekening Sales Order terlebih dahulu!")
            ''    Return False

        End If

        If txtNomerSuratPengajuan = String.Empty Then
            MessageBox.Show("Error : Silahkan isi nomor surat pengajuan terlebih dahulu!")
            Return False
        ElseIf txtNomorRekening = String.Empty And pTipePengajuan <> TipePengajuan.Offset Then
            MessageBox.Show("Error : Silahkan pilih nomer rekening terlebih dahulu!")
            Return False
        End If

        Return True
    End Function

    Private Function ValidateDuplicatePengajuanNo(ByVal NoSuratPengajuan As String) As Boolean
        Try
            'Dim objUser As UserInfo = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "NoSurat", MatchType.Exact, NoSuratPengajuan))
            Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.DepositAPencairanH), "ID", AggregateType.Count)
            Dim objDepositAPencairanH As New FinishUnit.DepositAPencairanHFacade(User)
            Dim Result As Integer = objDepositAPencairanH.RetrieveScalar(criterias, agg)
            If Result Then
                MessageBox.Show("Error : (" & NoSuratPengajuan & ") sudah ada!")
                Return False
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("Error : Data tidak ditemukan.")
            Return False
        End Try
    End Function

    Private Function ValidateDuplication(ByVal AccountofType As String, ByVal Type As String) As Boolean
        Try
            'cari di database
            'Dim objUser As UserInfo = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "Dealer.ID", MatchType.Exact, objDealer.ID.ToString()))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), Type, MatchType.Exact, AccountofType))
            Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.DepositAPencairanH), "ID", AggregateType.Count)
            Dim objDepositAPencairanH As New FinishUnit.DepositAPencairanHFacade(User)
            Dim Result As Integer = objDepositAPencairanH.RetrieveScalar(criterias, agg)
            If Result Then
                MessageBox.Show("Error : (" & AccountofType & ") sudah mengajukan pencairan!")
                Return False
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("Error : Data tidak ditemukan.")
            Return False
        End Try
    End Function

    Private Function CekAnnual() As Boolean
        'Dim objUser As UserInfo = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)
        Dim arrCashAnnual As New ArrayList
        Dim criteria As CriteriaComposite = New CriteriaComposite(New criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "Dealer.ID", MatchType.Exact, objDealer.ID))
        criteria.opAnd(New criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "ID", MatchType.InSet, "(select id from depositapencairanh where year(createdtime) = year(getdate())-1)"))
        criteria.opAnd(New criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "Type", MatchType.Exact, 2))
        arrCashAnnual = New FinishUnit.DepositAPencairanHFacade(User).Retrieve(criteria)
        If Not arrCashAnnual Is Nothing Then
            If arrCashAnnual.Count > 0 Then
                MessageBox.Show("Deposit A Tahun ini sudah dicairkan")
                ddlTipePengajuan.SelectedIndex = 0
                dgEntryPencairanDepositA.Visible = False
                Return False
            End If
        End If
        Return True
    End Function

    Private Sub ClearData()
        rbDN.Checked = True
        ddlDN.Items.Clear()
        ddlDN.Items.Add(New ListItem("Silahkan Pilih", 0))
        ddlSO.Items.Clear()
        ddlSO.Items.Add(New ListItem("Silahkan Pilih", 0))
        txtSONumber.Text = ""
        txtNomerSuratPengajuan.Text = ""
        'txtNomorRekening.Text = ""
        dgEntryPencairanDepositA.DataSource = New ArrayList
        dgEntryPencairanDepositA.DataBind()
    End Sub

    Private Function GetDepositA() As DepositA
        Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        Dim objDepositA As DepositA
        Dim sql As String = "(select ID from DepositA as dep where dep.ID = (select MAX(ID) from DepositA where DealerID= " & objDealer.ID & " and RowStatus=0))"
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositA), "ID", MatchType.Exact, sql))

        objDepositA = New DepositAFacade(User).Retrieve(criterias)(0)
        If Not IsNothing(objDepositA) Then
            Return objDepositA
        Else
            Return New DepositA
        End If
    End Function

#End Region

#Region "Cek Privilege"


    Private Function CekBtnPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanClaimCreateData_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Event Handlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'InitiateAuthorization()
        If Not IsPostBack Then
            Initialize()
            btnSave.Attributes.Add("onclick", "return confirm('Anda yakin menyimpan pencairan ini?');")
            btnValidasi.Enabled = False
        End If
    End Sub

    Private Sub InitiateAuthorization()

        'If Not SecurityProvider.Authorize(context.User, SR.DepositAView_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=FINISH UNIT - Pengajuan Pencairan Deposit A")
        'End If

        If Not SecurityProvider.Authorize(context.User, SR.DepositA_pengajuan_pencairan_depoA_buat_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Deposit A - Buat Pengajuan Pencairan")
            Me.btnSave.Visible = False
        End If
    End Sub

    Private Sub dgEntryPencairanDepositA_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgEntryPencairanDepositA.SortCommand
        'If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
        '    Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
        '        Case Sort.SortDirection.ASC
        '            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

        '        Case Sort.SortDirection.DESC
        '            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        '    End Select
        'Else
        '    ViewState("CurrentSortColumn") = e.SortExpression
        '    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        'End If
        'BindToGrid()

    End Sub

    Private TotalAmount As Double = 0
    Private Sub dgEntryPencairanDepositA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgEntryPencairanDepositA.ItemDataBound
        gridEdited = False
        'objPencairan = sesHelper.GetSession("SalesEntryPencairan")

        Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgEntryPencairanDepositA.CurrentPageIndex * dgEntryPencairanDepositA.PageSize)

            Dim dblDealerAmount As Double = 0
            Dim dblPPHAmount As Double = 0

            If IsDBNull(DataBinder.Eval(e.Item.DataItem, "DealerAmount")) = False Then
                'dblDealerAmount = Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "DealerAmount"))
                dblDealerAmount = DataBinder.Eval(e.Item.DataItem, "DealerAmount")
            End If


            If IsDBNull(DataBinder.Eval(e.Item.DataItem, "PPn")) = False Then
                dblPPHAmount = DataBinder.Eval(e.Item.DataItem, "PPn")
            End If

            'TotalAmount = TotalAmount + Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "DealerAmount"))
            'added by anh 20110915
            Select Case selectedTipe
                Case TipePengajuan.Offset
                    TotalAmount = TotalAmount + dblDealerAmount
                Case TipePengajuan.CashInterest
                    TotalAmount = dblPPHAmount
                Case TipePengajuan.CashAnnual
                    TotalAmount = TotalAmount + (dblDealerAmount + dblPPHAmount)
                Case TipePengajuan.CashIncidental
                    TotalAmount = TotalAmount + (dblDealerAmount + dblPPHAmount)
            End Select
            'end added by anh 20110915

            'remarked by anh 20110915
            'If selectedTipe <> TipePengajuan.CashInterest Then
            '    TotalAmount = TotalAmount + (dblDealerAmount + dblPPHAmount)
            'Else
            '    TotalAmount = dblPPHAmount
            'End If
            'end remarked by anh 20110915


            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            If selectedTipe = TipePengajuan.Offset Then
                lbtnDelete.Visible = False
                If rbDN.Checked = True Then
                    e.Item.Cells(5).Visible = False
                    'dgEntryPencairanDepositA.Columns(5).Visible = false
                Else
                    Dim lblHeaderAmount As Label = CType(e.Item.FindControl("lblHeaderAmount"), Label)
                    lblHeaderAmount.Visible = True
                End If
                dgEntryPencairanDepositA.Columns(3).Visible = False
            Else
                lbtnDelete.Visible = True
            End If

        ElseIf (e.Item.ItemType = ListItemType.Footer) Then
            'e.Item.Cells(1).HorizontalAlign = HorizontalAlign.Right
            'e.Item.Cells(1).Text = "Total:"
            'e.Item.Cells(2).Text = IIf(TotalAmount = 0, 0, TotalAmount.ToString("#,###"))
            ' lblTotal.Text = IIf(TotalAmount = 0, 0, TotalAmount.ToString("#,###"))

            Dim lbtnAdd As LinkButton = CType(e.Item.FindControl("lbtnAdd"), LinkButton)
            Dim txtJumlahPencairan As TextBox = CType(e.Item.FindControl("txtJumlahPencairan"), TextBox)
            Dim txtPPn As TextBox = CType(e.Item.FindControl("txtPPn"), TextBox)
            txtPPn.ReadOnly = False
            txtJumlahPencairan.ReadOnly = False
            txtJumlahPencairan.Text = String.Empty
            txtPPn.Text = String.Empty

            Dim arrDeposit As New ArrayList
            Dim lblTotalAmount As Label = CType(e.Item.FindControl("lblTotalAmount"), Label)
            If Not sesHelper.GetSession("TotalSalesOrder") Is Nothing Then
                Dim decTotalAmount As Decimal
                decTotalAmount = CType(sesHelper.GetSession("TotalSalesOrder"), Decimal)
                lblTotalAmount.Text = FormatNumber(decTotalAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                sesHelper.SetSession("TotalSalesOrder", Nothing)
                lbtnAdd.Visible = False
            End If

        ElseIf (e.Item.ItemType = ListItemType.EditItem) Then

        End If

        lblTotal.Text = IIf(TotalAmount = 0, 0, TotalAmount.ToString("#,###"))

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'start  :dna, remove logic temp
        'If dgEntryPencairanDepositA.Visible = False Then
        '    MessageBox.Show("Data Masih Kosong")
        '    Exit Sub
        'End If
        'end    :dna, remove logic temp
        Dim nResult As Integer = 0
        'Dim nResults As String = String.Empty
        Dim DNNumber As String = String.Empty
        Dim AssignmentNumber As String = String.Empty

        'looping grid untuk ambil pencairan
        Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
        Dim NomerSuratPengajuan As String = txtNomerSuratPengajuan.Text.Trim

        If NomerSuratPengajuan = String.Empty Then
            MessageBox.Show("No.Ref Surat Pengajuan harus diisi")
            Exit Sub
        End If

        'If selectedTipe <> TipePengajuan.Offset Then
        '    If txtNomorRekening.Text = String.Empty Then
        '        MessageBox.Show("Nomor Rekening harus diisi")
        '        Exit Sub
        '    End If
        'End If

        'start  :dna, remove logic temp
        'If lblTotal.Text = "0" Then
        '    MessageBox.Show("Simpan data gagal, data masih kosong")
        '    Exit Sub
        'End If
        'end    :dna, remove logic temp
        Try
            If selectedTipe = TipePengajuan.CashInterest Then
                Dim objHistoryDepositAPencairan As DepositAStatusHistory = New DepositAStatusHistory
                objHistoryDepositAPencairan.DocNumber = NomerSuratPengajuan
                objHistoryDepositAPencairan.NewStatus = StatusPencairanDealer.Setuju
                objHistoryDepositAPencairan.DocType = DocTypePengajuan 'HistoryType.Pencairan
                nResult = New FinishUnit.DepositAStatusHistoryFacade(User).Insert(objHistoryDepositAPencairan)
                If nResult > -1 Then
                    Dim objDepositAPencairanH As DepositAPencairanH = New DepositAPencairanH
                    'Dim objDealer As Dealer = New General.DealerFacade(User).GetDealer(oDealer.ID)
                    Dim objDealer As Dealer = New General.DealerFacade(User).GetDealer(txtKodeDealer.Text.Trim)
                    Dim TipePengajuan As Integer = ddlTipePengajuan.SelectedIndex
                    objDepositAPencairanH.Dealer = objDealer
                    objDepositAPencairanH.NoSurat = NomerSuratPengajuan
                    'objDepositAPencairanH.NoReg = lblNoReg.Text
                    If txtNomorRekening.Text <> String.Empty Then
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "BankAccount", MatchType.Exact, txtNomorRekening.Text))
                        Dim arlAccount As ArrayList = New FinishUnit.DealerBankAccountFacade(User).Retrieve(criterias)
                        If arlAccount.Count > 0 Then
                            objDepositAPencairanH.DealerBankAccount = arlAccount(0)
                        Else
                            Dim objDealerBankAccount As DealerBankAccount
                            objDealerBankAccount = New DealerBankAccountFacade(User).Retrieve(0)
                            objDepositAPencairanH.DealerBankAccount = objDealerBankAccount
                        End If
                    Else
                        Dim objDealerBankAccount As DealerBankAccount
                        objDealerBankAccount = New DealerBankAccountFacade(User).Retrieve(0)
                        objDepositAPencairanH.DealerBankAccount = objDealerBankAccount
                    End If

                    objDepositAPencairanH.Type = TipePengajuan
                    objDepositAPencairanH.Status = StatusPencairanDealer.Baru
                    objDepositAPencairanH.DealerAmount = CDbl(lblTotal.Text)
                    objDepositAPencairanH.ApprovalAmount = CDbl(lblTotal.Text)

                    Dim objDepositInterest As New DepositAInterestH
                    If Not sesHelper.GetSession("DepositInterest") Is Nothing Then
                        objDepositInterest = sesHelper.GetSession("DepositInterest")
                    End If

                    If objDepositInterest.ID > 0 Then
                        objDepositAPencairanH.DepositAInterestH = objDepositInterest
                    End If

                    'Save Detail
                    Dim intRow As Integer = 0
                    Dim intRows As Integer = dgEntryPencairanDepositA.Items.Count - 1
                    Dim GridItem As DataGridItem
                    Dim objDepositAPencairanDs As ArrayList = objDepositAPencairanH.DepositAPencairanDs
                    '  For intRow = 0 To intRows
                    ' Dim objDepositAPencairanD As DepositAPencairanD = New DepositAPencairanD

                    'Dim txtJumlahPencairan As TextBox = CType(dgEntryPencairanDepositA.Controls(0).Controls(dgEntryPencairanDepositA.Controls(0).Controls.Count - 1).FindControl("txtJumlahPencairan"), TextBox)
                    ' Dim txtPenjelasanEntry As TextBox = CType(dgEntryPencairanDepositA.Controls(0).Controls(dgEntryPencairanDepositA.Controls(0).Controls.Count - 1).FindControl("txtPenjelasanEntry"), TextBox
                    'objDepositAPencairanD.DealerAmount = CDec(txtJumlahPencairan.Text)
                    'objDepositAPencairanD.Description = txtPenjelasanEntry.Text

                    For intRow = 0 To intRows
                        Dim objDepositAPencairanD As DepositAPencairanD = New DepositAPencairanD
                        'objDepositAPencairan.DealerAmount = dgEntryPencairanDepositA.Items(intC).FindControl("")
                        GridItem = dgEntryPencairanDepositA.Items(intRow)
                        'objDepositAPencairan.DealerAmount = GridItem.Cells(2).Text.Trim
                        objDepositAPencairanD.DealerAmount = CDbl(lblTotal.Text)
                        objDepositAPencairanD.Description = DirectCast(GridItem.FindControl("lblPenjelasan"), Label).Text.Trim
                        objDepositAPencairanDs.Add(objDepositAPencairanD)
                    Next


                    ' Next


                    nResult = New FinishUnit.DepositAPencairanHFacade(User).Insert(objDepositAPencairanH)
                    If nResult <> -1 Then
                        MessageBox.Show("Pencairan Nomor " & NomerSuratPengajuan & " berhasil disimpan. ")

                        Dim objPencairanH As DepositAPencairanH
                        objPencairanH = New DepositAPencairanHFacade(User).Retrieve(nResult)
                        If objPencairanH.ID > 0 Then
                            lblNoReg.Text = objPencairanH.NoReg
                            sesHelper.SetSession("sessDepositeAPencairanH", objPencairanH)
                        End If

                        'ddlTipePengajuan.SelectedIndex = 0
                        'btnSave.Enabled = False
                        dtPengajuan.Rows.Clear()
                        objPencairan = Nothing
                        sesHelper.SetSession("SalesEntryPencairan", objPencairan)
                        'BindDetailToGrid(1, "")
                        'lblTotal.Text = 0
                        'txtNomerSuratPengajuan.Text = ""
                        'txtNomorRekening.Text = ""
                        'dgEntryPencairanDepositA.Visible = False
                        btnValidasi.Visible = True
                    Else
                        MessageBox.Show(SR.SaveFail)
                        btnValidasi.Visible = False
                    End If
                End If
            ElseIf selectedTipe = Me.TipePengajuan.Offset Then

                If Not ValidateItem(selectedTipe, txtNomerSuratPengajuan.Text.ToString, rbDN.Checked, ddlDN.SelectedIndex, rbSO.Checked, ddlSO.SelectedIndex, txtNomorRekening.Text.Trim) Then
                    Return
                End If

                'If Not ValidateDuplicatePengajuanNo(NomerSuratPengajuan) Then
                '    Return
                'End If

                Dim AccountOfType As String
                Dim Type As String

                If rbDN.Checked Then
                    If ddlDN.SelectedIndex > 0 Then
                        Dim objDN As New DebitNote
                        objDN = New DebitNoteFacade(User).Retrieve(CInt(ddlDN.SelectedValue))
                        If objDN.ID > 0 Then
                            DNNumber = objDN.DNNumber
                            AccountOfType = DNNumber
                            Type = "DNNumber"
                        Else
                            MessageBox.Show("Simpan data gagal")
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show("Gagal simpan pencairan, Dokumen Account tidak di pilih!")
                    End If

                Else
                    If txtSONumber.Text <> String.Empty Then
                        AssignmentNumber = txtSONumber.Text
                        AccountOfType = AssignmentNumber
                        Type = "AssignmentNumber"
                    Else
                        MessageBox.Show("Gagal simpan pencairan, SO Number tidak diisi!")
                    End If

                End If

                If Not ValidateDuplication(AccountOfType, Type) Then
                    Exit Sub
                End If

                Dim objHistoryDepositAPencairan As DepositAStatusHistory = New DepositAStatusHistory
                objHistoryDepositAPencairan.DocNumber = NomerSuratPengajuan
                objHistoryDepositAPencairan.NewStatus = StatusPencairanDealer.Baru
                objHistoryDepositAPencairan.DocType = DocTypePengajuan 'HistoryType.Pencairan
                nResult = New FinishUnit.DepositAStatusHistoryFacade(User).Insert(objHistoryDepositAPencairan)
                If nResult > -1 Then
                    'Save Header
                    Dim objDepositAPencairanH As DepositAPencairanH = New DepositAPencairanH
                    'Dim objDealer As Dealer = New General.DealerFacade(User).GetDealer(oDealer.ID)
                    Dim objDealer As Dealer = New General.DealerFacade(User).GetDealer(txtKodeDealer.Text.Trim)
                    Dim TipePengajuan As Integer = ddlTipePengajuan.SelectedIndex
                    objDepositAPencairanH.Dealer = objDealer
                    objDepositAPencairanH.NoSurat = NomerSuratPengajuan
                    objDepositAPencairanH.AssignmentNumber = AssignmentNumber
                    If txtNomorRekening.Text <> String.Empty Then
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "BankAccount", MatchType.Exact, txtNomorRekening.Text))
                        Dim arlAccount As ArrayList = New FinishUnit.DealerBankAccountFacade(User).Retrieve(criterias)
                        If arlAccount.Count > 0 Then
                            objDepositAPencairanH.DealerBankAccount = arlAccount(0)
                        Else
                            Dim objDealerBankAccount As DealerBankAccount
                            objDealerBankAccount = New DealerBankAccountFacade(User).Retrieve(0)
                            objDepositAPencairanH.DealerBankAccount = objDealerBankAccount
                        End If
                    Else
                        Dim objDealerBankAccount As DealerBankAccount
                        objDealerBankAccount = New DealerBankAccountFacade(User).Retrieve(0)
                        objDepositAPencairanH.DealerBankAccount = objDealerBankAccount
                    End If

                    If rbDN.Checked Then
                        objDepositAPencairanH.DNNumber = AccountOfType
                    End If

                    objDepositAPencairanH.Type = TipePengajuan
                    objDepositAPencairanH.Status = StatusPencairanDealer.Baru
                    objDepositAPencairanH.DealerAmount = CDbl(lblTotal.Text)

                    'Save Detail
                    Dim intRow As Integer = 0
                    Dim intRows As Integer = dgEntryPencairanDepositA.Items.Count - 1
                    Dim GridItem As DataGridItem
                    Dim objDepositAPencairanDs As ArrayList = objDepositAPencairanH.DepositAPencairanDs
                    For intRow = 0 To intRows
                        Dim objDepositAPencairanD As DepositAPencairanD = New DepositAPencairanD
                        'objDepositAPencairan.DealerAmount = dgEntryPencairanDepositA.Items(intC).FindControl("")
                        GridItem = dgEntryPencairanDepositA.Items(intRow)
                        'objDepositAPencairan.DealerAmount = GridItem.Cells(2).Text.Trim
                        objDepositAPencairanD.DealerAmount = CDbl(DirectCast(GridItem.FindControl("lblJumlahPencairan"), Label).Text())
                        objDepositAPencairanD.Description = DirectCast(GridItem.FindControl("lblPenjelasan"), Label).Text.Trim
                        objDepositAPencairanDs.Add(objDepositAPencairanD)
                    Next


                    nResult = New FinishUnit.DepositAPencairanHFacade(User).Insert(objDepositAPencairanH)
                    If nResult <> -1 Then
                        MessageBox.Show("Pencairan Nomor " & NomerSuratPengajuan & " berhasil disimpan. ")
                        'ddlTipePengajuan.SelectedIndex = 0

                        Dim objPencairanH As DepositAPencairanH
                        objPencairanH = New DepositAPencairanHFacade(User).Retrieve(nResult)
                        If objPencairanH.ID > 0 Then
                            lblNoReg.Text = objPencairanH.NoReg
                            sesHelper.SetSession("sessDepositeAPencairanH", objPencairanH)
                        End If

                        'btnSave.Enabled = False
                        dtPengajuan.Rows.Clear()
                        objPencairan = Nothing
                        sesHelper.SetSession("SalesEntryPencairan", objPencairan)
                        'BindDetailToGrid(1, "")
                        'lblTotal.Text = 0
                        'txtNomerSuratPengajuan.Text = ""
                        'txtNomorRekening.Text = ""
                        'dgEntryPencairanDepositA.Visible = False
                    Else
                        MessageBox.Show(SR.SaveFail)
                    End If
                End If
            ElseIf selectedTipe = Me.TipePengajuan.CashAnnual Then

                If CekAnnual() = False Then
                    Exit Sub
                End If

                'If Not ValidateDuplicatePengajuanNo(NomerSuratPengajuan) Then
                '    Return
                'End If

                Dim AccountOfType As String
                Dim Type As String

                Dim objHistoryDepositAPencairan As DepositAStatusHistory = New DepositAStatusHistory
                objHistoryDepositAPencairan.DocNumber = NomerSuratPengajuan
                objHistoryDepositAPencairan.NewStatus = StatusPencairanDealer.Setuju
                objHistoryDepositAPencairan.DocType = DocTypePengajuan 'HistoryType.Pencairan
                nResult = New FinishUnit.DepositAStatusHistoryFacade(User).Insert(objHistoryDepositAPencairan)
                If nResult > -1 Then
                    'Save Header
                    Dim objDepositAPencairanH As DepositAPencairanH = New DepositAPencairanH
                    'Dim objDealer As Dealer = New General.DealerFacade(User).GetDealer(oDealer.ID)
                    Dim objDealer As Dealer = New General.DealerFacade(User).GetDealer(txtKodeDealer.Text.Trim)
                    Dim TipePengajuan As Integer = ddlTipePengajuan.SelectedIndex
                    objDepositAPencairanH.Dealer = objDealer
                    objDepositAPencairanH.NoSurat = NomerSuratPengajuan
                    objDepositAPencairanH.AssignmentNumber = String.Empty
                    If txtNomorRekening.Text <> String.Empty Then
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "BankAccount", MatchType.Exact, txtNomorRekening.Text))
                        Dim arlAccount As ArrayList = New FinishUnit.DealerBankAccountFacade(User).Retrieve(criterias)
                        If arlAccount.Count > 0 Then
                            objDepositAPencairanH.DealerBankAccount = arlAccount(0)
                        Else
                            Dim objDealerBankAccount As DealerBankAccount
                            objDealerBankAccount = New DealerBankAccountFacade(User).Retrieve(0)
                            objDepositAPencairanH.DealerBankAccount = objDealerBankAccount
                        End If
                    Else
                        Dim objDealerBankAccount As DealerBankAccount
                        objDealerBankAccount = New DealerBankAccountFacade(User).Retrieve(0)
                        objDepositAPencairanH.DealerBankAccount = objDealerBankAccount
                    End If
                    objDepositAPencairanH.Type = TipePengajuan
                    objDepositAPencairanH.Status = StatusPencairanDealer.Baru
                    objDepositAPencairanH.DealerAmount = CDbl(lblTotal.Text)
                    objDepositAPencairanH.ApprovalAmount = CDbl(lblTotal.Text)

                    'Save Detail
                    Dim intRow As Integer = 0
                    Dim intRows As Integer = dgEntryPencairanDepositA.Items.Count - 1
                    Dim GridItem As DataGridItem
                    Dim objDepositAPencairanDs As ArrayList = objDepositAPencairanH.DepositAPencairanDs
                    For intRow = 0 To intRows
                        Dim objDepositAPencairanD As DepositAPencairanD = New DepositAPencairanD
                        'objDepositAPencairan.DealerAmount = dgEntryPencairanDepositA.Items(intC).FindControl("")
                        GridItem = dgEntryPencairanDepositA.Items(intRow)
                        'objDepositAPencairan.DealerAmount = GridItem.Cells(2).Text.Trim
                        objDepositAPencairanD.DealerAmount = CDbl(DirectCast(GridItem.FindControl("lblJumlahPencairan"), Label).Text())
                        objDepositAPencairanD.Description = DirectCast(GridItem.FindControl("lblPenjelasan"), Label).Text.Trim
                        objDepositAPencairanDs.Add(objDepositAPencairanD)
                    Next

                    nResult = New FinishUnit.DepositAPencairanHFacade(User).Insert(objDepositAPencairanH)
                    If nResult <> -1 Then

                        Dim oAnnualDepositAHeaderFacade As AnnualDepositAHeaderFacade = New AnnualDepositAHeaderFacade(User)

                        Dim objAnnualDepositAHeader As AnnualDepositAHeader
                        objAnnualDepositAHeader = CType(sesHelper.GetSession("Annual"), AnnualDepositAHeader)
                        If Not IsNothing(objAnnualDepositAHeader.ID) Then
                            objAnnualDepositAHeader.Status = StatusPencairanTahunan.DiCairkan
                            Dim intResult As Integer = 0
                            intResult = oAnnualDepositAHeaderFacade.Update(objAnnualDepositAHeader)
                            If intResult <> -1 Then
                                sesHelper.RemoveSession("Annual")
                            End If
                        End If

                        MessageBox.Show("Pencairan Nomor " & NomerSuratPengajuan & " berhasil disimpan. ")
                        'ddlTipePengajuan.SelectedIndex = 0

                        Dim objPencairanH As DepositAPencairanH
                        objPencairanH = New DepositAPencairanHFacade(User).Retrieve(nResult)
                        If objPencairanH.ID > 0 Then
                            lblNoReg.Text = objPencairanH.NoReg
                            sesHelper.SetSession("sessDepositeAPencairanH", objPencairanH)
                        End If

                        'btnSave.Enabled = False
                        dtPengajuan.Rows.Clear()
                        objPencairan = Nothing
                        sesHelper.SetSession("SalesEntryPencairan", objPencairan)
                        'BindDetailToGrid(1, "")
                        'lblTotal.Text = 0
                        'txtNomerSuratPengajuan.Text = ""
                        'txtNomorRekening.Text = ""
                        'dgEntryPencairanDepositA.Visible = False
                    Else
                        MessageBox.Show(SR.SaveFail)
                    End If
                End If
            ElseIf selectedTipe = Me.TipePengajuan.CashIncidental Then
                If dgEntryPencairanDepositA.Items.Count = 0 Then
                    MessageBox.Show("Belum tambah data (icon plus hijau belum dipilih)")
                    Exit Sub
                End If
                'If Not ValidateDuplicatePengajuanNo(NomerSuratPengajuan) Then
                '    Return
                'End If

                Dim AccountOfType As String
                Dim Type As String

                Dim objHistoryDepositAPencairan As DepositAStatusHistory = New DepositAStatusHistory
                objHistoryDepositAPencairan.DocNumber = NomerSuratPengajuan
                objHistoryDepositAPencairan.NewStatus = StatusPencairanDealer.Baru
                objHistoryDepositAPencairan.DocType = DocTypePengajuan 'HistoryType.Pencairan
                nResult = New FinishUnit.DepositAStatusHistoryFacade(User).Insert(objHistoryDepositAPencairan)
                If nResult > -1 Then
                    'Save Header
                    Dim objDepositAPencairanH As DepositAPencairanH = New DepositAPencairanH
                    'Dim objDealer As Dealer = New General.DealerFacade(User).GetDealer(oDealer.ID)
                    Dim objDealer As Dealer = New General.DealerFacade(User).GetDealer(txtKodeDealer.Text.Trim)
                    Dim TipePengajuan As Integer = ddlTipePengajuan.SelectedIndex
                    objDepositAPencairanH.Dealer = objDealer
                    objDepositAPencairanH.NoSurat = NomerSuratPengajuan
                    objDepositAPencairanH.AssignmentNumber = String.Empty
                    If txtNomorRekening.Text <> String.Empty Then
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "BankAccount", MatchType.Exact, txtNomorRekening.Text))
                        Dim arlAccount As ArrayList = New FinishUnit.DealerBankAccountFacade(User).Retrieve(criterias)
                        If arlAccount.Count > 0 Then
                            objDepositAPencairanH.DealerBankAccount = arlAccount(0)
                        Else
                            Dim objDealerBankAccount As DealerBankAccount
                            objDealerBankAccount = New DealerBankAccountFacade(User).Retrieve(0)
                            objDepositAPencairanH.DealerBankAccount = objDealerBankAccount
                        End If
                    Else
                        Dim objDealerBankAccount As DealerBankAccount
                        objDealerBankAccount = New DealerBankAccountFacade(User).Retrieve(0)
                        objDepositAPencairanH.DealerBankAccount = objDealerBankAccount
                    End If
                    objDepositAPencairanH.Type = TipePengajuan
                    objDepositAPencairanH.Status = StatusPencairanDealer.Baru
                    objDepositAPencairanH.DealerAmount = CDbl(lblTotal.Text)

                    'Save Detail
                    Dim intRow As Integer = 0
                    Dim intRows As Integer = dgEntryPencairanDepositA.Items.Count - 1
                    Dim GridItem As DataGridItem
                    Dim objDepositAPencairanDs As ArrayList = objDepositAPencairanH.DepositAPencairanDs
                    For intRow = 0 To intRows
                        Dim objDepositAPencairanD As DepositAPencairanD = New DepositAPencairanD
                        'objDepositAPencairan.DealerAmount = dgEntryPencairanDepositA.Items(intC).FindControl("")
                        GridItem = dgEntryPencairanDepositA.Items(intRow)
                        'objDepositAPencairan.DealerAmount = GridItem.Cells(2).Text.Trim
                        objDepositAPencairanD.DealerAmount = CDbl(DirectCast(GridItem.FindControl("lblJumlahPencairan"), Label).Text())
                        objDepositAPencairanD.Description = DirectCast(GridItem.FindControl("lblPenjelasan"), Label).Text.Trim
                        objDepositAPencairanDs.Add(objDepositAPencairanD)
                    Next

                    nResult = New FinishUnit.DepositAPencairanHFacade(User).Insert(objDepositAPencairanH)
                    If nResult <> -1 Then

                        MessageBox.Show("Pencairan Nomor " & NomerSuratPengajuan & " berhasil disimpan. ")
                        'ddlTipePengajuan.SelectedIndex = 0

                        Dim objPencairanH As DepositAPencairanH
                        objPencairanH = New DepositAPencairanHFacade(User).Retrieve(nResult)
                        If objPencairanH.ID > 0 Then
                            lblNoReg.Text = objPencairanH.NoReg
                            sesHelper.SetSession("sessDepositeAPencairanH", objPencairanH)
                        End If

                        'btnSave.Enabled = False
                        dtPengajuan.Rows.Clear()
                        objPencairan = Nothing
                        sesHelper.SetSession("SalesEntryPencairan", objPencairan)
                        'BindDetailToGrid(1, "")
                        'lblTotal.Text = 0
                        'txtNomerSuratPengajuan.Text = ""
                        'txtNomorRekening.Text = ""
                        'dgEntryPencairanDepositA.Visible = False
                    Else
                        MessageBox.Show(SR.SaveFail)
                    End If
                End If
            End If
            If nResult <> -1 Then
                btnNew.Enabled = False
                btnSave.Enabled = False
                btnCancel.Enabled = False
                btnValidasi.Enabled = True
            Else
                btnNew.Enabled = False
                btnSave.Enabled = True
                btnCancel.Enabled = True
                btnValidasi.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show("Gagal simpan pencairan")
            Return
        End Try

    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("../FinishUnit/FrmPengajuanPencairanDepositA.aspx", True)
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

        dtPengajuan.Rows.Clear()
        objPencairan = Nothing
        'sesHelper.SetSession("SalesEntryPencairan", objPencairan)
        Initialize()
        ClearData()

    End Sub

    Private Sub btnValidasi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidasi.Click
        Dim objDepositAPencairanH As DepositAPencairanH = CType(sesHelper.GetSession("sessDepositeAPencairanH"), DepositAPencairanH)
        If Not IsNothing(objDepositAPencairanH) Then
            Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
            Select Case selectedTipe
                Case TipePengajuan.CashIncidental
                    objDepositAPencairanH.Status = StatusPencairanDealer.Validasi
                Case TipePengajuan.Offset
                    objDepositAPencairanH.DealerAmount = CDbl(lblTotal.Text)
                    objDepositAPencairanH.ApprovalAmount = CDbl(lblTotal.Text)
                    objDepositAPencairanH.Status = StatusPencairanDealer.Setuju
                Case TipePengajuan.CashAnnual
                    objDepositAPencairanH.DealerAmount = CDbl(lblTotal.Text)
                    objDepositAPencairanH.ApprovalAmount = CDbl(lblTotal.Text)
                    objDepositAPencairanH.Status = StatusPencairanDealer.Setuju
                Case TipePengajuan.CashInterest
                    objDepositAPencairanH.DealerAmount = CDbl(lblTotal.Text)
                    objDepositAPencairanH.ApprovalAmount = CDbl(lblTotal.Text)
                    objDepositAPencairanH.Status = StatusPencairanDealer.Setuju
            End Select

            Dim oDepPencairanHFacade As DepositAPencairanHFacade = New DepositAPencairanHFacade(User)
            Dim intResult As Integer = oDepPencairanHFacade.Update(objDepositAPencairanH)
            If intResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                btnValidasi.Enabled = False
                btnNew.Enabled = True
                sesHelper.RemoveSession("sessDepositeAPencairanH")
            End If

        End If
    End Sub

    Private Sub ddlTipePengajuan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipePengajuan.SelectedIndexChanged
        Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
        'Dim objUser As UserInfo = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)
        lblDealerName.Text = objDealer.DealerName
        lblTotal.Text = 0
        lblNoReg.Text = String.Empty
        txtNomerSuratPengajuan.Text = String.Empty
        If selectedTipe = TipePengajuan.Offset Then
            'Find Debit note
            ddlDN.Enabled = True
            ddlSO.Enabled = True
            rbDN.Enabled = True
            rbSO.Enabled = True
            txtSONumber.Visible = True
            lblDNSONumber.Visible = True
            lblSONumber.Visible = True
            txtNomorRekening.Text = String.Empty
            txtNomorRekening.Enabled = False
            lnkAccount.Visible = False

            Dim criteriaDN As CriteriaComposite = New CriteriaComposite(New criteria(GetType(KTB.DNet.Domain.DebitNote), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriaDN.opAnd(New criteria(GetType(KTB.DNet.Domain.DebitNote), "Dealer.ID", MatchType.Exact, objDealer.ID.ToString()))
            criteriaDN.opAnd(New criteria(GetType(KTB.DNet.Domain.DebitNote), "DNNumber", MatchType.IsNotNull, Nothing))
            arlDN = New FinishUnit.DebitNoteFacade(User).Retrieve(criteriaDN)

            Dim arrCRDN As New ArrayList
            Dim arrDebitNote As New ArrayList

            ddlDN.Items.Clear()
            ddlDN.Items.Add(New ListItem("Silahkan Pilih", 0))
            For Each item As DebitNote In arlDN
                Dim criteria As CriteriaComposite = New CriteriaComposite(New criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteria.opAnd(New criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "Dealer.ID", MatchType.Exact, objDealer.ID.ToString()))
                criteria.opAnd(New criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "DNNumber", MatchType.Exact, item.DNNumber))
                arrCRDN = New FinishUnit.DepositAPencairanHFacade(User).Retrieve(criteria)
                If Not arrCRDN Is Nothing Then
                    If arrCRDN.Count = 0 Then
                        ddlDN.Items.Add(New ListItem(item.DNNumber & " - " & item.Description, item.ID))
                    End If
                End If
            Next
            'ddlDN.DataTextField = "DNNumber"
            'ddlDN.DataValueField = "ID"       
            'ddlDN.DataSource = arrDebitNote
            'ddlDN.DataBind()
            'ddlDN.Items.Insert(0, New ListItem("Silakan Pilih", "0"))
            ddlDN.SelectedIndex = 0

            'Dim criteriaSO As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DebitNote), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criteriaSO.opAnd(New Criteria(GetType(KTB.DNet.Domain.DebitNote), "Dealer.ID", MatchType.Exact, objUser.Dealer.ID.ToString()))
            'criteriaSO.opAnd(New Criteria(GetType(KTB.DNet.Domain.DebitNote), "Assignment", MatchType.IsNotNull, Nothing))
            'arlSO = New FinishUnit.DebitNoteFacade(User).Retrieve(criteriaSO)

            'ddlSO.DataTextField = "Assignment"
            'ddlSO.DataValueField = "ID"
            ''ddlSO.DataValueField = "Assignment"
            'ddlSO.DataSource = arlSO
            'ddlSO.DataBind()
            'ddlSO.Items.Insert(0, New ListItem("Silakan Pilih", "0"))

            If rbSO.Checked Then
                dgEntryPencairanDepositA.Columns(1).Visible = True
                dgEntryPencairanDepositA.Columns(3).Visible = True
                ddlDN.Visible = False
            Else
                dgEntryPencairanDepositA.Columns(1).Visible = False
                dgEntryPencairanDepositA.Columns(3).Visible = True
                ddlDN.Visible = True
                If ddlDN.SelectedIndex = 0 Then
                    dgEntryPencairanDepositA.Visible = False
                End If
            End If

            ddlYear.Visible = False
            ddlPeriode.Visible = False
            rbPeriode.Visible = False
            rbBulan.Visible = False
            lblPeriode.Visible = False
            lblBulan.Visible = False
            If rbDN.Checked = True Then
                txtSONumber.Visible = False
            End If

        ElseIf selectedTipe = TipePengajuan.CashInterest Then
            ddlYear.Visible = True
            ddlPeriode.Visible = True
            rbPeriode.Visible = True
            rbBulan.Visible = True
            lblPeriode.Visible = True
            lblBulan.Visible = True
            rbPeriode.Visible = True
            rbBulan.Visible = True
            rbDN.Enabled = False
            rbSO.Enabled = False
            ddlDN.Enabled = False
            ddlSO.Enabled = False
            txtSONumber.Visible = False
            lblDNSONumber.Visible = False
            lblSONumber.Visible = False
            dgEntryPencairanDepositA.Columns(1).Visible = False
            dgEntryPencairanDepositA.Columns(3).Visible = True
            ddlDN.Visible = False
            ddlPeriode.SelectedIndex = 0
            dgEntryPencairanDepositA.Visible = False
            'txtNomorRekening.Enabled = False
            lnkAccount.Visible = True
        ElseIf selectedTipe = TipePengajuan.CashAnnual Then
            'Disabled DNSO selection

            If CekAnnual() Then
                ddlDN.Visible = False
                ddlDN.Enabled = False
                ddlSO.Enabled = False
                rbDN.Enabled = False
                rbSO.Enabled = False
                txtSONumber.Visible = False
                lblDNSONumber.Visible = False
                lblSONumber.Visible = False
                'txtNomorRekening.Text = String.Empty
                'txtNomorRekening.Enabled = False
                lnkAccount.Visible = True

                ddlYear.Visible = False
                ddlPeriode.Visible = False
                rbPeriode.Visible = False
                rbBulan.Visible = False
                lblPeriode.Visible = False
                lblBulan.Visible = False
                btnCari.Visible = True
                dgEntryPencairanDepositA.Columns(1).Visible = False
                dgEntryPencairanDepositA.Visible = True
                BindDetailToGrid(selectedTipe, "")
                dgEntryPencairanDepositA.Columns(3).Visible = False
            End If

        ElseIf selectedTipe = TipePengajuan.CashIncidental Then
            ddlDN.Visible = False
            ddlDN.Enabled = False
            ddlSO.Enabled = False
            rbDN.Enabled = False
            rbSO.Enabled = False
            txtSONumber.Visible = False
            lblDNSONumber.Visible = False
            lblSONumber.Visible = False
            'txtNomorRekening.Enabled = False
            'txtNomorRekening.Text = String.Empty
            lnkAccount.Visible = True

            ddlYear.Visible = False
            ddlPeriode.Visible = False
            rbPeriode.Visible = False
            rbBulan.Visible = False
            lblPeriode.Visible = False
            lblBulan.Visible = False
            dgEntryPencairanDepositA.Columns(1).Visible = False
            dgEntryPencairanDepositA.Columns(3).Visible = False
            dgEntryPencairanDepositA.Visible = True
            BindDetailToGrid(selectedTipe, "")

        Else
            dgEntryPencairanDepositA.Visible = False

            objPencairan = Nothing
            dgEntryPencairanDepositA.DataSource = objPencairan
            dgEntryPencairanDepositA.DataBind()

        End If
    End Sub

    Private Sub dgEntryPencairanDepositA_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEntryPencairanDepositA.ItemCommand
        objPencairan = sesHelper.GetSession("SalesEntryPencairan")
        Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
        Select Case (e.CommandName)
            Case "Add"
                If Not Page.IsValid Then
                    Return
                End If
                Dim myRow As DataRow
                myRow = objPencairan.NewRow

                Dim txtJumlahPencairan As TextBox = e.Item.FindControl("txtJumlahPencairan")
                Dim dblJumlahPencairan As Double = 0
                If txtJumlahPencairan.Text.Length > 0 Then
                    Try
                        dblJumlahPencairan = CDbl(txtJumlahPencairan.Text.Trim)
                    Catch ex As Exception
                        MessageBox.Show("Jumlah Pengajuan bukan format number")
                        Exit Sub
                    End Try

                    If dblJumlahPencairan > 0 Then
                        Try
                            Dim objDepositA As DepositA = GetDepositA()
                            If objDepositA.ID > 0 Then
                                If dblJumlahPencairan > objDepositA.EndBalance Then
                                    MessageBox.Show("Saldo tidak mencukupi")
                                    Exit Select
                                End If
                            End If
                        Catch ex As Exception
                            Exit Sub
                        End Try
                        myRow("DealerAmount") = dblJumlahPencairan
                    Else
                        MessageBox.Show("Input Amount must valid number and above 0.")
                        Exit Select
                    End If
                Else
                    MessageBox.Show("Please input jumlah pencairan!")
                    Exit Select
                End If

                If selectedTipe = TipePengajuan.CashIncidental Then
                    myRow("PPn") = 0
                Else
                    myRow("PPn") = 0.1 * dblJumlahPencairan
                End If


                'MessageBox.Show("masuk ke item command ADD")

                Dim txtPenjelasanEntry As TextBox = e.Item.FindControl("txtPenjelasanEntry")
                myRow("Penjelasan") = txtPenjelasanEntry.Text

                objPencairan.Rows.Add(myRow)

                objPencairan.AcceptChanges()
                sesHelper.SetSession("SalesEntryPencairan", objPencairan)
            Case "Update"

                If Not Page.IsValid Then
                    Return
                End If

                Dim txtJumlahPencairanEdit As TextBox = CType(e.Item.FindControl("txtJumlahPencairanEdit"), TextBox)
                Dim txtPenjelasanEntryEdit As TextBox = CType(e.Item.FindControl("txtPenjelasanEntryEdit"), TextBox)

                Dim dblJumlahPencairan As Double = 0
                If txtJumlahPencairanEdit.Text.Length > 0 Then
                    dblJumlahPencairan = CDbl(txtJumlahPencairanEdit.Text.Trim)
                    If dblJumlahPencairan > 0 Then
                        objPencairan.Rows(0)("DealerAmount") = dblJumlahPencairan
                    Else
                        MessageBox.Show("Input Amount must valid number and above 0.")
                        Exit Select
                    End If
                Else
                    MessageBox.Show("Please input jumlah pencairan!")
                    Exit Select
                End If

                If rbSO.Enabled = False Or rbSO.Checked = False Then
                    objPencairan.Rows(0)("PPn") = 0.1 * dblJumlahPencairan
                End If

                'MessageBox.Show("masuk ke item command ADD")

                objPencairan.Rows(0)("Penjelasan") = txtPenjelasanEntryEdit.Text
                objPencairan.AcceptChanges()
                sesHelper.SetSession("SalesEntryPencairan", objPencairan)
        End Select

        gridEdited = True
        dgEntryPencairanDepositA.EditItemIndex = -1

        Dim pTipeDokumen As String
        If rbDN.Checked Then
            pTipeDokumen = "DN"
        Else
            pTipeDokumen = "SO"
        End If
        BindDetailToGrid(selectedTipe, pTipeDokumen)
    End Sub

    Private Sub dgEntryPencairanDepositA_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEntryPencairanDepositA.EditCommand
        dgEntryPencairanDepositA.EditItemIndex = CInt(e.Item.ItemIndex)
        dgEntryPencairanDepositA.ShowFooter = False


        Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
        lbtnDelete.Visible = False

        gridEdited = True
        objPencairan = sesHelper.GetSession("SalesEntryPencairan")
        sesHelper.SetSession("SalesEntryPencairan", objPencairan)

        Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
        Dim pTipeDokumen As String
        If rbDN.Checked Then
            pTipeDokumen = "DN"
        Else
            pTipeDokumen = "SO"
        End If
        BindDetailToGrid(selectedTipe, pTipeDokumen)
    End Sub

    Private Sub dgEntryPencairanDepositA_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEntryPencairanDepositA.CancelCommand
        gridEdited = True

        'MessageBox.Show("Masuk Cancel Command Grid")
        dgEntryPencairanDepositA.EditItemIndex = -1
        Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
        Dim pTipeDokumen As String
        If rbDN.Checked Then
            pTipeDokumen = "DN"
        Else
            pTipeDokumen = "SO"
        End If
        BindDetailToGrid(selectedTipe, pTipeDokumen)
        'dgEntryPencairanDepositA.ShowFooter = True
    End Sub

    Private Sub dgEntryPencairanDepositA_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEntryPencairanDepositA.UpdateCommand
        TotalAmount = 0
        objPencairan = sesHelper.GetSession("SalesEntryPencairan")

        Dim myRow As DataRow = objPencairan.Rows(e.Item.ItemIndex)
        Dim txtJumlahPencairanEdit As TextBox = e.Item.FindControl("txtJumlahPencairanEdit")
        Dim dblJumlahPencairan As Double = 0
        dblJumlahPencairan = CDbl(txtJumlahPencairanEdit.Text.Trim)
        If rbSO.Checked = True Then
            If txtJumlahPencairanEdit.Text < myRow("HeaderAmount") Then
                myRow("DealerAmount") = txtJumlahPencairanEdit.Text
            Else
                'myRow("DealerAmount") = DealerAmount
                Try
                    Dim objDepositA As DepositA = GetDepositA()
                    If objDepositA.ID > 0 Then
                        If dblJumlahPencairan > objDepositA.EndBalance Then
                            MessageBox.Show("Saldo tidak mencukupi")
                            Exit Sub
                        End If
                    End If
                Catch ex As Exception
                    Exit Sub
                End Try
            End If
        Else
            myRow("DealerAmount") = txtJumlahPencairanEdit.Text
        End If
        Dim txtPenjelasanEntryEdit As TextBox = e.Item.FindControl("txtPenjelasanEntryEdit")
        myRow("Penjelasan") = txtPenjelasanEntryEdit.Text
        If rbSO.Enabled = False Or rbSO.Checked = False Then
            myRow("PPn") = 0.1 * CDbl(txtJumlahPencairanEdit.Text)
        End If

        gridEdited = True
        objPencairan.AcceptChanges()
        sesHelper.SetSession("SalesEntryPencairan", objPencairan)

        dgEntryPencairanDepositA.EditItemIndex = -1
        Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
        Dim pTipeDokumen As String
        If rbDN.Checked Then
            pTipeDokumen = "DN"
        Else
            pTipeDokumen = "SO"
        End If
        BindDetailToGrid(selectedTipe, pTipeDokumen)
    End Sub

    Private Sub rbDN_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbDN.CheckedChanged
        Dim pTipeDokumen As String = ""
        If rbDN.Checked = True Then
            lblDNSONumber.Text = "DN Number"
            ddlDN.Visible = True
            ddlDN.SelectedIndex = 0
            ddlSO.Visible = False
            txtSONumber.Visible = False
            pTipeDokumen = "DN"

            'dgEntryPencairanDepositA.Columns(1).Visible = False
        Else
            lblDNSONumber.Text = "SO Number"
            ddlDN.Visible = False
            ddlSO.Visible = False
            txtSONumber.Visible = True
            ddlSO.SelectedIndex = 0
            pTipeDokumen = "SO"
        End If
        Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
        BindDetailToGrid(selectedTipe, pTipeDokumen)
    End Sub

    Private Sub rbSO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbSO.CheckedChanged
        Dim pTipeDokumen As String = ""
        If rbSO.Checked = True Then
            lblDNSONumber.Text = "SO Number"
            ddlDN.Visible = False
            ddlSO.Visible = False
            txtSONumber.Visible = True
            pTipeDokumen = "SO"
            ddlSO.SelectedIndex = 0
            btnCari.Visible = True
        Else
            lblDNSONumber.Text = "DN Number"
            ddlDN.SelectedIndex = 0
            ddlDN.Visible = True
            ddlSO.Visible = False
            txtSONumber.Visible = False
            pTipeDokumen = "DN"
            btnCari.Visible = False
        End If
        Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
        BindDetailToGrid(selectedTipe, pTipeDokumen)
    End Sub

    Private Sub ddlDN_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDN.SelectedIndexChanged
        'LoadGrid
        Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
        If ddlDN.SelectedIndex = 0 Then
            dgEntryPencairanDepositA.Visible = False
        End If
        dgEntryPencairanDepositA.Visible = True
        BindDetailToGrid(selectedTipe, "DN")
    End Sub

    Private Sub ddlSO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSO.SelectedIndexChanged
        'LoadGrid
        Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
        BindDetailToGrid(selectedTipe, "SO")

    End Sub

    Private Sub dgEntryPencairanDepositA_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEntryPencairanDepositA.DeleteCommand
        objPencairan = sesHelper.GetSession("SalesEntryPencairan")

        gridEdited = True
        objPencairan.Rows(e.Item.ItemIndex).Delete()
        sesHelper.SetSession("SalesEntryPencairan", objPencairan)

        Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
        BindDetailToGrid(selectedTipe, "SO")

        'MessageBox.Show("Masuk Delete Command grid")
    End Sub

    Private Sub ddlPeriode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPeriode.SelectedIndexChanged
        Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
        If selectedTipe = TipePengajuan.CashInterest Then
            dgEntryPencairanDepositA.Columns(1).Visible = False
            dgEntryPencairanDepositA.Visible = True
            BindDetailToGrid(selectedTipe, "")
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)
        If selectedTipe = TipePengajuan.Offset And rbSO.Checked = True Then
            Dim objSalesOrder As New SalesOrder
            If txtSONumber.Text <> String.Empty Then
                objSalesOrder = New SalesOrderFacade(User).Retrieve(txtSONumber.Text)
                If objSalesOrder.ID > 0 Then
                    'Dim objUser As UserInfo = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
                    If objSalesOrder.POHeader.ContractHeader.Dealer.ID = objDealer.ID Then
                        Dim arrDeposit As New ArrayList
                        Dim criteria As CriteriaComposite = New CriteriaComposite(New criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteria.opAnd(New criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "AssignmentNumber", MatchType.Exact, txtSONumber.Text))
                        arrDeposit = New DepositAPencairanHFacade(User).Retrieve(criteria)
                        If Not arrDeposit Is Nothing Then
                            If arrDeposit.Count > 0 Then
                                MessageBox.Show("SO Number sudah ada")
                                Exit Sub
                            End If
                        End If
                        dgEntryPencairanDepositA.Visible = True
                        BindDetailToGrid(selectedTipe, "SO")
                    Else
                        MessageBox.Show("SO Number harus sesuai dealer")
                    End If
                Else
                    MessageBox.Show("SO Number tidak di temukan")
                    Exit Sub
                End If
            Else
                MessageBox.Show("Isi dulu So Numbernya")
                Exit Sub
            End If
        ElseIf selectedTipe = TipePengajuan.CashAnnual Then
            BindDetailToGrid(selectedTipe, "")
            dgEntryPencairanDepositA.Columns(3).Visible = False

        End If
    End Sub

    Private Sub imgDealer_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDealer.Click
        Initialize()
    End Sub

#End Region

#Region "Internal Enum"
    Private Enum TipePengajuan
        Offset = 1
        CashAnnual = 2
        CashIncidental = 3
        CashInterest = 4
    End Enum

    Private Enum StatusPencairanDealer
        Baru = 0
        Validasi = 1
        Setuju = 11
    End Enum
    Private Enum StatusPencairanTahunan
        DiAjukan = 0
        DiCairkan = 1
    End Enum
#End Region

End Class

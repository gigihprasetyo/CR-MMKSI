Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessValidation.Helpers

Public Class FrmDepB_PopupPencairan
    Inherits System.Web.UI.Page

#Region "Custom Variable Declaration"
    'Private objDepositBPencairanH As DepositBPencairanHeader
    'Private arlDepositBPencairanD As ArrayList = New ArrayList
    'Private arlDepositBPencairanDFilter As ArrayList = New ArrayList
    'Private TotalAmount As Double = 0
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)

    Dim sessHelper As New SessionHelper
    Dim objDealerLogin As Dealer

#End Region

#Region "Custom Method"

    Private Sub BindData(ByVal ID As Integer)
        Dim objDepositBPencairanH As DepositBPencairanHeader = New DepositBPencairanHeaderFacade(User).Retrieve(ID)
        If Not IsNothing(objDepositBPencairanH) Then
            sessHelper.SetSession("PopupDepositBPencairanHeader", objDepositBPencairanH)
            ltrDealerCode.Text = String.Format("{0} / {1}", objDepositBPencairanH.Dealer.DealerCode.ToString(), objDepositBPencairanH.Dealer.SearchTerm2)
            ltrDealerName.Text = objDepositBPencairanH.Dealer.DealerName

            txtNomerSuratPengajuan.Text = objDepositBPencairanH.NoReferensi
            lblTipePengajuan.Text = DepositBEnum.GetStringValueTipePengajuan(objDepositBPencairanH.TipePengajuan)
            lblPostingDate.Text = Format(objDepositBPencairanH.CreatedTime, "dd/MM/yyyy")
            txtNomorRekening.Text = objDepositBPencairanH.DealerBankAccount.BankAccount

            'lnkAccount.Attributes("onclick") = "ShowPPDealerBankAccountSelection(" & objDepositBPencairanH.Dealer.ID.ToString & ");"
            BindDetailToGrid()
        End If
    End Sub

    Private Sub BindDetailToGrid()
        Dim TotalAmount As Double = 0
        Dim objDepositBPencairanH As DepositBPencairanHeader = sessHelper.GetSession("PopupDepositBPencairanHeader")
        If Not IsNothing(objDepositBPencairanH) Then
            Dim arlDepositBPencairanD As ArrayList = objDepositBPencairanH.DepositBPencairanDetails

            If (arlDepositBPencairanD.Count > 0) Then
                dgEntryPencairanDepositA.Visible = True
                dgEntryPencairanDepositA.DataSource = arlDepositBPencairanD
                dgEntryPencairanDepositA.DataBind()
            Else
                dgEntryPencairanDepositA.Visible = False

                MessageBox.Show("Data tidak ditemukan")
            End If

            Dim selectedTipe As DepositBEnum.TipePengajuan = CType([Enum].Parse(GetType(DepositBEnum.TipePengajuan), objDepositBPencairanH.TipePengajuan), DepositBEnum.TipePengajuan)

            Select Case selectedTipe
                Case DepositBEnum.TipePengajuan.Interest
                    dgEntryPencairanDepositA.Columns(1).HeaderText = "Interest"
                    dgEntryPencairanDepositA.Columns(2).HeaderText = "Tax 15%"
                Case DepositBEnum.TipePengajuan.Transfer
                    'dgEntryPencairanDepositA.Columns(3).Visible = False
                    'dgEntryPencairanDepositA.Columns(3).Visible = False
                Case Else
                    dgEntryPencairanDepositA.Columns(2).HeaderText = "Tax 10%"
            End Select

            For Each item As DataGridItem In dgEntryPencairanDepositA.Items
                If ((item.ItemType = ListItemType.Item) Or (item.ItemType = ListItemType.AlternatingItem)) Then
                    Dim lblJumlahPencairan As Label = CType(item.FindControl("lblJumlahPencairan"), Label)
                    Dim lblPPn As Label = CType(item.FindControl("lblPPn"), Label)
                    Dim amount As Double = CType(lblJumlahPencairan.Text, Double)
                    Dim ppn As Double = CType(lblPPn.Text, Double)
                    'TotalAmount = TotalAmount + (amount + ppn)
                    TotalAmount = TotalAmount + CalcHelper.PPNCalculation(CalcSourceTypeEnum.Total, ppn, 0, amount)
                End If
            Next

            lblTotal.Text = FormatNumber(TotalAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)

        End If

    End Sub

    'Private Function GetTotalPDepositB(ByVal dealerID As Integer, ByVal productCategoryID As Integer, ByVal periode As Integer) As Decimal
    '    Dim totalDepositB As Decimal = 0
    '    Dim transactioDateStart As DateTime = New DateTime(periode, 1, 1, 0, 0, 0)
    '    Dim transactioDateTo As DateTime = New DateTime(periode, 12, 31, 23, 59, 59)

    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "Dealer.ID", MatchType.Exact, dealerID))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "ProductCategory.ID", MatchType.Exact, productCategoryID))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "TransactionDate", MatchType.GreaterOrEqual, transactioDateStart))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "TransactionDate", MatchType.Lesser, transactioDateTo))

    '    Dim sortColl As SortCollection = New SortCollection
    '    sortColl.Add(New Sort(GetType(DepositBHeader), "TransactionDate", Sort.SortDirection.DESC))

    '    Dim arlDepositB As ArrayList = New DepositBHeaderFacade(User).Retrieve(criterias, sortColl)
    '    If arlDepositB.Count > 0 Then
    '        Dim objDepositB As DepositBHeader = CType(arlDepositB(0), DepositBHeader)
    '        totalDepositB = objDepositB.EndBalance + totalDepositB
    '    End If
    '    Return totalDepositB
    'End Function

    'Private Function GetTotalYangSudahDiAjukan(ByVal objDepositBPencairanHeader As DepositBPencairanHeader, ByVal dealerID As Integer, ByVal iTipePengajuan As Integer, ByVal productCategoryID As Integer) As Decimal
    '    Dim totalPengajuan As Decimal = 0
    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "Dealer.ID", MatchType.Exact, dealerID))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "ProductCategory.ID", MatchType.Exact, productCategoryID))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "Status", MatchType.InSet, "(0, 1, 4, 6 )"))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "ID", MatchType.NotInSet, "(" & objDepositBPencairanHeader.ID & ")"))

    '    Dim arlPencairan As ArrayList = New DepositBPencairanHeaderFacade(User).Retrieve(criterias)
    '    For Each pencairan As DepositBPencairanHeader In arlPencairan
    '        If pencairan.Status = DepositBEnum.StatusPencairan.Baru Or _
    '            pencairan.Status = DepositBEnum.StatusPencairan.Validasi Then
    '            totalPengajuan = pencairan.DealerAmount + totalPengajuan
    '        Else
    '            totalPengajuan = pencairan.ApprovalAmount + totalPengajuan
    '        End If
    '    Next
    '    Return totalPengajuan

    'End Function

    'Private Function GetTotalPlafon(ByVal dealerID As Integer, ByVal productCategoryID As Integer, ByVal periode As Integer) As Decimal
    '    Dim totalPlafon As Decimal = 0

    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "Dealer.ID", MatchType.Exact, dealerID))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "ProductCategory.ID", MatchType.Exact, productCategoryID))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "PeriodePlafon", MatchType.Exact, periode))

    '    Dim arlPlafon As ArrayList = New DepositBPlafonFacade(User).Retrieve(criterias)
    '    For Each plafon As DepositBPlafon In arlPlafon
    '        totalPlafon = plafon.JumlahPlafon + totalPlafon
    '    Next
    '    Return totalPlafon
    'End Function

    'Private Function PlafonChecking(ByVal objDepositBPencairanH As DepositBPencairanHeader, ByVal periode As Integer, ByRef msg As String) As Boolean

    '    Dim TotalAmount As Decimal = objDepositBPencairanH.DealerAmount
    '    Dim dealerID As Integer = objDepositBPencairanH.Dealer.ID
    '    Dim productCategoryID As Integer = objDepositBPencairanH.ProductCategory.ID
    '    Dim tipePengajuan As Integer = objDepositBPencairanH.TipePengajuan

    '    Dim totalDeposit As Decimal = GetTotalPDepositB(dealerID, productCategoryID, periode)
    '    If Not (totalDeposit > 0) Then
    '        msg = "Dealer tidak mempunyai Deposit B."
    '        Return False
    '    End If

    '    Dim totalPlafon = GetTotalPlafon(dealerID, productCategoryID, periode)
    '    If totalPlafon = 0 Then
    '        msg = "Plafon belum dibuat."
    '        Return False
    '    End If

    '    Dim totalDone = GetTotalYangSudahDiAjukan(objDepositBPencairanH, dealerID, tipePengajuan, productCategoryID)

    '    Dim maxPencairan = totalDeposit - totalPlafon

    '    Dim totalMaxPengajuan As Decimal = 0

    '    'totalMaxPengajuan = maxPencairan - totalDone

    '    If TotalAmount > totalMaxPengajuan Then
    '        'msg = "Plafon tidak mencukupi, maximal nilai pengajuan : " & FormatNumber(totalMaxPengajuan, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault) + ". "
    '        msg = "Plafon tidak mencukupi, maximal nilai pengajuan : " & FormatNumber(maxPencairan, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault) + ". " + Environment.NewLine
    '        msg = msg + " Nilai pengajuan dalam proses : " & FormatNumber(totalDone, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault) + ". " + Environment.NewLine

    '        Return True
    '    Else
    '        msg = String.Empty
    '        Return True
    '    End If

    'End Function
#End Region

#Region "Cek Privilege"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.DepositAView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SERVICE - Pengajuan Pencairan Deposit B Detail")
        End If

    End Sub

    Private Function CekBtnPriv() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.PengajuanClaimCreateData_Privilege) Then
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
            objDealerLogin = CType(sessHelper.GetSession("DEALER"), Dealer)
            ViewState("HeaderID") = CInt(Request.Params("id").ToString())
            BindData(CInt(Request.Params("id").ToString()))
            'lnkAccount.Attributes("onclick") = "ShowPPDealerBankAccountSelection(" & oDealer.ID & ");"
        End If
    End Sub

    Private Sub dgEntryPencairanDepositA_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEntryPencairanDepositA.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "edit"
                dgEntryPencairanDepositA.EditItemIndex = e.Item.ItemIndex
            Case "cancel"
                dgEntryPencairanDepositA.EditItemIndex = -1
        End Select
        BindDetailToGrid()
    End Sub

    Private Sub dgEntryPencairanDepositA_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEntryPencairanDepositA.CancelCommand
        dgEntryPencairanDepositA.EditItemIndex = -1
        txtNomerSuratPengajuan.Enabled = False
        txtNomorRekening.Enabled = False
        lnkAccount.Visible = False
        BindDetailToGrid()
    End Sub

    Private Sub dgEntryPencairanDepositA_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEntryPencairanDepositA.EditCommand
        dgEntryPencairanDepositA.EditItemIndex = e.Item.ItemIndex
        txtNomerSuratPengajuan.Enabled = True
        txtNomorRekening.Enabled = True
        lnkAccount.Visible = True
        BindDetailToGrid()

    End Sub

    Private Sub dgEntryPencairanDepositA_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEntryPencairanDepositA.UpdateCommand
        Dim objDepositBPencairanH As DepositBPencairanHeader = sessHelper.GetSession("PopupDepositBPencairanHeader")
        Dim objPencairanDetail As DepositBPencairanDetail = objDepositBPencairanH.DepositBPencairanDetails.Item(e.Item.ItemIndex)

        Dim txtJumlahPencairanEdit As TextBox = e.Item.FindControl("txtJumlahPencairanEdit")
        objPencairanDetail.DealerAmount = CDbl(txtJumlahPencairanEdit.Text)

        'Dim txtPPnEdit As TextBox = e.Item.FindControl("txtPPnEdit")
        'objPencairanDetail. = CDbl(txtPPnEdit.Text)

        Dim txtPenjelasanEntryEdit As TextBox = e.Item.FindControl("txtPenjelasanEntryEdit")
        objPencairanDetail.Description = txtPenjelasanEntryEdit.Text

        Dim nResultDetail As Integer = New DepositBPencairanDetailFacade(User).Update(objPencairanDetail)

        If nResultDetail <> -1 Then
            'looping detail untuk di sum
            Dim objD As DepositBPencairanDetail = New DepositBPencairanDetail
            Dim dblTotal As Double = 0
            For Each objD In objDepositBPencairanH.DepositBPencairanDetails
                dblTotal = dblTotal + objD.DealerAmount
            Next
            objDepositBPencairanH.DealerAmount = dblTotal

            Dim iPeriod As Integer

            Select Case CType(objDepositBPencairanH.TipePengajuan, DepositBEnum.TipePengajuan)
                Case DepositBEnum.TipePengajuan.Transfer
                    iPeriod = objDepositBPencairanH.CreatedTime.Year
                Case DepositBEnum.TipePengajuan.Interest
                    iPeriod = objDepositBPencairanH.DepositBInterestHeader.Periode
                Case DepositBEnum.TipePengajuan.ProjectService
                    iPeriod = objDepositBPencairanH.DepositBDebitNote.PostingDate.Year
                Case DepositBEnum.TipePengajuan.Offset_SP
                    iPeriod = objDepositBPencairanH.CreatedTime.Year
                Case Else
                    iPeriod = objDepositBPencairanH.DepositBKewajibanHeader.PeriodYear
            End Select

            Dim bPlafonChecking As Boolean = True
            Dim msgPlafonChecking As String

            Dim objDepositBHelper As DepositBHelper = New DepositBHelper
            bPlafonChecking = objDepositBHelper.PlafonChecking(objDepositBPencairanH.Dealer.ID, _
                                                               objDepositBPencairanH.ProductCategory.ID, _
                                                               objDepositBPencairanH.TipePengajuan, _
                                                               iPeriod, objDepositBPencairanH.DealerAmount, msgPlafonChecking)

            'bPlafonChecking = PlafonChecking(objDepositBPencairanH, iPeriod, msgPlafonChecking)

            If bPlafonChecking = False And msgPlafonChecking <> "" Then
                MessageBox.Show(msgPlafonChecking)
                Exit Sub
            End If

            If bPlafonChecking = True Then
                If msgPlafonChecking <> "" Then
                    lblErrMessage.Text = msgPlafonChecking + ". Apakah akan dilanjutkan?"
                    Confirmation(True)
                    Exit Sub
                Else
                    Update(objDepositBPencairanH, False)
                End If
            End If
            BindDetailToGrid()

        End If

    End Sub

    Private Sub Update(ByVal objDepositBPencairanHeader As DepositBPencairanHeader, ByVal isPlafonOver As Boolean)
        If isPlafonOver Then
            objDepositBPencairanHeader.Flag = 1 'over plafon
        Else
            objDepositBPencairanHeader.Flag = 0 'accepted
        End If
        Dim nResult As Integer = New DepositBPencairanHeaderFacade(User).Update(objDepositBPencairanHeader)

        If nResult > -1 Then
            MessageBox.Show(SR.UpdateSucces)
        Else
            MessageBox.Show(SR.UpdateFail)
        End If
        dgEntryPencairanDepositA.EditItemIndex = -1

        BindDetailToGrid()
    End Sub

    Private Sub dgEntryPencairanDepositA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgEntryPencairanDepositA.ItemDataBound
        Dim objDepositBPencairanH As DepositBPencairanHeader = sessHelper.GetSession("PopupDepositBPencairanHeader")

        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim objPencairanDepositAD As DepositBPencairanDetail = CType(e.Item.DataItem, DepositBPencairanDetail)

            Dim selectedTipe As DepositBEnum.TipePengajuan = CType([Enum].Parse(GetType(DepositBEnum.TipePengajuan), objDepositBPencairanH.TipePengajuan), DepositBEnum.TipePengajuan)
            Dim ppn As Double = 0
            Dim amount As Double = 0
            'Select Case selectedTipe
            '    Case DepositBEnum.TipePengajuan.Interest
            '        amount = objPencairanDepositAD.DealerAmount - (objPencairanDepositAD.DealerAmount * 0.15)
            '        ppn = amount * 0.15
            '    Case DepositBEnum.TipePengajuan.Transfer
            '        amount = objPencairanDepositAD.DealerAmount
            '        ppn = 0
            '    Case Else
            '        amount = objPencairanDepositAD.DealerAmount - (objPencairanDepositAD.DealerAmount * 0.1)
            '        ppn = amount * 0.1
            'End Select
            Select Case selectedTipe
                Case DepositBEnum.TipePengajuan.Transfer
                    amount = objPencairanDepositAD.DealerAmount
                    ppn = 0
                Case DepositBEnum.TipePengajuan.ProjectService 'debit Note
                    Dim objDepositBDebitNote As DepositBDebitNote = objPencairanDepositAD.DepositBPencairanHeader.DepositBDebitNote
                    amount = objDepositBDebitNote.Amount
                    ppn = 0
                Case DepositBEnum.TipePengajuan.Interest 'Interest
                    Dim objDepositBInterestHeader As DepositBInterestHeader = objPencairanDepositAD.DepositBPencairanHeader.DepositBInterestHeader
                    'For Each detail As DepositBInterestDetail In objDepositBInterestHeader.DepositBInterestDetails
                    '    amount = amount + detail.InterestAmount
                    '    ppn = ppn + detail.TaxAmount
                    'Next
                    amount = amount + objDepositBInterestHeader.NettoAmount
                    ppn = ppn + objDepositBInterestHeader.TaxAmount
                Case DepositBEnum.TipePengajuan.Offset_SP
                    Dim objIndentPartHeader As IndentPartHeader = objPencairanDepositAD.DepositBPencairanHeader.IndentPartHeader
                    For Each detail As IndentPartDetail In objIndentPartHeader.IndentPartDetails
                        Dim estDetail As EstimationEquipDetail = detail.EstimationEquipDetail
                        Dim ppnFromMasterPPN = CalcHelper.GetPPNMasterByTaxTypeId(CType(lblPostingDate.Text, Date), objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                        Dim totalAmont As Double = ((estDetail.Harga * detail.Qty) - ((estDetail.Harga * (estDetail.Discount / 100)) * detail.Qty))
                        amount = amount + totalAmont
                        'ppn = ppn + (totalAmont * 0.1)
                        ppn = ppn + CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnFromMasterPPN, dpp:=totalAmont)
                    Next
                Case DepositBEnum.TipePengajuan.KewajibanReguler
                    Dim objDepositBKewajibanHeader As DepositBKewajibanHeader = objPencairanDepositAD.DepositBPencairanHeader.DepositBKewajibanHeader
                    For Each detail As DepositBKewajibanDetail In objDepositBKewajibanHeader.DepositBKewajibanDetails
                        amount = amount + detail.Harga
                        ppn = ppn + detail.Tax
                    Next
                Case DepositBEnum.TipePengajuan.Kewajiban_NonReguler
                    Dim objDepositBKewajibanHeader As DepositBKewajibanHeader = objPencairanDepositAD.DepositBPencairanHeader.DepositBKewajibanHeader
                    For Each detail As DepositBKewajibanDetail In objDepositBKewajibanHeader.DepositBKewajibanDetails
                        amount = amount + detail.Harga
                        ppn = ppn + detail.Tax
                    Next
            End Select

            Dim lblJumlahPencairan As Label = CType(e.Item.FindControl("lblJumlahPencairan"), Label)
            Dim lblPPn As Label = CType(e.Item.FindControl("lblPPn"), Label)
            'Select Case selectedTipe
            '    Case DepositBEnum.TipePengajuan.Interest
            '        lblJumlahPencairan.Text = FormatNumber(amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
            '    Case Else
            '        lblJumlahPencairan.Text = FormatNumber(amount + ppn, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
            'End Select
            lblJumlahPencairan.Text = FormatNumber(amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
            lblPPn.Text = FormatNumber(ppn, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)

            dgEntryPencairanDepositA.Columns(4).Visible = False
            '  dgEntryPencairanDepositA.Columns(4).Visible = True


            If (objPencairanDepositAD.DepositBPencairanHeader.TipePengajuan = DepositBEnum.TipePengajuan.Transfer) AndAlso _
                (objPencairanDepositAD.DepositBPencairanHeader.Status = DepositBEnum.StatusPencairan.Baru) Then

                objDealerLogin = CType(sessHelper.GetSession("DEALER"), Dealer)
                If objDealerLogin.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    dgEntryPencairanDepositA.Columns(4).Visible = True
                End If
            End If

        ElseIf (e.Item.ItemType = ListItemType.EditItem) Then
            Dim objPencairanDepositAD As DepositBPencairanDetail = CType(e.Item.DataItem, DepositBPencairanDetail)
            '
            Dim selectedTipe As DepositBEnum.TipePengajuan = CType([Enum].Parse(GetType(DepositBEnum.TipePengajuan), objDepositBPencairanH.TipePengajuan), DepositBEnum.TipePengajuan)
            Dim amount As Double = 0
            Dim ppn As Double = 0
            'Select Case selectedTipe
            '    Case DepositBEnum.TipePengajuan.Interest
            '        amount = objPencairanDepositAD.DealerAmount - (objPencairanDepositAD.DealerAmount * 0.15)
            '        ppn = amount * 0.15
            '    Case DepositBEnum.TipePengajuan.Transfer
            '        amount = objPencairanDepositAD.DealerAmount
            '        ppn = 0
            '    Case Else
            '        amount = objPencairanDepositAD.DealerAmount - (objPencairanDepositAD.DealerAmount * 0.1)
            '        ppn = amount * 0.1
            'End Select
            Select Case selectedTipe
                Case DepositBEnum.TipePengajuan.Transfer
                    amount = objPencairanDepositAD.DealerAmount
                    ppn = 0
                Case DepositBEnum.TipePengajuan.ProjectService 'debit Note
                    Dim objDepositBDebitNote As DepositBDebitNote = objPencairanDepositAD.DepositBPencairanHeader.DepositBDebitNote
                    amount = objDepositBDebitNote.Amount
                    ppn = 0
                Case DepositBEnum.TipePengajuan.Interest 'Interest
                    Dim objDepositBInterestHeader As DepositBInterestHeader = objPencairanDepositAD.DepositBPencairanHeader.DepositBInterestHeader
                    'For Each detail As DepositBInterestDetail In objDepositBInterestHeader.DepositBInterestDetails
                    '    amount = amount + detail.InterestAmount
                    '    ppn = ppn + detail.TaxAmount
                    'Next
                    amount = amount + objDepositBInterestHeader.NettoAmount
                    ppn = ppn + objDepositBInterestHeader.TaxAmount
                Case DepositBEnum.TipePengajuan.Offset_SP
                    Dim objIndentPartHeader As IndentPartHeader = objPencairanDepositAD.DepositBPencairanHeader.IndentPartHeader
                    Dim ppnFromMasterPPN = CalcHelper.GetPPNMasterByTaxTypeId(CType(lblPostingDate.Text, Date), objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                    For Each detail As IndentPartDetail In objIndentPartHeader.IndentPartDetails
                        Dim estDetail As EstimationEquipDetail = detail.EstimationEquipDetail
                        Dim totalAmont As Double = (estDetail.Discount * estDetail.Harga) * detail.Qty
                        amount = amount + totalAmont
                        'ppn = ppn + (totalAmont * 0.1)
                        ppn = ppn + CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnFromMasterPPN, dpp:=totalAmont)
                    Next
                Case DepositBEnum.TipePengajuan.KewajibanReguler
                    Dim objDepositBKewajibanHeader As DepositBKewajibanHeader = objPencairanDepositAD.DepositBPencairanHeader.DepositBKewajibanHeader
                    For Each detail As DepositBKewajibanDetail In objDepositBKewajibanHeader.DepositBKewajibanDetails
                        amount = amount + detail.Harga
                        ppn = ppn + detail.Tax
                    Next
                Case DepositBEnum.TipePengajuan.Kewajiban_NonReguler
                    Dim objDepositBKewajibanHeader As DepositBKewajibanHeader = objPencairanDepositAD.DepositBPencairanHeader.DepositBKewajibanHeader
                    For Each detail As DepositBKewajibanDetail In objDepositBKewajibanHeader.DepositBKewajibanDetails
                        amount = amount + detail.Harga
                        ppn = ppn + detail.Tax
                    Next
            End Select
            Dim txtJumlahPencairanEdit As TextBox = CType(e.Item.FindControl("txtJumlahPencairanEdit"), TextBox)
            Dim txtPPnEdit As TextBox = CType(e.Item.FindControl("txtPPnEdit"), TextBox)

            'Select Case selectedTipe
            '    Case DepositBEnum.TipePengajuan.Interest
            '        txtJumlahPencairanEdit.Text = FormatNumber(amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
            '    Case Else
            '        txtJumlahPencairanEdit.Text = FormatNumber(amount + ppn, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
            'End Select
            txtJumlahPencairanEdit.Text = FormatNumber(amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
            txtPPnEdit.Text = FormatNumber(ppn, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
            txtPPnEdit.ReadOnly = True
        End If
    End Sub

#End Region

    Private Sub Confirmation(ByVal value As Boolean)
        lblErrMessage.Visible = value
        btnYes.Visible = value
        btnNo.Visible = value
    End Sub

    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        Dim objDepositBPencairanH As DepositBPencairanHeader = sessHelper.GetSession("PopupDepositBPencairanHeader")
        Update(objDepositBPencairanH, True)
        Confirmation(False)
    End Sub

    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        Confirmation(False)
    End Sub
End Class

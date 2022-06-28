Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessValidation.Helpers

Public Class PopupPengajuanPencairanDepositAViewEdit
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgEntryPencairanDepositA As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents ltrDealerCode As System.Web.UI.WebControls.Literal
    Protected WithEvents lblPostingDate As System.Web.UI.WebControls.Label
    Protected WithEvents ltrDealerName As System.Web.UI.WebControls.Literal
    Protected WithEvents txtNomerSuratPengajuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTipePengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorRekening As System.Web.UI.WebControls.Label
    Protected WithEvents txtNomorRekening As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTipeDokumen As System.Web.UI.WebControls.Label
    Protected WithEvents rbDN As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbSO As System.Web.UI.WebControls.RadioButton
    Protected WithEvents lblDNSONumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblDNSONumberContent As System.Web.UI.WebControls.Label
    Protected WithEvents lblAssignmentNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblAssignmentNoValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblDNSONumberCaption As System.Web.UI.WebControls.Label
    Protected WithEvents lnkAccount As System.Web.UI.WebControls.LinkButton

    Protected WithEvents lblProduk1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblProduk2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblProduk3 As System.Web.UI.WebControls.Label
    'Protected WithEvents btnCancel As System.Web.UI.WebControls.Button

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
    Private objDepositAPencairanH As DepositAPencairanH = New DepositAPencairanH
    Private arlDepositAPencairanD As ArrayList = New ArrayList
    Private arlDepositAPencairanDFilter As ArrayList = New ArrayList
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)

    Dim sHelper As New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub BindDetailToGrid(ByVal ID As Integer)
        objDepositAPencairanH = New FinishUnit.DepositAPencairanHFacade(User).Retrieve(ID)
        'If objDepositAPencairanH.Status = 10 Or objDepositAPencairanH.Status = 1 Or objDepositAPencairanH.Status = 11 Or objDepositAPencairanH.Status = 12 Or objDepositAPencairanH.Status = 13 Then
        '    dgEntryPencairanDepositA.Columns(5).Visible = False
        'End If

       


        If objDepositAPencairanH.Status = EnumDepositA.StatusPencairanKTB.Konfirmasi Or objDepositAPencairanH.Status = EnumDepositA.StatusPencairanKTB.Validasi Or objDepositAPencairanH.Status = EnumDepositA.StatusPencairanKTB.Setuju Or objDepositAPencairanH.Status = EnumDepositA.StatusPencairanKTB.Tolak Or objDepositAPencairanH.Status = 13 Then
            dgEntryPencairanDepositA.Columns(5).Visible = False
        End If

        lnkAccount.Attributes("onclick") = "ShowPPDealerBankAccountSelection(" & objDepositAPencairanH.Dealer.ID.ToString & ");"

        fillDataDealer(objDepositAPencairanH.Dealer)
        txtNomerSuratPengajuan.Text = objDepositAPencairanH.NoSurat
        Dim selectedTipe As EnumDepositA.TipePengajuan = CType([Enum].Parse(GetType(EnumDepositA.TipePengajuan), objDepositAPencairanH.Type), EnumDepositA.TipePengajuan)
        lblTipePengajuan.Text = selectedTipe.GetName(GetType(EnumDepositA.TipePengajuan), selectedTipe)
        dgEntryPencairanDepositA.Columns(3).Visible = True

        Dim objDepositAKuitansiPencairans As DepositAKuitansiPencairan = New DepositAKuitansiPencairanFacade(User).Retrieve(objDepositAPencairanH.NoReg)

        If selectedTipe <> EnumDepositA.TipePengajuan.Offset Then
            txtNomorRekening.Text = objDepositAPencairanH.DealerBankAccount.BankAccount
            'lnkAccount.Visible = True
            If selectedTipe = EnumDepositA.TipePengajuan.CashInterest Then
                dgEntryPencairanDepositA.Columns(1).HeaderText = "Interest"
                'dgEntryPencairanDepositA.Columns(2).HeaderText = "Tax 15%"
                dgEntryPencairanDepositA.Columns(2).HeaderText = "Tax"
                dgEntryPencairanDepositA.Columns(3).HeaderText = "Netto Amount"
            ElseIf selectedTipe = EnumDepositA.TipePengajuan.CashIncidental Then
                dgEntryPencairanDepositA.Columns(3).Visible = False
            End If
        Else
            txtNomorRekening.Text = String.Empty
            lnkAccount.Visible = False
            dgEntryPencairanDepositA.Columns(3).HeaderText = "PPn"
            If objDepositAPencairanH.DNNumber = String.Empty Then
                dgEntryPencairanDepositA.Columns(3).Visible = False
            End If

        End If
        lblPostingDate.Text = Format(objDepositAPencairanH.CreatedTime, "dd/MM/yyyy")
        lblAssignmentNoValue.Text = objDepositAPencairanH.AssignmentNumber
        lblDNSONumberContent.Text = objDepositAPencairanH.DNNumber



        'If objDepositAPencairanH.Type = EnumDepositA.TipePengajuan.Offset Then
        '    lblProduk1.Visible = False
        '    lblProduk2.Visible = False
        '    lblProduk3.Visible = False
        '    If objDepositAPencairanH.DNNumber.Trim().Equals(String.Empty) Then
        '        Dim objSalesOrder As New SalesOrder
        '        objSalesOrder = New SalesOrderFacade(User).Retrieve(objDepositAPencairanH.AssignmentNumber)
        '        Dim ObjProductCategory As ProductCategory = CType(objSalesOrder.POHeader.PODetails(0), PODetail).ContractDetail.VechileColor.VechileType.Category.ProductCategory
        '        lblProduk3.Text = ObjProductCategory.Code
        '        lblAssignmentNoValue.Text = objDepositAPencairanH.AssignmentNumber & " - " & ObjProductCategory.Code

        '    Else
        '        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DebitNote), "DNNumber", MatchType.Exact, objDepositAPencairanH.DNNumber))

        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DebitNote), "Dealer.ID", MatchType.Exact, objDepositAPencairanH.Dealer.ID))

        '        Dim ArrDebNote As ArrayList = New FinishUnit.DebitNoteFacade(User).Retrieve(criterias)
        '        Dim objDebitNote As DebitNote
        '        If Not IsNothing(ArrDebNote) AndAlso ArrDebNote.Count > 0 Then
        '            objDebitNote = CType(ArrDebNote(0), DebitNote)
        '        Else
        '            objDebitNote = New DebitNote
        '        End If




        '        lblDNSONumberContent.Text = objDepositAPencairanH.DNNumber & " - " & objDebitNote.ProductCategory.Code
        '    End If
        'End If


        arlDepositAPencairanD = objDepositAPencairanH.DepositAPencairanDs
        Dim DealerCode As String = String.Empty
        For Each item As DepositAPencairanD In arlDepositAPencairanD
            'If (Not IsExist(item.Dealer.DealerCode, arlDepositAPencairan Filter)) Then
            arlDepositAPencairanDFilter.Add(item)
            'End If
        Next

        If (arlDepositAPencairanDFilter.Count > 0) Then
            dgEntryPencairanDepositA.Visible = True
            dgEntryPencairanDepositA.DataSource = arlDepositAPencairanDFilter
            dgEntryPencairanDepositA.DataBind()
        Else
            dgEntryPencairanDepositA.Visible = False

            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Sub fillDataDealer(ByVal oD As Dealer)
        ltrDealerCode.Text = String.Format("{0} / {1}", oD.DealerCode.ToString(), oD.SearchTerm2)
        ltrDealerName.Text = oD.DealerName
    End Sub

#End Region

#Region "Cek Privilege"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.DepositAView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FINISH UNIT - Pengajuan Pencairan Deposit A Detail")
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
        Session("IsBindDataGrid") = False
        If Not IsPostBack Then
            Dim oDealer As Dealer
            oDealer = CType(sHelper.GetSession("DEALER"), Dealer)
            'lnkAccount.Attributes("onclick") = "ShowPPDealerBankAccountSelection(" & oDealer.ID & ");"
            ViewState("HeaderID") = CInt(Request.Params("id").ToString())
            BindDetailToGrid(CInt(Request.Params("id").ToString()))
            'BindDetailToGrid(CInt(Request.QueryString("id")))
        End If
    End Sub

    Private TotalAmount As Double = 0

    Private Sub dgEntryPencairanDepositA_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEntryPencairanDepositA.CancelCommand
        dgEntryPencairanDepositA.EditItemIndex = -1
        txtNomerSuratPengajuan.Enabled = False
        txtNomorRekening.Enabled = False
        lnkAccount.Visible = False
        BindDetailToGrid(ViewState("HeaderID"))
        'MessageBox.Show("Masuk Cancel Command")
    End Sub

    Private Sub dgEntryPencairanDepositA_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEntryPencairanDepositA.EditCommand
        dgEntryPencairanDepositA.EditItemIndex = e.Item.ItemIndex
        txtNomerSuratPengajuan.Enabled = True
        txtNomorRekening.Enabled = True
        lnkAccount.Visible = True
        BindDetailToGrid(ViewState("HeaderID"))

    End Sub

    Private Sub dgEntryPencairanDepositA_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEntryPencairanDepositA.ItemCommand
        'If e.CommandName = "Edit" Then
        '    dgEntryPencairanDepositA.EditItemIndex = e.Item.ItemIndex
        '    BindDetailToGrid(ViewState("HeaderID"))
        '    MessageBox.Show("Command Edit")
        'End If

        'If e.CommandName = "Cancel" Then
        '    MessageBox.Show("Command Cancel")
        '    dgEntryPencairanDepositA.EditItemIndex = -1
        '    BindDetailToGrid(ViewState("HeaderID"))
        'End If
    End Sub

    Private Sub dgEntryPencairanDepositA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgEntryPencairanDepositA.ItemDataBound
        Dim PPn As Decimal = 0
        Dim dppVal As Decimal = 0
        Dim total As Double = 0
        Dim objDepositAKuitansiPencairans As DepositAKuitansiPencairan = New DepositAKuitansiPencairanFacade(User).Retrieve(objDepositAPencairanH.NoReg)

        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim objPencairanDepositAD As DepositAPencairanD = CType(e.Item.DataItem, DepositAPencairanD)

            Dim selectedTipe As Integer = objPencairanDepositAD.DepositAPencairanH.Type
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgEntryPencairanDepositA.CurrentPageIndex * dgEntryPencairanDepositA.PageSize)

            total = Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "DealerAmount"))
            Dim lblPPn As Label = CType(e.Item.FindControl("lblPPn"), Label)

            Dim lblHeaderAmount As Label = CType(e.Item.FindControl("lblHeaderAmount"), Label)
            Dim lblJumlahPencairan As Label = CType(e.Item.FindControl("lblJumlahPencairan"), Label)
            If selectedTipe = EnumDepositA.TipePengajuan.Offset Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DebitNote), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.DebitNote), "Amount", AggregateType.Sum)
                If objPencairanDepositAD.DepositAPencairanH.DNNumber.ToString() <> "" Then
                    ' e.Item.Cells(5).Visible = False
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DebitNote), "DNNumber", MatchType.Exact, objPencairanDepositAD.DepositAPencairanH.DNNumber))
                    Dim DNAmount As Double = New FinishUnit.DebitNoteFacade(User).RetrieveScalar(criterias, agg)
                    lblHeaderAmount.Text = DNAmount.ToString("#,####")
                    Dim objDebitNote As DebitNote = New FinishUnit.DebitNoteFacade(User).Retrieve(objPencairanDepositAD.DepositAPencairanH.DNNumber)
                    Dim ppnVal As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objDebitNote.PostingDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                    'PPn = 0.10000000000000001 * objPencairanDepositAD.DealerAmount
                    dppVal = CalcHelper.DPPCalculation(0, objPencairanDepositAD.DealerAmount, ppnVal)
                    PPn = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=dppVal)
                    lblPPn.Text = PPn.ToString("#,###")
                    lblJumlahPencairan.Text = dppVal.ToString("#,###")
                    total = dppVal + PPn
                Else
                    Dim SOAmount As Double = 0
                    Dim objSalesOrder As New SalesOrder
                    objSalesOrder = New SalesOrderFacade(User).Retrieve(objPencairanDepositAD.DepositAPencairanH.AssignmentNumber)
                    If objSalesOrder.ID > 0 Then
                        SOAmount = objSalesOrder.Amount
                    End If
                    'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "SONumber", MatchType.Exact, objPencairanDepositAD.DepositAPencairanH.AssignmentNumber))
                    'Dim SOAmount As Double = New FinishUnit.DebitNoteFacade(User).RetrieveScalar(criterias, agg)

                    lblHeaderAmount.Text = SOAmount.ToString("#,####")
                    'lblPPn.Text = PPn.ToString("#,###")                    
                End If
                'lblHeaderAmount.Visible = True
            Else
                If selectedTipe = EnumDepositA.TipePengajuan.CashIncidental Then
                    'PPn = 0.1 * objPencairanDepositAD.DealerAmount
                    'TotalAmount = TotalAmount + PPn
                End If
                'lblPPn.Text = PPn.ToString("#,###")

                If selectedTipe = EnumDepositA.TipePengajuan.CashInterest Then
                    Dim objDepositAPencairan As New DepositAPencairanH
                    objDepositAPencairan = New DepositAPencairanHFacade(User).Retrieve(objPencairanDepositAD.DepositAPencairanH.ID)
                    If objDepositAPencairan.ID > 0 Then
                        Dim objInterest As New DepositAInterestH
                        objInterest = objDepositAPencairan.DepositAInterestH
                        lblHeaderAmount.Text = objInterest.InterestAmount.ToString("#,###")
                        lblJumlahPencairan.Text = objInterest.TaxAmount.ToString("#,###")
                        lblPPn.Text = objInterest.NettoAmount.ToString("#,###")
                    End If
                End If
                'e.Item.Cells(1).Visible = False
                'lblHeaderAmount.Visible = False
            End If

            TotalAmount += total
        ElseIf (e.Item.ItemType = ListItemType.EditItem) Then
            Dim txtPPnEdit As TextBox = CType(e.Item.FindControl("txtPPnEdit"), TextBox)
            Dim txtJumlahPencairanEdit As TextBox = CType(e.Item.FindControl("txtJumlahPencairanEdit"), TextBox)
            Dim temp As String = "CalculatePPn('dgEntryPencairanDepositA__ctl_txtJumlahPencairanEdit_', 'dgEntryPencairanDepositA__ctl_txtPPnEdit_' )"
            txtJumlahPencairanEdit.Attributes.Add("OnBlur", temp)
            Dim txtHeaderAmountEdit As TextBox = CType(e.Item.FindControl("txtHeaderAmountEdit"), TextBox)
            Dim txtPenjelasanEntryEdit As TextBox = CType(e.Item.FindControl("txtPenjelasanEntryEdit"), TextBox)
            Dim objPencairanDepositAD As DepositAPencairanD = CType(e.Item.DataItem, DepositAPencairanD)
            Dim selectedTipe As Integer = objPencairanDepositAD.DepositAPencairanH.Type
            total = Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "DealerAmount"))
            If selectedTipe = EnumDepositA.TipePengajuan.Offset Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DebitNote), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.DebitNote), "Amount", AggregateType.Sum)
                If objPencairanDepositAD.DepositAPencairanH.DNNumber.ToString() <> "" Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DebitNote), "DNNumber", MatchType.Exact, objPencairanDepositAD.DepositAPencairanH.DNNumber))
                    Dim DNAmount As Double = New FinishUnit.DebitNoteFacade(User).RetrieveScalar(criterias, agg)
                    txtHeaderAmountEdit.Text = DNAmount.ToString("#,####")
                    'Dim PPn As Double = 0.10000000000000001 * objPencairanDepositAD.DealerAmount
                    Dim objDebitNote As DebitNote = New FinishUnit.DebitNoteFacade(User).Retrieve(objPencairanDepositAD.DepositAPencairanH.DNNumber)
                    Dim ppnVal As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objDebitNote.PostingDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                    dppVal = CalcHelper.DPPCalculation(0, objPencairanDepositAD.DealerAmount, ppnVal)
                    PPn = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=dppVal)
                    txtPPnEdit.Text = PPn.ToString("#,###")
                    txtJumlahPencairanEdit.Text = dppVal.ToString("#,###")
                    total = dppVal + PPn
                Else
                    Dim SOAmount As Double = 0
                    Dim objSalesOrder As New SalesOrder
                    objSalesOrder = New SalesOrderFacade(User).Retrieve(objPencairanDepositAD.DepositAPencairanH.AssignmentNumber)
                    If objSalesOrder.ID > 0 Then
                        SOAmount = objSalesOrder.Amount
                    End If
                    txtHeaderAmountEdit.Text = SOAmount.ToString("#,####")
                    'Dim PPn As Double = 0
                    'txtPPnEdit.Text = PPn.ToString("#,###")
                    total = objPencairanDepositAD.DealerAmount
                End If

                txtJumlahPencairanEdit.ReadOnly = True
                txtPenjelasanEntryEdit.ReadOnly = True
            Else
                'PPn = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=objPencairanDepositAD.DealerAmount)
                If selectedTipe = EnumDepositA.TipePengajuan.CashIncidental Then
                    'txtPPnEdit.Text = PPn.ToString("#,###")
                    total = objPencairanDepositAD.DealerAmount
                End If

                If selectedTipe = EnumDepositA.TipePengajuan.CashAnnual Or selectedTipe = EnumDepositA.TipePengajuan.CashInterest Then

                    txtJumlahPencairanEdit.ReadOnly = True
                    txtPenjelasanEntryEdit.ReadOnly = True
                Else
                    txtJumlahPencairanEdit.ReadOnly = False
                    txtPenjelasanEntryEdit.ReadOnly = False
                End If

                If selectedTipe = EnumDepositA.TipePengajuan.CashInterest Then
                    Dim objDepositAPencairan As New DepositAPencairanH
                    objDepositAPencairan = New DepositAPencairanHFacade(User).Retrieve(objPencairanDepositAD.DepositAPencairanH.ID)
                    If objDepositAPencairan.ID > 0 Then
                        Dim objInterest As New DepositAInterestH
                        objInterest = objDepositAPencairan.DepositAInterestH
                        txtHeaderAmountEdit.Text = objInterest.InterestAmount.ToString("#,###")
                        txtJumlahPencairanEdit.Text = objInterest.TaxAmount.ToString("#,###")
                        txtPPnEdit.Text = objInterest.NettoAmount.ToString("#,###")
                    End If
                End If
            End If

            TotalAmount += total
        End If
        lblTotal.Text = IIf(TotalAmount = 0, 0, TotalAmount.ToString("#,###"))
    End Sub

    Private Sub dgEntryPencairanDepositA_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEntryPencairanDepositA.UpdateCommand
        Dim HeaderID As Integer = ViewState("HeaderID")
        objDepositAPencairanH = New FinishUnit.DepositAPencairanHFacade(User).Retrieve(HeaderID)
        Dim objPencairanDetail As DepositAPencairanD = objDepositAPencairanH.DepositAPencairanDs.Item(e.Item.ItemIndex)
        TotalAmount = 0

        Dim txtHeaderAmountEdit As TextBox = e.Item.FindControl("txtHeaderAmountEdit")
        Dim txtJumlahPencairanEdit As TextBox = e.Item.FindControl("txtJumlahPencairanEdit")
        Dim txtPPnEdit As TextBox = e.Item.FindControl("txtPPnEdit")
        If objDepositAPencairanH.Type = EnumDepositA.TipePengajuan.Offset Then
            If CDbl(txtJumlahPencairanEdit.Text) > CDbl(txtHeaderAmountEdit.Text) Then
                MessageBox.Show("[Offset] jumlah pencairan tidak boleh lebih dari jumlah yang ditentukan")
                Return
            Else
                objPencairanDetail.DealerAmount = CDbl(txtJumlahPencairanEdit.Text)
            End If
        Else
            If objDepositAPencairanH.Type = EnumDepositA.TipePengajuan.CashInterest Then
                objPencairanDetail.DealerAmount = CDbl(txtPPnEdit.Text)
            Else
                objPencairanDetail.DealerAmount = CDbl(txtJumlahPencairanEdit.Text)
            End If

        End If

        Dim txtPenjelasanEntryEdit As TextBox = e.Item.FindControl("txtPenjelasanEntryEdit")
        objPencairanDetail.Description = txtPenjelasanEntryEdit.Text

        'looping detail untuk di sum
        Dim objD As DepositAPencairanD = New DepositAPencairanD
        Dim dblTotal As Double = 0
        For Each objD In objDepositAPencairanH.DepositAPencairanDs
            dblTotal = dblTotal + objD.DealerAmount
        Next
        'objDepositAPencairanH.DealerAmount = CDbl(lblTotal.Text)
        objDepositAPencairanH.DealerAmount = dblTotal
        objDepositAPencairanH.NoSurat = txtNomerSuratPengajuan.Text

        If objDepositAPencairanH.Type <> EnumDepositA.TipePengajuan.Offset Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "BankAccount", MatchType.Exact, txtNomorRekening.Text))
            Dim arlAccount As ArrayList = New FinishUnit.DealerBankAccountFacade(User).Retrieve(criterias)
            If arlAccount.Count > 0 Then
                objDepositAPencairanH.DealerBankAccount = arlAccount(0)
            End If
        End If

        Dim nResult As Integer = New FinishUnit.DepositAPencairanHFacade(User).Update(objDepositAPencairanH)
        If nResult > -1 Then
            txtNomerSuratPengajuan.Enabled = False
            lnkAccount.Visible = False
            MessageBox.Show(SR.UpdateSucces)
        Else
            MessageBox.Show(SR.UpdateFail)
        End If
        dgEntryPencairanDepositA.EditItemIndex = -1
        BindDetailToGrid(HeaderID)
        'MessageBox.Show("masuk Command Update")
        Session("IsBindDataGrid") = True
    End Sub

#End Region

#Region "Internal Enum"
    'Private Enum TipePengajuan
    '    Offset = 1
    '    CashAnnual = 2
    '    CashIncidental = 3
    '    CashInterest = 4
    'End Enum
#End Region



End Class

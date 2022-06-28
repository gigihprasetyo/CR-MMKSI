Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports System.Globalization
Imports KTB.DNet.BusinessValidation.Helpers
Public Class PopUpPengembalianLogisticPPHDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ltrDealerCode As System.Web.UI.WebControls.Literal
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents ltrDealerName As System.Web.UI.WebControls.Literal
    Protected WithEvents dgPengembalianPPH As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTglPPH As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPPH As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents txtNoPPH As System.Web.UI.WebControls.TextBox
    Protected WithEvents lnlNoPPH As System.Web.UI.WebControls.Label

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
    Private objPFRH As LogisticPPHHeader = New LogisticPPHHeader
    Private arlPFRD As ArrayList
    Dim sessHelper As New SessionHelper
    Private TotalAmount As Double = 0
    Private objDealer As Dealer
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'InitiateAuthorization()
        If Not IsPostBack Then
            objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
            ViewState("HeaderID") = CInt(Request.Params("id").ToString())
            If Not IsNothing(Request.Params("id")) Then
                objPFRH = New FinishUnit.LogisticPPHHeaderFacade(User).Retrieve(CInt(Request.Params("id").ToString()))
                sessHelper.SetSession("PFRH", objPFRH)

                arlPFRD = CType(objPFRH.LogisticPPHDetails, ArrayList)
                sessHelper.SetSession("ArlPFRD", arlPFRD)
                FillDataDealer()
            End If
            BindDetailToGrid()
            If CInt(Request.QueryString("edit")) = 1 Then
                dgPengembalianPPH.ShowFooter = True
                btnSimpan.Enabled = True
            Else
                dgPengembalianPPH.ShowFooter = False
                btnSimpan.Enabled = False
            End If
        End If
    End Sub

    Private Sub dgPengembalianPPH_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPengembalianPPH.ItemDataBound
        Dim ddlDebitMemo As DropDownList
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Dim arlPFBind As ArrayList = CType(sessHelper.GetSession("ArlPFRD"), ArrayList)
        Dim RowValue As LogisticPPHDetail = CType(e.Item.DataItem, LogisticPPHDetail)
        If (e.Item.ItemType = ListItemType.Footer) OrElse (e.Item.ItemType = ListItemType.EditItem) Then
            If e.Item.ItemType = ListItemType.Footer Then
                ddlDebitMemo = CType(e.Item.FindControl("ddlDebitMemoF"), DropDownList)
            ElseIf e.Item.ItemType = ListItemType.EditItem Then
                ddlDebitMemo = CType(e.Item.FindControl("ddlDebitMemoE"), DropDownList)
            End If
            objPFRH = CType(sessHelper.GetSession("PFRH"), LogisticPPHHeader)

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.LogisticFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.LogisticFee), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.LogisticFee), "Status", MatchType.Exact, CType(EnumLogisticFeeStatus.LogisticFeeStatus.Baru, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.LogisticFee), "Dealer.CreditAccount", MatchType.Exact, objPFRH.Dealer.CreditAccount))
            If arlPFBind.Count > 0 Then
                Dim oDetail As LogisticPPHDetail = CType(arlPFBind(0), LogisticPPHDetail)
                Dim tglinvoice As DateTime = oDetail.LogisticFee.LogisticDN.BillingDate
                Dim awalbulan As DateTime = New DateTime(tglinvoice.Year, tglinvoice.Month, 1)
                Dim akhirbulan As DateTime = awalbulan.AddMonths(1).AddDays(-1)
                If oDetail.ID > 0 Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.LogisticFee), "LogisticDN.BillingDate", MatchType.GreaterOrEqual, awalbulan))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.LogisticFee), "LogisticDN.BillingDate", MatchType.LesserOrEqual, akhirbulan))
                End If
            End If
            Dim arlPF As ArrayList = New FinishUnit.LogisticFeeFacade(User).Retrieve(criterias)

            Dim arlPFRD As ArrayList = CType(sessHelper.GetSession("ArlPFRD"), ArrayList)

            Dim blankItem As New ListItem("Silahkan Pilih", 0)
            ddlDebitMemo.Items.Add(blankItem)
            For Each item As LogisticFee In arlPF
                Dim listItem As New ListItem(item.LogisticDN.DebitMemoNo, item.ID)
                If arlPFRD.Count > 0 Then
                    Dim oDet As LogisticPPHDetail = CType(arlPFRD(0), LogisticPPHDetail)

                    ddlDebitMemo.Items.Add(listItem)

                Else
                ddlDebitMemo.Items.Add(listItem)
                End If

            Next
            If ddlDebitMemo.Items.Count > 0 Then
                ddlDebitMemo.SelectedIndex = 0
            Else
                ddlDebitMemo.ClearSelection()
            End If
        End If
        Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
        If Not IsNothing(lblDealer) Then
            If Not IsNothing(RowValue) Then
                lblDealer.Text = RowValue.LogisticFee.Dealer.DealerCode
            Else
                lblDealer.Text = ""
            End If
        End If

        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        If Not IsNothing(lblNo) Then
            lblNo.Text = e.Item.ItemIndex + 1 + (dgPengembalianPPH.CurrentPageIndex * dgPengembalianPPH.PageSize)
        End If

        Dim lblTglInvoice As Label = CType(e.Item.FindControl("lblTglInvoice"), Label)
        If Not IsNothing(lblTglInvoice) Then
            If Not IsNothing(RowValue) Then
                lblTglInvoice.Text = RowValue.LogisticFee.LogisticDN.BillingDate
            Else
                lblTglInvoice.Text = ""
            End If
        End If

        objPFRH = CType(sessHelper.GetSession("PFRH"), LogisticPPHHeader)
        Dim pph As Decimal = CalcHelper.GetPercentage(objPFRH.PPHAmount, objPFRH.TotalAmount)

        TotalAmount = TotalAmount + Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "LogisticFee.Amount"))

        lblTotal.Text = "Rp. " & IIf(TotalAmount = 0, 0, TotalAmount.ToString("#,###"))
        'Dim pphAmount As Double = TotalAmount * 0.02
        Dim pphAmount As Decimal = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, TotalAmount)
        lblTotalPPH.Text = "Rp. " & IIf(pphAmount < 0, 0, pphAmount.ToString("#,###"))

        If CInt(Request.QueryString("edit")) = 0 Then
            dgPengembalianPPH.Columns(5).Visible = False
        End If

    End Sub

    Private Sub dgPengembalianPPH_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPengembalianPPH.ItemCommand
        Select Case (e.CommandName)
            Case "Add"
                AddCommand(e)
            Case "Delete"
                DeleteCommand(e)
        End Select
        If btnSimpan.Enabled = False Then
            btnSimpan.Enabled = True
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Try
            Dim objPFRHFac As LogisticPPHHeaderFacade = New LogisticPPHHeaderFacade(User)
            Dim objPFRDFac As LogisticPPHDetailFacade = New LogisticPPHDetailFacade(User)
            Dim objPFFac As LogisticFeeFacade = New LogisticFeeFacade(User)
            Dim arlPFRDExist As New ArrayList

            objPFRH = CType(sessHelper.GetSession("PFRH"), LogisticPPHHeader)
            Dim arlPFRD As ArrayList = CType(sessHelper.GetSession("ArlPFRD"), ArrayList)
            Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objPFRH.ReturnDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_2").ValueId)

            If Not IsNothing(objPFRH) Then
                objPFRH.BuktiPotongNumber = txtNoPPH.Text
                If objPFRHFac.Update(objPFRH) <> -1 Then
                    Dim objPFRDExisting As LogisticPPHHeader = New LogisticPPHHeaderFacade(User).Retrieve(objPFRH.ID)

                    For Each item As LogisticPPHDetail In objPFRDExisting.LogisticPPHDetails
                        Dim IsExist As Boolean = False
                        For Each objPFRD As LogisticPPHDetail In arlPFRD
                            If (objPFRD.ID > 0) AndAlso (objPFRD.ID = item.ID) Then
                                IsExist = True
                                Exit For
                            End If
                        Next
                        If Not IsExist Then
                            If item.RowStatus = CType(DBRowStatus.Active, Short) Then
                                objPFRDFac = New LogisticPPHDetailFacade(User)
                                item.LogisticPPHHeader = objPFRH
                                objPFRDFac.Delete(item)

                                objPFRHFac = New LogisticPPHHeaderFacade(User)
                                If objPFRH.TotalAmount > 0 Then
                                    objPFRH.TotalAmount = objPFRH.TotalAmount - item.LogisticFee.Amount
                                    'objPFRH.PPHAmount = objPFRH.TotalAmount * 0.02
                                    objPFRH.PPHAmount = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, objPFRH.TotalAmount)
                                Else
                                    objPFRH.TotalAmount = 0
                                    objPFRH.PPHAmount = 0
                                End If

                                objPFRHFac.Update(objPFRH)

                                Dim objPF As LogisticFee = item.LogisticFee
                                objPF.Status = EnumLogisticFeeStatus.LogisticFeeStatus.Baru

                                objPFFac.Update(objPF)
                            End If

                        End If
                    Next
                End If

            End If

            If arlPFRD.Count > 0 Then
                Dim totalAmount As Decimal = 0
                Dim totalPPh As Decimal = 0
                For Each objPFRD As LogisticPPHDetail In arlPFRD
                    If objPFRD.ID = 0 Then
                        objPFRDFac.Insert(objPFRD)
                        Dim objPF As LogisticFee = objPFRD.LogisticFee
                        objPF.Status = EnumLogisticFeeStatus.LogisticFeeStatus.Proses
                        objPFFac.Update(objPF)

                        'totalAmount = totalAmount + objPFRD.LogisticFee.Amount
                        'totalPPh = totalAmount * 0.02

                    Else
                        Dim objPF As LogisticFee = objPFRD.LogisticFee
                        objPF.Status = EnumLogisticFeeStatus.LogisticFeeStatus.Proses
                        objPFFac.Update(objPF)

                        'totalAmount = totalAmount + objPFRD.LogisticFee.Amount
                        'totalPPh = totalAmount * 0.02
                    End If

                    totalAmount = totalAmount + objPFRD.LogisticFee.Amount
                    totalPPh = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, totalAmount)
                Next
                objPFRHFac = New LogisticPPHHeaderFacade(User)
                objPFRH.TotalAmount = totalAmount
                objPFRH.PPHAmount = totalPPh
                objPFRH.RowStatus = CType(DBRowStatus.Active, Short)
                objPFRHFac.Update(objPFRH)
            Else
                objPFRHFac = New LogisticPPHHeaderFacade(User)
                objPFRH.TotalAmount = 0
                objPFRH.PPHAmount = 0
                objPFRH.RowStatus = CType(DBRowStatus.Deleted, Short)
                objPFRHFac.Update(objPFRH)
            End If

        Catch ex As Exception

        End Try
        MessageBox.Show("Simpan data berhasil")
        btnSimpan.Enabled = False
    End Sub

    Private Sub AddCommand(ByVal e As DataGridCommandEventArgs)
        Try
            If Not Page.IsValid Then
                Return
            End If
            Dim ddlDebitMemo As DropDownList = e.Item.FindControl("ddlDebitMemoF")
            objPFRH = CType(sessHelper.GetSession("PFRH"), LogisticPPHHeader)
            Dim arlPFRD As ArrayList = CType(sessHelper.GetSession("ArlPFRD"), ArrayList)

            If Validation(ddlDebitMemo.SelectedValue) Then
                Dim crits As New CriteriaComposite(New Criteria(GetType(LogisticFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crits.opAnd(New Criteria(GetType(LogisticFee), "ID", MatchType.Exact, CInt(ddlDebitMemo.SelectedValue)))

                Dim objPF As LogisticFee = CType(New LogisticFeeFacade(User).Retrieve(crits)(0), LogisticFee)

                For Each item As LogisticPPHDetail In arlPFRD
                    If (item.LogisticFee.LogisticDN.BillingDate.Month <> objPF.LogisticDN.BillingDate.Month) AndAlso (item.LogisticFee.LogisticDN.BillingDate.Year <> objPF.LogisticDN.BillingDate.Year) Then
                        MessageBox.Show("Periode Debit Memo tidak sama")
                        Exit Sub
                    End If
                Next

                Dim objPFRD As New LogisticPPHDetail
                objPFRD.LogisticPPHHeader = objPFRH
                objPFRD.LogisticFee = objPF
                arlPFRD.Add(objPFRD)
                sessHelper.SetSession("ArlPFRD", arlPFRD)
                BindDetailToGrid()
            Else
                Exit Sub
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub DeleteCommand(ByVal e As DataGridCommandEventArgs)
        Try
            objPFRH = CType(sessHelper.GetSession("PFRH"), LogisticPPHHeader)
            Dim arlPFRD As ArrayList = CType(sessHelper.GetSession("ArlPFRD"), ArrayList)

            Dim objPFRD As LogisticPPHDetail = CType(arlPFRD(e.Item.ItemIndex), LogisticPPHDetail)

            objPFRH.LogisticPPHDetails.Remove(objPFRD)
            arlPFRD.Remove(objPFRD)

            sessHelper.SetSession("ArlPFRD", arlPFRD)

            BindDetailToGrid()
        Catch ex As Exception

        End Try
    End Sub

    Private Function Validation(ByVal debitMemo As Integer) As Boolean

        Return True
    End Function

#Region "Custom"
    Private Sub BindDetailToGrid()
        objPFRH = CType(sessHelper.GetSession("PFRH"), LogisticPPHHeader)
        Dim arlPFRD As ArrayList = CType(sessHelper.GetSession("ArlPFRD"), ArrayList)

        lblTglPPH.Text = objPFRH.ReturnDate.ToString("dd MMM yyyy")

        If CInt(Request.QueryString("edit")) = 0 Then
            lnlNoPPH.Text = objPFRH.BuktiPotongNumber
            txtNoPPH.Text = objPFRH.BuktiPotongNumber
            lnlNoPPH.Visible = True
            txtNoPPH.Visible = False
        Else
            lnlNoPPH.Text = objPFRH.BuktiPotongNumber
            txtNoPPH.Text = objPFRH.BuktiPotongNumber
            lnlNoPPH.Visible = False
            txtNoPPH.Visible = True
        End If

        dgPengembalianPPH.Visible = True
        dgPengembalianPPH.DataSource = arlPFRD
        dgPengembalianPPH.DataBind()

    End Sub

    Sub FillDataDealer()
        objPFRH = CType(sessHelper.GetSession("PFRH"), LogisticPPHHeader)
        ltrDealerCode.Text = String.Format("{0} / {1}", objPFRH.Dealer.DealerCode.ToString(), objPFRH.Dealer.SearchTerm2)
        ltrDealerName.Text = objPFRH.Dealer.DealerName
    End Sub
#End Region



End Class

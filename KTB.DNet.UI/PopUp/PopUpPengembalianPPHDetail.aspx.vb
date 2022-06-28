Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports System.Globalization
Imports KTB.DNet.BusinessValidation.Helpers

Public Class PopUpPengembalianPPHDetail
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
    Private objPFRH As ParkingFeeReturnHeader = New ParkingFeeReturnHeader
    Private arlPFRD As ArrayList
    Dim sessHelper As New SessionHelper
    Private TotalAmount As Double = 0
    Private pphAmount As Double = 0
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
                objPFRH = New FinishUnit.ParkingFeeReturnHeaderFacade(User).Retrieve(CInt(Request.Params("id").ToString()))
                sessHelper.SetSession("PFRH", objPFRH)

                arlPFRD = CType(objPFRH.ParkingFeeReturnDetails, ArrayList)
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
        Dim RowValue As ParkingFeeReturnDetail = CType(e.Item.DataItem, ParkingFeeReturnDetail)
        If (e.Item.ItemType = ListItemType.Footer) OrElse (e.Item.ItemType = ListItemType.EditItem) Then
            If e.Item.ItemType = ListItemType.Footer Then
                ddlDebitMemo = CType(e.Item.FindControl("ddlDebitMemoF"), DropDownList)
            ElseIf e.Item.ItemType = ListItemType.EditItem Then
                ddlDebitMemo = CType(e.Item.FindControl("ddlDebitMemoE"), DropDownList)
            End If
            objPFRH = CType(sessHelper.GetSession("PFRH"), ParkingFeeReturnHeader)

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Dealer.ID", MatchType.Exact, objPFRH.Dealer.ID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Status", MatchType.Exact, CType(EnumParkingFeeStatus.ParkingFeeStatus.Baru, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "AssignmentNumber", MatchType.No, ""))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Dealer.CreditAccount", MatchType.Exact, objPFRH.Dealer.CreditAccount))
            If arlPFBind.Count > 0 Then
                Dim oDetail As ParkingFeeReturnDetail = CType(arlPFBind(0), ParkingFeeReturnDetail)
                If oDetail.ID > 0 Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Periode", MatchType.Exact, oDetail.ParkingFee.Periode))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Year", MatchType.Exact, oDetail.ParkingFee.Year))
                End If
            End If

            Dim arlPF As ArrayList = New FinishUnit.ParkingFeeFacade(User).Retrieve(criterias)

            Dim arlPFRD As ArrayList = CType(sessHelper.GetSession("ArlPFRD"), ArrayList)

            Dim blankItem As New listItem("Silahkan Pilih", 0)
            ddlDebitMemo.Items.Add(blankItem)
            For Each item As ParkingFee In arlPF
                Dim listItem As New listItem(item.DebitMemoNumber, item.ID)
                If arlPFRD.Count > 0 Then
                    Dim oDet As ParkingFeeReturnDetail = CType(arlPFRD(0), ParkingFeeReturnDetail)
                    If item.Periode = oDet.ParkingFee.Periode AndAlso item.Year = oDet.ParkingFee.Year Then
                        ddlDebitMemo.Items.Add(listItem)
                    End If
                Else
                    ddlDebitMemo.Items.Add(listItem)
                End If

                'For Each ipfrd As ParkingFeeReturnDetail In arlPFRD 'objPFRH.ParkingFeeReturnDetails
                '    If ipfrd.ParkingFee.ID = item.ID Then
                '        ddlDebitMemo.Items.Remove(listItem)
                '    End If
                'Next
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
                lblDealer.Text = RowValue.ParkingFee.Dealer.DealerCode
            Else
                lblDealer.Text = ""
            End If
        End If

        Dim lblPeriod As Label = CType(e.Item.FindControl("lblPeriod"), Label)
        If Not IsNothing(lblPeriod) Then
            If Not IsNothing(RowValue) Then
                lblPeriod.Text = EnumParkingFeePeriod.GetStringValue(CType(RowValue.ParkingFee.Periode, Integer))
            Else
                lblPeriod.Text = ""
            End If
        End If

        TotalAmount = TotalAmount + Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "ParkingFee.Amount"))
        lblTotal.Text = "Rp. " & IIf(TotalAmount = 0, 0, TotalAmount.ToString("#,###"))
        'Dim pphAmount As Double = TotalAmount / 10
        If Not IsNothing(RowValue) And btnSimpan.Enabled Then
            Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(DateSerial(RowValue.ParkingFee.Year, RowValue.ParkingFee.Periode, 1), objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_3").ValueId)
            pphAmount = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=TotalAmount)
        Else
            pphAmount = objPFRH.PPHAmount
        End If
        
        lblTotalPPH.Text = "Rp. " & IIf(pphAmount <= 0, 0, pphAmount.ToString("#,###"))

        If CInt(Request.QueryString("edit")) = 0 Then
            dgPengembalianPPH.Columns(7).Visible = False
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
            Dim objPFRHFac As ParkingFeeReturnHeaderFacade = New ParkingFeeReturnHeaderFacade(User)
            Dim objPFRDFac As ParkingFeeReturnDetailFacade = New ParkingFeeReturnDetailFacade(User)
            Dim objPFFac As ParkingFeeFacade = New ParkingFeeFacade(User)
            Dim arlPFRDExist As New ArrayList

            objPFRH = CType(sessHelper.GetSession("PFRH"), ParkingFeeReturnHeader)
            Dim arlPFRD As ArrayList = CType(sessHelper.GetSession("ArlPFRD"), ArrayList)

            If Not IsNothing(objPFRH) Then
                objPFRH.BuktiPotongNumber = txtNoPPH.Text
                If objPFRHFac.Update(objPFRH) <> -1 Then
                    Dim objPFRDExisting As ParkingFeeReturnHeader = New ParkingFeeReturnHeaderFacade(User).Retrieve(objPFRH.ID)

                    For Each item As ParkingFeeReturnDetail In objPFRDExisting.ParkingFeeReturnDetails
                        Dim IsExist As Boolean = False
                        For Each objPFRD As ParkingFeeReturnDetail In arlPFRD 'objPFRH.ParkingFeeReturnDetails
                            If (objPFRD.ID > 0) AndAlso (objPFRD.ID = item.ID) Then
                                IsExist = True
                                Exit For
                            End If
                        Next
                        If Not IsExist Then
                            If item.RowStatus = CType(DBRowStatus.Active, Short) Then
                                objPFRDFac = New ParkingFeeReturnDetailFacade(User)
                                item.ParkingFeeReturnHeader = objPFRH
                                objPFRDFac.Delete(item)

                                objPFRHFac = New ParkingFeeReturnHeaderFacade(User)
                                If objPFRH.TotalAmount > 0 Then
                                    objPFRH.TotalAmount = objPFRH.TotalAmount - item.ParkingFee.Amount
                                    Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(DateSerial(item.ParkingFee.Year, item.ParkingFee.Periode, 1), objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_3").ValueId)
                                    'objPFRH.PPHAmount = objPFRH.TotalAmount / 10
                                    objPFRH.PPHAmount = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=objPFRH.TotalAmount)
                                Else
                                    objPFRH.TotalAmount = 0
                                    objPFRH.PPHAmount = 0
                                End If

                                objPFRHFac.Update(objPFRH)

                                Dim objPF As ParkingFee = item.ParkingFee
                                objPF.Status = EnumParkingFeeStatus.ParkingFeeStatus.Baru
                                objPFFac.Update(objPF)
                            End If

                        End If
                    Next
                End If

            End If

            If arlPFRD.Count > 0 Then
                Dim totalAmount As Decimal = 0
                Dim totalPPh As Decimal = 0
                For Each objPFRD As ParkingFeeReturnDetail In arlPFRD 'objPFRH.ParkingFeeReturnDetails
                    If objPFRD.ID = 0 Then
                        objPFRDFac.Insert(objPFRD)
                        Dim objPF As ParkingFee = objPFRD.ParkingFee
                        objPF.Status = EnumParkingFeeStatus.ParkingFeeStatus.Proses
                        objPFFac.Update(objPF)

                        totalAmount = totalAmount + objPFRD.ParkingFee.Amount
                        Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(DateSerial(objPF.Year, objPF.Periode, 1), objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_3").ValueId)
                        'totalPPh = totalAmount / 10
                        totalPPh = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=totalAmount)


                        'objPFRHFac = New ParkingFeeReturnHeaderFacade(User)
                        'objPFRH.TotalAmount = objPFRH.TotalAmount + objPFRD.ParkingFee.Amount
                        'If objPFRH.TotalAmount > 0 Then
                        '    objPFRH.PPHAmount = objPFRH.TotalAmount / 10
                        'Else
                        '    objPFRH.PPHAmount = 0
                        'End If

                        'objPFRHFac.Update(objPFRH)
                    Else
                        Dim objPF As ParkingFee = objPFRD.ParkingFee
                        objPF.Status = EnumParkingFeeStatus.ParkingFeeStatus.Proses
                        objPFFac.Update(objPF)

                        totalAmount = totalAmount + objPFRD.ParkingFee.Amount
                        Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(DateSerial(objPF.Year, objPF.Periode, 1), objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_3").ValueId)
                        'totalPPh = totalAmount / 10
                        totalPPh = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=totalAmount)

                        'objPFRHFac = New ParkingFeeReturnHeaderFacade(User)
                        'objPFRH.TotalAmount = objPFRH.TotalAmount + objPFRD.ParkingFee.Amount
                        'If objPFRH.TotalAmount > 0 Then
                        '    objPFRH.PPHAmount = objPFRH.TotalAmount / 10
                        'Else
                        '    objPFRH.PPHAmount = 0
                        'End If

                        'objPFRHFac.Update(objPFRH)
                    End If
                Next
                objPFRHFac = New ParkingFeeReturnHeaderFacade(User)
                objPFRH.TotalAmount = totalAmount
                objPFRH.PPHAmount = totalPPh
                objPFRH.RowStatus = CType(DBRowStatus.Active, Short)
                objPFRHFac.Update(objPFRH)
            Else
                objPFRHFac = New ParkingFeeReturnHeaderFacade(User)
                objPFRH.TotalAmount = 0
                objPFRH.PPHAmount = 0
                objPFRH.RowStatus = CType(DBRowStatus.Deleted, Short)
                objPFRHFac.Update(objPFRH)
            End If

        Catch ex As Exception
            'MessageBox.Show("Simpan data gagal")
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
            objPFRH = CType(sessHelper.GetSession("PFRH"), ParkingFeeReturnHeader)
            Dim arlPFRD As ArrayList = CType(sessHelper.GetSession("ArlPFRD"), ArrayList)
            
            If Validation(ddlDebitMemo.SelectedValue) Then
                Dim crits As New CriteriaComposite(New Criteria(GetType(ParkingFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crits.opAnd(New Criteria(GetType(ParkingFee), "ID", MatchType.Exact, CInt(ddlDebitMemo.SelectedValue)))

                Dim objPF As ParkingFee = CType(New ParkingFeeFacade(User).Retrieve(crits)(0), ParkingFee)

                For Each item As ParkingFeeReturnDetail In arlPFRD
                    If (item.ParkingFee.Periode <> objPF.Periode) AndAlso (item.ParkingFee.Year <> objPF.Year) Then
                        MessageBox.Show("Periode Debit Memo tidak sama")
                        Exit Sub
                    End If
                Next

                Dim objPFRD As New ParkingFeeReturnDetail
                objPFRD.ParkingFeeReturnHeader = objPFRH
                objPFRD.ParkingFee = objPF
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
            objPFRH = CType(sessHelper.GetSession("PFRH"), ParkingFeeReturnHeader)
            Dim arlPFRD As ArrayList = CType(sessHelper.GetSession("ArlPFRD"), ArrayList)

            Dim objPFRD As ParkingFeeReturnDetail = CType(arlPFRD(e.Item.ItemIndex), ParkingFeeReturnDetail) 'objPFRH.ParkingFeeReturnDetails(e.Item.ItemIndex)

            'Dim objPFRDFacade As New ParkingFeeReturnDetailFacade(User)
            'objPFRD.ParkingFeeReturnHeader = objPFRH
            'objPFRDFacade.Delete(objPFRD)

            'Dim objPFFac As ParkingFeeFacade = New ParkingFeeFacade(User)
            'Dim objPF As ParkingFee = objPFRD.ParkingFee
            'objPF.Status = EnumParkingFeeStatus.ParkingFeeStatus.Baru
            'objPFFac.Update(objPF)

            objPFRH.ParkingFeeReturnDetails.Remove(objPFRD)
            arlPFRD.Remove(objPFRD)

            sessHelper.SetSession("ArlPFRD", arlPFRD)

            'Dim objPFRHFac As ParkingFeeReturnHeaderFacade = New ParkingFeeReturnHeaderFacade(User)
            'objPFRHFac = New ParkingFeeReturnHeaderFacade(User)

            'If objPFRH.TotalAmount > 0 Then
            '    objPFRH.TotalAmount = objPFRH.TotalAmount - objPFRD.ParkingFee.Amount
            '    objPFRH.PPHAmount = objPFRH.TotalAmount / 10
            'Else
            '    objPFRH.TotalAmount = 0
            '    objPFRH.PPHAmount = 0
            'End If

            'objPFRHFac.Update(objPFRH)


            BindDetailToGrid()
        Catch ex As Exception

        End Try
    End Sub

    Private Function Validation(ByVal debitMemo As Integer) As Boolean

        Return True
    End Function

#Region "Custom"
    Private Sub BindDetailToGrid()
        objPFRH = CType(sessHelper.GetSession("PFRH"), ParkingFeeReturnHeader)
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
        dgPengembalianPPH.DataSource = arlPFRD ' objPFRH.ParkingFeeReturnDetails
        dgPengembalianPPH.DataBind()

    End Sub

    Sub FillDataDealer()
        objPFRH = CType(sessHelper.GetSession("PFRH"), ParkingFeeReturnHeader)
        ltrDealerCode.Text = String.Format("{0} / {1}", objPFRH.Dealer.DealerCode.ToString(), objPFRH.Dealer.SearchTerm2)
        ltrDealerName.Text = objPFRH.Dealer.DealerName
    End Sub
#End Region



End Class

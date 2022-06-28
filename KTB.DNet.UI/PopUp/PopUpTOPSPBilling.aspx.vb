Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement

Public Class PopUpTOPSPBilling
    Inherits System.Web.UI.Page
    Dim _sessHelper As New SessionHelper


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState.Add("SortColBilling", "BillingNumber")
            ViewState.Add("SortDirBilling", Sort.SortDirection.DESC)
            ClearData()
        End If
        lblCreditAccount.Text = CType(_sessHelper.GetSession("Dealer"), Dealer).CreditAccount
        lblGroup.Text = CType(_sessHelper.GetSession("Dealer"), Dealer).DealerGroup.DealerGroupCode

        Dim lblSearchDealer As Label = CType(Page.FindControl("lblSearchDealer"), Label)
        lblSearchDealer.Attributes.Add("onclick", "ShowPPDealerSelection();")
    End Sub

    Private Sub dtgBillingNumber_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgBillingNumber.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim arr As ArrayList

        If Not e.Item.DataItem Is Nothing Then

            arr = New ArrayList

            e.Item.DataItem.GetType().ToString()
            Dim RowValue As SparePartBilling = CType(e.Item.DataItem, SparePartBilling)

            Dim criDueDate As New CriteriaComposite(New Criteria(GetType(TOPSPDueDate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim criDeposit As New CriteriaComposite(New Criteria(GetType(TOPSPDeposit), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgBillingNumber.CurrentPageIndex * dtgBillingNumber.PageSize)

                Dim lblTanggalJatuhTempo As Label = CType(e.Item.FindControl("lblTanggalJatuhTempo"), Label)
                criDueDate.opAnd(New Criteria(GetType(TOPSPDueDate), "SparePartBilling.ID", MatchType.Exact, RowValue.ID))
                arr = New TOPSPDueDateFacade(User).Retrieve(criDueDate)
                lblTanggalJatuhTempo.Text = CType(arr.Item(0), TOPSPDueDate).DueDate


                Dim lblAmountBillingTax As Label = CType(e.Item.FindControl("lblAmountBillingTax"), Label)
                lblAmountBillingTax.Text = (RowValue.TotalAmount + RowValue.Tax).ToString("N0")


                Dim lblAmountC2 As Label = CType(e.Item.FindControl("lblAmountC2"), Label)
                criDeposit.opAnd(New Criteria(GetType(TOPSPDeposit), "SparePartBilling.ID", MatchType.Exact, RowValue.ID))
                arr = New TOPSPDepositFacade(User).Retrieve(criDeposit)
                lblAmountC2.Text = CType(arr.Item(0), TOPSPDeposit).AmountC2.ToString("N0")


                Dim lblAmountTotal As Label = CType(e.Item.FindControl("lblAmountTotal"), Label)
                lblAmountTotal.Text = (CType(arr.Item(0), TOPSPDeposit).AmountC2 + RowValue.TotalAmount + RowValue.Tax).ToString("N0")



            End If
        End If
    End Sub

    Protected Sub dtgBillingNumber_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgBillingNumber.PageIndexChanged
        dtgBillingNumber.CurrentPageIndex = e.NewPageIndex
        bindSearch(e.NewPageIndex)
    End Sub

    Private Sub dtgBillingNumber_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgBillingNumber.SortCommand
        If e.SortExpression = ViewState("SortColBilling") Then
            If ViewState("SortDirBilling") = Sort.SortDirection.ASC Then
                ViewState.Add("SortDirBilling", Sort.SortDirection.DESC)
            Else
                ViewState.Add("SortDirBilling", Sort.SortDirection.ASC)
            End If
        End If
        ViewState.Add("SortColBilling", e.SortExpression)
        bindSearch(dtgBillingNumber.CurrentPageIndex)
    End Sub


    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If IsDate(icTanggalBillingFrom.Value) = False Then
            MessageBox.Show("Format Tanggal Tidak Valid")
            Return
        End If

        If IsDate(icTanggalBillingUntil.Value) = False Then
            MessageBox.Show("Format Tanggal Tidak Valid")
            Return
        End If

        If IsDate(icTanggalJatuhTempoFrom.Value) = False Then
            MessageBox.Show("Format Tanggal Tidak Valid")
            Return
        End If

        If IsDate(icTanggalJatuhTempoUntil.Value) = False Then
            MessageBox.Show("Format Tanggal Tidak Valid")
            Return
        End If
        dtgBillingNumber.CurrentPageIndex = 0
        bindSearch(dtgBillingNumber.CurrentPageIndex)
    End Sub


    Private Sub bindSearch(ByVal pageIndex As Integer)
        Dim TotalRow As Integer = 0

        Dim strDueDate As String = "(select sparepartbillingID from TOPSPDueDate where rowstatus= 0)"
        Dim strDeposit As String = "(select sparepartbillingID from TOPSPDeposit where rowstatus= 0)"

        Dim cri As New CriteriaComposite(New Criteria(GetType(SparePartBilling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cri.opAnd(New Criteria(GetType(SparePartBilling), "Dealer.CreditAccount", MatchType.Exact, lblCreditAccount.Text))
        cri.opAnd(New Criteria(GetType(SparePartBilling), "ID", MatchType.InSet, strDueDate))
        cri.opAnd(New Criteria(GetType(SparePartBilling), "ID", MatchType.InSet, strDeposit))
        'cri.opAnd(New Criteria(GetType(SparePartBilling), "ID", MatchType.NotInSet, str))

        Dim arrTOPSP As New ArrayList
        Dim objSPBilling As SparePartBilling
        Dim strId As String = "("
        Dim strBill As String = ""

        If _sessHelper.GetSession("BillingSel") IsNot Nothing Then

            arrTOPSP = _sessHelper.GetSession("BillingSel")

            For Each rowspbill As TOPSPTransferPaymentDetail In arrTOPSP
                If rowspbill.RowStatus = CType(DBRowStatus.Active, Short) Then
                    strBill = strBill + rowspbill.SparePartBilling.BillingNumber + "','"
                End If
            Next

            If strBill.Length > 5 Then
                strBill = "'" + strBill + "'"
                icTanggalJatuhTempoFrom.Value = (New TOPSPDueDateFacade(User).Retrieve(strBill, "BillingMulti").DueDate)
                icTanggalJatuhTempoUntil.Value = icTanggalJatuhTempoFrom.Value
                cri.opAnd(New Criteria(GetType(SparePartBilling), "BillingNumber", MatchType.NotInSet, strBill))
            End If
        End If


        CreateCriteria(cri)

        Dim arr As New ArrayList
        Dim sortColl As New SortCollection
        sortColl.Add(New Sort(GetType(SparePartBilling), "BillingNumber", Sort.SortDirection.DESC))

        'Dim sortColl As SortCollection = New SortCollection
        'If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
        '    sortColl.Add(New Sort(GetType(SparePartBilling), sortColumn, SortDirection))
        'Else
        '    sortColl = Nothing
        'End If

        'ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection
        'ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection
        'arr = New SparePartBillingFacade(User).RetrieveList(cri, pageIndex, dtgBillingNumber.PageSize, 0, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        'arr = New SparePartBillingFacade(User).RetrieveList(cri, sortColl)
        arr = New SparePartBillingFacade(User).RetrieveActiveList(cri,
                                                            pageIndex + 1,
                                                            dtgBillingNumber.PageSize,
                                                            TotalRow,
                                                                CType(ViewState("SortColBilling"), String),
                                                                CType(ViewState("SortDirBilling"), Sort.SortDirection),
                                                                icTanggalJatuhTempoFrom.Value,
                                                                icTanggalJatuhTempoUntil.Value,
                                                                lblCreditAccount.Text)
        If arr.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If

        dtgBillingNumber.DataSource = arr
        dtgBillingNumber.VirtualItemCount = TotalRow
        dtgBillingNumber.DataBind()
    End Sub

    Private Sub CreateCriteria(ByRef cri As CriteriaComposite)



        'If Not String.IsNullOrEmpty(CType(Request.QueryString("hrn"), String)) Then
        '    Dim str As String = "select sparepartbillingid from topsptransferpayment()"

        '    cri.opAnd(New Criteria(GetType(SparePartBilling), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))
        'End If

        If Not String.IsNullOrEmpty(txtDealerCode.Text) Then
            Dim str As String = "('" + txtDealerCode.Text + "')"
            cri.opAnd(New Criteria(GetType(SparePartBilling), "Dealer.DealerCode", MatchType.InSet, str.Replace(";", "','")))
        End If

        If Not String.IsNullOrEmpty(TxtBillingNumber.Text) Then
            cri.opAnd(New Criteria(GetType(SparePartBilling), "BillingNumber", MatchType.Partial, TxtBillingNumber.Text))
        End If

        If chxTanggalBilling.Checked = True Then
            cri.opAnd(New Criteria(GetType(SparePartBilling), "BillingDate", MatchType.GreaterOrEqual, icTanggalBillingFrom.Value))
            cri.opAnd(New Criteria(GetType(SparePartBilling), "BillingDate", MatchType.LesserOrEqual, icTanggalBillingUntil.Value))
        End If

        If chxTanggalJatuhTempo.Checked = True Then
            Dim strQuery As String
            strQuery = "(select sparepartbillingID from topspDueDate (nolock) "
            strQuery = strQuery + " where rowstatus = 0 and duedate between '" & icTanggalJatuhTempoFrom.Value.ToString("yyyyMMdd") & "' and '" & icTanggalJatuhTempoUntil.Value.ToString("yyyyMMdd") & "')"

            cri.opAnd(New Criteria(GetType(SparePartBilling), "ID", MatchType.InSet, strQuery))

        End If

    End Sub

    Private Sub ClearData()
        Me.TxtBillingNumber.Text = String.Empty
        Me.txtDealerCode.Text = String.Empty
        Me.chxTanggalBilling.Checked = False
        Me.chxTanggalJatuhTempo.Checked = True
        Me.icTanggalJatuhTempoFrom.Value = Now.Date
        Me.icTanggalJatuhTempoUntil.Value = DateAdd(DateInterval.Day, 60, Now.Date)
    End Sub




    
End Class
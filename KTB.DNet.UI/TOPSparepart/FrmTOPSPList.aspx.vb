#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.Sparepart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text
Imports System.Linq
Imports OfficeOpenXml

#End Region

Public Class FrmTOPSPList
    Inherits System.Web.UI.Page

#Region "Variables"
    Private _sessHelper As New SessionHelper()
    Private _sessData As String = "FrmTOPSPList._sessData"
    Private _sessBroadCast As String = "FrmTOPSPList._sessBroadCast"
    Private _vsCritSearch As String = "FrmTOPSPList._vstCritSearch"
    Private _sessCri As String = "FrmTOPSPList._sessCriteria"
    Private objDealer As Dealer = New Dealer
    Private totalAmount As Double = 0
    Private IsAuthorizedUbah As Boolean
    Private IsAuthorizedDownload As Boolean

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserPrivilege()
        checkPriv()

        If Not IsPostBack Then
            ViewState.Add("SortColTOPSP", "RegNumber")
            ViewState.Add("SortDirTOPSP", Sort.SortDirection.DESC)
            BindDDL()

            If CType(_sessHelper.GetSession(_sessCri), TOPSPTransferPayment) IsNot Nothing Then
                FillCOntrol()
            Else
                chkPlanTransferDate.Checked = True
            End If

            BindGrid()
        End If

        Dim objdealer As Dealer = CType(_sessHelper.GetSession("Dealer"), Dealer)
        lblSearchDealerPembayaran.Attributes.Add("onclick", "ShowPPDealerSelection();")
        lblSearchDealerBilling.Attributes.Add("onclick", "ShowPPBillingSelection();")
        lblPopCreditAccount.Attributes.Add("onclick", "ShowPPCreditAccountSelection();")

    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        If validateInput() Then
            _sessHelper.SetSession(_vsCritSearch, Nothing)
            BindGrid(dtgMain.CurrentPageIndex)
        End If
    End Sub

    Private Sub FillCOntrol()
        Dim objTOPSP As New TOPSPTransferPayment

        objTOPSP = CType(_sessHelper.GetSession(_sessCri), TOPSPTransferPayment)
        With objTOPSP
            calDueDateEnd.Value = .CalDueDateEnd
            calDueDateStart.Value = .CalDueDateStart
            calPlanTransferDateEnd.Value = .calPlanTransferDateEnd
            calPlanTransferDateStart.Value = .calPlanTransferDateStart
            txtCreditAccount.Text = objTOPSP.CreditAccount
            lblCreditAccount.Text = objTOPSP.CreditAccount
            txtRegDipercepat.Text = .NoRegDipercepat
            txtRegPemercepat.Text = .NoRegPemercepat
            txtCreditAccount.Text = .CreditAccount
            txtNoKodeBilling.Text = .NoKodeBilling
            txtNoBilling.Text = .NoBilling
            txtKodeDealer.Text = .KodeDealer
            txtNoRegPermbayaran.Text = .RegNumber
            chkDueDate.Checked = .TglBilling
            chkPlanTransferDate.Checked = .TglTransfer
            ddlStatus.SelectedIndex = .DdlStatusIndex

        End With

    End Sub

    Private Sub checkPriv()
        objDealer = _sessHelper.GetSession("DEALER")
        If objDealer.Title = 1 Then '1=KTB
            ctrVisible(True)
        Else
            ctrVisible(False)
        End If
    End Sub

    Private Sub UserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.TOPSP_Daftar_Transfer_Payment_Lihat_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=TOPSparePart")
        End If

        IsAuthorizedUbah = SecurityProvider.Authorize(Context.User, SR.TOPSP_Daftar_Transfer_Payment_Ubah_Privilege)
        'IsAuthorizedDownload = SecurityProvider.Authorize(Context.User, SR.TOPSP_Daftar_Transfer_Payment_Ubah_Privilege)

    End Sub

    Private Sub BindGrid(Optional ByVal pageIndex As Integer = 0)
        Dim aTPs As ArrayList = GetData(pageIndex)
        Me._sessHelper.SetSession(_sessData, aTPs)
        'If aTPs.Count > 0 Then
        '    dtgMain.DataSource = aTPs
        '    dtgMain.VirtualItemCount = aTPs.Count()
        '    dtgMain.Visible = True
        'Else
        '    dtgMain.DataSource = aTPs
        '    dtgMain.VirtualItemCount = 0
        '    dtgMain.CurrentPageIndex = 0
        '    dtgMain.Visible = True
        '    If IsPostBack Then
        '        MessageBox.Show(SR.DataNotFound("Daftar Pembayaran Transfer"))
        '    End If
        'End If
        'dtgMain.DataBind()
        lblTAmount.Text = CType(totalAmount, Double).ToString("N0")
    End Sub

    Private Sub GetPayment(ByVal hdnID As HiddenField)


        Dim critSend As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critSend.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.ID", MatchType.Exact, hdnID.Value))
        Dim setSes As ArrayList = New TOPSPTransferPaymentDetailFacade(User).Retrieve(critSend)
        _sessHelper.SetSession(_sessBroadCast, setSes)

    End Sub

    Private Function GetData(ByVal pageindex As Integer, Optional download As Boolean = False) As ArrayList
        Dim oTPFac As New TOPSPTransferPaymentFacade(User)
        Dim aTPs As New ArrayList
        Dim cTP As CriteriaComposite
        Dim sTP As New SortCollection()
        Dim objTOPSP As New TOPSPTransferPayment
        Dim totalrow As Integer = 0
        sTP.Add(New Sort(GetType(TOPSPTransferPayment), "CreatedTime", Sort.SortDirection.DESC))

        '    'Credit Acc

        With objTOPSP
            .CreditAccount = ""
            .NoRegDipercepat = ""
            .NoRegPemercepat = ""
            .NoKodeBilling = ""
            .NoBilling = ""
            .KodeDealer = ""
            .RegNumber = ""
            .TglBilling = False
            .TglTransfer = False
            .Status = 0
        End With

        cTP = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If objDealer.Title = 1 Then
            If txtCreditAccount.Text.Trim <> String.Empty Then
                Dim caList As String = txtCreditAccount.Text.Trim.Replace(";", "','")
                objTOPSP.CreditAccount = caList
                cTP.opAnd(New Criteria(GetType(TOPSPTransferPayment), "CreditAccount", MatchType.InSet, "('" & caList & "')"))
            End If
        Else
            objTOPSP.CreditAccount = lblCreditAccount.Text
            cTP.opAnd(New Criteria(GetType(TOPSPTransferPayment), "CreditAccount", MatchType.Exact, lblCreditAccount.Text.Trim))
        End If

        'Kode Dealer
        If txtKodeDealer.Text.Trim <> String.Empty Then
            objTOPSP.KodeDealer = txtKodeDealer.Text
            cTP.opAnd(New Criteria(GetType(TOPSPTransferPayment), "Dealer.DealerCode", MatchType.InSet, "('" + txtKodeDealer.Text.Trim.Replace(";", "','") + "')"))
        End If

        'Kode Dealer Billing
        If txtNoKodeBilling.Text.Trim <> String.Empty Then
            objTOPSP.NoKodeBilling = txtNoKodeBilling.Text
            Dim critCodeBill As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critCodeBill.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "SparePartBilling.Dealer.DealerCode", MatchType.InSet, "('" + txtNoKodeBilling.Text.Trim.Replace(";", "','") + "')"))
            Dim strID As String = ""
            Dim arlDetail As ArrayList = New TOPSPTransferPaymentDetailFacade(User).Retrieve(critCodeBill)
            Dim arlID = From model As TOPSPTransferPaymentDetail In arlDetail
                        Group By model.TOPSPTransferPayment.ID Into Group
                        Select ID
            For Each det As Integer In arlID
                If strID.Length = 0 Then
                    strID = det
                Else
                    strID = strID & "','" & det
                End If
            Next
            cTP.opAnd(New Criteria(GetType(TOPSPTransferPayment), "ID", MatchType.InSet, "('" + strID + "')"))
        End If

        '    'Tgl Transfer
        objTOPSP.calPlanTransferDateStart = calPlanTransferDateStart.Value
        objTOPSP.calPlanTransferDateEnd = calPlanTransferDateEnd.Value
        objTOPSP.TglTransfer = chkPlanTransferDate.Checked
        If chkPlanTransferDate.Checked Then
            cTP.opAnd(New Criteria(GetType(TOPSPTransferPayment), "TransferPlanDate", MatchType.GreaterOrEqual, calPlanTransferDateStart.Value))
            cTP.opAnd(New Criteria(GetType(TOPSPTransferPayment), "TransferPlanDate", MatchType.LesserOrEqual, calPlanTransferDateEnd.Value))
        End If

        '    'Tgl Kirim
        objTOPSP.CalDueDateEnd = calDueDateEnd.Value
        objTOPSP.CalDueDateStart = calDueDateStart.Value
        objTOPSP.TglBilling = chkDueDate.Checked
        If chkDueDate.Checked Then
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "SparePartBilling.BillingDate", MatchType.GreaterOrEqual, calDueDateStart.Value))
            crit.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "SparePartBilling.BillingDate", MatchType.LesserOrEqual, calDueDateEnd.Value))
            Dim objPay As ArrayList = New TOPSPTransferPaymentDetailFacade(User).Retrieve(crit)
            Dim inSetID As String = "0"
            If objPay.Count > 0 Then
                Dim ids = From model As TOPSPTransferPaymentDetail In objPay
                          Group By model.TOPSPTransferPayment.ID Into Group
                          Select ID
                For Each tpd As Integer In ids
                    If inSetID = "0" Then
                        inSetID = tpd
                    Else
                        inSetID = inSetID & "," & tpd
                    End If
                Next
            End If
            cTP.opAnd(New Criteria(GetType(TOPSPTransferPayment), "ID", MatchType.InSet, "(" & inSetID & ")"))
        End If

        'No Reg Pembayaran
        If txtNoRegPermbayaran.Text.Trim <> String.Empty Then
            objTOPSP.RegNumber = txtNoRegPermbayaran.Text
            cTP.opAnd(New Criteria(GetType(TOPSPTransferPayment), "RegNumber", MatchType.Exact, txtNoRegPermbayaran.Text.Trim))
        End If

        'No Reg Pemercepat
        If txtRegPemercepat.Text.Trim <> String.Empty Then
            Dim objPay As TOPSPTransferPayment
            Try
                objTOPSP.NoRegPemercepat = txtRegPemercepat.Text
                objPay = New TOPSPTransferPaymentFacade(User).Retrieve(txtRegPemercepat.Text.Trim)
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(TOPSPTransferPayment), "ID", MatchType.Exact, objPay.TOPSPTransferPaymentIDReff))

                objPay = New TOPSPTransferPaymentFacade(User).Retrieve(crit)(0)
            Catch ex As Exception
                objPay = New TOPSPTransferPayment
            End Try
            cTP.opAnd(New Criteria(GetType(TOPSPTransferPayment), "ID", MatchType.Exact, objPay.ID))
        End If

        'No Reg Dipercepat
        If txtRegDipercepat.Text.Trim <> String.Empty Then
            objTOPSP.NoRegDipercepat = txtRegDipercepat.Text
            Dim objPay As TOPSPTransferPayment = New TOPSPTransferPaymentFacade(User).Retrieve(txtRegDipercepat.Text.Trim)
            If IsNothing(objPay) Then
                objPay = New TOPSPTransferPayment
            End If
            cTP.opAnd(New Criteria(GetType(TOPSPTransferPayment), "TOPSPTransferPaymentIDReff", MatchType.Exact, objPay.ID))
            cTP.opAnd(New Criteria(GetType(TOPSPTransferPayment), "IsAccelerated", MatchType.Exact, "1"))
        End If

        'No Billing
        If txtNoBilling.Text.Trim <> String.Empty Then
            objTOPSP.NoBilling = txtNoBilling.Text
            Dim criter As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criter.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "SparePartBilling.BillingNumber", MatchType.Exact, txtNoBilling.Text.Trim))
            Dim q = From model As TOPSPTransferPaymentDetail In New TOPSPTransferPaymentDetailFacade(User).Retrieve(criter)
                    Group By model.TOPSPTransferPayment.ID Into Group
                    Select ID
            Dim d As String = String.Empty
            For Each a As Integer In q
                If d.Length = 0 Then
                    d = a
                Else
                    d = d & "','" & a
                End If
            Next
            cTP.opAnd(New Criteria(GetType(TOPSPTransferPayment), "ID", MatchType.InSet, "('" & d & "')"))
        End If

        'Status
        objTOPSP.DdlStatusIndex = ddlStatus.SelectedIndex
        If ddlStatus.SelectedIndex > 0 Then
            cTP.opAnd(New Criteria(GetType(TOPSPTransferPayment), "Status", MatchType.Exact, CType(ddlStatus.SelectedValue, Short)))
        End If

        'Amount
        If txtAmount.Text <> String.Empty Then
            Dim dt As DataTable = GetAmount()

            Dim d As String = String.Empty
            For Each a As DataRow In dt.Rows
                If CDec(a.Item(1)) = CDec(txtAmount.Text) Then
                    If d.Length = 0 Then
                        d = a.Item(0)
                    Else
                        d = d & "','" & a.Item(0)
                    End If
                End If
            Next
            cTP.opAnd(New Criteria(GetType(TOPSPTransferPayment), "ID", MatchType.InSet, "('" & d & "')"))
        End If
        _sessHelper.SetSession(_vsCritSearch, cTP)

        If Not download Then
            aTPs = oTPFac.RetrieveActiveList(cTP,
                                   pageindex + 1,
                                   dtgMain.PageSize,
                                   totalrow,
                                   CType(ViewState("SortColTOPSP"), String),
                                CType(ViewState("SortDirTOPSP"), Sort.SortDirection))
        Else
            aTPs = oTPFac.Retrieve(cTP)
        End If

        If aTPs.Count = 0 Then
            MessageBox.Show(SR.DataNotFound("Daftar Pembayaran Transfer"))
            Return aTPs
        End If

        dtgMain.DataSource = aTPs
        dtgMain.VirtualItemCount = totalrow
        dtgMain.DataBind()
        Me._sessHelper.SetSession(_sessCri, objTOPSP)

        '13, 14, 15
        If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            dtgMain.Columns(14).Visible = False
            dtgMain.Columns(15).Visible = False
        End If

        'If Not download Then
        '    dtgMain.DataSource = aTPs
        '    dtgMain.VirtualItemCount = totalrow
        '    dtgMain.DataBind()
        'End If

        Return aTPs
    End Function

    Private Function GetAmount() As DataTable
        Dim oTPFac As New TOPSPTransferPaymentFacade(User)
        Dim sqlStr As String = "select topsp.id as headerid, sum(Amount)Amount  "
        sqlStr += "from TOPSPTransferPayment topsp "
        sqlStr += "left join TOPSPTransferPaymentDetail toptt on topsp.id = toptt.TOPSPTransferPaymentID "
        sqlStr += "group by topsp.id , DealerID, CreditAccount, RegNumber, DueDate, topsp.TransferAmount, topsp.Status order by DealerID "

        Dim oDS As DataSet = oTPFac.RetrieveQuery(sqlStr)

        If oDS.Tables.Count > 0 Then
            Return oDS.Tables(0)
        Else
            Return Nothing
        End If
    End Function

    'Private Function GetData() As ArrayList
    '    Dim oTPFac As New TOPSPTransferPaymentDetailFacade(User)
    '    Dim aTPs As ArrayList
    '    Dim ProductCategoryID As Integer = 0
    '    Dim cTP As New CriteriaComposite(New Criteria(GetType(TOPSPTransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    Dim sTP As New SortCollection()
    '    sTP.Add(New Sort(GetType(TOPSPTransferPaymentDetail), "CreatedTime", Sort.SortDirection.DESC))

    '    'Credit Acc
    '    If objDealer.Title = 1 Then
    '        If txtCreditAccount.Text.Trim <> String.Empty Then
    '            Dim caList As String = txtCreditAccount.Text.Trim.Replace(";", ",")
    '            cTP.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.CreditAccount", MatchType.InSet, caList))
    '        End If
    '    Else
    '        cTP.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.CreditAccount", MatchType.Exact, lblCreditAccount.Text.Trim))
    '    End If

    '    'Kode Dealer
    '    If txtKodeDealer.Text.Trim <> String.Empty Then
    '        cTP.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.Dealer.ID", MatchType.InSet, txtKodeDealer.Text.Trim.Replace(";", ",")))
    '    End If

    '    'Kode Dealer Billing
    '    If txtNoKodeBilling.Text.Trim <> String.Empty Then
    '        cTP.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "SparePartBilling.Dealer.DealerCode", MatchType.InSet, txtNoKodeBilling.Text.Trim.Replace(";", ",")))
    '    End If

    '    'Tgl Transfer
    '    If chkPlanTransferDate.Checked Then
    '        cTP.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.TransferPlanDate", MatchType.GreaterOrEqual, calPlanTransferDateStart.Value))
    '        cTP.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.TransferPlanDate", MatchType.LesserOrEqual, calPlanTransferDateEnd.Value))
    '    End If

    '    'Tgl Kirim
    '    If chkDueDate.Checked Then
    '        cTP.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.DueDate", MatchType.GreaterOrEqual, calDueDateStart.Value))
    '        cTP.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.DueDate", MatchType.LesserOrEqual, calDueDateEnd.Value))
    '    End If

    '    'No Reg Pembayaran
    '    If txtNoRegPermbayaran.Text.Trim <> String.Empty Then
    '        cTP.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.RegNumber", MatchType.Exact, txtNoRegPermbayaran.Text.Trim))
    '    End If

    '    'No Reg Pemercepat
    '    If txtRegPemercepat.Text.Trim <> String.Empty Then
    '        cTP.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.ID", MatchType.Exact, txtRegPemercepat.Text.Trim))
    '        cTP.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.IsAccelerated", MatchType.Exact, "1"))
    '    End If

    '    'No Reg Dipercepat
    '    If txtRegDipercepat.Text.Trim <> String.Empty Then
    '        cTP.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.TOPSPTransferPaymentIDReff", MatchType.Exact, txtRegDipercepat.Text.Trim))
    '        cTP.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.IsAccelerated", MatchType.Exact, "1"))
    '    End If

    '    'No Billing
    '    If txtNoBilling.Text.Trim <> String.Empty Then
    '        cTP.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "SparePartBilling.BillingNumber", MatchType.Exact, txtNoBilling.Text.Trim))
    '    End If

    '    'Status
    '    If ddlStatus.SelectedIndex > 0 Then
    '        cTP.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.Status", MatchType.Exact, CType(ddlStatus.SelectedValue, Short)))
    '    End If

    '    aTPs = oTPFac.Retrieve(cTP, sTP)

    '    Return aTPs
    'End Function

    Private Sub ctrVisible(p1 As Boolean)
        If p1 Then
            divMKS.Visible = True
        Else
            divDealer.Visible = True
            lblCreditAccount.Text = objDealer.CreditAccount
        End If
    End Sub

    Private Sub BindDDL()
        With ddlStatus.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", -1))
            .Add(New ListItem("Baru", EnumStatusTOPSPTransferPayment.TOPSPStatus.Baru))
            .Add(New ListItem("Batal", EnumStatusTOPSPTransferPayment.TOPSPStatus.Batal))
            .Add(New ListItem("Konfirmasi", EnumStatusTOPSPTransferPayment.TOPSPStatus.Konfirmasi))
            .Add(New ListItem("Batal_Konfirmasi", EnumStatusTOPSPTransferPayment.TOPSPStatus.Batal_Konfirmasi))
            .Add(New ListItem("Validasi", EnumStatusTOPSPTransferPayment.TOPSPStatus.Validasi))
            .Add(New ListItem("Selesai", EnumStatusTOPSPTransferPayment.TOPSPStatus.Selesai))
        End With
    End Sub

    Protected Sub dtgMain_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim arr As ArrayList

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim oTOP As TOPSPTransferPayment = CType(e.Item.DataItem, TOPSPTransferPayment)

            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then

                Dim lblCreditAcc As Label = CType(e.Item.FindControl("lblCreditAcc"), Label)
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                Dim lblPlanTransferDate As Label = CType(e.Item.FindControl("lblPlanTransferDate"), Label)
                Dim lblDueDate As Label = CType(e.Item.FindControl("lblDueDate"), Label)
                Dim lblRegNumber As Label = CType(e.Item.FindControl("lblRegNumber"), Label)
                Dim lblRegNumberPemercepat As Label = CType(e.Item.FindControl("lblRegNumberPemercepat"), Label)
                Dim lblRegNumberDipercepat As Label = CType(e.Item.FindControl("lblRegNumberDipercepat"), Label)
                Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
                Dim lblSelisih As Label = CType(e.Item.FindControl("lblSelisih"), Label)
                Dim lbtnTransferactual As LinkButton = CType(e.Item.FindControl("lbtnTransferactual"), LinkButton)
                Dim lblNilaiTransfer As Label = CType(e.Item.FindControl("lblNilaiTransfer"), Label)
                Dim lblTglTransfer As Label = CType(e.Item.FindControl("lblTglTransfer"), Label)
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                Dim lbtnDetail As LinkButton = CType(e.Item.FindControl("lbtnDetail"), LinkButton)
                Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)

                Dim lblJmlKliring As Label = CType(e.Item.FindControl("lblJmlKliring"), Label)
                Dim lblTglKliring As Label = CType(e.Item.FindControl("lblTglKliring"), Label)
                Dim lblNoTR As Label = CType(e.Item.FindControl("lblNoTR"), Label)

                Dim lblDueDateLeft As Label = CType(e.Item.FindControl("lblDueDateLeft"), Label)

                Dim lblDate As Label = CType(e.Item.FindControl("lblDate"), Label)
                lblDate.Text = Date.Now.ToString("dd/MM/yyyy")

                CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgMain.CurrentPageIndex * dtgMain.PageSize)

                lblCreditAcc.Text = oTOP.CreditAccount
                lblDealerCode.Text = oTOP.Dealer.DealerCode
                lblPlanTransferDate.Text = oTOP.TransferPlanDate
                lblDueDate.Text = oTOP.DueDate
                lblRegNumber.Text = oTOP.RegNumber
                hdnID.Value = oTOP.ID

                If oTOP.Status = 0 OrElse oTOP.Status = 4 Then
                    lblDueDateLeft.Text = CInt(DateDiff(DateInterval.Day, Date.Now, oTOP.DueDate) * -1)

                    If DateDiff(DateInterval.Day, Now.Date, oTOP.DueDate) < 0 Then
                        e.Item.BackColor = Color.Red
                    ElseIf DateDiff(DateInterval.Day, Now.Date, oTOP.DueDate) > 0 AndAlso DateDiff(DateInterval.Day, Now.Date, oTOP.DueDate) <= 3 Then
                        e.Item.BackColor = Color.Yellow
                    End If
                Else
                    Dim dif As Integer = DateDiff(DateInterval.Day, oTOP.DueDate, oTOP.TransferActualDate) 'otop actual < duedate = 0
                    If dif < 0 Then
                        lblDueDateLeft.Text = 0
                    Else 'else itung
                        lblDueDateLeft.Text = CInt(dif * -1)
                    End If
                End If

                If oTOP.TransferActualDate.Year >= 2000 Then
                    lblTglTransfer.Text = oTOP.TransferActualDate
                End If


                Dim topAccNo As TOPSPAccountingNo = New TOPSPAccountingNoFacade(User).Retrieve(oTOP)
                lblJmlKliring.Text = topAccNo.KliringAmount.ToString("N0")

                If topAccNo.KliringDate.Year < 2000 Then
                    lblNoTR.Text = ""
                Else
                    lblTglKliring.Text = topAccNo.KliringDate
                End If

                lblNoTR.Text = topAccNo.TRNo

                Dim pemercepat As String = String.Empty
                Dim dipercepat As String = String.Empty
                If oTOP.TOPSPTransferPaymentIDReff <> 0 And oTOP.IsAccelerated = 1 Then
                    Dim critSPTP2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critSPTP2.opAnd(New Criteria(GetType(TOPSPTransferPayment), "ID", MatchType.Exact, oTOP.TOPSPTransferPaymentIDReff))
                    Dim topSPTP2 As ArrayList = New TOPSPTransferPaymentFacade(User).Retrieve(critSPTP2)
                    lblRegNumberDipercepat.Text = topSPTP2(0).RegNumber

                Else
                    Dim critSPTP2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critSPTP2.opAnd(New Criteria(GetType(TOPSPTransferPayment), "TOPSPTransferPaymentIDReff", MatchType.Exact, oTOP.ID))
                    Dim topSPTP2 As ArrayList = New TOPSPTransferPaymentFacade(User).Retrieve(critSPTP2)
                    If topSPTP2.Count > 0 Then
                        lblRegNumberPemercepat.Text = topSPTP2(0).RegNumber

                    End If
                End If

                Dim Amount As Double = 0
                Dim critAmount As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critAmount.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.ID", MatchType.Exact, oTOP.ID))
                Dim arlTOP As ArrayList = New TOPSPTransferPaymentDetailFacade(User).Retrieve(critAmount)
                For Each top As TOPSPTransferPaymentDetail In arlTOP
                    Amount = Amount + CType(top.Amount, Double)
                Next
                totalAmount = totalAmount + Amount
                lblAmount.Text = CType(Amount, Double).ToString("N0")

                Dim selisih As Double = oTOP.TransferAmount - Amount
                lblSelisih.Text = selisih.ToString("N0")
                If selisih < 0 Then
                    lblSelisih.Attributes("style") = "color: red;"
                    If e.Item.BackColor = Color.Red Then
                        lblSelisih.Attributes("style") = "color: black;"
                    End If
                Else
                    lblSelisih.Attributes("style") = "color: black;"
                End If

                lbtnTransferactual.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/TOPPopUpTransferActual.aspx?TransferPaymentID=" & oTOP.ID.ToString(), "", 600, 600, "DummyFunc")

                lblNilaiTransfer.Text = CType(oTOP.TransferAmount, Double).ToString("N0")
                lblStatus.Text = EnumStatusTOPSPTransferPayment.GetString(oTOP.Status)
                lbtnDetail.Visible = True
                If IsAuthorizedUbah Then
                    controlEditView(oTOP.Status, objDealer.Title, lbtnEdit, lbtnDetail, oTOP.IsAccelerated, oTOP.ID.ToString(), oTOP.IsAccelerated)
                Else
                    lbtnEdit.Visible = IsAuthorizedUbah
                End If
                'controlEditView(oTOP.Status, objDealer.Title, lbtnEdit, lbtnDetail, oTOP.IsAccelerated, oTOP.ID.ToString(), oTOP.IsAccelerated)
                'lbtnEdit.Visible = IsAuthorizedUbah
            End If
        End If
    End Sub

    Private Sub controlEditView(p1 As Short, p2 As String, edit As LinkButton, detail As LinkButton, isAccel As Short, ID As String, accel As Short)
        edit.Visible = False
        If p2 <> EnumDealerTittle.DealerTittle.KTB Then
            Select Case p1
                Case EnumStatusTOPSPTransferPayment.TOPSPStatus.Validasi
                    edit.Visible = True
                Case EnumStatusTOPSPTransferPayment.TOPSPStatus.Baru
                    If accel = 0 Then
                        edit.Visible = True
                    End If
                Case EnumStatusTOPSPTransferPayment.TOPSPStatus.Konfirmasi
                    edit.Visible = True
                Case Else
                    edit.Visible = False
            End Select
        Else
            Select Case p1
                Case EnumStatusTOPSPTransferPayment.TOPSPStatus.Baru
                    If accel = 1 Then
                        edit.Visible = True
                    End If
                Case EnumStatusTOPSPTransferPayment.TOPSPStatus.Konfirmasi
                    If accel = 1 Then
                        edit.Visible = True
                    End If
                Case Else
                    edit.Visible = False
            End Select
        End If
    End Sub

    Private Sub dtgMain_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.ItemCommand

        Select Case e.CommandName
            Case "Edit" 'Insert New item to datagrid
                Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)
                GetPayment(hdnID)


                If CType(_sessHelper.GetSession("Dealer"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB Then
                    Response.Redirect("FrmTOPSPTransferPaymentKTB.aspx?Mode=Edit")
                Else
                    Response.Redirect("FrmTOPSPTransferPayment.aspx?Mode=Edit")
                End If

            Case "Detail"
                Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)
                GetPayment(hdnID)


                If CType(_sessHelper.GetSession("Dealer"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB Then
                    Response.Redirect("FrmTOPSPTransferPaymentKTB.aspx?Mode=View")
                Else
                    Response.Redirect("FrmTOPSPTransferPayment.aspx?Mode=View")
                End If
        End Select

    End Sub

    Protected Sub dtgMain_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgMain.PageIndexChanged
        dtgMain.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
    End Sub

    Private Function validateInput() As Boolean
        If calPlanTransferDateStart.Value.Year.ToString.Trim.Length = 1 Then
            MessageBox.Show("Format tanggal salah")
            Return False
        End If

        If calPlanTransferDateEnd.Value.Year.ToString.Trim.Length = 1 Then
            MessageBox.Show("Format tanggal salah")
            Return False
        End If

        If calDueDateStart.Value.Year.ToString.Trim.Length = 1 Then
            MessageBox.Show("Format tanggal salah")
            Return False
        End If

        If calDueDateEnd.Value.Year.ToString.Trim.Length = 1 Then
            MessageBox.Show("Format tanggal salah")
            Return False
        End If
        Return True
    End Function

    Private Sub dtgMain_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgMain.SortCommand
        If e.SortExpression = ViewState("SortColTOPSP") Then
            If ViewState("SortDirTOPSP") = Sort.SortDirection.ASC Then
                ViewState.Add("SortDirTOPSP", Sort.SortDirection.DESC)
            Else
                ViewState.Add("SortDirTOPSP", Sort.SortDirection.ASC)
            End If
        End If
        ViewState.Add("SortColTOPSP", e.SortExpression)
        BindGrid(dtgMain.CurrentPageIndex)
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim data As ArrayList = GetData(0, True)
        DoDownload(data)

    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "Daftar Pembayaran" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        Dim DepositAData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(DepositAData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(DepositAData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteData(sw, data)

                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            imp.StopImpersonate()
            imp = Nothing
            MessageBox.Show("Download data gagal" & ex.Message)
        End Try
    End Sub

    Private Sub WriteData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("TOP Sparepart - Daftar Pembayaran")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Credit Account" & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Tgl Rencana Transfer" & tab)
            itemLine.Append("Tgl Jatuh Tempo" & tab)
            itemLine.Append("Sisa Waktu Jatuh Tempo" & tab)
            itemLine.Append("Nomor Reg." & tab)
            itemLine.Append("No. Reg. Pemercepat" & tab)
            itemLine.Append("No. Reg. Dipercepat" & tab)
            itemLine.Append("Total Amount" & tab)
            itemLine.Append("Selisih Transfer" & tab)
            itemLine.Append("Nilai Transfer" & tab)
            itemLine.Append("Tgl Aktual Transfer" & tab)
            itemLine.Append("Status" & tab)
            sw.WriteLine(itemLine.ToString())

            Dim Number As Integer


            For Each item As TOPSPTransferPayment In data
                If item.RowStatus = 0 Then
                    Number += 1
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(Number & tab)
                    itemLine.Append(item.CreditAccount & tab)
                    itemLine.Append(item.Dealer.DealerCode & tab)
                    itemLine.Append(item.TransferPlanDate & tab)
                    itemLine.Append(item.DueDate & tab)
                    'itemLine.Append(DateDiff(DateInterval.Day, item.TransferPlanDate, item.DueDate) & tab)
                    Dim dif As Integer = DateDiff(DateInterval.Day, Date.Now.Date, item.DueDate)
                    If dif < 0 Then
                        itemLine.Append(0 & tab)
                    Else
                        itemLine.Append(dif & tab)
                    End If
                    itemLine.Append(item.RegNumber & tab)
                    Dim pemercepat As String = "-"
                    Dim dipercepat As String = "-"
                    If item.TOPSPTransferPaymentIDReff <> 0 And item.IsAccelerated = 1 Then
                        Dim critSPTP2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        critSPTP2.opAnd(New Criteria(GetType(TOPSPTransferPayment), "ID", MatchType.Exact, item.TOPSPTransferPaymentIDReff))
                        Dim topSPTP2 As ArrayList = New TOPSPTransferPaymentFacade(User).Retrieve(critSPTP2)
                        pemercepat = topSPTP2(0).RegNumber
                    Else
                        Dim critSPTP2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        critSPTP2.opAnd(New Criteria(GetType(TOPSPTransferPayment), "TOPSPTransferPaymentIDReff", MatchType.Exact, item.ID))
                        Dim topSPTP2 As ArrayList = New TOPSPTransferPaymentFacade(User).Retrieve(critSPTP2)
                        If topSPTP2.Count > 0 Then
                            dipercepat = topSPTP2(0).RegNumber
                        End If
                    End If
                    itemLine.Append(pemercepat & tab)
                    itemLine.Append(dipercepat & tab)

                    Dim Amount As Double = 0
                    Dim critAmount As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critAmount.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.ID", MatchType.Exact, item.ID))
                    Dim arlTOP As ArrayList = New TOPSPTransferPaymentDetailFacade(User).Retrieve(critAmount)
                    For Each top As TOPSPTransferPaymentDetail In arlTOP
                        Amount = Amount + CType(top.Amount, Double)
                    Next

                    itemLine.Append(CType(Amount, Double) & tab)
                    Dim selisih = item.TransferAmount - Amount
                    itemLine.Append(CType(selisih, Double) & tab)
                    itemLine.Append(CType(item.TransferAmount, Double) & tab)
                    If item.TransferActualDate.Year >= 2018 Then
                        itemLine.Append(item.TransferActualDate & tab)
                    Else
                        itemLine.Append("-" & tab)
                    End If
                    itemLine.Append(EnumStatusTOPSPTransferPayment.GetString(item.Status) & tab)

                    sw.WriteLine(itemLine.ToString())
                End If
            Next
        End If
    End Sub

    Protected Sub btnDownloadDetail_Click(sender As Object, e As EventArgs) Handles btnDownloadDetail.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dtgMain.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(_sessHelper.GetSession(_vsCritSearch)) Then
            crits = CType(_sessHelper.GetSession(_vsCritSearch), CriteriaComposite)
        End If
        arrData = New TOPSPTransferPaymentFacade(User).Retrieve(crits)
        If arrData.Count > 0 Then
            Dim strFileName As String = "Daftar Pembayaran Detail" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
            CreateExcel(strFileName, arrData)
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim strDefDate As String = "1753/01/01"
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Credit Account"
            ws.Cells("C3").Value = "Kode Dealer"
            ws.Cells("D3").Value = "Tgl Rencana Transfer"
            ws.Cells("E3").Value = "Tgl Jatuh Tempo"
            ws.Cells("F3").Value = "Nomor Reg"
            ws.Cells("G3").Value = "Nomor Reg Pemercepat"
            ws.Cells("H3").Value = "Nomor Reg Dipercepat"
            ws.Cells("I3").Value = "Kode Dealer Billing"
            ws.Cells("J3").Value = "Billing Number"
            ws.Cells("K3").Value = "Tanggal Billing"
            ws.Cells("L3").Value = "Amount Billing + Tax"
            ws.Cells("M3").Value = "Amount C2"
            ws.Cells("N3").Value = "Total Amount"
            ws.Cells("O3").Value = "Selisih Transfer"
            ws.Cells("P3").Value = "Nilai Transfer"
            ws.Cells("Q3").Value = "Tgl Actual Transfer"
            ws.Cells("R3").Value = "Jumlah Kliring"
            ws.Cells("S3").Value = "Tgl Kliring"
            ws.Cells("T3").Value = "No TR"
            ws.Cells("U3").Value = "Status"

            Dim rowStart As Integer = 4

            Dim objTOPSPTransferPayment As TOPSPTransferPayment = New TOPSPTransferPayment
            Dim arrTOPSPDeposit As ArrayList = New ArrayList
            Dim objTOPSPDeposit As TOPSPDeposit = New TOPSPDeposit

            For i As Integer = 0 To Data.Count - 1

                Dim item As TOPSPTransferPayment = Data(i)

                objTOPSPTransferPayment = CType(Data(i), TOPSPTransferPayment)

                Dim cri As New CriteriaComposite(New Criteria(GetType(TOPSPTransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                cri.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.ID", MatchType.Exact, objTOPSPTransferPayment.ID))

                Dim sTP As New SortCollection()
                sTP.Add(New Sort(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.ID", Sort.SortDirection.DESC))

                Dim tOPSPTransferPaymentDetailArr As ArrayList = New TOPSPTransferPaymentDetailFacade(User).Retrieve(cri, sTP)
                Dim objTOPSPTransferPaymentDetail As TOPSPTransferPaymentDetail

                If tOPSPTransferPaymentDetailArr.Count > 0 Then

                    objTOPSPTransferPaymentDetail = CType(tOPSPTransferPaymentDetailArr(0), TOPSPTransferPaymentDetail)

                    For Each itemDetail As TOPSPTransferPaymentDetail In tOPSPTransferPaymentDetailArr

                        ws.Cells(String.Format("A{0}", rowStart)).Value = rowStart - 3
                        ws.Cells(String.Format("B{0}", rowStart)).Value = item.Dealer.CreditAccount
                        ws.Cells(String.Format("C{0}", rowStart)).Value = item.Dealer.DealerCode
                        ws.Cells(String.Format("D{0}", rowStart)).Value = IIf(item.TransferPlanDate <> Date.Parse(strDefDate), Format(item.TransferPlanDate, "dd/MM/yyyy").ToString, "")
                        ws.Cells(String.Format("E{0}", rowStart)).Value = IIf(item.DueDate <> Date.Parse(strDefDate), Format(item.DueDate, "dd/MM/yyyy").ToString, "")
                        ws.Cells(String.Format("F{0}", rowStart)).Value = item.RegNumber

                        Dim pemercepat As String = String.Empty
                        Dim dipercepat As String = String.Empty

                        If item.TOPSPTransferPaymentIDReff <> 0 And item.IsAccelerated = 1 Then
                            Dim critSPTP2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            critSPTP2.opAnd(New Criteria(GetType(TOPSPTransferPayment), "ID", MatchType.Exact, item.TOPSPTransferPaymentIDReff))
                            Dim topSPTP2 As ArrayList = New TOPSPTransferPaymentFacade(User).Retrieve(critSPTP2)
                            dipercepat = topSPTP2(0).RegNumber

                        Else
                            Dim critSPTP2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            critSPTP2.opAnd(New Criteria(GetType(TOPSPTransferPayment), "TOPSPTransferPaymentIDReff", MatchType.Exact, item.ID))
                            Dim topSPTP2 As ArrayList = New TOPSPTransferPaymentFacade(User).Retrieve(critSPTP2)
                            If topSPTP2.Count > 0 Then
                                pemercepat = topSPTP2(0).RegNumber
                            End If
                        End If

                        ws.Cells(String.Format("G{0}", rowStart)).Value = pemercepat
                        ws.Cells(String.Format("H{0}", rowStart)).Value = dipercepat
                        ws.Cells(String.Format("I{0}", rowStart)).Value = itemDetail.SparePartBilling.Dealer.DealerCode
                        ws.Cells(String.Format("J{0}", rowStart)).Value = itemDetail.SparePartBilling.BillingNumber
                        ws.Cells(String.Format("K{0}", rowStart)).Value = IIf(itemDetail.SparePartBilling.BillingDate <> Date.Parse(strDefDate), Format(itemDetail.SparePartBilling.BillingDate, "dd/MM/yyyy").ToString, "")
                        ws.Cells(String.Format("L{0}", rowStart)).Value = Convert.ToDecimal(itemDetail.SparePartBilling.TotalAmount + itemDetail.SparePartBilling.Tax)

                        Dim amountC2 As Decimal = 0

                        Dim cri2 As New CriteriaComposite(New Criteria(GetType(TOPSPDeposit), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        cri2.opAnd(New Criteria(GetType(TOPSPDeposit), "SparePartBilling.ID", MatchType.Exact, itemDetail.SparePartBilling.ID))
                        arrTOPSPDeposit = New TOPSPDepositFacade(User).Retrieve(cri2)

                        If arrTOPSPDeposit.Count > 0 Then
                            objTOPSPDeposit = CType(arrTOPSPDeposit(0), TOPSPDeposit)
                            amountC2 = objTOPSPDeposit.AmountC2
                        End If

                        ws.Cells(String.Format("M{0}", rowStart)).Value = amountC2

                        ws.Cells(String.Format("N{0}", rowStart)).Value = Convert.ToDecimal((itemDetail.SparePartBilling.TotalAmount + _
                                                                         itemDetail.SparePartBilling.Tax) + _
                                                                          CType(arrTOPSPDeposit(0), TOPSPDeposit).AmountC2)

                        Dim selisih As Double = item.TransferAmount - itemDetail.Amount
                        ws.Cells(String.Format("O{0}", rowStart)).Value = selisih.ToString("N0")
                        ws.Cells(String.Format("P{0}", rowStart)).Value = item.TransferAmount
                        ws.Cells(String.Format("Q{0}", rowStart)).Value = IIf(item.TransferActualDate <> Date.Parse(strDefDate), Format(item.TransferActualDate, "dd/MM/yyyy").ToString, "")
                        Dim topAccNo As TOPSPAccountingNo = New TOPSPAccountingNoFacade(User).Retrieve(item)
                        ws.Cells(String.Format("R{0}", rowStart)).Value = topAccNo.KliringAmount.ToString("N0")

                        If topAccNo.KliringDate.Year < 2000 Then
                            ws.Cells(String.Format("S{0}", rowStart)).Value = ""
                        Else
                            ws.Cells(String.Format("S{0}", rowStart)).Value = topAccNo.KliringDate
                            ws.Column(19).Style.Numberformat.Format = "DD/MM/YY"
                        End If

                        ws.Cells(String.Format("T{0}", rowStart)).Value = topAccNo.TRNo
                        ws.Cells(String.Format("U{0}", rowStart)).Value = EnumStatusTOPSPTransferPayment.GetString(item.Status)

                        rowStart += 1

                    Next

                End If

            Next

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
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

        'Response.AddHeader("content-disposition", "attachment; filename= Election_Results " + DateTime.Now.Year.ToString() + ".xls")
        'Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.AddHeader("content-disposition", "attachment; filename=" + fileName)
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"  'xlsx

        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()

    End Sub

    Protected Sub dtgMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dtgMain.SelectedIndexChanged

    End Sub
End Class
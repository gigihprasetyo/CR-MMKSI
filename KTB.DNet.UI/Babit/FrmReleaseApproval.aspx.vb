Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.BabitSalesComm
Imports KTB.DNet.BusinessFacade.General

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports KTB.DNet.UI.Helper



Public Class FrmReleaseApproval
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnGenerateNo As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dgBabitProposal As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtNomorPembayaran As System.Web.UI.WebControls.TextBox
    Protected WithEvents ICPaymentDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtDealerInvoice As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoPersetujuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlReleaseStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents chk02 As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblPopUpBabitProposal As System.Web.UI.WebControls.Label
    Protected WithEvents chk01 As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents ICPersetujuan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _BabitProposalFacade As New BabitProposalFacade(User)
    Private _BabitPaymentFacade As New BabitPaymentFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper
    Private objDealer As Dealer
#End Region

#Region "PrivateCustomMethods"




#End Region

#Region "EventHandlers"


#End Region
#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PaymentView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Babit Pembayaran")
        End If
    End Sub

    Private Function blnCekPaymentEditPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PaymentEdit_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function blnCekPaymentSavePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PaymentSave_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function blnCekPaymentCreatePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PaymentCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Sub checkDealer()
        If Not objDealer Is Nothing Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtDealerCode.Text = objDealer.DealerCode
                txtDealerCode.Enabled = False
                lblPopUpDealer.Visible = False
                dgBabitProposal.Columns(1).Visible = False
                btnGenerateNo.Visible = False
                btnSimpan.Visible = False
            Else
                txtDealerCode.Enabled = True
                lblPopUpDealer.Visible = True
                dgBabitProposal.Columns(1).Visible = True
                btnGenerateNo.Visible = True
                btnSimpan.Visible = True
            End If
        End If
    End Sub
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CheckPrivilege()
        objDealer = CType(sessHelper.GetSession("Dealer"), Dealer)
        InitiateAuthorization()

        If Not IsPostBack Then
            checkDealer()
            Initialize()
            BindDropDownLists()
            BindControlsAttribute()
            'BindDataGrid(0)
            'add security
            If Not blnCekPaymentEditPrivilege() Then
                dgBabitProposal.Columns(9).Visible = False  'aksi digrid untuk edit
            End If

            If Not blnCekPaymentSavePrivilege() Then
                dgBabitProposal.Columns(1).Visible = False
                btnSimpan.Enabled = False
            End If

            If Not blnCekPaymentCreatePrivilege() Then
                btnGenerateNo.Enabled = False
            End If
        Else
            If (Request.Form.Item("__EVENTTARGET") = "chk01") Or _
                (Request.Form.Item("__EVENTTARGET") = "chk02") Then
                SetValueSession()
                CheckAllSession()
            End If
        End If
    End Sub

    ' Untuk bind data yg bersangkutan - related
    Private Sub BindDropDownLists()
        CommonFunction.BindFromEnum("BabitReleaseStatus", ddlReleaseStatus, Me.User, False, "BabitCode", "BabitValue")
        'ddlJenisKegiatan.Items.Insert(0, New ListItem("Silakan Pilih", "-1"))
        ddlReleaseStatus.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))

    End Sub

    Private Sub BindControlsAttribute()
        lblPopUpDealer.Attributes("onClick") = "showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);"
        lblPopUpBabitProposal.Attributes("onClick") = "showPopUp('../General/../PopUp/PopUpBabitProposal.aspx','',500,760,BabitProposalSelection);"
    End Sub

    Private Sub ChangeButton(ByVal btnState As Boolean)

        If blnCekPaymentSavePrivilege() Then
            btnSimpan.Enabled = btnState
        Else
            btnSimpan.Enabled = blnCekPaymentSavePrivilege()
        End If

        If blnCekPaymentCreatePrivilege() Then
            btnGenerateNo.Enabled = btnState
        Else
            btnGenerateNo.Enabled = blnCekPaymentCreatePrivilege()
        End If

    End Sub

#Region "Need To Add"
    ' penambahan untuk initialize data
    Private Sub ClearData()
        ICPersetujuan.Value = Date.Now
        ICPersetujuan.Enabled = False

        If (objDealer.Title <> EnumDealerTittle.DealerTittle.DEALER) Then
            txtDealerCode.Text = String.Empty
        End If


        txtNomorPembayaran.Text = String.Empty
        ICPaymentDate.Value = Date.Now
        ICPaymentDate.Enabled = False
        txtDealerInvoice.Text = String.Empty
        txtNoPersetujuan.Text = String.Empty
        ddlReleaseStatus.SelectedIndex = -1
        chk01.Checked = False
        chk02.Checked = False

        dgBabitProposal.EditItemIndex = -1
        dgBabitProposal.DataSource = Nothing
        dgBabitProposal.DataBind()

        If dgBabitProposal.Items.Count > 0 Then
            dgBabitProposal.SelectedIndex = -1
        End If
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub Initialize()
        ClearData()
        'ViewState("CurrentSortColumn") = "AreaCode"
        'ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiLihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - Daftar SPL")
        End If

        _create = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiBuat_Privilege)
        _edit = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiUbah_Privilege)
        _view = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiDetail_Privilege)

        'lbtnNew.Visible = _create
        ' btnSearch.Visible = _view

    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim arrListBabitPayment As New ArrayList
        Dim strProposalID As String = ""
        Dim blnDefault As Boolean = True
        Dim mode As Integer = 1

        ' CRITERIA UNTUK BABIT PROPOSAL
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If (txtDealerCode.Text.Trim <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(BabitProposal), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
            End If
            blnDefault = False
        Else
            If (txtDealerCode.Text.Trim <> String.Empty) Then
                If New DataOwner().IsdealerExistInGroup(txtDealerCode.Text.Trim, objDealer) Then
                    criterias.opAnd(New Criteria(GetType(BabitProposal), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
                Else
                    mode = 0
                End If
            Else
                Dim strCrit As String = New DataOwner().GenerateDealerCodeSelection(objDealer, User)
                criterias.opAnd(New Criteria(GetType(BabitProposal), "Dealer.DealerCode", MatchType.InSet, strCrit))
            End If
            blnDefault = False
        End If


        If txtNoPersetujuan.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(BabitProposal), "NoPersetujuan", MatchType.InSet, "('" + Replace(txtNoPersetujuan.Text, ";", "','") + "')"))
            blnDefault = False
        End If

        ' UNTUK TGL PERSETUJUAN    
        If chk01.Checked Then
            criterias.opAnd(New Criteria(GetType(BabitProposal), "CreatedTime", MatchType.Exact, ICPersetujuan.Value))
            blnDefault = False
        End If

        ' DIAMBIL YANG SDH DISETUJUI STATUS PROPOSALNYA
        criterias.opAnd(New Criteria(GetType(BabitProposal), "Status", MatchType.Exact, CType(EnumBabit.StatusBabitProposal.Disetujui, Integer)))

        arrList = _BabitProposalFacade.Retrieve(criterias)

        If arrList.Count > 0 Then
            For Each item As BabitProposal In arrList
                If strProposalID = "" Then
                    strProposalID = item.ID
                Else
                    strProposalID = strProposalID & ";" & item.ID
                End If
            Next
        End If

        'CRITERIA UNTUK BABIT PAYMENT
        Dim criteriaBabitPayments As New CriteriaComposite(New Criteria(GetType(BabitPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtNomorPembayaran.Text <> "" Then
            criteriaBabitPayments.opAnd(New Criteria(GetType(BabitPayment), "NomorPembayaran", MatchType.[Partial], txtNomorPembayaran.Text))
        End If

        If txtDealerInvoice.Text <> "" Then
            criteriaBabitPayments.opAnd(New Criteria(GetType(BabitPayment), "DealerInvoice", MatchType.[Partial], txtDealerInvoice.Text))
        End If

        If chk02.Checked Then
            criteriaBabitPayments.opAnd(New Criteria(GetType(BabitPayment), "PaymentDate", MatchType.Exact, ICPaymentDate.Value))
        End If
        'If ddlReleaseStatus.SelectedValue = 1 Or ddlReleaseStatus.SelectedValue = 2 Then
        '    blnDefault = True
        'End If

        If (txtDealerCode.Text.Trim <> String.Empty) Then
            criteriaBabitPayments.opAnd(New Criteria(GetType(BabitPayment), "BabitProposal.Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
        End If

        If blnDefault = False Then
            If strProposalID <> "" Then
                Select Case ddlReleaseStatus.SelectedValue
                    Case 2 ' OUTSTANDING
                        criteriaBabitPayments.opAnd(New Criteria(GetType(BabitPayment), "BabitProposal.ID", MatchType.NotInSet, strProposalID.Replace(";", ",")))
                    Case Else
                        criteriaBabitPayments.opAnd(New Criteria(GetType(BabitPayment), "BabitProposal.ID", MatchType.InSet, CommonFunction.GetStrValue(strProposalID, ";", ",")))
                End Select

                ' 28-Nov-2007   Deddy H     Status pembayaran babit dilihat dr kolom Nomor Pembayarannya
                Select Case ddlReleaseStatus.SelectedItem.Text
                    Case EnumBabit.PaymentReleaseStatus.Complete.ToString
                        criteriaBabitPayments.opAnd(New Criteria(GetType(BabitPayment), "NomorPembayaran", MatchType.No, ""))
                    Case EnumBabit.PaymentReleaseStatus.OutStanding.ToString
                        criteriaBabitPayments.opAnd(New Criteria(GetType(BabitPayment), "NomorPembayaran", MatchType.Exact, ""))
                End Select

                arrListBabitPayment = _BabitPaymentFacade.RetrieveByCriteria(criteriaBabitPayments, idxPage + 1, dgBabitProposal.PageSize, totalRow, _
                                        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            Else
                arrListBabitPayment = New ArrayList
            End If
        Else
            arrListBabitPayment = _BabitPaymentFacade.RetrieveByCriteria(criteriaBabitPayments, idxPage + 1, dgBabitProposal.PageSize, totalRow, _
                                        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        End If

        If mode <> 0 Then
            dgBabitProposal.CurrentPageIndex = idxPage
            dgBabitProposal.DataSource = arrListBabitPayment
            dgBabitProposal.VirtualItemCount = totalRow
            dgBabitProposal.DataBind()
            If (IsNothing(arrListBabitPayment) Or arrListBabitPayment.Count <= 0) Then
                MessageBox.Show("Data tidak ditemukan")
            End If
        Else
            dgBabitProposal.DataSource = Nothing
            dgBabitProposal.DataBind()
            MessageBox.Show("Kode dealer tidak valid.")
        End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            dgBabitProposal.Columns(dgBabitProposal.Columns.Count - 1).Visible = False
        End If

    End Sub


#End Region

    Private Sub dgBabitProposal_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgBabitProposal.PageIndexChanged
        dgBabitProposal.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgBabitProposal.CurrentPageIndex)
    End Sub

    Private Sub dgBabitProposal_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgBabitProposal.ItemCommand
        If e.CommandName = "Edit" Then
            dgBabitProposal.EditItemIndex = e.Item.ItemIndex
            ChangeButton(False)
            BindDataGrid(0)
        End If

        If e.CommandName = "Cancel" Then
            dgBabitProposal.EditItemIndex = -1
            ChangeButton(True)
            BindDataGrid(0)
        End If

        If e.CommandName = "Save" Then
            Dim objBabitPayment As BabitPayment
            Dim strTmp() As String = CType(e.CommandArgument, String).Split(";")
            Dim intBabitProposalId As Integer = CType(strTmp(0), Integer)
            Dim intBabitPaymentId As Integer = CType(strTmp(1), Integer)

            objBabitPayment = New BabitPaymentFacade(User).Retrieve(intBabitPaymentId)

            Dim txtEditDealerInvoiceNew As TextBox = CType(e.Item.FindControl("txtEditDealerInvoice"), TextBox)
            If txtEditDealerInvoiceNew.Text = "" Then
                MessageBox.Show("Data Invoice Dealer harus diinput terlebih dahulu")
                Return
            End If
            'Dim txtEditPaymentDateNew As TextBox = CType(e.Item.FindControl("txtEditPaymentDate"), TextBox)
            Dim icEditPaymentDateNew As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icEditPaymentDate"), KTB.DNet.WebCC.IntiCalendar)

            Dim lblEditDealerCodeNew As Label = CType(e.Item.FindControl("lblEditDealerCode"), Label)
            Dim intResult As Integer
            Dim facade As BabitPaymentFacade = New BabitPaymentFacade(User)

            If Not IsNothing(objBabitPayment) Then
                ' bila ada lakukan proses update
                objBabitPayment.DealerInvoice = txtEditDealerInvoiceNew.Text
                objBabitPayment.PaymentDate = icEditPaymentDateNew.Value
                intResult = facade.Update(objBabitPayment)
            Else
                ' bila belum ada lakukan proses insert
                Dim objDealer As Dealer = New DealerFacade(User).Retrieve(lblEditDealerCodeNew.Text)
                Dim objBabitProposal As BabitProposal = New BabitProposalFacade(User).Retrieve(intBabitProposalId)
                Dim objBabitPaymentAdd As New BabitPayment

                objBabitPaymentAdd.Dealer = objDealer
                objBabitPaymentAdd.BabitProposal = objBabitProposal
                objBabitPaymentAdd.DealerInvoice = txtEditDealerInvoiceNew.Text
                objBabitPaymentAdd.PaymentDate = icEditPaymentDateNew.Value
                'objBabitPayment.PaymentStatus = EnumBabit.PaymentReleaseStatus.Semua
                intResult = facade.Insert(objBabitPaymentAdd)
            End If

            ' bila sdh ada lakukan proses update
            dgBabitProposal.EditItemIndex = -1
            ChangeButton(True)
            BindDataGrid(0)
        End If

        If (e.CommandName = "Reject") Then
            Dim bpfacade As New KTB.DNet.BusinessFacade.BabitSalesComm.BabitProposalFacade(Page.User)
            Dim pfacade As New BabitPaymentFacade(User)
            Dim obj As BabitPayment = pfacade.Retrieve(Convert.ToInt32(e.CommandArgument))
            obj.BabitProposal.KTBApprovalAmount = 0
            bpfacade.Update(obj.BabitProposal)
            obj.NomorPembayaran = BabitPayment.REJECTED_ALL_DESC
            pfacade.Update(obj)
            BindDataGrid(0)
            MessageBox.Show("Item sudah ditolak")
        End If

        If (e.CommandName = "detail") Then
            BindDataGrid(dgBabitProposal.CurrentPageIndex)
        End If


    End Sub

    Private Sub dgBabitProposal_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgBabitProposal.ItemDataBound
        Dim arrTmp As ArrayList
        'Dim objBabitPayment As BabitPayment = New BabitPayment
        Dim strDefDate As String = "1753/01/01"

        If Not e.Item.DataItem Is Nothing Then
            checkDealer()
            Dim objBabitPayment As BabitPayment = e.Item.DataItem

            'Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(BabitPayment), "BabitProposal.ID", MatchType.Exact, objBabitProposal.ID))

            'arrTmp = New BabitPaymentFacade(User).Retrieve(criterias)

            'If Not IsNothing(arrTmp) Then
            '    If arrTmp.Count > 0 Then
            '        For Each items As BabitPayment In arrTmp
            '            objBabitPayment.ID = items.ID
            '            objBabitPayment.NomorPembayaran = items.NomorPembayaran
            '            objBabitPayment.DealerInvoice = items.DealerInvoice
            '            objBabitPayment.PaymentDate = items.PaymentDate
            '        Next
            '    Else
            '        ' tdk ada datanya dipayment
            '        objBabitPayment.ID = -1
            '        objBabitPayment.NomorPembayaran = ""
            '        objBabitPayment.DealerInvoice = ""
            '        objBabitPayment.PaymentDate = Date.Parse(strDefDate)
            '    End If
            'End If

            e.Item.Cells(2).Text = e.Item.ItemIndex + 1 + (dgBabitProposal.CurrentPageIndex * dgBabitProposal.PageSize)

            ' untuk bagian item / alternate item
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                ' mapping value from database to component
                Dim lblDealerCodeNew As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                lblDealerCodeNew.Text = objBabitPayment.Dealer.DealerCode

                Dim lblNomorPembayaranNew As Label = CType(e.Item.FindControl("lblNomorPembayaran"), Label)
                lblNomorPembayaranNew.Text = objBabitPayment.NomorPembayaran

                Dim lblNoPersetujuanNew As Label = CType(e.Item.FindControl("lblNoPersetujuan"), Label)
                lblNoPersetujuanNew.Text = objBabitPayment.BabitProposal.NoPersetujuan

                Dim lblDealerInvoiceNew As Label = CType(e.Item.FindControl("lblDealerInvoice"), Label)
                lblDealerInvoiceNew.Text = objBabitPayment.DealerInvoice

                Dim lblPaymentDateNew As Label = CType(e.Item.FindControl("lblPaymentDate"), Label)
                lblPaymentDateNew.Text = IIf(objBabitPayment.PaymentDate.ToString = DateTime.Parse(strDefDate), "", objBabitPayment.PaymentDate.ToString("dd/MM/yyyy"))

                Dim lblKTBApprovalAmountNew As Label = CType(e.Item.FindControl("lblKTBApprovalAmount"), Label)
                lblKTBApprovalAmountNew.Text = objBabitPayment.BabitProposal.KTBApprovalAmount.ToString("#,##0")

                Dim lbtnTmpNew As LinkButton = CType(e.Item.FindControl("lbtnTmp"), LinkButton)
                lbtnTmpNew.CommandArgument = objBabitPayment.BabitProposal.ID & ";" & objBabitPayment.ID

                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)

                If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    lbtnEdit.Visible = False
                Else
                    lbtnEdit.Visible = True
                End If

                If (objBabitPayment.BabitProposal.ActivityType = CType(EnumBabit.BabitProposalType.Iklan, Short)) Then
                    Dim lnkDetail As LinkButton = CType(e.Item.FindControl("lnkDetail"), LinkButton)
                    lnkDetail.Attributes.Add("onclick", String.Format("javascript:showPopUp('../PopUp/PopUpBpIklan.aspx?id={0}&paymentid={1}&dummy={2}','',500,760,'');", objBabitPayment.BabitProposal.ID, objBabitPayment.ID, DateTime.Now.ToString("ddMMyyyyhhmmss")))
                    lnkDetail.Text = "Detil"
                Else
                    Dim lnkReject As LinkButton = e.Item.FindControl("lnkReject")
                    lnkReject.Text = "Reject"
                End If

            End If

            ' untuk bagian edit item
            If e.Item.ItemType = ListItemType.EditItem Then
                Dim lbtnSaveNew As LinkButton = CType(e.Item.FindControl("lbtnSave"), LinkButton)
                lbtnSaveNew.CommandArgument = objBabitPayment.BabitProposal.ID & ";" & objBabitPayment.ID

                Dim lblEditDealerCodeNew As Label = CType(e.Item.FindControl("lblEditDealerCode"), Label)
                lblEditDealerCodeNew.Text = objBabitPayment.BabitProposal.Dealer.DealerCode

                Dim lblEditNomorPembayaranNew As Label = CType(e.Item.FindControl("lblEditNomorPembayaran"), Label)
                lblEditNomorPembayaranNew.Text = objBabitPayment.NomorPembayaran

                Dim lblEditNoPersetujuanNew As Label = CType(e.Item.FindControl("lblEditNoPersetujuan"), Label)
                lblEditNoPersetujuanNew.Text = objBabitPayment.BabitProposal.NoPersetujuan

                Dim txtEditDealerInvoiceNew As TextBox = CType(e.Item.FindControl("txtEditDealerInvoice"), TextBox)
                txtEditDealerInvoiceNew.Text = objBabitPayment.DealerInvoice

                'Dim txtEditPaymentDateNew As TextBox = CType(e.Item.FindControl("txtEditPaymentDate"), TextBox)
                'txtEditPaymentDateNew.Text = IIf(objBabitPayment.PaymentDate.ToString = strDefDate, "", objBabitPayment.PaymentDate.ToString)

                Dim icEditPaymentDateNew As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icEditPaymentDate"), KTB.DNet.WebCC.IntiCalendar)
                icEditPaymentDateNew.Value = IIf(objBabitPayment.PaymentDate = strDefDate, Now(), objBabitPayment.PaymentDate)

                Dim lblEditKTBApprovalAmountNew As Label = CType(e.Item.FindControl("lblEditKTBApprovalAmount"), Label)
                lblEditKTBApprovalAmountNew.Text = objBabitPayment.BabitProposal.KTBApprovalAmount.ToString("#,##0")

            End If
        End If

    End Sub

    Private Sub dgBabitProposal_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgBabitProposal.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If
        dgBabitProposal.SelectedIndex = -1
        dgBabitProposal.CurrentPageIndex = 0
        BindDataGrid(dgBabitProposal.CurrentPageIndex)
    End Sub

    Private Sub chk01_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' setting date persetujuan
        If chk01.Checked Then
            ICPersetujuan.Enabled = True
        Else
            ICPersetujuan.Enabled = False
        End If

        'Dim hsTmp As New Hashtable
        'hsTmp.Add("ChkTmp", chk02.Checked)
        'hsTmp.Add("DateValue", ICPaymentDate.Value)
        'hsTmp.Add("DateEnabled", ICPaymentDate.Enabled)
        'sessHelper.SetSession("sessPaymentDate", hsTmp)
    End Sub

    Private Sub chk02_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk02.CheckedChanged
        ' setting date payment
        If chk02.Checked Then
            ICPaymentDate.Enabled = True
        Else
            ICPaymentDate.Enabled = False
        End If
        'Dim hsTmp As New Hashtable
        'hsTmp.Add("ChkTmp", chk01.Checked)
        'hsTmp.Add("DateValue", ICPersetujuan.Value)
        'hsTmp.Add("DateEnabled", ICPersetujuan.Enabled)
        'sessHelper.SetSession("sessPersetujuan", hsTmp)
    End Sub

    Private Sub SetValueSession()
        Dim hsPersetujuan As New Hashtable
        hsPersetujuan.Add("ChkTmp", chk01.Checked)
        hsPersetujuan.Add("DateValue", ICPersetujuan.Value)
        hsPersetujuan.Add("DateEnabled", IIf(chk01.Checked, True, False))
        sessHelper.SetSession("sessPersetujuan", hsPersetujuan)

        Dim hsPaymentDate As New Hashtable
        hsPaymentDate.Add("ChkTmp", chk02.Checked)
        hsPaymentDate.Add("DateValue", ICPaymentDate.Value)
        hsPaymentDate.Add("DateEnabled", IIf(chk02.Checked, True, False))
        sessHelper.SetSession("sessPaymentDate", hsPaymentDate)
    End Sub

    Private Sub ClearAllSessions()
        sessHelper.RemoveSession("sessPersetujuan")
        sessHelper.RemoveSession("sessPaymentDate")
    End Sub

    Private Sub CheckAllSession()

        ' set value type date
        If Not IsNothing(sessHelper.GetSession("sessPersetujuan")) Then
            Dim hsPersetujuan As Hashtable
            hsPersetujuan = CType(sessHelper.GetSession("sessPersetujuan"), Hashtable)
            chk01.Checked = CType(hsPersetujuan.Item("ChkTmp"), Boolean)
            ICPersetujuan.Value = CType(hsPersetujuan.Item("DateValue"), Date)
            ICPersetujuan.Enabled = CType(hsPersetujuan.Item("DateEnabled"), Boolean)
        End If

        If Not IsNothing(sessHelper.GetSession("sessPaymentDate")) Then
            Dim hsPaymentDate As Hashtable
            hsPaymentDate = CType(sessHelper.GetSession("sessPaymentDate"), Hashtable)
            chk02.Checked = CType(hsPaymentDate.Item("ChkTmp"), Boolean)
            ICPaymentDate.Value = CType(hsPaymentDate.Item("DateValue"), Date)
            ICPaymentDate.Enabled = CType(hsPaymentDate.Item("DateEnabled"), Boolean)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgBabitProposal.CurrentPageIndex = 0
        BindDataGrid(dgBabitProposal.CurrentPageIndex)
    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub btnGenerateNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerateNo.Click
        ' menghasilan no pembayaran secara langsung
        Dim facade As New BabitPaymentFacade(User)
        Dim intResult As Integer
        Dim strConstValue As String = "Payment"
        Dim intMaxCode As Integer
        intMaxCode = 3

        Dim counter As Integer = 0
        For Each item As DataGridItem In dgBabitProposal.Items
            Dim chkPilihNew As CheckBox = CType(item.FindControl("chkPilih"), CheckBox)
            If chkpilihnew.Checked Then
                counter += 1
            End If
        Next
        If counter = 0 Then
            MessageBox.Show("Pilih Data Pembayaran yang akan digenerate")
            Exit Sub
        End If

        For Each item As DataGridItem In dgBabitProposal.Items
            Dim chkPilihNew As CheckBox = CType(item.FindControl("chkPilih"), CheckBox)
            Dim lbtnTmpNew As LinkButton = CType(item.FindControl("lbtnTmp"), LinkButton)
            Dim lblDealerCodeNew As Label = CType(item.FindControl("lblDealerCode"), Label)
            Dim strTmp() As String = lbtnTmpNew.CommandArgument.Split(";")
            Dim intBabitPaymentId As Integer = strTmp(1)

            'untuk pengecekan saja
            Dim lblDealerInvoice As Label = CType(item.FindControl("lblDealerInvoice"), Label)
            Dim lblPaymentDate As Label = CType(item.FindControl("lblPaymentDate"), Label)

            If chkPilihNew.Checked = True Then
                ' update data yg sdh ada
                If intBabitPaymentId <> -1 Then
                    If lblDealerInvoice.Text = "" Or lblPaymentDate.Text = "" Then
                        MessageBox.Show("Data Invoice Dealer dan Tanggal Pembayaran harus diisi")
                        Exit Sub
                    Else
                        Dim objBabitPayment As BabitPayment = New BabitPaymentFacade(User).Retrieve(intBabitPaymentId)
                        If objBabitPayment.NomorPembayaran = "" Then
                            'ambil data max dahulu
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitPayment), "Dealer.DealerCode", MatchType.Exact, lblDealerCodeNew.Text))
                            criterias.opAnd(New Criteria(GetType(BabitPayment), "NomorPembayaran", MatchType.No, ""))

                            Dim agg As Aggregate = New Aggregate(GetType(BabitPayment), "DealerInvoice", AggregateType.Count)
                            Dim MaxRunNumber As String = New BabitPaymentFacade(User).RetrieveScalar(criterias, agg, False)
                            Dim curRunNumber As Integer

                            If MaxRunNumber = "" Then
                                curRunNumber = 1
                            Else
                                curRunNumber = CType(Right(MaxRunNumber, intMaxCode), Integer) + 1
                                If Not IsNumeric(curRunNumber) Then
                                    curRunNumber = 1
                                End If
                            End If

                            Dim strCurRunNumber As String
                            strCurRunNumber = CommonFunction.ConvIntToStr(curRunNumber, intMaxCode)

                            objBabitPayment.NomorPembayaran = (strCurRunNumber & "/" & objBabitPayment.Dealer.DealerCode & "/" & strConstValue & "/" & Now().Month.ToString.PadLeft(2, "0") & "/" & Right(Now().Year.ToString, 2)).ToUpper
                            intResult = facade.Update(objBabitPayment)
                        End If
                    End If
                End If
            End If
        Next
        If intResult <> -1 Then
            MessageBox.Show("Proses Generate berhasil dilakukan")
            BindDataGrid(0)
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim facade As New BabitPaymentFacade(User)
        Dim intResult As Integer

        For Each item As DataGridItem In dgBabitProposal.Items
            Dim chkPilihNew As CheckBox = CType(item.FindControl("chkPilih"), CheckBox)
            Dim lbtnTmpNew As LinkButton = CType(item.FindControl("lbtnTmp"), LinkButton)
            Dim strTmp() As String = lbtnTmpNew.CommandArgument.Split(";")
            Dim intBabitPaymentId As Integer = strTmp(1)

            ' update data yg sdh ada
            If intBabitPaymentId <> -1 Then
                Dim objBabitPayment As BabitPayment = New BabitPaymentFacade(User).Retrieve(intBabitPaymentId)
                objBabitPayment.PaymentStatus = EnumBabit.PaymentReleaseStatus.Complete
                intResult = facade.Update(objBabitPayment)
            End If
        Next

        If intResult > 0 Then
            MessageBox.Show(SR.UpdateSucces & " - Status pembayaran complete")
        Else
            MessageBox.Show(SR.UpdateFail)
        End If
    End Sub
End Class


Imports KTB.DNET.BusinessFacade.General
Imports KTB.DNET.BusinessFacade.IndentPart
Imports KTB.DNET.BusinessFacade.SparePart
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Domain
Imports KTB.DNET.Security
Imports KTB.DNET.Utility
Imports System.IO
Imports System.Text
Imports System.Web.UI.WebControls
Imports System.Linq
Imports System.Data.DataSetExtensions

Public Class FrmListIndentPart
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoPO As System.Web.UI.WebControls.TextBox
    Protected WithEvents lstStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents ddlupdatestatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents dtgIndentPart As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents icPODateFrom As KTB.DNET.WebCC.IntiCalendar
    Protected WithEvents icPODateUntil As KTB.DNET.WebCC.IntiCalendar
    Protected WithEvents lblPopUpPONo As System.Web.UI.WebControls.Label
    Protected WithEvents btnSubmit As System.Web.UI.WebControls.Button
    Protected WithEvents imgIndikator As System.Web.UI.WebControls.Image
    Protected WithEvents Image1 As System.Web.UI.WebControls.Image
    Protected WithEvents ddlMaterialType1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents DdlDesc As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTermOfPayment As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblGrandTotal As System.Web.UI.WebControls.Label
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim oDealer As Dealer
    Private arlIndentPart As ArrayList = New ArrayList
    Private _sesshelper As New SessionHelper
    Dim EmailIndentRecipient As String = KTB.DNET.Lib.WebConfig.GetValue("EmailIndentRecipient")

    Dim EmailIndentRecipientCC As String = KTB.DNET.Lib.WebConfig.GetValue("EmailIndentRecipientCC")

    Dim EmailIndentCC1 As String = KTB.DNET.Lib.WebConfig.GetValue("EmailIndentCC1")
    Dim EmailIndentCC2 As String = KTB.DNET.Lib.WebConfig.GetValue("EmailIndentCC2")
    Dim EmailIndentCC3 As String = KTB.DNET.Lib.WebConfig.GetValue("EmailIndentCC3")
    Dim EmailIndentCC4 As String = KTB.DNET.Lib.WebConfig.GetValue("EmailIndentCC4")
    Dim EmailIndentCC5 As String = KTB.DNET.Lib.WebConfig.GetValue("EmailIndentCC5")

    Dim EmailIndentSignedName As String = KTB.DNET.Lib.WebConfig.GetValue("EmailIndentSignedName")
    Dim EmailIndentSignedJob As String = KTB.DNET.Lib.WebConfig.GetValue("EmailIndentSignedJob")



#Region "Custom Method"

    Private Sub BindDdlPaymentType()
        Dim dlr As Dealer = CType(Session("DEALER"), Dealer)
        Dim spCa As VWI_DealerSettingCreditAccount = New VWI_DealerSettingCreditAccountFacade(User).RetrieveByDealerCode(dlr.DealerCode)
        Dim oTopCA As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(spCa.TermOfPaymentID)
        Dim listOfPayments As ArrayList = New ArrayList
        If (dlr.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            If Not IsNothing(oTopCA) Then
                listOfPayments = New TermOfPaymentFacade(User).RetrieveFromSP(oTopCA.PaymentType, spCa.KelipatanPembayaran, oTopCA.TermOfPaymentValue)
            End If
        Else
            Dim criteriaTOP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            listOfPayments = New TermOfPaymentFacade(User).Retrieve(criteriaTOP)
        End If

        ddlTermOfPayment.DataSource = listOfPayments
        ddlTermOfPayment.DataValueField = "ID"
        ddlTermOfPayment.DataTextField = "Description"
        ddlTermOfPayment.DataBind()
        ddlTermOfPayment.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
    End Sub

    Private Sub BindMaterialType()
        Dim arl As ArrayList = New EnumMaterialType().RetrieveType()
        ddlMaterialType1.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
        For Each imat As EnumMaterial In arl
            ddlMaterialType1.Items.Add(New ListItem(imat.NameType, imat.ValType.ToString))
        Next
        ddlMaterialType1.SelectedIndex = -1
    End Sub

    Private Sub BindStatus(ByVal Bval As Boolean)
        If Bval Then
            Dim arlList As New ArrayList
            Dim arlListKTB_Tmp As ArrayList = New EnumIndentPartStatus().RetrieveTypeUpdateForKTB()
            For Each item As EnumIPStatus In arlListKTB_Tmp
                Select Case item.ValType
                    Case EnumIndentPartStatus.IndentPartStatusKTB.Proses
                        If bCekDSProses Then
                            arlList.Add(item)
                        End If
                    Case EnumIndentPartStatus.IndentPartStatusKTB.KTB_Konfirmasi
                        If bCekStatusKTBConfirmationPriv Then
                            arlList.Add(item)
                        End If
                    Case EnumIndentPartStatus.IndentPartStatusKTB.Rilis
                        If bCekStatusKTBRilisPriv Then
                            arlList.Add(item)
                        End If
                    Case EnumIndentPartStatus.IndentPartStatusKTB.Tolak
                        If bCekStatusKTBRejectPriv Then
                            arlList.Add(item)
                        End If
                    Case EnumIndentPartStatus.IndentPartStatusKTB.Selesai
                        If bCekStatusKTBFinishedPriv Then
                            arlList.Add(item)
                        End If

                End Select
            Next

            ddlupdatestatus.DataSource = arlList
            'ddlupdatestatus.DataSource = New EnumIndentPartStatus().RetrieveTypeUpdateForKTB()
            ddlupdatestatus.DataTextField = "NameType"
            ddlupdatestatus.DataValueField = "ValType"
            ddlupdatestatus.DataBind()

            'Remove Tools
            'If bCekStatusKTBCancelConfirmationPriv Then
            '    ddlupdatestatus.Items.Insert(2, New ListItem("Batal Konfirmasi", "A"))
            'End If
            'If bCekStatusKTBCancelRilisPriv Then
            '    ddlupdatestatus.Items.Insert(4, New ListItem("Batal Rilis", "B"))
            'End If

            'ddlupdatestatus.Items.Insert(6, New ListItem("Batal Tolak", "C"))
            'ddlupdatestatus.Items.RemoveAt(0)

            lstStatus.DataSource = New EnumIndentPartStatus().RetrieveTypeForKTB()
            lstStatus.DataTextField = "NameType"
            lstStatus.DataValueField = "ValType"
            lstStatus.DataBind()
        Else
            Dim arlListDealer As ArrayList = New EnumIndentPartStatus().RetrieveTypeUpdateForDealer()
            Dim arlList As New ArrayList
            For Each item As EnumIPStatus In arlListDealer
                Select Case item.ValType
                    Case EnumIndentPartStatus.IndentPartStatusDealer.Batal
                        If bCekStatusBatalDealerPriv Then
                            arlList.Add(item)
                        End If
                    Case EnumIndentPartStatus.IndentPartStatusDealer.Kirim
                        If bCekStatusKirimDealerPriv Then
                            arlList.Add(item)
                        End If
                    Case EnumIndentPartStatus.IndentPartStatusDealer.Dealer_Konfirmasi
                        If bCekStatusDealerKonfirmDealerPriv Then
                            arlList.Add(item)
                        End If
                        ' fix bug 1686  , status batal order
                    Case EnumIndentPartStatus.IndentPartStatusDealer.Batal_Order
                        If bCekStatusBatalOrderDealerPriv Then
                            arlList.Add(item)
                        End If
                End Select
            Next
            ddlupdatestatus.DataSource = arlList
            ddlupdatestatus.DataTextField = "NameType"
            ddlupdatestatus.DataValueField = "ValType"
            ddlupdatestatus.DataBind()
            'ddlupdatestatus.Items.RemoveAt(0)

            lstStatus.DataSource = New EnumIndentPartStatus().RetrieveTypeForDealer
            lstStatus.DataTextField = "NameType"
            lstStatus.DataValueField = "ValType"
            lstStatus.DataBind()
            'lstStatus.Items.Add(New ListItem(EnumIndentPartStatus.IndentPartStatusDealer.InProgress.ToString, CInt(EnumIndentPartStatus.IndentPartStatusDealer.InProgress)))
            'lstStatus.Items.Add(New ListItem(EnumIndentPartStatus.IndentPartStatusDealer.Tolak.ToString, CInt(EnumIndentPartStatus.IndentPartStatusDealer.Tolak)))
            'lstStatus.Items.Add(New ListItem(EnumIndentPartStatus.IndentPartStatusDealer.Selesai.ToString, CInt(EnumIndentPartStatus.IndentPartStatusDealer.Selesai)))

        End If

        ddlupdatestatus.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlupdatestatus.SelectedIndex = 0

    End Sub

    Private Sub CreateCriteria()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'Exclude Equipment
        criterias.opAnd(New Criteria(GetType(IndentPartHeader), "MaterialType", MatchType.No, CInt(EnumMaterialType.MaterialType.Equipment)))

        oDealer = Session("DEALER")
        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then  '---User is login as Dealer


            criterias.opAnd(New Criteria(GetType(IndentPartHeader), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))

            '-----criteria status untuk dealer
            If lstStatus.SelectedIndex <> -1 Then
                Dim li As ListItem
                Dim _status As String = "("
                For Each li In lstStatus.Items
                    If li.Selected Then
                        _status = _status & li.Value & ","
                    End If
                Next
                _status = _status.Substring(0, _status.Length - 1) & ")"

                criterias.opAnd(New Criteria(GetType(IndentPartHeader), "Status", MatchType.InSet, _status))
            End If

        Else '----User Login As KTB

            '-----criteria dealer
            If txtDealerCode.Text <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(IndentPartHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
            End If

            '----criteria Status untuk KTB
            criterias.opAnd(New Criteria(GetType(IndentPartHeader), "StatusKTB", MatchType.No, CInt(EnumIndentPartStatus.IndentPartStatusKTB.BelumValidasi)))

            If lstStatus.SelectedIndex <> -1 Then
                Dim li As ListItem
                Dim _status As String = "("
                For Each li In lstStatus.Items
                    If li.Selected Then
                        _status = _status & li.Value & ","
                    End If
                Next
                _status = _status.Substring(0, _status.Length - 1) & ")"

                criterias.opAnd(New Criteria(GetType(IndentPartHeader), "StatusKTB", MatchType.InSet, _status))
            End If

        End If


        If Val(DdlDesc.SelectedValue) <> CInt(EnumIndentDesc.IndentDesc.Silakan_Pilih) Then
            criterias.opAnd(New Criteria(GetType(IndentPartHeader), "DescID", MatchType.Exact, DdlDesc.SelectedValue))
        End If

        If txtNoPO.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(IndentPartHeader), "RequestNo", MatchType.InSet, "('" & txtNoPO.Text.Replace(";", "','") & "')"))


        If icPODateFrom.Value <= icPODateUntil.Value Then
            criterias.opAnd(New Criteria(GetType(IndentPartHeader), "RequestDate", MatchType.GreaterOrEqual, icPODateFrom.Value))
            criterias.opAnd(New Criteria(GetType(IndentPartHeader), "RequestDate", MatchType.LesserOrEqual, icPODateUntil.Value))
        Else
            MessageBox.Show("Tanggal PO 'Dari' tidak boleh lebih Besar dari Tanggal PO 'Sampai' ")
            Exit Sub
        End If

        'Tipe Barang
        If ddlMaterialType1.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(IndentPartHeader), "MaterialType", MatchType.Exact, ddlMaterialType1.SelectedValue))
        End If

        If Not ddlTermOfPayment.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(IndentPartHeader), "TermOfPayment.ID", MatchType.Exact, ddlTermOfPayment.SelectedValue))
        End If
        _sesshelper.SetSession("crits", criterias)
    End Sub

    Sub BindToGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim xsum As IndentPartDetail



        arlIndentPart = New IndentPartHeaderFacade(User).RetrieveActiveList(_sesshelper.GetSession("crits"), currentPageIndex + 1, dtgIndentPart.PageSize, total, _
           CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        lblGrandTotal.Text = "Grand Total : " & getGrandTotal(CType(_sesshelper.GetSession("crits"), CriteriaComposite))

        dtgIndentPart.VirtualItemCount = total
        dtgIndentPart.DataSource = arlIndentPart
        dtgIndentPart.DataBind()
        _sesshelper.SetSession("ListIP", arlIndentPart)

    End Sub

    Private Sub setHeader()
        Dim arHeader As ArrayList = New ArrayList
        arHeader.Add(txtDealerCode.Text)
        arHeader.Add(txtNoPO.Text)
        arHeader.Add(lstStatus.SelectedValue)
        arHeader.Add(icPODateFrom.Value)
        arHeader.Add(icPODateUntil.Value)
        arHeader.Add(ddlMaterialType1.SelectedValue)
        arHeader.Add(dtgIndentPart.CurrentPageIndex)
        _sesshelper.SetSession("ListHeader", arHeader)
    End Sub

    Private Function CalculateTotalQty(ByVal arlIPD As ArrayList) As Decimal
        Dim nTotQty As Decimal = 0
        For Each objH As IndentPartHeader In arlIPD
            Dim arlD As ArrayList = objH.IndentPartDetails
            For Each objD As IndentPartDetail In arlD
                nTotQty = nTotQty + objD.Qty
            Next
        Next
        Return (nTotQty)
    End Function

    Private Sub HideEditPopUp()

    End Sub


#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.StatusListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=INDENT PART - Daftar Status")
        End If
    End Sub

    Dim bCekDSProses As Boolean = SecurityProvider.Authorize(Context.User, SR.StatusListStatusProcess_Privilege)
    Dim bCekStatusBatalDealerPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.StatusListStatusBatal_Privilege)
    Dim bCekStatusKirimDealerPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.StatusListStatusKirim_Privilege)
    Dim bCekStatusDealerKonfirmDealerPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.StatusListStatusDealerConfirmation_Privilege)
    Dim bCekStatusLihatDetailDealerPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.StatusListViewDetail_Privilege)
    Dim bCekStatusEditDetailDealerPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.StatusListIndentPartEditDetail_Privilege)
    Dim bCekStatusBatalOrderDealerPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.StatusListStatusBatalOrder_Privilege)



    'for KTB
    Dim bCekStatusEditDetailKTBPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.StatusListIndentPartEdit_Privilege)
    Dim bCekStatusKTBConfirmationPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.StatusListKTBConfirmation_Privilege)
    Dim bCekStatusKTBCancelRilisPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.StatusListCancelRilis_Privilege)
    Dim bCekStatusKTBRilisPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.StatusListRilis_Privilege)
    Dim bCekStatusKTBRejectPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.StatusListReject_Privilege)
    Dim bCekStatusKTBFinishedPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.StatusListFinished_Privilege)
    Dim bCekStatusKTBCancelConfirmationPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.StatusListCancelConfirmation_Privilege)


#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'test()
        oDealer = CType(_sesshelper.GetSession("DEALER"), Dealer)
        If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            InitiateAuthorization()
            btnSubmit.Visible = False
            txtDealerCode.Text = oDealer.DealerCode
            txtDealerCode.Enabled = False
            txtDealerCode.BorderStyle = BorderStyle.None
            lblSearchDealer.Visible = False
            lblPopUpPONo.Attributes("onclick") = "showPopUp('../PopUp/PopUpIndentPart.aspx?DealerID=" & oDealer.ID & "', '', 500, 600,PONOSelection);"
        ElseIf (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            InitiateAuthorization()
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            lblPopUpPONo.Attributes("onclick") = "showPopUp('../PopUp/PopUpIndentPart.aspx', '', 500, 600,PONOSelection);"
            txtDealerCode.Enabled = True
        End If
        If Not IsPostBack Then
            Dim arlitem As ArrayList = CType(_sesshelper.GetSession("ListIP"), ArrayList)
            Dim arlhdr As ArrayList = CType(_sesshelper.GetSession("ListHeader"), ArrayList)
            BindMaterialType()
            BindDdlPaymentType()
            BindDdlDescription()
            If (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
                BindStatus(True)
            Else
                BindStatus(False)
            End If
            'remark by ERY
            'BindMaterial()
            ViewState("currSortColumn") = "Status"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            dtgIndentPart.DataSource = arlIndentPart
            dtgIndentPart.DataBind()
            If Not IsNothing(arlhdr) Then
                txtDealerCode.Text = arlhdr(0)
                txtNoPO.Text = arlhdr(1)
                If arlhdr(2) <> String.Empty Then
                    lstStatus.SelectedValue = arlhdr(2)
                End If
                icPODateFrom.Value = arlhdr(3)
                icPODateUntil.Value = arlhdr(4)
                'remark by Ery
                If arlhdr(5) <> String.Empty Then
                    ddlMaterialType1.SelectedValue = arlhdr(5)
                End If
                btnSearch_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim idxPage As Integer = 0
        Dim arlhdr As ArrayList = CType(_sesshelper.GetSession("ListHeader"), ArrayList)
        If Not IsNothing(arlhdr) And IsNothing(sender) Then 'From back button
            idxPage = arlhdr(6)
        End If
        dtgIndentPart.CurrentPageIndex = idxPage
        CreateCriteria()
        BindToGrid(idxPage)
    End Sub

    Private Sub dtgIndentPart_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgIndentPart.SortCommand
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
        BindToGrid(dtgIndentPart.CurrentPageIndex)
    End Sub

    Private Function isVerifiedStatus() As Boolean
        'Based on Bug 1088 
        Dim TargetStatus As String
        If ddlupdatestatus.SelectedValue = "A" Then
            TargetStatus = EnumIndentPartStatus.IndentPartStatusKTB.Baru
        ElseIf ddlupdatestatus.SelectedValue = "B" Then
            TargetStatus = EnumIndentPartStatus.IndentPartStatusKTB.KTB_Konfirmasi
        ElseIf ddlupdatestatus.SelectedValue = "C" Then
            TargetStatus = EnumIndentPartStatus.IndentPartStatusKTB.Rilis
        Else
            TargetStatus = ddlupdatestatus.SelectedValue
        End If

        Dim sparePartPOTypeTOP As SparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve("I")
        Dim isTOP As Boolean = sparePartPOTypeTOP.IsTOP

        Dim i As Integer = 0
        For Each item As DataGridItem In dtgIndentPart.Items
            i += 1
            Dim chkItemChecked As CheckBox = item.FindControl("chkItemChecked")
            If chkItemChecked.Checked Then
                Dim objIPHeader As IndentPartHeader = CType(_sesshelper.GetSession("ListIP"), ArrayList)(item.ItemIndex)
                If (isTOP And objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Baru) And ((CByte(TargetStatus) = EnumIndentPartStatus.IndentPartStatusDealer.Kirim) And IsNothing(objIPHeader.TermOfPayment)) Then
                    MessageBox.Show("Item " & i.ToString & " : Cara pembayaran harus diisi")
                    Return False
                End If

                If objIPHeader.MaterialType = EnumMaterialType.MaterialType.Tools Then
                    If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then

                        If objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Batal_Order Or objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Tolak Or objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Selesai Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusDealerDesc & " tidak dapat diubah")
                            Return False
                        End If


                        If objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Baru And (CByte(TargetStatus) = EnumIndentPartStatus.IndentPartStatusDealer.Dealer_Konfirmasi Or CByte(TargetStatus) = EnumIndentPartStatus.IndentPartStatusDealer.Batal_Order) Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusDealerDesc & " tidak dapat diubah menjadi " & ddlupdatestatus.SelectedItem.Text)
                            Return False
                        End If

                        If objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Kirim And (CByte(TargetStatus) <> EnumIndentPartStatus.IndentPartStatusDealer.Batal_Order) Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusDealerDesc & " tidak dapat diubah menjadi " & ddlupdatestatus.SelectedItem.Text)
                            Return False
                        End If

                        If objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Batal Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusDealerDesc & " tidak dapat diubah")
                            Return False
                        End If

                        If objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Dealer_Konfirmasi Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusDealerDesc & " tidak dapat diubah")
                            Return False
                        End If

                        If objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.KTB_Konfirmasi And (CByte(TargetStatus) = EnumIndentPartStatus.IndentPartStatusDealer.Baru Or CByte(TargetStatus) = EnumIndentPartStatus.IndentPartStatusDealer.Kirim Or CByte(TargetStatus) = EnumIndentPartStatus.IndentPartStatusDealer.Batal) Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusDealerDesc & " tidak dapat diubah menjadi " & ddlupdatestatus.SelectedItem.Text)
                            Return False
                        End If

                    Else
                        'Dim StatusTarget As Byte
                        'If TargetStatus = "A" Then
                        '    StatusTarget = EnumIndentPartStatus.IndentPartStatusKTB.Baru
                        'ElseIf TargetStatus = "B" Then
                        '    StatusTarget = EnumIndentPartStatus.IndentPartStatusKTB.KTB_Konfirmasi
                        'Else
                        '    StatusTarget = TargetStatus
                        'End If

                        If TargetStatus = EnumIndentPartStatus.IndentPartStatusKTB.Selesai And objIPHeader.POQty = 0 Then
                            MessageBox.Show("Item " & i.ToString & " : Status tidak bisa diubah menjadi selesai. Lakukan alokasi pemesanan atau pilih tolak pengajuan")
                            Return False
                        End If

                        If objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Selesai Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusKTBDesc & " tidak dapat diubah ")
                            Return False
                        End If

                        If objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Baru And TargetStatus = EnumIndentPartStatus.IndentPartStatusKTB.Selesai Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusKTBDesc & " tidak dapat diubah menjadi " & ddlupdatestatus.SelectedItem.Text)
                            Return False
                        End If

                        If objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Baru And Not (TargetStatus = EnumIndentPartStatus.IndentPartStatusKTB.Tolak Or TargetStatus = EnumIndentPartStatus.IndentPartStatusKTB.KTB_Konfirmasi) Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusKTBDesc & " tidak dapat diubah menjadi " & ddlupdatestatus.SelectedItem.Text)
                            Return False
                        End If

                        If objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Batal_Order Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusKTBDesc & " tidak dapat diubah")
                            Return False
                        End If

                        'If (TargetStatus = CType(CByte(EnumIndentPartStatus.IndentPartStatusKTB.Baru), String) and ) Then
                        '    MessageBox.Show("Item " & i.ToString & " : Tidak Bisa Diubah Menjadi Baru")
                        '    Return False
                        'End If


                        If (TargetStatus = CType(CByte(EnumIndentPartStatus.IndentPartStatusKTB.Proses), String)) Then
                            MessageBox.Show("Item " & i.ToString & " : Tipe Tools Tidak Bisa Diubah Menjadi Proses")
                            Return False
                        End If

                        If objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Dealer_Konfirmasi And Not (TargetStatus = CType(EnumIndentPartStatus.IndentPartStatusKTB.Rilis, String) Or TargetStatus = CType(EnumIndentPartStatus.IndentPartStatusKTB.Tolak, String)) Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusKTBDesc & " tidak dapat diubah menjadi " & ddlupdatestatus.SelectedItem.Text)
                            Return False
                        End If

                        If objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.KTB_Konfirmasi And Not (ddlupdatestatus.SelectedValue = "A") Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusKTBDesc & " tidak dapat diubah menjadi " & ddlupdatestatus.SelectedItem.Text)
                            Return False
                        End If

                        If objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Rilis And Not (ddlupdatestatus.SelectedValue = "B" Or TargetStatus = EnumIndentPartStatus.IndentPartStatusKTB.Selesai) Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusKTBDesc & " tidak dapat diubah menjadi " & ddlupdatestatus.SelectedItem.Text)
                            Return False
                        End If

                        If objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Tolak Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusKTBDesc & " tidak dapat diubah menjadi " & ddlupdatestatus.SelectedItem.Text)
                            Return False
                        End If


                        If objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Selesai Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusKTBDesc & " tidak dapat diubah menjadi " & ddlupdatestatus.SelectedItem.Text)
                            Return False
                        End If


                    End If
                Else
                    If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then


                        'If objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Batal Or objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Kirim Or objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Dealer_Konfirmasi Or objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Rilis Or objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Selesai Or objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Tolak Then
                        '    MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusDealerDesc & " tidak dapat diubah")
                        '    Return False
                        'End If

                        'If objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Baru And Not (CByte(TargetStatus) = EnumIndentPartStatus.IndentPartStatusDealer.Batal Or CByte(TargetStatus) = EnumIndentPartStatus.IndentPartStatusDealer.Kirim) Then
                        '    MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusDealerDesc & " tidak dapat diubah menjadi " & ddlupdatestatus.SelectedItem.Text)
                        '    Return False
                        'End If

                        'If objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.KTB_Konfirmasi And Not CByte(TargetStatus) = EnumIndentPartStatus.IndentPartStatusDealer.Dealer_Konfirmasi Then
                        '    MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusDealerDesc & " tidak dapat diubah menjadi " & ddlupdatestatus.SelectedItem.Text)
                        '    Return False
                        'End If

                        If TargetStatus = EnumIndentPartStatus.IndentPartStatusDealer.Batal_Order Then
                            MessageBox.Show("Item " & i.ToString & " : Tipe Material selain tools tidak dapat diubah menjadi batal order")
                            Return False
                        End If

                        If objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Baru And TargetStatus = EnumIndentPartStatus.IndentPartStatusDealer.Dealer_Konfirmasi Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusDealerDesc & " tidak dapat diubah menjadi " & ddlupdatestatus.SelectedItem.Text)
                            Return False
                        End If

                        If objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Kirim Or objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Batal Or objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Dealer_Konfirmasi Or objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.KTB_Konfirmasi Or objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Proses Or objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Tolak Or objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Selesai Then  'Or objIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Rilis
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusDealerDesc & " tidak dapat diubah")
                            Return False
                        End If

                    Else

                        'If TargetStatus = "A" And objIPHeader.StatusKTB <> EnumIndentPartStatus.IndentPartStatusKTB.KTB_Konfirmasi Then
                        '    MessageBox.Show("Item " & i.ToString & " : Proses " & ddlupdatestatus.SelectedItem.Text & " tidak dapat mengubah data dengan status " & objIPHeader.StatusKTBDesc)
                        '    Return False
                        'End If

                        'If TargetStatus = "B" And objIPHeader.StatusKTB <> EnumIndentPartStatus.IndentPartStatusKTB.Rilis Then
                        '    MessageBox.Show("Item " & i.ToString & " : Proses " & ddlupdatestatus.SelectedItem.Text & " tidak dapat mengubah data dengan status " & objIPHeader.StatusKTBDesc)
                        '    Return False
                        'End If


                        'If objIPHeader.PaymentType <> EnumIndentPartStatus.IndentPartPaymentType.Silakan_Pilih And objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.KTB_Konfirmasi Then
                        '    MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusKTBDesc & " tidak dapat diubah setelah dealer menentukan tipe pembayaran")
                        '    Return False
                        'End If

                        'If TargetStatus = CType(EnumIndentPartStatus.IndentPartStatusKTB.Rilis, String) And objIPHeader.StatusKTB <> EnumIndentPartStatus.IndentPartStatusKTB.Dealer_Konfirmasi Then
                        '    MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusKTBDesc & " tidak dapat diubah menjadi " & ddlupdatestatus.SelectedItem.Text)
                        '    Return False
                        'End If


                        'If TargetStatus = CType(EnumIndentPartStatus.IndentPartStatusKTB.Tolak, String) And Not (objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Baru Or objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Dealer_Konfirmasi Or objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Rilis) Then
                        '    MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusKTBDesc & " tidak dapat diubah menjadi " & ddlupdatestatus.SelectedItem.Text)
                        '    Return False
                        'End If

                        'If objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Selesai Or objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Tolak Then
                        '    MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusKTBDesc & " tidak dapat diubah ")
                        '    Return False
                        'End If

                        'Bug 1688 Status Selesai bila Sisa Qty=0
                        'If TargetStatus = EnumIndentPartStatus.IndentPartStatusKTB.Selesai And objIPHeader.SisaQty > 0 Then
                        '    MessageBox.Show("Item " & i.ToString & " : Status tidak bisa diubah menjadi selesai. Lakukan alokasi pemesanan atau pilih tolak pengajuan")
                        '    Return False
                        'End If
                        If TargetStatus = EnumIndentPartStatus.IndentPartStatusKTB.Selesai And objIPHeader.POQty = 0 Then
                            MessageBox.Show("Item " & i.ToString & " : Status tidak bisa diubah menjadi selesai. Lakukan alokasi pemesanan atau pilih tolak pengajuan")
                            Return False
                        End If

                        If objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Dealer_Konfirmasi Or objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.KTB_Konfirmasi Or objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Rilis Or objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Selesai Or objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Tolak Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusKTBDesc & " tidak dapat diubah ")
                            Return False
                        End If

                        If objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Baru And Not (TargetStatus = EnumIndentPartStatus.IndentPartStatusKTB.Tolak Or TargetStatus = EnumIndentPartStatus.IndentPartStatusKTB.Proses) Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusKTBDesc & " tidak dapat diubah menjadi " & ddlupdatestatus.SelectedItem.Text)
                            Return False
                        End If

                        If objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Proses And Not (TargetStatus = EnumIndentPartStatus.IndentPartStatusKTB.Tolak Or TargetStatus = EnumIndentPartStatus.IndentPartStatusKTB.Selesai) Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusKTBDesc & " tidak dapat diubah menjadi " & ddlupdatestatus.SelectedItem.Text)
                            Return False
                        End If

                        If ddlupdatestatus.SelectedValue = "A" Or ddlupdatestatus.SelectedValue = "B" Then
                            MessageBox.Show("Item " & i.ToString & " : Proses " & ddlupdatestatus.SelectedItem.Text & " tidak dapat dilakukan untuk tipe material selain tools")
                            Return False
                        End If

                    End If
                End If

            End If


        Next

        Return True


    End Function

    Private Sub btnProses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProses.Click

        If ddlupdatestatus.SelectedIndex = 0 Then
            MessageBox.Show("Pilih Status Dulu")
            Exit Sub
        End If

        If Not isVerifiedStatus() Then
            Exit Sub
        End If

        Dim arlToUpdate As ArrayList = New ArrayList
        Dim arlToEmail As ArrayList = New ArrayList

        If ddlupdatestatus.SelectedValue = "A" Or ddlupdatestatus.SelectedValue = "B" Or ddlupdatestatus.SelectedValue = "C" Then
            For Each item As DataGridItem In dtgIndentPart.Items
                Dim chkItemChecked As CheckBox = item.FindControl("chkItemChecked")
                If chkItemChecked.Checked Then
                    Dim objIPHeader As IndentPartHeader = CType(_sesshelper.GetSession("ListIP"), ArrayList)(item.ItemIndex)
                    If (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
                        If ddlupdatestatus.SelectedValue = "A" And objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.KTB_Konfirmasi Then
                            arlToUpdate.Add(objIPHeader)
                        ElseIf ddlupdatestatus.SelectedValue = "B" And objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Rilis Then
                            arlToUpdate.Add(objIPHeader)
                        ElseIf ddlupdatestatus.SelectedValue = "C" And objIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Tolak Then
                            arlToUpdate.Add(objIPHeader)
                        End If
                    Else 'dEALER
                        arlToUpdate.Add(objIPHeader)
                    End If
                End If
            Next

        Else

            For Each item As DataGridItem In dtgIndentPart.Items
                Dim chkItemChecked As CheckBox = item.FindControl("chkItemChecked")
                If chkItemChecked.Checked Then
                    Dim objIPHeader As IndentPartHeader = CType(_sesshelper.GetSession("ListIP"), ArrayList)(item.ItemIndex)

                    arlToUpdate.Add(objIPHeader)
                End If
            Next

        End If


        If arlToUpdate.Count = 0 Then

            If ddlupdatestatus.SelectedValue = "A" Then
                MessageBox.Show("Tidak Ada Data Konfirmasi Yang Dipilih")
            ElseIf ddlupdatestatus.SelectedValue = "B" Then
                MessageBox.Show("Tidak Ada Data Rilis Yang Dipilih")
            ElseIf ddlupdatestatus.SelectedValue = "C" Then
                MessageBox.Show("Tidak Ada Data Status Tolak Yang Dipilih")
            Else
                MessageBox.Show("Tidak Ada Data Yang Dipilih")
            End If
            Exit Sub
        End If

        For Each itemToUpdate As IndentPartHeader In arlToUpdate

            If (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
                If ddlupdatestatus.SelectedValue = "A" Then
                    itemToUpdate.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Baru
                ElseIf ddlupdatestatus.SelectedValue = "B" Then
                    itemToUpdate.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.KTB_Konfirmasi
                ElseIf ddlupdatestatus.SelectedValue = "C" Then
                    itemToUpdate.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Rilis
                Else
                    itemToUpdate.StatusKTB = CByte(ddlupdatestatus.SelectedValue)
                End If
                Select Case itemToUpdate.StatusKTB
                    Case EnumIndentPartStatus.IndentPartStatusKTB.Baru
                        itemToUpdate.Status = EnumIndentPartStatus.IndentPartStatusDealer.Kirim
                    Case EnumIndentPartStatus.IndentPartStatusKTB.Proses
                        itemToUpdate.Status = EnumIndentPartStatus.IndentPartStatusDealer.Proses
                    Case EnumIndentPartStatus.IndentPartStatusKTB.KTB_Konfirmasi
                        itemToUpdate.KTBConfirmedDate = Date.Today
                        itemToUpdate.Status = EnumIndentPartStatus.IndentPartStatusDealer.KTB_Konfirmasi
                    Case EnumIndentPartStatus.IndentPartStatusKTB.Rilis
                        itemToUpdate.Status = EnumIndentPartStatus.IndentPartStatusDealer.Proses
                        'KTB Rilis = Dealer Proses
                    Case EnumIndentPartStatus.IndentPartStatusKTB.Tolak
                        itemToUpdate.Status = EnumIndentPartStatus.IndentPartStatusDealer.Tolak
                    Case EnumIndentPartStatus.IndentPartStatusKTB.Selesai
                        itemToUpdate.Status = EnumIndentPartStatus.IndentPartStatusDealer.Selesai
                    Case EnumIndentPartStatus.IndentPartStatusKTB.Dealer_Konfirmasi
                        itemToUpdate.Status = EnumIndentPartStatus.IndentPartStatusDealer.Dealer_Konfirmasi
                End Select
            Else
                'Add arlemail
                If ddlupdatestatus.SelectedValue = EnumIndentPartStatus.IndentPartStatusDealer.Dealer_Konfirmasi And itemToUpdate.Status = EnumIndentPartStatus.IndentPartStatusDealer.KTB_Konfirmasi Then
                    If itemToUpdate.MaterialType = EnumMaterialType.MaterialType.Tools And itemToUpdate.PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_B Then
                        arlToEmail.Add(itemToUpdate)
                    End If
                End If
                itemToUpdate.Status = CByte(ddlupdatestatus.SelectedValue)
                Select Case ddlupdatestatus.SelectedValue
                    Case EnumIndentPartStatus.IndentPartStatusDealer.Baru
                        itemToUpdate.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.BelumValidasi
                    Case EnumIndentPartStatus.IndentPartStatusDealer.Batal
                        itemToUpdate.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.BelumValidasi
                    Case EnumIndentPartStatus.IndentPartStatusDealer.Batal_Order
                        itemToUpdate.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Batal_Order
                    Case EnumIndentPartStatus.IndentPartStatusDealer.Kirim
                        itemToUpdate.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Baru
                    Case EnumIndentPartStatus.IndentPartStatusDealer.Dealer_Konfirmasi
                        itemToUpdate.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Dealer_Konfirmasi
                End Select
            End If


            Dim result As Integer = New IndentPartHeaderFacade(User).Update(itemToUpdate)
            If arlToEmail.Count > 0 Then
                SendEmailArrayList(arlToEmail)
            End If

            If result > 0 AndAlso itemToUpdate.Status = EnumIndentPartStatus.IndentPartStatusDealer.Proses Then
                GeneratePO(itemToUpdate.RequestNo)
            End If

            ''generate PO selain type ACCESSORIES
            'If result > 0 AndAlso itemToUpdate.Status = EnumIndentPartStatus.IndentPartStatusDealer.Proses AndAlso itemToUpdate.MaterialType <> 3 Then
            '    GeneratePO(itemToUpdate.RequestNo)
            'End If

        Next

        BindToGrid(dtgIndentPart.CurrentPageIndex)

        'MessageBox.Show("Data Telah Diupdate")
        MessageBox.Show("Ubah status " & ddlupdatestatus.SelectedItem.Text & " berhasil")
    End Sub

    Private Sub GeneratePO(ByVal RequestNo As String)
        Dim objIPDetailfacade As IndentPartDetailFacade = New IndentPartDetailFacade(User)
        Dim arlToUpdate As ArrayList = New ArrayList()
        Dim arlIPDetail As ArrayList = New ArrayList()

        Dim criteriasIPH As CriteriaComposite
        criteriasIPH = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasIPH.opAnd(New Criteria(GetType(IndentPartHeader), "RequestNo", MatchType.InSet, "('" & RequestNo & "')"))

        Dim IndentPartHeaderData As ArrayList = New IndentPartHeaderFacade(User).Retrieve(criteriasIPH)

        If IndentPartHeaderData.Count > 0 Then
            Dim StrIndentPartHeaderID As String = String.Empty
            For Each objIPH As IndentPartHeader In IndentPartHeaderData
                StrIndentPartHeaderID += objIPH.ID.ToString() + ","
            Next
            If StrIndentPartHeaderID.Length > 0 Then
                StrIndentPartHeaderID = Left(StrIndentPartHeaderID, StrIndentPartHeaderID.Length - 1)
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.ID", MatchType.InSet, "(" & StrIndentPartHeaderID & ")"))
            arlIPDetail = objIPDetailfacade.RetrieveByCriteria(criterias, "IndentPartHeader.Dealer.DealerCode", Sort.SortDirection.ASC)

            Dim arlIPDetailFilter = New System.Collections.ArrayList((From item As IndentPartDetail In arlIPDetail.OfType(Of IndentPartDetail)()
                    Where item.SisaQty > 0
                    Select item).ToList())

            For Each arrIPD As IndentPartDetail In arlIPDetailFilter 'arlIPDetail
                arrIPD.AllocationQty = arrIPD.SisaQty
                arlToUpdate.Add(arrIPD)
            Next
        End If

        Dim rslt As Integer = New IndentPartDetailFacade(User).UpdateAlokasi(arlToUpdate)

        If rslt > 0 Then
            Dim Retval As String = objIPDetailfacade.GeneratePO(arlToUpdate) '(arlIPDetail')
            If Retval <> "" Then
                MessageBox.Show("Generate PO Dengan No " & Retval & " Berhasil")
            End If
        Else
            MessageBox.Show("Gagal update data")
        End If

    End Sub

    Private Sub SendEmailArrayList(ByVal arlToEmail As ArrayList)

        Dim smtp As String = KTB.DNET.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailFrom As String = KTB.DNET.Lib.WebConfig.GetValue("EmailFrom")

        For Each itemToEmail As IndentPartHeader In arlToEmail
            Dim valueEmail As String = GenerateEmail(itemToEmail)
            ObjEmail.sendMail(EmailIndentRecipient, EmailIndentRecipientCC, emailFrom, "[MMKSI-DNet]", Mail.MailFormat.Html, valueEmail)

        Next
    End Sub

    Private Function GenerateEmail(ByVal itemToEmail As IndentPartHeader) As String

        Dim sb As StringBuilder = New StringBuilder("")
        sb.Append("<FONT face=Arial size=1>")
        sb.Append("<table width=700>")
        sb.Append("<tr>")
        sb.Append("<td colspan=6 align=center><b>PEMINDAHAN DEPOSIT B KE C</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=6 height=50></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=6 height=50>")
        sb.Append("Dengan hormat,&nbsp;")
        sb.Append("<br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Agar di bantu proses pemindahan deposit B ke C terhadap order service equipment atas nama dealer sbb ")
        sb.Append("</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=6 height=10></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td width=100>Kode Dealer</td>")
        sb.Append("<td width=10>:</td>")
        sb.Append("<td width=280>" & itemToEmail.Dealer.DealerCode & "</td>")
        sb.Append("<td width=140>No Pengajuan</td>")
        sb.Append("<td width=10>:</td>")
        sb.Append("<td width=160>" & itemToEmail.RequestNo & "</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td>Nama Dealer</td>")
        sb.Append("<td>:</td>")
        sb.Append("<td rowspan=3 valign=top>" & itemToEmail.Dealer.DealerName & "</td>")
        sb.Append("<td>Tanggal Pengajuan</td>")
        sb.Append("<td>:</td>")
        sb.Append("<td>" & itemToEmail.RequestDate.ToString("dd/MM/yyyy") & "</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td>Total Item</td>")
        sb.Append("<td>:</td>")
        sb.Append("<td>" & itemToEmail.TotalQty.ToString("#,##0") & "</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td>Total Amount</td>")
        sb.Append("<td>:</td>")
        sb.Append("<td>" & itemToEmail.AmountAfterTax.ToString("#,##0") & "</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td height=50 colspan=6 align=center><hr><b>DAFTAR PART ORDER</b></td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("<table border=1 width=700 cellpadding=0>")
        sb.Append("<tr>")
        sb.Append("<td width=30>No</td>")
        sb.Append("<td width=100>Part No</td>")
        sb.Append("<td width=295>Part Name</td>")
        sb.Append("<td width=50>Qty</td>")
        sb.Append("<td width=100>Harga/Pc</td>")
        sb.Append("<td width=125>Total</td>")
        sb.Append("</tr>")
        Dim counter As Integer = 0
        For Each itemDetail As IndentPartDetail In itemToEmail.IndentPartDetails
            counter += 1
            sb.Append("<tr>")
            sb.Append("<td>" & counter.ToString & "</td>")
            sb.Append("<td>" & itemDetail.SparePartMaster.PartNumber & "</td>")
            sb.Append("<td>" & itemDetail.SparePartMaster.PartName & "</td>")
            sb.Append("<td>" & itemDetail.Qty.ToString & "</td>")
            sb.Append("<td>" & itemDetail.Price.ToString("#,##0") & "</td>")
            sb.Append("<td>" & (itemDetail.Price * itemDetail.Qty).ToString("#,##0") & "</td>")
            sb.Append("</tr>")
        Next

        sb.Append("</table>")
        sb.Append("<table width=700>")
        sb.Append("<tr>")
        sb.Append("<td width=130></td>")
        sb.Append("<td width=295></td>")
        sb.Append("<td width=30></td>")
        sb.Append("<td width=120 align=right>Amount :</td>")
        sb.Append("<td width=125>" & itemToEmail.AmountBeforeTax.ToString("#,##0") & "</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td align=right>PPN 10 % :</td>")
        sb.Append("<td>" & (itemToEmail.AmountBeforeTax * 0.1).ToString("#,##0") & "</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td align=right>Total Amount :</td>")
        sb.Append("<td>" & itemToEmail.AmountAfterTax.ToString("#,##0") & "</td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("<br>")
        sb.Append("<table  width=700 >")
        sb.Append("<tr>")
        sb.Append("<td colspan=6>Terima kasih atas perhatian dan kerjasamanya.</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td height=30 width=30></td>")
        sb.Append("<td width=100></td>")
        sb.Append("<td width=295></td>")
        sb.Append("<td width=50></td>")
        sb.Append("<td width=225 align=center></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td align=center>Jakarta, " & Today.ToString("dd/MM/yyyy") & "</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td height=60 colspan=6></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td align=center><u>" & EmailIndentSignedName & "</u></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td align=center>" & EmailIndentSignedJob & "</td>")
        sb.Append("</tr>")
        'sb.Append("<tr>")
        'sb.Append("<td>CC :</td>")
        'sb.Append("<td colspan=4>" & EmailIndentCC1 & "</td>")
        'sb.Append("</tr>")
        'sb.Append("<tr>")
        'sb.Append("<td></td>")
        'sb.Append("<td colspan=4>" & EmailIndentCC2 & "</td>")
        'sb.Append("</tr>")
        'sb.Append("<tr>")
        'sb.Append("<td></td>")
        'sb.Append("<td colspan=4>" & EmailIndentCC3 & "</td>")
        'sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td></td>")
        sb.Append("<td colspan=4>" & EmailIndentCC4 & "</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td></td>")
        sb.Append("<td colspan=4>" & EmailIndentCC5 & "</td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("</FONT>")

        Return sb.ToString

    End Function


    Private Sub dtgIndentPart_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgIndentPart.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgIndentPart.PageSize * dtgIndentPart.CurrentPageIndex)).ToString
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim obj As IndentPartHeader = CType(e.Item.DataItem, IndentPartHeader)
            Dim lblStatus As Label = e.Item.FindControl("lblStatus")

            If obj.POQty <= 0 Then
                Dim imgIndikator As Image = CType(e.Item.FindControl("imgIndikator"), Image)
                imgIndikator.ImageUrl = "../images/red.gif"
            ElseIf obj.SisaQty = 0 Or obj.SisaQty < 0 Then
                Dim imgIndikator As Image = CType(e.Item.FindControl("imgIndikator"), Image)
                imgIndikator.ImageUrl = "../images/green.gif"
            ElseIf ((obj.SisaQty > 0) And (obj.POQty > 0)) Then
                Dim imgIndikator As Image = CType(e.Item.FindControl("imgIndikator"), Image)
                imgIndikator.ImageUrl = "../images/yellow.gif"
            End If


            If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                lblStatus.Text = obj.StatusKTBDesc
                'bug 719
                If obj.MaterialType = EnumMaterialType.MaterialType.Tools Then
                    If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                        ' change request, refer bug 982, hanya bs edit harga saja
                        CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = True
                        'CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
                        If obj.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Selesai Or obj.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Tolak Then
                            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
                        End If
                    End If
                Else
                    If obj.Status = EnumIndentPartStatus.IndentPartStatusDealer.Selesai Then
                        CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
                    Else
                        'Untuk mengubah catatan KTB
                        CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = True
                    End If
                End If
            Else
                lblStatus.Text = obj.StatusDealerDesc
                If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                    'Bug 1427
                    If obj.Status = EnumIndentPartStatus.IndentPartStatusDealer.Baru Or obj.Status = EnumIndentPartStatus.IndentPartStatusDealer.KTB_Konfirmasi Then
                        If obj.MaterialType <> EnumMaterialType.MaterialType.Tools And obj.Status = EnumIndentPartStatus.IndentPartStatusDealer.KTB_Konfirmasi Then
                            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
                        Else
                            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = bCekStatusEditDetailDealerPriv
                        End If
                    Else
                        CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
                    End If
                End If
            End If

            Dim lbtnDetails As LinkButton = CType(e.Item.FindControl("lbtnDetails"), LinkButton)
            If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If lbtnDetails.Visible = True Then
                    lbtnDetails.Visible = bCekStatusLihatDetailDealerPriv
                End If
            End If
            'Bug 1427
            'If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER And obj.Status = EnumIndentPartStatus.IndentPartStatusDealer.Proses Then
            '    lbtnDetails.Visible = False
            'End If
            'add by ery for highlight
            Dim maxday As Integer = CInt(KTB.DNET.Lib.WebConfig.GetValue("BlockedDaysKTBConfirm"))
            Dim confirmDate As Date = obj.KTBConfirmedDate
            Dim currentDate As Date = Date.Today
            Dim range As Date = currentDate.AddDays(-14)
            If confirmDate <> "1/1/1753" Then
                'Bug  1647
                If obj.Status = EnumIndentPartStatus.IndentPartStatusDealer.KTB_Konfirmasi Then
                    If range > confirmDate Then
                        e.Item.BackColor = Color.Red
                    End If
                End If

            End If

            'KTB and Dealer Same Privilege
            If lbtnDetails.Visible = True Then
                lbtnDetails.Visible = bCekStatusLihatDetailDealerPriv
            Else
                lbtnDetails.Visible = False
            End If

            'KTB and Dealer Different Privilege
            If CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = True And oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = bCekStatusEditDetailKTBPriv
            End If
            If CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = True And oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = bCekStatusEditDetailDealerPriv
            End If
        End If

    End Sub

    Private Sub dtgIndentPart_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgIndentPart.ItemCommand
        If e.CommandName.ToUpper = "SORT" Then Return

        Dim objIndent As IndentPartHeader = New IndentPartHeaderFacade(User).Retrieve(CInt(e.CommandArgument))
        If objIndent.MaterialType = EnumMaterialType.MaterialType.Tools Then
            MessageBox.Show("Data lama Material Tools tidak dapat diolah kembali")
            Exit Sub
        End If
        Dim checkTOP As New Label
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            checkTOP = CType(e.Item.FindControl("LabelTOP"), Label)
        End If
        If checkTOP.Text = "COD" Then
            _sesshelper.SetSession("COD", True)
        End If

        If (e.CommandName = "detail") Then
            setHeader()
            Response.Redirect("../IndentPart/FrmIndentPart.aspx?IndentPartHeaderID=" & e.CommandArgument & "&View=True")
        End If
        If (e.CommandName = "Edit") Then
            setHeader()
            Response.Redirect("../IndentPart/FrmIndentPart.aspx?IndentPartHeaderID=" & e.CommandArgument & "&View=False")
        End If
    End Sub

#End Region

    Private Sub BindDdlDescription()
        DdlDesc.DataValueField = "ID"
        DdlDesc.DataTextField = "Name"
        DdlDesc.DataSource = EnumIndentDesc.RetrieveIndentDesc
        DdlDesc.DataBind()
        DdlDesc.SelectedIndex = 0

    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim iph As IndentPartHeader
        Dim ipd As ArrayList

        Dim SW As StreamWriter
        Dim _filename As String = String.Format("{0}{1}{2}", "IndentPart", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        Dim _destFile As String = KTB.DNET.Lib.WebConfig.GetValue("SAN") & KTB.DNET.Lib.WebConfig.GetValue("IndentPartDownload") & "\" & _filename  '-- Destination file

        Dim _connected As Boolean = False
        Dim _success As Boolean = False
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate
        'get checked in grid
        For Each item As DataGridItem In dtgIndentPart.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If chk.Checked Then
                If Not _connected Then
                    imp = New SAPImpersonate(_user, _password, _webServer)
                    Dim finfo As New FileInfo(_destFile)
                    Try
                        _success = imp.Start()
                        If _success Then
                            If Not finfo.Directory.Exists Then
                                Directory.CreateDirectory(finfo.DirectoryName)
                            End If
                            SW = New StreamWriter(_destFile)
                            _connected = True
                        End If
                    Catch ex As Exception
                        Throw ex
                        Exit Sub
                    End Try
                End If
                iph = New IndentPartHeader
                Dim ipFacade As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)
                iph = ipFacade.Retrieve(CInt(item.Cells(0).Text))

                SW.WriteLine("H:" & iph.Dealer.DealerCode & ";" & iph.MaterialTypeDesc & ";" & iph.RequestNo & ";" & iph.RequestDate.ToString("dd/MM/yyyy") & ";" & iph.TotalQuantity.ToString)

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.ID", MatchType.Exact, iph.ID))

                ipd = New ArrayList
                ipd = New IndentPartDetailFacade(User).Retrieve(criterias)

                If ipd.Count > 0 Then
                    For Each itemD As IndentPartDetail In ipd
                        SW.WriteLine("D:" & itemD.IndentPartHeader.Dealer.DealerCode & ";" & itemD.IndentPartHeader.MaterialTypeDesc & ";" & itemD.IndentPartHeader.RequestNo & _
                        ";" & itemD.IndentPartHeader.RequestDate & ";" & itemD.SparePartMaster.PartNumber & ";" & itemD.Qty)
                    Next
                End If
            End If
        Next
        If _success Then
            SW.Close()
            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("IndentPartDownload") & "\" & _filename
            imp.StopImpersonate()
            imp = Nothing
            Response.Redirect("../Download.aspx?file=" & PathFile)
        Else
            MessageBox.Show("Daftar Indent Part belum dipilih")
        End If
    End Sub


    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim IpFacade As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)
        Dim arlToSubmit As ArrayList = New ArrayList
        For Each item As DataGridItem In dtgIndentPart.Items
            Dim chkItemChecked As CheckBox = item.FindControl("chkItemChecked")

            If chkItemChecked.Checked Then
                Dim lbtnDetails As LinkButton = item.FindControl("lbtnDetails")
                Dim obj As IndentPartHeader = IpFacade.Retrieve(CInt(lbtnDetails.CommandArgument))
                arlToSubmit.Add(obj)
            End If

        Next

        If arlToSubmit.Count = 0 Then
            MessageBox.Show("Tidak ada data yang dipilih")
            Return
        End If

        Dim FOLDER_NAME As String = KTB.DNET.Lib.WebConfig.GetValue("DNetServerFolder") & "IndentPart" '& txtPONumber.Text.Substring(1, 4)
        Dim FILE_NAME As String = "" '= FOLDER_NAME + "\E" + txtPONumber.Text + IIf(ddlOrderType.SelectedValue = "E", ".EOD", ".DAT")

        Dim _user As String = KTB.DNET.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNET.Lib.WebConfig.GetValue("Password")
        Dim _sapServer As String = KTB.DNET.Lib.WebConfig.GetValue("SAPServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _sapServer)
        Dim succes As Boolean = False
        Dim msg As String = String.Empty


        succes = imp.Start()

        If succes Then


            Dim counter As Integer = 0
            Dim result As Integer = 0
            For Each item As IndentPartHeader In arlToSubmit
                counter += 1
                item.SubmitFile = "request"
                result = IpFacade.Update(item)
                Dim objInserted As IndentPartHeader = IpFacade.Retrieve(item.ID)
                FILE_NAME = FOLDER_NAME + "\" + objInserted.SubmitFile
                CreateFolder(FOLDER_NAME)
                Dim fs As FileStream = New FileStream(FILE_NAME, FileMode.CreateNew)
                Dim w As StreamWriter = New StreamWriter(fs)

                WritePOHeaderToFile(w, item)
                WritePODetailToFile(w, item)

                w.Close()
                fs.Close()


            Next

            imp.StopImpersonate()
            imp = Nothing

            MessageBox.Show(arlToSubmit.Count.ToString & " file berhasil dibuat")

        Else
            MessageBox.Show("Proses Gagal, Login Server SAP Failed")
        End If



        'Try
        '    succes = imp.Start()
        '    If succes Then
        '        Dim objPO As SparePartPO = CType(Session("sessPOHeader"), SparePartPO)
        '        objPO.ProcessCode = "S"
        '        Dim nResult As Integer = New SparePartPOFacade(User).UpdateSPPOProcessCode(objPO)
        '        If nResult <> -1 Then
        '            CreateFolder(FOLDER_NAME)
        '            If File.Exists(FILE_NAME) Then
        '                File.Delete(FILE_NAME)
        '            End If
        '            Dim fs As FileStream = New FileStream(FILE_NAME, FileMode.CreateNew)
        '            Dim w As StreamWriter = New StreamWriter(fs)
        '            WritePOHeaderToFile(w)
        '            WritePODetailToFile(w)
        '            w.Close()
        '            fs.Close()
        '            imp.StopImpersonate()
        '            imp = Nothing
        '            msg = ChangeSPPOStatus("S")
        '            MessageBox.Show(msg)
        '        Else
        '            MessageBox.Show("Proses tidak berhasil coba beberapa saat lagi.")
        '        End If

        '    Else
        '        MessageBox.Show("Gagal Login ke SAP Server.")
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(SR.DataSendFail)
        'End Try

    End Sub

    Private Sub CreateFolder(ByVal folderName As String)
        Dim dirInfo As DirectoryInfo = New DirectoryInfo(folderName)
        If Not dirInfo.Exists Then
            dirInfo.Create()
        End If
    End Sub


    Private Sub WritePOHeaderToFile(ByRef w As StreamWriter, ByVal objHeader As IndentPartHeader)
        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim pad As Char = " "
        sbSetARecord.Append("T")
        sbSetARecord.Append(objHeader.RequestNo.PadRight(15, pad))
        sbSetARecord.Append(Left(objHeader.Dealer.DealerName, 25).PadRight(25, pad))
        sbSetARecord.Append(String.Format("{0:yyyyMMdd}", objHeader.RequestDate))
        sbSetARecord.Append(objHeader.IndentPartDetails.Count.ToString.PadLeft(4, "0"))
        w.WriteLine(sbSetARecord.ToString)
    End Sub

    Private Function WritePODetailToFile(ByRef w As StreamWriter, ByVal objHeader As IndentPartHeader)
        Dim _arlPODetail As ArrayList = objHeader.IndentPartDetails
        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim pad As Char = " "
        Dim indek As Integer = 0
        For Each objPODetail As IndentPartDetail In _arlPODetail
            indek = indek + 1
            sbSetARecord.Remove(0, sbSetARecord.Length)
            sbSetARecord.Append("D")
            sbSetARecord.Append(objPODetail.IndentPartHeader.RequestNo.PadRight(15, pad))
            sbSetARecord.Append(objPODetail.SparePartMaster.PartNumber.PadRight(20, pad))
            sbSetARecord.Append(objPODetail.Qty.ToString.PadLeft(5, "0"))
            sbSetARecord.Append(String.Format("{0:yyyyMMdd}", objPODetail.IndentPartHeader.RequestDate)) '(objPODetail.SparePartPO.PODate.ToString.Format("{0:yyyyMMdd}"))
            sbSetARecord.Append(indek.ToString.PadLeft(4, "0"))
            w.WriteLine(sbSetARecord.ToString)
        Next
    End Function


    Private Sub dtgIndentPart_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgIndentPart.PageIndexChanged
        dtgIndentPart.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgIndentPart.CurrentPageIndex)
    End Sub


    Private Function getGrandTotal(criterias As CriteriaComposite) As String
        Dim _ret As Decimal = 0
        Dim ListSPPO As ArrayList = New IndentPartHeaderFacade(User).Retrieve(criterias)
        For Each spPO As IndentPartHeader In ListSPPO
            Dim OrderedAmount As Decimal = 0
            Dim arlDetail As ArrayList = spPO.IndentPartDetails
            For Each spPODet As IndentPartDetail In arlDetail
                _ret += spPODet.Price
            Next
        Next
        Return _ret.ToString("#,##0")
    End Function
End Class

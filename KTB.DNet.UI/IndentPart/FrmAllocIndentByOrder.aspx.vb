#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.IndentPart
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports System.IO
Imports System.Text
Imports System.Linq
Imports System.Data.DataSetExtensions
#End Region

Public Class FrmAllocIndentByOrder
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoPO As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgIndentPart As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlMaterialType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSearchProduk As System.Web.UI.WebControls.Label
    Protected WithEvents txtSparePartName As System.Web.UI.WebControls.TextBox
    Protected WithEvents icPODateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPODateUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblPopUpPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents Textbox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGeneratePO As System.Web.UI.WebControls.Button
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label

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

    'Dim IPAD As IndentPartAllocationDetail
    'Dim IPAH As IndentPartAllocationHeader
    'Dim IPAHfcd As IndentPartAllocationHeaderFacade
    Private _sesshelper As New SessionHelper
    Private _arlIPAD As ArrayList = New ArrayList
    Private _arlIPDetail As ArrayList = New ArrayList
    Public isTOPdisplayed As Boolean = False
    ' Dim _IPAHID As Integer
    'Dim _state As Boolean
    Private _arlDetailProcess As ArrayList
    Private idxCaraPembayaran As Integer = 11

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        Dim IsIndentPartEquip As Boolean = False
        If Not IsNothing(Request.QueryString("MaterialType")) AndAlso Request.QueryString("MaterialType") = "4" Then
            IsIndentPartEquip = True
            isTOPdisplayed = False
            dtgIndentPart.Columns(idxCaraPembayaran).Visible = False
        End If

        If Not IsIndentPartEquip Then
            If Not SecurityProvider.Authorize(Context.User, SR.AlocationPemesananView_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=INDENT PART - Alokasi Pemesanan")
            End If
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.Lihat_alokasi_order_indent_part_equipment_privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=INDENT PART EQUIPMENT- Alokasi Pemesanan")
            End If
        End If
        'If IsIndentPartEquip Then 'Not Necessary
        '    If SecurityProvider.Authorize(context.User, SR.Lihat_alokasi_order_indent_part_equipment_privilege) Then
        '        btnDownload.Visible = True
        '        btnGeneratePO.Visible = True
        '    Else
        '        btnDownload.Visible = False
        '        btnGeneratePO.Visible = False
        '    End If
        'End If
    End Sub

    Dim bCekGridQtyPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.AlocationPemesananCreate_Privilege)
    Dim bCekDownloadPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.AlocationPemesananCreate_Privilege)

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Page.Server.ScriptTimeout = 300
        dtgIndentPart.Columns(idxCaraPembayaran).Visible = True
        isTOPdisplayed = True
        InitiateAuthorization()
        If Not IsPostBack Then
            If (Not IsNothing(Request.QueryString("MaterialType"))) Then
                'ddlMaterialType.Items.Add(New ListItem("", ""))
                'ddlMaterialType.Items.Add(New ListItem(EnumMaterialType.MaterialType.Equipment.ToString(), EnumMaterialType.MaterialType.Equipment))
                'ddlMaterialType.SelectedIndex = 1
                'ddlMaterialType.Enabled = False
                lblPopUpPengajuan.Attributes("onclick") = "showPopUp('../PopUp/PopUpIndentPart.aspx?MaterialType=" & EnumMaterialType.MaterialType.Equipment & "','', 500, 600,PONOSelection);"
                dtgIndentPart.Columns(idxCaraPembayaran).Visible = False
                isTOPdisplayed = False
            Else
                dtgIndentPart.Columns(idxCaraPembayaran).Visible = True
                'BindMaterialType()
                isTOPdisplayed = True
                lblPopUpPengajuan.Attributes("onClick") = "ShowPPIndent();"
            End If
            'lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            'lblSearchProduk.Attributes("OnClick") = "ShowPPSparePartSelection();"

            ViewState("currSortColumn") = "IndentPartHeader.Dealer.DealerCode"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            BindIPAD(_arlIPAD)

            'cek privilege
            dtgIndentPart.Columns(8).Visible = bCekGridQtyPriv
            'Button1.Enabled = bCekGridQtyPriv
            btnGeneratePO.Enabled = bCekGridQtyPriv
            btnDownload.Enabled = bCekDownloadPriv
        End If
    End Sub

    Private Sub BindIPAD(ByVal arlst As ArrayList)
    End Sub

    Private Sub BindMaterialType()
        Dim arl As ArrayList = New EnumMaterialType().RetrieveType()
        ddlMaterialType.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
        For Each imat As EnumMaterial In arl
            ddlMaterialType.Items.Add(New ListItem(imat.NameType, imat.ValType.ToString))
        Next
        ddlMaterialType.SelectedIndex = -1
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ViewState("SortColumn") = "IndentPartHeader.Dealer.DealerCode"
        ViewState("SortDirection") = Sort.SortDirection.ASC
        If txtNoPO.Text <> "" Then
            BindToGrid(0)
        End If
    End Sub
    Private Sub ValidateDealerCode()
        Dim str() As String = Me.txtDealerCode.Text.Trim.Split(Environment.NewLine)
        Dim sValid As String = String.Empty
        Dim i As Integer
        Dim sTemp As String

        If str.Length > 0 Then
            For i = 0 To str.Length - 1
                If str(i).Trim <> String.Empty Then
                    sTemp = IIf(sValid.Trim = String.Empty, "", ";") & str(i).Trim
                    sTemp = sTemp.Replace(";;", ";")
                    sValid &= sTemp
                End If
            Next
        End If
        If sValid <> String.Empty Then
            Me.txtDealerCode.Text = sValid
        End If
    End Sub

    Private Sub ValidateMatNumber()
        Dim str() As String = Me.txtSparePartName.Text.Trim.Split(Environment.NewLine)
        Dim sValid As String = String.Empty
        Dim i As Integer
        Dim sTemp As String

        If str.Length > 0 Then
            For i = 0 To str.Length - 1
                If str(i).Trim <> String.Empty Then
                    sTemp = IIf(sValid.Trim = String.Empty, "", ";") & str(i).Trim
                    sTemp = sTemp.Replace(";;", ";")
                    sValid &= sTemp
                End If
            Next
        End If
        If sValid <> String.Empty Then
            Me.txtSparePartName.Text = sValid
        End If
    End Sub

    Private Sub BindToGrid(ByVal currentPageIndex As Integer)
        'If icPODateFrom.Value > icPODateUntil.Value Then
        '    MessageBox.Show("Tanggal PO 'Dari' tidak boleh lebih Besar dari Tanggal PO 'Sampai' ")
        '    Exit Sub
        'End If
        'ValidateDealerCode()
        'ValidateMatNumber()
        Dim total As Integer = 0


        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(IndentPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.StatusKTB", MatchType.GreaterOrEqual, CInt(EnumIndentPartStatus.IndentPartStatusKTB.Baru)))
        'criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.StatusKTB", MatchType.LesserOrEqual, CInt(EnumIndentPartStatus.IndentPartStatusKTB.Rilis)))

        'Bug 1088
        If (Not IsNothing(Request.QueryString("MaterialType"))) Then
            criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.MaterialType", MatchType.Exact, CInt(EnumMaterialType.MaterialType.Equipment)))
            'criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.PaymentType", MatchType.Exact, CInt(EnumIndentPartStatus.IndentPartPaymentType.Deposit_C)))

            Dim statusktb As String = String.Format("({0},{1})", CInt(EnumIndentPartStatus.IndentPartStatus.ORDER_TO_VENDOR), CInt(EnumIndentPartStatus.IndentPartStatus.ALOKASI_SEBAGIAN))
            'criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.Status", MatchType.InSet, statusktb))
            criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.StatusKTB", MatchType.InSet, statusktb))
            lblTitle.Text = "INDENT PART EQUIPMENT - Alokasi Indent Part Equipment"
        Else
            'criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.MaterialType", MatchType.Exact, CInt(EnumMaterialType.MaterialType.Tools)), "((", True)
            criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.PaymentType", MatchType.Exact, CInt(EnumIndentPartStatus.IndentPartPaymentType.Deposit_C)), "(((", True)
            criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.StatusKTB", MatchType.Exact, CInt(EnumIndentPartStatus.IndentPartStatusKTB.Dealer_Konfirmasi)), ")", False)
            criterias.opOr(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.PaymentType", MatchType.Exact, CInt(EnumIndentPartStatus.IndentPartPaymentType.Deposit_B)), "(", True)
            criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.StatusKTB", MatchType.Exact, CInt(EnumIndentPartStatus.IndentPartStatusKTB.Rilis)), "))", False)
            criterias.opOr(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.MaterialType", MatchType.No, CInt(EnumMaterialType.MaterialType.Tools)), "(", True)
            criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.StatusKTB", MatchType.Exact, CInt(EnumIndentPartStatus.IndentPartStatusKTB.Proses)), "))", False)
        End If

        'Dealer 2 Level Criteria
        'If txtDealerCode.Text <> "" Then
        'Dim iPHeaderIdStr As String = ""
        '    'Dim arlIPHeader As ArrayList

        '    'Dim criteriaIPHeader As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "Dealer.DealerCode", MatchType.InSet, "('" & Replace(txtDealerCode.Text, ";", "','") & "')"))
        '    'arlIPHeader = New IndentPartHeaderFacade(User).Retrieve(criteriaIPHeader)

        '    'For Each itemIPHeader As IndentPartHeader In arlIPHeader
        '    '    iPHeaderIdStr = iPHeaderIdStr & itemIPHeader.ID.ToString & ","
        '    'Next

        '    'If iPHeaderIdStr <> "" Then
        '    '    iPHeaderIdStr = Left(iPHeaderIdStr, iPHeaderIdStr.Length - 1)
        '    '    criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.ID", MatchType.InSet, "(" & iPHeaderIdStr & ")"))
        '    'End If

        '    'Dim sDealer As String = Me.txtDealerCode.Text
        '    'sDealer = sDealer.Replace(";", ",")
        '    'criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(",", "','") & "')"))
        '    criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','").Trim() & "')"))
        'End If

        'No Pengajuan
        If txtNoPO.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.RequestNo", MatchType.InSet, "('" & Replace(txtNoPO.Text, ";", "','") & "')"))
        End If

        'Tgl Pengajuan
        'criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.RequestDate", MatchType.GreaterOrEqual, icPODateFrom.Value))
        'criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.RequestDate", MatchType.LesserOrEqual, icPODateUntil.Value.AddDays(1)))

        'Tipe Barang
        'Dim objUserInfo As UserInfo = _sesshelper.GetSession("LOGINUSERINFO")
        'If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
        '    If ddlMaterialType.SelectedIndex > 0 Then
        '        criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.MaterialType", MatchType.Exact, ddlMaterialType.SelectedValue))
        '    Else
        '        MessageBox.Show("Tipe barang belum dipilih")
        '        Exit Sub
        '    End If
        'End If


        'bug 1833
        'If txtSparePartName.Text.Trim <> "" Then
        '    Dim sTemp As String = Me.txtSparePartName.Text.Trim
        '    'Dim sOK As String = String.Empty
        '    'Dim i As Integer

        '    'For i = 0 To sTemp.Length - 1
        '    '    If Asc(sTemp.Substring(i, 1)) = 13 OrElse Asc(sTemp.Substring(i, 1)) = 10 Then
        '    '    Else
        '    '        sOK &= sTemp.Substring(i, 1)
        '    '    End If
        '    'Next

        '    Dim strParts As String = ""
        '    'For Each str As String In sOK.Split(";") ' txtSparePartName.Text.Trim.Split(";")
        '    '    strParts &= IIf(strParts.Trim = "", "", ",") & "'" & str & "'"
        '    'Next

        '    sTemp = sTemp.Replace(";", ",")
        '    sTemp = sTemp.Replace(",", "','")
        '    strParts = sTemp
        '    If strParts <> String.Empty Then
        '        criterias.opAnd(New Criteria(GetType(IndentPartDetail), "SparePartMaster.PartNumber", MatchType.InSet, "('" & strParts & "')"))

        '    End If
        'End If

        Dim totalRow As Integer = 0
        If (currentPageIndex >= 0) Then
            Dim dtgIndentPartPage As Integer = 0

            Dim _originalArlIPDetail As ArrayList = New ArrayList()
            _originalArlIPDetail = New IndentPartDetailFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgIndentPart.PageSize, totalRow, ViewState("currSortColumn"), ViewState("SortDirection"))
            If _originalArlIPDetail.Count = 0 Then
                _originalArlIPDetail = New IndentPartDetailFacade(User).RetrieveActiveList(criterias, currentPageIndex, dtgIndentPart.PageSize, totalRow, ViewState("currSortColumn"), ViewState("SortDirection"))
                If _originalArlIPDetail.Count = 0 Then
                    currentPageIndex = 0
                Else
                    If currentPageIndex > 0 Then
                        currentPageIndex = currentPageIndex - 1
                    End If
                End If
            End If
            'Dim result = New System.Collections.ArrayList((From item As IndentPartDetail In _originalArlIPDetail.OfType(Of IndentPartDetail)()
            '        Where item.SisaQty > 0
            '        Select item).ToList())

            'Show data only if reminingQty(sisaQty) is zero
            _arlIPDetail = _originalArlIPDetail 'result 
            _sesshelper.SetSession("arlgrid", _arlIPDetail)
            dtgIndentPart.CurrentPageIndex = currentPageIndex
            dtgIndentPart.DataSource = _arlIPDetail
            dtgIndentPart.VirtualItemCount = totalRow
            dtgIndentPart.DataBind()
        End If

    End Sub

    Private Sub dtgIndentPart_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgIndentPart.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            Dim oID As IndentPartDetail = CType(e.Item.DataItem, IndentPartDetail)
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgIndentPart.PageSize * dtgIndentPart.CurrentPageIndex)).ToString
            Dim lblPO As Label = CType(e.Item.FindControl("lblPO"), Label)

            If oID.IndentPartPOs.Count > 0 Then
                Dim strPO As String = ""
                For Each item As IndentPartPO In oID.IndentPartPOs
                    strPO = strPO & item.SparePartPODetail.SparePartPO.ID.ToString & ","
                Next

                If strPO <> "" Then
                    strPO = Left(strPO, strPO.Length - 1)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "ID", MatchType.InSet, "(" & strPO & ")"))

                    Dim arlPO As ArrayList = New SparePartPOFacade(User).Retrieve(criterias)

                    Dim strToDisplay As String = ""
                    For Each itemPO As SparePartPO In arlPO
                        strToDisplay = strToDisplay & itemPO.PONumber & ";"
                    Next

                    lblPO.Text = Replace(Left(strToDisplay, strToDisplay.Length - 1), ";", "<br>")
                End If

            End If

            Dim lblOrderQty As Label = CType(e.Item.FindControl("lblOrderQty"), Label)
            Dim lblRemainQty As Label = CType(e.Item.FindControl("lblRemainQty"), Label)
            Dim hdnRemainQty As HtmlInputHidden = CType(e.Item.FindControl("hdnRemainQty"), HtmlInputHidden)
            'Dim chkPO As CheckBox = e.Item.FindControl("chkPO")
            'Dim txtQtyAllocation As TextBox = CType(e.Item.FindControl("txtQtyAllocation"), TextBox)
            Dim lblQtyAllocation As Label = CType(e.Item.FindControl("lblQtyAllocation"), Label)
            'txtQtyAllocation.Attributes.Add("onblur", String.Format("javascript:NumOnlyBlurWithOnGridTxtCustom('{0}','{1}','{2}','{3}');", txtQtyAllocation.ClientID, lblRemainQty.ClientID, lblOrderQty.ClientID, hdnRemainQty.ClientID))
            'If (oID.SisaQty = 0) Then
            '    txtQtyAllocation.Enabled = False
            '    chkPO.Enabled = False
            'Else
            '    txtQtyAllocation.Text = oID.AllocationQty.ToString()
            'End If

            'handle for old data if remining quantity != order qty
            If oID.SisaQty = oID.Qty Then
                lblQtyAllocation.Text = oID.Qty
            Else
                lblQtyAllocation.Text = oID.SisaQty
            End If


            'If (Not IsNothing(Request.QueryString("MaterialType"))) Then
            '    Dim lblConfirmDate As Label = CType(e.Item.FindControl("lblConfirmDate"), Label)
            '    Dim arl_objEqPo As EstimationEquipPO = New KTB.DNet.BusinessFacade.IndentPartEquipment.EstimationEquipPOFacade(User).RetrieveByIndentPartDetailID(oID.ID)

            'End If
        End If

    End Sub

    Private Sub dtgIndentPart_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgIndentPart.SortCommand
        'ViewState("currSortColumn"), viewstate("SortDirection")
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("SortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("SortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("SortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("SortDirection") = Sort.SortDirection.ASC
        End If
        BindToGrid(dtgIndentPart.CurrentPageIndex)
    End Sub

    Private Sub dtgIndentPart_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgIndentPart.ItemCommand

        If e.CommandName = "Edit" Then
            dtgIndentPart.EditItemIndex = e.Item.ItemIndex
            BindToGrid(dtgIndentPart.CurrentPageIndex)
        End If

        If e.CommandName = "Cancel" Then
            dtgIndentPart.EditItemIndex = -1
            BindToGrid(dtgIndentPart.CurrentPageIndex)
        End If

        If e.CommandName = "Save" Then

            'Cek Sisa
            Dim lblRemain As Label = e.Item.FindControl("lblRemainQty")
            Dim lblTxtAlokasi As TextBox = e.Item.FindControl("txtQtyAllocation")
            If CInt(Val(lblTxtAlokasi.Text)) > CInt(lblRemain.Text) Then
                MessageBox.Show("Quantity Alokasi Tidak Boleh Melebihi Qty Sisa")
                Exit Sub
            End If

            Dim facadeIPDetail As IndentPartDetailFacade = New IndentPartDetailFacade(User)
            Dim objIndentPartDetail As IndentPartDetail = facadeIPDetail.Retrieve(CInt(e.CommandArgument))
            objIndentPartDetail.AllocationQty = CInt(Val(lblTxtAlokasi.Text))
            Dim result = facadeIPDetail.Update(objIndentPartDetail)
            If result = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If

            dtgIndentPart.EditItemIndex = -1
            BindToGrid(dtgIndentPart.CurrentPageIndex)


        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case CType(ViewState("vsAccess"), String)

        End Select
    End Sub

    Private Sub btnGeneratePO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGeneratePO.Click
        If dtgIndentPart.EditItemIndex > 0 Then
            MessageBox.Show("Simpan Data Terlebih Dulu")
            Return
        End If

        Dim objIPDetailfacade As IndentPartDetailFacade = New IndentPartDetailFacade(User)
        Dim idIPdetail As String = ""
        Dim arlToUpdate As ArrayList = New ArrayList()
        Dim arlIPDetail As ArrayList = New ArrayList()

        If txtNoPO.Text <> String.Empty Then
            If dtgIndentPart.Items.Count > 0 Then
                Dim RequestNo As String
                Dim ArrRequestNo As Array = txtNoPO.Text.Split(";")
                For Each ReqNo As String In ArrRequestNo
                    RequestNo += "'" + ReqNo + "',"
                Next
                If RequestNo.Length > 0 Then
                    RequestNo = Left(RequestNo, RequestNo.Length - 1)
                End If

                Dim criteriasIPH As CriteriaComposite
                criteriasIPH = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasIPH.opAnd(New Criteria(GetType(IndentPartHeader), "RequestNo", MatchType.InSet, "(" & RequestNo & ")"))

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
            Else
                MessageBox.Show("Silahkan klik cari untuk cek detail terlebih dahulu")
                Exit Sub
            End If

            'For Each item As DataGridItem In dtgIndentPart.Items
            'Dim chk As CheckBox = item.FindControl("chkPO")
            'If chk.Checked Then
            'Dim lblQtyAllocation As Label = item.FindControl("lblQtyAllocation")
            'Dim lblOrderQty As Label = item.FindControl("lblOrderQty")

            'If Val(lblQtyAllocation.Text) = 0 Then
            '    MessageBox.Show("Alokasi 0 Tidak Dapat Dibuat PO")
            '    Exit Sub
            'End If

            'Dim lbtnEdit As LinkButton = item.FindControl("lbtnEdit")
            'If Val(txtQtyAllocation.Text) <> CType(New IndentPartDetailFacade(User).Retrieve(CInt(lbtnEdit.CommandArgument)), IndentPartDetail).AllocationQty Then
            '    MessageBox.Show("Klik simpan dulu.")
            '    Exit Sub
            'End If

            'mod by ery
            'If IsValidQty(CInt(lblOrderQty.Text), CInt(lblQtyAllocation.Text)) = False Then
            '    MessageBox.Show("Alokasi melebihi Remain Qty tidak dapat dibuat PO")
            '    Exit Sub
            'End If

            '    idIPdetail = idIPdetail & lbtnEdit.CommandArgument & ","

            'End If

            'object to be updated allocQty'
            'Dim obj As IndentPartDetail = CType(_sesshelper.GetSession("arlgrid"), ArrayList)(item.ItemIndex)
            'obj.AllocationQty = Val(lblQtyAllocation.Text)
            'arlToUpdate.Add(obj)
            'Next

            'Update allocQty data
            Dim result As Integer = New IndentPartDetailFacade(User).UpdateAlokasi(arlToUpdate)

            If result > 0 Then
                Dim Retval As String = objIPDetailfacade.GeneratePO(arlToUpdate) '(arlIPDetail')
                If Retval <> "" Then
                    MessageBox.Show("Generate PO Dengan No " & Retval & " Berhasil")
                    BindToGrid(dtgIndentPart.CurrentPageIndex)
                End If
            Else
                MessageBox.Show("Gagal update data")
            End If
        Else
            MessageBox.Show("Silahkan Pilih Nomor Pengajuan terlebih dahulu")
            Exit Sub
        End If
    End Sub

    Private Sub SetFinishedStatus(ByVal arlDetail As ArrayList)

        Dim IDHeader As String = ""
        For Each item As IndentPartDetail In arlDetail
            IDHeader &= item.IndentPartHeader.ID.ToString & ","
        Next

        If IDHeader <> "" Then
            'Get One Header Each
            IDHeader = Left(IDHeader, IDHeader.Length - 1)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "ID", MatchType.InSet, "(" & IDHeader & ")"))
            Dim arlHeader As ArrayList = New IndentPartHeaderFacade(User).Retrieve(criterias)

            Dim result As Integer = 0
            Dim headerFacade As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)
            For Each ItemHeader As IndentPartHeader In arlHeader
                If ItemHeader.SisaQty = 0 Then
                    ItemHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Selesai
                    ItemHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Selesai
                    result = headerFacade.Update(ItemHeader)
                End If
            Next
        End If

    End Sub

    Private Function IsValidQty(ByVal qty As Integer, ByVal alokasi As Integer) As Boolean
        If ((qty - alokasi) < 0) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim arlToUpdate As ArrayList = New ArrayList
        For Each item As DataGridItem In dtgIndentPart.Items
            Dim txtQtyAllocation As TextBox = item.FindControl("txtQtyAllocation")
            Dim lblOrderQty As Label = item.FindControl("lblOrderQty")
            Dim lblRemainQty As Label = item.FindControl("lblRemainQty")
            Dim chkPO As CheckBox = item.FindControl("chkPO")

            If (chkPO.Checked) Then
                Dim obj As IndentPartDetail = CType(_sesshelper.GetSession("arlgrid"), ArrayList)(item.ItemIndex)
                Dim objFromDb As IndentPartDetail = New IndentPartDetailFacade(User).Retrieve(obj.ID)
                If IsValidQty(CInt(objFromDb.SisaQty), CInt(Val(txtQtyAllocation.Text))) = False Then
                    MessageBox.Show("Quantity Alokasi Tidak Boleh Melebihi Qty Sisa")
                    Exit Sub
                Else
                    obj.AllocationQty = Val(txtQtyAllocation.Text)
                    arlToUpdate.Add(obj)
                End If
            End If
        Next

        If arlToUpdate.Count = 0 Then
            MessageBox.Show("Tidak Ada Data")
            Exit Sub
        End If

        Dim result As Integer = New IndentPartDetailFacade(User).UpdateAlokasi(arlToUpdate)

        MessageBox.Show(SR.SaveSuccess)

    End Sub

    Private Sub dtgIndentPart_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgIndentPart.PageIndexChanged
        dtgIndentPart.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgIndentPart.CurrentPageIndex)
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As ArrayList = New ArrayList
        Dim strIPH_id As String
        Dim couItemSelected As Integer = 0
        Dim arlIPDetail As ArrayList
        Dim objIPDetailfacade As IndentPartDetailFacade = New IndentPartDetailFacade(User)
        If txtNoPO.Text <> String.Empty Then
            If dtgIndentPart.Items.Count > 0 Then
                Dim RequestNo As String
                Dim ArrRequestNo As Array = txtNoPO.Text.Split(";")
                For Each ReqNo As String In ArrRequestNo
                    RequestNo += "'" + ReqNo + "',"
                Next
                If RequestNo.Length > 0 Then
                    RequestNo = Left(RequestNo, RequestNo.Length - 1)
                End If

                Dim criteriasIPH As CriteriaComposite
                criteriasIPH = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasIPH.opAnd(New Criteria(GetType(IndentPartHeader), "RequestNo", MatchType.InSet, "(" & RequestNo & ")"))

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
                    For Each arrIPD As IndentPartDetail In arlIPDetail
                        arrIPD.AllocationQty = arrIPD.Qty
                        arrData.Add(arrIPD)
                    Next
                End If
            Else
                MessageBox.Show("Silahkan klik cari untuk cek detail terlebih dahulu")
                Exit Sub
            End If
        End If

        'For Each item As DataGridItem In dtgIndentPart.Items
        '    'Dim chkItemChecked As CheckBox = CType(item.FindControl("chkPO"), CheckBox)

        '    'If chkItemChecked.Checked Then
        '    Dim IPD_ID As Integer = 0
        '    IPD_ID = CInt(item.Cells(0).Text)
        '    If IPD_ID > 0 Then
        '        Dim grid_IndentPartDetail As IndentPartDetail = New IndentPartDetail
        '        grid_IndentPartDetail = New IndentPartDetailFacade(User).Retrieve(IPD_ID)
        '        arrData.Add(grid_IndentPartDetail)
        '    End If

        '    'End If
        'Next
        If arrData.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        Else
            DoDownload(arrData)
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "IndentPartOrder" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

        Dim IndentPartOrderData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(IndentPartOrderData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(IndentPartOrderData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteIndentPartOrderData(sw, data)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteIndentPartOrderData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Indent Part Order")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Nomor Pengajuan" & tab)
            itemLine.Append("Tangal Pengajuan" & tab)
            itemLine.Append("Nomor Barang" & tab)
            itemLine.Append("Nama Barang" & tab)
            itemLine.Append("Model" & tab)
            itemLine.Append("Order Quantity" & tab)
            itemLine.Append("Remind Quanti" & tab)
            If IsNothing(Request.QueryString("MaterialType")) AndAlso Request.QueryString("MaterialType") <> "4" Then
                itemLine.Append("Cara Pembayaran" & tab)
            End If

            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each item As IndentPartDetail In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                itemLine.Append(item.IndentPartHeader.Dealer.DealerCode & tab)
                itemLine.Append(item.IndentPartHeader.RequestNo & tab)
                itemLine.Append(item.IndentPartHeader.RequestDate.ToString("dd/MM/yyyy") & tab)
                itemLine.Append(item.SparePartMaster.PartNumber & tab)
                itemLine.Append(item.SparePartMaster.PartName & tab)
                itemLine.Append(item.SparePartMaster.ModelCode & tab)
                itemLine.Append(item.Qty & tab)
                itemLine.Append(item.SisaQty & tab)
                If IsNothing(Request.QueryString("MaterialType")) AndAlso Request.QueryString("MaterialType") <> "4" Then
                    If Not IsNothing(item.IndentPartHeader.TermOfPayment) Then
                        itemLine.Append(item.IndentPartHeader.TermOfPayment.Description & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                End If

                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next

        End If
    End Sub
End Class
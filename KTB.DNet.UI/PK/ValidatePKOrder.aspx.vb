#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
#End Region

Public Class ValidatePKOrder
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label18 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents Label19 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPurpose As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnValidate As System.Web.UI.WebControls.Button
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents tblOperator As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPKNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlRencanaPenebusan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dgListPK As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnTransferData As System.Web.UI.WebControls.Button
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblspaNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblspaDate As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lboxStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents RowPerhatian As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblQuantity As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents Label20 As System.Web.UI.WebControls.Label


    Protected WithEvents txtDealerBranchCode As TextBox
    Protected WithEvents lblDealerBranch As Label
    Protected WithEvents lblPopUpDealerBranch As Label
    Protected WithEvents txtBranchName As TextBox
    Protected WithEvents hdnTitle As HiddenField

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
    Private arlPK As ArrayList
    Private sessionHelper As New sessionHelper
    Private objDealer As Dealer
#End Region

#Region "Custom Method"

    Private Sub SetSessionCriteria()
        Dim objSSPO As ArrayList = New ArrayList
        objSSPO.Add(txtPKNumber.Text)
        objSSPO.Add(ddlOrderType.SelectedIndex)
        objSSPO.Add(GetSelectedItem(lboxStatus))
        objSSPO.Add(ddlRencanaPenebusan.SelectedIndex)
        objSSPO.Add(ddlCategory.SelectedIndex)
        objSSPO.Add(ddlPurpose.SelectedIndex)
        objSSPO.Add(dgListPK.CurrentPageIndex)
        objSSPO.Add(CType(ViewState("CurrentSortColumn"), String))
        objSSPO.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        sessionHelper.SetSession("SESSIONVALIDATEPKORDER", objSSPO)
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim objSSPO As ArrayList = sessionHelper.GetSession("SESSIONVALIDATEPKORDER")
        If Not objSSPO Is Nothing Then
            txtPKNumber.Text = objSSPO.Item(0)
            ddlOrderType.SelectedIndex = objSSPO.Item(1)
            Dim str() As String = objSSPO.Item(2).ToString().Split(",")
            For Each item As ListItem In lboxStatus.Items
                For i As Integer = 0 To str.Length - 1
                    If item.Value.ToString = str(i).ToString Then
                        item.Selected = True
                        Exit For
                    End If
                Next
            Next
            ddlRencanaPenebusan.SelectedIndex = objSSPO.Item(3)
            ddlCategory.SelectedIndex = objSSPO.Item(4)
            ddlPurpose.SelectedIndex = objSSPO.Item(5)
            dgListPK.CurrentPageIndex = objSSPO.Item(6)
            ViewState("CurrentSortColumn") = objSSPO.Item(7)
            ViewState("CurrentSortDirect") = objSSPO.Item(8)
            Return True
        End If
        Return False
    End Function


    Private Function GetSelectedItem(ByVal listboxStatus As ListBox) As String
        Dim _strStatus As String = String.Empty
        For Each item As ListItem In listboxStatus.Items
            If item.Selected Then
                If _strStatus = String.Empty Then
                    _strStatus = item.Value
                Else
                    _strStatus = _strStatus & "," & item.Value
                End If
            End If
        Next
        Return _strStatus
    End Function

    Private Sub RecordStatusChangeHistory(ByVal arrListPK As ArrayList, ByVal oldStatus As Integer)
        For Each item As PKHeader In arrListPK
            Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
            objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pesanan_Kendaraan), item.PKNumber, oldStatus, item.PKStatus)
        Next
    End Sub

    Private Sub RetrieveMaster()
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        If Not objDealer Is Nothing Then
            'lblDealerCode.Text = objDealer.DealerCode
            'lblDealerCode.Text = objDealer.DealerCode & " / " & objDealer.SearchTerm1
            lblspaNumber.Text = objDealer.SPANumber

            If objDealer.SPADate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                lblspaDate.Text = Nothing
            Else
                lblspaDate.Text = Format(objDealer.SPADate, "dd MMMMMMMMMMMMMMM yyyy")
            End If
            'lblDealerName.Text = objDealer.DealerName
            'lblCity.Text = objDealer.City.CityName
            lboxStatus.SelectedIndex = -1
        End If
    End Sub

    Private Sub BindToddlCategory()


        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim arrayListCategory As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)

        If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
            Dim listitemBlank As New ListItem("Silahkan Pilih", -1)
            ddlCategory.Items.Add(listitemBlank)
        End If

        For Each item As Category In arrayListCategory
            Dim listItem As New ListItem(item.CategoryCode, item.ID)
            If item.CategoryCode = "PC" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    ddlCategory.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "LCV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    ddlCategory.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "CV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    ddlCategory.Items.Add(listItem)
                End If
            End If
        Next

    End Sub

    Private Sub BindToddl()

        Dim listitemBlank As ListItem

        '--DropDownList Kondisi Pesanan

        If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) Then
            listitemBlank = New ListItem("Silahkan Pilih", -1)
            ddlPurpose.Items.Add(listitemBlank)
        End If

        For Each item As ListItem In LookUp.ArrayPurpose
            If item.Text = "Khusus" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKondisiKhusus_Privilege) Then
                    ddlPurpose.Items.Add(item)
                End If
            ElseIf item.Text = "Biasa" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKondisiBiasa_Privilege) Then
                    ddlPurpose.Items.Add(item)
                End If
            End If
        Next
        'ddlPurpose.SelectedValue = -1

        '--DropDownList Jenis Pesanan
        If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) Then
            Dim itemBlank As ListItem = New ListItem("Silahkan Pilih", -1)
            ddlOrderType.Items.Add(itemBlank)
        End If

        For Each item As ListItem In LookUp.ArrayJenisPesanan
            If item.Text = "Bulanan" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKindBulanan_Privilege) Then
                    ddlOrderType.Items.Add(item)
                End If
            ElseIf item.Text = "Tambahan" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKindTambahan_Privilege) Then
                    ddlOrderType.Items.Add(item)
                End If
            End If
        Next
        'ddlOrderType.SelectedValue = -1

        '--DropDownList Rencana Penebusan
        For Each item As ListItem In LookUp.ArraylistMonth(True, 0, 6, DateTime.Now)
            ddlRencanaPenebusan.Items.Add(item)
        Next
        ddlRencanaPenebusan.SelectedValue = Format(DateTime.Now, "MMM yyyy").ToString

    End Sub

    Private Sub BindGrid()
        BindDataGrid(dgListPK.CurrentPageIndex)
        TotalAmount()

        tblOperator.Visible = True
        RowPerhatian.Visible = True
    End Sub

    Private Sub BindDataGrid(ByVal currentPageIndex As Integer)

        Dim total As Integer = 0
        Dim objPK As ArrayList

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If txtKodeDealer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If

        If ddlOrderType.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "OrderType", MatchType.Exact, ddlOrderType.SelectedValue))


        If txtDealerBranchCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "DealerBranch.DealerBranchCode", MatchType.InSet, "('" & txtDealerBranchCode.Text.Replace(";", "','") & "')"))
        End If

        Dim tgl As DateTime = System.Convert.ToDateTime(ddlRencanaPenebusan.SelectedItem.ToString)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RequestPeriodeMonth", MatchType.Exact, CType(tgl.Month, Integer)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RequestPeriodeYear", MatchType.Exact, CType(tgl.Year, Integer)))


        'If (ddlOrderPlan.SelectedValue <> -1) Then
        '    Dim tanggal As DateTime = CType(ddlOrderPlan.SelectedValue, DateTime)
        '    criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PKHeader), "RequestPeriodeMonth", MatchType.Exact, tanggal.Month))
        '    criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PKHeader), "RequestPeriodeYear", MatchType.Exact, tanggal.Year))
        'End If

        If lboxStatus.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "PKStatus", MatchType.InSet, "(" & SelectedStatus & ")"))
        Else
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "PKStatus", MatchType.InSet, "(" & CType(enumStatusPK.Status.Baru, Integer) & "," & CType(enumStatusPK.Status.Validasi, Integer) & "," & CType(enumStatusPK.Status.Batal, Integer) & ")"))
        End If

        If txtPKNumber.Text <> "" Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "DealerPKNumber", MatchType.Exact, txtPKNumber.Text))

        If ddlCategory.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Category.ID", MatchType.Exact, ddlCategory.SelectedValue))

        If ddlPurpose.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Purpose", MatchType.Exact, ddlPurpose.SelectedValue))

        'objPK = New PKHeaderFacade(User).Retrieve(criterias)
        'dgListPK.DataSource = objPK
        'tblOperator.Visible = True

        'If currentPageIndex < 0 Then
        'objPK = New PKHeaderFacade(User).RetrieveByCriteria(criterias, dgListPK.PageCount, dgListPK.PageSize, total)
        'dgListPK.DataSource = objPK
        'Else
        'objPK = New PKHeaderFacade(User).RetrieveByCriteria(criterias, currentPageIndex + 1, dgListPK.PageSize, total)

        objPK = New PKHeaderFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dgListPK.PageSize, _
                        total, CType(ViewState("CurrentSortColumn"), String), _
                        CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        arlPK = New PKHeaderFacade(User).Retrieve(criterias)
        dgListPK.DataSource = objPK

        'End If
        dgListPK.VirtualItemCount = total

        If objPK.Count > 0 Then
            dgListPK.DataBind()
        Else
            dgListPK.DataBind()
            MessageBox.Show("Data Tidak Ditemukan")
        End If

    End Sub

    Private Sub TotalAmount()
        Dim tot As Double = 0
        Dim Qty As Double = 0
        For Each item As PKHeader In arlPK
            tot = tot + item.TotalHargaTebus
            Qty = Qty + item.TotalQuantity
        Next
        lblTotal.Text = FormatNumber(tot, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblQuantity.Text = FormatNumber(Qty, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & " Unit"
    End Sub

    Private Function PopulatePKValidate() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        Dim objPKHeaderFacade As New PKHeaderFacade(User)

        For Each oDataGridItem In dgListPK.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _pk As KTB.Dnet.Domain.PKHeader = New PKHeaderFacade(User).Retrieve(CInt(CType(oDataGridItem.FindControl("lblID"), Label).Text))
                '_pk.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                '_pk.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                '_pk.PKType = oDataGridItem.Cells(1).Text

                'If SecurityProvider.Authorize(Context.User, SR.PK_BiasaValidatePKOrderList_Privilege) Then
                'If _pk.PKType = LookUp.enumPurpose.Biasa Then
                If _pk.PKStatus = enumStatusPK.Status.Baru Then
                    _pk = objPKHeaderFacade.Retrieve(_pk.ID)
                    _pk.PKStatus = status.Validate
                    oExArgs.Add(_pk)
                End If
                'End If
                'ElseIf SecurityProvider.Authorize(Context.User, SR.PK_KhususValidatePKOrderList_Privilege) Then
                'If _pk.PKType = LookUp.enumPurpose.Khusus Then
                '    If _pk.PKStatus = enumStatusPK.Status.Baru Then
                '        _pk = objPKHeaderFacade.Retrieve(_pk.ID)
                '        _pk.PKStatus = status.Validate
                '        oExArgs.Add(_pk)
                '    End If
                'End If
                'End If

            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Function PopulatePKDelete() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        Dim objPKHeaderFacade As New PKHeaderFacade(User)

        For Each oDataGridItem In dgListPK.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _pk As KTB.Dnet.Domain.PKHeader = New PKHeaderFacade(User).Retrieve(CInt(CType(oDataGridItem.FindControl("lblID"), Label).Text))
                '_pk.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                '_pk.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                If _pk.PKStatus = enumStatusPK.Status.Validasi Then
                    _pk = objPKHeaderFacade.Retrieve(_pk.ID)
                    _pk.PKStatus = status.UnValidate
                    oExArgs.Add(_pk)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Function PopulatePKBatal() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        Dim objPKHeaderFacade As New PKHeaderFacade(User)

        For Each oDataGridItem In dgListPK.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _pk As KTB.Dnet.Domain.PKHeader = New PKHeaderFacade(User).Retrieve(CInt(CType(oDataGridItem.FindControl("lblID"), Label).Text))
                '_pk.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                '_pk.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                If _pk.PKStatus = enumStatusPK.Status.Baru Then
                    _pk = objPKHeaderFacade.Retrieve(_pk.ID)
                    _pk.PKStatus = status.Delete
                    oExArgs.Add(_pk)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "PKNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Sub UserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ValidatePKViewList_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Validasi PK")
        End If
        btnValidate.Visible = SecurityProvider.Authorize(Context.User, SR.PKValidate_Privilege)
        btnDelete.Visible = SecurityProvider.Authorize(Context.User, SR.ValidatePKCancelValidate_Privilege)
        btnBatal.Visible = SecurityProvider.Authorize(Context.User, SR.ValidatePKButtonCancel_Privilege)
        'btn.Visible = SecurityProvider.Authorize(Context.User, SR.ValidatePKCancelValidate_Privilege)

        Dim isPriceVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege))
        Label7.Visible = isPriceVisible
        Label20.Visible = isPriceVisible
        Label8.Visible = isPriceVisible
        lblTotal.Visible = isPriceVisible
        dgListPK.Columns(13).Visible = isPriceVisible
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        UserPrivilege()
        If Not IsPostBack Then
            sessionHelper.SetSession("IsInPeriodForFreezePK", CommonFunction.IsInPeriodForFreezePK(User))


            Dim FreezeFuncStatus As Boolean = True
            'only for PK Tambahan
            'If ddlJenisPesanan.SelectedItem.Value = EnumDealerTransType.DealerTransKind.PKTambahan Then
            'cek transactionControl
            Dim objTransaction As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(sessionHelper.GetSession("DEALER").ID, CInt(EnumDealerTransType.DealerTransKind.FreezePK).ToString)
            If Not (objTransaction Is Nothing) Then
                If objTransaction.Status = 0 Then
                    FreezeFuncStatus = False
                End If
            End If

            If Not ((Date.Now.ToString("yyyyMMdd") = New Date(2014, 6, 26).ToString("yyyyMMdd")) Or (Date.Now.ToString("yyyyMMdd") = New Date(2014, 6, 27).ToString("yyyyMMdd"))) And FreezeFuncStatus Then
                sessionHelper.SetSession("IsInPeriodForFreezePK", CommonFunction.IsInPeriodForFreezePK(User))
            Else
                sessionHelper.SetSession("IsInPeriodForFreezePK", FreezeFuncStatus)
            End If
            sessionHelper.SetSession("IsInPeriodForFreezePK", FreezeFuncStatus)

            RetrieveMaster()
            BindToddl()
            BindToddlCategory()
            InitiatePage()
            RowPerhatian.Visible = False
            tblOperator.Visible = False
            ddlOrderType.SelectedIndex = 0
            If ddlCategory.Items.Count = 0 OrElse ddlPurpose.Items.Count = 0 OrElse ddlOrderType.Items.Count = 0 Then
                btnCari.Enabled = False
            End If
            If GetSessionCriteria() Then
                BindGrid()
            End If

            Dim objDealer As Dealer = Session.Item("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                hdnTitle.Value = "MKS"

            Else
                hdnTitle.Value = "DEALER"
                txtDealerBranchCode.Attributes.Add("ReadOnly", "ReadOnly")
            End If

            lblPopUpDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"

        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        btnBatal.Attributes.Add("OnClick", "return confirm('Anda yakin melakukan BATAL ??');")
    End Sub

    Private Sub dgListPK_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgListPK.SortCommand
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

        dgListPK.SelectedIndex = -1
        dgListPK.CurrentPageIndex = 0
        BindDataGrid(dgListPK.CurrentPageIndex)

    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dgListPK.CurrentPageIndex = 0
        BindGrid()
    End Sub

    Private Function IsValidateFreeze() As Boolean

        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        Dim objPKHeaderFacade As New PKHeaderFacade(User)

        For Each oDataGridItem In dgListPK.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _pk As KTB.Dnet.Domain.PKHeader = New PKHeaderFacade(User).Retrieve(CInt(CType(oDataGridItem.FindControl("lblID"), Label).Text))

                If _pk.FreezeStatus(Session("IsInPeriodForFreezePK")) = enumStatusPK.EnumFreezeStatus.Freeze Then
                    MessageBox.Show("Pengajuan PK Tambahan untuk periode bulan ini sudah ditutup")
                    Return False
                End If

            End If
        Next

        Return True
    End Function

    Private Sub btnValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidate.Click

        'added by anh 20140626 , req by yurike
        'remove filter for 2 days 
        If Not ((Date.Now.ToString("yyyyMMdd") = New Date(2014, 6, 26).ToString("yyyyMMdd")) Or (Date.Now.ToString("yyyyMMdd") = New Date(2014, 6, 27).ToString("yyyyMMdd"))) Then
            If Not IsValidateFreeze() Then Exit Sub
        End If

        Dim listPK As ArrayList = PopulatePKValidate()
        If listPK.Count = 0 Then
            'to do
            MessageBox.Show(SR.DataNotFoundByStatus("PK", "Baru"))
        Else
            Dim objPKHeaderFacade As New PKHeaderFacade(User)
            objPKHeaderFacade.validatePK(listPK)
            RecordStatusChangeHistory(listPK, enumStatusPK.Status.Baru)
            BindGrid()
            MessageBox.Show(SR.ValidateSucces)
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim listPK As ArrayList = PopulatePKDelete()

        If listPK.Count = 0 Then
            'MessageBox.Show("Tidak ada PK dengan status Validasi yang Dipilih")
            MessageBox.Show(SR.DataNotSelectedByStatus("PK", "Validasi"))
        Else
            Dim objPKHeaderFacade As New PKHeaderFacade(User)
            objPKHeaderFacade.validatePK(listPK)
            RecordStatusChangeHistory(listPK, enumStatusPK.Status.Validasi)
            BindGrid()
            MessageBox.Show("Batal Validasi berhasil")
        End If
    End Sub

    Private ValidateIconUpdate As Boolean
    Private Sub dgListPK_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgListPK.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            'added by anh 20140626 , req by yurike
            'remove filter for 2 days 
            Dim isFreezePass As Boolean = False
            If Not ((Date.Now.ToString("yyyyMMdd") = New Date(2014, 6, 26).ToString("yyyyMMdd")) Or (Date.Now.ToString("yyyyMMdd") = New Date(2014, 6, 27).ToString("yyyyMMdd"))) Then
                isFreezePass = True
            End If

            If Not e.Item.DataItem Is Nothing Then
                'e.Item.DataItem.GetType().ToString()
                Dim RowValue As PKHeader = CType(e.Item.DataItem, PKHeader)

                If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then

                    Dim imgUnFreeze As HtmlImage = CType(e.Item.FindControl("imgUnFreeze"), HtmlImage)


                    If RowValue.FreezeStatus(Session("IsInPeriodForFreezePK")) = enumStatusPK.EnumFreezeStatus.NeverFreeze OrElse RowValue.FreezeStatus(Session("IsInPeriodForFreezePK")) = enumStatusPK.EnumFreezeStatus.NotFreeze Then
                        imgUnFreeze.Visible = False
                    ElseIf RowValue.FreezeStatus(Session("IsInPeriodForFreezePK")) = enumStatusPK.EnumFreezeStatus.Freeze Then
                        imgUnFreeze.Visible = Not isFreezePass
                        imgUnFreeze.Src = "../images/unlock.gif"
                    ElseIf RowValue.FreezeStatus(Session("IsInPeriodForFreezePK")) = enumStatusPK.EnumFreezeStatus.FreezeButUnlock Then
                        imgUnFreeze.Visible = Not isFreezePass
                        imgUnFreeze.Src = "../images/lock.gif"
                    End If

                    e.Item.Cells(3).Text = (e.Item.ItemIndex + 1 + (dgListPK.PageSize * dgListPK.CurrentPageIndex)).ToString
                    If e.Item.ItemIndex = 0 Then
                        ValidateIconUpdate = SecurityProvider.Authorize(Context.User, SR.ValidatePKIconUpdate_Privilege)
                    End If

                    Dim lblDealerBranchCode As Label = CType(e.Item.FindControl("lblDealerBranchCode"), Label)
                    If Not IsNothing(RowValue.DealerBranch) Then
                        lblDealerBranchCode.Text = RowValue.DealerBranch.DealerBranchCode & " / " & RowValue.DealerBranch.Term1
                    End If

                    Dim lblStatusString As Label = CType(e.Item.FindControl("lblStatusString"), Label)
                    Dim lblJenisPesanan As Label = CType(e.Item.FindControl("lblJenisPesanan"), Label)
                    Dim lblCategory As Label = CType(e.Item.FindControl("lblCategory"), Label)
                    Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
                    e.Item.Cells(14).Text = FormatNumber(RowValue.TotalHargaTebus, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    lblDealer.Text = RowValue.Dealer.DealerCode
                    If Not IsNothing(lblDealer) Then
                        lblDealer.ToolTip = RowValue.Dealer.SearchTerm1
                    End If
                    lblCategory.Text = RowValue.Category.CategoryCode
                    Dim EnumStatus As enumStatusPK.Status = RowValue.PKStatus
                    lblStatusString.Text = EnumStatus.ToString
                    lblJenisPesanan.Text = CType(RowValue.OrderType, LookUp.EnumJenisPesanan).ToString
                    e.Item.Cells(16).Text = RowValue.Dealer.DealerCode

                    Dim linkbtn As LinkButton = e.Item.FindControl("lbtnEdit")
                    Dim objDealer As Dealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
                    If objDealer Is Nothing Then
                        Response.Redirect("../SessionExpired.aspx")
                    End If

                    If (RowValue.PKStatus <> enumStatusPK.Status.Baru) OrElse ((Not (RowValue.Dealer Is Nothing)) AndAlso (RowValue.Dealer.ID <> objDealer.ID)) Then
                        linkbtn.Text = "<img src=""../images/detail.gif"" border=""0"" alt=""Lihat"">"
                        'linkbtn.Visible = SecurityProvider.Authorize(Context.User, SR.ValidatePKIconView_Privilege)
                    End If

                    linkbtn.Visible = ValidateIconUpdate

                End If
            End If
        End If
    End Sub

    Private Sub dgListPK_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgListPK.PageIndexChanged
        dgListPK.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        Dim listPK As ArrayList = PopulatePKBatal()

        If listPK.Count = 0 Then
            '     MessageBox.Show("Tidak ada PK dengan status Baru yang Dipilih")
            MessageBox.Show(SR.DataNotSelectedByStatus("PK", "Baru"))
        Else
            Dim objPKHeaderFacade As New PKHeaderFacade(User)
            objPKHeaderFacade.validatePK(listPK)
            RecordStatusChangeHistory(listPK, enumStatusPK.Status.Baru)
            BindGrid()
        End If
    End Sub

    Private Sub dgListPK_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgListPK.ItemCommand
        Dim objDealer As Dealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        If objDealer Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If

        If e.CommandName = "edit" Then
            If e.Item.Cells(1).Text = 1 Then
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                SetSessionCriteria()
                Response.Redirect("../PK/PesananKendaraanBiasa.aspx?PKNumber=" & e.Item.Cells(7).Text & "&DealerCode=" & e.Item.Cells(16).Text & "&Src=" & "validasi")
            Else
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                SetSessionCriteria()
                Response.Redirect("../PK/PesananKendaraanKhusus.aspx?PKNumber=" & e.Item.Cells(7).Text & "&DealerCode=" & e.Item.Cells(16).Text & "&Src=" & "validasi")
            End If
        End If
    End Sub

#End Region

End Class
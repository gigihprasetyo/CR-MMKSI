#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class ListPK
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents Label18 As System.Web.UI.WebControls.Label
    Protected WithEvents Label19 As System.Web.UI.WebControls.Label
    Protected WithEvents Label20 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPurpose As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dgPKList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtPKNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlRencanaPenebusan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents btnAgree As System.Web.UI.WebControls.Button
    Protected WithEvents btnDisagree As System.Web.UI.WebControls.Button
    Protected WithEvents tblOperator As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lboxStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblQuantity As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalHarga As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label

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

    Private arlPKHeader As ArrayList
    Private arlPK As ArrayList
    Private sessionHelper As New sessionHelper
    Private objDealer As Dealer

#End Region

#Region "Custom Method"

    Private Function GetSessionCriteria() As Boolean
        Dim objSSPO As ArrayList = sessionHelper.GetSession("SESSIONPK")
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
            ddlRencanaPenebusan.ClearSelection()
            ddlCategory.ClearSelection()
            ddlPurpose.ClearSelection()
            ddlRencanaPenebusan.SelectedIndex = objSSPO.Item(3)
            ddlCategory.SelectedIndex = objSSPO.Item(4)
            ddlPurpose.SelectedIndex = objSSPO.Item(5)
            dgPKList.CurrentPageIndex = objSSPO.Item(6)
            ViewState("CurrentSortColumn") = objSSPO.Item(7)
            ViewState("CurrentSortDirect") = objSSPO.Item(8)
            Return True
        End If
        Return False
    End Function

    Private Sub SetSessionCriteria()
        Dim objSSPO As ArrayList = New ArrayList
        objSSPO.Add(txtPKNumber.Text)
        objSSPO.Add(ddlOrderType.SelectedIndex)
        objSSPO.Add(GetSelectedItem(lboxStatus))
        objSSPO.Add(ddlRencanaPenebusan.SelectedIndex)
        objSSPO.Add(ddlCategory.SelectedIndex)
        objSSPO.Add(ddlPurpose.SelectedIndex)
        objSSPO.Add(dgPKList.CurrentPageIndex)
        objSSPO.Add(CType(ViewState("CurrentSortColumn"), String))
        objSSPO.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        sessionHelper.SetSession("SESSIONPK", objSSPO)
    End Sub

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
        'Dim objDealer As Dealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        'If Not objDealer Is Nothing Then
        '    'lblKodeDealer.Text = objDealer.DealerCode
        '    lblKodeDealer.Text = objDealer.DealerCode & "/" & objDealer.SearchTerm1
        '    '        'lblDealerName.Text = objDealer.DealerName
        '    '        'lblDealerCity.Text = objDealer.City.CityName
        'Else
        '    'Response.Redirect("../SessionExpired.htm")
        'End If
    End Sub

    Sub dgPKList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        dgPKList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
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

        'Dim listitemBlank As listItem
        'listitemBlank = New ListItem("Silahkan Pilih", -1)

        ''--DropDownList Kategori
        'Dim arrayListCategory As ArrayList = New PKHeaderFacade(User).RetrieveListCategory
        'ddlCategory.Items.Add(listitemBlank)
        'For Each item As Category In arrayListCategory
        '    Dim listItem As New ListItem(item.CategoryCode, item.ID)
        '    ddlCategory.Items.Add(listItem)
        'Next
    End Sub

    Private Sub BindToddl()
        Dim listitemBlank As ListItem
        For Each item As ListItem In LookUp.ArraylistMonth(True, 0, 6, DateTime.Now)
            item.Selected = False
            ddlRencanaPenebusan.Items.Add(item)
        Next
        ddlRencanaPenebusan.ClearSelection()
        ddlRencanaPenebusan.SelectedValue = Format(DateTime.Now, "MMM yyyy").ToString
        For Each itemPurpose As ListItem In LookUp.ArrayPurpose
            itemPurpose.Selected = False
            If itemPurpose.Text = "Khusus" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKondisiKhusus_Privilege) Then
                    ddlPurpose.Items.Add(itemPurpose)
                End If
            ElseIf itemPurpose.Text = "Biasa" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKondisiBiasa_Privilege) Then
                    ddlPurpose.Items.Add(itemPurpose)
                End If
            End If
        Next


        If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) Then
            Dim itemBlank As ListItem = New ListItem("Silahkan Pilih", -1)
            itemBlank.Selected = False
            ddlOrderType.Items.Add(itemBlank)
        End If

        For Each itemJenisPesanan As ListItem In LookUp.ArrayJenisPesanan
            itemJenisPesanan.Selected = False
            If itemJenisPesanan.Text = "Bulanan" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKindBulanan_Privilege) Then
                    ddlOrderType.Items.Add(itemJenisPesanan)
                End If
            ElseIf itemJenisPesanan.Text = "Tambahan" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKindTambahan_Privilege) Then
                    ddlOrderType.Items.Add(itemJenisPesanan)
                End If
            End If
        Next
        lboxStatus.ClearSelection()
        lboxStatus.SelectedIndex = -1
    End Sub

    Private Sub BindToddlOld()

        Dim listitemBlank As ListItem

        '--DropDownList Rencana Penebusan
        For Each item As ListItem In LookUp.ArraylistMonth(True, 0, 6, DateTime.Now)
            ddlRencanaPenebusan.Items.Add(item)
        Next
        ddlRencanaPenebusan.ClearSelection()
        ddlRencanaPenebusan.SelectedValue = Format(DateTime.Now, "MMM yyyy").ToString

        '--DropDownList Kondisi Pesanan
        'listitemBlank = New ListItem("Silahkan Pilih", -1)
        'ddlPurpose.Items.Add(listitemBlank)
        'For Each item As ListItem In LookUp.ArrayPurpose
        '    ddlPurpose.Items.Add(item)
        'Next
        'ddlPurpose.SelectedValue = -1

        'If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) Then
        '    listitemBlank = New ListItem("Silahkan Pilih", -1)
        '    ddlPurpose.Items.Add(listitemBlank)
        'End If

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

        '--DropDownList Jenis Pesanan
        'listitemBlank = New ListItem("Silahkan Pilih", -1)
        'ddlOrderType.Items.Add(listitemBlank)
        'For Each item As ListItem In LookUp.ArrayJenisPesanan
        '    ddlOrderType.Items.Add(item)
        'Next
        'ddlOrderType.SelectedValue = -1

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
        lboxStatus.ClearSelection()
        lboxStatus.SelectedIndex = -1
        '--DropDownList Status
        'listitemBlank = New listItem("Silahkan Pilih", -1)
        'ddlStatus.Items.Add(listitemBlank)
        'For Each item As listItem In LookUp.ArrayStatusPK
        '    ddlStatus.Items.Add(item)
        'Next
        'ddlStatus.SelectedValue = -1

    End Sub

    Private Sub BindGrid()
        BindDataGrid(dgPKList.CurrentPageIndex)
        TotalAmount()
        tblOperator.Visible = True
    End Sub

    Private Sub BindDataGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If txtKodeDealer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If

        'If ddlDealerCode.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Dealer.ID", MatchType.Exact, ddlDealerCode.SelectedValue))

        If ddlOrderType.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "OrderType", MatchType.Exact, ddlOrderType.SelectedValue))

        'Hanya bisa search PK yang status setuju, tidaksetuju, rilis, dan blok
        If lboxStatus.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "PKStatus", MatchType.InSet, "(" & SelectedStatus & ")"))
        Else
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "PKStatus", MatchType.InSet, "(" & CType(enumStatusPK.Status.Setuju, Integer) & "," & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPK.Status.Tidak_Setuju, Integer) & "," & CType(enumStatusPK.Status.DiBlok, Integer) & ")"))
        End If

        If txtPKNumber.Text <> "" Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "DealerPKNumber", MatchType.Exact, txtPKNumber.Text))

        If ddlCategory.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Category.ID", MatchType.Exact, ddlCategory.SelectedValue))

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Purpose", MatchType.Exact, ddlPurpose.SelectedValue))

        'If Not ddlRencanaPenebusan.SelectedIndex = 0 Then
        Dim tgl As DateTime = System.Convert.ToDateTime(ddlRencanaPenebusan.SelectedItem.ToString)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RequestPeriodeMonth", MatchType.Exact, CType(tgl.Month, Integer)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RequestPeriodeYear", MatchType.Exact, CType(tgl.Year, Integer)))
        'End If

        'If currentPageIndex >= 0 Then
        'arlPKHeader = New PKHeaderFacade(User).RetrieveByCriteria(criterias, dgPKList.PageCount, dgPKList.PageSize, total)
        'dgPKList.DataSource = arlPKHeader
        'Else
        'arlPKHeader = New PKHeaderFacade(User).RetrieveByCriteria(criterias, currentPageIndex + 1, dgPKList.PageSize, total)
        arlPKHeader = New PKHeaderFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dgPKList.PageSize, _
                       total, CType(ViewState("CurrentSortColumn"), String), _
                       CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgPKList.DataSource = arlPKHeader
        'End If
        dgPKList.VirtualItemCount = total
        sessionHelper.SetSession("PKHeader", arlPKHeader)
        If arlPKHeader.Count > 0 Then
            dgPKList.DataBind()
        Else
            dgPKList.DataBind()
            MessageBox.Show("Data Tidak Ditemukan")
        End If


        arlPK = New PKHeaderFacade(User).Retrieve(criterias)
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

    Private Function PopulatePKAgree() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        Dim objPKHeaderFacade As New PKHeaderFacade(User)

        For Each oDataGridItem In dgPKList.Items
            chkExport = oDataGridItem.FindControl("chkSelect")
            If chkExport.Checked Then
                Dim _pk As KTB.Dnet.Domain.PKHeader = New PKHeaderFacade(User).Retrieve(CInt(CType(oDataGridItem.FindControl("lblID"), Label).Text))
                '_pk.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                '_pk.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                If _pk.PKStatus = enumStatusPK.Status.Rilis Then
                    _pk = objPKHeaderFacade.Retrieve(_pk.ID)
                    _pk.PKStatus = status.Agree
                    oExArgs.Add(_pk)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Function PopulatePKDisagree() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        Dim objPKHeaderFacade As New PKHeaderFacade(User)
        Dim txtReason As System.Web.UI.WebControls.TextBox

        For Each oDataGridItem In dgPKList.Items
            chkExport = oDataGridItem.FindControl("chkSelect")
            If chkExport.Checked Then
                Dim _pk As KTB.Dnet.Domain.PKHeader = New PKHeaderFacade(User).Retrieve(CInt(CType(oDataGridItem.FindControl("lblID"), Label).Text))
                '_pk.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                '_pk.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                If _pk.PKStatus = enumStatusPK.Status.Rilis Then
                    txtReason = oDataGridItem.FindControl("txtRejectedReason")

                    _pk = objPKHeaderFacade.Retrieve(_pk.ID)
                    _pk.PKStatus = status.Disagree
                    _pk.RejectedReason = txtReason.Text.Trim
                    oExArgs.Add(_pk)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    'Private Function PopulatePKBlok() As ArrayList
    '    Dim oDataGridItem As DataGridItem
    '    Dim chkExport As System.Web.UI.WebControls.CheckBox
    '    Dim oExArgs As New System.Collections.ArrayList
    '    Dim status As New enumStatusPK
    '    Dim objPKHeaderFacade As New PKHeaderFacade(User)

    '    For Each oDataGridItem In dgPKList.Items
    '        chkExport = oDataGridItem.FindControl("chkSelect")
    '        If chkExport.Checked Then
    '            Dim _pk As New KTB.Dnet.Domain.PKHeader
    '            _pk.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
    '            _pk.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
    '            If _pk.PKStatus = enumStatusPK.Status.Rilis Then
    '                _pk = objPKHeaderFacade.Retrieve(_pk.ID)
    '                _pk.PKStatus = status.Block
    '                oExArgs.Add(_pk)
    '            End If
    '        End If
    '    Next
    '    status = Nothing
    '    Return oExArgs
    'End Function

    'Private Function PopulatePKBtlBlok() As ArrayList
    '    Dim oDataGridItem As DataGridItem
    '    Dim chkExport As System.Web.UI.WebControls.CheckBox
    '    Dim oExArgs As New System.Collections.ArrayList
    '    Dim status As New enumStatusPK
    '    Dim objPKHeaderFacade As New PKHeaderFacade(User)

    '    For Each oDataGridItem In dgPKList.Items
    '        chkExport = oDataGridItem.FindControl("chkSelect")
    '        If chkExport.Checked Then
    '            Dim _pk As New KTB.Dnet.Domain.PKHeader
    '            _pk.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
    '            _pk.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
    '            If _pk.PKStatus = enumStatusPK.Status.DiBlok Then
    '                _pk = objPKHeaderFacade.Retrieve(_pk.ID)
    '                _pk.PKStatus = status.Release
    '                oExArgs.Add(_pk)
    '            End If
    '        End If
    '    Next
    '    status = Nothing
    '    Return oExArgs
    'End Function

    'Private Function PopulatePKTransferData() As ArrayList
    '    Dim oDataGridItem As DataGridItem
    '    Dim chkSelect As System.Web.UI.WebControls.CheckBox
    '    Dim oExArgs As New System.Collections.ArrayList
    '    Dim status As New enumStatusPK
    '    Dim objPKHeaderFacade As New PKHeaderFacade(User)

    '    For Each oDataGridItem In dgPKList.Items
    '        chkSelect = oDataGridItem.FindControl("chkSelect")
    '        If chkSelect.Checked Then
    '            Dim _pk As New KTB.Dnet.Domain.PKHeader
    '            _pk.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
    '            _pk.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
    '            If _pk.PKStatus = enumStatusPK.Status.Setuju Or _pk.PKStatus = enumStatusPK.Status.Tidak_Setuju Or _pk.PKStatus = enumStatusPK.Status.Ditolak Then
    '                _pk = objPKHeaderFacade.Retrieve(_pk.ID)
    '                If _pk.PKStatus = enumStatusPK.Status.Tidak_Setuju Or _pk.PKStatus = enumStatusPK.Status.Ditolak Then
    '                    For Each item As PKDetail In _pk.PKDetails
    '                        item.ResponseQty = 0
    '                    Next
    '                End If
    '                oExArgs.Add(_pk)
    '            End If
    '        End If
    '    Next
    '    status = Nothing
    '    Return oExArgs
    'End Function

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "PKNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Sub UserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.PersetujuanPKView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Persetujuan PK")
        End If

        btnAgree.Visible = SecurityProvider.Authorize(Context.User, SR.PersetujuanPKApprove_Privilege)
        btnDisagree.Visible = SecurityProvider.Authorize(Context.User, SR.PersetujuanPKDisApprove_Privilege)

        Dim isPriceVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege))
        lblTotalHarga.Visible = isPriceVisible
        Label1.Visible = isPriceVisible
        Label2.Visible = isPriceVisible
        lblTotal.Visible = isPriceVisible
        dgPKList.Columns(12).Visible = isPriceVisible
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        UserPrivilege()
        If Not IsPostBack Then
            RetrieveMaster()
            BindToddl()
            BindToddlCategory()
            InitiatePage()
            tblOperator.Visible = False
            If ddlCategory.Items.Count = 0 OrElse ddlPurpose.Items.Count = 0 OrElse ddlOrderType.Items.Count = 0 Then
                btnCari.Enabled = False
            End If
            'BindToddlCategory()
            'BindToddlDealer()
            If GetSessionCriteria() Then
                BindGrid()
            End If
        End If
        btnAgree.Attributes.Add("OnClick", "return confirm('Anda yakin melakukan SETUJU ??');")
        btnDisagree.Attributes.Add("OnClick", "return confirm('Anda yakin melakukan TIDAK SETUJU ??');")
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Private Sub dgPKList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPKList.SortCommand
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

        dgPKList.SelectedIndex = -1
        dgPKList.CurrentPageIndex = 0
        BindDataGrid(dgPKList.CurrentPageIndex)

    End Sub

    Private Sub dgPKList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPKList.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            arlPKHeader = sessionHelper.GetSession("PKHeader")
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim objPKheader As PKHeader = arlPKHeader(e.Item.ItemIndex)
            Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
            lblDealer.Text = objPKheader.Dealer.DealerCode

            If Not IsNothing(lblDealer) Then
                lblDealer.ToolTip = objPKheader.Dealer.SearchTerm1
            End If
            e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dgPKList.PageSize * dgPKList.CurrentPageIndex)).ToString
            e.Item.Cells(8).Text = objPKheader.Category.CategoryCode
            e.Item.Cells(9).Text = CType(objPKheader.OrderType, LookUp.EnumJenisPesanan).ToString
            e.Item.Cells(12).Text = FormatNumber((objPKheader.TotalHargaTebus), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'e.Item.Cells(13).Visible = SecurityProvider.Authorize(Context.User, SR.PersetujuanPKIConView_Privilege)

            If objPKheader.PKStatus = enumStatusPK.Status.Rilis Then
                e.Item.BackColor = System.Drawing.Color.Yellow
            End If

            If Not e.Item.DataItem Is Nothing Then
                e.Item.DataItem.GetType().ToString()
                Dim RowValue As PKHeader = CType(e.Item.DataItem, PKHeader)

                If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                    Dim lblStatusString As Label = CType(e.Item.FindControl("lblStatusString"), Label)
                    Dim EnumStatus As enumStatusPK.Status = RowValue.PKStatus

                    lblStatusString.Text = EnumStatus.ToString

                    'If RowValue.PKStatus <> EnumStatus.Dikirim Then
                    '    Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                    '    lbtnEdit.Visible = False
                    'End If
                End If
            End If
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dgPKList.CurrentPageIndex = 0
        BindGrid()
    End Sub

    Private Sub dgPKList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPKList.ItemCommand
        If e.CommandName = "edit" Then
            Dim objDealer As Dealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
            'If objDealer Is Nothing Then
            '    Response.Redirect("../SessionExpired.htm")
            'End If
            If e.Item.Cells(13).Text = 1 Then
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                SetSessionCriteria()
                Response.Redirect("../PK/PesananKendaraanBiasa.aspx?PKNumber=" & e.Item.Cells(5).Text & "&DealerCode=" & objDealer.DealerCode.ToString & "&Src=" & "persetujuan")
            Else
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                SetSessionCriteria()
                Response.Redirect("../PK/PesananKendaraanKhusus.aspx?PKNumber=" & e.Item.Cells(5).Text & "&DealerCode=" & objDealer.DealerCode.ToString & "&Src=" & "persetujuan")
            End If
        End If
    End Sub

    Private Sub btnAgree_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgree.Click
        Dim listPK As ArrayList = PopulatePKAgree()

        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim txtReason As System.Web.UI.WebControls.TextBox

        For Each oDataGridItem In dgPKList.Items
            chkExport = oDataGridItem.FindControl("chkSelect")
            If chkExport.Checked Then
                txtReason = oDataGridItem.FindControl("txtRejectedReason")
                If txtReason.Text.Trim <> "" Then
                    'MessageBox.Show("Mohon tidak mengisi Alasan Tidak Setuju")
                    MessageBox.Show("Untuk status SETUJU mohon untuk tidak mengisi alasan tidak setuju")
                    Exit Sub
                End If
            End If
        Next
        If listPK.Count = 0 Then
            MessageBox.Show("Tidak ada PK dengan status rilis yang Dipilih")
        Else
            Dim objPKHeaderFacade As New PKHeaderFacade(User)
            objPKHeaderFacade.validatePK(listPK)
            RecordStatusChangeHistory(listPK, enumStatusPK.Status.Rilis)
            BindGrid()
        End If
    End Sub

    Private Sub btnDisagree_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisagree.Click
        Dim listPK As ArrayList = PopulatePKDisagree()

        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim txtReason As System.Web.UI.WebControls.TextBox

        For Each oDataGridItem In dgPKList.Items
            chkExport = oDataGridItem.FindControl("chkSelect")
            If chkExport.Checked Then
                txtReason = oDataGridItem.FindControl("txtRejectedReason")
                If txtReason.Text.Trim = "" Then
                    MessageBox.Show("Mohon mengisi Alasan Tidak Setuju terlebih dahulu")
                    Exit Sub
                ElseIf txtReason.Text.Trim.Length < 5 Then
                    MessageBox.Show("Alasan Tidak Setuju tidak boleh kurang dari lima karakter")
                    Exit Sub
                End If
            End If
        Next
        If listPK.Count = 0 Then
            MessageBox.Show("Tidak ada PK dengan status rilis yang Dipilih")
        Else
            Dim objPKHeaderFacade As New PKHeaderFacade(User)
            objPKHeaderFacade.validatePK(listPK)
            RecordStatusChangeHistory(listPK, enumStatusPK.Status.Rilis)
            BindGrid()
        End If
    End Sub

    'Private Sub btnBlok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlok.Click
    '    Dim listPK As ArrayList = PopulatePKBlok()

    '    If listPK.Count = 0 Then
    '        MessageBox.Show("Tidak ada PK dengan status rilis yang Dipilih")
    '    Else
    '        Dim objPKHeaderFacade As New PKHeaderFacade(User)
    '        objPKHeaderFacade.validatePK(listPK)
    '        BindGrid()
    '    End If
    'End Sub

    'Private Sub btnBtlBlok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBtlBlok.Click
    '    Dim listPK As ArrayList = PopulatePKBtlBlok()

    '    If listPK.Count = 0 Then
    '        MessageBox.Show("Tidak ada PK dengan status DiBlok yang Dipilih")
    '    Else
    '        Dim objPKHeaderFacade As New PKHeaderFacade(User)
    '        objPKHeaderFacade.validatePK(listPK)
    '        BindGrid()
    '    End If
    'End Sub

    'Private Sub btnTransferData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransferData.Click
    '    Dim listPK As ArrayList = PopulatePKTransferData()
    '    Dim _fileHelper As New FileHelper

    '    Try
    '        Dim str As FileInfo = _fileHelper.TransferPKtoSAP(listPK)
    '        MessageBox.Show("Berhasil Upload ke SAP : --> " & str.FullName)
    '    Catch ex As Exception
    '        MessageBox.Show("Gagal Upload ke SAP" & ex.Message.ToString)
    '    End Try
    'End Sub

#End Region

End Class
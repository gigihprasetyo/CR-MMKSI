Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Sparepart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports System
Imports System.Drawing.Color
Imports System.Text
Imports KTB.DNet.Security
Imports OfficeOpenXml

Public Class FrmSettingCreditAccount
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnSaveKelipatanPembayaran As System.Web.UI.WebControls.Button
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button
    Protected WithEvents dtgDealerList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cbSalesUnit As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddlstatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatusProses As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCaraPembayaranProses As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPropinsi As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTermOfPayment As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTermOfPaymentGrid As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cbService As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cbSparePart As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlGroup As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCreditAccount As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKelipatanPembayaran As System.Web.UI.WebControls.TextBox

    Protected WithEvents ddlPrevTermOfPayment As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPrevTermOfPaymentGrid As System.Web.UI.WebControls.DropDownList

    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button

    Private _sessHelper As SessionHelper = New SessionHelper
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        InitializeComponent()
    End Sub


    'CODEGEN: This method call is required by the Web Form Designer
    'Do not modify it using the code editor.

#End Region

    Private objDealer As Dealer
    Private m_bAdminAssigHakAccessOrganization_Privilege As Boolean = False
    Private m_bFormPrivilege As Boolean = False
    Private listOfPayments As ArrayList = New TermOfPaymentFacade(User).RetrieveActivePaymentTypeList()

    Private _sessDatas As String = "FrmSettingCreditAccount._sessDatas"

    Private Sub BindDatagrid(ByVal indexPage)
        Dim totalRow As Integer = 0
        Dim aDatas As ArrayList

        If (indexPage >= 0) And Not IsNothing(CType(_sessHelper.GetSession("Criteria"), CriteriaComposite)) Then
            'aDatas = New DealerFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession("Criteria"), CriteriaComposite), _
            '  indexPage + 1, dtgDealerList.PageSize, totalRow, _
            '  CType(ViewState("CurrentSortColumn"), String), _
            '  CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            Dim sD As New SortCollection
            sD.Add(New Sort(GetType(VWI_DealerSettingCreditAccount), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
            aDatas = New VWI_DealerSettingCreditAccountFacade(User).Retrieve(CType(_sessHelper.GetSession("Criteria"), CriteriaComposite), sD)
            Me._sessHelper.SetSession(Me._sessDatas, aDatas)
            dtgDealerList.DataSource = aDatas
            dtgDealerList.VirtualItemCount = totalRow
            dtgDealerList.DataBind()

        End If

    End Sub
    Private Sub BindDdlStatus()
        Dim listStatus As New EnumDealerStatus
        Dim al As ArrayList = listStatus.RetrieveStatus
        For Each item As EnumDealer In al
            ddlstatus.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
            ddlStatusProses.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
        Next
        ddlstatus.Items.Insert(0, New ListItem("Pilih Status", ""))
        ddlStatusProses.Items.Insert(0, New ListItem("Pilih Status", ""))
    End Sub

    Private Sub BindDdlCaraPembayaran()
        'Dim listStatus As New EnumDealerStatus
        'Dim al As ArrayList = listStatus.RetrieveStatus
        'For Each item As EnumDealer In al
        '    ddlstatus.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
        'Next
        'ddlstatus.Items.Insert(0, New ListItem("Pilih Status", ""))
    End Sub
    Private Sub BindGroup()
        Dim cDG As New CriteriaComposite(New Criteria(GetType(DealerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aDGs As ArrayList
        Dim oDGFac As New DealerGroupFacade(User)
        Dim sDG As New SortCollection

        sDG.Add(New Sort(GetType(DealerGroup), "GroupName", Sort.SortDirection.ASC))
        aDGs = oDGFac.Retrieve(cDG, sDG)
        With Me.ddlGroup.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", -1))
            For Each oDG As DealerGroup In aDGs
                .Add(New ListItem(oDG.GroupName, oDG.ID))
            Next
        End With
    End Sub
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindDdlStatus()
            BindDdlCaraPembayaran()
            BindDdlPaymentType()
            BindProvinceDropDown()

            Me.BindGroup()
            InitiatePage()
            'kalau dipanggil dari form dealer maintenance setelah proses update
            If Not (Request.QueryString("DealerID")) Is Nothing Then
                Dim nID As Integer = CType(Request.QueryString("DealerID"), Integer)
                If Not IsNothing(nID) Then
                    MessageBox.Show("Update Sukses !")
                    Dim ObjDealer As Dealer = New DealerFacade(User).Retrieve(nID)
                    If Not ObjDealer Is Nothing Then
                        Dim arrObjDealer As ArrayList = New ArrayList
                        arrObjDealer.Add(ObjDealer)
                        dtgDealerList.DataSource = arrObjDealer
                        dtgDealerList.DataBind()
                    End If
                End If
            End If
            Dim defaultKelipatanPembayaran As String = KTB.DNet.Lib.WebConfig.GetString("TOPKelipatanPembayaran")
            Dim updateTOPKelipatan As String = KTB.DNet.Lib.WebConfig.GetString("UpdateTOPKelipatan")
            txtKelipatanPembayaran.Text = defaultKelipatanPembayaran
            txtKelipatanPembayaran.Enabled = updateTOPKelipatan = 1
            btnSaveKelipatanPembayaran.Enabled = updateTOPKelipatan = 1
            btnProses.Enabled = False
            ddlStatusProses.Enabled = False
            ddlCaraPembayaranProses.Enabled = False
        End If
    End Sub
    Private Sub ActivateUserPrivilege()

        'm_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AdminUpdateOrganization_Privilege)
        'm_bAdminAssigHakAccessOrganization_Privilege = SecurityProvider.Authorize(Context.User, SR.AdminAssigHakAccessOrganization_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.Top_credit_account_lihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Parts - Setting Credit Account")
        End If
        Me.btnSearch.Visible = True
        Me.btnSearch.Enabled = True

        If SecurityProvider.Authorize(Context.User, SR.Top_credit_account_lihat_Privilege) Then
            Me.dtgDealerList.Columns(Me.dtgDealerList.Columns.Count - 1).Visible = True
        Else
            Me.dtgDealerList.Columns(Me.dtgDealerList.Columns.Count - 1).Visible = False
        End If

    End Sub

    Private Sub UpdateTOPUserPrivilege()

        'm_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AdminUpdateOrganization_Privilege)
        'm_bAdminAssigHakAccessOrganization_Privilege = SecurityProvider.Authorize(Context.User, SR.AdminAssigHakAccessOrganization_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.Top_credit_account_lihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Parts - Setting Credit Account")
        End If

    End Sub

    Private Sub InitiatePage()
        ClearData()
        AssignAttributeControl()
        ViewState("CurrentSortColumn") = "DealerCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    Private Sub ClearData()
        cbSalesUnit.Checked = False
        cbService.Checked = False
        cbSparePart.Checked = False
    End Sub
    Private Sub AssignAttributeControl()
        Dim lblPopUp As Label = CType(Page.FindControl("lblPopUpDealer"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDealerSelection.aspx", "", 500, 760, "DealerSelection")
    End Sub
    Private Function ConvertKodeDealer(ByVal sKodeDealerColl As String)
        Dim sKodeDealerTemp() As String = sKodeDealerColl.Split(New Char() {";"})
        Dim sKodeDealer As String = ""
        For i As Integer = 0 To sKodeDealerTemp.Length - 1
            sKodeDealer = sKodeDealer & "'" & sKodeDealerTemp(i).Trim() & "'"

            If Not (i = sKodeDealerTemp.Length - 1) Then
                sKodeDealer = sKodeDealer & ","
            End If
        Next
        sKodeDealer = "(" & sKodeDealer & ")"
        Return sKodeDealer
    End Function

    Private Sub CreateCriteriaSearch()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VWI_DealerSettingCreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VWI_DealerSettingCreditAccount), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If
        If Not (txtKodeDealer.Text.Trim() = "") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VWI_DealerSettingCreditAccount), "DealerCode", MatchType.InSet, ConvertKodeDealer(txtKodeDealer.Text.Trim())))
        End If
        If Not ddlstatus.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VWI_DealerSettingCreditAccount), "Status", MatchType.Exact, ddlstatus.SelectedValue))
        End If
        If cbSalesUnit.Checked Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VWI_DealerSettingCreditAccount), "SalesUnitFlag", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        End If
        If cbService.Checked Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VWI_DealerSettingCreditAccount), "ServiceFlag", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        End If
        If cbSparePart.Checked Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VWI_DealerSettingCreditAccount), "SparepartFlag", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        End If
        If Me.ddlGroup.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VWI_DealerSettingCreditAccount), "DealerGroupId", MatchType.Exact, CType(Me.ddlGroup.SelectedValue, Integer)))
        End If
        If Me.txtCreditAccount.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VWI_DealerSettingCreditAccount), "CreditAccount", MatchType.StartsWith, Me.txtCreditAccount.Text.Trim))
        End If
        If Not ddlTermOfPayment.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VWI_DealerSettingCreditAccount), "TermOfPaymentID", MatchType.Exact, ddlTermOfPayment.SelectedValue))
        End If
        If Not ddlPrevTermOfPayment.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VWI_DealerSettingCreditAccount), "PrevTermOfPaymentID", MatchType.Exact, ddlPrevTermOfPayment.SelectedValue))
        End If

        If ddlPropinsi.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VWI_DealerSettingCreditAccount), "ProvinceId", MatchType.Exact, ddlPropinsi.SelectedValue))
        End If

        _sessHelper.SetSession("Criteria", criterias)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        CreateCriteriaSearch()
        dtgDealerList.CurrentPageIndex = 0
        BindDatagrid(dtgDealerList.CurrentPageIndex)
        txtKelipatanPembayaran.Enabled = True
        btnSaveKelipatanPembayaran.Enabled = True

        btnProses.Enabled = True
        ddlStatusProses.Enabled = True
        ddlCaraPembayaranProses.Enabled = True
    End Sub

    Private Sub btnSaveKelipatanPembayaran_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveKelipatanPembayaran.Click
        UpdateTOPUserPrivilege()
        Dim result As Boolean = True

        If txtKelipatanPembayaran.Text <> "" Then
            Dim objAppConfigFacade As AppConfigFacade = New AppConfigFacade(User)
            Dim objAppConfig As AppConfig = objAppConfigFacade.Retrieve("TOPKelipatanPembayaran")
            objAppConfig.Value = txtKelipatanPembayaran.Text
            Dim res As Integer = objAppConfigFacade.Update(objAppConfig)
            If res > 0 Then
                MessageBox.Show("Kelipatan Pembayaran Berhasil Disimpan")
            Else
                MessageBox.Show("Kelipatan Pembayaran Gagal Disimpan")
            End If
        Else
            MessageBox.Show("Kelipatan tidak boleh kosong")
            Exit Sub
        End If
        'Dim txtKelipatanPembayaranE As TextBox = item.FindControl("txtKelipatanPembayaranE")
        'For Each item As DataGridItem In dtgDealerList.Items
        '    Dim checkBox As CheckBox = item.FindControl("cbSelected")

        '    If Not checkBox Is Nothing AndAlso checkBox.Checked Then
        '        ' My row ID is in the Third cell in an invisible column
        '        Dim ID As Integer = item.Cells(0).Text
        '        Dim dealerId As Integer = item.Cells(1).Text
        '        Dim topCAFacade As New TOPCreditAccountFacade(User)

        '        Dim oTopCA As TOPCreditAccount = topCAFacade.Retrieve(ID) ' CType(aDatas(e.Item.ItemIndex), Dealer)
        '        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(dealerId)
        '        If Not IsNothing(oTopCA) Then
        '            oTopCA.Dealer = objDealer
        '            oTopCA.KelipatanPembayaran = txtKelipatanPembayaran.Text
        '            'oTopCA.TermOfPayment = TermOfPaymentFacade.Retrieve(selectedTOP)

        '            oTopCA.ID = topCAFacade.Update(oTopCA)
        '            If oTopCA.ID > 0 Then
        '                'MessageBox.Show("Data Berhasil Disimpan")
        '            Else
        '                result = False
        '                'MessageBox.Show("Data Gagal Disimpan")
        '                'BindDatagrid(dtgDealerList.CurrentPageIndex)
        '            End If
        '        Else
        '            oTopCA = New TOPCreditAccount
        '            oTopCA.Dealer = objDealer
        '            oTopCA.KelipatanPembayaran = txtKelipatanPembayaran.Text
        '            'oTopCA.TermOfPayment = TermOfPaymentFacade.Retrieve(selectedTOP)

        '            oTopCA.ID = topCAFacade.Insert(oTopCA)
        '            If oTopCA.ID > 0 Then
        '                'MessageBox.Show("Data Berhasil Disimpan")
        '            Else
        '                result = False
        '                'MessageBox.Show("Data Gagal Disimpan")
        '                'BindDatagrid(dtgDealerList.CurrentPageIndex)
        '            End If
        '        End If
        '    End If
        'Next
        'If result Then
        '    MessageBox.Show("Data Berhasil Disimpan")
        'Else
        '    MessageBox.Show("Data/Sebagian Data Gagal Disimpan")
        'End If
        BindDatagrid(dtgDealerList.CurrentPageIndex)
    End Sub

    Private Sub btnProses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProses.Click
        Dim result As Boolean = True
        'Dim txtKelipatanPembayaranE As TextBox = item.FindControl("txtKelipatanPembayaranE")
        Dim selectedTOP As Integer = 0
        Dim selectedStatus As Integer = 0
        'If ddlCaraPembayaranProses.SelectedValue = "" And ddlStatusProses.SelectedValue = "" Then
        '    MessageBox.Show("Silahkan pilih Status & Tipe Pembayaran.")
        'End If
        Dim defaultKelipatanPembayaran As Integer = KTB.DNet.Lib.WebConfig.GetString("TOPKelipatanPembayaran")
        Dim kelPem As Integer = CInt(txtKelipatanPembayaran.Text)
        If kelPem <> defaultKelipatanPembayaran Then
            MessageBox.Show("Simpan kelipatan pembayaran terlebih dahulu.")
            Exit Sub
        End If
        If ddlStatusProses.SelectedIndex = 1 AndAlso ddlCaraPembayaranProses.SelectedIndex = 0 Then
            'If ddlStatusProses.SelectedIndex < 2 AndAlso ddlCaraPembayaranProses.SelectedIndex = 0 Then
            MessageBox.Show("Cara pembayaran harus dipilih")
            Exit Sub
        End If
        Dim termOfPaymentFacade As New TermOfPaymentFacade(User)
        For Each item As DataGridItem In dtgDealerList.Items
            Dim checkBox As CheckBox = item.FindControl("cbSelected")

            If Not checkBox Is Nothing AndAlso checkBox.Checked Then
                ' My row ID is in the Third cell in an invisible column
                Dim ID As Integer = item.Cells(0).Text
                Dim dealerId As Integer = item.Cells(1).Text
                Dim topCAFacade As New TOPCreditAccountFacade(User)

                Dim oTopCA As TOPCreditAccount = topCAFacade.Retrieve(ID) ' CType(aDatas(e.Item.ItemIndex), Dealer)
                Dim objDealer As Dealer = New DealerFacade(User).Retrieve(dealerId)
                If Not IsNothing(oTopCA) Then
                    oTopCA.Dealer = objDealer
                    If ddlStatusProses.SelectedValue <> "" Then
                        oTopCA.Status = ddlStatusProses.SelectedValue
                    End If

                    If ddlCaraPembayaranProses.SelectedValue <> "" Then
                        oTopCA.TermOfPayment = termOfPaymentFacade.Retrieve(CType(ddlCaraPembayaranProses.SelectedValue, Integer))
                    End If

                    oTopCA.KelipatanPembayaran = defaultKelipatanPembayaran

                    oTopCA.ID = topCAFacade.Update(oTopCA)
                    If oTopCA.ID > 0 Then
                        'MessageBox.Show("Data Berhasil Disimpan")
                    Else
                        result = False
                        'MessageBox.Show("Data Gagal Disimpan")
                        'BindDatagrid(dtgDealerList.CurrentPageIndex)
                    End If
                Else
                    oTopCA = New TOPCreditAccount
                    oTopCA.Dealer = objDealer
                    If ddlStatusProses.SelectedValue <> "" Then
                        oTopCA.Status = ddlStatusProses.SelectedValue
                    End If

                    If ddlCaraPembayaranProses.SelectedValue <> "" Then
                        oTopCA.TermOfPayment = termOfPaymentFacade.Retrieve(CType(ddlCaraPembayaranProses.SelectedValue, Integer))
                    End If

                    oTopCA.KelipatanPembayaran = defaultKelipatanPembayaran
                    oTopCA.ID = topCAFacade.Insert(oTopCA)
                    If oTopCA.ID > 0 Then
                        'MessageBox.Show("Data Berhasil Disimpan")
                    Else
                        result = False
                        'MessageBox.Show("Data Gagal Disimpan")
                        'BindDatagrid(dtgDealerList.CurrentPageIndex)
                    End If
                End If
            End If
        Next
        If result Then
            MessageBox.Show("Data Berhasil Disimpan")
        Else
            MessageBox.Show("Data/Sebagian Data Gagal Disimpan")
        End If
        BindDatagrid(dtgDealerList.CurrentPageIndex)
    End Sub

    Private Sub dtgDealerList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealerList.ItemDataBound
        Dim RowValue As VWI_DealerSettingCreditAccount = CType(e.Item.DataItem, VWI_DealerSettingCreditAccount)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            'o2nMe.dtgDealerList.Columns(Me.dtgDealerList.Columns.Count - 1).Visible
            'Dim defaultKelipatanPembayaran As String = KTB.DNet.Lib.WebConfig.GetValue("KelipatanPembayaran")
            'Dim txtKelipatanPembayaranE As TextBox = e.Item.FindControl("txtKelipatanPembayaranE")
            'If RowValue.id = 0 Then
            '    txtKelipatanPembayaranE.Text = defaultKelipatanPembayaran
            'End If

            Dim txtCreditAccountE As TextBox = e.Item.FindControl("txtCreditAccountE")
            txtCreditAccountE.Enabled = False 'Me.dtgDealerList.Columns(Me.dtgDealerList.Columns.Count - 2).Visible
            txtCreditAccountE.ReadOnly = Not (txtCreditAccountE.Enabled)

            Dim ddlTermOfPaymentGrid As DropDownList = e.Item.FindControl("ddlTermOfPaymentGrid")

            ddlTermOfPaymentGrid.DataSource = listOfPayments
            ddlTermOfPaymentGrid.DataValueField = "ID"
            ddlTermOfPaymentGrid.DataTextField = "Description"
            ddlTermOfPaymentGrid.DataBind()
            ddlTermOfPaymentGrid.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
            ddlTermOfPaymentGrid.SelectedValue = RowValue.TermOfPaymentID


            Dim ddlPrevTermOfPaymentGrid As DropDownList = e.Item.FindControl("ddlPrevTermOfPaymentGrid")
            Dim tCA As TOPCreditAccount
            If RowValue.id = 0 Then
                tCA = Nothing
            Else
                tCA = New TOPCreditAccountFacade(User).Retrieve(RowValue.id)
            End If

            ddlPrevTermOfPaymentGrid.DataSource = listOfPayments
            ddlPrevTermOfPaymentGrid.DataValueField = "ID"
            ddlPrevTermOfPaymentGrid.DataTextField = "Description"
            ddlPrevTermOfPaymentGrid.DataBind()
            ddlPrevTermOfPaymentGrid.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
            ddlPrevTermOfPaymentGrid.SelectedIndex = 0
            If Not IsNothing(tCA) AndAlso Not IsNothing(tCA.PrevTermOfPayment) Then
                ddlPrevTermOfPaymentGrid.SelectedValue = tCA.PrevTermOfPayment.ID
            End If
            ddlPrevTermOfPaymentGrid.Enabled = False

            If Not IsNothing(tCA) Then
                Dim lblLastUpdateTime As Label = CType(e.Item.FindControl("lblLastUpdateTime"), Label)
                Dim lblLastUpdateBy As Label = CType(e.Item.FindControl("lblLastUpdateBy"), Label)
                If tCA.LastUpdateBy.Trim.Length > 0 Then
                    lblLastUpdateBy.Text = tCA.LastUpdateBy.Remove(0, 6)
                Else
                    lblLastUpdateBy.Text = ""
                End If
                lblLastUpdateTime.Text = tCA.LastUpdateTime.ToString("dd MMMM yyyy")
            End If

            BoundRowItems(e)
        End If
    End Sub


    Private Function CreateAggreateForCountRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function GetUserInfoCriteria(ByVal nDealerID As Integer, ByVal cUserStatus As Byte) As ICriteria
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "Dealer.ID", MatchType.Exact, nDealerID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "UserStatus", MatchType.Exact, cUserStatus))
        Return criterias
    End Function

    Private Sub BoundRowItems(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        Dim objDealer As VWI_DealerSettingCreditAccount = CType(CType(dtgDealerList.DataSource, ArrayList)(e.Item.ItemIndex), VWI_DealerSettingCreditAccount)
        CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgDealerList.CurrentPageIndex * dtgDealerList.PageSize)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        If lblStatus.Text = "Tidak Aktif" Then
            lblStatus.ForeColor = Red
            e.Item.BackColor = Color.LightSalmon
        End If

        'privilege
        'ActivateUserPrivilege()
        If Not CType(e.Item.FindControl("lbtnEdit"), LinkButton) Is Nothing Then
            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = m_bFormPrivilege
        End If

        If Not CType(e.Item.FindControl("lbtnHakAkses"), LinkButton) Is Nothing Then
            CType(e.Item.FindControl("lbtnHakAkses"), LinkButton).Visible = m_bAdminAssigHakAccessOrganization_Privilege
        End If

        If Not CType(e.Item.FindControl("lbtnHakAkses"), LinkButton) Is Nothing Then
            'CType(e.Item.FindControl("lbtnHakAkses"), LinkButton).Visible = m_bFormPrivilege
        End If

        If Not CType(e.Item.FindControl("lblUserActive"), Label) Is Nothing Then
            CType(e.Item.FindControl("lblUserActive"), Label).Text = CType(New DealerFacade(User).RecordCount(CreateAggreateForCountRecord(GetType(UserInfo)), GetUserInfoCriteria(objDealer.id, EnumUserStatus.UserStatus.Aktif)), String)
        End If

    End Sub


    Private Sub dtgDealerList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgDealerList.ItemCommand
        If e.CommandName = "Save" Then
            If Not IsNothing(Me._sessHelper.GetSession(Me._sessDatas)) Then
                Dim aDatas As ArrayList = Me._sessHelper.GetSession(Me._sessDatas)
                Dim topCAFacade As New TOPCreditAccountFacade(User)
                Dim termOfPaymentFacade As New TermOfPaymentFacade(User)
                Dim ID As Integer = e.Item.Cells(0).Text

                Dim defaultKelipatanPembayaran As Integer = KTB.DNet.Lib.WebConfig.GetString("TOPKelipatanPembayaran")
                Dim kelPem As Integer = CInt(txtKelipatanPembayaran.Text)
                If kelPem <> defaultKelipatanPembayaran Then
                    MessageBox.Show("Simpan kelipatan pembayaran terlebih dahulu.")
                    Exit Sub
                End If
                Dim statusGrid As String = CType(e.Item.FindControl("lblStatus"), Label).Text
                Dim caraPembayaranGrid As Integer = CType(e.Item.FindControl("ddlTermOfPaymentGrid"), DropDownList).SelectedIndex
                Dim caraPembayaranSebelumnyaGrid As Integer = CType(e.Item.FindControl("ddlPrevTermOfPaymentGrid"), DropDownList).SelectedIndex
                If statusGrid = "Aktif" AndAlso caraPembayaranGrid = 0 AndAlso caraPembayaranSebelumnyaGrid = 0 Then
                    MessageBox.Show("Cara pembayaran harus dipilih")
                    Exit Sub
                End If
                'Dim txtKelipatanPembayaranE As TextBox = e.Item.FindControl("txtKelipatanPembayaranE") ' Me.dtgDealerList.Items(e.Item.ItemIndex).FindControl("txtCreditAccountE")
                Dim ddlTermOfPaymentGrid As DropDownList = e.Item.FindControl("ddlTermOfPaymentGrid")
                Dim selectedTOP As Integer = 0
                If ddlTermOfPaymentGrid.SelectedValue <> "" Then
                    selectedTOP = ddlTermOfPaymentGrid.SelectedValue
                End If

                Dim ddlPrevTermOfPaymentGrid As DropDownList = e.Item.FindControl("ddlPrevTermOfPaymentGrid")
                Dim prevSelectedTOP As Integer = 0
                If ddlPrevTermOfPaymentGrid.SelectedValue <> "" Then
                    prevSelectedTOP = ddlPrevTermOfPaymentGrid.SelectedValue
                End If

                Dim oTopCA As TOPCreditAccount = topCAFacade.Retrieve(ID) ' CType(aDatas(e.Item.ItemIndex), Dealer)
                Dim objDealer As Dealer = New DealerFacade(User).Retrieve(CInt(e.Item.Cells(1).Text.Trim()))
                If Not IsNothing(oTopCA) Then
                    oTopCA.Dealer = objDealer
                    oTopCA.KelipatanPembayaran = defaultKelipatanPembayaran
                    oTopCA.PrevTermOfPayment = oTopCA.TermOfPayment
                    oTopCA.TermOfPayment = termOfPaymentFacade.Retrieve(selectedTOP)

                    oTopCA.ID = topCAFacade.Update(oTopCA)
                    'If oTopCA.ID > 0 Then
                    '    MessageBox.Show("Data Berhasil Disimpan")
                    'Else
                    '    MessageBox.Show("Data Gagal Disimpan")
                    '    BindDatagrid(dtgDealerList.CurrentPageIndex)
                    'End If
                Else
                    oTopCA = New TOPCreditAccount
                    oTopCA.Dealer = objDealer
                    oTopCA.KelipatanPembayaran = defaultKelipatanPembayaran
                    oTopCA.TermOfPayment = termOfPaymentFacade.Retrieve(selectedTOP)

                    oTopCA.ID = topCAFacade.Insert(oTopCA)
                    'If oTopCA.ID > 0 Then
                    '    MessageBox.Show("Data Berhasil Disimpan")
                    'Else
                    '    MessageBox.Show("Data Gagal Disimpan")
                    '    BindDatagrid(dtgDealerList.CurrentPageIndex)
                    'End If
                End If
                If oTopCA.ID > 0 Then
                    MessageBox.Show("Data Berhasil Disimpan")
                Else
                    MessageBox.Show("Data Gagal Disimpan")
                End If
                BindDatagrid(dtgDealerList.CurrentPageIndex)
            End If
        End If
        If e.CommandName = "Edit" Then
            'Dim objDealer As Dealer = CType(CType(Session("sessDealer"), ArrayList)(e.Item.ItemIndex), Dealer)
            Response.Redirect("../General/FrmDealerMaintenance.aspx?dealerid=" + e.Item.Cells(0).Text.Trim() + "&user=MKS")
        End If
        If e.CommandName = "HakAkses" Then
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(CInt(e.Item.Cells(0).Text.Trim()))
            _sessHelper.SetSession("SessObjDealer", objDealer)
            Response.Redirect("../UserManagement/FrmAssignAccessibility.aspx")
        End If
    End Sub
    Private Sub dtgDealerList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDealerList.PageIndexChanged
        dtgDealerList.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgDealerList.CurrentPageIndex)
    End Sub

    Private Sub BindDdlPaymentType()
        ddlTermOfPayment.DataSource = listOfPayments
        ddlTermOfPayment.DataValueField = "ID"
        ddlTermOfPayment.DataTextField = "Description"
        ddlTermOfPayment.DataBind()
        ddlTermOfPayment.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))

        ddlPrevTermOfPayment.DataSource = listOfPayments
        ddlPrevTermOfPayment.DataValueField = "ID"
        ddlPrevTermOfPayment.DataTextField = "Description"
        ddlPrevTermOfPayment.DataBind()
        ddlPrevTermOfPayment.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))

        ddlCaraPembayaranProses.DataSource = listOfPayments
        ddlCaraPembayaranProses.DataValueField = "ID"
        ddlCaraPembayaranProses.DataTextField = "Description"
        ddlCaraPembayaranProses.DataBind()
        ddlCaraPembayaranProses.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
    End Sub

    Private Sub BindProvinceDropDown()
        Dim Provice_criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        ddlPropinsi.Items.Clear()
        ddlPropinsi.DataSource = New ProvinceFacade(User).RetrieveActiveList(Provice_criteria, "ProvinceName", Sort.SortDirection.ASC)
        ddlPropinsi.DataTextField = "ProvinceName"
        ddlPropinsi.DataValueField = "ID"
        ddlPropinsi.DataBind()
        ddlPropinsi.Items.Insert(0, New ListItem("Pilih Provinsi", 0))
        ddlPropinsi.SelectedIndex = 0
    End Sub

    Private Sub dtgDealerList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDealerList.SortCommand
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

        dtgDealerList.CurrentPageIndex = 0
        BindDatagrid(dtgDealerList.CurrentPageIndex)
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        If dtgDealerList.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        arrData = CType(_sessHelper.GetSession(_sessDatas), ArrayList)
        If arrData.Count > 0 Then
            CreateExcel("Daftar Credit Account", arrData)
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Kode Org"
            ws.Cells("C3").Value = "Nama Org"
            ws.Cells("D3").Value = "Kota"
            ws.Cells("E3").Value = "Propinsi"
            ws.Cells("F3").Value = "Grup"
            ws.Cells("G3").Value = "Cara Pembayaran"
            ws.Cells("H3").Value = "Cara Pembayaran Sebelumnya"
            ws.Cells("I3").Value = "Credit Account"
            ws.Cells("J3").Value = "Status"
            ws.Cells("K3").Value = "Tgl Diubah"
            ws.Cells("L3").Value = "Diubah Oleh"

            For i As Integer = 0 To Data.Count - 1
                Dim item As VWI_DealerSettingCreditAccount = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                ws.Cells(i + 4, 2).Value = item.DealerCode
                ws.Cells(i + 4, 3).Value = item.DealerName
                ws.Cells(i + 4, 4).Value = item.CityName
                ws.Cells(i + 4, 5).Value = item.ProvinceName
                ws.Cells(i + 4, 6).Value = item.GroupName

                Dim tCA As TOPCreditAccount
                If item.id = 0 Then
                    tCA = Nothing
                Else
                    tCA = New TOPCreditAccountFacade(User).Retrieve(item.id)
                End If
                If Not IsNothing(tCA) Then
                    If Not IsNothing(tCA.TermOfPayment) Then
                        ws.Cells(i + 4, 7).Value = tCA.TermOfPayment.Description
                    End If
                    If Not IsNothing(tCA.PrevTermOfPayment) Then
                        ws.Cells(i + 4, 8).Value = tCA.PrevTermOfPayment.Description
                    End If
                    ws.Cells(i + 4, 11).Value = tCA.LastUpdateTime
                    ws.Cells(i + 4, 12).Value = tCA.LastUpdateBy
                End If

                ws.Cells(i + 4, 9).Value = item.CreditAccount
                If item.Status = 0 Then
                    ws.Cells(i + 4, 10).Value = "Tidak Aktif"
                Else
                    ws.Cells(i + 4, 10).Value = "Aktif"
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

        'Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName)
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"  'xlsx
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()

    End Sub
End Class

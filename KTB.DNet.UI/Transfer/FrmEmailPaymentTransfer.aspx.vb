#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmEmailPaymentTransfer
    Inherits System.Web.UI.Page

    Private m_bFormPrivilege As Boolean = False
    Private sHelper As New SessionHelper
    Private _vstEmail As String = "_vst_FrmEmailPaymentTransfer"
    Private _edit_setting_email_transfer_Privilege As Boolean
    Private _lihat_setting_email_transfer_privilege As Boolean


    Private Sub checkPrivilege()
        Dim objDealer As Dealer = Me.sHelper.GetSession("DEALER")
        Dim _edit_setting_email_transfer_Privilege = SecurityProvider.Authorize(Context.User, SR.edit_setting_email_transfer_Privilege)
        Dim _lihat_setting_email_transfer_privilege = SecurityProvider.Authorize(Context.User, SR.lihat_setting_email_transfer_Privilege)

        'If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then '1=KTB
        '    If Not _lihat_setting_email_transfer_privilege Then
        '        Server.Transfer("../FrmAccessDenied.aspx?modulName=Sales - Transfer Payment")
        '    End If

        '    If Not _edit_setting_email_transfer_Privilege Then
        '        Dim idCol As Integer = dtgMain.Columns.Count()
        '        idCol = idCol - 1
        '        dtgMain.Columns(idCol).Visible = False
        '    End If

        'Else
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=Sales - Transfer Payment")
        'End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindDdlType()
            'BindDdlGroup()
            InitiatePage()
        End If
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PaymentNotifEmail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        sHelper.SetSession("CRITERIAS", criterias)
        dtgEmail.CurrentPageIndex = 0
        BindDatagrid(dtgEmail.CurrentPageIndex)
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objPaymentNotifEmail As PaymentNotifEmail = New PaymentNotifEmail
        Dim objPaymentNotifEmailFacade As PaymentNotifEmailFacade = New PaymentNotifEmailFacade(User)
        Dim nResult As Integer = -1

        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If txtKodeDealer.Text.Trim <> String.Empty Then
                Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)
                If Not IsNothing(objDealer) Then
                    objPaymentNotifEmail.Dealer = objDealer
                    objPaymentNotifEmail.Name = txtName.Text.Trim
                    objPaymentNotifEmail.Email = txtEmail.Text.Trim
                    objPaymentNotifEmail.ReceiverType = CType(ddlType.SelectedValue, Integer)
                    'objPaymentNotifEmail.EmailGroup = CType(ddlGroup.SelectedValue, Integer)
                    nResult = New PaymentNotifEmailFacade(User).Insert(objPaymentNotifEmail)
                    If nResult = -1 Then
                        MessageBox.Show("Simpan Gagal")
                    Else
                        MessageBox.Show("Simpan Sukses")
                    End If
                End If
            End If
        Else
            UpdatePaymentNotifEmail()
        End If

        If Not ViewState("vsProcess") Is Nothing Then
            If ViewState("vsProcess").ToString() = "Edit" Then
                RestoreCriterias()
            End If
        End If
        ClearData()
        dtgEmail.SelectedIndex = -1
        dtgEmail.CurrentPageIndex = 0
        BindDatagrid(dtgEmail.CurrentPageIndex)
    End Sub

    Private Sub dtgEmail_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgEmail.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewEmailReceiver(e.Item.Cells(0).Text, False)
            dtgEmail.SelectedIndex = e.Item.ItemIndex
            txtKodeDealer.ReadOnly = True
            txtName.ReadOnly = True
            txtEmail.ReadOnly = True
            'ddlGroup.Enabled = False
            ddlType.Enabled = False
        ElseIf e.CommandName = "Edit" Then
            SaveSearchCriterias()
            ViewState.Add("vsProcess", "Edit")
            ViewEmailReceiver(e.Item.Cells(0).Text, True)
            dtgEmail.SelectedIndex = e.Item.ItemIndex
            txtKodeDealer.ReadOnly = True
            txtName.ReadOnly = Not Me.m_bFormPrivilege '  
            txtEmail.ReadOnly = Not Me.m_bFormPrivilege '  
            'ddlGroup.Enabled = Me.m_bFormPrivilege ' 
            ddlType.Enabled = Me.m_bFormPrivilege ' 
        ElseIf e.CommandName = "Delete" Then
            DeleteEmailReceiver(e.Item.Cells(0).Text)
            txtKodeDealer.ReadOnly = True
            txtName.ReadOnly = Not Me.m_bFormPrivilege '  
            txtEmail.ReadOnly = Not Me.m_bFormPrivilege '  
            'ddlGroup.Enabled = Me.m_bFormPrivilege ' 
            ddlType.Enabled = Me.m_bFormPrivilege ' 
            ClearData()
        End If
    End Sub

    Private Sub dtgEmail_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgEmail.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgEmail.CurrentPageIndex * dtgEmail.PageSize)

            Dim RowValue As PaymentNotifEmail = CType(e.Item.DataItem, PaymentNotifEmail)
            Dim lblType As Label = CType(e.Item.FindControl("lblType"), Label)
            lblType.Text = EnumEmailNotification.GetStringValue(RowValue.ReceiverType)
            
        End If
    End Sub

    Private Sub dtgEmail_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgEmail.PageIndexChanged
        dtgEmail.SelectedIndex = -1
        dtgEmail.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgEmail.CurrentPageIndex)
    End Sub

    Private Sub dtgEmail_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgEmail.SortCommand
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

        dtgEmail.SelectedIndex = -1
        dtgEmail.CurrentPageIndex = 0
        BindDatagrid(dtgEmail.CurrentPageIndex)
    End Sub

#Region "Custom"

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.ChangeType_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.lihat_setting_email_transfer_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SALES - Penerima Email Notifikasi Payment Transfer")
        End If
        Me.ViewState.Add(Me._vstEmail, "0")
        If SecurityProvider.Authorize(Context.User, SR.edit_setting_email_transfer_Privilege) Then
            Me.ViewState.Item(Me._vstEmail) = "1"
        End If
    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "Dealer.ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Private Sub ClearData()
        txtKodeDealer.Enabled = True
        txtKodeDealer.Text = String.Empty

        txtName.Enabled = True
        txtName.Text = String.Empty
        txtName.ReadOnly = False
        txtEmail.Enabled = True
        txtEmail.Text = String.Empty
        txtEmail.ReadOnly = False

        'ddlGroup.Enabled = True
        ddlType.Enabled = True

        ViewState.Add("vsProcess", "Insert")

    End Sub

    Private Sub SetControlPrivilege()
        Dim m_bFormPrivilege_ori As Boolean = m_bFormPrivilege
        If Me.ViewState.Item(Me._vstEmail) = "1" Then m_bFormPrivilege = True
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
        m_bFormPrivilege = m_bFormPrivilege_ori
    End Sub

    Private Sub BindDdlType()

        Dim listType As New EnumEmailNotification
        Dim al As ArrayList = listType.RetrieveType
        For Each item As EnumEmailNotificationProperty In al
            ddlType.Items.Insert(item.ValType, New ListItem(item.NameType, item.ValType))
        Next
        ddlType.SelectedIndex = 0
    End Sub

    ''--Binding Data Down List Category
    'Private Sub BindDdlGroup()
    '    Dim listItemBlank As New ListItem("Silahkan Pilih", -1)
    '    ddlGroup.DataSource = New CategoryFacade(User).RetrieveList("Description", Sort.SortDirection.ASC)
    '    ddlGroup.DataValueField = "ID"
    '    ddlGroup.DataTextField = "Description"
    '    ddlGroup.DataBind()
    '    ddlGroup.Items.Insert(0, listItemBlank)
    'End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            Dim cVT As New CriteriaComposite(New Criteria(GetType(PaymentNotifEmail), "ID", MatchType.Greater, 0))
            If IsNothing(sHelper.GetSession("CRITERIAS")) Then
                sHelper.SetSession("CRITERIAS", cVT)
            End If
            dtgEmail.DataSource = New PaymentNotifEmailFacade(User).RetrieveActiveList(CType(sHelper.GetSession("CRITERIAS"), CriteriaComposite), indexPage + 1, dtgEmail.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgEmail.VirtualItemCount = totalRow
            dtgEmail.DataBind()
        End If

    End Sub

    Private Sub UpdatePaymentNotifEmail()
        Dim objPaymentNotifEmail As PaymentNotifEmail = CType(Session.Item("vsPaymentNotifEmail"), PaymentNotifEmail)
        objPaymentNotifEmail.Name = txtName.Text.Trim
        objPaymentNotifEmail.Email = txtEmail.Text.Trim
        objPaymentNotifEmail.ReceiverType = CType(ddlType.SelectedValue, Integer)
        'objPaymentNotifEmail.EmailGroup = CType(ddlGroup.SelectedValue, Integer)
        Dim nResult As Integer = New PaymentNotifEmailFacade(User).Update(objPaymentNotifEmail)
        If nResult = -1 Then
            MessageBox.Show("Simpan Gagal")
        Else
            MessageBox.Show("Simpan Sukses")
        End If
    End Sub

    Private Sub ViewEmailReceiver(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objPaymentNotifEmail As PaymentNotifEmail = New PaymentNotifEmailFacade(User).Retrieve(nID)
        Session.Add("vsPaymentNotifEmail", objPaymentNotifEmail)
        txtKodeDealer.Text = objPaymentNotifEmail.Dealer.DealerCode
        txtName.Text = objPaymentNotifEmail.Name
        txtEmail.Text = objPaymentNotifEmail.Email
        ddlType.SelectedValue = objPaymentNotifEmail.ReceiverType
        'ddlGroup.SelectedValue = objPaymentNotifEmail.EmailGroup
        
        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub DeleteEmailReceiver(ByVal nID As Integer)

        Dim objPaymentNotifEmail As PaymentNotifEmail = New PaymentNotifEmailFacade(User).Retrieve(nID)
        objPaymentNotifEmail.RowStatus = DBRowStatus.Deleted
        Dim facade As PaymentNotifEmailFacade = New PaymentNotifEmailFacade(User)
        facade.Delete(objPaymentNotifEmail)
        dtgEmail.CurrentPageIndex = 0
        BindDatagrid(dtgEmail.CurrentPageIndex)

    End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        If ddlType.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PaymentNotifEmail), "ReceiverType", MatchType.Exact, ddlType.SelectedValue))
        End If
        'If ddlGroup.SelectedValue > 0 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PaymentNotifEmail), "EmailGroup", MatchType.Exact, ddlGroup.SelectedValue))
        'End If
        If txtKodeDealer.Text <> String.Empty Then
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PaymentNotifEmail), "Dealer.ID", MatchType.Exact, objDealer.ID))
        End If
        If txtEmail.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PaymentNotifEmail), "Email", MatchType.[Partial], txtEmail.Text))
        End If
        If txtName.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PaymentNotifEmail), "Name", MatchType.[Partial], txtName.Text))
        End If

    End Sub

    Private Sub SaveSearchCriterias()
        Dim objHash As Hashtable = SearchCriterias
        objHash.Remove(ddlType.ID)
        objHash.Add(ddlType.ID, ddlType.SelectedValue)

        'objHash.Remove(ddlGroup.ID)
        'objHash.Add(ddlGroup.ID, ddlGroup.SelectedValue)

        objHash.Remove(txtName.ID)
        objHash.Add(txtName.ID, txtName.Text)

        objHash.Remove(txtEmail.ID)
        objHash.Add(txtEmail.ID, txtEmail.Text)

        objHash.Remove(txtKodeDealer.ID)
        objHash.Add(txtKodeDealer.ID, txtKodeDealer.Text)

    End Sub

    Private Sub RestoreCriterias()
        Dim objHash As Hashtable = SearchCriterias

        ddlType.SelectedValue = CStr(objHash(ddlType.ID))
        'ddlGroup.SelectedValue = objHash(ddlGroup.ID)
        txtKodeDealer.Text = objHash(txtKodeDealer.ID)
        txtName.Text = objHash(txtName.ID)
        txtEmail.Text = objHash(txtEmail.ID)
    End Sub

    Private ReadOnly Property SearchCriterias() As Hashtable
        Get
            If ViewState("SearchCriterias") Is Nothing Then
                Dim objHash As Hashtable = New Hashtable

                ViewState("SearchCriterias") = objHash
            End If

            Return CType(ViewState("SearchCriterias"), Hashtable)
        End Get
    End Property

#End Region

End Class
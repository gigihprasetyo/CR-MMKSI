#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.OnlinePayment
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

#End Region

Public Class FrmEntryObligationType
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtKode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtdescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgObligationType As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


#Region "Custom Declaration"
#End Region

#Region "Custom Method"

    Private Sub InitiatePage()
        bindStatus()
        ClearData()
        ViewState("CurrentSortColumn") = "Code"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub bindStatus()
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = New EnumObligationType().RetrieveObligationTypeStatus()
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataBind()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            dtgObligationType.DataSource = New PaymentObligationTypeFacade(User).RetrieveActiveList(indexPage + 1, dtgObligationType.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgObligationType.VirtualItemCount = totalRow
            dtgObligationType.DataBind()
        End If
    End Sub

    Private Sub ClearData()
        txtKode.Text = String.Empty
        txtdescription.Text = String.Empty
        ddlStatus.SelectedIndex = 0
        txtKode.ReadOnly = False
        txtdescription.ReadOnly = False
        ddlStatus.Enabled = True
        ViewState.Add("vsProcess", "Insert")

        btnSimpan.Visible = True
        btnBatal.Visible = False
    End Sub

    Private Sub UpdateArea()
        Dim objPaymentObligationType As PaymentObligationType = CType(Session.Item("vsPaymentObligationType"), PaymentObligationType)
        objPaymentObligationType.Description = txtdescription.Text
        objPaymentObligationType.Status = ddlStatus.SelectedValue
        Dim nResult = New PaymentObligationTypeFacade(User).Update(objPaymentObligationType)
    End Sub

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal PaymentObligationTypeID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "PaymentObligationType.ID", MatchType.Exact, PaymentObligationTypeID))
        Return criterias
    End Function

    Private Sub DeleteArea(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0

        If New HelperFacade(User, GetType(PaymentObligation)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(PaymentObligation), nID), _
            CreateAggreateForCheckRecord(GetType(PaymentObligation))) Then
            iRecordCount = iRecordCount + 1
        End If

        If iRecordCount > 0 Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objPaymentObligationType As PaymentObligationType = New PaymentObligationTypeFacade(User).Retrieve(nID)
            Dim facade As PaymentObligationTypeFacade = New PaymentObligationTypeFacade(User)
            facade.Delete(objPaymentObligationType)
            dtgObligationType.CurrentPageIndex = 0
            BindDataGrid(dtgObligationType.CurrentPageIndex)
        End If
    End Sub

    Private Sub ViewArea(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objPaymentObligationType As PaymentObligationType = New PaymentObligationTypeFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsPaymentObligationType", objPaymentObligationType)
        txtKode.Text = objPaymentObligationType.Code
        txtdescription.Text = objPaymentObligationType.Description
        ddlStatus.SelectedValue = objPaymentObligationType.Status
        If EditStatus Then
            Me.btnSimpan.Enabled = CekPrivCreate()
        Else
            Me.btnSimpan.Enabled = EditStatus
        End If
    End Sub

    'Private Sub CheckUserPrivilege()
    '    If Not SecurityProvider.Authorize(Context.User, SR.TujuanPembayaranView_Privilege) Then
    '        Server.Transfer("../FrmAccessDenied.aspx?modulName=Tujuan Pembayaran")
    '    End If
    '    btnSimpan.Visible = SecurityProvider.Authorize(Context.User, SR.TujuanPembayaranUpdate_Privilege)
    '    btnBatal.Visible = SecurityProvider.Authorize(Context.User, SR.TujuanPembayaranUpdate_Privilege)
    'End Sub
#End Region

#Region "Privilage"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.Pembayaran_tipe_pembayaran_lihat_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=CLAIM - Daftar Status")
        End If
    End Sub

    Private Function CekPrivCreate() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.Pembayaran_tipe_pembayaran_buat_privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            'CheckUserPrivilege()
            InitiatePage()
            BindDataGrid(0)
        End If
        btnSimpan.Enabled = CekPrivCreate()
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objPaymentObligationType As PaymentObligationType = New PaymentObligationType
        Dim objPaymentObligationTypeFacade As PaymentObligationTypeFacade = New PaymentObligationTypeFacade(User)
        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtKode.Text = String.Empty Then
                If objPaymentObligationTypeFacade.ValidateCode(txtKode.Text) <= 0 Then
                    objPaymentObligationType.Code = txtKode.Text
                    objPaymentObligationType.Description = txtdescription.Text
                    objPaymentObligationType.Status = ddlStatus.SelectedValue
                    nResult = New PaymentObligationTypeFacade(User).Insert(objPaymentObligationType)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                    End If
                Else
                    objPaymentObligationType = objPaymentObligationTypeFacade.RetrieveAllStatus(txtKode.Text)
                    If objPaymentObligationType.RowStatus = DBRowStatus.Active Then
                        MessageBox.Show(SR.DataIsExist("Kode " & objPaymentObligationType.Code))
                    Else
                        objPaymentObligationType.Description = txtdescription.Text
                        objPaymentObligationType.Status = ddlStatus.SelectedValue
                        objPaymentObligationType.RowStatus = DBRowStatus.Active
                        nResult = New PaymentObligationTypeFacade(User).Update(objPaymentObligationType)
                    End If
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Privilege"))
            End If
        Else
            UpdateArea()
        End If

        ClearData()
        dtgObligationType.CurrentPageIndex = 0
        BindDataGrid(dtgObligationType.CurrentPageIndex)
        dtgObligationType.SelectedIndex = -1
    End Sub

    Private Sub dtgObligationType_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgObligationType.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            Dim RowData As PaymentObligationType = CType(e.Item.DataItem, PaymentObligationType)
            Dim LbnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim LbnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim LbnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            LbnDelete.Visible = CekPrivCreate()
            LbnEdit.Visible = CekPrivCreate()
            LbnView.Visible = CekPrivCreate()
            'If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            '    CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            'End If
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgObligationType.CurrentPageIndex * dtgObligationType.PageSize)

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

            Select Case CType(RowData.Status, EnumObligationType.ObligationTypeStatus)
                Case EnumObligationType.ObligationTypeStatus.Aktif
                    lblStatus.Text = "Aktif"
                Case EnumObligationType.ObligationTypeStatus.TidakAktif
                    lblStatus.Text = "Tidak Aktif"
            End Select

        End If

    End Sub

    Private Sub dtgObligationType_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgObligationType.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewArea(e.Item.Cells(0).Text, False)
            txtKode.ReadOnly = True
            txtdescription.ReadOnly = True
            ddlStatus.Enabled = False

            btnBatal.Text = "Baru"
            btnBatal.Visible = True
            btnSimpan.Visible = False
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewArea(e.Item.Cells(0).Text, True)
            dtgObligationType.SelectedIndex = e.Item.ItemIndex
            txtKode.ReadOnly = True
            txtdescription.ReadOnly = False
            ddlStatus.Enabled = True

            btnBatal.Text = "Batal"
            btnBatal.Visible = True
            btnSimpan.Visible = True
        ElseIf e.CommandName = "Delete" Then
            DeleteArea(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        dtgObligationType.SelectedIndex = -1
        txtKode.ReadOnly = False

        If btnBatal.Text = "Baru" Then
            btnSimpan.Enabled = CekPrivCreate()
        End If
    End Sub

    Private Sub dtgObligationType_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgObligationType.SortCommand
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

        dtgObligationType.SelectedIndex = -1
        dtgObligationType.CurrentPageIndex = 0
        BindDataGrid(dtgObligationType.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgObligationType_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgObligationType.PageIndexChanged
        dtgObligationType.SelectedIndex = -1
        dtgObligationType.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgObligationType.CurrentPageIndex)
        ClearData()
    End Sub

#End Region


End Class


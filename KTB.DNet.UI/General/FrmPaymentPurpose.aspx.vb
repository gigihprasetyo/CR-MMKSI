#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

#End Region

Public Class FrmPaymentPurpose
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtdescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKode As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgPaymentPuspose As System.Web.UI.WebControls.DataGrid

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
        ClearData()
        ViewState("CurrentSortColumn") = "PaymentPurposeCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            dtgPaymentPuspose.DataSource = New PaymentPurposeFacade(User).RetrieveActiveList(indexPage + 1, dtgPaymentPuspose.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgPaymentPuspose.VirtualItemCount = totalRow
            dtgPaymentPuspose.DataBind()
        End If
    End Sub

    Private Sub ClearData()
        txtKode.Text = String.Empty
        txtdescription.Text = String.Empty
        btnSimpan.Enabled = True
        txtKode.ReadOnly = False
        txtdescription.ReadOnly = False
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub UpdateArea()
        Dim objPaymentPurpose As PaymentPurpose = CType(Session.Item("vsPaymentPurpose"), PaymentPurpose)
        objPaymentPurpose.Description = txtdescription.Text
        Dim nResult = New PaymentPurposeFacade(User).Update(objPaymentPurpose)
    End Sub

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal PaymentPurpose As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "PaymentPurpose.ID", MatchType.Exact, PaymentPurpose))
        Return criterias
    End Function

    Private Sub DeleteArea(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0

        If New HelperFacade(User, GetType(DailyPayment)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(DailyPayment), nID), _
            CreateAggreateForCheckRecord(GetType(DailyPayment))) Then
            iRecordCount = iRecordCount + 1
        End If

        If iRecordCount > 0 Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objPaymentPurpose As PaymentPurpose = New PaymentPurposeFacade(User).Retrieve(nID)
            Dim facade As PaymentPurposeFacade = New PaymentPurposeFacade(User)
            facade.DeleteFromDB(objPaymentPurpose)
            dtgPaymentPuspose.CurrentPageIndex = 0
            BindDataGrid(dtgPaymentPuspose.CurrentPageIndex)
        End If
    End Sub

    Private Sub ViewArea(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objPaymentPurpose As PaymentPurpose = New PaymentPurposeFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsPaymentPurpose", objPaymentPurpose)
        txtKode.Text = objPaymentPurpose.PaymentPurposeCode
        txtdescription.Text = objPaymentPurpose.Description
        Me.btnSimpan.Enabled = EditStatus
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            CheckUserPrivilege()
            BindDataGrid(0)
            InitiatePage()
        End If
    End Sub
    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.TujuanPembayaranView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Tujuan Pembayaran")
        End If
        btnSimpan.Visible = SecurityProvider.Authorize(Context.User, SR.TujuanPembayaranUpdate_Privilege)
        btnBatal.Visible = SecurityProvider.Authorize(Context.User, SR.TujuanPembayaranUpdate_Privilege)
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objPaymentPurpose As PaymentPurpose = New PaymentPurpose
        Dim objPaymentPurposeFacade As PaymentPurposeFacade = New PaymentPurposeFacade(User)
        Dim nResult As Integer = -1
        txtKode.ReadOnly = False
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtKode.Text = String.Empty Then
                If objPaymentPurposeFacade.ValidateCode(txtKode.Text) <= 0 Then
                    objPaymentPurpose.PaymentPurposeCode = txtKode.Text
                    objPaymentPurpose.Description = txtdescription.Text
                    nResult = New PaymentPurposeFacade(User).Insert(objPaymentPurpose)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Privilege"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Privilege"))
            End If
        Else
            UpdateArea()
        End If

        ClearData()
        dtgPaymentPuspose.CurrentPageIndex = 0
        BindDataGrid(dtgPaymentPuspose.CurrentPageIndex)
    End Sub

    Private Sub dtgPaymentPuspose_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPaymentPuspose.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            Dim LbnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim LbnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            LbnDelete.Visible = SecurityProvider.Authorize(Context.User, SR.TujuanPembayaranUpdate_Privilege)
            LbnEdit.Visible = SecurityProvider.Authorize(Context.User, SR.TujuanPembayaranUpdate_Privilege)

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            End If
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgPaymentPuspose.CurrentPageIndex * dtgPaymentPuspose.PageSize)
        End If

    End Sub

    Private Sub dtgPaymentPuspose_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPaymentPuspose.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewArea(e.Item.Cells(0).Text, False)
            txtKode.ReadOnly = True
            txtdescription.ReadOnly = True
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewArea(e.Item.Cells(0).Text, True)
            dtgPaymentPuspose.SelectedIndex = e.Item.ItemIndex
            txtKode.ReadOnly = True
            txtdescription.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            DeleteArea(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub

    Private Sub btnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewState.Clear()
        Response.Redirect("../default.aspx")
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        txtKode.ReadOnly = False
    End Sub

    Private Sub dtgPaymentPuspose_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPaymentPuspose.SortCommand
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

        dtgPaymentPuspose.SelectedIndex = -1
        dtgPaymentPuspose.CurrentPageIndex = 0
        BindDataGrid(dtgPaymentPuspose.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgPaymentPuspose_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPaymentPuspose.PageIndexChanged
        dtgPaymentPuspose.SelectedIndex = -1
        dtgPaymentPuspose.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgPaymentPuspose.CurrentPageIndex)
        ClearData()
    End Sub

#End Region


End Class

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search

Public Class FrmTermOfPayment
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

  End Sub
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnTutup As System.Web.UI.WebControls.Button
    Protected WithEvents txtKodePembayaran As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents dtgTermOfPayment As System.Web.UI.WebControls.DataGrid
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents txtJumlahHari As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private m_bFormPrivilege As Boolean = False

#Region "Custom Method"
    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "TermOfPaymentCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            dtgTermOfPayment.DataSource = New TermOfPaymentFacade(User).RetrieveActiveList(indexPage + 1, dtgTermOfPayment.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgTermOfPayment.VirtualItemCount = totalRow
            dtgTermOfPayment.DataBind()
        End If

    End Sub
    Private Sub ClearData()
        txtKodePembayaran.Text() = String.Empty
        txtJumlahHari.Text() = String.Empty
        txtDeskripsi.Text() = String.Empty
        btnSimpan.Enabled = True
        txtKodePembayaran.ReadOnly = False
        txtJumlahHari.ReadOnly = False
        txtDeskripsi.ReadOnly = False
        dtgTermOfPayment.SelectedIndex = -1
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.ChangeTermOfPaynent_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.ViewTermOfPaynent_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=GENERAL - Cara Pembayaran")
        End If

    End Sub

    Private Sub UpdateArea()
        Dim objTermOfPayment As TermOfPayment = CType(Session.Item("vsTermOfPayment"), TermOfPayment)
        objTermOfPayment.TermOfPaymentValue = txtJumlahHari.Text
        objTermOfPayment.Description = txtDeskripsi.Text
        Dim nResult As Integer = -1
        Try
            nResult = New TermOfPaymentFacade(User).Update(objTermOfPayment)
            MessageBox.Show(SR.UpdateSucces)
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try
    End Sub

    'Private Function KodeInUse(ByVal nID As Integer) As Boolean
    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.POHeader), "TermOfPayment.ID", MatchType.Exact, nID))
    '    Dim arlTermOfPayment As ArrayList = New POHeaderFacade(User).Retrieve(criterias)
    '    Return arlTermOfPayment.Count > 0
    'End Function

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
    ByVal TermOfPaymentID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "TermOfPayment", MatchType.Exact, TermOfPaymentID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Sub DeleteTermOfPayment(ByVal nID As Integer)
        If New HelperFacade(User, GetType(POHeader)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(POHeader), nID), _
            CreateAggreateForCheckRecord(GetType(POHeader))) Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Try
                Dim objTermOfPayment As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(nID)
                Dim facade As TermOfPaymentFacade = New TermOfPaymentFacade(User)
                facade.DeleteFromDB(objTermOfPayment)
                MessageBox.Show(SR.DeleteSucces)
                dtgTermOfPayment.CurrentPageIndex = 0
                BindDataGrid(dtgTermOfPayment.CurrentPageIndex)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
                dtgTermOfPayment.SelectedIndex = -1
                ClearData()
            End Try
        End If
    End Sub

    Private Sub ViewTermOfPayment(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objTermOfPayment As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(nID)
        If Not objTermOfPayment Is Nothing Then
            'Todo session
            Session.Add("vsTermOfPayment", objTermOfPayment)
            txtKodePembayaran.Text = objTermOfPayment.TermOfPaymentCode
            txtJumlahHari.Text = objTermOfPayment.TermOfPaymentValue
            txtDeskripsi.Text = objTermOfPayment.Description
            Me.btnSimpan.Enabled = EditStatus
        Else
            If EditStatus Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.ViewFail)
            End If
            dtgTermOfPayment.SelectedIndex = -1
            ClearData()
        End If
    End Sub

#End Region

#Region "Event Handlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objTermOfPayment As TermOfPayment = New TermOfPayment
        Dim objTermOfPaymentFacade As TermOfPaymentFacade = New TermOfPaymentFacade(User)
        Dim nResult As Integer = -1
        txtKodePembayaran.ReadOnly = False
        txtJumlahHari.ReadOnly = False
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtKodePembayaran.Text = String.Empty Then
                If objTermOfPaymentFacade.ValidateCode(txtKodePembayaran.Text) <= 0 Then
                    objTermOfPayment.TermOfPaymentCode = txtKodePembayaran.Text
                    objTermOfPayment.TermOfPaymentValue = txtJumlahHari.Text
                    objTermOfPayment.Description = txtDeskripsi.Text
                    nResult = New TermOfPaymentFacade(User).Insert(objTermOfPayment)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("kode Pembayaran"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode Pembayaran"))
            End If
        Else
            UpdateArea()
        End If
        ClearData()
        dtgTermOfPayment.CurrentPageIndex = 0
        BindDataGrid(dtgTermOfPayment.CurrentPageIndex)
    End Sub

    Private Sub dtgTermOfPayment_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgTermOfPayment.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewTermOfPayment(e.Item.Cells(0).Text, False)
            txtKodePembayaran.ReadOnly = True
            txtJumlahHari.ReadOnly = True
            txtDeskripsi.ReadOnly = True
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewTermOfPayment(e.Item.Cells(0).Text, True)
            dtgTermOfPayment.SelectedIndex = e.Item.ItemIndex
            txtKodePembayaran.ReadOnly = True
            txtJumlahHari.ReadOnly = True
            txtDeskripsi.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            DeleteTermOfPayment(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub
    Private Sub dtgTermOfPayment_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgTermOfPayment.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgTermOfPayment.CurrentPageIndex * dtgTermOfPayment.PageSize)
        End If
        If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = m_bFormPrivilege
        End If

        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = m_bFormPrivilege
        End If
    End Sub

    Private Sub btnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTutup.Click
        ViewState.Clear()
        Response.Redirect("../default.aspx")
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        txtKodePembayaran.ReadOnly = False
        txtJumlahHari.ReadOnly = False
    End Sub

    Private Sub dtgTermOfPayment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgTermOfPayment.SelectedIndexChanged

    End Sub

    Private Sub dtgTermOfPayment_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgTermOfPayment.SortCommand
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

        dtgTermOfPayment.SelectedIndex = -1
        dtgTermOfPayment.CurrentPageIndex = 0
        BindDataGrid(dtgTermOfPayment.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgTermOfPayment_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgTermOfPayment.PageIndexChanged
        dtgTermOfPayment.SelectedIndex = -1
        dtgTermOfPayment.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgTermOfPayment.CurrentPageIndex)
        ClearData()
    End Sub
#End Region
End Class

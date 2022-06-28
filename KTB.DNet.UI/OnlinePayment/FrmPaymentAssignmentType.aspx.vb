#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.OnlinePayment
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

#End Region

Public Class FrmPaymentAssignmentType
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtdescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPaymentObligationType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboProcess As System.Web.UI.WebControls.CheckBox
    Protected WithEvents dtgPaymentAssignmentType As System.Web.UI.WebControls.DataGrid
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator2 As System.Web.UI.WebControls.RequiredFieldValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Privilage"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.Pembayaran_assignment_lihat_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=CLAIM - Daftar Status")
        End If
    End Sub

    Private Function CekPrivCreate() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.Pembayaran_assignment_buat_privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "EventHandler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        If Not IsPostBack Then
            InitiatePage()
        End If
        btnSimpan.Enabled = CekPrivCreate()
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Try
            If Not ValidateData() Then
                Return
            End If

            If CType(ViewState("vsProcess"), String) = "Insert" Then
                insertData()
            ElseIf CType(ViewState("vsProcess"), String) = "Edit" Then
                UpdateData()
            End If
            clearData()

            bindToGrid(0)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        clearData()

    End Sub

    Private Sub dtgPaymentAssignmentType_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPaymentAssignmentType.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            Dim RowData As PaymentAssignmentType = CType(e.Item.DataItem, PaymentAssignmentType)
            Dim LbnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            LbnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            Dim LbnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim LbnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            Dim lblStatusAktif As Label = CType(e.Item.FindControl("lblStatusAktif"), Label)
            Dim lblStatusInAktif As Label = CType(e.Item.FindControl("lblStatusInAktif"), Label)
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgPaymentAssignmentType.CurrentPageIndex * dtgPaymentAssignmentType.PageSize)
            If RowData.Status = CInt(EnumOnlinePayment.OLStatus.Active) Then
                lblStatusAktif.Visible = True
                lblStatusInAktif.Visible = False
            Else
                lblStatusAktif.Visible = False
                lblStatusInAktif.Visible = True
            End If

            Dim lblSourceDoc As Label = CType(e.Item.FindControl("lblSourceDoc"), Label)
            Select Case CInt(lblSourceDoc.Text)
                Case 0
                    lblSourceDoc.Text = EnumOnlinePayment.SourceDocument.SAP.ToString
                Case 1
                    lblSourceDoc.Text = EnumOnlinePayment.SourceDocument.MANUAL.ToString()
            End Select

            Dim lbtnviewPIC As LinkButton = CType(e.Item.FindControl("lbtnviewPIC"), LinkButton)
            lbtnviewPIC.CommandArgument = RowData.ID
            LbnEdit.Visible = CekPrivCreate()
            LbnDelete.Visible = CekPrivCreate()
            LbnView.Visible = CekPrivCreate()
        End If
    End Sub

    Private Sub dtgPaymentAssignmentType_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPaymentAssignmentType.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewArea(e.Item.Cells(0).Text, False)
            dtgPaymentAssignmentType.SelectedIndex = e.Item.ItemIndex
            txtKode.ReadOnly = True
            txtdescription.ReadOnly = True
            ddlStatus.Enabled = False
            ddlPaymentObligationType.Enabled = False
            cboProcess.Enabled = False
            btnSimpan.Enabled = False
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewArea(e.Item.Cells(0).Text, True)
            dtgPaymentAssignmentType.SelectedIndex = e.Item.ItemIndex
            txtKode.ReadOnly = True
            txtdescription.ReadOnly = False
            cboProcess.Enabled = True
            ddlStatus.Enabled = True
            ddlPaymentObligationType.Enabled = True
            btnSimpan.Enabled = CekPrivCreate()
        ElseIf e.CommandName = "Delete" Then
            DeleteArea(e.Item.Cells(0).Text)
            'clearData()
        ElseIf e.CommandName = "PIC" Then
            Response.Redirect("../OnlinePayment/FrmPaymentAssignmentTypeReff.aspx?Id=" & e.CommandArgument)
        End If
    End Sub

    Private Sub dtgPaymentAssignmentType_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPaymentAssignmentType.PageIndexChanged
        dtgPaymentAssignmentType.SelectedIndex = -1
        dtgPaymentAssignmentType.CurrentPageIndex = e.NewPageIndex
        bindToGrid(dtgPaymentAssignmentType.CurrentPageIndex)
        clearData()
    End Sub

    Private Sub dtgPaymentAssignmentType_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPaymentAssignmentType.SortCommand
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

        dtgPaymentAssignmentType.SelectedIndex = -1
        dtgPaymentAssignmentType.CurrentPageIndex = 0
        bindToGrid(dtgPaymentAssignmentType.CurrentPageIndex)
        clearData()
    End Sub


#End Region

#Region "Custom Method"
    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "Code"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        BindDdl()
        clearData()
        bindToGrid(0)
    End Sub

    Private Sub BindDdl()
        '---list PaymentObligationType
        ddlPaymentObligationType.Items.Clear()
        Dim arrPaymentObligationType As ArrayList = New ArrayList
        Try
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligationType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PaymentObligationType), "Status", MatchType.Exact, CType(EnumObligationType.ObligationTypeStatus.Aktif, Integer)))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(PaymentObligationType), "Description", Sort.SortDirection.ASC))

            arrPaymentObligationType = New PaymentObligationTypeFacade(User).Retrieve(criterias, sortColl)
            ddlPaymentObligationType.Items.Add(New ListItem("Silahkan Pilih ", "-1"))
            For Each item As PaymentObligationType In arrPaymentObligationType
                Dim _temp As New ListItem(item.Code, item.ID)
                ddlPaymentObligationType.Items.Add(_temp)
            Next
            ddlPaymentObligationType.SelectedIndex = -1
        Catch ex As Exception

        End Try

        '---list Status
        ddlStatus.Items.Clear()
        Dim _statusOnlinePayment As New EnumOnlinePayment
        For Each item As OnlinePaymentItem In _statusOnlinePayment.OLStatusList
            Dim _temp As New ListItem(item.OPCode, item.OPValue)
            ddlStatus.Items.Add(_temp)
        Next
        ddlStatus.SelectedIndex = 0

    End Sub

    Private Sub clearData()
        txtKode.Text = String.Empty
        txtdescription.Text = String.Empty
        ddlPaymentObligationType.SelectedIndex = -1
        ddlStatus.SelectedIndex = 0
        ViewState.Add("vsProcess", "Insert")
        cboProcess.Checked = False
        dtgPaymentAssignmentType.SelectedIndex = -1

        txtKode.ReadOnly = False
        txtdescription.ReadOnly = False

        txtKode.Enabled = True
        txtdescription.Enabled = True
        ddlPaymentObligationType.Enabled = True
        ddlStatus.Enabled = True
        cboProcess.Enabled = True
        btnSimpan.Enabled = CekPrivCreate()
    End Sub

    Private Sub insertData()
        If isExist() Then
            MessageBox.Show("Data Kode Sudah Ada!")
            Return
        End If

        Dim oPaymentAssignmentType As PaymentAssignmentType = New PaymentAssignmentType
        oPaymentAssignmentType.Code = txtKode.Text.Trim
        oPaymentAssignmentType.Description = txtdescription.Text.Trim
        oPaymentAssignmentType.Status = CType(ddlStatus.SelectedValue, Integer)
        If cboProcess.Checked Then
            oPaymentAssignmentType.SourceDocument = EnumOnlinePayment.SourceDocument.MANUAL
        Else
            oPaymentAssignmentType.SourceDocument = EnumOnlinePayment.SourceDocument.SAP
        End If
        Dim oPaymentObligationType As PaymentObligationType = New PaymentObligationType
        oPaymentObligationType = New PaymentObligationTypeFacade(User).Retrieve(CInt(ddlPaymentObligationType.SelectedValue))
        If Not IsNothing(oPaymentObligationType) Then
            oPaymentAssignmentType.PaymentObligationType = oPaymentObligationType
        Else
            'refer bug  1243
            'MessageBox.Show("Data Tipe Tidak Ditemukan")
            'Return
            oPaymentAssignmentType.PaymentObligationType = Nothing
        End If

        Try
            Dim nresult As Integer = 0
            nresult = New PaymentAssignmentTypeFacade(User).Insert(oPaymentAssignmentType)
            If nresult > 0 Then
                MessageBox.Show(SR.SaveSuccess)
            Else
                MessageBox.Show(SR.SaveFail)
                Return
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub UpdateData()
        Try
            Dim objPaymentAssignmentType As PaymentAssignmentType = CType(Session.Item("vsPaymentAssignmentType"), PaymentAssignmentType)

            'If isExistToUpdate = Then
            '    MessageBox.Show("Data Kode Sudah Ada!")
            '    Return
            'End If

            objPaymentAssignmentType.Description = txtdescription.Text
            objPaymentAssignmentType.Status = ddlStatus.SelectedValue
            If cboProcess.Checked Then
                objPaymentAssignmentType.SourceDocument = CInt(EnumOnlinePayment.SourceDocument.MANUAL)
            Else
                objPaymentAssignmentType.SourceDocument = CInt(EnumOnlinePayment.SourceDocument.SAP)
            End If

            Dim oPaymentObligationType As PaymentObligationType = New PaymentObligationType
            oPaymentObligationType = New PaymentObligationTypeFacade(User).Retrieve(CInt(ddlPaymentObligationType.SelectedValue))
            If Not IsNothing(oPaymentObligationType) Then
                objPaymentAssignmentType.PaymentObligationType = oPaymentObligationType
            Else
                objPaymentAssignmentType.PaymentObligationType = Nothing
            End If

            Dim nResult = New PaymentAssignmentTypeFacade(User).Update(objPaymentAssignmentType)
            If nResult > 0 Then
                MessageBox.Show(SR.UpdateSucces)
            Else
                MessageBox.Show(SR.UpdateFail)
                Return
            End If
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try
    End Sub


    Private Function isExist() As Boolean
        Dim objSearch As PaymentAssignmentType = New PaymentAssignmentType
        objSearch = New PaymentAssignmentTypeFacade(User).Retrieve(txtKode.Text.Trim)
        If (objSearch.ID > 0) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function isExistToUpdate() As Integer
        Dim objSearch As PaymentAssignmentType = New PaymentAssignmentType
        objSearch = New PaymentAssignmentTypeFacade(User).Retrieve(txtKode.Text.Trim)
        If IsNothing(objSearch) Then
            Return 0
        Else
            Return objSearch.ID
        End If
    End Function

    Private Sub bindToGrid(ByVal curPage As Integer)
        Dim totalRow As Integer = 0
        If (curPage >= 0) Then
            dtgPaymentAssignmentType.DataSource = New PaymentAssignmentTypeFacade(User).RetrieveActiveList(curPage + 1, dtgPaymentAssignmentType.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgPaymentAssignmentType.VirtualItemCount = totalRow
            dtgPaymentAssignmentType.DataBind()
        End If
    End Sub

    Private Sub ViewArea(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objPaymentAssignmentType As PaymentAssignmentType = New PaymentAssignmentTypeFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsPaymentAssignmentType", objPaymentAssignmentType)
        txtKode.Text = objPaymentAssignmentType.Code
        txtdescription.Text = objPaymentAssignmentType.Description
        ddlStatus.SelectedValue = objPaymentAssignmentType.Status
        If IsNothing(objPaymentAssignmentType.PaymentObligationType) Then
            ddlPaymentObligationType.SelectedIndex = 0
        Else
            ddlPaymentObligationType.SelectedValue = objPaymentAssignmentType.PaymentObligationType.ID
        End If

        If objPaymentAssignmentType.SourceDocument = CInt(EnumOnlinePayment.SourceDocument.MANUAL) Then
            cboProcess.Checked = True
        Else
            cboProcess.Checked = False
        End If
        If EditStatus Then
            btnSimpan.Enabled = CekPrivCreate()
        Else
            Me.btnSimpan.Enabled = EditStatus
        End If
    End Sub

    Private Sub DeleteArea(ByVal nID As Integer)
        Try
            Dim nresult As Integer = 0
            Dim objPaymentAssignmentType As PaymentAssignmentType = New PaymentAssignmentTypeFacade(User).Retrieve(nID)
            Dim facade As PaymentAssignmentTypeFacade = New PaymentAssignmentTypeFacade(User)
            objPaymentAssignmentType.RowStatus = CType(DBRowStatus.Deleted, Short)
            nresult = facade.Update(objPaymentAssignmentType)
            If nresult > 0 Then
                dtgPaymentAssignmentType.CurrentPageIndex = 0
                bindToGrid(dtgPaymentAssignmentType.CurrentPageIndex)
            Else
                MessageBox.Show(SR.DeleteFail)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Function ValidateData() As Boolean
        If txtKode.Text = String.Empty Then
            MessageBox.Show("Silakan isi Kode.")
            Return False
        End If

        'If ddlPaymentObligationType.SelectedIndex < 1 Then
        '    MessageBox.Show("Silakan pilih tipe.")
        '    Return False
        'End If

        Return True
    End Function

#End Region

End Class

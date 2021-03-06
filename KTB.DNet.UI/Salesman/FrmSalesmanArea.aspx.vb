#Region "Custom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

#End Region



Public Class FrmSalesmanArea
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgSalesmanArea As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents txtAreaCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAreaDesc As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCity As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _SalesmanAreaFacade As New SalesmanAreaFacade(User)
    Private _createPriv As Boolean = False
    Private sessHelper As New SessionHelper

#End Region

#Region "PrivateCustomMethods"




#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CheckPrivilege()
        _createPriv = CheckCreatePriv()
        btnSimpan.Visible = _createPriv
        If Not IsPostBack Then
            Initialize()
            BindDataGrid(0)
        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgSalesmanArea.CurrentPageIndex = 0
        sessHelper.RemoveSession("criteriaArea")
        BindDataGrid(0)
    End Sub
    Private Sub dgSalesmanArea_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanArea.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanArea As SalesmanArea = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSalesmanArea.CurrentPageIndex * dgSalesmanArea.PageSize)
        End If
        
        If e.Item.ItemIndex <> -1 Then
            Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            If CheckCreatePriv() Then
                lbtnView.Visible = True
                lbtnEdit.Visible = True
                lbtnDelete.Visible = True
            Else
                lbtnView.Visible = False
                lbtnEdit.Visible = False
                lbtnDelete.Visible = False
            End If
        End If
        
    End Sub
    Private Sub dgSalesmanArea_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanArea.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewArea(e.Item.Cells(0).Text, False)
            txtAreaCode.ReadOnly = True
            txtAreaDesc.ReadOnly = True
            txtCity.ReadOnly = True
            btnSimpan.Enabled = False

        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewArea(e.Item.Cells(0).Text, True)
            dgSalesmanArea.SelectedIndex = e.Item.ItemIndex
            txtAreaCode.ReadOnly = False
            txtAreaDesc.ReadOnly = False
            txtCity.ReadOnly = False
            btnSimpan.Enabled = True
        ElseIf e.CommandName = "Delete" Then
            DeleteArea(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub
    Private Sub dgSalesmanArea_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesmanArea.PageIndexChanged
        dgSalesmanArea.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSalesmanArea.CurrentPageIndex)
    End Sub
    Private Sub dgSalesmanArea_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSalesmanArea.SortCommand
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
        dgSalesmanArea.SelectedIndex = -1
        'dgSalesmanArea.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanArea.CurrentPageIndex)
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        If CheckValidation() Then
            Dim objSalesmanArea As SalesmanArea = New SalesmanArea
            Dim objSalesmanAreaFacade As SalesmanAreaFacade = New SalesmanAreaFacade(User)
            Dim nResult As Integer = -1
            txtAreaCode.ReadOnly = False
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                Dim codeIsValid As Integer = New SalesmanAreaFacade(User).ValidateCode(txtAreaCode.Text.Trim, objSalesmanArea.ID)
                If codeIsValid > 0 Then
                    MessageBox.Show(SR.DataIsExist("Kode Area"))
                    Return
                End If
                objSalesmanArea.AreaCode = txtAreaCode.Text
                objSalesmanArea.AreaDesc = txtAreaDesc.Text
                objSalesmanArea.City = txtCity.Text
                nResult = New SalesmanAreaFacade(User).Insert(objSalesmanArea)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                End If
            Else
                UpdateArea()
            End If

            ClearData()
            dgSalesmanArea.CurrentPageIndex = 0
            BindDataGrid(dgSalesmanArea.CurrentPageIndex)
        End If
    End Sub
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        txtAreaCode.ReadOnly = False
        txtAreaDesc.ReadOnly = False
        txtCity.ReadOnly = False
    End Sub

#End Region

   #Region "Need To Add"
    ' penambahan untuk initialize data
    Private Sub ClearData()
        txtAreaCode.Text = String.Empty
        txtAreaDesc.Text = String.Empty
        txtCity.Text = String.Empty
        btnSimpan.Enabled = _createPriv
        'btnSimpan.Enabled = True
        If dgSalesmanArea.Items.Count > 0 Then
            dgSalesmanArea.SelectedIndex = -1
        End If
        ViewState.Add("vsProcess", "Insert")
    End Sub

    ' untuk update data yg sdh ada sebelumnya
    Private Sub UpdateArea()
        Dim objSalesmanArea As SalesmanArea = CType(sessHelper.GetSession("vsSalesmanArea"), SalesmanArea)
        objSalesmanArea.AreaDesc = txtAreaDesc.Text
        objSalesmanArea.City = txtCity.Text

        Dim codeIsValid As Integer = New SalesmanAreaFacade(User).ValidateCode(txtAreaCode.Text.Trim, objSalesmanArea.ID)
        If codeIsValid > 0 Then
            MessageBox.Show("Kode Sudah Ada !!")
            Return
        End If

        Dim assign As Integer = 0
        Dim crit1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanAreaAssign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit1.opAnd(New Criteria(GetType(SalesmanAreaAssign), "SalesmanArea.ID", MatchType.Exact, objSalesmanArea.ID))

        Dim arlSalesmanAreaAssign As ArrayList = New SalesmanAreaAssignFacade(User).Retrieve(crit1)
        If (arlSalesmanAreaAssign.Count > 0) And (txtAreaCode.Text <> objSalesmanArea.AreaCode) Then
            MessageBox.Show("Kode sudah digunakan untuk data lain")
        Else
            objSalesmanArea.AreaCode = txtAreaCode.Text
            Dim nResult = New SalesmanAreaFacade(User).Update(objSalesmanArea)
            If nResult <= 0 Then
                MessageBox.Show("Record Gagal Diupdate")
            End If
        End If
    End Sub

    Private Sub Initialize()
        txtAreaCode.Text = ""
        txtAreaDesc.Text = ""
        txtCity.Text = ""
        ViewState("CurrentSortColumn") = "AreaCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ViewState.Add("vsProcess", "Insert")
    End Sub
    Private Sub DeleteArea(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0

        Dim objSalesmanArea As SalesmanArea = New SalesmanAreaFacade(User).Retrieve(nID)
        Dim iReturn As Integer = -1
        Dim facade As SalesmanAreaFacade = New SalesmanAreaFacade(User)

        'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanAreaAssign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'crit.opAnd(New Criteria(GetType(SalesmanAreaAssign), "SalesmanArea.ID", MatchType.Exact, objSalesmanArea.ID))
        'Dim arlSalesmanAreaAssign As ArrayList = New SalesmanAreaAssignFacade(User).Retrieve(crit)

        'updated by anh, check to salesmanheader on SalesmanAreaID
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanArea.ID", MatchType.Exact, objSalesmanArea.ID))

        Dim arlSalesmanA As ArrayList = New SalesmanHeaderFacade(User).Retrieve(crit)
        If arlSalesmanA.Count > 0 Then
            MessageBox.Show("Data sudah dipakai di tabel lain. Field ini tidak dapat dihapus!")
        Else
            'iReturn = facade.DeleteFromDB(objSalesmanArea)
            iReturn = facade.Delete(objSalesmanArea)
            If iReturn <= 0 Then
                MessageBox.Show("Record Gagal Dihapus")
            End If
        End If

        dgSalesmanArea.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanArea.CurrentPageIndex)
    End Sub

    ' penambahan untuk view data
    Private Sub ViewArea(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objSalesmanArea As SalesmanArea = New SalesmanAreaFacade(User).Retrieve(nID)
        'Todo session
        'Session.Add("vsSalesmanArea", objSalesmanArea)
        sessHelper.SetSession("vsSalesmanArea", objSalesmanArea)
        txtAreaCode.Text = objSalesmanArea.AreaCode
        txtAreaDesc.Text = objSalesmanArea.AreaDesc
        txtCity.Text = objSalesmanArea.City
        If EditStatus Then
            btnSimpan.Enabled = _createPriv
        End If
        'Me.btnSimpan.Enabled = EditStatus
    End Sub

    ' ini perlu set security
    

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        If (IsNothing(sessHelper.GetSession("criteriaArea"))) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If txtAreaCode.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(SalesmanArea), "AreaCode", MatchType.[Partial], txtAreaCode.Text.Trim()))
            End If
            If txtAreaDesc.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(SalesmanArea), "AreaDesc", MatchType.[Partial], txtAreaDesc.Text.Trim()))
            End If
            If txtCity.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(SalesmanArea), "City", MatchType.[Partial], txtCity.Text.Trim()))
            End If
            sessHelper.SetSession("criteriaArea", criterias)
        End If
        arrList = _SalesmanAreaFacade.RetrieveByCriteria(CType(sessHelper.GetSession("criteriaArea"), CriteriaComposite), idxPage + 1, dgSalesmanArea.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgSalesmanArea.DataSource = arrList
        dgSalesmanArea.VirtualItemCount = totalRow
        dgSalesmanArea.DataBind()
    End Sub

    Private Function CheckValidation() As Boolean
        Dim blnValid As Boolean = True
        If (txtAreaCode.Text = "") Then
            blnValid = False
            MessageBox.Show("Kode Area harus diinput terlebih dahulu")
            Return (blnValid)
        End If
        'If (txtAreaCode.Text <> "") Then
        '    Dim objSalesmanArea As SalesmanArea = New SalesmanArea
        '    Dim objSalesmanAreaFacade As SalesmanAreaFacade = New SalesmanAreaFacade(User)
        '    If objSalesmanAreaFacade.ValidateCode(txtAreaCode.Text) > 0 Then
        '        blnValid = False
        '        MessageBox.Show(SR.DataIsExist("Kode Area"))
        '        Return (blnValid)
        '    End If
        'End If
        If (txtAreaDesc.Text = "") Then
            blnValid = False
            MessageBox.Show("Deskripsi Area harus diinput terlebih dahulu")
            Return (blnValid)
        End If
        'If (txtCity.Text = "") Then
        '    blnValid = False
        '    MessageBox.Show("Kota harus diinput terlebih dahulu")
        '    Return (blnValid)
        'End If
        Return blnValid
    End Function
#End Region

#Region "cek Privilege"
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.LTPArea_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Tenaga Penjual - Area Tenaga Penjual")
        End If
    End Sub
    Private Function CheckCreatePriv() As Boolean
        If SecurityProvider.Authorize(context.User, SR.ATPCreate_Privilege) Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region
End Class

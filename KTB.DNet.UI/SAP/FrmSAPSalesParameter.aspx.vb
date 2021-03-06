Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.SAP

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security



Public Class FrmSAPSalesParameter
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtGrade As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBobot As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSalesmanPoint As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSalesCounterPoint As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents dgSAPSalesParameter As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button

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
    Private _SAPSalesParameterFacade As New SAPSalesParameterFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper

#End Region

#Region "PrivateCustomMethods"

    

   
#End Region

#Region "EventHandlers"

   
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgSAPSalesParameter.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub

    Private Sub dgSAPSalesParameter_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSAPSalesParameter.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSAPSalesParameter As SAPSalesParameter = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSAPSalesParameter.CurrentPageIndex * dgSAPSalesParameter.PageSize)
        End If
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If
    End Sub

    Private Sub EnableControl(ByVal blnReadOnly As Boolean)
        txtCode.ReadOnly = blnReadOnly
        txtGrade.ReadOnly = blnReadOnly
        txtBobot.ReadOnly = blnReadOnly
        txtSalesmanPoint.ReadOnly = blnReadOnly
        txtSalesCounterPoint.ReadOnly = blnReadOnly
    End Sub

    Private Sub dgSAPSalesParameter_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSAPSalesParameter.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            View(e.Item.Cells(0).Text, False)
            EnableControl(True)

        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            View(e.Item.Cells(0).Text, True)
            dgSAPSalesParameter.SelectedIndex = e.Item.ItemIndex
            EnableControl(False)

        ElseIf e.CommandName = "Delete" Then
            Delete(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub

    Private Sub dgSAPSalesParameter_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSAPSalesParameter.PageIndexChanged
        dgSAPSalesParameter.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSAPSalesParameter.CurrentPageIndex)
    End Sub

    Private Sub dgSAPSalesParameter_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSAPSalesParameter.SortCommand
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
        dgSAPSalesParameter.SelectedIndex = -1
        BindDataGrid(dgSAPSalesParameter.CurrentPageIndex)
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CheckPrivilege()
        If Not IsPostBack Then
            Initialize()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        If CheckValidation() Then
            Dim objSAPSalesParameter As SAPSalesParameter = New SAPSalesParameter
            Dim objSAPSalesParameterFacade As SAPSalesParameterFacade = New SAPSalesParameterFacade(User)
            Dim nResult As Integer = -1
            '            txtAreaCode.ReadOnly = False
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                Dim codeIsValid As Integer = New SAPSalesParameterFacade(User).ValidateCode(txtCode.Text.Trim, objSAPSalesParameter.ID)
                If codeIsValid > 0 Then
                    MessageBox.Show(SR.DataIsExist("Kode"))
                    Return
                End If
                objSAPSalesParameter.Code = txtCode.Text
                objSAPSalesParameter.Grade = txtGrade.Text
                objSAPSalesParameter.Bobot = txtBobot.Text
                objSAPSalesParameter.SalesmanPoint = CType(txtSalesmanPoint.Text, Decimal)
                objSAPSalesParameter.SalesCounterPoint = CType(txtSalesCounterPoint.Text, Decimal)

                nResult = New SAPSalesParameterFacade(User).Insert(objSAPSalesParameter)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                End If
            Else
                Update()
            End If

            ClearData()
            dgSAPSalesParameter.CurrentPageIndex = 0
            BindDataGrid(dgSAPSalesParameter.CurrentPageIndex)
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        EnableControl(False)
    End Sub
#Region "Need To Add"
    ' penambahan untuk initialize data
    Private Sub ClearData()
        txtCode.Text = String.Empty
        txtGrade.Text = String.Empty
        txtBobot.Text = String.Empty
        txtSalesmanPoint.Text = String.Empty
        txtSalesCounterPoint.Text = String.Empty

        btnSimpan.Enabled = True
        If dgSAPSalesParameter.Items.Count > 0 Then
            dgSAPSalesParameter.SelectedIndex = -1
        End If
        ViewState.Add("vsProcess", "Insert")
    End Sub

    ' untuk update data yg sdh ada sebelumnya
    Private Sub Update()
        Dim objSAPSalesParameter As SAPSalesParameter = CType(sessHelper.GetSession("vsSAPSalesParameter "), SAPSalesParameter)
        objSAPSalesParameter.Code = txtCode.Text
        objSAPSalesParameter.Grade = txtGrade.Text
        objSAPSalesParameter.Bobot = txtBobot.Text
        objSAPSalesParameter.SalesmanPoint = txtSalesmanPoint.Text
        objSAPSalesParameter.SalesCounterPoint = txtSalesCounterPoint.Text

        Dim codeIsValid As Integer = New SAPSalesParameterFacade(User).ValidateCode(txtCode.Text.Trim, objSAPSalesParameter.ID)
        If codeIsValid > 0 Then
            MessageBox.Show("Kode Sudah Ada !!")
            Return
        End If
        Dim nResult = New SAPSalesParameterFacade(User).Update(objSAPSalesParameter)
        If nResult <= 0 Then
            MessageBox.Show("Record Gagal Diupdate")
        End If
    End Sub

    Private Sub Initialize()
        ClearData()
        ViewState("CurrentSortColumn") = "Code"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub Delete(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0

        Dim objSAPSalesParameter As SAPSalesParameter = New SAPSalesParameterFacade(User).Retrieve(nID)
        Dim facade As SAPSalesParameterFacade = New SAPSalesParameterFacade(User)
        Dim iReturn As Integer = -1
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPSalesParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(SAPSalesParameter), "ID", MatchType.Exact, objSAPSalesParameter.ID))

        iReturn = facade.DeleteFromDB(objSAPSalesParameter)
        If iReturn <= 0 Then
            MessageBox.Show("Record Gagal Dihapus")
        End If

        dgSAPSalesParameter.CurrentPageIndex = 0
        BindDataGrid(dgSAPSalesParameter.CurrentPageIndex)
    End Sub

    ' penambahan untuk view data
    Private Sub View(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objSAPSalesParameter As SAPSalesParameter = New SAPSalesParameterFacade(User).Retrieve(nID)
        'Todo session
        'Session.Add("vsSAPSalesParameter ", objSAPSalesParameter)
        sessHelper.SetSession("vsSAPSalesParameter ", objSAPSalesParameter)
        txtCode.Text = objSAPSalesParameter.Code
        txtGrade.Text = objSAPSalesParameter.Grade
        txtBobot.Text = CType(objSAPSalesParameter.Bobot, String)
        txtSalesmanPoint.Text = CType(objSAPSalesParameter.SalesmanPoint, String)
        txtSalesCounterPoint.Text = CType(objSAPSalesParameter.SalesCounterPoint, String)
        Me.btnSimpan.Enabled = EditStatus
    End Sub

    ' ini perlu set security
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiLihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - Daftar SPL")
        End If

        _create = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiBuat_Privilege)
        _edit = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiUbah_Privilege)
        _view = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiDetail_Privilege)

        'lbtnNew.Visible = _create
        btnSearch.Visible = _view

    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SAPSalesParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtCode.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPSalesParameter), "Code", MatchType.[Partial], txtCode.Text.Trim()))
        End If
        If txtGrade.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPSalesParameter), "Grade", MatchType.Exact, txtGrade.Text.Trim()))
        End If
        If txtBobot.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPSalesParameter), "Bobot", MatchType.Exact, CType(txtBobot.Text.Trim(), Integer)))
        End If
        If txtSalesmanPoint.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPSalesParameter), "Bobot", MatchType.Exact, CType(txtSalesmanPoint.Text.Trim(), Decimal)))
        End If
        If txtSalesCounterPoint.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPSalesParameter), "Bobot", MatchType.Exact, CType(txtSalesCounterPoint.Text.Trim(), Decimal)))
        End If

        arrList = _SAPSalesParameterFacade.RetrieveByCriteria(criterias, idxPage + 1, dgSAPSalesParameter.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgSAPSalesParameter.DataSource = arrList
        dgSAPSalesParameter.VirtualItemCount = totalRow
        dgSAPSalesParameter.DataBind()
    End Sub

    Private Function CheckValidation() As Boolean
        Dim blnValid As Boolean = True
        If (txtCode.Text = "") Then
            blnValid = False
            MessageBox.Show("Kode harus diinput terlebih dahulu")
            Return (blnValid)
        End If
        If (txtGrade.Text = "") Then
            blnValid = False
            MessageBox.Show("Nilai harus diinput terlebih dahulu")
            Return (blnValid)
        End If
        If (txtBobot.Text = "") Then
            blnValid = False
            MessageBox.Show("Bobot harus diinput terlebih dahulu")
            Return (blnValid)
        End If
        If (txtSalesmanPoint.Text = "") Then
            blnValid = False
            MessageBox.Show("Point Salesman harus diinput terlebih dahulu")
            Return (blnValid)
        End If
        If (txtSalesCounterPoint.Text = "") Then
            blnValid = False
            MessageBox.Show("Point Sales Counter harus diinput terlebih dahulu")
            Return (blnValid)
        End If
        Return blnValid
    End Function
#End Region
End Class

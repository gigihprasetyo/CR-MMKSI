#Region "Custom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

#End Region

Public Class FrmSalesmanMaintainUniform
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents dgMaintainUniform As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSalesmanUnifDistributionCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button

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
    Private sHelper As SessionHelper = New SessionHelper
    Private arlUniform As ArrayList = New ArrayList
    Dim objSalesmanUnifDistribution As SalesmanUnifDistribution = New SalesmanUnifDistribution
    Dim objSalesmanUnifDistributionFacade As SalesmanUnifDistributionFacade = New SalesmanUnifDistributionFacade(User)
    Private _createPriv As Boolean = False
#End Region

#Region "Custom Method"
    Private Function InsertSalesmanUnitDistribution() As Boolean
        Dim nResult As Integer

        If objSalesmanUnifDistributionFacade.ValidateCode(txtSalesmanUnifDistributionCode.Text, 0) = 0 Then
            objSalesmanUnifDistribution.SalesmanUnifDistributionCode = txtSalesmanUnifDistributionCode.Text
            objSalesmanUnifDistribution.Description = txtDescription.Text
            objSalesmanUnifDistribution.IsActive = CType(ddlStatus.SelectedValue, Short)

            nResult = New SalesmanUnifDistributionFacade(User).Insert(objSalesmanUnifDistribution)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
                Return False
            Else
                MessageBox.Show(SR.SaveSuccess)
                Return True
            End If
        Else
            MessageBox.Show(SR.DataIsExist("Kode Seragam"))
            Return False
        End If
    End Function

    Private Function UpdateSalesmanUnitDistribution(ByVal intID As Integer) As Boolean
        Dim nResult As Integer
        If objSalesmanUnifDistributionFacade.ValidateCode(txtSalesmanUnifDistributionCode.Text, intID) = 0 Then
            Dim objobjSalesmanUnifDistributionTmp As SalesmanUnifDistribution = New SalesmanUnifDistributionFacade(User).Retrieve(intID)
            objobjSalesmanUnifDistributionTmp.SalesmanUnifDistributionCode = txtSalesmanUnifDistributionCode.Text
            objobjSalesmanUnifDistributionTmp.Description = txtDescription.Text
            objobjSalesmanUnifDistributionTmp.IsActive = CType(ddlStatus.SelectedValue, Short)

            nResult = New SalesmanUnifDistributionFacade(User).Update(objobjSalesmanUnifDistributionTmp)
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
                Return False
            Else
                MessageBox.Show(SR.UpdateSucces)
                Return True
            End If
        Else
            MessageBox.Show(SR.DataIsExist("Kode Seragam"))
            Return False
        End If
    End Function

    Private Sub BindDataGridUniform(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUnifDistribution), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            arlUniform = New SalesmanUnifDistributionFacade(User).RetrieveActiveList(indexPage + 1, dgMaintainUniform.PageSize, totalRow, sHelper.GetSession("SortCol"), sHelper.GetSession("SortDirection"), criterias)
            dgMaintainUniform.DataSource = arlUniform
            dgMaintainUniform.VirtualItemCount = totalRow
            dgMaintainUniform.DataBind()
            sHelper.SetSession("SessArlUniform", arlUniform)
        End If
    End Sub
    Public Sub ClearData()
        txtDescription.Text = String.Empty
        txtSalesmanUnifDistributionCode.Text = String.Empty
        ddlStatus.SelectedIndex = 0
        dgMaintainUniform.SelectedIndex = -1
        btnSave.Text = "Simpan"
        ViewState.Add("vsProcess", "Insert")
        btnCancel.Visible = False
        sHelper.RemoveSession("ID")
        ReadOnlyControl(False)
    End Sub
    Private Sub DeleteUnifDistribution(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0
        Dim objUnit As SalesmanUnifDistribution = New SalesmanUnifDistributionFacade(User).Retrieve(nID)

        Try
            objSalesmanUnifDistributionFacade.DeleteFromDB(objUnit)
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
        End Try

        BindDataGridUniform(dgMaintainUniform.CurrentPageIndex)
    End Sub

    Private Sub ViewType(ByVal nID As Integer, ByVal blnReadOnlyStatus As Boolean)
        Dim objUnit As SalesmanUnifDistribution = New SalesmanUnifDistributionFacade(User).Retrieve(nID)
        txtSalesmanUnifDistributionCode.Text = objUnit.SalesmanUnifDistributionCode
        txtDescription.Text = objUnit.Description
        ddlStatus.SelectedValue = CStr(objUnit.IsActive)
        ' jika view , komponen hanya view saja -> ReadOnlyControl(True)
        ReadOnlyControl(blnReadOnlyStatus)
    End Sub

    Private Sub ReadOnlyControl(ByVal blnReadOnly As Boolean)
        txtSalesmanUnifDistributionCode.ReadOnly = blnReadOnly
        txtDescription.ReadOnly = blnReadOnly
        ddlStatus.Enabled = Not blnReadOnly
    End Sub

    Private Sub BindToDropdownList()
        ddlStatus.DataSource = enumStatusSalesmanUnifDistribution.RetrieveStatus()
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataBind()
        ddlStatus.SelectedIndex = -1
    End Sub
#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        _createPriv = CheckCreatePrivilege()
        If Not IsPostBack Then
            ClearData()
            sHelper.SetSession("SortCol", "SalesmanUnifDistributionCode")
            sHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            sHelper.SetSession("SessArlUniform", arlUniform)
            BindToDropdownList()
            BindDataGridUniform(0)
        End If
        btnSave.Visible = _createPriv
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim status As String = CType(ViewState("vsProcess"), String)
        Dim result As Boolean
        If Not Page.IsValid Then
            Return
        End If

        Select Case status
            Case "Insert"
                If btnSave.Text <> "Batal" Then
                    result = InsertSalesmanUnitDistribution()
                    dgMaintainUniform.CurrentPageIndex = 0
                    BindDataGridUniform(dgMaintainUniform.CurrentPageIndex)
                    If result = True Then
                        ClearData()
                    End If
                Else
                    ClearData()
                    BindDataGridUniform(0)
                End If
            Case "Edit"
                result = UpdateSalesmanUnitDistribution(CType(sHelper.GetSession("ID"), Integer))
                dgMaintainUniform.CurrentPageIndex = 0
                BindDataGridUniform(dgMaintainUniform.CurrentPageIndex)
                If result = True Then
                    ClearData()
                End If
            Case Else
                ClearData()
                BindDataGridUniform(0)
        End Select
    End Sub

    Private Sub dgMaintainUniform_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgMaintainUniform.ItemCommand
        If e.CommandName = "View" Then
            dgMaintainUniform.SelectedIndex = e.Item.ItemIndex
            Dim objUnit As SalesmanUnifDistribution = New SalesmanUnifDistributionFacade(User).Retrieve(CInt(e.CommandArgument))
            sHelper.SetSession("objedit", objUnit)
            ViewType(e.Item.Cells(0).Text, True)
            ViewState.Add("vsProcess", "Insert")
            btnSave.Text = "Batal"
            btnCancel.Visible = False
        End If

        If e.CommandName = "Delete" Then
            DeleteUnifDistribution(e.Item.Cells(0).Text)
            ClearData()
        End If

        ' refer to bug 1013
        If e.CommandName = "Edit" Then
            dgMaintainUniform.SelectedIndex = e.Item.ItemIndex
            ViewType(e.Item.Cells(0).Text, False)
            ViewState.Add("vsProcess", "Edit")
            sHelper.SetSession("ID", e.Item.Cells(0).Text)
            btnSave.Text = "Simpan"
            btnCancel.Visible = True
        End If

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClearData()
    End Sub

    Private Sub dgMaintainUniform_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgMaintainUniform.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            objSalesmanUnifDistribution = arlUniform(e.Item.ItemIndex)
            Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            _lblNo.Text = e.Item.ItemIndex + 1 + (dgMaintainUniform.CurrentPageIndex * dgMaintainUniform.PageSize)

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            lblStatus.Text = CType(objSalesmanUnifDistribution.IsActive, enumStatusSalesmanUnifDistribution.StatusSalesmanUnifDistribution).ToString.Replace("_", " ")

            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnEdit.Visible = _createPriv

            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            lbtnDelete.CommandArgument = objSalesmanUnifDistribution.ID
            lbtnDelete.Visible = _createPriv

            Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            lbtnView.CommandArgument = objSalesmanUnifDistribution.ID
            lbtnView.Visible = _createPriv
        End If
    End Sub

    Private Sub dgMaintainUniform_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgMaintainUniform.PageIndexChanged
        dgMaintainUniform.CurrentPageIndex = e.NewPageIndex
        BindDataGridUniform(dgMaintainUniform.CurrentPageIndex)
    End Sub

    Private Sub dgMaintainUniform_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgMaintainUniform.SortCommand
        If btnSave.Text <> "Simpan" Then
            ClearData()
            BindDataGridUniform(0)
        End If

        If e.SortExpression = sHelper.GetSession("SortCol") Then
            If sHelper.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sHelper.SetSession("SortCol", e.SortExpression)
        dgMaintainUniform.SelectedIndex = -1
        dgMaintainUniform.CurrentPageIndex = 0
        BindDataGridUniform(0)
    End Sub

#End Region

#Region "Privilege"

    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.PengaturanKodeSeragamView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Seragam Tenaga Penjual - Pengaturan Kode Seragam")
        End If
    End Sub
    Private Function CheckCreatePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengaturanKodeSeragamCreateCode_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region
End Class

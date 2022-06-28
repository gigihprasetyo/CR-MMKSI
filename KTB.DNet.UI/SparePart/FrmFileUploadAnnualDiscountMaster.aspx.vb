#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class FrmFileUploadAnnualDiscountMaster
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtProgramName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFileName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRemark As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgUploadFile As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlTipe As System.Web.UI.WebControls.DropDownList
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator

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
    Private _SessionHelper As SessionHelper = New SessionHelper
    'Private _arrUploadProgram As ArrayList
#End Region

#Region "Custom Method"

    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = "CreatedTime"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FileUploadAnnualDiscount), "Tipe", MatchType.Exact, ddlTipe.SelectedIndex))
            dtgUploadFile.DataSource = New FileUploadAnnualDiscountFacade(User).RetrieveActiveList(criterias, dtgUploadFile.CurrentPageIndex + 1, dtgUploadFile.PageSize, _
                           totalRow, CType(ViewState("CurrentSortColumn"), String), _
                           CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgUploadFile.VirtualItemCount = totalRow
            dtgUploadFile.DataBind()
        End If
    End Sub

    Private Sub ClearData()
        Me.txtFileName.ReadOnly = False
        Me.txtProgramName.ReadOnly = False
        Me.txtRemark.ReadOnly = False
        Me.txtFileName.ReadOnly = False
        Me.ddlTipe.Enabled = True

        Me.txtFileName.Text = String.Empty
        Me.txtProgramName.Text = String.Empty
        Me.txtRemark.Text = String.Empty

        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        viewstate.Add("Mode", "Save")
    End Sub

    Private Function InsertGroup() As Integer
        Dim objFileUploadAnnualDiscount As FileUploadAnnualDiscount = New FileUploadAnnualDiscount
        Dim nResult As Integer = -1
        'If Not IsExistGroup(CInt(ddlCourseId.SelectedValue), CInt(ddlPreReqCourID.SelectedValue)) Then
        objFileUploadAnnualDiscount.FileName = Me.txtFileName.Text
        objFileUploadAnnualDiscount.ProgramName = Me.txtProgramName.Text
        objFileUploadAnnualDiscount.Remark = Me.txtRemark.Text
        objFileUploadAnnualDiscount.Tipe = Me.ddlTipe.SelectedIndex
        nResult = New FileUploadAnnualDiscountFacade(User).Insert(objFileUploadAnnualDiscount)
        'Else
        'End If
        Return nResult
    End Function

    Private Function UpdateGroup() As Integer
        Dim objFileUploadAnnualDiscount As FileUploadAnnualDiscount = CType(_SessionHelper.GetSession("vsUploadFile"), FileUploadAnnualDiscount)
        If Not IsNothing(objFileUploadAnnualDiscount) Then
            objFileUploadAnnualDiscount.FileName = Me.txtFileName.Text
            objFileUploadAnnualDiscount.ProgramName = Me.txtProgramName.Text
            objFileUploadAnnualDiscount.Remark = Me.txtRemark.Text
            objFileUploadAnnualDiscount.Tipe = Me.ddlTipe.SelectedIndex
            Return New FileUploadAnnualDiscountFacade(User).Update(objFileUploadAnnualDiscount)
        End If
        Return -1
    End Function

    Private Sub DeleteProgram(ByVal nID As Integer)
        Dim objFileUploadAnnualDiscount As FileUploadAnnualDiscount = New FileUploadAnnualDiscountFacade(User).Retrieve(nID)
        Dim facade As FileUploadAnnualDiscountFacade = New FileUploadAnnualDiscountFacade(User)
        facade.DeleteFromDB(objFileUploadAnnualDiscount)
        dtgUploadFile.CurrentPageIndex = 0
        BindDataGrid(dtgUploadFile.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub ViewGroup(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objFileUploadAnnualDiscount As FileUploadAnnualDiscount = New FileUploadAnnualDiscountFacade(User).Retrieve(nID)
        _SessionHelper.SetSession("vsUploadFile", objFileUploadAnnualDiscount)

        Me.txtFileName.Text = objFileUploadAnnualDiscount.FileName
        Me.txtProgramName.Text = objFileUploadAnnualDiscount.ProgramName
        Me.txtRemark.Text = objFileUploadAnnualDiscount.Remark
        Me.ddlTipe.SelectedIndex = objFileUploadAnnualDiscount.Tipe
        Me.btnSimpan.Enabled = EditStatus
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        CheckUserPrivilege()
        If Not IsPostBack Then

            InitiatePage()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ViewAnnualDiscount_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Pengelolaan File Annual Discount ")
        End If
        btnSimpan.Visible = SecurityProvider.Authorize(Context.User, SR.SaveAnnualDiscount_Privilege)
        btnBatal.Visible = SecurityProvider.Authorize(Context.User, SR.CancelAnnualDiscount_Privilege)
    End Sub

    Private Sub dtgUploadFile_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgUploadFile.PageIndexChanged
        dtgUploadFile.SelectedIndex = -1
        dtgUploadFile.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgUploadFile.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objFileUploadAnnualDiscount As FileUploadAnnualDiscount = New FileUploadAnnualDiscount
        Dim objFileUploadAnnualDiscountFacade As FileUploadAnnualDiscountFacade = New FileUploadAnnualDiscountFacade(User)
        Dim nResult = -1
        Dim myFileName As String() = txtFileName.Text.Split("-")

        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If ddlTipe.SelectedIndex = 1 Then
                If myFileName.Length = 1 Then
                    MessageBox.Show("Nama File Harus dimulai dengan Kode Dealer")
                    Exit Sub
                End If
            End If
            If objFileUploadAnnualDiscountFacade.ValidateValueProgramName(txtProgramName.Text) = 0 Then
                If objFileUploadAnnualDiscountFacade.ValidateValueFileName(txtFileName.Text) = 0 Then
                    objFileUploadAnnualDiscount.Remark = Me.txtRemark.Text
                    objFileUploadAnnualDiscount.FileName = Me.txtFileName.Text
                    objFileUploadAnnualDiscount.ProgramName = Me.txtProgramName.Text
                    objFileUploadAnnualDiscount.Tipe = Me.ddlTipe.SelectedIndex
                    objFileUploadAnnualDiscount.RowStatus = DBRowStatus.Canceled
                    nResult = objFileUploadAnnualDiscountFacade.Insert(objFileUploadAnnualDiscount)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                        dtgUploadFile.CurrentPageIndex = 0
                        BindDataGrid(dtgUploadFile.CurrentPageIndex)
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Nama File"))
                End If
            Else
                MessageBox.Show(SR.DataIsExist("Nama Dokumen"))
            End If

        Else
            Dim intUpdateResult As Integer = UpdateGroup()
            If intUpdateResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                If intUpdateResult = -2 Then
                    MessageBox.Show(SR.DataIsExist("Nama Dokumen"))
                Else
                    MessageBox.Show(SR.UpdateSucces)
                    ClearData()
                    dtgUploadFile.CurrentPageIndex = 0
                    BindDataGrid(dtgUploadFile.CurrentPageIndex)
                End If
            End If
        End If
        dtgUploadFile.SelectedIndex = -1
        viewstate.Add("Mode", "Save")
    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        dtgUploadFile.SelectedIndex = -1
    End Sub

    Private Sub dtgUploadFile_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgUploadFile.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewGroup(e.Item.Cells(0).Text, False)
            Me.txtFileName.ReadOnly = True
            Me.txtProgramName.ReadOnly = True
            Me.txtRemark.ReadOnly = True
            Me.ddlTipe.Enabled = False
            dtgUploadFile.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewState.Add("Mode", "Update")
            ViewGroup(e.Item.Cells(0).Text, True)
            dtgUploadFile.SelectedIndex = e.Item.ItemIndex
            Me.txtFileName.ReadOnly = False
            Me.txtProgramName.ReadOnly = False
            Me.txtRemark.ReadOnly = False
            Me.ddlTipe.Enabled = True
        ElseIf e.CommandName = "Delete" Then
            DeleteProgram(e.Item.Cells(0).Text)
        End If
    End Sub

    Private Sub dtgUploadFile_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgUploadFile.ItemDataBound

        If (e.Item.ItemIndex <> -1) Then
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

            If Not e.Item.DataItem Is Nothing Then
                e.Item.DataItem.GetType().ToString()
                Dim RowValue As FileUploadAnnualDiscount = CType(e.Item.DataItem, FileUploadAnnualDiscount)

                If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Or ItemType = ListItemType.SelectedItem Then
                    CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgUploadFile.CurrentPageIndex * dtgUploadFile.PageSize)

                    If RowValue.Tipe = 0 Then
                        e.Item.Cells(5).Text = "Umum"
                    ElseIf RowValue.Tipe = 1 Then
                        e.Item.Cells(5).Text = "Per Dealer"
                    Else
                        e.Item.Cells(5).Text = "Per Group"
                    End If

                End If

                If Not e.Item.FindControl("btnHapus") Is Nothing Then
                    CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                End If
            End If
        End If
    End Sub

    Private Sub dtgUploadFile_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgUploadFile.SortCommand
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

        dtgUploadFile.SelectedIndex = -1
        dtgUploadFile.CurrentPageIndex = 0
        BindDataGrid(dtgUploadFile.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub ddlTipe_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipe.SelectedIndexChanged
        If viewstate("Mode") = "Save" Then
            dtgUploadFile.CurrentPageIndex = 0
            BindDataGrid(dtgUploadFile.CurrentPageIndex)
        Else
          
        End If
        
    End Sub
    Private Sub SearchTipe()
        Dim total As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FileUploadAnnualDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ddlTipe.SelectedIndex <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FileUploadAnnualDiscount), "Tipe", MatchType.Exact, ddlTipe.SelectedIndex))
        dtgUploadFile.DataSource = New FileUploadAnnualDiscountFacade(User).RetrieveActiveList(criterias, dtgUploadFile.CurrentPageIndex + 1, dtgUploadFile.PageSize, _
                       total, CType(ViewState("CurrentSortColumn"), String), _
                       CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgUploadFile.VirtualItemCount = total
        dtgUploadFile.DataBind()
    End Sub

#End Region

   
End Class
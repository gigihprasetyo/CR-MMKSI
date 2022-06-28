#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
#End Region

Public Class FrmNationalHoliday
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtdeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents icHariLibur As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents dtgHariLibur As System.Web.UI.WebControls.DataGrid

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
    Private arlNationalHoliday As ArrayList
    Private objNationalHoliday As NationalHoliday
    Private DatePlus As Date = DateTime.Now.AddDays(2)

    Private m_bFormPrivilege As Boolean = False
#End Region

#Region "Custom Method"

    Private Function IsNationalHolidayExist(ByVal year As Integer, ByVal month As Integer, ByVal day As Integer) As Boolean
        Dim objNatfacade As NationalHolidayFacade = New NationalHolidayFacade(User)
        If objNatfacade.IsActiveDateExist(year, month, day) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub ViewNationalHoliday(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objHoliday As NationalHoliday = New NationalHolidayFacade(User).Retrieve(nID)
        If Not IsNothing(objHoliday) Then
            Dim tgl As New DateTime(CInt(objHoliday.HolidayYear), CInt(objHoliday.HolidayMonth), CInt(objHoliday.HolidayDate), 0, 0, 0)
            'Todo session
            Session.Add("vsNationalHoliday", objHoliday)
            icHariLibur.Value = tgl
            txtdeskripsi.Text = objHoliday.Description
            Me.btnSimpan.Enabled = EditStatus
        Else
            If EditStatus Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.ViewFail)
            End If
            dtgHariLibur.SelectedIndex = -1
            ClearData()
        End If

    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.ChangeHoliday_Privilege)
        
        If Not SecurityProvider.Authorize(Context.User, SR.ViewHoliday_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=GENERAL - Hari Libur")
        End If
    End Sub

    Private Sub checkselecteddate()
        icHariLibur.Value = DateTime.Now.AddDays(3)
    End Sub
    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "HolidayDateTime"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
#End Region

#Region "Event Hendlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            BindDataGrid(0)
            checkselecteddate()
        End If
    End Sub
    Private Sub dtgHariLibur_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgHariLibur.PageIndexChanged
        dtgHariLibur.SelectedIndex = -1
        dtgHariLibur.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgHariLibur.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgHariLibur_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgHariLibur.SortCommand
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

        dtgHariLibur.SelectedIndex = -1
        dtgHariLibur.CurrentPageIndex = 0
        BindDataGrid(dtgHariLibur.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            arlNationalHoliday = New NationalHolidayFacade(User).RetrieveActiveList(indexPage + 1, dtgHariLibur.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgHariLibur.DataSource = arlNationalHoliday
            dtgHariLibur.VirtualItemCount = totalRow
            dtgHariLibur.DataBind()
        End If
    End Sub


    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objHoliday As NationalHoliday = New NationalHoliday
        Dim nResult As Integer = -1
        Dim TglCal As New DateTime(CInt(icHariLibur.Value.Year), CInt(icHariLibur.Value.Month), CInt(icHariLibur.Value.Day), 0, 0, 0)
        Dim isDuplicate As Boolean = IsNationalHolidayExist(icHariLibur.Value.Year, icHariLibur.Value.Month, icHariLibur.Value.Day)
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not isDuplicate Then
                objHoliday.Description = txtdeskripsi.Text
                objHoliday.HolidayDate = CInt(0 & icHariLibur.Value.Day)
                objHoliday.HolidayMonth = CInt(icHariLibur.Value.Month)
                objHoliday.HolidayYear = CInt(icHariLibur.Value.Year)
                objHoliday.HolidayDateTime = New Date(objHoliday.HolidayYear, objHoliday.HolidayMonth, objHoliday.HolidayDate)
                If objHoliday.HolidayDateTime >= DatePlus Then
                    nResult = New NationalHolidayFacade(User).Insert(objHoliday)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                    End If
                Else
                    MessageBox.Show("Hari Libur Minimal 2 Hari Dari Hari ini")
                End If
            Else
                MessageBox.Show(SR.DataIsExist("Hari Libur"))
            End If
        Else
            UpdateNationalHoliday()
        End If
        BindDataGrid(dtgHariLibur.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub UpdateNationalHoliday()
        Dim objHoliday As NationalHoliday = CType(Session.Item("vsNationalHoliday"), NationalHoliday)
        objHoliday.Description = txtdeskripsi.Text
        Dim nResult As Integer = -1
        Try
            nResult = New NationalHolidayFacade(User).Update(objHoliday)
            MessageBox.Show(SR.UpdateSucces)
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try
    End Sub

    Private Sub ClearData()
        txtdeskripsi.Text() = String.Empty
        btnSimpan.Enabled = True
        txtdeskripsi.ReadOnly = False
        dtgHariLibur.SelectedIndex = -1
        ViewState.Add("vsProcess", "Insert")
        icHariLibur.Enabled = True
    End Sub
    Private Sub dtgHariLibur_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgHariLibur.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewNationalHoliday(e.Item.Cells(0).Text, False)
            icHariLibur.Enabled = False
            txtdeskripsi.ReadOnly = True
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewNationalHoliday(e.Item.Cells(0).Text, True)
            dtgHariLibur.SelectedIndex = e.Item.ItemIndex
            icHariLibur.Enabled = False
            txtdeskripsi.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            DeleteNationalHoliday(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub

    Private Sub DeleteNationalHoliday(ByVal nID As Integer)
        Dim objHoliday As NationalHoliday = New NationalHolidayFacade(User).Retrieve(nID)

        Try
            If objHoliday.HolidayDateTime >= DatePlus.AddDays(-2) Then
                objHoliday.RowStatus = DBRowStatus.Deleted
                Dim facade As NationalHolidayFacade = New NationalHolidayFacade(User)
                facade.Delete(objHoliday)
                MessageBox.Show(SR.DeleteSucces)
                BindDataGrid(dtgHariLibur.CurrentPageIndex)
            Else
                MessageBox.Show("Data Tidak Dapat DiHapus")
            End If
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
            dtgHariLibur.SelectedIndex = -1
            ClearData()
        End Try

    End Sub

    Sub dtgHariLibur_itemdataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        'If Not (arlNationalHoliday Is Nothing) Then
        '    If Not (arlNationalHoliday.Count = 0 Or e.Item.ItemIndex = -1) Then
        '        objNationalHoliday = arlNationalHoliday(e.Item.ItemIndex)
        '        e.Item.Cells(1).Text = objNationalHoliday.HolidayDate & "/" & objNationalHoliday.HolidayMonth & "/" & objNationalHoliday.HolidayYear
        '        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
        '            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        '        End If
        '    End If
        'End If
        If (e.Item.ItemIndex <> -1) Then
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            If Not e.Item.DataItem Is Nothing Then
                e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgHariLibur.CurrentPageIndex * dtgHariLibur.PageSize)
                'Dim objNationalHoliday As NationalHoliday = CType(e.Item.DataItem, NationalHoliday)
                'If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                ' e.Item.Cells(1).Text = objNationalHoliday.HolidayDate & "/" & objNationalHoliday.HolidayMonth & "/" & objNationalHoliday.HolidayYear
                'End If
            End If
        End If

        If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = m_bFormPrivilege
        End If

        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = m_bFormPrivilege
        End If

    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

#End Region

End Class

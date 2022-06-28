#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Event
#End Region

Public Class FrmEventType
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtEventType As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgEventType As System.Web.UI.WebControls.DataGrid

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
    Private arlEventType As ArrayList
    Private objEventType As EventType
    Private sHelper As SessionHelper = New SessionHelper
#End Region

#Region "Custom Method"
    Private Sub DeleteEventType(ByVal nID As Integer)
        Dim crit As CriteriaComposite
        Dim objEventType As EventType = New EventTypeFacade(User).Retrieve(nID)
        Dim objEventTypeFacade As EventTypeFacade = New EventTypeFacade(User)

        crit = New CriteriaComposite(New Criteria(GetType(EventInfo), "EventType.ID", MatchType.Exact, objEventType.ID))
        Dim arlEventInfo As ArrayList = New EventInfoFacade(User).Retrieve(crit)
        If (arlEventInfo.Count > 0) Then
            MessageBox.Show("Data tidak dapat dihapus karena digunakan pada Event Info")
        Else
            objEventTypeFacade.DeleteFromDB(objEventType)
        End If
        BindDataGrid(dtgEventType.CurrentPageIndex)
    End Sub
    Private Sub ClearData()
        txtEventType.Text = ""
        ViewState.Add("vsProcess", "Insert")
        dtgEventType.SelectedIndex = -1
    End Sub
    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            arlEventType = New EventTypeFacade(User).RetrieveActiveList(indexPage + 1, dtgEventType.PageSize, totalRow, sHelper.GetSession("SortCol"), sHelper.GetSession("SortDirection"))
            dtgEventType.DataSource = arlEventType
            dtgEventType.VirtualItemCount = totalRow
            dtgEventType.DataBind()
        End If
    End Sub
    Private Sub alertMessage(ByVal nResult As Integer)
        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
        End If
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            ClearData()
            sHelper.SetSession("SortCol", "Description")
            sHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            BindDataGrid(0)
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim Idedit As Integer
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            Idedit = 0
        ElseIf CType(ViewState("vsProcess"), String) = "Edit" Then
            Idedit = CType(sHelper.GetSession("objedit"), EventType).ID
        End If

        Dim nResult As Integer = -1
        If txtEventType.Text <> "" Then
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                Dim objEventType As EventType = New EventType
                objEventType.Description = txtEventType.Text.Trim
                If New EventTypeFacade(User).ValidateCode(objEventType.Description) > 0 Then
                    MessageBox.Show("Jenis Kegiatan " & objEventType.Description & " sudah pernah digunakan, ganti dengan kode lainnya!")
                Else
                    nResult = New EventTypeFacade(User).Insert(objEventType)
                    alertMessage(nResult)
                End If
            ElseIf CType(ViewState("vsProcess"), String) = "Edit" Then
                Dim objEventType As EventType = CType(sHelper.GetSession("objedit"), EventType)
                objEventType.Description = txtEventType.Text.Trim
                If New EventTypeFacade(User).ValidateCode(objEventType.Description, objEventType.ID) > 0 Then
                    MessageBox.Show("Jenis Kegiatan " & objEventType.Description & " sudah pernah digunakan, ganti dengan kode lainnya!")
                Else
                    nResult = New EventTypeFacade(User).Update(objEventType)
                    alertMessage(nResult)
                End If
            End If
        End If

        BindDataGrid(dtgEventType.CurrentPageIndex)
        ClearData()

    End Sub

    Private Sub dtgEventType_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEventType.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (arlEventType Is Nothing) Then
                objEventType = arlEventType(e.Item.ItemIndex)
                Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                _lblNo.Text = e.Item.ItemIndex + 1 + (dtgEventType.CurrentPageIndex * dtgEventType.PageSize)

                Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                _lbtnDelete.CommandArgument = objEventType.ID

                Dim _lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                _lbtnEdit.CommandArgument = objEventType.ID

                Dim _lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
                _lbtnView.CommandArgument = objEventType.ID
            End If
        End If
    End Sub

    Private Sub dtgEventType_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEventType.ItemCommand
        If e.CommandName = "Delete" Then
            DeleteEventType(e.CommandArgument)
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            dtgEventType.SelectedIndex = e.Item.ItemIndex
            Dim objEventType As EventType = New EventTypeFacade(User).Retrieve(CInt(e.CommandArgument))
            sHelper.SetSession("objedit", objEventType)
            txtEventType.Text = objEventType.Description
            btnSimpan.Enabled = True
            txtEventType.ReadOnly = False

        ElseIf e.CommandName = "View" Then
            dtgEventType.SelectedIndex = e.Item.ItemIndex
            Dim objEventType As EventType = New EventTypeFacade(User).Retrieve(CInt(e.CommandArgument))
            txtEventType.Text = objEventType.Description
            txtEventType.ReadOnly = True
            btnSimpan.Enabled = False


        End If
    End Sub

    Private Sub dtgEventType_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEventType.SortCommand
        If e.SortExpression = sHelper.GetSession("SortCol") Then
            If sHelper.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sHelper.SetSession("SortCol", e.SortExpression)
        dtgEventType.SelectedIndex = -1
        dtgEventType.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub

    Private Sub dtgEventType_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEventType.PageIndexChanged
        dtgEventType.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgEventType.CurrentPageIndex)
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        btnSimpan.Enabled = True
        txtEventType.ReadOnly = False
    End Sub
End Class

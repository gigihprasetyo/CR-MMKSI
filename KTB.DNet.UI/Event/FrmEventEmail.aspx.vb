Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports System.Text.RegularExpressions
Public Class FrmEventEmail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents dgEventEmail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
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
    Private _sessHelper As New SessionHelper
    Private objEmailFacade As New EventEmailReceiverFacade(User)
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack() Then
            ViewState("CurrentSortColumn") = "Name"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            Viewstate.Add("vsproses", "Insert")
            BindData(0)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objEmail As New EventEmailReceiver
        Dim n As Integer
        If CheckDataValid() Then
            If CType(ViewState("vsproses"), String) = "Insert" Then
                objEmail.Email = txtEmail.Text
                objEmail.Name = txtName.Text.Trim
                n = objEmailFacade.Insert(objEmail)
            Else
                objEmail = CType(_sessHelper.GetSession("EDITCB"), EventEmailReceiver)
                objEmail.Email = txtEmail.Text.Trim
                objEmail.Name = txtName.Text.Trim
                n = objEmailFacade.Update(objEmail)
                Viewstate.Add("vsproses", "Insert")
            End If
            If n <> -1 Then
                MessageBox.Show("Simpan sukses")
                BindData(dgEventEmail.CurrentPageIndex)
            Else
                MessageBox.Show("Simpan gagal")
            End If
            ClearData()
            dgEventEmail.SelectedIndex = -1

        End If
    End Sub
    Private Sub ClearData()
        _sessHelper.RemoveSession("EDITCB")
        txtName.Text = String.Empty
        txtEmail.Text = String.Empty
    End Sub
    Private Sub BindData(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(EventEmailReceiver), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        arrList = objEmailFacade.RetrieveByCriteria(criterias, indexPage + 1, dgEventEmail.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgEventEmail.DataSource = arrList
        dgEventEmail.VirtualItemCount = totalRow
        dgEventEmail.DataBind()
    End Sub

    Private Sub dgEventEmail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEventEmail.ItemCommand
        Select Case e.CommandName
            Case "Edit"
                ViewState.Add("vsproses", "Edit")
                setControlUpdate(Integer.Parse(e.Item.Cells(0).Text))
            Case "Delete"
                Dim objEmailDelete As EventEmailReceiver = objEmailFacade.Retrieve(Integer.Parse(e.Item.Cells(0).Text))
                objEmailFacade.DeleteFromDB(objEmailDelete)
                MessageBox.Show("Data sudah dihapus")
                BindData(dgEventEmail.CurrentPageIndex)
        End Select
    End Sub
    Private Sub setControlUpdate(ByVal id As Integer)
        Dim objEmail As EventEmailReceiver = objEmailFacade.Retrieve(id)
        txtName.Text = objEmail.Name
        txtEmail.Text = objEmail.Email
        _sessHelper.SetSession("EDITCB", objEmail)
    End Sub

    Private Function CheckDataValid() As Boolean
        Dim objEmail As EventEmailReceiver = CType(_sessHelper.GetSession("EDITCB"), EventEmailReceiver)
        Dim bCheck As Boolean = True
        Dim criterias As New CriteriaComposite(New Criteria(GetType(EventEmailReceiver), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(EventEmailReceiver), "Name", MatchType.Exact, txtName.Text.Trim))
        criterias.opAnd(New Criteria(GetType(EventEmailReceiver), "Email", MatchType.Exact, txtEmail.Text.Trim))

        If CType(ViewState("vsproses"), String) = "Edit" Then
            criterias.opAnd(New Criteria(GetType(EventEmailReceiver), "ID", MatchType.No, objEmail.ID))
        End If
        Dim _arr As ArrayList = objEmailFacade.RetrieveByCriteria(criterias)
        If _arr.Count > 0 Then
            bCheck = False
            MessageBox.Show("Duplikasi nama dan email")
        End If
        If txtName.Text.Trim = String.Empty Then
            bCheck = False
            MessageBox.Show("Kode masih kosong")
        End If
        If txtEmail.Text.Trim = String.Empty Then
            bCheck = False
            MessageBox.Show("Deskripsi masih kosong")
        End If
        If EmailAddressCheck(txtEmail.Text.Trim) = False Then
            bCheck = False
            MessageBox.Show("Alamat email tidak valid")
        End If
        Return bCheck
    End Function

    Private Function EmailAddressCheck(ByVal emailAddress As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)
        If emailAddressMatch.Success Then
            EmailAddressCheck = True
        Else
            EmailAddressCheck = False
        End If
    End Function
    Private Sub dgEventEmail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgEventEmail.ItemDataBound

    End Sub

    Private Sub dgEventEmail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgEventEmail.PageIndexChanged
        dgEventEmail.CurrentPageIndex = e.NewPageIndex
        BindData(dgEventEmail.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dgEventEmail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgEventEmail.SortCommand
        If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("currentSortColumn") = e.SortExpression
            viewstate("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgEventEmail.SelectedIndex = -1
        dgEventEmail.CurrentPageIndex = 0
        bindGridSorting(dgEventEmail.CurrentPageIndex)
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            If CType(_sessHelper.GetSession("BINDSTATUS"), String) = "CARI" Then
                dgEventEmail.DataSource = objEmailFacade.RetrieveByCriteria(CType(_sessHelper.GetSession("sortViewVC"), CriteriaComposite), indexPage + 1, dgEventEmail.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            Else
                Dim criterias As New CriteriaComposite(New Criteria(GetType(EventEmailReceiver), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                dgEventEmail.DataSource = objEmailFacade.RetrieveByCriteria(criterias, indexPage + 1, dgEventEmail.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            End If
            dgEventEmail.VirtualItemCount = totalRow
            dgEventEmail.DataBind()
        End If

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClearData()
        Viewstate.Add("vsproses", "Insert")
        dgEventEmail.SelectedIndex = -1
    End Sub
End Class

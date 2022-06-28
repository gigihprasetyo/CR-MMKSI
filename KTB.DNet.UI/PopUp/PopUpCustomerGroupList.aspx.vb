Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade

Public Class PopUpCustomerGroupList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents dtgCustomerGroupSelection As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtKode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNama As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Protected countChk As Integer = 0
    Dim criterias As CriteriaComposite
    Private sHelper As SessionHelper = New SessionHelper
    Private strSQL As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            Dim term As String = Request.QueryString("Tyjiuy678")
            Dim tipe As String = Request.QueryString("tipe")
            If tipe <> "" Then
                txtNama.Text = tipe
            End If
            If term = "code" Then
                sHelper.SetSession("SortColPopUp", "Code")
                sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.ASC)
                BindSearch(0)
                If dtgCustomerGroupSelection.Items.Count < 1 Then
                    Response.Write("<script languange='javascript'>alert('Tidak Ada Kode');window.close();</script>")
                End If
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch(0)
        If dtgCustomerGroupSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub
    Private Sub ClearData()
        txtKode.Text = String.Empty
        txtNama.Text = String.Empty
    End Sub
    Public Sub BindSearch(ByVal indexPage As Integer)
        dtgCustomerGroupSelection.CurrentPageIndex = indexPage
        Dim totalRow As Integer = 0
        Dim _arr As New ArrayList
        Dim term As String = Request.QueryString("Tyjiuy678")


        If term = "code" Then
            CreateCriteriaForCode()
            If Not IsNothing(criterias) Then
                _arr = New CustomerGroupFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgCustomerGroupSelection.PageSize, totalRow, sHelper.GetSession("SortColPopUp"), sHelper.GetSession("SortDirectionPopUp"))
            End If
        End If
        If _arr.Count > 0 Then
            If indexPage >= 0 Then
                dtgCustomerGroupSelection.DataSource = _arr
                dtgCustomerGroupSelection.VirtualItemCount = totalRow
                sHelper.SetSession("DATA", _arr)
                dtgCustomerGroupSelection.DataBind()
            End If
        Else
            dtgCustomerGroupSelection.DataSource = New ArrayList
            MessageBox.Show("Data tidak ditemukan")
        End If


        If dtgCustomerGroupSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub
    
    Private Sub CreateCriteriaForCode()
        Dim _containerID As String = String.Empty
       
        Dim critNama As String = ""

        If txtNama.Text.Trim <> "" Then
            Dim i As Integer = 0
            For Each item As String In txtNama.Text.Split(" ")
                If i = 0 Then
                    critNama += " and Name like '%" & item & "%' "
                Else
                    critNama += " or Name like '%" & item & "%' "

                End If
                i = i + 1
            Next
        End If

        Dim critKode As String = ""

        If txtKode.Text <> "" Then
            critKode = " and Code like '%" & txtKode.Text.Trim & "%' "
        End If

        strSQL = "select ID from CustomerGroup where RowStatus=" & CType(DBRowStatus.Active, Short) & critKode & critNama

        criterias = New CriteriaComposite(New Criteria(GetType(CustomerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CustomerGroup), "ID", MatchType.InSet, "(" & strSQL & ")"))

    End Sub
    Private Sub dtgCustomerGroupSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCustomerGroupSelection.PageIndexChanged
        dtgCustomerGroupSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch(dtgCustomerGroupSelection.CurrentPageIndex)
    End Sub
    Private Sub dtgCustomerGroupSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCustomerGroupSelection.SortCommand
        If e.SortExpression = sHelper.GetSession("SortColPopUp") Then
            If sHelper.GetSession("SortDirectionPopUp") = Sort.SortDirection.ASC Then
                sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.DESC)
            Else
                sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.ASC)
            End If
        End If
        sHelper.SetSession("SortColPopUp", e.SortExpression)
        dtgCustomerGroupSelection.SelectedIndex = -1
        dtgCustomerGroupSelection.CurrentPageIndex = 0
        BindSearch(dtgCustomerGroupSelection.CurrentPageIndex)
    End Sub

    Private Sub dtgCustomerGroupSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCustomerGroupSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim term As String = Request.QueryString("Tyjiuy678")
        If Not e.Item.DataItem Is Nothing Then
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)
            End If

        End If
    End Sub
End Class
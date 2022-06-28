#Region "Custom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports KTB.DNet.BusinessFacade.General

#End Region


Public Class FrmSalesmanLevel
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgSalesmanLevel As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variable"
    Dim _sessHelper As New SessionHelper
#End Region

#Region "Custom Method"
    Public Sub BindDataGrid(ByVal idx As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanLevel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        _sessHelper.SetSession("SortViewPM", criterias)
        arrList = New SalesmanHeaderFacade(User).RetrieveSalesmanLevel(criterias, idx + 1, dgSalesmanLevel.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgSalesmanLevel.DataSource = arrList
        dgSalesmanLevel.VirtualItemCount = totalRow
        dgSalesmanLevel.DataBind()
    End Sub
    Private Sub setControlUpdate(ByVal id As Integer)
        Dim objSalesmanLevel As SalesmanLevel = New SalesmanHeaderFacade(User).RetrieveSalesmanLevelByID(id)
        txtDeskripsi.Text = objSalesmanLevel.Description
        'Todo session
        'Session.Add("vsSalesmanLevel", objSalesmanLevel)
        _sessHelper.SetSession("vsSalesmanLevel", objSalesmanLevel)
    End Sub
    Private Sub deleteData(ByVal id As Integer)
        Dim objSalesmanFacade As New SalesmanHeaderFacade(User)
        Dim objSalesmanLevel As SalesmanLevel = New SalesmanHeaderFacade(User).RetrieveSalesmanLevelByID(id)
        Dim result As Integer = objSalesmanFacade.DeleteFromDB(objSalesmanLevel)
        If result = -1 Then
            MessageBox.Show(SR.DeleteFail)
        Else
            BindDataGrid(dgSalesmanLevel.CurrentPageIndex)
        End If
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dgSalesmanLevel.DataSource = New SalesmanHeaderFacade(User).RetrieveActiveListSalesmanLevel(CType(_sessHelper.GetSession("sortViewPM"), CriteriaComposite), indexPage + 1, dgSalesmanLevel.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgSalesmanLevel.VirtualItemCount = totalRow
            dgSalesmanLevel.DataBind()
        End If
    End Sub
    Private Sub ClearData()
        txtDeskripsi.Text = ""
    End Sub

#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        btnSave.Visible = ChecKCreatePriv()
        If Not IsPostBack() Then
            ViewState("CurrentSortColumn") = "Description"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            Viewstate.Add("vsproses", "Insert")
            BindDataGrid(0)
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtDeskripsi.Text = "" Then
            MessageBox.Show("Deskripsi tidak boleh kosong")
            Return
        End If

        Dim objSalesmanLevel As New SalesmanLevel
        Dim n As Integer = -1
        If CType(ViewState("vsproses"), String) = "Insert" Then
            objSalesmanLevel.Description = txtDeskripsi.Text
            n = New SalesmanHeaderFacade(User).Insert(objSalesmanLevel)
        Else
            objSalesmanLevel = CType(_sessHelper.GetSession("vsSalesmanLevel"), SalesmanLevel)
            objSalesmanLevel.Description = txtDeskripsi.Text
            n = New SalesmanHeaderFacade(User).Update(objSalesmanLevel)
            Viewstate.Add("vsproses", "Insert")
        End If
        If n >= 0 Then
            MessageBox.Show("Simpan sukses")
            BindDataGrid(dgSalesmanLevel.CurrentPageIndex)
        Else
            MessageBox.Show("Simpan gagal")
        End If
        ClearData()
        dgSalesmanLevel.SelectedIndex = -1
    End Sub
    Private Sub dgSalesmanLevel_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanLevel.ItemCommand
        Select Case e.CommandName
            Case "Edit"
                ViewState.Add("vsproses", "Edit")
                setControlUpdate(Integer.Parse(e.Item.Cells(0).Text))
            Case "Delete"
                deleteData(Integer.Parse(e.Item.Cells(0).Text))
        End Select
    End Sub
    Private Sub dgSalesmanLevel_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSalesmanLevel.SortCommand
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

        dgSalesmanLevel.SelectedIndex = -1
        dgSalesmanLevel.CurrentPageIndex = 0
        bindGridSorting(dgSalesmanLevel.CurrentPageIndex)
    End Sub
    Private Sub dgSalesmanLevel_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesmanLevel.PageIndexChanged
        dgSalesmanLevel.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSalesmanLevel.CurrentPageIndex)
        ClearData()
    End Sub
    Private Sub dgSalesmanLevel_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanLevel.ItemDataBound
        Dim objSalesLevel As SalesmanLevel = e.Item.DataItem
        If Not e.Item.DataItem Is Nothing Then
            Dim lblCreatedTime As Label = CType(e.Item.FindControl("lblCreatedTime"), Label)
            lblCreatedTime.Text = objSalesLevel.CreatedTime.ToShortDateString

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgSalesmanLevel.CurrentPageIndex * dgSalesmanLevel.PageSize)

            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")

            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)

            If ChecKCreatePriv() Then
                lbtnDelete.Visible = True
                lbtnEdit.Visible = True
                Else
                lbtnDelete.Visible = False
                lbtnEdit.Visible = False
            End If
        End If

    End Sub
    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        ClearData()
        Viewstate.Add("vsproses", "Insert")
    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.LTPView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Tenaga Penjual - Level Tenaga Penjual")
        End If
    End Sub
    Private Function ChecKCreatePriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.LTPCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

End Class

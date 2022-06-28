#Region "Custom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports KTB.DNet.BusinessFacade.SparePart
#End Region

Public Class FrmPartIncidentalPriorityDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPrioritas As System.Web.UI.WebControls.Label
    Protected WithEvents lblDeskripsi As System.Web.UI.WebControls.Label
    Protected WithEvents dgPriorityDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button

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
    Private sessHelper As New SessionHelper
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            Dim objFacade As New PartIncidentalPriorityFacade(User)
            Dim objPriority As PartIncidentalPriority = objFacade.Retrieve(CInt(Request.QueryString("ID")))
            lblDeskripsi.Text = objPriority.Description
            lblPrioritas.Text = objPriority.Priority

            sessHelper.SetSession("objPriority", objPriority)
            sessHelper.SetSession("CurentSortColumnDetail", "TypeCode")
            sessHelper.SetSession("CurentSortDirectionDetail", Sort.SortDirection.ASC)
            BindToGrid()
        End If
    End Sub

    Private Sub BindToGrid()

        Dim criterias As New CriteriaComposite(New Criteria(GetType(PartIncidentalPriorityDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PartIncidentalPriorityDetail), "PartIncidentalPriority.ID", MatchType.Exact, Request.QueryString("ID")))

        Dim obj As PartIncidentalPriorityDetail

        Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
        sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PartIncidentalPriorityDetail), sessHelper.GetSession("CurentSortColumnDetail"), sessHelper.GetSession("CurentSortDirectionDetail")))

        Dim arrList As ArrayList = New PartIncidentalPriorityDetailFacade(User).Retrieve(criterias, sortColl)

        dgPriorityDetail.DataSource = arrList
        dgPriorityDetail.DataBind()

    End Sub

    Private Sub dgPriorityDetail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPriorityDetail.SortCommand
        If sessHelper.GetSession("CurentSortColumnDetail") = e.SortExpression Then
            If CType(sessHelper.GetSession("CurentSortDirectionDetail"), Sort.SortDirection) = Sort.SortDirection.ASC Then
                sessHelper.SetSession("CurentSortDirectionDetail", Sort.SortDirection.DESC)
            Else
                sessHelper.SetSession("CurentSortDirectionDetail", Sort.SortDirection.ASC)
            End If
        Else
            sessHelper.SetSession("CurentSortDirectionDetail", Sort.SortDirection.ASC)
        End If

        sessHelper.SetSession("CurentSortColumnDetail", e.SortExpression)

        BindToGrid()
    End Sub

    Private Sub dgPriorityDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPriorityDetail.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lbtnDelete As LinkButton = e.Item.FindControl("lbtnDelete")

            lblNo.Text = (dgPriorityDetail.CurrentPageIndex * dgPriorityDetail.PageSize + e.Item.ItemIndex + 1).ToString() '-- Column No
            lbtnDelete.Attributes.Add("onclick", "return confirm('Anda yakin akan menghapus data kode tipe " & CType(e.Item.DataItem, PartIncidentalPriorityDetail).TypeCode & " ?');")

        ElseIf e.Item.ItemType = ListItemType.EditItem Then
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim ddlEditTahun As DropDownList = e.Item.FindControl("ddlEditTahun")

            lblNo.Text = (dgPriorityDetail.CurrentPageIndex * dgPriorityDetail.PageSize + e.Item.ItemIndex + 1).ToString() '-- Column No
            IsiTahun(ddlEditTahun)
            ddlEditTahun.SelectedValue = CType(e.Item.DataItem, PartIncidentalPriorityDetail).StartProdYear

            e.Item.Cells(0).Text = CType(e.Item.DataItem, PartIncidentalPriorityDetail).ID

        ElseIf e.Item.ItemType = ListItemType.Footer Then
            Dim ddlInputTahun As DropDownList = e.Item.FindControl("ddlInputTahun")
            IsiTahun(ddlInputTahun)

        End If
    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Response.Redirect("FrmPartIncidentalPriority.aspx")
    End Sub

    Private Sub IsiTahun(ByRef ddl As DropDownList)
        For i As Integer = 1980 To Date.Today.Year
            ddl.Items.Add(i.ToString)
        Next
        ddl.SelectedIndex = ddl.Items.Count - 1
    End Sub

    Private Sub dgPriorityDetail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPriorityDetail.ItemCommand
        If e.CommandName = "SaveInput" Then

            Dim txtInputKode As TextBox = e.Item.FindControl("txtInputKode")
            If txtInputKode.Text.Length <> 4 Then
                MessageBox.Show("Kode tipe harus 4 digit")
                Return
            End If

            Dim criterias As New CriteriaComposite(New Criteria(GetType(PartIncidentalPriorityDetail), "TypeCode", MatchType.Exact, txtInputKode.Text.Trim))
            Dim arlExist As ArrayList = New PartIncidentalPriorityDetailFacade(User).Retrieve(criterias)

            If arlExist.Count > 0 Then
                MessageBox.Show("Kode tipe " & txtInputKode.Text & " sudah ada pada prioritas " & CType(arlExist(0), PartIncidentalPriorityDetail).PartIncidentalPriority.Priority)
                Return
            End If

            Dim objToSave As New PartIncidentalPriorityDetail
            objToSave.TypeCode = txtInputKode.Text.Trim
            objToSave.StartProdYear = CType(e.Item.FindControl("ddlInputTahun"), DropDownList).SelectedItem.Text
            objToSave.PartIncidentalPriority = sessHelper.GetSession("objPriority")


            Dim result As Integer = New PartIncidentalPriorityDetailFacade(User).Insert(objToSave)
            BindToGrid()

        ElseIf e.CommandName = "Delete" Then
            Dim objFacade As New PartIncidentalPriorityDetailFacade(User)
            Dim objToDelete As PartIncidentalPriorityDetail = objFacade.Retrieve(CInt(e.Item.Cells(0).Text))
            objFacade.DeleteFromDB(objToDelete)
            BindToGrid()
        ElseIf e.CommandName = "Edit" Then
            dgPriorityDetail.ShowFooter = False
            dgPriorityDetail.EditItemIndex = e.Item.ItemIndex
            BindToGrid()
        ElseIf e.CommandName = "CancelEdit" Then
            dgPriorityDetail.ShowFooter = True
            dgPriorityDetail.EditItemIndex = -1
            BindToGrid()
        ElseIf e.CommandName = "SaveEdit" Then

            Dim txtEditKode As TextBox = e.Item.FindControl("txtEditKode")
            If txtEditKode.Text.Trim.Length <> 4 Then
                MessageBox.Show("Kode tipe harus 4 digit")
                Return
            End If

            Dim objFacade As New PartIncidentalPriorityDetailFacade(User)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(PartIncidentalPriorityDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PartIncidentalPriorityDetail), "ID", MatchType.No, CInt(e.Item.Cells(0).Text)))
            criterias.opAnd(New Criteria(GetType(PartIncidentalPriorityDetail), "TypeCode", MatchType.Exact, txtEditKode.Text.Trim))

            Dim arlExist As ArrayList = objfacade.Retrieve(criterias)
            If arlexist.Count > 0 Then
                MessageBox.Show("Kode tipe " & txtEditKode.Text & " sudah ada pada prioritas " & CType(arlexist(0), PartIncidentalPriorityDetail).PartIncidentalPriority.Priority)
                Return
            End If

            Dim objToUpdate As PartIncidentalPriorityDetail = objFacade.Retrieve(CInt(e.Item.Cells(0).Text))
            objToUpdate.StartProdYear = CType(e.Item.FindControl("ddlEditTahun"), DropDownList).SelectedValue
            objToUpdate.TypeCode = txtEditKode.Text.Trim
            objFacade.Update(objToUpdate)
            dgPriorityDetail.ShowFooter = True
            dgPriorityDetail.EditItemIndex = -1
            BindToGrid()

        End If
    End Sub
End Class

#Region "Custom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports KTB.DNet.BusinessFacade.SparePart
#End Region

Public Class FrmPartIncidentalPriority
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents txtPrioritas As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgPriority As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ValPrioritas As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ValDeskripsi As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button

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
            sessHelper.SetSession("isInsert", 1)
            dgPriority.SelectedIndex = -1
            If IsNothing(sessHelper.GetSession("SessCriteriaPriority")) Then
                sessHelper.SetSession("CurentSortColumn", "ID")
                sessHelper.SetSession("CurentSortDirection", Sort.SortDirection.ASC)
                btnCari_Click(Me, System.EventArgs.Empty)
            Else
                BindDataGrid(sessHelper.GetSession("CurrentPageIndex"))
            End If
        End If

    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        sessHelper.SetSession("CurrentPageIndex", 0)
        dgPriority.CurrentPageIndex = sessHelper.GetSession("CurrentPageIndex")
        CreateCriteria()
        BindDataGrid(sessHelper.GetSession("CurrentPageIndex"))
    End Sub


    Private Sub CreateCriteria()

        Dim criterias As New CriteriaComposite(New Criteria(GetType(PartIncidentalPriority), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PartIncidentalPriority), "ID", MatchType.No, CInt(KTB.DNet.Lib.WebConfig.GetValue("PartIncidentalPriorityIDOther"))))
        If txtPrioritas.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(PartIncidentalPriority), "Priority", MatchType.[Partial], txtPrioritas.Text.Trim()))
        End If
        If txtDeskripsi.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(PartIncidentalPriority), "Description", MatchType.[Partial], txtDeskripsi.Text.Trim()))
        End If

        sessHelper.SetSession("SessCriteriaPriority", criterias)


    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        Dim arrList As ArrayList = New PartIncidentalPriorityFacade(User).RetrieveByCriteria(CType(sessHelper.GetSession("SessCriteriaPriority"), CriteriaComposite), idxPage + 1, dgPriority.PageSize, totalRow, _
            CType(sessHelper.GetSession("CurentSortColumn"), String), CType(sessHelper.GetSession("CurentSortDirection"), Sort.SortDirection))

        dgPriority.DataSource = arrList
        dgPriority.VirtualItemCount = totalRow

        dgPriority.DataBind()

    End Sub

    Private Sub dgPriority_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPriority.PageIndexChanged
        sessHelper.SetSession("CurrentPageIndex", e.NewPageIndex)
        dgPriority.CurrentPageIndex = sessHelper.GetSession("CurrentPageIndex")
        BindDataGrid(sessHelper.GetSession("CurrentPageIndex"))

    End Sub

    Private Sub dgPriority_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPriority.SortCommand
        sessHelper.SetSession("CurrentPageIndex", 1)

        If sessHelper.GetSession("CurentSortColumn") = e.SortExpression Then
            If CType(sessHelper.GetSession("CurentSortDirection"), Sort.SortDirection) = Sort.SortDirection.ASC Then
                sessHelper.SetSession("CurentSortDirection", Sort.SortDirection.DESC)
            Else
                sessHelper.SetSession("CurentSortDirection", Sort.SortDirection.ASC)
            End If
        Else
            sessHelper.SetSession("CurentSortDirection", Sort.SortDirection.ASC)
        End If

        sessHelper.SetSession("CurentSortColumn", e.SortExpression)

        BindDataGrid(sessHelper.GetSession("CurrentPageIndex"))


    End Sub

    Private Sub dgPriority_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPriority.ItemDataBound
        If e.Item.ItemIndex <> -1 Then

            Dim lbtnView As LinkButton = e.Item.FindControl("lbtnView")
            Dim lbtnEdit As LinkButton = e.Item.FindControl("lbtnEdit")
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lbtnDelete As LinkButton = e.Item.FindControl("lbtnDelete")
            lblNo.Text = (dgPriority.CurrentPageIndex * dgPriority.PageSize + e.Item.ItemIndex + 1).ToString() '-- Column No

            'If KTB.DNet.Lib.WebConfig.GetValue("PartIncidentalPriorityIDOther") <> e.Item.Cells(0).Text Then
            '    lbtnView.Visible = False
            '    lbtnDelete.Visible = False
            'End If

            If lbtnDelete.Visible = True Then
                lbtnDelete.Attributes.Add("onclick", "return confirm('Anda yakin akan menghapus data prioritas " & CType(e.Item.DataItem, PartIncidentalPriority).Priority & " ?');")
            End If
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim objFacade As New PartIncidentalPriorityFacade(User)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(PartIncidentalPriority), "Priority", MatchType.Exact, txtPrioritas.Text.Trim()))
        If Val(sessHelper.GetSession("isInsert")) = 0 Then
            criterias.opAnd(New Criteria(GetType(PartIncidentalPriority), "ID", MatchType.No, sessHelper.GetSession("IDEdit")))
        End If

        Dim arlExist As ArrayList = objFacade.Retrieve(criterias)

        If arlExist.Count > 0 Then
            MessageBox.Show("Prioritas Tidak Boleh Dobel")
            Return
        End If

        Dim objPriority As PartIncidentalPriority

        If Val(sessHelper.GetSession("isInsert")) = 1 Then
            objPriority = New PartIncidentalPriority
            objPriority.Priority = txtPrioritas.Text.Trim
            objPriority.Description = txtDeskripsi.Text.Trim
            Dim result As Integer = objFacade.Insert(objPriority)
            MessageBox.Show("Input Data Berhasil")
        Else
            objPriority = objFacade.Retrieve(CInt(sessHelper.GetSession("IDEdit")))
            objPriority.Priority = txtPrioritas.Text.Trim
            objPriority.Description = txtDeskripsi.Text.Trim
            objFacade.Update(objPriority)
            MessageBox.Show("Ubah Data Berhasil")
        End If

        btnBatal_Click(Me, System.EventArgs.Empty)
        BindDataGrid(sessHelper.GetSession("CurrentPageIndex"))


    End Sub

    Private Sub dgPriority_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPriority.ItemCommand
        If e.CommandName = "Delete" Then
            Dim objFacade As New PartIncidentalPriorityFacade(User)
            Dim objPriority As PartIncidentalPriority = objFacade.Retrieve(CInt(e.Item.Cells(0).Text))
            objFacade.DeleteFromDB(objPriority)
        ElseIf e.CommandName = "Edit" Then
            Dim objFacade As New PartIncidentalPriorityFacade(User)
            Dim objPriority As PartIncidentalPriority = objFacade.Retrieve(CInt(e.Item.Cells(0).Text))
            sessHelper.SetSession("isInsert", 0)
            sessHelper.SetSession("IDEdit", CInt(e.Item.Cells(0).Text))
            txtDeskripsi.Text = objPriority.Description
            txtPrioritas.Text = objPriority.Priority
            dgPriority.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "View" Then
            'Dim objFacade As New PartIncidentalPriorityFacade(User)
            'Dim objPriority As PartIncidentalPriority = objFacade.Retrieve(CInt(e.Item.Cells(0).Text))
            Response.Redirect("FrmPartIncidentalPriorityDetail.aspx?ID=" & e.Item.Cells(0).Text)

        End If
        Try
            BindDataGrid(sessHelper.GetSession("CurrentPageIndex"))
        Catch ex As Exception
            sessHelper.SetSession("CurrentPageIndex", sessHelper.GetSession("CurrentPageIndex") - 1)
            dgPriority.CurrentPageIndex = sessHelper.GetSession("CurrentPageIndex")
            BindDataGrid(sessHelper.GetSession("CurrentPageIndex"))
        End Try

    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        sessHelper.SetSession("isInsert", 1)
        txtDeskripsi.Text = ""
        txtPrioritas.Text = ""
        dgPriority.SelectedIndex = -1
    End Sub
End Class

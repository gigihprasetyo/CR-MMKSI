#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Tools
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmReportList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgReport As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtReportName As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator10 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Declaration"
    Dim sHelper As New SessionHelper
    Dim arrList As ArrayList
    Dim criterias As CriteriaComposite
#End Region

#Region "Event"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            ViewState("CurrentSortColumn") = "Title"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            If Request.QueryString("isBack") <> String.Empty And Request.QueryString("isBack") = "true" Then
                ViewState("CurrentSortColumn") = sHelper.GetSession("SortColumnGroup")
                ViewState("CurrentSortDirect") = sHelper.GetSession("SortDirectGroup")
                BindGrid(sHelper.GetSession("idxPageGroup"))
            Else
                BindGrid(0)
            End If
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgReport.CurrentPageIndex = 0
        BindGrid(dtgReport.CurrentPageIndex)
    End Sub

    Private Sub dtgReport_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgReport.ItemCommand
        If e.CommandName = "Delete" Then
            Try
                Dim facade As BCPQueryFacade = New BCPQueryFacade(User)
                facade.Delete(facade.Retrieve(CInt(e.CommandArgument)))
                MessageBox.Show(SR.DeleteSucces)
                BindGrid(0)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
           
            

        ElseIf e.CommandName = "Roles" Then
            Response.Redirect("FrmAccessRoles.aspx?id=" & e.CommandArgument)
        End If
    End Sub

    Private Sub dtgReport_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgReport.ItemDataBound
        If (e.Item.ItemIndex >= 0) Then
            Dim obj As BCPQuery = e.Item.DataItem

            Dim lblno As Label = CType(e.Item.FindControl("lblno"), Label)
            lblno.Text = (dtgReport.CurrentPageIndex * dtgReport.PageSize) + e.Item.ItemIndex + 1

            Dim lblIsKTB As Label = CType(e.Item.FindControl("lblIsKTB"), Label)
            If obj.IsKTB = 0 Then
                lblIsKTB.Text = "Tidak"
            Else
                lblIsKTB.Text = "Ya"
            End If
            Dim lblIsDealer As Label = CType(e.Item.FindControl("lblIsDealer"), Label)
            If obj.IsDealer = 0 Then
                lblIsDealer.Text = "Tidak"
            Else
                lblIsDealer.Text = "Ya"
            End If

            'cek privilege
            Dim lbtndelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)

            lbtndelete.CommandArgument = obj.ID.ToString
            lbtndelete.Attributes.Add("onclick", "return confirm('Yakin ingin dihapus?');")
            'lbtndelete.Visible = bCekEditGroupPriv
            lbtnView.CommandArgument = obj.ID.ToString
            'lbtnView.Visible = bCekEditGroupPriv

        End If
    End Sub

#End Region

#Region "Custom"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.Tools_lihat_daftar_report_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Tools Download - Daftar Report")
        End If
    End Sub

    Private Sub BindGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        CreateCriteria()
        arrList = New BCPQueryFacade(User).RetrieveActiveList(criterias, idxPage + 1, dtgReport.PageSize, totalRow, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))

        dtgReport.CurrentPageIndex = idxPage
        If arrList.Count > 0 Then
            dtgReport.DataSource = arrList
        Else
            dtgReport.DataSource = New ArrayList
            MessageBox.Show("Data tidak ditemukan")
        End If

        dtgReport.VirtualItemCount = totalRow
        dtgReport.DataBind()

        sHelper.SetSession("SortColumnGroup", ViewState("CurrentSortColumn"))
        sHelper.SetSession("SortDirectGroup", ViewState("CurrentSortDirect"))
        sHelper.SetSession("idxPageGroup", idxPage)
    End Sub


    Private Sub CreateCriteria()
        criterias = New CriteriaComposite(New Criteria(GetType(BCPQuery), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtReportName.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(BCPQuery), "Title", MatchType.[Partial], txtReportName.Text.Trim))
        End If

        If txtDeskripsi.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(BCPQuery), "Description", MatchType.Partial, txtDeskripsi.Text.Trim))
        End If
    End Sub

#End Region

    Protected Sub dtgReport_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgReport.PageIndexChanged

        dtgReport.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)

    End Sub
End Class

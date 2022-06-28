#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade

#End Region


Public Class PopUpProductionYearMultipleSelection
    Inherits System.Web.UI.Page

#Region " custom Declaration "
    Dim _vechileTypeGeneralID As String = ""
    Dim _modelYear As String = ""
#End Region


#Region " Custom Method "

#End Region

#Region " Event Hendler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsNothing(Request.QueryString("VechileTypeGeneralID")) AndAlso Request.QueryString("VechileTypeGeneralID") <> "" Then
            _vechileTypeGeneralID = Request.QueryString("VechileTypeGeneralID")
        End If
        If Not IsNothing(Request.QueryString("ModelYear")) AndAlso Request.QueryString("ModelYear") <> "" Then
            _modelYear = Request.QueryString("ModelYear")
        End If

        If Not IsPostBack Then
            ViewState.Add("SortCol", "ProductionYear")
            ViewState.Add("SortDir", Sort.SortDirection.ASC)
            BindDataGrid()
            If dtgProductionYear.Items.Count > 0 Then
                btnChoose.Disabled = False
            Else
                btnChoose.Disabled = True
                MessageBox.Show("Data tidak ditemukan")
            End If
        End If
    End Sub

    Private Sub BindDataGrid()
        If _vechileTypeGeneralID = String.Empty Then Exit Sub
        dtgProductionYear.DataSource = New VechileColorIsActiveOnPKFacade(User).RetrieveByVechileTypeGeneralIDAndModelYear(_vechileTypeGeneralID.Trim, _modelYear.Trim)
        dtgProductionYear.DataBind()
    End Sub

    Private Sub dtgProductionYear_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgProductionYear.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgProductionYear.CurrentPageIndex * dtgProductionYear.PageSize)
        End If
    End Sub

#End Region

    Private Sub dtgProductionYear_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgProductionYear.SortCommand
        If e.SortExpression = ViewState("SortCol") Then
            If ViewState("SortDir") = Sort.SortDirection.ASC Then
                ViewState.Add("SortDir", Sort.SortDirection.DESC)
            Else
                ViewState.Add("SortDir", Sort.SortDirection.ASC)
            End If
        End If
        ViewState.Add("SortCol", e.SortExpression)
        BindDataGrid()
    End Sub

    Private Sub dtgProductionYear_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgProductionYear.PageIndexChanged
        dtgProductionYear.CurrentPageIndex = e.NewPageIndex
        BindDataGrid()
    End Sub

End Class
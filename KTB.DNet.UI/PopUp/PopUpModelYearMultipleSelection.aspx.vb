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


Public Class PopUpModelYearMultipleSelection
    Inherits System.Web.UI.Page

#Region " custom Declaration "
    Dim _vechileTypeGeneralID As String = ""
#End Region


#Region " Custom Method "

#End Region

#Region " Event Hendler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsNothing(Request.QueryString("VechileTypeGeneralID")) AndAlso Request.QueryString("VechileTypeGeneralID") <> "" Then
            _vechileTypeGeneralID = Request.QueryString("VechileTypeGeneralID")
        End If

        If Not IsPostBack Then
            ViewState.Add("SortCol", "ModelYear")
            ViewState.Add("SortDir", Sort.SortDirection.ASC)
            BindDataGrid()
            If dtgModelYear.Items.Count > 0 Then
                btnChoose.Disabled = False
            Else
                btnChoose.Disabled = True
                MessageBox.Show("Data tidak ditemukan")
            End If
        End If
    End Sub

    Private Sub BindDataGrid()
        If _vechileTypeGeneralID = String.Empty Then Exit Sub
        dtgModelYear.DataSource = New VechileColorIsActiveOnPKFacade(User).RetrieveByVechileTypeGeneralID(_vechileTypeGeneralID.Trim)
        dtgModelYear.DataBind()
    End Sub

    Private Sub dtgModelYear_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgModelYear.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgModelYear.CurrentPageIndex * dtgModelYear.PageSize)
        End If
    End Sub

#End Region

    Private Sub dtgModelYear_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgModelYear.SortCommand
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

    Private Sub dtgModelYear_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgModelYear.PageIndexChanged
        dtgModelYear.CurrentPageIndex = e.NewPageIndex
        BindDataGrid()
    End Sub

End Class
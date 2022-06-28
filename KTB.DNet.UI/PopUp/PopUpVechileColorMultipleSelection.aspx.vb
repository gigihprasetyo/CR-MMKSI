#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility

#End Region


Public Class PopUpVechileColorMultipleSelection
    Inherits System.Web.UI.Page

#Region " custom Declaration "
    Dim _vechileTypeCode As String = ""
    Dim _vechileSubCategoryID As Integer = 0
    Dim _vechileCategory As String = ""
#End Region


#Region " Custom Method "

    Private Sub ClearData()
        Me.txtMaterialNumber.Text = String.Empty
        Me.txtDeskripsi.Text = String.Empty
    End Sub

#End Region

#Region " Event Hendler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsNothing(Request.QueryString("vechileCategory")) AndAlso Request.QueryString("vechileCategory") <> "-1" Then
            _vechileCategory = Request.QueryString("vechileCategory")
        End If

        If Not IsNothing(Request.QueryString("vechileTypeCode")) AndAlso Request.QueryString("vechileTypeCode") <> "" Then
            _vechileTypeCode = Request.QueryString("vechileTypeCode")
            If Not IsNothing(Request.QueryString("vechileSubCategoryID")) AndAlso Request.QueryString("vechileSubCategoryID") <> "-1" Then
                _vechileSubCategoryID = Request.QueryString("vechileSubCategoryID")
            End If
        End If
        If Not IsPostBack Then
            ViewState.Add("SortCol", "MaterialNumber")
            ViewState.Add("SortDir", Sort.SortDirection.ASC)

            btnSearch_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(_vechileTypeCode)
        If dtgMaterialSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub BindDataGrid(ByVal vechileTypeCode As String)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(VechileColor), "Status", MatchType.No, "X"))
        If vechileTypeCode.Trim <> "-1" Then
            criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.VechileTypeCode", MatchType.InSet, "('" & vechileTypeCode.Replace(";", "','") & "')"))
        Else
            If _vechileSubCategoryID <> 0 Then
                Dim strSql As String = String.Format("" & vbCrLf &
                "SELECT VechileType.ID FROM VechileColor with (nolock) " & vbCrLf &
                "INNER JOIN VechileType ON VechileColor.VechileTypeID=VechileType.ID " & vbCrLf &
                "INNER JOIN Category ON VechileType.CategoryID = Category.ID " & vbCrLf &
                "INNER JOIN VechileModel ON VechileType.ModelID = VechileModel.ID " & vbCrLf &
                "WHERE VechileType.RowStatus = 0 " & vbCrLf &
                "AND VechileType.Status <> 'X' " & vbCrLf &
                "AND VechileColor.Status <> 'X' " & vbCrLf &
                "AND Category.ID = {1} " & vbCrLf &
                "AND VechileModel.ID in (select VechileModelID from [SubCategoryVehicleToModel] " & vbCrLf &
                "where RowStatus = 0 " & vbCrLf &
                "and SubCategoryVehicleID = {0} )", _vechileSubCategoryID, _vechileCategory)
                criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.ID", MatchType.InSet, "(" & strSql & ")"))
            End If
        End If

        If txtMaterialNumber.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(VechileColor), "MaterialNumber", MatchType.Exact, txtMaterialNumber.Text.Trim))
        End If
        If txtDeskripsi.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(VechileColor), "MaterialDescription", MatchType.[Partial], txtDeskripsi.Text.Trim))
        End If
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(VechileColor), ViewState("SortCol").ToString(), [Enum].Parse(GetType(Sort.SortDirection), ViewState("SortDir").ToString())))

        dtgMaterialSelection.DataSource = New VechileColorFacade(User).Retrieve(criterias, sortColl)
        dtgMaterialSelection.DataBind()
    End Sub

    Private Sub dtgMaterialSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMaterialSelection.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgMaterialSelection.CurrentPageIndex * dtgMaterialSelection.PageSize)
        End If
    End Sub

#End Region

    Private Sub dtgMaterialSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMaterialSelection.SortCommand
        If e.SortExpression = ViewState("SortColDealer") Then
            If ViewState("SortDirDealer") = Sort.SortDirection.ASC Then
                ViewState.Add("SortDirDealer", Sort.SortDirection.DESC)
            Else
                ViewState.Add("SortDirDealer", Sort.SortDirection.ASC)
            End If
        End If
        ViewState.Add("SortColDealer", e.SortExpression)
        BindDataGrid(_vechileTypeCode)
    End Sub

    Private Sub dtgMaterialSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMaterialSelection.PageIndexChanged
        dtgMaterialSelection.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(_vechileTypeCode)
    End Sub

End Class
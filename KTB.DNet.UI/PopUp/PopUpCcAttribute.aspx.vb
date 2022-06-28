#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.CallCenter
Imports KTB.DNet.Utility
#End Region

Public Class PopUpCcAttribute
    Inherits System.Web.UI.Page


    Private ReadOnly Property CcCustomerCategoryID() As Integer
        Get
            Return CInt(Page.Request.QueryString("CustomerCategoryID"))
        End Get

    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindDdlVehicleCategory()
            BindDdlFactor()
        End If
    End Sub

    Private Sub BindDdlVehicleCategory()
        ddlVehicleCategory.Items.Clear()

        Dim arlCategory As ArrayList = New CcVehicleCategoryFacade(User).RetrieveActiveList()

        For Each Category As CcVehicleCategory In arlCategory
            ddlVehicleCategory.Items.Add(New ListItem(Category.Code, Category.ID))
        Next
    End Sub

    Private Sub BindDdlFactor()
        ddlFactor.Items.Clear()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcFactor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CcFactor), "Status", MatchType.Exact, 1))
        criterias.opAnd(New Criteria(GetType(CcFactor), "CcVehicleCategory.ID", MatchType.Exact, ddlVehicleCategory.SelectedValue))
        criterias.opAnd(New Criteria(GetType(CcFactor), "CcCustomerCategory.ID", MatchType.Exact, CcCustomerCategoryID))

        Dim arlResult As ArrayList = New CcFactorFacade(User).Retrieve(criterias)

        For Each factor As CcFactor In arlResult
            ddlFactor.Items.Add(New ListItem(factor.Description, factor.ID))
        Next

        ddlFactor.Items.Insert(0, New ListItem("SEMUA", "-1"))

    End Sub

    Private Sub ddlVehicleCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVehicleCategory.SelectedIndexChanged
        BindDdlFactor()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            dtgAttribute.CurrentPageIndex = 0
            BindDataGrid(dtgAttribute.CurrentPageIndex)
        Catch ex As Exception
            '  MessageBox.Show("Terjadi kesalahan dalam pencarian Atribut : " & ex.Message)
        End Try
    End Sub


    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            'Dim arlAtribut As ArrayList = New CcAttributeFacade(User).RetrieveByCriteria(GetSearchCriteria(), indexPage + 1, dtgAttribute.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            Dim arlAtribut As ArrayList = New CcAttributeFacade(User).Retrieve(GetSearchCriteria())
            dtgAttribute.DataSource = arlAtribut
            '  dtgAttribute.VirtualItemCount = totalRow
            dtgAttribute.DataBind()
        End If

        If dtgAttribute.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Function GetSearchCriteria() As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcAttribute), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ddlFactor.SelectedValue = "-1" Then
            criterias.opAnd(New Criteria(GetType(CcAttribute), "CcFactor.CcVehicleCategory.ID", MatchType.Exact, ddlVehicleCategory.SelectedValue))
            criterias.opAnd(New Criteria(GetType(CcAttribute), "CcFactor.CcCustomerCategory.ID", MatchType.Exact, CcCustomerCategoryID))
        Else
            criterias.opAnd(New Criteria(GetType(CcAttribute), "CcFactor.ID", MatchType.Exact, ddlFactor.SelectedValue))
        End If

        Return criterias
    End Function

    Private Sub dtgAttribute_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgAttribute.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim atributDat As CcAttribute = e.Item.DataItem
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim lblFactor As Label = CType(e.Item.FindControl("lblFactor"), Label)
                Dim lblVehicleCategory As Label = CType(e.Item.FindControl("lblVehicleCategory"), Label)

                lblFactor.Text = atributDat.CcFactor.Description
                lblVehicleCategory.Text = atributDat.CcFactor.CcVehicleCategory.Description

            End If
        End If
    End Sub
End Class
#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
#End Region

Public Class PopUpVechileType
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtVechileTypeCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgVechileType As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlModel As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private paramCategoryId As Integer = 0

    Private Sub ClearData()
        Me.txtVechileTypeCode.Text = String.Empty
        Me.txtDescription.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "VechileTypeCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        dtgVechileType.DataSource = New VechileTypeFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, _
            dtgVechileType.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgVechileType.VirtualItemCount = totalRow
        dtgVechileType.DataBind()

        If totalRow > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Public Function CriteriaSearch() As CriteriaComposite

        If paramCategoryId <> 0 Then
            ddlCategory.SelectedValue = paramCategoryId
            ddlCategory.Enabled = False
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtVechileTypeCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.[Partial], txtVechileTypeCode.Text))
        End If
        If txtDescription.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(VechileType), "Description", MatchType.[Partial], txtDescription.Text))
        End If
        If ddlCategory.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
        End If
        'If ddlModel.SelectedValue > 0 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "VechileModel.ID", MatchType.Exact, ddlModel.SelectedValue))
        'End If
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        If companyCode.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(VechileType), "ProductCategory.Code", MatchType.Exact, companyCode.ToUpper.Trim))
        End If

        If Not IsNothing(Request.QueryString("IsActive")) Then
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, Request.QueryString("IsActive")))
        End If

        If Not IsDBNull(paramCategoryId) AndAlso paramCategoryId > 0 Then
            criterias.opAnd(New Criteria(GetType(VechileType), "Category.ID", MatchType.Exact, paramCategoryId))
        End If

        If ddlCategory.SelectedValue <> -1 And ddlModel.SelectedValue <> -1 Then
            'Dim Sql As String = ""
            'Dim oEVSC As EnumVehicleSubCategory = New EnumVehicleSubCategory
            'Dim sVals As String = oEVSC.GetSQLValue(ddlCategory.SelectedItem.Text, ddlModel.SelectedValue)

            'Sql &= " select distinct(ID) from VechileType "
            'Sql &= " where RowStatus = 0 "
            'Dim i As Integer
            'For i = 0 To sVals.Split(";").Length - 1
            '    If i = 0 Then
            '        Sql &= " and (Description like '" & sVals.Split(";")(i) & "' "
            '        If sVals.Split(";").Length = 1 Then Sql &= ")"
            '    ElseIf i = sVals.Split(";").Length - 1 Then
            '        Sql &= " or Description like '" & sVals.Split(";")(i) & "') "
            '    Else
            '        Sql &= " or Description like '" & sVals.Split(";")(i) & "'"
            '    End If
            'Next
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "ID", MatchType.InSet, "(" & Sql & ")"))

            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlModel.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))

        End If
        Return criterias
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strCategoryID As String = Request.QueryString("categoryID")
        If Not IsNothing(strCategoryID) And strCategoryID.Trim <> "" Then
            paramCategoryId = strCategoryID
        End If


        Dim strCategoryID2 As String = Request.QueryString("CategoryID")
        If Not IsNothing(strCategoryID2) And strCategoryID2.Trim <> "" Then
            paramCategoryId = strCategoryID2
        End If

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
            BindDdlCategory()
            BindDdlSubCategory()
        End If
    End Sub

    Private Sub BindDdlSubCategory()
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlModel, ddlCategory.SelectedItem.Text)
    End Sub


    'Private Sub BindDdlModel()
    '    Dim listItemBlank As New ListItem("Silahkan Pilih", -1)
    '    Dim criteria As CriteriaComposite
    '    criteria = New CriteriaComposite(New Criteria(GetType(VechileModel), "Category.ID", MatchType.Exact, CType(Me.ddlCategory.SelectedValue, Integer)))
    '    ddlModel.DataSource = New VechileModelFacade(User).RetrieveList("Description", Sort.SortDirection.ASC, criteria)
    '    ddlModel.DataValueField = "ID"
    '    ddlModel.DataTextField = "Description"
    '    ddlModel.DataBind()
    '    ddlModel.Items.Insert(0, listItemBlank)
    'End Sub

    ''--Binding Data Down List Category
    Private Sub BindDdlCategory()
        Dim listItemBlank As New ListItem("Silahkan Pilih", -1)
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveActiveList("MMC") 'New CategoryFacade(User).RetrieveList("Description", Sort.SortDirection.ASC)
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, listItemBlank)

        If paramCategoryId > 0 AndAlso Not IsNothing(Request.QueryString("Filtered")) AndAlso Request.QueryString("Filtered").ToString() = "1" Then
            ddlCategory.SelectedValue = paramCategoryId.ToString()
            ddlCategory.Enabled = False
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)

    End Sub

    Private Sub dtgVechileType_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgVechileType.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectDealer"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If

    End Sub

    Private Sub dtgVechileType_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgVechileType.PageIndexChanged
        dtgVechileType.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgVechileType.CurrentPageIndex)
    End Sub

    Private Sub dtgVechileType_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgVechileType.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If
        dtgVechileType.CurrentPageIndex = 0
        BindDataGrid(dtgVechileType.CurrentPageIndex)
    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        If ddlCategory.SelectedIndex > 0 Then
            BindDdlSubCategory()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub ddlModel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlModel.SelectedIndexChanged
        BindDataGrid(0)
    End Sub
End Class
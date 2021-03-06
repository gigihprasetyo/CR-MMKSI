Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.AlertManagement

Public Class FrmAlertManagemenList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgAlerts As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents chkCreatedDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icStartDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlAlertCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAlertModul As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private sessHelper As New SessionHelper
    Private objAlert As AlertMaster = New AlertMaster
    Private objAlertFacade As AlertMasterFacade = New AlertMasterFacade(User)

#Region "Private Property"
    Private Property SesType() As EnumAlertManagement.AlertManagementType
        Get
            Return CType(sessHelper.GetSession("ListAlertMasterType"), EnumAlertManagement.AlertManagementType)
        End Get
        Set(ByVal Value As EnumAlertManagement.AlertManagementType)
            sessHelper.SetSession("ListAlertMasterType", Value)
        End Set
    End Property
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.AlertManagementListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=ALERT MANAGEMENT - Alert Managemen List")
        End If
    End Sub

    Dim bCekDetailPriv As Boolean = SecurityProvider.Authorize(context.User, SR.AlertManagementListView_Privilege)
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        InitiateAuthorization()
        If Not IsPostBack Then
            InitDisplayAlertCategory()
            ViewState("currentSortColumn") = "Name"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            'ViewState("currentSortDirection") = Sort.SortDirection.DESC

            If Request.QueryString("Type") = "T" Then
                SesType = EnumAlertManagement.AlertManagementType.Transactional
            ElseIf Request.QueryString("Type") = "A" Then
                SesType = EnumAlertManagement.AlertManagementType.Announcement
            End If

            dgAlerts.CurrentPageIndex = 0
            BindDataGrid(dgAlerts.CurrentPageIndex)
        End If
    End Sub

    Private Sub InitDisplayAlertCategory()
        Dim facade As New AlertCategoryFacade(User)
        Dim arlAlertCategory As ArrayList = facade.RetrieveActiveList()

        ddlAlertCategory.Items.Clear()
        ddlAlertCategory.Items.Add(New ListItem("Semua", 0))

        For Each cat As AlertCategory In arlAlertCategory
            ddlAlertCategory.Items.Add(New ListItem(cat.Description, cat.ID))
        Next
        ddlAlertCategory.SelectedIndex = 0
        RebindModulDropdownList()
    End Sub

    Private Sub RebindModulDropdownList()
        Dim categoryId As Integer = CInt(ddlAlertCategory.SelectedValue)
        Dim arlModul As ArrayList = New AlertModulFacade(User).RetrieveActiveListByCategoryID(categoryId)

        ddlAlertModul.Items.Clear()
        ddlAlertModul.Items.Add(New ListItem("Semua", 0))
        For Each modul As AlertModul In arlModul
            ddlAlertModul.Items.Add(New ListItem(modul.Description, modul.ID))
        Next
    End Sub

    Private Sub BindDataGrid(ByVal index As Integer)
        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AlertMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(AlertMaster), "AlertType", MatchType.Exact, CType(SesType, Short)))
        If txtName.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(AlertMaster), "Name", MatchType.[Partial], txtName.Text.Trim))
        End If
        If ddlAlertCategory.SelectedValue <> 0 Then
            criterias.opAnd(New Criteria(GetType(AlertMaster), "AlertModul.AlertCategory.ID", MatchType.Exact, ddlAlertCategory.SelectedValue))
        End If
        If ddlAlertModul.SelectedValue <> 0 Then
            criterias.opAnd(New Criteria(GetType(AlertMaster), "AlertModul.ID", MatchType.Exact, ddlAlertModul.SelectedValue))
        End If
        If chkCreatedDate.Checked Then
            Dim tglDibuatAwal As New DateTime(CInt(icStartDate.Value.Year), CInt(icStartDate.Value.Month), CInt(icStartDate.Value.Day), 0, 0, 0)
            Dim tglDibuatAkhir As New DateTime(CInt(icEndDate.Value.Year), CInt(icEndDate.Value.Month), CInt(icEndDate.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(AlertMaster), "CreatedTime", MatchType.GreaterOrEqual, Format(tglDibuatAwal, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(AlertMaster), "CreatedTime", MatchType.LesserOrEqual, Format(tglDibuatAkhir, "yyyy-MM-dd HH:mm:ss")))
        End If

        _arrList = New AlertMasterFacade(User).RetrieveActiveList(criterias, index + 1, dgAlerts.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        dgAlerts.DataSource = _arrList
        dgAlerts.VirtualItemCount = totalRow
        dgAlerts.DataBind()
    End Sub
    Private Sub dgAlerts_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgAlerts.ItemCommand
        Select Case e.CommandName
            Case "View"
                Response.Redirect("~/AlertManagement/FrmTransactionalAlert.aspx?Mode=View;" & Request.QueryString("Type") & "&id=" & CInt(e.CommandArgument))
            Case "Edit"
                Response.Redirect("~/AlertManagement/FrmTransactionalAlert.aspx?Mode=Edit;" & Request.QueryString("Type") & "&id=" & CInt(e.CommandArgument))
            Case "Delete"
                objAlert = objAlertFacade.Retrieve(CInt(e.CommandArgument))
                objAlert.RowStatus = CType(DBRowStatus.Deleted, Short)
                objAlertFacade.Delete(objAlert)
                BindDataGrid(dgAlerts.CurrentPageIndex)
                'Case "AlertSound"
                'Response.Write("<script language='javascript'></script>")
                'Response.Write("<script language='javascript'>showPopUp('../PopUp/PopUpAlertSound.aspx?id='" + CInt(e.CommandArgument) + ",'',500,760,'');</script>")
        End Select
    End Sub

    Private Sub dgAlerts_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgAlerts.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim objAlert2 As AlertMaster = CType(e.Item.DataItem, AlertMaster)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType.AlternatingItem Then

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgAlerts.CurrentPageIndex * dgAlerts.PageSize)
                Dim lnkbtnPopUp As LinkButton = CType(e.Item.FindControl("lnkbtnPopUp"), LinkButton)
                lnkbtnPopUp.Attributes("OnClick") = "showPopUp('../PopUp/PopUpAlertSound.aspx?ID=" & objAlert2.ID & "','',500,760,'');"


                Dim lblCategory As Label = CType(e.Item.FindControl("lblCategory"), Label)
                lblCategory.Text = objAlert2.AlertModul.AlertCategory.Description
                Dim lblModul As Label = CType(e.Item.FindControl("lblModul"), Label)
                lblModul.Text = objAlert2.AlertModul.Description

                Dim lnkbtnView As LinkButton = CType(e.Item.FindControl("lnkbtnView"), LinkButton)
                Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
                Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
                'add privilige
                lnkbtnPopUp.Visible = bCekDetailPriv
                lnkbtnView.Visible = bCekDetailPriv
                lnkbtnEdit.Visible = bCekDetailPriv
                lnkbtnDelete.Visible = bCekDetailPriv
            End If
        End If
    End Sub

    Private Sub dgAlerts_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgAlerts.PageIndexChanged
        dgAlerts.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgAlerts.CurrentPageIndex)
    End Sub

    Private Sub dgAlerts_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgAlerts.SortCommand
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

        dgAlerts.SelectedIndex = -1
        dgAlerts.CurrentPageIndex = 0
        BindDataGrid(dgAlerts.CurrentPageIndex)
    End Sub


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgAlerts.CurrentPageIndex = 0
        BindDataGrid(dgAlerts.CurrentPageIndex)
    End Sub

    Private Sub ddlAlertCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAlertCategory.SelectedIndexChanged
        RebindModulDropdownList()
    End Sub
End Class

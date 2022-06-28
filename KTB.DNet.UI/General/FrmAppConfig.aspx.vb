#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security

#End Region

Public Class FrmAppConfig
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents txtValue As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents DDAppID As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgAppConfig As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private _sessHelper As SessionHelper = New SessionHelper
    Private bPrivilegeChangesCity As Boolean
    Private arlAppConfig As ArrayList
    Private sessCriterias As String = "FrmAppConfig.sessCriterias"


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here


        ActivateUserPrivilege()
 
        If Not IsPostBack Then
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            InitiatePage()
            BindToDropdownList()
            btnSimpan.Enabled = False
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(AppConfig), "Name", MatchType.No, SR.PODateAllowed))
        CreateCriteria(criterias)
        _sessHelper.SetSession(Me.sessCriterias, criterias)
        dtgAppConfig.CurrentPageIndex = 0
        BindDatagrid(dtgAppConfig.CurrentPageIndex)
        btnSimpan.Enabled = True
    End Sub

   

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim ObjAppConfig As AppConfig = New AppConfig
        Dim objAppConfigFacade As AppConfigFacade = New AppConfigFacade(User)
        Dim nResult As Integer = -1
        Select Case CType(ViewState("vsProcess"), String)
            Case "Insert"
                ObjAppConfig.Name = txtName.Text.Trim()
                If objAppConfigFacade.ValidateCode(ObjAppConfig) And txtName.Text.Trim().ToLower() <> "podateallowed" Then
                    ObjAppConfig.Name = txtName.Text.Trim()
                    ObjAppConfig.Value = txtValue.Text.Trim()
                    ObjAppConfig.Status = CType(ddlStatus.SelectedValue, Short)
                    ObjAppConfig.AppID = DDAppID.SelectedValue.ToString()
                    nResult = New AppConfigFacade(User).Insert(ObjAppConfig)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("App Name"))
                End If
            Case "Edit"
                If txtName.Text.Trim().ToLower() <> "podateallowed" Then
                nResult = UpdateAppConfig()
                If nResult = -1 Then
                    MessageBox.Show(SR.UpdateFail)
                ElseIf nResult = -2 Then
                    MessageBox.Show(SR.DataIsExist("App Name"))
                Else
                    ClearData()
                End If
                Else
                    MessageBox.Show(SR.DataIsExist("App Name"))
                End If

        End Select

        dtgAppConfig.CurrentPageIndex = 0
        BindDatagrid(dtgAppConfig.CurrentPageIndex)
    End Sub

    Private Sub dtgAppConfig_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgAppConfig.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then

            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")

            Dim RowValue As AppConfig = CType(e.Item.DataItem, AppConfig)

            If RowValue.Status = 0 Then
                CType(e.Item.FindControl("LblStatus"), Label).Text = "Aktif"

            Else
                CType(e.Item.FindControl("LblStatus"), Label).Text = "Pasif"
            End If

            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgAppConfig.CurrentPageIndex * dtgAppConfig.PageSize)
            End If
        End If

    End Sub

    Private Sub dtgAppConfig_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgAppConfig.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            dtgAppConfig.SelectedIndex = e.Item.ItemIndex
            ViewAppConfig(e.Item.Cells(0).Text, False)
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewAppConfig(e.Item.Cells(0).Text, True)
            btnSearch.Enabled = False
            dtgAppConfig.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteAppConfig(e.Item.Cells(0).Text)

            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try


        End If
    End Sub

    Private Sub dtgAppConfig_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgAppConfig.PageIndexChanged
        dtgAppConfig.SelectedIndex = -1
        dtgAppConfig.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgAppConfig.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgAppConfig_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgAppConfig.SortCommand
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

        dtgAppConfig.SelectedIndex = -1
        dtgAppConfig.CurrentPageIndex = 0
        BindDatagrid(dtgAppConfig.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        'ViewState.Clear()
        'Response.Redirect("../Default.aspx")
        ViewState.Clear()
        'SessionHelper.RemoveAll()
        Response.Redirect("../default.aspx")
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

#Region "Custom Method"

    Private Sub ActivateUserPrivilege()
       
        If Not SetChecklistMaintenance() Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=AppConfig")
        End If

    End Sub


    Private Sub BindToDropdownList()
        '--DropdopwnList Status
        ddlStatus.DataSource = enumStatusBuletin.RetrieveStatus()
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataBind()
        ddlStatus.SelectedIndex = -1
    End Sub


    Private Function SetChecklistMaintenance() As Boolean
        Dim objUI As UserInfo = _sessHelper.GetSession("LOGINUSERINFO")
        Dim strTemp As String = ""
        Dim arrUser() As String
        Dim arrProp() As String
        Dim i As Integer = 0
        Dim IsAdmin As Boolean = False

        _sessHelper.SetSession("Checklist.SMSNumber", "")
        _sessHelper.SetSession("Checklist.EmailAddress", "")

        strTemp = KTB.DNet.Lib.WebConfig.GetValue("Checklist.UserName")
        arrUser = strTemp.Split(";")
        For i = 0 To arrUser.Length - 1
            arrProp = arrUser(i).Split("#")
            If arrProp(0).ToUpper = objUI.UserName.ToUpper And arrProp(1).ToUpper = objUI.Dealer.DealerCode.ToUpper Then
                _sessHelper.SetSession("Checklist.SMSNumber", arrProp(2))
                _sessHelper.SetSession("Checklist.EmailAddress", arrProp(3))
                IsAdmin = True
                Exit For
            End If
        Next
        Return IsAdmin
    End Function


    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = "Name"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub ClearData()
        txtName.Text() = String.Empty
        txtValue.Text() = String.Empty
        ddlStatus.SelectedValue = enumStatusBuletin.StatusBuletin.Aktif
        btnSimpan.Enabled = True
        DDAppID.SelectedValue = ""
        dtgAppConfig.SelectedIndex = -1
        btnSearch.Enabled = True
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)

        If txtName.Text().Trim().Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "Name", MatchType.Partial, txtName.Text().Trim()))
        End If
        If txtValue.Text().Trim().Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "Value", MatchType.Partial, txtValue.Text().Trim()))
        End If
        If DDAppID.SelectedValue().ToString().Trim().Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "AppID", MatchType.Partial, DDAppID.SelectedValue().ToString()))
        End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "Status", MatchType.Exact, ddlStatus.SelectedValue))

    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim TotalRow As Integer = 0
        If indexPage >= 0 Then
            arlAppConfig = New AppConfigFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession(Me.sessCriterias), CriteriaComposite), indexPage + 1, dtgAppConfig.PageSize, TotalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgAppConfig.DataSource = arlAppConfig
            dtgAppConfig.VirtualItemCount = TotalRow
            dtgAppConfig.DataBind()
        End If
    End Sub

    Private Sub ViewAppConfig(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objAppConfig As AppConfig = New AppConfigFacade(User).Retrieve(nID)
        If Not objAppConfig Is Nothing Then
            _sessHelper.SetSession("vsAppConfig", objAppConfig)
            'ViewState.Add("vsCity", objCity)
            txtName.Text = objAppConfig.Name.ToString()
            txtValue.Text = objAppConfig.Value.ToString()
            ddlStatus.SelectedValue = objAppConfig.Status()
            DDAppID.SelectedValue = objAppConfig.AppID()
            Me.btnSimpan.Enabled = EditStatus
        Else
            MessageBox.Show(SR.ViewFail)
        End If
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, ByVal AppConfigID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "ID", MatchType.Exact, AppConfigID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Sub DeleteAppConfig(ByVal nID As Integer)


        Dim objAppConfig As AppConfig = New AppConfigFacade(User).Retrieve(nID)
        'objCity.RowStatus = DBRowStatus.Deleted
        If Not objAppConfig Is Nothing Then
            Dim objAppConfigFacade As AppConfigFacade = New AppConfigFacade(User)
            objAppConfigFacade.DeleteFromDB(objAppConfig)

            dtgAppConfig.CurrentPageIndex = 0
            BindDatagrid(dtgAppConfig.CurrentPageIndex)
        Else
            MessageBox.Show(SR.DeleteFail)
        End If



    End Sub

    Private Function UpdateAppConfig() As Integer
        Dim objAppConfig As AppConfig = CType(Session.Item("vsAppConfig"), AppConfig)
        objAppConfig.Value = txtValue.Text.Trim()
        objAppConfig.Status = CType(ddlStatus.SelectedValue, Short)
        objAppConfig.Name = txtName.Text.Trim()
        objAppConfig.AppID = DDAppID.SelectedValue.ToString()
        Dim objAppConfigFacade As AppConfigFacade = New AppConfigFacade(User)

 
        If Not objAppConfigFacade.ValidateCode(objAppConfig) Then
            Return -2
        End If
        Try
            Return objAppConfigFacade.Update(objAppConfig)
        Catch ex As Exception
            Return -1
        End Try
    End Function

#End Region

End Class

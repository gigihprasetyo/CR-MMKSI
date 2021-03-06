Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security


Public Class FrmCity
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents dtgCity As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtCityName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCityCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlProvince As System.Web.UI.WebControls.DropDownList
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Private _sessHelper As SessionHelper = New SessionHelper
    Private bPrivilegeChangesCity As Boolean
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindDdlProvince()
            InitiatePage()
            btnSimpan.Enabled = False
        End If
        'Put user code to initialize the page here
    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = bPrivilegeChangesCity
        btnBatal.Visible = bPrivilegeChangesCity
    End Sub

    Private Sub ActivateUserPrivilege()
        bPrivilegeChangesCity = SecurityProvider.Authorize(Context.User, SR.ChangeCity_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.ViewCity_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=GENERAL - Kota")
        End If

    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "Province"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDdlProvince()
        ddlProvince.DataSource = New ProvinceFacade(User).RetrieveList("ProvinceName", Sort.SortDirection.ASC)
        ddlProvince.DataTextField = "ProvinceName"
        ddlProvince.DataValueField = "ID"
        ddlProvince.DataBind()
        ddlProvince.Items.Insert(0, New ListItem("", ""))
    End Sub
    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            

            dtgCity.DataSource = New CityFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession("CRITERIAS"), CriteriaComposite), indexPage + 1, _
                    dtgCity.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgCity.VirtualItemCount = totalRow
            dtgCity.DataBind()
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objCity As City = New City
        Dim objCityFacade As CityFacade = New CityFacade(User)
        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtCityCode.Text = String.Empty Then
                If objCityFacade.ValidateCode(txtCityCode.Text) <= 0 Then
                    objCity.Province = New ProvinceFacade(User).Retrieve(CType(ddlProvince.SelectedValue, Integer))
                    objCity.CityCode = txtCityCode.Text
                    objCity.CityName = txtCityName.Text
                    objCity.Status = "A"
                    nResult = New CityFacade(User).Insert(objCity)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Kode Kota"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode Kota"))
            End If
        Else
            nResult = UpdateCity()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                ClearData()
            End If
        End If

        dtgCity.CurrentPageIndex = 0
        BindDatagrid(dtgCity.CurrentPageIndex)
    End Sub

    Private Function UpdateCity() As Integer
        Dim objCity As City = CType(Session.Item("vsCity"), City)
        objCity.Province = New ProvinceFacade(User).Retrieve(CType(ddlProvince.SelectedValue, Integer))
        objCity.CityName = txtCityName.Text
        Try
            Return New CityFacade(User).Update(objCity)
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Private Sub dtgCity_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCity.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As City = CType(e.Item.DataItem, City)
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
            CType(e.Item.FindControl("linkButonActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan diAktifkan?');")
            CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan diNonAktifkan?');")
            Dim actLb As LinkButton = CType(e.Item.FindControl("linkButonActive"), LinkButton)
            Dim nactLb As LinkButton = CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton)

            'tambahan Privilege
            ActivateUserPrivilege()
            If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = bPrivilegeChangesCity
            End If

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = bPrivilegeChangesCity
            End If

            If Not e.Item.FindControl("linkButonActive") Is Nothing Then
                CType(e.Item.FindControl("linkButonActive"), LinkButton).Visible = bPrivilegeChangesCity
            End If

            If Not e.Item.FindControl("LinkButtonNonActive") Is Nothing Then
                CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton).Visible = bPrivilegeChangesCity
            End If

            If RowValue.Status.ToUpper = "X" Then
                actLb.Visible = bPrivilegeChangesCity
                nactLb.Visible = False
            Else
                actLb.Visible = False
                nactLb.Visible = bPrivilegeChangesCity
            End If

        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgCity.CurrentPageIndex * dtgCity.PageSize)
        End If
    End Sub
    Private Sub dtgCity_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgCity.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtCityCode.ReadOnly = True
            txtCityName.ReadOnly = True
            ddlProvince.Enabled = False
            ViewCity(e.Item.Cells(0).Text, False)
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewCity(e.Item.Cells(0).Text, True)
            dtgCity.SelectedIndex = e.Item.ItemIndex
            ddlProvince.Enabled = True
            txtCityCode.ReadOnly = True
            txtCityName.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteCity(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
        ElseIf e.CommandName = "Activate" Then
            Activate(e.Item.Cells(0).Text)
            ddlProvince.Enabled = True
            txtCityCode.ReadOnly = False
            txtCityName.ReadOnly = False
            ClearData()
        ElseIf e.CommandName = "Deactivate" Then
            DeActivate(e.Item.Cells(0).Text)
            ddlProvince.Enabled = True
            txtCityCode.ReadOnly = False
            txtCityName.ReadOnly = False
            ClearData()
        End If
    End Sub

    Private Sub Activate(ByVal nID As Integer)
        Dim objCity As City = New CityFacade(User).Retrieve(nID)
        Dim facade As CityFacade = New CityFacade(User)
        objCity.Status = "A"
        facade.Update(objCity)
        dtgCity.CurrentPageIndex = 0
        BindDatagrid(dtgCity.CurrentPageIndex)
    End Sub

    Private Sub DeActivate(ByVal nID As Integer)
        Dim objCity As City = New CityFacade(User).Retrieve(nID)
        Dim facade As CityFacade = New CityFacade(User)
        objCity.Status = "X"
        facade.Update(objCity)
        dtgCity.CurrentPageIndex = 0
        BindDatagrid(dtgCity.CurrentPageIndex)
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal CityID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "City", MatchType.Exact, CityID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Sub DeleteCity(ByVal nID As Integer)
        'Dim ArryList As ArrayList = New ArrayList
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "City.ID", MatchType.Exact, nID))
        'ArryList = New DealerFacade(User).Retrieve(criterias)

        If New HelperFacade(User, GetType(Dealer)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(Dealer), nID), _
            CreateAggreateForCheckRecord(GetType(Dealer))) Then

            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objCity As City = New CityFacade(User).Retrieve(nID)
            'objCity.RowStatus = DBRowStatus.Deleted
            If Not objCity Is Nothing Then
                Dim nResult = New CityFacade(User).DeleteFromDB(objCity)
            Else
                MessageBox.Show(SR.DeleteFail)
            End If

            dtgCity.CurrentPageIndex = 0
            BindDatagrid(dtgCity.CurrentPageIndex)
        End If
    End Sub

    Private Sub ViewCity(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objCity As City = New CityFacade(User).Retrieve(nID)
        If Not objCity Is Nothing Then
            _sessHelper.SetSession("vsCity", objCity)
            'ViewState.Add("vsCity", objCity)
            If IsNothing(objCity.Province) Then
                ddlProvince.SelectedValue = ""
            Else
                ddlProvince.SelectedValue = CType(objCity.Province.ID, String)
            End If
            txtCityCode.Text = objCity.CityCode
            txtCityName.Text = objCity.CityName
            Me.btnSimpan.Enabled = EditStatus
        Else
            MessageBox.Show(SR.ViewFail)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        'ViewState.Clear()
        'Response.Redirect("../Default.aspx")
        ViewState.Clear()
        'SessionHelper.RemoveAll()
        Response.Redirect("../default.aspx")
    End Sub

    Private Sub ClearData()
        txtCityCode.Text() = String.Empty
        txtCityName.Text = String.Empty
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        ddlProvince.Enabled = True
        ddlProvince.SelectedValue = ""
        txtCityCode.ReadOnly = False
        txtCityName.ReadOnly = False
    End Sub
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub


    Private Sub dtgCity_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgCity.SelectedIndexChanged

    End Sub

    Private Sub dtgCity_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCity.PageIndexChanged
        dtgCity.SelectedIndex = -1
        dtgCity.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgCity.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgCity_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCity.SortCommand
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

        dtgCity.SelectedIndex = -1
        dtgCity.CurrentPageIndex = 0
        BindDatagrid(dtgCity.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        _sessHelper.SetSession("CRITERIAS", criterias)
        dtgCity.CurrentPageIndex = 0
        BindDatagrid(dtgCity.CurrentPageIndex)
        btnSimpan.Enabled = True
    End Sub
    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)

        If ddlProvince.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.City), "Province.ID", MatchType.Exact, ddlProvince.SelectedValue))
        End If
        If txtCityCode.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.City), "CityCode", MatchType.Exact, txtCityCode.Text.Trim))
        End If
        If txtCityName.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.City), "CityName", MatchType.[Partial], txtCityName.Text.Trim))
        End If
    End Sub
End Class


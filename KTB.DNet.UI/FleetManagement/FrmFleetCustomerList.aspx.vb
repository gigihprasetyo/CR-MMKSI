Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class DaftarFleetCustomer
    Inherits System.Web.UI.Page
    Private bPrivilegeChanges As Boolean
    Private _sessHelper As SessionHelper = New SessionHelper
    Private objFleetCustomer As FleetCustomer
    Private objFleetCustomerContact As FleetCustomerContact
    Private objFleetHasilSurveyHeader As FleetHasilSurveyHeader
    Private objFleetHasilSurveyDetail As FleetHasilSurveyDetail
    Private objFleetCustomerToDealer As FleetCustomerToDealer
    Private facFleetCustomer As New FleetCustomerFacade(User)
    Private facFleetCustomerContact As New FleetCustomerContactFacade(User)
    Private facFleetHasilSurveyHeader As New FleetHasilSurveyHeaderFacade(User)
    Private facFleetHasilSurveyDetail As New FleetHasilSurveyDetailFacade(User)
    Private facFleetCustomerToDealer As New FleetCustomerToDealerFacade(User)
    Private _input As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private strSessCurrentPageIndex As String = "CurrentPageIndexFleetCustomerList"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            BindDropDown()

            Dim criterias As CriteriaComposite = CType(_sessHelper.GetSession("Criterias"), CriteriaComposite)
            If Not IsNothing(criterias) Then
                dtgFleetCustomerList.CurrentPageIndex = _sessHelper.GetSession(strSessCurrentPageIndex)
                BindDatagrid(dtgFleetCustomerList.CurrentPageIndex)
            End If
        End If
    End Sub

#Region "function"
    Private Sub CheckPrivilege()
        Dim bFleetCustomerPrivilege As Boolean = SecurityProvider.Authorize(Context.User, SR.FleetCustomer_List_Privilege)
        If Not bFleetCustomerPrivilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FLEET MANAGEMENT - Fleet Customer List")
        End If

        If Request.QueryString("Mode") = "View" Then
            _input = False
            _edit = False
        Else
            _input = SecurityProvider.Authorize(Context.User, SR.FleetCustomer_Input_Privilege)
            _edit = SecurityProvider.Authorize(Context.User, SR.FleetCustomer_Edit_Privilege)
        End If

    End Sub

    Private Sub InitiatePage()
        ClearData()

        ViewState("CurrentSortColumn") = "Code"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub ClearData()
        txtCode.Text() = String.Empty
        txtName.Text = String.Empty
        txtGroupName.Text = String.Empty
        ddlProvince.SelectedIndex = 0
        ddlCity.SelectedIndex = 0
    End Sub

    Private Sub BindDropDown()
        'dropdown propinsi
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim list As ArrayList = New ProvinceFacade(User).RetrieveActiveList(criterias, "ProvinceName", Sort.SortDirection.ASC)

        ddlProvince.Items.Clear()
        ddlProvince.DataSource = list
        ddlProvince.DataTextField = "ProvinceName"
        ddlProvince.DataValueField = "ID"
        ddlProvince.DataBind()
        ddlProvince.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlProvince.SelectedIndex = 0
        ddlProvince_SelectedIndexChanged(Me, System.EventArgs.Empty)
    End Sub

    Private Sub ddlProvince_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlProvince.SelectedIndexChanged
        ddlCity.Items.Clear()
        If ddlProvince.SelectedIndex <> 0 Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(City), "Province.ID", MatchType.Exact, ddlProvince.SelectedValue))
            criterias.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))

            ddlCity.DataSource = New CityFacade(User).RetrieveActiveList(criterias, "CityName", Sort.SortDirection.ASC)
            ddlCity.DataTextField = "CityName".ToUpper
            ddlCity.DataValueField = "ID"
            ddlCity.DataBind()
        End If
        ddlCity.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlCity.SelectedIndex = 0

    End Sub
#End Region


    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FleetCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        _sessHelper.SetSession("Criterias", criterias)
        dtgFleetCustomerList.CurrentPageIndex = 0
        BindDatagrid(dtgFleetCustomerList.CurrentPageIndex)
    End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)

        If txtCode.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FleetCustomer), "Code", MatchType.[Partial], txtCode.Text.Trim))
        End If
        If txtName.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FleetCustomer), "Name", MatchType.[Partial], txtName.Text.Trim))
        End If
        If txtGroupName.Text.Length > 0 Then
            Dim crt As New CriteriaComposite(New Criteria(GetType(CustomerGroup), "Name", MatchType.Exact, txtGroupName.Text.Trim))
            crt.opAnd(New Criteria(GetType(CustomerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim customerGroups As ArrayList = New CustomerGroupFacade(User).Retrieve(crt)
            If customerGroups.Count > 0 Then
                Dim customerGroup As CustomerGroup = customerGroups(0)
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FleetCustomer), "CustomerGroup", MatchType.Exact, customerGroup.ID))
            End If

        End If

        If ddlProvince.SelectedValue <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FleetCustomer), "ProvinceID", MatchType.Exact, ddlProvince.SelectedValue))
        End If

        If ddlCity.SelectedValue <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FleetCustomer), "City", MatchType.Exact, ddlCity.SelectedValue))
        End If
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        If (indexPage >= 0) Then

            Dim criterias As CriteriaComposite = CType(_sessHelper.GetSession("Criterias"), CriteriaComposite)
            arrList = New FleetCustomerFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgFleetCustomerList.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgFleetCustomerList.DataSource = arrList
            dtgFleetCustomerList.VirtualItemCount = totalRow
            dtgFleetCustomerList.DataBind()
        End If

        _sessHelper.SetSession(strSessCurrentPageIndex, indexPage)
    End Sub

    Protected Sub dtgFleetCustomerList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgFleetCustomerList.ItemDataBound

        ' set no
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgFleetCustomerList.CurrentPageIndex * dtgFleetCustomerList.PageSize)
        End If

        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As FleetCustomer = CType(e.Item.DataItem, FleetCustomer)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then

                Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
                If Not IsNothing(RowValue.City) Then
                    lblCity.Text = RowValue.City.CityName
                End If

                Dim lblCustomerGroup As Label = CType(e.Item.FindControl("lblCustomerGroup"), Label)
                If Not IsNothing(RowValue.CustomerGroup) Then
                    lblCustomerGroup.Text = RowValue.CustomerGroup.Name
                End If

                'Dim lblNeedtobeMapped As Label = CType(e.Item.FindControl("lblNeedtobeMapped"), Label)
                'lblNeedtobeMapped.Text = GetDealerNeedtobeMapped(RowValue.ID)

                ' set privilege
                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                If Not IsNothing(lbtnEdit) Then
                    lbtnEdit.Visible = _edit
                End If

                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                If Not IsNothing(lbtnDelete) Then
                    lbtnDelete.Visible = _input
                End If

                Dim lbtnAssignToCustomer As LinkButton = CType(e.Item.FindControl("lbtnAssignToCustomer"), LinkButton)
                If Not IsNothing(lbtnAssignToCustomer) Then
                    lbtnAssignToCustomer.Visible = _input
                End If

                Dim lbtnAssignToDealer As LinkButton = CType(e.Item.FindControl("lbtnAssignToDealer"), LinkButton)
                If Not IsNothing(lbtnAssignToDealer) Then
                    lbtnAssignToDealer.Visible = _input
                End If


                'Dim lblScoreGrade As Label = e.Item.FindControl("lblScoreGrade")
                'lblScoreGrade.Text = (New DAGradeScoreFacade(User).RetrievePrevScoreGrade(RowValue.ID)).Code
            End If

            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If
    End Sub

    Private Function GetDealerNeedtobeMapped(ByVal fleetCustomerID As Integer) As String
        Dim str As String = String.Empty
        Dim customerAssignedList As New ArrayList
        Dim facFleetCustomertoCustomer As FleetCustomerToCustomerFacade = New FleetCustomerToCustomerFacade(User)
        Dim crt As CriteriaComposite

        crt = New CriteriaComposite(New Criteria(GetType(FleetCustomerToCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crt.opAnd(New Criteria(GetType(FleetCustomerToCustomer), "FleetCustomerID", MatchType.Exact, fleetCustomerID))
        customerAssignedList = facFleetCustomertoCustomer.Retrieve(crt)

        If customerAssignedList.Count > 0 Then
            For Each itemCustomer As FleetCustomerToCustomer In customerAssignedList
                ' get dealer that already assign
                Dim strDealer As String = "-1"
                Dim dealerAssignedList As New ArrayList
                crt = New CriteriaComposite(New Criteria(GetType(FleetCustomerToDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crt.opAnd(New Criteria(GetType(FleetCustomerToDealer), "FleetCustomerID", MatchType.Exact, fleetCustomerID))
                dealerAssignedList = facFleetCustomerToDealer.Retrieve(crt)
                If dealerAssignedList.Count > 0 Then
                    For Each itemDealer As FleetCustomerToDealer In dealerAssignedList
                        If strDealer = "-1" Then
                            strDealer = itemDealer.DealerID
                        Else
                            strDealer += "," & itemDealer.DealerID
                        End If
                    Next
                End If

                ' get dealer need to be assign
                Dim dealerCustomerList As New ArrayList
                crt = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crt.opAnd(New Criteria(GetType(CustomerDealer), "Customer", MatchType.Exact, itemCustomer.CustomerID.ID))
                crt.opAnd(New Criteria(GetType(CustomerDealer), "Dealer", MatchType.NotInSet, strDealer))
                dealerCustomerList = New CustomerDealerFacade(User).Retrieve(crt)
                If dealerCustomerList.Count > 0 Then
                    For i As Integer = 0 To dealerCustomerList.Count - 1
                        str += CType(dealerCustomerList(i), CustomerDealer).Dealer.DealerCode & "; "
                    Next
                End If
            Next

        End If

        Return str
    End Function

    Protected Sub dtgFleetCustomerList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgFleetCustomerList.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "view"
                ' view here
                _sessHelper.SetSession("FleetCustomerID", CInt(e.Item.Cells(0).Text))
                _sessHelper.SetSession("Status", "View")
                Response.Redirect("FrmInputFleetCustomer.aspx")
            Case "edit"
                ' edit here 
                _sessHelper.SetSession("FleetCustomerID", CInt(e.Item.Cells(0).Text))
                _sessHelper.SetSession("Status", "Update")
                _sessHelper.SetSession("StatusUpdate", "UpdateFleet")
                Response.Redirect("FrmInputFleetCustomer.aspx")
            Case "delete"
                ' delete here
                DeleteFleetCustomer(CInt(e.Item.Cells(0).Text))
                BindDatagrid(dtgFleetCustomerList.CurrentPageIndex)
            Case "assigntocustomer"
                ' assign to customer here
                _sessHelper.SetSession("FleetCustomerID", CInt(e.Item.Cells(0).Text))
                Response.Redirect("FrmAssignToCustomer.aspx")
            Case "assigntodealer"
                ' assign to dealer here
                _sessHelper.SetSession("FleetCustomerID", CInt(e.Item.Cells(0).Text))
                _sessHelper.SetSession("Status", "Update")
                _sessHelper.SetSession("StatusUpdate", "UpdateDealer")
                Response.Redirect("FrmInputFleetCustomer.aspx")
        End Select
    End Sub

    Private Sub DeleteFleetCustomer(ByVal nID As Integer)
        objFleetCustomer = facFleetCustomer.Retrieve(nID)
        If Not objFleetCustomer Is Nothing Then
            objFleetCustomer.RowStatus = DBRowStatus.Deleted
            Dim nResult = facFleetCustomer.Update(objFleetCustomer)
        Else
            MessageBox.Show(SR.DeleteFail)
        End If

        dtgFleetCustomerList.CurrentPageIndex = 0
        BindDatagrid(dtgFleetCustomerList.CurrentPageIndex)
        'End If
    End Sub

    Protected Sub dtgFleetCustomerList_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgFleetCustomerList.SortCommand
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

        dtgFleetCustomerList.SelectedIndex = -1
        dtgFleetCustomerList.CurrentPageIndex = 0
        BindDatagrid(dtgFleetCustomerList.CurrentPageIndex)
    End Sub

    Protected Sub dtgFleetCustomerList_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgFleetCustomerList.PageIndexChanged
        dtgFleetCustomerList.SelectedIndex = -1
        dtgFleetCustomerList.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgFleetCustomerList.CurrentPageIndex)
    End Sub
End Class
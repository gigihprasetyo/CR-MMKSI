Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search

Public Class FrmDealerTerritory
    Inherits System.Web.UI.Page
    Private m_bFormPrivilege As Boolean = False
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchTerm1 As System.Web.UI.WebControls.Label
    Protected WithEvents TempName As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents TempSearch As System.Web.UI.HtmlControls.HtmlInputHidden
    Private objDealer As Dealer

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlCity As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgDealerTerritory As System.Web.UI.WebControls.DataGrid
    Private _sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents ddlProvince As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CompareValidator2 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator3 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblErrMsg As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Method/Function"
    Private Function UpdateDealerTerritory() As Integer
        Dim nResult As Integer
        Dim countRecord As Integer
        Dim DealerIDStart As Integer
        Dim CityIDStart As Integer

        Dim objDealerTerritory As DealerTerritory = CType(Session.Item("vsDealerTerritory"), DealerTerritory)
        Dim objDealerTerritoryNew As DealerTerritory = New DealerTerritory
        Dim objDealerTerritoryFacade As DealerTerritoryFacade = New DealerTerritoryFacade(User)

        DealerIDStart = objDealerTerritory.Dealer.ID
        CityIDStart = objDealerTerritory.City.ID

        Dim objDealerFacade As DealerFacade = New DealerFacade(User)
        Dim objSelectedDealer As Dealer = objDealerFacade.Retrieve(Me.txtKodeDealer.Text.Trim)
        objDealerTerritoryNew.Dealer = objSelectedDealer
        objDealerTerritoryNew.City = New CityFacade(User).Retrieve(CType(ddlCity.SelectedValue, Integer))


        If objSelectedDealer.ID = DealerIDStart And CType(ddlCity.SelectedValue, Integer) = CityIDStart Then
            countRecord = 0
        Else
            countRecord = objDealerTerritoryFacade.ValidateCode(objDealerTerritoryNew)
        End If

        If countRecord = 0 Then
            objDealerTerritoryNew.ID = objDealerTerritory.ID
            nResult = New DealerTerritoryFacade(User).Update(objDealerTerritoryNew)
        Else
            nResult = 10
        End If

        Return nResult
    End Function

    Private Sub ViewDealerTerritory(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objDealerTerritory As DealerTerritory = New DealerTerritoryFacade(User).Retrieve(nID)
        _sessHelper.SetSession("vsDealerTerritory", objDealerTerritory)

        If IsNothing(objDealerTerritory.Dealer) Then
            Me.txtKodeDealer.Text = ""
            Me.lblDealerName.Text = ""
            Me.lblSearchTerm1.Text = ""
            Me.TempName.Value = ""
            Me.TempSearch.Value = ""
            MessageBox.Show(SR.ViewFail)
        Else
            If objDealerTerritory.Dealer.Status = "1" Then
                Me.txtKodeDealer.Text = objDealerTerritory.Dealer.DealerCode
                Me.lblDealerName.Text = objDealerTerritory.Dealer.DealerName
                Me.lblSearchTerm1.Text = objDealerTerritory.Dealer.SearchTerm1
            Else
                Me.txtKodeDealer.Text = ""
                Me.lblDealerName.Text = ""
                Me.lblSearchTerm1.Text = ""
                Me.TempName.Value = ""
                Me.TempSearch.Value = ""
                Me.lblErrMsg.Text = Me.lblErrMsg.Text & "Dealer " & objDealerTerritory.Dealer.DealerCode & " sudah tidak aktif<BR>"
            End If

        End If

        If IsNothing(objDealerTerritory.Dealer) Then
            ddlProvince.SelectedValue = "-1"
        Else
            ddlProvince.SelectedValue = CType(objDealerTerritory.City.Province.ID, String)
        End If

        If EditStatus Then
            BindDdlCity()

            If IsNothing(objDealerTerritory.City) Then
                ddlCity.SelectedValue = "-1"
            Else
                If objDealerTerritory.City.Status = "X" Then
                    ddlCity.SelectedValue = "-1"
                    Me.lblErrMsg.Text = Me.lblErrMsg.Text & "Kota " & objDealerTerritory.City.CityName & " sudah tidak aktif<BR>"
                Else
                    ddlCity.SelectedValue = CType(objDealerTerritory.City.ID, String)
                End If
            End If
        Else
            Me.ddlCity.Items.Clear()
            If IsNothing(objDealerTerritory.City) Then
                Me.ddlCity.Items.Add(New ListItem("Silahkan Pilih", "0"))
            Else
                Me.ddlCity.Items.Add(New ListItem(objDealerTerritory.City.CityName, "0"))
            End If
        End If

        Me.btnSimpan.Enabled = EditStatus
        Me.btnSearch.Enabled = EditStatus
        Me.lblSearchDealer.Enabled = EditStatus
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal DealerTerritoryID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "DealerTerritory", MatchType.Exact, DealerTerritoryID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Sub DeleteDealerTerritory(ByVal nID As Integer)

        Dim objDealerTerritory As DealerTerritory = New DealerTerritoryFacade(User).Retrieve(nID)

        Dim nResult = New DealerTerritoryFacade(User).DeleteFromDB(objDealerTerritory)

        dtgDealerTerritory.CurrentPageIndex = 0
        BindDtgDealerTerritory(dtgDealerTerritory.CurrentPageIndex, False, False)

    End Sub

    Private Sub BindDdlProvince()
        ddlProvince.DataSource = New ProvinceFacade(User).RetrieveList("ProvinceName", Sort.SortDirection.ASC)
        ddlProvince.DataTextField = "ProvinceName"
        ddlProvince.DataValueField = "ID"
        ddlProvince.DataBind()
        ddlProvince.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub

    Private Sub BindDdlCity()
        ddlCity.Items.Clear()
        If ddlProvince.SelectedValue = "-1" Then
            ddlCity.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        Else
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "Province.ID", ddlProvince.SelectedValue))
            crit.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))
            crit.opAnd(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            ddlCity.Items.Clear()
            ddlCity.DataSource = New CityFacade(User).RetrieveList("CityName", Sort.SortDirection.ASC, crit)
            ddlCity.DataTextField = "CityName"
            ddlCity.DataValueField = "ID"
            ddlCity.DataBind()
            ddlCity.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        End If
    End Sub

    Private Sub BindDtgDealerTerritory(ByVal indexPage As Integer, ByVal isWithCriteria As Boolean, ByVal pastCrit As Boolean)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            Dim crit As CriteriaComposite
            
            If pastCrit And Not Session.Item("CritTemp") Is Nothing Then
                crit = CType(Session.Item("CritTemp"), CriteriaComposite)
            Else
                crit = New CriteriaComposite(New Criteria(GetType(DealerTerritory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            End If

            If isWithCriteria Then
                objDealer = Session("DEALER")
                If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerTerritory), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
                End If
                If Me.txtKodeDealer.Text.Trim <> "" Then
                    crit.opAnd(New Criteria(GetType(DealerTerritory), "Dealer.DealerCode", MatchType.Exact, Me.txtKodeDealer.Text))
                End If
                If ddlCity.SelectedValue <> "-1" Then
                    crit.opAnd(New Criteria(GetType(DealerTerritory), "City.ID", MatchType.Exact, CType(Me.ddlCity.SelectedValue, Integer)))
                End If
                If ddlProvince.SelectedValue <> "-1" Then
                    crit.opAnd(New Criteria(GetType(DealerTerritory), "City.Province.ID", MatchType.Exact, CType(Me.ddlProvince.SelectedValue, Integer)))
                End If
            End If

            'Todo session
            Session.Add("CritTemp", crit)

            dtgDealerTerritory.DataSource = New DealerTerritoryFacade(User).RetrieveActiveList(crit, indexPage + 1, dtgDealerTerritory.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                          CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            dtgDealerTerritory.VirtualItemCount = totalRow
            dtgDealerTerritory.DataBind()
        End If

    End Sub

    Private Sub initiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = ""
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.FakturKendaraanUpdateWilayahDaerah_Privilege)
        If Not SecurityProvider.Authorize(Context.User, SR.FakturKendaraanViewWilayahDaerah_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FAKTUR KENDARAAN - Wilayah Dealer")
        End If
    End Sub

    Private Sub ClearData()
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        txtKodeDealer.Enabled = True
        ddlCity.Enabled = True
        ddlCity.Items.Clear()
        ddlCity.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        ddlProvince.Enabled = True
        ddlProvince.SelectedValue = "-1"
        dtgDealerTerritory.SelectedIndex = -1
        Me.txtKodeDealer.Text = ""
        Me.lblDealerName.Text = ""
        Me.lblSearchTerm1.Text = ""
        Me.TempName.Value = ""
        Me.TempSearch.Value = ""
    End Sub

#End Region

#Region "Event Handlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblErrMsg.Text = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        If Not IsPostBack Then
            BindDdlProvince()
            Me.ddlProvince.SelectedValue = "-1"
            BindDdlCity()
            Me.ddlCity.SelectedValue = "-1"
            'BindDtgDealerTerritory(0, False)
            initiatePage()
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDtgDealerTerritory(0, True, False)
        lblDealerName.Text = TempName.Value
        lblSearchTerm1.Text = TempSearch.Value
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Exit Sub
        End If

        Dim objDealerTerritory As DealerTerritory = New DealerTerritory
        Dim objDealerFacade As DealerFacade = New DealerFacade(User)
        Dim objCityFacade As CityFacade = New CityFacade(User)
        Dim objDealer As Dealer
        objDealer = objDealerFacade.Retrieve(txtKodeDealer.Text)

        If (Not objDealer Is Nothing) AndAlso (Not objDealer.ID = 0) Then
            objDealerTerritory.Dealer = objDealer
            objDealerTerritory.City = objCityFacade.Retrieve(CType(ddlCity.SelectedValue, Integer))

            Dim objDealerTerritoryFacade As DealerTerritoryFacade = New DealerTerritoryFacade(User)
            Dim nResult As Integer = -1
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                If objDealerTerritoryFacade.ValidateCode(objDealerTerritory) = 0 Then
                    Try
                        nResult = New DealerTerritoryFacade(User).Insert(objDealerTerritory)
                    Catch ex As Exception
                        nResult = -1
                    End Try
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Kode Kota atau Kode Dealer"))
                End If
            Else
                Try
                    nResult = UpdateDealerTerritory()
                Catch ex As Exception
                    nResult = -1
                End Try


                If nResult = -1 Then
                    MessageBox.Show(SR.UpdateFail)
                ElseIf nResult = 10 Then
                    MessageBox.Show(SR.DataIsExist("Kode Kota atau Kode Dealer"))
                Else
                    MessageBox.Show(SR.UpdateSucces)
                    ClearData()
                End If
            End If

            dtgDealerTerritory.CurrentPageIndex = 0
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            BindDtgDealerTerritory(dtgDealerTerritory.CurrentPageIndex, False, False)
        Else
            MessageBox.Show(SR.DataNotFound("Dealer " & Me.txtKodeDealer.Text))
        End If

    End Sub

    Private Sub ddlProvince_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlProvince.SelectedIndexChanged
        BindDdlCity()
    End Sub

    Private Sub dtgDealerTerritory_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDealerTerritory.PageIndexChanged
        dtgDealerTerritory.SelectedIndex = -1
        dtgDealerTerritory.CurrentPageIndex = e.NewPageIndex
        BindDtgDealerTerritory(dtgDealerTerritory.CurrentPageIndex, False, True)
        ClearData()
    End Sub

    Private Sub dtgDealerTerritory_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDealerTerritory.SortCommand
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

        dtgDealerTerritory.SelectedIndex = -1
        dtgDealerTerritory.CurrentPageIndex = 0
        BindDtgDealerTerritory(dtgDealerTerritory.CurrentPageIndex, False, True)
        ClearData()
    End Sub

    Private Sub dtgDealerTerritory_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealerTerritory.ItemDataBound
        'If e.Item.ItemIndex <> -1 Then
        'e.Item.Cells(2).Text = e.Item.ItemIndex + 1
        'End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            Dim objDealerTeritory As DealerTerritory = CType(e.Item.DataItem, DealerTerritory)
            Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
            Dim lblProvince As Label = CType(e.Item.FindControl("lblProvince"), Label)
            Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
            lblCity.Text = ""
            lblProvince.Text = ""
            lblDealer.Text = ""

            If Not objDealerTeritory.City Is Nothing Then
                lblCity.Text = objDealerTeritory.City.CityName
                If Not objDealerTeritory.City.Province Is Nothing Then
                    lblProvince.Text = objDealerTeritory.City.Province.ProvinceName
                End If
            End If

            If Not objDealerTeritory.Dealer Is Nothing Then
                lblDealer.Text = objDealerTeritory.Dealer.DealerCode & " / " & objDealerTeritory.Dealer.SearchTerm1
            End If
            'DataItem.City.CityName
            'DataItem.City.Province.ProvinceName
            'DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") &amp; " / " &amp; DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1")
        End If

        If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = m_bFormPrivilege
        End If

        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = m_bFormPrivilege
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgDealerTerritory.CurrentPageIndex * dtgDealerTerritory.PageSize)
        End If

    End Sub

    Private Sub dtgDealerTerritory_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgDealerTerritory.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtKodeDealer.Enabled = False
            ddlProvince.Enabled = False
            ddlCity.Enabled = False
            ViewDealerTerritory(e.Item.Cells(0).Text, False)
            dtgDealerTerritory.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewDealerTerritory(e.Item.Cells(0).Text, True)
            dtgDealerTerritory.SelectedIndex = e.Item.ItemIndex
            txtKodeDealer.Enabled = True
            ddlProvince.Enabled = True
            ddlCity.Enabled = True
        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteDealerTerritory(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
        End If
    End Sub
#End Region



End Class

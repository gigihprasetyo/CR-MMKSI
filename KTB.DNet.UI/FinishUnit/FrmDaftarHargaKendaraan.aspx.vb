Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper

Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports OfficeOpenXml

Public Class FrmDaftarHargaKendaraan
    Inherits System.Web.UI.Page

    Private sesHelper As New SessionHelper
    Private displayPriv As Boolean
    Private editPriv As Boolean
    Private deletePriv As Boolean
    Private oDealer As Dealer
    Private SessionGridDataDealerVehiclePrice As String = "FrmDaftarHargaKendaraan.DealerVehiclePriceList"
    Private SessionCriteriaDealerVehiclePrice As String = "FrmDaftarHargaKendaraan.DealerVehiclePriceCriteria"
    Private SessionCriteria As String = "FrmDaftarHargaKendaraan.CriteriaDealerVehiclePriceList"

    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Authorization()
        If Not IsPostBack Then
            ViewState("currSortColumn") = "Dealer.DealerCode"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            PageInit()
            BindddlDNet()
            BindddlDMS()
            ''-- Restore selection criteria
            ReadCriteria()

            If IsLoginAsDealer() Then
                trKodeDealer.Attributes("style") = "display:none"
            End If

            ReadData()   '-- Read all data matching criteria
            BindGrid(dgVehiclePrice.CurrentPageIndex)  '-- Bind page-1
        End If

        lblSearchDealer.Attributes("onclick") = "ShowPopUpDealer();"
        'lblPopUpTO.Attributes("onclick") = "ShowPopUpTO()"
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Sub Authorization()
        'If Not SecurityProvider.Authorize(Context.User, SR.EVENTNASIONAL_DaftarEventNasional_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT NASIONAL - DAFTAR EVENT NASIONAL")
        'Else
        '    displayPriv = SecurityProvider.Authorize(Context.User, SR.EVENTNASIONAL_DaftarEventNasional_Privilege)
        '    'editPriv = SecurityProvider.Authorize(Context.User, SR.EVENTNASIONAL_DaftarEventNasional_Edit_Privilege)
        '    'deletePriv = SecurityProvider.Authorize(Context.User, SR.EVENTNASIONAL_DaftarEventNasional_Delete_Privilege)
        'End If
    End Sub

    Private Sub PageInit()
        icPeriodeStart.Value = Date.Now.AddMonths(-1)
        icPeriodeEnd.Value = Date.Now
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        'Dim arrDealerVehiclePriceList As ArrayList = CType(sesHelper.GetSession(SessionGridDataDealerVehiclePrice), ArrayList)
        Dim critDealerVehiclePrice As CriteriaComposite = CType(sesHelper.GetSession(SessionCriteriaDealerVehiclePrice), CriteriaComposite)
        Dim TotalRow As Integer = 0
        Dim PagedList As ArrayList = New DealerVehiclePriceFacade(User).RetrieveActiveList(critDealerVehiclePrice, pageIndex + 1, dgVehiclePrice.PageSize, TotalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        Dim arrDealerVehiclePriceList As ArrayList = PagedList

        If arrDealerVehiclePriceList.Count <> 0 Then
            'CommonFunction.SortListControl(arrDealerVehiclePriceList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            'Dim PagedList As ArrayList = ArrayListPager.DoPage(arrDealerVehiclePriceList, pageIndex, dgVehiclePrice.PageSize)
            dgVehiclePrice.DataSource = PagedList
            dgVehiclePrice.VirtualItemCount = TotalRow
            dgVehiclePrice.DataBind()
        Else
            dgVehiclePrice.DataSource = New ArrayList
            dgVehiclePrice.VirtualItemCount = 0
            dgVehiclePrice.CurrentPageIndex = 0
            dgVehiclePrice.DataBind()
        End If

        If arrDealerVehiclePriceList.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If
    End Sub

    Private Sub ReadData()
        '-- Row status = active
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerVehiclePrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If IsLoginAsDealer() Then
            txtKodeDealer.Text = SesDealer.DealerCode
        End If

        If txtKodeDealer.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(DealerVehiclePrice), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim.Replace(";", "','") & "')"))
        End If

        If txtCustomerClass.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(DealerVehiclePrice), "CustomerClass", MatchType.Partial, txtCustomerClass.Text.Trim))
        End If

        If ddlDNet.SelectedValue <> "-1" Then
            crit.opAnd(New Criteria(GetType(DealerVehiclePrice), "CustomerTypeDNET", MatchType.Exact, ddlDNet.SelectedItem.Value))
        End If

        If ddlDMS.SelectedValue <> "-1" Then
            crit.opAnd(New Criteria(GetType(DealerVehiclePrice), "CustomerTypeDMS", MatchType.Exact, ddlDMS.SelectedItem.Value))
        End If

        If txtCompany.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(DealerVehiclePrice), "Name", MatchType.Partial, txtCompany.Text.Trim))
        End If

        If cbDate.Checked Then
            Dim tglFrom As New Date(icPeriodeStart.Value.Year, icPeriodeStart.Value.Month, icPeriodeStart.Value.Day, 0, 0, 0)
            Dim tglTo As New Date(icPeriodeEnd.Value.Year, icPeriodeEnd.Value.Month, icPeriodeEnd.Value.Day, 23, 59, 59)
            crit.opAnd(New Criteria(GetType(DealerVehiclePrice), "EffectiveStartDate", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "", True)
            crit.opAnd(New Criteria(GetType(DealerVehiclePrice), "EffectiveStartDate", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), "", True)
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(DealerVehiclePrice), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        '-- Retrieve recordset
        'Dim arrDealerVehiclePriceList As ArrayList = New DealerVehiclePriceFacade(User).Retrieve(crit, sortColl)

        'sesHelper.SetSession(SessionGridDataDealerVehiclePrice, arrDealerVehiclePriceList)
        sesHelper.SetSession(SessionCriteriaDealerVehiclePrice, crit)
        'If arrDealerVehiclePriceList.Count <= 0 Then
        '    If IsPostBack Then
        '        MessageBox.Show(SR.DataNotFound("Data"))
        '    End If
        'End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ReadData()
        'dgVehiclePrice.CurrentPageIndex = 0 '-- Read all data matching criteria
        BindGrid(dgVehiclePrice.CurrentPageIndex)  '-- Bind page-1
        StoreCriteria()
    End Sub

    Private Sub dgVehiclePrice_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgVehiclePrice.PageIndexChanged
        '-- Change datagrid page

        ReadData()
        dgVehiclePrice.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
        StoreCriteria()
    End Sub

    Private Sub dgVehiclePrice_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgVehiclePrice.SortCommand
        '-- Sort datagrid rows based on a column header clicked

        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If

        '-- Bind page-1
        dgVehiclePrice.CurrentPageIndex = 0
        ReadData()
        BindGrid(dgVehiclePrice.CurrentPageIndex)
        StoreCriteria()
    End Sub

    Private Sub ReadCriteria()
        Dim crit As Hashtable
        crit = CType(sesHelper.GetSession(SessionCriteria), Hashtable)
        If Not crit Is Nothing Then
            txtKodeDealer.Text = CStr(crit.Item("Dealer.DealerCode"))
            cbDate.Checked = CBool(crit.Item("cbDate"))
            icPeriodeStart.Value = CStr(crit.Item("PeriodStart"))
            icPeriodeEnd.Value = CStr(crit.Item("PeriodEnd"))
            txtCompany.Text = CStr(crit.Item("Name"))
            txtCustomerClass.Text = CStr(crit.Item("CustomerClass"))

            ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
            ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
            dgVehiclePrice.CurrentPageIndex = CInt(crit.Item("PageIndex"))
        End If
    End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("Dealer.DealerCode", txtKodeDealer.Text)
        crit.Add("Name", txtCompany.Text)
        crit.Add("cbDate", cbDate.Checked)
        crit.Add("PeriodStart", icPeriodeStart.Value)
        crit.Add("PeriodEnd", icPeriodeEnd.Value)
        crit.Add("CustomerClass", txtCustomerClass.Text)

        crit.Add("PageIndex", dgVehiclePrice.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sesHelper.SetSession(SessionCriteria, crit) '-- Store in session
    End Sub

    Protected Sub dgVehiclePrice_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgVehiclePrice.ItemCommand
        Select Case e.CommandName
            Case "Detail"
                Response.Redirect("FrmHargaKendaraanDetail.aspx?Mode=View&DealerVehiclePriceID=" & e.Item.Cells(0).Text)
                'Case "Edit"
                '    Response.Redirect("FrmInputEventNational.aspx?Mode=Edit&DealerVehiclePriceID=" & e.Item.Cells(0).Text)
                'Case "Mapping"
                '    Response.Redirect("FrmRegistrasiEventNational.aspx?Mode=New&DealerVehiclePriceID=" & e.Item.Cells(0).Text)
                'Case "Delete"
                'DeleteDealerVehiclePrice(CInt(e.CommandArgument))
        End Select
    End Sub

    Private Sub BindddlDNet()
        ddlDNet.Items.Clear()
        ddlDNet.DataSource = New EnumTipePelangganCustomerRequest().RetrieveType(True)
        ddlDNet.DataTextField = "NameTipe"
        ddlDNet.DataValueField = "ValTipe"
        ddlDNet.DataBind()
    End Sub

    Private Sub BindddlDMS()
        ddlDMS.Items.Clear()
        ddlDMS.Items.Add(New ListItem("Semua", "-1"))
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerVehiclePrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arrDealerVehiclePrice As ArrayList = New DealerVehiclePriceFacade(User).RetrieveActiveList()
        Dim arrTypeDMS As New ArrayList()
        For Each _dealerVehPrice As DealerVehiclePrice In arrDealerVehiclePrice
            If Not arrTypeDMS.Contains(_dealerVehPrice.CustomerTypeDMS) Then
                ddlDMS.Items.Add(New ListItem(_dealerVehPrice.CustomerTypeDMS, _dealerVehPrice.CustomerTypeDMS))
                arrTypeDMS.Add(_dealerVehPrice.CustomerTypeDMS)
            End If
        Next
    End Sub

    Protected Sub dgVehiclePrice_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgVehiclePrice.ItemDataBound
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
        Dim lblNamaDealer As Label = CType(e.Item.FindControl("lblNamaDealer"), Label)
        Dim lblMataUang As Label = CType(e.Item.FindControl("lblMataUang"), Label)
        Dim lblPeriodStart As Label = CType(e.Item.FindControl("lblPeriodStart"), Label)
        Dim lblCustomerClass As Label = CType(e.Item.FindControl("lblCustomerClass"), Label)
        Dim lblCompany As Label = CType(e.Item.FindControl("lblCompany"), Label)
        Dim lblTypeDMS As Label = CType(e.Item.FindControl("lblTypeDMS"), Label)
        Dim lblTypeDNet As Label = CType(e.Item.FindControl("lblTypeDNet"), Label)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As DealerVehiclePrice = CType(e.Item.DataItem, DealerVehiclePrice)
            lblNo.Text = (e.Item.ItemIndex + 1 + (dgVehiclePrice.PageSize * dgVehiclePrice.CurrentPageIndex)).ToString
            lblKodeDealer.Text = oData.Dealer.DealerCode
            lblNamaDealer.Text = oData.Dealer.DealerName
            lblMataUang.Text = oData.Currency
            lblPeriodStart.Text = oData.EffectiveStartDate
            lblCustomerClass.Text = oData.CustomerClass
            lblCompany.Text = oData.Name
            lblTypeDMS.Text = oData.CustomerTypeDMS
            lblTypeDNet.Text = New EnumTipePelangganCustomerRequest().RetrieveTipePelangganCustomerRequest(oData.CustomerTypeDNET)

            Dim lnkbtnDetail As LinkButton = CType(e.Item.FindControl("lnkbtnDetail"), LinkButton)

            If Not IsNothing(lnkbtnDetail) Then
                lnkbtnDetail.Visible = True
            End If
        End If
    End Sub

    'Protected Sub hdnDealer_ValueChanged(sender As Object, e As EventArgs) Handles hdnDealer.ValueChanged
    '    Dim data As String() = hdnDealer.Value.Trim.Split(";")
    '    txtKodeDealer.Text = data(0)
    'End Sub

    'Protected Sub hdnTempEvent_ValueChanged(sender As Object, e As EventArgs) Handles hdnTempEvent.ValueChanged
    '    Dim data As String() = hdnTempEvent.Value.Trim.Split(";")
    '    txtKodeEvent.Text = data(0)
    'End Sub

    'Protected Sub txtKodeEvent_TextChanged(sender As Object, e As EventArgs) Handles txtKodeEvent.TextChanged
    '    If txtKodeEvent.Text.Trim = String.Empty Then
    '        lblNamaEvent.Text = ""
    '        Exit Sub
    '    End If
    '    Dim objDealerVehiclePrice As DealerVehiclePrice = New DealerVehiclePriceFacade(User).Retrieve(txtKodeEvent.Text)
    '    If objDealerVehiclePrice.ID > 0 Then
    '        lblNamaEvent.Text = objDealerVehiclePrice.DealerVehiclePriceType.Name & " " & objDealerVehiclePrice.DealerVehiclePriceCity.City.CityName
    '    End If

    'End Sub

End Class
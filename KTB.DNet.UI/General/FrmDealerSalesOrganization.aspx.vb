
#Region " Customer Name Space "

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports System.IO
Imports System.Text
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
#End Region


Public Class FrmDealerSalesOrganization
    Inherits System.Web.UI.Page


    Private objDealer As Dealer
    Private _sessHelper As SessionHelper = New SessionHelper

    Private Sub PermitEditMode()
        If Not SecurityProvider.Authorize(Context.User, SR.ViewSalesOrganization_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Umum - Sales Organization")
        End If
    End Sub

    Private _SalesOrganization As New List(Of StandardCode)
    Protected ReadOnly Property ListSalesOrganization As List(Of StandardCode)
        Get
            If _SalesOrganization.Count = 0 Then
                _SalesOrganization = New StandardCodeFacade(Me.User).RetrieveByCategory("AreaBusiness").Cast(Of StandardCode).ToList()
            End If

            Return _SalesOrganization
        End Get
    End Property

    Private _listDistributionChanel As New List(Of StandardCode)
    Protected ReadOnly Property ListDistributionChanel As List(Of StandardCode)
        Get
            If _listDistributionChanel.Count = 0 Then
                _listDistributionChanel = New StandardCodeFacade(Me.User).RetrieveByCategory("DealerDistributionChanel").Cast(Of StandardCode).ToList()
            End If

            Return _listDistributionChanel
        End Get
    End Property

    Private _listSalesDistric As New List(Of StandardCode)
    Protected ReadOnly Property ListSalesDistric As List(Of StandardCode)
        Get
            If _listSalesDistric.Count = 0 Then
                _listSalesDistric = New StandardCodeFacade(Me.User).RetrieveByCategory("DealerSalesDistric").Cast(Of StandardCode).ToList()
            End If

            Return _listSalesDistric
        End Get
    End Property

    Private _listCustomerGroup As New List(Of StandardCode)
    Protected ReadOnly Property ListCustomerGroup As List(Of StandardCode)
        Get
            If _listCustomerGroup.Count = 0 Then
                _listCustomerGroup = New StandardCodeFacade(Me.User).RetrieveByCategory("DealerCustomerGroup").Cast(Of StandardCode).ToList()
            End If

            Return _listCustomerGroup
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PermitEditMode()
            ViewState("CurrentSortColumn") = "DealerCode"
            ViewState("CurrentSortDirect") = SortDirection.Ascending
            InitPage()
        End If
    End Sub

    Private Sub InitPage()
        txtKodeDealer.Text = String.Empty

        chkSalesOrg.ClearSelection()
        chkSalesOrg.Items.Clear()
        chkSalesOrg.DataSource = Me.ListSalesOrganization
        chkSalesOrg.DataValueField = "valuecode"
        chkSalesOrg.DataTextField = "valuedesc"
        chkSalesOrg.DataBind()

        chkDistributionChannel.ClearSelection()
        chkDistributionChannel.Items.Clear()
        chkDistributionChannel.DataSource = Me.ListDistributionChanel
        chkDistributionChannel.DataValueField = "valuecode"
        chkDistributionChannel.DataTextField = "valuedesc"
        chkDistributionChannel.DataBind()

    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSalesOrganization), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim criteriasDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasDealer.opAnd(New Criteria(GetType(Dealer), "OrganizationBranchType", MatchType.Exact, 1))

        If Not txtKodeDealer.IsEmpty Then
            criteriasDealer.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, txtKodeDealer.Text.ConvertDealerCode()))
            criterias.opAnd(New Criteria(GetType(DealerSalesOrganization), "Dealer.DealerCode", MatchType.InSet, txtKodeDealer.Text.ConvertDealerCode()))
        End If

        Dim isFilter As Boolean = False
        Dim slsOrg As String = chkSalesOrg.GetValueSelected()
        If Not String.IsNullorEmpty(slsOrg) Then
            isFilter = True
            criterias.opAnd(New Criteria(GetType(DealerSalesOrganization), "SalesOrganizationCode", MatchType.InSet, slsOrg))
        End If

        Dim disChannel As String = chkDistributionChannel.GetValueSelected()
        If Not String.IsNullorEmpty(disChannel) Then
            isFilter = True
            criterias.opAnd(New Criteria(GetType(DealerSalesOrganization), "DistributionChannel", MatchType.InSet, disChannel))
        End If

        _sessHelper.SetSession("criterias", criterias)
        _sessHelper.SetSession("isfilter", isFilter)
        _sessHelper.SetSession("criteriasDealer", criteriasDealer)
        BindDataGrid()
    End Sub

    Private Sub BindDataGrid(Optional ByVal pageNumber As Integer = 0)
        Dim totalRow As Integer = 0
        Dim isFilter As Boolean = _sessHelper.GetSession("isfilter")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSalesOrganization), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim criteriasDealer As CriteriaComposite = _sessHelper.GetSession("criteriasDealer")
        Dim func As New DealerSalesOrganizationFacade(Me.User)



        Dim listDealer As New List(Of Dealer)
        Dim listSalesOrg As List(Of DealerSalesOrganization)

        If isFilter Then
            Dim listDealerCode As New List(Of Integer)
            criterias = _sessHelper.GetSession("criterias")
            listSalesOrg = func.Retrieve(criterias).Cast(Of DealerSalesOrganization).ToList()
            Dim listDealerTemp As New List(Of Dealer)
            Dim columns As String = CType(ViewState("CurrentSortColumn"), String)
            Select Case CType(ViewState("CurrentSortDirect"), SortDirection)
                Case SortDirection.Ascending
                    Select Case CType(ViewState("CurrentSortColumn"), String).ToLower()
                        Case "dealercode"
                            listDealerTemp = (From dl In listSalesOrg Select dl.Dealer).OrderBy(Function(x) x.DealerCode).ToList()

                        Case "dealername"
                            listDealerTemp = (From dl In listSalesOrg Select dl.Dealer).OrderBy(Function(x) x.DealerName).ToList()

                    End Select
                Case SortDirection.Descending
                    Select Case CType(ViewState("CurrentSortColumn"), String).ToLower()
                        Case "dealercode"
                            listDealerTemp = (From dl In listSalesOrg Select dl.Dealer).OrderByDescending(Function(x) x.DealerCode).ToList()

                        Case "dealername"
                            listDealerTemp = (From dl In listSalesOrg Select dl.Dealer).OrderByDescending(Function(x) x.DealerName).ToList()

                    End Select
            End Select
            listDealerCode = (From dl In listDealerTemp Select CInt(dl.DealerCode)).Distinct.ToList()

            For idx As Integer = 1 To dgSalesOrganization.PageSize
                Try
                    Dim dl As Dealer = listDealerTemp.FirstOrDefault(Function(d) d.DealerCode = listDealerCode(((pageNumber + 1) * 10 - 10) + idx - 1).ToString())
                    If Not dl Is Nothing Then
                        listDealer.Add(dl)
                    End If
                Catch
                End Try
            Next
            totalRow = listDealerCode.Count
        Else
            listDealer = New DealerFacade(Me.User).RetrieveByCriteria(criteriasDealer, pageNumber + 1, _
                                                              dgSalesOrganization.PageSize, totalRow, ViewState("CurrentSortColumn"), _
                                                               ViewState("CurrentSortDirect")).Cast(Of Dealer).ToList()
            Dim strDealerCode As String = String.Join(", ", (From dl In listDealer Select "'" + dl.DealerCode + "'").ToArray())
            criterias.opAnd(New Criteria(GetType(DealerSalesOrganization), "Dealer.DealerCode", MatchType.InSet, "(" + strDealerCode + ")"))
            listSalesOrg = func.Retrieve(criterias).Cast(Of DealerSalesOrganization).ToList()
        End If

        _sessHelper.SetSession("listSalesOrg", listSalesOrg)
        dgSalesOrganization.DataSource = listDealer
        dgSalesOrganization.VirtualItemCount = totalRow
        dgSalesOrganization.DataBind()
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        InitPage()
    End Sub

    Private Sub dgSalesOrganization_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgSalesOrganization.ItemDataBound
        If e.IsRowItems Then
            Dim objData As Dealer = e.DataItem(Of Dealer)()
            Dim listSalesOrg As List(Of DealerSalesOrganization) = _sessHelper.GetSession("listSalesOrg")

            Dim lblNo As Label = e.FindLabel("lblNo")
            Dim tblSalesOrganization As HtmlTable = e.Item.FindControl("tblSalesOrganization")
            Dim tblDistributionChannel As HtmlTable = e.Item.FindControl("tblDistributionChannel")
            Dim tblSalesDistric As HtmlTable = e.Item.FindControl("tblSalesDistric")
            Dim tblCustomerGroup As HtmlTable = e.Item.FindControl("tblCustomerGroup")

            For Each iSalesOrg As DealerSalesOrganization In listSalesOrg.Where(Function(x) x.Dealer.DealerCode = objData.DealerCode)
                tblSalesOrganization.Rows.Add(AddRowData("SalesOrganization", iSalesOrg))
                tblDistributionChannel.Rows.Add(AddRowData("DistributionChannel", iSalesOrg))
                tblSalesDistric.Rows.Add(AddRowData("SalesDistric", iSalesOrg))
                tblCustomerGroup.Rows.Add(AddRowData("CustomerGroup", iSalesOrg))
            Next

            lblNo.Text = e.CreateNumberPage()

        End If

    End Sub

    Private Function AddRowData(ByVal strCode As String, ByVal iSalesOrg As DealerSalesOrganization) As HtmlTableRow
        Dim cellData As New HtmlTableCell
        Dim rowData As New HtmlTableRow
        Select Case strCode
            Case "SalesOrganization"
                Try
                    cellData.InnerText = ListSalesOrganization.FirstOrDefault(Function(x) x.ValueCode = iSalesOrg.SalesOrganizationCode).ValueDesc
                Catch
                    cellData.InnerText = iSalesOrg.SalesOrganizationCode
                End Try

            Case "DistributionChannel"
                Try
                    cellData.InnerText = ListDistributionChanel.FirstOrDefault(Function(x) x.ValueCode = iSalesOrg.DistributionChannel).ValueDesc
                Catch
                    cellData.InnerText = iSalesOrg.DistributionChannel
                End Try


            Case "SalesDistric"
                Try
                    cellData.InnerText = ListSalesDistric.FirstOrDefault(Function(x) x.ValueCode = iSalesOrg.SalesDistrict).ValueDesc
                Catch
                    cellData.InnerText = iSalesOrg.SalesDistrict
                End Try


            Case "CustomerGroup"
                Try
                    cellData.InnerText = ListCustomerGroup.FirstOrDefault(Function(x) x.ValueCode = iSalesOrg.CustomerGroup).ValueDesc
                Catch
                    cellData.InnerText = iSalesOrg.CustomerGroup
                End Try

        End Select
        cellData.Style.Add("text-align", "center")
        cellData.Style.Add("vertical-align", "top")
        cellData.Width = "100%"
        rowData.Cells.Add(cellData)
        Return rowData
    End Function

    Private Sub dgSalesOrganization_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgSalesOrganization.PageIndexChanged
        dgSalesOrganization.SelectedIndex = -1
        dgSalesOrganization.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSalesOrganization.CurrentPageIndex)
    End Sub

    Private Sub dgSalesOrganization_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgSalesOrganization.SortCommand
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
        BindDataGrid()
    End Sub

End Class
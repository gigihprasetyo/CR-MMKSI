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

Public Class FrmListServiceGrade
    Inherits System.Web.UI.Page

    Private objDealer As Dealer
    Private _sessHelper As SessionHelper = New SessionHelper

    Private Sub PermitEditMode()
        If Not SecurityProvider.Authorize(Context.User, SR.ViewServiceGrade_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Umum - Grade Dealer")
        End If
    End Sub

    Private _listEquipmentClass As New List(Of StandardCode)
    Protected ReadOnly Property ListEquipmentClass As List(Of StandardCode)
        Get
            If _listEquipmentClass.Count = 0 Then
                _listEquipmentClass = New StandardCodeFacade(Me.User).RetrieveByCategory("EquipmentClass").Cast(Of StandardCode).ToList()
            End If

            Return _listEquipmentClass
        End Get
    End Property

    Private _listDealerFacility As New List(Of StandardCode)
    Protected ReadOnly Property ListDealerFacility As List(Of StandardCode)
        Get
            If _listDealerFacility.Count = 0 Then
                _listDealerFacility = New StandardCodeFacade(Me.User).RetrieveByCategory("DealerFacility").Cast(Of StandardCode).ToList()
            End If

            Return _listDealerFacility
        End Get
    End Property

    Private _listServiceGrade As New List(Of StandardCode)
    Protected ReadOnly Property ListServiceGrade As List(Of StandardCode)
        Get
            If _listServiceGrade.Count = 0 Then
                _listServiceGrade = New StandardCodeFacade(Me.User).RetrieveByCategory("ServiceGrade").Cast(Of StandardCode).ToList()
            End If

            Return _listServiceGrade
        End Get
    End Property

    Private _listStallEquipment As New List(Of StandardCode)
    Protected ReadOnly Property ListStallEquipment As List(Of StandardCode)
        Get
            If _listStallEquipment.Count = 0 Then
                _listStallEquipment = New StandardCodeFacade(Me.User).RetrieveByCategory("DealerStallEquipment").Cast(Of StandardCode).ToList()
            End If

            Return _listStallEquipment
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PermitEditMode()
            ViewState("CurrentSortColumn") = "Dealer.ID"
            ViewState("CurrentSortDirect") = SortDirection.Ascending
            InitPage()
        End If
    End Sub

    Private Sub InitPage()
        txtKodeDealer.Text = String.Empty

        chkGrade.ClearSelection()
        chkGrade.Items.Clear()
        chkGrade.DataSource = Me.ListServiceGrade
        chkGrade.DataValueField = "ValueId"
        chkGrade.DataTextField = "ValueDesc"
        chkGrade.DataBind()
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerAdditional), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If Not txtKodeDealer.IsEmpty Then
            criterias.opAnd(New Criteria(GetType(DealerAdditional), "Dealer.DealerCode", MatchType.InSet, txtKodeDealer.Text.ConvertDealerCode()))
        End If

        Dim svcGrade As String = chkGrade.GetValueSelected()
        If Not String.IsNullorEmpty(svcGrade) Then
            criterias.opAnd(New Criteria(GetType(DealerAdditional), "ServiceGrade", MatchType.InSet, svcGrade))
        End If

        _sessHelper.SetSession("criterias", criterias)
        BindDataGrid()
    End Sub

    Private Sub BindDataGrid(Optional ByVal pageNumber As Integer = 0)
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = _sessHelper.GetSession("criterias")
        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing(ViewState("CurrentSortColumn"))) And (Not ViewState("CurrentSortColumn") = "") Then
            sortColl.Add(New Sort(GetType(DealerAdditional), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
        Else
            sortColl = Nothing
        End If

        dgServiceGrade.DataSource = New DealerAdditionalFacade(Me.User).RetrieveByCriteria(criterias, pageNumber + 1, dgServiceGrade.PageSize, totalRow, sortColl)
        dgServiceGrade.VirtualItemCount = totalRow
        dgServiceGrade.DataBind()
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        InitPage()
    End Sub

    Private Sub dgServiceGrade_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgServiceGrade.ItemDataBound
        If e.IsRowItems Then
            Dim objData As DealerAdditional = e.DataItem(Of DealerAdditional)()

            Dim lblEquipmentClass As Label = e.FindLabel("lblEquipmentClass")
            Dim bulletFacility As BulletedList = e.Item.FindControl("bulletFacility")
            Dim bulletStall As BulletedList = e.Item.FindControl("bulletStall")
            'Dim lblStallEquipment As Label = e.FindLabel("lblStallEquipment")
            Dim lblServiceGrade As Label = e.FindLabel("lblServiceGrade")
            Dim lblNo As Label = e.FindLabel("lblNo")

            lblNo.Text = e.CreateNumberPage()

            If objData.EquipmentClass > 0 Then
                lblEquipmentClass.Text = ListEquipmentClass.FirstOrDefault(Function(x) x.ValueId = objData.EquipmentClass).ValueDesc
            End If

            Dim funcF As New DealerfacilityFacade(Me.User)
            Dim arrFacility As ArrayList = funcF.RetrieveByDealerID(objData.Dealer.ID)
            bulletFacility.Items.Clear()
            For Each item As Dealerfacility In arrFacility
                bulletFacility.Items.Add(ListDealerFacility.FirstOrDefault(Function(x) x.ValueId = item.Facility).ValueDesc)
            Next

            'If objData.DealerFacility > 0 Then
            '    lblDealerFacility.Text = ListDealerFacility.FirstOrDefault(Function(x) x.ValueId = objData.DealerFacility).ValueDesc
            'End If
            Dim funcS As New DealerStallEquipmentFacade(Me.User)
            Dim arrStall As ArrayList = funcS.RetrieveByDealerID(objData.Dealer.ID)
            bulletStall.Items.Clear()
            For Each item As DealerStallEquipment In arrStall
                bulletStall.Items.Add(ListStallEquipment.FirstOrDefault(Function(x) x.ValueId = item.StallEquipment).ValueDesc)
            Next
            'If objData.DealerStallEquipment > 0 Then
            '    lblStallEquipment.Text = ListStallEquipment.FirstOrDefault(Function(x) x.ValueId = objData.DealerStallEquipment).ValueDesc
            'End If

            If objData.ServiceGrade > 0 Then
                lblServiceGrade.Text = ListServiceGrade.FirstOrDefault(Function(x) x.ValueId = objData.ServiceGrade).ValueDesc
            End If

        End If

    End Sub

    Private Sub dgServiceGrade_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgServiceGrade.PageIndexChanged
        dgServiceGrade.SelectedIndex = -1
        dgServiceGrade.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgServiceGrade.CurrentPageIndex)
    End Sub

    Private Sub dgServiceGrade_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgServiceGrade.SortCommand
        If CType(viewstate("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("CurrentSortColumn") = e.SortExpression
            viewstate("CurrentSortDirect") = Sort.SortDirection.ASC
        End If
        BindDataGrid()
    End Sub

End Class
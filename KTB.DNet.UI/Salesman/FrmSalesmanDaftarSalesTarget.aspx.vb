#Region " Summary "
'----------------------------------------------------'
'-- Program Code : FrmInvoiceResultList.aspx       --'
'-- Program Name : Daftar Status Faktur Kendaraan  --'
'-- Description  :                                 --'
'----------------------------------------------------'
'-- Programmer   : Agus Pirnadi                    --'
'-- Start Date   : Nov 01 2005                     --'
'-- Update By    :                                 --'
'-- Last Update  : Jan 02 2005                     --'
'----------------------------------------------------'
'-- Copyright © 2005 by Intimedia                  --'
'----------------------------------------------------'
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.WebCC

#End Region

Public Class FrmSalesmanDaftarSalesTarget
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        If Not IsPostBack Then
            ViewState("currSortColumn") = "ID"
            ViewState("currSortDirect") = Sort.SortDirection.ASC
            BindDropdownList()
            BindPage()

            If Not IsNothing(Session("DEALER")) Then
                Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
                If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Dealer Sales Target")
                End If
            End If
        End If
    End Sub

#Region " Private fields "
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim state As Boolean = False
#End Region

    Private Sub BindDropdownList()

        '-- Category criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim cat As String = ""
        cat = cat & "'PC',"
        cat = cat & "'CV',"
        cat = cat & "'LCV',"
        If cat <> "" Then
            cat = "(" & cat.Substring(0, cat.Length - 1) & ")"
            criterias.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.InSet, cat))
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code

        '-- Bind Category dropdownlist
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveByCriteria(criterias, sortColl)
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataValueField = "CategoryCode"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item

        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
    End Sub

    Private Sub BindPage(Optional ByVal pageIndex As Integer = 0)
        Dim TotalRow As Integer = 0
        Dim InvoiceResList As ArrayList = GetData(TotalRow, pageIndex)
        If InvoiceResList.Count <> 0 Then
            'Dim PagedList As ArrayList = ArrayListPager.DoPage(InvoiceResList, pageIndex, dgSaleStarget.PageSize)
            dgSaleStarget.DataSource = InvoiceResList 'PagedList
            dgSaleStarget.VirtualItemCount = TotalRow 'InvoiceResList.Count()
            dgSaleStarget.DataBind()
        Else
            dgSaleStarget.DataSource = New ArrayList
            dgSaleStarget.VirtualItemCount = 0
            dgSaleStarget.CurrentPageIndex = 0
            dgSaleStarget.DataBind()
        End If
    End Sub

    Private Function GetData(ByRef TotalRow As Integer, Optional ByVal indexPage As Integer = 0) As ArrayList
        Dim totRow As Integer = 0
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSalesTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtKodeDealer.Text <> "" Then
            criteria.opAnd(New Criteria(GetType(DealerSalesTarget), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
        End If
        If state = True Then
            'criteria.opAnd(New Criteria(GetType(DealerSalesTarget), "ValidFrom", MatchType.Exact, "'" & Format(icHandoverDateStart.Value, "yyyy-MM-dd HH:mm:ss") & "'"))
            '-- Periode HandoverDate
            Dim HandoverDateStart As New DateTime(CInt(icHandoverDateStart.Value.Year), CInt(icHandoverDateStart.Value.Month), CInt(icHandoverDateStart.Value.Day), 0, 0, 0)
            Dim HandoverDateEnd As New DateTime(CInt(icHandoverDateEnd.Value.Year), CInt(icHandoverDateEnd.Value.Month), CInt(icHandoverDateEnd.Value.Day), 23, 59, 59)
            criteria.opAnd(New Criteria(GetType(DealerSalesTarget), "ValidFrom", MatchType.GreaterOrEqual, Format(HandoverDateStart, "yyyy-MM-dd HH:mm:ss")))
            criteria.opAnd(New Criteria(GetType(DealerSalesTarget), "ValidFrom", MatchType.LesserOrEqual, Format(HandoverDateEnd, "yyyy-MM-dd HH:mm:ss")))
        End If
        If ddlSubCategory.SelectedIndex <> 0 Then
            Dim selectedValue As String = ""
            Dim spliter As String() = ddlSubCategory.SelectedItem.Text.Split(" ")
            If spliter.Length = 2 Then
                selectedValue = Replace(spliter(1), " ", "%")
            Else
                selectedValue = Replace(ddlSubCategory.SelectedItem.ToString, " ", "%")
            End If
            criteria.opAnd(New Criteria(GetType(DealerSalesTarget), "VehicleModel.VechileModelIndCode", MatchType.Partial, selectedValue))
        End If
        'Dim _return As ArrayList = New DealerSalesTargetFacade(User).Retrieve(criteria)
        Dim _return As ArrayList = New DealerSalesTargetFacade(User).RetrieveActiveList(indexPage + 1, _
                    dgSaleStarget.PageSize, totRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection), criteria)
        TotalRow = totRow
        Return _return
    End Function

    Protected Sub dgSaleStarget_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgSaleStarget.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (dgSaleStarget.CurrentPageIndex * dgSaleStarget.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No
        End If
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        state = True
        BindPage()
    End Sub

    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
    End Sub

    Protected Sub dgSaleStarget_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgSaleStarget.SortCommand
        '-- Sort datagrid rows based on a column header clicked
        state = True
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
        dgSaleStarget.CurrentPageIndex = 0
        BindPage(dgSaleStarget.CurrentPageIndex)
    End Sub

    Protected Sub dgSaleStarget_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgSaleStarget.PageIndexChanged
        dgSaleStarget.CurrentPageIndex = e.NewPageIndex
        BindPage(dgSaleStarget.CurrentPageIndex)
    End Sub
End Class
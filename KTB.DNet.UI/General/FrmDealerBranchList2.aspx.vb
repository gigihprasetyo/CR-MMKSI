#Region "Import dll"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports System
Imports System.Drawing.Color
Imports System.Text
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper
#End Region

Public Class FrmDealerBranchList2
    Inherits System.Web.UI.Page


#Region "Var"
    Private _sessHelper As SessionHelper = New SessionHelper

#End Region

#Region "Custom Method"

    Private Function ConvertKodeDealer(ByVal sKodeDealerColl As String)
        Dim sKodeDealerTemp() As String = sKodeDealerColl.Split(New Char() {";"})
        Dim sKodeDealer As String = ""
        For i As Integer = 0 To sKodeDealerTemp.Length - 1
            sKodeDealer = sKodeDealer & "'" & sKodeDealerTemp(i).Trim() & "'"

            If Not (i = sKodeDealerTemp.Length - 1) Then
                sKodeDealer = sKodeDealer & ","
            End If
        Next
        sKodeDealer = "(" & sKodeDealer & ")"
        Return sKodeDealer
    End Function

    Private Sub CreateCriteriaSearch()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'objDealer = Session("DEALER")
        'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        'End If
        If Not (txtKodeDealer.Text.Trim() = "") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "Dealer.DealerCode", MatchType.InSet, ConvertKodeDealer(txtKodeDealer.Text.Trim())))
        End If
        If Not (txtDealerName.Text.Trim() = "") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "Name", MatchType.[Partial], txtDealerName.Text.Trim))
        End If
        If Not (ddlBranchType.SelectedValue = "") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "TypeBranch", MatchType.Exact, ddlBranchType.SelectedValue))
        End If

        If Not (ddlstatus.SelectedValue = "") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "Status", MatchType.Exact, ddlstatus.SelectedValue))
        End If

        If txtBranchCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "DealerBranchCode", MatchType.Partial, txtBranchCode.Text.Trim()))
        End If

        If cbSalesUnit.Checked Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "SalesUnitFlag", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        End If
        If cbService.Checked Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "ServiceFlag", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        End If
        If cbSparePart.Checked Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "SparepartFlag", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        End If

        _sessHelper.SetSession("CriteriaFrmDealerBranchList2", criterias)
    End Sub

    Private Sub BindDatagrid(ByVal indexPage)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) And Not IsNothing(CType(_sessHelper.GetSession("CriteriaFrmDealerBranchList2"), CriteriaComposite)) Then
            dtgDealerList.DataSource = New DealerBranchFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession("CriteriaFrmDealerBranchList2"), CriteriaComposite), _
              indexPage + 1, dtgDealerList.PageSize, totalRow, _
              CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgDealerList.VirtualItemCount = totalRow
            dtgDealerList.DataBind()

        Else
            dtgDealerList.DataSource = New ArrayList
            dtgDealerList.DataBind()
        End If
    End Sub

    Private Sub BoundRowItems(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        Dim objDealer As DealerBranch = CType(CType(dtgDealerList.DataSource, ArrayList)(e.Item.ItemIndex), DealerBranch)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgDealerList.CurrentPageIndex * dtgDealerList.PageSize)
        End If

        If Not IsNothing(objDealer.MainArea) Then
            CType(e.Item.FindControl("lblMainArea"), Label).Text = objDealer.MainArea.Description
        End If
        If Not IsNothing(objDealer.City) Then
            CType(e.Item.FindControl("lblCity"), Label).Text = objDealer.City.CityName
        End If
        Dim lblStatus As Label = e.Item.FindControl("lblStatus")
        lblStatus.Text = IIf(objDealer.Status = CInt(EnumDealerBranchStatus.DealerBranchStatus.Aktive).ToString(), "Aktif", "Non Aktif")
    End Sub

    Private Sub BindDdlStatus()
        Dim listStatus As New EnumDealerBranchStatus
        Dim al As ArrayList = listStatus.RetrieveStatus

        For Each item As EnumDealerBranch In al
            ddlstatus.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
        Next
        ddlstatus.Items.Insert(0, New ListItem("Silahkan Pilih", ""))

    End Sub

    Private Sub BindDdlType()
        Dim listStatus As New EnumDealerBranchType
        Dim al As ArrayList = listStatus.RetrieveStatus

        For Each item As EnumDealerBranch In al

            ddlBranchType.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
        Next
        ddlBranchType.Items.Insert(0, New ListItem("Silahkan Pilih", ""))
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            BindDdlStatus()
            BindDdlType()
            ViewState("CurrentSortColumn") = "Dealer.DealerCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            'GeneralScript.GetPopUpEventReference("../PopUp/PopUpSelectingDealer.aspx", "", 500, 760, "DealerSelection")
            'lblPopUpDealer.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpSelectingDealer.aspx", "", 500, 760, "DealerSelection")
            lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            If Not IsNothing(Request.QueryString("From")) AndAlso Not IsNothing(_sessHelper.GetSession("CriteriaFrmDealerBranchList2")) Then
                dtgDealerList.CurrentPageIndex = 0
                BindDatagrid(dtgDealerList.CurrentPageIndex)
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        CreateCriteriaSearch()
        dtgDealerList.CurrentPageIndex = 0
        BindDatagrid(dtgDealerList.CurrentPageIndex)
    End Sub

    Private Sub dtgDealerList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgDealerList.ItemCommand
        If (e.CommandName = "View") Then
            Dim id As String = e.Item.Cells(0).Text
            Response.Redirect("../General/FrmDealerBranchEntry.aspx?ID=" + e.Item.Cells(0).Text.Trim() + "&Proses=View")
        End If

        If (e.CommandName = "Edit") Then
            Dim id As String = e.Item.Cells(0).Text
            Response.Redirect("../General/FrmDealerBranchEntry.aspx?ID=" + e.Item.Cells(0).Text.Trim() + "&Proses=edit")
        End If
    End Sub

    Private Sub dtgDealerList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgDealerList.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            BoundRowItems(e)
        End If
    End Sub

    Private Sub dtgDealerList_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgDealerList.PageIndexChanged
        dtgDealerList.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgDealerList.CurrentPageIndex)
    End Sub
End Class
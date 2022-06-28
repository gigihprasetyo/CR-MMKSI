#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports Ktb.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

Public Class frmSummarySPPO
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents icPODateStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPODateEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlProcessCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgSPPO As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Private sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents cbTanggalPesanan As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cbTanggalPembuatan As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icStartTanggalPembuatan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndTanggalPembuatan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents lblTotalAmount As System.Web.UI.WebControls.Label
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents lblTotalQuantity As System.Web.UI.WebControls.Label
    Protected WithEvents lblGrandTotal As System.Web.UI.WebControls.Label


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private _sessHelper As SessionHelper = New SessionHelper
    Private objDealer As Dealer
#End Region

#Region "Custom Method"

    Private Sub checkDealer()
        'If Session("DEALER") Is Nothing Then
        '    Response.Redirect("..\SessionExpired.htm")
        'End If
    End Sub

    Private Sub BindOrderType()
        ddlOrderType.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For Each liOrderType As ListItem In LookUp.ArraySPOrderTypeKTBDealer
            ddlOrderType.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
        Next
       
    End Sub

    Private Sub BindProccessCode()
        'ddlProcessCode.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        'For Each liOrderType As ListItem In LookUp.ArraySPPOProccessCode
        '    If (liOrderType.Value <> "" AndAlso liOrderType.Value <> "C") Then
        '        ddlProcessCode.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
        '    End If
        'Next
        'ddlProcessCode.ClearSelection()

        Dim arrFilter As New ArrayList
        Dim arrTmp As New ArrayList
        arrTmp = LookUp.RetriveSPStatus
        For Each item As LookUp.enumStatStr In arrTmp
            If (item.ValStatus <> "" AndAlso item.ValStatus <> "C") Then
                arrFilter.Add(item)
            End If
        Next

        ddlProcessCode.DataSource = arrFilter
        ddlProcessCode.DataTextField = "NameStatus"
        ddlProcessCode.DataValueField = "ValStatus"
        ddlProcessCode.DataBind()
        ddlProcessCode.Items.Insert(0, New ListItem("Silahkan Pilih ", "-1"))
        ddlProcessCode.SelectedIndex = -1
    End Sub

    Private Sub BindHeader()
        BindOrderType()
        BindProccessCode()
    End Sub

    Private Sub BindTodtgSPPO(ByVal pageIndex As Integer)
        checkDealer()
        Dim ListSPPO As New ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SparePartPOSummary), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_SparePartPOSummary), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If
        If txtDealerCode.Text <> String.Empty Then
            Dim strDealer As String = "('" & txtDealerCode.Text.Replace(";", "','") & "')"
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_SparePartPOSummary), "DealerCode", MatchType.InSet, strDealer))

        End If
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_SparePartPOSummary), "ProcessCode", MatchType.No, ""))

        If cbTanggalPesanan.Checked = True Then
            If icPODateStart.Value <= icPODateEnd.Value Then
                criterias.opAnd(New Criteria(GetType(V_SparePartPOSummary), "PODate", MatchType.GreaterOrEqual, Format(icPODateStart.Value, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(V_SparePartPOSummary), "PODate", MatchType.LesserOrEqual, Format(icPODateEnd.Value, "yyyy/MM/dd")))
            Else
                criterias.opAnd(New Criteria(GetType(V_SparePartPOSummary), "PODate", MatchType.GreaterOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(V_SparePartPOSummary), "PODate", MatchType.LesserOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                icPODateStart.Value = Date.Now
                icPODateEnd.Value = Date.Now
            End If
        End If

        If cbTanggalPembuatan.Checked = True Then
            If icStartTanggalPembuatan.Value <= icEndTanggalPembuatan.Value Then
                criterias.opAnd(New Criteria(GetType(V_SparePartPOSummary), "CreatedTime", MatchType.GreaterOrEqual, Format(icStartTanggalPembuatan.Value, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(V_SparePartPOSummary), "CreatedTime", MatchType.LesserOrEqual, Format(icEndTanggalPembuatan.Value, "yyyy/MM/dd")))
            Else
                criterias.opAnd(New Criteria(GetType(V_SparePartPOSummary), "CreatedTime", MatchType.GreaterOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(V_SparePartPOSummary), "CreatedTime", MatchType.LesserOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                icStartTanggalPembuatan.Value = Date.Now
                icEndTanggalPembuatan.Value = Date.Now
            End If
        End If


        If ddlOrderType.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(V_SparePartPOSummary), "OrderType", MatchType.Exact, ddlOrderType.SelectedValue))
        If ddlProcessCode.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(V_SparePartPOSummary), "ProcessCode", MatchType.Exact, ddlProcessCode.SelectedValue))
        ListSPPO = New SparePartPOFacade(User).RetrieveActiveListSummaryByCriteria(criterias, pageIndex, dtgSPPO.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        sessHelper.SetSession("totalRow", totalRow)
        If ListSPPO.Count > 0 Then
            dtgSPPO.DataSource = ListSPPO
            dtgSPPO.VirtualItemCount = totalRow
        Else
            dtgSPPO.DataSource = New ArrayList
            dtgSPPO.VirtualItemCount = 0

            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Pesanan Spare Part"))
            End If
        End If
        sessHelper.SetSession("sesSPPO", ListSPPO)
        sessHelper.SetSession("MONITORINGPOTODOWNLOAD", criterias)
        dtgSPPO.DataBind()
        CalculateTotalAmount(criterias)
    End Sub

    Private Sub CalculateTotalAmount(ByVal criterias As CriteriaComposite)
        Dim objFacade As New SparePartPOFacade(User)
        'Dim POColl As ArrayList = New SparePartPOFacade(User).RetrieveSummary(criterias)
        Dim total As Int64 = 0
        Dim qty As Int64 = 0
        Dim itemCount As Int64 = 0
        'For Each item As V_SparePartPOSummary In POColl
        '    total += item.ItemAmount
        '    qty += item.ItemCount
        '    'itemCount += item.SparePartPODetails.Count
        '    itemCount += item.ItemCount
        'Next

        Dim agg As Aggregate = New Aggregate(GetType(V_SparePartPOSummary), "ItemAmount", AggregateType.Sum)
        Dim aggItemCount As Aggregate = New Aggregate(GetType(V_SparePartPOSummary), "ItemCount", AggregateType.Sum)
        Dim aggCount As Aggregate = New Aggregate(GetType(V_SparePartPOSummary), "ID", AggregateType.Count)




        total = objFacade.GetAggregateResult(agg, criterias)
        qty = objFacade.GetAggregateResult(aggItemCount, criterias)
        itemCount = qty

        'Dim DataCount As Int64 = objFacade.GetAggregateResult(aggCount, criterias)
        Dim DataCount As Int64 = Val(sessHelper.GetSession("totalRow"))

        lblTotalAmount.Text = "Total Amount : Rp." & FormatNumber(total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        'lblTotalQuantity.Text = "Total Qty : " & FormatNumber(qty, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblTotalQuantity.Text = "Total Order : " & FormatNumber(DataCount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblGrandTotal.Text = "Grand Total Item : " & FormatNumber(itemCount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

    End Sub

    'Private Function GetCanceledItem() As ArrayList

    '    Dim objSparePartPO As SparePartPO
    '    Dim arlItemCanceled As ArrayList = New ArrayList
    '    dtgSPPO.DataSource = CType(Session("sesSPPO"), ArrayList)
    '    For Each dgItem As DataGridItem In dtgSPPO.Items
    '        Dim intItemIndex As Integer = dgItem.ItemIndex
    '        objSparePartPO = New SparePartPOFacade(User).Retrieve(CType(dgItem.Cells(0).Text, Integer)) 'CType(CType(dtgSPPO.DataSource, ArrayList)(intItemIndex), SparePartPO)
    '        If CType(dgItem.Cells(8).FindControl("chkCanceled"), CheckBox).Checked Then
    '            If objSparePartPO.ProcessCode = "S" Then ' AndAlso objSparePartPO.CancelRequestBy <> String.Empty AndAlso Left(objSparePartPO.CancelRequestBy, 1) <> "-" Then
    '                arlItemCanceled.Add(objSparePartPO)
    '            End If
    '        End If
    '    Next
    '    If arlItemCanceled.Count > 0 Then
    '        Return arlItemCanceled
    '    Else
    '        Return Nothing
    '    End If

    'End Function

#End Region

#Region "EventHandler"
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgSPPO.CurrentPageIndex = 0
        BindTodtgSPPO(1)
    End Sub


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            If Not SecurityProvider.Authorize(Context.User, SR.ENHMonitorPOKTB_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Pemesanan")
            End If
            ViewState("currSortColumn") = ""
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            cbTanggalPesanan.Checked = True
            BindHeader()
            BindTodtgSPPO(1)
            lblSearchDealer.Attributes("onClick") = "ShowPPDealerSelection();"
        End If
        ActivateUserPrivilege()
    End Sub


    Private Sub dtgSPPO_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSPPO.PageIndexChanged
        dtgSPPO.CurrentPageIndex = e.NewPageIndex
        BindTodtgSPPO(e.NewPageIndex + 1)
    End Sub



    Private Sub dtgSPPO_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSPPO.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgSPPO.PageSize * dtgSPPO.CurrentPageIndex)).ToString
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

        End If
    End Sub

    Private Sub dtgSPPO_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSPPO.SortCommand
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
        dtgSPPO.CurrentPageIndex = 0
        BindTodtgSPPO(dtgSPPO.CurrentPageIndex + 1)
    End Sub

    Private Sub ActivateUserPrivilege()

    End Sub

#End Region


    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
      
        Response.Redirect("FrmSparepartSummaryDownload.aspx")
    End Sub
End Class
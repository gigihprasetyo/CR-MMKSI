Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports System.IO
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.Helper

Public Class FrmFieldFixRequestPartList
    Inherits System.Web.UI.Page

    Private sessionHelper As New SessionHelper
    Private objDealer As Dealer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objDealer = Session("DEALER")
        lblDealerCode.Text = objDealer.DealerCode
        lblDealerName.Text = objDealer.DealerName

        If Not IsPostBack Then
            ViewState("EditID") = Nothing
            ViewState("CurrentSortColumn") = "PoNumber"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            'lblSearchPart.Attributes("onclick") = "ShowPPSparePartSelection();"

            Dim id As String = Request.QueryString("ID")
            Dim mode As String = Request.QueryString("MODE")
            Dim status As String = Request.QueryString("STATUS")
            Dim pono As String = Request.QueryString("PONO")
            Dim dtstart As String = Request.QueryString("DTSTART")
            Dim dtend As String = Request.QueryString("DTEND")
            If Not String.IsNullorEmpty(id) And Not String.IsNullorEmpty(mode) Then
                ViewState("MODE") = mode
                LoadData(pono, dtstart, dtend, status)
            Else
                bindDDLStatus(ddlStatus)
                ClearAll()
                bindGrid(0)
            End If

        End If
    End Sub

    Private Sub LoadData(ByVal pono As String, ByVal dtstart As String, ByVal dtend As String, ByVal status As String)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartForecastHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SparePartForecastHeader), "Dealer.DealerCode", MatchType.Exact, lblDealerCode.Text.Trim()))
        If pono <> "" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastHeader), "PoNumber", MatchType.Partial, pono))
        End If

        Dim StartProses As New DateTime(CType(dtstart, Date).Year, CType(dtstart, Date).Month, CType(dtstart, Date).Day, 0, 0, 0)
        Dim EndProses As New DateTime(CType(dtend, Date).Year, CType(dtend, Date).Month, CType(dtend, Date).Day, 23, 59, 59)
        criterias.opAnd(New Criteria(GetType(SparePartForecastHeader), "PoDate", MatchType.GreaterOrEqual, Format(StartProses, "yyyy-MM-dd HH:mm:ss")))
        criterias.opAnd(New Criteria(GetType(SparePartForecastHeader), "PoDate", MatchType.LesserOrEqual, Format(EndProses, "yyyy-MM-dd HH:mm:ss")))

        icStartDateReq.Value = CType(dtstart, Date)
        icEndDateReq.Value = CType(dtend, Date)

        bindDDLStatus(ddlStatus)
        If status <> "-1" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastHeader), "Status", MatchType.Exact, status))
            ddlStatus.SelectedValue = status
        End If

        Dim totalRow As Integer = 0
        '-- Retrieve recordset
        Dim arrSPFCO As ArrayList = New SparePartForecastHeaderFacade(User).RetrieveActiveList(criterias, 1, dtgPOPart.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        If arrSPFCO.Count = 0 Then
            '-- Bind and display
            dtgPOPart.DataSource = New ArrayList
            'If IsPostBack Then
            '    MessageBox.Show(SR.DataNotFound("Field Fix"))
            'End If
        Else
            '-- Bind and display
            dtgPOPart.DataSource = arrSPFCO
        End If

        dtgPOPart.VirtualItemCount = totalRow
        dtgPOPart.DataBind()

    End Sub

    Private Sub bindDDLStatus(ByVal varDDLStatus As DropDownList)
        varDDLStatus.Items.Clear()
        varDDLStatus.DataSource = EnumFieldFixPartOrderStatus.RetrieveFieldFixPartOrderStatus()
        varDDLStatus.DataValueField = "Code"
        varDDLStatus.DataTextField = "Desc"
        varDDLStatus.DataBind()
        varDDLStatus.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        varDDLStatus.SelectedIndex = 0
    End Sub

    Private Sub bindGrid(ByVal currentPageIndex As Integer)
        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartForecastHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)

        Dim totalRow As Integer = 0
        '-- Retrieve recordset
        Dim arrSPFCO As ArrayList = New SparePartForecastHeaderFacade(User).RetrieveActiveList(criterias, currentPageIndex, dtgPOPart.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        If arrSPFCO.Count = 0 Then
            '-- Bind and display
            dtgPOPart.DataSource = New ArrayList
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Field Fix"))
            End If
        Else
            '-- Bind and display
            dtgPOPart.DataSource = arrSPFCO

        End If

        dtgPOPart.VirtualItemCount = totalRow
        dtgPOPart.DataBind()
    End Sub

    Private Sub CreateCriteria(ByRef criterias As CriteriaComposite)
        If lblDealerCode.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastHeader), "Dealer.DealerCode", MatchType.Exact, lblDealerCode.Text.Trim()))
        End If
        If txtPoNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastHeader), "PoNumber", MatchType.Partial, txtPoNo.Text.Trim()))
        End If
        'If txtPartNo.Text.Trim() <> "" Then
        '    criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "SparePartForecastMaster.SparePartMaster.PartNumber", MatchType.InSet, "('" & txtPartNo.Text.Trim().Replace(";", "','") & "')"))
        'End If

        If chkTglReq.Checked Then
            Dim StartProses As New DateTime(CInt(icStartDateReq.Value.Year), CInt(icStartDateReq.Value.Month), CInt(icStartDateReq.Value.Day), 0, 0, 0)
            Dim EndProses As New DateTime(CInt(icEndDateReq.Value.Year), CInt(icEndDateReq.Value.Month), CInt(icEndDateReq.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(SparePartForecastHeader), "PoDate", MatchType.GreaterOrEqual, Format(StartProses, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(SparePartForecastHeader), "PoDate", MatchType.LesserOrEqual, Format(EndProses, "yyyy-MM-dd HH:mm:ss")))
        End If
        If ddlStatus.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastHeader), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If
    End Sub

    Private Sub dtgPOPart_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgPOPart.ItemCommand
        If e.CommandName = "Edit" Then
            Response.Redirect("../Service/FrmFieldFixRequestPart.aspx?MODE=Edit&ID=" + e.Item.Cells(0).Text & "&PONO=" & txtPoNo.Text.Trim() _
                              & "&STATUS=" & ddlStatus.SelectedValue & "&DTSTART=" & icStartDateReq.Value & "&DTEND=" & icEndDateReq.Value)
        ElseIf e.CommandName = "View" Then
            Dim dtStr As String = icStartDateReq.Text
            Response.Redirect("../Service/FrmFieldFixRequestPartDetailList.aspx?MODE=View&ID=" & e.Item.Cells(0).Text & "&PONO=" & txtPoNo.Text.Trim() _
                              & "&STATUS=" & ddlStatus.SelectedValue & "&DTSTART=" & icStartDateReq.Value & "&DTEND=" & icEndDateReq.Value)
        ElseIf e.CommandName = "Cancel" Then
            dtgPOPart.EditItemIndex = -1
            bindGrid(dtgPOPart.CurrentPageIndex)
        End If

    End Sub

    Private Sub dtgPOPart_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgPOPart.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim rowValue As SparePartForecastHeader = CType(e.Item.DataItem, SparePartForecastHeader)
            CType(e.Item.FindControl("lblNo"), Label).Text = (dtgPOPart.CurrentPageIndex * dtgPOPart.PageSize + e.Item.ItemIndex + 1).ToString()
            CType(e.Item.FindControl("lbliPONo"), Label).Text = rowValue.PoNumber
            CType(e.Item.FindControl("lbliKdDealer"), Label).Text = rowValue.Dealer.DealerCode
            CType(e.Item.FindControl("lbliRequestDate"), Label).Text = rowValue.PoDate.ToString("dd/MM/yyyy")
            CType(e.Item.FindControl("lbliStatus"), Label).Text = EnumFieldFixPartOrderStatus.GetStringValue(rowValue.Status)
            
            If rowValue.Status <> CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Baru, Short) Then
                CType(e.Item.FindControl("lnkbtnEdit"), LinkButton).Visible = False
            End If

        ElseIf e.Item.ItemType = ListItemType.EditItem Then
            Dim rowValue As SparePartForecastDetail = CType(e.Item.DataItem, SparePartForecastDetail)
            'CType(e.Item.FindControl("lblNoEdit"), Label).Text = (dtgPOPart.CurrentPageIndex * dtgPOPart.PageSize + e.Item.ItemIndex + 1).ToString()
            'CType(e.Item.FindControl("lbliKdDealerEdit"), Label).Text = rowValue.SparePartForecastHeader.Dealer.DealerCode
            'CType(e.Item.FindControl("lbliPartNoEdit"), Label).Text = rowValue.SparePartForecastMaster.SparePartMaster.PartNumber
            'CType(e.Item.FindControl("lbliPartNameEdit"), Label).Text = rowValue.SparePartForecastMaster.SparePartMaster.PartName
            'CType(e.Item.FindControl("lbliNoBulletinEdit"), Label).Text = rowValue.SparePartForecastMaster.NoBulletinService
            'CType(e.Item.FindControl("lblRequestDateEdit"), Label).Text = rowValue.RequestDate.ToString("dd/MM/yyyy")
            'CType(e.Item.FindControl("lblRequestAmountEdit"), Label).Text = rowValue.RequestQty.ToString("N0")
            'CType(e.Item.FindControl("lbliStatusEdit"), Label).Text = EnumFieldFixPartOrderStatus.GetStringValue(rowValue.Status)
            'CType(e.Item.FindControl("txtNoAWBEdit"), TextBox).Text = rowValue.NoAWB


        End If
    End Sub

    Private Sub ClearAll()
        ViewState("EditID") = Nothing
        'txtPartNo.Text = ""
        icStartDateReq.Value = Date.Now.Date
        icEndDateReq.Value = Date.Now.Date
        ddlStatus.SelectedIndex = 0
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If chkTglReq.Checked = False Then
            MessageBox.Show("Tanggal permintaan harus dipilih")
            Return
        Else
            bindGrid(0)
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearAll()
        dtgPOPart.DataSource = New ArrayList
        dtgPOPart.DataBind()
    End Sub

    Private Function ToDate(ByVal strdate As String) As Date
        Return CType(strdate.Substring(0, 2).ToString & "/" & strdate.Substring(2, 2) & "/" & strdate.Substring(4, 4), Date)
        'Return dt
    End Function

End Class
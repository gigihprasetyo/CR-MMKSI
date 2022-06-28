Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports System.IO
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.Helper


Public Class FrmFieldFixRequestPartDetailList
    Inherits System.Web.UI.Page
    Private sessionHelper As New SessionHelper
    Private objDealer As Dealer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objDealer = Session("DEALER")
        'lblDealerCode.Text = objDealer.DealerCode
        'lblDealerName.Text = objDealer.DealerName
        Dim id As String = Request.QueryString("ID")
        Dim mode As String = Request.QueryString("MODE")
        Dim status As String = Request.QueryString("STATUS")
        Dim pono As String = Request.QueryString("PONO")
        Dim dtstart As String = Request.QueryString("DTSTART")
        Dim dtend As String = Request.QueryString("DTEND")
        hdID.Value = id
        HdPo.Value = pono
        HDStatus.Value = status
        HdDtStart.Value = dtstart
        HdDtEnd.Value = dtend

        If Not IsPostBack Then
            ViewState("EditID") = Nothing
            ViewState("CurrentSortColumn") = "RequestDate"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

            lblSearchPart.Attributes("onclick") = "ShowPPSparePartSelection();"

            bindDDLStatus(ddlStatus)
            ClearAll()
            LoadData(ID)
            'bindGrid(0)
        End If
    End Sub

    Private Sub LoadData(ByVal id As Integer)
        'Dim objDetail As SparePartForecastDetail = New SparePartForecastDetailFacade(User).Retrieve(id)
        Dim objHeader As SparePartForecastHeader = New SparePartForecastHeaderFacade(User).Retrieve(id)
        If Not IsNothing(objHeader) AndAlso objHeader.ID.ToString() <> "" Then
            hdID.Value = objHeader.ID
            lblDealerCode.Text = objHeader.Dealer.DealerCode
            lblDealerName.Text = objHeader.Dealer.DealerName
            lblPoNo.Text = objHeader.PoNumber

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastDetail), "SparePartForecastHeader.ID", MatchType.Exact, objHeader.ID))

            Dim arrSFDetail As ArrayList = New SparePartForecastDetailFacade(User).Retrieve(criterias)
            If Not IsNothing(arrSFDetail) AndAlso arrSFDetail.Count > 0 Then
                dtgPOPartDetail.DataSource = arrSFDetail
                dtgPOPartDetail.CurrentPageIndex = 0
                dtgPOPartDetail.DataBind()
                btnKembali.Visible = True
                sessionHelper.SetSession("DataParts", arrSFDetail)
            End If
            'Else
            '    sessionHelper.SetSession("addNewParts", _arlPartMaster)
            '    'DataGridInitialization(True)
            '    dtgPOPart.DataSource = _arlPartMaster
            '    dtgPOPart.DataBind()
        End If

    End Sub

    Private Sub bindDDLStatus(ByVal varDDLStatus As DropDownList)
        varDDLStatus.Items.Clear()
        varDDLStatus.DataSource = EnumFieldFixPartOrderStatus.RetrieveFieldFixPartOrderDetailStatus()
        varDDLStatus.DataValueField = "Code"
        varDDLStatus.DataTextField = "Desc"
        varDDLStatus.DataBind()
        varDDLStatus.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        varDDLStatus.SelectedIndex = 0
    End Sub

    Private Sub bindGrid(ByVal currentPageIndex As Integer)
        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartForecastDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)

        Dim totalRow As Integer = 0
        '-- Retrieve recordset
        Dim arrSPFCO As ArrayList = New SparePartForecastDetailFacade(User).RetrieveActiveList(criterias, currentPageIndex, dtgPOPartDetail.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        If arrSPFCO.Count = 0 Then
            '-- Bind and display
            dtgPOPartDetail.DataSource = New ArrayList
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Field Fix"))
            End If
        Else
            '-- Bind and display
            dtgPOPartDetail.DataSource = arrSPFCO

        End If
        dtgPOPartDetail.VirtualItemCount = totalRow
        dtgPOPartDetail.DataBind()
    End Sub

    Private Sub CreateCriteria(ByRef criterias As CriteriaComposite)
        If lblDealerCode.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "SparePartForecastHeader.Dealer.DealerCode", MatchType.Exact, lblDealerCode.Text.Trim()))
        End If
        If lblPoNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "SparePartForecastHeader.PoNumber", MatchType.Partial, lblPoNo.Text.Trim()))
        End If

        If txtPartNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "SparePartForecastMaster.SparePartMaster.PartNumber", MatchType.InSet, "('" & txtPartNo.Text.Trim().Replace(";", "','") & "')"))
        End If

        'If chkTglReq.Checked Then
        '    Dim StartProses As New DateTime(CInt(icStartDateReq.Value.Year), CInt(icStartDateReq.Value.Month), CInt(icStartDateReq.Value.Day), 0, 0, 0)
        '    Dim EndProses As New DateTime(CInt(icEndDateReq.Value.Year), CInt(icEndDateReq.Value.Month), CInt(icEndDateReq.Value.Day), 23, 59, 59)
        '    criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "RequestDate", MatchType.GreaterOrEqual, Format(StartProses, "yyyy-MM-dd HH:mm:ss")))
        '    criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "RequestDate", MatchType.LesserOrEqual, Format(EndProses, "yyyy-MM-dd HH:mm:ss")))
        'End If
        If ddlStatus.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If
    End Sub

    Private Sub dtgPOPart_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgPOPartDetail.ItemCommand
        If e.CommandName = "Edit" Then
            Response.Redirect("../Service/FrmFieldFixRequestPart.aspx?MODE=Edit&ID=" + e.Item.Cells(0).Text)
        ElseIf e.CommandName = "Cancel" Then
            dtgPOPartDetail.EditItemIndex = -1
            bindGrid(dtgPOPartDetail.CurrentPageIndex)
        End If

    End Sub

    Private Sub dtgPOPart_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgPOPartDetail.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim rowValue As SparePartForecastDetail = CType(e.Item.DataItem, SparePartForecastDetail)
            CType(e.Item.FindControl("lblNo"), Label).Text = (dtgPOPartDetail.CurrentPageIndex * dtgPOPartDetail.PageSize + e.Item.ItemIndex + 1).ToString()
            'CType(e.Item.FindControl("lbliPONo"), Label).Text = rowValue.SparePartForecastHeader.PoNumber
            'CType(e.Item.FindControl("lbliKdDealer"), Label).Text = rowValue.SparePartForecastHeader.Dealer.DealerCode
            CType(e.Item.FindControl("lbliPartNo"), Label).Text = rowValue.SparePartForecastMaster.SparePartMaster.PartNumber
            CType(e.Item.FindControl("lbliPartName"), Label).Text = rowValue.SparePartForecastMaster.SparePartMaster.PartName
            CType(e.Item.FindControl("lbliNoBulletin"), Label).Text = rowValue.SparePartForecastMaster.NoBulletinService
            CType(e.Item.FindControl("lbliRequestDate"), Label).Text = rowValue.RequestDate.ToString("dd/MM/yyyy")
            CType(e.Item.FindControl("lbliRequestQty"), Label).Text = rowValue.RequestQty.ToString("N0")
            CType(e.Item.FindControl("lbliApprovedQty"), Label).Text = rowValue.ApprovedQty.ToString("N0")
            CType(e.Item.FindControl("lbliStatus"), Label).Text = EnumFieldFixPartOrderStatus.GetStringValue(rowValue.Status)
            If rowValue.SendDate.Year > 2000 Then
                CType(e.Item.FindControl("lbliSendDate"), Label).Text = rowValue.SendDate.ToString("dd/MM/yyyy")
            End If
            CType(e.Item.FindControl("lbliNoAWB"), Label).Text = rowValue.NoAWB
            'If rowValue.Status <> CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Baru, Short) Then
            '    CType(e.Item.FindControl("lnkbtnEdit"), LinkButton).Visible = False
            'End If

        End If
    End Sub

    Private Sub ClearAll()
        ViewState("EditID") = Nothing
        txtPartNo.Text = ""
        'icStartDateReq.Value = Date.Now.Date
        'icEndDateReq.Value = Date.Now.Date
        ddlStatus.SelectedIndex = 0
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        'If chkTglReq.Checked = False Then
        '    MessageBox.Show("Tanggal permintaan harus dipilih")
        '    Return
        'Else
        '    bindGrid(0)
        'End If
        bindGrid(0)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearAll()
        dtgPOPartDetail.DataSource = New ArrayList
        dtgPOPartDetail.DataBind()
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("../Service/FrmFieldFixRequestPartList.aspx?MODE=NEW&ID=" & hdID.Value & "&PONO=" & HdPo.Value _
                              & "&STATUS=" & HDStatus.Value & "&DTSTART=" & HdDtStart.Value & "&DTEND=" & HdDtEnd.Value)
    End Sub
End Class
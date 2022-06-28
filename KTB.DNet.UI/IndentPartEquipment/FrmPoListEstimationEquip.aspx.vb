Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.IndentPart
Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports System.IO
Imports System.Text
Imports System.Web.UI.WebControls
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessValidation.Helpers

Public Class FrmPoListEstimationEquip
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpPoNo As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoPO As System.Web.UI.WebControls.TextBox
    Protected WithEvents lstStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents ddlPaymentType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents lblGrandTotal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalAmount As System.Web.UI.WebControls.Label
    Protected WithEvents txtPONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEstimationNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpEstNo As System.Web.UI.WebControls.Label
    Protected WithEvents dtgEquipPO As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnProcess As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents hdnRejectDesc As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents icPODateUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPODateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblTotalTagihan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalOrder As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "var"

    Dim oDealer As Dealer
    Private _sesshelper As New SessionHelper
    Private equippof As v_EquipPOFacade
    Private _szSsDealer As String = "DEALER"
    Private _szSsListEqpPo As String = "ListEquipPo"
    Private _szSsCrits As String = "crits"
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
    Const TEMP_EMAIL_REJECT = "../IndentPartEquipment/EmailTemplateRejectSPPO.htm"
    Const TEMP_EMAIL_APPROVED = "../IndentPartEquipment/EmailTemplateApprovedSPPO.htm"

#End Region

#Region "event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        equippof = New v_EquipPOFacade(User)

        oDealer = CType(_sesshelper.GetSession(_szSsDealer), Dealer)
        If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            InitiateAuthorization()
            txtDealerCode.Text = oDealer.DealerCode
            txtDealerCode.Enabled = False
            txtDealerCode.BorderStyle = BorderStyle.None
            lblSearchDealer.Visible = False
            lblSearchDealer.Visible = False
            lblPopUpEstNo.Attributes("onclick") = "showPopUp('../PopUp/PopUpEstimationEquip.aspx?DealerID=" & oDealer.ID & "', '', 500, 600,POSelection);"
            lblPopUpPoNo.Attributes("onclick") = "showPopUp('../PopUp/PopUpIndentPart.aspx?DealerID=" & oDealer.ID & "&MaterialType=" & CInt(EnumMaterialType.MaterialType.Equipment) & "', '', 500, 600,PONOSelection);"
        ElseIf (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            InitiateAuthorization()
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            lblPopUpEstNo.Attributes("onclick") = "showPopUp('../PopUp/PopUpEstimationEquip.aspx', '', 500, 600,POSelection);"
            lblPopUpPoNo.Attributes("onclick") = "showPopUp('../PopUp/PopUpIndentPart.aspx?MaterialType=" & CInt(EnumMaterialType.MaterialType.Equipment) & "','', 500, 600,PONOSelection);"
            txtDealerCode.Enabled = True
        End If

        If IsPostBack Then Exit Sub

        CommonFunction.BindFromEnum("IndentPartPaymentType", ddlPaymentType, Me.User, False, "NameType", "ValType")
        'Start  :RemainModule-IndentPart:dna-20091206
        'If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
        '    EnumIndentPartStatus.RetrieveStatusForDealer(lstStatus)
        '    EnumIndentPartStatus.RetrieveStatusUpdateForDealer(ddlStatus)
        'ElseIf (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
        '    EnumIndentPartStatus.RetrieveStatusForKTB(lstStatus, True)
        '    EnumIndentPartStatus.RetrieveStatusUpdateForKTB(ddlStatus, True)
        'End If
        BindStatus()
        'Start  :RemainModule-IndentPart:dna-20091206

        If (Not IsNothing(Request.QueryString("isBack"))) Then
            BindToGrid(dtgEquipPO.CurrentPageIndex)
        Else
            ViewState("currSortColumn") = "Status"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            Dim arlEqpPo As ArrayList = CType(_sesshelper.GetSession(_szSsListEqpPo), ArrayList)
            dtgEquipPO.DataSource = arlEqpPo
            dtgEquipPO.DataBind()
        End If
    End Sub

    Private Sub dtgEquipPO_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEquipPO.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgEquipPO.PageSize * dtgEquipPO.CurrentPageIndex)).ToString
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim obj As v_EquipPO = CType(e.Item.DataItem, v_EquipPO)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lbtnPrint As LinkButton = e.Item.FindControl("lbtnPrint")
            Dim lblDeadlineKwitansi As Label = e.Item.FindControl("lblDeadlineKwitansi")
            Dim lblTotalTagihan As Label = e.Item.FindControl("lblTotalTagihan")

            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = False
            'lblStatus.Text = obj.StatusDesc
            'If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            '    If obj.Status = EnumIndentPartStatus.IndentPartStatus.SENT Then
            '        CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = True
            '        CType(e.Item.FindControl("lbtnDetail"), LinkButton).Visible = False
            '        lblStatus.Text = EnumIndentPartStatus.BARU
            '    Else
            '        CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
            '        CType(e.Item.FindControl("lbtnDetail"), LinkButton).Visible = True
            '    End If
            '    'Start  :RemainModule-IndentPart:dna:20091204:update wrong status
            '    Dim objIPHFac As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)
            '    Dim objIPH As IndentPartHeader = objIPHFac.Retrieve(obj.ID)
            '    lblStatus.Text = EnumIndentPartStatus.GetIndentPartStatusKTBString(objIPH.StatusKTB)
            '    'End    :RemainModule-IndentPart:dna:20091204:update wrong status
            'ElseIf oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            '    If obj.Status = EnumIndentPartStatus.IndentPartStatus.BARU Then
            '        CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = True
            '        CType(e.Item.FindControl("lbtnDetail"), LinkButton).Visible = False
            '    Else
            '        CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
            '        CType(e.Item.FindControl("lbtnDetail"), LinkButton).Visible = True
            '        If (obj.Status = EnumIndentPartStatus.IndentPartStatus.ORDER_TO_VENDOR) Then
            '            lblStatus.Text = EnumIndentPartStatus.PROSSES
            '        ElseIf (obj.Status = EnumIndentPartStatus.IndentPartStatus.RILIS) Then
            '            lblStatus.Text = EnumIndentPartStatus.RILIS
            '            Dim lblPrint As Label = CType(e.Item.FindControl("lblPrint"), Label)
            '            lblPrint.Visible = True
            '            lblPrint.Attributes.Add("onclick", String.Format("javascript:PopUpSPPOPrintBill('{0}')", obj.ID))
            '        End If
            '    End If
            'End If

            If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                lblStatus.Text = EnumIndentPartEquipStatus.GetStatusKTBDesc(obj.StatusKTB)
                If obj.Status = EnumIndentPartEquipStatus.EnumStatusKTB.Baru Then
                    CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
                    CType(e.Item.FindControl("lbtnDetail"), LinkButton).Visible = True
                End If
                lbtnPrint.Visible = False
            Else
                lblStatus.Text = EnumIndentPartEquipStatus.GetStatusDealerDesc(obj.Status)
                If obj.Status = EnumIndentPartEquipStatus.EnumStatusDealer.Kirim Then
                    CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
                    CType(e.Item.FindControl("lbtnDetail"), LinkButton).Visible = True
                End If

                If obj.Status = EnumIndentPartEquipStatus.EnumStatusDealer.Expired Then
                    CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = True
                End If

                'Start  :CR:dna:20091216:add print kwitansi
                If obj.PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_B And obj.Status = EnumIndentPartEquipStatus.EnumStatusDealer.Proses Then ' obj.PaymentTypeDesc.ToUpper = "Deposit C".ToUpper And obj.StatusDesc.ToUpper = "Rilis".ToUpper Then
                    lbtnPrint.Visible = False
                    lbtnPrint.Attributes.Add("onclick", String.Format("javascript:PopUpSPPOPrintBill('{0}')", obj.ID))
                Else
                    lbtnPrint.Visible = False
                End If
                'End    :CR:dna:20091216:add print kwitansi
            End If

            Dim maxday As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("BlockedDaysKTBConfirm"))
            Dim confirmDate As Date = obj.CreatedTime
            Dim currentDate As Date = Date.Today
            Dim range As Date = currentDate.AddDays(-14)
            'If confirmDate <> "1/1/1753" Then
            '    'Bug  1647
            '    If obj.Status = EnumIndentPartStatus.IndentPartStatus.SENT Then
            '        If range > confirmDate Then
            '            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
            '            e.Item.BackColor = Color.Red
            '        End If
            '    End If

            'End If

            Dim lblLastUpdated As Label = e.Item.FindControl("lblLastUpdated")
            'lblLastUpdated.Attributes.Add("onclick", String.Format("javascript:ShowLastUpdatedHistory('{0}')", obj.EstimationNumber))
            Dim urlParams As String = ""
            Dim oEEH As EstimationEquipHeader = New EstimationEquipHeaderFacade(User).Retrieve(obj.EstimationNumber)

            urlParams &= "TableName=" & GetType(EstimationEquipHeader).Name
            urlParams &= "&TableID=" & oEEH.ID.ToString()
            urlParams &= "&TableCode=" & oEEH.EstimationNumber
            urlParams &= "&FieldName=" & "Status"

            urlParams &= "&TableName2=" & GetType(IndentPartHeader).Name
            urlParams &= "&TableID2=" & obj.ID.ToString()
            urlParams &= "&TableCode2=" & obj.RequestNo
            If oDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                urlParams &= "&FieldName2=" & "StatusKTB"
            Else
                urlParams &= "&FieldName2=" & "Status"
            End If
            lblLastUpdated.Attributes.Add("onclick", String.Format("javascript:ShowLastUpdatedHistory('{0}')", urlParams))
            Dim lblDocFlow As Label = e.Item.FindControl("lblDocFlow")
            lblDocFlow.Attributes.Add("onclick", String.Format("javascript:ShowDocFlow('{0}')", obj.RequestNo))

            Dim imgIndikator As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgIndikator"), System.Web.UI.WebControls.Image)
            Dim nAllocated As Integer = 0
            Dim TotalTagihan As Decimal = 0
            Dim ConfDate As Date

            For Each objIPD As IndentPartDetail In obj.IndentPartDetails
                nAllocated += IIf(objIPD.Qty > 0, 1, 0)
                If Not IsNothing(objIPD.EstimationEquipDetail) AndAlso objIPD.EstimationEquipDetail.ID > 0 Then
                    TotalTagihan += (objIPD.Qty * objIPD.EstimationEquipDetail.Harga) - ((objIPD.EstimationEquipDetail.Discount / 100) * objIPD.Qty * objIPD.EstimationEquipDetail.Harga)
                    ConfDate = objIPD.EstimationEquipDetail.ConfirmedDate
                End If
            Next
            ConfDate = CommonFunction.AddNWorkingDay(ConfDate, 10)
            Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(obj.CreatedTime, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
            lblTotalTagihan.Text = FormatNumber(CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=TotalTagihan) + TotalTagihan, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblDeadlineKwitansi.Text = ConfDate.ToString("dd MMM yyyy")
            If obj.StatusKTB = EnumIndentPartEquipStatus.EnumStatusKTB.Proses_Order Then
                If obj.SisaQty = obj.TotalItem Then ' nAllocated = 0 Then
                    imgIndikator.ImageUrl = "../images/red.gif"
                ElseIf obj.SisaQty = 0 Then ' nAllocated = obj.IndentPartDetails.Count Then
                    imgIndikator.ImageUrl = "../images/green.gif"
                Else
                    imgIndikator.ImageUrl = "../images/yellow.gif"
                End If
            Else
                imgIndikator.ImageUrl = "../images/red.gif"
            End If
            'If (obj.Status = EnumIndentPartStatus.IndentPartStatus.ALOKASI_SEBAGIAN) Then
            '    imgIndikator.ImageUrl = "../images/yellow.gif"
            'ElseIf (obj.Status = EnumIndentPartStatus.IndentPartStatus.SELESAI) Then
            '    imgIndikator.ImageUrl = "../images/green.gif"
            'Else
            '    imgIndikator.ImageUrl = "../images/red.gif"
            'End If


        End If

    End Sub

    Private Sub dtgEquipPO_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEquipPO.ItemCommand
        If e.CommandName.ToUpper = "SORT" Then Return

        If (e.CommandName = "edit") Then
            Response.Redirect("../IndentPartEquipment/FrmPoEstimationEquip.aspx?SPPOID=" & e.CommandArgument & "&View=False", True)
        ElseIf (e.CommandName = "detail") Then
            Response.Redirect("../IndentPartEquipment/FrmPoEstimationEquip.aspx?SPPOID=" & e.CommandArgument & "&View=True", True)
        ElseIf (e.CommandName = "delete") Then
            Dim hf As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)
            Dim dtEQHeader As IndentPartHeader = New IndentPartHeaderFacade(User).Retrieve(CType(e.CommandArgument, Integer))
            If dtEQHeader.ID > 0 Then
                dtEQHeader.RowStatus = -1
                hf.Update(dtEQHeader)
            End If
            BindToGrid(dtgEquipPO.CurrentPageIndex)
        End If
    End Sub

    Private Sub dtgEquipPO_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEquipPO.SortCommand
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
        BindToGrid(dtgEquipPO.CurrentPageIndex)
    End Sub

    Private Sub dtgEquipPO_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEquipPO.PageIndexChanged
        dtgEquipPO.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgEquipPO.CurrentPageIndex)
    End Sub

    Private Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim idxPage As Integer = 0
        Dim arlhdr As ArrayList = CType(_sesshelper.GetSession(_szSsListEqpPo), ArrayList)
        If Not IsNothing(arlhdr) And IsNothing(sender) Then 'From back button
            idxPage = arlhdr(6)
        End If
        dtgEquipPO.CurrentPageIndex = idxPage
        CreateCriteria()

        'update IndentpartHeader expired
        'UpdateToExpired()

        BindToGrid(idxPage)
    End Sub


    Private Function sendDeposit_CUnUsed(ByVal objSppo As IndentPartHeader)
        Dim oDealer As Dealer = _sesshelper.GetSession("DEALER")
        Dim euf As EquipUserFacade = New EquipUserFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim mail As DNetMail = New DNetMail(smtp)
        Dim szTos As String '= euf.CreateEmailToString(EquipUser.EquipUserGroup.Deposit_C) ' .Approved)
        Dim szCcs As String '= euf.CreateEmailCcString(EquipUser.EquipUserGroup.Deposit_C)  ' .Approved)
        Dim szItems As String = ""
        Dim i As Integer = 0
        Dim myculture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ID-id")
        Dim Total As Decimal = 0
        Dim ItemPrice As Decimal = 0
        Dim objEED As EstimationEquipDetail
        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objSppo.RequestDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

        CommonFunction.SetIndentPartEmailRecipient(EquipUser.EquipUserGroup.Deposit_C, szTos, szCcs, objSppo.Dealer)
        For Each objItem As IndentPartDetail In objSppo.IndentPartDetails
            objEED = GetEED(objItem)
            i += 1
            szItems += "<tr>"
            szItems += String.Format("<td>{0}</td>", i)
            szItems += String.Format("<td>{0}</td>", objItem.SparePartMaster.PartNumber)
            szItems += String.Format("<td>{0}</td>", objItem.SparePartMaster.PartName)
            szItems += String.Format("<td>{0}</td>", IIf(objItem.SparePartMaster.SupplierCode = "", "-", objItem.SparePartMaster.SupplierCode))
            szItems += String.Format("<td align='center'>{0}</td>", objItem.Qty)
            szItems += String.Format("<td align='right'>{0}</td>", FormatNumber(objEED.Harga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            szItems += String.Format("<td align='right'>{0}</td>", FormatNumber(objEED.Discount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            ItemPrice = (objItem.Qty * objEED.Harga) - objEED.Discount / 100 * (objItem.Qty * objEED.Harga)
            szItems += String.Format("<td align='right'>{0}</td>", FormatNumber(ItemPrice, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            szItems += "</tr>"
            Total += ItemPrice
        Next
        'Dim TotalPPN As Decimal = 0.1 * Total
        Dim TotalPPN As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=Total)
        Dim szFormats() As String = {objSppo.Dealer.DealerCode, objSppo.Dealer.DealerName, Format(objSppo.RequestDate, "dd/MM/yyyy"), objSppo.RequestNo, String.Format("Rp.{0}", FormatNumber(Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)), "Rp." & FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True), "Rp." & FormatNumber(TotalPPN, 2, TriState.UseDefault, TriState.UseDefault, TriState.True), "Rp." & FormatNumber(Total + TotalPPN, 2, TriState.UseDefault, TriState.UseDefault, TriState.True), szItems}
        mail.sendMail(Server.MapPath("../IndentPartEquipment/EmailTemplateApprovedSPPOSimple.htm"), szTos, szCcs, "[MMKSI-DNet] Parts - Pengajuan Order Dep.C - " & objSppo.RequestNo & " - " & objSppo.Dealer.DealerCode, szFormats)
    End Function

    Private Function sendDeposit_BUnUsed(ByVal objSppo As IndentPartHeader)
        Dim oDealer As Dealer = _sesshelper.GetSession("DEALER")
        Dim euf As EquipUserFacade = New EquipUserFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim mail As DNetMail = New DNetMail(smtp)
        Dim szTos As String '= euf.CreateEmailToString(EquipUser.EquipUserGroup.Deposit_B)
        Dim szCcs As String '= euf.CreateEmailCcString(EquipUser.EquipUserGroup.Deposit_B)
        Dim szItems As String = ""
        Dim i As Integer = 0
        Dim myculture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ID-id")
        Dim Total As Decimal = 0
        Dim ItemPrice As Decimal = 0
        Dim objEED As EstimationEquipDetail
        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objSppo.RequestDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

        CommonFunction.SetIndentPartEmailRecipient(EquipUser.EquipUserGroup.Deposit_B, szTos, szCcs, oDealer)
        For Each objItem As IndentPartDetail In objSppo.IndentPartDetails
            objEED = GetEED(objItem)
            i += 1
            szItems += "<tr>"
            szItems += String.Format("<td>{0}</td>", i)
            szItems += String.Format("<td>{0}</td>", objItem.SparePartMaster.PartNumber)
            szItems += String.Format("<td>{0}</td>", objItem.SparePartMaster.PartName)
            szItems += String.Format("<td>{0}</td>", IIf(objItem.SparePartMaster.SupplierCode = "", "-", objItem.SparePartMaster.SupplierCode))
            szItems += String.Format("<td align='center'>{0}</td>", objItem.Qty)
            szItems += String.Format("<td align='right'>{0}</td>", FormatNumber(objEED.Harga, 2, TriState.UseDefault, TriState.UseDefault, TriState.True))
            szItems += String.Format("<td align='right'>{0}</td>", FormatNumber(objEED.Discount, 2, TriState.UseDefault, TriState.UseDefault, TriState.True))
            ItemPrice = (objItem.Qty * objEED.Harga) - objEED.Discount / 100 * (objItem.Qty * objEED.Harga)
            szItems += String.Format("<td align='right'>{0}</td>", FormatNumber(ItemPrice, 2, TriState.UseDefault, TriState.UseDefault, TriState.True))
            szItems += "</tr>"
            Total += ItemPrice
        Next
        szItems += "<tr>"
        szItems += "<td colspan='6' rowspan='3'></td>"
        szItems += "<td><b>Total Order</b></td>"
        szItems += "<td></td>"
        szItems += "</tr>"
        szItems += "<tr>"
        szItems += "<td><b>PPN</b></td>"
        szItems += "<td></td>"
        szItems += "</tr>"
        szItems += "<tr>"
        szItems += "<td><b>Total Tagihan</b></td>"
        szItems += "<td></td>"
        szItems += "</tr>"
        'Dim TotalPPN As Decimal = 0.1 * Total

        Dim TotalPPN As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=Total)
        'Dim szFormats() As String = {objSppo.Dealer.DealerCode _
        ', objSppo.Dealer.DealerName _
        ', Format(objSppo.RequestDate, "dd/MM/yyyy") _
        ', objSppo.RequestNo _
        ', String.Format("Rp.{0}", FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)) _
        ', "Rp." & FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True) _
        ', "Rp." & FormatNumber(TotalPPN, 2, TriState.UseDefault, TriState.UseDefault, TriState.True) _
        ', "Rp." & FormatNumber(Total + TotalPPN, 2, TriState.UseDefault, TriState.UseDefault, TriState.True) _
        ', szItems}
        Dim szFormats() As String = {objSppo.Dealer.DealerCode _
        , objSppo.Dealer.DealerName _
        , Format(objSppo.RequestDate, "dd/MM/yyyy") _
        , objSppo.RequestNo _
        , szItems}
        mail.sendMail(Server.MapPath("../IndentPartEquipment/EmailTemplateDepositeBSPPOSimple.htm"), szTos, szCcs, "[MMKSI-DNet] Parts - " & objSppo.RequestNo, szFormats)
    End Function

    Private Function GetEED(ByVal ipd As IndentPartDetail) As EstimationEquipDetail
        Dim Sql As String = ""
        Dim objEEDFac As EstimationEquipDetailFacade = New EstimationEquipDetailFacade(User)
        Dim crtEED As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipDetail), "RowStatus", CType(DBRowStatus.Active, Short)))
        Dim objEED As EstimationEquipDetail
        Dim arlEED As New ArrayList


        Sql &= " select distinct(EstimationEquipDetailID)    "
        Sql &= " from EstimationEquipPO eepo   "
        Sql &= " where 1=1   "
        Sql &= "  and eepo.IndentPartDetailID in (  "
        Sql &= "   select ipd.ID   "
        Sql &= "   from IndentPartDetail ipd "
        Sql &= "   where 1=1 "
        Sql &= "    and ipd.IndentPartHeaderID=" & ipd.IndentPartHeader.ID
        Sql &= "    and ipd.SparePartMasterID=" & ipd.SparePartMaster.ID
        Sql &= " )"

        crtEED.opAnd(New Criteria(GetType(EstimationEquipDetail), "ID", MatchType.InSet, "(" & Sql & ")"))
        arlEED = objEEDFac.Retrieve(crtEED)
        If arlEED.Count > 0 Then
            Return CType(arlEED(0), EstimationEquipDetail)
        Else
            Dim oEED As EstimationEquipDetail = New EstimationEquipDetail
            oEED.ID = -1
            oEED.Harga = ipd.Price
            oEED.ConfirmedDate = Now
            oEED.Discount = 0
            Return oEED  ' New IndentPartDetailFacade(User).Retrieve(ipd.ID)
        End If

    End Function

    Private Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click

        UpdateStatus()

        Exit Sub
        If ddlStatus.SelectedIndex = 0 Then
            MessageBox.Show("Pilih Status Dulu")
            Exit Sub
        End If

        If Not isVerifiedStatus() Then
            Exit Sub
        End If

        Dim arlToUpdate As ArrayList = New ArrayList
        Dim i As Integer = 0
        For Each item As DataGridItem In dtgEquipPO.Items
            Dim chkItemChecked As CheckBox = item.FindControl("chkItemChecked")
            If chkItemChecked.Checked Then
                Dim objIPHeader As v_EquipPO = CType(_sesshelper.GetSession(_szSsListEqpPo), ArrayList)(item.ItemIndex)
                Dim szErrMsg As String = ""
                If (isItemValid(objIPHeader.Status, 0)) Then
                    If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                        'If (CByte(objIPHeader.PaymentType) <> EnumEstimationEquipStatus.EstimationEquipmentPaymentType.Deposit_C) Then
                        arlToUpdate.Add(objIPHeader)
                        'End If
                    Else
                        arlToUpdate.Add(objIPHeader)
                    End If
                End If
                i += 1
            End If
        Next

        If i = 0 Then
            MessageBox.Show("Tidak ada data yang dipilih")
            Exit Sub
        End If

        If (ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.TOLAK) Then
            If (hdnRejectDesc.Value = "") Then
                MessageBox.Show("Alasan penolakan belum diisi")
                ddlStatus.SelectedIndex = 0
                Exit Sub
            End If
        End If

        Dim sppoinfof As EquipSPPOInfoFacade = New EquipSPPOInfoFacade(User)
        Dim sppof As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)
        For Each itemToUpdate As v_EquipPO In arlToUpdate
            Dim objSppo As IndentPartHeader = sppof.Retrieve(itemToUpdate.ID)
            Dim objSppoInfo As EquipSPPOInfo = sppoinfof.RetrieveByHeaderID(objSppo.ID)

            If (ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.RILIS) Then
                'objSppo.PaymentType = EnumEstimationEquipStatus.EstimationEquipmentPaymentType.Deposit_C
                If objSppo.PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_B Then
                    sendApprovedEmail(objSppo)
                End If
            ElseIf (ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.TOLAK) Then
                objSppoInfo.Description = hdnRejectDesc.Value
                sppoinfof.Update(objSppoInfo)
                sendRejectEmail(objSppo)
            ElseIf (ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.SENT) Then
                If objSppo.PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_B Then
                    Dim oFPEE As New FrmPoEstimationEquip
                    oFPEE.sendDeposit_B(objSppo)
                    'sendDeposit_B(objSppo)
                ElseIf objSppo.PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_C Then
                    Dim oFPEE As New FrmPoEstimationEquip
                    oFPEE.sendDeposit_C(objSppo)
                    'sendDeposit_C(objSppo)
                End If
            End If

            Dim oldstatus As Integer = objSppo.Status

            'Start  :RemainModule:IndentPart:dna:20091203:update status 

            'objSppo.Status = ddlStatus.SelectedValue
            'objSppo.StatusKTB = ddlStatus.SelectedValue

            'DealerSide:kirim, batal, batal order
            'KTBSide:Rilis,prosesOrder,tolak,batalselesai,selesai 
            If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                If ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.SENT Then ' .SENT Then
                    objSppo.Status = EnumIndentPartStatus.IndentPartStatusDealer.Kirim
                ElseIf ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.BATAL Then ' .BATAL Then
                    objSppo.Status = EnumIndentPartStatus.IndentPartStatusDealer.Batal
                ElseIf ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.BATAL_ORDER Then ' .BATAL_ORDER Then
                    objSppo.Status = EnumIndentPartStatus.IndentPartStatusDealer.Batal_Order
                End If
                objSppo.Status = ddlStatus.SelectedValue
                If ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.SENT Then
                    If objSppo.PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_B Then
                        objSppo.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Baru
                    ElseIf objSppo.PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_C Then
                        objSppo.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Rilis
                    End If
                Else
                    objSppo.StatusKTB = ddlStatus.SelectedValue
                End If
                'DDLStatus=Kirim & paymentType=Deposit_C->KTBStatus=Rilis -->Custom
                'DDLStatus=Kirim & paymentType=Deposit_B->KTBStatus=Baru -->Custom
                'DDLStatus=Batal ->KTBStatus=Batal
                'DDLStatus=BatalOrder ->KTBStatus=BatalOrder
            Else
                'objSppo.StatusKTB = ddlStatus.SelectedValue
                'KTBSide:Rilis,prosesOrder,tolak,batalselesai,selesai 
                If ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.RILIS Then ' .RILIS Then
                    objSppo.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Rilis
                ElseIf ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.ORDER_TO_VENDOR Then ' .PROSSES Then
                    objSppo.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Proses
                ElseIf ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.TOLAK Then ' TOLAK Then
                    objSppo.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Tolak
                ElseIf ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.BATAL_KTB Then ' .BATAL_KTB Then
                    objSppo.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Batal_Order
                ElseIf ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.SELESAI Then ' .SELESAI Then
                    objSppo.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Selesai
                End If
            End If
            'End    :RemainModule:IndentPart:dna:20091203:update status

            Dim result As Integer = sppof.Update(objSppo)
            sppof.RecordStatusChangeHistory(objSppo, oldstatus)
        Next
        BindToGrid(dtgEquipPO.CurrentPageIndex)
        MessageBox.Show("Ubah status " & ddlStatus.SelectedItem.Text & " berhasil")
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim SW As StreamWriter
        Dim _filename As String = String.Format("{0}{1}{2}", "RequestIndentPartEquipment", Date.Now.ToString("ddMMyyyyHHmmss"), ".xls")
        Dim _destFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("IndentPartDownload") & "\" & _filename  '-- Destination file
        Dim _spliterChr As Char = Chr(9)
        Dim _connected As Boolean = False
        Dim _success As Boolean = False

        Dim iphf As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)
        Dim ipFacade As EstimationEquipHeaderFacade = New EstimationEquipHeaderFacade(User)
        Dim eqppof As EstimationEquipPOFacade = New EstimationEquipPOFacade(User)

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate
        'get checked in grid
        Dim i As Integer = 0
        For Each item As DataGridItem In dtgEquipPO.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If chk.Checked Then
                If Not _connected Then
                    imp = New SAPImpersonate(_user, _password, _webServer)
                    Dim finfo As New FileInfo(_destFile)
                    Try
                        _success = imp.Start()
                        If _success Then
                            If Not finfo.Directory.Exists Then
                                Directory.CreateDirectory(finfo.DirectoryName)
                            End If
                            SW = New StreamWriter(_destFile)
                            _connected = True
                            'write title
                            If (i = 0) Then
                                If (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
                                    SW.WriteLine(String.Format("Indent Part Number{0}Dealer Code{0}Dealer Name{0}Part Number{0}Part Name{0}Quantity{0}Price{0}Estimation Number{0}Indent Part Request Date{0}Price Confirm Date{0}Remain Qty{0}Vendor Code{0}Status", _spliterChr))
                                Else
                                    SW.WriteLine(String.Format("Indent Part Number{0}Dealer Code{0}Dealer Name{0}Part Number{0}Part Name{0}Quantity{0}Price{0}Estimation Number{0}Indent Part Request Date{0}Price Confirm Date{0}Remain Qty{0}Status", _spliterChr))
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        Throw ex
                        Exit Sub
                    End Try
                End If

                Dim objEqpPo As v_EquipPO = equippof.Retrieve(CInt(item.Cells(3).Text))
                Dim iph As IndentPartHeader = iphf.Retrieve(objEqpPo.ID)
                Dim objEstH As EstimationEquipHeader = ipFacade.Retrieve(objEqpPo.EstimationNumber)

                For Each itemdetail As IndentPartDetail In iph.IndentPartDetails
                    Dim objEstPo As EstimationEquipPO = eqppof.RetrieveByIndentPartDetailIDEstimationEquipHeaderID(itemdetail, objEstH.ID)
                    Dim szRow As String = ""
                    If (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
                        Dim szFormat As String = "{0}{13} " & _
                        "{1}{13}" & _
                        "{2}{13}" & _
                        "{3}{13}" & _
                        "{4}{13}" & _
                        "{5}{13}" & _
                        "{6}{13}" & _
                        "{7}{13}" & _
                        "{8}{13}" & _
                        "{9}{13}" & _
                        "{10}{13}" & _
                        "{11}{13}" & _
                        "{12}{13}"
                        szRow = String.Format(szFormat, _
                            objEqpPo.RequestNo, _
                            objEqpPo.DealerCode, _
                            objEqpPo.DealerName, _
                            itemdetail.SparePartMaster.PartNumber, _
                            itemdetail.SparePartMaster.PartName, _
                            itemdetail.Qty, _
                            itemdetail.Price, _
                            objEqpPo.EstimationNumber, _
                            objEstH.CreatedTime.ToString("dd MMM yyyy"), _
                            objEqpPo.CreatedTime.ToString("dd MMM yyyy"), _
                            (itemdetail.SisaQty()), _
                            itemdetail.SparePartMaster.SupplierCode, _
                            iph.StatusKTBDesc, _
                            _spliterChr)
                        '(itemdetail.Qty - itemdetail.AllocationQty), _
                    Else
                        Dim szFormat As String = "{0}{12} " & _
                        "{1}{12}" & _
                        "{2}{12}" & _
                        "{3}{12}" & _
                        "{4}{12}" & _
                        "{5}{12}" & _
                        "{6}{12}" & _
                        "{7}{12}" & _
                        "{8}{12}" & _
                        "{9}{12}" & _
                        "{10}{12}" & _
                        "{11}{12}"
                        szRow = String.Format(szFormat, _
                            objEqpPo.RequestNo, _
                            objEqpPo.DealerCode, _
                            objEqpPo.DealerName, _
                            itemdetail.SparePartMaster.PartNumber, _
                            itemdetail.SparePartMaster.PartName, _
                            itemdetail.Qty, _
                            itemdetail.Price, _
                            objEqpPo.EstimationNumber, _
                            objEstH.CreatedTime.ToString("dd MMM yyyy"), _
                            objEqpPo.CreatedTime.ToString("dd MMM yyyy"), _
                            (itemdetail.Qty - itemdetail.AllocationQty), _
                            iph.StatusDealerDesc, _
                            _spliterChr)
                    End If
                    SW.WriteLine(szRow)
                Next
            End If
        Next

        If _success Then
            SW.Close()
            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("IndentPartDownload") & "\" & _filename
            imp.StopImpersonate()
            imp = Nothing
            Response.Redirect("../Download.aspx?file=" & PathFile, True)
        Else
            MessageBox.Show("Daftar Request Indent Part belum dipilih")
        End If
    End Sub

#End Region

#Region "function"

    Private Sub InitiateAuthorization()
        Dim IsAllowedToAccess As Boolean = False
        If SecurityProvider.Authorize(context.User, SR.Lihat_daftar_pengajuan_order_indent_part_equipment_privilege) Then
            ddlStatus.Enabled = False
            btnProcess.Enabled = False
            IsAllowedToAccess = True
        End If
        If SecurityProvider.Authorize(context.User, SR.Ubah_status_rilis_tolak_privilege) _
                OrElse SecurityProvider.Authorize(context.User, SR.Ubah_status_pengajuan_order_indent_part_equipment_privilege) Then
            ddlStatus.Enabled = True
            btnProcess.Enabled = True
            IsAllowedToAccess = True
        End If
        If Not IsAllowedToAccess Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Estimation Indent Part Equipment - Daftar Pengajuan Order")
        End If
        'If Not SecurityProvider.Authorize(context.User, SR.Ubah_status_rilis_tolak_privilege) Then
        'If Not SecurityProvider.Authorize(context.User, SR.Lihat_daftar_pengajuan_order_indent_part_equipment_privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=Estimation Indent Part Equipment - Daftar Pengajuan Order")
        '    btnDownload.Visible = False

        'Else
        '    btnDownload.Visible = True
        '    btnProcess.Enabled = False
        '    ddlStatus.Enabled = False
        'End If
        'If SecurityProvider.Authorize(context.User, SR.Ubah_status_pengajuan_order_indent_part_equipment_privilege) Then
        '    btnProcess.Visible = True
        'Else
        '    btnProcess.Visible = False
        'End If
    End Sub

    Private Sub BindToGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim xsum As v_EquipPO

        Dim arlEquipPo As ArrayList = equippof.RetrieveActiveList(_sesshelper.GetSession(_szSsCrits), currentPageIndex + 1, dtgEquipPO.PageSize, total, _
           CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dtgEquipPO.VirtualItemCount = total
        dtgEquipPO.DataSource = arlEquipPo
        dtgEquipPO.DataBind()
        _sesshelper.SetSession(_szSsListEqpPo, arlEquipPo)
        calculateTotals(arlEquipPo)
        If (total = 0) Then
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub calculateTotals(ByVal arl As ArrayList)
        Dim totalAmount As Decimal = 0
        Dim totalItem As Integer = 0
        Dim sppof As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)
        For Each objEqpPo As v_EquipPO In arl
            Dim objSppo As IndentPartHeader = sppof.Retrieve(objEqpPo.ID)
            totalAmount += objSppo.AmountBeforeTax
            totalItem += objEqpPo.TotalItem
        Next
        lblTotalAmount.Text = String.Format("Total Amount: Rp.{0}", totalAmount.ToString("#,##0"))
        lblTotalOrder.Text = String.Format("Total Order: {0}", arl.Count)
        lblGrandTotal.Text = String.Format("Grand Total Item: {0}", totalItem.ToString())
    End Sub

    Private Sub CreateCriteria()
        Dim objSearch As v_EquipPOSearch = New v_EquipPOSearch
        objSearch.bytePaymentType = ddlPaymentType.SelectedValue
        objSearch.dtmFrom = icPODateFrom.Value
        objSearch.dtmTo = icPODateUntil.Value
        objSearch.arlDealerCode = New ArrayList(txtDealerCode.Text.Split(";"))
        objSearch.arlEstNo = New ArrayList(txtEstimationNumber.Text.Split(";"))
        objSearch.arlSPPONo = New ArrayList(txtPONumber.Text.Split(";"))

        Dim arlProcessCode As ArrayList = New ArrayList
        If lstStatus.SelectedIndex <> -1 Then
            Dim li As ListItem
            For Each li In lstStatus.Items
                If li.Selected Then
                    arlProcessCode.Add(li.Value)
                End If
            Next
        End If

        If (arlProcessCode.Count <= 0) Then
            If (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
                'objSearch.arlProcessCode = New ArrayList(String.Format("{0};{1};{2};{3};{4};{5};{6};{7}", _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.SENT), _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.BATAL_ORDER), _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.TOLAK), _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.ORDER_TO_VENDOR), _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.BATAL_KTB), _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.ALOKASI_SEBAGIAN), _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.RILIS), _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.SELESAI)).Split(";"))
                objSearch.arlProcessCode = New ArrayList(String.Format("{0};{1};{2};{3};{4};{5};{6}", _
                    CInt(EnumIndentPartEquipStatus.EnumStatusKTB.Baru), _
                    CInt(EnumIndentPartEquipStatus.EnumStatusKTB.Batal), _
                    CInt(EnumIndentPartEquipStatus.EnumStatusKTB.Proses_Order), _
                    CInt(EnumIndentPartEquipStatus.EnumStatusKTB.Rilis), _
                    CInt(EnumIndentPartEquipStatus.EnumStatusKTB.Selesai), _
                    CInt(EnumIndentPartEquipStatus.EnumStatusKTB.Expired), _
                    CInt(EnumIndentPartEquipStatus.EnumStatusKTB.Tolak)).Split(";"))
                'status expired hanya ada di Dealer
            Else
                'objSearch.arlProcessCode = New ArrayList(String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}", _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.BARU), _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.BATAL), _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.BATAL_ORDER), _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.SENT), _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.TOLAK), _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.ORDER_TO_VENDOR), _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.BATAL_KTB), _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.ALOKASI_SEBAGIAN), _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.RILIS), _
                '    CInt(EnumIndentPartStatus.IndentPartStatus.SELESAI)).Split(";"))
                objSearch.arlProcessCode = New ArrayList(String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}", _
                    CInt(EnumIndentPartEquipStatus.EnumStatusDealer.Baru), _
                    CInt(EnumIndentPartEquipStatus.EnumStatusDealer.Batal), _
                    CInt(EnumIndentPartEquipStatus.EnumStatusDealer.Batal_Order), _
                    CInt(EnumIndentPartEquipStatus.EnumStatusDealer.Kirim), _
                    CInt(EnumIndentPartEquipStatus.EnumStatusDealer.Proses), _
                    CInt(EnumIndentPartEquipStatus.EnumStatusDealer.Selesai), _
                    CInt(EnumIndentPartEquipStatus.EnumStatusDealer.Tolak), _
                    CInt(EnumIndentPartEquipStatus.EnumStatusDealer.Expired), _
                    CInt(EnumIndentPartEquipStatus.EnumStatusDealer.Baru)).Split(";"))
            End If
        Else
            objSearch.arlProcessCode = arlProcessCode
        End If

        Dim listSPPO As ArrayList = equippof.RetrieveSearch(objSearch)
        Dim crits As CriteriaComposite = equippof.RetrieveCriterias(objSearch, oDealer.Title)

        _sesshelper.SetSession(_szSsCrits, crits)
    End Sub

    Private Function isVerifiedStatus() As Boolean
        Dim isValid As Boolean = True
        Dim i As Integer = 0
        For Each item As DataGridItem In dtgEquipPO.Items
            i += 1
            Dim chkItemChecked As CheckBox = item.FindControl("chkItemChecked")
            If chkItemChecked.Checked Then
                Dim objIPHeader As v_EquipPO = CType(_sesshelper.GetSession(_szSsListEqpPo), ArrayList)(item.ItemIndex)
                Dim szErrMsg As String = ""
                If (Not isItemValid(objIPHeader.Status, i)) Then
                    'MessageBox.Show("Item -" & i.ToString & " : Status " & objIPHeader.StatusDesc & " tidak dapat diubah")
                    'szErrMsg &= "Item -" & i.ToString & " : Status " & objIPHeader.StatusDesc & " tidak dapat diubah" & "<br />"
                    isValid = False
                End If
                If (ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.ORDER_TO_VENDOR And objIPHeader.Status = EnumIndentPartStatus.IndentPartStatus.BARU And objIPHeader.PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_B) Then
                    MessageBox.Show(String.Format("Item -{0}, order dengan pembayaran deposit B harus di-RILIS terlebih dahulu", i))
                    isValid = False
                End If

                If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                    If (ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.BATAL_KTB) Then
                        If (objIPHeader.Status = EnumIndentPartStatus.IndentPartStatus.SENT) Then
                            If (objIPHeader.PaymentType = EnumEstimationEquipStatus.EstimationEquipmentPaymentType.Deposit_B) Then
                                isValid = False
                            Else : isValid = True
                            End If
                        Else : isValid = True
                        End If
                    End If
                End If
            End If
        Next
        Return isValid
    End Function

    Private Function isItemValid(ByVal status As String, ByVal i As Integer) As Boolean
        If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            If (ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatusDealer.Kirim) Then  ' .IndentPartStatus.SENT) Then
                If (status = EnumIndentPartStatus.IndentPartStatus.BARU Or status = "") Then
                    Return True
                Else
                    If (i <> 0) Then
                        MessageBox.Show(String.Format("Item -{0}, order dengan status {1} tidak dapat diubah menjadi {2}", i, EnumIndentPartStatus.IndentPartStatusDesc(status), ddlStatus.SelectedItem.Text))
                    End If
                    Return False
                End If
            ElseIf (ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.BATAL) Then
                If (status = EnumIndentPartStatus.IndentPartStatus.BARU Or status = "") Then
                    Return True
                Else
                    If (i <> 0) Then
                        MessageBox.Show(String.Format("Item -{0}, order dengan status {1} tidak dapat diubah menjadi {2}", i, EnumIndentPartStatus.IndentPartStatusDesc(status), ddlStatus.SelectedItem.Text))
                    End If
                    Return False
                End If
            ElseIf (ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.BATAL_ORDER) Then
                If (status = EnumIndentPartStatus.IndentPartStatus.SENT) Then
                    Return True
                Else
                    If (i <> 0) Then
                        MessageBox.Show(String.Format("Item -{0}, order dengan status {1} tidak dapat diubah menjadi {2}", i, EnumIndentPartStatus.IndentPartStatusDesc(status), ddlStatus.SelectedItem.Text))
                    End If
                    Return False
                End If
            End If
        ElseIf (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            If (ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatusKTB.Rilis) Then ' .IndentPartStatus.RILIS) Then
                If (status = EnumIndentPartStatus.IndentPartStatus.SENT) Then
                    Return True
                Else
                    If (i <> 0) Then
                        MessageBox.Show(String.Format("Item -{0}, order dengan status {1} tidak dapat diubah menjadi {2}", i, EnumIndentPartStatus.IndentPartStatusDesc(status), ddlStatus.SelectedItem.Text))
                    End If
                    Return False
                End If
            ElseIf (ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatusKTB.Rilis) Then ' EnumIndentPartStatus.IndentPartStatus.ORDER_TO_VENDOR) Then
                If (status = EnumIndentPartStatus.IndentPartStatus.RILIS) Then
                    Return True
                Else
                    If (i <> 0) Then
                        MessageBox.Show(String.Format("Item -{0}, order dengan status {1} tidak dapat diubah menjadi {2}", i, EnumIndentPartStatus.IndentPartStatusDesc(status), ddlStatus.SelectedItem.Text))
                    End If
                    Return False
                End If
            ElseIf (ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.SELESAI) Then
                If (status = EnumIndentPartStatus.IndentPartStatus.ORDER_TO_VENDOR Or status = EnumIndentPartStatus.IndentPartStatus.ALOKASI_SEBAGIAN) Then
                    Return True
                Else
                    If (i <> 0) Then
                        MessageBox.Show(String.Format("Item -{0}, order dengan status {1} tidak dapat diubah menjadi {2}", i, EnumIndentPartStatus.IndentPartStatusDesc(status), ddlStatus.SelectedItem.Text))
                    End If
                    Return False
                End If
            ElseIf (ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.TOLAK) Then
                If (status = EnumIndentPartStatus.IndentPartStatus.SENT) Then
                    Return True
                Else
                    If (i <> 0) Then
                        MessageBox.Show(String.Format("Item -{0}, order dengan status {1} tidak dapat diubah menjadi {2}", i, EnumIndentPartStatus.IndentPartStatusDesc(status), ddlStatus.SelectedItem.Text))
                    End If
                    Return False
                End If
            ElseIf (ddlStatus.SelectedValue = EnumIndentPartStatus.IndentPartStatus.BATAL_KTB) Then
                If (status = EnumIndentPartStatus.IndentPartStatus.RILIS) Then
                    Return True
                Else
                    If (i <> 0) Then
                        MessageBox.Show(String.Format("Item -{0}, order dengan status {1} tidak dapat diubah menjadi {2}", i, EnumIndentPartStatus.IndentPartStatusDesc(status), ddlStatus.SelectedItem.Text))
                    End If
                    Return False
                End If
            End If
        End If
    End Function

    Private Function sendRejectEmail(ByVal objSppo As IndentPartHeader)
        oDealer = _sesshelper.GetSession("DEALER")
        Dim euf As EquipUserFacade = New EquipUserFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim mail As DNetMail = New DNetMail(smtp)
        Dim szTos As String ' = euf.CreateEmailToString(EquipUser.EquipUserGroup.Reject)
        Dim szCcs As String ' = euf.CreateEmailCcString(EquipUser.EquipUserGroup.Reject)
        Dim szItems As String = ""
        Dim objEED As EstimationEquipDetail
        Dim myculture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ID-id")
        Dim ItemPrice As Double = 0
        Dim TotalPrice As Double = 0 'ok
        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objSppo.RequestDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

        CommonFunction.SetIndentPartEmailRecipient(EquipUser.EquipUserGroup.Reject, szTos, szCcs, objSppo.Dealer)
        For Each objItem As IndentPartDetail In objSppo.IndentPartDetails
            objEED = GetEED(objItem)
            szItems &= "<tr>"
            szItems &= String.Format("<td>{0}</td>", objItem.SparePartMaster.PartNumber)
            szItems &= String.Format("<td align='center'>{0}</td>", objItem.Qty)
            szItems &= String.Format("<td align='right'>{0}</td>", FormatNumber(objItem.Price, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            szItems &= String.Format("<td align='right'>{0}</td>", FormatNumber(objEED.Discount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            ItemPrice = (objItem.Price * objItem.Qty) - (objEED.Discount / 100) * (objItem.Price * objItem.Qty)
            szItems &= String.Format("<td align='right'>{0}</td>", FormatNumber(ItemPrice, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            TotalPrice += ItemPrice
            szItems &= "</tr>"
        Next
        'Dim TotalPPN As Decimal = 0.1 * TotalPrice
        Dim TotalPPN As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=TotalPrice)
        szItems += "<tr>"
        szItems += "<td colspan='3' rowspan='3'>. &nbsp;</td>"
        szItems += "<td><b>Total Order</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(TotalPrice, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        szItems += "<tr>"
        szItems += "<td><b>PPN</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        szItems += "<tr>"
        szItems += "<td><b>Total Tagihan</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(TotalPrice + TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"

        Dim objESPPOI As EquipSPPOInfo = New EquipSPPOInfoFacade(User).RetrieveByHeaderID(objSppo.ID)
        Dim sReason As String = IIf(IsNothing(objESPPOI), "", objESPPOI.Description)

        Dim sCC As String = ""

        Dim EmailIndentCC1 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC1")
        Dim EmailIndentCC2 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC2")
        Dim EmailIndentCC3 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC3")
        Dim EmailIndentCC4 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC4")
        Dim EmailIndentCC5 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC5")

        sCC &= "<tr><td>" & EmailIndentCC1 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC2 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC3 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC4 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC5 & "</td></tr>"

        sCC = Now.ToString("dd MMM yyyy")
        'Dim szFormats() As String = {objSppo.Dealer.DealerName, objSppo.Dealer.Address, objSppo.RequestNo, String.Format("Rp.{0}", objSppo.AmountBeforeTax.ToString("#,##0")), sReason, szItems}
        Dim szFormats() As String = {objSppo.Dealer.DealerCode _
        , objSppo.Dealer.DealerName _
        , objSppo.Dealer.City.CityName _
        , Format(objSppo.RequestDate, "dd/MM/yyyy") _
        , objSppo.RequestNo _
        , objSppo.Dealer.DealerName, objSppo.Dealer.Address, objSppo.Dealer.City.CityName, objSppo.RequestNo, sReason, szItems, sCC}
        mail.sendMail(Server.MapPath(TEMP_EMAIL_REJECT), szTos, szCcs, "[MMKSI-DNet] Parts - Tolak " & objSppo.RequestNo & " - " & objSppo.Dealer.DealerCode, szFormats)
    End Function

    Private Function sendApprovedEmail(ByVal objSppo As IndentPartHeader)
        oDealer = _sesshelper.GetSession("DEALER")
        Dim euf As EquipUserFacade = New EquipUserFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim mail As DNetMail = New DNetMail(smtp)
        Dim szTos As String ' = euf.CreateEmailToString(EquipUser.EquipUserGroup.Approved)
        Dim szCcs As String ' = euf.CreateEmailCcString(EquipUser.EquipUserGroup.Approved)
        Dim szItems As String = ""
        Dim myculture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ID-id")
        Dim i As Integer = 1
        Dim objEED As EstimationEquipDetail
        Dim TotalPrice As Double = 0
        Dim ItemPrice As Double = 0
        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objSppo.RequestDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

        CommonFunction.SetIndentPartEmailRecipient(EquipUser.EquipUserGroup.Approved, szTos, szCcs, objSppo.Dealer)
        For Each objItem As IndentPartDetail In objSppo.IndentPartDetails
            objEED = GetEED(objItem)
            szItems &= "<tr>"
            szItems &= String.Format("<td align='center'>{0}</td>", i)
            szItems &= String.Format("<td>{0}</td>", IIf(objItem.SparePartMaster.PartNumber.Trim = "", " ", objItem.SparePartMaster.PartNumber))
            szItems &= String.Format("<td align='center'>{0}</td>", IIf(objItem.SparePartMaster.PartName.Trim = "", " ", objItem.SparePartMaster.PartName))
            szItems &= String.Format("<td align='center'>{0}</td>", objItem.Qty)
            szItems &= String.Format("<td align='center'>{0}</td>", FormatNumber(objItem.Price, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            szItems &= String.Format("<td align='center'>{0}</td>", FormatNumber(objEED.Discount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            ItemPrice = (objItem.Qty * objItem.Price) - objEED.Discount / 100 * (objItem.Qty * objItem.Price)
            szItems &= String.Format("<td align='right'>{0}</td>", FormatNumber(ItemPrice, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            TotalPrice += ItemPrice
            szItems += "</tr>"
            i += 1
        Next

        'Dim TotalPPN As Decimal = 0.1 * TotalPrice
        Dim TotalPPN = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=TotalPrice)
        szItems += "<tr>"
        szItems += "<td colspan='5' rowspan='3'>. &nbsp;</td>"
        szItems += "<td><b>Total Order</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(TotalPrice, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        szItems += "<tr>"
        szItems += "<td><b>PPN</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        szItems += "<tr>"
        szItems += "<td><b>Total Tagihan</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(TotalPrice + TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        'Dim szFormats() As String = {objSppo.Dealer.DealerName, objSppo.Dealer.Address, objSppo.RequestNo, String.Format("Rp.{0}", objSppo.AmountBeforeTax.ToString("#,##0")), szItems}
        'Dim szFormats() As String = {objSppo.Dealer.DealerName, objSppo.Dealer.Address, objSppo.Dealer.City.CityName, objSppo.RequestNo, szItems}
        Dim sRequestNo As String = ""
        If objSppo.IndentPartDetails.Count > 0 Then
            Dim oEED As EstimationEquipDetail = GetEED(CType(objSppo.IndentPartDetails(0), IndentPartDetail))
            If Not IsNothing(oEED) AndAlso oEED.ID > 0 Then
                sRequestNo = oEED.EstimationEquipHeader.EstimationNumber
            End If
        End If

        Dim sCC As String = ""

        Dim EmailIndentCC1 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC1")
        Dim EmailIndentCC2 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC2")
        Dim EmailIndentCC3 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC3")
        Dim EmailIndentCC4 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC4")
        Dim EmailIndentCC5 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC5")

        sCC &= "<tr><td>" & EmailIndentCC1 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC2 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC3 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC4 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC5 & "</td></tr>"

        sCC = Now.ToString("dd MMM yyyy")
        Dim szFormats() As String = {objSppo.Dealer.DealerCode _
        , objSppo.Dealer.DealerName _
        , objSppo.Dealer.City.CityName _
        , Format(objSppo.RequestDate, "dd/MM/yyyy") _
        , objSppo.RequestNo _
        , objSppo.Dealer.DealerName, objSppo.Dealer.Address, objSppo.Dealer.City.CityName, objSppo.RequestNo, szItems, sCC, sRequestNo}
        mail.sendMail(Server.MapPath(TEMP_EMAIL_APPROVED), szTos, szCcs, "[MMKSI-DNet] Parts - Approval " & objSppo.RequestNo & " - " & objSppo.Dealer.DealerCode, szFormats)
    End Function

    Private Sub BindStatus()
        Dim objD As Dealer = CType(_sesshelper.GetSession(_szSsDealer), Dealer)

        If (objD.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            For Each objLI As ListItem In ConvertStringToListItem(EnumIndentPartEquipStatus.GetStatusDealerList())
                lstStatus.Items.Add(objLI)
            Next
            For Each objLI As ListItem In ConvertStringToListItem(EnumIndentPartEquipStatus.GetUpdateStatusDealerList())
                ddlStatus.Items.Add(objli)
            Next
        ElseIf (objD.Title = EnumDealerTittle.DealerTittle.KTB) Then
            For Each objLI As ListItem In ConvertStringToListItem(EnumIndentPartEquipStatus.GetStatusKTBList())
                lstStatus.Items.Add(objLI)
            Next
            For Each objLI As ListItem In ConvertStringToListItem(EnumIndentPartEquipStatus.GetUpdateStatusKTBList())
                ddlStatus.Items.Add(objli)
            Next
            'implement privilege
            Dim arlNewItems As New ArrayList
            If SecurityProvider.Authorize(context.User, SR.Ubah_status_rilis_tolak_privilege) Then
                For Each li As ListItem In ddlStatus.Items
                    If li.Value = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Rilis _
                    Or li.Value = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Tolak Then
                        arlNewItems.Add(li)
                    End If
                Next
            ElseIf SecurityProvider.Authorize(context.User, SR.Ubah_status_pengajuan_order_indent_part_equipment_privilege) Then
                For Each li As ListItem In ddlStatus.Items
                    If li.Value = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Proses _
                    Or li.Value = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Batal _
                    Or li.Value = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Selesai Then
                        arlNewItems.Add(li)
                    End If
                Next
            End If
            ddlStatus.Items.Clear()
            ddlStatus.Items.Add(New ListItem("", -1))
            For Each li As ListItem In arlNewItems
                ddlStatus.Items.Add(li)
            Next
        End If
    End Sub

    Private Function ConvertStringToListItem(ByVal arlStr As ArrayList) As ArrayList
        Dim arlRsl As New ArrayList

        For Each str As String In arlStr
            arlRsl.Add(New ListItem(str.Split(";")(1), str.Split(";")(0)))
        Next
        Return arlRsl
    End Function

    Private Function UpdateStatus()
        If ddlStatus.SelectedValue = -1 Then
            MessageBox.Show("Pilih Status Dulu")
            Exit Function
        End If


        If (ddlStatus.SelectedValue = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Tolak) Then
            If (hdnRejectDesc.Value.Trim = "") Then
                MessageBox.Show("Alasan penolakan belum diisi")
                ddlStatus.SelectedValue = -1
                Exit Function
            End If
        End If

        Dim arlToUpdate As New ArrayList
        Dim strMessage As String = ""
        For Each di As DataGridItem In dtgEquipPO.Items
            Dim chkItemChecked As CheckBox = di.FindControl("chkItemChecked")
            Dim objVEPO As v_EquipPO = CType(_sesshelper.GetSession(_szSsListEqpPo), ArrayList)(di.ItemIndex) 'CType(di.DataItem, v_EquipPO)

            If chkItemChecked.Checked Then
                If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    If EnumIndentPartEquipStatus.IsValidUpdateKTB(ddlStatus.SelectedValue, objVEPO.StatusKTB, objVEPO.StatusKTB, objVEPO.Status, objVEPO.Status, objVEPO.PaymentType, strMessage) Then
                        arlToUpdate.Add(objVEPO)
                    Else
                        MessageBox.Show(strMessage)
                        Exit Function
                    End If
                Else
                    If EnumIndentPartEquipStatus.IsValidUpdateDealer(ddlStatus.SelectedValue, objVEPO.StatusKTB, objVEPO.StatusKTB, objVEPO.Status, objVEPO.Status, objVEPO.PaymentType, strMessage) Then
                        arlToUpdate.Add(objVEPO)
                    Else
                        MessageBox.Show(strMessage)
                        Exit Function
                    End If
                End If
            End If
        Next

        Dim objESPPOIFac As EquipSPPOInfoFacade = New EquipSPPOInfoFacade(User)
        Dim objESPPOI As EquipSPPOInfo
        If arlToUpdate.Count > 0 Then
            Dim objIPHFac As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)
            For Each objEPO As v_EquipPO In arlToUpdate
                Dim objIPH As IndentPartHeader = objIPHFac.Retrieve(objEPO.ID)
                Dim NewKTBStatus As Integer
                Dim NewDealerStatus As Integer
                Dim OldStatus As Integer

                If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    EnumIndentPartEquipStatus.IsValidUpdateKTB(ddlStatus.SelectedValue, objIPH.StatusKTB, NewKTBStatus, objIPH.Status, NewDealerStatus, objIPH.PaymentType)
                Else
                    EnumIndentPartEquipStatus.IsValidUpdateDealer(ddlStatus.SelectedValue, objIPH.Status, NewDealerStatus, objIPH.StatusKTB, NewKTBStatus, objIPH.PaymentType)
                End If
                OldStatus = objIPH.Status
                objIPH.StatusKTB = NewKTBStatus
                objIPH.Status = NewDealerStatus
                objIPHFac.Update(objIPH)
                If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER AndAlso ddlStatus.SelectedValue = EnumIndentPartEquipStatus.EnumUpdateStatusDealer.Batal Then
                    Dim oEEPOFac As EstimationEquipPOFacade = New EstimationEquipPOFacade(User)
                    Dim cEEPO As CriteriaComposite
                    Dim aEEPO As New ArrayList
                    Dim oEEDFac As EstimationEquipDetailFacade = New EstimationEquipDetailFacade(User)
                    Dim oEED As EstimationEquipDetail
                    For Each oIPD As IndentPartDetail In objIPH.IndentPartDetails
                        cEEPO = New CriteriaComposite(New Criteria(GetType(EstimationEquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        cEEPO.opAnd(New Criteria(GetType(EstimationEquipPO), "IndentPartDetail.ID", MatchType.Exact, oIPD.ID))
                        aEEPO = oEEPOFac.Retrieve(cEEPO)
                        For Each oEEPO As EstimationEquipPO In aEEPO
                            oEED = oEEPO.EstimationEquipDetail
                            oEED.Status = 1
                            oEEDFac.Update(oEED)
                        Next
                    Next
                End If
                objIPHFac.RecordStatusChangeHistory(objIPH, OldStatus)   ' oldstatus)
                If oDealer.Title = EnumDealerTittle.DealerTittle.KTB AndAlso EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Tolak Then
                    objESPPOI = objESPPOIFac.RetrieveByHeaderID(objIPH.ID)
                    If IsNothing(objESPPOI) Then
                        objESPPOI = New EquipSPPOInfo
                        objESPPOI.Description = hdnRejectDesc.Value.Trim
                        objESPPOIFac.Insert(objESPPOI)
                    Else
                        objESPPOI.Description = hdnRejectDesc.Value.Trim
                        objESPPOIFac.Update(objESPPOI)
                    End If
                End If

                'Start  :RemainModule-IndentPart:Deliver Email Notification:dna:20091208
                If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    Select Case ddlStatus.SelectedValue
                        Case EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Batal
                        Case EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Proses
                        Case EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Rilis
                            sendApprovedEmail(objIPH)
                        Case EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Selesai
                        Case EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Tolak
                            sendRejectEmail(objIPH)
                    End Select
                Else
                    Select Case ddlStatus.SelectedValue
                        Case EnumIndentPartEquipStatus.EnumUpdateStatusDealer.Kirim
                            If objIPH.PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_B Then
                                'sendDeposit_B(objIPH)
                                Dim oFPEE As New FrmPoEstimationEquip
                                oFPEE.sendDeposit_B(objIPH)
                            ElseIf objIPH.PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_C Then
                                'sendDeposit_C(objIPH)
                                Dim oFPEE As New FrmPoEstimationEquip
                                oFPEE.sendDeposit_C(objIPH)
                            End If
                        Case EnumIndentPartEquipStatus.EnumUpdateStatusDealer.Batal
                            'Do Nothing
                    End Select
                End If
                'End    :RemainModule-IndentPart:Deliver Email Notification:dna:20091208

            Next
            BindToGrid(dtgEquipPO.CurrentPageIndex)
            MessageBox.Show("Ubah status " & ddlStatus.SelectedItem.Text & " berhasil")
        End If
    End Function

    Private Sub UpdateToExpired()
        Dim fcdIndenPartH As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)
        Dim objIndentPartH As IndentPartHeader

        Dim arlEquipPo As ArrayList = equippof.Retrieve(_sesshelper.GetSession(_szSsCrits))
        If Not IsNothing(arlEquipPo) AndAlso arlEquipPo.Count > 0 Then
            For Each dataExp As v_EquipPO In arlEquipPo
                objIndentPartH = fcdIndenPartH.Retrieve(dataExp.ID)
                If Not IsNothing(objIndentPartH) AndAlso objIndentPartH.PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_B _
                    AndAlso (objIndentPartH.Status = EnumIndentPartEquipStatus.EnumStatusDealer.Baru Or objIndentPartH.Status = EnumIndentPartEquipStatus.EnumStatusDealer.Kirim) Then
                    Dim dtDepositBPencairanHeader As DepositBPencairanHeader = New DepositBPencairanHeaderFacade(User).RetrieveIPHeader(objIndentPartH.ID)
                    If IsNothing(dtDepositBPencairanHeader) Or dtDepositBPencairanHeader.ID < 0 Then
                        If dataExp.CreatedTime.AddDays(14) < Date.Now Then
                            objIndentPartH.Status = EnumIndentPartEquipStatus.EnumStatusDealer.Expired
                            objIndentPartH.StatusKTB = EnumIndentPartEquipStatus.EnumStatusKTB.Expired
                            fcdIndenPartH.Update(objIndentPartH)
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Private Function sendExpiredEmail(ByVal objSppo As IndentPartHeader)
        oDealer = _sesshelper.GetSession("DEALER")
        Dim euf As EquipUserFacade = New EquipUserFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim mail As DNetMail = New DNetMail(smtp)
        Dim szTos As String ' = euf.CreateEmailToString(EquipUser.EquipUserGroup.Reject)
        Dim szCcs As String ' = euf.CreateEmailCcString(EquipUser.EquipUserGroup.Reject)
        Dim szItems As String = ""
        Dim objEED As EstimationEquipDetail
        Dim myculture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ID-id")
        Dim ItemPrice As Double = 0
        Dim TotalPrice As Double = 0 'ok
        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objSppo.RequestDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

        CommonFunction.SetIndentPartEmailRecipient(EquipUser.EquipUserGroup.Reject, szTos, szCcs, objSppo.Dealer)
        For Each objItem As IndentPartDetail In objSppo.IndentPartDetails
            objEED = GetEED(objItem)
            szItems &= "<tr>"
            szItems &= String.Format("<td>{0}</td>", objItem.SparePartMaster.PartNumber)
            szItems &= String.Format("<td align='center'>{0}</td>", objItem.Qty)
            szItems &= String.Format("<td align='right'>{0}</td>", FormatNumber(objItem.Price, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            szItems &= String.Format("<td align='right'>{0}</td>", FormatNumber(objEED.Discount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            ItemPrice = (objItem.Price * objItem.Qty) - (objEED.Discount / 100) * (objItem.Price * objItem.Qty)
            szItems &= String.Format("<td align='right'>{0}</td>", FormatNumber(ItemPrice, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            TotalPrice += ItemPrice
            szItems &= "</tr>"
        Next
        'Dim TotalPPN As Decimal = 0.1 * TotalPrice
        Dim TotalPPN As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=TotalPrice)
        szItems += "<tr>"
        szItems += "<td colspan='3' rowspan='3'>. &nbsp;</td>"
        szItems += "<td><b>Total Order</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(TotalPrice, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        szItems += "<tr>"
        szItems += "<td><b>PPN</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        szItems += "<tr>"
        szItems += "<td><b>Total Tagihan</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(TotalPrice + TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"

        Dim objESPPOI As EquipSPPOInfo = New EquipSPPOInfoFacade(User).RetrieveByHeaderID(objSppo.ID)
        Dim sReason As String = IIf(IsNothing(objESPPOI), "", objESPPOI.Description)

        Dim sCC As String = ""

        Dim EmailIndentCC1 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC1")
        Dim EmailIndentCC2 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC2")
        Dim EmailIndentCC3 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC3")
        Dim EmailIndentCC4 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC4")
        Dim EmailIndentCC5 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC5")

        sCC &= "<tr><td>" & EmailIndentCC1 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC2 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC3 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC4 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC5 & "</td></tr>"

        sCC = Now.ToString("dd MMM yyyy")
        'Dim szFormats() As String = {objSppo.Dealer.DealerName, objSppo.Dealer.Address, objSppo.RequestNo, String.Format("Rp.{0}", objSppo.AmountBeforeTax.ToString("#,##0")), sReason, szItems}
        Dim szFormats() As String = {objSppo.Dealer.DealerCode _
        , objSppo.Dealer.DealerName _
        , objSppo.Dealer.City.CityName _
        , Format(objSppo.RequestDate, "dd/MM/yyyy") _
        , objSppo.RequestNo _
        , objSppo.Dealer.DealerName, objSppo.Dealer.Address, objSppo.Dealer.City.CityName, objSppo.RequestNo, sReason, szItems, sCC}
        mail.sendMail(Server.MapPath(TEMP_EMAIL_REJECT), szTos, szCcs, "[MMKSI-DNet] Parts - Expired " & objSppo.RequestNo & " - " & objSppo.Dealer.DealerCode, szFormats)
    End Function


#End Region

End Class


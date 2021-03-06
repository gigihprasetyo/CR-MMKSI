#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.IndentPart
Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.io
Imports System.text
#End Region

Public Class FrmSPPOEquipList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtPONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTransferStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents TxtRequestNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgPO As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSubmit As System.Web.UI.WebControls.Button
    Protected WithEvents Download As System.Web.UI.WebControls.Button
    Protected WithEvents icPODateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPODateUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtSoNumber As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private sHelper As SessionHelper = New SessionHelper

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanPartIndentPartCreate_Privilege) Then
            'Server.Transfer("../FrmAccessDenied.aspx?modulName=INDENT PART - Daftar PO Indent Part")
        End If
        If Not SecurityProvider.Authorize(context.User, SR.Lihat_daftar_po_indent_part_equipment_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=INDENT PART - Daftar PO")
        End If
        If SecurityProvider.Authorize(context.User, SR.Lihat_daftar_po_indent_part_equipment_privilege) Then
            Download.Visible = True
        Else
            Download.Visible = False
        End If
        If SecurityProvider.Authorize(context.User, SR.Transfer_sap_po_indent_part_equipment_privilege) Then
            btnSubmit.Visible = True
        Else
            btnSubmit.Visible = False
        End If
    End Sub

    Dim bCek As Boolean = SecurityProvider.Authorize(context.User, SR.POIndentPartListTransferSAP_Privilege)
#End Region

#Region "private function"

    Private Sub bindDDL()
        ddlTransferStatus.Items.Add(New ListItem("Silakan Pilih", ""))
        ddlTransferStatus.Items.Add(New ListItem("Belum Transfer", "0"))
        ddlTransferStatus.Items.Add(New ListItem("Sudah Transfer", "1"))
        ddlTransferStatus.SelectedIndex = 0
    End Sub

    Private Sub CreateCriteria()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_EquipSPPOIndent), "OrderType", MatchType.Exact, "I"))

        If txtPONumber.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(v_EquipSPPOIndent), "PONumber", MatchType.[Partial], txtPONumber.Text.Trim))
        End If

        If TxtRequestNo.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(v_EquipSPPOIndent), "RequestNo", MatchType.[Partial], TxtRequestNo.Text.Trim))
        End If

        If txtSoNumber.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(v_EquipSPPOIndent), "SONumber", MatchType.[Partial], txtSoNumber.Text.Trim))
        End If

        If txtDealerCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(v_EquipSPPOIndent), "DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
        End If

        If ddlTransferStatus.SelectedValue = "1" Then
            criterias.opAnd(New Criteria(GetType(v_EquipSPPOIndent), "IndentTransfer", MatchType.Exact, 1))
        ElseIf ddlTransferStatus.SelectedValue = "0" Then
            criterias.opAnd(New Criteria(GetType(v_EquipSPPOIndent), "IndentTransfer", MatchType.No, 1))
        End If

        criterias.opAnd(New Criteria(GetType(v_EquipSPPOIndent), "PODate", MatchType.GreaterOrEqual, icPODateFrom.Value))
        criterias.opAnd(New Criteria(GetType(v_EquipSPPOIndent), "PODate", MatchType.LesserOrEqual, icPODateUntil.Value.AddDays(1)))

        sHelper.SetSession("crits", criterias)
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        sHelper.SetSession("arlPO", New KTB.DNet.BusinessFacade.IndentPartEquipment.v_EquipSPPOIndentFacade(User).RetrieveActiveListByCriteria(sHelper.GetSession("crits"), indexPage + 1, dgPO.PageSize, totalRow, sHelper.GetSession("SortCol"), sHelper.GetSession("SortDirect")))
        dgPO.CurrentPageIndex = indexPage
        dgPO.DataSource = sHelper.GetSession("arlPO")
        dgPO.VirtualItemCount = totalRow
        dgPO.DataBind()
        If (totalRow = 0) Then
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub TransferSAP(ByVal isReTransfer As Boolean)
        Dim IpFacade As SparePartPOFacade = New SparePartPOFacade(User)
        Dim arlToSubmit As ArrayList = New ArrayList
        For Each item As DataGridItem In dgPO.Items
            Dim chkItemChecked As CheckBox = item.FindControl("chkItemChecked")

            If chkItemChecked.Checked Then
                Dim objView As v_EquipSPPOIndent = CType(sHelper.GetSession("arlPO"), ArrayList)(item.ItemIndex)
                Dim obj As SparePartPO = IpFacade.Retrieve(objView.ID)
                If isReTransfer Then
                    If obj.IndentTransfer = 1 Then
                        arlToSubmit.Add(obj)
                    End If
                Else
                    If obj.IndentTransfer <> 1 Then
                        arlToSubmit.Add(obj)
                    End If
                End If
            End If
        Next

        If arlToSubmit.Count = 0 Then
            MessageBox.Show("Tidak ada data yang dipilih")
            Return
        End If

        Dim FOLDER_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\Sparepart\IndentPart"
        Dim FILE_NAME As String = ""

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _sapServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _sapServer)
        Dim succes As Boolean = False
        Dim msg As String = String.Empty

        succes = imp.Start()

        If succes Then
            Dim counter As Integer = 0
            Dim result As Integer = 0

            Dim objSparePartPOFacade As New SparePartPOFacade(User)
            For Each item As SparePartPO In arlToSubmit
                counter += 1
                Dim objInserted As SparePartPO = IpFacade.Retrieve(item.ID)
                FILE_NAME = FOLDER_NAME + "\T" + objInserted.PONumber + DateTime.Now.ToString("MMyymmss") + ".IDP"
                CreateFolder(FOLDER_NAME)
                Dim fs As FileStream = New FileStream(FILE_NAME, FileMode.CreateNew)
                Dim w As StreamWriter = New StreamWriter(fs)
                WritePOHeaderToFile(w, item)
                WritePODetailToFile(w, item)
                w.Close()
                fs.Close()
                item.ProcessCode = "S"
                item.IndentTransfer = 1
                Dim rslt As Integer = objSparePartPOFacade.Update(item)
            Next

            imp.StopImpersonate()
            imp = Nothing

            If isReTransfer Then
                MessageBox.Show(counter.ToString & " file berhasil dibuat ulang")
            Else
                MessageBox.Show(counter.ToString & " file berhasil dibuat")
            End If

        Else
            MessageBox.Show("Proses Gagal, Login Server SAP Failed")
        End If
    End Sub

    Private Sub CreateFolder(ByVal folderName As String)
        Dim dirInfo As DirectoryInfo = New DirectoryInfo(folderName)
        If Not dirInfo.Exists Then
            dirInfo.Create()
        End If
    End Sub

    'Start  :RemainModule-IndentPart:update confirmedDate:by dna:20091201
    Private Function GetConfirmedDate(ByVal sppo As SparePartPO) As String
        Dim Sql As String = ""
        Dim objEEDFac As EstimationEquipDetailFacade = New EstimationEquipDetailFacade(User)
        Dim arlEED As New ArrayList
        Dim crtEED As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim objEED As EstimationEquipDetail



        Sql &= "    select distinct(eed.ID) "
        Sql &= "	from SparePartPO sppo		"
        Sql &= "		, SparePartPODetail sppod	"
        Sql &= "		, IndentPartPO ippo	"
        Sql &= "		, IndentPartDetail ipd	"
        Sql &= "		, EstimationEquipPO eepo	"
        Sql &= "		, EstimationEquipDetail eed	"
        Sql &= "	where 1=1		"
        Sql &= "		and sppo.PONumber ='" & sppo.PONumber & "'"
        Sql &= "		/*and sppod.id=3855467*/	"
        Sql &= "		and sppod.SparePartPOID=sppo.ID	"
        Sql &= "		and ippo.SparePartPODetailID=sppod.ID	"
        Sql &= "		and ippo.IndentPartDetailID=ipd.ID	"
        Sql &= "		and eepo.IndentPartDetailID=ipd.ID	"
        Sql &= "		and eed.ID=eepo.EstimationEquipDetailID	"

        crtEED.opAnd(New Criteria(GetType(EstimationEquipDetail), "ID", MatchType.InSet, "(" & Sql & ")"))
        arlEED = objEEDFac.Retrieve(crtEED)
        If arlEED.Count = 0 Then
            Return ""
        Else
            Return Format(CType(arlEED(0), EstimationEquipDetail).ConfirmedDate, "yyyyMMdd")
        End If
    End Function
    'End    :RemainModule-IndentPart:update confirmedDate:by dna:20091201

    Private Sub WritePOHeaderToFile(ByRef w As StreamWriter, ByVal objHeader As SparePartPO)
        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim pad As Char = " "
        sbSetARecord.Append("T")
        sbSetARecord.Append(objHeader.PONumber.PadRight(15, pad))
        sbSetARecord.Append(Left(objHeader.Dealer.DealerName, 25).PadRight(25, pad))
        sbSetARecord.Append(String.Format("{0:yyyyMMdd}", objHeader.PODate))
        sbSetARecord.Append(objHeader.SparePartPODetails.Count.ToString.PadLeft(4, "0"))
        'Start  :RemainModule-IndentPart:update confirmedDate:by dna:20091201
        sbSetARecord.Append(GetConfirmedDate(objHeader))
        w.WriteLine(sbSetARecord.ToString)
        Exit Sub
        'End    :RemainModule-IndentPart:update confirmedDate:by dna:20091201

        Dim objv As v_EquipSPPOIndent = New v_EquipSPPOIndentFacade(User).Retrieve(objHeader.PONumber)
        If (Not IsNothing(objv)) Then
            Dim objv2 As v_EquipPO = New v_EquipPOFacade(User).Retrieve(objv.RequestNo)
            If (Not IsNothing(objv2)) Then
                Dim objEs As EstimationEquipHeader = New EstimationEquipHeaderFacade(User).Retrieve(objv2.EstimationNumber)
                If (Not IsNothing(objEs)) Then
                    If (Not IsNothing(objEs.EstimationEquipDetails)) Then
                        If (objEs.EstimationEquipDetails.Count > 0) Then
                            Dim objEd As EstimationEquipDetail = CType(objEs.EstimationEquipDetails(0), EstimationEquipDetail)
                            If (Not IsNothing(objEd)) Then

                                Dim szPricingDate As String = objEd.ConfirmedDate.ToString("yyyyMMdd")
                                sbSetARecord.Append(szPricingDate)
                            End If
                        End If
                    End If
                End If
            End If
        End If

        w.WriteLine(sbSetARecord.ToString)
    End Sub

    Private Function WritePODetailToFile(ByRef w As StreamWriter, ByVal objHeader As SparePartPO)
        Dim _arlPODetail As ArrayList = objHeader.SparePartPODetails
        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim pad As Char = " "
        Dim indek As Integer = 0
        For Each objPODetail As SparePartPODetail In _arlPODetail
            indek = indek + 1
            sbSetARecord.Remove(0, sbSetARecord.Length)
            sbSetARecord.Append("D")
            sbSetARecord.Append(objPODetail.SparePartPO.PONumber.PadRight(15, pad))
            sbSetARecord.Append(objPODetail.SparePartMaster.PartNumber.PadRight(20, pad))
            sbSetARecord.Append(objPODetail.Quantity.ToString.PadLeft(5, "0"))
            sbSetARecord.Append(String.Format("{0:yyyyMMdd}", objPODetail.SparePartPO.PODate)) '(objPODetail.SparePartPO.PODate.ToString.Format("{0:yyyyMMdd}"))
            sbSetARecord.Append(indek.ToString.PadLeft(4, "0"))
            w.WriteLine(sbSetARecord.ToString)
        Next
    End Function

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            sHelper.SetSession("SortCol", "PODate")
            sHelper.SetSession("SortDirect", Sort.SortDirection.ASC)
            bindDDL()
            btnSubmit.Enabled = bCek
            dgPO.Columns(1).Visible = bCek
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        CreateCriteria()
        BindDataGrid(0)
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        TransferSAP(False)
        CreateCriteria()
        BindDataGrid(0)
    End Sub

    Private Sub dgPO_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPO.SortCommand
        If CType(sHelper.GetSession("SortCol"), String) = e.SortExpression Then
            Select Case CType(sHelper.GetSession("SortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    sHelper.SetSession("SortDirect", Sort.SortDirection.DESC)

                Case Sort.SortDirection.DESC
                    sHelper.SetSession("SortDirect", Sort.SortDirection.ASC)
            End Select
        Else
            sHelper.SetSession("SortCol", e.SortExpression)
            sHelper.SetSession("SortDirect", Sort.SortDirection.ASC)
        End If
        BindDataGrid(dgPO.CurrentPageIndex)
    End Sub

    Private Sub dgPO_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPO.PageIndexChanged
        dgPO.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgPO.CurrentPageIndex)
    End Sub

    Private Sub dgPO_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPO.ItemDataBound
        If e.Item.ItemIndex <= -1 Then Return

        e.Item.Cells(2).Text = e.Item.ItemIndex + 1 + (dgPO.CurrentPageIndex * dgPO.PageSize)
        Dim itemPO As v_EquipSPPOIndent = e.Item.DataItem
        Dim lblNoPO As Label = e.Item.FindControl("lblNoPO")
        lblNoPO.Attributes.Add("OnClick", "ShowPODetail(" & itemPO.ID.ToString & ");")

        Dim lblRequestNo As Label = e.Item.FindControl("lblRequestNo")
        lblRequestNo.Attributes.Add("OnClick", "showPopUp('../IndentPartEquipment/FrmPoEstimationEquip.aspx?SPPOID=" & itemPO.IndentPartHeaderID & "&view=true&isPopUp=1','',600,780,null);")

        Dim lblAmount As Label = e.Item.FindControl("lblAmount")
        Dim objSPPO As SparePartPO = New SparePartPOFacade(User).Retrieve(itemPO.PONumber)
        lblAmount.Text = objSPPO.ItemAmount.ToString("#,##0")

        Dim lblDocFlow As Label = e.Item.FindControl("lblDocFlow")
        lblDocFlow.Attributes.Add("onclick", String.Format("javascript:ShowDocFlow('{0}')", itemPO.PONumber))

    End Sub

    Private Sub Download_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Download.Click
        Dim SW As StreamWriter
        Dim _filename As String = String.Format("{0}{1}{2}", "SparePartPOIndentPartEquipment", Date.Now.ToString("ddMMyyyyHHmmss"), ".xls")
        Dim _destFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("IndentPartDownload") & "\" & _filename  '-- Destination file
        Dim _spliterChr As Char = Chr(9)
        Dim _connected As Boolean = False
        Dim _success As Boolean = False
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Dim vsif As v_EquipSPPOIndentFacade = New v_EquipSPPOIndentFacade(User)
        Dim iphf As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)

        'get checked in grid
        Dim i As Integer = 0
        For Each item As DataGridItem In dgPO.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If chk.Checked Then
                If Not _connected Then
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
                                If (CType(sHelper.GetSession("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB) Then
                                    SW.WriteLine(String.Format("PO Number{0}Dealer Code{0}Dealer Name{0}Part Number{0}Part Name{0}Quantity{0}Price{0}Request Number{0}Indent Part Request Date{0}Price Confirm Date{0}Remain Qty{0}Vendor Code{0}Status", _spliterChr))
                                Else
                                    SW.WriteLine(String.Format("PO Number{0}Dealer Code{0}Dealer Name{0}Part Number{0}Part Name{0}Quantity{0}Price{0}Request Number{0}Indent Part Request Date{0}Price Confirm Date{0}Remain Qty{0}Status", _spliterChr))
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        imp.StopImpersonate()
                        imp = Nothing
                        Throw ex
                        Exit Sub
                    End Try
                End If

                Dim objEqpPo As v_EquipSPPOIndent = vsif.Retrieve(CInt(item.Cells(0).Text))
                Dim iph As IndentPartHeader = iphf.Retrieve(objEqpPo.RequestNo)

                For Each itemdetail As IndentPartDetail In iph.IndentPartDetails
                    Dim szRow As String = ""
                    If (CType(sHelper.GetSession("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB) Then
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
                            objEqpPo.PONumber, _
                            objEqpPo.DealerCode, _
                            objEqpPo.DealerName, _
                            itemdetail.SparePartMaster.PartNumber, _
                            itemdetail.SparePartMaster.PartName, _
                            itemdetail.Qty, _
                            itemdetail.Price, _
                            objEqpPo.RequestNo, _
                            iph.CreatedTime.ToString("dd MMM yyyy"), _
                            objEqpPo.CreatedTime.ToString("dd MMM yyyy"), _
                            (itemdetail.Qty - itemdetail.AllocationQty), _
                            itemdetail.SparePartMaster.SupplierCode, _
                            iph.StatusKTBDesc, _
                            _spliterChr)
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
                            objEqpPo.PONumber, _
                            objEqpPo.DealerCode, _
                            objEqpPo.DealerName, _
                            itemdetail.SparePartMaster.PartNumber, _
                            itemdetail.SparePartMaster.PartName, _
                            itemdetail.Qty, _
                            itemdetail.Price, _
                            objEqpPo.RequestNo, _
                            iph.CreatedTime.ToString("dd MMM yyyy"), _
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
            imp.StopImpersonate()
            imp = Nothing
            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("IndentPartDownload") & "\" & _filename
            Response.Redirect("../Download.aspx?file=" & PathFile, True)
        Else
            MessageBox.Show("Daftar Request Indent Part belum dipilih")
        End If
    End Sub

End Class

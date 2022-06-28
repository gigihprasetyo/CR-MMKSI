Imports Ktb.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports Ktb.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports System.IO
Imports System.Text
Imports System.Web.UI.WebControls
Imports System.Runtime.InteropServices

Public Class FrmListEstimationEquip
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents imgIndikator As System.Web.UI.WebControls.Image
    Protected WithEvents Image1 As System.Web.UI.WebControls.Image
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoPO As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpPONo As System.Web.UI.WebControls.Label
    Protected WithEvents lstStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents icPODateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPODateUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgIndentPart As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlupdatestatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents lblTotalAmount As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalOrder As System.Web.UI.WebControls.Label
    Protected WithEvents lblGrandTotal As System.Web.UI.WebControls.Label
    Protected WithEvents pnlStatus As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim oDealer As Dealer
    Private arlEstimationEquip As ArrayList = New ArrayList
    Private _sesshelper As New SessionHelper

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckUserPrivilege()

        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        oDealer = CType(_sesshelper.GetSession("DEALER"), Dealer)
        If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            InitiateAuthorization()
            txtDealerCode.Text = oDealer.DealerCode
            txtDealerCode.Enabled = False
            txtDealerCode.BorderStyle = BorderStyle.None
            lblSearchDealer.Visible = False
            lblPopUpPONo.Attributes("onclick") = "showPopUp('../PopUp/PopUpEstimationEquip.aspx?DealerID=" & oDealer.ID & "', '', 500, 600,PONOSelection);"
        ElseIf (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            InitiateAuthorization()
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            lblPopUpPONo.Attributes("onclick") = "showPopUp('../PopUp/PopUpEstimationEquip.aspx', '', 500, 600,PONOSelection);"
            txtDealerCode.Enabled = True
        End If

        If IsPostBack Then Exit Sub

        Dim arlitem As ArrayList = CType(_sesshelper.GetSession("ListIP"), ArrayList)
        Dim arlhdr As ArrayList = CType(_sesshelper.GetSession("ListHeader"), ArrayList)
        If (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            BindStatus(True)
        Else
            BindStatus(False)
        End If
        ViewState("currSortColumn") = "Status"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
        'BindToGrid(dtgIndentPart.CurrentPageIndex)
        'dtgIndentPart.DataSource = arlEstimationEquip
        'dtgIndentPart.DataBind()
        If Not IsNothing(arlhdr) Then
            txtDealerCode.Text = arlhdr(0)
            txtNoPO.Text = arlhdr(1)
            If arlhdr(2) <> String.Empty Then
                lstStatus.SelectedValue = arlhdr(2)
            End If
            icPODateFrom.Value = arlhdr(3)
            icPODateUntil.Value = arlhdr(4)
            btnSearch_Click(Nothing, Nothing)
        Else
            If (Not IsNothing(Request.QueryString("isBack"))) Then
                BindToGrid(dtgIndentPart.CurrentPageIndex)
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim idxPage As Integer = 0
        Dim arlhdr As ArrayList = CType(_sesshelper.GetSession("ListHeader"), ArrayList)
        If Not IsNothing(arlhdr) And IsNothing(sender) Then 'From back button
            idxPage = arlhdr(6)
        End If
        dtgIndentPart.CurrentPageIndex = idxPage
        CreateCriteria()
        BindToGrid(idxPage)
    End Sub

    Private Sub btnProses_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProses.Click
        'only for dealer
        If ddlupdatestatus.SelectedIndex = 0 Then
            MessageBox.Show("Pilih Status Dulu")
            Exit Sub
        End If

        If Not isVerifiedStatus() Then
            Exit Sub
        End If

        Dim arlToUpdate As ArrayList = New ArrayList
        Dim arlToEmail As ArrayList = New ArrayList

        For Each item As DataGridItem In dtgIndentPart.Items
            Dim chkItemChecked As CheckBox = item.FindControl("chkItemChecked")
            If chkItemChecked.Checked Then
                Dim objIPHeader As EstimationEquipHeader = CType(_sesshelper.GetSession("ListIP"), ArrayList)(item.ItemIndex)
                Dim szErrMsg As String = ""
                If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                    If (ddlupdatestatus.SelectedValue.ToLower() = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim) Then
                        If objIPHeader.Status <> EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal Then
                            arlToUpdate.Add(objIPHeader)
                        End If
                    ElseIf (ddlupdatestatus.SelectedValue.ToLower() = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal) Then
                        If objIPHeader.Status <> EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim Then
                            arlToUpdate.Add(objIPHeader)
                        End If
                    End If
                End If
            End If
        Next

        If arlToUpdate.Count = 0 Then
            If (ddlupdatestatus.SelectedValue.ToLower() = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim) Then
                MessageBox.Show("Tidak Ada Data Kirim Yang Dipilih")
            ElseIf (ddlupdatestatus.SelectedValue.ToLower() = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal) Then
                MessageBox.Show("Tidak Ada Data Status Tolak Yang Dipilih")
            End If
            Exit Sub
        End If

        Dim hf As EstimationEquipHeaderFacade = New EstimationEquipHeaderFacade(User)
        For Each itemToUpdate As EstimationEquipHeader In arlToUpdate
            Dim oldStatus As Integer = itemToUpdate.Status
            itemToUpdate.Status = Convert.ToInt32(ddlupdatestatus.SelectedValue)
            If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                If itemToUpdate.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru Then
                    itemToUpdate.CreatedTime = DateTime.Now
                End If
                Dim result As Integer = hf.Update(itemToUpdate)

                If itemToUpdate.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim Then
                    Dim objFrm As New FrmEstimationEquipment
                    objFrm.SendEmailEstimasi(itemToUpdate)
                End If

                hf.RecordStatusChangeHistory(itemToUpdate, oldStatus)
            End If
        Next

        BindToGrid(dtgIndentPart.CurrentPageIndex)
        MessageBox.Show("Ubah status " & ddlupdatestatus.SelectedItem.Text & " berhasil")
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim SW As StreamWriter
        Dim _filename As String = String.Format("{0}{1}{2}", "EstimationIndentPartEquipment", Date.Now.ToString("ddMMyyyyHHmmss"), ".xls")
        Dim _destFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("EstimationIndentPartDownload") & "\" & _filename  '-- Destination file
        Dim _spliterChr As Char = Chr(9)
        Dim _connected As Boolean = False
        Dim _success As Boolean = False
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate
        'get checked in grid
        Dim i As Integer = 0
        For Each item As DataGridItem In dtgIndentPart.Items
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
                                    SW.WriteLine(String.Format("Request Number{0}Dealer Code{0}Dealer Name{0}Part Number{0}Part Name{0}Quantity{0}Price{0}Estimation Number{0}Request Date{0}Price Confirm Date{0}Vendor Code{0}Status", _spliterChr))
                                Else
                                    SW.WriteLine(String.Format("Request Number{0}Dealer Code{0}Dealer Name{0}Part Number{0}Part Name{0}Quantity{0}Price{0}Estimation Number{0}Request Date{0}Price Confirm Date{0}Status", _spliterChr))
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        Throw ex
                        Exit Sub
                    End Try
                End If

                Dim ipFacade As EstimationEquipHeaderFacade = New EstimationEquipHeaderFacade(User)
                Dim iph As EstimationEquipHeader = ipFacade.Retrieve(CInt(item.Cells(0).Text))
                For Each itemdetail As EstimationEquipDetail In iph.EstimationEquipDetails
                    Dim szRow As String = ""
                    i += 1
                    If (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
                        Dim szFormat As String = "{0}{12}{1}{12}{2}{12}{3}{12}{4}{12}{5}{12}{6}{12}{7}{12}{8}{12}{9}{12}{10}{12}{11}"
                        szRow = String.Format(szFormat, i, iph.Dealer.DealerCode, iph.Dealer.DealerName, itemdetail.SparePartMaster.PartNumber, itemdetail.SparePartMaster.PartName, itemdetail.EstimationUnit, itemdetail.Harga, iph.EstimationNumber, iph.CreatedTime.ToString("dd MMM yyyy"), itemdetail.CreatedTime.ToString("dd MMM yyyy"), itemdetail.SparePartMaster.SupplierCode, iph.StatusDesc, _spliterChr)
                    Else
                        Dim szFormat As String = "{0}{11}{1}{11}{2}{11}{3}{11}{4}{11}{5}{11}{6}{11}{7}{11}{8}{11}{9}{11}{10}"
                        szRow = String.Format(szFormat, i, iph.Dealer.DealerCode, iph.Dealer.DealerName, itemdetail.SparePartMaster.PartNumber, itemdetail.SparePartMaster.PartName, itemdetail.EstimationUnit, itemdetail.Harga, iph.EstimationNumber, iph.CreatedTime.ToString("dd MMM yyyy"), itemdetail.CreatedTime.ToString("dd MMM yyyy"), iph.StatusDesc, _spliterChr)
                    End If
                    SW.WriteLine(szRow)
                Next
            End If
        Next

        If _success Then
            SW.Close()
            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("EstimationIndentPartDownload") & "\" & _filename
            imp.StopImpersonate()
            imp = Nothing
            Response.Redirect("../Download.aspx?file=" & PathFile, True)
        Else
            MessageBox.Show("Daftar Estimation Indent Part belum dipilih")
        End If
    End Sub

    Private Sub dtgIndentPart_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgIndentPart.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgIndentPart.PageSize * dtgIndentPart.CurrentPageIndex)).ToString
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim obj As EstimationEquipHeader = CType(e.Item.DataItem, EstimationEquipHeader)

            Dim lblNoRegKewajiban As Label = e.Item.FindControl("lblNoRegKewajiban")
            If Not IsNothing(obj.DepositBKewajibanHeader) AndAlso (obj.DepositBKewajibanHeader.ID > 0) Then
                lblNoRegKewajiban.Text = obj.DepositBKewajibanHeader.NoRegKewajiban
            End If

            If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = False
                If obj.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Selesai Or obj.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal Or obj.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru Then
                    CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
                    CType(e.Item.FindControl("lbtnDetail"), LinkButton).Visible = True
                Else
                    CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = True
                    CType(e.Item.FindControl("lbtnDetail"), LinkButton).Visible = False
                    If (obj.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim) Then
                        Dim lblStatus As Label = e.Item.FindControl("lblStatus")
                        lblStatus.Text = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru.ToString()
                    End If
                End If
            Else
                If obj.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Selesai Or obj.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal Or obj.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim Then
                    CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
                    CType(e.Item.FindControl("lbtnDetail"), LinkButton).Visible = True
                Else
                    CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = True
                    CType(e.Item.FindControl("lbtnDetail"), LinkButton).Visible = False
                End If
            End If

            Dim maxday As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("BlockedDaysKTBConfirm"))
            Dim confirmDate As Date = obj.CreatedTime
            Dim currentDate As Date = Date.Today
            Dim range As Date = currentDate.AddDays(-14)
            If confirmDate <> "1/1/1753" Then
                'Bug  1647
                If obj.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim Then
                    If range > confirmDate Then
                        'start  :by:dna;for:yurike;20100518(EstimationNumber = "TAEA010040001")
                        'CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
                        'e.Item.BackColor = Color.Red
                        If obj.EstimationNumber = "TAEA010040001" OrElse obj.EstimationNumber = "TAJF010040004" Then
                            'temporary allowed
                        Else
                            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
                            e.Item.BackColor = Color.Red
                            Dim lblStatus As Label = e.Item.FindControl("lblStatus")
                            lblStatus.Text = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Expired.ToString()
                            If oDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = True
                            End If
                        End If
                        'End    :by:dna;for:yurike;20100518
                    End If
                End If
            End If

            Dim lblLastUpdated As Label = e.Item.FindControl("lblLastUpdated")
            'lblLastUpdated.Attributes.Add("onclick", String.Format("javascript:ShowLastUpdatedHistory('{0}')", obj.EstimationNumber))
            Dim urlParams As String = ""

            urlParams &= "TableName=" & GetType(EstimationEquipHeader).Name
            urlParams &= "&TableID=" & obj.ID.ToString()
            urlParams &= "&TableCode=" & obj.EstimationNumber
            urlParams &= "&FieldName=" & "Status"
            lblLastUpdated.Attributes.Add("onclick", String.Format("javascript:ShowLastUpdatedHistory('{0}')", urlParams))
            Dim lblDocFlow As Label = e.Item.FindControl("lblDocFlow")
            lblDocFlow.Attributes.Add("onclick", String.Format("javascript:ShowDocFlow('{0}')", obj.EstimationNumber))


        End If
    End Sub

    Private Sub dtgIndentPart_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgIndentPart.ItemCommand
        If e.CommandName.ToUpper = "SORT" Then Return

        If (e.CommandName = "edit") Then
            Response.Redirect("../IndentPartEquipment/FrmDetailEstimationEquip.aspx?EstimationEquipHeaderID=" & e.CommandArgument & "&View=False")
            'If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            '    Response.Redirect("../IndentPartEquipment/FrmDetailEstimationEquip.aspx?EstimationEquipHeaderID=" & e.CommandArgument & "&View=False", True)
            'Else
            '    Response.Redirect("../IndentPartEquipment/FrmEstimationEquipment.aspx?EstimationEquipHeaderID=" & e.CommandArgument & "&View=False", True)
            'End If
        ElseIf (e.CommandName = "detail") Then
            Response.Redirect("../IndentPartEquipment/FrmDetailEstimationEquip.aspx?EstimationEquipHeaderID=" & e.CommandArgument & "&View=True")
            'If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            '    Response.Redirect("../IndentPartEquipment/FrmDetailEstimationEquip.aspx?EstimationEquipHeaderID=" & e.CommandArgument & "&View=True", True)
            'Else
            '    Response.Redirect("../IndentPartEquipment/FrmEstimationEquipment.aspx?EstimationEquipHeaderID=" & e.CommandArgument & "&View=True", True)
            'End If
        ElseIf (e.CommandName = "delete") Then
            Dim hf As EstimationEquipHeaderFacade = New EstimationEquipHeaderFacade(User)

            Dim dtEQHeader As EstimationEquipHeader = New EstimationEquipHeaderFacade(User).Retrieve(CType(e.CommandArgument, Integer))
            If dtEQHeader.ID > 0 Then
                dtEQHeader.RowStatus = -1
                hf.Update(dtEQHeader)
            End If
            BindToGrid(dtgIndentPart.CurrentPageIndex)
        End If

    End Sub

    Private Sub dtgIndentPart_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgIndentPart.PageIndexChanged
        dtgIndentPart.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgIndentPart.CurrentPageIndex)
    End Sub

    Private Sub dtgIndentPart_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgIndentPart.SortCommand
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
        BindToGrid(dtgIndentPart.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom Method"

    Private Sub BindStatus(ByVal Bval As Boolean)
        If Bval Then
            pnlStatus.Visible = False
            lstStatus.DataSource = New EnumEstimationEquipStatus().RetrieveTypeForKTB()
            lstStatus.DataTextField = "NameType"
            lstStatus.DataValueField = "ValType"
            lstStatus.DataBind()
        Else
            Dim arlListDealer As ArrayList = New EnumEstimationEquipStatus().RetrieveTypeUpdateForDealer()
            ddlupdatestatus.DataSource = arlListDealer
            ddlupdatestatus.DataTextField = "NameType"
            ddlupdatestatus.DataValueField = "ValType"
            ddlupdatestatus.DataBind()

            lstStatus.DataSource = New EnumEstimationEquipStatus().RetrieveTypeForDealer()
            lstStatus.DataTextField = "NameType"
            lstStatus.DataValueField = "ValType"
            lstStatus.DataBind()
        End If
    End Sub

    Private Sub BindToGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim xsum As EstimationEquipDetail

        arlEstimationEquip = New EstimationEquipHeaderFacade(User).RetrieveActiveList(_sesshelper.GetSession("crits"), currentPageIndex + 1, dtgIndentPart.PageSize, total, _
           CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dtgIndentPart.VirtualItemCount = total
        dtgIndentPart.DataSource = arlEstimationEquip
        dtgIndentPart.DataBind()
        _sesshelper.SetSession("ListIP", arlEstimationEquip)
        calculateTotals(arlEstimationEquip)
        If (total = 0) Then
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub CreateCriteria()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        oDealer = Session("DEALER")
        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then  '---User is login as Dealer
            criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))
            '-----criteria status untuk dealer
            If lstStatus.SelectedIndex <> -1 Then
                Dim li As ListItem
                Dim _status As String = "("
                For Each li In lstStatus.Items
                    If li.Selected Then
                        _status = _status & li.Value & ","
                    End If
                Next
                _status = _status.Substring(0, _status.Length - 1) & ")"
                criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "Status", MatchType.InSet, _status))
            End If
        Else '----User Login As KTB
            '-----criteria dealer
            If txtDealerCode.Text <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
            End If
            '----criteria Status untuk KTB
            If lstStatus.SelectedIndex <> -1 Then
                Dim li As ListItem
                Dim _status As String = "("
                For Each li In lstStatus.Items
                    If li.Selected Then
                        _status = _status & li.Value & ","
                    End If
                Next
                _status = _status.Substring(0, _status.Length - 1) & ")"
                criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "Status", MatchType.InSet, _status))
            Else
                criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "Status", MatchType.No, CInt(EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru)))
                criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "Status", MatchType.No, CInt(EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal)))
            End If
        End If

        If txtNoPO.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "EstimationNumber", MatchType.InSet, "('" & txtNoPO.Text.Replace(";", "','") & "')"))

        If icPODateFrom.Value <= icPODateUntil.Value Then
            criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "CreatedTime", MatchType.GreaterOrEqual, icPODateFrom.Value))
            criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "CreatedTime", MatchType.LesserOrEqual, icPODateUntil.Value.AddDays(1))) ' add days 1 krn gak mau tampilkan di item dgn tgl yg sama, hrs tambah 1 hari
        Else
            MessageBox.Show("Tanggal PO 'Dari' tidak boleh lebih Besar dari Tanggal PO 'Sampai' ")
            Exit Sub
        End If

        'Tipe Barang
        _sesshelper.SetSession("crits", criterias)
    End Sub

    Private Function isVerifiedStatus() As Boolean
        Dim isValid As Boolean = True
        Dim i As Integer = 0
        For Each item As DataGridItem In dtgIndentPart.Items
            i += 1
            Dim chkItemChecked As CheckBox = item.FindControl("chkItemChecked")
            If chkItemChecked.Checked Then
                Dim objIPHeader As EstimationEquipHeader = CType(_sesshelper.GetSession("ListIP"), ArrayList)(item.ItemIndex)
                'Check Status Hierarchy
                If ddlupdatestatus.SelectedValue = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal _
                    OrElse ddlupdatestatus.SelectedValue = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim Then
                    If objIPHeader.Status <> EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru Then
                        MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusDesc & " tidak bisa diubah")
                        Return False
                    End If
                End If

                Dim szErrMsg As String = ""
                If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                    If (ddlupdatestatus.SelectedValue.ToLower() = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim) Then
                        If objIPHeader.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusDesc & " tidak dapat diubah")
                            szErrMsg &= "Item " & i.ToString & " : Status " & objIPHeader.StatusDesc & " tidak dapat diubah" & "<br />"
                            isValid = False
                        End If
                    ElseIf (ddlupdatestatus.SelectedValue.ToLower() = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal) Then
                        If objIPHeader.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim Then
                            MessageBox.Show("Item " & i.ToString & " : Status " & objIPHeader.StatusDesc & " tidak dapat diubah")
                            szErrMsg &= "Item " & i.ToString & " : Status " & objIPHeader.StatusDesc & " tidak dapat diubah" & "<br />"
                            isValid = False
                        End If
                    End If
                End If
            End If
        Next
        Return isValid
    End Function

    Private Sub calculateTotals(ByVal arl As ArrayList)
        Dim totalAmount As Decimal = 0
        Dim totalItem As Integer = 0
        For Each headers As EstimationEquipHeader In arl
            totalAmount += headers.TotalAmount
            totalItem += headers.TotalQty
        Next
        lblTotalAmount.Text = String.Format("Total Amount: Rp.{0}", totalAmount.ToString("#,##0"))
        lblTotalOrder.Text = String.Format("Total Order: {0}", arl.Count)
        lblGrandTotal.Text = String.Format("Grand Total Item: {0}", totalItem.ToString())
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanPartIndentPartCreate_Privilege) Then
            'Server.Transfer("../FrmAccessDenied.aspx?modulName=Estimation Equipment Indent Part - Daftar Status")
        End If
    End Sub

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.Daftar_estimasi_indent_part_equipment_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Estimasi")
        End If
        If SecurityProvider.Authorize(context.User, SR.Ubah_status_estimasi_indent_part_equipment_privilege) Then
            btnProses.Visible = True
        Else
            btnProses.Visible = False
        End If
        If SecurityProvider.Authorize(context.User, SR.Daftar_estimasi_indent_part_equipment_privilege) Then
            btnDownload.Visible = True
        Else
            btnDownload.Visible = False
        End If
    End Sub

#End Region

End Class
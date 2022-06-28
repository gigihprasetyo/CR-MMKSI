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


Public Class FrmAlocEstimationEquip
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtPONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpPoNo As System.Web.UI.WebControls.Label
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents dtgEquipPO As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents icPODateFrom As Intimedia.WebCC.IntiCalendar
    Protected WithEvents icPODateUntil As Intimedia.WebCC.IntiCalendar
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents txtPartNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpSparePart As System.Web.UI.WebControls.Label

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
    Private _sesshelper As New SessionHelper
    Private _szSsDealer As String = "DEALER"
    Private _szSsCrits As String = "crits"
    Private _szSsListPoDetail As String = "sppodetail"
    Private _szSsListPoHeader As String = "sppoheader"
    Private sppof As SparePartPOFacade
    Private sppodetailf As SparePartPODetailFacade
    Private spmf As SparePartMasterFacade
    Private esteqpf As EstimationEquipHeaderFacade
    Private esteqpdetailf As EstimationEquipDetailFacade
    Private eqpof As EstimationEquipPOFacade
    Private detailalocf As EquipSPPOAlocationFacade
    Private vequippof As v_EquipPOFacade


#Region "event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        sppof = New SparePartPOFacade(User)
        sppodetailf = New SparePartPODetailFacade(User)
        spmf = New SparePartMasterFacade(User)
        esteqpf = New EstimationEquipHeaderFacade(User)
        esteqpdetailf = New EstimationEquipDetailFacade(User)
        eqpof = New EstimationEquipPOFacade(User)
        vequippof = New v_EquipPOFacade(User)
        detailalocf = New EquipSPPOAlocationFacade(User)

        oDealer = CType(_sesshelper.GetSession(_szSsDealer), Dealer)
        If (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            InitiateAuthorization()
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            Dim szProcessCodes As String = String.Format("{0},{1}", SPPOProcessCode.V_ORDER_TO_VENDOR, SPPOProcessCode.A_ALOCATION)
            lblPopUpPoNo.Attributes("onclick") = String.Format("showPopUp('../PopUp/PopUpSparePartPO.aspx?ProcessCodeInSet={0}','', 500, 600,PONOSelection);", szProcessCodes)
            lblPopUpSparePart.Attributes("onclick") = "ShowSparePartMasterSelection();"
            txtDealerCode.Enabled = True
        End If

        If IsPostBack Then Exit Sub

        ViewState("currSortColumn") = "Status"
        ViewState("currSortDirection") = Sort.SortDirection.ASC

        Dim arlEqpPo As ArrayList = CType(_sesshelper.GetSession(_szSsListPoDetail), ArrayList)
        dtgEquipPO.DataSource = arlEqpPo
        dtgEquipPO.DataBind()
    End Sub

    Private Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        If (txtDealerCode.Text = "") Then
            MessageBox.Show("Kode Dealer Belum Dimasukkan")
            Exit Sub
        End If

        oDealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SparePartPO), "Dealer", MatchType.Exact, oDealer.ID))
        If (txtPONumber.Text <> "") Then
            criterias.opAnd(sppof.createInSetCriteria(New ArrayList(txtPONumber.Text.Split(";")), "PONumber"))
        End If
        criterias.opAnd(New Criteria(GetType(SparePartPO), "CreatedTime", MatchType.GreaterOrEqual, CType(icPODateFrom.Value, Date)))
        criterias.opAnd(New Criteria(GetType(SparePartPO), "CreatedTime", MatchType.LesserOrEqual, CType(icPODateUntil.Value.AddDays(1), Date)))
        Dim arlSppo As ArrayList = sppof.Retrieve(criterias)

        Dim arlSppoDetail As ArrayList = New ArrayList
        For Each objSppo As SparePartPO In arlSppo
            If (objSppo.ProcessCode.ToLower() <> SPPOProcessCode.V_ORDER_TO_VENDOR.ToLower() And objSppo.ProcessCode.ToLower() <> SPPOProcessCode.A_ALOCATION.ToLower()) Then
                If (txtPONumber.Text <> "") Then
                    MessageBox.Show(String.Format("Nomor Pengajuan {0} Yang Anda Pilih Belum Berstatus Alokasi Sebagian atau Order To Vendor", objSppo.PONumber))
                    Return
                End If
            Else
                For Each objSppoDetail As SparePartPODetail In objSppo.SparePartPODetails
                    If (txtPartNumber.Text = "") Then
                        arlSppoDetail.Add(objSppoDetail)
                    Else
                        Dim arlPartNums As String() = txtPartNumber.Text.Split(";")
                        For Each szPartNums As String In arlPartNums
                            If (objSppoDetail.SparePartMaster.PartNumber.Trim().ToLower() = szPartNums.Trim().ToLower()) Then
                                arlSppoDetail.Add(objSppoDetail)
                            End If
                        Next
                    End If
                Next
            End If
        Next

        _sesshelper.SetSession(_szSsListPoDetail, arlSppoDetail)
        _sesshelper.SetSession(_szSsListPoHeader, arlSppo)
        _sesshelper.SetSession(_szSsCrits, criterias)

        Dim idxPage As Integer = 0
        dtgEquipPO.CurrentPageIndex = idxPage
        BindToGrid(idxPage)
        btnDownload.Enabled = True
        btnSave.Enabled = True
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim arlSppo As ArrayList = _sesshelper.GetSession(_szSsListPoHeader)
        For Each objSppo As SparePartPO In arlSppo

            Dim itemAloc As Integer = 0
            For Each item As DataGridItem In dtgEquipPO.Items

                Dim chkItemChecked As CheckBox = item.FindControl("chkItemChecked")
                If chkItemChecked.Checked Then
                    Dim intSppoDetailId As Integer = CType(item.Cells(0).Text, Integer)
                    Dim objSppoDetail As SparePartPODetail = sppodetailf.Retrieve(intSppoDetailId)
                    If (objSppoDetail.SparePartPO.ID = objSppo.ID) Then
                        Dim lblRemainQty As Label = CType(item.FindControl("lblRemainQty"), Label)
                        Dim lblOrderQty As Label = CType(item.FindControl("lblOrderQty"), Label)
                        Dim txtQtyAllocation As TextBox = CType(item.FindControl("txtQtyAllocation"), TextBox)
                        If (CInt(txtQtyAllocation.Text) > 0) Then
                            Dim objAloc As EquipSPPOAlocation = New EquipSPPOAlocation
                            objAloc.SparePartPODetail = objSppoDetail
                            objAloc.AlocationQty = CInt(txtQtyAllocation.Text)
                            objAloc.RemailQty = objSppoDetail.Quantity - objAloc.AlocationQty
                            objAloc.Note = ""
                            If (objAloc.AlocationQty > objSppoDetail.Quantity) Then
                                MessageBox.Show(String.Format("Alokasi qty tidak boleh lebih besar dari order qty pada nomor barang {0}", objSppoDetail.SparePartMaster.PartNumber))
                                Return
                            ElseIf (objAloc.AlocationQty = objSppoDetail.Quantity) Then
                                itemAloc += 1
                            End If
                            detailalocf.Insert(objAloc)
                        End If
                    End If
                End If

            Next
            If (itemAloc = objSppo.SparePartPODetails.Count) Then
                objSppo.ProcessCode = SPPOProcessCode.F_FINISH
            ElseIf (itemAloc < objSppo.SparePartPODetails.Count) And (itemAloc > 0) Then
                objSppo.ProcessCode = SPPOProcessCode.A_ALOCATION
            End If
            sppof.Update(objSppo)

        Next
        MessageBox.Show("Done")
    End Sub

    Private Sub btnGeneratePo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim SW As StreamWriter
        Dim _filename As String = String.Format("{0}{1}{2}", "SparePartPOEstimationIndentPartEquipment", Date.Now.ToString("ddMMyyyyHHmmss"), ".xls")
        Dim _destFile As String = ConfigurationSettings.AppSettings.Item("SAN") & ConfigurationSettings.AppSettings.Item("EstimationIndentPartDownload") & "\" & _filename  '-- Destination file
        Dim _spliterChr As Char = Chr(9)
        Dim _connected As Boolean = False
        Dim _success As Boolean = False

        'get checked in grid
        Dim i As Integer = 0
        For Each item As DataGridItem In dtgEquipPO.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If chk.Checked Then
                If Not _connected Then
                    Dim _user As String = ConfigurationSettings.AppSettings.Get("User")
                    Dim _password As String = ConfigurationSettings.AppSettings.Get("Password")
                    Dim _webServer As String = ConfigurationSettings.AppSettings.Get("WebServer") '172.17.104.204
                    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
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
                                    SW.WriteLine(String.Format("Indent Part Number{0}Dealer Code{0}Dealer Name{0}Part Number{0}Part Name{0}Quantity{0}Price{0}Estimation Number{0}Pricing Date{0}Indent Part Request Date{0}Price Confirm Date{0}Remain Qty{0}Vendor Code{0}Status", _spliterChr))
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        Throw ex
                        Exit Sub
                    End Try
                End If

                Dim intSppoDetailId As Integer = CType(item.Cells(0).Text, Integer)
                Dim itemdetail As SparePartPODetail = sppodetailf.Retrieve(intSppoDetailId)
                Dim objvequippo As v_EquipPO = vequippof.Retrieve(itemdetail.SparePartPO.ID)
                Dim objEstH As EstimationEquipHeader = esteqpf.Retrieve(objvequippo.EstimationNumber)
                Dim objEqpPo As EstimationEquipPO = eqpof.RetrieveBySparePartPODetailIDEstimationEquipHeaderID(itemdetail, objEstH.ID)
                Dim objequipdetail As EstimationEquipDetail = esteqpdetailf.Retrieve(objEqpPo.EstimationEquipDetail.ID)

                Dim totalaloc = detailalocf.CountSPPODetailTotalAloc(itemdetail.ID)
                Dim remainqty = itemdetail.Quantity - totalaloc

                Dim szRow As String = ""
                i += 1
                If (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
                    Dim szFormat As String = "{0}{14} " & _
                    "{1}{14}" & _
                    "{2}{14}" & _
                    "{3}{14}" & _
                    "{4}{14}" & _
                    "{5}{14}" & _
                    "{6}{14}" & _
                    "{7}{14}" & _
                    "{8}{14}" & _
                    "{9}{14}" & _
                    "{10}{14}" & _
                    "{11}{14}" & _
                    "{12}{14}" & _
                    "{13}{14}"
                    szRow = String.Format(szFormat, _
                        i, _
                        itemdetail.SparePartPO.Dealer.DealerCode, _
                        itemdetail.SparePartPO.Dealer.DealerName, _
                        itemdetail.SparePartMaster.PartNumber, _
                        itemdetail.SparePartMaster.PartName, _
                        itemdetail.Quantity, _
                        itemdetail.RetailPrice, _
                        objvequippo.EstimationNumber, _
                        itemdetail.CreatedTime.ToString("dd MMM yyyy"), _
                        objEstH.CreatedTime.ToString("dd MMM yyyy"), _
                        objequipdetail.ConfirmedDate.ToString("dd MMM yyyy"), _
                        remainqty, _
                        itemdetail.SparePartMaster.SupplierCode, _
                        objvequippo.ProcessDesc, _
                        _spliterChr)
                End If
                SW.WriteLine(szRow)
            End If
        Next

        If _success Then
            SW.Close()
            Dim PathFile As String = ConfigurationSettings.AppSettings.Item("EstimationIndentPartDownload") & "\" & _filename
            Response.Redirect("../Download.aspx?file=" & PathFile, True)
        Else
            MessageBox.Show("Daftar Estimation Indent Part belum dipilih")
        End If
    End Sub

    Private Sub dtgEquipPO_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEquipPO.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgEquipPO.PageSize * dtgEquipPO.CurrentPageIndex)).ToString
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim lblOrderQty As Label = CType(e.Item.FindControl("lblOrderQty"), Label)
            Dim lblRemainQty As Label = CType(e.Item.FindControl("lblRemainQty"), Label)
            Dim hdnRemainQty As HtmlInputHidden = CType(e.Item.FindControl("hdnRemainQty"), HtmlInputHidden)
            Dim txtQtyAllocation As TextBox = CType(e.Item.FindControl("txtQtyAllocation"), TextBox)
            txtQtyAllocation.Attributes.Add("onblur", String.Format("javascript:NumOnlyBlurWithOnGridTxtCustom('{0}','{1}','{2}','{3}');", txtQtyAllocation.ClientID, lblRemainQty.ClientID, lblOrderQty.ClientID, hdnRemainQty.ClientID))

            Dim objSppoDetail As SparePartPODetail = CType(e.Item.DataItem, SparePartPODetail)
            Dim totalaloc As Integer = detailalocf.CountSPPODetailTotalAloc(objSppoDetail.ID)
            If (totalaloc = 0) Then
                lblRemainQty.Text = "0"
                hdnRemainQty.Value = "0"
            ElseIf (totalaloc = objSppoDetail.Quantity) Then
                lblRemainQty.Text = totalaloc.ToString()
                hdnRemainQty.Value = totalaloc.ToString()
                txtQtyAllocation.Enabled = False
            ElseIf (totalaloc > objSppoDetail.Quantity) Then
                txtQtyAllocation.Enabled = False
                lblRemainQty.Text = "0"
                hdnRemainQty.Value = "0"
            Else
                lblRemainQty.Text = (objSppoDetail.Quantity - totalaloc).ToString()
                hdnRemainQty.Value = (objSppoDetail.Quantity - totalaloc).ToString()
            End If

        End If
    End Sub

    Private Sub dtgEquipPO_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEquipPO.ItemCommand
        If e.CommandName.ToUpper = "SORT" Then Return

        If e.CommandName = "Edit" Then
            dtgEquipPO.EditItemIndex = e.Item.ItemIndex
            BindToGrid(dtgEquipPO.CurrentPageIndex)
        End If

        If e.CommandName = "Cancel" Then
            dtgEquipPO.EditItemIndex = -1
            BindToGrid(dtgEquipPO.CurrentPageIndex)
        End If

        If e.CommandName = "Save" Then
            Dim lblRemain As Label = e.Item.FindControl("lblRemainQty")
            Dim lblTxtAlokasi As TextBox = e.Item.FindControl("txtQtyAllocation")
            If CInt(Val(lblTxtAlokasi.Text)) > CInt(lblRemain.Text) Then
                MessageBox.Show("Quantity Alokasi Tidak Boleh Melebihi Qty Sisa")
                Exit Sub
            End If
            dtgEquipPO.EditItemIndex = -1
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

#End Region

#Region "function"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanPartIndentPartCreate_Privilege) Then
            'Server.Transfer("../FrmAccessDenied.aspx?modulName=Estimation Indent Part Equipment - Alokasi Estimasi Indent Part Equipment")
        End If
    End Sub

    Private Sub BindToGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim arlSppoDetail As ArrayList = _sesshelper.GetSession(_szSsListPoDetail)

        dtgEquipPO.VirtualItemCount = total
        dtgEquipPO.DataSource = arlSppoDetail
        dtgEquipPO.DataBind()
    End Sub

#End Region

End Class

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text
Imports System.Linq
Imports System.Collections

#End Region
Public Class FrmPPHStatus
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgPPHStatus As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents ICStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ICEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnTransfer As System.Web.UI.WebControls.Button
    Protected WithEvents lblGenerate As System.Web.UI.WebControls.Label
    Protected WithEvents btnProcess As System.Web.UI.WebControls.Button
    Protected WithEvents txtNoReg As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoBukti As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlProcess As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Declaration"
    Private sessionHelper As New SessionHelper
    Private objDealer As Dealer
    Private IsAllow2Process As Boolean
    Private IsAllow2Edit As Boolean
    Private IsAllow2Transfer As Boolean
#End Region

#Region "Event"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        objDealer = sessionHelper.GetSession("DEALER")
        CheckUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
    End Sub

    Private Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        If ddlProcess.SelectedValue <> -1 Then
            SetProcess(ddlProcess.SelectedValue)
        Else
            MessageBox.Show("Pilih status terlebih dahulu")
        End If
    End Sub

    Private Sub btnTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransfer.Click
        Dim strIDs As String = GetSelectedIDs(dtgPPHStatus)
        If strIDs = ")" Then
            MessageBox.Show("Tidak ada data yang dipilih")
            Exit Sub
        End If

        If strIDs.Length > 0 Then
            Upload2SAP(strIDs)
        Else
            MessageBox.Show("Pilih kuitansi terlebih dahulu!")
        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim arlPPH As New ArrayList
        arlPPH = GetDataPPH()
        Download(arlPPH)
    End Sub

    Private Sub dtgPPHStatus_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPPHStatus.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If Not e.Item.DataItem Is Nothing Then
                Dim objPFRH As ParkingFeeReturnHeader = e.Item.DataItem
                e.Item.Cells(2).Text = e.Item.ItemIndex + 1 + (dtgPPHStatus.CurrentPageIndex * dtgPPHStatus.PageSize)

                Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
                lblID.Text = objPFRH.ID

                Dim lblStatusID As Label = CType(e.Item.FindControl("lblStatusID"), Label)
                lblStatusID.Text = objPFRH.Status

                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                lblStatus.Text = CType(objPFRH.Status, EnumPengembalianPPhStatus.PengembalianPPhStatus).ToString.Replace("_", " ")

                Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
                lblKodeDealer.Text = objPFRH.Dealer.DealerCode
                lblKodeDealer.ToolTip = objPFRH.Dealer.DealerName

                'PopUpPajakPenalyParkir.aspx
                Dim lblSurat As Label = CType(e.Item.FindControl("lblSurat"), Label)
                lblSurat.ToolTip = "Bukti Potong PPh"
                lblSurat.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpPajakPenalyParkir.aspx?id=" & lblID.Text, "scrollbars=auto", 1100, 800, "DealerSelection")

                'PopUpPajakPenalyParkir.aspx
                Dim lblInterest As Label = CType(e.Item.FindControl("lblInterest"), Label)
                lblInterest.ToolTip = "Bukti Potong PPh"
                lblInterest.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpPajakInterest.aspx?id=" & lblID.Text, "scrollbars=auto", 1100, 800, "DealerSelection")

                Dim lblCreditAccount As Label = CType(e.Item.FindControl("lblCreditAccount"), Label)
                lblCreditAccount.Text = objPFRH.Dealer.CreditAccount
                '
                Dim lblTglKembali As Label = CType(e.Item.FindControl("lblTglKembali"), Label)
                lblTglKembali.Text = objPFRH.ReturnDate.ToString("dd/MM/yyyy")

                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                lbtnEdit.Attributes("OnClick") = "showPopUp('../PopUp/PopUpPengembalianPPHDetail.aspx?id=" & objPFRH.ID & "&edit=1 ','',500,760,'');"
                lbtnEdit.ToolTip = "Edit Detail"
                If objPFRH.Status = EnumPengembalianPPhStatus.PengembalianPPhStatus.Baru Then
                    If objPFRH.Dealer.CreditAccount = objDealer.CreditAccount Then
                        lbtnEdit.Visible = True
                    Else
                        lbtnEdit.Visible = False
                    End If

                Else
                    lbtnEdit.Visible = False
                End If

                Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
                lbtnView.Attributes("OnClick") = "showPopUp('../PopUp/PopUpPengembalianPPHDetail.aspx?id=" & objPFRH.ID & "&edit=0 ','',500,760,'');"
                lbtnView.ToolTip = "Lihat Detail"
                lbtnView.Visible = True

                Dim lbtnHistory As LinkButton = CType(e.Item.FindControl("lbtnHistory"), LinkButton)
                lbtnHistory.Attributes("OnClick") = "showPopUp('../PopUp/PopUpHistoryDepositABiayaParkir.aspx?Id=" & objPFRH.ID & " ','',500,760,'');"
                lbtnHistory.ToolTip = "Lihat History"

                If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    e.Item.FindControl("lbtnEdit").Visible = False
                End If

            End If
        End If
    End Sub

    'Private Sub dtgPPHStatus_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPPHStatus.ItemCommand
    '    If e.CommandName = "Edit" Then
    '        Response.Redirect("FrmSPKHeader.aspx?Id=" & e.Item.Cells(1).Text & "&Mode=1&isBack=0")
    '    ElseIf e.CommandName = "View" Then
    '        Response.Redirect("FrmSPKHeader.aspx?Id=" & e.Item.Cells(1).Text & "&Mode=2&isBack=0")
    '    End If
    'End Sub

    Private Sub dtgPPHStatus_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPPHStatus.SortCommand
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

        dtgPPHStatus.SelectedIndex = -1
        dtgPPHStatus.CurrentPageIndex = 0
        BindDataGrid(dtgPPHStatus.CurrentPageIndex)
    End Sub

    Private Sub dtgPPHStatus_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPPHStatus.PageIndexChanged
        dtgPPHStatus.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgPPHStatus.CurrentPageIndex)
    End Sub

    Private Sub dtgPPHStatus_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPPHStatus.ItemCommand
        If e.CommandName = "Edit" Then
            btnSearch_Click(Nothing, Nothing)
        End If
    End Sub
#End Region

#Region "Custom"
    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.parking_fee_daftar_status_lihat_privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=DO Status")
        End If
        IsAllow2Transfer = SecurityProvider.Authorize(Context.User, SR.parking_fee_daftar_status_transfer_ke_sap_privilege) 'True '
        IsAllow2Process = SecurityProvider.Authorize(Context.User, SR.parking_fee_daftar_status_process_privilege) 'True '
        IsAllow2Edit = SecurityProvider.Authorize(Context.User, SR.parking_fee_daftar_status_edit_detail_privilege) 'True '

        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lblGenerate.Visible = True
            ddlProcess.Visible = IsAllow2Process
            btnProcess.Visible = IsAllow2Process
            btnTransfer.Visible = IsAllow2Transfer
        Else
            lblGenerate.Visible = False
            ddlProcess.Visible = False
            btnProcess.Visible = False
            btnTransfer.Visible = IsAllow2Transfer
        End If
    End Sub

    Private Sub InitiatePage()
        txtKodeDealer.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")
        Dim objUserInfo As UserInfo = sessionHelper.GetSession("LOGINUSERINFO")
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        btnDownload.Enabled = False
        btnTransfer.Enabled = False
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            'txtKodeDealer.ReadOnly = True
            txtKodeDealer.Attributes.Add("readonly", "readonly")
        Else
            txtKodeDealer.ReadOnly = False
        End If
        ViewState("CurrentSortColumn") = "NoReg"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Sub BindDataGrid(ByVal currentPageIndex As Integer)
        Dim objUserInfo As UserInfo = sessionHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If txtKodeDealer.Text.Trim() = String.Empty Then
                MessageBox.Show("Tentukan Dealer terlebih dahulu")
                Exit Sub
            End If
        End If

        Dim total As Integer = 0
        Dim arlPPH As New ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeReturnHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtKodeDealer.Text.Trim() <> "" Then
            Dim strKodeDealerIn As String = "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"
            criterias.opAnd(New Criteria(GetType(ParkingFeeReturnHeader), "Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
        End If
        If txtNoReg.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(ParkingFeeReturnHeader), "NoReg", MatchType.[Partial], txtNoReg.Text.Trim))
        End If
        If txtNoBukti.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(ParkingFeeReturnHeader), "BuktiPotongNumber", MatchType.[Partial], txtNoBukti.Text.Trim))
        End If

        If ddlStatus.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(ParkingFeeReturnHeader), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        criterias.opAnd(New Criteria(GetType(ParkingFeeReturnHeader), "ReturnDate", MatchType.GreaterOrEqual, ICStart.Value))
        criterias.opAnd(New Criteria(GetType(ParkingFeeReturnHeader), "ReturnDate", MatchType.LesserOrEqual, ICEnd.Value))

        arlPPH = New ParkingFeeReturnHeaderFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgPPHStatus.PageSize, _
                total, CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        sessionHelper.SetSession("CritsPPH", criterias)
        dtgPPHStatus.DataSource = arlPPH
        dtgPPHStatus.VirtualItemCount = total
        If arlPPH.Count > 0 Then
            dtgPPHStatus.DataBind()
            btnProcess.Enabled = True
            btnDownload.Enabled = True
            btnTransfer.Enabled = True
        Else
            dtgPPHStatus.DataBind()
            MessageBox.Show("Data Tidak Ditemukan")
            btnProcess.Enabled = False
            btnDownload.Enabled = False
            btnTransfer.Enabled = False
        End If
    End Sub

    Private Function GetDataPPH() As ArrayList
        Dim pphColl As New ArrayList
        Dim critsPPH As CriteriaComposite
        Try
            If sessionHelper.GetSession("CritsPPH") Is Nothing Then
                MessageBox.Show("Download data gagal ")
                Exit Function
            End If
            critsPPH = CType(sessionHelper.GetSession("CritsPPH"), CriteriaComposite)
            pphColl = New ParkingFeeReturnHeaderFacade(User).Retrieve(critsPPH)
        Catch ex As Exception

        End Try
        Return pphColl
    End Function

    Private Sub SetProcess(ByVal iStatus As Integer)
        Dim listPPH As ArrayList = GetDataPPH(iStatus)

        If listPPH.Count = 0 Then
            Dim errMsg As String = ""
            Select Case iStatus
                Case EnumPengembalianPPhStatus.PengembalianPPhStatus.Validasi '1
                    errMsg = "Baru"
                Case EnumPengembalianPPhStatus.PengembalianPPhStatus.Baru '0
                    errMsg = "Validasi"
                Case EnumPengembalianPPhStatus.PengembalianPPhStatus.Batal '3
                    errMsg = "Baru" ' / Validasi
            End Select
            MessageBox.Show(SR.DataNotSelectedByStatus("Status Pengembalian PPH", errMsg))
        Else
            Dim creditAccount As String = CType(listPPH(0), ParkingFeeReturnHeader).Dealer.CreditAccount
            If listPPH.Count > 0 Then
                For Each pfrh As ParkingFeeReturnHeader In listPPH
                    For Each pfrd As ParkingFeeReturnDetail In pfrh.ParkingFeeReturnDetails
                        Dim pf As ParkingFee = pfrd.ParkingFee
                        'Dim creditAccount As String = pfrd.ParkingFee.Dealer.CreditAccount.ToString
                        If pf.Dealer.CreditAccount <> creditAccount Then
                            MessageBox.Show("Data Parking Fee harus dalam satu Credit Account")
                            Exit Sub
                        End If
                    Next
                Next
            End If

            Dim objPFRHFacade As New ParkingFeeReturnHeaderFacade(User)
            If objPFRHFacade.UpdateParkingFeeReturHeaderStatus(listPPH, iStatus) = 1 Then
                BindDataGrid(dtgPPHStatus.CurrentPageIndex)
            Else
                MessageBox.Show("Update status gagal")
            End If
        End If
    End Sub

    Private Function GetDataPPH(ByVal iStatus As Integer) As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim objPFRHFacade As New ParkingFeeReturnHeaderFacade(User)

        For Each oDataGridItem In dtgPPHStatus.Items
            chkExport = oDataGridItem.FindControl("cbCheck")
            If chkExport.Checked Then
                Dim objPFRHStatus As Integer = CType(CType(oDataGridItem.FindControl("lblStatusID"), Label).Text, Integer)
                Dim id As Integer = CType(CType(oDataGridItem.FindControl("lblID"), Label).Text, Integer)
                Dim _objPFRH As New KTB.Dnet.Domain.ParkingFeeReturnHeader
                Select Case iStatus
                    Case EnumPengembalianPPhStatus.PengembalianPPhStatus.Validasi '1
                        If objPFRHStatus = EnumPengembalianPPhStatus.PengembalianPPhStatus.Baru Then
                            _objPFRH = objPFRHFacade.Retrieve(id)
                            oExArgs.Add(_objPFRH)
                        End If
                    Case EnumPengembalianPPhStatus.PengembalianPPhStatus.Baru '0
                        If objPFRHStatus = EnumPengembalianPPhStatus.PengembalianPPhStatus.Validasi Then
                            _objPFRH = objPFRHFacade.Retrieve(id)
                            oExArgs.Add(_objPFRH)
                        End If
                    Case EnumPengembalianPPhStatus.PengembalianPPhStatus.Batal '3
                        If objPFRHStatus = EnumPengembalianPPhStatus.PengembalianPPhStatus.Baru Then 'Or objPFRHStatus = EnumPengembalianPPhStatus.PengembalianPPhStatus.Validasi 
                            _objPFRH = objPFRHFacade.Retrieve(id)
                            oExArgs.Add(_objPFRH)
                        End If
                End Select
            End If
        Next
        Return oExArgs
    End Function

    Private Function GetSelectedIDs(ByVal dg As DataGrid) As String
        Dim i As Integer = 0
        Dim strResult As String = "("
        For Each item As DataGridItem In dtgPPHStatus.Items
            Dim chk As CheckBox = item.FindControl("cbCheck")
            If chk.Checked Then
                Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                Dim lblStatusID As Label = CType(item.FindControl("lblStatusID"), Label)
                If CInt(lblStatusID.Text) = EnumPengembalianPPhStatus.PengembalianPPhStatus.Validasi Then
                    strResult = strResult & lblID.Text.ToString & ","
                End If
            End If
        Next
        strResult = Left(strResult, strResult.Length - 1) & ")"
        Return strResult
    End Function

    Private Sub Download(ByVal arlPPH As ArrayList)
        Dim sFileName As String
        sFileName = "Daftar_Pengembalian_PPH [" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & "]"

        Dim SPKData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(SPKData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If
                Dim fs As FileStream = New FileStream(SPKData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)

                WriteToExcell(sw, arlPPH)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls", True)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub WriteToExcell(ByVal sw As StreamWriter, ByVal arlPPH As ArrayList)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)  '-- Empty line
        itemLine.Append("DO STATUS - Daftar Status Pengembalian PPH")
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)
        sw.WriteLine(" ")


        If (arlPPH.Count > 0) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("NO" & tab)
            itemLine.Append("STATUS" & tab)
            itemLine.Append("KODE DEALER" & tab)
            itemLine.Append("NAMA DEALER" & tab)
            itemLine.Append("CREDIT ACCOUNT" & tab)
            itemLine.Append("NO. REG. BUKTI POTONG" & tab)
            itemLine.Append("NO. BUKTI POTONG PPh" & tab)
            'Add by anh 20120720 req by angga
            itemLine.Append("DEBIT MEMO" & tab)
            itemLine.Append("PERIODE" & tab)
            itemLine.Append("TAHUN" & tab)
            itemLine.Append("DETAIL AMOUNT" & tab)
            'end added
            itemLine.Append("TOTAL AMOUNT" & tab)
            itemLine.Append("PPh AMOUNT" & tab)
            itemLine.Append("NO. JV" & tab)
            itemLine.Append("DESKRIPSI" & tab)
            itemLine.Append("TANGGAL GENERATE (dd-MM-yyyy)" & tab)
            sw.WriteLine(itemLine.ToString())
            Dim i As Integer = 1
            Try
                For Each item As ParkingFeeReturnHeader In arlPPH
                    For Each detail As ParkingFeeReturnDetail In item.ParkingFeeReturnDetails
                        Dim debitmemo As String = detail.ParkingFee.DebitMemoNumber
                        'dim periode as String = 
                        itemLine.Remove(0, itemLine.Length)
                        itemLine.Append(i.ToString & tab)
                        itemLine.Append(CType(CInt(item.Status), EnumPengembalianPPhStatus.PengembalianPPhStatus).ToString & tab)
                        itemLine.Append(detail.ParkingFee.Dealer.DealerCode & tab)
                        itemLine.Append(detail.ParkingFee.Dealer.DealerName & tab)
                        itemLine.Append(detail.ParkingFee.Dealer.CreditAccount & tab)
                        itemLine.Append(item.NoReg & tab)
                        itemLine.Append(item.BuktiPotongNumber & tab)
                        'Add by anh 20120720 req by angga
                        itemLine.Append(detail.ParkingFee.DebitMemoNumber & tab)
                        itemLine.Append(EnumParkingFeePeriod.GetStringValue(CType(detail.ParkingFee.Periode, Integer)) & tab)
                        itemLine.Append(detail.ParkingFee.Year & tab)
                        itemLine.Append(Decimal.Round(detail.ParkingFee.Amount, 0) & tab)
                        'end added
                        itemLine.Append(Decimal.Round(item.TotalAmount, 0) & tab)
                        itemLine.Append(Decimal.Round(item.PPHAmount, 0) & tab)
                        itemLine.Append(item.ReturnAssignNumber & tab)
                        itemLine.Append(item.Description & tab)
                        itemLine.Append(item.ReturnDate.ToString("dd-MM-yyyy") & tab)

                        sw.WriteLine(itemLine.ToString())
                        i = i + 1
                    Next
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If

        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)

    End Sub

    Private Sub Upload2SAP(ByVal strIDs As String)
        Dim i As Integer = 0
        Dim arl As ArrayList = New ArrayList
        Dim success As Boolean = False

        Dim IsCheck As Boolean = False
        Dim sw As StreamWriter
        Dim sb As New StringBuilder
        Dim filename = String.Format("{0}{1}{2}", "jvreturdepa", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")

        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("FinishUnitFileDirectory") & "\" & filename  '-- Destination file
        Dim LocalDest As String = Server.MapPath("") & "\..\DataTemp\" & filename

        Dim tmp As Integer = 0
        Dim objCulture As Globalization.CultureInfo = New Globalization.CultureInfo("id-ID")
        Dim arlPFRH As ArrayList

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeReturnHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ParkingFeeReturnHeader), "ID", MatchType.InSet, strIDs))
        arlPFRH = New ParkingFeeReturnHeaderFacade(User).Retrieve(criterias)

        Dim arlOffset As New ArrayList

        For Each item As ParkingFeeReturnHeader In arlPFRH
            If item.ReturnAssignNumber <> String.Empty Then
                MessageBox.Show("Sudah ada No JVnya, tidak bisa transfer SAP")
                Exit Sub
            End If
            arlOffset.Add(item)
        Next

        For Each item As ParkingFeeReturnHeader In arlOffset

            Dim ObjCheck = (From ObjPF As ParkingFeeReturnDetail In item.ParkingFeeReturnDetails
                           Where ObjPF.RowStatus = CType(DBRowStatus.Active, Short)
                           Group By ObjPF.ParkingFee.Category.ProductCategory.Code Into Group
                           Select Code).Count()


            If Not IsNothing(ObjCheck) AndAlso CInt(ObjCheck) > 1 Then
                sb.Clear()
                MessageBox.Show("Data MMC dan MFTBC tidak bisa dalam satu No. Reg. Bukti Potong")
                Return
            End If


            Dim strNoJV As String = ""
            Dim strDescription As String = ""
            Dim strAccountCode As String = String.Empty
            For Each itemDetail As ParkingFeeReturnDetail In item.ParkingFeeReturnDetails
                If itemDetail.RowStatus = CType(DBRowStatus.Active, Short) Then
                    If strNoJV <> "" Then
                        strNoJV = strNoJV & "," & itemDetail.ParkingFee.AssignmentNumber
                    Else
                        strNoJV = itemDetail.ParkingFee.AssignmentNumber
                    End If
                    If strAccountCode = String.Empty Then
                        strAccountCode = itemDetail.ParkingFee.Category.ProductCategory.AccountCode
                    End If

                End If
                strDescription = "Pengembalian PPh Parkir Bulan " & EnumParkingFeePeriod.GetStringValueShort(itemDetail.ParkingFee.Periode, itemDetail.ParkingFee.Year)
            Next
            sb.Append(DateTime.Now.ToString("ddMMyyyy") & ";" & item.BuktiPotongNumber & ";" & item.NoReg & ";" & Date.Now.ToString("yyyyMMdd") & ";" & item.Dealer.CreditAccount & ";" & FormatNumber(item.PPHAmount, 0, TriState.False, TriState.False, TriState.False) & ";" & strNoJV & ";" & strDescription & ";" & strAccountCode)
            sb.Append(vbNewLine)
        Next

        If (sb.Length > 0) Then
            If Transfer(DestFile, sb.ToString(), arl) Then         '>> Code utk upload data lsg ke SAP Folder
                Dim intResult = -1
                Dim objPFRHFacade As ParkingFeeReturnHeaderFacade = New ParkingFeeReturnHeaderFacade(User)
                If objPFRHFacade.UpdateParkingFeeReturHeaderStatus(arlOffset, EnumPengembalianPPhStatus.PengembalianPPhStatus.Konfirmasi) = 1 Then
                    BindDataGrid(dtgPPHStatus.CurrentPageIndex)
                Else
                    MessageBox.Show("Update status gagal")
                End If

            Else
                MessageBox.Show("Download data gagal")
                Exit Sub
            End If
        End If

        MessageBox.Show("Data berhasil di upload ke SAP")
        BindDataGrid(dtgPPHStatus.CurrentPageIndex)
    End Sub

    Private Function Transfer(ByVal DestFile As String, ByVal Val As String, ByVal arl As ArrayList) As Boolean
        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If

                sw = New StreamWriter(DestFile)
                sw.Write(Val)
                sw.Close()
                success = True
            End If
        Catch ex As Exception
            sw.Close()
            Throw ex
            Return success
        Finally
            imp.StopImpersonate()
        End Try
        Return success
    End Function
#End Region



End Class

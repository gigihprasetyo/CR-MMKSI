
#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNET.BusinessFacade.AfterSales
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports MMKSI.DNetUpload.Utility
Imports System.Collections.Generic

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmPartsSales
    Inherits System.Web.UI.Page
    Dim sessHelper As SessionHelper = New SessionHelper

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents rfvKode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgPartSales As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTotalSalesValue As System.Web.UI.WebControls.Label
    Protected WithEvents periodeFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents periodeTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtNoWorkOrder As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoPart As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlSalesChannel As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeDealerBranch As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents hAssistUploadLogID As System.Web.UI.WebControls.HiddenField
    Protected WithEvents lblKodeDealerMenu As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerBranchSeparator As System.Web.UI.WebControls.Label
    'Protected WithEvents lblStatusMenu As System.Web.UI.WebControls.Label
    'Protected WithEvents lblStatusSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerDetail As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerDetailSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealerDetail As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealerDetailSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents hDownloadURL As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hQuery As System.Web.UI.WebControls.HiddenField
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button

    Protected WithEvents lblPRCUMenu As System.Web.UI.WebControls.Label
    Protected WithEvents lblPRCUSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblPRCUUnit As System.Web.UI.WebControls.Label
    Protected WithEvents lblPRCUValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalGRMenu As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalGRSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalGRValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPMMenu As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPMSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPMValue As System.Web.UI.WebControls.Label
    Protected WithEvents pnlMonitoring As System.Web.UI.WebControls.Panel

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
    Private m_SaveFormPrivilege As Boolean = False
    Private ListModule As ArrayList
    Dim objAssistUploadLog As AssistUploadLog
#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            GetAssistUploadLog()
            sessHelper.SetSession("FrmPartsSales", Nothing)
            periodeFrom.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            If hAssistUploadLogID.Value = "" Then 'jika dibuka dari menu maka empty
                btnBack.Visible = False
                btnDownload.Visible = True
                btnDownload.Enabled = False
                lblKodeDealerDetail.Visible = False
                lblKodeDealerDetailSeparator.Visible = False
                lblDealerCode.Visible = False
                lblNamaDealerDetail.Visible = False
                lblNamaDealerDetailSeparator.Visible = False
                lblDealerName.Visible = False
                lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
                lblSearchDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
            Else
                'ddlStatus.Visible = False
                'lblStatusMenu.Visible = False
                'lblStatusSeparator.Visible = False
                btnDownload.Visible = False
                BindDataGrid(dtgPartSales.CurrentPageIndex, True)
                LoadInformationHeader(True)
                txtKodeDealer.Visible = False
                txtKodeDealerBranch.Visible = False
                lblSearchDealer.Visible = False
                lblSearchDealerBranch.Visible = False
                lblKodeDealerMenu.Text = String.Empty
                lblKodeDealerSeparator.Text = String.Empty
                lblKodeDealerBranchSeparator.Text = String.Empty
                lblPRCUMenu.Visible = False
                lblPRCUSeparator.Visible = False
                lblPRCUUnit.Visible = False
                lblPRCUValue.Visible = False
                lblTotalGRMenu.Visible = False
                lblTotalGRSeparator.Visible = False
                lblTotalGRValue.Visible = False
                lblTotalPMMenu.Visible = False
                lblTotalPMSeparator.Visible = False
                lblTotalPMValue.Visible = False

                'Try

                '    Dim objAssistLog As AssistPartSales = New AssistPartSalesFacade(User).Retrieve(CInt(hAssistUploadLogID.Value))
                '    lblDealerCode.Text = objAssistLog.Dealer.DealerCode & " / " & objAssistLog.Dealer.SearchTerm1
                '    lblDealerName.Text = objAssistLog.Dealer.DealerName
                'Catch ex As Exception

                'End Try


            End If

        End If
    End Sub

    Private Sub GetAssistUploadLog()
        Dim LogID As String = Request.QueryString("id")
        hAssistUploadLogID.Value = LogID
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Not sessHelper.GetSession("PrevPage") Is Nothing AndAlso Not sessHelper.GetSession("PrevPage") = String.Empty Then
            Response.Redirect(sessHelper.GetSession("PrevPage").ToString())
        Else
            Response.Redirect("../login.aspx")
        End If

    End Sub

    Private Sub BindSalesChannel()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistSalesChannel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistSalesChannel), "Status", MatchType.Exact, 1))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(AssistSalesChannel), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
        Dim Coll As ArrayList = New AssistSalesChannelFacade(User).Retrieve(criterias, sortColl)

        Dim item As ListItem
        item = New ListItem("Silahkan pilih", 0)
        ddlSalesChannel.Items.Add(item)
        If Coll.Count > 0 Then
            For Each cat As AssistSalesChannel In Coll
                item = New ListItem(cat.SalesChannelType & " | " & cat.SalesChannelCode, cat.ID)
                ddlSalesChannel.Items.Add(item)
            Next
        End If
    End Sub

    'Private Sub BindStatus()

    '    Dim listStatus As New EnumAssistStatusUpload
    '    Dim al As ArrayList = listStatus.RetrieveStatusType
    '    For Each item As enumassistupload In al
    '        ddlStatus.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
    '    Next
    '    ddlStatus.Items.Insert(0, New ListItem("Pilih Status", "-1"))

    'End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If txtKodeDealer.Text = "" And hAssistUploadLogID.Value = "" Then
            MessageBox.Show("Kode dealer harus diisi")
            Return
        End If
        SearchModuleByCriteria()
        btnDownload.Enabled = True
    End Sub

    Private Sub dtgPartSales_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPartSales.SortCommand
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

        dtgPartSales.SelectedIndex = -1
        dtgPartSales.CurrentPageIndex = 0
        BindDataGrid(dtgPartSales.CurrentPageIndex, False)
    End Sub


    Private Sub dtgPartSales_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPartSales.PageIndexChanged
        dtgPartSales.SelectedIndex = -1
        dtgPartSales.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgPartSales.CurrentPageIndex, False)
    End Sub

#End Region

#Region "Custom"

    Private Sub SetControlPrivilege()
        'btnUpload.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()

        If Not SecurityProvider.Authorize(Context.User, SR.AfterSales_DaftarUpload_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Data Upload- Part Sales")
        End If

    End Sub

    Private Sub InitiatePage()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        If Not sessHelper.GetSession("DEALER") Is Nothing Then
            Dim arDeal As ArrayList = New ArrayList
            Dim objDeal As Dealer
            objDeal = CType(sessHelper.GetSession("DEALER"), Dealer)
            If Not objDeal Is Nothing Then
                lblDealerCode.Text = objDeal.DealerCode & " / " & objDeal.SearchTerm1
                lblDealerName.Text = objDeal.DealerName
                If objDeal.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    txtKodeDealer.Attributes.Add("readonly", "readonly")
                    txtKodeDealer.Text = objDeal.DealerCode
                    txtKodeDealer.BorderStyle = BorderStyle.None
                    lblSearchDealer.Visible = False
                    pnlMonitoring.Visible = False
                End If
                Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")

            End If
        End If
        BindSalesChannel()
        'BindStatus()
    End Sub

    Function GetCriteria(ByVal firstLoad As Boolean) As CriteriaComposite
        Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")

        Dim TanggalAwal As New DateTime(CInt(periodeFrom.Value.Year), CInt(periodeFrom.Value.Month), CInt(periodeFrom.Value.Day), 0, 0, 0)
        Dim TanggalAkhir As New DateTime(CInt(periodeTo.Value.Year), CInt(periodeTo.Value.Month), CInt(periodeTo.Value.Day), 0, 0, 0)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistPartSales), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (firstLoad = False) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistPartSales), "TglTransaksi", MatchType.GreaterOrEqual, Format(TanggalAwal, "yyyy-MM-dd")))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistPartSales), "TglTransaksi", MatchType.Lesser, Format(TanggalAkhir.AddDays(1), "yyyy-MM-dd")))
        End If

        If txtNoWorkOrder.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistPartSales), "NoWorkOrder", MatchType.Partial, txtNoWorkOrder.Text))
        End If

        If txtNoPart.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistPartSales), "NoParts", MatchType.Partial, txtNoPart.Text))
        End If

        If ddlSalesChannel.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistPartSales), "AssistSalesChannel.ID", MatchType.Exact, ddlSalesChannel.SelectedValue))
        End If
        'If ddlStatus.SelectedValue > -1 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistPartSales), "AssistUploadLog.ValidateStatus", MatchType.Exact, ddlStatus.SelectedValue))
        'End If

        If Not hAssistUploadLogID.Value = "" Then 'jika dibuka dari daftar upload
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistPartSales), "AssistUploadLog.ID", MatchType.Exact, hAssistUploadLogID.Value))
        Else
            If txtKodeDealer.Text.Trim() <> "" Then
                Dim strKodeDealerIn As String = "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"
                criterias.opAnd(New Criteria(GetType(AssistPartSales), "Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
            End If

            If txtKodeDealerBranch.Text.Trim() <> "" Then
                Dim strKodeDealerBranchIn As String = "('" & txtKodeDealerBranch.Text.Trim().Replace(";", "','") & "')"
                criterias.opAnd(New Criteria(GetType(AssistPartSales), "DealerBranch.DealerBranchCode", MatchType.InSet, strKodeDealerBranchIn))
            End If

            'hanya yang sudah dikonfirmasi mmksi dan tidak double (sudah masuk ke BI)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistPartSales), "StatusAktif", MatchType.Exact, 1))

        End If
        sessHelper.SetSession("FrmPartsSales", criterias)
        Return criterias
    End Function

    Private Sub LoadInformationHeader(ByVal firstLoad As Boolean)
        Dim criterias As CriteriaComposite = GetCriteria(firstLoad)
        sessHelper.SetSession("FrmPartsSales", criterias)
        Dim _query As String = criterias.ToString()
        hQuery.Value = _query

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(AssistPartSales), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
        Dim listAll As ArrayList = New AssistPartSalesFacade(User).Retrieve(criterias, sortColl)

        Dim totalsales As Double = 0
        Dim totalPRCU As Double = 0
        If Not hAssistUploadLogID.Value = "" Then
            For Each item As AssistPartSales In listAll
                totalsales = totalsales + (Convert.ToDouble(IsNull(item.HargaJual, 0)) * Convert.ToDouble(IsNull(item.Qty, 0)))
            Next
        Else
            'Service Incoming (get PRCU)
            Dim TanggalAwal As New DateTime(CInt(periodeFrom.Value.Year), CInt(periodeFrom.Value.Month), CInt(periodeFrom.Value.Day), 0, 0, 0)
            Dim TanggalAkhir As New DateTime(CInt(periodeTo.Value.Year), CInt(periodeTo.Value.Month), CInt(periodeTo.Value.Day), 0, 0, 0)
            Dim criteriaSVC As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriaSVC.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "TglTutupTransaksi", MatchType.GreaterOrEqual, Format(TanggalAwal, "yyyy-MM-dd")))
            criteriaSVC.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "TglTutupTransaksi", MatchType.Lesser, Format(TanggalAkhir.AddDays(1), "yyyy-MM-dd")))
            criteriaSVC.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "StatusAktif", MatchType.Exact, 1))
            criteriaSVC.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "WorkOrderCategoryCode", MatchType.InSet, "('GR','PM')"))
            If txtKodeDealer.Text.Trim() <> "" Then
                Dim strKodeDealerIn As String = "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"
                criteriaSVC.opAnd(New Criteria(GetType(AssistServiceIncoming), "Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
            End If
            If txtKodeDealerBranch.Text.Trim() <> "" Then
                Dim strKodeDealerBranchIn As String = "('" & txtKodeDealerBranch.Text.Trim().Replace(";", "','") & "')"
                criteriaSVC.opAnd(New Criteria(GetType(AssistServiceIncoming), "DealerBranch.DealerBranchCode", MatchType.InSet, strKodeDealerBranchIn))
            End If
            Dim sortCollSVC As SortCollection = New SortCollection
            sortCollSVC.Add(New Sort(GetType(AssistServiceIncoming), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
            Dim SVCColl As ArrayList = New AssistServiceIncomingFacade(User).Retrieve(criteriaSVC, sortCollSVC)

            Dim arrayWOGR As New List(Of String)()
            Dim arrayWOPM As New List(Of String)()

            For Each item As AssistPartSales In listAll
                totalsales = totalsales + (Convert.ToDouble(IsNull(item.HargaJual, 0)) * Convert.ToDouble(IsNull(item.Qty, 0)))
                For Each itemSVC As AssistServiceIncoming In SVCColl
                    If item.NoWorkOrder = itemSVC.NoWorkOrder Then
                        totalPRCU += (Convert.ToDouble(IsNull(item.HargaJual, 0)) * Convert.ToDouble(IsNull(item.Qty, 0)))
                        If itemSVC.WorkOrderCategoryCode = "GR" Then
                            If Not arrayWOGR.Contains(item.NoWorkOrder) Then
                                arrayWOGR.Add(item.NoWorkOrder)
                            End If
                        ElseIf itemSVC.WorkOrderCategoryCode = "PM" Then
                            If Not arrayWOPM.Contains(item.NoWorkOrder) Then
                                arrayWOPM.Add(item.NoWorkOrder)
                            End If
                        End If
                    End If
                Next
            Next
            If (arrayWOGR.Count + arrayWOPM.Count) > 0 Then
                totalPRCU = totalPRCU / (arrayWOGR.Count + arrayWOPM.Count)
                lblTotalGRValue.Text = FormatNumber(arrayWOGR.Count, 0, , , TriState.UseDefault)
                lblTotalPMValue.Text = FormatNumber(arrayWOPM.Count, 0, , , TriState.UseDefault)
            Else
                totalPRCU = 0
            End If

        End If

        lblPRCUValue.Text = FormatNumber(totalPRCU, 0, , , TriState.UseDefault)
        lblTotalSalesValue.Text = FormatNumber(totalsales, 0, , , TriState.UseDefault)
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer, ByVal firstLoad As Boolean)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then

            Dim criterias As CriteriaComposite

            If Not IsNothing(sessHelper.GetSession("FrmPartsSales")) Then
                criterias = sessHelper.GetSession("FrmPartsSales")
            Else
                criterias = GetCriteria(firstLoad)
            End If

            ListModule = New AssistPartSalesFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
                    dtgPartSales.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))


            If Not IsNothing(ListModule) AndAlso ListModule.Count > 0 Then
                dtgPartSales.DataSource = ListModule
                dtgPartSales.VirtualItemCount = totRow
                dtgPartSales.DataBind()
            Else
                dtgPartSales.DataSource = New ArrayList
                dtgPartSales.VirtualItemCount = 0
                dtgPartSales.DataBind()
            End If
        End If
    End Sub
    Public Function IsNull(value As Double, defaultValue As Double) As String
        If String.IsNullOrEmpty(value) Then
            Return defaultValue
        Else
            Return value
        End If
    End Function
    'Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
    '    If (hQuery.Value <> "") Then
    '        Dim objDeal As Dealer
    '        objDeal = CType(sHRole.GetSession("DEALER"), Dealer)
    '        Dim url As String = KTB.DNet.Lib.WebConfig.GetValue("AssistDownloadByQueryURL").ToString()
    '        Dim _key = InsertUpdateAssistKeyUpload()
    '        Dim enc As Encryptor = New Encryptor()
    '        Dim objUserInfo As UserInfo = sHRole.GetSession("LOGINUSERINFO")
    '        url = url.Replace("[DEALERCODE]", objDeal.DealerCode)
    '        url = url.Replace("[ASSISTMODULETYPE]", "SPAREPARTSALES")
    '        url = url.Replace("[QUERY]", enc.Encrypt(hQuery.Value, System.Text.Encoding.Unicode))
    '        url = url.Replace("[USERNAMELOGIN]", enc.Encrypt(objUserInfo.UserName, System.Text.Encoding.Unicode))
    '        url = url.Replace("[KEY]", _key)

    '        hDownloadURL.Value = url

    '        System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(Page), "Script", "openWindowDownload();", True)
    '        btnDownload.Enabled = False
    '    End If
    'End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim criterias As CriteriaComposite

        If Not IsNothing(sessHelper.GetSession("FrmPartsSales")) Then
            criterias = sessHelper.GetSession("FrmPartsSales")
        Else
            criterias = GetCriteria(False)
        End If

        Dim sortColl As SortCollection = New SortCollection
        Dim SortDirection As Sort.SortDirection = CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
        Dim sortColumn As String = ViewState("CurrentSortColumn")
        If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
            sortColl.Add(New Sort(GetType(AssistPartSales), sortColumn, SortDirection))
        Else
            sortColl = Nothing
        End If

        Dim lstPartSales As ArrayList = New AssistPartSalesFacade(User).Retrieve(criterias, sortColl)
        If Not IsNothing(lstPartSales) Then
            DoDownload(lstPartSales)
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String

        sFileName = "SPartSales" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second

        '-- Temp file must be a randomly named file!
        Dim SvcIncomingData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(SvcIncomingData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(SvcIncomingData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteSPartSales(sw, data)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteSPartSales(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        Dim header As String

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Spare Part - Sales")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("Tgl Transaksi" & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Cabang Dealer" & tab)
            itemLine.Append("Kode Customer" & tab)
            itemLine.Append("Sales Channel" & tab)
            itemLine.Append("Kode Salesman" & tab)
            itemLine.Append("No Work Order" & tab)
            itemLine.Append("No Part" & tab)
            itemLine.Append("Qty" & tab)
            itemLine.Append("Harga Beli (per pc)" & tab)
            itemLine.Append("Harga Jual (per pc)" & tab)
            itemLine.Append("Status" & tab)

            sw.WriteLine(itemLine.ToString())

            For Each item As AssistPartSales In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(CDate(item.TglTransaksi).ToString("dd/MM/yyyy") & tab)
                itemLine.Append(item.DealerCode & tab)
                itemLine.Append(item.DealerBranchCode & tab)
                itemLine.Append(item.KodeCustomer & tab)
                itemLine.Append(item.SalesChannelCode & tab)
                itemLine.Append(item.KodeSalesman & tab)
                itemLine.Append(item.NoWorkOrder & tab)
                itemLine.Append(item.NoParts & tab)
                itemLine.Append(item.Qty & tab)
                itemLine.Append(item.HargaBeli.ToString("N") & tab)
                itemLine.Append(item.HargaJual.ToString("N") & tab)
                If Not IsNothing(item.AssistUploadLog) Then
                    itemLine.Append(item.AssistUploadLog.StatusDescription & tab)
                Else
                    itemLine.Append("" & tab)
                End If


                sw.WriteLine(itemLine.ToString())
            Next
        End If
    End Sub

    Function InsertUpdateAssistKeyUpload() As String
        Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")
        Dim enc As Encryptor = New Encryptor()
        Dim nResult As Integer
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistKeyUpload), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistKeyUpload), "UserName", MatchType.Exact, objUserInfo.UserName))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(AssistKeyUpload), "ID", ViewState("CurrentSortDirect")))
        Dim moduleColl As ArrayList = New AssistKeyUploadFacade(User).Retrieve(criterias, sortColl)
        Dim _key As String = enc.Encrypt(DateTime.Now.ToString("yyyy-MM-dd hh"), System.Text.Encoding.Unicode)

        If (moduleColl.Count > 0) Then
            'Update Key
            Dim assistkey As AssistKeyUpload = moduleColl(0)
            If (assistkey.Key <> _key) Then
                assistkey.Key = _key
                nResult = New AssistKeyUploadFacade(User).Update(assistkey)
            End If
        Else
            'Insert
            Dim objKeyFacade As AssistKeyUploadFacade = New AssistKeyUploadFacade(User)
            Dim objKey As AssistKeyUpload = New AssistKeyUpload

            objKey.UserName = objUserInfo.UserName
            objKey.Key = _key
            nResult = New AssistKeyUploadFacade(User).Insert(objKey)
        End If
        Return _key
    End Function

    Private Sub dtgPartSales_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPartSales.ItemDataBound
        'Dim RowValue AsV_AssistPartStockFlagLastData= CType(e.Item.DataItem, AssistUploadLog)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgPartSales.CurrentPageIndex * dtgPartSales.PageSize)
            Dim valuebeli As Label = CType(e.Item.FindControl("lblHargaBeli"), Label)
            valuebeli.Text = FormatNumber(Convert.ToDouble(valuebeli.Text), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Dim valuejual As Label = CType(e.Item.FindControl("lblHargaJual"), Label)
            valuejual.Text = FormatNumber(Convert.ToDouble(valuejual.Text), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If

    End Sub


    Private Sub SearchModuleByCriteria()
        sessHelper.SetSession("FrmPartsSales", GetCriteria(False))

        dtgPartSales.CurrentPageIndex = 0
        BindDataGrid(dtgPartSales.CurrentPageIndex, False)
        LoadInformationHeader(False)
    End Sub
#End Region

End Class


#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.AfterSales
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports MMKSI.DNetUpload.Utility

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmPartsStock
    Inherits System.Web.UI.Page
    Dim sessHelper As SessionHelper = New SessionHelper

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents rfvKode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgPartStock As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTotalPartsStockValue As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoPart As System.Web.UI.WebControls.TextBox
    'Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealerBranch As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealerBranch As Label
    Protected WithEvents hAssistUploadLogID As System.Web.UI.WebControls.HiddenField
    Protected WithEvents lblKodeDealerMenu As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerSeparator As System.Web.UI.WebControls.Label
    'Protected WithEvents lblStatusMenu As System.Web.UI.WebControls.Label
    'Protected WithEvents lblStatusSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerDetail As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerDetailSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealerDetail As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealerDetailSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents hDownloadURL As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hQuery As System.Web.UI.WebControls.HiddenField
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
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
            sessHelper.SetSession("FrmPartsStock", Nothing)
            InitiatePage()
            GetAssistUploadLog()

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
                BindDataGrid(dtgPartStock.CurrentPageIndex, True)
                LoadInformationHeader(True)
                txtKodeDealer.Visible = False
                lblSearchDealer.Visible = False
                lblKodeDealerMenu.Visible = False
                lblKodeDealerSeparator.Visible = False

                'Try
                '    Dim objAssistLog As V_AssistPartStockFlagLastData = New V_AssistPartStockFlagLastDataFacade(User).Retrieve(CInt(hAssistUploadLogID.Value))
                '    lblDealerCode.Text = objAssistLog.Dealer.DealerCode & " / " & objAssistLog.Dealer.SearchTerm1
                '    lblDealerName.Text = objAssistLog.Dealer.DealerName
                'Catch ex As Exception

                'End Try


            End If

        End If

    End Sub

    Private Sub GetAssistUploadLog()
        Dim LogID As String = Request.QueryString("id")
        If Not IsNothing(LogID) Then
            'Dim objAssistLog As AssistUploadLog = New AssistUploadLogFacade(User).Retrieve(CInt(LogID))
            'ddlMonth.SelectedValue = objAssistLog.Month
            'ddlYear.SelectedValue = objAssistLog.Year
            hAssistUploadLogID.Value = LogID
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Not sessHelper.GetSession("PrevPage") Is Nothing AndAlso Not sessHelper.GetSession("PrevPage") = String.Empty Then
            Response.Redirect(sessHelper.GetSession("PrevPage").ToString())
        Else
            Response.Redirect("../login.aspx")
        End If

    End Sub

    Private Sub BindMonth()

        Dim al As ArrayList = enumMonthGet.RetriveMonth()
        ddlMonth.DataSource = enumMonthGet.RetriveMonth()
        ddlMonth.DataValueField = "ValStatus"
        ddlMonth.DataTextField = "NameStatus"
        ddlMonth.DataBind()

        Dim item As ListItem
        item = New ListItem("Silahkan pilih", 0)
        ddlMonth.Items.Add(item)

        ddlMonth.SelectedValue = 0

    End Sub

    Private Sub BindYear()

        Dim a As Integer
        Dim now As DateTime = DateTime.Now
        Dim item As ListItem
        item = New ListItem("Silahkan pilih", 0)
        ddlYear.Items.Add(item)
        For a = -3 To 3
            ddlYear.Items.Insert(0, New ListItem(now.AddYears(a).Year, now.AddYears(a).Year))
        Next

        ddlYear.SelectedValue = 0
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

    Private Sub dtgPartStock_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPartStock.SortCommand
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

        dtgPartStock.SelectedIndex = -1
        dtgPartStock.CurrentPageIndex = 0
        BindDataGrid(dtgPartStock.CurrentPageIndex, False)
    End Sub


    Private Sub dtgPartStock_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPartStock.PageIndexChanged
        dtgPartStock.SelectedIndex = -1
        dtgPartStock.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgPartStock.CurrentPageIndex, False)
    End Sub

#End Region

#Region "Custom"

    Private Sub SetControlPrivilege()
        'btnUpload.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()

        If Not SecurityProvider.Authorize(Context.User, SR.AfterSales_DaftarUpload_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Data Upload - Part Stock")
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
        'BindStatus()
        BindMonth()
        BindYear()
    End Sub
    Function GetCriteria(ByVal firstLoad As Boolean) As CriteriaComposite
        Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.V_AssistPartStockFlagLastData), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If (firstLoad = False) Then
            If ddlMonth.SelectedValue > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_AssistPartStockFlagLastData), "Month", MatchType.Exact, ddlMonth.SelectedValue()))
            End If

            If ddlYear.SelectedValue > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_AssistPartStockFlagLastData), "Year", MatchType.Exact, ddlYear.SelectedValue()))
            End If
        End If

        If txtNoPart.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_AssistPartStockFlagLastData), "NoParts", MatchType.Partial, txtNoPart.Text))
        End If

        'If ddlStatus.SelectedValue > -1 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_AssistPartStockFlagLastData), "AssistUploadLog.ValidateStatus", MatchType.Exact, ddlStatus.SelectedValue))
        'End If

        If Not hAssistUploadLogID.Value = "" Then 'jika dibuka dari daftar upload
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_AssistPartStockFlagLastData), "AssistUploadLog.ID", MatchType.Exact, hAssistUploadLogID.Value))
        Else
            If txtKodeDealer.Text.Trim() <> "" Then
                Dim strKodeDealerIn As String = "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"
                criterias.opAnd(New Criteria(GetType(V_AssistPartStockFlagLastData), "Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
            End If

            If txtKodeDealerBranch.Text.Trim() <> "" Then
                Dim strKodeDealerBranchIn As String = "('" & txtKodeDealerBranch.Text.Trim().Replace(";", "','") & "')"
                criterias.opAnd(New Criteria(GetType(V_AssistPartStockFlagLastData), "DealerBranchCode", MatchType.InSet, strKodeDealerBranchIn))
            End If

            'jika selesai, hanya tampilkan data terakhir
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_AssistPartStockFlagLastData), "IsLastData", MatchType.Exact, 1))


        End If

        Return criterias
    End Function

    Function GetCriteriaUpload() As CriteriaComposite
        Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistPartStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'If (firstLoad = False) Then
        If ddlMonth.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistPartStock), "Month", MatchType.Exact, ddlMonth.SelectedValue()))
        End If

        If ddlYear.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistPartStock), "Year", MatchType.Exact, ddlYear.SelectedValue()))
        End If
        'End If

        If txtNoPart.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistPartStock), "NoParts", MatchType.Partial, txtNoPart.Text))
        End If

        'If ddlStatus.SelectedValue > -1 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistPartStock), "AssistUploadLog.ValidateStatus", MatchType.Exact, ddlStatus.SelectedValue))
        'End If

        Return criterias
    End Function
    Private Sub LoadInformationHeader(ByVal firstLoad As Boolean)
        Dim criterias As CriteriaComposite = GetCriteria(firstLoad)
        sessHelper.SetSession("FrmPartsStock", criterias)
        Dim _query As String = criterias.ToString()
        hQuery.Value = _query

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(V_AssistPartStockFlagLastData), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
        Dim listAll As ArrayList = New V_AssistPartStockFlagLastDataFacade(User).Retrieve(criterias, sortColl)

        Dim totalpartstock As Double = 0
        For Each item As V_AssistPartStockFlagLastData In listAll
            totalpartstock = totalpartstock + ((Convert.ToDouble(IsNull(item.JumlahStokAwal, 0)) + Convert.ToDouble(IsNull(item.JumlahDatang, 0))) * Convert.ToDouble(IsNull(item.HargaBeli, 0)))
        Next

        lblTotalPartsStockValue.Text = FormatNumber(totalpartstock, 0, , , TriState.UseDefault)

    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer, ByVal firstLoad As Boolean)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then

            Dim criterias As CriteriaComposite

            If Not IsNothing(sessHelper.GetSession("FrmPartsStock")) Then
                criterias = sessHelper.GetSession("FrmPartsStock")
            Else
                criterias = GetCriteria(firstLoad)
            End If

            If Not IsNothing(Request.QueryString("du")) Then
                If Request.QueryString("du").ToString = "1" Then
                    Dim AssistUploadLogId = Request.QueryString("id").ToString
                    criterias = GetCriteriaUpload()
                    criterias.opAnd(New Criteria(GetType(AssistPartStock), "AssistUploadLog.ID", MatchType.Exact, AssistUploadLogId))
                    ListModule = New AssistPartStockFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
                            dtgPartStock.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
                End If
            Else
                ListModule = New V_AssistPartStockFlagLastDataFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
                        dtgPartStock.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            End If


            If Not IsNothing(ListModule) AndAlso ListModule.Count > 0 Then
                dtgPartStock.DataSource = ListModule
                dtgPartStock.VirtualItemCount = totRow
                dtgPartStock.DataBind()
            Else
                dtgPartStock.DataSource = New ArrayList
                dtgPartStock.VirtualItemCount = 0
                dtgPartStock.DataBind()
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
    '        objDeal = CType(sessHelper.GetSession("DEALER"), Dealer)
    '        Dim url As String = KTB.DNet.Lib.WebConfig.GetValue("AssistDownloadByQueryURL").ToString()
    '        Dim _key = InsertUpdateAssistKeyUpload()
    '        Dim enc As Encryptor = New Encryptor()
    '        Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")
    '        url = url.Replace("[DEALERCODE]", objDeal.DealerCode)
    '        url = url.Replace("[ASSISTMODULETYPE]", "SPAREPARTSTOCK")
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

        If Not IsNothing(sessHelper.GetSession("FrmPartsStock")) Then
            criterias = sessHelper.GetSession("FrmPartsStock")
        Else
            criterias = GetCriteria(False)
        End If

        Dim sortColl As SortCollection = New SortCollection
        Dim SortDirection As Sort.SortDirection = CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
        Dim sortColumn As String = ViewState("CurrentSortColumn")
        If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
            sortColl.Add(New Sort(GetType(V_AssistPartStockFlagLastData), sortColumn, SortDirection))
        Else
            sortColl = Nothing
        End If

        Dim lstPartStocks As ArrayList = New V_AssistPartStockFlagLastDataFacade(User).Retrieve(criterias, sortColl)
        If Not IsNothing(lstPartStocks) Then
            DoDownload(lstPartStocks)
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String

        sFileName = "SPartStocks" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second

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
                WriteSPartStocks(sw, data)

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

    Private Sub WriteSPartStocks(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        Dim header As String

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Spare Part - Stocks")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("Bulan" & tab)
            itemLine.Append("Tahun" & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Cabang Dealer" & tab)
            itemLine.Append("No Parts" & tab)
            itemLine.Append("Jumlah Stok Awal" & tab)
            itemLine.Append("Jumlah Datang" & tab)
            itemLine.Append("Harga Beli" & tab)
            itemLine.Append("Status" & tab)

            sw.WriteLine(itemLine.ToString())

            For Each item As V_AssistPartStockFlagLastData In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(item.Month & tab)
                itemLine.Append(item.Year & tab)
                itemLine.Append(item.DealerCode & tab)
                itemLine.Append(item.DealerBranchCode & tab)
                itemLine.Append(item.NoParts & tab)
                itemLine.Append(item.JumlahStokAwal & tab)
                itemLine.Append(item.JumlahDatang & tab)
                itemLine.Append(item.HargaBeli.ToString("N") & tab)
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

    Private Sub dtgPartStock_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPartStock.ItemDataBound
        'Dim RowValue As AssistUploadLog = CType(e.Item.DataItem, AssistUploadLog)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgPartStock.CurrentPageIndex * dtgPartStock.PageSize)
            Dim lblJmlStokAwal As System.Web.UI.WebControls.Label = CType(e.Item.FindControl("lblJmlStokAwal"), System.Web.UI.WebControls.Label)
            Dim lblJmlDatang As System.Web.UI.WebControls.Label = CType(e.Item.FindControl("lblJmlDatang"), System.Web.UI.WebControls.Label)

            If e.Item.DataItem.GetType() Is GetType(AssistPartStock) Then
                Dim oAPS As AssistPartStock = CType(e.Item.DataItem, AssistPartStock)
                If Not IsNothing(oAPS.SparePartMaster) Then
                    If oAPS.SparePartMaster.ProductType.Trim.ToUpper() = "OIL" OrElse oAPS.SparePartMaster.ProductType.Trim.ToUpper() = "CHEMICAL" Then
                        lblJmlStokAwal.Text = Math.Round(oAPS.JumlahStokAwal, 3)
                        lblJmlDatang.Text = Math.Round(oAPS.JumlahDatang, 3)
                    Else
                        lblJmlStokAwal.Text = Math.Round(oAPS.JumlahStokAwal)
                        lblJmlDatang.Text = Math.Round(oAPS.JumlahDatang)
                    End If
                Else
                    lblJmlStokAwal.Text = Math.Round(oAPS.JumlahStokAwal)
                    lblJmlDatang.Text = Math.Round(oAPS.JumlahDatang)
                End If
                
            Else
                Dim oAPS As V_AssistPartStockFlagLastData = CType(e.Item.DataItem, V_AssistPartStockFlagLastData)
                If Not IsNothing(oAPS.SparePartMaster) Then
                    If oAPS.SparePartMaster.ProductType.Trim.ToUpper() = "OIL" OrElse oAPS.SparePartMaster.ProductType.Trim.ToUpper() = "CHEMICAL" Then
                        lblJmlStokAwal.Text = Decimal.Round(oAPS.JumlahStokAwal, 3)
                        lblJmlDatang.Text = Decimal.Round(oAPS.JumlahDatang, 3)
                    Else
                        lblJmlStokAwal.Text = Math.Round(oAPS.JumlahStokAwal)
                        lblJmlDatang.Text = Math.Round(oAPS.JumlahDatang)
                    End If
                Else
                    lblJmlStokAwal.Text = Math.Round(oAPS.JumlahStokAwal)
                    lblJmlDatang.Text = Math.Round(oAPS.JumlahDatang)
                End If     
            End If
            
            Dim month As Label = CType(e.Item.FindControl("lblMonth"), Label)
            If (month.Text <> "") Then
                month.Text = enumMonthGet.GetName(month.Text)
            End If

            Dim value As Label = CType(e.Item.FindControl("lblHargaBeli"), Label)
            If value.Text <> "" Then
                value.Text = FormatNumber(Convert.ToDouble(value.Text), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End If
        End If

    End Sub
    Private Sub ViewModel(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objModule As AssistUploadLog = New AssistUploadLogFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsUploadReport", objModule)

    End Sub

    Private Sub SearchModuleByCriteria()
        sessHelper.SetSession("FrmPartsStock", GetCriteria(False))
        dtgPartStock.CurrentPageIndex = 0
        BindDataGrid(dtgPartStock.CurrentPageIndex, False)
        LoadInformationHeader(False)
    End Sub
#End Region

End Class


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
Imports KTB.DNet.BusinessFacade
Imports System.Text
#End Region

Public Class FrmServiceIncoming
    Inherits System.Web.UI.Page
    Dim sessHelper As SessionHelper = New SessionHelper

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents rfvKode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents hDownloadURL As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hQuery As System.Web.UI.WebControls.HiddenField
    Protected WithEvents dtgServiceIncoming As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblSIUValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblMSIValue As System.Web.UI.WebControls.Label
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents tutupFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents tutupTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents bukaFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents bukaTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtNoWorkOrder As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoChassis As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlWorkOrderKategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTempatPengerjaan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlLayanan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlWOStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeDealerBranch As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents hAssistUploadLogID As System.Web.UI.WebControls.HiddenField
    Protected WithEvents lblKodeDealerMenu As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerBranchSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealerBranch As System.Web.UI.WebControls.Label
    'Protected WithEvents lblStatusMenu As System.Web.UI.WebControls.Label
    'Protected WithEvents lblStatusSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerDetail As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerDetailSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealerDetail As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealerDetailSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents pnlMonitoring As System.Web.UI.WebControls.Panel
    Protected WithEvents chkTanggalTutupTransaksi As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkTanggalBukaTransaksi As System.Web.UI.WebControls.CheckBox


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
    Private models As ArrayList

#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            GetAssistUploadLog()

            tutupFrom.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            bukaFrom.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            If hAssistUploadLogID.Value = "" Then 'jika dibuka dari menu maka empty
                btnSave.Visible = False
                btnDownload.Visible = True
                btnDownload.Enabled = False
                btnBack.Visible = False
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
                BindDataGrid(dtgServiceIncoming.CurrentPageIndex)
                LoadInformationHeader()
                txtKodeDealer.Visible = False
                txtKodeDealerBranch.Visible = False
                lblSearchDealer.Visible = False
                lblSearchDealerBranch.Visible = False
                lblKodeDealerMenu.Visible = False
                lblKodeDealerSeparator.Visible = False
                lblKodeDealerBranchSeparator.Visible = False
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

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim criterias As CriteriaComposite

        If Not IsNothing(sessHelper.GetSession("FrmServiceIncoming")) Then
            criterias = sessHelper.GetSession("FrmServiceIncoming")
        Else
            criterias = GetCriteria()
        End If

        Dim sortColl As SortCollection = New SortCollection
        Dim SortDirection As Sort.SortDirection = CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
        Dim sortColumn As String = ViewState("CurrentSortColumn")
        If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
            sortColl.Add(New Sort(GetType(AssistServiceIncoming), sortColumn, SortDirection))
        Else
            sortColl = Nothing
        End If

        Dim isFromDaftarUpload As Boolean = False
        If Not hAssistUploadLogID.Value = "" Then
            isFromDaftarUpload = True
        End If
        'Dim lstSvcIncoming As ArrayList = New AssistServiceIncomingFacade(User).Retrieve(criterias, sortColl)
        Dim lstSvcIncoming As ArrayList = New AssistServiceIncomingFacade(User).DownloadDataBySP(criterias, sortColl, isFromDaftarUpload)
        If Not IsNothing(lstSvcIncoming) Then
            DoDownload(lstSvcIncoming)
        End If

    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String

        sFileName = "SvcIncoming" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second

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
                WriteSvcIncoming(sw, data)

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

    Private Sub WriteSvcIncoming(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        Dim header As String

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Service Incoming")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("Tgl Buka Transaksi" & tab)
            itemLine.Append("Waktu Masuk" & tab)
            itemLine.Append("Tgl Tutup Transaksi" & tab)
            itemLine.Append("Waktu Keluar" & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Cabang Dealer" & tab)
            itemLine.Append("Kode Mekanik" & tab)
            itemLine.Append("No Work Order" & tab)
            itemLine.Append("No Chassis" & tab)
            itemLine.Append("Kategori Work Order" & tab)
            itemLine.Append("KM Service" & tab)
            itemLine.Append("Tempat Pengerjaan" & tab)
            itemLine.Append("Layanan" & tab)
            itemLine.Append("Total LC" & tab)
            itemLine.Append("Metode Pembayaran" & tab)
            itemLine.Append("Remarks" & tab)
            itemLine.Append("Adjusment" & tab)
            itemLine.Append("Model" & tab)
            itemLine.Append("Roda Penggerak" & tab)
            itemLine.Append("Transmisi" & tab)
            itemLine.Append("Status" & tab)

            sw.WriteLine(itemLine.ToString())

            For Each item As AssistServiceIncoming In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(CDate(item.TglBukaTransaksi).ToString("dd/MM/yyyy") & tab)
                itemLine.Append(item.WaktuMasuk.ToString() & tab)
                itemLine.Append(CDate(item.TglTutupTransaksi).ToString("dd/MM/yyyy") & tab)
                itemLine.Append(item.WaktuKeluar.ToString() & tab)
                itemLine.Append(item.DealerCode & tab)
                itemLine.Append(item.DealerBranchCode & tab)
                itemLine.Append(item.KodeMekanik & tab)
                itemLine.Append(item.NoWorkOrder & tab)
                itemLine.Append(item.KodeChassis & tab)
                itemLine.Append(item.WorkOrderCategoryCode & tab)
                itemLine.Append(item.KMService & tab)
                itemLine.Append(item.ServicePlaceCode & tab)
                itemLine.Append(item.ServiceTypeCode & tab)
                itemLine.Append(item.TotalLC & tab)
                itemLine.Append(item.MetodePembayaran & tab)
                itemLine.Append(item.RemarksSpecial & tab)
                itemLine.Append(item.RemarksBM & tab) 'adjusment'
                itemLine.Append(item.Model & tab)
                itemLine.Append(item.DriveSystem & tab)
                itemLine.Append(item.Transmition & tab)
                If Not IsNothing(item.AssistUploadLog) Then
                    itemLine.Append(item.AssistUploadLog.StatusDescription & tab)
                Else
                    itemLine.Append("" & tab)
                End If


                sw.WriteLine(itemLine.ToString())
            Next
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Try
        Dim isChange As Boolean = False
        For Each item As DataGridItem In dtgServiceIncoming.Items
            Dim lblID As Label = CType(item.FindControl("lblID"), Label)
            Dim ddlModel As DropDownList = CType(item.FindControl("ddlModel"), DropDownList)

            Dim lblRemarksBMHide As Label = CType(item.FindControl("lblRemarksBMHide"), Label)
            If ddlModel.SelectedValue <> "" Then
                If Not (ddlModel.SelectedValue = lblRemarksBMHide.Text) Then
                    Dim SplitModel As String() = ddlModel.SelectedItem.Text.Split(New Char() {"-"c})
                    isChange = True
                    Dim facade As AssistServiceIncomingFacade = New AssistServiceIncomingFacade(User)
                    Dim objSVC As AssistServiceIncoming = facade.Retrieve(Convert.ToInt32(lblID.Text))
                    objSVC.RemarksBM = ddlModel.SelectedItem.ToString()
                    objSVC.Model = SplitModel(0)
                    facade.Update(objSVC)
                End If
            End If
        Next
        If isChange = True Then
            MessageBox.Show(SR.SaveSuccess)
        Else
            MessageBox.Show("Tidak ada perubahan data")
        End If
        'Catch ex As Exception
        '    MessageBox.Show(SR.SaveFail & ". Error : " & ex.Message)
        'End Try
    End Sub


    Private Sub BindWorkOrderCategory()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistWorkOrderCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistWorkOrderCategory), "Status", MatchType.Exact, 1))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(AssistWorkOrderCategory), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
        Dim Coll As ArrayList = New AssistWorkOrderCategoryFacade(User).Retrieve(criterias, sortColl)

        Dim item As ListItem
        item = New ListItem("Silahkan pilih", 0)
        ddlWorkOrderKategory.Items.Add(item)
        If Coll.Count > 0 Then
            For Each cat As AssistWorkOrderCategory In Coll
                item = New ListItem(cat.WorkOrderCategory & " | " & cat.Description, cat.ID)
                ddlWorkOrderKategory.Items.Add(item)
            Next
        End If
    End Sub

    Private Sub BindTempatPengerjaan()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistServicePlace), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServicePlace), "Status", MatchType.Exact, 1))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(AssistServicePlace), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
        Dim Coll As ArrayList = New AssistServicePlaceFacade(User).Retrieve(criterias, sortColl)

        Dim item As ListItem
        item = New ListItem("Silahkan pilih", 0)
        ddlTempatPengerjaan.Items.Add(item)
        If Coll.Count > 0 Then
            For Each cat As AssistServicePlace In Coll
                item = New ListItem(cat.ServicePlaceCode & " | " & cat.Description, cat.ID)
                ddlTempatPengerjaan.Items.Add(item)
            Next
        End If
    End Sub


    Private Sub BindLayanan()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistServiceType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceType), "Status", MatchType.Exact, 1))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(AssistServiceType), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
        Dim Coll As ArrayList = New AssistServiceTypeFacade(User).Retrieve(criterias, sortColl)

        Dim item As ListItem
        item = New ListItem("Silahkan pilih", 0)
        ddlLayanan.Items.Add(item)
        If Coll.Count > 0 Then
            For Each cat As AssistServiceType In Coll
                item = New ListItem(cat.Description, cat.ID)
                ddlLayanan.Items.Add(item)
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

    Private Sub BindWOStatus()

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StandardCode), "Category", MatchType.Exact, "WOStatus"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "ValueId", Sort.SortDirection.ASC))
        Dim Coll As ArrayList = New StandardCodeFacade(User).Retrieve(criterias, sortColl)

        Dim item As ListItem
        item = New ListItem("Silahkan pilih", 0)
        ddlWOStatus.Items.Add(item)
        If Coll.Count > 0 Then
            For Each cat As StandardCode In Coll
                item = New ListItem(cat.ValueDesc, cat.ValueId)
                ddlWOStatus.Items.Add(item)
            Next
        End If
        If Not IsNothing(Request.QueryString("du")) Then
            ddlWOStatus.SelectedValue = 0
        Else
            ddlWOStatus.SelectedValue = 2
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If txtKodeDealer.Text = "" And hAssistUploadLogID.Value = "" Then
            MessageBox.Show("Kode dealer harus diisi")
            Return
        End If

        SearchModuleByCriteria()
        btnDownload.Enabled = True
    End Sub

    Private Sub dtgServiceIncoming_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgServiceIncoming.SortCommand
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

        dtgServiceIncoming.SelectedIndex = -1
        dtgServiceIncoming.CurrentPageIndex = 0
        BindDataGrid(dtgServiceIncoming.CurrentPageIndex)
    End Sub


    Private Sub dtgServiceIncoming_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgServiceIncoming.PageIndexChanged
        dtgServiceIncoming.SelectedIndex = -1
        dtgServiceIncoming.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgServiceIncoming.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom"

    Private Sub SetControlPrivilege()
        'btnUpload.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_SaveFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AfterSales_SvcIncoming_Ubah_Privilege)

        btnSave.Visible = m_SaveFormPrivilege

        If Not SecurityProvider.Authorize(Context.User, SR.AfterSales_DaftarUpload_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Data Upload - Service Incoming")
        End If

    End Sub

    Private Sub InitiatePage()
        SetControlPrivilege()
        If Not IsNothing(Request.QueryString("du")) Then
            'chkTanggalBukaTransaksi.Checked = True
            'chkTanggalTutupTransaksi.Checked = True
        Else
            chkTanggalBukaTransaksi.Checked = True
            chkTanggalTutupTransaksi.Checked = True
        End If
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
        BindTempatPengerjaan()
        BindLayanan()
        BindWorkOrderCategory()
        BindWOStatus()
    End Sub

    Function GetCriteria() As CriteriaComposite
        Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")

        Dim BukaAwal As New DateTime(CInt(bukaFrom.Value.Year), CInt(bukaFrom.Value.Month), CInt(bukaFrom.Value.Day), 0, 0, 0)
        Dim BukaAkhir As New DateTime(CInt(bukaTo.Value.Year), CInt(bukaTo.Value.Month), CInt(bukaTo.Value.Day), 0, 0, 0)

        Dim TutupAwal As New DateTime(CInt(tutupFrom.Value.Year), CInt(tutupFrom.Value.Month), CInt(tutupFrom.Value.Day), 0, 0, 0)
        Dim TutupAkhir As New DateTime(CInt(tutupTo.Value.Year), CInt(tutupTo.Value.Month), CInt(tutupTo.Value.Day), 0, 0, 0)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (chkTanggalBukaTransaksi.Checked) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "TglBukaTransaksi", MatchType.GreaterOrEqual, Format(BukaAwal, "yyyy-MM-dd")))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "TglBukaTransaksi", MatchType.Lesser, Format(BukaAkhir.AddDays(1), "yyyy-MM-dd")))
        End If

        If (chkTanggalTutupTransaksi.Checked) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "TglTutupTransaksi", MatchType.GreaterOrEqual, Format(TutupAwal, "yyyy-MM-dd")))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "TglTutupTransaksi", MatchType.Lesser, Format(TutupAkhir.AddDays(1), "yyyy-MM-dd")))
        End If

        If txtNoWorkOrder.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "NoWorkOrder", MatchType.Partial, txtNoWorkOrder.Text))
        End If

        If txtNoChassis.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "KodeChassis", MatchType.Partial, txtNoChassis.Text))
        End If

        If ddlWorkOrderKategory.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "AssistWorkOrderCategory.ID", MatchType.Exact, ddlWorkOrderKategory.SelectedValue))
        End If
        If ddlTempatPengerjaan.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "AssistServicePlace.ID", MatchType.Exact, ddlTempatPengerjaan.SelectedValue))
        End If
        If ddlLayanan.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "AssistServiceType.ID", MatchType.Exact, ddlLayanan.SelectedValue))
        End If

        If ddlWOStatus.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "WOStatus", MatchType.Exact, ddlWOStatus.SelectedValue))
        End If

        If Not hAssistUploadLogID.Value = "" Then 'jika dibuka dari daftar upload
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "AssistUploadLog.ID", MatchType.Exact, hAssistUploadLogID.Value))
        Else
            If txtKodeDealer.Text.Trim() <> "" Then
                Dim strKodeDealerIn As String = "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"
                criterias.opAnd(New Criteria(GetType(AssistServiceIncoming), "Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
            End If

            If txtKodeDealerBranch.Text.Trim() <> "" Then
                Dim strKodeDealerBranchIn As String = "('" & txtKodeDealerBranch.Text.Trim().Replace(";", "','") & "')"
                criterias.opAnd(New Criteria(GetType(AssistServiceIncoming), "DealerBranch.DealerBranchCode", MatchType.InSet, strKodeDealerBranchIn))
            End If

            'hanya yang sudah dikonfirmasi mmksi dan tidak double (sudah masuk ke BI)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "StatusAktif", MatchType.Exact, 1))

        End If

        Return criterias

    End Function

    Private Sub LoadInformationHeader()
        Dim criterias As CriteriaComposite = GetCriteria()
        sessHelper.SetSession("FrmServiceIncoming", criterias)
        Dim _query As String = criterias.ToString()
        hQuery.Value = _query

        Dim isFromDaftarUpload As Boolean = False
        If Not hAssistUploadLogID.Value = "" Then
            isFromDaftarUpload = True
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(AssistServiceIncoming), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
        'Dim listAll As ArrayList = New AssistServiceIncomingFacade(User).Retrieve(criterias, sortColl)
        Dim listAll As ArrayList = New AssistServiceIncomingFacade(User).DownloadDataBySP(criterias, sortColl, isFromDaftarUpload)

        Dim siu As Int32 = 0
        For Each item As AssistServiceIncoming In listAll
            If (Not IsNothing(item.AssistWorkOrderCategory)) AndAlso _
                (item.AssistWorkOrderCategory.WorkOrderCategory = "GR" Or item.AssistWorkOrderCategory.WorkOrderCategory = "PM") Then
                siu = siu + 1
            End If
        Next

        lblSIUValue.Text = siu
        lblMSIValue.Text = listAll.Count
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As CriteriaComposite

            If Not IsNothing(sessHelper.GetSession("FrmServiceIncoming")) Then
                criterias = sessHelper.GetSession("FrmServiceIncoming")
            Else
                criterias = GetCriteria()
            End If
            Dim isFromDaftarUpload As Boolean = False
            If Not hAssistUploadLogID.Value = "" Then
                isFromDaftarUpload = True
            End If
            'ListModule = New AssistServiceIncomingFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
            '        dtgServiceIncoming.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            ViewState("currSortTable") = GetType(AssistServiceIncoming)
            ListModule = New AssistServiceIncomingFacade(User).RetrieveCustomPagingBySP(criterias, indexPage + 1, dtgServiceIncoming.PageSize,
                                                                                     totRow, CType(ViewState("CurrentSortColumn"), String),
                                                                                     CType(ViewState("currSortTable"), System.Type),
                                                                                     CType(ViewState("CurrentSortDirect"), Sort.SortDirection), isFromDaftarUpload)

            If Not IsNothing(ListModule) AndAlso ListModule.Count > 0 Then
                dtgServiceIncoming.DataSource = ListModule
                dtgServiceIncoming.VirtualItemCount = totRow
                dtgServiceIncoming.DataBind()
            Else
                dtgServiceIncoming.DataSource = New ArrayList
                dtgServiceIncoming.VirtualItemCount = 0
                dtgServiceIncoming.DataBind()
            End If


        End If
    End Sub

    Private Sub dtgServiceIncoming_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgServiceIncoming.ItemDataBound
        'Dim RowValue As AssistUploadLog = CType(e.Item.DataItem, AssistUploadLog)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgServiceIncoming.CurrentPageIndex * dtgServiceIncoming.PageSize)

            Dim lblRemarksBMHide As Label = CType(e.Item.FindControl("lblRemarksBMHide"), Label)
            'Dim txtRemarksBM As TextBox = CType(e.Item.FindControl("txtRemarksBM"), TextBox)
            Dim ddlModel As DropDownList = CType(e.Item.FindControl("ddlModel"), DropDownList)

            Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")
            If Not objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                ddlModel.Visible = False
            Else
                If hAssistUploadLogID.Value = "" Then
                    ddlModel.Visible = False
                Else
                    Dim lblStatusValue As Label = CType(e.Item.FindControl("lblStatusValue"), Label)
                    If lblStatusValue.Text = EnumAssistStatusUpload.StatusUpload.MenungguValidasi Then
                        Dim lblModel As Label = CType(e.Item.FindControl("lblModel"), Label)
                        If lblModel.Text = String.Empty Then
                            lblRemarksBMHide.Visible = False
                            'load ddl

                            If IsNothing(models) Then
                                Dim criteriasmodel As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistMasterModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                Dim sortCollmodel As SortCollection = New SortCollection
                                sortCollmodel.Add(New Sort(GetType(AssistMasterModel), "ModelName", Sort.SortDirection.ASC))
                                models = New AssistMasterModelFacade(User).Retrieve(criteriasmodel, sortCollmodel)
                            End If
                            Dim item As ListItem
                            item = New ListItem("Silahkan pilih", 0)
                            ddlTempatPengerjaan.Items.Add(item)
                            If models.Count > 0 Then
                                For Each cat As AssistMasterModel In models
                                    item = New ListItem(cat.ModelName & " - " & cat.ReferenceSample, cat.ID)
                                    ddlModel.Items.Add(item)
                                Next
                            End If

                            ddlModel.Items.Insert(0, New ListItem("", ""))
                            ddlModel.SelectedValue = lblRemarksBMHide.Text
                        Else
                            ddlModel.Visible = False
                        End If
                    Else
                        ddlModel.Visible = False
                    End If
                End If
            End If

            Dim value As Label = CType(e.Item.FindControl("lblTotalLC"), Label)
            value.Text = FormatNumber(Convert.ToDouble(value.Text), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

        End If

    End Sub
    Private Sub ViewModel(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objModule As AssistUploadLog = New AssistUploadLogFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsUploadReport", objModule)

    End Sub

    Private Sub SearchModuleByCriteria()

        sessHelper.SetSession("FrmServiceIncoming", GetCriteria())

        dtgServiceIncoming.CurrentPageIndex = 0
        BindDataGrid(dtgServiceIncoming.CurrentPageIndex)
        LoadInformationHeader()
    End Sub
#End Region

End Class

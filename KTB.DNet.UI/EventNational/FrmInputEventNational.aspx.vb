#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Helper
#End Region

Public Class FrmInputEventNational
    Inherits System.Web.UI.Page

#Region "Custom Variables Declaration"
    Dim objDealer As New Dealer
    Dim objLoginUser As UserInfo
    Private sesHelper As New SessionHelper

    Private objNationalEvent As NationalEvent
    Private arlNationalEventDoc As ArrayList = New ArrayList
    Private arlDeleteNationalEventDoc As ArrayList = New ArrayList

    Private Property sessNewGuidScreen As String
        Get
            If ViewState("NewGuidScreen") Is Nothing Then
                ViewState("NewGuidScreen") = Guid.NewGuid().ToString()
            End If
            Return DirectCast(ViewState("NewGuidScreen"), String)
        End Get
        Set(value As String)
            ViewState("NewGuidScreen") = value
        End Set
    End Property
    Private ReadOnly Property sessNationalEvent() As String
        Get
            Return CType("FrmInputEventNational.sessDataNationalEvent_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessNationalEventDoc() As String
        Get
            Return CType("FrmInputEventNational.sessDataNationalEventDoc_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDeleteNationalEventDoc() As String
        Get
            Return CType("FrmInputEventNational.sessDeleteDataNationalEventDoc_" & sessNewGuidScreen, String)
        End Get
    End Property

    Const strProfixCode As String = "N"
    Private MAX_FILE_SIZE As Integer = 5120000
    Private TempDirectory As String = String.Empty
    Private TargetDirectory As String = String.Empty
    Private intItemIndex As Integer = 0

    Dim IsDealer As Boolean = False
    Dim Mode As String = "New"

    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property
#End Region

#Region "Custom Methods"
    Private Sub BindddlEventType()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(NationalEventType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        arrDDL = New NationalEventTypeFacade(User).Retrieve(criterias)

        With ddlEventType
            .Items.Clear()
            .DataSource = arrDDL
            .DataTextField = "Name"
            .DataValueField = "ID"
            .DataBind()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End With
    End Sub

    Private Sub BindddlCity()
        ddlCity.Items.Clear()
        ddlCity.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        With ddlCity.Items
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim arrCity As ArrayList = New NationalEventCityFacade(User).Retrieve(crits)
            For Each _neCity As NationalEventCity In arrCity
                .Add(New ListItem(_neCity.City.CityName, _neCity.ID))
            Next
        End With
    End Sub

    Private Sub BindddlVenue(ByVal intCityID As Integer)
        ddlVenue.Items.Clear()
        ddlVenue.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        With ddlVenue.Items
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventVenue), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(NationalEventVenue), "City.ID", MatchType.Exact, intCityID))
            Dim arrVenue As ArrayList = New NationalEventVenueFacade(User).Retrieve(crits)
            For Each _neVenue As NationalEventVenue In arrVenue
                .Add(New ListItem(_neVenue.VenueName, _neVenue.ID))
            Next
        End With
    End Sub

    Private Sub BindddlArea1()
        ddlArea1.Items.Clear()
        ddlArea1.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        With ddlArea1.Items
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area1), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim arrArea1 As ArrayList = New Area1Facade(User).Retrieve(crits)
            For Each _area1 As Area1 In arrArea1
                .Add(New ListItem(_area1.Description, _area1.ID))
            Next
        End With
    End Sub

    Private Sub LoadDataNationalEvent(intNationalEventID As Integer)
        arlNationalEventDoc = New ArrayList

        objNationalEvent = New NationalEventFacade(User).Retrieve(intNationalEventID)
        If Not IsNothing(objNationalEvent) Then
            sesHelper.SetSession(sessNationalEvent, objNationalEvent)

            hdnNationalEventID.Value = objNationalEvent.ID
            lblRegNumber.Text = objNationalEvent.RegNumber
            ddlEventType.SelectedValue = objNationalEvent.NationalEventType.ID
            ddlCity.SelectedValue = objNationalEvent.NationalEventCity.ID
            ddlCity_SelectedIndexChanged(Nothing, Nothing)
            ddlVenue.SelectedValue = objNationalEvent.NationalEventVenue.ID
            ddlArea1.SelectedValue = objNationalEvent.DealerArea1.ID
            icPeriodStart.Value = objNationalEvent.PeriodStart
            icPeriodEnd.Value = objNationalEvent.PeriodEnd
            txtTargetProspect.Text = objNationalEvent.TargetProspect
            txtTargetSPK.Text = objNationalEvent.TargetSPK

            Dim strDealerCityCodes As String = String.Empty
            Dim strDealerCityName As String = String.Empty
            If Not IsNothing(objNationalEvent.DealerCityID) AndAlso objNationalEvent.DealerCityID.Trim() <> "" Then
                Dim oCity As New City
                Dim arrDealerCityID As String() = objNationalEvent.DealerCityID.Split(";")
                For Each dealerCityID As String In arrDealerCityID
                    oCity = New KTB.DNet.BusinessFacade.General.CityFacade(User).Retrieve(CInt(dealerCityID))
                    If Not IsNothing(oCity) AndAlso oCity.ID > 0 Then
                        If strDealerCityCodes = String.Empty Then
                            strDealerCityCodes = oCity.CityCode
                            strDealerCityName = oCity.CityName
                        Else
                            strDealerCityCodes += ";" & oCity.CityCode
                            strDealerCityName += ";" & oCity.CityName
                        End If
                    End If
                Next
            End If
            txtDealerCityID.Text = strDealerCityCodes
            txtDealerCityName.Text = strDealerCityName

            Dim crit As New CriteriaComposite(New Criteria(GetType(DocumentUpload), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(DocumentUpload), "DocRegNumber", MatchType.Exact, objNationalEvent.RegNumber))
            arlNationalEventDoc = New DocumentUploadFacade(User).Retrieve(crit)
            sesHelper.SetSession(sessNationalEventDoc, arlNationalEventDoc)
            BindGridNationalEventDoc()

            ddlEventType.Enabled = False
            ddlCity.Enabled = False
            ddlVenue.Enabled = False
            ddlArea1.Enabled = False
            txtDealerCityID.Enabled = False
            lblPopUpDealer.Visible = False
            icPeriodStart.Enabled = False
            icPeriodEnd.Enabled = False
            txtTargetProspect.Enabled = False
            txtTargetSPK.Enabled = False
            dgUploadFile.ShowFooter = False
            dgUploadFile.Columns(dgUploadFile.Columns.Count - 1).Visible = False
            btnBack.Visible = True
            btnSave.Visible = False

            If Request.QueryString("Mode") = "Edit" Then
                ddlEventType.Enabled = True
                ddlCity.Enabled = True
                ddlVenue.Enabled = True
                ddlArea1.Enabled = True
                txtDealerCityID.Enabled = True
                lblPopUpDealer.Visible = True
                icPeriodStart.Enabled = True
                icPeriodEnd.Enabled = True
                txtTargetProspect.Enabled = True
                txtTargetSPK.Enabled = True
                dgUploadFile.ShowFooter = True
                dgUploadFile.Columns(dgUploadFile.Columns.Count - 1).Visible = True
                btnBack.Visible = True
                btnSave.Visible = True
            End If
        End If
    End Sub

    Private Sub ClearAll()
        hdnNationalEventID.Value = "0"
        lblRegNumber.Text = "[Auto Generated]"
        ddlEventType.SelectedIndex = 0
        ddlCity.SelectedIndex = 0
        ddlVenue.SelectedIndex = 0
        ddlArea1.SelectedIndex = 0
        txtDealerCityID.Text = ""
        icPeriodStart.Value = Date.Now
        icPeriodEnd.Value = Date.Now
        txtTargetProspect.Text = "0"
        txtTargetSPK.Text = "0"

        sesHelper.SetSession(sessNationalEventDoc, New ArrayList)
        BindGridNationalEventDoc()
    End Sub

    Private Function GetRegNumber() As String
        Dim _return As String
        Dim YearParameter As Integer = Date.Today.Year
        If Date.Today.Month = 12 Then
            YearParameter = Date.Today.Year + 1
        End If
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEvent), "RegNumber", MatchType.StartsWith, strProfixCode))
        crit.opAnd(New Criteria(GetType(NationalEvent), "CreatedTime", MatchType.GreaterOrEqual, Date.Today.Year & Date.Today.Month.ToString("d2") & "01"))
        crit.opAnd(New Criteria(GetType(NationalEvent), "CreatedTime", MatchType.Lesser, YearParameter & Date.Today.AddMonths(1).Month.ToString("d2") & "01"))
        Dim arrl As ArrayList = New NationalEventFacade(User).Retrieve(crit)
        If arrl.Count > 0 Then
            Dim objNE As NationalEvent = CommonFunction.SortListControl(arrl, "RegNumber", Sort.SortDirection.DESC)(0)
            Dim noReg As String = objNE.RegNumber
            _return = strProfixCode & Date.Today.Month.ToString("d2") & Date.Today.ToString("yy") & (CInt(noReg.Substring(5, 5)) + 1).ToString("d5")
        Else
            _return = strProfixCode & Date.Today.Month.ToString("d2") & Date.Today.ToString("yy") & CInt(1).ToString("d5")
        End If
        Return _return
    End Function

    Private Function TimeStamp() As String
        Return DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & DateTime.Now.Millisecond
    End Function

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Sub BindGridNationalEventDoc()
        arlNationalEventDoc = CType(sesHelper.GetSession(sessNationalEventDoc), ArrayList)
        If IsNothing(arlNationalEventDoc) Then arlNationalEventDoc = New ArrayList()
        dgUploadFile.DataSource = arlNationalEventDoc
        dgUploadFile.DataBind()
    End Sub


    Private Function ValidateDealerCityIDs(ByVal _cityIDs As String) As Boolean
        Dim bcheck As Boolean = True
        Dim i As Integer
        Dim items() As String = _cityIDs.Split(";")
        For i = 0 To items.Length - 1
            Dim sqlCmd As String = String.Empty
            sqlCmd = "SELECT DISTINCT b.ID "
            sqlCmd += "FROM Dealer a "
            sqlCmd += "JOIN City b on b.ID = a.CityID AND b.RowStatus = 0 "
            sqlCmd += "JOIN Area1 c on c.ID = a.Area1ID AND c.RowStatus = 0 "
            sqlCmd += "JOIN Province d on d.ID = b.ProvinceID AND d.RowStatus = 0 "
            sqlCmd += "WHERE A.RowStatus = 0 "
            sqlCmd += String.Format("AND c.ID = {0} ", ddlArea1.SelectedValue)
            sqlCmd += String.Format("AND b.CityCode = '{0}' ", items(i))
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(City), "ID", MatchType.InSet, "(" & sqlCmd & ")"))

            Dim arrCity As ArrayList = New CityFacade(User).Retrieve(criterias)
            Dim objCityTmp As City = Nothing
            If Not IsNothing(arrCity) AndAlso arrCity.Count > 0 Then
                objCityTmp = CType(arrCity(0), City)
            End If
            If Not IsNothing(objCityTmp) Then
                If objCityTmp.ID = 0 Then
                    MessageBox.Show("Kota " + items(i) + " tidak valid")
                    bcheck = False
                    Exit For
                End If
            Else
                MessageBox.Show("Kota " + items(i) + " tidak valid")
                bcheck = False
                Exit For
            End If
        Next
        If bcheck = False Then Return bcheck

        Dim strCityDuplication As String = ValidateDealerCityDuplication(_cityIDs)
        If strCityDuplication <> String.Empty Then
            MessageBox.Show("Ada duplikasi Kota Dealer : " + strCityDuplication)
            bcheck = False
        End If
        Return bcheck
    End Function

    Private Function ValidateDealerCityDuplication(ByVal _DealerCitys As String) As String
        Dim bcheck As Boolean = True
        Dim _DealerCityDuplicate As String = String.Empty
        Dim i As Integer
        Dim j As Integer
        Dim list() As String = _DealerCitys.Split(";")
        For i = 0 To list.Length - 2
            For j = i + 1 To list.Length - 1
                If list(i) = list(j) Then
                    bcheck = False
                    Exit For
                End If
            Next
            If bcheck = False Then
                _DealerCityDuplicate = list(i)
                Exit For
            End If
        Next
        Return _DealerCityDuplicate
    End Function

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        If ddlEventType.SelectedIndex = 0 Then
            sb.Append("Tipe Event Harus Diisi\n")
        End If
        If ddlCity.SelectedIndex = 0 Then
            sb.Append("Kota Harus Diisi\n")
        End If
        If ddlVenue.SelectedIndex = 0 Then
            sb.Append("Venue Harus Diisi\n")
        End If
        If ddlArea1.SelectedIndex = 0 Then
            sb.Append("Area Harus Diisi\n")
        End If
        If (txtDealerCityID.Text = String.Empty) Then
            sb.Append("Peserta Dealer Harus Diisi\n")
        End If
        If (icPeriodStart.Value > icPeriodEnd.Value) Then
            sb.Append("Tanggal awal harus lebih kecil atau sama dengan tanggal akhir\n")
        End If
        If txtTargetProspect.Text.Trim = "" Then
            sb.Append("Target Prospek harus Diisi\n")
        End If
        If txtTargetSPK.Text.Trim = "" Then
            sb.Append("Target SPK harus Diisi\n")
        End If

        If (sesHelper.GetSession(sessNationalEventDoc) Is Nothing) Then
            sb.Append("Dokumen Pelaksanaan Acara belum ada\n")
        Else
            If CType(sesHelper.GetSession(sessNationalEventDoc), ArrayList).Count = 0 Then
                sb.Append("Dokumen Pelaksanaan Acara belum ada\n")
            End If
        End If

        Return sb.ToString()
    End Function

    Private Sub UploadAttachment(ByVal ObjAttachment As DocumentUpload, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As FileInfo
        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(ObjAttachment.Path) Then
                    finfo = New FileInfo(TargetPath + ObjAttachment.Path)

                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    ObjAttachment.AttachmentData.SaveAs(TargetPath + ObjAttachment.Path)
                End If

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function FileIsExist(ByVal FileName As String, ByVal AttachmentCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If AttachmentCollection.Count > 0 Then
            For Each obj As DocumentUpload In AttachmentCollection
                If Not IsNothing(obj.AttachmentData) Then
                    If Path.GetFileName(obj.AttachmentData.FileName) = FileName Then
                        bResult = True
                        Exit For
                    End If
                End If
            Next
        End If
        Return bResult
    End Function

    Private Sub RemoveNationalEventAttachment(ByVal ObjAttachment As DocumentUpload, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As FileInfo
        Try
            success = imp.Start()
            If success Then
                finfo = New FileInfo(TargetPath + ObjAttachment.Path)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RemoveNationalEventAttachment(ByVal AttachmentCollection As ArrayList, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As FileInfo
        Try
            success = imp.Start()
            If success Then
                For Each obj As DocumentUpload In AttachmentCollection
                    finfo = New FileInfo(TargetPath + obj.Path)
                    If finfo.Exists Then
                        finfo.Delete()
                    End If
                Next

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function GetFromSession(ByVal sObject As String) As Object
        If Session("DEALER") Is Nothing Then
            Response.Redirect("../SessionExpired.htm")
        Else
            Return Session(sObject)
        End If
    End Function

    Private Sub CommitAttachment(ByVal AttachmentCollection As ArrayList)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim TargetFInfo As FileInfo
        Dim TempFInfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As DocumentUpload In AttachmentCollection
                    If Not IsNothing(obj.AttachmentData) Then
                        TargetFInfo = New FileInfo(TargetDirectory + obj.Path)
                        TempFInfo = New FileInfo(TempDirectory + obj.Path)

                        If TempFInfo.Exists Then
                            If Not TargetFInfo.Directory.Exists Then
                                Directory.CreateDirectory(TargetFInfo.DirectoryName)
                            End If
                            TempFInfo.MoveTo(TargetFInfo.FullName)
                        End If
                        obj.AttachmentData.SaveAs(TargetDirectory + obj.Path)
                    End If
                Next

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.EVENTNASIONAL_InputEventNasional_Privilege) Then
            If Not IsNothing(Request.QueryString("Mode")) Then
                If Request.QueryString("Mode").ToLower <> "view" Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT NASIONAL - INPUT EVENT NASIONAL")
                End If
            Else
                Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT NASIONAL - INPUT EVENT NASIONAL")
            End If
        End If
    End Sub
#End Region

#Region "Event Handlers"

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not IsNothing(Request.QueryString("Mode")) Then
            Mode = Request.QueryString("Mode")
            btnBack.Visible = True
        End If
        If Mode = "New" Then
            objDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        Else
            objDealer = CType(sesHelper.GetSession("FrmInputNationalEvent.DEALER"), Dealer)
        End If
        objLoginUser = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = Server.MapPath("") + "\..\DataTemp\"
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If (Not IsPostBack) Then
            InitiateAuthorization()
            BindGridNationalEventDoc()
            BindddlEventType()
            BindddlCity()
            BindddlVenue(ddlCity.SelectedValue)
            BindddlArea1()
            lblPopUpDealer.Attributes("onclick") = "ShowPPDealerCityMultiSelection();"

            If Not IsNothing(Request.QueryString("NationalEventID")) Then
                hdnNationalEventID.Value = Request.QueryString("NationalEventID")
                LoadDataNationalEvent(hdnNationalEventID.Value)
            End If
        End If
    End Sub

    Private Sub ddlCity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCity.SelectedIndexChanged
        If ddlCity.SelectedIndex = 0 Then
            ddlVenue.SelectedIndex = 0
            Exit Sub
        End If
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(NationalEventCity), "ID", MatchType.Exact, ddlCity.SelectedValue))
        Dim arrCity As ArrayList = New NationalEventCityFacade(User).Retrieve(crits)
        Dim objNationalEventCity As New NationalEventCity
        If arrCity.Count > 0 Then
            objNationalEventCity = CType(arrCity(0), NationalEventCity)
        End If
        BindddlVenue(objNationalEventCity.City.ID)
    End Sub

    Private Sub dgUploadFile_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgUploadFile.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgUploadFile.CurrentPageIndex * dgUploadFile.PageSize)

            Dim arrUpload As ArrayList = CType(sesHelper.GetSession(sessNationalEventDoc), ArrayList)
            If Not IsNothing(arrUpload) AndAlso arrUpload.Count > 0 Then
                Dim objDocumentUpload As DocumentUpload = arrUpload(e.Item.ItemIndex)
                Dim lblFileName As Label = CType(e.Item.FindControl("lblFileName"), Label)
                lblFileName.Text = Path.GetFileName(objDocumentUpload.FileName)
            End If
        End If
    End Sub

    Private Sub dgUploadFile_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgUploadFile.ItemCommand
        Dim _arrDataUploadFile As ArrayList = CType(sesHelper.GetSession(sessNationalEventDoc), ArrayList)
        If IsNothing(_arrDataUploadFile) Then _arrDataUploadFile = New ArrayList

        Select Case e.CommandName
            Case "add" 'Insert New item to datagrid
                Dim FileUpload As HtmlInputFile = CType(e.Item.FindControl("UploadFile"), HtmlInputFile)
                Dim txtKeterangan As TextBox = CType(e.Item.FindControl("txtKeterangan"), TextBox)
                Dim objPostedData As HttpPostedFile
                Dim objDocumentUpload As DocumentUpload = New DocumentUpload()
                Dim sFileName As String

                '========= Validasi  =======================================================================
                If IsNothing(FileUpload) OrElse FileUpload.Value = String.Empty Then
                    MessageBox.Show("Lampiran masih kosong")
                    Return
                End If
                Dim _filename As String = System.IO.Path.GetFileName(FileUpload.PostedFile.FileName)
                If _filename.Trim().Length <= 0 Then
                    MessageBox.Show("Upload file belum diisi\n")
                    Return
                End If
                If _filename.Trim().Length > 0 Then
                    If FileUpload.PostedFile.ContentLength > MAX_FILE_SIZE Then
                        MessageBox.Show("Ukuran file tidak boleh melebihi " & (MAX_FILE_SIZE / 1024) & "kb\n")
                        Return
                    End If
                End If
                Dim ext As String = System.IO.Path.GetExtension(FileUpload.PostedFile.FileName)
                If Not (ext.ToUpper() = ".PPT" OrElse ext.ToUpper() = ".PPTX" OrElse ext.ToUpper() = ".XLS" OrElse ext.ToUpper() = ".XLSX" OrElse ext.ToUpper() = ".PDF") Then
                    MessageBox.Show("Hanya menerima file format (PDF/PPT/PPTX/XLS/XLSX)")
                    Return
                End If
                If Not IsNothing(FileUpload) OrElse FileUpload.Value <> String.Empty Then
                    objPostedData = FileUpload.PostedFile
                Else
                    objPostedData = Nothing
                End If

                If Not (IsNothing(objPostedData)) Then
                    sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)

                    If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                        MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                        BindGridNationalEventDoc()
                        Return
                    End If
                    If Not FileIsExist(sFileName, _arrDataUploadFile) Then
                        Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
                        Dim strNationalEventPathConfig As String = KTB.DNet.Lib.WebConfig.GetValue("NationalEventFileDirectory")
                        Dim strNationalEventPathFile As String = "\" & Year(Date.Now) & "\" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)
                        Dim strDestFile As String = strNationalEventPathConfig & strNationalEventPathFile '--Destination File                       

                        objDocumentUpload.Type = 2
                        If Mode = "New" Then
                            objDocumentUpload.DocRegNumber = String.Empty
                        Else
                            objDocumentUpload.DocRegNumber = lblRegNumber.Text
                        End If
                        objDocumentUpload.AttachmentData = objPostedData
                        objDocumentUpload.FileName = sFileName
                        objDocumentUpload.Path = strDestFile
                        objDocumentUpload.FileDescription = IIf(txtKeterangan.Text.Trim = "", "Event Nasional Document", txtKeterangan.Text)

                        UploadAttachment(objDocumentUpload, TempDirectory)

                        _arrDataUploadFile.Add(objDocumentUpload)
                        sesHelper.SetSession(sessNationalEventDoc, _arrDataUploadFile)
                    Else
                        MessageBox.Show(SR.DataIsExist("Attachment File"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Attachment File"))
                End If

            Case "Delete" 'Delete this datagrid item 
                Dim oDocumentUpload As DocumentUpload = CType(_arrDataUploadFile(e.Item.ItemIndex), DocumentUpload)
                If oDocumentUpload.ID > 0 Then
                    Dim arrDelete As ArrayList = CType(sesHelper.GetSession(sessDeleteNationalEventDoc), ArrayList)
                    If IsNothing(arrDelete) Then arrDelete = New ArrayList
                    arrDelete.Add(oDocumentUpload)
                    sesHelper.SetSession(sessDeleteNationalEventDoc, arrDelete)
                End If
                RemoveNationalEventAttachment(CType(_arrDataUploadFile(e.Item.ItemIndex), DocumentUpload), TempDirectory)
                _arrDataUploadFile.RemoveAt(e.Item.ItemIndex)

            Case "Download" 'Download File
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)

        End Select

        BindGridNationalEventDoc()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim str As String
        str = ValidateData()
        If (str.Length > 0) Then
            MessageBox.Show(str)
            Exit Sub
        End If
        If txtDealerCityID.Text.Trim <> "" Then
            If Not ValidateDealerCityIDs(txtDealerCityID.Text.Trim) Then Exit Sub
        End If

        arlNationalEventDoc = CType(sesHelper.GetSession(sessNationalEventDoc), ArrayList)
        arlDeleteNationalEventDoc = CType(sesHelper.GetSession(sessDeleteNationalEventDoc), ArrayList)
        arlDeleteNationalEventDoc = IIf(Not IsNothing(arlDeleteNationalEventDoc), arlDeleteNationalEventDoc, New ArrayList)

        Dim strRegNumber As String = String.Empty
        If Request.QueryString("Mode") <> "Edit" Then
            objNationalEvent = New NationalEvent
            strRegNumber = GetRegNumber()
        Else
            objNationalEvent = CType(sesHelper.GetSession(sessNationalEvent), NationalEvent)
            strRegNumber = lblRegNumber.Text
        End If
        objNationalEvent.RegNumber = strRegNumber
        objNationalEvent.NationalEventType = New NationalEventTypeFacade(User).Retrieve(CInt(ddlEventType.SelectedValue))
        objNationalEvent.NationalEventCity = New NationalEventCityFacade(User).Retrieve(CInt(ddlCity.SelectedValue))
        objNationalEvent.NationalEventVenue = New NationalEventVenueFacade(User).Retrieve(CInt(ddlVenue.SelectedValue))
        objNationalEvent.DealerArea1 = New Area1Facade(User).Retrieve(CInt(ddlArea1.SelectedValue))
        objNationalEvent.PeriodStart = icPeriodStart.Value
        objNationalEvent.PeriodEnd = icPeriodEnd.Value
        objNationalEvent.TargetProspect = txtTargetProspect.Text
        objNationalEvent.TargetSPK = txtTargetSPK.Text

        Dim strDealerCityID As String = String.Empty
        If txtDealerCityID.Text.Trim() <> "" Then
            Dim strDealerCityCodes As String() = txtDealerCityID.Text.Split(";")
            For Each cityCode As String In strDealerCityCodes
                Dim oCity As City = New KTB.DNet.BusinessFacade.General.CityFacade(User).Retrieve(cityCode)
                If Not IsNothing(oCity) AndAlso oCity.ID > 0 Then
                    If strDealerCityID = String.Empty Then
                        strDealerCityID = CStr(oCity.ID)
                    Else
                        strDealerCityID += ";" & CStr(oCity.ID)
                    End If
                End If
            Next
        End If
        objNationalEvent.DealerCityID = strDealerCityID

        Dim _result As Integer = 0
        If Request.QueryString("Mode") <> "Edit" Then
            For Each obj As DocumentUpload In arlNationalEventDoc
                obj.DocRegNumber = objNationalEvent.RegNumber
            Next
            _result = New NationalEventFacade(User).InsertTransaction(objNationalEvent, arlNationalEventDoc)
        Else
            _result = New NationalEventFacade(User).UpdateTransaction(objNationalEvent, arlNationalEventDoc, arlDeleteNationalEventDoc)
        End If

        Dim strJs As String = String.Empty
        If _result > 0 Then
            CommitAttachment(arlNationalEventDoc)
            If Request.QueryString("Mode") = "Edit" Then
                RemoveNationalEventAttachment(arlDeleteNationalEventDoc, TargetDirectory)
            End If
            strJs = "alert('Data Event Nasional berhasil di simpan');"
            strJs += "window.location = '../EventNational/FrmDaftarEventNational.aspx';"
            ClearAll()
        Else
            strJs = "alert('Data Event Nasional gagal di simpan');"
        End If

        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
    End Sub

    Private Sub btnBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        ClearAll()
        btnSave.Visible = True
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/EventNational/FrmDaftarEventNational.aspx")
    End Sub

    Private Sub ddlArea1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlArea1.SelectedIndexChanged
        txtDealerCityID.Text = ""
        txtDealerCityName.Text = ""
    End Sub

    Private Sub txtDealerCityID_TextChanged(sender As Object, e As EventArgs) Handles txtDealerCityID.TextChanged
        Dim oCity As New City
        Dim strDealerCityName As String = String.Empty
        Dim arrDealerCityCode As String() = txtDealerCityID.Text.Split(";")
        For Each dealerCityCode As String In arrDealerCityCode
            oCity = New KTB.DNet.BusinessFacade.General.CityFacade(User).Retrieve(dealerCityCode)
            If Not IsNothing(oCity) AndAlso oCity.ID > 0 Then
                If strDealerCityName = String.Empty Then
                    strDealerCityName = oCity.CityName
                Else
                    strDealerCityName += ";" & oCity.CityName
                End If
            End If
        Next
        txtDealerCityName.text = strDealerCityName
    End Sub

    Private Sub txtDealerCityName_TextChanged(sender As Object, e As EventArgs) Handles txtDealerCityName.TextChanged

    End Sub

#End Region

End Class

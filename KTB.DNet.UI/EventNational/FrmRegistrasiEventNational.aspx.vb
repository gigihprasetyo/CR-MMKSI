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

Public Class FrmRegistrasiEventNational
    Inherits System.Web.UI.Page

#Region "Custom Variables Declaration"

    Dim objDealer As New Dealer
    Dim objLoginUser As UserInfo
    Dim objNationalEvent As NationalEvent

    'Private arlEventReportDtl As ArrayList = New ArrayList
    'Private arlEventReportDoc As ArrayList = New ArrayList
    'Private arlEventReportAct As ArrayList = New ArrayList

    Const sessNationalEvent As String = "sessDataNationalEvent"
    Const sessNationalEventDetail As String = "sessDataNationalEventDetail"

    Private MAX_FILE_SIZE As Integer = 5120000
    Private TempDirectory As String
    Private TargetDirectory As String
    Private sesHelper As New SessionHelper
    Private intActivityType As Integer = 0
    Private intItemIndex As Integer = 0
    Dim Mode As String = "New"
    Dim isRemove As Boolean = True

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

    Private Sub LoadDataNationalEvent(intNationalEventID As Integer)
        objNationalEvent = New NationalEventFacade(User).Retrieve(intNationalEventID)
        If Not IsNothing(objNationalEvent) Then
            sesHelper.SetSession(sessNationalEvent, objNationalEvent)

            Me.lblDealerCodeName.Text = SesDealer().DealerCode & " / " & SesDealer().DealerName
            Me.txtDealerCode.Text = SesDealer().DealerCode
            Me.lblNamaEvent.Text = objNationalEvent.NationalEventType.Name & " " & objNationalEvent.NationalEventCity.City.CityName
            Me.lblPeriode.Text = objNationalEvent.PeriodStart.ToString("dd/MM/yyyy") & " s.d " & objNationalEvent.PeriodEnd.ToString("dd/MM/yyyy")

            Me.btnBack.Visible = True

            Dim objNationalEventDetail As NationalEventDetail
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(NationalEventDetail), "NationalEvent.ID", MatchType.Exact, intNationalEventID))
            crit.opAnd(New Criteria(GetType(NationalEventDetail), "Dealer.ID", MatchType.Exact, SesDealer().ID))
            Dim arrNationalEventDetail As ArrayList = New NationalEventDetailFacade(User).Retrieve(crit)
            If Not IsNothing(arrNationalEventDetail) AndAlso arrNationalEventDetail.Count > 0 Then
                objNationalEventDetail = arrNationalEventDetail(0)
                If Not IsNothing(objNationalEventDetail) AndAlso objNationalEventDetail.ID > 0 Then
                    sesHelper.SetSession(sessNationalEventDetail, objNationalEventDetail)
                    txtNamaDealerPIC.Text = objNationalEventDetail.PICDealerName
                    txtNoHandphone.Text = objNationalEventDetail.PICDealerHPNo
                    txtAlamatEmail.Text = objNationalEventDetail.PICDealerEmail

                    Dim salesCode As String = ""
                    Dim salesmanName As String = ""
                    For Each salesID As String In objNationalEventDetail.SalesmanID.Split(";")
                        salesCode = salesCode & New KTB.DNet.BusinessFacade.Salesman.SalesmanHeaderFacade(User).Retrieve(CInt(salesID)).SalesmanCode & ";"
                        salesmanName = salesmanName & New KTB.DNet.BusinessFacade.Salesman.SalesmanHeaderFacade(User).Retrieve(CInt(salesID)).Name & ";"
                    Next
                    salesCode = salesCode.Remove(salesCode.Length - 1)
                    txtSalesman.Text = salesCode
                    salesmanName = salesmanName.Remove(salesmanName.Length - 1)
                    txtSalesmanShow.Text = salesmanName

                    Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventDetailDate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crits.opAnd(New Criteria(GetType(NationalEventDetailDate), "NationalEventDetail.ID", MatchType.Exact, objNationalEventDetail.ID))
                    Dim NatEventDetDate As ArrayList = New NationalEventDetailDateFacade(User).Retrieve(crits)
                    RefreshCalendar(NatEventDetDate)
                    lblJumlahHari.Text = CalKegiatan.SelectedDates.Count

                    hdnNationalEventDetailID.Value = objNationalEventDetail.ID
                Else
                    hdnNationalEventDetailID.Value = "0"
                End If
            Else
                hdnNationalEventDetailID.Value = "0"
            End If

            'If Request.QueryString("Mode") <> "Edit" Then
            '    enabledProperty(False)
            'Else
            '    enabledProperty(True)
            'End If
        End If
    End Sub

    Private Sub enabledProperty(ByVal enb As Boolean)
        Me.btnSave.Visible = enb
    End Sub

    Private Sub ClearTempData()
        'Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        'Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        'Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        'Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        'Dim success As Boolean = False

        'Try
        '    success = imp.Start()
        '    If success Then
        '        Dim dir As New DirectoryInfo(TempDirectory)
        '        dir.Delete(True)
        '    End If
        'Catch ex As Exception
        '    'Throw ex
        'End Try
    End Sub

    Private Sub ClearAll()

        'sesHelper.SetSession(sessEventReportDtl, New ArrayList)
        'sesHelper.SetSession(sessEventReportDoc, New ArrayList)
        'sesHelper.SetSession(sessEventReportAct, New ArrayList)

    End Sub

    Protected Sub CalKegiatan_PreRender(sender As Object, e As EventArgs)
        Dim calender As Calendar = sender

        If isRemove And Not String.IsNullOrEmpty(Request.Form("__EVENTTARGET")) Then
            If Request("__EVENTTARGET").Contains(calender.ID) Then
                calender.SelectedDates.Clear()
                tglList.Items.Clear()
                lblJumlahHari.Text = calender.SelectedDates.Count
                Exit Sub
            End If
        End If

        calender.SelectedDates.Clear()
        For Each item As ListItem In tglList.Items
            calender.SelectedDates.Add(CDate(item.Value))
        Next

        If tglList.Items.Count > 0 Then
            calender.VisibleDate = calender.SelectedDates(0).Date
        End If

        tglList.Items.Clear()
        For Each dt As Date In calender.SelectedDates
            tglList.Items.Add(FormatDateTime(dt, 2))
        Next

        lblJumlahHari.Text = calender.SelectedDates.Count
    End Sub

    Protected Sub CalKegiatan_SelectionChanged(sender As Object, e As EventArgs)
        Dim calender As Calendar = sender
        objNationalEvent = sesHelper.GetSession(sessNationalEvent)
        If calender.SelectedDate < objNationalEvent.PeriodStart OrElse calender.SelectedDate > objNationalEvent.PeriodEnd Then
            MessageBox.Show("Jadwal Dealer yang dipilih: " & calender.SelectedDate.ToString("dd/MM/yyyy") & " diluar Periode Event")
            isRemove = False
            Exit Sub
        End If


        Dim item As ListItem = tglList.Items.FindByValue(FormatDateTime(calender.SelectedDate, 2))
        If IsNothing(item) Then
            tglList.Items.Add(FormatDateTime(calender.SelectedDate, 2))
        Else
            tglList.Items.Remove(item)
        End If

        isRemove = False
    End Sub

    Protected Sub CalKegiatan_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs)
        isRemove = False
    End Sub

    Private Sub RefreshCalendar(ByVal data As ArrayList)
        tglList.Items.Clear()
        For Each dt As NationalEventDetailDate In data
            tglList.Items.Add(FormatDateTime(dt.ActivityDate, 2))
        Next
    End Sub

    Private Function TimeStamp() As String
        Return DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & DateTime.Now.Millisecond
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
                For Each obj As BabitEventReportDocument In AttachmentCollection
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

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Sub GetDealerData(ByVal oDealer As Dealer)
        txtDealerCode.Visible = False
        lblPopUpDealer.Visible = False
        lblDealerCodeName.Visible = True
        lblDealerCodeName.Text = oDealer.DealerCode & " / " & oDealer.DealerName
        lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"
    End Sub

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        If Not IsLoginAsDealer() Then
            If (txtDealerCode.Text = String.Empty) Then
                sb.Append("Kode Dealer Harus Diisi\n")
            End If
        End If

        If txtNamaDealerPIC.Text.Trim = "" Then
            sb.Append("Nama Dealer PIC harus Diisi\n")
        End If

        If txtNoHandphone.Text.Trim = "" OrElse txtNoHandphone.Text.Trim = "0" Then
            sb.Append("No Handphone harus Diisi\n")
        End If

        If txtAlamatEmail.Text.Trim = "" Then
            sb.Append("Alamat Email harus Diisi\n")
        End If

        If txtSalesman.Text.Trim = "" Then
            sb.Append("Salesman Harus Diisi\n")
        End If

        If tglList.Items.Count = 0 Then
            sb.Append("Jadwal Dealer Harus Diisi\n")
        End If

        Return sb.ToString()
    End Function

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Mode = "New" Then
            If Not SecurityProvider.Authorize(Context.User, SR.EVENT_Input_Event_Laporan_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT - INPUT LAPORAN EVENT ")
            End If
        ElseIf Mode = "Edit" Then
            If Not SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Laporan_Edit_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT - EDIT LAPORAN EVENT ")
            End If
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Laporan_Display_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT - DISPLAY LAPORAN EVENT ")
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
        objDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        objLoginUser = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = Server.MapPath("") + "\..\DataTemp\"
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If (Not IsPostBack) Then
            'InitiateAuthorization()

            'ClearAll()

            If Not IsNothing(Request.QueryString("NationalEventID")) Then
                hdnNationalEventID.Value = Request.QueryString("NationalEventID")
                LoadDataNationalEvent(hdnNationalEventID.Value)
            End If
            'GetDealerData(objDealer)

            lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            lblSearchSalesman.Attributes("onclick") = "ShowPopUpSalesman();"
            'lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelectionGab('" & strDealerGroupID & "','" & CType(GetFromSession("DEALER"), Dealer).DealerCode & "');"

        End If
    End Sub

    Private Function GetFromSession(ByVal sObject As String) As Object
        If Session("DEALER") Is Nothing Then
            Response.Redirect("../SessionExpired.htm")
        Else
            Return Session(sObject)
        End If
    End Function


    Private Function ValidateDealers(ByVal _dealers As String) As Boolean
        Dim bcheck As Boolean = True
        Dim i As Integer
        Dim items() As String = _dealers.Split(";")
        For i = 0 To items.Length - 1
            Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(items(i))
            If objDealerTmp.ID = 0 Then
                MessageBox.Show("Dealer " + items(i) + " tidak valid")
                bcheck = False
                Exit For
            End If

        Next
        If ValidateDealerDuplication(_dealers) <> String.Empty Then
            MessageBox.Show("Duplikasi Dealer " + ValidateDealerDuplication(_dealers))
            bcheck = False
        End If
        Return bcheck
    End Function

    Private Function ValidateDealerDuplication(ByVal _dealers As String) As String
        Dim bcheck As Boolean = True
        Dim _dealerDuplicate As String = String.Empty
        Dim i As Integer
        Dim j As Integer
        Dim list() As String = _dealers.Split(";")
        For i = 0 To list.Length - 2
            For j = i + 1 To list.Length - 1
                If list(i) = list(j) Then
                    bcheck = False
                    Exit For
                End If
            Next
            If bcheck = False Then
                _dealerDuplicate = list(i)
                Exit For
            End If
        Next
        Return _dealerDuplicate
    End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim str As String
        str = ValidateData()
        If (str.Length > 0) Then
            MessageBox.Show(str)
            Exit Sub
        End If
        If Not Page.IsValid Then
            Return
        End If

        Dim salesmanID As String = ""
        For Each salesCode As String In txtSalesman.Text.Trim.Split(";")
            salesmanID = salesmanID & New KTB.DNet.BusinessFacade.Salesman.SalesmanHeaderFacade(User).Retrieve(salesCode).ID & ";"
        Next
        salesmanID = salesmanID.Remove(salesmanID.Length - 1)

        Dim objNationalEventDetail As New NationalEventDetail
        With objNationalEventDetail
            .PICDealerName = txtNamaDealerPIC.Text
            .PICDealerHPNo = txtNoHandphone.Text.Trim
            .PICDealerEmail = txtAlamatEmail.Text.Trim
            .SalesmanID = salesmanID
            .NationalEvent = CType(sesHelper.GetSession(sessNationalEvent), NationalEvent)
            .Dealer = SesDealer()
        End With

        Dim _result As Integer = 0
        Dim _resultDetail As Integer = 0
        If hdnNationalEventDetailID.Value = "0" Or IsNothing(hdnNationalEventDetailID.Value) Then
            _result = New NationalEventDetailFacade(User).Insert(objNationalEventDetail)
            sesHelper.SetSession(sessNationalEventDetail, objNationalEventDetail)
            For Each tgl As Date In CalKegiatan.SelectedDates
                Dim objNationalEventDetailDate As New NationalEventDetailDate
                objNationalEventDetailDate.ActivityDate = tgl
                objNationalEventDetailDate.NationalEventDetail = New NationalEventDetailFacade(User).Retrieve(_result)
                _resultDetail = New NationalEventDetailDateFacade(User).Insert(objNationalEventDetailDate)
            Next
        Else
            objNationalEventDetail.ID = hdnNationalEventDetailID.Value
            _result = New NationalEventDetailFacade(User).Update(objNationalEventDetail)

            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventDetailDate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(NationalEventDetailDate), "NationalEventDetail.ID", MatchType.Exact, hdnNationalEventDetailID.Value))

            Dim arrNatEventDetDates As ArrayList = New NationalEventDetailDateFacade(User).Retrieve(crits)
            For Each objNatEventDetDate As NationalEventDetailDate In arrNatEventDetDates
                Dim delDate As NationalEventDetailDateFacade = New NationalEventDetailDateFacade(User)
                delDate.DeleteFromDB(objNatEventDetDate)
            Next

            For Each tgl As Date In CalKegiatan.SelectedDates
                Dim objNationalEventDetailDate As New NationalEventDetailDate
                objNationalEventDetailDate.ActivityDate = tgl
                objNationalEventDetailDate.NationalEventDetail = New NationalEventDetailFacade(User).Retrieve(CInt(hdnNationalEventDetailID.Value))
                _resultDetail = New NationalEventDetailDateFacade(User).Insert(objNationalEventDetailDate)
            Next

            _result = 1
        End If

        Dim strJs As String = String.Empty
        If _result > 0 Then
            strJs = "alert('Simpan Data Berhasil');"
            strJs += "window.location = '../EventNational/FrmDaftarEventNational.aspx';"
            ClearTempData()
            ClearAll()
        Else
            strJs = "alert('Simpan Data Gagal');"
        End If
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
    End Sub

    Private Sub btnGetInfoDealer_Click(sender As Object, e As EventArgs) Handles btnGetInfoDealer.Click
        Dim objDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
        lblDealerCodeName.Text = objDealer.DealerCode & " / " & objDealer.DealerName
    End Sub

    'Private Sub btnGetInfoDealerBranch_Click(sender As Object, e As EventArgs) Handles btnGetInfoDealerBranch.Click
    '    Dim objDealerBranch As DealerBranch = New DealerBranchFacade(User).Retrieve(hdntxtTOCode.Value.Trim())
    '    lblTOName.Text = objDealerBranch.Name
    '    txtTOCode.Text = objDealerBranch.DealerBranchCode
    'End Sub

    'Private Sub btnBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaru.Click
    '    Me.ClearAll()
    '    Me.btnSave.Visible = True
    'End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/EventNational/FrmDaftarEventNational.aspx")
    End Sub

#End Region

    Protected Sub txtSalesman_TextChanged(sender As Object, e As EventArgs) Handles txtSalesman.TextChanged
        Dim salesmanName As String = ""
        For Each salesCode As String In txtSalesman.Text.Trim.Split(";")
            salesmanName = salesmanName & New KTB.DNet.BusinessFacade.Salesman.SalesmanHeaderFacade(User).Retrieve(salesCode).Name & ";"
        Next
        salesmanName = salesmanName.Remove(salesmanName.Length - 1)
        txtSalesmanShow.Text = salesmanName
    End Sub
End Class
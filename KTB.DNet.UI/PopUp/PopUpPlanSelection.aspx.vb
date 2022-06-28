Imports DayPilot.Utils
Imports DayPilot.Web.Ui
Imports DayPilot.Web.Ui.Events.Bubble
Imports System.Collections.Generic
Imports System.Linq
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.AfterSales
Imports DayPilot.Web.Ui.Events.Common
Imports DayPilot.Web.Ui.Events.Scheduler
Imports DayPilot.Json

Public Class PopUpPlanSelection
    Inherits System.Web.UI.Page

    Private sessHelper As SessionHelper = New SessionHelper
    Private isRemove As Boolean = True
    Private crit As CriteriaComposite
    Private objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private stallMasterFacade As StallMasterFacade = New StallMasterFacade(User)
    Private stallWorkingTimeFacade As StallWorkingTimeFacade = New StallWorkingTimeFacade(User)
    Private standardCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
    Private serviceBookingFacade As ServiceBookingFacade = New ServiceBookingFacade(User)
    Private assistServiceTypeFacade As AssistServiceTypeFacade = New AssistServiceTypeFacade(User)
    Private serviceStandardTimeFacade As ServiceStandardTimeFacade = New ServiceStandardTimeFacade(User)
    Private appConfFacade As AppConfigFacade = New AppConfigFacade(User)
    Private ccRFacade As CustomerCaseResponseFacade = New CustomerCaseResponseFacade(User)
    Private isMQPExist As Boolean = False
    Private fSKindFacade As FSKindFacade = New FSKindFacade(User)
    Private pMKindFacade As PMKindFacade = New PMKindFacade(User)
    Private gRKindFacade As GRKindFacade = New GRKindFacade(User)
    Private recallCategoryFacade As RecallCategoryFacade = New RecallCategoryFacade(User)

#Region "Event Handler"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Check Parameter


            If Not String.IsNullorEmpty(Request.QueryString("ccid")) AndAlso Request.QueryString("ccid") <> "0" Then
                Dim tgl As DateTime
                Dim ccR As CustomerCaseResponse = ccRFacade.Retrieve(CInt(Request.QueryString("ccid")))
                If ccR.BookingDatetime.Year > 1900 Then
                    tgl = ccR.BookingDatetime
                ElseIf ccR.CustomerCase.BookingDatetime.Year > 1900 Then
                    tgl = ccR.CustomerCase.BookingDatetime
                Else
                    lblInfoBooking.Visible = False
                End If

                lblInfoBooking.Text = String.Format("Tanggal Permintaan Service Booking : {0}", tgl.ToString("dd MMMM yyyy HH:mm"))
            Else
                lblInfoBooking.Visible = False
            End If

            CalWork.SelectedDate = DateTime.Today
            RefreshData(DateTime.Today, isMQPExist)
            InitDdl(isMQPExist)
        End If
    End Sub

    Protected Sub CalWork_DayRender(sender As Object, e As DayRenderEventArgs)
        Dim lst As List(Of StallWorkingTime) = CType(sessHelper.GetSession("TimeWork"), List(Of StallWorkingTime))
        Dim isValid As Boolean = lst.Where(Function(x) x.Tanggal = e.Day.Date).Count > 0
        If isValid Then
            Dim isHoliday As Boolean = lst.Where(Function(x) x.Tanggal = e.Day.Date And x.IsHoliday = 0).Count = 0
            If isHoliday Then
                'e.Cell.ForeColor = Color.Red
                e.Cell.Style.Add("background-color", "#ff3300")
            Else
                e.Cell.BackColor = Color.DarkSeaGreen
            End If
        Else
            e.Cell.BackColor = Color.DarkSlateGray
        End If
        e.Cell.BorderColor = Color.Black
        e.Cell.BorderStyle = BorderStyle.Solid
    End Sub

    Protected Sub CalWork_SelectionChanged(sender As Object, e As EventArgs)
        RefreshData(CalWork.SelectedDate, isMQPExist)
        InitDdl(isMQPExist)
    End Sub

    Protected Sub CalWork_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs)
        RefreshData(e.NewDate)
    End Sub

    Protected Sub DayPilotScheduler1_PreRender(sender As Object, e As EventArgs)
        RefreshData(CalWork.SelectedDate)
    End Sub

    Protected Sub DayPilotScheduler1_BeforeEventRender(sender As Object, e As DayPilot.Web.Ui.Events.Scheduler.BeforeEventRenderEventArgs) Handles DayPilotScheduler1.BeforeEventRender
        If Not IsNothing(e.DataItem) Then
            e.DurationBarColor = e.DataItem("color").ToString
            e.BackgroundColor = e.DataItem("color").ToString
        End If
    End Sub

    Protected Sub DayPilotScheduler1_TimeRangeSelected(sender As Object, e As DayPilot.Web.Ui.Events.TimeRangeSelectedEventArgs) Handles DayPilotScheduler1.TimeRangeSelected
        crit = New CriteriaComposite(New Criteria(GetType(StallWorkingTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "StallMaster.Status", MatchType.Exact, CType(EnumStallMaster.StatusStall.Aktif, Short)))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "StallMaster.ID", MatchType.Exact, e.Resource))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "Tanggal", MatchType.Exact, CDate(e.Start)))


        Dim datas As ArrayList = stallWorkingTimeFacade.Retrieve(crit)
        If datas.Count > 0 Then
            Dim obj As StallWorkingTime = datas(0)
            lblTgl.Text = obj.Tanggal.ToString("dd MMMM yyyy")
            hdTgl.Value = obj.Tanggal.ToString("yyyy-MM-dd")
            lblJamOp.Text = String.Format("{0} s/d {1}", obj.TimeStart.ToString("HH:mm"), obj.TimeEnd.ToString("HH:mm"))
            lblJamBreak.Text = String.Format("{0} s/d {1}", obj.RestTimeStart.ToString("HH:mm"), obj.RestTimeEnd.ToString("HH:mm"))
            txtBookFrom.Text = e.Start.ToString("HH:mm")
            lblKodeStall.Text = obj.StallMaster.StallCodeDealer
            lblEstimasi.Text = "N/A"
            hdStandardTime.Value = 0
            ddlJnsService.ClearSelection()
            Dim ds As DataSet

            ' Dim objsba As ArrayList = CType(sessHelper.GetSession("SBActivity"), ArrayList)
            If Not IsNothing(sessHelper.GetSession("SBActivity")) Then
                Dim objSBAS As List(Of ServiceBookingActivity) = CType(sessHelper.GetSession("SBActivity"), ArrayList).Cast(Of ServiceBookingActivity).ToList
                'Dim KindCode As String = GetKindCodeByID(objSBAS., obj.KindCode)

                'Dim dtResult As DataTable = CommonFunction.ConvertArrayListToDataTable(New ArrayList(objSBAS.Select(Function(s) _
                '                                 New With {.ServiceTypeID = s.ServiceTypeID, _
                '                                           .VechileModelID = CShort(Request.QueryString("modelid").ToString), _
                '                                           .VechileTypeID = CShort(Request.QueryString("vechiletypeid").ToString), _
                '                                           .KindCode = s.KindCode, _
                '                                           .AssistServiceTypeCode = ""}).ToList))

                Dim dtResult As DataTable = CommonFunction.ConvertArrayListToDataTable(New ArrayList(objSBAS.Select(Function(s) _
                                                 New With {.ServiceTypeID = s.ServiceTypeID, _
                                                           .VechileModelID = CShort(Request.QueryString("modelid").ToString), _
                                                           .VechileTypeID = CShort(Request.QueryString("vechiletypeid").ToString), _
                                                           .KindCode = GetKindCodeByID(s.ServiceTypeID, s.KindCode), _
                                                           .AssistServiceTypeCode = ""}).ToList))

                ds = serviceStandardTimeFacade.GetServiceStandardTime(objDealer.ID, dtResult)

            End If
            If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dt As DataTable = ds.Tables(0)
                hdCalculate.Value = dt.Rows(0)(0)
            Else
                hdCalculate.Value = 0
            End If

            txtBookTo.Text = e.Start.ToString("HH:mm")

            btnSave.Enabled = True
            btnSave.Visible = True
            RegisterStartupScript("Open", "<script>PopUpOpen();</script>")
            Return
        Else
            MessageBox.Show("Stall - Setting Kalender Kerja belum di buat.")
        End If
    End Sub

    Protected Sub DayPilotScheduler1_EventClick(sender As Object, e As DayPilot.Web.Ui.Events.EventClickEventArgs) Handles DayPilotScheduler1.EventClick
        Dim sb As ServiceBooking = serviceBookingFacade.Retrieve(CInt(e.Id))

        crit = New CriteriaComposite(New Criteria(GetType(StallWorkingTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "StallMaster.Status", MatchType.Exact, CType(EnumStallMaster.StatusStall.Aktif, Short)))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "StallMaster.ID", MatchType.Exact, sb.StallMaster.ID))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "Tanggal", MatchType.Exact, CDate(sb.WorkingTimeStart)))

        Dim datas As ArrayList = stallWorkingTimeFacade.Retrieve(crit)
        If datas.Count > 0 Then
            Dim obj As StallWorkingTime = datas(0)
            lblTgl.Text = obj.Tanggal.ToString("dd MMMM yyyy")
            hdTgl.Value = obj.Tanggal.ToString("yyyy-MM-dd")
            lblJamOp.Text = String.Format("{0} s/d {1}", obj.TimeStart.ToString("HH:mm"), obj.TimeEnd.ToString("HH:mm"))
            lblJamBreak.Text = String.Format("{0} s/d {1}", obj.RestTimeStart.ToString("HH:mm"), obj.RestTimeEnd.ToString("HH:mm"))
            txtBookFrom.Text = sb.WorkingTimeStart.ToString("HH:mm")
            txtBookTo.Text = sb.WorkingTimeEnd.ToString("HH:mm")
            lblKodeStall.Text = sb.StallMaster.StallCodeDealer
            hdStandardTime.Value = sb.StandardTime

            If sb.StallServiceType <> 0 Then
                ddlJnsService.ClearSelection()
                ddlJnsService.Items.FindByValue(sb.StallServiceType.ToString).Selected = True
                lblEstimasi.Text = String.Format("{0} jam", FormatNumber(sb.StandardTime, 2))
            Else
                lblEstimasi.Text = "N/A"
            End If

            Dim ds As DataSet

            ' Dim objsba As ArrayList = CType(sessHelper.GetSession("SBActivity"), ArrayList)
            If Not IsNothing(sessHelper.GetSession("SBActivity")) Then
                Dim objSBAS As List(Of ServiceBookingActivity) = CType(sessHelper.GetSession("SBActivity"), ArrayList).Cast(Of ServiceBookingActivity).ToList

                'Dim dtResult = CommonFunction.ConvertArrayListToDataTable(New ArrayList(objSBAS.Select(Function(s) _
                '                                 New With {.ServiceTypeID = s.ServiceTypeID, _
                '                                           .VechileModelID = CShort(Request.QueryString("modelid").ToString), _
                '                                           .VechileTypeID = CShort(Request.QueryString("vechiletypeid").ToString), _
                '                                           .KindCode = s.KindCode, _
                '                                           .AssistServiceTypeCode = ""}).ToList))
                Dim dtResult As DataTable = CommonFunction.ConvertArrayListToDataTable(New ArrayList(objSBAS.Select(Function(s) _
                                     New With {.ServiceTypeID = s.ServiceTypeID, _
                                               .VechileModelID = CShort(Request.QueryString("modelid").ToString), _
                                               .VechileTypeID = CShort(Request.QueryString("vechiletypeid").ToString), _
                                               .KindCode = GetKindCodeByID(s.ServiceTypeID, s.KindCode), _
                                               .AssistServiceTypeCode = ""}).ToList))

                ds = serviceStandardTimeFacade.GetServiceStandardTime(objDealer.ID, dtResult)

            End If

            If Not IsNothing(ds) AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dt As DataTable = ds.Tables(0)
                hdCalculate.Value = dt.Rows(0)(0)
            Else
                hdCalculate.Value = 0
            End If

            btnSave.Enabled = CInt(Request.QueryString("id").ToString) = e.Id
            btnSave.Visible = CInt(Request.QueryString("id").ToString) = e.Id

            RegisterStartupScript("Open", "<script>PopUpOpen();</script>")
            Return
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim msgErr As String = String.Empty
        If Not Validate(msgErr) Then
            MessageBox.Show(msgErr)
            RegisterStartupScript("Open", "<script>PopUpOpen();</script>")
            Exit Sub
        End If

        Dim result As String = String.Format("{0};{1};{2};{3}", String.Format("{0} - {1} - {2}", _
                                lblTgl.Text, txtBookFrom.Text, txtBookTo.Text), lblKodeStall.Text, hdStandardTime.Value, ddlJnsService.SelectedValue)

        RegisterStartupScript("Save", String.Format("<script>onSuccess('{0}');</script>", result))
        Return
    End Sub
#End Region

#Region "Custom Method"
    Public Sub RefreshData(ByVal tanggal As Date, Optional ByRef isMQPExist As Boolean = False)
        GetAllData(tanggal)
        GetResources(isMQPExist)
        DayPilotScheduler1.StartDate = tanggal
        DayPilotScheduler1.DataSource = GetData()
        DayPilotScheduler1.DataBind()
    End Sub

    Private Sub GetAllData(ByVal tanggal As Date)
        crit = New CriteriaComposite(New Criteria(GetType(StallWorkingTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "Tanggal", MatchType.GreaterOrEqual, New DateTime(tanggal.Year, tanggal.Month, 1).AddDays(-60)))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "Tanggal", MatchType.LesserOrEqual, New DateTime(tanggal.Year, tanggal.Month, DateTime.DaysInMonth(tanggal.Year, tanggal.Month)).AddDays(60)))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "StallMaster.Status", MatchType.Exact, CType(EnumStallMaster.StatusStall.Aktif, Short)))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "StallMaster.StallType", MatchType.NotInSet, CType(EnumStallMaster.TipeStall.Washing, Short)))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "VisitType", MatchType.Exact, CType(EnumStallMaster.VisitType.BO, Short)))

        Dim lst As List(Of StallWorkingTime) = stallWorkingTimeFacade.Retrieve(crit).Cast(Of StallWorkingTime)().ToList
        sessHelper.SetSession("TimeWork", lst)
    End Sub

    Private Function GetResources(ByRef isMQPExist As Boolean) As List(Of DayPilot.Web.Ui.Resource)
        DayPilotScheduler1.Resources.Clear()
        Dim res As DayPilot.Web.Ui.Resource = New DayPilot.Web.Ui.Resource

        crit = New CriteriaComposite(New Criteria(GetType(StallWorkingTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "Tanggal", MatchType.Exact, CalWork.SelectedDate))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "IsHoliday", MatchType.Exact, 0))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "StallMaster.Status", MatchType.Exact, CType(EnumStallMaster.StatusStall.Aktif, Short)))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "StallMaster.StallType", MatchType.NotInSet, CType(EnumStallMaster.TipeStall.Washing, Short)))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "VisitType", MatchType.Exact, CType(EnumStallMaster.VisitType.BO, Short)))
        'crit.opAnd(New Criteria(GetType(StallWorkingTime), "VisitType", MatchType.InSet, String.Format("({0}, {1})", _
        'CType(EnumStallMaster.VisitType.BO, Short), _
        'CType(EnumStallMaster.VisitType.WI, Short))))


        Dim lst As List(Of StallWorkingTime) = stallWorkingTimeFacade.Retrieve(crit).Cast(Of StallWorkingTime).ToList
        isMQPExist = False

        If lst.Count > 0 Then
            For Each item As StallMaster In lst.Select(Function(x) x.StallMaster).Distinct
                'If item.StallType = CInt(EnumStallMaster.TipeStall.RealTimeService) Or item.StallType = CInt(EnumStallMaster.TipeStall.Booking) Then
                'End If
                If item.StallType = CInt(EnumStallMaster.TipeStall.MQP) Then
                    isMQPExist = True
                End If

                crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallMaster.TipeStall"))
                crit.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, item.StallType))

                res = New DayPilot.Web.Ui.Resource
                res.Name = String.Format("{0} ({1})", item.StallCodeDealer, CType(standardCodeFacade.Retrieve(crit)(0), StandardCode).ValueDesc)
                res.Value = item.ID
                DayPilotScheduler1.Resources.Add(res)
            Next
        End If
    End Function

    Private Function GetData() As DataTable
        Dim dt As DataTable
        dt = New DataTable()
        dt.Columns.Add("start", GetType(DateTime))
        dt.Columns.Add("end", GetType(DateTime))
        dt.Columns.Add("name", GetType(String))
        dt.Columns.Add("id", GetType(String))
        dt.Columns.Add("resource", GetType(String))
        dt.Columns.Add("color", GetType(String))

        Dim dr As DataRow

        crit = New CriteriaComposite(New Criteria(GetType(ServiceBooking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(ServiceBooking), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
        crit.opAnd(New Criteria(GetType(ServiceBooking), "WorkingTimeStart", MatchType.GreaterOrEqual, CalWork.SelectedDate.Date))
        crit.opAnd(New Criteria(GetType(ServiceBooking), "WorkingTimeEnd", MatchType.LesserOrEqual, CalWork.SelectedDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59)))
        'If sessHelper.GetSession("StatusSB") = "Request" Then
        '    crit.opAnd(New Criteria(GetType(ServiceBooking), "Status", MatchType.Exact, CInt(EnumStallMaster.StatusBooking.Request).ToString))
        'Else
        '    crit.opAnd(New Criteria(GetType(ServiceBooking), "Status", MatchType.Exact, CInt(EnumStallMaster.StatusBooking.Booked).ToString))
        'End If
        crit.opAnd(New Criteria(GetType(ServiceBooking), "Status", MatchType.Greater, CInt(EnumStallMaster.StatusBooking.Batal).ToString))
        Dim lst As ArrayList = serviceBookingFacade.Retrieve(crit)

        For Each item As ServiceBooking In lst
            crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallMaster.TipeStall"))
            crit.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, item.StallMaster.StallType))

            dr = dt.NewRow()
            dr("id") = item.ID.ToString
            dr("start") = item.WorkingTimeStart
            dr("end") = item.WorkingTimeEnd
            dr("resource") = item.StallMaster.ID
            dr("color") = GetColorStall(CType(standardCodeFacade.Retrieve(crit)(0), StandardCode).ValueDesc)
            dr("name") = "Booked"

            dt.Rows.Add(dr)
        Next

        dt.PrimaryKey = New DataColumn() {dt.Columns("id")}

        Return dt

    End Function

    Private Function GetColorStall(ByVal type As String) As String
        Dim color As String = String.Empty
        Select Case type
            Case "MQP"
                color = "darkcyan"
            Case "Booking"
                color = "darkolivegreen"
            Case "WalkIn"
                color = "blueviolet"
            Case "Washing"
                color = "sandybrown"
            Case "Real Time Service"
                color = "blue"
        End Select

        Return color
    End Function

    Private Function GetKindCodeByID(ByVal JenisKegiatan As Integer, ByVal JenisService As String) As String
        Dim nResult As String = ""
        Select Case JenisKegiatan

            Case 1
                Dim objFSKind As New FSKind
                crit = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(FSKind), "ID", MatchType.Exact, JenisService))
                Dim results As ArrayList = fSKindFacade.Retrieve(crit)
                If results.Count > 0 Then
                    objFSKind = CType(results(0), FSKind)
                    nResult = objFSKind.KindCode
                End If

            Case 2
                Dim objPMKind As New PMKind
                crit = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(PMKind), "ID", MatchType.Exact, JenisService))
                Dim results As ArrayList = pMKindFacade.Retrieve(crit)

                If results.Count > 0 Then
                    objPMKind = CType(results(0), PMKind)
                    nResult = objPMKind.KindCode
                End If
            Case 3
                Dim objRC As New RecallCategory
                crit = New CriteriaComposite(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(RecallCategory), "ID", MatchType.Exact, JenisService))
                Dim results As ArrayList = recallCategoryFacade.Retrieve(crit)

                If results.Count > 0 Then
                    objRC = CType(results(0), RecallCategory)
                    nResult = objRC.RecallRegNo
                End If
            Case 4
                Dim objGRKind As New GRKind
                crit = New CriteriaComposite(New Criteria(GetType(GRKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(GRKind), "ID", MatchType.Exact, JenisService))
                Dim results As ArrayList = gRKindFacade.Retrieve(crit)

                If results.Count > 0 Then
                    objGRKind = CType(results(0), GRKind)
                    nResult = objGRKind.KindCode
                End If
        End Select
        Return nResult
    End Function

    Private Function Validate(ByRef msgErr As String) As Boolean
        Dim jamOps() As String = lblJamOp.Text.Replace("s/d", "|").Split("|")
        Dim maxDay As Integer = CInt(appConfFacade.Retrieve("ServiceBooking.MaxDay").Value)
        Dim minDay As Integer = CInt(appConfFacade.Retrieve("ServiceBooking.MinDay").Value)

        If txtBookFrom.Text = "" Then
            msgErr = "Jam mulai pengerjaan harus diisi."
        ElseIf txtBookTo.Text = "" Then
            msgErr = "Jam akhir pengerjaan harus diisi."
        ElseIf Convert.ToDateTime(txtBookFrom.Text) > Convert.ToDateTime(txtBookTo.Text) Then
            msgErr = "Jam pengerjaan mulai tidak boleh lebih besar dari Jam pengerjaan sampai."
        ElseIf Convert.ToDateTime(txtBookFrom.Text) = Convert.ToDateTime(txtBookTo.Text) Then
            msgErr = "Jam pengerjaan mulai tidak boleh sama dengan dari Jam pengerjaan sampai."
        ElseIf Convert.ToDateTime(txtBookFrom.Text) < Convert.ToDateTime(jamOps(0).Trim) Then
            msgErr = "Jam pengerjaan mulai tidak boleh lebih kecil dari waktu operasional mulai."
        ElseIf Convert.ToDateTime(txtBookTo.Text) > Convert.ToDateTime(jamOps(1).Trim) Then
            msgErr = "Jam pengerjaan sampai tidak boleh lebih besar dari waktu operasional sampai."
        ElseIf CDate(lblTgl.Text) < Now.AddDays(minDay).Date Then
            msgErr = String.Format("Setting jadwal booking minimal {0} hari dari hari ini.", minDay)
        ElseIf CDate(lblTgl.Text) > Now.AddDays(maxDay).Date Then
            msgErr = String.Format("Setting jadwal booking maximal {0} hari dari hari ini.", maxDay)
        ElseIf ddlJnsService.SelectedIndex = 0 Then
            msgErr = "Jenis service harus dipilih."
        Else
            Dim startTime As DateTime = CType(String.Format("{0} {1}", lblTgl.Text, txtBookFrom.Text), DateTime)
            Dim endTime As DateTime = CType(String.Format("{0} {1}", lblTgl.Text, txtBookTo.Text), DateTime)

            Dim isBooked As Boolean = serviceBookingFacade.IsBooked(CInt(Request.QueryString("id").ToString), objDealer.ID, lblKodeStall.Text, startTime, endTime)
            If isBooked Then
                msgErr = String.Format("Terdapat jadwal yang sudah 'Booked' pada jam tersebut untuk stall : {0}. Mohon untuk memilih jadwal yang lain.", lblKodeStall.Text)
            End If

        End If
        Return String.IsNullorEmpty(msgErr)
    End Function

    Private Sub InitDdl(ByVal isMQPExist As Boolean)
        Dim results As ArrayList

        ddlJnsService.Items.Clear()

        crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.StallServiceType"))
        'If Not isMQPExist Then
        '    crit.opAnd(New Criteria(GetType(StandardCode), "ValueCode", MatchType.NotInSet, "('MQP')"))
        'End If

        results = standardCodeFacade.Retrieve(crit)

        With ddlJnsService.Items
            For Each obj As StandardCode In results
                .Add(New ListItem(obj.ValueDesc, obj.ValueId))
            Next
        End With

        ddlJnsService.Items.Insert(0, "Silahkan Pilih")
    End Sub
#End Region
End Class
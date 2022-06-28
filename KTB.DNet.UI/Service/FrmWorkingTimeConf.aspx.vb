Imports System.Collections.Generic
Imports System.Linq
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports System.Text
Imports DayPilot.Utils
Imports DayPilot.Web.Ui
Imports DayPilot.Web.Ui.Events.Bubble
Imports DayPilot.Web.Ui.Events.Common
Imports DayPilot.Web.Ui.Events.Scheduler
Imports DayPilot.Json

Public Class FrmWorkingTimeConf
    Inherits System.Web.UI.Page
    Private m_bLihatPrivilege As Boolean = False
    Private m_bInputPrivilege As Boolean = False
    Private sessHelper As SessionHelper = New SessionHelper
    Private isRemove As Boolean = True
    Private crit As CriteriaComposite
    Private objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private dealerFacade As DealerFacade = New DealerFacade(User)
    Private stallMasterFacade As StallMasterFacade = New StallMasterFacade(User)
    Private stallWorkingTimeFacade As StallWorkingTimeFacade = New StallWorkingTimeFacade(User)
    Private standardCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
    Private periodeFrom As Date
    Private periodeTo As Date

#Region "Event Handler"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            ResetControl()
            CalWork.SelectedDate = DateTime.Today
            InitStallDdl()
            RefreshData(DateTime.Today)
        End If
    End Sub

    Protected Sub DayPilotScheduler1_BeforeEventRender(sender As Object, e As DayPilot.Web.Ui.Events.Scheduler.BeforeEventRenderEventArgs) Handles DayPilotScheduler1.BeforeEventRender
        If Not IsNothing(e.DataItem) Then
            e.DurationBarColor = e.DataItem("color").ToString
            e.BackgroundColor = e.DataItem("color").ToString
        End If
    End Sub

    Protected Sub CalWork_SelectionChanged(sender As Object, e As EventArgs)
        RefreshData(CalWork.SelectedDate)
    End Sub

    Protected Sub CalWork_DayRender(sender As Object, e As DayRenderEventArgs)
        Dim lst As List(Of StallWorkingTime) = CType(sessHelper.GetSession("TimeWork"), List(Of StallWorkingTime))
        Dim isValid As Boolean = lst.Where(Function(x) x.Tanggal = e.Day.Date And x.StallMaster.Status = CInt(EnumStallMaster.StatusStall.Aktif)).Count > 0
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

    Protected Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Dim lstStall As List(Of String) = New List(Of String)
        If Not Validate(lstStall) Then
            Exit Sub
        End If

        If hdConfirm.Value = "-1" And lstStall.Count > 0 Then
            Dim confMsg As String = String.Format("Periode untuk stall {0} tersebut sudah pernah di generate. Anda yakin mau generate ulang?", String.Join(", ", lstStall))
            RegisterStartupScript("Confirm", String.Format("<script>ShowConfirm('{0}', 'btnGenerate');</script>", confMsg))
            Return
        Else
            hdConfirm.Value = "-1"
        End If

        Dim result As Integer = 0
        Dim obj As StallWorkingTime
        Dim lst As ArrayList = New ArrayList
        periodeFrom = CDate(txtTglPeriodeFrom.Text)
        periodeTo = CDate(txtTglPeriodeTo.Text)
        Dim rangeTgl As List(Of Date) =
            Enumerable.Range(0, 1 + periodeTo.Subtract(periodeFrom).Days).
            Select(Function(offset) periodeFrom.AddDays(offset)).
            ToList()

        For Each item As ListItem In stallList.Items
            If item.Selected Then
                crit = New CriteriaComposite(New Criteria(GetType(StallWorkingTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(StallWorkingTime), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
                crit.opAnd(New Criteria(GetType(StallWorkingTime), "StallMaster.ID", MatchType.Exact, CInt(item.Value)))
                crit.opAnd(New Criteria(GetType(StallWorkingTime), "Tanggal", MatchType.GreaterOrEqual, periodeFrom))
                crit.opAnd(New Criteria(GetType(StallWorkingTime), "Tanggal", MatchType.LesserOrEqual, periodeTo))

                Dim lstWorkingTime As List(Of StallWorkingTime) = stallWorkingTimeFacade.Retrieve(crit).Cast(Of StallWorkingTime)().ToList

                crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallWorkingTime.VisitType"))

                Dim lstVisitTypes As List(Of StandardCode) = standardCodeFacade.Retrieve(crit).Cast(Of StandardCode)().ToList

                crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallMaster.TipeStall"))

                Dim lstStallTypes As List(Of StandardCode) = standardCodeFacade.Retrieve(crit).Cast(Of StandardCode)().ToList

                For Each tgl As Date In rangeTgl
                    Dim isValid As Boolean = False
                    For Each chk As ListItem In chkHolidayList.Items
                        If chk.Selected Then
                            Select Case chk.Value
                                Case "1" 'Senin
                                    If tgl.DayOfWeek = DayOfWeek.Monday Then
                                        isValid = True
                                        Exit For
                                    End If
                                Case "2" 'Selasa
                                    If tgl.DayOfWeek = DayOfWeek.Tuesday Then
                                        isValid = True
                                        Exit For
                                    End If
                                Case "3" 'Rabu
                                    If tgl.DayOfWeek = DayOfWeek.Wednesday Then
                                        isValid = True
                                        Exit For
                                    End If
                                Case "4" 'Kamis
                                    If tgl.DayOfWeek = DayOfWeek.Thursday Then
                                        isValid = True
                                        Exit For
                                    End If
                                Case "5" 'Jumat
                                    If tgl.DayOfWeek = DayOfWeek.Friday Then
                                        isValid = True
                                        Exit For
                                    End If
                                Case "6" 'Sabtu
                                    If tgl.DayOfWeek = DayOfWeek.Saturday Then
                                        isValid = True
                                        Exit For
                                    End If
                                Case "7" 'Minggu
                                    If tgl.DayOfWeek = DayOfWeek.Sunday Then
                                        isValid = True
                                        Exit For
                                    End If
                            End Select
                        End If
                    Next

                    If isValid Then
                        If lstWorkingTime.Any(Function(x) x.Tanggal = tgl) Then
                            obj = lstWorkingTime.SingleOrDefault(Function(x) x.Tanggal = tgl)
                        Else
                            obj = New StallWorkingTime
                        End If

                        obj.IsHoliday = rbYes.Checked
                        obj.Dealer = dealerFacade.Retrieve(objDealer.ID)
                        obj.StallMaster = stallMasterFacade.Retrieve(CInt(item.Value))
                        obj.TimeStart = CType(String.Format("{0} {1}", tgl.ToShortDateString, txtOptFrom.Text), DateTime)
                        obj.TimeEnd = CType(String.Format("{0} {1}", tgl.ToShortDateString, txtOptTo.Text), DateTime)
                        obj.RestTimeStart = CType(String.Format("{0} {1}", tgl.ToShortDateString, txtBreakFrom.Text), DateTime)
                        obj.RestTimeEnd = CType(String.Format("{0} {1}", tgl.ToShortDateString, txtBreakTo.Text), DateTime)
                        obj.Tanggal = tgl
                        obj.Notes = String.Empty

                        Dim stallType As Integer = lstStallTypes.SingleOrDefault(Function(x) x.ValueId = obj.StallMaster.StallType).ValueId
                        If stallType = CInt(EnumStallMaster.TipeStall.Booking) Or stallType = CInt(EnumStallMaster.TipeStall.RealTimeService) Then
                            obj.VisitType = CInt(lstVisitTypes.SingleOrDefault(Function(x) x.ValueId = CInt(EnumStallMaster.VisitType.BO)).ValueId)
                        Else
                            obj.VisitType = CInt(lstVisitTypes.SingleOrDefault(Function(x) x.ValueId = CInt(EnumStallMaster.VisitType.WI)).ValueId)
                        End If

                        lst.Add(obj)
                    End If
                Next
            End If
        Next

        If lst.Count = 0 Then
            MessageBox.Show("Tidak ada hari yg dipilih dalam periode yang digenerate.")
        Else
            result = stallWorkingTimeFacade.Insert(lst)
            If result > 0 Then
                ResetControl()
                RefreshData(CalWork.SelectedDate)
                MessageBox.Show("Generate berhasil.")
            Else
                MessageBox.Show("Generate gagal.")
            End If
        End If
    End Sub

    Protected Sub btnTampilkan_Click(sender As Object, e As EventArgs)
        RefreshData(CalWork.SelectedDate)
    End Sub

    Protected Sub CalWork_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs)
        RefreshData(e.NewDate)
    End Sub

    Protected Sub DayPilotScheduler1_PreRender(sender As Object, e As EventArgs)
        RefreshData(CalWork.SelectedDate)
    End Sub
#End Region

#Region "Custom Method"
    Public Sub RefreshData(ByVal tanggal As Date)
        GetAllData(tanggal)
        GetResources(tanggal)
        DayPilotScheduler1.StartDate = tanggal
        DayPilotScheduler1.DataSource = GetData()
        DayPilotScheduler1.DataBind()
    End Sub

    Private Sub InitStallDdl()
        stallList.Items.Clear()

        crit = New CriteriaComposite(New Criteria(GetType(StallMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StallMaster), "Status", MatchType.Exact, CInt(EnumStallMaster.StatusStall.Aktif).ToString))
        crit.opAnd(New Criteria(GetType(StallMaster), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))

        Dim stallMasterFacade As StallMasterFacade = New StallMasterFacade(User)
        Dim results As ArrayList = stallMasterFacade.Retrieve(crit)

        With stallList.Items
            For Each obj As StallMaster In results
                .Add(New ListItem(String.Format("{0} - {1}", obj.StallCodeDealer, obj.StallName), obj.ID))
            Next
        End With
    End Sub

    Private Function GetResources(ByVal tanggal As Date) As List(Of DayPilot.Web.Ui.Resource)
        DayPilotScheduler1.Resources.Clear()
        Dim res As DayPilot.Web.Ui.Resource = New DayPilot.Web.Ui.Resource
        Dim dt As DataTable = stallMasterFacade.GetResourceStallWorkingTime(txtKodeDealer.Text, tanggal)

        For Each item As DataRow In dt.Rows
            res = New DayPilot.Web.Ui.Resource
            res.Name = String.Format("{0} ({1})", item("StallCodeDealer"), item("StallType"))
            res.Value = item("StallMasterID")
            DayPilotScheduler1.Resources.Add(res)
        Next
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

        crit = New CriteriaComposite(New Criteria(GetType(StallWorkingTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "Tanggal", MatchType.Exact, CalWork.SelectedDate))

        Dim lst As ArrayList = stallWorkingTimeFacade.Retrieve(crit)

        For Each item As StallWorkingTime In lst
            crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallMaster.TipeStall"))
            crit.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, item.StallMaster.StallType))

            dr = dt.NewRow()
            dr("id") = item.ID.ToString
            dr("start") = CType(String.Format("{0} {1}", item.Tanggal.ToShortDateString, item.TimeStart.ToString("HH:mm")), DateTime)
            dr("end") = CType(String.Format("{0} {1}", item.Tanggal.ToShortDateString, item.TimeEnd.ToString("HH:mm")), DateTime)
            dr("resource") = item.StallMaster.ID
            dr("color") = GetColorStall(CType(standardCodeFacade.Retrieve(crit)(0), StandardCode).ValueDesc)

            crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallWorkingTime.VisitType"))
            crit.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, item.VisitType))

            If item.IsHoliday = 0 Then
                dr("name") = CType(standardCodeFacade.Retrieve(crit)(0), StandardCode).ValueDesc
            Else
                dr("name") = String.Format("{0} (Libur)", CType(standardCodeFacade.Retrieve(crit)(0), StandardCode).ValueDesc)
            End If

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

    Private Sub GetAllData(ByVal tanggal As Date)
        crit = New CriteriaComposite(New Criteria(GetType(StallWorkingTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "Tanggal", MatchType.GreaterOrEqual, New DateTime(tanggal.Year, tanggal.Month, 1).AddDays(-60)))
        crit.opAnd(New Criteria(GetType(StallWorkingTime), "Tanggal", MatchType.LesserOrEqual, New DateTime(tanggal.Year, tanggal.Month, DateTime.DaysInMonth(tanggal.Year, tanggal.Month)).AddDays(60)))

        Dim lst As List(Of StallWorkingTime) = stallWorkingTimeFacade.Retrieve(crit).Cast(Of StallWorkingTime)().ToList
        sessHelper.SetSession("TimeWork", lst)
    End Sub

    Private Function Validate(ByRef lstStall As List(Of String)) As Boolean
        Dim msgErr As String = String.Empty

        If String.IsNullorEmpty(txtKodeDealer.Text) Then
            msgErr = "Kode dealer harus dipilih."
        ElseIf txtKodeDealer.Text <> objDealer.DealerCode Then
            msgErr = "Hanya kode dealer sesuai user login yang dapat melakukan generate."
        ElseIf String.IsNullorEmpty(txtTglPeriodeFrom.Text) Or String.IsNullorEmpty(txtTglPeriodeTo.Text) Then
            msgErr = "Periode dari atau sampai harus diisi."
        Else
            periodeFrom = CDate(txtTglPeriodeFrom.Text)
            periodeTo = CDate(txtTglPeriodeTo.Text)

            If periodeFrom > periodeTo Then
                msgErr = "Periode dari tidak boleh lebih besar dari periode dari."
            ElseIf periodeFrom < DateTime.Today.Date Then
                msgErr = "Periode dari harus minimal hari ini."
            ElseIf String.IsNullorEmpty(txtOptFrom.Text) Or String.IsNullorEmpty(txtOptTo.Text) Then
                msgErr = "Waktu operasional dari atau sampai harus diisi."
            ElseIf Convert.ToDateTime(txtOptFrom.Text) > Convert.ToDateTime(txtOptTo.Text) Then
                msgErr = "Waktu operasional dari tidak boleh lebih besar dari waktu operasional sampai."
            ElseIf String.IsNullorEmpty(txtBreakFrom.Text) Or String.IsNullorEmpty(txtBreakTo.Text) Then
                msgErr = "Waktu istirahat dari atau sampai harus diisi."
            ElseIf Convert.ToDateTime(txtBreakFrom.Text) > Convert.ToDateTime(txtBreakTo.Text) Then
                msgErr = "Waktu istirahat dari tidak boleh lebih besar dari waktu istirahat sampai."
            ElseIf stallList.GetSelectedIndices.Count = 0 Then
                msgErr = "Stall harus dipilih."
            Else
                For Each item As ListItem In stallList.Items
                    If item.Selected Then
                        crit = New CriteriaComposite(New Criteria(GetType(StallWorkingTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crit.opAnd(New Criteria(GetType(StallWorkingTime), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
                        crit.opAnd(New Criteria(GetType(StallWorkingTime), "StallMaster.ID", MatchType.Exact, CInt(item.Value)))
                        crit.opAnd(New Criteria(GetType(StallWorkingTime), "Tanggal", MatchType.GreaterOrEqual, periodeFrom))
                        crit.opAnd(New Criteria(GetType(StallWorkingTime), "Tanggal", MatchType.LesserOrEqual, periodeTo))

                        Dim lst As List(Of StallWorkingTime) = stallWorkingTimeFacade.Retrieve(crit).Cast(Of StallWorkingTime)().ToList
                        If lst.Count > 0 Then
                            lstStall.Add(item.Text)
                        End If
                    End If
                Next
            End If
        End If

        If Not String.IsNullorEmpty(msgErr) Then
            MessageBox.Show(msgErr)
        End If

        Return String.IsNullorEmpty(msgErr)
    End Function

    Private Sub ResetControl()
        txtKodeDealer.Attributes.Add("readonly", True)
        txtNamaDealer.Attributes.Add("readonly", True)
        txtTglPeriodeFrom.Text = FormatDateTime(DateTime.Now, Microsoft.VisualBasic.DateFormat.ShortDate)
        txtTglPeriodeTo.Text = FormatDateTime(DateTime.Now, Microsoft.VisualBasic.DateFormat.ShortDate)
        txtOptFrom.Text = "08:00"
        txtOptTo.Text = "17:00"
        txtBreakFrom.Text = "12:00"
        txtBreakTo.Text = "13:00"
        rbNo.Checked = True
        stallList.ClearSelection()

        If Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            txtKodeDealer.Text = objDealer.DealerCode
            txtNamaDealer.Text = objDealer.DealerName
        Else
            txtKodeDealer.Text = ""
            txtNamaDealer.Text = ""
        End If
    End Sub

    Private Sub CheckPrivilege()
        m_bLihatPrivilege = SecurityProvider.Authorize(Context.User, SR.WorkingConfigurationTime_View_Privilage)
        m_bInputPrivilege = SecurityProvider.Authorize(Context.User, SR.WorkingConfigurationTime_Input_Privilage)

        If Not m_bLihatPrivilege Or objDealer.IsDealerDMS Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Stall - Setting Kalender Kerja")
        End If

        If Not m_bInputPrivilege Then
            btnGenerate.Visible = False
        End If
    End Sub
#End Region
End Class
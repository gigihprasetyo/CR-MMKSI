#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
Imports OfficeOpenXml
#End Region

Public Class FrmReplacePendaftaran
    Inherits System.Web.UI.Page

    Private ReadOnly Property RegistrationID As String
        Get
            Return Request.QueryString("id")
        End Get
    End Property

    Private ReadOnly Property ClassCode As String
        Get
            Return Request.QueryString("classcode")
        End Get
    End Property

    Private ReadOnly Property DealerCode As String
        Get
            Return Request.QueryString("dealercode")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            InitInformationClass()
            BindDataGrid()
        End If
    End Sub

    Private Sub InitInformationClass()
        Dim func As New TrClassRegistrationFacade(User)
        Dim dataSiswa As TrClassRegistration = func.Retrieve(CInt(Me.RegistrationID))
        Dim data As TrClass = dataSiswa.TrClass
        If Not IsNothing(data) Then
            lblClassCode.Text = data.ClassCode
            lblClassName.Text = data.ClassName
            lblStartDate.Text = data.StartDate.DateToString
            lblFinishDate.Text = data.FinishDate.DateToString
            lblLocation.Text = data.TrMRTC.Name
        End If
        Dim strInfo = "Silakan pilih siswa yang akan menggantikan {0} ({1}) untuk mengikuti kelas ini"
        lblInformation.Text = String.Format(strInfo, dataSiswa.TrTrainee.ID, dataSiswa.TrTrainee.Name)
    End Sub

    Private Sub BindDataGrid()
        Dim data As New List(Of TrBookingCourse)
        data.AddRange(DataTerdaftar)
        data.AddRange(DataMemenuhiSyarat)
        dtgTrainee.DataSource = data
        dtgTrainee.DataBind()
    End Sub

    Private Function DataTerdaftar() As List(Of TrBookingCourse)
        Dim dataRest As New List(Of TrBookingCourse)

        Dim criteria As New CriteriaComposite(New Criteria(GetType(TrBookingClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(TrBookingClass), "TrClassRegistration.ID", MatchType.Exact, Me.RegistrationID))
        criteria.opAnd(New Criteria(GetType(TrBookingClass), "TrClass.ClassCode", MatchType.Exact, Me.ClassCode))
        Dim dtReplace As TrBookingClass = CType(New TrBookingClassFacade(User).Retrieve(criteria)(0), TrBookingClass)
        Dim dtBooking As TrBookingCourse = dtReplace.TrBookingCourse

        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.DealerCode", MatchType.Exact, Me.DealerCode))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrCourse.ID", MatchType.Exact, dtBooking.TrCourse.ID))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, dtBooking.FiscalYear))

        Dim dataBooking As New List(Of TrBookingCourse)
        Dim dtBookings As ArrayList = New TrBookingCourseFacade(User).Retrieve(criterias)
        If dtBookings.IsItems Then
            dataBooking = dtBookings.Cast(Of TrBookingCourse).OrderBy(Function(x) x.PrioritySequence).ToList()
        Else
            Return dataRest
        End If

        Dim dataBooking1 As List(Of TrBookingCourse) = dataBooking.Where(Function(x) x.TrClassRegistration IsNot Nothing).Where(Function(x) _
                                                    x.TrClassRegistration.TrClass.ClassCode <> Me.ClassCode And _
                                                    x.TrClassRegistration.TrClass.StartDate >= Me.DateNow).OrderBy(Function(x) _
                                                    x.PrioritySequence).ToList()
        Dim dataBooking2 As List(Of TrBookingCourse) = dataBooking.Where(Function(x) x.TrClassRegistration Is Nothing).OrderBy(Function(x) _
                                                    x.PrioritySequence).ToList()
        dataRest.AddRange(dataBooking1)
        dataRest.AddRange(dataBooking2)

        Return dataRest
    End Function

    Private Function DataMemenuhiSyarat() As List(Of TrBookingCourse)
        Dim dataClass As TrClass = New TrClassFacade(User).Retrieve(Me.ClassCode)
        Dim criteria As New CriteriaComposite(New Criteria(GetType(JobPositionToCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(JobPositionToCategory), "JobPositionCategory.AreaID", MatchType.Exact, 2))

        Dim jobPositions As List(Of String) = New List(Of String)()

        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrTrainee), "Dealer.DealerCode", MatchType.Exact, Me.DealerCode))
        criterias.opAnd(New Criteria(GetType(TrTrainee), "Status", MatchType.Exact, CType(EnumTrTrainee.TrTraineeStatus.Active, Short)))

        Dim isMandatory As Boolean = False
        Dim dataCourse As TrCourse = dataClass.TrCourse
        If dataCourse.Category.IsMandatory Then
            isMandatory = True
        End If
        If isMandatory Then
            jobPositions = dataCourse.Category.ListOfJobPosition.Cast(Of  _
                    TrCourseCategoryToJobPosition).Select(Function(x) x.JobPosition.ID.ToString()).ToList()
        Else
            jobPositions = New JobPositionToCategoryFacade(User).Retrieve(criteria).Cast(Of  _
            JobPositionToCategory).Select(Function(x) x.JobPosition.ID.ToString()).ToList()
        End If
        If jobPositions.Count > 0 Then
            criterias.opAnd(New Criteria(GetType(TrTrainee), "RefJobPosition.ID", MatchType.InSet, jobPositions.GenerateInSet()))
        End If

        Dim dataSiswa As ArrayList = New TrTraineeFacade(User).Retrieve(criterias)
        Dim dataBelumTerdaftar As New List(Of TrBookingCourse)
        For Each siswa As TrTrainee In dataSiswa
            Dim registrationState As String = New TrBookingCourseFacade(User).GetCourseRegistrationState(siswa.ID, dataCourse.ID, dataClass.FiscalYear, dataClass.StartDate)
            If registrationState.Equals("Belum Terdaftar") Then
                Dim dtDummyBooking As New TrBookingCourse
                dtDummyBooking.TrCourse = dataClass.TrCourse
                dtDummyBooking.TrTrainee = siswa
                dtDummyBooking.ValidateDate = Me.DateNow
                dtDummyBooking.PrioritySequence = 99
                dataBelumTerdaftar.Add(dtDummyBooking)
            End If
        Next
        Return dataBelumTerdaftar
    End Function

    Private Sub dtgTrainee_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgTrainee.ItemDataBound
        If e.IsRowItems Then
            Dim data As TrBookingCourse = e.DataItem(Of TrBookingCourse)()
            Dim rowValue As TrTrainee = data.TrTrainee
            Dim lblNo As Label = e.FindLabel("lblNo")
            Dim lblID As Label = e.FindLabel("lblID")
            Dim lblJobposition As Label = e.FindLabel("lblJobposition")
            Dim lblMulaiBekerja As Label = e.FindLabel("lblMulaiBekerja")
            Dim lblName As Label = e.FindLabel("lblName")
            Dim lblStatus As Label = e.FindLabel("lblStatus")
            Dim hdnIsregister As HiddenField = e.FindHiddenField("hdnIsregister")
            Dim hdnBookingID As HiddenField = e.FindHiddenField("hdnBookingID")

            hdnBookingID.Value = data.ID
            lblNo.Text = e.CreateNumberPage()
            lblID.Text = rowValue.ID
            lblName.Text = rowValue.Name
            lblJobposition.Text = rowValue.RefJobPosition.Description
            lblMulaiBekerja.Text = rowValue.StartWorkingDate.DateToString
            lblStatus.Text = data.GetStatusPendaftaran()
        End If
    End Sub

    Protected Sub rb_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each item As DataGridItem In dtgTrainee.Items
            Dim rb As RadioButton = CType(item.FindControl("rbnChecked"), RadioButton)
            If Not rb.ClientID = sender.ClientID Then
                rb.Checked = False
            Else
                dtgTrainee.SelectedIndex = item.ItemIndex
            End If
        Next
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("FrmListPendaftaranASS.aspx")
    End Sub

    Protected Sub btnDaftar_Click(sender As Object, e As EventArgs) Handles btnDaftar.Click
        Dim trID As Integer = 0
        Dim trBookingID As Integer = 0
        For Each item As DataGridItem In dtgTrainee.Items
            Dim rbn As RadioButton = CType(item.FindControl("rbnChecked"), RadioButton)
            If rbn.Checked Then
                trID = CInt(item.FindLabel("lblID").Text)
                trBookingID = CInt(item.FindHiddenField("hdnBookingID").Value)
            End If
        Next
        If trID.Equals(0) Then
            MessageBox.Show("Silahkan pilih siswa")
            Return
        End If

        Dim funcReg As New TrClassRegistrationFacade(User)
        Dim funcBookCourse As New TrBookingCourseFacade(User)
        Dim funcBookClass As New TrBookingClassFacade(User)

        Dim criteria As New CriteriaComposite(New Criteria(GetType(TrBookingClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(TrBookingClass), "TrClassRegistration.ID", MatchType.Exact, Me.RegistrationID))
        criteria.opAnd(New Criteria(GetType(TrBookingClass), "TrClass.ClassCode", MatchType.Exact, Me.ClassCode))
        Dim dtBookingClass As TrBookingClass = CType(New TrBookingClassFacade(User).Retrieve(criteria)(0), TrBookingClass)
        Dim dtBooking As TrBookingCourse = dtBookingClass.TrBookingCourse
        Dim dtClassReg As TrClassRegistration = dtBookingClass.TrClassRegistration
        Dim dtTrainee As TrTrainee = New TrTraineeFacade(User).Retrieve(trID)
        If trBookingID.Equals(0) Then
            dtBooking.TrTrainee = dtTrainee
            dtBooking.ValidateDate = Me.DateNow
            funcBookCourse.Update(dtBooking)

            dtClassReg.TrTrainee = dtTrainee
            funcReg.Update(dtClassReg)
        Else
            Dim dataBooking As TrBookingCourse = funcBookCourse.Retrieve(trBookingID)
            If IsNothing(dataBooking.TrClassRegistration) Then
                dataBooking.TrTrainee = dtBooking.TrTrainee
                funcBookCourse.Update(dataBooking)

                dtBooking.TrTrainee = dtTrainee
                dtBooking.ValidateDate = Me.DateNow
                funcBookCourse.Update(dtBooking)

                dtClassReg.TrTrainee = dtTrainee
                funcReg.Update(dtClassReg)
            Else

            End If
        End If

        Response.Redirect("FrmListPendaftaranASS.aspx")
    End Sub

End Class
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
#End Region

Public Class FrmCourseRegistrationInput
    Inherits System.Web.UI.Page
    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page)

    Private Sub TitleDescription(ByVal areaid As String)
        If areaid.Equals("1") Then
            lblPageTitle.Text = "Training Sales - Input Pendaftaran"
            hdnCategory.Value = "sales"
        ElseIf areaid.Equals("2") Then
            lblPageTitle.Text = "Training After Sales - Input Pendaftaran"
            hdnCategory.Value = "ass"
        ElseIf areaid.Equals("3") Then
            lblPageTitle.Text = "Training Customer Satisfaction - Input Pendaftaran"
            hdnCategory.Value = "cs"
        Else
            lblPageTitle.Text = "Training - Input Pendaftaran"
            hdnCategory.Value = "ass"
        End If
        If FormReadOnly.Equals("1") Then
            lblPageTitle.Text = lblPageTitle.Text.Replace("Input", "Daftar")
        End If

    End Sub

    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property

    Private ReadOnly Property DealerCode As String
        Get
            Return Request.QueryString("dealercode")
        End Get
    End Property

    Private ReadOnly Property CourseCode As String
        Get
            Return Request.QueryString("coursecode")
        End Get
    End Property

    Private ReadOnly Property FiscalYear As String
        Get
            Return Request.QueryString("fiscalyear")
        End Get
    End Property

    Private ReadOnly Property FormReadOnly As String
        Get
            Return Request.QueryString("readonly")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            TitleDescription(AreaId)
            SetDescription()
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

            If FormReadOnly.Equals("0") Then
                DataTerdaftarBinding(True)
                If helpers.GetSession("idxpage") Is Nothing Then
                    DataGridBinding()
                Else
                    DataGridBinding(helpers.GetSession("idxpage"))
                End If
                Dim course As TrCourse = New TrCourseFacade(User).Retrieve(Me.CourseCode)
                If course.PaymentType = CType(EnumTrCourse.PaymentType.CHARGE, Short) Then
                    lblTotalBayar.Text = String.Format("Total biaya yang harus dibayar pada tahun fiskal {0} yaitu Rp. {1}", Me.FiscalYear, GetTotalbayar(Me.DealerCode, Me.FiscalYear).ToString().AddThousandDelimiter())
                Else
                    trNotif.NonActiveControl()
                End If

            Else
                DataTerdaftarBinding()
                dvBooking.Style.Add("height", "300px")
                trGridHeader.Visible = False
                trBtnTambah.Visible = False
                btnSimpan.Visible = False
                btnValidasi.Visible = False
                btnBatal.Visible = False
            End If
            If Not String.IsNullorEmpty(Request.QueryString("IsSave")) Then
                MessageBox.Show(SR.SaveSuccess)
            End If

        End If
    End Sub

    Private Function GetTotalbayar(ByVal dealerCode As String, ByVal fiscalYear As String, Optional dataBookings As List(Of TrBookingCourse) = Nothing, Optional courseID As Integer = 0) As Double
        Dim rest As Double = 0

        Dim critMRCT As New CriteriaComposite(New Criteria(GetType(TrMRTCDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critMRCT.opAnd(New Criteria(GetType(TrMRTCDealer), "Dealer.DealerCode", MatchType.Exact, dealerCode))
        Dim dataMRCT As List(Of String) = New TrMRTCDealerFacade(User).Retrieve(critMRCT).Cast(Of  _
            TrMRTCDealer).Select(Function(x) x.TrMRTC.ID.ToString()).ToList()


        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.DealerCode", MatchType.Exact, dealerCode))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, fiscalYear))

        Dim dataBooking As List(Of TrBookingCourse) = New TrBookingCourseFacade(User).Retrieve(criterias).Cast(Of  _
           TrBookingCourse).OrderBy(Function(x) x.PrioritySequence).ToList()
        Dim DicData As Dictionary(Of Integer, Decimal) = New Dictionary(Of Integer, Decimal)
        Dim dataCourse As List(Of TrCourse) = dataBooking.Select(Function(x) x.TrCourse).Cast(Of TrCourse).ToList()

        For Each itemCourse As TrCourse In dataCourse.Distinct()
            Dim jumlahBayar As Decimal = 0
            If DicData.Where(Function(x) x.Key = itemCourse.ID).Count > 0 Then
                Continue For
            End If

            Dim crtria As New CriteriaComposite(New Criteria(GetType(TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtria.opAnd(New Criteria(GetType(TrClass), "FiscalYear", MatchType.Exact, fiscalYear))
            crtria.opAnd(New Criteria(GetType(TrClass), "TrMRTC.ID", MatchType.InSet, dataMRCT.GenerateInSet()))
            crtria.opAnd(New Criteria(GetType(TrClass), "TrCourse.ID", MatchType.Exact, itemCourse.ID))

            Dim dataClass As ArrayList = New TrClassFacade(User).Retrieve(crtria)
            If dataClass.Count > 0 Then
                jumlahBayar = CType(dataClass(0), TrClass).PriceTotal
            End If
            DicData.Add(itemCourse.ID, jumlahBayar)
        Next

        For Each itemData As TrBookingCourse In dataBooking
            If courseID.Equals(0) Then
                If itemData.TrCourse.PaymentType = CType(EnumTrCourse.PaymentType.CHARGE, Short) Then
                    rest = rest + CType(DicData.FirstOrDefault(Function(x) x.Key = itemData.TrCourse.ID).Value, Double)
                End If
            Else
                If itemData.TrCourse.PaymentType = CType(EnumTrCourse.PaymentType.CHARGE, Short) And itemData.TrCourse.ID <> courseID Then
                    rest = rest + CType(DicData.FirstOrDefault(Function(x) x.Key = itemData.TrCourse.ID).Value, Double)
                End If
            End If
        Next
        If dataBookings IsNot Nothing Then
            For Each itemData As TrBookingCourse In dataBookings
                If itemData.TrCourse.PaymentType = CType(EnumTrCourse.PaymentType.CHARGE, Short) Then
                    rest = rest + CType(DicData.FirstOrDefault(Function(x) x.Key = itemData.TrCourse.ID).Value, Double)
                End If
            Next
        End If


        Return rest
    End Function

    Private Sub SetDescription()
        Dim course As TrCourse = New TrCourseFacade(User).Retrieve(Me.CourseCode)
        lblKodeTraining.Text = course.CourseCode & " - " & course.CourseName
        lbltahunFiscal.Text = Me.FiscalYear
        lblKategori.Text = course.JobPositionCategory.Description
        lblFreePass.Text = GetFreePass()
    End Sub

    Private Function GetFreePass() As Integer
        Dim result As Integer = 0
        Dim criteria As New CriteriaComposite(New Criteria(GetType(TrFreePass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(TrFreePass), "Dealer.DealerCode", MatchType.Exact, Me.DealerCode))
        criteria.opAnd(New Criteria(GetType(TrFreePass), "FiscalYear", MatchType.Exact, Me.FiscalYear))

        Dim arlResult As ArrayList = New TrFreePassFacade(User).Retrieve(criteria)

        If arlResult.Count > 0 Then
            Dim freePassData As TrFreePass = CType(arlResult(0), TrFreePass)
            result = freePassData.Qty - freePassData.QtyUsed
        End If

        Return result
    End Function


    Private Sub DataGridBinding(Optional ByVal index As Integer = 0, Optional ByVal data As TrTrainee = Nothing)
        Dim totalRow As Integer = 0
        Dim criteria As New CriteriaComposite(New Criteria(GetType(JobPositionToCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(JobPositionToCategory), "JobPositionCategory.AreaID", MatchType.Exact, Me.AreaId))

        Dim jobPositions As List(Of String) = New List(Of String)()

        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrTrainee), "Dealer.DealerCode", MatchType.Exact, Me.DealerCode))
        criterias.opAnd(New Criteria(GetType(TrTrainee), "Status", MatchType.Exact, CType(EnumTrTrainee.TrTraineeStatus.Active, Short)))

        Dim isMandatory As Boolean = False
        Dim dataCourse As TrCourse = New TrCourse()
        If Not String.IsNullorEmpty(CourseCode) Then
            dataCourse = New TrCourseFacade(User).Retrieve(CourseCode)
            If dataCourse.Category.IsMandatory Then
                isMandatory = True
            End If
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
        Dim dataBelumTerdaftar As New List(Of TrTrainee)
        For Each siswa As TrTrainee In dataSiswa
            Dim registrationState As String = New TrBookingCourseFacade(User).GetCourseRegistrationState(siswa.ID, dataCourse.ID, Me.FiscalYear, Me.DateNow)
            If registrationState.Equals("Belum Terdaftar") Then
                dataBelumTerdaftar.Add(siswa)
            End If
        Next
        If Not IsNothing(data) Then
            If dataBelumTerdaftar.Where(Function(x) x.ID = data.ID).Count = 0 Then
                dataBelumTerdaftar.Add(data)
            End If
        End If

        dtgHeader.DataSource = dataBelumTerdaftar
        dtgHeader.DataBind()
        helpers.SetSession("idxpage", dtgHeader.CurrentPageIndex)
    End Sub

    Private Sub DataTerdaftarBinding(Optional ByVal isValidate As Boolean = False)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.DealerCode", MatchType.Exact, Me.DealerCode))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrCourse.CourseCode", MatchType.Exact, Me.CourseCode))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, Me.FiscalYear))

        Dim dataBooking As New List(Of TrBookingCourse)
        Dim dtBooking As ArrayList = New TrBookingCourseFacade(User).Retrieve(criterias)
        If dtBooking.IsItems Then
            dataBooking = dtBooking.Cast(Of TrBookingCourse).OrderBy(Function(x) x.PrioritySequence).ToList()
        End If
        helpers.SetSession("CountTerdaftar", dataBooking.Count)

        Dim dataBooking1 As List(Of TrBookingCourse) = dataBooking.Where(Function(x) x.TrClassRegistration IsNot Nothing).OrderBy(Function(x) _
                                                    x.PrioritySequence).ToList()
        Dim dataBooking2 As List(Of TrBookingCourse) = dataBooking.Where(Function(x) x.TrClassRegistration Is Nothing).OrderBy(Function(x) _
                                                    x.PrioritySequence).ToList()
        dataBooking.Clear()
        dataBooking.AddRange(dataBooking1)
        dataBooking.AddRange(dataBooking2)

        If isValidate Then
            If dataBooking.Count.Equals(0) Then
                btnBatal.Enabled = False
            ElseIf dataBooking2.Count.Equals(0) Then
                btnValidasi.Enabled = False
                btnAdd.Enabled = False
            Else
                Dim isNotValidate As Boolean = False
                For Each item As TrBookingCourse In dataBooking2
                    If item.ValidateDate.Year < 1900 Then
                        isNotValidate = True
                    End If
                Next
                If isNotValidate Then
                    btnBatal.Enabled = False
                Else
                    btnValidasi.Enabled = False
                    btnAdd.Enabled = False
                End If

            End If
        End If

        dtgBooking.DataSource = dataBooking
        dtgBooking.DataBind()
    End Sub

    Private Sub dtgHeader_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgHeader.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim data As TrTrainee = CType(e.Item.DataItem, TrTrainee)
            Dim chkItemChecked As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
            Dim lblNamaSiswa As Label = CType(e.Item.FindControl("lblNamaSiswa"), Label)
            Dim lblMulaiKerja As Label = CType(e.Item.FindControl("lblMulaiKerja"), Label)
            Dim lblPosisi As Label = CType(e.Item.FindControl("lblPosisi"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblNoKtp As Label = CType(e.Item.FindControl("lblNoKtp"), Label)

            lblNo.Text = e.CreateNumberPage()
            lblNoReg.Text = data.ID
            lblNamaSiswa.Text = data.Name
            lblMulaiKerja.Text = data.StartWorkingDate.DateToString()
            lblPosisi.Text = data.RefJobPosition.Description
            lblNoKtp.Text = data.NoKTP
            Dim dataCourse As TrCourse = New TrCourseFacade(User).Retrieve(CourseCode)
            Dim registrationState As String = New TrBookingCourseFacade(User).GetCourseRegistrationState(data.ID, dataCourse.ID, Me.FiscalYear, Me.DateNow)
            lblStatus.Text = registrationState
            Dim listBooking As List(Of TrBookingCourse) = GetDataBooking()

            If Not registrationState.Equals("Belum Terdaftar") Then
                If registrationState.Equals("Terdaftar") Then
                    If listBooking.Where(Function(x) x.TrTrainee.ID = data.ID).Count = 0 Then
                        lblStatus.Text = "Belum Terdaftar"
                    Else
                        e.Item.Visible = False
                    End If
                Else
                    e.Item.Visible = False
                End If

            End If
            If listBooking.Where(Function(x) x.TrTrainee.ID = data.ID).Count > 0 Then
                If registrationState.Equals("Belum Terdaftar") Then
                    lblStatus.Text = "Draft"
                End If
                chkItemChecked.Visible = False
            End If
        End If
    End Sub

    Private Function IsPassCourse(ByVal trId As Integer, ByVal courseCode As String) As Boolean
        Dim result As Boolean = False
        Dim crtria As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtria.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.ID", MatchType.Exact, trId))
        crtria.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.TrCourse.CourseCode", MatchType.Exact, courseCode))
        crtria.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Pass, Short)))

        If New TrClassRegistrationFacade(User).Retrieve(crtria).Count > 0 Then
            result = True
        End If
        Return result
    End Function

    Private Sub dtgHeader_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgHeader.PageIndexChanged
        dtgHeader.SelectedIndex = -1
        dtgHeader.CurrentPageIndex = e.NewPageIndex
        DataGridBinding(dtgHeader.CurrentPageIndex)
    End Sub

    Private Sub dtgHeader_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgHeader.SortCommand
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
        dtgHeader.SelectedIndex = -1
        dtgHeader.CurrentPageIndex = 0
        DataGridBinding(dtgHeader.CurrentPageIndex)
    End Sub

    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect(String.Format("FrmCourseRegistration.aspx?area={0}", AreaId))
    End Sub

    Private Function Simpan() As List(Of TrBookingCourse)
        Dim listBooking As List(Of TrBookingCourse) = GetDataBooking()
        If listBooking.Count.Equals(0) Then
            MessageBox.Show("Tambahkan data terlebih dahulu")
            Exit Function
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.DealerCode", MatchType.Exact, Me.DealerCode))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrCourse.CourseCode", MatchType.Exact, Me.CourseCode))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, Me.FiscalYear))

        Dim func As TrBookingCourseFacade = New TrBookingCourseFacade(User)
        Dim dataBooking As ArrayList = New TrBookingCourseFacade(User).Retrieve(criterias)
        For Each dataExist As TrBookingCourse In dataBooking
            If listBooking.Where(Function(x) x.ID = dataExist.ID).Count = 0 Then
                dataExist.RowStatus = CType(DBRowStatus.Deleted, Short)
                func.Update(dataExist)
            End If
        Next

        For Each item As TrBookingCourse In listBooking
            If item.ID.Equals(0) Then
                item.ID = func.Insert(item)
            Else
                func.Update(item)
            End If
        Next
        Return listBooking
    End Function

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If dtgBooking.Items.Count.Equals(0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.DealerCode", MatchType.Exact, Me.DealerCode))
            criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrCourse.CourseCode", MatchType.Exact, Me.CourseCode))
            criterias.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, Me.FiscalYear))

            Dim func As TrBookingCourseFacade = New TrBookingCourseFacade(User)
            Dim dataBooking As ArrayList = New TrBookingCourseFacade(User).Retrieve(criterias)
            If dataBooking.IsItems Then
                For Each dataExist As TrBookingCourse In dataBooking
                    dataExist.RowStatus = CType(DBRowStatus.Deleted, Short)
                    func.Update(dataExist)
                    Me.Page.Response.Redirect(Page.Request.Url.ToString() + "&IsSave=1", True)

                Next
            Else
                MessageBox.Show("Tidak ada siswa yang didaftarkan")
            End If
            Return
        End If

        Dim listBooking As List(Of TrBookingCourse) = Simpan()
        helpers.SetSession("CountTerdaftar", listBooking.Count)

        Me.Page.Response.Redirect(Page.Request.Url.ToString() + "&IsSave=1", True)

    End Sub

    Private Sub dtgBooking_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgBooking.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "delete"
                Dim listData As List(Of TrBookingCourse) = GetDataBooking()
                Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                Dim dataCourse As TrCourse = New TrCourseFacade(User).Retrieve(Me.CourseCode)
                Dim dataRemove As TrBookingCourse = listData.FirstOrDefault(Function(x) x.TrTrainee.ID = CInt(lblNoReg.Text))
                listData.Remove(dataRemove)
                helpers.SetSession("CountTerdaftar", listData.Count)

                lblTotalBayar.Text = String.Format("Total biaya yang harus dibayar pada tahun fiskal {0} yaitu Rp. {1}", _
                            Me.FiscalYear, GetTotalbayar(Me.DealerCode, Me.FiscalYear, listData, dataCourse.ID).ToString().AddThousandDelimiter())

                dtgBooking.DataSource = listData
                dtgBooking.DataBind()
                DataGridBinding(dtgHeader.CurrentPageIndex, dataRemove.TrTrainee)

            Case "uplist"
                Dim listData As List(Of TrBookingCourse) = GetDataBooking()
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
                Dim dataRemove As TrBookingCourse = listData.FirstOrDefault(Function(x) x.TrTrainee.ID = CInt(lblNoReg.Text))
                listData.Remove(dataRemove)

                Dim noBefore As Integer = CInt(lblNo.Text) - 1
                dataRemove.PrioritySequence = noBefore
                listData.FirstOrDefault(Function(x) x.PrioritySequence = noBefore).PrioritySequence = CInt(lblNo.Text)

                listData.Add(dataRemove)
                helpers.SetSession("CountTerdaftar", listData.Count)
                dtgBooking.DataSource = listData.OrderBy(Function(x) x.PrioritySequence).ToList()
                dtgBooking.DataBind()
            Case "downlist"
                Dim listData As List(Of TrBookingCourse) = GetDataBooking()
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
                Dim dataRemove As TrBookingCourse = listData.FirstOrDefault(Function(x) x.TrTrainee.ID = CInt(lblNoReg.Text))
                listData.Remove(dataRemove)

                Dim noAfter As Integer = CInt(lblNo.Text) + 1
                dataRemove.PrioritySequence = noAfter
                listData.FirstOrDefault(Function(x) x.PrioritySequence = noAfter).PrioritySequence = CInt(lblNo.Text)

                listData.Add(dataRemove)
                helpers.SetSession("CountTerdaftar", listData.Count)
                dtgBooking.DataSource = listData.OrderBy(Function(x) x.PrioritySequence).ToList()
                dtgBooking.DataBind()
        End Select

    End Sub

    Private Function GetDataBooking() As List(Of TrBookingCourse)
        Dim result As List(Of TrBookingCourse) = New List(Of TrBookingCourse)
        For Each item As DataGridItem In dtgBooking.Items
            Dim lblNoReg As Label = CType(item.FindControl("lblNoReg"), Label)
            Dim lblValidasi As Label = CType(item.FindControl("lblValidasi"), Label)
            Dim lblNo As Label = CType(item.FindControl("lblNo"), Label)
            Dim hdnClass As HiddenField = CType(item.FindControl("hdnClass"), HiddenField)
            Dim hdnIDBooking As HiddenField = CType(item.FindControl("hdnIDBooking"), HiddenField)
            Dim data As TrBookingCourse = New TrBookingCourse()
            data.ID = CInt(hdnIDBooking.Value)
            data.TrTrainee = New TrTraineeFacade(User).Retrieve(CInt(lblNoReg.Text))
            data.TrCourse = New TrCourseFacade(User).Retrieve(Me.CourseCode)
            data.Dealer = New DealerFacade(User).Retrieve(Me.DealerCode)
            data.FiscalYear = Me.FiscalYear
            If Not String.IsNullOrEmpty(lblValidasi.Text) Then
                data.ValidateDate = lblValidasi.Text.StringUIToDateTime()
            End If
            data.PrioritySequence = CInt(lblNo.Text)
            If Not String.IsNullOrEmpty(hdnClass.Value) Then
                data.TrClassRegistration = New TrClassRegistrationFacade(User).Retrieve(CInt(hdnClass.Value))
            End If
            result.Add(data)
        Next
        Return result
    End Function

    Private Sub dtgBooking_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgBooking.ItemDataBound
        If FormReadOnly.Equals("1") Then
            e.Item.Cells(0).Visible = False
            e.Item.Cells(7).Visible = False
        End If

        If e.Item.DataItem IsNot Nothing Then
            Dim data As TrBookingCourse = CType(e.Item.DataItem, TrBookingCourse)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
            Dim lblNamaSiswa As Label = CType(e.Item.FindControl("lblNamaSiswa"), Label)
            Dim lblMulaiKerja As Label = CType(e.Item.FindControl("lblMulaiKerja"), Label)
            Dim lblPosisi As Label = CType(e.Item.FindControl("lblPosisi"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblValidasi As Label = CType(e.Item.FindControl("lblValidasi"), Label)
            Dim lbtnUp As LinkButton = CType(e.Item.FindControl("lbtnUp"), LinkButton)
            Dim lbtnDown As LinkButton = CType(e.Item.FindControl("lbtnDown"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim hdnClass As HiddenField = CType(e.Item.FindControl("hdnClass"), HiddenField)
            Dim hdnIDBooking As HiddenField = CType(e.Item.FindControl("hdnIDBooking"), HiddenField)
            Dim hClassCode As HyperLink = CType(e.Item.FindControl("hKodeKelas"), HyperLink)

            lblNo.Text = e.CreateNumberPage()
            lblNoReg.Text = data.TrTrainee.ID
            lblNamaSiswa.Text = data.TrTrainee.Name
            lblMulaiKerja.Text = data.TrTrainee.StartWorkingDate.DateToString()
            If data.ValidateDate.IsValid Then
                lblValidasi.Text = data.ValidateDate.DateToString()
            End If
            lblPosisi.Text = data.TrTrainee.RefJobPosition.Description
            hdnIDBooking.Value = data.ID

            If e.Item.ItemIndex.Equals(0) Then
                lbtnUp.Visible = False
            Else
                Dim dataItem As DataGridItem = dtgBooking.Items(e.Item.ItemIndex - 1)
                Dim hdnClassID As HiddenField = CType(dataItem.FindControl("hdnClass"), HiddenField)
                If Not String.IsNullOrEmpty(hdnClassID.Value) Then
                    lbtnUp.Visible = False
                End If
            End If

            If e.Item.ItemIndex.Equals(CInt(helpers.GetSession("CountTerdaftar")) - 1) Then
                lbtnDown.Visible = False
            End If

            If data.ID.Equals(0) Then
                lblStatus.Text = "Draft"
            Else
                If data.TrClassRegistration IsNot Nothing Then
                    If Not FormReadOnly.Equals("1") Then
                        e.Item.BackColor = Color.LightSalmon
                    End If

                    hdnClass.Value = data.TrClassRegistration.ID
                    lbtnUp.Visible = False
                    lbtnDown.Visible = False
                    lbtnDelete.Visible = False
                    If data.TrCourse.PaymentType = CType(EnumTrCourse.PaymentType.CHARGE, Short) Then
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(TrBillingDetail), "TrBookingCourse.ID", MatchType.Exact, data.ID))

                        Dim arrDataTagihan As ArrayList = New TrBillingDetailFacade(User).Retrieve(criterias)
                        If arrDataTagihan.Count > 0 Then
                            If CType(arrDataTagihan(0), TrBillingDetail).TrBillingHeader.Status = CType(EnumTagihanTraining.TagihanStatus.Selesai, Short) Then
                                lblStatus.Text = "Terdaftar - "
                            Else
                                lblStatus.Text = "Belum Lunas - "
                            End If
                        Else
                            lblStatus.Text = "Belum Lunas - "
                        End If
                    Else
                        lblStatus.Text = "Terdaftar - "
                    End If

                    Dim classCode As String = data.TrClassRegistration.TrClass.ClassCode
                    Dim actionValue As String = "popUpClassInformation('" + classCode + "');"
                    hClassCode.Text = classCode
                    hClassCode.NavigateUrl = "javascript:" + actionValue
                Else
                    hClassCode.Visible = False
                    If String.IsNullorEmpty(lblValidasi.Text) Then
                        lblStatus.Text = "Draft"
                    Else
                        lblStatus.Text = "Diajukan"
                    End If
                End If
            End If

            If Not btnValidasi.Enabled Or Me.FormReadOnly.Equals("1") Then
                lbtnDelete.Visible = False
            End If

        End If
    End Sub

    Protected Sub BtnTambah_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If Not isValidAdd.Value.IsNullorEmpty() Then
            isValidAdd.Value = String.Empty
            Exit Sub
        End If

        Dim listBooking As List(Of TrBookingCourse) = New List(Of TrBookingCourse)
        Dim listData As List(Of TrBookingCourse) = GetDataBooking()
        Dim dataCourse As TrCourse = New TrCourseFacade(User).Retrieve(Me.CourseCode)
        Dim dataDealer As Dealer = New DealerFacade(User).Retrieve(Me.DealerCode)
        Dim isReady As Boolean = False
        For Each itemData As DataGridItem In dtgHeader.Items
            Dim chkItemChecked As CheckBox = CType(itemData.FindControl("chkItemChecked"), CheckBox)
            Dim lblNoReg As Label = CType(itemData.FindControl("lblNoReg"), Label)

            If chkItemChecked.Checked Then
                Dim dataItem As TrBookingCourse = New TrBookingCourse()
                dataItem.TrTrainee = New TrTraineeFacade(User).Retrieve(CInt(lblNoReg.Text))
                dataItem.TrCourse = dataCourse
                dataItem.Dealer = dataDealer
                dataItem.FiscalYear = Me.FiscalYear
                dataItem.RegistrationDate = DateTime.Now
                chkItemChecked.Checked = False

                If listData.Where(Function(x) x.TrTrainee.ID = dataItem.TrTrainee.ID).Count = 0 Then
                    If String.IsNullorEmpty(dataItem.TrTrainee.NoKTP) Then
                        MessageBox.Show(String.Format("Peserta {0} dengan No Registrasi {1} tidak memiliki data KTP", dataItem.TrTrainee.Name, dataItem.TrTrainee.ID))
                    Else
                        listBooking.Add(dataItem)
                    End If
                End If
                isReady = True
            End If
        Next

        If Not isReady Then
            MessageBox.Show("Silahkan Pilih Siswa")
            Exit Sub
        End If

        If listBooking.IsItems Then
            listData.AddRange(listBooking)
        End If
        lblTotalBayar.Text = String.Format("Total biaya yang harus dibayar pada tahun fiskal {0} yaitu Rp. {1}", _
                             Me.FiscalYear, GetTotalbayar(Me.DealerCode, Me.FiscalYear, listData, dataCourse.ID).ToString().AddThousandDelimiter())

        helpers.SetSession("CountTerdaftar", listData.Count)
        dtgBooking.DataSource = listData
        dtgBooking.DataBind()
        DataGridBinding(dtgHeader.CurrentPageIndex)
        MessageBox.Show("Berhasil ditambahkan")
    End Sub

    Protected Sub btnValidasi_Click(sender As Object, e As EventArgs) Handles btnValidasi.Click
        Try
            Dim func As TrBookingCourseFacade = New TrBookingCourseFacade(User)
            Dim listBooking As List(Of TrBookingCourse) = Simpan()
            If Not listBooking.IsItems Then
                MessageBox.Show("Tidak ada data siswa, mohon tambahkan terlebih dahulu")
            End If

            For Each itemBooking As TrBookingCourse In listBooking
                If itemBooking.TrClassRegistration Is Nothing Then
                    itemBooking.ValidateDate = DateTime.Now
                    func.Update(itemBooking)
                End If
            Next
            btnAdd.Enabled = False
            btnBatal.Enabled = True
            btnValidasi.Enabled = False

            DataTerdaftarBinding()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Try
            Dim func As TrBookingCourseFacade = New TrBookingCourseFacade(User)
            Dim listBooking As List(Of TrBookingCourse) = Simpan()
            For Each itemBooking As TrBookingCourse In listBooking
                If itemBooking.TrClassRegistration Is Nothing Then
                    itemBooking.ValidateDate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
                    func.Update(itemBooking)
                End If
            Next
            btnAdd.Enabled = True
            btnBatal.Enabled = False
            btnValidasi.Enabled = True

            DataTerdaftarBinding()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

End Class
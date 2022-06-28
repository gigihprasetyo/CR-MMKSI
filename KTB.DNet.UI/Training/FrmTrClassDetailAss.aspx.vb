﻿#Region "Custom Namespace Imports"
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
Imports GlobalExtensions
#End Region

Public Class FrmTrClassDetailAss
    Inherits System.Web.UI.Page

    Dim sHClass As SessionHelper = New SessionHelper
    Dim objClass As TrClass
    Dim helpers As TrainingHelpers = New TrainingHelpers(Me.Page)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'clearData()
        getDataClass()

        If Not IsPostBack Then
            BindddlClass()
            BindDDLTahunFiskal()
            BindDdlProvince()
            BindDDLTipeClass()
            LoadData()
        End If
        lblPopUpCourse.Attributes("onclick") = "ShowPPCourseSelection();"
        lblPopupClass.Attributes("onclick") = "ShowPPClassReference();"
    End Sub
   
    Public Sub clearData()
        txtKodeKategori.Text = String.Empty
        txtKapasitas.Text = String.Empty
        txtKeterangan.Text = String.Empty
        txtKodeKategori.Text = String.Empty
        txtKodeKelas.Text = String.Empty
        txtLokasi.Text = String.Empty
        txtPenginapan.Text = String.Empty
        txtLocationName.Text = String.Empty
        txtNamaKelas.Text = String.Empty
        txtPengajar1.Text = String.Empty
        txtPengajar2.Text = String.Empty
        txtPengajar3.Text = String.Empty

    End Sub
    Private Sub BindddlClass()
        ddlStatus.Items.Clear()
        Dim obj As New EnumTrDataStatus
        Dim arlStatusReg As ArrayList = obj.Retrieve()
        For Each en As EnumDataStatus In arlStatusReg
            Dim lItem As ListItem = New ListItem(en.NameType, en.ValueType.ToString())
            ddlStatus.Items.Add(lItem)
        Next
    End Sub
    Public Sub getDataClass()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not sHClass.GetSession("editID") Is Nothing Then
            objClass = New TrClassFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(sHClass.GetSession("editID"))
            RequiredFieldValidator1.Visible = False
            btnBatal.Visible = True
            btnSimpan.Visible = True

        End If

        If Not sHClass.GetSession("viewID") Is Nothing Then
            objClass = New TrClassFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(sHClass.GetSession("viewID"))
            RequiredFieldValidator1.Visible = False
        End If

        If (Not objClass Is Nothing) Then
            sHClass.SetSession("SESSION_CLASSID", objClass.ID)
        End If

    End Sub
    Public Sub visibleLblTxt()
        hideText()
    End Sub
    Public Sub visibleClassLbl()
        txtKodeKelas.Visible = False
        txtNamaKelas.Visible = False
        txtLokasi.Visible = False
        txtLocationName.Visible = False
        txtPenginapan.Visible = False
        txtKapasitas.Visible = False
        txtPengajar1.Visible = False
        txtPengajar2.Visible = False
        txtPengajar3.Visible = False
        txtKodeKategori.Visible = False
        txtKeterangan.Visible = False
        ddlStatus.Visible = False
        ICTanggalMulai.Visible = False
        ICTanggalSelesai.Visible = False
        btnBatal.Visible = False
        btnSimpan.Visible = False
        lblPopUpCourse.Visible = False
    End Sub
    Public Sub hideText()
        txtKodeKelas.ReadOnly = True
        txtNamaKelas.ReadOnly = True
        txtLokasi.ReadOnly = True
        txtLocationName.ReadOnly = True
        txtPenginapan.ReadOnly = True
        txtKapasitas.ReadOnly = True
        txtPengajar1.ReadOnly = True
        txtPengajar2.ReadOnly = True
        txtPengajar3.ReadOnly = True
        txtKodeKategori.ReadOnly = True
        txtKeterangan.ReadOnly = True
        btnBatal.Visible = False
        btnSimpan.Visible = False
        lblPopUpCourse.Visible = False
    End Sub
    Private Sub LoadData()
        viewEditClass()
    End Sub
    Public Sub viewEditClass()
        If Not IsNothing(objClass) Then
            txtKodeKelas.Text = objClass.ClassCode.Trim()
            txtNamaKelas.Text = objClass.ClassName.Trim()
            txtLokasi.Text = objClass.Location.Trim()
            txtLocationName.Text = objClass.LocationName.Trim()
            txtPenginapan.Text = objClass.Lodging.Trim()
            txtKapasitas.Text = objClass.Capacity.ToString()
            txtPengajar1.Text = objClass.Trainer1.Trim()
            txtPengajar2.Text = objClass.Trainer2.Trim()
            txtPengajar3.Text = objClass.Trainer3.Trim()
            txtKodeKategori.Text = objClass.TrCourse.CourseCode.Trim()
            txtKeterangan.Text = objClass.Description.Trim()
            ICTanggalMulai.Value = objClass.StartDate.ToString("dd/MM/yyyy").Trim()
            ICTanggalSelesai.Value = objClass.FinishDate.ToString("dd/MM/yyyy").Trim()

            Try
                If Not String.IsNullOrEmpty(objClass.TrMRTC.Code) Then
                    txtMRTCID.Text = objClass.TrMRTC.Code
                End If
            Catch ex As Exception
                txtMRTCID.Text = String.Empty
            End Try

            If objClass.TrCourse.PaymentType = EnumTrCourse.PaymentType.CHARGE Then
                divPayment.Visible = True
                lblPricePerDay.Text = objClass.PricePerDay.AddThousandDelimiter()
                txtPaidDay.Text = objClass.PaidDay
                lblTotalPrice.Text = objClass.PriceTotal.AddThousandDelimiter()
            Else
                divPayment.Visible = False
            End If

            hdnFilePath.Value = objClass.FilePath
            hdnFilePath2.Value = objClass.FileCertificatePath
            If Not String.IsNullOrEmpty(objClass.ClassType) Then
                ddlTipeKelas.SelectedValue = objClass.ClassType
            End If
            If objClass.City IsNot Nothing Then
                ddlPropinsi.Items.FindByValue(objClass.City.Province.ID).Selected = True
                BindDdlCity(objClass.City.Province.ID.ToString())
                ddlKota.ClearSelection()
                ddlKota.Items.FindByValue(objClass.City.ID.ToString()).Selected = True
            End If
            txtLokasi.Text = objClass.Location
            txtLocationName.Text = objClass.LocationName
            If ddlTahunFiscal.Items.FindByValue(objClass.FiscalYear) Is Nothing Then
                ddlTahunFiscal.ClearSelection()
                ddlTahunFiscal.Items.Add(New ListItem("objClass.FiscalYear", "objClass.FiscalYear"))
                ddlTahunFiscal.SelectedValue = objClass.FiscalYear
            Else
                ddlTahunFiscal.ClearSelection()
                ddlTahunFiscal.SelectedValue = objClass.FiscalYear
            End If
            ddlTahunFiscal.Enabled = False

            If Not sHClass.GetSession("viewID") Is Nothing Then
                hideText()
                ddlStatus.Visible = False
                lblStatus.Visible = True
                btnBatal.Visible = False
                btnSimpan.Visible = False
                lblPopUpCourse.Visible = False
                Dim obj As New EnumTrDataStatus
                If objClass.Status.ToString() = "0" Then
                    lblStatus.Text = obj.StatusByIndex(CInt(objClass.Status))
                Else
                    lblStatus.Text = obj.StatusByIndex(CInt(objClass.Status))
                End If
                helpers.ModeReadOnly()
                trRefClas.Visible = False
            Else
                ddlStatus.Visible = True
                lblStatus.Visible = False
                btnBatal.Visible = True
                btnSimpan.Visible = True
                lblPopUpCourse.Visible = True
                If objClass.Status.ToString() = "0" Then
                    ddlStatus.SelectedValue = CType(EnumTrDataStatus.DataStatusType.Deactive, Short)
                Else
                    ddlStatus.SelectedValue = CType(EnumTrDataStatus.DataStatusType.Active, Short)
                End If
                txtKodeKategori.ReadOnly = True
                lblPopUpCourse.Visible = False
            End If
            If Not String.IsNullOrEmpty(objClass.FilePath) Then
                trDownload.Visible = True
                trUpload.Visible = False
                lbtnDownload.Visible = True
            End If
            If Not String.IsNullorEmpty(objClass.FileCertificatePath) Then
                trDownloadCer.Visible = True
                trUploadCer.Visible = False
                lbtnDownloadCer.Visible = True
            End If
        Else
            trDownload.Visible = False
            trDownloadCer.Visible = False
        End If
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        If Not IsUnhack() Then
            MessageBox.Show("< dan > bukan karakter valid")
            Return
        End If
       
        If ICTanggalSelesai.Value.Subtract(ICTanggalMulai.Value).Days < 0 Then
            MessageBox.Show("Tanggal mulai tidak boleh lebih besar daripada tanggal selesai")
            Return
        End If

        If ddlStatus.SelectedIndex = 0 Then 'tidak aktif
            If IsHaveTraineeInClass() Then
                MessageBox.Show("Tidak bisa menonaktifkan kelas ini karena memiliki siswa")
                Return
            End If
        End If

        Dim tahun1 As Integer = CInt(ddlTahunFiscal.SelectedValue.Split("/")(0))
        Dim tahun2 As Integer = CInt(ddlTahunFiscal.SelectedValue.Split("/")(1))
        Dim fiskalYearStart As Date = New Date(tahun1, 4, 1)
        Dim fiskalYearEnd As Date = New Date(tahun2, 3, 31)
        If ICTanggalMulai.Value < fiskalYearStart Or ICTanggalMulai.Value > fiskalYearEnd Then
            Dim strMEssage As String = String.Format("Untuk tahun fiskal {0}. Tanggal mulai tidak boleh lebih kecil dari {1} atau lebih besar dari {2}.", _
                                                     ddlTahunFiscal.SelectedValue, _
                                                     fiskalYearStart.ToString("dd/MM/yyyy"), _
                                                     fiskalYearEnd.ToString("dd/MM/yyyy"))
            MessageBox.Show(strMEssage)
            Return
        End If
        save()
    End Sub
    Private Sub save()
        Dim objTrClassFacade As New TrClassFacade(User)
        Dim nResult = -1

        If sHClass.GetSession("editID") Is Nothing Then
            If ICTanggalMulai.Value <= DateTime.Now Then
                MessageBox.Show("Tanggal mulai tidak boleh lebih kecil atau sama dengan hari ini")
                Return
            End If
            If objTrClassFacade.ValidateCode(Me.txtKodeKelas.Text.Trim) = 0 Then
                insertTrClass(objClass, nResult)
                sHClass.SetSession("SESSION_CLASSID", nResult)
            Else
                MessageBox.Show(SR.DataIsExist("Kelas"))
            End If
        Else
            updateTrClass(nResult)
        End If

        If Not nResult = -1 Then
            sHClass.SetSession("backRes", 1)
        End If

    End Sub
    Private Function IsTraineeRegistered(ByVal traineeID As Integer, ByVal objClass As TrClass, ByVal objOrgClass As TrClass) As Boolean
        For Each item As TrClassRegistration In objClass.TrClassRegistrations
            If objClass.ID = objOrgClass.ID Then
                Return False
            End If
            If item.TrTrainee.ID = traineeID Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Function IsRangeDateOverlap(ByVal objTrClass As TrClass) As String
        Dim _result As String = String.Empty
        Dim sDate As Date = New Date(objTrClass.StartDate.Year, objTrClass.StartDate.Month, objTrClass.StartDate.Day, 0, 0, 0)
        Dim eDate As Date = New Date(objTrClass.FinishDate.Year, objTrClass.FinishDate.Month, objTrClass.FinishDate.Day, 23, 59, 59)
        Dim trClassFacade As TrClassFacade = New TrClassFacade(User)
        Dim trClassRegCollection As ArrayList = objTrClass.TrClassRegistrations
        Dim ClassCollection As ArrayList = trClassFacade.RetrieveActiveList()
        If ClassCollection.Count > 0 Then
            For Each item As TrClass In ClassCollection
                If (item.FinishDate >= sDate And item.FinishDate <= eDate) Or (item.StartDate >= sDate And item.StartDate <= eDate) Or (item.StartDate >= sDate And item.FinishDate <= eDate) Or (item.StartDate <= sDate And item.FinishDate >= eDate) Then
                    For Each objClassReg As TrClassRegistration In trClassRegCollection
                        If IsTraineeRegistered(objClassReg.TrTrainee.ID, item, objTrClass) = True Then
                            _result += objClassReg.TrTrainee.Name & " terdaftar di " & item.ClassCode & ","
                        End If
                    Next
                End If
            Next
        End If
        If _result = String.Empty Then
            Return _result
        Else
            Return _result.Remove(_result.Length - 1, 1)
        End If
    End Function
    Private Sub updateTrClass(ByRef nResult As Integer)
        Dim objTrCourse As New TrCourse
        txtKodeKelas.ReadOnly = True
        With objClass
            .ClassCode = txtKodeKelas.Text.Trim()
            .ClassName = txtNamaKelas.Text.Trim()

            objTrCourse = getCourseID(txtKodeKategori.Text.Trim())
            If Not IsNothing(objTrCourse) Then
                .TrCourse = objTrCourse
            Else
                MessageBox.Show(SR.DataNotFound("Kode Kategory"))
                Exit Sub
            End If
            If .TrCourse.PaymentType = CType(EnumTrCourse.PaymentType.CHARGE, Short) Then
                If txtMRTCID.Text.Trim() <> String.Empty Then
                    If IsNothing(.TrMRTC) Then
                        If txtPaidDay.IsEmpty Then
                            MessageBox.Show(SR.DataNotFound("Jumlah Hari Berbayar"))
                            Exit Sub
                        End If
                        If txtPaidDay.Text.Equals("0") Then
                            MessageBox.Show(SR.DataNotFound("Jumlah Hari Berbayar minimal 1 hari"))
                            Exit Sub
                        End If

                        .TrMRTC = New TrMRTCFacade(User).Retrieve(txtMRTCID.Text)
                        .PaidDay = CInt(txtPaidDay.Text)
                        .PricePerDay = .TrMRTC.PricePerDay
                        .PriceTotal = .PaidDay * .PricePerDay
                    Else
                        If txtMRTCID.Text.ToLower.Equals(.TrMRTC.Code.ToLower) Then
                            .PaidDay = CInt(txtPaidDay.Text)
                            .PriceTotal = .PaidDay * .PricePerDay
                        Else
                            .TrMRTC = New TrMRTCFacade(User).Retrieve(txtMRTCID.Text)
                            .PaidDay = CInt(txtPaidDay.Text)
                            .PricePerDay = .TrMRTC.PricePerDay
                            .PriceTotal = .PaidDay * .PricePerDay
                        End If
                    End If
                Else
                    .TrMRTC = Nothing
                    .PaidDay = 0
                    .PricePerDay = 0
                    .PriceTotal = 0
                End If
            Else
                If txtMRTCID.IsNotEmpty Then
                    .TrMRTC = New TrMRTCFacade(User).Retrieve(txtMRTCID.Text)
                End If
                .PaidDay = 0
                .PricePerDay = 0
                .PriceTotal = 0
            End If
            
            .Status = ddlStatus.SelectedIndex
            .Location = txtLokasi.Text.Trim()
            .Lodging = txtPenginapan.Text.Trim()
            .LocationName = txtLocationName.Text.Trim()
            .Trainer1 = txtPengajar1.Text.Trim()
            .Trainer2 = txtPengajar2.Text.Trim()
            .Trainer3 = txtPengajar3.Text.Trim()
            .StartDate = ICTanggalMulai.Value.ToString.Trim()
            .FinishDate = ICTanggalSelesai.Value.ToString.Trim()
            .Capacity = txtKapasitas.Text.Trim()
            .Description = txtKeterangan.Text.Trim()
            .City = New CityFacade(User).Retrieve(Integer.Parse(ddlKota.SelectedValue))
            .ClassType = CType(ddlTipeKelas.SelectedValue, Short)
            .FilePath = hdnFilePath.Value
            .FileCertificatePath = hdnFilePath2.Value
            .FiscalYear = ddlTahunFiscal.SelectedValue
        End With
        Dim collClass As String = IsRangeDateOverlap(objClass)
        If collClass = String.Empty Then
            nResult = New TrClassFacade(User).Update(objClass)
        Else
            MessageBox.Show(collClass)
            Return
        End If

        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
            txtKodeKelas.Visible = True
            RequiredFieldValidator1.Visible = True
        End If
    End Sub

    Private Sub insertTrClass(ByVal objTrClass As TrClass, ByRef nResult As Integer)
        objTrClass = New TrClass
        Dim objTrCourse As New TrCourse
        Me.txtKodeKelas.Enabled = True
        If ICTanggalMulai.Value < Today Then
            MessageBox.Show("Tanggal mulai tidak boleh lebih kecil dari tanggal hari ini")
            Return
        ElseIf ICTanggalSelesai.Value < Today Then
            MessageBox.Show("Tanggal selesai tidak boleh lebih kecil dari tanggal hari ini")
            Return
        End If
        With objTrClass
            .ClassCode = txtKodeKelas.Text.Trim()
            .ClassName = txtNamaKelas.Text.Trim()
            objTrCourse = getCourseID(txtKodeKategori.Text.Trim())

            If Not IsNothing(objTrCourse) Then
                .TrCourse = objTrCourse
            Else
                MessageBox.Show(SR.DataNotFound("Kode Kategory"))
                Exit Sub
            End If

            If txtMRTCID.Text.Trim() <> String.Empty Then
                .TrMRTC = New TrMRTCFacade(User).Retrieve(txtMRTCID.Text)
            Else
                .TrMRTC = Nothing
            End If

            If divPayment.Visible = True Then
                If txtPaidDay.IsEmpty Then
                    MessageBox.Show(SR.DataNotFound("Jumlah Hari Berbayar"))
                    Exit Sub
                End If
                If txtPaidDay.Text.Equals("0") Then
                    MessageBox.Show(SR.DataNotFound("Jumlah Hari Berbayar minimal 1 hari"))
                    Exit Sub
                End If

                If txtPaidDay.IsEmpty Then
                    .PaidDay = 0
                Else
                    .PaidDay = CInt(txtPaidDay.Text)
                End If
                .PricePerDay = .TrMRTC.PricePerDay
                .PriceTotal = .PaidDay * .PricePerDay
            Else
                .PaidDay = 0
                .PricePerDay = 0
                .PriceTotal = 0
            End If

            .Status = ddlStatus.SelectedIndex
            .Location = txtLokasi.Text.Trim()
            .Lodging = txtPenginapan.Text.Trim()
            .LocationName = txtLocationName.Text.Trim()
            .Trainer1 = txtPengajar1.Text.Trim()
            .Trainer2 = txtPengajar2.Text.Trim()
            .Trainer3 = txtPengajar3.Text.Trim()
            .StartDate = ICTanggalMulai.Value.ToString.Trim()
            .FinishDate = ICTanggalSelesai.Value.ToString.Trim()
            .Capacity = txtKapasitas.Text.Trim()
            .Description = txtKeterangan.Text.Trim()
            .City = New CityFacade(User).Retrieve(Integer.Parse(ddlKota.SelectedValue))
            .ClassType = CType(ddlTipeKelas.SelectedValue, Short)
            .FiscalYear = ddlTahunFiscal.SelectedValue
            .FilePath = hdnFilePath.Value
            .FileCertificatePath = hdnFilePath2.Value
        End With

        nResult = New TrClassFacade(User).InsertTransaction(objTrClass)
        sHClass.SetSession("SESSION_CLASSID", nResult)
        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            Try
                InsertDataClassNumEval()
                MessageBox.Show(SR.SaveSuccess)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

    End Sub
    Private Function IsUnhack() As Boolean

        If txtKapasitas.Text.IndexOf("<") >= 0 Or txtKapasitas.Text.IndexOf(">") >= 0 Or txtKapasitas.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtKeterangan.Text.IndexOf("<") >= 0 Or txtKeterangan.Text.IndexOf(">") >= 0 Or txtKeterangan.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtKodeKategori.Text.IndexOf("<") >= 0 Or txtKodeKategori.Text.IndexOf(">") >= 0 Or txtKodeKategori.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtKodeKelas.Text.IndexOf("<") >= 0 Or txtKodeKelas.Text.IndexOf(">") >= 0 Or txtKodeKelas.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtLokasi.Text.IndexOf("<") >= 0 Or txtLokasi.Text.IndexOf(">") >= 0 Or txtLokasi.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtPenginapan.Text.IndexOf("<") >= 0 Or txtPenginapan.Text.IndexOf(">") >= 0 Or txtPenginapan.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtLocationName.Text.IndexOf("<") >= 0 Or txtLocationName.Text.IndexOf(">") >= 0 Or txtLocationName.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtNamaKelas.Text.IndexOf("<") >= 0 Or txtNamaKelas.Text.IndexOf(">") >= 0 Or txtNamaKelas.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        Return True
    End Function

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        If Not sHClass.GetSession("editID") Is Nothing Then
            viewEditClass()
        End If
        If sHClass.GetSession("clear") = 1 And (sHClass.GetSession("editID") Is Nothing) Then
            Me.Page.ClearTextBoxWithPrefix("txt")
            
            ICTanggalMulai.Value = Today
            ICTanggalSelesai.Value = Today
            hdnFilePath.Value = String.Empty
            hdnFilePath2.Value = String.Empty
            ddlPropinsi.ClearSelection()
            ddlTipeKelas.ClearSelection()
            ddlKota.ClearSelection()
            ddlKota.Items.Clear()
            BindDDLTahunFiskal()

            hdnFilePath.Value = String.Empty
            
        End If

    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        sHClass.RemoveSession("viewID")
        sHClass.RemoveSession("editID")
        sHClass.RemoveSession("SESSION_CLASSID")
        sHClass.SetSession("backRes", 1)
        Response.Redirect("FrmTrClassAss.aspx")
    End Sub

    Private Function getCourseID(ByVal courseCode As String) As TrCourse
        Dim objTrCourseAl As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrCourse), "CourseCode", MatchType.Exact, courseCode))

        objTrCourseAl = New TrCourseFacade(User).Retrieve(criterias)

        If Not objTrCourseAl.Count <= 0 Then
            Return CType(objTrCourseAl.Item(0), TrCourse)
        Else
            Return Nothing
        End If
    End Function

#Region "CourseEvaluationHandler"

    Private Function GetCourseEvaluation() As ArrayList
        Dim objTrCourse As New TrCourse
        objTrCourse = getCourseID(txtKodeKategori.Text.Trim())
        If (Not objTrCourse Is Nothing) Then
            Return BindDataCourseEvaluation(objTrCourse.ID)
        Else
            Return Nothing
        End If
    End Function

    Private Function BindDataCourseEvaluation(ByVal _CourseId As Integer) As ArrayList
        Dim _trCourseEvaluationFacade As New TrCourseEvaluationFacade(User)
        Dim _arrayCourseEvaluation As New ArrayList
        Dim crtCourseEvalParam As CriteriaComposite

        Dim srtParam As SortCollection = New SortCollection

        crtCourseEvalParam = New CriteriaComposite(New Criteria(GetType(TrCourseEvaluation), "TrCourse", MatchType.Exact, _CourseId))

        srtParam.Add(New Sort(GetType(TrCourseEvaluation), "Type", Sort.SortDirection.ASC))
        srtParam.Add(New Sort(GetType(TrCourseEvaluation), "EvaluationCode", Sort.SortDirection.ASC))

        _arrayCourseEvaluation = _trCourseEvaluationFacade.Retrieve(crtCourseEvalParam, srtParam)

        Return _arrayCourseEvaluation
    End Function

    Sub InsertDataClassNumEval()

        'Delete All First
        DeleteAllClassNumEval()

        Dim _trClassNumEvalFacade As TrClassNumEvaluationFacade = New TrClassNumEvaluationFacade(User)
        Dim _trCourseEvalFacade As TrCourseEvaluationFacade = New TrCourseEvaluationFacade(User)
        Dim _trClassFacade As TrClassFacade = New TrClassFacade(User)

        Dim _trClassNumEval As TrClassNumEvaluation = New TrClassNumEvaluation
        Dim _trCourseEvaluation As TrCourseEvaluation = New TrCourseEvaluation
        Dim _trClass As TrClass = _trClassFacade.Retrieve(CType(sHClass.GetSession("SESSION_CLASSID"), Integer))

        Dim _arrayCourseEvaluation As New ArrayList
        _arrayCourseEvaluation = GetCourseEvaluation()

        If Not (_arrayCourseEvaluation Is Nothing) Then
            If (_arrayCourseEvaluation.Count > 0) Then

                For i As Integer = 0 To _arrayCourseEvaluation.Count - 1
                    'Insert Data
                    With _trClassNumEval
                        _trCourseEvaluation = CType(_arrayCourseEvaluation(i), TrCourseEvaluation)
                        .TrCourseEvaluation = _trCourseEvaluation
                        .TrClass = _trClass
                        .SpecialName = KTB.DNet.Utility.CommonFunction.GetNamaKhususEvalTraining(_trClass.ID.ToString(), _trCourseEvaluation.ID)
                    End With
                    _trClassNumEvalFacade.Insert(_trClassNumEval)

                Next
            End If
        Else
            Throw New ApplicationException("Kategori Training Tidak Ada")
        End If


    End Sub

    Private Sub DeleteAllClassNumEval()
        Dim objRetVal As New TrClassNumEvaluation
        Dim _trClassNumEvalFacade As TrClassNumEvaluationFacade = New TrClassNumEvaluationFacade(User)
        Dim _lst As New ArrayList
        Dim crtParam As CriteriaComposite
        Dim _classId As Integer = Val(sHClass.GetSession("SESSION_CLASSID"))

        crtParam = New CriteriaComposite(New Criteria(GetType(TrClassNumEvaluation), "TrClass.ID", MatchType.Exact, _classId))
        _lst = _trClassNumEvalFacade.Retrieve(crtParam)

        For Each item As TrClassNumEvaluation In _lst
            _trClassNumEvalFacade.DeleteFromDB(item)
        Next

    End Sub

#End Region

    Protected Sub BtnLoad_Click(sender As Object, e As EventArgs) Handles BtnLoad.Click
        If String.IsNullOrEmpty(txtRefClass.Text) Then
            MessageBox.Show("Kelas tidak ditemukan")
            Exit Sub
        End If
        Dim objClass As TrClass = New TrClassFacade(User).Retrieve(txtRefClass.Text)
        If objClass.ID.Equals(0) Then
            MessageBox.Show("Kelas tidak ditemukan")
            Exit Sub
        End If

        txtNamaKelas.Text = objClass.ClassName.Trim()
        txtLokasi.Text = objClass.Location.Trim()
        txtPenginapan.Text = objClass.Lodging
        txtLocationName.Text = objClass.LocationName.Trim()
        txtKapasitas.Text = objClass.Capacity.ToString()
        txtPengajar1.Text = objClass.Trainer1.Trim()
        txtPengajar2.Text = objClass.Trainer2.Trim()
        txtPengajar3.Text = objClass.Trainer3.Trim()
        txtKodeKategori.Text = objClass.TrCourse.CourseCode.Trim()
        txtKeterangan.Text = objClass.Description.Trim()
        ICTanggalMulai.Value = objClass.StartDate.ToString("dd/MM/yyyy").Trim()
        ICTanggalSelesai.Value = objClass.FinishDate.ToString("dd/MM/yyyy").Trim()
        If Not String.IsNullOrEmpty(objClass.ClassType) And Not objClass.ClassType.Equals(0) Then
            ddlTipeKelas.ClearSelection()
            ddlTipeKelas.SelectedValue = objClass.ClassType
        End If
        If objClass.City IsNot Nothing Then
            ddlPropinsi.ClearSelection()
            ddlPropinsi.Items.FindByValue(objClass.City.Province.ID).Selected = True
            BindDdlCity(objClass.City.Province.ID.ToString())
            ddlKota.ClearSelection()
            ddlKota.Items.FindByValue(objClass.City.ID.ToString()).Selected = True
        End If
        txtLokasi.Text = objClass.Location
        txtLocationName.Text = objClass.LocationName
        If ddlTahunFiscal.Items.FindByValue(objClass.FiscalYear) Is Nothing Then
            ddlTahunFiscal.Text = objClass.FiscalYear
        Else
            ddlTahunFiscal.ClearSelection()
            ddlTahunFiscal.SelectedValue = objClass.FiscalYear
        End If

    End Sub

    Protected Sub ddlPropinsi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPropinsi.SelectedIndexChanged
        Try
            BindDdlCity(ddlPropinsi.SelectedValue)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub BindDdlProvince()
        ddlPropinsi.Items.Clear()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ddlPropinsi.DataSource = New ProvinceFacade(User).RetrieveActiveList(criterias, "ProvinceName", Sort.SortDirection.ASC)
        ddlPropinsi.DataTextField = "ProvinceName"
        ddlPropinsi.DataValueField = "ID"
        ddlPropinsi.DataBind()
        ddlPropinsi.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
    End Sub

    Private Sub BindDdlCity(Optional ByVal selectedProvince As String = "0")
        ddlKota.Items.Clear()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A")) 'A = Aktif; X = Tidak Aktif
        criterias.opAnd(New Criteria(GetType(City), "Province.ID", MatchType.Exact, CType(selectedProvince, Integer)))

        ddlKota.DataSource = New CityFacade(User).RetrieveActiveList(criterias, "CityName", Sort.SortDirection.ASC)
        ddlKota.DataTextField = "CityName"
        ddlKota.DataValueField = "ID"
        ddlKota.DataBind()
        ddlKota.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
    End Sub

    Private Sub BindDDLTipeClass()
        ddlTipeKelas.ClearSelection()
        ddlTipeKelas.Items.Clear()
        ddlTipeKelas.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        ddlTipeKelas.Items.Add(New ListItem(EnumTrClass.GetStringValueClassType(EnumTrClass.EnumClassType.INCLASS_TRAINING), EnumTrClass.EnumClassType.INCLASS_TRAINING))
        ddlTipeKelas.Items.Add(New ListItem(EnumTrClass.GetStringValueClassType(EnumTrClass.EnumClassType.E_LEARNING), EnumTrClass.EnumClassType.E_LEARNING))
        ddlTipeKelas.Items.Add(New ListItem(EnumTrClass.GetStringValueClassType(EnumTrClass.EnumClassType.INHOUSE_TRAINING), EnumTrClass.EnumClassType.INHOUSE_TRAINING))
        ddlTipeKelas.Items.Add(New ListItem(EnumTrClass.GetStringValueClassType(EnumTrClass.EnumClassType.FLEET_TRAINING), EnumTrClass.EnumClassType.FLEET_TRAINING))
    End Sub

    Private Sub BindDDLTahunFiskal()
        Dim GetTahun As Integer = DateTime.Now.Year
        ddlTahunFiscal.ClearSelection()
        ddlTahunFiscal.Items.Clear()
        'Before
        For x As Integer = 4 To 0 Step -1
            Dim value1 As String = (GetTahun - x).ToString()
            Dim value2 As String = (GetTahun - x - 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value2, value1)
            ddlTahunFiscal.Items.Add(New ListItem(value, value))
        Next
        'After
        For x As Integer = 0 To 4
            Dim value1 As String = (GetTahun + x).ToString()
            Dim value2 As String = (GetTahun + x + 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value1, value2)
            ddlTahunFiscal.Items.Add(New ListItem(value, value))
        Next
        ddlTahunFiscal.SelectedValue = String.Format("{0}/{1}", GetTahun.ToString(), (GetTahun + 1).ToString())
    End Sub


    Protected Sub cvPropinsi_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If ddlPropinsi.SelectedValue = "0" Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            args.IsValid = False
        End Try
    End Sub

    Protected Sub cvKota_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If ddlKota.SelectedValue = "0" Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            args.IsValid = False
        End Try
    End Sub

    Private Sub GenerateCodeClass()
        Dim code As String = String.Empty
        Dim fiscalYear As String = ddlTahunFiscal.SelectedValue
        fiscalYear = fiscalYear.Substring(2, 2)
        Dim gelombang As Integer = 1
        Dim corse As TrCourse = New TrCourseFacade(User).Retrieve(txtKodeKategori.Text)
        If String.IsNullOrEmpty(corse.ClassCode) Then
            code = corse.CourseCode.ToUpper() + fiscalYear
        Else
            code = corse.ClassCode.ToUpper() + fiscalYear
        End If

        Dim srtParam As SortCollection = New SortCollection
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "ClassCode", MatchType.StartsWith, code))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "TrCourse.CourseCode", MatchType.Exact, corse.CourseCode))
        srtParam.Add(New Sort(GetType(TrClass), "CreatedTime", Sort.SortDirection.DESC))
        Dim arrClass As ArrayList = New TrClassFacade(User).Retrieve(criterias, srtParam)

        If arrClass.Count.Equals(0) Then
            txtKodeKelas.Text = code + gelombang.GenerateIncrement(2)
        Else
            Dim replacedCode As String = CType(arrClass(0), TrClass).ClassCode.Replace(code, "").ToString()

            If replacedCode = String.Empty Then
                replacedCode = "0"
            End If

            Dim number As Integer = Integer.Parse(replacedCode) + 1
            txtKodeKelas.Text = code + number.GenerateIncrement(2)
        End If
    End Sub


    Private Sub getCode_Click(sender As Object, e As EventArgs) Handles getCode.Click
        GenerateCodeClass()
    End Sub

    Protected Sub btnUploadMateri_Click(sender As Object, e As EventArgs) Handles btnUploadMateri.Click
        Try
            Dim rest As String = helpers.UploadFile(photoSrc, KTB.DNet.Lib.WebConfig.GetValue("TrClassMateri"), _
                                                    KTB.DNet.Lib.WebConfig.GetValue("MaximumTrClassMateriSize"))
            Dim errArr() As String = rest.Split("|")
            If errArr(0).Equals("Error") Then
                MessageBox.Show(errArr(1))
                Return
            Else
                hdnFilePath.Value = errArr(1)
            End If
            trDownload.Visible = True
            trUpload.Visible = False
        Catch ex As Exception
            MessageBox.Show("Error saat mengupload foto")
        End Try
    End Sub

    Private Sub lbnDelete_Click(sender As Object, e As EventArgs) Handles lbnDelete.Click
        hdnFilePath.Value = String.Empty
        trUpload.Visible = True
        trDownload.Visible = False
    End Sub

    Private Sub lbtnDownload_Click(sender As Object, e As EventArgs) Handles lbtnDownload.Click
        Try
            helpers.DownloadFile(hdnFilePath.Value)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtKodeKategori_TextChanged(sender As Object, e As EventArgs) Handles txtKodeKategori.TextChanged
        GenerateCodeClass()
    End Sub

    Private Sub btnTriggerMRTC_Click(sender As Object, e As EventArgs) Handles btnTriggerMRTC.Click
        txtMRTCID_TextChanged(Nothing, Nothing)
    End Sub

    Private Sub txtMRTCID_TextChanged(sender As Object, e As EventArgs) Handles txtMRTCID.TextChanged
        Try
            Dim mrtc As TrMRTC = New TrMRTCFacade(User).Retrieve(txtMRTCID.Text)
            If mrtc.ID <> 0 Then
                ddlPropinsi.ClearSelection()
                ddlPropinsi.Items.FindByValue(mrtc.City.Province.ID).Selected = True
                ddlPropinsi_SelectedIndexChanged(Nothing, Nothing)

                ddlKota.ClearSelection()
                ddlKota.Items.FindByValue(mrtc.City.ID).Selected = True

                txtLocationName.Text = mrtc.Name
                txtLokasi.Text = mrtc.Address
                If Not mrtc.Dealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                    txtPenginapan.Text = mrtc.Address
                Else
                    txtPenginapan.Clear()
                End If


                For i As Integer = 0 To mrtc.ListOfDetail.Count - 1
                    Dim pic As TrMRTCPIC = CType(mrtc.ListOfDetail(i), TrMRTCPIC)

                    If i = 0 Then
                        txtPengajar1.Text = pic.TrTrainee.Name
                    End If

                    If i = 1 Then
                        txtPengajar2.Text = pic.TrTrainee.Name
                    End If

                    If i = 2 Then
                        txtPengajar3.Text = pic.TrTrainee.Name
                    End If

                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cvMRTC_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try

            If txtMRTCID.Text.Trim() <> String.Empty Then
                Dim mrtc As TrMRTC = New TrMRTCFacade(User).Retrieve(txtMRTCID.Text)
                If mrtc.ID = 0 Then
                    args.IsValid = False
                    cvMRTC.ErrorMessage = "*MRTC tidak terdaftar dalam database"
                Else
                    args.IsValid = True
                End If
            Else
                cvMRTC.ErrorMessage = "*MRTC harus dipilih"
                args.IsValid = False
            End If


        Catch ex As Exception
            args.IsValid = False
        End Try
    End Sub

    Private Sub btnTriggerPrice_Click(sender As Object, e As EventArgs) Handles btnTriggerPrice.Click
        Try
            If txtMRTCID.ID <> String.Empty And txtKodeKategori.Text <> String.Empty Then
                Dim mrtcData As TrMRTC = New TrMRTCFacade(User).Retrieve(txtMRTCID.Text)
                Dim courseData As TrCourse = New TrCourseFacade(User).Retrieve(txtKodeKategori.Text)

                If mrtcData.ID <> 0 And courseData.ID <> 0 Then
                    If courseData.PaymentType = EnumTrCourse.PaymentType.CHARGE Then
                        divPayment.Visible = True

                        If Not IsNothing(objClass) Then
                            If IsClassHaveTrainee(objClass) Then
                                lblPricePerDay.Text = objClass.PricePerDay.AddThousandDelimiter()
                            Else
                                lblPricePerDay.Text = mrtcData.PricePerDay.AddThousandDelimiter()
                            End If
                        Else
                            lblPricePerDay.Text = mrtcData.PricePerDay.AddThousandDelimiter()
                        End If
                    Else
                        divPayment.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtPaidDay_TextChanged(sender As Object, e As EventArgs) Handles txtPaidDay.TextChanged
        If Not lblPricePerDay.Text.IsNullorEmpty And txtPaidDay.IsNotEmpty Then
            Dim price As Integer = CDec(lblPricePerDay.Text.RemoveThousandDelimeter())
            Dim day As Integer = CInt(txtPaidDay.Text)
            Dim total As Integer = price * day
            lblTotalPrice.Text = total.AddThousandDelimiter()
        Else
            lblTotalPrice.Text = "0"
        End If
    End Sub

    Protected Sub btnUploadDoc_Click(sender As Object, e As EventArgs) Handles btnUploadDoc.Click
        Try
            Dim rest As String = helpers.UploadFile(docSrc, KTB.DNet.Lib.WebConfig.GetValue("TrClassMateri"), _
                                                    KTB.DNet.Lib.WebConfig.GetValue("MaximumTrClassMateriSize"))
            Dim errArr() As String = rest.Split("|")
            If errArr(0).Equals("Error") Then
                MessageBox.Show(errArr(1))
                Return
            Else
                hdnFilePath2.Value = errArr(1)
            End If
            trDownloadCer.Visible = True
            trUploadCer.Visible = False
        Catch ex As Exception
            MessageBox.Show("Error saat mengupload foto")
        End Try
    End Sub

    Protected Sub lbnDeleteCer_Click(sender As Object, e As EventArgs) Handles lbnDeleteCer.Click
        hdnFilePath2.Value = String.Empty
        trUploadCer.Visible = True
        trDownloadCer.Visible = False
    End Sub

    Protected Sub lbtnDownloadCer_Click(sender As Object, e As EventArgs) Handles lbtnDownloadCer.Click
        Try
            helpers.DownloadFile(hdnFilePath2.Value)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function IsClassHaveTrainee(ByVal objClass As TrClass) As Boolean
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, objClass.ID))

        Dim arlResult As ArrayList = New TrClassRegistrationFacade(User).Retrieve(criterias)

        If arlResult.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub ddlTahunFiscal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahunFiscal.SelectedIndexChanged
        If txtKodeKategori.Text <> String.Empty Then
            getCode_Click(Nothing, Nothing)
        End If
    End Sub

    Private Function IsHaveTraineeInClass() As Boolean
        If sHClass.GetSession("editID") Is Nothing Then
            Return False
        Else
            If objClass.ID <> 0 Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, objClass.ID))

                Dim arlSiswa As ArrayList = New TrClassRegistrationFacade(User).Retrieve(criterias)

                If arlSiswa.Count = 0 Then
                    Return False
                Else
                    Return True
                End If

            Else
                Return False
            End If
        End If
    End Function

End Class
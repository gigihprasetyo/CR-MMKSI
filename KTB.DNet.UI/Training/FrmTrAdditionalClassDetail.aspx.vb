#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports KTB.DNet.Parser
Imports OfficeOpenXml
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
#End Region


Public Class FrmTrAdditionalClassDetail
    Inherits System.Web.UI.Page
    Dim helpers As TrainingHelpers = New TrainingHelpers(Me.Page)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                InitPage()
                Dim objDealer As Dealer = CType(Session("Dealer"), Dealer)
                If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                    If (Page.Request.QueryString("id") = "0") Then
                        LoadInputMode()
                    Else
                        LoadEditMode()
                        txtRevisi.Enabled = False
                    End If
                ElseIf objDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                    If (Page.Request.QueryString("id") = "0") Then
                        Throw New Exception("Data kelas inhouse / fleet tidak ditemukan")
                    Else
                        LoadEditMode()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub InitPage()
        BindDdlTipeKelas()
        BindDdlProvince()
        BindDDLTahunFiskal()
    End Sub

    Private Sub BindDdlTipeKelas()
        ddlTipeKelas.ClearSelection()
        ddlTipeKelas.Items.Clear()
        ddlTipeKelas.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        ddlTipeKelas.Items.Add(New ListItem(EnumTrClass.GetStringValueClassType(EnumTrClass.EnumClassType.INHOUSE_TRAINING), EnumTrClass.EnumClassType.INHOUSE_TRAINING))
        ddlTipeKelas.Items.Add(New ListItem(EnumTrClass.GetStringValueClassType(EnumTrClass.EnumClassType.FLEET_TRAINING), EnumTrClass.EnumClassType.FLEET_TRAINING))
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

    Private Sub BindDDLTahunFiskal()
        Dim GetTahun As Integer = DateTime.Now.Year
        ddlFiscalYear.ClearSelection()
        ddlFiscalYear.Items.Clear()
        'Before
        For x As Integer = 4 To 0 Step -1
            Dim value1 As String = (GetTahun - x).ToString()
            Dim value2 As String = (GetTahun - x - 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value2, value1)
            ddlFiscalYear.Items.Add(New ListItem(value, value))
        Next
        'After
        For x As Integer = 0 To 4
            Dim value1 As String = (GetTahun + x).ToString()
            Dim value2 As String = (GetTahun + x + 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value1, value2)
            ddlFiscalYear.Items.Add(New ListItem(value, value))
        Next
        ddlFiscalYear.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        ddlFiscalYear.SelectedValue = String.Format("{0}/{1}", GetTahun.ToString(), (GetTahun + 1).ToString())
    End Sub

    Private Sub LoadInputMode()
        btnApprove.Visible = False
        btnRevisi.Visible = False
        rowRevisi.Visible = False
        btnRequest.Visible = True

        trDownload.Visible = False
        trDownloadList.Visible = False

    End Sub

    Private Sub LoadEditMode()
        Dim classId As Integer = CType(Page.Request.QueryString("id"), Integer)
        Dim objAdditionalClass As TrAdditionalClass = New TrAdditionalClassFacade(User).Retrieve(classId)

        If objAdditionalClass.ID = 0 Then
            Throw New Exception("ID kelas inhouse / fleet tidak ditemukan")
        End If

        LoadDataToUi(objAdditionalClass)

        If objAdditionalClass.Status = EnumTrAdditionalClass.EnumStatus.REQUEST Or objAdditionalClass.Status = EnumTrAdditionalClass.EnumStatus.REVISI Then
            lblPopUpCourse.Visible = False
            txtKodeKategori.Disabled()
            ddlTipeKelas.Enabled = False
            btnRequest.Visible = False
            btnApprove.Visible = False
            btnRevisi.Visible = True
            rowRevisi.Visible = True

            Dim objDealer As Dealer = CType(Session("Dealer"), Dealer)
            If objDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                DisableAllInput()
                txtRevisi.Enabled = True
                btnRevisi.Visible = True
                btnApprove.Visible = True
            End If

            If CType(Page.Request.QueryString("mode"), String) = "view" Then
                DisableAllInput()
                txtRevisi.Enabled = False
            End If

            If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                btnRevisi.Text = "Simpan"
                If objAdditionalClass.Status = EnumTrAdditionalClass.EnumStatus.REQUEST Then
                    rowRevisi.Visible = False
                End If
            End If

        ElseIf objAdditionalClass.Status = EnumTrAdditionalClass.EnumStatus.APPROVE Then
            DisableAllInput()
            txtRevisi.Enabled = False
        End If

    End Sub

    Private Sub LoadDataToUi(ByVal obj As TrAdditionalClass)
        txtKodeKategori.Text = obj.TrCourse.CourseCode
        lblKodeKelas.Text = obj.ClassCode
        'lblNamaKelas.Text = obj.ClassName
        txtNamaKelas.Text = obj.ClassName

        ddlTipeKelas.ClearSelection()
        ddlTipeKelas.Items.FindByValue(obj.ClassType).Selected = True

        ddlPropinsi.ClearSelection()
        ddlPropinsi.Items.FindByValue(obj.City.Province.ID).Selected = True
        ddlPropinsi_SelectedIndexChanged(Nothing, Nothing)

        ddlKota.ClearSelection()
        ddlKota.Items.FindByValue(obj.City.ID).Selected = True

        Try
            ddlFiscalYear.ClearSelection()
            ddlFiscalYear.Items.FindByValue(obj.FiscalYear).Selected = True
        Catch ex As Exception

        End Try

        txtLocationName.Text = obj.LocationName
        txtLokasi.Text = obj.Location
        txtPengajar1.Text = obj.Trainer1
        txtPengajar2.Text = obj.Trainer2
        txtPengajar3.Text = obj.Trainer3
        txtKeterangan.Text = obj.Description
        txtNamaMateri.Text = obj.FileName

        ICTanggalMulai.Value = obj.StartDate
        ICTanggalSelesai.Value = obj.FinishDate

        hdnFilePath.Value = obj.FileMateriPath
        hdnFilePathList.Value = obj.FileSiswaPath

        txtRevisi.Text = obj.APMResponse

        If Not String.IsNullOrEmpty(hdnFilePath.Value) Then
            lbtnDownload.Text = "Download " & obj.FileName
            trDownload.Visible = True
            trUpload.Visible = False
            lbtnDownload.Visible = True
        Else
            trDownload.Visible = False
            trUpload.Visible = True
            lbtnDownload.Visible = False
        End If

        If Not String.IsNullOrEmpty(hdnFilePathList.Value) Then
            trDownloadList.Visible = True
            trUploadList.Visible = False
            lbtnDownloadList.Visible = True
        Else
            trDownloadList.Visible = False
            trUploadList.Visible = True
            lbtnDownloadList.Visible = True
        End If

    End Sub

    Private Sub ddlPropinsi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPropinsi.SelectedIndexChanged
        Try
            BindDdlCity(ddlPropinsi.SelectedValue)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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

    Private Sub DisableAllInput()
        txtKodeKategori.Enabled = False
        lblPopUpCourse.Visible = False
     
        ddlTipeKelas.Enabled = False
        ddlPropinsi.Enabled = False
        ddlFiscalYear.Enabled = False
        ddlKota.Enabled = False
        txtNamaKelas.Enabled = False
        txtLocationName.Enabled = False
        txtLokasi.Enabled = False
        txtPengajar1.Enabled = False
        txtPengajar2.Enabled = False
        txtPengajar3.Enabled = False
        txtKeterangan.Enabled = False
        txtNamaMateri.Enabled = False
        trUpload.Visible = False
        trDownload.Visible = True
        trUploadList.Visible = False
        trDownloadList.Visible = True
        ICTanggalMulai.Enabled = False
        ICTanggalSelesai.Enabled = False
        btnApprove.Visible = False
        btnBatal.Visible = False
        btnRequest.Visible = False
        btnRevisi.Visible = False
        lbtnDeleteList.Visible = False
        lbnDelete.Visible = False
    End Sub



    Private Sub MappingUiToData(ByRef result As TrAdditionalClass)

        result.TrCourse = New TrCourseFacade(User).Retrieve(txtKodeKategori.Text)
        result.ClassCode = lblKodeKelas.Text
        'result.ClassName = lblNamaKelas.Text
        result.ClassName = txtNamaKelas.Text
        result.City = New City(CInt(ddlKota.SelectedValue))
        result.FiscalYear = ddlFiscalYear.SelectedValue
        result.Location = txtLokasi.Text
        result.LocationName = txtLocationName.Text
        result.Trainer1 = txtPengajar1.Text
        result.Trainer2 = txtPengajar2.Text
        result.Trainer3 = txtPengajar3.Text
        result.Description = txtKeterangan.Text
        result.StartDate = ICTanggalMulai.Value
        result.FinishDate = ICTanggalSelesai.Value
        result.FileMateriPath = hdnFilePath.Value
        result.FileSiswaPath = hdnFilePathList.Value
        result.FileName = txtNamaMateri.Text
        result.ClassType = ddlTipeKelas.SelectedValue

    End Sub

    Private Sub btnRequest_Click(sender As Object, e As EventArgs) Handles btnRequest.Click
        Try

            If Not Page.IsValid Then
                Exit Sub
            End If

            Dim obj As New TrAdditionalClass
            MappingUiToData(obj)
            obj.Dealer = CType(Session("Dealer"), Dealer)
            obj.Status = EnumTrAdditionalClass.EnumStatus.REQUEST
            'ValidateRequest(obj)
            ' Dim classCodeResult As String = String.Empty
            Save(obj)
            'MessageBox.Show("Data berhasil disimpan dengan kode kelas : " & classCodeResult)
            ClearField()
            LoadInputMode()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ValidateRequest(ByVal obj As TrAdditionalClass)

        Dim existClass As TrAdditionalClass = New TrAdditionalClassFacade(User).Retrieve(obj.ClassCode)
        If existClass.ID <> 0 Then
            Throw New Exception("Kode kelas sudah terdaftar")
        End If

    End Sub

    Private Sub ClearField()
        txtKodeKategori.Text = String.Empty
        lblKodeKelas.Text = String.Empty
        'lblNamaKelas.Text = String.Empty
        txtNamaKelas.Text = String.Empty
        ddlTipeKelas.ClearSelection()
        ddlPropinsi.ClearSelection()
        ddlKota.ClearSelection()
        txtLocationName.Text = String.Empty
        txtLokasi.Text = String.Empty
        txtPengajar1.Text = String.Empty
        txtPengajar2.Text = String.Empty
        txtPengajar3.Text = String.Empty
        txtKeterangan.Text = String.Empty
        txtNamaMateri.Text = String.Empty
        ICTanggalMulai.Value = DateTime.Now
        ICTanggalSelesai.Value = DateTime.Now
        txtRevisi.Text = String.Empty
        hdnFilePath.Value = String.Empty
        hdnFilePathList.Value = String.Empty
    End Sub

    Private Sub btnRevisi_Click(sender As Object, e As EventArgs) Handles btnRevisi.Click
        Try

            'If Not Page.IsValid Then
            '    Exit Sub
            'End If

            If txtRevisi.Text.Trim = String.Empty Then
                MessageBox.Show("Alasan revisi harus diisi")
                Exit Sub
            End If

            Dim id As Integer = CInt(Page.Request.QueryString("id"))

            Dim obj As TrAdditionalClass = New TrAdditionalClassFacade(User).Retrieve(id)
            MappingUiToData(obj)

            Dim objDealer As Dealer = CType(Session("Dealer"), Dealer)
            If objDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                obj.APMResponse = txtRevisi.Text.Trim()
                obj.Status = EnumTrAdditionalClass.EnumStatus.REVISI
            Else
                obj.Status = EnumTrAdditionalClass.EnumStatus.REQUEST
            End If

            Save(obj)
            btnKembali_Click(Nothing, Nothing)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try

            'If Not Page.IsValid Then
            '    Exit Sub
            'End If
            Dim facade As TrAdditionalClassFacade = New TrAdditionalClassFacade(User)
            Dim obj As TrAdditionalClass = facade.Retrieve(CInt(Page.Request.QueryString("id")))
            obj.APMResponse = txtRevisi.Text
            obj.Status = EnumTrAdditionalClass.EnumStatus.APPROVE
            obj.ClassCode = facade.GetClassCode(obj.ClassType)
            Save(obj)
            MessageBox.Show("Kelas telah diapprove dengan kode kelas : " & obj.ClassCode)
            btnKembali_Click(Nothing, Nothing)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Save(ByVal obj As TrAdditionalClass, Optional ByRef classCodeResult As String = "")

        Dim facade As TrAdditionalClassFacade = New TrAdditionalClassFacade(User)

        If obj.ID = 0 Then
            classCodeResult = facade.Insert(obj)
        Else
            facade.Update(obj)
        End If
    End Sub


    Private Sub btnUploadMateri_Click(sender As Object, e As EventArgs) Handles btnUploadMateri.Click
        Try
            Dim rest As String = helpers.UploadFile(photoSrc, KTB.DNet.Lib.WebConfig.GetValue("TrAdditionalClassMateri"), _
                                                    KTB.DNet.Lib.WebConfig.GetValue("MaximumAdditionalClassUploadSize"))
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
            MessageBox.Show("Error saat mengupload materi")
        End Try
    End Sub

    Private Sub btnUploadList_Click(sender As Object, e As EventArgs) Handles btnUploadList.Click
        Try

            Dim errMessage As String = String.Empty
            If Not IsListFileValid(errMessage) Then
                MessageBox.Show(errMessage)
                Return
            End If

            Dim rest As String = helpers.UploadFile(listSrc, KTB.DNet.Lib.WebConfig.GetValue("TrAdditionalClassList"), _
                                                    KTB.DNet.Lib.WebConfig.GetValue("MaximumAdditionalClassUploadSize"))
            Dim errArr() As String = rest.Split("|")
            If errArr(0).Equals("Error") Then
                MessageBox.Show(errArr(1))
                Return
            Else
                hdnFilePathList.Value = errArr(1)
            End If
            trDownloadList.Visible = True
            trUploadList.Visible = False
        Catch ex As Exception
            MessageBox.Show("Error saat mengupload daftar siswa")
        End Try
    End Sub

    Private Sub lbtnDownload_Click(sender As Object, e As EventArgs) Handles lbtnDownload.Click
        Try
            helpers.DownloadFile(hdnFilePath.Value)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub lbtnDownloadList_Click(sender As Object, e As EventArgs) Handles lbtnDownloadList.Click
        Try
            helpers.DownloadFile(hdnFilePathList.Value)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub lbnDelete_Click(sender As Object, e As EventArgs) Handles lbnDelete.Click
        hdnFilePath.Value = String.Empty
        trUpload.Visible = True
        trDownload.Visible = False
    End Sub

    Private Sub lbtnDeleteList_Click(sender As Object, e As EventArgs) Handles lbtnDeleteList.Click
        hdnFilePathList.Value = String.Empty
        trUploadList.Visible = True
        trDownloadList.Visible = False
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

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("FrmTrAdditionalClass.aspx")
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Try
            If Page.Request.QueryString("id") = "0" Then
                ClearField()
            Else
                Dim obj As TrAdditionalClass = New TrAdditionalClassFacade(User).Retrieve(CInt(Page.Request.QueryString("id")))
                LoadDataToUi(obj)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Protected Sub cvKodeKategori_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try

            If txtKodeKategori.Text.Trim() = String.Empty Then
                Throw New Exception("Kategori harus diisi")
            End If

            Dim course As TrCourse = New TrCourseFacade(User).Retrieve(txtKodeKategori.Text.Trim())
            If course.ID = 0 Then
                Throw New Exception("Kategori tidak ditemukan di dalam database")
            End If

            args.IsValid = True
        Catch ex As Exception
            cvKodeKategori.ErrorMessage = "*" + ex.Message
            args.IsValid = False
        End Try
    End Sub

    Protected Sub cvTanggalMulai_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            'If ICTanggalMulai.Value < Date.Now Then
            '    Throw New Exception("Tanggal mulai tidak boleh kurang dari hari ini")
            'End If
            args.IsValid = True
        Catch ex As Exception
            cvTanggalMulai.ErrorMessage = "*" + ex.Message
            args.IsValid = False
        End Try
    End Sub

    Protected Sub cvMateri_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If hdnFilePath.Value = String.Empty Then
                cvMateri.ErrorMessage = "*Report harus diupload"
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            cvMateri.ErrorMessage = "*" + ex.Message
            args.IsValid = False
        End Try
    End Sub

    Protected Sub cvListSiswa_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If hdnFilePathList.Value = String.Empty Then
                cvListSiswa.ErrorMessage = "*List Siswa harus diupload"
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            cvListSiswa.ErrorMessage = "*" + ex.Message
            args.IsValid = False
        End Try
    End Sub

    Protected Sub cvFiscalYear_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If ddlFiscalYear.SelectedValue = "-1" Then
                cvFiscalYear.ErrorMessage = "*Tahun Fiscal harus dipilih"
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            cvFiscalYear.ErrorMessage = "*" + ex.Message
            args.IsValid = False
        End Try
    End Sub

    Protected Sub cvTipeKelas_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If ddlTipeKelas.SelectedValue = "-1" Then
                cvTipeKelas.ErrorMessage = "*Tipe Kelas harus dipilih"
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            cvTipeKelas.ErrorMessage = "*" + ex.Message
            args.IsValid = False
        End Try
    End Sub

    Private Sub btnTriggerKategori_Click(sender As Object, e As EventArgs) Handles btnTriggerKategori.Click
        Try
            Dim existingClass As TrClass = GetLastClassByCategory(txtKodeKategori.Text.Trim())
            DataClassToUi(existingClass)
        Catch ex As Exception
            MessageBox.Show("Tidak ditemukan kelas berdasar kategori " & txtKodeKategori.Text.Trim())
        End Try
    End Sub

    Private Function GetLastClassByCategory(ByVal courseCategoryCode As String) As TrClass

        Dim courseData As TrCourse = New TrCourseFacade(User).Retrieve(courseCategoryCode)

        Dim criteriasClass As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasClass.opAnd(New Criteria(GetType(TrClass), "TrCourse.ID", MatchType.Exact, courseData.ID))

        Dim arrClass As ArrayList = New TrClassFacade(User).RetrieveActiveList(criteriasClass, "FinishDate", Sort.SortDirection.DESC)
        Dim classData As New TrClass
        If arrClass.Count > 0 Then
            classData = arrClass(0)
        End If

        Return classData
    End Function

    Private Sub DataClassToUi(existingClass As TrClass)
        'lblKodeKelas.Text = existingClass.ClassCode
        'lblNamaKelas.Text = existingClass.ClassName
        If existingClass.ID <> 0 Then
            txtNamaKelas.Text = existingClass.ClassName
            txtLocationName.Text = existingClass.Location
        End If
    End Sub

    Private Function GenerateClassCode() As String
        Dim result As String = String.Empty

        Return result
    End Function


    'Private Sub txtKodeKategori_TextChanged(sender As Object, e As EventArgs) Handles txtKodeKategori.TextChanged
    '    btnTriggerKategori_Click(Nothing, Nothing)
    'End Sub

    'Private Sub ddlTipeKelas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipeKelas.SelectedIndexChanged
    '    Try
    '        lblKodeKelas.Text = New TrAdditionalClassFacade(User).GetClassCode(ddlTipeKelas.SelectedValue)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Function IsListFileValid(ByRef errMessage As String) As Boolean
        If listSrc.PostedFile.FileName = String.Empty Then
            errMessage = "Tidak ada file yang diupload"
            Return False
        End If

        Dim extension As String = System.IO.Path.GetExtension(listSrc.PostedFile.FileName)

        If extension <> ".xls" And extension <> ".xlsx" Then
            errMessage = "Hanya file excel yang bisa diupload"
            Return False
        End If

        Return True

    End Function

End Class
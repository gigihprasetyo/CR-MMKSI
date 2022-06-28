Imports System.Text
Imports System.IO

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


Public Class FrmTrAdditionalClass
    Inherits System.Web.UI.Page

    Dim sessionHelper As SessionHelper = New SessionHelper
    Dim rowNumber As Integer = 0
    Dim totalRow As Integer = 0
    Dim helpers As New TrainingHelpers(Me.Page, "Training After Sales - Kelas Inhouse dan Fleet")
    Dim sessPage As String = "SessAdditionalClass"
    Private Const URL_REGISTER As String = "FrmCreateTrAdditionalClass.aspx?id={0}"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditAdditionalKelas_Privilege)
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.view, SR.TrainingAssViewAdditionalKelas_Privilege)
        helpers.Privilage()
        Try
            If Not Page.IsPostBack Then
                InitUi()
                InitUiByLoginType()
                BindDataGridList(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub InitUi()
        BindDropDown()
        SetViewState()
    End Sub

    Private Sub BindDropDown()
        BindDdlTipeKelas()
        BindDdlStatus()
        BindDDLTahunFiskal()
    End Sub

    Private Sub SetViewState()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
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
    End Sub

    Private Sub BindDdlTipeKelas()
        ddlTipeKelas.ClearSelection()
        ddlTipeKelas.Items.Clear()
        ddlTipeKelas.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        ddlTipeKelas.Items.Add(New ListItem(EnumTrClass.GetStringValueClassType(EnumTrClass.EnumClassType.INHOUSE_TRAINING), EnumTrClass.EnumClassType.INHOUSE_TRAINING))
        ddlTipeKelas.Items.Add(New ListItem(EnumTrClass.GetStringValueClassType(EnumTrClass.EnumClassType.FLEET_TRAINING), EnumTrClass.EnumClassType.FLEET_TRAINING))
    End Sub

    Private Sub BindDdlStatus()
        ddlStatus.ClearSelection()
        ddlStatus.Items.Clear()
        ddlStatus.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        ddlStatus.Items.Add(New ListItem(EnumTrAdditionalClass.GetStringValueClassType(EnumTrAdditionalClass.EnumStatus.REQUEST), EnumTrAdditionalClass.EnumStatus.REQUEST))
        ddlStatus.Items.Add(New ListItem(EnumTrAdditionalClass.GetStringValueClassType(EnumTrAdditionalClass.EnumStatus.REVISI), EnumTrAdditionalClass.EnumStatus.REVISI))
        ddlStatus.Items.Add(New ListItem(EnumTrAdditionalClass.GetStringValueClassType(EnumTrAdditionalClass.EnumStatus.APPROVE), EnumTrAdditionalClass.EnumStatus.APPROVE))
    End Sub

    Private Sub InitUiByLoginType()
        Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
            divKodeDealer.Visible = True
            divUpload.Visible = False
            lbtnDownloadTemplate.Visible = False
            btnBaru.Visible = False
        ElseIf objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            divKodeDealer.Visible = False
            divUpload.Visible = helpers.IsEdit
            lbtnDownloadTemplate.Visible = helpers.IsEdit
            btnBaru.Visible = helpers.IsEdit
            txtKodeDealer.Text = objDealer.DealerCode
        End If
    End Sub

    Private Sub BindDataGridList(ByVal page As Integer)
        rowNumber = 0
        Dim totalRow As Integer = 0

        Dim criteria As CriteriaComposite = CreateCriteria()


        dgList.DataSource = New TrAdditionalClassFacade(User).RetrieveActiveList(criteria, page + 1, dgList.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgList.VirtualItemCount = totalRow

        If totalRow > 0 Then
            lblRowTotal.Text = totalRow
            sessionHelper.SetSession("trAdditionalClassCrit", criteria)
            btnDownload.Visible = True
        Else
            lblRowTotal.Text = 0
            btnDownload.Visible = False
        End If

        dgList.DataBind()
    End Sub

    Private Const EmptyDate As String = "01/01/0001 0:00:00"

    Private Function CreateCriteria() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrAdditionalClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtKodeKategori.Text <> String.Empty Then
            Dim kategori As String = New System.Text.StringBuilder("('").Append(txtKodeKategori.Text).Append("')").ToString().Replace(";", "','")
            criterias.opAnd(New Criteria(GetType(TrAdditionalClass), "TrCourse.CourseCode", MatchType.InSet, kategori))
        End If

        If txtLokasi.Text.Trim() <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(TrAdditionalClass), "LocationName", MatchType.Partial, txtLokasi.Text.Trim()))
        End If


        If txtKodeDealer.Text <> String.Empty Then
            Dim dealers As String = New System.Text.StringBuilder("('").Append(txtKodeDealer.Text).Append("')").ToString().Replace(";", "','")
            criterias.opAnd(New Criteria(GetType(TrAdditionalClass), "Dealer.DealerCode", MatchType.InSet, dealers))
        End If

        If txtKodeKelas.Text.Trim() <> String.Empty Then
            Dim classes As String = New System.Text.StringBuilder("('").Append(txtKodeKelas.Text).Append("')").ToString().Replace(";", "','")
            criterias.opAnd(New Criteria(GetType(TrAdditionalClass), "ClassCode", MatchType.InSet, classes))
        End If

        If ddlTipeKelas.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(TrAdditionalClass), "ClassType", MatchType.Exact, ddlTipeKelas.SelectedValue))
        End If

        If ddlStatus.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(TrAdditionalClass), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        If ddlFiscalYear.SelectedValue.ToString() <> "-1" Then
            criterias.opAnd(New Criteria(GetType(TrAdditionalClass), "FiscalYear", MatchType.Exact, ddlFiscalYear.SelectedValue))
        End If


        'If chkTanggalMulai.Checked Then
        If ICTanggalMulaiFrom.Value.ToString() <> EmptyDate Or ICTanggalMulaiTo.Value.ToString <> EmptyDate Then
            ValidateDateMulai()
            criterias.opAnd(New Criteria(GetType(TrAdditionalClass), "StartDate", MatchType.GreaterOrEqual, ICTanggalMulaiFrom.Value))
            criterias.opAnd(New Criteria(GetType(TrAdditionalClass), "StartDate", MatchType.LesserOrEqual, ICTanggalMulaiTo.Value.AddDays(1)))
        End If

        ' If chkTanggalSelesai.Checked Then
        If ICTanggalSelesaiFrom.Value.ToString() <> EmptyDate Or ICTanggalSelesaiTo.Value.ToString() <> EmptyDate Then
            ValidateDateSelesai()
            criterias.opAnd(New Criteria(GetType(TrAdditionalClass), "FinishDate", MatchType.GreaterOrEqual, ICTanggalSelesaiFrom.Value))
            criterias.opAnd(New Criteria(GetType(TrAdditionalClass), "FinishDate", MatchType.LesserOrEqual, ICTanggalSelesaiTo.Value.AddDays(1)))
        End If

        CreateSessionCriteria()

        Return criterias
    End Function

    Private Sub ValidateDateMulai()
        Try

            If ICTanggalMulaiFrom.Value.ToString() = EmptyDate Or ICTanggalMulaiTo.Value.ToString = EmptyDate Then
                Throw New Exception("")
            End If

            If Not ICTanggalMulaiFrom.Value <= ICTanggalMulaiTo.Value Then
                Throw New Exception("")
            End If
        Catch ex As Exception
            Throw New Exception("Tanggal mulai dari harus lebih kecil dari tanggal mulai sampai")
        End Try
    End Sub

    Private Sub ValidateDateSelesai()
        Try

            If ICTanggalSelesaiFrom.Value.ToString() = EmptyDate Or ICTanggalSelesaiTo.Value.ToString = EmptyDate Then
                Throw New Exception("")
            End If

            If Not ICTanggalSelesaiFrom.Value <= ICTanggalSelesaiTo.Value Then
                Throw New Exception("")
            End If
        Catch ex As Exception
            Throw New Exception("Tanggal selesai dari harus lebih kecil dari tanggal mulai sampai")
        End Try
    End Sub

    Private Sub CreateSessionCriteria()
        Dim lastSession As Dictionary(Of String, String) = New Dictionary(Of String, String)

        lastSession("txtKodeKategori") = txtKodeKategori.Text
        lastSession("txtLokasi") = txtLokasi.Text
        'lastSession("chkTanggalMulai") = chkTanggalMulai.Checked
        ' lastSession("chkTanggalSelesai") = chkTanggalSelesai.Checked

        'If chkTanggalMulai.Checked Then
        If ICTanggalMulaiFrom.Text <> String.Empty And ICTanggalMulaiTo.Text <> String.Empty Then
            lastSession("ICTanggalMulaiFrom") = ICTanggalMulaiFrom.Value
            lastSession("ICTanggalMulaiTo") = ICTanggalMulaiTo.Value
        End If

        'If chkTanggalSelesai.Checked Then
        If ICTanggalSelesaiFrom.Text <> String.Empty And ICTanggalSelesaiTo.Text <> String.Empty Then
            lastSession("ICTanggalSelesaiFrom") = ICTanggalSelesaiFrom.Value
            lastSession("ICTanggalSelesaiTo") = ICTanggalSelesaiTo.Value
        End If

        lastSession("txtKodeKelas") = txtKodeKelas.Text
        lastSession("ddlTipeKelas") = ddlTipeKelas.SelectedValue
        lastSession("ddlStatus") = ddlStatus.SelectedValue

        sessionHelper.SetSession(sessPage, lastSession)

    End Sub

    Private Sub lbtnDownloadTemplate_Click(sender As Object, e As EventArgs) Handles lbtnDownloadTemplate.Click
        Try
            DownloadTemplate()
        Catch ex As Exception
            MessageBox.Show("Error dalam download template ")
        End Try
    End Sub

    Private Sub DownloadTemplate()
        Dim template As ExcelTemplate = New ExcelTemplate(Me.Page)
        template.FileName = "TemplateUploadInhouseClass.xls"
        template.SheetName = "UploadKelasInhouse"
        template.Judul = "Upload Kelas Inhouse dan Fleet"
        template.AddField(1, "No")
        template.AddField(2, "Kode Kategori")
        template.AddField(3, "Kode Kelas")
        template.AddField(4, "Nama Kelas")

        Dim dataTipe As ExcelTemplateColumn = New ExcelTemplateColumn(5, "Tipe kelas", EnumTypeCell.Dropdownlist)
        Dim list As List(Of String) = New List(Of String)
        list.Add("INHOUSE TRAINING")
        list.Add("FLEET TRAINING")
        dataTipe.DataValidation = list
        template.AddField(dataTipe)

        template.AddField(6, "Kota")
        template.AddField(7, "Nama Lokasi")
        template.AddField(8, "Alamat")
        template.AddField(9, "Pengajar 1")
        template.AddField(10, "Pengajar 2")
        template.AddField(11, "Pengajar 3")
        template.AddField(12, "Keterangan")
        template.AddField(13, "Tanggal Mulai")
        template.AddField(14, "Tanggal Selesai")



        Dim dataKota As ExcelSheetData = New ExcelSheetData()
        dataKota.SheetName = "Data Kota"
        dataKota.Judul = "Data Kota"
        dataKota.ColumnCode = "Kode Kota"
        dataKota.ColumnName = "Nama Kota"
        Dim dicKota As Dictionary(Of String, String) = New Dictionary(Of String, String)
        Dim dataKotas As ArrayList = New CityFacade(User).RetrieveActiveList()
        For Each item As City In dataKotas
            dicKota.Add(item.CityCode, item.CityName)
        Next
        dataKota.AddData(dicKota)
        template.AddSheet(dataKota)
        template.DownLoad()
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            dgList.DataSource = Nothing
            dgList.Visible = False
            btnDownload.Visible = False

            rowUpload.Visible = True
            setUploadControl(True)
            UploadFile()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub setUploadControl(ByVal IsUploading As Boolean)


    End Sub


    Private Sub UploadFile()
        Dim fileName As String = String.Empty
        Dim listTrClass As List(Of TrAdditionalClass) = New List(Of TrAdditionalClass)
        Dim listError As List(Of ErrorExcelUpload) = New List(Of ErrorExcelUpload)
        Dim resultUpload As String = helpers.UploadFile(fileUpload, KTB.DNet.Lib.WebConfig.GetValue("AdditionalClassUpload"), _
                                                    KTB.DNet.Lib.WebConfig.GetValue("MaximumAdditionalClassUploadSize"))
        Dim errArr() As String = resultUpload.Split("|")
        If errArr(0).Equals("Error") Then
            Throw New Exception(errArr(1))
        Else
            fileName = KTB.DNet.Lib.WebConfig.GetValue("SAN") & errArr(1)
        End If
        Dim fileInfo As FileInfo = New FileInfo(fileName)
        Using excelPkg As New ExcelPackage(fileInfo)
            Using ws As ExcelWorksheet = excelPkg.Workbook.Worksheets.Item(1)
                Dim ColumnCount As Integer = ws.Dimension.End.Column
                Dim RowCount As Integer = ws.Dimension.End.Row
                If ColumnCount < 14 Then
                    MessageBox.Show("Format Tidak Sesuai")
                    Exit Sub
                End If

                Dim DataTipeClass As Dictionary(Of String, String) = New Dictionary(Of String, String)
                DataTipeClass.Add("3", "INHOUSE TRAINING")
                DataTipeClass.Add("4", "FLEET TRAINING")

                Dim listCodeKota As List(Of String) = (New CityFacade(User).RetrieveActiveList().Cast(Of City)( _
                                                       )).Select(Function(x) x.CityCode).ToList()
                Dim listTraining As List(Of String) = New List(Of String)
                Dim listTrainingNum As List(Of Integer) = New List(Of Integer)

                For idx As Integer = 4 To RowCount
                    Dim validasi As ExcelValidation = New ExcelValidation(ws)
                    Dim kodeTraining As ExcelField = validasi.Create("Kode Kategori", idx, 2, "required,max", 20)
                    Dim kodeKelas As ExcelField = validasi.Create("Kode Kelas", idx, 3, "max", 20)
                    Dim Nama As ExcelField = validasi.Create("Nama Kelas", idx, 4, "required,max", 50)
                    Dim TipeKelas As ExcelField = validasi.Create("Tipe kelas", idx, 5, "required")
                    Dim Kota As ExcelField = validasi.Create("Kota", idx, 6, "required,max", 50)
                    Dim NamaLokasi As ExcelField = validasi.Create("Nama Lokasi", idx, 7, "required,max", 100)
                    Dim Alamat As ExcelField = validasi.Create("Alamat", idx, 8, "required,max", 200)
                    Dim Pengajar1 As ExcelField = validasi.Create("Pengajar 1", idx, 9, "required,max", 50)
                    Dim Pengajar2 As ExcelField = validasi.Create("Pengajar 2", idx, 10, "max", 50)
                    Dim Pengajar3 As ExcelField = validasi.Create("Pengajar 3", idx, 11, "max", 50)
                    Dim Keterangan As ExcelField = validasi.Create("Keterangan", idx, 12, "max", 200)
                    Dim TanggalMasuk As ExcelField = validasi.Create("Tanggal Masuk", idx, 13, "required,date")
                    Dim TanggalKeluar As ExcelField = validasi.Create("Tanggal Keluar", idx, 14, "required,date")

                    'Validasi Requirment Value
                    Dim listErrorfield As List(Of ErrorExcelUpload) = validasi.Validate()
                    If Not listErrorfield.Count.Equals(0) Then
                        listError.AddRange(listErrorfield)
                        Continue For
                    End If

                    If TanggalMasuk.Value.StringCellToDateTime() > TanggalKeluar.Value.StringCellToDateTime() Then
                        listErrorfield.Add(validasi.CreateCustomError(TanggalMasuk, "lebih besar dari Tanggal Keluar"))
                    End If

                    If Not DataTipeClass.ContainsValue(TipeKelas.Value.ToUpper()) Then
                        listErrorfield.Add(validasi.CreateCustomError(TipeKelas, "harus berisi INHOUSE TRAINING atau FLEET TRAINING"))
                    End If

                    If Not listCodeKota.Contains(Kota.Value.ToUpper()) Then
                        listErrorfield.Add(validasi.CreateCustomError(Kota, "Kode Kota tidak ditemukan", False))
                    End If

                    Dim objCourse As TrCourse = New TrCourseFacade(User).Retrieve(kodeTraining.Value)
                    If objCourse.ID.Equals(0) Then
                        listErrorfield.Add(validasi.CreateCustomError(kodeTraining, "tidak ditemukan."))
                    Else
                        If objCourse.JobPositionCategory Is Nothing Then
                            listErrorfield.Add(validasi.CreateCustomError(kodeTraining, "tidak ditemukan."))
                            'Else
                            '    If Not objCourse.JobPositionCategory.ID.Equals(1) Then
                            '        listErrorfield.Add(validasi.CreateCustomError(kodeTraining, "tidak ditemukan."))
                            '    End If
                        End If
                    End If

                    Dim existingData As TrAdditionalClass = New TrAdditionalClassFacade(User).Retrieve(kodeKelas.Value)
                    If existingData.ID <> 0 Then
                        listErrorfield.Add(validasi.CreateCustomError(kodeKelas, "sudah terdaftar."))
                    End If


                    If Not listErrorfield.Count.Equals(0) Then
                        listError.AddRange(listErrorfield)
                        Continue For
                    End If

                    If listError.Count.Equals(0) Then
                        Dim objClass As TrAdditionalClass = New TrAdditionalClass()
                        objClass.TrCourse = objCourse
                        objClass.ClassCode = String.Empty
                        objClass.ClassName = Nama.Value
                        objClass.ClassType = CType(DataTipeClass.FirstOrDefault(Function(x) _
                                             x.Value = TipeKelas.Value.ToUpper()).Key, Short)
                        objClass.Description = Keterangan.Value
                        objClass.Trainer1 = Pengajar1.Value
                        objClass.Trainer2 = Pengajar2.Value
                        objClass.Trainer3 = Pengajar3.Value
                        objClass.FinishDate = TanggalKeluar.Value.StringCellToDateTime()
                        objClass.StartDate = TanggalMasuk.Value.StringCellToDateTime()
                        objClass.Status = EnumTrAdditionalClass.EnumStatus.REQUEST
                        objClass.Location = Alamat.Value
                        objClass.LocationName = NamaLokasi.Value
                        objClass.City = New CityFacade(User).Retrieve(Kota.Value)
                        objClass.Dealer = CType(Session("Dealer"), Dealer)

                        listTrClass.Add(objClass)

                    End If

                Next
                If Not listError.Count.Equals(0) Then
                    helpers.SetSession("namaFile", fileUpload.PostedFile.FileName)
                    helpers.SetSession("dataError", listError)
                    Me.Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "document.getElementById('btnShowPopup').click();", True)
                Else
                    If helpers.GetSession("namaFile") IsNot Nothing Then
                        helpers.RemoveSession("namaFile")
                    End If
                    If helpers.GetSession("dataError") IsNot Nothing Then
                        helpers.RemoveSession("dataError")
                    End If
                    helpers.SetSession("dataUpload", listTrClass)

                    dgUpload.DataSource = listTrClass
                    dgUpload.VirtualItemCount = listTrClass.Count
                    dgUpload.DataBind()
                    'dgUpload.Columns(12).Visible = False
                    dgUpload.Enabled = True
                End If

            End Using
        End Using
    End Sub


    Private Sub dgUpload_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgUpload.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            Dim objCFac As TrAdditionalClassFacade = New TrAdditionalClassFacade(User)
            Dim objC As TrAdditionalClass
            Dim arlCA As ArrayList = New ArrayList
            Dim strError As String = String.Empty

            Dim lblUNo As Label = e.Item.FindControl("lblUNo")
            Dim lblUClassCode As Label = e.Item.FindControl("lblUClassCode")
            Dim lblUClassName As Label = e.Item.FindControl("lblUClassName")
            Dim lblUCategory As Label = e.Item.FindControl("lblUCategory")
            Dim lblULocation As Label = e.Item.FindControl("lblULocation")
            Dim lblUTrainer1 As Label = e.Item.FindControl("lblUTrainer1")
            Dim lblUStartDate As Label = e.Item.FindControl("lblUStartDate")
            Dim lblUEndDate As Label = e.Item.FindControl("lblUEndDate")
            Dim lblUCapacity As Label = e.Item.FindControl("lblUCapacity")
            Dim lblURemain As Label = e.Item.FindControl("lblURemain")
            Dim lblUMessage As Label = e.Item.FindControl("lblUMessage")
            Dim lblUTipeKelas As Label = e.Item.FindControl("lblUTipeKelas")

            Dim i As Integer = 0

            lblUNo.Text = e.Item.ItemIndex + 1
            objC = CType(e.Item.DataItem, TrAdditionalClass)

            'trClass Data
            If objC Is Nothing Then
                lblUClassCode.Text = "-"
                lblUClassName.Text = "-"
                lblUCategory.Text = "-"
                lblULocation.Text = "-"
                lblUTrainer1.Text = "-"
                lblUStartDate.Text = "-"
                lblUEndDate.Text = "-"
                lblUCapacity.Text = "-"
                lblURemain.Text = "-"
                lblUMessage.Text = "Data kosong"
                If objC.ErrorMessage <> "" Then
                    e.Item.BackColor = System.Drawing.Color.Red
                End If
            Else
                lblUClassCode.Text = objC.ClassCode
                lblUClassName.Text = objC.ClassName
                If Not IsNothing(objC.TrCourse) Then
                    lblUCategory.Text = objC.TrCourse.CourseCode
                Else
                    lblUCategory.Text = String.Empty
                End If

                lblULocation.Text = objC.Location
                lblUTrainer1.Text = objC.Trainer1
                lblUStartDate.Text = objC.StartDate
                lblUEndDate.Text = objC.FinishDate
                lblUMessage.Text = objC.ErrorMessage

                If objC.ErrorMessage <> "" Then
                    e.Item.BackColor = System.Drawing.Color.Red
                End If

            End If

            If lblUMessage.Text.Trim <> String.Empty Then
                'Me.btnSimpan.Enabled = False
            End If
            If Not objC.ClassType.Equals(0) Then
                lblUTipeKelas.Text = EnumTrClass.GetStringValueClassType(objC.ClassType.ToString())
            End If

        End If
    End Sub

    Private Sub btnBaru_Click(sender As Object, e As EventArgs) Handles btnBaru.Click
        Response.Redirect("FrmTrAdditionalClassDetail.aspx?id=0")
    End Sub

    Private Sub dgList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgList.ItemCommand
        Try
            If (e.CommandName = "View") Then
                Response.Redirect("FrmTrAdditionalClassDetail.aspx?mode=view&id=" & e.Item.Cells(0).Text)
            ElseIf e.CommandName = "Edit" Then
                Response.Redirect("FrmTrAdditionalClassDetail.aspx?mode=edit&id=" & e.Item.Cells(0).Text)
            ElseIf e.CommandName = "Delete" Then
                DeleteTrAdditional(e.Item.Cells(0).Text)
            ElseIf e.CommandName = "DownloadMateri" Then
                DownloadMateri(CInt(e.Item.Cells(0).Text))
            ElseIf e.CommandName = "DownloadList" Then
                DownloadList(CInt(e.Item.Cells(0).Text))
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub DownloadMateri(ByVal classID As Integer)
        Try
            Dim classObj As TrAdditionalClass = New TrAdditionalClassFacade(User).Retrieve(classID)
            helpers.DownloadFile(classObj.FileMateriPath, classObj.FileName)
        Catch ex As Exception
            MessageBox.Show("Data materi tidak ditemukan")
        End Try
    End Sub

    Private Sub DownloadList(ByVal classID As Integer)
        Try
            Dim classObj As TrAdditionalClass = New TrAdditionalClassFacade(User).Retrieve(classID)
            helpers.DownloadFile(classObj.FileSiswaPath, "List Siswa " & classObj.ClassCode)
        Catch ex As Exception
            MessageBox.Show("Data materi tidak ditemukan")
        End Try
    End Sub

    Private Sub DeleteTrAdditional(ByVal id As Integer)
        Dim facade As TrAdditionalClassFacade = New TrAdditionalClassFacade(User)
        Dim obj As TrAdditionalClass = facade.Retrieve(id)
        If obj.ID <> 0 Then
            obj.RowStatus = CType(DBRowStatus.Deleted, Short)
            facade.Update(obj)
            dgList.CurrentPageIndex = 0
            BindDataGridList(dgList.CurrentPageIndex)
        End If
    End Sub

    Private Sub dgList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgList.ItemDataBound

        Dim RowValue As TrAdditionalClass
        If Not e.Item.DataItem Is Nothing Then
            RowValue = CType(e.Item.DataItem, TrAdditionalClass)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgList.CurrentPageIndex * dgList.PageSize)

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            lblStatus.Text = EnumTrAdditionalClass.GetStringValueClassType(RowValue.Status)

            Dim lblTipeClass As Label = CType(e.Item.FindControl("lblTipeKelas"), Label)
            If Not String.IsNullOrEmpty(RowValue.ClassType) Then
                lblTipeClass.Text = EnumTrClass.GetStringValueClassType(RowValue.ClassType)
            End If


            Dim lnkDownloadFile As LinkButton = CType(e.Item.FindControl("lnkDownloadFile"), LinkButton)
            Dim lnkDownloadList As LinkButton = CType(e.Item.FindControl("lnkDownloadList"), LinkButton)

            If RowValue.FileMateriPath <> String.Empty Then
                lnkDownloadFile.Visible = True
            Else
                lnkDownloadFile.Visible = False
            End If

            If RowValue.FileSiswaPath <> String.Empty Then
                lnkDownloadList.Visible = True
            Else
                lnkDownloadList.Visible = False
            End If


            Dim btnUbah As LinkButton = CType(e.Item.FindControl("btnUbah"), LinkButton)
            Dim btnHapus As LinkButton = CType(e.Item.FindControl("btnHapus"), LinkButton)
            btnHapus.Attributes.Add("OnClick", "return confirm('Yakin data ini akan dihapus?');")
            If RowValue.Status = EnumTrAdditionalClass.EnumStatus.APPROVE Or Not helpers.IsEdit Then
                btnUbah.Visible = False
                btnHapus.Visible = False
            End If

        End If
    End Sub

    Private Sub dgList_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgList.PageIndexChanged
        Try
            dgList.SelectedIndex = -1
            dgList.CurrentPageIndex = e.NewPageIndex
            BindDataGridList(dgList.CurrentPageIndex)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub dgList_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgList.SortCommand
        Try
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
            dgList.SelectedIndex = -1
            dgList.CurrentPageIndex = 0
            BindDataGridList(dgList.CurrentPageIndex)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Try
            dgUpload.DataSource = Nothing
            dgUpload.Visible = False
            dgList.Visible = True
            dgList.CurrentPageIndex = 0
            BindDataGridList(0)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try



    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim arlClass As List(Of TrAdditionalClass) = CType(helpers.GetSession("dataUpload"), List(Of TrAdditionalClass))
        Dim arlClassNew As New ArrayList
        If arlClass.Count > 0 Then
            For Each oClass As TrAdditionalClass In arlClass
                Dim iReturn As Integer
                iReturn = InsertClass(oClass)
                If iReturn > 0 Then
                    oClass.ID = iReturn
                End If
            Next
            helpers.SetSession("dataUpload", arlClass)
            MessageBox.Show("Data berhasil disimpan")
            'DataBindUpload()
        End If
        setUploadControl(False)
    End Sub

    Private Function InsertClass(ByVal objTrClass As TrAdditionalClass) As Integer
        Return New TrAdditionalClassFacade(User).InsertTransaction(objTrClass)
    End Function

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Try
            Response.Redirect("./FrmDownloadTrAdditionalClass.aspx")
        Catch ex As Exception
            MessageBox.Show("Gagal")
        End Try
    End Sub
End Class
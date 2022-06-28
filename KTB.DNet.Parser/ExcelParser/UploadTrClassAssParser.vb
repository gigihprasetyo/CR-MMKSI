Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports System.IO
Imports OfficeOpenXml
Imports System.Collections.Generic
Imports System.Linq
Imports System.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports System.Security.Principal
Imports System.Text
Imports KTB.DNet.Domain.Search

Namespace KTB.DNet.Parser

    Public Class UploadTrClassAssParser
        Inherits AbstractExcelParser

        Private m_userPrincipal As IPrincipal = Nothing
        Private excelValidation As ExcelValidation = New ExcelValidation()
        Private m_page As System.Web.UI.Page

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
        End Sub

        Public Sub New(ByVal userPrincipal As IPrincipal, ByVal page As System.Web.UI.Page)
            Me.m_userPrincipal = userPrincipal
            Me.m_page = page
        End Sub

        Protected Overrides Function ParsingExcelNoTransaction(fileName As String, sheetName As String, user As String) As Object
            Dim listTrClass As List(Of TrClass) = New List(Of TrClass)
            Dim listError As List(Of ErrorExcelUpload) = New List(Of ErrorExcelUpload)
            ParseFile(fileName, listTrClass, listError)

            If listError.Count = 0 Then
                SaveData(listTrClass)
            End If

            Return listError

        End Function

        Private Sub ParseFile(ByVal fileName As String, ByRef listTrClass As List(Of TrClass), ByRef listError As List(Of ErrorExcelUpload))
            Dim fileInfo As FileInfo = New FileInfo(fileName)
            Using excelPkg As New ExcelPackage(fileInfo)
                Using ws As ExcelWorksheet = excelPkg.Workbook.Worksheets.Item(1)
                    Dim ColumnCount As Integer = ws.Dimension.End.Column
                    Dim RowCount As Integer = ws.Dimension.End.Row
                    If ColumnCount < 17 Then
                        Throw New Exception("Format file tidak sesuai")
                        'MessageBox.Show("Format Tidak Sesuai")
                        'Exit Sub
                    End If

                    Dim DataTipeClass As Dictionary(Of String, String) = New Dictionary(Of String, String)
                    DataTipeClass.Add("1", "INCLASS TRAINING")
                    DataTipeClass.Add("2", "E-LEARNING")
                    DataTipeClass.Add("3", "INHOUSE TRAINING")
                    DataTipeClass.Add("4", "FLEET_TRAINING")

                    Dim listCodeKota As List(Of String) = (New CityFacade(m_userPrincipal).RetrieveActiveList().Cast(Of City)( _
                                                           )).Select(Function(x) x.CityCode).ToList()
                    Dim listTahunFiskal As List(Of String) = GetTahunFiskal()
                    Dim listTraining As List(Of String) = New List(Of String)
                    Dim listTrainingNum As List(Of Integer) = New List(Of Integer)

                    For idx As Integer = 4 To RowCount
                        Dim validasi As ExcelValidation = New ExcelValidation(ws)
                        Dim kodeTraining As ExcelField = validasi.Create("Kode Kategori", idx, 2, "required,max", 20)
                        Dim Nama As ExcelField = validasi.Create("Nama", idx, 3, "required,max", 50)
                        Dim TipeKelas As ExcelField = validasi.Create("Tipe kelas", idx, 4, "required")
                        Dim KodeMRTC As ExcelField = validasi.Create("Kode MRTC", idx, 5, "")
                        Dim Kota As ExcelField = validasi.Create("Kota", idx, 6, "required,max", 50)
                        Dim NamaLokasi As ExcelField = validasi.Create("Nama Lokasi", idx, 7, "required,max", 100)
                        Dim Alamat As ExcelField = validasi.Create("Alamat", idx, 8, "required,max", 200)
                        Dim Penginapan As ExcelField = validasi.Create("Penginapan", idx, 9, "max", 200)
                        Dim Pengajar1 As ExcelField = validasi.Create("Pengajar 1", idx, 10, "required,max", 50)
                        Dim Pengajar2 As ExcelField = validasi.Create("Pengajar 2", idx, 11, "max", 50)
                        Dim Pengajar3 As ExcelField = validasi.Create("Pengajar 3", idx, 12, "max", 50)
                        Dim Kapasitas As ExcelField = validasi.Create("Kapasitas", idx, 13, "required,numeric")
                        Dim TahunFiscal As ExcelField = validasi.Create("Tahun Fiskal", idx, 14, "required")
                        Dim Status As ExcelField = validasi.Create("Status", idx, 15, "required")
                        Dim Keterangan As ExcelField = validasi.Create("Keterangan", idx, 16, "max", 200)
                        Dim TanggalMasuk As ExcelField = validasi.Create("Tanggal Masuk", idx, 17, "required,date")
                        Dim TanggalKeluar As ExcelField = validasi.Create("Tanggal Keluar", idx, 18, "required,date")
                        Dim JmlHariBerbayar As ExcelField = validasi.Create("Jumlah Hari Berbayar", idx, 19, 2)

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
                            listErrorfield.Add(validasi.CreateCustomError(TipeKelas, "harus berisi INHOUSE TRAINING, E-LEARNING atau FACE TO FACE"))
                        End If

                        If Not listCodeKota.Contains(Kota.Value.ToUpper()) Then
                            listErrorfield.Add(validasi.CreateCustomError(Kota, "Kode Kota tidak ditemukan", False))
                        End If

                        If Not listTahunFiskal.Contains(TahunFiscal.Value) Then
                            listErrorfield.Add(validasi.CreateCustomError(TahunFiscal, "Tahun fiskal sudah tidak valid atau format tidak benar", False))
                        End If

                        If Not (Status.Value.Contains("Aktif") Or Status.Value.Contains("Tidak Aktif")) Then
                            listErrorfield.Add(validasi.CreateCustomError(Status, "harus berisi Aktif atau Tidak Aktif"))
                        End If

                        Dim objCourse As TrCourse = New TrCourseFacade(m_userPrincipal).Retrieve(kodeTraining.Value)
                        If objCourse.ID.Equals(0) Then
                            listErrorfield.Add(validasi.CreateCustomError(kodeTraining, "tidak ditemukan."))
                        Else
                            If objCourse.JobPositionCategory Is Nothing Then
                                listErrorfield.Add(validasi.CreateCustomError(kodeTraining, "tidak ditemukan."))
                            Else
                                Dim jp As JobPositionCategory = objCourse.JobPositionCategory
                                If jp.JobPositionCategoryArea.ID <> 2 Then
                                    listErrorfield.Add(validasi.CreateCustomError(kodeTraining, "tidak ditemukan."))
                                End If
                            End If
                        End If

                        Dim objMRTC As New TrMRTC

                        If Not KodeMRTC.Value = String.Empty Then
                            objMRTC = New TrMRTCFacade(m_userPrincipal).Retrieve(KodeMRTC.Value)
                            If objMRTC.ID = 0 Then
                                listErrorfield.Add(validasi.CreateCustomError(KodeMRTC, "tidak ditemukan."))
                            End If
                        End If

                        If objCourse.PaymentType = EnumTrCourse.PaymentType.CHARGE Then
                            If Not IsNumeric(JmlHariBerbayar.Value) Then
                                listErrorfield.Add(validasi.CreateCustomError(JmlHariBerbayar, "tidak valid."))
                            Else
                                If CInt(JmlHariBerbayar.Value < 0) Then
                                    listErrorfield.Add(validasi.CreateCustomError(JmlHariBerbayar, "tidak bisa minus."))
                                End If

                                If CInt(JmlHariBerbayar.Value) > DateDiff(DateInterval.Day, TanggalMasuk.Value.StringCellToDateTime(), TanggalKeluar.Value.StringCellToDateTime()) Then
                                    listErrorfield.Add(validasi.CreateCustomError(JmlHariBerbayar, "tidak bisa melebihi jumlah hari training."))
                                End If

                            End If
                        End If

                        If Not listErrorfield.Count.Equals(0) Then
                            listError.AddRange(listErrorfield)
                            Continue For
                        End If

                        If listError.Count.Equals(0) Then
                            Dim objClass As TrClass = New TrClass()
                            objClass.TrCourse = objCourse
                            objClass.ClassName = Nama.Value
                            objClass.ClassType = CType(DataTipeClass.FirstOrDefault(Function(x) _
                                                 x.Value = TipeKelas.Value.ToUpper()).Key, Short)



                            If objMRTC.ID <> 0 Then
                                objClass.TrMRTC = objMRTC
                                objClass.Location = objMRTC.Address
                                objClass.LocationName = objMRTC.Name
                                objClass.City = objMRTC.City

                                If objCourse.PaymentType = EnumTrCourse.PaymentType.CHARGE Then
                                    objClass.PaidDay = CInt(JmlHariBerbayar.Value)
                                    objClass.PricePerDay = objMRTC.PricePerDay
                                    objClass.PriceTotal = objClass.PaidDay * objClass.PricePerDay
                                End If

                                For i As Integer = 0 To objMRTC.ListOfDetail.Count - 1
                                    Dim pic As TrMRTCPIC = CType(objMRTC.ListOfDetail(i), TrMRTCPIC)

                                    If i = 0 Then
                                        objClass.Trainer1 = pic.TrTrainee.Name
                                    End If

                                    If i = 1 Then
                                        objClass.Trainer2 = pic.TrTrainee.Name
                                    End If

                                    If i = 2 Then
                                        objClass.Trainer3 = pic.TrTrainee.Name
                                    End If

                                Next

                            Else
                                objClass.Trainer1 = Pengajar1.Value
                                objClass.Trainer2 = Pengajar2.Value
                                objClass.Trainer3 = Pengajar3.Value
                                objClass.Location = Alamat.Value
                                objClass.LocationName = NamaLokasi.Value
                                objClass.City = New CityFacade(m_userPrincipal).Retrieve(Kota.Value)
                            End If
                            objClass.Lodging = Penginapan.Value
                            objClass.Description = Keterangan.Value
                            objClass.FiscalYear = TahunFiscal.Value

                            objClass.FinishDate = TanggalKeluar.Value.StringCellToDateTime()
                            objClass.StartDate = TanggalMasuk.Value.StringCellToDateTime()
                            objClass.Status = IIf(Status.Value.Equals("Aktif"), "1", "0")
                            objClass.Capacity = Integer.Parse(Kapasitas.Value)



                            If listTraining.Count.Equals(0) Then
                                objClass.ClassCode = GenerateCodeClass(kodeTraining.Value)
                                Dim number As Integer = Integer.Parse(objClass.ClassCode.Replace(objCourse.ClassCode.ToUpper() + _
                                        DateTime.Now.Year.ToString().Substring(2, 2), String.Empty))
                                listTraining.Add(kodeTraining.Value)
                                listTrainingNum.Add(number)
                            Else
                                If listTraining.Contains(kodeTraining.Value) Then
                                    Dim index As Integer = listTraining.IndexOf(kodeTraining.Value)
                                    objClass.ClassCode = objCourse.ClassCode.ToUpper() + _
                                        DateTime.Now.Year.ToString().Substring(2, 2) + (listTrainingNum(index) + 1).ToString()
                                    listTrainingNum(index) = listTrainingNum(index) + 1
                                Else
                                    objClass.ClassCode = GenerateCodeClass(kodeTraining.Value)
                                    Dim number As Integer = Integer.Parse(objClass.ClassCode.Replace(objCourse.ClassCode.ToUpper() + _
                                        DateTime.Now.Year.ToString().Substring(2, 2), String.Empty))
                                    listTraining.Add(kodeTraining.Value)
                                    listTrainingNum.Add(number)
                                End If

                            End If
                            listTrClass.Add(objClass)
                        End If
                    Next
                End Using
            End Using
        End Sub

        Public Sub DownloadTemplateAss()
            Dim template As ExcelTemplate = New ExcelTemplate(Me.m_page)
            template.FileName = "TemplateUploadClass.xls"
            template.SheetName = "UploadKelas"
            template.Judul = "Upload Kelas Training"
            template.AddField(1, "No")
            template.AddField(2, "Kode Kategori")
            template.AddField(3, "Nama")
            template.AddField(6, "Kota")
            template.AddField(7, "Nama Lokasi")
            template.AddField(8, "Alamat")
            template.AddField(9, "Penginapan")
            template.AddField(10, "Pengajar 1")
            template.AddField(11, "Pengajar 2")
            template.AddField(12, "Pengajar 3")
            template.AddField(13, "Kapasitas")
            template.AddField(16, "Keterangan")
            template.AddField(17, "Tanggal Mulai")
            template.AddField(18, "Tanggal Selesai")
            template.AddField(5, "Kode MRTC")
            template.AddField(19, "Jumlah hari berbayar")

            Dim dataTipe As ExcelTemplateColumn = New ExcelTemplateColumn(4, "Tipe kelas", EnumTypeCell.Dropdownlist)
            Dim list As List(Of String) = New List(Of String)
            list.Add("INCLASS TRAINING")
            list.Add("INHOUSE TRAINING")
            list.Add("E-LEARNING")
            list.Add("FLEET_TRAINING")
            dataTipe.DataValidation = list
            template.AddField(dataTipe)

            Dim dataTahunfiskal As ExcelTemplateColumn = New ExcelTemplateColumn(13, "Tahun Fiskal", EnumTypeCell.Dropdownlist)
            Dim listTahunfiskal As List(Of String) = GetTahunFiskal()
            dataTahunfiskal.DataValidation = listTahunfiskal
            template.AddField(dataTahunfiskal)

            Dim dataStatus As ExcelTemplateColumn = New ExcelTemplateColumn(14, "Status", EnumTypeCell.Dropdownlist)
            Dim listStatus As List(Of String) = New List(Of String)
            listStatus.Add("Aktif")
            listStatus.Add("Tidak Aktif")
            dataStatus.DataValidation = listStatus
            template.AddField(dataStatus)

            Dim dataKota As ExcelSheetData = New ExcelSheetData()
            dataKota.SheetName = "Data Kota"
            dataKota.Judul = "Data Kota"
            dataKota.ColumnCode = "Kode Kota"
            dataKota.ColumnName = "Nama Kota"
            Dim dicKota As Dictionary(Of String, String) = New Dictionary(Of String, String)
            Dim dataKotas As ArrayList = New CityFacade(m_userPrincipal).RetrieveActiveList()
            For Each item As City In dataKotas
                dicKota.Add(item.CityCode, item.CityName)
            Next
            dataKota.AddData(dicKota)
            template.AddSheet(dataKota)
            template.DownLoad()


        End Sub

        Private Function GetTahunFiskal() As List(Of String)
            Dim GetTahun As Integer = DateTime.Now.Year
            Dim result As List(Of String) = New List(Of String)

            'Before
            For x As Integer = 4 To 0 Step -1
                Dim value1 As String = (GetTahun - x).ToString()
                Dim value2 As String = (GetTahun - x - 1).ToString()
                Dim value As String = String.Format("{0}/{1}", value2, value1)
                result.Add(value)
            Next
            'After
            For x As Integer = 0 To 4
                Dim value1 As String = (GetTahun + x).ToString()
                Dim value2 As String = (GetTahun + x + 1).ToString()
                Dim value As String = String.Format("{0}/{1}", value1, value2)
                result.Add(value)
            Next
            Return result
        End Function

        Private Function GenerateCodeClass(ByVal kode As String) As String
            Dim code As String = String.Empty
            Dim gelombang As Integer = 1
            Dim corse As TrCourse = New TrCourseFacade(m_userPrincipal).Retrieve(kode)
            If String.IsNullOrEmpty(corse.ClassCode) Then
                code = corse.CourseCode.ToUpper() + DateTime.Now.Year.ToString().Substring(2, 2)
            Else
                code = corse.ClassCode.ToUpper() + DateTime.Now.Year.ToString().Substring(2, 2)
            End If

            Dim srtParam As SortCollection = New SortCollection
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "ClassCode", MatchType.StartsWith, code))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "TrCourse.CourseCode", MatchType.Exact, corse.CourseCode))
            srtParam.Add(New Sort(GetType(TrClass), "CreatedTime", Sort.SortDirection.DESC))
            Dim arrClass As ArrayList = New TrClassFacade(m_userPrincipal).Retrieve(criterias, srtParam)

            If arrClass.Count.Equals(0) Then
                Return code & gelombang.ToString()
            Else
                Dim number As Integer = Integer.Parse(CType(arrClass(0), TrClass).ClassCode.Replace(code, "")) + 1
                Return code & number.ToString()
            End If
        End Function

        Private Sub SaveData(listTrClass As List(Of TrClass))
            For Each oClass As TrClass In listTrClass
                Dim iReturn As Integer
                iReturn = InsertClass(oClass)
                If iReturn > 0 Then
                    oClass.ID = iReturn
                End If
            Next
        End Sub

        Private Function InsertClass(ByVal objTrClass As TrClass) As Integer
            Return New TrClassFacade(m_userPrincipal).InsertTransaction(objTrClass)
        End Function

    End Class

End Namespace


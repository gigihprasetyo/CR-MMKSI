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
Imports KTB.DNet.BusinessFacade.Salesman


Namespace KTB.DNet.Parser
    Public Class UploadSalesmanGradeParser
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
            Dim listSalesmanGrade As List(Of SalesmanGrade) = New List(Of SalesmanGrade)
            Dim listError As List(Of ErrorExcelUpload) = New List(Of ErrorExcelUpload)
            ParseFile(fileName, listSalesmanGrade, listError)

            If listError.Count = 0 Then
                SaveData(listSalesmanGrade)
            End If

            Return listError

        End Function

        Private Sub ParseFile(ByVal fileName As String, ByRef listSalesmanGrade As List(Of SalesmanGrade), ByRef listError As List(Of ErrorExcelUpload))
            Dim fileInfo As FileInfo = New FileInfo(fileName)
            Using excelPkg As New ExcelPackage(fileInfo)
                Using ws As ExcelWorksheet = excelPkg.Workbook.Worksheets.Item(1)
                    Dim ColumnCount As Integer = ws.Dimension.End.Column
                    Dim RowCount As Integer = ws.Dimension.End.Row
                    If ColumnCount < 4 Then
                        Throw New Exception("Format file tidak sesuai")
                    End If

                    Dim dataGrade As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)
                    Dim arrGrade As ArrayList = New StandardCodeFacade(Me.m_page.User).RetrieveByCategory("SalesmanGrade")
                    For Each iGrade As StandardCode In arrGrade
                        dataGrade.Add(iGrade.ValueId, iGrade.ValueDesc.ToUpper())
                    Next

                    Dim dataPeriode As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)
                    Dim arrPeriode As ArrayList = New StandardCodeFacade(Me.m_page.User).RetrieveByCategory("GradePeriode")
                    For Each iPeriode As StandardCode In arrPeriode
                        dataPeriode.Add(iPeriode.ValueId, iPeriode.ValueCode.ToUpper())
                    Next
                    Dim listJobPosition As List(Of JobPositionToCategory) = New JobPositionToCategoryFacade(Me.m_page.User).RetrieveByCategory("1").Cast(Of  _
                                                JobPositionToCategory).ToList()

                    For idx As Integer = 4 To RowCount
                        Dim validasi As ExcelValidation = New ExcelValidation(ws)
                        Dim SalesmanCode As ExcelField = validasi.Create("Salesman Code", idx, 1, "required,max", 20)
                        Dim Tahun As ExcelField = validasi.Create("Tahun", idx, 2, "required,numeric")
                        Dim Periode As ExcelField = validasi.Create("Periode", idx, 3, "required")
                        Dim Grade As ExcelField = validasi.Create("Grade", idx, 4, "required")
                        Dim Score As ExcelField = validasi.Create("Productivity", idx, 5, "required")

                        'Validasi Requirment Value
                        Dim listErrorfield As List(Of ErrorExcelUpload) = validasi.Validate()

                        If Not dataPeriode.ContainsValue(Periode.Value.ToUpper()) Then
                            listErrorfield.Add(validasi.CreateCustomError(Periode, "harus berisi Q1, Q2, Q3 atau Q4"))
                        End If

                        If Not dataGrade.ContainsValue(Grade.Value.ToUpper()) Then
                            listErrorfield.Add(validasi.CreateCustomError(Grade, "harus berisi New, Reguler, Silver atau Gold"))
                        End If

                        Dim mScore As Decimal = 0
                        Try
                            mScore = Decimal.Parse(Score.Value)
                        Catch 
                        End Try

                        If mScore < 0 Or mScore > 100 Then
                            listErrorfield.Add(validasi.CreateCustomError(Score, "Productivity harus diisi 1-100"))
                        End If


                        Dim sH As SalesmanHeader = New SalesmanHeaderFacade(Me.m_page.User).Retrieve(SalesmanCode.Value)
                        If Not IsNothing(sH) Then
                            If Not sH.Status = CType(EnumSalesmanStatus.SalesmanStatus.Aktif, String) Then
                                listErrorfield.Add(validasi.CreateCustomError(SalesmanCode, "status tidak aktif"))
                            Else
                                If Not listJobPosition.Where(Function(x) x.JobPosition.ID = sH.JobPosition.ID).IsData Then
                                    listErrorfield.Add(validasi.CreateCustomError(SalesmanCode, "bukan dari kategori sales"))
                                End If
                            End If
                        Else
                            listErrorfield.Add(validasi.CreateCustomError(SalesmanCode, "tidak terdaftar"))
                        End If

                        If Not listErrorfield.Count.Equals(0) Then
                            listError.AddRange(listErrorfield)
                            Continue For
                        End If

                        If listError.Count.Equals(0) Then
                            Dim nSalesmanGrade As New SalesmanGrade
                            nSalesmanGrade.Year = CInt(Tahun.Value)
                            nSalesmanGrade.SalesmanHeader = sH
                            nSalesmanGrade.Period = dataPeriode.FirstOrDefault(Function(x) x.Value.ToUpper = Periode.Value.ToUpper).Key
                            nSalesmanGrade.Grade = dataGrade.FirstOrDefault(Function(x) x.Value.ToUpper() = Grade.Value.ToUpper).Key
                            nSalesmanGrade.Score = Decimal.Parse(Score.Value)
                            nSalesmanGrade.Status = 1

                            listSalesmanGrade.Add(nSalesmanGrade)
                        End If

                    Next
                End Using
            End Using
        End Sub

        Public Sub DownloadTemplate()
            Dim template As ExcelTemplate = New ExcelTemplate(Me.m_page)
            template.FileName = "TemplateUploadSalesmanGrade.xls"
            template.SheetName = "UploadSalesmanGrade"
            template.Judul = "Upload Salesman Grade"
            template.AddField(1, "Salesman Code")
            template.AddField(2, "Tahun Fiskal")

            Dim dataPeriode As ExcelTemplateColumn = New ExcelTemplateColumn(3, "Periode", EnumTypeCell.Dropdownlist)
            Dim listP As List(Of String) = New List(Of String)
            Dim arrPeriode As ArrayList = New StandardCodeFacade(Me.m_page.User).RetrieveByCategory("GradePeriode")
            For Each iPeriode As StandardCode In arrPeriode
                listP.Add(iPeriode.ValueCode)
            Next
            dataPeriode.DataValidation = listP
            template.AddField(dataPeriode)


            Dim dataGrade As ExcelTemplateColumn = New ExcelTemplateColumn(4, "Grade", EnumTypeCell.Dropdownlist)
            Dim list As List(Of String) = New List(Of String)
            Dim arrGrade As ArrayList = New StandardCodeFacade(Me.m_page.User).RetrieveByCategory("SalesmanGrade")
            For Each iGrade As StandardCode In arrGrade
                list.Add(iGrade.ValueCode)
            Next
            dataGrade.DataValidation = list
            template.AddField(dataGrade)
            template.AddField(5, "Productivity")

            template.DownLoad()

        End Sub

        Private Sub SaveData(listSalesmanGrade As List(Of SalesmanGrade))
            Dim func As New SalesmanGradeFacade(Me.m_page.User)
            For Each iSalesmanGrade As SalesmanGrade In listSalesmanGrade
                Dim iReturn As Integer
                Dim iGrade As SalesmanGrade = func.GradeByPeriod(iSalesmanGrade.SalesmanHeader.SalesmanCode, iSalesmanGrade.Year, iSalesmanGrade.Period)
                If IsNothing(iGrade) Then
                    iReturn = InsertData(iSalesmanGrade)
                    If iReturn > 0 Then
                        iSalesmanGrade.ID = iReturn
                    End If
                Else
                    iGrade.Grade = iSalesmanGrade.Grade
                    iGrade.Score = iSalesmanGrade.Score
                    iReturn = func.Update(iGrade)
                End If
            Next
        End Sub

        Private Function InsertData(ByVal objSalesmanGrade As SalesmanGrade) As Integer
            Return New SalesmanGradeFacade(Me.m_page.User).Insert(objSalesmanGrade)
        End Function

    End Class
End Namespace


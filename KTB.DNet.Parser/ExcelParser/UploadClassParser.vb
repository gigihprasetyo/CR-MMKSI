#Region "Summary"
'// ===========================================================================		
'// Author Name   : Anna Nurhayanto
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2011
'// ---------------------
'// $History      : $
'// Generated on 20/12/2011
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"
'Imports System.Data.Odbc
Imports System.Security.Principal
Imports System.Text
Imports System
Imports System.Data
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.IO
'Imports System.Data.OleDb
Imports Excel
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.General
#End Region

Namespace KTB.DNet.Parser

    Public Class UploadClassParser
        Inherits AbstractExcelParser

#Region "Private Variables"
        Private TrClassList As ArrayList
        Private _fileName As String
        Private _trClass As TrClass
        Private ErrorMessage As StringBuilder
#End Region

#Region "Protected Methods"

        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            TrClassList = New ArrayList  '-- List of Class

            Dim parts() As String = fileName.Split(".".ToCharArray())
            Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
            Dim objReader As IExcelDataReader = Nothing


            'Dim conn As OleDbConnection = New OleDbConnection
            'Dim connStr As String = String.Format("provider=Microsoft.Jet.OLEDB.4.0; data source='{0}';Extended Properties=""Excel 8.0; IMEX=1; HDR=No;""", fileName)
            'conn.ConnectionString = connStr ' "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & fileName & ";Extended Properties=""Excel 12.0;IMEX=1"""
            'conn.Open()
            Dim dt As New System.Data.DataTable
            Dim Ds As New System.Data.DataSet


            Using stream As FileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)
                If (ext = "xls") Then
                    objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                ElseIf (ext = "xlsx") Then
                    objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                End If

                If (Not IsNothing(objReader)) Then
                    objReader.IsFirstRowAsColumnNames = False
                    Ds = objReader.AsDataSet()
                    Ds.DataSetName = "TestTable"
                    Ds.Tables(0).TableName = "Table"
                End If


            End Using

            'Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")
            Dim row As DataRow

            If Ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 1 To Ds.Tables(0).Rows.Count - 1
                    row = Ds.Tables(0).Rows(i)
                    _trClass = New TrClass
                    _trClass = ParseClass(row)
                    TrClassList.Add(_trClass)
                Next
            End If
       

            Return TrClassList
        End Function

#Region "Old ParsingExcelNoTransaction"

        'Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
        '    TrClassList = New ArrayList  '-- List of Class
        '    Dim conn As OleDbConnection = New OleDbConnection
        '    Dim connStr As String = String.Format("provider=Microsoft.Jet.OLEDB.4.0; data source='{0}';Extended Properties=""Excel 8.0; IMEX=1; HDR=No;""", fileName)
        '    conn.ConnectionString = connStr ' "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & fileName & ";Extended Properties=""Excel 12.0;IMEX=1"""
        '    conn.Open()
        '    Dim dt As New DataTable
        '    Dim da As OleDbDataAdapter = New OleDbDataAdapter("SELECT * FROM " & sheetName, conn)
        '    da.TableMappings.Add("Table", "TestTable")
        '    Dim Ds = New System.Data.DataSet
        '    da.Fill(Ds)
        '    conn.Close()


        '    'Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")
        '    Dim row As DataRow
        '    Dim i As Integer
        '    For i = 1 To Ds.Tables(0).Rows.Count - 1
        '        row = Ds.Tables(0).Rows(i)
        '        _trClass = New TrClass
        '        _trClass = ParseClass(row)
        '        TrClassList.Add(_trClass)
        '    Next

        '    Return TrClassList
        'End Function

#End Region
        Private Function ParseClass(ByVal row As DataRow) As TrClass

            ErrorMessage = New StringBuilder
            If row.ItemArray.Length < 11 Then
                ErrorMessage.Append("Struktur data tidak sesuai. " & Chr(13) & Chr(10))
                _trClass.ErrorMessage = ErrorMessage.ToString
                Return _trClass
            End If
            'Class Code
            If Not row(0) Is Nothing Then
                Try
                    Dim strClassCode As String = CType(row(0), String)
                    Dim oClass As TrClass = New TrClassFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(strClassCode)
                    If oClass.ID > 0 Then
                        _trClass.ClassCode = strClassCode
                        ErrorMessage.Append("Kode Kelas " & strClassCode & " sudah ada." & Chr(13) & Chr(10))
                    Else
                        _trClass.ClassCode = strClassCode
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Kode Kelas tidak terdefinisi. " & Chr(13) & Chr(10))
                End Try

            Else
                ErrorMessage.Append("Kode Kelas tidak terdefinisi. " & Chr(13) & Chr(10))
            End If
            'ClassName
            If Not row(1) Is Nothing Then
                Try
                    Dim strClassName As String = CType(row(1), String)
                    If strClassName <> "" Then
                        _trClass.ClassName = strClassName
                    Else
                        ErrorMessage.Append("Nama Kelas tidak terdefinisi." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Nama Kelas tidak terdefinisi. " & Chr(13) & Chr(10))
                End Try

            Else
                ErrorMessage.Append("Nama Kelas tidak terdefinisi. " & Chr(13) & Chr(10))
            End If
            'Location
            If Not row(2) Is Nothing Then
                Try
                    Dim strLocation As String = CType(row(2), String)
                    If strLocation.Length > 100 Then
                        ErrorMessage.Append("Lokasi > 100 karakter " & Chr(13) & Chr(10))
                    Else
                        _trClass.Location = strLocation
                    End If


                Catch ex As Exception
                    ErrorMessage.Append("Lokasi tidak terdefinisi " & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Lokasi tidak terdefinisi " & Chr(13) & Chr(10))
            End If
            'Trainer1
            If Not (row(3) Is System.DBNull.Value) Then
                Try
                    If CType(row(3), String) <> "" Then
                        If CType(row(3), String).Length > 100 Then
                            ErrorMessage.Append("Nama Trainer1 > 100 karakter " & Chr(13) & Chr(10))
                        Else
                            _trClass.Trainer1 = CType(row(3), String)
                        End If
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Nama Trainer1 tidak  terdefinisi " & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Nama Trainer1 tidak  terdefinisi " & Chr(13) & Chr(10))
            End If

            'Trainer2
            If Not (row(4) Is System.DBNull.Value) Then
                Try
                    If CType(row(4), String) <> "" Then
                        If CType(row(4), String).Length > 100 Then
                            ErrorMessage.Append("Nama Trainer2 > 100 karakter " & Chr(13) & Chr(10))
                        Else
                            _trClass.Trainer2 = CType(row(4), String)
                        End If
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Nama Trainer2 tidak  terdefinisi " & Chr(13) & Chr(10))
                End Try
                'Else
                'ErrorMessage.Append("Nama Trainer2 tidak  terdefinisi" & Chr(13) & Chr(10))
            End If

            'Trainer3
            If Not (row(5) Is System.DBNull.Value) Then
                Try
                    If CType(row(5), String) <> "" Then
                        If CType(row(5), String).Length > 100 Then
                            ErrorMessage.Append("Nama Trainer3 > 100 karakter " & Chr(13) & Chr(10))
                        Else
                            _trClass.Trainer3 = CType(row(5), String)
                        End If
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Nama Trainer3 tidak  terdefinisi " & Chr(13) & Chr(10))
                End Try
                'Else
                '    ErrorMessage.Append("Nama Trainer3 tidak  terdefinisi" & Chr(13) & Chr(10))
            End If

            'Capacity
            If Not row(6) Is Nothing Then
                Try
                    If CType(row(6), Integer) > 0 Then
                        _trClass.Capacity = CType(row(6), Integer)
                    Else
                        ErrorMessage.Append("Kapasitas harus terisi " & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Kapasitas tidak  terdefinisi " & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Kapasitas tidak  terdefinisi " & Chr(13) & Chr(10))
            End If

            'Category
            If Not row(7) Is Nothing Then
                Try
                    Dim strCategory As String = CType(row(7), String)
                    Dim oTrCourse As TrCourse = New TrCourseFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(strCategory)
                    If oTrCourse.ID > 0 Then
                        _trClass.TrCourse = oTrCourse
                        _trClass.Category = oTrCourse.Category.ID
                    Else
                        ErrorMessage.Append("Kategori tidak terdefinisi" & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Kategori tidak  terdefinisi " & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Kategori tidak  terdefinisi " & Chr(13) & Chr(10))
            End If

            'Keterangan
            If Not (row(8) Is System.DBNull.Value) Then
                Try
                    If CType(row(8), String) <> "" Then
                        If CType(row(8), String).Length <= 100 Then
                            _trClass.Description = CType(row(8), String)
                        Else
                            ErrorMessage.Append("Keterangan > 100 karakter " & Chr(13) & Chr(10))
                        End If
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Keterangan tidak  terdefinisi " & Chr(13) & Chr(10))
                End Try
                'Else
                '    ErrorMessage.Append("Keterangan tidak  terdefinisi " & Chr(13) & Chr(10))
            End If

            'Status
            _trClass.Status = "1"
            'Start Date
            Dim startDate As Date
            If Not row(9) Is Nothing Then
                Try
                    Dim strStartDate As String = CType(row(9), String)
                    If strStartDate.Length = 8 Then
                        startDate = New Date(strStartDate.Substring(4, 4), strStartDate.Substring(2, 2), strStartDate.Substring(0, 2))
                        _trClass.StartDate = startDate
                    ElseIf strStartDate.Length = 7 Then
                        startDate = New Date(strStartDate.Substring(3, 4), strStartDate.Substring(1, 2), "0" & strStartDate.Substring(0, 1))
                        _trClass.StartDate = startDate
                    Else
                        ErrorMessage.Append("Format tanggal mulai harus ddmmyyyy " & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Format tanggal mulai harus ddmmyyyy " & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Format tanggal mulai harus ddmmyyyy " & Chr(13) & Chr(10))
            End If

            'End Date
            Dim endDate As Date
            If Not row(10) Is Nothing Then
                Try
                    Dim strEndDate As String = CType(row(10), String)
                    If strEndDate.Length = 8 Then
                        endDate = New Date(strEndDate.Substring(4, 4), strEndDate.Substring(2, 2), strEndDate.Substring(0, 2))
                        _trClass.FinishDate = endDate
                    ElseIf strEndDate.Length = 7 Then
                        endDate = New Date(strEndDate.Substring(3, 4), strEndDate.Substring(1, 2), "0" & strEndDate.Substring(0, 1))
                        _trClass.FinishDate = endDate
                    Else
                        ErrorMessage.Append("Format tanggal selesai harus ddmmyyyy " & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Format tanggal selesai harus ddmmyyyy " & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Format tanggal selesai harus ddmmyyyy " & Chr(13) & Chr(10))
            End If

            If endDate.Subtract(startDate).Days < 0 Then
                ErrorMessage.Append("Tanggal selesai < Tanggal mulai " & Chr(13) & Chr(10))
            End If

            If startDate < Today Then
                ErrorMessage.Append("Tanggal mulai < hari ini " & Chr(13) & Chr(10))
            ElseIf endDate < Today Then
                ErrorMessage.Append("Tanggal selesai < hari ini " & Chr(13) & Chr(10))
            End If

            If ErrorMessage.Length > 0 Then
                _trClass.ErrorMessage = ErrorMessage.ToString
            End If

            Return _trClass
        End Function


        Private Function ExistClass(ByVal partNumber As String, ByVal dataCollection As ArrayList)
            For Each objPoDetail As SparePartPODetail In dataCollection
                If Not IsNothing(objPoDetail.SparePartMaster) Then
                    If objPoDetail.SparePartMaster.PartNumber.Trim.ToUpper = partNumber.Trim.ToUpper Then
                        Return True
                    End If
                End If
            Next
            Return False
        End Function

#End Region

    End Class

End Namespace
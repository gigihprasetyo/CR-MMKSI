#Region "Summary"
'// ===========================================================================		
'// Author Name   : Andra AR
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// 
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"
'Imports System.Data.Odbc
Imports System.Security.Principal
Imports System
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports Excel
Imports System.IO

#End Region

Namespace KTB.DNet.Parser

    Public Class UploadStockMaterialPromotionParser
        Inherits AbstractExcelParser

        Public Sub New()
        End Sub


#Region "Protected Methods"

        '----------Format Data Excel yang akan diupload-------------'
        '   Periode - Kode Dealer - Kode Barang - Jumlah Barang     '
        '-----------------------------------------------------------'

        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            DataCollection = New ArrayList  '-- List of Material Promotion Allocation
            'Dim objConn As OdbcConnection   '-- Connection object
            Dim strData As String        '-- Part number --kode barang

            Try

                Dim parts() As String = fileName.Split(".".ToCharArray())
                Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
                Dim objReader As IExcelDataReader = Nothing

                sheetName = sheetName.Replace("[", "").Replace("]", "").Replace("$", "")

                Using stream As FileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)
                    If (ext = "xls") Then
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    ElseIf (ext = "xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    End If

                    If (Not IsNothing(objReader)) Then

                        While objReader.Read()
                            '-- This row number
                            If (objReader.Name.Contains(sheetName)) Then
                                strData = objReader(0)

                            Else
                                objReader.NextResult()
                            End If

                        End While


                    End If

                End Using
 


            Catch ex As Exception

            End Try

        End Function

        Public Function GetSheet(ByVal filename As String) As ArrayList

            Dim parts() As String = filename.Split(".".ToCharArray())
            Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
            Dim objReader As IExcelDataReader = Nothing
            Dim objDs As New DataSet


            Using stream As FileStream = File.Open(filename, FileMode.Open, FileAccess.Read)
                If (ext = "xls") Then
                    objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                ElseIf (ext = "xlsx") Then
                    objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                End If

                If (Not IsNothing(objReader)) Then

                    objReader.IsFirstRowAsColumnNames = True
                    objDs = objReader.AsDataSet()
                    Dim arrSheet As New ArrayList
                    For Each row As System.Data.DataTable In objDs.Tables()
                        arrSheet.Add(row.TableName.ToString)
                    Next

                    Return arrSheet
                End If

            End Using

            Return New ArrayList
             

        End Function

        Public Function ParsingExcel(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            DataCollection = New ArrayList  '-- List of Material Promotion 
            'Dim objConn As OdbcConnection   '-- Connection object

            Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")

            Dim i As Integer = 0
            On Error Resume Next
            For i = 0 To Ds.Tables(0).Rows.Count - 1
                Dim obj As StockMaterialPromotionUpload = New StockMaterialPromotionUpload
                Dim ErrMsg As String = ""
                For Each item As StockMaterialPromotionUpload In DataCollection
                    If item.KodeBarang = CStr(Ds.Tables(0).Rows(i)(0)).Trim Then
                        ErrMsg = "Duplikat Kode Barang"
                        Exit For
                    End If
                Next

                obj.KodeBarang = CStr(Ds.Tables(0).Rows(i)(0)).Trim
                obj.NamaBarang = CStr(Ds.Tables(0).Rows(i)(1)).Trim
                obj.Unit = CStr(Ds.Tables(0).Rows(i)(2)).Trim
                obj.Stock = CStr(Ds.Tables(0).Rows(i)(3)).Trim
                If ErrMsg = "" Then
                    obj.ErrorMessage = GetErrorMessage(obj)
                Else
                    obj.ErrorMessage = ErrMsg
                End If
                DataCollection.Add(obj)
            Next

            Return DataCollection


        End Function

        Private Function GetErrorMessage(ByVal objUpload As StockMaterialPromotionUpload) As String

            If objUpload.KodeBarang = "" Or objUpload.NamaBarang = "" Or objUpload.Unit = "" Or objUpload.Stock = "" Then
                Return "Data tidak lengkap"
            End If

            Try
                Dim Stock As Integer = Integer.Parse(objUpload.Stock)
            Catch ex As Exception
                Return "Data Stock tidak valid"
            End Try

            Dim objMP As MaterialPromotion = New MaterialPromotionFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(objUpload.KodeBarang)
            If objMP.ID = 0 Then
                Return "Data Kode Barang tidak valid"
            End If

            Return ""
        End Function

#End Region

    End Class


#Region "Old UploadStockMaterialPromotionParser"

    '    Public Class UploadStockMaterialPromotionParser
    '        Inherits AbstractExcelParser

    '        Public Sub New()
    '        End Sub


    '#Region "Protected Methods"

    '        '----------Format Data Excel yang akan diupload-------------'
    '        '   Periode - Kode Dealer - Kode Barang - Jumlah Barang     '
    '        '-----------------------------------------------------------'

    '        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
    '            DataCollection = New ArrayList  '-- List of Material Promotion Allocation
    '            Dim objConn As OdbcConnection   '-- Connection object
    '            Dim strData As String        '-- Part number --kode barang

    '            Try
    '                Dim strConn As String = StrConnection & fileName  '-- Connection string
    '                objConn = New OdbcConnection(strConn)
    '                objConn.Open()  '-- Open connection

    '                Dim objCmd = New OdbcCommand("SELECT * FROM " & sheetName, objConn)  '-- Command object
    '                objCmd.CommandType = CommandType.Text
    '                objReader = objCmd.ExecuteReader()  '-- Read data

    '                While objReader.Read()
    '                    Try
    '                        strData = objReader(0)


    '                    Catch ex As Exception

    '                    End Try

    '                End While


    '            Catch ex As Exception

    '            End Try

    '        End Function

    '        Public Function GetSheet(ByVal filename As String) As ArrayList
    '            Dim objConn As OleDb.OleDbConnection             '-- Connection object
    '            Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename + ";Extended Properties=Excel 8.0;"


    '            'Dim strConn As String = StrConnection & filename  '-- Connection string
    '            objConn = New OleDb.OleDbConnection(strCon)

    '            objConn.Open()

    '            Dim dt As System.data.DataTable
    '            dt = objConn.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables, Nothing)
    '            If dt Is Nothing Then
    '                objConn.Close()
    '                Return New ArrayList
    '            Else
    '                Dim arrSheet As New ArrayList
    '                For Each row As DataRow In dt.Rows
    '                    arrSheet.Add(row("TABLE_NAME").ToString)
    '                Next
    '                objConn.Close()
    '                Return arrSheet
    '            End If

    '        End Function

    '        Public Function ParsingExcel(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
    '            DataCollection = New ArrayList  '-- List of Material Promotion 
    '            Dim objConn As OdbcConnection   '-- Connection object

    '            Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")

    '            Dim i As Integer = 0
    '            On Error Resume Next
    '            For i = 0 To Ds.Tables(0).Rows.Count - 1
    '                Dim obj As StockMaterialPromotionUpload = New StockMaterialPromotionUpload
    '                Dim ErrMsg As String = ""
    '                For Each item As StockMaterialPromotionUpload In DataCollection
    '                    If item.KodeBarang = CStr(Ds.Tables(0).Rows(i)(0)).Trim Then
    '                        ErrMsg = "Duplikat Kode Barang"
    '                        Exit For
    '                    End If
    '                Next

    '                obj.KodeBarang = CStr(Ds.Tables(0).Rows(i)(0)).Trim
    '                obj.NamaBarang = CStr(Ds.Tables(0).Rows(i)(1)).Trim
    '                obj.Unit = CStr(Ds.Tables(0).Rows(i)(2)).Trim
    '                obj.Stock = CStr(Ds.Tables(0).Rows(i)(3)).Trim
    '                If ErrMsg = "" Then
    '                    obj.ErrorMessage = GetErrorMessage(obj)
    '                Else
    '                    obj.ErrorMessage = ErrMsg
    '                End If
    '                DataCollection.Add(obj)
    '            Next

    '            Return DataCollection


    '        End Function

    '        Private Function GetErrorMessage(ByVal objUpload As StockMaterialPromotionUpload) As String

    '            If objUpload.KodeBarang = "" Or objUpload.NamaBarang = "" Or objUpload.Unit = "" Or objUpload.Stock = "" Then
    '                Return "Data tidak lengkap"
    '            End If

    '            Try
    '                Dim Stock As Integer = Integer.Parse(objUpload.Stock)
    '            Catch ex As Exception
    '                Return "Data Stock tidak valid"
    '            End Try

    '            Dim objMP As MaterialPromotion = New MaterialPromotionFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(objUpload.KodeBarang)
    '            If objMP.ID = 0 Then
    '                Return "Data Kode Barang tidak valid"
    '            End If

    '            Return ""
    '        End Function

    '#End Region

    '    End Class

#End Region
End Namespace



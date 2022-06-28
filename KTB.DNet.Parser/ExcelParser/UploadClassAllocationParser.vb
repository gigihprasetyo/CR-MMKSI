#Region "Summary"
'// ===========================================================================		
'// Author Name   : Ronny Teguh P
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
'Imports System.Data.OleDb
Imports Excel
Imports System.Security.Principal
Imports System.IO
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.MaterialPromotion
#End Region

Namespace KTB.DNet.Parser

    Public Class UploadClassAllocationParser
        Inherits AbstractExcelParser

        Public Sub New()
        End Sub


#Region "Protected Methods"

        '----------Format Data Excel yang akan diupload-------------'
        '   Periode - Kode Dealer - Kode Barang - Jumlah Barang     '
        '-----------------------------------------------------------'

        ''' <summary>
        ''' New ParsingExcelNoTransaction
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <param name="sheetName"></param>
        ''' <param name="user"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            DataCollection = New ArrayList  '-- List of Material Promotion Allocation

            Dim strData As String        '-- Part number --kode barang
            Dim parts() As String = fileName.Split(".".ToCharArray())
            Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
            Dim objReader As IExcelDataReader = Nothing


            Try

                Using stream As FileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)
                    If (ext = "xls") Then
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    ElseIf (ext = "xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    End If

                    If (Not IsNothing(objReader)) Then

                        While objReader.Read()
                            Try
                                strData = objReader.GetString(0)

                            Catch ex As Exception

                            End Try

                        End While


                    End If

                End Using


            Catch ex As Exception

            End Try

        End Function

#Region "Old ParsingExcelNoTransaction"
        'Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
        '    DataCollection = New ArrayList  '-- List of Material Promotion Allocation
        '    Dim objConn As OdbcConnection   '-- Connection object
        '    Dim strData As String        '-- Part number --kode barang

        '    Try
        '        Dim strConn As String = StrConnection & fileName  '-- Connection string
        '        objConn = New OdbcConnection(strConn)
        '        objConn.Open()  '-- Open connection

        '        Dim objCmd = New OdbcCommand("SELECT * FROM " & sheetName, objConn)  '-- Command object
        '        objCmd.CommandType = CommandType.Text
        '        objReader = objCmd.ExecuteReader()  '-- Read data

        '        While objReader.Read()
        '            Try
        '                strData = objReader(0)


        '            Catch ex As Exception

        '            End Try

        '        End While


        '    Catch ex As Exception

        '    End Try

        'End Function
#End Region

        ''' <summary>
        ''' NEw GetSheet
        ''' </summary>
        ''' <param name="filename"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetSheet(ByVal filename As String) As ArrayList

            Dim arlData As ArrayList = New ArrayList
            Dim parts() As String = filename.Split(".".ToCharArray())
            Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
            Dim objReader As IExcelDataReader = Nothing
            Dim errMsg As String = String.Empty

            Try

                Dim Ds As DataSet = ParseExcelDataSet(filename, "", "")

                If Ds Is Nothing Then
                    Return New ArrayList
                Else
                    Dim arrSheet As New ArrayList
                    For Each row As System.Data.DataTable In Ds.Tables
                        arrSheet.Add(row.TableName.ToString)
                    Next

                    Return arrSheet
                End If
            Catch ex As Exception
                errMsg = ex.Message
            End Try

            Return arlData

        End Function

#Region "old GetSheet"

        'Public Function GetSheet(ByVal filename As String) As ArrayList
        '    Dim errMsg As String = ""
        '    Dim arlData As ArrayList = New ArrayList
        '    Dim objConn As OleDb.OleDbConnection             '-- Connection object

        '    Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename + ";Extended Properties=Excel 8.0;" 'uid=sap;pwd=7Karakter"

        '    Try

        '        'Dim strConn As String = StrConnection & filename  '-- Connection string
        '        objConn = New OleDb.OleDbConnection(strCon)
        '        objConn.Open()

        '        Dim dt As System.data.DataTable
        '        dt = objConn.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables, Nothing)
        '        If dt Is Nothing Then
        '            objConn.Close()
        '            Return New ArrayList
        '        Else
        '            Dim arrSheet As New ArrayList
        '            For Each row As DataRow In dt.Rows
        '                arrSheet.Add(row("TABLE_NAME").ToString)
        '            Next
        '            objConn.Close()
        '            Return arrSheet
        '        End If
        '    Catch ex As Exception
        '        errMsg = ex.Message
        '    End Try

        '    Return arlData

        'End Function

#End Region

        'Public Function ParsingExcelToDataSet(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
        '    DataCollection = New ArrayList  '-- List of Material Promotion Allocation

        '    Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")

        '    Return Ds
        'End Function


        Public Function ParsingExcel(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            DataCollection = New ArrayList  '-- List of Material Promotion Allocation
            'Dim objConn As OdbcConnection   '-- Connection object
            Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")
            If Ds.Tables(0).Columns.Count <> 4 Then
                Return DataCollection
            End If
            Dim i As Integer = 0
            Dim strData1 As String
            Dim strData2 As String
            Dim strData3 As String
            Dim strData4 As String

            For i = 0 To Ds.Tables(0).Rows.Count - 1
                Try 'TrCourse
                    strData1 = IIf(Ds.Tables(0).Rows(i)(0) Is Nothing, "xxx", Ds.Tables(0).Rows(i)(0))
                Catch ex As Exception
                    strData1 = "xxx"
                End Try
                Try 'ClassCode
                    strData2 = IIf(Ds.Tables(0).Rows(i)(1) Is Nothing, "xxx", Ds.Tables(0).Rows(i)(1))
                Catch ex As Exception
                    strData2 = "xxx"
                End Try
                Try 'DealerCode
                    strData3 = IIf(Ds.Tables(0).Rows(i)(2) Is Nothing, "999999", Ds.Tables(0).Rows(i)(2))
                Catch ex As Exception
                    strData3 = "999999"
                End Try
                Try 'Alocation
                    strData4 = IIf(Ds.Tables(0).Rows(i)(3) Is Nothing, "0", Ds.Tables(0).Rows(i)(3))
                Catch ex As Exception
                    strData4 = "-1000"
                End Try
                'strData1 = IIf(Ds.Tables(0).Rows(i)(0) Is Nothing, "", Ds.Tables(0).Rows(i)(0))
                'strData2 = IIf(Ds.Tables(0).Rows(i)(0) Is Nothing, "0", Ds.Tables(0).Rows(i)(1))
                DataCollection.Add(strData1 & ";" & strData2 & ";" & strData3 & ";" & strData4)
                'DataCollection.Add(CType(Ds.Tables(0).Rows(i)(0), String) & ";" & CType(Ds.Tables(0).Rows(i)(1), String))
            Next

            Return DataCollection

        End Function

        Public Function ParsingExcelHeader(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            DataCollection = New ArrayList  '-- List of Material Promotion Allocation
            'Dim objConn As OdbcConnection   '-- Connection object
            Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")
            Dim i As Integer = 0

            On Error Resume Next
            For i = 0 To Ds.Tables(0).Columns.Count - 1
                DataCollection.Add(Ds.Tables(0).Columns(i).ColumnName)
            Next i

            Return DataCollection


            'Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")
            'Return Ds
            'Return ParseExcelHeader(fileName, sheetName, "")
        End Function

        Private Function GetErrorMessage(ByVal objUpload As MaterialPromotionUpload) As String

            If objUpload.Periode = "" Or objUpload.KodeDealer = "" Or objUpload.KodeBarang = "" Or objUpload.Qty = "" Then
                Return "Data tidak lengkap"
            End If

            Try
                Dim qty As Integer = Integer.Parse(objUpload.Qty)
            Catch ex As Exception
                Return "Data Qty tidak valid"
            End Try

            Dim criteriaPeriod As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriaPeriod.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "PeriodName", MatchType.Exact, objUpload.Periode))
            Dim arlPeriod As ArrayList = New MaterialPromotionPeriodFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteriaPeriod)
            If arlPeriod.Count = 0 Then
                Return "Data periode tidak valid"
            End If

            Dim objdealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(objUpload.KodeDealer)
            If objdealer.ID = 0 Then
                Return "Data Dealer tidak valid"
            End If

            Dim objMP As MaterialPromotion = New MaterialPromotionFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(objUpload.KodeBarang)
            If objMP.ID = 0 Then
                Return "Data Kode Barang tidak valid"
            End If

            Return ""
        End Function


#End Region



    End Class


End Namespace



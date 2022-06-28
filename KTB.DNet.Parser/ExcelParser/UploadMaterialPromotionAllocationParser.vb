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
Imports System.Data.Odbc
Imports System.Security.Principal

#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.MaterialPromotion
#End Region

Namespace KTB.DNet.Parser

    Public Class UploadMaterialPromotionAllocationParser
        Inherits AbstractExcelParser

        Public Sub New()
        End Sub


#Region "Protected Methods"

        '----------Format Data Excel yang akan diupload-------------'
        '   Periode - Kode Dealer - Kode Barang - Jumlah Barang     '
        '-----------------------------------------------------------'

        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            DataCollection = New ArrayList  '-- List of Material Promotion Allocation
            Dim objConn As OdbcConnection   '-- Connection object
            Dim strData As String        '-- Part number --kode barang

            Try
                Dim strConn As String = StrConnection & fileName  '-- Connection string
                objConn = New OdbcConnection(strConn)
                objConn.Open()  '-- Open connection

                Dim objCmd = New OdbcCommand("SELECT * FROM " & sheetName, objConn)  '-- Command object
                objCmd.CommandType = CommandType.Text
                objReader = objCmd.ExecuteReader()  '-- Read data

                While objReader.Read()
                    Try
                        strData = objReader(0)


                    Catch ex As Exception

                    End Try

                End While


            Catch ex As Exception

            End Try

        End Function

        Public Function GetSheet(ByVal filename As String) As ArrayList
            Dim arlData As ArrayList = New ArrayList
            Dim parts() As String = filename.Split(".".ToCharArray())
            Dim ext As String = parts(parts.Length - 1).Trim().ToLower()

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

#Region "Old GetSheet"

        'Public Function GetSheet(ByVal filename As String) As ArrayList
        '    Dim objConn As OleDb.OleDbConnection             '-- Connection object
        '    Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename + ";Extended Properties=Excel 8.0;"


        '    'Dim strConn As String = StrConnection & filename  '-- Connection string
        '    objConn = New OleDb.OleDbConnection(strCon)

        '    objConn.Open()

        '    Dim dt As System.data.DataTable
        '    dt = objConn.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables, Nothing)
        '    If dt Is Nothing Then
        '        objConn.Close()
        '        Return New ArrayList
        '    Else
        '        Dim arrSheet As New ArrayList
        '        For Each row As DataRow In dt.Rows
        '            arrSheet.Add(row("TABLE_NAME").ToString)
        '        Next
        '        objConn.Close()
        '        Return arrSheet
        '    End If



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
            Dim i As Integer = 0

            On Error Resume Next
            For i = 0 To Ds.Tables(0).Rows.Count - 1
                Dim obj As MaterialPromotionUpload = New MaterialPromotionUpload
                obj.KodeDealer = CStr(Ds.Tables(0).Rows(i)(0)).Trim
                obj.GoodNo1 = CStr(Ds.Tables(0).Rows(i)(1)).Trim
                obj.GoodNo2 = CStr(Ds.Tables(0).Rows(i)(2)).Trim
                obj.GoodNo3 = CStr(Ds.Tables(0).Rows(i)(3)).Trim
                obj.GoodNo4 = CStr(Ds.Tables(0).Rows(i)(4)).Trim
                obj.GoodNo5 = CStr(Ds.Tables(0).Rows(i)(5)).Trim
                obj.GoodNo6 = CStr(Ds.Tables(0).Rows(i)(6)).Trim
                obj.GoodNo7 = CStr(Ds.Tables(0).Rows(i)(7)).Trim
                obj.GoodNo8 = CStr(Ds.Tables(0).Rows(i)(8)).Trim
                obj.GoodNo9 = CStr(Ds.Tables(0).Rows(i)(9)).Trim
                obj.GoodNo10 = CStr(Ds.Tables(0).Rows(i)(10)).Trim
                obj.GoodNo11 = CStr(Ds.Tables(0).Rows(i)(11)).Trim
                obj.GoodNo12 = CStr(Ds.Tables(0).Rows(i)(12)).Trim
                obj.GoodNo13 = CStr(Ds.Tables(0).Rows(i)(13)).Trim
                obj.GoodNo14 = CStr(Ds.Tables(0).Rows(i)(14)).Trim
                obj.GoodNo15 = CStr(Ds.Tables(0).Rows(i)(15)).Trim
                obj.GoodNo16 = CStr(Ds.Tables(0).Rows(i)(16)).Trim
                obj.GoodNo17 = CStr(Ds.Tables(0).Rows(i)(17)).Trim
                obj.GoodNo18 = CStr(Ds.Tables(0).Rows(i)(18)).Trim
                obj.GoodNo19 = CStr(Ds.Tables(0).Rows(i)(19)).Trim
                obj.GoodNo20 = CStr(Ds.Tables(0).Rows(i)(20)).Trim

                'obj.Periode = CStr(Ds.Tables(0).Rows(i)(0)).Trim
                'obj.KodeDealer = CStr(Ds.Tables(0).Rows(i)(1)).Trim
                'obj.KodeBarang = CStr(Ds.Tables(0).Rows(i)(2)).Trim
                'obj.Qty = CStr(Ds.Tables(0).Rows(i)(3)).Trim

                'obj.ErrorMessage = GetErrorMessage(obj)

                DataCollection.Add(obj)
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



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
Imports System.Data.Odbc
Imports System.Security.Principal
Imports System.Linq

#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports Excel

#End Region

Namespace KTB.DNet.Parser

    Public Class UploadPencairanDepositATahunanParser
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

        Public Function ParsingExcel(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            DataCollection = New ArrayList  '-- List of Material Promotion 
            Dim objConn As OdbcConnection   '-- Connection object

            Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")

            Dim i As Integer = 0
            On Error Resume Next
            For i = 0 To Ds.Tables(0).Rows.Count - 1
                Dim obj As AnnualDepositAHeader = New AnnualDepositAHeader
                Dim ErrMsg As String = ""


                'Validasi di remark krena sekarang bisa double dealer asal beda produk
                'For Each item As AnnualDepositAHeader In DataCollection
                '    If item.Dealer.DealerCode = CStr(Ds.Tables(0).Rows(i)(0)).Trim Then
                '        ErrMsg = "Duplikat Dealer Code"
                '        Exit For
                '    End If
                'Next

                Dim objDealer As New Dealer
                objDealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("Excel"), Nothing)).Retrieve(CStr(Ds.Tables(0).Rows(i)(0)).Trim)
                If objDealer.ID > 0 Then
                    obj.Dealer = objDealer
                    obj.NettoAmount = CDec(CStr(Ds.Tables(0).Rows(i)(1)).Trim)
                    obj.FromDate = DateTime.Now.Date
                    obj.ToDate = DateTime.Now.Date

                   

                    If ErrMsg = "" Then
                        obj.ErrorMessage = GetErrorMessage(obj)
                    Else
                        obj.ErrorMessage = ErrMsg
                    End If

                    Dim StrProductCategoryCode As String = String.Empty

                    If Ds.Tables(0).Columns.Count <> 3 Then
                        obj.ErrorMessage = obj.ErrorMessage + "; FOrmat Produk tidak ada"
                    Else
                        Dim ObjProductCategory As ProductCategory
                        ObjProductCategory = New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("Excel"), Nothing)).Retrieve(CStr(Ds.Tables(0).Rows(i)(2)).Trim)

                        If ObjProductCategory.ID > 0 Then
                            obj.ProductCategory = ObjProductCategory
                        Else
                            obj.ErrorMessage = obj.ErrorMessage + "; Produk Categorry  tidak ada"
                        End If

                        StrProductCategoryCode = ObjProductCategory.Code
                    End If

                    'New Validation impact of Depsoita A
                    For Each item As AnnualDepositAHeader In DataCollection
                        If item.Dealer.DealerCode = CStr(Ds.Tables(0).Rows(i)(0)).Trim AndAlso item.ProductCategory.Code = StrProductCategoryCode Then
                            ErrMsg = "Duplikat Dealer Code"
                            obj.ErrorMessage = obj.ErrorMessage + " ; " + ErrMsg
                            Exit For
                        End If
                    Next

                    DataCollection.Add(obj)
                End If
            Next

            Return DataCollection


        End Function

#Region "Old Parser Excel"
        'Public Function ParsingExcel(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
        '    DataCollection = New ArrayList  '-- List of Material Promotion 
        '    Dim objConn As OdbcConnection   '-- Connection object

        '    Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")

        '    Dim i As Integer = 0
        '    On Error Resume Next
        '    For i = 0 To Ds.Tables(0).Rows.Count - 1
        '        Dim obj As AnnualDepositAHeader = New AnnualDepositAHeader
        '        Dim ErrMsg As String = ""
        '        For Each item As AnnualDepositAHeader In DataCollection
        '            If item.Dealer.DealerCode = CStr(Ds.Tables(0).Rows(i)(0)).Trim Then
        '                ErrMsg = "Duplikat Dealer Code"
        '                Exit For
        '            End If
        '        Next

        '        Dim objDealer As New Dealer
        '        objDealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("Excel"), Nothing)).Retrieve(CStr(Ds.Tables(0).Rows(i)(0)).Trim)
        '        If objDealer.ID > 0 Then
        '            obj.Dealer = objDealer
        '            obj.NettoAmount = CDec(CStr(Ds.Tables(0).Rows(i)(1)).Trim)
        '            obj.FromDate = DateTime.Now.Date
        '            obj.ToDate = DateTime.Now.Date
        '            If ErrMsg = "" Then
        '                obj.ErrorMessage = GetErrorMessage(obj)
        '            Else
        '                obj.ErrorMessage = ErrMsg
        '            End If
        '            DataCollection.Add(obj)
        '        End If
        '    Next

        '    Return DataCollection


        'End Function
#End Region
        Private Function GetErrorMessage(ByVal objUpload As AnnualDepositAHeader) As String

            If objUpload.Dealer Is Nothing Or objUpload.NettoAmount = 0 Then
                Return "Data tidak lengkap"
            End If

            Try
                Dim NettoAmount As Decimal = Decimal.Parse(objUpload.NettoAmount)
            Catch ex As Exception
                Return "Netto Amount tidak valid"
            End Try

            Return ""
        End Function

#End Region

    End Class

End Namespace



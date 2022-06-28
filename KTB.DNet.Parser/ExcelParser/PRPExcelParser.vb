#Region "Summary"
'// ===========================================================================		
'// Author Name   : Agus Pirnadi
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 14/10/2005
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"
'Imports System.Data.Odbc
Imports Excel
Imports KTB.DNet.Parser.Domain
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Utility
Imports System.IO

#End Region

Namespace KTB.DNet.Parser.ExcelParser

    Public Class PRPExcelParser
        Inherits AbstractExcelParser

        Public Enum ValidStatus
            ErrorDataInSheetToko = -5
            ErrorDataInSheetDealer = -4
            InValidFile = -3
            ErrorToko = -2
            ErrorDealer = -1
            Valid = 0
        End Enum

        Private Const PerDLRSheet As String = "PERDLR"
        Private Const PerTokoSheet As String = "PERTOKO"

        Public Sub New()
        End Sub

        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            DataCollection = New ArrayList
            'Dim objConn As OdbcConnection

            If Not sheetName.Contains(PerDLRSheet) And Not sheetName.Contains(PerTokoSheet) Then
                Return Nothing
            End If

            Try
                'Dim strConn As String = StrConnection & fileName
                'objConn = New OdbcConnection(strConn)
                'objConn.Open()

                'Dim objCmd = New OdbcCommand("SELECT * FROM " & sheetName, objConn)
                'objCmd.CommandType = CommandType.Text
                'objReader = objCmd.ExecuteReader()
                'Dim strError As String = String.Empty
                Dim parts() As String = fileName.Split(".".ToCharArray())
                Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
                Dim objReader As IExcelDataReader = Nothing


                Using stream As FileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)
                    If (ext = "xls") Then
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    ElseIf (ext = "xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    End If

                    If (Not IsNothing(objReader)) Then
                        'Do

                        If sheetName.Contains(PerTokoSheet) Then
                            DataCollection = ParseTokoData(objReader)
                        ElseIf sheetName.Contains(PerDLRSheet) Then
                            DataCollection = ParseDealerData(objReader)
                        End If

                    End If

                End Using

            Catch ex As Exception
                Throw ex
            Finally
                'If objConn.State = ConnectionState.Open Then
                '    objConn.Close()
                'End If
            End Try

            Return DataCollection
        End Function

        Public Function ParseExcelNoTransactionPerDealer(ByVal fileName As String, ByVal sheetName As String, ByVal user As String, ByVal nDealer As Dealer) As Object
            DataCollection = New ArrayList
            'Dim objConn As OdbcConnection

            If Not sheetName.Contains(PerDLRSheet) Then
                Return Nothing
            End If

            If IsNothing(nDealer) Then
                Return ParsingExcelNoTransaction(fileName, sheetName, user)
            End If

            Try
                'Dim strConn As String = StrConnection & fileName
                'objConn = New OdbcConnection(strConn)
                'objConn.Open()
                Dim parts() As String = fileName.Split(".".ToCharArray())
                Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
                Dim objReader As IExcelDataReader = Nothing


                Using stream As FileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)
                    If (ext = "xls") Then
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    ElseIf (ext = "xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    End If

                    If (Not IsNothing(objReader) AndAlso sheetName.Contains(PerDLRSheet)) Then
                        DataCollection = ParseDealerData(objReader, nDealer.SearchTerm2)

                    End If

                End Using

                'Dim objCmd = New OdbcCommand("SELECT * FROM " & sheetName & " WHERE Dealer='" & nDealer.SearchTerm2 & "'", objConn)
                'objCmd.CommandType = CommandType.Text
                'objReader = objCmd.ExecuteReader()
                'Dim strError As String = String.Empty
                'If sheetName = PerDLRSheet Then
                '    DataCollection = ParseDealerData(objReader, nDealer.SearchTerm2)
                'End If
            Catch ex As Exception
                Throw ex
            Finally
                'If objConn.State = ConnectionState.Open Then
                '    objConn.Close()
                'End If
            End Try

            Return DataCollection
        End Function

        Private Function ParseTokoData(ByRef objRdr As IExcelDataReader) As ArrayList
            Dim dataCol As ArrayList = New ArrayList
            Dim brs As Integer = 1
            Dim kol As Integer = 0
            Try
                While objRdr.Read()
                    If (brs = 1) Then
                        brs += 1
                        Continue While
                    End If
                    brs += 1
                    kol = 1
                    Dim _PRPExcelToko As PRPExcelPerToko = New PRPExcelPerToko
                    kol += 1
                    _PRPExcelToko.PsCode = objRdr.GetString(1)
                    kol += 1
                    _PRPExcelToko.PartShopName = objRdr.GetString(2)
                    kol += 1
                    _PRPExcelToko.Kota = objRdr.GetString(3)
                    kol += 1
                    _PRPExcelToko.Jan = objRdr.GetInt32(4)
                    kol += 1
                    _PRPExcelToko.Feb = objRdr.GetInt32(5)
                    kol += 1
                    _PRPExcelToko.Mar = objRdr.GetInt32(6)
                    kol += 1
                    _PRPExcelToko.Apr = objRdr.GetInt32(7)
                    kol += 1
                    _PRPExcelToko.May = objRdr.GetInt32(8)
                    kol += 1
                    _PRPExcelToko.Jun = objRdr.GetInt32(9)
                    kol += 1
                    _PRPExcelToko.Jul = objRdr.GetInt32(10)
                    kol += 1
                    _PRPExcelToko.Aug = objRdr.GetInt32(11)
                    kol += 1
                    _PRPExcelToko.Sep = objRdr.GetInt32(12)
                    kol += 1
                    _PRPExcelToko.Oct = objRdr.GetInt32(13)
                    kol += 1
                    _PRPExcelToko.Nov = objRdr.GetInt32(14)
                    kol += 1
                    _PRPExcelToko.Dec = objRdr.GetInt32(15)
                    kol += 1
                    _PRPExcelToko.Total = objRdr.GetInt32(16)
                    kol += 1
                    dataCol.Add(_PRPExcelToko)
                End While
            Catch ex As Exception
                Throw New Exception("Data pada sheet PERTOKO tidak valid. Baris:" + brs.ToString + " Kolom:" + kol.ToString)
            End Try
            Return dataCol
        End Function

        Private Function ParseDealerData(ByVal objRdr As IExcelDataReader, Optional ByVal SearchTerm2 As String = "") As ArrayList
            Dim dataCol As ArrayList = New ArrayList
            Dim brs As Integer = 1
            Dim kol As Integer = 0
            Try
                While objRdr.Read()
                    If (brs = 1) Then
                        brs += 1
                        Continue While
                    End If

                    If (Not SearchTerm2.Equals("")) Then
                        If objRdr.GetString(1) <> SearchTerm2 Then
                            brs += 1
                            Continue While
                        End If
                    End If

                    brs += 1
                    kol = 1
                    Dim _PRPExcelDealer As PRPExcelPerDealer = New PRPExcelPerDealer
                    kol += 1
                    _PRPExcelDealer.DealerCode = objRdr.GetString(1)
                    kol += 1
                    _PRPExcelDealer.PsCode = objRdr.GetString(2)
                    kol += 1
                    _PRPExcelDealer.PartShopName = objRdr.GetString(3)
                    kol += 1
                    _PRPExcelDealer.Kota = objRdr.GetString(4)
                    kol += 1
                    _PRPExcelDealer.Jan = Math.Round(objRdr.GetDouble(5))
                    kol += 1
                    _PRPExcelDealer.Feb = Math.Round(objRdr.GetDouble(6))
                    kol += 1
                    _PRPExcelDealer.Mar = Math.Round(objRdr.GetDouble(7))
                    kol += 1
                    _PRPExcelDealer.Apr = Math.Round(objRdr.GetDouble(8))
                    kol += 1
                    _PRPExcelDealer.May = Math.Round(objRdr.GetDouble(9))
                    kol += 1
                    _PRPExcelDealer.Jun = Math.Round(objRdr.GetDouble(10))
                    kol += 1
                    _PRPExcelDealer.Jul = Math.Round(objRdr.GetDouble(11))
                    kol += 1
                    _PRPExcelDealer.Aug = Math.Round(objRdr.GetDouble(12))
                    kol += 1
                    _PRPExcelDealer.Sep = Math.Round(objRdr.GetDouble(13))
                    kol += 1
                    _PRPExcelDealer.Oct = Math.Round(objRdr.GetDouble(14))
                    kol += 1
                    _PRPExcelDealer.Nov = Math.Round(objRdr.GetDouble(15))
                    kol += 1
                    _PRPExcelDealer.Dec = Math.Round(objRdr.GetDouble(16))
                    kol += 1
                    _PRPExcelDealer.Total = Math.Round(objRdr.GetDouble(17))
                    kol += 1
                    dataCol.Add(_PRPExcelDealer)
                End While
            Catch ex As Exception
                Throw New Exception("Data pada sheet PERDLR tidak valid. Baris:" + brs.ToString + " Kolom:" + kol.ToString)
            End Try
            Return dataCol
        End Function

        Public Function IsExcelValid(ByVal fileName As String) As Integer
            'Dim objConn As OdbcConnection

            Dim nResult As Integer = ValidStatus.Valid

            Try
                Dim parts() As String = fileName.Split(".".ToCharArray())
                Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
                Dim objReader As IExcelDataReader = Nothing


                Using stream As FileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)
                    If (ext = "xls") Then
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    ElseIf (ext = "xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    End If

                    If (Not IsNothing(objReader)) Then

                        'Dim _hasDlrSheet As Boolean = ErrorDLRSheet(objConn)
                        Dim _hasTokoSheet As Boolean = ErrorTokoSheet(objReader)

                        'If Not _hasDlrSheet Then
                        '    nResult = ValidStatus.ErrorDealer
                        'End If

                        If Not _hasTokoSheet Then
                            nResult = ValidStatus.ErrorToko
                        End If

                        If Not _hasTokoSheet Then
                            nResult = ValidStatus.InValidFile
                        End If

                    
 


                        'Loop While Not objReader.NextResult()

                    End If

                End Using

                If nResult = ValidStatus.Valid Then
                    TryParse(fileName, nResult)
                End If

                'Dim strConn As String = StrConnection & fileName
                'objConn = New OdbcConnection(strConn)
                'objConn.Open()

              
            Catch
                If nResult <> ValidStatus.ErrorDataInSheetToko Then
                    nResult = ValidStatus.InValidFile
                End If
            Finally
                'If objConn.State = ConnectionState.Open Then
                '    objConn.Close()
                'End If
            End Try
            Return nResult
        End Function

        'Private Function ErrorDLRSheet(ByVal objConn As IDbConnection) As Boolean
        '    Dim objRdr As OdbcDataReader
        '    Try

        '        While objReader.Read()


        '        End While
        '        Dim objCmd As OdbcCommand = New OdbcCommand("SELECT * FROM " & PerDLRSheet, objConn)
        '        objCmd.CommandType = CommandType.Text
        '        objRdr = objCmd.ExecuteReader()
        '        If IsNothing(objRdr) Then
        '            Return False
        '        End If
        '        If objRdr.FieldCount <> 18 Then
        '            Return False
        '        End If
        '        Return True
        '    Catch ex As Exception
        '        Return False
        '    Finally
        '        If Not objRdr.IsClosed Then
        '            objRdr.Close()
        '        End If
        '    End Try
        'End Function

        Private Function ErrorTokoSheet(ByRef objConn As IExcelDataReader) As Boolean
            'Dim objRdr As OdbcDataReader
            Try
                'Dim objCmd As OdbcCommand = New OdbcCommand("SELECT * FROM " & PerTokoSheet, objConn)
                'objCmd.CommandType = CommandType.Text
                'objRdr = objCmd.ExecuteReader()

                While objConn.Read()
                    If objConn.FieldCount <> 17 Then
                        Return False
                    Else
                        Return True
                    End If
                End While

                Return True
            Catch
                Throw
            Finally

            End Try
        End Function

        Private Sub TryParse(ByVal fileName As String, ByRef retVal As Integer)
            Try
                ParsingExcelNoTransaction(fileName, PerTokoSheet, "")
            Catch
                retVal = ValidStatus.ErrorDataInSheetToko
            End Try

            'Try
            '    ParsingExcelNoTransaction(fileName, PerDLRSheet, "")
            'Catch
            '    retVal = ValidStatus.ErrorDataInSheetDealer
            'End Try
        End Sub

    End Class


#Region "Old PRPExcelParser"

    'Public Class PRPExcelParser
    '    Inherits AbstractExcelParser

    '    Public Enum ValidStatus
    '        ErrorDataInSheetToko = -5
    '        ErrorDataInSheetDealer = -4
    '        InValidFile = -3
    '        ErrorToko = -2
    '        ErrorDealer = -1
    '        Valid = 0
    '    End Enum

    '    Private Const PerDLRSheet As String = "[PERDLR$]"
    '    Private Const PerTokoSheet As String = "[PERTOKO$]"

    '    Public Sub New()
    '    End Sub

    '    Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
    '        DataCollection = New ArrayList
    '        Dim objConn As OdbcConnection

    '        If sheetName <> PerDLRSheet And sheetName <> PerTokoSheet Then
    '            Return Nothing
    '        End If

    '        Try
    '            Dim strConn As String = StrConnection & fileName
    '            objConn = New OdbcConnection(strConn)
    '            objConn.Open()

    '            Dim objCmd = New OdbcCommand("SELECT * FROM " & sheetName, objConn)
    '            objCmd.CommandType = CommandType.Text
    '            objReader = objCmd.ExecuteReader()
    '            Dim strError As String = String.Empty
    '            If sheetName = PerTokoSheet Then
    '                DataCollection = ParseTokoData(objReader)
    '            ElseIf sheetName = PerDLRSheet Then
    '                DataCollection = ParseDealerData(objReader)
    '            End If
    '        Catch ex As Exception
    '            Throw ex
    '        Finally
    '            If objConn.State = ConnectionState.Open Then
    '                objConn.Close()
    '            End If
    '        End Try

    '        Return DataCollection
    '    End Function

    '    Public Function ParseExcelNoTransactionPerDealer(ByVal fileName As String, ByVal sheetName As String, ByVal user As String, ByVal nDealer As Dealer) As Object
    '        DataCollection = New ArrayList
    '        Dim objConn As OdbcConnection

    '        If sheetName <> PerDLRSheet Then
    '            Return Nothing
    '        End If

    '        If IsNothing(nDealer) Then
    '            Return ParsingExcelNoTransaction(fileName, sheetName, user)
    '        End If

    '        Try
    '            Dim strConn As String = StrConnection & fileName
    '            objConn = New OdbcConnection(strConn)
    '            objConn.Open()

    '            Dim objCmd = New OdbcCommand("SELECT * FROM " & sheetName & " WHERE Dealer='" & nDealer.SearchTerm2 & "'", objConn)
    '            objCmd.CommandType = CommandType.Text
    '            objReader = objCmd.ExecuteReader()
    '            Dim strError As String = String.Empty
    '            If sheetName = PerDLRSheet Then
    '                DataCollection = ParseDealerData(objReader)
    '            End If
    '        Catch ex As Exception
    '            Throw ex
    '        Finally
    '            If objConn.State = ConnectionState.Open Then
    '                objConn.Close()
    '            End If
    '        End Try

    '        Return DataCollection
    '    End Function

    '    Private Function ParseTokoData(ByVal objRdr As IDataReader) As ArrayList
    '        Dim dataCol As ArrayList = New ArrayList
    '        Dim brs As Integer = 1
    '        Dim kol As Integer = 0
    '        Try
    '            While objRdr.Read()
    '                brs += 1
    '                kol = 1
    '                Dim _PRPExcelToko As PRPExcelPerToko = New PRPExcelPerToko
    '                kol += 1
    '                _PRPExcelToko.PsCode = objRdr.GetString(1)
    '                kol += 1
    '                _PRPExcelToko.PartShopName = objRdr.GetString(2)
    '                kol += 1
    '                _PRPExcelToko.Kota = objRdr.GetString(3)
    '                kol += 1
    '                _PRPExcelToko.Jan = objRdr.GetInt32(4)
    '                kol += 1
    '                _PRPExcelToko.Feb = objRdr.GetInt32(5)
    '                kol += 1
    '                _PRPExcelToko.Mar = objRdr.GetInt32(6)
    '                kol += 1
    '                _PRPExcelToko.Apr = objRdr.GetInt32(7)
    '                kol += 1
    '                _PRPExcelToko.May = objRdr.GetInt32(8)
    '                kol += 1
    '                _PRPExcelToko.Jun = objRdr.GetInt32(9)
    '                kol += 1
    '                _PRPExcelToko.Jul = objRdr.GetInt32(10)
    '                kol += 1
    '                _PRPExcelToko.Aug = objRdr.GetInt32(11)
    '                kol += 1
    '                _PRPExcelToko.Sep = objRdr.GetInt32(12)
    '                kol += 1
    '                _PRPExcelToko.Oct = objRdr.GetInt32(13)
    '                kol += 1
    '                _PRPExcelToko.Nov = objRdr.GetInt32(14)
    '                kol += 1
    '                _PRPExcelToko.Dec = objRdr.GetInt32(15)
    '                kol += 1
    '                _PRPExcelToko.Total = objRdr.GetInt32(16)
    '                kol += 1
    '                dataCol.Add(_PRPExcelToko)
    '            End While
    '        Catch ex As Exception
    '            Throw New Exception("Data pada sheet PERTOKO tidak valid. Baris:" + brs.ToString + " Kolom:" + kol.ToString)
    '        End Try
    '        Return dataCol
    '    End Function

    '    Private Function ParseDealerData(ByVal objRdr As OdbcDataReader) As ArrayList
    '        Dim dataCol As ArrayList = New ArrayList
    '        Dim brs As Integer = 1
    '        Dim kol As Integer = 0
    '        Try
    '            While objRdr.Read()
    '                brs += 1
    '                kol = 1
    '                Dim _PRPExcelDealer As PRPExcelPerDealer = New PRPExcelPerDealer
    '                kol += 1
    '                _PRPExcelDealer.DealerCode = objRdr.GetString(1)
    '                kol += 1
    '                _PRPExcelDealer.PsCode = objRdr.GetString(2)
    '                kol += 1
    '                _PRPExcelDealer.PartShopName = objRdr.GetString(3)
    '                kol += 1
    '                _PRPExcelDealer.Kota = objRdr.GetString(4)
    '                kol += 1
    '                _PRPExcelDealer.Jan = Math.Round(objRdr.GetDouble(5))
    '                kol += 1
    '                _PRPExcelDealer.Feb = Math.Round(objRdr.GetDouble(6))
    '                kol += 1
    '                _PRPExcelDealer.Mar = Math.Round(objRdr.GetDouble(7))
    '                kol += 1
    '                _PRPExcelDealer.Apr = Math.Round(objRdr.GetDouble(8))
    '                kol += 1
    '                _PRPExcelDealer.May = Math.Round(objRdr.GetDouble(9))
    '                kol += 1
    '                _PRPExcelDealer.Jun = Math.Round(objRdr.GetDouble(10))
    '                kol += 1
    '                _PRPExcelDealer.Jul = Math.Round(objRdr.GetDouble(11))
    '                kol += 1
    '                _PRPExcelDealer.Aug = Math.Round(objRdr.GetDouble(12))
    '                kol += 1
    '                _PRPExcelDealer.Sep = Math.Round(objRdr.GetDouble(13))
    '                kol += 1
    '                _PRPExcelDealer.Oct = Math.Round(objRdr.GetDouble(14))
    '                kol += 1
    '                _PRPExcelDealer.Nov = Math.Round(objRdr.GetDouble(15))
    '                kol += 1
    '                _PRPExcelDealer.Dec = Math.Round(objRdr.GetDouble(16))
    '                kol += 1
    '                _PRPExcelDealer.Total = Math.Round(objRdr.GetDouble(17))
    '                kol += 1
    '                dataCol.Add(_PRPExcelDealer)
    '            End While
    '        Catch ex As Exception
    '            Throw New Exception("Data pada sheet PERDLR tidak valid. Baris:" + brs.ToString + " Kolom:" + kol.ToString)
    '        End Try
    '        Return dataCol
    '    End Function

    '    Public Function IsExcelValid(ByVal fileName As String) As Integer
    '        Dim objConn As OdbcConnection

    '        Dim nResult As Integer = ValidStatus.Valid

    '        Try
    '            Dim strConn As String = StrConnection & fileName
    '            objConn = New OdbcConnection(strConn)
    '            objConn.Open()

    '            'Dim _hasDlrSheet As Boolean = ErrorDLRSheet(objConn)
    '            Dim _hasTokoSheet As Boolean = ErrorTokoSheet(objConn)

    '            'If Not _hasDlrSheet Then
    '            '    nResult = ValidStatus.ErrorDealer
    '            'End If

    '            If Not _hasTokoSheet Then
    '                nResult = ValidStatus.ErrorToko
    '            End If

    '            If Not _hasTokoSheet Then
    '                nResult = ValidStatus.InValidFile
    '            End If

    '            If nResult = ValidStatus.Valid Then
    '                TryParse(fileName, nResult)
    '            End If
    '        Catch
    '            If nResult <> ValidStatus.ErrorDataInSheetToko Then
    '                nResult = ValidStatus.InValidFile
    '            End If
    '        Finally
    '            If objConn.State = ConnectionState.Open Then
    '                objConn.Close()
    '            End If
    '        End Try
    '        Return nResult
    '    End Function

    '    Private Function ErrorDLRSheet(ByVal objConn As IDbConnection) As Boolean
    '        Dim objRdr As OdbcDataReader
    '        Try
    '            Dim objCmd As OdbcCommand = New OdbcCommand("SELECT * FROM " & PerDLRSheet, objConn)
    '            objCmd.CommandType = CommandType.Text
    '            objRdr = objCmd.ExecuteReader()
    '            If IsNothing(objRdr) Then
    '                Return False
    '            End If
    '            If objRdr.FieldCount <> 18 Then
    '                Return False
    '            End If
    '            Return True
    '        Catch ex As Exception
    '            Return False
    '        Finally
    '            If Not objRdr.IsClosed Then
    '                objRdr.Close()
    '            End If
    '        End Try
    '    End Function

    '    Private Function ErrorTokoSheet(ByVal objConn As IDbConnection) As Boolean
    '        Dim objRdr As OdbcDataReader
    '        Try
    '            Dim objCmd As OdbcCommand = New OdbcCommand("SELECT * FROM " & PerTokoSheet, objConn)
    '            objCmd.CommandType = CommandType.Text
    '            objRdr = objCmd.ExecuteReader()
    '            If IsNothing(objRdr) Then
    '                Return False
    '            End If
    '            If objRdr.FieldCount <> 17 Then
    '                Return False
    '            End If
    '            Return True
    '        Catch
    '            Throw
    '        Finally
    '            If Not objRdr.IsClosed Then
    '                objRdr.Close()
    '            End If
    '        End Try
    '    End Function

    '    Private Sub TryParse(ByVal fileName As String, ByRef retVal As Integer)
    '        Try
    '            ParsingExcelNoTransaction(fileName, PerTokoSheet, "")
    '        Catch
    '            retVal = ValidStatus.ErrorDataInSheetToko
    '        End Try

    '        'Try
    '        '    ParsingExcelNoTransaction(fileName, PerDLRSheet, "")
    '        'Catch
    '        '    retVal = ValidStatus.ErrorDataInSheetDealer
    '        'End Try
    '    End Sub

    'End Class

#End Region
End Namespace



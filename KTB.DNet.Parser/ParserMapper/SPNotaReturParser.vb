#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Text
#End Region

#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Sparepart
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Security.Principal
#End Region

Namespace KTB.DNet.Parser
    Public Class SPNotaReturParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _fileName As String
        Private _stream As StreamReader
        Public ErrorMessage As StringBuilder
        Private grammar As Regex
        Private _NotaRetur As SparepartNotaRetur
        Private _ArrNotaRetur As ArrayList
#End Region

        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Return Nothing

            '_ArrNotaRetur = New ArrayList
            '_fileName = fileName  '-- The file that gets parsed
            '_stream = New StreamReader(fileName, True)  '-- Open stream
            'Dim StreamLine As String = MyBase.NextLine(_stream).Trim()  '-- Header line
            'While (Not StreamLine = "")
            '    Try
            '        ParseSparepartNotaRetur(StreamLine & ";")
            '    Catch ex As Exception
            '        _ArrNotaRetur = Nothing  '-- If any error occurs then set with Nothing
            '        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SPNotaReturParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SPNotaReturParser, BlockName)
            '        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & ex.Message)
            '        ExceptionPolicy.HandleException(e, "Parser Policy")
            '    End Try
            '    StreamLine = MyBase.NextLine(_stream)
            'End While
            '_stream.Close()  '-- Close & dispose stream
            '_stream = Nothing
            'Return _ArrNotaRetur  '-- Successfully read deposit text file, so return it

        End Function

        Protected Overrides Function DoTransaction() As Integer
            Return 0

            'Try
            '    If _ArrNotaRetur.Count > 0 Then
            '        For Each item As SparepartNotaRetur In _ArrNotaRetur
            '            Dim _NotaReturFacade As New SparepartNotaReturFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            '            _NotaReturFacade.Insert(item)
            '        Next

            '    End If
            'Catch ex As Exception
            '    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SPNotaReturParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SPNotaReturParser, BlockName)
            '    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & ex.Message)
            '    ExceptionPolicy.HandleException(e, "Parser Policy")
            'End Try

        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim UserNR As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim _NotaReturFacade As New SparepartNotaReturFacade(UserNR)
            Dim _NotaRetur As New SparepartNotaRetur
            Dim finfo As New FileInfo(fileName)
            Dim fullName As String = finfo.FullName
            Dim newFileName As String = String.Empty
            ErrorMessage = New StringBuilder()

            If finfo.Exists Then
                Dim fName As String = finfo.Name.Substring(0, finfo.Name.Length - 4)
                Dim Dealer As String = fName.Split("_")(1)
                Dim DocNumber As String = fName.Split("_")(2)
                Dim DocType As String = fName.Split("_")(3)
                Dim MonthP As String = fName.Split("_")(4)
                Dim YearP As String = fName.Split("_")(5)

                Try
                    Dim _dealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(Dealer)
                    If _dealer.ID > 0 Then
                        _NotaRetur.Dealer = _dealer
                    Else
                        ErrorMessage.Append("Dealer code tidak terdefinisi." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append(ex.Message.ToString() & "Dealer code tidak terdefinisi." & Chr(13) & Chr(10))
                End Try

                Try
                    Dim ctg As String = "EnumTOPSPNotaRetur.Document"
                    Dim oTypeDoc As StandardCode = New StandardCodeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).GetByCategoryValueCode(ctg, DocType)
                    If oTypeDoc.ID > 0 Then
                        _NotaRetur.TypeDoc = oTypeDoc.ValueId
                        newFileName = oTypeDoc.ValueCode & "_" & DocNumber & "_" & Dealer & "_" & MonthP & "_" & YearP & finfo.Extension
                    Else
                        ErrorMessage.Append("Type Document tidak terdefinisi." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append(ex.Message.ToString() & "Type Document tidak terdefinisi." & Chr(13) & Chr(10))
                End Try
                _NotaRetur.NoDoc = DocNumber
                _NotaRetur.PeriodeMonth = MonthP
                _NotaRetur.PeriodeYear = YearP
                _NotaRetur.FileNamePath = KTB.DNet.Lib.WebConfig.GetValue("SpNotaReturPath") & "\" & Date.Now.Year & "\" & newFileName
            Else
                Throw New Exception("File Tidak Ditemukan")
            End If

            Try
                If Not IsNothing(_NotaRetur) Then
                    If _NotaRetur.ErrorMessage = String.Empty Then
                        If _NotaReturFacade.Insert(_NotaRetur) < 0 Then
                            nError += 1
                        End If
                    Else
                        Throw New Exception(_NotaRetur.ErrorMessage)
                        nError += 1
                    End If
                    Dim debug = ""
                    Return newFileName.ToString
                End If
            Catch ex As Exception
                sMsg &= ex.Message.ToString() & ";"
                nError += 1
            End Try

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _NotaRetur.NoDoc, "ws-worker", "SPNotaReturParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SPNotaReturParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "SPNotaReturParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SPNotaReturParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return ""

        End Function

#End Region

        Private Sub ParseSparepartNotaRetur(ByVal streamLine As String)
            _NotaRetur = New SparepartNotaRetur
            ErrorMessage = New StringBuilder
            Dim sStart As Integer = 0
            Dim nCount As Integer = 0
            Dim sColumn As String

            For Each m As Match In grammar.Matches(streamLine)

                sColumn = streamLine.Substring(sStart, m.Index - sStart)
                sColumn = sColumn.Trim()  '-- Truncate leading & trailing spaces

                Select Case nCount
                    Case Is = 0  '-- Dealer
                        If sColumn.Length > 0 Then
                            Try
                                Dim _dealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sColumn)
                                If _dealer.ID > 0 Then
                                    _NotaRetur.Dealer = _dealer
                                Else
                                    ErrorMessage.Append("Dealer code tidak terdefinisi." & Chr(13) & Chr(10))
                                End If
                            Catch ex As Exception
                                ErrorMessage.Append(ex.Message.ToString() & "Dealer code tidak terdefinisi." & Chr(13) & Chr(10))
                            End Try
                        Else
                            ErrorMessage.Append("Dealer code tidak terdefinisi." & Chr(13) & Chr(10))
                        End If
                    Case Is = 1  '-- NoDoc 
                        If sColumn.Length > 0 Then
                            _NotaRetur.NoDoc = sColumn
                        Else
                            ErrorMessage.Append("Invalid No Document." & Chr(13) & Chr(10))
                        End If
                    Case Is = 2  '-- TypeDoc
                        If sColumn.Length > 0 Then
                            Try
                                Dim ctg As String = "EnumTOPSPNotaRetur.Document"
                                Dim oTypeDoc As StandardCode = New StandardCodeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).GetByCategoryValueCode(ctg, sColumn)
                                If oTypeDoc.ID > 0 Then
                                    _NotaRetur.TypeDoc = oTypeDoc.ValueId
                                Else
                                    ErrorMessage.Append("Type Document tidak terdefinisi." & Chr(13) & Chr(10))
                                End If
                            Catch ex As Exception
                                ErrorMessage.Append(ex.Message.ToString() & "Type Document tidak terdefinisi." & Chr(13) & Chr(10))
                            End Try
                        Else
                            ErrorMessage.Append("Type Document tidak terdefinisi." & Chr(13) & Chr(10))
                        End If
                    Case Is = 3  '-- PeriodeMonth 
                        If sColumn.Length > 0 Then
                            _NotaRetur.PeriodeMonth = sColumn
                        Else
                            ErrorMessage.Append("Invalid Periode Month " & Chr(13) & Chr(10))
                        End If
                    Case Is = 4  '-- PeriodeYear
                        If sColumn.Length > 0 Then
                            _NotaRetur.PeriodeYear = sColumn
                        Else
                            ErrorMessage.Append("Invalid Periode Year " & Chr(13) & Chr(10))
                        End If
                    Case Is = 5  '-- Filepath
                        If sColumn.Length > 0 Then
                            _NotaRetur.FileNamePath = "NotaRetur\" & sColumn
                        Else
                            ErrorMessage.Append("Invalid File Name Path " & Chr(13) & Chr(10))
                        End If
                End Select

                sStart = m.Index + 1  '-- Next start index
                nCount += 1           '-- Next column
            Next
            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            Else
                _ArrNotaRetur.Add(_NotaRetur)
            End If
        End Sub

    End Class
End Namespace
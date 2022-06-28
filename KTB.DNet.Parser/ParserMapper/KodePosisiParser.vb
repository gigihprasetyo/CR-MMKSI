
#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Text
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class KodePosisiParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _KodePosisies As ArrayList
        Private Grammar As Regex
        Private mapper As IMapper
        Private objTransactionManager As TransactionManager
        Private _fileName As String
        Private sBuilder As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser(";")
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            _KodePosisies = New ArrayList
            _fileName = fileName
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            While (Not val = "")
                Try
                    ParseKodePosisi(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "KodePosisiParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.KodePosisiParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return _KodePosisies
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Try
                If _KodePosisies.Count > 0 Then
                    Dim insertResult As Integer
                    For Each item As DeskripsiKodePosisi In _KodePosisies
                        Try
                            Dim objKodePosisiFacade As DeskripsiPositionCodeFacade = New DeskripsiPositionCodeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            Dim oldItem As DeskripsiKodePosisi = GetExistingKodePosisi(item)
                            If oldItem.ID > 0 Then
                                oldItem.Description = item.Description
                                insertResult = objKodePosisiFacade.Update(oldItem)
                                If insertResult < 0 Then
                                    Throw New Exception("Failed Update Database")
                                End If
                            Else
                                insertResult = objKodePosisiFacade.Insert(item)
                                If insertResult < 0 Then
                                    Throw New Exception("Failed insert into Database")
                                End If
                            End If
                        Catch ex As Exception
                            Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.KodePosition & Chr(13) & Chr(10) & ex.Message)
                            Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        End Try
                    Next
                End If
            Catch ex As Exception
                'Throw ex
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "KodePosisi", "KodePosisiParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.KodePosisiParser, BlockName)
            End Try
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"
        Private Function GetExistingKodePosisi(ByVal objKodePosisi As DeskripsiKodePosisi) As DeskripsiKodePosisi
            Dim _objKodePosisi As DeskripsiKodePosisi = New DeskripsiPositionCodeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(objKodePosisi.KodePosition)
            Return _objKodePosisi
        End Function

        Private Sub ParseKodePosisi(ByVal ValParser As String)
            Dim _kodePosisi As DeskripsiKodePosisi = New DeskripsiKodePosisi
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            sBuilder = New StringBuilder
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        If sTemp.Length > 0 Then
                            _kodePosisi.KodePosition = sTemp.Trim
                        Else
                            sBuilder.Append("Invalid Kode Posisi" & Chr(13) & Chr(10))
                        End If
                    Case Is = 1
                        If sTemp.Length > 0 Then
                            _kodePosisi.Description = sTemp.Trim
                        Else
                            sBuilder.Append("Invalid Description" & Chr(13) & Chr(10))
                        End If
                        'Case Is = 2
                        '    If sTemp.Length > 0 Then
                        '        Try
                        '            Dim status As Integer = CInt(sTemp.Trim)
                        '            _kodePosisi.Status = status
                        '        Catch ex As Exception
                        '            sBuilder.Append("Invalid Status" & Chr(13) & Chr(10))
                        '        End Try
                        '    Else
                        '            sBuilder.Append("Invalid Status" & Chr(13) & Chr(10))
                        '    End If
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            If sBuilder.Length > 0 Then
                Throw New Exception(sBuilder.ToString)
            Else
                _KodePosisies.Add(_kodePosisi)
            End If

        End Sub

#End Region

    End Class
End Namespace

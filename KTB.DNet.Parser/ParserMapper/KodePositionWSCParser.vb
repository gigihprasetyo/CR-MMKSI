 

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
    Public Class KodePositionWSCParser
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
                    ParseKodePosisiWSC(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "KodePositionWSCParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.KodePositionWSCParser, BlockName)
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
                    For Each item As KodePostionWSC In _KodePosisies
                        Try
                            Dim objKodePosisiFacade As KodePostionWSCFacade = New KodePostionWSCFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KodePostionWSC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KodePostionWSC), "CategoryCode", MatchType.Exact, item.CategoryCode))
                            criterias.opAnd(New Criteria(GetType(KodePostionWSC), "PostionCode", MatchType.Exact, item.PostionCode))
                            criterias.opAnd(New Criteria(GetType(KodePostionWSC), "Code", MatchType.Exact, item.Code))
                            Dim oldObjList As ArrayList = objKodePosisiFacade.Retrieve(criterias)
                            If oldObjList.Count < 1 Then
                                Dim facade As KodePostionWSCFacade = New KodePostionWSCFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                facade.Insert(item)
                            End If
                        Catch ex As Exception
                            Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.Code & Chr(13) & Chr(10) & ex.Message)
                            Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        End Try
                    Next
                End If
            Catch ex As Exception
                'Throw ex
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "KodePositionWSCParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.KodePositionWSCParser, BlockName)
            End Try
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"
        
        Private Sub ParseKodePosisiWSC(ByVal ValParser As String)
            Dim _kodePosisi As KodePostionWSC = New KodePostionWSC
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
                            _kodePosisi.CategoryCode = sTemp.Trim.ToUpper
                        Else
                            sBuilder.Append("Invalid kategori Kode Posisi" & Chr(13) & Chr(10))
                        End If
                    Case Is = 1
                        If sTemp.Length > 0 Then
                            _kodePosisi.PostionCode = sTemp.Trim.ToUpper
                        Else
                            sBuilder.Append("Invalid Kode Posisi" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        If sTemp.Length > 0 Then
                            _kodePosisi.Code = sTemp.Trim.ToUpper
                        Else
                            sBuilder.Append("Invalid Kode" & Chr(13) & Chr(10))
                        End If
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

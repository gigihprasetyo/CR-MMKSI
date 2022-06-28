#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports Newtonsoft.Json
Imports System.Collections.Generic
Imports System.Linq
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Parser.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Globalization
#End Region

Namespace KTB.DNet.Parser

    Public Class FlatRateMasterPMParser
        Inherits AbstractParser

#Region "Private Variables"
        Private grammar As Regex
        Private _FlatRateMasterPM As FlatRateMasterPM
        Private jsonData As List(Of FlatRateMasterPMJson)
        Private errorMessage As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"
        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub
#End Region

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Throw New NotImplementedException()
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Throw New NotImplementedException()
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Throw New NotImplementedException()
        End Function

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String, ByRef msg As String) As Object
            Try
                msg = String.Empty
                jsonData = JsonConvert.DeserializeObject(Of List(Of FlatRateMasterPMJson))(Content)
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "json-worker", "FlatRateMasterPMParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.FlatRateMasterPMParser, BlockName)
                Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                jsonData = Nothing

                Dim err As String = String.Format("Parsing Error : {0}", ex.Message)
                msg = IIf(String.IsNullorEmpty(msg), err, String.Concat(msg, " | ", err))
            End Try

            Return jsonData
        End Function

        Protected Overrides Function DoTransaction(ByRef msg As String) As Integer
            Dim FlatRateMasterPMFacade As FlatRateMasterPMFacade
            Dim isSuccess As Integer = 1
            For Each data As FlatRateMasterPMJson In jsonData.Distinct()
                Try
                    _FlatRateMasterPM = ParseFlatRateMasterPM(data)
                    If Validate(_FlatRateMasterPM, data, msg) Then
                        FlatRateMasterPMFacade = New FlatRateMasterPMFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
                        If _FlatRateMasterPM.ID = 0 Then
                            FlatRateMasterPMFacade.Insert(_FlatRateMasterPM)
                        Else
                            FlatRateMasterPMFacade.Update(_FlatRateMasterPM)
                        End If
                    Else
                        isSuccess = 0
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "WSJson-worker", "FlatRateMasterPMParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.FlatRateMasterPMParser)
                    Dim e As Exception = New Exception("FlatRateMasterPM" & Chr(13) & Chr(10) & ex.Message)
                    If ex.Message.Substring(0, 3) = "O/C" Then
                        Throw New System.Exception("O/C status in D-NET is not locked. Insert/Update failed.")
                    End If
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    isSuccess = 0

                    Dim err As String = String.Format("Transaction ({0}, {1}) Error : {2}", data.Varian, data.PMCode, ex.Message)
                    msg = IIf(String.IsNullorEmpty(msg), err, String.Concat(msg, " | ", err))
                End Try
            Next

            Return isSuccess
        End Function

#Region "Private Methods"
        Private Function Validate(ByVal obj As FlatRateMasterPM, ByVal data As FlatRateMasterPMJson, ByRef msg As String) As Boolean
            Dim err As String = String.Empty
            If IsNothing(obj.PMKind) Then
                err = String.Format("PMCode : '{0}' not found on D-Net.", data.PMCode)
            ElseIf String.IsNullorEmpty(obj.Varian) Then
                err = String.Format("Varian : '{0}' is empty.", data.Varian)
                'ElseIf obj.FlatRate = 0 Then
                '    err = String.Format("FlatRate : '{0}' must greater than 0.", data.FlatRate)
            End If

            If Not String.IsNullorEmpty(err) Then
                SysLogParameter.LogErrorToSyslog(err, "WSJson-worker", "FlatRateMasterPMParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.FlatRateMasterPMParser)
                msg = IIf(String.IsNullorEmpty(msg), err, String.Concat(msg, " | ", err))
            End If

            Return String.IsNullorEmpty(err)
        End Function

        Private Function ParseFlatRateMasterPM(ByVal data As FlatRateMasterPMJson) As FlatRateMasterPM
            'Dim cultureInfo As CultureInfo = New CultureInfo("en-US")
            Dim crit As CriteriaComposite
            Dim FlatRateMasterPMFacade As New FlatRateMasterPMFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim PMKindFacade As New PMKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            crit = New CriteriaComposite(New Criteria(GetType(FlatRateMasterPM), "RowStatus", MatchType.Exact, 0))
            crit.opAnd(New Criteria(GetType(FlatRateMasterPM), "Varian", MatchType.Exact, data.Varian))
            crit.opAnd(New Criteria(GetType(FlatRateMasterPM), "PMKind.KindCode", MatchType.Exact, data.PMCode))

            Dim arr As ArrayList = FlatRateMasterPMFacade.Retrieve(crit)
            If arr.Count = 0 Then
                _FlatRateMasterPM = New FlatRateMasterPM
            Else
                _FlatRateMasterPM = CType(arr(0), FlatRateMasterPM)
            End If

            _FlatRateMasterPM.Varian = data.Varian
            _FlatRateMasterPM.PMKind = PMKindFacade.Retrieve(data.PMCode)
            If String.IsNullorEmpty(data.FlatRate) Then
                _FlatRateMasterPM.FlatRate = 0
            Else
                _FlatRateMasterPM.FlatRate = Convert.ToDecimal(data.FlatRate)
            End If
            _FlatRateMasterPM.Status = "1"

            Return _FlatRateMasterPM
        End Function
#End Region

    End Class

End Namespace

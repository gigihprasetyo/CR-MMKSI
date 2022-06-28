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
Imports System.Globalization
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser

    Public Class ServiceTemplatePMLaborParser
        Inherits AbstractParser

#Region "Private Variables"
        Private grammar As Regex
        Private _ServiceTemplatePMLabor As ServiceTemplatePMLabor
        Private jsonData As List(Of ServiceTemplatePMLaborJson)
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
                jsonData = JsonConvert.DeserializeObject(Of List(Of ServiceTemplatePMLaborJson))(Content)
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "json-worker", "ServiceTemplatePMLaborParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ServiceTemplatePMLaborParser, BlockName)
                Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                jsonData = Nothing

                Dim err As String = String.Format("Parsing Error : {0}", ex.Message)
                msg = IIf(String.IsNullorEmpty(msg), err, String.Concat(msg, " | ", err))
            End Try

            Return jsonData
        End Function

        Protected Overrides Function DoTransaction(ByRef msg As String) As Integer
            Dim ServiceTemplatePMLaborFacade As ServiceTemplatePMLaborFacade
            Dim isSuccess As Integer = 1
            For Each data As ServiceTemplatePMLaborJson In jsonData.Distinct()
                Try
                    _ServiceTemplatePMLabor = ParseServiceTemplatePMLabor(data)
                    If Validate(_ServiceTemplatePMLabor, data, msg) Then
                        ServiceTemplatePMLaborFacade = New ServiceTemplatePMLaborFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
                        If _ServiceTemplatePMLabor.ID = 0 Then
                            ServiceTemplatePMLaborFacade.Insert(_ServiceTemplatePMLabor)
                        Else
                            ServiceTemplatePMLaborFacade.Update(_ServiceTemplatePMLabor)
                        End If
                    Else
                        isSuccess = 0
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "WSJson-worker", "ServiceTemplatePMLaborParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ServiceTemplatePMLaborParser)
                    Dim e As Exception = New Exception("ServiceTemplatePMLabor" & Chr(13) & Chr(10) & ex.Message)
                    If ex.Message.Substring(0, 3) = "O/C" Then
                        Throw New System.Exception("O/C status in D-NET is not locked. Insert/Update failed.")
                    End If
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    isSuccess = 0

                    Dim err As String = String.Format("Transaction ({0}, {1}, {2}) Error : {3}", data.KindofService, data.Customer, data.MaterialGroup, ex.Message)
                    msg = IIf(String.IsNullorEmpty(msg), err, String.Concat(msg, " | ", err))
                End Try
            Next

            Return isSuccess
        End Function

#Region "Private Methods"
        Private Function Validate(ByVal obj As ServiceTemplatePMLabor, ByVal data As ServiceTemplatePMLaborJson, ByRef msg As String) As Boolean
            Dim err As String = String.Empty
            If IsNothing(obj.PMKind) Then
                err = String.Format("KindOfService : '{0}' not found on D-Net.", data.KindofService)
            ElseIf IsNothing(obj.Dealer) Then
                err = String.Format("Customer : '{0}' not found on D-Net.", data.Customer)
            ElseIf IsNothing(obj.VechileType) Then
                err = String.Format("MaterialGroup : '{0}' not found on D-Net.", data.MaterialGroup)
                'ElseIf obj.LaborCost = 0 Then
                '    err = String.Format("RoundupLC : '{0}' must greater than 0.", data.RoundupLC)
            ElseIf obj.LaborDuration = 0 Then
                err = String.Format("Hours : '{0}' must greater than 0.", data.Hours)
            End If

            If Not String.IsNullorEmpty(err) Then
                SysLogParameter.LogErrorToSyslog(err, "WSJson-worker", "ServiceTemplatePMLaborParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ServiceTemplatePMLaborParser)
                msg = IIf(String.IsNullorEmpty(msg), err, String.Concat(msg, " | ", err))
            End If

            Return String.IsNullorEmpty(err)
        End Function

        Private Function ParseServiceTemplatePMLabor(ByVal data As ServiceTemplatePMLaborJson) As ServiceTemplatePMLabor
            Dim cultureInfo As CultureInfo = New CultureInfo("en-US")
            Dim crit As CriteriaComposite
            Dim ServiceTemplatePMLaborFacade As New ServiceTemplatePMLaborFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim PMKindFacade As New PMKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim dealerFacade As New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim vechileTypeFacade As New VechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))

            crit = New CriteriaComposite(New Criteria(GetType(ServiceTemplatePMLabor), "RowStatus", MatchType.Exact, 0))
            crit.opAnd(New Criteria(GetType(ServiceTemplatePMLabor), "Dealer.DealerCode", MatchType.Exact, data.Customer))
            crit.opAnd(New Criteria(GetType(ServiceTemplatePMLabor), "PMKind.KindCode", MatchType.Exact, data.KindofService))
            crit.opAnd(New Criteria(GetType(ServiceTemplatePMLabor), "VechileType.VechileTypeCode", MatchType.Exact, data.MaterialGroup))
            crit.opAnd(New Criteria(GetType(ServiceTemplatePMLabor), "ValidFrom", MatchType.Exact, CDate(data.ValidFrom)))

            Dim arr As ArrayList = ServiceTemplatePMLaborFacade.Retrieve(crit)
            If arr.Count = 0 Then
                _ServiceTemplatePMLabor = New ServiceTemplatePMLabor
            Else
                _ServiceTemplatePMLabor = CType(arr(0), ServiceTemplatePMLabor)
            End If

            _ServiceTemplatePMLabor.Dealer = dealerFacade.Retrieve(data.Customer)
            _ServiceTemplatePMLabor.PMKind = PMKindFacade.Retrieve(data.KindofService)
            _ServiceTemplatePMLabor.VechileType = vechileTypeFacade.Retrieve(data.MaterialGroup)
            _ServiceTemplatePMLabor.LaborDuration = Convert.ToDecimal(data.Hours, cultureInfo)
            If String.IsNullorEmpty(data.RoundupLC) Then
                _ServiceTemplatePMLabor.LaborCost = 0
            Else
                _ServiceTemplatePMLabor.LaborCost = Convert.ToDecimal(data.RoundupLC, cultureInfo)
            End If
            _ServiceTemplatePMLabor.ValidFrom = CDate(data.ValidFrom)

            Return _ServiceTemplatePMLabor
        End Function
#End Region

    End Class

End Namespace

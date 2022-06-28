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

    Public Class ServiceTemplateFFLaborParser
        Inherits AbstractParser

#Region "Private Variables"
        Private grammar As Regex
        Private _ServiceTemplateFFLabor As ServiceTemplateFFLabor
        Private jsonData As List(Of ServiceTemplateFFLaborJson)
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
                jsonData = JsonConvert.DeserializeObject(Of List(Of ServiceTemplateFFLaborJson))(Content)
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "json-worker", "ServiceTemplateFFLaborParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ServiceTemplateFFLaborParser, BlockName)
                Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                jsonData = Nothing

                Dim err As String = String.Format("Parsing Error : {0}", ex.Message)
                msg = IIf(String.IsNullorEmpty(msg), err, String.Concat(msg, " | ", err))
            End Try

            Return jsonData
        End Function

        Protected Overrides Function DoTransaction(ByRef msg As String) As Integer
            Dim ServiceTemplateFFLaborFacade As ServiceTemplateFFLaborFacade
            Dim isSuccess As Integer = 1
            For Each data As ServiceTemplateFFLaborJson In jsonData.Distinct()
                Try
                    _ServiceTemplateFFLabor = ParseServiceTemplateFFLabor(data)
                    If Validate(_ServiceTemplateFFLabor, data, msg) Then
                        ServiceTemplateFFLaborFacade = New ServiceTemplateFFLaborFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
                        If _ServiceTemplateFFLabor.ID = 0 Then
                            ServiceTemplateFFLaborFacade.Insert(_ServiceTemplateFFLabor)
                        Else
                            ServiceTemplateFFLaborFacade.Update(_ServiceTemplateFFLabor)
                        End If
                    Else
                        isSuccess = 0
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "WSJson-worker", "ServiceTemplateFFLaborParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ServiceTemplateFFLaborParser)
                    Dim e As Exception = New Exception("ServiceTemplateFFLabor" & Chr(13) & Chr(10) & ex.Message)
                    If ex.Message.Substring(0, 3) = "O/C" Then
                        Throw New System.Exception("O/C status in D-NET is not locked. Insert/Update failed.")
                    End If
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    isSuccess = 0

                    Dim err As String = String.Format("Transaction ({0}, {1}) Error : {3}", data.Customer, data.Material, ex.Message)
                    msg = IIf(String.IsNullorEmpty(msg), err, String.Concat(msg, " | ", err))
                End Try
            Next

            Return isSuccess
        End Function

#Region "Private Methods"
        Private Function Validate(ByVal obj As ServiceTemplateFFLabor, ByVal data As ServiceTemplateFFLaborJson, ByRef msg As String) As Boolean
            Dim err As String = String.Empty
            'If IsNothing(obj.RecallCategory) Then
            '    err = String.Format("KindOfService : '{0}' not found on D-Net.", data.KindofService)
            If IsNothing(obj.Dealer) Then
                err = String.Format("Customer : '{0}' not found on D-Net.", data.Customer)
                'ElseIf IsNothing(obj.VechileType) Then
                '    err = String.Format("Material : '{0}' not found on D-Net.", data.Material)
                'ElseIf obj.LaborCost = 0 Then
                '    err = String.Format("RoundupLC : '{0}' must greater than 0.", data.RoundupLC)
                'ElseIf obj.LaborDuration = 0 Then
                '    err = String.Format("Hours : '{0}' must greater than 0.", data.Hours)
            End If

            If Not String.IsNullorEmpty(err) Then
                SysLogParameter.LogErrorToSyslog(err, "WSJson-worker", "ServiceTemplateFFLaborParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ServiceTemplateFFLaborParser)
                msg = IIf(String.IsNullorEmpty(msg), err, String.Concat(msg, " | ", err))
            End If

            Return String.IsNullorEmpty(err)
        End Function

        Private Function ParseServiceTemplateFFLabor(ByVal data As ServiceTemplateFFLaborJson) As ServiceTemplateFFLabor
            'Dim cultureInfo As CultureInfo = New CultureInfo("en-US")
            Dim crit As CriteriaComposite
            Dim ServiceTemplateFFLaborFacade As New ServiceTemplateFFLaborFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim RecallCategoryFacade As New RecallCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim dealerFacade As New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim vechileTypeFacade As New VechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))

            crit = New CriteriaComposite(New Criteria(GetType(ServiceTemplateFFLabor), "RowStatus", MatchType.Exact, 0))
            crit.opAnd(New Criteria(GetType(ServiceTemplateFFLabor), "Dealer.DealerCode", MatchType.Exact, data.Customer))
            crit.opAnd(New Criteria(GetType(ServiceTemplateFFLabor), "Material", MatchType.Exact, data.Material))
            crit.opAnd(New Criteria(GetType(ServiceTemplateFFLabor), "ValidFrom", MatchType.Exact, CDate(data.ValidFrom)))

            Dim arr As ArrayList = ServiceTemplateFFLaborFacade.Retrieve(crit)
            If arr.Count = 0 Then
                _ServiceTemplateFFLabor = New ServiceTemplateFFLabor
            Else
                _ServiceTemplateFFLabor = CType(arr(0), ServiceTemplateFFLabor)
            End If

            _ServiceTemplateFFLabor.Dealer = dealerFacade.Retrieve(data.Customer)
            _ServiceTemplateFFLabor.Material = data.Material
            '_ServiceTemplateFFLabor.RecallCategory = RecallCategoryFacade.Retrieve(data.KindofService)
            '_ServiceTemplateFFLabor.VechileType = vechileTypeFacade.Retrieve(data.Material)
            '_ServiceTemplateFFLabor.LaborDuration = Convert.ToDecimal(data.Hours, cultureInfo)
            If String.IsNullorEmpty(data.LaborCost) Then
                _ServiceTemplateFFLabor.LaborCost = 0
            Else
                _ServiceTemplateFFLabor.LaborCost = Convert.ToDecimal(data.LaborCost)
            End If

            _ServiceTemplateFFLabor.ValidFrom = CDate(data.ValidFrom)

            Return _ServiceTemplateFFLabor
        End Function
#End Region

    End Class

End Namespace

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
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.SparePart
Imports System.Globalization
Imports KTB.DNet.BusinessFacade.PO

#End Region

Namespace KTB.DNet.Parser

    Public Class SOPPHOnlineMasterParser
        Inherits AbstractParser

#Region "Private Variables"
        Private jsonData As List(Of SOPPHDataJson)
        Private errorMessage As StringBuilder
        Private identity As New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing)
#End Region

#Region "Constructors/Destructors/Finalizers"
        Public Sub New()
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
                jsonData = JsonConvert.DeserializeObject(Of List(Of SOPPHDataJson))(Content)
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "json-worker", "SOPPHOnlineMasterParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ServiceTemplateFSPartParser, BlockName)
                Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                jsonData = Nothing

                Dim err As String = String.Format("Parsing Error : {0}", ex.Message)
                msg = IIf(String.IsNullorEmpty(msg), err, String.Concat(msg, " | ", err))
            End Try

            Return jsonData
        End Function

        Protected Overrides Function DoTransaction(ByRef msg As String) As Integer
            Try
                Dim result As Integer = 0
                If jsonData.Count > 0 Then
                    For Each obj As SOPPHDataJson In jsonData.OrderBy(Function(x) x.DocReference).ToList()
                        Dim existObj As New SalesOrderInterest()
                        Dim isReferenced As Boolean = False
                        If Not obj.DocReference = String.Empty Then
                            isReferenced = processReferenced(obj)
                        End If

                        If Not isReferenced Then
                            If isExist(obj, existObj) Then
                                existObj.Dealer = New DealerFacade(identity).Retrieve(obj.DealerCode)
                                existObj.BillingDate = obj.BillingDate
                                existObj.AdditionalAmount = obj.Amount
                                existObj.PPHAmount = obj.PPH
                                existObj.DPPAmount = obj.DPP
                                existObj.DocReference = obj.DocReference
                                existObj.DocNumber = obj.DocNumber
                                result = New SalesOrderInterestFacade(identity).Update(existObj)
                            Else
                                Dim newObj As New SalesOrderInterest()
                                newObj.Dealer = New DealerFacade(identity).Retrieve(obj.DealerCode)
                                newObj.BillingNumber = obj.BillingNumber
                                newObj.BillingDate = obj.BillingDate
                                newObj.AdditionalAmount = obj.Amount
                                newObj.PPHAmount = obj.PPH
                                newObj.DPPAmount = obj.DPP
                                newObj.SONumber = obj.SONumber
                                newObj.SalesOrder = getSO(obj.SONumber)
                                newObj.DocReference = obj.DocReference
                                newObj.TrType = obj.TrType
                                newObj.DocNumber = obj.DocNumber
                                newObj.Status = IIf(String.IsNullorEmpty(obj.DocReference), -1, 5)
                                result = New SalesOrderInterestFacade(identity).Insert(newObj)
                            End If
                        Else
                            Return True
                        End If
                    Next
                End If
                Return result
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "WSJson-worker", "SOPPHOnlineMasterParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ContractParser)
                Dim e As Exception = New Exception("SOPPHData" & Chr(13) & Chr(10) & JsonConvert.SerializeObject(jsonData) & Chr(13) & Chr(10) & ex.Message)
                If ex.Message.Substring(0, 3) = "O/C" Then
                    Throw New System.Exception("O/C status in D-NET is not locked. Update failed.")
                End If
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Return -1
            End Try
        End Function

#Region "Private Methods"
        Private Function isExist(ByVal obj As SOPPHDataJson, Optional ByRef existingObj As SalesOrderInterest = Nothing) As Boolean
            Dim crit As New CriteriaComposite(New Criteria(GetType(SalesOrderInterest), "RowStatus", MatchType.Exact, 0))
            crit.opAnd(New Criteria(GetType(SalesOrderInterest), "SONumber", MatchType.Exact, obj.SONumber))
            crit.opAnd(New Criteria(GetType(SalesOrderInterest), "BillingNumber", MatchType.Exact, obj.BillingNumber))
            crit.opAnd(New Criteria(GetType(SalesOrderInterest), "TrType", MatchType.Exact, obj.TrType))
            Dim arrExistingObj As ArrayList = New SalesOrderInterestFacade(identity).Retrieve(crit)

            If arrExistingObj.Count > 0 Then
                existingObj = CType(arrExistingObj(0), SalesOrderInterest)
                Return True
            End If

            Return False
        End Function



        Private Function processReferenced(ByVal obj As SOPPHDataJson) As Boolean
            Dim crit As New CriteriaComposite(New Criteria(GetType(SalesOrderInterest), "RowStatus", MatchType.Exact, 0))
            'crit.opAnd(New Criteria(GetType(SalesOrderInterest), "SONumber", MatchType.Exact, obj.SONumber))
            crit.opAnd(New Criteria(GetType(SalesOrderInterest), "BillingNumber", MatchType.Exact, obj.DocReference))
            Dim arrExistingObj As ArrayList = New SalesOrderInterestFacade(identity).Retrieve(crit)

            If arrExistingObj.Count > 0 Then
                Dim tempExisting As SalesOrderInterest = CType(arrExistingObj(0), SalesOrderInterest)
                tempExisting.Status = 5
                Dim res = New SalesOrderInterestFacade(identity).Update(tempExisting)



                ''
                crit = New CriteriaComposite(New Criteria(GetType(SalesOrderInterest), "RowStatus", MatchType.Exact, 0))
                'crit.opAnd(New Criteria(GetType(SalesOrderInterest), "SONumber", MatchType.Exact, obj.SONumber))
                crit.opAnd(New Criteria(GetType(SalesOrderInterest), "BillingNumber", MatchType.Exact, obj.BillingNumber))
                Dim arrExistingObj2 As ArrayList = New SalesOrderInterestFacade(identity).Retrieve(crit)

                If IsNothing(arrExistingObj2) OrElse arrExistingObj2.Count = 0 Then
                    Dim newObj As New SalesOrderInterest()
                    newObj.Dealer = New DealerFacade(identity).Retrieve(obj.DealerCode)
                    newObj.BillingNumber = obj.BillingNumber
                    newObj.BillingDate = obj.BillingDate
                    newObj.AdditionalAmount = obj.Amount
                    newObj.PPHAmount = obj.PPH
                    newObj.DPPAmount = obj.DPP
                    newObj.SONumber = obj.SONumber
                    newObj.DocReference = obj.DocReference
                    newObj.TrType = obj.TrType
                    newObj.Status = 5
                    newObj.DocNumber = obj.DocNumber
                    'newObj.RowStatus = CInt(DBRowStatus.Deleted)
                    res = New SalesOrderInterestFacade(identity).Insert(newObj)

                End If
               

                Return True
            End If

            Return False
        End Function

        Private Function getSO(ByVal SONumber As String) As SalesOrder
            Dim crit As New CriteriaComposite(New Criteria(GetType(SalesOrder), "RowStatus", MatchType.Exact, 0))
            crit.opAnd(New Criteria(GetType(SalesOrder), "SONumber", MatchType.Exact, SONumber))
            Dim arrData As ArrayList = New SalesOrderFacade(identity).Retrieve(crit)
            If arrData.Count > 0 Then
                Return arrData(0)
            Else
                Return Nothing
            End If
        End Function
#End Region

    End Class

End Namespace

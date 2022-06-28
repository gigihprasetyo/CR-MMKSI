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

#End Region

Namespace KTB.DNet.Parser

    Public Class SOPPHOnlineStatusParser
        Inherits AbstractParser

#Region "Private Variables"
        Private jsonData As List(Of SOPPHStatusJson)
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
                jsonData = JsonConvert.DeserializeObject(Of List(Of SOPPHStatusJson))(Content)
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "json-worker", "SOPPHOnlineStatusParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ServiceTemplateFSPartParser, BlockName)
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
                If jsonData.Count > 0 Then
                    Dim result As Integer = 0
                    For Each obj As SOPPHStatusJson In jsonData
                        Dim crit As New CriteriaComposite(New Criteria(GetType(InterestPPHHeader), "RowStatus", MatchType.Exact, CShort(DBRowStatus.Active)))
                        crit.opAnd(New Criteria(GetType(InterestPPHHeader), "NoReg", MatchType.Exact, obj.NoPengajuan))
                        Dim arrObj As ArrayList = New InterestPPHHeaderFacade(identity).Retrieve(crit)
                        If arrObj.Count > 0 Then
                            Dim tempObj As InterestPPHHeader = CType(arrObj(0), InterestPPHHeader)
                            tempObj.JVReturn = obj.JVNumber
                            tempObj.Remark = IIf(String.IsNullorEmpty(obj.Remark), tempObj.Remark, obj.Remark)
                            tempObj.SubmissionStatus = 4
                            result = New InterestPPHHeaderFacade(identity).Update(tempObj)

                            Dim arrDetail As ArrayList = New InterestPPHDetailFacade(identity).RetrieveDetails(tempObj.ID)
                            For Each det As InterestPPHDetail In arrDetail
                                Dim objSOInterestToEdit As SalesOrderInterest = det.SalesOrderInterest
                                objSOInterestToEdit.Status = 4
                                Dim res = New SalesOrderInterestFacade(identity).Update(objSOInterestToEdit)
                            Next
                        End If
                    Next

                    Return result
                End If
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "WSJson-worker", "SOPPHOnlineStatusParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ContractParser)
                Dim e As Exception = New Exception("SOPPHData" & Chr(13) & Chr(10) & JsonConvert.SerializeObject(jsonData) & Chr(13) & Chr(10) & ex.Message)
                If ex.Message.Substring(0, 3) = "O/C" Then
                    Throw New System.Exception("O/C status in D-NET is not locked. Update failed.")
                End If
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Return -1
            End Try
        End Function

#Region "Private Methods"

#End Region

    End Class

End Namespace

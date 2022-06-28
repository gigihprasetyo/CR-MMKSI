#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart

#End Region

Namespace KTB.DNet.Parser
    Public Class DepositCInterestPDFParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private _fileName As String
        Public errorMessage As StringBuilder
        Private arrDepositC2InterestHeader As ArrayList
        Private oDepositC2InterestHeader As DepositC2InterestHeader
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

        Protected Overloads Overrides Function DoParse(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Dim finfo As FileInfo = New FileInfo(fileName)
            Dim oDepositC2InterestHeaderFacade As DepositC2InterestHeaderFacade = New DepositC2InterestHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim newFileName As String = String.Empty
            If finfo.Exists Then
                Dim fName As String = finfo.Name.Substring(0, finfo.Name.Length - 4)
                Dim str() As String = fName.Split("_")
                Dim _Periode As Integer = CInt(str(1))
                Dim _Year As Integer = CInt(str(2))
                Dim _Dealer As String = str(3)
                newFileName = _Dealer & "_" & _Periode & "_" & _Year & finfo.Extension

                Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositC2InterestHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteria.opAnd(New Criteria(GetType(DepositC2InterestHeader), "Dealer.DealerCode", MatchType.Exact, _Dealer))
                criteria.opAnd(New Criteria(GetType(DepositC2InterestHeader), "Periode", MatchType.Exact, _Periode))
                criteria.opAnd(New Criteria(GetType(DepositC2InterestHeader), "Year", MatchType.Exact, _Year))
                Dim arrDepositC2InterestHeader As ArrayList = oDepositC2InterestHeaderFacade.Retrieve(criteria)
                If arrDepositC2InterestHeader.Count > 0 Then
                    oDepositC2InterestHeader = CType(arrDepositC2InterestHeader(0), DepositC2InterestHeader)
                    getDocType(str(0), newFileName)
                Else
                    Throw New Exception("Data Tidak Ditemukan")
                End If
            Else
                Throw New Exception("File Tidak Ditemukan")
            End If

            If Not oDepositC2InterestHeader Is Nothing Then
                Try
                    oDepositC2InterestHeaderFacade.Update(oDepositC2InterestHeader)
                    Return newFileName.ToString
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositCInterestPDFParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DepositCInterestPDFParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & _fileName & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            End If
            Return ""
        End Function

        Private Sub getDocType(ByVal fileNamePrefix As String, ByRef newFileName As String)
            Select Case fileNamePrefix.ToUpper
                Case KTB.DNet.Lib.WebConfig.GetValue("DEPOSIT_PDF").ToUpper
                    newFileName = "L_" & newFileName
                    oDepositC2InterestHeader.FilePathLetter = KTB.DNet.Lib.WebConfig.GetValue("DepositCDocumentPath") & "\" & Date.Now.Year & "\" & newFileName
                Case KTB.DNet.Lib.WebConfig.GetValue("KINTB_PDF").ToUpper
                    newFileName = "K_" & newFileName
                    oDepositC2InterestHeader.FilePathKwitansi = KTB.DNet.Lib.WebConfig.GetValue("DepositCDocumentPath") & "\" & Date.Now.Year & "\" & newFileName
            End Select
        End Sub

        Protected Overloads Overrides Function DoTransaction() As Integer
            Return 0
        End Function
    End Class
End Namespace

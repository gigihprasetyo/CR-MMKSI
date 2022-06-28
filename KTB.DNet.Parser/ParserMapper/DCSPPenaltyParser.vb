#Region "Summary"
'// ===========================================================================		
'// Author Name   : 
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 11/10/2005
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Text
Imports System.Collections
Imports System.Text.RegularExpressions
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Security.Principal
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
#End Region

Namespace KTB.DNet.Parser
    Public Class DCSPPenaltyParser
        Inherits AbstractParser

#Region "Private Constants"
#End Region

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _TOPSPPenalty As TOPSPPenalty
        Private _TOPSPPenaltys As ArrayList
        Private mapper As IMapper
        Private objTransactionManager As TransactionManager
        Private _fileName As String
        Private ErrorMessages As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Try
                DoParseFixFormatFile(fileName, user)
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DCSPPenaltyParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.DCSPPenaltyParser, BlockName)
            End Try
            Return 0
        End Function

        Protected Overrides Function DoTransaction() As Integer
            If Not IsNothing(_TOPSPPenaltys) AndAlso _TOPSPPenaltys.Count > 0 Then
                For Each objTOPSPPenalty As TOPSPPenalty In _TOPSPPenaltys
                    Try
                        Dim nResult = New TOPSPPenaltyFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Update(objTOPSPPenalty)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DCSPPenaltyParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.DCSPPenaltyParser, BlockName)
                        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objTOPSPPenalty.DebitMemoNumber & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    End Try
                Next
            End If
            Return 0
        End Function

        Private Sub CopyFileToWebServer(ByVal _fileName As String, ByVal strFolder As String)
            Dim _MachineName = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
            Dim _destinationFolder As String = String.Empty
            Dim _user = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _file As TransferFile
            Dim webMechine As String
            For Each item As String In KTB.DNet.Lib.WebConfig.GetValue("WebServerFolder").Split(";")
                _destinationFolder = item & strFolder
                webMechine = item.Replace("\", "")
                _file = New TransferFile(_user, _password, webMechine.Trim)
                _file.copyFile(_fileName, _destinationFolder)
            Next
        End Sub

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Dim fileInfo As FileInfo = New FileInfo(fileName)
            Dim _delimiter As String = "_"
            Dim strNamaFile As String = Path.GetFileName(fileName)
            Dim strPathNamaFile As String = String.Empty
            Dim strDealerCode As String = String.Empty
            Dim strDebitMemoNumber As String = String.Empty
            Dim strYear As String = String.Empty
            _fileName = fileName
            _TOPSPPenaltys = New ArrayList
            Try
                Dim allVal As String() = strNamaFile.Split(_delimiter.ToCharArray, StringSplitOptions.RemoveEmptyEntries)
                If allVal(0).ToString = "DCSPPenalty" Then
                    strDealerCode = allVal(1).ToString
                    strDebitMemoNumber = allVal(2).ToString
                    If allVal.Length > 3 Then
                        If allVal(3).Trim.ToString <> "" Then
                            strYear = Left(allVal(3).Trim, 4)
                        End If
                    End If
                End If
                strPathNamaFile = "SparePart\Pinalty\" & strYear & "\" & Path.GetFileName(fileName)
                _TOPSPPenalty = ParseTOPSPPenalty(strDealerCode, strDebitMemoNumber, strPathNamaFile)
                If Not IsNothing(_TOPSPPenalty) Then
                    CopyFileToWebServer(fileInfo.FullName, "SparePart\Pinalty\" & strYear & "\")
                End If

                If Not _TOPSPPenalty Is Nothing Then
                    _TOPSPPenaltys.Add(_TOPSPPenalty)
                End If
            Finally
            End Try
            Return _TOPSPPenaltys
        End Function

#End Region

#Region "Private Methods"

        Private Function ParseTOPSPPenalty(ByVal strDealerCode As String, ByVal strDebitMemoNumber As String, ByVal strPathNamaFile As String) As TOPSPPenalty
            Dim objTOPSPPenalty As TOPSPPenalty = New TOPSPPenalty
            Dim criterias As New CriteriaComposite(New Criteria(GetType(TOPSPPenalty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TOPSPPenalty), "Dealer.DealerCode", MatchType.Exact, strDealerCode))
            criterias.opAnd(New Criteria(GetType(TOPSPPenalty), "DebitMemoNumber", MatchType.Exact, strDebitMemoNumber))
            Dim arlTOPSPPenalty As ArrayList = New TOPSPPenaltyFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            If Not IsNothing(arlTOPSPPenalty) Then
                objTOPSPPenalty = CType(arlTOPSPPenalty(0), TOPSPPenalty)
                objTOPSPPenalty.DebitMemoPath = strPathNamaFile
            End If
            Return objTOPSPPenalty
        End Function

#End Region

#Region "Public Properties"

#End Region

    End Class

End Namespace
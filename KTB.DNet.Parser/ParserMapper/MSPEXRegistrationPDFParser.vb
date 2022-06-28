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
#End Region

Namespace KTB.DNet.Parser
    Public Class MSPEXRegistrationPDFParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private arrMSPExDebitMemo As ArrayList
        Private oMSPExDebitMemo As MSPExDebitMemo
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

        Protected Overrides Function DoParse(fileName As String, user As String) As Object
            Try
                Dim finfo As New FileInfo(fileName)
                Dim fullName As String = finfo.FullName
                fileName = finfo.Name
                oMSPExDebitMemo = Nothing
                If fileName.Length > 4 Then
                    If fileName.Substring(fileName.Length - 4).ToUpper = ".PDF" Then
                        Dim debug = ""
                        Dim DMnumber As String = fileName.Substring(2, fileName.Length - 2)
                        DMnumber = DMnumber.Split(".")(0)
                        oMSPExDebitMemo = New MSPExDebitMemoFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(DMnumber)
                        oMSPExDebitMemo.FileName = fileName
                    End If
                End If
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MSPEXRegistrationPDFParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPEXRegistrationPDFParser, BlockName)
                Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                oMSPExDebitMemo = Nothing
            End Try
            Return oMSPExDebitMemo
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object

        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim fMSPExDebitMemo As New MSPExDebitMemoFacade(user)
            Try
                If Not IsNothing(oMSPExDebitMemo) Then
                    If oMSPExDebitMemo.ErrorMessage = String.Empty Then
                        If fMSPExDebitMemo.Update(oMSPExDebitMemo) < 0 Then
                            nError += 1
                        End If
                    Else
                        Throw New Exception(oMSPExDebitMemo.ErrorMessage)
                        nError += 1
                    End If
                End If
            Catch ex As Exception
                sMsg &= ex.Message.ToString() & ";"
                nError += 1
            End Try


            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & oMSPExDebitMemo.DebitMemoNo, "ws-worker", "MSPEXRegistrationPDFParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPEXRegistrationPDFParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MSPEXRegistrationPDFParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPEXRegistrationPDFParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function
    End Class
End Namespace

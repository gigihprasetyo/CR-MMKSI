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
    Public Class MSPEXRegistrationParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private arrMSPExDebitCharge As ArrayList
        Private arrMSPExDebitMemo As ArrayList
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region
        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Dim lines As String() = MyBase.GetLines(Content)
            Dim ind As String
            Dim line As String = String.Empty

            arrMSPExDebitCharge = New ArrayList
            arrMSPExDebitMemo = New ArrayList

            For i As Integer = 0 To lines.Length - 1
                Try
                    line = lines(i)

                    ind = line.Split(MyBase.ColSeparator)(0)
                    If ind = MyBase.IndicatorHeader Then
                        Dim oMSPExDebitCharge As New MSPExDebitCharge
                        Dim oMSPExDebitMemo As New MSPExDebitMemo
                        ParseData(line, oMSPExDebitCharge, oMSPExDebitMemo)
                        If Not oMSPExDebitCharge Is Nothing Then
                            arrMSPExDebitCharge.Add(oMSPExDebitCharge)
                        End If
                        If Not oMSPExDebitMemo Is Nothing Then
                            arrMSPExDebitMemo.Add(oMSPExDebitMemo)
                        End If
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MSPEXRegistrationParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPEXRegistrationParser, BlockName)
                    Dim e As Exception = New Exception(ex.Message)
                    Throw e
                    arrMSPExDebitCharge = Nothing
                    arrMSPExDebitMemo = Nothing
                End Try
            Next
            Return Nothing
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object

        End Function

        Protected Overrides Function DoTransaction() As Integer
            Try
                Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
                'Dim result As Integer = New MSPExDebitChargeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)).InsertTransaction(arrMSPExDebitCharge, arrMSPExDebitMemo)
                If arrMSPExDebitCharge.Count > 0 Then
                    For Each item As MSPExDebitCharge In arrMSPExDebitCharge
                        Dim oMSPExDebitCharge As MSPExDebitCharge = New MSPExDebitChargeFacade(user).Retrieve(item.DebitChargeNo)
                        If oMSPExDebitCharge.ID = 0 Then
                            Dim result = New MSPExDebitChargeFacade(user).Insert(item)
                        Else
                            item.ID = oMSPExDebitCharge.ID
                            Dim result = New MSPExDebitChargeFacade(user).Update(item)
                        End If
                    Next
                End If

                If arrMSPExDebitMemo.Count > 0 Then
                    For Each item As MSPExDebitMemo In arrMSPExDebitMemo
                        Dim oMSPExDebitMemo As MSPExDebitMemo = New MSPExDebitMemoFacade(user).Retrieve(item.DebitMemoNo)
                        If oMSPExDebitMemo.ID = 0 Then
                            Dim result = New MSPExDebitMemoFacade(user).Insert(item)
                        Else
                            item.ID = oMSPExDebitMemo.ID
                            Dim result = New MSPExDebitMemoFacade(user).Update(item)
                        End If
                    Next
                End If

            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog("Failed " & ex.ToString(), "ws-worker", "MSPEXRegistrationParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPEXRegistrationParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & ex.ToString(), "ws-worker", "MSPEXRegistrationParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPEXRegistrationParser, BlockName)
                Dim e As Exception = New Exception(ex.ToString())
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End Try
            Return 1
        End Function

#Region "Private Methods"
        Private Sub ParseData(ByVal ValParser As String, ByRef oMSPExDebitCharge As MSPExDebitCharge, ByRef oMSPExDebitMemo As MSPExDebitMemo)
            Dim cols As String() = ValParser.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objMSPExMaster As New MSPExMaster
            Dim PDCode As String

            'H;EX00000001;5590002999;10112020;ZA91;4900000;490000;58000928298
            'H;RegNumber;DM;DocDate;DocType;Amount;PPN;DC
            Dim Amount As Double = 0
            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                'Case Is = 1 'MSP Extended RegNumber
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    Throw New Exception("RegNumber is Empty" & Chr(13) & Chr(10))
                Else
                    Dim oRegEx As MSPExRegistration = New MSPExRegistrationFacade(user).RetrieveByRegNumber(PDCode.Trim)
                    If oRegEx.ID > 0 Then
                        oMSPExDebitCharge.MSPExRegistration = oRegEx
                        oMSPExDebitMemo.MSPExRegistration = oRegEx
                    Else
                        errorMessage.Append("RegNumber is not registered in DNet" & Chr(13) & Chr(10))
                    End If
                End If

                'Case Is = 2 'DM
                PDCode = cols(2).Trim
                If PDCode = String.Empty Then
                    Throw New Exception("DM is Empty" & Chr(13) & Chr(10))
                Else
                    oMSPExDebitMemo.DebitMemoNo = PDCode.Trim
                    oMSPExDebitMemo.FileName = "DM" & PDCode.Trim & ".pdf"
                End If

                'Case Is = 3 'DocDate
                PDCode = cols(3).Trim
                If PDCode = String.Empty Then
                    Throw New Exception("DocDate is Empty" & Chr(13) & Chr(10))
                Else
                    Dim oDate As String = PDCode.Trim
                    oMSPExDebitCharge.DocumentDate = New Date(oDate.Substring(4, 4), oDate.Substring(2, 2), oDate.Substring(0, 2))
                    oMSPExDebitMemo.DocumentDate = New Date(oDate.Substring(4, 4), oDate.Substring(2, 2), oDate.Substring(0, 2))
                End If

                'Case Is = 4 'DocType
                PDCode = cols(4).Trim
                If PDCode = String.Empty Then
                    Throw New Exception("DocType is Empty" & Chr(13) & Chr(10))
                Else
                    oMSPExDebitMemo.DocType = PDCode.Trim
                End If

                'Case Is = 5 'Amount
                PDCode = cols(5).Trim
                If PDCode = String.Empty Then
                    Throw New Exception("Amount is Empty" & Chr(13) & Chr(10))
                Else
                    Try
                        Amount = CDbl(PDCode.Trim)
                    Catch ex As Exception
                        errorMessage.Append("Wrong format Amount" & Chr(13) & Chr(10))
                    End Try
                End If

                'Case Is = 6 'PPN
                PDCode = cols(6).Trim
                If PDCode = String.Empty Then
                    Throw New Exception("PPN is Empty" & Chr(13) & Chr(10))
                Else
                    Try
                        Amount = Amount + CDbl(PDCode.Trim)
                        oMSPExDebitCharge.Amount = Amount
                        oMSPExDebitMemo.Amount = Amount
                    Catch ex As Exception
                        errorMessage.Append("Wrong format PPN" & Chr(13) & Chr(10))
                    End Try
                End If

                'Case Is = 7 'DC
                PDCode = cols(7).Trim
                If PDCode = String.Empty Then
                    Throw New Exception("DC is Empty" & Chr(13) & Chr(10))
                Else
                    oMSPExDebitCharge.DebitChargeNo = PDCode.Trim
                End If

            End If

            If errorMessage.Length > 0 Then
                oMSPExDebitCharge = Nothing
                oMSPExDebitMemo = Nothing
                Throw New Exception(errorMessage.ToString)
            End If
        End Sub

        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub
#End Region
    End Class
End Namespace
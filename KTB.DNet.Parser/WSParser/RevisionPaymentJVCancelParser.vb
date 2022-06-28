#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Web

#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Collections.Generic
Imports KTB.DNet.Utility

#End Region

Namespace KTB.DNet.Parser
    Public Class RevisionPaymentJVCancelParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _JVCancelPayments As ArrayList
        Private _fileName As String
        Private _RevisionPaymentHeader As RevisionPaymentHeader
        Private _RevisionPaymentDetail As RevisionPaymentDetail
        Private errorMessage As StringBuilder
        Private _TP As TP
        Private _TPs As ArrayList
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"
        Private Class TP

            Private _RevisionPaymentHeader As RevisionPaymentHeader
            Private _RevisionPaymentDetail As RevisionPaymentDetail
            Private _list As List(Of RevisionPaymentDetail)
            Private _errorMessage As StringBuilder

            Public Sub New()
                _list = New List(Of RevisionPaymentDetail)
            End Sub


            Public Property RevisionPaymentHeader() As RevisionPaymentHeader
                Get
                    Return _RevisionPaymentHeader
                End Get
                Set(value As RevisionPaymentHeader)
                    _RevisionPaymentHeader = value
                End Set
            End Property

            Public Property RevisionPaymentDetail() As List(Of RevisionPaymentDetail)
                Get
                    Return _list
                End Get
                Set(value As List(Of RevisionPaymentDetail))
                    _list = value
                End Set
            End Property

            Public Property ErrorMessage() As StringBuilder
                Get
                    Return _errorMessage
                End Get
                Set(value As StringBuilder)
                    _errorMessage = value
                End Set
            End Property
        End Class

        Private Function ParseHeader(ByVal line As String) As RevisionPaymentHeader
            Dim oTC As New RevisionPaymentHeader

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WS"), Nothing)

            errorMessage = New StringBuilder()
            If cols.Length <> 6 Then
                errorMessage.Append("Invalid Header Format" & Chr(13) & Chr(10))
            Else
                'RegNumber
                If cols(1).Trim = String.Empty Then
                    errorMessage.Append("Reg Number is Empty" & Chr(13) & Chr(10))
                Else
                    oTC = RetrieveRevisionPaymentHeader(cols(1).Trim())
                    If (IsNothing(oTC) OrElse oTC.ID = 0) Then
                        errorMessage.Append("Reg Number is not Found!" & Chr(13) & Chr(10))
                    End If
                    If (IsNothing(oTC) OrElse oTC.Status <> EnumDNET.enumPaymentFakturKendaraanRev.Selesai) Then
                        errorMessage.Append("Status of the Reg Number must be completed (selesai)!" & Chr(13) & Chr(10))
                    End If
                End If

                'ChassisNumber
                If cols(2).Trim = String.Empty Then
                    errorMessage.Append("Chassis Number is Empty" & Chr(13) & Chr(10))
                Else
                End If

                'DebitNote
                If cols(3).Trim = String.Empty Then
                    errorMessage.Append("Debit Note Number is Empty!" & Chr(13) & Chr(10))
                Else
                End If

                'Amount
                If cols(4).Trim = String.Empty Then
                    errorMessage.Append("Amount is Empty" & Chr(13) & Chr(10))
                Else
                    Dim sTemp As String = cols(4).Trim
                    If sTemp.Trim.Length > 0 Then
                        If Not IsNumeric(sTemp.Trim) Then
                            errorMessage.Append("Invalid Amount" & Chr(13) & Chr(10))
                        End If
                    Else
                        errorMessage.Append("Invalid Amount" & Chr(13) & Chr(10))
                    End If
                End If

                'AccDocNumber /JV Number
                If cols(5).Trim = String.Empty Then
                    errorMessage.Append("Acc Document Number is Empty" & Chr(13) & Chr(10))
                Else
                    Dim sTemp As String = cols(5).Trim()
                    oTC.AccDocNumber = sTemp
                End If

            End If
            oTC.ErrorMessage = errorMessage.ToString()
            Return oTC
        End Function

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                _TPs = New ArrayList()
                _TP = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_TP) Then
                                If Not IsNothing(errorMessage) Then _TP.ErrorMessage = errorMessage
                                _TPs.Add(_TP)
                                _TP = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _TP = New TP()
                            _TP.RevisionPaymentHeader = ParseHeader(line)

                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "RevisionPaymentVirtuParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.RevisionPaymentGyroParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _TP = Nothing
                    End Try
                Next

                If Not IsNothing(_TP) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _TP.ErrorMessage = errorMessage
                    _TPs.Add(_TP)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try
            Return _TPs
        End Function


        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Try

            Catch ex As Exception
                Throw ex
            Finally

            End Try
            Return _JVCancelPayments
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim aTCTrans As New ArrayList
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WS"), Nothing)
            Dim oTCFac As New RevisionPaymentHeaderFacade(user)
            Dim oTCDFac As New RevisionPaymentDetailFacade(user)
            For i As Integer = 0 To _TPs.Count - 1
                _TP = _TPs(i)
                If (Not IsNothing(_TP.ErrorMessage) AndAlso _TP.ErrorMessage.ToString() <> String.Empty) Then
                    Throw New Exception(_TP.ErrorMessage.ToString())
                Else
                    If Not IsNothing(_TP.RevisionPaymentHeader) AndAlso _TP.RevisionPaymentHeader.ErrorMessage() = "" Then
                        oTCFac.Update(_TP.RevisionPaymentHeader)
                    End If
                End If
            Next

            Return 1
        End Function

#End Region

#Region "Private Methods"
        Private Function RetrieveRevisionPaymentHeader(ByVal code As String) As RevisionPaymentHeader
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WS"), Nothing)

            Dim _RevisionPaymentHeaderF As RevisionPaymentHeaderFacade = New RevisionPaymentHeaderFacade(user)
            Return _RevisionPaymentHeaderF.Retrieve(code.Trim())
        End Function

#End Region

    End Class
End Namespace

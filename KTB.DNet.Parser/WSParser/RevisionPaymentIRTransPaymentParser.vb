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
    Public Class RevisionPaymentIRTransPaymentParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _VirtuPayments As ArrayList
        Private _fileName As String
        Private _RevisionPaymentHeader As RevisionPaymentHeader
        Private _IRAccDocNumber As IRAccDocNumber
        Private errorMessage As StringBuilder
        Private _TP As TP
        Private _TPs As ArrayList
#End Region
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

                        ElseIf ind = MyBase.IndicatorDetail Then
                            _IRAccDocNumber = ParseDetail(line, _TP.RevisionPaymentHeader)
                            _TP.IRAccDocNumber.Add(_IRAccDocNumber)
                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "RevisionPaymentIRTransPaymentParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.RevisionPaymentIRTransPaymentParser, BlockName)
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

        'H;RegNumber;TotalAmount;GyroDate\n
        Private Function ParseHeader(ByVal line As String) As RevisionPaymentHeader
            Dim oTC As New RevisionPaymentHeader

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WS"), Nothing)

            errorMessage = New StringBuilder()
            If cols.Length <> 4 Then
                errorMessage.Append("Invalid Header Format" & Chr(13) & Chr(10))
            Else
                'RegNumber
                If cols(1) = String.Empty Then
                    errorMessage.Append("Reg Number is Empty" & Chr(13) & Chr(10))
                Else
                    oTC = RetrieveRevisionPaymentHeader(cols(1).Trim())
                    If (IsNothing(oTC) OrElse oTC.ID = 0) Then
                        errorMessage.Append("Reg Number is not Found!" & Chr(13) & Chr(10))
                    End If
                End If

                'TotalAmount
                If cols(2) = String.Empty Then
                    errorMessage.Append("Total Amount is Empty" & Chr(13) & Chr(10))
                Else
                    If cols(2).Trim.Length > 0 Then
                        If IsNumeric(cols(2).Trim) Then
                            Dim TotalAmount = oTC.TotalAmount + CDec(cols(2).Trim)
                            oTC.TotalAmount = TotalAmount
                        Else
                            errorMessage.Append("Invalid Total Amount " & Chr(13) & Chr(10))
                        End If
                    Else
                        errorMessage.Append("Invalid Total Amount " & Chr(13) & Chr(10))
                    End If
                End If

                'GyroDate
                If cols(3) = String.Empty Then
                    errorMessage.Append("Gyro Date is Empty" & Chr(13) & Chr(10))
                Else
                    Dim sTemp As String = cols(3).Trim()
                    If sTemp.Length > 0 Then
                        Try
                            Dim Thn As Integer = CInt(Strings.Left(sTemp, 4))
                            Dim Bln As Integer = CInt(sTemp.Substring(4, 2))
                            Dim Tgl As Integer = CInt(sTemp.Substring(6, 2))
                            oTC.GyroDate = New DateTime(Thn, Bln, Tgl)
                        Catch ex As Exception
                            errorMessage.Append("Invalid Gyro Date " & Chr(13) & Chr(10))
                        End Try

                    Else
                        errorMessage.Append("Invalid Gyro Date " & Chr(13) & Chr(10))
                    End If
                End If
            End If
            oTC.ErrorMessage = errorMessage.ToString()
            Return oTC
        End Function

        Private Function ParseDetail(ByVal line As String, ByVal RevisionPaymentHeader As RevisionPaymentHeader) As IRAccDocNumber

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim oTC As New IRAccDocNumber

            If (cols.Length <> 4) Then
                errorMessage.Append("Invalid Detail Format" & Chr(13) & Chr(10))
            Else

                oTC.RevisionPaymentHeader = RevisionPaymentHeader
                If cols(1).Trim = String.Empty Then
                    errorMessage.Append("Debit Charge Number is Empty" & Chr(13) & Chr(10))
                Else
                    oTC.DebitChargeNo = cols(1).Trim()
                End If

                If cols(2).Trim = String.Empty Then
                    errorMessage.Append("TR Number is Empty" & Chr(13) & Chr(10))
                Else
                    oTC.TRNo = cols(2).Trim()
                End If

                If cols(3).Trim = String.Empty Then
                    errorMessage.Append("Amount is Empty" & Chr(13) & Chr(10))
                Else
                    Dim sTemp As String = cols(3).Trim
                    If sTemp.Trim.Length > 0 Then
                        If IsNumeric(sTemp.Trim) Then
                            oTC.Amount = CDec(sTemp.Trim)
                        Else
                            errorMessage.Append("Invalid Amount" & Chr(13) & Chr(10))
                        End If
                    Else
                        errorMessage.Append("Invalid Amount" & Chr(13) & Chr(10))
                    End If
                End If

            End If
            oTC.ErrorMessage = errorMessage.ToString()
            Return oTC
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim aTCTrans As New ArrayList
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WS"), Nothing)
            Dim oTCDFac As New IRAccDocNumberFacade(user)
            For i As Integer = 0 To _TPs.Count - 1
                _TP = _TPs(i)
                If (Not IsNothing(_TP.ErrorMessage) AndAlso _TP.ErrorMessage.ToString() <> String.Empty) Then
                    Throw New Exception(_TP.ErrorMessage.ToString())
                Else
                    If Not IsNothing(_TP.RevisionPaymentHeader) AndAlso _TP.RevisionPaymentHeader.ErrorMessage() = "" Then
                        Dim RPHeader As RevisionPaymentHeader = New RevisionPaymentHeaderFacade(user).Retrieve(_TP.RevisionPaymentHeader.RegNumber)
                        For Each Otpd As IRAccDocNumber In _TP.IRAccDocNumber
                            Otpd.RevisionPaymentHeader = RPHeader
                            oTCDFac.Insert(Otpd)
                        Next
                    End If
                End If
            Next
            Return 1
        End Function

#Region "Custom Function"
        Private Function RetrieveRevisionPaymentHeader(ByVal code As String) As RevisionPaymentHeader
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WS"), Nothing)

            Dim _RevisionPaymentHeaderF As RevisionPaymentHeaderFacade = New RevisionPaymentHeaderFacade(user)
            Return _RevisionPaymentHeaderF.Retrieve(code.Trim())
        End Function

        Private Class TP
            Private _RevisionPaymentHeader As RevisionPaymentHeader
            Private _IRAccDocNumber As IRAccDocNumber
            Private _errorMessage As StringBuilder
            Private _list As List(Of IRAccDocNumber)

            Public Sub New()
                _list = New List(Of IRAccDocNumber)
            End Sub

            Public Property RevisionPaymentHeader() As RevisionPaymentHeader
                Get
                    Return _RevisionPaymentHeader
                End Get
                Set(value As RevisionPaymentHeader)
                    _RevisionPaymentHeader = value
                End Set
            End Property

            Public Property IRAccDocNumber() As List(Of IRAccDocNumber)
                Get
                    Return _list
                End Get
                Set(value As List(Of IRAccDocNumber))
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
#End Region
    End Class
End Namespace
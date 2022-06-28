Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text.RegularExpressions
Imports KTB.DNet.Domain
Imports System.Text
Imports System.Collections.Generic
Imports System.Security.Principal
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Transfer
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart

Public Class TOPTranActualDateParser
    Inherits AbstractParser

#Region "Private Variables"
    Private _Stream As StreamReader
    Private Grammar As Regex
    Private _TransferCeilings As ArrayList
    Private _fileName As String
    Private _TOPSPTransferPayment As TOPSPTransferPayment
    Private _TOPSPTransferPaymentDetail As TOPSPTransferPaymentDetail
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

        Private _TOPSPTransferPayment As TOPSPTransferPayment
        Private _TOPSPTransferPaymentDetail As TOPSPTransferPaymentDetail
        Private _list As List(Of TOPSPTransferPaymentDetail)
        Private _errorMessage As StringBuilder

        Public Property RefTransferbank As String

        Public Sub New()
            _list = New List(Of TOPSPTransferPaymentDetail)
            RefTransferbank = String.Empty
        End Sub



        Public Property TOPSPTransferPayment() As TOPSPTransferPayment
            Get
                Return _TOPSPTransferPayment
            End Get
            Set(value As TOPSPTransferPayment)
                _TOPSPTransferPayment = value
            End Set
        End Property

        Public Property TOPSPTransferPaymentDetail() As List(Of TOPSPTransferPaymentDetail)
            Get
                Return _list
            End Get
            Set(value As List(Of TOPSPTransferPaymentDetail))
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

    Private Function ParseHeader(ByVal line As String, ByRef ref As String) As TOPSPTransferPayment
        Dim oTC As New TOPSPTransferPayment

        Dim cols As String() = line.Split(MyBase.ColSeparator)
        Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)


        errorMessage = New StringBuilder()
        If cols.Length <> 5 Then
            errorMessage.Append("Invalid Header Format" & Chr(13) & Chr(10))
        Else
            'RegNumber
            If cols(1).Trim = String.Empty Then
                errorMessage.Append("Reg Number Empty" & Chr(13) & Chr(10))
            Else
                oTC = RetrieveTOPSPTransferPayment(cols(1).Trim())
                If (IsNothing(oTC) OrElse oTC.ID = 0) Then
                    errorMessage.Append("Reg Number is not Found!" & Chr(13) & Chr(10))
                End If
            End If

            'TransferAmount
            If cols(2).Trim = String.Empty Then
                errorMessage.Append("Amount Can Not Be Empty!" & Chr(13) & Chr(10))
            Else
                If cols(2).Trim.Length > 0 Then
                    If IsNumeric(cols(2).Trim) Then
                        oTC.TransferAmount = CDec(cols(2).Trim)
                    Else
                        errorMessage.Append("Invalid TransferAmount" & Chr(13) & Chr(10))
                    End If
                Else
                    errorMessage.Append("Invalid TransferAmount" & Chr(13) & Chr(10))
                End If
            End If

            'TransferDate
            If cols(3).Trim() = String.Empty Then
                errorMessage.Append("Actual Transfer Date" & Chr(13) & Chr(10))
            Else
                Dim sTemp As String = cols(3).Trim()
                If sTemp.Length > 0 Then
                    Try
                        Dim Thn As Integer = CInt(Strings.Left(sTemp, 4))
                        Dim Bln As Integer = CInt(sTemp.Substring(4, 2))
                        Dim Tgl As Integer = CInt(sTemp.Substring(6, 2))
                        oTC.TransferDate = New DateTime(Thn, Bln, Tgl)

                    Catch ex As Exception
                        errorMessage.Append("Invalid TransferDate" & Chr(13) & Chr(10))
                    End Try
                Else
                    errorMessage.Append("Invalid TransferDate" & Chr(13) & Chr(10))
                End If
            End If


            If cols(4).Trim() = String.Empty Then
                errorMessage.Append("Refrence Bank Empty" & Chr(13) & Chr(10))
            Else
                ref = cols(4).Trim()

            End If

        End If
        oTC.ErrorMessage = errorMessage.ToString()
        Return oTC
    End Function

    Private Function ParseDetail(ByVal line As String, Optional ByVal TOPSPTransferPaymentID As Integer = 0) As TOPSPTransferPaymentDetail
        Dim cols As String() = line.Split(MyBase.ColSeparator)
        Dim oTC As New TOPSPTransferPaymentDetail

        'If (cols.Length <> 3) Then
        '    errorMessage.Append("Invalid Detail Format" & Chr(13) & Chr(10))
        'Else


        '    If cols(1).Trim = String.Empty Then
        '        errorMessage.Append("SO Number Empty" & Chr(13) & Chr(10))
        '    Else

        '        oTC = RetrieveTOPSPTransferPaymentDetail(cols(1).Trim(), TOPSPTransferPaymentID)
        '        If (IsNothing(oTC) OrElse oTC.ID = 0) Then
        '            errorMessage.Append("SO Number is not Found!" & Chr(13) & Chr(10))
        '        End If

        '    End If


        '    If cols(2).Trim = String.Empty Then
        '        errorMessage.Append("Actual Amount Empty" & Chr(13) & Chr(10))
        '    Else
        '        Dim sTemp As String = cols(2).Trim
        '        If sTemp.Trim.Length > 0 Then
        '            If IsNumeric(sTemp.Trim) Then
        '                oTC.ActualAmount = CDec(sTemp.Trim)
        '            Else
        '                errorMessage.Append("Invalid TotalActualAmount" & Chr(13) & Chr(10))
        '            End If
        '        Else
        '            errorMessage.Append("Invalid TotalActualAmount" & Chr(13) & Chr(10))
        '        End If
        '    End If

        'End If
        oTC.ErrorMessage = errorMessage.ToString()
        Return oTC
    End Function

    Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
        Try
            Dim lines As String() = MyBase.GetLines(Content)
            Dim ind As String
            Dim line As String

            _TPs = New ArrayList()
            _TOPSPTransferPayment = Nothing

            For i As Integer = 0 To lines.Length - 1
                Try
                    line = lines(i)

                    ind = line.Split(MyBase.ColSeparator)(0)
                    If ind = MyBase.IndicatorHeader Then

                        Dim ref As String = String.Empty
                        errorMessage = New StringBuilder()
                        _TP = New TP()
                        _TP.TOPSPTransferPayment = ParseHeader(line, ref)
                        _TP.RefTransferbank = ref

                        If Not IsNothing(_TP) Then
                            If Not IsNothing(errorMessage) Then _TP.ErrorMessage = errorMessage
                            _TPs.Add(_TP)
                            _TP = Nothing
                        End If
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "TransferCeilingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.UserParser, BlockName)
                    Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    _TP = Nothing
                End Try
            Next

            'If Not IsNothing(_TOPSPTransferPaymentDetail) Then
            '    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _TP.ErrorMessage = errorMessage

            'End If

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
        Return _TransferCeilings
    End Function

    Protected Overrides Function DoTransaction() As Integer
        Dim aTCTrans As New ArrayList
        Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
        Dim oTCFac As New TOPSPTransferPaymentFacade(user)
        Dim oTCDFac As New TOPSPTransferPaymentDetailFacade(user)
        Dim ErrMsg As New StringBuilder

        If _TPs.Count > 0 Then

            For i As Integer = 0 To _TPs.Count - 1
                _TP = _TPs(i)
                If (Not IsNothing(_TP.ErrorMessage) AndAlso _TP.ErrorMessage.ToString() <> String.Empty) Then
                    ErrMsg.AppendLine(_TP.ErrorMessage.ToString())

                Else


                    If Not IsNothing(_TP.TOPSPTransferPayment) AndAlso _TP.TOPSPTransferPayment.ErrorMessage() = "" Then
                        oTCFac.UpdateTransferActual(_TP.TOPSPTransferPayment, _TP.RefTransferbank)

                    End If

                End If
            Next
        Else
            Return 0
        End If

        If ErrMsg.ToString() <> "" Then
            Throw New Exception(ErrMsg.ToString())
        End If
        Return 1
    End Function

    Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

    End Function

#End Region

#Region "Private Methods"



    Private Function RetrieveTOPSPTransferPaymentDetail(ByVal code As String, ByVal ID As Integer) As TOPSPTransferPaymentDetail
        Dim _ProductCategoryFacade As TOPSPTransferPaymentDetailFacade = New TOPSPTransferPaymentDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

        Dim cVCA As New CriteriaComposite(New Criteria(GetType(TOPSPTransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        cVCA.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "SalesOrder.SONumber", MatchType.Exact, code))
        cVCA.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.ID", MatchType.Exact, ID))

        Dim objD As New TOPSPTransferPaymentDetail
        Dim ObjDA As New ArrayList
        ObjDA = _ProductCategoryFacade.Retrieve(cVCA)
        If Not IsNothing(ObjDA) AndAlso ObjDA.Count > 0 Then
            objD = CType(ObjDA(0), TOPSPTransferPaymentDetail)
        End If
        Return objD
    End Function

    Private Function RetrieveTOPSPTransferPayment(ByVal code As String) As TOPSPTransferPayment
        Dim _TOPSPTransferPaymentF As TOPSPTransferPaymentFacade = New TOPSPTransferPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
        Return _TOPSPTransferPaymentF.Retrieve(code.Trim())
    End Function
#End Region


End Class

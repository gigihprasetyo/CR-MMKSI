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
    Public Class RevisionPaymentVirtuParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _VirtuPayments As ArrayList
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

            Private _RevisionTransferActual As RevisionTransferActual
            Private _RevisionPaymentHeader As RevisionPaymentHeader
            Private _RevisionPaymentDetail As RevisionPaymentDetail
            Private _list As List(Of RevisionPaymentDetail)
            Private _errorMessage As StringBuilder

            Public Sub New()
                _list = New List(Of RevisionPaymentDetail)
            End Sub

            Public Property RevisionTransferActual() As RevisionTransferActual
                Get
                    Return _RevisionTransferActual
                End Get
                Set(value As RevisionTransferActual)
                    _RevisionTransferActual = value
                End Set
            End Property

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

        'Private Function ParseHeader(ByVal line As String) As RevisionPaymentHeader
        Private Function ParseHeader(ByVal line As String) As RevisionTransferActual
            'Dim oTC As New RevisionPaymentHeader
            Dim oTC As New RevisionTransferActual

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WS"), Nothing)

            errorMessage = New StringBuilder()
            If cols.Length <> 5 Then
                errorMessage.Append("Invalid Header Format" & Chr(13) & Chr(10))
            Else
                'RegNumber
                If cols(1) = String.Empty Then
                    errorMessage.Append("Reg Number is Empty" & Chr(13) & Chr(10))
                Else
                    oTC.RevisionPaymentHeader = RetrieveRevisionPaymentHeader(cols(1).Trim())
                    If Not IsNothing(oTC.RevisionPaymentHeader) Then
                        If oTC.RevisionPaymentHeader.ID = 0 Then
                            errorMessage.Append("Reg Number is not Found!" & Chr(13) & Chr(10))
                        End If
                    Else
                        errorMessage.Append("Reg Number is not Found!" & Chr(13) & Chr(10))
                    End If
                End If

                ''ActualPaymentAmount
                'If cols(2).Trim = String.Empty Then
                '    errorMessage.Append("Actual Payment Amount is Empty!" & Chr(13) & Chr(10))
                'Else
                '    If cols(2).Trim.Length > 0 Then
                '        If IsNumeric(cols(2).Trim) Then
                '            oTC.ActualPaymentAmount = oTC.ActualPaymentAmount + CDec(cols(2).Trim)
                '        Else
                '            errorMessage.Append("Invalid Actual Payment Amount" & Chr(13) & Chr(10))
                '        End If
                '    Else
                '        errorMessage.Append("Invalid Actual Payment Amount" & Chr(13) & Chr(10))
                '    End If
                'End If

                ''ActualPaymentDate
                'If cols(3).Trim() = String.Empty Then
                '    errorMessage.Append("Actual Payment Date is Empty" & Chr(13) & Chr(10))
                'Else
                '    Dim sTemp As String = cols(3).Trim()
                '    If sTemp.Length > 0 Then
                '        Try
                '            Dim Thn As Integer = CInt(Strings.Left(sTemp, 4))
                '            Dim Bln As Integer = CInt(sTemp.Substring(4, 2))
                '            Dim Tgl As Integer = CInt(sTemp.Substring(6, 2))
                '            oTC.ActualPaymentDate = New DateTime(Thn, Bln, Tgl)
                '        Catch ex As Exception
                '            errorMessage.Append("Invalid Actual Payment Date" & Chr(13) & Chr(10))
                '        End Try

                '    Else
                '        errorMessage.Append("Invalid Actual Payment Date" & Chr(13) & Chr(10))
                '    End If
                'End If

                ''AccDocNumber
                'If cols(4).Trim = String.Empty Then
                '    errorMessage.Append("Acc Document Number is Empty" & Chr(13) & Chr(10))
                'Else
                '    Dim sTemp As String = cols(4).Trim()
                '    oTC.AccDocNumber = sTemp
                'End If

                'RegTransferBank
                If cols(2) = String.Empty Then
                    errorMessage.Append("Ref Transfer Bank is Empty" & Chr(13) & Chr(10))
                Else
                    oTC.RefTransferBank = cols(2).Trim()
                End If

                ''AccDocNmber
                'If cols(3) = String.Empty Then
                '    errorMessage.Append("Acc Document Number is Empty" & Chr(13) & Chr(10))
                'Else
                '    oTC.AccDocNumber = cols(3).Trim()
                'End If

                'Amount
                If cols(3) = String.Empty Then
                    errorMessage.Append("Amount is Empty" & Chr(13) & Chr(10))
                Else
                    If cols(3).Trim.Length > 0 Then
                        If IsNumeric(cols(3).Trim) Then
                            Dim ActualAmount = oTC.Amount + CDec(cols(3).Trim)
                            oTC.Amount = ActualAmount
                        Else
                            errorMessage.Append("Invalid Amount" & Chr(13) & Chr(10))
                        End If
                    Else
                        errorMessage.Append("Invalid Amount" & Chr(13) & Chr(10))
                    End If
                End If

                'PostingDate
                If cols(4) = String.Empty Then
                    errorMessage.Append("Posting Date is Empty" & Chr(13) & Chr(10))
                Else
                    Dim sTemp As String = cols(4).Trim()
                    If sTemp.Length > 0 Then
                        Try
                            Dim Thn As Integer = CInt(Strings.Left(sTemp, 4))
                            Dim Bln As Integer = CInt(sTemp.Substring(4, 2))
                            Dim Tgl As Integer = CInt(sTemp.Substring(6, 2))
                            oTC.PostingDate = New DateTime(Thn, Bln, Tgl)
                        Catch ex As Exception
                            errorMessage.Append("Invalid Posting Date " & Chr(13) & Chr(10))
                        End Try

                    Else
                        errorMessage.Append("Invalid Posting Date " & Chr(13) & Chr(10))
                    End If
                End If

                If Not IsNothing(oTC.RevisionPaymentHeader) Then
                    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionTransferActual), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(RevisionTransferActual), "RevisionPaymentHeader.RegNumber", MatchType.Exact, oTC.RevisionPaymentHeader.RegNumber))
                    crit.opAnd(New Criteria(GetType(RevisionTransferActual), "RefTransferBank", MatchType.Exact, oTC.RefTransferBank))
                    crit.opAnd(New Criteria(GetType(RevisionTransferActual), "Amount", MatchType.Exact, oTC.Amount))
                    Dim arlIR As ArrayList = New RevisionTransferActualFacade(user).Retrieve(crit)
                    If arlIR.Count > 0 Then
                        errorMessage.Append("Record already Exist")
                    End If
                End If
            End If
            oTC.ErrorMessage = errorMessage.ToString()
            Return oTC
        End Function

        Private Function ParseDetail(ByVal line As String, ByVal RevisionPaymentHeader As RevisionPaymentHeader) As RevisionPaymentDetail
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim oTC As New RevisionPaymentDetail

            If (cols.Length <> 3) Then
                errorMessage.Append("Invalid Detail Format" & Chr(13) & Chr(10))
            Else

                oTC.RevisionPaymentHeader = RevisionPaymentHeader
                If cols(1).Trim = String.Empty Then
                    errorMessage.Append("Debit Charge Number is Empty" & Chr(13) & Chr(10))
                Else
                    oTC.RevisionSAPDoc = RetrieveRevisionSAPDoc(cols(1).Trim())
                    oTC.RevisionFaktur = oTC.RevisionSAPDoc.RevisionFaktur
                End If
                If cols(2).Trim = String.Empty Then
                    errorMessage.Append("Amount is Empty" & Chr(13) & Chr(10))
                Else
                    Dim sTemp As String = cols(2).Trim
                    If sTemp.Trim.Length > 0 Then
                        If IsNumeric(sTemp.Trim) Then
                            oTC.RevisionSAPDoc.DCAmount = CDec(sTemp.Trim)
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
                            _TP.RevisionTransferActual = ParseHeader(line)

                            'ElseIf ind = MyBase.IndicatorDetail Then
                            '    _RevisionPaymentDetail = ParseDetail(line, _TP.RevisionPaymentHeader)
                            '    _TP.RevisionPaymentDetail.Add(_RevisionPaymentDetail)
                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "RevisionPaymentVirtuParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.RevisionPaymentVirtuParser, BlockName)
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
            Return _VirtuPayments
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim aTCTrans As New ArrayList
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WS"), Nothing)
            Dim oTCDFac As New RevisionPaymentDetailFacade(user)
            For i As Integer = 0 To _TPs.Count - 1
                _TP = _TPs(i)
                If (Not IsNothing(_TP.ErrorMessage) AndAlso _TP.ErrorMessage.ToString() = "Record already Exist") Then
                    Continue For
                Else
                    If (Not IsNothing(_TP.ErrorMessage) AndAlso _TP.ErrorMessage.ToString() <> String.Empty) Then
                        Throw New Exception(_TP.ErrorMessage.ToString())
                    Else
                        If Not IsNothing(_TP.RevisionTransferActual.RevisionPaymentHeader) AndAlso _TP.RevisionTransferActual.ErrorMessage() = "" Then
                            Dim ill As Integer = New RevisionTransferActualFacade(user).Insert(_TP.RevisionTransferActual)
                            'If _TP.RevisionPaymentHeader.PaymentType = enumPaymentTypeRevision.PaymentType.Virtual_Account Then
                            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionTransferActual), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crit.opAnd(New Criteria(GetType(RevisionTransferActual), "RevisionPaymentHeader.ID", MatchType.Exact, _TP.RevisionTransferActual.RevisionPaymentHeader.ID))
                            Dim arlRTA As ArrayList = New RevisionTransferActualFacade(user).Retrieve(crit)
                            Dim acAm As Double = 0
                            For Each RTA As RevisionTransferActual In arlRTA
                                acAm = acAm + RTA.Amount
                            Next
                            _TP.RevisionTransferActual.RevisionPaymentHeader.ActualPaymentAmount = acAm
                            '_TP.RevisionTransferActual.RevisionPaymentHeader.AccDocNumber = _TP.RevisionTransferActual.AccDocNumber
                            Dim RTFA As RevisionTransferActual = New RevisionTransferActualFacade(user).Retrieve(ill)
                            _TP.RevisionTransferActual.RevisionPaymentHeader.ActualPaymentDate = RTFA.PostingDate
                            If _TP.RevisionTransferActual.RevisionPaymentHeader.ActualPaymentAmount >= _TP.RevisionTransferActual.RevisionPaymentHeader.TotalAmount Then
                                _TP.RevisionTransferActual.RevisionPaymentHeader.Status = EnumDNET.enumPaymentFakturKendaraanRev.Selesai
                            End If
                            Dim q As Integer = New RevisionPaymentHeaderFacade(user).Update(_TP.RevisionTransferActual.RevisionPaymentHeader)
                            'End If
                        End If
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

        Private Function RetrieveRevisionSAPDoc(ByVal code As String) As RevisionSAPDoc
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WS"), Nothing)

            Dim _RevisionSAPDocF As RevisionSAPDocFacade = New RevisionSAPDocFacade(user)
            Return _RevisionSAPDocF.Retrieve(code.Trim())
        End Function

#End Region

    End Class
End Namespace

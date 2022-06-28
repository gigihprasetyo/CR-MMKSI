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
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Transfer
Imports System.Collections.Generic

#End Region

Namespace KTB.DNet.Parser

    Public Class TransferPaymentParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _TransferCeilings As ArrayList
        Private _fileName As String
        Private _TransferPayment As TransferPayment
        Private _TransferPaymentDetail As TransferPaymentDetail
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

            Private _TransferPayment As TransferPayment
            Private _TransferPaymentDetail As TransferPaymentDetail
            Private _list As List(Of TransferPaymentDetail)
            Private _errorMessage As StringBuilder

            Public Sub New()
                _list = New List(Of TransferPaymentDetail)
            End Sub



            Public Property TransferPayment() As TransferPayment
                Get
                    Return _TransferPayment
                End Get
                Set(value As TransferPayment)
                    _TransferPayment = value
                End Set
            End Property

            Public Property TransferPaymentDetail() As List(Of TransferPaymentDetail)
                Get
                    Return _list
                End Get
                Set(value As List(Of TransferPaymentDetail))
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

        Private Function ParseHeader(ByVal line As String) As TransferPayment
            Dim oTC As New TransferPayment

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)


            errorMessage = New StringBuilder()
            If cols.Length <> 4 Then
                errorMessage.Append("Invalid Header Format" & Chr(13) & Chr(10))
            Else
                'RegNumber
                If cols(1).Trim = String.Empty Then
                    errorMessage.Append("Reg Number Empty" & Chr(13) & Chr(10))
                Else
                    oTC = RetrieveTransferPayment(cols(1).Trim())
                    If (IsNothing(oTC) OrElse oTC.ID = 0) Then
                        errorMessage.Append("Reg Number is not Found!" & Chr(13) & Chr(10))
                    End If
                End If

                'TotalActualAmount
                If cols(2).Trim = String.Empty Then
                    errorMessage.Append("Amount Can Not Be Empty!" & Chr(13) & Chr(10))
                Else
                    If cols(2).Trim.Length > 0 Then
                        If IsNumeric(cols(2).Trim) Then
                            oTC.TotalActualAmount = CDec(cols(2).Trim)
                        Else
                            errorMessage.Append("Invalid TotalActualAmount" & Chr(13) & Chr(10))
                        End If
                    Else
                        errorMessage.Append("Invalid TotalActualAmount" & Chr(13) & Chr(10))
                    End If
                End If

                'Actual TransferDate
                If cols(3).Trim() = String.Empty Then
                    errorMessage.Append("Actual Transfer Date" & Chr(13) & Chr(10))
                Else
                    Dim sTemp As String = cols(3).Trim()
                    If sTemp.Length > 0 Then
                        Try
                            Dim Thn As Integer = CInt(Strings.Left(sTemp, 4))
                            Dim Bln As Integer = CInt(sTemp.Substring(4, 2))
                            Dim Tgl As Integer = CInt(sTemp.Substring(6, 2))
                            oTC.ActualTrfDate = New DateTime(Thn, Bln, Tgl)
                        Catch ex As Exception
                            errorMessage.Append("Invalid ActualTrfDate" & Chr(13) & Chr(10))
                        End Try

                    Else
                        errorMessage.Append("Invalid ActualTrfDate" & Chr(13) & Chr(10))
                    End If
                End If

            End If
            oTC.ErrorMessage = errorMessage.ToString()
            Return oTC
        End Function

        Private Function ParseDetail(ByVal line As String, Optional ByVal TransferPaymentID As Integer = 0, Optional ByVal PaymentPurposeID As Integer = 0) As TransferPaymentDetail
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim oTC As New TransferPaymentDetail

            If (cols.Length <> 3) Then
                errorMessage.Append("Invalid Detail Format" & Chr(13) & Chr(10))
            Else
                If cols(1).Trim = String.Empty Then
                    errorMessage.Append("SO Number or Debit Charge Number Empty" & Chr(13) & Chr(10))
                Else
                    '--> PaymentPurpose = Biaya Logistic
                    If PaymentPurposeID = 8 Then
                        oTC = RetrieveTransferPaymentDetailByLogistic(cols(1).Trim(), TransferPaymentID)
                        If (IsNothing(oTC) OrElse oTC.ID = 0) Then
                            errorMessage.Append("Debit Charge Number is not Found!" & Chr(13) & Chr(10))
                        End If
                    Else
                        oTC = RetrieveTransferPaymentDetail(cols(1).Trim(), TransferPaymentID)
                        If (IsNothing(oTC) OrElse oTC.ID = 0) Then
                            errorMessage.Append("SO Number is not Found!" & Chr(13) & Chr(10))
                        End If
                    End If
                End If

                If cols(2).Trim = String.Empty Then
                    errorMessage.Append("Actual Amount Empty" & Chr(13) & Chr(10))
                Else
                    Dim sTemp As String = cols(2).Trim
                    If sTemp.Trim.Length > 0 Then
                        If IsNumeric(sTemp.Trim) Then
                            oTC.ActualAmount = CDec(sTemp.Trim)
                        Else
                            errorMessage.Append("Invalid TotalActualAmount" & Chr(13) & Chr(10))
                        End If
                    Else
                        errorMessage.Append("Invalid TotalActualAmount" & Chr(13) & Chr(10))
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
                _TransferPayment = Nothing

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
                            _TP.TransferPayment = ParseHeader(line)


                        ElseIf ind = MyBase.IndicatorDetail Then
                            _TransferPaymentDetail = ParseDetail(line, _TP.TransferPayment.ID, _TP.TransferPayment.PaymentPurpose.ID)
                            _TP.TransferPaymentDetail.Add(_TransferPaymentDetail)
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "TransferCeilingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.UserParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _TP = Nothing
                    End Try
                Next

                If Not IsNothing(_TransferPaymentDetail) Then
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
            Return _TransferCeilings
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim aTCTrans As New ArrayList
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim oTCFac As New TransferPaymentFacade(user)
            Dim oTCDFac As New TransferPaymentDetailFacade(user)
            For i As Integer = 0 To _TPs.Count - 1
                _TP = _TPs(i)
                If (Not IsNothing(_TP.ErrorMessage) AndAlso _TP.ErrorMessage.ToString() <> String.Empty) Then
                    Throw New Exception(_TP.ErrorMessage.ToString())
                Else
                   

                    If Not IsNothing(_TP.TransferPayment) AndAlso _TP.TransferPayment.ErrorMessage() = "" Then
                        oTCFac.UpdateSimple(_TP.TransferPayment)
                        For Each Otpd As TransferPaymentDetail In _TP.TransferPaymentDetail
                            If Not IsNothing(Otpd) AndAlso Otpd.ID > 0 Then
                                oTCDFac.Update(Otpd)
                            End If
                        Next

                    End If
                      
                End If
            Next

            Return 1
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

       

        Private Function RetrieveTransferPaymentDetailByLogistic(ByVal code As String, ByVal ID As Integer) As TransferPaymentDetail
            Dim _ProductCategoryFacade As TransferPaymentDetailFacade = New TransferPaymentDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

            Dim cVCA As New CriteriaComposite(New Criteria(GetType(TransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim strSQL As String = String.Empty
            strSQL = "select distinct c.ID "
            strSQL += "from LogisticDCHeader a "
            strSQL += "join LogisticDN b on b.LogisticDCHeaderID = a.ID and b.RowStatus = 0 "
            strSQL += "join SalesOrder c on c.ID = b.SalesOrderID and c.RowStatus = 0 "
            strSQL += "Where a.RowStatus = 0 "
            strSQL += "and a.DebitChargeNo = '" & code & "'"
            cVCA.opAnd(New Criteria(GetType(TransferPaymentDetail), "SalesOrder.ID", MatchType.InSet, "(" & strSQL & ")"))
            cVCA.opAnd(New Criteria(GetType(TransferPaymentDetail), "TransferPayment.ID", MatchType.Exact, ID))

            Dim objD As New TransferPaymentDetail
            Dim ObjDA As New ArrayList
            ObjDA = _ProductCategoryFacade.Retrieve(cVCA)
            If Not IsNothing(ObjDA) AndAlso ObjDA.Count > 0 Then
                objD = CType(ObjDA(0), TransferPaymentDetail)
            End If
            Return objD
        End Function

        Private Function RetrieveTransferPaymentDetail(ByVal code As String, ByVal ID As Integer) As TransferPaymentDetail
            Dim _ProductCategoryFacade As TransferPaymentDetailFacade = New TransferPaymentDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

            Dim cVCA As New CriteriaComposite(New Criteria(GetType(TransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            cVCA.opAnd(New Criteria(GetType(TransferPaymentDetail), "SalesOrder.SONumber", MatchType.Exact, code))
            cVCA.opAnd(New Criteria(GetType(TransferPaymentDetail), "TransferPayment.ID", MatchType.Exact, ID))

            Dim objD As New TransferPaymentDetail
            Dim ObjDA As New ArrayList
            ObjDA = _ProductCategoryFacade.Retrieve(cVCA)
            If Not IsNothing(ObjDA) AndAlso ObjDA.Count > 0 Then
                objD = CType(ObjDA(0), TransferPaymentDetail)
            End If
            Return objD
        End Function

        Private Function RetrieveTransferPayment(ByVal code As String) As TransferPayment
            Dim _TransferPaymentF As TransferPaymentFacade = New TransferPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Return _TransferPaymentF.Retrieve(code.Trim())
        End Function
#End Region

    End Class
End Namespace

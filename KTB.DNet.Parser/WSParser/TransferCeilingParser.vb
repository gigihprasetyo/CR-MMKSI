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

#End Region

Namespace KTB.DNet.Parser

    Public Class TransferCeilingParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _TransferCeilings As ArrayList
        Private _fileName As String
        Private _TransferCeiling As TransferCeiling
        Private _TransferCeilingDetail As TransferCeilingDetail
        Private errorMessage As StringBuilder
        Private _TC As TC
        Private _TCs As ArrayList
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"
        Private Class TC
            Private _CreditAccount As String
            Private _ProductCategory As ProductCategory
            Private _TermOfPayment As TermOfPayment
            Private _TransferCeilings As New ArrayList
            Private _errorMessage As StringBuilder

            Public Property CreditAccount() As String
                Get
                    Return _CreditAccount
                End Get
                Set(value As String)
                    _CreditAccount = value
                End Set
            End Property
            Public Property ProductCategory() As ProductCategory
                Get
                    Return _ProductCategory
                End Get
                Set(value As ProductCategory)
                    _ProductCategory = value
                End Set
            End Property

            Public Property TermOfPayment() As TermOfPayment
                Get
                    Return _TermOfPayment
                End Get
                Set(value As TermOfPayment)
                    _TermOfPayment = value
                End Set
            End Property

            Public Property TransferCeilings() As ArrayList
                Get
                    Return _TransferCeilings
                End Get
                Set(value As ArrayList)
                    _TransferCeilings = value
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

        Private Function ParseHeader(ByVal line As String) As TC
            Dim oTC As New TC
            Dim oPC As ProductCategory
            Dim oTOP As TermOfPayment
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)


            errorMessage = New StringBuilder()
            If cols.Length <> 4 Then
                errorMessage.Append("Invalid Header Format" & Chr(13) & Chr(10))
            Else
                'Credit Account
                If cols(1).Trim = String.Empty Then
                    errorMessage.Append("Credit Account Empty" & Chr(13) & Chr(10))
                Else
                    Dim cVCA As New CriteriaComposite(New Criteria(GetType(v_CreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim aVCAs As ArrayList
                    cVCA.opAnd(New Criteria(GetType(v_CreditAccount), "CreditAccount", MatchType.Exact, cols(1)))
                    aVCAs = (New v_CreditAccountFacade(user)).Retrieve(cVCA)

                    If (aVCAs.Count = 0) Then
                        errorMessage.Append("Credit Account is not Found!" & Chr(13) & Chr(10))
                    End If
                End If
                oTC.CreditAccount = cols(1)

                'Product Category
                oTC.ProductCategory = Nothing

                If cols(2).Trim = String.Empty Then
                    errorMessage.Append("Product Category Cant Not Be Empty!" & Chr(13) & Chr(10))
                Else
                    Dim cPC As New CriteriaComposite(New Criteria(GetType(ProductCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim aPCs As ArrayList

                    cPC.opAnd(New Criteria(GetType(ProductCategory), "Name", MatchType.Exact, cols(2)))
                    aPCs = New ProductCategoryFacade(user).Retrieve(cPC)
                    If (aPCs.Count = 0) Then
                        errorMessage.Append("Product Category is not Found!" & Chr(13) & Chr(10))
                    Else
                        oTC.ProductCategory = aPCs(0)
                    End If
                End If

                'Term Of Payment
                If cols(3).Trim() = String.Empty Then
                    errorMessage.Append("Payment Type Can't Be Empty" & Chr(13) & Chr(10))
                Else
                    Dim cTP As New CriteriaComposite(New Criteria(GetType(TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim aTPs As ArrayList
                    cTP.opAnd(New Criteria(GetType(TermOfPayment), "Description", MatchType.Partial, cols(3)))
                    aTPs = New TermOfPaymentFacade(user).Retrieve(cTP)
                    If aTPs.Count = 0 Then
                        errorMessage.Append("Payment Type Not Valid" & Chr(13) & Chr(10))
                    Else
                        oTC.TermOfPayment = aTPs(0)
                    End If
                End If

            End If

            Return oTC
        End Function

        Private Function ParseDetail(ByVal line As String) As TransferCeiling
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim oTC As New TransferCeiling

            If (cols.Length <> 3) Then
                errorMessage.Append("Invalid Detail Format" & Chr(13) & Chr(10))
            Else
                'Effective Date
                oTC.EffectiveDate = MyBase.GetDateShort(cols(1))
                If oTC.EffectiveDate.Year = 1900 Then
                    errorMessage.Append("Effective Date Is Invalid Date Format" & Chr(13) & Chr(10))
                End If
                'Available Ceiling
                Try
                    oTC.AvailableCeiling = MyBase.GetCurrency(cols(2))
                Catch ex As Exception
                    oTC.AvailableCeiling = 0
                    errorMessage.Append("Available Ceiling Is Not Valid Format" & Chr(13) & Chr(10))
                End Try

            End If
            Return oTC
        End Function

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                _TCs = New ArrayList()
                _TC = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_TC) Then
                                If Not IsNothing(errorMessage) Then _TC.ErrorMessage = errorMessage
                                _TCs.Add(_TC)
                                _TC = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _TC = ParseHeader(line)
                        ElseIf ind = MyBase.IndicatorDetail Then
                            _TransferCeiling = ParseDetail(line)
                            _TC.TransferCeilings.Add(_TransferCeiling)
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "TransferCeilingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.UserParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _TC = Nothing
                    End Try
                Next

                If Not IsNothing(_TransferCeiling) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _TC.ErrorMessage = errorMessage
                    _TCs.Add(_TC)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try
            Return _TCs
        End Function


        'Protected Overrides Function DoParseXXX(ByVal fileName As String, ByVal user As String) As Object
        Private Function DoParseXXX(ByVal fileName As String, ByVal user As String) As Object

            Try
                _Stream = New StreamReader(fileName, True)
                _fileName = fileName
                _TransferCeilings = New ArrayList
                Dim val As String = MyBase.NextLine(_Stream).Trim()
                While (Not val = "")
                    Try
                        If Not _TransferCeiling Is Nothing Then
                            _TransferCeilings.Add(_TransferCeiling)
                        End If
                        _TransferCeiling = ParseTransferCeilingHeader(val + Delimited)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "TransferCeilingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.UserParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _TransferCeiling = Nothing
                    End Try
                    val = MyBase.NextLine(_Stream)
                End While
                If Not _TransferCeiling Is Nothing Then
                    _TransferCeilings.Add(_TransferCeiling)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                _Stream.Close()
                _Stream = Nothing
            End Try
            Return _TransferCeilings
        End Function


        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Try
                'o2n
                _TransferCeilings = New ArrayList

                For Each val As String In ValueString.Split(vbNewLine)
                    val = val.Trim()
                    If val.Trim() <> "" Then
                        Try
                            If val.Substring(0, 1).ToUpper.Equals("H") Then
                                If Not _TransferCeiling Is Nothing Then
                                    _TransferCeilings.Add(_TransferCeiling)
                                End If
                                _TransferCeiling = ParseTransferCeilingHeader(val + Delimited)

                            Else
                                'If Not _PurchaseOrderHeader Is Nothing Then
                                '    ParsePurchaseOrderDetail(val + Delimited, _PurchaseOrderHeader) 'Order detail input
                                'End If
                            End If
                        Catch ex As Exception
                            'SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "TransferCeilingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.UserParser, BlockName)
                            'Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                            'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                            '_TransferCeiling = Nothing
                        End Try
                    End If
                Next

                If Not _TransferCeiling Is Nothing Then
                    _TransferCeilings.Add(_TransferCeiling)
                End If
            Catch ex As Exception
                Throw ex
            Finally

            End Try
            Return _TransferCeilings
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim aTCTrans As New ArrayList
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim oTCFac As New TransferCeilingFacade(user)

            For i As Integer = 0 To _TCs.Count - 1
                _TC = _TCs(i)
                If (Not IsNothing(_TC.ErrorMessage) AndAlso _TC.ErrorMessage.ToString() <> String.Empty) Then
                    Throw New Exception(_TC.ErrorMessage.ToString())
                Else
                    Dim aTCs As ArrayList
                    Dim oTC As TransferCeiling

                    For j As Integer = 0 To _TC.TransferCeilings.Count - 1
                        oTC = New TransferCeiling

                        oTC.CreditAccount = _TC.CreditAccount
                        oTC.ProductCategory = _TC.ProductCategory
                        oTC.PaymentType = _TC.TermOfPayment.PaymentType
                        oTC.EffectiveDate = CType(_TC.TransferCeilings(j), TransferCeiling).EffectiveDate
                        oTC.AvailableCeiling = CType(_TC.TransferCeilings(j), TransferCeiling).AvailableCeiling

                        aTCTrans.Add(oTC)
                    Next

                    If aTCTrans.Count > 0 Then
                        If oTCFac.Insert(aTCTrans) = -1 Then 'Failed
                            SysLogParameter.LogErrorToSyslog("Transaction Insert Failed.", "ws-worker", "TransferCeilingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.DebitNoteParser, BlockName)
                        Else 'Success

                        End If
                    End If
                End If
            Next
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Function ParseTransferCeilingHeader(ByVal ValParser As String) As TransferCeiling


            _TransferCeiling = New TransferCeiling
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            Dim _dealer As New Dealer
            errorMessage = New StringBuilder
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0

                    Case Is = 1
                        If sTemp.Trim.Length > 0 Then
                            'Mapping dari DealerCode menjadi DealerID
                            Dim objProductC As ProductCategory = RetrieveProductCategory(sTemp.Trim)
                            If objProductC.ID > 0 Then
                                _TransferCeiling.ProductCategory = objProductC
                            Else
                                Throw New Exception("Product Kategori Tidak Ketemu")
                            End If
                        Else
                            errorMessage.Append("Invalid Product Code" & Chr(13) & Chr(10))
                        End If

                    Case Is = 2
                        If sTemp.Trim.Length > 0 Then
                            'Mapping dari DealerCode menjadi DealerID
                            Dim objDealer As Dealer = RetrieveDealer(sTemp.Trim)
                            If objDealer.ID > 0 Then
                                _TransferCeiling.CreditAccount = objDealer.DealerCode
                            Else
                                Throw New Exception("Credit Account Tidak Ketemu")
                            End If
                        Else
                            errorMessage.Append("Invalid Product Code" & Chr(13) & Chr(10))
                        End If

                    Case Is = 3
                        If sTemp.Length > 0 Then

                            Select Case sTemp.ToUpper()
                                Case "COD"
                                    _TransferCeiling.PaymentType = 1
                                Case "TOP"
                                    _TransferCeiling.PaymentType = 2

                                Case "RTGS"
                                    _TransferCeiling.PaymentType = 3
                                Case Else
                                    errorMessage.Append("Invalid Payment Type" & Chr(13) & Chr(10))
                            End Select
                        Else
                            'errorMessage.Append("Invalid Debit Note Number" & Chr(13) & Chr(10))
                            'dibolehkan kosong karena hubungan OR dengan assignment
                            errorMessage.Append("Invalid Payment Type" & Chr(13) & Chr(10))
                        End If
                    Case Is = 4
                        If sTemp.Length > 0 Then
                            Try
                                Dim Thn As Integer = CInt(Strings.Left(sTemp, 4))
                                Dim Bln As Integer = CInt(sTemp.Substring(4, 2))
                                Dim Tgl As Integer = CInt(sTemp.Substring(6, 2))
                                _TransferCeiling.EffectiveDate = New DateTime(Thn, Bln, Tgl)
                            Catch ex As Exception
                                errorMessage.Append("Invalid Efective Date" & Chr(13) & Chr(10))
                            End Try

                        Else
                            errorMessage.Append("Invalid Efective Date" & Chr(13) & Chr(10))
                        End If
                    Case Is = 5
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp.Trim) Then
                                _TransferCeiling.AvailableCeiling = CDec(sTemp.Trim)
                            Else
                                errorMessage.Append("Invalid Amount" & Chr(13) & Chr(10))
                            End If
                        Else
                            'errorMessage.Append("Invalid Assignment / SO ID" & Chr(13) & Chr(10))
                            errorMessage.Append("Invalid Amount" & Chr(13) & Chr(10))
                        End If

                    Case Else
                        'Do Nothing
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next
            If errorMessage.Length > 0 Then
                Throw New Exception(errorMessage.ToString)
            End If
            Return _TransferCeiling
        End Function

        Private Function RetrieveDealer(ByVal code As String) As Dealer
            Dim _dealerFacade As DealerFacade = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Return _dealerFacade.Retrieve(code)
        End Function

        Private Function RetrieveProductCategory(ByVal code As String) As ProductCategory
            Dim _ProductCategoryFacade As ProductCategoryFacade = New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Return _ProductCategoryFacade.Retrieve(code.Trim())
        End Function
#End Region

    End Class
End Namespace

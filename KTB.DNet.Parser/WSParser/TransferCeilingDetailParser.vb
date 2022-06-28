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
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Transfer

#End Region

Namespace KTB.DNet.Parser

    Public Class TransferCeilingDetailParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder


        Private _TCs As ArrayList
        Private _TC As TransferCeiling
        Private _TCD As TransferCeilingDetail
        Private _TCDs As ArrayList

#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                _TCs = New ArrayList
                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            _TC = New TransferCeiling()
                            errorMessage = New StringBuilder()
                            _TC = ParseHeader(line)
                        ElseIf ind = MyBase.IndicatorDetail Then
                            If IsNothing(_TC) OrElse Not IsNothing(_TC.ErrorMessage) Then
                            Else
                                _TCD = ParseDetail(line)

                                If Not IsNothing(_TCD) AndAlso (IsNothing(_TCD.ErrorMessage) OrElse _TCD.ErrorMessage = String.Empty) Then
                                    _TCD.TransferCeiling = _TC
                                    _TC.TransferCeilingDetails.Add(_TCD)
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "SOCreateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _TC = Nothing
                    End Try
                Next

                If Not IsNothing(_TC) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _TC.ErrorMessage = errorMessage.ToString()
                    _TCs.Add(_TC)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try
            Return _TCs
        End Function


        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim TCFac As New TransferCeilingFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WS"), Nothing))
            Dim TCDFac As TransferCeilingDetailFacade
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For Each oTC As TransferCeiling In _TCs
                Try
                    If Not IsNothing(oTC.ErrorMessage) AndAlso oTC.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & oTC.ErrorMessage.ToString() & ";"
                    Else
                        TCFac.UpdateDetail(oTC)
                    End If
                Catch ex As Exception
                    nError = nError + 1
                    sMsg = sMsg & ex.Message
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "TransferCeilingDetailParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "TransferCeilingDetailParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return Nothing
        End Function

#End Region

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As TransferCeiling
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)

            Dim oPOH As New POHeader
            Dim oPOHFac As New POHeaderFacade(user)
            Dim oSOFac As New SalesOrderFacade(user)
            Dim oSO As New SalesOrder()

            errorMessage = New StringBuilder()
            If cols.Length <> 6 Then
                writeError("Invalid Header Format")
            Else
                '0:              H()
                '1:              CreditAccount()
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
                _TC.CreditAccount = cols(1)
                '2:              SalesOrg()

                _TC.ProductCategory = Nothing

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
                        _TC.ProductCategory = aPCs(0)
                    End If
                End If

                '3:              PaymentType()
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
                        _TC.PaymentType = CType(aTPs(0), TermOfPayment).PaymentType
                    End If
                End If

                '4:              EffDate()
                _TC.EffectiveDate = MyBase.GetDateShort(cols(4))
                If _TC.EffectiveDate.Year = 1900 Then
                    errorMessage.Append("Invalid Effective Date")
                End If
                '5:              Plafon()
                Try
                    _TC.BalanceBefore = MyBase.GetCurrency(cols(5))
                Catch ex As Exception
                    errorMessage.Append("Invalid Plafon")
                End Try

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    If IsNothing(_TC) Then _TC = New TransferCeiling
                    _TC.ErrorMessage = errorMessage.ToString()
                Else
                    _TC.CreatedBy = "SAP WS"

                    Dim aTCs As ArrayList
                    Dim TCFac As New TransferCeilingFacade(user)
                    Dim cTC As New CriteriaComposite(New Criteria(GetType(TransferCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim oTCDb As TransferCeiling

                    cTC.opAnd(New Criteria(GetType(TransferCeiling), "ProductCategory.ID", MatchType.Exact, _TC.ProductCategory.ID))
                    cTC.opAnd(New Criteria(GetType(TransferCeiling), "CreditAccount", MatchType.Exact, _TC.CreditAccount))
                    cTC.opAnd(New Criteria(GetType(TransferCeiling), "PaymentType", MatchType.Exact, _TC.PaymentType))
                    cTC.opAnd(New Criteria(GetType(TransferCeiling), "EffectiveDate", MatchType.Exact, _TC.EffectiveDate.ToString("yyyy.MM.dd")))

                    aTCs = TCFac.Retrieve(cTC)
                    If aTCs.Count > 0 Then
                        oTCDb = aTCs(0)
                        oTCDb.BalanceBefore = _TC.BalanceBefore
                        oTCDb.EffectiveDate = _TC.EffectiveDate

                        _TC = oTCDb
                    Else
                        _TC.ErrorMessage = "Transfer Ceiling Header Is Not Exists"
                    End If
                End If
            End If

            Return _TC
        End Function

        Private Function ParseDetail(ByVal line As String) As TransferCeilingDetail
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim oSOFac As New SalesOrderFacade(user)
            Dim oSO As New SalesOrder()
            Dim oTPFac As New TransferPaymentFacade(user)
            Dim oTP As New TransferPayment

            _TCD = New TransferCeilingDetail()
            errorMessage = New StringBuilder()
            If (cols.Length <> 4) Then
                errorMessage.Append("Invalid Detail Format" & Chr(13) & Chr(10))
            Else
                '0:              D()
                '1:              P()
                '2:              Num()
                '3:              Amont()
                If cols(1).Trim().ToLower() = "s" Then
                    If cols(2).Trim = String.Empty Then
                        writeError("SO Number can't be empty")
                    Else
                        oSO = oSOFac.Retrieve(cols(2).Trim())
                    End If
                    If oSO Is Nothing OrElse oSO.ID < 1 Then
                        writeError("Invalid SO Number")
                    Else
                        _TCD.SalesOrder = oSO 'ID = oSO.ID
                    End If
                    _TCD.IsIncome = 0
                ElseIf cols(1).Trim().ToLower() = "p" Then
                    If cols(2).Trim = String.Empty Then
                        writeError("Reg Number can't be empty")
                    Else
                        oTP = oTPFac.Retrieve(cols(2).Trim())
                    End If
                    If oTP Is Nothing OrElse oTP.ID < 1 Then
                        writeError("Invalid Reg Number")
                    Else
                        _TCD.TransferPayment = oTP ' ID = oTP.ID
                    End If
                    _TCD.IsIncome = 1
                End If
                Try
                    _TCD.Amount = MyBase.GetCurrency(cols(3))
                Catch ex As Exception
                    _TCD.Amount = 0
                    writeError("Invalid Amount")
                End Try
                If Not IsNothing(errorMessage) Then
                    _TCD.ErrorMessage = errorMessage.ToString()
                End If
            End If
            Return _TCD
        End Function


        Private Function RetrieveContract(ByVal _ContractNumber As String) As ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "ContractNumber", MatchType.Exact, _ContractNumber))
            Dim objContractList As ArrayList = New ContractHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            Return objContractList
        End Function


        Private Function RetrieveContractDetail(ByVal _ContractNumber As String, ByVal materialNumber As String) As ContractDetail
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "MaterialNumber", MatchType.Exact, materialNumber))
            Dim objVColorList As ArrayList = New VechileColorFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            Dim objContractList As ArrayList = RetrieveContract(_ContractNumber)
            If objVColorList.Count > 0 And objContractList.Count > 0 Then
                For Each oCD As ContractDetail In CType(objContractList(0), ContractHeader).ContractDetails
                    If oCD.VechileColor.MaterialNumber = materialNumber Then
                        Return oCD
                    End If
                Next
                Return Nothing
                'Dim contractID As Integer = CType(objContractList.Item(0), ContractHeader).ID
                'Dim vecID As Integer = CType(objVColorList.Item(0), VechileColor).ID
                'Dim _contractDetail As ContractDetail = RetrieveContractDetail(CType(objContractList.Item(0), ContractHeader).ContractNumber, CType(objVColorList.Item(0), VechileColor).MaterialNumber)  'GetContractDetail(CType(objContractList.Item(0), ContractHeader), vecID) ' GetContractDetail(contractID, vecID)

                'If Not _contractDetail Is Nothing Then
                '    Return _contractDetail
                'Else
                '    Return Nothing
                'End If
            Else
                Return Nothing
            End If
        End Function


#End Region

    End Class
End Namespace

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Collections.Generic
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Transfer
Imports KTB.DNet.BusinessFacade.PK

#End Region

Namespace KTB.DNet.Parser


    Public Class CeilingUpdateParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder


        Private _TCs As ArrayList
        Private _XTC As TransferCeiling
        Private _XTCD As TransferCeilingDetail
        Private _TCDs As ArrayList

        Private _TCH As TCHeader
        Private _TCD As TCDetail

        Private _LsTCHeader As List(Of TCHeader)
        Private _KeyName As String

#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
            _LsTCHeader = New List(Of TCHeader)
            _KeyName = String.Empty
        End Sub


        Public Sub New(ByVal keyname As String)
            Grammar = MyBase.GetGrammarParser()
            _LsTCHeader = New List(Of TCHeader)
            _KeyName = keyname
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try


                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                _TCs = New ArrayList
                _TCH = New TCHeader()
                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then

                            errorMessage = New StringBuilder()
                            _TCH = ParseHeader(line)
                        ElseIf ind = MyBase.IndicatorDetail Then
                            If IsNothing(_TCH) OrElse Not IsNothing(_TCH.ErrorMessage) Then
                            Else
                                If line.Split(MyBase.ColSeparator)(1).ToLower() <> "c" Then
                                    _TCD = ParseDetail(line)

                                    If Not IsNothing(_TCD) AndAlso (IsNothing(_TCD.ErrorMessage) OrElse _TCD.ErrorMessage = String.Empty) Then
                                        _TCH.TCDetail.Add(_TCD)
                                    End If
                                Else
                                    ParseSubHeader(line, _TCH)
                                End If

                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "SOCreateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")

                    End Try
                Next



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
            Dim TCDFac As New TransferCeilingDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WS"), Nothing))
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim execHeader As Boolean = False

            Try
                Dim EffecticeDateHeader As String = _KeyName.Split("_")(1).ToString()
                Dim _year As Integer = CInt(EffecticeDateHeader.Substring(0, 4))
                Dim _month As Integer = CInt(EffecticeDateHeader.Substring(4, 2))
                Dim _date As Integer = CInt(EffecticeDateHeader.Substring(6, 2))

                _TCH.EffectiveDate = New DateTime(_year, _month, _date)
            Catch ex As Exception
            _TCH.EffectiveDate = Date.Today
            End Try






            If IsNothing(_TCH.ErrorMessage) OrElse _TCH.ErrorMessage.ToString() = String.Empty Then
                Dim StrSQLHeader As String = " exec TransferCeiling_BalanceUpdate @ProductCategoryID={0}, @CreditAccount='{1}', @PaymentType={2}, @EffectiveDate='{3}', @BalanceBefore={4}"
                StrSQLHeader = String.Format(StrSQLHeader, _TCH.ProductCategoryID, _TCH.CreditAccount, _TCH.PaymentType, _TCH.EffectiveDate.ToString("yyyy/MM/dd"), FormatNumber(_TCH.BalanceBefore, 0, TriState.UseDefault, TriState.UseDefault, TriState.True).Replace(",", "").Replace(".", ""))
                execHeader = TCFac.ExecuteSP(StrSQLHeader)
            End If
            For Each oTC As TCDetail In _TCH.TCDetail
                Try
                    If Not IsNothing(oTC.ErrorMessage) AndAlso oTC.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & oTC.ErrorMessage.ToString() & ";"
                    Else
                        Dim StrSQLDetail As String = " exec TransferCeilingDetail_BalanceUpdate @ProductCategoryID={0}, @CreditAccount='{1}', @PaymentType={2}, @EffectiveDateHeader='{3}',@Amount={4}"

                        StrSQLDetail = String.Format(StrSQLDetail, _TCH.ProductCategoryID, _TCH.CreditAccount, _TCH.PaymentType, _TCH.EffectiveDate.ToString("yyyy/MM/dd"), FormatNumber(oTC.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True).Replace(",", "").Replace(".", ""))
                        If oTC.IsIncome = 1 Then
                            StrSQLDetail = StrSQLDetail & ", @IsIncome={0},@TransDateDetail='{1}', @TransferPaymentID={2} "
                            StrSQLDetail = String.Format(StrSQLDetail, oTC.IsIncome, oTC.TransDate.ToString("yyyy/MM/dd"), oTC.TransferPayment.ID)
                        Else
                            StrSQLDetail = StrSQLDetail & ", @IsIncome={0},@TransDateDetail='{1}', @SalesOrderID={2} "
                            StrSQLDetail = String.Format(StrSQLDetail, oTC.IsIncome, oTC.TransDate.ToString("yyyy/MM/dd"), oTC.SalesOrder.ID)
                        End If


                        TCDFac.ExecuteSP(StrSQLDetail)
                    End If
                Catch ex As Exception
                    nError = nError + 1
                    sMsg = sMsg & ex.Message
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "CeilingUpdateParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "CeilingUpdateParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As TCHeader
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)

            Dim _ObjTch As New TCHeader

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
                _ObjTch.CreditAccount = cols(1)
                '2:              SalesOrg()

                _ObjTch.ProductCategory = Nothing

                If cols(2).Trim = String.Empty Then
                    errorMessage.Append("Product Category Cant Not Be Empty!" & Chr(13) & Chr(10))
                Else
                    If cols(2).Trim = "S101" Then
                        _ObjTch.ProductCategoryID = 2
                    Else
                        _ObjTch.ProductCategoryID = 1
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
                        _ObjTch.PaymentType = CType(aTPs(0), TermOfPayment).PaymentType
                    End If
                End If

                '4:              EffDate()
                _ObjTch.EffectiveDate = Date.Today ' MyBase.GetDateShort(cols(4))
                If _ObjTch.EffectiveDate.Year = 1900 Then
                    errorMessage.Append("Invalid Effective Date")
                End If
                '5:              Plafon()
                Try
                    _ObjTch.BalanceBefore = MyBase.GetCurrency(cols(5))
                Catch ex As Exception
                    errorMessage.Append("Invalid Plafon")
                End Try

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    If IsNothing(_ObjTch) Then _ObjTch = New TCHeader
                    _ObjTch.ErrorMessage = errorMessage.ToString()
                Else
                    _ObjTch.CreatedBy = "SAP WS"

                    Dim aTCs As ArrayList
                    Dim TCFac As New TransferCeilingFacade(user)
                    Dim cTC As New CriteriaComposite(New Criteria(GetType(TransferCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim oTCDb As TransferCeiling

                    cTC.opAnd(New Criteria(GetType(TransferCeiling), "ProductCategory.ID", MatchType.Exact, _ObjTch.ProductCategoryID))
                    cTC.opAnd(New Criteria(GetType(TransferCeiling), "CreditAccount", MatchType.Exact, _ObjTch.CreditAccount))
                    cTC.opAnd(New Criteria(GetType(TransferCeiling), "PaymentType", MatchType.Exact, _ObjTch.PaymentType))
                    cTC.opAnd(New Criteria(GetType(TransferCeiling), "EffectiveDate", MatchType.Exact, DateTime.Now.ToString("yyyy.MM.dd")))

                    aTCs = TCFac.Retrieve(cTC)
                    If aTCs.Count > 0 Then
                        oTCDb = aTCs(0)
                        _ObjTch.ID = oTCDb.ID
                    Else
                        _ObjTch.ErrorMessage = "Transfer Ceiling Header Is Not Exists"
                    End If
                End If
            End If

            Return _ObjTch
        End Function

        Private Function ParseDetail(ByVal line As String) As TCDetail
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim oSOFac As New SalesOrderFacade(user)
            Dim oSO As New SalesOrder()
            Dim oTPFac As New TransferPaymentFacade(user)
            Dim oTP As New TransferPayment

            errorMessage = New StringBuilder()
            _TCD = New TCDetail()
            If (cols.Length = 0) Then
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

                ElseIf cols(1).Trim().ToLower() = "c" Then

                End If
                Try
                    _TCD.Amount = MyBase.GetCurrency(cols(3))
                Catch ex As Exception
                    _TCD.Amount = 0
                    writeError("Invalid Amount")
                End Try

                Try
                    _TCD.TransDate = MyBase.GetDateShort(cols(4))
                Catch ex As Exception
                    _TCD.Amount = 0
                    writeError("Invalid Date")
                End Try

                If Not IsNothing(errorMessage) Then
                    _TCD.ErrorMessage = errorMessage.ToString()
                End If
            End If
            Return _TCD
        End Function

        Private Sub ParseSubHeader(ByVal line As String, ByRef objTcHeader As TCHeader)
            Dim cols As String() = line.Split(MyBase.ColSeparator)

            If cols(1).Trim().ToLower() = "c" Then
                Try
                    objTcHeader.BalanceBefore = MyBase.GetCurrency(cols(3))
                Catch ex As Exception
                    objTcHeader.ErrorMessage = "Invalid Balance"
                End Try

                Try
                    objTcHeader.EffectiveDate = MyBase.GetDateShort(cols(4))
                Catch ex As Exception
                    objTcHeader.ErrorMessage = "Invalid Date"
                End Try

                Return
            End If
        End Sub

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


#Region "Private Class"
        Private Class TCDetail
            Inherits TransferCeilingDetail

            Private _TransDate As DateTime

            Public Property TransDate As DateTime
                Get
                    Return _TransDate
                End Get
                Set(value As DateTime)
                    _TransDate = value
                End Set
            End Property
        End Class

        Private Class TCHeader
            Inherits TransferCeiling

            Private _ProductCategoryID As Integer
            Private _LsTCDetail As List(Of TCDetail)


            Public Sub New()
                _LsTCDetail = New List(Of TCDetail)
            End Sub


            Public Property ProductCategoryID As Integer
                Get
                    Return _ProductCategoryID
                End Get
                Set(value As Integer)
                    _ProductCategoryID = value
                End Set
            End Property


            Public Property TCDetail As List(Of TCDetail)
                Get
                    Return _LsTCDetail
                End Get
                Set(value As List(Of TCDetail))
                    _LsTCDetail = value
                End Set
            End Property
        End Class

#End Region
    End Class
End Namespace

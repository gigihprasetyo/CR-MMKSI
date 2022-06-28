#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.PO
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Configuration
#End Region

Namespace KTB.DNet.Parser
    Public Class DailyPaymentParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _DailyPayments As ArrayList
        Private Grammar As Regex
        Private mapper As IMapper
        Private _fileName As String
        Private ErrorMessage As StringBuilder
        Private _isCleansing As Integer '0 Production, 1 Cleansing
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _isCleansing = Val(KTB.DNet.Lib.WebConfig.GetValue("ISCLEANSING"))

            _fileName = fileName
            _Stream = New StreamReader(fileName, True)
            _DailyPayments = New ArrayList
            Dim vals As String = MyBase.NextLine(_Stream).Trim()

            While (Not vals = "")
                Try
                    ParseDailyPayment(vals + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DailyPaymentParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DailyPaymentParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & vals & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                vals = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return _DailyPayments
        End Function


        Protected Overrides Function DoTransaction() As Integer
            'TO DO call facade to insert using transaction
            Dim _DailyPaymentFacade As DailyPaymentFacade
            Dim arlTemp As New ArrayList
            '_DailyPaymentFacade._insertDP(_DailyPayments)
            _DailyPaymentFacade = New DailyPaymentFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)))

            For Each item As DailyPayment In _DailyPayments
                Try
                    Dim objDP As DailyPayment
                    If Not IsNothing(item.POHeader) Then
                        Dim ID As Integer
                        objDP = GetDailyPayment(item.POHeader.ID, item.DocNumber, item.FiscalYear)
                        ID = objDP.ID
                        objDP = item
                        objDP.ID = ID
                        'ToDO : Donas, please analyze due to DPH.RegNumber additional column
                    End If
                    _DailyPaymentFacade = New DailyPaymentFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)))
                    _DailyPaymentFacade.insertDP(objDP)
                    'If Not IsNothing(objDP) AndAlso objDP.ID > 0 Then
                    '    If objDP.IsReversed = 0 Then
                    '        _DailyPaymentFacade.insertDP(objDP)
                    '    Else
                    '        arlTemp = New ArrayList
                    '        arlTemp.Add(objDP)
                    '        _DailyPaymentFacade.Update(arlTemp)
                    '    End If
                    'Else
                    '    _DailyPaymentFacade.insertDP(objDP)
                    'End If

                    '_DailyPaymentFacade.insertDP(item)
                    'If objDP.ID > 0 Then
                    '    'update
                    'Else
                    '    'insert
                    'End If

                    'If objDP.ID > 0 And item.IsReversed = 0 Then
                    '    _DailyPaymentFacade = New DailyPaymentFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)))
                    '    _DailyPaymentFacade.insertDP(item)
                    'Else
                    '    'Todo   :put this logic into facade 
                    '    Dim tmpArlDP As New ArrayList
                    '    tmpArlDP.Add(item)
                    '    Dim arlDP As New ArrayList
                    '    Dim arlDPToUpdate As New ArrayList
                    '    Dim crtDP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    '    crtDP.opAnd(New Criteria(GetType(DailyPayment), "DocNumber", MatchType.Exact, item.DocNumber))
                    '    arlDP = _DailyPaymentFacade.Retrieve(crtDP)
                    '    For Each oDP As DailyPayment In arlDP
                    '        oDP.IsReversed = 1
                    '        'If oDP.ID = item.ID Then
                    '        oDP.EffectiveDate = item.EffectiveDate
                    '        oDP.IsCleared = item.IsCleared
                    '        'End If
                    '        arlDPToUpdate.Add(oDP)
                    '    Next
                    '    _DailyPaymentFacade = New DailyPaymentFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)))
                    '    _DailyPaymentFacade.Update(arlDPToUpdate)
                    'End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DailyPaymentParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DailyPaymentParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.DocNumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"


        Private Function GetDailyPayment(ByVal POID As Integer, ByVal docNumber As String, Optional ByVal FiscalYear As Short = 0) As DailyPayment
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "DocNumber", MatchType.Exact, docNumber))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, POID))
            Dim _dailyPOCollection As ArrayList = New DailyPaymentFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)

            If _dailyPOCollection.Count > 0 Then
                For Each objDP As DailyPayment In _dailyPOCollection
                    If objDP.FiscalYear = FiscalYear Then
                        Return objDP
                    End If
                Next
                Return _dailyPOCollection.Item(0)
            Else
                Return New DailyPayment
            End If

            If _dailyPOCollection.Count > 0 Then
                Return _dailyPOCollection.Item(0)
            Else
                Return New DailyPayment
            End If
        End Function


        Private Sub ParseDailyPayment(ByVal ValParser As String)
            Dim _Dp As DailyPayment = New DailyPayment
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim IsDataCessie As Boolean = False
            sStart = 0
            nCount = 0
            ErrorMessage = New StringBuilder
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        'Dim _arrDailyP As ArrayList
                        'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "SONumber", MatchType.Exact, sTemp.Trim))
                        ''_arrDailyP = New POHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                        '_arrDailyP = New SalesOrderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

                        'If _arrDailyP.Count > 0 Then
                        '    Dim _POHeader As POHeader = CType(_arrDailyP.Item(0), POHeader)
                        '    _Dp.POHeader = _POHeader
                        'Else
                        '    ErrorMessage.Append("Invalid SO Number" & Chr(13) & Chr(10))
                        'End If
                        If sTemp.Trim <> String.Empty Then
                            If _isCleansing = 0 Then    'Production
                                Dim objSalesOrder As SalesOrder
                                objSalesOrder = New SalesOrderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)

                                If Not IsNothing(objSalesOrder) AndAlso objSalesOrder.ID > 0 Then
                                    _Dp.POHeader = objSalesOrder.POHeader
                                    _Dp.SalesOrder = objSalesOrder
                                Else
                                    Dim oCFac As CessieFacade = New CessieFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                    Dim oC As Cessie = oCFac.Retrieve(sTemp.Trim)
                                    If Not IsNothing(oC) AndAlso oC.ID > 0 Then
                                        _Dp.POHeader = New POHeader
                                        _Dp.SalesOrder = New SalesOrder
                                        _Dp.Reason = oC.CessieNumber
                                        IsDataCessie = True
                                    Else
                                        ErrorMessage.Append("Invalid SO Number" & Chr(13) & Chr(10))
                                    End If
                                End If
                            Else                        'Cleansing
                                Dim _arrPO As ArrayList
                                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.POHeader), "SONumber", MatchType.Exact, sTemp.Trim))
                                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "SONumber", MatchType.Exact, sTemp.Trim))
                                _arrPO = New POHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

                                If _arrPO.Count > 0 Then
                                    _Dp.POHeader = _arrPO(0)
                                    '_Dp.SalesOrder = objSalesOrder
                                    Dim objSalesOrder As SalesOrder
                                    objSalesOrder = New SalesOrderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                                    If objSalesOrder.ID > 0 Then
                                        _Dp.SalesOrder = objSalesOrder
                                    End If
                                Else
                                    ErrorMessage.Append("Invalid SO Number" & Chr(13) & Chr(10))
                                End If
                            End If
                        End If

                    Case Is = 1
                        _Dp.DocNumber = sTemp
                    Case Is = 2
                        If sTemp.Length > 0 Then
                            _Dp.ReceiptNumber = sTemp
                        Else
                            ErrorMessage.Append("Invalid Receipt Number" & Chr(13) & Chr(10))
                        End If
                    Case Is = 3
                        '_Dp.PaymentPurpose.Description = sTemp
                        Dim _arrDailyP As ArrayList
                        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PaymentPurpose), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PaymentPurpose), "PaymentPurposeCode", MatchType.Exact, sTemp))
                        _arrDailyP = New PaymentPurposeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

                        If _arrDailyP.Count > 0 Then
                            Dim _PaymentPurpose As PaymentPurpose = CType(_arrDailyP.Item(0), PaymentPurpose)
                            _Dp.PaymentPurpose = _PaymentPurpose
                        Else
                            ErrorMessage.Append("Invalid Payment purpose" & Chr(13) & Chr(10))
                        End If
                    Case Is = 4
                        _Dp.SlipNumber = sTemp
                        'cek bank
                        Dim intSpacePos As Integer = _Dp.SlipNumber.IndexOf(" ")
                        Dim strFirst As String = ""
                        Dim strSecond As String = ""
                        Dim oBank As Bank = New Bank
                        Dim oBFac As BankFacade = New BankFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        strFirst = Left(_Dp.SlipNumber, intSpacePos).Trim
                        strSecond = Right(_Dp.SlipNumber, _Dp.SlipNumber.Length - intSpacePos).Trim
                        'assume as non-transfer
                        oBank = oBFac.Retrieve(strFirst)
                        If IsNothing(oBank) OrElse oBank.ID < 1 Then
                            'assume as transfer
                            oBank = oBFac.Retrieve(strSecond)
                            If IsNothing(oBank) OrElse oBank.ID < 1 Then
                                ErrorMessage.Append("Invalid Bank Code" & Chr(13) & Chr(10))
                            End If
                        End If
                        If _Dp.SlipNumber.Trim.StartsWith("TRF") Then
                            _Dp.EntryType = EnumGyroEntryType.EntryType.Transfer
                        Else
                            _Dp.EntryType = EnumGyroEntryType.EntryType.Gyro
                        End If
                    Case Is = 5
                        Dim tgl As String
                        Try
                            'tgl = sTemp.Substring(2, 2).ToString & "-" & sTemp.Substring(0, 2) & "-" & sTemp.Substring(4, 4)
                            Dim _date As Date = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2))
                            _Dp.DocDate = _date
                        Catch ex As Exception
                            ErrorMessage.Append("Invalid Doc Date" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 6
                        Dim tgl As String
                        Try
                            ' tgl = sTemp.Substring(2, 2).ToString & "-" & sTemp.Substring(0, 2) & "-" & sTemp.Substring(4, 4)
                            Dim _date As Date = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2))
                            _Dp.BaselineDate = _date
                        Catch ex As Exception
                            ErrorMessage.Append("Invalid BaselineDate Date" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 7
                        _Dp.Amount = sTemp
                    Case Is = 8
                        _Dp.SAPCreator = sTemp
                        'Case Is = 9
                        '_Dp.StatusTolakan = sTemp
                    Case Is = 9
                        Try
                            Dim _date As Date = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2))
                            _Dp.EffectiveDate = _date
                        Catch ex As Exception
                            ErrorMessage.Append("Invalid Effective Date" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 10
                        If sTemp.Trim = String.Empty Then  '"X"
                            _Dp.IsReversed = 0
                        Else
                            _Dp.IsReversed = 1
                            If sTemp.Trim.ToUpper = "X" Then
                            Else
                                _Dp.IsReversed = 2
                                '_Dp.DocNumber = sTemp
                                _dp.reverseddocnumber = stemp
                            End If
                        End If
                    Case Is = 11
                        If sTemp.ToUpper.Trim = "X" Then
                            _Dp.IsCleared = 1
                        Else
                            _Dp.IsCleared = 0
                        End If
                    Case Is = 12
                        If Not IsDataCessie Then
                            _Dp.Reason = sTemp.Trim
                        End If
                    Case Is = 13
                        Try
                            Dim _date As Date = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2))
                            _Dp.EntryDate = _date
                        Catch ex As Exception
                            ErrorMessage.Append("Invalid Entry Date" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 14
                        _Dp.PIC = sTemp.Trim
                    Case Is = 15
                        Try
                            If sTemp.Trim.Length <> 4 Then
                                ErrorMessage.Append("Invalid Fiscal Year" & Chr(13) & Chr(10))
                            Else
                                _Dp.FiscalYear = CType(sTemp.Trim, Integer)
                            End If
                        Catch ex As Exception
                            ErrorMessage.Append("Invalid Fiscal Year" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 16
                        Try
                            Dim oDPH As DailyPaymentHeader = New DailyPaymentHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            If IsNothing(oDPH) Then oDPH = New DailyPaymentHeader
                            _Dp.DailyPaymentHeader = oDPH
                        Catch ex As Exception
                            _Dp.DailyPaymentHeader = New DailyPaymentHeader
                        End Try
                    Case Else
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next

            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            End If
            If IsNothing(_Dp.POHeader) Then _Dp.POHeader = New POHeader
            'If IsNothing(_Dp.SalesOrder) Then _Dp.SalesOrder = New SalesOrder
            If (Not IsNothing(_Dp.POHeader)) Or (Not IsNothing(_Dp.PaymentPurpose)) Or (Not IsNothing(_Dp.ReceiptNumber)) Then
                _DailyPayments.Add(_Dp)
            End If

        End Sub

#End Region

    End Class
End Namespace
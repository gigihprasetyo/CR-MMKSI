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
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class ListInvoiceParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _ListInvoices As ArrayList
        Private _ListLogisticDNs As ArrayList
        Private Grammar As Regex
        Private mapper As IMapper
        Private _fileName As String
        Private ErrorMessage As StringBuilder

        Private _LogisticCostDocType As String = "ZL7A;ZL7B;ZF7A"

#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _fileName = fileName
            _Stream = New StreamReader(fileName, True)
            _ListInvoices = New ArrayList
            _ListLogisticDNs = New ArrayList
            Dim val As String = MyBase.NextLine(_Stream).Trim()

            While (Not val = "")
                Try
                    ParseListInvoice(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "ListInvoiceParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ListInvoiceParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return _ListInvoices
        End Function

        Protected Overrides Function DoTransaction() As Integer
            'TO DO call facade to insert using transaction
            Dim _InvoiceFacade As InvoiceFacade
            Dim objInvoice As New Invoice

            '_ListInvoiceFacade._insertDP(_ListInvoices)
            For idx As Integer = 0 To _ListInvoices.Count - 1
                Dim item As Invoice = CType(_ListInvoices(idx), Invoice)


                Try
                    Dim IsLogistic As Boolean = False
                    For Each str As String In _LogisticCostDocType.Split(";")
                        If item.InvoiceType.Trim().ToUpper() = str Then
                            IsLogistic = True
                            Exit For
                        End If
                    Next
                    If IsLogistic Then
                        Dim user As New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
                        Dim oLDNFac As New LogisticDNFacade(user)
                        Dim oLDN As New LogisticDN
                        Dim oLDNSAP As LogisticDN = _ListLogisticDNs(idx)
                        Dim oLF As LogisticFee
                        Dim oLFFac As New LogisticFeeFacade(user)

                        oLDN = oLDNFac.Retrieve(oLDNSAP.DebitMemoNo)
                        If Not IsNothing(oLDN) AndAlso oLDN.ID > 0 Then
                            oLDN.BillingDate = oLDNSAP.BillingDate
                            oLDN.DocumentType = oLDNSAP.DocumentType
                            oLDN.TotalAmount = oLDNSAP.TotalAmount
                            oLDN.SalesOrder = oLDNSAP.SalesOrder
                            oLDN.LogisticDCHeader = oLDNSAP.LogisticDCHeader
                            oLDNFac.Update(oLDN)
                        Else
                            oLDN = oLDNSAP
                            oLDN.ID = oLDNFac.Insert(oLDN)
                        End If

                        If oLDN.ID > 0 Then

                            item.LogisticDN = oLDN

                            'insert LogisticFee
                            oLF = oLFFac.Retrieve(oLDN.SalesOrder.POHeader.Dealer.ID, "", oLDN.DebitMemoNo)
                            If Not IsNothing(oLF) AndAlso oLF.ID > 0 Then
                                oLF.Dealer = oLDN.SalesOrder.POHeader.Dealer
                                oLF.DebitMemoDate = oLDN.BillingDate
                                oLF.LogisticDN = oLDN
                                'oLF.Amount = 0.02 * oLDN.TotalAmount
                                oLF.Amount = oLDN.TotalAmount 'edited by SLA confirm by Emi
                                oLF.FileNameDebitMemo = "fudmemo_" & oLDN.DebitMemoNo.ToString()
                                oLF.FileNameLogistic = "fudclog_" & oLDN.LogisticDCHeader.DebitChargeNo.ToString()
                                oLF.Description = "Pencairan PPH Logistic Bulan " & MonthName(oLDN.BillingDate.Month) & " " & oLDN.BillingDate.Year
                                oLFFac.Update(oLF)
                            Else
                                oLF = New LogisticFee

                                oLF.Dealer = oLDN.SalesOrder.POHeader.Dealer
                                oLF.DebitMemoDate = oLDN.BillingDate
                                oLF.LogisticDN = oLDN
                                oLF.FakturPajakNo = ""
                                'oLF.Amount = 0.02 * oLDN.TotalAmount
                                oLF.Amount = oLDN.TotalAmount 'edited by SLA confirm by Emi
                                oLF.FileNameDebitMemo = "fudmemo_" & oLDN.DebitMemoNo.ToString() & ".pdf"
                                oLF.FileNameLogistic = "fudclog_" & oLDN.LogisticDCHeader.DebitChargeNo.ToString() & ".pdf"
                                oLF.Description = "Pencairan PPH Logistic Bulan " & MonthName(oLDN.BillingDate.Month) & " " & oLDN.BillingDate.Year
                                oLF.ID = oLFFac.Insert(oLF)
                            End If

                        End If

                        'Get Invoice if exist
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Invoice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                         
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "InvoiceNumber", MatchType.Exact, item.InvoiceNumber))
                        Dim sortColl As SortCollection = New SortCollection
                        sortColl.Add(New Sort(GetType(Invoice), "ID", Sort.SortDirection.DESC))
                        Dim invoiceArray As ArrayList = New InvoiceFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))).RetrieveByCriteria(criterias, sortColl)
                        If invoiceArray.Count > 0 Then
                            Dim inv As Invoice = invoiceArray(0)
                            inv.LogisticDN = oLDN
                            inv.InvoiceKind = item.InvoiceKind
                            _InvoiceFacade = New InvoiceFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)))
                            _InvoiceFacade.Update(inv)

                        Else
                            item.LogisticDN = oLDN
                            _InvoiceFacade = New InvoiceFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)))
                            _InvoiceFacade.Insert(item)
                        End If

                    Else
                        'GetLogisticDN
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticDN), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.LogisticDN), "SalesOrder.ID", MatchType.Exact, item.SalesOrder.ID))
                        Dim sortColl As SortCollection = New SortCollection
                        sortColl.Add(New Sort(GetType(LogisticDN), "ID", Sort.SortDirection.DESC))
                        Dim dnArray As ArrayList = New LogisticDNFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))).RetrieveByCriteria(criterias, sortColl)
                        Dim logisticDN As LogisticDN = Nothing
                        If dnArray.Count > 0 Then
                            logisticDN = dnArray(0)
                        End If

                        objInvoice = New InvoiceFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))).Retrieve(item.InvoiceNumber)
                        _InvoiceFacade = New InvoiceFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)))
                        If objInvoice.ID = 0 Then
                            If item.InvoiceRef = item.InvoiceNumber Then
                                item.InvoiceRef = String.Empty
                            End If
                            If Not IsNothing(logisticDN) Then
                                item.LogisticDN = logisticDN
                            End If
                            _InvoiceFacade.Insert(item)
                        Else
                            If item.InvoiceRef <> objInvoice.InvoiceNumber Then
                                objInvoice.InvoiceRef = item.InvoiceRef
                            Else
                                objInvoice.InvoiceRef = String.Empty
                            End If
                            If Not IsNothing(logisticDN) Then
                                objInvoice.LogisticDN = logisticDN
                            End If
                            objInvoice.InvoiceKind = item.InvoiceKind
                            objInvoice.InvoiceDate = item.InvoiceDate
                            objInvoice.Amount = item.Amount

                            _InvoiceFacade.Update(objInvoice)
                        End If

                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "ListInvoiceParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ListInvoiceParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.InvoiceNumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseListInvoice(ByVal ValParser As String)
            Dim _Invoice As Invoice = New Invoice
            Dim objSalesOrder As New SalesOrder
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            ErrorMessage = New StringBuilder
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0

                    Case Is = 1
                        If sTemp.Trim <> String.Empty Then
                            _Invoice.InvoiceNumber = sTemp.Trim
                        Else
                            ErrorMessage.Append("Invalid Invoice Number" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        Dim tgl As String
                        Try
                            Dim _date As Date = New Date(sTemp.Trim.Substring(4, 4), sTemp.Trim.Substring(2, 2), sTemp.Trim.Substring(0, 2))
                            _Invoice.InvoiceDate = _date
                        Catch ex As Exception
                            ErrorMessage.Append("Invalid Invoice Date" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 3
                        If sTemp.Trim <> String.Empty Then
                            _Invoice.InvoiceType = sTemp.Trim
                        Else
                            ErrorMessage.Append("Invalid Invoice Type" & Chr(13) & Chr(10))
                        End If
                    Case Is = 4
                        If sTemp.Trim <> String.Empty Then
                            _Invoice.Amount = CDec(sTemp.Trim)
                        Else
                            ErrorMessage.Append("Invalid Invoice Amount" & Chr(13) & Chr(10))
                        End If
                    Case Is = 5
                        If sTemp.Trim <> String.Empty Then
                            objSalesOrder = New SalesOrderFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))).Retrieve(sTemp.Trim)
                            If objSalesOrder.ID > 0 Then
                                _Invoice.SalesOrder = objSalesOrder
                            Else
                                ErrorMessage.Append("Invalid SO Number" & Chr(13) & Chr(10))
                            End If
                        Else
                            ErrorMessage.Append("Invalid SO Number" & Chr(13) & Chr(10))
                        End If
                    Case Is = 6
                        If sTemp.Trim <> String.Empty Then
                            _Invoice.InvoiceRef = sTemp.Trim
                        End If
                    Case Is = 7
                        If sTemp.Trim <> String.Empty Then
                            _Invoice.InvoiceKind = 0
                            Select Case sTemp.Trim.ToUpper()

                                Case "AC"
                                    _Invoice.InvoiceKind = enumInvoice.InvoiceKind.AC
                                Case "VH"
                                    _Invoice.InvoiceKind = enumInvoice.InvoiceKind.VH
                                Case "DP"
                                    _Invoice.InvoiceKind = enumInvoice.InvoiceKind.DP
                                Case "LC"
                                    _Invoice.InvoiceKind = enumInvoice.InvoiceKind.LC
                                Case Else
                                    ErrorMessage.Append("Invalid Invoice Kind" & Chr(13) & Chr(10))

                            End Select
                        Else
                            ErrorMessage.Append("Invalid Invoice Kind" & Chr(13) & Chr(10))

                        End If
                    Case Else
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next

            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            End If

            If Not IsNothing(_Invoice) Then
                _ListInvoices.Add(_Invoice)
                ConvertToLogisticDN(_Invoice)
            End If

        End Sub


        Private Sub ConvertToLogisticDN(ByVal oInv As Invoice)
            Dim oLDN As LogisticDN
            Dim oLDCH As LogisticDCHeader
            Dim oLDCHFac As New LogisticDCHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

            If oInv.InvoiceType.Trim().ToUpper() = _LogisticCostDocType Then
                _ListLogisticDNs.Add(oLDN)
            Else
                oLDN = New LogisticDN
                oLDCH = oLDCHFac.Retrieve(oInv.InvoiceRef)

                oLDN.DebitMemoNo = oInv.InvoiceNumber
                oLDN.BillingDate = oInv.InvoiceDate
                oLDN.DocumentType = oInv.InvoiceType
                oLDN.TotalAmount = oInv.Amount
                oLDN.SalesOrder = oInv.SalesOrder
                oLDN.LogisticDCHeader = oLDCH

                _ListLogisticDNs.Add(oLDN)
            End If

        End Sub
#End Region

    End Class
End Namespace
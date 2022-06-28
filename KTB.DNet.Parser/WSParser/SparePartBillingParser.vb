#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNET.Domain
Imports KTB.DNET.BusinessFacade.General
Imports KTB.DNET.BusinessFacade.SparePart
Imports KTB.DNET.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.Parser

    Public Class SparePartBillingParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _aBillingHs As ArrayList
        Private _oBillingH As SparePartBilling
        Private _oBillingD As SparePartBillingDetail
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

                _aBillingHs = New ArrayList()
                _oBillingH = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0).Trim
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_oBillingH) Then
                                _aBillingHs.Add(_oBillingH)
                                _oBillingH = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _oBillingH = ParseHeader(line)
                        ElseIf ind = MyBase.IndicatorDetail Then
                            If IsNothing(_oBillingH) OrElse Not IsNothing(_oBillingH.ErrorMessage) Then
                            Else
                                _oBillingD = ParseDetail(line)

                                If Not IsNothing(_oBillingD) Then
                                    _oBillingD.SparePartBilling = _oBillingH
                                    _oBillingH.SparePartBillingDetails.Add(_oBillingD)
                                    If Not IsNothing(_oBillingD.ErrorMessage) AndAlso _oBillingD.ErrorMessage.Trim <> String.Empty Then
                                        _oBillingH.ErrorMessage = _oBillingH.ErrorMessage & ";" & _oBillingD.ErrorMessage
                                    End If
                                Else
                                    ''remarks, gak ada DO tetep masuk
                                    'If Not IsNothing(_oBillingD.ErrorMessage) Then
                                    '    _oBillingH.ErrorMessage = _oBillingH.ErrorMessage & ";" & "there are DO not found"
                                    'End If
                                    'Exit For ' gak ketemua DO langsung keluar
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "SparePartBillingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartBillingParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _oBillingH = Nothing
                    End Try
                Next

                If Not IsNothing(_oBillingH) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _oBillingH.ErrorMessage = errorMessage.ToString()
                    _aBillingHs.Add(_oBillingH)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Dim aDatas As New ArrayList
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For i As Integer = 0 To _aBillingHs.Count - 1
                _oBillingH = CType(_aBillingHs(i), SparePartBilling)
                If Not IsNothing(_oBillingH.ErrorMessage) AndAlso _oBillingH.ErrorMessage.Trim() <> String.Empty Then
                    If nError = 0 Then
                        SysLogParameter.LogErrorToSyslog("Start : Log Error of SparePartBillingParser", "ws-worker", "SparePartBillingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartBillingParser, BlockName)
                    End If
                    sMsg = sMsg & _oBillingH.ErrorMessage.Trim() & ";"
                    nError += 1
                    SysLogParameter.LogErrorToSyslog(_oBillingH.ErrorMessage, "ws-worker", "SparePartBillingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartBillingParser, BlockName)
                    'Else
                    '    aDatas.Add(_oBillingH)
                End If
                aDatas.Add(_oBillingH)
            Next
            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Number of Error " & nError.ToString() & " of " & _aBillingHs.Count.ToString() & " Data", "ws-worker", "SparePartBillingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartBillingParser, BlockName)
                SysLogParameter.LogErrorToSyslog("End : Log Error of SparePartBillingParser", "ws-worker", "SparePartBillingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartBillingParser, BlockName)

                Dim e As Exception = New Exception("Number of Error " & nError.ToString() & " of " & _aBillingHs.Count.ToString() & " Data. Message : " & sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                'Throw e
            End If
            _aBillingHs = New ArrayList()
            _aBillingHs = aDatas

            Return _aBillingHs
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim doFacade As SparePartBillingFacade
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim sparePartPOStatusFacade As SparePartPOStatusFacade = New SparePartPOStatusFacade(user)
            Dim fDocumenCancel As New DocumentCancelFacade(user)

            For Each objSparePartBilling As SparePartBilling In _aBillingHs
                Try
                    'Perubahan CR enhancement DSK 27092021
                    Dim objDocumentCancel As DocumentCancel = fDocumenCancel.Retrieve(objSparePartBilling.BillingNumber, EnumDocumentCancel.DocumentType.BillingPart)
                    If objDocumentCancel.DocNumber = "" Then
                        'End Perubahan CR enhancement DSK 27092021
                        If Not IsNothing(objSparePartBilling.ErrorMessage) AndAlso objSparePartBilling.ErrorMessage <> "" Then
                            nError += 1
                            sMsg = sMsg & objSparePartBilling.ErrorMessage.ToString() & ";"
                        Else
                            'check if spare part po status exist
                            Dim sparePartPOStatus As SparePartPOStatus
                            Dim sparePartPOStatusCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            sparePartPOStatusCriteria.opAnd(New Criteria(GetType(SparePartPOStatus), "BillingNumber", MatchType.Exact, objSparePartBilling.BillingNumber))
                            Dim sparePartPOStatusList As ArrayList = sparePartPOStatusFacade.RetrieveList(sparePartPOStatusCriteria)

                            If sparePartPOStatusList.Count = 1 Then
                                sparePartPOStatus = sparePartPOStatusList(0)
                                'check if block status exist
                                Dim topBlockStatus As TOPBlockStatus = New TOPBlockStatus
                                Dim topBlockStatusFacade As TOPBlockStatusFacade = New TOPBlockStatusFacade(user)
                                Dim topBlockStatusCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPBlockStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                topBlockStatusCriteria.opAnd(New Criteria(GetType(TOPBlockStatus), "SparePartPOStatus.ID", MatchType.Exact, sparePartPOStatus.ID))
                                Dim topBlockStatusList As ArrayList = topBlockStatusFacade.Retrieve(topBlockStatusCriteria)
                                'block status exist
                                If topBlockStatusList.Count = 1 Then
                                    topBlockStatus = topBlockStatusList(0)
                                    topBlockStatus.RowStatus = 0
                                    topBlockStatus.Status = 1
                                    topBlockStatus.LastUpdateBy = user.Identity.Name
                                    topBlockStatus.LastUpdateTime = DateTime.Now
                                    topBlockStatusFacade.Update(topBlockStatus)
                                Else
                                    'create new block status
                                    topBlockStatus.MarkLoaded()
                                    sparePartPOStatus.MarkLoaded()
                                    topBlockStatus.SparePartPOStatus = sparePartPOStatus
                                    topBlockStatus.RowStatus = 0
                                    topBlockStatus.Status = 1
                                    topBlockStatus.CreatedBy = user.Identity.Name
                                    topBlockStatus.CreatedTime = DateTime.Now
                                    topBlockStatusFacade.Insert(topBlockStatus)
                                End If
                            End If
                            doFacade = New SparePartBillingFacade(user)
                            'doFacade.InsertFromWebSevice_Old(objSparePartBilling)
                            doFacade.InsertFromWebSevice(objSparePartBilling)
                        End If
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "SparePartBillingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objSparePartBilling.ID.ToString & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _aBillingHs.Count.ToString(), "ws-worker", "SparePartBillingParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "SparePartBillingParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As SparePartBilling
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objSparePartBilling As New SparePartBilling
            Dim objSparePartBillingFac As New SparePartBillingFacade(user)


            errorMessage = New StringBuilder()
            If cols.Length <> 7 Then
                writeError("Invalid Spare Part Billing Header Format")
            Else
                'BillingNumber
                If cols(1).Trim = String.Empty Then
                    writeError("Billing Number can't be empty")
                Else
                    objSparePartBilling.BillingNumber = cols(1).Trim
                End If

                'DealerCode
                If cols(2).Trim = String.Empty Then
                    writeError("Dealer code can't be empty")
                Else
                    Try
                        Dim dealerCode As String = cols(2).Trim
                        Dim objDealerFacade As DealerFacade = New DealerFacade(user)
                        Dim objDealer As Dealer = objDealerFacade.Retrieve(dealerCode)
                        If Not IsNothing(objDealer) AndAlso objDealer.ID > 0 Then
                            objSparePartBilling.Dealer = objDealer
                        Else
                            writeError("Invalid Dealer Code :" & dealerCode)
                        End If
                    Catch ex As Exception
                        writeError("Error :  Dealer Code :" & cols(2).Trim)
                    End Try
                End If

                'Billing Date
                If cols(3).Trim <> String.Empty Then
                    objSparePartBilling.BillingDate = MyBase.GetDateShort(cols(3).Trim)
                End If

                'Total Amount
                If cols(4).Trim <> String.Empty Then
                    objSparePartBilling.TotalAmount = MyBase.GetCurrency(cols(4).Trim) 'CType(cols(4).Trim, Decimal)
                End If

                'Tax
                If cols(5).Trim <> String.Empty Then
                    objSparePartBilling.Tax = MyBase.GetCurrency(cols(5).Trim) 'CType(cols(5).Trim, Decimal)
                End If

                'TermOfPayment
                If cols(6).Trim <> String.Empty Then
                    Dim objTermOfPayment As New TermOfPayment
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TermOfPayment), "TermOfPaymentCode", MatchType.Exact, cols(6).Trim))
                    Dim arlTermOfPayment As ArrayList = New TermOfPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                    If arlTermOfPayment.Count > 0 Then
                        objTermOfPayment = CType(arlTermOfPayment(0), TermOfPayment)
                    End If
                    objSparePartBilling.TermOfPayment = objTermOfPayment
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    If IsNothing(objSparePartBilling) Then objSparePartBilling = New SparePartBilling()
                    objSparePartBilling.ErrorMessage = errorMessage.ToString()
                Else
                    objSparePartBilling.CreatedBy = "SAP"
                End If
            End If

            Return objSparePartBilling
        End Function

        Private Function ParseDetail(ByVal line As String) As SparePartBillingDetail
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim cols As String() = line.Split(MyBase.ColSeparator)

            _oBillingD = New SparePartBillingDetail

            If (cols.Length <> 12) Then
                errorMessage.Append("Invalid Detail Format" & Chr(13) & Chr(10))
            Else
                'Billing Item No
                If cols(1).Trim <> String.Empty Then
                    _oBillingD.BillingItemNo = MyBase.GetNumber(cols(1).Trim)
                End If

                'DO Number
                Dim DONumber As String = cols(2).Trim
                Dim itemDONumber As String = MyBase.GetNumber(cols(3).Trim)

                If DONumber = String.Empty Then
                    writeError("DO Item Number Can't Be Empty")
                Else
                    Try
                        Dim objSparePartDO As SparePartDO = RetrieveSparePartDO(DONumber)
                        If Not IsNothing(objSparePartDO) Then
                            'If MyBase.GetNumber(cols(8).Trim) > 0 Then 'Cek Total Price > 0
                            Dim objSparePartDODetail As SparePartDODetail = Me.RetrieveSparePartDODetail(objSparePartDO, itemDONumber)
                            If Not IsNothing(objSparePartDODetail) AndAlso objSparePartDODetail.ID > 0 Then
                                _oBillingD.SparePartDODetail = objSparePartDODetail
                            Else
                                'writeError("There is no DO Number")
                                Return Nothing
                            End If
                            'End If
                        End If
                    Catch ex As Exception
                        writeError("Error : DO Number :" & DONumber)
                    End Try
                End If

                'Quantity
                Try
                    If cols(6).Trim <> String.Empty Then
                        _oBillingD.Quantity = MyBase.GetNumber(cols(6).Trim)
                    End If
                Catch ex As Exception
                    writeError("Invalid Quantity")
                End Try

                'ItemPrice
                Try
                    If cols(7).Trim <> String.Empty Then
                        _oBillingD.ItemPrice = MyBase.GetCurrency(cols(7).Trim)
                    End If
                Catch ex As Exception
                    writeError("Invalid Item Price")
                End Try

                'ItemPrice
                Try
                    If cols(8).Trim <> String.Empty Then
                        _oBillingD.TotalPrice = MyBase.GetCurrency(cols(8).Trim)
                    End If
                Catch ex As Exception
                    writeError("Invalid Total Price")
                End Try

                'Tax
                Try
                    If cols(9).Trim <> String.Empty Then
                        _oBillingD.Tax = MyBase.GetCurrency(cols(9).Trim)
                    End If
                Catch ex As Exception
                    writeError("Invalid Tax")
                End Try

                'Retail Price
                Try
                    If cols(10).Trim <> String.Empty Then
                        _oBillingD.RetailPrice = MyBase.GetCurrency(cols(10).Trim)
                    End If
                Catch ex As Exception
                    writeError("Invalid Retail Price")
                End Try

                'Discount
                Try
                    If cols(11).Trim <> String.Empty Then
                        _oBillingD.Discount = MyBase.GetCurrency(cols(11).Trim)
                    End If
                Catch ex As Exception
                    writeError("Invalid Discount")
                End Try
            End If

            If Not IsNothing(errorMessage) Then
                _oBillingD.ErrorMessage = errorMessage.ToString()
            End If
            Return _oBillingD
        End Function


        Private Function RetrieveSparePartDODetail(ByVal _SparePartDO As SparePartDO, ByVal DOItemNo As Integer) As SparePartDODetail
            Try
                Dim objSparePartDODetail As SparePartDODetail
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartDODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartDODetail), "SparePartDO.ID", MatchType.Exact, _SparePartDO.ID))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartDODetail), "ItemNoDO", MatchType.Exact, DOItemNo))
                Dim arlSparePartDODetail As ArrayList = New SparePartDODetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                If arlSparePartDODetail.Count > 0 Then
                    objSparePartDODetail = CType(arlSparePartDODetail(0), SparePartDODetail)
                End If
                Return objSparePartDODetail
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Private Function RetrieveSparePartDO(ByVal _DONumber As String) As SparePartDO
            Try
                Dim objSparePartDO As SparePartDO
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartDO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartDO), "DONumber", MatchType.Exact, _DONumber))
                Dim arlSparePartDO As ArrayList = New SparePartDOFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                If arlSparePartDO.Count > 0 Then
                    objSparePartDO = CType(arlSparePartDO(0), SparePartDO)
                End If
                Return objSparePartDO
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

#End Region

    End Class
End Namespace

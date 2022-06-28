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
Imports KTB.DNet.BusinessFacade.Claim
Imports KTB.DNet.Domain.Search
Imports KTB.DNET.BusinessFacade.SparePart
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class BillingReturParser
        Inherits AbstractParser
#Region "Private Variables"
         Private stream As StreamReader
        Private grammar As Regex
        Private ClaimHeaders As ArrayList
        Private ClaimDetails As ArrayList
        Private _fileName As String
        Private _ClaimHeader As ClaimHeader  'Header
        Private _ClaimDetail As ClaimDetail
        Private errorMessage As StringBuilder

        Private arrClaimSPBillingRetur As ArrayList
        Private ArrClaimDebitNote As ArrayList
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Try
                _fileName = fileName
                Dim val As String
                ClaimHeaders = New ArrayList
                Dim claimSPBillRet As ClaimSPBillingRetur = New ClaimSPBillingRetur
                Dim claimSPBillRetDetail As ClaimSPBillingReturDetail
                Dim claimDebitNote As ClaimDebitNote
                arrClaimSPBillingRetur = New ArrayList
                ArrClaimDebitNote = New ArrayList
                stream = New StreamReader(fileName, True)
                val = MyBase.NextLine(stream).Trim()
                While (Not val = "")
                    Try
                        If val.Substring(0, 1).ToUpper.Equals("H") Then
                            If Not _ClaimHeader Is Nothing Then
                                ClaimHeaders.Add(_ClaimHeader)  'customer header input text
                            End If

                            If claimSPBillRet.BillingReturNumber <> "" Then
                                If Not _ClaimHeader Is Nothing Then
                                    claimSPBillRet.ClaimHeader = New ClaimHeader(_ClaimHeader.ID)
                                End If

                                Dim tempClaim As ClaimSPBillingRetur = claimSPBillRet
                                arrClaimSPBillingRetur.Add(tempClaim)
                            End If

                            _ClaimHeader = ParseClaimHeader(val + Delimited)
                            claimSPBillRet = ParseClaimSPBillingRetur(val + Delimited)
                            claimDebitNote = ParseClaimDebitNote(val + Delimited)
                            If Not claimDebitNote Is Nothing Then
                                Dim tempClaimDebitNote As ClaimDebitNote = claimDebitNote
                                'ArrClaimDebitNote.Add(tempClaimDebitNote)
                                claimSPBillRet.ClaimDebitNote = tempClaimDebitNote
                            End If

                        Else
                            If Not _ClaimHeader Is Nothing Then
                                ParseClaimDetail(val + Delimited, _ClaimHeader)  'Order detail input
                            End If
                            Dim tempclaimSPBillRetDetail = ParseClaimSPBillingReturDetail(val + Delimited, claimSPBillRet)

                            If claimSPBillRet.BillingReturNumber <> "" Then
                                claimSPBillRet.ClaimSPBillingReturDetail.Add(tempclaimSPBillRetDetail)
                            End If
                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "BillingReturParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.BillingReturParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _ClaimHeader = Nothing
                    End Try
                    val = MyBase.NextLine(stream)
                End While

                If claimSPBillRet.BillingReturNumber <> "" Then
                    If Not _ClaimHeader Is Nothing Then
                        claimSPBillRet.ClaimHeader = New ClaimHeader(_ClaimHeader.ID)
                    End If

                    Dim tempClaim As ClaimSPBillingRetur = claimSPBillRet
                    arrClaimSPBillingRetur.Add(tempClaim)
                End If

                If Not _ClaimHeader Is Nothing Then
                    ClaimHeaders.Add(_ClaimHeader)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                stream.Close()
                stream = Nothing
            End Try
            Return ClaimHeaders
        End Function

        'Protected Overrides Function DoTransaction() As Integer
        '    Dim _objClaimHeader As ClaimHeader
        '    Try
        '        Dim result As Integer = New ClaimHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).UpdateClaimHeader(ClaimHeaders)
        '    Catch ex As Exception
        '        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "BillingReturParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.BillingReturParser, BlockName)
        '        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & _objClaimHeader.ID & Chr(13) & Chr(10) & ex.Message)
        '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
        '    End Try
        'End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim _objClaimHeader As ClaimHeader
            For Each item As ClaimHeader In ClaimHeaders
                Try
                    Dim _claimHeaderFacade As ClaimHeaderFacade = New ClaimHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    Dim objFromDB As ClaimHeader = _claimHeaderFacade.Retrieve(item.ID)
                    Dim objHistory As ClaimStatusHistory
                    If Not objFromDB Is Nothing Then
                        If item.StatusKTB <> objFromDB.StatusKTB Then
                            objHistory = New ClaimStatusHistory
                            objHistory.ClaimHeader = item
                            objHistory.NewStatus = item.StatusKTB
                            objHistory.Status = objFromDB.StatusKTB
                            objHistory.OldProgress = objFromDB.ClaimProgress.ID
                            objHistory.Progress = item.ClaimProgress.ID
                        End If
                        Dim result As Integer = _claimHeaderFacade.UpdateClaimHeader(item, objHistory)
                    Else
                        errorMessage.Append("Claim is not found" & Chr(13) & Chr(10))
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "BillingReturParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.BillingReturParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & _objClaimHeader.ID & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            'Insert ClaimSPBillingRetur, ClaimSPBillingReturDetail, & ClaimDebitNote
            processClaimSP()
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Function ParseClaimHeader(ByVal ValParser As String) As ClaimHeader
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            Dim _dealer As New Dealer
            Dim _Category As New Category
            errorMessage = New StringBuilder
            _ClaimHeader = Nothing
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 1
                        If sTemp.Trim.Length > 0 Then
                            Try
                                Dim idStr As String = sTemp.Trim
                                If IsNumeric(idStr) Then
                                    _ClaimHeader = New ClaimHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(CInt(idStr))
                                Else
                                    errorMessage.Append("Invalid Claim No" & Chr(13) & Chr(10))
                                    Return Nothing
                                End If
                            Catch ex As Exception
                                errorMessage.Append("Claim No not found" & Chr(13) & Chr(10))
                                Return Nothing
                                Exit Select
                            End Try

                        Else
                            errorMessage.Append("Claim No is Empty" & Chr(13) & Chr(10))
                            Return Nothing
                        End If
                    Case Is = 2
                        _ClaimHeader.FakturRetur = sTemp
                    Case Is = 3
                        Try
                            'yyyymmdd  20040521'
                            Dim objDate As Date = New Date(sTemp.Substring(0, 4), sTemp.Substring(4, 2), sTemp.Substring(6, 2), 0, 0, 0)
                            _ClaimHeader.FakturReturDate = objDate
                        Catch ex As Exception
                            errorMessage.Append("Faktur Retur Date tidak valid." & Chr(13) & Chr(10))
                        End Try
                    Case Is = 4
                        _ClaimHeader.DONumber = sTemp.Trim
                    Case Is = 5
                        Try
                            'yyyymmdd  20040521'
                            Dim objDate As Date = New Date(sTemp.Substring(0, 4), sTemp.Substring(4, 2), sTemp.Substring(6, 2), 0, 0, 0)
                            _ClaimHeader.DeliveryDate = objDate
                        Catch ex As Exception
                            errorMessage.Append("DO Retur Date tidak valid." & Chr(13) & Chr(10))
                        End Try
                    Case Is = 6
                        _ClaimHeader.SORetur = sTemp
                    Case Is = 7
                        Try
                            'yyyymmdd  20040521'
                            Dim objDate As Date = New Date(sTemp.Substring(0, 4), sTemp.Substring(4, 2), sTemp.Substring(6, 2), 0, 0, 0)
                            _ClaimHeader.SOReturDate = objDate
                        Catch ex As Exception
                            errorMessage.Append("SO Retur Date tidak valid." & Chr(13) & Chr(10))
                        End Try
                    Case Else
                        'do nothing
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If _ClaimHeader.FakturRetur <> "" And _ClaimHeader.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Selesai Then
                _ClaimHeader.Status = EnumClaimStatus.ClaimStatus.Complete_Selesai
                _ClaimHeader.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Complete_Selesai
            End If

            If errorMessage.Length > 0 Then
                _ClaimHeader = Nothing 'Add By Dna on 20130201 For (error in textfile:condition before the update-> row-1 valid, row-2 invalid, then row-1 will be update by row-2)
                Throw New Exception(errorMessage.ToString)
            End If
            Return _ClaimHeader
        End Function

        Private Sub ParseClaimDetail(ByVal ValParser As String, ByVal _objClaimHeader As ClaimHeader)
            _ClaimDetail = New ClaimDetail
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            errorMessage = New StringBuilder
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 1
                        Try
                            Dim criteriapo As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criteriapo.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartMaster.PartNumber", MatchType.Exact, sTemp.Trim))
                            criteriapo.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartPOStatus.ID", MatchType.Exact, _objClaimHeader.SparePartPOStatus.ID))
                            Dim objSparepartPODetail As SparePartPOStatusDetail
                            Dim poList As ArrayList = New SparePartPOStatusDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveList(criteriapo)
                            If poList.Count > 0 Then
                                objSparepartPODetail = poList(0)
                            Else
                                errorMessage.Append("Invalid Claim Detail - Mat Num " & Chr(13) & Chr(10))
                            End If
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "SparePartPOStatusDetail.ID", MatchType.Exact, objSparepartPODetail.ID))
                            Dim listClaimDetail As ArrayList = New ClaimDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If listClaimDetail.Count > 0 Then
                                _ClaimDetail = CType(listClaimDetail(0), ClaimDetail)
                            Else
                                errorMessage.Append("Invalid Claim Detail - Mat Num " & Chr(13) & Chr(10))
                            End If
                        Catch ex As Exception
                            errorMessage.Append("Invalid Line Item" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 2
                        Dim qtyStr As String = sTemp.ToString
                        If qtyStr.Trim = String.Empty Then
                            _ClaimDetail.ApprovedQty = 0
                        Else
                            If IsNumeric(qtyStr) Then
                                _ClaimDetail.ApprovedQty = CInt(qtyStr)
                            Else
                                errorMessage.Append("Invalid Quantity" & Chr(13) & Chr(10))
                            End If
                        End If
                    Case Else
                        'Do Nothing else
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next
            If errorMessage.Length > 0 Then
                Throw New Exception(errorMessage.ToString)
            End If
            _objClaimHeader.ClaimDetails.Add(_ClaimDetail)
        End Sub

        Private Function getSPBilling(ByVal billNo As String) As SparePartBilling
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartBilling), "BillingNumber", MatchType.Exact, billNo))
            crit.opAnd(New Criteria(GetType(SparePartBilling), "RowStatus", MatchType.Exact, 0))
            Dim arrSparepartBill As ArrayList = New SparePartBillingFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(crit)
            If Not IsNothing(arrSparepartBill) AndAlso arrSparepartBill.Count > 0 Then
                Return CType(arrSparepartBill(0), SparePartBilling)
            Else
                Return Nothing
            End If
        End Function

        Private Function getSPDODetailID(ByVal partNumber As String, ByVal itemNo As Integer, ByVal billingNumberRef As String) As SparePartDODetail
            Dim objFacade As ClaimSPBillingReturDetailFacade = New ClaimSPBillingReturDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim spStr As String = "sp_ClaimSPBillingReturDetail_gteSPDODetailID @BillingNumberRef='" & billingNumberRef & "', @ItemNo=" & itemNo & ", @PartNumber='" & partNumber & "'"
            Dim idDS As DataSet = objFacade.RetrieveSp(spStr)
            If idDS.Tables.Count > 0 Then
                Dim idTbl As DataTable = idDS.Tables(0)
                If idTbl.Rows.Count > 0 Then
                    Try
                        Dim id = CInt(idTbl.Rows(0)("ID"))
                        If id > 0 Then
                            'Dim user As System.Security.Principal.GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
                            'Return New SparePartDODetailFacade(user).Retrieve(id)
                            Return New SparePartDODetail(id)
                        Else
                            Return Nothing
                        End If
                    Catch ex As Exception
                        Return Nothing
                    End Try
                End If
            End If

            Return Nothing
        End Function

        Private Function ParseClaimSPBillingRetur(ByVal ValParser As String) As ClaimSPBillingRetur
            Dim claimSPBillRet As ClaimSPBillingRetur = New ClaimSPBillingRetur
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim isClaimID As Boolean = False
            Dim isBillRetNumber As Boolean = False
            sStart = 0
            nCount = 0
            Dim _dealer As New Dealer
            Dim _Category As New Category
            errorMessage = New StringBuilder
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 1
                        If sTemp.Trim.Length > 0 Then
                            Try
                                Dim idStr As String = sTemp.Trim
                                If IsNumeric(idStr) Then
                                    claimSPBillRet.ClaimHeader = New ClaimHeader(CInt(idStr))
                                    isClaimID = True
                                Else
                                    errorMessage.Append("Invalid Claim No" & Chr(13) & Chr(10))
                                End If
                            Catch ex As Exception
                                errorMessage.Append("Claim No not found" & Chr(13) & Chr(10))
                                Exit Select
                            End Try

                        Else
                            isClaimID = False
                        End If
                    Case Is = 2
                        If sTemp <> "" Then
                            claimSPBillRet.BillingReturNumber = sTemp
                            isBillRetNumber = True
                        End If
                    Case Is = 3
                        If isBillRetNumber Then
                            Try
                                'yyyymmdd  20040521'
                                Dim objDate As Date = New Date(sTemp.Substring(0, 4), sTemp.Substring(4, 2), sTemp.Substring(6, 2), 0, 0, 0)
                                claimSPBillRet.BillingReturDate = objDate
                            Catch ex As Exception
                                errorMessage.Append("DO Retur Date tidak valid." & Chr(13) & Chr(10))
                            End Try
                        End If
                    Case Is = 4
                        If isBillRetNumber Then
                            claimSPBillRet.DORetur = sTemp
                        End If
                    Case Is = 5
                        If isBillRetNumber Then
                            Try
                                'yyyymmdd  20040521'
                                Dim objDate As Date = New Date(sTemp.Substring(0, 4), sTemp.Substring(4, 2), sTemp.Substring(6, 2), 0, 0, 0)
                                claimSPBillRet.DOReturDate = objDate
                            Catch ex As Exception
                                errorMessage.Append("DO Retur Date tidak valid." & Chr(13) & Chr(10))
                            End Try
                        End If
                    Case Is = 6
                        If isBillRetNumber Then
                            claimSPBillRet.SORetur = sTemp
                        End If
                    Case Is = 7
                        If isBillRetNumber Then
                            Try
                                'yyyymmdd  20040521'
                                Dim objDate As Date = New Date(sTemp.Substring(0, 4), sTemp.Substring(4, 2), sTemp.Substring(6, 2), 0, 0, 0)
                                claimSPBillRet.SOReturDate = objDate
                            Catch ex As Exception
                                errorMessage.Append("SO Retur Date tidak valid." & Chr(13) & Chr(10))
                            End Try
                        End If
                    Case Is = 8
                        If isBillRetNumber Then
                            Dim tempSPBil As SparePartBilling = getSPBilling(sTemp)
                            If Not IsNothing(tempSPBil) Then
                                claimSPBillRet.SparePartBilling = tempSPBil
                            Else
                                claimSPBillRet.BillingReturNumber = ""
                                Return claimSPBillRet
                            End If
                        End If
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If errorMessage.Length > 0 Then
                Throw New Exception(errorMessage.ToString)
            End If
            Return claimSPBillRet
        End Function

        Private Function ParseClaimDebitNote(ByVal ValParser As String) As ClaimDebitNote
            Dim ClaimDebitNote As ClaimDebitNote = New ClaimDebitNote
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim isDebitNoteNumber As Boolean = False
            sStart = 0
            nCount = 0
            Dim _dealer As New Dealer
            Dim _Category As New Category
            errorMessage = New StringBuilder
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 9
                        If sTemp <> "" Then
                            ClaimDebitNote.DebitNoteNumber = sTemp
                            isDebitNoteNumber = True
                        End If
                    Case Is = 10
                        If isDebitNoteNumber Then
                            Try
                                'yyyymmdd  20040521'
                                Dim objDate As Date = New Date(sTemp.Substring(0, 4), sTemp.Substring(4, 2), sTemp.Substring(6, 2), 0, 0, 0)
                                ClaimDebitNote.DebitNoteDate = objDate
                            Catch ex As Exception
                                errorMessage.Append("SO Retur Date tidak valid." & Chr(13) & Chr(10))
                            End Try
                        End If
                    Case Is = 11
                        If isDebitNoteNumber Then
                            ClaimDebitNote.TotalAmount = sTemp
                        End If
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If errorMessage.Length > 0 Then
                Throw New Exception(errorMessage.ToString)
            End If

            If isDebitNoteNumber Then
                Return ClaimDebitNote
            Else
                Return Nothing
            End If
        End Function

        Private Function ParseClaimSPBillingReturDetail(ByVal ValParser As String, ByVal claimSPBillRet As ClaimSPBillingRetur) As ClaimSPBillingReturDetail
            Dim claimSPBillRetDetail = New ClaimSPBillingReturDetail
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim partNumber As String = ""
            sStart = 0
            nCount = 0
            errorMessage = New StringBuilder
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 1
                        partNumber = sTemp
                    Case Is = 2
                        claimSPBillRetDetail.Qty = CInt(sTemp)
                    Case Is = 3
                        If partNumber <> "" AndAlso Not IsNothing(claimSPBillRet.SparePartBilling) Then
                            Dim spDODetail As SparePartDODetail = getSPDODetailID(partNumber, CInt(sTemp.Trim), claimSPBillRet.SparePartBilling.BillingNumber)
                            If Not spDODetail Is Nothing Then
                                claimSPBillRetDetail.SparePartDODetail = spDODetail
                            Else
                                errorMessage.Append("Can't retrieve SparePartDODetail" & Chr(13) & Chr(10))
                            End If
                        End If
                    Case Is = 4
                        claimSPBillRetDetail.Amount = sTemp
                    Case Is = 5
                        claimSPBillRetDetail.Tax = sTemp
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next
            If errorMessage.Length > 0 Then
                Throw New Exception(errorMessage.ToString)
            End If
            Return claimSPBillRetDetail
        End Function

        Private Sub processClaimSP()
            Dim _objClaimHeader As ClaimHeader
            Dim insertCount As Integer = 0
            Dim user As System.Security.Principal.GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            For i As Integer = 0 To arrClaimSPBillingRetur.Count - 1
                Dim claim As ClaimSPBillingRetur = CType(arrClaimSPBillingRetur(i), ClaimSPBillingRetur)
                'Dim claimDN As ClaimDebitNote = CType(ArrClaimDebitNote(i), ClaimDebitNote)
                Try
                    'precess claimSPBillingRetur
                    Dim claimSPBillRetId = processClaimSPBillingRetur(claim)
                    If claimSPBillRetId > 0 Then
                        'process claimSPBillingReturDetail
                        For Each claimDetail As ClaimSPBillingReturDetail In claim.ClaimSPBillingReturDetail
                            'claimDetail.ClaimSPBillingRetur = New ClaimSPBillingRetur(claimSPBillRetId)
                            Dim res = processClaimSPBillingReturDetail(claimDetail, claimSPBillRetId)
                            If Not res > 0 Then
                                Throw New Exception("Gagal insert ClaimSPBillingReturDetail")
                            End If
                        Next
                    Else
                        Throw New Exception("Gagal insert ClaimSPBillingRetur")
                    End If

                    If Not claim.ClaimDebitNote Is Nothing Then
                        Dim ClaimDNRes = processClaimDebitNote(claim.ClaimDebitNote, claimSPBillRetId)
                        If Not ClaimDNRes > 0 Then
                            Throw New Exception("Gagal insert ClaimDebitNote")
                        End If
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "BillingReturParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.BillingReturParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & _objClaimHeader.ID & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
        End Sub

        Private Function processClaimSPBillingRetur(ByVal claim As ClaimSPBillingRetur) As Integer
            Dim result As Integer = 0
            Dim returnID As Integer = 0
            Dim user As System.Security.Principal.GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim criteria As CriteriaComposite
            Dim arrClaimSPBillRet As ArrayList
            If Not claim.ClaimHeader Is Nothing Then
                criteria = New CriteriaComposite(New Criteria(GetType(ClaimSPBillingRetur), "RowStatus", MatchType.Exact, 0))
                criteria.opAnd(New Criteria(GetType(ClaimSPBillingRetur), "ClaimHeader", MatchType.Exact, claim.ClaimHeader.ID))
                criteria.opAnd(New Criteria(GetType(ClaimSPBillingRetur), "BillingReturNumber", MatchType.Exact, claim.BillingReturNumber))
                arrClaimSPBillRet = New ClaimSPBillingReturFacade(user).Retrieve(criteria)
            Else
                criteria = New CriteriaComposite(New Criteria(GetType(ClaimSPBillingRetur), "RowStatus", MatchType.Exact, 0))
                criteria.opAnd(New Criteria(GetType(ClaimSPBillingRetur), "ClaimHeader", MatchType.IsNull, True))
                criteria.opAnd(New Criteria(GetType(ClaimSPBillingRetur), "SparePartBilling", MatchType.Exact, claim.SparePartBilling.ID))
                criteria.opAnd(New Criteria(GetType(ClaimSPBillingRetur), "BillingReturNumber", MatchType.Exact, claim.BillingReturNumber))
                arrClaimSPBillRet = New ClaimSPBillingReturFacade(user).Retrieve(criteria)
            End If

            If Not arrClaimSPBillRet Is Nothing And arrClaimSPBillRet.Count > 0 Then
                Dim lastObj As ClaimSPBillingRetur = CType(arrClaimSPBillRet(0), ClaimSPBillingRetur)
                returnID = lastObj.ID
                claim.CreatedBy = lastObj.CreatedBy
                claim.CreatedTime = lastObj.CreatedTime
                claim.ID = returnID
                result = New ClaimSPBillingReturFacade(user).Update(claim)
            Else
                result = New ClaimSPBillingReturFacade(user).Insert(claim)
                returnID = result
            End If

            If result > 0 Then
                Return returnID
            End If
            Return result
        End Function

        Private Function processClaimSPBillingReturDetail(ByVal claim As ClaimSPBillingReturDetail, ByVal spBillingReturID As Integer) As Integer
            Dim user As System.Security.Principal.GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim claimFacade As ClaimSPBillingReturDetailFacade = New ClaimSPBillingReturDetailFacade(user)
            Dim criteria = New CriteriaComposite(New Criteria(GetType(ClaimSPBillingReturDetail), "RowStatus", MatchType.Exact, 0))
            criteria.opAnd(New Criteria(GetType(ClaimSPBillingReturDetail), "ClaimSPBillingRetur", MatchType.Exact, spBillingReturID))
            criteria.opAnd(New Criteria(GetType(ClaimSPBillingReturDetail), "SparePartDODetail", MatchType.Exact, claim.SparePartDODetail.ID))
            Dim arrClaimSPBillRetDetail As ArrayList = claimFacade.Retrieve(criteria)
            If Not arrClaimSPBillRetDetail Is Nothing And arrClaimSPBillRetDetail.Count > 0 Then
                Dim delClaim As ClaimSPBillingReturDetail = CType(arrClaimSPBillRetDetail(0), ClaimSPBillingReturDetail)
                claimFacade.Delete(delClaim)
            End If

            claim.ClaimSPBillingRetur = New ClaimSPBillingRetur(spBillingReturID)
            Dim result = claimFacade.Insert(claim)

            Return result
        End Function

        Private Function processClaimDebitNote(ByVal claim As ClaimDebitNote, ByVal spBillingReturID As Integer) As Integer
            Dim result As Integer = 0
            Dim user As System.Security.Principal.GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim criteria As CriteriaComposite
            Dim arrClaimDebitNote As ArrayList
            criteria = New CriteriaComposite(New Criteria(GetType(ClaimDebitNote), "RowStatus", MatchType.Exact, 0))
            criteria.opAnd(New Criteria(GetType(ClaimDebitNote), "ClaimSPBillingRetur", MatchType.Exact, spBillingReturID))
            criteria.opAnd(New Criteria(GetType(ClaimDebitNote), "DebitNoteNumber", MatchType.Exact, claim.DebitNoteNumber))
            arrClaimDebitNote = New ClaimDebitNoteFacade(user).Retrieve(criteria)
            If Not arrClaimDebitNote Is Nothing And arrClaimDebitNote.Count > 0 Then
                Dim lastObj As ClaimDebitNote = CType(arrClaimDebitNote(0), ClaimDebitNote)
                Dim id As Integer = lastObj.ID
                claim.ID = id
                claim.ClaimSPBillingRetur = New ClaimSPBillingRetur(spBillingReturID)
                claim.CreatedBy = lastObj.CreatedBy
                claim.CreatedTime = lastObj.CreatedTime
                result = New ClaimDebitNoteFacade(user).Update(claim)
            Else
                claim.ClaimSPBillingRetur = New ClaimSPBillingRetur(spBillingReturID)
                result = New ClaimDebitNoteFacade(user).Insert(claim)
            End If

            Return result
        End Function

#End Region

    End Class

End Namespace
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
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade

#End Region

Namespace KTB.DNet.Parser

    Public Class SparePartDOExpeditionParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _aPackingHs As ArrayList
        Private _oPackingH As SparePartDOExpedition
        Private _oPackingD As SparePartPacking
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

                _aPackingHs = New ArrayList()
                _oPackingH = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_oPackingH) Then
                                _aPackingHs.Add(_oPackingH)
                                _oPackingH = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _oPackingH = ParseHeader(line)

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "SparePartPackingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartPackingParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _oPackingH = Nothing
                    End Try
                Next

                If Not IsNothing(_oPackingH) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _oPackingH.ErrorMessage = errorMessage.ToString()
                    _aPackingHs.Add(_oPackingH)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Dim aDatas As New ArrayList
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For i As Integer = 0 To _aPackingHs.Count - 1
                _oPackingH = CType(_aPackingHs(i), SparePartDOExpedition)
                If Not IsNothing(_oPackingH.ErrorMessage) AndAlso _oPackingH.ErrorMessage.Trim() <> String.Empty Then
                    If nError = 0 Then
                        SysLogParameter.LogErrorToSyslog("Start : Log Error of SparePartPackingParser", "ws-worker", "SparePartPackingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartPackingParser, BlockName)
                    End If
                    sMsg = sMsg & _oPackingH.ErrorMessage.Trim() & ";"
                    nError += 1
                    SysLogParameter.LogErrorToSyslog(_oPackingH.ErrorMessage, "ws-worker", "SparePartPackingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartPackingParser, BlockName)
                    'Else
                    '    aDatas.Add(_oPackingH)
                End If
                aDatas.Add(_oPackingH)
            Next
            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Number of Error " & nError.ToString() & " of " & _aPackingHs.Count.ToString() & " Data", "ws-worker", "SparePartPackingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartPackingParser, BlockName)
                SysLogParameter.LogErrorToSyslog("End : Log Error of SparePartPackingParser", "ws-worker", "SparePartPackingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartPackingParser, BlockName)

                Dim e As Exception = New Exception("Number of Error " & nError.ToString() & " of " & _aPackingHs.Count.ToString() & " Data. Message : " & sMsg)
                'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                'Throw e
            End If
            _aPackingHs = New ArrayList()
            _aPackingHs = aDatas

            Return _aPackingHs
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer

            Dim doFacade As SparePartDOExpeditionFacade
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim IsChange As New IsChangeFacade

            For Each objSparePartDOExpedition As SparePartDOExpedition In _aPackingHs
                Try

                    If Not IsNothing(objSparePartDOExpedition.ErrorMessage) AndAlso objSparePartDOExpedition.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & objSparePartDOExpedition.ErrorMessage.ToString() & ";"
                    Else
                        doFacade = New SparePartDOExpeditionFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        ''doFacade.Insert(objSparePartPacking)
                        Dim iReturn As Integer = 0

                        Try
                            Dim exp As SparePartDOExpedition = doFacade.ValidateExpeditionNumber(objSparePartDOExpedition)
                            If Not IsNothing(exp) AndAlso exp.ID > 0 Then
                                exp.ExpeditionName = objSparePartDOExpedition.ExpeditionName
                                If exp.ATD < objSparePartDOExpedition.ATD Then
                                    exp.ATD = objSparePartDOExpedition.ATD
                                End If

                                If IsChange.ISchangeSparePartDOExpedition(objSparePartDOExpedition, exp) Then
                                    exp.CreatedBy = "SAP"
                                    iReturn = doFacade.Update(exp)
                                End If

                                If iReturn > 0 Or exp.ID > 0 Then
                                    For Each item As SparePartPacking In objSparePartDOExpedition.SparePartPackings
                                        Dim objSparePartPackingFacade As SparePartPackingFacade = New SparePartPackingFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                        Dim objSpPack As SparePartPacking = objSparePartPackingFacade.Retrieve(item.ID)
                                        If Not IsNothing(objSpPack) AndAlso objSpPack.ID > 0 Then

                                            'farid Additional 20180316
                                            item.SparePartDOExpedition = exp
                                            If IsChange.ISchangeSparePartPacking(item, objSpPack) Then
                                                objSpPack.SparePartDOExpedition = exp
                                                Dim iUpdate As Integer = objSparePartPackingFacade.Update(objSpPack)
                                            End If
                                        End If
                                    Next
                                End If

                            Else
                                exp = New SparePartDOExpedition()
                                exp.ExpeditionNo = objSparePartDOExpedition.ExpeditionNo
                                exp.ExpeditionName = objSparePartDOExpedition.ExpeditionName
                                exp.ATD = objSparePartDOExpedition.ATD
                                exp.CreatedBy = "SAP"

                                iReturn = doFacade.Insert(exp)
                                If iReturn > 0 Then
                                    Dim objSparePartDOExpeditionNew As SparePartDOExpedition = doFacade.Retrieve(iReturn)
                                    If Not IsNothing(objSparePartDOExpeditionNew) AndAlso objSparePartDOExpeditionNew.ID > 0 Then
                                        For Each item As SparePartPacking In objSparePartDOExpedition.SparePartPackings
                                            Dim objSparePartPackingFacade As SparePartPackingFacade = New SparePartPackingFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                            Dim objSpPack As SparePartPacking = objSparePartPackingFacade.Retrieve(item.ID)
                                            If Not IsNothing(objSpPack) AndAlso objSpPack.ID > 0 Then

                                                'farid additional 20180316

                                                item.SparePartDOExpedition = objSparePartDOExpeditionNew
                                                If IsChange.ISchangeSparePartPacking(item, objSpPack) Then
                                                    objSpPack.SparePartDOExpedition = objSparePartDOExpeditionNew
                                                    Dim iUpdate As Integer = objSparePartPackingFacade.Update(objSpPack)
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            End If
                            
                            
                        Catch ex As Exception

                        End Try
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "SparePartPackingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objSparePartDOExpedition.ID.ToString & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _aPackingHs.Count.ToString(), "ws-worker", "SparePartPackingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "SparePartPackingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As SparePartDOExpedition
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objSparePartDOExpedition As New SparePartDOExpedition
            Dim objSparePartDOExpeditionFac As New SparePartDOExpeditionFacade(user)
            'Dim objSparePartPackingDetail As SparePartPackingDetail

            errorMessage = New StringBuilder()
            If cols.Length <> 5 Then
                writeError("Invalid Surat Jalan Format")
            Else
                'ExpeditionNumber
                If cols(1).Trim = String.Empty Then
                    writeError("Internal HU Nbr is empty")
                Else
                    Dim internalHuNo As String = cols(1).Trim
                    Dim objSparePartPackingFacade As SparePartPackingFacade = New SparePartPackingFacade(user)
                    Dim objSparePartPacking As SparePartPacking = objSparePartPackingFacade.Retrieve(internalHuNo)
                    If Not IsNothing(objSparePartPacking) AndAlso objSparePartPacking.ID > 0 Then
                        objSparePartDOExpedition.SparePartPackings.Add(objSparePartPacking)
                        'If objSparePartPacking.SparePartPackingDetails.Count > 0 Then
                        '    objSparePartPackingDetail = CType(objSparePartPacking.SparePartPackingDetails(0), SparePartPackingDetail)
                        'End If
                    Else
                        writeError("There is no internal HU Number")
                        Return Nothing
                    End If

                End If


                'ExpeditionNumber
                If cols(2).Trim = String.Empty Then
                    writeError("No. Surat Jalan can't be empty")
                Else
                    objSparePartDOExpedition.ExpeditionNo = cols(2).Trim
                End If

                'Expedition Name
                If cols(3).Trim <> String.Empty Then
                    objSparePartDOExpedition.ExpeditionName = cols(3).Trim
                End If

                'ATD
                If cols(4).Trim <> String.Empty Then
                    objSparePartDOExpedition.ATD = MyBase.GetDateLong(cols(4).Trim)
                    Try
                        'Dim dealerID As Integer
                        'If Not IsNothing(objSparePartPackingDetail) AndAlso objSparePartPackingDetail.ID > 0 Then
                        '    dealerID = objSparePartPackingDetail.SparePartDO.Dealer.ID
                        '    Dim leadTime As Integer = Me.GetLeadTime(dealerID, 1) 'masih harus dicari parameter orderType
                        '    objSparePartDOExpedition.ETD = objSparePartDOExpedition.ATD.AddDays(leadTime)
                        'End If
                    Catch ex As Exception

                    End Try
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    If IsNothing(objSparePartDOExpedition) Then objSparePartDOExpedition = New SparePartDOExpedition()
                    objSparePartDOExpedition.ErrorMessage = errorMessage.ToString()
                Else
                    objSparePartDOExpedition.CreatedBy = "SAP"
                End If
            End If

            Return objSparePartDOExpedition
        End Function

        Private Function GetLeadTime(ByVal objDealerID As Integer, ByVal transType As Integer) As Integer
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim ireturn As Integer = 0
            Dim objDealerLeadTime As DealerLeadTime = New DealerLeadTime()
            Dim objDealerLeadTimeFac As DealerLeadTimeFacade = New DealerLeadTimeFacade(user)
            Select Case transType
                Case 1
                Case 2
            End Select
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerLeadTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerLeadTime), "Dealer.ID", MatchType.Exact, objDealerID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerLeadTime), "TransactionType", MatchType.Exact, transType))
            Dim arlLeadTime As ArrayList = objDealerLeadTimeFac.Retrieve(criterias)
            If arlLeadTime.Count > 0 Then
                objDealerLeadTime = CType(arlLeadTime(0), DealerLeadTime)
                ireturn = objDealerLeadTime.Value
            End If
            Return ireturn

        End Function

        'Private Function ParseDetail(ByVal line As String) As SparePartPacking
        '    Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
        '    Dim cols As String() = line.Split(MyBase.ColSeparator)

        '    _oPackingD = New SparePartPacking

        '    If (cols.Length <> 2) Then
        '        errorMessage.Append("Invalid Detail Format" & Chr(13) & Chr(10))
        '    Else
        '        'DO Number
        '        If cols(1).Trim = String.Empty Then
        '            writeError("DO Number Can't Be Empty")
        '        Else
        '            Dim DONUmber As String = cols(1).Trim
        '            Dim objSPDOFac As SparePartDOFacade = New SparePartDOFacade(user)
        '            Dim objSPDO As SparePartDO = objSPDOFac.Retrieve(DONUmber)
        '            If Not IsNothing(objSPDO) AndAlso objSPDO.ID > 0 Then
        '                _oPackingD.SparePartDOExpedition = objSPDO
        '            Else
        '                writeError("Invalid DO Number")
        '            End If
        '        End If

        '    End If

        '    If Not IsNothing(errorMessage) Then
        '        _oPackingD.ErrorMessage = errorMessage.ToString()
        '    End If
        '    Return _oPackingD
        'End Function

#End Region

    End Class
End Namespace

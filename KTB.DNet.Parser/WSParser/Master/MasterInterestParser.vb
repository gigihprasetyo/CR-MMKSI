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
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade.PK

#End Region

Namespace KTB.DNet.Parser
    Public Class MasterInterestParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        'Private objDealerSalesTarget As DealerSalesTarget
        Private _arrDealerSalesTarget As ArrayList
        Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WS"), Nothing)
#End Region

#Region "Constructors/Destructors/Finalizers"
        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub
#End Region

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String
                Dim nError As Integer = 0
                Dim errMsgSummary As String = String.Empty

                _arrDealerSalesTarget = New ArrayList()
                'objDealerSalesTarget = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind.Trim = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            ' create objek mspmaster
                            'objDealerSalesTarget = ParseHeader(line)
                            ParseHeader(line)
                        End If
                    Catch ex As Exception
                        nError += 1
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "DealerSalesTargetParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPMasterParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _arrDealerSalesTarget = Nothing
                        errMsgSummary = ex.Message & ";"
                    End Try
                Next

                If nError > 0 Then
                    Throw New Exception(errMsgSummary)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrDealerSalesTarget
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim facDealerSalesTarget As New DealerSalesTargetFacade(user)
            For Each objDealerSalesTarget As DealerSalesTarget In _arrDealerSalesTarget
                Try
                    If Not IsNothing(objDealerSalesTarget) Then
                        If objDealerSalesTarget.ErrorMessage = String.Empty Then
                            Dim IDData As Integer = 0
                            If duplicateData(objDealerSalesTarget, IDData) Then
                                objDealerSalesTarget.ID = IDData
                                If facDealerSalesTarget.Update(objDealerSalesTarget) < 0 Then
                                    nError += 1
                                End If
                            Else
                                If facDealerSalesTarget.Insert(objDealerSalesTarget) < 0 Then
                                    nError += 1
                                End If
                            End If
                        Else
                            Throw New Exception(objDealerSalesTarget.ErrorMessage)
                            nError += 1
                        End If

                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrDealerSalesTarget.Count.ToString(), "ws-worker", "MasterInterestParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterInterestParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MasterInterestParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterInterestParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String)
            ' K;MASTERINTEREST_20190319143142\nH;100001;PAJERO;1;15;150;20190319;20191231\n
            ' K;MASTERINTEREST_[TimeStamp]\nH;StringH-1;StringH-2;StringH-3;StringH-4;StringH-5;\n
            ' K;MASTERINTEREST_[TimeStamp]\nH;DealerCode;Model;Sequence;FreeDays;MaxQty;ValidFrom;ValidTo\n
            Dim cols As String() = line.Split(MyBase.ColSeparator)

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crt.opAnd(New Criteria(GetType(VechileModel), "IndDescription", MatchType.Exact, cols(2).Split()(0)))
                Dim arrVehicleModel As ArrayList = New VechileModelFacade(user).Retrieve(crt)
                For Each ve As VechileModel In arrVehicleModel
                    Dim objDealerSalesTarget As New DealerSalesTarget
                    Dim PDCode As String

                    '1 DealerCode
                    PDCode = cols(1).Trim
                    If PDCode = String.Empty Then
                        writeError("Dealer Code can't be empty")
                    Else
                        'objDealerSalesTarget.SAPCode = PDCode.Trim()
                        Try
                            Dim crtDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crtDealer.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, PDCode))
                            Dim objDealer As Dealer = New DealerFacade(user).Retrieve(crtDealer)(0)
                            If Not IsNothing(objDealer) Then
                                objDealerSalesTarget.Dealer = objDealer

                            Else
                                Throw New Exception("Invalid Dealer Code " & PDCode)
                            End If
                        Catch ex As Exception
                            writeError("Dealer Code  error: " & ex.Message)
                        End Try
                    End If

                    ''2 Model
                    'PDCode = cols(2).Trim
                    'If PDCode = String.Empty Then
                    '    writeError("Vehicle Model Code can't be empty")
                    'Else
                    '    Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    '    crt.opAnd(New Criteria(GetType(VechileModel), "IndDescription", MatchType.Exact, PDCode))
                    '    Dim arrVehicleModel As ArrayList = New VechileModelFacade(user).Retrieve(crt)
                    If Not IsNothing(ve) Then
                        objDealerSalesTarget.VehicleModel = ve
                    Else
                        Throw New Exception("Invalid Vehicle Model " & PDCode)
                    End If
                    'End If

                    '3 Sequence
                    PDCode = cols(3).Trim
                    If PDCode = String.Empty Then
                        writeError("Vehicle Model Description can't be empty")
                    Else
                        objDealerSalesTarget.Sequence = PDCode
                    End If

                    '4 FreeDays
                    PDCode = cols(4).Trim
                    If PDCode = String.Empty Then
                        writeError("Vehicle Model Ind Code can't be empty")
                    Else
                        objDealerSalesTarget.FreeDays = PDCode
                    End If

                    '5 MaxQty
                    PDCode = cols(5).Trim
                    If PDCode = String.Empty Then
                        writeError("Vehicle Model Ind Description can't be empty")
                    Else
                        objDealerSalesTarget.MaxQuantity = PDCode
                    End If

                    '6 ValidFrom
                    PDCode = cols(6).Trim
                    If PDCode = String.Empty Then
                        writeError("Vehicle Model Ind Description can't be empty")
                    Else
                        objDealerSalesTarget.ValidFrom = DateTime.ParseExact(PDCode, "yyyyMMdd", Nothing).ToString("yyyy-MM-dd 00:00:00.000")
                    End If

                    '8 MaxQty
                    PDCode = cols(8).Trim
                    If PDCode = String.Empty Then
                        writeError("MaxTOPDay can't be empty")
                    Else
                        Try
                            Dim crtTOP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crtTOP.opAnd(New Criteria(GetType(TermOfPayment), "TermOfPaymentCode", MatchType.Exact, PDCode))
                            Dim objTOP As TermOfPayment = New TermOfPaymentFacade(user).Retrieve(crtTOP)(0)
                            If Not IsNothing(objTOP) Then
                                objDealerSalesTarget.MaxTOPDay = objTOP.TermOfPaymentValue
                            Else
                                Throw New Exception("Term Of Payment Code " & PDCode)
                            End If
                        Catch ex As Exception
                            writeError("Term Of Payment Code  error: " & ex.Message)
                        End Try
                    End If

                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                        objDealerSalesTarget.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                    Else
                        objDealerSalesTarget.LastUpdateBy = user.Identity.Name
                    End If

                    ' insert to array objek MSPMaster
                    If Not IsNothing(objDealerSalesTarget) Then
                        If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objDealerSalesTarget.ErrorMessage = errorMessage.ToString()
                        _arrDealerSalesTarget.Add(objDealerSalesTarget)
                        objDealerSalesTarget = Nothing
                    End If
                Next
            End If
            'Return objDealerSalesTarget
        End Function

        Private Function duplicateData(ByVal objDealerSalesTarget As DealerSalesTarget, ByRef id As Integer) As Boolean
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSalesTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(DealerSalesTarget), "VehicleModel.ID", MatchType.Exact, objDealerSalesTarget.VehicleModel.ID))
            crits.opAnd(New Criteria(GetType(DealerSalesTarget), "Dealer.ID", MatchType.Exact, objDealerSalesTarget.Dealer.ID))
            crits.opAnd(New Criteria(GetType(DealerSalesTarget), "Sequence", MatchType.Exact, objDealerSalesTarget.Sequence))
            crits.opAnd(New Criteria(GetType(DealerSalesTarget), "ValidFrom", MatchType.Exact, objDealerSalesTarget.ValidFrom))
            Dim arr As ArrayList = New DealerSalesTargetFacade(user).Retrieve(crits)
            If arr.Count > 0 Then
                id = CType(arr(0), DealerSalesTarget).ID
                Return True
            Else
                Return False
            End If
        End Function
#End Region

    End Class
End Namespace
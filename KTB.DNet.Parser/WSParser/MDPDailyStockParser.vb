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
Imports KTB.DNet.BusinessFacade.MDP
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
#End Region

Namespace KTB.DNet.Parser
    Public Class MDPDailyStockParser
        Inherits AbstractParser
#Region "Private Variables"
        Private arrMDPDealerDailyStock As ArrayList
        Private Grammar As Regex
        Private errorMessage As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Dim lines As String() = MyBase.GetLines(Content)
            Dim ind As String
            Dim line As String
            Dim objMDPDealerDailyStock As MDPDealerDailyStock = Nothing
            arrMDPDealerDailyStock = New ArrayList


            For i As Integer = 0 To lines.Length - 1
                Try
                    line = lines(i)

                    ind = line.Split(MyBase.ColSeparator)(0)
                    If ind = MyBase.IndicatorHeader Then
                        objMDPDealerDailyStock = Nothing
                        errorMessage = New StringBuilder()
                        ' create objek MDP Dealer Daily Stock
                        objMDPDealerDailyStock = ParseHeader(line)
                        ' insert to array objek MDP Dealer Daily Stock
                        If Not IsNothing(objMDPDealerDailyStock) Then
                            If errorMessage.ToString() <> String.Empty Then
                                objMDPDealerDailyStock.ErrorMessage = errorMessage.ToString()
                            End If

                            arrMDPDealerDailyStock.Add(objMDPDealerDailyStock)
                        End If
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MDPDailyStockParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MDPDailyStockParser, BlockName)
                    Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    Throw e
                End Try
            Next
            Return arrMDPDealerDailyStock
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim mDPDealerDailyStockFacade As New MDPDealerDailyStockFacade(user)
            Dim mDPDealerDailyStockList As New ArrayList

            ' loop MDPDealerDailyStock array
            For Each mDPDealerDailyStock As MDPDealerDailyStock In arrMDPDealerDailyStock
                Try
                    If mDPDealerDailyStock.ErrorMessage = String.Empty AndAlso IsNothing(mDPDealerDailyStock.ErrorMessage) Then
                        mDPDealerDailyStockList.Add(mDPDealerDailyStock)
                    Else
                        nError += 1
                        sMsg &= mDPDealerDailyStock.ErrorMessage & ";"
                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try
            Next

            If mDPDealerDailyStockFacade.InserOrUpdatetWithTransactionManager(mDPDealerDailyStockList) < 0 Then
                nError += 1
            End If

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & mDPDealerDailyStockList.Count.ToString(), "ws-worker", "MDPDailyStockParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MDPDailyStockParser, BlockName)

                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MDPDailyStockParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MDPDailyStockParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As MDPDealerDailyStock
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objMDPDealerDailyStock As New MDPDealerDailyStock

            Dim PDCode As String
            Dim UPTCode As Boolean = False

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else

                'Period Month / H-1
                'Period Start Date / H-6
                'Period End Date / H-7
                PDCode = cols(1).Trim
                If CInt(PDCode.Substring(4, 2)) <= 12 Then
                    If PDCode = String.Empty Then
                        writeError("Period Month / Year can't be empty")
                    Else
                        Dim objDealer As New Dealer
                        Dim objVechileColor As New VechileColor

                        If cols(3).Trim = String.Empty Then
                            writeError("Dealer Code can't be empty")
                        Else
                            objDealer = New DealerFacade(user).Retrieve(cols(3).Trim)
                            If objDealer.ID = 0 Then
                                writeError("Dealer Code Not Found " & cols(3).Trim)
                            End If
                        End If

                        'Material Number / H-4
                        If cols(4).Trim = String.Empty Then
                            writeError("Material Number can't be empty")
                        Else
                            objVechileColor = New VechileColorFacade(user).RetrieveByMaterialNumber(cols(4).Trim)
                            If objVechileColor.ID = 0 Then
                                writeError("Material Number Not Found " & cols(4).Trim)
                            End If
                        End If

                        'Check MDPDealerDailyStock
                        Dim MDPDealerDailyStockCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPDealerDailyStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        MDPDealerDailyStockCriteria.opAnd(New Criteria(GetType(MDPDealerDailyStock), "PeriodMonth", MatchType.Exact, CInt(PDCode.Substring(4, 2))))
                        MDPDealerDailyStockCriteria.opAnd(New Criteria(GetType(MDPDealerDailyStock), "PeriodYear", MatchType.Exact, CInt(PDCode.Substring(0, 4))))
                        MDPDealerDailyStockCriteria.opAnd(New Criteria(GetType(MDPDealerDailyStock), "Dealer.ID", MatchType.Exact, objDealer.ID))
                        MDPDealerDailyStockCriteria.opAnd(New Criteria(GetType(MDPDealerDailyStock), "VechileColor.ID", MatchType.Exact, objVechileColor.ID))
                        MDPDealerDailyStockCriteria.opAnd(New Criteria(GetType(MDPDealerDailyStock), "PeriodStartDate", MatchType.Exact, MyBase.GetDateShort(cols(6).Trim)))
                        MDPDealerDailyStockCriteria.opAnd(New Criteria(GetType(MDPDealerDailyStock), "PeriodEndDate", MatchType.Exact, MyBase.GetDateShort(cols(7).Trim)))
                        MDPDealerDailyStockCriteria.opAnd(New Criteria(GetType(MDPDealerDailyStock), "PeriodeDate", MatchType.Exact, cols(2).Trim))
                        MDPDealerDailyStockCriteria.opAnd(New Criteria(GetType(MDPDealerDailyStock), "ProductionYear", MatchType.Exact, cols(8).Trim))
                        Dim MDPDealerDailyStockList As ArrayList = New MDPDealerDailyStockFacade(user).Retrieve(MDPDealerDailyStockCriteria)

                        If MDPDealerDailyStockList.Count > 0 Then
                            objMDPDealerDailyStock = MDPDealerDailyStockList(0)
                        End If

                        If (objMDPDealerDailyStock.PeriodMonth <> CInt(PDCode.Substring(4, 2))) Then
                            UPTCode = True
                        End If
                        If (objMDPDealerDailyStock.PeriodYear <> CInt(PDCode.Substring(0, 4))) Then
                            UPTCode = True
                        End If

                        objMDPDealerDailyStock.PeriodMonth = CInt(PDCode.Substring(4, 2))
                        objMDPDealerDailyStock.PeriodYear = CInt(PDCode.Substring(0, 4))


                        If objDealer.ID <> 0 Then
                            objMDPDealerDailyStock.Dealer = objDealer
                        Else
                            writeError("Dealer Code Not Found " & cols(2).Trim)
                        End If

                        If objVechileColor.ID <> 0 Then
                            objMDPDealerDailyStock.VechileColor = objVechileColor
                        Else
                            writeError("Material Number Not Found " & cols(3).Trim)
                        End If

                        'Get ID MDPDealerMonthlyStock
                        'Dim MDPDealerMonthlyStockCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPDealerMonthlyStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        'MDPDealerMonthlyStockCriteria.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "PeriodMonth", MatchType.Exact, CInt(PDCode.Substring(4, 2))))
                        'MDPDealerMonthlyStockCriteria.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "PeriodYear", MatchType.Exact, CInt(PDCode.Substring(0, 4))))
                        'MDPDealerMonthlyStockCriteria.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "Dealer.ID", MatchType.Exact, objDealer.ID))
                        'MDPDealerMonthlyStockCriteria.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "VechileColor.ID", MatchType.Exact, objVechileColor.ID))
                        'MDPDealerMonthlyStockCriteria.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "PeriodStartDate", MatchType.Exact, MyBase.GetDateShort(cols(6).Trim)))
                        'MDPDealerMonthlyStockCriteria.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "PeriodEndDate", MatchType.Exact, MyBase.GetDateShort(cols(7).Trim)))
                        'Dim MDPDealerMonthlyStockList As ArrayList = New MDPDealerMonthlyStockFacade(user).Retrieve(MDPDealerMonthlyStockCriteria)

                        'If MDPDealerMonthlyStockList.Count > 0 Then
                        '    objMDPDealerDailyStock.MDPDealerMonthlyStock = MDPDealerMonthlyStockList(0)

                        'Period Date / H-2
                        If cols(2).Trim = String.Empty Then
                            writeError("Period Date can't be empty")
                        Else
                            If (objMDPDealerDailyStock.PeriodeDate <> CInt(cols(2).Trim)) Then
                                UPTCode = True
                            End If
                            objMDPDealerDailyStock.PeriodeDate = CInt(cols(2).Trim)
                        End If

                        'If cols(6).Trim = String.Empty Then
                        '    writeError("Period Start Date can't be empty")
                        'End If

                        'If cols(7).Trim = String.Empty Then
                        '    writeError("Period End Date can't be empty")
                        'End If

                        'Plan Stock
                        PDCode = cols(5).Trim
                        If PDCode = String.Empty Then
                            writeError("Plan Stock can't be empty")
                        Else
                            If (objMDPDealerDailyStock.AllocationQuantity <> CInt(PDCode)) Then
                                UPTCode = True
                            End If
                            objMDPDealerDailyStock.AllocationQuantity = CInt(PDCode)
                        End If

                        'Period Start Date
                        PDCode = cols(6).Trim
                        If PDCode = String.Empty Then
                            writeError("Period Start Date can't be empty")
                        Else
                            If (objMDPDealerDailyStock.PeriodStartDate <> MyBase.GetDateShort(PDCode)) Then
                                UPTCode = True
                            End If
                            objMDPDealerDailyStock.PeriodStartDate = MyBase.GetDateShort(PDCode)
                        End If

                        'Period End Date
                        PDCode = cols(7).Trim
                        If PDCode = String.Empty Then
                            writeError("Period End Date can't be empty")
                        Else
                            If (objMDPDealerDailyStock.PeriodEndDate <> MyBase.GetDateShort(PDCode)) Then
                                UPTCode = True
                            End If
                            objMDPDealerDailyStock.PeriodEndDate = MyBase.GetDateShort(PDCode)
                        End If

                        'Production Year
                        PDCode = cols(8).Trim
                        If PDCode = String.Empty Then
                            writeError("Production Year can't be empty")
                        Else
                            If (objMDPDealerDailyStock.ProductionYear <> CInt(PDCode.Trim)) Then
                                UPTCode = True
                            End If
                            objMDPDealerDailyStock.ProductionYear = PDCode.Trim
                        End If

                        If (UPTCode) Then
                            objMDPDealerDailyStock.LastUpdatedBy = user.Identity.Name
                            objMDPDealerDailyStock.LastUpdatedTime = DateTime.Now
                        Else
                            objMDPDealerDailyStock.LastUpdatedBy = "Not Update"
                        End If
                        'Else
                        'writeError("MDPDealerMonthlyStockID Not Exist")
                        'End If
                    End If
                Else
                    writeError("Periode Month Invalid " + PDCode.Substring(4, 2))
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    objMDPDealerDailyStock.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                End If
            End If

            Return objMDPDealerDailyStock

        End Function

#End Region

#Region "Public Properties"



#End Region

    End Class

End Namespace
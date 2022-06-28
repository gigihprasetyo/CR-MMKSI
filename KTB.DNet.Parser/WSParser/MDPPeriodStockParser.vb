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
    Public Class MDPPeriodStockParser
        Inherits AbstractParser
#Region "Private Variables"
        Private arrMDPDealerPeriodStock As ArrayList
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
            Dim objMDPDealerPeriodStock As MDPDealerMonthlyStock = Nothing
            arrMDPDealerPeriodStock = New ArrayList


            For i As Integer = 0 To lines.Length - 1
                Try
                    line = lines(i)

                    ind = line.Split(MyBase.ColSeparator)(0)
                    If ind = MyBase.IndicatorHeader Then
                        objMDPDealerPeriodStock = Nothing
                        errorMessage = New StringBuilder()
                        ' create objek spare part master
                        objMDPDealerPeriodStock = ParseHeader(line)
                        ' insert to array objek spare part master
                        If Not IsNothing(objMDPDealerPeriodStock) Then
                            If errorMessage.ToString() <> String.Empty Then
                                objMDPDealerPeriodStock.ErrorMessage = errorMessage.ToString()
                            End If

                            arrMDPDealerPeriodStock.Add(objMDPDealerPeriodStock)
                        End If
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MDPPeriodStockParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MDPPeriodStockParser, BlockName)
                    Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    Throw e
                End Try
            Next
            Return arrMDPDealerPeriodStock
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim mDPDealerMonthlyStockFacade As New MDPDealerMonthlyStockFacade(user)
            Dim mDPDealerMonthlyStockList As New ArrayList

            ' loop MDPDealerMonthlyStock array
            For Each mDPDealerMonthlyStock As MDPDealerMonthlyStock In arrMDPDealerPeriodStock
                Try
                    If mDPDealerMonthlyStock.ErrorMessage = String.Empty AndAlso IsNothing(mDPDealerMonthlyStock.ErrorMessage) Then
                        mDPDealerMonthlyStockList.Add(mDPDealerMonthlyStock)
                    Else
                        nError += 1
                        sMsg &= mDPDealerMonthlyStock.ErrorMessage & ";"
                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try
            Next

            If mDPDealerMonthlyStockFacade.InserOrUpdatetWithTransactionManager(mDPDealerMonthlyStockList) < 0 Then
                nError += 1
            End If

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & mDPDealerMonthlyStockList.Count.ToString(), "ws-worker", "MDPPeriodStockParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MDPPeriodStockParser, BlockName)

                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MDPPeriodStockParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MDPPeriodStockParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As MDPDealerMonthlyStock
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objMDPDealerPeriodStock As New MDPDealerMonthlyStock

            Dim PDCode As String
            Dim UPTCode As Boolean = False

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else

                'Period Month / H-1
                PDCode = cols(1).Trim
                If CInt(PDCode.Substring(4, 2)) <= 12 Then
                    If PDCode = String.Empty Then
                        writeError("Period Month / Year can't be empty")
                    Else

                        Dim objDealer As New Dealer
                        Dim objVechileColor As New VechileColor

                        'Dealer Code / H-2
                        If cols(2).Trim = String.Empty Then
                            writeError("Dealer Code can't be empty")
                        Else
                            objDealer = New DealerFacade(user).Retrieve(cols(2).Trim)
                        End If

                        'Material Number / H-3
                        If cols(3).Trim = String.Empty Then
                            writeError("Material Number can't be empty")
                        Else
                            objVechileColor = New VechileColorFacade(user).RetrieveByMaterialNumber(cols(3).Trim)
                            If objVechileColor.ID = 0 Then
                                writeError("Material Number Not Found " & cols(3).Trim)
                            End If
                        End If

                        'Dim MDPDealerMonthlyStockCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPDealerMonthlyStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        'MDPDealerMonthlyStockCriteria.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "PeriodMonth", MatchType.Exact, CInt(PDCode.Substring(4, 2))))
                        'MDPDealerMonthlyStockCriteria.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "PeriodYear", MatchType.Exact, CInt(PDCode.Substring(0, 4))))
                        'MDPDealerMonthlyStockCriteria.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "ProductionYear", MatchType.Exact, CInt(cols(4).Trim)))
                        'MDPDealerMonthlyStockCriteria.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "Dealer.ID", MatchType.Exact, objDealer.ID))
                        'MDPDealerMonthlyStockCriteria.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "VechileColor.ID", MatchType.Exact, objVechileColor.ID))
                        'MDPDealerMonthlyStockCriteria.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "PeriodStartDate", MatchType.Exact, MyBase.GetDateShort(cols(7).Trim)))
                        'MDPDealerMonthlyStockCriteria.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "PeriodEndDate", MatchType.Exact, MyBase.GetDateShort(cols(8).Trim)))
                        'Dim MDPDealerMonthlyStockList As ArrayList = New MDPDealerMonthlyStockFacade(user).Retrieve(MDPDealerMonthlyStockCriteria)

                        'If MDPDealerMonthlyStockList.Count > 0 Then
                        '    objMDPDealerPeriodStock = MDPDealerMonthlyStockList(0)

                        '    If (objMDPDealerPeriodStock.Dealer.ID <> objDealer.ID) Then
                        '        UPTCode = True
                        '    End If

                        '    If (objMDPDealerPeriodStock.VechileColor.ID <> objVechileColor.ID) Then
                        '        UPTCode = True
                        '    End If
                        'End If

                        If (objMDPDealerPeriodStock.PeriodMonth <> CInt(PDCode.Substring(4, 2))) Then
                            UPTCode = True
                        End If
                        If (objMDPDealerPeriodStock.PeriodYear <> CInt(PDCode.Substring(0, 4))) Then
                            UPTCode = True
                        End If

                        objMDPDealerPeriodStock.PeriodMonth = CInt(PDCode.Substring(4, 2))
                        objMDPDealerPeriodStock.PeriodYear = CInt(PDCode.Substring(0, 4))


                        If objDealer.ID <> 0 Then
                            objMDPDealerPeriodStock.Dealer = objDealer
                        Else
                            writeError("Dealer Code Not Found " & cols(2).Trim)
                        End If

                        If objVechileColor.ID <> 0 Then
                            objMDPDealerPeriodStock.VechileColor = objVechileColor
                        Else
                            writeError("Material Number Not Found " & cols(3).Trim)
                        End If

                        'Production Year
                        PDCode = cols(4).Trim
                        If PDCode = String.Empty Then
                            writeError("Production Year can't be empty")
                        Else
                            If (objMDPDealerPeriodStock.ProductionYear <> CInt(PDCode)) Then
                                UPTCode = True
                            End If
                            objMDPDealerPeriodStock.ProductionYear = CInt(PDCode)
                        End If

                        'Carry Over Stock
                        PDCode = cols(5).Trim
                        If PDCode = String.Empty Then
                            writeError("Carry Over Stock can't be empty")
                        Else
                            If (objMDPDealerPeriodStock.CarryOverStock <> CInt(PDCode)) Then
                                UPTCode = True
                            End If
                            objMDPDealerPeriodStock.CarryOverStock = CInt(PDCode)
                        End If

                        'Plan Stock
                        PDCode = cols(6).Trim
                        If PDCode = String.Empty Then
                            writeError("Plan Stock can't be empty")
                        Else
                            If (objMDPDealerPeriodStock.PlanStock <> CInt(PDCode)) Then
                                UPTCode = True
                            End If
                            objMDPDealerPeriodStock.PlanStock = CInt(PDCode)
                        End If

                        'Period Start Date
                        PDCode = cols(7).Trim
                        If PDCode = String.Empty Then
                            writeError("Period Start Date can't be empty")
                        Else
                            If (objMDPDealerPeriodStock.PeriodStartDate <> MyBase.GetDateShort(PDCode)) Then
                                UPTCode = True
                            End If
                            objMDPDealerPeriodStock.PeriodStartDate = MyBase.GetDateShort(PDCode)
                        End If

                        'Period End Date
                        PDCode = cols(8).Trim
                        If PDCode = String.Empty Then
                            writeError("Period End Date can't be empty")
                        Else
                            If (objMDPDealerPeriodStock.PeriodEndDate <> MyBase.GetDateShort(PDCode)) Then
                                UPTCode = True
                            End If
                            objMDPDealerPeriodStock.PeriodEndDate = MyBase.GetDateShort(PDCode)
                        End If

                        If (UPTCode) Then
                            objMDPDealerPeriodStock.LastUpdatedBy = user.Identity.Name
                            objMDPDealerPeriodStock.LastUpdatedTime = DateTime.Now
                        Else
                            objMDPDealerPeriodStock.LastUpdatedBy = "Not Update"
                        End If
                    End If
                Else
                    writeError("Periode Month Invalid " + PDCode.Substring(4, 2))
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    objMDPDealerPeriodStock.ErrorMessage = errorMessage.ToString() & vbCrLf & line

                End If
            End If

            Return objMDPDealerPeriodStock

        End Function

#End Region

#Region "Public Properties"



#End Region

    End Class

End Namespace
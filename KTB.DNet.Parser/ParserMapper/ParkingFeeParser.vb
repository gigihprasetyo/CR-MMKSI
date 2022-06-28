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
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class ParkingFeeParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _stream As StreamReader
        Private _grammar As Regex
        Private _ListParkingFee As ArrayList
        Private _fileName As String
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            _grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _stream = New StreamReader(fileName, True)
            _ListParkingFee = New ArrayList
            _fileName = fileName
            Dim val As String = MyBase.NextLine(_stream).Trim()
            While (Not val = "")
                Try
                    ParsePenalty(val)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "PenaltyParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.InvoiceParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_stream)
            End While

            If Not _stream Is Nothing Then
                _stream.Close()
                _stream = Nothing
            End If
            Return _ListParkingFee
        End Function

        Private Sub ParsePenalty(ByVal ValParser As String)
            Dim objPF As New ParkingFee
            Dim objPKFac As ParkingFeeFacade = New ParkingFeeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim objDealerFac As DealerFacade = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim objCategoryFac As CategoryFacade = New CategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim Fields() As String
            Dim errorSb As StringBuilder = New StringBuilder
            Dim strCreditAccount As String = ""
            Dim sTemp As String = String.Empty


            Fields = ValParser.Split(";")
            'Changed lenght from 5 to 6, additional on categoryid at last 
            If Fields.Length = 6 Then
                'DealerCode;DebitChargeNumber;DebitMemoNumber;Amount;Periode
                'DealerCode
                sTemp = Fields(0)
                Try
                    Dim oDealer As Dealer = objDealerFac.Retrieve(sTemp)
                    If Not IsNothing(oDealer) AndAlso oDealer.ID > 0 Then
                        objPF.Dealer = oDealer
                    Else
                        errorSb.Append("Invalid Dealer Code" & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    errorSb.Append("Invalid Dealer Code" & Chr(13) & Chr(10))
                End Try
                ';DebitChargeNumber
                sTemp = Fields(1)
                objPF.DebitChargeNumber = sTemp
                ';DebitMemoNumber
                sTemp = Fields(2)
                objPF.DebitMemoNumber = sTemp
                'Amount
                sTemp = Fields(3)
                Try
                    Dim dAmount As Decimal = CDec(sTemp)
                    objPF.Amount = dAmount
                Catch ex As Exception
                    errorSb.Append("Invalid Amount" & Chr(13) & Chr(10))
                End Try
                'Periode
                sTemp = Fields(4)
                Try
                    Dim PeriodYear As Integer
                    objPF.Periode = EnumParkingFeePeriod.GetEnumValueForWSM(sTemp, PeriodYear)
                    objPF.Year = PeriodYear
                Catch ex As Exception
                    errorSb.Append("Invalid Period" & Chr(13) & Chr(10))
                End Try
                'Category
                sTemp = Fields(5)
                Try
                    Dim oCategroy As Category = objCategoryFac.Retrieve(CInt(sTemp))
                    If Not IsNothing(oCategroy) AndAlso oCategroy.ID > 0 Then
                        objPF.Category = oCategroy
                    Else
                        errorSb.Append("Invalid Category ID" & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    errorSb.Append("Invalid Category ID" & Chr(13) & Chr(10))
                End Try
            Else
                errorSb.Append("Struktur Data tidak valid" & Chr(13) & Chr(10))
            End If
            If errorSb.Length > 0 Then
                'Throw New Exception(errorSb.ToString)
            Else
                _ListParkingFee.Add(objPF)
            End If
        End Sub

        Protected Overrides Function DoTransaction() As Integer
            If _ListParkingFee.Count > 0 Then
                Dim objPFFac As ParkingFeeFacade = New ParkingFeeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Dim objPenaltyStatusFac As PenaltyParkirHistoryFacade = New PenaltyParkirHistoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Dim objPFExisting As ParkingFee
                Dim IsExisting As Boolean
                Dim IsSameWithOld As Boolean = True
                Dim crtPF As CriteriaComposite
                Dim aPFs As ArrayList
                Dim arlPFUpdated As New ArrayList

                For Each objPF As ParkingFee In _ListParkingFee
                    IsExisting = False

                    crtPF = New CriteriaComposite(New Criteria(GetType(ParkingFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crtPF.opAnd(New Criteria(GetType(ParkingFee), "Dealer.DealerCode", MatchType.Exact, objPF.Dealer.DealerCode))
                    crtPF.opAnd(New Criteria(GetType(ParkingFee), "Periode", MatchType.Exact, objPF.Periode))
                    crtPF.opAnd(New Criteria(GetType(ParkingFee), "Year", MatchType.Exact, objPF.Year))
                    crtPF.opAnd(New Criteria(GetType(ParkingFee), "DebitChargeNumber", MatchType.Exact, objPF.DebitChargeNumber))
                    aPFs = objPFFac.Retrieve(crtPF)
                    If aPFs.Count > 0 Then
                        objPFExisting = CType(aPFs(0), ParkingFee)
                    Else
                        objPFExisting = New ParkingFee
                    End If
                    If Not IsNothing(objPFExisting) AndAlso objPFExisting.ID > 0 Then IsExisting = True
                    Try
                        Dim iReturn As Integer = 0
                        If Not IsExisting Then
                            iReturn = objPFFac.Insert(objPF)
                        Else
                            If objPFExisting.DebitMemoNumber <> objPF.DebitMemoNumber Then
                                objPFExisting.DebitMemoNumber = objPF.DebitMemoNumber
                                IsSameWithOld = False
                            End If
                            If objPFExisting.DebitChargeNumber <> objPF.DebitChargeNumber Then
                                objPFExisting.DebitChargeNumber = objPF.DebitChargeNumber
                                IsSameWithOld = False
                            End If
                            If objPFExisting.Amount <> objPF.Amount Then
                                objPFExisting.Amount = objPF.Amount
                                IsSameWithOld = False
                            End If
                            If IsNothing(objPFExisting.Category) Then
                                objPFExisting.Category = objPF.Category
                                IsSameWithOld = False
                            Else
                                If objPFExisting.Category.CategoryCode <> objPF.Category.CategoryCode Then
                                    objPFExisting.Category = objPF.Category
                                    IsSameWithOld = False
                                End If
                            End If

                            If Not IsSameWithOld Then
                                iReturn = objPFFac.Update(objPFExisting)
                                arlPFUpdated.Add(objPFExisting)
                            End If
                        End If
                        If iReturn <> -1 Then

                            Dim crtHist As CriteriaComposite
                            crtHist = New CriteriaComposite(New Criteria(GetType(PenaltyParkirHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crtHist.opAnd(New Criteria(GetType(PenaltyParkirHistory), "ParkingFee.ID", MatchType.Exact, objPF.ID))

                            Dim sortColl As SortCollection = New SortCollection
                            sortColl.Add(New Sort(GetType(PenaltyParkirHistory), "CreatedTime", Sort.SortDirection.DESC))

                            Dim arlHist As ArrayList = objPenaltyStatusFac.Retrieve(crtHist, sortColl)
                            Dim objStatusHistOld As PenaltyParkirHistory

                            If arlHist.Count > 0 Then
                                objStatusHistOld = CType(arlHist(0), PenaltyParkirHistory)
                            Else
                                objStatusHistOld = New PenaltyParkirHistory
                            End If

                            Dim objStatusHistNew As New PenaltyParkirHistory
                            If Not IsExisting Then
                                Dim objPFNew As ParkingFee = objPFFac.Retrieve(iReturn)
                                objStatusHistNew.ParkingFee = objPFNew
                            Else
                                objStatusHistNew.ParkingFee = objPFExisting
                                If objStatusHistOld.ID > 0 Then
                                    objStatusHistNew.OldStatus = objStatusHistOld.NewStatus
                                End If
                            End If
                            objStatusHistNew.NewStatus = EnumParkingFeeStatus.ParkingFeeStatus.Baru
                            objPenaltyStatusFac.Insert(objStatusHistNew)


                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ParkingFee", "ParkingFeeParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.CessieParser, BlockName)
                        Dim e1 As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objPF.ID.ToString & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e1, "Parser Policy")
                    End Try
                Next
                'Update Total Amount sama PPH AMount di ParkingFeeReturnHeader
                'Updated by anh 20121228
                'Start 
                Try
                    If arlPFUpdated.Count > 0 Then
                        For Each objPF As ParkingFee In arlPFUpdated

                            Dim objPFRD As New ParkingFeeReturnDetail
                            Dim arlPFRD As ArrayList
                            Dim crtPFRD As CriteriaComposite

                            crtPFRD = New CriteriaComposite(New Criteria(GetType(ParkingFeeReturnDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crtPFRD.opAnd(New Criteria(GetType(ParkingFeeReturnDetail), "ParkingFee.ID", MatchType.Exact, objPF.ID))

                            Dim objPFRDFac As ParkingFeeReturnDetailFacade = New ParkingFeeReturnDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            arlPFRD = objPFRDFac.Retrieve(crtPFRD)
                            If arlPFRD.Count > 0 Then
                                objPFRD = arlPFRD(0)
                                Dim objPFRH As New ParkingFeeReturnHeader
                                Dim arlPFRH As ArrayList
                                Dim crtPFRH As CriteriaComposite
                                crtPFRH = New CriteriaComposite(New Criteria(GetType(ParkingFeeReturnHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                crtPFRH.opAnd(New Criteria(GetType(ParkingFeeReturnHeader), "ID", MatchType.Exact, objPFRD.ParkingFeeReturnHeader.ID))

                                Dim objPFRHFac As ParkingFeeReturnHeaderFacade = New ParkingFeeReturnHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                arlPFRH = objPFRHFac.Retrieve(crtPFRH)
                                If arlPFRH.Count > 0 Then
                                    objPFRH = arlPFRH(0)

                                    Dim amount As Long = 0
                                    Dim arlPFRD2 As ArrayList
                                    Dim crtPFRD2 As CriteriaComposite

                                    crtPFRD2 = New CriteriaComposite(New Criteria(GetType(ParkingFeeReturnDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                    crtPFRD2.opAnd(New Criteria(GetType(ParkingFeeReturnDetail), "ParkingFeeReturnHeader.ID", MatchType.Exact, objPFRH.ID))
                                    arlPFRD2 = objPFRDFac.Retrieve(crtPFRD2)

                                    If arlPFRD2.Count > 0 Then
                                        For Each pfrd As ParkingFeeReturnDetail In arlPFRD2
                                            amount += pfrd.ParkingFee.Amount
                                        Next
                                    End If

                                    objPFRH.TotalAmount = amount
                                    objPFRH.PPHAmount = amount * 0.1
                                    objPFRHFac.Update(objPFRH)
                                End If
                            End If
                        Next
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ParkingFee", "ParkingFeeParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.CessieParser, BlockName)
                    Dim e1 As Exception = New Exception(_fileName & Chr(13) & Chr(10) & " Update Amount on ParkingFeeReturnHeader error :" & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e1, "Parser Policy")
                End Try
                'End 
            End If
            Return 0
        End Function



        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function
    End Class
End Namespace


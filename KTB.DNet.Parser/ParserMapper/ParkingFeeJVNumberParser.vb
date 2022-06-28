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
    Public Class ParkingFeeJVNumberParser
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
            Dim Fields() As String
            Dim errorSb As StringBuilder = New StringBuilder
            Dim strCreditAccount As String = ""
            Dim sTemp As String = String.Empty


            Fields = ValParser.Split(";")
            'Changed lenght from 6 to 7, additional on dealerdepositA at last  (20121021)
            If Fields.Length = 7 Then
                'DealerCode;DebitMemoNumber;AssignmentNumber;AMOUNT,Description, BASELINEDATE, DealerDepositA
                'CAPITALIZED : UNUSED
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
                ';DebitMemoNumber
                sTemp = Fields(1)
                objPF.DebitMemoNumber = sTemp
                ';AssignmentNumber
                sTemp = Fields(2)
                objPF.AssignmentNumber = sTemp
                ';Description
                sTemp = Fields(4)
                objPF.Description = sTemp
                ';DealerDepositA (20121021)
                sTemp = Fields(6)
                Try
                    Dim oDealer2 As Dealer = objDealerFac.Retrieve(sTemp)
                    If Not IsNothing(oDealer2) AndAlso oDealer2.ID > 0 Then
                        objPF.DealerDepositA = oDealer2
                    Else
                        errorSb.Append("Invalid DepositA Dealer Code" & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    errorSb.Append("Invalid DepositA Dealer Code" & Chr(13) & Chr(10))
                End Try

            Else
                errorSb.Append("Struktur Data tidak valid" & Chr(13) & Chr(10))
            End If
            If errorSb.Length > 0 Then
                Throw New Exception(errorSb.ToString)
            Else
                _ListParkingFee.Add(objPF)
            End If
        End Sub

        Protected Overrides Function DoTransaction() As Integer
            If _ListParkingFee.Count > 0 Then
                Dim objPFFac As ParkingFeeFacade = New ParkingFeeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Dim objPFExisting As ParkingFee
                Dim IsExisting As Boolean
                Dim IsSameWithOld As Boolean = True
                Dim crtPF As CriteriaComposite
                Dim aPFs As ArrayList

                For Each objPF As ParkingFee In _ListParkingFee
                    IsExisting = False

                    crtPF = New CriteriaComposite(New Criteria(GetType(ParkingFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crtPF.opAnd(New Criteria(GetType(ParkingFee), "Dealer.DealerCode", MatchType.Exact, objPF.Dealer.DealerCode))
                    crtPF.opAnd(New Criteria(GetType(ParkingFee), "DebitMemoNumber", MatchType.Exact, objPF.DebitMemoNumber))
                    aPFs = objPFFac.Retrieve(crtPF)
                    If aPFs.Count > 0 Then
                        objPFExisting = CType(aPFs(0), ParkingFee)
                    Else
                        objPFExisting = New ParkingFee
                    End If
                    If Not IsNothing(objPFExisting) AndAlso objPFExisting.ID > 0 Then IsExisting = True
                    Try
                        If Not IsExisting Then
                            'objPFFac.Insert(objPF)
                        Else
                            If objPFExisting.AssignmentNumber <> objPF.AssignmentNumber Then
                                objPFExisting.AssignmentNumber = objPF.AssignmentNumber
                                IsSameWithOld = False
                            End If
                            If objPFExisting.Description <> objPF.Description Then
                                objPFExisting.Description = objPF.Description
                                IsSameWithOld = False
                            End If
                            If Not IsNothing(objPFExisting.DealerDepositA) Then
                                If objPFExisting.DealerDepositA.ID <> objPF.DealerDepositA.ID Then
                                    objPFExisting.DealerDepositA = objPF.DealerDepositA
                                    IsSameWithOld = False
                                End If
                            Else
                                objPFExisting.DealerDepositA = objPF.DealerDepositA
                                IsSameWithOld = False
                            End If

                            If Not IsSameWithOld Then
                                objPFFac.Update(objPFExisting)
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ParkingFee", "ParkingFeeParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.CessieParser, BlockName)
                        Dim e1 As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objPF.ID.ToString & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e1, "Parser Policy")
                    End Try
                Next
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function
    End Class
End Namespace


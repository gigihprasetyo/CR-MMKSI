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
    Public Class ParkingFeeJVReturnParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _stream As StreamReader
        Private _grammar As Regex
        Private _ListPFReturnHeader As ArrayList
        Private _fileName As String
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            _grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _stream = New StreamReader(fileName, True)
            _ListPFReturnHeader = New ArrayList
            _fileName = fileName
            Dim val As String = MyBase.NextLine(_stream).Trim()
            While (Not val = "")
                Try
                    ParseJVReturn(val)
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
            Return _ListPFReturnHeader
        End Function

        Private Sub ParseJVReturn(ByVal ValParser As String)
            Dim objPFRH As New ParkingFeeReturnHeader
            Dim objPKFac As ParkingFeeReturnHeaderFacade = New ParkingFeeReturnHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim objDealerFac As DealerFacade = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim Fields() As String
            Dim errorSb As StringBuilder = New StringBuilder
            Dim strCreditAccount As String = ""
            Dim sTemp As String = String.Empty


            Fields = ValParser.Split(";")
            If Fields.Length = 9 Then
                'DocumentDate(ReturnDate:DDMMYYYY);Reference2(NoReg)
                ';POSTINGDATE;DocumentNumber(ReturnAssignNumber)
                ';DOCUMENTNUMER,CUSTOMER;AMOUNT;ASSIGNMENT;Text
                '

                'NOTE   :CAPITALIZED:UNUSED

                'DocumentDate(ReturnDate)
                sTemp = Fields(0)

                Try
                    Dim DocDate As Date
                    DocDate = DateSerial(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2))
                    objPFRH.ReturnDate = DocDate
                Catch ex As Exception
                    objPFRH.ReturnDate = DateSerial(1900, 1, 1)
                    errorSb.Append("Invalid DocumentDate" & Chr(13) & Chr(10))
                End Try
                Dim oPFRD As ParkingFeeReturnDetail

                'Reference2(NoReg)
                sTemp = Fields(1)
                objPFRH.NoReg = sTemp
                'DocumentNumber(ReturnAssignNumber)
                sTemp = Fields(3)
                objPFRH.ReturnAssignNumber = sTemp
                'DocumentNumber(BuktiPotongNumber)
                sTemp = Fields(8)
                objPFRH.Description = sTemp

            Else
                errorSb.Append("Struktur Data tidak valid" & Chr(13) & Chr(10))
            End If
            If errorSb.Length > 0 Then
                Throw New Exception(errorSb.ToString)
            Else
                _ListPFReturnHeader.Add(objPFRH)
            End If
        End Sub

        Protected Overrides Function DoTransaction() As Integer
            If _ListPFReturnHeader.Count > 0 Then
                Dim objPFRHFac As ParkingFeeReturnHeaderFacade = New ParkingFeeReturnHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Dim objPFFac As ParkingFeeFacade = New ParkingFeeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Dim objPenaltyStatusFac As PenaltyParkirHistoryFacade = New PenaltyParkirHistoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Dim objPFRHExisting As ParkingFeeReturnHeader
                Dim IsExisting As Boolean
                Dim IsSameWithOld As Boolean = True
                Dim crtPF As CriteriaComposite
                Dim aPFs As ArrayList

                For Each objPFRH As ParkingFeeReturnHeader In _ListPFReturnHeader
                    IsExisting = False

                    crtPF = New CriteriaComposite(New Criteria(GetType(ParkingFeeReturnHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crtPF.opAnd(New Criteria(GetType(ParkingFeeReturnHeader), "NoReg", MatchType.Exact, objPFRH.NoReg))
                    aPFs = objPFRHFac.Retrieve(crtPF)
                    Dim oldStatus As Integer = 0
                    If aPFs.Count > 0 Then
                        objPFRHExisting = CType(aPFs(0), ParkingFeeReturnHeader)
                        oldStatus = objPFRHExisting.Status
                    Else
                        objPFRHExisting = New ParkingFeeReturnHeader
                    End If

                    If Not IsNothing(objPFRHExisting) AndAlso objPFRHExisting.ID > 0 Then IsExisting = True
                    Try
                        Dim iReturn As Integer = 0
                        If Not IsExisting Then
                            'objPFRHFac.Insert(objPFRH)
                        Else
                            If objPFRHExisting.ReturnAssignNumber <> objPFRH.ReturnAssignNumber Then
                                objPFRHExisting.ReturnAssignNumber = objPFRH.ReturnAssignNumber
                                IsSameWithOld = False
                            End If
                            If objPFRHExisting.Description <> objPFRH.Description Then
                                objPFRHExisting.Description = objPFRH.Description
                                IsSameWithOld = False
                            End If
                            If Not IsSameWithOld Then
                                objPFRHExisting.Status = EnumPengembalianPPhStatus.PengembalianPPhStatus.Selesai
                                Dim aTemps As New ArrayList
                                aTemps.Add(objPFRHExisting)
                                objPFRHFac.Update(objPFRHExisting)
                                objPFRHFac.UpdateParkingFeeReturHeaderStatusSelesai(aTemps, objPFRHExisting.Status, oldStatus)

                                Dim oPF As ParkingFee
                                For Each oPFRD As ParkingFeeReturnDetail In objPFRHExisting.ParkingFeeReturnDetails
                                    oPF = oPFRD.ParkingFee
                                    'oPF.Status = CType(EnumParkingFeeStatus.ParkingFeeStatus.Selesai, Short)
                                    oPF.Status = EnumParkingFeeStatus.ParkingFeeStatus.Selesai
                                    'insert nto history

                                    iReturn = objPFFac.Update(oPF)
                                Next
                                'If iReturn <> -1 Then
                                Dim objStatusHistOld As PenaltyParkirHistory
                                Dim crtHist As CriteriaComposite
                                crtHist = New CriteriaComposite(New Criteria(GetType(PenaltyParkirHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                crtHist.opAnd(New Criteria(GetType(PenaltyParkirHistory), "ParkingFee.ID", MatchType.Exact, oPF.ID))
                                crtHist.opAnd(New Criteria(GetType(PenaltyParkirHistory), "NewStatus", MatchType.Exact, CType(EnumParkingFeeStatus.ParkingFeeStatus.Selesai, Short)))

                                Dim sortColl As SortCollection = New SortCollection
                                sortColl.Add(New Sort(GetType(PenaltyParkirHistory), "CreatedTime", Sort.SortDirection.DESC))

                                Dim arlHist As ArrayList = objPenaltyStatusFac.Retrieve(crtHist, sortColl)
                                If arlHist.Count > 0 Then
                                    objStatusHistOld = CType(arlHist(0), PenaltyParkirHistory)
                                Else
                                    objStatusHistOld = New PenaltyParkirHistory
                                End If

                                Dim objStatusHist As New PenaltyParkirHistory
                                objStatusHist.ParkingFee = oPF
                                If objStatusHistOld.ID > 0 Then
                                    objStatusHist.OldStatus = objStatusHistOld.NewStatus
                                End If
                                objStatusHist.NewStatus = EnumParkingFeeStatus.ParkingFeeStatus.Selesai
                                objPenaltyStatusFac.Insert(objStatusHist)

                                'End If
                            End If
                        End If
                        
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ParkingFeeReturnHeader", "ParkingFeeReturnHeaderParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.CessieParser, BlockName)
                        Dim e1 As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objPFRH.ID.ToString & Chr(13) & Chr(10) & ex.Message)
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


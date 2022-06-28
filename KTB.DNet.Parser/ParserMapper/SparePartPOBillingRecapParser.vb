#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Security.Principal
#End Region

Namespace KTB.DNet.Parser
    Public Class SparePartPOBillingRecapParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _SPPOBillingRecap As SparePartPOBillingRecap
        Private _ListSPPOBillingRecap As ArrayList
        Private _CurrentDealer As Dealer
        Private _CurrentPeriodMonth As Integer
        Private _CurrentPeriodYear As Integer
        Private _bIsHeaderValid As Boolean
        Private _fileName As String = ""
#End Region

#Region "Constructors/Destructors/Finalizers"
        Public Sub New()
        End Sub
#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _fileName = fileName
            _Stream = New StreamReader(fileName, True)
            _ListSPPOBillingRecap = New ArrayList

            Dim val As String = MyBase.NextLine(_Stream).Trim()

            While (Not val = "")
                Try
                    ParseSPPOBillingRecap(val)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SparePartPOBillingRecapParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SparePartPOBillingRecapParser, BlockName)
                    Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return _ListSPPOBillingRecap
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

        Protected Overrides Function DoTransaction() As Integer
            If _ListSPPOBillingRecap.Count > 0 Then
                'Dim objSPPOBillingRecapFacade As SparePartPOBillingFacade = New SparePartPOBillingFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                'objSPPOBillingRecapFacade.InsertOrUpdate(_ListSPPOBillingRecap)
                Try
                    Dim objSPPOBillingRecapFacade As SparePartPOBillingFacade = New SparePartPOBillingFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    objSPPOBillingRecapFacade.InsertOrUpdate(_ListSPPOBillingRecap)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SparePartPOBillingRecapParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SparePartPOBillingRecapParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            End If
        End Function

#End Region

#Region "Private Methods"
        Private Sub ParseSPPOBillingRecap(ByVal ValParser As String)
            Dim splittedVal As String()
            splittedVal = ValParser.Split(";")

            If splittedVal.Length <> 3 Or splittedVal.Length <> 6 Then
                If splittedVal(0) = "H" Then
                    Try
                        Dim month As Integer
                        Dim year As Integer

                        month = CType(splittedVal(2).Substring(0, 2), Integer)
                        year = CType(splittedVal(2).Substring(2, 4), Integer)


                        Dim objDealerfacade As DealerFacade = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        Dim objDealer As Dealer
                        objDealer = objDealerfacade.Retrieve(splittedVal(1))
                        If (Not objDealer Is Nothing) AndAlso (Not objDealer.ID = 0) Then
                            _bIsHeaderValid = True
                        Else
                            _bIsHeaderValid = False
                        End If

                        _CurrentDealer = objDealer
                        _CurrentPeriodMonth = month
                        _CurrentPeriodYear = year

                    Catch ex As Exception
                        Throw ex
                    End Try

                ElseIf splittedVal(0) = "D" Then
                    Dim objSPPOBillingRecap As SparePartPOBillingRecap
                    objSPPOBillingRecap = New SparePartPOBillingRecap

                    If _bIsHeaderValid Then
                        Try
                            objSPPOBillingRecap.Dealer = _CurrentDealer
                            objSPPOBillingRecap.PeriodMonth = _CurrentPeriodMonth
                            objSPPOBillingRecap.PeriodYear = _CurrentPeriodYear

                            objSPPOBillingRecap.BillingNumber = splittedVal(1)
                            Dim billDate As DateTime

                            billDate = New Date(CType(splittedVal(2).Substring(4, 4), Integer), CType(splittedVal(2).Substring(2, 2), Integer), CType(splittedVal(2).Substring(0, 2), Integer))

                            objSPPOBillingRecap.BillingDate = billDate
                            objSPPOBillingRecap.BillingAmount = IIf(splittedVal(5) = "T", CType(splittedVal(3), Decimal) * -1, CType(splittedVal(3), Decimal))
                            objSPPOBillingRecap.PPN = IIf(splittedVal(5) = "T", CType(splittedVal(4), Decimal) * -1, CType(splittedVal(4), Decimal))
                            objSPPOBillingRecap.OrderType = splittedVal(5)

                            _ListSPPOBillingRecap.Add(objSPPOBillingRecap)
                        Catch ex As Exception
                            Throw ex
                        End Try

                    Else
                        Throw New Exception("Invalid Dealer")
                    End If
                Else
                    Throw New Exception("Invalid Row")
                End If
            Else
                Throw New Exception("Invalid Row")
            End If
        End Sub
#End Region
    End Class
End Namespace


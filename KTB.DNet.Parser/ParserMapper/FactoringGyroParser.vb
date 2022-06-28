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
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Utility
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Configuration
#End Region

Namespace KTB.DNet.Parser
    Public Class FactoringGyroParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _DailyPayments As ArrayList
        Private Grammar As Regex
        Private mapper As IMapper
        Private _fileName As String
        Private ErrorMessage As StringBuilder
        Private _isCleansing As Integer '0 Production, 1 Cleansing
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _isCleansing = Val(KTB.DNet.Lib.WebConfig.GetValue("ISCLEANSING"))

            _fileName = fileName
            _Stream = New StreamReader(fileName, True)
            _DailyPayments = New ArrayList
            Dim vals As String = MyBase.NextLine(_Stream).Trim()

            While (Not vals = "")
                Try
                    ParseDailyPayment(vals + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DailyPaymentParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DailyPaymentParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & vals & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                vals = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return _DailyPayments
        End Function


        Protected Overrides Function DoTransaction() As Integer
            'TO DO call facade to insert using transaction
            Dim _DailyPaymentFacade As DailyPaymentFacade
            Dim arlTemp As New ArrayList
            Dim oPP As PaymentPurpose 'VH+PP+IT
            _DailyPaymentFacade = New DailyPaymentFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)))
            oPP = New PaymentPurposeFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))).Retrieve("VH+PP+IT")

            If IsNothing(oPP) OrElse oPP.ID < 1 Then
                Throw New Exception("Undefined Payment Purpose VH+PP+IT")
                Return 0
            End If
            For Each item As DailyPayment In _DailyPayments
                Try
                    Dim objDP As DailyPayment
                    'Default Value
                    item.PaymentPurpose = oPP
                    item.IsReversed = 0
                    item.IsCleared = 0
                    If Not IsNothing(item.POHeader) Then
                        Dim ID As Integer
                        objDP = GetDailyPayment(item.POHeader.ID, item.DocNumber, item.FiscalYear)

                        With objDP
                            .PaymentPurpose = item.PaymentPurpose
                            .IsReversed = item.IsReversed
                            .IsCleared = item.IsCleared
                            .DocDate = item.DocDate
                            .BaselineDate = item.BaselineDate
                            .SlipNumber = item.SlipNumber
                            .Amount = item.Amount
                            .DailyPaymentHeader = item.DailyPaymentHeader
                            .EffectiveDate = CommonFunction.AddNWorkingDay(item.BaselineDate, 1)
                            If IsNothing(objDP) OrElse objDP.ID < 1 Then
                                objDP.SalesOrder = item.SalesOrder
                                objDP.POHeader = item.POHeader
                            End If
                        End With
                    End If
                    objDP.Status = EnumPaymentStatus.PaymentStatus.Selesai

                    If objDP.SlipNumber.Trim.StartsWith("TRF") Then
                        objDP.EntryType = EnumGyroEntryType.EntryType.Transfer
                    Else
                        objDP.EntryType = EnumGyroEntryType.EntryType.Gyro
                    End If
                    objDP.GyroType = EnumGyroType.GyroType.Normal

                    'objDP.AcceleratedDate = DateSerial(1753, 1, 1)
                    'objDP.EntryDate = DateSerial(1753, 1, 1)
                    'objDP.CessieTime = DateSerial(1753, 1, 1)
                    'objDP.LastUploadedTime = DateSerial(1753, 1, 1)

                    _DailyPaymentFacade = New DailyPaymentFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)))
                    _DailyPaymentFacade.insertDP(objDP)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DailyPaymentParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DailyPaymentParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.DocNumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"


        Private Function GetDailyPayment(ByVal POID As Integer, ByVal docNumber As String, Optional ByVal FiscalYear As Short = 0) As DailyPayment
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "DocNumber", MatchType.Exact, docNumber))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, POID))
            Dim _dailyPOCollection As ArrayList = New DailyPaymentFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)

            If _dailyPOCollection.Count > 0 Then
                For Each objDP As DailyPayment In _dailyPOCollection
                    If objDP.FiscalYear = FiscalYear Then
                        Return objDP
                    End If
                Next
                Return _dailyPOCollection.Item(0)
            Else
                Return New DailyPayment
            End If

            If _dailyPOCollection.Count > 0 Then
                Return _dailyPOCollection.Item(0)
            Else
                Return New DailyPayment
            End If
        End Function


        Private Sub ParseDailyPayment(ByVal ValParser As String)
            Dim _Dp As DailyPayment = New DailyPayment
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim IsDataCessie As Boolean = False
            sStart = 0
            nCount = 0
            ErrorMessage = New StringBuilder
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()

                'OLD FORMAT:SONumber;DocDate; BaselineDate;EffectiveDate;SlipNumber;Amount;Currency;DailyPaymentHeader.RegNumber
                'NEW FORMAT:SONumber;DocDate; PostingDate;BaselineDate;SlipNumber;Amount;Currency;DailyPaymentHeader.RegNumber

                Select Case (nCount)
                    Case Is = 0
                        Dim objSalesOrder As SalesOrder
                        objSalesOrder = New SalesOrderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                        If Not IsNothing(objSalesOrder) AndAlso objSalesOrder.ID > 0 Then
                            _Dp.POHeader = objSalesOrder.POHeader
                            _Dp.SalesOrder = objSalesOrder
                        Else
                            ErrorMessage.Append("Invalid SO Number" & Chr(13) & Chr(10))
                        End If
                    Case Is = 1
                        Try
                            Dim _date As Date = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2))
                            _Dp.DocDate = _date
                        Catch ex As Exception
                            ErrorMessage.Append("Invalid Doc Date" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 2
                    Case Is = 3
                        'Try
                        '    Dim _date As Date = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2))
                        '    _Dp.BaselineDate = _date
                        'Catch ex As Exception
                        '    ErrorMessage.Append("Invalid BaselineDate" & Chr(13) & Chr(10))
                        'End Try
                        Try
                            Dim _date As Date = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2))
                            _Dp.BaselineDate = _date
                        Catch ex As Exception
                            ErrorMessage.Append("Invalid BaselineDate" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 4
                        If sTemp.Trim <> String.Empty Then
                            _Dp.SlipNumber = sTemp
                        Else
                            ErrorMessage.Append("Invalid SlipNumber (Empty)" & Chr(13) & Chr(10))
                        End If
                    Case Is = 5
                        Dim Amount As Decimal
                        Try
                            Amount = CType(sTemp, Decimal)
                            _Dp.Amount = Amount
                        Catch ex As Exception
                            ErrorMessage.Append("Invalid Amount" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 6 'IDR
                        'Do Nothing
                    Case Is = 7
                        If sTemp.Trim <> String.Empty Then
                            Dim sRegNo As String = sTemp.Trim
                            Dim oDPHFac As New DailyPaymentHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            Dim oDPH As DailyPaymentHeader = oDPHFac.Retrieve(sRegNo)
                            If Not IsNothing(oDPH) AndAlso oDPH.ID > 0 Then
                                _Dp.DailyPaymentHeader = oDPH
                            Else
                                ErrorMessage.Append("Invalid Gyro Reg Number (Not Exist)" & Chr(13) & Chr(10))
                            End If
                        Else
                            'Remove by :Dna:20130205:for:Yurike:allow empty regnumber
                            'ErrorMessage.Append("Invalid Gyro Reg Number (Empty)" & Chr(13) & Chr(10))
                            'jika kosong, berarti insert dailypaymentheader
                            _Dp.DailyPaymentHeader = New DailyPaymentHeader
                        End If
                    Case Else
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next

            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            Else
                _DailyPayments.Add(_Dp)
            End If
        End Sub

#End Region

    End Class
End Namespace
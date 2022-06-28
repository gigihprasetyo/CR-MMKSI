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
    Public Class GyroParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _stream As StreamReader
        Private _grammar As Regex
        Private _ListCreditMaster As ArrayList
        Private _fileName As String
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            _grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _stream = New StreamReader(fileName, True)
            _ListCreditMaster = New ArrayList
            _fileName = fileName
            Dim val As String = MyBase.NextLine(_stream).Trim()
            While (Not val = "")
                Try
                    ParseGyro(val)
                Catch ex As Exception
                    'SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "FactoringParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.InvoiceParser, BlockName)
                    'Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_stream)
            End While

            If Not _stream Is Nothing Then
                _stream.Close()
                _stream = Nothing
            End If
            Return _ListCreditMaster
        End Function

        Public Function ParseFromString(ByVal sData As String) As DailyPayment
            _ListCreditMaster = New ArrayList
            ParseGyro(sData)
            If _ListCreditMaster.Count > 0 Then
                Return _ListCreditMaster(0)
            Else
                Return New DailyPayment
            End If
        End Function

        Private Sub ParseGyro(ByVal ValParser As String)
            Dim oDP As DailyPayment = New DailyPayment
            Dim Fields() As String = ValParser.Split(";")

            oDP.ErrorMessage = New String("")
            If Fields.Length >= 4 Then 'SONumber,SlipNumber,Amount,Status,EffectiveDate
                Dim oPOH As POHeader = Me.GetPOHeaderBySONumber(Fields(0))
                Dim Amount As Decimal = 0

                If IsNothing(oPOH) OrElse oPOH.ID < 1 Then
                    oDP.ErrorMessage = "SO Number tidak valid ( " & Fields(0) & " )"
                    oDP.POHeader = New POHeader
                Else
                    Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                    If oPOH.ContractHeader.Category.ProductCategory.Code.Trim <> companyCode AndAlso companyCode = "MMC" Then
                        oDP.ErrorMessage = "SO tidak terdapat pada Kategori Produk " & companyCode
                    End If
                    oDP.POHeader = oPOH
                End If

                'Add by anh 20110824
                Dim _dailypaymentFacade As New DailyPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Dim criterias As New CriteriaComposite(New Criteria(GetType(DailyPayment), "POHeader.SONumber", MatchType.Exact, Fields(0)))
                Dim objDP As DailyPayment = _dailypaymentFacade.Retrieve(criterias)(0)

                If oDP.ErrorMessage = "" Then

                    If (objDP.ID > 0) And (objDP.ReUpload = 0 And objDP.RemarkStatus > 0) Then
                        oDP.ErrorMessage = oDP.ErrorMessage & ";Tidak diijinkan untuk diupdate"
                    End If
                End If
                'end added by anh 20110824


                'oDP.IsCleared = IIf(Fields(1).ToUpper = "X", 1, 0)
                'oDP.IsReversed = IIf(Fields(2).ToUpper = "X", 1, 0)
                oDP.IsCleared = 1 'all uploaded data will treated as Clearing
                oDP.IsReversed = 0
                oDP.SlipNumber = Fields(1)
                If oDP.SlipNumber = "" Then
                    oDP.ErrorMessage = oDP.ErrorMessage & ";Slip number tidak boleh kosong"
                Else
                    Try
                        Dim spacePos As Integer = oDP.SlipNumber.IndexOf(" ")
                        If spacePos = -1 Then
                            'oDP.ErrorMessage = oDP.ErrorMessage & ";Format slip number tidak valid"
                        Else
                            Dim strBankCode As String = Left(oDP.SlipNumber, spacePos).Trim()
                            Dim objBank As Bank = New BankFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(strBankCode)
                            If objBank.ID = 0 Then
                                oDP.ErrorMessage = oDP.ErrorMessage & ";Kode Bank dalam slip number tidak valid"
                            End If
                        End If
                    Catch ex As Exception
                        oDP.ErrorMessage = oDP.ErrorMessage & ";Kode Bank dalam slip number tidak valid"
                    End Try

                End If

                Try
                    Amount = CType(Fields(2), Decimal)
                    If Amount < 0 Then
                        oDP.Amount = Amount
                        oDP.ErrorMessage &= IIf(oDP.ErrorMessage = "", "", ";") & "Nilai amount tidak boleh kurang dari 0"
                    Else
                        oDP.Amount = Amount
                    End If
                Catch ex As Exception
                    'Amount = 0
                    oDP.Amount = Amount
                    oDP.ErrorMessage &= IIf(oDP.ErrorMessage = "", "", ";") & "Format nilai tidak valid"
                End Try
                'oDP.Amount = Amount
                Try
                    Dim nStatus As Integer = CType(Fields(3), Integer)
                    If nStatus < 0 Or nStatus > 5 Then 'max of EnumPaymentRemarkStatus
                        oDP.RemarkStatus = nStatus
                        oDP.ErrorMessage &= IIf(oDP.ErrorMessage = "", "", ";") & "Status Pembayaran tidak valid"
                    Else
                        oDP.RemarkStatus = nStatus
                    End If
                Catch ex As Exception
                    oDP.RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.Cleared
                    oDP.ErrorMessage &= IIf(oDP.ErrorMessage = "", "", ";") & "Status Pembayaran tidak valid"
                End Try
                'Remarks by anh 20110822
                Try
                    'Dim sDate As String = Fields(4)
                    'Dim dED As DateTime = DateSerial(sDate.Substring(4, 4), sDate.Substring(2, 2), sDate.Substring(0, 2))
                    'oDP.EffectiveDate = dED
                    'If (objDP.ID > 0) And (objDP.RemarkStatus < 1) Then
                    oDP.EffectiveDate = Now
                    'End If

                Catch ex As Exception
                    oDP.EffectiveDate = Now
                    oDP.ErrorMessage &= IIf(oDP.ErrorMessage = "", "", ";") & "Effective Date tidak valid"
                End Try
                'End Remarks by anh 20110822


            Else
                oDP.ErrorMessage &= "Format Data tidak valid;"
            End If

            If oDP.ErrorMessage = "" Then
                Dim dpTemp As DailyPayment = Me.GetDPByKeys(oDP.POHeader.ID, oDP.SlipNumber)
                If Not IsNothing(dpTemp) AndAlso dpTemp.ID > 0 Then
                    dpTemp.Amount = oDP.Amount
                    dpTemp.IsCleared = oDP.IsCleared
                    dpTemp.IsReversed = oDP.IsReversed
                    dpTemp.RemarkStatus = oDP.RemarkStatus
                    If oDP.RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.PT _
                    OrElse oDP.RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.PTOffset _
                    OrElse oDP.RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.RejectPaid _
                    OrElse oDP.RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.Cleared Then
                        dpTemp.EffectiveDate = oDP.EffectiveDate
                    End If
                    oDP = dpTemp
                Else
                    oDP.ErrorMessage &= "Gyro Tidak Terdaftar;"
                End If
            End If
            _ListCreditMaster.Add(oDP)
        End Sub

        Protected Overrides Function DoTransaction() As Integer
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function
#Region "Private Methods"
        Private Function GetPOHeaderBySONumber(ByVal SONumber As String) As POHeader
            Dim oPOHFac As POHeaderFacade = New POHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim cPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim aPOH As New ArrayList

            cPOH.opAnd(New Criteria(GetType(POHeader), "SONumber", MatchType.Exact, SONumber))
            aPOH = oPOHFac.Retrieve(cPOH)
            If aPOH.Count > 0 Then
                Return aPOH(0)
            Else
                Return Nothing
            End If
        End Function

        Private Function GetDPByKeys(ByVal POHID As Integer, ByVal SlipNumber As String) As DailyPayment
            Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim cDP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim aDP As New ArrayList

            cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, POHID))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "SlipNumber", MatchType.Exact, SlipNumber))
            aDP = oDPFac.Retrieve(cDP)
            If aDP.Count > 0 Then
                Return aDP(0)
            Else
                Return Nothing
            End If
        End Function


#End Region
    End Class
End Namespace


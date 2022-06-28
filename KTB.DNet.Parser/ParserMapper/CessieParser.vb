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
    Public Class CessieParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _stream As StreamReader
        Private _grammar As Regex
        Private _ListCessie As ArrayList
        Private _fileName As String
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            _grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _stream = New StreamReader(fileName, True)
            _ListCessie = New ArrayList
            _fileName = fileName
            Dim val As String = MyBase.NextLine(_stream).Trim()
            While (Not val = "")
                Try
                    ParseCessie(val)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "CessieParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.InvoiceParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_stream)
            End While

            If Not _stream Is Nothing Then
                _stream.Close()
                _stream = Nothing
            End If
            Return _ListCessie
        End Function

        Private Sub ParseCessie(ByVal ValParser As String)
            Dim objCM As New Cessie
            Dim objVCAFac As v_CreditAccountFacade = New v_CreditAccountFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim objVCA As v_CreditAccount
            Dim Fields() As String
            Dim errorSb As StringBuilder = New StringBuilder
            Dim strCreditAccount As String = ""
            Dim nPaymentType As Integer = 0
            Dim Plafon As Decimal = 0
            Dim OutStanding As Decimal = 0

            Fields = ValParser.Split(";")
            If Fields.Length = 8 Then
                'Cessie Number'AdminFee'Selisih
                objCM.CessieNumber = Fields(0)
                'Cessie Date
                Dim sDate As String = Fields(1)
                Try
                    Dim _date As Date = New Date(sDate.Substring(4, 4), sDate.Substring(2, 2), sDate.Substring(0, 2))
                    objCM.CessieDate = _date
                Catch ex As Exception
                    errorSb.Append("Invalid Cessie Date" & Chr(13) & Chr(10))
                End Try
                'Piutang
                Try
                    Dim dPiutang As Decimal

                    dPiutang = CDec(Fields(2))
                    objCM.Amount = dPiutang
                Catch ex As Exception
                    errorSb.Append("Invalid Amount" & Chr(13) & Chr(10))
                End Try
                'Tgl Pembayaran
                sDate = Fields(3)
                Try
                    Dim _date As Date = New Date(sDate.Substring(4, 4), sDate.Substring(2, 2), sDate.Substring(0, 2))
                    objCM.PaymentDate = _date
                Catch ex As Exception
                    errorSb.Append("Invalid Payment Date" & Chr(13) & Chr(10))
                End Try
                'Nilai Pembayaran
                Try
                    Dim dPayment As Decimal = CDec(Fields(4))
                    objCM.PurchaseAmount = dPayment
                Catch ex As Exception
                    errorSb.Append("Invalid Purchase amount" & Chr(13) & Chr(10))
                End Try
                'AdminFee
                Try
                    Dim dAdminFee As Decimal = CDec(Fields(5))
                    objCM.AdminFee = dAdminFee
                Catch ex As Exception
                    errorSb.Append("Invalid AdminFee amount" & Chr(13) & Chr(10))
                End Try
                'Selisih
                Try
                    Dim dDifference As Decimal = CDec(Fields(6))
                    objCM.DifferenceAmount = dDifference
                Catch ex As Exception
                    errorSb.Append("Invalid Difference amount" & Chr(13) & Chr(10))
                End Try
                'ProductCategory
                Try
                    Try
                        Dim oPC As ProductCategory
                        Dim oPCFac As New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        Dim strCompanyCode As String
                        If Fields(7).ToString().ToLower() = "mmksi" OrElse Fields(7).ToString().ToLower() = "mmc" Then
                            strCompanyCode = "mmc"
                        Else
                            strCompanyCode = "mftbc"
                        End If
                        oPC = oPCFac.Retrieve(strCompanyCode)
                        If IsNothing(oPC) OrElse oPC.ID < 1 Then
                            errorSb.Append("Invalid Product" & Chr(13) & Chr(10))
                        Else
                            objCM.ProductCategory = oPC
                        End If
                    Catch ex As Exception
                        errorSb.Append("Invalid Product" & Chr(13) & Chr(10))
                    End Try
                Catch ex As Exception
                    errorSb.Append("Invalid AdminFee amount" & Chr(13) & Chr(10))
                End Try
            Else
                errorSb.Append("Struktur Data tidak valid" & Chr(13) & Chr(10))
            End If
            If errorSb.Length > 0 Then
                Throw New Exception(errorSb.ToString)
            Else
                _ListCessie.Add(objCM)
            End If
        End Sub

        Protected Overrides Function DoTransaction() As Integer
            If _ListCessie.Count > 0 Then
                Dim objCMFac As CessieFacade = New CessieFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Dim objCMExisting As Cessie
                Dim IsExisting As Boolean
                Dim IsSameWithOld As Boolean = True

                For Each objCM As Cessie In _ListCessie
                    IsExisting = False

                    objCMExisting = objCMFac.Retrieve(objCM.CessieNumber)
                    If Not IsNothing(objCMExisting) AndAlso objCMExisting.ID > 0 Then IsExisting = True
                    Try
                        If Not IsExisting Then
                            objCMFac.Insert(objCM)
                        Else
                            If objCMExisting.CessieDate <> objCM.CessieDate Then
                                objCMExisting.CessieDate = objCM.CessieDate
                                IsSameWithOld = False
                            End If
                            If objCMExisting.Amount <> objCM.Amount Then
                                objCMExisting.Amount = objCM.Amount
                                IsSameWithOld = False
                            End If
                            If objCMExisting.PaymentDate <> objCM.PaymentDate Then
                                objCMExisting.PaymentDate = objCM.PaymentDate
                                IsSameWithOld = False
                            End If
                            If objCMExisting.PurchaseAmount <> objCM.PurchaseAmount Then
                                objCMExisting.PurchaseAmount = objCM.PurchaseAmount
                                IsSameWithOld = False
                            End If
                            If objCMExisting.AdminFee <> objCM.AdminFee Then
                                objCMExisting.AdminFee = objCM.AdminFee
                                IsSameWithOld = False
                            End If
                            If objCMExisting.DifferenceAmount <> objCM.DifferenceAmount Then
                                objCMExisting.DifferenceAmount = objCM.DifferenceAmount
                                IsSameWithOld = False
                            End If
                            If objCMExisting.ProductCategory.ID <> objCM.ProductCategory.ID Then
                                objCMExisting.ProductCategory = objCM.ProductCategory
                                IsSameWithOld = False
                            End If
                            If Not IsSameWithOld Then
                                objCMFac.Update(objCMExisting)
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "Cessie", "CessieParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.CessieParser, BlockName)
                        Dim e1 As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objCM.CessieNumber & Chr(13) & Chr(10) & ex.Message)
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


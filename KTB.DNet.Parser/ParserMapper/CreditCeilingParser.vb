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
    Public Class CreditCeilingParser
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
                    ParseCreditMaster(val)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "CreditCeilingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.InvoiceParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_stream)
            End While

            If Not _stream Is Nothing Then
                _stream.Close()
                _stream = Nothing
            End If
            Return _ListCreditMaster
        End Function

        Private Sub ParseCreditMaster(ByVal ValParser As String)
            Dim objCM As New CreditMaster
            Dim objVCAFac As v_CreditAccountFacade = New v_CreditAccountFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim objVCA As v_CreditAccount
            Dim Fields() As String
            Dim errorSb As StringBuilder = New StringBuilder
            Dim strCreditAccount As String = ""
            Dim nPaymentType As Integer = 0
            Dim Plafon As Decimal = 0
            Dim OutStanding As Decimal = 0
            Dim PCCode As String
            Dim oPC As ProductCategory
            Dim oPCFac As New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))


            Fields = ValParser.Split(";")
            If Fields.Length = 5 Then
                'Credit Account
                objVCA = objVCAFac.Retrieve(Fields(0))
                If objVCA Is Nothing Then
                    errorSb.Append("Credit Account tidak terdaftar" & Chr(13) & Chr(10))
                Else
                    If objVCA.ID < 1 Then
                        errorSb.Append("Credit Account tidak terdaftar" & Chr(13) & Chr(10))
                    Else
                        objCM.CreditAccount = objVCA.CreditAccount
                    End If
                End If
                'Payment Type
                nPaymentType = (New enumPaymentType).GetEnumValue(Fields(1))
                If nPaymentType = 0 Then
                    errorSb.Append("Tipe Pembayaran tidak valid" & Chr(13) & Chr(10))
                Else
                    objCM.PaymentType = nPaymentType
                End If
                'Plafon or Ceiling
                Try
                    Plafon = CDec(Fields(2))
                    objCM.Plafon = Plafon
                Catch ex As Exception
                    errorSb.Append("Nilai ceiling tidak valid" & Chr(13) & Chr(10))
                End Try
                'Outstanding
                Try
                    OutStanding = CDec(Fields(3))
                    objCM.OutStanding = OutStanding
                Catch ex As Exception
                    errorSb.Append("Nilai Outstanding tidak valid" & Chr(13) & Chr(10))
                End Try

                'ProductCategory
                Try
                    PCCode = (Fields(4))
                    oPC = oPCFac.Retrieve(PCCode)
                    objCM.ProductCategory = oPC
                Catch ex As Exception
                    errorSb.Append("Produk Kendaraan Tidak Valid" & Chr(13) & Chr(10))
                End Try
            Else
                errorSb.Append("Struktur Data tidak valid" & Chr(13) & Chr(10))
            End If

            If errorSb.Length > 0 Then
                Throw New Exception(errorSb.ToString)
            Else
                _ListCreditMaster.Add(objCM)
            End If

        End Sub

        Protected Overrides Function DoTransaction() As Integer
            If _ListCreditMaster.Count > 0 Then
                Dim objCMFac As CreditMasterFacade = New CreditMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Dim objCMExisting As CreditMaster
                Dim IsExisting As Boolean
                Dim IsSameWithOld As Boolean = True
                Dim objFMFac As FactoringMasterFacade = New FactoringMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Dim objFMExisting As FactoringMaster
                Dim oFMNew As FactoringMaster

                For Each objCM As CreditMaster In _ListCreditMaster
                    IsExisting = False

                    objCMExisting = objCMFac.Retrieve(objCM.ProductCategory, objCM.CreditAccount, objCM.PaymentType)
                    If Not objCMExisting Is Nothing Then
                        If objCMExisting.ID > 0 Then
                            IsExisting = True
                        End If
                    End If
                    Try
                        If Not IsExisting Then
                            objCMFac.Insert(objCM)
                        Else
                            'objCMExisting.CreditAccount = objCM.CreditAccount
                            If objCMExisting.Plafon <> objCM.Plafon Then
                                objCMExisting.Plafon = objCM.Plafon
                                IsSameWithOld = False
                            End If
                            If objCMExisting.OutStanding <> objCM.OutStanding Then
                                objCMExisting.OutStanding = objCM.OutStanding
                                IsSameWithOld = False
                            End If
                            If Not IsSameWithOld Then
                                objCMFac.Update(objCMExisting)
                            End If
                        End If
                        'Syncronize with FactoringMaster
                        objFMExisting = objFMFac.Retrieve(objCM.ProductCategory, objCM.CreditAccount)
                        If IsNothing(objFMExisting) OrElse objFMExisting.ID < 1 Then
                            oFMNew = New FactoringMaster
                            oFMNew.CreditAccount = objCM.CreditAccount
                            oFMNew.TotalCeiling = 0
                            oFMNew.StandardCeiling = 0
                            oFMNew.FactoringCeiling = 0
                            oFMNew.GiroTolakan = 0
                            oFMNew.Outstanding = 0
                            oFMNew.AvailableCeiling = 0
                            oFMNew.Status = 1
                            oFMNew.MaxTOPDate = DateSerial(1900, 1, 1)
                            oFMNew.ProductCategory = objCM.ProductCategory
                            oFMNew.ID = objFMFac.Insert(oFMNew)
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "CreditCeiling", "CreditCeilingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.CreditCeilingParser, BlockName)
                        Dim e1 As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objCM.CreditAccount & Chr(13) & Chr(10) & ex.Message)
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


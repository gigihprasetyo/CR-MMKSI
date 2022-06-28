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
    Public Class FactoringParser
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

        Private Sub ParseCreditMaster(ByVal ValParser As String)
            Dim objFM As New FactoringMaster
            Dim objVCAFac As v_CreditAccountFacade = New v_CreditAccountFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim objVCA As v_CreditAccount
            Dim Fields() As String
            Dim errorSb As StringBuilder = New StringBuilder
            Dim strCreditAccount As String = ""
            Dim nPaymentType As Integer = 0
            Dim Plafon As Decimal = 0
            Dim GiroTolakan As Decimal = 0
            Dim OutStanding As Decimal = 0
            Dim AvCeiling As Decimal = 0
            Dim TotalCeiling As Decimal = 0
            Dim StandardCeiling As Decimal = 0


            Fields = ValParser.Split(";")
            '--for dsf file
            If Fields.Length = 5 Then
                'Credit Account
                objVCA = objVCAFac.Retrieve(Fields(0))
                If objVCA Is Nothing Then
                    objFM.ErrorMessage &= "Credit Account tidak terdaftar;"
                Else
                    Dim oPC As ProductCategory
                    Try
                        Dim oPCFac As New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        oPC = oPCFac.Retrieve(CType(Fields(4), Integer))
                        If IsNothing(oPC) OrElse oPC.ID < 1 Then
                            objFM.ErrorMessage &= "Produk tidak valid;"
                        Else
                            objFM.ProductCategory = oPC
                        End If
                    Catch ex As Exception
                        objFM.ErrorMessage &= "Produk tidak valid;"
                    End Try

                    If objVCA.ID < 1 Then
                        objFM.ErrorMessage &= "Credit Account tidak terdaftar;"
                    Else
                        objFM.CreditAccount = objVCA.CreditAccount
                        Dim oFMTemp As FactoringMaster = New FactoringMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(oPC, objFM.CreditAccount)
                        If Not IsNothing(oFMTemp) AndAlso oFMTemp.ID > 0 Then
                            objFM.Status = oFMTemp.Status
                            objFM.MaxTOPDate = oFMTemp.MaxTOPDate
                        End If
                        If CDec(Fields(1)) > oFMTemp.StandardCeiling Then
                            objFM.ErrorMessage &= "Nilai Factoring Ceiling lebih besar dari Standard ;"
                        End If
                    End If
                End If
                'Plafon or Ceiling
                Try
                    Plafon = CDec(Fields(1))
                    objFM.FactoringCeiling = Plafon
                Catch ex As Exception
                    objFM.ErrorMessage &= "Nilai ceiling tidak valid;"
                End Try
                ''Giro Tolakan
                'Try
                '    GiroTolakan = CDec(Fields(2))
                '    objFM.GiroTolakan = GiroTolakan
                'Catch ex As Exception
                '    objFM.ErrorMessage &= "Nilai giro tolakan tidak valid;"
                'End Try
                'Outstanding
                Try
                    OutStanding = CDec(Fields(2))
                    objFM.Outstanding = OutStanding
                Catch ex As Exception
                    objFM.ErrorMessage &= "Nilai Outstanding tidak valid;"
                End Try
                'Available Ceiling
                Try
                    AvCeiling = CDec(Fields(3))
                    objFM.AvailableCeiling = AvCeiling
                Catch ex As Exception
                    objFM.ErrorMessage &= "Nilai Available Ceiling tidak valid;"
                End Try
                'Product Category
                'taruh di atas utk keperluan pencarian factoring master

                If objFM.FactoringCeiling < objFM.Outstanding Then
                    objFM.ErrorMessage &= "Over Ceiling;"
                End If
            ElseIf Fields.Length = 4 Then
                'Credit Account
                objVCA = objVCAFac.Retrieve(Fields(0))


                If objVCA Is Nothing Then
                    objFM.ErrorMessage &= "Credit Account tidak terdaftar;"
                Else
                    Dim sProduct As String = Fields(3)
                    Dim oPCFac As New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    Dim oPC As ProductCategory = opcfac.Retrieve(sProduct)
                    Try
                        If IsNothing(opc) OrElse opc.ID < 1 Then
                            objFM.ErrorMessage &= "Kode Produk Tidak Valid;"
                        Else
                            objFM.ProductCategory = oPC
                        End If
                    Catch ex As Exception
                        objFM.ErrorMessage &= "Produk tidak valid;"
                    End Try
                    If Not IsNothing(oPC) AndAlso oPC.ID > 0 Then
                        If objVCA.ID < 1 Then
                            objFM.ErrorMessage &= "Credit Account tidak terdaftar;"
                        Else
                            objFM.CreditAccount = objVCA.CreditAccount
                            Dim oFMTemp As FactoringMaster = New FactoringMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(oPC, objFM.CreditAccount)
                            If Not IsNothing(oFMTemp) AndAlso oFMTemp.ID > 0 Then
                                objFM.Status = oFMTemp.Status
                                objFM.MaxTOPDate = oFMTemp.MaxTOPDate
                            End If
                        End If
                    End If
                End If
                Try
                    TotalCeiling = CDec(Fields(1))
                    objFM.TotalCeiling = TotalCeiling
                Catch ex As Exception
                    objFM.ErrorMessage &= "Nilai Total Ceiling tidak valid;"
                End Try
                Try
                    StandardCeiling = CDec(Fields(2))
                    objFM.StandardCeiling = StandardCeiling
                Catch ex As Exception
                    objFM.ErrorMessage &= "Nilai Standard Ceiling tidak valid;"
                End Try
                If TotalCeiling < StandardCeiling Then
                    objFM.ErrorMessage &= "Nilai Standard Ceiling lebih besar dari nilai Total Ceiling;"
                End If
            Else
                objFM.ErrorMessage &= "Produk tidak valid;"
            End If


            If (objFM.FactoringCeiling - objFM.Outstanding <> objFM.AvailableCeiling) Or (objFM.FactoringCeiling < objFM.Outstanding) Then
                objFM.ErrorMessage &= "Nilai Ceiling tidak valid"
            End If

            _ListCreditMaster.Add(objFM)
        End Sub

        Protected Overrides Function DoTransaction() As Integer
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function
    End Class
End Namespace


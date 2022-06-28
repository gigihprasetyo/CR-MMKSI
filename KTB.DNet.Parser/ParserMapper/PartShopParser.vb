#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Text
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
#End Region

Namespace KTB.DNet.Parser

    Public Class PartShopParser
        Inherits AbstractParser

#Region "Private Variables"
        Private errMessage As String
        Private status As String
        Private _Stream As StreamReader
        Private arlPartShop As ArrayList
        Private Grammar As Regex
        Private _sessHelper As SessionHelper = New SessionHelper
        Private _Dealer As KTB.DNet.Domain.Dealer ' Akan diisi dengan kode dealer session
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser(",")
        End Sub


#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            arlPartShop = New ArrayList
            Dim val As String = MyBase.NextLine(_Stream).Trim()

            While (Not val = "")
                Try
                    'ParsePartShop(val + delimited)
                    ParsePartShop(val)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "PartShopParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PartShopParser, BlockName)
                End Try

                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return arlPartShop
        End Function

        Protected Overrides Function DoTransaction() As Integer
            If arlPartShop.Count > 0 Then
                'Do Business - Like save to database
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub ParsePartShop(ByVal ValParser As String)

            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim mUser As IPrincipal
            Dim arrOfSpllitedRow() As String
            Dim objPartShop As PartShop = New PartShop
            Dim objPartShopFacade As PartShopFacade = New PartShopFacade(mUser)
            Dim objCityPartFacade As CityPartFacade = New CityPartFacade(mUser)


            sStart = 0
            nCount = 0

            arrOfSpllitedRow = ValParser.Split(";")

            If arrOfSpllitedRow.Length <> 7 Then
                objPartShop.ErrorMessage = "Format Data tidak lengkap"
            Else
                'Dealer
                If arrOfSpllitedRow(0).Trim = String.Empty Then
                    objPartShop.ErrorMessage &= "Kode Dealer tidak boleh kosong" & "<br /> &nbsp;"
                Else
                    Dim objDealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(arrOfSpllitedRow(0).Trim)
                    If objDealer.ID <> _Dealer.ID Then
                        objPartShop.Dealer = objDealer
                        objPartShop.ErrorMessage &= "Kode Dealer tidak sesuai dengan kode dealer anda" & "<br /> &nbsp;"
                    Else
                        objPartShop.Dealer = objDealer
                    End If

                End If

                'Name
                If arrOfSpllitedRow(1).Trim = String.Empty Then
                    objPartShop.ErrorMessage &= "Nama Part Shop tidak boleh kosong" & "<br /> &nbsp;"
                Else
                    objPartShop.Name = arrOfSpllitedRow(1).Trim
                End If

                'Address
                If arrOfSpllitedRow(2).Trim = String.Empty Then
                    objPartShop.ErrorMessage &= "Alamat tidak boleh kosong" & "<br /> &nbsp;"
                Else
                    objPartShop.Address = arrOfSpllitedRow(2).Trim
                End If

                'City
                If arrOfSpllitedRow(3).Trim = String.Empty Then
                    objPartShop.ErrorMessage &= "Nama Kota tidak boleh kosong" & "<br /> &nbsp;"
                Else
                    Dim objCityPart As CityPart = objCityPartFacade.Retrieve(arrOfSpllitedRow(3).Trim, 1)
                    If objCityPart Is Nothing Then
                        objPartShop.ErrorMessage &= "Nama Kota tidak terdaftar" & "<br /> &nbsp;"
                    Else
                        objPartShop.CityPart = objCityPart
                    End If
                End If

                'Province
                'If arrOfSpllitedRow(3).Trim = String.Empty Then
                'objPartShop.ErrorMessage &= "Provinsi tidak boleh kosong" & "<br /> &nbsp;"
                'Else
                'objPartShop.Address = arrOfSpllitedRow(1).Trim
                'End If

                'Phone
                If arrOfSpllitedRow(5).Trim = String.Empty Then
                    objPartShop.ErrorMessage &= "Nomor Telp tidak boleh kosong" & "<br /> &nbsp;"
                Else
                    objPartShop.Phone = arrOfSpllitedRow(5).Trim
                End If
                'Fax
                If arrOfSpllitedRow(6).Trim = String.Empty Then
                    'objPartShop.ErrorMessage &= "Kode dealer tidak boleh kosong" & "<br /> &nbsp;"
                Else
                    objPartShop.Fax = arrOfSpllitedRow(6).Trim
                End If

                objPartShop.Status = EnumPartShopStatus.PartShopStatus.Baru
                objPartShop.RowStatus = CType(DBRowStatus.Active, Short)

            End If

            arlPartShop.Add(objPartShop)
        End Sub
#End Region

#Region "Public method"
        Public Function IsAllowToSave() As Boolean
            If errMessage = String.Empty Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Property Dealer() As Dealer
            Get
                Return _Dealer
            End Get
            Set(ByVal Value As Dealer)
                _Dealer = Value
            End Set
        End Property

#End Region

    End Class

End Namespace
#Region "Summary"
'// ===========================================================================		
'// Author Name   : Heru
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 11/10/2005
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports System.Security.Principal
#End Region

Namespace KTB.DNet.Parser
    Public Class SparePartPOParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _sparePartPO As SparePartPO
        Private _companyCode As String
        Private _strPQRNo As String = String.Empty
#End Region

#Region "Constructors/Destructors/Finalizers"
        Public Sub New()
        End Sub

        Public Sub New(ByVal companyCode As String)
            Me._companyCode = companyCode
        End Sub
#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
        End Function

        Protected Overrides Function DoTransaction() As Integer
            '-- Do Business - Like saveTo Database
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            '-- Parse fix-formatted text file

            _sparePartPO = New SparePartPO

            Try
                _Stream = New StreamReader(fileName, True)  '-- Open stream
                Dim StreamLine As String = MyBase.NextLine(_Stream).Trim()  '-- Header line

                If StreamLine <> "" Then

                    ParseSPPOHeader(StreamLine)  '-- Parse header line
                    StreamLine = MyBase.NextLine(_Stream).Trim()  '-- First detail line

                    Do While StreamLine <> ""

                        ParseSPPODetail(StreamLine)  '-- Parse detail line
                        StreamLine = MyBase.NextLine(_Stream).Trim()  '-- Next detail line
                    Loop
                End If

                _Stream.Close()  '-- Close & dispose stream
                _Stream = Nothing

            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SparePartPOParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SparePartPOParser, BlockName)
                _sparePartPO = Nothing  '-- If any error occurs then set with Nothing
            End Try

            Return _sparePartPO  '-- Successfully read text file, so return it
        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseSPPOHeader(ByVal streamLine As String)
            '-- Parse PO header line

            Dim poNumber As String = streamLine.Substring(1, 15).Trim()
            _sparePartPO.PONumber = poNumber  '-- PO number
            _sparePartPO.OrderType = poNumber.Substring(0, 1).ToUpper  '-- Order type

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Dealer), "SearchTerm2", MatchType.Exact, poNumber.Substring(1, 4)))
            Dim al As ArrayList = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

            If al.Count > 0 Then
                _sparePartPO.Dealer = CType(al(0), Dealer)  '-- Dealer object
            Else
                _sparePartPO.ErrorMessage &= "Dealer tidak valid;"
            End If

            Dim _date As String = streamLine.Substring(41, 8)
            Try
                '-- PO date
                _sparePartPO.PODate = New Date(_date.Substring(0, 4), _date.Substring(4, 2), _date.Substring(6, 2))
                If _sparePartPO.PODate.Year < Now.Year - 1 OrElse _sparePartPO.PODate.Year > Now.Year + 1 Then
                    _sparePartPO.ErrorMessage &= "Tahun tidak valid;"
                End If
            Catch ex As Exception
                _sparePartPO.ErrorMessage &= "Tgl tidak valid;"
            End Try

            If _sparePartPO.OrderType = "P" Then
                _strPQRNo = streamLine.Substring(49, Len(streamLine) - 49)
                If _strPQRNo.Trim = "" Then
                    _sparePartPO.ErrorMessage &= "Nomor PQR harap diisi;"
                Else
                    Dim objPQRHeader As PQRHeader = New PQRHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(_strPQRNo.Trim)
                    If IsNothing(objPQRHeader) Then
                        _sparePartPO.ErrorMessage &= "Nomor PQR tidak valid;"
                    Else
                        If objPQRHeader.ID <= 0 Then
                            _sparePartPO.ErrorMessage &= "Nomor PQR tidak valid;"
                        Else
                            _sparePartPO.PQRHeader = objPQRHeader
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub ParseSPPODetail(ByVal streamLine As String)
            '-- Parse PO detail line

            Dim bDetailValid As Boolean = True

            Dim _sparePartPODetail As SparePartPODetail = New SparePartPODetail

            '-- For validation only. Check date position!
            Dim _date As String = streamLine.Substring(41, 8)
            Try
                '-- Convertion test
                Dim dummyDate As Date = New Date(_date.Substring(0, 4), _date.Substring(4, 2), _date.Substring(6, 2))
                '-- Then compare with its header date! They must be equal
                If dummyDate <> _sparePartPO.PODate Then
                    _sparePartPODetail.ErrorMessage &= "Data tidak valid;"
                Else
                    If dummyDate.Year < Now.Year - 1 OrElse dummyDate.Year > Now.Year + 1 Then
                        _sparePartPODetail.ErrorMessage &= "Tahun tidak valid;"
                    End If
                End If
            Catch ex As Exception
                _sparePartPODetail.ErrorMessage &= "Data tidak valid;"
            End Try

            Dim partNumber As String = streamLine.Substring(16, 18).Trim()  '-- Part number

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, partNumber))
            Dim partColl As ArrayList = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

            If partColl.Count < 1 Then
                _sparePartPODetail.NotExistPartNumber = partNumber
                _sparePartPODetail.ErrorMessage &= "No. Part tdk ada;"
            Else
                Dim objSparepartMaster As SparePartMaster = CType(partColl(0), SparePartMaster)
                _sparePartPODetail.RetailPrice = CType(partColl(0), SparePartMaster).RetalPrice  '-- Retail price
                _sparePartPODetail.SparePartMaster = CType(partColl(0), SparePartMaster)  '-- Sparepart master object

                If Me._companyCode.Trim.ToUpper <> objSparepartMaster.ProductCategory.Code.ToUpper Then
                    _sparePartPODetail.ErrorMessage &= "No. Part tdk ada di " & Me._companyCode.Trim.ToUpper & ";"
                End If

                If _sparePartPODetail.SparePartMaster.ActiveStatus <> CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short) Then
                    _sparePartPODetail.ErrorMessage &= "No.Part tidak aktif"
                End If

                Try
                    Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                    If _sparePartPODetail.SparePartMaster.ProductCategory.Code.Trim.ToUpper <> companyCode.Trim.ToUpper Then
                        If companyCode.ToUpper() = "MFTBC" Then
                            companyCode = "KTB"
                        End If

                        If companyCode.ToUpper() = "MMC" Then
                            companyCode = "MMKSI"
                        End If
                        _sparePartPODetail.ErrorMessage = "Sparepart tidak ada untuk produk " & companyCode
                    End If
                Catch ex As Exception

                End Try

                'New LOC 
                'DATE : 2014 -08 -15
                'On behalf VAlidasi I,E, & A
                'By : Ali
                If (_sparePartPODetail.SparePartMaster.TypeCode.ToUpper = "I" OrElse _sparePartPODetail.SparePartMaster.TypeCode.ToUpper = "E" OrElse _sparePartPODetail.SparePartMaster.TypeCode.ToUpper = "A") Then
                    _sparePartPODetail.ErrorMessage &= "Untuk Sparepart dengan Type I,E dan A harap dipesan lewat menu Indent Part;"
                End If
                ' END OF NEW LOC

                'start add by anh 2017-02-01 req by Maria Anna
                If _sparePartPODetail.SparePartMaster.TypeCode = "P" Then
                    _sparePartPODetail.ErrorMessage &= "Untuk Sparepart dengan Type P harap hubungi Customer Support;"
                End If
                'end add by anh 2017-02-01 req by Maria Anna

                If _sparePartPO.OrderType = "P" Then    ''Jika type Ordernya dari Emergency PQR 
                    Dim intPQRHeaderID As Integer = 0
                    If _strPQRNo.Trim <> "" Then
                        Dim objPQRHeader As PQRHeader = New PQRHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(_strPQRNo.Trim)
                        If Not IsNothing(objPQRHeader) Then
                            If objPQRHeader.ID > 0 Then
                                intPQRHeaderID = objPQRHeader.ID
                                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                crit.opAnd(New Criteria(GetType(SparePartMaster), "ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))
                                crit.opAnd(New Criteria(GetType(SparePartMaster), "TypeCode", MatchType.NotInSet, "'I','E','A'"))
                                If intPQRHeaderID > 0 Then
                                    Dim strSQL As String = "SELECT Distinct SparePartMasterID FROM PQRPartsCode WHERE PQRHeaderID = " & intPQRHeaderID
                                    crit.opAnd(New Criteria(GetType(SparePartMaster), "ID", MatchType.InSet, "(" + strSQL + ")"))
                                End If
                                crit.opAnd(New Criteria(GetType(SparePartMaster), "ID", MatchType.Exact, objSparepartMaster.ID))
                                Dim arrSparePartMaster As ArrayList = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(crit)
                                If Not IsNothing(arrSparePartMaster) AndAlso arrSparePartMaster.Count > 0 Then
                                Else
                                    _sparePartPODetail.ErrorMessage &= "No. Part yang diinput tidak sesuai dengan No. Part pada PQR"
                                End If
                            Else
                                _sparePartPODetail.ErrorMessage &= "No. Part yang diinput tidak sesuai dengan No. Part pada PQR"
                            End If
                        End If
                    End If
                End If

                '-- Check if this already exists in preceeding lines
                If ExistPart(_sparePartPODetail.SparePartMaster.PartNumber) Then
                    _sparePartPODetail.ErrorMessage &= "No. Part sudah ada;"
                End If
            End If

            Try
                Dim qty As Integer = CType(streamLine.Substring(36, 5), Integer)
                _sparePartPODetail.Quantity = qty  '-- Quantity
                If _sparePartPODetail.Quantity <= 0 Then
                    bDetailValid = False  '-- Skip this SPPO detail
                End If
            Catch ex As Exception
                _sparePartPODetail.ErrorMessage &= "Quantity error;"
                bDetailValid = False  '-- Skip this SPPO detail
            End Try

            If bDetailValid Then
                _sparePartPODetail.SparePartPO = _sparePartPO  '-- Its parent
                _sparePartPO.SparePartPODetails.Add(_sparePartPODetail)  '-- Add detail line
            End If

        End Sub

        Private Function ExistPart(ByVal partNumber As String)
            '-- Check if PO detail already exists in preceeding line(s)

            For Each objPoDetail As SparePartPODetail In _sparePartPO.SparePartPODetails
                If Not IsNothing(objPoDetail.SparePartMaster) Then

                    If objPoDetail.SparePartMaster.PartNumber.Trim.ToUpper = partNumber.Trim.ToUpper Then
                        Return True  '-- Exists
                    End If
                End If
            Next

            Return False  '-- Not exist
        End Function

#End Region

#Region "Public Properties"

        ReadOnly Property SparePartPurchaseOrder() As SparePartPO
            Get
                Return _sparePartPO
            End Get
        End Property

#End Region

    End Class

End Namespace

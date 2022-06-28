#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.PO
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Security.Principal
#End Region

Namespace KTB.DNet.Parser

    Public Class PKAlocationParser
        Inherits AbstractParser

     
#Region "Private Variables"
        Private _Stream As StreamReader
        Private _PKAloctionList As ArrayList
        Private Grammar As Regex
        Private _fileName As String
        Private MatNumber As String = "DEFAULT"
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            'Grouping -> dalam satau text file periodeMonth;periodeYear;productionyear sama
            'Grouping berdasarkan material yang sama(di Text disort)
            'Ngga boleh dupilikat material dan pk regnumber
            _Stream = New StreamReader(fileName, True)
            _PKAloctionList = New ArrayList
            Dim splitedText() As String
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            Dim partialCollection As ArrayList = New ArrayList
            While (Not val = "")
                'splitedText = val.Trim.Split(";")
                'If splitedText.Length <> 6 Then
                '    Throw New Exception("Format File Salah")
                '    Exit Function
                'End If
                'If Not (splitedText(3).Trim.ToUpper = MatNumber Or MatNumber.Trim.ToUpper = "DEFAULT") Then
                '    _PKAloctionList.Add(partialCollection)
                '    partialCollection = New ArrayList
                'End If
                'Dim pkdet As PKDetail = ParsePKAlocationParser(splitedText, partialCollection)
                'partialCollection.Add(pkdet)
                'MatNumber = splitedText(3).Trim.ToUpper
                Try
                    splitedText = val.Trim.Split(";")
                    If splitedText.Length <> 6 Then
                        Throw New Exception("Format File Salah")
                        Exit Function
                    End If
                    If Not (splitedText(3).Trim.ToUpper = MatNumber Or MatNumber.Trim.ToUpper = "DEFAULT") Then
                        _PKAloctionList.Add(partialCollection)
                        partialCollection = New ArrayList
                    End If
                    Dim pkdet As PKDetail = ParsePKAlocationParser(splitedText, partialCollection)
                    partialCollection.Add(pkdet)
                    MatNumber = splitedText(3).Trim.ToUpper
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "PKAlocationParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PKAlocationParser, BlockName)
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            _PKAloctionList.Add(partialCollection)
            Return _PKAloctionList
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            'Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            'Nothing
        End Function

#End Region

#Region "Private Methods"

        Private Function ParsePKAlocationParser(ByVal splitedText() As String, ByVal coll As ArrayList) As PKDetail
            'periodeMonth;periodeYear;productionyear;materialNuber,pkNumber;qtyallocate
            Dim _pkDetail As PKDetail
            _pkDetail = GetPKDetails(splitedText(0).Trim, splitedText(1).Trim, splitedText(2).Trim, splitedText(3).Trim, splitedText(4).Trim, splitedText(5).Trim)
            If _pkDetail.ID > 0 Then
                For Each item As PKDetail In coll
                    If item.ID > 0 Then
                        If item.VechileColor.ID = _pkDetail.VechileColor.ID AndAlso item.PKHeader.ID = _pkDetail.PKHeader.ID Then
                            _pkDetail.ErrorMessage = _pkDetail.ErrorMessage & "Duplikasi Material Number <br>"
                            Return _pkDetail
                        End If
                    End If
                Next
            End If
            Return _pkDetail
        End Function

        Private Function GetPKDetails(ByVal pMonth As String, ByVal pYear As String, ByVal ProdYear As String, ByVal materialNumber As String, ByVal pkNumber As String, ByVal qty As String) As PKDetail
            Dim _pkDetail As PKDetail = New PKDetail
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim month As Integer = 0
            Dim year As Integer = 0
            Dim productionYear As Integer = 0
            Dim allocQty As Integer = 0

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.PKNumber", MatchType.Exact, pkNumber.Trim))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "VechileColor.MaterialNumber", MatchType.Exact, materialNumber.Trim))
            Dim PKColl As ArrayList = New PKDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            'Cek Format Text
            Try
                month = CType(pMonth, Integer)
                year = CType(pYear, Integer)
                productionYear = CType(ProdYear, Integer)
                allocQty = CType(qty, Integer)
            Catch ex As Exception
                _pkDetail.ErrorMessage = _pkDetail.ErrorMessage & "Nilai Integer Tidak Valid<br>"
            End Try
            If PKColl.Count > 0 Then
                _pkDetail = CType(PKColl(0), PKDetail)

                'Validasi
                '1. Cek Pk dng status confirm
                '2. peridemonth sama 
                '3. periodeYear sama
                '4. Production Year sama
                '5. AllocQuanity dari text harus <= targetQuantity 
                Dim _pkHeader As PKHeader = _pkDetail.PKHeader
                If Not _pkHeader.PKStatus = enumStatusPK.Status.Konfirmasi Then
                    _pkDetail.ErrorMessage = _pkDetail.ErrorMessage & "Status PK Bukan Konfirmasi<br>"
                End If
                If (Not _pkHeader.ProductionYear = productionYear) Or (Not _pkHeader.RequestPeriodeMonth = month) Or (Not _pkHeader.RequestPeriodeYear = year) Then
                    _pkDetail.ErrorMessage = _pkDetail.ErrorMessage & "ProdYear, PeriodMonth atau PeriodYear tidak Valid <br>"
                End If
                If allocQty > _pkDetail.TargetQty Then
                    _pkDetail.ErrorMessage = _pkDetail.ErrorMessage & "Jumlah Alokasi melebihi Order<br>"
                End If
                _pkDetail.AgreeQty = allocQty
            Else
                _pkDetail.VehicleColorCode = pkNumber
                _pkDetail.TargetQty = month
                _pkDetail.LineItem = year
                _pkDetail.RowStatus = productionYear
                _pkDetail.MaterialNumber = materialNumber
                _pkDetail.AgreeQty = allocQty
                _pkDetail.ErrorMessage = _pkDetail.ErrorMessage & "PK tidak ditemukan <br>"
            End If
            Return _pkDetail
        End Function

#End Region

    End Class

End Namespace
#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Text
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class PMParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _PMHeaders As ArrayList
        Private Grammar As Regex
        Private mapper As IMapper
        Private objTransactionManager As TransactionManager
        Private _fileName As String
        Private sBuilder As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser(";")
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal oDealerCode As String) As Object
            _Stream = New StreamReader(fileName, True)
            _PMHeaders = New ArrayList
            _fileName = fileName
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            While (Not val = "")
                Try
                    'ParsePM(val + ";")
                    ParsePM(val + ";", oDealerCode)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "PMParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PMParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return _PMHeaders
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"
        'add by anh 20120402
        Private Sub ParsePM(ByVal valParser As String, ByVal oDealerCode As String)
            Dim _PMHeader As PMHeader = New PMHeader
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            sBuilder = New StringBuilder
            For Each m As Match In Grammar.Matches(valParser)
                sTemp = valParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        If sTemp.Length > 0 Then
                            Dim objDealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            If Not objDealer Is Nothing And objDealer.ID > 0 Then
                                If oDealerCode = objDealer.DealerCode Then
                                    _PMHeader.Dealer = objDealer
                                    _PMHeader.PMStatus = EnumPMStatus.PMStatus.Baru
                                Else
                                    _PMHeader.Dealer = objDealer
                                    _PMHeader.PMStatus = EnumPMStatus.PMStatus.Baru
                                    sBuilder.Append("Kode Dealer tidak sesuai" & Chr(13) & Chr(10))
                                End If
                            Else
                                sBuilder.Append("Kode Dealer tidak ditemukan" & Chr(13) & Chr(10))
                            End If
                        Else
                            sBuilder.Append("Kode Dealer tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 1
                        If sTemp.Length > 0 Then
                            Dim objChassisMaster As ChassisMaster = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            If Not objChassisMaster Is Nothing And objChassisMaster.ID > 0 Then
                                _PMHeader.ChassisMaster = objChassisMaster
                            Else
                                sBuilder.Append("No Chassis tidak ditemukan" & Chr(13) & Chr(10))
                            End If
                        Else
                            sBuilder.Append("No Chassis tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        If sTemp.Length = 8 Then
                            Try
                                _PMHeader.ServiceDate = New DateTime(CInt(sTemp.Substring(4, 4)), CInt(sTemp.Substring(2, 2)), CInt(sTemp.Substring(0, 2)))

                                If _PMHeader.ServiceDate > DateTime.Now Then
                                    sBuilder.Append("Tgl PM di atas tanggal sistem" & Chr(13) & Chr(10))
                                End If

                                If _PMHeader.ServiceDate.Year < 1753 Or _PMHeader.ServiceDate.Year > 9999 Then
                                    Throw New Exception("Tanggal PM tidak valid")
                                End If

                            Catch ex As Exception
                                sBuilder.Append("Tgl PM tidak valid" & Chr(13) & Chr(10))
                            End Try
                        Else
                            sBuilder.Append("Tgl PM tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 3
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp) Then
                                If CInt(sTemp) >= 0 Then
                                    _PMHeader.StandKM = CInt(sTemp)
                                Else
                                    sBuilder.Append("Stand KM harus lebih besar dari 0" & Chr(13) & Chr(10))
                                End If
                            Else
                                sBuilder.Append("Stand KM harus angka" & Chr(13) & Chr(10))
                            End If
                        Else
                            sBuilder.Append("Stand KM tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 4
                        If sTemp.Length > 0 AndAlso Not IsNothing(_PMHeader.ChassisMaster) Then
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, _PMHeader.ChassisMaster.ChassisNumber))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EngineNumber", MatchType.Exact, sTemp.Trim()))

                            Dim arrChassisMaster As ArrayList = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If arrChassisMaster.Count = 0 Then
                                sBuilder.Append("No Chassis dan No Mesin tidak sesuai" & Chr(13) & Chr(10))
                            End If
                        Else
                            sBuilder.Append("No Chassis dan No Mesin tidak sesuai" & Chr(13) & Chr(10))
                        End If
                        'If sTemp.Length > 0 Then
                        '    For Each item As String In sTemp.Split("-")
                        '        Dim objReplacementPart As ReplecementPartMaster = New ReplecementPartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(item)
                        '        If Not IsNothing(objReplacementPart) And objReplacementPart.ID > 0 Then
                        '            Dim _PMDetail As New PMDetail
                        '            _PMDetail.ReplecementPartMaster = objReplacementPart
                        '            _PMHeader.PMDetails.Add(_PMDetail)
                        '        Else
                        '            sBuilder.Append("Kode Replacement Part (" & item & ") tidak valid" & Chr(13) & Chr(10))
                        '        End If
                        '    Next
                        'Else
                        '    sBuilder.Append("PM Detail tidak valid" & Chr(13) & Chr(10))
                        'End If
                    Case Is = 5
                        If sTemp.Length > 0 And (sTemp.Trim() = "WI" Or sTemp.Trim() = "BO") Then
                            _PMHeader.VisitType = sTemp.Trim()
                        Else
                            sBuilder.Append("Tipe Visit tidak sesuai" & Chr(13) & Chr(10))
                        End If
                    Case Is = 6
                        If sTemp.Length > 0 Then
                            Dim pmkindCode As String = sTemp.Trim
                            If pmkindCode.Length = 1 Then
                                pmkindCode = "0" & sTemp.Trim
                            End If
                            'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMKind), "KindCode", MatchType.Exact, pmkindCode))
                            ''criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMKind), "KM", MatchType.GreaterOrEqual, _PMHeader.StandKM))
                            'Dim sortColl As SortCollection = New SortCollection

                            'If (Not IsNothing("KM")) And (Not IsNothing("KM")) Then
                            '    sortColl.Add(New Sort(GetType(PMKind), "KM", Sort.SortDirection.ASC))
                            'Else
                            '    sortColl = Nothing
                            'End If

                            Dim oPMKind As PMKind = New PMKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(pmkindCode)
                            If IsNothing(oPMKind) OrElse oPMKind.ID = 0 Then
                                _PMHeader.PMKind = Nothing
                                sBuilder.Append("Jenis PM " & pmkindCode & " tidak ada." & Chr(13) & Chr(10))
                            Else
                                If _PMHeader.StandKM > oPMKind.KM Then
                                    sBuilder.Append("Jarak Tempuh melebihi batas jenis PM." & Chr(13) & Chr(10))
                                End If
                                _PMHeader.PMKind = CType(oPMKind, PMKind)
                            End If
                        Else
                            sBuilder.Append("Silahkan isi Jenis PM." & Chr(13) & Chr(10))
                            _PMHeader.PMKind = Nothing
                        End If
                    Case Is = 7
                        If Not String.IsNullOrEmpty(sTemp.Trim) Then
                            'StrDealerBranchFSTxtCode = sTemp.Trim
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "DealerBranchCode", MatchType.Exact, sTemp.Trim))
                            Dim ArryList As ArrayList = New DealerBranchFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If ArryList.Count > 0 Then
                                For Each ObjDealerBranch As DealerBranch In ArryList
                                    If ObjDealerBranch.Dealer.DealerCode = oDealerCode Then
                                        _PMHeader.DealerBranch = ObjDealerBranch
                                    Else
                                        If _PMHeader.ErrorMessage = "" Then
                                            sBuilder.Append("Kode Cabang Tidak Cocok")
                                        Else
                                            sBuilder.Append(sBuilder.ToString + ";<br> Kode Cabang Tidak Cocok")
                                        End If
                                        _PMHeader.DealerBranchCodeMsg = sTemp.Trim
                                    End If
                                Next
                            Else
                                If _PMHeader.ErrorMessage = "" Then
                                    sBuilder.Append("Kode Cabang Tidak Terdaftar")
                                Else
                                    sBuilder.Append(sBuilder.ToString + ";<br> Kode Cabang Tidak Terdaftar")
                                End If
                                _PMHeader.DealerBranchCodeMsg = sTemp.Trim
                            End If
                        End If
                    Case Is = 8
                        If Not String.IsNullOrEmpty(sTemp.Trim) Then
                            _PMHeader.WorkOrderNumber = sTemp.Trim
                        End If
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            Dim words As String() = valParser.Split(New Char() {";"c})

            If words.Length <> 10 AndAlso words.Length <> 8 AndAlso words.Length <> 9 Then
                sBuilder.Append("Format tidak sesuai" & Chr(13) & Chr(10))
            End If

            _PMHeader.ErrorMessage = sBuilder.ToString
            _PMHeaders.Add(_PMHeader)
        End Sub
        ' end added by anh


        Private Sub ParsePM(ByVal ValParser As String)
            Dim _PMHeader As PMHeader = New PMHeader
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            sBuilder = New StringBuilder
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        If sTemp.Length > 0 Then
                            Dim objDealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            If Not objDealer Is Nothing And objDealer.ID > 0 Then
                                _PMHeader.Dealer = objDealer
                                _PMHeader.PMStatus = EnumPMStatus.PMStatus.Baru
                            Else
                                sBuilder.Append("Kode Dealer tidak ditemukan" & Chr(13) & Chr(10))
                            End If
                        Else
                            sBuilder.Append("Kode Dealer tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 1
                        If sTemp.Length > 0 Then
                            Dim objChassisMaster As ChassisMaster = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            If Not objChassisMaster Is Nothing And objChassisMaster.ID > 0 Then
                                _PMHeader.ChassisMaster = objChassisMaster
                            Else
                                sBuilder.Append("No Chassis tidak ditemukan" & Chr(13) & Chr(10))
                            End If
                        Else
                            sBuilder.Append("No Chassis tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        If sTemp.Length = 8 Then
                            Try
                                _PMHeader.ServiceDate = New DateTime(CInt(sTemp.Substring(4, 4)), CInt(sTemp.Substring(2, 2)), CInt(sTemp.Substring(0, 2)))
                            Catch ex As Exception
                                sBuilder.Append("Tgl PM tidak valid" & Chr(13) & Chr(10))
                            End Try
                        Else
                            sBuilder.Append("Tgl PM tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 3
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp) Then
                                If CInt(sTemp) >= 0 Then
                                    _PMHeader.StandKM = CInt(sTemp)
                                Else
                                    sBuilder.Append("Stand KM harus lebih besar dari 0" & Chr(13) & Chr(10))
                                End If
                            Else
                                sBuilder.Append("Stand KM harus angka" & Chr(13) & Chr(10))
                            End If
                        Else
                            sBuilder.Append("Stand KM tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 4
                        'If sTemp.Length > 0 Then
                        '    For Each item As String In sTemp.Split("-")
                        '        Dim objReplacementPart As ReplecementPartMaster = New ReplecementPartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(item)
                        '        If Not IsNothing(objReplacementPart) And objReplacementPart.ID > 0 Then
                        '            Dim _PMDetail As New PMDetail
                        '            _PMDetail.ReplecementPartMaster = objReplacementPart
                        '            _PMHeader.PMDetails.Add(_PMDetail)
                        '        Else
                        '            sBuilder.Append("Kode Replacement Part (" & item & ") tidak valid" & Chr(13) & Chr(10))
                        '        End If
                        '    Next
                        'Else
                        '    sBuilder.Append("PM Detail tidak valid" & Chr(13) & Chr(10))
                        'End If
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            'If _PMHeader.PMDetails.Count > 0 Then
            '    Dim nIndex As Integer
            '    Dim nIterate As Integer = 1
            '    Dim isDouble As Integer = 0
            '    For Each objPMdetail1 As PMDetail In _PMHeader.PMDetails
            '        If Not IsNothing(objPMdetail1.ReplecementPartMaster) Then
            '            For nIndex = nIterate To _PMHeader.PMDetails.Count - 1
            '                Dim objPMdetail2 As PMDetail
            '                objPMdetail2 = _PMHeader.PMDetails(nIndex)
            '                If Not IsNothing(objPMdetail2.ReplecementPartMaster) Then
            '                    Dim sReplecementPartMaster1 = objPMdetail1.ReplecementPartMaster.Code
            '                    Dim sReplecementPartMaster2 = objPMdetail2.ReplecementPartMaster.Code

            '                    If sReplecementPartMaster1 = sReplecementPartMaster2 Then
            '                        isDouble = isDouble + 1
            '                    End If
            '                End If
            '            Next
            '        End If
            '        nIterate = nIterate + 1
            '    Next
            '    If isDouble > 0 Then
            '        sBuilder.Append("Replacement Part ada yg ganda dgn kode yg sama" & Chr(13) & Chr(10))
            '    End If
            'End If

            _PMHeader.ErrorMessage = sBuilder.ToString
            _PMHeaders.Add(_PMHeader)
        End Sub

#End Region
    End Class
End Namespace


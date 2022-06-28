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
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser

    Public Class WSCSStatusParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private WSCHeaders As ArrayList
        Private WSCDetails As ArrayList
        Private _fileName As String
        Private _WSCHeader As WSCHeader 'Header
        Private _WSCDetail As WSCDetail
        Private ErrorMessage As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Try
                _fileName = fileName
                Dim val As String
                WSCHeaders = New ArrayList
                stream = New StreamReader(fileName, True)
                val = MyBase.NextLine(stream).Trim()
                While (Not val = "")
                    Try
                        If val.Substring(0, 1).Equals("H") Then
                            If Not _WSCHeader Is Nothing Then
                                WSCHeaders.Add(_WSCHeader)  '-- WSC header input text
                            End If
                            _WSCHeader = ParseWSCHeader(val + delimited)
                        Else
                            If Not _WSCHeader Is Nothing Then
                                ParseWSCDetail(val + delimited, _WSCHeader)  'Order detail input
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "WSCSStatusParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.WSCSStatusParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _WSCHeader = Nothing
                    End Try
                    val = MyBase.NextLine(stream)
                End While

                If Not _WSCHeader Is Nothing Then
                    WSCHeaders.Add(_WSCHeader)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                stream.Close()
                stream = Nothing
            End Try
            Return WSCHeaders

        End Function

        Protected Overrides Function DoTransaction() As Integer
            For Each item As WSCHeader In WSCHeaders
                Try
                    Dim objHeaderFromDB As WSCHeader = GetWSC(item)
                    'If Not item.ClaimStatus.Trim.ToUpper = "WFAP" Then
                    If objHeaderFromDB Is Nothing Then
                        InsertWsc(item)
                    Else
                        UpdateWsc(objHeaderFromDB, item)
                    End If
                    'Else
                    '    Dim exc As Exception = New Exception("Claim Status still WFAP")
                    '    Throw exc
                    'End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "WSCSStatusParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.WSCSStatusParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.ClaimNumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
        End Function

        Private Sub InsertWsc(ByVal obj As WSCHeader)
            Dim _headerFacade As New WSCHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            _headerFacade.Insert(obj)
        End Sub

        Private Sub UpdateWsc(ByVal objOld As WSCHeader, ByVal objNew As WSCHeader)
            Try
                Dim _headerFacade As New WSCHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                objOld.Description = objNew.Description
                objOld.EvidencePhoto = objNew.EvidencePhoto
                objOld.EvidenceInvoice = objNew.EvidenceInvoice
                objOld.EvidenceDmgPart = objNew.EvidenceDmgPart
                objOld.NotificationNumber = objNew.NotificationNumber
                objOld.Status = objNew.Status
                objOld.Reason = objNew.Reason
                'objOld.Reason2 = objNew.Reason2
                'objOld.Reason3 = objNew.Reason3
                'objOld.Reason4 = objNew.Reason4
                'objOld.Reason5 = objNew.Reason5
                objOld.DecideDate = objNew.DecideDate
                objOld.LaborAmount = objNew.LaborAmount
                objOld.PartAmount = objNew.PartAmount
                objOld.ClaimStatus = objNew.ClaimStatus

                Dim isExist As Boolean = False
                For Each oldDetail As WSCDetail In objOld.WSCDetails
                    For Each newDetail As WSCDetail In objNew.WSCDetails
                        If (newDetail.WSCType = "P") Then '(oldDetail.WSCType = newDetail.WSCType) 
                            'If Not IsNothing(oldDetail.LaborMaster) Then
                            '    If oldDetail.LaborMaster.ID = newDetail.LaborMaster.ID Then
                            '        isExist = True
                            '    End If
                            'End If
                            If Not IsNothing(oldDetail.SparePartMaster) Then
                                If oldDetail.SparePartMaster.ID = newDetail.SparePartMaster.ID Then
                                    isExist = True
                                End If
                            End If
                            If isExist Then
                                'oldDetail.WSCType = newDetail.WSCType
                                'oldDetail.LaborMaster = newDetail.LaborMaster
                                'oldDetail.SparePartMaster = newDetail.SparePartMaster
                                'oldDetail.Quantity = newDetail.Quantity
                                'oldDetail.PartPrice = newDetail.PartPrice
                                'oldDetail.MainPart = newDetail.MainPart
                                oldDetail.QuantityReceived = newDetail.QuantityReceived
                                oldDetail.ReceivedBy = newDetail.ReceivedBy
                                oldDetail.ReceivedDate = newDetail.ReceivedDate
                                isExist = False
                                Exit For
                            End If
                        End If
                    Next
                Next
                '_headerFacade.Update(objOld)
                _headerFacade.UpdateHeaderDetail(objOld)
            Catch ex As Exception
                Dim str As String = String.Empty
            End Try
        End Sub

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
        End Function

#End Region

#Region "Private Methods"

        Private Function ParseWSCHeader(ByVal ValParser As String) As WSCHeader
            _WSCHeader = New WSCHeader
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            ErrorMessage = New StringBuilder
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()

                Select Case nCount
                    Case Is = 1  '-- Dealer
                        If sTemp.Length > 0 Then
                            Try
                                Dim _dealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                                If _dealer.ID > 0 Then
                                    _WSCHeader.Dealer = _dealer
                                Else
                                    ErrorMessage.Append("Dealer code tidak terdefinisi." & Chr(13) & Chr(10))
                                End If
                                'Catch
                            Catch ex As Exception
                                ErrorMessage.Append("Dealer code tidak terdefinisi." & Chr(13) & Chr(10))
                            End Try
                        Else
                            ErrorMessage.Append("Dealer code tidak terdefinisi." & Chr(13) & Chr(10))
                        End If

                    Case Is = 2  '-- Claim #
                        If sTemp.Length = 6 Then 'Harus 6 digit
                            _WSCHeader.ClaimNumber = sTemp
                        Else
                            ErrorMessage.Append("Claim Number tidak valid." & Chr(13) & Chr(10))
                        End If
                    Case Is = 3  '-- Claim type
                        If sTemp.Length > 0 Then
                            _WSCHeader.ClaimType = sTemp
                        Else
                            ErrorMessage.Append("Claim Type tidak valid." & Chr(13) & Chr(10))
                        End If
                    Case Is = 4  '-- Ref. claim #
                        _WSCHeader.RefClaimNumber = sTemp
                    Case Is = 5  '-- Chassis #
                        If sTemp.Length > 0 Then
                            Dim _chassis As ChassisMaster = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                            If _chassis.ID > 0 Then
                                _WSCHeader.ChassisMaster = _chassis
                            Else
                                ErrorMessage.Append("Chassis number tidak terdefinisi." & Chr(13) & Chr(10))
                            End If
                        Else
                            ErrorMessage.Append("Chassis number tidak terdefinisi." & Chr(13) & Chr(10))
                        End If

                    Case Is = 6  '-- Claim date (Service date)
                        Try
                            _WSCHeader.ServiceDate = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2))
                        Catch ex As Exception
                            ErrorMessage.Append("Claim date tidak valid." & Chr(13) & Chr(10))
                        End Try
                    Case Is = 7  '-- Mileage
                        If sTemp.Length > 0 Then
                            Try
                                _WSCHeader.Miliage = CType(sTemp, Integer)
                            Catch ex As Exception
                                ErrorMessage.Append("Mileage tidak valid." & Chr(13) & Chr(10))
                            End Try
                        Else
                            ErrorMessage.Append("Mileage tidak valid." & Chr(13) & Chr(10))
                        End If
                    Case Is = 8  '-- PQR
                        _WSCHeader.PQR = sTemp
                    Case Is = 9  '-- PQR Status
                        _WSCHeader.PQRStatus = sTemp
                    Case Is = 10  '-- Kode A
                        _WSCHeader.CodeA = sTemp
                    Case Is = 11  '-- Kode B
                        _WSCHeader.CodeB = sTemp
                    Case Is = 12  '-- Kode C
                        _WSCHeader.CodeC = sTemp
                    Case Is = 13  '-- Description
                        _WSCHeader.Description = sTemp
                    Case Is = 14  '-- Photo
                        _WSCHeader.EvidencePhoto = sTemp
                    Case Is = 15  '-- Kwitansi
                        _WSCHeader.EvidenceInvoice = sTemp
                    Case Is = 16  '-- Damage
                        _WSCHeader.EvidenceDmgPart = sTemp
                    Case Is = 17  '-- Notification #
                        _WSCHeader.NotificationNumber = sTemp
                    Case Is = 18  '-- Claim status (Update DNet if any)
                        If sTemp.Length > 0 Then
                            If sTemp = "WFAP" Then
                                _WSCHeader.Status = enumStatusWSC.Status.Proses  '-- WSC status = Selesai
                                _WSCHeader.ClaimStatus = sTemp
                            Else
                                _WSCHeader.Status = enumStatusWSC.Status.Selesai  '-- WSC status = Selesai
                                _WSCHeader.ClaimStatus = sTemp
                            End If
                        Else
                            ErrorMessage.Append("Claim Status tidak valid." & Chr(13) & Chr(10))
                        End If
                    Case Is = 19  '-- Reason 
                        If sTemp.Length > 0 Then
                            Try
                                Dim _reason As Reason = New ReasonFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                                If _reason.ID > 0 Then
                                    _WSCHeader.Reason = _reason
                                Else
                                    _WSCHeader.Reason = Nothing
                                End If
                            Catch
                                _WSCHeader.Reason = Nothing
                                ErrorMessage.Append("Reason Id tidak valid." & Chr(13) & Chr(10))
                            End Try
                        End If
                    Case Is = 20  '-- Decide date
                        If sTemp.Length > 0 Then
                            Try
                                _WSCHeader.DecideDate = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2))
                            Catch ex As Exception
                                ErrorMessage.Append("Decide date tidak valid." & Chr(13) & Chr(10))
                            End Try
                        Else
                            _WSCHeader.DecideDate = New Date(1900, 1, 1) ' if date is blank replace with minimum date
                        End If
                    Case Is = 21  '-- Labor amount (Update DNet if any)
                        If sTemp.Length > 0 Then
                            Try
                                _WSCHeader.LaborAmount = CType(sTemp, Double)
                            Catch ex As Exception
                                ErrorMessage.Append("Labor amount tidak valid." & Chr(13) & Chr(10))
                            End Try
                        Else
                            _WSCHeader.LaborAmount = 0
                        End If
                    Case Is = 22  '-- Part amount (Update DNet if any)
                        If sTemp.Length > 0 Then
                            Try
                                _WSCHeader.PartAmount = CType(sTemp, Double)
                            Catch ex As Exception
                                ErrorMessage.Append("Part amount tidak valid." & Chr(13) & Chr(10))
                            End Try
                        Else
                            _WSCHeader.PartAmount = 0
                        End If
                        'Case Is = 23  '-- Reason  2
                        '    If sTemp.Length > 0 Then
                        '        Try
                        '            Dim _reason2 As Reason = New ReasonFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                        '            If _reason2.ID > 0 Then
                        '                _WSCHeader.Reason2 = _reason2
                        '            Else
                        '                _WSCHeader.Reason2 = Nothing
                        '            End If
                        '        Catch
                        '            _WSCHeader.Reason2 = Nothing
                        '            ErrorMessage.Append("Reason Id tidak valid." & Chr(13) & Chr(10))
                        '        End Try
                        '    End If
                        'Case Is = 24  '-- Reason  3
                        '    If sTemp.Length > 0 Then
                        '        Try
                        '            Dim _reason3 As Reason = New ReasonFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                        '            If _reason3.ID > 0 Then
                        '                _WSCHeader.Reason3 = _reason3
                        '            Else
                        '                _WSCHeader.Reason3 = Nothing
                        '            End If
                        '        Catch
                        '            _WSCHeader.Reason3 = Nothing
                        '            ErrorMessage.Append("Reason Id tidak valid." & Chr(13) & Chr(10))
                        '        End Try
                        '    End If
                        'Case Is = 25  '-- Reason  4
                        '    If sTemp.Length > 0 Then
                        '        Try
                        '            Dim _reason4 As Reason = New ReasonFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                        '            If _reason4.ID > 0 Then
                        '                _WSCHeader.Reason4 = _reason4
                        '            Else
                        '                _WSCHeader.Reason4 = Nothing
                        '            End If
                        '        Catch
                        '            _WSCHeader.Reason4 = Nothing
                        '            ErrorMessage.Append("Reason Id tidak valid." & Chr(13) & Chr(10))
                        '        End Try
                        '    End If
                        'Case Is = 26  '-- Reason  5
                        '    If sTemp.Length > 0 Then
                        '        Try
                        '            Dim _reason5 As Reason = New ReasonFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                        '            If _reason5.ID > 0 Then
                        '                _WSCHeader.Reason5 = _reason5
                        '            Else
                        '                _WSCHeader.Reason5 = Nothing
                        '            End If
                        '        Catch
                        '            _WSCHeader.Reason5 = Nothing
                        '            ErrorMessage.Append("Reason Id tidak valid." & Chr(13) & Chr(10))
                        '        End Try
                        '    End If
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next
            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            End If
            Return _WSCHeader
        End Function

        Private Sub ParseWSCDetail(ByVal ValParser As String, ByVal _objWSCHeader As WSCHeader)
            _WSCDetail = New WSCDetail
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim TempLabor As String = String.Empty
            Dim Position As String = String.Empty
            sStart = 0
            nCount = 0
            ErrorMessage = New StringBuilder
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case nCount
                    Case Is = 3  '-- WSC type
                        If sTemp.Length > 0 Then
                            _WSCDetail.WSCType = sTemp
                        Else
                            ErrorMessage.Append("WSC Type tidak valid." & Chr(13) & Chr(10))
                        End If
                    Case Is = 4  '-- Labor/Part
                        If sTemp.Length > 0 Then
                            Select Case _WSCDetail.WSCType
                                Case Is = "L"  '-- Labor
                                    If sTemp.Length > 0 Then
                                        TempLabor = sTemp
                                    Else
                                        ErrorMessage.Append("Labor code tidak terdefinisi" & Chr(13) & Chr(10))
                                    End If
                                Case Is = "P"  '-- Sparepart
                                    Try
                                        Dim _sppart As SparePartMaster = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                                        If _sppart.ID > 0 Then
                                            _WSCDetail.SparePartMaster = _sppart
                                        Else
                                            ErrorMessage.Append("Part Number tidak terdefinisi" & Chr(13) & Chr(10))
                                        End If
                                    Catch
                                        ErrorMessage.Append("Part Number tidak terdefinisi" & Chr(13) & Chr(10))
                                    End Try
                            End Select
                        Else
                            Select Case _WSCDetail.WSCType
                                Case Is = "L"  '-- Labor
                                    ErrorMessage.Append("Labor code kosong" & Chr(13) & Chr(10))
                                Case Is = "P"  '-- Part
                                    ErrorMessage.Append("Part code kosong" & Chr(13) & Chr(10))
                            End Select
                        End If

                    Case Is = 5  '-- If Type=L then "WorkCode" (ignore) else if Type=P then "Quantity"
                        Select Case _WSCDetail.WSCType
                            Case Is = "L"  '-- Labor
                                Position = sTemp
                                If Position <> "99" Then
                                    Dim _laborMaster As LaborMaster = GetLaborMaster(TempLabor, sTemp, _objWSCHeader.ChassisMaster)
                                    If Not _laborMaster Is Nothing Then
                                        _WSCDetail.LaborMaster = _laborMaster
                                    Else
                                        ErrorMessage.Append("Labor code tidak terdefinisi" & Chr(13) & Chr(10))
                                    End If
                                End If

                            Case Is = "P"  '-- Sparepart
                                If sTemp.Length > 0 Then
                                    Try
                                        _WSCDetail.Quantity = CType(sTemp, Double)
                                    Catch
                                        ErrorMessage.Append("Quantity tidak valid" & Chr(13) & Chr(10))
                                    End Try
                                Else
                                    ErrorMessage.Append("Quantity tidak valid" & Chr(13) & Chr(10))
                                End If
                        End Select

                    Case Is = 6  '-- If Type=L then "Quantity" else if Type=P then "PartPrice"
                        Select Case _WSCDetail.WSCType
                            Case Is = "L"  '-- Labor
                                If Position = "99" Then
                                    Try
                                        _WSCDetail.PartPrice = CType(sTemp, Double)
                                    Catch ex As Exception
                                        _WSCDetail.PartPrice = 0
                                    End Try
                                Else
                                    Try
                                        _WSCDetail.Quantity = CType(sTemp, Double)
                                    Catch ex As Exception
                                        _WSCDetail.Quantity = 0
                                    End Try
                                End If
                            Case Is = "P"  '-- Sparepart
                                If sTemp.Length > 0 Then
                                    Try
                                        _WSCDetail.PartPrice = CType(sTemp, Double)
                                    Catch
                                        _WSCDetail.PartPrice = 0
                                    End Try
                                End If
                        End Select
                        'Case Is = 7  '-- If Type=P then "MainPart" else if Type=P then "0"
                        '    Select Case _WSCDetail.WSCType
                        '        Case Is = "L"  '-- Labor
                        '            _WSCDetail.MainPart = 0
                        '        Case Is = "P"  '-- Sparepart
                        '            If sTemp = "X" Then
                        '                _WSCDetail.MainPart = 1
                        '            Else
                        '                _WSCDetail.MainPart = 0
                        '            End If
                        '    End Select
                    Case Is = 7  'QuantityReceived
                        If _WSCDetail.WSCType = "P" Then
                            If sTemp.Length > 0 Then
                                Try
                                    _WSCDetail.QuantityReceived = CType(sTemp, Double)
                                Catch
                                    _WSCDetail.QuantityReceived = 0
                                End Try
                            End If
                        End If
                    Case Is = 8  'ReceivedBy
                        If _WSCDetail.WSCType = "P" Then
                            If sTemp.Length > 0 Then
                                Try
                                    _WSCDetail.ReceivedBy = CType(sTemp, String)
                                Catch
                                    _WSCDetail.ReceivedBy = ""
                                End Try
                            End If
                        End If
                    Case Is = 9  'ReceivedDate
                        If _WSCDetail.WSCType = "P" Then
                            If sTemp.Length > 0 Then
                                Try
                                    _WSCDetail.ReceivedDate = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2))
                                Catch ex As Exception
                                    ErrorMessage.Append("Tanggal terima part tidak valid." & Chr(13) & Chr(10))
                                End Try
                            Else
                                _WSCDetail.ReceivedDate = New Date(1900, 1, 1) ' if date is blank replace with minimum date
                            End If
                        End If
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next
            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            End If
            _objWSCHeader.WSCDetails.Add(_WSCDetail)
        End Sub

        Private Function GetLaborMaster(ByVal LaborCode As String, ByVal position As String, ByVal objChassisMaster As ChassisMaster) As LaborMaster
            Dim vecTypeID As Integer = objChassisMaster.VechileColor.VechileType.ID
            Dim criterias As New CriteriaComposite(New Criteria(GetType(LaborMaster), "LaborCode", MatchType.Exact, LaborCode))
            criterias.opAnd(New Criteria(GetType(LaborMaster), "WorkCode", MatchType.Exact, position))
            criterias.opAnd(New Criteria(GetType(LaborMaster), "VechileType.ID", MatchType.Exact, vecTypeID))
            Dim _laborFacade As LaborMasterFacade = New LaborMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim laborColl As ArrayList = _laborFacade.Retrieve(criterias)
            If laborColl.Count > 0 Then
                Return CType(laborColl(0), LaborMaster)
            Else
                Return Nothing
            End If
        End Function

        Private Function isExistsWSC(ByVal _header As WSCHeader) As Boolean
            '-- Check to see if _header exists in DNet based on Dealer ID and Claim number

            Dim criterias As New CriteriaComposite(New Criteria(GetType(WSCHeader), "Dealer.ID", MatchType.Exact, _header.Dealer.ID))
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ClaimNumber", MatchType.Exact, _header.ClaimNumber))

            Dim _headerFacade As New WSCHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim headerColl As ArrayList = _headerFacade.Retrieve(criterias)

            If headerColl.Count > 0 Then
                '-- If exists then take its ID
                _header.ID = CType(headerColl(0), WSCHeader).ID
            End If
            Return headerColl.Count > 0

        End Function

        Private Function GetWSC(ByVal _header As WSCHeader) As WSCHeader
            Dim criterias As New CriteriaComposite(New Criteria(GetType(WSCHeader), "Dealer.ID", MatchType.Exact, _header.Dealer.ID))
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ClaimNumber", MatchType.Exact, _header.ClaimNumber))
            Dim _headerFacade As New WSCHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim headerColl As ArrayList = _headerFacade.Retrieve(criterias)
            If headerColl.Count > 0 Then
                Return CType(headerColl(0), WSCHeader)
            Else
                Return Nothing
            End If
        End Function


#End Region

    End Class

End Namespace

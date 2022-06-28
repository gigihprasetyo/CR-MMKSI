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
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.SparePart
#End Region

Namespace KTB.DNet.Parser

    Public Class FreeServiceSynChronizationParser
        Inherits AbstractParser

#Region "Private Variables"

        Private status As String
        Private objStreamReader As StreamReader
        Private objFreeServiceAL As ArrayList
        Private _fileName As String
        Private Grammar As Regex
        Private sBuilder As StringBuilder

#End Region
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++
#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser(",")
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _fileName = fileName
            Me.objStreamReader = New StreamReader(fileName, True)
            Me.objFreeServiceAL = New ArrayList
            Dim val As String = MyBase.NextLine(Me.objStreamReader).Trim()
            While (Not val = "")
                Try
                    If val.Substring(0, 1).Equals("H") Then
                        parseFreeServiceSynChronization(val + ",")
                    Else
                        ParseFreeServicePartDetail(val + ",")
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "FreeServiceSynChronizationParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.FreeServiceSynChronizationParser, BlockName)
                    Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(Me.objStreamReader)
            End While
            Me.objStreamReader.Close()
            Me.objStreamReader = Nothing
            Return Me.objFreeServiceAL
        End Function

        Protected Overrides Function doParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

        Protected Overrides Function doTransaction() As Integer
            For Each objFreeService As FreeService In Me.objFreeServiceAL
                Try
                    synchronizeFreeServiceStatus(objFreeService)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "FreeServiceSynChronization", "FreeServiceSynChronizationParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.FreeServiceSynChronizationParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objFreeService.ChassisMaster.ChassisNumber & " - " & objFreeService.FSKind.KindCode & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
        End Function

#End Region

#Region "Private Methods"

        Private Sub parseFreeServiceSynChronization(ByVal ValParser As String)
            Dim objFreeService As FreeService = New FreeService
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sBuilder = New StringBuilder
            sStart = 0
            nCount = 0
            Dim isReleasDateExist As Boolean = False

            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case 1
                        Dim objDealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                        If objDealer.ID > 0 Then
                            objFreeService.Dealer = objDealer
                        Else
                            sBuilder.Append("Invalid Dealer" & Chr(13) & Chr(10))
                        End If
                    Case 2
                        Dim objChasis As ChassisMaster = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                        If objChasis.ID > 0 Then
                            objFreeService.ChassisMaster = objChasis
                        Else
                            sBuilder.Append("Invalid Chassis Master " & Chr(13) & Chr(10))
                        End If
                    Case 3
                        Dim objFsKind As FSKind = New FSKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                        If objFsKind.ID > 0 Then
                            objFreeService.FSKind = objFsKind
                        Else
                            sBuilder.Append("Invalid Chassis Kind " & Chr(13) & Chr(10))
                        End If
                    Case 4
                        Try
                            ' objFreeService.ServiceDate = CDate(sTemp.Substring(2, 2).ToString & "/" & sTemp.Substring(0, 2) & "/" & sTemp.Substring(4, 4))
                            objFreeService.ServiceDate = New Date(CInt(sTemp.Substring(4, 4)), CInt(sTemp.Substring(2, 2).ToString), CInt(sTemp.Substring(0, 2)))
                        Catch ex As Exception
                            sBuilder.Append("Invalid Service Date " & Chr(13) & Chr(10))
                        End Try
                    Case 5
                        Try
                            'objFreeService.SoldDate = CDate(sTemp.Substring(2, 2).ToString & "/" & sTemp.Substring(0, 2) & "/" & sTemp.Substring(4, 4))
                            objFreeService.SoldDate = New Date(CInt(sTemp.Substring(4, 4)), CInt(sTemp.Substring(2, 2).ToString), CInt(sTemp.Substring(0, 2)))
                        Catch ex As Exception
                            sBuilder.Append("Invalid Sold Date " & Chr(13) & Chr(10))
                        End Try
                    Case 6
                        If Not sTemp.Trim = "" Then
                            Try
                                objFreeService.MileAge = CInt(sTemp.Trim())
                            Catch ex As Exception
                                sBuilder.Append("Invalid MileAge " & Chr(13) & Chr(10))
                            End Try
                        Else
                            objFreeService.MileAge = 0
                        End If
                    Case 7
                        If Not sTemp.Trim() = "" AndAlso IsNumeric(sTemp) Then
                            Try

                                objFreeService.NotificationNumber = sTemp.Trim()
                            Catch ex As Exception
                                sBuilder.Append("Invalid Notification Number " & Chr(13) & Chr(10))
                            End Try
                        Else
                            objFreeService.NotificationNumber = 0
                        End If
                    Case 8
                        objFreeService.NotificationType = sTemp.Trim()
                    Case 9
                        If sTemp.ToUpper = "APP" Or sTemp.ToUpper = "DAPP" Then
                            objFreeService.Reject = sTemp.Trim()
                        Else
                            sBuilder.Append("Invalid Reject Type Must APP or DAPP " & Chr(13) & Chr(10))
                        End If
                    Case 10
                        Dim objCriterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Reason), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        objCriterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Reason), "ReasonCode", MatchType.Exact, sTemp.Trim))
                        Dim objReasonAL As ArrayList = New ReasonFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(objCriterias)
                        If objReasonAL.Count > 0 Then
                            objFreeService.Reason = CType(objReasonAL.Item(0), Reason)
                        End If
                    Case 11
                        If Not sTemp.Trim() = "" Then
                            Try
                                objFreeService.LabourAmount = CInt(sTemp.Trim())
                            Catch ex As Exception
                                sBuilder.Append("Invalid LabourAmount " & Chr(13) & Chr(10))
                            End Try
                        Else
                            objFreeService.LabourAmount = 0
                        End If
                    Case 12
                        If Not sTemp.Trim() = "" Then
                            Try
                                objFreeService.PartAmount = CInt(sTemp.Trim())
                            Catch ex As Exception
                                sBuilder.Append("Invalid PartAmount " & Chr(13) & Chr(10))
                            End Try
                        Else
                            objFreeService.PartAmount = 0
                        End If
                    Case 13
                        If Not sTemp.Trim() = "" Then
                            Try
                                objFreeService.PPNAmount = CInt(sTemp.Trim())
                            Catch ex As Exception
                                sBuilder.Append("Invalid PPNAmount " & Chr(13) & Chr(10))
                            End Try
                        Else
                            objFreeService.PPNAmount = 0
                        End If
                    Case 14
                        If Not sTemp.Trim() = "" Then
                            Try
                                objFreeService.PPHAmount = CInt(sTemp.Trim())
                            Catch ex As Exception
                                sBuilder.Append("Invalid PPHAmount " & Chr(13) & Chr(10))
                            End Try
                        Else
                            objFreeService.PPHAmount = 0
                        End If

                    Case 15
                        Dim FSReleaseDate As DateTime
                        isReleasDateExist = True
                        Try
                            If sTemp.Length > 0 Then
                                FSReleaseDate = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2))
                            Else
                                FSReleaseDate = System.DateTime.Now
                            End If
                            objFreeService.ReleaseDate = FSReleaseDate
                        Catch ex As Exception
                            sBuilder.Append("Invalid PDI Date " & sTemp & Chr(13) & Chr(10))
                        End Try

                    Case 16
                        If Not sTemp.Trim() = "" Then
                            Try
                                objFreeService.CashBack = CType(sTemp.Trim(), Decimal)
                            Catch ex As Exception
                                sBuilder.Append("Invalid CashBack " & Chr(13) & Chr(10))
                            End Try
                        End If
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If isReleasDateExist = False Then
                objFreeService.ReleaseDate = System.DateTime.Now
            End If


            If sBuilder.Length > 0 Then
                Throw New Exception(sBuilder.ToString)
            Else
                objFreeService.TotalAmount = getTotalAmount(objFreeService)
                objFreeService.Status = CStr(EnumFSStatus.FSStatus.Selesai)
                Me.objFreeServiceAL.Add(objFreeService)
            End If
        End Sub

        Private Function getFreeServiceList(ByVal code As String) As FreeService
            Dim userPrinciple As IPrincipal
            Dim objFreeServiceFacade As New FreeServiceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim objFreeService As FreeService = objFreeServiceFacade.Retrieve(code)
            Return objFreeService
        End Function

        Private Function getTotalAmount(ByVal objFreeService As FreeService)
            Dim dblTotalAmount, dblLabourAmount, dblPartAmount, dblPPNAmount, dblPPHAmount As Double
            If Not IsNothing(objFreeService.LabourAmount) Then
                dblLabourAmount = objFreeService.LabourAmount
            Else
                dblLabourAmount = 0
            End If

            If Not IsNothing(objFreeService.PartAmount) Then
                dblPartAmount = objFreeService.PartAmount
            Else
                dblPartAmount = 0
            End If

            If Not IsNothing(objFreeService.PPNAmount) Then
                dblPPNAmount = objFreeService.PPNAmount
            Else
                dblPPNAmount = 0
            End If

            If Not IsNothing(objFreeService.PPNAmount) Then
                dblPPNAmount = objFreeService.PPNAmount
            Else
                dblPPNAmount = 0
            End If

            dblTotalAmount = dblLabourAmount + dblPartAmount + dblPPHAmount + dblPPNAmount

            Return dblTotalAmount

        End Function

        Private Function checkChassisAndFsKind(ByVal objFreeService As FreeService) As Boolean
            Dim user As IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim objChassisMasterAL As ArrayList
            Dim objFsKindAL As ArrayList
            Dim objCriteria As CriteriaComposite
            objCriteria = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, objFreeService.ChassisMaster.ChassisNumber.ToString.Trim()))
            objChassisMasterAL = New ChassisMasterFacade(user).Retrieve(objCriteria)
            If objChassisMasterAL.Count <> 0 Then
                objCriteria = New CriteriaComposite(New Criteria(GetType(FSKind), "KindCode", MatchType.Exact, objFreeService.FSKind.KindCode.ToString.Trim()))
                objCriteria.opAnd(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, DBRowStatus.Active))
                objFsKindAL = New FSKindFacade(user).Retrieve(objCriteria)
                If objFsKindAL.Count <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Function

        Private Function checkFreeService(ByVal objFreeService As FreeService) As Boolean
            Dim objFreeServiceAL As ArrayList
            Dim objCriteria As CriteriaComposite
            Dim user As IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)

            objCriteria = New CriteriaComposite(New Criteria(GetType(FreeService), "FSKind.KindCode", MatchType.Exact, objFreeService.FSKind.KindCode.ToString.Trim()))
            objCriteria.opAnd(New Criteria(GetType(FreeService), "ChassisMaster.ChassisNumber", MatchType.GreaterOrEqual, objFreeService.ChassisMaster.ChassisNumber.ToString.Trim()))
            objCriteria.opAnd(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, DBRowStatus.Active))

            objFreeServiceAL = New FreeServiceFacade(user).Retrieve(objCriteria)

            If objFreeServiceAL.Count <> 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Private Function checkDealer(ByVal objFreeService As FreeService) As Boolean
            Dim objFreeServiceAl As ArrayList
            Dim objCriteria As CriteriaComposite
            Dim user As IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)

            objCriteria = New CriteriaComposite(New Criteria(GetType(FreeService), "Dealer.DealerCode", MatchType.Exact, objFreeService.Dealer.DealerCode.ToString.Trim()))
            objCriteria.opAnd(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, DBRowStatus.Active))

            objFreeServiceAl = New FreeServiceFacade(user).Retrieve(objCriteria)

            If objFreeServiceAl.Count <> 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Private Sub insertFreeService(ByVal objFreeService As FreeService)
            Dim objFreeServiceFacade As FreeServiceFacade = New FreeServiceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            objFreeServiceFacade.Insert(objFreeService)
        End Sub

        Private Sub updateFreeService(ByVal objFreeService As FreeService)
            Dim objFreeServiceFacade As New FreeServiceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim objFreeService2 As FreeService
            Dim objFreeService2Al As ArrayList
            Dim objCriterias As New CriteriaComposite(New Criteria(GetType(FreeService), "Dealer.ID", MatchType.Exact, objFreeService.Dealer.ID))
            objCriterias.opAnd(New Criteria(GetType(FreeService), "FSKind.ID", MatchType.Exact, objFreeService.FSKind.ID))
            objCriterias.opAnd(New Criteria(GetType(FreeService), "ChassisMaster.ID", MatchType.Exact, objFreeService.ChassisMaster.ID))
            objFreeService2Al = objFreeServiceFacade.Retrieve(objCriterias)
            If Not objFreeService2Al.Count = 0 Then
                objFreeService2 = objFreeService2Al.Item(0)
                objFreeService2.Dealer = objFreeService.Dealer
                objFreeService2.ChassisMaster = objFreeService.ChassisMaster
                objFreeService2.FSKind = objFreeService.FSKind
                objFreeService2.ServiceDate = objFreeService.ServiceDate
                objFreeService2.SoldDate = objFreeService.SoldDate
                objFreeService2.MileAge = objFreeService.MileAge
                objFreeService2.NotificationNumber = objFreeService.NotificationNumber
                objFreeService2.NotificationType = objFreeService.NotificationType
                objFreeService2.Reject = objFreeService.Reject
                objFreeService2.Reason = objFreeService.Reason
                objFreeService2.LabourAmount = objFreeService.LabourAmount
                objFreeService2.PartAmount = objFreeService.PartAmount
                objFreeService2.PPNAmount = objFreeService.PPNAmount
                objFreeService2.PPHAmount = objFreeService.PPHAmount
                objFreeService2.Status = objFreeService.Status
                objFreeService2.ReleaseDate = objFreeService.ReleaseDate
                objFreeService2.CashBack = objFreeService.CashBack
                objFreeServiceFacade.Update(objFreeService2)
            Else
                Throw New Exception("Data Not Found")
            End If
        End Sub

        Private Sub UpdateFreeServiceData(ByVal objFreeServiceFromDB As FreeService, ByVal objFreeService As FreeService)
            Dim isChange As New IsChangeFacade
            Dim objFreeServiceFacade As New FreeServiceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

            If isChange.ISchangeFreeService(objFreeService, objFreeServiceFromDB) = True Then

                objFreeServiceFromDB.ServiceDate = objFreeService.ServiceDate
                objFreeServiceFromDB.SoldDate = objFreeService.SoldDate
                objFreeServiceFromDB.MileAge = objFreeService.MileAge
                objFreeServiceFromDB.NotificationNumber = objFreeService.NotificationNumber
                objFreeServiceFromDB.NotificationType = objFreeService.NotificationType
                objFreeServiceFromDB.Reject = objFreeService.Reject
                objFreeServiceFromDB.Reason = objFreeService.Reason
                objFreeServiceFromDB.LabourAmount = objFreeService.LabourAmount
                objFreeServiceFromDB.PartAmount = objFreeService.PartAmount
                objFreeServiceFromDB.PPNAmount = objFreeService.PPNAmount
                objFreeServiceFromDB.PPHAmount = objFreeService.PPHAmount
                objFreeServiceFromDB.Status = objFreeService.Status
                objFreeServiceFromDB.ReleaseDate = objFreeService.ReleaseDate
                objFreeServiceFromDB.FleetRequest = objFreeServiceFromDB.FleetRequest
                objFreeServiceFromDB.CashBack = objFreeService.CashBack
                objFreeServiceFacade.Update(objFreeServiceFromDB)

            End If

        End Sub

        Private Sub synchronizeFreeServiceStatus(ByVal objFreeService As FreeService)
            'If checkChassisAndFsKind(objFreeService) = True Then
            '    If checkFreeService(objFreeService) = True Then
            '        If checkDealer(objFreeService) = True Then
            '            updateFreeService(objFreeService)
            '        End If
            '    Else
            '        insertFreeService(objFreeService)
            '    End If
            'End If

            Dim objFreeServiceFromDB As FreeService = GetFreeServiceByChassisAndKind(objFreeService.ChassisMaster.ID, objFreeService.FSKind.ID, objFreeService.MileAge)
            If Not objFreeServiceFromDB Is Nothing Then
                If objFreeService.Dealer.ID = objFreeServiceFromDB.Dealer.ID Then
                    UpdateFreeServiceData(objFreeServiceFromDB, objFreeService)
                Else
                    Throw New Exception("Invalid Data, ChasisNumber,FSKins Identical but Dealer Different, Dealer Existing : " & objFreeServiceFromDB.Dealer.DealerCode & " ,New Dealer : " & objFreeService.Dealer.DealerCode)
                End If
            Else
                Dim objType() As String = {"EN10", "EF10", "ET10", "EU10", "UD00", "TC00", "UC00", "EV10", "EW10", "WD00", "EC00", "HW00", "HL00", "ML00", "FL00", "MH00", "MF00", "FN00", "TN00", "DK00", "DL00", "DH00", "HH00", "MD0T", "TZ00", "TY00", "TX00", "TW00", "TV00", "TU00", "TS00", "TQ00", "TP00", "TM00", "TR00", "PA00", "PC00", "PD00", "PN00", "PT00"}
                Dim isAllowInsert As Boolean = True

                For i As Integer = 0 To objType.Length - 1
                    If objFreeService.ChassisMaster.VechileColor.VechileType.VechileTypeCode = objType(i) And (objFreeService.FSKind.KindCode = "3" Or objFreeService.FSKind.KindCode = "4" Or objFreeService.FSKind.KindCode = "5") Then
                        isAllowInsert = False
                        Exit For
                    End If
                Next

                If isAllowInsert Then
                    insertFreeService(objFreeService)
                End If
            End If
        End Sub

        Private Function GetFreeServiceByChassisAndKind(ByVal chasisID As Integer, ByVal kindID As Integer, ByVal milage As Integer) As FreeService
            Dim objCriterias As New CriteriaComposite(New Criteria(GetType(FreeService), "FSKind.ID", MatchType.Exact, kindID))
            objCriterias.opAnd(New Criteria(GetType(FreeService), "ChassisMaster.ID", MatchType.Exact, chasisID))
            objCriterias.opAnd(New Criteria(GetType(FreeService), "MileAge", MatchType.Exact, milage))
            objCriterias.opAnd(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, 0))

            Dim objFreeServiceFacade As FreeServiceFacade = New FreeServiceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim FreeServiceList As ArrayList = objFreeServiceFacade.Retrieve(objCriterias)
            If FreeServiceList.Count > 0 Then
                Return CType(FreeServiceList(0), FreeService)
            Else
                Return Nothing
            End If
        End Function

        Private Sub ParseFreeServicePartDetail(ByVal ValParser As String)
            Dim objFSPartDetail As FreeServicePartDetail = New FreeServicePartDetail
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sBuilder = New StringBuilder
            sStart = 0
            nCount = 0
            Dim isReleasDateExist As Boolean = False
            Dim DealerCode As String = String.Empty
            Dim ChassisMasterNo As String = String.Empty
            Dim FSKindCode As String = String.Empty
            Dim User As System.Security.Principal.GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)

            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case 1
                        If Not sTemp.Trim() = "" Then
                            Try
                                DealerCode = sTemp.Trim()
                            Catch ex As Exception
                                sBuilder.Append("Invalid DealerCode " & Chr(13) & Chr(10))
                            End Try
                        End If
                    Case 2
                        If Not sTemp.Trim() = "" Then
                            Try
                                ChassisMasterNo = sTemp.Trim()
                            Catch ex As Exception
                                sBuilder.Append("Invalid ChassisMasterNo " & Chr(13) & Chr(10))
                            End Try
                        End If
                    Case 3
                        If Not sTemp.Trim() = "" Then
                            Try
                                Dim objCriterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                objCriterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, ChassisMasterNo))
                                objCriterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "Dealer.DealerCode", MatchType.Exact, DealerCode))
                                objCriterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "FSKind.KindCode", MatchType.Exact, sTemp.Trim()))
                                Dim sortColl As SortCollection = New SortCollection
                                sortColl.Add(New Sort(GetType(FreeService), "LastUpdateTime", Sort.SortDirection.DESC))

                                Dim arrFreeService As ArrayList = New FreeServiceFacade(User).Retrieve(objCriterias, sortColl)
                                If arrFreeService.Count > 0 Then
                                    objFSPartDetail.FreeService = CType(arrFreeService(0), FreeService)
                                Else
                                    sBuilder.Append("Cannot Find FreeService ID " & Chr(13) & Chr(10))
                                End If
                            Catch ex As Exception
                                sBuilder.Append("Invalid FSKind KindCode " & Chr(13) & Chr(10))
                            End Try
                        End If
                    Case 4
                        If Not sTemp.Trim() = "" Then
                            Try
                                Dim objCriterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                objCriterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartMaster), "PartNumber", MatchType.Exact, sTemp.Trim()))

                                Dim arrSparePartMaster As ArrayList = New SparePartMasterFacade(User).Retrieve(objCriterias)
                                If arrSparePartMaster.Count > 0 Then
                                    objFSPartDetail.SparePartMaster = CType(arrSparePartMaster(0), SparePartMaster)
                                Else
                                    sBuilder.Append("Cannot Find SpartPartMaster ID " & Chr(13) & Chr(10))
                                End If

                            Catch ex As Exception
                                sBuilder.Append("Invalid SparePartNumber " & Chr(13) & Chr(10))
                            End Try
                        End If
                    Case 5
                        If Not sTemp.Trim() = "" Then
                            Try
                                objFSPartDetail.Quantity = CType(sTemp.Trim(), Decimal)
                            Catch ex As Exception
                                sBuilder.Append("Invalid Quantity " & Chr(13) & Chr(10))
                            End Try
                        End If
                    Case 6
                        If Not sTemp.Trim() = "" Then
                            Try
                                objFSPartDetail.PartPrice = CType(sTemp.Trim(), Decimal)
                            Catch ex As Exception
                                sBuilder.Append("Invalid PartPrice " & Chr(13) & Chr(10))
                            End Try
                        End If
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            Dim objCrit As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeServicePartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            objCrit.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServicePartDetail), "FreeService.ID", MatchType.Exact, objFSPartDetail.FreeService.ID))
            objCrit.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServicePartDetail), "SparePartMaster.ID", MatchType.Exact, objFSPartDetail.SparePartMaster.ID))

            Dim sortFSPartDetail As SortCollection = New SortCollection
            sortFSPartDetail.Add(New Sort(GetType(FreeServicePartDetail), "LastUpdateTime", Sort.SortDirection.DESC))

            Dim arrFSPartDetail As ArrayList = New FreeServicePartDetailFacade(User).Retrieve(objCrit, sortFSPartDetail)
            If arrFSPartDetail.Count <= 0 Then
                insertFreeServicePartDetail(objFSPartDetail)
            Else
                Dim extFSPartDetail As FreeServicePartDetail = New FreeServicePartDetail
                extFSPartDetail = CType(arrFSPartDetail(0), FreeServicePartDetail)
                objFSPartDetail.ID = extFSPartDetail.ID
                updateFreeServicePartDetail(objFSPartDetail)
            End If

        End Sub

        Private Sub insertFreeServicePartDetail(ByVal oFSPartDEtail As FreeServicePartDetail)
            Dim objFSPartDetailFacade As FreeServicePartDetailFacade = New FreeServicePartDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            objFSPartDetailFacade.Insert(oFSPartDEtail)
        End Sub

        Private Sub updateFreeServicePartDetail(ByVal oFSPartDEtail As FreeServicePartDetail)
            Dim objFSPartDetailFacade As FreeServicePartDetailFacade = New FreeServicePartDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            objFSPartDetailFacade.Update(oFSPartDEtail)
        End Sub
#End Region

    End Class

End Namespace
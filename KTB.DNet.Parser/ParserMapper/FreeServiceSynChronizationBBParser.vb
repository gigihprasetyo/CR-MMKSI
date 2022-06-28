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
#End Region

Namespace KTB.DNet.Parser

    Public Class FreeServiceSynChronizationBBParser
        Inherits AbstractParser

#Region "Private Variables"

        Private status As String
        Private objStreamReader As StreamReader
        Private objFreeServiceBBAL As ArrayList
        Private _fileName As String
        Private Grammar As Regex
        Private sBuilder As StringBuilder

#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser(",")
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _fileName = fileName
            Me.objStreamReader = New StreamReader(fileName, True)
            Me.objFreeServiceBBAL = New ArrayList
            Dim val As String = MyBase.NextLine(Me.objStreamReader).Trim()
            While (Not val = "")
                Try
                    parseFreeServiceBBSynChronization(val + ",")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "FreeServiceBBSynChronizationParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.FreeServiceSynChronizationParser, BlockName)
                    Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(Me.objStreamReader)
            End While
            Me.objStreamReader.Close()
            Me.objStreamReader = Nothing
            Return Me.objFreeServiceBBAL
        End Function

        Protected Overrides Function doParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

        Protected Overrides Function doTransaction() As Integer
            For Each objFreeServiceBB As FreeServiceBB In Me.objFreeServiceBBAL
                Try
                    synchronizeFreeServiceBBStatus(objFreeServiceBB)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "FreeServiceBBSynChronization", "FreeServiceBBSynChronizationParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.FreeServiceSynChronizationParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objFreeServiceBB.ChassisMasterBB.ChassisNumber & " - " & objFreeServiceBB.FSKind.KindCode & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
        End Function

#End Region

#Region "Private Methods"

        Private Sub parseFreeServiceBBSynChronization(ByVal ValParser As String)
            Dim objFreeServiceBB As FreeServiceBB = New FreeServiceBB
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
                    Case 0
                        Dim objDealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                        If objDealer.ID > 0 Then
                            objFreeServiceBB.Dealer = objDealer
                        Else
                            sBuilder.Append("Invalid Dealer" & Chr(13) & Chr(10))
                        End If
                    Case 1
                        Dim objChasis As ChassisMasterBB = New ChassisMasterBBFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                        If objChasis.ID > 0 Then
                            objFreeServiceBB.ChassisMasterBB = objChasis
                        Else
                            sBuilder.Append("Invalid Chassis Master " & Chr(13) & Chr(10))
                        End If
                    Case 2
                        Dim objFsKind As FSKind = New FSKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                        If objFsKind.ID > 0 Then
                            objFreeServiceBB.FSKind = objFsKind
                        Else
                            sBuilder.Append("Invalid Chassis Kind " & Chr(13) & Chr(10))
                        End If
                    Case 3
                        Try
                            objFreeServiceBB.ServiceDate = CDate(sTemp.Substring(2, 2).ToString & "/" & sTemp.Substring(0, 2) & "/" & sTemp.Substring(4, 4))
                        Catch ex As Exception
                            sBuilder.Append("Invalid Service Date " & Chr(13) & Chr(10))
                        End Try
                    Case 4
                        Try
                            objFreeServiceBB.SoldDate = CDate(sTemp.Substring(2, 2).ToString & "/" & sTemp.Substring(0, 2) & "/" & sTemp.Substring(4, 4))
                        Catch ex As Exception
                            objFreeServiceBB.SoldDate = DateSerial(1900, 1, 1)
                            'sBuilder.Append("Invalid Sold Date " & Chr(13) & Chr(10))
                        End Try
                    Case 5
                        If Not sTemp.Trim = "" Then
                            Try
                                objFreeServiceBB.MileAge = CInt(sTemp.Trim())
                            Catch ex As Exception
                                sBuilder.Append("Invalid MileAge " & Chr(13) & Chr(10))
                            End Try
                        Else
                            objFreeServiceBB.MileAge = 0
                        End If
                    Case 6
                        If Not sTemp.Trim() = "" Then
                            Try
                                'objFreeServiceBB.NotificationNumber = CInt(sTemp.Trim())
                                objFreeServiceBB.NotificationNumber = sTemp.Trim()
                            Catch ex As Exception
                                objFreeServiceBB.NotificationNumber = 0
                                'sBuilder.Append("Invalid Notification Number " & Chr(13) & Chr(10))
                            End Try
                        Else
                            objFreeServiceBB.NotificationNumber = 0
                        End If
                    Case 7
                        objFreeServiceBB.NotificationType = sTemp.Trim()
                    Case 8
                        If sTemp.ToUpper = "APP" Or sTemp.ToUpper = "DAPP" Then
                            objFreeServiceBB.Reject = sTemp.Trim()
                        Else
                            objFreeServiceBB.Reject = ""
                            'sBuilder.Append("Invalid Reject Type Must APP or DAPP " & Chr(13) & Chr(10))
                        End If
                    Case 9
                        Dim objCriterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Reason), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        objCriterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Reason), "ReasonCode", MatchType.Exact, sTemp.Trim))
                        Dim objReasonAL As ArrayList = New ReasonFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(objCriterias)
                        If objReasonAL.Count > 0 Then
                            objFreeServiceBB.Reason = CType(objReasonAL.Item(0), Reason)
                        End If
                    Case 10
                        If Not sTemp.Trim() = "" Then
                            Try
                                objFreeServiceBB.LabourAmount = CInt(sTemp.Trim())
                            Catch ex As Exception
                                objFreeServiceBB.LabourAmount = 0
                                'sBuilder.Append("Invalid LabourAmount " & Chr(13) & Chr(10))
                            End Try
                        Else
                            objFreeServiceBB.LabourAmount = 0
                        End If
                    Case 11
                        If Not sTemp.Trim() = "" Then
                            Try
                                objFreeServiceBB.PartAmount = CInt(sTemp.Trim())
                            Catch ex As Exception
                                objFreeServiceBB.PartAmount = 0
                                'sBuilder.Append("Invalid PartAmount " & Chr(13) & Chr(10))
                            End Try
                        Else
                            objFreeServiceBB.PartAmount = 0
                        End If
                    Case 12
                        If Not sTemp.Trim() = "" Then
                            Try
                                objFreeServiceBB.PPNAmount = CInt(sTemp.Trim())
                            Catch ex As Exception
                                objFreeServiceBB.PPNAmount = 0
                                'sBuilder.Append("Invalid PPNAmount " & Chr(13) & Chr(10))
                            End Try
                        Else
                            objFreeServiceBB.PPNAmount = 0
                        End If
                    Case 13
                        If Not sTemp.Trim() = "" Then
                            Try
                                objFreeServiceBB.PPHAmount = CInt(sTemp.Trim())
                            Catch ex As Exception
                                objFreeServiceBB.PPHAmount = 0
                                'sBuilder.Append("Invalid PPHAmount " & Chr(13) & Chr(10))
                            End Try
                        Else
                            objFreeServiceBB.PPHAmount = 0
                        End If

                    Case 14
                        Dim FSReleaseDate As DateTime
                        isReleasDateExist = True
                        Try
                            If sTemp.Length > 0 Then
                                FSReleaseDate = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2))
                            Else
                                FSReleaseDate = System.DateTime.Now
                            End If
                            objFreeServiceBB.ReleaseDate = FSReleaseDate
                        Catch ex As Exception
                            objFreeServiceBB.ReleaseDate = DateSerial(1900, 1, 1)
                            'sBuilder.Append("Invalid PDI Date " & sTemp & Chr(13) & Chr(10))
                        End Try
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If isReleasDateExist = False Then
                objFreeServiceBB.ReleaseDate = System.DateTime.Now
            End If


            If sBuilder.Length > 0 Then
                Throw New Exception(sBuilder.ToString)
            Else
                objFreeServiceBB.TotalAmount = getTotalAmount(objFreeServiceBB)
                objFreeServiceBB.Status = CStr(EnumFSStatus.FSStatus.Selesai)
                Me.objFreeServiceBBAL.Add(objFreeServiceBB)
            End If
        End Sub

        Private Function getFreeServiceBBList(ByVal code As String) As FreeServiceBB
            Dim userPrinciple As IPrincipal
            Dim objFreeServiceBBFacade As New FreeServiceBBFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim objFreeServiceBB As FreeServiceBB = objFreeServiceBBFacade.Retrieve(code)
            Return objFreeServiceBB
        End Function

        Private Function getTotalAmount(ByVal objFreeServiceBB As FreeServiceBB)
            Dim dblTotalAmount, dblLabourAmount, dblPartAmount, dblPPNAmount, dblPPHAmount As Double
            If Not IsNothing(objFreeServiceBB.LabourAmount) Then
                dblLabourAmount = objFreeServiceBB.LabourAmount
            Else
                dblLabourAmount = 0
            End If

            If Not IsNothing(objFreeServiceBB.PartAmount) Then
                dblPartAmount = objFreeServiceBB.PartAmount
            Else
                dblPartAmount = 0
            End If

            If Not IsNothing(objFreeServiceBB.PPNAmount) Then
                dblPPNAmount = objFreeServiceBB.PPNAmount
            Else
                dblPPNAmount = 0
            End If

            If Not IsNothing(objFreeServiceBB.PPNAmount) Then
                dblPPNAmount = objFreeServiceBB.PPNAmount
            Else
                dblPPNAmount = 0
            End If

            dblTotalAmount = dblLabourAmount + dblPartAmount + dblPPHAmount + dblPPNAmount

            Return dblTotalAmount

        End Function

        Private Function checkChassisAndFsKind(ByVal objFreeServiceBB As FreeServiceBB) As Boolean
            Dim user As IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim objChassisMasterBBAL As ArrayList
            Dim objFsKindAL As ArrayList
            Dim objCriteria As CriteriaComposite
            objCriteria = New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "ChassisNumber", MatchType.Exact, objFreeServiceBB.ChassisMasterBB.ChassisNumber.ToString.Trim()))
            objChassisMasterBBAL = New ChassisMasterBBFacade(user).Retrieve(objCriteria)
            If objChassisMasterBBAL.Count <> 0 Then
                objCriteria = New CriteriaComposite(New Criteria(GetType(FSKind), "KindCode", MatchType.Exact, objFreeServiceBB.FSKind.KindCode.ToString.Trim()))
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

        Private Function checkFreeServiceBB(ByVal objFreeServiceBB As FreeServiceBB) As Boolean
            Dim objFreeServiceBBAL As ArrayList
            Dim objCriteria As CriteriaComposite
            Dim user As IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)

            objCriteria = New CriteriaComposite(New Criteria(GetType(FreeServiceBB), "FSKind.KindCode", MatchType.Exact, objFreeServiceBB.FSKind.KindCode.ToString.Trim()))
            objCriteria.opAnd(New Criteria(GetType(FreeServiceBB), "ChassisMasterBB.ChassisNumber", MatchType.GreaterOrEqual, objFreeServiceBB.ChassisMasterBB.ChassisNumber.ToString.Trim()))
            objCriteria.opAnd(New Criteria(GetType(FreeServiceBB), "RowStatus", MatchType.Exact, DBRowStatus.Active))

            objFreeServiceBBAL = New FreeServiceBBFacade(user).Retrieve(objCriteria)

            If objFreeServiceBBAL.Count <> 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Private Function checkDealer(ByVal objFreeServiceBB As FreeServiceBB) As Boolean
            Dim objFreeServiceBBAl As ArrayList
            Dim objCriteria As CriteriaComposite
            Dim user As IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)

            objCriteria = New CriteriaComposite(New Criteria(GetType(FreeServiceBB), "Dealer.DealerCode", MatchType.Exact, objFreeServiceBB.Dealer.DealerCode.ToString.Trim()))
            objCriteria.opAnd(New Criteria(GetType(FreeServiceBB), "RowStatus", MatchType.Exact, DBRowStatus.Active))

            objFreeServiceBBAl = New FreeServiceBBFacade(user).Retrieve(objCriteria)

            If objFreeServiceBBAl.Count <> 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Private Sub insertFreeServiceBB(ByVal objFreeServiceBB As FreeServiceBB)
            Dim objFreeServiceBBFacade As FreeServiceBBFacade = New FreeServiceBBFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            objFreeServiceBBFacade.Insert(objFreeServiceBB)
        End Sub

        Private Sub updateFreeServiceBB(ByVal objFreeServiceBB As FreeServiceBB)
            Dim objFreeServiceBBFacade As New FreeServiceBBFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim objFreeServiceBB2 As FreeServiceBB
            Dim objFreeServiceBB2Al As ArrayList
            Dim objCriterias As New CriteriaComposite(New Criteria(GetType(FreeServiceBB), "Dealer.ID", MatchType.Exact, objFreeServiceBB.Dealer.ID))
            objCriterias.opAnd(New Criteria(GetType(FreeServiceBB), "FSKind.ID", MatchType.Exact, objFreeServiceBB.FSKind.ID))
            objCriterias.opAnd(New Criteria(GetType(FreeServiceBB), "ChassisMasterBB.ID", MatchType.Exact, objFreeServiceBB.ChassisMasterBB.ID))
            objFreeServiceBB2Al = objFreeServiceBBFacade.Retrieve(objCriterias)
            If Not objFreeServiceBB2Al.Count = 0 Then
                objFreeServiceBB2 = objFreeServiceBB2Al.Item(0)
                objFreeServiceBB2.Dealer = objFreeServiceBB.Dealer
                objFreeServiceBB2.ChassisMasterBB = objFreeServiceBB.ChassisMasterBB
                objFreeServiceBB2.FSKind = objFreeServiceBB.FSKind
                objFreeServiceBB2.ServiceDate = objFreeServiceBB.ServiceDate
                objFreeServiceBB2.SoldDate = objFreeServiceBB.SoldDate
                objFreeServiceBB2.MileAge = objFreeServiceBB.MileAge
                objFreeServiceBB2.NotificationNumber = objFreeServiceBB.NotificationNumber
                objFreeServiceBB2.NotificationType = objFreeServiceBB.NotificationType
                objFreeServiceBB2.Reject = objFreeServiceBB.Reject
                objFreeServiceBB2.Reason = objFreeServiceBB.Reason
                objFreeServiceBB2.LabourAmount = objFreeServiceBB.LabourAmount
                objFreeServiceBB2.PartAmount = objFreeServiceBB.PartAmount
                objFreeServiceBB2.PPNAmount = objFreeServiceBB.PPNAmount
                objFreeServiceBB2.PPHAmount = objFreeServiceBB.PPHAmount
                objFreeServiceBB2.Status = objFreeServiceBB.Status
                objFreeServiceBB2.ReleaseDate = objFreeServiceBB.ReleaseDate
                objFreeServiceBBFacade.Update(objFreeServiceBB2)
            Else
                Throw New Exception("Data Not Found")
            End If
        End Sub

        Private Sub UpdateFreeServiceBBData(ByVal objFreeServiceBBFromDB As FreeServiceBB, ByVal objFreeServiceBB As FreeServiceBB)
            Dim objFreeServiceBBFacade As New FreeServiceBBFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            objFreeServiceBBFromDB.ServiceDate = objFreeServiceBB.ServiceDate
            objFreeServiceBBFromDB.SoldDate = objFreeServiceBB.SoldDate
            objFreeServiceBBFromDB.MileAge = objFreeServiceBB.MileAge
            objFreeServiceBBFromDB.NotificationNumber = objFreeServiceBB.NotificationNumber
            objFreeServiceBBFromDB.NotificationType = objFreeServiceBB.NotificationType
            objFreeServiceBBFromDB.Reject = objFreeServiceBB.Reject
            objFreeServiceBBFromDB.Reason = objFreeServiceBB.Reason
            objFreeServiceBBFromDB.LabourAmount = objFreeServiceBB.LabourAmount
            objFreeServiceBBFromDB.PartAmount = objFreeServiceBB.PartAmount
            objFreeServiceBBFromDB.PPNAmount = objFreeServiceBB.PPNAmount
            objFreeServiceBBFromDB.PPHAmount = objFreeServiceBB.PPHAmount
            objFreeServiceBBFromDB.Status = objFreeServiceBB.Status
            objFreeServiceBBFromDB.ReleaseDate = objFreeServiceBB.ReleaseDate
            objFreeServiceBBFacade.Update(objFreeServiceBBFromDB)
        End Sub

        Private Sub synchronizeFreeServiceBBStatus(ByVal objFreeServiceBB As FreeServiceBB)
            'If checkChassisAndFsKind(objFreeServiceBB) = True Then
            '    If checkFreeServiceBB(objFreeServiceBB) = True Then
            '        If checkDealer(objFreeServiceBB) = True Then
            '            updateFreeServiceBB(objFreeServiceBB)
            '        End If
            '    Else
            '        insertFreeServiceBB(objFreeServiceBB)
            '    End If
            'End If

            Dim objFreeServiceBBFromDB As FreeServiceBB = GetFreeServiceBBByChassisAndKind(objFreeServiceBB.ChassisMasterBB.ID, objFreeServiceBB.FSKind.ID)
            If Not objFreeServiceBBFromDB Is Nothing Then
                If objFreeServiceBB.Dealer.ID = objFreeServiceBBFromDB.Dealer.ID Then
                    UpdateFreeServiceBBData(objFreeServiceBBFromDB, objFreeServiceBB)
                Else
                    Throw New Exception("Invalid Data, ChasisNumber,FSKins Identical but Dealer Different, Dealer Existing : " & objFreeServiceBBFromDB.Dealer.DealerCode & " ,New Dealer : " & objFreeServiceBB.Dealer.DealerCode)
                End If
            Else
                Dim objType() As String = {"EN10", "EF10", "ET10", "EU10", "UD00", "TC00", "UC00", "EV10", "EW10", "WD00", "EC00", "HW00", "HL00", "ML00", "FL00", "MH00", "MF00", "FN00", "TN00", "DK00", "DL00", "DH00", "HH00", "MD0T", "TZ00", "TY00", "TX00", "TW00", "TV00", "TU00", "TS00", "TQ00", "TP00", "TM00", "TR00", "PA00", "PC00", "PD00", "PN00", "PT00"}
                Dim isAllowInsert As Boolean = True

                For i As Integer = 0 To objType.Length - 1
                    If objFreeServiceBB.ChassisMasterBB.VechileColor.VechileType.VechileTypeCode = objType(i) And (objFreeServiceBB.FSKind.KindCode = "3" Or objFreeServiceBB.FSKind.KindCode = "4" Or objFreeServiceBB.FSKind.KindCode = "5") Then
                        isAllowInsert = False
                        Exit For
                    End If
                Next

                If isAllowInsert Then
                    insertFreeServiceBB(objFreeServiceBB)
                End If
            End If
        End Sub

        Private Function GetFreeServiceBBByChassisAndKind(ByVal chasisID As Integer, ByVal kindID As Integer) As FreeServiceBB
            Dim objCriterias As New CriteriaComposite(New Criteria(GetType(FreeServiceBB), "FSKind.ID", MatchType.Exact, kindID))
            objCriterias.opAnd(New Criteria(GetType(FreeServiceBB), "ChassisMasterBB.ID", MatchType.Exact, chasisID))
            objCriterias.opAnd(New Criteria(GetType(FreeServiceBB), "RowStatus", MatchType.Exact, 0))

            Dim objFreeServiceBBFacade As FreeServiceBBFacade = New FreeServiceBBFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim FreeServiceBBList As ArrayList = objFreeServiceBBFacade.Retrieve(objCriterias)
            If FreeServiceBBList.Count > 0 Then
                Return CType(FreeServiceBBList(0), FreeServiceBB)
            Else
                Return Nothing
            End If
        End Function

#End Region

    End Class

End Namespace
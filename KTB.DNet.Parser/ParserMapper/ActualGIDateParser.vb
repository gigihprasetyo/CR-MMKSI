
#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
#End Region

Namespace KTB.DNet.Parser

    Public Class ActualGIDateParser
        Inherits AbstractParser

#Region "Private Variables"

        Private status As String
        Private objStreamReader As StreamReader
        Private DOStatusListAL As ArrayList
        Private Grammar As Regex
        Private _fileName As String
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _fileName = fileName
            Me.objStreamReader = New StreamReader(fileName, True)
            Me.DOStatusListAL = New ArrayList
            Dim val As String = MyBase.NextLine(Me.objStreamReader).Trim()
            While (Not val = "")
                Try
                    parseDOStatusList(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "ActualGIDateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ActualGIDateParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(Me.objStreamReader)
            End While
            Me.objStreamReader.Close()
            Me.objStreamReader = Nothing
            Return Me.DOStatusListAL
        End Function

        Protected Overrides Function doParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

        Protected Overrides Function doTransaction() As Integer
            If Me.DOStatusListAL.Count > 0 Then
                For Each item As ChassisMaster In Me.DOStatusListAL
                    Try
                        'If Not IsExistCode(item.ChassisNumber) Then
                        '    Throw New Exception("Chasis Number not found.")
                        'Else
                        Try
                            updateGIDate(item)
                        Catch ex As Exception
                            System.Threading.Thread.CurrentThread.Sleep(4000)
                            updateGIDate(item)
                        End Try
                        'End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "ActualGIDateParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ActualGIDateParser, BlockName)
                        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.ChassisNumber & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    End Try
                Next
            End If
            Return 0
        End Function


        Private Sub updateGIDate(ByVal objChassisMaster As ChassisMaster)
            Dim criterias As CriteriaComposite
            Dim objChassisMaster2 As ChassisMaster
            Dim objAL2 As ArrayList

            Dim objChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            criterias = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, objChassisMaster.ChassisNumber))

            Dim sortColl As SortCollection = New SortCollection

            sortColl.Add(New Sort(GetType(ChassisMaster), "RowStatus", Sort.SortDirection.DESC))

            objAL2 = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias, sortColl)

            If objAL2.Count > 0 Then
                objChassisMaster2 = CType(objAL2.Item(0), ChassisMaster)
            End If

            objChassisMaster2.IsChangedWSM = False
            If objChassisMaster2.GIDate <> objChassisMaster.GIDate Then
                objChassisMaster2.GIDate = objChassisMaster.GIDate
                objChassisMaster2.IsChangedWSM = True
            End If
            If objChassisMaster2.ParkingDays <> objChassisMaster.ParkingDays Then
                objChassisMaster2.ParkingDays = objChassisMaster.ParkingDays
                objChassisMaster2.IsChangedWSM = True
            End If
            If objChassisMaster2.ParkingAmount <> objChassisMaster.ParkingAmount Then
                objChassisMaster2.ParkingAmount = objChassisMaster.ParkingAmount
                objChassisMaster2.IsChangedWSM = True
            End If

            If objChassisMaster2.IsChangedWSM Then
                objChassisMasterFacade.Update(objChassisMaster2)
            End If

        End Sub

        Private Function IsExistCode(ByVal sCode As String) As Boolean

            Dim objChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Return objChassisMasterFacade.ValidateCode(sCode) > 0

        End Function

#End Region

#Region "Private Methods"

        Private Sub parseDOStatusList(ByVal ValParser As String)

            Dim objChassisMaster As ChassisMaster = New ChassisMaster
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim ArryList, ArryList2 As ArrayList
            sStart = 0
            nCount = 0

            If getNumberOfColumns(ValParser) = 3 Then

                For Each m As Match In Grammar.Matches(ValParser)

                    sTemp = ValParser.Substring(sStart, m.Index - sStart)
                    sTemp = sTemp.Trim("""")
                    sTemp = sTemp.Trim()

                    Select Case (nCount)
                        Case 0
                            objChassisMaster.ChassisNumber = sTemp.Trim()
                        Case 1
                            'Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "ChassisNumber", _
                            '    MatchType.Exact, objChassisMaster.ChassisNumber))
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, objChassisMaster.ChassisNumber))

                            Dim objAl As ArrayList
                            Dim objChassisMaster2 As New ChassisMaster
                            Dim GIDate As Date

                            objAl = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

                            If objAl.Count <> 0 Then

                                objChassisMaster2 = CType(objAL.Item(0), ChassisMaster)
                                GIDate = objChassisMaster2.GIDate

                                'objChassisMaster.GIDate = Left(sTemp, 2) + "/" + Mid(sTemp, 2, 2) + "/" + Right(sTemp.Trim, 2)
                                objChassisMaster.GIDate = sTemp.Substring(2, 2) & "/" & sTemp.Substring(0, 2) & "/" & sTemp.Substring(4, 4)

                                If Not (objChassisMaster2.DODate = System.DateTime.MinValue) And objChassisMaster2.DODate <= objChassisMaster.GIDate Then

                                    If IsNothing(objChassisMaster.GIDate) Then
                                        If IsNothing(GIDate) Or GIDate = System.DateTime.MinValue Then
                                            objChassisMaster.ParkingDays = (Today.Date.Subtract(objChassisMaster2.DODate).TotalDays) + 1
                                        Else
                                            objChassisMaster.ParkingDays = (GIDate.Subtract(objChassisMaster2.DODate).TotalDays) + 1
                                        End If
                                    Else
                                        objChassisMaster.ParkingDays = (objChassisMaster.GIDate.Subtract(objChassisMaster2.DODate).TotalDays) + 1
                                    End If

                                    Dim parser As New ActualGIDateParser
                                    Dim temp As Integer = objChassisMaster.ParkingDays

                                    temp -= 10
                                    objChassisMaster.PenaltyParkingDays = temp

                                    Dim amount As Long = 0

                                    If (objChassisMaster2.DODate < New DateTime(2012, 9, 1, 0, 0, 0)) Then
                                        'amount += PenaltyCalculation(temp, objChassisMaster)
                                        If temp >= 0 Then

                                            Dim iDays1 As Integer = DateDiff(DateInterval.Day, objChassisMaster2.DODate, New DateTime(2012, 9, 1, 0, 0, 0))
                                            Dim iDays2 As Integer = DateDiff(DateInterval.Day, New DateTime(2012, 9, 1, 0, 0, 0), New DateTime(2012, 11, 1, 0, 0, 0))
                                            Dim iDays3 As Integer = DateDiff(DateInterval.Day, New DateTime(2012, 11, 1, 0, 0, 0), New DateTime(2013, 1, 1, 0, 0, 0))

                                            Dim temp1 As Integer = iDays1 - 10
                                            If (temp1 > 0) Then
                                                If (temp1 <= 20) Then
                                                    amount += temp1 * 10000
                                                    temp1 = 20 - temp1
                                                    objChassisMaster.PenaltyParkingDays = temp1
                                                    amount += parser.SepToOct12(objChassisMaster, True, objChassisMaster2)
                                                Else
                                                    amount += ((temp1 - 20) * 20000) + 200000
                                                End If
                                            End If
                                            Dim temp2 As Integer = iDays2
                                            If temp1 > 0 Then
                                                If iDays2 > temp1 Then
                                                    If temp1 > 20 Then
                                                        temp2 = iDays2
                                                    Else
                                                        temp2 = iDays2 - temp1
                                                    End If
                                                End If
                                            Else
                                                temp2 = iDays2 - Math.Abs(temp1)
                                            End If
                                            If temp2 > 0 Then
                                                If temp1 < 0 Then
                                                    objChassisMaster.ParkingDays = temp2 + 5
                                                    amount += parser.SepToOct12(objChassisMaster, False, objChassisMaster2)
                                                Else
                                                    objChassisMaster.PenaltyParkingDays = temp2
                                                    amount += parser.SepToOct12(objChassisMaster, True, objChassisMaster2)
                                                End If
                                            End If
                                            Dim temp3 As Integer = 0
                                            If temp1 < 0 Then
                                                temp3 = temp - temp2
                                            Else
                                                temp3 = temp - temp2 - (iDays1 - 10)
                                                If temp1 <= 20 Then
                                                    temp3 -= temp1
                                                End If
                                            End If
                                            If temp3 > 0 Then
                                                objChassisMaster.PenaltyParkingDays = temp3
                                                amount += parser.NocToDec12(objChassisMaster, True, objChassisMaster2)
                                            End If
                                            objChassisMaster.ParkingDays = temp + 10
                                            objChassisMaster.PenaltyParkingDays = temp
                                            objChassisMaster.ParkingAmount = amount
                                        Else
                                            objChassisMaster.PenaltyParkingDays = 0
                                            objChassisMaster.ParkingAmount = 0
                                        End If

                                    ElseIf (objChassisMaster2.DODate >= New DateTime(2012, 9, 1, 0, 0, 0) And objChassisMaster2.DODate <= New DateTime(2012, 10, 31, 23, 59, 59)) Or (objChassisMaster2.DODate >= New DateTime(2013, 1, 1, 0, 0, 0)) Then
                                        objChassisMaster.ParkingAmount = parser.SepToOct12(objChassisMaster, False, objChassisMaster2)
                                    ElseIf objChassisMaster2.DODate >= New DateTime(2012, 11, 1, 0, 0, 0) And objChassisMaster2.DODate <= New DateTime(2012, 12, 31, 23, 59, 59) Then
                                        objChassisMaster.ParkingAmount = parser.NocToDec12(objChassisMaster, False, objChassisMaster2)
                                    End If
                                    If objChassisMaster2.DONumber.Substring(0, 3) = "125" Then
                                        objChassisMaster.ParkingDays = 0
                                        objChassisMaster.PenaltyParkingDays = 0
                                        objChassisMaster.ParkingAmount = 0
                                    End If
                                Else
                                    Throw New Exception("Ada kesalahan data DO pada " + objChassisMaster.ChassisNumber)
                                End If

                            Else
                                Throw New Exception(objChassisMaster.ChassisNumber + " tidak terdaftar")
                            End If

                    End Select

                    sStart = m.Index + 1
                    nCount += 1

                Next

                Me.DOStatusListAL.Add(objChassisMaster)

            Else
                For Each m As Match In Grammar.Matches(ValParser)

                    sTemp = ValParser.Substring(sStart, m.Index - sStart)
                    sTemp = sTemp.Trim("""")
                    sTemp = sTemp.Trim()

                    Select Case (nCount)
                        Case Is = 1
                            objChassisMaster.ChassisNumber = sTemp.Trim
                        Case Is = 2

                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Category), "CategoryCode", MatchType.Exact, sTemp.Trim))
                            Dim ArryList3 = New TermOfPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If Not IsNothing(ArryList3) Then
                                For Each ObjCategory As Category In ArryList3
                                    objChassisMaster.Category = ObjCategory
                                Next
                            End If


                            'objChassisMaster.Category = sTemp.Trim
                        Case Is = 3
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "MaterialNumber", MatchType.Exact, sTemp.Trim))
                            ArryList = New VechileColorFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

                            If Not IsNothing(ArryList) Then
                                For Each ObjVechile As VechileColor In ArryList
                                    objChassisMaster.VechileColor = ObjVechile
                                Next
                            End If

                        Case Is = 4
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, sTemp.Trim))
                            ArryList2 = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)


                            Dim objDealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            objChassisMaster.Dealer = objDealer

                        Case Is = 5
                            Dim tgl As String
                            tgl = sTemp.Substring(2, 2).ToString & "/" & sTemp.Substring(0, 2) & "/" & sTemp.Substring(4, 4)
                            objChassisMaster.DODate = tgl

                            Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, objChassisMaster.ChassisNumber))
                            Dim objAl As ArrayList
                            Dim objChassisMaster2 As New ChassisMaster
                            Dim GIDate As Date


                            objAl = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

                            If objAl.Count > 0 Then
                                objChassisMaster2 = CType(objAl.Item(0), ChassisMaster)
                                GIDate = objChassisMaster2.GIDate
                            Else
                                Throw New Exception(objChassisMaster.ChassisNumber + " tidak terdaftar")
                            End If

                            If IsNothing(GIDate) Or GIDate = "1/1/1900" Then
                                objChassisMaster.ParkingDays = Today.Date.Subtract(objChassisMaster.DODate).TotalDays
                            Else
                                objChassisMaster.ParkingDays = GIDate.Subtract(objChassisMaster.DODate).TotalDays
                            End If

                            If objChassisMaster.ParkingDays < 10 Then
                                objChassisMaster.ParkingAmount = 0
                            ElseIf objChassisMaster.ParkingDays > 10 And objChassisMaster.ParkingDays < 20 Then
                                objChassisMaster.ParkingAmount = (objChassisMaster.ParkingAmount - 10) * 10000
                            Else
                                objChassisMaster.ParkingAmount = ((objChassisMaster.ParkingAmount - 10) * 10000) + ((objChassisMaster.ParkingAmount - 20) * 20000)
                            End If
                        Case Is = 6
                            objChassisMaster.DONumber = sTemp.Trim
                        Case Is = 7
                            objChassisMaster.SONumber = sTemp.Trim
                        Case Is = 8
                            objChassisMaster.PONumber = sTemp.Trim
                        Case Is = 9
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TermOfPayment), "TermOfPaymentCode", MatchType.Exact, sTemp.Trim))
                            Dim ArryList3 = New TermOfPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If ArryList3.Count > 0 Then
                                objChassisMaster.TermOfPayment = ArryList3(0)
                            End If
                            'objChassisMaster.TOPayment = sTemp.Trim
                        Case Is = 10
                            Try
                                objChassisMaster.DiscountAmount = sTemp.Trim
                            Catch ex As Exception
                                Throw ex
                            End Try
                        Case Is = 11
                            objChassisMaster.EngineNumber = sTemp.Trim
                        Case Is = 12
                            objChassisMaster.SerialNumber = sTemp.Trim
                        Case Else
                    End Select

                    sStart = m.Index + 1
                    nCount += 1
                Next

                If ArryList2.Count > 0 Then
                    objChassisMaster.GIDate = New Date(1900, 1, 1)
                    Me.DOStatusListAL.Add(objChassisMaster)
                End If

            End If

        End Sub

        Public Function SepToOct12(ByVal objChassisMaster As ChassisMaster, ByVal isContinued As Boolean, Optional ByVal objChassisMaster2 As ChassisMaster = Nothing) As Long
            Dim amount As Long = 0
            If objChassisMaster2.Category.CategoryCode = "PC" Then
                If isContinued Then
                    If objChassisMaster.PenaltyParkingDays > 0 And objChassisMaster.PenaltyParkingDays <= 20 Then
                        amount = objChassisMaster.PenaltyParkingDays * 10000
                    ElseIf objChassisMaster.PenaltyParkingDays > 20 Then
                        amount = objChassisMaster.PenaltyParkingDays * 20000
                    Else
                        amount = 0
                    End If
                Else
                    objChassisMaster.PenaltyParkingDays = objChassisMaster.ParkingDays - 5
                    If objChassisMaster.PenaltyParkingDays > 0 And objChassisMaster.PenaltyParkingDays <= 20 Then
                        amount = objChassisMaster.PenaltyParkingDays * 10000
                    ElseIf objChassisMaster.PenaltyParkingDays > 20 Then
                        amount = (20 * 10000) + (objChassisMaster.PenaltyParkingDays - 20) * 20000
                    Else
                        amount = 0
                    End If
                End If
            ElseIf (objChassisMaster2.Category.CategoryCode = "LCV") Or (objChassisMaster2.Category.CategoryCode = "CV") Then
                If isContinued Then
                    If objChassisMaster.PenaltyParkingDays > 0 And objChassisMaster.PenaltyParkingDays <= 20 Then
                        amount = objChassisMaster.PenaltyParkingDays * 25000
                    ElseIf objChassisMaster.PenaltyParkingDays > 20 Then
                        amount = objChassisMaster.PenaltyParkingDays * 40000
                    Else
                        amount = 0
                    End If
                Else
                    objChassisMaster.PenaltyParkingDays = objChassisMaster.ParkingDays - 5
                    If objChassisMaster.PenaltyParkingDays > 0 And objChassisMaster.PenaltyParkingDays <= 20 Then
                        amount = objChassisMaster.PenaltyParkingDays * 25000
                    ElseIf objChassisMaster.PenaltyParkingDays > 20 Then
                        amount = (20 * 25000) + (objChassisMaster.PenaltyParkingDays - 20) * 40000
                    Else
                        amount = 0
                    End If
                End If

            Else
                amount = 0
            End If
            Return amount
        End Function

        Public Function NocToDec12(ByVal objChassisMaster As ChassisMaster, ByVal isContinued As Boolean, Optional ByVal objChassisMaster2 As ChassisMaster = Nothing) As Long
            Dim amount As Long = 0
            If objChassisMaster2.Category.CategoryCode = "PC" Then
                If isContinued Then
                    If objChassisMaster.PenaltyParkingDays > 0 And objChassisMaster.PenaltyParkingDays <= 20 Then
                        amount = objChassisMaster.PenaltyParkingDays * 10000
                    ElseIf objChassisMaster.PenaltyParkingDays > 20 Then
                        amount = objChassisMaster.PenaltyParkingDays * 20000
                    Else
                        amount = 0
                    End If
                Else
                    objChassisMaster.PenaltyParkingDays = objChassisMaster.ParkingDays - 20
                    If objChassisMaster.PenaltyParkingDays > 0 And objChassisMaster.PenaltyParkingDays <= 20 Then
                        amount = objChassisMaster.PenaltyParkingDays * 10000
                    ElseIf objChassisMaster.PenaltyParkingDays > 20 Then
                        amount = (20 * 10000) + (objChassisMaster.PenaltyParkingDays - 20) * 20000
                    Else
                        amount = 0
                    End If
                End If

            ElseIf (objChassisMaster2.Category.CategoryCode = "LCV") Or (objChassisMaster2.Category.CategoryCode = "CV") Then
                If isContinued Then
                    If objChassisMaster.PenaltyParkingDays > 0 And objChassisMaster.PenaltyParkingDays <= 20 Then
                        amount = objChassisMaster.PenaltyParkingDays * 25000
                    ElseIf objChassisMaster.PenaltyParkingDays > 20 Then
                        amount = objChassisMaster.PenaltyParkingDays * 40000
                    Else
                        amount = 0
                    End If
                Else
                    objChassisMaster.PenaltyParkingDays = objChassisMaster.ParkingDays - 5
                    If objChassisMaster.PenaltyParkingDays > 0 And objChassisMaster.PenaltyParkingDays <= 20 Then
                        amount = objChassisMaster.PenaltyParkingDays * 25000
                    ElseIf objChassisMaster.PenaltyParkingDays > 20 Then
                        amount = (20 * 25000) + (objChassisMaster.PenaltyParkingDays - 20) * 40000
                    Else
                        amount = 0
                    End If
                End If
            Else
                amount = 0
            End If
            Return amount
        End Function

        Private Function getDOStatusList(ByVal code As String) As ChassisMaster

            Dim userPrinciple As IPrincipal
            Dim objChassisMasterFacade As New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim objChassisMaster As ChassisMaster = objChassisMasterFacade.Retrieve(code)
            Return objChassisMaster

        End Function

        Private Function getNumberOfColumns(ByVal strText As String)
            Dim seperator As Char = ";"
            Dim textContainer() As String

            textContainer = strText.Trim.Split(seperator)

            Return textContainer.Length
        End Function

#End Region

    End Class

End Namespace
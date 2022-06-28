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

    Public Class DOStatusListParser
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
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DOStatusListParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DOStatusListParser, BlockName)
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
                        If Not IsExistCode(item.ChassisNumber) Then
                            InsertChassisMaster(item)
                        Else
                            UpdateChassisMaster(item)
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DOStatusListParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DOStatusListParser, BlockName)
                        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.ChassisNumber & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    End Try
                Next
                'Do Business - Like save to database
            End If
            Return 0
        End Function

        Private Sub InsertChassisMaster(ByVal objChassisMaster As ChassisMaster)

            Dim objChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            objChassisMasterFacade.Insert(objChassisMaster)

        End Sub

        Private Sub UpdateChassisMaster(ByVal objChassisMaster As ChassisMaster)
            Dim criterias As CriteriaComposite
            Dim objNewChassisMaster As ChassisMaster
            Dim ChasisMasterColl As ArrayList

            Dim objChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            criterias = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, objChassisMaster.ChassisNumber))

            ChasisMasterColl = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

            If ChasisMasterColl.Count > 0 Then
                objNewChassisMaster = ChasisMasterColl(0)
            End If

            objChassisMaster.ID = objNewChassisMaster.ID
            If objChassisMaster.RowStatus = DBRowStatus.Deleted Then
                objChassisMaster.RowStatus = DBRowStatus.Active
                objChassisMasterFacade.Update(objChassisMaster)
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
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "ChassisNumber", _
                                MatchType.Exact, objChassisMaster.ChassisNumber))
                            Dim objAl As ArrayList
                            Dim objChassisMaster2 As New ChassisMaster
                            Dim GIDate As Date

                            objAl = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

                            If Not objAl.Count = 0 Then

                                objChassisMaster2 = CType(objAL.Item(0), ChassisMaster)
                                GIDate = objChassisMaster2.GIDate

                                'objChassisMaster.GIDate = Left(sTemp, 2) + "/" + Mid(sTemp, 2, 2) + "/" + Right(sTemp.Trim, 2)
                                objChassisMaster.GIDate = sTemp.Substring(2, 2) & "/" & sTemp.Substring(0, 2) & "/" & sTemp.Substring(4, 4)

                                If Not (objChassisMaster2.DODate = System.DateTime.MinValue) _
                                    And objChassisMaster2.DODate < objChassisMaster.GIDate Then

                                    If IsNothing(objChassisMaster.GIDate) Then
                                        If IsNothing(GIDate) Then
                                            objChassisMaster.ParkingDays = Today.Date.Subtract(objChassisMaster2.DODate).TotalDays
                                        Else
                                            objChassisMaster.ParkingDays = GIDate.Subtract(objChassisMaster2.DODate).TotalDays
                                        End If
                                    Else
                                        objChassisMaster.ParkingDays = objChassisMaster.GIDate.Subtract(objChassisMaster2.DODate).TotalDays
                                    End If

                                    If objChassisMaster.ParkingDays <= 10 Then
                                        objChassisMaster.ParkingAmount = (10 * 0)
                                    ElseIf objChassisMaster.ParkingDays > 10 And objChassisMaster.ParkingDays <= 20 Then
                                        objChassisMaster.ParkingAmount = (10 * 0) + (objChassisMaster.ParkingDays - 10) * 10000
                                    ElseIf objChassisMaster.ParkingDays > 20 Then
                                        objChassisMaster.ParkingAmount = (10 * 0) + (10 * 10000) + (objChassisMaster.ParkingDays - 20) * 20000
                                    Else
                                        'error
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
                            ArryList = New CategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

                            If Not IsNothing(ArryList) Then
                                For Each ObjCategory As Category In ArryList
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
                                GIDate = Nothing
                            End If

                            If IsNothing(GIDate) Then
                                'objChassisMaster.ParkingDays = Today.Date.Subtract(objChassisMaster.DODate).TotalDays
                                objChassisMaster.ParkingDays = 0
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
                            ArryList2 = New TermOfPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If Not IsNothing(ArryList2) Then
                                For Each ObjCategory As Category In ArryList2
                                    objChassisMaster.Category = ObjCategory
                                Next
                            End If


                            'If ArryList2.Count > 0 Then
                            '    objChassisMaster.TermOfPayment = ArryList2(0)
                            'End If
                        Case Is = 10
                            objChassisMaster.DiscountAmount = sTemp.Trim
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
                    objChassisMaster.GIDate = "01/01/1900"
                    Me.DOStatusListAL.Add(objChassisMaster)
                End If

            End If

        End Sub

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
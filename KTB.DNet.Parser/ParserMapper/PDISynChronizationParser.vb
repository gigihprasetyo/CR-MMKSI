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
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser

    Public Class PDISynChronizationParser
        Inherits AbstractParser

#Region "Private Variables"
        Private status As String
        Private _Stream As StreamReader
        Private arrPDI As ArrayList
        Private _fileName As String
        Private Grammar As Regex
        Private errorSb As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser(",")
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            arrPDI = New ArrayList
            _fileName = fileName
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            While (Not val = "")
                Try
                    ParsePDI(val + ",")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "PDISynChronizationParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PDISynChronizationParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return arrPDI
        End Function

        Protected Overrides Function DoTransaction() As Integer
            If arrPDI.Count > 0 Then
                For Each objpdi As PDI In arrPDI
                    Try
                        savePDIData(objpdi)
                    Catch ex As Exception
                        'Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objpdi.ChassisMaster.ChassisNumber & " - " & objpdi.Dealer.DealerCode & Chr(13) & Chr(10) & ex.Message)
                        'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "PDISynChronizationParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PDISynChronizationParser, BlockName)
                    End Try
                Next
                Return 0
            Else
                Return -1
            End If

        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"


#Region "Update By Heru"
        Private Sub ParsePDI(ByVal ValParser As String)
            Dim _PDI As PDI = New PDI
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim isReleasDateExist As Boolean = False
            sStart = 0
            nCount = 0
            errorSb = New StringBuilder
            'Dim ArryList, ArryList2 As ArrayList

            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, sTemp.Trim))
                        'Dim objDealerList As ArrayList = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                        Dim objDealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                        If objDealer.ID > 0 Then
                            _PDI.Dealer = objDealer
                        Else
                            errorSb.Append("Invalid Dealer " & sTemp & Chr(13) & Chr(10))
                        End If
                    Case Is = 1
                        'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, sTemp.Trim))
                        'ArryList2 = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                        'For Each objChassis As ChassisMaster In ArryList2
                        '    _PDI.ChassisMaster = objChassis
                        'Next
                        Dim objChasisMaster As ChassisMaster = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                        If objChasisMaster.ID > 0 Then
                            _PDI.ChassisMaster = objChasisMaster
                        Else
                            errorSb.Append("Invalid Chasis Master " & sTemp & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        If sTemp.ToUpper = "A" Or sTemp.ToUpper = "B" Or sTemp.ToUpper = "C" Or sTemp.ToUpper = "D" Then
                            _PDI.Kind = sTemp.Trim
                        Else
                            errorSb.Append("Invalid PDI Kind " & sTemp & Chr(13) & Chr(10))
                        End If
                    Case Is = 3
                        Dim PDIdate As DateTime
                        Try
                            'PDIdate = sTemp.Substring(2, 2).ToString & "-" & sTemp.Substring(0, 2) & "-" & sTemp.Substring(4, 4)
                            PDIdate = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2))
                            _PDI.PDIDate = PDIdate
                        Catch ex As Exception
                            errorSb.Append("Invalid PDI Date " & sTemp & Chr(13) & Chr(10))
                        End Try
                    Case Is = 4
                        Dim PDIReleaseDate As DateTime
                        isReleasDateExist = True
                        Try
                            'PDIdate = sTemp.Substring(2, 2).ToString & "-" & sTemp.Substring(0, 2) & "-" & sTemp.Substring(4, 4)
                            If sTemp.Length > 0 Then
                                PDIReleaseDate = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2))
                            Else
                                PDIReleaseDate = System.DateTime.Now
                            End If
                            _PDI.ReleaseDate = PDIReleaseDate
                        Catch ex As Exception
                            errorSb.Append("Invalid PDI Date " & sTemp & Chr(13) & Chr(10))
                        End Try
                    Case Else
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            'Untuk handle jika fieldReleaseDate Belum ada
            If isReleasDateExist = False Then
                _PDI.ReleaseDate = System.DateTime.Now
            End If

            If errorSb.Length > 0 Then
                Throw New Exception(errorSb.ToString)
            Else
                _PDI.PDIStatus = CStr(EnumFSStatus.FSStatus.Selesai)
                arrPDI.Add(_PDI)
            End If

            'If ArryList.Count > 0 And ArryList2.Count > 0 Then
            '    _PDI.PDIStatus = CStr(EnumFSStatus.FSStatus.Selesai)
            '    arrPDI.Add(_PDI)
            'End If

        End Sub

        Private Sub InsertDataPDI(ByVal objPDI As PDI)
            Dim objPDIFacade As PDIFacade = New PDIFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            objPDIFacade.Insert(objPDI)
        End Sub

        Private Sub UpdateDataPDI(ByVal objPDI As PDI)
            Dim objPDIFacade As PDIFacade = New PDIFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            objPDIFacade.Update(objPDI)
        End Sub

        Private Sub savePDIData(ByVal objPDI As PDI)
            'If checkChassis(objPDI) = True Then
            '    If checkPDI(objPDI) = True Then
            '        If checkDealer(objPDI) = True Then
            '            UpdatePDI(objPDI)
            '        End If
            '    Else
            '        InsertPDi(objPDI)
            '    End If
            'End If
            Dim isChange As New IsChangeFacade
            Dim objPDIfromDB As PDI = GetPDIStatus(objPDI.ChassisMaster.ID)
            If Not objPDIfromDB Is Nothing Then
                If objPDI.Dealer.ID = objPDIfromDB.Dealer.ID Then
                    'Dealer and Chasis identic so update kind and date
                    If isChange.ISchangePDI(objPDI, objPDIfromDB) Then

                        objPDIfromDB.Kind = objPDI.Kind
                        objPDIfromDB.PDIDate = objPDI.PDIDate
                        objPDIfromDB.PDIStatus = objPDI.PDIStatus
                        objPDIfromDB.ReleaseDate = objPDI.ReleaseDate
                        UpdateDataPDI(objPDIfromDB)

                    End If

                Else
                    Throw New Exception("Invalid Data, ChasisNumber Identical but Dealer Different, Dealer Existing :  " & objPDIfromDB.Dealer.DealerCode & " ,New Dealer : " & objPDI.Dealer.DealerCode)
                End If
            Else
                'Data baru ---> insert
                InsertDataPDI(objPDI)
            End If
        End Sub

        Private Function GetPDIStatus(ByVal chasisNumberID As Integer) As PDI
            Dim objPDIFacade As PDIFacade = New PDIFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMaster.ID", MatchType.Exact, chasisNumberID))
            Dim objPDIList As ArrayList = objPDIFacade.Retrieve(criterias)
            If objPDIList.Count > 0 Then
                Return CType(objPDIList(0), PDI)
            Else
                Return Nothing
            End If
        End Function

#End Region

#Region "Created by Arris"
        Private Function GetPDI(ByVal code As String)
            Dim userPrinciple As IPrincipal
            Dim PDIfacade As New PDIfacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim _pdi As PDI = PDIfacade.Retrieve(code)
            Return _pdi
        End Function

        Private Sub UpdatePDI(ByVal arrPDI As PDI)
            Dim objPDI2 As PDI
            Dim objPDIRetAl As ArrayList
            Dim objPDIFacade As PDIFacade = New PDIFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim criterias As New CriteriaComposite(New Criteria(GetType(PDI), "ChassisMaster.ChassisNumber", MatchType.Exact, arrPDI.ChassisMaster.ChassisNumber.Trim()))
            criterias.opAnd(New Criteria(GetType(PDI), "Dealer.DealerCode", MatchType.Exact, arrPDI.Dealer.DealerCode.Trim()))
            objPDIRetAl = New PDIFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            If objPDIRetAl.Count > 0 Then
                objPDI2 = CType(objPDIRetAl.Item(0), PDI)
                objPDI2.ChassisMaster.ChassisNumber = arrPDI.ChassisMaster.ChassisNumber
                objPDI2.Dealer.DealerCode = arrPDI.Dealer.DealerCode
                objPDI2.Kind = arrPDI.Kind
                objPDI2.PDIDate = arrPDI.PDIDate
                objPDI2.PDIStatus = arrPDI.PDIStatus
                objPDI2.ReleaseDate = arrPDI.ReleaseDate
                objPDIFacade.Update(objPDI2)
            Else
                Throw New Exception("Data Not Found")
            End If
        End Sub

        Private Function IsExistCode(ByVal sCode As String) As Boolean
            Dim objPDI As PDIFacade = New PDIFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Return objPDI.ValidateCode(sCode) > 0
        End Function

        Private Function IsExistCode2(ByVal ChassisId As Integer) As Boolean
            Dim objPDIFacade As PDIFacade = New PDIFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            'Periksa agar tidak ada key ganda 
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMaster.ID", MatchType.Exact, ChassisId))
            Dim TExist As ArrayList = New EndCustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            If TExist.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Function checkChassis(ByVal objPDI As PDI) As Boolean
            Dim user As IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim objChassisMasterAl As ArrayList
            Dim objCriteria As CriteriaComposite

            objCriteria = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, objPDI.ChassisMaster.ChassisNumber.ToString.Trim()))
            objChassisMasterAl = New ChassisMasterFacade(user).Retrieve(objCriteria)


            If objChassisMasterAl.Count <> 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Private Function checkPDI(ByVal objPDI As PDI) As Boolean
            Dim user As IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim objPDIAl As ArrayList
            Dim objCriteria As CriteriaComposite

            objCriteria = New CriteriaComposite(New Criteria(GetType(PDI), "ChassisMaster.ChassisNumber", MatchType.Exact, objPDI.ChassisMaster.ChassisNumber.ToString.Trim()))

            objPDIAl = New PDIFacade(user).Retrieve(objCriteria)

            If objPDIAl.Count <> 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Private Function checkDealer(ByVal objPDI As PDI) As Boolean
            Dim user As IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim objPDIAl As ArrayList
            Dim objCriteria As CriteriaComposite

            objCriteria = New CriteriaComposite(New Criteria(GetType(PDI), "Dealer.DealerCode", MatchType.Exact, objPDI.Dealer.DealerCode.ToString.Trim()))

            objPDIAl = New PDIFacade(user).Retrieve(objCriteria)

            If objPDIAl.Count <> 0 Then
                Return True
            Else
                Return False
            End If

        End Function
#End Region



#End Region

    End Class

End Namespace
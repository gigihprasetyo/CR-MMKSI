#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Profile

#End Region

Namespace KTB.DNet.Parser

    Public Class DealerContactParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _arrBusinessArea As ArrayList
        Private _BusinessArea As BusinessArea
        Private _arrDealerProfile As ArrayList
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                _arrBusinessArea = New ArrayList()
                _arrDealerProfile = New ArrayList()
                _BusinessArea = Nothing
                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_BusinessArea) Then
                                _arrBusinessArea.Add(_BusinessArea)
                                _BusinessArea = Nothing
                            End If
                            errorMessage = New StringBuilder()
                            Dim cols As String() = line.Split(MyBase.ColSeparator)
                            If cols.Length > 3 Then
                                If cols(2) = String.Empty And cols(3).Trim.ToLower = "branchmanager" Then
                                    Dim arrTemp As ArrayList = ParseHeaderBranchManager(line)
                                    If arrTemp.Count > 0 Then
                                        _arrDealerProfile.AddRange(arrTemp)
                                    End If
                                Else
                                    _BusinessArea = ParseHeader(line)
                                End If

                            End If


                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "LeasingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _BusinessArea = Nothing
                    End Try
                Next

                If Not IsNothing(_BusinessArea) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _BusinessArea.ErrorMessage = errorMessage.ToString()
                    _arrBusinessArea.Add(_BusinessArea)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try


            Return _arrBusinessArea
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer

            Dim doFacade As BusinessAreaFacade
            Dim doFacade2 As DealerProfileFacade
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For Each objContactPerson As BusinessArea In _arrBusinessArea
                Try

                    If Not IsNothing(objContactPerson.ErrorMessage) AndAlso objContactPerson.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & objContactPerson.ErrorMessage.ToString() & ";"
                    Else
                        doFacade = New BusinessAreaFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        doFacade.InsertFromWS(objContactPerson)
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "LeasingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objContactPerson.ContactPerson & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            For Each objDealerProfil As DealerProfile In _arrDealerProfile
                Try
                    If Not IsNothing(objDealerProfil.ErrorMessage) AndAlso objDealerProfil.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & objDealerProfil.ErrorMessage.ToString() & ";"
                    Else
                        doFacade2 = New DealerProfileFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        doFacade2.InsertFromWS(objDealerProfil)
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "LeasingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objDealerProfil.ProfileHeader.Description & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrBusinessArea.Count.ToString(), "ws-worker", "LeasingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "LeasingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return Nothing
        End Function

#End Region

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub


        Private Function ParseHeaderBranchManager(ByVal line As String) As ArrayList
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objDealerprofilName As New DealerProfile
            Dim objDealerprofilHP As New DealerProfile
            Dim funcDealer As New DealerFacade(user)
            Dim funcProfile As New ProfileHeaderFacade(user)
            Dim funcProfileGroup As New ProfileGroupFacade(user)
            Dim result As New ArrayList

            errorMessage = New StringBuilder()
            If cols.Length < 4 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                Try '1 Code
                    Dim Code As String = cols(1).Trim

                    If Code = String.Empty Then
                        writeError("Code can't be empty")
                    End If
                    Dim objDealer As Dealer = funcDealer.Retrieve(Code)
                    If objDealer.ID = 0 Then
                        writeError("Dealer Code " + Code + " tidak terdaftar di D-Net.")
                    End If

                    objDealerprofilName.Dealer = objDealer
                    objDealerprofilHP.Dealer = objDealer
                Catch ex As Exception
                    writeError("Dealer Code error: " & ex.Message)
                End Try

                Try '4 Name
                    Dim Name As String = cols(4).Trim

                    If Name = String.Empty Then
                        writeError("Name can't be empty")
                    End If

                    objDealerprofilName.ProfileHeader = funcProfile.Retrieve("BRANCH_MGR")
                    objDealerprofilName.ProfileGroup = funcProfileGroup.Retrieve("sales_pic")
                    objDealerprofilName.ProfileValue = Name
                    objDealerprofilName.RowStatus = 0

                Catch ex As Exception
                    writeError("Code error: " & ex.Message)
                End Try
                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then

                    objDealerprofilName.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objDealerprofilName.LastUpdateBy = "WS"
                    result.Add(objDealerprofilName)
                    'objWitholdTax.Status = 1
                End If

                Try '5 HP
                    If cols.Length > 4 Then
                        Dim Hp As String = cols(5).Trim

                        If Not Hp = String.Empty Then
                            objDealerprofilHP.ProfileHeader = funcProfile.Retrieve("NO_HP")
                            objDealerprofilHP.ProfileGroup = funcProfileGroup.Retrieve("sales_pic")
                            objDealerprofilHP.ProfileValue = Hp
                            objDealerprofilHP.RowStatus = 0
                        End If

                    End If


                Catch ex As Exception
                    writeError("HP error: " & ex.Message)
                End Try
                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then

                    objDealerprofilHP.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objDealerprofilHP.LastUpdateBy = "WS"
                    result.Add(objDealerprofilHP)
                    'objWitholdTax.Status = 1
                End If

            End If
            Return result
        End Function

        Private Function ParseHeader(ByVal line As String) As BusinessArea
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objBusinessArea As New BusinessArea
            Dim func As New BusinessAreaFacade(user)
            Dim funcDealer As New DealerFacade(user)


            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else

                Dim strData As String = String.Empty


                Try '1 Code
                    Dim Code As String = cols(1).Trim

                    If Code = String.Empty Then
                        writeError("Code can't be empty")
                    End If

                    objBusinessArea.Dealer = funcDealer.Retrieve(Code)
                    If objBusinessArea.Dealer.ID = 0 Then
                        writeError("Dealer Code " + Code + " tidak terdaftar di D-Net.")
                    End If

                Catch ex As Exception
                    writeError("Dealer Code " + cols(1).Trim + " tidak terdaftar di D-Net.")
                End Try


                Try '2 Kind 
                    Dim kind As String = cols(2).Trim

                    If kind = String.Empty And cols(3).Trim.ToLower = "branchmanager" Then
                        writeError("Kind can't be empty")
                    End If

                    objBusinessArea.Kind = Me.GetValueIDbyDesc(kind, "AreaBusiness").ToString()

                Catch ex As Exception
                    writeError("Kind error: " & ex.Message)
                End Try


                Try '3 Position 
                    Dim position As String = cols(3).Trim

                    If position = String.Empty Then
                        writeError("Position can't be empty")
                    End If

                    objBusinessArea.Position = Me.GetValueID(position, "BusinessAreaPosition")

                Catch ex As Exception
                    writeError("Position error: " & ex.Message)
                End Try


                Try '4 Contact Person
                    Dim contactPerson As String = cols(4).Trim

                    If contactPerson = String.Empty Then
                        writeError("Contact Person can't be empty")
                    End If

                    objBusinessArea.ContactPerson = contactPerson
                    'objSalesOrg.DistributionChannel = distributionChannel
                Catch ex As Exception
                    writeError("Distribution Channel error: " & ex.Message)
                End Try

                'Try '5 Email
                '    Dim email As String = cols(5).Trim

                '    If Not email = String.Empty Then
                '        objBusinessArea.Email = email
                '    End If

                '    'objSalesOrg.Division = division
                'Catch ex As Exception
                '    writeError("Email error: " & ex.Message)
                'End Try


                'Try '6 Phone
                '    Dim phone As String = cols(6).Trim

                '    If Not phone = String.Empty Then
                '        objBusinessArea.Phone = phone
                '    End If

                '    'objSalesOrg.Division = division
                'Catch ex As Exception
                '    writeError("Phone error: " & ex.Message)
                'End Try

                'Try '6 Phone
                '    Dim phone As String = cols(6).Trim

                '    If Not phone = String.Empty Then
                '        objBusinessArea.Phone = phone
                '    End If

                '    'objSalesOrg.Division = division
                'Catch ex As Exception
                '    writeError("Phone error: " & ex.Message)
                'End Try

                'Try '7 Fax
                '    Dim fax As String = cols(7).Trim

                '    If Not fax = String.Empty Then
                '        objBusinessArea.Fax = fax
                '    End If

                '    'objSalesOrg.Division = division
                'Catch ex As Exception
                '    writeError("Fax error: " & ex.Message)
                'End Try


                Try '8 Hand Phone
                    If cols.Length > 5 Then
                        Dim hp As String = cols(5).Trim

                        If Not hp = String.Empty Then
                            objBusinessArea.Phone = hp
                        End If
                    End If

                    'objSalesOrg.Division = division
                Catch ex As Exception
                    writeError("Phone error: " & ex.Message)
                End Try

                'Try '9 Status
                '    Dim status As String = cols(9).Trim

                '    If Not status = String.Empty Then
                '        Select Case status.ToLower
                '            Case "active"
                '                objBusinessArea.RowStatus = 0
                '            Case "inactive"
                '                objBusinessArea.RowStatus = -1
                '        End Select
                '    End If

                'Catch ex As Exception
                '    writeError("Stautus error: " & ex.Message)
                'End Try

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then

                    objBusinessArea.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objBusinessArea.LastUpdateBy = "WS"
                    'objWitholdTax.Status = 1
                End If
            End If

            Return objBusinessArea
        End Function

        Private Function GetFacilityID(ByVal facilityCode) As Integer
            Dim result As Integer = 0
            Dim func As New StandardCodeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StandardCode), "ValueCode", MatchType.Exact, facilityCode))
            criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "DealerFacility"))

            Dim arrFacility As ArrayList = func.Retrieve(criterias)
            If arrFacility.Count > 0 Then
                result = CType(arrFacility(0), StandardCode).ValueId
            Else
                Throw New Exception("Fasilitas tidak terdaftar.")
            End If

            Return result
        End Function

        Private Function GetDealerID(ByVal dealerCode As String) As Integer
            Dim result As Integer = 0

            Try
                Dim objDealer As New Dealer

                objDealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(dealerCode)
                result = objDealer.ID
            Catch ex As Exception

            End Try
            Return IIf(result = 0, 0, result)

        End Function

        Private Function GetValueIDbyDesc(ByVal code As String, category As String) As Integer
            Dim result As Integer = 0
            Dim func As New StandardCodeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StandardCode), "ValueDesc", MatchType.Exact, code))
            criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, category))

            Dim arrFacility As ArrayList = func.Retrieve(criterias)
            If arrFacility.Count > 0 Then
                result = CType(arrFacility(0), StandardCode).ValueId
            Else
                Throw New Exception(category + " tidak terdaftar.")
            End If

            Return result
        End Function

        Private Function GetValueID(ByVal code As String, category As String) As Integer
            Dim result As Integer = 0
            Dim func As New StandardCodeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StandardCode), "ValueCode", MatchType.Exact, code))
            criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, category))

            Dim arrFacility As ArrayList = func.Retrieve(criterias)
            If arrFacility.Count > 0 Then
                result = CType(arrFacility(0), StandardCode).ValueId
            Else
                Throw New Exception(category + " tidak terdaftar.")
            End If

            Return result
        End Function
#End Region

    End Class
End Namespace

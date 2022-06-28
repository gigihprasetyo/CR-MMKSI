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
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
#End Region

Namespace KTB.DNet.Parser
    Public Class DealerBranchParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _arrDealerBranch As ArrayList
        Private _arrDealerBranchBusinessArea As ArrayList

        Private _hashDealerBranch As Hashtable
#End Region

#Region "Constructors/Destructors/Finalizers"
        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub
#End Region

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                _arrDealerBranch = New ArrayList()
                _arrDealerBranchBusinessArea = New ArrayList()

                _hashDealerBranch = New Hashtable()

                Dim objDealerBranch As DealerBranch = Nothing
                Dim objDealerBranchBusinessArea As DealerBranchBusinessArea = Nothing

                ''Dim objCity As City = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            ' Dealer Branch
                            objDealerBranch = Nothing
                            errorMessage = New StringBuilder()
                            objDealerBranch = ParseHeader(line)


                            If Not IsNothing(objDealerBranch) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objDealerBranch.ErrorMessage = errorMessage.ToString()
                                _arrDealerBranch.Add(objDealerBranch)

                            End If

                        ElseIf ind = MyBase.IndicatorDetail Then
                            ' Area I
                            If IsNothing(objDealerBranch) Then
                                writeError("Dealer Branch Tidak Valid")
                            Else
                                objDealerBranchBusinessArea = ParseDetail(line, objDealerBranch)
                            End If

                            If Not IsNothing(objDealerBranchBusinessArea) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objDealerBranchBusinessArea.ErrorMessage = errorMessage.ToString()
                                _arrDealerBranchBusinessArea.Add(objDealerBranchBusinessArea)

                            End If

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "DealerBranchParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.DealerBranchParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        '_arrDealerBranch = Nothing
                        '_arrDealerBranchBusinessArea = Nothing
                        Throw e
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            _hashDealerBranch.Add("DealerBranch", _arrDealerBranch)
            _hashDealerBranch.Add("DealerBranchBusinessArea", _arrDealerBranchBusinessArea)

            Return _hashDealerBranch
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim dealerBranchFacade As New DealerBranchFacade(user)

            ' loop Header (Main Area)
            For Each objDealerBranch As DealerBranch In _hashDealerBranch("DealerBranch")
                Try
                    ' Check if Province already in Database
                    ' Province found in Database
                    If objDealerBranch.ErrorMessage = "" Or objDealerBranch.ErrorMessage = String.Empty Then
                        If objDealerBranch.ID <> 0 Then
                            ' update Dealer Branch

                            'objDealerBranch.RowStatus = 0
                            'objDealerBranch.LastUpdateBy = user.Identity.Name
                            'objDealerBranch.LastUpdateTime = DateTime.Now

                            ' Loop Dealer Branch Business Area
                            Dim dealerBranchBusinessAreaList As ArrayList
                            Dim dealerBranchBusinessAreaListUpdate As ArrayList
                            dealerBranchBusinessAreaList = _hashDealerBranch("DealerBranchBusinessArea")
                            dealerBranchBusinessAreaListUpdate = New ArrayList

                            For Each dealerBranchBusinessArea As DealerBranchBusinessArea In dealerBranchBusinessAreaList
                                If dealerBranchBusinessArea.ErrorMessage = "" Or dealerBranchBusinessArea.ErrorMessage = String.Empty Then
                                    If dealerBranchBusinessArea.DealerBranch.ID = objDealerBranch.ID Then
                                        dealerBranchBusinessAreaListUpdate.Add(dealerBranchBusinessArea)

                                    End If
                                Else
                                    nError += 1
                                    sMsg &= dealerBranchBusinessArea.ErrorMessage & ";"
                                End If

                            Next

                            If dealerBranchFacade.UpdateWithTransactionManager(objDealerBranch, dealerBranchBusinessAreaListUpdate) < 0 Then
                                nError += 1
                            End If

                        Else
                            ' Dealer Branch not found in Database
                            objDealerBranch.CreatedBy = user.Identity.Name
                            objDealerBranch.CreatedTime = DateTime.Now

                            ' Dealer Branch Business Area
                            Dim dealerBranchBusinessAreaList As ArrayList
                            Dim dealerBranchBusinessAreaListInsert As ArrayList
                            dealerBranchBusinessAreaList = _hashDealerBranch("DealerBranchBusinessArea")
                            dealerBranchBusinessAreaListInsert = New ArrayList


                            For Each dealerBranchBusinessArea As DealerBranchBusinessArea In dealerBranchBusinessAreaList
                                If dealerBranchBusinessArea.ErrorMessage = "" Or dealerBranchBusinessArea.ErrorMessage = String.Empty Then
                                    If dealerBranchBusinessArea.DealerBranch.ID = objDealerBranch.ID Then
                                        dealerBranchBusinessAreaListInsert.Add(dealerBranchBusinessArea)
                                    End If
                                Else
                                    nError += 1
                                    sMsg &= dealerBranchBusinessArea.ErrorMessage & ";"
                                End If

                            Next


                            ' Insert Main Area, Area1, Area2
                            If dealerBranchFacade.InsertWithTransactionManager(objDealerBranch, dealerBranchBusinessAreaListInsert) < 0 Then
                                nError += 1
                            End If
                        End If
                    Else
                        nError += 1
                        sMsg &= objDealerBranch.ErrorMessage & ";"
                    End If

                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrDealerBranch.Count.ToString(), "ws-worker", "DealerBranchParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.DealerBranchParser, BlockName)

                Dim e As Exception = New Exception(sMsg)
                'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As DealerBranch
            ' K;MASTERAREA_timestamp\nH;StringH-1;StringH-2;StringH-3;\n
            ' D;StringD-n.1;StringD-n.2;StringD-n.3;\n
            ' DD;StringDD-m.n.1;StringDD-m.n.2;StringDD-m.n.3;StringDD-m.n.4;StringDD-m.n.5;\n

            ' K;MASTERAREA _20180810112801\nH;R001;Region 1;ADMIN;\n
            ' D;JBT;Jabodetabek;;\nDD;JBT1;JABODETABEK 1 AREA (JAKBAR & JAKSEL);unit;sprt;srv;/n
            ' DD;JBT2;JABODETABEK 1 AREA (JAKPUS & JAKUT);unit;sprt;srv;/nD;JBR;Jawa Barat;;\nDD;JBT1;JABODETABEK 1 AREA (JAKBAR & JAKSEL);unit;sprt;srv;/nDD;JBR1;WEST JAVA 1 AREA;unit;sprt;srv;/n

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim dealerBranch As DealerBranch


            Dim PDCode As String
            Dim UPTCode As Boolean = False

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else

                '13 Dealer Branch Code / H-13
                PDCode = cols(13).Trim
                If PDCode = String.Empty Then
                    writeError("Dealer Branch Code can't be empty")
                Else
                    Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBranch), "DealerBranchCode", MatchType.Exact, PDCode))
                    crt.opAnd(New Criteria(GetType(DealerBranch), "RowStatus", MatchType.Exact, 0))
                    Dim dealerBranchList As ArrayList = New DealerBranchFacade(user).Retrieve(crt)

                    If dealerBranchList.Count > 1 Then
                        writeError("There is more than 1 row for this Dealer Branch Code : " & PDCode)
                        dealerBranch = New DealerBranch()
                        dealerBranch.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                        Return dealerBranch
                    ElseIf dealerBranchList.Count = 1 Then
                        dealerBranch = dealerBranchList(0)
                    ElseIf dealerBranchList.Count = 0 Then
                        dealerBranch = New DealerBranch()
                        dealerBranch.DealerBranchCode = PDCode
                        dealerBranch.MarkLoaded()
                    End If
                End If

                If (Not IsNothing(dealerBranch)) Then
                    '1 DealerID / H-1
                    PDCode = cols(1).Trim
                    If PDCode = String.Empty Then
                        writeError("Dealer Code can't be empty")
                    Else
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, PDCode.Substring(PDCode.Length - 6)))
                        crt.opAnd(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, 0))
                        Dim dealerList As ArrayList = New DealerFacade(user).Retrieve(crt)
                        Dim dealer As Dealer

                        If dealerList.Count > 1 Then
                            writeError("There is more than 1 row for this dealer code : " & PDCode)
                        ElseIf dealerList.Count = 1 Then
                            dealer = dealerList(0)
                            dealerBranch.Dealer = dealer
                        ElseIf dealerList.Count = 0 Then
                            writeError("There is no Dealer with the Dealer Code : " & PDCode)
                        End If
                    End If

                    '2 Name / H-2
                    PDCode = cols(2).Trim
                    If (dealerBranch.Name <> PDCode) Then
                        UPTCode = True
                    End If
                    dealerBranch.Name = PDCode

                    '3 Status / H-3
                    ' Active = 1, INACTIVE = 0
                    PDCode = cols(3).Trim
                    If PDCode.ToUpper = "ACTIVE" Or PDCode.ToUpper = "INACTIVE" Then
                        'If PDCode.ToUpper = "ACTIVE" Then
                        '    dealerBranch.Status = 1
                        'Else
                        '    dealerBranch.Status = 0
                        'End If

                        Dim _Status As String = ""
                        If PDCode.ToUpper = "ACTIVE" Then
                            _Status = 1
                        Else
                            _Status = 0
                        End If

                        If _Status <> dealerBranch.Status Then
                            dealerBranch.Status = _Status
                            UPTCode = True
                        End If

                    Else
                        writeError("Invalid input for status")
                    End If

                    '4 Address / H-4
                    PDCode = cols(4).Trim
                    If (dealerBranch.Address <> PDCode) Then
                        UPTCode = True
                    End If
                    dealerBranch.Address = PDCode

                    '5 City Code / H-5
                    PDCode = cols(5).Trim
                    If PDCode = String.Empty Then
                        writeError("City Code can't be empty")
                    Else
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "CityCode", MatchType.Exact, PDCode))
                        crt.opAnd(New Criteria(GetType(City), "RowStatus", MatchType.Exact, 0))
                        Dim cityList As ArrayList = New CityFacade(user).Retrieve(crt)

                        If cityList.Count > 1 Then
                            writeError("There is more than 1 row for this City Code : " & PDCode)
                        ElseIf cityList.Count = 1 Then
                            dealerBranch.City = cityList(0)
                        ElseIf cityList.Count = 0 Then
                            writeError("There is no City with the City Code : " & PDCode)
                        End If
                    End If

                    '6 Zip Code / H-6
                    PDCode = cols(6).Trim
                    If PDCode <> String.Empty Then
                        If PDCode.Length <> 5 Or Not IsNumeric(PDCode) Then
                            writeError("Zip code must be 5 digit number : " & PDCode)
                        Else
                            If (dealerBranch.ZipCode <> PDCode) Then
                                UPTCode = True
                            End If
                            dealerBranch.ZipCode = PDCode
                        End If

                    End If

                    '7 Province Code / H-7
                    PDCode = cols(7).Trim
                    If PDCode = String.Empty Then
                        writeError("Province Code can't be empty")
                    Else
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "ProvinceCode", MatchType.Exact, PDCode))
                        crt.opAnd(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, 0))
                        Dim provinceList As ArrayList = New ProvinceFacade(user).Retrieve(crt)

                        If provinceList.Count > 1 Then
                            writeError("There is more than 1 row for this province code : " & PDCode)
                        ElseIf provinceList.Count = 1 AndAlso Not IsNothing(dealerBranch.City) Then
                            If dealerBranch.City.Province.ProvinceCode <> PDCode Then
                                writeError("Province and City data invalid : " & PDCode)
                            Else
                                dealerBranch.Province = provinceList(0)
                            End If
                        ElseIf provinceList.Count = 0 Then
                            writeError("There is no Province with the Province Code : " & PDCode)
                        Else
                            writeError("Error in processing Province/City data : " & PDCode)
                        End If
                    End If

                    '8 Phone / H-8
                    PDCode = cols(8).Trim
                    If (dealerBranch.Phone <> PDCode) Then
                        UPTCode = True
                    End If
                    dealerBranch.Phone = PDCode

                    '9 Fax / H-9
                    PDCode = cols(9).Trim
                    If (dealerBranch.Fax <> PDCode) Then
                        UPTCode = True
                    End If
                    dealerBranch.Fax = PDCode

                    '10 Website / H-10
                    PDCode = cols(10).Trim
                    If (dealerBranch.Website <> PDCode) Then
                        UPTCode = True
                    End If
                    dealerBranch.Website = PDCode

                    '11 Email / H-11
                    PDCode = cols(11).Trim
                    If (dealerBranch.Email <> PDCode) Then
                        UPTCode = True
                    End If
                    dealerBranch.Email = PDCode

                    '12 TypeBranch / H-12
                    ' TO = 0, OUTLET = 1
                    PDCode = cols(12).Trim
                    If PDCode.ToUpper = "TO" Or PDCode.ToUpper = "OUTLET" Then
                        'If PDCode.ToUpper = "TO" Then
                        '    dealerBranch.TypeBranch = 0
                        'Else
                        '    dealerBranch.TypeBranch = 1
                        'End If
                        Dim _TypeBranch As String = ""
                        If PDCode.ToUpper = "TO" Then
                            _TypeBranch = 0
                        Else
                            _TypeBranch = 1
                        End If

                        If _TypeBranch <> dealerBranch.TypeBranch Then
                            dealerBranch.TypeBranch = _TypeBranch
                            UPTCode = True
                        End If
                    Else
                        writeError("Invalid input for Type Branch")
                    End If


                    '14 Term 1 / H-14
                    PDCode = cols(14).Trim
                    If (dealerBranch.Term1 <> PDCode) Then
                        UPTCode = True
                    End If
                    dealerBranch.Term1 = PDCode

                    '15 Term 2 / H-15
                    PDCode = cols(15).Trim
                    If (dealerBranch.Term2 <> PDCode) Then
                        UPTCode = True
                    End If
                    dealerBranch.Term2 = PDCode

                    '16 Main Area / H-16
                    PDCode = cols(16).Trim
                    If PDCode = String.Empty Then
                        writeError("Main Area Code can't be empty")
                    Else
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MainArea), "AreaCode", MatchType.Exact, PDCode))
                        crt.opAnd(New Criteria(GetType(MainArea), "RowStatus", MatchType.Exact, 0))
                        Dim mainAreaList As ArrayList = New MainAreaFacade(user).Retrieve(crt)

                        If mainAreaList.Count > 1 Then
                            writeError("There is more than 1 row for this Main Area: " & PDCode)
                        ElseIf mainAreaList.Count = 1 Then
                            dealerBranch.MainArea = mainAreaList(0)
                        ElseIf mainAreaList.Count = 0 Then
                            writeError("There is no Main Area with the Area Code: " & PDCode)
                        End If
                    End If

                    '17 Area 1 / H-17
                    PDCode = cols(17).Trim
                    If PDCode = String.Empty Then
                        writeError("Area 1 Code can't be empty")
                    Else
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area1), "AreaCode", MatchType.Exact, PDCode))
                        crt.opAnd(New Criteria(GetType(Area1), "RowStatus", MatchType.Exact, 0))
                        Dim area1List As ArrayList = New Area1Facade(user).Retrieve(crt)
                        Dim area1 As Area1

                        If area1List.Count > 1 Then
                            writeError("There is more than 1 row for this Area: " & PDCode)
                        ElseIf area1List.Count = 1 AndAlso Not IsNothing(dealerBranch.MainArea) Then
                            area1 = area1List(0)
                            If area1.MainArea.AreaCode <> dealerBranch.MainArea.AreaCode Then
                                writeError("Area 1 : " & area1.MainArea.AreaCode & " and Main Area Data Invalid : " & dealerBranch.MainArea.AreaCode)
                            Else
                                dealerBranch.Area1 = area1List(0)
                            End If
                        ElseIf area1List.Count = 0 Then
                            writeError("There is no Area 1 with the Area Code : " & PDCode)
                        Else
                            writeError("Error in processing Area 1/Main Area data")
                        End If
                    End If

                    '18 Area 2 / H-18
                    PDCode = cols(18).Trim
                    If PDCode = String.Empty Then
                        writeError("Area 2 Code can't be empty")
                    Else
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area2), "AreaCode", MatchType.Exact, PDCode))
                        crt.opAnd(New Criteria(GetType(Area2), "RowStatus", MatchType.Exact, 0))
                        Dim area2List As ArrayList = New Area2Facade(user).Retrieve(crt)
                        Dim area2 As Area2

                        If area2List.Count > 1 Then
                            writeError("There is more than 1 row for this province code: " & PDCode)
                        ElseIf area2List.Count = 1 AndAlso Not IsNothing(dealerBranch.Area1) Then
                            area2 = area2List(0)
                            If area2.Area1.AreaCode <> dealerBranch.Area1.AreaCode Then
                                writeError("Area 2: " & area2.Area1.AreaCode & " and Area 1: " & dealerBranch.Area1.AreaCode & "Data Invalid")
                            Else
                                dealerBranch.Area2 = area2List(0)
                            End If
                        ElseIf area2List.Count = 0 Then
                            writeError("There is no Area 2 with the Area Code: " & PDCode)
                        Else
                            writeError("Error in processing Area 2/Area 1 data")
                        End If
                    End If

                    '19 Branch Assignment No / H-19
                    PDCode = cols(19).Trim
                    If (dealerBranch.BranchAssignmentNo <> PDCode) Then
                        UPTCode = True
                    End If
                    dealerBranch.BranchAssignmentNo = PDCode

                    '20 Branch Assignment Date / H-20
                    PDCode = cols(20).Trim
                    If Not IsNumeric(PDCode) Then
                        writeError("Branch Assignment Date Format invalid " & PDCode)
                    Else

                        Dim year As Integer = CInt(PDCode.Substring(0, 4))
                        Dim month As Integer = CInt(PDCode.Substring(4, 2))
                        Dim day As Integer = CInt(PDCode.Substring(6, 2))

                        Try
                            Dim branchAssignmentDate As Date = New Date(year, month, day)
                            dealerBranch.BranchAssignmentDate = branchAssignmentDate
                        Catch ex As Exception
                            writeError("Branch Assignment Date Format invalid")
                        End Try
                    End If

                    '21 Sales Unit Flag / H-21
                    'X = 1, "" = 0
                    PDCode = cols(21).Trim
                    If PDCode.ToUpper = "X" Or PDCode = String.Empty Or PDCode = "" Then
                        'If PDCode = "X" Then
                        '    dealerBranch.SalesUnitFlag = 0
                        'Else
                        '    dealerBranch.SalesUnitFlag = 1
                        'End If
                        Dim _SalesUnitFlag As String = ""
                        If PDCode.ToUpper = "X" Then
                            _SalesUnitFlag = 0
                        Else
                            _SalesUnitFlag = 1
                        End If

                        If _SalesUnitFlag <> dealerBranch.SalesUnitFlag Then
                            dealerBranch.SalesUnitFlag = _SalesUnitFlag
                            UPTCode = True
                        End If
                    Else
                        writeError("Invalid input for Sales Unit Flag")
                    End If

                    '22 Service Flag / H-22
                    'X = 1, "" = 0
                    PDCode = cols(22).Trim
                    If PDCode.ToUpper = "X" Or PDCode = String.Empty Or PDCode = "" Then
                        'If PDCode = "X" Then
                        '    dealerBranch.ServiceFlag = 1
                        'Else
                        '    dealerBranch.ServiceFlag = 0
                        'End If
                        Dim _ServiceFlag As String = ""
                        If PDCode.ToUpper = "X" Then
                            _ServiceFlag = 0
                        Else
                            _ServiceFlag = 1
                        End If

                        If _ServiceFlag <> dealerBranch.ServiceFlag Then
                            dealerBranch.ServiceFlag = _ServiceFlag
                            UPTCode = True
                        End If
                    Else
                        writeError("Invalid input for Service Flag")
                    End If

                    '23 Spare Part Flag / H-23
                    'X = 1, "" = 0
                    PDCode = cols(23).Trim
                    If PDCode.ToUpper = "X" Or PDCode = String.Empty Or PDCode = "" Then
                        'If PDCode = "X" Then
                        '    dealerBranch.SparepartFlag = 1
                        'Else
                        '    dealerBranch.SparepartFlag = 0
                        'End If
                        Dim _SparepartFlag As String = ""
                        If PDCode.ToUpper = "X" Then
                            _SparepartFlag = 0
                        Else
                            _SparepartFlag = 1
                        End If

                        If _SparepartFlag <> dealerBranch.SparepartFlag Then
                            dealerBranch.SparepartFlag = _SparepartFlag
                            UPTCode = True
                        End If
                    Else
                        writeError("Invalid input for Sparepart Flag")
                    End If
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    dealerBranch.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    dealerBranch.RowStatus = 0
                    If (UPTCode) Then
                        dealerBranch.LastUpdateBy = user.Identity.Name
                        dealerBranch.LastUpdateTime = DateTime.Now
                    Else
                        dealerBranch.LastUpdateBy = "Not Update"
                    End If

                End If
            End If

            Return dealerBranch
        End Function

        Private Function ParseDetail(ByVal line As String, ByVal dealerBranch As DealerBranch) As DealerBranchBusinessArea

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim dealerBranchBusinessArea As DealerBranchBusinessArea '= New DealerBranchBusinessArea
            Dim PDCode As String
            Dim UPTCode As Boolean = False

            errorMessage = New StringBuilder()

            If IsNothing(dealerBranch) Then
                writeError("Invalid Header Format")
            Else
                If cols.Length = 0 Then ' validasi colom Count
                    writeError("Invalid Detail Format")
                Else
                    '1 Kind / D-n.1
                    PDCode = cols(1).Trim
                    If PDCode = String.Empty Then
                        writeError("Kind can't be empty")
                    ElseIf PDCode <> 1 AndAlso PDCode <> 0 AndAlso PDCode <> 2 Then
                        writeError("Kind format invalid")
                    Else
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBranchBusinessArea), "Kind", MatchType.Exact, PDCode))
                        crt.opAnd(New Criteria(GetType(DealerBranchBusinessArea), "DealerBranch.DealerBranchCode", dealerBranch.DealerBranchCode))
                        crt.opAnd(New Criteria(GetType(DealerBranchBusinessArea), "RowStatus", MatchType.Exact, 0))

                        Dim dealerBranchBusinessAreaList As ArrayList = New DealerBranchBusinessAreaFacade(user).Retrieve(crt)

                        If dealerBranchBusinessAreaList.Count > 1 Then
                            writeError("Dealer Branch Business Area Data More Than 1 for the same code: " & PDCode)
                            dealerBranchBusinessArea = New DealerBranchBusinessArea()
                            dealerBranchBusinessArea.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                            Return dealerBranchBusinessArea
                        ElseIf dealerBranchBusinessAreaList.Count = 1 Then
                            dealerBranchBusinessArea = dealerBranchBusinessAreaList(0)
                            If (dealerBranchBusinessArea.DealerBranch.ID <> dealerBranch.ID) Then
                                UPTCode = True
                            End If
                            dealerBranchBusinessArea.DealerBranch = dealerBranch
                            dealerBranchBusinessArea.Dealer = Nothing
                        ElseIf dealerBranchBusinessAreaList.Count = 0 Then
                            dealerBranchBusinessArea = New DealerBranchBusinessArea
                            dealerBranchBusinessArea.DealerBranch = dealerBranch
                            dealerBranchBusinessArea.Kind = PDCode
                            dealerBranchBusinessArea.Dealer = Nothing
                        End If
                    End If

                    If (Not IsNothing(dealerBranchBusinessArea)) Then
                        '2 Title / D-n.2
                        PDCode = cols(2).Trim
                        If (dealerBranchBusinessArea.Title <> PDCode) Then
                            UPTCode = True
                        End If
                        dealerBranchBusinessArea.Title = PDCode

                        '3 Contact Person / D-n.3
                        PDCode = cols(3).Trim
                        If (dealerBranchBusinessArea.ContactPerson <> PDCode) Then
                            UPTCode = True
                        End If
                        dealerBranchBusinessArea.ContactPerson = PDCode

                        '4 Email / D-n.4
                        PDCode = cols(4).Trim
                        If (dealerBranchBusinessArea.Email <> PDCode) Then
                            UPTCode = True
                        End If
                        dealerBranchBusinessArea.Email = PDCode

                        '5 Phone / D-n.5
                        PDCode = cols(5).Trim
                        If (dealerBranchBusinessArea.Phone <> PDCode) Then
                            UPTCode = True
                        End If
                        dealerBranchBusinessArea.Phone = PDCode

                        '6 Fax / D-n.6
                        PDCode = cols(6).Trim
                        If (dealerBranchBusinessArea.Fax <> PDCode) Then
                            UPTCode = True
                        End If
                        dealerBranchBusinessArea.Fax = PDCode

                        '7 HP / D-n.7
                        PDCode = cols(7).Trim
                        If (dealerBranchBusinessArea.HP <> PDCode) Then
                            UPTCode = True
                        End If
                        dealerBranchBusinessArea.HP = PDCode

                        '8 DepHeadPIC / D-n.8
                        PDCode = cols(8).Trim
                        If (dealerBranchBusinessArea.DepHeadPIC <> PDCode) Then
                            UPTCode = True
                        End If
                        dealerBranchBusinessArea.DepHeadPIC = PDCode

                        '9 SectionHeadPIC / D-n.9
                        PDCode = cols(9).Trim
                        If (dealerBranchBusinessArea.SectionHeadPIC <> PDCode) Then
                            UPTCode = True
                        End If
                        dealerBranchBusinessArea.SectionHeadPIC = PDCode

                        '10 SalesACPIC / D-n.10
                        PDCode = cols(10).Trim
                        If (dealerBranchBusinessArea.SalesACPIC <> PDCode) Then
                            UPTCode = True
                        End If
                        dealerBranchBusinessArea.SalesACPIC = PDCode
                    End If

                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                        dealerBranchBusinessArea.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                    Else
                        dealerBranchBusinessArea.RowStatus = 0
                        If (UPTCode) Then
                            dealerBranchBusinessArea.LastUpdateBy = user.Identity.Name
                            dealerBranchBusinessArea.LastUpdateTime = DateTime.Now
                        Else
                            dealerBranchBusinessArea.LastUpdateBy = "Not Update"
                        End If
                    End If
                End If
            End If

            Return dealerBranchBusinessArea
        End Function

        Private Function isValidNumeric(ByVal stemp As String) As Boolean
            '-- Validate numeric field.
            '-- If stemp is a numeric and its value >= 0 then return True else return False
            Try
                Dim x As Long = CLng(stemp)
                If x >= 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region
    End Class
End Namespace


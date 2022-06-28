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
    Public Class AreaParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _arrMainArea As ArrayList
        Private _arrArea1 As ArrayList
        Private _arrArea2 As ArrayList
        Private _hashMasterArea As Hashtable
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

                _arrMainArea = New ArrayList()
                _arrArea1 = New ArrayList()
                _arrArea2 = New ArrayList()
                _hashMasterArea = New Hashtable()

                Dim objMainArea As MainArea = Nothing
                Dim objArea1 As Area1 = Nothing
                Dim objArea2 As Area2 = Nothing
                ''Dim objCity As City = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            ' Main Area
                            objMainArea = Nothing
                            errorMessage = New StringBuilder()
                            objMainArea = ParseHeader(line)


                            If Not IsNothing(objMainArea) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objMainArea.ErrorMessage = errorMessage.ToString()
                                _arrMainArea.Add(objMainArea)

                            End If

                        ElseIf ind = MyBase.IndicatorDetail Then
                            ' Area I
                            If IsNothing(objMainArea) Then
                                writeError("Province Tidak Valid")
                            Else
                                objArea1 = ParseDetail(line, objMainArea)
                            End If

                            If Not IsNothing(objArea1) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objArea1.ErrorMessage = errorMessage.ToString()
                                _arrArea1.Add(objArea1)

                            End If
                        ElseIf ind = MyBase.IndicatorDetailChild Then
                            ' Area II
                            If IsNothing(objArea1) Then
                                writeError("Province Tidak Valid")
                            Else
                                objArea2 = ParseDetailChild(line, objArea1)
                            End If

                            If Not IsNothing(objArea2) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objArea2.ErrorMessage = errorMessage.ToString()
                                _arrArea2.Add(objArea2)
                                objArea2 = Nothing
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "ProvinceCityParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.ProvinceCityParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _arrMainArea = Nothing
                        _arrArea1 = Nothing
                        _arrArea2 = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            _hashMasterArea.Add("MainArea", _arrMainArea)
            _hashMasterArea.Add("Area1", _arrArea1)
            _hashMasterArea.Add("Area2", _arrArea2)
            Return _hashMasterArea
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim areaFacade As New MainAreaFacade(user)

            ' loop Header (Main Area)
            For Each objMainArea As MainArea In _hashMasterArea("MainArea")
                Try
                    ' Check if Province already in Database
                    ' Province found in Database

                    If objMainArea.ErrorMessage = "" Or objMainArea.ErrorMessage = String.Empty Then
                        If objMainArea.ID <> 0 Then
                            ' update Main Area

                            'objMainArea.RowStatus = 0
                            'objMainArea.LastUpdateBy = user.Identity.Name
                            'objMainArea.LastUpdateTime = DateTime.Now

                            ' Loop Area 1 and Area 2
                            Dim area1List As ArrayList
                            Dim area1ListUpdate As ArrayList
                            area1List = _hashMasterArea("Area1")
                            area1ListUpdate = New ArrayList

                            Dim area2List As ArrayList
                            Dim area2ListUpdate As ArrayList
                            area2List = _hashMasterArea("Area2")
                            area2ListUpdate = New ArrayList

                            For Each area1 As Area1 In area1List
                                If area1.ErrorMessage = "" Or area1.ErrorMessage = String.Empty Then
                                    If area1.MainArea.ID = objMainArea.ID Then
                                        area1ListUpdate.Add(area1)

                                        ' Loop Area 2
                                        For Each area2 As Area2 In area2List
                                            If area2.ErrorMessage = "" Or area2.ErrorMessage = String.Empty Then
                                                If area2.Area1.ID = area1.ID Then

                                                    area2ListUpdate.Add(area2)
                                                End If
                                            Else
                                                nError += 1
                                                sMsg &= area2.ErrorMessage & ";"
                                            End If

                                        Next
                                    End If
                                Else
                                    nError += 1
                                    sMsg &= area1.ErrorMessage & ";"
                                End If

                            Next

                            If areaFacade.UpdateWithTransactionManager(objMainArea, area1ListUpdate, area2ListUpdate) < 0 Then
                                nError += 1
                            End If

                        Else
                            ' Main Area not found in Database
                            objMainArea.CreatedBy = user.Identity.Name
                            objMainArea.CreatedTime = DateTime.Now

                            ' Loop Area 1 and Area 2
                            Dim area1List As ArrayList
                            Dim area1ListInsert As ArrayList
                            area1List = _hashMasterArea("Area1")
                            area1ListInsert = New ArrayList

                            Dim area2List As ArrayList
                            Dim area2ListInsert As ArrayList
                            area2List = _hashMasterArea("Area2")
                            area2ListInsert = New ArrayList

                            For Each area1 As Area1 In area1List
                                If area1.ErrorMessage = "" Or area1.ErrorMessage = String.Empty Then
                                    If area1.MainArea.ID = objMainArea.ID Then
                                        area1ListInsert.Add(area1)

                                        ' Loop Area 2
                                        For Each area2 As Area2 In area2List
                                            If area2.ErrorMessage = "" Or area2.ErrorMessage = String.Empty Then
                                                If area2.Area1.ID = area1.ID Then
                                                    area2ListInsert.Add(area2)
                                                End If
                                            Else
                                                nError += 1
                                                sMsg &= area2.ErrorMessage & ";"
                                            End If

                                        Next
                                    End If
                                Else
                                    nError += 1
                                    sMsg &= area1.ErrorMessage & ";"
                                End If

                            Next

                            ' Insert Main Area, Area1, Area2
                            If areaFacade.InsertWithTransactionManager(objMainArea, area1ListInsert, area2ListInsert) < 0 Then
                                nError += 1
                            End If
                        End If
                    Else
                        nError += 1
                        sMsg &= objMainArea.ErrorMessage & ";"
                    End If

                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrMainArea.Count.ToString(), "ws-worker", "AreaParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.AreaParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "AreaParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.AreaParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As MainArea
            ' K;MASTERAREA_timestamp\nH;StringH-1;StringH-2;StringH-3;\n
            ' D;StringD-n.1;StringD-n.2;StringD-n.3;\n
            ' DD;StringDD-m.n.1;StringDD-m.n.2;StringDD-m.n.3;StringDD-m.n.4;StringDD-m.n.5;\n

            ' K;MASTERAREA _20180810112801\nH;R001;Region 1;ADMIN;\n
            ' D;JBT;Jabodetabek;;\nDD;JBT1;JABODETABEK 1 AREA (JAKBAR & JAKSEL);unit;sprt;srv;/n
            ' DD;JBT2;JABODETABEK 1 AREA (JAKPUS & JAKUT);unit;sprt;srv;/nD;JBR;Jawa Barat;;\nDD;JBT1;JABODETABEK 1 AREA (JAKBAR & JAKSEL);unit;sprt;srv;/nDD;JBR1;WEST JAVA 1 AREA;unit;sprt;srv;/n

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim mainArea As MainArea

            Dim PDCode As String
            Dim UPTCode As Boolean = False

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                '1 Area Code / H-1
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    Throw New Exception("Area Code can't be empty")
                Else
                    Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MainArea), "AreaCode", MatchType.Exact, PDCode))
                    crt.opAnd(New Criteria(GetType(MainArea), "RowStatus", MatchType.Exact, 0))
                    Dim mainAreaList As ArrayList = New MainAreaFacade(user).Retrieve(crt)

                    If mainAreaList.Count > 1 Then
                        writeError("There is more than 1 row for this area code:" & PDCode)
                        mainArea = New MainArea
                        mainArea.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                        Return mainArea
                    ElseIf mainAreaList.Count = 1 Then
                        mainArea = mainAreaList(0)
                    ElseIf mainAreaList.Count = 0 Then
                        mainArea = New MainArea
                        mainArea.MarkLoaded()
                        mainArea.AreaCode = PDCode
                    End If
                End If


                If (Not IsNothing(mainArea)) Then
                    '2 Description / H-2
                    PDCode = cols(2).Trim
                    If PDCode = String.Empty Then
                        writeError("Description Must be Filled.")
                    Else
                        If (mainArea.Description <> PDCode) Then
                            UPTCode = True
                        End If
                        mainArea.Description = PDCode

                    End If

                    '3 PIC Sales / H-3
                    PDCode = cols(3).Trim
                    'If PDCode = String.Empty Then
                    '    writeError("PIC Must be Filled.")
                    'Else
                    If (mainArea.PICSales <> PDCode) Then
                        UPTCode = True
                    End If
                    mainArea.PICSales = PDCode

                    'End If

                    '4 PIC Services / H-4
                    PDCode = cols(4).Trim
                    If (mainArea.PICServices <> PDCode) Then
                        UPTCode = True
                    End If
                    mainArea.PICServices = PDCode

                    '5 PIC Spareparts / H-5
                    PDCode = cols(5).Trim
                    If (mainArea.PICSpareparts <> PDCode) Then
                        UPTCode = True
                    End If
                    mainArea.PICSpareparts = PDCode
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    mainArea.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    mainArea.RowStatus = 0
                    If (UPTCode) Then
                        mainArea.LastUpdateBy = user.Identity.Name
                        mainArea.LastUpdateTime = DateTime.Now
                    Else
                        mainArea.LastUpdateBy = "Not Update"
                    End If

                End If
            End If

            Return mainArea
        End Function

        Private Function ParseDetail(ByVal line As String, ByVal mainArea As MainArea) As Area1

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim area1 As Area1

            Dim PDCode As String
            Dim UPTCode As Boolean = False

            errorMessage = New StringBuilder()

            If IsNothing(mainArea) Then
                writeError("Invalid Header Format")
            Else
                If cols.Length = 0 Then ' validasi colom Count
                    writeError("Invalid Detail Format")
                Else
                    '1 Area Code / D-n.1
                    PDCode = cols(1).Trim
                    If PDCode = String.Empty Then
                        Throw New Exception("Area Code can't be empty")
                    Else
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area1), "AreaCode", MatchType.Exact, PDCode))
                        crt.opAnd(New Criteria(GetType(Area1), "RowStatus", MatchType.Exact, 0))
                        Dim area1List As ArrayList = New Area1Facade(user).Retrieve(crt)

                        If area1List.Count > 1 Then
                            writeError("Area 1 Data More Than 1 for the same code:" & PDCode)
                            area1 = New Area1
                            area1.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                            Return area1
                        ElseIf area1List.Count = 1 Then
                            area1 = area1List(0)
                            If (area1.MainArea.ID <> mainArea.ID) Then
                                UPTCode = True
                            End If
                            area1.MainArea = mainArea
                        ElseIf area1List.Count = 0 Then
                            area1 = New Area1
                            area1.AreaCode = PDCode
                            area1.MainArea = mainArea
                            area1.MarkLoaded()
                        End If
                    End If

                    If (Not IsNothing(area1)) Then
                        '2 Description / D-n.2
                        PDCode = cols(2).Trim
                        If (area1.Description <> PDCode) Then
                            UPTCode = True
                        End If
                        area1.Description = PDCode

                        '3 PIC Sales / D-n.3
                        PDCode = cols(3).Trim
                        If (area1.PICSales <> PDCode) Then
                            UPTCode = True
                        End If
                        area1.PICSales = PDCode.Trim()

                        '4 PIC Services / D-n.4
                        PDCode = cols(4).Trim
                        If (area1.PICServices <> PDCode) Then
                            UPTCode = True
                        End If
                        area1.PICServices = PDCode.Trim()

                        '5 PIC Spareparts / D-n.5
                        PDCode = cols(5).Trim
                        If (area1.PICSpareparts <> PDCode) Then
                            UPTCode = True
                        End If
                        area1.PICSpareparts = PDCode.Trim()
                    End If
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    area1.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    area1.RowStatus = 0
                    If (UPTCode) Then
                        area1.LastUpdateBy = user.Identity.Name
                        area1.LastUpdateTime = DateTime.Now
                    Else
                        area1.LastUpdateBy = "Not Update"
                    End If
                End If
            End If

            Return area1
        End Function

        Private Function ParseDetailChild(ByVal line As String, ByVal area1 As Area1) As Area2

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim area2 As Area2

            Dim PDCode As String
            Dim UPTCode As Boolean = False

            errorMessage = New StringBuilder()

            If IsNothing(area1) Then
                writeError("Invalid Header Format")
            Else
                If cols.Length = 0 Then ' validasi colom Count
                    writeError("Invalid Detail Format")
                Else
                    '1 Area Code / D-n.1
                    PDCode = cols(1).Trim
                    If PDCode = String.Empty Then
                        Throw New Exception("Area Code can't be empty")
                    Else
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area2), "AreaCode", MatchType.Exact, PDCode))
                        crt.opAnd(New Criteria(GetType(Area2), "RowStatus", MatchType.Exact, 0))
                        Dim area2List As ArrayList = New Area2Facade(user).Retrieve(crt)

                        If area2List.Count > 1 Then
                            writeError("Area 2 Data More Than 1 for the same code:" & PDCode)
                            area2 = New Area2
                            area2.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                            Return area2
                        ElseIf area2List.Count = 1 Then
                            area2 = area2List(0)
                            If (area2.Area1.ID <> area1.ID) Then
                                UPTCode = True
                            End If
                            area2.Area1 = area1
                        ElseIf area2List.Count = 0 Then
                            area2 = New Area2
                            area2.Area1 = area1
                            area2.AreaCode = PDCode
                        End If
                    End If

                    If (Not IsNothing(area2)) Then
                        '2 Description / D-n.2
                        PDCode = cols(2).Trim
                        If (area2.Description <> PDCode) Then
                            UPTCode = True
                        End If
                        area2.Description = PDCode

                        '3 AC Finish Unit / D-n.3
                        PDCode = cols(3).Trim
                        If (area2.ACFinishUnit <> PDCode) Then
                            UPTCode = True
                        End If
                        area2.ACFinishUnit = PDCode

                        '4 AC Spare Part / D-n.4
                        PDCode = cols(4).Trim
                        If (area2.ACSparePart <> PDCode) Then
                            UPTCode = True
                        End If
                        area2.ACSparePart = PDCode

                        '5 AC Service / D-n.5
                        PDCode = cols(5).Trim
                        If (area2.ACService <> PDCode) Then
                            UPTCode = True
                        End If
                        area2.ACService = PDCode
                    End If
                End If
                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    area2.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    area2.RowStatus = 0
                    If (UPTCode) Then
                        area2.LastUpdateBy = user.Identity.Name
                        area2.LastUpdateTime = DateTime.Now
                    Else
                        area2.LastUpdateBy = "Not Update"
                    End If

                End If
            End If

            Return area2
        End Function
#End Region
    End Class
End Namespace


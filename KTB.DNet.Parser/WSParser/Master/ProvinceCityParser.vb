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
    Public Class ProvinceCityParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private objCity As City
        Private _arrCity As ArrayList
        Private _arrProvince As ArrayList
        Private _hashProvinceCity As Hashtable
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

                _arrProvince = New ArrayList()
                _arrCity = New ArrayList()
                _hashProvinceCity = New Hashtable()
                objCity = Nothing
                Dim objProvince As Province = Nothing
                ''Dim objCity As City = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            objProvince = Nothing
                            errorMessage = New StringBuilder()
                            objProvince = ParseHeader(line)
                            If Not IsNothing(objProvince) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objProvince.ErrorMessage = errorMessage.ToString()
                                _arrProvince.Add(objProvince)

                            End If
                        ElseIf ind = MyBase.IndicatorDetail Then
                            If IsNothing(objProvince) Then
                                writeError("Province Tidak Valid")
                            Else
                                objCity = ParseDetail(line, objProvince)
                            End If

                            If Not IsNothing(objCity) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objCity.ErrorMessage = errorMessage.ToString()
                                _arrCity.Add(objCity)
                                objCity = Nothing
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "ProvinceCityParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.ProvinceCityParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        Throw e
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            _hashProvinceCity.Add("Province", _arrProvince)
            _hashProvinceCity.Add("City", _arrCity)
            Return _hashProvinceCity
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim facProvince As New ProvinceFacade(user)
            Dim inactiveCityCount As Integer

            ' loop Header (Province)
            For Each objProvince As Province In _hashProvinceCity("Province")
                Try
                    ' Check if Province already in Database
                    ' Province found in Database
                    If IsNothing(objProvince.ErrorMessage) OrElse objProvince.ErrorMessage = "" Then
                        If objProvince.ID >= 0 Then
                            inactiveCityCount = 0
                            ' update Province data based on SAP

                            'objProvince.RowStatus = 0
                            'objProvince.LastUpdateBy = user.Identity.Name
                            'objProvince.LastUpdateTime = DateTime.Now

                            ' update City
                            Dim cityList As ArrayList
                            Dim cityListUpdate As ArrayList
                            cityList = _hashProvinceCity("City")
                            cityListUpdate = New ArrayList

                            ' map City and Province
                            For Each city As City In cityList
                                If IsNothing(city.ErrorMessage) OrElse city.ErrorMessage = String.Empty Then
                                    If city.Province.ProvinceCode = objProvince.ProvinceCode Then
                                        cityListUpdate.Add(city)

                                        If city.Status = "X" Then
                                            inactiveCityCount += 1
                                        End If

                                    End If
                                Else
                                    nError += 1
                                    sMsg &= city.ErrorMessage & ";"
                                End If

                            Next

                            If inactiveCityCount = cityListUpdate.Count Then
                                objProvince.RowStatus = -1

                                If objProvince.LastUpdateBy.ToLower = "not update" Then
                                    objProvince.LastUpdateBy = user.Identity.Name
                                End If

                            End If

                            If facProvince.UpdateWithTransactionManager(objProvince, cityListUpdate) < 0 Then
                                nError += 1
                            End If

                        Else
                            inactiveCityCount = 0
                            ' Province Not Found in Database
                            objProvince.CreatedBy = user.Identity.Name
                            objProvince.CreatedTime = DateTime.Now

                            Dim cityList As ArrayList
                            Dim cityListInsert As ArrayList
                            cityList = _hashProvinceCity("City")
                            cityListInsert = New ArrayList

                            ' cannot set vehicle type in DoParse, set here
                            For Each city As City In cityList
                                If IsNothing(city.ErrorMessage) OrElse city.ErrorMessage = String.Empty Then
                                    If city.Province.ProvinceCode = objProvince.ProvinceCode Then
                                        city.CreatedBy = user.Identity.Name
                                        city.CreatedTime = DateTime.Now
                                        cityListInsert.Add(city)

                                        If city.Status = "X" Then
                                            inactiveCityCount += 1
                                        End If

                                    End If
                                Else
                                    nError += 1
                                    sMsg &= city.ErrorMessage & ";"
                                End If

                            Next

                            If inactiveCityCount = cityListInsert.Count Then
                                objProvince.RowStatus = -1

                                If objProvince.LastUpdateBy.ToLower = "not update" Then
                                    objProvince.LastUpdateBy = user.Identity.Name
                                End If

                            End If

                            If facProvince.InsertWithTransactionManager(objProvince, cityListInsert) < 0 Then
                                nError += 1
                            End If
                        End If
                    Else
                        nError += 1
                        sMsg &= objProvince.ErrorMessage & ";"
                    End If

                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrProvince.Count.ToString(), "ws-worker", "ProvinceCityParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.ProvinceCityParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "ProvinceCityParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.ProvinceCityParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As Province
            ' K;MASTERCITY_timestamp\nH;StringH-1;StringH-2;\nD;StringD-1.1;StringD-1.2;StringD-1.3;\nD;StringD-2.1;StringD-2.2;StringD-2.3\n
            ' K;MASTERCITY_timestamp\nH;StringH-1;StringH-2;\nD;StringD-1.1;StringD-1.2;StringD-1.3;\nD;StringD-2.1;StringD-2.2;StringD-2.3\n
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objProvince As Province

            Dim PDCode As String
            Dim UPTCode As Boolean = False

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                '1 Province Code / H-1
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    writeError("Province Code can't be empty")
                Else
                    Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "ProvinceCode", MatchType.Exact, PDCode))
                    ' crt.opAnd(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, 0))
                    Dim objProvinceList As ArrayList = New ProvinceFacade(user).Retrieve(crt)

                    If objProvinceList.Count > 1 Then
                        writeError("Province Data More Than 1 for the same code :" & PDCode)
                        objProvince = New Province
                        objProvince.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                        Return objProvince
                    ElseIf objProvinceList.Count = 1 Then
                        objProvince = objProvinceList(0)
                    ElseIf objProvinceList.Count = 0 Then
                        objProvince = New Province
                        objProvince.ID = -1
                        objProvince.MarkLoaded()
                        objProvince.ProvinceCode = PDCode
                    End If
                End If

                If (Not IsNothing(objProvince)) Then
                    '2 Province Desc / H-2
                    PDCode = cols(2).Trim
                    If PDCode = String.Empty Then
                        writeError("Province Name Must be Filled.")
                    Else
                        If (objProvince.ProvinceName <> PDCode.Trim()) Then
                            UPTCode = True
                        End If
                        objProvince.ProvinceName = PDCode.Trim()

                    End If
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    objProvince.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                End If

                If objProvince.RowStatus = -1 Then
                    UPTCode = True
                End If

                objProvince.RowStatus = 0
                If (UPTCode) Then
                    objProvince.LastUpdateBy = user.Identity.Name
                    objProvince.LastUpdateTime = DateTime.Now
                Else
                    'If Data objProvince Same Not Update
                    objProvince.LastUpdateBy = "Not Update"
                End If

            End If

            Return objProvince
        End Function

        Private Function ParseDetail(ByVal line As String, ByVal objProv As Province) As City
            ' K;MASTERCITY_timestamp\nH;StringH-1;StringH-2;\nD;StringD-1.1;StringD-1.2;StringD-1.3;\nD;StringD-2.1;StringD-2.2;StringD-2.3\n
            ' K;MASTERCITY_timestamp\nH;StringH-1;StringH-2;\nD;StringD-1.1;StringD-1.2;StringD-1.3;\nD;StringD-2.1;StringD-2.2;StringD-2.3\n
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objCity As City = New City()

            Dim PDCode As String
            Dim UPTCode As Boolean = False

            errorMessage = New StringBuilder()
            Try
                If IsNothing(objProv) Then
                    writeError("Invalid Header Format")
                Else
                    If cols.Length = 0 Then ' validasi colom Count
                        writeError("Invalid Detail Format")
                    Else
                        '1 City Code / D-n.1
                        PDCode = cols(1).Trim
                        If PDCode = String.Empty Then
                            writeError("City Code can't be empty")
                        Else
                            Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "CityCode", MatchType.Exact, PDCode))
                            'crt.opAnd(New Criteria(GetType(City), "RowStatus", MatchType.Exact, 0))
                            Dim objCityList As ArrayList = New CityFacade(user).Retrieve(crt)

                            If objCityList.Count > 1 Then
                                writeError("City Data More Than 1 for the same code : " & PDCode)
                            ElseIf objCityList.Count = 1 Then
                                objCity = objCityList(0)
                                If (objCity.Province.ID <> objProv.ID) Then
                                    UPTCode = True
                                End If
                                objCity.Province = objProv
                            ElseIf objCityList.Count = 0 Then
                                objCity = New City
                                objCity.ID = -1
                                objCity.Province = objProv
                                objCity.CityCode = PDCode

                            End If
                        End If


                        If (Not IsNothing(objCity)) Then
                            '2 City Name / D-n.2
                            PDCode = cols(2).Trim
                            If PDCode = String.Empty Then
                                writeError("City Name Must be Filled.")
                            Else
                                If (objCity.CityName <> PDCode.Trim()) Then
                                    UPTCode = True
                                End If
                                objCity.CityName = PDCode.Trim()
                            End If
                        End If

                        '3 Status / D-n.3
                        PDCode = cols(3).Trim
                        If PDCode.ToUpper = "X" Or PDCode = "" Or PDCode = String.Empty Then
                            Dim _Status As String = ""
                            If PDCode.ToUpper = "X" Then
                                _Status = "X"
                            Else
                                _Status = "A"
                            End If

                            If _Status <> objCity.Status Then
                                objCity.Status = _Status
                                UPTCode = True
                            End If
                        Else
                            writeError("Invalid input for Status")
                        End If

                    End If

                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                        objCity.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                    Else

                        If objCity.RowStatus = -1 Then
                            UPTCode = True
                        End If

                        objCity.RowStatus = 0
                        If (UPTCode) Then
                            objCity.LastUpdateBy = user.Identity.Name
                            objCity.LastUpdateTime = DateTime.Now
                        Else
                            'If Data objCity Same Not Update
                            objCity.LastUpdateBy = "Not Update"
                        End If

                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return objCity
        End Function
#End Region
    End Class
End Namespace


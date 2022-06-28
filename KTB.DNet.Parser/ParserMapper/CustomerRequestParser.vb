#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Configuration
#End Region

#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Security.Principal
#End Region

Namespace KTB.DNet.Parser

    Public Class CustomerRequestParser
        Inherits AbstractParser

#Region "Private Variables"

        Private _fileName As String
        Private _stream As StreamReader
        Private ErrorMessage As StringBuilder
        Private grammar As Regex
        Private _Customer As Customer
        Private _Customers As ArrayList
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _stream = New StreamReader(fileName, True)
            _Customers = New ArrayList
            _fileName = fileName
            Dim val As String = MyBase.NextLine(_stream).Trim()
            While (Not val = "")
                Try
                    ParseCustomer(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "CustomerRequestParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.CustomerRequestParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_stream)
            End While
            _stream.Close()
            _stream = Nothing
            Return _Customers
        End Function


        Private Function GetClearName(ByVal objCustomer As Customer) As String
            Dim completeName As String = objCustomer.Name1 & objCustomer.Name2
            completeName = completeName.Replace(".", " ")
            completeName = completeName.Replace(",", " ")
            Dim filter As String = KTB.DNet.Lib.WebConfig.GetValue("FilterVerificationCustomer")
            Dim sb As StringBuilder = New StringBuilder
            Dim found As Boolean = False
            For Each item As String In completeName.Split(" ")
                found = False
                For Each itemFilter As String In filter.Split(";")
                    If itemFilter.ToUpper = item.ToUpper Then
                        found = True
                    End If
                Next
                If Not found Then
                    sb.Append(item)
                End If
            Next
            completeName = sb.ToString
            completeName = completeName.Replace(" ", "")
            completeName = completeName.Replace("""", "")
            completeName = completeName.Replace("'", "")
            Return completeName
        End Function

        Protected Overrides Function DoTransaction() As Integer
            For Each item As Customer In _Customers
                Try
                    Dim objCustomerFacade As CustomerFacade = New CustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    Dim completeName As String = GetClearName(item)
                    item.CompleteName = completeName
                    objCustomerFacade.Insert(item)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "CustomerRequestParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.CustomerRequestParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.Code & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseCustomer(ByVal streamLine As String)
            _Customer = New Customer
            _Customer.RowStatus = DBRowStatus.Active
            ErrorMessage = New StringBuilder
            Dim sStart As Integer = 0
            Dim nCount As Integer = 0
            Dim sColumn As String
            Dim sProfileType As String = ""

            For Each m As Match In grammar.Matches(streamLine)
                sColumn = streamLine.Substring(sStart, m.Index - sStart)
                sColumn = sColumn.Trim()
                Select Case nCount
                    Case Is = 0
                        If sColumn.Length > 0 Then
                            Dim _customerRequest As CustomerRequest = New CustomerRequestFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(CInt(sColumn))
                            If Not _Customer Is Nothing Then
                                If Not _customerRequest Is Nothing Then
                                    If _customerRequest.ID > 0 Then

                                        _Customer.MyCustomerRequest = _customerRequest

                                    Else
                                        _Customer.MyCustomerRequest = Nothing
                                    End If
                                Else
                                    _Customer.MyCustomerRequest = Nothing
                                End If
                            Else
                                _Customer.MyCustomerRequest = Nothing
                            End If
                        Else
                            _Customer.MyCustomerRequest = Nothing
                        End If
                    Case Is = 1
                        If sColumn.Length > 0 Then
                            _Customer.Code = sColumn
                        Else
                            ErrorMessage.Append("Customer Code tidak boleh kosong" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        'Start  :by:dna;for:rina;on:20110815;remark:DO NOT UPDATE NAME1 FOR CUSTOMERCODE='', IT HAS  " (DBL QUOTE STRING) PROBLEM
                        'Original Code:
                        '_Customer.Name1 = sColumn
                        'Updated:
                        If _Customer.Code.Trim.ToUpper = "30795026" Then
                            Dim oCust As Customer = New CustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sColumn)
                            If Not IsNothing(oCust) AndAlso oCust.ID > 0 Then
                                _Customer.Name1 = oCust.Name1
                            Else
                                _Customer.Name1 = "PT. PETERNAKAN AYAM ""MANGGIS"""
                            End If
                        Else
                            _Customer.Name1 = sColumn
                        End If
                        'End    :by:dna;for:rina;on:20110815;remark:DO NOT UPDATE NAME1 FOR CUSTOMERCODE='', IT HAS  " (DBL QUOTE STRING) PROBLEM

                    Case Is = 3
                        _Customer.Name2 = sColumn
                    Case Is = 4
                        _Customer.Name3 = sColumn
                    Case Is = 5
                        _Customer.Alamat = sColumn
                    Case Is = 6
                        _Customer.Kelurahan = sColumn
                    Case Is = 7
                        _Customer.Kecamatan = sColumn
                    Case Is = 8
                        _Customer.PostalCode = sColumn
                    Case Is = 9
                        _Customer.PreArea = sColumn.Trim.ToUpper
                    Case Is = 10
                        _Customer.PrintRegion = sColumn
                    Case Is = 11
                        'Propinsi sudah ada di kota
                    Case Is = 12
                        Dim _city As City = New CityFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sColumn)
                        If Not _city Is Nothing Then
                            If _city.ID > 0 Then
                                _Customer.City = _city
                            Else
                                ErrorMessage.Append("City tidak boleh kosong" & Chr(13) & Chr(10))
                            End If
                        Else
                            ErrorMessage.Append("City tidak boleh kosong" & Chr(13) & Chr(10))
                        End If
                    Case Is = 13
                        If sColumn.Trim.ToUpper = "X" Then
                            _Customer.RowStatus = DBRowStatus.Deleted
                        End If
                    Case Is = 15 'NO KTP
                        If sColumn.Trim = "" Then
                            ErrorMessage.Append("No KTP/TDP tidak boleh kosong" & Chr(13) & Chr(10))
                        Else
                            Dim sKTP As String = ""
                            Dim oCRP As CustomerRequestProfile
                            Dim i As Integer

                            For i = 0 To sColumn.Length - 1
                                If sColumn.Substring(i, 1) = Chr(9) Then
                                    Exit For
                                Else
                                    sKTP = sKTP & sColumn.Substring(i, 1)
                                End If
                            Next
                            sColumn = sKTP
                            If Not IsNothing(_Customer.MyCustomerRequest) AndAlso _Customer.MyCustomerRequest.ID > 0 Then
                                oCRP = _Customer.MyCustomerRequest.GetCustomerRequestProfile("NOKTP")
                                If Not IsNothing(oCRP) Then
                                    Dim oCRPFac As CustomerRequestProfileFacade = New CustomerRequestProfileFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                    oCRP.ProfileValue = sColumn
                                    '_Customer.MyCustomerRequest.UpdateCustomerRequestProfile(oCRP)
                                    oCRPFac.Update(oCRP)
                                End If
                            End If
                        End If
                        ''Start  : CR:by:dna;For:rina;On:20100701;remark:add new profile header (KTP,NPWP,SIUP,TDP)
                        'sProfileType = sColumn
                        ''If sColumn.Trim.ToUpper = "KTP" Then
                        ''ElseIf sColumn.Trim.ToUpper = "NPWP" Then
                        ''ElseIf sColumn.Trim.ToUpper = "SIUP" Then
                        ''ElseIf sColumn.Trim.ToUpper = "TDP" Then
                        ''End If
                        'Case Is = 15
                        'If sProfileType.Trim <> "" Then
                        '    Dim oCRP As CustomerRequestProfile
                        '    Dim IsValidProfileHeader As Boolean = False

                        '    If sColumn.Trim.ToUpper = "KTP" Then
                        '        IsValidProfileHeader = True
                        '    ElseIf sColumn.Trim.ToUpper = "NPWP" Then
                        '        IsValidProfileHeader = True
                        '    ElseIf sColumn.Trim.ToUpper = "SIUP" Then
                        '        IsValidProfileHeader = True
                        '    ElseIf sColumn.Trim.ToUpper = "TDP" Then
                        '        IsValidProfileHeader = True
                        '    End If
                        '    If IsValidProfileHeader AndAlso Not IsNothing(_Customer.MyCustomerRequest) Then
                        '        oCRP = _Customer.MyCustomerRequest.GetCustomerRequestProfile("NO" & sProfileType)
                        '        If Not IsNothing(oCRP) Then
                        '            oCRP.ProfileValue = sColumn
                        '        End If
                        '        _Customer.MyCustomerRequest.UpdateCustomerRequestProfile(oCRP)
                        '    End If
                        'End If
                        ''End    : CR:by:dna;For:rina;On:20100701;remark:add new profile header (KTP,NPWP,SIUP,TDP)
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            Else
                _Customers.Add(_Customer)
            End If
        End Sub
#End Region

    End Class

End Namespace


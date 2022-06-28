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

#End Region

Namespace KTB.DNet.Parser

    Public Class LeasingParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _aMPIRs As ArrayList
        Private _oMPIR As Leasing
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

                _aMPIRs = New ArrayList()
                _oMPIR = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_oMPIR) Then
                                _aMPIRs.Add(_oMPIR)
                                _oMPIR = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _oMPIR = ParseHeader(line)

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "LeasingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _oMPIR = Nothing
                    End Try
                Next

                If Not IsNothing(_oMPIR) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _oMPIR.ErrorMessage = errorMessage.ToString()
                    _aMPIRs.Add(_oMPIR)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try


            Return _aMPIRs
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer

            Dim doFacade As LeasingFacade
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For Each objLeasing As Leasing In _aMPIRs
                Try

                    If Not IsNothing(objLeasing.ErrorMessage) AndAlso objLeasing.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & objLeasing.ErrorMessage.ToString() & ";"
                    Else
                        doFacade = New LeasingFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        doFacade.InsertFromWebSevice(objLeasing)
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "LeasingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objLeasing.LeasingCode & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _aMPIRs.Count.ToString(), "ws-worker", "LeasingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As Leasing
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objLeasing As New Leasing
            Dim objLeasingFac As New LeasingFacade(user)


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

                    objLeasing.LeasingCode = Code
                Catch ex As Exception
                    writeError("Code error: " & ex.Message)
                End Try


                Try '2 Name
                    Dim Name As String = cols(2).Trim & " " & cols(3).Trim

                    If Name = String.Empty Then
                        writeError("Name can't be empty")
                    End If

                    objLeasing.LeasingName = Name

                Catch ex As Exception
                    writeError("Name error: " & ex.Message)
                End Try


                Try '4 Group Name
                    Dim LeasingGroupName As String = cols(4).Trim

                    'If LeasingGroupName = String.Empty Then
                    '    writeError("Group Name can't be empty")
                    'End If

                    objLeasing.LeasingGroupName = LeasingGroupName

                Catch ex As Exception
                    writeError("Group Name error: " & ex.Message)
                End Try


                Try '5 Alamat
                    Dim Alamat As String = cols(5).Trim

                    'If Alamat = String.Empty Then
                    '    writeError("Alamat can't be empty")
                    'End If

                    objLeasing.Alamat = Alamat

                Catch ex As Exception
                    writeError("Alamat error: " & ex.Message)
                End Try


                Try '6 PostalCode
                    Dim PostalCode As String = cols(6).Trim

                    'If PostalCode = String.Empty Then
                    '    writeError("Postal Code can't be empty")
                    'End If

                    objLeasing.PostalCode = PostalCode

                Catch ex As Exception
                    writeError("Postal Code error: " & ex.Message)
                End Try


                Try '8 Province
                    Dim Province As String = cols(8).Trim

                    'If Province = String.Empty Then
                    '    writeError("Province can't be empty")
                    'End If

                    Province = Me.GetProvinceName(Province)

                    objLeasing.Province = Province

                Catch ex As Exception
                    writeError("Province error: " & ex.Message)
                End Try

                Try '7 City
                    Dim City As String = cols(7).Trim

                    'If City = String.Empty Then
                    '    writeError("City can't be empty")
                    'End If

                    objLeasing.City = City

                Catch ex As Exception
                    writeError("City error: " & ex.Message)
                End Try

                Try '9 ContactPerson
                    Dim ContactPerson As String = cols(9).Trim

                    'If ContactPerson = String.Empty Then
                    '    writeError("Contact Person can't be empty")
                    'End If

                    objLeasing.ContactPerson = ContactPerson

                Catch ex As Exception
                    writeError("Contact Person error: " & ex.Message)
                End Try

                Try '10 PhoneNo
                    Dim PhoneNo As String = cols(10).Trim

                    'If PhoneNo = String.Empty Then
                    '    writeError("Phone No can't be empty")
                    'End If

                    objLeasing.PhoneNo = PhoneNo

                Catch ex As Exception
                    writeError("Phone No error: " & ex.Message)
                End Try

                Try '11 HP
                    Dim HP As String = cols(11).Trim

                    'If HP = String.Empty Then
                    '    writeError("HP can't be empty")
                    'End If

                    objLeasing.HP = HP

                Catch ex As Exception
                    writeError("HP error: " & ex.Message)
                End Try

                Try '12 Email
                    Dim Email As String = cols(12).Trim

                    'If Email = String.Empty Then
                    '    writeError("Email can't be empty")
                    'End If

                    objLeasing.Email = Email

                Catch ex As Exception
                    writeError("Email error: " & ex.Message)
                End Try


                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then

                    objLeasing.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objLeasing.LastUpdateBy = "WS"
                    objLeasing.Status = 1
                End If
            End If

            Return objLeasing
        End Function



        Private Function GetProvinceName(ByVal ProvinceCode As String) As String
            Dim result As String = String.Empty

            Try
                Dim pp As New Province

                pp = New ProvinceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(ProvinceCode)
                result = pp.ProvinceName
            Catch ex As Exception

            End Try
            Return IIf(result = String.Empty, ProvinceCode, result)

        End Function
#End Region

    End Class
End Namespace

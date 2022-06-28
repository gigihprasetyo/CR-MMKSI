#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNET.Domain
Imports KTB.DNET.BusinessFacade.General
Imports KTB.DNET.BusinessFacade
Imports KTB.DNET.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic

#End Region

Namespace KTB.DNet.Parser

    Public Class DealerFacilityParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _arrDealerFacility As ArrayList
        Private _DealerFacility As Dealerfacility
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

                _arrDealerFacility = New ArrayList()
                _DealerFacility = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_DealerFacility) Then
                                _arrDealerFacility.Add(_DealerFacility)
                                _DealerFacility = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _DealerFacility = ParseHeader(line)

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "LeasingParser.vb", "Parsing", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _DealerFacility = Nothing
                    End Try
                Next

                If Not IsNothing(_DealerFacility) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _DealerFacility.ErrorMessage = errorMessage.ToString()
                    _arrDealerFacility.Add(_DealerFacility)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try


            Return _arrDealerFacility
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer

            Dim doFacade As DealerfacilityFacade
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For Each objDealerFacility As Dealerfacility In _arrDealerFacility
                Try

                    If Not IsNothing(objDealerFacility.ErrorMessage) AndAlso objDealerFacility.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & objDealerFacility.ErrorMessage.ToString() & ";"
                    Else
                        doFacade = New DealerfacilityFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        doFacade.Insert(objDealerFacility)
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "LeasingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objDealerFacility.Facility & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrDealerFacility.Count.ToString(), "ws-worker", "LeasingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As Dealerfacility
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objDealerFacility As New Dealerfacility
            Dim func As New DealerfacilityFacade(user)


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

                    objDealerFacility.DealerID = Me.GetDealerID(Code)
                Catch ex As Exception
                    writeError("Code error: " & ex.Message)
                End Try


                Try '2 Name
                    Dim Name As String = cols(2).Trim

                    If Name = String.Empty Then
                        writeError("Name can't be empty")
                    End If

                    objDealerFacility.Facility = Me.GetFacilityID(Name)
                Catch ex As Exception
                    writeError("Name error: " & ex.Message)
                End Try


                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then

                    objDealerFacility.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objDealerFacility.LastUpdateBy = "WS"
                    'objWitholdTax.Status = 1
                End If
            End If

            Return objDealerFacility
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
#End Region

    End Class
End Namespace

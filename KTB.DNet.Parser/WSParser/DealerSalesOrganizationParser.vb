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

    Public Class DealerSalesOrganizationParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _arrSalesOrg As ArrayList
        Private _SalesOrg As DealerSalesOrganization
        Private _PaymentMethod As DealerPaymentMethod
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

                _arrSalesOrg = New ArrayList()
                _SalesOrg = Nothing
                _PaymentMethod = Nothing
                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_SalesOrg) Then
                                _arrSalesOrg.Add(_SalesOrg)
                                _SalesOrg = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _SalesOrg = ParseHeader(line)
                            'ElseIf ind = MyBase.IndicatorDetail Then
                            '    If IsNothing(_SalesOrg) OrElse Not IsNothing(_SalesOrg.ErrorMessage) Then
                            '    Else
                            '        _PaymentMethod = ParseDetail(line)
                            '        If Not IsNothing(_PaymentMethod) Then
                            '            If Not IsNothing(_PaymentMethod.ErrorMessage) AndAlso _PaymentMethod.ErrorMessage.Trim <> String.Empty Then
                            '                _SalesOrg.ErrorMessage = _SalesOrg.ErrorMessage & ";" & _PaymentMethod.ErrorMessage
                            '            Else
                            '                _SalesOrg.AddPaymentMethod(_PaymentMethod)
                            '            End If
                            '        End If
                            '    End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "LeasingParser.vb", "Parsing", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _SalesOrg = Nothing
                    End Try
                Next

                If Not IsNothing(_SalesOrg) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _SalesOrg.ErrorMessage = errorMessage.ToString()
                Else

                    _arrSalesOrg.Add(_SalesOrg)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try


            Return _arrSalesOrg
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer

            Dim doFacade As DealerSalesOrganizationFacade
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For Each objSalesOrg As DealerSalesOrganization In _arrSalesOrg
                Try

                    If Not IsNothing(objSalesOrg.ErrorMessage) AndAlso objSalesOrg.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & objSalesOrg.ErrorMessage.ToString() & ";"
                    Else
                        doFacade = New DealerSalesOrganizationFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        doFacade.InsertFromWS(objSalesOrg)
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "LeasingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objSalesOrg.SalesOrganizationCode & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrSalesOrg.Count.ToString(), "ws-worker", "LeasingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
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

        'Private Function ParseDetail(ByVal line As String) As DealerSalesOrganizationPaymentMethod
        '    Dim cols As String() = line.Split(MyBase.ColSeparator)
        '    Dim objPaymetMethod As New DealerSalesOrganizationPaymentMethod
        '    Try
        '        errorMessage = New StringBuilder()
        '        If cols.Length = 0 Then ' validasi colom Count
        '            writeError("Invalid Header Format")
        '        Else

        '            Dim strData As String = String.Empty
        '            Try '1 Payment Method
        '                Dim Code As String = cols(1).Trim

        '                If Code = String.Empty Then
        '                    writeError("Code can't be empty")
        '                End If

        '                objPaymetMethod.PaymentMethod = Code
        '            Catch ex As Exception
        '                writeError("Code error: " & ex.Message)
        '            End Try
        '        End If
        '        Return objPaymetMethod
        '    Catch ex As Exception
        '    End Try
        '    Return Nothing
        'End Function

        Private Function ParseHeader(ByVal line As String) As DealerSalesOrganization
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objSalesOrg As New DealerSalesOrganization
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

                    objSalesOrg.DealerID = Me.GetDealerID(Code)
                    If objSalesOrg.DealerID = 0 Then
                        writeError("Dealer Code " + Code + " tidak terdaftar di D-Net.")
                    End If

                Catch ex As Exception
                    writeError("Code error: " & ex.Message)
                End Try


                Try '2 Sales OrganizationCode 
                    Dim SalesOrganizationCode As String = cols(2).Trim

                    If SalesOrganizationCode = String.Empty Then
                        writeError("Sales Organization can't be empty")
                    End If

                    objSalesOrg.SalesOrganizationCode = SalesOrganizationCode
                Catch ex As Exception
                    writeError("Sales Organization error: " & ex.Message)
                End Try

                Try '3 Distribution Channel
                    Dim distributionChannel As String = cols(3).Trim

                    If distributionChannel = String.Empty Then
                        writeError("Distribution Channel can't be empty")
                    End If

                    objSalesOrg.DistributionChannel = distributionChannel
                Catch ex As Exception
                    writeError("Distribution Channel error: " & ex.Message)
                End Try

                Try '4 SalesDistrict
                    Dim SalesDistrict As String = cols(4).Trim

                    If SalesDistrict = String.Empty Then
                        writeError("SalesDistric can't be empty")
                    End If

                    objSalesOrg.SalesDistrict = SalesDistrict
                Catch ex As Exception
                    writeError("Sales Distric error: " & ex.Message)
                End Try

                Try 'Customer Group
                    If cols.Length > 5 Then
                        Dim CustGroup As String = cols(5).Trim

                        If Not CustGroup = String.Empty Then
                            objSalesOrg.CustomerGroup = CustGroup
                        End If

                    End If

                Catch ex As Exception
                    writeError("Customer Group error: " & ex.Message)
                End Try


                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then

                    objSalesOrg.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objSalesOrg.LastUpdateBy = "WS"
                    'objWitholdTax.Status = 1
                End If
            End If

            Return objSalesOrg
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
                Throw New Exception(ex.Message)
            End Try
            Return IIf(result = 0, 0, result)

        End Function
#End Region

    End Class
End Namespace

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
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade.FinishUnit

#End Region

Namespace KTB.DNet.Parser

    Public Class LogCostParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _aVTHs As ArrayList
        Private _oVTH As LogisticPrice
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

                _aVTHs = New ArrayList()
                _oVTH = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_oVTH) Then
                                _aVTHs.Add(_oVTH)
                                _oVTH = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _oVTH = ParseHeader(line)

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "LogCostParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LogCostParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _oVTH = Nothing
                    End Try
                Next

                If Not IsNothing(_oVTH) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _oVTH.ErrorMessage = errorMessage.ToString()
                    _aVTHs.Add(_oVTH)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try


            Return _aVTHs
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)


            Dim oVTFac As New LogisticPriceFacade(user)

            For Each oVT As LogisticPrice In _aVTHs
                Try
                    If Not IsNothing(oVT) Then
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(LogisticPrice), "RegionCode", MatchType.Exact, oVT.RegionCode))
                        criterias.opAnd(New Criteria(GetType(LogisticPrice), "SAPModel", MatchType.Exact, oVT.SAPModel))
                        criterias.opAnd(New Criteria(GetType(LogisticPrice), "EffectiveDate", MatchType.GreaterOrEqual, oVT.EffectiveDate))
                        Dim sortColl As SortCollection = New SortCollection
                        sortColl.Add(New Sort(GetType(LogisticPrice), "ID", Sort.SortDirection.DESC))
                        Dim LogisticPriceList As ArrayList = New LogisticPriceFacade(user).RetrieveByCriteria(criterias, sortColl)
                        If LogisticPriceList.Count > 0 Then
                            Dim old As LogisticPrice = LogisticPriceList(0)
                            old.PPn = oVT.PPn
                            old.RegionDescription = oVT.RegionDescription
                            old.LogisticPrice = oVT.LogisticPrice
                            If oVTFac.Update(old) < 0 Then
                                nError += 1
                            End If
                        Else
                            If oVTFac.Insert(oVT) < 0 Then
                                nError += 1
                            End If
                        End If
                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try
            Next


            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _aVTHs.Count.ToString(), "ws-worker", "LogCostParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LogCostParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "LogCostParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LogCostParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As LogisticPrice
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim oVT As New LogisticPrice
            Dim oVTFac As New VechileTypeFacade(user)

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                Dim strData As String = String.Empty

                Try '1 Region Code
                    Dim PDCode As String = cols(1).Trim
                    strData = PDCode
                    If PDCode = String.Empty Then
                        Throw New Exception("Empty Region Code " & strData)
                    Else
                        oVT = New LogisticPrice

                        oVT.RegionCode = PDCode
                    End If
                Catch ex As Exception
                    writeError("Vehicle Region  error: " & ex.Message)
                End Try

                '2 Region Desc
                If cols(2).Trim = String.Empty Then
                    writeError("Region Desc can't be empty")
                Else
                    Try ' Code
                        Dim PDCode As String = cols(2).Trim
                        oVT.RegionDescription = PDCode
                    Catch ex As Exception
                        writeError("Region Desc error: " & ex.Message)
                    End Try

                End If


                '3 SAP Model
                If cols(3).Trim = String.Empty Then
                    writeError("SAP Model can't be empty")
                Else
                    Try ' Code
                        Dim PDCode As String = cols(3).Trim
                        oVT.SAPModel = PDCode
                    Catch ex As Exception
                        writeError("SAP Model error: " & ex.Message)
                    End Try

                End If


                '4 Price
                Try
                    oVT.LogisticPrice = MyBase.GetCurrency(cols(4))
                Catch ex As Exception
                    errorMessage.Append("Invalid Logistic Price")
                End Try

                '5 PPN
                Try
                    oVT.PPn = MyBase.GetCurrency(cols(5))
                Catch ex As Exception
                    errorMessage.Append("Invalid PPN")
                End Try
                '6 ValidFrom
                oVT.EffectiveDate = MyBase.GetDateShort(cols(6))
                If oVT.EffectiveDate.Year = 1900 Then
                    errorMessage.Append("Invalid Effective Date")
                End If


                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    oVT.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    oVT.LastUpdateBy = "WS"
                End If
            End If

            Return oVT
        End Function

#End Region

    End Class
End Namespace

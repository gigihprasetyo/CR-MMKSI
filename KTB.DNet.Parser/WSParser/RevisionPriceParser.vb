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
Imports KTB.DNET.BusinessFacade.FinishUnit

#End Region


Namespace KTB.DNet.Parser
    Public Class RevisionPriceParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _aPriceList As ArrayList
        Private _oPrice As RevisionPrice
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

                _aPriceList = New ArrayList()
                _oPrice = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_oPrice) Then
                                _aPriceList.Add(_oPrice)
                                _oPrice = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            Dim listPrice As ArrayList = ParseHeader(line)

                            If listPrice.Count > 0 Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _oPrice.ErrorMessage = errorMessage.ToString()
                                _aPriceList.AddRange(listPrice)
                            End If

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "RevisionPriceParser.vb", "Parsing", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.RevisionPriceParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _oPrice = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try


            Return _aPriceList
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim doFacade As RevisionPriceFacade
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For Each objPrice As RevisionPrice In _aPriceList
                Try

                    If Not IsNothing(objPrice.ErrorMessage) AndAlso objPrice.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & objPrice.ErrorMessage.ToString() & ";"
                    Else
                        doFacade = New RevisionPriceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        doFacade.InsertFromWebSevice(objPrice)
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "RevisionPriceParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.RevisionPriceParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objPrice.RevisionType.Description & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _aPriceList.Count.ToString(), "ws-worker", "RevisionPriceParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.RevisionPriceParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "RevisionPriceParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.RevisionPriceParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function
#End Region

#Region "Private Method"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As ArrayList
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objPrice As New RevisionPrice
            Dim objRevisionPriceFac As New RevisionPriceFacade(user)
            Dim objRevisionTypeFac As New RevisionTypeFacade(user)
            Dim objCategoryFac As New CategoryFacade(user)
            Dim aPriceList As ArrayList = New ArrayList()
            Dim isNoCat As Boolean = False

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else

                Dim strData As String = String.Empty

                Try '1 Code
                    Dim Type As String = cols(5).Trim.Split("-")(0)

                    If Type = String.Empty Then
                        writeError("Revision Type can't be empty")
                    Else
                        Dim objType As RevisionType = New RevisionType
                        objType = objRevisionTypeFac.RetrieveByCode(Type)

                        If Not IsNothing(objType) Then
                            objPrice.RevisionType = objType
                        Else
                            writeError("Revision Type can't be found")
                        End If
                    End If

                Catch ex As Exception
                    writeError("Code error: " & ex.Message)
                End Try

                Try '2 Amount
                    Dim Amount As String = cols(2).Trim

                    If Amount = String.Empty Then
                        writeError("Amount can't be empty")
                    Else
                        objPrice.Amount = CType(Amount, Decimal)
                    End If

                Catch ex As Exception
                    writeError("Name error: " & ex.Message)
                End Try

                Try '3 Valid From
                    Dim validFrom As String = cols(3).Trim

                    If validFrom = String.Empty Then
                        writeError("Valid from can't be empty")
                    Else
                        objPrice.ValidFrom = Date.ParseExact(validFrom, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                    End If

                Catch ex As Exception
                    writeError("Valid From error: " & ex.Message)
                End Try

                Try '5 Category
                    If cols(5).Trim.Split("-").Count = 1 Then
                        isNoCat = True
                    End If

                    objPrice.LastUpdateBy = "WS"
                    objPrice.RowStatus = 0

                    If isNoCat Then
                        Dim objCat As Category = New Category
                        objCat = objCategoryFac.Retrieve("PC")

                        If Not IsNothing(objCat) Then
                            Dim objRevPrice As RevisionPrice = New RevisionPrice
                            objRevPrice.RevisionType = objPrice.RevisionType
                            objRevPrice.Amount = objPrice.Amount
                            objRevPrice.ValidFrom = objPrice.ValidFrom
                            objRevPrice.LastUpdateBy = objPrice.LastUpdateBy
                            objRevPrice.RowStatus = objPrice.RowStatus
                            objRevPrice.Category = objCat

                            If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                                objPrice.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                            Else
                                aPriceList.Add(objRevPrice)
                            End If
                        Else
                            writeError("Category can't be found")
                        End If

                        Dim objCat2 As Category = New Category
                        objCat2 = objCategoryFac.Retrieve("LCV")

                        If Not IsNothing(objCat2) Then
                            Dim objRevPrice2 As RevisionPrice = New RevisionPrice
                            objRevPrice2.RevisionType = objPrice.RevisionType
                            objRevPrice2.Amount = objPrice.Amount
                            objRevPrice2.ValidFrom = objPrice.ValidFrom
                            objRevPrice2.LastUpdateBy = objPrice.LastUpdateBy
                            objRevPrice2.RowStatus = objPrice.RowStatus
                            objRevPrice2.Category = objCat2

                            If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                                objPrice.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                            Else
                                aPriceList.Add(objRevPrice2)
                            End If
                        Else
                            writeError("Category can't be found")
                        End If
                    Else
                        Dim Cat As String = cols(5).Trim.Split("-")(1)

                        If Cat = String.Empty Then
                            writeError("Category can't be empty")
                        Else
                            Dim objCat As Category = New Category
                            objCat = objCategoryFac.Retrieve(Cat)

                            If Not IsNothing(objCat) Then
                                objPrice.Category = objCat

                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                                    objPrice.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                                Else
                                    aPriceList.Add(objPrice)
                                End If
                            Else
                                writeError("Category can't be found")
                            End If
                        End If
                    End If
                Catch ex As Exception
                    writeError("Category error: " & ex.Message)
                End Try
            End If

            Return aPriceList
        End Function

#End Region

    End Class
End Namespace

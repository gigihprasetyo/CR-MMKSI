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
    Public Class MSPTypeParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private objMSPType As MSPType
        Private _arrMSPType As ArrayList
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

                _arrMSPType = New ArrayList()
                objMSPType = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            ' create objek mspmaster
                            objMSPType = ParseHeader(line)
                            ' insert to array objek MSPMaster
                            If Not IsNothing(objMSPType) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objMSPType.ErrorMessage = errorMessage.ToString()
                                _arrMSPType.Add(objMSPType)
                                objMSPType = Nothing
                            End If

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MSPTypeParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPTypeParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        objMSPType = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrMSPType
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim facMSPType As New MSPTypeFacade(user)
            For Each objMSPType As MSPType In _arrMSPType
                Try
                    If Not IsNothing(objMSPType) Then
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(MSPType), "Description", MatchType.Exact, objMSPType.Description))
                        Dim mspTypeList As ArrayList = New MSPTypeFacade(user).Retrieve(criterias)
                        ' update data jika hanya ada perubahan
                        If mspTypeList.Count > 0 Then
                            Dim old As MSPType = mspTypeList(0)
                            old.Code = objMSPType.Code
                            old.Sequence = objMSPType.Sequence
                            If facMSPType.Update(old) < 0 Then
                                nError += 1
                            End If
                        Else
                            ' validate code and sequence before insert new data
                            If facMSPType.ValidateCode(objMSPType.Code) <= 0 Then
                                If facMSPType.ValidateSequence(objMSPType.Code, objMSPType.Sequence) <= 0 Then
                                    ' insert new data
                                    If facMSPType.Insert(objMSPType) < 0 Then
                                        nError += 1
                                    End If
                                Else
                                    sMsg &= "Sequence " + objMSPType.Sequence + "sudah ada.;"
                                    nError += 1
                                End If
                            Else
                                sMsg &= "Code " + objMSPType.Code + "sudah ada.;"
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
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrMSPType.Count.ToString(), "ws-worker", "MSPTypeParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPTypeParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MSPTypeParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPTypeParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function
#End Region

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As MSPType
            ' K;MSPType_TimeStamp\nH;Code;Description;Sequence
            ' K;MSPType_20161011\nH;SP01;SILVER;1\nH;SP02;GOLD;2
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim str As String
            objMSPType = New MSPType

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                Try '1 Code
                    str = cols(1).Trim
                    If str = String.Empty Then
                        Throw New Exception("Empty Code " & str)
                    Else
                        objMSPType.Code = str
                    End If
                Catch ex As Exception
                    writeError("Code  error: " & ex.Message)
                End Try

                '2 Description
                str = cols(2).Trim
                If str = String.Empty Then
                    Throw New Exception("Description can't be empty")
                Else
                    objMSPType.Description = str
                End If

                '3 sequence
                str = cols(3).Trim
                If str = String.Empty Then
                    Throw New Exception("Sequence can't be empty")
                Else
                    objMSPType.Sequence = CInt(str)
                End If


                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    objMSPType.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objMSPType.LastUpdateBy = "WS"
                End If
            End If

            Return objMSPType
        End Function
#End Region

    End Class
End Namespace
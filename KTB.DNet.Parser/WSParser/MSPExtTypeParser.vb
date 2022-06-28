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
Imports KTB.DNET.BusinessFacade
Imports KTB.DNET.BusinessFacade.General
Imports KTB.DNET.BusinessFacade.FinishUnit
Imports KTB.DNET.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
#End Region

Namespace KTB.DNet.Parser
    Public Class MSPExtTypeParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private objMSPExType As MSPExType
        Private _arrMSPExType As ArrayList
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

                _arrMSPExType = New ArrayList()
                objMSPExType = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            ' create objek mspmaster
                            objMSPExType = ParseHeader(line)
                            ' insert to array objek MSPMaster
                            If Not IsNothing(objMSPExType) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objMSPExType.ErrorMessage = errorMessage.ToString()
                                _arrMSPExType.Add(objMSPExType)
                                objMSPExType = Nothing
                            End If

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MSPExTypeExParser.vb", "Parsing", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPExTypeParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        objMSPExType = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrMSPExType
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim facMSPExType As New MSPExTypeFacade(user)
            For Each objMSPExType As MSPExType In _arrMSPExType
                Try
                    If Not IsNothing(objMSPExType) Then
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(MSPExType), "Code", MatchType.Exact, objMSPExType.Code))
                        Dim MSPExTypeList As ArrayList = New MSPExTypeFacade(user).Retrieve(criterias)
                        If MSPExTypeList.Count > 0 Then
                            Dim old As MSPExType = MSPExTypeList(0)
                            old.Description = objMSPExType.Description
                            old.Sequence = objMSPExType.Sequence
                            If facMSPExType.Update(old) < 0 Then
                                nError += 1
                            End If
                        Else
                            If facMSPExType.Insert(objMSPExType) < 0 Then
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
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrMSPExType.Count.ToString(), "ws-worker", "MSPExTypeParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPExTypeParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MSPExTypeParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPExTypeParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object

        End Function

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As MSPExType

            'K;MSPExtType_[TimeStamp]\nH;Code;Description;Sequence
            'K;MSPExtType_202009141000\nH;EX01;2xPM;1
            'MSP TYPE [confirmed]
            'MSPExtType_[TimeStamp]\nH;Code;Description;
            'Sample MSP TYPE :
            'MSPExtType_202009141000\nH;2XPM;2XPM extended smart package;
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim str As String
            objMSPExType = New MSPExType

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                Try '1 Code
                    str = cols(1).Trim
                    If str = String.Empty Then
                        Throw New Exception("Empty Code " & str)
                    Else
                        objMSPExType.Code = str
                    End If
                Catch ex As Exception
                    writeError("Code  error: " & ex.Message)
                End Try

                '2 Description
                str = cols(2).Trim
                If str = String.Empty Then
                    Throw New Exception("Description can't be empty")
                Else
                    objMSPExType.Description = str
                End If


                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    objMSPExType.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objMSPExType.LastUpdateBy = "WS"
                End If
            End If

            Return objMSPExType
        End Function
#End Region

    End Class
End Namespace
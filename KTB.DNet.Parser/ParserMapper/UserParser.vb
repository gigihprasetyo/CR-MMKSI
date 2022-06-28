#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
#End Region

Namespace KTB.DNet.Parser
    Public Class UserParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Users As ArrayList
        Private Grammar As Regex
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            Users = New ArrayList
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String

            While (Not val = "")
                'sStart = 0
                'nCount = 0
                'For Each m As Match In Grammar.Matches(val)
                '    sTemp = val.Substring(sStart, m.Index - sStart)
                '    sTemp = sTemp.Trim("""")
                '    If (nCount = 0) Then
                '        ParseUser(val + ";")
                '    End If
                '    nCount += 1
                'Next
                Try
                    sStart = 0
                    nCount = 0
                    For Each m As Match In Grammar.Matches(val)
                        sTemp = val.Substring(sStart, m.Index - sStart)
                        sTemp = sTemp.Trim("""")
                        If (nCount = 0) Then
                            ParseUser(val + ";")
                        End If
                        nCount += 1
                    Next
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "UserParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.UserParser, BlockName)
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return Users
        End Function

        Protected Overrides Function DoTransaction() As Integer
            If Users.Count > 0 Then
                'Do Business - Like saveTo Database
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseUser(ByVal ValParser As String)
            Dim _User As User = New User
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0

            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        _User.UserId = sTemp
                    Case Is = 1
                        _User.UserName = sTemp
                    Case Else
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            Users.Add(_User)

        End Sub

#End Region

#Region "Public Properties"

        ReadOnly Property UserCollection() As ArrayList
            Get
                Return Users
            End Get
        End Property

#End Region

    End Class
End Namespace
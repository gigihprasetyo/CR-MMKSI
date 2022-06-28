Imports System.Diagnostics

Public Class LogHelper

    Public Shared Sub WriteLog(ByVal sMessage As String)
        Dim el As EventLog = New EventLog
        el.Source = Library.EventLogSource
        el.Log = Library.EventLogName

        el.WriteEntry(sMessage)
    End Sub
End Class

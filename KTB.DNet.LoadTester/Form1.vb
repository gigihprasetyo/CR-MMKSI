Public Class Form1

    Private timer As New Timer()

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim url As String = "http://localhost/phase4/login.aspx"
        Dim uri As New Uri(url)

        Me.WebBrowser1.ScriptErrorsSuppressed = True
        lblTimer.Text = "0 ms"
        timer.Start()
        Me.WebBrowser1.Navigate(uri)

    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        timer.Stop()
        lblTimer.Text = timer.Interval.ToString() & " ms"
    End Sub
End Class

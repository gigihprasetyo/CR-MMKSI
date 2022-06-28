Imports System.Globalization

Public Class ResourcesCultur
    Public Shared ReadOnly Property CultureInfo() As CultureInfo
        Get
            Return CultureInfo.CurrentCulture
        End Get
    End Property

End Class

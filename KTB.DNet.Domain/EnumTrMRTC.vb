Namespace KTB.DNet.Domain
    Public Class EnumTrMRTC
        Public Enum TypePIC
            Head = 1
            Instruktur = 2
        End Enum

        Public Shared Function GetStringValue(ByVal Type As Integer) As String
            Dim str As String = ""
            If Type = 1 Then str = "Head"
            If Type = 2 Then str = "Instruktur"
            Return str
        End Function
    End Class
End Namespace

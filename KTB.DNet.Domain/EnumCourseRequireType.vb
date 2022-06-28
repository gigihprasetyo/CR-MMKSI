Namespace KTB.DNet.Domain
    Public Class EnumCourseRequireType
        Public Enum RequireType
            SyaratLulus = 1
            SyaratBelumLulus = 2
        End Enum

        Public Shared Function GetStringValue(ByVal Type As Integer) As String
            Dim str As String = ""
            If Type = 1 Then str = "Prasyarat Lulus"
            If Type = 2 Then str = "Prasyarat Belum Lulus"
            Return str
        End Function
    End Class


End Namespace

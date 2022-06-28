Namespace KTB.DNet.Domain
    Public Class EnumTrAdditionalClass

        Public Enum EnumStatus
            REQUEST = 1
            REVISI = 2
            APPROVE = 3
        End Enum

        Public Shared Function GetStringValueClassType(ByVal Type As Integer) As String
            Dim str As String = ""
            If Type = 1 Then str = "Request"
            If Type = 2 Then str = "Revisi"
            If Type = 3 Then str = "Approve"
            Return str
        End Function

    End Class
End Namespace

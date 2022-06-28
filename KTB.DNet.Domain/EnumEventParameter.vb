Namespace KTB.DNet.Domain
    Public Class EnumEventParameter
        Public Enum StatusEventParameter As Short
            Baru = 0
            Kirim
        End Enum
        Public Shared Function RetrieveStatus() As ArrayList
            Dim al As New ArrayList
            al.Add(New EnumItem(StatusEventParameter.Baru, StatusEventParameter.Baru.ToString))
            al.Add(New EnumItem(StatusEventParameter.Kirim, StatusEventParameter.Kirim.ToString))
            Return al
        End Function
    End Class
End Namespace
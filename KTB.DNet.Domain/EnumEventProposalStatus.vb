Namespace KTB.DNet.Domain
    Public Class EnumEventProposalStatus
        Public Enum EventProposalStatus As Byte
            Baru = 0
            Validasi
        End Enum
        Public Shared Function RetrieveStatus() As ArrayList
            Dim al As New ArrayList
            al.Add(New EnumItem(EventProposalStatus.Baru, EventProposalStatus.Baru.ToString))
            al.Add(New EnumItem(EventProposalStatus.Validasi, EventProposalStatus.Validasi.ToString))
            Return al
        End Function
    End Class
    Public Class EnumEventAgreementStatus
        Public Enum EventAgreementStatus As Byte
            Baru = 0
            Validasi
        End Enum
        Public Shared Function RetrieveStatus() As ArrayList
            Dim al As New ArrayList
            al.Add(New EnumItem(EventAgreementStatus.Baru, EventAgreementStatus.Baru.ToString))
            al.Add(New EnumItem(EventAgreementStatus.Validasi, EventAgreementStatus.Validasi.ToString))
            Return al
        End Function
    End Class
End Namespace
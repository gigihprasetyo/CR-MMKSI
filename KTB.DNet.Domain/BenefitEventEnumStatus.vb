
Namespace KTB.DNet.Domain

    Public Class BenefitEventEnumStatus


        Public Enum Status
            Baru
            Validasi
            Batal_Validasi
            Disetujui
            Batal_Disetujui
            Ditolak
        End Enum


        

        Public Function RetrieveStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As BenefitEventStatus
            sts = New BenefitEventStatus(0, "Baru")
            al.Add(sts)
            sts = New BenefitEventStatus(1, "Validasi")
            al.Add(sts)
            'sts = New BenefitEventStatus(2, "Batal Validasi")
            'al.Add(sts)
            sts = New BenefitEventStatus(3, "Disetujui")
            al.Add(sts)
            'sts = New BenefitEventStatus(4, "Batal Disetujui")
            'al.Add(sts)
            sts = New BenefitEventStatus(5, "Ditolak")
            al.Add(sts)
            Return al
        End Function


        Public Function RetrieveStatusDealer() As ArrayList
            Dim al As New ArrayList
            Dim sts As BenefitEventStatus
            sts = New BenefitEventStatus(0, "Baru")
            al.Add(sts)
            sts = New BenefitEventStatus(1, "Validasi")
            al.Add(sts)
            sts = New BenefitEventStatus(2, "Batal Validasi")
            al.Add(sts)
            Return al
        End Function


        Public Function RetrieveStatusKTB() As ArrayList
            Dim al As New ArrayList
            Dim sts As BenefitEventStatus
            sts = New BenefitEventStatus(3, "Disetujui")
            al.Add(sts)
            sts = New BenefitEventStatus(4, "Batal Disetujui")
            al.Add(sts)
            sts = New BenefitEventStatus(5, "Ditolak")
            al.Add(sts)
            Return al
           
        End Function

        Public Shared Function GetString(ByVal iStatus As Integer) As String
            Dim str As String = ""
            Select Case iStatus
                Case 0
                    str = "Baru"
                Case 1
                    str = "Validasi"
                Case 2
                    str = "Batal Validasi"
                Case 3
                    str = "Disetujui"
                Case 4
                    str = "Batal Disetujui"
                Case 5
                    str = "Ditolak"
            End Select
            Return str
        End Function

    End Class

    Public Class BenefitEventStatus
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValStatus() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameStatus() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class
End Namespace
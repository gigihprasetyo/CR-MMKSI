Namespace KTB.DNet.Domain

    Public Class EquipmentStatus

        Public Enum EquipmentStatusEnum
            Baru
            Validasi
            'Batal_Validasi
            Konfirmasi
            'Batal_Konfirmasi
            Rilis1
            Rilis2
            'Batal_Rilis
            ' Setuju
            '  Tidak_Setuju
            Ditolak
            'Batal_Tolak
        End Enum

        Public Shared Function RetrieveEquipmentProses() As ArrayList
            Dim al As New ArrayList
            Dim odr As OrderStatusEnum
            odr = New OrderStatusEnum(1, "Validasi")
            al.Add(odr)
            odr = New OrderStatusEnum(2, "Batal_Validasi")
            al.Add(odr)
            odr = New OrderStatusEnum(3, "Konfirmasi")
            al.Add(odr)
            odr = New OrderStatusEnum(4, "Batal_Konfirmasi")
            al.Add(odr)
            odr = New OrderStatusEnum(5, "Rilis1")
            al.Add(odr)
            odr = New OrderStatusEnum(6, "Rilis2")
            al.Add(odr)
            odr = New OrderStatusEnum(7, "Batal_Rilis")
            al.Add(odr)
            'odr = New OrderStatusEnum(8, "Setuju")
            'al.Add(odr)
            'odr = New OrderStatusEnum(9, "Tidak_Setuju")
            'al.Add(odr)
            odr = New OrderStatusEnum(8, "Tolak")
            al.Add(odr)
            odr = New OrderStatusEnum(9, "Batal_Tolak")
            al.Add(odr)
            Return al
        End Function

        Public Shared Function RetrieveEquipmentStatus() As ArrayList
            Dim al As New ArrayList
            Dim odr As OrderStatusEnum
            odr = New OrderStatusEnum(0, "Baru")
            al.Add(odr)
            odr = New OrderStatusEnum(1, "Validasi")
            al.Add(odr)
            odr = New OrderStatusEnum(2, "Konfirmasi")
            al.Add(odr)
            odr = New OrderStatusEnum(3, "Rilis1")
            al.Add(odr)
            odr = New OrderStatusEnum(4, "Rilis2")
            al.Add(odr)
            'odr = New OrderStatusEnum(5, "Setuju")
            'al.Add(odr)
            'odr = New OrderStatusEnum(6, "Tidak_Setuju")
            'al.Add(odr)
            odr = New OrderStatusEnum(5, "Ditolak")
            al.Add(odr)
            Return al
        End Function


    End Class

    Public Class OrderStatusEnum

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
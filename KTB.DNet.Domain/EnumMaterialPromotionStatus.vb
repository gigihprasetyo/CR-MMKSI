Namespace KTB.DNet.Domain
    Public Class EnumMaterialPromotion

        Public Enum MaterialPromotionStatus
            Baru = 0
            Batal = 1
            Validasi = 2
            Disetujui = 3
            Ditolak = 4
        End Enum

        Public Enum MasterMaterialPromotionStatus
            Pilih = 2
            Aktif = 0
            Tidak_Aktif = 1
        End Enum

        Public Shared Function RetrieveMPMasterStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumMaterialPromotionStatus
            sts = New EnumMaterialPromotionStatus(2, "Silahkan Pilih")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(0, "Aktif")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(1, "Tidak Aktif")
            al.Add(sts)

            Return al
        End Function

        Public Function RetrieveMPMasterStatusAlert() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumMaterialPromotionStatus
            sts = New EnumMaterialPromotionStatus(0, "Aktif")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(1, "Tidak Aktif")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function RetrieveMaterialPromotionStatusUpdateDealer() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumMaterialPromotionStatus
            sts = New EnumMaterialPromotionStatus(1, "Batal")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(2, "Validasi")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveMaterialPromotionStatusGAB() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumMaterialPromotionStatus
            sts = New EnumMaterialPromotionStatus(0, "Baru")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(1, "Batal")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(2, "Validasi")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(3, "Disetujui")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(4, "Ditolak")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveMaterialPromotionStatusGABAlert() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumMaterialPromotionStatus
            sts = New EnumMaterialPromotionStatus(0, "Baru")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(2, "Validasi")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(3, "Disetujui")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(4, "Ditolak")
            al.Add(sts)
            Return al
        End Function

        'refer bug 1343
        Public Function RetrieveMaterialPromotionStatusKTB() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumMaterialPromotionStatus
            'sts = New EnumMaterialPromotionStatus(0, "Baru")
            'al.Add(sts)
            'sts = New EnumMaterialPromotionStatus(1, "Batal")
            'al.Add(sts)
            sts = New EnumMaterialPromotionStatus(2, "Validasi")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(3, "Disetujui")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(4, "Ditolak")
            al.Add(sts)
            Return al
        End Function

        'refer bug 1343
        Public Function RetrieveMaterialPromotionStatusDealer() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumMaterialPromotionStatus
            sts = New EnumMaterialPromotionStatus(0, "Baru")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(1, "Batal")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(2, "Validasi")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(3, "Disetujui")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(4, "Ditolak")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function RetrieveMaterialPromotionStatusForKTB() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumMaterialPromotionStatus
            sts = New EnumMaterialPromotionStatus(3, "Disetujui")
            al.Add(sts)
            sts = New EnumMaterialPromotionStatus(4, "Ditolak")
            al.Add(sts)
            Return al
        End Function
    End Class

    Public Class EnumMaterialPromotionStatus
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public ReadOnly Property ValStatus() As Integer
            Get
                Return _val
            End Get
        End Property

        Public ReadOnly Property NameStatus() As String
            Get
                Return _Name
            End Get
        End Property
    End Class

End Namespace


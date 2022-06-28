Namespace KTB.DNet.Domain
    Public Class PartIncidentalStatus
        Public Enum PartIncidentalStatusEnum
            Pemesanan
            Estimasi
            Lain_lain
        End Enum

        Public Shared Function RetrievePartIncidentalStatus() As ArrayList
            Dim al As New ArrayList
            Dim objPartIncidentalStatus As PartIncidentalStatusList
            objPartIncidentalStatus = New PartIncidentalStatusList(0, CType(0, PartIncidentalStatus.PartIncidentalStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            objPartIncidentalStatus = New PartIncidentalStatusList(1, CType(1, PartIncidentalStatus.PartIncidentalStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            objPartIncidentalStatus = New PartIncidentalStatusList(2, CType(2, PartIncidentalStatus.PartIncidentalStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            Return al
        End Function

        Public Enum PartIncidentalEmailStatusEnum
            Belum_Dikirim
            Dikirim
        End Enum

        Public Shared Function RetrievePartIncidentalEmailStatus() As ArrayList
            Dim al As New ArrayList
            Dim objPartIncidentalStatus As PartIncidentalStatusList
            objPartIncidentalStatus = New PartIncidentalStatusList(0, CType(0, PartIncidentalStatus.PartIncidentalEmailStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            objPartIncidentalStatus = New PartIncidentalStatusList(1, CType(1, PartIncidentalStatus.PartIncidentalEmailStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            Return al
        End Function

        Public Enum PartIncidentalKTBStatusEnum
            Baru
            Sedang_Proses
            Hapus
            Selesai
            Batal
            Batal_Sebagian
        End Enum

        Public Shared Function RetrievePartIncidentalKTBStatus() As ArrayList
            Dim al As New ArrayList
            Dim objPartIncidentalStatus As PartIncidentalStatusList
            objPartIncidentalStatus = New PartIncidentalStatusList(-1, "Silahkan Pilih")
            al.Add(objPartIncidentalStatus)
            objPartIncidentalStatus = New PartIncidentalStatusList(0, CType(0, PartIncidentalStatus.PartIncidentalKTBStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            objPartIncidentalStatus = New PartIncidentalStatusList(1, CType(1, PartIncidentalStatus.PartIncidentalKTBStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            objPartIncidentalStatus = New PartIncidentalStatusList(2, CType(2, PartIncidentalStatus.PartIncidentalKTBStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            objPartIncidentalStatus = New PartIncidentalStatusList(3, CType(3, PartIncidentalStatus.PartIncidentalKTBStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            objPartIncidentalStatus = New PartIncidentalStatusList(4, CType(4, PartIncidentalStatus.PartIncidentalKTBStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            objPartIncidentalStatus = New PartIncidentalStatusList(5, CType(5, PartIncidentalStatus.PartIncidentalKTBStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            Return al
        End Function

        Public Enum PartIncidentalDetailStatusEnum
            Aktif
            Batal
            Batal_Sebagian
        End Enum

        Public Shared Function RetrievePartIncidentalDetailStatus() As ArrayList
            Dim al As New ArrayList
            Dim objPartIncidentalStatus As PartIncidentalStatusList
            objPartIncidentalStatus = New PartIncidentalStatusList(-1, "Silahkan Pilih")
            al.Add(objPartIncidentalStatus)
            objPartIncidentalStatus = New PartIncidentalStatusList(0, CType(0, PartIncidentalStatus.PartIncidentalDetailStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            objPartIncidentalStatus = New PartIncidentalStatusList(1, CType(1, PartIncidentalStatus.PartIncidentalDetailStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            objPartIncidentalStatus = New PartIncidentalStatusList(2, CType(2, PartIncidentalStatus.PartIncidentalDetailStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            Return al
        End Function

        Public Function RetrievePartIncidentalKTBStatusAlert() As ArrayList
            Dim al As New ArrayList
            Dim objPartIncidentalStatus As PartIncidentalStatusList
            objPartIncidentalStatus = New PartIncidentalStatusList(0, CType(0, PartIncidentalStatus.PartIncidentalKTBStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            objPartIncidentalStatus = New PartIncidentalStatusList(1, CType(1, PartIncidentalStatus.PartIncidentalKTBStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            objPartIncidentalStatus = New PartIncidentalStatusList(2, CType(2, PartIncidentalStatus.PartIncidentalKTBStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            objPartIncidentalStatus = New PartIncidentalStatusList(3, CType(3, PartIncidentalStatus.PartIncidentalKTBStatusEnum).ToString)
            al.Add(objPartIncidentalStatus)
            Return al
        End Function
    End Class

    Public Class PartIncidentalStatusList
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

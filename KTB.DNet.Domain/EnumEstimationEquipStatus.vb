Namespace KTB.DNet.Domain
    Public Class EnumEstimationEquipStatus

        Public Const TYPE_EQUIPMENT As String = "E"

        Public Enum EstimationEquipStatusHeader
            'Status Untuk Dealer
            Baru = 0
            Kirim = 1
            Batal = 2
            Konfirmasi_Sebagian = 3
            Selesai = 4
            Expired = 9
        End Enum

        Public Enum EstimationEquipStatusDetail
            'Status Untuk KTB
            BelumKonfirmasi = 0
            Konfirmasi_BelumOrder = 1
            Konfirmasi_SudahOrder = 2
        End Enum

        Public Enum EstimationEquipmentPaymentType
            Silakan_Pilih
            Deposit_B
            Deposit_C
        End Enum

        Public Function GetEstimationEquipStatusHeader(ByVal status As Integer) As String
            Dim str As String = ""
            If status = 0 Then str = "Baru"
            If status = 1 Then str = "Kirim"
            If status = 2 Then str = "Batal"
            If status = 3 Then str = "Konfirmasi Sebagian"
            If status = 4 Then str = "Selesai"
            If status = 9 Then str = "Expired"
            Return str
        End Function

        Public Function RetrievePaymentType() As ArrayList
            Dim al As New ArrayList
            Dim ipstatus As EnumEquipStatus
            ipstatus = New EnumEquipStatus(EstimationEquipmentPaymentType.Silakan_Pilih, EstimationEquipmentPaymentType.Silakan_Pilih.ToString())
            al.Add(ipstatus)

            ipstatus = New EnumEquipStatus(EstimationEquipmentPaymentType.Deposit_B, EstimationEquipmentPaymentType.Deposit_B.ToString())
            al.Add(ipstatus)

            ipstatus = New EnumEquipStatus(EstimationEquipmentPaymentType.Deposit_C, EstimationEquipmentPaymentType.Deposit_C.ToString())
            al.Add(ipstatus)

            Return (al)
        End Function

        Public Function RetrieveTypeForKTB() As ArrayList
            Dim al As New ArrayList
            Dim IPStatus As EnumEquipStatus

            IPStatus = New EnumEquipStatus(EstimationEquipStatusHeader.Kirim, EstimationEquipStatusHeader.Kirim.ToString)
            al.Add(IPStatus)

            IPStatus = New EnumEquipStatus(EstimationEquipStatusHeader.Konfirmasi_Sebagian, EstimationEquipStatusHeader.Konfirmasi_Sebagian.ToString)
            al.Add(IPStatus)

            IPStatus = New EnumEquipStatus(EstimationEquipStatusHeader.Selesai, EstimationEquipStatusHeader.Selesai.ToString)
            al.Add(IPStatus)

            Return al
        End Function

        'Public Function RetrieveTypeUpdateForKTB() As ArrayList
        '    Dim al As New ArrayList
        '    Dim IPStatus As EnumEquipStatus

        '    IPStatus = New EnumEquipStatus(EstimationEquipStatusHeader.Selesai, EstimationEquipStatusHeader.Kirim.ToString)
        '    al.Add(IPStatus)

        '    Return al
        'End Function


        Public Function RetrieveTypeForDealer() As ArrayList
            Dim al As New ArrayList
            Dim IPStatus As EnumEquipStatus

            IPStatus = New EnumEquipStatus(EstimationEquipStatusHeader.Baru, EstimationEquipStatusHeader.Baru.ToString)
            al.Add(IPStatus)

            IPStatus = New EnumEquipStatus(EstimationEquipStatusHeader.Batal, EstimationEquipStatusHeader.Batal.ToString)
            al.Add(IPStatus)

            IPStatus = New EnumEquipStatus(EstimationEquipStatusHeader.Kirim, EstimationEquipStatusHeader.Kirim.ToString)
            al.Add(IPStatus)

            Return al
        End Function

        Public Function RetrieveTypeUpdateForDealer() As ArrayList
            Dim al As New ArrayList
            Dim IPStatus As EnumEquipStatus

            IPStatus = New EnumEquipStatus(-1, "Silahkan Pilih")
            al.Add(IPStatus)

            IPStatus = New EnumEquipStatus(EstimationEquipStatusHeader.Batal, EstimationEquipStatusHeader.Batal.ToString)
            al.Add(IPStatus)

            IPStatus = New EnumEquipStatus(EstimationEquipStatusHeader.Kirim, EstimationEquipStatusHeader.Kirim.ToString)
            al.Add(IPStatus)

            Return al
        End Function

    End Class

    Public Class EnumEquipStatus
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub

        Public Property ValType() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameType() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property
    End Class

    Public Class SPPODetailEstimateStatus
        Public Const BelumKonfirmasi As String = "0"
        Public Const Konfirmasi_BelumOrder As String = "1"
        Public Const Konfirmasi_SudahOrder As String = "2"
    End Class

End Namespace

Namespace KTB.DNet.Domain

    Public Class EnumFieldFixPartOrderStatus
        Public Enum FieldFixPartOrderStatus As Short
            None = 0
            Baru = 1
            Validasi = 2
            Proses = 3
            Kirim = 4
            Batal = 5
            Pemenuhan_Sebagian = 6
            Stok_Kosong = 7
        End Enum

        Public Shared Function RetrieveFieldFixPartOrderStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumFieldFixPartOrderStatusValue

            sts = New EnumFieldFixPartOrderStatusValue(1, "Baru")
            al.Add(sts)
            sts = New EnumFieldFixPartOrderStatusValue(2, "Validasi")
            al.Add(sts)
            sts = New EnumFieldFixPartOrderStatusValue(3, "Proses")
            al.Add(sts)
            sts = New EnumFieldFixPartOrderStatusValue(4, "Kirim")
            al.Add(sts)
            sts = New EnumFieldFixPartOrderStatusValue(5, "Batal")
            al.Add(sts)
            sts = New EnumFieldFixPartOrderStatusValue(6, "Pemenuhan Sebagian")
            al.Add(sts)
            sts = New EnumFieldFixPartOrderStatusValue(7, "Stok Kosong")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function RetrieveFieldFixPartOrderDetailStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumFieldFixPartOrderStatusValue

            sts = New EnumFieldFixPartOrderStatusValue(1, "Baru")
            al.Add(sts)
            sts = New EnumFieldFixPartOrderStatusValue(2, "Validasi")
            al.Add(sts)
            sts = New EnumFieldFixPartOrderStatusValue(3, "Proses")
            al.Add(sts)
            sts = New EnumFieldFixPartOrderStatusValue(4, "Kirim")
            al.Add(sts)
            sts = New EnumFieldFixPartOrderStatusValue(5, "Batal")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function GetStringValue(ByVal FieldFixPartOrderStatus As String) As String
            Dim str As String = ""
            If FieldFixPartOrderStatus = "1" Then str = "Baru"
            If FieldFixPartOrderStatus = "2" Then str = "Validasi"
            If FieldFixPartOrderStatus = "3" Then str = "Proses"
            If FieldFixPartOrderStatus = "4" Then str = "Kirim"
            If FieldFixPartOrderStatus = "5" Then str = "Batal"
            If FieldFixPartOrderStatus = "6" Then str = "Pemenuhan Sebagian"
            If FieldFixPartOrderStatus = "7" Then str = "Stok Kosong"
            Return str
        End Function
    End Class

    Public Class EnumFieldFixPartOrderStatusValue
        Private _code As Integer
        Private _desc As String

        Public Sub New()

        End Sub

        Public Sub New(ByVal code As Integer, ByVal desc As String)
            _code = code
            _desc = desc
        End Sub

        Public Property Code() As Integer
            Get
                Return _code
            End Get
            Set(ByVal Value As Integer)
                _code = Value
            End Set
        End Property

        Public Property Desc() As String
            Get
                Return _desc
            End Get
            Set(ByVal Value As String)
                _desc = Value
            End Set
        End Property

    End Class
End Namespace

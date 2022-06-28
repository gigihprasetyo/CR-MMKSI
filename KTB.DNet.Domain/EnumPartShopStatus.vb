Namespace KTB.DNet.Domain

    Public Class EnumPartShopStatus


        Public Enum PartShopStatus
            Baru = 0
            Request_ID = 1
            Aktif = 2
            Tidak_Aktif = 3
        End Enum

        Public Function Baru() As String
            Return PartShopStatus.Baru
        End Function

        Public Function RequestID() As String
            Return PartShopStatus.Request_ID
        End Function

        Public Function Registered() As String
            Return PartShopStatus.Aktif
        End Function

        Public Shared Function GetStringValue(ByVal Status As Integer) As String
            Dim str As String = ""
            If Status = PartShopStatus.Baru Then str = PartShopStatus.Baru.ToString
            If Status = PartShopStatus.Request_ID Then str = PartShopStatus.Request_ID.ToString
            If Status = PartShopStatus.Aktif Then str = PartShopStatus.Aktif.ToString
            If Status = PartShopStatus.Tidak_Aktif Then str = PartShopStatus.Tidak_Aktif.ToString
            Return str
        End Function

        Public Function RetrieveStatus() As ArrayList


            Dim al As New ArrayList
            Dim sts As EnumPartShopStatusProp
            sts = New EnumPartShopStatusProp(-1, "Semua")
            al.Add(sts)
            sts = New EnumPartShopStatusProp(0, "Baru")
            al.Add(sts)
            sts = New EnumPartShopStatusProp(1, "Request ID")
            al.Add(sts)
            sts = New EnumPartShopStatusProp(2, "Aktif")
            al.Add(sts)
            sts = New EnumPartShopStatusProp(3, "Tidak Aktif")
            al.Add(sts)
            Return al

        End Function

        Public Function RetrieveStatusEdit() As ArrayList


            Dim al As New ArrayList
            Dim sts As EnumPartShopStatusProp
            sts = New EnumPartShopStatusProp(2, "Aktif")
            al.Add(sts)
            sts = New EnumPartShopStatusProp(3, "Tidak Aktif")
            al.Add(sts)
            Return al

        End Function

    End Class

    Public Class EnumPartShopStatusProp
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


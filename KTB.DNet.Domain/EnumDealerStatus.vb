Namespace KTB.DNet.Domain

    Public Class EnumDealerStatus
        Public Enum DealerStatus
            NonAktive
            Aktive
        End Enum

        Public Function RetrieveStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumDealer
            sts = New EnumDealer(0, "Tidak Aktif")
            al.Add(sts)
            sts = New EnumDealer(1, "Aktif")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveStatusPublish() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumDealer
            sts = New EnumDealer(0, "Not Publish")
            al.Add(sts)
            sts = New EnumDealer(1, "Publish")
            al.Add(sts)
            Return al
        End Function

    End Class

    Public Class EnumDealer
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


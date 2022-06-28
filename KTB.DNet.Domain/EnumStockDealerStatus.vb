Namespace KTB.DNet.Domain

    Public Class EnumStockDealerStatus

        Public Enum StockDealerStatus
            Baru = 0
            Validasi = 1
        End Enum

        Public Function Baru() As String
            Return StockDealerStatus.Baru
        End Function

        Public Function Validasi() As String
            Return StockDealerStatus.Validasi
        End Function

        Public Function GetStringValue(ByVal ID As Integer) As String
            If (ID = 0) Then
                Return "Baru"
            ElseIf (ID = 1) Then
                Return "Validasi"
            End If
        End Function

        Public Function RetrieveStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumStockDealerStatusProp
            sts = New EnumStockDealerStatusProp(0, "Baru")
            al.Add(sts)
            sts = New EnumStockDealerStatusProp(1, "Validasi")
            al.Add(sts)
            Return al
        End Function

    End Class

    Public Class EnumStockDealerStatusProp
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


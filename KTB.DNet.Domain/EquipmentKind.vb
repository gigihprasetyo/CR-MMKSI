 

Namespace KTB.DNet.Domain

    Public Class EquipmentKind
        Public Enum EquipmentKindEnum
            Pembelian
            Pembelian_Baru
            Perbaikan
        End Enum

        Public Shared Function RetrieveEquipmentKind() As ArrayList
            Dim al As New ArrayList
            Dim kind As KindEnum
            'kind = New KindEnum(0, "Pembelian")
            'al.Add(kind)
            kind = New KindEnum(1, "Pembelian_Baru")
            al.Add(kind)
            kind = New KindEnum(2, "Perbaikan")
            al.Add(kind)
            Return al
        End Function

    End Class

    Public Class KindEnum
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



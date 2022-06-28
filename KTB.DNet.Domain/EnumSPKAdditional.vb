Namespace KTB.DNet.Domain
    Public Class EnumSPKAdditional
        Public Enum SPKAdditionalParts
            Aksesoris
            Komponen_Lainnya
            Tidak_Ada
        End Enum

        Public Function RetrieveSPKAdditional() As ArrayList
            Dim al As New ArrayList
            Dim prts As EnumSPKAdditionalParts
            prts = New EnumSPKAdditionalParts("0", "Aksesories")
            al.Add(prts)
            prts = New EnumSPKAdditionalParts("1", "Komponen / Lainnya")
            al.Add(prts)
            prts = New EnumSPKAdditionalParts("2", "Tidak Ada")
            al.Add(prts)
            Return al
        End Function
    End Class


    Public Class EnumSPKAdditionalParts
        Private _val As String
        Private _Name As String


        Public Sub New(ByVal val As String, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValParts() As String
            Get
                Return _val
            End Get
            Set(ByVal Value As String)
                _val = Value
            End Set
        End Property

        Property NameParts() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class
End Namespace

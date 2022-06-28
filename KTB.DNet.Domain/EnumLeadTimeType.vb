Namespace KTB.DNet.Domain

    Public Class EnumLeadTimeType
        Public Enum LeadTimeType
            RO
            EO
        End Enum

        Public Function RetrieveType() As ArrayList
            Dim al As New ArrayList
            Dim typ As EnumType
            typ = New EnumType(0, "Reguler Order")
            al.Add(typ)
            typ = New EnumType(1, "Emergency Order")
            al.Add(typ)
            Return al
        End Function

    End Class

    Public Class EnumType
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


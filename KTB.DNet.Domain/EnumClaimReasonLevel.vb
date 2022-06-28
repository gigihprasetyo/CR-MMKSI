Namespace KTB.DNet.Domain

    Public Class EnumClaimReasonLevel

        Public Enum ClaimReasonLevel
            Item
            Header
        End Enum

        Public Function RetrieveType() As ArrayList
            Dim al As New ArrayList
            Dim ClmLevel As EnumClaimReasonlvl
            ClmLevel = New EnumClaimReasonlvl(0, "Item")
            al.Add(ClmLevel)
            ClmLevel = New EnumClaimReasonlvl(1, "Header")
            al.Add(ClmLevel)
            Return al
        End Function

    End Class

    Public Class EnumClaimReasonlvl

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

End Namespace



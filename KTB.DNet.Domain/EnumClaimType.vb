Namespace KTB.DNet.Domain

    Public Class EnumClaimType


        Public Enum ClaimType
            Z2 = 0
            Z4 = 1
            ZA = 2
            ZB = 3
        End Enum

        Public Function Z2() As String
            Return ClaimType.Z2
        End Function

        Public Function Z4() As String
            Return ClaimType.Z4
        End Function

        Public Function ZA() As String
            Return ClaimType.ZA
        End Function

        Public Function ZB() As String
            Return ClaimType.ZB
        End Function

        Public Function RetrieveClaimType() As ArrayList
            Dim al As New ArrayList
            Dim typ As EnumClaimTypeProp
            typ = New EnumClaimTypeProp(0, "Z2")
            al.Add(typ)
            typ = New EnumClaimTypeProp(1, "Z4")
            al.Add(typ)
            typ = New EnumClaimTypeProp(2, "ZA")
            al.Add(typ)
            typ = New EnumClaimTypeProp(3, "ZB")
            al.Add(typ)
            Return al
        End Function

    End Class

    Public Class EnumClaimTypeProp
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValClaimType() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameClaimType() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class
End Namespace


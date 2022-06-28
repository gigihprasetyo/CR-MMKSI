Namespace KTB.DNet.Domain
    Public Class EnumChangesDataType
        Public Enum EnumChangesDataType
            [Default] = 0
            [Date] = 1
            [String] = 2
            Amount = 3
            Qty = 4

        End Enum

        Public Function [Default]() As String
            Return EnumChangesDataType.[Default]
        End Function

        Public Function [Date]() As String
            Return EnumChangesDataType.[Date]
        End Function

        Public Function [String]() As String
            Return EnumChangesDataType.[String]
        End Function

        Public Function Amount() As String
            Return EnumChangesDataType.Amount
        End Function

        Public Function Qty() As String
            Return EnumChangesDataType.Qty
        End Function

        Public Function RetrieveClaimType() As ArrayList
            Dim al As New ArrayList
            Dim typ As EnumChangesDataTypeProp
            typ = New EnumChangesDataTypeProp(0, "Default")
            al.Add(typ)
            typ = New EnumChangesDataTypeProp(1, "Date")
            al.Add(typ)
            typ = New EnumChangesDataTypeProp(2, "String")
            al.Add(typ)
            typ = New EnumChangesDataTypeProp(3, "Amount")
            al.Add(typ)
            typ = New EnumChangesDataTypeProp(4, "Qty")
            al.Add(typ)
            Return al
        End Function

    End Class

    Public Class EnumChangesDataTypeProp
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValChangesDataType() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameChangesDataType() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class
End Namespace
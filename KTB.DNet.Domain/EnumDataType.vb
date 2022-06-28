Namespace KTB.DNet.Domain
    Public Class EnumDataType

        Public Enum DataType
            Text = 1
            Dates = 2
            Numeric = 3
        End Enum

        Public Function RetrieveDataType() As ArrayList
            Dim al As New ArrayList
            Dim dt As EnumDataTypeDesc
            dt = New EnumDataTypeDesc(1, "Text")
            al.Add(dt)
            dt = New EnumDataTypeDesc(2, "Dates")
            al.Add(dt)
            dt = New EnumDataTypeDesc(3, "Numeric")
            al.Add(dt)

            Return al
        End Function
    End Class

    Public Class EnumDataTypeDesc
        Private _val As Integer
        Private _desc As String

        Public Sub New(ByVal val As Integer, ByVal desc As String)
            _val = val
            _desc = desc
        End Sub

        Public Property ValStatus() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Public Property DescStatus() As String
            Get
                Return _desc
            End Get
            Set(ByVal Value As String)
                _desc = Value
            End Set
        End Property
    End Class

End Namespace

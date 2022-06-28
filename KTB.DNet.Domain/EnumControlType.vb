Namespace KTB.DNet.Domain
    Public Class EnumControlType
        Public Enum ControlType
            Text = 1
            List = 2
            Calendar = 3
            CheckListBox = 4
        End Enum

        Public Function RetrieveControlType() As ArrayList
            Dim al As New ArrayList
            Dim dt As EnumControlTypeDesc
            dt = New EnumControlTypeDesc(1, "Text")
            al.Add(dt)
            dt = New EnumControlTypeDesc(2, "List")
            al.Add(dt)
            dt = New EnumControlTypeDesc(3, "Calendar")
            al.Add(dt)
            dt = New EnumControlTypeDesc(4, "CheckListBox")
            al.Add(dt)
            Return al
        End Function
    End Class

    Public Class EnumControlTypeDesc

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

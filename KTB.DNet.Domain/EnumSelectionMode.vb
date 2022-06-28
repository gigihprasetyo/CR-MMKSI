Namespace KTB.DNet.Domain
    Public Class EnumSelectionMode

        Public Enum SelectionMode
            SingleSel
            MultiSel
        End Enum

        Public Function RetrieveSelectionMode() As ArrayList
            Dim al As New ArrayList
            Dim dt As EnumSelectionModeDesc
            dt = New EnumSelectionModeDesc(1, "Single")
            al.Add(dt)
            dt = New EnumSelectionModeDesc(2, "Multi")
            al.Add(dt)

            Return al
        End Function
    End Class

    Public Class EnumSelectionModeDesc
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

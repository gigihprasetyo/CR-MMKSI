Namespace KTB.DNet.Domain
    Public Class EnumStatusProfile

        Public Enum StatusMode
            Active = 1
            Inactive = 2
        End Enum

        Public Function RetrieveStatusMode() As ArrayList
            Dim al As New ArrayList
            Dim dt As EnumStatusModeDesc
            dt = New EnumStatusModeDesc(1, "Active")
            al.Add(dt)
            dt = New EnumStatusModeDesc(2, "Inactive")
            al.Add(dt)

            Return al
        End Function
    End Class

    Public Class EnumStatusModeDesc
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

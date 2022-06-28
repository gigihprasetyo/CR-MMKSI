Namespace KTB.DNet.Domain
    Public Class EnumMSPProgram
        Public Enum MSPProgram
            MSPExtended = 1
            FleetPackage = 2
        End Enum
        Public Function RetrieveMSPProgram() As ArrayList
            Dim al As New ArrayList
            Dim dt As EnumMSPProgramDesc
            dt = New EnumMSPProgramDesc(1, "MSP Extended")
            al.Add(dt)
            dt = New EnumMSPProgramDesc(2, "Fleet Package")
            al.Add(dt)

            Return al
        End Function
    End Class

    Public Class EnumMSPProgramDesc
        Private _val As Integer
        Private _desc As String

        Public Sub New(ByVal val As Integer, ByVal desc As String)
            _val = val
            _desc = desc
        End Sub

        Public Property Val() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Public Property Desc() As String
            Get
                Return _desc
            End Get
            Set(ByVal Value As String)
                _desc = Value
            End Set
        End Property
    End Class
End Namespace

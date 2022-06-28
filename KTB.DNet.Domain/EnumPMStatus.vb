Namespace KTB.DNet.Domain

    Public Class EnumPMStatus

        Public Enum PMStatus
            Baru = 0
            Proses = 2
            Selesai = 3
        End Enum

        Public Function RetrieveFSStatus() As ArrayList
            Dim al As New ArrayList

            Dim sts As PMEnum
            sts = New PMEnum(0, "Baru")
            al.Add(sts)
            sts = New PMEnum(2, "Proses")
            al.Add(sts)
            sts = New PMEnum(3, "Selesai")
            al.Add(sts)
            Return al
        End Function

    End Class
    Public Class PMEnum
        Private _code As Integer
        Private _desc As String

        Public Sub New()

        End Sub

        Public Sub New(ByVal code As Integer, ByVal desc As String)
            _code = code
            _desc = desc
        End Sub

        Public Property Code() As Integer
            Get
                Return _code
            End Get
            Set(ByVal Value As Integer)
                _code = Value
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



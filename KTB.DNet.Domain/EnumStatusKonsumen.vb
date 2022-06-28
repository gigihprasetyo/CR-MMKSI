

Namespace KTB.DNet.Domain

    Public Class EnumStatusKonsumen


        Public Enum StatusKonsumen
            Maintain = 0
            ReConquest = 1
            Conquest = 2
        End Enum


        Public Function RetrieveStatusKonsumen() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumStsKonsumen
            sts = New EnumStsKonsumen(0, "Maintain")
            al.Add(sts)
            sts = New EnumStsKonsumen(1, "Re-Conquest")
            al.Add(sts)
            sts = New EnumStsKonsumen(2, "Conquest")
            al.Add(sts)
            Return al
        End Function
    End Class

    Public Class EnumStsKonsumen
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public ReadOnly Property ValStatus() As Integer
            Get
                Return _val
            End Get
        End Property

        Public ReadOnly Property NameStatus() As String
            Get
                Return _Name
            End Get
        End Property

    End Class

End Namespace

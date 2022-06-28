Namespace KTB.DNet.Domain
    Public Class EnumPDIKind
        Public Enum PDIKind
            A
            B
            C
            D
        End Enum

        Public Function RetrievePDIKind() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumPDI
            sts = New EnumPDI(0, "A")
            al.Add(sts)
            sts = New EnumPDI(1, "B")
            al.Add(sts)
            sts = New EnumPDI(2, "C")
            al.Add(sts)
            sts = New EnumPDI(3, "D")
            al.Add(sts)

            Return al
        End Function

    End Class

    Public Class EnumPDI
        Private _val As Integer
        Private _Name As String


        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValPDIKind() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NamePDIKind() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class

End Namespace

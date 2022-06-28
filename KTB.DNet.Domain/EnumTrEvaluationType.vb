Namespace KTB.DNet.Domain
    Public Class EnumTrEvaluationType
        Public Enum TrEvaluationType
            Angka
            Sikap
            Prestasi
        End Enum

        Public Function Retrieve() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumEvaluationType
            sts = New EnumEvaluationType(0, "Angka")
            al.Add(sts)
            sts = New EnumEvaluationType(1, "Sikap")
            al.Add(sts)
            'sts = New EnumEvaluationType(2, "Prestasi")
            'al.Add(sts)
            Return al
        End Function

    End Class

    Public Class EnumEvaluationType
        Private _val As Integer
        Private _Name As String


        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValueType() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameType() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class

End Namespace

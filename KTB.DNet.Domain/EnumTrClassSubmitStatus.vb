Namespace KTB.DNet.Domain
    Public Class EnumTrClassSubmitStatus
        Public Enum TrClassSubmitStatus
            Submited
            NotSubmited
        End Enum

        Public Function RetrieveStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumSubmitStatus
            sts = New EnumSubmitStatus(0, "Sudah Submit")
            al.Add(sts)
            sts = New EnumSubmitStatus(1, "Belum Submit")
            al.Add(sts)
            Return al
        End Function

    End Class

    Public Class EnumSubmitStatus
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValStatus() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameStatus() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class
End Namespace





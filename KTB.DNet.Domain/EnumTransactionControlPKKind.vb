
Namespace KTB.DNet.Domain
    Public Class EnumTransactionControlPKKind
        Public Enum TransactionControlPKKind
            INPUT_PK_TAMBAHAN = 0
            KONFIRMASI_PK_TAMBAHAN = 1
        End Enum

        Public Function RetrieveTransKind() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTransPKKind
            sts = New EnumTransPKKind(TransactionControlPKKind.INPUT_PK_TAMBAHAN, "Bypass Input PK Tambahan")
            al.Add(sts)
            sts = New EnumTransPKKind(TransactionControlPKKind.KONFIRMASI_PK_TAMBAHAN, "Bypass Konfirmasi PK")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveTransKindFlag() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTransPKKind
            sts = New EnumTransPKKind(0, "InputPKFlag")
            al.Add(sts)
            sts = New EnumTransPKKind(1, "KonfirmasiPKFlag")
            al.Add(sts)
            Return al
        End Function

    End Class
    Public Class EnumTransPKKind
        Private _val As Integer
        Private _Name As String


        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValTransKind() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameTransKind() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class
End Namespace
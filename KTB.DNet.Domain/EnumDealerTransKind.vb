Namespace KTB.DNet.Domain
    Public Class EnumDealerTransKind
        Public Enum DealerTransKind
            SalesUnit
            ServiceUnit
            SparePartUnit
        End Enum

        Public Function RetrieveTransKind() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTransKind
            sts = New EnumTransKind(0, "Sales Unit")
            al.Add(sts)
            sts = New EnumTransKind(1, "Service Unit")
            al.Add(sts)
            sts = New EnumTransKind(2, "Spare Part")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveTransKindFlag() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTransKind
            sts = New EnumTransKind(0, "SalesUnitFlag")
            al.Add(sts)
            sts = New EnumTransKind(1, "ServiceFlag")
            al.Add(sts)
            sts = New EnumTransKind(2, "SparepartFlag")
            al.Add(sts)
            Return al
        End Function

    End Class


    Public Class EnumTransKind
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

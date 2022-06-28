Namespace KTB.DNet.Domain

    Public Class EnumDealerType
        Public Enum DealerType
            Semua
            Dealer
            Branch
        End Enum

        Public Function RetrieveType() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumDealerTypeProperty
            sts = New EnumDealerTypeProperty(0, "Semua")
            al.Add(sts)
            sts = New EnumDealerTypeProperty(1, "Dealer")
            al.Add(sts)
            sts = New EnumDealerTypeProperty(2, "Branch")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function GetStringValue(ByVal iType As Integer) As String
            Dim str As String = ""
            If iType = 0 Then str = "Semua"
            If iType = 1 Then str = "Dealer"
            If iType = 2 Then str = "Branch"
            Return str
        End Function
    End Class

    Public Class EnumDealerTypeProperty
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValType() As Integer
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


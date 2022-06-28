Namespace KTB.DNet.Domain

    Public Class EnumDealerBranchType
        Public Enum BranchType
            TemporaryOutlet
            Outlet
        End Enum

        Public Enum DealerBranchType
            TemporaryOutlet = 3
            Outlet = 2
        End Enum

        Public Function Retrieve() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumDealerBranch
            sts = New EnumDealerBranch(3, "Temporary Outlet")
            al.Add(sts)
            sts = New EnumDealerBranch(2, "Outlet")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumDealerBranch
            sts = New EnumDealerBranch(0, "Temporary Outlet")
            al.Add(sts)
            sts = New EnumDealerBranch(1, "Outlet")
            al.Add(sts)
            Return al
        End Function
    End Class

     
    Public Class EnumDealerBranchStatus
        Public Enum DealerBranchStatus
            NonAktive
            Aktive
        End Enum

        Public Function RetrieveStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumDealerBranch
            sts = New EnumDealerBranch(0, "Tidak Aktif")
            al.Add(sts)
            sts = New EnumDealerBranch(1, "Aktif")
            al.Add(sts)
            Return al
        End Function

    End Class

    Public Class EnumDealerBranchTransKind

        Public Enum DealerTransKind
            SalesUnit
            ServiceUnit
            SparePartUnit
        End Enum

        Public Function RetrieveTransKind() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumDBTransKind
            sts = New EnumDBTransKind(0, "Sales Unit")
            al.Add(sts)
            sts = New EnumDBTransKind(1, "Service Unit")
            al.Add(sts)
            sts = New EnumDBTransKind(2, "Spare Part")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveTransKindFlag() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumDBTransKind
            sts = New EnumDBTransKind(0, "SalesUnitFlag")
            al.Add(sts)
            sts = New EnumDBTransKind(1, "ServiceFlag")
            al.Add(sts)
            sts = New EnumDBTransKind(2, "SparepartFlag")
            al.Add(sts)
            Return al
        End Function


    End Class

    Public Class EnumDealerBranch
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


    Public Class EnumDBTransKind
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
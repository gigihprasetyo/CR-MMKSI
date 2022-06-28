Public Class EnumStallMaster
    Public Enum LokasiStall
        Inside = 0
        Outside = 1
    End Enum

    Public Function LokasiStallList() As ArrayList
        Dim arrList As ArrayList = New ArrayList
        arrList.Add(New EnumStallMasterList(0, "Inside"))
        arrList.Add(New EnumStallMasterList(1, "Outside"))
        Return arrList
    End Function

    Public Enum TipeStall
        Booking = 0
        Walkin = 1
        MQP = 2
        Washing = 3
        RealTimeService = 4
    End Enum

    Public Function TipeStallList() As ArrayList
        Dim arrList As ArrayList = New ArrayList
        arrList.Add(New EnumStallMasterList(0, "Booking"))
        arrList.Add(New EnumStallMasterList(1, "Walkin"))
        arrList.Add(New EnumStallMasterList(2, "MQP"))
        arrList.Add(New EnumStallMasterList(3, "Washing"))
        arrList.Add(New EnumStallMasterList(4, "Real Time Service"))
        Return arrList
    End Function

    Public Enum KategoriStall
        Lift = 0
        WithoutLift = 1
        Washing = 2
    End Enum

    Public Function KategoriStallList() As ArrayList
        Dim arrList As ArrayList = New ArrayList
        arrList.Add(New EnumStallMasterList(0, "Lift"))
        arrList.Add(New EnumStallMasterList(1, "WithoutLift"))
        arrList.Add(New EnumStallMasterList(2, "Washing"))
        Return arrList
    End Function

    Public Enum StatusStall
        Aktif = 0
        NonAktif = 1
    End Enum

    Public Function StatusStallList() As ArrayList
        Dim arrList As ArrayList = New ArrayList
        arrList.Add(New EnumStallMasterList(0, "Aktif"))
        arrList.Add(New EnumStallMasterList(1, "NonAktif"))
        Return arrList
    End Function

    Public Enum IsBodyPaint
        Ya = 0
        Tidak = 1
    End Enum

    Public Function IsBodyPaintList() As ArrayList
        Dim arrList As ArrayList = New ArrayList
        arrList.Add(New EnumStallMasterList(0, "Ya"))
        arrList.Add(New EnumStallMasterList(1, "Tidak"))
        Return arrList
    End Function

    Public Enum VisitType
        BO = 0
        WI = 1
    End Enum

    Public Function VisitTypeList() As ArrayList
        Dim arrList As ArrayList = New ArrayList
        arrList.Add(New EnumStallMasterList(0, "BO"))
        arrList.Add(New EnumStallMasterList(1, "WI"))
        Return arrList
    End Function

    Public Enum StatusBooking
        Batal = 0
        Booked = 1
        Request = 2
    End Enum

    Public Function StatusBookingList() As ArrayList
        Dim arrList As ArrayList = New ArrayList
        arrList.Add(New EnumStallMasterList(0, "Batal"))
        arrList.Add(New EnumStallMasterList(1, "Booked"))
        arrList.Add(New EnumStallMasterList(2, "Request"))
        Return arrList
    End Function

    Public Enum ServiceType
        FS = 1
        PM = 2
        FF = 3
        OTH = 4
    End Enum

    Public Enum PickupType
        DiTinggal = 1
        DiTunggu = 2
    End Enum

    Public Function PickupTypeList() As ArrayList
        Dim arrList As ArrayList = New ArrayList
        arrList.Add(New EnumStallMasterList(1, "DiTinggal"))
        arrList.Add(New EnumStallMasterList(2, "DiTunggu"))
        Return arrList
    End Function

    Public Class EnumStallMasterList
        Private _id As Integer
        Private _val As String

        Public Sub New(ByVal id As Integer, ByVal val As String)
            _id = id
            _val = val
        End Sub
        Public ReadOnly Property ID() As Integer
            Get
                Return _id
            End Get
        End Property

        Public ReadOnly Property Value() As String
            Get
                Return _val
            End Get
        End Property

    End Class
End Class

Imports System.Web.UI.WebControls
Namespace KTB.DNet.Domain
    Public Class EnumParkingFeeStatus

        Public Enum ParkingFeeStatus
            Baru = 0
            Selesai = 1
            Proses = 2
        End Enum

        Public Function Baru() As String
            Return ParkingFeeStatus.Baru
        End Function

        Public Function Selesai() As String
            Return ParkingFeeStatus.Selesai
        End Function

        Public Function Proses() As String
            Return ParkingFeeStatus.Proses
        End Function

        Public Shared Function GetStringValue(ByVal ParkingStatus As Integer) As String
            Dim str As String = ""
            If ParkingStatus = EnumParkingFeeStatus.ParkingFeeStatus.Baru Then str = EnumParkingFeeStatus.ParkingFeeStatus.Baru.ToString
            If ParkingStatus = EnumParkingFeeStatus.ParkingFeeStatus.Selesai Then str = EnumParkingFeeStatus.ParkingFeeStatus.Selesai.ToString
            If ParkingStatus = EnumParkingFeeStatus.ParkingFeeStatus.Proses Then str = EnumParkingFeeStatus.ParkingFeeStatus.Proses.ToString
            Return str
        End Function

        Public Shared Function RetrieveParkingFeeStatus() As ArrayList
            Dim arl As ArrayList = New ArrayList
            arl.Add(New ListItem("Baru", 0))
            arl.Add(New ListItem("Selesai", 1))
            arl.Add(New ListItem("Proses", 2))
            Return arl
        End Function

    End Class

    Public Class EnumParkingFeeStatusProp
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


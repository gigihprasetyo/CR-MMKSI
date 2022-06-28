Namespace KTB.DNet.Domain

    Public Class EnumVehicleTypeStatus
        Public Enum VehicleTypeStatus
            Aktif = 0
            Tidak_Aktif = 1
        End Enum

        Public Function Aktif() As String
            Return VehicleTypeStatus.Aktif
        End Function

        Public Function Tidak_Aktif() As String
            Return VehicleTypeStatus.Tidak_Aktif
        End Function

        Public Function RetrieveVehicleTypeStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumVehicleTypeStatusProp
            sts = New EnumVehicleTypeStatusProp("A", "Aktif")
            al.Add(sts)
            sts = New EnumVehicleTypeStatusProp("X", "Tidak Aktif")
            al.Add(sts)
            Return al
        End Function
    End Class

    Public Class EnumVehicleTypeStatusProp
        Private _val As String
        Private _Name As String

        Public Sub New(ByVal val As String, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValStatus() As String
            Get
                Return _val
            End Get
            Set(ByVal Value As String)
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


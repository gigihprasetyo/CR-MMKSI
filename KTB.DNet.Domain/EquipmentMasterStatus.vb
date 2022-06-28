 


Namespace KTB.DNet.Domain
    Public Class EquipmentMasterStatus
        Public Enum EquipmentMasterStatusEnum
            Aktive
            NonAktive
        End Enum

        Public Shared Function RetrieveEquipmentMasterStatus() As ArrayList
            Dim al As New ArrayList
            Dim odr As EquipmentMasterStatusList
            odr = New EquipmentMasterStatusList(0, "Aktive")
            al.Add(odr)
            odr = New EquipmentMasterStatusList(1, "NonAktive")
            al.Add(odr)
            Return al
        End Function

    End Class

    Public Class EquipmentMasterStatusList
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




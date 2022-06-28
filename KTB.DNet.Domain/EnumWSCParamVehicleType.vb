Public Class EnumWSCParamVehicleType
    Public Enum WSCParamVehicleType
        StradaTriton = 0
        Pajero_PajeroSport = 1
        OutlanderSport = 2
        Lancer_Grandis_Maven = 3
        Mirage = 4
        Delica = 5
        XPander = 6
        T120ss = 7
        L300 = 8
    End Enum

    Public Function StradaTriton() As String
        Return WSCParamVehicleType.StradaTriton
    End Function

    Public Function Pajero_PajeroSport() As String
        Return WSCParamVehicleType.Pajero_PajeroSport
    End Function

    Public Function OutlanderSport() As String
        Return WSCParamVehicleType.OutlanderSport
    End Function

    Public Function Lancer_Grandis_Maven() As String
        Return WSCParamVehicleType.Lancer_Grandis_Maven
    End Function

    Public Function Mirage() As String
        Return WSCParamVehicleType.Mirage
    End Function

    Public Function Delica() As String
        Return WSCParamVehicleType.Delica
    End Function

    Public Function XPander() As String
        Return WSCParamVehicleType.XPander
    End Function

    Public Function T120ss() As String
        Return WSCParamVehicleType.T120ss
    End Function

    Public Function L300() As String
        Return WSCParamVehicleType.L300
    End Function

    Public Function RetrieveWSCParamVehicleType() As ArrayList
        Dim al As New ArrayList
        Dim fsType As WSCParamVehicleTypeProp
        fsType = New WSCParamVehicleTypeProp(0, "Triton".ToUpper)
        al.Add(fsType)
        fsType = New WSCParamVehicleTypeProp(1, "Pajero Sport".ToUpper)
        al.Add(fsType)
        fsType = New WSCParamVehicleTypeProp(2, "Outlander Sport".ToUpper)
        al.Add(fsType)
        fsType = New WSCParamVehicleTypeProp(3, "Lancer, Grandis, Maven".ToUpper)
        al.Add(fsType)
        fsType = New WSCParamVehicleTypeProp(4, "Mirage".ToUpper)
        al.Add(fsType)
        fsType = New WSCParamVehicleTypeProp(5, "Delica".ToUpper)
        al.Add(fsType)
        fsType = New WSCParamVehicleTypeProp(6, "XPander".ToUpper)
        al.Add(fsType)
        fsType = New WSCParamVehicleTypeProp(7, "T120ss".ToUpper)
        al.Add(fsType)
        fsType = New WSCParamVehicleTypeProp(8, "L300".ToUpper)
        al.Add(fsType)
        Return al
    End Function

    Public Function RetrieveWSCParamVehicleType(ByVal typeID As Short) As String
        Dim al As String = String.Empty
        Select Case typeID
            Case 0
                al = "Triton".ToUpper
            Case 1
                al = "Pajero Sport".ToUpper
            Case 2
                al = "Outlander Sport".ToUpper
            Case 3
                al = "Lancer, Grandis, Maven".ToUpper
            Case 4
                al = "Mirage".ToUpper
            Case 5
                al = "Delica".ToUpper
            Case 6
                al = "XPander".ToUpper
            Case 7
                al = "T120ss".ToUpper
            Case 8
                al = "L300".ToUpper
        End Select
        Return al
    End Function

    Public Class WSCParamVehicleTypeProp
        Private _val As String
        Private _Name As String

        Public Sub New(ByVal val As String, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property Val() As String
            Get
                Return _val
            End Get
            Set(ByVal Value As String)
                _val = Value
            End Set
        End Property

        Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class
End Class

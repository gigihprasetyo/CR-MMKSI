Namespace KTB.DNet.Domain
    Public Class EnumInformationType
        Public Enum InformationType
            'Incoming_Call = 1
            'Walk_In
            'Events
            Surat_Kabar = 3
            Televisi
            Majalah
            Radio
            Papan_Reklame
            Internet
            Mobile_Apps
            Social_Media
            Kebetulan_Melintas
            Database
        End Enum

        Public Shared Function GetStringInformationType(ByVal Type As Integer) As String
            Dim str As String = ""
            Select Case Type
                Case 3
                    str = "Surat Kabar"
                Case 4
                    str = "Televisi"
                Case 5
                    str = "Majalah"
                Case 6
                    str = "Radio"
                Case 7
                    str = "Papan Reklame"
                Case 8
                    str = "Internet"
                Case 9
                    str = "Mobile Apps"
                Case 10
                    str = "Social Media"
                Case 11
                    str = "Kebetulan Melintas"
                Case 12
                    str = "Database"
                    'Case 1
                    '    str = "Incoming Call"
                    'Case 2
                    '    str = "Walk In"
                    'Case 3
                    '    str = "Event"
            End Select
            Return str
        End Function

    End Class

    Public Class EnumInformationTypeOp
        Private _Val As Integer
        Private _Name As String

        Public Shared Function RetriveInformationType(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim EnumInformationTypeOp As EnumInformationTypeOp

            If (isIncludeBlank) Then
                EnumInformationTypeOp = New EnumInformationTypeOp(0, "Silahkan pilih")
                arr.Add(EnumInformationTypeOp)
            End If
            EnumInformationTypeOp = New EnumInformationTypeOp(3, "Surat Kabar")
            arr.Add(EnumInformationTypeOp)

            EnumInformationTypeOp = New EnumInformationTypeOp(4, "Televisi")
            arr.Add(EnumInformationTypeOp)

            EnumInformationTypeOp = New EnumInformationTypeOp(5, "Majalah")
            arr.Add(EnumInformationTypeOp)

            EnumInformationTypeOp = New EnumInformationTypeOp(6, "Radio")
            arr.Add(EnumInformationTypeOp)

            EnumInformationTypeOp = New EnumInformationTypeOp(7, "Papan Reklame")
            arr.Add(EnumInformationTypeOp)

            EnumInformationTypeOp = New EnumInformationTypeOp(8, "Internet")
            arr.Add(EnumInformationTypeOp)

            EnumInformationTypeOp = New EnumInformationTypeOp(9, "Mobile Apps")
            arr.Add(EnumInformationTypeOp)

            EnumInformationTypeOp = New EnumInformationTypeOp(10, "Social Media")
            arr.Add(EnumInformationTypeOp)

            EnumInformationTypeOp = New EnumInformationTypeOp(11, "Kebetulan Melintas")
            arr.Add(EnumInformationTypeOp)

            EnumInformationTypeOp = New EnumInformationTypeOp(12, "Database")
            arr.Add(EnumInformationTypeOp)


            'EnumInformationTypeOp = New EnumInformationTypeOp(1, "Incoming Call")
            'arr.Add(EnumInformationTypeOp)

            'EnumInformationTypeOp = New EnumInformationTypeOp(2, "Walk In")
            'arr.Add(EnumInformationTypeOp)

            'EnumInformationTypeOp = New EnumInformationTypeOp(3, "Event")
            'arr.Add(EnumInformationTypeOp)
            Return arr
        End Function

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _Val = val
            _Name = name
        End Sub

        Public Property ValStatus() As Integer
            Get
                Return _Val
            End Get
            Set(ByVal Value As Integer)
                _Val = Value
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


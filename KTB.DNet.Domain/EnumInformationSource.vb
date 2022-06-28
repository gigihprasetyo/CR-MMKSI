Namespace KTB.DNet.Domain
    Public Class EnumInformationSource
        Public Enum InformationSource
            'Surat_Kabar = 1
            'Televisi
            'Majalah
            'Radio
            Rekomendasi = 5
            Kunjungan = 6
            Pameran = 7
            'PapanReklame
            'Internet
            'Kebetulan_Melintas
            Microsite = 11
            'Web_Xpander
            'Mobile_Apps
            Social_Media = 13
            Call_Center = 14
            Database = 15
            Walk_in = 16
            Web_Campaign = 17
            Website = 18
            Web_Dealer = 19
        End Enum

        Public Shared Function GetStringInformationSource(ByVal source As Integer) As String
            Dim str As String = ""
            Select Case source
                'Case 1 
                '   str = "surat kabar"
                'Case 2
                '    str = "televisi"
                'Case 3
                '    str = "majalah"
                'Case 4
                '    str = "radio"
                Case 5
                    str = "Rekomendasi"
                Case 6
                    str = "Kunjungan Sales"
                Case 7
                    str = "Pameran/Event/Exhibition"
                    'Case 8
                    '    str = "papan reklame"
                    'Case 9
                    '    str = "internet"
                    'Case 10
                    '    str = "kebetulan melintas"
                Case 11
                    str = "Web xpander"
                    'Case 12
                    '    str = "mobile apps"
                Case 13
                    str = "Social media"
                Case 14
                    str = "Call Center"
                Case 15
                    str = "Database"
                Case 16
                    str = "Walk In"
                Case 17
                    str = "Web Campaign"
                Case 18
                    str = "Web Corporate"
                Case 19
                    str = "Web Dealer"

            End Select
            Return str
        End Function

        Public Shared Function GetIDInformationSource(ByVal source As String) As String
            Dim id As Integer = 0
            Select Case source.ToLower
                'Case "surat kabar"
                '    id = 1
                'Case "televisi"
                '    id = 2
                'Case "majalah"
                '    id = 3
                'Case "radio"
                '    id = 4
                Case "Rekomendasi"
                    id = 5
                Case "Kunjungan Sales"
                    id = 6
                Case "Pameran/Event/Exhibition"
                    id = 7
                    'Case "papan reklame"
                    '    id = 8
                    'Case "internet"
                    '    id = 9
                    'Case "kebetulan melintas"
                    '    id = 10
                Case "Web Xpander"
                    id = 11
                    'Case "mobile apps"
                    '    id = 12
                Case "Social Media"
                    id = 13
                Case "Call Center"
                    id = 14
                Case "Database"
                    id = 15
                Case "Walk In"
                    id = 16
                Case "Web Campaign"
                    id = 17
                Case "Web Corporate"
                    id = 18
                Case "Web Dealer"
                    id = 19
            End Select
            Return id
        End Function

    End Class

    Public Class EnumInformationSourceOp
        Private _Val As Integer
        Private _Name As String

        Public Shared Function RetriveInformationSource(ByVal isIncludeBlank As Boolean, Optional ByVal isEntry As Boolean = False) As ArrayList
            Dim arr As New ArrayList
            Dim EnumInformationSourceOp As EnumInformationSourceOp

            If (isIncludeBlank) Then
                EnumInformationSourceOp = New EnumInformationSourceOp(0, "Silahkan pilih")
                arr.Add(EnumInformationSourceOp)
            End If
            'EnumInformationSourceOp = New EnumInformationSourceOp(1, "Surat Kabar")
            'arr.Add(EnumInformationSourceOp)

            'EnumInformationSourceOp = New EnumInformationSourceOp(2, "Televisi")
            'arr.Add(EnumInformationSourceOp)

            'EnumInformationSourceOp = New EnumInformationSourceOp(3, "Majalah")
            'arr.Add(EnumInformationSourceOp)

            'EnumInformationSourceOp = New EnumInformationSourceOp(4, "Radio")
            'arr.Add(EnumInformationSourceOp)

            EnumInformationSourceOp = New EnumInformationSourceOp(5, "Rekomendasi")
            arr.Add(EnumInformationSourceOp)

            EnumInformationSourceOp = New EnumInformationSourceOp(6, "Kunjungan Sales")
            arr.Add(EnumInformationSourceOp)

            EnumInformationSourceOp = New EnumInformationSourceOp(7, "Pameran/Event/Exhibition")
            arr.Add(EnumInformationSourceOp)

            EnumInformationSourceOp = New EnumInformationSourceOp(16, "Walk in")
            arr.Add(EnumInformationSourceOp)

            EnumInformationSourceOp = New EnumInformationSourceOp(15, "Database")
            arr.Add(EnumInformationSourceOp)

            'EnumInformationSourceOp = New EnumInformationSourceOp(8, "Papan Reklame")
            'arr.Add(EnumInformationSourceOp)

            'EnumInformationSourceOp = New EnumInformationSourceOp(9, "Internet")
            'arr.Add(EnumInformationSourceOp)

            'EnumInformationSourceOp = New EnumInformationSourceOp(10, "Kebetulan melintas")
            'arr.Add(EnumInformationSourceOp)
            If isEntry = False Then
                EnumInformationSourceOp = New EnumInformationSourceOp(11, "Web Xpander")
                arr.Add(EnumInformationSourceOp)

                'EnumInformationSourceOp = New EnumInformationSourceOp(12, "Mobile Apps")
                'arr.Add(EnumInformationSourceOp)

                EnumInformationSourceOp = New EnumInformationSourceOp(13, "Social Media")
                arr.Add(EnumInformationSourceOp)

                EnumInformationSourceOp = New EnumInformationSourceOp(14, "Call Center")
                arr.Add(EnumInformationSourceOp)

                EnumInformationSourceOp = New EnumInformationSourceOp(17, "Web Campaign")
                arr.Add(EnumInformationSourceOp)

                EnumInformationSourceOp = New EnumInformationSourceOp(18, "Web Corporate")
                arr.Add(EnumInformationSourceOp)

                EnumInformationSourceOp = New EnumInformationSourceOp(19, "Web Dealer")
                arr.Add(EnumInformationSourceOp)

            End If

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


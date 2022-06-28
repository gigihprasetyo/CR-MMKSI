

Namespace KTB.DNet.Domain

    Public Class EnumPQR

        Public Enum AttachmentLocation
            Top
            Bottom
        End Enum

        Public Enum PQRStatus
            Baru = 0
            Validasi = 1
            Proses = 2
            Rilis = 3
            Selesai = 4
            Batal = 5

        End Enum


        'Public Shared Function PQRStatusDesc(ByVal iPQRStatus As String) As String
        '    If iPQRStatus = "" Then
        '        Return ""
        '    Else
        '        Return CType(iPQRStatus, PQRStatus).ToString()
        '    End If

        'End Function

        Public Function RetrievePQRStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumPQRStatus
            sts = New EnumPQRStatus(0, "Baru")
            al.Add(sts)
            sts = New EnumPQRStatus(1, "Validasi")
            al.Add(sts)
            sts = New EnumPQRStatus(2, "Proses")
            al.Add(sts)
            sts = New EnumPQRStatus(3, "Rilis")
            al.Add(sts)
            sts = New EnumPQRStatus(4, "Selesai")
            al.Add(sts)
            'If isKTB Then
            sts = New EnumPQRStatus(5, "Batal")
            al.Add(sts)
            'End If
            Return al
        End Function
    End Class

    Public Class EnumPQRStatus
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public ReadOnly Property ValStatus() As Integer
            Get
                Return _val
            End Get
        End Property

        Public ReadOnly Property NameStatus() As String
            Get
                Return _Name
            End Get
        End Property

    End Class

End Namespace

Namespace KTB.DNet.Domain
    Public Class EnumAgeSegment
        Public Enum AgeSegment
            usia_s_d_29_tahun = 1
            usia_30_39_tahun
            usia_40_49_tahun
            usia_50_tahun_keatas
        End Enum

        Public Shared Function GetStringAgeSegment(ByVal segmentId As Integer) As String
            Dim str As String = ""
            Select Case segmentId
                Case 1
                    str = "s/d 29 tahun"
                Case 2
                    str = "30-39 tahun"
                Case 3
                    str = "40-49 tahun"
                Case 4
                    str = "50 tahun keatas"
            End Select
            Return str
        End Function

    End Class

    Public Class EnumAgeSegmentOp
        Private _Val As Integer
        Private _Name As String

        Public Shared Function RetriveAgeSegment(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim EnumAgeSegmentOp As EnumAgeSegmentOp

            If (isIncludeBlank) Then
                EnumAgeSegmentOp = New EnumAgeSegmentOp(0, "Silahkan pilih")
                arr.Add(EnumAgeSegmentOp)
            End If
            EnumAgeSegmentOp = New EnumAgeSegmentOp(1, "s/d 29 tahun")
            arr.Add(EnumAgeSegmentOp)

            EnumAgeSegmentOp = New EnumAgeSegmentOp(2, "30 - 39 tahun")
            arr.Add(EnumAgeSegmentOp)

            EnumAgeSegmentOp = New EnumAgeSegmentOp(3, "40 - 49 tahun")
            arr.Add(EnumAgeSegmentOp)

            EnumAgeSegmentOp = New EnumAgeSegmentOp(4, "50 tahun keatas")
            arr.Add(EnumAgeSegmentOp)

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


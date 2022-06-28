Namespace KTB.DNet.Domain

    Public Class EnumStatusSPL
        Public Enum StatusSPL
            Aktif
            Tidak_Aktif
        End Enum

        Public Shared Function RetrieveStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As enumStatSPL

            sts = New enumStatSPL(0, "Aktif")
            al.Add(sts)
            sts = New enumStatSPL(1, "Tidak Aktif")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function RetrieveStatus(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim al As New ArrayList
            Dim sts As enumStatSPL

            If (isIncludeBlank) Then
                sts = New enumStatSPL(-1, "-Silahkan Pilih-")
                al.Add(sts)
            End If

            sts = New enumStatSPL(0, "Aktif")
            al.Add(sts)
            sts = New enumStatSPL(1, "Tidak Aktif")
            al.Add(sts)
            Return al
        End Function

    End Class

    Public Class enumStatSPL
        Private _Val As Integer
        Private _Name As String

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


Namespace KTB.DNet.Domain
    Public Class EnumLKPPClassification
        Public Enum LKPPClassification
            E_Catalog = 0
            PengadaanLangsung = 1
            PengadaanDesa = 2
            HibahPemerintah = 3
            HibahNonPemerintah = 4
            Lelang = 5
        End Enum

        Public Shared Function RetrieveClassification() As ArrayList
            Dim al As New ArrayList
            Dim sts As enumLKPPClass

            sts = New enumLKPPClass(0, "E-Catalog")
            al.Add(sts)
            sts = New enumLKPPClass(5, "Lelang")
            al.Add(sts)

            sts = New enumLKPPClass(1, "Pengadaan Langsung")
            al.Add(sts)
            sts = New enumLKPPClass(2, "Pengadaan Desa")
            al.Add(sts)
            sts = New enumLKPPClass(3, "Hibah dari Pemerintah")
            al.Add(sts)
            sts = New enumLKPPClass(4, "Hibah Non Pemerintah")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function RetrieveClassification(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim al As New ArrayList
            Dim sts As enumLKPPClass

            If (isIncludeBlank) Then
                sts = New enumLKPPClass(-1, "-Silahkan Pilih-")
                al.Add(sts)
            End If
            sts = New enumLKPPClass(0, "E-Catalog")
            al.Add(sts)
            sts = New enumLKPPClass(5, "Lelang")
            al.Add(sts)

            sts = New enumLKPPClass(1, "Pengadaan Langsung")
            al.Add(sts)
            

            sts = New enumLKPPClass(2, "Pengadaan Desa")
            al.Add(sts)
            sts = New enumLKPPClass(3, "Hibah dari Pemerintah")
            al.Add(sts)
            sts = New enumLKPPClass(4, "Hibah Non Pemerintah")
            al.Add(sts)
            Return al
        End Function

    End Class

    Public Class enumLKPPClass
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

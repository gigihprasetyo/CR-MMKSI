Imports System.Web.UI.WebControls

Public Class EnumWSCParamParameter
#Region "Parameter"
    Public Enum WSCDetailParameter
        NomorBuletin = 0
        TglPKT = 1
        TglKerusakan = 2
        TglPerbaikan = 3
        JarakTempuh = 4
        KodePosisi = 5
        JarakTglPKTKeService = 6
        JarakServiceKeInputClaim = 7
        JarakKerusakanKePerbaikan = 8
        KodeKerja = 9
        KodeKerusakanA = 10
        KodeKerusakanB = 11
        KodeKerusakanC = 12
        NomorRangka = 13
        ChassisSpecialProject = 14
        Part = 15
        Amount = 16
        Jumlah = 17
        TanggalPendaftaran = 18
        TipeMSP = 19
        JarakTanggalKerusakanKeService = 20

    End Enum

    Public Function NomorBuletin() As String
        Return WSCDetailParameter.NomorBuletin
    End Function

    Public Function TglPKT() As String
        Return WSCDetailParameter.TglPKT
    End Function

    Public Function TglKerusakan() As String
        Return WSCDetailParameter.TglKerusakan
    End Function

    Public Function TglPerbaikan() As String
        Return WSCDetailParameter.TglPerbaikan
    End Function

    Public Function JarakTempuh() As String
        Return WSCDetailParameter.JarakTempuh
    End Function

    Public Function KodePosisi() As String
        Return WSCDetailParameter.KodePosisi
    End Function

    Public Function JarakTglPKTKeService() As String
        Return WSCDetailParameter.JarakTglPKTKeService
    End Function

    Public Function JarakServiceKeInputClaim() As String
        Return WSCDetailParameter.JarakServiceKeInputClaim
    End Function

    Public Function JarakKerusakanKePerbaikan() As String
        Return WSCDetailParameter.JarakKerusakanKePerbaikan
    End Function

    Public Function KodeKerja() As String
        Return WSCDetailParameter.KodeKerja
    End Function

    Public Function KodeKerusakanA() As String
        Return WSCDetailParameter.KodeKerusakanA
    End Function

    Public Function KodeKerusakanB() As String
        Return WSCDetailParameter.KodeKerusakanB
    End Function

    Public Function KodeKerusakanC() As String
        Return WSCDetailParameter.KodeKerusakanC
    End Function

    Public Function NomorRangka() As String
        Return WSCDetailParameter.NomorRangka
    End Function
    Public Function ChassisSpecialProject() As String
        Return WSCDetailParameter.ChassisSpecialProject
    End Function

    Private Shared _arrayListWSCParameter As ArrayList
    Public Shared ReadOnly Property RetrieveWSCParameterLI()
        Get
            If (_arrayListWSCParameter Is Nothing) Then
                _arrayListWSCParameter = New ArrayList
                Dim listItemStatus0 As New ListItem("Silahkan Pilih".ToUpper, -1)
                Dim listItemStatus1 As New ListItem("Nomor Buletin".ToUpper, 0)
                Dim listItemStatus2 As New ListItem("Tanggal PKT".ToUpper, 1)
                Dim listItemStatus3 As New ListItem("Tanggal Kerusakan".ToUpper, 2)
                Dim listItemStatus4 As New ListItem("Tanggal Perbaikan".ToUpper, 3)
                Dim listItemStatus5 As New ListItem("Jarak Tempuh".ToUpper, 4)
                Dim listItemStatus6 As New ListItem("Kode Posisi".ToUpper, 5)
                Dim listItemStatus7 As New ListItem("Jarak Tanggal PKT ke Service".ToUpper, 6)
                Dim listItemStatus8 As New ListItem("Jarak Tanggal Service ke Input Claim".ToUpper, 7)
                Dim listItemStatus9 As New ListItem("Jarak Kerusakan ke Perbaikan".ToUpper, 8)
                Dim listItemStatus10 As New ListItem("Kode Kerja".ToUpper, 9)
                Dim listItemStatus11 As New ListItem("Kode Kerusakan A".ToUpper, 10)
                Dim listItemStatus12 As New ListItem("Kode Kerusakan B".ToUpper, 11)
                Dim listItemStatus13 As New ListItem("Kode Kerusakan C".ToUpper, 12)
                Dim listItemStatus14 As New ListItem("Nomor Rangka".ToUpper, 13)
                Dim listItemStatus15 As New ListItem("Chassis Special Project".ToUpper, 14)
                Dim listItemStatus16 As New ListItem("Part".ToUpper, 15)
                Dim listItemStatus17 As New ListItem("Amount".ToUpper, 16)
                Dim listItemStatus18 As New ListItem("Jumlah".ToUpper, 17)
                Dim listItemStatus19 As New ListItem("Tanggal Pendaftaran".ToUpper, 18)
                Dim listItemStatus20 As New ListItem("Tipe MSP".ToUpper, 19)
                Dim listItemStatus21 As New ListItem("Jarak Tanggal Kerusakan ke Service".ToUpper, 20)

                With _arrayListWSCParameter
                    .Add(listItemStatus0)
                    .Add(listItemStatus1)
                    .Add(listItemStatus2)
                    .Add(listItemStatus3)
                    .Add(listItemStatus4)
                    .Add(listItemStatus5)
                    .Add(listItemStatus6)
                    .Add(listItemStatus7)
                    .Add(listItemStatus8)
                    .Add(listItemStatus9)
                    .Add(listItemStatus10)
                    .Add(listItemStatus11)
                    .Add(listItemStatus12)
                    .Add(listItemStatus13)
                    'KTB
                    '.Add(listItemStatus14)
                    '.Add(listItemStatus15)
                    .Add(listItemStatus16)
                    .Add(listItemStatus17)
                    .Add(listItemStatus18)
                    .Add(listItemStatus19)
                    .Add(listItemStatus20)
                    .Add(listItemStatus21)
                End With

            End If
            Return _arrayListWSCParameter
        End Get
    End Property

    Public Function RetrieveWSCParameter() As ArrayList
        Dim al As New ArrayList
        Dim fsType As WSCParameterProp
        fsType = New WSCParameterProp(0, "Nomor Buletin".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(1, "Tanggal PKT".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(2, "Tanggal Kerusakan".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(3, "Tanggal Perbaikan".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(4, "Jarak Tempuh".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(5, "Kode Posisi".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(6, "Jarak Tanggal PKT ke Service".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(7, "Jarak Tanggal Service ke Input Claim".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(8, "Jarak Kerusakan ke Perbaikan".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(9, "Kode Kerja".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(10, "Kode Kerusakan A".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(11, "Kode Kerusakan B".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(12, "Kode Kerusakan C".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(13, "Nomor Rangka".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(14, "Chassis Special Project".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(15, "Part".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(16, "Amount".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(17, "Jumlah".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(18, "Tanggal Pendaftaran".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(19, "Tipe MSP".ToUpper)
        al.Add(fsType)
        fsType = New WSCParameterProp(20, "Jarak Tanggal Kerusakan ke Service".ToUpper)
        al.Add(fsType)
        Return al
    End Function

    Public Function RetrieveWSCParameter(ByVal typeID As Short) As String
        Dim al As String = String.Empty
        Select Case typeID
            Case 0
                al = "Nomor Buletin".ToUpper
            Case 1
                al = "Tanggal PKT".ToUpper
            Case 2
                al = "Tanggal Kerusakan".ToUpper
            Case 3
                al = "Tanggal Perbaikan".ToUpper
            Case 4
                al = "Jarak Tempuh".ToUpper
            Case 5
                al = "Kode Posisi".ToUpper
            Case 6
                al = "Jarak Tanggal PKT ke Service".ToUpper
            Case 7
                al = "Jarak Tanggal Service ke Input Claim".ToUpper
            Case 8
                al = "Jarak Kerusakan ke Perbaikan".ToUpper
            Case 9
                al = "Kode Kerja".ToUpper
            Case 10
                al = "Kode Kerusakan A".ToUpper
            Case 11
                al = "Kode Kerusakan B".ToUpper
            Case 12
                al = "Kode Kerusakan C".ToUpper
            Case 13
                al = "Nomor Rangka".ToUpper
            Case 14
                al = "Chassis Special Project".ToUpper
            Case 15
                al = "Part".ToUpper
            Case 16
                al = "Amount".ToUpper
            Case 17
                al = "Jumlah".ToUpper
            Case 18
                al = "Tanggal Pendaftaran".ToUpper
            Case 19
                al = "Tipe MSP".ToUpper
            Case 20
                al = "Jarak Tanggal Kerusakan ke Service".ToUpper
        End Select
        Return al
    End Function

    Public Class WSCParameterProp
        Private _val As String
        Private _Name As String

        Public Sub New(ByVal val As String, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property Value() As String
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
#End Region

#Region "Operator"
    Public Enum WSCDetailOperator
        SamaDengan = 0
        TidakSamaDengan = 1
        SebagianSamaDengan = 2
        DiawaliDengan = 3
        DiakhiriDengan = 4
        LebihBesarDari = 5
        LebihKecilDari = 6
        LebihBesarAtauSamaDengan = 7
        LebihKecilAtauSamaDengan = 8
        TerdiriDari = 9
        TidakTerdiriDari = 10
    End Enum

    Public Function SamaDengan() As String
        Return WSCDetailOperator.SamaDengan
    End Function

    Public Function TidakSamaDengan() As String
        Return WSCDetailOperator.TidakSamaDengan
    End Function

    Public Function SebagianSamaDengan() As String
        Return WSCDetailOperator.SebagianSamaDengan
    End Function

    Public Function DiawaliDengan() As String
        Return WSCDetailOperator.DiawaliDengan
    End Function

    Public Function DiakhiriDengan() As String
        Return WSCDetailOperator.DiakhiriDengan
    End Function

    Public Function LebihBesarDari() As String
        Return WSCDetailOperator.LebihBesarDari
    End Function

    Public Function LebihKecilDari() As String
        Return WSCDetailOperator.LebihKecilDari
    End Function

    Public Function LebihBesarAtauSamaDengan() As String
        Return WSCDetailOperator.LebihBesarAtauSamaDengan
    End Function

    Public Function LebihKecilAtauSamaDengan() As String
        Return WSCDetailOperator.LebihKecilAtauSamaDengan
    End Function

    Public Function TerdiriDari() As String
        Return WSCDetailOperator.TerdiriDari
    End Function

    Public Function TidakTerdiriDari() As String
        Return WSCDetailOperator.TidakTerdiriDari
    End Function

    Private Shared _arrayListWSCOperator As ArrayList
    Public Shared ReadOnly Property RetrieveWSCOperatorLI()
        Get
            If (_arrayListWSCOperator Is Nothing) Then
                _arrayListWSCOperator = New ArrayList
                Dim listItemStatus0 As New ListItem("Silahkan Pilih".ToUpper, -1)
                Dim listItemStatus1 As New ListItem("sama dengan".ToUpper, 0)
                Dim listItemStatus2 As New ListItem("tidak sama dengan".ToUpper, 1)
                Dim listItemStatus3 As New ListItem("sebagian sama dengan".ToUpper, 2)
                Dim listItemStatus4 As New ListItem("diawali dengan".ToUpper, 3)
                Dim listItemStatus5 As New ListItem("diakhiri dengan".ToUpper, 4)
                Dim listItemStatus6 As New ListItem("Lebih besar dari".ToUpper, 5)
                Dim listItemStatus7 As New ListItem("Lebih kecil dari".ToUpper, 6)
                Dim listItemStatus8 As New ListItem("Lebih besar / sama dengan".ToUpper, 7)
                Dim listItemStatus9 As New ListItem("Lebih kecil / sama dengan".ToUpper, 8)
                Dim listItemStatus10 As New ListItem("Terdiri dari".ToUpper, 9)
                Dim listItemStatus11 As New ListItem("Tidak terdiri dari".ToUpper, 10)

                With _arrayListWSCOperator
                    .Add(listItemStatus0)
                    .Add(listItemStatus1)
                    .Add(listItemStatus2)
                    .Add(listItemStatus3)
                    .Add(listItemStatus4)
                    .Add(listItemStatus5)
                    .Add(listItemStatus6)
                    .Add(listItemStatus7)
                    .Add(listItemStatus8)
                    .Add(listItemStatus9)
                    .Add(listItemStatus10)
                    .Add(listItemStatus11)
                End With

            End If
            Return _arrayListWSCOperator
        End Get
    End Property

    Public Shared ReadOnly Property RetrieveWSCOperatorLI(ByVal _Param As EnumWSCParamParameter.WSCDetailParameter)
        Get
            If (_arrayListWSCOperator Is Nothing) Then
                _arrayListWSCOperator = New ArrayList
                Dim listItemStatus0 As New ListItem("Silahkan Pilih".ToUpper, -1)
                Dim listItemStatus1 As New ListItem("sama dengan".ToUpper, 0)
                Dim listItemStatus2 As New ListItem("tidak sama dengan".ToUpper, 1)
                Dim listItemStatus3 As New ListItem("sebagian sama dengan".ToUpper, 2)
                Dim listItemStatus4 As New ListItem("diawali dengan".ToUpper, 3)
                Dim listItemStatus5 As New ListItem("diakhiri dengan".ToUpper, 4)
                Dim listItemStatus6 As New ListItem("Lebih besar dari".ToUpper, 5)
                Dim listItemStatus7 As New ListItem("Lebih kecil dari".ToUpper, 6)
                Dim listItemStatus8 As New ListItem("Lebih besar / sama dengan".ToUpper, 7)
                Dim listItemStatus9 As New ListItem("Lebih kecil / sama dengan".ToUpper, 8)
                Dim listItemStatus10 As New ListItem("Terdiri dari".ToUpper, 9)
                Dim listItemStatus11 As New ListItem("Tidak terdiri dari".ToUpper, 10)

                With _arrayListWSCOperator
                    If _Param = WSCDetailParameter.NomorBuletin _
                        OrElse _Param = WSCDetailParameter.KodePosisi _
                        OrElse _Param = WSCDetailParameter.KodeKerja _
                        OrElse _Param = WSCDetailParameter.KodeKerusakanA _
                        OrElse _Param = WSCDetailParameter.KodeKerusakanB _
                        OrElse _Param = WSCDetailParameter.KodeKerusakanC Then
                        .Add(listItemStatus0)
                        .Add(listItemStatus1)
                        .Add(listItemStatus2)
                        .Add(listItemStatus3)
                        .Add(listItemStatus4)
                        .Add(listItemStatus5)
                        .Add(listItemStatus10)
                        .Add(listItemStatus11)
                    ElseIf _Param = WSCDetailParameter.TglPKT _
                        OrElse _Param = WSCDetailParameter.TglKerusakan _
                        OrElse _Param = WSCDetailParameter.TglPerbaikan _
                        OrElse _Param = WSCDetailParameter.JarakTempuh _
                        OrElse _Param = WSCDetailParameter.JarakTglPKTKeService _
                        OrElse _Param = WSCDetailParameter.JarakServiceKeInputClaim _
                        OrElse _Param = WSCDetailParameter.JarakKerusakanKePerbaikan Then
                        .Add(listItemStatus0)
                        .Add(listItemStatus1)
                        .Add(listItemStatus2)
                        .Add(listItemStatus6)
                        .Add(listItemStatus7)
                        .Add(listItemStatus8)
                        .Add(listItemStatus9)
                    Else
                        .Add(listItemStatus0)
                        .Add(listItemStatus1)
                        .Add(listItemStatus2)
                        .Add(listItemStatus3)
                        .Add(listItemStatus4)
                        .Add(listItemStatus5)
                        .Add(listItemStatus6)
                        .Add(listItemStatus7)
                        .Add(listItemStatus8)
                        .Add(listItemStatus9)
                        .Add(listItemStatus10)
                        .Add(listItemStatus11)
                    End If
                End With

            End If
            Return _arrayListWSCOperator
        End Get
    End Property

    Public Function RetrieveWSCParamOperator() As ArrayList
        Dim al As New ArrayList
        Dim fsType As WSCOperatorProp
        fsType = New WSCOperatorProp(0, "sama dengan".ToUpper)
        al.Add(fsType)
        fsType = New WSCOperatorProp(1, "tidak sama dengan".ToUpper)
        al.Add(fsType)
        fsType = New WSCOperatorProp(2, "sebagian sama dengan".ToUpper)
        al.Add(fsType)
        fsType = New WSCOperatorProp(3, "diawali dengan".ToUpper)
        al.Add(fsType)
        fsType = New WSCOperatorProp(4, "diakhiri dengan".ToUpper)
        al.Add(fsType)
        fsType = New WSCOperatorProp(5, "Lebih besar dari".ToUpper)
        al.Add(fsType)
        fsType = New WSCOperatorProp(6, "Lebih kecil dari".ToUpper)
        al.Add(fsType)
        fsType = New WSCOperatorProp(7, "Lebih besar / sama dengan".ToUpper)
        al.Add(fsType)
        fsType = New WSCOperatorProp(8, "Lebih kecil / sama dengan".ToUpper)
        al.Add(fsType)
        fsType = New WSCOperatorProp(9, "Terdiri dari".ToUpper)
        al.Add(fsType)
        fsType = New WSCOperatorProp(10, "Tidak terdiri dari".ToUpper)
        al.Add(fsType)
        Return al
    End Function

    Public Function RetrieveWSCParamOperator(ByVal typeID As Short) As String
        Dim al As String = String.Empty
        Select Case typeID
            Case 0
                al = "sama dengan".ToUpper
            Case 1
                al = "tidak sama dengan".ToUpper
            Case 2
                al = "sebagian sama dengan".ToUpper
            Case 3
                al = "diawali dengan".ToUpper
            Case 4
                al = "diakhiri dengan".ToUpper
            Case 5
                al = "Lebih besar dari".ToUpper
            Case 6
                al = "Lebih kecil dari".ToUpper
            Case 7
                al = "Lebih besar / sama dengan".ToUpper
            Case 8
                al = "Lebih kecil / sama dengan".ToUpper
            Case 9
                al = "Terdiri dari".ToUpper
            Case 10
                al = "Tidak terdiri dari".ToUpper
        End Select
        Return al
    End Function

    Public Class WSCOperatorProp
        Private _val As String
        Private _Name As String

        Public Sub New(ByVal val As String, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property Value() As String
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
#End Region
End Class

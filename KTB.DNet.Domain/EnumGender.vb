Namespace KTB.DNet.Domain
    Public Class EnumGender
        Public Enum Gender
            Pria = 1
            Wanita = 2
        End Enum

        Public Shared Function GetStringGender(ByVal gender As Integer) As String
            Dim str As String = ""
            If gender = EnumGender.Gender.Pria Then str = EnumGender.Gender.Pria.ToString
            If gender = EnumGender.Gender.Wanita Then str = EnumGender.Gender.Wanita.ToString
            Return str
        End Function

        Public Function Pria() As String
            Return EnumGender.Gender.Pria
        End Function

        Public Function Wanita() As String
            Return EnumGender.Gender.Wanita
        End Function

    End Class

    Public Class EnumGenderOp
        Private _Val As Integer
        Private _Name As String

        Public Shared Function RetriveSalesGender(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim EmGenderOp As EnumGenderOp

            If (isIncludeBlank) Then
                EmGenderOp = New EnumGenderOp(0, "Silahkan Pilih")
                arr.Add(EmGenderOp)
            End If
            EmGenderOp = New EnumGenderOp(1, "Pria")
            arr.Add(EmGenderOp)

            EmGenderOp = New EnumGenderOp(2, "Wanita")
            arr.Add(EmGenderOp)

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


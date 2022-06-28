Namespace KTB.DNet.Domain
    Public Class EnumReligion
        Public Enum Religion
            Islam
            Kristen
            Katholik
            Hindu
            Budha
        End Enum
    End Class

    Public Class EnumReligionValue
        Private _Val As Integer
        Private _Name As String

        Public Shared Function RetriveReligion(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim emReligion As EnumReligionValue

            If (isIncludeBlank) Then
                emReligion = New EnumReligionValue(99, "")
                arr.Add(emReligion)
            End If
            emReligion = New EnumReligionValue(0, "Islam")
            arr.Add(emReligion)

            emReligion = New EnumReligionValue(1, "Kristen")
            arr.Add(emReligion)

            emReligion = New EnumReligionValue(2, "Katholik")
            arr.Add(emReligion)

            emReligion = New EnumReligionValue(3, "Hindu")
            arr.Add(emReligion)

            emReligion = New EnumReligionValue(4, "Budha")
            arr.Add(emReligion)
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



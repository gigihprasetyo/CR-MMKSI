Namespace KTB.DNet.Domain
    Public Class EnumSalesmanUniformAssigned
        Public Enum IsReleased
            Not_Released = 0
            Released
        End Enum

        Public Enum IsValidate
            Not_Validate = 0
            Validate
        End Enum
    End Class

    Public Class EnumValue
        Private _Val As Integer
        Private _Name As String

        Public Shared Function RetriveSalesmanUniformAssignedIsReleased(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim emIsReleased As EnumValue
            If (isIncludeBlank) Then
                emIsReleased = New EnumValue(99, "")
                arr.Add(emIsReleased)
            End If
            emIsReleased = New EnumValue(0, EnumSalesmanUniformAssigned.IsReleased.Not_Released.ToString.Replace("_", " "))
            arr.Add(emIsReleased)

            emIsReleased = New EnumValue(1, EnumSalesmanUniformAssigned.IsReleased.Released.ToString.Replace("_", " "))
            arr.Add(emIsReleased)

            Return arr
        End Function

        Public Shared Function RetriveSalesmanUniformAssignedIsValidate(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim emIsValidate As EnumValue
            If (isIncludeBlank) Then
                emIsValidate = New EnumValue(99, "")
                arr.Add(emIsValidate)
            End If
            emIsValidate = New EnumValue(0, EnumSalesmanUniformAssigned.IsValidate.Not_Validate.ToString.Replace("_", " "))
            arr.Add(emIsValidate)

            emIsValidate = New EnumValue(1, EnumSalesmanUniformAssigned.IsValidate.Validate.ToString.Replace("_", " "))
            arr.Add(emIsValidate)

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
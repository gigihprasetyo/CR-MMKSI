Namespace KTB.DNet.Domain
    Public Class EnumSalesmanPartLevel
        Public Enum Level
            Junior
            Senior
            Top
        End Enum

        Public Shared Function RetriveSalesmanPartLevel(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim enmVal As EnumSalesmanPartLevelValue

            If (isIncludeBlank) Then
                enmVal = New EnumSalesmanPartLevelValue(99, "Silahkan Pilih")
                arr.Add(enmVal)
            End If
            enmVal = New EnumSalesmanPartLevelValue(0, "Junior")
            arr.Add(enmVal)

            enmVal = New EnumSalesmanPartLevelValue(1, "Senior")
            arr.Add(enmVal)

            enmVal = New EnumSalesmanPartLevelValue(2, "Top")
            arr.Add(enmVal)
            Return arr
        End Function

        Public Shared Function RetrieveName(ByVal EnumIndex As Integer) As String
            Select Case EnumIndex
                Case 0
                    Return "Junior"
                Case 1
                    Return "Senior"
                Case 2
                    Return "Top"
                Case 99
                    Return ""
            End Select

        End Function

    End Class

    Public Class EnumSalesmanPartLevelValue
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



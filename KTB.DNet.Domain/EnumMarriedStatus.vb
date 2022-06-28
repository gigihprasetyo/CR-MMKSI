Namespace KTB.DNet.Domain
    Public Class EnumSalesmanMarriedStatus
        Public Enum MarriedStatus
            Menikah = 1
            Belum_Menikah
            Janda
            Duda
        End Enum
    End Class

    Public Class EnumSalesMarriedStatus
        Private _Val As Integer
        Private _Name As String

        Public Shared Function RetriveMarriedStatus(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim emMarriedStatus As EnumSalesMarriedStatus

            If (isIncludeBlank) Then
                emMarriedStatus = New EnumSalesMarriedStatus(99, "")
                arr.Add(emMarriedStatus)
            End If
            emMarriedStatus = New EnumSalesMarriedStatus(1, "Menikah")
            arr.Add(emMarriedStatus)

            emMarriedStatus = New EnumSalesMarriedStatus(2, "Belum Menikah")
            arr.Add(emMarriedStatus)

            emMarriedStatus = New EnumSalesMarriedStatus(3, "Janda")
            arr.Add(emMarriedStatus)

            emMarriedStatus = New EnumSalesMarriedStatus(4, "Duda")
            arr.Add(emMarriedStatus)
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



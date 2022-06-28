Namespace KTB.DNet.Domain
    Public Class EnumSalesmanIsRequest
        Public Enum SalesmanIsRequest
            Belum_Request = 0
            Sudah_Request
        End Enum
    End Class

    Public Class EnumSalesIsRequest
        Private _Val As Integer
        Private _Name As String

        Public Shared Function RetriveSalesmanIsRequest(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim emSalesIsRequest As EnumSalesIsRequest

            If (isIncludeBlank) Then
                emSalesIsRequest = New EnumSalesIsRequest(99, "")
                arr.Add(emSalesIsRequest)
            End If
            emSalesIsRequest = New EnumSalesIsRequest(0, "Belum Request")
            arr.Add(emSalesIsRequest)

            emSalesIsRequest = New EnumSalesIsRequest(1, "Sudah Request")
            arr.Add(emSalesIsRequest)
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

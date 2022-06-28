Namespace KTB.DNet.Domain
    Public Class EnumSalesmanRegisterStatus
        Public Enum SalesmanRegisterStatus
            Belum_Register = 0
            Sudah_Register
        End Enum
    End Class

    Public Class EnumSalesRegisterStatus
        Private _Val As Integer
        Private _Name As String

        Public Shared Function RetriveSalesmanRegisterStatus(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim emSalesRegisterStatus As EnumSalesRegisterStatus

            If (isIncludeBlank) Then
                emSalesRegisterStatus = New EnumSalesRegisterStatus(99, "Silahkan Pilih")
                arr.Add(emSalesRegisterStatus)
            End If
            emSalesRegisterStatus = New EnumSalesRegisterStatus(0, "Belum Register")
            arr.Add(emSalesRegisterStatus)

            emSalesRegisterStatus = New EnumSalesRegisterStatus(1, "Sudah Register")
            arr.Add(emSalesRegisterStatus)

            emSalesRegisterStatus = New EnumSalesRegisterStatus(4, "Konfirmasi")
            arr.Add(emSalesRegisterStatus)
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

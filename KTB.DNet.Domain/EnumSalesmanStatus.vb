Namespace KTB.DNet.Domain
    Public Class EnumSalesmanStatus
        Public Enum SalesmanStatus
            Baru = 1
            Aktif
            Tidak_Aktif
            Konfirmasi      ' untuk salesman yg sdh resign & akan direload kembali
        End Enum

        Public Function RetriveSalesmanStatusEnum() As ArrayList
            Dim arrTmps As New ArrayList
            arrTmps = RetriveSalesmanStatus(False)
            Return arrTmps
        End Function

        Public Function RetriveSalesmanStatus(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim emSalesStatus As EnumSalesStatus

            If (isIncludeBlank) Then
                emSalesStatus = New EnumSalesStatus(99, "Silahkan Pilih")
                arr.Add(emSalesStatus)
            End If
            emSalesStatus = New EnumSalesStatus(1, "Baru")
            arr.Add(emSalesStatus)

            emSalesStatus = New EnumSalesStatus(2, "Aktif")
            arr.Add(emSalesStatus)

            emSalesStatus = New EnumSalesStatus(3, "Tidak Aktif")
            arr.Add(emSalesStatus)

            emSalesStatus = New EnumSalesStatus(4, "Konfirmasi")
            arr.Add(emSalesStatus)

            Return arr
        End Function

        Public Function RetriveSalesmanStatusAlert() As ArrayList
            Dim arr As New ArrayList
            Dim emSalesStatus As EnumSalesStatus
            emSalesStatus = New EnumSalesStatus(1, "Baru")
            arr.Add(emSalesStatus)

            emSalesStatus = New EnumSalesStatus(2, "Aktif")
            arr.Add(emSalesStatus)

            emSalesStatus = New EnumSalesStatus(3, "Tidak Aktif")
            arr.Add(emSalesStatus)

            emSalesStatus = New EnumSalesStatus(4, "Konfirmasi")
            arr.Add(emSalesStatus)

            Return arr
        End Function

    End Class

    Public Class EnumSalesStatus
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


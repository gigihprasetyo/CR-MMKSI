Namespace KTB.DNet.Domain
    Public Class EnumSAPCustomerResponse
        Public Enum SAPCustomerResponse
            Dealer_Visit = 1
            Test_Drive
            Negotiation
            SPK
            Sudah_Teralokasi
            Sudah_PKT
        End Enum

        Public Shared Function GetStringValue(ByVal source As Integer) As String
            Dim str As String = ""
            Select Case source
                Case 1
                    str = "Dealer Visit"
                Case 2
                    str = "Test Drive"
                Case 3
                    str = "Negotiation"
                Case 4
                    str = "SPK"
                Case 5
                    str = "Sudah Teralokasi"
                Case 6
                    str = "Sudah PKT"
            End Select
            Return str
        End Function

    End Class

    Public Class EnumSAPCustomerResponseOp
        Private _Val As Integer
        Private _Name As String

        Public Shared Function RetriveResponse(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim EnumSAPCustomerResponseOp As EnumSAPCustomerResponseOp

            If (isIncludeBlank) Then
                EnumSAPCustomerResponseOp = New EnumSAPCustomerResponseOp(0, "Silahkan pilih")
                arr.Add(EnumSAPCustomerResponseOp)
            End If
            EnumSAPCustomerResponseOp = New EnumSAPCustomerResponseOp(1, "Dealer Visit")
            arr.Add(EnumSAPCustomerResponseOp)

            EnumSAPCustomerResponseOp = New EnumSAPCustomerResponseOp(2, "Test Drive")
            arr.Add(EnumSAPCustomerResponseOp)

            EnumSAPCustomerResponseOp = New EnumSAPCustomerResponseOp(3, "Negotiation")
            arr.Add(EnumSAPCustomerResponseOp)

            EnumSAPCustomerResponseOp = New EnumSAPCustomerResponseOp(4, "SPK")
            arr.Add(EnumSAPCustomerResponseOp)

            'EnumSAPCustomerResponseOp = New EnumSAPCustomerResponseOp(5, "Sudah Teralokasi")
            'arr.Add(EnumSAPCustomerResponseOp)

            'EnumSAPCustomerResponseOp = New EnumSAPCustomerResponseOp(6, "Sudah PKT")
            'arr.Add(EnumSAPCustomerResponseOp)

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


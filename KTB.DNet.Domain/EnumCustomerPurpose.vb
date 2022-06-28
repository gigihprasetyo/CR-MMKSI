Namespace KTB.DNet.Domain
    Public Class EnumCustomerPurpose
        Public Enum CustomerPurpose
            Tanya = 1
            Test_Drive
            Memesan
            Promo
            Fasilitas_Dealer
            Komplain
            Mengantarkan
            Lain_lain
        End Enum

        Public Shared Function GetStringCustomerPurpose(ByVal source As Integer) As String
            Dim str As String = ""
            Select Case source
                Case 1
                    str = "Tanya kendaraan"
                Case 2
                    str = "Test drive"
                Case 3
                    str = "Memesan kendaraan"
                Case 4
                    str = "Tanya promosi"
                Case 5
                    str = "Tanya fasilitas dealer"
                Case 6
                    str = "Komplain"
                Case 7
                    str = "Mengantar saudara / teman"
                Case 8
                    str = "Lain lain"
            End Select
            Return str
        End Function

    End Class

    Public Class EnumCustomerPurposeOp
        Private _Val As Integer
        Private _Name As String

        Public Shared Function RetriveCustomerPurpose(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim EnumCustomerPurposeOp As EnumCustomerPurposeOp

            If (isIncludeBlank) Then
                EnumCustomerPurposeOp = New EnumCustomerPurposeOp(0, "Silahkan pilih")
                arr.Add(EnumCustomerPurposeOp)
            End If
            EnumCustomerPurposeOp = New EnumCustomerPurposeOp(1, "Tanya kendaraan")
            arr.Add(EnumCustomerPurposeOp)

            EnumCustomerPurposeOp = New EnumCustomerPurposeOp(2, "Test Drive")
            arr.Add(EnumCustomerPurposeOp)

            EnumCustomerPurposeOp = New EnumCustomerPurposeOp(3, "Memesan kendaraan")
            arr.Add(EnumCustomerPurposeOp)

            EnumCustomerPurposeOp = New EnumCustomerPurposeOp(4, "Tanya promosi")
            arr.Add(EnumCustomerPurposeOp)

            EnumCustomerPurposeOp = New EnumCustomerPurposeOp(5, "Tanya fasilitas dealer")
            arr.Add(EnumCustomerPurposeOp)

            EnumCustomerPurposeOp = New EnumCustomerPurposeOp(6, "Komplain")
            arr.Add(EnumCustomerPurposeOp)

            EnumCustomerPurposeOp = New EnumCustomerPurposeOp(7, "Mengantar saudara / teman")
            arr.Add(EnumCustomerPurposeOp)

            EnumCustomerPurposeOp = New EnumCustomerPurposeOp(8, "Lain lain")
            arr.Add(EnumCustomerPurposeOp)

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


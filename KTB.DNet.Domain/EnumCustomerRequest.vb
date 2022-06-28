Namespace KTB.DNet.Domain
    Public Class EnumStatusCustomer
        Public Enum ReadyForUpdate
            No
            Yes
        End Enum
    End Class

    

    Public Class EnumStatusCustomerRequest
        Public Enum TipePengajuanCustomerRequest
            Baru
            Validasi
            Proses
            Block
            Selesai
        End Enum
        Public Function RetrieveStatusVerifikasi() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumDescStatusCustomerRequest
            sts = New EnumDescStatusCustomerRequest(1, "Validasi")
            al.Add(sts)
            sts = New EnumDescStatusCustomerRequest(2, "Proses")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveType() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumDescStatusCustomerRequest
            sts = New EnumDescStatusCustomerRequest(0, "Baru")
            al.Add(sts)
            sts = New EnumDescStatusCustomerRequest(1, "Validasi")
            al.Add(sts)
            sts = New EnumDescStatusCustomerRequest(2, "Proses")
            al.Add(sts)
            sts = New EnumDescStatusCustomerRequest(3, "Block")
            al.Add(sts)
            sts = New EnumDescStatusCustomerRequest(4, "Selesai")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveName(ByVal EnumIndex As Integer) As String
            Select Case EnumIndex
                Case 0
                    Return "Baru"
                Case 1
                    Return "Validasi"
                Case 2
                    Return "Proses"
                Case 3
                    Return "Block"
                Case 4
                    Return "Selesai"
            End Select

        End Function


    End Class

    Public Class EnumDescStatusCustomerRequest
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValTipe() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameTipe() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class

    Public Class EnumTipePengajuanCustomerRequest
        Public Enum TipePengajuanCustomerRequest
            Baru
            Revisi
        End Enum

        Public Function RetrieveType() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTipePengajuan
            sts = New EnumTipePengajuan(0, "Baru")
            al.Add(sts)
            sts = New EnumTipePengajuan(1, "Revisi")
            al.Add(sts)
            Return al
        End Function
        Public Function RetrieveTypeDummy() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTipePengajuan
            sts = New EnumTipePengajuan(0, "Baru")
            al.Add(sts)
            
            Return al
        End Function

        Public Function RetrieveName(ByVal EnumIndex As Integer) As String
            Select Case EnumIndex
                Case 0
                    Return "Baru"
                Case 1
                    Return "Revisi"
            End Select

        End Function

        Public Function Baru()
            Return TipePengajuanCustomerRequest.Baru
        End Function

        Public Function Revisi()
            Return TipePengajuanCustomerRequest.Revisi
        End Function

    End Class

    Public Class EnumTipePengajuan
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValTipe() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameTipe() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class


    Public Class EnumTipePelangganCustomerRequest
        Public Enum TipePelangganCustomerRequest
            Perorangan
            Perusahaan
            BUMN_Pemerintah
            Lainnya
        End Enum

        Public Function RetrieveTypeDiscountProposal() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTipePelanggan
            sts = New EnumTipePelanggan(0, "Perorangan")
            al.Add(sts)
            sts = New EnumTipePelanggan(1, "Perusahaan")
            al.Add(sts)
            sts = New EnumTipePelanggan(2, "BUMN/Pemerintah")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveType(Optional isAll As Boolean = False) As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTipePelanggan
            If isAll = True Then
                sts = New EnumTipePelanggan(-1, "Semua")
                al.Add(sts)
            End If
            sts = New EnumTipePelanggan(0, "Perorangan")
            al.Add(sts)
            sts = New EnumTipePelanggan(1, "Perusahaan")
            al.Add(sts)
            sts = New EnumTipePelanggan(2, "BUMN/Pemerintah")
            al.Add(sts)
            sts = New EnumTipePelanggan(3, "Lainnya")
            al.Add(sts)
            Return al
        End Function
        Public Function RetrieveTypeFleet() As ArrayList
            Dim aL As New ArrayList
            Dim sts As EnumTipePelanggan

            sts = New EnumTipePelanggan(1, "Perusahaan")
            aL.Add(sts)
            sts = New EnumTipePelanggan(2, "BUMN/Pemerintah")
            aL.Add(sts)

            Return aL
        End Function

        Public Shared Function RetrieveTipePelangganCustomerRequest(ByVal EnumIndex As Integer) As String
            Dim vReturn As String = String.Empty
            Select Case EnumIndex
                Case 0
                    vReturn = "Perorangan"
                Case 1
                    vReturn = "Perusahaan"
                Case 2
                    vReturn = "BUMN/Pemerintah"
                Case 3
                    vReturn = "Lainnya"
            End Select
            Return vReturn
        End Function
    End Class

    Public Class EnumTipePerusahaan
        Public Enum EnumTipePerusahaan
            PT
            CV
            UD
            'Lainnya
            PF
            PP
            Yayasan
            Koperasi
            'Lainnya
            '
        End Enum

        Public Function RetrieveType() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTipePelanggan
            sts = New EnumTipePelanggan(0, "PT")
            al.Add(sts)
            sts = New EnumTipePelanggan(1, "CV")
            al.Add(sts)
            sts = New EnumTipePelanggan(2, "UD")
            'CR SPK
            'al.Add(sts)
            'sts = New EnumTipePelanggan(3, "Lainnya")
            al.Add(sts)
            sts = New EnumTipePelanggan(4, "PF")
            al.Add(sts)
            sts = New EnumTipePelanggan(5, "PP")
            al.Add(sts)
            sts = New EnumTipePelanggan(6, "Yayasan")
            al.Add(sts)
            sts = New EnumTipePelanggan(7, "Koperasi")
            al.Add(sts)
            'sts = New EnumTipePelanggan(7, "Lainnya")
            'al.Add(sts)
            '
            Return al
        End Function

        Public Shared Function RetrieveNameTipePerusahaan(ByVal EnumIndex As Integer) As String
            Dim vReturn As String = String.Empty
            Select Case EnumIndex
                Case 0
                    vReturn = "PT"
                Case 1
                    vReturn = "CV"
                Case 2
                    vReturn = "UD"
                    'cr spk
                Case 4
                    vReturn = "PF"
                Case 5
                    vReturn = "PP"
                Case 6
                    vReturn = "Yayasan"
                Case 7
                    vReturn = "Koperasi"
                    'Case 7
                    '   vReturn = "Lainnya"
                    '
            End Select
            Return vReturn
        End Function

    End Class


    Public Class EnumTipePelanggan
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValTipe() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameTipe() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class

    'CR_SPK_SALESMAN_CUSTOMER_ENHANCE juni 2021
    Public Class EnumTipePerorangan
        Public Enum EnumTipePerorangan
            Domestik
            Asing
        End Enum

        Public Function RetrieveType() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTipePelanggan
            sts = New EnumTipePelanggan(0, "Domestik")
            al.Add(sts)
            sts = New EnumTipePelanggan(1, "Asing")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function RetrieveNameTipePerorangan(ByVal EnumIndex As Integer) As String
            Dim vReturn As String = String.Empty
            Select Case EnumIndex
                Case 0
                    vReturn = "Domestik"
                Case 1
                    vReturn = "Asing"

            End Select
            Return vReturn
        End Function

    End Class
    'end CR_SPK_SALESMAN_CUSTOMER_ENHANCE juni 2021
End Namespace


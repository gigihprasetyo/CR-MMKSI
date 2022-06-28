Namespace KTB.DNet.Domain
    Public Class EnumStatusMSP
        Public Enum Status
            Baru
            Validasi
            Batal_Validasi
            Konfirmasi
            Batal_Konfirmasi
            Proses
            Selesai
        End Enum

        Public Enum StatusType
            Baru
            Upgrade
        End Enum

        Public Function RetrieveStatusClaim() As ArrayList
            Dim arr As New ArrayList
            Dim sts As EnumMSPStatusObj
            sts = New EnumMSPStatusObj(5, "Proses")
            arr.Add(sts)
            sts = New EnumMSPStatusObj(6, "Selesai")
            arr.Add(sts)

            Return arr
        End Function

        Public Function RetrieveStatus() As ArrayList
            Dim arr As New ArrayList
            Dim sts As EnumMSPStatusObj
            sts = New EnumMSPStatusObj(0, "Baru")
            arr.Add(sts)
            sts = New EnumMSPStatusObj(1, "Validasi")
            arr.Add(sts)
            'sts = New EnumMSPStatusObj(2, "Batal Validasi")
            'arr.Add(sts)
            sts = New EnumMSPStatusObj(3, "Konfirmasi")
            arr.Add(sts)
            'sts = New EnumMSPStatusObj(4, "Ditolak")
            'arr.Add(sts)
            sts = New EnumMSPStatusObj(5, "Proses")
            arr.Add(sts)
            sts = New EnumMSPStatusObj(6, "Selesai")
            arr.Add(sts)

            Return arr
        End Function

        Public Function RetrieveStatusForProsesKTB() As ArrayList
            Dim arr As New ArrayList
            Dim sts As EnumMSPStatusObj

            sts = New EnumMSPStatusObj(3, "Konfirmasi")
            arr.Add(sts)
            sts = New EnumMSPStatusObj(4, "Batal Konfirmasi")
            arr.Add(sts)

            Return arr
        End Function

        Public Function RetrieveStatusForProsesDealer() As ArrayList
            Dim arr As New ArrayList
            Dim sts As EnumMSPStatusObj
            
            sts = New EnumMSPStatusObj(1, "Validasi")
            arr.Add(sts)
            sts = New EnumMSPStatusObj(2, "Batal Validasi")
            arr.Add(sts)

            Return arr
        End Function

        Public Function RetrieveStatusType() As ArrayList
            Dim arr As New ArrayList
            Dim sts As EnumMSPStatusObj
            sts = New EnumMSPStatusObj(0, "Baru")
            arr.Add(sts)
            sts = New EnumMSPStatusObj(1, "Upgrade")
            arr.Add(sts)

            Return arr
        End Function

        Public Function Create() As String
            Return Status.Baru
        End Function

        Public Function Validate() As String
            Return Status.Validasi
        End Function

        Public Function UnValidate() As String
            Return Status.Batal_Validasi
        End Function

        Public Function Confirm() As String
            Return Status.Konfirmasi
        End Function

        Public Function Process() As String
            Return Status.Proses
        End Function

        Public Function SAPUpdate() As String
            Return Status.Selesai
        End Function

        Public Class EnumMSPStatusObj
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
    End Class
End Namespace

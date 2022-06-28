Namespace KTB.DNet.Domain
    Public Class EnumMSPEx
        Public Enum MSPExStatus
            Baru = 0
            Validasi = 1
            Konfirmasi = 2
            Proses = 3
            Selesai = 4
        End Enum

        Public Enum MSPExStatusPayment
            Baru = 0
            Validasi = 1
            Konfirmasi = 2
            Proses = 3
            Selesai = 4
        End Enum

        Public Shared Function RetrieveStatus() As ArrayList
            Dim arr As New ArrayList
            arr.Add(New EnumMSPExList(0, "Baru"))
            arr.Add(New EnumMSPExList(1, "Validasi"))
            'arr.Add(New EnumMSPExList(2, "Konfirmasi"))
            'arr.Add(New EnumMSPExList(3, "Proses"))
            arr.Add(New EnumMSPExList(4, "Selesai"))
            Return arr
        End Function

        Public Shared Function RetrieveAllStatus() As ArrayList
            Dim arr As New ArrayList
            arr.Add(New EnumMSPExList(0, "Baru"))
            arr.Add(New EnumMSPExList(1, "Validasi"))
            'arr.Add(New EnumMSPExList(2, "Konfirmasi"))
            arr.Add(New EnumMSPExList(3, "Proses"))
            arr.Add(New EnumMSPExList(4, "Selesai"))
            Return arr
        End Function

        Public Shared Function RetrieveStatusProcessForKTB() As ArrayList
            Dim arr As New ArrayList
            arr.Add(New EnumMSPExList(2, "Konfirmasi"))
            Return arr
        End Function

        Public Shared Function RetrieveStatusProcessForDealer() As ArrayList
            Dim arr As New ArrayList
            arr.Add(New EnumMSPExList(1, "Validasi"))
            Return arr
        End Function

        Public Shared Function GetStringValue(ByVal iType As Integer) As String
            Dim str As String = ""
            If iType = 0 Then str = "Baru"
            If iType = 1 Then str = "Validasi"
            If iType = 2 Then str = "Konfirmasi"
            If iType = 3 Then str = "Proses"
            If iType = 4 Then str = "Selesai"
            Return str
        End Function

        Public Class EnumMSPExList
            Private _id As Integer
            Private _val As String

            Public Sub New(ByVal id As Integer, ByVal val As String)
                _id = id
                _val = val
            End Sub
            Public ReadOnly Property ID() As Integer
                Get
                    Return _id
                End Get
            End Property

            Public ReadOnly Property Value() As String
                Get
                    Return _val
                End Get
            End Property

        End Class
    End Class
End Namespace
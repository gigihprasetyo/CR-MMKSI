Imports System.Web.UI.WebControls

Namespace KTB.DNet.Domain

    Public Class EnumStatusDSFLeasingClaim
        Public Enum Status
            Baru = 0
            Validasi = 1
            Konfirmasi = 2
            Setuju_by_Dealer = 3
            Selesai = 4
            Tolak_by_Dealer = 5
            Batal_Validasi = 6
            Batal_Konfirmasi = 7
            Setuju_by_DSF = 8
            Reject_by_DSF = 9
            Tolak_by_MKS = 10
        End Enum

        Public Shared Function RetrieveStatus() As ArrayList
            Dim arl As New ArrayList

            arl.Add(New EnumItem(CInt(Status.Baru), Status.Baru.ToString()))
            arl.Add(New EnumItem(CInt(Status.Validasi), Status.Validasi.ToString()))
            arl.Add(New EnumItem(CInt(Status.Konfirmasi), Status.Konfirmasi.ToString()))
            arl.Add(New EnumItem(CInt(Status.Setuju_by_Dealer), Status.Setuju_by_Dealer.ToString().Replace("_", " ")))
            arl.Add(New EnumItem(CInt(Status.Selesai), Status.Selesai.ToString()))
            arl.Add(New EnumItem(CInt(Status.Tolak_by_Dealer), Status.Tolak_by_Dealer.ToString().Replace("_", " ")))
            arl.Add(New EnumItem(CInt(Status.Batal_Validasi), Status.Batal_Validasi.ToString().Replace("_", " ")))
            arl.Add(New EnumItem(CInt(Status.Batal_Konfirmasi), Status.Batal_Konfirmasi.ToString().Replace("_", " ")))
            arl.Add(New EnumItem(CInt(Status.Setuju_by_DSF), Status.Setuju_by_DSF.ToString().Replace("_", " ")))
            arl.Add(New EnumItem(CInt(Status.Reject_by_DSF), Status.Reject_by_DSF.ToString().Replace("_", " ")))
            arl.Add(New EnumItem(CInt(Status.Tolak_by_MKS), Status.Tolak_by_MKS.ToString().Replace("_", " ")))

            Return arl
        End Function

        Public Shared Function GetStringValueStatus(ByVal iStatus As Integer) As String
            Dim str As String = ""
            If iStatus = 0 Then str = "Baru"
            If iStatus = 1 Then str = "Validasi"
            If iStatus = 2 Then str = "Konfirmasi"
            If iStatus = 3 Then str = "Setuju by Dealer"
            If iStatus = 4 Then str = "Selesai"
            If iStatus = 5 Then str = "Tolak by Dealer"
            If iStatus = 6 Then str = "Batal Validasi"
            If iStatus = 7 Then str = "Batal Konfirmasi"
            If iStatus = 8 Then str = "Setuju by DSF"
            If iStatus = 9 Then str = "Reject by DSF"
            If iStatus = 10 Then str = "Tolak by MKS"

            Return str
        End Function

    End Class
End Namespace

Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class enumPORemarkStatus
        Public Enum PORemarkStatus
            TahanDO = 1
            GantiKeRTGS = 2
            GantiKeCOD = 3
            GantiKeTOP = 4
            Batal = 5
        End Enum

        Public Shared Function GetStringValue(ByVal RemarkStatus As Integer) As String
            Dim str As String = ""
            If RemarkStatus = 1 Then str = "Tahan DO"
            If RemarkStatus = 2 Then str = "Ganti Ke RTGS"
            If RemarkStatus = 3 Then str = "Ganti Ke COD"
            If RemarkStatus = 4 Then str = "Ganti Ke TOP"
            If RemarkStatus = 5 Then str = "Batal"
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sRemarkStatus As String) As Integer
            Dim Rsl As Integer = 0
            If sRemarkStatus.ToUpper = "Tahan DO".ToUpper Then Rsl = 1
            If sRemarkStatus.ToUpper = "Ganti Ke RTGS".ToUpper Then Rsl = 2
            If sRemarkStatus.ToUpper = "Ganti Ke COD".ToUpper Then Rsl = 3
            If sRemarkStatus.ToUpper = "Ganti Ke TOP".ToUpper Then Rsl = 4
            If sRemarkStatus.ToUpper = "Batal".ToUpper Then Rsl = 5
            Return Rsl
        End Function

        Public Shared Function GetList() As ArrayList
            Dim arl As ArrayList = New ArrayList

            arl.Add(New ListItem("Tahan DO", 1))
            arl.Add(New ListItem("Ganti Ke RTGS", 2))
            arl.Add(New ListItem("Ganti Ke COD", 3))
            arl.Add(New ListItem("Ganti Ke TOP", 4))
            arl.Add(New ListItem("Batal", 5))

            Return arl
        End Function



    End Class
End Namespace

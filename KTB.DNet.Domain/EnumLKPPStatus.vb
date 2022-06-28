Imports System.Web.UI.WebControls
Namespace KTB.DNet.Domain
    Public Class EnumLKPPStatus
        Public Enum LKPPStatus
            NonLKPP = 0
            NotVerifiedLKPP = 1
            VerifiedLKPP = 2
            IndicatedLKPP = 3
        End Enum

        Public Shared Function GetStringValue(ByVal pLKPPStatus As Integer) As String
            Dim str As String = ""
            If pLKPPStatus = LKPPStatus.NonLKPP Then str = "NonLKPP"
            If pLKPPStatus = LKPPStatus.NotVerifiedLKPP Then str = "Not Verified LKPP"
            If pLKPPStatus = LKPPStatus.VerifiedLKPP Then str = "Verified LKPP"
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sLKPPStatus As String) As Integer
            Dim Rsl As Integer = 0
            If sLKPPStatus.ToUpper = GetStringValue(LKPPStatus.NonLKPP) Then Rsl = LKPPStatus.NonLKPP
            If sLKPPStatus.ToUpper = GetStringValue(LKPPStatus.NotVerifiedLKPP) Then Rsl = LKPPStatus.NotVerifiedLKPP
            If sLKPPStatus.ToUpper = GetStringValue(LKPPStatus.VerifiedLKPP) Then Rsl = LKPPStatus.VerifiedLKPP
            Return Rsl
        End Function

        Public Shared Function GetList() As ArrayList
            Dim arl As ArrayList = New ArrayList
            arl.Add(New ListItem(GetStringValue(LKPPStatus.NonLKPP), LKPPStatus.NonLKPP.ToString()))
            arl.Add(New ListItem(GetStringValue(LKPPStatus.NotVerifiedLKPP), LKPPStatus.NotVerifiedLKPP.ToString()))
            arl.Add(New ListItem(GetStringValue(LKPPStatus.VerifiedLKPP), LKPPStatus.VerifiedLKPP.ToString()))
            Return arl
        End Function
    End Class
End Namespace

Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class enumPaymentTypeRevision
        Public Enum PaymentType
            Gyro = 1
            Transfer = 2
            Virtual_Account = 3
        End Enum

        Public Shared Function GetStringValue(ByVal PaymentType As Integer) As String
            Dim str As String = ""
            If PaymentType = 1 Then str = "Gyro"
            If PaymentType = 2 Then str = "Transfer"
            If PaymentType = 3 Then str = "Virtual Account"
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sPaymentType As String) As Integer
            Dim Rsl As Integer = 0
            If sPaymentType.ToUpper = "Gyro" Then Rsl = 1
            If sPaymentType.ToUpper = "Transfer" Then Rsl = 2
            If sPaymentType.ToUpper = "Virtual Account" Then Rsl = 3
            Return Rsl
        End Function

        ''' <summary>
        ''' Allo Transfer or not
        ''' </summary>
        ''' <param name="AllowTransfer"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetList(Optional ByVal AllowTransfer As Boolean = True) As ArrayList
            Dim arl As ArrayList = New ArrayList

            'arl.Add(New ListItem("Gyro", 1))
            'If AllowTransfer Then
            '    arl.Add(New ListItem("Transfer", 2))
            'End If

            arl.Add(New ListItem("Virtual Account", 3))

            Return arl
        End Function



    End Class
End Namespace

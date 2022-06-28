Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class enumAccessoriesType
        Public Enum AccessoriesType
            NonAccessories = 0
            Accessories = 1
        End Enum

        Public Shared Function GetStringValue(ByVal AccType As AccessoriesType) As String
            Dim str As String = ""
            If AccType = AccessoriesType.Accessories Then str = AccessoriesType.Accessories.ToString
            If AccType = AccessoriesType.NonAccessories Then str = AccessoriesType.NonAccessories.ToString

            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sAccessoriesType As String) As Integer
            Dim Rsl As Integer = 0
            If sAccessoriesType.ToUpper = AccessoriesType.Accessories.ToString Then Rsl = AccessoriesType.Accessories
            If sAccessoriesType.ToUpper = AccessoriesType.NonAccessories.ToString Then Rsl = AccessoriesType.NonAccessories

            Return Rsl
        End Function

        Public Shared Function GetList() As ArrayList
            Dim arl As ArrayList = New ArrayList

            arl.Add(New ListItem(AccessoriesType.Accessories.ToString, AccessoriesType.Accessories))
            arl.Add(New ListItem(AccessoriesType.NonAccessories.ToString, AccessoriesType.NonAccessories))

            Return arl
        End Function



    End Class
End Namespace

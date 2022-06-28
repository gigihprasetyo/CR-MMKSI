Namespace KTB.DNet.Domain
    Public Class EnumSellingType
        Public Enum SellingType As Short
            Avalis = 0
            NonAvalis
        End Enum
        Public Shared Function RetrieveSellingType() As ArrayList
            Dim arrResult As New ArrayList
            Dim obj As EnumItem
            obj = New EnumItem(SellingType.Avalis, SellingType.Avalis.ToString.Replace("_", " "))
            arrResult.Add(obj)
            obj = New EnumItem(SellingType.NonAvalis, SellingType.NonAvalis.ToString.Replace("_or_", "/"))
            arrResult.Add(obj)
            Return arrResult
        End Function
    End Class
End Namespace
Namespace KTB.DNet.Domain
    Public Class EnumPositionCategoryWSC
        Public Enum PositionCategory
            A
            B
            C
        End Enum

        Public Shared Function PosCategory(ByVal Pos As Integer) As Char
            Dim ht As Hashtable = New Hashtable
            ht.Add(0, "A")
            ht.Add(1, "B")
            ht.Add(2, "C")
            Return ht(Pos)
        End Function
    End Class
End Namespace


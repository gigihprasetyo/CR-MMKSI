Namespace KTB.DNet.Domain

    Public Class EnumEndCustomer
        Public Enum TemporaryFaktur
            Normal = 0
            Temporary = 1
        End Enum

        Public Shared Function TemporaryFakturDesc(ByVal iTemporaryFaktur As String) As String
            If iTemporaryFaktur = "" Then
                Return ""
            Else
                Return CType(iTemporaryFaktur, TemporaryFaktur).ToString()
            End If

        End Function
    End Class
End Namespace

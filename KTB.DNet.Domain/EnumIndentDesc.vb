Namespace KTB.DNet.Domain
    Public Class EnumIndentDesc

        Public Enum IndentDesc
            Silakan_Pilih = 0
            Pasang_or_Stamping = 1
            Kirim = 2
        End Enum

        Public Shared Function RetrieveIndentDesc() As ArrayList
            Dim arrResult As New ArrayList
            Dim obj As EnumItem

            obj = New EnumItem(IndentDesc.Silakan_Pilih, IndentDesc.Silakan_Pilih.ToString.Replace("_", " "))
            arrResult.Add(obj)

            obj = New EnumItem(IndentDesc.Pasang_or_Stamping, IndentDesc.Pasang_or_Stamping.ToString.Replace("_or_", "/"))
            arrResult.Add(obj)

            obj = New EnumItem(IndentDesc.Kirim, IndentDesc.Kirim.ToString)
            arrResult.Add(obj)

            Return arrResult

        End Function

    End Class
End Namespace
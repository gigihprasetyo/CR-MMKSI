Namespace KTB.DNet.Domain
    Public Class EnumDocumentType

        Public Enum DocumentType As Short
            SPAF = 0
            Subsidi = 1
        End Enum

        Public Shared Function RetrieveDocumentType() As ArrayList
            Dim arrResult As New ArrayList
            Dim obj As EnumItem
            obj = New EnumItem(DocumentType.SPAF, DocumentType.SPAF.ToString.Replace("_", " "))
            arrResult.Add(obj)
            obj = New EnumItem(DocumentType.Subsidi, DocumentType.Subsidi.ToString.Replace("_or_", "/"))
            arrResult.Add(obj)
            Return arrResult
        End Function
    End Class
End Namespace
Imports System.Collections.Generic

Namespace KTB.DNet.Parser.Domain
    Public Class ServiceTemplateFFPartHeaderJson
        Public Varian As String
        Public RegNo As String
        Public Detail As List(Of ServiceTemplateFFPartDetailJson)
    End Class

    Public Class ServiceTemplateFFPartDetailJson
        Public MaterialDetail As String
        Public Quantity As String
    End Class
End Namespace

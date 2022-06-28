Imports System.Collections.Generic

Namespace KTB.DNet.Parser.Domain
    Public Class ServiceTemplatePMPartHeaderJson
        Public Varian As String
        Public PMCode As String
        Public Detail As List(Of ServiceTemplatePMPartDetailJson)
    End Class

    Public Class ServiceTemplatePMPartDetailJson
        Public MaterialDetail As String
        Public Quantity As String
    End Class
End Namespace

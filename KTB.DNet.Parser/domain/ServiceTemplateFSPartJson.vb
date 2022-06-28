Imports System.Collections.Generic

Namespace KTB.DNet.Parser.Domain
    Public Class ServiceTemplateFSPartHeaderJson
        Public KindofService As String
        Public MaterialGroup As String
        Public ValidFrom As String
        Public Detail As List(Of ServiceTemplateFSPartDetailJson)
    End Class

    Public Class ServiceTemplateFSPartDetailJson
        Public MaterialDetail As String
        Public NetAmount As String
        Public Quantity As String
    End Class
End Namespace

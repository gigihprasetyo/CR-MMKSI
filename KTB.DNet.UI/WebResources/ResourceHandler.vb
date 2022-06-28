Imports System
Imports System.Web

Namespace KTB.DNet.UI.WebResources

    Public Class ResourceHandler
        Implements IHttpHandler

        Public ReadOnly Property IsReusable() As Boolean Implements System.Web.IHttpHandler.IsReusable
            Get
                Return True
            End Get
        End Property

        Public Sub ProcessRequest(ByVal context As System.Web.HttpContext) Implements System.Web.IHttpHandler.ProcessRequest
            Dim strType As String = String.Empty
            Dim strName As String = String.Empty

            If IsNothing(context.Request("type")) OrElse IsNothing(context.Request("name")) Then
                WriteError()
                Return
            Else
                strType = context.Request("type").ToString
                strName = context.Request("name").ToString
            End If


            Select Case (strType)
                Case "Javascript"
                    Dim strScript As String = Javascript.Instance.GetScript(strName)
                    If strScript.Length = 0 Then
                        WriteError()
                        Return
                    End If
                    context.Response.Write(strScript)

            End Select
        End Sub
#Region "Private Methods"
        Private Sub WriteError()
            HttpContext.Current.Response.Write("No resource type or name specified")
        End Sub
		
#End Region
    End Class





End Namespace


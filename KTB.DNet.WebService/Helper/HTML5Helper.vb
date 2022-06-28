Imports System.Linq
Imports System.Data
Imports System.Text
Imports System.Security.Principal
Imports System.Web
Imports System.Web.Mvc
Imports System.Runtime.CompilerServices



Namespace Helper

    Public Module HtmlHelperExtensions


        ''' <summary>
        ''' Cut Left String
        ''' </summary>
        ''' <param name="htlmHelper"></param>
        ''' <param name="ObjectModel"></param>
        ''' <param name="objLength"></param>
        ''' <returns> String</returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function Left(htlmHelper As HtmlHelper, ObjectModel As [Object], ByVal objLength As Integer) As MvcHtmlString
            Dim sb As New StringBuilder()

            Dim Str As String = ""

            If ObjectModel.ToString().Length > objLength Then
                Str = Strings.Left(ObjectModel.ToString(), objLength) + "...."
            Else
                Str = ObjectModel.ToString()
            End If

            If Str = "" Then
                Str = "....."
            End If

            sb.Append(Str)

            Return New MvcHtmlString(sb.ToString())
        End Function

        ''' <summary>
        ''' Replace  \n to  <br/>
        ''' </summary>
        ''' <param name="htlmHelper"></param>
        ''' <param name="ObjectModel"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function ToTable(htlmHelper As HtmlHelper, ObjectModel As [Object]) As MvcHtmlString
            Dim sb As New StringBuilder()

            Dim str As String = ObjectModel.ToString().Replace("\n", "</br>")
            sb.Append(str)
            Return New MvcHtmlString(sb.ToString())
        End Function

    End Module







End Namespace
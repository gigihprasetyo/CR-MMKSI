Imports System.Reflection
Imports System.Resources
Imports System.Globalization

Namespace KTB.Dnet.ResourceManager
    Public Class DNetResourceManager
        Private rm As System.Resources.ResourceManager

        Public Sub New(ByVal ResourceName As String, ByVal _assembly As System.Reflection.Assembly)
            rm = New System.Resources.ResourceManager(ResourceName, _assembly)
        End Sub

        Public Function GetKeyValue(ByVal key As String) As String
            Return rm.GetString(key)
        End Function

        Public Function GetKeyValue(ByVal key As String, ByVal uc As CultureInfo) As String
            Return rm.GetString(key, uc)
        End Function

    End Class
End Namespace

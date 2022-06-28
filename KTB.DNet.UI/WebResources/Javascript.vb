

imports  System
Imports System.Resources

Public Class Javascript


#Region "Private Variables"
    Private Shared _Instance As Javascript = Nothing
    Private resourceManager As resourceManager = Nothing
#End Region

#Region "Creator/Destructor/Finalizer"
    Protected Sub New()
        'resourceManager = New resourceManager(GetType(Javascript))
        resourceManager = New resourceManager("Javascript", Reflection.Assembly.GetExecutingAssembly())
    End Sub



#End Region

#Region "Public Methods"
    Public Shared ReadOnly Property Instance() As Javascript
        Get
            If IsNothing(_Instance) Then _Instance = New Javascript
            Return _Instance

        End Get

    End Property


    Public ReadOnly Property GetScript(ByVal Name As String) As String
        Get
            Dim script As String = ""
            Try
                script = resourceManager.GetString(Name)
            Catch ex As Exception
                Dim strEx As String = ex.ToString()
            End Try
            Return IIf(IsNothing(script), String.Empty, script)
        End Get
    End Property

#End Region
End Class

Imports System.ServiceProcess
Namespace KTB.DNet.SapServiceManager

    Public Class SAPListenerManager

#Region "Varriable Declaration"
        Private myController As ServiceController
        Private _ServiceName As String = String.Empty
#End Region

#Region "Constructor"
        Public Sub New()

        End Sub

        Public Sub New(ByVal ServiceName As String)
            _ServiceName = ServiceName
            myController = New ServiceController(_ServiceName)
        End Sub

#End Region

#Region "Properties"
        ' TODO: Is this property really needed?
        ' Write only property indicate flawed design
        ' U can either add a getter for ServiceName,
        ' or remove default constructor & this setter
        WriteOnly Property ServiceName() As String
            Set(ByVal Value As String)
                _ServiceName = Value
                myController = New ServiceController(_ServiceName)
            End Set
        End Property

        ReadOnly Property Status() As ServiceControllerStatus
            Get
                myController.Refresh()
                Return myController.Status
            End Get
        End Property

#End Region

#Region "Public Operation"
        Public Function GetServiceStatus() As String
            If myController Is Nothing Then
                Throw New Exception("Undifined Service Name")
            Else
                Dim sStatus As String
                myController.Refresh()
                sStatus = myController.Status.ToString
                sStatus = myController.ServiceName & " is in state : " & sStatus
                Return sStatus
            End If
        End Function

        Public Sub StartService()
            Try
                myController.Start()
                myController.WaitForStatus(ServiceControllerStatus.Running)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub StopService()
            If myController.CanStop Then
                myController.Stop()
                myController.WaitForStatus(ServiceControllerStatus.Stopped)
            Else
                Throw New Exception("Service cannot be stopped")
            End If
        End Sub

#End Region

    End Class
End Namespace

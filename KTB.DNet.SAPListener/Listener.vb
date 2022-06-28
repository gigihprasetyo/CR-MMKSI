Imports System.ServiceProcess
Namespace KTB.DNet.SapListener

    Public Class Listener
        <MTAThread()> _
        Shared Sub Main()
            Dim ServicesToRun() As System.ServiceProcess.ServiceBase
            ServicesToRun = New System.ServiceProcess.ServiceBase() {New SAPListenerService}
            'ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
            System.ServiceProcess.ServiceBase.Run(ServicesToRun)
        End Sub
    End Class
End Namespace

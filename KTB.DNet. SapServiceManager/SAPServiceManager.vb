Imports System.Threading
Imports System.Runtime.InteropServices
Imports KTB.DNet.SapServiceManager
Namespace KTB.DNet.SapServiceManager

    Public Class SAPServiceManager
        <STAThread()> _
     Shared Sub Main()
            Dim createdNew As Boolean
            Dim _mutex As Mutex
            Dim ApplicationName As String = Application.ProductName
            Dim AppContext As ServiceManagerApplicationContext
            Try
                _mutex = New Mutex(True, ApplicationName, createdNew)
                If Not createdNew Then
                    MessageBox.Show("Instance already running.", "SAP Service Manager.", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1)
                    Return
                End If
                AppContext = New ServiceManagerApplicationContext
                Application.Run(AppContext)
            Catch ex As Exception
                MessageBox.Show("Error Starting SAP Service Manager :" & ex.ToString, "SAP Service Manager.", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1)
            Finally
                If Not _mutex Is Nothing Then
                    _mutex = Nothing
                End If
                GC.KeepAlive(_mutex)
            End Try
        End Sub
    End Class
End Namespace
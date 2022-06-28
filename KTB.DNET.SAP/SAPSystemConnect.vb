Imports SAP.Middleware.Connector
Imports SAP.Middleware.Connector.RfcServerManager
Imports System.Threading
Imports System.Configuration

Public Class SAPSystemConnect
    Implements IDestinationConfiguration

    Public Function ChangeEventsSupported() As Boolean Implements Global.SAP.Middleware.Connector.IDestinationConfiguration.ChangeEventsSupported
        Return True
    End Function

    Public Event ConfigurationChanged(destinationName As String, args As Global.SAP.Middleware.Connector.RfcConfigurationEventArgs) Implements IDestinationConfiguration.ConfigurationChanged

    Public Function GetParameters(destinationName As String) As Global.SAP.Middleware.Connector.RfcConfigParameters Implements IDestinationConfiguration.GetParameters
        'Dim parms As New RfcConfigParameters
        ''If "SAPConnectionString".Equals(destinationName) Then
        'parms.Add(RfcConfigParameters.Name, KTB.DNET.Lib.WebConfig.GetValue("SAP_APPSERVERHOST"))
        'parms.Add(RfcConfigParameters.AppServerHost, KTB.DNET.Lib.WebConfig.GetValue("SAP_APPSERVERHOST"))
        'parms.Add(RfcConfigParameters.SystemNumber, KTB.DNET.Lib.WebConfig.GetValue("SAP_SYSTEMNUM"))
        'parms.Add(RfcConfigParameters.User, KTB.DNET.Lib.WebConfig.GetValue("SAP_USERNAME"))
        'parms.Add(RfcConfigParameters.Password, KTB.DNET.Lib.WebConfig.GetValue("SAP_PASSWORD"))
        'parms.Add(RfcConfigParameters.Client, KTB.DNET.Lib.WebConfig.GetValue("SAP_CLIENT"))
        'parms.Add(RfcConfigParameters.Language, KTB.DNET.Lib.WebConfig.GetValue("SAP_LANGUAGE"))
        'parms.Add(RfcConfigParameters.PoolSize, KTB.DNET.Lib.WebConfig.GetValue("SAP_POOLSIZE"))
        'parms.Add(RfcConfigParameters.IdleTimeout, KTB.DNET.Lib.WebConfig.GetValue("SAP_IDLE_TIMEOUT"))
        'End If
        Dim parms As New RfcConfigParameters
        Dim strConnection As String = ""
        Dim arrConnection() As String
        Dim arrProp() As String
        Dim i As Integer = 0

        'commented by donas on 20161002 for dyn user password strConnection = KTB.DNet.Lib.WebConfig.GetValue("SAP_CONNECTION") 'SAPConnectionString ' 
        strConnection = destinationName 'added by donas on 20161002 for dyn user password 
        arrConnection = strConnection.Split(";")
        For i = 0 To arrConnection.Length - 1
            arrProp = arrConnection(i).Split("=")
            If arrProp(0).ToUpper = "NAME" Then
                parms.Add(RfcConfigParameters.Name, arrProp(1))
            End If
            If arrProp(0).ToUpper = "APPSERVERHOST" Then
                parms.Add(RfcConfigParameters.AppServerHost, arrProp(1))
            End If
            If arrProp(0).ToUpper = "SYSTEMNUM" Then
                parms.Add(RfcConfigParameters.SystemNumber, arrProp(1))
            End If
            If arrProp(0).ToUpper = "USERNAME" Then
                parms.Add(RfcConfigParameters.User, arrProp(1))
            End If
            If arrProp(0).ToUpper = "PASSWORD" Then
                parms.Add(RfcConfigParameters.Password, arrProp(1))
            End If
            If arrProp(0).ToUpper = "CLIENT" Then
                parms.Add(RfcConfigParameters.Client, arrProp(1))
            End If
            If arrProp(0).ToUpper = "LANGUAGE" Then
                parms.Add(RfcConfigParameters.Language, arrProp(1))
            End If
            If arrProp(0).ToUpper = "POOLSIZE" Then
                parms.Add(RfcConfigParameters.PoolSize, arrProp(1))
            End If
            If arrProp(0).ToUpper = "IDLE_TIMEOUT" Then
                parms.Add(RfcConfigParameters.IdleTimeout, arrProp(1))
            End If
        Next

        Return parms
    End Function
End Class

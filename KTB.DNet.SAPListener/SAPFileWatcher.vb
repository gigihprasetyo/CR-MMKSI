' TODO: parameter in methods should be camelCase, not PascalCase
#Region "Imports"
Imports System.IO
Imports System.Threading
Imports System.Configuration
Imports KTB.DNet.Parser
Imports KTB.DNet.Lib
#End Region


Namespace KTB.DNet.SapListener

    Public Class SAPFileWatcher

        Private WithEvents objFsw As IO.FileSystemWatcher
        Public Event FileCreated(ByVal FullPath As String)
        Public Event FileDeleted(ByVal FilePath As String)
        Public Event FileChanged(ByVal FullPath As String)
        Public Event FileRenamed(ByVal OldFileName As String, ByVal newFileName As String)
        Public Event FileWatchError(ByVal ErrMsg As String)
        Private myThread As Thread

        Private smtp As String
        Private sender As String
        Private rec As String
        Private subject As String


        Public Sub New()
            objFsw = New IO.FileSystemWatcher
            'Watch all changes
            objFsw.NotifyFilter = IO.NotifyFilters.Attributes Or _
                                             IO.NotifyFilters.CreationTime Or _
                                            IO.NotifyFilters.DirectoryName Or _
                                          IO.NotifyFilters.FileName Or _
                                        IO.NotifyFilters.LastWrite Or _
                                      IO.NotifyFilters.Security Or _
                                    IO.NotifyFilters.Size
        End Sub

        Public Property FolderToMonitor() As String 'Folder to monitor
            Get
                FolderToMonitor = objFsw.Path
            End Get
            Set(ByVal Value As String)
                If Right(Value, 1) <> "\" Then Value = Value & "\"
                If IO.Directory.Exists(Value) Then
                    objFsw.Path = Value
                End If
            End Set
        End Property

        Public Property IncludeSubfolders() As Boolean
            Get
                IncludeSubfolders = objFsw.IncludeSubdirectories
            End Get
            Set(ByVal Value As Boolean)
                objFsw.IncludeSubdirectories = Value
            End Set
        End Property

        Public Function StartWatch() As Boolean
            Dim bAns As Boolean = False
            Try
                objFsw.EnableRaisingEvents = True
                bAns = True
            Catch ex As Exception
                RaiseEvent FileWatchError(ex.Message)
            End Try
            Return bAns
        End Function

        Public Function StopWatch() As Boolean
            Dim bAns As Boolean = False
            Try
                objFsw.EnableRaisingEvents = False
                bAns = True
            Catch ex As Exception
                RaiseEvent FileWatchError(ex.Message)
            End Try
            Return bAns
        End Function

        Private Sub objFsw_Created(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles objFsw.Created
            Dim fileName As String = e.FullPath
            Dim ext As String = WebConfig.GetValue("SIGNAL_EXTENSION")
            If fileName.Trim.Length > 0 Then
                If ext.Trim.Length > 0 Then
                    ProcessingFile(fileName, ext)
                Else
                    ProcessingFile(fileName)
                End If
            End If
        End Sub

        Private Sub ProcessingFile(ByVal fileName As String)
            Dim finfo As FileInfo = New FileInfo(fileName)
            If Not finfo.Directory.Name.ToUpper = "HISTORY" Then
                If finfo.Extension.Length > 0 Then
                    Dim destFolder As String = WebConfig.GetValue("DestinationFolder")
                    Dim _worker As New Worker(fileName, destFolder)
                    _worker.UseSignal = False
                    Dim _waitCallBack As WaitCallback = New WaitCallback(AddressOf _worker.Work)
                    ThreadPool.QueueUserWorkItem(_waitCallBack, "Doing Work")
                    If Not _worker Is Nothing Then
                        _worker = Nothing
                    End If
                End If
            End If
        End Sub

        Private Sub ProcessingFile(ByVal fileName As String, ByVal extension As String)
            Dim finfo As FileInfo = New FileInfo(fileName)
            Dim correctFileInfo As FileInfo
            If Not finfo.Directory.Name.ToUpper = "HISTORY" Then
                If finfo.Extension.Length > 0 Then
                    If finfo.Extension.ToUpper = extension.ToUpper Then
                        Dim destFolder As String = WebConfig.GetValue("DestinationFolder")
                        Dim _worker As New Worker(fileName, destFolder)
                        _worker.UseSignal = True
                        Dim _waitCallBack As WaitCallback = New WaitCallback(AddressOf _worker.Work)
                        ThreadPool.QueueUserWorkItem(_waitCallBack, "Doing Work")
                        If Not _worker Is Nothing Then
                            _worker = Nothing
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub objFsw_Changed(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles objFsw.Changed
            RaiseEvent FileChanged(e.FullPath)
        End Sub

        Private Sub objFsw_Renamed(ByVal sender As Object, ByVal e As System.IO.RenamedEventArgs) Handles objFsw.Renamed
            RaiseEvent FileRenamed(e.OldFullPath, e.FullPath)
        End Sub

        Private Sub objFsw_Deleted(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles objFsw.Deleted
            RaiseEvent FileDeleted(e.FullPath)
        End Sub

        Private Sub objFsw_Error(ByVal sender As Object, ByVal e As System.IO.ErrorEventArgs) Handles objFsw.Error
            RaiseEvent FileWatchError(e.GetException.Message)
        End Sub
    End Class
End Namespace

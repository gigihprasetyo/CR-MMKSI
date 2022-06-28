#Region "Summary"
'// ===========================================================================		
'// Author Name   : Agus Pirnadi
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 11/10/2005
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region



Namespace KTB.DNet.Utility

    Public Class UploadToWebServer

        Public Sub CreateDirectory(ByVal finfo As FileInfo)
            Dim dirInfo As DirectoryInfo = New DirectoryInfo(finfo.DirectoryName)
            If Not dirInfo.Exists Then
                dirInfo.Create()
            End If
        End Sub

        Public Sub Upload(ByVal From As Stream, ByVal DestFile As String)

            Dim finfo As FileInfo = New FileInfo(DestFile)
            If finfo.Exists Then
                finfo.Delete()  '-- Delete destination file if exists
            End If

            '-- Create destination file
            CreateDirectory(New FileInfo(DestFile))
            Dim Dest As FileStream = New FileStream(DestFile, FileMode.Create, FileAccess.ReadWrite)

            Try
                Dim buffer(4096) As Byte
                Dim readen As Integer = From.Read(buffer, 0, buffer.Length)  '-- Copy stream to buffer
                Do While readen <> 0
                    Dest.Write(buffer, 0, readen)  '-- Write buffer to destination file
                    readen = From.Read(buffer, 0, buffer.Length)  '-- Next copy
                Loop
            Finally
                Dest.Close()
            End Try

        End Sub

        Public Sub DeleteFile(ByVal DestFile As String)
            Dim finfo As FileInfo = New FileInfo(DestFile)
            If finfo.Exists Then
                finfo.Delete()  '-- Delete destination file if exists
            End If
        End Sub
    End Class

End Namespace

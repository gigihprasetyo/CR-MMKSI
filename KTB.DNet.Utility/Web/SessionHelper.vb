#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2005

'Intimedia grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by Intimedia for you 
'(ii) or in solutions that are developed in join development between you and Intimedia.

'All rights not expressly granted, are reserved.
#End Region

#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/11/2005 - 11:50:00 AM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.Web
Imports System.Collections

#End Region

Namespace KTB.DNet.Utility

    Public Class SessionHelper

        Private Const LoginDataSessionName1 = "DEALER"
        Private Const LoginDataSessionName2 = "LOGINUSERINFO"

        Private Sub IntoCollection(ByVal strSessionName As String, ByVal boolAddOrRemove As Boolean)

            Dim arlSessionCollection As ArrayList = New ArrayList

            If IsNothing(HttpContext.Current.Session.Contents("SessionCollection")) Then
                HttpContext.Current.Session.Add("SessionCollection", arlSessionCollection)
            Else
                arlSessionCollection = CType(HttpContext.Current.Session("SessionCollection"), ArrayList)
            End If

            If (boolAddOrRemove) Then

                If Not arlSessionCollection.Contains(strSessionName) Then
                    '//Add Into Collection
                    arlSessionCollection.Add(strSessionName)
                    HttpContext.Current.Session("SessionCollection") = arlSessionCollection
                End If
            Else
                '//Remove From Collection
                arlSessionCollection.Remove(strSessionName)
                HttpContext.Current.Session("SessionCollection") = arlSessionCollection
            End If

        End Sub

        Public Sub RemoveSession(ByVal strSessionName As String)
            HttpContext.Current.Session.Remove(strSessionName)
            IntoCollection(strSessionName, False)
        End Sub

        Public Shared Sub RemoveAll()
            If Not IsNothing(HttpContext.Current.Session.Contents("SessionCollection")) Then
                For Each strSessionName As String In CType(HttpContext.Current.Session.Contents("SessionCollection"), ArrayList)
                    HttpContext.Current.Session.Remove(strSessionName)
                Next
                HttpContext.Current.Session.Contents.Remove("SessionCollection")
            End If
        End Sub

        Public Shared Sub RemoveAllExceptLoginData()
            If Not IsNothing(HttpContext.Current.Session.Contents("SessionCollection")) Then
                For Each strSessionName As String In CType(HttpContext.Current.Session.Contents("SessionCollection"), ArrayList)
                    If strSessionName <> LoginDataSessionName1 And strSessionName <> LoginDataSessionName2 Then
                        HttpContext.Current.Session.Remove(strSessionName)
                    End If
                Next
            End If
        End Sub

        Public Sub SetSession(ByVal strSessionName As String, ByVal SessionContent As Object)
            HttpContext.Current.Session(strSessionName) = SessionContent
            IntoCollection(strSessionName, True)
        End Sub

        Public Function GetSession(ByVal strSessionName As String) As Object
            Return CType(HttpContext.Current.Session(strSessionName), Object)
        End Function

    End Class

End Namespace
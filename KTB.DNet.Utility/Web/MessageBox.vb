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
'// Generated on 8/11/2005 - 8:33:00 AM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.Collections
Imports System.Text
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

#End Region

Namespace KTB.DNet.Utility

    Public Class MessageBox

        Private Shared m_executingPages As Hashtable = New Hashtable

        Private MessageBox()

        Public Shared Sub Show(ByVal sMessage As String)

            '// If this is the first time a page has called this method then
            If Not m_executingPages.Contains(HttpContext.Current.Handler) Then

                '// Attempt tox cast HttpHandler as a Page.
                'Dim executingPage As Page = CType(HttpContext.Current.Handler, Page)

                Dim executingPage As Page = IIf(TypeOf HttpContext.Current.Handler Is Page, CType(HttpContext.Current.Handler, Page), CType(Nothing, Page))

                'Page executingPage = HttpContext.Current.Handler as Page
                'If TypeOf HttpContext.Current.Handler Is Page Then
                '    Dim executing As Page = CType(HttpContext.Current.Handler, Page)
                'Else
                '    Dim executing As Page = CType(Nothing, Page)
                'End If


                If Not IsNothing(executingPage) Then

                    '// Create a Queue to hold one or more messages.
                    Dim messageQueue As queue = New queue

                    '// Add our message to the Queue
                    messageQueue.Enqueue(sMessage)

                    '// Add our message queue to the hash table. Use our page reference
                    '// (IHttpHandler) as the key.
                    m_executingPages.Add(HttpContext.Current.Handler, messageQueue)

                    '// Wire up Unload event so that we can inject 
                    '// some JavaScript for the alerts.

                    'executingPage.Unload += New EventHandler(ExecutingPage_Unload)
                    AddHandler executingPage.Unload, AddressOf ExecutingPage_Unload

                End If

            Else

                '// If were here then the method has allready been 
                '// called from the executing Page.
                '// We have allready created a message queue and stored a
                '// reference to it in our hastable. 
                Dim queue As queue = CType(m_executingPages(HttpContext.Current.Handler), queue)

                '// Add our message to the Queue
                queue.Enqueue(sMessage)

            End If

        End Sub

        '// Our page has finished rendering so lets output the
        '// JavaScript to produce the alert's
        Private Shared Sub ExecutingPage_Unload(ByVal sender As Object, ByVal e As EventArgs)

            '// Get our message queue from the hashtable
            Dim queue As queue = CType(m_executingPages(HttpContext.Current.Handler), queue)

            If Not IsNothing(queue) Then

                Dim sb As StringBuilder = New StringBuilder

                '// How many messages have been registered?
                Dim iMsgCount As Integer = queue.Count

                '// Use StringBuilder to build up our client slide JavaScript.
                sb.Append("<script language='javascript'>")

                '// Loop round registered messages
                Dim sMsg As String

                While (iMsgCount) > 0

                    sMsg = CType(queue.Dequeue, String)
                    'sementara di comment supaya bisa pindah baris 
                    'sMsg = sMsg.Replace("\n", "\\n")
                    'sMsg = sMsg.Replace("\", "'")
                    sb.Append("alert( """ + sMsg + """ );")

                    iMsgCount -= 1

                End While

                '// Close our JS
                sb.Append("</script>")

                '// Were done, so remove our page reference from the hashtable
                m_executingPages.Remove(HttpContext.Current.Handler)

                '// Write the JavaScript to the end of the response stream.
                HttpContext.Current.Response.Write(sb.ToString())

            End If

        End Sub


        Public Shared Sub Confirm(ByVal sMessage As String, ByVal sHiddenField As String)

            '// If this is the first time a page has called this method then
            If Not m_executingPages.Contains(HttpContext.Current.Handler) Then

                '// Attempt tox cast HttpHandler as a Page.
                'Dim executingPage As Page = CType(HttpContext.Current.Handler, Page)

                Dim executingPage As Page = IIf(TypeOf HttpContext.Current.Handler Is Page, CType(HttpContext.Current.Handler, Page), CType(Nothing, Page))

                'Page executingPage = HttpContext.Current.Handler as Page
                'If TypeOf HttpContext.Current.Handler Is Page Then
                '    Dim executing As Page = CType(HttpContext.Current.Handler, Page)
                'Else
                '    Dim executing As Page = CType(Nothing, Page)
                'End If


                If Not IsNothing(executingPage) Then

                    '// Create a Queue to hold one or more messages.
                    Dim messageQueue As queue = New queue

                    '// Add our message to the Queue
                    messageQueue.Enqueue(sMessage)
                    messageQueue.Enqueue(sHiddenField)

                    '// Add our message queue to the hash table. Use our page reference
                    '// (IHttpHandler) as the key.
                    m_executingPages.Add(HttpContext.Current.Handler, messageQueue)

                    '// Wire up Unload event so that we can inject 
                    '// some JavaScript for the alerts.

                    'executingPage.Unload += New EventHandler(ExecutingPage_Unload)
                    AddHandler executingPage.Unload, AddressOf ConfirmPage_Unload

                End If

            Else

                '// If were here then the method has allready been 
                '// called from the executing Page.
                '// We have allready created a message queue and stored a
                '// reference to it in our hastable. 
                Dim queue As queue = CType(m_executingPages(HttpContext.Current.Handler), queue)

                '// Add our message to the Queue
                queue.Enqueue(sMessage)
                queue.Enqueue(sHiddenField)

            End If

        End Sub

        Private Shared Sub ConfirmPage_Unload(ByVal sender As Object, ByVal e As EventArgs)

            '// Get our message queue from the hashtable
            Dim queue As queue = CType(m_executingPages(HttpContext.Current.Handler), queue)

            If Not IsNothing(queue) Then

                Dim sb As StringBuilder = New StringBuilder

                '// How many messages have been registered?
                Dim iMsgCount As Integer = queue.Count

                '// Use StringBuilder to build up our client slide JavaScript.
                
                '// Loop round registered messages
                Dim sMsg As String
                Dim hiddenfield_name As String

                sMsg = CType(queue.Dequeue, String)
                'sMsg = sMsg.Replace("\n", "\\n")
                'sMsg = sMsg.Replace("\", "'")
                hiddenfield_name = CType(queue.Dequeue, String)
                'put JS here
                'sb.Append("<INPUT  type=hidden value='0' ID='" + hiddenfield_name + "'>")
                sb.Append("<script language='javascript'>")
                sb.Append(" if(confirm( """ + sMsg + """ ))")
                sb.Append(" { ")
                'sb.Append("alert('OK');")
                sb.Append("document.forms[0]." + hiddenfield_name + ".value='1';}")
                sb.Append(" else { ")
                'sb.Append("alert('NO');")
                sb.Append("document.forms[0]." + hiddenfield_name + ".value='0';}")
                sb.Append("document.forms[0].submit();")
                'sb.Append("alert(document.forms[0]." + hiddenfield_name + ".value); ")
                '// Close our JS
                sb.Append("</script>")

                '// Were done, so remove our page reference from the hashtable
                m_executingPages.Remove(HttpContext.Current.Handler)

                '// Write the JavaScript to the end of the response stream.
                HttpContext.Current.Response.Write(sb.ToString())

            End If

        End Sub



    End Class

End Namespace
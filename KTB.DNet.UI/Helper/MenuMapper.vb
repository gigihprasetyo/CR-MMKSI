#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2005

'Intimedia grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by Intimedia for you 
'(ii) or in solutions that are developed in join development between you and KTB.DNet.

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
'// Generated on 7/26/2005 - 09:56:00 AM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.Collections
Imports System.Reflection
Imports System.Xml

#End Region

Public Class MenuMapper

#Region "Private Variables"
    Private m_XmlMap As XmlTextReader
    Private m_XmlMapPath As String = "\MenuMapper.xml"
    Private m_XmlMenuIDAttribute As String = "MenuID"
    Private m_XmlMenuPageAttribute As String = "MenuPage"
    Private m_MapTable As Hashtable = New Hashtable

#End Region

#Region "Constructors/Destructors/Finalizers"

    Public Sub New()
        Me.LoadMap()
        Me.PrepareMap()
    End Sub

#End Region

#Region "Public Methods"
    Public Function GetMenuPage(ByVal _MenuID As String) As String
        Try
            Return Me.m_MapTable(_MenuID).ToString()
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
#End Region

#Region "private Methods"

    Private Sub LoadMap()
        Try
            Me.m_XmlMap = New XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + Me.m_XmlMapPath)
        Catch xmlEx As XmlException
            Throw xmlEx
        End Try
    End Sub

    Private Sub PrepareMap()
        Try
            While (Me.m_XmlMap.Read())
                If (Me.m_XmlMap.NodeType = XmlNodeType.Element) Then
                    If (Me.m_XmlMap.AttributeCount > 0) Then
                        Dim MenuID As String = Me.m_XmlMap.GetAttribute(Me.m_XmlMenuIDAttribute)
                        Dim MenuPage As String = Me.m_XmlMap.GetAttribute(Me.m_XmlMenuPageAttribute)
                        Me.m_MapTable.Add(MenuID, MenuPage)
                    End If
                End If
            End While

        Catch xmlEx As XmlException
            '//TODO: Handle Exception here, depends on exception handling mechanism
            Throw
        Finally
            Me.m_XmlMap.Close()
        End Try
    End Sub



#End Region

End Class
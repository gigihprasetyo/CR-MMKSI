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

Namespace KTB.DNet.DataMapper.Framework

    Public Class MapperFactory

#Region "Private Variables"

        Private Shared m_Instance As MapperFactory
        Private m_XmlMap As XmlTextReader
        Private m_XmlMapPath As String = "\DomainDataMap.xml"
        Private m_XmlDomainAttribute As String = "Domain"
        Private m_XmlMapperAttribute As String = "Mapper"
        Private m_MapTable As Hashtable = New Hashtable
        Private m_instanceName As String
#End Region

#Region "Constructors/Destructors/Finalizers"

        Private Sub New()

            Me.LoadMap()
            Me.PrepareMap()

        End Sub

        Private Sub New(ByVal instanceName As String)
            Me.m_instanceName = instanceName
            Me.LoadMap()
            Me.PrepareMap()

        End Sub

#End Region

#Region "Public Methods"

        Public Shared Function GetInstance() As MapperFactory
            If (IsNothing(m_Instance)) Then
                m_Instance = New MapperFactory
            End If
            Return m_Instance
        End Function

        Public Function GetMapper(ByVal domainType As String) As IMapper
            Try
                Dim _mapperType As Type = Type.GetType(MapperType(domainType))
                Return CType(Activator.CreateInstance(_mapperType), IMapper)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetMapper(ByVal domainType As String, ByVal instanceName As String) As IMapper
            Try
                Dim _mapperType As Type = Type.GetType(MapperType(domainType))
                Return CType(Activator.CreateInstance(_mapperType, instanceName), IMapper)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "private Methods"

        Private Sub LoadMap()
            Try
                Me.m_XmlMap = New XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + Me.m_XmlMapPath)
            Catch xmlEx As XmlException
                '//TODO: Handle Exception here, depends on exception handling mechanism
                Throw xmlEx
            End Try
        End Sub

        Private Sub PrepareMap()
            Try
                While (Me.m_XmlMap.Read())
                    If (Me.m_XmlMap.NodeType = XmlNodeType.Element) Then
                        If (Me.m_XmlMap.AttributeCount > 0) Then
                            Dim domainType As String = Me.m_XmlMap.GetAttribute(Me.m_XmlDomainAttribute)
                            Dim mapperType As String = Me.m_XmlMap.GetAttribute(Me.m_XmlMapperAttribute)
                            Me.m_MapTable.Add(domainType, mapperType)
                        End If
                    End If
                End While

            Catch xmlEx As XmlException
                '//TODO: Handle Exception here, depends on exception handling mechanism
                Throw xmlEx
            Finally
                Me.m_XmlMap.Close()
            End Try
        End Sub

        Private Function MapperType(ByVal domainType As String) As String
            Return Me.m_MapTable(domainType).ToString()
        End Function

#End Region

    End Class

End Namespace
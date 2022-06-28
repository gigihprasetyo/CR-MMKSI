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
'// Generated on 8/3/2005 - 9:48:00 AM
'//
'// ===========================================================================		
#End Region

#Region ".NET Namespace"

Imports System
Imports System.Collections

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.Caching
Imports Microsoft.Practices.EnterpriseLibrary.Caching.Expirations

#End Region

Namespace KTB.DNet.BusinessFacade

    Public MustInherit Class AbstractFacade

#Region "Private variables"
        Private _DomainTypeCollection As ArrayList = New ArrayList
        Private m_CacheManager As CacheManager = CacheFactory.GetCacheManager
#End Region

#Region "Protecteds"

        Protected ReadOnly Property DomainTypeCollection() As ArrayList
            Get
                Return _DomainTypeCollection
            End Get
        End Property

        Protected Sub SetTaskLocking()
            For Each domainType As Object In _DomainTypeCollection
                m_CacheManager.Add(GetTableName(CType(domainType, Type)), True, CacheItemPriority.Normal, Nothing, New AbsoluteTime(DateTime.Now.AddMinutes(2)))
            Next
        End Sub

        Protected Sub RemoveTaskLocking()
            For Each domainType As Object In _DomainTypeCollection
                m_CacheManager.Remove(GetTableName(CType(domainType, Type)))
            Next
        End Sub

        Protected Function IsTaskFree() As Boolean
            For Each domainType As Object In _DomainTypeCollection
                If m_CacheManager(GetTableName(CType(domainType, Type))) <> Nothing Then
                    Return False
                End If
            Next
            Return True
        End Function

#End Region

#Region "Private Methods"

        Private Function GetTableName(ByVal DomainType As Type) As String
            Dim tableInfoAttr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(DomainType, GetType(TableInfoAttribute)), TableInfoAttribute)
            If Not IsNothing(tableInfoAttr) Then
                Return tableInfoAttr.TableName
            Else
                Return String.Empty
            End If
        End Function

#End Region

    End Class

End Namespace
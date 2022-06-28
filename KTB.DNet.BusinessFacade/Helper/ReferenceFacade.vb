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
'// Copyright  2005
'// ---------------------
'// $History      : $
'// Generated on 10/12/2005 - 8:26:12 AM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Reflection

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.BusinessFacade.Helper

    Public Class ReferenceFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ReferencesMapper As IMapper

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_ReferencesMapper = MapperFactory.GetInstance().GetMapper(GetType(Reference).ToString)
        End Sub

#End Region

#Region "Retrieve"



        Public Function Retrieve(ByVal ID As Integer) As Reference
            Return CType(m_ReferencesMapper.Retrieve(ID), Reference)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ReferencesMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ReferencesMapper.RetrieveList
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Reference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _references As ArrayList = m_ReferencesMapper.RetrieveByCriteria(criterias)
            Return _references
        End Function

        Public Function RetrieveActiveList(ByVal TypeRef As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Reference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Reference), "Type", MatchType.Exact, TypeRef))
            Dim _references As ArrayList = m_ReferencesMapper.RetrieveByCriteria(criterias)
            Return _references
        End Function

        Public Function RetrieveActiveList(ByVal TypeRef As String, ByVal CodeRef As String) As Reference
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Reference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Reference), "Type", MatchType.Exact, TypeRef))
            criterias.opAnd(New Criteria(GetType(Reference), "Code", MatchType.Exact, CodeRef))
            Dim _references As ArrayList = m_ReferencesMapper.RetrieveByCriteria(criterias)
            If _references.Count = 0 Then
                Return New Reference
            End If
            Return CType(_references(0), Reference)
        End Function

#End Region

#Region "Transaction"
        Public Function Update(ByVal objDomain As Reference) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ReferencesMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function
#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

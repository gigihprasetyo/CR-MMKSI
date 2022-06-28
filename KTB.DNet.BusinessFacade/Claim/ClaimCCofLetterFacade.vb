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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 11:10:52 AM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"
Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
#End Region

#Region "Custom Namespace"

Imports KTb.DNet.Domain
Imports KTb.DNet.Domain.Search
Imports KTb.DNet.DataMapper.Framework
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.Claim
    Public Class ClaimCCofLetterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ClaimCCofLetterMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_ClaimCCofLetterMapper = MapperFactory.GetInstance.GetMapper(GetType(ClaimCCofLetter).ToString)
        End Sub

#End Region

#Region "Retrieve"
        Public Function ValidateCC(ByVal CCName As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimCCofLetter), "CCList", MatchType.Exact, CCName))
            Dim agg As Aggregate = New Aggregate(GetType(ClaimCCofLetter), "CCList", AggregateType.Count)
            Return CType(m_ClaimCCofLetterMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As ClaimCCofLetter
            Return CType(m_ClaimCCofLetterMapper.Retrieve(ID), ClaimCCofLetter)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
      ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimCCofLetter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ClaimCCofLetter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ClaimCCofLetterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimCCofLetter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Return m_ClaimCCofLetterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ClaimCCofLetterColl As ArrayList = m_ClaimCCofLetterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ClaimCCofLetterColl
        End Function
#End Region

#Region "Transaction/Other Public Method"
        Public Function Insert(ByVal objDomain As ClaimCCofLetter) As Integer
            Dim iReturn As Integer = -2
            Try
                m_ClaimCCofLetterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As ClaimCCofLetter) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ClaimCCofLetterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub DeleteFromDB(ByVal objDomain As ClaimCCofLetter)
            Try
                m_ClaimCCofLetterMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub
#End Region
    End Class
End Namespace




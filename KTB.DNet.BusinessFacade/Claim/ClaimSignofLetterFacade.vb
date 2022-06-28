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
    Public Class ClaimSignofLetterFacade
        Inherits AbstractFacade
#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ClaimSignofLetterMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_ClaimSignofLetterMapper = MapperFactory.GetInstance.GetMapper(GetType(ClaimSignofLetter).ToString)
        End Sub

#End Region

#Region "Retrieve"
        Public Function Retrieve(ByVal ID As Integer) As ClaimSignofLetter
            Return CType(m_ClaimSignofLetterMapper.Retrieve(ID), ClaimSignofLetter)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
      ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimSignofLetter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ClaimSignofLetter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ClaimSignofLetterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimSignofLetter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Return m_ClaimSignofLetterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ClaimSignofLetterColl As ArrayList = m_ClaimSignofLetterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ClaimSignofLetterColl
        End Function


#End Region

#Region "Transaction/Other Public Method"
        Public Function Insert(ByVal objDomain As ClaimSignofLetter) As Integer
            Dim iReturn As Integer = -2
            Try
                m_ClaimSignofLetterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As ClaimSignofLetter) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ClaimSignofLetterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub DeleteFromDB(ByVal objDomain As ClaimSignofLetter)
            Try
                m_ClaimSignofLetterMapper.Delete(objDomain)
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

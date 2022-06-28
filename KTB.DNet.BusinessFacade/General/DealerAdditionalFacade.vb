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
'// Generated on 7/16/2007 - 11:09:45 AM
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

Namespace KTB.DNet.BusinessFacade.General
    Public Class DealerAdditionalFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DealerAdditionalMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_DealerAdditionalMapper = MapperFactory.GetInstance.GetMapper(GetType(DealerAdditional).ToString)
        End Sub

#End Region

#Region "Retrieve"
        Public Function Retrieve(ByVal ID As Integer) As DealerAdditional
            Return CType(m_DealerAdditionalMapper.Retrieve(ID), DealerAdditional)
        End Function

        Public Function Retrieve(ByVal DealerID As String, ByVal Status As Boolean) As DealerAdditional
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerAdditional), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerAdditional), "Dealer.ID", MatchType.Exact, CType(DealerID, Integer)))

            Dim DealerAdditionalColl As ArrayList = m_DealerAdditionalMapper.RetrieveByCriteria(criterias)
            If (DealerAdditionalColl.Count > 0) Then
                Return CType(DealerAdditionalColl(0), DealerAdditional)
            End If
            Return New DealerAdditional
        End Function

        Public Function Retrieve(ByVal DealerCode As String) As DealerAdditional
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerAdditional), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerAdditional), "Dealer.DealerCode", MatchType.Exact, DealerCode))

            Dim DealerAdditionalColl As ArrayList = m_DealerAdditionalMapper.RetrieveByCriteria(criterias)
            If (DealerAdditionalColl.Count > 0) Then
                Return CType(DealerAdditionalColl(0), DealerAdditional)
            End If
            Return New DealerAdditional
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
     ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerAdditional), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerAdditional), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DealerAdditionalMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerAdditional), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim DealerAdditionalColl As ArrayList = m_DealerAdditionalMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DealerAdditionalColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, sortColl As SortCollection) As ArrayList
            
            Dim DealerAdditionalColl As ArrayList = m_DealerAdditionalMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DealerAdditionalColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DealerAdditionalColl As ArrayList = m_DealerAdditionalMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DealerAdditionalColl
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DealerAdditionalMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DealerAdditionalMapper.RetrieveByCriteria(criterias, sorts)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As DealerAdditional) As Integer
            Dim iReturn As Integer = -2
            Try
                m_DealerAdditionalMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Sub DeleteFromDB(ByVal objDomain As DealerAdditional)
            Try
                m_DealerAdditionalMapper.Delete(objDomain)

            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function Update(ByVal objDomain As DealerAdditional) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DealerAdditionalMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function ValidateCode(ByVal DealerID As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerAdditional), "Dealer.ID", MatchType.Exact, DealerID))
            Dim agg As Aggregate = New Aggregate(GetType(DealerAdditional), "ID", AggregateType.Count)
            Return CType(m_DealerAdditionalMapper.RetrieveScalar(agg, crit), Integer)
        End Function
#End Region

#Region "Custom Method"

        Public Function RetrieveByDealerID(ByVal DealerID As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerAdditional), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerAdditional), "Dealer.ID", MatchType.Exact, DealerID))
            Return m_DealerAdditionalMapper.RetrieveByCriteria(criterias, Nothing, 1, 1, 1)
        End Function

#End Region


    End Class
End Namespace


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
'// Generated on 11/14/2005 - 10:42:45 AM
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

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.PO
    Public Class v_ProposedPOFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_v_ProposedPOMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_v_ProposedPOMapper = MapperFactory.GetInstance.GetMapper(GetType(v_ProposedPO).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As v_ProposedPO
            Return CType(m_v_ProposedPOMapper.Retrieve(ID), v_ProposedPO)
        End Function

        Public Function Retrieve(ByVal Code As String) As v_ProposedPO
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_ProposedPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(v_ProposedPO), "Code", MatchType.Exact, Code))

            Dim v_ProposedPOColl As ArrayList = m_v_ProposedPOMapper.RetrieveByCriteria(criterias)
            If (v_ProposedPOColl.Count > 0) Then
                Return CType(v_ProposedPOColl(0), v_ProposedPO)
            End If
            Return New v_ProposedPO
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_v_ProposedPOMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_v_ProposedPOMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_v_ProposedPOMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(v_ProposedPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_v_ProposedPOMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(v_ProposedPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_v_ProposedPOMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_ProposedPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _v_ProposedPO As ArrayList = m_v_ProposedPOMapper.RetrieveByCriteria(criterias)
            Return _v_ProposedPO
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_ProposedPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim v_ProposedPOColl As ArrayList = m_v_ProposedPOMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return v_ProposedPOColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(v_ProposedPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_v_ProposedPOMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(v_ProposedPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_ProposedPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim v_ProposedPOColl As ArrayList = m_v_ProposedPOMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return v_ProposedPOColl
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(v_ProposedPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim v_ProposedPOColl As ArrayList = m_v_ProposedPOMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return v_ProposedPOColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(v_ProposedPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim v_ProposedPOColl As ArrayList = m_v_ProposedPOMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return v_ProposedPOColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim v_ProposedPOColl As ArrayList = m_v_ProposedPOMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return v_ProposedPOColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_ProposedPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim v_ProposedPOColl As ArrayList = m_v_ProposedPOMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(v_ProposedPO), columnName, matchOperator, columnValue))
            Return v_ProposedPOColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(v_ProposedPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_ProposedPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(v_ProposedPO), columnName, matchOperator, columnValue))

            Return m_v_ProposedPOMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_ProposedPO), "ClassCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(v_ProposedPO), "ClassCode", AggregateType.Count)
            Return CType(m_v_ProposedPOMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As v_ProposedPO) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_v_ProposedPOMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As v_ProposedPO) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_v_ProposedPOMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As v_ProposedPO) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_v_ProposedPOMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                nResult = objDomain.ID
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
            Return nResult
        End Function
        Public Sub DeleteFromDB(ByVal objDomain As v_ProposedPO)
            Try
                m_v_ProposedPOMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace




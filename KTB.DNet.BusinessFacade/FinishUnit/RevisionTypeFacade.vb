#Region "Code Disclaimer"
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
'// Copyright  2018
'// ---------------------
'// $History      : $
'// Generated on 1/23/2018 - 8:44:11 AM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System.Security.Principal

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class RevisionTypeFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_RevisionTypeMapper As IMapper

        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_RevisionTypeMapper = MapperFactory.GetInstance.GetMapper(GetType(RevisionType).ToString)

            Me.m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Short) As RevisionType
            Return CType(m_RevisionTypeMapper.Retrieve(ID), RevisionType)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_RevisionTypeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_RevisionTypeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_RevisionTypeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RevisionTypeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RevisionTypeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _RevisionType As ArrayList = m_RevisionTypeMapper.RetrieveByCriteria(criterias)
            Return _RevisionType
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RevisionTypeColl As ArrayList = m_RevisionTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return RevisionTypeColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim RevisionTypeColl As ArrayList = m_RevisionTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return RevisionTypeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RevisionType), columnName, matchOperator, columnValue))
            Dim RevisionTypeColl As ArrayList = m_RevisionTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return RevisionTypeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionType), columnName, matchOperator, columnValue))

            Return m_RevisionTypeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As RevisionType) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_RevisionTypeMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As RevisionType) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_RevisionTypeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As RevisionType)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_RevisionTypeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As RevisionType)
            Try
                m_RevisionTypeMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function RetrieveByCode(ByVal code As String) As RevisionType
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RevisionType), "RevisionCode", MatchType.Exact, code))
            Dim RevisionTypeColl As ArrayList = m_RevisionTypeMapper.RetrieveByCriteria(criterias)

            If (RevisionTypeColl.Count > 0) Then
                Return CType(RevisionTypeColl(0), RevisionType)
            End If
            Return New RevisionType
        End Function
#End Region

    End Class

End Namespace


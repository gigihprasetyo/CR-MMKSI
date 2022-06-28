
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
'// Copyright  2017
'// ---------------------
'// $History      : $
'// Generated on 12/12/2017 - 1:46:39 PM
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

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Framework
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

namespace KTB.DNET.BusinessFacade

	public class MSPRegistrationHistoryFacade
		inherits AbstractFacade

#Region "Private Variables"

	Private m_userPrincipal As IPrincipal = Nothing
	Private m_MSPRegistrationHistoryMapper as IMapper
	
	Private	m_TransactionManager As TransactionManager
	
#end region

#region "Constructor"

Public Sub New(ByVal userPrincipal As IPrincipal)

	Me.m_userPrincipal = userPrincipal
	me.m_MSPRegistrationHistoryMapper = MapperFactory.GetInstance.GetMapper(GetType(MSPRegistrationHistory).ToString)
	
		
End Sub

#end region

#Region "Retrieve"

       Public Function Retrieve(ByVal ID as integer ) As MSPRegistrationHistory
            Return CType(m_MSPRegistrationHistoryMapper.Retrieve(ID), MSPRegistrationHistory)
       End Function
        
        
        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MSPRegistrationHistoryMapper.RetrieveByCriteria(criterias)
        End Function
        
        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MSPRegistrationHistoryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

		Public Function RetrieveList() As ArrayList
            Return m_MSPRegistrationHistoryMapper.RetrieveList
        End Function
        
        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPRegistrationHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
           End If

            Return m_MSPRegistrationHistoryMapper.RetrieveList(sortColl)
        End Function
        
        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPRegistrationHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

			Return m_MSPRegistrationHistoryMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function
		
		Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPRegistrationHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MSPRegistrationHistory As ArrayList = m_MSPRegistrationHistoryMapper.RetrieveByCriteria(criterias)
            Return _MSPRegistrationHistory
        End Function

		Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPRegistrationHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MSPRegistrationHistoryColl As ArrayList = m_MSPRegistrationHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

			Return MSPRegistrationHistoryColl
        End Function

		Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MSPRegistrationHistoryColl As ArrayList = m_MSPRegistrationHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MSPRegistrationHistoryColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(MSPRegistrationHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim MSPRegistrationHistoryColl As ArrayList = m_MSPRegistrationHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MSPRegistrationHistoryColl

        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPRegistrationHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MSPRegistrationHistoryColl As ArrayList = m_MSPRegistrationHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(MSPRegistrationHistory), columnName, matchOperator, columnValue))
            Return MSPRegistrationHistoryColl
        End Function

		Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPRegistrationHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPRegistrationHistory), columnName, matchOperator, columnValue))

            Return m_MSPRegistrationHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#end Region

#Region "Transaction/Other Public Method"

		
		Public Function Insert(ByVal objDomain As  MSPRegistrationHistory) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_MSPRegistrationHistoryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As MSPRegistrationHistory) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_MSPRegistrationHistoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As MSPRegistrationHistory)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_MSPRegistrationHistoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As MSPRegistrationHistory)
            Try
                m_MSPRegistrationHistoryMapper.Delete(objDomain)
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
		
	end class
	
end namespace


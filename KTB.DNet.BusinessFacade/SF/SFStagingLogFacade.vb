
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
'// Copyright  2018
'// ---------------------
'// $History      : $
'// Generated on 03/02/2018 - 11:26:16 AM
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

	public class SFStagingLogFacade
		inherits AbstractFacade

#Region "Private Variables"

	Private m_userPrincipal As IPrincipal = Nothing
	Private m_SFStagingLogMapper as IMapper
	
	Private	m_TransactionManager As TransactionManager
	
#end region

#region "Constructor"

Public Sub New(ByVal userPrincipal As IPrincipal)

	Me.m_userPrincipal = userPrincipal
	me.m_SFStagingLogMapper = MapperFactory.GetInstance.GetMapper(GetType(SFStagingLog).ToString)
	
		
End Sub

#end region

#Region "Retrieve"

       Public Function Retrieve(ByVal ID as integer ) As SFStagingLog
            Return CType(m_SFStagingLogMapper.Retrieve(ID), SFStagingLog)
       End Function
        
        Public Function Retrieve(ByVal Code As String) As SFStagingLog
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFStagingLog), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SFStagingLog), "SFStagingLogCode", MatchType.Exact, Code))

			Dim SFStagingLogColl As ArrayList = m_SFStagingLogMapper.RetrieveByCriteria(criterias)
            If (SFStagingLogColl.Count > 0) Then
                Return CType(SFStagingLogColl(0), SFStagingLog)
            End If
            Return New SFStagingLog
        End Function
        
        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SFStagingLogMapper.RetrieveByCriteria(criterias)
        End Function
        
        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SFStagingLogMapper.RetrieveByCriteria(criterias, sorts)
        End Function

		Public Function RetrieveList() As ArrayList
            Return m_SFStagingLogMapper.RetrieveList
        End Function
        
        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SFStagingLog), sortColumn, sortDirection))
            Else
                sortColl = Nothing
           End If

            Return m_SFStagingLogMapper.RetrieveList(sortColl)
        End Function
        
        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SFStagingLog), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

			Return m_SFStagingLogMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function
		
		Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFStagingLog), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SFStagingLog As ArrayList = m_SFStagingLogMapper.RetrieveByCriteria(criterias)
            Return _SFStagingLog
        End Function

		Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFStagingLog), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SFStagingLogColl As ArrayList = m_SFStagingLogMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

			Return SFStagingLogColl
        End Function
		
		 Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SFStagingLog), SortColumn, sortDirection))
            Dim  SFStagingLogColl As ArrayList = m_SFStagingLogMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
           Return SFStagingLogColl
        End Function
		

		Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SFStagingLogColl As ArrayList = m_SFStagingLogMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SFStagingLogColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFStagingLog), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SFStagingLogColl As ArrayList = m_SFStagingLogMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SFStagingLog), columnName, matchOperator, columnValue))
            Return SFStagingLogColl
        End Function

		Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SFStagingLog), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFStagingLog), columnName, matchOperator, columnValue))

            Return m_SFStagingLogMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#end Region

#Region "Transaction/Other Public Method"

		Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFStagingLog), "SFStagingLogCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SFStagingLog), "SFStagingLogCode", AggregateType.Count)
            Return CType(m_SFStagingLogMapper.RetrieveScalar(agg, crit), Integer)
        End Function
		
		Public Function Insert(ByVal objDomain As  SFStagingLog) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SFStagingLogMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SFStagingLog) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SFStagingLogMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SFStagingLog)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SFStagingLogMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SFStagingLog)
            Try
                m_SFStagingLogMapper.Delete(objDomain)
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


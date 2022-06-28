
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
'// Generated on 8/25/2017 - 2:50:17 PM
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

	public class CompetitorFacade
		inherits AbstractFacade

#Region "Private Variables"

	Private m_userPrincipal As IPrincipal = Nothing
	Private m_CompetitorMapper as IMapper
	
	Private	m_TransactionManager As TransactionManager
	
#end region

#region "Constructor"

Public Sub New(ByVal userPrincipal As IPrincipal)

	Me.m_userPrincipal = userPrincipal
	me.m_CompetitorMapper = MapperFactory.GetInstance.GetMapper(GetType(Competitor).ToString)
	
		
End Sub

#end region

#Region "Retrieve"

       Public Function Retrieve(ByVal ID as integer ) As Competitor
            Return CType(m_CompetitorMapper.Retrieve(ID), Competitor)
       End Function
        
        
        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_CompetitorMapper.RetrieveByCriteria(criterias)
        End Function
        
        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_CompetitorMapper.RetrieveByCriteria(criterias, sorts)
        End Function

		Public Function RetrieveList() As ArrayList
            Return m_CompetitorMapper.RetrieveList
        End Function
        
        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Competitor), sortColumn, sortDirection))
            Else
                sortColl = Nothing
           End If

            Return m_CompetitorMapper.RetrieveList(sortColl)
        End Function
        
        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Competitor), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

			Return m_CompetitorMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function
		
		Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Competitor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(Competitor), "Sequence", Sort.SortDirection.ASC))
            Dim _Competitor As ArrayList = m_CompetitorMapper.RetrieveByCriteria(criterias, sortColl)
            Return _Competitor
        End Function

		Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Competitor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CompetitorColl As ArrayList = m_CompetitorMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

			Return CompetitorColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            If IsNothing(Criterias) Then
                Criterias = New CriteriaComposite(New Criteria(GetType(Competitor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            End If

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Competitor), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim CompetitorColl As ArrayList = m_CompetitorMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return CompetitorColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
          ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Competitor), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_CompetitorMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
               ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Competitor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Competitor), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CompetitorMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

		Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim CompetitorColl As ArrayList = m_CompetitorMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return CompetitorColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim CompetitorColl As ArrayList = m_CompetitorMapper.RetrieveByCriteria(criterias)
            Return CompetitorColl
        End Function

        Public Function GetCompetitorByName(ByVal name As String) As Competitor
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Competitor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Competitor), "Name", MatchType.Exact, name))
            Dim CompetitorColl As ArrayList = m_CompetitorMapper.RetrieveByCriteria(criterias)

            Return CType(CompetitorColl(0), Competitor)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(Competitor), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim CompetitorColl As ArrayList = m_CompetitorMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return CompetitorColl

        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Competitor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CompetitorColl As ArrayList = m_CompetitorMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(Competitor), columnName, matchOperator, columnValue))
            Return CompetitorColl
        End Function

		Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Competitor), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Competitor), columnName, matchOperator, columnValue))

            Return m_CompetitorMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#end Region

#Region "Transaction/Other Public Method"

		
		Public Function Insert(ByVal objDomain As  Competitor) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_CompetitorMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As Competitor) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_CompetitorMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As Competitor)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_CompetitorMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As Competitor) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_CompetitorMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function
        
#End Region

#Region "Custom Method"
        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Competitor), "Code", MatchType.Exact, Code))
            crit.opAnd(New Criteria(GetType(Competitor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim agg As Aggregate = New Aggregate(GetType(Competitor), "Code", AggregateType.Count)

            Return CType(m_CompetitorMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateSequence(ByVal code As String, ByVal sequence As String, Optional isCreate As Boolean = True) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Competitor), "Sequence", MatchType.Exact, sequence))
            crit.opAnd(New Criteria(GetType(Competitor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If (isCreate = False) Then
                crit.opAnd(New Criteria(GetType(Competitor), "Code", MatchType.No, code))
            End If

            Dim agg As Aggregate = New Aggregate(GetType(Competitor), "Sequence", AggregateType.Count)

            Return CType(m_CompetitorMapper.RetrieveScalar(agg, crit), Integer)
        End Function
#End Region
		
	end class
	
end namespace


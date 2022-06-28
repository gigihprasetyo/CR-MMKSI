
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
'// Copyright  2019
'// ---------------------
'// $History      : $
'// Generated on 05/23/2019 - 1:54:56 PM
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

	public class BabitSpecialProvinceFacade
		inherits AbstractFacade

#Region "Private Variables"

	Private m_userPrincipal As IPrincipal = Nothing
	Private m_BabitSpecialProvinceMapper as IMapper
	
	Private	m_TransactionManager As TransactionManager
	
#end region

#region "Constructor"

Public Sub New(ByVal userPrincipal As IPrincipal)

	Me.m_userPrincipal = userPrincipal
	me.m_BabitSpecialProvinceMapper = MapperFactory.GetInstance.GetMapper(GetType(BabitSpecialProvince).ToString)
	
		
End Sub

#end region

#Region "Retrieve"

       Public Function Retrieve(ByVal ID as integer ) As BabitSpecialProvince
            Return CType(m_BabitSpecialProvinceMapper.Retrieve(ID), BabitSpecialProvince)
       End Function
        
        Public Function Retrieve(ByVal Code As String) As BabitSpecialProvince
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialProvince), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitSpecialProvince), "BabitSpecialProvinceCode", MatchType.Exact, Code))

			Dim BabitSpecialProvinceColl As ArrayList = m_BabitSpecialProvinceMapper.RetrieveByCriteria(criterias)
            If (BabitSpecialProvinceColl.Count > 0) Then
                Return CType(BabitSpecialProvinceColl(0), BabitSpecialProvince)
            End If
            Return New BabitSpecialProvince
        End Function
        
        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitSpecialProvinceMapper.RetrieveByCriteria(criterias)
        End Function
        
        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BabitSpecialProvinceMapper.RetrieveByCriteria(criterias, sorts)
        End Function

		Public Function RetrieveList() As ArrayList
            Return m_BabitSpecialProvinceMapper.RetrieveList
        End Function
        
        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitSpecialProvince), sortColumn, sortDirection))
            Else
                sortColl = Nothing
           End If

            Return m_BabitSpecialProvinceMapper.RetrieveList(sortColl)
        End Function
        
        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitSpecialProvince), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

			Return m_BabitSpecialProvinceMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function
		
		Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialProvince), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BabitSpecialProvince As ArrayList = m_BabitSpecialProvinceMapper.RetrieveByCriteria(criterias)
            Return _BabitSpecialProvince
        End Function

		Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialProvince), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitSpecialProvinceColl As ArrayList = m_BabitSpecialProvinceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

			Return BabitSpecialProvinceColl
        End Function
		
		 Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(BabitSpecialProvince), SortColumn, sortDirection))
            Dim  BabitSpecialProvinceColl As ArrayList = m_BabitSpecialProvinceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
           Return BabitSpecialProvinceColl
        End Function
		

		Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BabitSpecialProvinceColl As ArrayList = m_BabitSpecialProvinceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BabitSpecialProvinceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialProvince), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitSpecialProvinceColl As ArrayList = m_BabitSpecialProvinceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BabitSpecialProvince), columnName, matchOperator, columnValue))
            Return BabitSpecialProvinceColl
        End Function

		Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitSpecialProvince), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialProvince), columnName, matchOperator, columnValue))

            Return m_BabitSpecialProvinceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#end Region

#Region "Transaction/Other Public Method"

		Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialProvince), "BabitSpecialProvinceCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BabitSpecialProvince), "BabitSpecialProvinceCode", AggregateType.Count)
            Return CType(m_BabitSpecialProvinceMapper.RetrieveScalar(agg, crit), Integer)
        End Function
		
		Public Function Insert(ByVal objDomain As  BabitSpecialProvince) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_BabitSpecialProvinceMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As BabitSpecialProvince) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BabitSpecialProvinceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As BabitSpecialProvince)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BabitSpecialProvinceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As BabitSpecialProvince)
            Try
                m_BabitSpecialProvinceMapper.Delete(objDomain)
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


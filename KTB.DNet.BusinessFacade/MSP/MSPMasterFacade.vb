
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
'// Generated on 12/6/2017 - 10:37:11 AM
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

	public class MSPMasterFacade
		inherits AbstractFacade

#Region "Private Variables"

	Private m_userPrincipal As IPrincipal = Nothing
	Private m_MSPMasterMapper as IMapper
	
	Private	m_TransactionManager As TransactionManager
	
#end region

#region "Constructor"

Public Sub New(ByVal userPrincipal As IPrincipal)

	Me.m_userPrincipal = userPrincipal
	me.m_MSPMasterMapper = MapperFactory.GetInstance.GetMapper(GetType(MSPMaster).ToString)
	
		
End Sub

#end region

#Region "Retrieve"

       Public Function Retrieve(ByVal ID as integer ) As MSPMaster
            Return CType(m_MSPMasterMapper.Retrieve(ID), MSPMaster)
       End Function
        
        
        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MSPMasterMapper.RetrieveByCriteria(criterias)
        End Function
        
        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MSPMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

		Public Function RetrieveList() As ArrayList
            Return m_MSPMasterMapper.RetrieveList
        End Function
        
        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
           End If

            Return m_MSPMasterMapper.RetrieveList(sortColl)
        End Function
        
        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

			Return m_MSPMasterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function
		
		Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MSPMaster As ArrayList = m_MSPMasterMapper.RetrieveByCriteria(criterias)
            Return _MSPMaster
        End Function

		Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MSPMasterColl As ArrayList = m_MSPMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

			Return MSPMasterColl
        End Function

		Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MSPMasterColl As ArrayList = m_MSPMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MSPMasterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MSPMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(MSPMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim MSPMasterColl As ArrayList = m_MSPMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MSPMasterColl

        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MSPMasterColl As ArrayList = m_MSPMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(MSPMaster), columnName, matchOperator, columnValue))
            Return MSPMasterColl
        End Function

		Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPMaster), columnName, matchOperator, columnValue))

            Return m_MSPMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#end Region

#Region "Transaction/Other Public Method"

		
		Public Function Insert(ByVal objDomain As  MSPMaster) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_MSPMasterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As MSPMaster) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_MSPMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As MSPMaster)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_MSPMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As MSPMaster)
            Try
                m_MSPMasterMapper.Delete(objDomain)
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


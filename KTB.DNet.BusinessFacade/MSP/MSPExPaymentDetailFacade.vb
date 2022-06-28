
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
'// Copyright  2020
'// ---------------------
'// $History      : $
'// Generated on 11/10/2020 - 4:05:33 PM
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

	public class MSPExPaymentDetailFacade

#Region "Private Variables"

	Private m_userPrincipal As IPrincipal = Nothing
	Private m_MSPExPaymentDetailMapper as IMapper
	
	
#end region

#region "Constructor"

Public Sub New(ByVal userPrincipal As IPrincipal)

	Me.m_userPrincipal = userPrincipal
	me.m_MSPExPaymentDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(MSPExPaymentDetail).ToString)
	
		
End Sub

#end region

#Region "Retrieve"

       Public Function Retrieve(ByVal ID as integer ) As MSPExPaymentDetail
            Return CType(m_MSPExPaymentDetailMapper.Retrieve(ID), MSPExPaymentDetail)
       End Function
        
        Public Function Retrieve(ByVal Header As MSPExPayment) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MSPExPaymentDetail), "MSPExPayment.ID", MatchType.Exact, Header.ID))

            Dim MSPExPaymentDetailColl As ArrayList = m_MSPExPaymentDetailMapper.RetrieveByCriteria(criterias)
            If (MSPExPaymentDetailColl.Count > 0) Then
                Return MSPExPaymentDetailColl
            End If
            Return New ArrayList
        End Function
        
        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MSPExPaymentDetailMapper.RetrieveByCriteria(criterias)
        End Function
        
        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MSPExPaymentDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

		Public Function RetrieveList() As ArrayList
            Return m_MSPExPaymentDetailMapper.RetrieveList
        End Function
        
        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPExPaymentDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
           End If

            Return m_MSPExPaymentDetailMapper.RetrieveList(sortColl)
        End Function
        
        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPExPaymentDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

			Return m_MSPExPaymentDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function
		
		Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MSPExPaymentDetail As ArrayList = m_MSPExPaymentDetailMapper.RetrieveByCriteria(criterias)
            Return _MSPExPaymentDetail
        End Function

		Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MSPExPaymentDetailColl As ArrayList = m_MSPExPaymentDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

			Return MSPExPaymentDetailColl
        End Function
		
		 Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(MSPExPaymentDetail), SortColumn, sortDirection))
            Dim  MSPExPaymentDetailColl As ArrayList = m_MSPExPaymentDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
           Return MSPExPaymentDetailColl
        End Function
		

		Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MSPExPaymentDetailColl As ArrayList = m_MSPExPaymentDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MSPExPaymentDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MSPExPaymentDetailColl As ArrayList = m_MSPExPaymentDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(MSPExPaymentDetail), columnName, matchOperator, columnValue))
            Return MSPExPaymentDetailColl
        End Function

		Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPExPaymentDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExPaymentDetail), columnName, matchOperator, columnValue))

            Return m_MSPExPaymentDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#end Region

#Region "Transaction/Other Public Method"

		Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExPaymentDetail), "MSPExPaymentDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(MSPExPaymentDetail), "MSPExPaymentDetailCode", AggregateType.Count)
            Return CType(m_MSPExPaymentDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function
		
		Public Function Insert(ByVal objDomain As  MSPExPaymentDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_MSPExPaymentDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As MSPExPaymentDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_MSPExPaymentDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As MSPExPaymentDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_MSPExPaymentDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As MSPExPaymentDetail)
            Try
                m_MSPExPaymentDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub
        
#End Region

#Region "Custom Method"
        Public Function RetrieveID(ByVal CodeID As Integer) As MSPExPaymentDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MSPExPaymentDetail), "MSPExRegistration.ID", MatchType.Exact, CodeID))

            Dim MSPExPaymentDetailColl As ArrayList = m_MSPExPaymentDetailMapper.RetrieveByCriteria(criterias)
            If (MSPExPaymentDetailColl.Count > 0) Then
                Return CType(MSPExPaymentDetailColl(0), MSPExPaymentDetail)
            End If
            Return New MSPExPaymentDetail
        End Function
#End Region
		
	end class
	
end namespace


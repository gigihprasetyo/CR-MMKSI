#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2008

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
'// Copyright  2008
'// ---------------------
'// $History      : $
'// Generated on 12/1/2008 - 16:07:00 PM
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

Imports KTB.Dnet.Domain
Imports KTB.Dnet.Domain.Search
Imports KTB.Dnet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit

    Public Class ParkingFeeReturnDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ParkingFeeReturnDetailMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_ParkingFeeReturnDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.ParkingFeeReturnDetail).ToString)
            Me.m_TransactionManager = New TransactionManager
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.ParkingFeeReturnDetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ParkingFeeReturnDetail
            Return CType(m_ParkingFeeReturnDetailMapper.Retrieve(ID), ParkingFeeReturnDetail)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ParkingFeeReturnDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveScalar(ByVal criterias As ICriteria, ByVal agg As Aggregate) As Object
            Return m_ParkingFeeReturnDetailMapper.RetrieveScalar(agg, criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ParkingFeeReturnDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ParkingFeeReturnDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(ParkingFeeReturnDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_ParkingFeeReturnDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(ParkingFeeReturnDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_ParkingFeeReturnDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeReturnDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ParkingFeeReturnDetail As ArrayList = m_ParkingFeeReturnDetailMapper.RetrieveByCriteria(criterias)
            Return _ParkingFeeReturnDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeReturnDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ParkingFeeReturnDetailColl As ArrayList = m_ParkingFeeReturnDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ParkingFeeReturnDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ParkingFeeReturnDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ParkingFeeReturnDetailColl As ArrayList = m_ParkingFeeReturnDetailMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ParkingFeeReturnDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
              ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeReturnDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ParkingFeeReturnDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ParkingFeeReturnDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ParkingFeeReturnDetailColl As ArrayList = m_ParkingFeeReturnDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ParkingFeeReturnDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeReturnDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ParkingFeeReturnDetailColl As ArrayList = m_ParkingFeeReturnDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(ParkingFeeReturnDetail), columnName, matchOperator, columnValue))
            Return ParkingFeeReturnDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(ParkingFeeReturnDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeReturnDetail), columnName, matchOperator, columnValue))

            Return m_ParkingFeeReturnDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As ParkingFeeReturnDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_ParkingFeeReturnDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Sub Delete(ByVal objDomain As ParkingFeeReturnDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_ParkingFeeReturnDetailMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
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

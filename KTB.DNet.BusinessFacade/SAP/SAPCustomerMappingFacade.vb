
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
'// Generated on 10/07/2019 - 10:11:23
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

Namespace KTB.DNET.BusinessFacade

    Public Class SAPCustomerMappingFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SAPCustomerMappingMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SAPCustomerMappingMapper = MapperFactory.GetInstance.GetMapper(GetType(SAPCustomerMapping).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SAPCustomerMapping
            Return CType(m_SAPCustomerMappingMapper.Retrieve(ID), SAPCustomerMapping)
        End Function

        Public Function Retrieve(ByVal NoPengajuan As String) As SAPCustomerMapping
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomerMapping), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SAPCustomerMapping), "NoPengajuan", MatchType.Exact, NoPengajuan))

            Dim SAPCustomerMappingColl As ArrayList = m_SAPCustomerMappingMapper.RetrieveByCriteria(criterias)
            If (SAPCustomerMappingColl.Count > 0) Then
                Return CType(SAPCustomerMappingColl(0), SAPCustomerMapping)
            End If
            Return New SAPCustomerMapping
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SAPCustomerMappingMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SAPCustomerMappingMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SAPCustomerMappingMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SAPCustomerMapping), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SAPCustomerMappingMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SAPCustomerMapping), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SAPCustomerMappingMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomerMapping), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SAPCustomerMapping As ArrayList = m_SAPCustomerMappingMapper.RetrieveByCriteria(criterias)
            Return _SAPCustomerMapping
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomerMapping), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SAPCustomerMappingColl As ArrayList = m_SAPCustomerMappingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SAPCustomerMappingColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SAPCustomerMapping), SortColumn, sortDirection))
            Dim SAPCustomerMappingColl As ArrayList = m_SAPCustomerMappingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SAPCustomerMappingColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SAPCustomerMappingColl As ArrayList = m_SAPCustomerMappingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SAPCustomerMappingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomerMapping), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SAPCustomerMappingColl As ArrayList = m_SAPCustomerMappingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SAPCustomerMapping), columnName, matchOperator, columnValue))
            Return SAPCustomerMappingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SAPCustomerMapping), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomerMapping), columnName, matchOperator, columnValue))

            Return m_SAPCustomerMappingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomerMapping), "SAPCustomerMappingCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SAPCustomerMapping), "SAPCustomerMappingCode", AggregateType.Count)
            Return CType(m_SAPCustomerMappingMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SAPCustomerMapping) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SAPCustomerMappingMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SAPCustomerMapping) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SAPCustomerMappingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SAPCustomerMapping)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SAPCustomerMappingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SAPCustomerMapping)
            Try
                m_SAPCustomerMappingMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SAPCustomerMapping), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim TrCourseColl As ArrayList = m_SAPCustomerMappingMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrCourseColl
        End Function

        Public Function GetCurrentSalesmanHeader(ByVal phone As String, ByVal dealerID As Integer) As Integer
            Dim arrParam As ArrayList = New ArrayList()
            Dim param1 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@Phone", phone)
            arrParam.Add(param1)
            Dim param2 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@DealerID", dealerID)
            arrParam.Add(param2)

            Dim dataTable As DataTable = m_SAPCustomerMappingMapper.RetrieveDataSet("up_GetCurrentSalesmanDSE", arrParam).Tables(0)

            Return CInt(dataTable.Rows(0)(0))
        End Function

        Public Function InsertFromLead(ByVal objDomain As SAPCustomerMapping) As Integer
            Dim iReturn As Integer = -2
            Try
                Dim objDomainOld As New SAPCustomerMapping()
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomerMapping), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SAPCustomerMapping), "SAPCustomer.ID", MatchType.Exact, objDomain.SAPCustomer.ID))

                Dim SAPCustomerMappingColl As ArrayList = m_SAPCustomerMappingMapper.RetrieveByCriteria(criterias)
                If (SAPCustomerMappingColl.Count > 0) Then
                    objDomainOld = CType(SAPCustomerMappingColl(0), SAPCustomerMapping)
                    objDomainOld.SourceInformation = objDomain.SourceInformation
                    objDomainOld.SourceLead = objDomain.SourceLead
                    iReturn = m_SAPCustomerMappingMapper.Update(objDomainOld, m_userPrincipal.Identity.Name)
                Else
                    iReturn = m_SAPCustomerMappingMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
                End If


            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function
#End Region

    End Class

End Namespace


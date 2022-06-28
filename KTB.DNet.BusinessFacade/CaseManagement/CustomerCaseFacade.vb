
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
'// Generated on 7/10/2017 - 10:28:00 AM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Collections.Generic

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class CustomerCaseFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_CustomerCaseMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_CustomerCaseMapper = MapperFactory.GetInstance.GetMapper(GetType(CustomerCase).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As CustomerCase
            Return CType(m_CustomerCaseMapper.Retrieve(ID), CustomerCase)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_CustomerCaseMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_CustomerCaseMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_CustomerCaseMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerCase), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CustomerCaseMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerCase), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CustomerCaseMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerCase), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _CustomerCase As ArrayList = m_CustomerCaseMapper.RetrieveByCriteria(criterias)
            Return _CustomerCase
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerCase), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CustomerCaseColl As ArrayList = m_CustomerCaseMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CustomerCaseColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerCase), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CustomerCaseMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim CustomerCaseColl As ArrayList = m_CustomerCaseMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return CustomerCaseColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerCase), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CustomerCaseColl As ArrayList = m_CustomerCaseMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(CustomerCase), columnName, matchOperator, columnValue))
            Return CustomerCaseColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerCase), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerCase), columnName, matchOperator, columnValue))

            Return m_CustomerCaseMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As CustomerCase) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_CustomerCaseMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As CustomerCase) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_CustomerCaseMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As CustomerCase)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_CustomerCaseMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As CustomerCase)
            Try
                m_CustomerCaseMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function RetrieveSp(str As String) As DataSet
            Return m_CustomerCaseMapper.RetrieveDataSet(str)
        End Function

        Public Function RetrievePhoneNumber(ID As Integer) As ArrayList
            Dim str = "exec up_CustomerCaseRetrievePhoneNumber " & ID
            Dim result As New ArrayList
            Dim rawDS As DataSet = m_CustomerCaseMapper.RetrieveDataSet(str)
            If rawDS.Tables.Count > 0 Then
                Dim rawDT As DataTable = rawDS.Tables(0)
                If rawDT.Rows.Count > 0 Then
                    For Each r As DataRow In rawDT.Rows
                        result.Add(CType(r(0), String))
                    Next
                End If
            End If
            Return result
        End Function

        Public Function GetBodyMessage(ByVal customerCaseID As Integer) As Dictionary(Of String, String)
            Dim arrParam As ArrayList = New ArrayList()
            Dim result As New Dictionary(Of String, String)
            Dim param1 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@CustomerCaseID", customerCaseID)
            arrParam.Add(param1)

            Dim dsResult As DataSet = m_CustomerCaseMapper.RetrieveDataSet("up_GetBodymessageFromCustomerCase", arrParam)
            result.Add("BodyMessage", dsResult.Tables(0).Rows(0)(0).ToString())
            result.Add("ClientID", dsResult.Tables(1).Rows(0)(0).ToString())
            result.Add("Username", dsResult.Tables(1).Rows(0)(1).ToString())
            result.Add("Password", dsResult.Tables(1).Rows(0)(2).ToString())

            Return result
        End Function
#End Region

    End Class

End Namespace


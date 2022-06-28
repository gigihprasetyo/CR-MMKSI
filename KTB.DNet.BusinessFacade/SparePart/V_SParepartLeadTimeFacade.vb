
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
'// Copyright  2016
'// ---------------------
'// $History      : $
'// Generated on 9/9/2016 - 2:48:57 PM
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

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class V_SParepartLeadTimeFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_V_SParepartLeadTimeMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_V_SParepartLeadTimeMapper = MapperFactory.GetInstance.GetMapper(GetType(V_SParepartLeadTime).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Short) As V_SParepartLeadTime
            Return CType(m_V_SParepartLeadTimeMapper.Retrieve(ID), V_SParepartLeadTime)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_V_SParepartLeadTimeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_V_SParepartLeadTimeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_V_SParepartLeadTimeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_SParepartLeadTime), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_SParepartLeadTimeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_SParepartLeadTime), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_SParepartLeadTimeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveList(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_SParepartLeadTime), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim V_SParepartLeadTimeColl As ArrayList = m_V_SParepartLeadTimeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return V_SParepartLeadTimeColl

        End Function

        Public Function RetrieveList(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_SParepartLeadTime), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim V_SParepartLeadTimeColl As ArrayList = m_V_SParepartLeadTimeMapper.RetrieveByCriteria(criterias, sortColl)
            Return V_SParepartLeadTimeColl

            'Return m_V_SParepartLeadTimeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        'Public Function RetrieveActiveList() As ArrayList
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SParepartLeadTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    Dim _V_SParepartLeadTime As ArrayList = m_V_SParepartLeadTimeMapper.RetrieveByCriteria(criterias)
        '    Return _V_SParepartLeadTime
        'End Function

        'Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SParepartLeadTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    Dim V_SParepartLeadTimeColl As ArrayList = m_V_SParepartLeadTimeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

        '    Return V_SParepartLeadTimeColl
        'End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim V_SParepartLeadTimeColl As ArrayList = m_V_SParepartLeadTimeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return V_SParepartLeadTimeColl
        End Function

        'Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SParepartLeadTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    Dim V_SParepartLeadTimeColl As ArrayList = m_V_SParepartLeadTimeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
        '    criterias.opAnd(New Criteria(GetType(V_SParepartLeadTime), columnName, matchOperator, columnValue))
        '    Return V_SParepartLeadTimeColl
        'End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_SParepartLeadTime), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SParepartLeadTime), columnName, matchOperator, columnValue))

            Return m_V_SParepartLeadTimeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As V_SParepartLeadTime) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_V_SParepartLeadTimeMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As V_SParepartLeadTime) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_V_SParepartLeadTimeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        'Public Sub Delete(ByVal objDomain As V_SParepartLeadTime)
        '    Dim nResult As Integer = -1
        '    Try
        '        nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
        '        m_V_SParepartLeadTimeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
        '    Catch ex As Exception
        '        nResult = -1
        '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
        '        If rethrow Then
        '            Throw
        '        End If
        '    End Try
        'End Sub

        Public Sub DeleteFromDB(ByVal objDomain As V_SParepartLeadTime)
            Try
                m_V_SParepartLeadTimeMapper.Delete(objDomain)
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

    End Class

End Namespace


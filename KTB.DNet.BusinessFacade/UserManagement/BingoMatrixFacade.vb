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
'// Copyright  2005
'// ---------------------
'// $History      : $
'// Generated on 9/26/2005 - 1:07:25 PM
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

Namespace KTB.Dnet.BusinessFacade.UserManagement

    Public Class BingoMatrixFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BingoMatrixMapper As IMapper

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BingoMatrixMapper = MapperFactory.GetInstance.GetMapper(GetType(BingoMatrix).ToString)

        End Sub

        Public Sub New(ByVal userPrincipal As IPrincipal, ByVal instanceName As String)

            Me.m_userPrincipal = userPrincipal
            Me.m_BingoMatrixMapper = MapperFactory.GetInstance.GetMapper(GetType(BingoMatrix).ToString, instanceName)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BingoMatrix
            Return CType(m_BingoMatrixMapper.Retrieve(ID), BingoMatrix)
        End Function

        Public Function Retrieve(ByVal Code As String) As BingoMatrix
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BingoMatrix), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BingoMatrix), "BingoMatrixCode", MatchType.Exact, Code))

            Dim BingoMatrixColl As ArrayList = m_BingoMatrixMapper.RetrieveByCriteria(criterias)
            If (BingoMatrixColl.Count > 0) Then
                Return CType(BingoMatrixColl(0), BingoMatrix)
            End If
            Return New BingoMatrix
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BingoMatrixMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BingoMatrixMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BingoMatrix), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_BingoMatrixMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BingoMatrixMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(BingoMatrix), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BingoMatrixMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(BingoMatrix), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BingoMatrixMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BingoMatrix), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BingoMatrix As ArrayList = m_BingoMatrixMapper.RetrieveByCriteria(criterias)
            Return _BingoMatrix
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BingoMatrix), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BingoMatrixColl As ArrayList = m_BingoMatrixMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BingoMatrixColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BingoMatrixColl As ArrayList = m_BingoMatrixMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BingoMatrixColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BingoMatrix), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BingoMatrixColl As ArrayList = m_BingoMatrixMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BingoMatrix), columnName, matchOperator, columnValue))
            Return BingoMatrixColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(BingoMatrix), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BingoMatrix), columnName, matchOperator, columnValue))

            Return m_BingoMatrixMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As BingoMatrix) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_BingoMatrixMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Sub Update(ByVal objDomain As BingoMatrix)
            Try
                m_BingoMatrixMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Update(ByVal ArrList As ArrayList)
            Try
                For Each objDomain As BingoMatrix In ArrList
                    m_BingoMatrixMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                Next
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Delete(ByVal objDomain As BingoMatrix)
            Try
                m_BingoMatrixMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BingoMatrix), "BingoMatrixCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BingoMatrix), "BingoMatrixCode", AggregateType.Count)
            Return CType(m_BingoMatrixMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace
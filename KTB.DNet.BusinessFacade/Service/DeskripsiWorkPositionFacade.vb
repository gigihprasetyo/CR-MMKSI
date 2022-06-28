 
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
'// Generated on 9/27/2005 - 2:35:23 PM
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
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.Service

    Public Class DeskripsiWorkPositionFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DescriptionWorkCodeMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DescriptionWorkCodeMapper = MapperFactory.GetInstance.GetMapper(GetType(DeskripsiKodeKerja).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DeskripsiKodeKerja
            Return CType(m_DescriptionWorkCodeMapper.Retrieve(ID), DeskripsiKodeKerja)
        End Function

        Public Function Retrieve(ByVal Code As String) As DeskripsiKodeKerja
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeskripsiKodeKerja), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DeskripsiKodeKerja), "KodeKerja", MatchType.Exact, Code))

            Dim DeskripsiKodeKerjaColl As ArrayList = m_DescriptionWorkCodeMapper.RetrieveByCriteria(criterias)
            If (DeskripsiKodeKerjaColl.Count > 0) Then
                Return CType(DeskripsiKodeKerjaColl(0), DeskripsiKodeKerja)
            End If
            Return New DeskripsiKodeKerja
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DescriptionWorkCodeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DescriptionWorkCodeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DescriptionWorkCodeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeskripsiKodeKerja), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DescriptionWorkCodeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal criteria As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeskripsiKodeKerja), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DescriptionWorkCodeMapper.RetrieveByCriteria(criteria, sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeskripsiKodeKerja), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DescriptionWorkCodeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeskripsiKodeKerja), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DeskripsiKodeKerja As ArrayList = m_DescriptionWorkCodeMapper.RetrieveByCriteria(criterias)
            Return _DeskripsiKodeKerja
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeskripsiKodeKerja), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DeskripsiKodeKerjaColl As ArrayList = m_DescriptionWorkCodeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DeskripsiKodeKerjaColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeskripsiKodeKerja), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeskripsiKodeKerja), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DescriptionWorkCodeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
      ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeskripsiKodeKerja), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DescriptionWorkCodeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeskripsiKodeKerja), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DeskripsiKodeKerjaColl As ArrayList = m_DescriptionWorkCodeMapper.RetrieveByCriteria(Criterias, sortColl)
            Return DeskripsiKodeKerjaColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DeskripsiKodeKerjaColl As ArrayList = m_DescriptionWorkCodeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DeskripsiKodeKerjaColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeskripsiKodeKerja), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DeskripsiKodeKerjaColl As ArrayList = m_DescriptionWorkCodeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DeskripsiKodeKerja), columnName, matchOperator, columnValue))
            Return DeskripsiKodeKerjaColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeskripsiKodeKerja), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeskripsiKodeKerja), columnName, matchOperator, columnValue))

            Return m_DescriptionWorkCodeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"
        Public Function Insert(ByVal objDomain As DeskripsiKodeKerja) As Integer
            Dim iReturn As Integer = -2
            Try
                m_DescriptionWorkCodeMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DeskripsiKodeKerja) As Integer
            Dim nResult As Integer = -2
            Try
                m_DescriptionWorkCodeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                Else

                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DeskripsiKodeKerja)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DescriptionWorkCodeMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DeskripsiKodeKerja)
            Try
                m_DescriptionWorkCodeMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeskripsiKodeKerja), "KodeKerja", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(DeskripsiKodeKerja), "KodeKerja", AggregateType.Count)
            Return CType(m_DescriptionWorkCodeMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"
        Public Function DoRetrieveDataset(ByVal strSql As String) As DataSet
            Dim ds As New DataSet()
            ds = m_DescriptionWorkCodeMapper.RetrieveDataSet(strSql)
            Return ds
        End Function
#End Region

    End Class

End Namespace



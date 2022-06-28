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
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/3/2005 - 10:53:00 AM
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

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data.SqlClient
Imports System.Collections.Generic


#End Region

Namespace KTB.DNet.BusinessFacade.Service
    Public Class FlatRateMasterFSFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_FlatRateMasterFSMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager



#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_FlatRateMasterFSMapper = MapperFactory.GetInstance().GetMapper(GetType(FlatRateMasterFS).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(FlatRateMasterFS))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As FlatRateMasterFS
            Return CType(m_FlatRateMasterFSMapper.Retrieve(ID), FlatRateMasterFS)
        End Function

        Public Function Retrieve(ByVal ID As String) As FlatRateMasterFS
            Return CType(m_FlatRateMasterFSMapper.Retrieve(Convert.ToInt32(ID)), FlatRateMasterFS)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_FlatRateMasterFSMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_FlatRateMasterFSMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_FlatRateMasterFSMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FlatRateMasterFS), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FlatRateMasterFSMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FlatRateMasterFS), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FlatRateMasterFSMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FlatRateMasterFS), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _FlatRateMasterFS As ArrayList = m_FlatRateMasterFSMapper.RetrieveByCriteria(criterias)
            Return _FlatRateMasterFS
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FlatRateMasterFS), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FlatRateMasterFSColl As ArrayList = m_FlatRateMasterFSMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FlatRateMasterFSColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FlatRateMasterFS), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim FlatRateMasterFSColl As ArrayList = m_FlatRateMasterFSMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return FlatRateMasterFSColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal criterias As CriteriaComposite) As ArrayList


            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FlatRateMasterFS), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FlatRateMasterFSMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim FlatRateMasterFSColl As ArrayList = m_FlatRateMasterFSMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FlatRateMasterFSColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(FlatRateMasterFS), sortColumn, sortDirection))
            Else
                'sortColl = Nothing
                sortColl.Add(New Sort(GetType(FlatRateMasterFS), "ID", Sort.SortDirection.DESC))

            End If

            Dim FlatRateMasterFSColl As ArrayList = m_FlatRateMasterFSMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return FlatRateMasterFSColl
        End Function


        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FlatRateMasterFS), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FlatRateMasterFS), columnName, matchOperator, columnValue))
            Dim FlatRateMasterFSColl As ArrayList = m_FlatRateMasterFSMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FlatRateMasterFSColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FlatRateMasterFS), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FlatRateMasterFS), columnName, matchOperator, columnValue))

            Return m_FlatRateMasterFSMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveScalar(ByVal crit As CriteriaComposite, ByVal aggr As Aggregate) As Integer
            Return m_FlatRateMasterFSMapper.RetrieveScalar(aggr, crit)
        End Function
#End Region

#Region "Transaction/Other Public Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is FlatRateMasterFS) Then
                CType(InsertArg.DomainObject, FlatRateMasterFS).ID = InsertArg.ID
                CType(InsertArg.DomainObject, FlatRateMasterFS).MarkLoaded()
            End If
        End Sub

        Public Function Insert(ByVal objDomain As FlatRateMasterFS) As Integer
            Dim iReturn As Integer = -2
            Try
                m_FlatRateMasterFSMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As FlatRateMasterFS) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_FlatRateMasterFSMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function


        Public Sub Delete(ByVal objDomain As FlatRateMasterFS)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_FlatRateMasterFSMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As FlatRateMasterFS)
            Try
                m_FlatRateMasterFSMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal sID As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FlatRateMasterFS), "ID", MatchType.Exact, sID))
            Dim agg As Aggregate = New Aggregate(GetType(FlatRateMasterFS), "ID", AggregateType.Count)

            Return CType(m_FlatRateMasterFSMapper.RetrieveScalar(agg, crit), Integer)
        End Function
#End Region

#Region "Custom Method"
        Public Function Insert(ByVal objDomains As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            Try
                Dim performTransaction As Boolean = True
                Dim ObjMapper As IMapper
                Dim objDomain As FlatRateMasterFS
                _user = m_userPrincipal.Identity.Name

                For Each obj As FlatRateMasterFS In objDomains
                    If obj.ID = 0 Then
                        objDomain = obj
                        m_TransactionManager.AddInsert(objDomain, _user)
                    Else
                        m_TransactionManager.AddUpdate(obj, _user)
                    End If
                Next

                If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try

            Return returnValue

        End Function

        Public Function GetFSKIND() As KTB.DNet.Domain.FlatRateMasterFS
            Try
                Dim spName As String
                Dim Param As New List(Of SqlClient.SqlParameter)

                spName = "sp_GetFSKind"
                Dim lststallmaster = m_FlatRateMasterFSMapper.RetrieveSP(spName, New ArrayList(Param))
                If (lststallmaster.Count > 0) Then
                    Return CType(lststallmaster(0), KTB.DNet.Domain.FlatRateMasterFS)
                End If
                'Dim arrlist As ArrayList = CType(lststallmaster(0), ArrayList)
                'Dim strRunNum As String = ""
                'For Each item As String In arrlist
                '    strRunNum = item(2).ToString()
                'Next
                'Return lststallmaster
                'Return CType(lststallmaster(0), KTB.DNet.Domain.StallMaster)
                Return New KTB.DNet.Domain.FlatRateMasterFS
            Catch ex As Exception
                Dim ss As String = ex.Message()
            End Try

        End Function

#End Region

    End Class

End Namespace


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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 2:31:06 PM
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

Namespace KTB.DNet.BusinessFacade.Salesman

    Public Class SalesmanAreaAssignFacade

        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SalesmanAreaAssignMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SalesmanAreaAssignMapper = MapperFactory.GetInstance.GetMapper(GetType(SalesmanAreaAssign).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SalesmanAreaAssign
            Return CType(m_SalesmanAreaAssignMapper.Retrieve(ID), SalesmanAreaAssign)
        End Function

        Public Function Retrieve(ByVal Code As String) As SalesmanAreaAssign
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanAreaAssign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanAreaAssign), "SalesmanAreaAssignCode", MatchType.Exact, Code))

            Dim SalesmanAreaAssignColl As ArrayList = m_SalesmanAreaAssignMapper.RetrieveByCriteria(criterias)
            If (SalesmanAreaAssignColl.Count > 0) Then
                Return CType(SalesmanAreaAssignColl(0), SalesmanAreaAssign)
            End If
            Return New SalesmanAreaAssign
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SalesmanAreaAssignMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SalesmanAreaAssignMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SalesmanAreaAssignMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanAreaAssign), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesmanAreaAssignMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanAreaAssign), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesmanAreaAssignMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanAreaAssign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SalesmanAreaAssign As ArrayList = m_SalesmanAreaAssignMapper.RetrieveByCriteria(criterias)
            Return _SalesmanAreaAssign
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanAreaAssign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesmanAreaAssignColl As ArrayList = m_SalesmanAreaAssignMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SalesmanAreaAssignColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(SalesmanAreaAssign), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim SalesmanAreaAssignColl As ArrayList = m_SalesmanAreaAssignMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SalesmanAreaAssignColl


            'Dim SalesmanAreaAssignColl As ArrayList = m_SalesmanAreaAssignMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            'Return SalesmanAreaAssignColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList

            ' modify code for sorting

            Dim SalesmanAreaAssignColl As ArrayList = m_SalesmanAreaAssignMapper.RetrieveByCriteria(criterias)
            Return SalesmanAreaAssignColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanAreaAssign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesmanAreaAssignColl As ArrayList = m_SalesmanAreaAssignMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SalesmanAreaAssign), columnName, matchOperator, columnValue))
            Return SalesmanAreaAssignColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanAreaAssign), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanAreaAssign), columnName, matchOperator, columnValue))

            Return m_SalesmanAreaAssignMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"





#End Region

#Region "Need To Add"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanAreaAssign), "AreaCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SalesmanAreaAssign), "AreaCode", AggregateType.Count)
            Return CType(m_SalesmanAreaAssignMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function InsertTransaction(ByVal objSalesManHeader As SalesmanHeader, ByVal arlSalesmanArea As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arlSalesmanArea.Count > 0 Then
                        For Each item As SalesmanArea In arlSalesmanArea
                            Dim objSalesmanAreaAssign As SalesmanAreaAssign = New SalesmanAreaAssign
                            objSalesmanAreaAssign.SalesmanHeader = objSalesManHeader
                            objSalesmanAreaAssign.SalesmanArea = item
                            m_TransactionManager.AddInsert(objSalesmanAreaAssign, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = 0
                Catch ex As Exception
                    returnValue = -1
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Function Insert(ByVal objDomain As SalesmanAreaAssign) As Integer
            Dim iReturn As Integer = -2
            Try
                m_SalesmanAreaAssignMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As SalesmanAreaAssign) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SalesmanAreaAssignMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SalesmanAreaAssign)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SalesmanAreaAssignMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As SalesmanAreaAssign) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_SalesmanAreaAssignMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is SalesmanAreaAssign) Then
                CType(InsertArg.DomainObject, SalesmanAreaAssign).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SalesmanAreaAssign).MarkLoaded()

            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace


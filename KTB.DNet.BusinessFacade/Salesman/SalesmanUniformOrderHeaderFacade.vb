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
'// Generated on 8/7/2007 - 6:05:14 PM
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

    Public Class SalesmanUniformOrderHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SalesmanUniformOrderHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SalesmanUniformOrderHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(SalesmanUniformOrderHeader).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(SalesmanUniformOrderHeader))
            Me.DomainTypeCollection.Add(GetType(SalesmanUniformOrderDetail))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SalesmanUniformOrderHeader
            Return CType(m_SalesmanUniformOrderHeaderMapper.Retrieve(ID), SalesmanUniformOrderHeader)
        End Function

        'Public Function Retrieve(ByVal Code As String) As SalesmanUniformOrderHeader
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformOrderHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "SalesmanUniformOrderHeaderCode", MatchType.Exact, Code))

        '    Dim SalesmanUniformOrderHeaderColl As ArrayList = m_SalesmanUniformOrderHeaderMapper.RetrieveByCriteria(criterias)
        '    If (SalesmanUniformOrderHeaderColl.Count > 0) Then
        '        Return CType(SalesmanUniformOrderHeaderColl(0), SalesmanUniformOrderHeader)
        '    End If
        '    Return New SalesmanUniformOrderHeader
        'End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SalesmanUniformOrderHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SalesmanUniformOrderHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SalesmanUniformOrderHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanUniformOrderHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesmanUniformOrderHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanUniformOrderHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesmanUniformOrderHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformOrderHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SalesmanUniformOrderHeader As ArrayList = m_SalesmanUniformOrderHeaderMapper.RetrieveByCriteria(criterias)
            Return _SalesmanUniformOrderHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformOrderHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesmanUniformOrderHeaderColl As ArrayList = m_SalesmanUniformOrderHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SalesmanUniformOrderHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SalesmanUniformOrderHeader), SortColumn, sortDirection))

            Dim SalesmanUniformOrderHeaderColl As ArrayList = m_SalesmanUniformOrderHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SalesmanUniformOrderHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SalesmanUniformOrderHeaderColl As ArrayList = m_SalesmanUniformOrderHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SalesmanUniformOrderHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformOrderHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesmanUniformOrderHeaderColl As ArrayList = m_SalesmanUniformOrderHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), columnName, matchOperator, columnValue))
            Return SalesmanUniformOrderHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanUniformOrderHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformOrderHeader), columnName, matchOperator, columnValue))

            Return m_SalesmanUniformOrderHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformOrderHeader), "SalesmanUniformOrderHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SalesmanUniformOrderHeader), "SalesmanUniformOrderHeaderCode", AggregateType.Count)
            Return CType(m_SalesmanUniformOrderHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function InsertTransaction(ByVal objHeader As SalesmanUniformOrderHeader, ByVal arldetail As ArrayList, ByVal arlToDelete As ArrayList, ByVal arlSalesmanUniformAssigned As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If objHeader.ID = 0 Then
                        m_TransactionManager.AddInsert(objHeader, m_userPrincipal.Identity.Name)
                    Else
                        'm_TransactionManager.AddUpdate(objHeader, m_userPrincipal.Identity.Name)
                    End If

                    For Each itemdetail As SalesmanUniformOrderDetail In arlToDelete
                        If itemdetail.ID > 0 Then
                            m_TransactionManager.AddDelete(itemdetail)
                        End If
                    Next

                    For Each itemdetail As SalesmanUniformOrderDetail In arldetail
                        If itemdetail.ID = 0 Then
                            m_TransactionManager.AddInsert(itemdetail, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddUpdate(itemdetail, m_userPrincipal.Identity.Name)
                        End If
                    Next

                    For Each itemAssigned As SalesmanUniformAssigned In arlSalesmanUniformAssigned
                        m_TransactionManager.AddUpdate(itemAssigned, m_userPrincipal.Identity.Name)
                    Next

                    m_TransactionManager.PerformTransaction()
                    returnValue = objHeader.ID
                Catch ex As Exception
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

        Public Function Update(ByVal objDomain As KTB.DNet.Domain.SalesmanUniformOrderHeader) As Integer
            Dim iReturn As Integer = -1
            Try
                m_SalesmanUniformOrderHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                iReturn = objDomain.ID
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal arrHeader As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrHeader.Count > 0 Then
                        For Each objHeader As SalesmanUniformOrderHeader In arrHeader
                            m_TransactionManager.AddUpdate(objHeader, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = 1
                Catch ex As Exception
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

        Public Function UpdateSalesmanUniformOrderHeader(ByVal objDomain As KTB.DNet.Domain.SalesmanUniformOrderHeader, ByVal arrDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    'm_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If arrDetail.Count > 0 Then
                        For Each objDetail As SalesmanUniformOrderDetail In arrDetail
                            'If IsNothing(New SalesmanUniformOrderDetailFacade(Me.m_userPrincipal).ValidateItem(objDomain.ID, objDetail.UniformSize)) Then
                            m_TransactionManager.AddUpdate(objDetail, m_userPrincipal.Identity.Name)
                            'End If
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                Catch ex As Exception
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

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is SalesmanUniformOrderHeader) Then
                CType(InsertArg.DomainObject, SalesmanUniformOrderHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SalesmanUniformOrderHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is SalesmanUniformOrderDetail) Then
                CType(InsertArg.DomainObject, SalesmanUniformOrderDetail).ID = InsertArg.ID
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace


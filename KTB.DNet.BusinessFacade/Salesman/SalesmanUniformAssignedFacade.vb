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
'// Generated on 8/2/2007 - 4:36:26 PM
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

    Public Class SalesmanUniformAssignedFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SalesmanUniformAssignedMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SalesmanUniformAssignedMapper = MapperFactory.GetInstance.GetMapper(GetType(SalesmanUniformAssigned).ToString)

            Me.m_TransactionManager = New TransactionManager

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SalesmanUniformAssigned
            Return CType(m_SalesmanUniformAssignedMapper.Retrieve(ID), SalesmanUniformAssigned)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SalesmanUniformAssignedMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SalesmanUniformAssignedMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SalesmanUniformAssignedMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanUniformAssigned), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesmanUniformAssignedMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanUniformAssigned), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesmanUniformAssignedMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformAssigned), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SalesmanUniformAssigned As ArrayList = m_SalesmanUniformAssignedMapper.RetrieveByCriteria(criterias)
            Return _SalesmanUniformAssigned
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(SalesmanUniformAssigned), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformAssigned), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesmanUniformAssignedColl As ArrayList = m_SalesmanUniformAssignedMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SalesmanUniformAssignedColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformAssigned), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesmanUniformAssignedColl As ArrayList = m_SalesmanUniformAssignedMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SalesmanUniformAssignedColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SalesmanUniformAssignedColl As ArrayList = m_SalesmanUniformAssignedMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SalesmanUniformAssignedColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(SalesmanUniformAssigned), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim SalesmanUniformAssignedColl As ArrayList = m_SalesmanUniformAssignedMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SalesmanUniformAssignedColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformAssigned), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesmanUniformAssignedColl As ArrayList = m_SalesmanUniformAssignedMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), columnName, matchOperator, columnValue))
            Return SalesmanUniformAssignedColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanUniformAssigned), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformAssigned), columnName, matchOperator, columnValue))

            Return m_SalesmanUniformAssignedMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Delete(ByVal objDomain As SalesmanUniformAssigned) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_SalesmanUniformAssignedMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Insert(ByVal objDomain As SalesmanUniformAssigned) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SalesmanUniformAssignedMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                iReturn = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SalesmanUniformAssigned) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SalesmanUniformAssignedMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

#End Region

#Region "Custom Method"
        Public Function isExist(ByVal Salesman As SalesmanHeader, ByVal Uniform As SalesmanUniform) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformAssigned), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanHeader.ID", MatchType.Exact, Salesman.ID))
            criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanUniform.ID", MatchType.Exact, Uniform.ID))

            Dim SalesmanUniformAssignedColl As ArrayList = m_SalesmanUniformAssignedMapper.RetrieveByCriteria(criterias)
            If SalesmanUniformAssignedColl.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Function isExistInPeriod(ByVal Salesman As SalesmanHeader, ByVal Uniform As SalesmanUniform, ByVal intYear As Integer) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformAssigned), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanHeader.ID", MatchType.Exact, Salesman.ID))
            criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanUniform.ID", MatchType.Exact, Uniform.ID))
            criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "CreatedTime", MatchType.GreaterOrEqual, New Date(intYear, 1, 1)))
            criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "CreatedTime", MatchType.Lesser, New Date(intYear + 1, 1, 1)))

            Dim SalesmanUniformAssignedColl As ArrayList = m_SalesmanUniformAssignedMapper.RetrieveByCriteria(criterias)
            If SalesmanUniformAssignedColl.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Function AssignUniform(ByVal SalesmanList As ArrayList, ByVal Uniform As SalesmanUniform) As Integer
            Dim returnValue As Integer = -1
            Dim salesmanAssigned As SalesmanUniformAssigned
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    For Each item As SalesmanHeader In SalesmanList
                        salesmanAssigned = New SalesmanUniformAssigned
                        salesmanAssigned.SalesmanHeader = item
                        salesmanAssigned.SalesmanUniform = Uniform
                        m_TransactionManager.AddInsert(salesmanAssigned, m_userPrincipal.Identity.Name)
                    Next

                    m_TransactionManager.PerformTransaction()
                    returnValue = salesmanAssigned.ID
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

        Public Function AssignUniform(ByVal SalesmanList As ArrayList, ByVal UniformList As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim salesmanAssigned As SalesmanUniformAssigned
            If (Me.IsTaskFree()) Then
                Try

                    Me.SetTaskLocking()

                    For Each item As SalesmanHeader In SalesmanList
                        For Each itemUniform As SalesmanUniform In UniformList

                            If Not isExistInPeriod(item, itemUniform, Date.Today.Year) Then
                                salesmanAssigned = New SalesmanUniformAssigned
                                salesmanAssigned.SalesmanHeader = item
                                salesmanAssigned.SalesmanUniform = itemUniform
                                m_TransactionManager.AddInsert(salesmanAssigned, m_userPrincipal.Identity.Name)
                            End If

                        Next
                    Next

                    m_TransactionManager.PerformTransaction()
                    If IsNothing(salesmanAssigned) = False Then
                        returnValue = salesmanAssigned.ID
                    Else
                        returnValue = 1
                    End If
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

        Public Function RilisAssignUniform(ByVal UniformAssignList As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    For Each item As SalesmanUniformAssigned In UniformAssignList
                        item.IsReleased = 1
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next

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

#End Region

    End Class

End Namespace


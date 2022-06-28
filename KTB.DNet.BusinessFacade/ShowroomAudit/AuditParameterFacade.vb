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
'// Generated on 8/24/2007 - 3:10:15 PM
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
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.ShowroomAudit

    Public Class AuditParameterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_AuditParameterMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_AuditParameterMapper = MapperFactory.GetInstance.GetMapper(GetType(AuditParameter).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.AuditParameter))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.AuditParameterPhoto))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As AuditParameter
            Return CType(m_AuditParameterMapper.Retrieve(ID), AuditParameter)
        End Function

        Public Function Retrieve(ByVal Code As String) As AuditParameter
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AuditParameter), "Code", MatchType.Exact, Code))

            Dim AuditParameterColl As ArrayList = m_AuditParameterMapper.RetrieveByCriteria(criterias)
            If (AuditParameterColl.Count > 0) Then
                Return CType(AuditParameterColl(0), AuditParameter)
            End If
            Return New AuditParameter
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_AuditParameterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_AuditParameterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_AuditParameterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditParameter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AuditParameterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditParameter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AuditParameterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _AuditParameter As ArrayList = m_AuditParameterMapper.RetrieveByCriteria(criterias)
            Return _AuditParameter
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AuditParameterColl As ArrayList = m_AuditParameterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AuditParameterColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditParameter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AuditParameterColl As ArrayList = m_AuditParameterMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AuditParameterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim AuditParameterColl As ArrayList = m_AuditParameterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return AuditParameterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AuditParameterColl As ArrayList = m_AuditParameterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(AuditParameter), columnName, matchOperator, columnValue))
            Return AuditParameterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditParameter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditParameter), columnName, matchOperator, columnValue))

            Return m_AuditParameterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditParameter), "Code", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(AuditParameter), "Code", AggregateType.Count)
            Return CType(m_AuditParameterMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As AuditParameter) As Integer
            Dim returnVal As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If Not objDomain.AuditParameterPhotos Is Nothing Then
                        If objDomain.AuditParameterPhotos.Count > 0 Then
                            For Each item As AuditParameterPhoto In objDomain.AuditParameterPhotos
                                item.AuditParameter = objDomain
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnVal = objDomain.ID
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

            Return returnVal
        End Function

        Public Function Update(ByVal objDomain As AuditParameter) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_AuditParameterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Update(ByVal objDomain As AuditParameter, ByVal arlListPhoto As ArrayList) As Integer
            Dim returnVal As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper

                    If arlListPhoto.Count > 0 Then
                        For Each item As AuditParameterPhoto In arlListPhoto
                            item.AuditParameter = objDomain
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    Else
                        m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnVal = 1
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

            Return returnVal
        End Function

        Public Function UpdateTransaction(ByVal ArlDomain As ArrayList) As Integer
            Dim returnVal As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper
                    For Each item As AuditParameter In ArlDomain
                        item.IsRilis = 1
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnVal = 1
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

            Return returnVal
        End Function

        Public Function Delete(ByVal objDomain As AuditParameter) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = DBRowStatus.Deleted
                nResult = m_AuditParameterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.AuditParameter) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.AuditParameter).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.AuditParameter).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is AuditParameterPhoto) Then
                CType(InsertArg.DomainObject, AuditParameterPhoto).ID = InsertArg.ID
            End If
        End Sub
#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace


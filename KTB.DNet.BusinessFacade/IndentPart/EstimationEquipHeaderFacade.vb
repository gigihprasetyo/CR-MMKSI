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
'// Generated on 8/2/2007 - 12:59:07 PM
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

Namespace KTB.DNET.BusinessFacade.IndentPart


    Public Class EstimationEquipHeaderFacade
        Inherits AbstractFacade


#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_EstimationEquipHeaderMapper As IMapper
        'Private m_IndentPartPOHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_EstimationEquipHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(EstimationEquipHeader).ToString)
            'Me.m_IndentPartPOHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(IndentPartPOHeader).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(EstimationEquipHeader))
            Me.DomainTypeCollection.Add(GetType(IndentPartDetail))


        End Sub

#End Region

#Region "Retrieve"

        'Public Function RetrievePOHeader(ByVal ID As Integer) As IndentPartPOHeader
        '    Return CType(m_IndentPartPOHeaderMapper.Retrieve(ID), IndentPartPOHeader)
        'End Function

        Public Function Retrieve(ByVal ID As Integer) As EstimationEquipHeader
            Return CType(m_EstimationEquipHeaderMapper.Retrieve(ID), EstimationEquipHeader)
        End Function

        Public Function Retrieve(ByVal EstimationNumber As String) As EstimationEquipHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "EstimationNumber", MatchType.Exact, EstimationNumber))

            Dim EstimationEquipHeaderColl As ArrayList = m_EstimationEquipHeaderMapper.RetrieveByCriteria(criterias)
            If (EstimationEquipHeaderColl.Count > 0) Then
                Return CType(EstimationEquipHeaderColl(0), EstimationEquipHeader)
            End If
            Return New EstimationEquipHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_EstimationEquipHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_EstimationEquipHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_EstimationEquipHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EstimationEquipHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EstimationEquipHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EstimationEquipHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EstimationEquipHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _EstimationEquipHeader As ArrayList = m_EstimationEquipHeaderMapper.RetrieveByCriteria(criterias)
            Return _EstimationEquipHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EstimationEquipHeaderColl As ArrayList = m_EstimationEquipHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EstimationEquipHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EstimationEquipHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ClaimReasonColl As ArrayList = m_EstimationEquipHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ClaimReasonColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(EstimationEquipHeader), SortColumn, sortDirection))

            Dim EstimationEquipHeaderColl As ArrayList = m_EstimationEquipHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return EstimationEquipHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim EstimationEquipHeaderColl As ArrayList = m_EstimationEquipHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return EstimationEquipHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EstimationEquipHeaderColl As ArrayList = m_EstimationEquipHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), columnName, matchOperator, columnValue))
            Return EstimationEquipHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EstimationEquipHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipHeader), columnName, matchOperator, columnValue))

            Return m_EstimationEquipHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipHeader), "EstimationEquipHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(EstimationEquipHeader), "EstimationEquipHeaderCode", AggregateType.Count)
            Return CType(m_EstimationEquipHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function InsertEstimationEquipHeader(ByVal objDomain As KTB.DNet.Domain.EstimationEquipHeader, ByVal arrIPDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If arrIPDetail.Count > 0 Then
                        For Each objIPDetail As EstimationEquipDetail In arrIPDetail
                            objIPDetail.EstimationEquipHeader = objDomain
                            m_TransactionManager.AddInsert(objIPDetail, m_userPrincipal.Identity.Name)
                        Next
                    End If
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

        Public Function InsertEstimationEquipDetail(ByVal objDomain As KTB.DNet.Domain.EstimationEquipHeader, ByVal objIPDetail As EstimationEquipDetail) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objIPDetail, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = objIPDetail.ID
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

        Public Function UpdateEstimationEquipHeader(ByVal objDomain As KTB.DNet.Domain.EstimationEquipHeader, ByVal arrIPDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    'm_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If arrIPDetail.Count > 0 Then
                        For Each objIPDetail As EstimationEquipDetail In arrIPDetail
                            If IsNothing(New EstimationEquipDetailFacade(Me.m_userPrincipal).ValidateItem(objDomain.ID, objIPDetail.SparePartMaster.PartNumber)) Then
                                objIPDetail.EstimationEquipHeader = objDomain
                                m_TransactionManager.AddInsert(objIPDetail, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddUpdate(objIPDetail, m_userPrincipal.Identity.Name)
                            End If
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

        Public Function Update(ByVal arlIPH As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arlIPH.Count > 0 Then
                        For Each objIPHH As EstimationEquipHeader In arlIPH
                            m_TransactionManager.AddUpdate(objIPHH, m_userPrincipal.Identity.Name)
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

        Public Function Update(ByVal objDomain As EstimationEquipHeader) As Integer
            Dim nResult As Integer = -1
            Try
                Return m_EstimationEquipHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function Delete(ByVal objDomain As EstimationEquipHeader) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                Return m_EstimationEquipHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Sub DeleteFromDB(ByVal objDomain As EstimationEquipHeader)
            Try
                m_EstimationEquipHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteEstimationEquipHeader(ByVal objDomain As KTB.DNet.Domain.EstimationEquipHeader, ByVal arrIPDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arrIPDetail.Count > 0 Then
                        For Each objIPDetail As EstimationEquipDetail In arrIPDetail
                            m_TransactionManager.AddDelete(objIPDetail)
                        Next
                    End If

                    m_TransactionManager.AddDelete(objDomain)
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

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is EstimationEquipHeader) Then
                CType(InsertArg.DomainObject, EstimationEquipHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, EstimationEquipHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is EstimationEquipDetail) Then
                CType(InsertArg.DomainObject, EstimationEquipDetail).ID = InsertArg.ID
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace


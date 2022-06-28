
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
'// Copyright  2019
'// ---------------------
'// $History      : $
'// Generated on 12/09/2019 - 14:24:18
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

    Public Class BabitReportEventDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BabitReportEventDetailMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BabitReportEventDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(BabitReportEventDetail).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(BabitReportEventDetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BabitReportEventDetail
            Return CType(m_BabitReportEventDetailMapper.Retrieve(ID), BabitReportEventDetail)
        End Function

        Public Function Retrieve(ByVal Code As String) As BabitReportEventDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitReportEventDetail), "BabitReportEventDetailCode", MatchType.Exact, Code))

            Dim BabitReportEventDetailColl As ArrayList = m_BabitReportEventDetailMapper.RetrieveByCriteria(criterias)
            If (BabitReportEventDetailColl.Count > 0) Then
                Return CType(BabitReportEventDetailColl(0), BabitReportEventDetail)
            End If
            Return New BabitReportEventDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitReportEventDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BabitReportEventDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BabitReportEventDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitReportEventDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitReportEventDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitReportEventDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitReportEventDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BabitReportEventDetail As ArrayList = m_BabitReportEventDetailMapper.RetrieveByCriteria(criterias)
            Return _BabitReportEventDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitReportEventDetailColl As ArrayList = m_BabitReportEventDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BabitReportEventDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(BabitReportEventDetail), SortColumn, sortDirection))
            Dim BabitReportEventDetailColl As ArrayList = m_BabitReportEventDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BabitReportEventDetailColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BabitReportEventDetailColl As ArrayList = m_BabitReportEventDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BabitReportEventDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitReportEventDetailColl As ArrayList = m_BabitReportEventDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BabitReportEventDetail), columnName, matchOperator, columnValue))
            Return BabitReportEventDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitReportEventDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportEventDetail), columnName, matchOperator, columnValue))

            Return m_BabitReportEventDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportEventDetail), "BabitReportEventDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BabitReportEventDetail), "BabitReportEventDetailCode", AggregateType.Count)
            Return CType(m_BabitReportEventDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As BabitReportEventDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_BabitReportEventDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As BabitReportEventDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BabitReportEventDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As BabitReportEventDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BabitReportEventDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As BabitReportEventDetail)
            Try
                m_BabitReportEventDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is BabitReportHeader) Then
                CType(InsertArg.DomainObject, BabitReportHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, BabitReportHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is BabitReportEventDetail) Then
                CType(InsertArg.DomainObject, BabitReportEventDetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function InsertTransaction(ByVal objBabitReportHeader As BabitReportHeader, ByVal arrBabitReportDetail As ArrayList, ByVal arrBabitReportDoc As ArrayList, ByVal arrBabitReportEventAct As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objBabitReportHeader, m_userPrincipal.Identity.Name)

                    If Not IsNothing(arrBabitReportDoc) Then
                        If arrBabitReportDoc.Count > 0 Then
                            For Each oBabitReportDoc As BabitReportDocument In arrBabitReportDoc
                                oBabitReportDoc.BabitReportHeader = objBabitReportHeader
                                m_TransactionManager.AddInsert(oBabitReportDoc, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrBabitReportDetail) Then
                        If arrBabitReportDetail.Count > 0 Then
                            For Each oBabitReportDetail As BabitReportEventDetail In arrBabitReportDetail
                                oBabitReportDetail.BabitReportHeader = objBabitReportHeader
                                m_TransactionManager.AddInsert(oBabitReportDetail, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrBabitReportEventAct) Then
                        If arrBabitReportEventAct.Count > 0 Then
                            For Each oBabitReportDetail As BabitReportEventDetail In arrBabitReportEventAct
                                oBabitReportDetail.BabitReportHeader = objBabitReportHeader
                                m_TransactionManager.AddInsert(oBabitReportDetail, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = objBabitReportHeader.ID

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
        Function DeleteTransaction(ByVal objHeader As BabitReportHeader, ByVal arrEventDetail As ArrayList, ByVal arrDocument As ArrayList, ByVal arrDisplayCar As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If Not IsNothing(arrEventDetail) Then
                        If arrEventDetail.Count > 0 Then
                            For Each oPameranDetail As BabitReportEventDetail In arrEventDetail
                                m_TransactionManager.AddDelete(oPameranDetail)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrDocument) Then
                        If arrDocument.Count > 0 Then
                            For Each oDocument As BabitReportDocument In arrDocument
                                m_TransactionManager.AddDelete(oDocument)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrDisplayCar) Then
                        If arrDisplayCar.Count > 0 Then
                            For Each oDisplayTarget As BabitDisplayCar In arrDisplayCar
                                m_TransactionManager.AddDelete(oDisplayTarget)
                            Next
                        End If
                    End If

                    m_TransactionManager.AddDelete(objHeader)

                    m_TransactionManager.PerformTransaction()
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

        Function UpdateTransaction(_oBabitReportHeader As BabitReportHeader, arlEvent As ArrayList, arlDelEvent As ArrayList, arrBabitReportDocs As ArrayList, arlDelDocument As ArrayList, arrBabitReportEventAct As ArrayList, arlDeleteBabitReportAct As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not IsNothing(arlDelEvent) Then
                        If arlDelEvent.Count > 0 Then
                            For Each item As BabitReportEventDetail In arlDelEvent
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arlEvent) Then
                        If arlEvent.Count > 0 Then
                            For Each item As BabitReportEventDetail In arlEvent
                                item.BabitReportHeader = _oBabitReportHeader
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDelDocument) Then
                        If arlDelDocument.Count > 0 Then
                            For Each item As BabitReportDocument In arlDelDocument
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrBabitReportDocs) Then
                        If arrBabitReportDocs.Count > 0 Then
                            For Each item As BabitReportDocument In arrBabitReportDocs
                                item.BabitReportHeader = _oBabitReportHeader
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDeleteBabitReportAct) Then
                        If arlDeleteBabitReportAct.Count > 0 Then
                            For Each item As BabitReportEventDetail In arlDeleteBabitReportAct
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrBabitReportEventAct) Then
                        If arrBabitReportEventAct.Count > 0 Then
                            For Each item As BabitReportEventDetail In arrBabitReportEventAct
                                item.BabitReportHeader = _oBabitReportHeader
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    m_TransactionManager.AddUpdate(_oBabitReportHeader, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = _oBabitReportHeader.ID
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

#End Region

    End Class

End Namespace


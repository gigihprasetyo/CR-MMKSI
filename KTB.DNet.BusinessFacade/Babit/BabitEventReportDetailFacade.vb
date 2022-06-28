
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
'// Generated on 27/05/2019 - 9:55:31
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

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Framework
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class BabitEventReportDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BabitEventReportDetailMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BabitEventReportDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(BabitEventReportDetail).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(BabitEventReportDetail))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BabitEventReportDetail
            Return CType(m_BabitEventReportDetailMapper.Retrieve(ID), BabitEventReportDetail)
        End Function

        Public Function Retrieve(ByVal Code As String) As BabitEventReportDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitEventReportDetail), "BabitEventReportDetailCode", MatchType.Exact, Code))

            Dim BabitEventReportDetailColl As ArrayList = m_BabitEventReportDetailMapper.RetrieveByCriteria(criterias)
            If (BabitEventReportDetailColl.Count > 0) Then
                Return CType(BabitEventReportDetailColl(0), BabitEventReportDetail)
            End If
            Return New BabitEventReportDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitEventReportDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BabitEventReportDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BabitEventReportDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitEventReportDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitEventReportDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitEventReportDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitEventReportDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BabitEventReportDetail As ArrayList = m_BabitEventReportDetailMapper.RetrieveByCriteria(criterias)
            Return _BabitEventReportDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitEventReportDetailColl As ArrayList = m_BabitEventReportDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BabitEventReportDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(BabitEventReportDetail), SortColumn, sortDirection))
            Dim BabitEventReportDetailColl As ArrayList = m_BabitEventReportDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BabitEventReportDetailColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BabitEventReportDetailColl As ArrayList = m_BabitEventReportDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BabitEventReportDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitEventReportDetailColl As ArrayList = m_BabitEventReportDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BabitEventReportDetail), columnName, matchOperator, columnValue))
            Return BabitEventReportDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitEventReportDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportDetail), columnName, matchOperator, columnValue))

            Return m_BabitEventReportDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportDetail), "BabitEventReportDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BabitEventReportDetail), "BabitEventReportDetailCode", AggregateType.Count)
            Return CType(m_BabitEventReportDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As BabitEventReportDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_BabitEventReportDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As BabitEventReportDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BabitEventReportDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As BabitEventReportDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BabitEventReportDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As BabitEventReportDetail)
            Try
                m_BabitEventReportDetailMapper.Delete(objDomain)
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

            If (TypeOf InsertArg.DomainObject Is BabitEventReportHeader) Then
                CType(InsertArg.DomainObject, BabitEventReportHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, BabitEventReportHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is BabitEventReportDetail) Then
                CType(InsertArg.DomainObject, BabitEventReportDetail).ID = InsertArg.ID
            End If
        End Sub

        Function DeleteTransaction(ByVal objHeader As BabitEventReportHeader, ByVal arrBabitEventReportDetail As ArrayList, ByVal arrEventReportDoc As ArrayList, ByVal arrEventReportAct As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If Not IsNothing(arrBabitEventReportDetail) Then
                        If arrBabitEventReportDetail.Count > 0 Then
                            For Each oPameranDetail As BabitEventReportDetail In arrBabitEventReportDetail
                                m_TransactionManager.AddDelete(oPameranDetail)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrEventReportDoc) Then
                        If arrEventReportDoc.Count > 0 Then
                            For Each oEventReportDoc As BabitEventReportDocument In arrEventReportDoc
                                m_TransactionManager.AddDelete(oEventReportDoc)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrEventReportAct) Then
                        If arrEventReportAct.Count > 0 Then
                            For Each oDisplayTarget As BabitEventReportActivity In arrEventReportAct
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

        Public Function InsertTransaction(ByVal objBabitEventReportHeader As BabitEventReportHeader, ByVal arrBabitEventReportDetail As ArrayList, ByVal arlEventReportDoc As ArrayList, ByVal arlEventReportAct As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objBabitEventReportHeader, m_userPrincipal.Identity.Name)

                    If Not IsNothing(arlEventReportAct) Then
                        If arlEventReportAct.Count > 0 Then
                            For Each oActivity As BabitEventReportDetail In arlEventReportAct
                                oActivity.BabitEventReportHeader = objBabitEventReportHeader
                                m_TransactionManager.AddInsert(oActivity, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arlEventReportDoc) Then
                        If arlEventReportDoc.Count > 0 Then
                            For Each oDocument As BabitEventReportDocument In arlEventReportDoc
                                oDocument.BabitEventReportHeader = objBabitEventReportHeader
                                m_TransactionManager.AddInsert(oDocument, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrBabitEventReportDetail) Then
                        If arrBabitEventReportDetail.Count > 0 Then
                            For Each oBabitEventReportDetail As BabitEventReportDetail In arrBabitEventReportDetail
                                oBabitEventReportDetail.BabitEventReportHeader = objBabitEventReportHeader
                                m_TransactionManager.AddInsert(oBabitEventReportDetail, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = objBabitEventReportHeader.ID
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

        Public Function UpdateTransaction(ByVal objBabitEventReportHeader As BabitEventReportHeader, ByVal arrEventReportDetail As ArrayList, ByVal arrDeletedEventReportDetail As ArrayList, ByVal arlEventReportDoc As ArrayList, ByVal arlDeleteEventReportDoc As ArrayList, ByVal arlEventReportAct As ArrayList, ByVal arlDeleteEventReportAct As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not IsNothing(arrDeletedEventReportDetail) Then
                        If arrDeletedEventReportDetail.Count > 0 Then
                            For Each item As BabitEventReportDetail In arrDeletedEventReportDetail
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If
                    If Not IsNothing(arrEventReportDetail) Then
                        If arrEventReportDetail.Count > 0 Then
                            For Each item As BabitEventReportDetail In arrEventReportDetail
                                item.BabitEventReportHeader = objBabitEventReportHeader
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDeleteEventReportDoc) Then
                        If arlDeleteEventReportDoc.Count > 0 Then
                            For Each item As BabitEventReportDocument In arlDeleteEventReportDoc
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If
                    If Not IsNothing(arlEventReportDoc) Then
                        If arlEventReportDoc.Count > 0 Then
                            For Each item As BabitEventReportDocument In arlEventReportDoc
                                item.BabitEventReportHeader = objBabitEventReportHeader
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDeleteEventReportAct) Then
                        If arlDeleteEventReportAct.Count > 0 Then
                            For Each item As BabitEventReportDetail In arlDeleteEventReportAct
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If
                    If Not IsNothing(arlEventReportAct) Then
                        If arlEventReportAct.Count > 0 Then
                            For Each item As BabitEventReportDetail In arlEventReportAct
                                item.BabitEventReportHeader = objBabitEventReportHeader
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    m_TransactionManager.AddUpdate(objBabitEventReportHeader, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objBabitEventReportHeader.ID
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


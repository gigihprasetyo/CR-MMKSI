
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
'// Generated on 05/13/2019 - 2:00:25 PM
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

    Public Class BabitPameranDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BabitPameranDetailMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BabitPameranDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(BabitPameranDetail).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(BabitPameranDetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BabitPameranDetail
            Return CType(m_BabitPameranDetailMapper.Retrieve(ID), BabitPameranDetail)
        End Function

        Public Function Retrieve(ByVal Code As String) As BabitPameranDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitPameranDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitPameranDetail), "BabitPameranDetailCode", MatchType.Exact, Code))

            Dim BabitPameranDetailColl As ArrayList = m_BabitPameranDetailMapper.RetrieveByCriteria(criterias)
            If (BabitPameranDetailColl.Count > 0) Then
                Return CType(BabitPameranDetailColl(0), BabitPameranDetail)
            End If
            Return New BabitPameranDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitPameranDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BabitPameranDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BabitPameranDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitPameranDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitPameranDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitPameranDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitPameranDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitPameranDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BabitPameranDetail As ArrayList = m_BabitPameranDetailMapper.RetrieveByCriteria(criterias)
            Return _BabitPameranDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitPameranDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitPameranDetailColl As ArrayList = m_BabitPameranDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BabitPameranDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(BabitPameranDetail), SortColumn, sortDirection))
            Dim BabitPameranDetailColl As ArrayList = m_BabitPameranDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BabitPameranDetailColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BabitPameranDetailColl As ArrayList = m_BabitPameranDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BabitPameranDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitPameranDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitPameranDetailColl As ArrayList = m_BabitPameranDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BabitPameranDetail), columnName, matchOperator, columnValue))
            Return BabitPameranDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitPameranDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitPameranDetail), columnName, matchOperator, columnValue))

            Return m_BabitPameranDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitPameranDetail), "BabitPameranDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BabitPameranDetail), "BabitPameranDetailCode", AggregateType.Count)
            Return CType(m_BabitPameranDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As BabitPameranDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_BabitPameranDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As BabitPameranDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BabitPameranDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As BabitPameranDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BabitPameranDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As BabitPameranDetail)
            Try
                m_BabitPameranDetailMapper.Delete(objDomain)
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

            If (TypeOf InsertArg.DomainObject Is BabitHeader) Then
                CType(InsertArg.DomainObject, BabitHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, BabitHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is BabitPameranDetail) Then
                CType(InsertArg.DomainObject, BabitPameranDetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function InsertTransaction(ByVal objBabitHeader As BabitHeader, ByVal arrPameranDetail As ArrayList,
                                          ByVal arrBabitPameranExpense As ArrayList, ByVal arrDocument As ArrayList,
                                          ByVal arrDisplayTarget As ArrayList, ByVal _arrBabitDealerAlloc As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objBabitHeader, m_userPrincipal.Identity.Name)

                    If Not IsNothing(arrPameranDetail) Then
                        If arrPameranDetail.Count > 0 Then
                            For Each oPameranDetail As BabitPameranDetail In arrPameranDetail
                                oPameranDetail.BabitHeader = objBabitHeader
                                m_TransactionManager.AddInsert(oPameranDetail, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrBabitPameranExpense) Then
                        If arrBabitPameranExpense.Count > 0 Then
                            For Each oPameranExpense As BabitPameranExpense In arrBabitPameranExpense
                                oPameranExpense.BabitHeader = objBabitHeader
                                m_TransactionManager.AddInsert(oPameranExpense, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrDocument) Then
                        If arrDocument.Count > 0 Then
                            For Each oDocument As BabitDocument In arrDocument
                                oDocument.BabitHeader = objBabitHeader
                                m_TransactionManager.AddInsert(oDocument, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrDisplayTarget) Then
                        If arrDisplayTarget.Count > 0 Then
                            For Each oDisplayTarget As BabitDisplayCar In arrDisplayTarget
                                oDisplayTarget.BabitHeader = objBabitHeader
                                m_TransactionManager.AddInsert(oDisplayTarget, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(_arrBabitDealerAlloc) Then
                        If _arrBabitDealerAlloc.Count > 0 Then
                            For Each oBabitDealerAllocation As BabitDealerAllocation In _arrBabitDealerAlloc
                                oBabitDealerAllocation.BabitHeader = objBabitHeader
                                m_TransactionManager.AddInsert(oBabitDealerAllocation, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    'If Not IsNothing(objBabitDealerAllocation) Then
                    '    m_TransactionManager.AddInsert(objBabitDealerAllocation, m_userPrincipal.Identity.Name)
                    'End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = objBabitHeader.ID
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

        Function DeleteTransaction(ByVal objBabitHeader As BabitHeader, ByVal arrPameranDetail As ArrayList, ByVal arrBabitPameranExpense As ArrayList,
                                   ByVal arrDocument As ArrayList, ByVal arrDisplayTarget As ArrayList, Optional ByVal objBabitDealerAllocation As BabitDealerAllocation = Nothing) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arrPameranDetail.Count > 0 Then
                        For Each oPameranDetail As BabitPameranDetail In arrPameranDetail
                            oPameranDetail.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(oPameranDetail, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrBabitPameranExpense.Count > 0 Then
                        For Each oPameranExpense As BabitPameranExpense In arrBabitPameranExpense
                            oPameranExpense.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(oPameranExpense, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrDocument.Count > 0 Then
                        For Each oDocument As BabitDocument In arrDocument
                            oDocument.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(oDocument, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrDisplayTarget.Count > 0 Then
                        For Each oDisplayTarget As BabitDisplayCar In arrDisplayTarget
                            oDisplayTarget.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(oDisplayTarget, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    If Not IsNothing(objBabitDealerAllocation) Then
                        If objBabitDealerAllocation.ID > 0 Then
                            objBabitDealerAllocation.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(objBabitDealerAllocation, m_userPrincipal.Identity.Name)
                        End If
                    End If
                    objBabitHeader.RowStatus = CType(DBRowStatus.Deleted, Short)
                    m_TransactionManager.AddUpdate(objBabitHeader, m_userPrincipal.Identity.Name)

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

        Function UpdateTransaction(babitHeader As BabitHeader, arlPameranDetail As ArrayList, arlEvent As ArrayList, arlDelEvent As ArrayList, arrBabitDocs As ArrayList,
                                   arlDelDocument As ArrayList, arlDisplayAndTarget As ArrayList, arlDelDisplayAndTarget As ArrayList,
                                   arrBabitDealerAlloc As ArrayList, arrDelBabitDealerAlloc As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arlPameranDetail.Count > 0 Then
                        For Each oPameranDetail As BabitPameranDetail In arlPameranDetail
                            oPameranDetail.BabitHeader = babitHeader
                            If oPameranDetail.ID <> 0 Then
                                m_TransactionManager.AddUpdate(oPameranDetail, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(oPameranDetail, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    If Not IsNothing(arlDelEvent) Then
                        If arlDelEvent.Count > 0 Then
                            For Each item As BabitPameranExpense In arlDelEvent
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arlEvent) Then
                        If arlEvent.Count > 0 Then
                            For Each item As BabitPameranExpense In arlEvent
                                item.BabitHeader = babitHeader
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
                            For Each item As BabitDocument In arlDelDocument
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrBabitDocs) Then
                        If arrBabitDocs.Count > 0 Then
                            For Each item As BabitDocument In arrBabitDocs
                                item.BabitHeader = babitHeader
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDelDisplayAndTarget) Then
                        If arlDelDisplayAndTarget.Count > 0 Then
                            For Each item As BabitDisplayCar In arlDelDisplayAndTarget
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDisplayAndTarget) Then
                        If arlDisplayAndTarget.Count > 0 Then
                            For Each item As BabitDisplayCar In arlDisplayAndTarget
                                item.BabitHeader = babitHeader
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arrDelBabitDealerAlloc) Then
                        If arrDelBabitDealerAlloc.Count > 0 Then
                            For Each item As BabitDealerAllocation In arrDelBabitDealerAlloc
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrBabitDealerAlloc) Then
                        If arrBabitDealerAlloc.Count > 0 Then
                            For Each item As BabitDealerAllocation In arrBabitDealerAlloc
                                item.BabitHeader = babitHeader
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    'If Not IsNothing(objBabitDealerAllocation) Then
                    '    If objBabitDealerAllocation.ID > 0 Then
                    '        m_TransactionManager.AddUpdate(objBabitDealerAllocation, m_userPrincipal.Identity.Name)
                    '    Else
                    '        m_TransactionManager.AddInsert(objBabitDealerAllocation, m_userPrincipal.Identity.Name)
                    '    End If
                    'End If

                    m_TransactionManager.AddUpdate(babitHeader, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = babitHeader.ID
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

    End Class

End Namespace


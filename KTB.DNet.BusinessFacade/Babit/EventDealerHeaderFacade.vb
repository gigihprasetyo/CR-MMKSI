
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
'// Generated on 14/05/2019 - 16:04:51
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

    Public Class EventDealerHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_EventDealerHeaderMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_EventDealerHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(EventDealerHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(EventDealerHeader))
            Me.DomainTypeCollection.Add(GetType(EventDealerDocument))


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As EventDealerHeader
            Return CType(m_EventDealerHeaderMapper.Retrieve(ID), EventDealerHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As EventDealerHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventDealerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EventDealerHeader), "EventDealerHeaderCode", MatchType.Exact, Code))

            Dim EventDealerHeaderColl As ArrayList = m_EventDealerHeaderMapper.RetrieveByCriteria(criterias)
            If (EventDealerHeaderColl.Count > 0) Then
                Return CType(EventDealerHeaderColl(0), EventDealerHeader)
            End If
            Return New EventDealerHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_EventDealerHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_EventDealerHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_EventDealerHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventDealerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventDealerHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventDealerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventDealerHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventDealerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _EventDealerHeader As ArrayList = m_EventDealerHeaderMapper.RetrieveByCriteria(criterias)
            Return _EventDealerHeader
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sorts As ICollection) As ArrayList
            Return m_EventDealerHeaderMapper.RetrieveByCriteria(Criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventDealerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EventDealerHeaderColl As ArrayList = m_EventDealerHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EventDealerHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(EventDealerHeader), SortColumn, sortDirection))
            Dim EventDealerHeaderColl As ArrayList = m_EventDealerHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return EventDealerHeaderColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim EventDealerHeaderColl As ArrayList = m_EventDealerHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return EventDealerHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventDealerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EventDealerHeaderColl As ArrayList = m_EventDealerHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(EventDealerHeader), columnName, matchOperator, columnValue))
            Return EventDealerHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventDealerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventDealerHeader), columnName, matchOperator, columnValue))

            Return m_EventDealerHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventDealerHeader), "EventDealerHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(EventDealerHeader), "EventDealerHeaderCode", AggregateType.Count)
            Return CType(m_EventDealerHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As EventDealerHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_EventDealerHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As EventDealerHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_EventDealerHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As EventDealerHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_EventDealerHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As EventDealerHeader)
            Try
                m_EventDealerHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function InsertTransaction(oHeader As EventDealerHeader, arrDet As ArrayList, arrDoc As ArrayList, arrReqDoc As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(oHeader, m_userPrincipal.Identity.Name)

                    If arrDet.Count > 0 Then
                        For Each oDet As EventDealerDetail In arrDet
                            oDet.EventDealerHeader = oHeader
                            m_TransactionManager.AddInsert(oDet, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrDoc.Count > 0 Then
                        For Each oDoc As EventDealerDocument In arrDoc
                            oDoc.EventDealerHeader = oHeader
                            m_TransactionManager.AddInsert(oDoc, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrReqDoc.Count > 0 Then
                        For Each oReqDoc As EventDealerRequiredDocument In arrReqDoc
                            oReqDoc.EventDealerHeader = oHeader
                            m_TransactionManager.AddInsert(oReqDoc, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = oHeader.ID
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
            If (TypeOf InsertArg.DomainObject Is EventDealerHeader) Then
                CType(InsertArg.DomainObject, EventDealerHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, EventDealerHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is EventDealerDetail) Then
                CType(InsertArg.DomainObject, EventDealerDetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_EventDealerHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function UpdateTransaction(ByVal objEventDealerHeader As EventDealerHeader, ByVal arrDet As ArrayList, ByVal arrUpdateDet As ArrayList, ByVal arrDoc As ArrayList, ByVal arlDeleteDoc As ArrayList, ByVal arrReqDoc As ArrayList, ByVal arlDeleteRequiredDoc As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrDet.Count > 0 Then
                        For Each item As EventDealerDetail In arrDet
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    If arrUpdateDet.Count > 0 Then
                        For Each item As EventDealerDetail In arrUpdateDet
                            item.EventDealerHeader = objEventDealerHeader
                            item.RowStatus = DBRowStatus.Active
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    If arlDeleteDoc.Count > 0 Then
                        For Each item As EventDealerDocument In arlDeleteDoc
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    If arrDoc.Count > 0 Then
                        For Each item As EventDealerDocument In arrDoc
                            item.EventDealerHeader = objEventDealerHeader
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If
                    If arlDeleteRequiredDoc.Count > 0 Then
                        For Each item As EventDealerRequiredDocument In arlDeleteRequiredDoc
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    If arrReqDoc.Count > 0 Then
                        For Each item As EventDealerRequiredDocument In arrReqDoc
                            item.EventDealerHeader = objEventDealerHeader
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If


                    m_TransactionManager.AddUpdate(objEventDealerHeader, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objEventDealerHeader.ID
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

        Public Function DeleteTransaction(ByVal objEventDealerHeader As EventDealerHeader, ByVal arrDeleteDet As ArrayList, ByVal arlDeleteDoc As ArrayList, ByVal arlDeleteRequiredDoc As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrDeleteDet.Count > 0 Then
                        For Each item As EventDealerDetail In arrDeleteDet
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    If arlDeleteDoc.Count > 0 Then
                        For Each item As EventDealerDocument In arlDeleteDoc
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    If arlDeleteRequiredDoc.Count > 0 Then
                        For Each item As EventDealerRequiredDocument In arlDeleteRequiredDoc
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objEventDealerHeader, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objEventDealerHeader.ID
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


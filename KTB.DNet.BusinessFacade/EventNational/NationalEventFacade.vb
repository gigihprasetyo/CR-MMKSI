
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
'// Generated on 27/05/2019 - 9:53:00
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

    Public Class NationalEventFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_NationalEventMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_NationalEventMapper = MapperFactory.GetInstance.GetMapper(GetType(NationalEvent).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(DocumentUpload))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As NationalEvent
            Return CType(m_NationalEventMapper.Retrieve(ID), NationalEvent)
        End Function

        Public Function Retrieve(ByVal Code As String) As NationalEvent
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(NationalEvent), "RegNumber", MatchType.Exact, Code))

            Dim NationalEventColl As ArrayList = m_NationalEventMapper.RetrieveByCriteria(criterias)
            If (NationalEventColl.Count > 0) Then
                Return CType(NationalEventColl(0), NationalEvent)
            End If
            Return New NationalEvent
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_NationalEventMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_NationalEventMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_NationalEventMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(NationalEvent), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_NationalEventMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(NationalEvent), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_NationalEventMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _NationalEvent As ArrayList = m_NationalEventMapper.RetrieveByCriteria(criterias)
            Return _NationalEvent
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim NationalEventColl As ArrayList = m_NationalEventMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return NationalEventColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(NationalEvent), SortColumn, sortDirection))
            Dim NationalEventColl As ArrayList = m_NationalEventMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return NationalEventColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_NationalEventMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim NationalEventColl As ArrayList = m_NationalEventMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return NationalEventColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim NationalEventColl As ArrayList = m_NationalEventMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(NationalEvent), columnName, matchOperator, columnValue))
            Return NationalEventColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(NationalEvent), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEvent), columnName, matchOperator, columnValue))

            Return m_NationalEventMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEvent), "RegNumber", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(NationalEvent), "RegNumber", AggregateType.Count)
            Return CType(m_NationalEventMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As NationalEvent) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_NationalEventMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As NationalEvent) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_NationalEventMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As NationalEvent)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_NationalEventMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As NationalEvent)
            Try
                m_NationalEventMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Function UpdateTransaction(ByVal arrCheckedHeader As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arrCheckedHeader.Count > 0 Then
                        For Each oNationalEvent As NationalEvent In arrCheckedHeader
                            m_TransactionManager.AddUpdate(oNationalEvent, m_userPrincipal.Identity.Name)
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

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is NationalEvent) Then
                CType(InsertArg.DomainObject, NationalEvent).ID = InsertArg.ID
                CType(InsertArg.DomainObject, NationalEvent).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is DocumentUpload) Then
                CType(InsertArg.DomainObject, DocumentUpload).ID = InsertArg.ID
            End If
        End Sub

        Public Function InsertTransaction(ByVal objNationalEvent As NationalEvent, ByVal arrDocumentUpload As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objNationalEvent, m_userPrincipal.Identity.Name)

                    If arrDocumentUpload.Count > 0 Then
                        For Each oDocumentUpload As DocumentUpload In arrDocumentUpload
                            oDocumentUpload.DocRegNumber = objNationalEvent.RegNumber
                            m_TransactionManager.AddInsert(oDocumentUpload, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = objNationalEvent.ID
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

        Public Function UpdateTransaction(ByVal objNationalEvent As NationalEvent, ByVal arlNationalEventDoc As ArrayList, ByVal arlDeleteNationalEventDoc As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arlDeleteNationalEventDoc.Count > 0 Then
                        For Each item As DocumentUpload In arlDeleteNationalEventDoc
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    If arlNationalEventDoc.Count > 0 Then
                        For Each item As DocumentUpload In arlNationalEventDoc
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objNationalEvent, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objNationalEvent.ID
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


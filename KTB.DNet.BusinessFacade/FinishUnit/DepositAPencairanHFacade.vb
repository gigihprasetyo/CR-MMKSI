#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2008

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
'// Copyright  2008
'// ---------------------
'// $History      : $
'// Generated on 12/04/2008 - 16:03:00 
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

Imports KTB.Dnet.Domain
Imports KTB.Dnet.Domain.Search
Imports KTB.Dnet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit

    Public Class DepositAPencairanHFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DepositAPencairanHMapper As IMapper
        Private m_DepositAPencairanDMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_DepositAPencairanHMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.DepositAPencairanH).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.DepositAPencairanH))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.DepositAPencairanD))
        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrieveScalar(ByVal criterias As ICriteria, ByVal agg As Aggregate) As Object
            Return m_DepositAPencairanHMapper.RetrieveScalar(agg, criterias)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As DepositAPencairanH
            Return CType(m_DepositAPencairanHMapper.Retrieve(ID), DepositAPencairanH)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DepositAPencairanHMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DepositAPencairanHMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DepositAPencairanHMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAPencairanH), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_DepositAPencairanHMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAPencairanH), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_DepositAPencairanHMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DepositAPencairan As ArrayList = m_DepositAPencairanHMapper.RetrieveByCriteria(criterias)
            Return _DepositAPencairan
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositAPencairanColl As ArrayList = m_DepositAPencairanHMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DepositAPencairanColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositAPencairanH), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DepositAPencairanColl As ArrayList = m_DepositAPencairanHMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DepositAPencairanColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
              ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositAPencairanH), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositAPencairanHMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DepositAPencairanColl As ArrayList = m_DepositAPencairanHMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DepositAPencairanColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositAPencairanColl As ArrayList = m_DepositAPencairanHMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DepositAPencairanH), columnName, matchOperator, columnValue))
            Return DepositAPencairanColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAPencairanH), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAPencairanH), columnName, matchOperator, columnValue))

            Return m_DepositAPencairanHMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function GetExistingDepositAPencairanH(ByVal objDealer As Dealer, ByVal AccountofType As String, ByVal Type As String) As DepositAPencairanH
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositAPencairanH), "Dealer.ID", MatchType.Exact, objDealer.ID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), Type, MatchType.Exact, AccountofType))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "Status", MatchType.NotInSet, CInt(EnumDepositA.StatusPencairanKTB.Blok).ToString() & ", " & CInt(EnumDepositA.StatusPencairanKTB.Tolak).ToString()))
            Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.DepositAPencairanH), "ID", AggregateType.Count)

            Dim list As ArrayList = Me.Retrieve(criterias)
            If list.Count > 0 Then
                Return CType(list(0), DepositAPencairanH)
            Else
                Return Nothing
            End If
        End Function

        Public Function Insert(ByVal objDomain As DepositAPencairanH) As Integer
            Dim returnValue As Integer = -2
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim AccountOfType As String
                    Dim Type As String
                    Dim oldDep As DepositAPencairanH

                    If objDomain.DNNumber.Length > 0 And objDomain.AssignmentNumber.Length = 0 Then
                        AccountOfType = objDomain.DNNumber
                        Type = "DNNumber"
                        oldDep = GetExistingDepositAPencairanH(objDomain.Dealer, AccountOfType, Type)
                    ElseIf objDomain.DNNumber.Length = 0 And objDomain.AssignmentNumber.Length > 0 Then
                        AccountOfType = objDomain.AssignmentNumber
                        Type = "AssignmentNumber"
                        oldDep = GetExistingDepositAPencairanH(objDomain.Dealer, AccountOfType, Type)
                    Else
                        oldDep = Nothing
                    End If

                    If oldDep Is Nothing Then
                        m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                        For Each item As DepositAPencairanD In objDomain.DepositAPencairanDs
                            item.DepositAPencairanH = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                    Else
                        performTransaction = False
                    End If
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
                    End If
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

            'Dim nResult As Integer = -2
            'Try
            '    nResult = m_DepositAPencairanHMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            'Catch ex As Exception
            '    Dim s As String = ex.Message
            '    nResult = -1
            'End Try
            'Return nResult

        End Function

        Public Function Update(ByVal objDomain As DepositAPencairanH) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As DepositAPencairanD In objDomain.DepositAPencairanDs
                        item.DepositAPencairanH = objDomain
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
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

            'Dim nResult As Integer = -1
            'Try
            '    nResult = m_DepositAPencairanHMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            'Catch ex As Exception
            '    nResult = -1
            '    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
            '    If rethrow Then
            '        Throw
            '    End If
            'End Try
            'Return nResult
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.DepositAPencairanH) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.DepositAPencairanH).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DepositAPencairanH).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is DepositAPencairanD) Then

                CType(InsertArg.DomainObject, DepositAPencairanD).ID = InsertArg.ID
            End If
        End Sub

#End Region

#Region "Custom Method"
        Public Function Retrieve(ByVal Code As String) As DepositAPencairanH
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositAPencairanH), "NoReg", MatchType.Exact, Code))

            Dim DepositAPencairanHColl As ArrayList = m_DepositAPencairanHMapper.RetrieveByCriteria(criterias)
            If (DepositAPencairanHColl.Count > 0) Then
                Return CType(DepositAPencairanHColl(0), DepositAPencairanH)
            End If
            Return New DepositAPencairanH
        End Function

#End Region

    End Class

End Namespace

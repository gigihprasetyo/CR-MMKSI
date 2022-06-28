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
'// Generated on 12/3/2008 - 13:15:00
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

Public Class DebitNoteFacade
        Inherits AbstractFacade
    
#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DebitNoteMapper As IMapper
        'Private m_DepositAPencairanHMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_DebitNoteMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.DebitNote).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.DebitNote))
            'Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.DepositAPencairanH))
        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrieveScalar(ByVal criterias As ICriteria, ByVal agg As Aggregate) As Object
            Return m_DebitNoteMapper.RetrieveScalar(agg, criterias)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As DebitNote
            Return CType(m_DebitNoteMapper.Retrieve(ID), DebitNote)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DebitNoteMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DebitNoteMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DebitNoteMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DebitNote), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_DebitNoteMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DebitNote), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_DebitNoteMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DebitNote), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DebitNote As ArrayList = m_DebitNoteMapper.RetrieveByCriteria(criterias)
            Return _DebitNote
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DebitNote), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DebitNoteColl As ArrayList = m_DebitNoteMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DebitNoteColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DebitNote), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DebitNoteColl As ArrayList = m_DebitNoteMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DebitNoteColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
              ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DebitNote), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DebitNote), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DebitNoteMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DebitNoteColl As ArrayList = m_DebitNoteMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DebitNoteColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DebitNote), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DebitNoteColl As ArrayList = m_DebitNoteMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DebitNote), columnName, matchOperator, columnValue))
            Return DebitNoteColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DebitNote), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DebitNote), columnName, matchOperator, columnValue))

            Return m_DebitNoteMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
#End Region

#Region "Transaction/Other Public Method"
        Public Function GetExistingDebitNote(ByVal objDealer As Dealer, ByVal DNNumber As String, ByVal SONumber As String) As DebitNote
            'Public Function GetExistingDebitNote(ByVal objDealer As Dealer) As DebitNote
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DebitNote), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DebitNote), "Dealer.ID", MatchType.Exact, objDealer.ID))
            'If DNNumber.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(DebitNote), "DNNumber", MatchType.Exact, DNNumber))
            'End If
            'If SONumber.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(DebitNote), "Assignment", MatchType.Exact, SONumber))
            'End If
            Dim list As ArrayList = Me.Retrieve(criterias)
            If list.Count > 0 Then
                Return CType(list(0), DebitNote)
            Else
                Return Nothing
            End If
        End Function

        'Public Sub Update(ByVal objDomain As DebitNote)
        '    Dim returnValue As Integer = -1
        '    If (Me.IsTaskFree()) Then
        '        Try
        '            Me.SetTaskLocking()
        '            Dim performTransaction As Boolean = True
        '            Dim ObjMapper As IMapper

        '            For Each item As DebitNoteD In objDomain.DebitNoteDs
        '                item.DebitNote = objDomain
        '                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
        '            Next
        '            m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

        '            If performTransaction Then
        '                m_TransactionManager.PerformTransaction()
        '                returnValue = objDomain.ID
        '            End If
        '        Catch ex As Exception
        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
        '            If rethrow Then
        '                Throw
        '            End If
        '        Finally
        '            Me.RemoveTaskLocking()
        '        End Try
        '    End If

        'End Sub

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.DebitNote) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim oldDep As DebitNote = GetExistingDebitNote(objDomain.Dealer, objDomain.DNNumber, objDomain.Assignment)
                    'Dim oldDep As DebitNote = GetExistingDebitNote(objDomain.Dealer)
                    If oldDep Is Nothing Then
                        m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    Else
                        oldDep.Dealer = objDomain.Dealer
                        oldDep.DNNumber = objDomain.DNNumber
                        oldDep.Amount = objDomain.Amount
                        ' oldDep.Description = objDomain.Description
                        oldDep.Assignment = objDomain.Assignment
                        oldDep.PostingDate = objDomain.PostingDate

                        If oldDep.ProductCategory.ID <> objDomain.ProductCategory.ID Then

                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "DNNumber", MatchType.Exact, objDomain.DNNumber))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "Type", MatchType.Exact, CInt(EnumDepositA.TipePengajuan.Offset)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "Dealer.ID", MatchType.Exact, objDomain.Dealer.ID))

                            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
                            sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositAPencairanH), "NoReg", Sort.SortDirection.DESC))

                            Dim arlDepositAPencarianH As ArrayList = New FinishUnit.DepositAPencairanHFacade(m_userPrincipal).Retrieve(criterias, sortColl)

                            If Not IsNothing(arlDepositAPencarianH) AndAlso arlDepositAPencarianH.Count > 0 Then
                                If CType(arlDepositAPencarianH(0), DepositAPencairanH).Status = CInt(EnumDepositA.StatusPencairanDealer.Blok) OrElse CType(arlDepositAPencarianH(0), DepositAPencairanH).Status = CInt(EnumDepositA.StatusPencairanDealer.Tolak) Then
                                    oldDep.ProductCategory = objDomain.ProductCategory
                                    oldDep.Description = objDomain.Description
                                End If
                            Else
                                oldDep.ProductCategory = objDomain.ProductCategory
                                oldDep.Description = objDomain.Description
                            End If
                        Else
                            oldDep.Description = objDomain.Description
                            oldDep.ProductCategory = objDomain.ProductCategory
                        End If

                        m_TransactionManager.AddUpdate(oldDep, m_userPrincipal.Identity.Name)
                    End If
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
        End Function
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.DebitNote) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.DebitNote).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DebitNote).MarkLoaded()

                'ElseIf (TypeOf InsertArg.DomainObject Is DepositAPencairanH) Then

                '    CType(InsertArg.DomainObject, DepositAPencairanH).ID = InsertArg.ID
            End If
        End Sub
#End Region

#Region "Custom Method"
        Public Function Retrieve(ByVal Code As String) As DebitNote
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DebitNote), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DebitNote), "DNNumber", MatchType.Exact, Code))

            Dim DebitNoteColl As ArrayList = m_DebitNoteMapper.RetrieveByCriteria(criterias)
            If (DebitNoteColl.Count > 0) Then
                Return CType(DebitNoteColl(0), DebitNote)
            End If
            Return New DebitNote
        End Function

#End Region

End Class
End Namespace

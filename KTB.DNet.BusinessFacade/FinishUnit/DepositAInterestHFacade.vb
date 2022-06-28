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
'// Generated on 11/28/2008 - 15:41:31 PM
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

    Public Class DepositAInterestHFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DepositAInterestHMapper As IMapper
        Private m_DepositAInterestDMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_DepositAInterestHMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.DepositAInterestH).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.DepositAInterestH))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.DepositAInterestD))
        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrieveScalar(ByVal criterias As ICriteria, ByVal agg As Aggregate) As Object
            Return m_DepositAInterestHMapper.RetrieveScalar(agg, criterias)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As DepositAInterestH
            Return CType(m_DepositAInterestHMapper.Retrieve(ID), DepositAInterestH)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DepositAInterestHMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DepositAInterestHMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DepositAInterestHMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAInterestH), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_DepositAInterestHMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAInterestH), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_DepositAInterestHMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAInterestH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DepositAInterestH As ArrayList = m_DepositAInterestHMapper.RetrieveByCriteria(criterias)
            Return _DepositAInterestH
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAInterestH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositAInterestHColl As ArrayList = m_DepositAInterestHMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DepositAInterestHColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositAInterestH), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DepositAInterestHColl As ArrayList = m_DepositAInterestHMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DepositAInterestHColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
              ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAInterestH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositAInterestH), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositAInterestHMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DepositAInterestHColl As ArrayList = m_DepositAInterestHMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DepositAInterestHColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAInterestH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositAInterestHColl As ArrayList = m_DepositAInterestHMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DepositAInterestH), columnName, matchOperator, columnValue))
            Return DepositAInterestHColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAInterestH), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAInterestH), columnName, matchOperator, columnValue))

            Return m_DepositAInterestHMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
#End Region

#Region "Transaction/Other Public Method"
        Public Function GetExistingDepositAInterestH(ByVal objDealer As Dealer, ByVal Periode As String, ByVal Year As Short) As DepositAInterestH
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAInterestH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositAInterestH), "Dealer.ID", MatchType.Exact, objDealer.ID))
            criterias.opAnd(New Criteria(GetType(DepositAInterestH), "Periode", MatchType.Exact, Periode))
            criterias.opAnd(New Criteria(GetType(DepositAInterestH), "Year", MatchType.Exact, Year))
            Dim list As ArrayList = Me.Retrieve(criterias)
            If list.Count > 0 Then
                Return CType(list(0), DepositAInterestH)
            Else
                Return Nothing
            End If
        End Function

        'CR Split Deposit A
        Public Function GetExistingDepositAInterestH(ByVal objDealer As Dealer, ByVal objProductCategory As ProductCategory, ByVal Periode As String, ByVal Year As Short) As DepositAInterestH
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAInterestH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositAInterestH), "Dealer.ID", MatchType.Exact, objDealer.ID))
            criterias.opAnd(New Criteria(GetType(DepositAInterestH), "ProductCategory.ID", MatchType.Exact, objProductCategory.ID))
            criterias.opAnd(New Criteria(GetType(DepositAInterestH), "Periode", MatchType.Exact, Periode))
            criterias.opAnd(New Criteria(GetType(DepositAInterestH), "Year", MatchType.Exact, Year))
            Dim list As ArrayList = Me.Retrieve(criterias)
            If list.Count > 0 Then
                Return CType(list(0), DepositAInterestH)
            Else
                Return Nothing
            End If
        End Function

        Public Sub Update(ByVal objDomain As DepositAInterestH)
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As DepositAInterestD In objDomain.DepositAInterestDs
                        item.DepositAInterestH = objDomain
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

        End Sub

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.DepositAInterestH) As Integer
            Dim returnValue As Integer = -1
            Dim nResult As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    'CR  DEposit A
                    '  Dim oldDep As DepositAInterestH = GetExistingDepositAInterestH(objDomain.Dealer, objDomain.Periode, objDomain.Year)
                    Dim oldDep As DepositAInterestH = GetExistingDepositAInterestH(objDomain.Dealer, objDomain.ProductCategory, objDomain.Periode, objDomain.Year)
                    If oldDep Is Nothing Then
                        m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                        For Each item As DepositAInterestD In objDomain.DepositAInterestDs
                            item.DepositAInterestH = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                        nResult = objDomain.ID
                    Else
                        For Each item As DepositAInterestD In oldDep.DepositAInterestDs
                            m_TransactionManager.AddDelete(item)
                        Next
                        For Each item As DepositAInterestD In objDomain.DepositAInterestDs
                            item.DepositAInterestH = oldDep
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                        oldDep.Dealer = objDomain.Dealer
                        oldDep.Periode = objDomain.Periode
                        oldDep.ProductCategory = objDomain.ProductCategory
                        oldDep.Year = objDomain.Year
                        oldDep.InterestAmount = objDomain.InterestAmount
                        oldDep.TaxAmount = objDomain.TaxAmount
                        oldDep.NettoAmount = objDomain.NettoAmount
                        m_TransactionManager.AddUpdate(oldDep, m_userPrincipal.Identity.Name)
                        nResult = oldDep.ID
                    End If
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        'returnValue = objDomain.ID
                        returnValue = nResult
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
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.DepositAInterestH) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.DepositAInterestH).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DepositAInterestH).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is DepositAInterestD) Then

                CType(InsertArg.DomainObject, DepositAInterestD).ID = InsertArg.ID
            End If
        End Sub
#End Region

#Region "Custom Method"

        'Public Function SynchronizeDepositAInterestH(ByVal objDomain As KTB.DNet.Domain.DepositAInterestH) As Integer
        '    Try

        '        Dim objDepositAInterestH As DepositAInterestH = RetrieveDepositAInterestH(objDomain)
        '        If objDepositAInterestH Is Nothing Then
        '            Insert(objDomain)
        '        Else
        '            'objDepositAInterestH.Dealer = objDomain.Dealer
        '            objDepositAInterestH.InterestAmount = objDomain.InterestAmount
        '            objDepositAInterestH.TaxAmount = objDomain.TaxAmount
        '            objDepositAInterestH.NettoAmount = objDomain.NettoAmount

        '            For Each item As DepositAInterestD In objDomain.DepositAInterestDs
        '                Dim objDepositAInterestDFacade As DepositAInterestDFacade = New DepositAInterestDFacade(System.Threading.Thread.CurrentPrincipal)
        '                '    objDepositAInterestDFacade.SynchronizeDepositAInterestD(item, objDepositAInterestH)
        '            Next
        '            Update(objDepositAInterestH)
        '        End If
        '    Catch ex As Exception
        '        Throw ex
        '    End Try

        'End Function

        'Private Function RetrieveDepositAInterestH(ByVal _DepositAInterestH As DepositAInterestH) As DepositAInterestH
        '    Dim _DepositAInterestHFacade As DepositAInterestHFacade
        '    Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestH), "Dealer.ID", MatchType.Exact, _DepositAInterestH.Dealer.ID))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestH), "Periode", MatchType.Exact, _DepositAInterestH.Periode))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestH), "Year", MatchType.Exact, _DepositAInterestH.Year))
        '    _DepositAInterestHFacade = New DepositAInterestHFacade(System.Threading.Thread.CurrentPrincipal)
        '    Dim arlDepositAInterestH As ArrayList = _DepositAInterestHFacade.Retrieve(criterias)
        '    If arlDepositAInterestH.Count > 0 Then
        '        Dim objDepositAInterestH As DepositAInterestH = CType(arlDepositAInterestH(0), DepositAInterestH)
        '        Return objDepositAInterestH
        '    Else
        '        Return Nothing
        '    End If
        'End Function

#End Region

    End Class
End Namespace

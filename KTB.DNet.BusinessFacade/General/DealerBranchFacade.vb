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
Imports System.Data.SqlClient

#End Region
Namespace KTB.DNet.BusinessFacade.General
    Public Class DealerBranchFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DealerBranchMapper As IMapper
        Private m_DealerBranchBusinessAreaMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_DealerBranchMapper = MapperFactory.GetInstance.GetMapper(GetType(DealerBranch).ToString)
            Me.m_DealerBranchBusinessAreaMapper = MapperFactory.GetInstance.GetMapper(GetType(DealerBranchBusinessArea).ToString)
            Me.m_TransactionManager = New TransactionManager
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.DealerBranch))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.DealerBranchBusinessArea))
            AddHandler Me.m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DealerBranch
            Return CType(m_DealerBranchMapper.Retrieve(ID), DealerBranch)
        End Function

        Public Function Retrieve(ByVal Code As String) As KTB.DNet.Domain.DealerBranch
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "DealerBranchCode", MatchType.Exact, Code))

            Dim DealerColl As ArrayList = m_DealerBranchMapper.RetrieveByCriteria(criterias)
            If (DealerColl.Count > 0) Then
                Return CType(DealerColl(0), KTB.DNet.Domain.DealerBranch)
            End If
            Return Nothing
        End Function

        Public Function Retrieve(ByVal BranchCode As String, ByVal DealerCode As String) As DealerBranch
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "DealerBranchCode", MatchType.Exact, BranchCode))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "Dealer.DealerCode", MatchType.Exact, DealerCode))

            Dim DealerColl As ArrayList = m_DealerBranchMapper.RetrieveByCriteria(criterias)
            If (DealerColl.Count > 0) Then
                Return CType(DealerColl(0), KTB.DNet.Domain.DealerBranch)
            End If
            Return Nothing
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DealerBranchMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DealerBranchMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DealerBranchMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerBranch), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DealerBranchMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerBranch), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DealerBranchMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DealerBranch As ArrayList = m_DealerBranchMapper.RetrieveByCriteria(criterias)
            Return _DealerBranch
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sorts As ICollection) As ArrayList
            Return m_DealerBranchMapper.RetrieveByCriteria(Criterias, sorts)
        End Function


        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DealerBranchColl As ArrayList = m_DealerBranchMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DealerBranchColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerBranch), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DealerBranchColl As ArrayList = m_DealerBranchMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DealerBranchColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DealerBranchColl As ArrayList = m_DealerBranchMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DealerBranchColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DealerBranchColl As ArrayList = m_DealerBranchMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DealerBranch), columnName, matchOperator, columnValue))
            Return DealerBranchColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerBranch), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBranch), columnName, matchOperator, columnValue))

            Return m_DealerBranchMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Update(ByVal objDomain As DealerBranch) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DealerBranchMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Insert(ByVal objDomain As DealerBranch) As Integer
            Dim returnValue As Integer = -1
            If Me.IsTaskFree Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

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

        Public Function Delete(ByVal objDomain As DealerBranch) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = DBRowStatus.Deleted
                nResult = m_DealerBranchMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Insert(ByVal objDomain As DealerBranch, ByVal arrDBBusinesArea As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If 1 = 1 Then 'Me.IsTaskFree Then
                Try
                    'Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    If arrDBBusinesArea.Count > 0 Then
                        For Each objBusinessArea As DealerBranchBusinessArea In arrDBBusinesArea
                            objBusinessArea.DealerBranch = objDomain
                            objBusinessArea.Dealer = objDomain.Dealer
                            m_TransactionManager.AddInsert(objBusinessArea, m_userPrincipal.Identity.Name)
                        Next
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

        Public Function Update(ByVal objDomain As DealerBranch, ByVal arrDBBusinesArea As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If 1 = 1 Then 'Me.IsTaskFree Then
                Try
                    'Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper
                    '  m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    m_DealerBranchMapper.Update(objDomain, m_userPrincipal.Identity.Name)

                    Dim arrDBArea As New ArrayList
                    arrDBArea = objDomain.DealerBranchBusinesAreas

                    'delete existing
                    If Not IsNothing(arrDBArea) AndAlso arrDBArea.Count > 0 Then
                        For Each objBusinessArea As DealerBranchBusinessArea In arrDBArea
                            objBusinessArea.DealerBranch = objDomain
                            objBusinessArea.RowStatus = CType(DBRowStatus.Deleted, Short)
                            ' m_TransactionManager.AddUpdate(objBusinessArea, m_userPrincipal.Identity.Name)
                            m_DealerBranchBusinessAreaMapper.Update(objBusinessArea, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    'Merge Existing
                    If Not IsNothing(arrDBBusinesArea) AndAlso arrDBBusinesArea.Count > 0 Then
                        For Each objBusinessArea As DealerBranchBusinessArea In arrDBBusinesArea

                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBranchBusinessArea), "DealerBranch.ID", MatchType.Exact, objDomain.ID))
                            criterias.opAnd(New Criteria(GetType(DealerBranchBusinessArea), "Kind", MatchType.Exact, objBusinessArea.Kind))
                            Dim arlDealerProfile As ArrayList = New DealerBranchBusinessAreaFacade(m_userPrincipal).Retrieve(criterias)

                            If Not IsNothing(arlDealerProfile) AndAlso arlDealerProfile.Count > 0 Then
                                Dim objDB As DealerBranchBusinessArea = CType(arlDealerProfile(0), DealerBranchBusinessArea)
                                objDB.DealerBranch = objDomain
                                objDB.ContactPerson = objBusinessArea.ContactPerson
                                objDB.DepHeadPIC = objBusinessArea.DepHeadPIC
                                objDB.SectionHeadPIC = objBusinessArea.SectionHeadPIC
                                objDB.SalesACPIC = objBusinessArea.SalesACPIC
                                objDB.Phone = objBusinessArea.Phone
                                objDB.HP = objBusinessArea.HP
                                objDB.Dealer = objBusinessArea.Dealer
                                objDB.Email = objBusinessArea.Email
                                objDB.RowStatus = CType(DBRowStatus.Active, Short)
                                objDB.DealerBranch = objDomain

                                m_DealerBranchBusinessAreaMapper.Update(objDB, m_userPrincipal.Identity.Name)
                            Else
                                objBusinessArea.Dealer = objDomain.Dealer
                                objBusinessArea.DealerBranch = objDomain
                                m_DealerBranchBusinessAreaMapper.Update(objBusinessArea, m_userPrincipal.Identity.Name)
                            End If

                        Next
                    End If
                    returnValue = objDomain.ID
                    'If performTransaction Then
                    '    m_TransactionManager.PerformTransaction()

                    'End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    'Me.RemoveTaskLocking()
                End Try
            End If

            Return returnValue
        End Function
        Public Function InsertWithTransactionManager(ByVal dealerBranch As DealerBranch, ByVal dealerBranchBusinessAreaList As ArrayList) As Integer
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.m_TransactionManager_Insert
            If Me.IsTaskFree Then
                Try
                    Me.SetTaskLocking()

                    ' add command to insert Dealer Branch
                    Me.m_TransactionManager.AddInsert(dealerBranch, m_userPrincipal.Identity.Name)
                    ' add command to insert Dealer Branch Business Area
                    For Each dealerBranchBusinessArea As DealerBranchBusinessArea In dealerBranchBusinessAreaList
                        Me.m_TransactionManager.AddInsert(dealerBranchBusinessArea, m_userPrincipal.Identity.Name)

                    Next
                   
                    Me.m_TransactionManager.PerformTransaction()
                    result = dealerBranch.ID

                    Return result
                Catch sqlException As SqlException
                    Throw sqlException
                Catch ex As Exception
                    Throw ex
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return result

        End Function

        Public Function UpdateWithTransactionManager(ByVal dealerBranch As DealerBranch, ByVal dealerBranchBusinessAreaList As ArrayList) As Integer
            ' mark as loaded to prevent it loads from db
            dealerBranch.MarkLoaded()
            For Each dealerBranchBusinessArea As DealerBranchBusinessArea In dealerBranchBusinessAreaList
                dealerBranchBusinessArea.MarkLoaded()
            Next

            ' set default result
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.m_TransactionManager_Insert
            If Me.IsTaskFree Then
                Try
                    Me.SetTaskLocking()

                    ' add command to Dealer Branch Business Area
                    
                    For Each dealerBranchBusinessArea As DealerBranchBusinessArea In dealerBranchBusinessAreaList
                        If dealerBranchBusinessArea.ID <> 0 Then
                            If (dealerBranchBusinessArea.LastUpdateBy.ToLower <> "not update") Then
                                m_TransactionManager.AddUpdate(dealerBranchBusinessArea, m_userPrincipal.Identity.Name)
                            End If
                        Else
                            m_TransactionManager.AddInsert(dealerBranchBusinessArea, m_userPrincipal.Identity.Name)
                        End If

                        dealerBranchBusinessArea.MarkLoaded()
                    Next
                    ' add command to update Dealer Branch
                    If (dealerBranch.LastUpdateBy.ToLower <> "not update") Then
                        m_TransactionManager.AddUpdate(dealerBranch, m_userPrincipal.Identity.Name)
                    End If
                    m_TransactionManager.PerformTransaction()

                    result = dealerBranch.ID
                Catch ex As Exception
                    Throw ex
                Finally
                    Me.RemoveTaskLocking()
                End Try

            End If

            Return result
        End Function


        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.DealerBranch) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DealerBranch).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DealerBranch).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.DealerBranchBusinessArea) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DealerBranchBusinessArea).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DealerBranchBusinessArea).MarkLoaded()
            End If
        End Sub


#End Region

#Region "Custom Method"
        Public Function ValidateName(ByVal strDealerBranchName As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(DealerBranch), "DealerName", MatchType.Exact, strDealerBranchName))
            Dim agg As Aggregate = New Aggregate(GetType(DealerBranch), "DealerName", AggregateType.Count)

            Return CType(m_DealerBranchMapper.RetrieveScalar(agg, crit), Integer)
        End Function
#End Region
    End Class
End Namespace


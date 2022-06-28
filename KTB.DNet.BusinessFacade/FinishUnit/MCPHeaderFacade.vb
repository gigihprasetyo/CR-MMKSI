#Region "imports library namespace"
Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
#End Region

#Region "imports custom namespace"
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class MCPHeaderFacade
        Inherits AbstractFacade

#Region "private variables"
        Private m_MCPHeaderMapper As IMapper
        Private m_MCPDealerMapper As IMapper
        Private m_MCPDetailMapper As IMapper
        Private m_TransactionManager As TransactionManager
        Private m_userPrincipal As IPrincipal = Nothing
#End Region

#Region "constructor"
        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_MCPHeaderMapper = MapperFactory.GetInstance().GetMapper(GetType(MCPHeader).ToString)
            Me.m_MCPDealerMapper = MapperFactory.GetInstance().GetMapper(GetType(MCPDealer).ToString)
            Me.m_MCPDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(MCPDetail).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.MCPHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.MCPDealer))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.MCPDetail))
        End Sub
#End Region

#Region "retrieve"
        Public Function Retrieve(ByVal ID As Integer) As MCPHeader
            Return CType(m_MCPHeaderMapper.Retrieve(ID), MCPHeader)
        End Function

        Public Function Retrieve(ByVal MCPNumber As String) As MCPHeader
            Dim objReturnValue As MCPHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MCPHeader), "ReferenceNumber", MatchType.Exact, MCPNumber))
            Dim List As ArrayList
            List = m_MCPHeaderMapper.RetrieveByCriteria(criterias)
            If Not List Is Nothing Then
                If List.Count > 0 Then
                    objReturnValue = CType(List.Item(0), MCPHeader)
                End If
            End If
            Return objReturnValue
        End Function

        Public Function IsMainUsageFound(ByVal strMainUsageCode As String) As Boolean _
        'lagi trace yg ini jgn lupa

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim bResult As Boolean = False
            Dim MCPHeaderColl As ArrayList = m_MCPHeaderMapper.RetrieveByCriteria(criterias)
            If (MCPHeaderColl.Count > 0) Then
                bResult = True
            Else
            End If
            Return bResult

        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MCPHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MCPHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_MCPHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MCPHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MCPHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MCPHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MCPHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MainUsage As ArrayList = m_MCPHeaderMapper.RetrieveByCriteria(criterias)
            Return _MainUsage
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MCPHeaderColl As ArrayList = m_MCPHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MCPHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MCPHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim MCPHeaderColl As ArrayList = m_MCPHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MCPHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
          ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MCPHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_MCPHeaderMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
               ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(MCPHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MCPHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(MCPHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim MCPHeaderColl As ArrayList = m_MCPHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MCPHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MCPHeader), columnName, matchOperator, columnValue))
            Dim MCPHeaderColl As ArrayList = m_MCPHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MCPHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MCPHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPHeader), columnName, matchOperator, columnValue))

            Return m_MCPHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveDealerID(ByVal ID As Integer)

            Dim objReturnValue As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MCPDealer), "MCPHeader.ID", MatchType.Exact, ID))
            Dim List As ArrayList
            List = m_MCPDealerMapper.RetrieveByCriteria(criterias)
            If Not List Is Nothing Then
                If List.Count > 0 Then
                    objReturnValue = List
                End If
            End If
            Return objReturnValue
        End Function

        Public Function RetrieveMCPDetail(ByVal ID As Integer)
            Dim objReturnValue As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MCPDetail), "MCPHeader.ID", MatchType.Exact, ID))
            Dim List As ArrayList
            List = m_MCPDetailMapper.RetrieveByCriteria(criterias)
            If Not List Is Nothing Then
                If List.Count > 0 Then
                    objReturnValue = List
                End If
            End If
            Return objReturnValue
        End Function


#End Region

#Region "trans/other public method"
        'aa
#Region "Insert"
        Public Function InsertMCPHeader(ByVal objDomain As MCPHeader, ByVal objDetails As ArrayList, ByVal objDealers As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    If objDetails.Count > 0 Then
                        For Each items As MCPDetail In objDetails
                            items.MCPHeader = objDomain
                            m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    If objDealers.Count > 0 Then
                        For Each items As MCPDealer In objDealers
                            items.MCPHeader = objDomain
                            m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
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

        Public Function Insert(ByVal objDomain As MCPHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                m_MCPHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function
#End Region
#Region "Update"
        Public Function Update(ByVal objDomain As MCPHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_MCPHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function
        Public Function UpdateMCPHeader(ByVal objDomain As MCPHeader) As Integer
            Dim returnValue As Integer = -1

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    If objDomain.MCPDetails.Count > 0 Then
                        For Each objMCPDetail As MCPDetail In objDomain.MCPDetails
                            'objMCPDetail.MCPHeader = objDomain
                            'If objMCPDetail.ID <> 0 Then
                            '    m_TransactionManager.AddUpdate(objMCPDetail, m_userPrincipal.Identity.Name)
                            'Else
                            m_TransactionManager.AddInsert(objMCPDetail, m_userPrincipal.Identity.Name)
                            'End If
                        Next
                    End If

                    If objDomain.MCPDealers.Count > 0 Then
                        For Each objMCPDealer As MCPDealer In objDomain.MCPDealers
                            If Not IsNothing(objMCPDealer.Dealer) Then
                                'objMCPDealer.MCPHeader = objDomain
                                'If objMCPDealer.ID <> 0 Then
                                '    m_TransactionManager.AddUpdate(objMCPDealer, m_userPrincipal.Identity.Name)
                                'Else
                                m_TransactionManager.AddInsert(objMCPDealer, m_userPrincipal.Identity.Name)
                                'End If
                            End If
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    'returnValue = m_MCPHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)

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

        Public Function UpdateMCPHeader(ByVal objDomain As MCPHeader, ByVal arrDetail As ArrayList, ByVal arrDealer As ArrayList) As Integer
            Dim returnValue As Integer = -1

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    If arrDetail.Count > 0 Then
                        For Each objMCPDetail As MCPDetail In arrDetail
                            objMCPDetail.MCPHeader = objDomain
                            If objMCPDetail.ID <> 0 Then
                                m_TransactionManager.AddUpdate(objMCPDetail, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(objMCPDetail, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    If arrDealer.Count > 0 Then
                        For Each objMCPDealer As MCPDealer In arrDealer
                            If Not IsNothing(objMCPDealer.Dealer) Then
                                objMCPDealer.MCPHeader = objDomain
                                If objMCPDealer.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(objMCPDealer, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(objMCPDealer, m_userPrincipal.Identity.Name)
                                End If
                            End If
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    'returnValue = m_MCPHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)

                    'If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                    'End If
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
#Region "Delete"
        Public Sub Delete(ByVal objDomain As MCPHeader)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_MCPHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As MCPHeader) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_MCPHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Public Function DeleteOLDDealer(ByVal arrDealer As ArrayList) As Integer
            Dim iReturn As Integer = -1
            Try
                For Each _dealer As MCPDealer In arrDealer
                    Dim objDomain As MCPDealer = _dealer
                    iReturn = m_MCPDealerMapper.Delete(objDomain)
                Next

            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Public Function DeleteOLDMCPDetail(ByVal arrDetail As ArrayList) As Integer
            Dim iReturn As Integer = -1
            Try
                For Each _detail As MCPDetail In arrDetail
                    Dim objDomain As MCPDetail = _detail
                    iReturn = m_MCPDetailMapper.Delete(objDomain)
                Next

            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

#End Region

        Public Function ValidateMCPNumber(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPHeader), "ReferenceNumber", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(MCPHeader), "ID", AggregateType.Count)
            Return CType(m_MCPHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        
#End Region

#Region "custom method"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.MCPHeader) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.MCPHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.MCPHeader).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is MCPDetail) Then
                CType(InsertArg.DomainObject, MCPDetail).ID = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is MCPDealer) Then
                CType(InsertArg.DomainObject, MCPDealer).ID = InsertArg.ID
            End If
        End Sub
#End Region


    End Class

End Namespace


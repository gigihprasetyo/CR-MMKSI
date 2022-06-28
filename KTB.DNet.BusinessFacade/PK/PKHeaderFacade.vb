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
'// Copyright  2005
'// ---------------------
'// $History      : $
'// Generated on 9/26/2005 - 1:43:31 PM
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

Namespace KTB.DNet.BusinessFacade.PK

    Public Class PKHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PKHeaderMapper As IMapper
        Private m_PKHeaderMapperView As IMapper
        Private m_CategoryMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_PKHeaderMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.PKHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PKHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PKDetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PKHeader
            Return CType(m_PKHeaderMapper.Retrieve(ID), PKHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As PKHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PKHeader), "PKNumber", MatchType.Exact, Code))

            Dim PKHeaderColl As ArrayList = m_PKHeaderMapper.RetrieveByCriteria(criterias)
            If (PKHeaderColl.Count > 0) Then
                Return CType(PKHeaderColl(0), PKHeader)
            End If
            Return New PKHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PKHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PKHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PKHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PKHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PKHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PKHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PKHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PKHeader As ArrayList = m_PKHeaderMapper.RetrieveByCriteria(criterias)
            Return _PKHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PKHeaderColl As ArrayList = m_PKHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PKHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PKHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PKHeaderColl As ArrayList = m_PKHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PKHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
              ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PKHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PKHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PKHeaderColl As ArrayList = m_PKHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PKHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PKHeaderColl As ArrayList = m_PKHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PKHeader), columnName, matchOperator, columnValue))
            Return PKHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PKHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKHeader), columnName, matchOperator, columnValue))

            Return m_PKHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKHeader), "PKHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(PKHeader), "PKHeaderCode", AggregateType.Count)
            Return CType(m_PKHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        'Public Sub Insert(ByVal objDomain As PKHeader)
        '    Dim iReturn As Integer = -2
        '    Try
        '        m_PKHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
        '    Catch ex As Exception
        '        Dim s As String = ex.Message
        '        iReturn = -1
        '    End Try

        'End Sub

        Public Function Update(ByVal objDomain As PKHeader) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_PKHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                ireturn = -1
                'Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                'If rethrow Then
                '    Throw
                'End If
            End Try
            Return iReturn
        End Function

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.PKHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    For Each item As PKDetail In objDomain.PKDetails
                        item.PKHeader = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next

                    'For i As Integer = 0 To objDomain.PKDetail.Count - 1
                    'CType(objDomain.PKDetail(i), PKDetail).PKHeader = objDomain
                    'm_TransactionManager.AddInsert(objDomain.PKDetail(i), m_userPrincipal.Identity.Name)
                    'Next
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

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.PKHeader) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.PKHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PKHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is PKDetail) Then

                CType(InsertArg.DomainObject, PKDetail).ID = InsertArg.ID

            End If

        End Sub

        Public Sub Delete(ByVal objDomain As PKHeader)
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                For Each item As PKDetail In objDomain.PKDetails
                    item.RowStatus = CType(DBRowStatus.Deleted, Short)
                Next
                UpdateTransaction(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function UpdateTransaction(ByVal objDomain As KTB.DNet.Domain.PKHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    'For Each item As PKPayment In objDomain.PKPayments
                    '    item.PKHeader = objDomain
                    '    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    'Next

                    For Each item As PKDetail In objDomain.PKDetails
                        item.PKHeader = objDomain
                        If item.ID <> 0 Then
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        End If

                    Next

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    'For i As Integer = 0 To objDomain.PKDetail.Count - 1
                    'CType(objDomain.PKDetail(i), PKDetail).PKHeader = objDomain
                    'm_TransactionManager.AddInsert(objDomain.PKDetail(i), m_userPrincipal.Identity.Name)
                    'Next
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

#End Region

#Region "Custom Method"

        Public Function RetrieveListCategory() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Me.m_CategoryMapper = MapperFactory.GetInstance.GetMapper(GetType(Category).ToString)
            Return m_CategoryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Shared Function RetrieveListDealer() As ArrayList
            Dim m_DealerMapper As IMapper
            m_DealerMapper = MapperFactory.GetInstance.GetMapper(GetType(Dealer).ToString)
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing("DealerCode")) And (Not IsNothing("DealerCode")) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.Dealer), "DealerCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Return m_DealerMapper.RetrieveList(sortColl)
        End Function

        Public Function retrieveListView() As ArrayList
            Return m_PKHeaderMapperView.RetrieveList
        End Function

        Public Function RetrieveByCriteriaView(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Me.m_PKHeaderMapperView = MapperFactory.GetInstance.GetMapper(GetType(view_PKList).ToString)

            Dim PKHeaderCollView As ArrayList = m_PKHeaderMapperView.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PKHeaderCollView
        End Function

        Public Function validatePK(ByVal pkColl As ArrayList)
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As PKHeader In pkColl
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next

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

        Public Function UpdatePKHeaderDetail(ByVal objDomain As KTB.DNet.Domain.PKHeader, ByVal arlPKDetail As ArrayList, _
                                             Optional ByVal arlDeletePKDetail As ArrayList = Nothing, _
                                             Optional ByVal arlPKDetailtoDiscount As ArrayList = Nothing, _
                                             Optional ByVal arlDeletePKDetailtoDiscount As ArrayList = Nothing) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not IsNothing(arlDeletePKDetailtoDiscount) Then
                        If arlDeletePKDetailtoDiscount.Count > 0 Then
                            For Each objPKDetailtoDiscount As PKDetailtoDiscount In arlDeletePKDetailtoDiscount
                                If objPKDetailtoDiscount.ID > 0 Then
                                    objPKDetailtoDiscount.RowStatus = DBRowStatus.Deleted
                                    m_TransactionManager.AddUpdate(objPKDetailtoDiscount, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arlPKDetailtoDiscount) Then
                        If arlPKDetailtoDiscount.Count > 0 Then
                            For Each objPKDetailtoDiscount As PKDetailtoDiscount In arlPKDetailtoDiscount
                                If objPKDetailtoDiscount.ID > 0 Then
                                    m_TransactionManager.AddUpdate(objPKDetailtoDiscount, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(objPKDetailtoDiscount, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDeletePKDetail) Then
                        If arlDeletePKDetail.Count > 0 Then
                            For Each objPKDetail As PKDetail In arlDeletePKDetail
                                If objPKDetail.ID > 0 Then
                                    objPKDetail.RowStatus = DBRowStatus.Deleted
                                    m_TransactionManager.AddUpdate(objPKDetail, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arlPKDetail) Then
                        If arlPKDetail.Count > 0 Then
                            For Each objPKDetail As PKDetail In arlPKDetail
                                objPKDetail.PKHeader = objDomain
                                m_TransactionManager.AddUpdate(objPKDetail, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    'For Each item As PKDetail In objDomain.PKDetail
                    '    item.PKHeader = objDomain
                    '    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    'Next

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

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

        Public Function UpdatePKStatusQty(ByVal pkColl As ArrayList)
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As PKHeader In pkColl
                        If item.PKDetails.Count > 0 Then
                            For Each objPKDetail As PKDetail In item.PKDetails
                                objPKDetail.PKHeader = item
                                m_TransactionManager.AddUpdate(objPKDetail, m_userPrincipal.Identity.Name)
                            Next
                        End If
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next

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

        Public Sub TransferDataSAPFile(ByVal pkColl As ArrayList)

        End Sub

        Public Function RetrieveSp(str As String) As DataSet
            Return m_PKHeaderMapper.RetrieveDataSet(str)
        End Function

        Public Function RefreshDiscount(ByVal pkheaderID As Integer) As Boolean

            Dim sp As String = " sp_RefreshDiscountPKHeader @ID={0}, @CReatedBy='{1}'"

            Return m_PKHeaderMapper.ExecuteSP(String.Format(sp, pkheaderID.ToString(), m_userPrincipal.Identity.Name))
            Return True
        End Function
#End Region

    End Class

End Namespace
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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 7/18/2007 - 2:19:39 PM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Collections.Generic
Imports System.Linq

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.UserManagement

#End Region

Namespace KTB.DNet.BusinessFacade.Salesman

    Public Class SalesmanHeaderFacade

        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SalesmanHeaderMapper As IMapper
        Private m_SalesmanHeaderToDealerMapper As IMapper
        Private m_SalesmanLevelMapper As IMapper
        Private m_V_SalesmanDownloadMapper As IMapper
        Private m_V_SalesmanPartMapper As IMapper
        Private m_V_SalesmanCSMapper As IMapper


        Private m_TransactionManager As TransactionManager
        Private ID_Insert As Integer
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SalesmanHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(SalesmanHeader).ToString)
            Me.m_SalesmanHeaderToDealerMapper = MapperFactory.GetInstance.GetMapper(GetType(SalesmanHeaderToDealer).ToString)
            Me.m_SalesmanLevelMapper = MapperFactory.GetInstance.GetMapper(GetType(SalesmanLevel).ToString)
            Me.m_V_SalesmanDownloadMapper = MapperFactory.GetInstance.GetMapper(GetType(V_SalesmanDownload).ToString)
            Me.m_V_SalesmanPartMapper = MapperFactory.GetInstance.GetMapper(GetType(V_SalesmanPart).ToString)
            Me.m_V_SalesmanCSMapper = MapperFactory.GetInstance.GetMapper(GetType(V_SalesmanCSTeam).ToString())
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.SalesmanHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.SalesmanProfile))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.SalesmanProfileHistory))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.SalesmanLevel))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.SalesmanHeaderToDealer))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SalesmanHeader
            Return CType(m_SalesmanHeaderMapper.Retrieve(ID), SalesmanHeader)
        End Function
        Public Function Retrieve(ByVal SalesmanCode As String) As SalesmanHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanCode", MatchType.Exact, SalesmanCode))

            Dim SalesmanHeaderColl As ArrayList = m_SalesmanHeaderMapper.RetrieveByCriteria(criterias)
            If (SalesmanHeaderColl.Count > 0) Then
                Return CType(SalesmanHeaderColl(0), SalesmanHeader)
            End If
            Return New SalesmanHeader
        End Function
        Public Function RetrieveByCode(ByVal SalesmanCode As String) As SalesmanHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "SalesmanCode", MatchType.Exact, SalesmanCode))

            Dim SalesmanHeaderColl As ArrayList = m_SalesmanHeaderMapper.RetrieveByCriteria(criterias)
            If (SalesmanHeaderColl.Count > 0) Then
                Return CType(SalesmanHeaderColl(0), SalesmanHeader)
            End If
            Return New SalesmanHeader
        End Function
        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SalesmanHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SalesmanHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SalesmanHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesmanHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesmanHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SalesmanHeader As ArrayList = m_SalesmanHeaderMapper.RetrieveByCriteria(criterias)
            Return _SalesmanHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesmanHeaderColl As ArrayList = m_SalesmanHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SalesmanHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(SalesmanHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim SalesmanHeaderColl As ArrayList = m_SalesmanHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SalesmanHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(SalesmanHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim SalesmanHeaderColl As ArrayList = m_SalesmanHeaderMapper.RetrieveByCriteria(criterias, sortColl)
            Return SalesmanHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As SalesmanHeader
            Dim SalesmanHeaderColl As ArrayList = m_SalesmanHeaderMapper.RetrieveByCriteria(criterias)
            If SalesmanHeaderColl.Count > 0 Then
                Return CType(SalesmanHeaderColl(0), SalesmanHeader)
            Else
                Return New SalesmanHeader
            End If
        End Function
        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesmanHeaderColl As ArrayList = m_SalesmanHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), columnName, matchOperator, columnValue))
            Return SalesmanHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), columnName, matchOperator, columnValue))

            Return m_SalesmanHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveSalesmanLevel(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(SalesmanLevel), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim SalesmanLevelColl As ArrayList = m_SalesmanLevelMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SalesmanLevelColl

        End Function
        Public Function RetrieveSalesmanLevel(ByVal criterias As ICriteria) As ArrayList
            Return m_SalesmanLevelMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveSalesmanLevelByID(ByVal ID As Integer) As SalesmanLevel
            Return CType(m_SalesmanLevelMapper.Retrieve(ID), SalesmanLevel)
        End Function


        Public Function RetrieveActiveListSalesmanLevel(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanLevel), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SalesmanLevelColl As ArrayList = m_SalesmanLevelMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SalesmanLevelColl
        End Function


        Public Function RetrieveV_SalesmanDownload(ByVal criterias As ICriteria) As ArrayList
            Return m_V_SalesmanDownloadMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveV_SalesmanDownloadNew(ByVal criterias As ICriteria) As ArrayList
            Dim whereCondition As String = criterias.ToString().Replace("{", "").Replace("}", "")
           
            Dim arrParam As ArrayList = New ArrayList()
            Dim param1 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@WhereCondition", whereCondition)
            arrParam.Add(param1)

            Return m_V_SalesmanDownloadMapper.RetrieveSP("up_DownloadSalesman", arrParam)
        End Function

        Public Function RetrieveV_SalesmanPart(ByVal criterias As ICriteria) As ArrayList
            Return m_V_SalesmanPartMapper.RetrieveByCriteria(criterias)
        End Function

#End Region

#Region "Transaction/Other Public Method"

#End Region

#Region "Need To Add"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "SalesmanCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SalesmanHeader), "SalesmanCode", AggregateType.Count)
            Return CType(m_SalesmanHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.SalesmanHeader, ByVal objListProfile As ArrayList, ByVal arrHistory As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    For Each item As SalesmanProfile In objListProfile
                        item.SalesmanHeader = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        For Each items As SalesmanProfileHistory In item.SalesmanProfileHistorys
                            items.SalesmanProfile = item
                            m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                        Next
                    Next

                    For Each item As SalesmanDealerHistory In arrHistory
                        item.SalesmanHeader = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = ID_Insert
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        returnValue = -1
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Function InsertTransaction(ByVal objDomain As KTB.DNet.Domain.SalesmanHeader, ByVal arrHistory As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    'For Each item As SalesmanProfile In objListProfile
                    '    item.SalesmanHeader = objDomain
                    '    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    '    For Each items As SalesmanProfileHistory In item.SalesmanProfileHistorys
                    '        items.SalesmanProfile = item
                    '        m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                    '    Next
                    'Next

                    For Each item As SalesmanDealerHistory In arrHistory
                        item.SalesmanHeader = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = ID_Insert
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        returnValue = -1
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Function GetListSalesmanProfile(ByVal objSalesmanHeader As SalesmanHeader) As ArrayList
            Dim objFacade As SalesmanProfileFacade = New SalesmanProfileFacade(System.Threading.Thread.CurrentPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.ID", MatchType.Exact, objSalesmanHeader.ID))

            Dim objListSalesmanProfile As ArrayList = objFacade.Retrieve(criterias)

            Return objListSalesmanProfile
        End Function

        Public Function GetSalesmanProfile(ByVal objSalesmanHeader As SalesmanHeader, ByVal objGroup As ProfileGroup, ByVal objDomain As ProfileHeader) As SalesmanProfile
            Dim objFacade As SalesmanProfileFacade = New SalesmanProfileFacade(System.Threading.Thread.CurrentPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.ID", MatchType.Exact, objSalesmanHeader.ID))
            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, objDomain.ID))
            Dim objListSalesmanProfile As ArrayList = objFacade.Retrieve(criterias)
            If objListSalesmanProfile.Count > 0 Then
                Return CType(objListSalesmanProfile(0), SalesmanProfile)
            End If
            Return New SalesmanProfile
        End Function


        Public Function Update(ByVal objDomain As SalesmanHeader, ByVal objListProfile1 As ArrayList, ByVal objGroup1 As ProfileGroup) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    For Each item As SalesmanProfile In objListProfile1
                        item.SalesmanHeader = objDomain
                        Dim oldProfile1 As SalesmanProfile = GetSalesmanProfile(objDomain, objGroup1, item.ProfileHeader)
                        If oldProfile1.ID > 0 Then
                            For Each items As SalesmanProfileHistory In item.SalesmanProfileHistorys
                                items.SalesmanProfile = oldProfile1
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                            oldProfile1.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile1, m_userPrincipal.Identity.Name)
                        Else
                            item.SalesmanHeader = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            For Each items As SalesmanProfileHistory In item.SalesmanProfileHistorys
                                items.SalesmanProfile = item
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                            oldProfile1.ProfileValue = item.ProfileValue
                        End If

                    Next
                    'm_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    If performTransaction Then
                        UpdateWithTransaction(objDomain)
                        returnValue = 0
                    End If
                Catch ex As Exception
                    'Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    'If rethrow Then
                    '    Throw
                    'End If
                    Throw New Exception(ex.Message)
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        'Public Function Update(ByVal objDomain As KTB.DNet.Domain.SalesmanHeader, ByVal objListProfile As ArrayList, ByVal objGroup As ProfileGroup) As Integer
        '    Dim returnValue As Short = -1
        '    If (Me.IsTaskFree()) Then
        '        Try
        '            Me.SetTaskLocking()
        '            Dim performTransaction As Boolean = True
        '            Dim ObjMapper As IMapper
        '            For Each item As SalesmanProfile In objListProfile
        '                item.SalesmanHeader = objDomain
        '                Dim oldProfile As SalesmanProfile = GetSalesmanProfile(objDomain, objGroup, item.ProfileHeader)
        '                If oldProfile.ID > 0 Then
        '                    For Each items As SalesmanProfileHistory In item.SalesmanProfileHistorys
        '                        items.SalesmanProfile = oldProfile
        '                        m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
        '                    Next
        '                    oldProfile.ProfileValue = item.ProfileValue
        '                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
        '                Else
        '                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
        '                    For Each items As SalesmanProfileHistory In item.SalesmanProfileHistorys
        '                        items.SalesmanProfile = item
        '                        m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
        '                    Next
        '                    oldProfile.ProfileValue = item.ProfileValue
        '                End If
        '            Next
        '            m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
        '            If performTransaction Then
        '                m_TransactionManager.PerformTransaction()
        '                returnValue = 0
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
        '    Return returnValue
        'End Function

        Public Function Insert(ByVal objDomain As SalesmanHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                m_SalesmanHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        'Public Function Update(ByVal objDomain As SalesmanHeader) As Integer
        '    Dim nResult As Integer = -1
        '    Try
        '        nResult = m_SalesmanHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
        '        nResult = 0
        '    Catch ex As Exception
        '        nResult = -1
        '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
        '        If rethrow Then
        '            Throw
        '        End If
        '    End Try
        '    Return nResult
        'End Function

        Public Sub Delete(ByVal objDomain As SalesmanHeader)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SalesmanHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As SalesmanHeader) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_SalesmanHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        '24/Jul/2007 Deddy H    keperluan aggregate value
        Public Function RetrieveScalar(ByVal crit As CriteriaComposite, ByVal aggr As Aggregate) As Decimal
            Dim returnValue As Decimal = 0
            Try
                returnValue = CType(m_SalesmanHeaderMapper.RetrieveScalar(aggr, crit), Decimal)
            Catch ex As Exception
                returnValue = 0
            End Try
            Return returnValue
        End Function

        '26/Jul/2007 Deddy H    keperluan aggregate value , kembalian value dinamic
        Public Function RetrieveScalar(ByVal crit As CriteriaComposite, ByVal aggr As Aggregate, ByVal blnIsNumeric As Boolean) As String
            Dim returnValue As String = "0"
            Try
                If blnIsNumeric = True Then
                    returnValue = CType(CType(m_SalesmanHeaderMapper.RetrieveScalar(aggr, crit), Decimal), String)
                Else
                    returnValue = CType(m_SalesmanHeaderMapper.RetrieveScalar(aggr, crit), String)
                End If
            Catch ex As Exception
                returnValue = "0"
            End Try
            Return returnValue
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.SalesmanHeader) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.SalesmanHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.SalesmanHeader).MarkLoaded()
                ID_Insert = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is SalesmanProfile) Then
                CType(InsertArg.DomainObject, SalesmanProfile).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.SalesmanProfile).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is SalesmanProfileHistory) Then
                CType(InsertArg.DomainObject, SalesmanProfileHistory).ID = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is TrTrainee) Then
                CType(InsertArg.DomainObject, TrTrainee).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.TrTrainee).MarkLoaded()
                ID_Insert = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is SalesmanHeaderToDealer) Then
                CType(InsertArg.DomainObject, SalesmanHeaderToDealer).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.SalesmanHeaderToDealer).MarkLoaded()
            End If
        End Sub

        'Salesman Level 6 Agustus 2007

        Public Function Insert(ByVal objDomain As SalesmanLevel) As Integer
            Dim iReturn As Integer = 1
            Try
                m_SalesmanLevelMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Sub Delete(ByVal objDomain As SalesmanLevel)
            Dim nResult As Integer = 1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SalesmanLevelMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As SalesmanLevel) As Integer
            Dim iReturn As Integer = 1
            Try
                iReturn = m_SalesmanLevelMapper.Delete(objDomain)
            Catch ex As Exception
                iReturn = -1
            End Try
            Return iReturn
        End Function
        Public Function Update(ByVal objDomain As SalesmanLevel) As Integer
            Dim nResult As Integer = 1
            Try
                nResult = m_SalesmanLevelMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function
#End Region

#Region "Custom Method"
        'terpaksa dibuat karena bug dri source KTB
        Public Function UpdateCSTeamOnly(ByVal objDomain As SalesmanHeader, ByVal newArlProfile As ArrayList, ByVal profileGroup As ProfileGroup) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True


                    Dim oldarlProfile As ArrayList = GetListSalesmanProfile(objDomain)
                    Dim listProfile As List(Of SalesmanProfile) = oldarlProfile.Cast(Of SalesmanProfile).ToList()

                    Dim noKtp As String = String.Empty
                    Dim profileKTP As SalesmanProfile = listProfile.FirstOrDefault(Function(x) x.ProfileHeader.ID = 29)
                    Dim jobCtg As JobPositionCategory = New JobPositionCategoryFacade(m_userPrincipal).Retrieve(CInt(objDomain.SalesIndicator))

                    If Not IsNothing(profileKTP) Then
                        noKtp = profileKTP.ProfileValue
                    End If
                    If objDomain.Status <> CType(EnumSalesmanStatus.SalesmanStatus.Baru, String) Then
                        If noKtp <> String.Empty Then
                            Dim trTraineeFacade As TrTraineeFacade = New TrTraineeFacade(m_userPrincipal)

                            Dim arlTrainee As ArrayList = trTraineeFacade.GetTrTraineeByKTP(noKtp)
                            If arlTrainee.Count = 0 Then

                                Dim traineeData As New TrTrainee
                                SetTrTraineeBySalesmanHeader(traineeData, objDomain, newArlProfile.Cast(Of SalesmanProfile).ToList(), True)
                                Dim traineeSalesmanHeaderData As New TrTraineeSalesmanHeader
                                SetTraineeSalesmanHeader(traineeSalesmanHeaderData, traineeData, objDomain, jobCtg.AreaID)
                                m_TransactionManager.AddInsert(traineeData, m_userPrincipal.Identity.Name)
                                m_TransactionManager.AddInsert(traineeSalesmanHeaderData, m_userPrincipal.Identity.Name)

                            Else
                                Dim traineeData As TrTrainee = CType(arlTrainee(0), TrTrainee)
                                Dim dataLog As TrTraineeDataLog = MappingLogFromOldTrTrainee(traineeData)
                                SetTrTraineeBySalesmanHeader(traineeData, objDomain, newArlProfile.Cast(Of SalesmanProfile).ToList())
                                Dim traineeSalesmanHeaderData As TrTraineeSalesmanHeader = GetExistingTraineeSalesmanHeader(traineeData, objDomain)
                                SetTraineeSalesmanHeader(traineeSalesmanHeaderData, traineeData, objDomain, jobCtg.AreaID)
                                If traineeSalesmanHeaderData.ID = 0 Then
                                    m_TransactionManager.AddInsert(traineeSalesmanHeaderData, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddUpdate(traineeSalesmanHeaderData, m_userPrincipal.Identity.Name)
                                End If
                                m_TransactionManager.AddInsert(dataLog, m_userPrincipal.Identity.Name)
                                m_TransactionManager.AddUpdate(traineeData, m_userPrincipal.Identity.Name)
                            End If
                        End If
                    End If

                    For Each item As SalesmanProfile In newArlProfile
                        item.SalesmanHeader = objDomain
                        Dim oldProfile1 As SalesmanProfile = GetSalesmanProfile(objDomain, profileGroup, item.ProfileHeader)
                        If oldProfile1.ID > 0 Then
                            For Each items As SalesmanProfileHistory In item.SalesmanProfileHistorys
                                items.SalesmanProfile = oldProfile1
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                            oldProfile1.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile1, m_userPrincipal.Identity.Name)
                        Else
                            item.SalesmanHeader = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            For Each items As SalesmanProfileHistory In item.SalesmanProfileHistorys
                                items.SalesmanProfile = item
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                            oldProfile1.ProfileValue = item.ProfileValue
                        End If

                    Next

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

        Public Function Update(ByVal objDomain As SalesmanHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                returnValue = UpdateWithTransaction(objDomain)
            End If
            Return returnValue
        End Function

        Private Function UpdateWithTransaction(ByVal objDomain As SalesmanHeader) As Integer
            Dim returnValue As Integer = -1
            Try
                Me.SetTaskLocking()
                Dim performTransaction As Boolean = True


                Dim arlProfile As ArrayList = GetListSalesmanProfile(objDomain)
                Dim listProfile As List(Of SalesmanProfile) = arlProfile.Cast(Of SalesmanProfile).ToList()

                Dim noKtp As String = String.Empty
                Dim profileKTP As SalesmanProfile = listProfile.FirstOrDefault(Function(x) x.ProfileHeader.ID = 29)
                Dim jobCtg As JobPositionCategory = New JobPositionCategoryFacade(m_userPrincipal).Retrieve(CInt(objDomain.SalesIndicator))

                If Not IsNothing(profileKTP) Then
                    noKtp = profileKTP.ProfileValue
                End If
                If objDomain.Status <> CType(EnumSalesmanStatus.SalesmanStatus.Baru, String) Then
                    If noKtp <> String.Empty Then
                        Dim trTraineeFacade As TrTraineeFacade = New TrTraineeFacade(m_userPrincipal)

                        Dim arlTrainee As ArrayList = trTraineeFacade.GetTrTraineeByKTP(noKtp)
                        If arlTrainee.Count = 0 Then

                            Dim traineeData As New TrTrainee
                            SetTrTraineeBySalesmanHeader(traineeData, objDomain, listProfile, True)
                            Dim traineeSalesmanHeaderData As New TrTraineeSalesmanHeader
                            SetTraineeSalesmanHeader(traineeSalesmanHeaderData, traineeData, objDomain, jobCtg.AreaID)
                            m_TransactionManager.AddInsert(traineeData, m_userPrincipal.Identity.Name)
                            m_TransactionManager.AddInsert(traineeSalesmanHeaderData, m_userPrincipal.Identity.Name)

                        Else
                            Dim traineeData As TrTrainee = CType(arlTrainee(0), TrTrainee)

                            If objDomain.Status = CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String) Then
                                Dim listInvitationTraining As ArrayList = GetExistingActiveTraining(traineeData.ID)

                                For Each invitation As TrClassRegistration In listInvitationTraining
                                    invitation.Status = EnumTrClassRegistration.DataStatusType.Reject
                                    m_TransactionManager.AddUpdate(invitation, m_userPrincipal.Identity.Name)
                                Next

                                If String.IsNullOrEmpty(traineeData.JobPosition) Then
                                    traineeData.Status = CType(EnumTrTrainee.TrTraineeStatus.Deactive, String)
                                End If

                                'Else
                                '    If traineeData.Dealer.ID <> objDomain.Dealer.ID And traineeData.TrTraineeSalesmanHeader.Status = CType(EnumTrTrainee.TrTraineeStatus.Active, String) Then
                                '        Throw New Exception("Mohon maaf no KTP yang diproses telah terdaftar pada siswa training aktif di dealer yang berbeda.")
                                '    End If
                            End If

                            Dim dataLog As TrTraineeDataLog = MappingLogFromOldTrTrainee(traineeData)
                            SetTrTraineeBySalesmanHeader(traineeData, objDomain, listProfile)
                            Dim traineeSalesmanHeaderData As TrTraineeSalesmanHeader = GetExistingTraineeSalesmanHeader(traineeData, objDomain)
                            SetTraineeSalesmanHeader(traineeSalesmanHeaderData, traineeData, objDomain, jobCtg.AreaID)
                            If traineeSalesmanHeaderData.ID = 0 Then
                                m_TransactionManager.AddInsert(traineeSalesmanHeaderData, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddUpdate(traineeSalesmanHeaderData, m_userPrincipal.Identity.Name)
                            End If

                            m_TransactionManager.AddInsert(dataLog, m_userPrincipal.Identity.Name)
                            m_TransactionManager.AddUpdate(traineeData, m_userPrincipal.Identity.Name)
                        End If
                    End If
                End If

                If objDomain.SalesmanCode = "CS_Employee" Then
                    Dim func As New SalesmanHeaderToDealerFacade(m_userPrincipal)
                    Dim dtAsigneToCS As New SalesmanHeaderToDealer
                    Dim dtAsigne As List(Of SalesmanHeaderToDealer) = func.GetDatabySalesmanHeader(objDomain.ID)
                    If dtAsigne.Count = 0 Then
                        Dim crits As New CriteriaComposite(New Criteria(GetType(SalesmanHeaderToDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crits.opAnd(New Criteria(GetType(SalesmanHeaderToDealer), "Dealer.ID", MatchType.Exact, objDomain.Dealer.ID))
                        crits.opAnd(New Criteria(GetType(SalesmanHeaderToDealer), "SalesmanHeader.JobPosition.Code", MatchType.Exact, objDomain.JobPosition.Code))
                        crits.opAnd(New Criteria(GetType(SalesmanHeaderToDealer), "SalesmanHeader.ID", MatchType.No, objDomain.ID))

                        Dim arrSHTD As ArrayList = m_SalesmanHeaderToDealerMapper.RetrieveByCriteria(crits)
                        If arrSHTD.Count > 0 And objDomain.JobPosition.Code.ToUpper() = "CSO" Then
                            Dim dtSHTD As SalesmanHeaderToDealer = CType(arrSHTD(0), SalesmanHeaderToDealer)
                            dtSHTD.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(dtAsigneToCS, m_userPrincipal.Identity.Name)
                        End If

                        dtAsigneToCS.SalesmanHeader = objDomain
                        dtAsigneToCS.Dealer = objDomain.Dealer
                        dtAsigneToCS.IsMainDealer = True
                        dtAsigneToCS.Status = 1
                        Dim pUserName As New SalesmanProfile
                        If Not IsNothing(objDomain.SalesmanProfiles.Cast(Of SalesmanProfile).FirstOrDefault(Function(x) x.ProfileHeader.ID = 53)) Then
                            pUserName = objDomain.SalesmanProfiles.Cast(Of SalesmanProfile).FirstOrDefault(Function(x) x.ProfileHeader.ID = 53)
                            dtAsigneToCS.Username = pUserName.ProfileValue
                        End If
                        m_TransactionManager.AddInsert(dtAsigneToCS, m_userPrincipal.Identity.Name)
                    Else
                        dtAsigneToCS = dtAsigne.FirstOrDefault(Function(x) x.IsMainDealer = True)
                        Dim pUserName As New SalesmanProfile
                        If Not IsNothing(objDomain.SalesmanProfiles.Cast(Of SalesmanProfile).FirstOrDefault(Function(x) x.ProfileHeader.ID = 53)) Then
                            pUserName = objDomain.SalesmanProfiles.Cast(Of SalesmanProfile).FirstOrDefault(Function(x) x.ProfileHeader.ID = 53)
                            dtAsigneToCS.Username = pUserName.ProfileValue
                        End If
                        m_TransactionManager.AddUpdate(dtAsigneToCS, m_userPrincipal.Identity.Name)

                    End If
                End If
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
            Return returnValue
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim salesmanHeaderColl As ArrayList = m_SalesmanHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return salesmanHeaderColl
        End Function

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.SalesmanHeader, ByVal objListProfile As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    For Each item As SalesmanProfile In objListProfile
                        item.SalesmanHeader = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        For Each items As SalesmanProfileHistory In item.SalesmanProfileHistorys
                            items.SalesmanProfile = item
                            m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                        Next
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = ID_Insert
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        returnValue = -1
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Function InsertTransactionCS(ByVal objDomain As KTB.DNet.Domain.SalesmanHeader, ByVal arrHistory As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    'For Each item As SalesmanProfile In objListProfile
                    '    item.SalesmanHeader = objDomain
                    '    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    '    For Each items As SalesmanProfileHistory In item.SalesmanProfileHistorys
                    '        items.SalesmanProfile = item
                    '        m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                    '    Next
                    'Next

                    For Each item As SalesmanTraining In arrHistory
                        item.SalesmanHeader = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = ID_Insert
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        returnValue = -1
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Function RetrieveV_SalesmanCS(ByVal criterias As ICriteria) As ArrayList
            Return m_V_SalesmanCSMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function GetNoKTPByProfile(ByVal salesmanHeaderID As Integer) As String
            Dim result As String = String.Empty

            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29))
            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.ID", MatchType.Exact, salesmanHeaderID))

            Dim arlResult As ArrayList = New SalesmanProfileFacade(m_userPrincipal).Retrieve(criterias)

            If arlResult.Count > 0 Then
                result = CType(arlResult(0), SalesmanProfile).ProfileValue
            End If

            Return result
        End Function

        Public Sub DoEffectUserGroupMember(ByVal objSalesmanHeader As SalesmanHeader)
            Try
                Dim profileUserName As String = GetProfileUserName(objSalesmanHeader)
                Dim facade As UserGroupMemberFacade = New UserGroupMemberFacade(m_userPrincipal)

                If profileUserName <> "" Then
                    Dim UserData As UserInfo = GetUserInfo(profileUserName, objSalesmanHeader)
                    If UserData.ID <> 0 Then
                        Dim UserGroupMemberData As UserGroupMember = SetUserGroupMemberData(UserData, objSalesmanHeader.JobPosition)
                        Dim oldGroupMemberData As UserGroupMember = GetOldGroupMemberData(UserData)

                        If oldGroupMemberData.ID = 0 Then
                            facade.Insert(UserGroupMemberData)
                        Else
                            If UserGroupMemberData.UserGroup.ID <> oldGroupMemberData.UserGroup.ID Then
                                facade.Insert(UserGroupMemberData)
                                oldGroupMemberData.RowStatus = CType(DBRowStatus.Deleted, Short)
                                facade.Update(oldGroupMemberData)
                            End If
                        End If



                        'If oldGroupMemberData.UserGroup.ID <> UserGroupMemberData.UserGroup.ID Then
                        '    facade.Insert(UserGroupMemberData)
                        '    facade.DeleteFromDB(oldGroupMemberData)
                        'ElseIf UserGroupMemberData.ID = 0 Then
                        '    facade.Insert(UserGroupMemberData)
                        'End If

                    End If
                End If
            Catch ex As Exception
                Dim errorMessage = ex.Message
            End Try

        End Sub


        Private Function GetProfileUserName(objSalesmanHeader As SalesmanHeader) As String

            Dim result As String = ""

            Dim critUsername As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critUsername.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.Code", MatchType.Exact, "USER_DNET"))
            critUsername.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.ID", MatchType.Exact, objSalesmanHeader.ID))

            Dim arlProfile As ArrayList = New SalesmanProfileFacade(m_userPrincipal).Retrieve(critUsername)

            If arlProfile.Count > 0 Then
                result = CType(arlProfile(0), SalesmanProfile).ProfileValue
            End If

            Return result

        End Function

        Private Function GetUserInfo(profileUserName As String, objSalesmanHeader As SalesmanHeader) As UserInfo

            Dim result As New UserInfo

            Dim critUserProfile As New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critUserProfile.opAnd(New Criteria(GetType(UserInfo), "OrganizationID", MatchType.Exact, objSalesmanHeader.Dealer.ID))
            critUserProfile.opAnd(New Criteria(GetType(UserInfo), "UserName", MatchType.Exact, profileUserName))

            Dim arlResult As ArrayList = New UserInfoFacade(m_userPrincipal).Retrieve(critUserProfile)

            If arlResult.Count > 0 Then
                result = CType(arlResult(0), UserInfo)
            End If

            Return result

        End Function

#End Region

        Private Sub SetTrTraineeBySalesmanHeader(ByRef traineeData As TrTrainee, ByVal objDomain As SalesmanHeader, ByVal arlProfile As List(Of SalesmanProfile), Optional isNewData As Boolean = False)
            traineeData.SalesmanHeader = objDomain
            traineeData.Name = objDomain.Name
            traineeData.Dealer = objDomain.Dealer
            traineeData.DealerBranch = objDomain.DealerBranch
            traineeData.BirthDate = objDomain.DateOfBirth
            traineeData.Gender = objDomain.Gender
            traineeData.StartWorkingDate = objDomain.HireDate
            If traineeData.ID <> 0 Then
                Dim jobCtg As JobPositionCategory = New JobPositionCategoryFacade(m_userPrincipal).Retrieve(CInt(objDomain.SalesIndicator))

                If jobCtg.AreaID = 2 Then 'jika data terbaru adalah ASS maka harus update
                    traineeData.JobPosition = objDomain.JobPosition.Code

                    If objDomain.Status = EnumSalesmanStatus.SalesmanStatus.Aktif Then
                        traineeData.Status = EnumTrTrainee.TrTraineeStatus.Active
                    End If

                    If objDomain.Status = EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif Then
                        traineeData.Status = EnumTrTrainee.TrTraineeStatus.Deactive
                    End If
                End If
            Else
                If isNewData Then
                    traineeData.Status = EnumTrTrainee.TrTraineeStatus.Active
                    If objDomain.SalesIndicator = 0 Then
                        traineeData.JobPosition = objDomain.JobPosition.Code
                    End If
                End If
            End If
            traineeData.Photo = objDomain.Image

            Dim profileKTP As SalesmanProfile = arlProfile.FirstOrDefault(Function(x) x.ProfileHeader.ID = 29)
            If Not IsNothing(profileKTP) Then
                traineeData.NoKTP = profileKTP.ProfileValue
            End If

            Dim profileEmail As SalesmanProfile = arlProfile.FirstOrDefault(Function(x) x.ProfileHeader.ID = 26)
            If Not IsNothing(profileEmail) Then
                traineeData.Email = profileEmail.ProfileValue
            End If

            Dim profilePendidikan As SalesmanProfile = arlProfile.FirstOrDefault(Function(x) x.ProfileHeader.ID = 31)
            If Not IsNothing(profilePendidikan) Then
                traineeData.EducationLevel = profilePendidikan.ProfileValue
            End If

        End Sub

        Private Sub SetTraineeSalesmanHeader(ByRef data As TrTraineeSalesmanHeader, ByVal traineeData As TrTrainee, ByVal objDomain As SalesmanHeader, ByVal areaID As Integer)
            data.TrTrainee = traineeData
            data.SalesmanHeader = objDomain
            data.JobPosition = objDomain.JobPosition.Code
            data.JobPositionAreaID = areaID
            If objDomain.Status = EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif Then
                data.Status = 2
            Else
                data.Status = 1
            End If
        End Sub

        Private Function GetExistingTraineeSalesmanHeader(traineeData As TrTrainee, objDomain As SalesmanHeader) As TrTraineeSalesmanHeader
            Dim result As TrTraineeSalesmanHeader = New TrTraineeSalesmanHeader
            Dim jobCtg As JobPositionCategory = New JobPositionCategoryFacade(m_userPrincipal).Retrieve(CInt(objDomain.SalesIndicator))

            Dim criterias As New CriteriaComposite(New Criteria(GetType(TrTraineeSalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrTraineeSalesmanHeader), "TrTrainee.ID", MatchType.Exact, traineeData.ID))
            'criterias.opAnd(New Criteria(GetType(TrTraineeSalesmanHeader), "SalesmanHeader.ID", MatchType.Exact, objDomain.ID))
            criterias.opAnd(New Criteria(GetType(TrTraineeSalesmanHeader), "JobPositionAreaID", MatchType.Exact, jobCtg.AreaID))

            Dim arlResult As ArrayList = New TrTraineeSalesmanHeaderFacade(m_userPrincipal).Retrieve(criterias)

            If arlResult.Count > 0 Then
                result = CType(arlResult(0), TrTraineeSalesmanHeader)
            End If

            Return result
        End Function

        Private Function MappingLogFromOldTrTrainee(oldData As TrTrainee) As TrTraineeDataLog
            Dim log As New TrTraineeDataLog
            log.TrTrainee = oldData
            log.Name = oldData.Name
            log.SalesmanHeader = oldData.SalesmanHeader
            log.Dealer = oldData.Dealer
            log.DealerBranch = oldData.DealerBranch
            log.BirthDate = oldData.BirthDate
            log.Gender = oldData.Gender
            log.NoKTP = oldData.NoKTP
            log.Email = oldData.Email
            log.StartWorkingDate = oldData.StartWorkingDate
            log.Status = oldData.Status
            log.JobPositionCode = oldData.JobPosition
            log.Photo = oldData.Photo
            log.ShirtSize = oldData.ShirtSize
            log.RowStatus = 0

            Return log
        End Function

        Private Function GetExistingActiveTraining(traineeID As Integer) As ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.ID", MatchType.Exact, traineeID))
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CInt(EnumTrClassRegistration.DataStatusType.Invite)), "(", True)
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CInt(EnumTrClassRegistration.DataStatusType.Register)), ")", False)

            Dim arlCurrent As ArrayList = New TrClassRegistrationFacade(m_userPrincipal).Retrieve(criterias)
            Dim arlResult As New ArrayList
            For Each classRegis As TrClassRegistration In arlCurrent

                Dim criteriaCert As New CriteriaComposite(New Criteria(GetType(TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriaCert.opAnd(New Criteria(GetType(TrCertificateLine), "TrClassRegistration.ID", MatchType.Exact, classRegis.ID))

                Dim arlCertificate As ArrayList = New TrCertificateLineFacade(m_userPrincipal).Retrieve(criteriaCert)

                If arlCertificate.Count = 0 Then
                    arlResult.Add(classRegis)
                End If

            Next


            Return arlResult
        End Function



        Private Function SetUserGroupMemberData(UserData As UserInfo, jobPosition As JobPosition) As UserGroupMember
            Dim result As New UserGroupMember
            Dim listGroupCS As List(Of UserGroup) = New UserGroupFacade(m_userPrincipal).GetGroupCS().Cast(Of UserGroup).ToList()

            If listGroupCS.Count > 0 Then
                Dim userGroupData As New UserGroup
                If jobPosition.Code = "CSO" Then
                    userGroupData = listGroupCS.FirstOrDefault(Function(x) x.Code = "CS_CSO")
                ElseIf jobPosition.Code = "CS_Sales" Then
                    userGroupData = listGroupCS.FirstOrDefault(Function(x) x.Code = "CS_SLS")
                ElseIf jobPosition.Code = "CS_ASS" Then
                    userGroupData = listGroupCS.FirstOrDefault(Function(x) x.Code = "CS_ASS")
                End If

                result.UserGroup = userGroupData
                result.UserInfo = UserData
            End If

            Return result
        End Function

        Private Function GetOldGroupMemberData(UserData As UserInfo) As UserGroupMember
            Dim criterias As New CriteriaComposite(New Criteria(GetType(UserGroupMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(UserGroupMember), "UserGroup.Code", MatchType.InSet, "('CS_CSO','CS_SLS','CS_ASS')"))
            criterias.opAnd(New Criteria(GetType(UserGroupMember), "UserInfo.ID", MatchType.Exact, UserData.ID))

            Dim arlResult As ArrayList = New UserGroupMemberFacade(m_userPrincipal).Retrieve(criterias)

            If arlResult.Count = 0 Then
                Return New UserGroupMember
            Else
                Return CType(arlResult(0), UserGroupMember)
            End If
        End Function



    End Class

End Namespace



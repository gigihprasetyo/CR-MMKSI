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
'// Generated on 9/26/2005 - 2:38:25 PM
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
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.BusinessFacade.General
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class SparePartPOFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartPOMapper As IMapper
        Private m_V_SparePartPOSummaryMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartPOMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartPO).ToString)
            Me.m_V_SparePartPOSummaryMapper = MapperFactory.GetInstance.GetMapper(GetType(V_SparePartPOSummary).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(SparePartPO))
            Me.DomainTypeCollection.Add(GetType(SparePartPODetail))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartPO
            Return CType(m_SparePartPOMapper.Retrieve(ID), SparePartPO)
        End Function

        Public Function Retrieve(ByVal pONumber As String) As SparePartPO
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartPO), "PONumber", MatchType.Exact, pONumber))
            Dim arlPO As ArrayList = m_SparePartPOMapper.RetrieveByCriteria(criterias)
            If arlPO.Count > 0 Then
                Return CType(arlPO(0), SparePartPO)
            End If
            Return Nothing
        End Function


        Public Function RetrievePO(ByVal pONumber As String) As SparePartPO
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartPO), "PONumber", MatchType.Exact, pONumber))
            Dim arlPO As ArrayList = New ArrayList
            arlPO = m_SparePartPOMapper.RetrieveByCriteria(criterias)
            If arlPO.Count > 0 Then
                Return CType(arlPO(0), SparePartPO)
            ElseIf (Strings.Left(pONumber, 1).ToUpper() = "I" OrElse Strings.Left(pONumber, 1).ToUpper() = "A") Then
                Dim TmpPO As String = ""
                If (Strings.Left(pONumber, 1).ToUpper() = "I") Then
                    TmpPO = "A" + pONumber.Substring(1, pONumber.Length - 1)
                ElseIf (Strings.Left(pONumber, 1).ToUpper() = "A") Then
                    TmpPO = "A" + pONumber.Substring(1, pONumber.Length - 1)
                End If
                criterias = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SparePartPO), "PONumber", MatchType.Exact, TmpPO))
                arlPO = m_SparePartPOMapper.RetrieveByCriteria(criterias)
                If arlPO.Count > 0 Then
                    Return CType(arlPO(0), SparePartPO)
                End If
            End If
            Return Nothing
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartPOMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartPOMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartPOMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartPOMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartPOMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SparePartPO As ArrayList = m_SparePartPOMapper.RetrieveByCriteria(criterias)
            Return _SparePartPO
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartPOColl As ArrayList = m_SparePartPOMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartPOColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartPOColl As ArrayList = m_SparePartPOMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SparePartPOColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SparePartPO), SortColumn, sortDirection))

            Dim SparePartPOColl As ArrayList = m_SparePartPOMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SparePartPOColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartPOColl As ArrayList = m_SparePartPOMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartPOColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartPOColl As ArrayList = m_SparePartPOMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SparePartPO), columnName, matchOperator, columnValue))
            Return SparePartPOColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), columnName, matchOperator, columnValue))

            Return m_SparePartPOMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveListByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If Not IsNothing(sortColumn) And sortColumn <> "" Then
                sortColl.Add(New Sort(GetType(SparePartPO), sortColumn, sortDirection))

                If sortColumn.ToUpper() = "TermOfPayment.ID".ToUpper() Then
                    Dim sSQL As String = GetRetrieveSpSortByTOP(criterias, sortColl, pageNumber, pageSize, totalRow)
                    Dim result As ArrayList = m_SparePartPOMapper.RetrieveSP(sSQL)
                    totalRow = GetRowCount(criterias)
                    Return result
                End If

            Else
                sortColl = Nothing
            End If

            Return m_SparePartPOMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveListSummaryByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If Not IsNothing(sortColumn) And sortColumn <> "" Then
                sortColl.Add(New Sort(GetType(V_SparePartPOSummary), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_SparePartPOSummaryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveSummary(ByVal criterias As ICriteria) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(V_SparePartPOSummary), "ID", Sort.SortDirection.ASC))
            Return m_V_SparePartPOSummaryMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function GetAggregateResult(ByVal aggregate As IAggregate, ByVal criteria As ICriteria) As Int64
            Dim result As Object = m_V_SparePartPOSummaryMapper.RetrieveScalar(aggregate, criteria)
            If result Is System.DBNull.Value Then
                Return 0
            Else
                Return CType(result, Int64)
            End If
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function createInSetCriteria(ByVal arl As ArrayList, ByVal szFieldName As String) As Criteria
            Dim _sz As String = "("
            For Each item As String In arl
                _sz = _sz & "'" & item & "',"
            Next
            _sz = _sz.Substring(0, _sz.Length - 1) & ")"
            If (_sz = "('')") Then
                Return New Criteria(GetType(SparePartPO), szFieldName, MatchType.StartsWith, "%")
            Else
                Return New Criteria(GetType(SparePartPO), szFieldName, MatchType.InSet, _sz)
            End If
        End Function

        Public Function InsertSparePartPO(ByVal objDomain As KTB.DNet.Domain.SparePartPO, ByVal arrPODetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If arrPODetail.Count > 0 Then
                        For Each objPODetail As SparePartPODetail In arrPODetail
                            objPODetail.SparePartPO = objDomain
                            m_TransactionManager.AddInsert(objPODetail, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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

        Public Function InsertSparePartPO(ByVal objDomain As KTB.DNet.Domain.SparePartPO, ByVal objPODetail As SparePartPODetail) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    objPODetail.SparePartPO = objDomain
                    m_TransactionManager.AddInsert(objPODetail, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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

        Public Function InsertSparePartPODetail(ByVal objDomain As KTB.DNet.Domain.SparePartPO, ByVal objPODetail As SparePartPODetail) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objPODetail, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = objPODetail.ID
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


        Public Function UpdateSparePartPO(ByVal objDomain As KTB.DNet.Domain.SparePartPO, ByVal arrPODetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrPODetail.Count > 0 Then
                        For Each objPODetail As SparePartPODetail In arrPODetail
                            If IsNothing(New SparePartPODetailFacade(System.Threading.Thread.CurrentPrincipal).ValidateItem(objDomain.ID, objPODetail.SparePartMaster.PartNumber)) Then
                                objPODetail.SparePartPO = objDomain
                                m_TransactionManager.AddInsert(objPODetail, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddUpdate(objPODetail, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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

        Public Function UpdateSparePartPO(ByVal objDomain As KTB.DNet.Domain.SparePartPO, ByVal objPODetail As SparePartPODetail) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddUpdate(objPODetail, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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

        Public Function UpdateSparePartPO(ByVal arrSPPO As ArrayList, ByVal poDate As Date) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    For Each objPO As SparePartPO In arrSPPO
                        objPO.PODate = poDate
                        m_TransactionManager.AddUpdate(objPO, m_userPrincipal.Identity.Name)
                    Next
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

        Public Function UpdateSparePartPO(ByVal arrSPPO As ArrayList, ByVal newProcessStatus As String) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    For Each objPO As SparePartPO In arrSPPO
                        objPO.ProcessCode = newProcessStatus
                        m_TransactionManager.AddUpdate(objPO, m_userPrincipal.Identity.Name)
                    Next
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

        Public Function Insert(ByVal objDomain As SparePartPO) As Integer
            Dim iReturn As Integer = -2
            Try
                m_SparePartPOMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartPO) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartPOMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartPO)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SparePartPOMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SparePartPO)
            Try
                m_SparePartPOMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is SparePartPO) Then
                CType(InsertArg.DomainObject, SparePartPO).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SparePartPO).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is SparePartPODetail) Then
                CType(InsertArg.DomainObject, SparePartPODetail).ID = InsertArg.ID
            End If
        End Sub

#End Region

#Region "Custom Method"

        Private Function AggreateForTotalAmount() As Aggregate
            Dim aggregates As New Aggregate(GetType(SparePartPO), "ID", AggregateType.Count)
            Return aggregates
        End Function

        Public Function TotalAmount(ByVal criteria As ICriteria, ByVal aggregate As IAggregate) As Integer
            Return CType(m_SparePartPOMapper.RetrieveScalar(aggregate, criteria), Integer)
        End Function


        Public Function UpdateSPPOProcessCode(ByVal objDomain As SparePartPO) As Integer
            Dim retValue As Integer = -1
            If Me.IsTaskFree() Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    retValue = 1
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return retValue  '-- Return status
        End Function

        Public Function UpdateSPPOChecklist(ByVal objSPartPO As SparePartPO) As Integer
            '-- Update checklist status of all sparepartPO details

            If IsNothing(objSPartPO) Then  '-- If sparepart PO doesn't exist then return 0
                Return 0  '-- PO doesn't exist
            End If

            Dim retValue As Integer = -1  '-- Assume not successful at first

            If Me.IsTaskFree() Then
                Try
                    Me.SetTaskLocking()

                    '-- Update checklist status of all sparepartPO details one-by-one
                    For Each SPPODetail As SparePartPODetail In objSPartPO.SparePartPODetails
                        ''SPPODetail.SparePartPO = objSPartPO  '-- Assign its parent object

                        '-- Update each sparepartPO detail
                        m_TransactionManager.AddUpdate(SPPODetail, m_userPrincipal.Identity.Name)
                    Next

                    m_TransactionManager.PerformTransaction()
                    retValue = 1  '-- Successfully update sparepart PO details

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If

                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If

            Return retValue  '-- Return status

        End Function

        Public Function InsertAndOrUpdateSPPO(ByVal objSPartPO As SparePartPO) As Integer
            '-- Insert and/or update Sparepart PO header and/or details

            Dim retValue As Integer = -1  '-- Assume not successful at first

            If Me.IsTaskFree() Then
                Try
                    Me.SetTaskLocking()

                    If Not isExistPOHead(objSPartPO) Then  '-- Check to see if the Sparepart PO header already exists
                        '-- If not exist then insert PO header
                        m_TransactionManager.AddInsert(objSPartPO, m_userPrincipal.Identity.Name)

                        '-- Insert PO details if any
                        InsertPODetails(objSPartPO)

                        m_TransactionManager.PerformTransaction()
                        retValue = 1  '-- Successfully insert new sparepart PO

                    Else
                        '-- The PO header exist
                        '-- Then check if it is still active (i.e. not canceled nor submitted)
                        If Not isCancelOrSubmit(objSPartPO) Then

                            '-- Retrieve and assign PO header's ID first
                            AssignPOHeaderID(objSPartPO)

                            '-- Delete physically all PO details of the sparepart PO
                            ''DeletePODetails(objSPartPO)

                            '-- Insert PO details if any
                            ''InsertPODetails(objSPartPO)

                            '-- Insert or update PO details
                            InsertOrUpdatePODetails(objSPartPO)

                            '-- Update PO header
                            m_TransactionManager.AddUpdate(objSPartPO, m_userPrincipal.Identity.Name)

                            m_TransactionManager.PerformTransaction()
                            retValue = 2  '-- Successfully update sparepart PO

                        Else
                            retValue = 0  '-- Can not update sparepart PO
                        End If
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

            Return retValue  '-- Return status

        End Function

        Private Function isExistPOHead(ByVal objSPartPO As SparePartPO) As Boolean
            '-- Check to see if the Sparepart PO header already exists

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "PONumber", MatchType.Exact, objSPartPO.PONumber))
            Dim _sppo As ArrayList = New SparePartPOFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)
            Return _sppo.Count <> 0

        End Function

        Private Function isCancelOrSubmit(ByVal objSPartPO As SparePartPO) As Boolean
            '-- Check to see if the Sparepart PO header already C-canceled or S-submitted

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "PONumber", MatchType.Exact, objSPartPO.PONumber))
            Dim _sppo As ArrayList = New SparePartPOFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)

            '-- Return True if already C-canceled or S-submitted
            Return CType(_sppo(0), SparePartPO).ProcessCode = "C" _
                OrElse CType(_sppo(0), SparePartPO).ProcessCode = "P" _
                OrElse (CType(_sppo(0), SparePartPO).ProcessCode = "S" And Left(CType(_sppo(0), SparePartPO).PONumber, 1) = "E")

        End Function

        Public Sub AssignPOHeaderID(ByVal objSPartPO As SparePartPO)
            '-- Retrieve and assign PO header's ID first

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "PONumber", MatchType.Exact, objSPartPO.PONumber))
            Dim _sppo As ArrayList = New SparePartPOFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)

            objSPartPO.ID = CType(_sppo(0), SparePartPO).ID  '-- Assign PO header's ID

        End Sub

        ''Private Sub DeletePODetails(ByVal objSPartPO As SparePartPO)
        ''    '-- Delete physically all PO details of the sparepart PO

        ''    '-- First, retrieve all PO details
        ''    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPODetail), "SparePartPO.PONumber", MatchType.Exact, objSPartPO.PONumber))
        ''    Dim _sppoDetails As ArrayList = New SparePartPODetailFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)

        ''    '-- Then delete them one-by-one
        ''    For Each objPODetail As SparePartPODetail In _sppoDetails

        ''        '-- Delete sparepart PO detail one-by-one
        ''        m_TransactionManager.AddDelete(objPODetail)
        ''    Next

        ''End Sub

        Private Sub InsertPODetails(ByVal objSPartPO As SparePartPO)
            '-- Insert PO details if any

            For Each objPODetail As SparePartPODetail In objSPartPO.SparePartPODetails
                objPODetail.SparePartPO = objSPartPO  '-- Assign its parent object

                '-- Insert sparepart PO detail one-by-one
                m_TransactionManager.AddInsert(objPODetail, m_userPrincipal.Identity.Name)
            Next

        End Sub

        Private Sub InsertOrUpdatePODetails(ByVal objSPartPO As SparePartPO)
            '-- Insert or update PO detail

            '-- First delete unmatched old records
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPODetail), "SparePartPO.PONumber", MatchType.Exact, objSPartPO.PONumber))
            Dim _sppoDetails As ArrayList = New SparePartPODetailFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)

            For Each objPODetail As SparePartPODetail In _sppoDetails

                If Not isExists(objPODetail, objSPartPO) Then
                    '-- Delete sparepart PO detail
                    objPODetail.RowStatus = DBRowStatus.Deleted
                    m_TransactionManager.AddUpdate(objPODetail, m_userPrincipal.Identity.Name)
                End If
            Next

            '-- Second insert new or update old records
            For Each objPODetail As SparePartPODetail In objSPartPO.SparePartPODetails
                objPODetail.SparePartPO = objSPartPO  '-- Its PO header

                If isExistPODetail(objPODetail) Then
                    '-- Set its ID and its parents' objects
                    SetPODetailID(objPODetail)

                    '-- Update sparepart PO detail
                    m_TransactionManager.AddUpdate(objPODetail, m_userPrincipal.Identity.Name)

                Else
                    '-- Insert sparepart PO detail
                    m_TransactionManager.AddInsert(objPODetail, m_userPrincipal.Identity.Name)

                End If
            Next

        End Sub

        Private Function isExists(ByVal objPODetail As SparePartPODetail, ByVal objSPartPO As SparePartPO) As Boolean
            '-- Return True if objPODetail is in objSPartPO, otherwise return False

            Dim bExists As Boolean = False

            For Each item As SparePartPODetail In objSPartPO.SparePartPODetails
                If objPODetail.SparePartMaster.PartNumber = item.SparePartMaster.PartNumber Then
                    bExists = True
                    Exit For
                End If
            Next

            Return bExists
        End Function

        Private Function isExistPODetail(ByVal objPODetail As SparePartPODetail) As Boolean
            '-- Check to see if the Sparepart PO detail already exists

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPODetail), "SparePartPO.PONumber", MatchType.Exact, objPODetail.SparePartPO.PONumber))
            criterias.opAnd(New Criteria(GetType(SparePartPODetail), "SparePartMaster.PartNumber", MatchType.Exact, objPODetail.SparePartMaster.PartNumber))

            Dim _sppo As ArrayList = New SparePartPODetailFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)
            Return _sppo.Count <> 0

        End Function

        Private Sub SetPODetailID(ByVal objPODetail As SparePartPODetail)
            '-- Set its ID and its parents' objects

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPODetail), "SparePartPO.PONumber", MatchType.Exact, objPODetail.SparePartPO.PONumber))
            criterias.opAnd(New Criteria(GetType(SparePartPODetail), "SparePartMaster.PartNumber", MatchType.Exact, objPODetail.SparePartMaster.PartNumber))
            Dim _sppoDetails As ArrayList = New SparePartPODetailFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)
            objPODetail.ID = _sppoDetails(0).ID  '-- Set its ID

            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "PONumber", MatchType.Exact, objPODetail.SparePartPO.PONumber))
            Dim _sppoHeader As ArrayList = New SparePartPOFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias2)
            objPODetail.SparePartPO = _sppoHeader(0)  '-- Set its PO header

            Dim criterias3 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, objPODetail.SparePartMaster.PartNumber))
            Dim _spMaster As ArrayList = New SparePartMasterFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias3)
            objPODetail.SparePartMaster = _spMaster(0)  '-- Set its spare part master

        End Sub

        'Public Sub RecordStatusChangeHistory(ByVal item As SparePartPO, ByVal oldStatus As Integer)
        '    Dim objStatusChangeHistoryFacade As StatusChangeHistoryFacade
        '    If Not item Is Nothing Then
        '        objStatusChangeHistoryFacade = New StatusChangeHistoryFacade(m_userPrincipal)
        '        objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.PO_Harian), item.PONumber, oldStatus, item.LookUpStatus)
        '    End If
        'End Sub

        Private Function GetRetrieveSpSortByTOP(ByVal Criterias As ICriteria, ByVal Sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As String
            Dim sSQL As String = "EXEC up_PagingQuery "
            sSQL &= "@Table = N'SparePartPO', "
            sSQL &= "@PK = N'ID', "
            sSQL &= "@PageSize = " & pageSize & ", "
            sSQL &= "@PageNumber = " & pageNumber & ", "
            sSQL &= "@Filter = N' INNER JOIN Dealer ON SparePartPO.DealerID = Dealer.ID LEFT JOIN TermOfPayment ON SparePartPO.TermOfPaymentID = TermOfPayment.ID "
            sSQL &= Criterias.ToString().Replace("'", "''").Replace("INNER JOIN Dealer ON SparePartPO.DealerID = Dealer.ID", "")
            sSQL &= "', @Sort = N'" & Sorts.ToString() & "'"

            Return sSQL
        End Function

        Private Function GetRowCount(ByVal Criterias As ICriteria) As Integer
            Dim agg As Aggregate = New Aggregate(GetType(SparePartPO), "ID", AggregateType.Count)

            Return CType(m_SparePartPOMapper.RetrieveScalar(agg, Criterias), Integer)
        End Function

        Public Function RetrieveDataTableForSendMail() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim spName As String = "sp_SPBO_GetBodyEmailNotification"
            ds = m_SparePartPOMapper.RetrieveDataSet(spName)
            If ds.Tables.Count > 0 Then
                dt = ds.Tables(0)
            End If
            Return dt
        End Function

#End Region

    End Class

End Namespace

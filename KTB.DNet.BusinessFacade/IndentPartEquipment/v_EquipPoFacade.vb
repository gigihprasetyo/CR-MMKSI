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
'// Generated on 8/2/2007 - 12:59:07 PM
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
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNET.BusinessFacade.IndentPartEquipment


    Public Class v_EquipPOFacade
        Inherits AbstractFacade


#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_v_EquipPoMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_v_EquipPoMapper = MapperFactory.GetInstance.GetMapper(GetType(v_EquipPo).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(v_EquipPo))
            Me.DomainTypeCollection.Add(GetType(IndentPartPO))

        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrieveSearch(ByVal objSearch As v_EquipPOSearch) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_EquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If (objSearch.bytePaymentType <> 0) Then
                criterias.opAnd(New Criteria(GetType(v_EquipPO), "PaymentType", MatchType.Exact, CType(objSearch.bytePaymentType, Byte)))
            End If

            If (objSearch.dtmFrom <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, Date)) Then
                criterias.opAnd(New Criteria(GetType(v_EquipPO), "CreatedTime", MatchType.GreaterOrEqual, CType(objSearch.dtmFrom, Date)))
            End If

            If (objSearch.dtmTo <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, Date)) Then
                criterias.opAnd(New Criteria(GetType(v_EquipPO), "CreatedTime", MatchType.LesserOrEqual, CType(objSearch.dtmTo.AddDays(1), Date)))
            End If

            If (objSearch.arlDealerCode.Count > 0) Then
                criterias.opAnd(createInSetCriteria(objSearch.arlDealerCode, "DealerCode"))
            End If

            If (objSearch.arlEstNo.Count > 0) Then
                criterias.opAnd(createInSetCriteria(objSearch.arlEstNo, "EstimationNumber"))
            End If

            If (objSearch.arlProcessCode.Count > 0) Then
                criterias.opAnd(createInSetCriteria(objSearch.arlProcessCode, "Status"))
            End If

            If (objSearch.arlSPPONo.Count > 0) Then
                criterias.opAnd(createInSetCriteria(objSearch.arlSPPONo, "RequestNo"))
            End If

            Return Retrieve(criterias)
        End Function

        Public Function RetrieveCriterias(ByVal objSearch As v_EquipPOSearch, Optional ByVal DealerTitle As Short = EnumDealerTittle.DealerTittle.DEALER) As CriteriaComposite
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_EquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If (objSearch.bytePaymentType <> 0) Then
                criterias.opAnd(New Criteria(GetType(v_EquipPO), "PaymentType", MatchType.Exact, CType(objSearch.bytePaymentType, Byte)))
            End If

            If (objSearch.dtmFrom <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, Date)) Then
                criterias.opAnd(New Criteria(GetType(v_EquipPO), "CreatedTime", MatchType.GreaterOrEqual, CType(objSearch.dtmFrom, Date)))
            End If

            If (objSearch.dtmTo <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, Date)) Then
                criterias.opAnd(New Criteria(GetType(v_EquipPO), "CreatedTime", MatchType.LesserOrEqual, CType(objSearch.dtmTo.AddDays(1), Date)))
            End If

            If (objSearch.arlDealerCode.Count > 0) Then
                criterias.opAnd(createInSetCriteria(objSearch.arlDealerCode, "DealerCode"))
            End If

            If (objSearch.arlEstNo.Count > 0) Then
                criterias.opAnd(createInSetCriteria(objSearch.arlEstNo, "EstimationNumber"))
            End If

            If (objSearch.arlProcessCode.Count > 0) Then
                If DealerTitle <> EnumDealerTittle.DealerTittle.KTB Then
                    criterias.opAnd(createInSetCriteria(objSearch.arlProcessCode, "Status"))
                Else
                    criterias.opAnd(createInSetCriteria(objSearch.arlProcessCode, "StatusKTB"))
                End If
            End If

            If (objSearch.arlSPPONo.Count > 0) Then
                criterias.opAnd(createInSetCriteria(objSearch.arlSPPONo, "RequestNo"))
            End If

            Return criterias
        End Function

        Public Function Retrieve(ByVal ID As Integer) As v_EquipPO
            Return CType(m_v_EquipPoMapper.Retrieve(ID), v_EquipPO)
        End Function

        Public Function Retrieve(ByVal RequestNo As String) As v_EquipPO
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_EquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(v_EquipPO), "RequestNo", MatchType.Exact, RequestNo))

            Dim v_EquipPOColl As ArrayList = m_v_EquipPoMapper.RetrieveByCriteria(criterias)
            If (v_EquipPOColl.Count > 0) Then
                Return CType(v_EquipPOColl(0), v_EquipPO)
            End If
            Return New v_EquipPO
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_v_EquipPoMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_v_EquipPoMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_v_EquipPoMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(v_EquipPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_v_EquipPoMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(v_EquipPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_v_EquipPoMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_EquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _v_EquipPO As ArrayList = m_v_EquipPoMapper.RetrieveByCriteria(criterias)
            Return _v_EquipPO
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_EquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim v_EquipPOColl As ArrayList = m_v_EquipPoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return v_EquipPOColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(v_EquipPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ClaimReasonColl As ArrayList = m_v_EquipPoMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ClaimReasonColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(v_EquipPO), SortColumn, sortDirection))

            Dim v_EquipPOColl As ArrayList = m_v_EquipPoMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return v_EquipPOColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim v_EquipPOColl As ArrayList = m_v_EquipPoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return v_EquipPOColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_EquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim v_EquipPOColl As ArrayList = m_v_EquipPoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(v_EquipPO), columnName, matchOperator, columnValue))
            Return v_EquipPOColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(v_EquipPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_EquipPO), columnName, matchOperator, columnValue))

            Return m_v_EquipPoMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As v_EquipPO) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_v_EquipPOMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                Throw
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal arlIPH As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arlIPH.Count > 0 Then
                        For Each objIPHH As v_EquipPO In arlIPH
                            m_TransactionManager.AddUpdate(objIPHH, m_userPrincipal.Identity.Name)
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

        Public Function Update(ByVal objDomain As v_EquipPO) As Integer
            Dim nResult As Integer = -1
            Try
                Return m_v_EquipPOMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function Delete(ByVal objDomain As v_EquipPO) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                Return m_v_EquipPOMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function DeleteFromDB(ByVal objDomain As v_EquipPO) As Integer
            Dim nResult As Integer = -1
            Try
                Return m_v_EquipPOMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function Deletev_EquipPO(ByVal objDomain As KTB.DNet.Domain.v_EquipPO, ByVal arrIPPO As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arrIPPO.Count > 0 Then
                        For Each objIPPO As v_EquipPO In arrIPPO
                            m_TransactionManager.AddDelete(objIPPO)
                        Next
                    End If

                    m_TransactionManager.AddDelete(objDomain)
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

            If (TypeOf InsertArg.DomainObject Is v_EquipPO) Then
                CType(InsertArg.DomainObject, v_EquipPO).ID = InsertArg.ID
                CType(InsertArg.DomainObject, v_EquipPO).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is v_EquipPO) Then
                CType(InsertArg.DomainObject, v_EquipPO).ID = InsertArg.ID
            End If
        End Sub

#End Region

#Region "Custom Method"

        Private Function createInSetCriteria(ByVal arl As ArrayList, ByVal szFieldName As String) As Criteria
            Dim _sz As String = "("
            For Each item As String In arl
                _sz = _sz & "'" & item & "',"
            Next
            _sz = _sz.Substring(0, _sz.Length - 1) & ")"
            If (_sz = "('')") Then
                Return New Criteria(GetType(v_EquipPO), szFieldName, MatchType.StartsWith, "%")
            Else
                Return New Criteria(GetType(v_EquipPO), szFieldName, MatchType.InSet, _sz)
            End If
        End Function

#End Region

    End Class

End Namespace


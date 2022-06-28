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
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/3/2005 - 10:53:00 AM
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

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class VehicleKindGroupFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_VehicleKindGroupMapper As IMapper
        Private m_VehicleKindMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private objTransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_VehicleKindGroupMapper = MapperFactory.GetInstance().GetMapper(GetType(VehicleKindGroup).ToString)
            Me.m_VehicleKindMapper = MapperFactory.GetInstance().GetMapper(GetType(VehicleKind).ToString)

            Me.objTransactionManager = New TransactionManager
            AddHandler objTransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.VehicleKindGroup))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.VehicleKind))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As VehicleKindGroup
            Return CType(m_VehicleKindGroupMapper.Retrieve(ID), VehicleKindGroup)
        End Function

        Public Function Retrieve(ByVal VehicleKindGroupCode As String) As VehicleKindGroup
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKindGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VehicleKindGroup), "Description", MatchType.Exact, VehicleKindGroupCode))

            Dim VehicleKindGroupColl As ArrayList = m_VehicleKindGroupMapper.RetrieveByCriteria(criterias)
            If (VehicleKindGroupColl.Count > 0) Then
                Return CType(VehicleKindGroupColl(0), VehicleKindGroup)
            End If
            Return New VehicleKindGroup
        End Function

        Public Function RetrieveByCode(ByVal VehicleKindGroupCode As String) As VehicleKindGroup
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKindGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, VehicleKindGroupCode))

            Dim VehicleKindGroupColl As ArrayList = m_VehicleKindGroupMapper.RetrieveByCriteria(criterias)
            If (VehicleKindGroupColl.Count > 0) Then
                Return CType(VehicleKindGroupColl(0), VehicleKindGroup)
            End If
            Return New VehicleKindGroup
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_VehicleKindGroupMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_VehicleKindGroupMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_VehicleKindGroupMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VehicleKindGroup), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VehicleKindGroupMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VehicleKindGroup), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VehicleKindGroupMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKindGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("Code")) Then
                sortColl.Add(New Sort(GetType(VehicleKindGroup), "Code", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _VehicleKindGroup As ArrayList = m_VehicleKindGroupMapper.RetrieveByCriteria(criterias, sortColl)
            Return _VehicleKindGroup
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKindGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim VehicleKindGroupColl As ArrayList = m_VehicleKindGroupMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return VehicleKindGroupColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim VehicleKindGroupColl As ArrayList = m_VehicleKindGroupMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return VehicleKindGroupColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKindGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VehicleKindGroup), columnName, matchOperator, columnValue))
            Dim VehicleKindGroupColl As ArrayList = m_VehicleKindGroupMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return VehicleKindGroupColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VehicleKindGroup), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKindGroup), columnName, matchOperator, columnValue))

            Return m_VehicleKindGroupMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKindGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VehicleKindGroup), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VehicleKindGroupMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As VehicleKindGroup) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_VehicleKindGroupMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As VehicleKindGroup) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_VehicleKindGroupMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As VehicleKindGroup)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_VehicleKindGroupMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As VehicleKindGroup)
            Try
                m_VehicleKindGroupMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.VehicleKindGroup) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.VehicleKindGroup).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.VehicleKindGroup).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is VehicleKind) Then
                CType(InsertArg.DomainObject, VehicleKind).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.VehicleKind).MarkLoaded()
            End If
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKindGroup), "VehicleKindGroupCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(VehicleKindGroup), "VehicleKindGroupCode", AggregateType.Count)

            Return CType(m_VehicleKindGroupMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_VehicleKindGroupMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VehicleKindGroup), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim VehicleKindGroupColl As ArrayList = m_VehicleKindGroupMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return VehicleKindGroupColl
        End Function

        Public Function GetDataVehicleKindGroupOld(ByVal oVehicleKindGroup As VehicleKindGroup) As VehicleKindGroup
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, oVehicleKindGroup.Code))
            Dim arlVehicleKindGroup As ArrayList = m_VehicleKindGroupMapper.RetrieveByCriteria(criteria)
            If arlVehicleKindGroup.Count > 0 Then
                Return CType(arlVehicleKindGroup(0), VehicleKindGroup)
            End If
            Return Nothing
        End Function

        Public Function GetDataVehicleKindOld(ByVal _RowStatusBefore As Short) As ArrayList
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKind), "RowStatus", MatchType.Exact, _RowStatusBefore))
            Dim arlVehicleKind As ArrayList = m_VehicleKindMapper.RetrieveByCriteria(criteria)
            If Not IsNothing(arlVehicleKind) AndAlso arlVehicleKind.Count > 0 Then
                Return arlVehicleKind
            End If
            Return New ArrayList
        End Function

        Public Function GetDataVehicleKindGroupOld(ByVal _RowStatusBefore As Short) As ArrayList
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKindGroup), "RowStatus", MatchType.Exact, _RowStatusBefore))
            Dim arlVehicleKindGroup As ArrayList = m_VehicleKindGroupMapper.RetrieveByCriteria(criteria)
            If Not IsNothing(arlVehicleKindGroup) AndAlso arlVehicleKindGroup.Count > 0 Then
                Return arlVehicleKindGroup
            End If
            Return New ArrayList
        End Function

        Public Function InsertFromWebSevice(ByVal oVehicleKindGroup As VehicleKindGroup) As Short
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim oVehicleKindGroupFacade As VehicleKindGroupFacade
            Dim returnValue As Integer = -1

            If Me.IsTaskFree() Then
                Try
                    '  Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim oVehicleKindGroup_old As VehicleKindGroup = GetDataVehicleKindGroupOld(oVehicleKindGroup)

                    If IsNothing(oVehicleKindGroup_old) Then
                        objTransactionManager.AddInsert(oVehicleKindGroup, m_userPrincipal.Identity.Name)
                        If oVehicleKindGroup.VehicleKinds.Count > 0 Then
                            For Each itemDetail As VehicleKind In oVehicleKindGroup.VehicleKinds
                                itemDetail.VehicleKindGroup = oVehicleKindGroup
                                objTransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    Else
                        For Each itemDetail As VehicleKind In oVehicleKindGroup.VehicleKinds
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(VehicleKind), "VehicleKindGroup.ID", MatchType.Exact, oVehicleKindGroup_old.ID))
                            criterias.opAnd(New Criteria(GetType(VehicleKind), "Code", MatchType.Exact, itemDetail.Code))
                            Dim arlVehicleKinds As ArrayList = New VehicleKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)).Retrieve(criterias)

                            If arlVehicleKinds.Count > 0 Then
                                Dim objVehicleKind As VehicleKind = CType(arlVehicleKinds(0), VehicleKind)

                                If objVehicleKind.Code.Trim <> itemDetail.Code.Trim OrElse
                                    objVehicleKind.Description.Trim <> itemDetail.Description.Trim OrElse
                                    objVehicleKind.RowStatus <> CType(DBRowStatus.Active, Short) Then

                                    objVehicleKind.Code = itemDetail.Code
                                    objVehicleKind.Description = itemDetail.Description
                                    objVehicleKind.RowStatus = CType(DBRowStatus.Active, Short)
                                    objTransactionManager.AddUpdate(objVehicleKind, m_userPrincipal.Identity.Name)
                                End If

                            Else
                                itemDetail.RowStatus = CType(DBRowStatus.Active, Short)
                                itemDetail.VehicleKindGroup = oVehicleKindGroup_old
                                objTransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                            End If
                        Next

                        If oVehicleKindGroup_old.Code.Trim <> oVehicleKindGroup.Code.Trim OrElse
                                oVehicleKindGroup_old.Description.Trim <> oVehicleKindGroup.Description.Trim OrElse
                                oVehicleKindGroup_old.RowStatus <> CType(DBRowStatus.Active, Short) Then

                            ''--- Process Update VehicleKindGroup 
                            oVehicleKindGroup_old.Code = oVehicleKindGroup.Code.Trim
                            oVehicleKindGroup_old.Description = oVehicleKindGroup.Description.Trim
                            oVehicleKindGroup_old.RowStatus = DBRowStatus.Active
                            objTransactionManager.AddUpdate(oVehicleKindGroup_old, m_userPrincipal.Identity.Name)
                        End If
                    End If

                    If performTransaction Then
                        objTransactionManager.PerformTransaction()
                        returnValue = 0
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    '    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Function UpdateRowStatusVehicleKind(ByVal _RowStatusBefore As Short, ByVal _RowStatusAfter As Short) As Short
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim oVehicleKindGroupFacade As VehicleKindGroupFacade
            Dim returnValue As Integer = -1

            If Me.IsTaskFree() Then
                Try
                    '  Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim arrVehicleKindOld As ArrayList = GetDataVehicleKindOld(_RowStatusBefore)
                    'update rowstatus = -1 for old detail
                    For Each itemDetail_Old As VehicleKind In arrVehicleKindOld
                        itemDetail_Old.RowStatus = _RowStatusAfter
                        objTransactionManager.AddUpdate(itemDetail_Old, m_userPrincipal.Identity.Name)
                    Next

                    Dim _arrVehicleKindGroup_old As ArrayList = GetDataVehicleKindGroupOld(_RowStatusBefore)
                    'update rowstatus = -1 for old Header
                    For Each oVehicleKindGroupOld As VehicleKindGroup In _arrVehicleKindGroup_old
                        oVehicleKindGroupOld.RowStatus = _RowStatusAfter
                        objTransactionManager.AddUpdate(oVehicleKindGroupOld, m_userPrincipal.Identity.Name)
                    Next

                    If performTransaction Then
                        objTransactionManager.PerformTransaction()
                        returnValue = 0
                    End If

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    '    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

#End Region

    End Class

End Namespace

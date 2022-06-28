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
'// Copyright  2020
'// ---------------------
'// $History      : $
'// Generated on 7/30/2020 - 12:45:10 PM
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

Imports Intimedia.PersistentFramework.Domain
Imports Intimedia.PersistentFramework.Domain.Search
Imports Intimedia.PersistentFramework.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain
Imports System.Web.UI.WebControls

#End Region

Namespace KTB.DNet.BusinessFacade

    Public Class FleetCustomerDetailMappingFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_FleetCustomerDetailMappingMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_FleetCustomerDetailMappingMapper = MapperFactory.GetInstance.GetMapper(GetType(FleetCustomerDetailMapping).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(FleetCustomerDetailMapping))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As FleetCustomerDetailMapping
            Return CType(m_FleetCustomerDetailMappingMapper.Retrieve(ID), FleetCustomerDetailMapping)
        End Function

        Public Function Retrieve(ByVal Code As String) As FleetCustomerDetailMapping
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerDetailMapping), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FleetCustomerDetailMapping), "FleetCustomerDetailMappingCode", MatchType.Exact, Code))

            Dim FleetCustomerDetailMappingColl As ArrayList = m_FleetCustomerDetailMappingMapper.RetrieveByCriteria(criterias)
            If (FleetCustomerDetailMappingColl.Count > 0) Then
                Return CType(FleetCustomerDetailMappingColl(0), FleetCustomerDetailMapping)
            End If
            Return New FleetCustomerDetailMapping
        End Function

        Public Function Retrieve(ByVal oFleetCustomerHeader As FleetCustomerHeader) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerDetailMapping), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FleetCustomerDetailMapping), "FleetCustomerHeader.ID", MatchType.Exact, oFleetCustomerHeader.ID))

            Return m_FleetCustomerDetailMappingMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_FleetCustomerDetailMappingMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_FleetCustomerDetailMappingMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_FleetCustomerDetailMappingMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FleetCustomerDetailMapping), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FleetCustomerDetailMappingMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FleetCustomerDetailMapping), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FleetCustomerDetailMappingMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerDetailMapping), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _FleetCustomerDetailMapping As ArrayList = m_FleetCustomerDetailMappingMapper.RetrieveByCriteria(criterias)
            Return _FleetCustomerDetailMapping
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerDetailMapping), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FleetCustomerDetailMappingColl As ArrayList = m_FleetCustomerDetailMappingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FleetCustomerDetailMappingColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FleetCustomerDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim _FleetCustomerDetailMapping As ArrayList = m_FleetCustomerDetailMappingMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return _FleetCustomerDetailMapping
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim FleetCustomerDetailMappingColl As ArrayList = m_FleetCustomerDetailMappingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return FleetCustomerDetailMappingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerDetailMapping), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FleetCustomerDetailMappingColl As ArrayList = m_FleetCustomerDetailMappingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(FleetCustomerDetailMapping), columnName, matchOperator, columnValue))
            Return FleetCustomerDetailMappingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FleetCustomerDetailMapping), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerDetailMapping), columnName, matchOperator, columnValue))

            Return m_FleetCustomerDetailMappingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerDetailMapping), "FleetCustomerDetailMappingCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(FleetCustomerDetailMapping), "FleetCustomerDetailMappingCode", AggregateType.Count)
            Return CType(m_FleetCustomerDetailMappingMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is FleetCustomerDetailMapping) Then
                CType(InsertArg.DomainObject, FleetCustomerDetailMapping).ID = InsertArg.ID
                CType(InsertArg.DomainObject, FleetCustomerDetailMapping).MarkLoaded()
            End If
        End Sub

        Function UpdateTransaction(ByVal objFleetCustomerHeaderNew As FleetCustomerHeader, ByVal arlFleetCustomerDetailMappingNew As ArrayList, ByVal arlFleetCustomerDetailMappingOld As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim isExistDataFleetNew As Boolean = False
                    If Not IsNothing(arlFleetCustomerDetailMappingNew) Then
                        If arlFleetCustomerDetailMappingNew.Count > 0 Then
                            isExistDataFleetNew = True
                        End If
                    End If

                    If isExistDataFleetNew = True Then
                        If Not IsNothing(arlFleetCustomerDetailMappingOld) Then
                            If arlFleetCustomerDetailMappingOld.Count > 0 Then
                                For Each item As FleetCustomerDetailMapping In arlFleetCustomerDetailMappingOld
                                    item.RowStatus = DBRowStatus.Deleted
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Next
                            End If
                        End If
                    End If

                    If Not IsNothing(arlFleetCustomerDetailMappingNew) Then
                        If arlFleetCustomerDetailMappingNew.Count > 0 Then
                            For Each item As FleetCustomerDetailMapping In arlFleetCustomerDetailMappingNew
                                item.FleetCustomerHeader = objFleetCustomerHeaderNew
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 7
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


        Public Function Insert(ByVal objDomain As FleetCustomerDetailMapping) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_FleetCustomerDetailMappingMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As FleetCustomerDetailMapping) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_FleetCustomerDetailMappingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As FleetCustomerDetailMapping)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_FleetCustomerDetailMappingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As FleetCustomerDetailMapping)
            Try
                m_FleetCustomerDetailMappingMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

    End Class

End Namespace

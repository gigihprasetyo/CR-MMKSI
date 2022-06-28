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

    Public Class FleetCustomerHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_FleetCustomerHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_FleetCustomerHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(FleetCustomerHeader).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(FleetCustomerHeader))
            Me.DomainTypeCollection.Add(GetType(FleetCustomerDetail))
            Me.DomainTypeCollection.Add(GetType(FleetCustomerDetailMapping))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As FleetCustomerHeader
            Return CType(m_FleetCustomerHeaderMapper.Retrieve(ID), FleetCustomerHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As FleetCustomerHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FleetCustomerHeader), "FleetCustomerHeaderCode", MatchType.Exact, Code))

            Dim FleetCustomerHeaderColl As ArrayList = m_FleetCustomerHeaderMapper.RetrieveByCriteria(criterias)
            If (FleetCustomerHeaderColl.Count > 0) Then
                Return CType(FleetCustomerHeaderColl(0), FleetCustomerHeader)
            End If
            Return New FleetCustomerHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_FleetCustomerHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_FleetCustomerHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_FleetCustomerHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FleetCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FleetCustomerHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FleetCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FleetCustomerHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FleetCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim _FleetCustomerHeader As ArrayList = m_FleetCustomerHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return _FleetCustomerHeader
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _FleetCustomerHeader As ArrayList = m_FleetCustomerHeaderMapper.RetrieveByCriteria(criterias)
            Return _FleetCustomerHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FleetCustomerHeaderColl As ArrayList = m_FleetCustomerHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FleetCustomerHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim FleetCustomerHeaderColl As ArrayList = m_FleetCustomerHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return FleetCustomerHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FleetCustomerHeaderColl As ArrayList = m_FleetCustomerHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(FleetCustomerHeader), columnName, matchOperator, columnValue))
            Return FleetCustomerHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FleetCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerHeader), columnName, matchOperator, columnValue))

            Return m_FleetCustomerHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"
        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerHeader), "FleetCustomerHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(FleetCustomerHeader), "FleetCustomerHeaderCode", AggregateType.Count)
            Return CType(m_FleetCustomerHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Update(ByVal objDomain As FleetCustomerHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_FleetCustomerHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function UpdateTransactionDelete(ByVal objfleetcustomerheader As FleetCustomerHeader, ByVal arrFleetCustomerDetails As ArrayList, ByVal arrFleetCustomerDetailMappings As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrFleetCustomerDetails.Count > 0 Then
                        For Each item As FleetCustomerDetail In arrFleetCustomerDetails
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrFleetCustomerDetailMappings.Count > 0 Then
                        For Each item As FleetCustomerDetailMapping In arrFleetCustomerDetailMappings
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objfleetcustomerheader, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objfleetcustomerheader.ID
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

            If (TypeOf InsertArg.DomainObject Is FleetCustomerHeader) Then
                CType(InsertArg.DomainObject, FleetCustomerHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, FleetCustomerHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is FleetCustomerDetail) Then
                CType(InsertArg.DomainObject, FleetCustomerDetail).ID = InsertArg.ID

            ElseIf (TypeOf InsertArg.DomainObject Is FleetCustomerDetailMapping) Then
                CType(InsertArg.DomainObject, FleetCustomerDetailMapping).ID = InsertArg.ID
            End If
        End Sub
#End Region

#Region "Custom Method"
        Public Function UpdateTransactionDetail(ByVal objFleetCustomerHeader As FleetCustomerHeader, ByVal arrFleetCustomerDetailEdit As ArrayList, ByVal arrFleetCustomerDetailDelete As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrFleetCustomerDetailDelete.Count > 0 Then
                        For Each item As FleetCustomerDetail In arrFleetCustomerDetailDelete
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrFleetCustomerDetailEdit.Count > 0 Then
                        For Each item As FleetCustomerDetail In arrFleetCustomerDetailEdit
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objFleetCustomerHeader, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objFleetCustomerHeader.ID
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

    End Class

End Namespace

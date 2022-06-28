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
'// Author Name: Ronny Teguh P.<ronny@intimediatalents.com>
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 8/2/2007 - 1:07:49 PM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Reflection
#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNET.BusinessFacade.Event
Imports KTB.DNET.BusinessFacade.General

#End Region

Namespace KTB.DNET.BusinessFacade.Event
    Public Class EventSalesFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_EventSalesMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"
        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_EventSalesMapper = MapperFactory.GetInstance.GetMapper(GetType(EventSales).ToString)
        End Sub
#End Region

#Region "Retrieve"
        Public Function Retrieve(ByVal ID As Integer) As EventSales
            Return CType(m_EventSalesMapper.Retrieve(ID), EventSales)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventSales), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim EventSalesColl As ArrayList = m_EventSalesMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return EventSalesColl
        End Function
#End Region

#Region "Transaction/Other Public Method"
        Public Function Insert(ByVal objDomain As EventSales) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_EventSalesMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                Throw
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As EventSales) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_EventSalesMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As EventSales)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_EventSalesMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As EventSales) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_EventSalesMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.EventInfo) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.EventInfo).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.EventInfo).MarkLoaded()
            End If
        End Sub

#End Region
        
#Region "Custom Method"
        Public Function ValidateItem(ByVal nEventInfoID As Integer, ByVal strVehicle As String) As EventSales
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventSales), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EventSales), "EventInfo.ID ", MatchType.Exact, nEventInfoID))
            criterias.opAnd(New Criteria(GetType(EventSales), "VehicleType.VehicleTypeCode", MatchType.Exact, strVehicle))
            Dim arlEventSales As ArrayList = m_EventSalesMapper.RetrieveByCriteria(criterias)
            If arlEventSales.Count > 0 Then
                Return CType(arlEventSales(0), EventSales)
            Else
                Return Nothing
            End If
        End Function
#End Region

    End Class

End Namespace

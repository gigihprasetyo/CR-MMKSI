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
'// Generated on 12/19/2005 - 4:19:33 PM
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

Namespace KTB.DNet.BusinessFacade

    Public Class PRPSenderInfoFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PRPSenderInfo As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_PRPSenderInfo = MapperFactory.GetInstance.GetMapper(GetType(PRPSenderInfo).ToString)
            Me.DomainTypeCollection.Add(GetType(PRPSenderInfo))
            Me.DomainTypeCollection.Add(GetType(PRPReceiverInfo))
            Me.m_TransactionManager = New TransactionManager
            AddHandler Me.m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PRPSenderInfo
            Return CType(m_PRPSenderInfo.Retrieve(ID), PRPSenderInfo)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PRPSenderInfo.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PRPSenderInfo.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PRPSenderInfo.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PRPSenderInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PRPSenderInfo.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PRPSenderInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PRPSenderInfo.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPSenderInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PRPSenderInfo As ArrayList = m_PRPSenderInfo.RetrieveByCriteria(criterias)
            Return _PRPSenderInfo
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPSenderInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PRPSenderInfoColl As ArrayList = m_PRPSenderInfo.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PRPSenderInfoColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PRPSenderInfoColl As ArrayList = m_PRPSenderInfo.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PRPSenderInfoColl
        End Function
        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPSenderInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PRPSenderInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PRPSenderInfo.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PRPSenderInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PRPSenderInfoColl As ArrayList = m_PRPSenderInfo.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PRPSenderInfoColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPSenderInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PRPSenderInfoColl As ArrayList = m_PRPSenderInfo.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PRPSenderInfo), columnName, matchOperator, columnValue))
            Return PRPSenderInfoColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PRPSenderInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPSenderInfo), columnName, matchOperator, columnValue))

            Return m_PRPSenderInfo.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is PRPSenderInfo) Then

                CType(InsertArg.DomainObject, PRPSenderInfo).ID = InsertArg.ID
                CType(InsertArg.DomainObject, PRPSenderInfo).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is PRPReceiverInfo) Then

                CType(InsertArg.DomainObject, PRPReceiverInfo).ID = InsertArg.ID

            End If

        End Sub

        Public Function Insert(ByVal objDomain As PRPSenderInfo) As Integer
            Dim returnValue As Integer = -2
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    objDomain.CreatedBy = m_userPrincipal.Identity.Name
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    _user = m_userPrincipal.Identity.Name
                    For Each item As PRPReceiverInfo In objDomain.PRPReceiverInfos
                        item.PRPSenderInfo = objDomain
                        m_TransactionManager.AddInsert(item, _user)
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
                    End If
                Catch ex As Exception
                    returnValue = -1
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

        Public Function Update(ByVal objDomain As PRPSenderInfo) As Integer
            Dim nResult As Integer = -2
            Try
                nResult = m_PRPSenderInfo.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As PRPSenderInfo) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_PRPSenderInfo.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean
            End Try
            Return nResult
        End Function
#End Region

#Region "Custom Method"

#End Region
    End Class
End Namespace

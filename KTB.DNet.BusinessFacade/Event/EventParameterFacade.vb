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
'// Author Name   : Ariwibawa
'// PURPOSE       : Facade for Page Event - Parameter Event
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2009 
'// ---------------------
'// $History      : $
'// Generated on 8/26/2009 - 11:26:00 AM
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

Namespace KTB.DNet.BusinessFacade.Event
    Public Class EventParameterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_EventParameterMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_EventParameterMapper = MapperFactory.GetInstance().GetMapper(GetType(EventParameter).ToString)
            Me.m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrieveSearch(ByVal obj As EventParameter) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EventParameter), "EventDateStart", MatchType.Exact, obj.EventDateStart))
            criterias.opAnd(New Criteria(GetType(EventParameter), "EventDateEnd", MatchType.Exact, obj.EventDateEnd))
            criterias.opAnd(New Criteria(GetType(EventParameter), "EventName", MatchType.Exact, obj.EventName))
            criterias.opAnd(New Criteria(GetType(EventParameter), "EventYear", MatchType.Exact, obj.EventYear))
            If (Not IsNothing(obj.Dealer)) Then
                criterias.opAnd(New Criteria(GetType(EventParameter), "Dealer", MatchType.Exact, obj.Dealer.ID))
            End If
            If (Not IsNothing(obj.SalesmanArea)) Then
                criterias.opAnd(New Criteria(GetType(EventParameter), "SalesmanArea", MatchType.Exact, obj.SalesmanArea.ID))
            End If
            If (Not IsNothing(obj.ActivityType)) Then
                criterias.opAnd(New Criteria(GetType(EventParameter), "ActivityType", MatchType.Exact, obj.ActivityType.ID))
            End If
            If (Not IsNothing(obj.Category)) Then
                criterias.opAnd(New Criteria(GetType(EventParameter), "Category", MatchType.Exact, obj.Category.ID))
            End If
            If (Not IsNothing(obj.VechileType)) Then
                criterias.opAnd(New Criteria(GetType(EventParameter), "VechileType", MatchType.Exact, obj.VechileType.ID))
            End If
            If (obj.ID > 0) Then
                criterias.opAnd(New Criteria(GetType(EventParameter), "ID", MatchType.No, obj.ID))
            End If
            Return m_EventParameterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function IsExist(ByVal obj As EventParameter) As Boolean
            Dim arl As ArrayList = RetrieveSearch(obj)
            If (IsNothing(arl) Or arl.Count <= 0) Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Function RetrieveByObjectData(ByVal obj As EventParameter) As EventParameter
            Dim arl As ArrayList = RetrieveSearch(obj)
            If (IsNothing(arl) Or arl.Count <= 0) Then
                Return Nothing
            Else
                Return arl(0)
            End If
        End Function

        Public Function Retrieve(ByVal ID As Integer) As EventParameter
            Return CType(m_EventParameterMapper.Retrieve(ID), EventParameter)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_EventParameterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_EventParameterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_EventParameterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventParameter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventParameterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventParameter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventParameterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _EventParameter As ArrayList = m_EventParameterMapper.RetrieveByCriteria(criterias)
            Return _EventParameter
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EventParameterColl As ArrayList = m_EventParameterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EventParameterColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventParameter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim EventParameterColl As ArrayList = m_EventParameterMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return EventParameterColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
            ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventParameter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_EventParameterMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventParameter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventParameterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim EventParameterColl As ArrayList = m_EventParameterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EventParameterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EventParameter), columnName, matchOperator, columnValue))
            Dim EventParameterColl As ArrayList = m_EventParameterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EventParameterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventParameter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventParameter), columnName, matchOperator, columnValue))

            Return m_EventParameterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objCollection As ArrayList) As Integer
            Dim iReturn As Integer = -1
            If MyBase.IsTaskFree Then
                Try
                    MyBase.SetTaskLocking()
                    Dim performTransaction As Boolean = True

                    For Each objDomain As EventParameter In objCollection
                        m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        iReturn = objCollection.Count
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    MyBase.RemoveTaskLocking()
                End Try
            End If
            Return iReturn
        End Function

        Public Function Insert(ByVal objDomain As EventParameter) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_EventParameterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                Throw
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objCollection As ArrayList) As Integer
            Dim iReturn As Integer = -1
            If MyBase.IsTaskFree Then
                Try
                    MyBase.SetTaskLocking()
                    Dim performTransaction As Boolean = True

                    For Each objDomain As EventParameter In objCollection
                        m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        iReturn = objCollection.Count
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    MyBase.RemoveTaskLocking()
                End Try
            End If
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As EventParameter) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_EventParameterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As EventParameter) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_EventParameterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As EventParameter) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_EventParameterMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

#End Region

#Region "Custom Method"
        Private Function GenerateDateCriteria(ByVal nDate As Date, ByVal startDate As Boolean) As DateTime
            Dim Hour As Integer
            Dim Minute As Integer
            Dim Second As Integer

            If startDate Then
                Hour = 0
                Minute = 0
                Second = 0
            Else
                Hour = 23
                Minute = 59
                Second = 59
            End If

            Return New DateTime(nDate.Year, nDate.Month, nDate.Day, Hour, Minute, Second)
        End Function
        Public Function RetrieveNamaKegiatan(ByVal ActivityTypeID As Integer) As ArrayList
            Return RetrieveNamaKegiatan(ActivityTypeID, "")
        End Function
        Public Function RetrieveNamaKegiatan(ByVal ActivityTypeID As Integer, ByVal DealerCode As String) As ArrayList
            Dim crit As New CriteriaComposite(New Criteria(GetType(EventParameter), "RowStatus", CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(EventParameter), "ActivityType", ActivityTypeID))
            If DealerCode.Length > 0 Then
                crit.opAnd(New Criteria(GetType(EventParameter), "Dealer.DealerCode", DealerCode))
            End If
            Dim objArray As ArrayList = m_EventParameterMapper.RetrieveByCriteria(crit)
            Dim hashCheck As New Hashtable
            For Each eventParam As EventParameter In objArray
                If Not hashCheck.ContainsKey(eventParam.EventName) Then
                    hashCheck.Add(eventParam.EventName, eventParam)
                End If
            Next
            objArray.Clear()
            For Each name As EventParameter In hashCheck.Values
                objArray.Add(name)
            Next
            Return objArray
        End Function
#End Region

    End Class
End Namespace
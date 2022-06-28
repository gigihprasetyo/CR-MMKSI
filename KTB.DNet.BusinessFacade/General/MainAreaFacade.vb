
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
'// Copyright  2016
'// ---------------------
'// $History      : $
'// Generated on 11/11/2016 - 14:17:10
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

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Framework
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data.SqlClient


#End Region

Namespace KTB.DNET.BusinessFacade.General

    Public Class MainAreaFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_MainAreaMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_MainAreaMapper = MapperFactory.GetInstance.GetMapper(GetType(MainArea).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As MainArea
            Return CType(m_MainAreaMapper.Retrieve(ID), MainArea)
        End Function

        Public Function Retrieve(ByVal Code As String) As MainArea
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MainArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MainArea), "AreaCode", MatchType.Exact, Code))

            Dim MainAreaColl As ArrayList = m_MainAreaMapper.RetrieveByCriteria(criterias)
            If (MainAreaColl.Count > 0) Then
                Return CType(MainAreaColl(0), MainArea)
            End If
            Return New MainArea
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MainAreaMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MainAreaMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_MainAreaMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MainArea), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MainAreaMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MainArea), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MainAreaMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MainArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MainArea As ArrayList = m_MainAreaMapper.RetrieveByCriteria(criterias)
            Return _MainArea
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MainArea), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_MainAreaMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MainArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MainArea), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MainAreaMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MainArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MainAreaColl As ArrayList = m_MainAreaMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MainAreaColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(MainArea), SortColumn, sortDirection))
            Dim MainAreaColl As ArrayList = m_MainAreaMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MainAreaColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MainAreaColl As ArrayList = m_MainAreaMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MainAreaColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MainArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MainAreaColl As ArrayList = m_MainAreaMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(MainArea), columnName, matchOperator, columnValue))
            Return MainAreaColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MainArea), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MainArea), columnName, matchOperator, columnValue))

            Return m_MainAreaMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MainArea), "AreaCode", MatchType.Exact, Code))
            crit.opAnd(New Criteria(GetType(MainArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim agg As Aggregate = New Aggregate(GetType(MainArea), "AreaCode", AggregateType.Count)
            Return CType(m_MainAreaMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Sub InsertWithTransactionManagerHandler(ByVal sender As Object, ByVal args As TransactionManager.OnInsertArgs)

            ' set the object ID from db returned id
            If (args.DomainObject.GetType = GetType(MainArea)) Then
                CType(args.DomainObject, MainArea).ID = args.ID
                CType(args.DomainObject, MainArea).MarkLoaded()
            ElseIf (args.DomainObject.GetType = GetType(Area1)) Then
                CType(args.DomainObject, Area1).ID = args.ID
                CType(args.DomainObject, Area1).MarkLoaded()
            ElseIf (args.DomainObject.GetType = GetType(Area2)) Then
                CType(args.DomainObject, Area2).ID = args.ID
                CType(args.DomainObject, Area2).ID = args.ID
            End If

        End Sub

        Public Function InsertWithTransactionManager(ByVal mainArea As MainArea, ByVal area1List As ArrayList, ByVal area2List As ArrayList) As Integer
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
            If Me.IsTaskFree Then
                Try
                    Me.SetTaskLocking()

                    ' add command to insert mainArea
                    Me.m_TransactionManager.AddInsert(mainArea, m_userPrincipal.Identity.Name)
                    ' add command to insert area1
                    For Each area1 As Area1 In area1List
                        If area1.ID <> 0 Then
                            Me.m_TransactionManager.AddUpdate(area1, m_userPrincipal.Identity.Name)
                        Else
                            Me.m_TransactionManager.AddInsert(area1, m_userPrincipal.Identity.Name)
                        End If

                    Next
                    ' add command to insert area2
                    For Each area2 As Area2 In area2List
                        If area2.ID <> 0 Then
                            Me.m_TransactionManager.AddUpdate(area2, m_userPrincipal.Identity.Name)
                        Else
                            Me.m_TransactionManager.AddInsert(area2, m_userPrincipal.Identity.Name)
                        End If

                    Next

                    Me.m_TransactionManager.PerformTransaction()
                    result = mainArea.ID

                    Return result
                Catch sqlException As SqlException
                    Throw sqlException
                Catch ex As Exception
                    Throw ex
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return result

        End Function

        Public Function UpdateWithTransactionManager(ByVal mainArea As MainArea, ByVal area1List As ArrayList, ByVal area2List As ArrayList) As Integer
            ' mark as loaded to prevent it loads from db
            mainArea.MarkLoaded()
            For Each area1 As Area1 In area1List
                area1.MarkLoaded()
            Next

            For Each area2 As Area2 In area2List
                area2.MarkLoaded()
            Next
            ' set default result
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
            If Me.IsTaskFree Then
                Try
                    Me.SetTaskLocking()

                    ' add command to update Area 1
                    For Each area1 As Area1 In area1List
                        If area1.ID <> 0 Then
                            If (area1.LastUpdateBy.ToLower <> "not update") Then
                                m_TransactionManager.AddUpdate(area1, m_userPrincipal.Identity.Name)
                            End If
                        Else
                            m_TransactionManager.AddInsert(area1, m_userPrincipal.Identity.Name)
                        End If

                        area1.MarkLoaded()
                    Next

                    ' add command to update Area 2
                    For Each area2 As Area2 In area2List
                        If area2.ID <> 0 Then
                            If (area2.LastUpdateBy.ToLower <> "not update") Then
                                m_TransactionManager.AddUpdate(area2, m_userPrincipal.Identity.Name)
                            End If
                        Else
                            m_TransactionManager.AddInsert(area2, m_userPrincipal.Identity.Name)
                        End If

                        area2.MarkLoaded()
                    Next

                    ' add command to update province
                    If (mainArea.LastUpdateBy.ToLower <> "not update") Then
                        m_TransactionManager.AddUpdate(mainArea, m_userPrincipal.Identity.Name)
                    End If
                    m_TransactionManager.PerformTransaction()

                    result = mainArea.ID
                Catch ex As Exception
                    Throw ex
                Finally
                    Me.RemoveTaskLocking()
                End Try

            End If

            Return result
        End Function

        Public Function Insert(ByVal objDomain As MainArea) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_MainAreaMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As MainArea) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_MainAreaMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As MainArea)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_MainAreaMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        'Public Function DeleteFromDB(ByVal objDomain As MainArea) As Integer
        '    Dim nResult As Integer = -1
        '    Try
        '        nResult = m_MainAreaMapper.Delete(objDomain)
        '    Catch ex As Exception
        '        nResult = -1
        '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '        If rethrow Then
        '            Throw
        '        End If
        '    End Try
        '    Return nResult
        'End Function

        Public Sub DeleteFromDB(ByVal objDomain As MainArea)
            Try
                m_MainAreaMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace


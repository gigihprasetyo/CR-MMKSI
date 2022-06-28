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

Namespace KTB.DNet.BusinessFacade.General
    Public Class JobPositionFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_JobPositionMapper As IMapper
        Private m_JobPositionToMenuMapper As IMapper
        Private m_TransactionManager As TransactionManager
        Private ID_Insert As Integer
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_JobPositionMapper = MapperFactory.GetInstance().GetMapper(GetType(JobPosition).ToString)
            Me.m_JobPositionToMenuMapper = MapperFactory.GetInstance().GetMapper(GetType(JobPositionToMenu).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.JobPosition))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.JobPositionToMenu))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As JobPosition
            Return CType(m_JobPositionMapper.Retrieve(ID), JobPosition)
        End Function

        Public Function Retrieve(ByVal JobPositionCode As String) As JobPosition
            Dim objReturnValue As JobPosition
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(JobPosition), "Code", MatchType.Exact, JobPositionCode))
            Dim CityList As ArrayList
            CityList = m_JobPositionMapper.RetrieveByCriteria(criterias)
            If Not CityList Is Nothing Then
                If CityList.Count > 0 Then
                    objReturnValue = CType(CityList.Item(0), JobPosition)
                End If
            End If
            Return objReturnValue
        End Function

        Public Function RetrieveNotActive(ByVal JobPositionCode As String) As JobPosition
            Dim objReturnValue As JobPosition
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Deleted, Short)))
            criterias.opAnd(New Criteria(GetType(JobPosition), "Code", MatchType.Exact, JobPositionCode))
            Dim CityList As ArrayList
            CityList = m_JobPositionMapper.RetrieveByCriteria(criterias)
            If Not CityList Is Nothing Then
                If CityList.Count > 0 Then
                    objReturnValue = CType(CityList.Item(0), JobPosition)
                End If
            End If
            Return objReturnValue
        End Function

        Public Function RetrieveByDesc(ByVal JobPositionDesc As String) As JobPosition
            Dim objReturnValue As JobPosition
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(JobPosition), "Description", MatchType.Exact, JobPositionDesc))
            Dim arrList As ArrayList
            arrList = m_JobPositionMapper.RetrieveByCriteria(criterias)
            If Not arrList Is Nothing Then
                If arrList.Count > 0 Then
                    objReturnValue = CType(arrList.Item(0), JobPosition)
                End If
            End If
            Return objReturnValue
        End Function

        Public Function RetrieveIDByCode(ByVal JobPositionCode As String) As Integer
            Dim objReturnValue As JobPosition
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(JobPosition), "Code", MatchType.Exact, JobPositionCode))
            Dim CityList As ArrayList
            CityList = m_JobPositionMapper.RetrieveByCriteria(criterias)
            If Not CityList Is Nothing Then
                If CityList.Count > 0 Then
                    objReturnValue = CType(CityList.Item(0), JobPosition)
                End If
            End If
            If Not IsNothing(objReturnValue) Then
                Return objReturnValue.ID
            Else
                Return 0
            End If
        End Function

        Public Function IsJobPositionFound(ByVal JobPositionCode As String) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim bResult As Boolean = False
            criterias.opAnd(New Criteria(GetType(JobPosition), "Code", MatchType.Exact, JobPositionCode))
            Dim JobPositionColl As ArrayList = m_JobPositionMapper.RetrieveByCriteria(criterias)
            If (JobPositionColl.Count > 0) Then
                bResult = True
            Else
            End If
            Return bResult
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_JobPositionMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_JobPositionMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Return m_JobPositionMapper.RetrieveByCriteria(criterias)
            'Return m_JobPositionMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(JobPosition), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_JobPositionMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal crit As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(JobPosition), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_JobPositionMapper.RetrieveByCriteria(crit, sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(JobPosition), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_JobPositionMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _City As ArrayList = m_JobPositionMapper.RetrieveByCriteria(criterias)
            Return _City
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim JobPositionColl As ArrayList = m_JobPositionMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return JobPositionColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(JobPosition), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim JobPositionColl As ArrayList = m_JobPositionMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return JobPositionColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
          ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(JobPosition), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_JobPositionMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
               ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(JobPosition), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_JobPositionMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
        Public Function RetriveListByMenuAssigned(ByVal iMenu As Integer) As ArrayList
            Dim critJptm As New CriteriaComposite(New Criteria(GetType(JobPositionToMenu), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If Not IsNothing(iMenu) Then
                critJptm.opAnd(New Criteria(GetType(JobPositionToMenu), "JobPositionMenu.ID", MatchType.Exact, iMenu))
            End If
            Dim distinctIDs As New Hashtable
            Dim arrJptmList As ArrayList = m_JobPositionToMenuMapper.RetrieveByCriteria(critJptm)
            Dim strIDJptm As String = ""
            For i As Integer = 0 To arrJptmList.Count - 1
                If strIDJptm.Length > 0 Then
                    strIDJptm += ","
                End If
                Dim jptm As JobPositionToMenu = CType(arrJptmList(i), JobPositionToMenu)
                If distinctIDs(jptm.ID) Is Nothing Then
                    strIDJptm += jptm.JobPosition.ID.ToString()
                    distinctIDs.Add(jptm.ID, jptm.JobPosition.ID)
                End If
            Next
            Dim criterias As New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If strIDJptm.Length > 0 Then
                strIDJptm = "(" + strIDJptm + ")"
                criterias.opAnd(New Criteria(GetType(JobPosition), "ID", MatchType.InSet, strIDJptm))
                Return m_JobPositionMapper.RetrieveByCriteria(criterias)
            Else
                Return New ArrayList
            End If
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim JobPositionColl As ArrayList = m_JobPositionMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return JobPositionColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, _
            ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(JobPosition), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim JobPositionColl As ArrayList = m_JobPositionMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return JobPositionColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(JobPosition), columnName, matchOperator, columnValue))
            Dim JobPositionColl As ArrayList = m_JobPositionMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return JobPositionColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(JobPosition), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPosition), columnName, matchOperator, columnValue))

            Return m_JobPositionMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As JobPosition) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_JobPositionMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Insert(ByVal objJobPosition As JobPosition, ByVal arrJobPositionMenu As ArrayList) As Integer
            Dim iReturn As Integer = -2
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True

                    m_TransactionManager.AddInsert(objJobPosition, m_userPrincipal.Identity.Name)
                    If arrJobPositionMenu.Count > 0 Then
                        For Each objJobPositionMenu As JobPositionMenu In arrJobPositionMenu
                            Dim objJobPositionToMenu As New JobPositionToMenu
                            objJobPositionToMenu.JobPosition = objJobPosition
                            objJobPositionToMenu.JobPositionMenu = objJobPositionMenu
                            objJobPositionToMenu.RowStatus = CType(DBRowStatus.Active, Short)

                            m_TransactionManager.AddInsert(objJobPositionToMenu, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If performTransaction Then
                        Me.m_TransactionManager.PerformTransaction()
                        iReturn = ID_Insert
                    End If

                Catch ex As Exception
                    Dim s As String = ex.Message
                    iReturn = -1

                Finally
                    Me.RemoveTaskLocking()
                End Try

            End If
            Return iReturn
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.JobPosition) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.JobPosition).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.JobPosition).MarkLoaded()
                ID_Insert = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is JobPositionToMenu) Then
                CType(InsertArg.DomainObject, JobPositionToMenu).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.JobPositionToMenu).MarkLoaded()
            End If
        End Sub

        Public Function Update(ByVal objDomain As JobPosition) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_JobPositionMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Update(ByVal objJobPosition As JobPosition, ByVal arrJobPositionMenu As ArrayList) As Integer
            Dim iReturn As Integer = -2
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True

                    Dim arrListJPtoMenu As ArrayList
                    Dim criteria As CriteriaComposite = New CriteriaComposite(New criteria(GetType(JobPositionToMenu), "JobPosition.ID", MatchType.Exact, objJobPosition.ID))
                    arrListJPtoMenu = m_JobPositionToMenuMapper.RetrieveByCriteria(criteria)
                    For Each objTPToMenu As JobPositionToMenu In arrListJPtoMenu
                        objTPToMenu.RowStatus = -1
                        m_JobPositionToMenuMapper.Update(objTPToMenu, m_userPrincipal.Identity.Name)
                    Next
                    If arrJobPositionMenu.Count > 0 Then
                        For Each objJobPositionMenu As JobPositionMenu In arrJobPositionMenu
                            Dim criteriaJPtoMenu As CriteriaComposite = New CriteriaComposite(New criteria(GetType(JobPositionToMenu), "JobPosition.ID", MatchType.Exact, objJobPosition.ID))
                            criteriaJPtoMenu.opAnd(New criteria(GetType(JobPositionToMenu), "JobPositionMenu.ID", MatchType.Exact, objJobPositionMenu.ID))
                            Dim objJobPositionToMenu As New JobPositionToMenu
                            arrListJPtoMenu = m_JobPositionToMenuMapper.RetrieveByCriteria(criteriaJPtoMenu)
                            If arrListJPtoMenu.Count < 1 Then
                                'Insert new
                                objJobPositionToMenu.JobPosition = objJobPosition
                                objJobPositionToMenu.JobPositionMenu = objJobPositionMenu
                                objJobPositionToMenu.RowStatus = CType(DBRowStatus.Active, Short)

                                m_TransactionManager.AddInsert(objJobPositionToMenu, m_userPrincipal.Identity.Name)
                            Else
                                'update
                                For Each objTPToMenu As JobPositionToMenu In arrListJPtoMenu
                                    If objTPToMenu.JobPositionMenu.ID = objJobPositionMenu.ID And objTPToMenu.JobPosition.ID = objJobPosition.ID Then
                                        objTPToMenu.RowStatus = 0
                                        m_TransactionManager.AddUpdate(objTPToMenu, m_userPrincipal.Identity.Name)
                                    End If
                                Next
                            End If

                        Next
                    End If

                    m_TransactionManager.AddUpdate(objJobPosition, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        Me.m_TransactionManager.PerformTransaction()
                        iReturn = ID_Insert
                    End If

                Catch ex As Exception
                    Dim s As String = ex.Message
                    iReturn = -1

                Finally
                    Me.RemoveTaskLocking()
                End Try

            End If
            Return iReturn
        End Function
        

        Public Sub Delete(ByVal objDomain As JobPosition)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_JobPositionMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As JobPosition) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_JobPositionMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPosition), "CityCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(JobPosition), "CityCode", AggregateType.Count)

            Return CType(m_JobPositionMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateDesc(ByVal Code As String, ByVal idedit As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPosition), "Description", MatchType.Exact, Code))

            If idedit <> 0 Then
                crit.opAnd(New Criteria(GetType(JobPosition), "ID", MatchType.No, idedit))
            End If
            crit.opAnd(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, 0))

            Dim agg As Aggregate = New Aggregate(GetType(JobPosition), "Description", AggregateType.Count)

            Return CType(m_JobPositionMapper.RetrieveScalar(agg, crit), Integer)
        End Function


#End Region

#Region "Custom Method"
        Public Function GetJobPositionBySalesmanHeader(SalesmanHeaderID As Integer) As JobPosition
            Dim strQuery As String = "EXEC sp_GetJobPartEmployee '" & SalesmanHeaderID & "'"
            Dim arr As New ArrayList
            arr = m_JobPositionMapper.RetrieveSP(strQuery)

            If arr.Count > 0 Then
                Return CType(arr(0), JobPosition)
            Else
                Return Nothing
            End If

        End Function
#End Region

    End Class

End Namespace

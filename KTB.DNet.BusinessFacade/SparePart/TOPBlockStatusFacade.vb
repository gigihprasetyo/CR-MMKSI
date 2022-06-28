
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
'// Generated on 9/27/2016 - 11:43:51 AM
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
Imports System.Collections.Generic


#End Region

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class TOPBlockStatusFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TOPBlockStatusMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_TOPBlockStatusMapper = MapperFactory.GetInstance.GetMapper(GetType(TOPBlockStatus).ToString)
            Me.m_TransactionManager = New TransactionManager
            'AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            AddHandler m_TransactionManager.Insert, AddressOf Me.m_TransactionManager_Insert

            Me.DomainTypeCollection.Add(GetType(TOPBlockStatus))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TOPBlockStatus
            Return CType(m_TOPBlockStatusMapper.Retrieve(ID), TOPBlockStatus)
        End Function

        Public Function Retrieve(ByVal DONumber As String) As TOPBlockStatus
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPBlockStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TOPBlockStatus), "DONumber", MatchType.Exact, DONumber))
            Dim arlDO As ArrayList = m_TOPBlockStatusMapper.RetrieveByCriteria(criterias)
            If arlDO.Count > 0 Then
                Return CType(arlDO(0), TOPBlockStatus)
            End If
            Return Nothing
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TOPBlockStatusMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TOPBlockStatusMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TOPBlockStatusMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TOPBlockStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TOPBlockStatusMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TOPBlockStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TOPBlockStatusMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPBlockStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TOPBlockStatus As ArrayList = m_TOPBlockStatusMapper.RetrieveByCriteria(criterias)
            Return _TOPBlockStatus
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPBlockStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TOPBlockStatusColl As ArrayList = m_TOPBlockStatusMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TOPBlockStatusColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TOPBlockStatusColl As ArrayList = m_TOPBlockStatusMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TOPBlockStatusColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPBlockStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TOPBlockStatusColl As ArrayList = m_TOPBlockStatusMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TOPBlockStatus), columnName, matchOperator, columnValue))
            Return TOPBlockStatusColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TOPBlockStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPBlockStatus), columnName, matchOperator, columnValue))

            Return m_TOPBlockStatusMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.TOPBlockStatus) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.TOPBlockStatus).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.TOPBlockStatus).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.SparePartPOStatus) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.SparePartPOStatus).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.SparePartPOStatus).MarkLoaded()
            End If
        End Sub

        Public Function InsertWithTransactionManager(ByVal topBlockStatus As TOPBlockStatus, ByVal sparePartPOStatus As SparePartPOStatus) As Integer
            Dim result As Integer = -1
            If Me.IsTaskFree Then
                Try
                    Me.SetTaskLocking()

                    ' add command to insert Top Block Status

                    ' add command to insert SparePart PO Status
                    'If sparePartPOStatus.ID <> 0 Then
                    '    Me.m_TransactionManager.AddUpdate(sparePartPOStatus, m_userPrincipal.Identity.Name)
                    'Else
                    '    'Me.m_TransactionManager.AddInsert(sparePartPOStatus, m_userPrincipal.Identity.Name)
                    'End If

                    sparePartPOStatus.MarkLoaded()
                    Me.m_TransactionManager.AddInsert(topBlockStatus, m_userPrincipal.Identity.Name)
                    Me.m_TransactionManager.PerformTransaction()
                    result = topBlockStatus.ID
                    TOPBlockWS(result)
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

        Public Function UpdateWithTransactionManager(ByVal topBlockStatus As TOPBlockStatus, ByVal sparePartPOStatus As SparePartPOStatus) As Integer
            ' mark as loaded to prevent it loads from db
            topBlockStatus.MarkLoaded()
            sparePartPOStatus.MarkLoaded()
            ' set default result
            Dim result As Integer = -1
            If Me.IsTaskFree Then
                Try
                    Me.SetTaskLocking()

                    ' add command to insert vehicle color

                    If (sparePartPOStatus.ID <> 0) Then
                        m_TransactionManager.AddUpdate(sparePartPOStatus, m_userPrincipal.Identity.Name)
                    Else
                        'm_TransactionManager.AddInsert(sparePartPOStatus, m_userPrincipal.Identity.Name)
                    End If

                    sparePartPOStatus.MarkLoaded()

                    ' add command to update spk
                    m_TransactionManager.AddUpdate(topBlockStatus, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    result = topBlockStatus.ID
                    TOPBlockWS(result)
                Catch ex As Exception
                    Throw ex
                Finally
                    Me.RemoveTaskLocking()
                End Try

            End If

            Return result
        End Function
        Public Function Insert(ByVal objDomain As TOPBlockStatus) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TOPBlockStatusMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TOPBlockStatus) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TOPBlockStatusMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TOPBlockStatus)
            Dim nResult As Integer = -1

        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As TOPBlockStatus)
            Try
                m_TOPBlockStatusMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

        Private Sub TOPBlockWS(ByVal TOPBlockID As Integer)

            Try
                Dim strSPName As String = "sp_InsertTOPBlockStatus_WS"
                Dim Param As New List(Of SqlClient.SqlParameter)

                Param.Add(New SqlClient.SqlParameter("@ID", TOPBlockID))
                Param.Add(New SqlClient.SqlParameter("@CreatedBy", m_userPrincipal.Identity.Name))

                m_TOPBlockStatusMapper.ExecuteSP(strSPName, New ArrayList(Param))
            Catch ex As Exception

            End Try
        End Sub

#End Region

#Region "Customs"


#End Region

    End Class

End Namespace


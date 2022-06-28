
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
'// Generated on 3/14/2016 - 11:41:26 AM
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
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade.Service

    Public Class DepositBPencairanHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DepositBPencairanHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DepositBPencairanHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(DepositBPencairanHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.DepositBPencairanHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.DepositBPencairanDetail))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DepositBPencairanHeader
            Return CType(m_DepositBPencairanHeaderMapper.Retrieve(ID), DepositBPencairanHeader)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DepositBPencairanHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DepositBPencairanHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DepositBPencairanHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBPencairanHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositBPencairanHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBPencairanHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositBPencairanHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBPencairanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DepositBPencairanHeader As ArrayList = m_DepositBPencairanHeaderMapper.RetrieveByCriteria(criterias)
            Return _DepositBPencairanHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBPencairanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositBPencairanHeaderColl As ArrayList = m_DepositBPencairanHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DepositBPencairanHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DepositBPencairanHeaderColl As ArrayList = m_DepositBPencairanHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DepositBPencairanHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBPencairanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositBPencairanHeaderColl As ArrayList = m_DepositBPencairanHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DepositBPencairanHeader), columnName, matchOperator, columnValue))
            Return DepositBPencairanHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBPencairanHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBPencairanHeader), columnName, matchOperator, columnValue))

            Return m_DepositBPencairanHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveScalar(ByVal criterias As ICriteria, ByVal agg As Aggregate) As Object
            Return m_DepositBPencairanHeaderMapper.RetrieveScalar(agg, criterias)
        End Function
#End Region

#Region "Transaction/Other Public Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.DepositBPencairanHeader) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.DepositBPencairanHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.DepositBPencairanHeader).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is DepositBPencairanDetail) Then
                CType(InsertArg.DomainObject, DepositBPencairanDetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function Insert(ByVal objDomain As DepositBPencairanHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DepositBPencairanHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function


        Public Function InsertTransaction(ByVal objDomain As DepositBPencairanHeader) As Integer
            Dim returnValue As Integer = -2
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim AccountOfType As String
                    Dim Type As String
                    Dim oldDep As DepositBPencairanHeader

                    'If objDomain.DNNumber.Length > 0 And objDomain.AssignmentNumber.Length = 0 Then
                    '    AccountOfType = objDomain.DNNumber
                    '    Type = "DNNumber"
                    '    oldDep = GetExistingDepositAPencairanH(objDomain.Dealer, AccountOfType, Type)
                    'ElseIf objDomain.DNNumber.Length = 0 And objDomain.AssignmentNumber.Length > 0 Then
                    '    AccountOfType = objDomain.AssignmentNumber
                    '    Type = "AssignmentNumber"
                    '    oldDep = GetExistingDepositAPencairanH(objDomain.Dealer, AccountOfType, Type)
                    'Else
                    '    oldDep = Nothing
                    'End If

                    If oldDep Is Nothing Then
                        m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                        For Each item As DepositBPencairanDetail In objDomain.DepositBPencairanDetails
                            item.DepositBPencairanHeader = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                    Else
                        performTransaction = False
                    End If
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

        Public Function Update(ByVal objDomain As DepositBPencairanHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DepositBPencairanHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DepositBPencairanHeader)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DepositBPencairanHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DepositBPencairanHeader)
            Try
                m_DepositBPencairanHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function Retrieve(ByVal Code As String) As DepositBPencairanHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBPencairanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositBPencairanHeader), "NoReg", MatchType.Exact, Code))

            Dim DepositBPencairanHeaderColl As ArrayList = m_DepositBPencairanHeaderMapper.RetrieveByCriteria(criterias)
            If (DepositBPencairanHeaderColl.Count > 0) Then
                Return CType(DepositBPencairanHeaderColl(0), DepositBPencairanHeader)
            End If
            Return New DepositBPencairanHeader
        End Function

        Public Function RetrieveIPHeader(ByVal IndenPartID As Integer) As DepositBPencairanHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBPencairanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositBPencairanHeader), "IndentPartHeader.ID", MatchType.Exact, IndenPartID))

            Dim DepositBPencairanHeaderColl As ArrayList = m_DepositBPencairanHeaderMapper.RetrieveByCriteria(criterias)
            If (DepositBPencairanHeaderColl.Count > 0) Then
                Return CType(DepositBPencairanHeaderColl(0), DepositBPencairanHeader)
            End If
            Return New DepositBPencairanHeader
        End Function
#End Region

    End Class

End Namespace



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


#End Region

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class TOPCreditAccountFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TOPCreditAccountMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_TOPCreditAccountMapper = MapperFactory.GetInstance.GetMapper(GetType(TOPCreditAccount).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.m_TransactionManager = New TransactionManager
            Me.DomainTypeCollection.Add(GetType(TOPCreditAccount))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TOPCreditAccount
            Return CType(m_TOPCreditAccountMapper.Retrieve(ID), TOPCreditAccount)
        End Function

        Public Function Retrieve(ByVal DONumber As String) As TOPCreditAccount
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPCreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TOPCreditAccount), "DONumber", MatchType.Exact, DONumber))
            Dim arlDO As ArrayList = m_TOPCreditAccountMapper.RetrieveByCriteria(criterias)
            If arlDO.Count > 0 Then
                Return CType(arlDO(0), TOPCreditAccount)
            End If
            Return Nothing
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TOPCreditAccountMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TOPCreditAccountMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TOPCreditAccountMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TOPCreditAccount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TOPCreditAccountMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TOPCreditAccount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TOPCreditAccountMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPCreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TOPCreditAccount As ArrayList = m_TOPCreditAccountMapper.RetrieveByCriteria(criterias)
            Return _TOPCreditAccount
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPCreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TOPCreditAccountColl As ArrayList = m_TOPCreditAccountMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TOPCreditAccountColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TOPCreditAccountColl As ArrayList = m_TOPCreditAccountMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TOPCreditAccountColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPCreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TOPCreditAccountColl As ArrayList = m_TOPCreditAccountMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TOPCreditAccount), columnName, matchOperator, columnValue))
            Return TOPCreditAccountColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TOPCreditAccount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPCreditAccount), columnName, matchOperator, columnValue))

            Return m_TOPCreditAccountMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.TOPCreditAccount) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.TOPCreditAccount).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.TOPCreditAccount).MarkLoaded()
            End If
        End Sub

        Public Function Insert(ByVal objDomain As TOPCreditAccount) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TOPCreditAccountMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TOPCreditAccount) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TOPCreditAccountMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TOPCreditAccount)
            Dim nResult As Integer = -1

        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As TOPCreditAccount)
            Try
                m_TOPCreditAccountMapper.Delete(objDomain)
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

#Region "Customs"


#End Region

    End Class

End Namespace


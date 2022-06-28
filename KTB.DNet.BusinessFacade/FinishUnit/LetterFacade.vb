 


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
'// Author Name   : Agus Soepriadi
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 10/10/2005 - 10:53:00 AM
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

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class LetterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_LetterMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_LetterMapper = MapperFactory.GetInstance().GetMapper(GetType(Letter).ToString)

            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.Letter))
            Me.m_TransactionManager = New TransactionManager
            AddHandler Me.m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As Letter
            Return CType(m_LetterMapper.Retrieve(ID), Letter)
        End Function

        Public Function Retrieve(ByVal sCode As DateTime) As Letter
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Letter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(Letter), "NomorSurat", MatchType.Exact, sCode))

            Dim LetterColl As ArrayList = m_LetterMapper.RetrieveByCriteria(crit)
            If (LetterColl.Count > 0) Then
                Return CType(LetterColl(0), Letter)
            End If
            Return New Letter
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_LetterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_LetterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_LetterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Letter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LetterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Letter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LetterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Letter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _Letter As ArrayList = m_LetterMapper.RetrieveByCriteria(criterias)
            Return _Letter
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Letter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim LetterColl As ArrayList = m_LetterMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return LetterColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Letter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim LetterColl As ArrayList = m_LetterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return LetterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim LetterColl As ArrayList = m_LetterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return LetterColl
        End Function
        Public Function RetrieveByCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(Letter), SortColumn, sortDirection))

            Dim LetterColl As ArrayList = m_LetterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return LetterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Letter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Letter), columnName, matchOperator, columnValue))
            Dim LetterColl As ArrayList = m_LetterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return LetterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Letter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Letter), columnName, matchOperator, columnValue))

            Return m_LetterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As Letter) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_LetterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As Letter) As Integer
            Dim nResult As Integer = 1
            Try
                nResult = m_LetterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function UpdateTransaction(ByVal objDomain As Letter, ByVal objHistory As LetterHistory) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    objHistory.Letter = objDomain
                    m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
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

        Private Function GetLetterNumber(ByVal objDomain As Letter) As String
            Dim str As String = objDomain.Department.Code.Substring(0, 3) & Now.Year.ToString.Substring(2, 2) & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
            Return str
        End Function

        Public Function InsertTransaction(ByVal objDomains As ArrayList) As String
            Dim returnValue As String = String.Empty
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim number As String = GetLetterNumber(objDomains(0))
                    For Each item As Letter In objDomains
                        item.NomorSurat = number
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = number
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

        Public Sub Delete(ByVal objDomain As Letter)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_LetterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As Letter)
            Try
                m_LetterMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub


        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.Letter) Then

                CType(InsertArg.DomainObject, KTB.DNET.Domain.Letter).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.Letter).MarkLoaded()
            Else
                If (TypeOf InsertArg.DomainObject Is LetterHistory) Then

                    CType(InsertArg.DomainObject, LetterHistory).ID = InsertArg.ID
                    CType(InsertArg.DomainObject, LetterHistory).MarkLoaded()
                End If
            End If
        End Sub
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace



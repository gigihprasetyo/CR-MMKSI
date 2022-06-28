 
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
'// Generated on 10/7/2005 - 1:28:25 PM
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

Namespace KTB.DNet.BusinessFacade.SPAF

    Public Class SPAFFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SPAFDocMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SPAFDocMapper = MapperFactory.GetInstance.GetMapper(GetType(SPAFDoc).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.SPAFDoc))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.SPAFDocHistory))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SPAFDoc
            Return CType(m_SPAFDocMapper.Retrieve(ID), SPAFDoc)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SPAFDocMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SPAFDocMapper.RetrieveByCriteria(criterias, sorts)
        End Function

      

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(SPAFDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_SPAFDocMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SPAFDocMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal ContractHeaderID As Integer) As ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SPAFDoc), "ContractHeaderID", _
                MatchType.Exact, ContractHeaderID))
            criterias.opAnd(New Criteria(GetType(SPAFDoc), "RowStatus", MatchType.Exact, _
                CType(DBRowStatus.Active, Short)))
            Return m_SPAFDocMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(SPAFDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPAFDocMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(SPAFDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPAFDocMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPAFDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SPAFDoc As ArrayList = m_SPAFDocMapper.RetrieveByCriteria(criterias)
            Return _SPAFDoc
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPAFDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPAFDocColl As ArrayList = m_SPAFDocMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SPAFDocColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPAFDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SPAFDocColl As ArrayList = m_SPAFDocMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SPAFDocColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SPAFDocColl As ArrayList = m_SPAFDocMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SPAFDocColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPAFDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPAFDocColl As ArrayList = m_SPAFDocMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SPAFDoc), columnName, matchOperator, columnValue))
            Return SPAFDocColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.domain.Search.SortCollection = New KTB.DNet.domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.domain.Search.Sort(GetType(SPAFDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPAFDoc), columnName, matchOperator, columnValue))

            Return m_SPAFDocMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.SPAFDoc) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.SPAFDoc).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.SPAFDoc).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is SPAFDocHistory) Then
                CType(InsertArg.DomainObject, SPAFDocHistory).ID = InsertArg.ID
            End If
        End Sub

        Public Sub Delete(ByVal objDomain As SPAFDoc)
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SPAFDocMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ProsesStatus(ByVal list As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    For Each item As SPAFDoc In list
                        Dim objHistory As SPAFDocHistory = New SPAFDocHistory
                        objHistory.SPAFDoc = item
                        objHistory.Status = item.Status
                        objHistory.ProcessBy = m_userPrincipal.Identity.Name
                        objHistory.ProcessDate = Now
                        m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 1
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

        Public Function InsertTransactionSPAFAndSubsidiFromTextFile(ByVal list As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim arl As ArrayList
                    For Each item As SPAFDoc In list
                        Dim objHistory As SPAFDocHistory = New SPAFDocHistory
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "ChassisMaster.ID", MatchType.Exact, item.ChassisMaster.ID))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "DateLetter", MatchType.Exact, item.DateLetter))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "DocType", MatchType.Exact, item.DocType))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "Dealer.ID", MatchType.Exact, item.Dealer.ID))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "ReffLetter", MatchType.Exact, item.ReffLetter))

                        arl = Me.Retrieve(criterias)
                        If (arl.Count > 0) Then
                            Dim objSPAF As SPAFDoc = CType(arl(0), SPAFDoc)
                            item.ID = objSPAF.ID
                            objHistory.SPAFDoc = objSPAF
                            objHistory.Status = item.Status
                            objHistory.ProcessBy = m_userPrincipal.Identity.Name
                            objHistory.ProcessDate = Now
                            item.PostingDate = Now
                            m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            objHistory.SPAFDoc = item
                            objHistory.Status = item.Status
                            objHistory.ProcessBy = m_userPrincipal.Identity.Name
                            objHistory.ProcessDate = Now
                            item.PostingDate = Now
                            m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)
                        End If
                    Next
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 1
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

        Public Function Update(ByVal objDomain As KTB.DNet.Domain.SPAFDoc) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SPAFDocMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

#End Region

#Region "Custom Method"



#End Region

    End Class

End Namespace
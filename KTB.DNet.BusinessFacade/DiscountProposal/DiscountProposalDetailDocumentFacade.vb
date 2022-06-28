
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
'// Copyright  2020
'// ---------------------
'// $History      : $
'// Generated on 19/06/2020 - 14:52:43
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

Namespace KTB.DNET.BusinessFacade

    Public Class DiscountProposalDetailDocumentFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DiscountProposalDetailDocumentMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DiscountProposalDetailDocumentMapper = MapperFactory.GetInstance.GetMapper(GetType(DiscountProposalDetailDocument).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(DiscountProposalDetailDocument))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DiscountProposalDetailDocument
            Return CType(m_DiscountProposalDetailDocumentMapper.Retrieve(ID), DiscountProposalDetailDocument)
        End Function

        Public Function Retrieve(ByVal Code As String) As DiscountProposalDetailDocument
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DiscountProposalDetailDocument), "DiscountProposalDetailDocumentCode", MatchType.Exact, Code))

            Dim DiscountProposalDetailDocumentColl As ArrayList = m_DiscountProposalDetailDocumentMapper.RetrieveByCriteria(criterias)
            If (DiscountProposalDetailDocumentColl.Count > 0) Then
                Return CType(DiscountProposalDetailDocumentColl(0), DiscountProposalDetailDocument)
            End If
            Return New DiscountProposalDetailDocument
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DiscountProposalDetailDocumentMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DiscountProposalDetailDocumentMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DiscountProposalDetailDocumentMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DiscountProposalDetailDocument), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DiscountProposalDetailDocumentMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DiscountProposalDetailDocument), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DiscountProposalDetailDocumentMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DiscountProposalDetailDocument As ArrayList = m_DiscountProposalDetailDocumentMapper.RetrieveByCriteria(criterias)
            Return _DiscountProposalDetailDocument
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DiscountProposalDetailDocumentColl As ArrayList = m_DiscountProposalDetailDocumentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DiscountProposalDetailDocumentColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(DiscountProposalDetailDocument), SortColumn, sortDirection))
            Dim DiscountProposalDetailDocumentColl As ArrayList = m_DiscountProposalDetailDocumentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DiscountProposalDetailDocumentColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DiscountProposalDetailDocumentColl As ArrayList = m_DiscountProposalDetailDocumentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DiscountProposalDetailDocumentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DiscountProposalDetailDocumentColl As ArrayList = m_DiscountProposalDetailDocumentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DiscountProposalDetailDocument), columnName, matchOperator, columnValue))
            Return DiscountProposalDetailDocumentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DiscountProposalDetailDocument), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailDocument), columnName, matchOperator, columnValue))

            Return m_DiscountProposalDetailDocumentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailDocument), "DiscountProposalDetailDocumentCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(DiscountProposalDetailDocument), "DiscountProposalDetailDocumentCode", AggregateType.Count)
            Return CType(m_DiscountProposalDetailDocumentMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As DiscountProposalDetailDocument) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DiscountProposalDetailDocumentMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DiscountProposalDetailDocument) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DiscountProposalDetailDocumentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DiscountProposalDetailDocument)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DiscountProposalDetailDocumentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DiscountProposalDetailDocument)
            Try
                m_DiscountProposalDetailDocumentMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is DiscountProposalHeader) Then
                CType(InsertArg.DomainObject, DiscountProposalHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, DiscountProposalHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is DiscountProposalDetailDocument) Then
                CType(InsertArg.DomainObject, DiscountProposalDetailDocument).ID = InsertArg.ID
            End If
        End Sub

        Public Function InsertTransaction(ByVal objDiscountProposalHeader As DiscountProposalHeader, ByVal arrDiscountProposalDetailDoc As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If Not IsNothing(arrDiscountProposalDetailDoc) Then
                        If arrDiscountProposalDetailDoc.Count > 0 Then
                            For Each oDiscountProposalDetailDoc As DiscountProposalDetailDocument In arrDiscountProposalDetailDoc
                                oDiscountProposalDetailDoc.DiscountProposalHeader = objDiscountProposalHeader
                                m_TransactionManager.AddInsert(oDiscountProposalDetailDoc, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = objDiscountProposalHeader.ID

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

        Function UpdateTransaction(objDiscountProposalHeader As DiscountProposalHeader, arlDiscountProposalDetailDoc As ArrayList, arlDelDiscountProposalDetailDoc As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not IsNothing(arlDelDiscountProposalDetailDoc) Then
                        If arlDelDiscountProposalDetailDoc.Count > 0 Then
                            For Each item As DiscountProposalDetailDocument In arlDelDiscountProposalDetailDoc
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDiscountProposalDetailDoc) Then
                        If arlDiscountProposalDetailDoc.Count > 0 Then
                            For Each item As DiscountProposalDetailDocument In arlDiscountProposalDetailDoc
                                item.DiscountProposalHeader = objDiscountProposalHeader
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDiscountProposalHeader.ID
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

#End Region


    End Class

End Namespace


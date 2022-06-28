
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
'// Copyright  2019
'// ---------------------
'// $History      : $
'// Generated on 09/12/2019 - 14:49:33
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

Namespace KTB.DNET.BusinessFacade

    Public Class DSFLeasingClaimDocumentFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DSFLeasingClaimDocumentMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DSFLeasingClaimDocumentMapper = MapperFactory.GetInstance.GetMapper(GetType(DSFLeasingClaimDocument).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(DSFLeasingClaimDocument))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DSFLeasingClaimDocument
            Return CType(m_DSFLeasingClaimDocumentMapper.Retrieve(ID), DSFLeasingClaimDocument)
        End Function

        Public Function Retrieve(ByVal Code As String) As DSFLeasingClaimDocument
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DSFLeasingClaimDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaimDocument), "DSFLeasingClaimDocumentCode", MatchType.Exact, Code))

            Dim DSFLeasingClaimDocumentColl As ArrayList = m_DSFLeasingClaimDocumentMapper.RetrieveByCriteria(criterias)
            If (DSFLeasingClaimDocumentColl.Count > 0) Then
                Return CType(DSFLeasingClaimDocumentColl(0), DSFLeasingClaimDocument)
            End If
            Return New DSFLeasingClaimDocument
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DSFLeasingClaimDocumentMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DSFLeasingClaimDocumentMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DSFLeasingClaimDocumentMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DSFLeasingClaimDocument), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DSFLeasingClaimDocumentMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DSFLeasingClaimDocument), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DSFLeasingClaimDocumentMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DSFLeasingClaimDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DSFLeasingClaimDocument As ArrayList = m_DSFLeasingClaimDocumentMapper.RetrieveByCriteria(criterias)
            Return _DSFLeasingClaimDocument
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DSFLeasingClaimDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DSFLeasingClaimDocumentColl As ArrayList = m_DSFLeasingClaimDocumentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DSFLeasingClaimDocumentColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(DSFLeasingClaimDocument), SortColumn, sortDirection))
            Dim DSFLeasingClaimDocumentColl As ArrayList = m_DSFLeasingClaimDocumentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DSFLeasingClaimDocumentColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DSFLeasingClaimDocumentColl As ArrayList = m_DSFLeasingClaimDocumentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DSFLeasingClaimDocumentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DSFLeasingClaimDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DSFLeasingClaimDocumentColl As ArrayList = m_DSFLeasingClaimDocumentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaimDocument), columnName, matchOperator, columnValue))
            Return DSFLeasingClaimDocumentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DSFLeasingClaimDocument), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DSFLeasingClaimDocument), columnName, matchOperator, columnValue))

            Return m_DSFLeasingClaimDocumentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DSFLeasingClaimDocument), "DSFLeasingClaimDocumentCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(DSFLeasingClaimDocument), "DSFLeasingClaimDocumentCode", AggregateType.Count)
            Return CType(m_DSFLeasingClaimDocumentMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As DSFLeasingClaimDocument) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DSFLeasingClaimDocumentMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DSFLeasingClaimDocument) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DSFLeasingClaimDocumentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DSFLeasingClaimDocument)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DSFLeasingClaimDocumentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DSFLeasingClaimDocument)
            Try
                m_DSFLeasingClaimDocumentMapper.Delete(objDomain)
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
            If (TypeOf InsertArg.DomainObject Is DSFLeasingClaim) Then
                CType(InsertArg.DomainObject, DSFLeasingClaim).ID = InsertArg.ID
                CType(InsertArg.DomainObject, DSFLeasingClaim).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is DSFLeasingClaimDocument) Then
                CType(InsertArg.DomainObject, DSFLeasingClaimDocument).ID = InsertArg.ID
            End If
        End Sub

        Public Function InsertTransaction(ByVal objDSFLeasingClaim As DSFLeasingClaim, ByVal arrDocument As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If Not IsNothing(arrDocument) Then
                        If arrDocument.Count > 0 Then
                            For Each oDocument As DSFLeasingClaimDocument In arrDocument
                                oDocument.DSFLeasingClaim = objDSFLeasingClaim
                                If objDSFLeasingClaim.RemarkByDealer <> "" Then
                                    oDocument.SourceData = 0
                                End If
                                m_TransactionManager.AddInsert(oDocument, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(objDSFLeasingClaim) Then
                        m_TransactionManager.AddUpdate(objDSFLeasingClaim, m_userPrincipal.Identity.Name)
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = objDSFLeasingClaim.ID

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

        Function UpdateTransaction(objDSFLeasingClaim As DSFLeasingClaim, arrDocument As ArrayList, arlDelDocument As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not IsNothing(arlDelDocument) Then
                        If arlDelDocument.Count > 0 Then
                            For Each item As DSFLeasingClaimDocument In arlDelDocument
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrDocument) Then
                        If arrDocument.Count > 0 Then
                            For Each item As DSFLeasingClaimDocument In arrDocument
                                item.DSFLeasingClaim = objDSFLeasingClaim
                                If objDSFLeasingClaim.RemarkByDealer <> "" Then
                                    item.SourceData = 0
                                End If
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(objDSFLeasingClaim) Then
                        m_TransactionManager.AddUpdate(objDSFLeasingClaim, m_userPrincipal.Identity.Name)
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDSFLeasingClaim.ID
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


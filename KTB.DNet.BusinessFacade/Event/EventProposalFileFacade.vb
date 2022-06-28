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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 8/2/2007 - 12:59:07 PM
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

Namespace KTB.DNET.BusinessFacade.Event


    Public Class EventProposalFileFacade
        Inherits AbstractFacade


#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_EventProposalFileMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_EventProposalFileMapper = MapperFactory.GetInstance.GetMapper(GetType(EventProposalFile).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(EventProposalFile))

        End Sub

#End Region

#Region "Retrieve"

        Public Function RetriveEventProposalFile(ByVal eventProposalID As Integer, ByVal isKTB As Boolean, ByVal contentType As Integer) As EventProposalFile
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposalFile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(EventProposalFile), "EventProposal", MatchType.Exact, eventProposalID))
            crits.opAnd(New Criteria(GetType(EventProposalFile), "ContentType", MatchType.Exact, contentType))
            If (isKTB) Then
                crits.opAnd(New Criteria(GetType(EventProposalFile), "Status", MatchType.InSet, String.Format("({0},{1},{2})", CInt(EventProposalFile.EnumStatus.Baru_KTB), CInt(EventProposalFile.EnumStatus.Validasi_Dealer), CInt(EventProposalFile.EnumStatus.Validasi_KTB))))
            Else
                crits.opAnd(New Criteria(GetType(EventProposalFile), "Status", MatchType.InSet, String.Format("({0},{1},{2})", CInt(EventProposalFile.EnumStatus.Baru_Dealer), CInt(EventProposalFile.EnumStatus.Validasi_Dealer), CInt(EventProposalFile.EnumStatus.Validasi_KTB))))
            End If

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(EventProposalFile), "ContentType", Sort.SortDirection.ASC))
            sortColl.Add(New Sort(GetType(EventProposalFile), "ID", Sort.SortDirection.ASC))
            Dim arl As ArrayList = Retrieve(crits, sortColl)
            If (Not IsNothing(arl) And arl.Count > 0) Then
                Return arl(0)
            Else : Return Nothing
            End If
        End Function

        Public Function RetriveEventProposalFiles(ByVal eventProposalID As Integer, ByVal isKTB As Boolean, ByVal contentType As Integer) As ArrayList
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposalFile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(EventProposalFile), "EventProposal", MatchType.Exact, eventProposalID))
            crits.opAnd(New Criteria(GetType(EventProposalFile), "ContentType", MatchType.Exact, contentType))
            If (isKTB) Then
                crits.opAnd(New Criteria(GetType(EventProposalFile), "Status", MatchType.InSet, String.Format("({0},{1},{2})", CInt(EventProposalFile.EnumStatus.Baru_KTB), CInt(EventProposalFile.EnumStatus.Validasi_Dealer), CInt(EventProposalFile.EnumStatus.Validasi_KTB))))
            Else
                crits.opAnd(New Criteria(GetType(EventProposalFile), "Status", MatchType.InSet, String.Format("({0},{1},{2})", CInt(EventProposalFile.EnumStatus.Baru_Dealer), CInt(EventProposalFile.EnumStatus.Validasi_Dealer), CInt(EventProposalFile.EnumStatus.Validasi_KTB))))
            End If

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(EventProposalFile), "ContentType", Sort.SortDirection.ASC))
            sortColl.Add(New Sort(GetType(EventProposalFile), "ID", Sort.SortDirection.ASC))
            Return Retrieve(crits, sortColl)
        End Function

        Public Function RetrieveByEventProposal_Dealer(ByVal eventProposalId As Integer, ByVal isRetrievePenilaian As Boolean) As ArrayList
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposalFile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(EventProposalFile), "EventProposal", MatchType.Exact, eventProposalId))
            If (Not isRetrievePenilaian) Then
                crits.opAnd(New Criteria(GetType(EventProposalFile), "ContentType", MatchType.No, CInt(EventProposalFile.EnumContentType.Penilaian_KTB)))
            End If

            crits.opAnd(New Criteria(GetType(EventProposalFile), "Status", MatchType.InSet, String.Format("({0},{1},{2})", CInt(EventProposalFile.EnumStatus.Baru_Dealer), CInt(EventProposalFile.EnumStatus.Validasi_Dealer), CInt(EventProposalFile.EnumStatus.Validasi_KTB))))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(EventProposalFile), "ContentType", Sort.SortDirection.ASC))
            sortColl.Add(New Sort(GetType(EventProposalFile), "ID", Sort.SortDirection.ASC))

            Return Retrieve(crits, sortColl)
        End Function

        Public Function RetrieveByEventProposal_KTB(ByVal eventProposalId As Integer, ByVal isRetrievePenilaianOnly As Boolean) As ArrayList
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposalFile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(EventProposalFile), "EventProposal", MatchType.Exact, eventProposalId))
            crits.opAnd(New Criteria(GetType(EventProposalFile), "Status", MatchType.InSet, String.Format("({0},{1},{2})", CInt(EventProposalFile.EnumStatus.Baru_KTB), CInt(EventProposalFile.EnumStatus.Validasi_KTB), CInt(EventProposalFile.EnumStatus.Baru_Dealer))))

            If (isRetrievePenilaianOnly) Then
                crits.opAnd(New Criteria(GetType(EventProposalFile), "ContentType", MatchType.Exact, CInt(EventProposalFile.EnumContentType.Penilaian_KTB)))
            End If

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(EventProposalFile), "ContentType", Sort.SortDirection.ASC))
            sortColl.Add(New Sort(GetType(EventProposalFile), "ID", Sort.SortDirection.ASC))

            Return Retrieve(crits, sortColl)
        End Function

        'Public Function RetrievePenilaianListByEventProposal(ByVal eventProposalId As Integer) As ArrayList
        '    Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposalFile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    crits.opAnd(New Criteria(GetType(EventProposalFile), "EventProposal", MatchType.Exact, eventProposalId))
        '    crits.opAnd(New Criteria(GetType(EventProposalFile), "ContentType", MatchType.Exact, EventProposalFile.EnumContentType.Penilaian_KTB))

        '    Dim sortColl As SortCollection = New SortCollection
        '    sortColl.Add(New Sort(GetType(EventProposalFile), "ContentType", Sort.SortDirection.DESC))

        '    Return Retrieve(crits, sortColl)
        'End Function

        'Public Function RetrievePenilaianByEventProposal(ByVal eventProposalId As Integer) As EventProposalFile
        '    Dim arl As ArrayList = RetrievePenilaianListByEventProposal(eventProposalId)
        '    If (Not IsNothing(arl) And arl.Count > 0) Then
        '        Return arl(0)
        '    Else
        '        Return Nothing
        '    End If
        'End Function

        Public Function Retrieve(ByVal ID As Integer) As EventProposalFile
            Return CType(m_EventProposalFileMapper.Retrieve(ID), EventProposalFile)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_EventProposalFileMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_EventProposalFileMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_EventProposalFileMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposalFile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventProposalFileMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposalFile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventProposalFileMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposalFile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _EventProposalFile As ArrayList = m_EventProposalFileMapper.RetrieveByCriteria(criterias)
            Return _EventProposalFile
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposalFile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EventProposalFileColl As ArrayList = m_EventProposalFileMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EventProposalFileColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposalFile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ClaimReasonColl As ArrayList = m_EventProposalFileMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ClaimReasonColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(EventProposalFile), SortColumn, sortDirection))

            Dim EventProposalFileColl As ArrayList = m_EventProposalFileMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return EventProposalFileColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposalFile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposalFile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventProposalFileMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim EventProposalFileColl As ArrayList = m_EventProposalFileMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return EventProposalFileColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposalFile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EventProposalFileColl As ArrayList = m_EventProposalFileMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(EventProposalFile), columnName, matchOperator, columnValue))
            Return EventProposalFileColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposalFile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposalFile), columnName, matchOperator, columnValue))

            Return m_EventProposalFileMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As EventProposalFile) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_EventProposalFileMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                Throw
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal arlIPH As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arlIPH.Count > 0 Then
                        For Each objIPHH As EventProposalFile In arlIPH
                            m_TransactionManager.AddUpdate(objIPHH, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = 1
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

        Public Function Update(ByVal objDomain As EventProposalFile) As Integer
            Dim nResult As Integer = -1
            Try
                Return m_EventProposalFileMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function Delete(ByVal objDomain As EventProposalFile) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                Return m_EventProposalFileMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function DeleteFromDB(ByVal objDomain As EventProposalFile) As Integer
            Dim nResult As Integer = -1
            Try
                Return m_EventProposalFileMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function DeleteEventProposalFile(ByVal objDomain As KTB.DNet.Domain.EventProposalFile, ByVal arrIPDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arrIPDetail.Count > 0 Then
                        For Each objIPDetail As EventProposalFile In arrIPDetail
                            m_TransactionManager.AddDelete(objIPDetail)
                        Next
                    End If

                    m_TransactionManager.AddDelete(objDomain)
                    m_TransactionManager.PerformTransaction()
                    returnValue = 1
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

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is EventProposalFile) Then
                CType(InsertArg.DomainObject, EventProposalFile).ID = InsertArg.ID
                CType(InsertArg.DomainObject, EventProposalFile).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is EventProposalFile) Then
                CType(InsertArg.DomainObject, EventProposalFile).ID = InsertArg.ID
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace


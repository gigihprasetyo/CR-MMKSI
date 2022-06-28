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
'// Generated on 7/16/2007 - 11:33:42 AM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Text

#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTb.DNet.DataMapper.Framework
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.BusinessFacade.BusinessForum
    Public Class ForumPMFacade
        Inherits AbstractFacade


#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ForumPMMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_ForumPMMapper = MapperFactory.GetInstance.GetMapper(GetType(ForumPM).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ForumPM
            Return CType(m_ForumPMMapper.Retrieve(ID), ForumPM)
        End Function

        Public Function Retrieve(ByVal Code As String) As KTB.DNet.Domain.ForumPM
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ForumPM), "ForumPMCode", MatchType.Exact, Code))

            Dim ForumPMColl As ArrayList = m_ForumPMMapper.RetrieveByCriteria(criterias)
            If (ForumPMColl.Count > 0) Then
                Return CType(ForumPMColl(0), ForumPM)
            End If
            Return New ForumPM
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ForumPMMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ForumPMMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ForumPMMapper.RetrieveList
        End Function


        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ForumPM), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ForumPMMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ForumPM), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ForumPMMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ForumPM As ArrayList = m_ForumPMMapper.RetrieveByCriteria(criterias)
            Return _ForumPM
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ForumPMColl As ArrayList = m_ForumPMMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ForumPMColl
        End Function
        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection

            sortColl.Add(New Search.Sort(GetType(ForumPM), SortColumn, sortDirection))

            Dim ForumPMColl As ArrayList = m_ForumPMMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return ForumPMColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ForumPMColl As ArrayList = m_ForumPMMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ForumPMColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ForumPMColl As ArrayList = m_ForumPMMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(ForumPM), columnName, matchOperator, columnValue))
            Return ForumPMColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ForumPM), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPM), columnName, matchOperator, columnValue))

            Return m_ForumPMMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String, ByVal id As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPM), "Title", MatchType.Exact, Code))

            If id <> 0 Then
                crit.opAnd(New Criteria(GetType(ForumPM), "ID", MatchType.No, id))
            End If

            Dim agg As Aggregate = New Aggregate(GetType(ForumPM), "Title", AggregateType.Count)
            Return CType(m_ForumPMMapper.RetrieveScalar(agg, crit), Integer)
        End Function



        'Public Function InsertTransaction(ByVal objForumPM As ForumPM, ByVal arrForumPMMember As ArrayList) As Integer
        '    Dim returnValue As Integer = -1
        '    If (Me.IsTaskFree()) Then
        '        Try
        '            Me.SetTaskLocking()
        '            objForumPM.Description = objForumPM.Description 'ClearBannedWord(objForumPM.Description)
        '            m_TransactionManager.AddInsert(objForumPM, m_userPrincipal.Identity.Name)

        '            If arrForumPMMember.Count > 0 Then
        '                For Each objForumPMMember As ForumPMMember In arrForumPMMember
        '                    objForumPMMember.ForumPM = objForumPM
        '                    m_TransactionManager.AddInsert(objForumPMMember, m_userPrincipal.Identity.Name)
        '                Next
        '            End If
        '            m_TransactionManager.PerformTransaction()
        '            returnValue = objForumPM.ID
        '        Catch ex As Exception
        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
        '            If rethrow Then
        '                Throw
        '            End If
        '        Finally
        '            Me.RemoveTaskLocking()
        '        End Try
        '    End If
        '    Return returnValue
        'End Function

        Public Function Insert(ByVal objDomain As ForumPM) As Integer
            Dim iReturn As Integer = -1
            Try
                If objDomain.isRead <> 0 Then objDomain.isRead = 0
                iReturn = m_ForumPMMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Sub DeleteFromDB(ByVal objDomain As ForumPM)
            Try
                m_ForumPMMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is ForumPM) Then
                CType(InsertArg.DomainObject, ForumPM).ID = InsertArg.ID
                CType(InsertArg.DomainObject, ForumPM).MarkLoaded()

                'ElseIf (TypeOf InsertArg.DomainObject Is ForumPMMember) Then
                '    CType(InsertArg.DomainObject, ForumPMMember).ID = InsertArg.ID
            End If
        End Sub

        Public Function Update(ByVal objDomain As ForumPM) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ForumPMMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Update(ByVal arrForumPMMember As ArrayList, ByVal modePM As Integer) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    For Each item As ForumPM In arrForumPMMember
                        Select Case modePM
                            Case 1  'delete inbox
                                item.isDeletedInbox = 1
                            Case 2  'delete outbox
                                item.isDeletedOutBox = 1
                        End Select
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        returnValue = item.ID
                    Next
                    m_TransactionManager.PerformTransaction()
                    returnValue = returnValue
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

#Region "Custom Method"

        'Public Function ClearBannedWord(ByVal description As String) As String

        '    Dim arlBannedWord As ArrayList = New ForumPMBannedWordFacade(m_userPrincipal).RetrieveActiveList()
        '    Dim Result As String = description
        '    For Each objBannedWord As ForumPMBannedWord In arlBannedWord
        '        Dim sb As StringBuilder = New StringBuilder
        '        Result = Result.Replace(objBannedWord.BannedWord, sb.Append(Char.Parse("*"), objBannedWord.BannedWord.Length).ToString)
        '        sb = New StringBuilder
        '        Result = Result.Replace(objBannedWord.BannedWord.ToUpper, sb.Append(Char.Parse("*"), objBannedWord.BannedWord.Length).ToString)
        '        sb = New StringBuilder
        '        Result = Result.Replace(Left(objBannedWord.BannedWord, 1).ToUpper & Right(objBannedWord.BannedWord, objBannedWord.BannedWord.Length - 1), sb.Append(Char.Parse("*"), objBannedWord.BannedWord.Length).ToString)
        '    Next

        '    Return Result

        'End Function


#End Region

    End Class

End Namespace

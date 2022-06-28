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
    Public Class ForumFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ForumMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_ForumMapper = MapperFactory.GetInstance.GetMapper(GetType(Forum).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As Forum
            Return CType(m_ForumMapper.Retrieve(ID), Forum)
        End Function

        Public Function Retrieve(ByVal Code As String) As KTB.DNet.Domain.Forum
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Forum), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Forum), "ForumCode", MatchType.Exact, Code))

            Dim ForumColl As ArrayList = m_ForumMapper.RetrieveByCriteria(criterias)
            If (ForumColl.Count > 0) Then
                Return CType(ForumColl(0), Forum)
            End If
            Return New Forum
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ForumMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ForumMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ForumMapper.RetrieveList
        End Function


        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Forum), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ForumMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Forum), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ForumMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Forum), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _Forum As ArrayList = m_ForumMapper.RetrieveByCriteria(criterias)
            Return _Forum
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Forum), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ForumColl As ArrayList = m_ForumMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ForumColl
        End Function
        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Forum), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection

            sortColl.Add(New Search.Sort(GetType(Forum), SortColumn, sortDirection))

            Dim ClaimGoodConditionColl As ArrayList = m_ForumMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return ClaimGoodConditionColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ForumColl As ArrayList = m_ForumMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ForumColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Forum), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ForumColl As ArrayList = m_ForumMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(Forum), columnName, matchOperator, columnValue))
            Return ForumColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Forum), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Forum), columnName, matchOperator, columnValue))

            Return m_ForumMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCategoryID(ByVal CategoryID As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Forum), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Forum), "ForumCategory.ID", MatchType.Exact, CategoryID))
            Return Me.m_ForumMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveOpenForumByCategoryID(ByVal CategoryID As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Forum), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Forum), "ForumCategory.ID", MatchType.Exact, CategoryID))
            criterias.opAnd(New Criteria(GetType(Forum), "Status", MatchType.Exact, "1"))
            'criterias.opAnd(New Criteria(GetType(Forum), "Type", MatchType.Exact, "1"))
            'Return Me.m_ForumMapper.RetrieveByCriteria(criterias)
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(Forum), "Title", Sort.SortDirection.ASC))
            Return m_ForumMapper.RetrieveByCriteria(criterias, sortColl)
        End Function


#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String, ByVal id As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Forum), "Title", MatchType.Exact, Code))

            If id <> 0 Then
                crit.opAnd(New Criteria(GetType(Forum), "ID", MatchType.No, id))
            End If

            Dim agg As Aggregate = New Aggregate(GetType(Forum), "Title", AggregateType.Count)
            Return CType(m_ForumMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function InsertTransaction(ByVal objForum As Forum, ByVal objUser As UserInfo) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objForum, m_userPrincipal.Identity.Name)
                    Dim objMember As ForumMember = New ForumMember
                    objMember.UserInfo = objUser
                    objMember.Forum = objForum

                    m_TransactionManager.AddInsert(objMember, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
                    returnValue = objForum.ID
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


        Public Function InsertTransaction(ByVal objForum As Forum, ByVal arrForumMember As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    objForum.Description = objForum.Description 'ClearBannedWord(objForum.Description)
                    m_TransactionManager.AddInsert(objForum, m_userPrincipal.Identity.Name)

                    If arrForumMember.Count > 0 Then
                        For Each objForumMember As ForumMember In arrForumMember
                            objForumMember.Forum = objForum
                            m_TransactionManager.AddInsert(objForumMember, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = objForum.ID
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

        Public Function Insert(ByVal objDomain As Forum) As Integer
            Dim iReturn As Integer = -2
            Try
                m_ForumMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Sub DeleteFromDB(ByVal objDomain As Forum)
            Try
                m_ForumMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is Forum) Then
                CType(InsertArg.DomainObject, Forum).ID = InsertArg.ID
                CType(InsertArg.DomainObject, Forum).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is ForumMember) Then
                CType(InsertArg.DomainObject, ForumMember).ID = InsertArg.ID
            End If
        End Sub

        Public Function Update(ByVal objDomain As Forum) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.Description = objDomain.Description 'ClearBannedWord(objDomain.Description)
                nResult = m_ForumMapper.Update(objDomain, m_userPrincipal.Identity.Name)
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

        'Public Function ClearBannedWord(ByVal description As String) As String

        '    Dim arlBannedWord As ArrayList = New ForumBannedWordFacade(m_userPrincipal).RetrieveActiveList()
        '    Dim Result As String = description
        '    For Each objBannedWord As ForumBannedWord In arlBannedWord
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


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
'// Generated on 10/5/2005 - 3:23:28 PM
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

Namespace KTB.DNet.BusinessFacade.UserManagement

    Public Class BingoFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BingoMapper As IMapper
        Private m_TransactionManager As TransactionManager
        Private m_BingoID As Integer
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BingoMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.Bingo).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.Bingo))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BingoMatrix))


        End Sub

        Public Sub New(ByVal userPrincipal As IPrincipal, ByVal instanceName As String)

            Me.m_userPrincipal = userPrincipal
            Me.m_BingoMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.Bingo).ToString, instanceName)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.Bingo))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BingoMatrix))


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As Bingo
            Return CType(m_BingoMapper.Retrieve(ID), Bingo)
        End Function

        Public Function Retrieve(ByVal serialNumber As String) As Bingo
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Bingo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Bingo), "SerialNumber", MatchType.Exact, serialNumber))

            Dim BingoColl As ArrayList = m_BingoMapper.RetrieveByCriteria(criterias)
            If (BingoColl.Count > 0) Then
                Return CType(BingoColl(0), Bingo)
            End If
            Return New Bingo
        End Function

        Public Function RetrieveByHP(ByVal hp As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Bingo), _
                "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Bingo), "Handphone", MatchType.Exact, hp))

            Dim BingoColl As ArrayList = m_BingoMapper.RetrieveByCriteria(criterias)
            Return BingoColl
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BingoMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BingoMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BingoMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(Bingo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BingoMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(Bingo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BingoMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Bingo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _Bingo As ArrayList = m_BingoMapper.RetrieveByCriteria(criterias)
            Return _Bingo
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Bingo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BingoColl As ArrayList = m_BingoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BingoColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Bingo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim BingoColl As ArrayList = m_BingoMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BingoColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BingoColl As ArrayList = m_BingoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BingoColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Bingo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BingoColl As ArrayList = m_BingoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(Bingo), columnName, matchOperator, columnValue))
            Return BingoColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(Bingo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Bingo), columnName, matchOperator, columnValue))

            Return m_BingoMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Sub Update(ByVal objDomain As Bingo)
            Try
                m_BingoMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Bingo), "BingoCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(Bingo), "BingoCode", AggregateType.Count)
            Return CType(m_BingoMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function Delete(ByVal objDomain As KTB.DNet.Domain.Bingo) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As BingoMatrix In objDomain.BingoMatrixs
                        item.Bingo = objDomain
                        m_TransactionManager.AddDelete(item)
                    Next
                    m_TransactionManager.AddDelete(objDomain)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
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

        Public Function UpdateTransaction(ByVal objDomain As KTB.DNet.Domain.Bingo) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As BingoMatrix In objDomain.BingoMatrixs
                        item.Bingo = objDomain
                        m_TransactionManager.AddUpdate(item, "SAP")
                    Next
                    m_TransactionManager.AddUpdate(objDomain, "SAP")

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
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

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.Bingo) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    For Each item As BingoMatrix In objDomain.BingoMatrixs
                        item.Bingo = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
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

        Private Function GetListUserByHP(ByVal ActCode As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(UserProfile), "ActivationCode", MatchType.Exact, ActCode))
            Dim list As ArrayList = New UserProfileFacade(Nothing).Retrieve(criterias)
            Return list
        End Function

        Private Function GetListUserByHPNumber(ByVal ActCode As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(UserProfile), "UserInfo.HandPhone", MatchType.Exact, ActCode))
            Dim list As ArrayList = New UserProfileFacade(Nothing).Retrieve(criterias)
            Return list
        End Function

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.Bingo, ByVal objProfile As UserProfile, _
                               ByVal user As String, ByVal Status As Boolean) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim BingoID As Integer

                    m_TransactionManager.AddInsert(objDomain, user)
                    For Each item As BingoMatrix In objDomain.BingoMatrixs
                        item.Bingo = objDomain
                        m_TransactionManager.AddInsert(item, user)
                    Next
                    objProfile.Bingo = objDomain

                    If Status Then
                        objProfile.ActivationCode = objProfile.TempActivationCode
                        objProfile.TempActivationCode = String.Empty
                        objProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Active
                    End If
                    m_TransactionManager.AddUpdate(objProfile, user)

                    Dim currentId As Integer = objProfile.ID
                    Dim listOtherUser As ArrayList = GetListUserByHPNumber(objProfile.UserInfo.HandPhone) ' GetListUserByHP(objProfile.ActivationCode)
                    For Each item As UserProfile In listOtherUser
                        If item.ID <> currentId Then
                            item.Bingo = objDomain
                            m_TransactionManager.AddUpdate(item, user)
                        End If
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
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

        Private Function RetrieveBingo(ByVal serialNumber As String, ByVal principle As IPrincipal) As Bingo
            Dim _bingoFacade As BingoFacade = New BingoFacade(principle)
            Return _bingoFacade.Retrieve(serialNumber)
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.Bingo) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.Bingo).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.Bingo).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is BingoMatrix) Then
                CType(InsertArg.DomainObject, BingoMatrix).ID = InsertArg.ID
            End If
        End Sub

        Public Function RetrieveIDDealer(ByVal id As Integer) As Bingo
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Bingo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Bingo), "Dealer.ID", MatchType.Exact, id))

            Dim BingoColl As ArrayList = m_BingoMapper.RetrieveByCriteria(criterias)
            If (BingoColl.Count > 0) Then
                Return CType(BingoColl(0), Bingo)
            End If
            Return New Bingo
        End Function

        Public Function RetrieveBingoForValidation(ByVal serialNumber As String) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Bingo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Bingo), "SerialNumber", MatchType.Exact, serialNumber))
            Dim BingoColl As ArrayList = m_BingoMapper.RetrieveByCriteria(criterias)

            'For Each item As Bingo In BingoColl
            '    If item.CreatedTime.AddDays(item.ExpiredCount) > System.DateTime.Now Then
            '        Return False
            '    End If
            'Next

            If BingoColl.Count > 0 Then
                Return False
            Else
                Return True
            End If

        End Function

#End Region

    End Class

End Namespace
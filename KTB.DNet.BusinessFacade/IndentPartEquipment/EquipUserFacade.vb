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

Namespace KTB.DNET.BusinessFacade.IndentPartEquipment


    Public Class EquipUserFacade
        Inherits AbstractFacade


#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_EquipUserMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_EquipUserMapper = MapperFactory.GetInstance.GetMapper(GetType(EquipUser).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(EquipUser))

        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrieveByGroupType(ByVal grouptype As String) As ArrayList
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(EquipUser), "GroupType", MatchType.Exact, grouptype))
            Return Retrieve(crits)
        End Function

        Public Function RetrieveByGroupTypeAndTipe(ByVal grouptype As String, ByVal tipe As String) As ArrayList
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(EquipUser), "GroupType", MatchType.Exact, grouptype))
            crits.opAnd(New Criteria(GetType(EquipUser), "Tipe", MatchType.Exact, tipe))
            Return Retrieve(crits)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As EquipUser
            Return CType(m_EquipUserMapper.Retrieve(ID), EquipUser)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_EquipUserMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_EquipUserMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_EquipUserMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EquipUser), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EquipUserMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EquipUser), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EquipUserMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _EquipUser As ArrayList = m_EquipUserMapper.RetrieveByCriteria(criterias)
            Return _EquipUser
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EquipUserColl As ArrayList = m_EquipUserMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EquipUserColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EquipUser), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ClaimReasonColl As ArrayList = m_EquipUserMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ClaimReasonColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(EquipUser), SortColumn, sortDirection))

            Dim EquipUserColl As ArrayList = m_EquipUserMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return EquipUserColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EquipUser), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EquipUserMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim EquipUserColl As ArrayList = m_EquipUserMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return EquipUserColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EquipUserColl As ArrayList = m_EquipUserMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(EquipUser), columnName, matchOperator, columnValue))
            Return EquipUserColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EquipUser), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipUser), columnName, matchOperator, columnValue))

            Return m_EquipUserMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EquipUser), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_EquipUserMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As EquipUser) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_EquipUserMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
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
                        For Each objIPHH As EquipUser In arlIPH
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

        Public Function Update(ByVal objDomain As EquipUser) As Integer
            Dim nResult As Integer = -1
            Try
                Return m_EquipUserMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function Delete(ByVal objDomain As EquipUser) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                Return m_EquipUserMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function DeleteFromDB(ByVal objDomain As EquipUser) As Integer
            Dim nResult As Integer = -1
            Try
                Return m_EquipUserMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function DeleteEquipUser(ByVal objDomain As KTB.DNet.Domain.EquipUser, ByVal arrIPDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arrIPDetail.Count > 0 Then
                        For Each objIPDetail As EquipUser In arrIPDetail
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

            If (TypeOf InsertArg.DomainObject Is EquipUser) Then
                CType(InsertArg.DomainObject, EquipUser).ID = InsertArg.ID
                CType(InsertArg.DomainObject, EquipUser).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is EquipUser) Then
                CType(InsertArg.DomainObject, EquipUser).ID = InsertArg.ID
            End If
        End Sub

#End Region

#Region "Custom Method"


        Public Function ValidateValue(ByVal _Email As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(EquipUser), "Email", MatchType.Exact, _Email))

            Dim agg As Aggregate = New Aggregate(GetType(EquipUser), "id", AggregateType.Count)

            Return CType(m_EquipUserMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateValue(ByVal _UserName As String, ByVal _Email As String, ByVal _GroupType As Short) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(EquipUser), "UserName", MatchType.Exact, _UserName))
            crit.opAnd(New Criteria(GetType(EquipUser), "GroupType", MatchType.Exact, _GroupType))
            crit.opAnd(New Criteria(GetType(EquipUser), "Email", MatchType.Exact, _Email))

            Dim agg As Aggregate = New Aggregate(GetType(EquipUser), "id", AggregateType.Count)

            Return CType(m_EquipUserMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateValue(ByVal _UserName As String, ByVal _Email As String, ByVal _GroupType As Short, ByVal _Tipe As Short) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(EquipUser), "UserName", MatchType.Exact, _UserName))
            crit.opAnd(New Criteria(GetType(EquipUser), "GroupType", MatchType.Exact, _GroupType))
            crit.opAnd(New Criteria(GetType(EquipUser), "Email", MatchType.Exact, _Email))
            crit.opAnd(New Criteria(GetType(EquipUser), "Tipe", MatchType.Exact, _Tipe))

            Dim agg As Aggregate = New Aggregate(GetType(EquipUser), "id", AggregateType.Count)

            Return CType(m_EquipUserMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateValue(ByVal _UserName As String, ByVal _Email As String, ByVal _GroupType As Short, ByVal _Tipe As Short, ByVal _Posisi As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(EquipUser), "UserName", MatchType.Exact, _UserName))
            crit.opAnd(New Criteria(GetType(EquipUser), "GroupType", MatchType.Exact, _GroupType))
            crit.opAnd(New Criteria(GetType(EquipUser), "Email", MatchType.Exact, _Email))
            crit.opAnd(New Criteria(GetType(EquipUser), "Tipe", MatchType.Exact, _Tipe))
            crit.opAnd(New Criteria(GetType(EquipUser), "PositionCC", MatchType.Exact, _Posisi))

            Dim agg As Aggregate = New Aggregate(GetType(EquipUser), "id", AggregateType.Count)

            Return CType(m_EquipUserMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateValue(ByVal _Email As String, ByVal Id As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            crit.opAnd(New Criteria(GetType(EquipUser), "Email", MatchType.Exact, _Email))
            crit.opAnd(New Criteria(GetType(EquipUser), "id", MatchType.No, Id))

            Dim agg As Aggregate = New Aggregate(GetType(EquipUser), "id", AggregateType.Count)

            Return CType(m_EquipUserMapper.RetrieveScalar(agg, crit), Integer)
        End Function


        Public Function CreateEmailString(ByVal groupType As String, ByVal tipe As String) As String
            Dim arl As ArrayList = RetrieveByGroupTypeAndTipe(groupType, tipe)
            Dim szResult As String = ""
            For Each obj As EquipUser In arl
                szResult &= obj.Email & ";"
            Next
            If (szResult <> "") Then
                szResult = szResult.Substring(0, szResult.Length - 1)
            End If
            Return szResult
        End Function

        Public Function CreateEmailToString(ByVal groupType As String) As String
            Return Me.CreateEmailString(groupType, EquipUser.EquipUserTipe.TO_SENT)
        End Function

        Public Function CreateEmailCcString(ByVal groupType As String) As String
            Return Me.CreateEmailString(groupType, EquipUser.EquipUserTipe.CC_TO)
        End Function

#End Region

    End Class

End Namespace


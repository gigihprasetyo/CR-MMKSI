
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
'// Generated on 25/02/2016 - 13:27:19
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
Imports System.Collections.Generic


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class PresentationGroupFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PresentationGroupMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_PresentationGroupMapper = MapperFactory.GetInstance.GetMapper(GetType(PresentationGroup).ToString)
            Me.m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PresentationGroup
            Return CType(m_PresentationGroupMapper.Retrieve(ID), PresentationGroup)
        End Function

        Public Function Retrieve(ByVal Code As String) As PresentationGroup
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PresentationGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PresentationGroup), "PresentationGroupCode", MatchType.Exact, Code))

            Dim PresentationGroupColl As ArrayList = m_PresentationGroupMapper.RetrieveByCriteria(criterias)
            If (PresentationGroupColl.Count > 0) Then
                Return CType(PresentationGroupColl(0), PresentationGroup)
            End If
            Return New PresentationGroup
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PresentationGroupMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PresentationGroupMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PresentationGroupMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PresentationGroup), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PresentationGroupMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PresentationGroup), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PresentationGroupMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PresentationGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PresentationGroup As ArrayList = m_PresentationGroupMapper.RetrieveByCriteria(criterias)
            Return _PresentationGroup
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PresentationGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PresentationGroupColl As ArrayList = m_PresentationGroupMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PresentationGroupColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PresentationGroupColl As ArrayList = m_PresentationGroupMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PresentationGroupColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PresentationGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PresentationGroupColl As ArrayList = m_PresentationGroupMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PresentationGroup), columnName, matchOperator, columnValue))
            Return PresentationGroupColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PresentationGroup), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PresentationGroup), columnName, matchOperator, columnValue))

            Return m_PresentationGroupMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(PresentationGroup), SortColumn, sortDirection))
            Dim PresentationGroupColl As ArrayList = m_PresentationGroupMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PresentationGroupColl
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PresentationGroup), "PresentationGroupCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(PresentationGroup), "PresentationGroupCode", AggregateType.Count)
            Return CType(m_PresentationGroupMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As PresentationGroup) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_PresentationGroupMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function


        Public Function InsertUserGroup(ByVal objDomain As Presentation, ByVal objArrayUserGroup As List(Of UserGroup)) As Integer
            Dim iReturn As Integer = -1
            Try
                If (Me.IsTaskFree()) Then
                    Try
                        Me.SetTaskLocking()

                        '

                        'Retrieve List userGroup
                        For Each ObjuserGroup As UserGroup In objArrayUserGroup
                            Dim objPresentationgroup As New PresentationGroup
                            objPresentationgroup.Presentation = objDomain
                            objPresentationgroup.UserGroup = ObjuserGroup
                            m_TransactionManager.AddInsert(objPresentationgroup, m_userPrincipal.Identity.Name)
                        Next

                        m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                        m_TransactionManager.PerformTransaction()
                        iReturn = 1
                    Catch ex As Exception
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                        If rethrow Then
                            Throw
                        End If
                    Finally
                        Me.RemoveTaskLocking()
                    End Try
                End If


            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As PresentationGroup) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_PresentationGroupMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As PresentationGroup)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_PresentationGroupMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As PresentationGroup)
            Try
                m_PresentationGroupMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objArrPresentationGroup As ArrayList)
            Try
                If (Me.IsTaskFree()) Then
                    Try
                        Me.SetTaskLocking()

                        '

                        'Retrieve List userGroup
                        For Each ObjPresentationGroup As PresentationGroup In objArrPresentationGroup
                            m_TransactionManager.AddDelete(ObjPresentationGroup)
                        Next


                        m_TransactionManager.PerformTransaction()

                    Catch ex As Exception
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                        If rethrow Then
                            Throw
                        End If
                    Finally
                        Me.RemoveTaskLocking()
                    End Try
                End If



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

    End Class

End Namespace


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
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/3/2005 - 10:53:00 AM
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

Namespace KTB.DNet.BusinessFacade.Service
    Public Class FSCampaignKindFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_FSCampaignKindMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_FSCampaignKindMapper = MapperFactory.GetInstance.GetMapper(GetType(FSCampaignKind).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As FSCampaignKind
            Return CType(m_FSCampaignKindMapper.Retrieve(ID), FSCampaignKind)
        End Function

        Public Function RetrieveList(ByVal fsCampaignID As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FSCampaignKind), "FSCampaign.ID", MatchType.Exact, fsCampaignID))
            Dim FSCampaignKindColl As ArrayList = m_FSCampaignKindMapper.RetrieveByCriteria(criterias)
            If (FSCampaignKindColl.Count > 0) Then
                Return FSCampaignKindColl
            Else
                Return New ArrayList
            End If
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_FSCampaignKindMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_FSCampaignKindMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_FSCampaignKindMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSCampaignKind), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FSCampaignKindMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSCampaignKind), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FSCampaignKindMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _FSCampaignKind As ArrayList = m_FSCampaignKindMapper.RetrieveByCriteria(criterias)
            Return _FSCampaignKind
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FSCampaignKindColl As ArrayList = m_FSCampaignKindMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FSCampaignKindColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
       ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSCampaignKind), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FSCampaignKindMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim FSCampaignKindColl As ArrayList = m_FSCampaignKindMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FSCampaignKindColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FSCampaignKind), columnName, matchOperator, columnValue))
            Dim FSCampaignKindColl As ArrayList = m_FSCampaignKindMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FSCampaignKindColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSCampaignKind), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignKind), columnName, matchOperator, columnValue))

            Return m_FSCampaignKindMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As FSCampaignKind) As Integer
            Dim iReturn As Integer = -2
            Try
                m_FSCampaignKindMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function
        Public Function Update(ByVal objDomain As FSCampaignKind) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_FSCampaignKindMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As FSCampaignKind) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_FSCampaignKindMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As FSCampaignKind) As Integer
            Dim nResult As Integer = 1
            Try
                m_FSCampaignKindMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function InsertTransaction(ByVal arlFSKind As ArrayList, ByVal fsCampaign As FSCampaign) As Integer
            Dim returnValue As Integer = -1
            Dim arrListFSKind As ArrayList = New ArrayList
            If arlFSKind.Count > 0 Then
                For Each item As String In arlFSKind
                    Dim objFSKindFacade As FSKindFacade = New FSKindFacade(m_userPrincipal)
                    Dim objFSK As FSKind = objFSKindFacade.Retrieve(CType(item, Integer))
                    If Not objFSK Is Nothing Then
                        arrListFSKind.Add(objFSK)
                    End If
                Next
            End If
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arlFSKind.Count > 0 Then
                        For Each objFSKind As FSKind In arrListFSKind
                            Dim objFSCampaignKind As FSCampaignKind = New FSCampaignKind
                            objFSCampaignKind.FSCampaign = fsCampaign
                            objFSCampaignKind.FSKind = objFSKind
                            objFSCampaignKind.RowStatus = 0
                            m_TransactionManager.AddInsert(objFSCampaignKind, m_userPrincipal.Identity.Name)
                        Next
                    Else
                        arlFSKind = New ArrayList
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = -2
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

        Public Function UpdateTransaction(ByVal arlFSKind As ArrayList, ByVal fsCampaign As FSCampaign) As Integer
            Dim returnValue As Integer = -1

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arlFSKind.Count > 0 Then
                        For Each fsID As String In arlFSKind
                            Dim arrFSCampaignKind As ArrayList = New ArrayList
                            Dim objFSCampaignKind As FSCampaignKind = New FSCampaignKind

                            Dim objFSKindFacade As FSKindFacade = New FSKindFacade(m_userPrincipal)
                            Dim objFSKind As FSKind = objFSKindFacade.Retrieve(CType(fsID, Integer))

                            Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignKind), "FSCampaign", MatchType.Exact, fsCampaign.ID))
                            critCol.opAnd(New Criteria(GetType(FSCampaignKind), "FSKind", MatchType.Exact, objFSKind.ID))

                            arrFSCampaignKind = Me.Retrieve(critCol)
                            If arrFSCampaignKind.Count > 0 Then
                                objFSCampaignKind = arrFSCampaignKind(0)
                                objFSCampaignKind.RowStatus = 0
                                objFSCampaignKind.LastUpdateBy = m_userPrincipal.Identity.Name
                                objFSCampaignKind.LastUpdateTime = Date.Now
                                m_TransactionManager.AddUpdate(objFSCampaignKind, m_userPrincipal.Identity.Name)
                            Else

                                objFSCampaignKind = New FSCampaignKind
                                objFSCampaignKind.FSCampaign = fsCampaign
                                objFSCampaignKind.FSKind = objFSKind
                                objFSCampaignKind.RowStatus = 0
                                m_TransactionManager.AddInsert(objFSCampaignKind, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = -2
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

        Public Function DeleteTransaction(ByVal fsCampaign As FSCampaign) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim arlFSCampaignKind As ArrayList = New ArrayList
                    arlFSCampaignKind = Me.RetrieveList(fsCampaign.ID)

                    For Each objFSCampaignKind As FSCampaignKind In arlFSCampaignKind

                        objFSCampaignKind.RowStatus = 1
                        objFSCampaignKind.LastUpdateBy = m_userPrincipal.Identity.Name
                        objFSCampaignKind.LastUpdateTime = Date.Now
                        m_TransactionManager.AddUpdate(objFSCampaignKind, m_userPrincipal.Identity.Name)
                    Next
                    m_TransactionManager.PerformTransaction()
                    returnValue = -2
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
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is FSCampaignKind) Then
                CType(InsertArg.DomainObject, FSCampaignKind).ID = InsertArg.ID
                CType(InsertArg.DomainObject, FSCampaignKind).MarkLoaded()

            End If
        End Sub
#End Region

    End Class

End Namespace

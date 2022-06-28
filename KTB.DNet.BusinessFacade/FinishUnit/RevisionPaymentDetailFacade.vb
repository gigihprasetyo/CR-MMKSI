
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
'// Copyright  2018
'// ---------------------
'// $History      : $
'// Generated on 9/4/2018 - 9:15:55 AM
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

    Public Class RevisionPaymentDetailFacade
        Inherits AbstractFacade
#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_RevisionPaymentDetailMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_RevisionPaymentDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(RevisionPaymentDetail).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As RevisionPaymentDetail
            Return CType(m_RevisionPaymentDetailMapper.Retrieve(ID), RevisionPaymentDetail)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_RevisionPaymentDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_RevisionPaymentDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_RevisionPaymentDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionPaymentDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RevisionPaymentDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionPaymentDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RevisionPaymentDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _RevisionPaymentDetail As ArrayList = m_RevisionPaymentDetailMapper.RetrieveByCriteria(criterias)
            Return _RevisionPaymentDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RevisionPaymentDetailColl As ArrayList = m_RevisionPaymentDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return RevisionPaymentDetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim RevisionPaymentDetailColl As ArrayList = m_RevisionPaymentDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return RevisionPaymentDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RevisionPaymentDetailColl As ArrayList = m_RevisionPaymentDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(RevisionPaymentDetail), columnName, matchOperator, columnValue))
            Return RevisionPaymentDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionPaymentDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionPaymentDetail), columnName, matchOperator, columnValue))

            Return m_RevisionPaymentDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As RevisionPaymentDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_RevisionPaymentDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As RevisionPaymentDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_RevisionPaymentDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As RevisionPaymentDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_RevisionPaymentDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As RevisionPaymentDetail)
            Try
                m_RevisionPaymentDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function InsertRevisionPaymentHeaderDetail(ByVal objDomain As KTB.DNet.Domain.RevisionPaymentHeader, ByVal arrRevisionPaymentDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If arrRevisionPaymentDetail.Count > 0 Then
                        For Each objRevisionPaymentDetail As RevisionPaymentDetail In arrRevisionPaymentDetail
                            objRevisionPaymentDetail.RevisionPaymentHeader = objDomain
                            m_TransactionManager.AddInsert(objRevisionPaymentDetail, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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

        Public Function UpdateRevisionPaymentHeaderDetail(ByVal objDomain As RevisionPaymentHeader, ByVal objDomainCurrent As RevisionPaymentHeader, ByVal arrDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1

            Dim ariddetailOld As ArrayList = New ArrayList

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If Not IsNothing(objDomainCurrent.RevisionPaymentDetails) AndAlso objDomainCurrent.RevisionPaymentDetails.Count > 0 Then
                        For Each objDetailOld As RevisionPaymentDetail In objDomainCurrent.RevisionPaymentDetails
                            'm_TransactionManager.AddDelete(objDetail)
                            Dim statusDetail As Boolean = False
                            'update detail old to detail new
                            For Each objDetailNew As RevisionPaymentDetail In arrDetail
                                If objDetailOld.RevisionFaktur.ID = objDetailNew.RevisionFaktur.ID Then
                                    statusDetail = True

                                    objDetailOld.RevisionSAPDoc = objDetailNew.RevisionSAPDoc
                                    objDetailOld.RevisionFaktur = objDetailNew.RevisionFaktur
                                    objDetailOld.RevisionPaymentHeader = objDomain
                                    objDetailOld.CancelReason = objDetailNew.CancelReason
                                    objDetailOld.IsCancel = objDetailNew.IsCancel

                                    'update RevisionPaymentDetail
                                    m_TransactionManager.AddUpdate(objDetailOld, m_userPrincipal.Identity.Name)
                                    Exit For
                                End If
                            Next

                            If statusDetail = False Then
                                'delete RevisionPaymentDetail                                
                                objDetailOld.RowStatus = -1
                                m_TransactionManager.AddUpdate(objDetailOld, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    If arrDetail.Count > 0 Then
                        For Each objDetail As RevisionPaymentDetail In arrDetail
                            Dim statusDetail As Boolean = False
                            For Each objDetailOld As RevisionPaymentDetail In objDomainCurrent.RevisionPaymentDetails
                                If objDetail.RevisionFaktur.ID = objDetailOld.RevisionFaktur.ID Then
                                    statusDetail = True
                                    Exit For
                                End If
                            Next
                            If statusDetail = False Then
                                'insert new RevisionPaymentDetail
                                objDetail.RevisionPaymentHeader = objDomain
                                objDetail.RowStatus = 0

                                m_TransactionManager.AddInsert(objDetail, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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
            If (TypeOf InsertArg.DomainObject Is RevisionPaymentHeader) Then
                CType(InsertArg.DomainObject, RevisionPaymentHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, RevisionPaymentHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is RevisionPaymentDetail) Then
                CType(InsertArg.DomainObject, RevisionPaymentDetail).ID = InsertArg.ID
            End If
        End Sub


#End Region

#Region "Custom Method"
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_RevisionPaymentDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function
#End Region

    End Class

End Namespace


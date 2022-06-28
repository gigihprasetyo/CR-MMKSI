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
'// Generated on 7/16/2007 - 11:09:45 AM
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

Imports KTb.DNet.Domain
Imports KTb.DNet.Domain.Search
Imports KTb.DNet.DataMapper.Framework
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.Claim
    Public Class ClaimDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ClaimDetailMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_ClaimDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(ClaimDetail).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
        End Sub

#End Region

#Region "Retrieve"

        Public Function GetTotalClaim(ByVal crits As CriteriaComposite) As Decimal

            Dim Criterias As CriteriaComposite = Nothing
            Criterias = crits

            Dim Result As Decimal = 0

            Dim arlToCalculate As ArrayList = Me.Retrieve(criterias)


            For Each item As ClaimDetail In arlToCalculate
                Result = Result + (item.ApprovedQty * item.SparePartPOStatusDetail.ClaimPriceUnit)
            Next
            'End If


            Return Result

        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ClaimDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As ClaimDetail
            Return CType(m_ClaimDetailMapper.Retrieve(ID), ClaimDetail)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
     ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ClaimDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ClaimDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ClaimDetailColl As ArrayList = m_ClaimDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ClaimDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ClaimDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ClaimDetailColl As ArrayList = m_ClaimDetailMapper.RetrieveByCriteria(Criterias, sortColl)
            Return ClaimDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite) As ArrayList
            Dim ClaimDetailColl As ArrayList = m_ClaimDetailMapper.RetrieveByCriteria(Criterias)
            Return ClaimDetailColl
        End Function
#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As ClaimDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                m_ClaimDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Sub DeleteFromDB(ByVal objDomain As ClaimDetail)
            Try
                m_ClaimDetailMapper.Delete(objDomain)

            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function Update(ByVal objDomain As ClaimDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ClaimDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function InsertClaimHeaderDetail(ByVal objDomain As KTB.DNet.Domain.ClaimHeader, ByVal arrClaimDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If arrClaimDetail.Count > 0 Then
                        For Each objClaimDetail As ClaimDetail In arrClaimDetail
                            objClaimDetail.ClaimHeader = objDomain
                            m_TransactionManager.AddInsert(objClaimDetail, m_userPrincipal.Identity.Name)
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

        Public Function InsertUpdateDeleteClaimHeaderDetail(ByVal objDomain As KTB.DNet.Domain.ClaimHeader, ByVal arrClaimDetailAdd As ArrayList, ByVal arrClaimDetailUpdate As ArrayList, ByVal arrClaimDetailDelete As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrClaimDetailAdd.Count > 0 Then
                        For Each objClaimDetailAdd As ClaimDetail In arrClaimDetailAdd
                            objClaimDetailAdd.ClaimHeader = objDomain
                            m_TransactionManager.AddInsert(objClaimDetailAdd, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    If arrClaimDetailUpdate.Count > 0 Then
                        For Each objClaimDetailUpdate As ClaimDetail In arrClaimDetailUpdate
                            m_TransactionManager.AddUpdate(objClaimDetailUpdate, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    If arrClaimDetailDelete.Count > 0 Then
                        For Each objClaimDetailDelete As ClaimDetail In arrClaimDetailDelete
                            m_TransactionManager.AddDelete(objClaimDetailDelete)
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
            If (TypeOf InsertArg.DomainObject Is ClaimHeader) Then
                CType(InsertArg.DomainObject, ClaimHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, ClaimHeader).MarkLoaded()
                'ElseIf (TypeOf InsertArg.DomainObject Is ClaimDetail) Then
                '    CType(InsertArg.DomainObject, ClaimDetail).ID = InsertArg.ID
            End If
        End Sub
#End Region

#Region "Custom Method"

#End Region


    End Class
End Namespace


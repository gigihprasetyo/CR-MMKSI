
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
'// Generated on 1/9/2018 - 11:29:16 AM
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

namespace KTB.DNET.BusinessFacade

	public class MSPTransferPaymentFacade
		inherits AbstractFacade

#Region "Private Variables"

	Private m_userPrincipal As IPrincipal = Nothing
	Private m_MSPTransferPaymentMapper as IMapper
	
	Private	m_TransactionManager As TransactionManager
	
#end region

#region "Constructor"

Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_MSPTransferPaymentMapper = MapperFactory.GetInstance.GetMapper(GetType(MSPTransferPayment).ToString)
            m_TransactionManager = New TransactionManager
            ' custom, add transaction detail 
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.MSPTransferPayment))
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.MSPTransferPaymentDetail))
		
End Sub

#end region

#Region "Retrieve"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.MSPTransferPayment) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.MSPTransferPayment).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.MSPTransferPayment).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is MSPTransferPaymentDetail) Then
                CType(InsertArg.DomainObject, MSPTransferPaymentDetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function Retrieve(ByVal ID As Integer) As MSPTransferPayment
            Return CType(m_MSPTransferPaymentMapper.Retrieve(ID), MSPTransferPayment)
        End Function

        Public Function Retrieve(ByVal RegNumber As String) As MSPTransferPayment
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MSPTransferPayment), "RegNumber", MatchType.Exact, RegNumber))

            Dim MSPTrfPaymentCColl As ArrayList = m_MSPTransferPaymentMapper.RetrieveByCriteria(criterias)
            If (MSPTrfPaymentCColl.Count > 0) Then
                Return CType(MSPTrfPaymentCColl(0), MSPTransferPayment)
            End If
            Return New MSPTransferPayment
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MSPTransferPaymentMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MSPTransferPaymentMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_MSPTransferPaymentMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPTransferPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MSPTransferPaymentMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPTransferPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MSPTransferPaymentMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MSPTransferPayment As ArrayList = m_MSPTransferPaymentMapper.RetrieveByCriteria(criterias)
            Return _MSPTransferPayment
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MSPTransferPaymentColl As ArrayList = m_MSPTransferPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MSPTransferPaymentColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MSPTransferPaymentColl As ArrayList = m_MSPTransferPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MSPTransferPaymentColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(MSPTransferPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim MSPTrfPaymentColl As ArrayList = m_MSPTransferPaymentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MSPTrfPaymentColl

        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MSPTransferPaymentColl As ArrayList = m_MSPTransferPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(MSPTransferPayment), columnName, matchOperator, columnValue))
            Return MSPTransferPaymentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPTransferPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPTransferPayment), columnName, matchOperator, columnValue))

            Return m_MSPTransferPaymentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

		
		Public Function Insert(ByVal objDomain As  MSPTransferPayment) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    'insert MSPTransferPayment
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    'insert MSPTranserPaymentDetail
                    For Each detail As MSPTransferPaymentDetail In objDomain.MSPTransferPaymentDetails
                        detail.MSPTransferPayment = objDomain
                        m_TransactionManager.AddInsert(detail, m_userPrincipal.Identity.Name)
                    Next
                    
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
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

        Public Function Update(ByVal objDomain As MSPTransferPayment) As Integer
           Dim nResult As Integer = -1
            Try
                nResult = m_MSPTransferPaymentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function UpdateTransaction(ByVal objDomainColl As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    For Each item As MSPTransferPayment In objDomainColl
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next
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

        Public Sub Delete(ByVal objDomain As MSPTransferPayment)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_MSPTransferPaymentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As MSPTransferPayment)
            Try
                m_MSPTransferPaymentMapper.Delete(objDomain)
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
		
	end class
	
end namespace


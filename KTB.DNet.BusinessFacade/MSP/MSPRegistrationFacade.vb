
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
'// Copyright  2017
'// ---------------------
'// $History      : $
'// Generated on 12/12/2017 - 1:46:13 PM
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

	public class MSPRegistrationFacade
		inherits AbstractFacade

#Region "Private Variables"

	Private m_userPrincipal As IPrincipal = Nothing
	Private m_MSPRegistrationMapper as IMapper
	
	Private	m_TransactionManager As TransactionManager
	
#end region

#region "Constructor"

Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            m_TransactionManager = New TransactionManager
            Me.m_MSPRegistrationMapper = MapperFactory.GetInstance.GetMapper(GetType(MSPRegistration).ToString)
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.MSPRegistration))
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.MSPRegistrationHistory))
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.MSPCustomer))
		
End Sub

#end region

#Region "Retrieve"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.MSPRegistration) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.MSPRegistration).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.MSPRegistration).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is MSPRegistrationHistory) Then
                CType(InsertArg.DomainObject, MSPRegistrationHistory).ID = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is MSPCustomer) Then
                CType(InsertArg.DomainObject, MSPCustomer).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.MSPCustomer).MarkLoaded()
            End If
        End Sub

       Public Function Retrieve(ByVal ID as integer ) As MSPRegistration
            Return CType(m_MSPRegistrationMapper.Retrieve(ID), MSPRegistration)
       End Function
        
        
        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MSPRegistrationMapper.RetrieveByCriteria(criterias)
        End Function
        
        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MSPRegistrationMapper.RetrieveByCriteria(criterias, sorts)
        End Function

		Public Function RetrieveList() As ArrayList
            Return m_MSPRegistrationMapper.RetrieveList
        End Function
        
        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPRegistration), sortColumn, sortDirection))
            Else
                sortColl = Nothing
           End If

            Return m_MSPRegistrationMapper.RetrieveList(sortColl)
        End Function
        
        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPRegistration), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

			Return m_MSPRegistrationMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function
		
		Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MSPRegistration As ArrayList = m_MSPRegistrationMapper.RetrieveByCriteria(criterias)
            Return _MSPRegistration
        End Function

		Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MSPRegistrationColl As ArrayList = m_MSPRegistrationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

			Return MSPRegistrationColl
        End Function

		Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MSPRegistrationColl As ArrayList = m_MSPRegistrationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MSPRegistrationColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(MSPRegistration), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim MSPRegistrationColl As ArrayList = m_MSPRegistrationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MSPRegistrationColl

        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MSPRegistrationColl As ArrayList = m_MSPRegistrationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(MSPRegistration), columnName, matchOperator, columnValue))
            Return MSPRegistrationColl
        End Function

		Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPRegistration), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPRegistration), columnName, matchOperator, columnValue))

            Return m_MSPRegistrationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#end Region

#Region "Transaction/Other Public Method"

		
        Public Function Insert(ByVal objDomain As MSPRegistration, ByVal objMSPRegistrationHistory As MSPRegistrationHistory, ByVal objMSPCustomer As MSPCustomer) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    'insert MSPCustomer
                    m_TransactionManager.AddInsert(objMSPCustomer, m_userPrincipal.Identity.Name)
                    'insert MSPRegistration
                    objDomain.MSPCustomer = objMSPCustomer
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    'insert MSPRegistrationHistory
                    objMSPRegistrationHistory.MSPRegistration = objDomain
                    m_TransactionManager.AddInsert(objMSPRegistrationHistory, m_userPrincipal.Identity.Name)

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

        Public Function Update(ByVal objDomain As MSPRegistration, ByVal objMSPRegistrationHistory As MSPRegistrationHistory, ByVal objMSPCustomer As MSPCustomer) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    'update MSPCustomer
                    m_TransactionManager.AddUpdate(objMSPCustomer, m_userPrincipal.Identity.Name)
                    'update MSPRegistration
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    'update MSPRegistrationHistory
                    objMSPRegistrationHistory.MSPRegistration = objDomain
                    m_TransactionManager.AddUpdate(objMSPRegistrationHistory, m_userPrincipal.Identity.Name)

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

        Public Sub Delete(ByVal objDomain As MSPRegistration)
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    'update MSPCustomer
                    objDomain.MSPCustomer.RowStatus = CType(DBRowStatus.Deleted, Short)
                    m_TransactionManager.AddDelete(objDomain.MSPCustomer)
                    'update MSPRegistration
                    objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                    m_TransactionManager.AddDelete(objDomain)
                    'update MSPRegistrationHistory
                    If objDomain.MSPRegistrationHistorys.Count > 0 Then
                        For Each objMSPRegistrationHistory As MSPRegistrationHistory In objDomain.MSPRegistrationHistorys
                            objMSPRegistrationHistory.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddDelete(objMSPRegistrationHistory)
                        Next
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
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
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As MSPRegistration)
            Try
                m_MSPRegistrationMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub
        
#End Region

#Region "Custom Method"
        Public Function RetrieveSp(str As String) As DataSet
            Return m_MSPRegistrationMapper.RetrieveDataSet(str)
        End Function
#End Region
		
	end class
	
end namespace


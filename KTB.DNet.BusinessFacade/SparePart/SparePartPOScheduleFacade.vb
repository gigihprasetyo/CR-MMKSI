
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
'// Generated on 08/03/2016 - 13:07:16
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

namespace KTB.DNET.BusinessFacade.SparePart

	public class SparePartPOScheduleFacade
		inherits AbstractFacade

#Region "Private Variables"

	Private m_userPrincipal As IPrincipal = Nothing
	Private m_SparePartPOScheduleMapper as IMapper
	
	Private	m_TransactionManager As TransactionManager
	
#end region

#region "Constructor"

Public Sub New(ByVal userPrincipal As IPrincipal)

	Me.m_userPrincipal = userPrincipal
	me.m_SparePartPOScheduleMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartPOSchedule).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(SparePartPOSchedule))
            Me.DomainTypeCollection.Add(GetType(SparePartPOScheduleDealer))
		
End Sub

#end region

#Region "Retrieve"

       Public Function Retrieve(ByVal ID as integer ) As SparePartPOSchedule
            Return CType(m_SparePartPOScheduleMapper.Retrieve(ID), SparePartPOSchedule)
       End Function
        
        Public Function Retrieve(ByVal Code As String) As SparePartPOSchedule
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOSchedule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartPOSchedule), "SparePartPOScheduleCode", MatchType.Exact, Code))

			Dim SparePartPOScheduleColl As ArrayList = m_SparePartPOScheduleMapper.RetrieveByCriteria(criterias)
            If (SparePartPOScheduleColl.Count > 0) Then
                Return CType(SparePartPOScheduleColl(0), SparePartPOSchedule)
            End If
            Return New SparePartPOSchedule
        End Function
        
        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartPOScheduleMapper.RetrieveByCriteria(criterias)
        End Function
        
        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartPOScheduleMapper.RetrieveByCriteria(criterias, sorts)
        End Function

		Public Function RetrieveList() As ArrayList
            Return m_SparePartPOScheduleMapper.RetrieveList
        End Function
        
        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPOSchedule), sortColumn, sortDirection))
            Else
                sortColl = Nothing
           End If

            Return m_SparePartPOScheduleMapper.RetrieveList(sortColl)
        End Function
        
        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPOSchedule), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

			Return m_SparePartPOScheduleMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function
		
		Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOSchedule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SparePartPOSchedule As ArrayList = m_SparePartPOScheduleMapper.RetrieveByCriteria(criterias)
            Return _SparePartPOSchedule
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPOSchedule), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AppConfigColl As ArrayList = m_SparePartPOScheduleMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AppConfigColl
        End Function

		Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOSchedule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartPOScheduleColl As ArrayList = m_SparePartPOScheduleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

			Return SparePartPOScheduleColl
        End Function

		Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartPOScheduleColl As ArrayList = m_SparePartPOScheduleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartPOScheduleColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOSchedule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartPOScheduleColl As ArrayList = m_SparePartPOScheduleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SparePartPOSchedule), columnName, matchOperator, columnValue))
            Return SparePartPOScheduleColl
        End Function

		Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPOSchedule), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOSchedule), columnName, matchOperator, columnValue))

            Return m_SparePartPOScheduleMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#end Region

#Region "Transaction/Other Public Method"

		Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOSchedule), "SparePartPOScheduleCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SparePartPOSchedule), "SparePartPOScheduleCode", AggregateType.Count)
            Return CType(m_SparePartPOScheduleMapper.RetrieveScalar(agg, crit), Integer)
        End Function
		
		Public Function Insert(ByVal objDomain As  SparePartPOSchedule) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SparePartPOScheduleMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Insert(ByVal objDomain As SparePartPOSchedule, ByVal arrDealer As ArrayList) As Integer
            Dim iReturn As Integer = -2
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If arrDealer.Count > 0 Then
                        For Each objSPOSDealer As SparePartPOScheduleDealer In arrDealer
                            objSPOSDealer.SparePartPOSchedule = objDomain
                            m_TransactionManager.AddInsert(objSPOSDealer, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    iReturn = objDomain.ID
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartPOSchedule, ByVal arrDealerOld As ArrayList, ByVal arrDealer As ArrayList) As Integer
            Dim iReturn As Integer = -2
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()


                    For Each objSPOSDealer As SparePartPOScheduleDealer In arrDealerOld
                        m_TransactionManager.AddDelete(objSPOSDealer)
                    Next

                    If arrDealer.Count > 0 Then
                        For Each objSPOSDealer As SparePartPOScheduleDealer In arrDealer
                            objSPOSDealer.SparePartPOSchedule = objDomain
                            m_TransactionManager.AddInsert(objSPOSDealer, m_userPrincipal.Identity.Name)
                        Next
                    End If
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
            Return iReturn

        End Function


        Public Function Update(ByVal objDomain As SparePartPOSchedule) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartPOScheduleMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartPOSchedule)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SparePartPOScheduleMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SparePartPOSchedule)
            Try
                m_SparePartPOScheduleMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub
        
#End Region

#Region "Custom Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is SparePartPOSchedule) Then
                CType(InsertArg.DomainObject, SparePartPOSchedule).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SparePartPOSchedule).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is SparePartPOScheduleDealer) Then
                CType(InsertArg.DomainObject, SparePartPOScheduleDealer).ID = InsertArg.ID
            End If
        End Sub
#End Region
		
	end class
	
end namespace


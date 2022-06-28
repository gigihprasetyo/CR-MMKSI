
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
'// Copyright  2020
'// ---------------------
'// $History      : $
'// Generated on 11/10/2020 - 4:07:25 PM
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

Namespace KTB.DNET.BusinessFacade

    Public Class MSPExDebitChargeFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_MSPExDebitChargeMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_MSPExDebitChargeMapper = MapperFactory.GetInstance.GetMapper(GetType(MSPExDebitCharge).ToString)


            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(MSPExDebitCharge))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As MSPExDebitCharge
            Return CType(m_MSPExDebitChargeMapper.Retrieve(ID), MSPExDebitCharge)
        End Function

        Public Function Retrieve(ByVal Code As String) As MSPExDebitCharge
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExDebitCharge), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MSPExDebitCharge), "DebitChargeNo", MatchType.Exact, Code))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(MSPExDebitCharge), "ID", Sort.SortDirection.DESC))

            Dim MSPExDebitChargeColl As ArrayList = m_MSPExDebitChargeMapper.RetrieveByCriteria(criterias)
            If (MSPExDebitChargeColl.Count > 0) Then
                Return CType(MSPExDebitChargeColl(0), MSPExDebitCharge)
            End If
            Return New MSPExDebitCharge
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MSPExDebitChargeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MSPExDebitChargeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_MSPExDebitChargeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPExDebitCharge), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MSPExDebitChargeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPExDebitCharge), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MSPExDebitChargeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExDebitCharge), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MSPExDebitCharge As ArrayList = m_MSPExDebitChargeMapper.RetrieveByCriteria(criterias)
            Return _MSPExDebitCharge
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExDebitCharge), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MSPExDebitChargeColl As ArrayList = m_MSPExDebitChargeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MSPExDebitChargeColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(MSPExDebitCharge), SortColumn, sortDirection))
            Dim MSPExDebitChargeColl As ArrayList = m_MSPExDebitChargeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MSPExDebitChargeColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MSPExDebitChargeColl As ArrayList = m_MSPExDebitChargeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MSPExDebitChargeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExDebitCharge), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MSPExDebitChargeColl As ArrayList = m_MSPExDebitChargeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(MSPExDebitCharge), columnName, matchOperator, columnValue))
            Return MSPExDebitChargeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPExDebitCharge), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExDebitCharge), columnName, matchOperator, columnValue))

            Return m_MSPExDebitChargeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExDebitCharge), "MSPExDebitChargeCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(MSPExDebitCharge), "MSPExDebitChargeCode", AggregateType.Count)
            Return CType(m_MSPExDebitChargeMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As MSPExDebitCharge) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_MSPExDebitChargeMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As MSPExDebitCharge) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_MSPExDebitChargeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As MSPExDebitCharge)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.Rowstatus = CType(DBRowStatus.Deleted, Short)
                m_MSPExDebitChargeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As MSPExDebitCharge)
            Try
                m_MSPExDebitChargeMapper.Delete(objDomain)
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

            If (TypeOf InsertArg.DomainObject Is MSPExDebitMemo) Then
                CType(InsertArg.DomainObject, MSPExDebitMemo).ID = InsertArg.ID
                CType(InsertArg.DomainObject, MSPExDebitMemo).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is MSPExDebitCharge) Then
                CType(InsertArg.DomainObject, MSPExDebitCharge).ID = InsertArg.ID
            End If
        End Sub

        Function InsertTransaction(arrMSPExDebitCharge As ArrayList, arrMSPExDebitMemo As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If Not IsNothing(arrMSPExDebitCharge) Then
                        If arrMSPExDebitCharge.Count > 0 Then
                            For Each oMSPExDebitCharge As MSPExDebitCharge In arrMSPExDebitCharge
                                m_TransactionManager.AddInsert(oMSPExDebitCharge, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrMSPExDebitMemo) Then
                        If arrMSPExDebitMemo.Count > 0 Then
                            For Each oMSPExDebitMemo As MSPExDebitMemo In arrMSPExDebitMemo
                                m_TransactionManager.AddInsert(oMSPExDebitMemo, m_userPrincipal.Identity.Name)
                            Next
                        End If
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

        Public Function RetrieveByRegistration(ByVal oMSPExRegistration As MSPExRegistration) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExDebitCharge), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MSPExDebitCharge), "MSPExRegistration.ID", MatchType.Exact, oMSPExRegistration.ID))

            Dim MSPExDebitChargeColl As ArrayList = m_MSPExDebitChargeMapper.RetrieveByCriteria(criterias)
            If (MSPExDebitChargeColl.Count > 0) Then
                Return MSPExDebitChargeColl
            End If
            Return New ArrayList
        End Function
#End Region
    End Class
End Namespace


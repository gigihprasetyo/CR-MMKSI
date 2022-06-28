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

Namespace KTB.DNet.BusinessFacade.PO
    Public Class CreditMasterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_CreditMasterMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_CreditMasterMapper = MapperFactory.GetInstance().GetMapper(GetType(CreditMaster).ToString)
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As CreditMaster
            Return CType(m_CreditMasterMapper.Retrieve(ID), CreditMaster)
        End Function

        Public Function Retrieve_(ByVal CreditAccount As String, ByVal PaymentType As Byte) As CreditMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CreditMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CreditMaster), "CreditAccount", MatchType.Exact, CreditAccount))
            criterias.opAnd(New Criteria(GetType(CreditMaster), "PaymentType", MatchType.Exact, PaymentType))

            Dim CreditMasterColl As ArrayList = m_CreditMasterMapper.RetrieveByCriteria(criterias)
            If (CreditMasterColl.Count > 0) Then
                Return CType(CreditMasterColl(0), CreditMaster)
            End If
            Return New CreditMaster
        End Function

        Public Function Retrieve(ByVal PC As ProductCategory, ByVal CreditAccount As String, ByVal PaymentType As Byte) As CreditMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CreditMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CreditMaster), "CreditAccount", MatchType.Exact, CreditAccount))
            criterias.opAnd(New Criteria(GetType(CreditMaster), "PaymentType", MatchType.Exact, PaymentType))
            criterias.opAnd(New Criteria(GetType(CreditMaster), "ProductCategory.ID", MatchType.Exact, PC.ID))

            Dim CreditMasterColl As ArrayList = m_CreditMasterMapper.RetrieveByCriteria(criterias)
            If (CreditMasterColl.Count > 0) Then
                Return CType(CreditMasterColl(0), CreditMaster)
            End If
            Return New CreditMaster
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_CreditMasterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_CreditMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_CreditMasterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CreditMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CreditMasterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CreditMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CreditMasterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CreditMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("CreditMasterCode")) Then
                sortColl.Add(New Sort(GetType(CreditMaster), "CreditMasterCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _CreditMaster As ArrayList = m_CreditMasterMapper.RetrieveByCriteria(criterias, sortColl)
            Return _CreditMaster
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CreditMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CreditMasterColl As ArrayList = m_CreditMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CreditMasterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim CreditMasterColl As ArrayList = m_CreditMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CreditMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CreditMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CreditMaster), columnName, matchOperator, columnValue))
            Dim CreditMasterColl As ArrayList = m_CreditMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CreditMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CreditMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CreditMaster), columnName, matchOperator, columnValue))

            Return m_CreditMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CreditMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CreditMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CreditMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As CreditMaster) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_CreditMasterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As CreditMaster) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_CreditMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Update(ByVal arlCM As ArrayList) As Integer
            Dim nReturn As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As CreditMaster In arlCM
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        nReturn = 0
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
        End Function

        Public Sub Delete(ByVal objDomain As CreditMaster)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_CreditMasterMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As CreditMaster)
            Try
                m_CreditMasterMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CreditMaster), "CreditMasterCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(CreditMaster), "CreditMasterCode", AggregateType.Count)

            Return CType(m_CreditMasterMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_CreditMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CreditMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim CreditMasterColl As ArrayList = m_CreditMasterMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return CreditMasterColl
        End Function

#End Region

    End Class

End Namespace

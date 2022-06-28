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
    Public Class sp_AllocPOFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_sp_AllocPOMapper As IMapper
        Private m_V_POTotalDetailMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_V_POTotalDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(V_POTotalDetail).ToString)
            Me.m_sp_AllocPOMapper = MapperFactory.GetInstance().GetMapper(GetType(sp_AllocPO).ToString)
        End Sub

#End Region

#Region "Retrieve"
        Public Function RetrieveFromSP(ByVal AllocationDate As Date, ByVal sPODetails As String) As sp_AllocPO
            Dim SQL As String
            Dim aSAPs As ArrayList
            Dim oSAP As sp_AllocPO

            SQL = "exec sp_AllocPO '" & AllocationDate.ToString("yyyy.MM.dd HH:mm:ss") & "', '" & sPODetails & "'"
            aSAPs = m_sp_AllocPOMapper.RetrieveSP(SQL)
            If aSAPs.Count > 0 Then
                oSAP = aSAPs(0)
            Else
                oSAP = Nothing
            End If
            Return oSAP
        End Function

        Public Function Retrieve(ByVal ID As Integer) As sp_AllocPO
            Return CType(m_sp_AllocPOMapper.Retrieve(ID), sp_AllocPO)
        End Function

        Public Function Retrieve(ByVal sp_AllocPOCode As String) As sp_AllocPO
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_AllocPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(sp_AllocPO), "sp_AllocPOCode", MatchType.Exact, sp_AllocPOCode))

            Dim sp_AllocPOColl As ArrayList = m_sp_AllocPOMapper.RetrieveByCriteria(criterias)
            If (sp_AllocPOColl.Count > 0) Then
                Return CType(sp_AllocPOColl(0), sp_AllocPO)
            End If
            Return New sp_AllocPO
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_sp_AllocPOMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_sp_AllocPOMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_sp_AllocPOMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_AllocPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_AllocPOMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_AllocPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_AllocPOMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_AllocPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("sp_AllocPOCode")) Then
                sortColl.Add(New Sort(GetType(sp_AllocPO), "sp_AllocPOCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _sp_AllocPO As ArrayList = m_sp_AllocPOMapper.RetrieveByCriteria(criterias, sortColl)
            Return _sp_AllocPO
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_AllocPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sp_AllocPOColl As ArrayList = m_sp_AllocPOMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_AllocPOColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim sp_AllocPOColl As ArrayList = m_sp_AllocPOMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_AllocPOColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_AllocPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(sp_AllocPO), columnName, matchOperator, columnValue))
            Dim sp_AllocPOColl As ArrayList = m_sp_AllocPOMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_AllocPOColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_AllocPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_AllocPO), columnName, matchOperator, columnValue))

            Return m_sp_AllocPOMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_AllocPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_AllocPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_AllocPOMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As sp_AllocPO) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_sp_AllocPOMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As sp_AllocPO) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_sp_AllocPOMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As sp_AllocPO)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_sp_AllocPOMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As sp_AllocPO)
            Try
                m_sp_AllocPOMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_AllocPO), "sp_AllocPOCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(sp_AllocPO), "sp_AllocPOCode", AggregateType.Count)

            Return CType(m_sp_AllocPOMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_sp_AllocPOMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_AllocPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim sp_AllocPOColl As ArrayList = m_sp_AllocPOMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return sp_AllocPOColl
        End Function

#End Region

    End Class

End Namespace

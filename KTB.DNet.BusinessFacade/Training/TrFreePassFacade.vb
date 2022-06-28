
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
'// Copyright  2019
'// ---------------------
'// $History      : $
'// Generated on 10/07/2019 - 10:11:23
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
Imports System.Collections.Generic


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class TrFreePassFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TrFreePassMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_TrFreePassMapper = MapperFactory.GetInstance.GetMapper(GetType(TrFreePass).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TrFreePass
            Return CType(m_TrFreePassMapper.Retrieve(ID), TrFreePass)
        End Function

        'Public Function Retrieve(ByVal Code As String) As TrFreePass
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrFreePass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(TrFreePass), "TrMRTC.Code", MatchType.Exact, Code))

        '    Dim TrFreePassColl As ArrayList = m_TrFreePassMapper.RetrieveByCriteria(criterias)
        '    If (TrFreePassColl.Count > 0) Then
        '        Return CType(TrFreePassColl(0), TrFreePass)
        '    End If
        '    Return New TrFreePass
        'End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TrFreePassMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TrFreePassMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TrFreePassMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrFreePass), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrFreePassMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrFreePass), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrFreePassMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrFreePass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TrFreePass As ArrayList = m_TrFreePassMapper.RetrieveByCriteria(criterias)
            Return _TrFreePass
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrFreePass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrFreePassColl As ArrayList = m_TrFreePassMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrFreePassColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(TrFreePass), SortColumn, sortDirection))
            Dim TrFreePassColl As ArrayList = m_TrFreePassMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrFreePassColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrFreePass), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim TrFreePassColl As ArrayList = m_TrFreePassMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrFreePassColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TrFreePassColl As ArrayList = m_TrFreePassMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TrFreePassColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrFreePass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrFreePassColl As ArrayList = m_TrFreePassMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TrFreePass), columnName, matchOperator, columnValue))
            Return TrFreePassColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrFreePass), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrFreePass), columnName, matchOperator, columnValue))

            Return m_TrFreePassMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        'Public Function ValidateCode(ByVal Code As String) As Integer
        '    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrFreePass), "TrMRTC.Code", MatchType.Exact, Code))
        '    Dim agg As Aggregate = New Aggregate(GetType(TrFreePass), "TrMRTC.Code", AggregateType.Count)
        '    Return CType(m_TrFreePassMapper.RetrieveScalar(agg, crit), Integer)
        'End Function

        Public Function Insert(ByVal objDomain As TrFreePass) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TrFreePassMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TrFreePass) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TrFreePassMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TrFreePass)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TrFreePassMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As TrFreePass)
            Try
                m_TrFreePassMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"  
        Public Function DownloadTemplate() As DataTable
            Dim dtTemp As New DataTable
            dtTemp.Columns.Add("DealerCode")
            dtTemp.Columns.Add("TahunFiskal")
            dtTemp.Columns.Add("Qty")

            Dim row As DataRow = dtTemp.NewRow
            row("DealerCode") = "100001"
            row("TahunFiskal") = "2019/2020"
            row("Qty") = "1"

            dtTemp.Rows.Add(row)

            Return dtTemp
        End Function

#End Region

    End Class

End Namespace


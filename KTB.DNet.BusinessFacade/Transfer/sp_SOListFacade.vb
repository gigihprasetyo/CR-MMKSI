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
    Public Class sp_SOListFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_sp_SOListMapper As IMapper
        Private m_V_POTotalDetailMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_V_POTotalDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(V_POTotalDetail).ToString)
            Me.m_sp_SOListMapper = MapperFactory.GetInstance().GetMapper(GetType(sp_SOList).ToString)
        End Sub

#End Region

#Region "Retrieve"


        Public Function RetrieveFromSP(ByVal DealerID As Integer, DueDate As DateTime, PaymentPurposeID As Integer, Optional ByVal CompanyCode As String = "") As ArrayList
            Dim SQL As String
            If CompanyCode = "" Then
                SQL = "exec sp_SOList " & DealerID.ToString() & ", '" & Format(DueDate, "yyyy/MM/dd") & "', " & PaymentPurposeID.ToString() & " "
            Else
                SQL = "exec sp_SOList " & DealerID.ToString() & ", '" & Format(DueDate, "yyyy/MM/dd") & "', " & PaymentPurposeID.ToString() & ", '" & CompanyCode & "'"
            End If

            Return Me.m_sp_SOListMapper.RetrieveSP(SQL)
        End Function


        Public Function Retrieve(ByVal ID As Integer) As sp_SOList
            Return CType(m_sp_SOListMapper.Retrieve(ID), sp_SOList)
        End Function

        Public Function Retrieve(ByVal sp_SOListCode As String) As sp_SOList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_SOList), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(sp_SOList), "sp_SOListCode", MatchType.Exact, sp_SOListCode))

            Dim sp_SOListColl As ArrayList = m_sp_SOListMapper.RetrieveByCriteria(criterias)
            If (sp_SOListColl.Count > 0) Then
                Return CType(sp_SOListColl(0), sp_SOList)
            End If
            Return New sp_SOList
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_sp_SOListMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_sp_SOListMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_sp_SOListMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_SOList), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_SOListMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_SOList), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_SOListMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_SOList), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("sp_SOListCode")) Then
                sortColl.Add(New Sort(GetType(sp_SOList), "sp_SOListCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _sp_SOList As ArrayList = m_sp_SOListMapper.RetrieveByCriteria(criterias, sortColl)
            Return _sp_SOList
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_SOList), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sp_SOListColl As ArrayList = m_sp_SOListMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_SOListColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim sp_SOListColl As ArrayList = m_sp_SOListMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_SOListColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_SOList), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(sp_SOList), columnName, matchOperator, columnValue))
            Dim sp_SOListColl As ArrayList = m_sp_SOListMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_SOListColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_SOList), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_SOList), columnName, matchOperator, columnValue))

            Return m_sp_SOListMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_SOList), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_SOList), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_SOListMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As sp_SOList) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_sp_SOListMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As sp_SOList) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_sp_SOListMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As sp_SOList)
            'Dim nResult As Integer = -1
            'Try
            '    nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
            '    m_sp_SOListMapper.Delete(objDomain)
            'Catch ex As Exception
            '    nResult = -1
            '    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
            '    If rethrow Then
            '        Throw
            '    End If

            'End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As sp_SOList)
            Try
                m_sp_SOListMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_SOList), "sp_SOListCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(sp_SOList), "sp_SOListCode", AggregateType.Count)

            Return CType(m_sp_SOListMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_sp_SOListMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_SOList), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim sp_SOListColl As ArrayList = m_sp_SOListMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return sp_SOListColl
        End Function

#End Region

    End Class

End Namespace

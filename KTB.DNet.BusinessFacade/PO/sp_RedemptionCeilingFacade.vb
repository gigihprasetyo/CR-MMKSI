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
    Public Class sp_RedemptionCeilingFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_sp_RedemptionCeilingMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_sp_RedemptionCeilingMapper = MapperFactory.GetInstance().GetMapper(GetType(sp_RedemptionCeiling).ToString)
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As sp_RedemptionCeiling
            Return CType(m_sp_RedemptionCeilingMapper.Retrieve(ID), sp_RedemptionCeiling)
        End Function

        Public Function Retrieve(ByVal CreditAccount As String, ByVal PaymentType As Byte) As sp_RedemptionCeiling
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_RedemptionCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(sp_RedemptionCeiling), "CreditAccount", MatchType.Exact, CreditAccount))
            criterias.opAnd(New Criteria(GetType(sp_RedemptionCeiling), "PaymentType", MatchType.Exact, CType(PaymentType, Byte)))

            Dim sp_RedemptionCeilingColl As ArrayList = m_sp_RedemptionCeilingMapper.RetrieveByCriteria(criterias)
            If (sp_RedemptionCeilingColl.Count > 0) Then
                Return CType(sp_RedemptionCeilingColl(0), sp_RedemptionCeiling)
            End If
            Return New sp_RedemptionCeiling
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_sp_RedemptionCeilingMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_sp_RedemptionCeilingMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_sp_RedemptionCeilingMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_RedemptionCeiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_RedemptionCeilingMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_RedemptionCeiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_RedemptionCeilingMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_RedemptionCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("sp_RedemptionCeilingCode")) Then
                sortColl.Add(New Sort(GetType(sp_RedemptionCeiling), "sp_RedemptionCeilingCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _sp_RedemptionCeiling As ArrayList = m_sp_RedemptionCeilingMapper.RetrieveByCriteria(criterias, sortColl)
            Return _sp_RedemptionCeiling
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_RedemptionCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sp_RedemptionCeilingColl As ArrayList = m_sp_RedemptionCeilingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_RedemptionCeilingColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim sp_RedemptionCeilingColl As ArrayList = m_sp_RedemptionCeilingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_RedemptionCeilingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_RedemptionCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(sp_RedemptionCeiling), columnName, matchOperator, columnValue))
            Dim sp_RedemptionCeilingColl As ArrayList = m_sp_RedemptionCeilingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_RedemptionCeilingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_RedemptionCeiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_RedemptionCeiling), columnName, matchOperator, columnValue))

            Return m_sp_RedemptionCeilingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_RedemptionCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_RedemptionCeiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_RedemptionCeilingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As sp_RedemptionCeiling) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_sp_RedemptionCeilingMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As sp_RedemptionCeiling) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_sp_RedemptionCeilingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As sp_RedemptionCeiling)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_sp_RedemptionCeilingMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As sp_RedemptionCeiling)
            Try
                m_sp_RedemptionCeilingMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_RedemptionCeiling), "sp_RedemptionCeilingCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(sp_RedemptionCeiling), "sp_RedemptionCeilingCode", AggregateType.Count)

            Return CType(m_sp_RedemptionCeilingMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function RetrieveFromSP(ByVal pCreditAccount As String, ByVal pPaymentType As Integer, ByVal pDate As Date) As ArrayList
            Dim SQL As String
            Dim SQLOpt As String = ""
            Dim SQLWhere As String
            Dim SQLOrder As String

            'If (Not IsNothing(criterias)) Then
            '    SQLOpt = criterias.ToString
            '    If (SQLOpt.EndsWith(" WHERE ")) Then
            '        SQLOpt = SQLOpt.Replace(" WHERE ", "")
            '    End If
            'End If
            'SQLWhere = SQLOpt
            'SQLWhere = SQLWhere.Replace("'", "''")
            'If (Not IsNothing(sorts)) Then
            '    For Each obj As Object In sorts
            '        Dim joinClauses As System.Collections.Specialized.StringCollection = CType(obj, Sort).GetJoinClause()
            '        For Each joinClause As String In joinClauses
            '            If (SQLOpt.IndexOf(joinClause) = -1) Then
            '                SQLOpt = SQLOpt.Insert(SQLOpt.IndexOf(" WHERE "), joinClause)
            '            End If
            '        Next
            '    Next
            '    SQLWhere = SQLOpt
            '    SQLWhere = SQLWhere.Replace("'", "''")
            '    SQLOrder = " ORDER BY " + CType(sorts, Object).ToString()
            '    If (SQLOrder.EndsWith(" ORDER BY ")) Then
            '        SQLOrder = SQLOrder.Replace(" ORDER BY ", "")
            '    End If
            '    'SQLOpt += " ORDER BY " + CType(sorts, Object).ToString()
            '    'If (SQLOpt.EndsWith(" ORDER BY ")) Then
            '    '    SQLOpt = SQLOpt.Replace(" ORDER BY ", "")
            '    'End If
            'End If



            SQL = "exec sp_RedemptionCeiling '" & pCreditAccount & "', " & pPaymenttype & ", '" & Format(pDate, "yyyy/MM/dd") & "'"

            Return m_sp_RedemptionCeilingMapper.RetrieveSP(SQL)
        End Function


#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_sp_RedemptionCeilingMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_RedemptionCeiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim sp_RedemptionCeilingColl As ArrayList = m_sp_RedemptionCeilingMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return sp_RedemptionCeilingColl
        End Function

#End Region

    End Class

End Namespace

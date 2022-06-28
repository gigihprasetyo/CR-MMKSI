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
    Public Class sp_CeilingFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_sp_CeilingMapper As IMapper
        Private m_V_POTotalDetailMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_V_POTotalDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(V_POTotalDetail).ToString)
            Me.m_sp_CeilingMapper = MapperFactory.GetInstance().GetMapper(GetType(sp_Ceiling).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrieveFromSP_(ByVal StartDate As Date, ByVal EndDate As Date, ByVal sCreditAccount As String, ByVal PaymentType As Integer) As ArrayList
            'Public Function RetrieveFromSP(ByVal StartDate As Date, ByVal EndDate As Date, Optional ByVal criterias As ICriteria = Nothing, Optional ByVal sorts As ICollection = Nothing) As ArrayList
            Dim SQL As String
            Dim SQLOpt As String = ""
            Dim SQLWhere As String
            Dim SQLOrder As String
            If sCreditAccount.Trim <> "" Then
                sCreditAccount = "''" & sCreditAccount.Replace(",", "'',''") & "''"
                SQL = "exec sp_Ceiling '" & Format(StartDate, "yyyy/MM/dd") & "', '" & Format(EndDate, "yyyy/MM/dd") & "', '" & sCreditAccount & "'," & PaymentType.ToString() & ""
            Else
                SQL = "exec sp_Ceiling '" & Format(StartDate, "yyyy/MM/dd") & "', '" & Format(EndDate, "yyyy/MM/dd") & "'," & PaymentType.ToString() & ""
            End If

            Return m_sp_CeilingMapper.RetrieveSP(SQL)
        End Function

        Public Function RetrieveFromSP(ByVal PC As ProductCategory, ByVal StartDate As Date, ByVal EndDate As Date, ByVal sCreditAccount As String, ByVal PaymentType As Integer) As ArrayList
            'Public Function RetrieveFromSP(ByVal StartDate As Date, ByVal EndDate As Date, Optional ByVal criterias As ICriteria = Nothing, Optional ByVal sorts As ICollection = Nothing) As ArrayList
            Dim SQL As String
            Dim SQLOpt As String = ""
            Dim SQLWhere As String
            Dim SQLOrder As String
            If sCreditAccount.Trim <> "" Then
                sCreditAccount = "''" & sCreditAccount.Replace(",", "'',''") & "''"
                SQL = "exec sp_Ceiling " & PC.ID.ToString() & ", '" & Format(StartDate, "yyyy/MM/dd") & "', '" & Format(EndDate, "yyyy/MM/dd") & "', '" & sCreditAccount & "'," & PaymentType.ToString() & ""
            Else
                SQL = "exec sp_Ceiling " & PC.ID.ToString() & ", '" & Format(StartDate, "yyyy/MM/dd") & "', '" & Format(EndDate, "yyyy/MM/dd") & "',''," & PaymentType.ToString() & ""
            End If

            Return m_sp_CeilingMapper.RetrieveSP(SQL)
        End Function

        Public Function RetrieveFromSPTOP(ByVal PC As ProductCategory, ByVal StartDate As Date, ByVal EndDate As Date, ByVal sCreditAccount As String, ByVal PaymentType As Integer) As ArrayList
            'Public Function RetrieveFromSP(ByVal StartDate As Date, ByVal EndDate As Date, Optional ByVal criterias As ICriteria = Nothing, Optional ByVal sorts As ICollection = Nothing) As ArrayList
            Dim SQL As String
            Dim SQLOpt As String = ""
            Dim SQLWhere As String
            Dim SQLOrder As String
            If sCreditAccount.Trim <> "" Then
                sCreditAccount = "''" & sCreditAccount.Replace(",", "'',''") & "''"
                SQL = "exec sp_CeilingTOP " & PC.ID.ToString() & ", '" & Format(StartDate, "yyyy/MM/dd") & "', '" & Format(EndDate, "yyyy/MM/dd") & "', '" & sCreditAccount & "'," & PaymentType.ToString() & ""
            Else
                SQL = "exec sp_CeilingTOP " & PC.ID.ToString() & ", '" & Format(StartDate, "yyyy/MM/dd") & "', '" & Format(EndDate, "yyyy/MM/dd") & "',''," & PaymentType.ToString() & ""
            End If

            Return m_sp_CeilingMapper.RetrieveSP(SQL)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As sp_Ceiling
            Return CType(m_sp_CeilingMapper.Retrieve(ID), sp_Ceiling)
        End Function

        Public Function Retrieve(ByVal sp_CeilingCode As String) As sp_Ceiling
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_Ceiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(sp_Ceiling), "sp_CeilingCode", MatchType.Exact, sp_CeilingCode))

            Dim sp_CeilingColl As ArrayList = m_sp_CeilingMapper.RetrieveByCriteria(criterias)
            If (sp_CeilingColl.Count > 0) Then
                Return CType(sp_CeilingColl(0), sp_Ceiling)
            End If
            Return New sp_Ceiling
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_sp_CeilingMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_sp_CeilingMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_sp_CeilingMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_Ceiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_CeilingMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_Ceiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_CeilingMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_Ceiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("sp_CeilingCode")) Then
                sortColl.Add(New Sort(GetType(sp_Ceiling), "sp_CeilingCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _sp_Ceiling As ArrayList = m_sp_CeilingMapper.RetrieveByCriteria(criterias, sortColl)
            Return _sp_Ceiling
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_Ceiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sp_CeilingColl As ArrayList = m_sp_CeilingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_CeilingColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim sp_CeilingColl As ArrayList = m_sp_CeilingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_CeilingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_Ceiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(sp_Ceiling), columnName, matchOperator, columnValue))
            Dim sp_CeilingColl As ArrayList = m_sp_CeilingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_CeilingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_Ceiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_Ceiling), columnName, matchOperator, columnValue))

            Return m_sp_CeilingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_Ceiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_Ceiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_CeilingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As sp_Ceiling) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_sp_CeilingMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As sp_Ceiling) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_sp_CeilingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As sp_Ceiling)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_sp_CeilingMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As sp_Ceiling)
            Try
                m_sp_CeilingMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_Ceiling), "sp_CeilingCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(sp_Ceiling), "sp_CeilingCode", AggregateType.Count)

            Return CType(m_sp_CeilingMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_sp_CeilingMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_Ceiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim sp_CeilingColl As ArrayList = m_sp_CeilingMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return sp_CeilingColl
        End Function

#End Region

    End Class

End Namespace

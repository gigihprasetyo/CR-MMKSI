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
    Public Class sp_MaxTOPDayFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_sp_MaxTOPDayMapper As IMapper
        Private m_V_POTotalDetailMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_V_POTotalDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(V_POTotalDetail).ToString)
            Me.m_sp_MaxTOPDayMapper = MapperFactory.GetInstance().GetMapper(GetType(sp_MaxTOPDay).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrieveFromSP(ByVal TransactionType As Integer _
            , ByVal PageIndex As Integer, ByVal PageSize As Integer, ByVal IsFactoring As Short _
            , ByVal NewNormal As Integer, ByVal NewFactoring As Integer _
            , ByVal DealerIDs As String, ByVal ProvinceIDs As String _
            , ByVal CategoryID As Integer, ByVal VechileTypeIDs As String _
            , ByVal MaxTOPDay As Integer _
            , ByVal TotalRow As Integer, ByVal ExecutedBy As String _
            ) As ArrayList
            Dim SQL As String = String.Empty

            SQL = "exec sp_MaxTOPDay " & TransactionType & "," & PageIndex & "," & PageSize & "," & IsFactoring _
                & "," & NewNormal.ToString & "," & NewFactoring.ToString _
                & ",'" & DealerIDs & "','" & ProvinceIDs & "'" _
                & "," & CategoryID & ",'" & VechileTypeIDs & "'," & MaxTOPDay & "," & TotalRow & " ,'" & ExecutedBy & "'"

            Return m_sp_MaxTOPDayMapper.RetrieveSP(SQL)
        End Function

        Public Function RetrieveFromSP(ByVal TransactionType As Integer _
            , ByVal PageIndex As Integer, ByVal PageSize As Integer, ByVal IsFactoring As Short _
            , ByVal NewNormal As Integer, ByVal NewFactoring As Integer _
            , ByVal DealerIDs As String, ByVal ProvinceIDs As String _
            , ByVal CategoryID As Integer, ByVal VechileTypeIDs As String _
            , ByVal MaxTOPDay As Integer _
            , ByVal TotalRow As Integer, ByVal ExecutedBy As String, IsCOD As Integer
            ) As ArrayList
            Dim SQL As String = String.Empty

            SQL = "exec sp_MaxTOPDay " & TransactionType & "," & PageIndex & "," & PageSize & "," & IsFactoring _
                & "," & NewNormal.ToString & "," & NewFactoring.ToString _
                & ",'" & DealerIDs & "','" & ProvinceIDs & "'" _
                & "," & CategoryID & ",'" & VechileTypeIDs & "'," & MaxTOPDay & "," & TotalRow & " ,'" & ExecutedBy & "'," & IsCOD.ToString()

            Return m_sp_MaxTOPDayMapper.RetrieveSP(SQL)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As sp_MaxTOPDay
            Return CType(m_sp_MaxTOPDayMapper.Retrieve(ID), sp_MaxTOPDay)
        End Function

        Public Function Retrieve(ByVal sp_MaxTOPDayCode As String) As sp_MaxTOPDay
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_MaxTOPDay), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(sp_MaxTOPDay), "sp_MaxTOPDayCode", MatchType.Exact, sp_MaxTOPDayCode))

            Dim sp_MaxTOPDayColl As ArrayList = m_sp_MaxTOPDayMapper.RetrieveByCriteria(criterias)
            If (sp_MaxTOPDayColl.Count > 0) Then
                Return CType(sp_MaxTOPDayColl(0), sp_MaxTOPDay)
            End If
            Return New sp_MaxTOPDay
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_sp_MaxTOPDayMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_sp_MaxTOPDayMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_sp_MaxTOPDayMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_MaxTOPDay), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_MaxTOPDayMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_MaxTOPDay), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_MaxTOPDayMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_MaxTOPDay), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("sp_MaxTOPDayCode")) Then
                sortColl.Add(New Sort(GetType(sp_MaxTOPDay), "sp_MaxTOPDayCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _sp_MaxTOPDay As ArrayList = m_sp_MaxTOPDayMapper.RetrieveByCriteria(criterias, sortColl)
            Return _sp_MaxTOPDay
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_MaxTOPDay), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sp_MaxTOPDayColl As ArrayList = m_sp_MaxTOPDayMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_MaxTOPDayColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim sp_MaxTOPDayColl As ArrayList = m_sp_MaxTOPDayMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_MaxTOPDayColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_MaxTOPDay), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(sp_MaxTOPDay), columnName, matchOperator, columnValue))
            Dim sp_MaxTOPDayColl As ArrayList = m_sp_MaxTOPDayMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_MaxTOPDayColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_MaxTOPDay), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_MaxTOPDay), columnName, matchOperator, columnValue))

            Return m_sp_MaxTOPDayMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_MaxTOPDay), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_MaxTOPDay), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_MaxTOPDayMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As sp_MaxTOPDay) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_sp_MaxTOPDayMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As sp_MaxTOPDay) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_sp_MaxTOPDayMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As sp_MaxTOPDay)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_sp_MaxTOPDayMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As sp_MaxTOPDay)
            Try
                m_sp_MaxTOPDayMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_MaxTOPDay), "sp_MaxTOPDayCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(sp_MaxTOPDay), "sp_MaxTOPDayCode", AggregateType.Count)

            Return CType(m_sp_MaxTOPDayMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_sp_MaxTOPDayMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_MaxTOPDay), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim sp_MaxTOPDayColl As ArrayList = m_sp_MaxTOPDayMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return sp_MaxTOPDayColl
        End Function

#End Region

    End Class

End Namespace

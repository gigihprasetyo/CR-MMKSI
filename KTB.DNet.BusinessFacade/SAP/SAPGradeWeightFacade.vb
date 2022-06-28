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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 9/5/2007 - 4:20:56 PM
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
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Public Class SAPGradeWeightFacade
    Inherits AbstractFacade

#Region "Private Variables"

    Private m_userPrincipal As IPrincipal = Nothing
    Private m_SAPGradeWeightMapper As IMapper

    Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

    Public Sub New(ByVal userPrincipal As IPrincipal)

        Me.m_userPrincipal = userPrincipal
        Me.m_SAPGradeWeightMapper = MapperFactory.GetInstance.GetMapper(GetType(SAPGradeWeight).ToString)


    End Sub

#End Region

#Region "Retrieve"

    Public Function Retrieve(ByVal ID As Integer) As SAPGradeWeight
        Return CType(m_SAPGradeWeightMapper.Retrieve(ID), SAPGradeWeight)
    End Function

    Public Function Retrieve(ByVal Code As String) As SAPGradeWeight
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPGradeWeight), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SAPGradeWeight), "Code", MatchType.Exact, Code))

        Dim SAPGradeWeightColl As ArrayList = m_SAPGradeWeightMapper.RetrieveByCriteria(criterias)
        If (SAPGradeWeightColl.Count > 0) Then
            Return CType(SAPGradeWeightColl(0), SAPGradeWeight)
        End If
        Return New SAPGradeWeight
    End Function

    Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
        Return m_SAPGradeWeightMapper.RetrieveByCriteria(criterias)
    End Function

    Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
        Return m_SAPGradeWeightMapper.RetrieveByCriteria(criterias, sorts)
    End Function

    Public Function RetrieveList() As ArrayList
        Return m_SAPGradeWeightMapper.RetrieveList
    End Function

    Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
        Dim sortColl As SortCollection = New SortCollection

        If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
            sortColl.Add(New Sort(GetType(SAPGradeWeight), sortColumn, sortDirection))
        Else
            sortColl = Nothing
        End If

        Return m_SAPGradeWeightMapper.RetrieveList(sortColl)
    End Function

    Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
        Dim sortColl As SortCollection = New SortCollection

        If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
            sortColl.Add(New Sort(GetType(SAPGradeWeight), sortColumn, sortDirection))
        Else
            sortColl = Nothing
        End If

        Return m_SAPGradeWeightMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

    End Function

    Public Function RetrieveActiveList() As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPGradeWeight), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim _SAPGradeWeight As ArrayList = m_SAPGradeWeightMapper.RetrieveByCriteria(criterias)
        Return _SAPGradeWeight
    End Function

    Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPGradeWeight), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim SAPGradeWeightColl As ArrayList = m_SAPGradeWeightMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

        Return SAPGradeWeightColl
    End Function

    Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
        Dim SAPGradeWeightColl As ArrayList = m_SAPGradeWeightMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
        Return SAPGradeWeightColl
    End Function

    Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPGradeWeight), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim SAPGradeWeightColl As ArrayList = m_SAPGradeWeightMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
        criterias.opAnd(New Criteria(GetType(SAPGradeWeight), columnName, matchOperator, columnValue))
        Return SAPGradeWeightColl
    End Function

    Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
        Dim sortColl As SortCollection = New SortCollection

        If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
            sortColl.Add(New Sort(GetType(SAPGradeWeight), sortColumn, sortDirection))
        Else
            sortColl = Nothing
        End If

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPGradeWeight), columnName, matchOperator, columnValue))

        Return m_SAPGradeWeightMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
    End Function

#End Region

#Region "Transaction/Other Public Method"

    Public Function ValidateCode(ByVal Code As String) As Integer
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPGradeWeight), "SAPGradeWeightCode", MatchType.Exact, Code))
        Dim agg As Aggregate = New Aggregate(GetType(SAPGradeWeight), "SAPGradeWeightCode", AggregateType.Count)
        Return CType(m_SAPGradeWeightMapper.RetrieveScalar(agg, crit), Integer)
    End Function

    Public Function ValidateCode(ByVal Code As String, ByVal tipeEnum As Integer) As Integer
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPGradeWeight), "Code", MatchType.Exact, Code))
        crit.opAnd(New Criteria(GetType(SAPGradeWeight), "Type", MatchType.Exact, tipeEnum))
        Dim agg As Aggregate = New Aggregate(GetType(SAPGradeWeight), "Code", AggregateType.Count)
        Return CType(m_SAPGradeWeightMapper.RetrieveScalar(agg, crit), Integer)
    End Function

    Public Function CountBobotEdit(ByVal tipeEnum As Integer, ByVal editedCode As String) As Integer
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPGradeWeight), "Type", MatchType.Exact, tipeEnum))
        crit.opAnd(New Criteria(GetType(SAPGradeWeight), "Code", MatchType.No, editedCode))
        Dim agg As Aggregate = New Aggregate(GetType(SAPGradeWeight), "Bobot", AggregateType.Sum)
        Return CType(m_SAPGradeWeightMapper.RetrieveScalar(agg, crit), Integer)
    End Function

    Public Function Insert(ByVal objDomain As SAPGradeWeight) As Integer
        Dim iReturn As Integer = -1
        Try
            m_SAPGradeWeightMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            iReturn = 1
        Catch ex As Exception
            Dim s As String = ex.Message
            iReturn = -1
        End Try
        Return iReturn
    End Function

    Public Function DeleteDB(ByVal objDomain As SAPGradeWeight) As Integer
        Dim iReturn As Integer = -1
        Try
            m_SAPGradeWeightMapper.Delete(objDomain)
            iReturn = 1
        Catch ex As Exception
            Dim s As String = ex.Message
            iReturn = -1
        End Try
        Return iReturn
    End Function

    Public Function Update(ByVal objDomain As SAPGradeWeight) As Integer
        Dim iReturn As Integer = -1
        Try
            m_SAPGradeWeightMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            iReturn = 1
        Catch ex As Exception
            Dim s As String = ex.Message
            iReturn = -1
        End Try
        Return iReturn
    End Function
#End Region

#Region "Custom Method"

#End Region

End Class

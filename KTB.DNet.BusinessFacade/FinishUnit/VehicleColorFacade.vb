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
'// Author Name   : Agus Pirnadi
'// PURPOSE       : 
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

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class VechileColorFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_VechileColorMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_VechileColorMapper = MapperFactory.GetInstance().GetMapper(GetType(VechileColor).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As VechileColor
            Return CType(m_VechileColorMapper.Retrieve(ID), VechileColor)
        End Function

        Public Function Retrieve(ByVal nTypeID As Integer, ByVal sCode As String) As VechileColor
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "VechileType.ID", MatchType.Exact, nTypeID))
            crit.opAnd(New Criteria(GetType(VechileColor), "ColorCode", MatchType.Exact, sCode))

            Dim VechileColorColl As ArrayList = m_VechileColorMapper.RetrieveByCriteria(crit)
            If (VechileColorColl.Count > 0) Then
                Return CType(VechileColorColl(0), VechileColor)
            End If
            Return New VechileColor
        End Function

        Public Function Retrieve(ByVal Code As String) As VechileColor
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileColor), "ColorCode", MatchType.Exact, Code))

            Dim VechileColorColl As ArrayList = m_VechileColorMapper.RetrieveByCriteria(criterias)
            If (VechileColorColl.Count > 0) Then
                Return CType(VechileColorColl(0), VechileColor)
            End If
            Return New VechileColor
        End Function
        'cr sfid
        Public Function RetrieveByName(ByVal Code As String) As VechileColor
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileColor), "ColorCode", MatchType.Exact, Code))

            Dim VechileColorColl As ArrayList = m_VechileColorMapper.RetrieveByCriteria(criterias)
            If (VechileColorColl.Count > 0) Then
                Return CType(VechileColorColl(2), VechileColor)
            End If
            Return New VechileColor
        End Function
        '

        Public Function RetrieveByMaterialNumber(ByVal MaterialNumber As String) As VechileColor
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileColor), "MaterialNumber", MatchType.Exact, MaterialNumber))

            Dim VechileColorColl As ArrayList = m_VechileColorMapper.RetrieveByCriteria(criterias)
            If (VechileColorColl.Count > 0) Then
                Return CType(VechileColorColl(0), VechileColor)
            End If
            Return New VechileColor
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_VechileColorMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_VechileColorMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_VechileColorMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VechileColor), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VechileColorMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VechileColor), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VechileColorMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _VechileColor As ArrayList = m_VechileColorMapper.RetrieveByCriteria(criterias)
            Return _VechileColor
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim VechileColorColl As ArrayList = m_VechileColorMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return VechileColorColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim VechileColorColl As ArrayList = m_VechileColorMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return VechileColorColl
        End Function
        'CR SFID
        Public Function RetrieveByCategory(ByVal Category As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType", MatchType.Exact, Category))

            Dim VechileColorColl As ArrayList = m_VechileColorMapper.RetrieveByCriteria(criterias)
            Return VechileColorColl
        End Function
        Public Function RetrieveByCategoryView(ByVal Category As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileColor), "ID", MatchType.Exact, Category))

            Dim VechileColorColl As ArrayList = m_VechileColorMapper.RetrieveByCriteria(criterias)
            Return VechileColorColl
        End Function
        '

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileColor), columnName, matchOperator, columnValue))
            Dim VechileColorColl As ArrayList = m_VechileColorMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return VechileColorColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VechileColor), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), columnName, matchOperator, columnValue))

            Return m_VechileColorMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As VechileColor) As Integer
            Dim iReturn As Integer = -2
            Try
                m_VechileColorMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As VechileColor) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_VechileColorMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As VechileColor)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_VechileColorMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As VechileColor)
            Try
                m_VechileColorMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal nTypeID As Integer, ByVal sCode As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "VechileType.ID", MatchType.Exact, nTypeID))
            crit.opAnd(New Criteria(GetType(VechileColor), "ColorCode", MatchType.Exact, sCode))

            Dim agg As Aggregate = New Aggregate(GetType(VechileColor), "ColorCode", AggregateType.Count)

            Return CType(m_VechileColorMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveMaterial(ByVal MaterialNumber As String) As VechileColor
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileColor), "MaterialNumber", MatchType.Exact, MaterialNumber))

            Dim VechileColorColl As ArrayList = m_VechileColorMapper.RetrieveByCriteria(criterias)
            If (VechileColorColl.Count > 0) Then
                Return CType(VechileColorColl(0), VechileColor)
            End If
            Return New VechileColor
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Return m_VechileColorMapper.RetrieveByCriteria(criterias, sorts, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_VechileColorMapper.RetrieveByCriteria(criterias, sorts)
        End Function

#End Region

    End Class
End Namespace

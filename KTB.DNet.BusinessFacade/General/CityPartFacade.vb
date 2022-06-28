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

Namespace KTB.DNet.BusinessFacade.General
    Public Class CityPartFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_CityPartMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_CityPartMapper = MapperFactory.GetInstance().GetMapper(GetType(CityPart).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As CityPart
            Return CType(m_CityPartMapper.Retrieve(ID), CityPart)
        End Function

        Public Function Retrieve(ByVal CityId As Integer, ByVal decoy As Boolean) As CityPart
            Dim objReturnValue As CityPart
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CityPart), "City.ID", MatchType.Exact, CityId))
            criterias.opAnd(New Criteria(GetType(CityPart), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CityList As ArrayList
            CityList = m_CityPartMapper.RetrieveByCriteria(criterias)
            If Not CityList Is Nothing Then
                If CityList.Count > 0 Then
                    objReturnValue = CType(CityList.Item(0), CityPart)
                End If
            End If
            Return objReturnValue
        End Function

        Public Function Retrieve(ByVal CityName As String, ByVal x As Integer) As CityPart
            Dim objReturnValue As CityPart
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CityPart), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CityPart), "CityName", MatchType.Exact, CityName))
            Dim CityList As ArrayList
            CityList = m_CityPartMapper.RetrieveByCriteria(criterias)
            If Not CityList Is Nothing Then
                If CityList.Count > 0 Then
                    objReturnValue = CType(CityList.Item(0), CityPart)
                End If
            End If
            Return objReturnValue
        End Function

        Public Function Retrieve(ByVal CityCode As String) As CityPart
            Dim objReturnValue As CityPart
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CityPart), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CityPart), "CityCode", MatchType.Exact, CityCode))
            Dim CityList As ArrayList
            CityList = m_CityPartMapper.RetrieveByCriteria(criterias)
            If Not CityList Is Nothing Then
                If CityList.Count > 0 Then
                    objReturnValue = CType(CityList.Item(0), CityPart)
                End If
            End If
            Return objReturnValue
        End Function

        Public Function IsCityFound(ByVal strCityCode As String) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CityPart), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim bResult As Boolean = False
            criterias.opAnd(New Criteria(GetType(CityPart), "CityCode", MatchType.Exact, strCityCode))
            Dim CityColl As ArrayList = m_CityPartMapper.RetrieveByCriteria(criterias)
            If (CityColl.Count > 0) Then
                bResult = True
            Else
            End If
            Return bResult

        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_CityPartMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_CityPartMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_CityPartMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CityPart), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CityPartMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal crit As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CityPart), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CityPartMapper.RetrieveByCriteria(crit, sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CityPart), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CityPartMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CityPart), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _City As ArrayList = m_CityPartMapper.RetrieveByCriteria(criterias)
            Return _City
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CityPart), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CityColl As ArrayList = m_CityPartMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CityColl
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CityPart), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim CityColl As ArrayList = m_CityPartMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return CityColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
          ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CityPart), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_CityPartMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
               ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CityPart), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CityPart), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CityPartMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim CityColl As ArrayList = m_CityPartMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CityColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
               ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CityPart), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CityPartMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            'Dim CityColl As ArrayList = m_CityPartMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            'Return CityColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CityPart), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CityPart), columnName, matchOperator, columnValue))
            Dim CityColl As ArrayList = m_CityPartMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CityColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CityPart), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CityPart), columnName, matchOperator, columnValue))

            Return m_CityPartMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As CityPart) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_CityPartMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As CityPart) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_CityPartMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As CityPart)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_CityPartMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As CityPart) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_CityPartMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CityPart), "CityCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(CityPart), "CityCode", AggregateType.Count)

            Return CType(m_CityPartMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

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

Imports KTB.DNET.BusinessFacade
Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data.SqlClient

#End Region

Namespace KTB.DNet.BusinessFacade.General
    Public Class V_LogisticPriceAndNationalityFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private mV_LogisticPriceAndNationalityMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.mV_LogisticPriceAndNationalityMapper = MapperFactory.GetInstance().GetMapper(GetType(V_LogisticPriceAndNationality).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
        End Sub

#End Region

#Region "Retrieve"



        Public Function Retrieve(ByVal ID As Integer) As V_LogisticPriceAndNationality
            Return CType(mV_LogisticPriceAndNationalityMapper.Retrieve(ID), V_LogisticPriceAndNationality)
        End Function

        Public Function Retrieve(ByVal Code As String) As V_LogisticPriceAndNationality
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_LogisticPriceAndNationality), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(V_LogisticPriceAndNationality), "ProvinceCode", MatchType.Exact, Code))

            Dim V_LogisticPriceAndNationalityColl As ArrayList = mV_LogisticPriceAndNationalityMapper.RetrieveByCriteria(criterias)
            If (V_LogisticPriceAndNationalityColl.Count > 0) Then
                Return CType(V_LogisticPriceAndNationalityColl(0), V_LogisticPriceAndNationality)
            End If
            Return New V_LogisticPriceAndNationality
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return mV_LogisticPriceAndNationalityMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return mV_LogisticPriceAndNationalityMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return mV_LogisticPriceAndNationalityMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LogisticPriceAndNationality), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return mV_LogisticPriceAndNationalityMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LogisticPriceAndNationality), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return mV_LogisticPriceAndNationalityMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_LogisticPriceAndNationality), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim V_LogisticPriceAndNationalityColl As ArrayList = mV_LogisticPriceAndNationalityMapper.RetrieveByCriteria(criterias)
            Return V_LogisticPriceAndNationalityColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_LogisticPriceAndNationality), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim V_LogisticPriceAndNationalityColl As ArrayList = mV_LogisticPriceAndNationalityMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return V_LogisticPriceAndNationalityColl
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Province), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim V_LogisticPriceAndNationalityColl As ArrayList = mV_LogisticPriceAndNationalityMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return V_LogisticPriceAndNationalityColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LogisticPriceAndNationality), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim V_LogisticPriceAndNationalityColl As ArrayList = mV_LogisticPriceAndNationalityMapper.RetrieveByCriteria(Criterias, sortColl)
            Return V_LogisticPriceAndNationalityColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_LogisticPriceAndNationality), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LogisticPriceAndNationality), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return mV_LogisticPriceAndNationalityMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim V_LogisticPriceAndNationalityColl As ArrayList = mV_LogisticPriceAndNationalityMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return V_LogisticPriceAndNationalityColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return mV_LogisticPriceAndNationalityMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_LogisticPriceAndNationality), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(V_LogisticPriceAndNationality), columnName, matchOperator, columnValue))
            Dim V_LogisticPriceAndNationalityColl As ArrayList = mV_LogisticPriceAndNationalityMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return V_LogisticPriceAndNationalityColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LogisticPriceAndNationality), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_LogisticPriceAndNationality), columnName, matchOperator, columnValue))

            Return mV_LogisticPriceAndNationalityMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Sub InsertWithTransactionManagerHandler(ByVal sender As Object, ByVal args As TransactionManager.OnInsertArgs)

            ' set the object ID from db returned id
            If (args.DomainObject.GetType = GetType(V_LogisticPriceAndNationality)) Then
                CType(args.DomainObject, V_LogisticPriceAndNationality).ID = args.ID
                CType(args.DomainObject, V_LogisticPriceAndNationality).MarkLoaded()
            ElseIf (args.DomainObject.GetType = GetType(City)) Then
                CType(args.DomainObject, City).ID = args.ID
                CType(args.DomainObject, City).MarkLoaded()
            End If

        End Sub

        Public Function Insert(ByVal objDomain As V_LogisticPriceAndNationality) As Integer
            Dim iReturn As Integer = -2
            Try
                mV_LogisticPriceAndNationalityMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As V_LogisticPriceAndNationality) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = mV_LogisticPriceAndNationalityMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As V_LogisticPriceAndNationality)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                mV_LogisticPriceAndNationalityMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As V_LogisticPriceAndNationality)
            Try
                mV_LogisticPriceAndNationalityMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_LogisticPriceAndNationality), "ProvinceCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(V_LogisticPriceAndNationality), "ProvinceCode", AggregateType.Count)

            Return CType(mV_LogisticPriceAndNationalityMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"
#End Region

    End Class

End Namespace

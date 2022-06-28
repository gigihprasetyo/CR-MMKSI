
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
'// Generated on 22/01/2019 - 10:04:39
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
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class SubCategoryVehicleToModelFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SubCategoryVehicleToModelMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SubCategoryVehicleToModelMapper = MapperFactory.GetInstance.GetMapper(GetType(SubCategoryVehicleToModel).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SubCategoryVehicleToModel
            Return CType(m_SubCategoryVehicleToModelMapper.Retrieve(ID), SubCategoryVehicleToModel)
        End Function

        Public Function Retrieve(ByVal Code As String) As SubCategoryVehicleToModel
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "SubCategoryVehicleToModelCode", MatchType.Exact, Code))

            Dim SubCategoryVehicleToModelColl As ArrayList = m_SubCategoryVehicleToModelMapper.RetrieveByCriteria(criterias)
            If (SubCategoryVehicleToModelColl.Count > 0) Then
                Return CType(SubCategoryVehicleToModelColl(0), SubCategoryVehicleToModel)
            End If
            Return New SubCategoryVehicleToModel
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SubCategoryVehicleToModelMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SubCategoryVehicleToModelMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SubCategoryVehicleToModelMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SubCategoryVehicleToModel), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SubCategoryVehicleToModelMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SubCategoryVehicleToModel), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SubCategoryVehicleToModelMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SubCategoryVehicleToModel As ArrayList = m_SubCategoryVehicleToModelMapper.RetrieveByCriteria(criterias)
            Return _SubCategoryVehicleToModel
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SubCategoryVehicleToModelColl As ArrayList = m_SubCategoryVehicleToModelMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SubCategoryVehicleToModelColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SubCategoryVehicleToModel), SortColumn, sortDirection))
            Dim SubCategoryVehicleToModelColl As ArrayList = m_SubCategoryVehicleToModelMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SubCategoryVehicleToModelColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SubCategoryVehicleToModelColl As ArrayList = m_SubCategoryVehicleToModelMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SubCategoryVehicleToModelColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SubCategoryVehicleToModelColl As ArrayList = m_SubCategoryVehicleToModelMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), columnName, matchOperator, columnValue))
            Return SubCategoryVehicleToModelColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SubCategoryVehicleToModel), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), columnName, matchOperator, columnValue))

            Return m_SubCategoryVehicleToModelMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "SubCategoryVehicleToModelCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SubCategoryVehicleToModel), "SubCategoryVehicleToModelCode", AggregateType.Count)
            Return CType(m_SubCategoryVehicleToModelMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SubCategoryVehicleToModel) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SubCategoryVehicleToModelMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SubCategoryVehicleToModel) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SubCategoryVehicleToModelMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SubCategoryVehicleToModel)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SubCategoryVehicleToModelMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SubCategoryVehicleToModel)
            Try
                m_SubCategoryVehicleToModelMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace


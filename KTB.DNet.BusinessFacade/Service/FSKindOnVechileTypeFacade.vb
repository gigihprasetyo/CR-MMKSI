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

Namespace KTB.DNet.BusinessFacade.Service
    Public Class FSKindOnVechileTypeFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_FSKindOnVechileTypeMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_FSKindOnVechileTypeMapper = MapperFactory.GetInstance().GetMapper(GetType(FSKindOnVechileType).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As FSKindOnVechileType
            Return CType(m_FSKindOnVechileTypeMapper.Retrieve(ID), FSKindOnVechileType)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_FSKindOnVechileTypeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_FSKindOnVechileTypeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_FSKindOnVechileTypeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSKindOnVechileType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FSKindOnVechileTypeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSKindOnVechileType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FSKindOnVechileTypeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _FSKindOnVechileType As ArrayList = m_FSKindOnVechileTypeMapper.RetrieveByCriteria(criterias)
            Return _FSKindOnVechileType
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FSKindOnVechileTypeColl As ArrayList = m_FSKindOnVechileTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FSKindOnVechileTypeColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
       ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSKindOnVechileType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FSKindOnVechileTypeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSKindOnVechileType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim FSKindOnVechileTypeColl As ArrayList = m_FSKindOnVechileTypeMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return FSKindOnVechileTypeColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim FSKindOnVechileTypeColl As ArrayList = m_FSKindOnVechileTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FSKindOnVechileTypeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FSKindOnVechileType), columnName, matchOperator, columnValue))
            Dim FSKindOnVechileTypeColl As ArrayList = m_FSKindOnVechileTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FSKindOnVechileTypeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSKindOnVechileType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), columnName, matchOperator, columnValue))

            Return m_FSKindOnVechileTypeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        'Add 14-09-2018
        Public Function RetrieveFromSP(ByVal strChassisNumber As String) As DataSet
            Dim _SQL As String = "EXEC [up_RetrieveFSTypeFromChassisMaster]"
            _SQL += " @ChassisNumber = '" & strChassisNumber & "'"

            Return m_FSKindOnVechileTypeMapper.RetrieveDataSet(_SQL)
        End Function

        Public Function RetrieveFromSP_MSP(ByVal strChassisNumber As String) As DataSet
            Dim _SQL As String = "EXEC [up_RetrieveFSTypeFromChassisMaster_MSP]"
            _SQL += " @ChassisNumber = '" & strChassisNumber & "'"

            Return m_FSKindOnVechileTypeMapper.RetrieveDataSet(_SQL)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As FSKindOnVechileType) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_FSKindOnVechileTypeMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As FSKindOnVechileType) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_FSKindOnVechileTypeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As FSKindOnVechileType) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_FSKindOnVechileTypeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                nResult = 1
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As FSKindOnVechileType) As Integer
            Dim nResult As Integer = 1
            Try
                m_FSKindOnVechileTypeMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function ValidateCode(ByVal KindID As Integer, ByVal VechileTypeID As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "FSKind.ID", MatchType.Exact, KindID))
            crit.opAnd(New Criteria(GetType(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, VechileTypeID))
            Dim agg As Aggregate = New Aggregate(GetType(FSKindOnVechileType), "ID", AggregateType.Count)
            Return CType(m_FSKindOnVechileTypeMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

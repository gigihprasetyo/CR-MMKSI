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
'// Generated on 7/16/2007 - 2:31:06 PM
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
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.Salesman

    Public Class SalesmanUnifGuideFacade

        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SalesmanUnifGuideMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SalesmanUnifGuideMapper = MapperFactory.GetInstance.GetMapper(GetType(SalesmanUnifGuide).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SalesmanUnifGuide
            Return CType(m_SalesmanUnifGuideMapper.Retrieve(ID), SalesmanUnifGuide)
        End Function

        'Public Function Retrieve(ByVal Code As String) As SalesmanUnifGuide
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUnifGuide), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(SalesmanUnifGuide), "AreaCode", MatchType.Exact, Code))

        '    Dim SalesmanUnifGuideColl As ArrayList = m_SalesmanUnifGuideMapper.RetrieveByCriteria(criterias)
        '    If (SalesmanUnifGuideColl.Count > 0) Then
        '        Return CType(SalesmanUnifGuideColl(0), SalesmanUnifGuide)
        '    End If
        '    Return New SalesmanUnifGuide
        'End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SalesmanUnifGuideMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SalesmanUnifGuideMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SalesmanUnifGuideMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanUnifGuide), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesmanUnifGuideMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanUnifGuide), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesmanUnifGuideMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUnifGuide), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SalesmanUnifGuide As ArrayList = m_SalesmanUnifGuideMapper.RetrieveByCriteria(criterias)
            Return _SalesmanUnifGuide
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUnifGuide), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesmanUnifGuideColl As ArrayList = m_SalesmanUnifGuideMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SalesmanUnifGuideColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(SalesmanUnifGuide), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim SalesmanUnifGuideColl As ArrayList = m_SalesmanUnifGuideMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SalesmanUnifGuideColl


            'Dim SalesmanUnifGuideColl As ArrayList = m_SalesmanUnifGuideMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            'Return SalesmanUnifGuideColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUnifGuide), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesmanUnifGuideColl As ArrayList = m_SalesmanUnifGuideMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SalesmanUnifGuide), columnName, matchOperator, columnValue))
            Return SalesmanUnifGuideColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanUnifGuide), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUnifGuide), columnName, matchOperator, columnValue))

            Return m_SalesmanUnifGuideMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"





#End Region

#Region "Need To Add"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUnifGuide), "AreaCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SalesmanUnifGuide), "AreaCode", AggregateType.Count)
            Return CType(m_SalesmanUnifGuideMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SalesmanUnifGuide) As Integer
            Dim iReturn As Integer = -2
            Try
                m_SalesmanUnifGuideMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As SalesmanUnifGuide) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SalesmanUnifGuideMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SalesmanUnifGuide)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SalesmanUnifGuideMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As SalesmanUnifGuide) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_SalesmanUnifGuideMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function
#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace


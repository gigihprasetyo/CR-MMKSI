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
'// Generated on 8/14/2007 - 2:31:36 PM
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

Namespace KTB.DNet.BusinessFacade.DealerReport

    Public Class CompetitorTypeFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_CompetitorTypeMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_CompetitorTypeMapper = MapperFactory.GetInstance.GetMapper(GetType(CompetitorType).ToString)
            m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As CompetitorType
            Return CType(m_CompetitorTypeMapper.Retrieve(ID), CompetitorType)
        End Function

        Public Function Retrieve(ByVal Code As String) As CompetitorType
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CompetitorType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CompetitorType), "Code", MatchType.Exact, Code))

            Dim CompetitorTypeColl As ArrayList = m_CompetitorTypeMapper.RetrieveByCriteria(criterias)
            If (CompetitorTypeColl.Count > 0) Then
                Return CType(CompetitorTypeColl(0), CompetitorType)
            End If
            Return New CompetitorType
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_CompetitorTypeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_CompetitorTypeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_CompetitorTypeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CompetitorType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CompetitorTypeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CompetitorType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CompetitorTypeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CompetitorType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _CompetitorType As ArrayList = m_CompetitorTypeMapper.RetrieveByCriteria(criterias)
            Return _CompetitorType
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CompetitorType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CompetitorTypeColl As ArrayList = m_CompetitorTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CompetitorTypeColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim CompetitorTypeColl As ArrayList = m_CompetitorTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return CompetitorTypeColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim CompetitorTypeColl As ArrayList = m_CompetitorTypeMapper.RetrieveByCriteria(criterias)
            Return CompetitorTypeColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(CompetitorType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim CompetitorBrandColl As ArrayList = m_CompetitorTypeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return CompetitorBrandColl

        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CompetitorType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CompetitorTypeColl As ArrayList = m_CompetitorTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(CompetitorType), columnName, matchOperator, columnValue))
            Return CompetitorTypeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CompetitorType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CompetitorType), columnName, matchOperator, columnValue))

            Return m_CompetitorTypeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CompetitorType), "CompetitorTypeCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(CompetitorType), "CompetitorTypeCode", AggregateType.Count)
            Return CType(m_CompetitorTypeMapper.RetrieveScalar(agg, crit), Integer)
        End Function
        Public Function Insert(ByVal objDomain As CompetitorType) As Integer
            Dim iReturn As Integer = 1
            Try
                m_CompetitorTypeMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As CompetitorType) As Integer
            Dim nResult As Integer = 1
            Try
                nResult = m_CompetitorTypeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function
#End Region

#Region "Custom Method"

        Public Function Retrieve(ByVal Code As String, ByVal merk As String) As CompetitorType
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CompetitorType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CompetitorType), "Code", MatchType.Exact, Code))
            criterias.opAnd(New Criteria(GetType(CompetitorType), "CompetitorBrand.Code", MatchType.Exact, merk))
            Dim CompetitorTypeColl As ArrayList = m_CompetitorTypeMapper.RetrieveByCriteria(criterias)
            If (CompetitorTypeColl.Count > 0) Then
                Return CType(CompetitorTypeColl(0), CompetitorType)
            End If
            Return New CompetitorType
        End Function

        Public Function Import(ByVal Coll As ArrayList)
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim compType As CompetitorType
                    For Each item As CompetitorType In Coll
                        compType = Me.Retrieve(item.Code, item.CompetitorBrand.Code)
                        If compType.ID > 0 Then
                            compType.Description = item.Description
                            m_TransactionManager.AddUpdate(compType, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        End If
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 1
                    End If
                Catch ex As Exception
                    returnValue = -1
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

#End Region

    End Class

End Namespace


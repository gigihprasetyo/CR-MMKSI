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
'// Generated on 8/2/2007 - 9:56:16 AM
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

Imports ktb.DNet.Domain
Imports ktb.DNet.Domain.Search
Imports ktb.DNet.DataMapper.Framework
Imports ktb.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region
Public Class MaterialPromotionStockAdjustmentFacade

#Region "Private Variables"

    Private m_userPrincipal As IPrincipal = Nothing
    Private m_MaterialPromotionStockAdjustmentMapper As IMapper

    Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

    Public Sub New(ByVal userPrincipal As IPrincipal)

        Me.m_userPrincipal = userPrincipal
        Me.m_MaterialPromotionStockAdjustmentMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNET.Domain.MaterialPromotionStockAdjustment).ToString)
        'Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.MaterialPromotionStockAdjustment))
        Me.m_TransactionManager = New TransactionManager
        AddHandler Me.m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
    End Sub

#End Region

#Region "Retrieve"

    Public Function Retrieve(ByVal ID As Integer) As KTB.DNET.Domain.MaterialPromotionStockAdjustment
        Return CType(m_MaterialPromotionStockAdjustmentMapper.Retrieve(ID), KTB.DNET.Domain.MaterialPromotionStockAdjustment)
    End Function

    'Public Function Retrieve(ByVal Code As String) As KTB.DNET.Domain.MaterialPromotionStockAdjustment
    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotionStockAdjustment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotionStockAdjustment), "GoodNo", MatchType.Exact, Code))

    '    Dim MaterialPromotionStockAdjustmentColl As ArrayList = m_MaterialPromotionStockAdjustmentMapper.RetrieveByCriteria(criterias)
    '    If (MaterialPromotionStockAdjustmentColl.Count > 0) Then
    '        Return CType(MaterialPromotionStockAdjustmentColl(0), KTB.DNET.Domain.MaterialPromotionStockAdjustment)
    '    End If
    '    Return New KTB.DNET.Domain.MaterialPromotionStockAdjustment
    'End Function

    Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
        Return m_MaterialPromotionStockAdjustmentMapper.RetrieveByCriteria(criterias)
    End Function

    Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
        Return m_MaterialPromotionStockAdjustmentMapper.RetrieveByCriteria(criterias, sorts)
    End Function

    Public Function RetrieveList() As ArrayList
        Return m_MaterialPromotionStockAdjustmentMapper.RetrieveList
    End Function

    Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
        Dim sortColl As SortCollection = New SortCollection

        If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
            sortColl.Add(New Sort(GetType(KTB.DNET.Domain.MaterialPromotionStockAdjustment), sortColumn, sortDirection))
        Else
            sortColl = Nothing
        End If

        Return m_MaterialPromotionStockAdjustmentMapper.RetrieveList(sortColl)
    End Function

    Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
        Dim sortColl As SortCollection = New SortCollection

        If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
            sortColl.Add(New Sort(GetType(KTB.DNET.Domain.MaterialPromotionStockAdjustment), sortColumn, sortDirection))
        Else
            sortColl = Nothing
        End If

        Return m_MaterialPromotionStockAdjustmentMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

    End Function

    Public Function RetrieveActiveList() As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotionStockAdjustment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim _MaterialPromotionStockAdjustment As ArrayList = m_MaterialPromotionStockAdjustmentMapper.RetrieveByCriteria(criterias)
        Return _MaterialPromotionStockAdjustment
    End Function

    Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotionStockAdjustment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim MaterialPromotionStockAdjustmentColl As ArrayList = m_MaterialPromotionStockAdjustmentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

        Return MaterialPromotionStockAdjustmentColl
    End Function

    Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal criterias As CriteriaComposite) As ArrayList
        Dim MaterialPromotionStockAdjustmentColl As ArrayList = m_MaterialPromotionStockAdjustmentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

        Return MaterialPromotionStockAdjustmentColl
    End Function

    Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Search.Sort(GetType(KTB.DNET.Domain.MaterialPromotionStockAdjustment), SortColumn, sortDirection))

        Dim MaterialPromotionColl As ArrayList = m_MaterialPromotionStockAdjustmentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

        Return MaterialPromotionColl
    End Function

    Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
        Dim MaterialPromotionStockAdjustmentColl As ArrayList = m_MaterialPromotionStockAdjustmentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
        Return MaterialPromotionStockAdjustmentColl
    End Function

    Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotionStockAdjustment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim MaterialPromotionStockAdjustmentColl As ArrayList = m_MaterialPromotionStockAdjustmentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
        criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotionStockAdjustment), columnName, matchOperator, columnValue))
        Return MaterialPromotionStockAdjustmentColl
    End Function

    Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
        Dim sortColl As SortCollection = New SortCollection

        If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
            sortColl.Add(New Sort(GetType(KTB.DNET.Domain.MaterialPromotionStockAdjustment), sortColumn, sortDirection))
        Else
            sortColl = Nothing
        End If

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotionStockAdjustment), columnName, matchOperator, columnValue))

        Return m_MaterialPromotionStockAdjustmentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
    End Function

#End Region

#Region "Transaction/Other Public Method"

    Public Function ValidateCode(ByVal Code As String) As Integer
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotionStockAdjustment), "GoodNo", MatchType.Exact, Code))
        Dim agg As Aggregate = New Aggregate(GetType(KTB.DNET.Domain.MaterialPromotionStockAdjustment), "GoodNo", AggregateType.Count)
        Return CType(m_MaterialPromotionStockAdjustmentMapper.RetrieveScalar(agg, crit), Integer)
    End Function
    Public Function Insert(ByVal objDomain As KTB.DNet.Domain.MaterialPromotionStockAdjustment) As Integer
        Dim returnValue As Short = 1
        Try
            m_MaterialPromotionStockAdjustmentMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
        Catch ex As Exception
            Dim s As String = ex.Message
            returnValue = -1
        End Try

        Return returnValue
    End Function

#End Region

#Region "Custom Method"
    Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

        If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.MaterialPromotionStockAdjustment) Then

            CType(InsertArg.DomainObject, KTB.DNET.Domain.MaterialPromotionStockAdjustment).ID = InsertArg.ID
            CType(InsertArg.DomainObject, KTB.DNET.Domain.MaterialPromotionStockAdjustment).MarkLoaded()


        End If

    End Sub
#End Region

End Class

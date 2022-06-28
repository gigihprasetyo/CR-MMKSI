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

Public Class MaterialPromotionPriceHistoryFacade

#Region "Private Variables"

    Private m_userPrincipal As IPrincipal = Nothing
    Private m_MaterialPromotionPriceHistoryMapper As IMapper

    Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

    Public Sub New(ByVal userPrincipal As IPrincipal)

        Me.m_userPrincipal = userPrincipal
        Me.m_MaterialPromotionPriceHistoryMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNET.Domain.MaterialPromotionPriceHistory).ToString)
        'Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.MaterialPromotionPriceHistory))
        Me.m_TransactionManager = New TransactionManager
        AddHandler Me.m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
    End Sub

#End Region

#Region "Retrieve"

    Public Function Retrieve(ByVal ID As Integer) As KTB.DNET.Domain.MaterialPromotionPriceHistory
        Return CType(m_MaterialPromotionPriceHistoryMapper.Retrieve(ID), KTB.DNET.Domain.MaterialPromotionPriceHistory)
    End Function

    'Public Function Retrieve(ByVal Code As String) As KTB.DNET.Domain.MaterialPromotionPriceHistory
    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotionPriceHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotionPriceHistory), "GoodNo", MatchType.Exact, Code))

    '    Dim MaterialPromotionPriceHistoryColl As ArrayList = m_MaterialPromotionPriceHistoryMapper.RetrieveByCriteria(criterias)
    '    If (MaterialPromotionPriceHistoryColl.Count > 0) Then
    '        Return CType(MaterialPromotionPriceHistoryColl(0), KTB.DNET.Domain.MaterialPromotionPriceHistory)
    '    End If
    '    Return New KTB.DNET.Domain.MaterialPromotionPriceHistory
    'End Function

    Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
        Return m_MaterialPromotionPriceHistoryMapper.RetrieveByCriteria(criterias)
    End Function

    Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
        Return m_MaterialPromotionPriceHistoryMapper.RetrieveByCriteria(criterias, sorts)
    End Function

    Public Function RetrieveList() As ArrayList
        Return m_MaterialPromotionPriceHistoryMapper.RetrieveList
    End Function

    Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
        Dim sortColl As SortCollection = New SortCollection

        If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
            sortColl.Add(New Sort(GetType(KTB.DNET.Domain.MaterialPromotionPriceHistory), sortColumn, sortDirection))
        Else
            sortColl = Nothing
        End If

        Return m_MaterialPromotionPriceHistoryMapper.RetrieveList(sortColl)
    End Function

    Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
        Dim sortColl As SortCollection = New SortCollection

        If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
            sortColl.Add(New Sort(GetType(KTB.DNET.Domain.MaterialPromotionPriceHistory), sortColumn, sortDirection))
        Else
            sortColl = Nothing
        End If

        Return m_MaterialPromotionPriceHistoryMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

    End Function

    Public Function RetrieveActiveList() As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotionPriceHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim _MaterialPromotionPriceHistory As ArrayList = m_MaterialPromotionPriceHistoryMapper.RetrieveByCriteria(criterias)
        Return _MaterialPromotionPriceHistory
    End Function

    Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotionPriceHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim MaterialPromotionPriceHistoryColl As ArrayList = m_MaterialPromotionPriceHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

        Return MaterialPromotionPriceHistoryColl
    End Function

    Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal criterias As CriteriaComposite) As ArrayList
        Dim MaterialPromotionPriceHistoryColl As ArrayList = m_MaterialPromotionPriceHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

        Return MaterialPromotionPriceHistoryColl
    End Function

    Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Search.Sort(GetType(KTB.DNET.Domain.MaterialPromotionPriceHistory), SortColumn, sortDirection))

        Dim MaterialPromotionColl As ArrayList = m_MaterialPromotionPriceHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

        Return MaterialPromotionColl
    End Function

    Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
        Dim MaterialPromotionPriceHistoryColl As ArrayList = m_MaterialPromotionPriceHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
        Return MaterialPromotionPriceHistoryColl
    End Function

    Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotionPriceHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim MaterialPromotionPriceHistoryColl As ArrayList = m_MaterialPromotionPriceHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
        criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotionPriceHistory), columnName, matchOperator, columnValue))
        Return MaterialPromotionPriceHistoryColl
    End Function

    Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
        Dim sortColl As SortCollection = New SortCollection

        If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
            sortColl.Add(New Sort(GetType(KTB.DNET.Domain.MaterialPromotionPriceHistory), sortColumn, sortDirection))
        Else
            sortColl = Nothing
        End If

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotionPriceHistory), columnName, matchOperator, columnValue))

        Return m_MaterialPromotionPriceHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
    End Function

#End Region

#Region "Transaction/Other Public Method"

    Public Function ValidateCode(ByVal Code As String) As Integer
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotionPriceHistory), "GoodNo", MatchType.Exact, Code))
        Dim agg As Aggregate = New Aggregate(GetType(KTB.DNET.Domain.MaterialPromotionPriceHistory), "GoodNo", AggregateType.Count)
        Return CType(m_MaterialPromotionPriceHistoryMapper.RetrieveScalar(agg, crit), Integer)
    End Function

#End Region

#Region "Custom Method"
    Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

        If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.MaterialPromotionPriceHistory) Then

            CType(InsertArg.DomainObject, KTB.DNET.Domain.MaterialPromotionPriceHistory).ID = InsertArg.ID
            CType(InsertArg.DomainObject, KTB.DNET.Domain.MaterialPromotionPriceHistory).MarkLoaded()


        End If

    End Sub
#End Region

End Class

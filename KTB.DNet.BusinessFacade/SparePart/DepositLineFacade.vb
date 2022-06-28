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

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class DepositLineFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_DepositLineMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_DepositLineMapper = MapperFactory.GetInstance().GetMapper(GetType(DepositLine).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(Deposit))
            Me.DomainTypeCollection.Add(GetType(DepositLine))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DepositLine
            Return CType(m_DepositLineMapper.Retrieve(ID), DepositLine)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DepositLineMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Integer
            Return CType(m_DepositLineMapper.RetrieveScalar(aggregation, criterias), Integer)
        End Function

        Public Function Retrieve(ByVal Code As String) As DepositLine
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositLine), "ClaimNumber", MatchType.Exact, Code))

            Dim DepositColl As ArrayList = m_DepositLineMapper.RetrieveByCriteria(criterias)
            If (DepositColl.Count > 0) Then
                Return CType(DepositColl(0), DepositLine)
            End If
            Return New DepositLine
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If Not IsNothing(sortColumn) AndAlso sortColumn <> "" Then
                sortColl.Add(New Sort(GetType(DepositLine), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositLineMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal colSortOrder As SortCollection) As ArrayList
            Return m_DepositLineMapper.RetrieveByCriteria(criterias, colSortOrder, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal colSortOrder As SortCollection) As ArrayList
            Return m_DepositLineMapper.RetrieveByCriteria(criterias, colSortOrder)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is Deposit) Then
                CType(InsertArg.DomainObject, Deposit).ID = InsertArg.ID
                CType(InsertArg.DomainObject, Deposit).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is DepositLine) Then
                CType(InsertArg.DomainObject, DepositLine).ID = InsertArg.ID
            End If
        End Sub

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As DepositLine
            Dim DepositColl As ArrayList = m_DepositLineMapper.RetrieveByCriteria(criterias, sorts)
            If DepositColl.Count > 0 Then
                Return CType(DepositColl(0), DepositLine)
            Else
                Return Nothing
            End If
        End Function

#End Region

    End Class

End Namespace

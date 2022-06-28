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
    Public Class WSCDetailBBFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_WSCDetailBBMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private objTransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_WSCDetailBBMapper = MapperFactory.GetInstance().GetMapper(GetType(WSCDetailBB).ToString)
            Me.objTransactionManager = New TransactionManager
            AddHandler objTransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(WSCHeaderBB))
            Me.DomainTypeCollection.Add(GetType(WSCDetailBB))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As WSCDetailBB
            Return CType(m_WSCDetailBBMapper.Retrieve(ID), WSCDetailBB)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_WSCDetailBBMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Integer
            Return CType(m_WSCDetailBBMapper.RetrieveScalar(aggregation, criterias), Integer)
        End Function

        Public Function Retrieve(ByVal Code As String) As WSCDetailBB
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCDetailBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(WSCDetailBB), "ClaimNumber", MatchType.Exact, Code))

            Dim WSCDetailBBColl As ArrayList = m_WSCDetailBBMapper.RetrieveByCriteria(criterias)
            If (WSCDetailBBColl.Count > 0) Then
                Return CType(WSCDetailBBColl(0), WSCDetailBB)
            End If
            Return New WSCDetailBB
        End Function

        Public Function RetrieveOpenWSC() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCDetailBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(WSCDetailBB), "ClaimStatus", MatchType.Exact, "Open"))

            Return m_WSCDetailBBMapper.RetrieveByCriteria(criterias)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is WSCHeaderBB) Then
                CType(InsertArg.DomainObject, WSCHeaderBB).ID = InsertArg.ID
                CType(InsertArg.DomainObject, WSCHeaderBB).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is WSCDetailBB) Then
                CType(InsertArg.DomainObject, WSCDetailBB).ID = InsertArg.ID
            End If
        End Sub

        Public Function Update(ByVal objDomain As WSCDetailBB) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_WSCDetailBBMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
            End Try
            Return nResult
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_WSCDetailBBMapper.RetrieveByCriteria(criterias, sorts)
        End Function


        Public Function ValidateCode(ByVal sLaborCode As String, ByVal sWorkCode As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCDetailBB), "Status", MatchType.Exact, 0))
            crit.opAnd(New Criteria(GetType(WSCDetailBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(WSCDetailBB), "WorkCode", MatchType.Exact, sWorkCode))
            crit.opAnd(New Criteria(GetType(WSCDetailBB), "PositionCode", MatchType.Exact, sLaborCode))

            Dim agg As Aggregate = New Aggregate(GetType(WSCDetailBB), "ID", AggregateType.Count)

            Dim count As Integer = -1
            Try
                count = CType(m_WSCDetailBBMapper.RetrieveScalar(agg, crit), Integer)
            Catch ex As Exception
                count = -1
            End Try
            Return count
        End Function
#End Region

    End Class
End Namespace

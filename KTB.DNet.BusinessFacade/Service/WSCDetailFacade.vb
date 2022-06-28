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
Imports System.Collections.Generic

#End Region

Namespace KTB.DNet.BusinessFacade.Service
    Public Class WSCDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_WSCDetailMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private objTransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_WSCDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(WSCDetail).ToString)
            Me.objTransactionManager = New TransactionManager
            AddHandler objTransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(WSCHeader))
            Me.DomainTypeCollection.Add(GetType(WSCDetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As WSCDetail
            Return CType(m_WSCDetailMapper.Retrieve(ID), WSCDetail)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_WSCDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Integer
            Return CType(m_WSCDetailMapper.RetrieveScalar(aggregation, criterias), Integer)
        End Function

        Public Function Retrieve(ByVal Code As String) As WSCDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(WSCDetail), "ClaimNumber", MatchType.Exact, Code))

            Dim WSCDetailColl As ArrayList = m_WSCDetailMapper.RetrieveByCriteria(criterias)
            If (WSCDetailColl.Count > 0) Then
                Return CType(WSCDetailColl(0), WSCDetail)
            End If
            Return New WSCDetail
        End Function

        Public Function RetrieveOpenWSC() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(WSCDetail), "ClaimStatus", MatchType.Exact, "Open"))

            Return m_WSCDetailMapper.RetrieveByCriteria(criterias)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is WSCHeader) Then
                CType(InsertArg.DomainObject, WSCHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, WSCHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is WSCDetail) Then
                CType(InsertArg.DomainObject, WSCDetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function Update(ByVal objDomain As WSCDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_WSCDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
            End Try
            Return nResult
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_WSCDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function


        Public Function ValidateCode(ByVal sLaborCode As String, ByVal sWorkCode As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCDetail), "Status", MatchType.Exact, 0))
            crit.opAnd(New Criteria(GetType(WSCDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(WSCDetail), "WorkCode", MatchType.Exact, sWorkCode))
            crit.opAnd(New Criteria(GetType(WSCDetail), "PositionCode", MatchType.Exact, sLaborCode))

            Dim agg As Aggregate = New Aggregate(GetType(WSCDetail), "ID", AggregateType.Count)

            Dim count As Integer = -1
            Try
                count = CType(m_WSCDetailMapper.RetrieveScalar(agg, crit), Integer)
            Catch ex As Exception
                count = -1
            End Try
            Return count
        End Function

        Public Function RetrieveSP(ByVal fakturNumber As String, dealerCode As String) As DataSet
            Dim arr As New DataSet
            Dim strQuery = "exec [sp_RetrieveFakturWSC] '" & fakturNumber & "', '" & dealerCode & "'"
            arr = m_WSCDetailMapper.RetrieveDataSet(strQuery)
            Return arr
        End Function
#End Region

    End Class
End Namespace

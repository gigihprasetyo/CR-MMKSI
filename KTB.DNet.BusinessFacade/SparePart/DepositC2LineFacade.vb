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

    Public Class DepositC2LineFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_DepositC2LineMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_DepositC2LineMapper = MapperFactory.GetInstance().GetMapper(GetType(DepositC2Line).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(DepositC2))
            Me.DomainTypeCollection.Add(GetType(DepositC2Line))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DepositC2Line
            Return CType(m_DepositC2LineMapper.Retrieve(ID), DepositC2Line)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(DepositC2Line), "DocumentDate", Sort.SortDirection.ASC))
           
            Return m_DepositC2LineMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Integer
            Return CType(m_DepositC2LineMapper.RetrieveScalar(aggregation, criterias), Integer)
        End Function

        Public Function Retrieve(ByVal Code As String) As DepositC2Line
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositC2Line), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositC2Line), "ClaimNumber", MatchType.Exact, Code))

            Dim DepositC2Coll As ArrayList = m_DepositC2LineMapper.RetrieveByCriteria(criterias)
            If (DepositC2Coll.Count > 0) Then
                Return CType(DepositC2Coll(0), DepositC2Line)
            End If
            Return New DepositC2Line
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is DepositC2) Then
                CType(InsertArg.DomainObject, DepositC2).ID = InsertArg.ID
                CType(InsertArg.DomainObject, DepositC2).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is DepositC2Line) Then
                CType(InsertArg.DomainObject, DepositC2Line).ID = InsertArg.ID
            End If
        End Sub

        Public Function Update(ByVal objDomain As DepositC2Line) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DepositC2LineMapper.Update(objDomain, m_userPrincipal.Identity.Name)
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

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As DepositC2Line
            Dim DepositC2Coll As ArrayList = m_DepositC2LineMapper.RetrieveByCriteria(criterias, sorts)
            If DepositC2Coll.Count > 0 Then
                Return CType(DepositC2Coll(0), DepositC2Line)
            Else
                Return Nothing
            End If
        End Function

        Public Function RetrieveByBillingNumber(ByVal Code As String) As DepositC2Line
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositC2Line), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositC2Line), "DocumentNo", MatchType.Exact, Code))

            Dim DepositC2Coll As ArrayList = m_DepositC2LineMapper.RetrieveByCriteria(criterias)
            If (DepositC2Coll.Count > 0) Then
                Return CType(DepositC2Coll(0), DepositC2Line)
            End If
            Return New DepositC2Line
        End Function

#End Region

    End Class

End Namespace

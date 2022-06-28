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
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region
Namespace KTB.DNet.BusinessFacade.General
    Public Class ManualDocFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ManualDocMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_ManualDocMapper = MapperFactory.GetInstance.GetMapper(GetType(ManualDoc).ToString)
            Me.m_TransactionManager = New TransactionManager
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.ManualDoc))
            AddHandler Me.m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ManualDoc
            Return CType(m_ManualDocMapper.Retrieve(ID), ManualDoc)
        End Function

        Public Function Retrieve(ByVal Code As String) As ManualDoc
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ManualDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ManualDoc), "Code", MatchType.Exact, Code))

            Dim ManualDocColl As ArrayList = m_ManualDocMapper.RetrieveByCriteria(criterias)
            If (ManualDocColl.Count > 0) Then
                Return CType(ManualDocColl(0), ManualDoc)
            End If
            Return New ManualDoc
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ManualDocMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ManualDocMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ManualDocMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ManualDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ManualDocMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ManualDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ManualDocMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ManualDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ManualDoc As ArrayList = m_ManualDocMapper.RetrieveByCriteria(criterias)
            Return _ManualDoc
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ManualDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ManualDocColl As ArrayList = m_ManualDocMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ManualDocColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ManualDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ManualDocColl As ArrayList = m_ManualDocMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ManualDocColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ManualDocColl As ArrayList = m_ManualDocMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ManualDocColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ManualDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ManualDocColl As ArrayList = m_ManualDocMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(ManualDoc), columnName, matchOperator, columnValue))
            Return ManualDocColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ManualDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ManualDoc), columnName, matchOperator, columnValue))

            Return m_ManualDocMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"
        Public Function ValidateManualName(ByVal manualName As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ManualDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(ManualDoc), "Name", MatchType.Exact, manualName))
            Dim aggregat As Aggregate = New Aggregate(GetType(ManualDoc), "Name", AggregateType.Count)
            Return CType(m_ManualDocMapper.RetrieveScalar(aggregat, crit), Integer)
        End Function

        Public Function ValidateSeqNo(ByVal SeqNo As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ManualDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(ManualDoc), "Sequence", MatchType.Exact, SeqNo))
            Dim aggregat As Aggregate = New Aggregate(GetType(ManualDoc), "Sequence", AggregateType.Count)
            Return CType(m_ManualDocMapper.RetrieveScalar(aggregat, crit), Integer)
        End Function

        Public Function Update(ByVal objDomain As ManualDoc) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ManualDocMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Insert(ByVal objDomain As ManualDoc) As Integer
            Dim returnValue As Integer = -1
            If Me.IsTaskFree Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
                    End If
                Catch ex As Exception
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

        Public Function Delete(ByVal objDomain As ManualDoc) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = DBRowStatus.Deleted
                nResult = m_ManualDocMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.ManualDoc) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.ManualDoc).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.ManualDoc).MarkLoaded()
            End If
        End Sub
#End Region

#Region "Custom Method"

#End Region
    End Class
End Namespace


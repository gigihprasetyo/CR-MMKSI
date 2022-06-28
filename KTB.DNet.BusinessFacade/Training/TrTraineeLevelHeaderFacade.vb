
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

Namespace KTB.DNet.BusinessFacade.Training
    Public Class TrTraineeLevelHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TrTraineeLevelHeaderMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_TrTraineeLevelHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(TrTraineeLevelHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TrTraineeLevelHeader
            Return CType(m_TrTraineeLevelHeaderMapper.Retrieve(ID), TrTraineeLevelHeader)
        End Function

        Public Function Retrieve(ByVal Description As String) As TrTraineeLevelHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTraineeLevelHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrTraineeLevelHeader), "Description", MatchType.Exact, Description))

            Dim TrTraineeLevelHeaderColl As ArrayList = m_TrTraineeLevelHeaderMapper.RetrieveByCriteria(criterias)
            If (TrTraineeLevelHeaderColl.Count > 0) Then
                Return CType(TrTraineeLevelHeaderColl(0), TrTraineeLevelHeader)
            End If
            Return New TrTraineeLevelHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TrTraineeLevelHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TrTraineeLevelHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TrTraineeLevelHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTraineeLevelHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrTraineeLevelHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTraineeLevelHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrTraineeLevelHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTraineeLevelHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TrTraineeLevelHeader As ArrayList = m_TrTraineeLevelHeaderMapper.RetrieveByCriteria(criterias)
            Return _TrTraineeLevelHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTraineeLevelHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrTraineeLevelHeaderColl As ArrayList = m_TrTraineeLevelHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrTraineeLevelHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTraineeLevelHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_TrTraineeLevelHeaderMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTraineeLevelHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTraineeLevelHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrTraineeLevelHeaderColl As ArrayList = m_TrTraineeLevelHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return TrTraineeLevelHeaderColl
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTraineeLevelHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim TrTraineeLevelHeaderColl As ArrayList = m_TrTraineeLevelHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrTraineeLevelHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTraineeLevelHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim TrTraineeLevelHeaderColl As ArrayList = m_TrTraineeLevelHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrTraineeLevelHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TrTraineeLevelHeaderColl As ArrayList = m_TrTraineeLevelHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TrTraineeLevelHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTraineeLevelHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrTraineeLevelHeaderColl As ArrayList = m_TrTraineeLevelHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TrTraineeLevelHeader), columnName, matchOperator, columnValue))
            Return TrTraineeLevelHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTraineeLevelHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTraineeLevelHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(TrTraineeLevelHeader), columnName, matchOperator, columnValue))

            Return m_TrTraineeLevelHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTraineeLevelHeader), "ClassCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(TrTraineeLevelHeader), "ClassCode", AggregateType.Count)
            Return CType(m_TrTraineeLevelHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As TrTraineeLevelHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                m_TrTraineeLevelHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function InsertTransaction(ByVal objDomain As TrTraineeLevelHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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
        Public Function Update(ByVal objDomain As TrTraineeLevelHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TrTraineeLevelHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub DeleteFromDB(ByVal objDomain As TrTraineeLevelHeader)
            Try
                m_TrTraineeLevelHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is TrTraineeLevelHeader) Then
                CType(InsertArg.DomainObject, TrTraineeLevelHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, TrTraineeLevelHeader).MarkLoaded()

            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


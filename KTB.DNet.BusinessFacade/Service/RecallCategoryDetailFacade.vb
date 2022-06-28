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
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade.Service

    Public Class RecallCategoryDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_RecallCategoryDetailMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_RecallCategoryDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(RecallCategoryDetail).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As RecallCategoryDetail
            Return CType(m_RecallCategoryDetailMapper.Retrieve(ID), RecallCategoryDetail)
        End Function

        Public Function Retrieve(ByVal Code As String) As RecallCategoryDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallCategoryDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RecallCategoryDetail), "RecallRegNo", MatchType.Exact, Code))

            Dim RecallCategoryDetailColl As ArrayList = m_RecallCategoryDetailMapper.RetrieveByCriteria(criterias)
            If (RecallCategoryDetailColl.Count > 0) Then
                Return CType(RecallCategoryDetailColl(0), RecallCategoryDetail)
            End If
            Return New RecallCategoryDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_RecallCategoryDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_RecallCategoryDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_RecallCategoryDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RecallCategoryDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RecallCategoryDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RecallCategoryDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RecallCategoryDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallCategoryDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _RecallCategory As ArrayList = m_RecallCategoryDetailMapper.RetrieveByCriteria(criterias)
            Return _RecallCategory
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RecallCategoryDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AppConfigColl As ArrayList = m_RecallCategoryDetailMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AppConfigColl
        End Function


        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RecallCategoryDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AppConfigColl As ArrayList = m_RecallCategoryDetailMapper.RetrieveByCriteria(Criterias, sortColl)
            Return AppConfigColl
        End Function


        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallCategoryDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RecallCategoryDetailColl As ArrayList = m_RecallCategoryDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return RecallCategoryDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(RecallCategoryDetail), SortColumn, sortDirection))
            Dim RecallCategoryDetailColl As ArrayList = m_RecallCategoryDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return RecallCategoryDetailColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim RecallCategoryDetailColl As ArrayList = m_RecallCategoryDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return RecallCategoryDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallCategoryDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RecallCategoryDetailColl As ArrayList = m_RecallCategoryDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(RecallCategoryDetail), columnName, matchOperator, columnValue))
            Return RecallCategoryDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RecallCategoryDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallCategoryDetail), columnName, matchOperator, columnValue))

            Return m_RecallCategoryDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallCategoryDetail), "RecallCategoryCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(RecallCategoryDetail), "RecallCategoryCode", AggregateType.Count)
            Return CType(m_RecallCategoryDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As RecallCategoryDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_RecallCategoryDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As RecallCategoryDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_RecallCategoryDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As RecallCategoryDetail)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)

                nResult = m_RecallCategoryDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As RecallCategoryDetail)
            Try
                m_RecallCategoryDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function DoRetrieveDataset(ByVal strSql As String) As DataSet
            Dim ds As New DataSet()
            ds = m_RecallCategoryDetailMapper.RetrieveDataSet(strSql)
            Return ds
        End Function
#End Region

    End Class

End Namespace


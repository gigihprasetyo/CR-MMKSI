#Region "imports library namespace"
Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
#End Region

#Region "imports custom namespace"
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class CustomerBusinessFacade

#Region "private variables"
        Private m_CustomerBusinessMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
#End Region

#Region "constructor"
        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_CustomerBusinessMapper = MapperFactory.GetInstance().GetMapper(GetType(CustomerBusiness).ToString)
        End Sub
#End Region

#Region "retrieve"
        Public Function Retrieve(ByVal ID As Integer) As CustomerBusiness
            Return CType(m_CustomerBusinessMapper.Retrieve(ID), CustomerBusiness)
        End Function

        Public Function Retrieve(ByVal Code As String) As CustomerBusiness
            Dim objReturnValue As CustomerBusiness
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerBusiness), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CustomerBusiness), "Code", MatchType.Exact, Code))
            Dim List As ArrayList
            List = m_CustomerBusinessMapper.RetrieveByCriteria(criterias)
            If Not List Is Nothing Then
                If List.Count > 0 Then
                    objReturnValue = CType(List.Item(0), CustomerBusiness)
                End If
            End If
            Return objReturnValue
        End Function

        Public Function IsCustomerBusinessFound(ByVal strCustomerBusinessCode As String) As Boolean _
        'lagi trace yg ini jgn lupa

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerBusiness), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim bResult As Boolean = False
            Dim CustomerBusinessColl As ArrayList = m_CustomerBusinessMapper.RetrieveByCriteria(criterias)
            If (CustomerBusinessColl.Count > 0) Then
                bResult = True
            Else
            End If
            Return bResult

        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_CustomerBusinessMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_CustomerBusinessMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_CustomerBusinessMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerBusiness), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CustomerBusinessMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerBusiness), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CustomerBusinessMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerBusiness), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _CustomerBusiness As ArrayList = m_CustomerBusinessMapper.RetrieveByCriteria(criterias)
            Return _CustomerBusiness
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerBusiness), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CustomerBusinessColl As ArrayList = m_CustomerBusinessMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return CustomerBusinessColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerBusiness), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim CustomerBusinessColl As ArrayList = m_CustomerBusinessMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return CustomerBusinessColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
          ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerBusiness), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_CustomerBusinessMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
               ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerBusiness), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(CustomerBusiness), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CustomerBusinessMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim CustomerBusinessColl As ArrayList = m_CustomerBusinessMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CustomerBusinessColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerBusiness), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CustomerBusiness), columnName, matchOperator, columnValue))
            Dim CustomerBusinessColl As ArrayList = m_CustomerBusinessMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CustomerBusinessColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerBusiness), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerBusiness), columnName, matchOperator, columnValue))

            Return m_CustomerBusinessMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "trans/other public method"
        Public Function Insert(ByVal objDomain As CustomerBusiness) As Integer
            Dim iReturn As Integer = -2
            Try
                m_CustomerBusinessMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As CustomerBusiness) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_CustomerBusinessMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As CustomerBusiness)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_CustomerBusinessMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As CustomerBusiness) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_CustomerBusinessMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerBusiness), "Code", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(CustomerBusiness), "ID", AggregateType.Count)
            Return CType(m_CustomerBusinessMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "custom method"

#End Region


    End Class

End Namespace


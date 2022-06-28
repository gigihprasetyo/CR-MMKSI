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
    Public Class ParkingFeeDealerInfoFacade

#Region "private variables"
        Private m_ParkingFeeDealerInfoMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
#End Region

#Region "constructor"
        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_ParkingFeeDealerInfoMapper = MapperFactory.GetInstance().GetMapper(GetType(ParkingFeeDealerInfo).ToString)
        End Sub
#End Region

#Region "retrieve"
        Public Function Retrieve(ByVal ID As Integer) As ParkingFeeDealerInfo
            Return CType(m_ParkingFeeDealerInfoMapper.Retrieve(ID), ParkingFeeDealerInfo)
        End Function

        Public Function IsParkingFeeDealerInfoFound(ByVal strParkingFeeDealerInfoCode As String) As Boolean _
        'lagi trace yg ini jgn lupa

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeDealerInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim bResult As Boolean = False
            Dim ParkingFeeDealerInfoColl As ArrayList = m_ParkingFeeDealerInfoMapper.RetrieveByCriteria(criterias)
            If (ParkingFeeDealerInfoColl.Count > 0) Then
                bResult = True
            Else
            End If
            Return bResult

        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ParkingFeeDealerInfoMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ParkingFeeDealerInfoMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ParkingFeeDealerInfoMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ParkingFeeDealerInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ParkingFeeDealerInfoMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ParkingFeeDealerInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ParkingFeeDealerInfoMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeDealerInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ParkingFeeDealerInfo As ArrayList = m_ParkingFeeDealerInfoMapper.RetrieveByCriteria(criterias)
            Return _ParkingFeeDealerInfo
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeDealerInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ParkingFeeDealerInfoColl As ArrayList = m_ParkingFeeDealerInfoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ParkingFeeDealerInfoColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ParkingFeeDealerInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ParkingFeeDealerInfoColl As ArrayList = m_ParkingFeeDealerInfoMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ParkingFeeDealerInfoColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
          ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ParkingFeeDealerInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_ParkingFeeDealerInfoMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
               ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeDealerInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(ParkingFeeDealerInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ParkingFeeDealerInfoMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ParkingFeeDealerInfoColl As ArrayList = m_ParkingFeeDealerInfoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ParkingFeeDealerInfoColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeDealerInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ParkingFeeDealerInfo), columnName, matchOperator, columnValue))
            Dim ParkingFeeDealerInfoColl As ArrayList = m_ParkingFeeDealerInfoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ParkingFeeDealerInfoColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ParkingFeeDealerInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeDealerInfo), columnName, matchOperator, columnValue))

            Return m_ParkingFeeDealerInfoMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "trans/other public method"
        Public Function Insert(ByVal objDomain As ParkingFeeDealerInfo) As Integer
            Dim iReturn As Integer = -2
            Try
                m_ParkingFeeDealerInfoMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As ParkingFeeDealerInfo) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ParkingFeeDealerInfoMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As ParkingFeeDealerInfo)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_ParkingFeeDealerInfoMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As ParkingFeeDealerInfo) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_ParkingFeeDealerInfoMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

#End Region

#Region "custom method"

#End Region


    End Class

End Namespace


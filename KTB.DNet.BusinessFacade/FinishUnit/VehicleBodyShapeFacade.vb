Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class VehicleBodyShapeFacade
        Inherits AbstractFacade

#Region "private variables"
        Private m_VehicleBodyShapeMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
#End Region

#Region "constructor"
        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_VehicleBodyShapeMapper = MapperFactory.GetInstance().GetMapper(GetType(VehicleBodyShape).ToString)
        End Sub
#End Region

#Region "retrieve"
        Public Function Retrieve(ByVal ID As Integer) As VehicleBodyShape
            Return CType(m_VehicleBodyShapeMapper.Retrieve(ID), VehicleBodyShape)
        End Function

        Public Function Retrieve(ByVal Code As String, ByVal CategoryCode As String) As VehicleBodyShape
            Dim objReturnValue As VehicleBodyShape
            Dim objCategory As Category = New CategoryFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(CategoryCode)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleBodyShape), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VehicleBodyShape), "Code", MatchType.Exact, Code))
            criterias.opAnd(New Criteria(GetType(VehicleBodyShape), "Category.ID", MatchType.Exact, objCategory.ID))

            Dim List As ArrayList
            List = m_VehicleBodyShapeMapper.RetrieveByCriteria(criterias)
            If Not List Is Nothing Then
                If List.Count > 0 Then
                    objReturnValue = CType(List.Item(0), VehicleBodyShape)
                End If
            End If
            Return objReturnValue
        End Function

        Public Function IsVehicleBodyShapeFound(ByVal strVehicleBodyShapeCode As String) As Boolean _
        'lagi trace yg ini jgn lupa

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleBodyShape), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim bResult As Boolean = False
            'criterias.opAnd(New Criteria(GetType(VehicleBodyShape), "CityCode", MatchType.Exact, strVehicleBodyShapeCode))
            Dim VehicleBodyShapeColl As ArrayList = m_VehicleBodyShapeMapper.RetrieveByCriteria(criterias)
            If (VehicleBodyShapeColl.Count > 0) Then
                bResult = True
            Else
            End If
            Return bResult

        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_VehicleBodyShapeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_VehicleBodyShapeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_VehicleBodyShapeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VehicleBodyShape), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VehicleBodyShapeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VehicleBodyShape), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VehicleBodyShapeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleBodyShape), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _VehicleBodyShape As ArrayList = m_VehicleBodyShapeMapper.RetrieveByCriteria(criterias)
            Return _VehicleBodyShape
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleBodyShape), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim VehicleBodyShapeColl As ArrayList = m_VehicleBodyShapeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return VehicleBodyShapeColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VehicleBodyShape), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim VehicleBodyShapeColl As ArrayList = m_VehicleBodyShapeMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return VehicleBodyShapeColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
          ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VehicleBodyShape), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_VehicleBodyShapeMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
               ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleBodyShape), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(VehicleBodyShape), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VehicleBodyShapeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim VehicleBodyShapeColl As ArrayList = m_VehicleBodyShapeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return VehicleBodyShapeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleBodyShape), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VehicleBodyShape), columnName, matchOperator, columnValue))
            Dim VehicleBodyShapeColl As ArrayList = m_VehicleBodyShapeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return VehicleBodyShapeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VehicleBodyShape), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleBodyShape), columnName, matchOperator, columnValue))

            Return m_VehicleBodyShapeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "trans/other public method"
        Public Function Insert(ByVal objDomain As VehicleBodyShape) As Integer
            Dim iReturn As Integer = -2
            Try
                m_VehicleBodyShapeMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As VehicleBodyShape) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_VehicleBodyShapeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As VehicleBodyShape)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_VehicleBodyShapeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As VehicleBodyShape) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_VehicleBodyShapeMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Public Function ValidateCode(ByVal categoryID As Integer, ByVal Code As String) As Integer
            'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleBodyShape), "Dealer.ID", MatchType.Exact, objVehicleBodyShape.Dealer.ID))
            'crit.opAnd(New Criteria(GetType(VehicleBodyShape), "City.ID", objVehicleBodyShape.City.ID))
            'Dim agg As Aggregate = New Aggregate(GetType(VehicleBodyShape), "ID", AggregateType.Count)

            'Return CType(m_VehicleBodyShapeMapper.RetrieveScalar(agg, crit), Integer)

            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleBodyShape), "Code", MatchType.Exact, Code))
            crit.opAnd(New Criteria(GetType(VehicleBodyShape), "Category.ID", MatchType.Exact, categoryID))
            Dim agg As Aggregate = New Aggregate(GetType(VehicleBodyShape), "Code", AggregateType.Count)

            Return CType(m_VehicleBodyShapeMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "custom method"

#End Region

    End Class

End Namespace
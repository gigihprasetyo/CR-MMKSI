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
Imports System.Collections.Generic

#End Region

Namespace KTB.DNET.BusinessFacade
    Public Class SparePartForecastStockFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartForecastStockMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"
        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartForecastStockMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartForecastStock).ToString)
            Me.m_TransactionManager = New TransactionManager
        End Sub
#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartForecastStock
            Return CType(m_SparePartForecastStockMapper.Retrieve(ID), SparePartForecastStock)
        End Function

        Public Function Retrieve(ByVal Code As String) As SparePartForecastStock
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartForecastStock), "PoNumber", MatchType.Exact, Code))

            Dim SparePartForecastStockColl As ArrayList = m_SparePartForecastStockMapper.RetrieveByCriteria(criterias)
            If (SparePartForecastStockColl.Count > 0) Then
                Return CType(SparePartForecastStockColl(0), SparePartForecastStock)
            End If
            Return New SparePartForecastStock
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartForecastStockMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartForecastStockMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartForecastStockMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastStock), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartForecastStockMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastStock), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartForecastStockMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SparePartForecastStock As ArrayList = m_SparePartForecastStockMapper.RetrieveByCriteria(criterias)
            Return _SparePartForecastStock
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartForecastStockColl As ArrayList = m_SparePartForecastStockMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartForecastStockColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SparePartForecastStock), SortColumn, sortDirection))
            Dim SparePartForecastStockColl As ArrayList = m_SparePartForecastStockMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SparePartForecastStockColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastStock), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AppConfigColl As ArrayList = m_SparePartForecastStockMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AppConfigColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartForecastStockColl As ArrayList = m_SparePartForecastStockMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartForecastStockColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartForecastStockColl As ArrayList = m_SparePartForecastStockMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SparePartForecastStock), columnName, matchOperator, columnValue))
            Return SparePartForecastStockColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastStock), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastStock), columnName, matchOperator, columnValue))

            Return m_SparePartForecastStockMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastStock), "NoReCallCategory", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SparePartForecastStock), "PoNumber", AggregateType.Count)
            Return CType(m_SparePartForecastStockMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SparePartForecastStock) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SparePartForecastStockMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartForecastStock) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartForecastStockMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartForecastStock)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SparePartForecastStockMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SparePartForecastStock)
            Try
                m_SparePartForecastStockMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub InsertWithTransactionManagerHandler(ByVal sender As Object, ByVal args As TransactionManager.OnInsertArgs)
            ' set the object ID from db returned id
            If (args.DomainObject.GetType = GetType(SparePartForecastStock)) Then
                CType(args.DomainObject, SparePartForecastStock).ID = args.ID
                CType(args.DomainObject, SparePartForecastStock).MarkLoaded()
            End If

        End Sub

        Public Function BatchInsertOrUpdateWithTransactionManager(ByVal SparePartForecastStockList As ArrayList) As Integer
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
            If Me.IsTaskFree Then
                Me.SetTaskLocking()
                Try
                    For Each SparePartForecastStock As SparePartForecastStock In SparePartForecastStockList
                        If SparePartForecastStock.ID = 0 Then
                            Me.m_TransactionManager.AddInsert(SparePartForecastStock, m_userPrincipal.Identity.Name)
                        Else
                            Me.m_TransactionManager.AddUpdate(SparePartForecastStock, m_userPrincipal.Identity.Name)
                            SparePartForecastStock.MarkLoaded()
                        End If

                    Next
                    Me.m_TransactionManager.PerformTransaction()
                    result = 1
                Catch ex As Exception
                    Throw ex
                    result = -1
                Finally
                    Me.RemoveTaskLocking()
                End Try

            End If

            Return result

        End Function

#End Region

#Region "Custom Method"
        Public Function UpdateSparePartMaster(ByVal SparePartMasterID As Integer, ByVal RetailPrice As Decimal,
                                        ByVal LastUpdateBy As String, ByVal ValidFrom As DateTime) As Boolean

            Dim spName As String = "sp_UpdateSparePartMaster"
            Dim Param As New List(Of SqlClient.SqlParameter)

            Param.Add(New SqlClient.SqlParameter("@SparePartMasterID", SparePartMasterID))
            Param.Add(New SqlClient.SqlParameter("@RetailPrice", RetailPrice))
            Param.Add(New SqlClient.SqlParameter("@LastUpdateBy", LastUpdateBy))
            Param.Add(New SqlClient.SqlParameter("@ValidFrom", ValidFrom))

            Dim arrRencanaProduksi As ArrayList = New ArrayList

            Dim dt As Boolean = m_SparePartForecastStockMapper.ExecuteSP(spName, New ArrayList(Param))

            Return dt
        End Function

        Public Function GetSparePartPrice(ByVal ValidFrom As DateTime) As List(Of SparePartForecastStock)

            Dim spName As String = "sp_GetSparePartForecastStocktoUpdate"
            Dim Param As New List(Of SqlClient.SqlParameter)

            Param.Add(New SqlClient.SqlParameter("@ValidFrom", ValidFrom))

            Dim arrRencanaProduksi As ArrayList = New ArrayList

            Dim arr As ArrayList = New ArrayList
            arr = m_SparePartForecastStockMapper.RetrieveSP(spName, New ArrayList(Param))

            Dim ReturnList As List(Of SparePartForecastStock) = New List(Of SparePartForecastStock)
            For Each SPPrice As SparePartForecastStock In arr
                ReturnList.Add(SPPrice)
            Next
            Return ReturnList

        End Function

        Public Function RetrieveByPartNumber(ByVal PartNumber As String) As SparePartForecastStock
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartForecastStock), "SparePartForecastMaster.SparePartMaster.PartNumber", MatchType.Exact, PartNumber))

            Dim SparePartForecastStockColl As ArrayList = m_SparePartForecastStockMapper.RetrieveByCriteria(criterias)
            If (SparePartForecastStockColl.Count > 0) Then
                Return CType(SparePartForecastStockColl(0), SparePartForecastStock)
            End If
            Return New SparePartForecastStock
        End Function
#End Region

    End Class
End Namespace
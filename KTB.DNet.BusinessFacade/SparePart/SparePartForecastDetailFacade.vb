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
    Public Class SparePartForecastDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartForecastDetailMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"
        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartForecastDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartForecastDetail).ToString)
            Me.m_TransactionManager = New TransactionManager
        End Sub
#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartForecastDetail
            Return CType(m_SparePartForecastDetailMapper.Retrieve(ID), SparePartForecastDetail)
        End Function

        Public Function RetrieveHd(ByVal Code As String) As SparePartForecastDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "SparePartForecastHeader.ID", MatchType.Exact, Code))

            Dim SparePartForecastDetailColl As ArrayList = m_SparePartForecastDetailMapper.RetrieveByCriteria(criterias)
            If (SparePartForecastDetailColl.Count > 0) Then
                Return CType(SparePartForecastDetailColl(0), SparePartForecastDetail)
            End If
            Return New SparePartForecastDetail
        End Function

        Public Function Retrieve(ByVal Code As String) As SparePartForecastDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "SparePartForecastMaster.ID", MatchType.Exact, Code))

            Dim SparePartForecastDetailColl As ArrayList = m_SparePartForecastDetailMapper.RetrieveByCriteria(criterias)
            If (SparePartForecastDetailColl.Count > 0) Then
                Return CType(SparePartForecastDetailColl(0), SparePartForecastDetail)
            End If
            Return New SparePartForecastDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartForecastDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartForecastDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartForecastDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartForecastDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartForecastDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SparePartForecastDetail As ArrayList = m_SparePartForecastDetailMapper.RetrieveByCriteria(criterias)
            Return _SparePartForecastDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartForecastDetailColl As ArrayList = m_SparePartForecastDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartForecastDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SparePartForecastDetail), SortColumn, sortDirection))
            Dim SparePartForecastDetailColl As ArrayList = m_SparePartForecastDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SparePartForecastDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AppConfigColl As ArrayList = m_SparePartForecastDetailMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AppConfigColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartForecastDetailColl As ArrayList = m_SparePartForecastDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartForecastDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartForecastDetailColl As ArrayList = m_SparePartForecastDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), columnName, matchOperator, columnValue))
            Return SparePartForecastDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastDetail), columnName, matchOperator, columnValue))

            Return m_SparePartForecastDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastDetail), "NoReCallCategory", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SparePartForecastDetail), "PoNumber", AggregateType.Count)
            Return CType(m_SparePartForecastDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SparePartForecastDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SparePartForecastDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartForecastDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartForecastDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartForecastDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SparePartForecastDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SparePartForecastDetail)
            Try
                m_SparePartForecastDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub InsertWithTransactionManagerHandler(ByVal sender As Object, ByVal args As TransactionManager.OnInsertArgs)
            ' set the object ID from db returned id
            If (args.DomainObject.GetType = GetType(SparePartForecastDetail)) Then
                CType(args.DomainObject, SparePartForecastDetail).ID = args.ID
                CType(args.DomainObject, SparePartForecastDetail).MarkLoaded()
            End If

        End Sub

        Public Function BatchInsertOrUpdateWithTransactionManager(ByVal SparePartForecastDetailList As ArrayList) As Integer
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
            If Me.IsTaskFree Then
                Me.SetTaskLocking()
                Try
                    For Each SparePartForecastDetail As SparePartForecastDetail In SparePartForecastDetailList
                        If SparePartForecastDetail.ID = 0 Then
                            Me.m_TransactionManager.AddInsert(SparePartForecastDetail, m_userPrincipal.Identity.Name)
                        Else
                            Me.m_TransactionManager.AddUpdate(SparePartForecastDetail, m_userPrincipal.Identity.Name)
                            SparePartForecastDetail.MarkLoaded()
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

            Dim dt As Boolean = m_SparePartForecastDetailMapper.ExecuteSP(spName, New ArrayList(Param))

            Return dt
        End Function

        Public Function GetSparePartPrice(ByVal ValidFrom As DateTime) As List(Of SparePartForecastDetail)

            Dim spName As String = "sp_GetSparePartForecastDetailtoUpdate"
            Dim Param As New List(Of SqlClient.SqlParameter)

            Param.Add(New SqlClient.SqlParameter("@ValidFrom", ValidFrom))

            Dim arrRencanaProduksi As ArrayList = New ArrayList

            Dim arr As ArrayList = New ArrayList
            arr = m_SparePartForecastDetailMapper.RetrieveSP(spName, New ArrayList(Param))

            Dim ReturnList As List(Of SparePartForecastDetail) = New List(Of SparePartForecastDetail)
            For Each SPPrice As SparePartForecastDetail In arr
                ReturnList.Add(SPPrice)
            Next
            Return ReturnList

        End Function

        Function UpdateTransaction(oSparePartForecastDetail As SparePartForecastDetail, oSparePartForecastMaster As SparePartForecastMaster) As Integer
            Dim returnValue As Integer = -1
            Try
                If Not IsNothing(oSparePartForecastDetail) Then
                    m_TransactionManager.AddUpdate(oSparePartForecastDetail, m_userPrincipal.Identity.Name)
                End If

                If Not IsNothing(oSparePartForecastMaster) Then
                    m_TransactionManager.AddUpdate(oSparePartForecastMaster, m_userPrincipal.Identity.Name)
                End If

                m_TransactionManager.PerformTransaction()
                returnValue = 1
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return returnValue
        End Function

#End Region



    End Class
End Namespace

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography

#End Region

#Region "Custom Namespace"

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Framework
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Collections.Generic

#End Region

Namespace KTB.DNET.BusinessFacade
    Public Class SparePartForecastMasterFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartForecastMasterMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"
        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartForecastMasterMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartForecastMaster).ToString)
            Me.m_TransactionManager = New TransactionManager
        End Sub
#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartForecastMaster
            Return CType(m_SparePartForecastMasterMapper.Retrieve(ID), SparePartForecastMaster)
        End Function

        Public Function Retrieve(ByVal Code As String) As SparePartForecastMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartForecastMaster), "SparePartMaster.PartNumber", MatchType.Exact, Code))

            Dim SparePartForecastMasterColl As ArrayList = m_SparePartForecastMasterMapper.RetrieveByCriteria(criterias)
            If (SparePartForecastMasterColl.Count > 0) Then
                Return CType(SparePartForecastMasterColl(0), SparePartForecastMaster)
            End If
            Return New SparePartForecastMaster
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartForecastMasterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartForecastMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartForecastMasterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartForecastMasterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartForecastMasterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SparePartForecastMaster As ArrayList = m_SparePartForecastMasterMapper.RetrieveByCriteria(criterias)
            Return _SparePartForecastMaster
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartForecastMasterColl As ArrayList = m_SparePartForecastMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartForecastMasterColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SparePartForecastMaster), SortColumn, sortDirection))
            Dim SparePartForecastMasterColl As ArrayList = m_SparePartForecastMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SparePartForecastMasterColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AppConfigColl As ArrayList = m_SparePartForecastMasterMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AppConfigColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartForecastMasterColl As ArrayList = m_SparePartForecastMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartForecastMasterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SparePartMasterColl As ArrayList = m_SparePartForecastMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SparePartMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartForecastMasterColl As ArrayList = m_SparePartForecastMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SparePartForecastMaster), columnName, matchOperator, columnValue))
            Return SparePartForecastMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastMaster), columnName, matchOperator, columnValue))

            Return m_SparePartForecastMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal q As String) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartForecastMaster), columnName, matchOperator, columnValue))
            criterias.opAnd(New Criteria(GetType(SparePartForecastMaster), "SparePartMaster.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartForecastMaster), "SparePartMaster.ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))

            Dim SparePartMasterColl As ArrayList = m_SparePartForecastMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            If SparePartMasterColl.Count > 0 Then
                Return SparePartMasterColl
            End If
            Return New ArrayList
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartForecastMaster), "SparePartMaster.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartForecastMaster), "SparePartMaster.ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))

            Dim SparePartMasterColl As ArrayList = m_SparePartForecastMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            If SparePartMasterColl.Count > 0 Then
                Return SparePartMasterColl
            End If
            Return New ArrayList
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastMaster), "NoReCallCategory", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SparePartForecastMaster), "NoReCallCategory", AggregateType.Count)
            Return CType(m_SparePartForecastMasterMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SparePartForecastMaster) As Integer
            Dim iReturn As Integer = -2
            Dim _user As String = m_userPrincipal.Identity.Name
            Try
                iReturn = m_SparePartForecastMasterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)

            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try

            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartForecastMaster) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartForecastMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartForecastMaster)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SparePartForecastMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SparePartForecastMaster)
            Try
                m_SparePartForecastMasterMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub InsertWithTransactionManagerHandler(ByVal sender As Object, ByVal args As TransactionManager.OnInsertArgs)
            ' set the object ID from db returned id
            If (args.DomainObject.GetType = GetType(SparePartForecastMaster)) Then
                CType(args.DomainObject, SparePartForecastMaster).ID = args.ID
                CType(args.DomainObject, SparePartForecastMaster).MarkLoaded()
            End If

        End Sub

        Public Function BatchInsertOrUpdateWithTransactionManager(ByVal SparePartForecastMasterList As ArrayList) As Integer
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
            If Me.IsTaskFree Then
                Me.SetTaskLocking()
                Try
                    For Each SparePartForecastMaster As SparePartForecastMaster In SparePartForecastMasterList
                        If SparePartForecastMaster.ID = 0 Then
                            Me.m_TransactionManager.AddInsert(SparePartForecastMaster, m_userPrincipal.Identity.Name)
                        Else
                            Me.m_TransactionManager.AddUpdate(SparePartForecastMaster, m_userPrincipal.Identity.Name)
                            SparePartForecastMaster.MarkLoaded()
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

            Dim dt As Boolean = m_SparePartForecastMasterMapper.ExecuteSP(spName, New ArrayList(Param))

            Return dt
        End Function

        Public Function GetSparePartPrice(ByVal ValidFrom As DateTime) As List(Of SparePartForecastMaster)

            Dim spName As String = "sp_GetSparePartForecastMastertoUpdate"
            Dim Param As New List(Of SqlClient.SqlParameter)

            Param.Add(New SqlClient.SqlParameter("@ValidFrom", ValidFrom))

            Dim arrRencanaProduksi As ArrayList = New ArrayList

            Dim arr As ArrayList = New ArrayList
            arr = m_SparePartForecastMasterMapper.RetrieveSP(spName, New ArrayList(Param))

            Dim ReturnList As List(Of SparePartForecastMaster) = New List(Of SparePartForecastMaster)
            For Each SPPrice As SparePartForecastMaster In arr
                ReturnList.Add(SPPrice)
            Next
            Return ReturnList

        End Function

        Public Function RetrieveByPartNumber(ByVal PartNumber As String) As SparePartForecastMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartForecastMaster), "SparePartMaster.PartNumber", MatchType.Exact, PartNumber))

            Dim SparePartForecastMasterColl As ArrayList = m_SparePartForecastMasterMapper.RetrieveByCriteria(criterias)
            If (SparePartForecastMasterColl.Count > 0) Then
                Return CType(SparePartForecastMasterColl(0), SparePartForecastMaster)
            End If
            Return New SparePartForecastMaster
        End Function


#End Region

    End Class
End Namespace

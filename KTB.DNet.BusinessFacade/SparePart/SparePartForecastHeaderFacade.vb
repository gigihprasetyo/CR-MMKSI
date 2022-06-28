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
    Public Class SparePartForecastHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartForecastHeaderMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"
        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartForecastHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartForecastHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
        End Sub
#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartForecastHeader
            Return CType(m_SparePartForecastHeaderMapper.Retrieve(ID), SparePartForecastHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As SparePartForecastHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartForecastHeader), "SparePartMaster.PartNumber", MatchType.Exact, Code))

            Dim SparePartForecastHeaderColl As ArrayList = m_SparePartForecastHeaderMapper.RetrieveByCriteria(criterias)
            If (SparePartForecastHeaderColl.Count > 0) Then
                Return CType(SparePartForecastHeaderColl(0), SparePartForecastHeader)
            End If
            Return New SparePartForecastHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartForecastHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartForecastHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartForecastHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartForecastHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartForecastHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SparePartForecastHeader As ArrayList = m_SparePartForecastHeaderMapper.RetrieveByCriteria(criterias)
            Return _SparePartForecastHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartForecastHeaderColl As ArrayList = m_SparePartForecastHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartForecastHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SparePartForecastHeader), SortColumn, sortDirection))
            Dim SparePartForecastHeaderColl As ArrayList = m_SparePartForecastHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SparePartForecastHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AppConfigColl As ArrayList = m_SparePartForecastHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AppConfigColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartForecastHeaderColl As ArrayList = m_SparePartForecastHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartForecastHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartForecastHeaderColl As ArrayList = m_SparePartForecastHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SparePartForecastHeader), columnName, matchOperator, columnValue))
            Return SparePartForecastHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastHeader), columnName, matchOperator, columnValue))

            Return m_SparePartForecastHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal q As String) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartForecastHeader), columnName, matchOperator, columnValue))

            Dim SparePartMasterColl As ArrayList = m_SparePartForecastHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            If SparePartMasterColl.Count > 0 Then
                Return SparePartMasterColl
            End If
            Return New ArrayList
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartForecastHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim SparePartMasterColl As ArrayList = m_SparePartForecastHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            If SparePartMasterColl.Count > 0 Then
                Return SparePartMasterColl
            End If
            Return New ArrayList
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastHeader), "NoReCallCategory", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SparePartForecastHeader), "PoNumber", AggregateType.Count)
            Return CType(m_SparePartForecastHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SparePartForecastHeader) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    objDomain.PoDate = Date.Now.Date
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    For Each oSPFDetail As SparePartForecastDetail In objDomain.SPFDetails
                        oSPFDetail.SparePartForecastHeader = objDomain
                        oSPFDetail.RequestDate = Date.Now.Date
                        m_TransactionManager.AddInsert(oSPFDetail, m_userPrincipal.Identity.Name)
                    Next

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

            'Dim iReturn As Integer = -2
            'Try
            '    iReturn = m_SparePartForecastHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            'Catch ex As Exception
            '    Dim s As String = ex.Message
            '    iReturn = -1
            'End Try
            'Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartForecastHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartForecastHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartForecastHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SparePartForecastHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SparePartForecastHeader)
            Try
                m_SparePartForecastHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub InsertWithTransactionManagerHandler(ByVal sender As Object, ByVal args As TransactionManager.OnInsertArgs)
            ' set the object ID from db returned id
            If (args.DomainObject.GetType = GetType(SparePartForecastHeader)) Then
                CType(args.DomainObject, SparePartForecastHeader).ID = args.ID
                CType(args.DomainObject, SparePartForecastHeader).MarkLoaded()
            End If

        End Sub

        Public Function InsertTransaction(ByVal objSPFHeader As SparePartForecastHeader, ByVal arrSPFDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    objSPFHeader.PoDate = Date.Now.Date
                    m_TransactionManager.AddInsert(objSPFHeader, m_userPrincipal.Identity.Name)

                    If arrSPFDetail.Count > 0 Then
                        For Each oSPFDetail As SparePartForecastDetail In arrSPFDetail
                            oSPFDetail.SparePartForecastHeader = objSPFHeader
                            oSPFDetail.RequestDate = Date.Now.Date
                            m_TransactionManager.AddInsert(oSPFDetail, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = objSPFHeader.ID
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

        Public Function UpdateTransaction(ByVal objSPFHeader As SparePartForecastHeader, ByVal arrSPFDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arrSPFDetail.Count > 0 Then
                        For Each oSPFDetail As SparePartForecastDetail In arrSPFDetail
                            oSPFDetail.SparePartForecastHeader = objSPFHeader
                            If oSPFDetail.ID <> 0 Then
                                m_TransactionManager.AddUpdate(oSPFDetail, m_userPrincipal.Identity.Name)
                            Else
                                oSPFDetail.RequestDate = Date.Now.Date
                                m_TransactionManager.AddInsert(oSPFDetail, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objSPFHeader, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
                    returnValue = objSPFHeader.ID
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

        Public Function BatchInsertOrUpdateWithTransactionManager(ByVal SparePartForecastHeaderList As ArrayList) As Integer
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
            If Me.IsTaskFree Then
                Me.SetTaskLocking()
                Try
                    For Each SparePartForecastHeader As SparePartForecastHeader In SparePartForecastHeaderList
                        If SparePartForecastHeader.ID = 0 Then
                            Me.m_TransactionManager.AddInsert(SparePartForecastHeader, m_userPrincipal.Identity.Name)
                        Else
                            Me.m_TransactionManager.AddUpdate(SparePartForecastHeader, m_userPrincipal.Identity.Name)
                            SparePartForecastHeader.MarkLoaded()
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

            Dim dt As Boolean = m_SparePartForecastHeaderMapper.ExecuteSP(spName, New ArrayList(Param))

            Return dt
        End Function

        Public Function GetSparePartPrice(ByVal ValidFrom As DateTime) As List(Of SparePartForecastHeader)

            Dim spName As String = "sp_GetSparePartForecastHeadertoUpdate"
            Dim Param As New List(Of SqlClient.SqlParameter)

            Param.Add(New SqlClient.SqlParameter("@ValidFrom", ValidFrom))

            Dim arrRencanaProduksi As ArrayList = New ArrayList

            Dim arr As ArrayList = New ArrayList
            arr = m_SparePartForecastHeaderMapper.RetrieveSP(spName, New ArrayList(Param))

            Dim ReturnList As List(Of SparePartForecastHeader) = New List(Of SparePartForecastHeader)
            For Each SPPrice As SparePartForecastHeader In arr
                ReturnList.Add(SPPrice)
            Next
            Return ReturnList

        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.SparePartForecastHeader) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.SparePartForecastHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.SparePartForecastHeader).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is SparePartForecastDetail) Then
                CType(InsertArg.DomainObject, SparePartForecastDetail).ID = InsertArg.ID
            End If
        End Sub

#End Region

    End Class
End Namespace
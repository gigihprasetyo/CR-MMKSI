#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2005

'Intimedia grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by Intimedia for you 
'(ii) or in solutions that are developed in join development between you and Intimedia.

'All rights not expressly granted, are reserved.
#End Region

#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020
'// ---------------------
'// $History      : $
'// Generated on 7/13/2020 - 7:38:58 PM
'//
'// ===========================================================================		
#End Region

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

    Public Class SparePartMasterPriceFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartMasterPriceMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartMasterPriceMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartMasterPrice).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartMasterPrice
            Return CType(m_SparePartMasterPriceMapper.Retrieve(ID), SparePartMasterPrice)
        End Function

        Public Function Retrieve(ByVal Code As String) As SparePartMasterPrice
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartMasterPrice), "SparePartMasterPriceCode", MatchType.Exact, Code))

            Dim SparePartMasterPriceColl As ArrayList = m_SparePartMasterPriceMapper.RetrieveByCriteria(criterias)
            If (SparePartMasterPriceColl.Count > 0) Then
                Return CType(SparePartMasterPriceColl(0), SparePartMasterPrice)
            End If
            Return New SparePartMasterPrice
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartMasterPriceMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartMasterPriceMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartMasterPriceMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMasterPrice), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartMasterPriceMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMasterPrice), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartMasterPriceMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SparePartMasterPrice As ArrayList = m_SparePartMasterPriceMapper.RetrieveByCriteria(criterias)
            Return _SparePartMasterPrice
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartMasterPriceColl As ArrayList = m_SparePartMasterPriceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartMasterPriceColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SparePartMasterPrice), SortColumn, sortDirection))
            Dim SparePartMasterPriceColl As ArrayList = m_SparePartMasterPriceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SparePartMasterPriceColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartMasterPriceColl As ArrayList = m_SparePartMasterPriceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartMasterPriceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartMasterPriceColl As ArrayList = m_SparePartMasterPriceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SparePartMasterPrice), columnName, matchOperator, columnValue))
            Return SparePartMasterPriceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMasterPrice), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterPrice), columnName, matchOperator, columnValue))

            Return m_SparePartMasterPriceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterPrice), "SparePartMasterPriceCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SparePartMasterPrice), "SparePartMasterPriceCode", AggregateType.Count)
            Return CType(m_SparePartMasterPriceMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SparePartMasterPrice) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SparePartMasterPriceMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartMasterPrice) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartMasterPriceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartMasterPrice)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SparePartMasterPriceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SparePartMasterPrice)
            Try
                m_SparePartMasterPriceMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub InsertWithTransactionManagerHandler(ByVal sender As Object, ByVal args As TransactionManager.OnInsertArgs)

            ' set the object ID from db returned id
            If (args.DomainObject.GetType = GetType(SparePartMasterPrice)) Then
                CType(args.DomainObject, SparePartMasterPrice).ID = args.ID
                CType(args.DomainObject, SparePartMasterPrice).MarkLoaded()
            End If

        End Sub

        Public Function BatchInsertOrUpdateWithTransactionManager(ByVal sparePartMasterPriceList As ArrayList) As Integer
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
            If Me.IsTaskFree Then
                Me.SetTaskLocking()
                Try
                    For Each sparePartMasterPrice As SparePartMasterPrice In sparePartMasterPriceList
                        If sparePartMasterPrice.ID = 0 Then
                            Me.m_TransactionManager.AddInsert(sparePartMasterPrice, m_userPrincipal.Identity.Name)
                        Else
                            Me.m_TransactionManager.AddUpdate(sparePartMasterPrice, m_userPrincipal.Identity.Name)
                            sparePartMasterPrice.MarkLoaded()
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

            Dim dt As Boolean = m_SparePartMasterPriceMapper.ExecuteSP(spName, New ArrayList(Param))

            Return dt
        End Function

        Public Function GetSparePartPrice(ByVal ValidFrom As DateTime) As List(Of SparePartMasterPrice)

            Dim spName As String = "sp_GetSparePartMasterPricetoUpdate"
            Dim Param As New List(Of SqlClient.SqlParameter)

            Param.Add(New SqlClient.SqlParameter("@ValidFrom", ValidFrom))

            Dim arrRencanaProduksi As ArrayList = New ArrayList

            Dim arr As ArrayList = New ArrayList
            arr = m_SparePartMasterPriceMapper.RetrieveSP(spName, New ArrayList(Param))

            Dim ReturnList As List(Of SparePartMasterPrice) = New List(Of SparePartMasterPrice)
            For Each SPPrice As SparePartMasterPrice In arr
                ReturnList.Add(SPPrice)
            Next
            Return ReturnList
            
        End Function

#End Region

    End Class

End Namespace

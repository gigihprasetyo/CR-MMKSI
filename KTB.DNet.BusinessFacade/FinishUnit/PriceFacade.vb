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
'// Author Name   : Agus Pirnadi
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 29/9/2005 - 10:53:00 AM
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

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data.SqlClient

#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class PriceFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_PriceMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing

        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_PriceMapper = MapperFactory.GetInstance().GetMapper(GetType(Price).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As Price
            Return CType(m_PriceMapper.Retrieve(ID), Price)
        End Function

        Public Function Retrieve(ByVal nTypeID As Integer, ByVal sCode As DateTime) As Price
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Price), "VechileColor.ID", MatchType.Exact, nTypeID))
            crit.opAnd(New Criteria(GetType(Price), "ValidFrom", MatchType.Exact, sCode))

            Dim PriceColl As ArrayList = m_PriceMapper.RetrieveByCriteria(crit)
            If (PriceColl.Count > 0) Then
                Return CType(PriceColl(0), Price)
            End If
            Return New Price
        End Function

        Public Function Retrieve(ByVal nTypeID As Integer, ByVal sCode As DateTime, ByVal DealerID As Integer) As Price
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Price), "VechileColor.ID", MatchType.Exact, nTypeID))
            crit.opAnd(New Criteria(GetType(Price), "ValidFrom", MatchType.Exact, sCode))
            crit.opAnd(New Criteria(GetType(Price), "Dealer.ID", MatchType.Exact, DealerID))

            Dim PriceColl As ArrayList = m_PriceMapper.RetrieveByCriteria(crit)
            If (PriceColl.Count > 0) Then
                Return CType(PriceColl(0), Price)
            End If
            Return New Price
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PriceMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PriceMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PriceMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Price), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PriceMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Price), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PriceMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _Price As ArrayList = m_PriceMapper.RetrieveByCriteria(criterias)
            Return _Price
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PriceColl As ArrayList = m_PriceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PriceColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PriceColl As ArrayList = m_PriceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PriceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Price), columnName, matchOperator, columnValue))
            Dim PriceColl As ArrayList = m_PriceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PriceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Price), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Price), columnName, matchOperator, columnValue))

            Return m_PriceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As Price) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_PriceMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As Price) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_PriceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As Price)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_PriceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As Price)
            Try
                m_PriceMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal nTypeID As Integer, ByVal dValidFrom As DateTime, ByVal DealerID As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Price), "VechileColor.ID", MatchType.Exact, nTypeID))
            crit.opAnd(New Criteria(GetType(Price), "ValidFrom", MatchType.Exact, dValidFrom))
            crit.opAnd(New Criteria(GetType(Price), "Dealer.ID", MatchType.Exact, DealerID))


            Dim agg As Aggregate = New Aggregate(GetType(Price), "ValidFrom", AggregateType.Count)

            Return CType(m_PriceMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Return m_PriceMapper.RetrieveByCriteria(criterias, sorts, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PriceMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        '' CR Sirkular Rewards
        '' by : ali Akbar
        '' 2014-09-24

        Public Function RetrieveByCriteria(ByVal ObjContractDetail As ContractDetail) As Price
            Dim ObjPrice As Price = New Price
            Dim objPriceArrayList As ArrayList = New ArrayList
            Dim validFrom As DateTime = New DateTime(ObjContractDetail.ContractHeader.PricePeriodYear, ObjContractDetail.ContractHeader.PricePeriodMonth, ObjContractDetail.ContractHeader.PricePeriodDay)

            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(Price), "Dealer.ID", MatchType.Exact, ObjContractDetail.ContractHeader.Dealer.ID))
            'criterias.opAnd(New Criteria(GetType(Price), "VechileColor.ID", MatchType.Exact, ObjContractDetail.VechileColor.ID))
            'criterias.opAnd(New Criteria(GetType(Price), "ValidFrom", MatchType.LesserOrEqual, validFrom))
            'Dim sortColl As SortCollection = New SortCollection
            'sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.DESC))
            'objPriceArrayList = m_PriceMapper.RetrieveByCriteria(criterias, sortColl)

            'For Each item As Price In objPriceArrayList
            '    If item.ValidFrom <= ValidFrom Then
            '        ObjPrice = item
            '        Exit For
            '    End If
            'Next
            Dim SQL As String

            SQL = String.Format("exec up_RetrievePriceList_Active @ValidFrom='{0}', @dealerid={1} ,@VechileColorID={2}", validFrom.ToString("yyyy/MM/dd"), ObjContractDetail.ContractHeader.Dealer.ID.ToString(), ObjContractDetail.VechileColor.ID.ToString())

            objPriceArrayList = m_PriceMapper.RetrieveSP(SQL)


            For Each item As Price In objPriceArrayList
                If item.ValidFrom <= validFrom Then
                    ObjPrice = item
                    Exit For
                End If
            Next


            Return ObjPrice

        End Function

        Public Function Retrieve(ByVal ObjContractDetail As ContractDetail) As ArrayList

            Dim objPriceArrayList As ArrayList = New ArrayList
            Dim validFrom As DateTime = New DateTime(ObjContractDetail.ContractHeader.PricePeriodYear, ObjContractDetail.ContractHeader.PricePeriodMonth, ObjContractDetail.ContractHeader.PricePeriodDay)

            Dim SQL As String

            SQL = String.Format("exec up_RetrievePriceList_Active @ValidFrom='{0}', @dealerid={1} ,@VechileColorID={2}", validFrom.ToString("yyyy/MM/dd"), ObjContractDetail.ContractHeader.Dealer.ID.ToString(), ObjContractDetail.VechileColor.ID.ToString())

            objPriceArrayList = m_PriceMapper.RetrieveSP(SQL)

            Return objPriceArrayList

        End Function

        '' END OF CR Sirkular Rewards
        ' Add Transaction Manager
        Public Sub InsertWithTransactionManagerHandler(ByVal sender As Object, ByVal args As TransactionManager.OnInsertArgs)

            ' set the object ID from db returned id
            If (args.DomainObject.GetType = GetType(Price)) Then
                CType(args.DomainObject, Price).ID = args.ID
                CType(args.DomainObject, Price).MarkLoaded()
            End If

        End Sub

        Public Function InsertOrUpdateWithTransactionManager(ByVal priceList As ArrayList) As Integer
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
            If Me.IsTaskFree Then
                Me.SetTaskLocking()
                Try
                    For Each price As Price In priceList
                        If price.ID = 0 Then
                            InsertWithTransactionManager(price)
                        Else
                            UpdateWithTransactionManager(price)
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

        Public Sub InsertWithTransactionManager(ByVal price As Price)
            Try
                ' add command to insert Price
                Me.m_TransactionManager.AddInsert(price, m_userPrincipal.Identity.Name)
            Catch sqlException As SqlException
                Throw sqlException
            Catch ex As Exception
                Throw ex
            End Try


        End Sub

        Public Sub UpdateWithTransactionManager(ByVal price As Price)
            ' mark as loaded to prevent it loads from db
            price.MarkLoaded()
            ' set default result
            Dim result As Integer = -1
            Try
                ' add command to update price
                m_TransactionManager.AddUpdate(price, m_userPrincipal.Identity.Name)

            Catch ex As Exception
                Throw ex
            End Try

        End Sub
#End Region

    End Class
End Namespace

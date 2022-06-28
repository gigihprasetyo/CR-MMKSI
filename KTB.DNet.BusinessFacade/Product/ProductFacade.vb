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
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/3/2005 - 3:58:00 PM
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


Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionHandlingException
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging

#End Region

Namespace KTB.DNet.BusinessFacade.Product

    Public Class ProductFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_ProductMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private objTransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Try

                Me.m_userPrincipal = userPrincipal
                Me.m_ProductMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.Product).ToString)

                Me.objTransactionManager = New TransactionManager

                AddHandler objTransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

                Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.Product))
                Me.DomainTypeCollection.Add(GetType(ProductDetail))
                'Me.DomainTypeCollection.Add(GetType(BasicProduct))

            Catch ex As Exception

                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If

            End Try

        End Sub

#End Region

#Region "Get Public Method"

        Public Function GetProduct(ByVal ID As Integer) As KTB.DNet.Domain.Product
            Try
                Return CType(m_ProductMapper.Retrieve(ID), KTB.DNet.Domain.Product)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return Nothing
        End Function

        Public Function GetProduct(ByVal ProductCode As String) As KTB.DNet.Domain.Product
            Try
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Product), "ProductCode", MatchType.Exact, ProductCode))
                Dim poColl As ArrayList = m_ProductMapper.RetrieveByCriteria(criterias)
                If (poColl.Count > 0) Then
                    Return CType(poColl(0), KTB.DNet.Domain.Product)
                End If
                Return New KTB.DNet.Domain.Product
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return Nothing
        End Function

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As KTB.DNet.Domain.Product
            Return CType(m_ProductMapper.Retrieve(ID), KTB.DNet.Domain.Product)
        End Function

        Public Function Retrieve(ByVal Code As String) As KTB.DNet.Domain.Product
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Product), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Product), "ProductDetailCode", MatchType.Exact, Code))

            Dim ProductColl As ArrayList = m_ProductMapper.RetrieveByCriteria(criterias)
            If (ProductColl.Count > 0) Then
                Return CType(ProductColl(0), KTB.DNet.Domain.Product)
            End If
            Return New KTB.DNet.Domain.Product
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ProductMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ProductMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ProductMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.Product), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ProductMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.Product), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ProductMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Product), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _Product As ArrayList = m_ProductMapper.RetrieveByCriteria(criterias)
            Return _Product
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Product), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ProductColl As ArrayList = m_ProductMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ProductColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ProductColl As ArrayList = m_ProductMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ProductColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Product), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ProductColl As ArrayList = m_ProductMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Product), columnName, matchOperator, columnValue))

            Return ProductColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.Product), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Product), columnName, matchOperator, columnValue))

            Return m_ProductMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction Methods"

        '<AuthorizationPermission(Security.Permissions.SecurityAction.Demand, Context:="CreateProduct", ProviderName:="RuleProvider")> _
        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.Product) As Integer

            Dim returnValue As Integer = -1

            If (Me.IsTaskFree()) Then

                Try

                    Dim performTransaction As Boolean = True

                    objTransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    For i As Integer = 0 To objDomain.ProductDetail.Count - 1

                        'CType(objDomain.ProductDetail(i), ProductDetail).Product = objDomain
                        objTransactionManager.AddInsert(objDomain.ProductDetail(i), m_userPrincipal.Identity.Name)
                    Next

                    If performTransaction Then
                        objTransactionManager.PerformTransaction()
                        returnValue = 0
                    End If

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

        '<AuthorizationPermission(Security.Permissions.SecurityAction.Demand, Context:="CreateSalesOrder", ProviderName:="RuleProvider")> _
        Public Function Update(ByVal objDomain As KTB.DNet.Domain.Product) As Integer

            Dim returnValue As Integer = -1

            If Me.IsTaskFree Then

                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim oldPO As KTB.DNet.Domain.Product = GetProduct(objDomain.ProductCode)

                    '// if Product details' not modified
                    If (CType(objDomain.ProductDetail(objDomain.ProductDetail.Count - 1), ProductDetail).Product Is Nothing) Then

                        '// Product detail's is modified

                        Dim objTempOldPODetail As ProductDetail
                        Dim objTempNewPODetail As ProductDetail

                        '// update or create Product detail

                        For i As Integer = 0 To i < objDomain.ProductDetail.Count

                            '// update
                            If (oldPO.ProductDetail.Count > i) Then

                                objTempOldPODetail = CType(oldPO.ProductDetail(i), ProductDetail)
                                objTempNewPODetail = CType(objDomain.ProductDetail(i), ProductDetail)

                                'objTempOldPODetail.Product = objTempNewPODetail.Product
                                objTempOldPODetail.Description = objTempNewPODetail.Description
                                objTempOldPODetail.BasicProduct = objTempNewPODetail.BasicProduct

                                objTransactionManager.AddUpdate(objTempOldPODetail, m_userPrincipal.Identity.Name)

                            Else
                                'CType(objDomain.ProductDetail(i, ProductDetail.Product) = oldPO
                                objTransactionManager.AddInsert((CType(objDomain.ProductDetail(i), ProductDetail)), m_userPrincipal.Identity.Name)

                            End If

                        Next

                        '// delete Product details (if needed)
                        For i As Integer = objDomain.ProductDetail.Count To (i < oldPO.ProductDetail.Count) And (oldPO.ProductDetail.Count > objDomain.ProductDetail.Count)

                            '// set detail row status to deleted
                            objTempOldPODetail = CType(oldPO.ProductDetail(i), ProductDetail)
                            objTempOldPODetail.RowStatus = DBRowStatus.Deleted

                        Next

                    End If

                    '// update Product header data
                    CopySalesOrderData(oldPO, objDomain)

                    If (performTransaction) Then
                        '// update new sales order header
                        objTransactionManager.AddUpdate(oldPO, m_userPrincipal.Identity.Name)

                        objTransactionManager.PerformTransaction()
                        objDomain.ID = oldPO.ID
                        returnValue = 0
                    End If

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

        '<AuthorizationPermission(Security.Permissions.SecurityAction.Demand, Context:="DeleteSalesOrder", ProviderName:="RuleProvider")> _
        Public Function Delete(ByVal objDomain As KTB.DNet.Domain.Product) As Integer
            Return Delete(objDomain, objTransactionManager, True)
        End Function

        '<AuthorizationPermission(Security.Permissions.SecurityAction.Demand.Demand, Context:="DeleteSalesOrder", ProviderName:="RuleProvider")> _
        Public Function Delete(ByVal objDomain As KTB.DNet.Domain.Product, ByVal currTransManager As TransactionManager, ByVal doPerformTransaction As Boolean) As Integer

            Dim returnValue As Integer = -1

            If Me.IsTaskFree Then

                Try

                    Me.SetTaskLocking()
                    '// set SO Header row status to deleted
                    objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                    currTransManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    If doPerformTransaction Then
                        currTransManager.PerformTransaction()
                        returnValue = 0
                    End If

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

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.Product) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.Product).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.Product).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is ProductDetail) Then

                CType(InsertArg.DomainObject, ProductDetail).ID = InsertArg.ID

            End If

        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Try
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Product), "ProductCode", MatchType.Exact, Code))
                Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.Product), "ProductCode", AggregateType.Count)

                Return CType(m_ProductMapper.RetrieveScalar(agg, crit), Integer)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try

            Return Nothing
        End Function

#End Region

#Region "Helper Method"

        Private Sub CopySalesOrderData(ByVal target As KTB.DNet.Domain.Product, ByVal source As KTB.DNet.Domain.Product)

            Try

                target.CreatedBy = source.CreatedBy
                target.CreatedTime = source.CreatedTime
                target.Description = source.Description
                target.ID = source.ID
                target.LastUpdatedBy = source.LastUpdatedBy
                target.LastUpdatedTime = source.LastUpdatedTime
                target.LockedBy = source.LockedBy
                target.ProductCode = source.ProductCode
                target.RowStatus = source.RowStatus

            Catch ex As Exception

                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace
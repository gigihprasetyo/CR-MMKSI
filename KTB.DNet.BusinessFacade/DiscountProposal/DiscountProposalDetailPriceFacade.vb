
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
'// Generated on 19/06/2020 - 14:51:12
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
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class DiscountProposalDetailPriceFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DiscountProposalDetailPriceMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DiscountProposalDetailPriceMapper = MapperFactory.GetInstance.GetMapper(GetType(DiscountProposalDetailPrice).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(DiscountProposalHeader))
            Me.DomainTypeCollection.Add(GetType(DiscountProposalDetail))
            Me.DomainTypeCollection.Add(GetType(DiscountProposalDetailOwnership))
            Me.DomainTypeCollection.Add(GetType(DiscountProposalDetailPrice))
            Me.DomainTypeCollection.Add(GetType(DiscountProposalDetailDocument))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DiscountProposalDetailPrice
            Return CType(m_DiscountProposalDetailPriceMapper.Retrieve(ID), DiscountProposalDetailPrice)
        End Function

        Public Function Retrieve(ByVal Code As String) As DiscountProposalDetailPrice
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DiscountProposalDetailPrice), "DiscountProposalDetailPriceCode", MatchType.Exact, Code))

            Dim DiscountProposalDetailPriceColl As ArrayList = m_DiscountProposalDetailPriceMapper.RetrieveByCriteria(criterias)
            If (DiscountProposalDetailPriceColl.Count > 0) Then
                Return CType(DiscountProposalDetailPriceColl(0), DiscountProposalDetailPrice)
            End If
            Return New DiscountProposalDetailPrice
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DiscountProposalDetailPriceMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DiscountProposalDetailPriceMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DiscountProposalDetailPriceMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DiscountProposalDetailPrice), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DiscountProposalDetailPriceMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DiscountProposalDetailPrice), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DiscountProposalDetailPriceMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DiscountProposalDetailPrice As ArrayList = m_DiscountProposalDetailPriceMapper.RetrieveByCriteria(criterias)
            Return _DiscountProposalDetailPrice
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DiscountProposalDetailPriceColl As ArrayList = m_DiscountProposalDetailPriceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DiscountProposalDetailPriceColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(DiscountProposalDetailPrice), SortColumn, sortDirection))
            Dim DiscountProposalDetailPriceColl As ArrayList = m_DiscountProposalDetailPriceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DiscountProposalDetailPriceColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DiscountProposalDetailPriceColl As ArrayList = m_DiscountProposalDetailPriceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DiscountProposalDetailPriceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DiscountProposalDetailPriceColl As ArrayList = m_DiscountProposalDetailPriceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DiscountProposalDetailPrice), columnName, matchOperator, columnValue))
            Return DiscountProposalDetailPriceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DiscountProposalDetailPrice), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailPrice), columnName, matchOperator, columnValue))

            Return m_DiscountProposalDetailPriceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailPrice), "DiscountProposalDetailPriceCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(DiscountProposalDetailPrice), "DiscountProposalDetailPriceCode", AggregateType.Count)
            Return CType(m_DiscountProposalDetailPriceMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As DiscountProposalDetailPrice) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DiscountProposalDetailPriceMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DiscountProposalDetailPrice) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DiscountProposalDetailPriceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DiscountProposalDetailPrice)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DiscountProposalDetailPriceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DiscountProposalDetailPrice)
            Try
                m_DiscountProposalDetailPriceMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is DiscountProposalHeader) Then
                CType(InsertArg.DomainObject, DiscountProposalHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, DiscountProposalHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is DiscountProposalDetail) Then
                CType(InsertArg.DomainObject, DiscountProposalDetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function InsertTransaction(ByVal objDiscountProposalHeader As DiscountProposalHeader, ByVal arrDiscountProposalDetailPrice As ArrayList, _
                                          ByVal arrDiscountProposalDetailApprovaltoSPL As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If Not IsNothing(objDiscountProposalHeader.DiscountProposalDetails) Then
                        If objDiscountProposalHeader.DiscountProposalDetails.Count > 0 Then
                            For Each oDiscountProposalDetail As DiscountProposalDetail In objDiscountProposalHeader.DiscountProposalDetails
                                If Not IsNothing(arrDiscountProposalDetailPrice) Then
                                    If arrDiscountProposalDetailPrice.Count > 0 Then
                                        For Each oDiscountProposalDetailPrice As DiscountProposalDetailPrice In arrDiscountProposalDetailPrice
                                            If oDiscountProposalDetail.SubCategoryVehicle.ID = oDiscountProposalDetailPrice.SubCategoryVehicleID Then
                                                If oDiscountProposalDetail.VechileColorIsActiveOnPK.VechileColor.ID = oDiscountProposalDetailPrice.VechileColorID Then
                                                    oDiscountProposalDetailPrice.DiscountProposalHeader = objDiscountProposalHeader
                                                    oDiscountProposalDetailPrice.DiscountProposalDetail = oDiscountProposalDetail
                                                    If oDiscountProposalDetailPrice.ID = 0 Then
                                                        m_TransactionManager.AddInsert(oDiscountProposalDetailPrice, m_userPrincipal.Identity.Name)
                                                    Else
                                                        m_TransactionManager.AddUpdate(oDiscountProposalDetailPrice, m_userPrincipal.Identity.Name)
                                                    End If
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(objDiscountProposalHeader.DiscountProposalDetailApprovals) Then
                        If objDiscountProposalHeader.DiscountProposalDetailApprovals.Count > 0 Then
                            For Each oDiscountProposalDetailApproval As DiscountProposalDetailApproval In objDiscountProposalHeader.DiscountProposalDetailApprovals
                                If Not IsNothing(arrDiscountProposalDetailApprovaltoSPL) Then
                                    If arrDiscountProposalDetailApprovaltoSPL.Count > 0 Then
                                        For Each oDiscountProposalDetailApprovaltoSPL As DiscountProposalDetailApprovaltoSPL In arrDiscountProposalDetailApprovaltoSPL
                                            If oDiscountProposalDetailApproval.ModelID = oDiscountProposalDetailApprovaltoSPL.ModelID Then
                                                If oDiscountProposalDetailApproval.VechileType.ID = oDiscountProposalDetailApprovaltoSPL.VechileTypeID Then
                                                    oDiscountProposalDetailApprovaltoSPL.DiscountProposalDetailApproval = oDiscountProposalDetailApproval
                                                    If oDiscountProposalDetailApprovaltoSPL.ID = 0 Then
                                                        m_TransactionManager.AddInsert(oDiscountProposalDetailApprovaltoSPL, m_userPrincipal.Identity.Name)
                                                    Else
                                                        m_TransactionManager.AddUpdate(oDiscountProposalDetailApprovaltoSPL, m_userPrincipal.Identity.Name)
                                                    End If
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = objDiscountProposalHeader.ID

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

        Function UpdateTransaction(objDiscountProposalHeader As DiscountProposalHeader, arlDiscountProposalDetailPrice As ArrayList, arlDelDiscountProposalDetailPrice As ArrayList, _
                                   arlDiscountProposalDetailApprovaltoSPL As ArrayList, arlDelDiscountProposalDetailApprovaltoSPL As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not IsNothing(arlDelDiscountProposalDetailPrice) Then
                        If arlDelDiscountProposalDetailPrice.Count > 0 Then
                            For Each item As DiscountProposalDetailPrice In arlDelDiscountProposalDetailPrice
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(objDiscountProposalHeader.DiscountProposalDetails) Then
                        If objDiscountProposalHeader.DiscountProposalDetails.Count > 0 Then
                            For Each oDiscountProposalDetail As DiscountProposalDetail In objDiscountProposalHeader.DiscountProposalDetails
                                If Not IsNothing(arlDiscountProposalDetailPrice) Then
                                    If arlDiscountProposalDetailPrice.Count > 0 Then
                                        For Each oDiscountProposalDetailPrice As DiscountProposalDetailPrice In arlDiscountProposalDetailPrice
                                            If oDiscountProposalDetail.SubCategoryVehicle.ID = oDiscountProposalDetailPrice.SubCategoryVehicleID Then
                                                If oDiscountProposalDetail.VechileColorIsActiveOnPK.VechileColor.ID = oDiscountProposalDetailPrice.VechileColorID Then
                                                    oDiscountProposalDetailPrice.DiscountProposalHeader = objDiscountProposalHeader
                                                    oDiscountProposalDetailPrice.DiscountProposalDetail = oDiscountProposalDetail
                                                    If oDiscountProposalDetailPrice.ID <> 0 Then
                                                        m_TransactionManager.AddUpdate(oDiscountProposalDetailPrice, m_userPrincipal.Identity.Name)
                                                    Else
                                                        m_TransactionManager.AddInsert(oDiscountProposalDetailPrice, m_userPrincipal.Identity.Name)
                                                    End If
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDelDiscountProposalDetailApprovaltoSPL) Then
                        If arlDelDiscountProposalDetailApprovaltoSPL.Count > 0 Then
                            For Each item As DiscountProposalDetailApprovaltoSPL In arlDelDiscountProposalDetailApprovaltoSPL
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(objDiscountProposalHeader.DiscountProposalDetailApprovals) Then
                        If objDiscountProposalHeader.DiscountProposalDetailApprovals.Count > 0 Then
                            For Each oDiscountProposalDetailApproval As DiscountProposalDetailApproval In objDiscountProposalHeader.DiscountProposalDetailApprovals
                                If Not IsNothing(arlDiscountProposalDetailApprovaltoSPL) Then
                                    If arlDiscountProposalDetailApprovaltoSPL.Count > 0 Then
                                        For Each oDiscountProposalDetailApprovaltoSPL As DiscountProposalDetailApprovaltoSPL In arlDiscountProposalDetailApprovaltoSPL
                                            If oDiscountProposalDetailApproval.ModelID = oDiscountProposalDetailApprovaltoSPL.ModelID Then
                                                If oDiscountProposalDetailApproval.VechileType.ID = oDiscountProposalDetailApprovaltoSPL.VechileTypeID Then
                                                    oDiscountProposalDetailApprovaltoSPL.DiscountProposalDetailApproval = oDiscountProposalDetailApproval
                                                    If oDiscountProposalDetailApprovaltoSPL.ID <> 0 Then
                                                        m_TransactionManager.AddUpdate(oDiscountProposalDetailApprovaltoSPL, m_userPrincipal.Identity.Name)
                                                    Else
                                                        m_TransactionManager.AddInsert(oDiscountProposalDetailApprovaltoSPL, m_userPrincipal.Identity.Name)
                                                    End If
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDiscountProposalHeader.ID
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

#End Region

    End Class

End Namespace


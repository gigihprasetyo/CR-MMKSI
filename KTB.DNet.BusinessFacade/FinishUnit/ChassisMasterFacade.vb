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
'// Author Name   : Agus Soepriadi
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 10/10/2005 - 10:53:00 AM
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
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Collections.Generic

#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class ChassisMasterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_ChassisMasterMapper As IMapper
        Private m_V_CMHelperMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_EndCustomerMapper As IMapper
        Private m_CustomerMapper As IMapper
        Private m_CustomerRequestMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_ChassisMasterMapper = MapperFactory.GetInstance().GetMapper(GetType(ChassisMaster).ToString)
            Me.m_V_CMHelperMapper = MapperFactory.GetInstance().GetMapper(GetType(V_CMHelper).ToString)
            Me.m_EndCustomerMapper = MapperFactory.GetInstance().GetMapper(GetType(EndCustomer).ToString)
            Me.m_CustomerMapper = MapperFactory.GetInstance().GetMapper(GetType(Customer).ToString)
            Me.m_CustomerRequestMapper = MapperFactory.GetInstance().GetMapper(GetType(CustomerRequest).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManagerInsert)
            Me.DomainTypeCollection.Add(GetType(ChassisMaster))
            Me.DomainTypeCollection.Add(GetType(EndCustomer))
            Me.DomainTypeCollection.Add(GetType(Customer))
        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrieveScalar(ByVal crit As CriteriaComposite, ByVal aggr As Aggregate) As Integer
            Return CType(m_ChassisMasterMapper.RetrieveScalar(aggr, crit), Integer)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As ChassisMaster
            Return CType(m_ChassisMasterMapper.Retrieve(ID), ChassisMaster)
        End Function

        Public Function RetrieveCmHelper(ByVal ID As Integer) As V_CMHelper
            Return CType(m_V_CMHelperMapper.Retrieve(ID), V_CMHelper)
        End Function

        Public Function Retrieve(ByVal Code As String) As ChassisMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, Code))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ChassisMasterColl As ArrayList = m_ChassisMasterMapper.RetrieveByCriteria(criterias)
            If (ChassisMasterColl.Count > 0) Then
                Return CType(ChassisMasterColl(0), ChassisMaster)
            End If
            Return New ChassisMaster
        End Function

        Public Function RetrieveByChassisAndEngine(ByVal ChassisNumber As String, ByVal EngineNumber As String) As ChassisMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, ChassisNumber))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EngineNumber", MatchType.Exact, EngineNumber))
            Dim ChassisMasterColl As ArrayList = m_ChassisMasterMapper.RetrieveByCriteria(criterias)
            If (ChassisMasterColl.Count > 0) Then
                Return CType(ChassisMasterColl(0), ChassisMaster)
            End If
            Return New ChassisMaster
        End Function

        Public Function RetrieveByCompany(ByVal Code As String, ByVal CompanyCode As String) As ChassisMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, Code))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "Category.ProductCategory.Code", MatchType.Exact, CompanyCode))
            Dim ChassisMasterColl As ArrayList = m_ChassisMasterMapper.RetrieveByCriteria(criterias)
            If (ChassisMasterColl.Count > 0) Then
                Return CType(ChassisMasterColl(0), ChassisMaster)
            End If
            Return New ChassisMaster
        End Function

        Public Function Retrieve(ByVal nTypeID As Integer, ByVal sCode As DateTime) As ChassisMaster
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "VechileColor.ID", MatchType.Exact, nTypeID))
            crit.opAnd(New Criteria(GetType(ChassisMaster), "ValidFrom", MatchType.Exact, sCode))

            Dim ChassisMasterColl As ArrayList = m_ChassisMasterMapper.RetrieveByCriteria(crit)
            If (ChassisMasterColl.Count > 0) Then
                Return CType(ChassisMasterColl(0), ChassisMaster)
            End If
            Return New ChassisMaster
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ChassisMasterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ChassisMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ChassisMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ChassisMasterMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveByChassisNumbers(ByVal _arrID As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, _arrID))

            Dim ChassisMasterColl As ArrayList = m_ChassisMasterMapper.RetrieveByCriteria(criterias)

            Return ChassisMasterColl
        End Function
        Public Function RetrieveList() As ArrayList
            Return m_ChassisMasterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ChassisMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ChassisMasterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ChassisMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ChassisMasterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ChassisMaster As ArrayList = m_ChassisMasterMapper.RetrieveByCriteria(criterias)
            Return _ChassisMaster
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ChassisMasterColl As ArrayList = m_ChassisMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ChassisMasterColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
       ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal criterias As CriteriaComposite) As ArrayList

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ChassisMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ChassisMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ChassisMasterColl As ArrayList = m_ChassisMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ChassisMasterColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim ChassisMasterColl As ArrayList = m_ChassisMasterMapper.RetrieveByCriteria(criterias)

            Return ChassisMasterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(ChassisMaster), sortColumn, sortDirection))
            Else
                'sortColl = Nothing
                sortColl.Add(New Sort(GetType(ChassisMaster), "ID", Sort.SortDirection.DESC))

            End If

            Dim ChassisMasterColl As ArrayList = m_ChassisMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ChassisMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), columnName, matchOperator, columnValue))
            Dim ChassisMasterColl As ArrayList = m_ChassisMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ChassisMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ChassisMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), columnName, matchOperator, columnValue))

            Return m_ChassisMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Private Sub m_TransactionManagerInsert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is EndCustomer) Then
                CType(InsertArg.DomainObject, EndCustomer).ID = InsertArg.ID
                CType(InsertArg.DomainObject, EndCustomer).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is ChassisMasterProfile) Then
                CType(InsertArg.DomainObject, ChassisMasterProfile).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.ChassisMasterProfile).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is ChassisMasterProfileHistory) Then
                CType(InsertArg.DomainObject, ChassisMasterProfileHistory).ID = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is Customer) Then
                CType(InsertArg.DomainObject, Customer).ID = InsertArg.ID
            End If
        End Sub

        Public Function ExecuteSPChassisMaster(ByVal chassisMaster As String, ByVal pendingDesc As String) As Boolean
            Dim sp = String.Format(" exec up_UpdateChassisMasterField @ChassisNumber='{0}', @PendingDesc='{1}',@LastUpdateBy='{2}'", chassisMaster, pendingDesc, m_userPrincipal.Identity.Name)
            Return Me.m_ChassisMasterMapper.ExecuteSP(sp)
        End Function

        Public Function ExecuteSPChassisMasterProfile(ByVal chassisNumber As String, ByVal pendingDesc As String, ByVal lastupdateProfile As DateTime) As Boolean
            Dim sp = String.Format(" exec up_UpdateChassisMasterField @ChassisNumber = '{0}',@PendingDesc = '{1}', @Lastupdateprofile='{2}',@LastUpdateBy='{3}'", chassisNumber, pendingDesc, lastupdateProfile.ToString("yyyy-MM-dd HH:mm:ss"), m_userPrincipal.Identity.Name)
            Return Me.m_ChassisMasterMapper.ExecuteSP(sp)
        End Function

        Public Function Insert(ByVal objDomain As ChassisMaster) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_ChassisMasterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As ChassisMaster) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ChassisMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As ChassisMaster)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = objDomain.RowStatus

                m_ChassisMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteDB(ByVal objDomain As ChassisMaster) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = objDomain.RowStatus
                m_ChassisMasterMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub DeleteFromDB(ByVal objDomain As ChassisMaster)
            Try
                m_ChassisMasterMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(ChassisMaster), "ChassisNumber", AggregateType.Count)

            Return CType(m_ChassisMasterMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function UpdateTransaction(ByVal objDomainColl As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    For Each item As ChassisMaster In objDomainColl
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        If Not IsNothing(item.EndCustomer) Then
                            m_TransactionManager.AddUpdate(item.EndCustomer, m_userPrincipal.Identity.Name)
                        End If
                    Next

                    m_TransactionManager.PerformTransaction()
                    returnValue = 1

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



        Public Function SynchronizeSAPToDNET(ByVal objChassisMaster As ChassisMaster) As Integer
            If (Me.IsTaskFree() Or 1 = 1) Then
                Try
                    Me.SetTaskLocking()
                    Dim orgCus As Customer = objChassisMaster.EndCustomer.OriginalCustomer
                    Dim ischange As New IsChangeFacade
                    If Not orgCus Is Nothing Then
                        If orgCus.ID > 0 Then
                            If ischange.IsChangeCustomer(orgCus) Then
                                m_TransactionManager.AddUpdate(orgCus, m_userPrincipal.Identity.Name)
                            End If

                            orgCus.MarkLoaded()
                            objChassisMaster.EndCustomer.Customer = orgCus
                        Else
                            Dim objCustDB As Customer = GetCustomer(orgCus.Code)
                            If objCustDB.ID > 0 Then
                                orgCus = objCustDB
                            Else
                                m_TransactionManager.AddInsert(orgCus, m_userPrincipal.Identity.Name)
                                orgCus.MarkLoaded()
                            End If
                            objChassisMaster.EndCustomer.Customer = orgCus
                        End If
                    Else
                        If ischange.IsChangeCustomer(objChassisMaster.EndCustomer.Customer) Then
                            m_TransactionManager.AddUpdate(objChassisMaster.EndCustomer.Customer, m_userPrincipal.Identity.Name)
                        End If

                    End If

                    If objChassisMaster.EndCustomer.ID = 0 Then
                        objChassisMaster.EndCustomer.Name1 = objChassisMaster.EndCustomer.Customer.Name1
                        m_TransactionManager.AddInsert(objChassisMaster.EndCustomer, m_userPrincipal.Identity.Name)
                    Else
                        objChassisMaster.EndCustomer.Name1 = objChassisMaster.EndCustomer.Customer.Name1
                        If ischange.IsChangeEndCustomer(objChassisMaster.EndCustomer) Then
                            m_TransactionManager.AddUpdate(objChassisMaster.EndCustomer, m_userPrincipal.Identity.Name)
                        End If

                    End If
                    objChassisMaster.FakturStatus = CType(CType(EnumChassisMaster.FakturStatus.Selesai, Short), String)
                    objChassisMaster.AlreadySaled = 2
                    If ischange.IsChangeChassisMaster(objChassisMaster) Then
                        m_TransactionManager.AddUpdate(objChassisMaster, m_userPrincipal.Identity.Name & "Step3")
                    End If

                    If Not IsNothing(objChassisMaster.EndCustomer) AndAlso Not IsNothing(objChassisMaster.EndCustomer.Customer) AndAlso Not IsNothing(objChassisMaster.EndCustomer.Customer.MyCustomerRequest) AndAlso objChassisMaster.EndCustomer.Customer.MyCustomerRequest.ID > 0 Then
                        Dim vUpdate As Boolean = False
                        If objChassisMaster.EndCustomer.Customer.MyCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP Then
                            objChassisMaster.EndCustomer.Customer.MyCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.VerifiedMCP
                            vUpdate = True
                            'm_TransactionManager.AddUpdate(objChassisMaster.EndCustomer.Customer.MyCustomerRequest, m_userPrincipal.Identity.Name)
                        End If

                        If objChassisMaster.EndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP Then
                            objChassisMaster.EndCustomer.Customer.MyCustomerRequest.LKPPStatus = EnumLKPPStatus.LKPPStatus.VerifiedLKPP
                            vUpdate = True
                            'm_TransactionManager.AddUpdate(objChassisMaster.EndCustomer.Customer.MyCustomerRequest, m_userPrincipal.Identity.Name)
                        End If

                        If vUpdate Then
                            If ischange.ISchangeCustomerRequest(objChassisMaster.EndCustomer.Customer.MyCustomerRequest) Then
                                m_TransactionManager.AddUpdate(objChassisMaster.EndCustomer.Customer.MyCustomerRequest, m_userPrincipal.Identity.Name)
                            End If

                        End If

                    End If
                    m_TransactionManager.PerformTransaction()
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
        End Function

        Public Function AddCustomerToChassisMaster(ByVal objEndCustomer As EndCustomer, ByVal objChassisMaster As ChassisMaster) As Integer
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objEndCustomer, m_userPrincipal.Identity.Name)
                    objChassisMaster.EndCustomer = objEndCustomer
                    objChassisMaster.FakturStatus = "0"
                    m_TransactionManager.AddUpdate(objChassisMaster, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    objEndCustomer.ID = 0
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If

            Return objEndCustomer.ID
        End Function

        Public Function IsAreaViolationFree(ByVal objChassisMaster As ChassisMaster) As Boolean
            Dim returnVal As Boolean = False
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerTerritory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(DealerTerritory), "Dealer.ID", MatchType.Exact, objChassisMaster.Dealer.ID))
            crit.opAnd(New Criteria(GetType(DealerTerritory), "City.ID", MatchType.Exact, objChassisMaster.EndCustomer.Customer.City.ID))
            Dim aggr As Aggregate = New Aggregate(GetType(DealerTerritory), "ID", AggregateType.Count)
            Dim fac As DealerTerritoryFacade = New DealerTerritoryFacade(m_userPrincipal)
            returnVal = (fac.RetrieveScalar(aggr, crit) = 1)
            Return returnVal
        End Function

        Public Function ValidateInvoice(ByVal objChassisMaster As ChassisMaster) As Integer

            Me.DomainTypeCollection.Add(GetType(ChassisMaster))
            Me.DomainTypeCollection.Add(GetType(EndCustomer))
            Dim iReturnValue As Integer = 0
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddUpdate(objChassisMaster, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddUpdate(objChassisMaster.EndCustomer, m_userPrincipal.Identity.Name)
                    Dim objCust As Customer = objChassisMaster.EndCustomer.Customer
                    If Not objCust Is Nothing Then
                        objCust.DeletionMark = 1
                        m_TransactionManager.AddUpdate(objCust, m_userPrincipal.Identity.Name)
                    End If
                    m_TransactionManager.PerformTransaction()
                    iReturnValue = 2

                Catch ex As Exception
                    iReturnValue = -1
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return iReturnValue

        End Function

        Public Function CancelInvoiceValidation(ByVal objChassisMaster As ChassisMaster) As Integer
            Me.DomainTypeCollection.Add(GetType(ChassisMaster))
            Me.DomainTypeCollection.Add(GetType(EndCustomer))
            Dim iReturnValue As Integer = 0
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddUpdate(objChassisMaster, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddUpdate(objChassisMaster.EndCustomer, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
                    iReturnValue = 2

                Catch ex As Exception
                    iReturnValue = -1
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return iReturnValue
        End Function

        Public Function GetChassisMasterProfile(ByVal obj As ChassisMaster, ByVal objGroup As ProfileGroup, ByVal objDomain As ProfileHeader) As ChassisMasterProfile
            Dim objFacade As Profile.ChassisMasterProfileFacade = New Profile.ChassisMasterProfileFacade(System.Threading.Thread.CurrentPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, obj.ID))
            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileHeader.ID", MatchType.Exact, objDomain.ID))
            Dim objList As ArrayList = objFacade.Retrieve(criterias)
            If objList.Count > 0 Then
                Return CType(objList(0), ChassisMasterProfile)
            End If
            Return New ChassisMasterProfile
        End Function


        Public Function GetChassisMasterProfile(ByVal obj As ChassisMaster, ByVal profileHeaderID As Integer) As ChassisMasterProfile
            Dim objFacade As Profile.ChassisMasterProfileFacade = New Profile.ChassisMasterProfileFacade(System.Threading.Thread.CurrentPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, obj.ID))
            'criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileGroup.ID", MatchType.Exact, profileGroupID))
            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileHeader.ID", MatchType.Exact, profileHeaderID))
            Dim objList As ArrayList = objFacade.Retrieve(criterias)
            If objList.Count > 0 Then
                Return CType(objList(0), ChassisMasterProfile)
            End If
            Return New ChassisMasterProfile
        End Function


        Public Function GetCustomer(ByVal code As String) As Customer
            Dim objFacade As CustomerFacade = New CustomerFacade(System.Threading.Thread.CurrentPrincipal)
            Dim objCust As Customer = objFacade.RetrieveByCode(code)
            Return objCust
        End Function

        Public Function InsertUpdateChassisMasterProfile(ByVal objDomain As KTB.DNet.Domain.ChassisMaster, ByVal objListProfile As ArrayList, ByVal objGroup As ProfileGroup) As Integer
            Dim returnValue As Integer = -1
            Dim isProfileChange As Boolean
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    isProfileChange = False
                    For Each item As ChassisMasterProfile In objListProfile
                        item.ChassisMaster = objDomain
                        Dim oldProfile As ChassisMasterProfile = GetChassisMasterProfile(objDomain, objGroup, item.ProfileHeader)
                        If oldProfile.ID > 0 Then
                            For Each items As ChassisMasterProfileHistory In item.ChassisMasterProfileHistorys
                                items.ChassisMasterProfile = oldProfile
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next

                            If oldProfile.ProfileValue <> item.ProfileValue Then
                                isProfileChange = True
                            End If

                            oldProfile.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile, m_userPrincipal.Identity.Name)
                        Else
                            item.ChassisMaster = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            For Each items As ChassisMasterProfileHistory In item.ChassisMasterProfileHistorys
                                items.ChassisMasterProfile = item
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                            oldProfile.ProfileValue = item.ProfileValue
                            isProfileChange = True
                        End If

                    Next

                    If isProfileChange Then
                        objDomain.LastUpdateProfile = Now
                    End If

                    m_TransactionManager.AddUpdate(objDomain.EndCustomer, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddUpdate(objDomain.EndCustomer.Customer, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    'tambahan
                    If Not IsNothing(objDomain.EndCustomer.Customer.MyCustomerRequest) Then
                        m_TransactionManager.AddUpdate(objDomain.EndCustomer.Customer.MyCustomerRequest, m_userPrincipal.Identity.Name)
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
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
#End Region

#Region "Custom Method"

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ChassisMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim TrCourseColl As ArrayList = m_ChassisMasterMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrCourseColl
        End Function

        Public Function RetrieveActiveListByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If Not IsNothing(sortColumn) And sortColumn <> "" Then
                sortColl.Add(New Sort(GetType(ChassisMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ChassisMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ChassisMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function IsExist(ByVal ChassisNumber As String) As Boolean
            Dim bReturnValue As Boolean = False
            Dim objCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            objCriteria.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, ChassisNumber))

            Dim objAggregate As Aggregate = New Aggregate(GetType(ChassisMaster), "ID", AggregateType.Count)

            bReturnValue = (m_ChassisMasterMapper.RetrieveScalar(objAggregate, objCriteria) = 1)

            Return bReturnValue
        End Function
        Public Function IsExistByChassisAndEngineNumber(ByVal ChassisNumber As String, ByVal EngineNumber As String) As Boolean
            Dim bReturnValue As Boolean = False
            Dim objCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            objCriteria.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, ChassisNumber))
            objCriteria.opAnd(New Criteria(GetType(ChassisMaster), "EngineNumber", MatchType.Exact, EngineNumber))

            Dim objAggregate As Aggregate = New Aggregate(GetType(ChassisMaster), "ID", AggregateType.Count)

            bReturnValue = (m_ChassisMasterMapper.RetrieveScalar(objAggregate, objCriteria) = 1)

            Return bReturnValue
        End Function

        Public Function RetrieveByField(ByVal Field As String, ByVal Code As String) As ChassisMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), Field, MatchType.Exact, Code))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ChassisMasterColl As ArrayList = m_ChassisMasterMapper.RetrieveByCriteria(criterias)
            If (ChassisMasterColl.Count > 0) Then
                Return CType(ChassisMasterColl(0), ChassisMaster)
            End If
            Return New ChassisMaster
        End Function

        Public Function RetrieveChassisFromSP(ByVal strChassisNumber As String, ByVal strEngineNumber As String) As ArrayList
            Dim SQL As String

            SQL = "exec sp_retrieveChassisByDSF '" & strChassisNumber & "', '" & strEngineNumber & "'"

            Return m_ChassisMasterMapper.RetrieveSP(SQL)
        End Function

        Public Function ValidasiPengajuanFaktur(ByVal dealerID As Integer, ByVal chassisNumber As String) As Dictionary(Of String, String)
            Dim rest As New Dictionary(Of String, String)
            Dim arrParam As ArrayList = New ArrayList()
            Dim param1 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@DealerID", dealerID)
            arrParam.Add(param1)
            Dim param2 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@ChassisNumber", chassisNumber)
            arrParam.Add(param2)

            Dim dataTable As DataTable = m_ChassisMasterMapper.RetrieveDataSet("up_ValidatePengajuanFaktur", arrParam).Tables(0)
            For Each dr As DataRow In dataTable.Rows
                rest.Add(dr("ChassisNumber").ToString, dr("DealerCode").ToString)
            Next
            Return rest
        End Function
#End Region

    End Class
End Namespace

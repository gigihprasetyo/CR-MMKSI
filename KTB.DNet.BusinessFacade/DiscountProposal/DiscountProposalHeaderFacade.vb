
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
'// Generated on 19/06/2020 - 14:48:57
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
Imports System.Linq
Imports System.Reflection

#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class DiscountProposalHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DiscountProposalHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DiscountProposalHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(DiscountProposalHeader).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(StatusChangeHistory))
            Me.DomainTypeCollection.Add(GetType(StatusChangeHistorySendEmailFlag))
            Me.DomainTypeCollection.Add(GetType(FleetCustomerHeader))
            Me.DomainTypeCollection.Add(GetType(FleetCustomerDetail))
            Me.DomainTypeCollection.Add(GetType(DiscountProposalHeader))
            Me.DomainTypeCollection.Add(GetType(DiscountProposalDetailOwnership))
            Me.DomainTypeCollection.Add(GetType(DiscountProposalDetail))
            Me.DomainTypeCollection.Add(GetType(DiscountProposalDetailPrice))
            Me.DomainTypeCollection.Add(GetType(DiscountProposalDetailDocument))
            Me.DomainTypeCollection.Add(GetType(DiscountProposalDetailCustomer))
            Me.DomainTypeCollection.Add(GetType(DiscountProposalDetailApproval))
            Me.DomainTypeCollection.Add(GetType(DiscountProposalDetailApprovaltoSPL))
            Me.DomainTypeCollection.Add(GetType(DiscountProposalPricetoParameter))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DiscountProposalHeader
            Return CType(m_DiscountProposalHeaderMapper.Retrieve(ID), DiscountProposalHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As DiscountProposalHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DiscountProposalHeader), "DiscountProposalHeaderCode", MatchType.Exact, Code))

            Dim DiscountProposalHeaderColl As ArrayList = m_DiscountProposalHeaderMapper.RetrieveByCriteria(criterias)
            If (DiscountProposalHeaderColl.Count > 0) Then
                Return CType(DiscountProposalHeaderColl(0), DiscountProposalHeader)
            End If
            Return New DiscountProposalHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DiscountProposalHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DiscountProposalHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DiscountProposalHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DiscountProposalHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DiscountProposalHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DiscountProposalHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DiscountProposalHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DiscountProposalHeader As ArrayList = m_DiscountProposalHeaderMapper.RetrieveByCriteria(criterias)
            Return _DiscountProposalHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DiscountProposalHeaderColl As ArrayList = m_DiscountProposalHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DiscountProposalHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(DiscountProposalHeader), SortColumn, sortDirection))
            Dim DiscountProposalHeaderColl As ArrayList = m_DiscountProposalHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DiscountProposalHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DiscountProposalHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_DiscountProposalHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DiscountProposalHeaderColl As ArrayList = m_DiscountProposalHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DiscountProposalHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DiscountProposalHeaderColl As ArrayList = m_DiscountProposalHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DiscountProposalHeader), columnName, matchOperator, columnValue))
            Return DiscountProposalHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DiscountProposalHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalHeader), columnName, matchOperator, columnValue))

            Return m_DiscountProposalHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveDownloadDataLine(ByRef id As Integer) As ArrayList
            Dim result As ArrayList = New ArrayList
            Dim dtbs As New DataSet
            Dim strCommand As String = ""

            strCommand = "select " _
& "a.Status [Status], " _
& "a.SubmitDate [Tgl Pengajuan Dealer], " _
& "a.DealerProposalNo [No Aplikasi Dealer], " _
& "a.proposalregno [No Reg Aplikasi], " _
& "i.DealerCode [Dealer], " _
& "i.SearchTerm1 [Term 1], " _
& "j.FleetCustomerName [Nama Customer], " _
& "g.name [Model], " _
& "h.name [Tipe], " _
& "f.ColorIndName [Warna], " _
& "d.AssyYear [Assy Year], " _
& "d.proposeQty [Unit], " _
& "SUM(e.DiscountRequest) [Permohonan Diskon], " _
& "(SELECT SUM(DiscountApproved)  " _
& "FROM DiscountProposalDetailApprovaltoSPL x " _
& "join DiscountProposalDetailApproval y on y.ID = x.discountProposalDetailApprovalID " _
& "and y.DiscountProposalHeaderID = a.ID and f.VechileTypeID = y.VechileTypeID and y.rowstatus = 0 " _
& "WHERE x.RowStatus = 0 " _
& "GROUP BY y.DiscountProposalHeaderID, y.VechileTypeID " _
& ") AS [Diskon Disetujui] " _
& "FROM DiscountProposalHeader a " _
& "LEFT JOIN Dealer b on a.DealerID = b.ID and b.rowstatus = 0 " _
& "LEFT JOIN FleetCustomerDetail c on c.ID = a.FleetCustomerDetailID and c.rowstatus = 0 " _
& "LEFT JOIN DiscountProposalDetail d on d.DiscountProposalHeaderID = a.ID and d.rowstatus = 0 " _
& "LEFT JOIN DiscountPRoposalDetailPrice e on e.DiscountProposalDetailID = d.ID and e.rowstatus = 0 " _
& "left join VechileColorIsActiveOnPK g0 on g0.ID = d.VechileColorIsActiveOnPKID and g0.rowstatus = 0 " _
& "LEFT JOIN VechileColor f on g0.VehicleColorID = f.ID and f.rowstatus = 0 " _
& "LEFT JOIN SubCategoryVehicle g on g.ID = d.SubCategoryVehicleID and g.rowstatus = 0 " _
& "LEFT JOIN VechileTypeGeneral h on h.ID = g0.VechileTypeGeneralID and h.rowstatus = 0 " _
& "LEFT JOIN Dealer i on a.DealerID = i.ID and i.RowStatus = 0 " _
& "LEFT JOIN FleetCustomerHeader j on c.FleetCustomerHeaderID = j.ID and j.RowStatus = 0 " _
& "WHERE  " _
& "a.rowstatus = 0 " _
& "and a.ID = " & id & " " _
& "GROUP by " _
& "a.Status, " _
& "a.SubmitDate, " _
& "a.DealerProposalNo, " _
& "a.proposalregno, " _
& "i.DealerCode, " _
& "i.SearchTerm1, " _
& "j.FleetCustomerName, " _
& "a.ID, " _
& "a.proposalregno, " _
& "g.name, " _
& "h.name, " _
& "f.ColorIndName, " _
& "f.VechileTypeID, " _
& "d.AssyYear, " _
& "d.proposeQty "

            dtbs = m_DiscountProposalHeaderMapper.RetrieveDataSet(strCommand)
            For Each dr As DataRow In dtbs.Tables(0).Rows
                result.Add(dr)
            Next
            Return result
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalHeader), "DiscountProposalHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(DiscountProposalHeader), "DiscountProposalHeaderCode", AggregateType.Count)
            Return CType(m_DiscountProposalHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As DiscountProposalHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DiscountProposalHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DiscountProposalHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DiscountProposalHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DiscountProposalHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DiscountProposalHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DiscountProposalHeader)
            Try
                m_DiscountProposalHeaderMapper.Delete(objDomain)
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
            If (TypeOf InsertArg.DomainObject Is FleetCustomerHeader) Then
                CType(InsertArg.DomainObject, FleetCustomerHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, FleetCustomerHeader).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is FleetCustomerDetail) Then
                CType(InsertArg.DomainObject, FleetCustomerDetail).ID = InsertArg.ID
            End If

            If (TypeOf InsertArg.DomainObject Is FleetCustomerDetail) Then
                CType(InsertArg.DomainObject, FleetCustomerDetail).ID = InsertArg.ID
                CType(InsertArg.DomainObject, FleetCustomerDetail).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is DiscountProposalHeader) Then
                CType(InsertArg.DomainObject, DiscountProposalHeader).ID = InsertArg.ID
            End If

            If (TypeOf InsertArg.DomainObject Is DiscountProposalHeader) Then
                CType(InsertArg.DomainObject, DiscountProposalHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, DiscountProposalHeader).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is DiscountProposalDetail) Then
                CType(InsertArg.DomainObject, DiscountProposalDetail).ID = InsertArg.ID
            End If

            If (TypeOf InsertArg.DomainObject Is DiscountProposalDetail) Then
                CType(InsertArg.DomainObject, DiscountProposalDetail).ID = InsertArg.ID
                CType(InsertArg.DomainObject, DiscountProposalDetail).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is DiscountProposalDetailPrice) Then
                CType(InsertArg.DomainObject, DiscountProposalDetailPrice).ID = InsertArg.ID
            End If

            If (TypeOf InsertArg.DomainObject Is StatusChangeHistory) Then
                CType(InsertArg.DomainObject, StatusChangeHistory).id = InsertArg.ID
                CType(InsertArg.DomainObject, StatusChangeHistory).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is StatusChangeHistorySendEmailFlag) Then
                CType(InsertArg.DomainObject, StatusChangeHistorySendEmailFlag).id = InsertArg.ID
            End If

            If (TypeOf InsertArg.DomainObject Is DiscountProposalDetailApproval) Then
                CType(InsertArg.DomainObject, DiscountProposalDetailApproval).ID = InsertArg.ID
                CType(InsertArg.DomainObject, DiscountProposalDetailApproval).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is DiscountProposalDetailApprovaltoSPL) Then
                CType(InsertArg.DomainObject, DiscountProposalDetailApprovaltoSPL).ID = InsertArg.ID
                CType(InsertArg.DomainObject, DiscountProposalDetailApprovaltoSPL).MarkLoaded()
            End If

            If (TypeOf InsertArg.DomainObject Is DiscountProposalDetailPrice) Then
                CType(InsertArg.DomainObject, DiscountProposalDetailPrice).ID = InsertArg.ID
                CType(InsertArg.DomainObject, DiscountProposalDetailPrice).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is DiscountProposalPricetoParameter) Then
                CType(InsertArg.DomainObject, DiscountProposalPricetoParameter).ID = InsertArg.ID
            End If
        End Sub

        Public Function InsertTransaction(ByVal objDiscountProposalHeader As DiscountProposalHeader, _
                                          ByVal objFleetCustomerDetail As FleetCustomerDetail, ByVal objFleetCustomerHeader As FleetCustomerHeader, _
                                          ByVal arrDiscountProposalDetail As ArrayList, ByVal arrDiscountProposalOwnership As ArrayList, _
                                          ByVal arrDiscountProposalDetailDoc As ArrayList, ByVal arrDiscountProposalDetailCustomer As ArrayList, _
                                          ByVal arrDiscountProposalEmailUser As ArrayList, ByVal arrDiscountProposalDetailApproval As ArrayList, _
                                          ByVal arrDiscountProposalDetailPrice As ArrayList, ByVal arrDiscountProposalDetailApprovaltoSPL As ArrayList, _
                                          ByVal arrDiscountProposalPricetoParameter As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    '---Insert 
                    If Not IsNothing(objFleetCustomerHeader) Then
                        If objFleetCustomerHeader.ID = 0 Then
                            m_TransactionManager.AddInsert(objFleetCustomerHeader, m_userPrincipal.Identity.Name)
                        End If
                        If Not IsNothing(objFleetCustomerDetail) Then
                            If objFleetCustomerDetail.ID = 0 Then
                                objFleetCustomerDetail.FleetCustomerHeader = objFleetCustomerHeader
                                m_TransactionManager.AddInsert(objFleetCustomerDetail, m_userPrincipal.Identity.Name)
                            End If
                            objDiscountProposalHeader.FleetCustomerDetail = objFleetCustomerDetail
                            m_TransactionManager.AddInsert(objDiscountProposalHeader, m_userPrincipal.Identity.Name)
                        End If
                    End If

                    '---Edit
                    If Not IsNothing(objFleetCustomerHeader) AndAlso Not IsNothing(objFleetCustomerDetail) Then
                        If objFleetCustomerHeader.ID > 0 AndAlso objFleetCustomerDetail.ID > 0 Then
                            objFleetCustomerDetail.FleetCustomerHeader = objFleetCustomerHeader
                            m_TransactionManager.AddUpdate(objFleetCustomerDetail, m_userPrincipal.Identity.Name)
                            m_TransactionManager.AddUpdate(objFleetCustomerHeader, m_userPrincipal.Identity.Name)
                        End If
                    End If

                    If Not IsNothing(arrDiscountProposalDetail) Then
                        If arrDiscountProposalDetail.Count > 0 Then
                            For Each oDiscountProposalDetail As DiscountProposalDetail In arrDiscountProposalDetail
                                oDiscountProposalDetail.DiscountProposalHeader = objDiscountProposalHeader
                                m_TransactionManager.AddInsert(oDiscountProposalDetail, m_userPrincipal.Identity.Name)

                                If Not IsNothing(arrDiscountProposalDetailPrice) Then
                                    If arrDiscountProposalDetailPrice.Count > 0 Then
                                        For Each oDiscountProposalDetailPrice As DiscountProposalDetailPrice In arrDiscountProposalDetailPrice
                                            If oDiscountProposalDetail.SubCategoryVehicle.ID = oDiscountProposalDetailPrice.SubCategoryVehicleID Then
                                                If oDiscountProposalDetail.AssyYear = oDiscountProposalDetailPrice.AssyYear Then
                                                    If oDiscountProposalDetail.ModelYear = oDiscountProposalDetailPrice.ModelYear Then
                                                        Dim intVechileColorID As Integer = 0
                                                        If Not IsNothing(oDiscountProposalDetail.VechileColorIsActiveOnPK) Then
                                                            intVechileColorID = oDiscountProposalDetail.VechileColorIsActiveOnPK.VechileColor.ID
                                                        End If
                                                        If intVechileColorID = oDiscountProposalDetailPrice.VechileColorID Then
                                                            Dim intVechileTypeID As Integer = 0
                                                            If Not IsNothing(oDiscountProposalDetail.VechileColorIsActiveOnPK) Then
                                                                If Not IsNothing(oDiscountProposalDetail.VechileColorIsActiveOnPK.VechileTypeGeneral) Then
                                                                    intVechileTypeID = oDiscountProposalDetail.VechileColorIsActiveOnPK.VechileTypeGeneral.ID
                                                                End If
                                                            End If
                                                            If intVechileTypeID = oDiscountProposalDetailPrice.VechileTypeID Then
                                                                oDiscountProposalDetailPrice.DiscountProposalHeader = objDiscountProposalHeader
                                                                oDiscountProposalDetailPrice.DiscountProposalDetail = oDiscountProposalDetail
                                                                m_TransactionManager.AddInsert(oDiscountProposalDetailPrice, m_userPrincipal.Identity.Name)

                                                                If Not IsNothing(arrDiscountProposalPricetoParameter) Then
                                                                    If arrDiscountProposalPricetoParameter.Count > 0 Then
                                                                        For Each oDiscountProposalPricetoParameter As DiscountProposalPricetoParameter In arrDiscountProposalPricetoParameter
                                                                            If oDiscountProposalDetailPrice.NumberRow = oDiscountProposalPricetoParameter.NumberRowParent Then
                                                                                oDiscountProposalPricetoParameter.DiscountProposalDetailPrice = oDiscountProposalDetailPrice

                                                                                m_TransactionManager.AddInsert(oDiscountProposalPricetoParameter, m_userPrincipal.Identity.Name)
                                                                            End If
                                                                        Next
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arrDiscountProposalOwnership) Then
                        If arrDiscountProposalOwnership.Count > 0 Then
                            For Each oDiscountProposalDetailOwnership As DiscountProposalDetailOwnership In arrDiscountProposalOwnership
                                oDiscountProposalDetailOwnership.DiscountProposalHeader = objDiscountProposalHeader
                                m_TransactionManager.AddInsert(oDiscountProposalDetailOwnership, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrDiscountProposalDetailDoc) Then
                        If arrDiscountProposalDetailDoc.Count > 0 Then
                            For Each oDiscountProposalDetailDoc As DiscountProposalDetailDocument In arrDiscountProposalDetailDoc
                                oDiscountProposalDetailDoc.DiscountProposalHeader = objDiscountProposalHeader
                                m_TransactionManager.AddInsert(oDiscountProposalDetailDoc, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrDiscountProposalDetailCustomer) Then
                        If arrDiscountProposalDetailCustomer.Count > 0 Then
                            For Each oDiscountProposalDetailCustomer As DiscountProposalDetailCustomer In arrDiscountProposalDetailCustomer
                                oDiscountProposalDetailCustomer.DiscountProposalHeader = objDiscountProposalHeader
                                m_TransactionManager.AddInsert(oDiscountProposalDetailCustomer, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrDiscountProposalEmailUser) Then
                        If arrDiscountProposalEmailUser.Count > 0 Then
                            For Each oDiscountProposalEmailUser As DiscountProposalEmailUser In arrDiscountProposalEmailUser
                                oDiscountProposalEmailUser.DiscountProposalHeader = objDiscountProposalHeader
                                m_TransactionManager.AddInsert(oDiscountProposalEmailUser, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrDiscountProposalDetailApproval) Then
                        If arrDiscountProposalDetailApproval.Count > 0 Then
                            For Each oDiscountProposalDetailApproval As DiscountProposalDetailApproval In arrDiscountProposalDetailApproval
                                oDiscountProposalDetailApproval.DiscountProposalHeader = objDiscountProposalHeader
                                m_TransactionManager.AddInsert(oDiscountProposalDetailApproval, m_userPrincipal.Identity.Name)

                                If Not IsNothing(arrDiscountProposalDetailApprovaltoSPL) Then
                                    If arrDiscountProposalDetailApprovaltoSPL.Count > 0 Then
                                        For Each oDiscountProposalDetailApprovaltoSPL As DiscountProposalDetailApprovaltoSPL In arrDiscountProposalDetailApprovaltoSPL
                                            If oDiscountProposalDetailApproval.ModelID = oDiscountProposalDetailApprovaltoSPL.ModelID Then
                                                If oDiscountProposalDetailApproval.VechileType.ID = oDiscountProposalDetailApprovaltoSPL.VechileTypeID Then
                                                    oDiscountProposalDetailApprovaltoSPL.DiscountProposalDetailApproval = oDiscountProposalDetailApproval
                                                    m_TransactionManager.AddInsert(oDiscountProposalDetailApprovaltoSPL, m_userPrincipal.Identity.Name)
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(objDiscountProposalHeader) Then
                        Dim objStatusChangeHistory As StatusChangeHistory = New StatusChangeHistory
                        objStatusChangeHistory.DocumentType = 17
                        objStatusChangeHistory.DocumentRegNumber = objDiscountProposalHeader.ProposalRegNo
                        objStatusChangeHistory.OldStatus = 0
                        objStatusChangeHistory.NewStatus = 0
                        m_TransactionManager.AddInsert(objStatusChangeHistory, m_userPrincipal.Identity.Name)

                        Dim objStatusChangeHistorySendEmailFlag As StatusChangeHistorySendEmailFlag = New StatusChangeHistorySendEmailFlag
                        objStatusChangeHistorySendEmailFlag.StatusChangeHistory = objStatusChangeHistory
                        objStatusChangeHistorySendEmailFlag.IsSendEmail = 0
                        m_TransactionManager.AddInsert(objStatusChangeHistorySendEmailFlag, m_userPrincipal.Identity.Name)
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = objDiscountProposalHeader.ID

                    Dim arrParam As New ArrayList()
                    Dim par1 As New SqlClient.SqlParameter

                    par1.DbType = DbType.Int32
                    par1.Value = returnValue
                    par1.ParameterName = "@DiscountProposalHeaderID"
                    arrParam.Add(par1)
                    m_DiscountProposalHeaderMapper.ExecuteSP("up_CleansingDoubleDiscPropParam", arrParam)

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

        Function DeleteTransaction(ByVal objDiscountProposalHeader As DiscountProposalHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If IsNothing(objDiscountProposalHeader) Then Return returnValue
                    If Not IsNothing(objDiscountProposalHeader) AndAlso objDiscountProposalHeader.ID <= 0 Then Return returnValue

                    Dim arlDiscountProposalDetails As New ArrayList
                    Dim arlDiscountProposalDetailPrices As New ArrayList
                    Dim arlDiscountProposalDetailOwnerships As New ArrayList
                    Dim arlDiscountProposalDetailDocuments As New ArrayList
                    Dim arlDiscountProposalDetailCustomers As New ArrayList
                    Dim arlDiscountProposalEmailUser As New ArrayList
                    Dim arlDiscountProposalDetailApproval As New ArrayList
                    Dim arlDiscountProposalDetailApprovaltoSPL As New ArrayList

                    If Not IsNothing(objDiscountProposalHeader.DiscountProposalDetails) Then
                        arlDiscountProposalDetails = objDiscountProposalHeader.DiscountProposalDetails
                    End If
                    If Not IsNothing(objDiscountProposalHeader.DiscountProposalDetailPrices) Then
                        arlDiscountProposalDetailPrices = objDiscountProposalHeader.DiscountProposalDetailPrices
                    End If
                    If Not IsNothing(objDiscountProposalHeader.DiscountProposalDetailOwnerships) Then
                        arlDiscountProposalDetailOwnerships = objDiscountProposalHeader.DiscountProposalDetailOwnerships
                    End If
                    If Not IsNothing(objDiscountProposalHeader.DiscountProposalDetailDocuments) Then
                        arlDiscountProposalDetailDocuments = objDiscountProposalHeader.DiscountProposalDetailDocuments
                    End If
                    If Not IsNothing(objDiscountProposalHeader.DiscountProposalDetailCustomers) Then
                        arlDiscountProposalDetailCustomers = objDiscountProposalHeader.DiscountProposalDetailCustomers
                    End If
                    If Not IsNothing(objDiscountProposalHeader.DiscountProposalEmailUsers) Then
                        arlDiscountProposalEmailUser = objDiscountProposalHeader.DiscountProposalEmailUsers
                    End If
                    If Not IsNothing(objDiscountProposalHeader.DiscountProposalDetailApprovals) Then
                        arlDiscountProposalDetailApproval = objDiscountProposalHeader.DiscountProposalDetailApprovals
                    End If

                    If Not IsNothing(arlDiscountProposalDetails) Then
                        If arlDiscountProposalDetails.Count > 0 Then
                            For Each oDiscountProposalDtl As DiscountProposalDetail In arlDiscountProposalDetails
                                For Each oDiscountProposalDtlPrice As DiscountProposalDetailPrice In oDiscountProposalDtl.DiscountProposalDetailPrices
                                    For Each oDiscountProposalPricetoParameter As DiscountProposalPricetoParameter In oDiscountProposalDtlPrice.DiscountProposalPricetoParameters
                                        oDiscountProposalPricetoParameter.RowStatus = CType(DBRowStatus.Deleted, Short)
                                        m_TransactionManager.AddUpdate(oDiscountProposalPricetoParameter, m_userPrincipal.Identity.Name)
                                    Next
                                Next
                                For Each oDiscountProposalDtlPrice As DiscountProposalDetailPrice In oDiscountProposalDtl.DiscountProposalDetailPrices
                                    oDiscountProposalDtlPrice.RowStatus = CType(DBRowStatus.Deleted, Short)
                                    m_TransactionManager.AddUpdate(oDiscountProposalDtlPrice, m_userPrincipal.Identity.Name)
                                Next
                            Next
                            For Each oDiscountProposalDetail As DiscountProposalDetail In arlDiscountProposalDetails
                                oDiscountProposalDetail.RowStatus = CType(DBRowStatus.Deleted, Short)
                                m_TransactionManager.AddUpdate(oDiscountProposalDetail, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDiscountProposalDetailOwnerships) Then
                        If arlDiscountProposalDetailOwnerships.Count > 0 Then
                            For Each oDiscountProposalDetailOwnership As DiscountProposalDetailOwnership In arlDiscountProposalDetailOwnerships
                                oDiscountProposalDetailOwnership.RowStatus = CType(DBRowStatus.Deleted, Short)
                                m_TransactionManager.AddUpdate(oDiscountProposalDetailOwnership, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDiscountProposalDetailDocuments) Then
                        If arlDiscountProposalDetailDocuments.Count > 0 Then
                            For Each oDiscountProposalDetailDoc As DiscountProposalDetailDocument In arlDiscountProposalDetailDocuments
                                oDiscountProposalDetailDoc.RowStatus = CType(DBRowStatus.Deleted, Short)
                                m_TransactionManager.AddUpdate(oDiscountProposalDetailDoc, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDiscountProposalDetailCustomers) Then
                        If arlDiscountProposalDetailCustomers.Count > 0 Then
                            For Each oDiscountProposalDetailCust As DiscountProposalDetailCustomer In arlDiscountProposalDetailCustomers
                                oDiscountProposalDetailCust.RowStatus = CType(DBRowStatus.Deleted, Short)
                                m_TransactionManager.AddUpdate(oDiscountProposalDetailCust, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDiscountProposalEmailUser) Then
                        If arlDiscountProposalEmailUser.Count > 0 Then
                            For Each oDiscountProposalEmailUser As DiscountProposalEmailUser In arlDiscountProposalEmailUser
                                oDiscountProposalEmailUser.RowStatus = CType(DBRowStatus.Deleted, Short)
                                m_TransactionManager.AddUpdate(oDiscountProposalEmailUser, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDiscountProposalDetailApproval) Then
                        If arlDiscountProposalDetailApproval.Count > 0 Then
                            For Each oDiscountProposalDtlApproval As DiscountProposalDetailApproval In arlDiscountProposalDetailApproval
                                For Each oDiscountProposalDtlApprovaltoSPL As DiscountProposalDetailApprovaltoSPL In oDiscountProposalDtlApproval.DiscountProposalDetailApprovaltoSPLs
                                    oDiscountProposalDtlApprovaltoSPL.RowStatus = CType(DBRowStatus.Deleted, Short)
                                    m_TransactionManager.AddUpdate(oDiscountProposalDtlApprovaltoSPL, m_userPrincipal.Identity.Name)
                                Next
                            Next

                            For Each oDiscountProposalDtlApproval As DiscountProposalDetailApproval In arlDiscountProposalDetailApproval
                                oDiscountProposalDtlApproval.RowStatus = CType(DBRowStatus.Deleted, Short)
                                m_TransactionManager.AddUpdate(oDiscountProposalDtlApproval, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    objDiscountProposalHeader.RowStatus = CType(DBRowStatus.Deleted, Short)
                    m_TransactionManager.AddUpdate(objDiscountProposalHeader, m_userPrincipal.Identity.Name)

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

        Function UpdateTransaction(objDiscountProposalHeader As DiscountProposalHeader, _
                                   objFleetCustomerDetail As FleetCustomerDetail, ByRef objFleetCustomerHeader As FleetCustomerHeader, _
                                   arlDiscountProposalDetail As ArrayList, arlDelDiscountProposalDetail As ArrayList, _
                                   arlDiscountProposalDetailOwnership As ArrayList, arlDelDiscountProposalDetailOwnership As ArrayList, _
                                   arlDiscountProposalDetailDoc As ArrayList, arlDelDiscountProposalDetailDoc As ArrayList, _
                                   arlDiscountProposalDetailCustomer As ArrayList, arlDelDiscountProposalDetailCustomer As ArrayList, _
                                   arlDiscountProposalEmailUser As ArrayList, arlDelDiscountProposalEmailUser As ArrayList, _
                                   arlDiscountProposalDetailApproval As ArrayList, arlDelDiscountProposalDetailApproval As ArrayList, _
                                   arlDiscountProposalDetailPrice As ArrayList, arlDelDiscountProposalDetailPrice As ArrayList, _
                                   arlDiscountProposalDetailApprovaltoSPL As ArrayList, arlDelDiscountProposalDetailApprovaltoSPL As ArrayList, _
                                   arlDiscountProposalPricetoParameter As ArrayList, arlDelDiscountProposalPricetoParameter As ArrayList) As Integer

            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not IsNothing(arlDelDiscountProposalPricetoParameter) Then
                        If arlDelDiscountProposalPricetoParameter.Count > 0 Then
                            For Each item As DiscountProposalPricetoParameter In arlDelDiscountProposalPricetoParameter
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDelDiscountProposalDetailPrice) Then
                        If arlDelDiscountProposalDetailPrice.Count > 0 Then
                            For Each item As DiscountProposalDetailPrice In arlDelDiscountProposalDetailPrice
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDelDiscountProposalDetail) Then
                        If arlDelDiscountProposalDetail.Count > 0 Then
                            For Each item As DiscountProposalDetail In arlDelDiscountProposalDetail
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDiscountProposalDetail) Then
                        If arlDiscountProposalDetail.Count > 0 Then
                            For Each oDiscountProposalDetail As DiscountProposalDetail In arlDiscountProposalDetail
                                oDiscountProposalDetail.DiscountProposalHeader = objDiscountProposalHeader
                                If oDiscountProposalDetail.ID <> 0 Then
                                    If Not IsNothing(arlDiscountProposalDetailPrice) Then
                                        If arlDiscountProposalDetailPrice.Count > 0 Then
                                            For Each oDiscountProposalDetailPrice As DiscountProposalDetailPrice In arlDiscountProposalDetailPrice
                                                If oDiscountProposalDetail.SubCategoryVehicle.ID = oDiscountProposalDetailPrice.SubCategoryVehicleID Then
                                                    If oDiscountProposalDetail.AssyYear = oDiscountProposalDetailPrice.AssyYear Then
                                                        If oDiscountProposalDetail.ModelYear = oDiscountProposalDetailPrice.ModelYear Then
                                                            Dim intVechileColorID As Integer = 0
                                                            If Not IsNothing(oDiscountProposalDetail.VechileColorIsActiveOnPK) Then
                                                                intVechileColorID = oDiscountProposalDetail.VechileColorIsActiveOnPK.VechileColor.ID
                                                            End If
                                                            If intVechileColorID = oDiscountProposalDetailPrice.VechileColorID Then
                                                                Dim intVechileTypeID As Integer = 0
                                                                If Not IsNothing(oDiscountProposalDetail.VechileColorIsActiveOnPK) Then
                                                                    If Not IsNothing(oDiscountProposalDetail.VechileColorIsActiveOnPK.VechileTypeGeneral) Then
                                                                        intVechileTypeID = oDiscountProposalDetail.VechileColorIsActiveOnPK.VechileTypeGeneral.ID
                                                                    End If
                                                                End If
                                                                If intVechileTypeID = oDiscountProposalDetailPrice.VechileTypeID Then
                                                                    oDiscountProposalDetailPrice.DiscountProposalHeader = objDiscountProposalHeader
                                                                    oDiscountProposalDetailPrice.DiscountProposalDetail = oDiscountProposalDetail
                                                                    If oDiscountProposalDetailPrice.ID <> 0 Then
                                                                        If Not IsNothing(arlDiscountProposalPricetoParameter) Then
                                                                            For Each oDiscountProposalPricetoParameter As DiscountProposalPricetoParameter In arlDiscountProposalPricetoParameter
                                                                                If oDiscountProposalPricetoParameter.NumberRowParent = oDiscountProposalDetailPrice.NumberRow Then
                                                                                    oDiscountProposalPricetoParameter.DiscountProposalDetailPrice = oDiscountProposalDetailPrice
                                                                                    If oDiscountProposalPricetoParameter.ID <> 0 Then
                                                                                        m_TransactionManager.AddUpdate(oDiscountProposalPricetoParameter, m_userPrincipal.Identity.Name)
                                                                                    Else
                                                                                        m_TransactionManager.AddInsert(oDiscountProposalPricetoParameter, m_userPrincipal.Identity.Name)
                                                                                    End If
                                                                                End If
                                                                            Next
                                                                        End If
                                                                        m_TransactionManager.AddUpdate(oDiscountProposalDetailPrice, m_userPrincipal.Identity.Name)
                                                                    Else
                                                                        m_TransactionManager.AddInsert(oDiscountProposalDetailPrice, m_userPrincipal.Identity.Name)
                                                                        If Not IsNothing(arlDiscountProposalPricetoParameter) Then
                                                                            For Each oDiscountProposalPricetoParameter As DiscountProposalPricetoParameter In arlDiscountProposalPricetoParameter
                                                                                If oDiscountProposalPricetoParameter.NumberRowParent = oDiscountProposalDetailPrice.NumberRow Then
                                                                                    oDiscountProposalPricetoParameter.DiscountProposalDetailPrice = oDiscountProposalDetailPrice
                                                                                    If oDiscountProposalPricetoParameter.ID <> 0 Then
                                                                                        m_TransactionManager.AddUpdate(oDiscountProposalPricetoParameter, m_userPrincipal.Identity.Name)
                                                                                    Else
                                                                                        m_TransactionManager.AddInsert(oDiscountProposalPricetoParameter, m_userPrincipal.Identity.Name)
                                                                                    End If
                                                                                End If
                                                                            Next
                                                                        End If
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If
                                    m_TransactionManager.AddUpdate(oDiscountProposalDetail, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(oDiscountProposalDetail, m_userPrincipal.Identity.Name)
                                    If Not IsNothing(arlDiscountProposalDetailPrice) Then
                                        If arlDiscountProposalDetailPrice.Count > 0 Then
                                            For Each oDiscountProposalDetailPrice As DiscountProposalDetailPrice In arlDiscountProposalDetailPrice
                                                If oDiscountProposalDetail.SubCategoryVehicle.ID = oDiscountProposalDetailPrice.SubCategoryVehicleID Then
                                                    If oDiscountProposalDetail.AssyYear = oDiscountProposalDetailPrice.AssyYear Then
                                                        If oDiscountProposalDetail.ModelYear = oDiscountProposalDetailPrice.ModelYear Then
                                                            Dim intVechileColorID As Integer = 0
                                                            If Not IsNothing(oDiscountProposalDetail.VechileColorIsActiveOnPK) Then
                                                                intVechileColorID = oDiscountProposalDetail.VechileColorIsActiveOnPK.VechileColor.ID
                                                            End If
                                                            If intVechileColorID = oDiscountProposalDetailPrice.VechileColorID Then
                                                                Dim intVechileTypeID As Integer = 0
                                                                If Not IsNothing(oDiscountProposalDetail.VechileColorIsActiveOnPK) Then
                                                                    If Not IsNothing(oDiscountProposalDetail.VechileColorIsActiveOnPK.VechileTypeGeneral) Then
                                                                        intVechileTypeID = oDiscountProposalDetail.VechileColorIsActiveOnPK.VechileTypeGeneral.ID
                                                                    End If
                                                                End If
                                                                If intVechileTypeID = oDiscountProposalDetailPrice.VechileTypeID Then
                                                                    oDiscountProposalDetailPrice.DiscountProposalHeader = objDiscountProposalHeader
                                                                    oDiscountProposalDetailPrice.DiscountProposalDetail = oDiscountProposalDetail
                                                                    If oDiscountProposalDetailPrice.ID <> 0 Then
                                                                        If Not IsNothing(arlDiscountProposalPricetoParameter) Then
                                                                            For Each oDiscountProposalPricetoParameter As DiscountProposalPricetoParameter In arlDiscountProposalPricetoParameter
                                                                                If oDiscountProposalPricetoParameter.NumberRowParent = oDiscountProposalDetailPrice.NumberRow Then
                                                                                    oDiscountProposalPricetoParameter.DiscountProposalDetailPrice = oDiscountProposalDetailPrice
                                                                                    If oDiscountProposalPricetoParameter.ID <> 0 Then
                                                                                        m_TransactionManager.AddUpdate(oDiscountProposalPricetoParameter, m_userPrincipal.Identity.Name)
                                                                                    Else
                                                                                        m_TransactionManager.AddInsert(oDiscountProposalPricetoParameter, m_userPrincipal.Identity.Name)
                                                                                    End If
                                                                                End If
                                                                            Next
                                                                        End If
                                                                        m_TransactionManager.AddUpdate(oDiscountProposalDetailPrice, m_userPrincipal.Identity.Name)
                                                                    Else
                                                                        m_TransactionManager.AddInsert(oDiscountProposalDetailPrice, m_userPrincipal.Identity.Name)
                                                                        If Not IsNothing(arlDiscountProposalPricetoParameter) Then
                                                                            For Each oDiscountProposalPricetoParameter As DiscountProposalPricetoParameter In arlDiscountProposalPricetoParameter
                                                                                If oDiscountProposalPricetoParameter.NumberRowParent = oDiscountProposalDetailPrice.NumberRow Then
                                                                                    oDiscountProposalPricetoParameter.DiscountProposalDetailPrice = oDiscountProposalDetailPrice
                                                                                    If oDiscountProposalPricetoParameter.ID <> 0 Then
                                                                                        m_TransactionManager.AddUpdate(oDiscountProposalPricetoParameter, m_userPrincipal.Identity.Name)
                                                                                    Else
                                                                                        m_TransactionManager.AddInsert(oDiscountProposalPricetoParameter, m_userPrincipal.Identity.Name)
                                                                                    End If
                                                                                End If
                                                                            Next
                                                                        End If
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDelDiscountProposalDetailOwnership) Then
                        If arlDelDiscountProposalDetailOwnership.Count > 0 Then
                            For Each item As DiscountProposalDetailOwnership In arlDelDiscountProposalDetailOwnership
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDiscountProposalDetailOwnership) Then
                        If arlDiscountProposalDetailOwnership.Count > 0 Then
                            For Each item As DiscountProposalDetailOwnership In arlDiscountProposalDetailOwnership
                                item.DiscountProposalHeader = objDiscountProposalHeader
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDelDiscountProposalDetailDoc) Then
                        If arlDelDiscountProposalDetailDoc.Count > 0 Then
                            For Each item As DiscountProposalDetailDocument In arlDelDiscountProposalDetailDoc
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDiscountProposalDetailDoc) Then
                        If arlDiscountProposalDetailDoc.Count > 0 Then
                            For Each item As DiscountProposalDetailDocument In arlDiscountProposalDetailDoc
                                item.DiscountProposalHeader = objDiscountProposalHeader
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDelDiscountProposalDetailCustomer) Then
                        If arlDelDiscountProposalDetailCustomer.Count > 0 Then
                            For Each item As DiscountProposalDetailCustomer In arlDelDiscountProposalDetailCustomer
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If
                    If Not IsNothing(arlDiscountProposalDetailCustomer) Then
                        If arlDiscountProposalDetailCustomer.Count > 0 Then
                            For Each item As DiscountProposalDetailCustomer In arlDiscountProposalDetailCustomer
                                item.DiscountProposalHeader = objDiscountProposalHeader
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arlDelDiscountProposalEmailUser) Then
                        If arlDelDiscountProposalEmailUser.Count > 0 Then
                            For Each item As DiscountProposalEmailUser In arlDelDiscountProposalEmailUser
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If
                    If Not IsNothing(arlDiscountProposalEmailUser) Then
                        If arlDiscountProposalEmailUser.Count > 0 Then
                            For Each item As DiscountProposalEmailUser In arlDiscountProposalEmailUser
                                item.DiscountProposalHeader = objDiscountProposalHeader
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    Dim arlDiscountProposalDetailApproval0 As ArrayList
                    If Not IsNothing(arlDiscountProposalDetailApproval) Then
                        arlDiscountProposalDetailApproval0 = New System.Collections.ArrayList(
                                                        (From obj As DiscountProposalDetailApproval In arlDiscountProposalDetailApproval.OfType(Of DiscountProposalDetailApproval)()
                                                            Where obj.ID = 0
                                                            Order By obj.ModelID, obj.VechileTypeID
                                                            Select obj).ToList())
                        arlDiscountProposalDetailApproval = New System.Collections.ArrayList(
                                                (From obj As DiscountProposalDetailApproval In arlDiscountProposalDetailApproval.OfType(Of DiscountProposalDetailApproval)()
                                                    Where obj.ID > 0
                                                    Order By obj.ModelID, obj.VechileTypeID
                                                    Select obj).ToList())
                    End If
                    Dim arlDiscountProposalDetailApprovaltoSPL0 As ArrayList
                    If Not IsNothing(arlDiscountProposalDetailApprovaltoSPL) Then
                        arlDiscountProposalDetailApprovaltoSPL0 = New System.Collections.ArrayList(
                                                        (From obj As DiscountProposalDetailApprovaltoSPL In arlDiscountProposalDetailApprovaltoSPL.OfType(Of DiscountProposalDetailApprovaltoSPL)()
                                                            Where obj.ID = 0
                                                            Order By obj.ModelID, obj.VechileTypeID
                                                            Select obj).ToList())
                    End If
                    '===================================================================================================
                    'INSERT DiscountProposalDetailApproval and DiscountProposalDetailApprovaltoSPL
                    If Not IsNothing(arlDiscountProposalDetailApproval0) Then
                        If arlDiscountProposalDetailApproval0.Count > 0 Then
                            For Each oDiscountProposalDetailApproval0 As DiscountProposalDetailApproval In arlDiscountProposalDetailApproval0
                                oDiscountProposalDetailApproval0.DiscountProposalHeader = objDiscountProposalHeader
                                If oDiscountProposalDetailApproval0.ID = 0 Then
                                    m_TransactionManager.AddInsert(oDiscountProposalDetailApproval0, m_userPrincipal.Identity.Name)
                                End If
                                If Not IsNothing(arlDiscountProposalDetailApprovaltoSPL0) Then
                                    If arlDiscountProposalDetailApprovaltoSPL0.Count > 0 Then
                                        For Each oDiscountProposalDetailApprovaltoSPL0 As DiscountProposalDetailApprovaltoSPL In arlDiscountProposalDetailApprovaltoSPL0
                                            If oDiscountProposalDetailApproval0.ModelID = oDiscountProposalDetailApprovaltoSPL0.ModelID Then
                                                If oDiscountProposalDetailApproval0.VechileType.ID = oDiscountProposalDetailApprovaltoSPL0.VechileTypeID Then
                                                    oDiscountProposalDetailApprovaltoSPL0.DiscountProposalDetailApproval = oDiscountProposalDetailApproval0
                                                    If oDiscountProposalDetailApprovaltoSPL0.ID = 0 Then
                                                        m_TransactionManager.AddInsert(oDiscountProposalDetailApprovaltoSPL0, m_userPrincipal.Identity.Name)
                                                    End If
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    End If
                    '============================================================================
                    'EDIT DiscountProposalDetailApproval and DiscountProposalDetailApprovaltoSPL
                    '============================================================================
                    If Not IsNothing(arlDelDiscountProposalDetailApprovaltoSPL) Then
                        If arlDelDiscountProposalDetailApprovaltoSPL.Count > 0 Then
                            For Each oDiscountProposalDetailApprovaltoSPL As DiscountProposalDetailApprovaltoSPL In arlDelDiscountProposalDetailApprovaltoSPL
                                oDiscountProposalDetailApprovaltoSPL.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(oDiscountProposalDetailApprovaltoSPL, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If
                    If Not IsNothing(arlDiscountProposalDetailApproval) Then
                        If arlDiscountProposalDetailApproval.Count > 0 Then
                            For Each oDiscountProposalDetailApproval As DiscountProposalDetailApproval In arlDiscountProposalDetailApproval
                                oDiscountProposalDetailApproval.DiscountProposalHeader = objDiscountProposalHeader
                                If oDiscountProposalDetailApproval.ID > 0 Then
                                    If Not IsNothing(arlDiscountProposalDetailApprovaltoSPL) Then
                                        If arlDiscountProposalDetailApprovaltoSPL.Count > 0 Then
                                            For Each oDiscountProposalDetailApprovaltoSPL As DiscountProposalDetailApprovaltoSPL In arlDiscountProposalDetailApprovaltoSPL
                                                If oDiscountProposalDetailApproval.ModelID = oDiscountProposalDetailApprovaltoSPL.ModelID Then
                                                    If oDiscountProposalDetailApproval.VechileType.ID = oDiscountProposalDetailApprovaltoSPL.VechileTypeID Then
                                                        oDiscountProposalDetailApprovaltoSPL.DiscountProposalDetailApproval = oDiscountProposalDetailApproval
                                                        If oDiscountProposalDetailApprovaltoSPL.ID > 0 Then
                                                            m_TransactionManager.AddUpdate(oDiscountProposalDetailApprovaltoSPL, m_userPrincipal.Identity.Name)
                                                        Else
                                                            m_TransactionManager.AddInsert(oDiscountProposalDetailApprovaltoSPL, m_userPrincipal.Identity.Name)
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If
                    '=========>
                    If Not IsNothing(arlDelDiscountProposalDetailApproval) Then
                        If arlDelDiscountProposalDetailApproval.Count > 0 Then
                            For Each oDiscountProposalDetailApproval As DiscountProposalDetailApproval In arlDelDiscountProposalDetailApproval
                                oDiscountProposalDetailApproval.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(oDiscountProposalDetailApproval, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If
                    If Not IsNothing(arlDiscountProposalDetailApproval) Then
                        If arlDiscountProposalDetailApproval.Count > 0 Then
                            For Each oDiscountProposalDetailApproval As DiscountProposalDetailApproval In arlDiscountProposalDetailApproval
                                oDiscountProposalDetailApproval.DiscountProposalHeader = objDiscountProposalHeader
                                If oDiscountProposalDetailApproval.ID > 0 Then
                                    m_TransactionManager.AddUpdate(oDiscountProposalDetailApproval, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If
                    '============================================================================


                    If objFleetCustomerHeader.ID = 0 AndAlso objFleetCustomerDetail.ID = 0 Then
                        m_TransactionManager.AddInsert(objFleetCustomerHeader, m_userPrincipal.Identity.Name)
                        If Not IsNothing(objFleetCustomerDetail) Then
                            objFleetCustomerDetail.FleetCustomerHeader = objFleetCustomerHeader
                            m_TransactionManager.AddInsert(objFleetCustomerDetail, m_userPrincipal.Identity.Name)
                        End If
                    End If
                    If Not IsNothing(objFleetCustomerDetail) Then
                        If objFleetCustomerDetail.ID <= 0 Then
                            If Not IsNothing(objFleetCustomerHeader) AndAlso objFleetCustomerHeader.ID <= 0 Then
                                m_TransactionManager.AddInsert(objFleetCustomerHeader, m_userPrincipal.Identity.Name)
                            End If
                            objFleetCustomerDetail.FleetCustomerHeader = objFleetCustomerHeader
                            m_TransactionManager.AddInsert(objFleetCustomerDetail, m_userPrincipal.Identity.Name)
                            If Not IsNothing(objDiscountProposalHeader) Then
                                objDiscountProposalHeader.FleetCustomerDetail = objFleetCustomerDetail
                                m_TransactionManager.AddUpdate(objDiscountProposalHeader, m_userPrincipal.Identity.Name)
                            End If
                        Else
                            If Not IsNothing(objDiscountProposalHeader) Then
                                objDiscountProposalHeader.FleetCustomerDetail = objFleetCustomerDetail
                                m_TransactionManager.AddUpdate(objDiscountProposalHeader, m_userPrincipal.Identity.Name)
                            End If
                            m_TransactionManager.AddUpdate(objFleetCustomerDetail, m_userPrincipal.Identity.Name)
                        End If
                    End If
                    If Not IsNothing(objFleetCustomerHeader) AndAlso objFleetCustomerHeader.ID > 0 Then
                        m_TransactionManager.AddUpdate(objFleetCustomerHeader, m_userPrincipal.Identity.Name)
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDiscountProposalHeader.ID
                    End If

                    Dim arrParam As New ArrayList()
                    Dim par1 As New SqlClient.SqlParameter

                    par1.DbType = DbType.Int32
                    par1.Value = returnValue
                    par1.ParameterName = "@DiscountProposalHeaderID"
                    arrParam.Add(par1)
                    m_DiscountProposalHeaderMapper.ExecuteSP("up_CleansingDoubleDiscPropParam", arrParam)

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

        Public Function UpdateStatus(arrDiscountProposalHeader As ArrayList, arrDiscountProposalHeaderOld As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not IsNothing(arrDiscountProposalHeader) Then
                        If arrDiscountProposalHeader.Count > 0 Then
                            Dim listOld As List(Of DiscountProposalHeader) = arrDiscountProposalHeaderOld.Cast(Of DiscountProposalHeader).ToList()
                            For Each item As DiscountProposalHeader In arrDiscountProposalHeader
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)

                                Dim objStatusChangeHistory As StatusChangeHistory = New StatusChangeHistory
                                objStatusChangeHistory.DocumentType = 17
                                objStatusChangeHistory.DocumentRegNumber = item.ProposalRegNo
                                objStatusChangeHistory.OldStatus = listOld.Where(Function(i) i.ID = item.ID).FirstOrDefault().Status
                                objStatusChangeHistory.NewStatus = item.Status
                                objStatusChangeHistory.RowStatus = 0
                                objStatusChangeHistory.CreatedBy = m_userPrincipal.Identity.Name
                                objStatusChangeHistory.CreatedTime = Date.Now
                                m_TransactionManager.AddInsert(objStatusChangeHistory, m_userPrincipal.Identity.Name)

                                If objStatusChangeHistory.NewStatus = 8 Then
                                    Dim objStatusChangeHistorySendEmailFlag As StatusChangeHistorySendEmailFlag = New StatusChangeHistorySendEmailFlag
                                    objStatusChangeHistorySendEmailFlag.StatusChangeHistory = objStatusChangeHistory
                                    objStatusChangeHistorySendEmailFlag.IsSendEmail = 0
                                    m_TransactionManager.AddInsert(objStatusChangeHistorySendEmailFlag, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 1
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

        Function UpdateTransactionToGroupware(ByVal arrCheckedHeader As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True

                    If Not IsNothing(arrCheckedHeader) Then
                        If arrCheckedHeader.Count > 0 Then
                            For Each oDiscountProposalHeader As DiscountProposalHeader In arrCheckedHeader
                                Dim objDPH As DiscountProposalHeader = m_DiscountProposalHeaderMapper.Retrieve(oDiscountProposalHeader.ID)
                                If IsNothing(objDPH) Then objDPH = New DiscountProposalHeader

                                oDiscountProposalHeader.Status = 5  '-- Status Proses
                                m_TransactionManager.AddUpdate(oDiscountProposalHeader, m_userPrincipal.Identity.Name)

                                Dim objStatusChangeHistory As StatusChangeHistory = New StatusChangeHistory
                                objStatusChangeHistory.DocumentType = 17
                                objStatusChangeHistory.DocumentRegNumber = oDiscountProposalHeader.ProposalRegNo
                                objStatusChangeHistory.OldStatus = objDPH.Status
                                objStatusChangeHistory.NewStatus = oDiscountProposalHeader.Status
                                objStatusChangeHistory.RowStatus = 0
                                objStatusChangeHistory.CreatedBy = m_userPrincipal.Identity.Name
                                objStatusChangeHistory.CreatedTime = Date.Now
                                m_TransactionManager.AddInsert(objStatusChangeHistory, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 1
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


        Public Function InsertTransactionGenerateFiletoGW(ByVal objDiscountProposalHeader As DiscountProposalHeader, _
                                                           ByVal arrDiscountProposalDetailDoc As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If Not IsNothing(arrDiscountProposalDetailDoc) Then
                        If arrDiscountProposalDetailDoc.Count > 0 Then
                            For Each oDiscountProposalDetailDoc As DiscountProposalDetailDocument In arrDiscountProposalDetailDoc
                                oDiscountProposalDetailDoc.DiscountProposalHeader = objDiscountProposalHeader
                                m_TransactionManager.AddInsert(oDiscountProposalDetailDoc, m_userPrincipal.Identity.Name)
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

        Public Function DoRetrieveDataHistoryPembelian(ByVal strCustomerName As String, ByVal strNIKNIB As String)
            Dim strSql As String = String.Empty
            strSql = "EXEC sp_DP_Levenshtein_CustomerNameAndNIKNIB '" & strCustomerName & "', '" & strNIKNIB & "'"
            Return m_DiscountProposalHeaderMapper.RetrieveDataSet(strSql)
        End Function

        Public Function DoRetrieveDataSet(Optional ByVal strCustIDs As String = "") As DataSet
            Dim strSql As String = String.Empty
            strSql = "SELECT j.Name [Model],"
            strSql += " YEAR(b.FakturDate) [Tahun_Faktur], "
            strSql += " count(YEAR(b.FakturDate)) [Jumlah] "
            strSql += " ,b.Name1 [CustomerName],"
            strSql += " e.ProfileValue [NIK_NIB]"
            strSql += " FROM ChassisMaster a"
            strSql += " JOIN EndCustomer b on b.ID = a.EndCustomerID and b.RowStatus = 0"
            strSql += " JOIN Customer c on c.ID = b.CustomerID and c.RowStatus = 0"
            strSql += " JOIN CustomerRequest d on d.CustomerCode = c.Code and d.RowStatus = 0"
            strSql += " JOIN CustomerRequestProfile e on e.CustomerRequestID = d.ID and e.RowStatus = 0 and e.ProfileHeaderID = 29"
            strSql += " JOIN VechileColor f on f.ID = a.VechileColorID and f.RowStatus = 0"
            strSql += " JOIN VechileType g on g.ID = f.VechileTypeID and g.RowStatus = 0"
            strSql += " JOIN VechileModel h on h.ID = g.ModelID and h.RowStatus = 0"
            strSql += " JOIN SubCategoryVehicleToModel i on i.VechileModelID = h.ID and i.RowStatus = 0"
            strSql += " JOIN SubCategoryVehicle j on j.ID = i.SubCategoryVehicleID and j.RowStatus = 0"
            strSql += " WHERE a.RowStatus = 0"
            If strCustIDs <> "" Then
                strSql += " AND c.ID IN (" & strCustIDs & ")"
            End If
            strSql += " GROUP BY j.Name,YEAR(b.FakturDate)"
            strSql += " , b.Name1, e.ProfileValue"
            strSql += " ORDER BY j.Name,YEAR(b.FakturDate)"
            Return m_DiscountProposalHeaderMapper.RetrieveDataSet(strSql)
        End Function

        Public Function RetrieveDataTableForSendMail() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim spName As String = "sp_DP_GetBodyEmailNotification"
            ds = m_DiscountProposalHeaderMapper.RetrieveDataSet(spName)
            If ds.Tables.Count > 0 Then
                dt = ds.Tables(0)
            End If
            Return dt
        End Function

        Function RetrieveByFleetCustomerHeader(ByVal objFCH As FleetCustomerHeader) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DiscountProposalHeader), "FleetCustomerDetail.FleetCustomerHeader.ID", MatchType.Exact, objFCH.ID))

            Return m_DiscountProposalHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Function RetrieveByFleetCustomerDetail(ByVal objFCD As FleetCustomerDetail) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DiscountProposalHeader), "FleetCustomerDetail.ID", MatchType.Exact, objFCD.ID))

            Return m_DiscountProposalHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function InsertDataLogForSendMail(ByVal message As String, ByVal token As String, ByVal isSuccess As Integer) As Integer
            Dim spName As String = "sp_InsertToTableLogSendEmail"
            Dim _param1 = New SqlClient.SqlParameter
            Dim _param2 = New SqlClient.SqlParameter
            Dim _param3 = New SqlClient.SqlParameter
            Dim _param4 = New SqlClient.SqlParameter
            Dim arrParam As New ArrayList

            _param1.DbType = DbType.AnsiString
            _param1.Value = message
            _param1.ParameterName = "@ErrorMessage"
            arrParam.Add(_param1)

            _param2.DbType = DbType.AnsiString
            _param2.Value = token
            _param2.ParameterName = "@Token"
            arrParam.Add(_param2)

            _param3.DbType = DbType.Int16
            _param3.Value = isSuccess
            _param3.ParameterName = "@IsSuccess"
            arrParam.Add(_param3)

            _param4.DbType = DbType.Int16
            _param4.Value = 0
            _param4.ParameterName = "@ID"
            arrParam.Add(_param4)

            Dim _results As Integer = m_DiscountProposalHeaderMapper.ExecuteSP(spName, arrParam)

            Return _results
        End Function

       
#End Region



    End Class

End Namespace


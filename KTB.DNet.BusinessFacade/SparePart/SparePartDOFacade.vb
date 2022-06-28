
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
'// Copyright  2016
'// ---------------------
'// $History      : $
'// Generated on 9/27/2016 - 11:43:51 AM
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

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class SparePartDOFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartDOMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartDOMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartDO).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(SparePartDO))
            Me.DomainTypeCollection.Add(GetType(SparePartDODetail))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartDO
            Return CType(m_SparePartDOMapper.Retrieve(ID), SparePartDO)
        End Function

        Public Function Retrieve(ByVal DONumber As String) As SparePartDO
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartDO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartDO), "DONumber", MatchType.Exact, DONumber))
            Dim arlDO As ArrayList = m_SparePartDOMapper.RetrieveByCriteria(criterias)
            If arlDO.Count > 0 Then
                Return CType(arlDO(0), SparePartDO)
            End If
            Return Nothing
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartDOMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartDOMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartDOMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartDO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartDOMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartDO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartDOMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartDO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SparePartDO As ArrayList = m_SparePartDOMapper.RetrieveByCriteria(criterias)
            Return _SparePartDO
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartDO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartDOColl As ArrayList = m_SparePartDOMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartDOColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartDOColl As ArrayList = m_SparePartDOMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartDOColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartDO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartDOColl As ArrayList = m_SparePartDOMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SparePartDO), columnName, matchOperator, columnValue))
            Return SparePartDOColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartDO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartDO), columnName, matchOperator, columnValue))

            Return m_SparePartDOMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As SparePartDO) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SparePartDOMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartDO) As Integer
            Dim nResult As Integer = -1
            Dim isChange As New IsChangeFacade
            Try

                'farid additional 20180316

                Dim criSPDO As New CriteriaComposite(New Criteria(GetType(SparePartDO), "Dealer.ID", MatchType.Exact, CType(objDomain.Dealer.ID, Integer)))
                criSPDO.opAnd(New Criteria(GetType(SparePartDO), "DONumber", MatchType.Exact, CType(objDomain.DONumber, String)))
                criSPDO.opAnd(New Criteria(GetType(SparePartDO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                Dim arlSPDO As New ArrayList
                arlSPDO = New SparePartDOFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criSPDO)

                Dim objSPDO_DB As New SparePartDO
                objSPDO_DB = CType(arlSPDO.Item(0), SparePartDO)

                If isChange.ISchangeSparePartDO(objDomain, objSPDO_DB) Then
                    nResult = m_SparePartDOMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                End If

            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartDO)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SparePartDOMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SparePartDO)
            Try
                m_SparePartDOMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

        Public Function InsertFromWebSevice(ByVal spDO As SparePartDO) As Short
            Dim returnValue As Integer = -1
            Dim isChange As New IsChangeFacade
            If Me.IsTaskFree() Then
                Try
                    '  Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim spDO_old As SparePartDO = ValidateSparePartDO(spDO)

                    If IsNothing(spDO_old) Then
                        m_TransactionManager.AddInsert(spDO, m_userPrincipal.Identity.Name)
                        If spDO.SparePartDODetails.Count > 0 Then
                            For Each itemDetail As SparePartDODetail In spDO.SparePartDODetails
                                itemDetail.SparePartDO = spDO
                                m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    Else
                        For Each itemDetail_Old As SparePartDODetail In spDO_old.SparePartDODetails
                            itemDetail_Old.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(itemDetail_Old, m_userPrincipal.Identity.Name)
                        Next

                        For Each itemDetail As SparePartDODetail In spDO.SparePartDODetails

                            'Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartDODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartDODetail), "SparePartDO.ID", MatchType.Exact, spDO_old.ID))
                            criterias.opAnd(New Criteria(GetType(SparePartDODetail), "SparePartMaster.ID", MatchType.Exact, itemDetail.SparePartMaster.ID))
                            criterias.opAnd(New Criteria(GetType(SparePartDODetail), "SparePartPOEstimate.ID", MatchType.Exact, itemDetail.SparePartPOEstimate.ID))
                            Dim arlSparePartDODetails As ArrayList = New SparePartDODetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

                            If arlSparePartDODetails.Count > 0 Then
                                Dim oSPDODetDB As SparePartDODetail = New SparePartDODetail
                                oSPDODetDB = CType(arlSparePartDODetails(0), SparePartDODetail)


                                ' this line is commanded where condition rowstatys has been updated to be -1 before. so
                                ' If isChange.ISchangeSparePartDODetail(itemDetail,oSPDODetDB)  Then

                                oSPDODetDB.Qty = itemDetail.Qty
                                oSPDODetDB.ItemNoDO = itemDetail.ItemNoDO
                                oSPDODetDB.ItemNoSO = itemDetail.ItemNoSO
                                oSPDODetDB.RowStatus = CType(DBRowStatus.Active, Short)

                                m_TransactionManager.AddUpdate(oSPDODetDB, m_userPrincipal.Identity.Name)
                            Else
                                itemDetail.SparePartDO = spDO_old
                                m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                            End If
                        Next

                        'Farid Additonal 20180315
                        If isChange.ISchangeSparePartDO(spDO, spDO_old) Then

                            spDO_old.DoDate = spDO.DoDate
                            spDO_old.EstmationDeliveryDate = spDO.EstmationDeliveryDate
                            spDO_old.PickingDate = spDO.PickingDate
                            spDO_old.PackingDate = spDO.PackingDate
                            'spDO_old.PaymentDate = spDO.PaymentDate
                            spDO_old.GoodIssueDate = spDO.GoodIssueDate
                            spDO_old.PaymentDate = spDO.PaymentDate
                            spDO_old.ReadyForDeliveryDate = spDO.ReadyForDeliveryDate

                            m_TransactionManager.AddUpdate(spDO_old, m_userPrincipal.Identity.Name)
                        End If
                    End If
                    'update rowstatus = -1 for old detail

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
                    '    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Function DeleteFromWebSevice(ByVal spDO As SparePartDO) As Short
            Dim returnValue As Integer = -1
            If Me.IsTaskFree() Then
                Try
                    'Me.SetTaskLocking()
                    Dim performtransaction As Boolean = True
                    Dim objmapper As IMapper

                    Dim spDO_DB As SparePartDO = ValidateSparePartDO(spDO)
                    If Not IsNothing(spDO_DB) Then
                        For Each itemDetail As SparePartDODetail In spDO_DB.SparePartDODetails
                            itemDetail.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(itemDetail, m_userPrincipal.Identity.Name)
                        Next
                        spDO_DB.RowStatus = CType(DBRowStatus.Deleted, Short)
                        m_TransactionManager.AddUpdate(spDO_DB, m_userPrincipal.Identity.Name)

                        If performtransaction Then
                            m_TransactionManager.PerformTransaction()
                            returnValue = 0
                        End If
                    End If
                    
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    'Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is SparePartDO) Then
                CType(InsertArg.DomainObject, SparePartDO).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SparePartDO).MarkLoaded()
            End If
        End Sub
#End Region

#Region "Customs"

        Public Function ValidateSparePartDO(ByVal SparePartDO As SparePartDO) As SparePartDO
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartDO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartDO), "Dealer", MatchType.Exact, SparePartDO.Dealer.ID))
            criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartDO), "DONumber", MatchType.Exact, SparePartDO.DONumber))
            Dim arlSparePartDO As ArrayList = m_SparePartDOMapper.RetrieveByCriteria(criteria)
            If arlSparePartDO.Count > 0 Then
                Return CType(arlSparePartDO(0), SparePartDO)
            End If
            Return Nothing

        End Function

        Private Function MergeDataSparePartDO(ByRef objOrigspDODetails As ArrayList, ByVal objNewspDODetails As ArrayList)
            For Each objItemDetail As SparePartDODetail In objOrigspDODetails
                objItemDetail.RowStatus = DBRowStatus.Deleted
            Next
            For Each itemDetail As SparePartDODetail In objNewspDODetails
                Dim objOriItemDetail As SparePartDODetail = GetItemDoDetail(itemDetail.SparePartDO.DONumber, objOrigspDODetails)
                If Not IsNothing(objOriItemDetail) Then
                    objOriItemDetail.RowStatus = DBRowStatus.Active
                    objOriItemDetail.SparePartPOEstimate = itemDetail.SparePartPOEstimate
                    objOriItemDetail.SparePartMaster = itemDetail.SparePartMaster
                    objOriItemDetail.Qty = itemDetail.Qty
                    objOriItemDetail.ItemNoSO = itemDetail.ItemNoSO
                    objOriItemDetail.ItemNoDO = itemDetail.ItemNoDO
                Else
                    objOrigspDODetails.Add(itemDetail)
                End If
            Next
        End Function

        Private Function GetItemDoDetail(ByVal spMasterNumber As String, ByVal arlOrigSparePartDODetail As ArrayList) As SparePartDODetail
            For Each itemDetail As SparePartDODetail In arlOrigSparePartDODetail
                If itemDetail.SparePartMaster.PartNumber = spMasterNumber Then
                    Return itemDetail
                End If
            Next
            Return Nothing
        End Function

#End Region

    End Class

End Namespace


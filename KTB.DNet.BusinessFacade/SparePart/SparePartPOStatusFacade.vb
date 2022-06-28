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
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Collections.Generic

#End Region

Namespace KTB.DNET.BusinessFacade.SparePart
    Public Class SparePartPOStatusFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartPOStatusMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"



        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartPOStatusMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartPOStatus).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf TransactionManager_Insert)

            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.SparePartPOStatus))
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.SparePartPOStatusDetail))
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.SparePartPO))

        End Sub

#End Region

#Region "Retrieve"
        Public Function RetrieveList(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartPOStatusMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPOStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartPOStatus), "BillingNumber", MatchType.No, ""))

            Dim SparePartPOStatusColl As ArrayList = m_SparePartPOStatusMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SparePartPOStatusColl
        End Function

        Public Function RetrieveActiveListPerDealer(ByVal DealerID As Integer, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList


            'Dim criteriaDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criteriaDealer.opAnd(New Criteria(GetType(SparePartPO), "Dealer.ID", MatchType.Exact, DealerID))

            'Dim arlPO As ArrayList = New SparePartPOFacade(m_userPrincipal).Retrieve(criteriaDealer)

            'Dim strPOToInclude As String = ""

            'For Each itemPO As SparePartPO In arlPO
            '    strPOToInclude = strPOToInclude & itemPO.ID.ToString & ","
            'Next

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPOStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartPOStatus), "BillingNumber", MatchType.No, ""))

            Dim SparePartPOStatusColl As ArrayList
            criterias.opAnd(New Criteria(GetType(SparePartPOStatus), "SparePartPO.ID", MatchType.InSet, "(Select ID from SparePartPO where DealerID=" & DealerID & ")"))

            SparePartPOStatusColl = m_SparePartPOStatusMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            'If strPOToInclude <> "" Then
            '    strPOToInclude = Left(strPOToInclude, strPOToInclude.Length - 1)
            '    criterias.opAnd(New Criteria(GetType(SparePartPOStatus), "SparePartPO.ID", MatchType.InSet, "(" & strPOToInclude & ")"))
            '    SparePartPOStatusColl = m_SparePartPOStatusMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            'Else
            '    SparePartPOStatusColl = New ArrayList
            'End If


            Return SparePartPOStatusColl
        End Function

        Public Function RetrieveActiveListPerDealerClaim(ByVal DealerID As Integer, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList


            'Dim criteriaDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criteriaDealer.opAnd(New Criteria(GetType(SparePartPO), "Dealer.ID", MatchType.Exact, DealerID))

            'Dim arlPO As ArrayList = New SparePartPOFacade(m_userPrincipal).Retrieve(criteriaDealer)

            'Dim strPOToInclude As String = ""

            'For Each itemPO As SparePartPO In arlPO
            '    strPOToInclude = strPOToInclude & itemPO.ID.ToString & ","
            'Next

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPOStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartPOStatus), "BillingNumber", MatchType.No, ""))
            criterias.opAnd(New Criteria(GetType(SparePartPOStatus), "BillingDate", MatchType.GreaterOrEqual, New Date(2007, 4, 1)))

            Dim SparePartPOStatusColl As ArrayList
            criterias.opAnd(New Criteria(GetType(SparePartPOStatus), "SparePartPO.ID", MatchType.InSet, "(Select ID from SparePartPO where DealerID=" & DealerID & ")"))

            SparePartPOStatusColl = m_SparePartPOStatusMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            'If strPOToInclude <> "" Then
            '    strPOToInclude = Left(strPOToInclude, strPOToInclude.Length - 1)
            '    criterias.opAnd(New Criteria(GetType(SparePartPOStatus), "SparePartPO.ID", MatchType.InSet, "(" & strPOToInclude & ")"))
            '    SparePartPOStatusColl = m_SparePartPOStatusMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            'Else
            '    SparePartPOStatusColl = New ArrayList
            'End If


            Return SparePartPOStatusColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartPOStatusColl As ArrayList = m_SparePartPOStatusMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartPOStatusColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartPOStatusColl As ArrayList = m_SparePartPOStatusMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SparePartPOStatus), columnName, matchOperator, columnValue))
            Return SparePartPOStatusColl
        End Function


        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPOStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOStatus), columnName, matchOperator, columnValue))
            criterias.opAnd(New Criteria(GetType(SparePartPOStatus), "BillingNumber", MatchType.No, ""))
            Return m_SparePartPOStatusMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveWithOneCriteriaPerDealerClaim(ByVal DealerID As Integer, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            'Todo Inset
            'Dim criteriaDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criteriaDealer.opAnd(New Criteria(GetType(SparePartPO), "Dealer.ID", MatchType.Exact, DealerID))

            'Dim arlPO As ArrayList = New SparePartPOFacade(m_userPrincipal).Retrieve(criteriaDealer)

            'Dim strPOToInclude As String = ""

            'For Each itemPO As SparePartPO In arlPO
            '    strPOToInclude = strPOToInclude & itemPO.ID.ToString & ","
            'Next

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPOStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            'If arlPO.Count > 0 Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOStatus), columnName, matchOperator, columnValue))
            criterias.opAnd(New Criteria(GetType(SparePartPOStatus), "BillingNumber", MatchType.No, ""))
            criterias.opAnd(New Criteria(GetType(SparePartPOStatus), "BillingDate", MatchType.GreaterOrEqual, New Date(2007, 4, 1)))

            'strPOToInclude = Left(strPOToInclude, strPOToInclude.Length - 1)
            criterias.opAnd(New Criteria(GetType(SparePartPOStatus), "SparePartPO.ID", MatchType.InSet, "(" & "select id from sparepartpo where dealerid='" & DealerID & "'" & ")"))
            Return m_SparePartPOStatusMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            'Else
            '    Return New ArrayList
            'End If

        End Function

        Public Function RetrieveWithOneCriteriaPerDealer(ByVal DealerID As Integer, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim criteriaDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriaDealer.opAnd(New Criteria(GetType(SparePartPO), "Dealer.ID", MatchType.Exact, DealerID))

            Dim arlPO As ArrayList = New SparePartPOFacade(m_userPrincipal).Retrieve(criteriaDealer)

            Dim strPOToInclude As String = ""

            For Each itemPO As SparePartPO In arlPO
                strPOToInclude = strPOToInclude & itemPO.ID.ToString & ","
            Next

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPOStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            If strPOToInclude <> "" Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOStatus), columnName, matchOperator, columnValue))
                criterias.opAnd(New Criteria(GetType(SparePartPOStatus), "BillingNumber", MatchType.No, ""))

                strPOToInclude = Left(strPOToInclude, strPOToInclude.Length - 1)
                criterias.opAnd(New Criteria(GetType(SparePartPOStatus), "SparePartPO.ID", MatchType.InSet, "(" & strPOToInclude & ")"))
                Return m_SparePartPOStatusMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Else
                Return New ArrayList
            End If

        End Function


        Public Function RetrieveActiveList(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If Not IsNothing(sortColumn) And sortColumn <> "" Then
                sortColl.Add(New Sort(GetType(SparePartPOStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartPOStatusMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortTable As Type, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If Not IsNothing(sortColumn) And sortColumn <> "" Then
                sortColl.Add(New Sort(sortTable, sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartPOStatusMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function Retrieve(ByVal SparePartPOStatusID As Integer) As SparePartPOStatus
            Return CType(m_SparePartPOStatusMapper.Retrieve(SparePartPOStatusID), KTB.DNET.Domain.SparePartPOStatus)
        End Function

        Public Function Retrieve(ByVal NoFaktur As String) As SparePartPOStatus
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOStatus), "BillingNumber", MatchType.Exact, NoFaktur))

            Dim arlSPPOStatus As ArrayList = m_SparePartPOStatusMapper.RetrieveByCriteria(criterias)
            If arlSPPOStatus.Count > 0 Then
                Return CType(arlSPPOStatus(0), SparePartPOStatus)
            End If
            Return Nothing
        End Function

        Public Function RetrieveSO(ByVal NoFaktur As String, ByVal NoSO As String) As SparePartPOStatus
            '  Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOStatus), "BillingNumber", MatchType.Exact, NoFaktur))
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.SparePartPOStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.SparePartPOStatus), "BillingNumber", MatchType.Exact, NoFaktur))
            criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.SparePartPOStatus), "SONumber", MatchType.Exact, NoSO))

            Dim arlSPPOStatus As ArrayList = m_SparePartPOStatusMapper.RetrieveByCriteria(criterias)
            If arlSPPOStatus.Count > 0 Then
                Return CType(arlSPPOStatus(0), SparePartPOStatus)
            End If
            Return Nothing
        End Function

        Public Function RetrievePO(ByVal NoFaktur As String, ByVal NoSO As String) As SparePartPOStatus
            '  Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOStatus), "BillingNumber", MatchType.Exact, NoFaktur))
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.SparePartPOStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.SparePartPOStatus), "BillingNumber", MatchType.Exact, NoFaktur))
            criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.SparePartPOStatus), "SONumber", MatchType.Exact, NoSO))

            Dim arlSPPOStatus As ArrayList = m_SparePartPOStatusMapper.RetrieveByCriteria(criterias)
            If arlSPPOStatus.Count > 0 Then
                Return CType(arlSPPOStatus(0), SparePartPOStatus)
            End If
            Return Nothing
        End Function

        Public Function RetrieveBilling(ByVal BillingNumber As String) As SparePartPOStatus
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.SparePartPOStatus), "BillingNumber", MatchType.Exact, BillingNumber))
            Dim arlSPPOStatus As ArrayList = m_SparePartPOStatusMapper.RetrieveByCriteria(criteria)
            If arlSPPOStatus.Count > 0 Then
                Return CType(arlSPPOStatus(0), SparePartPOStatus)
            End If
            Return Nothing

        End Function

        Public Function ValidateSPPO(ByVal SparePartPOID As Integer) As SparePartPOStatus
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.SparePartPOStatus), "SparePartPO.ID", MatchType.Exact, SparePartPOID))
            Dim arlSPPOStatus As ArrayList = m_SparePartPOStatusMapper.RetrieveByCriteria(criteria)
            If arlSPPOStatus.Count > 0 Then
                Return CType(arlSPPOStatus(0), SparePartPOStatus)
            End If
            Return Nothing
        End Function

        Public Function IsPOValid(ByVal spPOID As Integer) As SparePartPOStatus
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.SparePartPOStatus), "SparePartPOID", MatchType.Exact, spPOID))
            Dim arlSPPOStatus As ArrayList = m_SparePartPOStatusMapper.RetrieveByCriteria(criteria)
            If arlSPPOStatus.Count > 0 Then
                Return CType(arlSPPOStatus(0), SparePartPOStatus)
            End If
            Return Nothing
        End Function

        Public Function CountPOValid(ByVal spPOID As Integer) As Integer
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.SparePartPOStatus), "SparePartPOID", MatchType.Exact, spPOID))
            Dim arlSPPOStatus As ArrayList = m_SparePartPOStatusMapper.RetrieveByCriteria(criteria)
            Return arlSPPOStatus.Count()
        End Function

        Public Function Retrieve(ByVal crit As CriteriaComposite) As SparePartPOStatus
            Dim arlSPPOStatus As ArrayList = m_SparePartPOStatusMapper.RetrieveByCriteria(crit)
            If arlSPPOStatus.Count > 0 Then
                Return CType(arlSPPOStatus(0), SparePartPOStatus)
            End If
            Return Nothing
        End Function

        Public Function RetrieveScalar(ByVal agregate As Aggregate, ByVal criteria As CriteriaComposite) As Integer
            Return m_SparePartPOStatusMapper.RetrieveScalar(agregate, criteria)
        End Function
#End Region

#Region "Transaction/Other Public Method"


        Private Function GetItemPOStatus(ByVal spMasterID As Integer, ByVal arlOrigSPPODStatusDetail As ArrayList) As SparePartPOStatusDetail
            For Each itemDetail As SparePartPOStatusDetail In arlOrigSPPODStatusDetail
                If itemDetail.SparePartMaster.ID = spMasterID Then
                    Return itemDetail
                End If
            Next
            Return Nothing
        End Function

        Private Function MergeDataSPPOStatus(ByRef objOrigSPPOStatusDetails As ArrayList, ByVal objNewSPPOStatusDetails As ArrayList)
            For Each objItemDetail As SparePartPOStatusDetail In objOrigSPPOStatusDetails
                Dim objOriItemDetail As SparePartPOStatusDetail = GetItemPOStatus(objItemDetail.SparePartMaster.ID, objNewSPPOStatusDetails)
                If Not IsNothing(objOriItemDetail) Then
                    objItemDetail.IsChangedWSM = False
                Else
                    objItemDetail.RowStatus = DBRowStatus.Deleted
                    objItemDetail.IsChangedWSM = True
                End If
            Next
            For Each itemDetail As SparePartPOStatusDetail In objNewSPPOStatusDetails
                Dim objOriItemDetail As SparePartPOStatusDetail = GetItemPOStatus(itemDetail.SparePartMaster.ID, objOrigSPPOStatusDetails)
                If Not IsNothing(objOriItemDetail) Then
                    If objOriItemDetail.RowStatus <> DBRowStatus.Active Then
                        objOriItemDetail.RowStatus = DBRowStatus.Active
                        objOriItemDetail.IsChangedWSM = True
                    End If

                    If objOriItemDetail.BillingQuantity <> itemDetail.BillingQuantity Then
                        objOriItemDetail.BillingQuantity = itemDetail.BillingQuantity
                        objOriItemDetail.IsChangedWSM = True
                    End If

                    If objOriItemDetail.NetPrice <> itemDetail.NetPrice Then
                        objOriItemDetail.NetPrice = itemDetail.NetPrice
                        objOriItemDetail.IsChangedWSM = True
                    End If

                    If objOriItemDetail.SOQuantity <> itemDetail.SOQuantity Then
                        objOriItemDetail.SOQuantity = itemDetail.SOQuantity
                        objOriItemDetail.IsChangedWSM = True
                    End If

                    If objOriItemDetail.BillingPrice <> itemDetail.BillingPrice Then
                        objOriItemDetail.BillingPrice = itemDetail.BillingPrice
                        objOriItemDetail.IsChangedWSM = True
                    End If

                    If objOriItemDetail.DONumber <> itemDetail.DONumber Then
                        objOriItemDetail.DONumber = itemDetail.DONumber
                        objOriItemDetail.IsChangedWSM = True
                    End If

                Else
                    objOrigSPPOStatusDetails.Add(itemDetail)
                End If
            Next
        End Function

        Private Function IsValidPOStatus(ByVal oSPPOS As SparePartPOStatus)
            'IsValidPOStatus, by donas for AA on 2015.09.11  
            'before : PO-----SO-----DO----Billing-->Claim 
            'after  : PO----(n)SO(n)-----DO----Billing-->Claim
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.SparePartPOStatus), "SparePartPOID", MatchType.Exact, oSPPOS.SparePartPO.ID))
            criteria.opAnd(New Criteria(GetType(SparePartPOStatus), "SONumber", MatchType.Exact, oSPPOS.SONumber))
            'criteria.opAnd(New Criteria(GetType(SparePartPOStatus), "BillingNumber", MatchType.Exact, oSPPOS.BillingNumber))

            Dim arlSPPOStatus As ArrayList = m_SparePartPOStatusMapper.RetrieveByCriteria(criteria)
            If arlSPPOStatus.Count > 0 Then
                Return CType(arlSPPOStatus(0), SparePartPOStatus)
            End If
            Return Nothing
        End Function

        Public Function InsertFromWindowSevice(ByVal spPOStatus As SparePartPOStatus)
            Dim returnValue As Integer = -1
            If Me.IsTaskFree() Then
                Try

                    Me.SetTaskLocking()
                    Dim spPOValidate As SparePartPOStatus = IsValidPOStatus(spPOStatus) ' IsPOValid(spPOStatus.SparePartPO.ID)
                    Dim spPO As SparePartPO = spPOStatus.SparePartPO

                    spPO.IsChangedWSM = False
                    If spPO.ProcessCode <> "P" Then
                        spPO.ProcessCode = "P"
                        spPO.IsChangedWSM = True
                    End If

                    If IsNothing(spPOValidate) Then
                        m_TransactionManager.AddInsert(spPOStatus, "WSM")
                        For Each itemDetail As SparePartPOStatusDetail In spPOStatus.SparePartPOStatusDetails
                            itemDetail.SparePartPOStatus = spPOStatus
                            m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                        Next
                    Else
                        spPOStatus.IsChangedWSM = False

                        'If spPOValidate.SONumber <> spPOStatus.SONumber Then
                        '    spPOValidate.SONumber = spPOStatus.SONumber
                        '    spPOStatus.IsChangedWSM = True
                        'End If

                        If spPOValidate.SODate <> spPOStatus.SODate Then
                            spPOValidate.SODate = spPOStatus.SODate
                            spPOStatus.IsChangedWSM = True
                        End If

                        If spPOValidate.DeliveryDate <> spPOStatus.DeliveryDate Then
                            spPOValidate.DeliveryDate = spPOStatus.DeliveryDate
                            spPOStatus.IsChangedWSM = True
                        End If

                        If spPOValidate.BillingNumber <> spPOStatus.BillingNumber Then
                            spPOValidate.BillingNumber = spPOStatus.BillingNumber
                            spPOStatus.IsChangedWSM = True
                        End If

                        If spPOValidate.SparePartPO.ID <> spPOStatus.SparePartPO.ID Then
                            spPOStatus.IsChangedWSM = True
                        End If

                        If spPOValidate.PackingStatus <> spPOStatus.PackingStatus Then
                            spPOStatus.IsChangedWSM = True
                        End If

                        If spPOValidate.BillingDate <> spPOStatus.BillingDate Then
                            spPOStatus.IsChangedWSM = True
                        End If

                        If spPOValidate.RowStatus <> spPOStatus.RowStatus Then
                            spPOStatus.IsChangedWSM = True
                        End If


                        spPOStatus.ID = spPOValidate.ID

                        Dim arlSPPOStatusDetail As ArrayList = MergeDataSPPOStatus(spPOValidate.SparePartPOStatusDetails, spPOStatus.SparePartPOStatusDetails)
                        For Each itemDetail As SparePartPOStatusDetail In spPOValidate.SparePartPOStatusDetails
                            itemDetail.SparePartPOStatus = spPOStatus
                            If itemDetail.ID = 0 Then
                                m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                            Else
                                If itemDetail.IsChangedWSM Then
                                    m_TransactionManager.AddUpdate(itemDetail, m_userPrincipal.Identity.Name)
                                End If
                            End If
                        Next
                        If spPOStatus.IsChangedWSM Then
                            m_TransactionManager.AddUpdate(spPOStatus, m_userPrincipal.Identity.Name)
                        End If
                    End If

                    If spPO.IsChangedWSM Then
                        m_TransactionManager.AddUpdate(spPO, m_userPrincipal.Identity.Name)
                    End If

                    m_TransactionManager.PerformTransaction()
                    UnblockTOP(spPOStatus.ID)
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

        Private Sub TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.SparePartPOStatus) Then

                CType(InsertArg.DomainObject, KTB.DNET.Domain.SparePartPOStatus).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.SparePartPOStatus).MarkLoaded()

            End If

        End Sub

        Public Function Insert(ByVal objDomain As SparePartPOStatus) As Integer
            Dim iReturn As Integer
            iReturn = m_SparePartPOStatusMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As SparePartPOStatus) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartPOStatusMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As SparePartPOStatus) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartPOStatusMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

#End Region

        Private Sub UnblockTOP(ByVal SPPOID As Integer)

            Try
                Dim strSPName As String = "sp_UpdateTOPBlockStatus_WSM"
                Dim Param As New List(Of SqlClient.SqlParameter)

                Param.Add(New SqlClient.SqlParameter("@ID", SPPOID))
                Param.Add(New SqlClient.SqlParameter("@CreatedBy", m_userPrincipal.Identity.Name))

                m_SparePartPOStatusMapper.ExecuteSP(strSPName, New ArrayList(Param))
            Catch ex As Exception

            End Try
        End Sub

    End Class
End Namespace



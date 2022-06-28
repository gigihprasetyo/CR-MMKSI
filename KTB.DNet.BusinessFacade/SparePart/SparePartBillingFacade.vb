
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
'// Generated on 9/27/2016 - 11:41:17 AM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Linq

#End Region

#Region "Custom Namespace"

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Framework
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Collections.Generic


#End Region

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class SparePartBillingFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartBillingMapper As IMapper
        'Private m_SparePartBillingDetailMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartBillingMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartBilling).ToString)
            'Me.m_SparePartBillingDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartBillingDetail).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(SparePartBilling))
            Me.DomainTypeCollection.Add(GetType(SparePartBillingDetail))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartBilling
            Return CType(m_SparePartBillingMapper.Retrieve(ID), SparePartBilling)
        End Function

        Public Function Retrieve(ByVal BillingNumber As String) As SparePartBilling
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartBilling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartBilling), "BillingNumber", MatchType.Exact, BillingNumber))
            Dim arlBilling As ArrayList = m_SparePartBillingMapper.RetrieveByCriteria(criterias)
            If arlBilling.Count > 0 Then
                Return CType(arlBilling(0), SparePartBilling)
            End If
            Return Nothing
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartBillingMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartBillingMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartBillingMapper.RetrieveList
        End Function

        Public Function Retrieve(Creditaccount As String, tglFromDuedate As Date, tglToDueDate As Date, Optional ByVal DealerCode As String = "",
                                 Optional ByVal NomorBilling As String = "", Optional ByVal NomorReg As String = "", Optional ByVal varAmount As Double = 0) As DataSet


            'Dim query As String = "exec up_Retrieve_OutStandingBilling '" + tglFromDuedate.ToString("yyyyMMdd") + "','" + tglToDueDate.ToString("yyyyMMdd") + "','" + Creditaccount + "'"



            'dtbs = m_SparePartBillingMapper.RetrieveDataSet(query)

            'If dtbs.Tables.Count > 0 Then
            '    Return dtbs
            'Else
            '    Return Nothing
            'End If
            Dim dtbs As New DataSet

            Dim strSPName As String = "up_Retrieve_OutStandingBilling"
            Dim Param As New List(Of SqlClient.SqlParameter)

            Param.Add(New SqlClient.SqlParameter("@DueDateStart ", tglFromDuedate))
            Param.Add(New SqlClient.SqlParameter("@DueDateEnd", tglToDueDate))
            Param.Add(New SqlClient.SqlParameter("@creacc", Creditaccount))
            Param.Add(New SqlClient.SqlParameter("@DealerCode", DealerCode))
            Param.Add(New SqlClient.SqlParameter("@NomorBilling", NomorBilling))
            Param.Add(New SqlClient.SqlParameter("@NomorReg", NomorReg))
            Param.Add(New SqlClient.SqlParameter("@varAmount", varAmount))
            dtbs = m_SparePartBillingMapper.RetrieveDataSet(strSPName, New ArrayList(Param))

            If dtbs.Tables.Count > 0 Then
                Return dtbs
            Else
                Return Nothing
            End If

        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite,
                                           ByVal pageNumber As Integer,
                                           ByVal pageSize As Integer,
                                           ByRef totalRow As Integer,
                                           ByVal sortColumn As String,
                                           ByVal sortDirection As Sort.SortDirection,
                                           ByVal icTanggalJatuhTempoFrom As Date,
                                           ByVal icTanggalJatuhTempountil As Date,
                                           ByVal lblCreditAccount As String) As ArrayList

            Dim arrsppbill As ArrayList
            Dim arrFinal As New ArrayList
            Dim strDeposit As String = "(select sparepartbillingID from TOPSPDeposit where rowstatus= 0)"
            Dim strQuery As String
            strQuery = "(select sparepartbillingID from topspDueDate (nolock) "
            strQuery = strQuery + " where rowstatus = 0 and duedate between '" & icTanggalJatuhTempoFrom.ToString("yyyyMMdd") & "' and '" & icTanggalJatuhTempountil.ToString("yyyyMMdd") & "')"

            Dim str As String = "(select d.sparepartbillingid from topsptransferpaymentdetail d (nolock) "
            str = str & " join topsptransferpayment (nolock) h on h.id = d.topsptransferpaymentid "
            str = str & " where d.rowstatus = 0 and h.rowstatus = 0 and status not in (select valueid from StandardCode where Category = 'EnumTOPSPTransferPayment.Status' and RowStatus = 0 and (ValueDesc LIKE '%Batal%' )) )"

            Dim cri As New CriteriaComposite(New Criteria(GetType(SparePartBilling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cri.opAnd(New Criteria(GetType(SparePartBilling), "Dealer.CreditAccount", MatchType.Exact, lblCreditAccount))
            cri.opAnd(New Criteria(GetType(SparePartBilling), "ID", MatchType.InSet, strQuery))
            cri.opAnd(New Criteria(GetType(SparePartBilling), "ID", MatchType.InSet, strDeposit))
            cri.opAnd(New Criteria(GetType(SparePartBilling), "ID", MatchType.InSet, str))


            arrsppbill = m_SparePartBillingMapper.RetrieveByCriteria(cri)

            str = ""
            Dim objspbill As SparePartBilling
            For x As Integer = 0 To arrsppbill.Count - 1
                objspbill = New SparePartBilling
                objspbill = arrsppbill.Item(x)

                If x = arrsppbill.Count - 1 Then
                    str = str + objspbill.ID.ToString
                Else
                    str = str + objspbill.ID.ToString + ","
                End If
            Next
            str = str + ""

            If str.Length > 5 Then
                Criterias.opAnd(New Criteria(GetType(SparePartBilling), "ID", MatchType.NotInSet, str))
            End If

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartBilling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim _spBilling As New ArrayList
            _spBilling = m_SparePartBillingMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)

            'For Each row As SparePartBilling In _spBilling
            '    Dim lin = (From arrbillpage As SparePartBilling In arrsppbill.Cast(Of SparePartBilling)() Where arrbillpage.ID = row.ID
            '        Select arrbillpage).Count

            '    If lin = 0 Then
            '        arrFinal.Add(row)
            '    End If
            'Next

            Return _spBilling
        End Function

        Public Function RetrieveList(ByVal criterias As ICriteria, ByVal sortColl As SortCollection) As ArrayList
            'Dim sortColl As SortCollection = New SortCollection

            'If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
            '    sortColl.Add(New Sort(GetType(SparePartBilling), sortColumn, sortDirection))
            'Else
            '    sortColl = Nothing
            'End If

            Return m_SparePartBillingMapper.RetrieveByCriteria(criterias, sortColl)
        End Function


        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartBilling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartBillingMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartBilling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartBillingMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartBilling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SparePartBilling As ArrayList = m_SparePartBillingMapper.RetrieveByCriteria(criterias)
            Return _SparePartBilling
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartBilling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartBillingColl As ArrayList = m_SparePartBillingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartBillingColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartBillingColl As ArrayList = m_SparePartBillingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartBillingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartBilling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartBillingColl As ArrayList = m_SparePartBillingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SparePartBilling), columnName, matchOperator, columnValue))
            Return SparePartBillingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartBilling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartBilling), columnName, matchOperator, columnValue))

            Return m_SparePartBillingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As SparePartBilling) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SparePartBillingMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartBilling) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartBillingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartBilling)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SparePartBillingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SparePartBilling)
            Try
                m_SparePartBillingMapper.Delete(objDomain)
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

            If (TypeOf InsertArg.DomainObject Is SparePartBilling) Then
                CType(InsertArg.DomainObject, SparePartBilling).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SparePartBilling).MarkLoaded()
            End If
        End Sub


        Public Function InsertFromWebSevice_Old(ByVal spBill As SparePartBilling) As Short
            Try
                Dim objDetailFacade As SparePartBillingDetailFacade = New SparePartBillingDetailFacade(m_userPrincipal)
                Dim doNUmber As String = ""
                For Each itemBill As SparePartBillingDetail In spBill.SparePartBillingDetails
                    doNUmber = itemBill.SparePartDODetail.SparePartDO.DONumber
                Next

                Dim spBill_old As SparePartBilling
                Dim arlBillDetail As ArrayList = objDetailFacade.Retrieve(doNUmber)
                If arlBillDetail.Count > 0 Then
                    spBill_old = CType(arlBillDetail(0), SparePartBillingDetail).SparePartBilling
                End If

                Dim doNUmberOld As String = ""
                If Not IsNothing(spBill_old) Then
                    For Each itemBill As SparePartBillingDetail In spBill_old.SparePartBillingDetails
                        doNUmberOld = itemBill.SparePartDODetail.SparePartDO.DONumber
                    Next
                End If

                If doNUmber = doNUmberOld Then
                    If Not IsNothing(spBill_old) Then
                        Me.Delete(spBill_old)
                        For Each itemDetail_Old As SparePartBillingDetail In spBill_old.SparePartBillingDetails
                            objDetailFacade.Delete(itemDetail_Old)
                        Next
                    End If
                End If
            Catch ex As Exception

            End Try

            Dim returnValue As Integer = -1
            If Me.IsTaskFree() Then
                Try
                    'Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(spBill, m_userPrincipal.Identity.Name)
                    For Each itemDetail As SparePartBillingDetail In spBill.SparePartBillingDetails
                        itemDetail.SparePartBilling = spBill
                        m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                    Next

                    m_TransactionManager.PerformTransaction()


                    returnValue = 0

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

        Public Function InsertFromWebSevice(ByVal spBill As SparePartBilling) As Short
            Dim isChange As New IsChangeFacade
            Dim returnValue As Integer = -1
            If Me.IsTaskFree() Then
                'Try
                'Me.SetTaskLocking()
                Dim performTransaction As Boolean = True
                Dim ObjMapper As IMapper

                Dim spBill_DB As SparePartBilling = ValidateSparePartBilling(spBill)
                If IsNothing(spBill_DB) Then
                    m_TransactionManager.AddInsert(spBill, m_userPrincipal.Identity.Name)
                    For Each itemDetail As SparePartBillingDetail In spBill.SparePartBillingDetails
                        itemDetail.SparePartBilling = spBill
                        m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                    Next
                Else
                    For Each itemDetail_Old As SparePartBillingDetail In spBill_DB.SparePartBillingDetails
                        itemDetail_Old.RowStatus = CType(DBRowStatus.Deleted, Short)
                        m_TransactionManager.AddUpdate(itemDetail_Old, m_userPrincipal.Identity.Name)
                    Next

                    For Each itemDetail As SparePartBillingDetail In spBill.SparePartBillingDetails

                        'Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartBillingDetail), "SparePartBilling.ID", MatchType.Exact, spBill.ID))
                        criterias.opAnd(New Criteria(GetType(SparePartBillingDetail), "BillingItemNo", MatchType.Exact, itemDetail.BillingItemNo))

                        If Not IsNothing(itemDetail.SparePartDODetail) Then
                            criterias.opAnd(New Criteria(GetType(SparePartBillingDetail), "SparePartDODetail.ID", MatchType.Exact, itemDetail.SparePartDODetail.ID))

                            Dim arlDet As ArrayList = New SparePartBillingDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If Not IsNothing(arlDet) AndAlso arlDet.Count > 0 Then
                                Dim objDetail As New SparePartBillingDetail
                                objDetail = CType(arlDet(0), SparePartBillingDetail)

                                'Farid Additional 20180316
                                'If isChange.ISchangeSparePartBillingDetail(itemDetail,objDetail)  Then

                                objDetail.Quantity = itemDetail.Quantity
                                objDetail.ItemPrice = itemDetail.ItemPrice
                                objDetail.TotalPrice = itemDetail.TotalPrice
                                objDetail.Tax = itemDetail.Tax
                                objDetail.RowStatus = CType(DBRowStatus.Active, Short)

                                m_TransactionManager.AddUpdate(objDetail, m_userPrincipal.Identity.Name)
                            Else

                                itemDetail.SparePartBilling = spBill_DB
                                m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                            End If
                        End If
                    Next

                    'Farid Additinoal 20180316
                    If isChange.ISchangeSparePartBilling(spBill, spBill_DB) Then

                        spBill_DB.BillingDate = spBill.BillingDate
                        spBill_DB.TotalAmount = spBill.TotalAmount
                        spBill_DB.Tax = spBill.Tax
                        spBill_DB.BillingNumber = spBill.BillingNumber
                        spBill_DB.TermOfPayment = spBill.TermOfPayment
                        m_TransactionManager.AddUpdate(spBill_DB, m_userPrincipal.Identity.Name)
                    End If
                End If
                If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = 0
                    InsertDueDate(spBill.BillingNumber, spBill.Dealer.DealerCode)
                End If
                'Catch ex As Exception
                '    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                '    If rethrow Then
                '        Throw
                '    End If
                'Finally
                '    'Me.RemoveTaskLocking()
                'End Try
            End If
            Return returnValue
        End Function

        Public Function DeleteFromWebSevice(ByVal spBill As SparePartBilling) As Short
            Dim returnValue As Integer = -1
            If Me.IsTaskFree() Then
                Try
                    'Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim spBill_DB As SparePartBilling = ValidateSparePartBilling(spBill)
                    If Not IsNothing(spBill_DB) Then
                        For Each itemDetail As SparePartBillingDetail In spBill_DB.SparePartBillingDetails
                            itemDetail.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(itemDetail, m_userPrincipal.Identity.Name)
                        Next
                        spBill_DB.RowStatus = CType(DBRowStatus.Deleted, Short)
                        m_TransactionManager.AddUpdate(spBill_DB, m_userPrincipal.Identity.Name)

                        If performTransaction Then
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

        Public Function ValidateSparePartBilling(ByVal SparePartBilling As SparePartBilling) As SparePartBilling
            Try
                Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.SparePartBilling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteria.opAnd(New Criteria(GetType(KTB.DNET.Domain.SparePartBilling), "Dealer.ID", MatchType.Exact, SparePartBilling.Dealer.ID))
                criteria.opAnd(New Criteria(GetType(KTB.DNET.Domain.SparePartBilling), "BillingNumber", MatchType.Exact, SparePartBilling.BillingNumber))
                Dim arlSparePartBilling As ArrayList = m_SparePartBillingMapper.RetrieveByCriteria(criteria)
                If arlSparePartBilling.Count > 0 Then
                    Return CType(arlSparePartBilling(0), SparePartBilling)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        'Public Function ValidateSparePartBilling(ByVal doNumber As String) As SparePartBilling
        '    Try
        '        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '        criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartBillingDetail), "SparePartDODetail.SparePartDO.DONumber", MatchType.Exact, doNumber))
        '        Dim arlSparePartBillingDetail As ArrayList = m_SparePartBillingMapper.RetrieveByCriteria(criteria)
        '        If arlSparePartBillingDetail.Count > 0 Then
        '            Dim objSparePartBillingDetail As SparePartBillingDetail = CType(arlSparePartBillingDetail(0), SparePartBillingDetail)
        '            Return objSparePartBillingDetail.SparePartBilling
        '        Else
        '            Return Nothing
        '        End If
        '    Catch ex As Exception
        '        Return Nothing
        '    End Try

        'End Function

        'Private Function GetItemDoDetail(ByVal spMasterNumber As String, ByVal arlOrigSparePartBillingDetail As ArrayList) As SparePartBillingDetail
        '    For Each itemDetail As SparePartBillingDetail In arlOrigSparePartBillingDetail
        '        If itemDetail.SparePartDODetail.SparePartDO.DONumber = spMasterNumber Then
        '            Return itemDetail
        '        End If
        '    Next
        '    Return Nothing
        'End Function

        Private Sub InsertDueDate(ByVal BillingNumber As String, ByVal DealerCode As String)
            Dim spName As String = "sp_SetTOPSPOrderDueDate @BillingNumber='{0}' , @DealerCode='{1}'"

            Dim m As Boolean = m_SparePartBillingMapper.ExecuteSP(String.Format(spName, BillingNumber, DealerCode))
        End Sub

        Public Function GetWsLog() As List(Of String)
            Dim result As New List(Of String)

            Dim dataTable As DataTable = m_SparePartBillingMapper.RetrieveDataSet("Up_WSResend_SPBilling").Tables(0)
            For Each drWLog As DataRow In dataTable.Rows
                result.Add(drWLog(0).ToString())
            Next

            Return result
        End Function

        Public Function GetCountData(ByVal BillingNumber As String) As Integer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartBilling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartBilling), "BillingNumber", MatchType.InSet, BillingNumber))
            Dim arlBilling As ArrayList = m_SparePartBillingMapper.RetrieveByCriteria(criterias)

            Return arlBilling.Count
        End Function

        Public Function GetWsLogTOPSPBillingDeposit(ByVal BillingNumber As String) As String
            Dim result As String = String.Empty

            Dim Param As New List(Of SqlClient.SqlParameter)

            Param.Add(New SqlClient.SqlParameter("@BillingNumber ", BillingNumber))

            Dim dataTable As DataTable = m_SparePartBillingMapper.RetrieveDataSet("Up_WSResend_TOPSPBillingDeposit", New ArrayList(Param)).Tables(0)
            For Each drWLog As DataRow In dataTable.Rows
                result = drWLog(0).ToString()
            Next

            Return result
        End Function

        Public Function GetTOPSPBillingDepositData() As List(Of SparePartBilling)
            Dim result As New List(Of SparePartBilling)

            'Dim dataTable As DataTable = m_TOPSPDepositMapper.RetrieveDataSet("Up_WSResend_GetTOPSPBillingDepositData").Tables(0)
            'For Each drWLog As DataRow In dataTable.Rows
            '    result.Add(drWLog(0))
            'Next
            Dim dt As ArrayList = m_SparePartBillingMapper.RetrieveSP("Up_WSResend_GetTOPSPBillingDepositData")
            For Each drWLog As SparePartBilling In dt
                result.Add(drWLog)
            Next

            Return result
        End Function
#End Region

        Function RetrieveActiveList(ByVal Criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EquipUser), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ClaimReasonColl As ArrayList = m_SparePartBillingMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ClaimReasonColl
        End Function

    End Class

End Namespace


'If doNUmber <> doNUmberOld Then
'    m_TransactionManager.AddInsert(spBill, m_userPrincipal.Identity.Name)
'    For Each itemDetail As SparePartBillingDetail In spBill.SparePartBillingDetails
'        itemDetail.SparePartBilling = spBill
'        m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
'    Next

'Else

'    ''Delete old one
'    ''update rowstatus = -1 for old header
'    'spBill_old.RowStatus = CType(DBRowStatus.Deleted, Short)
'    'm_TransactionManager.AddUpdate(spBill_old, m_userPrincipal.Identity.Name)
'    ''update rowstatus = -1 for old detail
'    'For Each itemDetail_Old As SparePartBillingDetail In spBill_old.SparePartBillingDetails
'    '    itemDetail_Old.RowStatus = CType(DBRowStatus.Deleted, Short)
'    '    m_TransactionManager.AddUpdate(itemDetail_Old, m_userPrincipal.Identity.Name)
'    'Next

'    Me.DeleteFromWebSevice(spBill_old)

'    'insert new one
'    m_TransactionManager.AddInsert(spBill, m_userPrincipal.Identity.Name)
'    For Each itemDetail As SparePartBillingDetail In spBill.SparePartBillingDetails
'        itemDetail.SparePartBilling = spBill
'        m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
'    Next
'End If
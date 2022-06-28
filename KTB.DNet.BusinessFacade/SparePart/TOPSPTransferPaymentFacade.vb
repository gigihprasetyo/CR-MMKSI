
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
'// Copyright  2018
'// ---------------------
'// $History      : $
'// Generated on 9/10/2018 - 11:36:54 AM
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
Imports KTB.DNET.BusinessFacade.General
Imports Microsoft.VisualBasic.ApplicationServices
Imports System.Collections.Generic

#End Region

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class TOPSPTransferPaymentFacade
        Inherits AbstractFacade

#Region "Private Variables"



        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TOPSPTransferPaymentMapper As IMapper
        Private m_TOPSPTransferPaymentDetailMapper As IMapper
        Private m_AppConfigMapper As IMapper
        Private m_BankMapper As IMapper
        Private m_StatusChangeHistoryMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_TransactionManager = New TransactionManager
            Me.m_TOPSPTransferPaymentMapper = MapperFactory.GetInstance.GetMapper(GetType(TOPSPTransferPayment).ToString)
            Me.m_TOPSPTransferPaymentDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(TOPSPTransferPaymentDetail).ToString)
            Me.m_StatusChangeHistoryMapper = MapperFactory.GetInstance.GetMapper(GetType(StatusChangeHistory).ToString)
            Me.m_AppConfigMapper = MapperFactory.GetInstance.GetMapper(GetType(AppConfig).ToString)
            Me.m_BankMapper = MapperFactory.GetInstance.GetMapper(GetType(Bank).ToString)

            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.TOPSPTransferPayment))
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.TOPSPTransferPaymentDetail))
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.StatusChangeHistory))
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.Bank))
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.AppConfig))
            Me.m_userPrincipal = userPrincipal

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TOPSPTransferPayment
            Return CType(m_TOPSPTransferPaymentMapper.Retrieve(ID), TOPSPTransferPayment)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TOPSPTransferPaymentMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TOPSPTransferPaymentMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TOPSPTransferPaymentMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TOPSPTransferPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TOPSPTransferPaymentMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TOPSPTransferPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TOPSPTransferPaymentMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite,
                                           ByVal pageNumber As Integer,
                                           ByVal pageSize As Integer,
                                           ByRef totalRow As Integer,
                                           ByVal sortColumn As String,
                                           ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TOPSPTransferPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim _spBilling As New ArrayList
            _spBilling = m_TOPSPTransferPaymentMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)

            Return _spBilling
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TOPSPTransferPayment As ArrayList = m_TOPSPTransferPaymentMapper.RetrieveByCriteria(criterias)
            Return _TOPSPTransferPayment
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TOPSPTransferPaymentColl As ArrayList = m_TOPSPTransferPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TOPSPTransferPaymentColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TOPSPTransferPaymentColl As ArrayList = m_TOPSPTransferPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TOPSPTransferPaymentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TOPSPTransferPaymentColl As ArrayList = m_TOPSPTransferPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TOPSPTransferPayment), columnName, matchOperator, columnValue))
            Return TOPSPTransferPaymentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TOPSPTransferPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPayment), columnName, matchOperator, columnValue))

            Return m_TOPSPTransferPaymentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCode(ByVal RegNumber As String, ByVal DealerCode As String) As TOPSPTransferPayment
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TOPSPTransferPayment), "RegNumber", MatchType.Exact, RegNumber))
            criterias.opAnd(New Criteria(GetType(TOPSPTransferPayment), "Dealer.DealerCode", MatchType.Exact, DealerCode))
            Dim TOPSPTransferPaymentColl As ArrayList = m_TOPSPTransferPaymentMapper.RetrieveByCriteria(criterias)
            If (TOPSPTransferPaymentColl.Count > 0) Then
                Return CType(TOPSPTransferPaymentColl(0), TOPSPTransferPayment)
            End If
            Return New TOPSPTransferPayment
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Retrieve_Bank() As ArrayList
            Dim arr As ArrayList
            Dim objAppConfig As AppConfig
            Dim cri As New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cri.opAnd(New Criteria(GetType(AppConfig), "Name", MatchType.Exact, "TOPSPTransferPaymentListBank"))
            cri.opAnd(New Criteria(GetType(AppConfig), "AppID", MatchType.Exact, "KTB.DNet.UI.40"))

            objAppConfig = CType(m_AppConfigMapper.RetrieveByCriteria(cri).Item(0), AppConfig)

            cri = New CriteriaComposite(New Criteria(GetType(Bank), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cri.opAnd(New Criteria(GetType(Bank), "ID", MatchType.InSet, "(" + objAppConfig.Value.ToString + ")"))

            arr = m_BankMapper.RetrieveByCriteria(cri)

            If arr.Count = 0 Then
                Dim objbank As New Bank
                objbank.ID = 0
                objbank.BankName = ""
                arr.Add(objbank)
            End If

            Return arr
        End Function

        Public Function InsertTOPSPTransferPayment_int(ByVal objDomain As TOPSPTransferPayment, ByVal objDetails As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    If objDetails.Count > 0 Then
                        For Each items As TOPSPTransferPaymentDetail In objDetails
                            m_TransactionManager.AddUpdate(items, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
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

        Public Function InsertTOPSPTransferPayment(ByVal objDomain As TOPSPTransferPayment, ByVal objDetails As ArrayList) As TOPSPTransferPayment
            Dim returnValue As TOPSPTransferPayment
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    If objDetails.Count > 0 Then
                        For Each items As TOPSPTransferPaymentDetail In objDetails
                            items.TOPSPTransferPayment = objDomain
                            m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain
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


        Public Function UpdateTOPSPTransferPayment(ByVal objDomain As TOPSPTransferPayment, ByVal objDetails As ArrayList) As Integer
            Dim returnValue As Integer
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    'm_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    If objDetails.Count > 0 Then
                        For Each items As TOPSPTransferPaymentDetail In objDetails
                            items.TOPSPTransferPayment = objDomain
                            If items.ID = 0 Then
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddUpdate(items, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
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

        Public Function Insert(ByVal objDomain As TOPSPTransferPayment) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TOPSPTransferPaymentMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TOPSPTransferPayment) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TOPSPTransferPaymentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function UpdateBatalPembayaran(ByVal objDomain As TOPSPTransferPayment) As Integer
            Dim nResult As Integer = -1
            Try

                objDomain.CanceledBy = m_userPrincipal.Identity.Name
                objDomain.CanceledTime = Now.Date

                nResult = m_TOPSPTransferPaymentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function UpdateBatalKonfirmasi(ByVal objDomain As TOPSPTransferPayment) As Integer
            Dim nResult As Integer = -1
            Try

                nResult = m_TOPSPTransferPaymentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                If nResult > 0 Then
                    Dim objTOPSPIold As TOPSPTransferPayment
                    objTOPSPIold = m_TOPSPTransferPaymentMapper.Retrieve(objDomain.TOPSPTransferPaymentIDReff)
                    objTOPSPIold.Status = CType(EnumStatusTOPSPTransferPayment.TOPSPStatus.Validasi, Integer)
                    nResult = m_TOPSPTransferPaymentMapper.Update(objTOPSPIold, m_userPrincipal.Identity.Name)
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

        Public Function UpdateValidasi(ByVal objDomain As TOPSPTransferPayment) As Integer
            Dim nResult As Integer = -1
            Try

                objDomain.ValidatedBy = m_userPrincipal.Identity.Name
                objDomain.ValidatedTime = Now.Date

                nResult = m_TOPSPTransferPaymentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function


        Public Function UpdateKonfirmasi(ByVal objDomain As TOPSPTransferPayment) As Integer
            Dim nResult As Integer = -1
            Try

                objDomain.ConfirmedBy = m_userPrincipal.Identity.Name
                objDomain.ConfirmedTime = Now.Date

                nResult = m_TOPSPTransferPaymentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TOPSPTransferPayment)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TOPSPTransferPaymentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As TOPSPTransferPayment)
            Try
                m_TOPSPTransferPaymentMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

        Public Function ExecuteSQL(ByVal sql As String) As ArrayList
            Return m_TOPSPTransferPaymentMapper.RetrieveSP(sql)
        End Function

        Private Sub m_TransactionManager_Insert(sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.TOPSPTransferPayment) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.TOPSPTransferPayment).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.TOPSPTransferPayment).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is TOPSPTransferPaymentDetail) Then
                CType(InsertArg.DomainObject, TOPSPTransferPaymentDetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function up_RetrieveTOPSPTransferPaymentMultiPercepatan(ByVal id As Integer) As List(Of TOPSPTransferPayment)
            Dim arrReturn As New List(Of TOPSPTransferPayment)
            Dim arr As New ArrayList
            Dim strQuery As String

            Try
                strQuery = "Exec up_RetrieveTOPSPTransferPaymentMultiPercepatan " + id.ToString

                arr = m_TOPSPTransferPaymentMapper.RetrieveSP(strQuery)

                For Each row As TOPSPTransferPayment In arr
                    arrReturn.Add(row)
                Next

                If arr.Count > 0 Then
                    Return arrReturn
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function GetTOPSPSparePartBilling(ByVal id As Integer) As Integer
            Dim ds As New DataSet
            Dim strQuery As String = "Exec up_GetTOPSPSparePartBillingID " + id.ToString


            ds = m_TOPSPTransferPaymentMapper.RetrieveDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows.Count
            End If

        End Function

        Public Function InsertReturnObject(ByVal objDomain As TOPSPTransferPayment, PTdate As Date) As TOPSPTransferPayment
            Dim iReturn As New TOPSPTransferPayment
            Dim strQuery As String = "Exec up_InsertTOPSPTransferPaymentPercepatan "
            Dim arr As ArrayList
            Try
                strQuery = strQuery + objDomain.ID.ToString + "," + objDomain.Dealer.ID.ToString + ",'" + m_userPrincipal.Identity.Name + "','" + PTdate.ToString("yyyyMMdd") + "'"
                arr = m_TOPSPTransferPaymentMapper.RetrieveSP(strQuery)
                If arr.Count > 0 OrElse arr IsNot Nothing Then
                    iReturn = CType(arr.Item(0), TOPSPTransferPayment)
                Else
                    iReturn = Nothing
                End If
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = Nothing
            End Try
            Return iReturn

        End Function


        ''' <summary>
        ''' Retrieve By RegNumber
        ''' </summary>
        ''' <param name="RegNumber"></param>
        ''' <returns>TOPSPTransferPayment</returns>
        ''' <remarks>Created By AA</remarks>
        Public Function Retrieve(ByVal RegNumber As String) As TOPSPTransferPayment
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TOPSPTransferPayment), "RegNumber", MatchType.Exact, RegNumber))
            Dim arlBilling As ArrayList = m_TOPSPTransferPaymentMapper.RetrieveByCriteria(criterias)
            If arlBilling.Count > 0 Then
                Return CType(arlBilling(0), TOPSPTransferPayment)
            End If
            Return Nothing
        End Function

        Public Function UpdateTransferActual(ByVal tp As TOPSPTransferPayment, ByVal RefTransferbank As String) As Boolean

            Try
                Dim arr As New ArrayList
                Dim strSP As String = "up_UpdateTOPSPTransferPayment_TransferActual"
                Dim arrParam As New ArrayList

                Dim par1 As New SqlClient.SqlParameter

                par1.DbType = DbType.Int32
                par1.Value = tp.ID
                par1.ParameterName = "@TOPSPTransferPaymentID"
                arrParam.Add(par1)


                Dim par2 = New SqlClient.SqlParameter

                par2.DbType = DbType.AnsiString
                par2.Value = RefTransferbank
                par2.ParameterName = "@RefTransferbank"
                arrParam.Add(par2)


                Dim par3 = New SqlClient.SqlParameter

                par3.DbType = DbType.Decimal
                par3.Value = tp.TransferAmount
                par3.ParameterName = "@Amount"
                arrParam.Add(par3)


                Dim par4 = New SqlClient.SqlParameter

                par4.DbType = DbType.DateTime
                par4.Value = tp.TransferDate
                par4.ParameterName = "@PostingDate"
                arrParam.Add(par4)


                Dim par5 = New SqlClient.SqlParameter

                par5.DbType = DbType.AnsiString
                par5.Value = m_userPrincipal.Identity.Name
                par5.ParameterName = "@LastUpdatedBy"
                arrParam.Add(par5)

                Dim b As Boolean = m_TOPSPTransferPaymentMapper.ExecuteSP(strSP, arrParam)

                Return b
            Catch ex As Exception
                Return False
            End Try


            Return True
        End Function

        'Public Function UpdateTOPTransferPayment(ByVal tp As TOPSPTransferPayment, ByVal TransferAmount As Integer, ByVal ActualDate As Date, ByVal RefTransferbank As String, ByVal DtKliring As Date, ByVal TotalKliring As Integer, ByVal NoTR As String) As Boolean
        Public Function UpdateTOPTransferPayment(ByVal tp As TOPSPTransferPayment, ByVal TOPSP As TOPSPTransferPayment) As Boolean

            Try
                Dim arr As New ArrayList
                Dim strSP As String = "up_UpdateTOPSPTP_TransferActual_AccountingNo"
                Dim arrParam As New ArrayList

                Dim par1 As New SqlClient.SqlParameter

                par1.DbType = DbType.Int32
                par1.Value = tp.ID
                par1.ParameterName = "@TOPSPTransferPaymentID"
                arrParam.Add(par1)


                Dim par2 = New SqlClient.SqlParameter

                par2.DbType = DbType.AnsiString
                par2.Value = TOPSP.ReffBank
                par2.ParameterName = "@RefTransferbank"
                arrParam.Add(par2)


                Dim par3 = New SqlClient.SqlParameter

                par3.DbType = DbType.Decimal
                par3.Value = TOPSP.TransferAmount
                par3.ParameterName = "@Amount"
                arrParam.Add(par3)


                Dim par4 = New SqlClient.SqlParameter

                par4.DbType = DbType.DateTime
                par4.Value = TOPSP.ActualDate
                par4.ParameterName = "@PostingDate"
                arrParam.Add(par4)


                Dim par5 = New SqlClient.SqlParameter

                par5.DbType = DbType.AnsiString
                par5.Value = m_userPrincipal.Identity.Name
                par5.ParameterName = "@LastUpdatedBy"
                arrParam.Add(par5)

                Dim par6 = New SqlClient.SqlParameter

                par6.DbType = DbType.Decimal
                par6.Value = TOPSP.TotalKliring
                par6.ParameterName = "@KliringAmount"
                arrParam.Add(par6)


                Dim par7 = New SqlClient.SqlParameter

                par7.DbType = DbType.DateTime
                par7.Value = TOPSP.KliringDate
                par7.ParameterName = "@KliringDate"
                arrParam.Add(par7)


                Dim par8 = New SqlClient.SqlParameter

                par8.DbType = DbType.AnsiString
                par8.Value = TOPSP.DocClearing
                par8.ParameterName = "@TRNo"
                arrParam.Add(par8)

                Dim iResult As Integer
                Dim b As Boolean = True
                iResult = m_TOPSPTransferPaymentMapper.ExecuteSP(strSP, arrParam)
                If iResult <> 0 Then
                    b = False
                End If
                Return b
            Catch ex As Exception
                Return False
            End Try


            Return True
        End Function

        Public Function RetrieveQuery(sqlStr) As DataSet
            Dim ds As DataSet = New DataSet
            Try
                ds = m_TOPSPTransferPaymentMapper.RetrieveDataSet(sqlStr)
            Catch ex As Exception
            End Try
            Return ds
        End Function

#End Region


    End Class

End Namespace


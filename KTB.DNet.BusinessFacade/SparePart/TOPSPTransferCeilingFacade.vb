
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
'// Generated on 9/14/2018 - 11:15:02 AM
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

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class TOPSPTransferCeilingFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TOPSPTransferCeilingMapper As IMapper
        Private m_TOPSPTransferCeilingDetailMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_TOPSPTransferCeilingMapper = MapperFactory.GetInstance.GetMapper(GetType(TOPSPTransferCeiling).ToString)
            Me.m_TOPSPTransferCeilingDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(TOPSPTransferCeilingDetail).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TOPSPTransferCeiling
            Return CType(m_TOPSPTransferCeilingMapper.Retrieve(ID), TOPSPTransferCeiling)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TOPSPTransferCeilingMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TOPSPTransferCeilingMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TOPSPTransferCeilingMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TOPSPTransferCeiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TOPSPTransferCeilingMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TOPSPTransferCeiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TOPSPTransferCeilingMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TOPSPTransferCeiling As ArrayList = m_TOPSPTransferCeilingMapper.RetrieveByCriteria(criterias)
            Return _TOPSPTransferCeiling
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TOPSPTransferCeilingColl As ArrayList = m_TOPSPTransferCeilingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TOPSPTransferCeilingColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TOPSPTransferCeilingColl As ArrayList = m_TOPSPTransferCeilingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TOPSPTransferCeilingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TOPSPTransferCeilingColl As ArrayList = m_TOPSPTransferCeilingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TOPSPTransferCeiling), columnName, matchOperator, columnValue))
            Return TOPSPTransferCeilingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TOPSPTransferCeiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferCeiling), columnName, matchOperator, columnValue))

            Return m_TOPSPTransferCeilingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As TOPSPTransferCeiling) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TOPSPTransferCeilingMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TOPSPTransferCeiling) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TOPSPTransferCeilingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TOPSPTransferCeiling)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TOPSPTransferCeilingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As TOPSPTransferCeiling)
            Try
                m_TOPSPTransferCeilingMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"


        Public Function RetrieveCeilingStatus(ProductCategoryID As Integer, CreditAccount As String _
                                            , PaymentType As Short, StartDate As DateTime, EndDate As DateTime _
                                            , Optional IsReport As Boolean = False _
                                            , Optional IsReportDetail As Boolean = False) As DataSet
            Dim ds As New DataSet
            Dim strSPName As String = "sp_TOPSPCeiling"
            'Get param
            'Dim arrParam As New ArrayList
            Dim Param As New List(Of SqlClient.SqlParameter)
             


            Param.Add(New SqlClient.SqlParameter("@ProductCategoryID", ProductCategoryID))
            Param.Add(New SqlClient.SqlParameter("@CreditAccount", CreditAccount))
            Param.Add(New SqlClient.SqlParameter("@PaymentType", PaymentType))
            Param.Add(New SqlClient.SqlParameter("@StartDate", StartDate))
            Param.Add(New SqlClient.SqlParameter("@EndDate", EndDate))
            Param.Add(New SqlClient.SqlParameter("@IsReport", IIf(IsReport, 1, 0)))
            Param.Add(New SqlClient.SqlParameter("@IsShowReportDetail", IIf(IsReportDetail, 1, 0)))



            ds = m_TOPSPTransferCeilingMapper.RetrieveDataSet(strSPName, New ArrayList(Param))

            Return ds

        End Function


        Public Function RetrieveCeilingDetail(ByVal TransferCeilingID As Integer) As DataSet
            Dim Sql As String

            Sql = "exec sp_TOPSPCeilingDetail " & TransferCeilingID.ToString()
            Return Me.m_TOPSPTransferCeilingMapper.RetrieveDataSet(Sql)

        End Function



        Public Function RetrieveGabunganCeilingStatus(ProductCategoryID As Integer, CreditAccount As String _
                                        , PaymentType As Short, StartDate As DateTime, EndDate As DateTime _
                                        , Optional IsReport As Boolean = False _
                                        , Optional IsReportDetail As Boolean = False) As DataSet
            Dim ds As New DataSet
            Dim strSPName As String = "sp_TOPCombineCeiling"
            'Get param
            'Dim arrParam As New ArrayList
            Dim Param As New List(Of SqlClient.SqlParameter)
             

            Param.Add(New SqlClient.SqlParameter("@ProductCategoryID", ProductCategoryID))
            Param.Add(New SqlClient.SqlParameter("@CreditAccount", CreditAccount))
            Param.Add(New SqlClient.SqlParameter("@PaymentType", PaymentType))
            Param.Add(New SqlClient.SqlParameter("@StartDate", StartDate))
            Param.Add(New SqlClient.SqlParameter("@EndDate", EndDate))
            Param.Add(New SqlClient.SqlParameter("@IsReport", IIf(IsReport, 1, 0)))
            Param.Add(New SqlClient.SqlParameter("@IsShowReportDetail", IIf(IsReportDetail, 1, 0)))



            ds = m_TOPSPTransferCeilingMapper.RetrieveDataSet(strSPName, New ArrayList(Param))

            Return ds

        End Function

        Public Function Insert(ByVal aTCs As ArrayList) As Integer
            Try
                For Each oTC As TOPSPTransferCeiling In aTCs
                    m_TOPSPTransferCeilingMapper.Insert(oTC, "ws")
                Next
                Return 0
            Catch ex As Exception
                Return -1
            End Try

            Return -1
             
        End Function


        Public Function UpdateDetail(ByVal _TC As TOPSPTransferCeiling) As Integer
            Dim iRes As Integer = -1
            Dim TCDs As ArrayList = _TC.TOPSPTransferCeilingDetails
            Dim TCDb As TransferCeiling
            Dim ID As Integer

            Try
                ID = _TC.ID
                iRes = Me.Update(_TC)
                If (iRes > 0) Then
                    _TC = Me.Retrieve(ID)
                    For Each oTCD As TOPSPTransferCeilingDetail In TCDs
                        Dim cTCD As CriteriaComposite
                        Dim oTCDDb As TOPSPTransferCeilingDetail
                        Dim TCDsDb As ArrayList

                        cTCD = New CriteriaComposite(New Criteria(GetType(TOPSPTransferCeilingDetail), "RowStatus", MatchType.No, CType(DBRowStatus.Canceled, Short)))
                        If Not IsNothing(oTCD.SparePartBilling) AndAlso oTCD.SparePartBilling.ID > 0 Then
                            cTCD.opAnd(New Criteria(GetType(TOPSPTransferCeilingDetail), "SparePartBilling.ID", MatchType.Exact, oTCD.SparePartBilling.ID))
                        ElseIf Not IsNothing(oTCD.TOPSPTransferPayment) AndAlso oTCD.TOPSPTransferPayment.ID > 0 Then
                            cTCD.opAnd(New Criteria(GetType(TOPSPTransferCeilingDetail), "TOPSPTransferPayment.ID", MatchType.Exact, oTCD.TOPSPTransferPayment.ID))
                        End If

                        TCDsDb = m_TOPSPTransferCeilingDetailMapper.RetrieveByCriteria(cTCD)
                        Dim _Amount As Decimal = oTCD.Amount
                        Dim StrSQLDetail As String = " exec TOPSPTransferCeilingDetail_BalanceUpdate @ProductCategoryID={0}, @CreditAccount='{1}', @PaymentType={2}, @EffectiveDateHeader='{3}',@Amount={4}"

                        StrSQLDetail = String.Format(StrSQLDetail, _TC.ProductCategory.ID, _TC.CreditAccount, _TC.PaymentType, _TC.EffectiveDate.ToString("yyyy/MM/dd"), FormatNumber(_Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True).Replace(",", "").Replace(".", ""))

                        If TCDsDb.Count > 0 Then
                            oTCDDb = TCDsDb(0)

                            oTCDDb.TOPSPTransferCeiling = _TC
                            oTCDDb.Amount = oTCD.Amount
                            oTCDDb.RowStatus = CInt(DBRowStatus.Active)
                            m_TOPSPTransferCeilingDetailMapper.Update(oTCDDb, "ws")

                        Else
                            oTCD.TOPSPTransferCeiling = _TC
                            m_TOPSPTransferCeilingDetailMapper.Insert(oTCD, "ws")

                        End If
                        If oTCD.IsIncome = 1 Then
                            StrSQLDetail = StrSQLDetail & ", @IsIncome={0},@TransDateDetail='{1}', @TOPSPTransferPaymentID={2} "
                            StrSQLDetail = String.Format(StrSQLDetail, oTCD.IsIncome, _TC.EffectiveDate.ToString("yyyy/MM/dd"), oTCD.TOPSPTransferPayment.ID)
                        Else
                            StrSQLDetail = StrSQLDetail & ", @IsIncome={0},@TransDateDetail='{1}', @SparePartBillingID={2} "
                            StrSQLDetail = String.Format(StrSQLDetail, oTCD.IsIncome, _TC.EffectiveDate.ToString("yyyy/MM/dd"), oTCD.SparePartBilling.ID)
                        End If


                        m_TOPSPTransferCeilingDetailMapper.ExecuteSP(StrSQLDetail)

                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return iRes
        End Function



        Public Function ExecuteSP(ByVal SQL As String) As Boolean
            'If SQL.IndexOf("exec ") < 0 Then
            '    SQL = SQL.Replace("'", "''")
            '    SQL = "exec SPHelper '" & SQL & "'"
            'End If
            Return Me.m_TOPSPTransferCeilingMapper.ExecuteSP(SQL)
        End Function

#End Region

    End Class

End Namespace


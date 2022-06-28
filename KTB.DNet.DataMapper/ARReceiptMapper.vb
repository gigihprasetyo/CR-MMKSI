
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : MitraisTeam
'// PURPOSE       : ARReceipt Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 23/03/2018 - 16:16:48
'//
'// ===========================================================================	
#End Region


#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Data
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.DataMapper

    Public Class ARReceiptMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertARReceipt"
        Private m_UpdateStatement As String = "up_UpdateARReceipt"
        Private m_RetrieveStatement As String = "up_RetrieveARReceipt"
        Private m_RetrieveListStatement As String = "up_RetrieveARReceiptList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteARReceipt"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim aRReceipt As ARReceipt = Nothing
            While dr.Read

                aRReceipt = Me.CreateObject(dr)

            End While

            Return aRReceipt

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim aRReceiptList As ArrayList = New ArrayList

            While dr.Read
                Dim aRReceipt As ARReceipt = Me.CreateObject(dr)
                aRReceiptList.Add(aRReceipt)
            End While

            Return aRReceiptList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim aRReceipt As ARReceipt = CType(obj, ARReceipt)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, aRReceipt.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim aRReceipt As ARReceipt = CType(obj, ARReceipt)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, aRReceipt.Owner)
            DbCommandWrapper.AddInParameter("@GeneratedToken", DbType.AnsiString, aRReceipt.GeneratedToken)
            DbCommandWrapper.AddInParameter("@ARInvoiceReferenceNo", DbType.AnsiString, aRReceipt.ARInvoiceReferenceNo)
            DbCommandWrapper.AddInParameter("@ARReceiptNo", DbType.AnsiString, aRReceipt.ARReceiptNo)
            DbCommandWrapper.AddInParameter("@ARReceiptReferenceNo", DbType.AnsiString, aRReceipt.ARReceiptReferenceNo)
            DbCommandWrapper.AddInParameter("@Type", DbType.Int16, aRReceipt.Type)
            DbCommandWrapper.AddInParameter("@BookingFee", DbType.Boolean, aRReceipt.BookingFee)
            DbCommandWrapper.AddInParameter("@BU", DbType.AnsiString, aRReceipt.BU)
            DbCommandWrapper.AddInParameter("@Cancelled", DbType.Boolean, aRReceipt.Cancelled)
            DbCommandWrapper.AddInParameter("@CashAndBank", DbType.AnsiString, aRReceipt.CashAndBank)
            DbCommandWrapper.AddInParameter("@Customer", DbType.AnsiString, aRReceipt.Customer)
            DbCommandWrapper.AddInParameter("@CustomerNo", DbType.AnsiString, aRReceipt.CustomerNo)
            DbCommandWrapper.AddInParameter("@EndOrderDate", DbType.DateTime, aRReceipt.EndOrderDate)
            DbCommandWrapper.AddInParameter("@MethodOfPayment", DbType.AnsiString, aRReceipt.MethodOfPayment)
            DbCommandWrapper.AddInParameter("@AvailableBalance", DbType.Currency, aRReceipt.AvailableBalance)
            DbCommandWrapper.AddInParameter("@StartOrderDate", DbType.DateTime, aRReceipt.StartOrderDate)
            DbCommandWrapper.AddInParameter("@State", DbType.Int16, aRReceipt.State)
            DbCommandWrapper.AddInParameter("@AppliedToDocument", DbType.Currency, aRReceipt.AppliedToDocument)
            DbCommandWrapper.AddInParameter("@TotalAmountBase", DbType.Currency, aRReceipt.TotalAmountBase)
            DbCommandWrapper.AddInParameter("@TotalChangeAmount", DbType.Currency, aRReceipt.TotalChangeAmount)
            DbCommandWrapper.AddInParameter("@TotalOutstandingBalanceBase", DbType.Currency, aRReceipt.TotalOutstandingBalanceBase)
            DbCommandWrapper.AddInParameter("@TotalReceiptAmount", DbType.Currency, aRReceipt.TotalReceiptAmount)
            DbCommandWrapper.AddInParameter("@TotalRemainingBalanceBase", DbType.Currency, aRReceipt.TotalRemainingBalanceBase)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, aRReceipt.TransactionDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, aRReceipt.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, aRReceipt.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DbCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim aRReceipt As ARReceipt = CType(obj, ARReceipt)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, aRReceipt.ID)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, aRReceipt.Owner)
            DbCommandWrapper.AddInParameter("@GeneratedToken", DbType.AnsiString, aRReceipt.GeneratedToken)
            DbCommandWrapper.AddInParameter("@ARInvoiceReferenceNo", DbType.AnsiString, aRReceipt.ARInvoiceReferenceNo)
            DbCommandWrapper.AddInParameter("@ARReceiptNo", DbType.AnsiString, aRReceipt.ARReceiptNo)
            DbCommandWrapper.AddInParameter("@ARReceiptReferenceNo", DbType.AnsiString, aRReceipt.ARReceiptReferenceNo)
            DbCommandWrapper.AddInParameter("@Type", DbType.Int16, aRReceipt.Type)
            DbCommandWrapper.AddInParameter("@BookingFee", DbType.Boolean, aRReceipt.BookingFee)
            DbCommandWrapper.AddInParameter("@BU", DbType.AnsiString, aRReceipt.BU)
            DbCommandWrapper.AddInParameter("@Cancelled", DbType.Boolean, aRReceipt.Cancelled)
            DbCommandWrapper.AddInParameter("@CashAndBank", DbType.AnsiString, aRReceipt.CashAndBank)
            DbCommandWrapper.AddInParameter("@Customer", DbType.AnsiString, aRReceipt.Customer)
            DbCommandWrapper.AddInParameter("@CustomerNo", DbType.AnsiString, aRReceipt.CustomerNo)
            DbCommandWrapper.AddInParameter("@EndOrderDate", DbType.DateTime, aRReceipt.EndOrderDate)
            DbCommandWrapper.AddInParameter("@MethodOfPayment", DbType.AnsiString, aRReceipt.MethodOfPayment)
            DbCommandWrapper.AddInParameter("@AvailableBalance", DbType.Currency, aRReceipt.AvailableBalance)
            DbCommandWrapper.AddInParameter("@StartOrderDate", DbType.DateTime, aRReceipt.StartOrderDate)
            DbCommandWrapper.AddInParameter("@State", DbType.Int16, aRReceipt.State)
            DbCommandWrapper.AddInParameter("@AppliedToDocument", DbType.Currency, aRReceipt.AppliedToDocument)
            DbCommandWrapper.AddInParameter("@TotalAmountBase", DbType.Currency, aRReceipt.TotalAmountBase)
            DbCommandWrapper.AddInParameter("@TotalChangeAmount", DbType.Currency, aRReceipt.TotalChangeAmount)
            DbCommandWrapper.AddInParameter("@TotalOutstandingBalanceBase", DbType.Currency, aRReceipt.TotalOutstandingBalanceBase)
            DbCommandWrapper.AddInParameter("@TotalReceiptAmount", DbType.Currency, aRReceipt.TotalReceiptAmount)
            DbCommandWrapper.AddInParameter("@TotalRemainingBalanceBase", DbType.Currency, aRReceipt.TotalRemainingBalanceBase)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, aRReceipt.TransactionDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, aRReceipt.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, aRReceipt.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ARReceipt

            Dim aRReceipt As ARReceipt = New ARReceipt

            aRReceipt.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then aRReceipt.Owner = dr("Owner").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GeneratedToken")) Then aRReceipt.GeneratedToken = dr("GeneratedToken").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ARInvoiceReferenceNo")) Then aRReceipt.ARInvoiceReferenceNo = dr("ARInvoiceReferenceNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ARReceiptNo")) Then aRReceipt.ARReceiptNo = dr("ARReceiptNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ARReceiptReferenceNo")) Then aRReceipt.ARReceiptReferenceNo = dr("ARReceiptReferenceNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Type")) Then aRReceipt.Type = CType(dr("Type"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("BookingFee")) Then aRReceipt.BookingFee = CType(dr("BookingFee"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("BU")) Then aRReceipt.BU = dr("BU").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Cancelled")) Then aRReceipt.Cancelled = CType(dr("Cancelled"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("CashAndBank")) Then aRReceipt.CashAndBank = dr("CashAndBank").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Customer")) Then aRReceipt.Customer = dr("Customer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerNo")) Then aRReceipt.CustomerNo = dr("CustomerNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EndOrderDate")) Then aRReceipt.EndOrderDate = CType(dr("EndOrderDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MethodOfPayment")) Then aRReceipt.MethodOfPayment = dr("MethodOfPayment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AvailableBalance")) Then aRReceipt.AvailableBalance = CType(dr("AvailableBalance"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("StartOrderDate")) Then aRReceipt.StartOrderDate = CType(dr("StartOrderDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("State")) Then aRReceipt.State = CType(dr("State"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("AppliedToDocument")) Then aRReceipt.AppliedToDocument = CType(dr("AppliedToDocument"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmountBase")) Then aRReceipt.TotalAmountBase = CType(dr("TotalAmountBase"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalChangeAmount")) Then aRReceipt.TotalChangeAmount = CType(dr("TotalChangeAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalOutstandingBalanceBase")) Then aRReceipt.TotalOutstandingBalanceBase = CType(dr("TotalOutstandingBalanceBase"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalReceiptAmount")) Then aRReceipt.TotalReceiptAmount = CType(dr("TotalReceiptAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalRemainingBalanceBase")) Then aRReceipt.TotalRemainingBalanceBase = CType(dr("TotalRemainingBalanceBase"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionDate")) Then aRReceipt.TransactionDate = CType(dr("TransactionDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then aRReceipt.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then aRReceipt.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then aRReceipt.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then aRReceipt.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then aRReceipt.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return aRReceipt

        End Function

        Private Sub SetTableName()

            If Not (GetType(ARReceipt) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ARReceipt), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ARReceipt).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


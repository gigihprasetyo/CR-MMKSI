
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : MitraisTeam
'// PURPOSE       : SparePartSalesOrder Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 22/03/2018 - 16:42:16
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

    Public Class SparePartSalesOrderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartSalesOrder"
        Private m_UpdateStatement As String = "up_UpdateSparePartSalesOrder"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartSalesOrder"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartSalesOrderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartSalesOrder"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartSalesOrder As SparePartSalesOrder = Nothing
            While dr.Read

                sparePartSalesOrder = Me.CreateObject(dr)

            End While

            Return sparePartSalesOrder

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartSalesOrderList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartSalesOrder As SparePartSalesOrder = Me.CreateObject(dr)
                sparePartSalesOrderList.Add(sparePartSalesOrder)
            End While

            Return sparePartSalesOrderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartSalesOrder As SparePartSalesOrder = CType(obj, SparePartSalesOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartSalesOrder.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartSalesOrder As SparePartSalesOrder = CType(obj, SparePartSalesOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SalesChannel", DbType.Int16, sparePartSalesOrder.SalesChannel)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, sparePartSalesOrder.Owner)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sparePartSalesOrder.Status)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, sparePartSalesOrder.DealerCode)
            DbCommandWrapper.AddInParameter("@Customer", DbType.AnsiString, sparePartSalesOrder.Customer)
            DbCommandWrapper.AddInParameter("@CustomerNo", DbType.AnsiString, sparePartSalesOrder.CustomerNo)
            DbCommandWrapper.AddInParameter("@DownPaymentAmount", DbType.Currency, sparePartSalesOrder.DownPaymentAmount)
            DbCommandWrapper.AddInParameter("@DownPaymentAmountReceived", DbType.Currency, sparePartSalesOrder.DownPaymentAmountReceived)
            DbCommandWrapper.AddInParameter("@DownPaymentIsPaid", DbType.Boolean, sparePartSalesOrder.DownPaymentIsPaid)
            DbCommandWrapper.AddInParameter("@ExternalReferenceNo", DbType.AnsiString, sparePartSalesOrder.ExternalReferenceNo)
            DbCommandWrapper.AddInParameter("@GrandTotal", DbType.Currency, sparePartSalesOrder.GrandTotal)
            DbCommandWrapper.AddInParameter("@Handling", DbType.Int16, sparePartSalesOrder.Handling)
            DbCommandWrapper.AddInParameter("@MethodOfPayment", DbType.AnsiString, sparePartSalesOrder.MethodOfPayment)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, sparePartSalesOrder.OrderType)
            DbCommandWrapper.AddInParameter("@SalesOrderNo", DbType.AnsiString, sparePartSalesOrder.SalesOrderNo)
            DbCommandWrapper.AddInParameter("@SalesPerson", DbType.AnsiString, sparePartSalesOrder.SalesPerson)
            DbCommandWrapper.AddInParameter("@ShipmentType", DbType.AnsiString, sparePartSalesOrder.ShipmentType)
            DbCommandWrapper.AddInParameter("@State", DbType.AnsiString, sparePartSalesOrder.State)
            DbCommandWrapper.AddInParameter("@TermOfPayment", DbType.AnsiString, sparePartSalesOrder.TermOfPayment)
            DbCommandWrapper.AddInParameter("@TotalAmountBeforeDiscount", DbType.Currency, sparePartSalesOrder.TotalAmountBeforeDiscount)
            DbCommandWrapper.AddInParameter("@TotalBaseAmount", DbType.Currency, sparePartSalesOrder.TotalBaseAmount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTaxAmount", DbType.Currency, sparePartSalesOrder.TotalConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TotalDiscountAmount", DbType.Currency, sparePartSalesOrder.TotalDiscountAmount)
            DbCommandWrapper.AddInParameter("@TotalReceipt", DbType.Currency, sparePartSalesOrder.TotalReceipt)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, sparePartSalesOrder.TransactionDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartSalesOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartSalesOrder.LastUpdateBy)
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

            Dim sparePartSalesOrder As SparePartSalesOrder = CType(obj, SparePartSalesOrder)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartSalesOrder.ID)
            DbCommandWrapper.AddInParameter("@SalesChannel", DbType.Int16, sparePartSalesOrder.SalesChannel)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, sparePartSalesOrder.Owner)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sparePartSalesOrder.Status)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, sparePartSalesOrder.DealerCode)
            DbCommandWrapper.AddInParameter("@Customer", DbType.AnsiString, sparePartSalesOrder.Customer)
            DbCommandWrapper.AddInParameter("@CustomerNo", DbType.AnsiString, sparePartSalesOrder.CustomerNo)
            DbCommandWrapper.AddInParameter("@DownPaymentAmount", DbType.Currency, sparePartSalesOrder.DownPaymentAmount)
            DbCommandWrapper.AddInParameter("@DownPaymentAmountReceived", DbType.Currency, sparePartSalesOrder.DownPaymentAmountReceived)
            DbCommandWrapper.AddInParameter("@DownPaymentIsPaid", DbType.Boolean, sparePartSalesOrder.DownPaymentIsPaid)
            DbCommandWrapper.AddInParameter("@ExternalReferenceNo", DbType.AnsiString, sparePartSalesOrder.ExternalReferenceNo)
            DbCommandWrapper.AddInParameter("@GrandTotal", DbType.Currency, sparePartSalesOrder.GrandTotal)
            DbCommandWrapper.AddInParameter("@Handling", DbType.Int16, sparePartSalesOrder.Handling)
            DbCommandWrapper.AddInParameter("@MethodOfPayment", DbType.AnsiString, sparePartSalesOrder.MethodOfPayment)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, sparePartSalesOrder.OrderType)
            DbCommandWrapper.AddInParameter("@SalesOrderNo", DbType.AnsiString, sparePartSalesOrder.SalesOrderNo)
            DbCommandWrapper.AddInParameter("@SalesPerson", DbType.AnsiString, sparePartSalesOrder.SalesPerson)
            DbCommandWrapper.AddInParameter("@ShipmentType", DbType.AnsiString, sparePartSalesOrder.ShipmentType)
            DbCommandWrapper.AddInParameter("@State", DbType.AnsiString, sparePartSalesOrder.State)
            DbCommandWrapper.AddInParameter("@TermOfPayment", DbType.AnsiString, sparePartSalesOrder.TermOfPayment)
            DbCommandWrapper.AddInParameter("@TotalAmountBeforeDiscount", DbType.Currency, sparePartSalesOrder.TotalAmountBeforeDiscount)
            DbCommandWrapper.AddInParameter("@TotalBaseAmount", DbType.Currency, sparePartSalesOrder.TotalBaseAmount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTaxAmount", DbType.Currency, sparePartSalesOrder.TotalConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TotalDiscountAmount", DbType.Currency, sparePartSalesOrder.TotalDiscountAmount)
            DbCommandWrapper.AddInParameter("@TotalReceipt", DbType.Currency, sparePartSalesOrder.TotalReceipt)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, sparePartSalesOrder.TransactionDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartSalesOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartSalesOrder.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartSalesOrder

            Dim sparePartSalesOrder As SparePartSalesOrder = New SparePartSalesOrder

            sparePartSalesOrder.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesChannel")) Then sparePartSalesOrder.SalesChannel = CType(dr("SalesChannel"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then sparePartSalesOrder.Owner = dr("Owner").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sparePartSalesOrder.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then sparePartSalesOrder.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Customer")) Then sparePartSalesOrder.Customer = dr("Customer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerNo")) Then sparePartSalesOrder.CustomerNo = dr("CustomerNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DownPaymentAmount")) Then sparePartSalesOrder.DownPaymentAmount = CType(dr("DownPaymentAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DownPaymentAmountReceived")) Then sparePartSalesOrder.DownPaymentAmountReceived = CType(dr("DownPaymentAmountReceived"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DownPaymentIsPaid")) Then sparePartSalesOrder.DownPaymentIsPaid = CType(dr("DownPaymentIsPaid"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("ExternalReferenceNo")) Then sparePartSalesOrder.ExternalReferenceNo = dr("ExternalReferenceNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GrandTotal")) Then sparePartSalesOrder.GrandTotal = CType(dr("GrandTotal"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Handling")) Then sparePartSalesOrder.Handling = CType(dr("Handling"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("MethodOfPayment")) Then sparePartSalesOrder.MethodOfPayment = dr("MethodOfPayment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderType")) Then sparePartSalesOrder.OrderType = dr("OrderType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesOrderNo")) Then sparePartSalesOrder.SalesOrderNo = dr("SalesOrderNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesPerson")) Then sparePartSalesOrder.SalesPerson = dr("SalesPerson").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ShipmentType")) Then sparePartSalesOrder.ShipmentType = dr("ShipmentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("State")) Then sparePartSalesOrder.State = dr("State").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPayment")) Then sparePartSalesOrder.TermOfPayment = dr("TermOfPayment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmountBeforeDiscount")) Then sparePartSalesOrder.TotalAmountBeforeDiscount = CType(dr("TotalAmountBeforeDiscount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalBaseAmount")) Then sparePartSalesOrder.TotalBaseAmount = CType(dr("TotalBaseAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalConsumptionTaxAmount")) Then sparePartSalesOrder.TotalConsumptionTaxAmount = CType(dr("TotalConsumptionTaxAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalDiscountAmount")) Then sparePartSalesOrder.TotalDiscountAmount = CType(dr("TotalDiscountAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalReceipt")) Then sparePartSalesOrder.TotalReceipt = CType(dr("TotalReceipt"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionDate")) Then sparePartSalesOrder.TransactionDate = CType(dr("TransactionDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartSalesOrder.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartSalesOrder.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartSalesOrder.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartSalesOrder.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartSalesOrder.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sparePartSalesOrder

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartSalesOrder) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartSalesOrder), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartSalesOrder).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


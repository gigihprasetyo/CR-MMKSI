
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartDeliveryOrder Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 22/03/2018 - 16:14:40
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

    Public Class SparePartDeliveryOrderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartDeliveryOrder"
        Private m_UpdateStatement As String = "up_UpdateSparePartDeliveryOrder"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartDeliveryOrder"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartDeliveryOrderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartDeliveryOrder"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartDeliveryOrder As SparePartDeliveryOrder = Nothing
            While dr.Read

                sparePartDeliveryOrder = Me.CreateObject(dr)

            End While

            Return sparePartDeliveryOrder

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartDeliveryOrderList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartDeliveryOrder As SparePartDeliveryOrder = Me.CreateObject(dr)
                sparePartDeliveryOrderList.Add(sparePartDeliveryOrder)
            End While

            Return sparePartDeliveryOrderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartDeliveryOrder As SparePartDeliveryOrder = CType(obj, SparePartDeliveryOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartDeliveryOrder.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartDeliveryOrder As SparePartDeliveryOrder = CType(obj, SparePartDeliveryOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, sparePartDeliveryOrder.Owner)
            DbCommandWrapper.AddInParameter("@Address1", DbType.AnsiString, sparePartDeliveryOrder.Address1)
            DbCommandWrapper.AddInParameter("@Address2", DbType.AnsiString, sparePartDeliveryOrder.Address2)
            DbCommandWrapper.AddInParameter("@Address3", DbType.AnsiString, sparePartDeliveryOrder.Address3)
            DbCommandWrapper.AddInParameter("@Address4", DbType.AnsiString, sparePartDeliveryOrder.Address4)
            DbCommandWrapper.AddInParameter("@BusinessPhone", DbType.AnsiString, sparePartDeliveryOrder.BusinessPhone)
            DbCommandWrapper.AddInParameter("@BU", DbType.AnsiString, sparePartDeliveryOrder.BU)
            DbCommandWrapper.AddInParameter("@CancellationDate", DbType.DateTime, sparePartDeliveryOrder.CancellationDate)
            DbCommandWrapper.AddInParameter("@City", DbType.AnsiString, sparePartDeliveryOrder.City)
            DbCommandWrapper.AddInParameter("@CustomerContacts", DbType.AnsiString, sparePartDeliveryOrder.CustomerContacts)
            DbCommandWrapper.AddInParameter("@Customer", DbType.AnsiString, sparePartDeliveryOrder.Customer)
            DbCommandWrapper.AddInParameter("@CustomerNo", DbType.AnsiString, sparePartDeliveryOrder.CustomerNo)
            DbCommandWrapper.AddInParameter("@DeliveryAddress", DbType.AnsiString, sparePartDeliveryOrder.DeliveryAddress)
            DbCommandWrapper.AddInParameter("@DeliveryOrderNo", DbType.AnsiString, sparePartDeliveryOrder.DeliveryOrderNo)
            DbCommandWrapper.AddInParameter("@DeliveryType", DbType.Int32, sparePartDeliveryOrder.DeliveryType)
            DbCommandWrapper.AddInParameter("@ExternalReferenceNo", DbType.AnsiString, sparePartDeliveryOrder.ExternalReferenceNo)
            DbCommandWrapper.AddInParameter("@GrandTotal", DbType.Currency, sparePartDeliveryOrder.GrandTotal)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sparePartDeliveryOrder.Status)
            DbCommandWrapper.AddInParameter("@MethodofPayment", DbType.AnsiString, sparePartDeliveryOrder.MethodofPayment)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, sparePartDeliveryOrder.OrderType)
            DbCommandWrapper.AddInParameter("@ReferenceNo", DbType.AnsiString, sparePartDeliveryOrder.ReferenceNo)
            DbCommandWrapper.AddInParameter("@Salesperson", DbType.AnsiString, sparePartDeliveryOrder.Salesperson)
            DbCommandWrapper.AddInParameter("@State", DbType.Int16, sparePartDeliveryOrder.State)
            DbCommandWrapper.AddInParameter("@TermofPayment", DbType.AnsiString, sparePartDeliveryOrder.TermofPayment)
            DbCommandWrapper.AddInParameter("@TotalAmountBeforeDiscount", DbType.Currency, sparePartDeliveryOrder.TotalAmountBeforeDiscount)
            DbCommandWrapper.AddInParameter("@TotalBaseAmount", DbType.Currency, sparePartDeliveryOrder.TotalBaseAmount)
            DbCommandWrapper.AddInParameter("@TotalDiscountAmount", DbType.Currency, sparePartDeliveryOrder.TotalDiscountAmount)
            DbCommandWrapper.AddInParameter("@TotalMiscChargeBaseAmount", DbType.Currency, sparePartDeliveryOrder.TotalMiscChargeBaseAmount)
            DbCommandWrapper.AddInParameter("@TotalMiscChargeConsumptionTaxAmount", DbType.Currency, sparePartDeliveryOrder.TotalMiscChargeConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TotalReceipt", DbType.Currency, sparePartDeliveryOrder.TotalReceipt)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTaxAmount", DbType.Currency, sparePartDeliveryOrder.TotalConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, sparePartDeliveryOrder.TransactionDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartDeliveryOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartDeliveryOrder.LastUpdateBy)
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

            Dim sparePartDeliveryOrder As SparePartDeliveryOrder = CType(obj, SparePartDeliveryOrder)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartDeliveryOrder.ID)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, sparePartDeliveryOrder.Owner)
            DbCommandWrapper.AddInParameter("@Address1", DbType.AnsiString, sparePartDeliveryOrder.Address1)
            DbCommandWrapper.AddInParameter("@Address2", DbType.AnsiString, sparePartDeliveryOrder.Address2)
            DbCommandWrapper.AddInParameter("@Address3", DbType.AnsiString, sparePartDeliveryOrder.Address3)
            DbCommandWrapper.AddInParameter("@Address4", DbType.AnsiString, sparePartDeliveryOrder.Address4)
            DbCommandWrapper.AddInParameter("@BusinessPhone", DbType.AnsiString, sparePartDeliveryOrder.BusinessPhone)
            DbCommandWrapper.AddInParameter("@BU", DbType.AnsiString, sparePartDeliveryOrder.BU)
            DbCommandWrapper.AddInParameter("@CancellationDate", DbType.DateTime, sparePartDeliveryOrder.CancellationDate)
            DbCommandWrapper.AddInParameter("@City", DbType.AnsiString, sparePartDeliveryOrder.City)
            DbCommandWrapper.AddInParameter("@CustomerContacts", DbType.AnsiString, sparePartDeliveryOrder.CustomerContacts)
            DbCommandWrapper.AddInParameter("@Customer", DbType.AnsiString, sparePartDeliveryOrder.Customer)
            DbCommandWrapper.AddInParameter("@CustomerNo", DbType.AnsiString, sparePartDeliveryOrder.CustomerNo)
            DbCommandWrapper.AddInParameter("@DeliveryAddress", DbType.AnsiString, sparePartDeliveryOrder.DeliveryAddress)
            DbCommandWrapper.AddInParameter("@DeliveryOrderNo", DbType.AnsiString, sparePartDeliveryOrder.DeliveryOrderNo)
            DbCommandWrapper.AddInParameter("@DeliveryType", DbType.Int32, sparePartDeliveryOrder.DeliveryType)
            DbCommandWrapper.AddInParameter("@ExternalReferenceNo", DbType.AnsiString, sparePartDeliveryOrder.ExternalReferenceNo)
            DbCommandWrapper.AddInParameter("@GrandTotal", DbType.Currency, sparePartDeliveryOrder.GrandTotal)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sparePartDeliveryOrder.Status)
            DbCommandWrapper.AddInParameter("@MethodofPayment", DbType.AnsiString, sparePartDeliveryOrder.MethodofPayment)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, sparePartDeliveryOrder.OrderType)
            DbCommandWrapper.AddInParameter("@ReferenceNo", DbType.AnsiString, sparePartDeliveryOrder.ReferenceNo)
            DbCommandWrapper.AddInParameter("@Salesperson", DbType.AnsiString, sparePartDeliveryOrder.Salesperson)
            DbCommandWrapper.AddInParameter("@State", DbType.Int16, sparePartDeliveryOrder.State)
            DbCommandWrapper.AddInParameter("@TermofPayment", DbType.AnsiString, sparePartDeliveryOrder.TermofPayment)
            DbCommandWrapper.AddInParameter("@TotalAmountBeforeDiscount", DbType.Currency, sparePartDeliveryOrder.TotalAmountBeforeDiscount)
            DbCommandWrapper.AddInParameter("@TotalBaseAmount", DbType.Currency, sparePartDeliveryOrder.TotalBaseAmount)
            DbCommandWrapper.AddInParameter("@TotalDiscountAmount", DbType.Currency, sparePartDeliveryOrder.TotalDiscountAmount)
            DbCommandWrapper.AddInParameter("@TotalMiscChargeBaseAmount", DbType.Currency, sparePartDeliveryOrder.TotalMiscChargeBaseAmount)
            DbCommandWrapper.AddInParameter("@TotalMiscChargeConsumptionTaxAmount", DbType.Currency, sparePartDeliveryOrder.TotalMiscChargeConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TotalReceipt", DbType.Currency, sparePartDeliveryOrder.TotalReceipt)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTaxAmount", DbType.Currency, sparePartDeliveryOrder.TotalConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, sparePartDeliveryOrder.TransactionDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartDeliveryOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartDeliveryOrder.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartDeliveryOrder

            Dim sparePartDeliveryOrder As SparePartDeliveryOrder = New SparePartDeliveryOrder

            sparePartDeliveryOrder.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then sparePartDeliveryOrder.Owner = dr("Owner").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address1")) Then sparePartDeliveryOrder.Address1 = dr("Address1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address2")) Then sparePartDeliveryOrder.Address2 = dr("Address2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address3")) Then sparePartDeliveryOrder.Address3 = dr("Address3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address4")) Then sparePartDeliveryOrder.Address4 = dr("Address4").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BusinessPhone")) Then sparePartDeliveryOrder.BusinessPhone = dr("BusinessPhone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BU")) Then sparePartDeliveryOrder.BU = dr("BU").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CancellationDate")) Then sparePartDeliveryOrder.CancellationDate = CType(dr("CancellationDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("City")) Then sparePartDeliveryOrder.City = dr("City").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerContacts")) Then sparePartDeliveryOrder.CustomerContacts = dr("CustomerContacts").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Customer")) Then sparePartDeliveryOrder.Customer = dr("Customer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerNo")) Then sparePartDeliveryOrder.CustomerNo = dr("CustomerNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryAddress")) Then sparePartDeliveryOrder.DeliveryAddress = dr("DeliveryAddress").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryOrderNo")) Then sparePartDeliveryOrder.DeliveryOrderNo = dr("DeliveryOrderNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryType")) Then sparePartDeliveryOrder.DeliveryType = CType(dr("DeliveryType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ExternalReferenceNo")) Then sparePartDeliveryOrder.ExternalReferenceNo = dr("ExternalReferenceNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GrandTotal")) Then sparePartDeliveryOrder.GrandTotal = CType(dr("GrandTotal"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sparePartDeliveryOrder.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("MethodofPayment")) Then sparePartDeliveryOrder.MethodofPayment = dr("MethodofPayment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderType")) Then sparePartDeliveryOrder.OrderType = dr("OrderType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceNo")) Then sparePartDeliveryOrder.ReferenceNo = dr("ReferenceNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Salesperson")) Then sparePartDeliveryOrder.Salesperson = dr("Salesperson").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("State")) Then sparePartDeliveryOrder.State = CType(dr("State"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TermofPayment")) Then sparePartDeliveryOrder.TermofPayment = dr("TermofPayment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmountBeforeDiscount")) Then sparePartDeliveryOrder.TotalAmountBeforeDiscount = CType(dr("TotalAmountBeforeDiscount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalBaseAmount")) Then sparePartDeliveryOrder.TotalBaseAmount = CType(dr("TotalBaseAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalDiscountAmount")) Then sparePartDeliveryOrder.TotalDiscountAmount = CType(dr("TotalDiscountAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalMiscChargeBaseAmount")) Then sparePartDeliveryOrder.TotalMiscChargeBaseAmount = CType(dr("TotalMiscChargeBaseAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalMiscChargeConsumptionTaxAmount")) Then sparePartDeliveryOrder.TotalMiscChargeConsumptionTaxAmount = CType(dr("TotalMiscChargeConsumptionTaxAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalReceipt")) Then sparePartDeliveryOrder.TotalReceipt = CType(dr("TotalReceipt"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalConsumptionTaxAmount")) Then sparePartDeliveryOrder.TotalConsumptionTaxAmount = CType(dr("TotalConsumptionTaxAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionDate")) Then sparePartDeliveryOrder.TransactionDate = CType(dr("TransactionDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartDeliveryOrder.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartDeliveryOrder.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartDeliveryOrder.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartDeliveryOrder.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartDeliveryOrder.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sparePartDeliveryOrder

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartDeliveryOrder) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartDeliveryOrder), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartDeliveryOrder).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


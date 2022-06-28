
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartDeliveryOrderDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 24/03/2018 - 13:23:02
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

    Public Class SparePartDeliveryOrderDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartDeliveryOrderDetail"
        Private m_UpdateStatement As String = "up_UpdateSparePartDeliveryOrderDetail"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartDeliveryOrderDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartDeliveryOrderDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartDeliveryOrderDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartDeliveryOrderDetail As SparePartDeliveryOrderDetail = Nothing
            While dr.Read

                sparePartDeliveryOrderDetail = Me.CreateObject(dr)

            End While

            Return sparePartDeliveryOrderDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartDeliveryOrderDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartDeliveryOrderDetail As SparePartDeliveryOrderDetail = Me.CreateObject(dr)
                sparePartDeliveryOrderDetailList.Add(sparePartDeliveryOrderDetail)
            End While

            Return sparePartDeliveryOrderDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartDeliveryOrderDetail As SparePartDeliveryOrderDetail = CType(obj, SparePartDeliveryOrderDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartDeliveryOrderDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartDeliveryOrderDetail As SparePartDeliveryOrderDetail = CType(obj, SparePartDeliveryOrderDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, sparePartDeliveryOrderDetail.Owner)
            DbCommandWrapper.AddInParameter("@AmountBeforeDiscount", DbType.Currency, sparePartDeliveryOrderDetail.AmountBeforeDiscount)
            DbCommandWrapper.AddInParameter("@BaseAmount", DbType.Currency, sparePartDeliveryOrderDetail.BaseAmount)
            DbCommandWrapper.AddInParameter("@BaseQtyDelivered", DbType.Double, sparePartDeliveryOrderDetail.BaseQtyDelivered)
            DbCommandWrapper.AddInParameter("@BaseQtyOrder", DbType.Double, sparePartDeliveryOrderDetail.BaseQtyOrder)
            DbCommandWrapper.AddInParameter("@BatchNo", DbType.AnsiString, sparePartDeliveryOrderDetail.BatchNo)
            DbCommandWrapper.AddInParameter("@BU", DbType.AnsiString, sparePartDeliveryOrderDetail.BU)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1Amount", DbType.Currency, sparePartDeliveryOrderDetail.ConsumptionTax1Amount)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1", DbType.AnsiString, sparePartDeliveryOrderDetail.ConsumptionTax1)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2Amount", DbType.Currency, sparePartDeliveryOrderDetail.ConsumptionTax2Amount)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2", DbType.AnsiString, sparePartDeliveryOrderDetail.ConsumptionTax2)
            DbCommandWrapper.AddInParameter("@DeliveryOrderDetail", DbType.AnsiString, sparePartDeliveryOrderDetail.DeliveryOrderDetail)
            DbCommandWrapper.AddInParameter("@DeliveryOrderNo", DbType.AnsiString, sparePartDeliveryOrderDetail.DeliveryOrderNo)
            DbCommandWrapper.AddInParameter("@DiscountAmount", DbType.Currency, sparePartDeliveryOrderDetail.DiscountAmount)
            DbCommandWrapper.AddInParameter("@DiscountBaseAmount", DbType.Currency, sparePartDeliveryOrderDetail.DiscountBaseAmount)
            DbCommandWrapper.AddInParameter("@DiscountPercentage", DbType.Double, sparePartDeliveryOrderDetail.DiscountPercentage)
            DbCommandWrapper.AddInParameter("@Location", DbType.AnsiString, sparePartDeliveryOrderDetail.Location)
            DbCommandWrapper.AddInParameter("@ProductCrossReference", DbType.AnsiString, sparePartDeliveryOrderDetail.ProductCrossReference)
            DbCommandWrapper.AddInParameter("@ProductDescription", DbType.AnsiString, sparePartDeliveryOrderDetail.ProductDescription)
            DbCommandWrapper.AddInParameter("@Product", DbType.AnsiString, sparePartDeliveryOrderDetail.Product)
            DbCommandWrapper.AddInParameter("@PromiseDate", DbType.DateTime, sparePartDeliveryOrderDetail.PromiseDate)
            DbCommandWrapper.AddInParameter("@QtyDelivered", DbType.Double, sparePartDeliveryOrderDetail.QtyDelivered)
            DbCommandWrapper.AddInParameter("@QtyOrder", DbType.Double, sparePartDeliveryOrderDetail.QtyOrder)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.DateTime, sparePartDeliveryOrderDetail.RequestDate)
            DbCommandWrapper.AddInParameter("@RunningNumber", DbType.Int32, sparePartDeliveryOrderDetail.RunningNumber)
            DbCommandWrapper.AddInParameter("@SalesOrderDetail", DbType.AnsiString, sparePartDeliveryOrderDetail.SalesOrderDetail)
            DbCommandWrapper.AddInParameter("@SalesUnit", DbType.AnsiString, sparePartDeliveryOrderDetail.SalesUnit)
            DbCommandWrapper.AddInParameter("@Site", DbType.AnsiString, sparePartDeliveryOrderDetail.Site)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, sparePartDeliveryOrderDetail.TotalAmount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTaxAmount", DbType.Currency, sparePartDeliveryOrderDetail.TotalConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TransactionAmount", DbType.Currency, sparePartDeliveryOrderDetail.TransactionAmount)
            DbCommandWrapper.AddInParameter("@UnitPrice", DbType.Currency, sparePartDeliveryOrderDetail.UnitPrice)
            DbCommandWrapper.AddInParameter("@Warehouse", DbType.AnsiString, sparePartDeliveryOrderDetail.Warehouse)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartDeliveryOrderDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartDeliveryOrderDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartDeliveryOrderID", DbType.Int32, Me.GetRefObject(sparePartDeliveryOrderDetail.SparePartDeliveryOrder))

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

            Dim sparePartDeliveryOrderDetail As SparePartDeliveryOrderDetail = CType(obj, SparePartDeliveryOrderDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartDeliveryOrderDetail.ID)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, sparePartDeliveryOrderDetail.Owner)
            DbCommandWrapper.AddInParameter("@AmountBeforeDiscount", DbType.Currency, sparePartDeliveryOrderDetail.AmountBeforeDiscount)
            DbCommandWrapper.AddInParameter("@BaseAmount", DbType.Currency, sparePartDeliveryOrderDetail.BaseAmount)
            DbCommandWrapper.AddInParameter("@BaseQtyDelivered", DbType.Double, sparePartDeliveryOrderDetail.BaseQtyDelivered)
            DbCommandWrapper.AddInParameter("@BaseQtyOrder", DbType.Double, sparePartDeliveryOrderDetail.BaseQtyOrder)
            DbCommandWrapper.AddInParameter("@BatchNo", DbType.AnsiString, sparePartDeliveryOrderDetail.BatchNo)
            DbCommandWrapper.AddInParameter("@BU", DbType.AnsiString, sparePartDeliveryOrderDetail.BU)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1Amount", DbType.Currency, sparePartDeliveryOrderDetail.ConsumptionTax1Amount)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1", DbType.AnsiString, sparePartDeliveryOrderDetail.ConsumptionTax1)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2Amount", DbType.Currency, sparePartDeliveryOrderDetail.ConsumptionTax2Amount)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2", DbType.AnsiString, sparePartDeliveryOrderDetail.ConsumptionTax2)
            DbCommandWrapper.AddInParameter("@DeliveryOrderDetail", DbType.AnsiString, sparePartDeliveryOrderDetail.DeliveryOrderDetail)
            DbCommandWrapper.AddInParameter("@DeliveryOrderNo", DbType.AnsiString, sparePartDeliveryOrderDetail.DeliveryOrderNo)
            DbCommandWrapper.AddInParameter("@DiscountAmount", DbType.Currency, sparePartDeliveryOrderDetail.DiscountAmount)
            DbCommandWrapper.AddInParameter("@DiscountBaseAmount", DbType.Currency, sparePartDeliveryOrderDetail.DiscountBaseAmount)
            DbCommandWrapper.AddInParameter("@DiscountPercentage", DbType.Double, sparePartDeliveryOrderDetail.DiscountPercentage)
            DbCommandWrapper.AddInParameter("@Location", DbType.AnsiString, sparePartDeliveryOrderDetail.Location)
            DbCommandWrapper.AddInParameter("@ProductCrossReference", DbType.AnsiString, sparePartDeliveryOrderDetail.ProductCrossReference)
            DbCommandWrapper.AddInParameter("@ProductDescription", DbType.AnsiString, sparePartDeliveryOrderDetail.ProductDescription)
            DbCommandWrapper.AddInParameter("@Product", DbType.AnsiString, sparePartDeliveryOrderDetail.Product)
            DbCommandWrapper.AddInParameter("@PromiseDate", DbType.DateTime, sparePartDeliveryOrderDetail.PromiseDate)
            DbCommandWrapper.AddInParameter("@QtyDelivered", DbType.Double, sparePartDeliveryOrderDetail.QtyDelivered)
            DbCommandWrapper.AddInParameter("@QtyOrder", DbType.Double, sparePartDeliveryOrderDetail.QtyOrder)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.DateTime, sparePartDeliveryOrderDetail.RequestDate)
            DbCommandWrapper.AddInParameter("@RunningNumber", DbType.Int32, sparePartDeliveryOrderDetail.RunningNumber)
            DbCommandWrapper.AddInParameter("@SalesOrderDetail", DbType.AnsiString, sparePartDeliveryOrderDetail.SalesOrderDetail)
            DbCommandWrapper.AddInParameter("@SalesUnit", DbType.AnsiString, sparePartDeliveryOrderDetail.SalesUnit)
            DbCommandWrapper.AddInParameter("@Site", DbType.AnsiString, sparePartDeliveryOrderDetail.Site)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, sparePartDeliveryOrderDetail.TotalAmount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTaxAmount", DbType.Currency, sparePartDeliveryOrderDetail.TotalConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TransactionAmount", DbType.Currency, sparePartDeliveryOrderDetail.TransactionAmount)
            DbCommandWrapper.AddInParameter("@UnitPrice", DbType.Currency, sparePartDeliveryOrderDetail.UnitPrice)
            DbCommandWrapper.AddInParameter("@Warehouse", DbType.AnsiString, sparePartDeliveryOrderDetail.Warehouse)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartDeliveryOrderDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartDeliveryOrderDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartDeliveryOrderID", DbType.Int32, Me.GetRefObject(sparePartDeliveryOrderDetail.SparePartDeliveryOrder))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartDeliveryOrderDetail

            Dim sparePartDeliveryOrderDetail As SparePartDeliveryOrderDetail = New SparePartDeliveryOrderDetail

            sparePartDeliveryOrderDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then sparePartDeliveryOrderDetail.Owner = dr("Owner").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AmountBeforeDiscount")) Then sparePartDeliveryOrderDetail.AmountBeforeDiscount = CType(dr("AmountBeforeDiscount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("BaseAmount")) Then sparePartDeliveryOrderDetail.BaseAmount = CType(dr("BaseAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("BaseQtyDelivered")) Then sparePartDeliveryOrderDetail.BaseQtyDelivered = CType(dr("BaseQtyDelivered"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("BaseQtyOrder")) Then sparePartDeliveryOrderDetail.BaseQtyOrder = CType(dr("BaseQtyOrder"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("BatchNo")) Then sparePartDeliveryOrderDetail.BatchNo = dr("BatchNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BU")) Then sparePartDeliveryOrderDetail.BU = dr("BU").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax1Amount")) Then sparePartDeliveryOrderDetail.ConsumptionTax1Amount = CType(dr("ConsumptionTax1Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax1")) Then sparePartDeliveryOrderDetail.ConsumptionTax1 = dr("ConsumptionTax1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax2Amount")) Then sparePartDeliveryOrderDetail.ConsumptionTax2Amount = CType(dr("ConsumptionTax2Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax2")) Then sparePartDeliveryOrderDetail.ConsumptionTax2 = dr("ConsumptionTax2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryOrderDetail")) Then sparePartDeliveryOrderDetail.DeliveryOrderDetail = dr("DeliveryOrderDetail").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryOrderNo")) Then sparePartDeliveryOrderDetail.DeliveryOrderNo = dr("DeliveryOrderNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountAmount")) Then sparePartDeliveryOrderDetail.DiscountAmount = CType(dr("DiscountAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountBaseAmount")) Then sparePartDeliveryOrderDetail.DiscountBaseAmount = CType(dr("DiscountBaseAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountPercentage")) Then sparePartDeliveryOrderDetail.DiscountPercentage = CType(dr("DiscountPercentage"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("Location")) Then sparePartDeliveryOrderDetail.Location = dr("Location").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCrossReference")) Then sparePartDeliveryOrderDetail.ProductCrossReference = dr("ProductCrossReference").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductDescription")) Then sparePartDeliveryOrderDetail.ProductDescription = dr("ProductDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Product")) Then sparePartDeliveryOrderDetail.Product = dr("Product").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PromiseDate")) Then sparePartDeliveryOrderDetail.PromiseDate = CType(dr("PromiseDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("QtyDelivered")) Then sparePartDeliveryOrderDetail.QtyDelivered = CType(dr("QtyDelivered"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("QtyOrder")) Then sparePartDeliveryOrderDetail.QtyOrder = CType(dr("QtyOrder"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestDate")) Then sparePartDeliveryOrderDetail.RequestDate = CType(dr("RequestDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RunningNumber")) Then sparePartDeliveryOrderDetail.RunningNumber = CType(dr("RunningNumber"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesOrderDetail")) Then sparePartDeliveryOrderDetail.SalesOrderDetail = dr("SalesOrderDetail").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesUnit")) Then sparePartDeliveryOrderDetail.SalesUnit = dr("SalesUnit").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Site")) Then sparePartDeliveryOrderDetail.Site = dr("Site").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then sparePartDeliveryOrderDetail.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalConsumptionTaxAmount")) Then sparePartDeliveryOrderDetail.TotalConsumptionTaxAmount = CType(dr("TotalConsumptionTaxAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionAmount")) Then sparePartDeliveryOrderDetail.TransactionAmount = CType(dr("TransactionAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("UnitPrice")) Then sparePartDeliveryOrderDetail.UnitPrice = CType(dr("UnitPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Warehouse")) Then sparePartDeliveryOrderDetail.Warehouse = dr("Warehouse").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartDeliveryOrderDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartDeliveryOrderDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartDeliveryOrderDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartDeliveryOrderDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartDeliveryOrderDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartDeliveryOrderID")) Then
                sparePartDeliveryOrderDetail.SparePartDeliveryOrder = New SparePartDeliveryOrder(CType(dr("SparePartDeliveryOrderID"), Integer))
            End If

            Return sparePartDeliveryOrderDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartDeliveryOrderDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartDeliveryOrderDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartDeliveryOrderDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


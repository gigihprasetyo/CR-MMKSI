
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : MitraisTeam
'// PURPOSE       : SparePartSalesOrderDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 22/03/2018 - 16:42:39
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

    Public Class SparePartSalesOrderDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartSalesOrderDetail"
        Private m_UpdateStatement As String = "up_UpdateSparePartSalesOrderDetail"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartSalesOrderDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartSalesOrderDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartSalesOrderDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartSalesOrderDetail As SparePartSalesOrderDetail = Nothing
            While dr.Read

                sparePartSalesOrderDetail = Me.CreateObject(dr)

            End While

            Return sparePartSalesOrderDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartSalesOrderDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartSalesOrderDetail As SparePartSalesOrderDetail = Me.CreateObject(dr)
                sparePartSalesOrderDetailList.Add(sparePartSalesOrderDetail)
            End While

            Return sparePartSalesOrderDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartSalesOrderDetail As SparePartSalesOrderDetail = CType(obj, SparePartSalesOrderDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartSalesOrderDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartSalesOrderDetail As SparePartSalesOrderDetail = CType(obj, SparePartSalesOrderDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, sparePartSalesOrderDetail.Owner)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sparePartSalesOrderDetail.Status)
            DbCommandWrapper.AddInParameter("@AmountBeforeDiscount", DbType.Currency, sparePartSalesOrderDetail.AmountBeforeDiscount)
            DbCommandWrapper.AddInParameter("@BaseAmount", DbType.Currency, sparePartSalesOrderDetail.BaseAmount)
            DbCommandWrapper.AddInParameter("@KodeDealer", DbType.AnsiString, sparePartSalesOrderDetail.KodeDealer)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1Amount", DbType.Currency, sparePartSalesOrderDetail.ConsumptionTax1Amount)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1", DbType.AnsiString, sparePartSalesOrderDetail.ConsumptionTax1)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2Amount", DbType.Currency, sparePartSalesOrderDetail.ConsumptionTax2Amount)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2", DbType.AnsiString, sparePartSalesOrderDetail.ConsumptionTax2)
            DbCommandWrapper.AddInParameter("@DiscountAmount", DbType.Currency, sparePartSalesOrderDetail.DiscountAmount)
            DbCommandWrapper.AddInParameter("@DiscountPercentAge", DbType.Decimal, sparePartSalesOrderDetail.DiscountPercentAge)
            DbCommandWrapper.AddInParameter("@ProductCrossReference", DbType.AnsiString, sparePartSalesOrderDetail.ProductCrossReference)
            DbCommandWrapper.AddInParameter("@ProductDescription", DbType.AnsiString, sparePartSalesOrderDetail.ProductDescription)
            DbCommandWrapper.AddInParameter("@Product", DbType.AnsiString, sparePartSalesOrderDetail.Product)
            DbCommandWrapper.AddInParameter("@PromiseDate", DbType.DateTime, sparePartSalesOrderDetail.PromiseDate)
            DbCommandWrapper.AddInParameter("@QtyDelivered", DbType.Double, sparePartSalesOrderDetail.QtyDelivered)
            DbCommandWrapper.AddInParameter("@QtyOrder", DbType.Double, sparePartSalesOrderDetail.QtyOrder)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.DateTime, sparePartSalesOrderDetail.RequestDate)
            DbCommandWrapper.AddInParameter("@SalesOrderDetailID", DbType.AnsiString, sparePartSalesOrderDetail.SalesOrderDetailID)
            DbCommandWrapper.AddInParameter("@SalesOrderNo", DbType.AnsiString, sparePartSalesOrderDetail.SalesOrderNo)
            DbCommandWrapper.AddInParameter("@SalesUnit", DbType.AnsiString, sparePartSalesOrderDetail.SalesUnit)
            DbCommandWrapper.AddInParameter("@Site", DbType.AnsiString, sparePartSalesOrderDetail.Site)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, sparePartSalesOrderDetail.TotalAmount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTaxAmount", DbType.Currency, sparePartSalesOrderDetail.TotalConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TransactionAmount", DbType.Currency, sparePartSalesOrderDetail.TransactionAmount)
            DbCommandWrapper.AddInParameter("@UnitPrice", DbType.Currency, sparePartSalesOrderDetail.UnitPrice)
            DbCommandWrapper.AddInParameter("@Warehouse", DbType.AnsiString, sparePartSalesOrderDetail.Warehouse)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartSalesOrderDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartSalesOrderDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartSalesOrderID", DbType.Int32, Me.GetRefObject(sparePartSalesOrderDetail.SparePartSalesOrder))

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

            Dim sparePartSalesOrderDetail As SparePartSalesOrderDetail = CType(obj, SparePartSalesOrderDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartSalesOrderDetail.ID)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, sparePartSalesOrderDetail.Owner)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sparePartSalesOrderDetail.Status)
            DbCommandWrapper.AddInParameter("@AmountBeforeDiscount", DbType.Currency, sparePartSalesOrderDetail.AmountBeforeDiscount)
            DbCommandWrapper.AddInParameter("@BaseAmount", DbType.Currency, sparePartSalesOrderDetail.BaseAmount)
            DbCommandWrapper.AddInParameter("@KodeDealer", DbType.AnsiString, sparePartSalesOrderDetail.KodeDealer)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1Amount", DbType.Currency, sparePartSalesOrderDetail.ConsumptionTax1Amount)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1", DbType.AnsiString, sparePartSalesOrderDetail.ConsumptionTax1)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2Amount", DbType.Currency, sparePartSalesOrderDetail.ConsumptionTax2Amount)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2", DbType.AnsiString, sparePartSalesOrderDetail.ConsumptionTax2)
            DbCommandWrapper.AddInParameter("@DiscountAmount", DbType.Currency, sparePartSalesOrderDetail.DiscountAmount)
            DbCommandWrapper.AddInParameter("@DiscountPercentAge", DbType.Decimal, sparePartSalesOrderDetail.DiscountPercentAge)
            DbCommandWrapper.AddInParameter("@ProductCrossReference", DbType.AnsiString, sparePartSalesOrderDetail.ProductCrossReference)
            DbCommandWrapper.AddInParameter("@ProductDescription", DbType.AnsiString, sparePartSalesOrderDetail.ProductDescription)
            DbCommandWrapper.AddInParameter("@Product", DbType.AnsiString, sparePartSalesOrderDetail.Product)
            DbCommandWrapper.AddInParameter("@PromiseDate", DbType.DateTime, sparePartSalesOrderDetail.PromiseDate)
            DbCommandWrapper.AddInParameter("@QtyDelivered", DbType.Double, sparePartSalesOrderDetail.QtyDelivered)
            DbCommandWrapper.AddInParameter("@QtyOrder", DbType.Double, sparePartSalesOrderDetail.QtyOrder)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.DateTime, sparePartSalesOrderDetail.RequestDate)
            DbCommandWrapper.AddInParameter("@SalesOrderDetailID", DbType.AnsiString, sparePartSalesOrderDetail.SalesOrderDetailID)
            DbCommandWrapper.AddInParameter("@SalesOrderNo", DbType.AnsiString, sparePartSalesOrderDetail.SalesOrderNo)
            DbCommandWrapper.AddInParameter("@SalesUnit", DbType.AnsiString, sparePartSalesOrderDetail.SalesUnit)
            DbCommandWrapper.AddInParameter("@Site", DbType.AnsiString, sparePartSalesOrderDetail.Site)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, sparePartSalesOrderDetail.TotalAmount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTaxAmount", DbType.Currency, sparePartSalesOrderDetail.TotalConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TransactionAmount", DbType.Currency, sparePartSalesOrderDetail.TransactionAmount)
            DbCommandWrapper.AddInParameter("@UnitPrice", DbType.Currency, sparePartSalesOrderDetail.UnitPrice)
            DbCommandWrapper.AddInParameter("@Warehouse", DbType.AnsiString, sparePartSalesOrderDetail.Warehouse)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartSalesOrderDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartSalesOrderDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartSalesOrderID", DbType.Int32, Me.GetRefObject(sparePartSalesOrderDetail.SparePartSalesOrder))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartSalesOrderDetail

            Dim sparePartSalesOrderDetail As SparePartSalesOrderDetail = New SparePartSalesOrderDetail

            sparePartSalesOrderDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then sparePartSalesOrderDetail.Owner = dr("Owner").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sparePartSalesOrderDetail.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("AmountBeforeDiscount")) Then sparePartSalesOrderDetail.AmountBeforeDiscount = CType(dr("AmountBeforeDiscount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("BaseAmount")) Then sparePartSalesOrderDetail.BaseAmount = CType(dr("BaseAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("KodeDealer")) Then sparePartSalesOrderDetail.KodeDealer = dr("KodeDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax1Amount")) Then sparePartSalesOrderDetail.ConsumptionTax1Amount = CType(dr("ConsumptionTax1Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax1")) Then sparePartSalesOrderDetail.ConsumptionTax1 = dr("ConsumptionTax1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax2Amount")) Then sparePartSalesOrderDetail.ConsumptionTax2Amount = CType(dr("ConsumptionTax2Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax2")) Then sparePartSalesOrderDetail.ConsumptionTax2 = dr("ConsumptionTax2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountAmount")) Then sparePartSalesOrderDetail.DiscountAmount = CType(dr("DiscountAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountPercentAge")) Then sparePartSalesOrderDetail.DiscountPercentAge = CType(dr("DiscountPercentAge"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCrossReference")) Then sparePartSalesOrderDetail.ProductCrossReference = dr("ProductCrossReference").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductDescription")) Then sparePartSalesOrderDetail.ProductDescription = dr("ProductDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Product")) Then sparePartSalesOrderDetail.Product = dr("Product").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PromiseDate")) Then sparePartSalesOrderDetail.PromiseDate = CType(dr("PromiseDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("QtyDelivered")) Then sparePartSalesOrderDetail.QtyDelivered = CType(dr("QtyDelivered"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("QtyOrder")) Then sparePartSalesOrderDetail.QtyOrder = CType(dr("QtyOrder"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestDate")) Then sparePartSalesOrderDetail.RequestDate = CType(dr("RequestDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesOrderDetailID")) Then sparePartSalesOrderDetail.SalesOrderDetailID = dr("SalesOrderDetailID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesOrderNo")) Then sparePartSalesOrderDetail.SalesOrderNo = dr("SalesOrderNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesUnit")) Then sparePartSalesOrderDetail.SalesUnit = dr("SalesUnit").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Site")) Then sparePartSalesOrderDetail.Site = dr("Site").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then sparePartSalesOrderDetail.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalConsumptionTaxAmount")) Then sparePartSalesOrderDetail.TotalConsumptionTaxAmount = CType(dr("TotalConsumptionTaxAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionAmount")) Then sparePartSalesOrderDetail.TransactionAmount = CType(dr("TransactionAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("UnitPrice")) Then sparePartSalesOrderDetail.UnitPrice = CType(dr("UnitPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Warehouse")) Then sparePartSalesOrderDetail.Warehouse = dr("Warehouse").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartSalesOrderDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartSalesOrderDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartSalesOrderDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartSalesOrderDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartSalesOrderDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartSalesOrderID")) Then
                sparePartSalesOrderDetail.SparePartSalesOrder = New SparePartSalesOrder(CType(dr("SparePartSalesOrderID"), Integer))
            End If

            Return sparePartSalesOrderDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartSalesOrderDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartSalesOrderDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartSalesOrderDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


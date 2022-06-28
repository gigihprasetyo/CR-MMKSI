
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : MitraisTeam
'// PURPOSE       : VehiclePurchaseDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 21/03/2018 - 17:24:01
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

    Public Class VehiclePurchaseDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVehiclePurchaseDetail"
        Private m_UpdateStatement As String = "up_UpdateVehiclePurchaseDetail"
        Private m_RetrieveStatement As String = "up_RetrieveVehiclePurchaseDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveVehiclePurchaseDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVehiclePurchaseDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vehiclePurchaseDetail As VehiclePurchaseDetail = Nothing
            While dr.Read

                vehiclePurchaseDetail = Me.CreateObject(dr)

            End While

            Return vehiclePurchaseDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vehiclePurchaseDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim vehiclePurchaseDetail As VehiclePurchaseDetail = Me.CreateObject(dr)
                vehiclePurchaseDetailList.Add(vehiclePurchaseDetail)
            End While

            Return vehiclePurchaseDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vehiclePurchaseDetail As VehiclePurchaseDetail = CType(obj, VehiclePurchaseDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vehiclePurchaseDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vehiclePurchaseDetail As VehiclePurchaseDetail = CType(obj, VehiclePurchaseDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@BUCode", DbType.AnsiString, vehiclePurchaseDetail.BUCode)
            DbCommandWrapper.AddInParameter("@BUName", DbType.AnsiString, vehiclePurchaseDetail.BUName)
            DbCommandWrapper.AddInParameter("@CloseLine", DbType.Boolean, vehiclePurchaseDetail.CloseLine)
            DbCommandWrapper.AddInParameter("@CloseLineName", DbType.AnsiString, vehiclePurchaseDetail.CloseLineName)
            DbCommandWrapper.AddInParameter("@CloseReason", DbType.AnsiString, vehiclePurchaseDetail.CloseReason)
            DbCommandWrapper.AddInParameter("@Completed", DbType.Boolean, vehiclePurchaseDetail.Completed)
            DbCommandWrapper.AddInParameter("@CompletedName", DbType.AnsiString, vehiclePurchaseDetail.CompletedName)
            DbCommandWrapper.AddInParameter("@ProductDescription", DbType.AnsiString, vehiclePurchaseDetail.ProductDescription)
            DbCommandWrapper.AddInParameter("@ProductName", DbType.AnsiString, vehiclePurchaseDetail.ProductName)
            DbCommandWrapper.AddInParameter("@ProductVariantName", DbType.AnsiString, vehiclePurchaseDetail.ProductVariantName)
            DbCommandWrapper.AddInParameter("@PODetail", DbType.AnsiString, vehiclePurchaseDetail.PODetail)
            DbCommandWrapper.AddInParameter("@POName", DbType.AnsiString, vehiclePurchaseDetail.POName)
            DbCommandWrapper.AddInParameter("@PRDetailName", DbType.AnsiString, vehiclePurchaseDetail.PRDetailName)
            DbCommandWrapper.AddInParameter("@PurchaseUnitName", DbType.AnsiString, vehiclePurchaseDetail.PurchaseUnitName)
            DbCommandWrapper.AddInParameter("@QtyOrder", DbType.Double, vehiclePurchaseDetail.QtyOrder)
            DbCommandWrapper.AddInParameter("@QtyReceipt", DbType.Double, vehiclePurchaseDetail.QtyReceipt)
            DbCommandWrapper.AddInParameter("@QtyReturn", DbType.Double, vehiclePurchaseDetail.QtyReturn)
            DbCommandWrapper.AddInParameter("@RecallProduct", DbType.Boolean, vehiclePurchaseDetail.RecallProduct)
            DbCommandWrapper.AddInParameter("@RecallProductName", DbType.AnsiString, vehiclePurchaseDetail.RecallProductName)
            DbCommandWrapper.AddInParameter("@SODetailName", DbType.AnsiString, vehiclePurchaseDetail.SODetailName)
            DbCommandWrapper.AddInParameter("@ScheduledShippingDate", DbType.DateTime, vehiclePurchaseDetail.ScheduledShippingDate)
            DbCommandWrapper.AddInParameter("@ServicePartsAndMaterial", DbType.AnsiString, vehiclePurchaseDetail.ServicePartsAndMaterial)
            DbCommandWrapper.AddInParameter("@ShippingDate", DbType.DateTime, vehiclePurchaseDetail.ShippingDate)
            DbCommandWrapper.AddInParameter("@Site", DbType.AnsiString, vehiclePurchaseDetail.Site)
            DbCommandWrapper.AddInParameter("@StockNumberName", DbType.AnsiString, vehiclePurchaseDetail.StockNumberName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vehiclePurchaseDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vehiclePurchaseDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@VehiclePurchaseHeaderID", DbType.Int32, Me.GetRefObject(vehiclePurchaseDetail.VehiclePurchaseHeader))

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

            Dim vehiclePurchaseDetail As VehiclePurchaseDetail = CType(obj, VehiclePurchaseDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vehiclePurchaseDetail.ID)
            DbCommandWrapper.AddInParameter("@BUCode", DbType.AnsiString, vehiclePurchaseDetail.BUCode)
            DbCommandWrapper.AddInParameter("@BUName", DbType.AnsiString, vehiclePurchaseDetail.BUName)
            DbCommandWrapper.AddInParameter("@CloseLine", DbType.Boolean, vehiclePurchaseDetail.CloseLine)
            DbCommandWrapper.AddInParameter("@CloseLineName", DbType.AnsiString, vehiclePurchaseDetail.CloseLineName)
            DbCommandWrapper.AddInParameter("@CloseReason", DbType.AnsiString, vehiclePurchaseDetail.CloseReason)
            DbCommandWrapper.AddInParameter("@Completed", DbType.Boolean, vehiclePurchaseDetail.Completed)
            DbCommandWrapper.AddInParameter("@CompletedName", DbType.AnsiString, vehiclePurchaseDetail.CompletedName)
            DbCommandWrapper.AddInParameter("@ProductDescription", DbType.AnsiString, vehiclePurchaseDetail.ProductDescription)
            DbCommandWrapper.AddInParameter("@ProductName", DbType.AnsiString, vehiclePurchaseDetail.ProductName)
            DbCommandWrapper.AddInParameter("@ProductVariantName", DbType.AnsiString, vehiclePurchaseDetail.ProductVariantName)
            DbCommandWrapper.AddInParameter("@PODetail", DbType.AnsiString, vehiclePurchaseDetail.PODetail)
            DbCommandWrapper.AddInParameter("@POName", DbType.AnsiString, vehiclePurchaseDetail.POName)
            DbCommandWrapper.AddInParameter("@PRDetailName", DbType.AnsiString, vehiclePurchaseDetail.PRDetailName)
            DbCommandWrapper.AddInParameter("@PurchaseUnitName", DbType.AnsiString, vehiclePurchaseDetail.PurchaseUnitName)
            DbCommandWrapper.AddInParameter("@QtyOrder", DbType.Double, vehiclePurchaseDetail.QtyOrder)
            DbCommandWrapper.AddInParameter("@QtyReceipt", DbType.Double, vehiclePurchaseDetail.QtyReceipt)
            DbCommandWrapper.AddInParameter("@QtyReturn", DbType.Double, vehiclePurchaseDetail.QtyReturn)
            DbCommandWrapper.AddInParameter("@RecallProduct", DbType.Boolean, vehiclePurchaseDetail.RecallProduct)
            DbCommandWrapper.AddInParameter("@RecallProductName", DbType.AnsiString, vehiclePurchaseDetail.RecallProductName)
            DbCommandWrapper.AddInParameter("@SODetailName", DbType.AnsiString, vehiclePurchaseDetail.SODetailName)
            DbCommandWrapper.AddInParameter("@ScheduledShippingDate", DbType.DateTime, vehiclePurchaseDetail.ScheduledShippingDate)
            DbCommandWrapper.AddInParameter("@ServicePartsAndMaterial", DbType.AnsiString, vehiclePurchaseDetail.ServicePartsAndMaterial)
            DbCommandWrapper.AddInParameter("@ShippingDate", DbType.DateTime, vehiclePurchaseDetail.ShippingDate)
            DbCommandWrapper.AddInParameter("@Site", DbType.AnsiString, vehiclePurchaseDetail.Site)
            DbCommandWrapper.AddInParameter("@StockNumberName", DbType.AnsiString, vehiclePurchaseDetail.StockNumberName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vehiclePurchaseDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vehiclePurchaseDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@VehiclePurchaseHeaderID", DbType.Int32, Me.GetRefObject(vehiclePurchaseDetail.VehiclePurchaseHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VehiclePurchaseDetail

            Dim vehiclePurchaseDetail As VehiclePurchaseDetail = New VehiclePurchaseDetail

            vehiclePurchaseDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BUCode")) Then vehiclePurchaseDetail.BUCode = dr("BUCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BUName")) Then vehiclePurchaseDetail.BUName = dr("BUName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CloseLine")) Then vehiclePurchaseDetail.CloseLine = CType(dr("CloseLine"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("CloseLineName")) Then vehiclePurchaseDetail.CloseLineName = dr("CloseLineName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CloseReason")) Then vehiclePurchaseDetail.CloseReason = dr("CloseReason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Completed")) Then vehiclePurchaseDetail.Completed = CType(dr("Completed"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("CompletedName")) Then vehiclePurchaseDetail.CompletedName = dr("CompletedName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductDescription")) Then vehiclePurchaseDetail.ProductDescription = dr("ProductDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductName")) Then vehiclePurchaseDetail.ProductName = dr("ProductName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductVariantName")) Then vehiclePurchaseDetail.ProductVariantName = dr("ProductVariantName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PODetail")) Then vehiclePurchaseDetail.PODetail = dr("PODetail").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("POName")) Then vehiclePurchaseDetail.POName = dr("POName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PRDetailName")) Then vehiclePurchaseDetail.PRDetailName = dr("PRDetailName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PurchaseUnitName")) Then vehiclePurchaseDetail.PurchaseUnitName = dr("PurchaseUnitName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("QtyOrder")) Then vehiclePurchaseDetail.QtyOrder = CType(dr("QtyOrder"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("QtyReceipt")) Then vehiclePurchaseDetail.QtyReceipt = CType(dr("QtyReceipt"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("QtyReturn")) Then vehiclePurchaseDetail.QtyReturn = CType(dr("QtyReturn"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("RecallProduct")) Then vehiclePurchaseDetail.RecallProduct = CType(dr("RecallProduct"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("RecallProductName")) Then vehiclePurchaseDetail.RecallProductName = dr("RecallProductName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SODetailName")) Then vehiclePurchaseDetail.SODetailName = dr("SODetailName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ScheduledShippingDate")) Then vehiclePurchaseDetail.ScheduledShippingDate = CType(dr("ScheduledShippingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ServicePartsAndMaterial")) Then vehiclePurchaseDetail.ServicePartsAndMaterial = dr("ServicePartsAndMaterial").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ShippingDate")) Then vehiclePurchaseDetail.ShippingDate = CType(dr("ShippingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Site")) Then vehiclePurchaseDetail.Site = dr("Site").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StockNumberName")) Then vehiclePurchaseDetail.StockNumberName = dr("StockNumberName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vehiclePurchaseDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vehiclePurchaseDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vehiclePurchaseDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then vehiclePurchaseDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vehiclePurchaseDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VehiclePurchaseHeaderID")) Then
                vehiclePurchaseDetail.VehiclePurchaseHeader = New VehiclePurchaseHeader(CType(dr("VehiclePurchaseHeaderID"), Integer))
            End If

            Return vehiclePurchaseDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(VehiclePurchaseDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VehiclePurchaseDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VehiclePurchaseDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


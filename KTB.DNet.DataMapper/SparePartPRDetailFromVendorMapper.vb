
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : SparePartPRDetailFromVendor Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 22/03/2018 - 15:16:03
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

    Public Class SparePartPRDetailFromVendorMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartPRDetailFromVendor"
        Private m_UpdateStatement As String = "up_UpdateSparePartPRDetailFromVendor"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartPRDetailFromVendor"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartPRDetailFromVendorList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartPRDetailFromVendor"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartPRDetailFromVendor As SparePartPRDetailFromVendor = Nothing
            While dr.Read

                sparePartPRDetailFromVendor = Me.CreateObject(dr)

            End While

            Return sparePartPRDetailFromVendor

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartPRDetailFromVendorList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartPRDetailFromVendor As SparePartPRDetailFromVendor = Me.CreateObject(dr)
                sparePartPRDetailFromVendorList.Add(sparePartPRDetailFromVendor)
            End While

            Return sparePartPRDetailFromVendorList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPRDetailFromVendor As SparePartPRDetailFromVendor = CType(obj, SparePartPRDetailFromVendor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPRDetailFromVendor.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPRDetailFromVendor As SparePartPRDetailFromVendor = CType(obj, SparePartPRDetailFromVendor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PRDetailNumber", DbType.AnsiString, sparePartPRDetailFromVendor.PRDetailNumber)
            DbCommandWrapper.AddInParameter("@PRNumber", DbType.AnsiString, sparePartPRDetailFromVendor.PRNumber)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, sparePartPRDetailFromVendor.Owner)
            DbCommandWrapper.AddInParameter("@BaseReceivedQuantity", DbType.Double, sparePartPRDetailFromVendor.BaseReceivedQuantity)
            DbCommandWrapper.AddInParameter("@BatchNumber", DbType.AnsiString, sparePartPRDetailFromVendor.BatchNumber)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, sparePartPRDetailFromVendor.DealerCode)
            DbCommandWrapper.AddInParameter("@ChassisModel", DbType.AnsiString, sparePartPRDetailFromVendor.ChassisModel)
            DbCommandWrapper.AddInParameter("@ChassisNumberRegister", DbType.AnsiString, sparePartPRDetailFromVendor.ChassisNumberRegister)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1Amount", DbType.Currency, sparePartPRDetailFromVendor.ConsumptionTax1Amount)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1", DbType.AnsiString, sparePartPRDetailFromVendor.ConsumptionTax1)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2Amount", DbType.Currency, sparePartPRDetailFromVendor.ConsumptionTax2Amount)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2", DbType.AnsiString, sparePartPRDetailFromVendor.ConsumptionTax2)
            DbCommandWrapper.AddInParameter("@DiscountAmount", DbType.Currency, sparePartPRDetailFromVendor.DiscountAmount)
            DbCommandWrapper.AddInParameter("@EngineNumber", DbType.AnsiString, sparePartPRDetailFromVendor.EngineNumber)
            DbCommandWrapper.AddInParameter("@EventData", DbType.AnsiString, sparePartPRDetailFromVendor.EventData)
            DbCommandWrapper.AddInParameter("@InventoryUnit", DbType.AnsiString, sparePartPRDetailFromVendor.InventoryUnit)
            DbCommandWrapper.AddInParameter("@KeyNumber", DbType.AnsiString, sparePartPRDetailFromVendor.KeyNumber)
            DbCommandWrapper.AddInParameter("@LandedCost", DbType.Currency, sparePartPRDetailFromVendor.LandedCost)
            DbCommandWrapper.AddInParameter("@Location", DbType.AnsiString, sparePartPRDetailFromVendor.Location)
            DbCommandWrapper.AddInParameter("@ProductDescription", DbType.AnsiString, sparePartPRDetailFromVendor.ProductDescription)
            DbCommandWrapper.AddInParameter("@Product", DbType.AnsiString, sparePartPRDetailFromVendor.Product)
            DbCommandWrapper.AddInParameter("@ProductVolume", DbType.Double, sparePartPRDetailFromVendor.ProductVolume)
            DbCommandWrapper.AddInParameter("@ProductWeight", DbType.Double, sparePartPRDetailFromVendor.ProductWeight)
            DbCommandWrapper.AddInParameter("@PurchaseUnit", DbType.AnsiString, sparePartPRDetailFromVendor.PurchaseUnit)
            DbCommandWrapper.AddInParameter("@ReceivedQuantity", DbType.Double, sparePartPRDetailFromVendor.ReceivedQuantity)
            DbCommandWrapper.AddInParameter("@ReferenceNumber", DbType.AnsiString, sparePartPRDetailFromVendor.ReferenceNumber)
            DbCommandWrapper.AddInParameter("@ReturnPRDetail", DbType.AnsiString, sparePartPRDetailFromVendor.ReturnPRDetail)
            DbCommandWrapper.AddInParameter("@ServicePartsAndMaterial", DbType.AnsiString, sparePartPRDetailFromVendor.ServicePartsAndMaterial)
            DbCommandWrapper.AddInParameter("@Site", DbType.AnsiString, sparePartPRDetailFromVendor.Site)
            DbCommandWrapper.AddInParameter("@StockNumber", DbType.AnsiString, sparePartPRDetailFromVendor.StockNumber)
            DbCommandWrapper.AddInParameter("@TitleRegistrationFee", DbType.Currency, sparePartPRDetailFromVendor.TitleRegistrationFee)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, sparePartPRDetailFromVendor.TotalAmount)
            DbCommandWrapper.AddInParameter("@TotalBaseAmount", DbType.Currency, sparePartPRDetailFromVendor.TotalBaseAmount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTaxAmount", DbType.Currency, sparePartPRDetailFromVendor.TotalConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TotalVolume", DbType.Double, sparePartPRDetailFromVendor.TotalVolume)
            DbCommandWrapper.AddInParameter("@TotalWeight", DbType.Double, sparePartPRDetailFromVendor.TotalWeight)
            DbCommandWrapper.AddInParameter("@TransactionAmount", DbType.Currency, sparePartPRDetailFromVendor.TransactionAmount)
            DbCommandWrapper.AddInParameter("@UnitCost", DbType.Currency, sparePartPRDetailFromVendor.UnitCost)
            DbCommandWrapper.AddInParameter("@Warehouse", DbType.AnsiString, sparePartPRDetailFromVendor.Warehouse)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPRDetailFromVendor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartPRDetailFromVendor.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartPRID", DbType.Int32, Me.GetRefObject(sparePartPRDetailFromVendor.SparePartPRFromVendor))

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

            Dim sparePartPRDetailFromVendor As SparePartPRDetailFromVendor = CType(obj, SparePartPRDetailFromVendor)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPRDetailFromVendor.ID)
            DbCommandWrapper.AddInParameter("@PRDetailNumber", DbType.AnsiString, sparePartPRDetailFromVendor.PRDetailNumber)
            DbCommandWrapper.AddInParameter("@PRNumber", DbType.AnsiString, sparePartPRDetailFromVendor.PRNumber)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, sparePartPRDetailFromVendor.Owner)
            DbCommandWrapper.AddInParameter("@BaseReceivedQuantity", DbType.Double, sparePartPRDetailFromVendor.BaseReceivedQuantity)
            DbCommandWrapper.AddInParameter("@BatchNumber", DbType.AnsiString, sparePartPRDetailFromVendor.BatchNumber)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, sparePartPRDetailFromVendor.DealerCode)
            DbCommandWrapper.AddInParameter("@ChassisModel", DbType.AnsiString, sparePartPRDetailFromVendor.ChassisModel)
            DbCommandWrapper.AddInParameter("@ChassisNumberRegister", DbType.AnsiString, sparePartPRDetailFromVendor.ChassisNumberRegister)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1Amount", DbType.Currency, sparePartPRDetailFromVendor.ConsumptionTax1Amount)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1", DbType.AnsiString, sparePartPRDetailFromVendor.ConsumptionTax1)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2Amount", DbType.Currency, sparePartPRDetailFromVendor.ConsumptionTax2Amount)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2", DbType.AnsiString, sparePartPRDetailFromVendor.ConsumptionTax2)
            DbCommandWrapper.AddInParameter("@DiscountAmount", DbType.Currency, sparePartPRDetailFromVendor.DiscountAmount)
            DbCommandWrapper.AddInParameter("@EngineNumber", DbType.AnsiString, sparePartPRDetailFromVendor.EngineNumber)
            DbCommandWrapper.AddInParameter("@EventData", DbType.AnsiString, sparePartPRDetailFromVendor.EventData)
            DbCommandWrapper.AddInParameter("@InventoryUnit", DbType.AnsiString, sparePartPRDetailFromVendor.InventoryUnit)
            DbCommandWrapper.AddInParameter("@KeyNumber", DbType.AnsiString, sparePartPRDetailFromVendor.KeyNumber)
            DbCommandWrapper.AddInParameter("@LandedCost", DbType.Currency, sparePartPRDetailFromVendor.LandedCost)
            DbCommandWrapper.AddInParameter("@Location", DbType.AnsiString, sparePartPRDetailFromVendor.Location)
            DbCommandWrapper.AddInParameter("@ProductDescription", DbType.AnsiString, sparePartPRDetailFromVendor.ProductDescription)
            DbCommandWrapper.AddInParameter("@Product", DbType.AnsiString, sparePartPRDetailFromVendor.Product)
            DbCommandWrapper.AddInParameter("@ProductVolume", DbType.Double, sparePartPRDetailFromVendor.ProductVolume)
            DbCommandWrapper.AddInParameter("@ProductWeight", DbType.Double, sparePartPRDetailFromVendor.ProductWeight)
            DbCommandWrapper.AddInParameter("@PurchaseUnit", DbType.AnsiString, sparePartPRDetailFromVendor.PurchaseUnit)
            DbCommandWrapper.AddInParameter("@ReceivedQuantity", DbType.Double, sparePartPRDetailFromVendor.ReceivedQuantity)
            DbCommandWrapper.AddInParameter("@ReferenceNumber", DbType.AnsiString, sparePartPRDetailFromVendor.ReferenceNumber)
            DbCommandWrapper.AddInParameter("@ReturnPRDetail", DbType.AnsiString, sparePartPRDetailFromVendor.ReturnPRDetail)
            DbCommandWrapper.AddInParameter("@ServicePartsAndMaterial", DbType.AnsiString, sparePartPRDetailFromVendor.ServicePartsAndMaterial)
            DbCommandWrapper.AddInParameter("@Site", DbType.AnsiString, sparePartPRDetailFromVendor.Site)
            DbCommandWrapper.AddInParameter("@StockNumber", DbType.AnsiString, sparePartPRDetailFromVendor.StockNumber)
            DbCommandWrapper.AddInParameter("@TitleRegistrationFee", DbType.Currency, sparePartPRDetailFromVendor.TitleRegistrationFee)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, sparePartPRDetailFromVendor.TotalAmount)
            DbCommandWrapper.AddInParameter("@TotalBaseAmount", DbType.Currency, sparePartPRDetailFromVendor.TotalBaseAmount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTaxAmount", DbType.Currency, sparePartPRDetailFromVendor.TotalConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TotalVolume", DbType.Double, sparePartPRDetailFromVendor.TotalVolume)
            DbCommandWrapper.AddInParameter("@TotalWeight", DbType.Double, sparePartPRDetailFromVendor.TotalWeight)
            DbCommandWrapper.AddInParameter("@TransactionAmount", DbType.Currency, sparePartPRDetailFromVendor.TransactionAmount)
            DbCommandWrapper.AddInParameter("@UnitCost", DbType.Currency, sparePartPRDetailFromVendor.UnitCost)
            DbCommandWrapper.AddInParameter("@Warehouse", DbType.AnsiString, sparePartPRDetailFromVendor.Warehouse)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPRDetailFromVendor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartPRDetailFromVendor.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartPRID", DbType.Int32, Me.GetRefObject(sparePartPRDetailFromVendor.SparePartPRFromVendor))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartPRDetailFromVendor

            Dim sparePartPRDetailFromVendor As SparePartPRDetailFromVendor = New SparePartPRDetailFromVendor

            sparePartPRDetailFromVendor.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PRDetailNumber")) Then sparePartPRDetailFromVendor.PRDetailNumber = dr("PRDetailNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PRNumber")) Then sparePartPRDetailFromVendor.PRNumber = dr("PRNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then sparePartPRDetailFromVendor.Owner = dr("Owner").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BaseReceivedQuantity")) Then sparePartPRDetailFromVendor.BaseReceivedQuantity = CType(dr("BaseReceivedQuantity"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("BatchNumber")) Then sparePartPRDetailFromVendor.BatchNumber = dr("BatchNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then sparePartPRDetailFromVendor.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisModel")) Then sparePartPRDetailFromVendor.ChassisModel = dr("ChassisModel").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumberRegister")) Then sparePartPRDetailFromVendor.ChassisNumberRegister = dr("ChassisNumberRegister").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax1Amount")) Then sparePartPRDetailFromVendor.ConsumptionTax1Amount = CType(dr("ConsumptionTax1Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax1")) Then sparePartPRDetailFromVendor.ConsumptionTax1 = dr("ConsumptionTax1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax2Amount")) Then sparePartPRDetailFromVendor.ConsumptionTax2Amount = CType(dr("ConsumptionTax2Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax2")) Then sparePartPRDetailFromVendor.ConsumptionTax2 = dr("ConsumptionTax2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountAmount")) Then sparePartPRDetailFromVendor.DiscountAmount = CType(dr("DiscountAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("EngineNumber")) Then sparePartPRDetailFromVendor.EngineNumber = dr("EngineNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EventData")) Then sparePartPRDetailFromVendor.EventData = dr("EventData").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InventoryUnit")) Then sparePartPRDetailFromVendor.InventoryUnit = dr("InventoryUnit").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KeyNumber")) Then sparePartPRDetailFromVendor.KeyNumber = dr("KeyNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LandedCost")) Then sparePartPRDetailFromVendor.LandedCost = CType(dr("LandedCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Location")) Then sparePartPRDetailFromVendor.Location = dr("Location").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductDescription")) Then sparePartPRDetailFromVendor.ProductDescription = dr("ProductDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Product")) Then sparePartPRDetailFromVendor.Product = dr("Product").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductVolume")) Then sparePartPRDetailFromVendor.ProductVolume = CType(dr("ProductVolume"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductWeight")) Then sparePartPRDetailFromVendor.ProductWeight = CType(dr("ProductWeight"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("PurchaseUnit")) Then sparePartPRDetailFromVendor.PurchaseUnit = dr("PurchaseUnit").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReceivedQuantity")) Then sparePartPRDetailFromVendor.ReceivedQuantity = CType(dr("ReceivedQuantity"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceNumber")) Then sparePartPRDetailFromVendor.ReferenceNumber = dr("ReferenceNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReturnPRDetail")) Then sparePartPRDetailFromVendor.ReturnPRDetail = dr("ReturnPRDetail").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServicePartsAndMaterial")) Then sparePartPRDetailFromVendor.ServicePartsAndMaterial = dr("ServicePartsAndMaterial").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Site")) Then sparePartPRDetailFromVendor.Site = dr("Site").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StockNumber")) Then sparePartPRDetailFromVendor.StockNumber = dr("StockNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TitleRegistrationFee")) Then sparePartPRDetailFromVendor.TitleRegistrationFee = CType(dr("TitleRegistrationFee"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then sparePartPRDetailFromVendor.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalBaseAmount")) Then sparePartPRDetailFromVendor.TotalBaseAmount = CType(dr("TotalBaseAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalConsumptionTaxAmount")) Then sparePartPRDetailFromVendor.TotalConsumptionTaxAmount = CType(dr("TotalConsumptionTaxAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalVolume")) Then sparePartPRDetailFromVendor.TotalVolume = CType(dr("TotalVolume"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalWeight")) Then sparePartPRDetailFromVendor.TotalWeight = CType(dr("TotalWeight"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionAmount")) Then sparePartPRDetailFromVendor.TransactionAmount = CType(dr("TransactionAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("UnitCost")) Then sparePartPRDetailFromVendor.UnitCost = CType(dr("UnitCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Warehouse")) Then sparePartPRDetailFromVendor.Warehouse = dr("Warehouse").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartPRDetailFromVendor.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartPRDetailFromVendor.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartPRDetailFromVendor.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartPRDetailFromVendor.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartPRDetailFromVendor.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartPRID")) Then
                sparePartPRDetailFromVendor.SparePartPRFromVendor = New SparePartPRFromVendor(CType(dr("SparePartPRID"), Integer))
            End If

            Return sparePartPRDetailFromVendor

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartPRDetailFromVendor) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartPRDetailFromVendor), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartPRDetailFromVendor).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
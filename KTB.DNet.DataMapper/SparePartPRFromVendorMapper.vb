
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : SparePartPRFromVendor Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 22/03/2018 - 15:14:53
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

    Public Class SparePartPRFromVendorMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartPRFromVendor"
        Private m_UpdateStatement As String = "up_UpdateSparePartPRFromVendor"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartPRFromVendor"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartPRFromVendorList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartPRFromVendor"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartPRFromVendor As SparePartPRFromVendor = Nothing
            While dr.Read

                sparePartPRFromVendor = Me.CreateObject(dr)

            End While

            Return sparePartPRFromVendor

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartPRFromVendorList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartPRFromVendor As SparePartPRFromVendor = Me.CreateObject(dr)
                sparePartPRFromVendorList.Add(sparePartPRFromVendor)
            End While

            Return sparePartPRFromVendorList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPRFromVendor As SparePartPRFromVendor = CType(obj, SparePartPRFromVendor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPRFromVendor.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPRFromVendor As SparePartPRFromVendor = CType(obj, SparePartPRFromVendor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PRNumber", DbType.String, sparePartPRFromVendor.PRNumber)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, sparePartPRFromVendor.PONumber)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, sparePartPRFromVendor.Owner)
            DbCommandWrapper.AddInParameter("@APVoucherNumber", DbType.AnsiString, sparePartPRFromVendor.APVoucherNumber)
            DbCommandWrapper.AddInParameter("@AssignLandedCost", DbType.Boolean, sparePartPRFromVendor.AssignLandedCost)
            DbCommandWrapper.AddInParameter("@AutoInvoiced", DbType.Boolean, sparePartPRFromVendor.AutoInvoiced)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, sparePartPRFromVendor.DealerCode)
            DbCommandWrapper.AddInParameter("@DeliveryOrderDate", DbType.DateTime, sparePartPRFromVendor.DeliveryOrderDate)
            DbCommandWrapper.AddInParameter("@DeliveryOrderNumber", DbType.AnsiString, sparePartPRFromVendor.DeliveryOrderNumber)
            DbCommandWrapper.AddInParameter("@EventData", DbType.AnsiString, sparePartPRFromVendor.EventData)
            DbCommandWrapper.AddInParameter("@EventData2", DbType.AnsiString, sparePartPRFromVendor.EventData2)
            DbCommandWrapper.AddInParameter("@GrandTotal", DbType.Currency, sparePartPRFromVendor.GrandTotal)
            DbCommandWrapper.AddInParameter("@Handling", DbType.Int16, sparePartPRFromVendor.Handling)
            DbCommandWrapper.AddInParameter("@LoadData", DbType.Boolean, sparePartPRFromVendor.LoadData)
            DbCommandWrapper.AddInParameter("@PackingSlipDate", DbType.DateTime, sparePartPRFromVendor.PackingSlipDate)
            DbCommandWrapper.AddInParameter("@PackingSlipNumber", DbType.AnsiString, sparePartPRFromVendor.PackingSlipNumber)
            DbCommandWrapper.AddInParameter("@PRReferenceRequired", DbType.Boolean, sparePartPRFromVendor.PRReferenceRequired)
            DbCommandWrapper.AddInParameter("@ReturnPRNumber", DbType.AnsiString, sparePartPRFromVendor.ReturnPRNumber)
            DbCommandWrapper.AddInParameter("@State", DbType.Int16, sparePartPRFromVendor.State)
            DbCommandWrapper.AddInParameter("@TotalBaseAmount", DbType.Currency, sparePartPRFromVendor.TotalBaseAmount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTax1Amount", DbType.Currency, sparePartPRFromVendor.TotalConsumptionTax1Amount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTax2Amount", DbType.Currency, sparePartPRFromVendor.TotalConsumptionTax2Amount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTaxAmount", DbType.Currency, sparePartPRFromVendor.TotalConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TotalTitleRegistrationFree", DbType.Currency, sparePartPRFromVendor.TotalTitleRegistrationFree)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, sparePartPRFromVendor.TransactionDate)
            DbCommandWrapper.AddInParameter("@TransferOrderRequestingNumber", DbType.AnsiString, sparePartPRFromVendor.TransferOrderRequestingNumber)
            DbCommandWrapper.AddInParameter("@Type", DbType.Int16, sparePartPRFromVendor.Type)
            DbCommandWrapper.AddInParameter("@VendorDescription", DbType.AnsiString, sparePartPRFromVendor.VendorDescription)
            DbCommandWrapper.AddInParameter("@Vendor", DbType.AnsiString, sparePartPRFromVendor.Vendor)
            DbCommandWrapper.AddInParameter("@VendorInvoiceNumber", DbType.AnsiString, sparePartPRFromVendor.VendorInvoiceNumber)
            DbCommandWrapper.AddInParameter("@WONumber", DbType.AnsiString, sparePartPRFromVendor.WONumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPRFromVendor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartPRFromVendor.LastUpdateBy)
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

            Dim sparePartPRFromVendor As SparePartPRFromVendor = CType(obj, SparePartPRFromVendor)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPRFromVendor.ID)
            DbCommandWrapper.AddInParameter("@PRNumber", DbType.String, sparePartPRFromVendor.PRNumber)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, sparePartPRFromVendor.PONumber)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, sparePartPRFromVendor.Owner)
            DbCommandWrapper.AddInParameter("@APVoucherNumber", DbType.AnsiString, sparePartPRFromVendor.APVoucherNumber)
            DbCommandWrapper.AddInParameter("@AssignLandedCost", DbType.Boolean, sparePartPRFromVendor.AssignLandedCost)
            DbCommandWrapper.AddInParameter("@AutoInvoiced", DbType.Boolean, sparePartPRFromVendor.AutoInvoiced)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, sparePartPRFromVendor.DealerCode)
            DbCommandWrapper.AddInParameter("@DeliveryOrderDate", DbType.DateTime, sparePartPRFromVendor.DeliveryOrderDate)
            DbCommandWrapper.AddInParameter("@DeliveryOrderNumber", DbType.AnsiString, sparePartPRFromVendor.DeliveryOrderNumber)
            DbCommandWrapper.AddInParameter("@EventData", DbType.AnsiString, sparePartPRFromVendor.EventData)
            DbCommandWrapper.AddInParameter("@EventData2", DbType.AnsiString, sparePartPRFromVendor.EventData2)
            DbCommandWrapper.AddInParameter("@GrandTotal", DbType.Currency, sparePartPRFromVendor.GrandTotal)
            DbCommandWrapper.AddInParameter("@Handling", DbType.Int16, sparePartPRFromVendor.Handling)
            DbCommandWrapper.AddInParameter("@LoadData", DbType.Boolean, sparePartPRFromVendor.LoadData)
            DbCommandWrapper.AddInParameter("@PackingSlipDate", DbType.DateTime, sparePartPRFromVendor.PackingSlipDate)
            DbCommandWrapper.AddInParameter("@PackingSlipNumber", DbType.AnsiString, sparePartPRFromVendor.PackingSlipNumber)
            DbCommandWrapper.AddInParameter("@PRReferenceRequired", DbType.Boolean, sparePartPRFromVendor.PRReferenceRequired)
            DbCommandWrapper.AddInParameter("@ReturnPRNumber", DbType.AnsiString, sparePartPRFromVendor.ReturnPRNumber)
            DbCommandWrapper.AddInParameter("@State", DbType.Int16, sparePartPRFromVendor.State)
            DbCommandWrapper.AddInParameter("@TotalBaseAmount", DbType.Currency, sparePartPRFromVendor.TotalBaseAmount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTax1Amount", DbType.Currency, sparePartPRFromVendor.TotalConsumptionTax1Amount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTax2Amount", DbType.Currency, sparePartPRFromVendor.TotalConsumptionTax2Amount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTaxAmount", DbType.Currency, sparePartPRFromVendor.TotalConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TotalTitleRegistrationFree", DbType.Currency, sparePartPRFromVendor.TotalTitleRegistrationFree)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, sparePartPRFromVendor.TransactionDate)
            DbCommandWrapper.AddInParameter("@TransferOrderRequestingNumber", DbType.AnsiString, sparePartPRFromVendor.TransferOrderRequestingNumber)
            DbCommandWrapper.AddInParameter("@Type", DbType.Int16, sparePartPRFromVendor.Type)
            DbCommandWrapper.AddInParameter("@VendorDescription", DbType.AnsiString, sparePartPRFromVendor.VendorDescription)
            DbCommandWrapper.AddInParameter("@Vendor", DbType.AnsiString, sparePartPRFromVendor.Vendor)
            DbCommandWrapper.AddInParameter("@VendorInvoiceNumber", DbType.AnsiString, sparePartPRFromVendor.VendorInvoiceNumber)
            DbCommandWrapper.AddInParameter("@WONumber", DbType.AnsiString, sparePartPRFromVendor.WONumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPRFromVendor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartPRFromVendor.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartPRFromVendor

            Dim sparePartPRFromVendor As SparePartPRFromVendor = New SparePartPRFromVendor

            sparePartPRFromVendor.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PRNumber")) Then sparePartPRFromVendor.PRNumber = dr("PRNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then sparePartPRFromVendor.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then sparePartPRFromVendor.Owner = dr("Owner").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("APVoucherNumber")) Then sparePartPRFromVendor.APVoucherNumber = dr("APVoucherNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AssignLandedCost")) Then sparePartPRFromVendor.AssignLandedCost = CType(dr("AssignLandedCost"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("AutoInvoiced")) Then sparePartPRFromVendor.AutoInvoiced = CType(dr("AutoInvoiced"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then sparePartPRFromVendor.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryOrderDate")) Then sparePartPRFromVendor.DeliveryOrderDate = CType(dr("DeliveryOrderDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryOrderNumber")) Then sparePartPRFromVendor.DeliveryOrderNumber = dr("DeliveryOrderNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EventData")) Then sparePartPRFromVendor.EventData = dr("EventData").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EventData2")) Then sparePartPRFromVendor.EventData2 = dr("EventData2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GrandTotal")) Then sparePartPRFromVendor.GrandTotal = CType(dr("GrandTotal"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Handling")) Then sparePartPRFromVendor.Handling = CType(dr("Handling"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LoadData")) Then sparePartPRFromVendor.LoadData = CType(dr("LoadData"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("PackingSlipDate")) Then sparePartPRFromVendor.PackingSlipDate = CType(dr("PackingSlipDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PackingSlipNumber")) Then sparePartPRFromVendor.PackingSlipNumber = dr("PackingSlipNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PRReferenceRequired")) Then sparePartPRFromVendor.PRReferenceRequired = CType(dr("PRReferenceRequired"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("ReturnPRNumber")) Then sparePartPRFromVendor.ReturnPRNumber = dr("ReturnPRNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("State")) Then sparePartPRFromVendor.State = CType(dr("State"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalBaseAmount")) Then sparePartPRFromVendor.TotalBaseAmount = CType(dr("TotalBaseAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalConsumptionTax1Amount")) Then sparePartPRFromVendor.TotalConsumptionTax1Amount = CType(dr("TotalConsumptionTax1Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalConsumptionTax2Amount")) Then sparePartPRFromVendor.TotalConsumptionTax2Amount = CType(dr("TotalConsumptionTax2Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalConsumptionTaxAmount")) Then sparePartPRFromVendor.TotalConsumptionTaxAmount = CType(dr("TotalConsumptionTaxAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalTitleRegistrationFree")) Then sparePartPRFromVendor.TotalTitleRegistrationFree = CType(dr("TotalTitleRegistrationFree"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionDate")) Then sparePartPRFromVendor.TransactionDate = CType(dr("TransactionDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TransferOrderRequestingNumber")) Then sparePartPRFromVendor.TransferOrderRequestingNumber = dr("TransferOrderRequestingNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Type")) Then sparePartPRFromVendor.Type = CType(dr("Type"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("VendorDescription")) Then sparePartPRFromVendor.VendorDescription = dr("VendorDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Vendor")) Then sparePartPRFromVendor.Vendor = dr("Vendor").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VendorInvoiceNumber")) Then sparePartPRFromVendor.VendorInvoiceNumber = dr("VendorInvoiceNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WONumber")) Then sparePartPRFromVendor.WONumber = dr("WONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartPRFromVendor.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartPRFromVendor.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartPRFromVendor.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartPRFromVendor.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartPRFromVendor.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sparePartPRFromVendor

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartPRFromVendor) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartPRFromVendor), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartPRFromVendor).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

#Region "Summary"
'// ===========================================================================
'// AUTHOR        : MItrais Team
'// PURPOSE       : POOtherVendor Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 22/03/2018 - 16:12:16
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

    Public Class POOtherVendorMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPOOtherVendor"
        Private m_UpdateStatement As String = "up_UpdatePOOtherVendor"
        Private m_RetrieveStatement As String = "up_RetrievePOOtherVendor"
        Private m_RetrieveListStatement As String = "up_RetrievePOOtherVendorList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePOOtherVendor"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pOOtherVendor As POOtherVendor = Nothing
            While dr.Read

                pOOtherVendor = Me.CreateObject(dr)

            End While

            Return pOOtherVendor

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pOOtherVendorList As ArrayList = New ArrayList

            While dr.Read
                Dim pOOtherVendor As POOtherVendor = Me.CreateObject(dr)
                pOOtherVendorList.Add(pOOtherVendor)
            End While

            Return pOOtherVendorList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pOOtherVendor As POOtherVendor = CType(obj, POOtherVendor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pOOtherVendor.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pOOtherVendor As POOtherVendor = CType(obj, POOtherVendor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, pOOtherVendor.Owner)
            DbCommandWrapper.AddInParameter("@Address1", DbType.AnsiString, pOOtherVendor.Address1)
            DbCommandWrapper.AddInParameter("@Address2", DbType.AnsiString, pOOtherVendor.Address2)
            DbCommandWrapper.AddInParameter("@Address3", DbType.AnsiString, pOOtherVendor.Address3)
            DbCommandWrapper.AddInParameter("@AllocationPeriod", DbType.AnsiString, pOOtherVendor.AllocationPeriod)
            DbCommandWrapper.AddInParameter("@Balance", DbType.Currency, pOOtherVendor.Balance)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, pOOtherVendor.DealerCode)
            DbCommandWrapper.AddInParameter("@City", DbType.AnsiString, pOOtherVendor.City)
            DbCommandWrapper.AddInParameter("@CloseRespon", DbType.AnsiString, pOOtherVendor.CloseRespon)
            DbCommandWrapper.AddInParameter("@Country", DbType.AnsiString, pOOtherVendor.Country)
            DbCommandWrapper.AddInParameter("@DeliveryMethod", DbType.Int16, pOOtherVendor.DeliveryMethod)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, pOOtherVendor.Description)
            DbCommandWrapper.AddInParameter("@DownPayment", DbType.Currency, pOOtherVendor.DownPayment)
            DbCommandWrapper.AddInParameter("@DownPaymentAmountPaid", DbType.Currency, pOOtherVendor.DownPaymentAmountPaid)
            DbCommandWrapper.AddInParameter("@DownPaymentIsPaid", DbType.Boolean, pOOtherVendor.DownPaymentIsPaid)
            DbCommandWrapper.AddInParameter("@EventDate", DbType.AnsiString, pOOtherVendor.EventDate)
            DbCommandWrapper.AddInParameter("@ExternalDocNo", DbType.AnsiString, pOOtherVendor.ExternalDocNo)
            DbCommandWrapper.AddInParameter("@FormSource", DbType.Int16, pOOtherVendor.FormSource)
            DbCommandWrapper.AddInParameter("@GrandTotal", DbType.Currency, pOOtherVendor.GrandTotal)
            DbCommandWrapper.AddInParameter("@PaymentGroup", DbType.Int16, pOOtherVendor.PaymentGroup)
            DbCommandWrapper.AddInParameter("@PersonInCharge", DbType.AnsiString, pOOtherVendor.PersonInCharge)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.AnsiString, pOOtherVendor.PostalCode)
            DbCommandWrapper.AddInParameter("@Priority", DbType.Int16, pOOtherVendor.Priority)
            DbCommandWrapper.AddInParameter("@Province", DbType.AnsiString, pOOtherVendor.Province)
            DbCommandWrapper.AddInParameter("@PRPOType", DbType.AnsiString, pOOtherVendor.PRPOType)
            DbCommandWrapper.AddInParameter("@PurchaseOrderNo", DbType.AnsiString, pOOtherVendor.PurchaseOrderNo)
            DbCommandWrapper.AddInParameter("@SONo", DbType.AnsiString, pOOtherVendor.SONo)
            DbCommandWrapper.AddInParameter("@Site", DbType.AnsiString, pOOtherVendor.Site)
            DbCommandWrapper.AddInParameter("@State", DbType.Int16, pOOtherVendor.State)
            DbCommandWrapper.AddInParameter("@StockReferenceNo", DbType.AnsiString, pOOtherVendor.StockReferenceNo)
            DbCommandWrapper.AddInParameter("@Taxable", DbType.Int16, pOOtherVendor.Taxable)
            DbCommandWrapper.AddInParameter("@TermsOfPayment", DbType.AnsiString, pOOtherVendor.TermsOfPayment)
            DbCommandWrapper.AddInParameter("@TotalAmountBeforeDiscount", DbType.Currency, pOOtherVendor.TotalAmountBeforeDiscount)
            DbCommandWrapper.AddInParameter("@TotalBaseAmount", DbType.Currency, pOOtherVendor.TotalBaseAmount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTaxAmount", DbType.Currency, pOOtherVendor.TotalConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TotalDiscountAmount", DbType.Currency, pOOtherVendor.TotalDiscountAmount)
            DbCommandWrapper.AddInParameter("@TotalTitleRegistrationFee", DbType.Currency, pOOtherVendor.TotalTitleRegistrationFee)
            DbCommandWrapper.AddInParameter("@PurchaseOrderDate", DbType.DateTime, pOOtherVendor.PurchaseOrderDate)
            DbCommandWrapper.AddInParameter("@VendorDescription", DbType.AnsiString, pOOtherVendor.VendorDescription)
            DbCommandWrapper.AddInParameter("@Vendor", DbType.AnsiString, pOOtherVendor.Vendor)
            DbCommandWrapper.AddInParameter("@Warehouse", DbType.AnsiString, pOOtherVendor.Warehouse)
            DbCommandWrapper.AddInParameter("@WONo", DbType.AnsiString, pOOtherVendor.WONo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pOOtherVendor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pOOtherVendor.LastUpdateBy)
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

            Dim pOOtherVendor As POOtherVendor = CType(obj, POOtherVendor)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pOOtherVendor.ID)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, pOOtherVendor.Owner)
            DbCommandWrapper.AddInParameter("@Address1", DbType.AnsiString, pOOtherVendor.Address1)
            DbCommandWrapper.AddInParameter("@Address2", DbType.AnsiString, pOOtherVendor.Address2)
            DbCommandWrapper.AddInParameter("@Address3", DbType.AnsiString, pOOtherVendor.Address3)
            DbCommandWrapper.AddInParameter("@AllocationPeriod", DbType.AnsiString, pOOtherVendor.AllocationPeriod)
            DbCommandWrapper.AddInParameter("@Balance", DbType.Currency, pOOtherVendor.Balance)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, pOOtherVendor.DealerCode)
            DbCommandWrapper.AddInParameter("@City", DbType.AnsiString, pOOtherVendor.City)
            DbCommandWrapper.AddInParameter("@CloseRespon", DbType.AnsiString, pOOtherVendor.CloseRespon)
            DbCommandWrapper.AddInParameter("@Country", DbType.AnsiString, pOOtherVendor.Country)
            DbCommandWrapper.AddInParameter("@DeliveryMethod", DbType.Int16, pOOtherVendor.DeliveryMethod)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, pOOtherVendor.Description)
            DbCommandWrapper.AddInParameter("@DownPayment", DbType.Currency, pOOtherVendor.DownPayment)
            DbCommandWrapper.AddInParameter("@DownPaymentAmountPaid", DbType.Currency, pOOtherVendor.DownPaymentAmountPaid)
            DbCommandWrapper.AddInParameter("@DownPaymentIsPaid", DbType.Boolean, pOOtherVendor.DownPaymentIsPaid)
            DbCommandWrapper.AddInParameter("@EventDate", DbType.AnsiString, pOOtherVendor.EventDate)
            DbCommandWrapper.AddInParameter("@ExternalDocNo", DbType.AnsiString, pOOtherVendor.ExternalDocNo)
            DbCommandWrapper.AddInParameter("@FormSource", DbType.Int16, pOOtherVendor.FormSource)
            DbCommandWrapper.AddInParameter("@GrandTotal", DbType.Currency, pOOtherVendor.GrandTotal)
            DbCommandWrapper.AddInParameter("@PaymentGroup", DbType.Int16, pOOtherVendor.PaymentGroup)
            DbCommandWrapper.AddInParameter("@PersonInCharge", DbType.AnsiString, pOOtherVendor.PersonInCharge)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.AnsiString, pOOtherVendor.PostalCode)
            DbCommandWrapper.AddInParameter("@Priority", DbType.Int16, pOOtherVendor.Priority)
            DbCommandWrapper.AddInParameter("@Province", DbType.AnsiString, pOOtherVendor.Province)
            DbCommandWrapper.AddInParameter("@PRPOType", DbType.AnsiString, pOOtherVendor.PRPOType)
            DbCommandWrapper.AddInParameter("@PurchaseOrderNo", DbType.AnsiString, pOOtherVendor.PurchaseOrderNo)
            DbCommandWrapper.AddInParameter("@SONo", DbType.AnsiString, pOOtherVendor.SONo)
            DbCommandWrapper.AddInParameter("@Site", DbType.AnsiString, pOOtherVendor.Site)
            DbCommandWrapper.AddInParameter("@State", DbType.Int16, pOOtherVendor.State)
            DbCommandWrapper.AddInParameter("@StockReferenceNo", DbType.AnsiString, pOOtherVendor.StockReferenceNo)
            DbCommandWrapper.AddInParameter("@Taxable", DbType.Int16, pOOtherVendor.Taxable)
            DbCommandWrapper.AddInParameter("@TermsOfPayment", DbType.AnsiString, pOOtherVendor.TermsOfPayment)
            DbCommandWrapper.AddInParameter("@TotalAmountBeforeDiscount", DbType.Currency, pOOtherVendor.TotalAmountBeforeDiscount)
            DbCommandWrapper.AddInParameter("@TotalBaseAmount", DbType.Currency, pOOtherVendor.TotalBaseAmount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTaxAmount", DbType.Currency, pOOtherVendor.TotalConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TotalDiscountAmount", DbType.Currency, pOOtherVendor.TotalDiscountAmount)
            DbCommandWrapper.AddInParameter("@TotalTitleRegistrationFee", DbType.Currency, pOOtherVendor.TotalTitleRegistrationFee)
            DbCommandWrapper.AddInParameter("@PurchaseOrderDate", DbType.DateTime, pOOtherVendor.PurchaseOrderDate)
            DbCommandWrapper.AddInParameter("@VendorDescription", DbType.AnsiString, pOOtherVendor.VendorDescription)
            DbCommandWrapper.AddInParameter("@Vendor", DbType.AnsiString, pOOtherVendor.Vendor)
            DbCommandWrapper.AddInParameter("@Warehouse", DbType.AnsiString, pOOtherVendor.Warehouse)
            DbCommandWrapper.AddInParameter("@WONo", DbType.AnsiString, pOOtherVendor.WONo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pOOtherVendor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pOOtherVendor.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As POOtherVendor

            Dim pOOtherVendor As POOtherVendor = New POOtherVendor

            pOOtherVendor.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then pOOtherVendor.Owner = dr("Owner").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address1")) Then pOOtherVendor.Address1 = dr("Address1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address2")) Then pOOtherVendor.Address2 = dr("Address2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address3")) Then pOOtherVendor.Address3 = dr("Address3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AllocationPeriod")) Then pOOtherVendor.AllocationPeriod = dr("AllocationPeriod").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Balance")) Then pOOtherVendor.Balance = CType(dr("Balance"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then pOOtherVendor.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("City")) Then pOOtherVendor.City = dr("City").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CloseRespon")) Then pOOtherVendor.CloseRespon = dr("CloseRespon").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Country")) Then pOOtherVendor.Country = dr("Country").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryMethod")) Then pOOtherVendor.DeliveryMethod = CType(dr("DeliveryMethod"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then pOOtherVendor.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DownPayment")) Then pOOtherVendor.DownPayment = CType(dr("DownPayment"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DownPaymentAmountPaid")) Then pOOtherVendor.DownPaymentAmountPaid = CType(dr("DownPaymentAmountPaid"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DownPaymentIsPaid")) Then pOOtherVendor.DownPaymentIsPaid = CType(dr("DownPaymentIsPaid"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("EventDate")) Then pOOtherVendor.EventDate = dr("EventDate").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ExternalDocNo")) Then pOOtherVendor.ExternalDocNo = dr("ExternalDocNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FormSource")) Then pOOtherVendor.FormSource = CType(dr("FormSource"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("GrandTotal")) Then pOOtherVendor.GrandTotal = CType(dr("GrandTotal"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentGroup")) Then pOOtherVendor.PaymentGroup = CType(dr("PaymentGroup"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PersonInCharge")) Then pOOtherVendor.PersonInCharge = dr("PersonInCharge").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PostalCode")) Then pOOtherVendor.PostalCode = dr("PostalCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Priority")) Then pOOtherVendor.Priority = CType(dr("Priority"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Province")) Then pOOtherVendor.Province = dr("Province").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PRPOType")) Then pOOtherVendor.PRPOType = dr("PRPOType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PurchaseOrderNo")) Then pOOtherVendor.PurchaseOrderNo = dr("PurchaseOrderNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SONo")) Then pOOtherVendor.SONo = dr("SONo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Site")) Then pOOtherVendor.Site = dr("Site").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("State")) Then pOOtherVendor.State = CType(dr("State"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("StockReferenceNo")) Then pOOtherVendor.StockReferenceNo = dr("StockReferenceNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Taxable")) Then pOOtherVendor.Taxable = CType(dr("Taxable"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TermsOfPayment")) Then pOOtherVendor.TermsOfPayment = dr("TermsOfPayment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmountBeforeDiscount")) Then pOOtherVendor.TotalAmountBeforeDiscount = CType(dr("TotalAmountBeforeDiscount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalBaseAmount")) Then pOOtherVendor.TotalBaseAmount = CType(dr("TotalBaseAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalConsumptionTaxAmount")) Then pOOtherVendor.TotalConsumptionTaxAmount = CType(dr("TotalConsumptionTaxAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalDiscountAmount")) Then pOOtherVendor.TotalDiscountAmount = CType(dr("TotalDiscountAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalTitleRegistrationFee")) Then pOOtherVendor.TotalTitleRegistrationFee = CType(dr("TotalTitleRegistrationFee"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PurchaseOrderDate")) Then pOOtherVendor.PurchaseOrderDate = CType(dr("PurchaseOrderDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VendorDescription")) Then pOOtherVendor.VendorDescription = dr("VendorDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Vendor")) Then pOOtherVendor.Vendor = dr("Vendor").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Warehouse")) Then pOOtherVendor.Warehouse = dr("Warehouse").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WONo")) Then pOOtherVendor.WONo = dr("WONo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pOOtherVendor.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pOOtherVendor.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pOOtherVendor.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pOOtherVendor.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pOOtherVendor.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return pOOtherVendor

        End Function

        Private Sub SetTableName()

            If Not (GetType(POOtherVendor) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(POOtherVendor), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(POOtherVendor).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


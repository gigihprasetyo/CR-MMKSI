
using System;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Domain
{
    public class SparePartForecast_IF
    {
        public List<SparePartForecastStockManagement_IF> StockManagement;
        public List<SparepartForecastReject_IF> Reject;
        public List<SparepartForecastPOEstimateHeader_IF> POEstimateHeader;
        public List<SparepartForecastPOEstimateDetail_IF> POEstimateDetail;
        public List<SparepartForecastGoodReceiptHeader_IF> GoodReceiptHeader;
        public List<SparepartForecastGoodReceiptDetail_IF> GoodReceiptDetail;
    }

    public class SparePartMaster_IF
    {
        public int ID { get; set; }
        public int ProductCategoryID { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string PartNumberReff { get; set; }
        public string UoM { get; set; }
        public string MaterialCategoryCode { get; set; }
        public string AltPartNumber { get; set; }
        public string AltPartName { get; set; }
        public string PartCode { get; set; }
        public string ModelCode { get; set; }
        public string SupplierCode { get; set; }
        public string TypeCode { get; set; }
        public int Stock { get; set; }
        public decimal RetalPrice { get; set; }
        public string PartStatus { get; set; }
        public int ActiveStatus { get; set; }
        public int AccessoriesType { get; set; }
        public string ProductType { get; set; }
        public int IsWarranty { get; set; }
        public int RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }

    public class SparePartForecastMaster_IF
    {
        public int ID { get; set; }
        public int SparepartMasterID { get; set; }
        public int Stock { get; set; }
        public int MaxOrder { get; set; }
        public string NoBulletinService { get; set; }
        public string NoRecallCategory { get; set; }
        public int Status { get; set; }
        public int Rowstatus { get; set; }
        public string Createdby { get; set; }
        public DateTime Createdtime { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime LastUpdatedtime { get; set; }
    }

    public class SparePartForecastStockManagement_IF
    {
        public int ID { get; set; }
        public string PartNumber { get; set; }
        public int Stock { get; set; }
        public string NoBulletinService { get; set; }
        public string NoRecallCategory { get; set; }
        public int MaxOrder { get; set; }
        public int Status { get; set; }
        public int Rowstatus { get; set; }
        public string LastUpdateby { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }

    public class SparepartForecastReject_IF
    {
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public string PartNumber { get; set; }
        public bool isReject { get; set; }
        public string PoNumber { get; set; }
        public string DMSPRNo { get; set; }
        public int Rowstatus { get; set; }
        public string LastUpdateby { get; set; }
        public DateTime LastUpdateTime { get; set; }

    }

    public class SparepartForecastPOEstimateHeader_IF
    {
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public string DMSPRNo { get; set; }
        public string PONumber { get; set; }
        public string SONumber { get; set; }
        public DateTime? SODate { get; set; }
        public string DocumentType { get; set; }
        public int TermOfPaymentValue { get; set; }
        public string TermOfPaymentCode { get; set; }
        public string TermOfPaymentDesc { get; set; }
        public decimal AmountC2 { get; set; }
        public int Rowstatus { get; set; }
        public string LastUpdateby { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }

    public class SparepartForecastPOEstimateDetail_IF
    {
        public int ID { get; set; }
        public string PartNumber { get; set; }
        public decimal AllocQty { get; set; }
        public decimal OrderQty { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public int Rowstatus { get; set; }
        public string LastUpdateby { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }

    public class SparepartForecastGoodReceiptHeader_IF
    {
        public int ID { get; set; }
        public DateTime? DODate { get; set; }
        public DateTime? DueDate { get; set; }
        public string DONumber { get; set; }
        public DateTime? BillingDate { get; set; }
        public string BillingNumber { get; set; }
        public string ExpeditionNumber { get; set; }
        public int TermOfPaymentValue { get; set; }
        public string TermOfPaymentCode { get; set; }
        public string TermOfPaymentDesc { get; set; }
        public decimal AmountC2 { get; set; }
        public int Rowstatus { get; set; }
        public string LastUpdateby { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public string DealerCode { get; set; }
    }

    public class SparepartForecastGoodReceiptDetail_IF
    {
        public string SONumber { get; set; }
        public decimal Discount { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public decimal Qty { get; set; }
        public decimal Tax { get; set; }
        public decimal RetailPrice { get; set; }
    }
}

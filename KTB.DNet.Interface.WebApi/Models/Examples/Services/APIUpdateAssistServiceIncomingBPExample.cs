#region Summary
/*
 ===========================================================================
 AUTHOR        : Ivan
 PURPOSE       : APIUpdateAssistServiceIncomingBPExample class
 SPECIAL NOTES : DNet WebApi Project
 ---------------------
 Copyright  (c) 2021 
 ---------------------
 $History      : $
 Updated on 2021-03-23
 ===========================================================================
*/
#endregion

using Swashbuckle.Examples;


namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateAssistServiceIncomingBPExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                ID = 1,
                TglPengajuanEstimasi = "22/03/21",
                TglPersetujuanEstimasi = "22/03/21",
                TglBukaTransaksi = "22/03/21",
                WaktuMasuk = "22/03/21 09:07",
                TglJanjiSelesai = "22/03/21",
                TglTutupTransaksi = "22/03/21",
                WaktuKeluar = "22/03/21 11:30",
                KodeMekanik = "15324",
                VehicleModelDesc = "Outlander PHEV",
                VehicleColorDesc = "White Pearl",
                KMService = 5000,
                ServiceTypeCode = "Baru",
                ServiceBooking = "Yes",
                TotalLC = 1500000,
                TotalSubOrder = 1500000,
                TotalCat = 1500000,
                TotalNonCat = 1500000,
                DamageCategory = "Ringan",
                TotalPanel = "36",
                MethodofPayment = "TUNAI",
                InsuranceName = "Tokio Marine",
                WOStatus = 2,
                CustomerOwnerName = "PT Kuda Lima",
                CustomerOwnerPhoneNumber = "081221231213",
                CustomerVisitName = "Joni K",
                CustomerVisitPhoneNumber = "081221231213",
                UpdatedBy = "DealerUser"
            };
            return obj;
        }
    }
}
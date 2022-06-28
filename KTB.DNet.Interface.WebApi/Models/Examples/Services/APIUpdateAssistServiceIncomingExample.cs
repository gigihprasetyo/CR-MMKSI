#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIUpdateAssistServiceIncomingExample class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:14
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateAssistServiceIncomingExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                TglBukaTransaksi = "25/03/18",
                WaktuMasuk = "25/03/18 09:07",
                TglTutupTransaksi = "25/03/18",
                WaktuKeluar = "25/03/18 11:07",
                DealerCode = "100009",
                DealerBranchCode = "300033",
                KodeMekanik = "8815",
                NoWorkOrder = "IFS-1700574",
                KodeChassis = "MMBJYKL30GH016181",
                WorkOrderCategoryCode = "FS02",
                KMService = 6861,
                ServicePlaceCode = "Inside",
                ServiceTypeCode = "None",
                TotalLC = 203292,
                MetodePembayaran = "KREDIT",
                Model = "TRITON",
                Transmition = "A/T",
                DriveSystem = "4x4",
                WOStatus = 2,
                CustomerOwnerName =  "Miftahul Ulum",
                CustomerOwnerPhoneNumber = "082299801866",
                CustomerVisitName = "Ade Sukarja",
                CustomerVisitPhoneNumber = "085699991234",
                BookingCode = "Booking 1",
                StallCode = "100170-S51"
            };
            return obj;
        }
    }
}
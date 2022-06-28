using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateRealtimeServiceBookingExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                DealerCode = "100014",
                VechileModel = "XPANDER",
                VariantType = "Exceed",
                ServiceBookingCode = "",
                ChassisNumber = "MK2NCWPARHJ001626",
                PlateNumber = "AB5454AB",
                CustomerName = "Anjar Budi",
                CustomerPhoneNumber = "087463334123",
                PickupType = "DiTinggal",
                StallServiceType = "SB",
                IncomingDateStart = "2021-09-25T08:00:00",
                IncomingDateEnd = "2021-09-25T17:00:00",
                WorkingTimeStart = "2021-09-25T08:00:00",
                WorkingTimeEnd = "2021-09-25T09:30:00",
                IsMitsubishi = 1,
                ServiceAdvisorID = 0,
                Status = "",
                GRNote = "Ganti Aki, Ganti Filter AC, Ganti Oli",
                Complaint = "Kaki-kaki keras, Mobil terasa tidak stabil",
                Voucher = "KJ857JH",
                ServiceBookingActivities = new List<object>
                {
                    new 
                    {
                        ServiceTypeCode = "PM",
                        KindCode = "07"
                    }
                }
            };

            return obj;
        }
    }
}
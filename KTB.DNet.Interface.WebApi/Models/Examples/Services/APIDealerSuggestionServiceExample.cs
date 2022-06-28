using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIDealerSuggestionServiceExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                RequestedDate = "2021-08-26",
                ChassisNumber = "MK2NCWPARHJ001626",
                PickupType = 2,
                VechileModel = "XPANDER",
                VariantType = "Exceed",
                CustomerLatitude = -6.2062487,
                CustomerLongitude = 106.7993197,
                CheckinTime = "2021-08-26T08:00:00",
                CheckoutTime = "2021-08-26T12:00:00",
                RequestTime = "2021-08-26T12:00:00",
                ServiceBookingCode = "100572-202109-00004",
                AssistServiceTypeCode = "SB",
                FavDealers = new List<object>
                {
                    new 
                    {
                        DealerCode = "100572"
                    }
                },
                SelectedDealers = new List<object>
                {
                    new 
                    {
                        DealerCode = "100572"
                    }
                },
                ServiceTypes = new List<object>
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
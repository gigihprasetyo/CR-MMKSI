using Embarr.WebAPI.AntiXss;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class DealerSuggestionServiceParameterDto
    {
        [AntiXss]
        public DateTime RequestedDate { get; set; }
        [AntiXss]
        public string ChassisNumber { get; set; }
        [AntiXss]
        public string PickupType { get; set; }
        //[AntiXss]
        //public string VechileTypeCode { get; set; }
        [AntiXss]
        public string VechileModel { get; set; }
        [AntiXss]
        public string VariantType { get; set; }
        [AntiXss]
        public decimal CustomerLatitude { get; set; }
        [AntiXss]
        public decimal CustomerLongitude { get; set; }
        [AntiXss]
        public DateTime CheckinTime { get; set; }
        [AntiXss]
        public DateTime CheckoutTime { get; set; }
        [AntiXss]
        public DateTime RequestTime { get; set; }
        [AntiXss]
        public string ServiceBookingCode { get; set; }
        [AntiXss]
        public string AssistServiceTypeCode { get; set; }
        [AntiXss]
        public List<DealerSuggestionServiceFavDealerParameterDto> FavDealers { get; set; }
        [AntiXss]
        public List<DealerSuggestionServiceFavDealerParameterDto> SelectedDealers { get; set; }
        [AntiXss]
        public List<DealerSuggestionServiceParameterDetailDto> ServiceTypes { get; set; }
    }

    public class DealerSuggestionServiceParameterDetailDto
    {
        [AntiXss]
        public string ServiceTypeCode { get; set; }
        [AntiXss]
        public int ServiceTypeID { get; set; }
        [AntiXss]
        public int VechileModelID { get; set; }
        [AntiXss]
        public int VechileTypeID { get; set; }
        [AntiXss]
        public string KindCode { get; set; }
        [AntiXss]
        public string AssistServiceTypeCode { get; set; } 
    }

    public class DealerSuggestionServiceFavDealerParameterDto
    {
        [AntiXss]
        public string DealerCode { get; set; }
    }
}

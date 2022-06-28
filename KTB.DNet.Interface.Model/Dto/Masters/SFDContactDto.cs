#region Namespace Imports
using System;
using System.Collections.Generic;
#endregion
namespace KTB.DNet.Interface.Model 
{
    public class SFDContactDto  
    {
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public string SalesmanCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryCode { get; set; }
        public string HPNo { get; set; }
        public int PhoneType { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? Gender { get; set; }
        public string Address { get; set; }
        public string CityCode { get; set; }
        public string CustomerType { get; set; }
        public int LeadSource { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string LastUpdateBy { get; set; }

    }
}

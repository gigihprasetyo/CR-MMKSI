﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Domain 
{
    public class VWI_CRM_invalidcustomer
    {
        public Guid Id { get; set; }
        public bool InvalidIDCard { get; set; }
        public bool InvalidIDCard_Length { get; set; }
        public bool InvalidIDCard_Format { get; set; }
        public bool InvalidIDCard_Duplicate { get; set; }
        public bool InvalidIDCard_Dummy { get; set; }
        public bool InvalidMobile { get; set; }
        public bool InvalidMobile_Blank { get; set; }
        public bool InvalidMobile_Length { get; set; }
        public bool InvalidMobile_Format { get; set; }
        public bool InvalidMobile_Duplicate { get; set; }
        public bool InvalidMobile_Dummy { get; set; }
        public bool InvalidBirthDate { get; set; }
        public bool InvalidBirthDate_Blank { get; set; }
        public bool InvalidBirthDate_Range { get; set; }
        public bool InvalidEmail { get; set; }
        public bool InvalidEmail_Blank { get; set; }
        public bool InvalidEmail_Length { get; set; }
        public bool InvalidEmail_Format { get; set; }
        public bool InvalidEmail_Duplicate { get; set; }
        public bool InvalidCustClass { get; set; }
        public bool InvalidCustClass_Blank { get; set; }
        public bool InvalidCustClass_Data { get; set; }
        public string ID_Card_Error_Reason { get; set; }
        public string Mobile_Nbr_Error_Reason { get; set; }
        public string Birth_Date_Error_Reason { get; set; }
        public string Email_Address_Error_Reason { get; set; }
        public string Customer_Class_Error_Reason { get; set; }
        public DateTime LastCheckedTime { get; set; }
        public string ktb_dealercode { get; set; }
        public string ktb_bucompany { get; set; }
        public bool NGData { get; set; }

    }
}

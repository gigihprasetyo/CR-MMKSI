#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DealerDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Interface.Model.CustomAttribute;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class DealerDto : DtoBase
    {
        public string DealerCode { get; set; }

        public string DealerName { get; set; }

        public string Status { get; set; }

        public string Term { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string SalesUnitFlag { get; set; }

        public string ServiceFlag { get; set; }

        public string SparepartFlag { get; set; }

        public string CityName { get; set; }

        public string ProvinceName { get; set; }

        public string Kategori { get; set; }

        public short SystemID { get; set; }

        public int ID { get; set; }

        //public string DealerCode { get; set; }

        //public string DealerName { get; set; }

        //public string Status { get; set; }

        //public string Title { get; set; }

        //public string SearchTerm1 { get; set; }

        //public string SearchTerm2 { get; set; }

        //public string Address { get; set; }

        //public string ZipCode { get; set; }

        //public string Phone { get; set; }

        //public string Fax { get; set; }

        //public string Website { get; set; }

        //public string Email { get; set; }

        //public string SalesUnitFlag { get; set; }

        //public string ServiceFlag { get; set; }

        //public string SparepartFlag { get; set; }

        //public string SPANumber { get; set; }

        //[DataType(DataType.Date)]
        //[DateDisplayFormatAttribute]
        //public DateTime SPADate { get; set; }

        //public int FreePPh22Indicator { get; set; }

        //[DateTimeDisplayFormatAttribute]
        //public DateTime FreePPh22From { get; set; }

        //[DateTimeDisplayFormatAttribute]
        //public DateTime FreePPh22To { get; set; }

        //public int DueDate { get; set; }

        //public string AgreementNo { get; set; }

        //[DataType(DataType.Date)]
        //[DateDisplayFormatAttribute]
        //public DateTime AgreementDate { get; set; }

        //public string CreditAccount { get; set; }

        //public string LegalStatus { get; set; }

        //public DealerDto MainDealer { get; set; }

        //public Area1Dto Area1;

        //public Area2Dto Area2;

        //public MainAreaDto MainArea;

        //public CityDto City;

        //public ProvinceDto Province;

        //public DealerGroupDto DealerGroup { get; set; }

        //public ArrayList DealerAdditionals { get; set; }

        //public ArrayList BuletinOrganizations { get; set; }

        //public ArrayList Deposits { get; set; }

        //public ArrayList DepositC2s { get; set; }
    }
}

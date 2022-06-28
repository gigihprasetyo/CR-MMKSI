#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_EmployeeSalesDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using System;

namespace KTB.DNet.Interface.Model
{
    public class VWI_EmployeeSalesDto
    {
        public int ID { get; set; }
        public string SalesmanCode { get; set; }
        public string Name { get; set; }
        public int BirthCityID { get; set; }
        public string BirthPlaceCityCode { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public int MarriedStatus { get; set; }
        public string Address { get; set; }
        public int AddressCityID { get; set; }
        public string AddressCityCode { get; set; }
        public string City { get; set; }
        public int SalesmanAreaID { get; set; }
        public string SalesmanAreaCode { get; set; }
        public string SalesmanAreaDesc { get; set; }
        public int SalesmanLevelID { get; set; }
        public string SalesmanLevelDesc { get; set; }
        public int JobPositionID { get; set; }
        public string JobPositionDesc { get; set; }
        public int LeaderId { get; set; }
        public string LeaderSalesmanCode { get; set; }
        public string LeaderSalesmanName { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime ResignDate { get; set; }
        public string ResignReason { get; set; }
        public int DealerId { get; set; }
        public string DealerCode { get; set; }
        public int DealerBranchID { get; set; }
        public string DealerBranchCode { get; set; }

        public string Email { get; set; }
        public string NoHP { get; set; }
        public string Kategori { get; set; }
        public string Pendidikan { get; set; }
        public string NoKTP { get; set; }
        public int Status { get; set; }
        public string StatusDNET { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}


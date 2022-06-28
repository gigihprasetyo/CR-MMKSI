#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_EmployeePartsDto  class
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
    public class VWI_EmployeePartsDto
    {
        public int ID { get; set; }
        public string SalesmanCode { get; set; }
        public string Name { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public int MarriedStatus { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int SalesmanCategoryLevelId { get; set; }
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        public int ParentSalesmanCategoryLevelId { get; set; }
        public string ParentPositionCode { get; set; }
        public string ParentPositionName { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime ResignDate { get; set; }
        public string ResignReason { get; set; }
        public int DealerId { get; set; }
        public string DealerCode { get; set; }
        public int DealerBranchId { get; set; }
        public string DealerBranchCode { get; set; }

        public string Email { get; set; }
        public string NoHP { get; set; }
        public string Pendidikan { get; set; }
        public string NoKTP { get; set; }
        public int Status { get; set; }
        public string StatusDNET { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}


#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SalesmanHeaderDto  class
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
    public class SalesmanHeaderDto : DtoBase
    {
        #region Public Properties

        public int ID { get; set; }

        public string SalesmanCode { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public string PlaceOfBirth { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime DateOfBirth { get; set; }

        public byte Gender { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public int ShopSiteNumber { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime HireDate { get; set; }

        public int JobPositionId_Second { get; set; }

        public int JobPositionId_Third { get; set; }

        public int LeaderId { get; set; }

        public int JobPositionId_Leader { get; set; }

        public string RegisterStatus { get; set; }

        public string MarriedStatus { get; set; }

        public short ResignType { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime ResignDate { get; set; }

        public string ResignReason { get; set; }

        public byte SalesIndicator { get; set; }

        public byte SalesUnitIndicator { get; set; }

        public byte MechanicIndicator { get; set; }

        public byte SparePartIndicator { get; set; }

        public byte SPAdminIndicator { get; set; }

        public byte SPWareHouseIndicator { get; set; }

        public byte SPCounterIndicator { get; set; }

        public byte SPSalesIndicator { get; set; }

        public byte IsRequestID { get; set; }

        public string Status { get; set; }

        public ArrayList SAPRegisters { get; set; }

        public ArrayList SAPCustomers { get; set; }

        public ArrayList SalesmanTrainings { get; set; }

        public ArrayList SalesmanExperiences { get; set; }

        public ArrayList SalesmanAreaAssigns { get; set; }

        public ArrayList SalesmanTrainingParticipants { get; set; }

        public ArrayList SalesmanSalesTargets { get; set; }

        public ArrayList SalesmanAccomplishs { get; set; }

        public ArrayList SalesmanProfiles { get; set; }

        public ArrayList SalesmanUniformOrderDetails { get; set; }

        public ArrayList SalesmanUniformAssigneds { get; set; }

        public ArrayList UniformSalesmans { get; set; }

        public ArrayList TrainingSaless { get; set; }

        public ArrayList TrainingConfirmations { get; set; }

        public ArrayList SAPCustomerProspects { get; set; }

        public ArrayList SalesmanAdditionalInfo { get; set; }

        public ArrayList SalesmanPartShop { get; set; }

        public ArrayList SalesmanPartTarget { get; set; }

        public ArrayList SalesmanPartPerformance { get; set; }

        public DealerDto Dealer { get; set; }

        public DealerBranchDto DealerBranch { get; set; }

        public SalesmanAreaDto SalesmanArea { get; set; }

        public SalesmanLevelDto SalesmanLevel { get; set; }

        public JobPositionDto JobPosition { get; set; }

        #endregion

        #region "Custom Properties"

        private readonly int _lamaBekerjaBln = 0;

        public int LamaBekerjaBulan
        {
            get
            {
                return _lamaBekerjaBln;
            }
        }

        private readonly int _lamaBekerjaTahun = 0;

        public int LamaBekerjaTh
        {
            get
            {
                return _lamaBekerjaTahun;
            }
        }

        #endregion

        #region Custom Method

        public int TotalHire { get; set; }

        public int TotalResign { get; set; }

        public int TotalBM { get; set; }

        public int TotalMgr { get; set; }

        public int TotalAMGR { get; set; }

        public int Totalspv1 { get; set; }

        public int Totalspv2 { get; set; }

        public int Totalspv3 { get; set; }

        public int Totalsl1 { get; set; }

        public int Totalsl2 { get; set; }

        public int Totaltr { get; set; }

        #endregion
    }
}

#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SAPCustomerDto  class
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
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
#endregion

namespace KTB.DNet.Interface.Model
{
    [DataContract]
    public class SAPCustomerDto : DtoBase
    {
        #region Data Members
        [DataMember]
        public string CustomerCode { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public short CustomerType { get; set; }

        [DataMember]
        public short CustomerPurpose { get; set; }

        [DataMember]
        public string CustomerAddress { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public byte Sex { get; set; }

        [DataMember]
        public byte AgeSegment { get; set; }

        [DataMember]
        public short InformationType { get; set; }

        [DataMember]
        public short InformationSource { get; set; }

        [DataMember]
        public byte Status { get; set; }

        [DataMember]
        public short StateCode { get; set; }

        [DataMember]
        public short StatusCode { get; set; }

        [DataMember]
        public int Qty { get; set; }

        [DataMember]
        public string CurrVehicleBrand { get; set; }

        [DataMember]
        public string CurrVehicleType { get; set; }

        [DataMember]
        public string Note { get; set; }

        [DataMember]
        public string WebID { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        [DataMember]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        [DataMember]
        public DateTime ProspectDate { get; set; }

        [DataMember]
        public string CampaignCode { get; set; }
        #endregion

        #region Ignored Data Members
        public int ID { get; set; }

        [IgnoreDataMember]
        public VehicleTypeDto VehicleType { get; set; }

        [IgnoreDataMember]
        public SalesmanHeaderDto SalesmanHeader { get; set; }

        [IgnoreDataMember]
        public DealerDto Dealer { get; set; }

        [IgnoreDataMember]
        public BusinessSectorDetailDto BusinessSectorDetail { get; set; }

        [IgnoreDataMember]
        public string SalesforceID { get; set; }

        [IgnoreDataMember]
        public bool IsSPK { get; set; }

        [IgnoreDataMember]
        public Guid OriginatingLeadId { get; set; }

        [IgnoreDataMember]
        public short LeadStatus { get; set; }

        [IgnoreDataMember]
        public string Description { get; set; }

        [IgnoreDataMember]
        public string PreferedVehicleModel { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        [IgnoreDataMember]
        public DateTime EstimatedCloseDate { get; set; }
        #endregion

        #region Customs Properties
        private int _businessSectorDetailID;
        private string _vehicleTypeCode;
        private string _salesmanHeaderCode;
        private string _dealerCode;

        [DataMember]
        public int BusinessSectorDetailID
        {
            get
            {
                if (BusinessSectorDetail != null)
                    return BusinessSectorDetail.ID;
                else
                    return _businessSectorDetailID;
            }
            set
            {
                _businessSectorDetailID = value;
            }
        }
        [DataMember]
        public string VehicleTypeCode
        {
            get
            {
                if (VehicleType != null)
                    return VehicleType.VehicleTypeCode;
                else
                    return _vehicleTypeCode;
            }
            set
            {
                _vehicleTypeCode = value;
            }
        }
        [DataMember]
        public string SalesmanHeaderCode
        {
            get
            {
                if (SalesmanHeader != null)
                    return SalesmanHeader.SalesmanCode;
                else
                    return _salesmanHeaderCode;
            }
            set
            {
                _salesmanHeaderCode = value;
            }
        }
        [DataMember]
        public string DealerCode
        {
            get
            {
                if (Dealer != null)
                    return Dealer.DealerCode;
                else
                    return _dealerCode;
            }
            set
            {
                _dealerCode = value;
            }
        }

        [IgnoreDataMember]
        public string CustomerTypeValue { get; set; }
        [IgnoreDataMember]
        public byte AgeSegmentDate { get; set; }
        [IgnoreDataMember]
        public string CustomerPurposeValue { get; set; }
        [IgnoreDataMember]
        public string InformationTypeValue { get; set; }
        [IgnoreDataMember]
        public string SexValue { get; set; }
        [IgnoreDataMember]
        public string InformationSourceValue { get; set; }
        #endregion
    }
}

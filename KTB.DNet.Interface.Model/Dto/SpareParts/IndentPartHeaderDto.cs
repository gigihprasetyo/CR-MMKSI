#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : IndentPartHeaderDto  class
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
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class IndentPartHeaderDto : DtoBase
    {
        #region Public Properties

        public int ID { get; set; }

        public string RequestNo { get; set; }

        public DateTime RequestDate { get; set; }

        public int MaterialType { get; set; }

        public byte Status { get; set; }

        public byte StatusKTB { get; set; }

        public string SubmitFile { get; set; }

        public byte PaymentType { get; set; }

        public decimal Price { get; set; }

        public DateTime KTBConfirmedDate { get; set; }

        public byte DescID { get; set; }

        public string ChassisNumber { get; set; }

        public string DMSPRNo { get; set; }

        public DealerDto Dealer { get; set; }

        #endregion

        #region Custom Properties
        public string MaterialTypeDesc { get; set; }

        public string StatusDealerDesc { get; set; }

        public string StatusKTBDesc { get; set; }

        public string StatusInProgres { get; set; }

        public int TotalQuantity { get; set; }

        #endregion
    }

    public class IndentPartHeaderCreateResponseDto
    {
        #region Public Properties

        public string RequestNo { get; set; }

        public DateTime RequestDate { get; set; }

        #endregion

        #region Custom Properties

        #endregion
    }
}

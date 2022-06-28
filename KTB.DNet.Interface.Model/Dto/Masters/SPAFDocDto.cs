#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPAFDocDto  class
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
#endregion

namespace KTB.DNet.Interface.Model
{
    public class SPAFDocDto : DtoBase
    {
        public int ID { get; set; }

        public short Status { get; set; }

        public short DocType { get; set; }

        public string OrderDealer { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime PostingDate { get; set; }

        public string ReffLetter { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime DateLetter { get; set; }

        public string CustomerName { get; set; }

        public decimal RetailPrice { get; set; }

        public decimal SPAF { get; set; }

        public decimal Subsidi { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime TglSetuju { get; set; }

        public string UploadFile { get; set; }

        public string UploadBy { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime UploadDate { get; set; }

        public string AlasanPenolakan { get; set; }

        public string DealerLeasing { get; set; }

        public short SellingType { get; set; }

        public decimal PPHPercent { get; set; }

        public DealerDto Dealer { get; set; }

        public ChassisMasterDto ChassisMaster { get; set; }

        //public List<SPAFDocHistoryDto> SPAFDocHistory { get; set; }
    }
}

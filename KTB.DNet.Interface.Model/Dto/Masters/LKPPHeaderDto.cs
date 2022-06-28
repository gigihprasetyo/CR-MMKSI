#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : LKPPHeaderDto  class
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class LKPPHeaderDto : DtoBase
    {
        public int ID { get; set; }

        public string ReferenceNumber { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime LetterDate { get; set; }

        public string GovInstName { get; set; }

        public string Description { get; set; }

        public string Attachment { get; set; }

        public short Classification { get; set; }

        public short Status { get; set; }

        public string Notes { get; set; }

        public string DealerCode { get; set; }

        //public List<LKPPDealerDto> LKPPDealers { get; set; }

        public List<LKPPDetailDto> LKPPDetails { get; set; }
    }
}

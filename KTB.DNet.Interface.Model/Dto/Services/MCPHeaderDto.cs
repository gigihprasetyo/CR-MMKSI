#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : MCPHeaderDto  class
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
    public class MCPHeaderDto : DtoBase
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

        public DealerDto Dealer { get; set; }

        public List<MCPDealerDto> MCPDealers { get; set; }

        public List<MCPDetailDto> MCPDetails { get; set; }
    }
}

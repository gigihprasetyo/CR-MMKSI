#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMasterPKTDto  class
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
    public class ChassisMasterPKTDto : DtoBase
    {
        public int ID { get; set; }

        public int ChassisMasterID { get; set; }

        public string ChassisNumber { get; set; }

        public string DealerCode { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime PKTDate { get; set; }

        //public ChassisMasterDto ChassisMaster { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
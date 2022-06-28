#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EstimationEquipHeaderResponseDto  class
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
    public class EstimationEquipHeaderResponseDto
    {
        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime EstimationDate { get; set; }

        public string EstimationNumber { get; set; }
    }
}


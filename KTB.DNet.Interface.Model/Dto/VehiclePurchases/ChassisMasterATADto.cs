#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChasssisMAsterATADto  class
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
    /// <summary>
    /// ChassisMaster ATA DTO
    /// </summary>
    /// <seealso cref="KTB.DNet.Interface.Model.DtoBase" />
    public class ChassisMasterATADto : DtoBase
    {
        [IgnoreDataMember]
        public int ID { get; set; }
        public string ChassisNumber { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime ETADate
        {
            get; set;
        }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime ATADate
        {
            get; set;
        }

        public string RemarkATA { get; set; }
    }
}

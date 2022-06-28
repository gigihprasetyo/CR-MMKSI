#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : MsAppVersion  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace KTB.DNet.Interface.Domain
{
    [Table("MsAppVersion")]
    public class MsAppVersion : BaseDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VersionId { get; set; }

        public Guid AppId { get; set; }

        [ForeignKey("AppId")]
        [Required]
        public virtual MsApplication MsApplication { get; set; }

        public string Version { get; set; }

        public string Description { get; set; }

        public bool IsCurrentDeployment { get; set; }
    }
}

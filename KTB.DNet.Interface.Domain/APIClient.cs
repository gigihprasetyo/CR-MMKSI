#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIClient  class
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace KTB.DNet.Interface.Domain
{
    [Table("APIClient")]
    public class APIClient : BaseDomain
    {
        [Key]
        public Guid ClientId { get; set; }

        public string Name { get; set; }

        public Guid SecretKey { get; set; }

        public Guid AppId { get; set; }

        [ForeignKey("AppId")]
        [Required]
        public virtual MsApplication MsApplication { get; set; }

        public virtual ICollection<APIClientUser> Users { get; set; }


    }
}

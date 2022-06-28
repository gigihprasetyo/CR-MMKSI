#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIClientPermission  class
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
using KTB.DNet.Interface.Framework.CustomAttribute;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace KTB.DNet.Interface.Domain
{
    [Table("APIClientPermission")]
    public class APIClientPermission : BaseDomain
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public Guid ClientId { get; set; }

        [ForeignKey("ClientId")]
        [Required]
        [Ignore]
        public virtual APIClient Client { get; set; }

        public int PermissionId { get; set; }

        [ForeignKey("PermissionId")]
        [Required]
        [Ignore]
        public virtual APIEndpointPermission Permission { get; set; }

    }
}

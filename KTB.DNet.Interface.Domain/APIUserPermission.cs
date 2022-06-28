#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIUserPermission  class
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace KTB.DNet.Interface.Domain
{
    [Table("APIUserPermission")]
    public class APIUserPermission : BaseDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ClientUserId { get; set; }

        [ForeignKey("ClientUserId")]
        [Required]
        [Ignore]
        public virtual APIClientUser ClientUser { get; set; }

        public int PermissionId { get; set; }

        [ForeignKey("PermissionId")]
        [Required]
        [Ignore]
        public virtual APIEndpointPermission Permission { get; set; }

        public bool IsCustomPermission { get; set; }

        public bool IsDismantledPermission { get; set; }

        [NotMapped]
        public List<APIEndpointPermission> Permissions { get; set; }
    }
}

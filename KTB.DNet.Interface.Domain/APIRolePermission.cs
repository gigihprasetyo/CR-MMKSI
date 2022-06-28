#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIRolePermission  class
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace KTB.DNet.Interface.Domain
{
    [Table("APIRolePermission")]
    public class APIRolePermission : BaseDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ClientRoleId { get; set; }

        [ForeignKey("ClientRoleId")]
        [Ignore]
        public virtual APIClientRole ClientRole { get; set; }

        public int PermissionId { get; set; }

        [ForeignKey("PermissionId")]
        [Ignore]
        public APIEndpointPermission Permission { get; set; }
    }
}

#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIClientRole  class
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace KTB.DNet.Interface.Domain
{
    [Table("APIClientRole")]
    public class APIClientRole : BaseDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid ClientId { get; set; }

        [ForeignKey("ClientId")]
        [Required]
        [Ignore]
        public virtual APIClient Client { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        [Required]
        [Ignore]
        public virtual APIRole Role { get; set; }

        private ICollection<APIRolePermission> _rolepermissions;
        [Ignore]
        public virtual ICollection<APIRolePermission> RolePermissions
        {
            get { return _rolepermissions ?? (_rolepermissions = new Collection<APIRolePermission>()); }
            set { _rolepermissions = value; }
        }
    }
}

#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIClientUser  class
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
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace KTB.DNet.Interface.Domain
{
    [Table("APIClientUser")]
    public class APIClientUser : BaseDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid ClientId { get; set; }

        [ForeignKey("ClientId")]
        [Required]
        public virtual APIClient Client { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [Required]
        public virtual APIUser User { get; set; }

        private ICollection<APIUserPermission> _userPermissions;
        public virtual ICollection<APIUserPermission> UserPermissions
        {
            get { return _userPermissions ?? (_userPermissions = new Collection<APIUserPermission>()); }
            set { _userPermissions = value; }
        }

        public string Token { get; set; }

        public DateTime? TokenExpired { get; set; }

        public DateTime? LastActivity { get; set; }

        public DateTime? LastLogin { get; set; }
    }
}

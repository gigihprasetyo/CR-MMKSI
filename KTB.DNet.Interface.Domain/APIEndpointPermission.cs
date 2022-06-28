#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIPermission  class
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
using KTB.DNet.Interface.Framework.Enums;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Domain
{
    [Table("APIEndpointPermission")]
    public class APIEndpointPermission : BaseDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string PermissionCode { get; set; }

        public string URI { get; set; }

        public int EndpointGroup { get; set; }

        public TransactionType EndpointType { get; set; }

        public OperationType OperationType { get; set; }

        public string Description { get; set; }

        public bool IsScheduled { get; set; }

        public bool IsLogEnabled { get; set; }

        public bool IsRuntimeLogEnabled { get; set; }

        //ROLEPERMISSIONS
        private ICollection<int> _rolePermissionIds;
        [NotMapped]
        public ICollection<int> RolePermissionIds
        {
            get { return _rolePermissionIds ?? (_rolePermissionIds = RolePermissions.Select(s => s.Id).ToList()); }
            set { _rolePermissionIds = value; }
        }
        private ICollection<APIRolePermission> _rolepermissions;
        public virtual ICollection<APIRolePermission> RolePermissions
        {
            get { return _rolepermissions ?? (_rolepermissions = new Collection<APIRolePermission>()); }
            set { _rolepermissions = value; }
        }

        private ICollection<APIUserPermission> _userPermissions;
        public virtual ICollection<APIUserPermission> UserPermissions
        {
            get { return _userPermissions ?? (_userPermissions = new Collection<APIUserPermission>()); }
            set { _userPermissions = value; }
        }

        //Throttles
        private ICollection<APIThrottle> _throttles;
        public virtual ICollection<APIThrottle> apiThrottles
        {
            get { return _throttles ?? (_throttles = new Collection<APIThrottle>()); }
            set { _throttles = value; }
        }
    }
}

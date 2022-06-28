#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIUser  class
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
    public class APIUser : BaseDomain
    {
        public int AccessFailedCount { get; set; }

        public ICollection<APIUserClaim> Claims { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public int Id { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public ICollection<APIUserLogin> Logins { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public ICollection<APIUserRole> Roles { get; set; }

        public string SecurityStamp { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public string UserName { get; set; }

        public List<string> RoleNames { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Street1 { get; set; }

        public string Street2 { get; set; }

        public string Street3 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string Company { get; set; }

        [Required(ErrorMessage = "Please enter a status.")]
        public bool Status { get; set; }

        [NotMapped]
        public string FullName { get { return String.Format("{0} {1}", FirstName, LastName); } }

        public bool IsActive { get; set; }

        public virtual ICollection<APIClientUser> Clients { get; set; }

        public short? DealerId { get; set; }
        [ForeignKey("DealerId")]
        public Dealer Dealer { get; set; }

        public int? DealerCompanyId { get; set; }

        public int? GroupDealerId { get; set; }

        [NotMapped]
        public string DealerCode { get; set; }

        [NotMapped]
        public string NewPassword { get; set; }

        [NotMapped]
        public ICollection<APIUserRole> UserRoles { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedTime { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedTime { get; set; }
    }

}

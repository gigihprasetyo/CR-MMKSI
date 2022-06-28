#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : User ViewModel class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/11/2018 17:43
//
// ===========================================================================	
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KTB.DNet.Interface.WebUI.Models
{
    public class UserViewModel
    {
        private string _fullName;
        public int Id { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Street")]
        public string Street1 { get; set; }

        public string Street2 { get; set; }

        public string Street3 { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Company")]
        public string Company { get; set; }

        [Display(Name = "Dealer Id")]
        public short? DealerId { get; set; }

        [Display(Name = "Dealer Code")]
        public string DealerCode { get; set; }

        [Display(Name = "Status")]
        public bool Status { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(_fullName))
                    return String.Format("{0} {1}", FirstName, LastName);
                else
                    return _fullName;
            }
            set
            {
                _fullName = value;
            }
        }

        public bool IsDMSAdmin { get; set; }

        [Display(Name = "Client")]

        public List<ClientViewModel> Clients { get; set; }

        [Display(Name = "Role")]
        public List<RoleViewModel> Roles { get; set; }


        public List<ClientUserViewModel> ClientUsers { get; set; }

        public List<UserPermissionViewModel> UserPermissions { get; set; }

        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Compare("NewPassword")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        //[Display(Name = "Permissions")]
        ////[Required(ErrorMessage = "Please select at least one item")]
        //public List<int> ListOfSelectedPermissionId { get; set; }
    }

}
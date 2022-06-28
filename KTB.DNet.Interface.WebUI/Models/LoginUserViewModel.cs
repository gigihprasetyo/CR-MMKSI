#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : LoginUser ViewModel class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/11/2018 17:43
//
// ===========================================================================	
#endregion


namespace KTB.DNet.Interface.WebUI.Models
{
    public class LoginUserViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DealerCode { get; set; }
        public string ClientId { get; set; }
    }
}
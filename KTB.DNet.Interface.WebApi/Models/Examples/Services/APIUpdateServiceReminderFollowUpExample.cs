#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIUpdateMSPClaimExample class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 9/11/2018 15:50
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;
using System;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateServiceReminderFollowUpmExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                ID = 1,
                ServiceReminderID = 11,
                FollowUpStatus = 2,
                FollowUpAction = "Konsumen belum ada respon",
                FollowUpDate = DateTime.Now
            };

            return obj;
        }
    }
}
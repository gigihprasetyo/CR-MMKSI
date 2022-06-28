#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreatePDIExample class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:14
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIFollowUpServiceReminderExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy  = "Dealer User",
                ServiceReminderID = 111,
                FollowUpStatus = 2,
                FollowUpAction = "Sudah booking service",
                FollowUpDate = "25/07/20",
                BookingDate = "25/07/20 09:07:10",
                WorkOrderNumber = "WOM-111111-2020-0000001"
            };

            return obj;
        }
    }
}
#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : APICreateCRM_bookableresourcebookingExample class
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 6 Okt 2021
 ===========================================================================
*/
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateCRM_bookableresourcebookingExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                name = "name value",
                bookingtype = "bookingtype value",
                bookingstatus = "bookingstatus value",
                starttime = "starttime value",
                endtime = "endtime value",
                duration = "duration value",
                ownerid = "ownerid value",
                owningbusinessunit = "owningbusinessunit value",
                createdby = "createdby value",
                createdon = "createdon value",
                modifiedby = "modifiedby value",
                modifiedon = "modifiedon value",
                UpdatedBy = "UpdatedBy value"
            };

            return obj;
        }
    }
}
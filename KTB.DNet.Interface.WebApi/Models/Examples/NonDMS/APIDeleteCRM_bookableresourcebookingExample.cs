#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : APIDeleteCRM_bookableresourcebookingExample class
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
    public class APIDeleteCRM_bookableresourcebookingExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                bookableresourcebookingid = "bookableresourcebookingid value",
                modifiedby = "modifiedby value",
                modifiedon = "modifiedon value",
                UpdatedBy = "UpdatedBy value"
            };

            return obj;
        }
    }
}
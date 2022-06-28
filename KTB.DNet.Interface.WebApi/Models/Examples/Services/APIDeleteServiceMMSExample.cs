#region Summary
/*
 ===========================================================================
 AUTHOR        : Ivan
 PURPOSE       : APIUpdateServiceMMSExample class
 SPECIAL NOTES : DNet WebApi Project
 ---------------------
 Copyright  (c) 2021 
 ---------------------
 $History      : $
 Created on 2021-10-26
 ===========================================================================
*/
#endregion

using Swashbuckle.Examples;


namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIDeleteServiceMMSExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                ID = 1,
                Status = 1,
                UpdatedBy = "DealerUser"
            };
            return obj;
        }
    }
}
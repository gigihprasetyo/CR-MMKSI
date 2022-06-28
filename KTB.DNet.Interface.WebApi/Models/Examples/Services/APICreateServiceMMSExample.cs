#region Summary
/*
 ===========================================================================
 AUTHOR        : Ivan
 PURPOSE       : APICreateServiceMMSExample class
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
    public class APICreateServiceMMSExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                PBU = "PT-DIPO",
                BU = "100011",
                BU_Branch = "200017",
                WO_No = "200017-WOM-2021-09-000042",
                WO_Service_Date = "25/10/2021",
                ChassisNo = "MKNCWTARJ00012",
                PlateNo = "K1025NN",
                Next_Estimated_Service_Date = "25/11/2021",
                Notes = "Ganti Ban Kanan belakang, ban kiri depan",
                Status = 0,
                UpdatedBy = "DealerUser"
            };
            return obj;
        }
    }
}
#region Summary
/*
 ===========================================================================
 AUTHOR        : Ivan
 PURPOSE       : APIDeleteAssistServiceIncomingBPExample class
 SPECIAL NOTES : DNet WebApi Project
 ---------------------
 Copyright  (c) 2021 
 ---------------------
 $History      : $
 Created on 2021-03-23
 ===========================================================================
*/
#endregion

using Swashbuckle.Examples;


namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIDeleteAssistServiceIncomingBPExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                DealerCode = "150001",
                NoWorkOrder = "150001-BPM-2021-04-000100",
                TglTutupTransaksi = "22/03/21",
                WaktuKeluar = "22/03/21 11:30",
                UpdatedBy = "DealerUser"
            };
            return obj;
        }
    }
}
#region Namespace Imports
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model.Parameters.Services
{
    public class VWI_MobileServiceReminderParameterDto : ParameterDtoBase 
    {
        public string DealerCode { get; set; }
        public string ChassisNumber { get; set; }
        public DateTime ServiceReminderDate { get; set; }
        public string KindCode { get; set; }
        public string KindDescription { get; set; }
        public string Remark { get; set; }
        public int ReminderType { get; set; }
        public int ReminderDelta { get; set; }
    }
}

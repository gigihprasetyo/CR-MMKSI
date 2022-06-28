#region Namespace Imports
using System;
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_BabitMasterRetailTargetDto : DtoBase
    {
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public string DealerBranchCode { get; set; }
        public string SubCategoryVehicle { get; set; }
        public int MonthPeriod { get; set; }
        public int YearPeriod { get; set; }
        public string RetailTarget { get; set; }
        public int Status { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdateBy { get; set; }
    }
}

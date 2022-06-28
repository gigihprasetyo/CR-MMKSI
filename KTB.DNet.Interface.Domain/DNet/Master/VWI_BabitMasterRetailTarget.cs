using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Domain
{
    public class VWI_BabitMasterRetailTarget
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
        public string CreatedBy { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string LastUpdateBy { get; set; }
    }
}

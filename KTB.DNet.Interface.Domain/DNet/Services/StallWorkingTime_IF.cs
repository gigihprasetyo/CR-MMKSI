
using System;
namespace KTB.DNet.Interface.Domain
{
    public class StallWorkingTime_IF
    {
        public Int64? ID { get; set; }
        public int DealerID { get; set; }
        public DateTime Tanggal { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public TimeSpan RestTimeStart { get; set; }
        public TimeSpan RestTimeEnd { get; set; }
        public int StallMasterID { get; set; }
        public int IsHoliday { get; set; }
        public int VisitType { get; set; }
        public string Notes { get; set; }
        public int RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }
}

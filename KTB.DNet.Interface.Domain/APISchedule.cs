using KTB.DNet.Interface.Framework.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTB.DNet.Interface.Domain
{

    [Table("APISchedule")]
    public class APISchedule : BaseDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public ScheduleType ScheduleType { get; set; }

        public ScheduleDay? ScheduleDay { get; set; }

        public int? MonthDay { get; set; }

        public TimeSpan ScheduleTime { get; set; }

        public int Interval { get; set; }

        public string DealerCode { get; set; }
    }
}

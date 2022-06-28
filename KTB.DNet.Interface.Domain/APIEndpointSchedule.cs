using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTB.DNet.Interface.Domain
{
    [Table("APIEndpointSchedule")]
    public class APIEndpointSchedule : BaseDomain
    {
        [Key]
        public int Id { get; set; }

        public int EndpointId { get; set; }

        [ForeignKey("EndpointId")]
        public virtual APIEndpointPermission Endpoint { get; set; }

        public int ScheduleId { get; set; }

        [ForeignKey("ScheduleId")]
        public virtual APISchedule Schedule { get; set; }

    }
}

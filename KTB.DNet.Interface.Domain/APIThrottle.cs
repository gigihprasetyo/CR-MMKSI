using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Domain
{
    [Table("APIThrottle")]
    public class APIThrottle: BaseDomain
    {
        [Key]
        public int Id { get; set; }

        public int EndpointId { get; set; }

        [ForeignKey("EndpointId")]
        public virtual APIEndpointPermission Endpoint { get; set; }                

        public int RequestLimit { get; set; }

        public int TimeInSeconds { get; set; }

        public bool Enable { get; set; }
    }
}

using KTB.DNet.Interface.Framework.CustomAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Domain
{
    [Table("MsApplicationPermission")]
    public class MsApplicationPermission: BaseDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid AppId { get; set; }

        [ForeignKey("AppId")]
        [Required]
        [Ignore]
        public virtual MsApplication MsApplication { get; set; }

        public int PermissionId { get; set; }

        [ForeignKey("PermissionId")]
        [Required]
        [Ignore]
        public virtual APIEndpointPermission Permission { get; set; }
    }
}

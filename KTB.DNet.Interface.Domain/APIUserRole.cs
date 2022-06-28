
using System.ComponentModel.DataAnnotations.Schema;

namespace KTB.DNet.Interface.Domain
{
    [Table("APIUserRole")]
    public class APIUserRole : BaseDomain
    {
        public int RoleId { get; set; }

        public int UserId { get; set; }
    }
}

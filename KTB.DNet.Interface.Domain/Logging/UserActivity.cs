using KTB.DNet.Interface.Framework.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTB.DNet.Interface.Domain
{
    [Table("UserActivity")]
    public class UserActivity : BaseDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, MaxLength(256)]
        public string Username { get; set; }

        [MaxLength(256)]
        public string Endpoint { get; set; }

        [Required]
        public UserActivityType Activity { get; set; }

        [Required]
        public DateTime ActivityTime { get; set; }

        public string ActivityDesc { get; set; }

        public string DealerCode { get; set; }

        public Guid AppId { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTB.DNet.Interface.Domain
{
    [Table("TransactionLog")]
    public class TransactionLog : BaseDomain
    {
        [Key]
        public long Id { get; set; }

        public string SenderIP { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }

        public string Endpoint { get; set; }

        public string Input { get; set; }

        public string Output { get; set; }

        public bool Status { get; set; }

        public long? ParentId { get; set; }

        public string DealerCode { get; set; }

        public Guid ClientId { get; set; }

        public Guid AppId { get; set; }

        [NotMapped]
        public bool IsResolved { get; set; }

        [NotMapped]
        public int Total { get; set; }
    }
}

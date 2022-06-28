using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTB.DNet.Interface.Domain
{
    [Table("TransactionRuntime")]
    public class TransactionRuntime : BaseDomain
    {
        [Key]
        public long Id { get; set; }

        public long TransactionLogId { get; set; }

        [ForeignKey("TransactionLogId")]
        public virtual TransactionLog TransactionLog { get; set; }

        public string MethodName { get; set; }

        public DateTime StartedTime { get; set; }

        public DateTime FinishedTime { get; set; }

        [NotMapped]
        public int ExecutionTime { get; set; }
    }
}

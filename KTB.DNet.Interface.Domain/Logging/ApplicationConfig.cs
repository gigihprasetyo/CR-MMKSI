
using KTB.DNet.Interface.Framework.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTB.DNet.Interface.Domain
{
    [Table("ApplicationConfig")]
    public class ApplicationConfig : BaseDomain
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string ConfigKey { get; set; }

        public string Value { get; set; }

        public ConfigDataType DataType { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}

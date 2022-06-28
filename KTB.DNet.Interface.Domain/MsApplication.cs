using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Domain
{
    [Table("MsApplication")]
    public class MsApplication : BaseDomain
    {
        [Key]
        public Guid AppId { get; set; }

        public string Name { get; set; }

        public string DeploymentJenkinsJobName { get; set; }

        public string DeploymentBackupFolder { get; set; }

    }
}

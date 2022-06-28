using System;
using KTB.DNet.Interface.Framework.CustomAttribute;

namespace KTB.DNet.Interface.Model
{
    public class DSFLeasingClaimDAPPDto : DtoBase
    {
        public string RegNumber { get; set; }
        public string RemarkByDealer { get; set; }
        [Ignore]
        public string FileName { get; set; }
        [Ignore]
        public string FileDescription { get; set; }
        [Ignore]
        public string Path { get; set; }
    }
}

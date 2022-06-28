using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class WarrantyActivationGetFileParameter
    {
        public string ChassisNumber { get; set; }
        [DefaultValue(false)]
        public bool IsEncrypted { get; set; } 
    }
}

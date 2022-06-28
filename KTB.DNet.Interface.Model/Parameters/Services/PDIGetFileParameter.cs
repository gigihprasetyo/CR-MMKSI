using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model.Parameters.Services
{
    public class PDIGetFileParameter 
    {
        public string ChassisNumber { get; set; }

        [DefaultValue(false)]
        public bool IsEncrypted { get; set; } 
    }
}

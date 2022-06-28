using Embarr.WebAPI.AntiXss;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class GetServiceTypeParameterDto
    {
        [AntiXss]
        public string ChassisNumber { get; set; }

        [AntiXss]
        public string DealerCode { get; set; }
    }

    
}

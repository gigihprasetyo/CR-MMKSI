using Embarr.WebAPI.AntiXss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class ServiceRecommendationParameterDto
    {
        [AntiXss]
        public string ChassisNumber { get; set; }
        [AntiXss]
        public string PhoneNumber { get; set; }
    }
}

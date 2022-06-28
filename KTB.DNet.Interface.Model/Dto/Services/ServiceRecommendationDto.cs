using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class ServiceRecommendationDto
    {
        public string Tipe { get; set; }
        public List<ServiceRecommendationDetailDto> Rekomendasi { get; set; }
    }

    public class ServiceRecommendationDetailDto
    {
        public string Description { get; set; }
    }
}

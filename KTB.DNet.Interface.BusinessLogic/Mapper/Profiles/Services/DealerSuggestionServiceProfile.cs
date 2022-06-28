using AutoMapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.BusinessLogic.MapperBL
{
    public class DealerSuggestionServiceProfile : Profile
    {
        public DealerSuggestionServiceProfile()
        {
            CreateMap<DealerSuggestionService, DealerSuggestionServiceDto>()
            .ForMember(dest => dest.DealerID, opt => opt.MapFrom(src => src.DealerID))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.DealerName, opt => opt.MapFrom(src => src.DealerName))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Distance, opt => opt.MapFrom(src => src.Distance))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.ListJadwal, cfg =>
                    cfg.MapFrom(src => JsonConvert.DeserializeObject<List<DealerSuggestionServiceDetailDto>>(src.ListJadwal)))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

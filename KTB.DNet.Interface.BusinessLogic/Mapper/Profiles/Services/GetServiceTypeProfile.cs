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
    public class GetServiceTypeProfile : Profile
    {
        public GetServiceTypeProfile()
        {
            CreateMap<GetServiceType, GetServiceTypeDto>()
            .ForMember(dest => dest.Kategori, opt => opt.MapFrom(src => src.Kategori))
            .ForMember(dest => dest.ServiceTypeCode, opt => opt.MapFrom(src => src.ServiceTypeCode))
            .ForMember(dest => dest.KindCode, opt => opt.MapFrom(src => src.KindCode))
            .ForMember(dest => dest.KindDescription, opt => opt.MapFrom(src => src.KindDescription))
            .ForMember(dest => dest.MaxDuration, opt => opt.MapFrom(src => src.MaxDuration))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

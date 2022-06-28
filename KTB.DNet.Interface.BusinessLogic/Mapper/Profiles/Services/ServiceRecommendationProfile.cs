
#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ServiceBookingProfile class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region "Namespace Imports"
using AutoMapper;
using KTB.DNet.Domain;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.BusinessLogic.MapperBL
{
    public class ServiceRecommendationProfile : Profile
    {
        public ServiceRecommendationProfile()
        {
            CreateMap<ServiceRecommendation, ServiceRecommendationDto>()
                .ForMember(dest => dest.Tipe, opt => opt.MapFrom(src => src.Tipe))
                .ForMember(dest => dest.Rekomendasi, cfg =>
                    cfg.MapFrom(src => JsonConvert.DeserializeObject<List<ServiceRecommendationDetailDto>>(src.Rekomendasi)))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

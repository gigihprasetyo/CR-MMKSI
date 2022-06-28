#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_ServiceCostEstimationProfile mapper class
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
using KTB.DNet.Interface.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.BusinessLogic.MapperBL
{
    public class VWI_ServiceCostEstimationProfile : Profile   
    {
        public VWI_ServiceCostEstimationProfile()
        {
            CreateMap<VWI_ServiceCostEstimation, VWI_ServiceCostEstimationSummaryDto>()
                .ForMember(dest => dest.JasaService, opt => opt.MapFrom(src => src.JasaService))
                .ForMember(dest => dest.JenisService, opt => opt.MapFrom(src => src.JenisService))
                .ForMember(dest => dest.JenisKegiatan, opt => opt.MapFrom(src => src.JenisKegiatan))
                .ForMember(dest => dest.KindCode, opt => opt.MapFrom(src => src.KindCode))
                .ForMember(dest => dest.Details, cfg =>
                    cfg.MapFrom(src => JsonConvert.DeserializeObject<List<VWI_ServiceCostEstimationDetailDto>>(src.Details)))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

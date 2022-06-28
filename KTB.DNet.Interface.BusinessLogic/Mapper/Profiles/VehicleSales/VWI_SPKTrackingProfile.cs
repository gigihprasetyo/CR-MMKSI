#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_SPKTrackingProfile mapper class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using AutoMapper;
using KTB.DNet.Domain;
using KTB.DNet.Interface.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.BusinessLogic.MapperBL
{
    public class VWI_SPKTrackingProfile : Profile    
    {
        public VWI_SPKTrackingProfile() 
        {
            CreateMap<VWI_SPKTracking, VWI_SPKTrackingDto>()
            .ForMember(dest => dest.DealerSPKDate, cfg =>
                    cfg.MapFrom(src => src.DealerSPKDate.ToString("yyyy-MM-dd").Equals("1753-01-01") ? (DateTime?)null : src.DealerSPKDate))
            .ForMember(dest => dest.SPKDetail, cfg =>
                    cfg.MapFrom(src => JsonConvert.DeserializeObject<List<VWI_SPKTrackingDetailDto>>(src.SPKDetail)));
        }
    }
}

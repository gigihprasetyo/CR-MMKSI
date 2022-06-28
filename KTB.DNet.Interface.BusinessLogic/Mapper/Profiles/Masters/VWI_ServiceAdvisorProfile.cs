#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_ServiceAdvisorProfile mapper class
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
    public class VWI_ServiceAdvisorProfile : Profile
    {
        public VWI_ServiceAdvisorProfile()
        {
            CreateMap<VWI_ServiceAdvisor, VWI_ServiceAdvisorDto>()
            .ForMember(dest => dest.RegistrationNumber, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Photo, cfg =>
                    cfg.MapFrom(src => Convert.ToBase64String(src.Photo)));
        }
    }
}

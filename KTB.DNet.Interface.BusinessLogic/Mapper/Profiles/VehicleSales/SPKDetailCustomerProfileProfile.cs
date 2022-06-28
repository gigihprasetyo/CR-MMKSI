#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKDetailCustomerProfileProfile mapper class
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
using KTB.DNet.Interface.Model;
using DNetDomain = KTB.DNet.Domain;
#endregion

namespace KTB.DNet.Interface.BusinessLogic.MapperBL
{
    public class SPKDetailCustomerProfileProfile : Profile
    {
        public SPKDetailCustomerProfileProfile()
        {
            CreateMap<DNetDomain.SPKDetailCustomerProfile, SPKDetailCustomerProfileDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ProfileValue, opt => opt.MapFrom(src => src.ProfileValue))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LasUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SPKDetailCustomerProfileDto, DNetDomain.SPKDetailCustomerProfile>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ProfileValue, opt => opt.MapFrom(src => src.ProfileValue))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LasUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

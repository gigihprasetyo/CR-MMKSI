#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKProfileProfile mapper class
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
#endregion

namespace KTB.DNet.Interface.BusinessLogic.MapperBL
{
    public class SPKProfileProfile : Profile
    {
        public SPKProfileProfile()
        {
            CreateMap<SPKProfile, SPKProfileDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ProfileValue, opt => opt.MapFrom(src => src.ProfileValue))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SPKProfile, SPKProfileParameterDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ProfileValue, opt => opt.MapFrom(src => src.ProfileValue))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SPKProfileDto, SPKProfile>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ProfileValue, opt => opt.MapFrom(src => src.ProfileValue))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SPKProfileParameterDto, SPKProfile>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ProfileValue, opt => opt.MapFrom(src => src.ProfileValue))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForAllOtherMembers(opt => opt.Ignore());

        }
    }
}
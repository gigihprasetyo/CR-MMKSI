#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StandardCodeProfile mapper class
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
#endregion

namespace KTB.DNet.Interface.BusinessLogic.MapperBL
{
    public class StandardCodeProfile : Profile
    {
        public StandardCodeProfile()
        {
            CreateMap<StandardCode, StandardCodeDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.ValueId, opt => opt.MapFrom(src => src.ValueId))
            .ForMember(dest => dest.ValueCode, opt => opt.MapFrom(src => src.ValueCode))
            .ForMember(dest => dest.ValueDesc, opt => opt.MapFrom(src => src.ValueDesc))
            .ForMember(dest => dest.Sequence, opt => opt.MapFrom(src => src.Sequence))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<StandardCodeParameterDto, StandardCode>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.ValueId, opt => opt.MapFrom(src => src.ValueId))
            .ForMember(dest => dest.ValueCode, opt => opt.MapFrom(src => src.ValueCode))
            .ForMember(dest => dest.ValueDesc, opt => opt.MapFrom(src => src.ValueDesc))
            .ForMember(dest => dest.Sequence, opt => opt.MapFrom(src => src.Sequence))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

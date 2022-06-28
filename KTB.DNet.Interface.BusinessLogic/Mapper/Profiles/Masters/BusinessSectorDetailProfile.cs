﻿#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BusinessSectorDetailProfile mapper class
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
    public class BusinessSectorDetailProfile : Profile
    {
        public BusinessSectorDetailProfile()
        {
            CreateMap<BusinessSectorDetail, BusinessSectorDetailDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.BusinessDomain, opt => opt.MapFrom(src => src.BusinessDomain))
            .ForMember(dest => dest.BusinessSectorHeaderID, opt => opt.MapFrom(src => src.BusinessSectorHeader == null ? 0 : src.BusinessSectorHeader.ID))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<BusinessSectorDetailParameterDto, BusinessSectorDetail>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.BusinessDomain, opt => opt.MapFrom(src => src.BusinessDomain))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

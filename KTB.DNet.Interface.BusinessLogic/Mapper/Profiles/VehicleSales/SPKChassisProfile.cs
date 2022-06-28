#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKChassisProfile mapper class
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
    public class SPKChassisProfile : Profile
    {
        public SPKChassisProfile()
        {
            CreateMap<SPKChassis, SPKChassisDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.MatchingType, opt => opt.MapFrom(src => src.MatchingType))
            .ForMember(dest => dest.MatchingDate, opt => opt.MapFrom(src => src.MatchingDate))
            .ForMember(dest => dest.MatchingNumber, opt => opt.MapFrom(src => src.MatchingNumber))
            .ForMember(dest => dest.KeyNumber, opt => opt.MapFrom(src => src.KeyNumber))
            .ForMember(dest => dest.ReferenceNumber, opt => opt.MapFrom(src => src.ReferenceNumber))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SPKChassisParameterDto, SPKChassis>()
            .ForMember(dest => dest.MatchingType, opt => opt.MapFrom(src => src.MatchingType))
            .ForMember(dest => dest.MatchingDate, opt => opt.MapFrom(src => src.MatchingDate))
            .ForMember(dest => dest.MatchingNumber, opt => opt.MapFrom(src => src.MatchingNumber))
            .ForMember(dest => dest.ReferenceNumber, opt => opt.MapFrom(src => src.ReferenceNumber))
            .ForMember(dest => dest.KeyNumber, opt => opt.MapFrom(src => src.KeyNumber))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

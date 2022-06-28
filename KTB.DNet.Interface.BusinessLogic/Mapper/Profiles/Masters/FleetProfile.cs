#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : FleetProfile mapper class
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
    public class FleetProfile : Profile
    {
        public FleetProfile()
        {
            CreateMap<Fleet, FleetDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.FleetCode, opt => opt.MapFrom(src => src.FleetCode))
            .ForMember(dest => dest.FleetName, opt => opt.MapFrom(src => src.FleetName))
            .ForMember(dest => dest.FleetNickName, opt => opt.MapFrom(src => src.FleetNickName))
            .ForMember(dest => dest.FleetGroup, opt => opt.MapFrom(src => src.FleetGroup))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.IdentityType, opt => opt.MapFrom(src => src.IdentityType))
            .ForMember(dest => dest.IdentityNumber, opt => opt.MapFrom(src => src.IdentityNumber))
            .ForMember(dest => dest.FleetNote, opt => opt.MapFrom(src => src.FleetNote))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<FleetParameterDto, Fleet>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.FleetCode, opt => opt.MapFrom(src => src.FleetCode))
            .ForMember(dest => dest.FleetName, opt => opt.MapFrom(src => src.FleetName))
            .ForMember(dest => dest.FleetNickName, opt => opt.MapFrom(src => src.FleetNickName))
            .ForMember(dest => dest.FleetGroup, opt => opt.MapFrom(src => src.FleetGroup))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.IdentityType, opt => opt.MapFrom(src => src.IdentityType))
            .ForMember(dest => dest.IdentityNumber, opt => opt.MapFrom(src => src.IdentityNumber))
            .ForMember(dest => dest.FleetNote, opt => opt.MapFrom(src => src.FleetNote))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

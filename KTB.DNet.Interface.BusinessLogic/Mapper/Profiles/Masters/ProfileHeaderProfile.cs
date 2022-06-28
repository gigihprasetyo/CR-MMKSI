#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ProfileHeaderProfile mapper class
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
    public class ProfileHeaderProfile : Profile
    {
        public ProfileHeaderProfile()
        {
            CreateMap<ProfileHeader, ProfileHeaderDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.DataType, opt => opt.MapFrom(src => src.DataType))
                .ForMember(dest => dest.DataLength, opt => opt.MapFrom(src => src.DataLength))
                .ForMember(dest => dest.ControlType, opt => opt.MapFrom(src => src.ControlType))
                .ForMember(dest => dest.SelectionMode, opt => opt.MapFrom(src => src.SelectionMode))
                .ForMember(dest => dest.Mandatory, opt => opt.MapFrom(src => src.Mandatory))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<ProfileHeaderDto, ProfileHeader>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.DataType, opt => opt.MapFrom(src => src.DataType))
                .ForMember(dest => dest.DataLength, opt => opt.MapFrom(src => src.DataLength))
                .ForMember(dest => dest.ControlType, opt => opt.MapFrom(src => src.ControlType))
                .ForMember(dest => dest.SelectionMode, opt => opt.MapFrom(src => src.SelectionMode))
                .ForMember(dest => dest.Mandatory, opt => opt.MapFrom(src => src.Mandatory))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

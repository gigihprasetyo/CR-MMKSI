#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ProfileHeaderToGroupProfile mapper class
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
    public class ProfileHeaderToGroupProfile : Profile
    {
        public ProfileHeaderToGroupProfile()
        {
            CreateMap<ProfileHeaderToGroup, ProfileHeaderToGroupDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.ProfileValue, opt => opt.MapFrom(src => src.ProfileValue))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupID))
                .ForMember(dest => dest.ProfileYbsId, opt => opt.MapFrom(src => src.ProfileYbsId))
                .ForMember(dest => dest.ProfileHeaderId, opt => opt.MapFrom(src => src.ProfileHeaderId))
                .ForMember(dest => dest.ProfileGroup, opt => opt.MapFrom(src => src.ProfileGroup))
                .ForMember(dest => dest.ProfileHeader, opt => opt.MapFrom(src => src.ProfileHeader))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

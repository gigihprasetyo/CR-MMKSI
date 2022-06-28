#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : FieldFixListProfile mapper class
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
    public class FieldFixListProfile : Profile
    {
        public FieldFixListProfile()
        {
            CreateMap<VWI_FieldFixList, FieldFixListDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.ChassisNo, opt => opt.MapFrom(src => src.ChassisNo))
            .ForMember(dest => dest.RecallRegNo, opt => opt.MapFrom(src => src.RecallRegNo))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.BuletinDescription, opt => opt.MapFrom(src => src.BuletinDescription))
            .ForMember(dest => dest.ValidStartDate, opt => opt.MapFrom(src => src.ValidStartDate))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.DealerName, opt => opt.MapFrom(src => src.DealerName))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

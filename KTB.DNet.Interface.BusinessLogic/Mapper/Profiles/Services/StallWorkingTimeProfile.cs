
#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StallWorkingTimeProfile mapper class
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
    public class StallWorkingTimeProfile : Profile
    {
        public StallWorkingTimeProfile()
        {
            CreateMap<StallWorkingTimeParameterDto, StallWorkingTime>()
                .ForMember(dest => dest.Tanggal, opt => opt.MapFrom(src => src.Tanggal))
                .ForMember(dest => dest.TimeStart, opt => opt.MapFrom(src => src.TimeStart))
                .ForMember(dest => dest.TimeEnd, opt => opt.MapFrom(src => src.TimeEnd))
                .ForMember(dest => dest.RestTimeStart, opt => opt.MapFrom(src => src.RestTimeStart))
                .ForMember(dest => dest.RestTimeEnd, opt => opt.MapFrom(src => src.RestTimeEnd))
                .ForMember(dest => dest.IsHoliday, opt => opt.MapFrom(src => src.IsHoliday))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdatedTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<StallWorkingTimeUpdateParameterDto, StallWorkingTime>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Tanggal, opt => opt.MapFrom(src => src.Tanggal))
                .ForMember(dest => dest.TimeStart, opt => opt.MapFrom(src => src.TimeStart))
                .ForMember(dest => dest.TimeEnd, opt => opt.MapFrom(src => src.TimeEnd))
                .ForMember(dest => dest.RestTimeStart, opt => opt.MapFrom(src => src.RestTimeStart))
                .ForMember(dest => dest.RestTimeEnd, opt => opt.MapFrom(src => src.RestTimeEnd))
                .ForMember(dest => dest.IsHoliday, opt => opt.MapFrom(src => src.IsHoliday))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdatedTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());

        }
    }
}


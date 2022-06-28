
#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ServiceBookingActivityProfile class
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
    public class ServiceBookingActivityProfile : Profile
    {
        public ServiceBookingActivityProfile()
        {
            CreateMap<ServiceBookingActivityParameterDto, ServiceBookingActivity>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ServiceTypeID, opt => opt.MapFrom(src => src.ServiceTypeID))
                .ForMember(dest => dest.KindCode, opt => opt.MapFrom(src => src.KindCode))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdatedTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<ServiceBookingActivityRealtimeParameterDto, ServiceBookingActivity>()
                .ForMember(dest => dest.ServiceTypeID, opt => opt.MapFrom(src => src.ServiceTypeID))
                .ForMember(dest => dest.KindCode, opt => opt.MapFrom(src => src.KindCode))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}


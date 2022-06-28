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
    public class ServiceReminderFollowUpProfile : Profile
    {
        public ServiceReminderFollowUpProfile()
        {
            CreateMap<ServiceReminderFollowUp, ServiceReminderFollowUpDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.ServiceReminderID, opt => opt.MapFrom(src => src.ServiceReminder.ID))
            .ForMember(dest => dest.FollowUpStatus, opt => opt.MapFrom(src => src.FollowUpStatus))
            .ForMember(dest => dest.FollowUpAction, opt => opt.MapFrom(src => src.FollowUpAction))
            .ForMember(dest => dest.FollowUpDate, opt => opt.MapFrom(src => src.FollowUpDate))
            .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.BookingDate))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<ServiceReminderFollowUpParameterDto, ServiceReminderFollowUp>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.ServiceReminder, opt => opt.MapFrom(src => src.ServiceReminderID))
            .ForMember(dest => dest.FollowUpStatus, opt => opt.MapFrom(src => src.FollowUpStatus))
            .ForMember(dest => dest.FollowUpAction, opt => opt.MapFrom(src => src.FollowUpAction))
            .ForMember(dest => dest.FollowUpDate, opt => opt.MapFrom(src => src.FollowUpDate))
            .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.BookingDate))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

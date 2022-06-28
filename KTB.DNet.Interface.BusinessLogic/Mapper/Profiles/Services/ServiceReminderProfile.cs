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
    public class ServiceReminderProfile : Profile
    {
        public ServiceReminderProfile()
        {
            CreateMap<ServiceReminder, VWI_ServiceReminderDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.SalesforceID, opt => opt.MapFrom(src => src.SalesforceID))
            .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
            .ForMember(dest => dest.EngineNumber, opt => opt.MapFrom(src => src.EngineNumber))
            .ForMember(dest => dest.WONumber, opt => opt.MapFrom(src => src.WONumber))
            .ForMember(dest => dest.ServiceReminderDate, opt => opt.MapFrom(src => src.ServiceReminderDate))
            .ForMember(dest => dest.MaxFUDealerDate, opt => opt.MapFrom(src => src.MaxFUDealerDate))
            .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.BookingDate))
            .ForMember(dest => dest.BookingTime, opt => opt.MapFrom(src => src.BookingTime))
            .ForMember(dest => dest.CaseNumber, opt => opt.MapFrom(src => src.CaseNumber))
            .ForMember(dest => dest.AssistServiceIncomingID, opt => opt.MapFrom(src => src.AssistServiceIncoming.ID))
            .ForMember(dest => dest.ServiceActualDate, opt => opt.MapFrom(src => src.ServiceActualDate))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
            .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.CustomerPhoneNumber))
            .ForMember(dest => dest.ContactPersonName, opt => opt.MapFrom(src => src.ContactPersonName))
            .ForMember(dest => dest.ContactPersonPhoneNumber, opt => opt.MapFrom(src => src.ContactPersonPhoneNumber))
            .ForMember(dest => dest.ServiceType, opt => opt.MapFrom(src => src.PMKind.KindCode))
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType))
            .ForMember(dest => dest.ActualKM, opt => opt.MapFrom(src => src.ActualKM))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForAllOtherMembers(opt => opt.Ignore());
    }
    }
}

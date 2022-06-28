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
    public class MobileServiceReminderProfile : Profile
    {
        public MobileServiceReminderProfile()
        {
            CreateMap<VWI_MobileServiceReminder, VWI_MobileServiceReminderDto>()
               .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
          .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
          .ForMember(dest => dest.KindCode, opt => opt.MapFrom(src => src.KindCode))
          .ForMember(dest => dest.KindDescription, opt => opt.MapFrom(src => src.KindDescription))
          .ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.Remark))
          .ForMember(dest => dest.ReminderDelta, opt => opt.MapFrom(src => src.ReminderDelta))
          .ForMember(dest => dest.ReminderType, opt => opt.MapFrom(src => src.ReminderType))
          .ForMember(dest => dest.ServiceReminderDate, opt => opt.MapFrom(src => src.ServiceReminderDate))
          .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

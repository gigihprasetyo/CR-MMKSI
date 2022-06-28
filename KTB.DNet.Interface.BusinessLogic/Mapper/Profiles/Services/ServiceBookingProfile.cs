
#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ServiceBookingProfile class
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
using Newtonsoft.Json;
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.BusinessLogic.MapperBL
{
    public class ServiceBookingProfile : Profile
    {
        public ServiceBookingProfile()
        {
            CreateMap<ServiceBooking, ServiceBookingDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.DealerID, opt => opt.MapFrom(src => src.Dealer.ID))
                //.ForMember(dest => dest.ChassisMasterID, opt => opt.MapFrom(src => src.ChassisMaster.ID))
                .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
                //.ForMember(dest => dest.VechileModelID, opt => opt.MapFrom(src => src.VechileModel.ID))
                .ForMember(dest => dest.PlateNumber, opt => opt.MapFrom(src => src.PlateNumber))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.CustomerPhoneNumber))
                .ForMember(dest => dest.Odometer, opt => opt.MapFrom(src => src.OdoMeter))
                //.ForMember(dest => dest.ServiceTypeID, opt => opt.MapFrom(src => src.ServiceTypeID))
                //.ForMember(dest => dest.KindCode, opt => opt.MapFrom(src => src.KindCode))
                .ForMember(dest => dest.StallMasterID, opt => opt.MapFrom(src => src.StallMaster.ID))
                .ForMember(dest => dest.IncomingDateStart, opt => opt.MapFrom(src => src.IncomingDateStart))
                .ForMember(dest => dest.IncomingDateEnd, opt => opt.MapFrom(src => src.IncomingDateEnd))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.PickupType, opt => opt.MapFrom(src => src.PickupType))
                .ForMember(dest => dest.StallServiceType, opt => opt.MapFrom(src => src.StallServiceType))
                .ForMember(dest => dest.IsMitsubishi, opt => opt.MapFrom(src => src.IsMitsubishi))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.servicebookingactivity, opt => opt.MapFrom(src => src.ServiceBookingActivities))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdatedBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdatedTime))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<ServiceBookingParameterDto, ServiceBooking>()
                .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
                .ForMember(dest => dest.PlateNumber, opt => opt.MapFrom(src => src.PlateNumber))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.CustomerPhoneNumber))
                .ForMember(dest => dest.OdoMeter, opt => opt.MapFrom(src => src.Odometer))
                .ForMember(dest => dest.ServiceBookingCode, opt => opt.MapFrom(src => src.ServiceBookingCode))
                //.ForMember(dest => dest.ServiceTypeID, opt => opt.MapFrom(src => src.ServiceType))
                //.ForMember(dest => dest.KindCode, opt => opt.MapFrom(src => src.KindCode))
                .ForMember(dest => dest.IncomingDateStart, opt => opt.MapFrom(src => src.IncomingDateStart))
                .ForMember(dest => dest.IncomingDateEnd, opt => opt.MapFrom(src => src.IncomingDateEnd))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.PickupType, opt => opt.MapFrom(src => src.PickupType))
                .ForMember(dest => dest.StallServiceType, opt => opt.MapFrom(src => src.StallServiceType))
                .ForMember(dest => dest.IsMitsubishi, opt => opt.MapFrom(src => src.IsMitsubishi))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdatedTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<ServiceBookingUpdateParameterDto, ServiceBooking>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
                .ForMember(dest => dest.PlateNumber, opt => opt.MapFrom(src => src.PlateNumber))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.CustomerPhoneNumber))
                .ForMember(dest => dest.OdoMeter, opt => opt.MapFrom(src => src.Odometer))
                //.ForMember(dest => dest.ServiceTypeID, opt => opt.MapFrom(src => src.ServiceType))
                //.ForMember(dest => dest.KindCode, opt => opt.MapFrom(src => src.KindCode))
                .ForMember(dest => dest.IncomingDateStart, opt => opt.MapFrom(src => src.IncomingDateStart))
                .ForMember(dest => dest.IncomingDateEnd, opt => opt.MapFrom(src => src.IncomingDateEnd))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.PickupType, opt => opt.MapFrom(src => src.PickupType))
                .ForMember(dest => dest.StallServiceType, opt => opt.MapFrom(src => src.StallServiceType))
                .ForMember(dest => dest.IsMitsubishi, opt => opt.MapFrom(src => src.IsMitsubishi))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdatedTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<ServiceBookingRealtimeParameterDto, ServiceBooking>()
                .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
                .ForMember(dest => dest.PlateNumber, opt => opt.MapFrom(src => src.PlateNumber))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.CustomerPhoneNumber))
                .ForMember(dest => dest.OdoMeter, opt => opt.MapFrom(src => src.Odometer))
                .ForMember(dest => dest.ServiceBookingCode, opt => opt.MapFrom(src => src.ServiceBookingCode))
                .ForMember(dest => dest.WorkingTimeStart, opt => opt.MapFrom(src => src.WorkingTimeStart))
                .ForMember(dest => dest.WorkingTimeEnd, opt => opt.MapFrom(src => src.WorkingTimeEnd))
                .ForMember(dest => dest.IncomingDateStart, opt => opt.MapFrom(src => src.IncomingDateStart))
                .ForMember(dest => dest.IncomingDateEnd, opt => opt.MapFrom(src => src.IncomingDateEnd))
                .ForMember(dest => dest.IsMitsubishi, opt => opt.MapFrom(src => src.IsMitsubishi))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<ServiceBooking, ServiceBookingRealtimeDto>()
                .ForMember(dest => dest.ServiceBookingCode, opt => opt.MapFrom(src => src.ServiceBookingCode))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<VWI_ServiceBooking, ServiceBookingRealtimeReadDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ServiceBookingCode, opt => opt.MapFrom(src => src.ServiceBookingCode))
                .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
                .ForMember(dest => dest.DealerName, opt => opt.MapFrom(src => src.DealerName))
                .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
                .ForMember(dest => dest.StallMasterID, opt => opt.MapFrom(src => src.StallMasterID))
                .ForMember(dest => dest.StallCode, opt => opt.MapFrom(src => src.StallCode))
                .ForMember(dest => dest.VehicleTypeCode, opt => opt.MapFrom(src => src.VehicleTypeCode))
                .ForMember(dest => dest.VechileModelDescription, opt => opt.MapFrom(src => src.VechileModelDescription))
                .ForMember(dest => dest.VechileTypeDescription, opt => opt.MapFrom(src => src.VechileTypeDescription))
                .ForMember(dest => dest.PlateNumber, opt => opt.MapFrom(src => src.PlateNumber))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.CustomerPhoneNumber))
                .ForMember(dest => dest.Odometer, opt => opt.MapFrom(src => src.Odometer))
                .ForMember(dest => dest.ServiceAdvisorID, opt => opt.MapFrom(src => src.ServiceAdvisorID))
                .ForMember(dest => dest.ServiceAdvisorName, opt => opt.MapFrom(src => src.ServiceAdvisorName))
                .ForMember(dest => dest.IncomingPlan, opt => opt.MapFrom(src => src.IncomingPlan))
                .ForMember(dest => dest.StallServiceType, opt => opt.MapFrom(src => src.StallServiceType))
                .ForMember(dest => dest.StandardTime, opt => opt.MapFrom(src => src.StandardTime))
                .ForMember(dest => dest.IncomingDateStart, opt => opt.MapFrom(src => src.IncomingDateStart))
                .ForMember(dest => dest.IncomingDateEnd, opt => opt.MapFrom(src => src.IncomingDateEnd))
                .ForMember(dest => dest.WorkingTimeStart, opt => opt.MapFrom(src => src.WorkingTimeStart))
                .ForMember(dest => dest.WorkingTimeEnd, opt => opt.MapFrom(src => src.WorkingTimeEnd))
                .ForMember(dest => dest.IsMitsubishi, opt => opt.MapFrom(src => src.IsMitsubishi))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.ServiceBookingActivities, cfg =>
                    cfg.MapFrom(src => JsonConvert.DeserializeObject<List<ServiceBookingActivityRealtimeReadDto>>(src.ServiceBookingActivities)))
                .ForMember(dest => dest.LastUpdatedTime, opt => opt.MapFrom(src => src.LastUpdatedTime))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<KTB.DNet.Interface.Domain.ServiceBookingRealtimeAll_IF, ServiceBookingRealtimeReadDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ServiceBookingCode, opt => opt.MapFrom(src => src.ServiceBookingCode))
                .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
                .ForMember(dest => dest.DealerName, opt => opt.MapFrom(src => src.DealerName))
                .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
                .ForMember(dest => dest.StallMasterID, opt => opt.MapFrom(src => src.StallMasterID))
                .ForMember(dest => dest.StallCode, opt => opt.MapFrom(src => src.StallCode))
                .ForMember(dest => dest.VehicleTypeCode, opt => opt.MapFrom(src => src.VehicleTypeCode))
                .ForMember(dest => dest.VechileModelDescription, opt => opt.MapFrom(src => src.VechileModelDescription))
                .ForMember(dest => dest.VechileTypeDescription, opt => opt.MapFrom(src => src.VechileTypeDescription))
                .ForMember(dest => dest.PlateNumber, opt => opt.MapFrom(src => src.PlateNumber))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.CustomerPhoneNumber))
                .ForMember(dest => dest.Odometer, opt => opt.MapFrom(src => src.Odometer))
                .ForMember(dest => dest.ServiceAdvisorID, opt => opt.MapFrom(src => src.ServiceAdvisorID))
                .ForMember(dest => dest.ServiceAdvisorName, opt => opt.MapFrom(src => src.ServiceAdvisorName))
                .ForMember(dest => dest.IncomingPlan, opt => opt.MapFrom(src => src.IncomingPlan))
                .ForMember(dest => dest.StallServiceType, opt => opt.MapFrom(src => src.StallServiceType))
                .ForMember(dest => dest.StandardTime, opt => opt.MapFrom(src => src.StandardTime))
                .ForMember(dest => dest.IncomingDateStart, opt => opt.MapFrom(src => src.IncomingDateStart))
                .ForMember(dest => dest.IncomingDateEnd, opt => opt.MapFrom(src => src.IncomingDateEnd))
                .ForMember(dest => dest.WorkingTimeStart, opt => opt.MapFrom(src => src.WorkingTimeStart))
                .ForMember(dest => dest.WorkingTimeEnd, opt => opt.MapFrom(src => src.WorkingTimeEnd))
                .ForMember(dest => dest.IsMitsubishi, opt => opt.MapFrom(src => src.IsMitsubishi))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.ServiceBookingActivities, cfg =>
                    cfg.MapFrom(src => JsonConvert.DeserializeObject<List<ServiceBookingActivityRealtimeReadDto>>(src.ServiceBookingActivities)))
                .ForMember(dest => dest.LastUpdatedTime, opt => opt.MapFrom(src => src.LastUpdatedTime))
                .ForAllOtherMembers(opt => opt.Ignore());

        }
    }
}


#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKDetailProfile mapper class
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
    public class SPKDetailProfile : Profile
    {
        public SPKDetailProfile()
        {
            CreateMap<SPKDetail, SPKDetailDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                //.ForMember(dest => dest.VehicleColorCode, opt => opt.MapFrom(src => src.VehicleColorCode))
                //.ForMember(dest => dest.VehicleTypeCode, opt => opt.MapFrom(src => src.VehicleTypeCode))
                .ForMember(dest => dest.SPKDetailCustomers, opt => opt.MapFrom(src => src.SPKDetailCustomers))
                .ForAllOtherMembers(opt => opt.Ignore());


            CreateMap<SPKDetailParameterDto, SPKDetail>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.LineItem, opt => opt.MapFrom(src => src.LineItem))
                .ForMember(dest => dest.VehicleTypeCode, opt => opt.MapFrom(src => src.VehicleTypeCode))
                .ForMember(dest => dest.VehicleColorCode, opt => opt.MapFrom(src => src.VehicleColorCode))
                .ForMember(dest => dest.Additional, opt => opt.MapFrom(src => src.Additional))
                .ForMember(dest => dest.Remarks, opt => opt.MapFrom(src => src.Remarks))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.RejectedReason, opt => opt.MapFrom(src => src.RejectedReason))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))

                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))

                .ForAllOtherMembers(opt => opt.Ignore());

        }
    }
}

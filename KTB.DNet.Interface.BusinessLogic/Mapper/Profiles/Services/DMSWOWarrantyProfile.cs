#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DMSWOWarrantyProfile mapper class
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
    public class DMSWOWarrantyClaimProfile : Profile
    {
        public DMSWOWarrantyClaimProfile()
        {
            CreateMap<DMSWOWarrantyClaim, DMSWOWarrantyDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
            .ForMember(dest => dest.isBB, opt => opt.MapFrom(src => src.isBB))
            .ForMember(dest => dest.WorkOrderNumber, opt => opt.MapFrom(src => src.WorkOrderNumber))
            .ForMember(dest => dest.FailureDate, opt => opt.MapFrom(src => src.FailureDate))
            .ForMember(dest => dest.ServiceDate, opt => opt.MapFrom(src => src.ServiceDate))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Mileage))
            .ForMember(dest => dest.ServiceBuletin, opt => opt.MapFrom(src => src.ServiceBuletin))
            .ForMember(dest => dest.Symptoms, opt => opt.MapFrom(src => src.Symptoms))
            .ForMember(dest => dest.Causes, opt => opt.MapFrom(src => src.Causes))
            .ForMember(dest => dest.Results, opt => opt.MapFrom(src => src.Results))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreateBy, opt => opt.MapFrom(src => src.CreateBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<DMSWOWarrantyParameterDto, DMSWOWarrantyClaim>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
            .ForMember(dest => dest.isBB, opt => opt.MapFrom(src => src.isBB))
            .ForMember(dest => dest.WorkOrderNumber, opt => opt.MapFrom(src => src.WorkOrderNumber))
            .ForMember(dest => dest.FailureDate, opt => opt.MapFrom(src => src.FailureDate))
            .ForMember(dest => dest.ServiceDate, opt => opt.MapFrom(src => src.ServiceDate))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Mileage))
            .ForMember(dest => dest.ServiceBuletin, opt => opt.MapFrom(src => src.ServiceBuletin))
            .ForMember(dest => dest.Symptoms, opt => opt.MapFrom(src => src.Symptoms))
            .ForMember(dest => dest.Causes, opt => opt.MapFrom(src => src.Causes))
            .ForMember(dest => dest.Results, opt => opt.MapFrom(src => src.Results))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreateBy, opt => opt.MapFrom(src => src.CreateBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

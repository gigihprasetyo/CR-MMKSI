#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKHeaderProfile mapper class
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
using System;
#endregion

namespace KTB.DNet.Interface.BusinessLogic.MapperBL
{
    public class SPKHeaderProfile : Profile
    {
        public SPKHeaderProfile()
        {
            // SPKHeaderParameterDto Mapper
            CreateMap<SPKHeaderParameterDto, SPKHeader>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.DealerSPKNumber, opt => opt.MapFrom(src => src.DealerSPKNumber))
                .ForMember(dest => dest.RejectedReason, opt => opt.MapFrom(src => src.RejectedReason))
                .ForMember(dest => dest.DealerSPKDate, opt => opt.MapFrom(src => src.DealerSPKDate))
                .ForMember(dest => dest.SPKReferenceNumber, opt => opt.MapFrom(src => src.SPKReferenceNumber))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.Condition(src => !string.IsNullOrEmpty(src.CreatedBy)))
                .ForMember(dest => dest.CreatedTime, opt => opt.Condition(src => src.CreatedTime != DateTime.MinValue))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());

            // SPKHeader Mapper
            CreateMap<SPKHeader, SPKHeaderDto>()
                .ForMember(dest => dest.SPKNumber, opt => opt.MapFrom(src => src.SPKNumber))
                .ForMember(dest => dest.SPKDetails, opt => opt.MapFrom(src => src.SPKDetails))
                .ForMember(dest => dest.SPKCustomer, opt => opt.MapFrom(src => src.SPKCustomer))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForAllOtherMembers(opt => opt.Ignore());

            // Copy SPKHeader
            CreateMap<SPKHeader, SPKHeader>();
            
        }
    }
}

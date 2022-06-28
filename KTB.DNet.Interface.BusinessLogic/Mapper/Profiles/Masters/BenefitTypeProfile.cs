#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BenefitTypeProfile mapper class
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
    public class BenefitTypeProfile : Profile
    {
        public BenefitTypeProfile()
        {
            CreateMap<BenefitType, BenefitTypeDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.LeasingBox, opt => opt.MapFrom(src => src.LeasingBox))
            .ForMember(dest => dest.AssyYearBox, opt => opt.MapFrom(src => src.AssyYearBox))
            .ForMember(dest => dest.ReceiptBox, opt => opt.MapFrom(src => src.ReceiptBox))
            .ForMember(dest => dest.EventValidation, opt => opt.MapFrom(src => src.EventValidation))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForMember(dest => dest.WSDiscount, opt => opt.MapFrom(src => src.WSDiscount))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<BenefitTypeParameterDto, BenefitType>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.LeasingBox, opt => opt.MapFrom(src => src.LeasingBox))
            .ForMember(dest => dest.AssyYearBox, opt => opt.MapFrom(src => src.AssyYearBox))
            .ForMember(dest => dest.ReceiptBox, opt => opt.MapFrom(src => src.ReceiptBox))
            .ForMember(dest => dest.EventValidation, opt => opt.MapFrom(src => src.EventValidation))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForMember(dest => dest.WSDiscount, opt => opt.MapFrom(src => src.WSDiscount))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

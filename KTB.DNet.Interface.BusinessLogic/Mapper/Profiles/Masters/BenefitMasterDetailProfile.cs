#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BenefitMasterDetailProfile mapper class
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
    public class BenefitMasterDetailProfile : Profile
    {
        public BenefitMasterDetailProfile()
        {
            CreateMap<BenefitMasterDetail, BenefitMasterDetailDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.FormulaID, opt => opt.MapFrom(src => src.FormulaID))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.FakturValidationStart, opt => opt.MapFrom(src => src.FakturValidationStart))
            .ForMember(dest => dest.FakturValidationEnd, opt => opt.MapFrom(src => src.FakturValidationEnd))
            .ForMember(dest => dest.FakturOpenStart, opt => opt.MapFrom(src => src.FakturOpenStart))
            .ForMember(dest => dest.FakturOpenEnd, opt => opt.MapFrom(src => src.FakturOpenEnd))
            .ForMember(dest => dest.AssyYear, opt => opt.MapFrom(src => src.AssyYear))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForMember(dest => dest.MaxClaim, opt => opt.MapFrom(src => src.MaxClaim))
            .ForMember(dest => dest.WSDiscount, opt => opt.MapFrom(src => src.WSDiscount))
            .ForMember(dest => dest.BenefitType, opt => opt.MapFrom(src => src.BenefitType))
            .ForMember(dest => dest.BenefitMasterHeader, opt => opt.MapFrom(src => src.BenefitMasterHeader))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<BenefitMasterDetailParameterDto, BenefitMasterDetail>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.FormulaID, opt => opt.MapFrom(src => src.FormulaID))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.FakturValidationStart, opt => opt.MapFrom(src => src.FakturValidationStart))
            .ForMember(dest => dest.FakturValidationEnd, opt => opt.MapFrom(src => src.FakturValidationEnd))
            .ForMember(dest => dest.FakturOpenStart, opt => opt.MapFrom(src => src.FakturOpenStart))
            .ForMember(dest => dest.FakturOpenEnd, opt => opt.MapFrom(src => src.FakturOpenEnd))
            .ForMember(dest => dest.AssyYear, opt => opt.MapFrom(src => src.AssyYear))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForMember(dest => dest.MaxClaim, opt => opt.MapFrom(src => src.MaxClaim))
            .ForMember(dest => dest.WSDiscount, opt => opt.MapFrom(src => src.WSDiscount))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

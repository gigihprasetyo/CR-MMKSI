#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPOEstimateProfile mapper class
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
    public class SparePartPOEstimateProfile : Profile
    {
        public SparePartPOEstimateProfile()
        {
            CreateMap<SparePartPOEstimate, SparePartPOEstimateDto>()
                //.ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.SONumber, opt => opt.MapFrom(src => src.SONumber))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.SparePartPO.Dealer != null ? src.SparePartPO.Dealer.DealerCode : ""))
            .ForMember(dest => dest.DMSPRNo, opt => opt.MapFrom(src => src.SparePartPO.DMSPRNo))
            .ForMember(dest => dest.PONumber, opt => opt.MapFrom(src => src.SparePartPO.PONumber))
            .ForMember(dest => dest.SODate, opt => opt.MapFrom(src => src.SODate))
                //.ForMember(dest => dest.DeliveryDate, opt => opt.MapFrom(src => src.DeliveryDate))
            .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SparePartPOEstimateParameterDto, SparePartPOEstimate>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.SONumber, opt => opt.MapFrom(src => src.SONumber))
            .ForMember(dest => dest.SODate, opt => opt.MapFrom(src => src.SODate))
                //.ForMember(dest => dest.DeliveryDate, opt => opt.MapFrom(src => src.DeliveryDate))
            .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<VWI_POEstimateHaveBilling, SparePartPOEstimateDto>()
            .ForMember(dest => dest.SONumber, opt => opt.MapFrom(src => src.SONumber))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.DMSPRNo, opt => opt.MapFrom(src => src.DMSPRNo))
            .ForMember(dest => dest.PONumber, opt => opt.MapFrom(src => src.PONumber))
            .ForMember(dest => dest.SODate, opt => opt.MapFrom(src => src.SODate))
            .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType))
            .ForMember(dest => dest.TermOfPaymentValue, opt => opt.MapFrom(src => src.TermOfPaymentValue))
            .ForMember(dest => dest.TermOfPaymentCode, opt => opt.MapFrom(src => src.TermOfPaymentCode))
            .ForMember(dest => dest.TermOfPaymentDesc, opt => opt.MapFrom(src => src.TermOfPaymentDesc))
            .ForMember(dest => dest.AmountC2, opt => opt.MapFrom(src => src.AmountC2))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<VWI_POEstimateHaveBillingOne, SparePartPOEstimateDto>();
        }
    }
}

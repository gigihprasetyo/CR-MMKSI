#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartDOProfile mapper class
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
    public class SparePartDOProfile : Profile
    {
        public SparePartDOProfile()
        {
            CreateMap<SparePartDO, SparePartDODto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Dealer, opt => opt.MapFrom(src => src.Dealer))
            .ForMember(dest => dest.DONumber, opt => opt.MapFrom(src => src.DONumber))
            .ForMember(dest => dest.DoDate, opt => opt.MapFrom(src => src.DoDate))
            .ForMember(dest => dest.EstmationDeliveryDate, opt => opt.MapFrom(src => src.EstmationDeliveryDate))
            .ForMember(dest => dest.PickingDate, opt => opt.MapFrom(src => src.PickingDate))
            .ForMember(dest => dest.PackingDate, opt => opt.MapFrom(src => src.PackingDate))
            .ForMember(dest => dest.GoodIssueDate, opt => opt.MapFrom(src => src.GoodIssueDate))
            .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate))
            .ForMember(dest => dest.ReadyForDeliveryDate, opt => opt.MapFrom(src => src.ReadyForDeliveryDate))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SparePartDOParameterDto, SparePartDO>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.DONumber, opt => opt.MapFrom(src => src.DONumber))
            .ForMember(dest => dest.DoDate, opt => opt.MapFrom(src => src.DoDate))
            .ForMember(dest => dest.EstmationDeliveryDate, opt => opt.MapFrom(src => src.EstmationDeliveryDate))
            .ForMember(dest => dest.PickingDate, opt => opt.MapFrom(src => src.PickingDate))
            .ForMember(dest => dest.PackingDate, opt => opt.MapFrom(src => src.PackingDate))
            .ForMember(dest => dest.GoodIssueDate, opt => opt.MapFrom(src => src.GoodIssueDate))
            .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate))
            .ForMember(dest => dest.ReadyForDeliveryDate, opt => opt.MapFrom(src => src.ReadyForDeliveryDate))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<VWI_SparePartPODOHaveBilling, DeliveryOrderBillingResponseDto>()
            .ForMember(dest => dest.BillingDate, opt => opt.MapFrom(src => src.BillingDate))
            .ForMember(dest => dest.BillingNumber, opt => opt.MapFrom(src => src.BillingNumber))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.DODate, opt => opt.MapFrom(src => src.DoDate))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.DONumber, opt => opt.MapFrom(src => src.DONumber))
            .ForMember(dest => dest.ExpeditionNumber, opt => opt.MapFrom(src => src.ExpeditionNo))
            .ForMember(dest => dest.TermOfPaymentValue, opt => opt.MapFrom(src => src.TermOfPaymentValue))
            .ForMember(dest => dest.TermOfPaymentCode, opt => opt.MapFrom(src => src.TermOfPaymentCode))
            .ForMember(dest => dest.TermOfPaymentDesc, opt => opt.MapFrom(src => src.TermOfPaymentDesc))
            .ForMember(dest => dest.AmountC2, opt => opt.MapFrom(src => src.AmountC2))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<VWI_SparePartPODOHaveBillingOne, DeliveryOrderBillingResponseDto>()
            .ForMember(dest => dest.BillingDate, opt => opt.MapFrom(src => src.BillingDate))
            .ForMember(dest => dest.BillingNumber, opt => opt.MapFrom(src => src.BillingNumber))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.DODate, opt => opt.MapFrom(src => src.DoDate))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.DONumber, opt => opt.MapFrom(src => src.DONumber))
            .ForMember(dest => dest.ExpeditionNumber, opt => opt.MapFrom(src => src.ExpeditionNo))
            .ForMember(dest => dest.TermOfPaymentValue, opt => opt.MapFrom(src => src.TermOfPaymentValue))
            .ForMember(dest => dest.TermOfPaymentCode, opt => opt.MapFrom(src => src.TermOfPaymentCode))
            .ForMember(dest => dest.TermOfPaymentDesc, opt => opt.MapFrom(src => src.TermOfPaymentDesc))
            .ForMember(dest => dest.AmountC2, opt => opt.MapFrom(src => src.AmountC2))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<VWI_SparePartPODOHaveBillingHeaderDetail, DeliveryOrderBillingResponseDto>()
            .ForMember(dest => dest.BillingDate, opt => opt.MapFrom(src => src.BillingDate))
            .ForMember(dest => dest.BillingNumber, opt => opt.MapFrom(src => src.BillingNumber))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.DODate, opt => opt.MapFrom(src => src.DoDate))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.DONumber, opt => opt.MapFrom(src => src.DONumber))
            .ForMember(dest => dest.ExpeditionNumber, opt => opt.MapFrom(src => src.ExpeditionNo))
            .ForMember(dest => dest.TermOfPaymentValue, opt => opt.MapFrom(src => src.TermOfPaymentValue))
            .ForMember(dest => dest.TermOfPaymentCode, opt => opt.MapFrom(src => src.TermOfPaymentCode))
            .ForMember(dest => dest.TermOfPaymentDesc, opt => opt.MapFrom(src => src.TermOfPaymentDesc))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

        }
    }
}

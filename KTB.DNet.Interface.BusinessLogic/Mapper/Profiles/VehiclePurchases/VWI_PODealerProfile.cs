#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_PODealerProfile mapper class
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
    public class VWI_PODealerProfile : Profile
    {
        public VWI_PODealerProfile()
        {
            CreateMap<VWI_PODealer, VWI_PODealerDto>()
            .ForMember(dest => dest.POHeaderId, opt => opt.MapFrom(src => src.POHeaderId))
            .ForMember(dest => dest.DealerId, opt => opt.MapFrom(src => src.DealerId))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.DealerName, opt => opt.MapFrom(src => src.DealerName))
            .ForMember(dest => dest.PONumber, opt => opt.MapFrom(src => src.PONumber))
            .ForMember(dest => dest.POType, opt => opt.MapFrom(src => src.POType))
            .ForMember(dest => dest.NumOfInstallment, opt => opt.MapFrom(src => src.NumOfInstallment))
            .ForMember(dest => dest.AllocQty, opt => opt.MapFrom(src => src.AllocQty))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.Interest, opt => opt.MapFrom(src => src.Interest))
            .ForMember(dest => dest.ContractNumber, opt => opt.MapFrom(src => src.ContractNumber))
            .ForMember(dest => dest.PKNumber, opt => opt.MapFrom(src => src.PKNumber))
            .ForMember(dest => dest.DealerPKNumber, opt => opt.MapFrom(src => src.DealerPKNumber))
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.ProjectName))
            .ForMember(dest => dest.SalesOrderId, opt => opt.MapFrom(src => src.SalesOrderId))
            .ForMember(dest => dest.SONumber, opt => opt.MapFrom(src => src.SONumber))
            .ForMember(dest => dest.SODate, opt => opt.MapFrom(src => src.SODate))
            .ForMember(dest => dest.PaymentRef, opt => opt.MapFrom(src => src.PaymentRef))
            .ForMember(dest => dest.SOType, opt => opt.MapFrom(src => src.SOType))
            .ForMember(dest => dest.TermOfPaymentCode, opt => opt.MapFrom(src => src.TermOfPaymentCode))
            .ForMember(dest => dest.TOPDescription, opt => opt.MapFrom(src => src.TOPDescription))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForMember(dest => dest.VehicleColorCode, opt => opt.MapFrom(src => src.VehicleColorCode))
            .ForMember(dest => dest.VehicleTypeCode, opt => opt.MapFrom(src => src.VehicleTypeCode))
            .ForMember(dest => dest.MaterialNumber, opt => opt.MapFrom(src => src.MaterialNumber))
            .ForMember(dest => dest.MaterialDescription, opt => opt.MapFrom(src => src.MaterialDescription))
            .ForMember(dest => dest.BasePrice, opt => opt.MapFrom(src => src.BasePrice))
            .ForMember(dest => dest.OptionPrice, opt => opt.MapFrom(src => src.OptionPrice))
            .ForMember(dest => dest.DiscountBeforeTax, opt => opt.MapFrom(src => src.DiscountBeforeTax))
            .ForMember(dest => dest.NetPrice, opt => opt.MapFrom(src => src.NetPrice))
            .ForMember(dest => dest.TotalHarga, opt => opt.MapFrom(src => src.TotalHarga))
            .ForMember(dest => dest.PPN, opt => opt.MapFrom(src => src.PPN))
            .ForMember(dest => dest.TotalHargaPPN, opt => opt.MapFrom(src => src.TotalHargaPPN))
            .ForMember(dest => dest.TotalHargaPP, opt => opt.MapFrom(src => src.TotalHargaPP))
            .ForMember(dest => dest.TotalHargaLC, opt => opt.MapFrom(src => src.TotalHargaLC))
            .ForMember(dest => dest.TotalDeposit, opt => opt.MapFrom(src => src.TotalDeposit))
            .ForMember(dest => dest.TotalInterest, opt => opt.MapFrom(src => src.TotalInterest))
            .ForMember(dest => dest.SPLNumber, opt => opt.MapFrom(src => src.SPLNumber))
            .ForMember(dest => dest.EffectiveDate, opt => opt.MapFrom(src => src.EffectiveDate))
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
            .ForMember(dest => dest.ETDDate, opt => opt.MapFrom(src => src.ETDDate))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<VWI_PODealerParameterDto, VWI_PODealer>()
            .ForMember(dest => dest.POHeaderId, opt => opt.MapFrom(src => src.POHeaderId))
            .ForMember(dest => dest.DealerId, opt => opt.MapFrom(src => src.DealerId))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.DealerName, opt => opt.MapFrom(src => src.DealerName))
            .ForMember(dest => dest.PONumber, opt => opt.MapFrom(src => src.PONumber))
            .ForMember(dest => dest.POType, opt => opt.MapFrom(src => src.POType))
            .ForMember(dest => dest.NumOfInstallment, opt => opt.MapFrom(src => src.NumOfInstallment))
            .ForMember(dest => dest.AllocQty, opt => opt.MapFrom(src => src.AllocQty))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.Interest, opt => opt.MapFrom(src => src.Interest))
            .ForMember(dest => dest.ContractNumber, opt => opt.MapFrom(src => src.ContractNumber))
            .ForMember(dest => dest.PKNumber, opt => opt.MapFrom(src => src.PKNumber))
            .ForMember(dest => dest.DealerPKNumber, opt => opt.MapFrom(src => src.DealerPKNumber))
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.ProjectName))
            .ForMember(dest => dest.SalesOrderId, opt => opt.MapFrom(src => src.SalesOrderId))
            .ForMember(dest => dest.SONumber, opt => opt.MapFrom(src => src.SONumber))
            .ForMember(dest => dest.SODate, opt => opt.MapFrom(src => src.SODate))
            .ForMember(dest => dest.PaymentRef, opt => opt.MapFrom(src => src.PaymentRef))
            .ForMember(dest => dest.SOType, opt => opt.MapFrom(src => src.SOType))
            .ForMember(dest => dest.TermOfPaymentCode, opt => opt.MapFrom(src => src.TermOfPaymentCode))
            .ForMember(dest => dest.TOPDescription, opt => opt.MapFrom(src => src.TOPDescription))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForMember(dest => dest.VehicleColorCode, opt => opt.MapFrom(src => src.VehicleColorCode))
            .ForMember(dest => dest.VehicleTypeCode, opt => opt.MapFrom(src => src.VehicleTypeCode))
            .ForMember(dest => dest.MaterialNumber, opt => opt.MapFrom(src => src.MaterialNumber))
            .ForMember(dest => dest.MaterialDescription, opt => opt.MapFrom(src => src.MaterialDescription))
            .ForMember(dest => dest.BasePrice, opt => opt.MapFrom(src => src.BasePrice))
            .ForMember(dest => dest.OptionPrice, opt => opt.MapFrom(src => src.OptionPrice))
            .ForMember(dest => dest.DiscountBeforeTax, opt => opt.MapFrom(src => src.DiscountBeforeTax))
            .ForMember(dest => dest.NetPrice, opt => opt.MapFrom(src => src.NetPrice))
            .ForMember(dest => dest.TotalHarga, opt => opt.MapFrom(src => src.TotalHarga))
            .ForMember(dest => dest.PPN, opt => opt.MapFrom(src => src.PPN))
            .ForMember(dest => dest.TotalHargaPPN, opt => opt.MapFrom(src => src.TotalHargaPPN))
            .ForMember(dest => dest.TotalHargaPP, opt => opt.MapFrom(src => src.TotalHargaPP))
            .ForMember(dest => dest.TotalHargaLC, opt => opt.MapFrom(src => src.TotalHargaLC))
            .ForMember(dest => dest.EffectiveDate, opt => opt.MapFrom(src => src.EffectiveDate))
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
            .ForMember(dest => dest.ETDDate, opt => opt.MapFrom(src => src.ETDDate))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

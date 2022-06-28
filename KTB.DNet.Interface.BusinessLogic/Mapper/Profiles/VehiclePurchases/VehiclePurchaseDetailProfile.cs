#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehiclePurchaseDetailProfile mapper class
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
    public class VehiclePurchaseDetailProfile : Profile
    {
        public VehiclePurchaseDetailProfile()
        {
            CreateMap<VehiclePurchaseDetail, VehiclePurchaseDetailDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.BUCode, opt => opt.MapFrom(src => src.BUCode))
                .ForMember(dest => dest.BUName, opt => opt.MapFrom(src => src.BUName))
                .ForMember(dest => dest.CloseLine, opt => opt.MapFrom(src => src.CloseLine))
                .ForMember(dest => dest.CloseLineName, opt => opt.MapFrom(src => src.CloseLineName))
                .ForMember(dest => dest.CloseReason, opt => opt.MapFrom(src => src.CloseReason))
                .ForMember(dest => dest.Completed, opt => opt.MapFrom(src => src.Completed))
                .ForMember(dest => dest.CompletedName, opt => opt.MapFrom(src => src.CompletedName))
                .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.ProductVariantName, opt => opt.MapFrom(src => src.ProductVariantName))
                .ForMember(dest => dest.PODetail, opt => opt.MapFrom(src => src.PODetail))
                .ForMember(dest => dest.POName, opt => opt.MapFrom(src => src.POName))
                .ForMember(dest => dest.PRDetailName, opt => opt.MapFrom(src => src.PRDetailName))
                .ForMember(dest => dest.PurchaseUnitName, opt => opt.MapFrom(src => src.PurchaseUnitName))
                .ForMember(dest => dest.QtyOrder, opt => opt.MapFrom(src => src.QtyOrder))
                .ForMember(dest => dest.QtyReceipt, opt => opt.MapFrom(src => src.QtyReceipt))
                .ForMember(dest => dest.QtyReturn, opt => opt.MapFrom(src => src.QtyReturn))
                .ForMember(dest => dest.RecallProduct, opt => opt.MapFrom(src => src.RecallProduct))
                .ForMember(dest => dest.RecallProductName, opt => opt.MapFrom(src => src.RecallProductName))
                .ForMember(dest => dest.SODetailName, opt => opt.MapFrom(src => src.SODetailName))
                .ForMember(dest => dest.ScheduledShippingDate, opt => opt.MapFrom(src => src.ScheduledShippingDate))
                .ForMember(dest => dest.ServicePartsAndMaterial, opt => opt.MapFrom(src => src.ServicePartsAndMaterial))
                .ForMember(dest => dest.ShippingDate, opt => opt.MapFrom(src => src.ShippingDate))
                .ForMember(dest => dest.Site, opt => opt.MapFrom(src => src.Site))
                .ForMember(dest => dest.StockNumberName, opt => opt.MapFrom(src => src.StockNumberName))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<VehiclePurchaseDetailParameterDto, VehiclePurchaseDetail>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.BUCode, opt => opt.MapFrom(src => src.BUCode))
                .ForMember(dest => dest.BUName, opt => opt.MapFrom(src => src.BUName))
                .ForMember(dest => dest.CloseLine, opt => opt.MapFrom(src => src.CloseLine))
                .ForMember(dest => dest.CloseLineName, opt => opt.MapFrom(src => src.CloseLineName))
                .ForMember(dest => dest.CloseReason, opt => opt.MapFrom(src => src.CloseReason))
                .ForMember(dest => dest.Completed, opt => opt.MapFrom(src => src.Completed))
                .ForMember(dest => dest.CompletedName, opt => opt.MapFrom(src => src.CompletedName))
                .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.ProductVariantName, opt => opt.MapFrom(src => src.ProductVariantName))
                .ForMember(dest => dest.PODetail, opt => opt.MapFrom(src => src.PODetail))
                .ForMember(dest => dest.POName, opt => opt.MapFrom(src => src.POName))
                .ForMember(dest => dest.PRDetailName, opt => opt.MapFrom(src => src.PRDetailName))
                .ForMember(dest => dest.PurchaseUnitName, opt => opt.MapFrom(src => src.PurchaseUnitName))
                .ForMember(dest => dest.QtyOrder, opt => opt.MapFrom(src => src.QtyOrder))
                .ForMember(dest => dest.QtyReceipt, opt => opt.MapFrom(src => src.QtyReceipt))
                .ForMember(dest => dest.QtyReturn, opt => opt.MapFrom(src => src.QtyReturn))
                .ForMember(dest => dest.RecallProduct, opt => opt.MapFrom(src => src.RecallProduct))
                .ForMember(dest => dest.RecallProductName, opt => opt.MapFrom(src => src.RecallProductName))
                .ForMember(dest => dest.SODetailName, opt => opt.MapFrom(src => src.SODetailName))
                .ForMember(dest => dest.ScheduledShippingDate, opt => opt.MapFrom(src => src.ScheduledShippingDate))
                .ForMember(dest => dest.ServicePartsAndMaterial, opt => opt.MapFrom(src => src.ServicePartsAndMaterial))
                .ForMember(dest => dest.ShippingDate, opt => opt.MapFrom(src => src.ShippingDate))
                .ForMember(dest => dest.Site, opt => opt.MapFrom(src => src.Site))
                .ForMember(dest => dest.StockNumberName, opt => opt.MapFrom(src => src.StockNumberName))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartDeliveryOrderDetailProfile mapper class
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
    public class SparePartDeliveryOrderDetailProfile : Profile
    {
        public SparePartDeliveryOrderDetailProfile()
        {
            CreateMap<SparePartDeliveryOrderDetail, SparePartDeliveryOrderDetailDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.AmountBeforeDiscount, opt => opt.MapFrom(src => src.AmountBeforeDiscount))
            .ForMember(dest => dest.BaseAmount, opt => opt.MapFrom(src => src.BaseAmount))
            .ForMember(dest => dest.BaseQtyDelivered, opt => opt.MapFrom(src => src.BaseQtyDelivered))
            .ForMember(dest => dest.BaseQtyOrder, opt => opt.MapFrom(src => src.BaseQtyOrder))
            .ForMember(dest => dest.BatchNo, opt => opt.MapFrom(src => src.BatchNo))
            .ForMember(dest => dest.BU, opt => opt.MapFrom(src => src.BU))
            .ForMember(dest => dest.ConsumptionTax1Amount, opt => opt.MapFrom(src => src.ConsumptionTax1Amount))
            .ForMember(dest => dest.ConsumptionTax1, opt => opt.MapFrom(src => src.ConsumptionTax1))
            .ForMember(dest => dest.ConsumptionTax2Amount, opt => opt.MapFrom(src => src.ConsumptionTax2Amount))
            .ForMember(dest => dest.ConsumptionTax2, opt => opt.MapFrom(src => src.ConsumptionTax2))
            .ForMember(dest => dest.DeliveryOrderDetail, opt => opt.MapFrom(src => src.DeliveryOrderDetail))
            .ForMember(dest => dest.DeliveryOrderNo, opt => opt.MapFrom(src => src.DeliveryOrderNo))
            .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.DiscountAmount))
            .ForMember(dest => dest.DiscountBaseAmount, opt => opt.MapFrom(src => src.DiscountBaseAmount))
            .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.ProductCrossReference, opt => opt.MapFrom(src => src.ProductCrossReference))
            .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.PromiseDate, opt => opt.MapFrom(src => src.PromiseDate))
            .ForMember(dest => dest.QtyDelivered, opt => opt.MapFrom(src => src.QtyDelivered))
            .ForMember(dest => dest.QtyOrder, opt => opt.MapFrom(src => src.QtyOrder))
            .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => src.RequestDate))
            .ForMember(dest => dest.RunningNumber, opt => opt.MapFrom(src => src.RunningNumber))
            .ForMember(dest => dest.SalesOrderDetail, opt => opt.MapFrom(src => src.SalesOrderDetail))
            .ForMember(dest => dest.SalesUnit, opt => opt.MapFrom(src => src.SalesUnit))
            .ForMember(dest => dest.Site, opt => opt.MapFrom(src => src.Site))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.TotalConsumptionTaxAmount, opt => opt.MapFrom(src => src.TotalConsumptionTaxAmount))
            .ForMember(dest => dest.TransactionAmount, opt => opt.MapFrom(src => src.TransactionAmount))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SparePartDeliveryOrderDetailParameterDto, SparePartDeliveryOrderDetail>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.AmountBeforeDiscount, opt => opt.MapFrom(src => src.AmountBeforeDiscount))
            .ForMember(dest => dest.BaseAmount, opt => opt.MapFrom(src => src.BaseAmount))
            .ForMember(dest => dest.BaseQtyDelivered, opt => opt.MapFrom(src => src.BaseQtyDelivered))
            .ForMember(dest => dest.BaseQtyOrder, opt => opt.MapFrom(src => src.BaseQtyOrder))
            .ForMember(dest => dest.BatchNo, opt => opt.MapFrom(src => src.BatchNo))
            .ForMember(dest => dest.BU, opt => opt.MapFrom(src => src.BU))
            .ForMember(dest => dest.ConsumptionTax1Amount, opt => opt.MapFrom(src => src.ConsumptionTax1Amount))
            .ForMember(dest => dest.ConsumptionTax1, opt => opt.MapFrom(src => src.ConsumptionTax1))
            .ForMember(dest => dest.ConsumptionTax2Amount, opt => opt.MapFrom(src => src.ConsumptionTax2Amount))
            .ForMember(dest => dest.ConsumptionTax2, opt => opt.MapFrom(src => src.ConsumptionTax2))
            .ForMember(dest => dest.DeliveryOrderDetail, opt => opt.MapFrom(src => src.DeliveryOrderDetail))
            .ForMember(dest => dest.DeliveryOrderNo, opt => opt.MapFrom(src => src.DeliveryOrderNo))
            .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.DiscountAmount))
            .ForMember(dest => dest.DiscountBaseAmount, opt => opt.MapFrom(src => src.DiscountBaseAmount))
            .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.ProductCrossReference, opt => opt.MapFrom(src => src.ProductCrossReference))
            .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.PromiseDate, opt => opt.MapFrom(src => src.PromiseDate))
            .ForMember(dest => dest.QtyDelivered, opt => opt.MapFrom(src => src.QtyDelivered))
            .ForMember(dest => dest.QtyOrder, opt => opt.MapFrom(src => src.QtyOrder))
            .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => src.RequestDate))
            .ForMember(dest => dest.RunningNumber, opt => opt.MapFrom(src => src.RunningNumber))
            .ForMember(dest => dest.SalesOrderDetail, opt => opt.MapFrom(src => src.SalesOrderDetail))
            .ForMember(dest => dest.SalesUnit, opt => opt.MapFrom(src => src.SalesUnit))
            .ForMember(dest => dest.Site, opt => opt.MapFrom(src => src.Site))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.TotalConsumptionTaxAmount, opt => opt.MapFrom(src => src.TotalConsumptionTaxAmount))
            .ForMember(dest => dest.TransactionAmount, opt => opt.MapFrom(src => src.TransactionAmount))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

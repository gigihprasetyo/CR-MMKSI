#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : POOtherVendorDetailProfile mapper class
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
    public class POOtherVendorDetailProfile : Profile
    {
        public POOtherVendorDetailProfile()
        {
            CreateMap<POOtherVendorDetail, POOtherVendorDetailDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.CloseLine, opt => opt.MapFrom(src => src.CloseLine))
            .ForMember(dest => dest.CloseReason, opt => opt.MapFrom(src => src.CloseReason))
            .ForMember(dest => dest.Completed, opt => opt.MapFrom(src => src.Completed))
            .ForMember(dest => dest.ConsumptionTax1Amount, opt => opt.MapFrom(src => src.ConsumptionTax1Amount))
            .ForMember(dest => dest.ConsumptionTax1, opt => opt.MapFrom(src => src.ConsumptionTax1))
            .ForMember(dest => dest.ConsumptionTax2Amount, opt => opt.MapFrom(src => src.ConsumptionTax2Amount))
            .ForMember(dest => dest.ConsumptionTax2, opt => opt.MapFrom(src => src.ConsumptionTax2))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.DiscountAmount))
            .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
            .ForMember(dest => dest.EventData, opt => opt.MapFrom(src => src.EventData))
            .ForMember(dest => dest.FormSource, opt => opt.MapFrom(src => src.FormSource))
            .ForMember(dest => dest.BaseQtyOrder, opt => opt.MapFrom(src => src.BaseQtyOrder))
            .ForMember(dest => dest.BaseQtyReceipt, opt => opt.MapFrom(src => src.BaseQtyReceipt))
            .ForMember(dest => dest.BaseQtyReturn, opt => opt.MapFrom(src => src.BaseQtyReturn))
            .ForMember(dest => dest.InventoryUnit, opt => opt.MapFrom(src => src.InventoryUnit))
            .ForMember(dest => dest.ProductCrossReference, opt => opt.MapFrom(src => src.ProductCrossReference))
            .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.ProductSubstitute, opt => opt.MapFrom(src => src.ProductSubstitute))
            .ForMember(dest => dest.ProductVariant, opt => opt.MapFrom(src => src.ProductVariant))
            .ForMember(dest => dest.ProductVolume, opt => opt.MapFrom(src => src.ProductVolume))
            .ForMember(dest => dest.ProductWeight, opt => opt.MapFrom(src => src.ProductWeight))
            .ForMember(dest => dest.PromisedDate, opt => opt.MapFrom(src => src.PromisedDate))
            .ForMember(dest => dest.PurchaseFor, opt => opt.MapFrom(src => src.PurchaseFor))
            .ForMember(dest => dest.PurchaseOrderNo, opt => opt.MapFrom(src => src.PurchaseOrderNo))
            .ForMember(dest => dest.PurchaseRequisitionDetail, opt => opt.MapFrom(src => src.PurchaseRequisitionDetail))
            .ForMember(dest => dest.PurchaseUnit, opt => opt.MapFrom(src => src.PurchaseUnit))
            .ForMember(dest => dest.QtyOrder, opt => opt.MapFrom(src => src.QtyOrder))
            .ForMember(dest => dest.QtyReceipt, opt => opt.MapFrom(src => src.QtyReceipt))
            .ForMember(dest => dest.QtyReturn, opt => opt.MapFrom(src => src.QtyReturn))
            .ForMember(dest => dest.RecallProduct, opt => opt.MapFrom(src => src.RecallProduct))
            .ForMember(dest => dest.ReferenceNo, opt => opt.MapFrom(src => src.ReferenceNo))
            .ForMember(dest => dest.RequiredDate, opt => opt.MapFrom(src => src.RequiredDate))
            .ForMember(dest => dest.SalesOrderDetail, opt => opt.MapFrom(src => src.SalesOrderDetail))
            .ForMember(dest => dest.ScheduledShippingDate, opt => opt.MapFrom(src => src.ScheduledShippingDate))
            .ForMember(dest => dest.ServicePartsAndMaterial, opt => opt.MapFrom(src => src.ServicePartsAndMaterial))
            .ForMember(dest => dest.ShippingDate, opt => opt.MapFrom(src => src.ShippingDate))
            .ForMember(dest => dest.Site, opt => opt.MapFrom(src => src.Site))
            .ForMember(dest => dest.StockNumber, opt => opt.MapFrom(src => src.StockNumber))
            .ForMember(dest => dest.TitleRegistrationFee, opt => opt.MapFrom(src => src.TitleRegistrationFee))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.TotalAmountBeforeDiscount, opt => opt.MapFrom(src => src.TotalAmountBeforeDiscount))
            .ForMember(dest => dest.TotalBaseAmount, opt => opt.MapFrom(src => src.TotalBaseAmount))
            .ForMember(dest => dest.TotalConsumptionTaxAmount, opt => opt.MapFrom(src => src.TotalConsumptionTaxAmount))
            .ForMember(dest => dest.TotalVolume, opt => opt.MapFrom(src => src.TotalVolume))
            .ForMember(dest => dest.TotalWeight, opt => opt.MapFrom(src => src.TotalWeight))
            .ForMember(dest => dest.TransactionAmount, opt => opt.MapFrom(src => src.TransactionAmount))
            .ForMember(dest => dest.UnitCost, opt => opt.MapFrom(src => src.UnitCost))
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<POOtherVendorDetailParameterDto, POOtherVendorDetail>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.CloseLine, opt => opt.MapFrom(src => src.CloseLine))
            .ForMember(dest => dest.CloseReason, opt => opt.MapFrom(src => src.CloseReason))
            .ForMember(dest => dest.Completed, opt => opt.MapFrom(src => src.Completed))
            .ForMember(dest => dest.ConsumptionTax1Amount, opt => opt.MapFrom(src => src.ConsumptionTax1Amount))
            .ForMember(dest => dest.ConsumptionTax1, opt => opt.MapFrom(src => src.ConsumptionTax1))
            .ForMember(dest => dest.ConsumptionTax2Amount, opt => opt.MapFrom(src => src.ConsumptionTax2Amount))
            .ForMember(dest => dest.ConsumptionTax2, opt => opt.MapFrom(src => src.ConsumptionTax2))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.DiscountAmount))
            .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
            .ForMember(dest => dest.EventData, opt => opt.MapFrom(src => src.EventData))
            .ForMember(dest => dest.FormSource, opt => opt.MapFrom(src => src.FormSource))
            .ForMember(dest => dest.BaseQtyOrder, opt => opt.MapFrom(src => src.BaseQtyOrder))
            .ForMember(dest => dest.BaseQtyReceipt, opt => opt.MapFrom(src => src.BaseQtyReceipt))
            .ForMember(dest => dest.BaseQtyReturn, opt => opt.MapFrom(src => src.BaseQtyReturn))
            .ForMember(dest => dest.InventoryUnit, opt => opt.MapFrom(src => src.InventoryUnit))
            .ForMember(dest => dest.ProductCrossReference, opt => opt.MapFrom(src => src.ProductCrossReference))
            .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.ProductSubstitute, opt => opt.MapFrom(src => src.ProductSubstitute))
            .ForMember(dest => dest.ProductVariant, opt => opt.MapFrom(src => src.ProductVariant))
            .ForMember(dest => dest.ProductVolume, opt => opt.MapFrom(src => src.ProductVolume))
            .ForMember(dest => dest.ProductWeight, opt => opt.MapFrom(src => src.ProductWeight))
            .ForMember(dest => dest.PromisedDate, opt => opt.MapFrom(src => src.PromisedDate))
            .ForMember(dest => dest.PurchaseFor, opt => opt.MapFrom(src => src.PurchaseFor))
            .ForMember(dest => dest.PurchaseOrderNo, opt => opt.MapFrom(src => src.PurchaseOrderNo))
            .ForMember(dest => dest.PurchaseRequisitionDetail, opt => opt.MapFrom(src => src.PurchaseRequisitionDetail))
            .ForMember(dest => dest.PurchaseUnit, opt => opt.MapFrom(src => src.PurchaseUnit))
            .ForMember(dest => dest.QtyOrder, opt => opt.MapFrom(src => src.QtyOrder))
            .ForMember(dest => dest.QtyReceipt, opt => opt.MapFrom(src => src.QtyReceipt))
            .ForMember(dest => dest.QtyReturn, opt => opt.MapFrom(src => src.QtyReturn))
            .ForMember(dest => dest.RecallProduct, opt => opt.MapFrom(src => src.RecallProduct))
            .ForMember(dest => dest.ReferenceNo, opt => opt.MapFrom(src => src.ReferenceNo))
            .ForMember(dest => dest.RequiredDate, opt => opt.MapFrom(src => src.RequiredDate))
            .ForMember(dest => dest.SalesOrderDetail, opt => opt.MapFrom(src => src.SalesOrderDetail))
            .ForMember(dest => dest.ScheduledShippingDate, opt => opt.MapFrom(src => src.ScheduledShippingDate))
            .ForMember(dest => dest.ServicePartsAndMaterial, opt => opt.MapFrom(src => src.ServicePartsAndMaterial))
            .ForMember(dest => dest.ShippingDate, opt => opt.MapFrom(src => src.ShippingDate))
            .ForMember(dest => dest.Site, opt => opt.MapFrom(src => src.Site))
            .ForMember(dest => dest.StockNumber, opt => opt.MapFrom(src => src.StockNumber))
            .ForMember(dest => dest.TitleRegistrationFee, opt => opt.MapFrom(src => src.TitleRegistrationFee))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.TotalAmountBeforeDiscount, opt => opt.MapFrom(src => src.TotalAmountBeforeDiscount))
            .ForMember(dest => dest.TotalBaseAmount, opt => opt.MapFrom(src => src.TotalBaseAmount))
            .ForMember(dest => dest.TotalConsumptionTaxAmount, opt => opt.MapFrom(src => src.TotalConsumptionTaxAmount))
            .ForMember(dest => dest.TotalVolume, opt => opt.MapFrom(src => src.TotalVolume))
            .ForMember(dest => dest.TotalWeight, opt => opt.MapFrom(src => src.TotalWeight))
            .ForMember(dest => dest.TransactionAmount, opt => opt.MapFrom(src => src.TransactionAmount))
            .ForMember(dest => dest.UnitCost, opt => opt.MapFrom(src => src.UnitCost))
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

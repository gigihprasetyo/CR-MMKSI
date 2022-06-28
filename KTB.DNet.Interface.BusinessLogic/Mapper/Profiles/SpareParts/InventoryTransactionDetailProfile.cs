#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : InventoryTransactionDetailProfile mapper class
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
    public class InventoryTransactionDetailProfile : Profile
    {
        public InventoryTransactionDetailProfile()
        {
            CreateMap<InventoryTransactionDetail, InventoryTransactionDetailDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.BaseQuantity, opt => opt.MapFrom(src => src.BaseQuantity))
            .ForMember(dest => dest.BatchNo, opt => opt.MapFrom(src => src.BatchNo))
            .ForMember(dest => dest.BU, opt => opt.MapFrom(src => src.BU))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.FromBU, opt => opt.MapFrom(src => src.FromBU))
            .ForMember(dest => dest.InventoryTransactionNo, opt => opt.MapFrom(src => src.InventoryTransactionNo))
            .ForMember(dest => dest.InventoryTransferDetail, opt => opt.MapFrom(src => src.InventoryTransferDetail))
            .ForMember(dest => dest.InventoryUnit, opt => opt.MapFrom(src => src.InventoryUnit))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.ProductCrossReference, opt => opt.MapFrom(src => src.ProductCrossReference))
            .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.ReasonCode, opt => opt.MapFrom(src => src.ReasonCode))
            .ForMember(dest => dest.ReferenceNo, opt => opt.MapFrom(src => src.ReferenceNo))
            .ForMember(dest => dest.RegisterSerialNumber, opt => opt.MapFrom(src => src.RegisterSerialNumber))
            .ForMember(dest => dest.RunningNumber, opt => opt.MapFrom(src => src.RunningNumber))
            .ForMember(dest => dest.SerialNo, opt => opt.MapFrom(src => src.SerialNo))
            .ForMember(dest => dest.ServicePartsAndMaterial, opt => opt.MapFrom(src => src.ServicePartsAndMaterial))
            .ForMember(dest => dest.Site, opt => opt.MapFrom(src => src.Site))
            .ForMember(dest => dest.SourceData, opt => opt.MapFrom(src => src.SourceData))
            .ForMember(dest => dest.StockNumber, opt => opt.MapFrom(src => src.StockNumber))
            .ForMember(dest => dest.StockNumberNV, opt => opt.MapFrom(src => src.StockNumberNV))
            .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.TotalCost))
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType))
            .ForMember(dest => dest.TransactionUnit, opt => opt.MapFrom(src => src.TransactionUnit))
            .ForMember(dest => dest.UnitCost, opt => opt.MapFrom(src => src.UnitCost))
            .ForMember(dest => dest.VIN, opt => opt.MapFrom(src => src.VIN))
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<InventoryTransactionDetailParameterDto, InventoryTransactionDetail>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.BaseQuantity, opt => opt.MapFrom(src => src.BaseQuantity))
            .ForMember(dest => dest.BatchNo, opt => opt.MapFrom(src => src.BatchNo))
            .ForMember(dest => dest.BU, opt => opt.MapFrom(src => src.BU))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.FromBU, opt => opt.MapFrom(src => src.FromBU))
            .ForMember(dest => dest.InventoryTransactionNo, opt => opt.MapFrom(src => src.InventoryTransactionNo))
            .ForMember(dest => dest.InventoryTransferDetail, opt => opt.MapFrom(src => src.InventoryTransferDetail))
            .ForMember(dest => dest.InventoryUnit, opt => opt.MapFrom(src => src.InventoryUnit))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.ProductCrossReference, opt => opt.MapFrom(src => src.ProductCrossReference))
            .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.ReasonCode, opt => opt.MapFrom(src => src.ReasonCode))
            .ForMember(dest => dest.ReferenceNo, opt => opt.MapFrom(src => src.ReferenceNo))
            .ForMember(dest => dest.RegisterSerialNumber, opt => opt.MapFrom(src => src.RegisterSerialNumber))
            .ForMember(dest => dest.RunningNumber, opt => opt.MapFrom(src => src.RunningNumber))
            .ForMember(dest => dest.SerialNo, opt => opt.MapFrom(src => src.SerialNo))
            .ForMember(dest => dest.ServicePartsAndMaterial, opt => opt.MapFrom(src => src.ServicePartsAndMaterial))
            .ForMember(dest => dest.Site, opt => opt.MapFrom(src => src.Site))
            .ForMember(dest => dest.SourceData, opt => opt.MapFrom(src => src.SourceData))
            .ForMember(dest => dest.StockNumber, opt => opt.MapFrom(src => src.StockNumber))
            .ForMember(dest => dest.StockNumberNV, opt => opt.MapFrom(src => src.StockNumberNV))
            .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.TotalCost))
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType))
            .ForMember(dest => dest.TransactionUnit, opt => opt.MapFrom(src => src.TransactionUnit))
            .ForMember(dest => dest.UnitCost, opt => opt.MapFrom(src => src.UnitCost))
            .ForMember(dest => dest.VIN, opt => opt.MapFrom(src => src.VIN))
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

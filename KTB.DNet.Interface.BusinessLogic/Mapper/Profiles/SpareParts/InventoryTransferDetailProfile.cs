#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : InventoryTransferDetailProfile mapper class
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
    public class InventoryTransferDetailProfile : Profile
    {
        public InventoryTransferDetailProfile()
        {
            CreateMap<InventoryTransferDetailParameterDto, InventoryTransferDetail>()
                .ForMember(dest => dest.ErrorMessage, opt => opt.Ignore())
                .ForMember(dest => dest.InventoryTransfer, opt => opt.Ignore());

            CreateMap<InventoryTransferDetail, InventoryTransferDetailDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.BaseQuantity, opt => opt.MapFrom(src => src.BaseQuantity))
            .ForMember(dest => dest.ConsumptionTaxIn, opt => opt.MapFrom(src => src.ConsumptionTaxIn))
            .ForMember(dest => dest.ConsumptionTaxOut, opt => opt.MapFrom(src => src.ConsumptionTaxOut))
            .ForMember(dest => dest.FromBatchNo, opt => opt.MapFrom(src => src.FromBatchNo))
            .ForMember(dest => dest.FromDealer, opt => opt.MapFrom(src => src.FromDealer))
            .ForMember(dest => dest.FromConfiguration, opt => opt.MapFrom(src => src.FromConfiguration))
            .ForMember(dest => dest.FromExteriorColor, opt => opt.MapFrom(src => src.FromExteriorColor))
            .ForMember(dest => dest.FromInteriorColor, opt => opt.MapFrom(src => src.FromInteriorColor))
            .ForMember(dest => dest.FromLocation, opt => opt.MapFrom(src => src.FromLocation))
            .ForMember(dest => dest.FromSerialNo, opt => opt.MapFrom(src => src.FromSerialNo))
            .ForMember(dest => dest.FromSite, opt => opt.MapFrom(src => src.FromSite))
            .ForMember(dest => dest.FromStyle, opt => opt.MapFrom(src => src.FromStyle))
            .ForMember(dest => dest.FromWarehouse, opt => opt.MapFrom(src => src.FromWarehouse))
            .ForMember(dest => dest.InventoryTransferNo, opt => opt.MapFrom(src => src.InventoryTransferNo))
            .ForMember(dest => dest.InventoryUnit, opt => opt.MapFrom(src => src.InventoryUnit))
            .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Remarks, opt => opt.MapFrom(src => src.Remarks))
            .ForMember(dest => dest.ServicePartsandMaterial, opt => opt.MapFrom(src => src.ServicePartsandMaterial))
            .ForMember(dest => dest.SourceData, opt => opt.MapFrom(src => src.SourceData))
            .ForMember(dest => dest.StockNumber, opt => opt.MapFrom(src => src.StockNumber))
            .ForMember(dest => dest.StockNumberNV, opt => opt.MapFrom(src => src.StockNumberNV))
            .ForMember(dest => dest.StockNumberLookupName, opt => opt.MapFrom(src => src.StockNumberLookupName))
            .ForMember(dest => dest.StockNumberLookupType, opt => opt.MapFrom(src => src.StockNumberLookupType))
            .ForMember(dest => dest.ToBatchNo, opt => opt.MapFrom(src => src.ToBatchNo))
            .ForMember(dest => dest.ToDealer, opt => opt.MapFrom(src => src.ToDealer))
            .ForMember(dest => dest.ToConfiguration, opt => opt.MapFrom(src => src.ToConfiguration))
            .ForMember(dest => dest.ToExteriorColor, opt => opt.MapFrom(src => src.ToExteriorColor))
            .ForMember(dest => dest.ToInteriorColor, opt => opt.MapFrom(src => src.ToInteriorColor))
            .ForMember(dest => dest.ToLocation, opt => opt.MapFrom(src => src.ToLocation))
            .ForMember(dest => dest.ToSerialNo, opt => opt.MapFrom(src => src.ToSerialNo))
            .ForMember(dest => dest.ToSite, opt => opt.MapFrom(src => src.ToSite))
            .ForMember(dest => dest.ToStyle, opt => opt.MapFrom(src => src.ToStyle))
            .ForMember(dest => dest.ToWarehouse, opt => opt.MapFrom(src => src.ToWarehouse))
            .ForMember(dest => dest.TransactionUnit, opt => opt.MapFrom(src => src.TransactionUnit))
            .ForMember(dest => dest.VIN, opt => opt.MapFrom(src => src.VIN))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

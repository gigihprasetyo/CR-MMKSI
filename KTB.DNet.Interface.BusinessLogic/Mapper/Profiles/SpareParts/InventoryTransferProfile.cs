#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : InventoryTransferProfile mapper class
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
using System;
#endregion

namespace KTB.DNet.Interface.BusinessLogic.MapperBL
{
    public class InventoryTransferProfile : Profile
    {
        public InventoryTransferProfile()
        {
            //CreateMap<InventoryTransferDto, InventoryTransfer>().ConvertUsing(new TransferConverter());

            CreateMap<InventoryTransfer, InventoryTransferDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.FromDealer, opt => opt.MapFrom(src => src.FromDealer))
            .ForMember(dest => dest.FromSite, opt => opt.MapFrom(src => src.FromSite))
            .ForMember(dest => dest.InventoryTransferNo, opt => opt.MapFrom(src => src.InventoryTransferNo))
            .ForMember(dest => dest.ItemTypeForTransfer, opt => opt.MapFrom(src => src.ItemTypeForTransfer))
            .ForMember(dest => dest.PersonInCharge, opt => opt.MapFrom(src => src.PersonInCharge))
            .ForMember(dest => dest.ReceiptDate, opt => opt.MapFrom(src => src.ReceiptDate))
            .ForMember(dest => dest.ReceiptNo, opt => opt.MapFrom(src => src.ReceiptNo))
            .ForMember(dest => dest.ReferenceNo, opt => opt.MapFrom(src => src.ReferenceNo))
            .ForMember(dest => dest.SearchVehicle, opt => opt.MapFrom(src => src.SearchVehicle))
            .ForMember(dest => dest.SourceData, opt => opt.MapFrom(src => src.SourceData))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.ToDealer, opt => opt.MapFrom(src => src.ToDealer))
            .ForMember(dest => dest.ToSite, opt => opt.MapFrom(src => src.ToSite))
            .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate))
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType))
            .ForMember(dest => dest.TransferStatus, opt => opt.MapFrom(src => src.TransferStatus))
            .ForMember(dest => dest.TransferStep, opt => opt.MapFrom(src => src.TransferStep))
            .ForMember(dest => dest.WONo, opt => opt.MapFrom(src => src.WONo))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<InventoryTransferParameterDto, InventoryTransfer>()
                 .ForMember(dest => dest.ErrorMessage, opt => opt.Ignore());
        }
    }

    public class TransferConverter : ITypeConverter<InventoryTransferDto, InventoryTransfer>
    {

        public InventoryTransfer Convert(InventoryTransferDto source, InventoryTransfer destination, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}



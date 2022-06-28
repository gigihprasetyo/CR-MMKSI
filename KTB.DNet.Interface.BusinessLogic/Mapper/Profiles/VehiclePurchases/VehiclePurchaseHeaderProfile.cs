#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehiclePurchaseHeaderProfile mapper class
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
    public class VehiclePurchaseHeaderProfile : Profile
    {
        public VehiclePurchaseHeaderProfile()
        {
            CreateMap<VehiclePurchaseHeader, VehiclePurchaseHeaderDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.BUCode, opt => opt.MapFrom(src => src.BUCode))
            .ForMember(dest => dest.BUName, opt => opt.MapFrom(src => src.BUName))
            .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.PRPOTypeName, opt => opt.MapFrom(src => src.PRPOTypeName))
            .ForMember(dest => dest.DMSPONo, opt => opt.MapFrom(src => src.DMSPONo))
            .ForMember(dest => dest.DMSPOStatus, opt => opt.MapFrom(src => src.DMSPOStatus))
            .ForMember(dest => dest.DMSPODate, opt => opt.MapFrom(src => src.DMSPODate))
            .ForMember(dest => dest.VendorDescription, opt => opt.MapFrom(src => src.VendorDescription))
            .ForMember(dest => dest.Vendor, opt => opt.MapFrom(src => src.Vendor))
            .ForMember(dest => dest.PurchaseOrderNo, opt => opt.MapFrom(src => src.PurchaseOrderNo))
            .ForMember(dest => dest.PurchaseReceiptNo, opt => opt.MapFrom(src => src.PurchaseReceiptNo))
            .ForMember(dest => dest.PurchaseReceiptDetailNo, opt => opt.MapFrom(src => src.PurchaseReceiptDetailNo))
            .ForMember(dest => dest.ChassisModel, opt => opt.MapFrom(src => src.ChassisModel))
            .ForMember(dest => dest.ChassisNumberRegister, opt => opt.MapFrom(src => src.ChassisNumberRegister))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<VehiclePurchaseHeaderParameterDto, VehiclePurchaseHeader>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.BUCode, opt => opt.MapFrom(src => src.BUCode))
            .ForMember(dest => dest.BUName, opt => opt.MapFrom(src => src.BUName))
            .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.PRPOTypeName, opt => opt.MapFrom(src => src.PRPOTypeName))
            .ForMember(dest => dest.DMSPONo, opt => opt.MapFrom(src => src.DMSPONo))
            .ForMember(dest => dest.DMSPOStatus, opt => opt.MapFrom(src => src.DMSPOStatus))
            .ForMember(dest => dest.DMSPODate, opt => opt.MapFrom(src => src.DMSPODate))
            .ForMember(dest => dest.VendorDescription, opt => opt.MapFrom(src => src.VendorDescription))
            .ForMember(dest => dest.Vendor, opt => opt.MapFrom(src => src.Vendor))
            .ForMember(dest => dest.PurchaseOrderNo, opt => opt.MapFrom(src => src.PurchaseOrderNo))
            .ForMember(dest => dest.PurchaseReceiptNo, opt => opt.MapFrom(src => src.PurchaseReceiptNo))
            .ForMember(dest => dest.PurchaseReceiptDetailNo, opt => opt.MapFrom(src => src.PurchaseReceiptDetailNo))
            .ForMember(dest => dest.ChassisModel, opt => opt.MapFrom(src => src.ChassisModel))
            .ForMember(dest => dest.ChassisNumberRegister, opt => opt.MapFrom(src => src.ChassisNumberRegister))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

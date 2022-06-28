#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_POReceiptDealerProfile mapper class
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
    public class VWI_POReceiptDealerProfile : Profile
    {
        public VWI_POReceiptDealerProfile()
        {
            CreateMap<VWI_POReceiptDealer, VWI_POReceiptDealerDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
                .ForMember(dest => dest.EndCustomerID, opt => opt.MapFrom(src => src.EndCustomerID))
                .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryID))
                .ForMember(dest => dest.VehicleColorCode, opt => opt.MapFrom(src => src.VehicleColorCode))
                .ForMember(dest => dest.VehicleColorDesc, opt => opt.MapFrom(src => src.VehicleColorDesc))
                .ForMember(dest => dest.MaterialNumber, opt => opt.MapFrom(src => src.MaterialNumber))
                .ForMember(dest => dest.MaterialDescription, opt => opt.MapFrom(src => src.MaterialDescription))
                .ForMember(dest => dest.VehicleKindCode, opt => opt.MapFrom(src => src.VehicleKindCode))
                .ForMember(dest => dest.VehicleKindDesc, opt => opt.MapFrom(src => src.VehicleKindDesc))
                .ForMember(dest => dest.VehicleTypeCode, opt => opt.MapFrom(src => src.VehicleTypeCode))
                .ForMember(dest => dest.VehicleTypeDesc, opt => opt.MapFrom(src => src.VehicleTypeDesc))
                .ForMember(dest => dest.SoldDealerCode, opt => opt.MapFrom(src => src.SoldDealerCode))
                .ForMember(dest => dest.SoldDealerName, opt => opt.MapFrom(src => src.SoldDealerName))
                .ForMember(dest => dest.DONumber, opt => opt.MapFrom(src => src.DONumber))
                .ForMember(dest => dest.SONumber, opt => opt.MapFrom(src => src.SONumber))
                .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.DiscountAmount))
                .ForMember(dest => dest.PONumber, opt => opt.MapFrom(src => src.PONumber))
                .ForMember(dest => dest.EngineNumber, opt => opt.MapFrom(src => src.EngineNumber))
                .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.SerialNumber))
                .ForMember(dest => dest.DODate, opt => opt.MapFrom(src => src.DODate))
                .ForMember(dest => dest.GIDate, opt => opt.MapFrom(src => src.GIDate))
                .ForMember(dest => dest.ParkingAmount, opt => opt.MapFrom(src => src.ParkingAmount))
                .ForMember(dest => dest.ProductionYear, opt => opt.MapFrom(src => src.ProductionYear))
                .ForMember(dest => dest.ATDDate, opt => opt.MapFrom(src => src.ATDDate))
                .ForMember(dest => dest.ETADate, opt => opt.MapFrom(src => src.ETADate))
                .ForMember(dest => dest.ATADate, opt => opt.MapFrom(src => src.ATADate))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

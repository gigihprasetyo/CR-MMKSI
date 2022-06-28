#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMasterProfile mapper class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region "Namespace Imports"
using AutoMapper;
using KTB.DNet.Domain;
using KTB.DNet.Interface.Model;
#endregion

namespace KTB.DNet.Interface.BusinessLogic.MapperBL
{
    public class ChassisMasterProfile : Profile
    {
        public ChassisMasterProfile()
        {
            CreateMap<ChassisMaster, ChassisMasterDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
                .ForMember(dest => dest.EndCustomerID, opt => opt.MapFrom(src => src.EndCustomerID))
                .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.Category.ID))
                .ForMember(dest => dest.VehicleColorID, opt => opt.MapFrom(src => src.VechileColor.ID))
                .ForMember(dest => dest.VehicleColorCode, opt => opt.MapFrom(src => src.VechileColor.ColorCode))
                .ForMember(dest => dest.VehicleColorDesc, opt => opt.MapFrom(src => src.VechileColor.ColorIndName))
                .ForMember(dest => dest.MaterialNumber, opt => opt.MapFrom(src => src.VechileColor.MaterialNumber))
                .ForMember(dest => dest.MaterialDescription, opt => opt.MapFrom(src => src.VechileColor.MaterialDescription))
                .ForMember(dest => dest.VehicleKindID, opt => opt.MapFrom(src => src.VehicleKind.ID))
                .ForMember(dest => dest.VehicleKindCode, opt => opt.MapFrom(src => src.VehicleKind.Code))
                .ForMember(dest => dest.VehicleKindDesc, opt => opt.MapFrom(src => src.VehicleKind.Description))
                .ForMember(dest => dest.VehicleTypeCode, opt => opt.MapFrom(src => src.VechileColor.VechileType.VechileTypeCode))
                .ForMember(dest => dest.VehicleTypeDesc, opt => opt.MapFrom(src => src.VechileColor.VechileType.Description))
                .ForMember(dest => dest.SoldDealerID, opt => opt.MapFrom(src => src.Dealer.ID))
                .ForMember(dest => dest.SoldDealerCode, opt => opt.MapFrom(src => src.Dealer.DealerCode))
                .ForMember(dest => dest.SoldDealerName, opt => opt.MapFrom(src => src.Dealer.DealerName))
                .ForMember(dest => dest.DONumber, opt => opt.MapFrom(src => src.DONumber))
                .ForMember(dest => dest.SONumber, opt => opt.MapFrom(src => src.SONumber))
                .ForMember(dest => dest.TOPID, opt => opt.MapFrom(src => src.TermOfPayment.ID))
                .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.DiscountAmount))
                .ForMember(dest => dest.PONumber, opt => opt.MapFrom(src => src.PONumber))
                .ForMember(dest => dest.EngineNumber, opt => opt.MapFrom(src => src.EngineNumber))
                .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.SerialNumber))
                .ForMember(dest => dest.DODate, opt => opt.MapFrom(src => src.DODate))
                .ForMember(dest => dest.GIDate, opt => opt.MapFrom(src => src.GIDate))
                .ForMember(dest => dest.ParkingDays, opt => opt.MapFrom(src => src.ParkingDays))
                .ForMember(dest => dest.ParkingAmount, opt => opt.MapFrom(src => src.ParkingAmount))
                .ForMember(dest => dest.FakturStatus, opt => opt.MapFrom(src => src.FakturStatus))
                .ForMember(dest => dest.PendingDesc, opt => opt.MapFrom(src => src.PendingDesc))
                .ForMember(dest => dest.IsSAPDownload, opt => opt.MapFrom(src => src.IsSAPDownload))
                .ForMember(dest => dest.StockDealer, opt => opt.MapFrom(src => src.StockDealer))
                .ForMember(dest => dest.StockDate, opt => opt.MapFrom(src => src.StockDate))
                .ForMember(dest => dest.ProductionYear, opt => opt.MapFrom(src => src.ProductionYear))
                .ForMember(dest => dest.StockStatus, opt => opt.MapFrom(src => src.StockStatus))
                .ForMember(dest => dest.LastUpdateProfile, opt => opt.MapFrom(src => src.LastUpdateProfile))
                .ForMember(dest => dest.AlreadySaled, opt => opt.MapFrom(src => src.AlreadySaled))
                .ForMember(dest => dest.AlreadySaledTime, opt => opt.MapFrom(src => src.AlreadySaledTime))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

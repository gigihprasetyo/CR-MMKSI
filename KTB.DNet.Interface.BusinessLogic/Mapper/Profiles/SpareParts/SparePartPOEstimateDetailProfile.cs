#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPOEstimateDetailProfile mapper class
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
    public class SparePartPOEstimateDetailProfile : Profile
    {
        public SparePartPOEstimateDetailProfile()
        {
            CreateMap<SparePartPOEstimateDetail, SparePartPOEstimateDetailDto>()
                //.ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.PartNumber, opt => opt.MapFrom(src => src.PartNumber))
            .ForMember(dest => dest.PartName, opt => opt.MapFrom(src => src.PartName))
            .ForMember(dest => dest.OrderQty, opt => opt.MapFrom(src => src.OrderQty))
            .ForMember(dest => dest.AllocQty, opt => opt.MapFrom(src => src.AllocQty))
                //.ForMember(dest => dest.AllocationQty, opt => opt.MapFrom(src => src.AllocationQty))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.SparePartPOEstimate.SparePartPO.Dealer != null ? src.SparePartPOEstimate.SparePartPO.Dealer.DealerCode : ""))
                //.ForMember(dest => dest.UOM, opt => opt.MapFrom(string.Empty))
                //.ForMember(dest => dest.OpenQty, opt => opt.MapFrom(src => src.OpenQty))
            .ForMember(dest => dest.RetailPrice, opt => opt.MapFrom(src => src.RetailPrice))
            .ForMember(dest => dest.AltPartNumber, opt => opt.MapFrom(src => src.AltPartNumber))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
                //.ForMember(dest => dest.ItemStatus, opt => opt.MapFrom(src => src.ItemStatus))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SparePartPOEstimateDetailParameterDto, SparePartPOEstimateDetail>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.PartNumber, opt => opt.MapFrom(src => src.PartNumber))
            .ForMember(dest => dest.PartName, opt => opt.MapFrom(src => src.PartName))
            .ForMember(dest => dest.OrderQty, opt => opt.MapFrom(src => src.OrderQty))
            .ForMember(dest => dest.AllocQty, opt => opt.MapFrom(src => src.AllocQty))
            .ForMember(dest => dest.AllocationQty, opt => opt.MapFrom(src => src.AllocationQty))
            .ForMember(dest => dest.OpenQty, opt => opt.MapFrom(src => src.OpenQty))
            .ForMember(dest => dest.RetailPrice, opt => opt.MapFrom(src => src.RetailPrice))
            .ForMember(dest => dest.AltPartNumber, opt => opt.MapFrom(src => src.AltPartNumber))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.ItemStatus, opt => opt.MapFrom(src => src.ItemStatus))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<VWI_POEstimateHaveBillingDetail, SparePartPOEstimateDetailParameterDto>();
        }
    }
}

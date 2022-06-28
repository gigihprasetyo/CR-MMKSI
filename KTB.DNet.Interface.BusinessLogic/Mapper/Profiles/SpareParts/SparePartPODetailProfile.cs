#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPODetailProfile mapper class
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
    public class SparePartPODetailProfile : Profile
    {
        public SparePartPODetailProfile()
        {
            CreateMap<SparePartPODetail, SparePartPODetailDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                //.ForMember(dest => dest.SparePartPOID, opt => opt.MapFrom(src => src.SparePartPO.ID))
                //.ForMember(dest => dest.SparePartMasterID, opt => opt.MapFrom(src => src.SparePartMaster.ID))
            .ForMember(dest => dest.CheckListStatus, opt => opt.MapFrom(src => src.CheckListStatus))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.RetailPrice, opt => opt.MapFrom(src => src.RetailPrice))
            .ForMember(dest => dest.EstimateStatus, opt => opt.MapFrom(src => src.EstimateStatus))
            .ForMember(dest => dest.StopMark, opt => opt.MapFrom(src => src.StopMark))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForMember(dest => dest.TotalForecast, opt => opt.MapFrom(src => src.TotalForecast))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SparePartPODetailDto, SparePartPODetail>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                //.ForMember(dest => dest.SparePartPOID, opt => opt.MapFrom(src => src.SparePartPO.ID))
                //.ForMember(dest => dest.SparePartMasterID, opt => opt.MapFrom(src => src.SparePartMaster.ID))
            .ForMember(dest => dest.CheckListStatus, opt => opt.MapFrom(src => src.CheckListStatus))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.RetailPrice, opt => opt.MapFrom(src => src.RetailPrice))
            .ForMember(dest => dest.EstimateStatus, opt => opt.MapFrom(src => src.EstimateStatus))
            .ForMember(dest => dest.StopMark, opt => opt.MapFrom(src => src.StopMark))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForMember(dest => dest.TotalForecast, opt => opt.MapFrom(src => src.TotalForecast))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SparePartPODetailParameterDto, SparePartPODetail>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                //.ForMember(dest => dest.SparePartPO.ID, opt => opt.MapFrom(src => src.SparePartPOID))
                //.ForMember(dest => dest.SparePartMaster.ID, opt => opt.MapFrom(src => src.SparePartMasterID))
                //.ForMember(dest => dest.CheckListStatus, opt => opt.MapFrom(src => src.CheckListStatus))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                //.ForMember(dest => dest.RetailPrice, opt => opt.MapFrom(src => src.RetailPrice))
                //.ForMember(dest => dest.EstimateStatus, opt => opt.MapFrom(src => src.EstimateStatus))
                //.ForMember(dest => dest.StopMark, opt => opt.MapFrom(src => src.StopMark))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForMember(dest => dest.TotalForecast, opt => opt.MapFrom(src => src.TotalForecast))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SparePartPODetail, SparePartPOOtherDetailDto>()
                .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.SparePartPO.Dealer.DealerCode))
                .ForMember(dest => dest.PartNumber, opt => opt.MapFrom(src => src.SparePartMaster.PartNumber))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UoM, opt => opt.MapFrom(src => src.SparePartMaster.UoM))
                .ForMember(dest => dest.RetailPrice, opt => opt.MapFrom(src => src.RetailPrice))
                .ForMember(dest => dest.PODate, opt => opt.MapFrom(src => src.SparePartPO.PODate))
                .ForMember(dest => dest.TotalForecast, opt => opt.MapFrom(src => src.TotalForecast))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

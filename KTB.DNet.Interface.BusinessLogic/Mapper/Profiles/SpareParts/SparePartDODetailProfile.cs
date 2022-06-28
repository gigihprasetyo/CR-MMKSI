#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartDODetailProfile mapper class
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
    public class SparePartDODetailProfile : Profile
    {
        public SparePartDODetailProfile()
        {
            CreateMap<SparePartDODetail, SparePartDODetailDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.ItemNoDO, opt => opt.MapFrom(src => src.ItemNoDO))
            .ForMember(dest => dest.ItemNoSO, opt => opt.MapFrom(src => src.ItemNoSO))
            .ForMember(dest => dest.Qty, opt => opt.MapFrom(src => src.Qty))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SparePartDODetailParameterDto, SparePartDODetail>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.ItemNoDO, opt => opt.MapFrom(src => src.ItemNoDO))
            .ForMember(dest => dest.ItemNoSO, opt => opt.MapFrom(src => src.ItemNoSO))
            .ForMember(dest => dest.Qty, opt => opt.MapFrom(src => src.Qty))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SparePartDODetail, DeliveryOrderDetailResponseDto>()
            .ForMember(dest => dest.Qty, opt => opt.MapFrom(src => src.Qty))
            .ForMember(dest => dest.PartNumber, opt => opt.MapFrom(src => src.SparePartMaster.PartNumber))
            .ForMember(dest => dest.PartName, opt => opt.MapFrom(src => src.SparePartMaster.PartName))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<VWI_SparePartPODOHaveBillingDetail, DeliveryOrderDetailResponseDto>();

            CreateMap<VWI_SparePartPODOHaveBillingHeaderDetail, DeliveryOrderDetailResponseDto>();
        }
    }
}

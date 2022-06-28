#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EstimationEquipDetailProfile mapper class
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
    public class EstimationEquipDetailProfile : Profile
    {
        public EstimationEquipDetailProfile()
        {
            CreateMap<EstimationEquipDetail, EstimationEquipDetailDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Harga, opt => opt.MapFrom(src => src.Harga))
                //.ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.TotalForecast, opt => opt.MapFrom(src => src.TotalForecast))
            .ForMember(dest => dest.EstimationUnit, opt => opt.MapFrom(src => src.EstimationUnit))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ConfirmedDate, opt => opt.MapFrom(src => src.ConfirmedDate))
            .ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.Remark))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdatedBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdatedTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<EstimationEquipDetailParameterDto, EstimationEquipDetail>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                //.ForMember(dest => dest.Harga, opt => opt.MapFrom(src => src.Harga))
                //.ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.TotalForecast, opt => opt.MapFrom(src => src.TotalForecast))
            .ForMember(dest => dest.EstimationUnit, opt => opt.MapFrom(src => src.EstimationUnit))
                //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ConfirmedDate, opt => opt.MapFrom(src => src.ConfirmedDate))
                //.ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.Remark))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdatedTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

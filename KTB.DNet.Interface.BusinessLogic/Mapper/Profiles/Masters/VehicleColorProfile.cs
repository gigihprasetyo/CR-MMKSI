#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleColorProfile mapper class
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
    public class VehicleColorProfile : Profile
    {
        public VehicleColorProfile()
        {
            CreateMap<VechileColor, VehicleColorDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => src.VechileType))
                .ForMember(dest => dest.ColorCode, opt => opt.MapFrom(src => src.ColorCode))
                .ForMember(dest => dest.ColorIndName, opt => opt.MapFrom(src => src.ColorIndName))
                .ForMember(dest => dest.ColorEngName, opt => opt.MapFrom(src => src.ColorEngName))
                .ForMember(dest => dest.MaterialNumber, opt => opt.MapFrom(src => src.MaterialNumber))
                .ForMember(dest => dest.MaterialDescription, opt => opt.MapFrom(src => src.MaterialDescription))
                .ForMember(dest => dest.HeaderBOM, opt => opt.MapFrom(src => src.HeaderBOM))
                .ForMember(dest => dest.MarketCode, opt => opt.MapFrom(src => src.MarketCode))
                .ForMember(dest => dest.SpecialFlag, opt => opt.MapFrom(src => src.SpecialFlag))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

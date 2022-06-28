#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CityProfile mapper class
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
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<VWI_City, VWI_CityDto>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                    .ForMember(dest => dest.CityCode, opt => opt.MapFrom(src => src.CityCode))
                    .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.CityName))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                    .ForMember(dest => dest.ProvinceCode, opt => opt.MapFrom(src => src.ProvinceCode))
                    .ForMember(dest => dest.ProvinceName, opt => opt.MapFrom(src => src.ProvinceName))
                    .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                    .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<City, CityDto>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                    .ForMember(dest => dest.CityCode, opt => opt.MapFrom(src => src.CityCode))
                    .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.CityName))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                    .ForMember(dest => dest.ProvinceCode, opt => opt.MapFrom(src => src.Province.ProvinceCode))
                    .ForMember(dest => dest.ProvinceName, opt => opt.MapFrom(src => src.Province.ProvinceName))
                    .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                    .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

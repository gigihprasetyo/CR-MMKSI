#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PODestination mapper class
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
    class PODestinationProfile : Profile
    {
        public PODestinationProfile()
        {
            CreateMap<PODestination, PODestinationDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Nama, opt => opt.MapFrom(src => src.Nama))
            .ForMember(dest => dest.Alamat, opt => opt.MapFrom(src => src.Alamat))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.Dealer.DealerCode))
            .ForMember(dest => dest.CityCode, opt => opt.MapFrom(src => src.City.CityCode))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<PODestinationParameterDto, PODestination>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Nama, opt => opt.MapFrom(src => src.Nama))
            .ForMember(dest => dest.Alamat, opt => opt.MapFrom(src => src.Alamat))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

        }
    }
}

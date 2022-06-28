#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PartShopProfile mapper class
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
    public class PartShopProfile : Profile
    {
        public PartShopProfile()
        {
            CreateMap<PartShop, PartShopDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.PartShopCode, opt => opt.MapFrom(src => src.PartShopCode))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Fax, opt => opt.MapFrom(src => src.Fax))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<PartShopParameterDto, PartShop>()
                //.ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.PartShopCode, opt => opt.MapFrom(src => src.PartShopCode))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Fax, opt => opt.MapFrom(src => src.Fax))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<PartShopUpdateParameterDto, PartShop>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                //.ForMember(dest => dest.PartShopCode, opt => opt.MapFrom(src => src.PartShopCode))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Fax, opt => opt.MapFrom(src => src.Fax))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<VWI_PartShop, VPartShopDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.CityCode, opt => opt.MapFrom(src => src.CityCode))
            .ForMember(dest => dest.PartShopCode, opt => opt.MapFrom(src => src.PartShopCode))
            .ForMember(dest => dest.OldPartShopCode, opt => opt.MapFrom(src => src.OldPartShopCode))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Fax, opt => opt.MapFrom(src => src.Fax))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

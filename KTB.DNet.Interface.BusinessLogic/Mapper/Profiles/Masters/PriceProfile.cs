#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PriceProfile mapper class
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
    public class PriceProfile : Profile
    {
        public PriceProfile()
        {
            CreateMap<Price, PriceDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                //.ForMember(dest => dest.DealerID, opt => opt.MapFrom(src => src.DealerID))
                .ForMember(dest => dest.ValidFrom, opt => opt.MapFrom(src => src.ValidFrom))
                .ForMember(dest => dest.BasePrice, opt => opt.MapFrom(src => src.BasePrice))
                .ForMember(dest => dest.OptionPrice, opt => opt.MapFrom(src => src.OptionPrice))
                .ForMember(dest => dest.PPN_BM, opt => opt.MapFrom(src => src.PPN_BM))
                .ForMember(dest => dest.PPN, opt => opt.MapFrom(src => src.PPN))
                .ForMember(dest => dest.PPh22, opt => opt.MapFrom(src => src.PPh22))
                .ForMember(dest => dest.Interest, opt => opt.MapFrom(src => src.Interest))
                .ForMember(dest => dest.FactoringInt, opt => opt.MapFrom(src => src.FactoringInt))
                .ForMember(dest => dest.PPh23, opt => opt.MapFrom(src => src.PPh23))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.DiscountReward, opt => opt.MapFrom(src => src.DiscountReward))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

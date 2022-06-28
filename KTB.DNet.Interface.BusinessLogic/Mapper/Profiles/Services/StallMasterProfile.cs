#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PDIProfile mapper class
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
    public class StallMasterProfile : Profile
    {
        public StallMasterProfile()
        {
            CreateMap<StallMaster, StallMasterDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.DealerID, opt => opt.MapFrom(src => src.Dealer.ID))
                .ForMember(dest => dest.StallCode, opt => opt.MapFrom(src => src.StallCode))
                .ForMember(dest => dest.StallCodeDealer, opt => opt.MapFrom(src => src.StallCodeDealer))
                .ForMember(dest => dest.StallName, opt => opt.MapFrom(src => src.StallName))
                .ForMember(dest => dest.StallLocation, opt => opt.MapFrom(src => src.StallLocation))
                .ForMember(dest => dest.StallType, opt => opt.MapFrom(src => src.StallType))
                .ForMember(dest => dest.StallCategory, opt => opt.MapFrom(src => src.StallCategory))
                .ForMember(dest => dest.IsBodyPaint, opt => opt.MapFrom(src => src.IsBodyPaint))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdatedBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdatedTime))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<StallMasterParameterDto, StallMaster>()
                .ForMember(dest => dest.StallCodeDealer, opt => opt.MapFrom(src => src.StallCodeDealer))
                .ForMember(dest => dest.StallName, opt => opt.MapFrom(src => src.StallName))
                .ForMember(dest => dest.StallLocation, opt => opt.MapFrom(src => src.StallLocation))
                .ForMember(dest => dest.StallType, opt => opt.MapFrom(src => src.StallType))
                .ForMember(dest => dest.StallCategory, opt => opt.MapFrom(src => src.StallCategory))
                .ForMember(dest => dest.IsBodyPaint, opt => opt.MapFrom(src => src.IsBodyPaint))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdatedTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<StallMasterUpdateParameterDto, StallMaster>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.StallCode, opt => opt.MapFrom(src => src.StallCode))
                .ForMember(dest => dest.StallCodeDealer, opt => opt.MapFrom(src => src.StallCodeDealer))
                .ForMember(dest => dest.StallName, opt => opt.MapFrom(src => src.StallName))
                .ForMember(dest => dest.StallLocation, opt => opt.MapFrom(src => src.StallLocation))
                .ForMember(dest => dest.StallType, opt => opt.MapFrom(src => src.StallType))
                .ForMember(dest => dest.StallCategory, opt => opt.MapFrom(src => src.StallCategory))
                .ForMember(dest => dest.IsBodyPaint, opt => opt.MapFrom(src => src.IsBodyPaint))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdatedTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());

        }
    }
}

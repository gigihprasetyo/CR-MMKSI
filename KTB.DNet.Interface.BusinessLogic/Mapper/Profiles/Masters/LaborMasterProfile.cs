﻿#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : LaborMasterProfile mapper class
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
    public class LaborMasterProfile : Profile
    {
        public LaborMasterProfile()
        {
            CreateMap<LaborMaster, LaborMasterDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.LaborCode, opt => opt.MapFrom(src => src.LaborCode))
                .ForMember(dest => dest.WorkCode, opt => opt.MapFrom(src => src.WorkCode))
                .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => src.VechileType))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleTypeProfile mapper class
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
    public class VehicleTypeProfile : Profile
    {
        public VehicleTypeProfile()
        {
            // CreateMap <source, destination>()
            CreateMap<VechileType, VehicleTypeDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.VehicleTypeCode, opt => opt.MapFrom(src => src.VechileTypeCode))
                .ForMember(dest => dest.VehicleClass, opt => opt.MapFrom(src => src.VehicleClass))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.VehicleModel, opt => opt.MapFrom(src => src.VechileModel))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.IsVehicleKind1, opt => opt.MapFrom(src => src.IsVehicleKind1))
                .ForMember(dest => dest.IsVehicleKind2, opt => opt.MapFrom(src => src.IsVehicleKind2))
                .ForMember(dest => dest.IsVehicleKind3, opt => opt.MapFrom(src => src.IsVehicleKind3))
                .ForMember(dest => dest.IsVehicleKind4, opt => opt.MapFrom(src => src.IsVehicleKind4))
                .ForMember(dest => dest.MaxTOPDays, opt => opt.MapFrom(src => src.MaxTOPDays))
                //.ForMember(dest => dest.SAPModel, opt => opt.MapFrom(src => src.SAPModel))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

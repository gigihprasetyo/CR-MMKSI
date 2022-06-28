#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMasterATA Profile mapper class
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
    public class ChassisMasterATAProfile : Profile
    {
        public ChassisMasterATAProfile()
        {
            CreateMap<ChassisMasterATA, ChassisMasterATADto>()
                .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisMaster.ChassisNumber))
                .ForMember(dest => dest.ETADate, opt => opt.MapFrom(src => src.ETA))
                .ForMember(dest => dest.ATADate, opt => opt.MapFrom(src => src.ATA))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

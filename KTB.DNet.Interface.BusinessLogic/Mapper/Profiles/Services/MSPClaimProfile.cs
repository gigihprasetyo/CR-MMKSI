#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : MSPClaimProfile mapper class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 9/11/2018 9:51
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
    public class MSPClaimProfile : Profile
    {
        public MSPClaimProfile()
        {
            CreateMap<MSPClaim, MSPClaimDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisMaster.ChassisNumber))
            .ForMember(dest => dest.StandKM, opt => opt.MapFrom(src => src.StandKM))
            .ForMember(dest => dest.EngineNumber, opt => opt.MapFrom(src => src.ChassisMaster.EngineNumber))
            .ForMember(dest => dest.PMKindCode, opt => opt.MapFrom(src => src.PMKind.KindCode))
            .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType))
            .ForMember(dest => dest.ServiceDate, opt => opt.MapFrom(src => src.ServiceDate))
            .ForMember(dest => dest.Remarks, opt => opt.MapFrom(src => src.Remarks))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<MSPClaimParameterDto, MSPClaim>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                //.ForMember(dest => dest.ChassisMaster.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
            .ForMember(dest => dest.StandKM, opt => opt.MapFrom(src => src.StandKM))
            .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType))
            .ForMember(dest => dest.ServiceDate, opt => opt.MapFrom(src => src.ServiceDate))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

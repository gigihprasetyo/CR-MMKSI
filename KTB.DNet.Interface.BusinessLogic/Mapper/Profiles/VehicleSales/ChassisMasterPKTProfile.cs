#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMasterPKTProfile mapper class
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
using KTB.DNet.Interface.Model.Parameters;
#endregion

namespace KTB.DNet.Interface.BusinessLogic.MapperBL
{
    public class ChassisMasterPKTProfile : Profile
    {
        public ChassisMasterPKTProfile()
        {
            CreateMap<ChassisMasterPKT, ChassisMasterPKTDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ChassisMasterID, opt => opt.MapFrom(src => src.ChassisMaster.ID))
                .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisMaster.ChassisNumber))
                .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.ChassisMaster.Dealer.DealerCode))
                .ForMember(dest => dest.PKTDate, opt => opt.MapFrom(src => src.PKTDate))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<ChassisMasterPKTParameterDto, ChassisMasterPKT>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.PKTDate, opt => opt.MapFrom(src => src.PKTDate))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
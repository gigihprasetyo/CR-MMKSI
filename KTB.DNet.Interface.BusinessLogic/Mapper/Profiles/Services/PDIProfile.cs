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
using System;
#endregion

namespace KTB.DNet.Interface.BusinessLogic.MapperBL
{
    public class PDIProfile : Profile
    {
        public PDIProfile()
        {
            CreateMap<PDI, PDIDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisMaster.ChassisNumber))
                .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.Dealer.DealerCode))
                .ForMember(dest => dest.DealerBranchCode, opt => opt.MapFrom(src => src.DealerBranch.DealerBranchCode))
                .ForMember(dest => dest.Kind, opt => opt.MapFrom(src => src.Kind))
                .ForMember(dest => dest.PDIStatus, opt => opt.MapFrom(src => src.PDIStatus))
                .ForMember(dest => dest.PDIDate, opt => opt.MapFrom(src => src.PDIDate.Date))
                .ForMember(dest => dest.ReleaseBy, opt => opt.MapFrom(src => src.ReleaseBy))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate.Date))
                .ForMember(dest => dest.WorkOrderNumber, opt => opt.MapFrom(src => src.WorkOrderNumber))
                .ForMember(dest => dest.PilotingPDI, opt => opt.MapFrom(src => src.PilotingPDI))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime.Date))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime.Date))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<PDIParameterDto, PDI>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Kind, opt => opt.MapFrom(src => src.Kind))
                .ForMember(dest => dest.PDIStatus, opt => opt.MapFrom(src => src.PDIStatus))
                .ForMember(dest => dest.PDIDate, opt => opt.MapFrom(src => src.PDIDate))
                .ForMember(dest => dest.ReleaseBy, opt => opt.MapFrom(src => src.ReleaseBy))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(dest => dest.WorkOrderNumber, opt => opt.MapFrom(src => src.WorkOrderNumber))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

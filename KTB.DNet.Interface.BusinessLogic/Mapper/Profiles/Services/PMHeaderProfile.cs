#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PMHeaderProfile mapper class
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
    public class PMHeaderProfile : Profile
    {
        public PMHeaderProfile()
        {
            CreateMap<PMHeaderParameterDto, PMHeader>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.StandKM, opt => opt.MapFrom(src => src.StandKM))
                .ForMember(dest => dest.ServiceDate, opt => opt.MapFrom(src => src.ServiceDate))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(dest => dest.PMStatus, opt => opt.MapFrom(src => src.PMStatus))
                .ForMember(dest => dest.EntryType, opt => opt.MapFrom(src => src.EntryType))
                .ForMember(dest => dest.WorkOrderNumber, opt => opt.MapFrom(src => src.WorkOrderNumber))
                .ForMember(dest => dest.BookingNo, opt => opt.MapFrom(src => src.BookingNo))
                .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
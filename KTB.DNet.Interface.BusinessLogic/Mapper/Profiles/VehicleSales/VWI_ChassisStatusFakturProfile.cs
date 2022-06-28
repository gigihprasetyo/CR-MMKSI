#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomerProfile mapper class
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
    public class VWI_ChassisStatusFakturProfile : Profile
    {
        public VWI_ChassisStatusFakturProfile()
        {
            CreateMap<VWI_ChassisStatusFaktur, VWI_ChassisStatusFakturDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
                .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
                .ForMember(dest => dest.DealerName, opt => opt.MapFrom(src => src.DealerName))
                .ForMember(dest => dest.DownloadDate, opt => opt.MapFrom(src => src.DownloadDate))
                .ForMember(dest => dest.FakturDate, opt => opt.MapFrom(src => src.FakturDate))
                .ForMember(dest => dest.FakturStatus, opt => opt.MapFrom(src => src.FakturStatus))
                .ForMember(dest => dest.FakturNumber, opt => opt.MapFrom(src => src.FakturNumber))
                .ForMember(dest => dest.OpenFakturDate, opt => opt.MapFrom(src => src.OpenFakturDate))
                .ForMember(dest => dest.PrintedDate, opt => opt.MapFrom(src => src.PrintedDate))
                .ForMember(dest => dest.RevisionDate, opt => opt.MapFrom(src => src.RevisionDate))
                .ForMember(dest => dest.RevisionStatus, opt => opt.MapFrom(src => src.RevisionStatus))
                .ForMember(dest => dest.RevisionType, opt => opt.MapFrom(src => src.RevisionType))
                .ForMember(dest => dest.SPKNumber, opt => opt.MapFrom(src => src.SPKNumber))
                .ForMember(dest => dest.DealerSPKNumber, opt => opt.MapFrom(src => src.DealerSPKNumber))
                .ForMember(dest => dest.ValidateDate, opt => opt.MapFrom(src => src.ValidateDate))
                .ForMember(dest => dest.EffectiveDate, opt => opt.MapFrom(src => src.EffectiveDate))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(dest => dest.ETDDate, opt => opt.MapFrom(src => src.ETDDate))
                .ForMember(dest => dest.ATDDate, opt => opt.MapFrom(src => src.ATDDate))
                .ForMember(dest => dest.ETADate, opt => opt.MapFrom(src => src.ETADate))
                .ForMember(dest => dest.ATADate, opt => opt.MapFrom(src => src.ATADate))
                .ForAllOtherMembers(opt => opt.Ignore());

        }


    }
}

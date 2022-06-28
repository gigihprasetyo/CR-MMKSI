#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CarrosserieDetailProfile mapper class
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
    public class CarrosserieDetailProfile : Profile
    {
        public CarrosserieDetailProfile()
        {
            CreateMap<CarrosserieDetail, CarrosserieDetailDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.PDIStateCode, opt => opt.MapFrom(src => src.PDIStateCode))
            .ForMember(dest => dest.PDIStatusCode, opt => opt.MapFrom(src => src.PDIStatusCode))
            .ForMember(dest => dest.AccessorriesDescription, opt => opt.MapFrom(src => src.AccessorriesDescription))
            .ForMember(dest => dest.AccessorriesName, opt => opt.MapFrom(src => src.AccessorriesName))
            .ForMember(dest => dest.BUCode, opt => opt.MapFrom(src => src.BUCode))
            .ForMember(dest => dest.BUName, opt => opt.MapFrom(src => src.BUName))
            .ForMember(dest => dest.KITName, opt => opt.MapFrom(src => src.KITName))
            .ForMember(dest => dest.PBUCode, opt => opt.MapFrom(src => src.PBUCode))
            .ForMember(dest => dest.PBUName, opt => opt.MapFrom(src => src.PBUName))
            .ForMember(dest => dest.PDIDetailName, opt => opt.MapFrom(src => src.PDIDetailName))
            .ForMember(dest => dest.PDIReceiptDetailNo, opt => opt.MapFrom(src => src.PDIReceiptDetailNo))
            .ForMember(dest => dest.PDIReceiptName, opt => opt.MapFrom(src => src.PDIReceiptName))
            .ForMember(dest => dest.ReceiveQuantity, opt => opt.MapFrom(src => src.ReceiveQuantity))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<CarrosserieDetailParameterDto, CarrosserieDetail>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.PDIStateCode, opt => opt.MapFrom(src => src.PDIStateCode))
            .ForMember(dest => dest.PDIStatusCode, opt => opt.MapFrom(src => src.PDIStatusCode))
            .ForMember(dest => dest.AccessorriesDescription, opt => opt.MapFrom(src => src.AccessorriesDescription))
            .ForMember(dest => dest.AccessorriesName, opt => opt.MapFrom(src => src.AccessorriesName))
            .ForMember(dest => dest.BUCode, opt => opt.MapFrom(src => src.BUCode))
            .ForMember(dest => dest.BUName, opt => opt.MapFrom(src => src.BUName))
            .ForMember(dest => dest.KITName, opt => opt.MapFrom(src => src.KITName))
            .ForMember(dest => dest.PBUCode, opt => opt.MapFrom(src => src.PBUCode))
            .ForMember(dest => dest.PBUName, opt => opt.MapFrom(src => src.PBUName))
            .ForMember(dest => dest.PDIDetailName, opt => opt.MapFrom(src => src.PDIDetailName))
            .ForMember(dest => dest.PDIReceiptDetailNo, opt => opt.MapFrom(src => src.PDIReceiptDetailNo))
            .ForMember(dest => dest.PDIReceiptName, opt => opt.MapFrom(src => src.PDIReceiptName))
            .ForMember(dest => dest.ReceiveQuantity, opt => opt.MapFrom(src => src.ReceiveQuantity))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

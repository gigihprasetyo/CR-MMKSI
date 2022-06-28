#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CarrosserieHeaderProfile mapper class
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
    public class CarrosserieHeaderProfile : Profile
    {
        public CarrosserieHeaderProfile()
        {
            CreateMap<CarrosserieHeader, CarrosserieHeaderDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.PDIStateCode, opt => opt.MapFrom(src => src.PDIStateCode))
            .ForMember(dest => dest.PDIStatusCode, opt => opt.MapFrom(src => src.PDIStatusCode))
            .ForMember(dest => dest.BUCode, opt => opt.MapFrom(src => src.BUCode))
            .ForMember(dest => dest.BUName, opt => opt.MapFrom(src => src.BUName))
            .ForMember(dest => dest.PDIName, opt => opt.MapFrom(src => src.PDIName))
            .ForMember(dest => dest.PDIReceiptNo, opt => opt.MapFrom(src => src.PDIReceiptNo))
            .ForMember(dest => dest.PDIReceiptRefName, opt => opt.MapFrom(src => src.PDIReceiptRefName))
            .ForMember(dest => dest.PDIReceiptStatus, opt => opt.MapFrom(src => src.PDIReceiptStatus))
            .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate))
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType))
            .ForMember(dest => dest.VendorName, opt => opt.MapFrom(src => src.VendorName))
            .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<CarrosserieHeaderParameterDto, CarrosserieHeader>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.PDIStateCode, opt => opt.MapFrom(src => src.PDIStateCode))
            .ForMember(dest => dest.PDIStatusCode, opt => opt.MapFrom(src => src.PDIStatusCode))
            .ForMember(dest => dest.BUCode, opt => opt.MapFrom(src => src.BUCode))
            .ForMember(dest => dest.BUName, opt => opt.MapFrom(src => src.BUName))
            .ForMember(dest => dest.PDIName, opt => opt.MapFrom(src => src.PDIName))
            .ForMember(dest => dest.PDIReceiptNo, opt => opt.MapFrom(src => src.PDIReceiptNo))
            .ForMember(dest => dest.PDIReceiptRefName, opt => opt.MapFrom(src => src.PDIReceiptRefName))
            .ForMember(dest => dest.PDIReceiptStatus, opt => opt.MapFrom(src => src.PDIReceiptStatus))
            .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate))
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType))
            .ForMember(dest => dest.VendorName, opt => opt.MapFrom(src => src.VendorName))
            .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

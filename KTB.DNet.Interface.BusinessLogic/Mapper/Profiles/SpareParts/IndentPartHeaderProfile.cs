#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : IndentPartHeaderProfile mapper class
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
    public class IndentPartHeaderProfile : Profile
    {
        public IndentPartHeaderProfile()
        {
            CreateMap<IndentPartHeader, IndentPartHeaderDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Dealer, opt => opt.MapFrom(src => src.Dealer))
            .ForMember(dest => dest.RequestNo, opt => opt.MapFrom(src => src.RequestNo))
            .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => src.RequestDate))
            .ForMember(dest => dest.MaterialType, opt => opt.MapFrom(src => src.MaterialType))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.StatusKTB, opt => opt.MapFrom(src => src.StatusKTB))
            .ForMember(dest => dest.SubmitFile, opt => opt.MapFrom(src => src.SubmitFile))
            .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.KTBConfirmedDate, opt => opt.MapFrom(src => src.KTBConfirmedDate))
            .ForMember(dest => dest.DescID, opt => opt.MapFrom(src => src.DescID))
            .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
            .ForMember(dest => dest.DMSPRNo, opt => opt.MapFrom(src => src.DMSPRNo))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<IndentPartHeaderParameterDto, IndentPartHeader>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.RequestNo, opt => opt.MapFrom(src => src.RequestNo))
            .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => src.RequestDate))
            .ForMember(dest => dest.MaterialType, opt => opt.MapFrom(src => src.MaterialType))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.StatusKTB, opt => opt.MapFrom(src => src.StatusKTB))
            .ForMember(dest => dest.SubmitFile, opt => opt.MapFrom(src => src.SubmitFile))
            .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.KTBConfirmedDate, opt => opt.MapFrom(src => src.KTBConfirmedDate))
            .ForMember(dest => dest.DescID, opt => opt.MapFrom(src => src.DescID))
            .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
            .ForMember(dest => dest.DMSPRNo, opt => opt.MapFrom(src => src.DMSPRNo))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<IndentPartHeaderCreateParameterDto, IndentPartHeaderParameterDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.RequestNo, opt => opt.MapFrom(src => src.RequestNo))
            .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => src.RequestDate))
            .ForMember(dest => dest.MaterialType, opt => opt.MapFrom(src => src.MaterialType))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.StatusKTB, opt => opt.MapFrom(src => src.StatusKTB))
            .ForMember(dest => dest.SubmitFile, opt => opt.MapFrom(src => src.SubmitFile))
            .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.KTBConfirmedDate, opt => opt.MapFrom(src => src.KTBConfirmedDate))
            .ForMember(dest => dest.DescID, opt => opt.MapFrom(src => src.DescID))
            .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
            .ForMember(dest => dest.DMSPRNo, opt => opt.MapFrom(src => src.DMSPRNo))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
           .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<IndentPartHeader, IndentPartHeaderCreateResponseDto>()
            .ForMember(dest => dest.RequestNo, opt => opt.MapFrom(src => src.RequestNo))
            .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => src.RequestDate))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<IndentPartHeaderUpdateParameterDto, IndentPartHeaderParameterDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.RequestNo, opt => opt.MapFrom(src => src.RequestNo))
            .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => src.RequestDate))
            .ForMember(dest => dest.MaterialType, opt => opt.MapFrom(src => src.MaterialType))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.StatusKTB, opt => opt.MapFrom(src => src.StatusKTB))
            .ForMember(dest => dest.SubmitFile, opt => opt.MapFrom(src => src.SubmitFile))
            .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.KTBConfirmedDate, opt => opt.MapFrom(src => src.KTBConfirmedDate))
            .ForMember(dest => dest.DescID, opt => opt.MapFrom(src => src.DescID))
            .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisNumber))
            .ForMember(dest => dest.DMSPRNo, opt => opt.MapFrom(src => src.DMSPRNo))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
           .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

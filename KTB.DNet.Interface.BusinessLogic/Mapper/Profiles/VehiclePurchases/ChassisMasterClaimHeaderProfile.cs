#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMasterClaimHeaderProfile mapper class
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
    class ChassisMasterClaimHeaderProfile : Profile
    {
        public ChassisMasterClaimHeaderProfile()
        {
            CreateMap<ChassisMasterClaimHeader, ChassisMastertClaimHeaderDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.Dealer.DealerCode))
            .ForMember(dest => dest.ClaimNumber, opt => opt.MapFrom(src => src.ClaimNumber))
            .ForMember(dest => dest.StatusID, opt => opt.MapFrom(src => src.StatusID))
            .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisMaster.ChassisNumber))
            .ForMember(dest => dest.ResponClaim, opt => opt.MapFrom(src => src.ResponClaim))
            .ForMember(dest => dest.ChassisNumberReplacement, opt => opt.MapFrom(src => src.ChassisNumberReplacement))
            .ForMember(dest => dest.ReporterIssue, opt => opt.MapFrom(src => src.ReporterIssue))
            .ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.Remark))
            .ForMember(dest => dest.StatusProcessRetur, opt => opt.MapFrom(src => src.StatusProcessRetur))
            .ForMember(dest => dest.EstimationRepairDate, opt => opt.MapFrom(src => src.RepairEstimationDate))
            .ForMember(dest => dest.ActualRepairDate, opt => opt.MapFrom(src => src.CompletionDate))
            .ForMember(dest => dest.Nominal, opt => opt.MapFrom(src => src.Nominal))
            .ForMember(dest => dest.SORetur, opt => opt.MapFrom(src => src.SORetur))
            .ForMember(dest => dest.DORetur, opt => opt.MapFrom(src => src.DORetur))
            .ForMember(dest => dest.BillingRetur, opt => opt.MapFrom(src => src.BillingRetur))
            .ForMember(dest => dest.SONormalRetur, opt => opt.MapFrom(src => src.SONormalRetur))
            .ForMember(dest => dest.DONormalRetur, opt => opt.MapFrom(src => src.DONormalRetur))
            .ForMember(dest => dest.BillingNormalRetur, opt => opt.MapFrom(src => src.BillingNormalRetur))
            .ForMember(dest => dest.TransferDate, opt => opt.MapFrom(src => src.TransferDate))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<ChassisMasterClaimHeaderParameterDto, ChassisMasterClaimHeader>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.DealerPIC, opt => opt.MapFrom(src => src.DealerPIC))
            .ForMember(dest => dest.ReporterIssue, opt => opt.MapFrom(src => src.ReporterIssue))
            .ForMember(dest => dest.ClaimDate, opt => opt.MapFrom(src => src.ClaimDate))
            .ForMember(dest => dest.StatusID, opt => opt.MapFrom(src => src.StatusID))
            .ForMember(dest => dest.DateOccur, opt => opt.MapFrom(src => src.DateOccur))
            .ForMember(dest => dest.PlaceOccur, opt => opt.MapFrom(src => src.PlaceOccur))
            .ForMember(dest => dest.StatusStockDMS, opt => opt.MapFrom(src => src.StatusStockDMS))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTIme, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForMember(dest => dest.ChassisMasterClaimDetails, opt => opt.MapFrom(src => src.ChassisMasterClaimDetails))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<ChassisMasterClaimHeaderUpdateParameterDto, ChassisMasterClaimHeader>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.DealerPIC, opt => opt.MapFrom(src => src.DealerPIC))
            .ForMember(dest => dest.ReporterIssue, opt => opt.MapFrom(src => src.ReporterIssue))
            .ForMember(dest => dest.ClaimDate, opt => opt.MapFrom(src => src.ClaimDate))
            .ForMember(dest => dest.StatusID, opt => opt.MapFrom(src => src.StatusID))
            .ForMember(dest => dest.DateOccur, opt => opt.MapFrom(src => src.DateOccur))
            .ForMember(dest => dest.ClaimNumber, opt => opt.MapFrom(src => src.ClaimNumber))
            .ForMember(dest => dest.PlaceOccur, opt => opt.MapFrom(src => src.PlaceOccur))
            .ForMember(dest => dest.StatusStockDMS, opt => opt.MapFrom(src => src.StatusStockDMS))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTIme, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForMember(dest => dest.ChassisMasterClaimDetails, opt => opt.MapFrom(src => src.ChassisMasterClaimDetails))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<ChassisMasterClaimHeader, ChassisMastertClaimHeaderCreateResponseDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.ClaimNumber, opt => opt.MapFrom(src => src.ClaimNumber))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTIme))
            .ForMember(dest => dest.ChassisMasterClaimDetails, opt => opt.MapFrom(src => src.ChassisMasterClaimDetails))
            .ForMember(dest => dest.DocumentUpload, opt => opt.MapFrom(src => src.DocumentUploads))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<ChassisMasterClaimHeader, ChassisMastertClaimHeaderUpdateResponseDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.ClaimNumber, opt => opt.MapFrom(src => src.ClaimNumber))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTIme))
             .ForMember(dest => dest.ChassisMasterClaimDetails, opt => opt.MapFrom(src => src.ChassisMasterClaimDetails))
            .ForMember(dest => dest.DocumentUpload, opt => opt.MapFrom(src => src.DocumentUploads))
            .ForAllOtherMembers(opt => opt.Ignore());

        }
    }
}

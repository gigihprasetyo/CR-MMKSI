#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistPartStockProfile mapper class
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
    public class AssistPartStockProfile : Profile
    {
        public AssistPartStockProfile()
        {
            CreateMap<AssistPartStock, AssistPartStockDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.AssistUploadLogID, opt => opt.MapFrom(src => src.AssistUploadLog.ID))
            .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Month))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                //.ForMember(dest => dest.DealerID, opt => opt.MapFrom(src => src.Dealer.ID))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.DealerBranchCode, opt => opt.MapFrom(src => src.DealerBranchCode))
            .ForMember(dest => dest.SparepartMasterID, opt => opt.MapFrom(src => src.SparePartMaster.ID))
            .ForMember(dest => dest.NoParts, opt => opt.MapFrom(src => src.NoParts))
            .ForMember(dest => dest.JumlahStokAwal, opt => opt.MapFrom(src => src.JumlahStokAwal))
            .ForMember(dest => dest.JumlahDatang, opt => opt.MapFrom(src => src.JumlahDatang))
            .ForMember(dest => dest.HargaBeli, opt => opt.MapFrom(src => src.HargaBeli))
            .ForMember(dest => dest.RemarksSystem, opt => opt.MapFrom(src => src.RemarksSystem))
            .ForMember(dest => dest.StatusAktif, opt => opt.MapFrom(src => src.StatusAktif))
            .ForMember(dest => dest.ValidateSystemStatus, opt => opt.MapFrom(src => src.ValidateSystemStatus))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<AssistPartStockParameterDto, AssistPartStock>();
        }
    }
}

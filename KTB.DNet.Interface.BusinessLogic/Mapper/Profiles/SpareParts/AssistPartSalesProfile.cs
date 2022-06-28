#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistPartSalesProfile mapper class
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
    public class AssistPartSalesProfile : Profile
    {
        public AssistPartSalesProfile()
        {
            CreateMap<AssistPartSales, AssistPartSalesDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                //.ForMember(dest => dest.AssistUploadLogID, opt => opt.MapFrom(src => src.AssistUploadLogID))
            .ForMember(dest => dest.TglTransaksi, opt => opt.MapFrom(src => src.TglTransaksi))
                //.ForMember(dest => dest.DealerID, opt => opt.MapFrom(src => src.DealerID))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.KodeCustomer, opt => opt.MapFrom(src => src.KodeCustomer))
                //.ForMember(dest => dest.SalesChannelID, opt => opt.MapFrom(src => src.SalesChannelID))
            .ForMember(dest => dest.SalesChannelCode, opt => opt.MapFrom(src => src.SalesChannelCode))
            .ForMember(dest => dest.TrTraineeSalesSparepartID, opt => opt.MapFrom(src => src.TrTraineeSalesSparepartID))
            .ForMember(dest => dest.SalesmanHeaderID, opt => opt.MapFrom(src => src.SalesmanHeaderID))
            .ForMember(dest => dest.KodeSalesman, opt => opt.MapFrom(src => src.KodeSalesman))
            .ForMember(dest => dest.NoWorkOrder, opt => opt.MapFrom(src => src.NoWorkOrder))
                //.ForMember(dest => dest.SparepartMasterID, opt => opt.MapFrom(src => src.SparepartMasterID))
            .ForMember(dest => dest.NoParts, opt => opt.MapFrom(src => src.NoParts))
            .ForMember(dest => dest.Qty, opt => opt.MapFrom(src => src.Qty))
            .ForMember(dest => dest.HargaBeli, opt => opt.MapFrom(src => src.HargaBeli))
            .ForMember(dest => dest.HargaJual, opt => opt.MapFrom(src => src.HargaJual))
            .ForMember(dest => dest.RemarksSystem, opt => opt.MapFrom(src => src.RemarksSystem))
            .ForMember(dest => dest.StatusAktif, opt => opt.MapFrom(src => src.StatusAktif))
            .ForMember(dest => dest.ValidateSystemStatus, opt => opt.MapFrom(src => src.ValidateSystemStatus))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<AssistPartSalesParameterDto, AssistPartSales>();

            CreateMap<AssistPartSales, AssistPartSalesReadDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                //.ForMember(dest => dest.AssistUploadLogID, opt => opt.MapFrom(src => src.AssistUploadLogID))
            .ForMember(dest => dest.TglTransaksi, opt => opt.MapFrom(src => src.TglTransaksi))
                //.ForMember(dest => dest.DealerID, opt => opt.MapFrom(src => src.DealerID))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.KodeCustomer, opt => opt.MapFrom(src => src.KodeCustomer))
                //.ForMember(dest => dest.SalesChannelID, opt => opt.MapFrom(src => src.SalesChannelID))
            .ForMember(dest => dest.SalesChannelCode, opt => opt.MapFrom(src => src.SalesChannelCode))
            .ForMember(dest => dest.TrTraineeSalesSparepartID, opt => opt.MapFrom(src => src.TrTraineeSalesSparepartID))
            //.ForMember(dest => dest.SalesmanHeaderID, opt => opt.MapFrom(src => src.SalesmanHeaderID))
            .ForMember(dest => dest.KodeSalesman, opt => opt.MapFrom(src => src.KodeSalesman))
            .ForMember(dest => dest.NoWorkOrder, opt => opt.MapFrom(src => src.NoWorkOrder))
                //.ForMember(dest => dest.SparepartMasterID, opt => opt.MapFrom(src => src.SparepartMasterID))
            .ForMember(dest => dest.NoParts, opt => opt.MapFrom(src => src.NoParts))
            .ForMember(dest => dest.Qty, opt => opt.MapFrom(src => src.Qty))
            .ForMember(dest => dest.HargaBeli, opt => opt.MapFrom(src => src.HargaBeli))
            .ForMember(dest => dest.HargaJual, opt => opt.MapFrom(src => src.HargaJual))
            .ForMember(dest => dest.RemarksSystem, opt => opt.MapFrom(src => src.RemarksSystem))
            .ForMember(dest => dest.StatusAktif, opt => opt.MapFrom(src => src.StatusAktif))
            .ForMember(dest => dest.ValidateSystemStatus, opt => opt.MapFrom(src => src.ValidateSystemStatus))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdatedTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());


            //CreateMap<AssistPartSalesParameterDto, AssistPartSales>().ConvertUsing(new AssistPartSalesConverter());
        }


    }

    //public class AssistPartSalesConverter : ITypeConverter<AssistPartSalesParameterDto, AssistPartSales>
    //{

    //    public AssistPartSales Convert(AssistPartSalesParameterDto source, AssistPartSales destination, ResolutionContext context)
    //    {    
    //        destination.TglTransaksi  = source.TglTransaksi;
    //        destination.DealerCode  = source.DealerCode;
    //        destination.KodeCustomer  = source.KodeCustomer;
    //        destination.SalesChannelCode  = source.SalesChannelCode;
    //        destination.KodeSalesman  = source.KodeSalesman;
    //        destination.NoWorkOrder  = source.NoWorkOrder;;
    //        destination.NoParts  = source.NoParts;
    //        destination.Qty  = source.Qty;
    //        destination.HargaBeli  = source.HargaBeli;
    //        destination.HargaJual  = source.HargaJual;
    //        destination.IsCampaign  = source.IsCampaign;
    //        destination.CampaignNo  = source.CampaignNo;
    //        destination.CampaignDescription  = source.CampaignDescription;
    //        destination.DealerBranchCode  = source.DealerBranchCode;
    //        destination.RemarksSystem  = source.RemarksSystem;
    //        destination.StatusAktif  = source.StatusAktif;
    //        destination.ValidateSystemStatus  = source.ValidateSystemStatus;

    //        return destination;
    //    }
    //}
}
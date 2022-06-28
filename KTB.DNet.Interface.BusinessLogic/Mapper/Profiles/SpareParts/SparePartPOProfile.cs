#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPOProfile mapper class
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
    public class SparePartPOProfile : Profile
    {
        public SparePartPOProfile()
        {
            CreateMap<SparePartPO, SparePartPODto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.PONumber, opt => opt.MapFrom(src => src.PONumber))
            .ForMember(dest => dest.OrderType, opt => opt.MapFrom(src => src.OrderType))
            .ForMember(dest => dest.PODate, opt => opt.MapFrom(src => src.PODate))
            .ForMember(dest => dest.DeliveryDate, opt => opt.MapFrom(src => src.DeliveryDate))
            .ForMember(dest => dest.ProcessCode, opt => opt.MapFrom(src => src.ProcessCode))
            .ForMember(dest => dest.CancelRequestBy, opt => opt.MapFrom(src => src.CancelRequestBy))
            .ForMember(dest => dest.IndentTransfer, opt => opt.MapFrom(src => src.IndentTransfer))
            .ForMember(dest => dest.PickingTicket, opt => opt.MapFrom(src => src.PickingTicket))
            .ForMember(dest => dest.SentPODate, opt => opt.MapFrom(src => src.SentPODate))
            .ForMember(dest => dest.IsTransfer, opt => opt.MapFrom(src => src.IsTransfer))
            .ForMember(dest => dest.DMSPRNo, opt => opt.MapFrom(src => src.DMSPRNo))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SparePartPOParameterDto, SparePartPO>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.OrderType, opt => opt.MapFrom(src => src.OrderType))
            .ForMember(dest => dest.PODate, opt => opt.MapFrom(src => src.PODate))
            .ForMember(dest => dest.DeliveryDate, opt => opt.MapFrom(src => src.DeliveryDate))
            .ForMember(dest => dest.CancelRequestBy, opt => opt.MapFrom(src => src.CancelRequestBy))
            .ForMember(dest => dest.DMSPRNo, opt => opt.MapFrom(src => src.DMSPRNo))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SparePartPO, SparePartPOResponse>()
            .ForMember(dest => dest.PONumber, opt => opt.MapFrom(src => src.PONumber))
            .ForMember(dest => dest.PODate, opt => opt.MapFrom(src => src.PODate))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SparePartPO, SparePartPOOtherDto>()
                .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.Dealer.DealerCode))
                .ForMember(dest => dest.DMSPRNo, opt => opt.MapFrom(src => src.DMSPRNo))
                .ForMember(dest => dest.OrderType, opt => opt.MapFrom(src => src.OrderType))
                .ForMember(dest => dest.PickingTicket, opt => opt.MapFrom(src => src.PickingTicket))
                .ForMember(dest => dest.PODate, opt => opt.MapFrom(src => src.PODate))
                .ForMember(dest => dest.PONumber, opt => opt.MapFrom(src => src.PONumber))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

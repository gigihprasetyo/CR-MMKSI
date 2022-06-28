#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DepositLineProfile mapper class
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
    public class DepositLineProfile : Profile
    {
        public DepositLineProfile()
        {
            CreateMap<DepositLine, DepositLineDto>()
                //.ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                //.ForMember(dest => dest.DocumentNo, opt => opt.MapFrom(src => src.DocumentNo))
            .ForMember(dest => dest.PostingDate, opt => opt.MapFrom(src => src.PostingDate))
                //.ForMember(dest => dest.ClearingDate, opt => opt.MapFrom(src => src.ClearingDate))
            .ForMember(dest => dest.Debit, opt => opt.MapFrom(src => src.Debit))
                //.ForMember(dest => dest.Credit, opt => opt.MapFrom(src => src.Credit))
            .ForMember(dest => dest.ReferenceNo, opt => opt.MapFrom(src => src.ReferenceNo))
            .ForMember(dest => dest.InvoiceNo, opt => opt.MapFrom(src => src.InvoiceNo))
                //.ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.Remark))
                //.ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                //.ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                //.ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<DepositLineParameterDto, DepositLine>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.DocumentNo, opt => opt.MapFrom(src => src.DocumentNo))
            .ForMember(dest => dest.PostingDate, opt => opt.MapFrom(src => src.PostingDate))
            .ForMember(dest => dest.ClearingDate, opt => opt.MapFrom(src => src.ClearingDate))
            .ForMember(dest => dest.Debit, opt => opt.MapFrom(src => src.Debit))
            .ForMember(dest => dest.Credit, opt => opt.MapFrom(src => src.Credit))
            .ForMember(dest => dest.ReferenceNo, opt => opt.MapFrom(src => src.ReferenceNo))
            .ForMember(dest => dest.InvoiceNo, opt => opt.MapFrom(src => src.InvoiceNo))
            .ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.Remark))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

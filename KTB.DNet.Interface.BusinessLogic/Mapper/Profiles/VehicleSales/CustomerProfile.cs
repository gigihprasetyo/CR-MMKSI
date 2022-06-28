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
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.ReffCode, opt => opt.MapFrom(src => src.ReffCode))
                .ForMember(dest => dest.Name1, opt => opt.MapFrom(src => src.Name1))
                .ForMember(dest => dest.Name2, opt => opt.MapFrom(src => src.Name2))
                .ForMember(dest => dest.Name3, opt => opt.MapFrom(src => src.Name3))
                .ForMember(dest => dest.Alamat, opt => opt.MapFrom(src => src.Alamat))
                .ForMember(dest => dest.Kelurahan, opt => opt.MapFrom(src => src.Kelurahan))
                .ForMember(dest => dest.Kecamatan, opt => opt.MapFrom(src => src.Kecamatan))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.PreArea, opt => opt.MapFrom(src => src.PreArea))
                .ForMember(dest => dest.PrintRegion, opt => opt.MapFrom(src => src.PrintRegion))
                .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(src => src.PhoneNo))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Attachment, opt => opt.MapFrom(src => src.Attachment))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.DeletionMark, opt => opt.MapFrom(src => src.DeletionMark))
                .ForMember(dest => dest.CompleteName, opt => opt.MapFrom(src => src.CompleteName))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());

        }


    }
}

#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKDetailCustomerProfile mapper class
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
    public class SPKDetailCustomerProfile : Profile
    {
        public SPKDetailCustomerProfile()
        {
            CreateMap<SPKDetailCustomer, SPKDetailCustomerDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.DMSSPKDetailNo, opt => opt.MapFrom(src => src.DMSSPKDetailNo))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SPKDetailCustomer,SPKDetailCustomerParameterDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ReffCode, opt => opt.MapFrom(src => src.ReffCode))
                .ForMember(dest => dest.TipeCustomer, opt => opt.MapFrom(src => src.TipeCustomer))
                .ForMember(dest => dest.TipePerusahaan, opt => opt.MapFrom(src => src.TipePerusahaan))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Name1))
                .ForMember(dest => dest.CustomerName2, opt => opt.MapFrom(src => src.Name2))
                .ForMember(dest => dest.Gedung, opt => opt.MapFrom(src => src.Name3))
                .ForMember(dest => dest.Alamat, opt => opt.MapFrom(src => src.Alamat))
                .ForMember(dest => dest.Kelurahan, opt => opt.MapFrom(src => src.Kelurahan))
                .ForMember(dest => dest.Kecamatan, opt => opt.MapFrom(src => src.Kecamatan))
                .ForMember(dest => dest.KODEPOS, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.PreArea, opt => opt.MapFrom(src => src.PreArea))
                .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(src => src.PhoneNo))
                .ForMember(dest => dest.OfficeNo, opt => opt.MapFrom(src => src.OfficeNo))
                .ForMember(dest => dest.HomeNo, opt => opt.MapFrom(src => src.HomeNo))
                .ForMember(dest => dest.HpNo, opt => opt.MapFrom(src => src.HpNo))
                .ForMember(dest => dest.EMAIL, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.LKPPReference, opt => opt.MapFrom(src => src.LKPPReference))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.PrintRegion, opt => opt.MapFrom(src => src.PrintRegion))
                .ForMember(dest => dest.DMSSPKDetailNo, opt => opt.MapFrom(src => src.DMSSPKDetailNo))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SPKDetailCustomerParameterDto, SPKDetailCustomer>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ReffCode, opt => opt.MapFrom(src => src.ReffCode))
                .ForMember(dest => dest.TipeCustomer, opt => opt.MapFrom(src => src.TipeCustomer))
                .ForMember(dest => dest.TipePerusahaan, opt => opt.MapFrom(src => src.TipePerusahaan))
                .ForMember(dest => dest.Name1, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.Name2, opt => opt.MapFrom(src => src.CustomerName2))
                .ForMember(dest => dest.Name3, opt => opt.MapFrom(src => src.Gedung))
                .ForMember(dest => dest.Alamat, opt => opt.MapFrom(src => src.Alamat))
                .ForMember(dest => dest.Kelurahan, opt => opt.MapFrom(src => src.Kelurahan))
                .ForMember(dest => dest.Kecamatan, opt => opt.MapFrom(src => src.Kecamatan))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.KODEPOS))
                .ForMember(dest => dest.PreArea, opt => opt.MapFrom(src => src.PreArea))
                .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(src => src.PhoneNo))
                .ForMember(dest => dest.OfficeNo, opt => opt.MapFrom(src => src.OfficeNo))
                .ForMember(dest => dest.HomeNo, opt => opt.MapFrom(src => src.HomeNo))
                .ForMember(dest => dest.HpNo, opt => opt.MapFrom(src => src.HpNo))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EMAIL))
                .ForMember(dest => dest.LKPPReference, opt => opt.MapFrom(src => src.LKPPReference))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.PrintRegion, opt => opt.MapFrom(src => src.PrintRegion))
                .ForMember(dest => dest.DMSSPKDetailNo, opt => opt.MapFrom(src => src.DMSSPKDetailNo))
                .ForAllOtherMembers(opt => opt.Ignore());

        }
    }
}

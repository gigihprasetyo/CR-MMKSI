#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomerVehicleProfile mapper class
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
    public class CustomerVehicleProfile : Profile
    {
        public CustomerVehicleProfile()
        {
            CreateMap<CustomerRequest, CustomerVehicleDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Alamat, opt => opt.MapFrom(src => src.Alamat))
                .ForMember(dest => dest.CityID, opt => opt.MapFrom(src => src.CityID))
                .ForMember(dest => dest.DealerID, opt => opt.MapFrom(src => src.Dealer.ID))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Kecamatan, opt => opt.MapFrom(src => src.Kecamatan))
                .ForMember(dest => dest.Kelurahan, opt => opt.MapFrom(src => src.Kelurahan))
                .ForMember(dest => dest.Name1, opt => opt.MapFrom(src => src.Name1))
                .ForMember(dest => dest.Name2, opt => opt.MapFrom(src => src.Name2))
                .ForMember(dest => dest.Gedung, opt => opt.MapFrom(src => src.Name3))
                .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(src => src.PhoneNo))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.PreArea, opt => opt.MapFrom(src => src.PreArea))
                .ForMember(dest => dest.ReffCode, opt => opt.MapFrom(src => src.ReffCode))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Status1, opt => opt.MapFrom(src => src.Status1))
                .ForMember(dest => dest.TipePerusahaan, opt => opt.MapFrom(src => src.TipePerusahaan))
                .ForMember(dest => dest.ProcessDate, opt => opt.MapFrom(src => src.ProcessDate))
                .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => src.RequestDate))
                .ForMember(dest => dest.CustomerRequestProfiles, opt => opt.MapFrom(src => src.CustomerRequestProfiles))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))

                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<CustomerVehicleParameterDto, CustomerRequest>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Alamat, opt => opt.MapFrom(src => src.Alamat))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Kecamatan, opt => opt.MapFrom(src => src.Kecamatan))
                .ForMember(dest => dest.Kelurahan, opt => opt.MapFrom(src => src.Kelurahan))
                .ForMember(dest => dest.RequestType, opt => opt.MapFrom(src => src.RequestType))
                .ForMember(dest => dest.Name1, opt => opt.MapFrom(src => src.Name1))
                .ForMember(dest => dest.Name2, opt => opt.MapFrom(src => src.Name2))
                .ForMember(dest => dest.Name3, opt => opt.MapFrom(src => src.Gedung))
                .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(src => src.PhoneNo))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.PreArea, opt => opt.MapFrom(src => src.PreArea))
                .ForMember(dest => dest.ReffCode, opt => opt.MapFrom(src => src.ReffCode))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Status1, opt => opt.MapFrom(src => src.Status1))
                .ForMember(dest => dest.TipePerusahaan, opt => opt.MapFrom(src => src.TipePerusahaan))
                .ForMember(dest => dest.ProcessDate, opt => opt.MapFrom(src => src.ProcessDate))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))

                .ForAllOtherMembers(opt => opt.Ignore());
            
        }
    }
}

#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SalesmanHeaderProfile mapper class
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
    public class SalesmanHeaderProfile : Profile
    {
        public SalesmanHeaderProfile()
        {
            // CreateMap <source, destination>()
            CreateMap<SalesmanHeader, SalesmanHeaderDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.SalesmanCode, opt => opt.MapFrom(src => src.SalesmanCode))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.PlaceOfBirth, opt => opt.MapFrom(src => src.PlaceOfBirth))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.ShopSiteNumber, opt => opt.MapFrom(src => src.ShopSiteNumber))
                .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate))
                .ForMember(dest => dest.JobPositionId_Second, opt => opt.MapFrom(src => src.JobPositionId_Second))
                .ForMember(dest => dest.JobPositionId_Third, opt => opt.MapFrom(src => src.JobPositionId_Third))
                .ForMember(dest => dest.LeaderId, opt => opt.MapFrom(src => src.LeaderId))
                .ForMember(dest => dest.JobPositionId_Leader, opt => opt.MapFrom(src => src.JobPositionId_Leader))
                .ForMember(dest => dest.RegisterStatus, opt => opt.MapFrom(src => src.RegisterStatus))
                .ForMember(dest => dest.MarriedStatus, opt => opt.MapFrom(src => src.MarriedStatus))
                .ForMember(dest => dest.ResignType, opt => opt.MapFrom(src => src.ResignType))
                .ForMember(dest => dest.ResignDate, opt => opt.MapFrom(src => src.ResignDate))
                .ForMember(dest => dest.ResignReason, opt => opt.MapFrom(src => src.ResignReason))
                .ForMember(dest => dest.SalesIndicator, opt => opt.MapFrom(src => src.SalesIndicator))
                .ForMember(dest => dest.SalesUnitIndicator, opt => opt.MapFrom(src => src.SalesUnitIndicator))
                .ForMember(dest => dest.MechanicIndicator, opt => opt.MapFrom(src => src.MechanicIndicator))
                .ForMember(dest => dest.SparePartIndicator, opt => opt.MapFrom(src => src.SparePartIndicator))
                .ForMember(dest => dest.SPAdminIndicator, opt => opt.MapFrom(src => src.SPAdminIndicator))
                .ForMember(dest => dest.SPWareHouseIndicator, opt => opt.MapFrom(src => src.SPWareHouseIndicator))
                .ForMember(dest => dest.SPCounterIndicator, opt => opt.MapFrom(src => src.SPCounterIndicator))
                .ForMember(dest => dest.IsRequestID, opt => opt.MapFrom(src => src.IsRequestID))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartMasterProfile mapper class
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
    public class SparePartMasterProfile : Profile
    {
        public SparePartMasterProfile()
        {
            CreateMap<SparePartMaster, SparePartMasterDto>()
                //.ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                //.ForMember(dest => dest.ProductCategoryID, opt => opt.MapFrom(src => src.ProductCategory.ID))
            .ForMember(dest => dest.PartNumber, opt => opt.MapFrom(src => src.PartNumber))
            .ForMember(dest => dest.PartName, opt => opt.MapFrom(src => src.PartName))
            .ForMember(dest => dest.AltPartNumber, opt => opt.MapFrom(src => src.AltPartNumber))
            .ForMember(dest => dest.AltPartName, opt => opt.MapFrom(src => src.AltPartName))
            .ForMember(dest => dest.PartCode, opt => opt.MapFrom(src => src.PartCode))
            .ForMember(dest => dest.ModelCode, opt => opt.MapFrom(src => src.ModelCode))
                //.ForMember(dest => dest.SupplierCode, opt => opt.MapFrom(src => src.SupplierCode))
            .ForMember(dest => dest.TypeCode, opt => opt.MapFrom(src => src.TypeCode))
                //.ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock))
            .ForMember(dest => dest.RetailPrice, opt => opt.MapFrom(src => src.RetalPrice))
                //.ForMember(dest => dest.PartStatus, opt => opt.MapFrom(src => src.PartStatus))
             .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ActiveStatus))
                //.ForMember(dest => dest.AccessoriesType, opt => opt.MapFrom(src => src.AccessoriesType))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SparePartMasterParameterDto, SparePartMaster>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                //.ForMember(dest => dest.ProductCategoryID, opt => opt.MapFrom(src => src.ProductCategoryID))
            .ForMember(dest => dest.PartNumber, opt => opt.MapFrom(src => src.PartNumber))
            .ForMember(dest => dest.PartName, opt => opt.MapFrom(src => src.PartName))
            .ForMember(dest => dest.AltPartNumber, opt => opt.MapFrom(src => src.AltPartNumber))
            .ForMember(dest => dest.AltPartName, opt => opt.MapFrom(src => src.AltPartName))
            .ForMember(dest => dest.PartCode, opt => opt.MapFrom(src => src.PartCode))
            .ForMember(dest => dest.ModelCode, opt => opt.MapFrom(src => src.ModelCode))
            .ForMember(dest => dest.SupplierCode, opt => opt.MapFrom(src => src.SupplierCode))
            .ForMember(dest => dest.TypeCode, opt => opt.MapFrom(src => src.TypeCode))
            .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock))
            .ForMember(dest => dest.RetalPrice, opt => opt.MapFrom(src => src.RetalPrice))
            .ForMember(dest => dest.PartStatus, opt => opt.MapFrom(src => src.PartStatus))
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
            .ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.AccessoriesType, opt => opt.MapFrom(src => src.AccessoriesType))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

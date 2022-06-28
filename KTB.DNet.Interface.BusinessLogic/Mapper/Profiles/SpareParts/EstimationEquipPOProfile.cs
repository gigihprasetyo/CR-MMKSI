#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EstimationEquipPOProfile mapper class
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
    public class EstimationEquipPOProfile : Profile
    {
        public EstimationEquipPOProfile()
        {
            CreateMap<EstimationEquipPO, EstimationEquipPODto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.EstimationEquipDetailID, opt => opt.MapFrom(src => src.EstimationEquipDetailID))
                //.ForMember(dest => dest.IndentPartDetailID, opt => opt.MapFrom(src => src.IndentPartDetailID))
            .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(src => src.LastUpdatedBy))
            .ForMember(dest => dest.LastUpdatedTime, opt => opt.MapFrom(src => src.LastUpdatedTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<EstimationEquipPOParameterDto, EstimationEquipPO>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.EstimationEquipDetailID, opt => opt.MapFrom(src => src.EstimationEquipDetailID))
                //.ForMember(dest => dest.IndentPartDetailID, opt => opt.MapFrom(src => src.IndentPartDetailID))
            .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(src => src.LastUpdatedBy))
            .ForMember(dest => dest.LastUpdatedTime, opt => opt.MapFrom(src => src.LastUpdatedTime))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

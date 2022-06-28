#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartUoMProfile mapper class
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
    public class SparePartUoMProfile : Profile
    {
        public SparePartUoMProfile()
        {
            CreateMap<VWI_SparePartUoM, VWI_SparePartUoMDto>()
            .ForMember(dest => dest.UoM, opt => opt.MapFrom(src => src.UoM))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SparePartUoMParameterDto, VWI_SparePartUoM>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.UoM, opt => opt.MapFrom(src => src.UoM))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

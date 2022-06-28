#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_SparePartPriceListProfile mapper class
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
    public class VWI_SparePartPriceListProfile : Profile
    {
        public VWI_SparePartPriceListProfile()
        {
            CreateMap<SparePartPriceListParameterDto, VWI_SparePartPriceList>()
                 .ForMember(dest => dest.ErrorMessage, opt => opt.Ignore());
            CreateMap<VWI_SparePartPriceList, VWI_SparePartPriceListDto>();
            //.ForMember(dest => dest., opt => opt.Ignore());
        }
    }
}



#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartSalesOrderDetailProfile mapper class
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
    public class SparePartSalesOrderDetailProfile : Profile
    {
        public SparePartSalesOrderDetailProfile()
        {
            CreateMap<SparePartSalesOrderDetailParameterDto, SparePartSalesOrderDetail>()
                .ForMember(dest => dest.ErrorMessage, opt => opt.Ignore())
                .ForMember(dest => dest.SparePartSalesOrder, opt => opt.Ignore());
        }
    }
}

#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EmployeeSalesProfile mapper class
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
    public class EmployeeSalesProfile : Profile
    {
        public EmployeeSalesProfile()
        {
            // SalesmanHeader => EmployeeSalesDto
            CreateMap<SalesmanHeader, EmployeeSalesDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.Dealer.DealerCode))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

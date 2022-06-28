#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ARReceiptDetailProfile mapper class
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
    public class ARReceiptDetailProfile : Profile
    {
        public ARReceiptDetailProfile()
        {
            CreateMap<ARReceiptDetailParameterDto, ARReceiptDetail>()
                .ForMember(dest => dest.ErrorMessage, opt => opt.Ignore())
                .ForMember(dest => dest.ARReceipt, opt => opt.Ignore());
        }
    }
}

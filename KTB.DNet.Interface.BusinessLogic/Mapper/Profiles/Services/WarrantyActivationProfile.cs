#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PMHeaderProfile mapper class
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
    public class WarrantyActivationProfile : Profile
    {
        public WarrantyActivationProfile()
        {
            CreateMap<WarrantyActivation, WarrantyActivationDto>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                    .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisMaster.ChassisNumber))
                    .ForMember(dest => dest.PDIDate, opt => opt.MapFrom(src => src.PDI.PDIDate.Date))
                    .ForMember(dest => dest.PDIDealerCode, opt => opt.MapFrom(src => src.PDI.Dealer.DealerCode))
                    .ForMember(dest => dest.PKTDate, opt => opt.MapFrom(src => src.ChassisMasterPKT.PKTDate.Date))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                    .ForMember(dest => dest.WADate, opt => opt.MapFrom(src => src.WADate.Date))
                    .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
                    .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                    .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                    .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime.Date))
                    .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdatedBy))
                    .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdatedTime.Date))
                    .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

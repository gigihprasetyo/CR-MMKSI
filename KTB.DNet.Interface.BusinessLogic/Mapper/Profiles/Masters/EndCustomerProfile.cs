#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EndCustomerProfile mapper class
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
    public class EndCustomerProfile : Profile
    {
        public EndCustomerProfile()
        {
            CreateMap<EndCustomer, EndCustomerDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ProjectIndicator, opt => opt.MapFrom(src => src.ProjectIndicator))
                .ForMember(dest => dest.RefChassisNumberID, opt => opt.MapFrom(src => src.RefChassisNumberID))
                .ForMember(dest => dest.Name1, opt => opt.MapFrom(src => src.Name1))
                .ForMember(dest => dest.FakturDate, opt => opt.MapFrom(src => src.FakturDate))
                .ForMember(dest => dest.OpenFakturDate, opt => opt.MapFrom(src => src.OpenFakturDate))
                .ForMember(dest => dest.FakturNumber, opt => opt.MapFrom(src => src.FakturNumber))
                .ForMember(dest => dest.AreaViolationFlag, opt => opt.MapFrom(src => src.AreaViolationFlag))
                .ForMember(dest => dest.AreaViolationAmount, opt => opt.MapFrom(src => src.AreaViolationyAmount))
                .ForMember(dest => dest.AreaViolationBankName, opt => opt.MapFrom(src => src.AreaViolationBankName))
                .ForMember(dest => dest.AreaViolationGyroNumber, opt => opt.MapFrom(src => src.AreaViolationGyroNumber))
                .ForMember(dest => dest.PenaltyFlag, opt => opt.MapFrom(src => src.PenaltyFlag))
                .ForMember(dest => dest.PenaltyAmount, opt => opt.MapFrom(src => src.PenaltyAmount))
                .ForMember(dest => dest.PenaltyBankName, opt => opt.MapFrom(src => src.PenaltyBankName))
                .ForMember(dest => dest.PenaltyGyroNumber, opt => opt.MapFrom(src => src.PenaltyGyroNumber))
                .ForMember(dest => dest.ReferenceLetterFlag, opt => opt.MapFrom(src => src.ReferenceLetterFlag))
                .ForMember(dest => dest.ReferenceLetter, opt => opt.MapFrom(src => src.ReferenceLetter))
                .ForMember(dest => dest.SaveBy, opt => opt.MapFrom(src => src.SaveBy))
                .ForMember(dest => dest.SaveTime, opt => opt.MapFrom(src => src.SaveTime))
                .ForMember(dest => dest.ValidateBy, opt => opt.MapFrom(src => src.ValidateBy))
                .ForMember(dest => dest.ValidateTime, opt => opt.MapFrom(src => src.ValidateTime))
                .ForMember(dest => dest.ConfirmBy, opt => opt.MapFrom(src => src.ConfirmBy))
                .ForMember(dest => dest.ConfirmTime, opt => opt.MapFrom(src => src.ConfirmTime))
                .ForMember(dest => dest.DownloadBy, opt => opt.MapFrom(src => src.DownloadBy))
                .ForMember(dest => dest.DownloadTime, opt => opt.MapFrom(src => src.DownloadTime))
                .ForMember(dest => dest.PrintedBy, opt => opt.MapFrom(src => src.PrintedBy))
                .ForMember(dest => dest.PrintedTime, opt => opt.MapFrom(src => src.PrintedTime))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.MCPStatus, opt => opt.MapFrom(src => src.MCPStatus))
                .ForMember(dest => dest.LKPPStatus, opt => opt.MapFrom(src => src.LKPPStatus))
                .ForMember(dest => dest.Remark1, opt => opt.MapFrom(src => src.Remark1))
                .ForMember(dest => dest.Remark2, opt => opt.MapFrom(src => src.Remark2))
                .ForMember(dest => dest.HandoverDate, opt => opt.MapFrom(src => src.HandoverDate))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

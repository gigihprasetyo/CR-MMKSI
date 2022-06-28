#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : FreeServiceProfile mapper class
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
    public class FreeServiceProfile : Profile
    {
        public FreeServiceProfile()
        {
            CreateMap<FreeService, FreeServiceDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.MileAge, opt => opt.MapFrom(src => src.MileAge))
            .ForMember(dest => dest.ServiceDate, opt => opt.MapFrom(src => src.ServiceDate))
            .ForMember(dest => dest.SoldDate, opt => opt.MapFrom(src => src.SoldDate))
            .ForMember(dest => dest.NotificationNumber, opt => opt.MapFrom(src => src.NotificationNumber))
            .ForMember(dest => dest.NotificationType, opt => opt.MapFrom(src => src.NotificationType))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.LabourAmount, opt => opt.MapFrom(src => src.LabourAmount))
            .ForMember(dest => dest.PartAmount, opt => opt.MapFrom(src => src.PartAmount))
            .ForMember(dest => dest.PPNAmount, opt => opt.MapFrom(src => src.PPNAmount))
            .ForMember(dest => dest.PPHAmount, opt => opt.MapFrom(src => src.PPHAmount))
            .ForMember(dest => dest.Reject, opt => opt.MapFrom(src => src.Reject))
            .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.Reason.ID))
            .ForMember(dest => dest.ReasonCode, opt => opt.MapFrom(src => src.Reason.ReasonCode))
            .ForMember(dest => dest.ReasonDesc, opt => opt.MapFrom(src => src.Reason.Description))
            .ForMember(dest => dest.ReleaseBy, opt => opt.MapFrom(src => src.ReleaseBy))
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
            .ForMember(dest => dest.WorkOrderNumber, opt => opt.MapFrom(src => src.WorkOrderNumber))
            .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.Dealer.DealerCode))
            .ForMember(dest => dest.DealerBranchCode, opt => opt.MapFrom(src => src.DealerBranch.DealerBranchCode))
            .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisMaster.ChassisNumber))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<FreeServiceParameterDto, FreeService>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.MileAge, opt => opt.MapFrom(src => src.MileAge))
            .ForMember(dest => dest.ServiceDate, opt => opt.MapFrom(src => src.ServiceDate))
            .ForMember(dest => dest.SoldDate, opt => opt.MapFrom(src => src.SoldDate))
            .ForMember(dest => dest.NotificationNumber, opt => opt.MapFrom(src => src.NotificationNumber))
            .ForMember(dest => dest.NotificationType, opt => opt.MapFrom(src => src.NotificationType))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.LabourAmount, opt => opt.MapFrom(src => src.LabourAmount))
            .ForMember(dest => dest.PartAmount, opt => opt.MapFrom(src => src.PartAmount))
            .ForMember(dest => dest.PPNAmount, opt => opt.MapFrom(src => src.PPNAmount))
            .ForMember(dest => dest.PPHAmount, opt => opt.MapFrom(src => src.PPHAmount))
            .ForMember(dest => dest.Reject, opt => opt.MapFrom(src => src.Reject))
            .ForMember(dest => dest.ReleaseBy, opt => opt.MapFrom(src => src.ReleaseBy))
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
            .ForMember(dest => dest.WorkOrderNumber, opt => opt.MapFrom(src => src.WorkOrderNumber))
            .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<FreeServiceBB, FreeServiceDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.MileAge, opt => opt.MapFrom(src => src.MileAge))
            .ForMember(dest => dest.ServiceDate, opt => opt.MapFrom(src => src.ServiceDate))
            .ForMember(dest => dest.SoldDate, opt => opt.MapFrom(src => src.SoldDate))
            .ForMember(dest => dest.NotificationNumber, opt => opt.MapFrom(src => src.NotificationNumber))
            .ForMember(dest => dest.NotificationType, opt => opt.MapFrom(src => src.NotificationType))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.LabourAmount, opt => opt.MapFrom(src => src.LabourAmount))
            .ForMember(dest => dest.PartAmount, opt => opt.MapFrom(src => src.PartAmount))
            .ForMember(dest => dest.PPNAmount, opt => opt.MapFrom(src => src.PPNAmount))
            .ForMember(dest => dest.PPHAmount, opt => opt.MapFrom(src => src.PPHAmount))
            .ForMember(dest => dest.Reject, opt => opt.MapFrom(src => src.Reject))
            .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.Reason.ID))
            .ForMember(dest => dest.ReasonCode, opt => opt.MapFrom(src => src.Reason.ReasonCode))
            .ForMember(dest => dest.ReasonDesc, opt => opt.MapFrom(src => src.Reason.Description))
            .ForMember(dest => dest.ReleaseBy, opt => opt.MapFrom(src => src.ReleaseBy))
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
            .ForMember(dest => dest.WorkOrderNumber, opt => opt.MapFrom(src => src.WorkOrderNumber))
            .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.Dealer.DealerCode))
            .ForMember(dest => dest.DealerBranchCode, opt => opt.MapFrom(src => src.DealerBranch.DealerBranchCode))
            .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.ChassisMasterBB.ChassisNumber))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<FreeServiceParameterDto, FreeServiceBB>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.MileAge, opt => opt.MapFrom(src => src.MileAge))
            .ForMember(dest => dest.ServiceDate, opt => opt.MapFrom(src => src.ServiceDate))
            .ForMember(dest => dest.SoldDate, opt => opt.MapFrom(src => src.SoldDate))
            .ForMember(dest => dest.NotificationNumber, opt => opt.MapFrom(src => src.NotificationNumber))
            .ForMember(dest => dest.NotificationType, opt => opt.MapFrom(src => src.NotificationType))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.LabourAmount, opt => opt.MapFrom(src => src.LabourAmount))
            .ForMember(dest => dest.PartAmount, opt => opt.MapFrom(src => src.PartAmount))
            .ForMember(dest => dest.PPNAmount, opt => opt.MapFrom(src => src.PPNAmount))
            .ForMember(dest => dest.PPHAmount, opt => opt.MapFrom(src => src.PPHAmount))
            .ForMember(dest => dest.Reject, opt => opt.MapFrom(src => src.Reject))
            .ForMember(dest => dest.ReleaseBy, opt => opt.MapFrom(src => src.ReleaseBy))
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
            .ForMember(dest => dest.WorkOrderNumber, opt => opt.MapFrom(src => src.WorkOrderNumber))
            .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

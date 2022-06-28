#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistServiceIncomingProfile mapper class
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
    public class AssistServiceIncomingProfile : Profile
    {
        public AssistServiceIncomingProfile()
        {
            CreateMap<AssistServiceIncoming, AssistServiceIncomingDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                //.ForMember(dest => dest.AssistUploadLogID, opt => opt.MapFrom(src => src.AssistUploadLog.ID))
            .ForMember(dest => dest.TglBukaTransaksi, opt => opt.MapFrom(src => src.TglBukaTransaksi))
            .ForMember(dest => dest.WaktuMasuk, opt => opt.MapFrom(src => src.WaktuMasuk))
            .ForMember(dest => dest.TglTutupTransaksi, opt => opt.MapFrom(src => src.TglTutupTransaksi))
            .ForMember(dest => dest.WaktuKeluar, opt => opt.MapFrom(src => src.WaktuKeluar))
                //.ForMember(dest => dest.DealerID, opt => opt.MapFrom(src => src.Dealer.ID))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.DealerBranchCode, opt => opt.MapFrom(src => src.DealerBranchCode))
            .ForMember(dest => dest.TrTraineMekanikID, opt => opt.MapFrom(src => src.TrTraineMekanikID))
            .ForMember(dest => dest.KodeMekanik, opt => opt.MapFrom(src => src.KodeMekanik))
            .ForMember(dest => dest.NoWorkOrder, opt => opt.MapFrom(src => src.NoWorkOrder))
                //.ForMember(dest => dest.ChassisMasterID, opt => opt.MapFrom(src => src.ChassisMaster.ID))
            .ForMember(dest => dest.KodeChassis, opt => opt.MapFrom(src => src.KodeChassis))
                //.ForMember(dest => dest.WorkOrderCategoryID, opt => opt.MapFrom(src => src.AssistWorkOrderCategory.ID))
            .ForMember(dest => dest.WorkOrderCategoryCode, opt => opt.MapFrom(src => src.WorkOrderCategoryCode))
            .ForMember(dest => dest.KMService, opt => opt.MapFrom(src => src.KMService))
                //.ForMember(dest => dest.ServicePlaceID, opt => opt.MapFrom(src => src.AssistServicePlace.ID))
            .ForMember(dest => dest.ServicePlaceCode, opt => opt.MapFrom(src => src.ServicePlaceCode))
                //.ForMember(dest => dest.ServiceTypeID, opt => opt.MapFrom(src => src.AssistServiceType.ID))
            .ForMember(dest => dest.ServiceTypeCode, opt => opt.MapFrom(src => src.AssistServiceType == null ? src.ServiceTypeCode : src.AssistServiceType.ServiceTypeCode))
            .ForMember(dest => dest.TotalLC, opt => opt.MapFrom(src => src.TotalLC))
            .ForMember(dest => dest.MetodePembayaran, opt => opt.MapFrom(src => src.MetodePembayaran))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.Transmition, opt => opt.MapFrom(src => src.Transmition))
            .ForMember(dest => dest.DriveSystem, opt => opt.MapFrom(src => src.DriveSystem))
            .ForMember(dest => dest.RemarksSystem, opt => opt.MapFrom(src => src.RemarksSystem))
            .ForMember(dest => dest.RemarksSpecial, opt => opt.MapFrom(src => src.RemarksSpecial))
            .ForMember(dest => dest.RemarksBM, opt => opt.MapFrom(src => src.RemarksBM))
            .ForMember(dest => dest.StatusAktif, opt => opt.MapFrom(src => src.StatusAktif))
            .ForMember(dest => dest.WOStatus, opt => opt.MapFrom(src => src.WOStatus))
            .ForMember(dest => dest.ValidateSystemStatus, opt => opt.MapFrom(src => src.ValidateSystemStatus))
            .ForMember(dest => dest.CustomerOwnerName, opt => opt.MapFrom(src => src.CustomerOwnerName))
            .ForMember(dest => dest.CustomerOwnerPhoneNumber, opt => opt.MapFrom(src => src.CustomerOwnerPhoneNumber))
            .ForMember(dest => dest.CustomerVisitName, opt => opt.MapFrom(src => src.CustomerVisitName))
            .ForMember(dest => dest.CustomerVisitPhoneNumber, opt => opt.MapFrom(src => src.CustomerVisitPhoneNumber))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<AssistServiceIncomingParameterDto, AssistServiceIncoming>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                //.ForPath(dest => dest.AssistUploadLog.ID, opt => opt.MapFrom(src => src.AssistUploadLogID))
            .ForMember(dest => dest.TglBukaTransaksi, opt => opt.MapFrom(src => src.TglBukaTransaksi))
            .ForMember(dest => dest.WaktuMasuk, opt => opt.MapFrom(src => src.WaktuMasuk))
            .ForMember(dest => dest.TglTutupTransaksi, opt => opt.MapFrom(src => src.TglTutupTransaksi))
            .ForMember(dest => dest.WaktuKeluar, opt => opt.MapFrom(src => src.WaktuKeluar))
                //.ForPath(dest => dest.Dealer.ID, opt => opt.MapFrom(src => src.DealerID))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.DealerBranchCode, opt => opt.MapFrom(src => src.DealerBranchCode))
            .ForMember(dest => dest.TrTraineMekanikID, opt => opt.MapFrom(src => src.TrTraineMekanikID))
            .ForMember(dest => dest.KodeMekanik, opt => opt.MapFrom(src => src.KodeMekanik))
            .ForMember(dest => dest.NoWorkOrder, opt => opt.MapFrom(src => src.NoWorkOrder))
                //.ForPath(dest => dest.ChassisMaster.ID, opt => opt.MapFrom(src => src.ChassisMasterID))
            .ForMember(dest => dest.KodeChassis, opt => opt.MapFrom(src => src.KodeChassis))
                //.ForPath(dest => dest.AssistWorkOrderCategory.ID, opt => opt.MapFrom(src => src.WorkOrderCategoryID))
            .ForMember(dest => dest.WorkOrderCategoryCode, opt => opt.MapFrom(src => src.WorkOrderCategoryCode))
            .ForMember(dest => dest.KMService, opt => opt.MapFrom(src => src.KMService))
                //.ForPath(dest => dest.AssistServicePlace.ID, opt => opt.MapFrom(src => src.ServicePlaceID))
            .ForMember(dest => dest.ServicePlaceCode, opt => opt.MapFrom(src => src.ServicePlaceCode))
                //.ForPath(dest => dest.AssistServiceType.ID, opt => opt.MapFrom(src => src.ServiceTypeID))
            .ForMember(dest => dest.ServiceTypeCode, opt => opt.MapFrom(src => src.ServiceTypeCode))
            .ForMember(dest => dest.TotalLC, opt => opt.MapFrom(src => src.TotalLC))
            .ForMember(dest => dest.MetodePembayaran, opt => opt.MapFrom(src => src.MetodePembayaran))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.Transmition, opt => opt.MapFrom(src => src.Transmition))
            .ForMember(dest => dest.DriveSystem, opt => opt.MapFrom(src => src.DriveSystem))
            .ForMember(dest => dest.RemarksSystem, opt => opt.MapFrom(src => src.RemarksSystem))
            .ForMember(dest => dest.RemarksSpecial, opt => opt.MapFrom(src => src.RemarksSpecial))
            .ForMember(dest => dest.RemarksBM, opt => opt.MapFrom(src => src.RemarksBM))
            .ForMember(dest => dest.StatusAktif, opt => opt.MapFrom(src => src.StatusAktif))
            .ForMember(dest => dest.WOStatus, opt => opt.MapFrom(src => src.WOStatus))
            .ForMember(dest => dest.ValidateSystemStatus, opt => opt.MapFrom(src => src.ValidateSystemStatus))
            .ForMember(dest => dest.CustomerOwnerName, opt => opt.MapFrom(src => src.CustomerOwnerName))
            .ForMember(dest => dest.CustomerOwnerPhoneNumber, opt => opt.MapFrom(src => src.CustomerOwnerPhoneNumber))
            .ForMember(dest => dest.CustomerVisitName, opt => opt.MapFrom(src => src.CustomerVisitName))
            .ForMember(dest => dest.CustomerVisitPhoneNumber, opt => opt.MapFrom(src => src.CustomerVisitPhoneNumber))
            .ForMember(dest => dest.StallCode, opt => opt.MapFrom(src => src.StallCode))
            //.ForMember(dest => dest.BookingCode, opt => opt.MapFrom(src => src.BookingCode))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
